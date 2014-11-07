using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Entity;
using DataAccess;
using BussinessLogic;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
namespace RoomManager
{
    public partial class frmRpt_SplitPayment_BookingHs : DevExpress.XtraReports.UI.XtraReport
    {
        private NewPaymentEN aNewPaymentEN = new NewPaymentEN();
        private int IndexSub = 0;

        public frmRpt_SplitPayment_BookingHs(NewPaymentEN aNewPaymentEN, int IndexSub)
        {
            InitializeComponent();
            this.aNewPaymentEN = aNewPaymentEN;
            this.IndexSub = IndexSub;
            try
            {


                lblNumberVote.Text = Convert.ToString(this.aNewPaymentEN.IDBookingH);
                lblIIDBookingH.Text = Convert.ToString(this.aNewPaymentEN.IDBookingH);
                lblNameCustomer.Text = this.aNewPaymentEN.NameCustomer;
                lblGroup.Text = this.aNewPaymentEN.NameCustomerGroup;
                lblCompany.Text = this.aNewPaymentEN.NameCompany;
                lblTaxNumberCode.Text = this.aNewPaymentEN.AddressCompany;

                int day = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                lblDayMonthYear.Text = "Hà nội , ngày " + day.ToString() + " tháng " + month.ToString() + " năm " + year.ToString();

                //------------------------------- Hoi truong ---------------------

                List<BookingHallUsedEN> aListBookingHallUsedEN = new List<BookingHallUsedEN>();
                aListBookingHallUsedEN = this.aNewPaymentEN.aListBookingHallUsed.Where(r => r.IndexSubPayment == this.IndexSub).OrderBy(r => r.HallSku).ToList();
                aListBookingHallUsedEN.Count();
                List<ServiceUsedEN> aListServicesEN = new List<ServiceUsedEN>();
                aListServicesEN = this.aNewPaymentEN.GetAllServiceUsedInHall().Where(r => r.IndexSubPayment == this.IndexSub).OrderBy(r => r.Sku).ToList();

                decimal? sumMoneyHallBeforeTax = aListBookingHallUsedEN.Sum(r => r.MoneyHallBeforeTax);

                decimal? sumMoneyHallBehindTax = aListBookingHallUsedEN.Sum(r => r.MoneyHall);
                decimal? sumMoneyServiceHallBehindTax = aListServicesEN.Sum(s => s.TotalMoney);
                decimal? sumMoneyServiceHallBeforeTax = aListServicesEN.Sum(s => s.TotalMoneyBeforeTax);

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
                this.DetailReportService.DataSource = aListServicesEN;
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


            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
    }
}

