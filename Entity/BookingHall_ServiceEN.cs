using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Entity
{
   public class BookingHall_ServiceEN : BookingHalls_Services
    {
        public string Service_Name { get; set; }
        public string Service_Unit { get; set; }
    }
}
