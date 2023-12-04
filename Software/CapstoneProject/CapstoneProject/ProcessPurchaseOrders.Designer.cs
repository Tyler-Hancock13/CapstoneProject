namespace CapstoneProject
{
    partial class ProcessPurchaseOrders
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessPurchaseOrders));
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvPurchaseOrders = new System.Windows.Forms.DataGridView();
            this.lblDetails = new System.Windows.Forms.Label();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.lblReason = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblJustification = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnSaveItem = new System.Windows.Forms.Button();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtJustification = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.lblEmployeeName = new System.Windows.Forms.Label();
            this.lblEmp = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblCurrentDate = new System.Windows.Forms.Label();
            this.lblOrderSubtotal = new System.Windows.Forms.Label();
            this.lblOrderTax = new System.Windows.Forms.Label();
            this.lblOrderTotal = new System.Windows.Forms.Label();
            this.lblSubtotalLabel = new System.Windows.Forms.Label();
            this.lblTaxLabel = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.grpItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(350, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(395, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Process Purchase Orders";
            // 
            // dgvPurchaseOrders
            // 
            this.dgvPurchaseOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseOrders.Location = new System.Drawing.Point(299, 60);
            this.dgvPurchaseOrders.Name = "dgvPurchaseOrders";
            this.dgvPurchaseOrders.RowTemplate.Height = 24;
            this.dgvPurchaseOrders.Size = new System.Drawing.Size(776, 154);
            this.dgvPurchaseOrders.TabIndex = 1;
            this.dgvPurchaseOrders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseOrders_CellContentClick_1);
            // 
            // lblDetails
            // 
            this.lblDetails.AutoSize = true;
            this.lblDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetails.Location = new System.Drawing.Point(279, 233);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(151, 32);
            this.lblDetails.TabIndex = 2;
            this.lblDetails.Text = "PO Details";
            this.lblDetails.Visible = false;
            // 
            // dgvDetails
            // 
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Location = new System.Drawing.Point(12, 362);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.RowTemplate.Height = 24;
            this.dgvDetails.Size = new System.Drawing.Size(676, 170);
            this.dgvDetails.TabIndex = 3;
            this.dgvDetails.Visible = false;
            this.dgvDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetails_CellContentClick);
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.lblReason);
            this.grpItem.Controls.Add(this.lblStatus);
            this.grpItem.Controls.Add(this.lblSubtotal);
            this.grpItem.Controls.Add(this.lblPrice);
            this.grpItem.Controls.Add(this.lblQuantity);
            this.grpItem.Controls.Add(this.lblLocation);
            this.grpItem.Controls.Add(this.lblJustification);
            this.grpItem.Controls.Add(this.lblDescription);
            this.grpItem.Controls.Add(this.lblName);
            this.grpItem.Controls.Add(this.btnSaveItem);
            this.grpItem.Controls.Add(this.txtReason);
            this.grpItem.Controls.Add(this.cboStatus);
            this.grpItem.Controls.Add(this.txtSubtotal);
            this.grpItem.Controls.Add(this.txtPrice);
            this.grpItem.Controls.Add(this.txtQuantity);
            this.grpItem.Controls.Add(this.txtLocation);
            this.grpItem.Controls.Add(this.txtJustification);
            this.grpItem.Controls.Add(this.txtDescription);
            this.grpItem.Controls.Add(this.txtName);
            this.grpItem.Location = new System.Drawing.Point(694, 301);
            this.grpItem.Name = "grpItem";
            this.grpItem.Size = new System.Drawing.Size(397, 231);
            this.grpItem.TabIndex = 4;
            this.grpItem.TabStop = false;
            this.grpItem.Text = "Item";
            this.grpItem.Visible = false;
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(205, 142);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(61, 17);
            this.lblReason.TabIndex = 18;
            this.lblReason.Text = "Reason:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(212, 114);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 17);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "Status:";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(202, 83);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(64, 17);
            this.lblSubtotal.TabIndex = 16;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(220, 55);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(44, 17);
            this.lblPrice.TabIndex = 15;
            this.lblPrice.Text = "Price:";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(201, 27);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(65, 17);
            this.lblQuantity.TabIndex = 14;
            this.lblQuantity.Text = "Quantity:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(23, 114);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(66, 17);
            this.lblLocation.TabIndex = 13;
            this.lblLocation.Text = "Location:";
            // 
            // lblJustification
            // 
            this.lblJustification.AutoSize = true;
            this.lblJustification.Location = new System.Drawing.Point(3, 83);
            this.lblJustification.Name = "lblJustification";
            this.lblJustification.Size = new System.Drawing.Size(86, 17);
            this.lblJustification.TabIndex = 12;
            this.lblJustification.Text = "Justification:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 55);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(83, 17);
            this.lblDescription.TabIndex = 11;
            this.lblDescription.Text = "Description:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(40, 27);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 17);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Name:";
            // 
            // btnSaveItem
            // 
            this.btnSaveItem.Location = new System.Drawing.Point(95, 139);
            this.btnSaveItem.Name = "btnSaveItem";
            this.btnSaveItem.Size = new System.Drawing.Size(100, 55);
            this.btnSaveItem.TabIndex = 9;
            this.btnSaveItem.Text = "Save Item";
            this.btnSaveItem.UseVisualStyleBackColor = true;
            this.btnSaveItem.Click += new System.EventHandler(this.btnSaveItem_Click);
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(270, 139);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(121, 55);
            this.txtReason.TabIndex = 8;
            this.txtReason.Visible = false;
            // 
            // cboStatus
            // 
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(270, 111);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(121, 24);
            this.cboStatus.TabIndex = 7;
            this.cboStatus.SelectedValueChanged += new System.EventHandler(this.cboStatus_SelectedValueChanged);
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Enabled = false;
            this.txtSubtotal.Location = new System.Drawing.Point(270, 80);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.Size = new System.Drawing.Size(121, 22);
            this.txtSubtotal.TabIndex = 6;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(270, 52);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(121, 22);
            this.txtPrice.TabIndex = 5;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(270, 24);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(121, 22);
            this.txtQuantity.TabIndex = 4;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            this.txtQuantity.Leave += new System.EventHandler(this.txtQuantity_Leave);
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(95, 111);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(100, 22);
            this.txtLocation.TabIndex = 3;
            this.txtLocation.TextChanged += new System.EventHandler(this.txtLocation_TextChanged);
            // 
            // txtJustification
            // 
            this.txtJustification.Enabled = false;
            this.txtJustification.Location = new System.Drawing.Point(95, 80);
            this.txtJustification.Name = "txtJustification";
            this.txtJustification.Size = new System.Drawing.Size(100, 22);
            this.txtJustification.TabIndex = 2;
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(95, 52);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(100, 22);
            this.txtDescription.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(95, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 22);
            this.txtName.TabIndex = 0;
            // 
            // grpSearch
            // 
            this.grpSearch.Location = new System.Drawing.Point(12, 60);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(264, 154);
            this.grpSearch.TabIndex = 5;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search PO\'s";
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEmployeeName.Location = new System.Drawing.Point(103, 281);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(88, 20);
            this.lblEmployeeName.TabIndex = 6;
            this.lblEmployeeName.Text = "Employee";
            this.lblEmployeeName.Visible = false;
            // 
            // lblEmp
            // 
            this.lblEmp.AutoSize = true;
            this.lblEmp.Location = new System.Drawing.Point(23, 279);
            this.lblEmp.Name = "lblEmp";
            this.lblEmp.Size = new System.Drawing.Size(74, 17);
            this.lblEmp.TabIndex = 7;
            this.lblEmp.Text = "Employee:";
            this.lblEmp.Visible = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(55, 307);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(42, 17);
            this.lblDate.TabIndex = 8;
            this.lblDate.Text = "Date:";
            this.lblDate.Visible = false;
            // 
            // lblCurrentDate
            // 
            this.lblCurrentDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCurrentDate.Location = new System.Drawing.Point(103, 307);
            this.lblCurrentDate.Name = "lblCurrentDate";
            this.lblCurrentDate.Size = new System.Drawing.Size(88, 20);
            this.lblCurrentDate.TabIndex = 9;
            this.lblCurrentDate.Text = "Employee";
            this.lblCurrentDate.Visible = false;
            // 
            // lblOrderSubtotal
            // 
            this.lblOrderSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOrderSubtotal.Location = new System.Drawing.Point(281, 278);
            this.lblOrderSubtotal.Name = "lblOrderSubtotal";
            this.lblOrderSubtotal.Size = new System.Drawing.Size(88, 20);
            this.lblOrderSubtotal.TabIndex = 10;
            this.lblOrderSubtotal.Text = "Employee";
            this.lblOrderSubtotal.Visible = false;
            // 
            // lblOrderTax
            // 
            this.lblOrderTax.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOrderTax.Location = new System.Drawing.Point(281, 305);
            this.lblOrderTax.Name = "lblOrderTax";
            this.lblOrderTax.Size = new System.Drawing.Size(88, 20);
            this.lblOrderTax.TabIndex = 11;
            this.lblOrderTax.Text = "Employee";
            this.lblOrderTax.Visible = false;
            // 
            // lblOrderTotal
            // 
            this.lblOrderTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOrderTotal.Location = new System.Drawing.Point(281, 330);
            this.lblOrderTotal.Name = "lblOrderTotal";
            this.lblOrderTotal.Size = new System.Drawing.Size(88, 20);
            this.lblOrderTotal.TabIndex = 12;
            this.lblOrderTotal.Text = "Employee";
            this.lblOrderTotal.Visible = false;
            // 
            // lblSubtotalLabel
            // 
            this.lblSubtotalLabel.AutoSize = true;
            this.lblSubtotalLabel.Location = new System.Drawing.Point(211, 281);
            this.lblSubtotalLabel.Name = "lblSubtotalLabel";
            this.lblSubtotalLabel.Size = new System.Drawing.Size(64, 17);
            this.lblSubtotalLabel.TabIndex = 13;
            this.lblSubtotalLabel.Text = "Subtotal:";
            this.lblSubtotalLabel.Visible = false;
            // 
            // lblTaxLabel
            // 
            this.lblTaxLabel.AutoSize = true;
            this.lblTaxLabel.Location = new System.Drawing.Point(240, 306);
            this.lblTaxLabel.Name = "lblTaxLabel";
            this.lblTaxLabel.Size = new System.Drawing.Size(35, 17);
            this.lblTaxLabel.TabIndex = 14;
            this.lblTaxLabel.Text = "Tax:";
            this.lblTaxLabel.Visible = false;
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Location = new System.Drawing.Point(231, 330);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(44, 17);
            this.lblTotalLabel.TabIndex = 15;
            this.lblTotalLabel.Text = "Total:";
            this.lblTotalLabel.Visible = false;
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.Location = new System.Drawing.Point(564, 307);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(124, 49);
            this.btnSaveOrder.TabIndex = 16;
            this.btnSaveOrder.Text = "Save Order";
            this.btnSaveOrder.UseVisualStyleBackColor = true;
            this.btnSaveOrder.Visible = false;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // ProcessPurchaseOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 544);
            this.Controls.Add(this.btnSaveOrder);
            this.Controls.Add(this.lblTotalLabel);
            this.Controls.Add(this.lblTaxLabel);
            this.Controls.Add(this.lblSubtotalLabel);
            this.Controls.Add(this.lblOrderTotal);
            this.Controls.Add(this.lblOrderTax);
            this.Controls.Add(this.lblOrderSubtotal);
            this.Controls.Add(this.lblCurrentDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblEmp);
            this.Controls.Add(this.lblEmployeeName);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.grpItem);
            this.Controls.Add(this.dgvDetails);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.dgvPurchaseOrders);
            this.Controls.Add(this.lblTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessPurchaseOrders";
            this.Text = "Process PO\'s";
            this.Load += new System.EventHandler(this.ProcessPurchaseOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvPurchaseOrders;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Label lblEmployeeName;
        private System.Windows.Forms.Label lblEmp;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtJustification;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblJustification;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnSaveItem;
        private System.Windows.Forms.Label lblCurrentDate;
        private System.Windows.Forms.Label lblOrderSubtotal;
        private System.Windows.Forms.Label lblOrderTax;
        private System.Windows.Forms.Label lblOrderTotal;
        private System.Windows.Forms.Label lblSubtotalLabel;
        private System.Windows.Forms.Label lblTaxLabel;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Button btnSaveOrder;
    }
}