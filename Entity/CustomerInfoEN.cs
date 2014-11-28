using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Entity
{
    public class CustomerInfoEN : Customers
    {
        public int IDBookingRoom { get; set; }
        public string RoomCode { get; set; }
        public string PurposeComeVietnam { get; set; }
        public Nullable<System.DateTime> DateEnterCountry { get; set; }
        public string EnterGate { get; set; }
        public Nullable<System.DateTime> TemporaryResidenceDate { get; set; }
        public Nullable<System.DateTime> LeaveDate { get; set; }
        public string Organization { get; set; }
        public Nullable<System.DateTime> LimitDateEnterCountry { get; set; }
        //Hiennv        bo xung them truong de hien thi nguoi duoc chon lam dai dien
        public Nullable<bool> PepoleRepresentative { get; set; }
    }
}
