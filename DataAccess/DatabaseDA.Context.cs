﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class DatabaseDA : DbContext
    {
        public DatabaseDA()
            : base("name=DatabaseDA")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Configs> Configs { get; set; }
        public DbSet<CustomerGroups> CustomerGroups { get; set; }
        public DbSet<CustomerGroups_Customers> CustomerGroups_Customers { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<PermitDetails> PermitDetails { get; set; }
        public DbSet<Permits> Permits { get; set; }
        public DbSet<Permits_SystemUsers> Permits_SystemUsers { get; set; }
        public DbSet<ServiceGroups> ServiceGroups { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<SystemUsers> SystemUsers { get; set; }
        public DbSet<Permits_SystemUsers1> Permits_SystemUsers1 { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<SystemUserExts> SystemUserExts { get; set; }
        public DbSet<BookingHalls> BookingHalls { get; set; }
        public DbSet<BookingHalls_Services> BookingHalls_Services { get; set; }
        public DbSet<BookingHs> BookingHs { get; set; }
        public DbSet<Foods> Foods { get; set; }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Halls> Halls { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Menus_Foods> Menus_Foods { get; set; }
        public DbSet<Allowances> Allowances { get; set; }
        public DbSet<AlternateMissions> AlternateMissions { get; set; }
        public DbSet<AuditHistories> AuditHistories { get; set; }
        public DbSet<CalculatorSalaries> CalculatorSalaries { get; set; }
        public DbSet<Certificates> Certificates { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Contracts_Allowances> Contracts_Allowances { get; set; }
        public DbSet<Designations> Designations { get; set; }
        public DbSet<Divisions> Divisions { get; set; }
        public DbSet<DocumentSystemUsers> DocumentSystemUsers { get; set; }
        public DbSet<FamilyMembers> FamilyMembers { get; set; }
        public DbSet<GroupTableSalaries> GroupTableSalaries { get; set; }
        public DbSet<RewardAndPunishments> RewardAndPunishments { get; set; }
        public DbSet<SystemUserExts1> SystemUserExts1 { get; set; }
        public DbSet<SystemUsers_Certificates> SystemUsers_Certificates { get; set; }
        public DbSet<SystemUsers_Divisions> SystemUsers_Divisions { get; set; }
        public DbSet<TableSalaries> TableSalaries { get; set; }
        public DbSet<BookingRooms> BookingRooms { get; set; }
        public DbSet<BookingRooms_Services> BookingRooms_Services { get; set; }
        public DbSet<BookingRoomsMembers> BookingRoomsMembers { get; set; }
        public DbSet<BookingRs> BookingRs { get; set; }
        public DbSet<BookingRs_BookingHs> BookingRs_BookingHs { get; set; }
        public DbSet<CheckPoints> CheckPoints { get; set; }
        public DbSet<ExtraCosts> ExtraCosts { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<vw__BookingHalls_ServicesInfo__BookingHalls_BookingHallsServices_Services_ServiceGroups> vw__BookingHalls_ServicesInfo__BookingHalls_BookingHallsServices_Services_ServiceGroups { get; set; }
        public DbSet<vw__BookingHInfo__BookingHs_Customers_Companies_SystemUsers> vw__BookingHInfo__BookingHs_Customers_Companies_SystemUsers { get; set; }
        public DbSet<vw__BookingRInfo__BookingRooms_Rooms_SystemUsers_Customers_CustomerGroups> vw__BookingRInfo__BookingRooms_Rooms_SystemUsers_Customers_CustomerGroups { get; set; }
        public DbSet<vw__BookingRInfo__BookingRs_Customers_Companies_SystemUsers> vw__BookingRInfo__BookingRs_Customers_Companies_SystemUsers { get; set; }
        public DbSet<vw__BookingRoomInfo__BookingRs_BookingRooms_Rooms> vw__BookingRoomInfo__BookingRs_BookingRooms_Rooms { get; set; }
        public DbSet<vw__BookingRooms_ServicesInfo__BookingRooms_BookingRoomsServices_Services_ServiceGroups> vw__BookingRooms_ServicesInfo__BookingRooms_BookingRoomsServices_Services_ServiceGroups { get; set; }
        public DbSet<vw__CertificatesInfo__SystemUsers_Certificates> vw__CertificatesInfo__SystemUsers_Certificates { get; set; }
        public DbSet<vw__ContractsInfo__SystemUsers_Contracts_Allowances> vw__ContractsInfo__SystemUsers_Contracts_Allowances { get; set; }
        public DbSet<vw__PaymentInfo__BookingRs_BookingRooms_Customers> vw__PaymentInfo__BookingRs_BookingRooms_Customers { get; set; }
        public DbSet<vw__PermitInfo__SystemUsers_Permits_PermitDetails> vw__PermitInfo__SystemUsers_Permits_PermitDetails { get; set; }
        public DbSet<vw__SearchCustomer__Companies_CustomerGroups_Customers> vw__SearchCustomer__Companies_CustomerGroups_Customers { get; set; }
        public DbSet<vw__ServicesInfo__Services_ServiceGroups> vw__ServicesInfo__Services_ServiceGroups { get; set; }
        public DbSet<vw__SystemUsersInfo__SystemUsers_Divisions> vw__SystemUsersInfo__SystemUsers_Divisions { get; set; }
        public DbSet<vw_QueryDataPayment> vw_QueryDataPayment { get; set; }
        public DbSet<vw_Support1_DataPayment> vw_Support1_DataPayment { get; set; }
        public DbSet<vw_Support2_DataPayment> vw_Support2_DataPayment { get; set; }
    
        [EdmFunction("DatabaseDA", "f_split")]
        public virtual IQueryable<f_split_Result> f_split(string param, string delimiter)
        {
            var paramParameter = param != null ?
                new ObjectParameter("param", param) :
                new ObjectParameter("param", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("delimiter", delimiter) :
                new ObjectParameter("delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<f_split_Result>("[DatabaseDA].[f_split](@param, @delimiter)", paramParameter, delimiterParameter);
        }
    
        [EdmFunction("DatabaseDA", "Get_Status_Room_In_Date")]
        public virtual IQueryable<Get_Status_Room_In_Date_Result> Get_Status_Room_In_Date(Nullable<System.DateTime> checkDate)
        {
            var checkDateParameter = checkDate.HasValue ?
                new ObjectParameter("CheckDate", checkDate) :
                new ObjectParameter("CheckDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Get_Status_Room_In_Date_Result>("[DatabaseDA].[Get_Status_Room_In_Date](@CheckDate)", checkDateParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual ObjectResult<sp_BookingExt_GetAllBooking_Result> sp_BookingExt_GetAllBooking(Nullable<System.DateTime> from, Nullable<System.DateTime> to)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("From", from) :
                new ObjectParameter("From", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("To", to) :
                new ObjectParameter("To", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BookingExt_GetAllBooking_Result>("sp_BookingExt_GetAllBooking", fromParameter, toParameter);
        }
    
        public virtual ObjectResult<sp_BookingRooms_CheckConflict_CheckTimeError_Result> sp_BookingRooms_CheckConflict_CheckTimeError()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BookingRooms_CheckConflict_CheckTimeError_Result>("sp_BookingRooms_CheckConflict_CheckTimeError");
        }
    
        public virtual ObjectResult<sp_BookingRooms_CheckConflict_Status678Now_Result> sp_BookingRooms_CheckConflict_Status678Now(Nullable<System.DateTime> now)
        {
            var nowParameter = now.HasValue ?
                new ObjectParameter("Now", now) :
                new ObjectParameter("Now", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BookingRooms_CheckConflict_Status678Now_Result>("sp_BookingRooms_CheckConflict_Status678Now", nowParameter);
        }
    
        public virtual ObjectResult<sp_BookingRooms_CheckConflict_StatusError_Result> sp_BookingRooms_CheckConflict_StatusError()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BookingRooms_CheckConflict_StatusError_Result>("sp_BookingRooms_CheckConflict_StatusError");
        }
    
        public virtual ObjectResult<sp_BookingRooms_Select_ByTime_Result> sp_BookingRooms_Select_ByTime(Nullable<System.DateTime> time)
        {
            var timeParameter = time.HasValue ?
                new ObjectParameter("Time", time) :
                new ObjectParameter("Time", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BookingRooms_Select_ByTime_Result>("sp_BookingRooms_Select_ByTime", timeParameter);
        }
    
        public virtual ObjectResult<sp_BookingRs_CheckConflict_BookingType_BookingRoomStatus_Result> sp_BookingRs_CheckConflict_BookingType_BookingRoomStatus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BookingRs_CheckConflict_BookingType_BookingRoomStatus_Result>("sp_BookingRs_CheckConflict_BookingType_BookingRoomStatus");
        }
    
        public virtual ObjectResult<sp_BookingRs_CheckConflict_Level_CustomerType_Result> sp_BookingRs_CheckConflict_Level_CustomerType()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BookingRs_CheckConflict_Level_CustomerType_Result>("sp_BookingRs_CheckConflict_Level_CustomerType");
        }
    
        public virtual ObjectResult<sp_BookingRsExt_GetInfo_ByTime_ByCustomerType_ByStatusPay_Result> sp_BookingRsExt_GetInfo_ByTime_ByCustomerType_ByStatusPay(Nullable<System.DateTime> from, Nullable<System.DateTime> to, Nullable<int> customerType, string listStatus)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("From", from) :
                new ObjectParameter("From", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("To", to) :
                new ObjectParameter("To", typeof(System.DateTime));
    
            var customerTypeParameter = customerType.HasValue ?
                new ObjectParameter("CustomerType", customerType) :
                new ObjectParameter("CustomerType", typeof(int));
    
            var listStatusParameter = listStatus != null ?
                new ObjectParameter("ListStatus", listStatus) :
                new ObjectParameter("ListStatus", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BookingRsExt_GetInfo_ByTime_ByCustomerType_ByStatusPay_Result>("sp_BookingRsExt_GetInfo_ByTime_ByCustomerType_ByStatusPay", fromParameter, toParameter, customerTypeParameter, listStatusParameter);
        }
    
        public virtual ObjectResult<sp_Contracts_GetCurrentHaveNotAllowances_Result> sp_Contracts_GetCurrentHaveNotAllowances(Nullable<int> iDAllowance)
        {
            var iDAllowanceParameter = iDAllowance.HasValue ?
                new ObjectParameter("IDAllowance", iDAllowance) :
                new ObjectParameter("IDAllowance", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Contracts_GetCurrentHaveNotAllowances_Result>("sp_Contracts_GetCurrentHaveNotAllowances", iDAllowanceParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_Get_Status_ListRooms_In_Month_Result> sp_Get_Status_ListRooms_In_Month(Nullable<System.DateTime> checkpoint)
        {
            var checkpointParameter = checkpoint.HasValue ?
                new ObjectParameter("checkpoint", checkpoint) :
                new ObjectParameter("checkpoint", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Get_Status_ListRooms_In_Month_Result>("sp_Get_Status_ListRooms_In_Month", checkpointParameter);
        }
    
        public virtual ObjectResult<sp_HallExt_GetCurrentStatusHalls_ByIDHall_ByTime_Result> sp_HallExt_GetCurrentStatusHalls_ByIDHall_ByTime(Nullable<int> iDHall, Nullable<System.DateTime> date, Nullable<bool> isLunarDate)
        {
            var iDHallParameter = iDHall.HasValue ?
                new ObjectParameter("IDHall", iDHall) :
                new ObjectParameter("IDHall", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            var isLunarDateParameter = isLunarDate.HasValue ?
                new ObjectParameter("IsLunarDate", isLunarDate) :
                new ObjectParameter("IsLunarDate", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_HallExt_GetCurrentStatusHalls_ByIDHall_ByTime_Result>("sp_HallExt_GetCurrentStatusHalls_ByIDHall_ByTime", iDHallParameter, dateParameter, isLunarDateParameter);
        }
    
        public virtual ObjectResult<sp_HallExt_GetStatusBookingHalls_ByRankTime_Result> sp_HallExt_GetStatusBookingHalls_ByRankTime(Nullable<System.DateTime> from, Nullable<System.DateTime> to, Nullable<bool> isLunarDate)
        {
            var fromParameter = from.HasValue ?
                new ObjectParameter("From", from) :
                new ObjectParameter("From", typeof(System.DateTime));
    
            var toParameter = to.HasValue ?
                new ObjectParameter("To", to) :
                new ObjectParameter("To", typeof(System.DateTime));
    
            var isLunarDateParameter = isLunarDate.HasValue ?
                new ObjectParameter("IsLunarDate", isLunarDate) :
                new ObjectParameter("IsLunarDate", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_HallExt_GetStatusBookingHalls_ByRankTime_Result>("sp_HallExt_GetStatusBookingHalls_ByRankTime", fromParameter, toParameter, isLunarDateParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_PaymentExt_GetAllData_Result> sp_PaymentExt_GetAllData()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_PaymentExt_GetAllData_Result>("sp_PaymentExt_GetAllData");
        }
    
        public virtual ObjectResult<sp_PaymentExt_GetAllData_1_Result> sp_PaymentExt_GetAllData_1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_PaymentExt_GetAllData_1_Result>("sp_PaymentExt_GetAllData_1");
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual ObjectResult<sp_RoomExt_GetCurrentStatusRooms_ByIDRoom_ByTime_Result> sp_RoomExt_GetCurrentStatusRooms_ByIDRoom_ByTime(Nullable<int> iDRoom, Nullable<System.DateTime> now)
        {
            var iDRoomParameter = iDRoom.HasValue ?
                new ObjectParameter("IDRoom", iDRoom) :
                new ObjectParameter("IDRoom", typeof(int));
    
            var nowParameter = now.HasValue ?
                new ObjectParameter("Now", now) :
                new ObjectParameter("Now", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RoomExt_GetCurrentStatusRooms_ByIDRoom_ByTime_Result>("sp_RoomExt_GetCurrentStatusRooms_ByIDRoom_ByTime", iDRoomParameter, nowParameter);
        }
    
        public virtual ObjectResult<sp_Rooms_GetAvailable_ByTime_ByLang_Result> sp_Rooms_GetAvailable_ByTime_ByLang(Nullable<System.DateTime> start, Nullable<System.DateTime> end, Nullable<int> iDLang)
        {
            var startParameter = start.HasValue ?
                new ObjectParameter("Start", start) :
                new ObjectParameter("Start", typeof(System.DateTime));
    
            var endParameter = end.HasValue ?
                new ObjectParameter("End", end) :
                new ObjectParameter("End", typeof(System.DateTime));
    
            var iDLangParameter = iDLang.HasValue ?
                new ObjectParameter("IDLang", iDLang) :
                new ObjectParameter("IDLang", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Rooms_GetAvailable_ByTime_ByLang_Result>("sp_Rooms_GetAvailable_ByTime_ByLang", startParameter, endParameter, iDLangParameter);
        }
    
        public virtual ObjectResult<sp_RoomsExt_CalculationEfficiency_Result> sp_RoomsExt_CalculationEfficiency(string codeRoom, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime)
        {
            var codeRoomParameter = codeRoom != null ?
                new ObjectParameter("CodeRoom", codeRoom) :
                new ObjectParameter("CodeRoom", typeof(string));
    
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RoomsExt_CalculationEfficiency_Result>("sp_RoomsExt_CalculationEfficiency", codeRoomParameter, startTimeParameter, endTimeParameter);
        }
    
        public virtual ObjectResult<sp_RoomsExt_CalculationRevenue_Result> sp_RoomsExt_CalculationRevenue(string codeRoom, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime)
        {
            var codeRoomParameter = codeRoom != null ?
                new ObjectParameter("CodeRoom", codeRoom) :
                new ObjectParameter("CodeRoom", typeof(string));
    
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_RoomsExt_CalculationRevenue_Result>("sp_RoomsExt_CalculationRevenue", codeRoomParameter, startTimeParameter, endTimeParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> sp_Save_bookingroom_service(Nullable<int> iD, string info, Nullable<int> type, Nullable<int> status, Nullable<int> disable, Nullable<int> iDBookingRoom, Nullable<int> iDService, Nullable<decimal> cost, Nullable<System.DateTime> date, Nullable<double> percentTax, Nullable<decimal> costRef_Services, Nullable<double> quantity, Nullable<int> indexSubPayment, Nullable<System.DateTime> invoiceDate, string invoiceNumber, Nullable<System.DateTime> acceptDate)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var infoParameter = info != null ?
                new ObjectParameter("Info", info) :
                new ObjectParameter("Info", typeof(string));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(int));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(int));
    
            var disableParameter = disable.HasValue ?
                new ObjectParameter("Disable", disable) :
                new ObjectParameter("Disable", typeof(int));
    
            var iDBookingRoomParameter = iDBookingRoom.HasValue ?
                new ObjectParameter("IDBookingRoom", iDBookingRoom) :
                new ObjectParameter("IDBookingRoom", typeof(int));
    
            var iDServiceParameter = iDService.HasValue ?
                new ObjectParameter("IDService", iDService) :
                new ObjectParameter("IDService", typeof(int));
    
            var costParameter = cost.HasValue ?
                new ObjectParameter("Cost", cost) :
                new ObjectParameter("Cost", typeof(decimal));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            var percentTaxParameter = percentTax.HasValue ?
                new ObjectParameter("PercentTax", percentTax) :
                new ObjectParameter("PercentTax", typeof(double));
    
            var costRef_ServicesParameter = costRef_Services.HasValue ?
                new ObjectParameter("CostRef_Services", costRef_Services) :
                new ObjectParameter("CostRef_Services", typeof(decimal));
    
            var quantityParameter = quantity.HasValue ?
                new ObjectParameter("Quantity", quantity) :
                new ObjectParameter("Quantity", typeof(double));
    
            var indexSubPaymentParameter = indexSubPayment.HasValue ?
                new ObjectParameter("IndexSubPayment", indexSubPayment) :
                new ObjectParameter("IndexSubPayment", typeof(int));
    
            var invoiceDateParameter = invoiceDate.HasValue ?
                new ObjectParameter("InvoiceDate", invoiceDate) :
                new ObjectParameter("InvoiceDate", typeof(System.DateTime));
    
            var invoiceNumberParameter = invoiceNumber != null ?
                new ObjectParameter("InvoiceNumber", invoiceNumber) :
                new ObjectParameter("InvoiceNumber", typeof(string));
    
            var acceptDateParameter = acceptDate.HasValue ?
                new ObjectParameter("AcceptDate", acceptDate) :
                new ObjectParameter("AcceptDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("sp_Save_bookingroom_service", iDParameter, infoParameter, typeParameter, statusParameter, disableParameter, iDBookingRoomParameter, iDServiceParameter, costParameter, dateParameter, percentTaxParameter, costRef_ServicesParameter, quantityParameter, indexSubPaymentParameter, invoiceDateParameter, invoiceNumberParameter, acceptDateParameter);
        }
    
        public virtual ObjectResult<sp_SystemUsers_GetCurrentInDivision_Result> sp_SystemUsers_GetCurrentInDivision(Nullable<int> iDDivision)
        {
            var iDDivisionParameter = iDDivision.HasValue ?
                new ObjectParameter("IDDivision", iDDivision) :
                new ObjectParameter("IDDivision", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_SystemUsers_GetCurrentInDivision_Result>("sp_SystemUsers_GetCurrentInDivision", iDDivisionParameter);
        }
    
        public virtual ObjectResult<sp_SystemUsers_GetCurrentNotInDivision_Result> sp_SystemUsers_GetCurrentNotInDivision(Nullable<int> iDDivision)
        {
            var iDDivisionParameter = iDDivision.HasValue ?
                new ObjectParameter("IDDivision", iDDivision) :
                new ObjectParameter("IDDivision", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_SystemUsers_GetCurrentNotInDivision_Result>("sp_SystemUsers_GetCurrentNotInDivision", iDDivisionParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
