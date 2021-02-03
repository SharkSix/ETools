using eTools.Data.Entities.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eTools.Data.DAL;
using eTools.Data.Entities;
using System.ComponentModel;
using System.Security.Principal;

namespace eTools.Application.BLL
{
    [DataObject]
    public class SalesController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CategoryList> GetCategoryList()
        {
            using (var context = new eToolsContext())
            {
                var data = from item in context.Categories
                           select new CategoryList()
                           {
                               CategoryID = item.CategoryID,
                               CategoryDescription = item.Description,
                               ItemCount= item.StockItems.Count()

                           };

                return data.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StockItem> GetStockItemList(int? categoryid)
        {
            using (var context = new eToolsContext())
            {
                var data = from item in context.StockItems
                           select item;

                if (categoryid != 0)
                {
                    data = from item in context.StockItems
                           where item.CategoryID == categoryid
                           select item;
                }
                return data.ToList();
            }
        }

        public int GetOnlineCustomerIDByUserName(string username)
        {
            using (var context = new eToolsContext())
            {
                int result = (from customer in context.OnlineCustomers
                              where customer.UserName == username
                              select customer.OnlineCustomerID).Count();
                return result;
            }
        }

        public int GetShoppingCartByUserName(string username)
        {
            using (var context = new eToolsContext())
            {
                int result = (from customer in context.ShoppingCarts
                              where customer.OnlineCustomer.UserName == username
                              select customer.ShoppingCartID).Count();
                return result;
            }
        }

        public int GetShoppingCartID(string username)
        {
            using (var context = new eToolsContext())
            {
                int result = (from customer in context.ShoppingCarts
                              where customer.OnlineCustomer.UserName == username
                              select customer.ShoppingCartID).FirstOrDefault();
                return result;
            }
        }

        public int GetShoppingCartItemID(int onlineCustomerID)
        {
            using (var context = new eToolsContext())
            {
                int result = (from customer in context.ShoppingCarts
                              where customer.OnlineCustomerID == onlineCustomerID
                              select customer.ShoppingCartID).FirstOrDefault();

                int result2 = (from cart in context.ShoppingCartItems
                               where cart.ShoppingCartID == result
                               select cart.ShoppingCartItemID).FirstOrDefault();
                return result2;
            }
        }

        public int FindCustomerID(string username)
        {
            using (var context = new eToolsContext())
            {
                int result = (from customer in context.OnlineCustomers
                              where customer.UserName == username
                              select customer.OnlineCustomerID).First();
                return result;
            }
        }

        public int AddOnlineCustomer(OnlineCustomer item)
        {
            using (var context = new eToolsContext())
            {

                var added = context.OnlineCustomers.Add(item);

                context.SaveChanges();

                return added.OnlineCustomerID;
            }
        }

        public void AddShoppingCart(ShoppingCart item2)
        {
            using (var context = new eToolsContext())
            {
                // Add the item to the dbContext
                var added = context.ShoppingCarts.Add(item2);

                context.SaveChanges();

            }
        }

        public void AddToShoppingCart(int stockitemid, string username, int quantity)
        {
            using (var context = new eToolsContext())
            {
                var result = from cus in context.OnlineCustomers
                             where cus.UserName == username
                             select new
                             {
                                 id = cus.OnlineCustomerID,
                                 cart = cus.ShoppingCarts.FirstOrDefault()
                             };
                var result2 = from cart in result
                              where cart.id == cart.cart.OnlineCustomerID
                              select new
                              {
                                  id = cart.id,
                                  cartid = cart.cart.ShoppingCartID
                              };

                var result3 = from quan in context.ShoppingCartItems
                              where quan.StockItemID == stockitemid && quan.ShoppingCartID == result2.FirstOrDefault().cartid
                              select new
                              {
                                  quan = quan.Quantity
                              };

                ShoppingCartItem item = new ShoppingCartItem();

                item.ShoppingCartID = result2.FirstOrDefault().cartid;
                item.StockItemID = stockitemid;
                item.Quantity = quantity;

                context.ShoppingCartItems.Add(item);

                context.SaveChanges();

            }
        }

        public void UpdateToShoppingCart(int stockitemid, int shoppingcartitemid, string username, int quantity)
        {
            using (var context = new eToolsContext())
            {
                int result = (from cart in context.ShoppingCartItems
                            where cart.ShoppingCart.OnlineCustomer.UserName == username
                            select cart.ShoppingCartID).First();
                int result2 = (from cart in context.ShoppingCartItems
                             where cart.ShoppingCart.OnlineCustomer.UserName == username && cart.StockItemID == stockitemid
                            select cart.Quantity).First();

                ShoppingCartItem item = new ShoppingCartItem();
                item.ShoppingCartItemID = shoppingcartitemid;
                item.StockItemID = stockitemid;
                item.Quantity = quantity + result2;
                item.ShoppingCartID = result;
                context.Entry<ShoppingCartItem>(context.ShoppingCartItems.Attach(item)).State =
                System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<ShoppingCartItemList> GetShoppingCartItems(string userName)
        {
            using (var context = new eToolsContext())
            {
                var result = (from item in context.OnlineCustomers
                              where item.UserName == userName
                              select item).FirstOrDefault();

                var result2 = (from x in context.ShoppingCarts
                              where x.OnlineCustomerID == result.OnlineCustomerID
                              select x).FirstOrDefault();

                var result3 = from y in context.ShoppingCartItems
                              where y.ShoppingCartID == result2.ShoppingCartID
                              select new ShoppingCartItemList()
                              {
                                  Quantity = y.Quantity,
                                  StockItemID = y.StockItemID,
                                  Description = y.StockItem.Description,
                                  SellingPrice = y.StockItem.SellingPrice,
                                  ItemTotal = y.Quantity * y.StockItem.SellingPrice,
                                  ShoppingCartItemID = y.ShoppingCartItemID
                              };

                return result3.ToList();
                             
            }
        }

        public int DeleteShoppingCartItem(int ShoppingCartItemID)
        {
            using (var context = new eToolsContext())
            {
                ShoppingCartItem cartItem = context.ShoppingCartItems.Find(ShoppingCartItemID);

                context.ShoppingCartItems.Remove(cartItem);

                return context.SaveChanges();

            }
        }

        public int UpdateShoppingCartItem(int ShoppingCartItemID, int quantity)
        {
            using (var context = new eToolsContext())
            {
                ShoppingCartItem cartItem = context.ShoppingCartItems.Find(ShoppingCartItemID);
                cartItem.Quantity = quantity;

                var attached = context.ShoppingCartItems.Attach(cartItem);

                context.Entry(attached).State = System.Data.Entity.EntityState.Modified;

                return context.SaveChanges();
            }
        }

        public decimal GetSubTotal(int ShoppingCartID)
        {
            decimal result2 = 0;

            using (var context = new eToolsContext())
            {
                var resutl = (from a in context.ShoppingCartItems
                             where a.ShoppingCartID == ShoppingCartID
                             select a.Quantity * a.StockItem.SellingPrice).ToList();

                foreach(var item in resutl)
                {
                    result2 = result2 + item;
                }

                return result2;
            }
        }

        public int GetCoupons(string value)
        {
            using (var context = new eToolsContext())
            {
                var result = (from x in context.Coupons
                              where x.CouponIDValue == value
                              select x.CouponDiscount).FirstOrDefault();

                return result;
            }
        }


    }
}
