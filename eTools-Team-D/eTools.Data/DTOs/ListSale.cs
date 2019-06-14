using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.DTOs
{
    public class ListSale
    {
        public decimal SubTotal { get; set; }

        public string paymentType;
        public string PaymentType
        {
            get
            {
                return paymentType;
            }
            set
            {
                if (value.Equals("Money"))
                {
                    paymentType = "M";
                }
                else if (value.Equals("Credit"))
                {
                    paymentType = "C";
                }
                else
                {
                    paymentType = "D";
                }
            }
        }
        public int? CouponID { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
