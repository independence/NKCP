//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw__BookingHInfo__BookingHs_Customers_Companies_SystemUsers
    {
        public int BookingHs_ID { get; set; }
        public Nullable<System.DateTime> BookingHs_CreatedDate { get; set; }
        public Nullable<int> BookingHs_CustomerType { get; set; }
        public Nullable<int> BookingHs_BookingType { get; set; }
        public string BookingHs_Note { get; set; }
        public int BookingHs_IDCustomer { get; set; }
        public int BookingHs_IDSystemUser { get; set; }
        public Nullable<int> BookingHs_PayMenthod { get; set; }
        public Nullable<int> BookingHs_StatusPay { get; set; }
        public Nullable<decimal> BookingHs_BookingMoney { get; set; }
        public Nullable<decimal> BookingHs_ExchangeRate { get; set; }
        public Nullable<int> BookingHs_Status { get; set; }
        public Nullable<int> BookingHs_Type { get; set; }
        public Nullable<bool> BookingHs_Disable { get; set; }
        public Nullable<int> BookingHs_Level { get; set; }
        public string BookingHs_Subject { get; set; }
        public string BookingHs_Description { get; set; }
        public Nullable<System.DateTime> BookingHs_DatePay { get; set; }
        public int BookingHs_IDCustomerGroup { get; set; }
        public string SystemUsers_Username { get; set; }
        public Nullable<int> Customers_ID { get; set; }
        public string Customers_Name { get; set; }
        public string Customers_Identifier1 { get; set; }
        public string Customers_Identifier2 { get; set; }
        public string Customers_Identifier3 { get; set; }
        public Nullable<int> Customers_Type { get; set; }
        public Nullable<int> Companies_ID { get; set; }
        public string Companies_Name { get; set; }
        public Nullable<int> Companies_Type { get; set; }
        public Nullable<int> Companies_Status { get; set; }
        public Nullable<int> CustomerGroups_ID { get; set; }
        public string CustomerGroups_Name { get; set; }
        public Nullable<int> CustomerGroups_Type { get; set; }
        public Nullable<int> CustomerGroups_Status { get; set; }
        public string Customers_Gender { get; set; }
        public Nullable<int> Customers_Citizen { get; set; }
    }
}
