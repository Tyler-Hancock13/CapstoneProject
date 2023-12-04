using CapstoneProject.Model.Entities;
using CapstoneProject.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CapstoneProject.Model.Types;

namespace CapstoneProject.Service
{
    public class RequestService
    {
        private PurchaseOrder _purchaseOrder = new PurchaseOrder();
        private PurchaseOrderItem _item = new PurchaseOrderItem();
        private RequestRepo requestRepo = new RequestRepo();
        private IEmployeeService employeeService;
        private IEmailService emailService;
        private EmailService s = new EmailService();
        private EmployeeService empService = new EmployeeService();

        public RequestService(IEmailService emailService, IEmployeeService employeeService)
        {
            this.emailService = emailService;
            this.employeeService = employeeService;
        }

        public RequestService()
        {

        }

        /// <summary>
        /// Inserts a request into the database
        /// </summary>
        /// <param name="order">The PurchaseOrder to be inserted</param>
        /// <returns>A boolean value indicating if a PurchaseOrder was successfully submitted</returns>
        public int InsertRequestAndItem(PurchaseOrder order, PurchaseOrderItem item)
        {

            if(Validate(item))
            {
                Employee employee = new Employee();
                employee = empService.Get(order.EmployeeId);
                int returnIndex = CheckIfDuplicateItem(item);

                if (returnIndex == -1)
                {
                    _purchaseOrder = CreateNewPurchaseOrder(order, employee);
                    _item = item;
                    _purchaseOrder.Status = RequestStatus.Pending;
                    _purchaseOrder.Items.Add(_item);
                    CalculateOrderTotalCost(_purchaseOrder);

                    int id =  requestRepo.InsertRequestAndItem(_purchaseOrder, _item);
                    return id;
                }
                else
                {
                    Update(item);

                    PurchaseOrder po = requestRepo.GetPurchaseOrderByPOID(item.PONumber);
                    po.Items.Add(item);
                    CalculateOrderTotalCost(po);
                    Update(po);
                }
            }

            return 0;
        }

        /// <summary>
        /// Retrieves PurchaseOrders by employee ID
        /// </summary>
        /// <param name="id">The Employee ID to get PurchaseOrders by</param>
        /// <returns>A list that contains all PurchaseOrders that correlate with the employee ID parameter</returns>
        public List<PurchaseOrder> GetPurchaseOrderByEmployeeId(string id)
        {
            return requestRepo.SelectById(id);
        }

        /// <summary>
        /// Retrieves PurchaseOrderItems by PurchaseOrder ID
        /// </summary>
        /// <param name="id">The PurchaseOrder ID to get PurchaseOrderItems by</param>
        /// <returns>A list that contains all PurchaseOrderItems that correlate with the PurchaseOrder ID</returns>
        public List<PurchaseOrderItem> GetPurchaseOrderItemByPOId(int id)
        {
            List<PurchaseOrderItem> items = requestRepo.GetItemsByPOId(id);
            _purchaseOrder = requestRepo.GetPurchaseOrderByPOID(id);
            _purchaseOrder.EmployeeName = "John Doe";
            foreach(PurchaseOrderItem i in items)
            {
                _purchaseOrder.Items.Add(i);
            }
            return items;
        }

        public PurchaseOrder GetPurchaseOrder(int id)
        {
            return requestRepo.GetPurchaseOrderByPOID(id);
        }

        public PurchaseOrderItem Get(int id)
        {
            return requestRepo.GetItemById(id);
        }

        public int InsertItem(PurchaseOrderItem item)
        {
            _item = item;
            if (Validate(_item))
            {
                int returnId = CheckIfDuplicateItem(_item);

                if (returnId != -1)
                {
                    item = MergeDuplicateItem(returnId, _item);
                    item = requestRepo.UpdateItem(item);
                    //return item.Id;
                    return 0;
                }
                else
                {
                    int id = requestRepo.InsertItem(item);
                    return id;
                }
            }

            return -1;
        }

        public PurchaseOrder Update(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.Items = requestRepo.GetItemsByPOId(purchaseOrder.PONumber);

            if(purchaseOrder.Status == RequestStatus.Pending)
            {
                purchaseOrder.Status = RequestStatus.Pending;
            }

            CalculateOrderTotalCost(purchaseOrder);
            purchaseOrder = requestRepo.UpdateOrder(purchaseOrder);
            return purchaseOrder;
        }

        public PurchaseOrderItem Update(PurchaseOrderItem item)
        {
            _item = item;

            byte[] previousTimestamp = item.Timestamp;

            if(item.Status != ItemStatus.Pending)
            {
                requestRepo.UpdateItem(item);
            }
            else
            {
                if (Validate(item))
                {
                    //int returnId = CheckIfDuplicateItem(_item);

                    //if (returnId != -1)
                    //{
                        //_item = MergeDuplicateItem(returnId, _item);

                        item = requestRepo.UpdateItem(_item);
                        //item.Errors.Add(new Error($"Item was a duplicate. Item has been merged with ID: {item.Id}", ErrorType.Business));

                        if (item.Timestamp == previousTimestamp)
                        {
                            item.AddError(new Error("The item has been modified since you last retrieved it. Please try again.", ErrorType.Business));
                        }

                        if (item != null)
                        {
                            return item;
                        }
                    }

                    if (_item.Id > 0)
                    {
                        //if(_item.Timestamp == previousTimestamp)
                        //{
                        //    item.AddError(new Error("The item has been modified since you last retrieved it. Please try again.", ErrorType.Business));
                        //}

                        item = requestRepo.UpdateItem(_item);

                        if (item != null)
                        {
                            return item;
                        }
                    }

                    InsertItem(item);
                //}
            }


            return item;
        }

        public PurchaseOrderItem ProcessItem(PurchaseOrderItem item)
        {
            if(item.Reason == "")
            {
                item.Reason = "N/A";
            }

            return requestRepo.ProcessItem(item);
        }

        public List<PurchaseOrder> GetByDepartment(int id)
        {
            return requestRepo.GetByDepartment(id);
        }

        public PurchaseOrderItem RemoveItem(int id)
        {
            _item = requestRepo.GetItemById(id);
            SetItemsAsNotNeeded(_item);
            _item = requestRepo.UpdateItem(_item);

            _purchaseOrder.Items = requestRepo.GetItemsByPOId(_item.PONumber);

            List<PurchaseOrderItem> nonPendingItems = new List<PurchaseOrderItem>();
            foreach(PurchaseOrderItem i in _purchaseOrder.Items)
            {
                if(i.Status != ItemStatus.Pending)
                {
                    nonPendingItems.Add(i);
                }
            }

            if(nonPendingItems.Count == _purchaseOrder.Items.Count)
            {
                _purchaseOrder.Status = RequestStatus.Closed;
            }

            Update(_purchaseOrder);

            return _item;
        }

        public List<PurchaseOrder> SearchById(int id, string userId)
        {
            return requestRepo.SearchById(id, userId);
        }

        public List<PurchaseOrder> SearchByDate(DateTime dateOne, DateTime dateTwo, string userId)
        {
            return requestRepo.SearchByDate(dateOne, dateTwo, userId);
        }

        public PurchaseOrderItem ApproveItem(PurchaseOrderItem item)
        {
            List<PurchaseOrderItem> nonPendingItems = new List<PurchaseOrderItem>();

            List<PurchaseOrderItem> items = new List<PurchaseOrderItem>();
            _purchaseOrder = requestRepo.GetPurchaseOrderByPOID(item.PONumber);
            _purchaseOrder.Items = requestRepo.GetItemsByPOId(item.PONumber);
            _purchaseOrder.Items.Add(item);
            items = requestRepo.GetItemsByPOId(item.PONumber);

            //if(items.Select(i => i.Status == item.Status).Count() > 0)
            //{
            //    _purchaseOrder.Status = RequestStatus.UnderReview;
            //    item.Status = ItemStatus.Approved;
            //    Update(item);
            //    Update(_purchaseOrder);
            //    return item;
            //}

            foreach(PurchaseOrderItem i in _purchaseOrder.Items)
            {
                if(i.Status != ItemStatus.Pending)
                {
                    nonPendingItems.Add(i);
                }
            }

            if(nonPendingItems.Count == _purchaseOrder.Items.Count)
            {
                _purchaseOrder.Status = RequestStatus.Closed;
                SendEmail();
                ProcessItem(item);
                Update(_purchaseOrder);
                return item;
            }
            else if(nonPendingItems.Count > 0)
            {
                _purchaseOrder.Status = RequestStatus.UnderReview;
                ProcessItem(item);
                Update(_purchaseOrder);
                return item;
            }
            else
            {

            }

            return item;
        }

        public PurchaseOrderItem DenyItem(PurchaseOrderItem item)
        {
            List<PurchaseOrderItem> deniedItems = new List<PurchaseOrderItem>();

            List<PurchaseOrderItem> items = new List<PurchaseOrderItem>();
            _purchaseOrder = requestRepo.GetPurchaseOrderByPOID(item.PONumber);
            items = requestRepo.GetItemsByPOId(item.PONumber);

            foreach (PurchaseOrderItem i in _purchaseOrder.Items)
            {
                if (i.Status == item.Status)
                {
                    deniedItems.Add(i);
                }
            }

            if (deniedItems.Count == _purchaseOrder.Items.Count)
            {
                _purchaseOrder.Status = RequestStatus.Closed;
                item.Status = ItemStatus.Denied;
                ProcessItem(item);
                Update(_purchaseOrder);
                SendEmail();
                return item;
            }
            else if (deniedItems.Count > 0)
            {
                _purchaseOrder.Status = RequestStatus.UnderReview;
                item.Status = ItemStatus.Denied;
                ProcessItem(item);
                Update(_purchaseOrder);
                return item;
            }


            return item;
        }

        private bool SendEmail()
        {
            try
            {
                Employee employeeToEmail = new Employee();

                StringBuilder sb = new StringBuilder();

                sb.Append($"Dear Employee,<br><br> This is to inform you your purchase order #{_purchaseOrder.PONumber} has been processed. You can view the purchase order <a href='http://localhost:4200/purchaseorders/1/order'>Here</a>. <br><br> Thank you, <br> Cryptech");

                Email emailToSend = new Email("test@email.com", "no-reply@cryptech.com", $"Purchase Order #{_purchaseOrder.PONumber} has been closed.", sb.ToString());
                s.Send(emailToSend);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public int CheckIfDuplicateItem(PurchaseOrderItem purchaseOrderItem)
        {
            _purchaseOrder.Items = requestRepo.GetAllItems();

            if(purchaseOrderItem.Id != 0)
            {
                _item = purchaseOrderItem;
            }

            return _purchaseOrder.Items.FindIndex(c => c.Name == purchaseOrderItem.Name); 
        }

        private PurchaseOrderItem MergeDuplicateItem(int index, PurchaseOrderItem item)
        {
            _purchaseOrder.Items = requestRepo.GetAllItems();


            _purchaseOrder.Items[index].Quantity += item.Quantity;
            _purchaseOrder.Items[index].Subtotal = _purchaseOrder.Items[index].Quantity * _purchaseOrder.Items[index].Price;

            return _purchaseOrder.Items[index];

        }

        private PurchaseOrderItem SetItemsAsNotNeeded(PurchaseOrderItem item)
        {
            item.Description = "No longer needed";
            item.Quantity = 0;
            item.Price = 0;
            item.Subtotal = 0;
            item.Status = ItemStatus.Denied;

            return item;
        }

        public PurchaseOrder CreateNewPurchaseOrder(PurchaseOrder order, Employee employee)
        {
            order = new PurchaseOrder();
            order.PONumber = 3;
            order.Status = RequestStatus.Pending;
            order.EmployeeId = employee.ID;
            order.DepartmentId = 2;
            order.SupervisorId = employee.SupervisorID;
            order.Subtotal = 0m;
            order.SalesTax = 0m;
            order.Total = 0m;
            order.Items = new List<PurchaseOrderItem>();
            order.DateCreated = DateTime.Now;

            return order;
        }

        public decimal CalculateOrderSubtotal(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.Subtotal = 0;
            foreach (PurchaseOrderItem item in purchaseOrder.Items)
            {
                purchaseOrder.Subtotal += item.Subtotal;
            }
            return purchaseOrder.Subtotal;
        }

        public decimal CalculateOrderTax(PurchaseOrder purchaseOrder)
        {

            purchaseOrder.SalesTax = purchaseOrder.Subtotal * 0.15m;

            return purchaseOrder.SalesTax;
        }

        public decimal CalculateOrderTotalCost(PurchaseOrder purchaseOrder)
        {
            decimal subtotal = CalculateOrderSubtotal(purchaseOrder);
            decimal tax = CalculateOrderTax(purchaseOrder);

            purchaseOrder.Total = subtotal + tax;

            return purchaseOrder.Total;
        }

        private bool Validate(PurchaseOrder purchaseOrder)
        {
            return purchaseOrder.Items.Count == 0;
        }

        private bool Validate(PurchaseOrderItem item)
        {
            ValidationContext context = new ValidationContext(item);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(item, context, results, true);

            foreach (ValidationResult result in results)
            {
                Error error = new Error(result.ErrorMessage, ErrorType.Model);
                item.AddError(error);
            }

            return item.Errors.Count == 0;
        }
    }
}
