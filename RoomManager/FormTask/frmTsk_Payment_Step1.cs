using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BussinessLogic;
using DataAccess;
using Entity;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using Library;
using CORESYSTEM;
using DevExpress.Utils;

namespace RoomManager
{
    public partial class frmTsk_Payment_Step1 : DevExpress.XtraEditors.XtraForm
    {
        public frmMain afrmMain = null;
        private int IDBookingR = 0;
        private int CustomerType;
        private DateTime CheckInPlan;
        private DateTime CheckOutPlan;

        //Hiennv
        public frmTsk_Payment_Step1(frmMain afrmMain, int CustomerType)
        {
            InitializeComponent();
            this.afrmMain = afrmMain;
            this.CustomerType = CustomerType;

        }
        //Hiennv
        public frmTsk_Payment_Step1(frmMain afrmMain, int IDBookingR, int CustomerType, DateTime CheckInPlan, DateTime CheckOutPlan)
        {
            InitializeComponent();
            this.afrmMain = afrmMain;
            this.IDBookingR = IDBookingR;
            this.CustomerType = CustomerType;
            this.CheckInPlan = CheckInPlan;
            this.CheckOutPlan = CheckOutPlan;

        }
        
        // NgocBM
        // Form thanh toán được load tất cả các phòng chưa thanh toán bất kể loại nào
        public frmTsk_Payment_Step1(frmMain afrmMain)
        {
            InitializeComponent();
            this.afrmMain = afrmMain;

        }
        //Hiennv
        public void LoadDataListUnPayBookingR()
        {
            try
            {
                string StatusPay;
                DateTime From = dtpFrom.DateTime;
                DateTime To = dtpTo.DateTime;
                if (Convert.ToInt32(lueStatusPay.EditValue) == 4)
                {
                    StatusPay = "1,2,3";
                }
                else
                {
                    StatusPay = lueStatusPay.EditValue.ToString();
                }
                
                int CustomerType = Convert.ToInt32(lueCustomerType.EditValue);

                if (StatusPay == "1" || StatusPay == "2")
                {
                    colCodeRoom.VisibleIndex = 0;
                    colCreatedDate.VisibleIndex = 1;
                    colCustomer_Name.VisibleIndex = 2;
                    colBookingMoney.VisibleIndex = 3;
                    colPaymentBookingRs.VisibleIndex = 4;
                    colPaymentBookingRs.Visible = true;
                    colSku.VisibleIndex = 5;
                    colBookingRoom_Status.VisibleIndex = 6;
                    colChekOut.VisibleIndex = 7;
                    colChekOut.Visible = true;
                    colPrintBookingRs.Visible = false;
                }
                else if (StatusPay == "3")
                {
                    colCodeRoom.VisibleIndex = 0;
                    colCreatedDate.VisibleIndex = 1;
                    colCustomer_Name.VisibleIndex = 2;
                    colBookingMoney.VisibleIndex = 3;
                    colPaymentBookingRs.Visible = false;
                    colChekOut.Visible = false;
                    colPrintBookingRs.VisibleIndex = 4;
                    colPrintBookingRs.Visible = true;
                    colSku.VisibleIndex = 6;
                    colBookingRoom_Status.VisibleIndex = 7;
                }
                
                if (this.IDBookingR == 0)
                {
                    if (From > To)
                    {
                        MessageBox.Show("Vui lòng nhập ngày bắt đầu kiểm tra nhỏ hơn ngày kết thúc .\n", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
                        dgvOwePay.DataSource = aReceptionTaskBO.GetListBookingRUnPayment(From, To, CustomerType, StatusPay);
                        dgvOwePay.RefreshDataSource();
                    }
                }
                else if (this.IDBookingR > 0)
                {
                    ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
                    dgvOwePay.DataSource = aReceptionTaskBO.GetListBookingRUnPayment(From, To, CustomerType, StatusPay).Where(r=>r.IDBookingR == this.IDBookingR).ToList();
                    dgvOwePay.RefreshDataSource();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_Payment_Step1.GetListUnPayBookingR \n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                this.LoadDataListUnPayBookingR();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_Payment_Step1.btnSearch_Click \n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckOut_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int IDBookingRoom = int.Parse(viewOwePay.GetFocusedRowCellValue("IDBookingRoom").ToString());
            string BookingStatusDisplay = viewOwePay.GetFocusedRowCellValue("BookingRoomStatusPayDisplay").ToString();
            if (BookingStatusDisplay == "Đã check in")
            {
                frmTsk_CheckOut afrmTsk_CheckOut = new frmTsk_CheckOut(IDBookingRoom, this);
                afrmTsk_CheckOut.ShowDialog();
            }
            else
            {
                MessageBox.Show("Phòng này đã được check out . \n", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //hiennv
        private void frmTsk_Check_StatusPay_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.IDBookingR == 0)
                {
                    dtpFrom.DateTime = DateTime.Now.AddDays(-30);
                    dtpTo.DateTime = DateTime.Now;
                }
                else
                {
                    dtpFrom.DateTime = this.CheckInPlan;
                    dtpTo.DateTime = this.CheckOutPlan;
                    lueCustomerType.Enabled = false;
                    dtpFrom.Enabled = false;
                    dtpTo.Enabled = false;


                }

                lueCustomerType.Properties.DataSource = CORE.CONSTANTS.ListCustomerTypes; // Load CustomerType
                lueCustomerType.Properties.DisplayMember = "Name";
                lueCustomerType.Properties.ValueMember = "ID";
                lueCustomerType.EditValue = this.CustomerType;

                lueStatusPay.Properties.DataSource = CORE.CONSTANTS.ListStatusPays;// Load StatusPay
                lueStatusPay.Properties.DisplayMember = "Name";
                lueStatusPay.Properties.ValueMember = "ID";
                lueStatusPay.EditValue = CORE.CONSTANTS.SelectedStatusPay(1).ID;



                this.LoadDataListUnPayBookingR();

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_Payment_Step1.frmTsk_Check_StatusPay_Load \n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //hiennv
        private void btnGetIDBookingRsAndIDCustomer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int IDBookigR = Convert.ToInt32(viewOwePay.GetFocusedRowCellValue("IDBookingR"));
                int IDBookigH = Convert.ToInt32(viewOwePay.GetFocusedRowCellValue("IDBookingH"));
                frmTsk_Payment_Step2 afrmTsk_Payment_Goverment_Step2 = new frmTsk_Payment_Step2(this, IDBookigR,IDBookigH);
                afrmTsk_Payment_Goverment_Step2.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_Payment_Step1.btnGetIDBookingRsAndIDCustomer_ButtonClick \n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //hiennv
        private void viewOwePay_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                int IDBookigR = Convert.ToInt32(viewOwePay.GetFocusedRowCellValue("IDBookingR"));
                int IDBookigH = Convert.ToInt32(viewOwePay.GetFocusedRowCellValue("IDBookingH"));
                frmTsk_Payment_Step2 afrmTsk_Payment_Goverment_Step2 = new frmTsk_Payment_Step2(this, IDBookigR, IDBookigH);
                afrmTsk_Payment_Goverment_Step2.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_Payment_Step1.viewOwePay_RowClick \n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintBookingRs_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {

                int IDBookigR = Convert.ToInt32(viewOwePay.GetFocusedRowCellValue("IDBookingR"));
                int IDBookigH = Convert.ToInt32(viewOwePay.GetFocusedRowCellValue("IDBookingH"));


                frmTsk_Payment_Step2 afrmTsk_Payment_Goverment_Step2 = new frmTsk_Payment_Step2(this, IDBookigR, IDBookigH,3);
                afrmTsk_Payment_Goverment_Step2.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_Payment_Step1.viewOwePay_RowClick \n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

    }
}