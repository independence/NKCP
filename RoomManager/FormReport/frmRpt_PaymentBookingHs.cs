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
    public partial class frmRpt_PaymentBookingHs : DevExpress.XtraReports.UI.XtraReport
    {
        private NewPaymentEN aNewPaymentEN = new NewPaymentEN();
        List<ServiceGroupEN> aListServicesGroupEN = new List<ServiceGroupEN>();
        List<ServiceUsedEN> aListServiceUsed = new List<ServiceUsedEN>();
        List<int> aListIDServicesGroup = new List<int>();

        public frmRpt_PaymentBookingHs(NewPaymentEN aNewPaymentEN)
        {
            InitializeComponent();
            this.aNewPaymentEN = aNewPaymentEN;
            try
            {
                //------------------------------- Hoi truong ---------------------

                lblNumberVote.Text = Convert.ToString(this.aNewPaymentEN.IDBookingH);
                lblIDBookingH.Text = Convert.ToString(this.aNewPaymentEN.IDBookingH);
                lblNameCustomer.Text = this.aNewPaymentEN.NameCustomer;
                lblGroup.Text = this.aNewPaymentEN.NameCustomerGroup;
                lblCompany.Text = this.aNewPaymentEN.NameCompany;
                lblTaxNumberCode.Text = this.aNewPaymentEN.AddressCompany;

                int day = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                lblDayMonthYear.Text = "Hà nội , ngày " + day.ToString() + " tháng " + month.ToString() + " năm " + year.ToString();

                aListServiceUsed = this.aNewPaymentEN.GetAllServiceUsedInRoom();
                //Lấy List< IDServiceGroup>
                List<int> aTemp = new List<int>();
                int IDServiceGroup;
                foreach (ServiceUsedEN item in aListServiceUsed)
                {
                    IDServiceGroup = new int();
                    IDServiceGroup = item.IDServiceGroup;
                    aTemp.Add(IDServiceGroup);
                }
                aListIDServicesGroup = aTemp.Distinct().ToList();


                ServiceGroupEN aServicesGroupEN;
                ServiceGroupsBO aServiceGroupsBO = new ServiceGroupsBO();

                foreach (int item in aListIDServicesGroup)
                {
                    aServicesGroupEN = new ServiceGroupEN();
                    aServicesGroupEN.IDServiceGroup = item;
                    aServicesGroupEN.TotalMoneyBeforeTax = this.GetTotalMoneyServiceGroupBeforeTax(item);
                    aServicesGroupEN.TotalMoneyAfterTax = this.GetTotalMoneyServiceGroupAfterTax(item);
                    aServicesGroupEN.ServiceGroupName = aServiceGroupsBO.Sel_ByID(item).Name;
                    aListServicesGroupEN.Add(aServicesGroupEN);
                }

                //danh sach hoi truong
                this.DetailReportHall.DataSource = this.aNewPaymentEN.aListBookingHallUsed;
                colSkuHall.DataBindings.Add("Text", this.DetailReportHall.DataSource, "HallSku");
                colCreateDate.DataBindings.Add("Text", this.DetailReportHall.DataSource, "Date", "{0:dd/MM/yyyy}");
                colBookingHallCost.DataBindings.Add("Text", this.DetailReportHall.DataSource, "Cost", "{0:0,0}");
                colPercentTax.DataBindings.Add("Text", this.DetailReportHall.DataSource, "PercentTax");
                colPaymentMoneyHall.DataBindings.Add("Text", this.DetailReportHall.DataSource, "MoneyHall", "{0:0,0}");
                colMoneyHallBeforeTax.DataBindings.Add("Text", this.DetailReportHall.DataSource, "MoneyHallBeforeTax", "{0:0,0}");
                
                //danh sach dich vu su dung
                this.DetailReportService.DataSource = aListServicesGroupEN;
                colNamService.DataBindings.Add("Text", this.DetailReportService.DataSource, "ServiceGroupName");
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


            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        public decimal? GetTotalMoneyServiceGroupBeforeTax(int IDServiceGroup)
        {
            decimal? TotalMoneyServiceGroupBeforeTax = 0;
            List<ServiceUsedEN> aTemp = aListServiceUsed.Where(a => a.IDServiceGroup == IDServiceGroup).ToList();
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
            List<ServiceUsedEN> aTemp = aListServiceUsed.Where(a => a.IDServiceGroup == IDServiceGroup).ToList();
            foreach (ServiceUsedEN item in aTemp)
            {
                decimal? cost = item.GetMoneyService();
                TotalMoneyServiceGroupAfterTax = TotalMoneyServiceGroupAfterTax + cost;
            }
            return TotalMoneyServiceGroupAfterTax;
        }
    }



}
