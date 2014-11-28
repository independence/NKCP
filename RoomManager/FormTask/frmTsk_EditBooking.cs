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
using DevExpress.XtraGrid.Views.Grid;
using Entity;
using DevExpress.XtraGrid.Columns;
using CORESYSTEM;

namespace RoomManager
{
    public partial class frmTsk_EditBooking : DevExpress.XtraEditors.XtraForm
    {
        private frmMain afrmMain = null;
        string SkuCurrentRoom = string.Empty;
        int IDBookingRs = 0;
        int IDBookingRooms = 0;
        int IDCustomerGroup = 0;

        List<Customers> aListAvailableCustomers = new List<Customers>();
        List<CustomerInfoEN> aListCustomersInRoom = new List<CustomerInfoEN>();
        List<RoomsEN> aListRooms = new List<RoomsEN>();

        public frmTsk_EditBooking()
        {
            InitializeComponent();
        }
        public frmTsk_EditBooking(frmMain afrmMain, int IDBookingRs, int IDBookingRooms)
        {
            InitializeComponent();
            this.afrmMain = afrmMain;
            this.IDBookingRs = IDBookingRs;
            this.IDBookingRooms = IDBookingRooms;
        }

        //Hiennv
        private void frmTsk_EditBooking_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadData();
                this.LoadListRooms();
                this.ReloadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.frmTsk_EditBooking_Load\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiennv
        private void LoadListRooms()
        {
            try
            {
                
                RoomsEN aRoomsEN;
                RoomsBO aRoomsBO = new RoomsBO();
                BookingRoomsBO aBookingRoomsBO = new BookingRoomsBO();
                List<BookingRooms> aListBookingRooms = aBookingRoomsBO.Select_ByIDBookingRs(this.IDBookingRs).Where(r=>r.Status < 7).ToList();
                foreach (BookingRooms items1 in aListBookingRooms)
                {
                    aRoomsEN = new RoomsEN();
                    aRoomsEN.IDBookingRooms = items1.ID;
                    aRoomsEN.Code = items1.CodeRoom;
                    Rooms aRooms = aRoomsBO.Select_ByCodeRoom(items1.CodeRoom, 1);//IDLang= 1 : Ngon ngu tieng viet
                    if (aRooms != null)
                    {
                        aRoomsEN.Sku = aRooms.Sku;
                    }
                    aRoomsEN.Cost = items1.CostRef_Rooms;
                    this.aListRooms.Add(aRoomsEN);
                }

                BookingRooms aBookingRooms = aBookingRoomsBO.Select_ByID(this.IDBookingRooms);
                if (aBookingRooms != null)
                {
                    ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
                    List<Rooms> aListTemp = aReceptionTaskBO.GetListAvailableRooms(aBookingRooms.CheckInActual, aBookingRooms.CheckOutPlan, 1); //IDLang = 1: Ngon ngu tieng viet
                    int count = -1;
                    foreach (Rooms items2 in aListTemp)
                    {
                        aRoomsEN = new RoomsEN();
                        aRoomsEN.IDBookingRooms = count;
                        aRoomsEN.Code = items2.Code;
                        aRoomsEN.Sku = items2.Sku;
                        aRoomsEN.Cost = items2.CostRef;
                        this.aListRooms.Add(aRoomsEN);
                        count--;
                    }
                }
                lueRooms.Properties.DataSource = this.aListRooms;
                lueRooms.Properties.DisplayMember = "Sku";
                lueRooms.Properties.ValueMember = "IDBookingRooms";
                lueRooms.EditValue = this.IDBookingRooms;
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.LoadListRooms\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Hiennv
        public void ReloadCustomers()
        {
            try
            {
                
                this.aListAvailableCustomers.Clear();
                CustomersBO aCustomersBO = new CustomersBO();
                this.aListAvailableCustomers = aCustomersBO.Select_All();
                dgvAvailableCustomers.DataSource = this.aListAvailableCustomers;
                dgvAvailableCustomers.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.ReloadCustomers\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Hiennv
        private void LoadListAvailableCustomers()
        {
            try
            {
                dgvAvailableCustomers.DataSource = this.aListAvailableCustomers;
                dgvAvailableCustomers.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.LoadListAvailableCustomers\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Hiennv
        private void GetListCustomers_ByIDBookingRoom(int IDBookingRoom)
        {
            try
            {
                dgvSelectCustomers.DataSource = this.aListCustomersInRoom.Where(r => r.IDBookingRoom == IDBookingRoom).ToList();
                dgvSelectCustomers.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.GetListCustomers_ByIDBookingRoom\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadData()
        {
            try
            {
                CustomersBO aCustomersBO = new CustomersBO();
                BookingRoomsBO aBookingRoomsBO = new BookingRoomsBO();
                BookingRooms aBookingRooms = new BookingRooms();
                aBookingRooms = aBookingRoomsBO.Select_ByID(this.IDBookingRooms);
                if (aBookingRooms != null)
                {
                    lblCheckIn.Text = aBookingRooms.CheckInActual.ToString("dd/MM/yyyy HH:mm");
                    dtpCheckOut.DateTime = aBookingRooms.CheckOutPlan;
                }
                BookingRsBO aBookingRsBO = new BookingRsBO();
                BookingRs aBookingRs = new BookingRs();
                aBookingRs = aBookingRsBO.Select_ByID(this.IDBookingRs);
                if (aBookingRs != null)
                {
                    lblCustomerType.Text = CORE.CONSTANTS.SelectedCustomerType(Convert.ToInt32(aBookingRs.CustomerType)).Name;

                    CompaniesBO aCompaniesBO = new CompaniesBO();
                    lblCompany.Text = aCompaniesBO.Select_ByIDBookingRoom(this.IDBookingRooms).Name;

                    lblCustomer.Text = aCustomersBO.Select_ByID(aBookingRs.IDCustomer).Name;

                    CustomerGroupsBO aCustomerGroupsBO = new CustomerGroupsBO();
                    lblGroup.Text = aCustomerGroupsBO.Select_ByID(aBookingRs.IDCustomerGroup).Name;

                    lblTel.Text = aCustomersBO.Select_ByID(aBookingRs.IDCustomer).Tel;

                    this.IDCustomerGroup = aBookingRs.IDCustomerGroup;
                }

                //Hiennv

                CustomerInfoEN aCustomerInfoEN;
                List<BookingRooms> aListBookingRooms = aBookingRoomsBO.Select_ByIDBookingRs(this.IDBookingRs);
                foreach (BookingRooms items in aListBookingRooms)
                {
                    List<Customers> aListCustomers = aCustomersBO.SelectListCustomer_ByIDBookingRoom(items.ID);
                    foreach (Customers items1 in aListCustomers)
                    {
                        aCustomerInfoEN = new CustomerInfoEN();
                        aCustomerInfoEN.IDBookingRoom = items.ID;
                        aCustomerInfoEN.ID = items1.ID;
                        aCustomerInfoEN.Name = items1.Name;
                        aCustomerInfoEN.Identifier1 = items1.Identifier1;
                        aCustomerInfoEN.Birthday = items1.Birthday;
                        this.aListCustomersInRoom.Add(aCustomerInfoEN);
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.ReloadData\n" + ex.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            frmIns_Customers afrmIns_Customers = new frmIns_Customers(this);
            afrmIns_Customers.ShowDialog();
        }

        //Hiennv
        private void btnRemoveSelectCustomers_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                DateTime? dateTime = null;
                Customers aCustomers = new Customers();
                aCustomers.ID = Convert.ToInt32(viewSelectCustomers.GetFocusedRowCellValue("ID"));
                aCustomers.Name = String.IsNullOrEmpty(Convert.ToString(viewSelectCustomers.GetFocusedRowCellValue("Name"))) == true ? String.Empty : Convert.ToString(viewSelectCustomers.GetFocusedRowCellValue("Name"));
                aCustomers.Identifier1 = String.IsNullOrEmpty(Convert.ToString(viewSelectCustomers.GetFocusedRowCellValue("Identifier1"))) == true ? String.Empty : Convert.ToString(viewSelectCustomers.GetFocusedRowCellValue("Identifier1"));
                aCustomers.Birthday = String.IsNullOrEmpty(Convert.ToString(viewSelectCustomers.GetFocusedRowCellValue("Birthday"))) == true ? dateTime : Convert.ToDateTime(viewSelectCustomers.GetFocusedRowCellValue("Birthday"));

                this.aListAvailableCustomers.Insert(0, aCustomers);
                this.LoadListAvailableCustomers();
                List<CustomerInfoEN> aListTemp1 = this.aListCustomersInRoom.Where(r => r.IDBookingRoom == Convert.ToInt32(lueRooms.EditValue) && r.ID == Convert.ToInt32(viewSelectCustomers.GetFocusedRowCellValue("ID"))).ToList();
                if (aListTemp1.Count > 0)
                {
                    this.aListCustomersInRoom.Remove(aListTemp1[0]);
                    this.GetListCustomers_ByIDBookingRoom(Convert.ToInt32(lueRooms.EditValue));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmAddListCustomerToCustomerGroups.btnRemoveSelectCustomers_ButtonClick\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Hiennv
        private void btnSelectCustomers_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (lueRooms.EditValue != null)
                {
                    List<CustomerInfoEN> aListTemp1 = this.aListCustomersInRoom.Where(r => r.IDBookingRoom == Convert.ToInt32(lueRooms.EditValue) && r.ID == Convert.ToInt32(viewAvailableCustomers.GetFocusedRowCellValue("ID"))).ToList();
                    if (aListTemp1.Count > 0)
                    {
                        MessageBox.Show("Người này đã có trong phòng vui lòng chọn người khác .", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        DateTime? dateTime = null;
                        CustomerInfoEN aCustomerInfoEN = new CustomerInfoEN();
                        aCustomerInfoEN.IDBookingRoom = Convert.ToInt32(lueRooms.EditValue);
                        aCustomerInfoEN.ID = Convert.ToInt32(viewAvailableCustomers.GetFocusedRowCellValue("ID"));
                        aCustomerInfoEN.Name = String.IsNullOrEmpty(Convert.ToString(viewAvailableCustomers.GetFocusedRowCellValue("Name"))) == true ? String.Empty : Convert.ToString(viewAvailableCustomers.GetFocusedRowCellValue("Name"));
                        aCustomerInfoEN.Identifier1 = String.IsNullOrEmpty(Convert.ToString(viewAvailableCustomers.GetFocusedRowCellValue("Identifier1"))) == true ? String.Empty : Convert.ToString(viewAvailableCustomers.GetFocusedRowCellValue("Identifier1"));
                        aCustomerInfoEN.Birthday = String.IsNullOrEmpty(Convert.ToString(viewAvailableCustomers.GetFocusedRowCellValue("Birthday"))) == true ? dateTime : Convert.ToDateTime(viewAvailableCustomers.GetFocusedRowCellValue("Birthday"));

                        this.aListCustomersInRoom.Insert(0, aCustomerInfoEN);
                        this.GetListCustomers_ByIDBookingRoom(Convert.ToInt32(lueRooms.EditValue));
                    }
                    List<Customers> aListTemp2 = this.aListAvailableCustomers.Where(c => c.ID == Convert.ToInt32(viewAvailableCustomers.GetFocusedRowCellValue("ID"))).ToList();
                    if (aListTemp2.Count > 0)
                    {
                        this.aListAvailableCustomers.Remove(aListTemp2[0]);
                        this.LoadListAvailableCustomers();
                    }
                }
                else
                {
                    lueRooms.Focus();
                    MessageBox.Show("Vui lòng chọn phòng cần chuyển vào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("frmAddListCustomerToCustomerGroups.btnSelectCustomers_ButtonClick\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEditCustomers_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(viewAvailableCustomers.GetFocusedRowCellValue("ID"));
                frmUpd_Customers afrmUpd_Customers = new frmUpd_Customers(this, ID);
                afrmUpd_Customers.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.btnEditCustomers_ButtonClick\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Hiennv
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BookingRoomsMembersBO aBookingRoomsMembersBO = new BookingRoomsMembersBO();
                BookingRoomsBO aBookingRoomsBO = new BookingRoomsBO();
                BookingRooms aBookingRoomsUpdate = aBookingRoomsBO.Select_ByID(this.IDBookingRooms);
                aBookingRoomsUpdate.CheckOutActual = DateTime.Now;
                int updateType = 0;
                int insertType = 0;

                if (aBookingRoomsUpdate.Type == 3)//Tính checkin sớm và Checkout muộn
                {
                    updateType = 2;
                    insertType = 1;
                }
                else if (aBookingRoomsUpdate.Type == 0)//Không tính checkIn sớm và checkout muộn.
                {
                    updateType = 0;
                    insertType = 0;
                }
                else if (aBookingRoomsUpdate.Type == 2)//Tính checkin sớm ,không tính checkout muộn.
                {
                    updateType = 2;
                    insertType = 0;
                }
                else if (aBookingRoomsUpdate.Type == 1)//Không tính checkin sớm ,tính checkout muộn
                {
                    updateType = 0;
                    insertType = 1;
                }
                aBookingRoomsUpdate.Type = updateType;
                aBookingRoomsUpdate.Status = 7;//da checkout nhung chua thanh toan
                // cap nhat lai checkoutActual va status cho bookingroom
                int count = aBookingRoomsBO.Update(aBookingRoomsUpdate);
                if (count > 0)
                {
                    //them moi mot bookingroom
                    aBookingRoomsBO = new BookingRoomsBO();
                    BookingRooms aBookingRooms = aBookingRoomsBO.Select_ByID(this.IDBookingRooms);
                    BookingRooms aBookingRoomsAddNew = new BookingRooms();
                    aBookingRoomsAddNew.IDBookingR =this.IDBookingRs;
                    aBookingRoomsAddNew.CodeRoom = aBookingRooms.CodeRoom;
                    aBookingRoomsAddNew.PercentTax = aBookingRooms.PercentTax;
                    aBookingRoomsAddNew.CostRef_Rooms = aBookingRooms.CostRef_Rooms;
                    aBookingRoomsAddNew.Note = aBookingRooms.Note;
                    aBookingRoomsAddNew.CheckInPlan = aBookingRooms.CheckOutActual;
                    aBookingRoomsAddNew.CheckInActual = aBookingRooms.CheckOutActual;
                    aBookingRoomsAddNew.CheckOutPlan = aBookingRooms.CheckOutPlan;
                    aBookingRoomsAddNew.CheckOutActual = aBookingRooms.CheckOutPlan;
                    aBookingRoomsAddNew.BookingStatus = aBookingRooms.BookingStatus;
                    aBookingRoomsAddNew.Status = 3;// 3 = Da CheckIn                  
                    aBookingRoomsAddNew.Type = insertType;
                    aBookingRoomsAddNew.StartTime = aBookingRooms.StartTime;
                    aBookingRoomsAddNew.EndTime = aBookingRooms.EndTime;
                    aBookingRoomsAddNew.IsAllDayEvent = aBookingRooms.IsAllDayEvent;
                    aBookingRoomsAddNew.Color = aBookingRooms.Color;
                    aBookingRoomsAddNew.IsRecurring = aBookingRooms.IsRecurring;
                    aBookingRoomsAddNew.IsEditable = aBookingRooms.IsEditable;
                    aBookingRoomsAddNew.AdditionalColumn1 = aBookingRooms.AdditionalColumn1;
                    int ID = aBookingRoomsBO.Insert(aBookingRoomsAddNew);

                    if (ID > 0)
                    {
                        BookingRoomsMembers aBookingRoomsMembers;
                        List<CustomerInfoEN> aListCustomerInfoEN = this.aListCustomersInRoom.Where(r => r.IDBookingRoom == this.IDBookingRooms).ToList();
                        foreach (CustomerInfoEN item1 in aListCustomerInfoEN)
                        {
                            aBookingRoomsMembers = new BookingRoomsMembers();
                            aBookingRoomsMembers.IDBookingRoom = ID;
                            aBookingRoomsMembers.IDCustomer = item1.ID;
                            aBookingRoomsMembersBO.Insert(aBookingRoomsMembers);
                        }
                    }
                }

                if (Convert.ToInt32(lueRooms.EditValue) != this.IDBookingRooms)
                {
                    aBookingRoomsBO = new BookingRoomsBO();
                    BookingRooms aBookingRoomsCheck = aBookingRoomsBO.Select_ByID(Convert.ToInt32(lueRooms.EditValue));
                    if (aBookingRoomsCheck != null)
                    {
                        aBookingRoomsBO = new BookingRoomsBO();
                        BookingRooms aBookingRoomsUpdate1 = aBookingRoomsBO.Select_ByID(Convert.ToInt32(lueRooms.EditValue));
                        aBookingRoomsUpdate1.CheckOutActual = DateTime.Now;

                        if (aBookingRoomsUpdate1.Type == 1)//Tính checkin sớm và Checkout muộn
                        {
                            updateType = 3;
                        }
                        else if (aBookingRoomsUpdate1.Type == 2)//Không tính checkIn sớm và checkout muộn.
                        {
                            updateType = 4;
                        }
                        else if (aBookingRoomsUpdate1.Type == 3)//Tính checkin sớm ,không tính checkout muộn.
                        {
                            updateType = 4;
                        }
                        else if (aBookingRoomsUpdate1.Type == 4)//Không tính checkin sớm ,tính checkout muộn
                        {
                            updateType = 2;
                        }

                        aBookingRoomsUpdate1.Type = updateType;
                        aBookingRoomsUpdate1.Status = 7;//da checkout nhung chua thanh toan
                        // cap nhat lai checkoutActual va status cho bookingroom
                        int count1 = aBookingRoomsBO.Update(aBookingRoomsUpdate);

                        if (count1 > 0)
                        {
                            //them moi mot bookingroom
                            aBookingRoomsBO = new BookingRoomsBO();
                            BookingRooms aBookingRooms = aBookingRoomsBO.Select_ByID(Convert.ToInt32(lueRooms.EditValue));

                            BookingRooms aBookingRoomsAddNew = new BookingRooms();
                            aBookingRoomsAddNew.IDBookingR =this.IDBookingRs;
                            aBookingRoomsAddNew.CodeRoom = aBookingRooms.CodeRoom;
                            aBookingRoomsAddNew.PercentTax = aBookingRooms.PercentTax;
                            aBookingRoomsAddNew.CostRef_Rooms = aBookingRooms.CostRef_Rooms;
                            aBookingRoomsAddNew.Note = aBookingRooms.Note;
                            aBookingRoomsAddNew.CheckInPlan = aBookingRooms.CheckOutActual;
                            aBookingRoomsAddNew.CheckInActual = aBookingRooms.CheckOutActual;
                            aBookingRoomsAddNew.CheckOutPlan = aBookingRooms.CheckOutPlan;
                            aBookingRoomsAddNew.CheckOutActual = aBookingRooms.CheckOutPlan;
                            aBookingRoomsAddNew.BookingStatus = aBookingRooms.BookingStatus;
                            aBookingRoomsAddNew.Status = 3;// 3 = Da CheckIn

                            if (aBookingRoomsUpdate.Type == 1)//Tính checkin sớm và Checkout muộn
                            {
                                insertType = 3;
                            }
                            else if (aBookingRoomsUpdate.Type == 2)//Không tính checkIn sớm và checkout muộn.
                            {
                                insertType = 4;
                            }
                            else if (aBookingRoomsUpdate.Type == 3)//Tính checkin sớm ,không tính checkout muộn.
                            {
                                insertType = 4;
                            }
                            else if (aBookingRoomsUpdate.Type == 4)//Không tính checkin sớm ,tính checkout muộn
                            {
                                insertType = 2;
                            }

                            aBookingRoomsAddNew.Type = insertType;
                            aBookingRoomsAddNew.StartTime = aBookingRooms.StartTime;
                            aBookingRoomsAddNew.EndTime = aBookingRooms.EndTime;
                            aBookingRoomsAddNew.IsAllDayEvent = aBookingRooms.IsAllDayEvent;
                            aBookingRoomsAddNew.Color = aBookingRooms.Color;
                            aBookingRoomsAddNew.IsRecurring = aBookingRooms.IsRecurring;
                            aBookingRoomsAddNew.IsEditable = aBookingRooms.IsEditable;
                            aBookingRoomsAddNew.AdditionalColumn1 = aBookingRooms.AdditionalColumn1;
                            int ID = aBookingRoomsBO.Insert(aBookingRoomsAddNew);

                            if (ID > 0)
                            {
                                BookingRoomsMembers aBookingRoomsMembers;
                                List<CustomerInfoEN> aListCustomerInfoEN = this.aListCustomersInRoom.Where(r => r.IDBookingRoom == Convert.ToInt32(lueRooms.EditValue)).ToList();
                                foreach (CustomerInfoEN item1 in aListCustomerInfoEN)
                                {
                                    aBookingRoomsMembers = new BookingRoomsMembers();
                                    aBookingRoomsMembers.IDBookingRoom = ID;
                                    aBookingRoomsMembers.IDCustomer = item1.ID;
                                    aBookingRoomsMembersBO.Insert(aBookingRoomsMembers);
                                }
                            }
                        }
                    }
                    else
                    {
                        //them moi mot bookingroom
                        aBookingRoomsBO = new BookingRoomsBO();
                        BookingRooms aBookingRooms = aBookingRoomsBO.Select_ByID(this.IDBookingRooms);

                        BookingRooms aBookingRoomsAddNew = new BookingRooms();
                        aBookingRoomsAddNew.IDBookingR =this.IDBookingRs;
                        List<RoomsEN> aListTemp = this.aListRooms.Where(r => r.IDBookingRooms == Convert.ToInt32(lueRooms.EditValue)).ToList();
                        if (aListTemp.Count > 0)
                        {
                            aBookingRoomsAddNew.CodeRoom = aListTemp[0].Code;
                        }
                        aBookingRoomsAddNew.PercentTax = aBookingRooms.PercentTax;
                        aBookingRoomsAddNew.CostRef_Rooms = aBookingRooms.CostRef_Rooms;
                        aBookingRoomsAddNew.Note = aBookingRooms.Note;
                        aBookingRoomsAddNew.CheckInPlan = aBookingRooms.CheckOutActual;
                        aBookingRoomsAddNew.CheckInActual = aBookingRooms.CheckOutActual;
                        aBookingRoomsAddNew.CheckOutPlan = aBookingRooms.CheckOutPlan;
                        aBookingRoomsAddNew.CheckOutActual = aBookingRooms.CheckOutPlan;
                        aBookingRoomsAddNew.BookingStatus = aBookingRooms.BookingStatus;
                        aBookingRoomsAddNew.Status = 3;// 3 = Da CheckIn
                        aBookingRoomsAddNew.Type = 1;//Tính checkin sớm và Checkout muộn
                        aBookingRoomsAddNew.StartTime = aBookingRooms.StartTime;
                        aBookingRoomsAddNew.EndTime = aBookingRooms.EndTime;
                        aBookingRoomsAddNew.IsAllDayEvent = aBookingRooms.IsAllDayEvent;
                        aBookingRoomsAddNew.Color = aBookingRooms.Color;
                        aBookingRoomsAddNew.IsRecurring = aBookingRooms.IsRecurring;
                        aBookingRoomsAddNew.IsEditable = aBookingRooms.IsEditable;
                        aBookingRoomsAddNew.AdditionalColumn1 = aBookingRooms.AdditionalColumn1;
                        int ID = aBookingRoomsBO.Insert(aBookingRoomsAddNew);
                        if (ID > 0)
                        {
                            BookingRoomsMembers aBookingRoomsMembers;
                            List<CustomerInfoEN> aListCustomerInfoEN = this.aListCustomersInRoom.Where(r => r.IDBookingRoom == Convert.ToInt32(lueRooms.EditValue)).ToList();
                            foreach (CustomerInfoEN item1 in aListCustomerInfoEN)
                            {
                                aBookingRoomsMembers = new BookingRoomsMembers();
                                aBookingRoomsMembers.IDBookingRoom = ID;
                                aBookingRoomsMembers.IDCustomer = item1.ID;
                                aBookingRoomsMembersBO.Insert(aBookingRoomsMembers);
                            }
                        }
                    }
                }
                MessageBox.Show("Thực hiện thành công.", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (this.afrmMain != null)
                {
                    this.afrmMain.ReloadData();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.btnSave_Click\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiennv
        private void lueRooms_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.GetListCustomers_ByIDBookingRoom(Convert.ToInt32(lueRooms.EditValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.lueRooms_EditValueChanged\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Hiennv
        private void btnExtensionRoom_Click(object sender, EventArgs e)
        {
            try
            {
                BookingRoomsBO aBookingRoomsBO = new BookingRoomsBO();
                BookingRooms aBookingRoomsUpdate = aBookingRoomsBO.Select_ByID(this.IDBookingRooms);
                aBookingRoomsUpdate.CheckOutPlan = dtpCheckOut.DateTime;
                aBookingRoomsUpdate.CheckOutActual = dtpCheckOut.DateTime;
                // cap nhat lai checkoutActual va status cho bookingroom
                int count = aBookingRoomsBO.Update(aBookingRoomsUpdate);
                MessageBox.Show("Thực hiện thành công.", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (this.afrmMain != null)
                {
                    this.afrmMain.ReloadData();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_EditBooking.btnExtensionRoom_Click\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}