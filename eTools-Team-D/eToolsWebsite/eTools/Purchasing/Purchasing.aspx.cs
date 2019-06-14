using AppSecurity.BLL;
using AppSecurity.DAL;
using AppSecurity.Entities;
using eTools.Data.Entities;
using eTools.Data.POCOs;
using eToolsSystem.BLL;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eToolsWebsite.eTools.Purchasing
{
    public partial class Purchasing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (!Request.IsAuthenticated)
            //    {
            //        Response.Redirect("~/Account/Login.aspx");
            //    }
            //    else
            //    {
            //        if (!User.IsInRole("Purchase"))
            //        {
            //            Response.Redirect("~/eTools/Unauthorized.aspx");
            //        }
            //    }
            //    string username = User.Identity.Name;
            //    Label1.Text = username;
            //}
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (VendorDDL.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("OOps", "Please select a vendor to begin");
            }
        }

        protected void VendorDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VendorDDL.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Wait", "Please select a vendor to begin");
                VendorNameL.Text = "";
                VendorAddressL.Text = "";
                VendorPhoneL.Text = "";
                ItemToOrderPanel.Visible = false;
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    ItemToOrderPanel.Visible = true;
                    int vendorid = int.Parse(VendorDDL.SelectedValue);
                    VendorController sysmg = new VendorController();
                    var theVendor = sysmg.Vendor_Get(vendorid);
                    VendorNameL.Text = theVendor.VendorName;
                    VendorAddressL.Text = theVendor.Address + ", " + theVendor.City + ", " + theVendor.PostalCode;
                    VendorPhoneL.Text = theVendor.Phone;

                    string username = User.Identity.Name;
                    ApplicationUserManager secmgr = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    EmployeeInfo info = secmgr.User_GetEmployee(username);
                    int employeeid = info.EmployeeID;

                    // check if active order exist
                    PurchaseOrderController pomgr = new PurchaseOrderController();
                    if (pomgr.ActiveOrderDetails(vendorid) == null)
                    {
                        pomgr.Create_PurchaseOrder(vendorid, employeeid);
                        MessageUserControl.ShowInfo("Success", "A suggested order has been created");
                        // refresh both listviews
                        //List<PODetail> suggestedItems = pomgr.SuggestedOrderDetails(vendorid);
                        //ItemToOrderListView.DataSource = suggestedItems;
                        //ItemToOrderListView.DataBind();

                        //List<PODetail> remainItems = pomgr.ItemsInStock(vendorid);
                        //RemainingStockItemsListView.DataSource = remainItems;
                        //RemainingStockItemsListView.DataBind();
                    }
                    else
                    {
                        MessageUserControl.ShowInfo("Success", "Retrived a Active Order");
                    }

                    List<PODetail> activeItems = pomgr.ActiveOrderDetails(vendorid);
                    ItemToOrderListView.DataSource = activeItems;
                    ItemToOrderListView.DataBind();

                    List<PODetail> remainItems = pomgr.ItemsInStock(vendorid);
                    RemainingStockItemsListView.DataSource = remainItems;
                    RemainingStockItemsListView.DataBind();
                    DisplaySubtotal(activeItems);
                });

            }

        }

        private void DisplaySubtotal(List<PODetail> activeItems)
        {
            decimal sub = 0;
            foreach (var item in activeItems)
            {
                sub += (item.price * item.qto);
            }
            SubLabel.Text = sub.ToString();
            GSTLabel.Text = (sub * (decimal)0.05).ToString();
        }
        #region two lists transfer
        protected void RemoveButton_Click(object sender, CommandEventArgs e)
        {
            int vendorid = int.Parse(VendorDDL.SelectedValue);
            int podID = int.Parse((e.CommandArgument).ToString());
            PurchaseOrderController sysmgr = new PurchaseOrderController();
            sysmgr.PurchaseOrderDetail_Remove(podID);
            RefreshLists(vendorid);
            
        }

        protected void AddButton_Command(object sender, CommandEventArgs e)
        {
            int vendorid = int.Parse(VendorDDL.SelectedValue);
            int stockItemid = int.Parse((e.CommandArgument).ToString());
            PurchaseOrderController sysmgr = new PurchaseOrderController();
            sysmgr.PurchaseOrderDetail_Add(stockItemid, vendorid);
            RefreshLists(vendorid);

        }
        #endregion
        public void RefreshLists(int vendorid)
        {

            PurchaseOrderController sysmgr = new PurchaseOrderController();
            List<PODetail> activeItems = sysmgr.ActiveOrderDetails(vendorid);
            ItemToOrderListView.DataSource = activeItems;
            ItemToOrderListView.DataBind();

            List<PODetail> remainItems = sysmgr.ItemsInStock(vendorid);
            RemainingStockItemsListView.DataSource = remainItems;
            RemainingStockItemsListView.DataBind();
            DisplaySubtotal(activeItems);
        }
        #region four control buttons
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                int vendorid = int.Parse(VendorDDL.SelectedValue);

                List<PurchaseOrderDetail> polist = new List<PurchaseOrderDetail>();
                foreach (ListViewItem item in ItemToOrderListView.Items)
                {
                    TextBox qtoTB = item.FindControl("qtoTB") as TextBox;
                    TextBox priceTB = item.FindControl("priceTB") as TextBox;
                    LinkButton podetailid = item.FindControl("RemoveButton") as LinkButton;

                    PurchaseOrderDetail tempPODetail = new PurchaseOrderDetail();
                    tempPODetail.PurchaseOrderDetailID = int.Parse(podetailid.CommandArgument);
                    tempPODetail.Quantity = int.Parse(qtoTB.Text);
                    tempPODetail.PurchasePrice = decimal.Parse(priceTB.Text);
                    polist.Add(tempPODetail);
                }
                PurchaseOrderController sysmgr = new PurchaseOrderController();
                sysmgr.PurchaseOrder_Update(polist);
                RefreshLists(vendorid);
            }, "Success", "Purchase Order Updated");
        }

        protected void PlaceButton_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                int vendorid = int.Parse(VendorDDL.SelectedValue);
                List<PurchaseOrderDetail> polist = new List<PurchaseOrderDetail>();

                foreach (ListViewItem item in ItemToOrderListView.Items)
                {
                    Label sid = item.FindControl("SIDLabel") as Label;
                    TextBox qtoTB = item.FindControl("qtoTB") as TextBox;
                    TextBox priceTB = item.FindControl("priceTB") as TextBox;
                    LinkButton podetailid = item.FindControl("RemoveButton") as LinkButton;

                    PurchaseOrderDetail tempPODetail = new PurchaseOrderDetail();
                    tempPODetail.PurchaseOrderDetailID = int.Parse(podetailid.CommandArgument);
                    tempPODetail.StockItemID = int.Parse(sid.Text);
                    tempPODetail.Quantity = int.Parse(qtoTB.Text);
                    if (tempPODetail.Quantity == 0)
                    {
                        throw new Exception("Shouldn not have any Items with 0 quantity.");
                    }
                    tempPODetail.PurchasePrice = decimal.Parse(priceTB.Text);
                    polist.Add(tempPODetail);
                }
                PurchaseOrderController sysmgr = new PurchaseOrderController();
                sysmgr.PurchaseOrder_Place(polist);
                VendorDDL.SelectedIndex = 0;
                ItemToOrderPanel.Visible = false;
            }, "Success", "Purchase Order have been send to the vendor");
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                int vendorid = int.Parse(VendorDDL.SelectedValue);
                PurchaseOrderController sysmgr = new PurchaseOrderController();
                sysmgr.PurchaseOrder_Delete(vendorid);
            }, "Success", "Purchase Order Deleted"); 

        }
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            VendorDDL.SelectedIndex = 0;
            VendorNameL.Text = "";
            VendorAddressL.Text = "";
            VendorPhoneL.Text = "";
            ItemToOrderPanel.Visible = false;
        }
        #endregion
    }
}