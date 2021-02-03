using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.Entities.Pocos
{
    public class OrderInfo
    {
        public int ItemID { get; set; }
        public string Desc { get; set; }
        public int QOH { get; set; }
        public int ROL { get; set; }
        public int QOO { get; set; }
        public int Buffer { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
