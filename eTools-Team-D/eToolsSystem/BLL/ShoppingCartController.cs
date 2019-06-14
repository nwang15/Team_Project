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
    public class ShoppingCartController
    {
        public void DeleteShoppingCart(int employeeid)
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
                    context.ShoppingCarts.Remove(exists);
                    context.SaveChanges();

                }
            }
        }//eom
    }
}
