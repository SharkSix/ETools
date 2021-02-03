using eTools.Data.DAL;
using eTools.Data.Entities;
using eTools.Data.Entities.Pocos;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace eTools.Application.BLL
{
    [DataObject]
    public class PurchasingController
    {
        #region Lists
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<VendorList> ListVendors()
        {
            using (var context = new eToolsContext())
            {
                var ven = from buyer in context.PurchaseOrders
                          where buyer.OrderDate == null
                          orderby buyer.Vendor.VendorName
                                select new VendorList
                                {
                                    VendorName = buyer.Vendor.VendorName+ "- " + buyer.PurchaseOrderID,
                                    VendorID = buyer.VendorID
                                };
                return ven.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<EmployeeList> ListEmployees()
        {
            using (eToolsContext context = new eToolsContext())
            {
                var emp = from buyer in context.Employees
                          orderby buyer.LastName ascending
                          select new EmployeeList
                          {
                              EmployeeName = buyer.LastName + ", " + buyer.FirstName,
                              EmployeeID = buyer.EmployeeID
                          };
                return emp.ToList();
            }
        }
        #endregion

        #region Get's

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<OrderInfo> ListOrderInfo(int vendorid)
        {
            using (var context = new eToolsContext())
            {

                var info = from form in context.PurchaseOrderDetails
                            where form.PurchaseOrder.VendorID == vendorid
                            where form.Quantity > 0
                            select new OrderInfo
                            {
                                ItemID = form.StockItem.StockItemID,
                                Desc = form.StockItem.Description,
                                QOH = form.StockItem.QuantityOnHand,
                                ROL = form.StockItem.ReOrderLevel,
                                QOO = form.StockItem.QuantityOnOrder,
                                Buffer = form.StockItem.QuantityOnHand - form.StockItem.ReOrderLevel,
                                PurchasePrice = form.PurchasePrice
                            };
                return info.ToList();

            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PurchaseOrdersList> ListPurchaseOrder(int vendorid)
        {
            using (var context = new eToolsContext())
            {
                var result = from data in context.PurchaseOrderDetails
                             where data.PurchaseOrder.VendorID == vendorid
                             where data.StockItem.StockItemID == data.StockItemID
                             where data.Quantity > 0
                             select new PurchaseOrdersList()
                             {
                                 StockItemID = data.StockItem.StockItemID,
                                 Description = data.StockItem.Description,
                                 QuantityOnHand = data.StockItem.QuantityOnHand,
                                 ReOrderLevel = data.StockItem.ReOrderLevel,
                                 QuantityOnOrder = data.StockItem.QuantityOnOrder,
                                 PurchaseOrderQuantity = data.Quantity,
                                 PPrice = data.StockItem.PurchasePrice
                             };
                             return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public PurchaseOrderStats GetPurchaseOrderInfo(int vendorid)
        {
            PurchaseOrderStats results = null;
   

            using (var context = new eToolsContext())
            {

                var theOrder = (from x in context.PurchaseOrders
                                where x.VendorID == vendorid
                                select x).FirstOrDefault();


                if (theOrder != null)
                {
                    results = new PurchaseOrderStats()
                    {
                        ID = theOrder.PurchaseOrderID,
                        Location = theOrder.Vendor.City,
                        Name = theOrder.Vendor.VendorName,
                        Phone = theOrder.Vendor.Phone,
                        GST = theOrder.TaxAmount,
                        Subtotal = theOrder.SubTotal,
                        Total = theOrder.SubTotal + theOrder.TaxAmount
                        
                    };
                }
                return results;
            }
        }

        #endregion

        #region PO CRUD
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void StartOrder (PurchaseOrder item)
        {
            using (var context = new eToolsContext())
            {

                var added = context.PurchaseOrders.Add(item);

                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void DeleteOrder (PurchaseOrder item)
        {
            using (var context = new eToolsContext())
            {
                var existing = context.PurchaseOrders.Find(item.PurchaseOrderID);

                context.PurchaseOrders.Remove(existing);

                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateOrder (PurchaseOrder item)
        {
            using(eToolsContext context = new eToolsContext())
            {

                var attached = context.PurchaseOrders.Attach(item);

                var existing = context.Entry(attached);

                existing.State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }
        }
        #endregion

 
        #region Vendor 
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void DeleteItem(PurchaseOrderDetail item)
        {
            using (var context = new eToolsContext())
            {
                var existing = context.StockItems.Find(item.StockItemID);

                context.StockItems.Remove(existing);

                context.SaveChanges();
            }
        }
        #endregion

    }
}
