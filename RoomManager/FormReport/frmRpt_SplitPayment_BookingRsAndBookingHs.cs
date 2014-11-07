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

namespace RoomManager
{
    public partial class frmRpt_SplitPayment_BookingRsAndBookingHs : XtraReport
    {
        private NewPaymentEN aNewPaymentEN = new NewPaymentEN();
        private int IndexSub = 0;

        public frmRpt_SplitPayment_BookingRsAndBookingHs(NewPaymentEN aNewPaymentEN,int IndexSub)
        {
            InitializeComponent();
            this.aNewPaymentEN = aNewPaymentEN;
            this.IndexSub = IndexSub;
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

                List<BookingRoomUsedEN> aListBookingRoomUsedEN = new List<BookingRoomUsedEN>();
                aListBookingRoomUsedEN = this.aNewPaymentEN.aListBookingRoomUsed.Where(r => r.IndexSubPayment == this.IndexSub).OrderBy(r => r.RoomSku).ToList();
                aListBookingRoomUsedEN.Count();
                List<ServiceUsedEN> aListServicesEN = new List<ServiceUsedEN>();
                aListServicesEN = this.aNewPaymentEN.GetAllServiceUsedInRoom().Where(r => r.IndexSubPayment == this.IndexSub).OrderBy(r => r.Sku).ToList();

                decimal? sumMoneyRoomBeforeTax = aListBookingRoomUsedEN.Sum(r => r.MoneyRoomBeforeTax);

                decimal? sumMoneyRoomBehindTax = aListBookingRoomUsedEN.Sum(r => r.MoneyRoom);
                decimal? sumMoneyServiceRoomBehindTax = aListServicesEN.Sum(s => s.TotalMoney);
                decimal? sumMoneyServiceRoomBeforeTax = aListServicesEN.Sum(s => s.TotalMoneyBeforeTax);

                decimal? BookingMoneyR = 0;

                //danh sach phong
                this.DetailReport.DataSource = aListBookingRoomUsedEN;
                colSkuRoom.DataBindings.Add("Text", this.DetailReport.DataSource, "RoomSku");
                colCheckIn.DataBindings.Add("Text", this.DetailReport.DataSource, "CheckInActual", "{0:dd-MM-yyyy HH:mm}");
                colCheckOut.DataBindings.Add("Text", this.DetailReport.DataSource, "CheckOutActual", "{0:dd-MM-yyyy HH:mm}");
                colBookingRoomCost.DataBindings.Add("Text", this.DetailReport.DataSource, "Cost", "{0:0,0.##}");
                colDateInUse.DataBindings.Add("Text", this.DetailReport.DataSource, "DateUsed", "{0:0,0.##}");
                colPercentTaxRoom.DataBindings.Add("Text", this.DetailReport.DataSource, "PercentTax");
                colPaymentMoneyaRoom.DataBindings.Add("Text", this.DetailReport.DataSource, "MoneyRoom", "{0:0,0}");
                colMoneyRoomBeforeTax.DataBindings.Add("Text", this.DetailReport.DataSource, "MoneyRoomBeforeTax", "{0:0,0}");

                //tong tien phong truoc thue
                lblSumMoneyRoomsBeforeTax.Text = String.Format("{0:0,0} (VND)", sumMoneyRoomBeforeTax);
                //tong tien phong sau thue
                lblSumMoneyRoomsBehindTax.Text = String.Format("{0:0,0} (VND)", sumMoneyRoomBehindTax);

                //danh sach dich vu
                this.DetailReport2.DataSource = aListServicesEN;
                colNameSku.DataBindings.Add("Text", this.DetailReport2.DataSource, "Sku");
                colNamService.DataBindings.Add("Text", this.DetailReport2.DataSource, "NameService");
                colDateUse.DataBindings.Add("Text", this.DetailReport2.DataSource, "DateUsed", "{0:dd-MM-yyyy}");
                colQuantity.DataBindings.Add("Text", this.DetailReport2.DataSource, "Quantity");
                colBookingRooms_ServicesCost.DataBindings.Add("Text", this.DetailReport2.DataSource, "Cost", "{0:0,0}");
                colPercentTaxService.DataBindings.Add("Text", this.DetailReport2.DataSource, "Tax");
                colPaymentMoneyService.DataBindings.Add("Text", this.DetailReport2.DataSource, "TotalMoney", "{0:0,0}");

                //tong tien dich vu truoc thue
                lblSumMoneyService_BookingRBeforeTax.Text = String.Format("{0:0,0} (VND)", sumMoneyServiceRoomBeforeTax);

                //tong tien dich vu sau thue
                lblSumMoneyService_BookingRBehindTax.Text = String.Format("{0:0,0} (VND)", sumMoneyServiceRoomBehindTax);


                //tong tien thanh toan truoc thue
                lblTotalMoneyBookingRBeforeTax.Text = String.Format("{0:0,0} (VND)",(sumMoneyRoomBeforeTax + sumMoneyServiceRoomBeforeTax));
                //tong tien thanh toan sau thue
                lblTotalMoneyBookingRBehindTax.Text = String.Format("{0:0,0} (VND)",(sumMoneyRoomBehindTax + sumMoneyServiceRoomBehindTax));
                //So tien ung truoc
                lblBookingMoney_BookingR.Text = String.Format("{0:0,0} (VND)", BookingMoneyR);
                //so tien con lai can thanh toan
                lblTotalMoney_BookingR.Text = String.Format("{0:0,0} (VND)", ((sumMoneyRoomBehindTax + sumMoneyServiceRoomBehindTax) - BookingMoneyR));


                //------------------------------- Hoi truong ---------------------

                List<BookingHallUsedEN> aListBookingHallUsedEN = new List<BookingHallUsedEN>();
                aListBookingHallUsedEN = this.aNewPaymentEN.aListBookingHallUsed.Where(r => r.IndexSubPayment == this.IndexSub).OrderBy(r => r.HallSku).ToList();
                aListBookingHallUsedEN.Count();
                List<ServiceUsedEN> aListServicesHallEN = new List<ServiceUsedEN>();
                aListServicesHallEN = this.aNewPaymentEN.GetAllServiceUsedInHall().Where(r => r.IndexSubPayment == this.IndexSub).OrderBy(r => r.Sku).ToList();

                decimal? sumMoneyHallBeforeTax = aListBookingHallUsedEN.Sum(r => r.MoneyHallBeforeTax);

                decimal? sumMoneyHallBehindTax = aListBookingHallUsedEN.Sum(r => r.MoneyHall);
                decimal? sumMoneyServiceHallBehindTax = aListServicesHallEN.Sum(s => s.TotalMoney);
                decimal? sumMoneyServiceHallBeforeTax = aListServicesHallEN.Sum(s => s.TotalMoneyBeforeTax);

                decimal? bookingMoneyH = 0;

                //danh sach hoi truong
                this.DetailReportHall.DataSource = aListBookingHallUsedEN;
                colSkuHall.DataBindings.Add("Text", this.DetailReportHall.DataSource, "HallSku");
                colCreateDate.DataBindings.Add("Text", this.DetailReportHall.DataSource, "Date", "{0:dd/MM/yyyy}");
                colBookingHallCost.DataBindings.Add("Text", this.DetailReportHall.DataSource, "Cost", "{0:0,0}");
                colPercentTax.DataBindings.Add("Text", this.DetailReportHall.DataSource, "PercentTax");
                colPaymentMoneyHall.DataBindings.Add("Text", this.DetailReportHall.DataSource, "MoneyHall", "{0:0,0}");
                colMoneyHallBeforeTax.DataBindings.Add("Text", this.DetailReportHall.DataSource, "MoneyHallBeforeTax", "{0:0,0}");

                //danh sach dich vu su dung
                this.DetailReportService.DataSource = aListServicesHallEN;
                colService_Sku.DataBindings.Add("Text", this.DetailReportService.DataSource, "Sku");
                colService_Name.DataBindings.Add("Text", this.DetailReportService.DataSource, "NameService");
                colService_Date.DataBindings.Add("Text", this.DetailReportService.DataSource, "DateUsed", "{0:dd/MM/yyyy}");
                colService_Quantity.DataBindings.Add("Text", this.DetailReportService.DataSource, "Quantity", "{0:0,0}");
                colService_Cost.DataBindings.Add("Text", this.DetailReportService.DataSource, "Cost", "{0:0,0}");
                colService_PercentTax.DataBindings.Add("Text", this.DetailReportService.DataSource, "Tax");
                colService_Money.DataBindings.Add("Text", this.DetailReportService.DataSource, "TotalMoney", "{0:0,0}");

                //tong tien hoi truong truoc thue
                lblTotalMoneyHallBeforeTax.Text = String.Format("{0:0,0} (VND)", sumMoneyHallBeforeTax);
                //tong tien hoi truong sau thue
                lblTotalMoneyHallBehindTax.Text = String.Format("{0:0,0} (VND)", sumMoneyHallBehindTax);

                //tong tien dich vu hoi truong truoc thue
                lblTotalMoneyService_BookingHBeforeTax.Text = String.Format("{0:0,0} (VND)", sumMoneyServiceHallBeforeTax);
                //tong tien dich vu hoi truong sau thue
                lblTotalMoneyServices_BookingHBehindTax.Text = String.Format("{0:0,0} (VND)", sumMoneyServiceHallBehindTax);


                //tong tien bookingh
                lblTotalMoneyBookingHBeforeTax.Text = String.Format("{0:0,0} (VND)", (sumMoneyHallBeforeTax + sumMoneyServiceHallBeforeTax));
                lblTotalMoneyBookingHBehindTax.Text = String.Format("{0:0,0} (VND)", (sumMoneyHallBehindTax + sumMoneyServiceHallBehindTax));
                lblBookingMoney_BookingH.Text = String.Format("{0:0,0} (VND)", bookingMoneyH);
                lblTotalBookingH.Text = String.Format("{0:0,0} (VND)", ((sumMoneyHallBehindTax + sumMoneyServiceHallBehindTax) - bookingMoneyH));



                //Tong tien hoa don can thanh toan
                decimal? beforTax =sumMoneyRoomBeforeTax + sumMoneyServiceRoomBeforeTax + sumMoneyHallBeforeTax + sumMoneyServiceHallBeforeTax ;
                decimal? behindTax = sumMoneyRoomBehindTax + sumMoneyServiceRoomBehindTax + sumMoneyHallBehindTax + sumMoneyServiceHallBehindTax;
                decimal? bookingMoney = BookingMoneyR + bookingMoneyH;

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

    }
}
