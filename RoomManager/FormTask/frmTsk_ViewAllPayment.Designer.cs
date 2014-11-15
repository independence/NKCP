namespace RoomManager
{
    partial class frmTsk_ViewAllPayment
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
            this.grdViewAllPayment = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewAllPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdViewAllPayment
            // 
            this.grdViewAllPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdViewAllPayment.Location = new System.Drawing.Point(0, 0);
            this.grdViewAllPayment.MainView = this.gridView1;
            this.grdViewAllPayment.Name = "grdViewAllPayment";
            this.grdViewAllPayment.Size = new System.Drawing.Size(1025, 469);
            this.grdViewAllPayment.TabIndex = 0;
            this.grdViewAllPayment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.grdViewAllPayment;
            this.gridView1.Name = "gridView1";
            // 
            // frmTsk_ViewAllPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 469);
            this.Controls.Add(this.grdViewAllPayment);
            this.Name = "frmTsk_ViewAllPayment";
            this.Text = "frmTsk_ViewAllPayment";
            this.Load += new System.EventHandler(this.frmTsk_ViewAllPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdViewAllPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdViewAllPayment;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}