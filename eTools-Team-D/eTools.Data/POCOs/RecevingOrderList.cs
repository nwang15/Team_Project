using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.POCOs
{
    public class RecevingOrderList
    {
        public int SID { get; set; }
        public string Desription { get; set; }

        public int Ordered { get; set; }

        private int? _Outstanding;
        public int? Outstanding
        {
            get
            {
                return _Outstanding;
            }
            set
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    _Outstanding = Ordered;
                }
                else
                {
                    _Outstanding = value;
                }
            }
        }
        public int? Receiving { get; set; }
        public int? Returning { get; set; }
        public string Reason { get; set; }
        public int QOS { get; set; }
        public int QtyO { get; set; }
        public int PurchaseOrderID { get; set; }
        public int PurchasePrderDetail { get; set; }
        
      
        public int CategoryID { get; set; }
    }
}
