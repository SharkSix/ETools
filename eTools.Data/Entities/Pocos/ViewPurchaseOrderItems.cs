namespace eTools.Data.Entities.Pocos
{
    public class ViewPurchaseOrderItems
    {
        public int ItemID { get; set; }
        public string ItemDescription { get; set; }
        public int Ordered { get; set; }
        public int Outstanding { get; set; }
        public int Received { get; set; }
        public int Returned { get; set; }
        public string Reason { get; set; }
    }
}
