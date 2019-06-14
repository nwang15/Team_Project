using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.DTOs
{
    public class PaymentTypeDDLData
    {
        public int PaymentTypeNumber { get; set; }
        public string PaymentType { get; set; }
        public PaymentTypeDDLData()
        {

        }
        public PaymentTypeDDLData(int paymentTypeNumber, string paymentType)
        {
            //greedy constructor
            PaymentTypeNumber = paymentTypeNumber;
            PaymentType = paymentType;
        }
    }
}
