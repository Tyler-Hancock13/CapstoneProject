using CapstoneProject.Model.Entities;
using CapstoneProject.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CapstoneProject.Model.Types;

namespace CapstoneProject
{
    public partial class RequestDetails : Form
    {
        private DepartmentService departmentService = new DepartmentService();
        private EmployeeService employeeService = new EmployeeService();
        private RequestService requestService = new RequestService();

        private PurchaseOrderItem item = new PurchaseOrderItem();
        private PurchaseOrder request = new PurchaseOrder();
        private List<PurchaseOrderItem> _items;
        private Employee emp;
        private Department department;
        private int itemId;
        private int updateFlag = 1;
        private User user;
        

        public RequestDetails(PurchaseOrder order, User user)
        {
            request = order;
            this.user = user;
            InitializeComponent();
        }

        public RequestDetails(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RequestDetails_Load(object sender, EventArgs e)
        {
            try
            {
                if (request.PONumber != 0)
                {
                    request = requestService.GetPurchaseOrder(request.PONumber);
                    this.Text = $"Purchase Order #{request.PONumber} - {request.Status}";
                    lblPONumber.Visible = false;
                }
                else
                {
                    this.Text = "New Purchase Order";
                    lblPONumber.Visible = false;
                }
                emp = employeeService.Get(user.EmployeeID);
                txtEmployee.Text = emp.FirstName + " " + emp.LastName;

                department = departmentService.Get(emp.DepartmentID);
                txtDepartment.Text = department.Name;

                if (emp.SupervisorID == null)
                {
                    txtSupervisor.Text = "CEO";
                }
                else
                {
                    emp = employeeService.GetSupervisorById(emp.SupervisorID);
                    txtSupervisor.Text = emp.FirstName + " " + emp.LastName;
                }

                if (request.PONumber != 0)
                {
                    request = requestService.GetPurchaseOrder(request.PONumber);
                    txtOrderSubtotal.Text = request.Subtotal.ToString("c");
                    txtOrderTax.Text = request.SalesTax.ToString("c");
                    txtOrderTotal.Text = request.Total.ToString("c");
                }

                txtQuantity.Text = "0";
                txtPrice.Text = "0.00";
                txtSubtotal.Text = "0.00";

                if (request.PONumber > 0)
                {
                    List<PurchaseOrderItem> items = requestService.GetPurchaseOrderItemByPOId(request.PONumber);
                    dgvRequests.AutoGenerateColumns = true;
                    dgvRequests.DataSource = items;
                    dgvRequests.Columns[4].DefaultCellStyle.Format = "c";
                    dgvRequests.Columns[8].DefaultCellStyle.Format = "c";
                    CreateRemoveButton();
                    CreateEditButton();
                    HideColumns();
                }

                txtDate.Text = DateTime.Now.Date.ToShortDateString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int num;
                decimal dec;
                item = new PurchaseOrderItem();

                //FillItemDetails(item);

                if(request.Status == RequestStatus.Closed)
                {
                    MessageBox.Show("Item was not added. Order is closed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Decimal.TryParse(txtPrice.Text, out dec)& !Int32.TryParse(txtQuantity.Text, out num))
                {
                    MessageBox.Show("Quantity and Price must be numeric values.");
                }
                else if (!Decimal.TryParse(txtPrice.Text, out dec))
                {
                    MessageBox.Show("Price must be a numberic value");
                }
                else if (!Int32.TryParse(txtQuantity.Text, out num))
                {
                    MessageBox.Show("Quantity must be a numeric value.");
                }
                else
                {
                    FillItemDetails(item);
                    RequestStatus status = request.Status;

                    if (dgvRequests.Rows.Count == 0)
                    {
                        FillItemDetails(item);
                        int id = requestService.InsertRequestAndItem(request, item);
                        if (id != 0)
                        {
                            MessageBox.Show($"Successfully created new purchase order. PONumber: {id}");
                            this.Text = $"Purchase Order #{id} - {status}";
                            request = requestService.GetPurchaseOrder(id);
                            UpdateOrderInfo();
                            _items = requestService.GetPurchaseOrderItemByPOId(id);
                            dgvRequests.DataSource = _items;
                            dgvRequests.Columns[4].DefaultCellStyle.Format = "c";
                            dgvRequests.Columns[8].DefaultCellStyle.Format = "c";
                            FillItemDetails(item);
                            HideColumns();
                            CreateEditButton();
                            CreateRemoveButton();
                            ResetForm();
                            btnSaveOrder.Enabled = true;
                            return;
                        }
                        else
                        {
                            ShowMessage(item);
                        }
                    }
                    else
                    {
                        int returnVal = requestService.InsertItem(item);

                        if (returnVal > 0)
                        {
                            MessageBox.Show($"Item has been added to order. ID: {item.Id}");
                            _items = requestService.GetPurchaseOrderItemByPOId(request.PONumber);
                            dgvRequests.DataSource = _items;
                            UpdateOrderInfo();
                            ResetForm();
                            btnSaveOrder.Enabled = true;
                            dgvRequests.Columns[4].DefaultCellStyle.Format = "c";
                            dgvRequests.Columns[8].DefaultCellStyle.Format = "c";
                            CreateRemoveButton();
                            CreateEditButton();
                            HideColumns();
                            return;
                        }
                        
                        if(returnVal == -1)
                        {
                            ShowMessage(item);
                        }

                        if (returnVal == 0)
                        {
                            item = requestService.Update(item);
                            MessageBox.Show($"Duplicate item. Item has been merged with ID: {item.Id}");
                            _items = requestService.GetPurchaseOrderItemByPOId(item.PONumber);
                            dgvRequests.DataSource = _items;
                            UpdateOrderInfo();
                            ResetForm();
                            btnSaveOrder.Enabled = true;
                            HideColumns();
                        }
                    }
                } //If no try parse remove this bracket
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while creating the new purchase order. Please Try Again.", "Error");
            }
        }

        private void HideColumns()
        {
            dgvRequests.Columns["TimeStamp"].Visible = false;
            dgvRequests.Columns["PONumber"].Visible = false;
        }

        private void dgvRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Test");
        }

        private PurchaseOrderItem FillItemDetails(PurchaseOrderItem item)
        {
            item.Name = txtName.Text;
            item.Description = txtDescription.Text;
            item.Quantity = Convert.ToInt32(txtQuantity.Text);
            item.Price = Convert.ToDecimal(txtPrice.Text);
            item.Justification = txtJustification.Text;
            item.Location = txtLocation.Text;
            item.Subtotal = Convert.ToDecimal(txtSubtotal.Text);
            item.PONumber = request.PONumber;
            item.Status = ItemStatus.Pending;

            return item;
        }

        //private PurchaseOrder FillRequestDetails(PurchaseOrder r)
        //{
        //    r.Status = RequestStatus.Pending;
        //    r.PONumber = request.PONumber;
        //    r.EmployeeId = user.EmployeeID;
        //    r.DepartmentId = 2;
        //    r.SupervisorId = "00000001";
        //    r.Subtotal = 0m;
        //    r.SalesTax = 0m;
        //    r.Total = 0m;

        //    return r;
        //}

        private void dgvRequests_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var col = dgvRequests.Columns[e.ColumnIndex];
                item = requestService.Get(Convert.ToInt32(dgvRequests.Rows[e.RowIndex].Cells[0].Value));

                if (col.HeaderText == "Edit Item")
                {


                    if (item.Status == ItemStatus.Pending)
                    {
                        FillItemToEdit(item);
                        btnSave.Enabled = true;
                        btnAdd.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Cannot edit items that have been processed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (item.Status == ItemStatus.Pending)
                    {
                        if (MessageBox.Show("Are you sure you want to remove this item?", "Warning",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        int id = Convert.ToInt32(dgvRequests.Rows[e.RowIndex].Cells[0].Value);

                        item = requestService.RemoveItem(Convert.ToInt32(dgvRequests.Rows[e.RowIndex].Cells[0].Value));

                        MessageBox.Show($"Item Successfully removed. ID: {item.Id}");
                        _items = requestService.GetPurchaseOrderItemByPOId(request.PONumber);
                        request = requestService.GetPurchaseOrder(item.PONumber);
                        dgvRequests.DataSource = _items;
                        txtOrderSubtotal.Text = request.Subtotal.ToString("c");
                        txtOrderTax.Text = request.Subtotal.ToString("c");
                        txtOrderTotal.Text = request.Total.ToString("c");

                        if(request.Status == RequestStatus.Closed)
                        {
                            MessageBox.Show("Purchase Order has been closed");
                            this.Text = $"Purchase Order #{request.PONumber} - {request.Status}";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot remove items that have been processed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtJustification.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtSubtotal.Text = string.Empty;
        }

        private void GetItems()
        {
            _items = requestService.GetPurchaseOrderItemByPOId(request.PONumber);
            dgvRequests.DataSource = _items;
        }

        private void CreateRemoveButton()
        {
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Remove Item";
            btnColumn.Text = "Remove";
            dgvRequests.Columns.Add(btnColumn);
        }

        private void CreateEditButton()
        {
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Edit Item";
            btnColumn.Text = "Edit";
            dgvRequests.Columns.Add(btnColumn);
        }

        private void FillItemToEdit(PurchaseOrderItem item)
        {
            txtName.Text = item.Name;
            txtDescription.Text = item.Description;
            txtQuantity.Text = item.Quantity.ToString();
            txtPrice.Text = item.Price.ToString();
            txtJustification.Text = item.Justification;
            txtLocation.Text = item.Location;
            txtSubtotal.Text = item.Subtotal.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(request.Status != RequestStatus.Closed)
                {
                    FillItemDetails(item);
                    item = requestService.Update(item);

                    if (item.Errors.Count == 1)
                    {
                        ShowMessage(item);
                    }
                    else
                    {
                        MessageBox.Show($"Item has been updated successfully. ID:{item.Id}");
                        _items = requestService.GetPurchaseOrderItemByPOId(item.PONumber);
                        dgvRequests.DataSource = _items;
                        UpdateOrderInfo();
                        ResetForm();
                        btnSaveOrder.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Cannot add items to a processed purchase order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowMessage(PurchaseOrderItem request)
        {
            string msg = "";
            foreach (Error error in request.Errors)
            {
                msg += error.Message + Environment.NewLine;
            }

            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            try
            {
                request = requestService.Update(request);
                if (request != null)
                {
                    MessageBox.Show($"Purchase Order was successfully updated. PO# {request.PONumber}");
                    UpdateOrderInfo();
                    btnSaveOrder.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPrice.Text != string.Empty && txtQuantity.Text != string.Empty && Int32.TryParse(txtQuantity.Text, out int qtyResult) && Decimal.TryParse(txtPrice.Text, out decimal priceResult))
                {
                    decimal subtotal = Convert.ToDecimal(txtPrice.Text) * Convert.ToInt32(txtQuantity.Text);
                    txtSubtotal.Text = subtotal.ToString();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPrice.Text != string.Empty && txtQuantity.Text != string.Empty && Int32.TryParse(txtQuantity.Text, out int qtyResult) && Decimal.TryParse(txtPrice.Text, out decimal priceResult))
                {
                    decimal subtotal = Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(txtQuantity.Text);
                    txtSubtotal.Text = subtotal.ToString();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateOrderInfo()
        {
            requestService.Update(request);
            txtOrderSubtotal.Text = request.Subtotal.ToString("c");
            txtOrderTax.Text = request.SalesTax.ToString("c");
            txtOrderTotal.Text = request.Total.ToString("c");
        }
    }
}
