namespace eTools.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReturnedOrderDetail
    {
        public int ReturnedOrderDetailID { get; set; }

        public int ReceiveOrderID { get; set; }

        public int? PurchaseOrderDetailID { get; set; }

        [StringLength(50, ErrorMessage = "ItemDescription cannot be more than 50 characters.")]
        public string ItemDescription { get; set; }

        public int Quantity { get; set; }

        [StringLength(50, ErrorMessage = "Reason cannot be more than 50 characters.")]
        public string Reason { get; set; }

        [StringLength(50, ErrorMessage = "VendorStockNumber cannot be more than 50 characters.")]
        public string VendorStockNumber { get; set; }

        public virtual PurchaseOrderDetail PurchaseOrderDetail { get; set; }

        public virtual ReceiveOrder ReceiveOrder { get; set; }
    }
}
