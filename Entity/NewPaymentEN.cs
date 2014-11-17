using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data.Entity.Migrations;


namespace Entity
{
   public class NewPaymentEN
    {
       public int? IDBookingH { get; set; }
       public int? IDBookingR { get; set; }
       public List<BookingHallUsedEN> aListBookingHallUsed = new List<BookingHallUsedEN>();
       public List<BookingRoomUsedEN> aListBookingRoomUsed = new List<BookingRoomUsedEN>();
       public Nullable<DateTime> CreatedDate_BookingR { get; set; }
       public Nullable<DateTime> CreatedDate_BookingH { get; set; }
       public int? CustomerType { get; set; }
       public int? PayMenthod { get; set; }
       public int? StatusPay { get; set; }
       public decimal? BookingHMoney { get; set; }
       public decimal? BookingRMoney { get; set; }
       public int? Status_BookingR { get; set; }
       public int? Status_BookingH { get; set; }
       public int? IDCustomer { get; set; }
       public string NameCustomer { get; set; }
       public int? IDSystemUser { get; set; }
       public string NameSystemUser { get; set; }
       public int? IDCustomerGroup { get; set; }
       public string NameCustomerGroup { get; set; }
       public int? IDCompany { get; set; }      
       public string NameCompany { get; set; }
       public string TaxNumberCodeCompany { get; set; }
       public string AddressCompany { get; set; }
       public string InvoiceNumber { get; set; }
       public DateTime? AcceptDate { get; set; }
      

       public List<int> ListIndex = new List<int>();
       public Nullable<DateTime> InvoiceDate { get; set; } // ngay tren hoa don do, ngay chot doanh thu

       public decimal? GetMoneyRooms()
       {
           decimal? MoneyRooms = 0;
           foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
           {
               MoneyRooms = MoneyRooms + item.GetMoneyRoom();
           }
           return MoneyRooms;
       }
       public decimal? GetMoneyHalls()
       {
           decimal? MoneyHalls = 0;
           foreach (BookingHallUsedEN item in this.aListBookingHallUsed)
           {
               MoneyHalls = MoneyHalls + item.GetMoneyHall();
           }
           return MoneyHalls;
       }
       

       //Hiennv  03/11/2014
       public decimal? GetMoneyTax(decimal? TotalMoney , decimal? PercentTax)
       {
           return(TotalMoney * PercentTax) / 100;
       }


       public decimal? GetMoneyRoomsBeforeTax()
       {
           decimal? MoneyRoomsBeforeTax = 0;
           foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
           {
               MoneyRoomsBeforeTax = MoneyRoomsBeforeTax + item.GetMoneyRoomBeforeTax();
           }
           return MoneyRoomsBeforeTax;
       }
       public decimal? GetMoneyHallsBeforeTax()
       {
           decimal? MoneyHallsBeforeTax = 0;
           foreach (BookingHallUsedEN item in this.aListBookingHallUsed)
           {
               MoneyHallsBeforeTax = MoneyHallsBeforeTax + item.GetMoneyHallBeforeTax();
           }
           return MoneyHallsBeforeTax;
       }
       public decimal? GetOnlyMoneyRooms()
       {
           decimal? MoneyRooms = 0;
           foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
           {
               MoneyRooms = MoneyRooms + item.GetOnlyMoneyRoom();
           }
           return MoneyRooms;
       }
       public decimal? GetOnlyMoneyRoomsBeforeTax()
       {
           decimal? MoneyRooms = 0;
           foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
           {
               MoneyRooms = MoneyRooms + item.GetOnlyMoneyRoomBeforeTax();
           }
           return MoneyRooms;
       }
       public decimal? GetOnlyMoneyHalls()
       {
           decimal? MoneyHalls = 0;
           foreach (BookingHallUsedEN item in this.aListBookingHallUsed)
           {
               MoneyHalls = MoneyHalls + item.GetOnlyMoneyHall();
           }
           return MoneyHalls;
       }
       public decimal? GetOnlyMoneyHallsBeforeTax()
       {
           decimal? MoneyHalls = 0;
           foreach (BookingHallUsedEN item in this.aListBookingHallUsed)
           {
               MoneyHalls = MoneyHalls + item.GetOnlyMoneyHallBeforeTax();
           }
           return MoneyHalls;
       }
       public decimal? GetTotalMoney()
       {
           if (this.GetMoneyHalls() != 0)
           {
               decimal? TotalMoney = this.GetMoneyHalls() + this.GetMoneyRooms();
               return TotalMoney;
           }
           else
           {
               return this.GetMoneyRooms();
           }
       }
       public decimal? GetTotalMoneyBeforeTax()
       {
           if (this.GetMoneyHallsBeforeTax() != 0)
           {
               decimal? TotalMoneyBeforeTax = this.GetMoneyHallsBeforeTax() + this.GetMoneyRoomsBeforeTax();
               return TotalMoneyBeforeTax;
           }
           else
           {
               return this.GetMoneyRoomsBeforeTax();
           }
       }
       public decimal? GetMoneyARoom(int IDBookingRoom)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               return this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].GetOnlyMoneyRoom();
           }
           return 0;
       }
       public decimal? GetMoneyAHall(int IDBookingHall)
       {
           if (this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList().Count > 0)
           {
               return this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].GetOnlyMoneyHall();
           }
           return 0;
       }
       public void PaymentRoom()
       {
           if (this.IDBookingH != null)
           {
               foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
               {

                   foreach (ServiceUsedEN item1 in item.ListServiceUsed)
                   {
                       item1.StatusPay = 8;
                   }
                   item.Status = 8;
                   item.CheckOutActual = DateTime.Now;
                   item.Save();
               }
           }
           else
           {
               DatabaseDA aDatabaseDA = new DatabaseDA();             
               if (aDatabaseDA.BookingRs.Where(a => a.ID == IDBookingR).ToList().Count > 0)
               {
                   BookingRs aTemp = aDatabaseDA.BookingRs.Where(a => a.ID == IDBookingR).ToList()[0];
                   aTemp.DatePay = DateTime.Now;
                   aTemp.Status = 8;
                   aTemp.InvoiceDate = this.InvoiceDate;
                   aTemp.InvoiceNumber = this.InvoiceNumber;
                   aTemp.BookingMoney = this.BookingRMoney;
                   aDatabaseDA.BookingRs.AddOrUpdate(aTemp);
                   aDatabaseDA.SaveChanges();
                   foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
                   {

                       foreach (ServiceUsedEN item1 in item.ListServiceUsed)
                       {
                           item1.StatusPay = 8;
                       }
                       item.Status = 8;
                       item.CheckOutActual = DateTime.Now;
                       item.Save();
                   }
               }             

           }
              
           
       }
       public void PaymentHall()
       {
           DatabaseDA aDatabaseDA = new DatabaseDA();
           if (aDatabaseDA.BookingHs.Where(a => a.ID == IDBookingH).ToList().Count > 0)
           {
               BookingHs aTemp = aDatabaseDA.BookingHs.Where(a => a.ID == IDBookingH).ToList()[0];
               aTemp.CreatedDate = this.CreatedDate_BookingH;
               aTemp.Status = 8;
               aTemp.BookingMoney = this.BookingHMoney;
               aTemp.InvoiceDate = this.InvoiceDate;
               aTemp.InvoiceNumber = this.InvoiceNumber;
               aTemp.DatePay = DateTime.Now;
               aDatabaseDA.BookingHs.AddOrUpdate(aTemp);
               aDatabaseDA.SaveChanges();
               foreach (BookingHallUsedEN item in this.aListBookingHallUsed)
               {

                   foreach (ServiceUsedEN item1 in item.aListServiceUsed)
                   {
                       item1.StatusPay = 8;
                   }
                   item.Status = 8;
                   item.Save();
               }
           }
        
       }
       public void PaymentTotal()
       {
           if (this.aListBookingHallUsed.Count > 0)
           {
               this.PaymentHall();
               DatabaseDA aDatabaseDA = new DatabaseDA();
               if (aDatabaseDA.BookingRs.Where(a => a.ID == IDBookingR).ToList().Count > 0)
               {
                   BookingRs aTemp = aDatabaseDA.BookingRs.Where(a => a.ID == IDBookingR).ToList()[0];
                   aTemp.DatePay = DateTime.Now;
                   aTemp.Status = 8;
                   aTemp.InvoiceDate = this.InvoiceDate;
                   aTemp.InvoiceNumber = this.InvoiceNumber;
                   aTemp.BookingMoney = this.BookingRMoney;
                   aDatabaseDA.BookingRs.AddOrUpdate(aTemp);
                   aDatabaseDA.SaveChanges();
                   foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
                   {

                       foreach (ServiceUsedEN item1 in item.ListServiceUsed)
                       {
                           item1.StatusPay = 8;
                       }
                       item.Status = 8;
                       item.CheckOutActual = DateTime.Now;
                       item.Save();
                   }
               }
           }
           else
           {
               DatabaseDA aDatabaseDA = new DatabaseDA();
               if (aDatabaseDA.BookingRs.Where(a => a.ID == IDBookingR).ToList().Count > 0)
               {
                   BookingRs aTemp = aDatabaseDA.BookingRs.Where(a => a.ID == IDBookingR).ToList()[0];
                   aTemp.DatePay = DateTime.Now;
                   aTemp.Status = 8;
                   aTemp.InvoiceDate = this.InvoiceDate;
                   aTemp.InvoiceNumber = this.InvoiceNumber;
                   aTemp.BookingMoney = this.BookingRMoney;
                   aDatabaseDA.BookingRs.AddOrUpdate(aTemp);
                   aDatabaseDA.SaveChanges();
                   foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
                   {

                       foreach (ServiceUsedEN item1 in item.ListServiceUsed)
                       {
                           item1.StatusPay = 8;
                       }
                       item.Status = 8;
                       item.CheckOutActual = DateTime.Now;
                       item.Save();
                   }
               }
           }
       }
       public void ChangeCostRoom(int IDBookingRoom, decimal Cost)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].Cost = Cost;
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].TotalMoney = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].GetMoneyRoom();
           }
          
       }
       public void ChangeCostHall(int IDBookingHall, decimal Cost)
       {
           if (this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList().Count > 0)
           {
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].Cost = Cost;
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].TotalMoney = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].GetMoneyHall();
           }
       }
       public void ChangeCheckInActual(int IDBookingRoom, DateTime CheckInActual)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].CheckInActual = CheckInActual;
           }
       }
       public void ChangeCheckOutActual(int IDBookingRoom, DateTime CheckOutActual)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].CheckOutActual = CheckOutActual;
           }
       }
       public void ChangeTimeInUsed(int IDBookingRoom, decimal TimeUsed)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].TimeInUse = TimeUsed;
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].TotalMoney = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].GetMoneyRoom();
           }
       }
       public void ChangeCostServiceUsedInRoom(int IDBookingRoom, int IDBookingRoomService, decimal Cost)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].SetCostServiceUsed(IDBookingRoomService, Cost);
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].TotalMoney = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].GetMoneyRoom();

           }
       }
       public void ChangeQuantityServiceUsedInRoom(int IDBookingRoom, int IDBookingRoomService, double Quantity)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].SetQuantityServiceUsed(IDBookingRoomService, Quantity);
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].TotalMoney = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].GetMoneyRoom();

           }
       }
       public void SetCheckInEarly(int IDBookingRoom,bool CheckInEarly)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].IsCheckInEarly = CheckInEarly;
           }
       }
       public void SetCheckOutLate(int IDBookingRoom, bool CheckOutLate)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].IsCheckOutLate = CheckOutLate;
           }
       }
       public void ChangeTaxServiceInRoom(int IDBookingRoom, int IDBookingRoomService, double Tax)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].ChangeTaxService(IDBookingRoomService, Tax);
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].TotalMoney = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].GetMoneyRoom();

           }
       }
       public void ChangePercentTaxRoom(int IDBookingRoom, double Tax)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].PercentTax = Tax;
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].TotalMoney = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].GetMoneyRoom();

           }
       }
       public void ChangePercentTaxHall(int IDBookingHall, double Tax)
       {
           if (this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList().Count > 0)
           {
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].PercentTax = Tax;
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].TotalMoney = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].GetMoneyHall();

           }
       }
       public void ChangeTaxServiceInHall(int IDBookingHall, int IDBookingHallService, double Tax)
       {
           if (this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList().Count > 0)
           {
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].ChangeTaxService(IDBookingHallService, Tax);
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].TotalMoney = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].GetMoneyHall();
           }
       }
       public void ChangeCostServiceUsedInHall(int IDBookingHall, int IDBookingHallService, decimal Cost)
       {
           if (this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList().Count > 0)
           {
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].SetCostServiceUsed(IDBookingHallService, Cost);
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].TotalMoney = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].GetMoneyHall();

           }
       }
       public void ChangeQuantityServiceUsedInHall(int IDBookingHall, int IDBookingHallService, double Quantity)
       {
           if (this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList().Count > 0)
           {
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].SetQuantityServiceUsed(IDBookingHallService, Quantity);
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].TotalMoney = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].GetMoneyHall();

           }
       }
       public List<ServiceUsedEN> GetListServiceUsedPaidInRoom(int IDBookingRoom)
       {
           BookingRoomUsedEN aTemp = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0];
           return aTemp.ListServiceUsed.Where(a => a.IsPaidService() == true).ToList();
       }
       public List<ServiceUsedEN> GetListServiceUsedUnPaidInRoom(int IDBookingRoom)
       {
           BookingRoomUsedEN aTemp = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0];
           return aTemp.ListServiceUsed.Where(a => a.IsPaidService() == false).ToList();
       }
       public List<ServiceUsedEN> GetListServiceUsedPaidInHall(int IDBookingHall)
       {
           BookingHallUsedEN aTemp = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0];
           return aTemp.aListServiceUsed.Where(a => a.IsPaidService() == true).ToList();
       }
       public List<ServiceUsedEN> GetListServiceUsedUnPaidInHall(int IDBookingHall)
       {
           BookingHallUsedEN aTemp = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0];
           return aTemp.aListServiceUsed.Where(a => a.IsPaidService() == false).ToList();
       }
       public decimal? GetTotalMoneyServiceUsedInRooms()
       {
           decimal? TotalMoneyService = 0;
           foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
           {
               TotalMoneyService = TotalMoneyService + item.GetMoneyListServiceUsed();           
           }
           return TotalMoneyService;
       }
       public decimal? GetTotalMoneyServiceUsedInRoomsBeforeTax()
       {
           decimal? TotalMoneyService = 0;
           foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
           {
               TotalMoneyService = TotalMoneyService + item.GetMoneyListServiceUsedBeforeTax();
           }
           return TotalMoneyService;
       }
       public decimal? GetTotalMoneyServiceUsedInHalls()
       {
           decimal? TotalMoneyService = 0;
           foreach (BookingHallUsedEN item in this.aListBookingHallUsed)
           {
               TotalMoneyService = TotalMoneyService + item.GetMoneyListServiceUsed();
           }
           return TotalMoneyService;
       }
       public decimal? GetTotalMoneyServiceUsedInHallsBeforeTax()
       {
           decimal? TotalMoneyService = 0;
           foreach (BookingHallUsedEN item in this.aListBookingHallUsed)
           {
               TotalMoneyService = TotalMoneyService + item.GetMoneyListServiceUsedBeforeTax();
           }
           return TotalMoneyService;
       }
       public decimal? GetMoneyListServiceUsedInARoom(int IDBookingRoom)
       {
           BookingRoomUsedEN aTemp = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0];
           return aTemp.GetMoneyListServiceUsed();
       }
       public decimal? GetMoneyListServiceUsedInARoomBeforeTax(int IDBookingRoom)
       {
           BookingRoomUsedEN aTemp = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0];
           return aTemp.GetMoneyListServiceUsedBeforeTax();
       }
       public decimal? GetMoneyListServiceUsedInAHall(int IDBookingHall)
       {
           BookingHallUsedEN aTemp = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0];
           return aTemp.GetMoneyListServiceUsed();
       }
       public decimal? GetMoneyListServiceUsedInAHallBeforeTax(int IDBookingHall)
       {
           BookingHallUsedEN aTemp = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0];
           return aTemp.GetMoneyListServiceUsedBeforeTax();
       }
       public List<ServiceUsedEN> GetListServiceUsedInRoom(int IDBookingRoom)
       {
           BookingRoomUsedEN aTemp = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0];
           return aTemp.ListServiceUsed;
       }
       public List<ServiceUsedEN> GetListServiceUsedInHall(int IDBookingHall)
       {
           BookingHallUsedEN aTemp = this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0];
           return aTemp.aListServiceUsed;
       }
       public decimal? GetCostRoom(int IDBookingRoom)
       {
           BookingRoomUsedEN aTemp = this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0];
           return aTemp.Cost;
       }
      
       public List<ServiceUsedEN> GetAllServiceUsedInRoom()
       {
           List<ServiceUsedEN> aListServiceUsed = new List<ServiceUsedEN>();
           
           foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
           {
               foreach (ServiceUsedEN item1 in item.ListServiceUsed)
               {
                   aListServiceUsed.Add(item1);
               }
           }
           return aListServiceUsed;
       }

       public List<ServiceUsedEN> GetAllServiceUsedInHall()
       {
           List<ServiceUsedEN> aListServiceUsed = new List<ServiceUsedEN>();

           foreach (BookingHallUsedEN item in this.aListBookingHallUsed)
           {
               foreach (ServiceUsedEN item1 in item.aListServiceUsed)
               {
                   aListServiceUsed.Add(item1);
               }
           }
           return aListServiceUsed;
       }
       public void Save()
       {
           try
           {
               DatabaseDA aDatabaseDA = new DatabaseDA();
               if (IDBookingR != null)
               {
                   BookingRs aTemp = aDatabaseDA.BookingRs.Where(a => a.ID == IDBookingR).ToList()[0];
                   if (aTemp != null)
                   {
                       aTemp.CreatedDate = this.CreatedDate_BookingR;
                       aTemp.ID = Convert.ToInt32(this.IDBookingR);
                       aTemp.Status = this.Status_BookingR;                      
                       aTemp.InvoiceNumber = this.InvoiceNumber;
                       aTemp.AcceptDate = this.AcceptDate;
                       aTemp.InvoiceDate = this.InvoiceDate;                      
                       aTemp.BookingMoney = this.BookingRMoney;
                       aTemp.PayMenthod = this.PayMenthod;

                       aDatabaseDA.BookingRs.AddOrUpdate(aTemp);
                       aDatabaseDA.SaveChanges();
                   }
                   foreach (BookingRoomUsedEN item in this.aListBookingRoomUsed)
                   {
                       item.Save();
                   }
               }
               else if (IDBookingH != null)
               {

                   BookingHs aTemp = aDatabaseDA.BookingHs.Where(a => a.ID == IDBookingH).ToList()[0];
                   if (aTemp != null)
                   {
                       aTemp.CreatedDate = this.CreatedDate_BookingH;
                       aTemp.ID = Convert.ToInt32(this.IDBookingH);
                       aTemp.Status = this.Status_BookingH;
                       aTemp.BookingMoney = this.BookingHMoney;
                       aTemp.InvoiceNumber = this.InvoiceNumber;
                       aTemp.AcceptDate = this.AcceptDate;
                       aTemp.InvoiceDate = this.InvoiceDate;
                       aTemp.PayMenthod = this.PayMenthod;
                       aDatabaseDA.BookingHs.AddOrUpdate(aTemp);
                       aDatabaseDA.SaveChanges();
                   }
                   foreach (BookingHallUsedEN item in this.aListBookingHallUsed)
                   {
                       item.Save();
                   }
               }
               // Luu thong tin cong ty
               Companies aCompany = aDatabaseDA.Companies.Where(a => a.ID == this.IDCompany).ToList()[0];
               aCompany.TaxNumberCode = this.TaxNumberCodeCompany;
               aCompany.Address = this.AddressCompany;
               aCompany.ID = Convert.ToInt32(this.IDCompany);
               aDatabaseDA.Companies.AddOrUpdate(aCompany);
               aDatabaseDA.SaveChanges();
               // Luu thong tin bookingRoom + Hall
             
          
           }
           catch (Exception ex)
           {
               throw new Exception(string.Format("BookingRoomUsedEN.Save :" + ex.Message.ToString()));
           }
       }

       public void ChangeIndexSubPaymentServiceRoom(int IDBookingRoom, int IDBookingRoomService, int Index)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].ChangeIndexSubPaymentService(IDBookingRoomService, Index);

           }
       }
       public void ChangeIndexSubPaymentServiceHall(int IDBookingHall, int IDBookingHallService, int Index)
       {
           if (this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList().Count > 0)
           {
               this.aListBookingHallUsed.Where(a => a.ID == IDBookingHall).ToList()[0].ChangeIndexSubPaymentService(IDBookingHallService, Index);

           }
       }
       public void ChangeInvoiceDate(DateTime InvoiceDate)
       {
           this.InvoiceDate = InvoiceDate;
           if (this.aListBookingRoomUsed.Count > 0)
           {
               foreach (BookingRoomUsedEN aBookingRoom in this.aListBookingRoomUsed)
               {
                   if (aBookingRoom.IndexSubPayment == null)
                   {
                       aBookingRoom.InvoiceDate = InvoiceDate;
                   }
                   if (aBookingRoom.ListServiceUsed.Count > 0)
                   {
                       foreach (ServiceUsedEN aTemp in aBookingRoom.ListServiceUsed)
                       {
                           if (aTemp.IndexSubPayment == null)
                           {
                               aTemp.InvoiceDate = InvoiceDate;
                           }
                       }
                   }

               }
           }
           else if (this.aListBookingHallUsed.Count > 0)
           {
               foreach (BookingHallUsedEN aBookingHall in this.aListBookingHallUsed)
               {
                   if (aBookingHall.IndexSubPayment == null)
                   {
                       aBookingHall.InvoiceDate = InvoiceDate;
                   }
                   if (aBookingHall.aListServiceUsed.Count > 0)
                   {
                       foreach (ServiceUsedEN aTemp in aBookingHall.aListServiceUsed)
                       {
                           if (aTemp.IndexSubPayment == null)
                           {
                               aTemp.InvoiceDate = InvoiceDate;
                           }
                       }
                   }
               }
           }
       }
       public void ChangeInvoiceNumber(string InvoiNumber)
       {
           this.InvoiceNumber = InvoiNumber;
           if (this.aListBookingRoomUsed.Count > 0)
           {
               foreach (BookingRoomUsedEN aBookingRoom in this.aListBookingRoomUsed)
               {
                   if (aBookingRoom.IndexSubPayment == null)
                   {
                       aBookingRoom.InvoiceNumber = InvoiNumber;
                   }
                   if (aBookingRoom.ListServiceUsed.Count > 0)
                   {
                       foreach (ServiceUsedEN aTemp in aBookingRoom.ListServiceUsed)
                       {
                           if (aTemp.IndexSubPayment == null)
                           {
                               aTemp.InvoiceNumber = InvoiNumber;
                           }
                       }
                   }
               }
           }
           else if (this.aListBookingHallUsed.Count > 0)
           {
               foreach (BookingHallUsedEN aBookingHall in this.aListBookingHallUsed)
               {
                   if (aBookingHall.IndexSubPayment == null)
                   {
                       aBookingHall.InvoiceNumber = InvoiNumber;
                   }
                   if (aBookingHall.aListServiceUsed.Count > 0)
                   {
                       foreach (ServiceUsedEN aTemp in aBookingHall.aListServiceUsed)
                       {
                           if (aTemp.IndexSubPayment == null)
                           {
                               aTemp.InvoiceDate = InvoiceDate;
                           }
                       }
                   }
               }
           }
       }
       public void ChangeAcceptDate(DateTime AcceptDate)
       {
           this.AcceptDate = AcceptDate;
           if (this.aListBookingRoomUsed.Count > 0)
           {
               foreach (BookingRoomUsedEN aBookingRoom in this.aListBookingRoomUsed)
               {
                   if (aBookingRoom.IndexSubPayment == null)
                   {
                       aBookingRoom.AcceptDate = AcceptDate;
                   }
                   if (aBookingRoom.ListServiceUsed.Count > 0)
                   {
                       foreach (ServiceUsedEN aTemp in aBookingRoom.ListServiceUsed)
                       {
                           if (aTemp.IndexSubPayment == null)
                           {
                               aTemp.AcceptDate = AcceptDate;
                           }
                       }
                   }

               }
           }
           else if (this.aListBookingHallUsed.Count > 0)
           {
               foreach (BookingHallUsedEN aBookingHall in this.aListBookingHallUsed)
               {
                   if (aBookingHall.IndexSubPayment == null)
                   {
                       aBookingHall.AcceptDate = AcceptDate;
                   }
                   if (aBookingHall.aListServiceUsed.Count > 0)
                   {
                       foreach (ServiceUsedEN aTemp in aBookingHall.aListServiceUsed)
                       {
                           if (aTemp.IndexSubPayment == null)
                           {
                               aTemp.AcceptDate = AcceptDate;
                           }
                       }
                   }
               }
           }
       }
       public void ChangeTypeBookingRoom(int IDBookingRoom, bool CheckIn, bool CheckOut)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
                if (CheckIn == true && CheckOut == true)
                {
                    this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].Type = 3;
                    
                }
                else if (CheckIn == false && CheckOut == false)
                {
                    this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].Type = 0;
                    
                }
                else if (CheckIn == false && CheckOut == true)
                {
                    this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].Type = 1;
                
                }
                else if (CheckIn == true && CheckOut == false)
                {
                    this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].Type = 2;
                }
           
           }
       }
       public void ChangePriceType(int IDBookingRoom, string PriceType)
       {
           if (this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList().Count > 0)
           {
               this.aListBookingRoomUsed.Where(a => a.ID == IDBookingRoom).ToList()[0].PriceType = PriceType;

           }
       }

    }

}
