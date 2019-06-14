using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.Entities
{
    [Table("UnorderedPurchaseItemCart")]
    public partial class UnorderedPurchaseItemCart
    {
        [Key]
        public int CartID { get; set; }

        [Required(ErrorMessage = "PurchaseOrderNumber is reuired.")]
        public int PurchaseOrderID { get; set; }

        [StringLength(100, ErrorMessage = "Description cannot be more than 100 characters.")]
        public string Description { get; set; }

        [StringLength(25, ErrorMessage = "Vendor Stock Number cannot be more than 25 characters.")]
        public string VendorStockNumber { get; set; }

        [Required(ErrorMessage = "Quantity is reuired.")]
        public int Quantity { get; set; }
    }
}
