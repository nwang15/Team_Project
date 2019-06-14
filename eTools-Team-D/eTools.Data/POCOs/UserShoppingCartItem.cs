using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.POCOs
{
    public class UserShoppingCartItem
    {
        public int ItemID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int StockItemID { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public int Count { get; set; }
        public int ShoppingItemID { get; set; }
    }
}
