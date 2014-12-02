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
using DataAccess;
using BussinessLogic;
using Entity;
using CORESYSTEM;
using Library;
using System.IO;

namespace RoomManager
{
    public partial class frmTsk_BookingHall_Customer_New : DevExpress.XtraEditors.XtraForm
    {
        CompaniesBO aCompaniesBO = new CompaniesBO();
        CustomerGroupsBO aCustomerGroupsBO = new CustomerGroupsBO();
        CustomersBO aCustomersBO = new CustomersBO();
        GuestsBO aGuestsBO = new GuestsBO();
        private frmMain afrmMain = null;
        frmMain_Halls afrmMain_Halls = null;
        private List<HallExtStatusEN> aListAvailableHall = new List<HallExtStatusEN>();

        private NewBookingHEN aNewBookingHEN = new NewBookingHEN();

        private string CurrentCodeHall = null;
        private int IDCustomerGroup = 0;
        private int IDCompany = 0;
        private int IDCustomer = 0;
        private int IDBookingH,IDBookingR = 0;
        // Khởi tạo cho chọn Menus
        private List<Foods> aListFoods = new List<Foods>();
        private MenusEN aMenusEN = new MenusEN();
        
        public frmTsk_BookingHall_Customer_New(frmMain afrmMain)
        {
            InitializeComponent();
            this.afrmMain = afrmMain;
        }
         public frmTsk_BookingHall_Customer_New(int? IDBookingR,int? IDCompany,int? IDCustomer)
        {
            InitializeComponent();
            this.IDBookingR = Convert.ToInt32(IDBookingR);
            this.IDCompany = Convert.ToInt32(IDCompany);
            
            this.IDCustomer = Convert.ToInt32(IDCustomer);
        }


         public frmTsk_BookingHall_Customer_New(frmMain_Halls afrmMain_Halls)
        {
            InitializeComponent();
            this.afrmMain_Halls = afrmMain_Halls;
        }

        private void frmTsk_BookingHall_Customer_New_Load(object sender, EventArgs e)
        {
            dtpFrom.DateTime = DateTime.Now;
            this.LoadData();
            this.LoadService();
            if (this.CurrentCodeHall == null)
            {
                gridColumn14.Visible = false;
                btnAddService.Enabled = false;
            }
        }
        public void LoadData()
        {
            //Load dữ liệu công ty
            lueCompany.Properties.DataSource = aCompaniesBO.Select_ByType(3);// [Company] Type = 3 : khách lẻ 
            lueCompany.Properties.DisplayMember = "Name";
            lueCompany.Properties.ValueMember = "ID";
            if (this.IDCompany > 0)
            {
                lueCompany.EditValue = this.IDCompany;
            }
            else
            {
                lueCompany.EditValue = 0;
            }
            //Load dữ liệu khách
            lueCustomer.Properties.DataSource = aCustomersBO.Select_All();
            lueCustomer.Properties.DisplayMember = "Name";
            lueCustomer.Properties.ValueMember = "ID";
            if (this.IDCustomer > 0)
            {
                lueCustomer.EditValue = this.IDCustomer;

            }
            else
            {
                lueCustomer.EditValue = 0;
            }
           
            // Load khách mời
            GuestsBO aGuestsBO = new GuestsBO();
            lueGuest.Properties.DataSource = aGuestsBO.Select_All();
            lueGuest.Properties.DisplayMember = "Name";
            lueGuest.Properties.ValueMember = "ID";
            LoadDataListFoods();
        }

        public void LoadService()
        {
            try
            {
                ServicesBO aServicesBO = new ServicesBO();
                dgvService.DataSource = aServicesBO.Select_ServiceForHalls();
                dgvService.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.LoadService\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
           
        }

        public void LoadGuest()
        {
            try
            {
                GuestsBO aGuestsBO = new GuestsBO();
                lueGuest.Properties.DataSource = aGuestsBO.Select_All();
                lueGuest.Properties.DisplayMember = "Name";
                lueGuest.Properties.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.LoadService\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //Call Back data IDCustomer
        public void CallBackIDCustomer(int IDCustomer)
        {
            try
            {
                lueCustomer.EditValue = IDCustomer;
                txtPhoneNumber.Text = aCustomersBO.Select_ByID(IDCustomer).Tel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.CallBackIDCustomer\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //CallBackIDCompany
        public void CallBackIDCompany(int IDCompany)
        {
            try
            {
                lueCompany.EditValue = IDCompany;
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.CallBackIDCompany\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CallBackGuest(int IDGuest)
        {
            try
            {
                GuestsBO aGuestsBO = new GuestsBO();
                lueGuest.Properties.DataSource = aGuestsBO.Select_All();
                lueGuest.Properties.DisplayMember = "Name";
                lueGuest.Properties.ValueMember = "ID";
                lueGuest.EditValue = IDGuest;
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.CallBackGuest\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // truyền lại Service vừa thêm vào 2 List
        public void CallBackService(Services aServices)
        {
           
            try
            {
                this.LoadService();
                ServicesBO aServicesBO = new ServicesBO();
                ServiceUsedEN aServiceUsedEN = new ServiceUsedEN();  
              
                aServiceUsedEN.IDService = aServices.ID;
                aServiceUsedEN.NameService = aServices.Name;
                aServiceUsedEN.Cost = aServices.CostRef;
                aServiceUsedEN.CostRef_Service = aServices.CostRef;
                aServiceUsedEN.Unit = aServices.Unit;

                if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList().Count > 0)
                {
                    this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].InsertService(aServiceUsedEN);

                    dgvServiceInHall.DataSource = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListServiceUsed;
                    dgvServiceInHall.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btnSelectService_ButtonClick\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSearchCompany_Click(object sender, EventArgs e)
        {
            try
            {
                frmLst_Companies afrmLst_Companies = new frmLst_Companies(this, 3);//3:Khách lẻ
                afrmLst_Companies.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btn_BookingRs_Search_Company_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                frmLst_Customers afrmLst_Customers = new frmLst_Customers(this);
                afrmLst_Customers.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btn_BookingRs_Search_Company_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lueCustomer_EditValueChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(lueCustomer.EditValue);
            if (ID != 0)
            {
                txtPhoneNumber.Text = aCustomersBO.Select_ByID(ID).Tel;
                txtCustomerName.Enabled = false;
            }
            else
            {
                txtCustomerName.Enabled = true;
            }

        }


        private void btnSearchHall_Click(object sender, EventArgs e)
        {
             try
            {
                BookingHallsBO aBookingHallsBO = new BookingHallsBO();
                bool IsLunar;
                if (cbbLunarDate.Text == "Dương")
                {
                    IsLunar = false;
                }
                else
                {
                    IsLunar = true;
                }
                aListAvailableHall = aBookingHallsBO.GetListStatusHall(dtpFrom.DateTime, IsLunar).Where(p => p.HallStatus == 0).ToList();
                dgvAvailableHalls.DataSource = aListAvailableHall;
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btnSearchHalls_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelect_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == grvAvailableHalls.GetFocusedRowCellValue("Code").ToString()).ToList().Count > 0)
                {
                    MessageBox.Show("Hội trường này đã được chọn vui lòng chọn hội trường khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    NewBookingHallEN aNewBookingHallEN = new NewBookingHallEN();
                    aNewBookingHallEN.CodeHall = grvAvailableHalls.GetFocusedRowCellValue("Code").ToString();
                    aNewBookingHallEN.HallSku = grvAvailableHalls.GetFocusedRowCellValue("Sku").ToString();
                    aNewBookingHallEN.CostRef_Halls = Convert.ToDecimal(grvAvailableHalls.GetFocusedRowCellValue("CostRef"));
                    aNewBookingHallEN.HallType = Convert.ToInt32(grvAvailableHalls.GetFocusedRowCellValue("Type"));
                    aNewBookingHallEN.DisplayHallType = CORE.CONSTANTS.SelectedHallType(Convert.ToInt32(aNewBookingHallEN.HallType)).Name;
                    aNewBookingHallEN.TableOrPerson = Convert.ToInt32(grvAvailableHalls.GetFocusedRowCellValue("TableOrPerson"));
                    IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime Lunardate = DateTime.ParseExact(Convert.ToString(LunarDateExt.ToLunarDate(DateTime.Now, 7)), "d/M/yyyy", theCultureInfo);
                    aNewBookingHallEN.LunarDate = Lunardate;
                    aNewBookingHallEN.Date = DateTime.Now;
                    aNewBookingHallEN.BookingStatus = null;

                    if (aNewBookingHallEN.TableOrPerson == 1)
                    {
                        aNewBookingHallEN.DisplayTableOrPerson = "Người";
                    }
                    if (aNewBookingHallEN.TableOrPerson == 2)
                    {
                        aNewBookingHallEN.DisplayTableOrPerson = "Bàn";
                    }

                    this.aNewBookingHEN.aListBookingHallUsed.Add(aNewBookingHallEN);

                    dgvSelectedHalls.DataSource = this.aNewBookingHEN.aListBookingHallUsed;
                    dgvSelectedHalls.RefreshDataSource();

                    lueHalls.Reset();
                    lueHalls.Properties.DataSource = this.aNewBookingHEN.aListBookingHallUsed;
                    lueHalls.Properties.DisplayMember = "HallSku";
                    lueHalls.Properties.ValueMember = "CodeHall";


                    HallExtStatusEN aTemp = aListAvailableHall.Where(a => a.Code == grvAvailableHalls.GetFocusedRowCellValue("Code").ToString()).ToList()[0];
                    aListAvailableHall.Remove(aTemp);
                    dgvAvailableHalls.DataSource = aListAvailableHall;
                    dgvAvailableHalls.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btnSelect_ButtonClick\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUnSelect_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                HallExtStatusEN aHallExtStatusEN = new HallExtStatusEN();
                aHallExtStatusEN.Code = grvSelectedHalls.GetFocusedRowCellValue("CodeHall").ToString();
                aHallExtStatusEN.Sku = grvSelectedHalls.GetFocusedRowCellValue("HallSku").ToString();
                aHallExtStatusEN.CostRef = Convert.ToDecimal(grvSelectedHalls.GetFocusedRowCellValue("CostRef_Halls"));
                aHallExtStatusEN.Type = Convert.ToInt32(grvSelectedHalls.GetFocusedRowCellValue("HallType"));


                aListAvailableHall.Insert(0, aHallExtStatusEN);
                dgvAvailableHalls.DataSource = aListAvailableHall;
                dgvAvailableHalls.RefreshDataSource();

                NewBookingHallEN aTemp = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == grvSelectedHalls.GetFocusedRowCellValue("CodeHall").ToString()).ToList()[0];
                this.aNewBookingHEN.aListBookingHallUsed.Remove(aTemp);
                dgvSelectedHalls.DataSource = this.aNewBookingHEN.aListBookingHallUsed;

                lueHalls.Reset();
                lueHalls.Properties.DataSource = this.aNewBookingHEN.aListBookingHallUsed;
                lueHalls.Properties.DisplayMember = "HallSku";
                lueHalls.Properties.ValueMember = "CodeHall";
                
                dgvSelectedHalls.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btnUnSelect_ButtonClick\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grvSelectedHalls_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            this.CurrentCodeHall = grvSelectedHalls.GetFocusedRowCellValue("CodeHall").ToString();
            gridColumn14.Visible = true;
            btnAddService.Enabled = true;
            if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList().Count > 0)
            {
                NewBookingHallEN aTemp = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0];
            dgvServiceInHall.DataSource = aTemp.aListServiceUsed;
            dgvServiceInHall.RefreshDataSource();
           
            }
        }

        private void btnSelectService_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                ServiceUsedEN aServiceUsedEN = new ServiceUsedEN();
                int IDService = Convert.ToInt32(grvService.GetFocusedRowCellValue("ID"));
                aServiceUsedEN.IDService = IDService;
                aServiceUsedEN.NameService = grvService.GetFocusedRowCellValue("Name").ToString();
                aServiceUsedEN.Cost = Convert.ToDecimal(grvService.GetFocusedRowCellValue("CostRef"));
                aServiceUsedEN.CostRef_Service = Convert.ToDecimal(grvService.GetFocusedRowCellValue("CostRef"));
                aServiceUsedEN.Unit = grvService.GetFocusedRowCellValue("Unit").ToString();

                if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList().Count > 0)
                {
                    this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].InsertService(aServiceUsedEN);

                    dgvServiceInHall.DataSource = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListServiceUsed;
                    dgvServiceInHall.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btnSelectService_ButtonClick\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int IDService = Convert.ToInt32(grvServiceInHall.GetFocusedRowCellValue("IDService"));
                if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList().Count > 0)
                {
                    if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList().Count > 0)
                    {
                        this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].RemoveService(IDService);

                        dgvServiceInHall.DataSource = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListServiceUsed;
                        dgvServiceInHall.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btnCancel_ButtonClick\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grvSelectedHalls_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumn11")
            {
                string Sku = grvSelectedHalls.GetRowCellValue(e.RowHandle, "HallSku").ToString();
                for (int i = 0; i < this.aNewBookingHEN.aListBookingHallUsed.Count; i++)
                {
                    if (this.aNewBookingHEN.aListBookingHallUsed[i].HallSku == Sku)
                    {
                        this.aNewBookingHEN.aListBookingHallUsed[i].Cost = Convert.ToDecimal(e.Value); ;
                    }
                }
            }
            else if (e.Column.Name == "gridColumn12")
            {
                string Sku = grvSelectedHalls.GetRowCellValue(e.RowHandle, "HallSku").ToString();
                for (int i = 0; i < this.aNewBookingHEN.aListBookingHallUsed.Count; i++)
                {
                    if (this.aNewBookingHEN.aListBookingHallUsed[i].HallSku == Sku)
                    {
                        this.aNewBookingHEN.aListBookingHallUsed[i].Unit = Convert.ToInt32(e.Value);
                    }
                }
            }
            else if (e.Column.Name == "gridColumn13")
            {
                string Sku = grvSelectedHalls.GetRowCellValue(e.RowHandle, "HallSku").ToString();
                for (int i = 0; i < this.aNewBookingHEN.aListBookingHallUsed.Count; i++)
                {
                    if (this.aNewBookingHEN.aListBookingHallUsed[i].HallSku == Sku)
                    {
                        if (e.Value == "Người")
                        {
                            this.aNewBookingHEN.aListBookingHallUsed[i].TableOrPerson = 1;
                        }
                        else
                        {
                            this.aNewBookingHEN.aListBookingHallUsed[i].TableOrPerson = 2;
                        }
                    }
                }
            }
        }

        private void grvServiceInHall_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
             if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList().Count > 0)
                {
            if (e.Column.Name == "gridColumn16")
            {
                int ID = Convert.ToInt32(grvServiceInHall.GetFocusedRowCellValue("IDService"));

                for (int i = 0; i < this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListServiceUsed.Count; i++)
                {
                    if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListServiceUsed[i].IDService == ID)
                    {
                        this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListServiceUsed[i].Quantity = Convert.ToDouble(e.Value);
                    }
                }
            }
            if (e.Column.Name == "gridColumn17")
            {
                int ID = Convert.ToInt32(grvServiceInHall.GetFocusedRowCellValue("IDService"));
                for (int i = 0; i < this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListServiceUsed.Count; i++)
                {
                    if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListServiceUsed[i].IDService == ID)
                    {
                        this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListServiceUsed[i].Cost = Convert.ToDecimal(e.Value);
                    }
                }
            }
             }
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            frmIns_Services afrmIns_Services = new frmIns_Services(this);
            afrmIns_Services.ShowDialog();
        }

        private bool ValidateData()
        {
            if (txtSubject.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên buổi tiệc ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (Convert.ToInt32(lueCompany.EditValue) == 0 && txtCompanyName.Text == "")
            {

                MessageBox.Show("Vui lòng chọn hoặc nhập tên công ty.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }          
            else if (Convert.ToInt32(lueCustomer.EditValue) == 0 &&txtCustomerName.Text == "")
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập tên người đại diện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (this.aNewBookingHEN.aListBookingHallUsed.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hội trường.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (Convert.ToInt32(lueHalls.EditValue) !=0 && txtMenusName.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên thực đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;      
            }
            return true;
        }
        private void btnBook_Click(object sender, EventArgs e)
        {
            try
            {
                ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
                if (this.ValidateData() == true)
                {
                    //Truyền dữ liệu BookingH
                    this.aNewBookingHEN.Subject = txtSubject.Text;
                     this.aNewBookingHEN.CreatedDate = DateTime.Now;
                     this.aNewBookingHEN.CustomerType = 3;// 3 : Khách lẻ
                     this.aNewBookingHEN.BookingType = 3;//3 : Đặt trực tiếp
                    if (txtBookingMoney.Text == "")
                    {
                         this.aNewBookingHEN.BookingMoney = 0;
                        this.aNewBookingHEN.StatusPay = 1;//1 : Trạng thái chưa thanh toán
                    }
                    else
                    {
                         this.aNewBookingHEN.BookingMoney =  this.aNewBookingHEN.BookingMoney = Convert.ToDecimal(txtBookingMoney.Text);
                         this.aNewBookingHEN.StatusPay = 2;//2 : Trạng thái tạm ứng
                    }

                    this.aNewBookingHEN.Status = 2;//2: Trạng thái đã xác thực

                     this.aNewBookingHEN.PayMenthod = 1;                 
                     this.aNewBookingHEN.Type = 2;//2: Tiệc thuộc phạm trù bếp
                    
                     this.aNewBookingHEN.Disable = false;
                     this.aNewBookingHEN.Description = "";
                     if (Convert.ToInt32(lueCompany.EditValue) == 0)
                     {
                         this.IDCompany = this.aCompaniesBO.AutoInsertCompany(txtCompanyName.Text, 3);// 3 : Loại khách lẻ
                         string CustomerGroupName = "[Khách lẻ][" + txtCompanyName.Text + "][" + DateTime.Now.ToShortDateString() + "]";
                         this.IDCustomerGroup = this.aCustomerGroupsBO.AutoInsertCustomerGroup(CustomerGroupName, IDCompany);
                     }
                     else
                     {
                         this.IDCompany = Convert.ToInt32(lueCompany.EditValue);
                         string CustomerGroupName = "[Khách lẻ][" + lueCompany.Text + "][" + DateTime.Now.ToShortDateString() + "]";
                         this.IDCustomerGroup = this.aCustomerGroupsBO.AutoInsertCustomerGroup(CustomerGroupName, Convert.ToInt32(lueCompany.EditValue));
                     }
                     if (Convert.ToInt32(lueCustomer.EditValue) == 0)
                     {
                         this.IDCustomer = this.aCustomersBO.AutoInsertCustomer(txtCustomerName.Text, this.IDCustomerGroup, txtPhoneNumber.Text, DateTime.Now);
                     }
                     else
                     {
                         CustomerGroups_CustomersBO aCustomerGroups_CustomersBO = new CustomerGroups_CustomersBO();
                         this.IDCustomer = Convert.ToInt32(lueCustomer.EditValue);
                         aCustomerGroups_CustomersBO.AutoInsertCustomerToGroup(IDCustomer, this.IDCustomerGroup, DateTime.Now);
                     }
                     this.aNewBookingHEN.Disable = false;
                     this.aNewBookingHEN.IDCustomer = this.IDCustomer;
                     this.aNewBookingHEN.IDSystemUser = CORE.CURRENTUSER.SystemUser.ID;
                     this.aNewBookingHEN.IDCustomerGroup = this.IDCustomerGroup;
                    // 
                     this.IDBookingH = aReceptionTaskBO.NewBookHall(this.aNewBookingHEN);
                     if (this.IDBookingR != 0)
                     {
                         BookingRs_BookingHsBO aBookingRs_BookingHsBO = new BookingRs_BookingHsBO();

                         BookingRs_BookingHs aBookingRs_BookingHs = new BookingRs_BookingHs();
                         aBookingRs_BookingHs.IDBookingR = this.IDBookingR;
                         aBookingRs_BookingHs.IDBookingH = this.IDBookingH;
                         aBookingRs_BookingHs.Type = String.Empty;
                         aBookingRs_BookingHs.Status = String.Empty;
                         aBookingRs_BookingHs.Disable = false;
                         aBookingRs_BookingHs.Extension1 = String.Empty;
                         aBookingRs_BookingHs.Extension2 = String.Empty;
                         aBookingRs_BookingHs.Extension3 = String.Empty;

                         aBookingRs_BookingHsBO.Insert(aBookingRs_BookingHs);
                     }
                     MessageBox.Show("Đặt hội trường thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     if (afrmMain_Halls != null)
                     {
                         this.afrmMain_Halls.Reload();
                     }
                     this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btnBook_Click\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            BookingHs aBookingHs = new BookingHs();

            aBookingHs.CreatedDate = DateTime.Now;
            aBookingHs.CustomerType = 1;
            aBookingHs.BookingType = 1;
            aBookingHs.Note = "";
            aBookingHs.IDGuest = 0;
            aBookingHs.StatusPay = 2;
            aBookingHs.BookingMoney = 0;
            aBookingHs.Status = 1;
            aBookingHs.Type = 2;
            aBookingHs.Disable = false;
            aBookingHs.Level = 2;
            aBookingHs.Subject = "aaaa";
            aBookingHs.IDCustomerGroup = 65;
            aBookingHs.IDCustomer = 3;
            aBookingHs.IDSystemUser = 1;
            aBookingHs.Description = "";
            BookingHsBO aBookingHsBO = new BookingHsBO();
            int IDBookingH = aBookingHsBO.InsertUnSync(aBookingHs);
        }

        private void lueCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lueCompany.EditValue) == 0)
            {
                txtCompanyName.Enabled = true;
            }
            else
            {
                txtCompanyName.Enabled = false;
            }
        }


        private void btnAddGuest_Click(object sender, EventArgs e)
        {
            try
            {
                frmIns_Guest afrmIns_Guest = new frmIns_Guest(this);
                afrmIns_Guest.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btnAddGuest_Click\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchGuest_Click(object sender, EventArgs e)
        {
            try
            {
                frmLst_Guests afrmLst_Guests = new frmLst_Guests(this);
                afrmLst_Guests.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.btnSearchGuest_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lueGuest_EditValueChanged(object sender, EventArgs e)
        {
            int IDGuest = Convert.ToInt32(lueGuest.EditValue);
        }
        #region Menus
        public Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmIns_Menus.ConvertByteArrayToImage\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
  
        public byte[] ConvertImageToByteArray(Image imageIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmIns_Menus.ConvertimageToByteArray\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        // Load data cho bảng món ăn 
        public void LoadDataListFoods()
        {
            try
            {
                this.aListFoods.Clear();
                FoodsBO aFoodsBO = new FoodsBO();
                MenusBO aMenusBO = new MenusBO();
                List<Foods> aListFoods = aFoodsBO.Select_All();
                List<int> ListID = new List<int>();
                for (int i = 0; i < aListFoods.Count; i++)
                {
                    ListID.Add(aListFoods[i].ID);
                }
                List<Foods> aListTemp = aFoodsBO.Select_ByListID(ListID);
                foreach (Foods item in aListFoods)
                {
                    Foods aFoods = aListTemp.Where(p => p.ID == item.ID).ToList()[0];
                    if (aFoods.Image1 != null)
                    {
                        if (aFoods.Image1.Length <= 0)
                        {
                            Image image = RoomManager.Properties.Resources.logo_nkcp_small;
                            image = image.GetThumbnailImage(70, 70, null, IntPtr.Zero);
                            Byte[] aImageByte = this.ConvertImageToByteArray(image);
                            aFoods.Image1 = aImageByte;
                        }
                    }
                    else
                    {
                        Image image = RoomManager.Properties.Resources.logo_nkcp_small;
                        image = image.GetThumbnailImage(70, 70, null, IntPtr.Zero);
                        Byte[] aImageByte = this.ConvertImageToByteArray(image);
                        aFoods.Image1 = aImageByte;
                    }

                    this.aListFoods.Add(aFoods);
                }
                dgvAvailableFoods.DataSource = this.aListFoods;
                dgvAvailableFoods.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_BookingHall_Customer_New.LoadDataListFoods\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Chọn món ăn vào Menus trong hội trường
        private void btnSelectFoods_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].Name == "")
                {
                    this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].Name = txtMenusName.Text;
                }
                if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].aListFoods.Where(f => f.ID == Convert.ToInt32(viewAvailableFoods.GetFocusedRowCellValue("ID"))).ToList().Count() > 0)
                {
                    MessageBox.Show("Món ăn này đã có trong thực đơn vui lòng chọn món ăn khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Foods aFoods = new Foods();
                    aFoods.ID = Convert.ToInt32(viewAvailableFoods.GetFocusedRowCellValue("ID"));
                    aFoods.Name = Convert.ToString(viewAvailableFoods.GetFocusedRowCellValue("Name"));
                    aFoods.Name1 = Convert.ToString(viewAvailableFoods.GetFocusedRowCellValue("Name1"));
                    aFoods.Name2 = Convert.ToString(viewAvailableFoods.GetFocusedRowCellValue("Name2"));
                    aFoods.Name3 = Convert.ToString(viewAvailableFoods.GetFocusedRowCellValue("Name3"));
                    aFoods.Image1 = (byte[])viewAvailableFoods.GetFocusedRowCellValue("Image1");

                    this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].aListFoods.Add(aFoods);
                    dgvSelectFoods.DataSource = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].aListFoods;
                    dgvSelectFoods.RefreshDataSource();

                    Foods aFoodsTemp = this.aListFoods.Where(f => f.ID == Convert.ToInt32(viewAvailableFoods.GetFocusedRowCellValue("ID"))).ToList()[0];
                    this.aListFoods.Remove(aFoodsTemp);
                    dgvAvailableFoods.DataSource = this.aListFoods;
                    dgvAvailableFoods.RefreshDataSource();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmIns_Menus.btnSelectFoods_ButtonClick\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUnSelectFoods_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (this.aListFoods.Where(f => f.ID == Convert.ToInt32(viewSelectFoods.GetFocusedRowCellValue("ID"))).ToList().Count() == 0)
                {
                    Foods aFoods = new Foods();
                    aFoods.ID = Convert.ToInt32(viewSelectFoods.GetFocusedRowCellValue("ID"));
                    aFoods.Name = Convert.ToString(viewSelectFoods.GetFocusedRowCellValue("Name"));
                    aFoods.Name1 = Convert.ToString(viewSelectFoods.GetFocusedRowCellValue("Name1"));
                    aFoods.Name2 = Convert.ToString(viewSelectFoods.GetFocusedRowCellValue("Name2"));
                    aFoods.Name3 = Convert.ToString(viewSelectFoods.GetFocusedRowCellValue("Name3"));
                    aFoods.Image1 = (byte[])viewSelectFoods.GetFocusedRowCellValue("Image1");

                    this.aListFoods.Insert(0, aFoods);
                    dgvAvailableFoods.DataSource = this.aListFoods;
                    dgvAvailableFoods.RefreshDataSource();

                }
                Foods aFoodTemp = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].aListFoods.Where(f => f.ID == Convert.ToInt32(viewSelectFoods.GetFocusedRowCellValue("ID"))).ToList()[0];
                this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].aListFoods.Remove(aFoodTemp);
                dgvSelectFoods.DataSource = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].aListFoods;
                dgvSelectFoods.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmIns_Menus.btnUnSelectFoods_ButtonClick\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lueHalls_EditValueChanged(object sender, EventArgs e)
        {
            this.CurrentCodeHall = lueHalls.EditValue.ToString();
            if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList().Count > 0)
            {
                if (this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN.Count > 0)
                {
                    txtMenusName.Text = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].Name;
                    dgvSelectFoods.DataSource = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].aListFoods;
                    dgvServiceInHall.RefreshDataSource();
                }
                else
                {
                    MenusEN aMenusEN = new MenusEN();
                    aMenusEN.Name = txtMenusName.Text;
                    this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].InsertMenu(aMenusEN);
                    dgvSelectFoods.DataSource = this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].aListFoods;
                    dgvServiceInHall.RefreshDataSource();
                }
            }
            
        }
        #endregion      

        private void txtMenusName_Leave(object sender, EventArgs e)
        {
            this.aNewBookingHEN.aListBookingHallUsed.Where(a => a.CodeHall == CurrentCodeHall).ToList()[0].aListMenuEN[0].Name = txtMenusName.Text;
        }

        


        

      

       
       

       

        

      

        

       



    }
}