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
using eToolsSystem.UserControls;
using eTools.Data.DTOs;
#endregion

namespace eToolsSystem.BLL
{
    public class SaleDetailController
    {
        public void Add_AddToSale(int employeeid, List<ListSale> saleList, List<SaleDetailsList> saledetailsList)
        {
            List<string> reasons = new List<string>();
            using (var context = new eToolsContext())
            {
                Sale exists = context.Sales
                    .Where(x => x.EmployeeID == employeeid).Select(x => x).FirstOrDefault();
                SaleDetail newSaleDetail = null;
                int saleDetailID = 0;

                if (exists == null)
                {
                    exists = new Sale();
                    foreach (ListSale item in saleList)
                    {
                        exists.TaxAmount = item.TaxAmount;
                        exists.SubTotal = item.SubTotal;
                        exists.CouponID = item.CouponID;
                        exists.PaymentType = item.paymentType;
                    }
                    exists.EmployeeID = employeeid;
                    exists.SaleDate = DateTime.Now;
                    
                    exists = context.Sales.Add(exists);
                    saleDetailID = 1;
                }
                else
                {
                    if (exists.SaleDate != null)
                    {
                        exists = new Sale();
                        foreach (ListSale item in saleList)
                        {
                            exists.TaxAmount = item.TaxAmount;
                            exists.SubTotal = item.SubTotal;
                            exists.CouponID = item.CouponID;
                            exists.PaymentType = item.paymentType;
                        }
                        exists.EmployeeID = employeeid;
                        exists.SaleDate = DateTime.Now;                      
                        exists = context.Sales.Add(exists);
                        saleDetailID = 1;
                    }

                    saleDetailID = exists.SaleDetails.Count() + 1;
                    newSaleDetail = exists.SaleDetails.SingleOrDefault(x => x.SaleID == exists.SaleID);
                    if (newSaleDetail != null)
                    {
                        reasons.Add("Item already exists in the order.");
                    }
                                                      
                }
                if (reasons.Count > 0)
                {
                    throw new BusinessRuleException("Adding item to order", reasons);
                }
                else
                {
                    foreach (SaleDetailsList item in saledetailsList)
                    {
                        newSaleDetail = new SaleDetail();
                        var newStockItem = context.StockItems.Find(item.StockItemID);

                        if ((newStockItem.QuantityOnHand - item.Quantity) <= 0)
                        {
                            reasons.Add($"Theres no enough item{item.Description} on stock");
                            throw new BusinessRuleException("Exceed quantity on stock", reasons);
                        }
                        else
                        {
                            newSaleDetail.StockItemID = item.StockItemID;
                            newSaleDetail.SellingPrice = item.Price;
                            newSaleDetail.Quantity = item.Quantity;
                            newSaleDetail.Backordered = false;
                            newStockItem.QuantityOnHand = newStockItem.QuantityOnHand - item.Quantity;
                            exists.SaleDetails.Add(newSaleDetail);
                        }                       
                    }
                }
                context.SaveChanges();
            }
            ;
        }
    }
}
