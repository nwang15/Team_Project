using eTools.Data.Entities;
using eTools.Data.POCOs;
using eToolsSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eToolsSystem.BLL
{

    [DataObject]
    public class PurchaseOrderController
    {
        #region PO CRUD
        public void PurchaseOrder_Update(List<PurchaseOrderDetail> polist)
        {
            using (var context = new eToolsContext())
            {
                foreach (var item in polist)
                {
                    var resault = context.PurchaseOrderDetails.Find(item.PurchaseOrderDetailID);
                    resault.Quantity = item.Quantity;
                    resault.PurchasePrice = item.PurchasePrice;
                }
                context.SaveChanges();
            }
        }
        public void PurchaseOrder_Place(List<PurchaseOrderDetail> polist)
        {
            using (var context = new eToolsContext())
            {
                decimal total = 0;
                PurchaseOrder_Update(polist);
                foreach (var item in polist)
                {
                    var stockItem = context.StockItems.Find(item.StockItemID);
                    stockItem.QuantityOnOrder += item.Quantity;
                    total += item.Quantity * item.PurchasePrice;
                }
                //get PurchaseOrderDetail id from ListView
                int poDetailid = (from x in polist
                                  select x.PurchaseOrderDetailID).FirstOrDefault();

                //get PurchaseOrder id from PurchaseOrderDetail
                PurchaseOrderDetail getPurchaseOrderDetail = context.PurchaseOrderDetails.Find(poDetailid);
                int poid = getPurchaseOrderDetail.PurchaseOrderID;

                //get max PurchaseOrderNumber
                var lastPONumber = (from x in context.PurchaseOrders
                                    select x.PurchaseOrderNumber).ToList().Max();
                int lpoNumber = lastPONumber.GetValueOrDefault();
                var resault = context.PurchaseOrders.Find(poid);
                resault.PurchaseOrderNumber = lpoNumber + 1;
                resault.OrderDate = DateTime.Now;
                resault.SubTotal = total;
                resault.TaxAmount = total * (decimal)0.05;

                context.SaveChanges();
            }
        }
        public void PurchaseOrder_Delete(int vendorid)
        {
            using (var context = new eToolsContext())
            {
                PurchaseOrder getPO = context.PurchaseOrders.Where(x => x.VendorID.Equals(vendorid) && x.OrderDate == null).FirstOrDefault();
                List<PurchaseOrderDetail> getPODetails = (from x in context.PurchaseOrderDetails
                                                          where x.PurchaseOrderID.Equals(getPO.PurchaseOrderID)
                                                          select x).ToList();
                if (getPODetails != null)
                {
                    foreach (PurchaseOrderDetail item in getPODetails)
                    {
                        context.PurchaseOrderDetails.Remove(item);
                    }
                }
                context.PurchaseOrders.Remove(getPO);
                context.SaveChanges();
            }
        }
        #endregion
        #region PO Details
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PODetail> ActiveOrderDetails(int vendorid)
        {
            using (var context = new eToolsContext())
            {
                var orderExist = (from x in context.PurchaseOrders
                                  where x.VendorID.Equals(vendorid) && x.OrderDate == null
                                  select x).FirstOrDefault();
                if (orderExist != null)
                {
                    List<PODetail> stockItems = (from x in orderExist.PurchaseOrderDetails
                                                 select new PODetail
                                                 {
                                                     podtailid = x.PurchaseOrderDetailID,
                                                     SID = x.StockItem.StockItemID,
                                                     description = x.StockItem.Description,
                                                     qoh = x.StockItem.QuantityOnHand,
                                                     qoo = x.StockItem.QuantityOnOrder,
                                                     rol = x.StockItem.ReOrderLevel,
                                                     price = x.StockItem.PurchasePrice,
                                                     qto = x.Quantity
                                                 }).ToList();
                    return stockItems;
                }
                else
                {
                    return null;
                }

            }
        }
        //public List<PODetail> SuggestedOrderDetails(int vendorid)
        //{
        //    using (var context = new eToolsContext())
        //    {
        //        List<PODetail> stockItems = (from x in context.StockItems
        //                                     where x.VendorID.Equals(vendorid) && (x.ReOrderLevel - (x.QuantityOnHand + x.QuantityOnOrder)) > 0
        //                                     select new PODetail
        //                                     {
        //                                         SID = x.StockItemID,
        //                                         description = x.Description,
        //                                         qoh = x.QuantityOnHand,
        //                                         qoo = x.QuantityOnOrder,
        //                                         rol = x.ReOrderLevel,
        //                                         price = x.PurchasePrice,
        //                                         qto = x.ReOrderLevel - (x.QuantityOnHand + x.QuantityOnOrder)
        //                                     }).ToList();
        //        return stockItems;
        //    }
        //}
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PODetail> ItemsInStock(int vendorid)
        {
            List<PODetail> itemToOrder = ActiveOrderDetails(vendorid);
            using (var context = new eToolsContext())
            {
                //var allItems = context.StockItems.ToList();
                List<int> poDetailItems = (from x in itemToOrder
                                           select x.SID).ToList();
                List<StockItem> remainStockItems = new List<StockItem>();
                foreach (var item in context.StockItems.Where(x => x.VendorID == vendorid))
                {
                    if (!poDetailItems.Contains(item.StockItemID))
                    {
                        remainStockItems.Add(item);
                    }
                }

                var resault = from x in remainStockItems
                              select new PODetail
                              {
                                  SID = x.StockItemID,
                                  description = x.Description,
                                  qoo = x.QuantityOnOrder,
                                  qoh = x.QuantityOnHand,
                                  rol = x.ReOrderLevel,
                                  price = x.PurchasePrice,
                                  qto = ((x.QuantityOnHand + x.QuantityOnOrder) - x.ReOrderLevel) <= 0 ? 0 : ((x.QuantityOnHand + x.QuantityOnOrder) - x.ReOrderLevel)
                              };
                //IEnumerable<int> stockids = allItems.Select(x => x.StockItemID).Except(itemToOrder.Select(x => x.SID));

                //List<PODetail> itemsInStock = new List<PODetail>();
                //foreach (var index in stockids)
                //{
                //    var itemInStock = (from y in context.StockItems
                //                            where y.StockItemID.Equals(index)
                //                            select new PODetail
                //                            {
                //                                SID = y.StockItemID,
                //                                description = y.Description,
                //                                qoh = y.QuantityOnHand,
                //                                qoo = y.QuantityOnOrder,
                //                                rol = y.ReOrderLevel
                //                            });
                //}
                //List<PODetail> itemInStock = allItems.Any(itemToOrder.Any(y => y.SID)).ToList();
                return resault.ToList();
            }
        }
        //public List<PODetail> ItemsInStockSuggested(int vendorid)
        //{
        //    List<PODetail> itemToOrder = SuggestedOrderDetails(vendorid);
        //    using (var context = new eToolsContext())
        //    {
        //        //var allItems = context.StockItems.ToList();
        //        List<int> poDetailItems = (from x in itemToOrder
        //                                   select x.SID).ToList();
        //        List<StockItem> remainStockItems = new List<StockItem>();
        //        foreach (var item in context.StockItems.Where(x => x.VendorID == vendorid))
        //        {
        //            if (!poDetailItems.Contains(item.StockItemID))
        //            {
        //                remainStockItems.Add(item);
        //            }
        //        }

        //        var resault = from x in remainStockItems
        //                      select new PODetail
        //                      {
        //                          SID = x.StockItemID,
        //                          description = x.Description,
        //                          qoh = x.QuantityOnHand,
        //                          qoo = x.QuantityOnOrder,
        //                          rol = x.ReOrderLevel,
        //                          price = x.PurchasePrice,
        //                          qto = ((x.QuantityOnHand + x.QuantityOnOrder) - x.ReOrderLevel) >= 0 ? ((x.QuantityOnHand + x.QuantityOnOrder) - x.ReOrderLevel) : 0
        //                      };
        //        return resault.ToList();
        //    }
        //}

        public void Create_PurchaseOrder(int vendorid, int employeeid)
        {
            PurchaseOrder newPO = new PurchaseOrder();
            newPO.VendorID = vendorid;
            newPO.EmployeeID = employeeid;
            newPO.Closed = false;
            newPO.TaxAmount = 0;
            newPO.SubTotal = 0;
            decimal subTotal = 0;

            //List<PurchaseOrderDetail> showPODetails = new List<PurchaseOrderDetail>();
            using (var context = new eToolsContext())
            {
                context.PurchaseOrders.Add(newPO);
                context.SaveChanges();

                List<StockItem> vendorStockItems = (from x in context.StockItems
                                                    where x.VendorID.Equals(vendorid) && (x.ReOrderLevel - (x.QuantityOnHand + x.QuantityOnOrder)) > 0
                                                    select x).ToList();
                // generate PurchaseOrderDetail for the new PurchaseOrder

                foreach (var item in vendorStockItems)
                {
                    PurchaseOrderDetail newItem = new PurchaseOrderDetail();
                    newItem.PurchaseOrderID = newPO.PurchaseOrderID;
                    newItem.StockItemID = item.StockItemID;
                    newItem.PurchasePrice = item.PurchasePrice;
                    newItem.Quantity = item.ReOrderLevel - (item.QuantityOnHand + item.QuantityOnOrder);

                    subTotal += item.PurchasePrice;

                    //PODetail showPODetail = new PODetail();
                    //showPODetail.SID = item.StockItemID;
                    //showPODetail.description = item.Description;
                    //showPODetail.qoh = item.QuantityOnHand;
                    //showPODetail.qoo = item.QuantityOnOrder;
                    //showPODetail.rol = item.ReOrderLevel;
                    //showPODetail.QtyToOrder = newItem.Quantity;

                    //showPODetails.Add(newItem);
                    context.PurchaseOrderDetails.Add(newItem);
                }

                newPO.SubTotal = decimal.Round(subTotal, 2);
                newPO.TaxAmount = decimal.Round(subTotal * ((decimal)0.05), 2);
                context.Entry(newPO).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        #region PO Detail ADD/Remove
        public void PurchaseOrderDetail_Remove(int poDetailid)
        {
            using (var context = new eToolsContext())
            {
                var res = context.PurchaseOrderDetails.Find(poDetailid);
                context.PurchaseOrderDetails.Remove(res);
                context.SaveChanges();
            }
        }
        public void PurchaseOrderDetail_Add(int stockItemID, int vendorid)
        {
            using (var context = new eToolsContext())
            {
                PurchaseOrder getPO = context.PurchaseOrders.Where(x => x.VendorID.Equals(vendorid) && x.OrderDate == null).FirstOrDefault();
                if (getPO == null)
                {
                    throw new Exception("no purchase order exist yet.");
                }
                else
                {
                    StockItem getStockItem = context.StockItems.Find(stockItemID);
                    PurchaseOrderDetail newpoDetail = new PurchaseOrderDetail();
                    newpoDetail.PurchaseOrderID = getPO.PurchaseOrderID;
                    newpoDetail.StockItemID = getStockItem.StockItemID;
                    newpoDetail.PurchasePrice = getStockItem.PurchasePrice;
                    newpoDetail.Quantity = getStockItem.ReOrderLevel - (getStockItem.QuantityOnHand + getStockItem.QuantityOnOrder) <= 0 ? 0 : (getStockItem.ReOrderLevel - (getStockItem.QuantityOnHand + getStockItem.QuantityOnOrder));

                    context.PurchaseOrderDetails.Add(newpoDetail);
                    context.SaveChanges();
                }
            }
        }
        #endregion
        #endregion

        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<PurchaseOrderList> List_PurchaseOrderForReceiveOrdersSelection()


        //{
        //    using (var context = new eToolsContext())
        //    {
        //        var results = from x in context.PurchaseOrders
        //                      orderby x.OrderDate
        //                      where x.Closed.Equals(false)
        //                      select new PurchaseOrderList
        //                      {
        //                          PON = x.PurchaseOrderNumber,
        //                          OrderDate = x.OrderDate,
        //                          VendorName = x.Vendor.VendorName,
        //                          PurchaseOrderID = x.PurchaseOrderID
        //                      };

        //        return results.ToList();
        //    }
        //}
    }
}
