using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using BussinessLogic;
using Entity;
using DataAccess;
using DevExpress.XtraRichEdit.API.Word;
using System.Linq;
using Library;
using System.Globalization;

namespace RoomManager
{
    public partial class frmRpt_Payment_BookingRsAndBookingHs : XtraReport
    {
        private NewPaymentEN aNewPaymentEN = new NewPaymentEN();
        List<ServiceGroupEN> aListServicesGroupEN = new List<ServiceGroupEN>();
        List<ServiceUsedEN> aListServiceUsedRoom = new List<ServiceUsedEN>();
        List<ServiceUsedEN> aListServiceUsedHall = new List<ServiceUsedEN>();

        List<int> aListIDServicesGroupRoom = new List<int>();
        List<int> aListIDServicesGroupHall = new List<int>();

        public frmRpt_Payment_BookingRsAndBookingHs(NewPaymentEN aNewPaymentEN)
        {
            InitializeComponent();
            this.aNewPaymentEN = aNewPaymentEN;
            try
            {


                lblNumberVote.Text = Convert.ToString(this.aNewPaymentEN.IDBookingR);
                lblIIDBookingR.Text = Convert.ToString(this.aNewPaymentEN.IDBookingR);
                lblNameCustomer.Text = this.aNewPaymentEN.NameCustomer;
                lblGroup.Text = this.aNewPaymentEN.NameCustomerGroup;
                lblCompany.Text = this.aNewPaymentEN.NameCompany;
                lblTaxNumberCode.Text = this.aNewPaymentEN.TaxNumberCodeCompany;

                int day = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                lblDayMonthYear.Text = "Hà nội , ngày " + day.ToString() + " tháng " + month.ToString() + " năm " + year.ToString();

                //------------- Phong ------------------------

                aListServiceUsedRoom = this.aNewPaymentEN.GetAllServiceUsedInRoom();
                //Lấy List< IDServiceGroup>
                List<int> aTemp = new List<int>();
                int IDServiceGroup;
                foreach (ServiceUsedEN item in aListServiceUsedRoom)
                {
                    IDServiceGroup = new int();
                    IDServiceGroup = item.IDServiceGroup;
                    aTemp.Add(IDServiceGroup);
                }
                aListIDServicesGroupRoom = aTemp.Distinct().ToList();


                ServiceGroupEN aServicesGroupEN;
                ServiceGroupsBO aServiceGroupsBO = new ServiceGroupsBO();

                foreach (int item in aListIDServicesGroupRoom)
                {
                    aServicesGroupEN = new ServiceGroupEN();
                    aServicesGroupEN.IDServiceGroup = item;
                    aServicesGroupEN.TotalMoneyBeforeTax = this.GetTotalMoneyServiceGroupBeforeTax(item);
                    aServicesGroupEN.TotalMoneyAfterTax = this.GetTotalMoneyServiceGroupAfterTax(item);
                    aServicesGroupEN.ServiceGroupName = aServiceGroupsBO.Sel_ByID(item).Name;
                    aListServicesGroupEN.Add(aServicesGroupEN);
                }

                //danh sach phong
                this.DetailReport.DataSource = aNewPaymentEN.aListBookingRoomUsed;


                colSkuRoom.DataBindings.Add("Text", this.DetailReport.DataSource, "RoomSku");
                colCheckIn.DataBindings.Add("Text", this.DetailReport.DataSource, "CheckInActual", "{0:dd-MM-yyyy HH:mm}");
                colCheckOut.DataBindings.Add("Text", this.DetailReport.DataSource, "CheckOutActual", "{0:dd-MM-yyyy HH:mm}");
                colBookingRoomCost.DataBindings.Add("Text", this.DetailReport.DataSource, "Cost", "{0:0,0.##}");
                colDateInUse.DataBindings.Add("Text", this.DetailReport.DataSource, "DateUsed", "{0:0,0.##}");
                colMoneyRoomBeforeTax.DataBindings.Add("Text", this.DetailReport.DataSource, "MoneyRoomBeforeTax", "{0:0,0}");
                colPercentTaxRoom.DataBindings.Add("Text", this.DetailReport.DataSource, "DisplayMoneyTaxRoom", "{0:0,0}");
                colPaymentMoneyaRoom.DataBindings.Add("Text", this.DetailReport.DataSource, "MoneyRoom", "{0:0,0}");

                ////tong tien phong truoc thue
                //lblSumMoneyRoomsBeforeTax.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.GetMoneyRoomsBeforeTax()));
                ////Tong tien thue
                //lblSumMoneyRoomTax.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.GetMoneyTax(this.aNewPaymentEN.GetMoneyRoomsBeforeTax(), 10)));
                ////tong tien phong sau thue
                //lblSumMoneyRoomsBehindTax.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.GetMoneyRooms()));


                XRSummary aXRSummaryMoneyRoomBeforeTax = new XRSummary();
                aXRSummaryMoneyRoomBeforeTax.Func = SummaryFunc.Sum;
                aXRSummaryMoneyRoomBeforeTax.Running = SummaryRunning.Group;
                aXRSummaryMoneyRoomBeforeTax.IgnoreNullValues = true;
                aXRSummaryMoneyRoomBeforeTax.FormatString = "{0:0,0}";
                XRBinding aXRBindingMoneyRoomBeforeTax = new XRBinding("Text", this.DetailReport.DataSource, "MoneyRoomBeforeTax", "{0:0,0}");
                XRBinding[] listXRBindingMoneyRoomBeforeTax = new XRBinding[] { aXRBindingMoneyRoomBeforeTax };
                lblSumMoneyRoomsBeforeTax.DataBindings.AddRange(listXRBindingMoneyRoomBeforeTax);
                lblSumMoneyRoomsBeforeTax.Summary = aXRSummaryMoneyRoomBeforeTax;


                XRSummary aXRSummaryDisplayMoneyTaxRoom = new XRSummary();
                aXRSummaryDisplayMoneyTaxRoom.Func = SummaryFunc.Sum;
                aXRSummaryDisplayMoneyTaxRoom.Running = SummaryRunning.Group;
                aXRSummaryDisplayMoneyTaxRoom.IgnoreNullValues = true;
                aXRSummaryDisplayMoneyTaxRoom.FormatString = "{0:0,0}";
                XRBinding aXRBindingDisplayMoneyTaxRoom = new XRBinding("Text", this.DetailReport.DataSource, "DisplayMoneyTaxRoom", "{0:0,0}");
                XRBinding[] listXRBindingDisplayMoneyTaxRoom = new XRBinding[] { aXRBindingDisplayMoneyTaxRoom };
                lblSumMoneyRoomTax.DataBindings.AddRange(listXRBindingDisplayMoneyTaxRoom);
                lblSumMoneyRoomTax.Summary = aXRSummaryDisplayMoneyTaxRoom;


                XRSummary aXRSummaryMoneyRoom = new XRSummary();
                aXRSummaryMoneyRoom.Func = SummaryFunc.Sum;
                aXRSummaryMoneyRoom.Running = SummaryRunning.Group;
                aXRSummaryMoneyRoom.IgnoreNullValues = true;
                aXRSummaryMoneyRoom.FormatString = "{0:0,0}";
                XRBinding aXRBindingMoneyRoom = new XRBinding("Text", this.DetailReport.DataSource, "MoneyRoom", "{0:0,0}");
                XRBinding[] listXRBindingMoneyRoom = new XRBinding[] { aXRBindingMoneyRoom };
                lblSumMoneyRoomsBehindTax.DataBindings.AddRange(listXRBindingMoneyRoom);
                lblSumMoneyRoomsBehindTax.Summary = aXRSummaryMoneyRoom;




                //danh sach dich vu
                this.DetailReport2.DataSource = aListServicesGroupEN;
                colNamService.DataBindings.Add("Text", this.DetailReport2.DataSource, "ServiceGroupName");
                colTotalMoneyBeforeTax.DataBindings.Add("Text", this.DetailReport2.DataSource, "TotalMoneyBeforeTax", "{0:0,0}");
                colPercentTaxService.DataBindings.Add("Text", this.DetailReport2.DataSource, "DisplayMoneyTax", "{0:0,0}");
                colTotalMoneyServiceAfterTax.DataBindings.Add("Text", this.DetailReport2.DataSource, "TotalMoneyAfterTax", "{0:0,0}");

                ////tong tien dich vu truoc thue
                //lblSumMoneyService_BookingRBeforeTax.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.GetTotalMoneyServiceUsedInRoomsBeforeTax()));
                ////Tong so tien thue
                //lblSumMoneyServiceTax.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.GetMoneyTax(this.aNewPaymentEN.GetTotalMoneyServiceUsedInRoomsBeforeTax(), 10)));
                ////tong tien dich vu sau thue
                //lblSumMoneyService_BookingRBehindTax.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.GetTotalMoneyServiceUsedInRooms()));

                XRSummary aXRSummaryTotalMoneyBeforeTax = new XRSummary();
                aXRSummaryTotalMoneyBeforeTax.Func = SummaryFunc.Sum;
                aXRSummaryTotalMoneyBeforeTax.Running = SummaryRunning.Group;
                aXRSummaryTotalMoneyBeforeTax.IgnoreNullValues = true;
                aXRSummaryTotalMoneyBeforeTax.FormatString = "{0:0,0}";
                lblSumMoneyService_BookingRBeforeTax.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DetailReport2.DataSource, "TotalMoneyBeforeTax", "{0:0,0}") });
                lblSumMoneyService_BookingRBeforeTax.Summary = aXRSummaryTotalMoneyBeforeTax;

                XRSummary aXRSummaryDisplayMoneyTax = new XRSummary();
                aXRSummaryDisplayMoneyTax.Func = SummaryFunc.Sum;
                aXRSummaryDisplayMoneyTax.Running = SummaryRunning.Group;
                aXRSummaryDisplayMoneyTax.IgnoreNullValues = true;
                aXRSummaryDisplayMoneyTax.FormatString = "{0:0,0}";
                lblSumMoneyServiceTax.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DetailReport2.DataSource, "DisplayMoneyTax", "{0:0,0}") });
                lblSumMoneyServiceTax.Summary = aXRSummaryDisplayMoneyTax;

                XRSummary aXRSummaryTotalMoneyAfterTax = new XRSummary();
                aXRSummaryTotalMoneyAfterTax.Func = SummaryFunc.Sum;
                aXRSummaryTotalMoneyAfterTax.Running = SummaryRunning.Group;
                aXRSummaryTotalMoneyAfterTax.IgnoreNullValues = true;
                aXRSummaryTotalMoneyAfterTax.FormatString = "{0:0,0}";
                lblSumMoneyService_BookingRBehindTax.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", this.DetailReport2.DataSource, "TotalMoneyAfterTax", "{0:0,0}") });
                lblSumMoneyService_BookingRBehindTax.Summary = aXRSummaryTotalMoneyAfterTax;



                //tong tien thanh toan truoc thue
                lblTotalMoneyBookingRBeforeTax.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.GetMoneyRoomsBeforeTax()) + Convert.ToDecimal(this.aNewPaymentEN.GetTotalMoneyServiceUsedInRoomsBeforeTax()));
                //tien thue
                lblTotalMoneyTax.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.GetMoneyTax(Convert.ToDecimal(this.aNewPaymentEN.GetMoneyRoomsBeforeTax()) + Convert.ToDecimal(this.aNewPaymentEN.GetTotalMoneyServiceUsedInRoomsBeforeTax()), 10)));
                //tong tien thanh toan sau thue
                lblTotalMoneyBookingRBehindTax.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.GetMoneyRooms()) + Convert.ToDecimal(this.aNewPaymentEN.GetTotalMoneyServiceUsedInRooms()));
                //So tien ung truoc
                lblBookingMoney_BookingR.Text = String.Format("{0:0,0}", Convert.ToDecimal(this.aNewPaymentEN.BookingRMoney));
                //so tien con lai can thanh toan
                lblTotalMoney_BookingR.Text = String.Format("{0:0,0}", (Convert.ToDecimal(this.aNewPaymentEN.GetMoneyRooms()) + Convert.ToDecimal(this.aNewPaymentEN.GetTotalMoneyServiceUsedInRooms())) - Convert.ToDecimal(this.aNewPaymentEN.BookingRMoney));
                string TotalMoney_BookingRString = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(StringUtility.ConvertDecimalToString((Convert.ToDecimal(this.aNewPaymentEN.GetMoneyRooms()) + Convert.ToDecimal(this.aNewPaymentEN.GetTotalMoneyServiceUsedInRooms())) - Convert.ToDecimal(this.aNewPaymentEN.BookingRMoney)));

                lblTotalMoney_BookingRString.Text = "(" + TotalMoney_BookingRString + ")";


                //------------------------------- Hoi truong ---------------------
                aListServiceUsedHall = this.aNewPaymentEN.GetAllServiceUsedInRoom();
                //Lấy List< IDServiceGroup>
                List<int> aTemp1 = new List<int>();
                int IDServiceGroupHall;
                foreach (ServiceUsedEN item in aListServiceUsedHall)
                {
                    IDServiceGroupHall = new int();
                    IDServiceGroupHall = item.IDServiceGroup;
                    aTemp1.Add(IDServiceGroupHall);
                }
                aListIDServicesGroupHall = aTemp.Distinct().ToList();


                ServiceGroupEN aServicesGroupHallEN;
                foreach (int item in aListIDServicesGroupHall)
                {
                    aServicesGroupHallEN = new ServiceGroupEN();
                    aServicesGroupHallEN.IDServiceGroup = item;
                    aServicesGroupHallEN.TotalMoneyBeforeTax = this.GetTotalMoneyServiceGroupHallBeforeTax(item);
                    aServicesGroupHallEN.TotalMoneyAfterTax = this.GetTotalMoneyServiceGroupHallAfterTax(item);
                    aServicesGroupHallEN.ServiceGroupName = aServiceGroupsBO.Sel_ByID(item).Name;
                    aListServicesGroupEN.Add(aServicesGroupHallEN);
                }

                //danh sach hoi truong
                this.DetailReportHall.DataSource = aNewPaymentEN.aListBookingHallUsed;
                colSkuHall.DataBindings.Add("Text", this.DetailReportHall.DataSource, "HallSku");
                colCreateDate.DataBindings.Add("Text", this.DetailReportHall.DataSource, "Date", "{0:dd/MM/yyyy}");
                colBookingHallCost.DataBindings.Add("Text", this.DetailReportHall.DataSource, "Cost", "{0:0,0}");
                colPercentTax.DataBindings.Add("Text", this.DetailReportHall.DataSource, "PercentTax");
                colPaymentMoneyHall.DataBindings.Add("Text", this.DetailReportHall.DataSource, "MoneyHall", "{0:0,0}");
                colMoneyHallBeforeTax.DataBindings.Add("Text", this.DetailReportHall.DataSource, "MoneyHallBeforeTax", "{0:0,0}");

                //danh sach dich vu su dung
                this.DetailReportService.DataSource = aListServicesGroupEN;
                colNamServiceHall.DataBindings.Add("Text", this.DetailReportService.DataSource, "ServiceGroupName");
                colTotalMoneyBeforeTax.DataBindings.Add("Text", this.DetailReportService.DataSource, "TotalMoneyBeforeTax", "{0:0,0}");
                colTotalMoneyServiceAfterTax.DataBindings.Add("Text", this.DetailReportService.DataSource, "TotalMoneyAfterTax", "{0:0,0}");


                //tong tien hoi truong truoc thue
                lblTotalMoneyHallBeforeTax.Text = String.Format("{0:0,0} (VND)", Convert.ToDecimal(this.aNewPaymentEN.GetOnlyMoneyHallsBeforeTax()));
                //tong tien hoi truong sau thue
                lblTotalMoneyHallBehindTax.Text = String.Format("{0:0,0} (VND)", Convert.ToDecimal(this.aNewPaymentEN.GetOnlyMoneyHalls()));

                //tong tien dich vu hoi truong truoc thue
                lblTotalMoneyService_BookingHBeforeTax.Text = String.Format("{0:0,0} (VND)", Convert.ToDecimal(this.aNewPaymentEN.GetTotalMoneyServiceUsedInHallsBeforeTax()));
                //tong tien dich vu hoi truong sau thue
                lblTotalMoneyServices_BookingHBehindTax.Text = String.Format("{0:0,0} (VND)", Convert.ToDecimal(this.aNewPaymentEN.GetTotalMoneyServiceUsedInHalls()));


                //tong tien bookingh

                decimal? moneyBookingHBeforeTax = Convert.ToDecimal(this.aNewPaymentEN.GetMoneyHallsBeforeTax());
                decimal? moneyBookingHBehindTax = Convert.ToDecimal(this.aNewPaymentEN.GetMoneyHalls());
                lblTotalMoneyBookingHBeforeTax.Text = String.Format("{0:0,0} (VND)", moneyBookingHBeforeTax);
                lblTotalMoneyBookingHBehindTax.Text = String.Format("{0:0,0} (VND)", moneyBookingHBehindTax);
                lblBookingMoney_BookingH.Text = String.Format("{0:0,0} (VND)", Convert.ToDecimal(this.aNewPaymentEN.BookingHMoney));
                lblTotalBookingH.Text = String.Format("{0:0,0} (VND)", (moneyBookingHBehindTax - Convert.ToDecimal(this.aNewPaymentEN.BookingHMoney)));



                //Tong tien hoa don can thanh toan
                decimal? beforTax = Convert.ToDecimal(this.aNewPaymentEN.GetMoneyRoomsBeforeTax()) + Convert.ToDecimal(this.aNewPaymentEN.GetMoneyHallsBeforeTax());
                decimal? behindTax = Convert.ToDecimal(this.aNewPaymentEN.GetMoneyHalls()) + Convert.ToDecimal(this.aNewPaymentEN.GetMoneyRooms());
                decimal? bookingMoney = Convert.ToDecimal(this.aNewPaymentEN.BookingHMoney) + Convert.ToDecimal(this.aNewPaymentEN.BookingRMoney);

                lblMoneyBookingRAndBookingHBeforeTax.Text = String.Format("{0:0,0} (VND)", beforTax);
                lblMoneyBookingRAndBookingHBehindTax.Text = String.Format("{0:0,0} (VND)", behindTax);
                lblBookingmoney_BookingRAndBookingH.Text = String.Format("{0:0,0} (VND)", bookingMoney);
                lblTotalMoneyBookigRAndBookingH.Text = String.Format("{0:0,0} (VND)", (behindTax - bookingMoney));

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public decimal? GetTotalMoneyServiceGroupBeforeTax(int IDServiceGroup)
        {
            decimal? TotalMoneyServiceGroupBeforeTax = 0;
            List<ServiceUsedEN> aTemp = aListServiceUsedRoom.Where(a => a.IDServiceGroup == IDServiceGroup).ToList();
            foreach (ServiceUsedEN item in aTemp)
            {
                decimal? cost = item.GetMoneyServiceBeforeTax();
                TotalMoneyServiceGroupBeforeTax = TotalMoneyServiceGroupBeforeTax + cost;
            }
            return TotalMoneyServiceGroupBeforeTax;
        }
        public decimal? GetTotalMoneyServiceGroupAfterTax(int IDServiceGroup)
        {
            decimal? TotalMoneyServiceGroupAfterTax = 0;
            List<ServiceUsedEN> aTemp = aListServiceUsedRoom.Where(a => a.IDServiceGroup == IDServiceGroup).ToList();
            foreach (ServiceUsedEN item in aTemp)
            {
                decimal? cost = item.GetMoneyService();
                TotalMoneyServiceGroupAfterTax = TotalMoneyServiceGroupAfterTax + cost;
            }
            return TotalMoneyServiceGroupAfterTax;
        }

        public decimal? GetTotalMoneyServiceGroupHallBeforeTax(int IDServiceGroup)
        {
            decimal? TotalMoneyServiceGroupBeforeTax = 0;
            List<ServiceUsedEN> aTemp = aListServiceUsedHall.Where(a => a.IDServiceGroup == IDServiceGroup).ToList();
            foreach (ServiceUsedEN item in aTemp)
            {
                decimal? cost = item.GetMoneyServiceBeforeTax();
                TotalMoneyServiceGroupBeforeTax = TotalMoneyServiceGroupBeforeTax + cost;
            }
            return TotalMoneyServiceGroupBeforeTax;
        }
        public decimal? GetTotalMoneyServiceGroupHallAfterTax(int IDServiceGroup)
        {
            decimal? TotalMoneyServiceGroupAfterTax = 0;
            List<ServiceUsedEN> aTemp = aListServiceUsedHall.Where(a => a.IDServiceGroup == IDServiceGroup).ToList();
            foreach (ServiceUsedEN item in aTemp)
            {
                decimal? cost = item.GetMoneyService();
                TotalMoneyServiceGroupAfterTax = TotalMoneyServiceGroupAfterTax + cost;
            }
            return TotalMoneyServiceGroupAfterTax;
        }
    }
}
