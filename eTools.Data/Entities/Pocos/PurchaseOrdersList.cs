namespace eTools.Data.Entities.Pocos
{
    public class PurchaseOrdersList
    {
        public int StockItemID { get; set; }
        public string Description { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReOrderLevel { get; set; }
        public int QuantityOnOrder { get; set; }
        public int PurchaseOrderQuantity { get; set; }
        public decimal PPrice { get; set; }


    }
}
