namespace CapstoneProject
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabControl = new MdiTabControl.TabControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslUserRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSearchEmployees = new System.Windows.Forms.ToolStripButton();
            this.tsbAddEmployee = new System.Windows.Forms.ToolStripButton();
            this.tsbCreateDepartment = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiEmployees = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSearchEmployees = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDepartments = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddDepartment = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUpdateDepartment = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteDepartment = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProcessPO = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbProcessPO = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Location = new System.Drawing.Point(0, 58);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 55, 3, 3);
            this.tabControl.MenuRenderer = null;
            this.tabControl.Name = "tabControl";
            this.tabControl.Size = new System.Drawing.Size(1146, 582);
            this.tabControl.TabCloseButtonImage = null;
            this.tabControl.TabCloseButtonImageDisabled = null;
            this.tabControl.TabCloseButtonImageHot = null;
            this.tabControl.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslUserName,
            this.tslUserRole});
            this.statusStrip1.Location = new System.Drawing.Point(0, 637);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1145, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslUserName
            // 
            this.tslUserName.Name = "tslUserName";
            this.tslUserName.Size = new System.Drawing.Size(151, 20);
            this.tslUserName.Text = "toolStripStatusLabel1";
            // 
            // tslUserRole
            // 
            this.tslUserRole.Name = "tslUserRole";
            this.tslUserRole.Size = new System.Drawing.Size(151, 20);
            this.tslUserRole.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSearchEmployees,
            this.tsbAddEmployee,
            this.tsbCreateDepartment,
            this.toolStripButton1,
            this.tsbProcessPO});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1145, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSearchEmployees
            // 
            this.tsbSearchEmployees.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSearchEmployees.Image = ((System.Drawing.Image)(resources.GetObject("tsbSearchEmployees.Image")));
            this.tsbSearchEmployees.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSearchEmployees.Name = "tsbSearchEmployees";
            this.tsbSearchEmployees.Size = new System.Drawing.Size(24, 24);
            this.tsbSearchEmployees.Text = "Search Employees";
            this.tsbSearchEmployees.Click += new System.EventHandler(this.tsbSearchEmployees_Click);
            // 
            // tsbAddEmployee
            // 
            this.tsbAddEmployee.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddEmployee.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddEmployee.Image")));
            this.tsbAddEmployee.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddEmployee.Name = "tsbAddEmployee";
            this.tsbAddEmployee.Size = new System.Drawing.Size(24, 24);
            this.tsbAddEmployee.Text = "Add Employee Information";
            this.tsbAddEmployee.Visible = false;
            this.tsbAddEmployee.Click += new System.EventHandler(this.tsbAddEmployee_Click);
            // 
            // tsbCreateDepartment
            // 
            this.tsbCreateDepartment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCreateDepartment.Image = ((System.Drawing.Image)(resources.GetObject("tsbCreateDepartment.Image")));
            this.tsbCreateDepartment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreateDepartment.Name = "tsbCreateDepartment";
            this.tsbCreateDepartment.Size = new System.Drawing.Size(24, 24);
            this.tsbCreateDepartment.Text = "Create Department";
            this.tsbCreateDepartment.Visible = false;
            this.tsbCreateDepartment.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton1.Text = "Purchase Orders";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEmployees,
            this.tsmiDepartments,
            this.purchaseOrdersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1145, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiEmployees
            // 
            this.tsmiEmployees.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSearchEmployees,
            this.tsmiAddEmployee});
            this.tsmiEmployees.Name = "tsmiEmployees";
            this.tsmiEmployees.Size = new System.Drawing.Size(93, 24);
            this.tsmiEmployees.Text = "Employees";
            // 
            // tsmiSearchEmployees
            // 
            this.tsmiSearchEmployees.Name = "tsmiSearchEmployees";
            this.tsmiSearchEmployees.Size = new System.Drawing.Size(128, 26);
            this.tsmiSearchEmployees.Text = "Search";
            this.tsmiSearchEmployees.Click += new System.EventHandler(this.tsmiSearchEmployees_Click);
            // 
            // tsmiAddEmployee
            // 
            this.tsmiAddEmployee.Name = "tsmiAddEmployee";
            this.tsmiAddEmployee.Size = new System.Drawing.Size(128, 26);
            this.tsmiAddEmployee.Text = "Add";
            this.tsmiAddEmployee.Visible = false;
            this.tsmiAddEmployee.Click += new System.EventHandler(this.tsmiAddEmployee_Click);
            // 
            // tsmiDepartments
            // 
            this.tsmiDepartments.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddDepartment,
            this.tsmiUpdateDepartment,
            this.tsmiDeleteDepartment});
            this.tsmiDepartments.Name = "tsmiDepartments";
            this.tsmiDepartments.Size = new System.Drawing.Size(107, 24);
            this.tsmiDepartments.Text = "Departments";
            this.tsmiDepartments.Visible = false;
            // 
            // tsmiAddDepartment
            // 
            this.tsmiAddDepartment.Name = "tsmiAddDepartment";
            this.tsmiAddDepartment.Size = new System.Drawing.Size(133, 26);
            this.tsmiAddDepartment.Text = "Add";
            this.tsmiAddDepartment.Visible = false;
            this.tsmiAddDepartment.Click += new System.EventHandler(this.tsmiAddDepartment_Click);
            // 
            // tsmiUpdateDepartment
            // 
            this.tsmiUpdateDepartment.Name = "tsmiUpdateDepartment";
            this.tsmiUpdateDepartment.Size = new System.Drawing.Size(133, 26);
            this.tsmiUpdateDepartment.Text = "Update";
            this.tsmiUpdateDepartment.Visible = false;
            this.tsmiUpdateDepartment.Click += new System.EventHandler(this.tsmiUpdateDepartment_Click);
            // 
            // tsmiDeleteDepartment
            // 
            this.tsmiDeleteDepartment.Name = "tsmiDeleteDepartment";
            this.tsmiDeleteDepartment.Size = new System.Drawing.Size(133, 26);
            this.tsmiDeleteDepartment.Text = "Delete";
            this.tsmiDeleteDepartment.Visible = false;
            this.tsmiDeleteDepartment.Click += new System.EventHandler(this.tsmiDeleteDepartment_Click);
            // 
            // purchaseOrdersToolStripMenuItem
            // 
            this.purchaseOrdersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewAllToolStripMenuItem,
            this.tsmiProcessPO});
            this.purchaseOrdersToolStripMenuItem.Name = "purchaseOrdersToolStripMenuItem";
            this.purchaseOrdersToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.purchaseOrdersToolStripMenuItem.Text = "Purchase Orders";
            // 
            // viewAllToolStripMenuItem
            // 
            this.viewAllToolStripMenuItem.Name = "viewAllToolStripMenuItem";
            this.viewAllToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.viewAllToolStripMenuItem.Text = "View All";
            this.viewAllToolStripMenuItem.Click += new System.EventHandler(this.viewAllToolStripMenuItem_Click);
            // 
            // tsmiProcessPO
            // 
            this.tsmiProcessPO.Name = "tsmiProcessPO";
            this.tsmiProcessPO.Size = new System.Drawing.Size(243, 26);
            this.tsmiProcessPO.Text = "Process Purchase Orders";
            this.tsmiProcessPO.Visible = false;
            this.tsmiProcessPO.Click += new System.EventHandler(this.tsmiProcessPO_Click);
            // 
            // tsbProcessPO
            // 
            this.tsbProcessPO.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProcessPO.Image = ((System.Drawing.Image)(resources.GetObject("tsbProcessPO.Image")));
            this.tsbProcessPO.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProcessPO.Name = "tsbProcessPO";
            this.tsbProcessPO.Size = new System.Drawing.Size(24, 24);
            this.tsbProcessPO.Text = "toolStripButton2";
            this.tsbProcessPO.Visible = false;
            this.tsbProcessPO.Click += new System.EventHandler(this.tsbProcessPO_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 662);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cryptech";
            this.Load += new System.EventHandler(this.Main_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MdiTabControl.TabControl tabControl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSearchEmployees;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiEmployees;
        private System.Windows.Forms.ToolStripMenuItem tsmiSearchEmployees;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddEmployee;
        private System.Windows.Forms.ToolStripMenuItem tsmiDepartments;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddDepartment;
        private System.Windows.Forms.ToolStripButton tsbAddEmployee;
        private System.Windows.Forms.ToolStripButton tsbCreateDepartment;
        private System.Windows.Forms.ToolStripMenuItem purchaseOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdateDepartment;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteDepartment;
        private System.Windows.Forms.ToolStripStatusLabel tslUserName;
        private System.Windows.Forms.ToolStripStatusLabel tslUserRole;
        private System.Windows.Forms.ToolStripMenuItem tsmiProcessPO;
        private System.Windows.Forms.ToolStripButton tsbProcessPO;
    }
}