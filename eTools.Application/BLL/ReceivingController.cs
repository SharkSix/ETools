using eTools.Data.DAL;
using eTools.Data.Entities;
using eTools.Data.Entities.Pocos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Application.BLL
{
    [DataObject]
    public class ReceivingController
    {
        public List<ViewPurchaseOrder> GetOpenPurchaseOrders()
        {
            using (var context = new eToolsContext())
            {
                var results = from aPurchaseOrder in context.PurchaseOrders
                              where aPurchaseOrder.Closed.Equals(false)
                              && aPurchaseOrder.OrderDate.HasValue
                              orderby aPurchaseOrder.OrderDate
                              select new ViewPurchaseOrder()
                              {
                                  OrderNumber = aPurchaseOrder.PurchaseOrderID,
                                  DateOrdered = aPurchaseOrder.OrderDate,
                                  Name = aPurchaseOrder.Vendor.VendorName,
                                  ContactPhone = aPurchaseOrder.Vendor.Phone
                              };
                return results.ToList();
            }
        }

        public List<ViewPurchaseOrderItems> GetPurchaseOrderDetails(int purchaseOrderID)
        {
            using (var context = new eToolsContext())
            {
                var result = from item in context.PurchaseOrderDetails
                           where item.PurchaseOrderID.Equals(purchaseOrderID)
                           select new ViewPurchaseOrderItems()
                           {
                               ItemID = item.StockItemID,
                               ItemDescription = item.StockItem.Description,
                               Ordered = item.Quantity,
                               //Outstanding = item.Quantity - item.ReceiveOrderDetails.Select(receive => receive.QuantityReceived).DefaultIfEmpty(0).Sum(),
                               Outstanding = item.StockItem.QuantityOnOrder,
                               Received = 0,
                               Returned = 0,
                               Reason = ""
                           };
                return result.ToList();
            }
        }

        public void ReceivePurchaseOrder(int orderNumber, List<ViewPurchaseOrderItems> orderDetails, bool orderCompleted)
        {
            using (var context = new eToolsContext())
            {
                PurchaseOrder purchaseOrder = context.PurchaseOrders.Find(orderNumber);
                purchaseOrder.Closed = orderCompleted;
                context.Entry(purchaseOrder).Property("Closed").IsModified = true;

                ReceiveOrder receiveOrder = new ReceiveOrder();
                receiveOrder.PurchaseOrderID = orderNumber;
                receiveOrder.ReceiveDate = DateTime.Now;
                context.ReceiveOrders.Add(receiveOrder);

                StockItem stockItem = null;
                PurchaseOrderDetail purchaseOrderDetail = null;
                ReceiveOrderDetail receiveOrderDetail = null;
                ReturnedOrderDetail returnedOrderDetail = null;

                foreach (ViewPurchaseOrderItems item in orderDetails)
                {
                    purchaseOrderDetail = purchaseOrder.PurchaseOrderDetails.Where(order => order.StockItemID == item.ItemID).SingleOrDefault();
                    if (item.Received > 0)
                    {
                        receiveOrderDetail = new ReceiveOrderDetail();
                        receiveOrderDetail.ReceiveOrderID = receiveOrder.ReceiveOrderID;
                        receiveOrderDetail.PurchaseOrderDetailID = purchaseOrderDetail.PurchaseOrderDetailID;
                        receiveOrderDetail.QuantityReceived = item.Received;
                        context.ReceiveOrderDetails.Add(receiveOrderDetail);

                        stockItem = context.StockItems.Find(item.ItemID);
                        stockItem.QuantityOnHand += item.Received;
                        context.Entry(stockItem).Property("QuantityOnHand").IsModified = true;

                        if (item.Received > stockItem.QuantityOnOrder)
                        {
                            stockItem.QuantityOnOrder = 0;
                        }
                        else
                        {
                            stockItem.QuantityOnOrder -= item.Received;
                        }
                        context.Entry(stockItem).Property("QuantityOnOrder").IsModified = true;
                    }
                    if (item.Returned > 0)
                    {
                        returnedOrderDetail = new ReturnedOrderDetail();
                        returnedOrderDetail.ReceiveOrderID = receiveOrder.ReceiveOrderID;
                        returnedOrderDetail.PurchaseOrderDetailID = purchaseOrderDetail.PurchaseOrderDetailID;
                        returnedOrderDetail.ItemDescription = stockItem.Description;
                        returnedOrderDetail.Quantity = item.Returned;
                        returnedOrderDetail.Reason = item.Reason;
                        context.ReturnedOrderDetails.Add(returnedOrderDetail);
                    }
                }
                context.SaveChanges();
            }
        }

        public void ForceClosePurchaseOrder(int orderNumber, string reason)
        {
            using (var context = new eToolsContext())
            {
                PurchaseOrder purchaseOrder = context.PurchaseOrders.Find(orderNumber);
                purchaseOrder.Notes = reason;
                context.Entry(purchaseOrder).Property("Notes").IsModified = true;
                purchaseOrder.Closed = true;
                context.Entry(purchaseOrder).Property("Closed").IsModified = true;

                StockItem stockItem = null;
                foreach (PurchaseOrderDetail item in purchaseOrder.PurchaseOrderDetails)
                {
                    stockItem = context.StockItems.Find(item.StockItemID);
                    //stockItem.QuantityOnOrder -= item.Quantity - item.ReceiveOrderDetails.Select(receive => receive.QuantityReceived).DefaultIfEmpty(0).Sum();
                    stockItem.QuantityOnOrder = 0;
                    context.Entry(stockItem).Property("QuantityOnOrder").IsModified = true;
                }
                context.SaveChanges();
            }
        }
    }
}