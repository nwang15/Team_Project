using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel;
using eTools.Data.Entities;
using eToolsSystem.DAL;
using eTools.Data.POCOs;
using eToolsSystem.UserControls;
#endregion

namespace eToolsSystem.BLL
{
    public class ShoppingCartItemController
    {
        public List<UserShoppingCartItem> List_ItemsForShoppingCart(int employeeid)
        {
            using (var context = new eToolsContext())
            {
                var results = (from x in context.ShoppingCarts
                               where x.EmployeeID.Equals(employeeid)
                               select x).FirstOrDefault();
                if (results == null)
                {
                    return null;
                }
                else
                {
                    var theItems = from x in context.ShoppingCartItems
                                   where x.ShoppingCartID.Equals(results.ShoppingCartID)
                                   orderby x.ShoppingCartItemID
                                   select new UserShoppingCartItem
                                   {
                                       ItemID = x.ShoppingCartItemID,
                                       StockItemID = x.StockItemID,
                                       Description = x.StockItem.Description,
                                       Quantity = x.Quantity,
                                       Price = x.StockItem.SellingPrice,
                                       Total = x.Quantity * x.StockItem.SellingPrice,
                                       Count = (from y in context.ShoppingCartItems
                                                where y.ShoppingCartID.Equals(results.ShoppingCartID)
                                                orderby y.ShoppingCartItemID
                                                select y.ShoppingCartID).Count(),
                                       ShoppingItemID = x.ShoppingCartItemID
                                    };
                    return theItems.ToList();
                }

            }
        }

        public List<ShoppingCartItem> List_ItemsForShoppingCartbyEmployee(int employeeid)
        {
            using (var context = new eToolsContext())
            {
                var results = (from x in context.ShoppingCarts
                               where x.EmployeeID.Equals(employeeid)
                               select x).FirstOrDefault();
                if (results == null)
                {
                    return null;
                }
                else
                {                                 
                    return context.ShoppingCartItems.ToList();
                }

            }
        }
        public void Add_AddToCart(int employeeid, int stockitemid, int quantity)
        {
            List<string> reasons = new List<string>();

            using (var context = new eToolsContext())
            {
                ShoppingCart exists = context.ShoppingCarts
                    .Where(x => x.EmployeeID.Equals(employeeid)).Select(x => x).FirstOrDefault();
                ShoppingCartItem newShoppingCartItem = null;
                int shoppingCartItemID = 0;

                if (exists == null)
                {
                    exists = new ShoppingCart();
                    exists.EmployeeID = employeeid;
                    exists.CreatedOn = DateTime.Today;
                    exists = context.ShoppingCarts.Add(exists);
                    shoppingCartItemID = 1;
                }
                else
                {
                    exists.UpdatedOn = DateTime.Now;
                    shoppingCartItemID = exists.ShoppingCartItems.Count() + 1;
                    newShoppingCartItem = exists.ShoppingCartItems.SingleOrDefault(x => x.StockItemID == stockitemid);

                    if (newShoppingCartItem != null)
                    {
                        reasons.Add("Item already exists on the shopping cart. Edit the quantity in Shopping Cart");
                    }
                }

                if (reasons.Count() > 0)
                {
                    throw new BusinessRuleException("Adding item to shopping cart", reasons);
                }
                else
                {
                    newShoppingCartItem = new ShoppingCartItem();
                    newShoppingCartItem.ShoppingCartItemID = shoppingCartItemID;
                    newShoppingCartItem.Quantity = quantity;
                    newShoppingCartItem.StockItemID = stockitemid;
                    exists.ShoppingCartItems.Add(newShoppingCartItem);                 
                }
                context.SaveChanges();
            }
        }

        public void DeleteShoppingItems(int employeeid, int shoppingitemid)
        {
            using (var context = new eToolsContext())
            {
                //code to go here
                var exists = (from x in context.ShoppingCarts
                              where x.EmployeeID.Equals(employeeid)                         
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("Shooping Cart has been removed from the files.");
                }
                else
                {
                    ShoppingCartItem items = context.ShoppingCartItems.Find(shoppingitemid);
                    if (items == null)
                    {
                        throw new Exception("Item does not exists on file.");
                    }
                    context.ShoppingCartItems.Remove(items);
                    context.SaveChanges();

                }
            }
        }//eom

        public void Update_RefreshCart(int employeeid, int shoppingitemid, int quantity)
        {
            using (var context = new eToolsContext())
            {
                var exists = (from x in context.ShoppingCarts
                              where x.EmployeeID.Equals(employeeid)
                              select x).FirstOrDefault();
                if (exists == null)
                {
                    throw new Exception("shopping cart has been removed from the file");
                }
                else
                {
                    exists.UpdatedOn = DateTime.Now;
                    var newShoppingcartitems = context.ShoppingCartItems.Find(shoppingitemid);
                    newShoppingcartitems.Quantity = quantity;
                }
                context.SaveChanges();
            }
        }

    }
}
