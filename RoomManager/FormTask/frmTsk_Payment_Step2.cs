using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using DataAccess;
using BussinessLogic;
using Entity;
using DevExpress.XtraReports.UI;
using DevExpress.Utils;
using System.Globalization;
using CORESYSTEM;
using System.Drawing;
using System.IO;

namespace RoomManager
{
    public partial class frmTsk_Payment_Step2 : DevExpress.XtraEditors.XtraForm
    {
        public bool IsLockForm = false;
        public frmTsk_Payment_Step1 afrmTsk_Payment_Step1 = null;
        private NewPaymentEN aNewPaymentEN = new NewPaymentEN();
        private PaymentEN aPaymentEN = new PaymentEN();
        private int IDBookingR = 0;
        private int CurrentIDBookingRoom = 0;
        private int CurrentIDBookingHall = 0;

        private PaymentHallsEN aPaymentHallsEN = new PaymentHallsEN();
        private int IDBookingH = 0;
        private DateTime CheckOut = DateTime.Now;
        private decimal ExtraMoneyRoom = 0;
        private string CodeRoom = string.Empty;

        //Hiennv
        public frmTsk_Payment_Step2(frmTsk_Payment_Step1 afrmTsk_Payment_Step1, int IDBookingR, int IDBookingH)
        {
            InitializeComponent();
            this.afrmTsk_Payment_Step1 = afrmTsk_Payment_Step1;
            this.IDBookingR = IDBookingR;
            this.IDBookingH = IDBookingH;
        }
        //Hiennv
        public frmTsk_Payment_Step2(int IDBookingR, int IDBookingH)
        {
            InitializeComponent();
            this.IDBookingR = IDBookingR;
            this.IDBookingH = IDBookingH;
            xtraTabControl1.SelectedTabPageIndex = 2;
        }
        #region Payment Hiển
       
        //Hiennv


        //----------------------------- Code lien quan den phong  ------------------------------------

        //hiennv
      
        //Hiennv
        private void LoadListRoom()
        {
            try
            {
                List<RoomsEN> aListRoom = new List<RoomsEN>();
                RoomsEN aRoomsEN;
                for (int i = 0; i < aPaymentEN.aListInfoDetailPaymentEN.Count; i++)
                {
                    aRoomsEN = new RoomsEN();
                    aRoomsEN.Sku = aPaymentEN.aListInfoDetailPaymentEN[i].Sku;
                    aRoomsEN.Code = aPaymentEN.aListInfoDetailPaymentEN[i].aBookingRooms.CodeRoom;
                    aRoomsEN.IDBookingR = aPaymentEN.IDBookingR;
                    aRoomsEN.IDBookingRooms = aPaymentEN.aListInfoDetailPaymentEN[i].aBookingRooms.ID;
                    aRoomsEN.TotalMoney = aPaymentEN.GetMoneyRoomAndService(aPaymentEN.aListInfoDetailPaymentEN[i].aBookingRooms.ID);
                    aListRoom.Add(aRoomsEN);
                }
                dgvRooms.DataSource = aListRoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.LoadListRoom\n" + ex.ToString());
            }
        }
        //Hiennv

        //Hiennv
     
        //Hiennv
    
        //Hiennv
      
        //Hiennv
        private void InsertDataToPayment()
        {
            try
            {
                /*-------*/
                RoomsBO aRoomsBO = new RoomsBO();
                ServicesBO aServicesBO = new ServicesBO();
                PaymentBO aPaymentBO = new PaymentBO();
                Payment aPayment = new Payment();



                aPayment.IDBookingR = this.aPaymentEN.IDBookingR;
                aPayment.IDCustomer = this.aPaymentEN.IDCustomer;
                aPayment.NameCustomer = this.aPaymentEN.NameCustomer;
                aPayment.IDSystemUser = this.aPaymentEN.IDSystemUser;
                aPayment.NameSystemUser = this.aPaymentEN.NameSystemUser;
                aPayment.IDCustomerGroup = this.aPaymentEN.IDCustomerGroup;
                aPayment.NameCustomerGroup = this.aPaymentEN.NameCustomerGroup;
                aPayment.IDCompany = this.aPaymentEN.IDCompany;
                aPayment.NameCompany = this.aPaymentEN.NameCustomer;
                aPayment.CreatedDate_BookingR = this.aPaymentEN.CreatedDate_BookingR;
                aPayment.CustomerType = this.aPaymentEN.CustomerType;
                aPayment.BookingType = this.aPaymentEN.BookingType;
                aPayment.PayMenthod = Convert.ToInt32(lueBookingR_Paymethod.EditValue);
                aPayment.StatusPay = 3;
                aPayment.BookingMoney = this.aPaymentEN.GetBookingMoney();
                aPayment.ExchangeRate = this.aPaymentEN.ExchangeRate;
                aPayment.Status_BookingR = 8;
                aPayment.Level = this.aPaymentEN.Level;
                aPayment.Status = 8; // de tam
                aPayment.Type = 1; // de tam

                foreach (InfoDetailPaymentEN item1 in this.aPaymentEN.aListInfoDetailPaymentEN)
                {
                    if (item1.aBookingRooms.IDBookingR == aPaymentEN.IDBookingR)
                    {
                        aPayment.IDBookingRoom = item1.aBookingRooms.ID;
                        aPayment.CodeRoom = item1.aBookingRooms.CodeRoom;
                        Rooms aRooms = aRoomsBO.Select_ByCodeRoom(item1.aBookingRooms.CodeRoom, 1);
                        if (aRooms != null)
                        {
                            aPayment.Sku = aRooms.Sku;
                        }
                        aPayment.Cost_BookingRoom = item1.aBookingRooms.Cost;
                        aPayment.PercentTax_BookingRoom = item1.aBookingRooms.PercentTax;
                        aPayment.CostRef_Rooms = item1.aBookingRooms.CostRef_Rooms;
                        aPayment.CheckInActual = item1.aBookingRooms.CheckInActual;
                        aPayment.CheckOutActual = item1.aBookingRooms.CheckOutActual;
                        aPayment.Status_BookingRoom = 8;
                        aPayment.TimeInUse = Convert.ToDecimal(item1.DateInUse * 24 * 60);
                        aPayment.IndexSubRooms = item1.IndexSubRooms;
                        foreach (ServicesEN item2 in item1.aListService)
                        {
                            if (item1.aBookingRooms.ID == item2.IDBookingRoom)
                            {
                                aPayment.IDService = item2.IDService;
                                aPayment.NameService = item2.Name;
                                aPayment.Cost_Services = item2.Cost;
                                aPayment.DateUseServices = item2.Date;
                                aPayment.PercentTax_Services = item2.PercentTax;
                                aPayment.CostRef_Services = item2.CostRef_Service;
                                aPayment.Quantity_Services = item2.Quantity;
                                aPayment.Status_Services = 8;
                                aPayment.IndexSubRooms = item2.IndexSubPayment;

                                aPaymentBO.Insert(aPayment);
                            }

                        }

                    }
                }

                /*-------*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.InsertDataToPayment\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiennv
       
        //Hiennv
       
        //Hiennv
     
        //Hiennv
    
        //Hiennv
        private void GetTotalMoney(string codeRoom)
        {
            try
            {
                if (String.IsNullOrEmpty(codeRoom) == false && this.aPaymentEN.Status_BookingR == 8)
                {
                    txtBookingRMoney.EditValue = 0;
                    lblTotalMoneyR.Text = String.Format("{0:0,0}", (aPaymentEN.GetTotalMoneyBookingRBehindTax()));
                }
                else
                {
                    txtBookingRMoney.EditValue = this.aPaymentEN.GetBookingMoney();
                    lblTotalMoneyR.Text = String.Format("{0:0,0}", (aPaymentEN.GetTotalMoneyBookingRBehindTax() - this.aPaymentEN.GetBookingMoney()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.GetTotalMoney\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiennv
      
        //Hiennv
       
        //Hiennv
     
        //Hiennv
      
        //Hiennv
      

        //------------------------------- Ket thuc code lien quan den phong ---------------------------------------


        //------------------------------- Code lien quan den hoi truong  ------------------------------------------

        //hiennv
        public void LoadListHall()
        {
            try
            {
                List<HallsEN> aListHallsEN = new List<HallsEN>();
                HallsEN aHallsEN;
                for (int i = 0; i < aPaymentHallsEN.aListInfoDetailPaymentHallsEN.Count; i++)
                {
                    aHallsEN = new HallsEN();
                    HallsBO aHallsBO = new HallsBO();
                    Halls aHalls = aHallsBO.Select_ByCodeHall(aPaymentHallsEN.aListInfoDetailPaymentHallsEN[i].aBookingHalls.CodeHall, 1);
                    aHallsEN.Sku = aHalls.Sku;
                    aHallsEN.CodeHall = aPaymentHallsEN.aListInfoDetailPaymentHallsEN[i].aBookingHalls.CodeHall;
                    aHallsEN.IDBookingH = aPaymentHallsEN.IDBookingH;
                    aHallsEN.IDBookingHall = aPaymentHallsEN.aListInfoDetailPaymentHallsEN[i].aBookingHalls.ID;
                    aHallsEN.TotalMoney = aPaymentHallsEN.GetMoneyHallAndService(aPaymentHallsEN.aListInfoDetailPaymentHallsEN[i].aBookingHalls.ID);
                    aListHallsEN.Add(aHallsEN);
                }
                dgvHalls.DataSource = aListHallsEN;
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.LoadListHall\n" + ex.ToString());
            }
        }
        // Author : Hiennv
        public void LoadCustomerInfo()
        {
            try
            {
                BookingHsBO aBookingHsBO = new BookingHsBO();
                BookingHs aBookingHs = aBookingHsBO.Select_ByID(this.IDBookingH);
                if (aBookingHs != null)
                {
                    CompaniesBO aCompaniesBO = new CompaniesBO();
                    Companies aCompanies = aCompaniesBO.Select_ByIDCustomerGroup(aBookingHs.IDCustomerGroup);
                    if (aCompanies != null)
                    {
                        lblCompany.Text = aCompanies.Name;
                    }
                    CustomerGroupsBO aCustomerGroupsBO = new CustomerGroupsBO();
                    CustomerGroups aCustomerGroups = aCustomerGroupsBO.Select_ByID(aBookingHs.IDCustomerGroup);
                    if (aCustomerGroups != null)
                    {
                        lblNameCustomerGroup.Text = aCustomerGroups.Name;
                    }
                    CustomersBO aCustomersBO = new CustomersBO();
                    Customers aCustomers = aCustomersBO.Select_ByID(aBookingHs.IDCustomer);
                    if (aCustomers != null)
                    {
                        lblNameCustomer.Text = aCustomers.Name;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.LoadCustomerInfo\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiennv
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
                MessageBox.Show("frmTsk_PaymentStep2.ConvertByteArrayToImage\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //Hiennv
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
                MessageBox.Show("frmTsk_PaymentStep2.ConvertimageToByteArray\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        //hiennv
        
        //Hiennv
   
        //Hiennv
       
        //hiennv
        
        //Hiennv
       

        //Hiennv
       
        //Hiennv
       
        //Hiennv
      
        //Hiennv
       

        //---------------------- ket thuc code hoi truong ------------------------

        //Hiennv
     
        //Hiennv
      
        //Hiennv
      
        //Hiennv
        public void GetTotalMoneyBookingRAndBookingH()
        {
            try
            {
                decimal? beforTax = Convert.ToDecimal(this.aPaymentEN.GetTotalMoneyBookingRBeforeTax()) + Convert.ToDecimal(this.aPaymentHallsEN.GetTotalMoneyBookingHBeforeTax());
                decimal? behindTax = Convert.ToDecimal(this.aPaymentEN.GetTotalMoneyBookingRBehindTax()) + Convert.ToDecimal(this.aPaymentHallsEN.GetTotalMoneyBookingHBehindTax());
                decimal? bookingMoney = Convert.ToDecimal(this.aPaymentEN.GetBookingMoney()) + Convert.ToDecimal(this.aPaymentHallsEN.GetBookingMoney());



            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.GetTotalMoneyBookingRAndBookingH\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiennv
        private void lblTotalMoneyH_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.GetTotalMoneyBookingRAndBookingH();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.lblTotalMoneyH_TextChanged\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiennv
        private void lblTotalMoneyR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.GetTotalMoneyBookingRAndBookingH();

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.lblTotalMoneyR_TextChanged\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiennv




      
        #endregion

        #region Payment Linh
        public void InitData(int IDBookingR, int IDBookingH)
        {
            CompaniesBO aCompaniesBO = new CompaniesBO();
            CustomerGroupsBO aCustomerGroupsBO = new CustomerGroupsBO();
            SystemUsersBO aSystemUsersBO = new SystemUsersBO();
            BookingHsBO aBookingHsBO = new BookingHsBO();
            BookingRsBO aBookingRsBO = new BookingRsBO();
            BookingRoomsBO aBookingRoomBO = new BookingRoomsBO();
            CustomersBO aCustomersBO = new CustomersBO();
            ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
            RoomsBO aRoomsBO = new RoomsBO();
            HallsBO aHallsBO = new HallsBO();
            BookingHallsBO aBookingHallsBO = new BookingHallsBO();
            FoodsBO aFoodsBO = new FoodsBO();


            BookingRs aBookingRs = aBookingRsBO.Select_ByID(IDBookingR);
            BookingHs aBookingHs = aBookingHsBO.Select_ByID(IDBookingH);

            // Truyen du lieu chung cua NewPayment
            if (aBookingRs != null)
            {
                aNewPaymentEN.IDBookingR = aBookingRs.ID;
                aNewPaymentEN.IDCustomer = aBookingRs.IDCustomer;
                Customers aCustomers = aCustomersBO.Select_ByID(aBookingRs.IDCustomer);
                if (aCustomers != null)
                {
                    aNewPaymentEN.NameCustomer = aCustomers.Name;
                }
                aNewPaymentEN.IDSystemUser = aBookingRs.IDSystemUser;
                SystemUsers aSystemUsers = aSystemUsersBO.Select_ByID(aBookingRs.IDSystemUser);
                if (aSystemUsers != null)
                {
                    aNewPaymentEN.NameSystemUser = aSystemUsers.Name;
                }
                aNewPaymentEN.IDCustomerGroup = aBookingRs.IDCustomerGroup;
                CustomerGroups aCustomerGroups = aCustomerGroupsBO.Select_ByID(aBookingRs.IDCustomerGroup);
                if (aCustomerGroups != null)
                {
                    aNewPaymentEN.NameCustomerGroup = aCustomerGroups.Name;
                    aNewPaymentEN.IDCompany = aCustomerGroups.IDCompany;
                    Companies aCompanies = aCompaniesBO.Select_ByID(aCustomerGroups.IDCompany);
                    if (aCompanies != null)
                    {
                        aNewPaymentEN.NameCompany = aCompanies.Name;
                        aNewPaymentEN.TaxNumberCodeCompany = aCompanies.TaxNumberCode;
                        aNewPaymentEN.AddressCompany = aCompanies.Address;
                    }
                }
                aNewPaymentEN.PayMenthod = aBookingRs.PayMenthod;
                aNewPaymentEN.CreatedDate_BookingR = aBookingRs.CreatedDate;
                aNewPaymentEN.CustomerType = aBookingRs.CustomerType;
                aNewPaymentEN.Status_BookingR = aBookingRs.Status;
                aNewPaymentEN.StatusPay = aBookingRs.StatusPay;
                aNewPaymentEN.BookingRMoney = aBookingRs.BookingMoney;
                aNewPaymentEN.Status_BookingR = aBookingRs.Status;

                // Truyen du lieu cho List BookingRoom cua NewPayment
                List<BookingRooms> aListBookingRooms = aBookingRoomBO.Select_ByIDBookingRs(this.IDBookingR);
                if (aListBookingRooms.Count > 0)
                {
                    BookingRoomUsedEN aBookingRoomUsedEN;

                    foreach (BookingRooms item in aListBookingRooms)
                    {
                        aBookingRoomUsedEN = new BookingRoomUsedEN();
                        aBookingRoomUsedEN.SetValue(item);
                       
                        Rooms aRooms = aRoomsBO.Select_ByCodeRoom(item.CodeRoom, 1);
                        if (aRooms != null)
                        {
                            aBookingRoomUsedEN.RoomSku = aRooms.Sku;
                        }
                        else
                        {
                            aBookingRoomUsedEN.RoomSku = string.Empty;
                        }
                        if (item.Status == 8 || item.Status == 7)
                        {
                            aBookingRoomUsedEN.AddTimeEnd = item.AddTimeEnd;
                            aBookingRoomUsedEN.AddTimeStart = item.AddTimeStart;
                            aBookingRoomUsedEN.TimeInUse = item.TimeInUse;
                            aBookingRoomUsedEN.DateUsed = Convert.ToDouble(item.TimeInUse / (24 * 60)+item.AddTimeStart + item.AddTimeEnd);
                            
                        }
                        else
                        {
                            aBookingRoomUsedEN.AddTimeStart = Convert.ToDecimal(aReceptionTaskBO.GetAddTimeStart(item.ID, item.CheckInActual));
                            aBookingRoomUsedEN.AddTimeEnd = Convert.ToDecimal(aReceptionTaskBO.GetAddTimeEnd(item.ID, item.CheckOutPlan));
                            aBookingRoomUsedEN.TimeInUse = Convert.ToDecimal(aReceptionTaskBO.GetTimeInUsed(item.ID, item.CheckInActual, item.CheckOutPlan) * 24 * 60);
                            aBookingRoomUsedEN.DateUsed = Convert.ToDouble(aBookingRoomUsedEN.TimeInUse / (24 * 60) + aBookingRoomUsedEN.AddTimeStart + aBookingRoomUsedEN.AddTimeEnd);
                        }
                        decimal? cost = 0;
                        if (item.Cost == null || item.Cost == 0)
                        {
                            cost = item.CostRef_Rooms;
                        }
                        else
                        {
                            cost = item.Cost;
                        }
                        aBookingRoomUsedEN.ListServiceUsed = aReceptionTaskBO.GetListServiceUsedInRoom_ByIDBookingRoom(item.ID);
                        aBookingRoomUsedEN.ListCustomer = aCustomersBO.SelectListCustomer_ByIDBookingRoom(item.ID);
                        aBookingRoomUsedEN.Cost = cost + this.ExtraMoney(aRooms.Sku, aBookingRoomUsedEN.ListCustomer.Count, Convert.ToInt32(aBookingRs.CustomerType), "G1");
                        aBookingRoomUsedEN.TotalMoney = aBookingRoomUsedEN.GetMoneyRoom();
                        aBookingRoomUsedEN.MoneyRoomBeforeTax = aBookingRoomUsedEN.GetOnlyMoneyRoomBeforeTax();
                        //Hiennv 
                        aBookingRoomUsedEN.DisplayMoneyTaxRoom = aBookingRoomUsedEN.GetOnlyMoneyRoomBeforeTax() * 10 / 100;
                        
                        aBookingRoomUsedEN.MoneyRoom = aBookingRoomUsedEN.GetOnlyMoneyRoom();
                        aBookingRoomUsedEN.IsPaid = aBookingRoomUsedEN.IsPaidRoom();
                        aNewPaymentEN.aListBookingRoomUsed.Add(aBookingRoomUsedEN);
                    }
                }

                if (aBookingHs != null)
                {
                    aNewPaymentEN.IDBookingH = aBookingHs.ID;
                    aNewPaymentEN.CreatedDate_BookingH = aBookingHs.CreatedDate;
                    aNewPaymentEN.CustomerType = aBookingHs.CustomerType;
                    aNewPaymentEN.Status_BookingH = aBookingHs.Status;
                    aNewPaymentEN.BookingHMoney = aBookingHs.BookingMoney;
                    // Truyen du lieu cho List BookingHall cua NewPayment
                    List<BookingHalls> aListBookingHalls = aBookingHallsBO.Select_ByIDBookigH(this.IDBookingH);
                    if (aListBookingHalls != null)
                    {
                        BookingHallUsedEN aBookingHallUsedEN;
                        foreach (BookingHalls item in aListBookingHalls)
                        {
                            aBookingHallUsedEN = new BookingHallUsedEN();
                            aBookingHallUsedEN.SetValue(item);
                            Halls aHalls = aHallsBO.Select_ByCodeHall(item.CodeHall, 1);
                            if (aHalls != null)
                            {
                                aBookingHallUsedEN.HallSku = aHalls.Sku;
                            }
                            else
                            {
                                aBookingHallUsedEN.HallSku = string.Empty;
                            }
                            aBookingHallUsedEN.CustomerType = aBookingHs.CustomerType;
                            aBookingHallUsedEN.BookingTypeBookingH = aBookingHs.BookingType;
                            aBookingHallUsedEN.StatusPayBookingH = aBookingHs.StatusPay;
                            aBookingHallUsedEN.LevelBookingH = aBookingHs.Level;
                            aBookingHallUsedEN.aListMenuEN = aReceptionTaskBO.GetListMenus_ByIDBookingHall(item.ID);
                            aBookingHallUsedEN.IsPaid = aBookingHallUsedEN.IsPaidHall();
                            List<ServiceUsedEN> aListServiceTemp = aReceptionTaskBO.GetListServiceUsedInHall_ByIDBookingHall(item.ID);
                            foreach (ServiceUsedEN aTemp in aListServiceTemp)
                            {
                                aTemp.TotalMoney = aTemp.GetMoneyService();
                                aTemp.TotalMoneyBeforeTax = aTemp.GetMoneyServiceBeforeTax();
                                aTemp.IsPaid = aTemp.IsPaidService();
                                aBookingHallUsedEN.aListServiceUsed.Add(aTemp);
                            }
                            aNewPaymentEN.aListBookingHallUsed.Add(aBookingHallUsedEN);
                        }
                    }
                }


            }

        }
        private void frmTsk_Payment_Step2_Load(object sender, EventArgs e)
        {
            try
            {
                this.InitData(this.IDBookingR, this.IDBookingH);
                this.LoadData();

            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.frmTsk_Payment_Step2_Load\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Reload(NewPaymentEN aNewPayment)
        {
            this.aNewPaymentEN = aNewPaymentEN;
            this.LoadData();
        }
      
        public void LoadDataServiceForBookingHall(int IDBookingHall)
        {
            try
            {
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.LoadDataServiceForBookingHall\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void LoadData()
        {
            //-------------- Phòng ---------------------

            //Thong tin nguoi dat
            lblCompany.Text = this.aNewPaymentEN.NameCompany;
            lblNameCustomerGroup.Text = this.aNewPaymentEN.NameCustomerGroup;
            lblNameCustomer.Text = this.aNewPaymentEN.NameCustomer;
            txtAddressR.Text = this.aNewPaymentEN.AddressCompany;
            txtTaxNumberCodeR.Text = this.aNewPaymentEN.TaxNumberCodeCompany;


            // Trang thai, hinh thuc thanh toan
            lueBookingR_Paymethod.Properties.DataSource = CORE.CONSTANTS.ListPayMethods;
            lueBookingR_Paymethod.Properties.DisplayMember = "Name";
            lueBookingR_Paymethod.Properties.ValueMember = "ID";
            lueBookingR_Paymethod.EditValue = CORE.CONSTANTS.SelectedPayMethod(Convert.ToInt32(this.aNewPaymentEN.PayMenthod)).ID;

            // Thong tin gia tiền, đặt trước
            lblTotalMoneyRooms1.Text = String.Format("{0:0,0}", this.aNewPaymentEN.GetMoneyRooms());
            txtBookingRMoney.EditValue = this.aNewPaymentEN.BookingRMoney;
            lblTotalMoneyR.Text = String.Format("{0:0,0}", this.aNewPaymentEN.GetMoneyRooms() - this.aNewPaymentEN.BookingRMoney);
            // Bang danh sach phong
            dgvRooms.DataSource = this.aNewPaymentEN.aListBookingRoomUsed;
            dgvRooms.RefreshDataSource();
            //Thong tin 1 phong                      

            if (this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList().Count > 0)
            {
                BookingRoomUsedEN aBookingRoomUsedEN = this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0];
                if (aBookingRoomUsedEN.Type == 3)//Tính checkin sớm và Checkout muộn
                {
                    chkCheckIn.Checked = true;
                    chkCheckOut.Checked = true;
                }
                else if (aBookingRoomUsedEN.Type == 0)//Không tính checkIn sớm và checkout muộn.
                {
                    chkCheckIn.Checked = false;
                    chkCheckOut.Checked = false;
                }
                else if (aBookingRoomUsedEN.Type == 2)//Tính checkin sớm ,không tính checkout muộn.
                {
                    chkCheckIn.Checked = true;
                    chkCheckOut.Checked = false;
                }
                else if (aBookingRoomUsedEN.Type == 1)//Không tính checkin sớm ,tính checkout muộn
                {
                    chkCheckIn.Checked = false;
                    chkCheckOut.Checked = true;
                }

                lblSkuRooms.Text = aBookingRoomUsedEN.RoomSku;               
                dtpCheckInActual.DateTime = aBookingRoomUsedEN.CheckInActual;
                if (aBookingRoomUsedEN.Status == 8 || aBookingRoomUsedEN.Status == 7)
                {
                    dtpCheckOutActual.DateTime = aBookingRoomUsedEN.CheckOutActual;

                }
                else
                {
                    dtpCheckOutActual.DateTime = aBookingRoomUsedEN.CheckOutPlan;
                }
                txtPercentTax_Room.Text = Convert.ToString(aBookingRoomUsedEN.PercentTax);
                txtNumberDate.Text = Convert.ToString((aBookingRoomUsedEN.TimeInUse) / (24 * 60));
                txtAddTimeEnd.Text = aBookingRoomUsedEN.AddTimeEnd.ToString();
                txtAddTimeStart.Text = aBookingRoomUsedEN.AddTimeStart.ToString();
                if (chkCheckIn.Checked == true)
                {
                    txtAddTimeStart.Enabled = true;
                }
                else if (chkCheckIn.Checked == false)
                {
                    txtAddTimeStart.Enabled = false;
                }
                else if (chkCheckOut.Checked == true)
                {
                    txtAddTimeEnd.Enabled = true;
                }
                else if (chkCheckOut.Checked == false)
                {
                    txtAddTimeEnd.Enabled = false;
                }
                lblMoneyRoom.Text = String.Format("{0:0,0} (VND)", this.aNewPaymentEN.GetMoneyARoom(aBookingRoomUsedEN.ID));

                txtBookingRoomsCost.EditValue = aBookingRoomUsedEN.Cost ;
                // Danh sách khách
                dgvCustomers.DataSource = aBookingRoomUsedEN.ListCustomer;
                dgvCustomers.RefreshDataSource();
                // Danh sách dịch vụ
                dgvServices.DataSource = aNewPaymentEN.GetListServiceUsedInRoom(aBookingRoomUsedEN.ID);
                dgvServices.RefreshDataSource();

                lblTotalMoneyService.Text = String.Format("{0:0,0} (VND)", this.aNewPaymentEN.GetMoneyListServiceUsedInARoom(aBookingRoomUsedEN.ID));
            }


            //--------- Hội trường ----------------
            if (this.IDBookingH > 0)
            {
                //Thong tin nguoi dat
                lblNameCompany_BookingH.Text = this.aNewPaymentEN.NameCompany;
                lblNameCustomerGroup_BookingH.Text = this.aNewPaymentEN.NameCustomerGroup;
                lblNameCustomer_BookingH.Text = this.aNewPaymentEN.NameCustomer;
                txtAddressH.Text = this.aNewPaymentEN.AddressCompany;
                txtTaxNumberCodeH.Text = this.aNewPaymentEN.TaxNumberCodeCompany;
                // Trang thai, hinh thuc thanh toan
                lblTotalMoneyBookingHs.Text = String.Format("{0:0,0}", this.aNewPaymentEN.GetMoneyHalls());
                txtBookingHMoney.EditValue = this.aNewPaymentEN.BookingHMoney;
                lblTotalMoneyH.Text = String.Format("{0:0,0}", this.aNewPaymentEN.GetMoneyHalls() - this.aNewPaymentEN.BookingHMoney);
                // Thong tin gia tiền, đặt trước
                lueBookingH_PayMethod.Properties.DataSource = CORE.CONSTANTS.ListPayMethods;
                lueBookingH_PayMethod.Properties.DisplayMember = "Name";
                lueBookingH_PayMethod.Properties.ValueMember = "ID";
                lueBookingH_PayMethod.EditValue = CORE.CONSTANTS.SelectedPayMethod(Convert.ToInt32(this.aNewPaymentEN.PayMenthod)).ID;
                // Danh sách các hội trường
                dgvHalls.DataSource = this.aNewPaymentEN.aListBookingHallUsed;
                dgvHalls.RefreshDataSource();
                // Thông tin 1 hội trường


                if (this.aNewPaymentEN.aListBookingHallUsed.Where(a => a.ID == this.CurrentIDBookingHall).ToList().Count > 0)
                {
                    BookingHallUsedEN aBookingHallUsedEN = this.aNewPaymentEN.aListBookingHallUsed.Where(a => a.ID == this.CurrentIDBookingHall).ToList()[0];
                    lblSkuHalls.Text = Convert.ToString(aBookingHallUsedEN.HallSku);
                    decimal? cost = 0;
                    if (aBookingHallUsedEN.Cost == null || aBookingHallUsedEN.Cost == 0)
                    {
                        cost = aBookingHallUsedEN.CostRef_Halls;
                    }
                    else
                    {
                        cost = aBookingHallUsedEN.Cost;
                    }

                    lblDate.Text = String.Format("{0:dd/MM/yyyy}", aBookingHallUsedEN.Date);
                    lblLunarDate.Text = String.Format("{0:dd/MM/yyyy}", aBookingHallUsedEN.LunarDate);
                    lblStartTime.Text = String.Format(@"{0:hh\:mm}", aBookingHallUsedEN.StartTime);
                    lblEndTime.Text = String.Format(@"{0:hh\:mm}", aBookingHallUsedEN.EndTime);

                    lueMenus.Properties.DataSource = aBookingHallUsedEN.aListMenuEN;
                    lueMenus.Properties.DisplayMember = "Name";
                    lueMenus.Properties.ValueMember = "ID";
                    if (aBookingHallUsedEN.aListMenuEN.Count > 0)
                    {
                        lueMenus.EditValue = aBookingHallUsedEN.aListMenuEN[0].ID;
                        MenusEN aMenusEN = aBookingHallUsedEN.aListMenuEN[0];                       
                        
                        List<Foods> aListFoods = new List<Foods>();
                        FoodsBO aFoodsBO = new FoodsBO();
                        foreach (Foods item in aMenusEN.aListFoods)
                        {
                            Foods aFoods = aFoodsBO.Select_ByID(item.ID);
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
                            aListFoods.Add(aFoods);

                        }

                        dgvFoods.DataSource = aListFoods;
                        dgvFoods.RefreshDataSource();
                    }
                    txtPercentTax_Hall.EditValue = aBookingHallUsedEN.PercentTax;

                    lblMoneyHall.Text = String.Format("{0:0,0} (VND)", this.aNewPaymentEN.GetMoneyAHall(aBookingHallUsedEN.ID));

                    decimal? BookingHallsCost = aBookingHallUsedEN.Cost == null ? aBookingHallUsedEN.CostRef_Halls : aBookingHallUsedEN.Cost;

                    txtBookingHallsCost.EditValue = BookingHallsCost;
                    dgvBookingHallUseServices.DataSource = this.aNewPaymentEN.GetListServiceUsedInHall(aBookingHallUsedEN.ID);
                    dgvBookingHallUseServices.RefreshDataSource();
                    lblTotalMoneyServices_BookingH.Text = String.Format("{0:0,0} (VND)", this.aNewPaymentEN.GetMoneyListServiceUsedInAHall(aBookingHallUsedEN.ID));

                }              
            }
            else
            {
                btnPrepay.Enabled = false;
                btnPaymentHall.Enabled = false;
                btnPrintBookingH.Enabled = false;
                txtTaxNumberCodeH.Enabled = false;
            }
            // Thông tin tổng tiền
            lblTotalBookingRAndBookingHBeforeTax.Text = String.Format("{0:0,0} (VND)", this.aNewPaymentEN.GetTotalMoneyBeforeTax());
            lblTotalBookingRAndBookingHAfterTax.Text = String.Format("{0:0,0} (VND)", this.aNewPaymentEN.GetTotalMoney());
            if (this.aNewPaymentEN.BookingHMoney != null)
            {
                lblBookingMoney_BookingRAndBookingH.Text = String.Format("{0:0,0} (VND)", this.aNewPaymentEN.BookingHMoney + this.aNewPaymentEN.BookingRMoney);
                lblTotalBookingRAndBookingH.Text = String.Format("{0:0,0} (VND)", this.aNewPaymentEN.GetTotalMoney() - (this.aNewPaymentEN.BookingHMoney + this.aNewPaymentEN.BookingRMoney));
            }
            else
            {
                lblBookingMoney_BookingRAndBookingH.Text = String.Format("{0:0,0} (VND)",this.aNewPaymentEN.BookingRMoney);
                lblTotalBookingRAndBookingH.Text = String.Format("{0:0,0} (VND)", this.aNewPaymentEN.GetTotalMoney() - this.aNewPaymentEN.BookingRMoney);

            }
        }   

        private decimal ExtraMoney(string sku, int numberPepole, int customerType, string PriceType)
        {
            try
            {
                return CORE.CONSTANTS.SelectedExtraCost(sku, numberPepole, customerType, PriceType);
            }
            catch (Exception ex)
            {
                return 0;
                MessageBox.Show("frmTsk_PaymentStep2.ExtraMoney\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Các hàm thay đổi thông tin trong PaymentEN
        private void txtTaxNumberCodeR_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtTaxNumberCodeR.Text != this.aNewPaymentEN.TaxNumberCodeCompany)
                {
                    this.aNewPaymentEN.TaxNumberCodeCompany = txtTaxNumberCodeR.Text;
                    this.LoadData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.txtTaxNumberCodeR_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTaxNumberCodeH_Leave(object sender, EventArgs e)
        {
            try
            {

                if (txtTaxNumberCodeH.Text != this.aNewPaymentEN.TaxNumberCodeCompany)
                {
                    this.aNewPaymentEN.TaxNumberCodeCompany = txtTaxNumberCodeH.Text;
                    this.LoadData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.txtTaxNumberCodeH_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAddressR_Leave(object sender, EventArgs e)
        {
            try
            {

                if (txtAddressR.Text != this.aNewPaymentEN.AddressCompany)
                {
                    this.aNewPaymentEN.AddressCompany = txtAddressR.Text;
                    this.LoadData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.txtTaxNumberCodeR_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAddressH_Leave(object sender, EventArgs e)
        {
            try
            {

                if (txtAddressH.Text != this.aNewPaymentEN.AddressCompany)
                {
                    this.aNewPaymentEN.AddressCompany = txtAddressH.Text;
                    this.LoadData();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.txtTaxNumberCodeR_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBookingMoney_EditValueChanged(object sender, EventArgs e)
        {
            try
            {                
                decimal? bookingMoney;
                if (string.IsNullOrEmpty(txtBookingRMoney.Text) == true)
                {
                    bookingMoney = 0;
                }
                else
                {
                    bookingMoney = Convert.ToDecimal(txtBookingRMoney.Text);
                }
                this.aNewPaymentEN.BookingRMoney = bookingMoney;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.txtBookingMoney_EditValueChanged\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBookingH_BookingMoney_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal? bookingMoney;
                if (string.IsNullOrEmpty(txtBookingHMoney.Text) == true)
                {
                    bookingMoney = 0;
                }
                else
                {
                    bookingMoney = Convert.ToDecimal(txtBookingHMoney.Text);
                }
                this.aNewPaymentEN.BookingHMoney = bookingMoney;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.txtBookingH_BookingMoney_EditValueChanged\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Phần Phòng
        private void btnDetailRooms_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                cbbPriceType.Properties.ReadOnly = false;
                this.ExtraMoneyRoom = 0;
                //cbbPriceType.SelectedIndex = 0;
                this.CurrentIDBookingRoom = Convert.ToInt32(viewRooms.GetFocusedRowCellValue("ID"));
                this.CodeRoom = viewRooms.GetFocusedRowCellValue("CodeRoom").ToString();

              this.LoadData();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnDetailRooms_ButtonClick\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                int IDBookingRoomService = Convert.ToInt32(viewServices.GetFocusedRowCellValue("IDBookingService"));

                DevExpress.XtraEditors.TextEdit txtQuality = (DevExpress.XtraEditors.TextEdit)sender;

                string input = txtQuality.Text;

                double Quantity;
                if (string.IsNullOrEmpty(input) == true)
                {
                    Quantity = 0;
                }
                else
                {
                    Quantity = double.Parse(input);
                }

                this.aNewPaymentEN.ChangeQuantityServiceUsedInRoom(this.CurrentIDBookingRoom, IDBookingRoomService, Quantity);
                this.LoadData();

            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.txtQuantity_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private void txtPercentTaxService_Leave(object sender, EventArgs e)
        {
            try
            {
                int IDBookingRoomSertvice = Convert.ToInt32(viewServices.GetFocusedRowCellValue("IDBookingService"));

                DevExpress.XtraEditors.TextEdit txtPercentTaxService = (DevExpress.XtraEditors.TextEdit)sender;

                string input = txtPercentTaxService.Text;


                double PercentTaxService;
                if (string.IsNullOrEmpty(input) == true)
                {
                    PercentTaxService = 0;
                }
                else
                {
                    PercentTaxService = double.Parse(input);
                }

                aNewPaymentEN.ChangeTaxServiceInRoom(this.CurrentIDBookingRoom, IDBookingRoomSertvice, PercentTaxService);
                this.LoadData();               
            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.txtPercentTaxService_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void txtServiceCost_Leave(object sender, EventArgs e)
        {
            try
            {
                int IDBookingRoomSertvice = Convert.ToInt32(viewServices.GetFocusedRowCellValue("IDBookingService"));

                DevExpress.XtraEditors.TextEdit txtCost = (DevExpress.XtraEditors.TextEdit)sender;

                string input = txtCost.Text;

                decimal Cost;
                if (string.IsNullOrEmpty(input) == true)
                {
                    Cost = 0;
                }
                else
                {
                    Cost = Convert.ToDecimal(txtCost.Text);
                }
                aNewPaymentEN.ChangeCostServiceUsedInRoom(this.CurrentIDBookingRoom, IDBookingRoomSertvice, Cost);
                this.LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.txtServiceCost_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBookingRoomsCost_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                string input = txtBookingRoomsCost.Text;
                decimal cost;
                if (string.IsNullOrEmpty(input) == true)
                {
                    cost = 0;
                }
                else
                {
                    cost = Convert.ToDecimal(input);
                }

                this.aNewPaymentEN.ChangeCostRoom(this.CurrentIDBookingRoom, cost);
                this.LoadData();             

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.txtBookingRoomsCost_EditValueChanged\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNumberDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string input = txtNumberDate.Text;
                decimal NumberDate;
                if (string.IsNullOrEmpty(input) == true)
                {
                    NumberDate = 0;
                }
                else
                {
                    NumberDate = decimal.Parse(input);
                }
                this.aNewPaymentEN.ChangeTimeInUsed(this.CurrentIDBookingRoom, NumberDate*24*60);
                this.LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.txtNumberDate_EditValueChanged\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void txtPercentTax_Room_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string input = txtPercentTax_Room.Text;
                double PercentTax;
                if (string.IsNullOrEmpty(input) == true)
                {
                    PercentTax = 0;
                }
                else
                {
                    PercentTax = double.Parse(input);
                }
                this.aNewPaymentEN.ChangePercentTaxRoom(this.CurrentIDBookingRoom, PercentTax);
                this.LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.txtPercentTax_Room_EditValueChanged\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpCheckOutActual_Leave(object sender, EventArgs e)
        {
            try
            {
                if (dtpCheckInActual.DateTime != null && dtpCheckOutActual.DateTime != null)
                {
                    if (dtpCheckInActual.DateTime < dtpCheckOutActual.DateTime)
                    {
                       
                        this.aNewPaymentEN.ChangeCheckOutActual(this.CurrentIDBookingRoom, DateTime.ParseExact(dtpCheckOutActual.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));                                        
                    }
                    else
                    {
                        dtpCheckOutActual.Focus();
                        MessageBox.Show("Vui lòng nhập ngày giờ CheckIn phải nhỏ hơn ngày giờ CheckOut", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.dtpCheckOutActual_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCaculateTimeUsed_Click(object sender, EventArgs e)
        {
            ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
            if (this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList().Count > 0)
            {
                if (chkCheckIn.Checked == true && chkCheckOut.Checked == true)
                {
                    this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].Type = 3;
                    txtAddTimeStart.Enabled = true;
                    txtAddTimeEnd.Enabled = true;
                }
                else if (chkCheckIn.Checked == false && chkCheckOut.Checked == false)
                {
                    this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].Type = 0;
                    txtAddTimeStart.Enabled = false;
                    txtAddTimeEnd.Enabled = false;
                }
                else if (chkCheckIn.Checked == false && chkCheckOut.Checked == true)
                {
                    this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].Type = 1;
                    txtAddTimeStart.Enabled = false;
                    txtAddTimeEnd.Enabled = true;
                }
                else if (chkCheckIn.Checked == true && chkCheckOut.Checked == false)
                {
                    this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].Type = 2;
                    txtAddTimeStart.Enabled = true;
                    txtAddTimeEnd.Enabled = false;
                }
                txtAddTimeStart.Text = aReceptionTaskBO.GetAddTimeStart(this.aNewPaymentEN, this.CurrentIDBookingRoom, dtpCheckInActual.DateTime).ToString();
                txtAddTimeEnd.Text = aReceptionTaskBO.GetAddTimeEnd(this.aNewPaymentEN, this.CurrentIDBookingRoom, dtpCheckOutActual.DateTime).ToString();
                  txtNumberDate.Text = (aReceptionTaskBO.GetTimeInUsed(this.CurrentIDBookingRoom,dtpCheckInActual.DateTime,dtpCheckOutActual.DateTime)).ToString();
                
                this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].CheckInActual = dtpCheckInActual.DateTime;
                this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].AddTimeStart = Convert.ToDecimal(txtAddTimeStart.Text);
                this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].AddTimeStart = Convert.ToDecimal(txtAddTimeStart.Text);

                BookingRoomUsedEN aTemp = this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0];
                if (aTemp.Status != 7 || aTemp.Status != 8)
                {
                    this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].CheckOutPlan = dtpCheckOutActual.DateTime;
                }
                else 
                {
                   this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].CheckOutActual = dtpCheckOutActual.DateTime;
               }
               // this.LoadData();
            }
        }

        private void cbbPriceType_EditValueChanged(object sender, EventArgs e)
        {
            CustomersBO aCustomersBO = new CustomersBO();
            BookingRsBO aBookingRsBO = new BookingRsBO();
            ExtraCostBO aExtraCostBO = new ExtraCostBO();
            RoomsBO aRoomsBO = new RoomsBO();
            
            
            List<Customers> aListCustomers = aCustomersBO.SelectListCustomer_ByIDBookingRoom(this.CurrentIDBookingRoom);
            int CustomerType = aBookingRsBO.Select_ByID(this.IDBookingR).CustomerType.GetValueOrDefault();
            decimal? CostRoom = aRoomsBO.Select_ByIDBookingRoom(this.CurrentIDBookingRoom).CostRef;
            this.ExtraMoneyRoom = Convert.ToDecimal(aExtraCostBO.Select_BySku_ByPriceType_ByNumberPeople(lblSkuRooms.Text,cbbPriceType.Text,aListCustomers.Count).ExtraValue);
                
            txtBookingRoomsCost.Text = Convert.ToString(CostRoom + this.ExtraMoneyRoom);
            
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            try
            {
                frmTsk_UseServices afrmTsk_UseServices = new frmTsk_UseServices(this, this.CodeRoom, this.IDBookingR, this.CurrentIDBookingRoom,this.aNewPaymentEN);
                afrmTsk_UseServices.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnAddService_Click\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phần Hội trường

        private void btnEditBookingHall_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                this.CurrentIDBookingHall = Convert.ToInt32(viewHalls.GetFocusedRowCellValue("ID"));
                this.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnEditBookingHall_ButtonClick\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBookingHallsCost_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string input = txtBookingHallsCost.Text;
                decimal cost;
                if (string.IsNullOrEmpty(input) == true)
                {
                    cost = 0;
                }
                else
                {
                    cost = Convert.ToDecimal(input);
                }

                this.aNewPaymentEN.ChangeCostHall(this.CurrentIDBookingHall, cost);
                this.LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.txtBookingHallsCost_EditValueChanged\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPercentTax_Hall_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string input = txtPercentTax_Hall.Text;
                double PercentTax;
                if (string.IsNullOrEmpty(input) == true)
                {
                    PercentTax = 0;
                }
                else
                {
                    PercentTax = double.Parse(input);
                }
                this.aNewPaymentEN.ChangePercentTaxHall(this.CurrentIDBookingHall, PercentTax);
                this.LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.txtPercentTax_Hall_EditValueChanged\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //public void AddServiceForBookingHall(int IDBookingHall)
        //{
        //    try
        //    {
        //        ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
        //        List<ServicesHallsEN> aListServicesHallsEN = new List<ServicesHallsEN>();
        //        aListServicesHallsEN = aReceptionTaskBO.GetListServicesHallsEN_ByIDBookingHall(IDBookingHall);
        //        foreach (InfoDetailPaymentHallsEN aInfoDetailPaymentHallsEN in aPaymentHallsEN.aListInfoDetailPaymentHallsEN)
        //        {
        //            if (aInfoDetailPaymentHallsEN.aBookingHalls.ID == IDBookingHall)
        //            {
        //                aInfoDetailPaymentHallsEN.aListServicesHallsEN.Clear();
        //                foreach (ServicesHallsEN aServicesHallsEN in aListServicesHallsEN)
        //                {
        //                    aInfoDetailPaymentHallsEN.aListServicesHallsEN.Add(aServicesHallsEN);
        //                }
        //            }
        //        }
        //        this.LoadDataServiceForBookingHall(this.CurrentIDBookingHall);
        //        lblTotalMoneyServices_BookingH.Text = String.Format("{0:0,0} (VND)", aPaymentHallsEN.GetTotalMoneyServiceHallBehindTax_ByIDBookingHall(this.cuIDBookingHall));
        //        this.LoadListHall();
        //        lblTotalMoneyBookingHs.Text = String.Format("{0:0,0}", Convert.ToDecimal(aPaymentHallsEN.GetTotalMoneyBookingHBehindTax()));
        //        txtBookingHMoney.EditValue = this.aPaymentHallsEN.GetBookingMoney();
        //        lblTotalMoneyH.Text = String.Format("{0:0,0}", (Convert.ToDecimal(aPaymentHallsEN.GetTotalMoneyBookingHBehindTax()) - Convert.ToDecimal(this.aPaymentHallsEN.GetBookingMoney())));

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("frmTsk_PaymentStep2.AddServiceForBookingHall\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        
        private void btnServicesQuantityForHalls_Leave(object sender, EventArgs e)
        {
            try
            {
                int IDBookingHallService = Convert.ToInt32(viewBookingHallUseServices.GetFocusedRowCellValue("IDBookingService"));

                DevExpress.XtraEditors.TextEdit txtQuality = (DevExpress.XtraEditors.TextEdit)sender;

                string input = txtQuality.Text;

                double Quantity;
                if (string.IsNullOrEmpty(input) == true)
                {
                    Quantity = 0;
                }
                else
                {
                    Quantity = double.Parse(input);
                }
                this.aNewPaymentEN.ChangeQuantityServiceUsedInHall(this.CurrentIDBookingHall, IDBookingHallService, Quantity);
                this.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnServicesQuantityForHalls_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        private void btnServicesPercentTaxForHalls_Leave(object sender, EventArgs e)
        {
            try
            {
                int IDBookingHallService = Convert.ToInt32(viewBookingHallUseServices.GetFocusedRowCellValue("IDBookingHallService"));

                DevExpress.XtraEditors.TextEdit txtPercentTaxService = (DevExpress.XtraEditors.TextEdit)sender;

                string input = txtPercentTaxService.Text;

                double PercentTaxService;
                if (string.IsNullOrEmpty(input) == true)
                {
                    PercentTaxService = 0;
                }
                else
                {
                    PercentTaxService = double.Parse(input);
                }
                this.aNewPaymentEN.ChangeTaxServiceInHall(this.CurrentIDBookingHall, IDBookingHallService, PercentTaxService);
                this.LoadData();

             }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.btnServicesPercentTaxForHalls_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnServicesCostForHalls_Leave(object sender, EventArgs e)
        {
            try
            {
                int IDBookingHallService = Convert.ToInt32(viewBookingHallUseServices.GetFocusedRowCellValue("IDBookingHallService"));

                DevExpress.XtraEditors.TextEdit txtCost = (DevExpress.XtraEditors.TextEdit)sender;

                string input = txtCost.Text;

                decimal Cost;
                if (string.IsNullOrEmpty(input) == true)
                {
                    Cost = 0;
                }
                else
                {
                    Cost = Convert.ToDecimal(txtCost.Text);
                }
                this.aNewPaymentEN.ChangeCostServiceUsedInHall(this.CurrentIDBookingHall, IDBookingHallService, Cost);
                this.LoadData();  
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnServicesCostForHalls_Leave\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddServicesForHalls_Click(object sender, EventArgs e)
        {
            try
            {
                frmIns_BookingHalls_Services afrmIns_BookingHalls_Services = new frmIns_BookingHalls_Services(this, this.CurrentIDBookingHall,this.aNewPaymentEN);
                afrmIns_BookingHalls_Services.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnPrintPaymentTotal_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Chức năng thanh toán phòng
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (aNewPaymentEN.Status_BookingR == 8 || aNewPaymentEN.Status_BookingR == 7)
                {
                    frmRpt_Payment_BookingRs afrmRpt_Payment_BookingRs = new frmRpt_Payment_BookingRs(this.aNewPaymentEN);
                    ReportPrintTool tool = new ReportPrintTool(afrmRpt_Payment_BookingRs);
                    tool.ShowPreview();
                }
                else
                {
                    frmRpt_Payment_BookingRsUnPay afrmRpt_Payment_BookingRs = new frmRpt_Payment_BookingRsUnPay(this.aNewPaymentEN);
                    ReportPrintTool tool = new ReportPrintTool(afrmRpt_Payment_BookingRs);
                    tool.ShowPreview();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnPrint_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // In ra phiếu thanh toán của phòng

        private void btnDownPayment_Click(object sender, EventArgs e) // Tạm thanh toán ( Update lại BookingRMoney)
        {
            try
            {
                BookingRsBO aBookingRsBO = new BookingRsBO();
                BookingRs aBookingRs = aBookingRsBO.Select_ByID(this.IDBookingR);
                aBookingRs.BookingMoney = this.aNewPaymentEN.BookingRMoney;
                aBookingRs.StatusPay = 2;// Tạm ứng
                int count = aBookingRsBO.Update(aBookingRs);
                if (this.afrmTsk_Payment_Step1 != null)
                {
                    this.afrmTsk_Payment_Step1.LoadDataListUnPayBookingR();
                }
                this.LoadData();
                MessageBox.Show("Thực hiện thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnDownPayment_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Thanh toán tất cả các phòng. Tiếp tục?", "Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.aNewPaymentEN.PaymentRoom();

                    if (this.afrmTsk_Payment_Step1 != null)
                    {
                        this.afrmTsk_Payment_Step1.LoadDataListUnPayBookingR();
                        if (this.afrmTsk_Payment_Step1.afrmMain != null)
                        {
                            this.afrmTsk_Payment_Step1.afrmMain.ReloadData();
                        }
                    }
                    //this.aPaymentEN = new PaymentEN();
                    //this.InitDataBookingR(this.IDBookingR, this.aPaymentEN);
                    MessageBox.Show("Thanh toán tiền phòng thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.btnPayment_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Chức năng thanh toán tiệc
        private void btnPrepay_Click(object sender, EventArgs e) // Tạm thanh toán ( Update lại BookingHMoney)
        {
            try
            {
                BookingHsBO aBookingHsBO = new BookingHsBO();
                BookingHs aBookingHs = aBookingHsBO.Select_ByID(this.IDBookingH);
                if (aBookingHs != null)
                {
                    aBookingHs.BookingMoney = this.aNewPaymentEN.BookingHMoney;
                    aBookingHs.PayMenthod = 2; // tam ung
                    aBookingHsBO.Update(aBookingHs);
                    MessageBox.Show("Thực hiện thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnPrepay_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintBookingH_Click(object sender, EventArgs e) // In phiếu thanh toán của tiệc
        {
            try
            {
                frmRpt_PaymentBookingHs afrmRpt_PaymentBookingHs = new frmRpt_PaymentBookingHs(this.aNewPaymentEN);
                ReportPrintTool tool = new ReportPrintTool(afrmRpt_PaymentBookingHs);
                tool.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnPrintBookingH_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPaymentHall_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Thanh toán tất cả các hội trường. Tiếp tục?", "Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.aNewPaymentEN.PaymentHall();
                    MessageBox.Show("Thanh toán tiền hội trường thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("frmTsk_PaymentStep2.btnPaymentHall_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Thanh toán tổng
        private void btnPrintPaymentTotal_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.aNewPaymentEN.aListBookingRoomUsed.Count > 0 && this.aNewPaymentEN.aListBookingHallUsed.Count > 0)
                {
                    frmRpt_Payment_BookingRsAndBookingHs afrmRpt_Payment_BookingRs = new frmRpt_Payment_BookingRsAndBookingHs( this.aNewPaymentEN);
                    ReportPrintTool tool = new ReportPrintTool(afrmRpt_Payment_BookingRs);
                    tool.ShowPreview();
                }
                else
                {
                    if (this.aNewPaymentEN.aListBookingRoomUsed.Count > 0)
                    {
                        frmRpt_Payment_BookingRs afrmRpt_Payment_BookingRs = new frmRpt_Payment_BookingRs(this.aNewPaymentEN);
                        ReportPrintTool tool = new ReportPrintTool(afrmRpt_Payment_BookingRs);
                        tool.ShowPreview();
                    }
                    else if (this.aNewPaymentEN.aListBookingHallUsed.Count > 0)
                    {
                        frmRpt_PaymentBookingHs afrmRpt_PaymentBookingHs = new frmRpt_PaymentBookingHs(this.aNewPaymentEN);
                        ReportPrintTool tool = new ReportPrintTool(afrmRpt_PaymentBookingHs);
                        tool.ShowPreview();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnPrintPaymentTotal_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPaymentTotal_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Thanh toán tất cả các phòng + hội trường. Tiếp tục?", "Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.aNewPaymentEN.PaymentTotal();
                    if (this.afrmTsk_Payment_Step1 != null)
                    {
                        this.afrmTsk_Payment_Step1.LoadDataListUnPayBookingR();
                        if (this.afrmTsk_Payment_Step1.afrmMain != null)
                        {
                            this.afrmTsk_Payment_Step1.afrmMain.ReloadData();
                        }
                    }
                    MessageBox.Show("Thanh toán tiền thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnPaymentTotal_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Tách phiếu thu

        private void btnSplitBill_Click(object sender, EventArgs e)
        {
            try
            {
                this.aPaymentEN.PayMenthod = Convert.ToInt32(lueBookingR_Paymethod.EditValue);
                frmTsk_SplitBill_Step1 afrmTsk_SplitBill_Step1 = new frmTsk_SplitBill_Step1(this, this.aNewPaymentEN);
                afrmTsk_SplitBill_Step1.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_PaymentStep2.btnSplitBill_Click\n" + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void lueMenus_EditValueChanged(object sender, EventArgs e)
        {
            BookingHallUsedEN aBookingHallUsedEN = this.aNewPaymentEN.aListBookingHallUsed.Where(a => a.ID == this.CurrentIDBookingHall).ToList()[0];
            MenusEN aMenusEN = aBookingHallUsedEN.aListMenuEN.Where(a => a.ID == Convert.ToInt32(lueMenus.EditValue)).ToList()[0];
            List<Foods> aListFoods = new List<Foods>();
            FoodsBO aFoodsBO = new FoodsBO();
            foreach (Foods item in aMenusEN.aListFoods)
            {
                Foods aFoods = aFoodsBO.Select_ByID(item.ID);
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
                aListFoods.Add(aFoods);

            }

            dgvFoods.DataSource = aListFoods;
            dgvFoods.RefreshDataSource();
        }

        private void dtpInvoiceDate_EditValueChanged(object sender, EventArgs e)
        {
            DateTime aTemp = dtpInvoiceDate.DateTime;
            dtpInvoiceDateH.DateTime = aTemp;
            dtpAcceptDate.DateTime = aTemp;
            this.aNewPaymentEN.ChangeInvoiceDate(aTemp);
         
        }

        private void txtInvoiceNumber_EditValueChanged(object sender, EventArgs e)
        {
            string aTemp = txtInvoiceNumber.Text;
            txtInvoiceNumberH.Text = aTemp;
            this.aNewPaymentEN.ChangeInvoiceNumber(aTemp);
            
        }

        private void dtpAcceptDate_EditValueChanged(object sender, EventArgs e)
        {
            DateTime aTemp = dtpAcceptDate.DateTime;           
           
            dtpAcceptDateH.DateTime = aTemp;
            this.aNewPaymentEN.ChangeAcceptDate(aTemp);
          
        }

        private void chkCheckIn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckIn.Checked == true && this.CurrentIDBookingRoom != 0)
            {
                txtAddTimeStart.Enabled = true;
                if (txtAddTimeStart.EditValue != null)
                {
                    this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].AddTimeStart = Convert.ToDecimal(txtAddTimeStart.Text);
                    this.aNewPaymentEN.ChangeTypeBookingRoom(this.CurrentIDBookingRoom, chkCheckIn.Checked, chkCheckOut.Checked);
                    this.LoadData();
                }
            }
            else if (chkCheckIn.Checked == false && this.CurrentIDBookingRoom != 0)
            {
                txtAddTimeStart.Enabled = false;
                this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].AddTimeStart = 0;
                this.aNewPaymentEN.ChangeTypeBookingRoom(this.CurrentIDBookingRoom, chkCheckIn.Checked, chkCheckOut.Checked);
                this.LoadData();
            }      
        }

        private void chkCheckOut_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckOut.Checked == true && this.CurrentIDBookingRoom != 0)
            {
                txtAddTimeEnd.Enabled = true;
                if (txtAddTimeEnd.EditValue != null)
                {
                    this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].AddTimeEnd = Convert.ToDecimal(txtAddTimeEnd.Text);
                    this.aNewPaymentEN.ChangeTypeBookingRoom(this.CurrentIDBookingRoom, chkCheckIn.Checked, chkCheckOut.Checked);
                    this.LoadData();
                }
            }
            else if (chkCheckOut.Checked == false && this.CurrentIDBookingRoom != 0)
            {
                txtAddTimeEnd.Enabled = false;
                this.aNewPaymentEN.aListBookingRoomUsed.Where(a => a.ID == this.CurrentIDBookingRoom).ToList()[0].AddTimeEnd = 0;
                this.aNewPaymentEN.ChangeTypeBookingRoom(this.CurrentIDBookingRoom, chkCheckIn.Checked, chkCheckOut.Checked);
                this.LoadData();
            }      
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.aNewPaymentEN.Save();
            MessageBox.Show("Lưu thông tin hóa đơn thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void LockForm()
        {
            txtAddressH.Properties.ReadOnly = true;
            txtAddressR.Properties.ReadOnly = true;
            txtAddTimeEnd.Properties.ReadOnly = true;
            txtAddTimeStart.Properties.ReadOnly = true;
            txtBookingHallsCost.Properties.ReadOnly = true;
            txtBookingHMoney.Properties.ReadOnly = true;
            txtBookingRMoney.Properties.ReadOnly = true;
            txtBookingRoomsCost.Properties.ReadOnly = true;
            txtInvoiceNumber.Properties.ReadOnly = true;
            txtInvoiceNumberH.Properties.ReadOnly = true;
            txtNumberDate.Properties.ReadOnly = true;
            txtPercentTax_Hall.Properties.ReadOnly = true;
            txtPercentTax_Room.Properties.ReadOnly = true;
            txtPercentTaxService.ReadOnly = true;
            txtQuantity.ReadOnly = true;
            txtServiceCost.ReadOnly = true;
            txtTaxNumberCodeH.Properties.ReadOnly = true;
            txtTaxNumberCodeR.Properties.ReadOnly = true;
            cbbPriceType.Properties.ReadOnly = true;

            dtpAcceptDate.Properties.ReadOnly = true;
            dtpAcceptDateH.Properties.ReadOnly = true;
            dtpCheckInActual.Properties.ReadOnly = true;
            dtpCheckOutActual.Properties.ReadOnly = true;
            dtpInvoiceDate.Properties.ReadOnly = true;
            dtpInvoiceDateH.Properties.ReadOnly = true;

            chkCheckIn.Properties.ReadOnly = true;
            chkCheckOut.Properties.ReadOnly = true;

            lueBookingH_PayMethod.Properties.ReadOnly = true;
            lueBookingR_Paymethod.Properties.ReadOnly = true;
            lueMenus.Properties.ReadOnly = true;

            btnAddService.Enabled = false;
            btnAddServicesForHalls.Enabled = false;
            btnCaculateTimeUsed.Enabled = false;
            btnDownPayment.Enabled = false;
            btnPayment.Enabled = false;
            btnPaymentHall.Enabled = false;




        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
         
        }

        private void simpleButton1_Click_2(object sender, EventArgs e)
        {
            LockForm();
        }

    }
}