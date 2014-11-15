namespace RoomManager
{
    partial class frmTsk_PendingRoom_Fee_Step2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTsk_PendingRoom_Fee_Step2));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnRemoveSeclectedRooms = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnPending = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lblIDBookingR = new DevExpress.XtraEditors.LabelControl();
            this.lblNameCompany = new DevExpress.XtraEditors.LabelControl();
            this.lblNameCustomerGroup = new DevExpress.XtraEditors.LabelControl();
            this.lblNameCustomer = new DevExpress.XtraEditors.LabelControl();
            this.lblSku = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCharge = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.dtpTo = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRemoveSeclectedRooms)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCharge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRemoveSeclectedRooms
            // 
            this.btnRemoveSeclectedRooms.AutoHeight = false;
            this.btnRemoveSeclectedRooms.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("btnRemoveSeclectedRooms.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.btnRemoveSeclectedRooms.Name = "btnRemoveSeclectedRooms";
            this.btnRemoveSeclectedRooms.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // btnPending
            // 
            this.btnPending.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPending.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnPending.Appearance.Options.UseFont = true;
            this.btnPending.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPending.Image = ((System.Drawing.Image)(resources.GetObject("btnPending.Image")));
            this.btnPending.Location = new System.Drawing.Point(192, 329);
            this.btnPending.Name = "btnPending";
            this.btnPending.Size = new System.Drawing.Size(100, 28);
            this.btnPending.TabIndex = 16;
            this.btnPending.Text = "Pending";
            this.btnPending.Click += new System.EventHandler(this.btnPending_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnPending, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 362);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.43096F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.27615F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.Controls.Add(this.labelControl1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.labelControl2, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.labelControl3, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.labelControl4, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.labelControl5, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.labelControl7, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this.labelControl8, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.lblIDBookingR, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblNameCompany, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblNameCustomerGroup, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.lblNameCustomer, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.lblSku, 2, 4);
            this.tableLayoutPanel4.Controls.Add(this.panel1, 2, 6);
            this.tableLayoutPanel4.Controls.Add(this.dtpTo, 2, 5);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 39);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 7;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(478, 283);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(50, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "IDBookingR";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Location = new System.Drawing.Point(50, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 16);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Công ty";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Location = new System.Drawing.Point(50, 92);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(35, 16);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Nhóm";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Location = new System.Drawing.Point(50, 131);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(99, 17);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Người đại diện";
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Location = new System.Drawing.Point(50, 172);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(67, 16);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Tên phòng";
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl7.Location = new System.Drawing.Point(50, 211);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(66, 17);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "Đến ngày";
            // 
            // labelControl8
            // 
            this.labelControl8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl8.Location = new System.Drawing.Point(50, 253);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(41, 17);
            this.labelControl8.TabIndex = 14;
            this.labelControl8.Text = "Lệ phí";
            // 
            // lblIDBookingR
            // 
            this.lblIDBookingR.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblIDBookingR.Location = new System.Drawing.Point(162, 13);
            this.lblIDBookingR.Name = "lblIDBookingR";
            this.lblIDBookingR.Size = new System.Drawing.Size(6, 13);
            this.lblIDBookingR.TabIndex = 3;
            this.lblIDBookingR.Text = "1";
            // 
            // lblNameCompany
            // 
            this.lblNameCompany.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNameCompany.Location = new System.Drawing.Point(162, 53);
            this.lblNameCompany.Name = "lblNameCompany";
            this.lblNameCompany.Size = new System.Drawing.Size(18, 13);
            this.lblNameCompany.TabIndex = 5;
            this.lblNameCompany.Text = "Abc";
            // 
            // lblNameCustomerGroup
            // 
            this.lblNameCustomerGroup.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNameCustomerGroup.Location = new System.Drawing.Point(162, 93);
            this.lblNameCustomerGroup.Name = "lblNameCustomerGroup";
            this.lblNameCustomerGroup.Size = new System.Drawing.Size(17, 13);
            this.lblNameCustomerGroup.TabIndex = 7;
            this.lblNameCustomerGroup.Text = "Xyz";
            // 
            // lblNameCustomer
            // 
            this.lblNameCustomer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNameCustomer.Location = new System.Drawing.Point(162, 133);
            this.lblNameCustomer.Name = "lblNameCustomer";
            this.lblNameCustomer.Size = new System.Drawing.Size(7, 13);
            this.lblNameCustomer.TabIndex = 9;
            this.lblNameCustomer.Text = "A";
            // 
            // lblSku
            // 
            this.lblSku.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSku.Location = new System.Drawing.Point(162, 173);
            this.lblSku.Name = "lblSku";
            this.lblSku.Size = new System.Drawing.Size(18, 13);
            this.lblSku.TabIndex = 11;
            this.lblSku.Text = "100";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCharge);
            this.panel1.Controls.Add(this.labelControl9);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(162, 243);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 37);
            this.panel1.TabIndex = 19;
            // 
            // txtCharge
            // 
            this.txtCharge.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCharge.Location = new System.Drawing.Point(3, 8);
            this.txtCharge.Name = "txtCharge";
            this.txtCharge.Size = new System.Drawing.Size(100, 20);
            this.txtCharge.TabIndex = 15;
            // 
            // labelControl9
            // 
            this.labelControl9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl9.Location = new System.Drawing.Point(109, 11);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(26, 16);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "(%)";
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpTo.EditValue = null;
            this.dtpTo.Location = new System.Drawing.Point(162, 210);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTo.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dtpTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTo.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpTo.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpTo.Properties.Mask.BeepOnError = true;
            this.dtpTo.Properties.Mask.EditMask = "([012]?[1-9]|[123]0|31)/(0?[1-9]|1[012])/([123][0-9])?[0-9][0-9]";
            this.dtpTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.dtpTo.Size = new System.Drawing.Size(209, 20);
            this.dtpTo.TabIndex = 13;
            // 
            // frmTsk_PendingRoom_Fee_Step2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTsk_PendingRoom_Fee_Step2";
            this.Text = "Pending có tính phí";
            this.Load += new System.EventHandler(this.frmTsk_PendingRoom_Fee_Step2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnRemoveSeclectedRooms)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCharge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnRemoveSeclectedRooms;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl lblIDBookingR;
        private DevExpress.XtraEditors.LabelControl lblNameCompany;
        private DevExpress.XtraEditors.LabelControl lblNameCustomerGroup;
        private DevExpress.XtraEditors.LabelControl lblNameCustomer;
        private DevExpress.XtraEditors.LabelControl lblSku;
        private DevExpress.XtraEditors.TextEdit txtCharge;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SimpleButton btnPending;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.DateEdit dtpTo;
    }
}