using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.Entities.Pocos
{
    public class ShoppingCartItemList
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ItemTotal { get; set; }
        public int StockItemID { get; set; }
        public int ShoppingCartItemID { get; set; }
    }
}
