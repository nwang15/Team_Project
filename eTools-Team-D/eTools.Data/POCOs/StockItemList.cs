﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Data.POCOs
{
    public class StockItemList
    {
        public int StockItemID { get; set; }
        public string Items { get; set; }
        public decimal Price { get; set; }
        public int QtyOnStock { get; set; }
        public int Count { get; set; }
    }
}
