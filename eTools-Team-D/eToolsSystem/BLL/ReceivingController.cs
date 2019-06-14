
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using System.Collections.Generic;
using System.ComponentModel;
using eTools.Data.Entities;
using eTools.Data.POCOs;
using eToolsSystem.DAL;
#endregion

namespace eToolsSystem.BLL
{
    [DataObject]

    public class ReceivingController
    {
        #region part-one, Select outstanding order.
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PurchaseOrderList> List_PurchaseOrderForReceiveOrdersSelection()

        {
            using (var context = new eToolsContext())
            {
                var results = from x in context.PurchaseOrders
                              orderby x.OrderDate
                              where x.Closed.Equals(false)
                              select new PurchaseOrderList
                              {
                                  PON = x.PurchaseOrderNumber,
                                  OrderDate = x.OrderDate,
                                  VendorName = x.Vendor.VendorName,
                                  Phone = x.Vendor.Phone,
                                  PurchaseOrderID = x.PurchaseOrderID,
                                  VendorID = x.VendorID,
                                  EmployeeID = x.EmployeeID
                              };

                return results.ToList();
            }
        }// part-one, select outstanding orders.
        #endregion

        #region part-two, review outstanding order details
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RecevingOrderList> List_PurchaseOrderDetail(int purchaseorderid)
        {
            using (var context = new eToolsContext())
            {
                var results = from x in context.PurchaseOrderDetails
                              orderby x.StockItemID
                              where x.PurchaseOrderID == purchaseorderid
                              select new RecevingOrderList
                              {
                                  PurchaseOrderID = x.PurchaseOrderID,
                                  PurchasePrderDetail = x.PurchaseOrderDetailID,
                                  SID = x.StockItemID,
                                  Desription = x.StockItem.Description,
                                  QtyO = x.StockItem.QuantityOnOrder,
                                  Ordered = x.Quantity,
                                  CategoryID = x.StockItem.CategoryID,
                                  QOS = x.StockItem.QuantityOnHand,
                                  Outstanding = x.Quantity - (int?)(from y in x.ReceiveOrderDetails
                                                                    select y.QuantityReceived).Sum(),
                              };
                return results.ToList();
            }
        } // part-two,view outstanding order details.
        #endregion

        #region unordered and return form.
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnOrderedLists> UnOrderedReturns_Select(int purchaseorderid)
        {
            using (var context = new eToolsContext())
            {
                var results = from x in context.UnorderedPurchaseItemCarts
                              orderby x.CartID
                              where x.PurchaseOrderID.Equals(purchaseorderid)
                              select new UnOrderedLists
                              {
                                  CID = x.CartID,
                                  PON = x.PurchaseOrderID,
                                  Description = x.Description,
                                  VSN = x.VendorStockNumber,
                                  QTY = x.Quantity
                              };
                return results.ToList();
            }

        }// part-three, unordered and return form.
        #endregion

        #region Part-Three,UnorderedPurchaseOrderCURD, Select List
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnorderedPurchaseItemCart> UnOrderedReturns_List(int purchaseorderid)
        {
            using (var context = new eToolsContext())
            {
                //context.UnorderedPurchaseItemCarts.All(item);
                //context.SaveChanges();

                return context.UnorderedPurchaseItemCarts.Where(x => x.PurchaseOrderID == purchaseorderid).ToList();
            }
        }// part-three, Insert button.
        #endregion

        #region Part-Three,UnorderedPurchaseOrderCURD, Insert Button
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void UnOrderedReturns_Add(UnorderedPurchaseItemCart item)
        {

            using (var context = new eToolsContext())
            {

                // var purchaseorderid = context.PurchaseOrders.Find(item.PurchaseOrderID);


                context.UnorderedPurchaseItemCarts.Add(item);
                //context.UnorderedPurchaseItemCarts.Add();

                context.SaveChanges();
            }
        }// part-three, Insert button.
        #endregion

        #region part-Three,UnorderedPurchaseOrderCURD, Delete Button
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void UnOrderReturns_Delete(UnorderedPurchaseItemCart item)
        {
            using (var context = new eToolsContext())
            {
                var existing = context.UnorderedPurchaseItemCarts.Find(item.CartID);


                context.UnorderedPurchaseItemCarts.Remove(existing);
                context.SaveChanges();


            }
        }// part-three, Delete button
        #endregion

        #region receving/returning/reasons data transaction.
        public void RecevingReturningReason_transaction(bool checkOutstand, int purchaseorderid, List<RecevingOrderList> recevOrder)
        {

            using (var context = new eToolsContext())
            {
                foreach (var item in recevOrder)
                {

                    var newstockItem = context.StockItems.Find(item.SID);
                    //newOrderOnHand += (item.Receiving).GetValueOrDefault();

                    newstockItem.QuantityOnHand += (item.Receiving).GetValueOrDefault();
                    newstockItem.QuantityOnOrder -= (item.Receiving).GetValueOrDefault();

                    context.SaveChanges();

                    //if(item.Outstanding == 0)
                    //{
                    //    Remove_PurchaseOrderDetail(item.PurchasePrderDetail);
                    //}
                }

                Create_ReceiveOrder(purchaseorderid, recevOrder);
                //Create_ReturnedOrderDetails(purchaseorderid, purorderdetailid, recevOrder);

                PurchaseOrder getPO = context.PurchaseOrders.Find(purchaseorderid);

                if (checkOutstand)
                {
                    getPO.Closed = true;
                    context.SaveChanges();
                }


            }
        }
        #endregion

        #region Create RceiveOrder   
        public void Create_ReceiveOrder(int purchaseorderid, List<RecevingOrderList> quanreceList)
        {

            using (var context = new eToolsContext())
            {

                ReceiveOrder newReceiveOrder = new ReceiveOrder();
                newReceiveOrder.PurchaseOrderID = purchaseorderid;
                newReceiveOrder.ReceiveDate = DateTime.Now;

                context.ReceiveOrders.Add(newReceiveOrder);

                context.SaveChanges();

                var get_lastReceiveOrder = (from x in context.ReceiveOrders
                                            select x).ToList();
                ReceiveOrder lastRece = get_lastReceiveOrder.LastOrDefault();



                Create_ReturnedOrderDetails(lastRece.ReceiveOrderID, quanreceList);
                Create_ReceiveOrderDetail(lastRece.ReceiveOrderID, quanreceList);
            }
        }// part-three, Insert button.
        #endregion

        #region Create RceiveOrderDetail   
        public void Create_ReceiveOrderDetail(int receorderid, List<RecevingOrderList> quanreceList)
        {

            using (var context = new eToolsContext())
            {


                foreach (var item in quanreceList)
                {
                    ReceiveOrderDetail newReceiveOrderDetail = new ReceiveOrderDetail();
                    newReceiveOrderDetail.ReceiveOrderID = receorderid;
                    newReceiveOrderDetail.PurchaseOrderDetailID = item.PurchasePrderDetail;

                    newReceiveOrderDetail.QuantityReceived = item.Receiving.GetValueOrDefault();
                    if (item.Outstanding > 0 && item.Receiving != 0)
                    {
                        context.ReceiveOrderDetails.Add(newReceiveOrderDetail);
                    }

                }



                context.SaveChanges();
            }
        }// part-three, Insert button.
        #endregion

        #region Create ReturnedOrderDetails
        public void Create_ReturnedOrderDetails(int receorderid, List<RecevingOrderList> quanreceList)
        {

            using (var context = new eToolsContext())
            {

                // var purchaseorderid = context.PurchaseOrders.Find(item.PurchaseOrderID);
                foreach (var item in quanreceList)
                {
                    if (item.Returning > 0)
                    {
                        ReturnedOrderDetail newReturnOrderDetail = new ReturnedOrderDetail();
                        newReturnOrderDetail.ReceiveOrderID = receorderid;
                        newReturnOrderDetail.PurchaseOrderDetailID = item.PurchasePrderDetail;

                        var stoItem = context.StockItems.Find(item.SID);
                        newReturnOrderDetail.ItemDescription = stoItem.Description;
                        newReturnOrderDetail.VendorStockNumber = stoItem.VendorStockNumber;

                        newReturnOrderDetail.Reason = item.Reason;
                        newReturnOrderDetail.Quantity = item.Returning.GetValueOrDefault();

                        context.ReturnedOrderDetails.Add(newReturnOrderDetail);
                    }


                }

                context.SaveChanges();
            }
        }// Create ReturnedOrderDetails.
        #endregion

        #region Update PurchaseOrder
        public int Update_PurchaseOrder(PurchaseOrder item)
        {

            using (var context = new eToolsContext())
            {

                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();

            }
        }// Create ReturnedOrderDetails.
        #endregion

     }
}

