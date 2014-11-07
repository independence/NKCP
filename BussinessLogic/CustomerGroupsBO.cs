﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

using System.Data.Entity.Migrations;

namespace BussinessLogic
{
   public class CustomerGroupsBO
    {
       private DatabaseDA aDatabaseDA = new DatabaseDA();
       BookingRsBO aBookingRsBO = new BookingRsBO();
       BookingRoomsBO aBookingRoomsBO = new BookingRoomsBO();
       
        //=======================================================
        //Author: LinhTing
        //Function : Chon tat ca CustomerGroup
        //=======================================================
        public List<CustomerGroups> Select_All()
        {
            try
            {
                var aList = aDatabaseDA.CustomerGroups.OrderByDescending(cg => cg.ID).ToList();
                return aList;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Select_All :"+ ex.Message.ToString()));
            }
        }
       
        //=======================================================
        //Author: Hiennv
        //Function : Select_ByIDCompnay
        //=======================================================
        public List<CustomerGroups> Select_ByIDCompany(int IDCompany)
        {
            try
            {
                return  aDatabaseDA.CustomerGroups.Where(cg=>cg.IDCompany==IDCompany).OrderByDescending(cg=>cg.ID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Select_ByIDCompnay :"+ ex.Message.ToString()));
            }
        }


       //=======================================================
        //Author: Hiennv
        //Function : Select_ByName_ByIDCompany
        //=======================================================
        public List<CustomerGroups> Select_ByName_ByIDCompany(string name,int IDCompany)
        {
            try
            {
                return  aDatabaseDA.CustomerGroups.Where(cg=>cg.Name.Contains(name)).Where(cg=>cg.IDCompany==IDCompany).OrderByDescending(cg=>cg.ID).ToList();
               
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Select_ByName_ByIDCompany :"+ ex.Message.ToString()));
            }
        }

        //=======================================================
        //Author: Hiennv
        //Function : Select_ByName
        //=======================================================
        public List<CustomerGroups> Select_ByName(string name)
        {
            try
            {
                return aDatabaseDA.CustomerGroups.Where(cg => cg.Name.Contains(name)).OrderByDescending(cg => cg.ID).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Select_ByName :"+ ex.Message.ToString()));
            }
        }

        //=======================================================
        //Author: LinhTing
        //Function : Chọn CustomerGroup bang ID
        //=======================================================
        public CustomerGroups Select_ByID(int ID)
        {
            try
            {
                List<CustomerGroups> aListCustomerGroups = aDatabaseDA.CustomerGroups.Where(c => c.ID == ID).ToList();
                if (aListCustomerGroups.Count > 0)
                {
                    return aDatabaseDA.CustomerGroups.Where(c => c.ID == ID).ToList()[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Sel_ByID :"+ ex.Message.ToString()));
            }
        }
        //=======================================================
        //Author: LinhTing
        //Function : Chọn CustomerGroup bang ListID
        //=======================================================
        public List<CustomerGroups> Select_ByListID(List<int> ListID)
        {
            try
            {
                return aDatabaseDA.CustomerGroups.Where(c => ListID.Contains(c.ID)).ToList();               
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Select_ByListID :" + ex.Message.ToString()));
            }
        }
        //=======================================================
        //Author: LinhTing
        //Function : Thêm CustomerGroup
        //=======================================================
        public int Insert(CustomerGroups CustomerGroups)
        {
            try
            {
                aDatabaseDA.CustomerGroups.Add(CustomerGroups);
                aDatabaseDA.SaveChanges();
                return CustomerGroups.ID;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Insert :"+ ex.Message.ToString()));
            }
        }

        //=======================================================
        //Author: LinhTing
        //Function : Xoa CustomerGroup bang ID
        //=======================================================
        public int Delete_ByID (int ID)
        {
            try
            {
                CustomerGroups aCustomerGroups = aDatabaseDA.CustomerGroups.Find(ID);
                aDatabaseDA.CustomerGroups.Remove(aCustomerGroups);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Delete_ByID :"+ ex.Message.ToString()));
            }
        }

        //=======================================================
        //Author: LinhTing
        //Function : Xoa CustomerGroup bang IDCompany
        //=======================================================
        public int Delete_ByIDCompany(int IDCompany)
        {
            try
            {
                CustomerGroups aCustomerGroups = aDatabaseDA.CustomerGroups.Find(IDCompany);
                aDatabaseDA.CustomerGroups.Remove(aCustomerGroups);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Delete_ByIDCompany :"+ ex.Message.ToString()));
            }
        }

        //=======================================================
        //Author: LinhTing
        //Function : Update CustomerGroup
        //=======================================================
        public int Update(CustomerGroups CustomerGroups)
        {
            try
            {
                aDatabaseDA.CustomerGroups.AddOrUpdate(CustomerGroups);
                return aDatabaseDA.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Update :"+ ex.Message.ToString()));
            }
        }
        //=======================================================
        //Author: LinhTing
        //Function : Chọn CustomerGroup bang ListIDBookingRoom
        //=======================================================
        public List<CustomerGroups> Select_ByListIDBookingRoom(List<int> ListIDBookingRoom)
        {
            try
            {
                List<BookingRs> aListBookingRs = aBookingRsBO.Select_ByListIDBookingRoom(ListIDBookingRoom);
                List<int> aListIDCustomerGroups = new List<int>();
                int aIDCustomerGroups;
                for (int i = 0; i < aListBookingRs.Count; i++)
                {
                    aIDCustomerGroups = new int();
                    aIDCustomerGroups = aListBookingRs[i].IDCustomerGroup;
                    aListIDCustomerGroups.Add(aIDCustomerGroups);
                }
                return this.Select_ByListID(aListIDCustomerGroups);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("CustomerGroupBO.Select_ByListID :" + ex.Message.ToString()));
            }
        }
    }
}
