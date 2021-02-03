using System;

namespace eTools.Data.Entities.Pocos
{
    public class ViewPurchaseOrder
    {
        public int OrderNumber { get; set; }

        public DateTime? DateOrdered { get; set; }
        public string Name { get; set; }
        public string ContactPhone { get; set; }
    }
}
