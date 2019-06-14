using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.POCOs
{
    public class Unorder_Close
    {
        public string Notes { get; set; }

        public bool Closed { get; set; }
        public int PurchaseOrderID { get; set; }
        public int EmployeeID { get; set; }
        public int VendorID { get; set; }

    }
}
