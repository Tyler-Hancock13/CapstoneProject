using CapstoneProject.Model.Entities;
using CapstoneProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapstoneProject.API.Controllers
{
    [RoutePrefix("api/purchaseorders")]
    public class PurchaseOrdersController : CommonController
    {
        private RequestService requestService = new RequestService();
        

        [HttpGet]
        [Route("{employeeId}")]
        public IHttpActionResult GetById(string employeeId)
        {
            try
            {
                List<PurchaseOrder> purchaseOrders = requestService.GetPurchaseOrderByEmployeeId(employeeId);

                return Ok(purchaseOrders);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}/order")]
        public IHttpActionResult GetItemsById(int id)
        {
            try
            {
                List<PurchaseOrderItem> items = requestService.GetPurchaseOrderItemByPOId(id);

                return Ok(items);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("{employeeId}/search/{id}")]
        public IHttpActionResult GetPurchaseOrdersByPOID(int id, string employeeId)
        {
            try
            {
                List<PurchaseOrder> purchaseOrders = requestService.SearchById(id, employeeId);

                return Ok(purchaseOrders);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("{employeeId}/search/{startDate}/{endDate}")]
        public IHttpActionResult GetPurchaseOrdersByDateRange(DateTime startDate, DateTime endDate, string employeeId)
        {
            try
            {
                List<PurchaseOrder> purchaseOrders = requestService.SearchByDate(startDate, endDate, employeeId);

                return Ok(purchaseOrders);
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpDelete]
        [Route("order/{id}")]
        public IHttpActionResult RemovePurchaseOrderItem(int id)
        {
            try
            {
                PurchaseOrderItem item = new PurchaseOrderItem();
                item = requestService.RemoveItem(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        [Route("{employeeId}/create")]
        public IHttpActionResult InsertRequestAndItem(PurchaseOrderItem item, string employeeId)
        {
            try
            {
                PurchaseOrder request = new PurchaseOrder();
                request.EmployeeId = employeeId;
                int id = requestService.InsertRequestAndItem(request, item);
               
                return Content(HttpStatusCode.OK, id);
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        [Route("{poId}/order/create")]
        public IHttpActionResult InsertItem(PurchaseOrderItem item)
        {
            try
            {
                int id = requestService.InsertItem(item);

                if(id == 0)
                {
                    item = requestService.Update(item);
                    id = item.Id;
                }

                //if (requestService.InsertItem(item))
                //{
                //    List<PurchaseOrderItem> items = requestService.GetPurchaseOrderItemByPOId(1);
                //    return Ok(items);
                //}

                return Ok(id);
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("{poId}/order/edit/{id}")]
        public IHttpActionResult GetItemToEdit(int id)
        {
            try
            {
                PurchaseOrderItem item = requestService.Get(id);

                return Ok(item);
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        [Route("{poId}/order/edit/{id}")]
        public IHttpActionResult UpdateItem(int id, PurchaseOrderItem item)
        {
            try
            {
                //PurchaseOrderItem savedItem = requestService.Get(item.Id);
                //savedItem.Name = item.Name;
                //savedItem.Description = item.Description;
                //savedItem.Justification = item.Justification;
                //savedItem.Location = item.Location;
                //savedItem.Quantity = item.Quantity;
                //savedItem.Subtotal = item.Subtotal;
                
                return Ok(requestService.Update(item));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        [Route("{poId}/order")]
        public IHttpActionResult UpdateRequest(int poId)
        {
            try
            {
                PurchaseOrder order = requestService.GetPurchaseOrder(poId);
                order.Items = requestService.GetPurchaseOrderItemByPOId(poId);
                order = requestService.Update(order);

                return Ok(order);
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("{departmentId}/order/GetDepartment")]
        public IHttpActionResult GetDepartment(int departmentId)
        {
            try
            {
                DepartmentService departmentService = new DepartmentService();

                //PurchaseOrder po = requestService.GetPurchaseOrder(poId);

                return Ok(departmentService.Get(departmentId));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("{poId}/order/create")]
        public IHttpActionResult GetPurchaseOrderDetails(int poId)
        {
            try
            {
                return Ok(requestService.GetPurchaseOrder(poId));
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("process/{id}")]
        public IHttpActionResult GetPurchaseOrdersByDepartment(int id)
        {
            try
            {
                return Ok(requestService.GetByDepartment(id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        [Route("{supervisorId}/order/GetSupervisor")]
        public IHttpActionResult GetSupervisor(string supervisorId)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();

                return Ok(employeeService.GetSupervisorById(supervisorId));
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        [Route("process/{id}")]
        public IHttpActionResult ProcessPOItem(PurchaseOrderItem item)
        {
            try
            {
                if(item.Status == Model.Types.ItemStatus.Approved)
                {
                    requestService.ApproveItem(item);
                }
                else if(item.Status == Model.Types.ItemStatus.Denied)
                {
                    requestService.DenyItem(item);
                }

                return Ok(item);
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
