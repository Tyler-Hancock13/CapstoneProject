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
    public partial class ProcessPurchaseOrders : Form
    {
        EmployeeService employeeService = new EmployeeService();
        RequestService requestService = new RequestService();

        private List<PurchaseOrderItem> items;
        private PurchaseOrder purchaseOrder;
        private PurchaseOrderItem item;
        private Employee employee;
        private User currentUser = new User();
        int id;

        public ProcessPurchaseOrders()
        {
            InitializeComponent();
        }

        public ProcessPurchaseOrders(User user, Main main)
        {
            currentUser = user;
            InitializeComponent();
        }

        private void ProcessPurchaseOrders_Load(object sender, EventArgs e)
        {
            try
            {
                employee = employeeService.Get(currentUser.EmployeeID);

                dgvPurchaseOrders.DataSource = requestService.GetByDepartment(employee.DepartmentID);
                SetupPOGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPurchaseOrders_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                items = new List<PurchaseOrderItem>();
                item = new PurchaseOrderItem();
                employee = new Employee();
                purchaseOrder = new PurchaseOrder();

                int poId = Convert.ToInt32(dgvPurchaseOrders.Rows[e.RowIndex].Cells[1].Value);
                items = requestService.GetPurchaseOrderItemByPOId(poId);
                purchaseOrder = requestService.GetPurchaseOrder(poId);

                if (purchaseOrder.EmployeeId == currentUser.EmployeeID)
                {
                    MessageBox.Show("A supervisor cannot process their own purchase orders.");
                    return;
                }

                int itemId = Convert.ToInt32(dgvPurchaseOrders.Rows[e.RowIndex].Cells[1].Value);
                item = requestService.Get(itemId);
                employee = employeeService.Get(purchaseOrder.EmployeeId);
                dgvDetails.DataSource = items;
                SetupItemGridView();
                dgvDetails.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupPOGridView()
        {
            dgvPurchaseOrders.Columns[2].Visible = false;
            dgvPurchaseOrders.Columns[4].Visible = false;
            dgvPurchaseOrders.Columns[5].Visible = false;

            dgvPurchaseOrders.Columns[7].DefaultCellStyle.Format = "c";
            dgvPurchaseOrders.Columns[8].DefaultCellStyle.Format = "c";
            dgvPurchaseOrders.Columns[9].DefaultCellStyle.Format = "c";

            dgvPurchaseOrders.Columns[3].HeaderText = "Employee";
            dgvPurchaseOrders.Columns[6].HeaderText = "Department";
            dgvPurchaseOrders.Columns[10].HeaderText = "Date Created";

            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "View Order";
            btnColumn.Text = "View";
            dgvPurchaseOrders.Columns.Add(btnColumn);
        }

        private void SetupItemGridView()
        {
            lblEmployeeName.Text = $"{employee.FirstName} {employee.LastName}";
            lblCurrentDate.Text = DateTime.Now.Date.ToShortDateString();
            lblOrderSubtotal.Text = purchaseOrder.Subtotal.ToString("c");
            lblOrderTax.Text = purchaseOrder.SalesTax.ToString("c");
            lblOrderTotal.Text = purchaseOrder.Total.ToString("c");
            

            lblDetails.Text = $"PO #{item.PONumber.ToString()} Details";
            

            dgvDetails.Columns[11].Visible = false;
            dgvDetails.Columns[9].Visible = false;
            dgvDetails.Columns[4].DefaultCellStyle.Format = "c";
            dgvDetails.Columns[8].DefaultCellStyle.Format = "c";

            if(dgvDetails.Columns["Select Item"] == null)
            {
                DataGridViewButtonColumn btnStatus = new DataGridViewButtonColumn();
                btnStatus.HeaderText = "Select Item";
                btnStatus.Text = "Select";
                dgvDetails.Columns.Add(btnStatus);
            }

            lblCurrentDate.Visible = true;
            lblDate.Visible = true;

            lblEmp.Visible = true;
            lblEmployeeName.Visible = true;

            lblOrderSubtotal.Visible = true;
            lblSubtotalLabel.Visible = true;

            lblOrderTax.Visible = true;
            lblTaxLabel.Visible = true;

            lblOrderTotal.Visible = true;
            lblTotalLabel.Visible = true;

            btnSaveOrder.Visible = true;

            lblDetails.Visible = true;
        }

        private void dgvDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cboStatus = e.Control as ComboBox;
            if(e.Control is DataGridViewComboBoxEditingControl)
            {
                cboStatus.SelectedIndexChanged -= new EventHandler(cboStatus_SelectedIndexChanged);

                cboStatus.SelectedIndexChanged += new EventHandler(cboStatus_SelectedIndexChanged);
            }
        }

        void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Selected index changed");
        }

        private void dgvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                item = new PurchaseOrderItem();
                id = (Convert.ToInt32(dgvDetails.Rows[e.RowIndex].Cells[1].Value));
                item = requestService.Get(id);
                PopulateItemDetails();
                grpItem.Visible = true;
                txtReason.Visible = false;
                lblReason.Visible = false;
                grpItem.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PurchaseOrderItem PrepareItemToProcess(PurchaseOrderItem item) {
            
            item.Location = txtLocation.Text;
            item.Quantity = Convert.ToInt32(txtQuantity.Text);
            item.Price = Convert.ToDecimal(txtPrice.Text);
            item.Subtotal = Convert.ToDecimal(txtSubtotal.Text);
            item.Reason = txtReason.Text;
            item.Status = (ItemStatus)cboStatus.SelectedValue;

            return item;
        }

        private void PopulateItemDetails()
        {
            txtName.Text = item.Name;
            txtDescription.Text = item.Description;
            txtJustification.Text = item.Justification;
            txtLocation.Text = item.Location;
            txtQuantity.Text = item.Quantity.ToString();
            txtPrice.Text = item.Price.ToString();
            txtSubtotal.Text = item.Subtotal.ToString();
            cboStatus.DataSource = Enum.GetValues(typeof(ItemStatus));
            cboStatus.SelectedItem = item.Status;
        }

        private void cboStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cboStatus.SelectedValue) == 3)
                {
                    txtReason.Visible = true;
                    lblReason.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLocation.Text != item.Location || txtQuantity.Text != item.Quantity.ToString() || txtPrice.Text != item.Price.ToString() || Convert.ToInt32(cboStatus.SelectedValue) == 3)
                {
                    if (txtReason.Text == "")
                    {
                        MessageBox.Show("A reason is required for denied or modified items.");
                        return;
                    }
                }

                item = requestService.Get(id);
                PrepareItemToProcess(item);

                if (Convert.ToInt32(cboStatus.SelectedValue) == 2)
                {
                    item.Status = (ItemStatus)cboStatus.SelectedValue;
                    PurchaseOrderItem approvedItem = new PurchaseOrderItem();
                    approvedItem = requestService.ApproveItem(item);

                    if (approvedItem != null)
                    {
                        MessageBox.Show("Item was successfully proccessed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    dgvPurchaseOrders.DataSource = requestService.GetByDepartment(employee.DepartmentID);
                    dgvDetails.DataSource = requestService.GetPurchaseOrderItemByPOId(item.PONumber);
                }

                if (Convert.ToInt32(cboStatus.SelectedValue) == 3)
                {
                    requestService.DenyItem(item);
                    dgvPurchaseOrders.DataSource = requestService.GetByDepartment(employee.DepartmentID);
                    dgvDetails.DataSource = requestService.GetPurchaseOrderItemByPOId(item.PONumber);
                }

                if (Convert.ToInt32(cboStatus.SelectedValue) == 1)
                {
                    requestService.Update(item);
                    dgvPurchaseOrders.DataSource = requestService.GetByDepartment(employee.DepartmentID);
                    dgvDetails.DataSource = requestService.GetPurchaseOrderItemByPOId(item.PONumber);
                }

                grpItem.Visible = false;
                txtReason.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtReason.Visible = true;
                lblReason.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtReason.Visible = true;
                lblReason.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtReason.Visible = true;
                lblReason.Visible = true;
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

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Would you like to save this purchase order?", "Save Order", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (DialogResult == DialogResult.OK)
                {
                    PurchaseOrder order = requestService.Update(purchaseOrder);

                    if (order != null)
                    {
                        MessageBox.Show("Purchase Order was saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        dgvDetails.Visible = false;
                        grpItem.Visible = false;

                    }
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
    }
}
