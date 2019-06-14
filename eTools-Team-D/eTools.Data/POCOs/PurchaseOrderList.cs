using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.POCOs
{
    public class PurchaseOrderList
    {
        public int? PON { get; set; }
        public DateTime? OrderDate { get; set; }
        public string VendorName { get; set; }
        public int PurchaseOrderID { get; set; }
        public string Phone { get; set; }
        public int VendorID { get; set; }
        public int EmployeeID { get; set; }
        
    }
}
