using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using System.Data.Entity.Migrations;

namespace BussinessLogic
{
    public class CustomerGroups_CustomersBO
    {
        private DatabaseDA aDatabaseDA = new DatabaseDA();
         
        //----------------Display Customers----------------------
        public List<CustomerGroups_Customers> Select_All()
        {
            try
            {
                return aDatabaseDA.CustomerGroups_Customers.OrderByDescending(c => c.ID).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("CustomerGroups_CustomersBO.Select_All:" + ex.ToString());
            }
        }
        //-----------------Select by id-------------------------------
        public CustomerGroups_Customers Select_ByID(int id)
        {
            try
            {
                List<CustomerGroups_Customers> aListCustomerGroups_Customers = aDatabaseDA.CustomerGroups_Customers.Where(a => a.ID == id).ToList();
                if(aListCustomerGroups_Customers.Count > 0)
                {
                    return aListCustomerGroups_Customers[0];
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("CustomerGroups_CustomersBO.Select_ByID:" + ex.ToString());
            }
        }
        //-----------------Select_ByIDCustomer_ByIDCustomerGroup-------------------------------
        public List<CustomerGroups_Customers> Select_ByIDCustomer_ByIDCustomerGroup(int IDCustomer,int IDCustomerGroup)
        {
            try
            {
                return aDatabaseDA.CustomerGroups_Customers.Where(c=>c.IDCustomer==IDCustomer).Where(c=>c.IDCustomerGroup==IDCustomerGroup).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("CustomerGroups_CustomersBO.Select_ByIDCustomer_ByIDCustomerGroup:" + ex.ToString());
            }
        }
        //-----------------Select by IDCustomerGrou-------------------------------
        public List<CustomerGroups_Customers> Select_ByIDCustomerGroup(int IDCustomerGroup)
        {
            try
            {
                return aDatabaseDA.CustomerGroups_Customers.Where(c => c.IDCustomerGroup == IDCustomerGroup).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("CustomerGroups_CustomersBO.Select_ByIDCustomerGroup:" + ex.ToString());
            }
        }      
        //-----------------Select by Name-------------------------------
        public List<CustomerGroups_Customers> Select_ByName(string name)
        {
            try
            {
                return aDatabaseDA.CustomerGroups_Customers.Where(c => c.Name.Contains(name)).OrderByDescending(c=>c.ID).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("CustomerGroups_CustomersBO.Select_ByName:" + ex.ToString());
            }
        }
        //-----------------Add New ---------------------------------

        public int Insert(CustomerGroups_Customers customerGroups_Customers)
        {
            try
            {
                aDatabaseDA.CustomerGroups_Customers.Add(customerGroups_Customers);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("CustomerGroups_CustomersBO.Insert:" + ex.ToString());
            }
        }
        //----------------Update Customers -----------------------------
        public int Update(CustomerGroups_Customers customerGroups_Customers)
        {
            try
            {
                aDatabaseDA.CustomerGroups_Customers.AddOrUpdate(customerGroups_Customers);
                return aDatabaseDA.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception("CustomerGroups_CustomersBO.Update:" + ex.ToString());
            }
        }
        //----------------- Delete Customers  ------------------------------
        public int Delete(int IDCustomer,int IDCustomerGroup)
        {
            try
            {
                
               List<CustomerGroups_Customers>  aCustomerGroups_Customers=aDatabaseDA.CustomerGroups_Customers.Where(c => c.IDCustomer == IDCustomer).Where(c => c.IDCustomerGroup == IDCustomerGroup).ToList();
               aDatabaseDA.CustomerGroups_Customers.RemoveRange(aCustomerGroups_Customers);
               return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("CustomerGroups_CustomersBO.Delete:" + ex.ToString());
            }
        } 
        //Linhting - Tự động thêm khách vào nhóm
        public int AutoInsertCustomerToGroup(int IDCustomer, int IDCustomerGroup, DateTime From)
        {
            try
            {
                if (this.Select_ByIDCustomer_ByIDCustomerGroup(IDCustomer, IDCustomerGroup).Count > 0)
                {
                    return this.Select_ByIDCustomer_ByIDCustomerGroup(IDCustomer, IDCustomerGroup)[0].ID;
                }
                else
                {
                    CustomerGroups_Customers aCustomerGroups_Customers = new CustomerGroups_Customers();
                    aCustomerGroups_Customers.IDCustomer = IDCustomer;
                    aCustomerGroups_Customers.IDCustomerGroup = IDCustomerGroup;
                    aCustomerGroups_Customers.FromDate = From;
                    aCustomerGroups_Customers.Disable = false;
                    return this.Insert(aCustomerGroups_Customers);
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw new Exception("CustomersBO.AutoInsertCustomer:" + ex.ToString());
            }
        }
    }
}
