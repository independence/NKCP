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

namespace RoomManager
{
    public partial class frmTsk_UseServices : DevExpress.XtraEditors.XtraForm
    {
        DatabaseDA aDatabaseDA = new DatabaseDA();
        ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
        List<RoomServiceInfoEN> alistRoomServiceInfo = new List<RoomServiceInfoEN>();
        List<Services> aListService = new List<Services>();
        List<RoomExtStatusEN> aListRoomExtStatus = new List<RoomExtStatusEN>();
        NewPaymentEN aNewPayment = new NewPaymentEN();
        List<int> aListIDBookingRoom_Servicers = new List<int>();

        List<BookingRoom_ServiceEN> aListSelected = new List<BookingRoom_ServiceEN>();
        List<BookingRooms_Services> aListRemove = new List<BookingRooms_Services>();


        private frmTsk_Payment_Step2 afrmTsk_Payment_Step2 = null;
      
        string CodeCurrentRoom = string.Empty;
        int IDBookingRs = 0;
        int IDBookingRoom = 0;



        public frmTsk_UseServices()
        {
            InitializeComponent();
        }

        //NgocBM
        public frmTsk_UseServices(string CodeRoom, int IDBookingRs, int IDBookingRoom)
        {
            InitializeComponent();
            this.CodeCurrentRoom = CodeRoom;
            this.IDBookingRs = IDBookingRs;
            this.IDBookingRoom = IDBookingRoom;
        }

        // Truyền thêm đối số NewPayment để khi thêm dịch vụ từ form Payment sẽ load lại số dịch vụ đã thêm, các thông số còn lại vẫn để nguyên vì chưa chỉnh sửa hết được)
        public frmTsk_UseServices(frmTsk_Payment_Step2 afrmTsk_Payment_Step2, string CodeRoom, int IDBookingRs, int IDBookingRoom, NewPaymentEN aNewPayment)
        {
            InitializeComponent();
            this.afrmTsk_Payment_Step2 = afrmTsk_Payment_Step2;
            this.CodeCurrentRoom = CodeRoom;
            this.IDBookingRs = IDBookingRs;
            this.IDBookingRoom = IDBookingRoom;
            this.aNewPayment = aNewPayment;
        }

        //NgocBM
        private void frmTsk_UseServices_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.afrmTsk_Payment_Step2 != null)
                {
                    this.Reload();
                    this.LoadServicesInRoom();
                }
                else
                {
                    dtpDate.EditValue = DateTime.Now;
                    ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
                    RoomsBO aRoomsBO = new RoomsBO();
                    ServicesBO aServicesBO = new ServicesBO();

                    if (this.afrmTsk_Payment_Step2 != null)
                    {
                        this.aListRoomExtStatus = aReceptionTaskBO.GetInformationRoom_ByCodeRoomAndIDBookingRoom(this.IDBookingRoom, this.CodeCurrentRoom);
                    }
                    else
                    {
                        this.aListRoomExtStatus = aRoomsBO.GetListUsingRooms_ByCode(DateTime.Now, this.CodeCurrentRoom);
                    }

                    dgvRooms.DataSource = this.aListRoomExtStatus;

                    this.aListService = aServicesBO.Select_ServiceForRooms();
                    dgvServices.DataSource = this.aListService;

                    List<string> aListCode = this.aListRoomExtStatus.Select(p => p.Code).ToList();
                    this.alistRoomServiceInfo = LoadDataRoomServiceInfo(DateTime.Now, aListCode);

                    this.LoadDataRoom_Services();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.frmTsk_UseServices_Load\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //hàm tính tiền 
        decimal PayService(GridView view, int RowIndex)
        {
            decimal Cost = Convert.ToDecimal(view.GetListSourceRowCellValue(RowIndex, "Cost"));
            decimal Quantity = Convert.ToDecimal(view.GetListSourceRowCellValue(RowIndex, "Quantity"));
            decimal VAT = Convert.ToDecimal(view.GetListSourceRowCellValue(RowIndex, "PercentTax"));
            return ((Cost * Quantity) + (Cost * Quantity * (VAT / 100)));
        }

        private void gridView2_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Sum" && e.IsGetData) e.Value = PayService(view, e.ListSourceRowIndex);
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            grvRoom_Services.UpdateCurrentRow();
        }

       
        #region New
        public void Reload()
        {
            BookingRoomsBO aBookingRoomsBO = new BookingRoomsBO();
            RoomsBO aRoomsBO = new RoomsBO();

            List<BookingRoomsEN> aListBookingRoom = new List<BookingRoomsEN>();
            BookingRoomsEN aBookingRoomsEN = new BookingRoomsEN();
            BookingRooms aBookingRooms = aBookingRoomsBO.Select_ByID(IDBookingRoom);
            aBookingRoomsEN.SetValue(aBookingRooms);
            aBookingRoomsEN.ID = aBookingRooms.ID;
            aBookingRoomsEN.RoomSku = aRoomsBO.Select_ByCodeRoom(aBookingRooms.CodeRoom, 1).Sku;
            aListBookingRoom.Add(aBookingRoomsEN);
            dgvRooms.DataSource = aListBookingRoom;
            dgvRooms.RefreshDataSource();

            ServicesBO aServicesBO = new ServicesBO();
            aListService = aServicesBO.Select_ByType(1);
            dgvServices.DataSource = aListService;
            dgvServices.RefreshDataSource();


        }
        private void LoadServicesInRoom()
        {
            try
            {
                ServicesBO aServicesBO = new ServicesBO();
                BookingRooms_ServicesBO aBookingRooms_ServicesBO = new BookingRooms_ServicesBO();
                List<BookingRooms_Services> aListTemp = aBookingRooms_ServicesBO.Select_ByIDBookingRooms(this.IDBookingRoom);
                BookingRoom_ServiceEN aBookingRoom_ServiceEN;

                for (int i = 0; i < aListTemp.Count; i++)
                {
                    aBookingRoom_ServiceEN = new BookingRoom_ServiceEN();
                    aBookingRoom_ServiceEN.ID = aListTemp[i].ID;
                    aBookingRoom_ServiceEN.Info = aListTemp[i].Info;
                    aBookingRoom_ServiceEN.Type = aListTemp[i].Type;
                    aBookingRoom_ServiceEN.Status = aListTemp[i].Status;
                    aBookingRoom_ServiceEN.Disable = aListTemp[i].Disable;
                    aBookingRoom_ServiceEN.IDBookingRoom = aListTemp[i].IDBookingRoom;
                    aBookingRoom_ServiceEN.IDService = aListTemp[i].IDService;
                    aBookingRoom_ServiceEN.Service_Name = aServicesBO.Select_ByID(aListTemp[i].IDService).Name;
                    aBookingRoom_ServiceEN.Service_Unit = aServicesBO.Select_ByID(aListTemp[i].IDService).Unit;
                    aBookingRoom_ServiceEN.Cost = aListTemp[i].Cost == null ? aListTemp[i].CostRef_Services : aListTemp[i].Cost;
                    aBookingRoom_ServiceEN.CostRef_Services = aListTemp[i].CostRef_Services;
                    aBookingRoom_ServiceEN.Date = aListTemp[i].Date;
                    aBookingRoom_ServiceEN.PercentTax = aListTemp[i].PercentTax;
                    aBookingRoom_ServiceEN.Quantity = aListTemp[i].Quantity;
                    aListSelected.Add(aBookingRoom_ServiceEN);
                }
                dgvRoom_Services.DataSource = aListSelected;
                dgvRoom_Services.RefreshDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmIns_BookingRooms_Services.grvRooms_RowClick\n" + ex.ToString(), "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void btnAddService_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.CodeCurrentRoom) == true)
            {
                MessageBox.Show("Vui lòng chọn phòng muốn sử dụng dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                BookingRoom_ServiceEN aBookingRoom_ServiceEN = new BookingRoom_ServiceEN();
                int IDService = int.Parse(grvServices.GetFocusedRowCellValue("ID").ToString());
                aBookingRoom_ServiceEN.IDService = IDService;
                aBookingRoom_ServiceEN.IDBookingRoom = this.IDBookingRoom;
                aBookingRoom_ServiceEN.Service_Name = grvServices.GetFocusedRowCellValue("Name").ToString();
                aBookingRoom_ServiceEN.Cost = Convert.ToDecimal(grvServices.GetFocusedRowCellValue("CostRef"));
                aBookingRoom_ServiceEN.CostRef_Services = Convert.ToDecimal(grvServices.GetFocusedRowCellValue("CostRef"));
                aBookingRoom_ServiceEN.Service_Unit = grvServices.GetFocusedRowCellValue("Unit").ToString();

                this.aListSelected.Insert(0, aBookingRoom_ServiceEN);
                dgvRoom_Services.DataSource = aListSelected;
                dgvRoom_Services.RefreshDataSource();
            }
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int IDService = Convert.ToInt32(grvRoom_Services.GetFocusedRowCellValue("IDService"));
                List<BookingRoom_ServiceEN> aListTemp = aListSelected.Where(a => a.IDService == IDService).ToList();
                if (aListTemp.Count > 0)
                {
                    aListSelected.Remove(aListTemp[0]);
                }
                BookingRooms_ServicesBO aBookingRooms_ServicesBO = new BookingRooms_ServicesBO();
                BookingRooms_Services aTemp = aBookingRooms_ServicesBO.Select_ByIDBookingRoom_ByIDService(IDBookingRoom, IDService);
                if (aTemp != null)
                {                   
                    this.aListRemove.Add(aTemp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.btnDelete_ButtonClick\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

             BookingRooms_ServicesBO aBookingRooms_ServicesBO = new BookingRooms_ServicesBO();
            BookingRooms_Services aBookingRooms_Services;
            for (int i = 0; i < aListSelected.Count; i++)
            {
                aBookingRooms_Services = aBookingRooms_ServicesBO.Select_ByID(aListSelected[i].ID);
                if (aBookingRooms_Services != null)
                {
                    aBookingRooms_Services.Cost = aListSelected[i].Cost;
                    aBookingRooms_Services.Quantity = aListSelected[i].Quantity;
                    aBookingRooms_ServicesBO.Update(aBookingRooms_Services);
                }
                else
                {
                    aBookingRooms_Services = new BookingRooms_Services();
                    aBookingRooms_Services.Info = "";
                    aBookingRooms_Services.Type = 1;
                    aBookingRooms_Services.Status = 1;
                    aBookingRooms_Services.Disable = false;
                    aBookingRooms_Services.IDBookingRoom = this.IDBookingRoom;
                    aBookingRooms_Services.IDService = aListSelected[i].IDService;
                    aBookingRooms_Services.Cost = aListSelected[i].Cost;
                    aBookingRooms_Services.Date = DateTime.Now;
                    aBookingRooms_Services.CostRef_Services = aListSelected[i].CostRef_Services;
                    aBookingRooms_Services.PercentTax = 10;// de mac dinh
                    aBookingRooms_Services.Quantity = aListSelected[i].Quantity;
                    aBookingRooms_ServicesBO.Insert(aBookingRooms_Services);
                }
            }
            foreach (BookingRooms_Services items in this.aListRemove)
            {
                aBookingRooms_ServicesBO.Delete(items.IDService, items.IDBookingRoom,Convert.ToDateTime(items.Date));
            }

            if (this.afrmTsk_Payment_Step2 != null)
            {
                if (aNewPayment.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
                {
                    aNewPayment.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].ListServiceUsed.Clear();
                    aNewPayment.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].ListServiceUsed = aReceptionTaskBO.GetListServiceUsedInRoom_ByIDBookingRoom(IDBookingRoom);

                }
                this.afrmTsk_Payment_Step2.Reload(this.aNewPayment);
            }

            MessageBox.Show("Thực hiện thành công!", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
            }
        

      
        #endregion
      
      
        //hiennv
        private void LoadDataRoom_Services()
        {
            try{
                if (this.aListRoomExtStatus.Where(p => p.Code == this.CodeCurrentRoom).ToList().Count > 0)
                {
                   
                    dgvRoom_Services.DataSource = this.alistRoomServiceInfo;

                    int index = this.aListRoomExtStatus.IndexOf(this.aListRoomExtStatus.Where(p => p.Code == this.CodeCurrentRoom).ToList()[0]);
                    this.LoadDataServiceInUseForEachRoom(this.CodeCurrentRoom);
                    viewRooms.SelectRow(index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.LoadDataRoom_Services\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                this.CodeCurrentRoom = viewRooms.GetRowCellValue(e.RowHandle, "Code").ToString();
                this.IDBookingRoom = this.aListRoomExtStatus.Where(p => p.Code == CodeCurrentRoom).Select(p => p.BookingRooms_ID).ToList()[0];
                this.IDBookingRs = this.aListRoomExtStatus.Where(p => p.Code == CodeCurrentRoom).Select(p => p.BookingRs_ID).ToList()[0].GetValueOrDefault();

                this.LoadDataServiceInUseForEachRoom(this.CodeCurrentRoom);
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.gridView1_RowClick\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //NgocBM
        private List<RoomServiceInfoEN> LoadDataRoomServiceInfo(DateTime dt, List<string> ListCodeRoom)
        {
            try
            {
                BookingRooms_ServicesBO aBookingRooms_ServicesBO = new BookingRooms_ServicesBO();

                List<RoomServiceInfoEN> alist = new List<RoomServiceInfoEN>();
                for (int a = 0; a < ListCodeRoom.Count; a++)
                {
                    alist = alist.Union(aBookingRooms_ServicesBO.Select_Service_ByCodeRoom_ByStatus(ListCodeRoom[a], dt,8).ToList()).ToList();
                }
                return alist;
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.LoadDataRoomServiceInfo\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void LoadDataServiceInUseForEachRoom(string CodeRoom)
        {
            try
            {
                List<RoomServiceInfoEN> aList = this.alistRoomServiceInfo.Where(p => p.CodeRoom == CodeRoom).ToList();
                this.dgvRoom_Services.DataSource = aList;
                this.dgvRoom_Services.RefreshDataSource();
                if (this.aListRoomExtStatus.Where(p => p.Code == CodeRoom).ToList().Count > 0)
                {
                    this.lbCurrentRoom.Text = "Phòng số : " + this.aListRoomExtStatus.Where(p => p.Code == CodeRoom).ToList()[0].Sku;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.LoadDataServiceInUseForEachRoom\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AddServiceToRoom(Services Service, string CodeRoom, int IDBookingRoom, int IDBookingRs)
        {
            try
            {
                RoomsBO aRoomsBO = new RoomsBO();
                RoomServiceInfoEN Item = new RoomServiceInfoEN();
                Rooms aRoom = aRoomsBO.Select_ByCodeRoom(CodeRoom, 1); //1: IDLang , tieng anh
                if(aRoom !=null)
                {
                    Item.Sku = aRoom.Sku;
                    Item.CodeRoom = aRoom.Code;
                }


                Item.IDService = Service.ID;
                Item.IDServiceGroup = Service.IDServiceGroups;
                Item.ServiceName = Service.Name;
                Item.Unit = Service.Unit;
                Item.CostRef = Service.CostRef;

                Item.PercentTax = 10;
                Item.Quantity = 1;
                
                Item.Date = DateTime.Now;
                //Item.Cost = Item.CostRef * decimal.Parse(Item.Quantity.ToString()) + (Item.CostRef * decimal.Parse((Item.Quantity * Item.PercentTax).ToString())) / 100;
                Item.Cost = Item.CostRef;
                Item.IDBookingRooms = IDBookingRoom;
                Item.IDBookingRs = IDBookingRs;

                this.alistRoomServiceInfo.Add(Item);
                this.LoadDataServiceInUseForEachRoom(CodeRoom);
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.AddServiceToRoom\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
       
    }

}