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
    public class CouponController
    {
        public List<Coupon> Coupons_List()
        {
            //create an transaction instance of your Context class
            using (var context = new eToolsContext())
            {
                return context.Coupons.OrderBy(x => x.CouponIDValue).ToList();
            }
        }

        public Coupon Coupons_Get(string couponvalue)
        {
            //create an transaction instance of your Context class
            using (var context = new eToolsContext())
            {
                return context.Coupons.Where(x => x.CouponIDValue == couponvalue).FirstOrDefault();
            }
        }
    }
}
