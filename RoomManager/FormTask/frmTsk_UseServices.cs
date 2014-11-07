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
        List<int> aListIDBookingRooms_Servicers = new List<int>();


        private frmTsk_Payment_Step2 afrmTsk_Payment_Step2 = null;
      
        string CodeCurrentRoom = string.Empty;
        int IDBookingRs = 0;
        int IDBookingRooms = 0;



        public frmTsk_UseServices()
        {
            InitializeComponent();
        }

        //NgocBM
        public frmTsk_UseServices(string CodeRoom, int IDBookingRs, int IDBookingRooms)
        {
            InitializeComponent();
            this.CodeCurrentRoom = CodeRoom;
            this.IDBookingRs = IDBookingRs;
            this.IDBookingRooms = IDBookingRooms;
        }

        // Truyền thêm đối số NewPayment để khi thêm dịch vụ từ form Payment sẽ load lại số dịch vụ đã thêm, các thông số còn lại vẫn để nguyên vì chưa chỉnh sửa hết được)
        public frmTsk_UseServices(frmTsk_Payment_Step2 afrmTsk_Payment_Step2, string CodeRoom, int IDBookingRs, int IDBookingRooms, NewPaymentEN aNewPayment)
        {
            InitializeComponent();
            this.afrmTsk_Payment_Step2 = afrmTsk_Payment_Step2;
            this.CodeCurrentRoom = CodeRoom;
            this.IDBookingRs = IDBookingRs;
            this.IDBookingRooms = IDBookingRooms;
            this.aNewPayment = aNewPayment;
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
            viewRoom_Services.UpdateCurrentRow();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            BookingRooms_ServicesBO aBookingRooms_ServicesBO = new BookingRooms_ServicesBO();
            BookingRooms_Services aBookingRooms_Services;
            int count = 0;

            for (int i = 0; i < this.alistRoomServiceInfo.Count; i++)
            {
                try
                {
                    aBookingRooms_Services = new BookingRooms_Services();
                    aBookingRooms_Services.CostRef_Services = this.alistRoomServiceInfo[i].CostRef;
                    aBookingRooms_Services.Cost = this.alistRoomServiceInfo[i].Cost;
                    if (this.alistRoomServiceInfo[i].ID > 0)
                    {
                        aBookingRooms_Services.ID = this.alistRoomServiceInfo[i].ID;
                    }
                    
                    aBookingRooms_Services.IDService = this.alistRoomServiceInfo[i].IDService;
                    aBookingRooms_Services.PercentTax = this.alistRoomServiceInfo[i].PercentTax;
                    aBookingRooms_Services.Quantity = this.alistRoomServiceInfo[i].Quantity;
                    aBookingRooms_Services.IDBookingRoom = this.alistRoomServiceInfo[i].IDBookingRooms;
                    aBookingRooms_Services.Status = this.alistRoomServiceInfo[i].Status;
                    if (aBookingRooms_Services.ID > 0)  // Gán mặc định trong cấu tử
                    {
                        aBookingRooms_Services.Date = this.alistRoomServiceInfo[i].Date;
                        aBookingRooms_ServicesBO.Update(aBookingRooms_Services);
                        count = 1;
                    }
                    else
                    {
                        aBookingRooms_Services.Date = dtpDate.DateTime;
                        aBookingRooms_ServicesBO.Insert(aBookingRooms_Services);
                        count = 1;
                    }
                }
                catch (Exception ex)
                {
                    count = 0;
                    MessageBox.Show("frmTsk_UseServices.btnSave_Click\n" + ex.Message.ToString());
                    break;
                }
            }

            if (this.alistRoomServiceInfo.Count <= 0)
            {
                count = 1;
            }


            //dung de xoa cac dich vu ma khong su dung nua
            foreach (int IDBookingRooms_Services in this.aListIDBookingRooms_Servicers)
            {
                try
                {
                    aBookingRooms_ServicesBO.Delete(IDBookingRooms_Services);
                    count = 1;
                }
                catch (Exception ex)
                {
                    count = 0;
                    MessageBox.Show("frmTsk_UseServices.btnSave_Click\n" + ex.Message.ToString());
                    break;
                }
            }


            if (count > 0)
            {
                if(this.afrmTsk_Payment_Step2 !=null)
                {
                    if (aNewPayment.aListBookingRoomUsed.Where(a => a.ID == IDBookingRooms).ToList().Count > 0)
                    {
                        aNewPayment.aListBookingRoomUsed.Where(a => a.ID == IDBookingRooms).ToList()[0].ListServiceUsed.Clear();
                        aNewPayment.aListBookingRoomUsed.Where(a => a.ID == IDBookingRooms).ToList()[0].ListServiceUsed = aReceptionTaskBO.GetListServiceUsedInRoom_ByIDBookingRoom(IDBookingRooms);
                             
                    }
                    this.afrmTsk_Payment_Step2.Reload(this.aNewPayment);
                }
                MessageBox.Show("Thực hiện thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        //------------------------------------------------------------------------------
        private void btnAddService_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.CodeCurrentRoom) == true)
            {
                MessageBox.Show("Vui lòng chọn phòng muốn sử dụng dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int ServiceID = int.Parse(viewServices.GetFocusedRowCellValue("ID").ToString());
                Services aService = aListService.Where(p => p.ID == ServiceID).ToList()[0];
                this.AddServiceToRoom(aService, this.CodeCurrentRoom, this.IDBookingRooms, this.IDBookingRs);
            }
        }

        //NgocBM
        private void frmTsk_UseServices_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDate.EditValue = DateTime.Now;
                ReceptionTaskBO aReceptionTaskBO = new ReceptionTaskBO();
                RoomsBO aRoomsBO = new RoomsBO();
                ServicesBO aServicesBO = new ServicesBO();

                if (this.afrmTsk_Payment_Step2 != null)
                {
                    this.aListRoomExtStatus = aReceptionTaskBO.GetInformationRoom_ByCodeRoomAndIDBookingRoom(this.IDBookingRooms, this.CodeCurrentRoom);
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
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.frmTsk_UseServices_Load\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
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
                this.IDBookingRooms = this.aListRoomExtStatus.Where(p => p.Code == CodeCurrentRoom).Select(p => p.BookingRooms_ID).ToList()[0];
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

        public void AddServiceToRoom(Services Service, string CodeRoom, int IDBookingRooms, int IDBookingRs)
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
                Item.IDBookingRooms = IDBookingRooms;
                Item.IDBookingRs = IDBookingRs;

                this.alistRoomServiceInfo.Add(Item);
                this.LoadDataServiceInUseForEachRoom(CodeRoom);
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.AddServiceToRoom\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //hiennv
        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int IDBookingRooms = Convert.ToInt32(viewRoom_Services.GetFocusedRowCellValue("IDBookingRooms"));
                int IDService = Convert.ToInt32(viewRoom_Services.GetFocusedRowCellValue("IDService"));
                int IDBookingRooms_Service = Convert.ToInt32(viewRoom_Services.GetFocusedRowCellValue("ID"));
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa ???", "Câu hỏi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == result)
                {
                    if (this.alistRoomServiceInfo.Where(rs => rs.IDBookingRooms == IDBookingRooms && rs.IDService == IDService).ToList().Count > 0)
                    {
                        RoomServiceInfoEN aRoomServiceInfoEN = new RoomServiceInfoEN();
                        aRoomServiceInfoEN = this.alistRoomServiceInfo.Where(rs => rs.IDBookingRooms == IDBookingRooms && rs.IDService == IDService).ToList()[0];
                        this.alistRoomServiceInfo.Remove(aRoomServiceInfoEN);
                        this.LoadDataRoom_Services();
                    }
                    if (IDBookingRooms_Service > 0)
                    {
                        this.aListIDBookingRooms_Servicers.Add(IDBookingRooms_Service);
                    }
                    MessageBox.Show("Bạn đã thực hiện thành công thành công .", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("frmTsk_UseServices.btnDelete_ButtonClick\n" + ex.Message.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}