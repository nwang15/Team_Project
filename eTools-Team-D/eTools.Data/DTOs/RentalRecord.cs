using eTools.Data.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.DTOs
{
    public class RentalRecord
    {
        public int? CouponID { get; set; }

        public DateTime RentalDate { get; set; }

        public string CreditCard { get; set; }

        public List<RentalDetailRecord> Details { get; set; }
    }
}
