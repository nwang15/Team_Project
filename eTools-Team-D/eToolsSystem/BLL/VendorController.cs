using eTools.Data.Entities;
using eToolsSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eToolsSystem.BLL
{
    [DataObject]
    public class VendorController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Vendor> Vendor_List()
        {
            using (var context = new eToolsContext())
            {
                return context.Vendors.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Vendor Vendor_Get(int vendorid)
        {
            using(var context = new eToolsContext())
            {
                return context.Vendors.Find(vendorid);
            }
        }
    }
}
