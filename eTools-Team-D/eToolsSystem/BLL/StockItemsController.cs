using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel;
using eTools.Data.Entities;
using eToolsSystem.DAL;
using eTools.Data.POCOs;
#endregion

namespace eToolsSystem.BLL
{
    [DataObject]
    public class StockItemsController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StockItemList> List_StockItemsForCategorySelection (int? categoryid)
        {
            using (var context = new eToolsContext())
            {
                if (categoryid != null)
                {
                    var results = from x in context.StockItems
                                  where x.CategoryID == categoryid
                                  && x.Discontinued.Equals(false)
                                  orderby x.Description
                                  select new StockItemList
                                  {
                                      StockItemID = x.StockItemID,
                                      Items = x.Description,
                                      Price = x.SellingPrice,
                                      QtyOnStock = x.QuantityOnHand,
                                      Count = (from y in context.StockItems
                                               where y.CategoryID == categoryid
                                               && y.Discontinued.Equals(false)
                                               select y.StockItemID).Count()
                                  };
                    return results.ToList();                 
                }
                else
                {
                    var results = from x in context.StockItems
                                  where x.CategoryID.Equals(x.CategoryID)
                                  && x.Discontinued.Equals(false)
                                  orderby x.Description
                                  select new StockItemList
                                  {
                                      StockItemID = x.StockItemID,
                                      Items = x.Description,
                                      Price = x.SellingPrice,
                                      QtyOnStock = x.QuantityOnHand,
                                      Count = (from y in context.StockItems
                                               where y.CategoryID == categoryid
                                               && y.Discontinued.Equals(false)
                                               select y.StockItemID).Count()
                                  };
                    return results.ToList();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StockItem> StockItems_List()
        {
            //create an transaction instance of your Context class
            using (var context = new eToolsContext())
            {
                return context.StockItems.OrderBy(x => x.Description).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public StockItem StockItems_Get(int stockitemid)
        {
            //create an transaction instance of your Context class
            using (var context = new eToolsContext())
            {
                return context.StockItems.Find(stockitemid);
            }
        }

    }
}
