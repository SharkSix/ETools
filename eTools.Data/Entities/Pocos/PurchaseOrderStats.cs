using eTools.Data.Entities.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.Entities.Pocos
{
    public class PurchaseOrderStats
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public decimal Subtotal { get; set; }
        public decimal GST { get; set; }
        public decimal Total { get; set; }
    }
}
