using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
namespace Entity
{
    public class MenusEN : Menus
    {
        public string CodeHall { get; set; }
        public string SkuHall { get; set; }
        public string NameFood { get; set; }
        public decimal? PriceMenu { get; set;}

        public List<Foods> aListFoods = new List<Foods>();
    }
}
