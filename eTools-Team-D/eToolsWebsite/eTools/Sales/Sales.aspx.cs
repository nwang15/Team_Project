using AppSecurity.BLL;
using AppSecurity.DAL;
using AppSecurity.Entities;
using eTools.Data.DTOs;
using eTools.Data.Entities;
using eTools.Data.POCOs;
using eToolsSystem.BLL;
using eToolsSystem.UserControls;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eToolsWebsite.eTools.Sales
{
    public partial class Sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {        
                      
                List<PaymentTypeDDLData> DataCollection = new List<PaymentTypeDDLData>();

                DataCollection.Add(new PaymentTypeDDLData(1, "Money"));
                DataCollection.Add(new PaymentTypeDDLData(2, "Credit"));
                DataCollection.Add(new PaymentTypeDDLData(3, "Debit"));
                DataCollection.Sort((x, y) => x.PaymentTypeNumber.CompareTo(y.PaymentTypeNumber));

                PaymentTypeDDL.DataSource = DataCollection;
                PaymentTypeDDL.DataTextField = "PaymentType";
                PaymentTypeDDL.DataValueField = "PaymentTypeNumber";
                PaymentTypeDDL.DataBind();
                PaymentTypeDDL.Items.Insert(0, "select payment type.....");
            }
        }

        #region SECURITY
        public bool User_Link()
        {

            if (!User.IsInRole("Staff"))
            {
                if (CategoryDDL.SelectedIndex == 0)
                {
                    MessageUserControl.ShowInfo("Please login as Employee to place order.");
                }
                return false;
            }

            else
            {

                return true;
            }

        }
        #endregion

        #region SELF IMPLEMENT CONTROLS
        public void refresh_shoppingcart()
        {
            string username = User.Identity.Name;
            int employeeid;
            ApplicationUserManager secmgr = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            EmployeeInfo info = secmgr.User_GetEmployee(username);
            employeeid = info.EmployeeID;

            ShoppingCartItemController systemmgr = new ShoppingCartItemController();
            List<UserShoppingCartItem> infos = systemmgr.List_ItemsForShoppingCart(employeeid);
            ShoppingCartList.DataSource = infos;
            ShoppingCartList.DataBind();
            totalLabel.DataBind();
        }


        public void checkout_details()
        {
            double totalprice = 0;
            if (SaleList.Items.Count == 0)
            {
                subtotalLabel.Text = "$" + "0";
                TaxLabel.Text = "$" + "0";
                DiscountLabel.Text = "$" + "0";
                totalLabel2.Text = "$" + "0";
            }
            else
            {
                foreach (ListViewItem item in SaleList.Items)
                {
                    Label temp = item.FindControl("TotalLabel") as Label;

                    totalprice += double.Parse(temp.Text.Replace(@"$", string.Empty));
                    subtotalLabel.Text = "$" + totalprice.ToString();
                    TaxLabel.Text = "$" + (totalprice * 0.05).ToString();
                    double discount = double.Parse(DiscountLabel.Text.Replace(@"$", string.Empty));
                    totalLabel2.Text = "$" + (totalprice * (1 + 0.05) - discount).ToString();
                }
            }            
        }
        //private static List<ShoppingCartItem> shoppingCartItem = new List<ShoppingCartItem>();        

        public void refresh_totallabel()
        {
            double totalprice = 0;
            foreach (ListViewItem item in ShoppingCartList.Items)
            {
                Label temp = item.FindControl("TotalLabel") as Label;

                if (temp.Text == "")
                {
                    totalprice = 0;
                }
                else
                {
                    totalprice += double.Parse(temp.Text[0] == '$' ? temp.Text.Replace(@"$", string.Empty) : temp.Text);
                }
                
                totalLabel.Text = "$" + totalprice.ToString();
            }
        }
        #endregion

        #region NAVIGATIONAL BUTTONS CONTROLS
        protected void ShoppingCart_Click(object sender, EventArgs e)
        {
            refresh_shoppingcart();
            refresh_totallabel();
        }
        protected void Cart_Click(object sender, EventArgs e)
        {
            refresh_shoppingcart();
            refresh_totallabel();
            checkout_details();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            CategoryDDL.SelectedIndex = 0;

            //Shopping Cart Controls
            ShoppingCartList.Visible = false;          
            totalLabel.Visible = false;
            TotalbuttonLabel.Visible = false;

            //Checkout Controls
            SaleList.Visible = false;
            totalLabel2.Visible = false;
            TaxLabel.Visible = false;
            subtotalLabel.Visible = false;
            CouponTextBox.Visible = false;
            DiscountLabel.Visible = false;
            CheckCouponButton.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            Label6.Visible = false;
            Label4.Visible = false;
            Label8.Visible = false;
            PaymentTypeDDL.Visible = false;

            string username = User.Identity.Name;
            ApplicationUserManager secmgr = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            EmployeeInfo info = secmgr.User_GetEmployee(username);
            int employeeid = info.EmployeeID;

            //foreach (ListViewItem item in ShoppingCartList.Items)
            //{
            //    HiddenField itemidLabel = item.FindControl("ShoppingItemID") as HiddenField;
            //    int itemid = int.Parse(itemidLabel.Value);
            //    ShoppingCartItemController spcitemmgr = new ShoppingCartItemController();
            //    spcitemmgr.DeleteShoppingItems(employeeid, itemid);
            //}

            //ShoppingCartController spcmgr = new ShoppingCartController();
            //spcmgr.DeleteShoppingCart(employeeid);

            ////refresh the table
            //ShoppingCartItemController systemmgr = new ShoppingCartItemController();
            //List<UserShoppingCartItem> infos = systemmgr.List_ItemsForShoppingCart(employeeid);
            //ShoppingCartList.DataSource = infos;
            //ShoppingCartList.DataBind();

        }
        #endregion

        #region CONTINUE SHOPPING CONTROLS
        protected void CategoryDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            MessageUserControl.TryRun(() =>
            {
                StockItemsController sysmgr = new StockItemsController();
                int? categoryid;

                if (CategoryDDL.SelectedValue != "")
                {
                    categoryid = int.Parse(CategoryDDL.SelectedValue);
                }
                else
                {
                    categoryid = null;
                }
                List<StockItemList> info = sysmgr.List_StockItemsForCategorySelection(categoryid);
                categoryCount.Value = info.Count.ToString();

            }, "Found", "Products currently on stock has been retrieved.");
        }
        protected void stockList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            
            ShoppingCartList.Visible = true;
            totalLabel.Visible = true;
            TotalbuttonLabel.Visible = true;           

            string username = User.Identity.Name;          
            int itemid = int.Parse(e.CommandArgument.ToString());
            int employeeid;            
            MessageUserControl.TryRun(() =>
            {
                //connect to your BLL
                ApplicationUserManager secmgr = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                EmployeeInfo info = secmgr.User_GetEmployee(username);
                employeeid = info.EmployeeID;
                int quantity = int.Parse((e.Item.FindControl("QuantityLabel") as TextBox).Text);
                ShoppingCartItemController sysmgr = new ShoppingCartItemController();
                sysmgr.Add_AddToCart(employeeid, itemid, quantity);//Need to put the quantity in the list!!!!!                             
              
                List<UserShoppingCartItem> infos = sysmgr.List_ItemsForShoppingCart(employeeid);            
                notificationIcon.Style.Remove("visibility");
                //TotalButton.InnerText = (e.Item.FindControl("Total") as Label).ToString();
                ShoppingCartList.DataSource = infos;           
                notificationIcon.Value = infos.Count.ToString();
                ShoppingCartList.DataBind();

                refresh_totallabel();

            }, "Shopping Items Added", $"The item has been addded, you have currently {notificationIcon.Value} items in the shopping cart");
        }

        #endregion

        #region SHOPPING CART CONTROLS
        protected void ShoppingCartList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
           
            if (e.CommandName == "remove")
            {
                string username = User.Identity.Name;
                int itemid = int.Parse(e.CommandArgument.ToString());
                int employeeid;

                if (ShoppingCartList.Items.Count == 0)
                {
                    MessageUserControl.ShowInfo("Warning", "You have clear the shopping cart.");
                }
                else
                {

                    MessageUserControl.TryRun(() =>
                    {
                        ApplicationUserManager secmgr = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                        EmployeeInfo info = secmgr.User_GetEmployee(username);
                        employeeid = info.EmployeeID;
                        ShoppingCartItemController sysmgr = new ShoppingCartItemController();
                        sysmgr.DeleteShoppingItems(employeeid, itemid);

                        List<UserShoppingCartItem> infos = sysmgr.List_ItemsForShoppingCart(employeeid);
                        ShoppingCartList.DataSource = infos;
                        notificationIcon.Value = infos.Count.ToString();
                        ShoppingCartList.DataBind();                        
                        totalLabel.DataBind();

                        refresh_totallabel();

                    }, "Removed", $"Item(s) {itemid} have been removed");

                }
            }
            else if (e.CommandName == "refresh")
            {
                string username = User.Identity.Name;
                int itemid = int.Parse(e.CommandArgument.ToString());
                int employeeid;
                int quantity = int.Parse((e.Item.FindControl("QuantityLabel") as TextBox).Text);

                MessageUserControl.TryRun(() =>
                {
                    ApplicationUserManager secmgr = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    EmployeeInfo info = secmgr.User_GetEmployee(username);
                    employeeid = info.EmployeeID;
                    ShoppingCartItemController sysmgr = new ShoppingCartItemController();
                    sysmgr.Update_RefreshCart(employeeid, itemid, quantity);

                    List<UserShoppingCartItem> infos = sysmgr.List_ItemsForShoppingCart(employeeid);
                    ShoppingCartList.DataSource = infos;
                    notificationIcon.Value = infos.Count.ToString();
                    ShoppingCartList.DataBind();
                    totalLabel.DataBind();

                    refresh_totallabel();

                }, "Updated", $"Item(s) {itemid} have been updated");
            }
            else
            {
                MessageUserControl.ShowInfo("Glad you found this error, cauz I don't even know what should I call this error.");
            }
            
        }
        #endregion

        #region CHEKOUT CONTROLS
        protected void Checkout_Click(object sender, EventArgs e)
        {
            SaleList.Visible = true;
            totalLabel2.Visible = true;
            TaxLabel.Visible = true;
            subtotalLabel.Visible = true;
            CouponTextBox.Visible = true;
            DiscountLabel.Visible = true;
            CheckCouponButton.Visible = true;
            Label2.Visible = true;
            Label3.Visible = true;
            Label6.Visible = true;
            Label4.Visible = true;
            Label8.Visible = true;
            PaymentTypeDDL.Visible = true;

            string username = User.Identity.Name;
            int employeeid;
            if (ShoppingCartList.Items.Count == 0)
            {
                MessageUserControl.ShowInfo("Warning", "No items in current shopping cart");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    ApplicationUserManager secmgr = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    EmployeeInfo info = secmgr.User_GetEmployee(username);
                    employeeid = info.EmployeeID;
                    ShoppingCartItemController sysmgr = new ShoppingCartItemController();
                    List<UserShoppingCartItem> infos = sysmgr.List_ItemsForShoppingCart(employeeid);
                    if (infos.Count == 0)
                    {
                        MessageUserControl.ShowInfo("Warning", "No items in current shopping cart");
                    }
                    SaleList.DataSource = infos;
                    SaleList.DataBind();
                    
                }, "Great", "Here is your chekout info");
                checkout_details();
            }
        }

        protected void CheckCouponButton_Click(object sender, EventArgs e)
        {

            double totalprice = double.Parse(subtotalLabel.Text.Replace(@"$", string.Empty));
            double taxprice = double.Parse(TaxLabel.Text.Replace(@"$", string.Empty));
            CouponController sysmgr = new CouponController();
            Coupon info = sysmgr.Coupons_Get(CouponTextBox.Text);
            if (info == null)
            {
                PercentageLabel.Text = "";
                DiscountLabel.Text = "0";
                MessageUserControl.ShowInfo("Warning", "Your coupon does not exist");
            }
            else
            {
                if (DateTime.Now > info.EndDate)
                {
                    PercentageLabel.Text = "";
                    DiscountLabel.Text = "0";
                    MessageUserControl.ShowInfo("Warning", "Your coupon seems already expired");

                }
                else
                {
                    MessageUserControl.TryRun(() =>
                    {
                    PercentageLabel.Text = info.CouponDiscount.ToString();
                    int discountTemp = info.CouponDiscount;
                    DiscountLabel.Text = "$" + (totalprice * (double)discountTemp * 0.01).ToString();
                    totalLabel2.Text = "$" + ((totalprice + taxprice - (totalprice * (double)discountTemp * 0.01))).ToString();
                    }, "Success", "Your coupon is valid!");
                }
            }
        }
        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
            List<SaleDetailsList> listsaledetails = new List<SaleDetailsList>();
            List<ListSale> listsales = new List<ListSale>();            
            string username = User.Identity.Name;
            int employeeid;
            if (SaleList.Items.Count == 0)
            {
                MessageUserControl.ShowInfo("Warning", "Please add at least one product in order to place order.");
            }
            else
            {
                if (PaymentTypeDDL.SelectedIndex == 0)
                {
                    MessageUserControl.ShowInfo("Warning", "Please select a payment method to place order.");
                }
                else
                {
                    MessageUserControl.TryRun(() =>
                    {
                        ApplicationUserManager secmgr = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                        EmployeeInfo info = secmgr.User_GetEmployee(username);
                        employeeid = info.EmployeeID;

                        foreach (ListViewItem item in SaleList.Items)
                        {
                            Label quantityLabel = item.FindControl("QuantityLabel") as Label;
                            Label priceLabel = item.FindControl("PriceLabel") as Label;
                            HiddenField itemIDLabel = item.FindControl("StockItemIDLabel") as HiddenField;
                            Label descriptionLabel = item.FindControl("DescriptionLabel") as Label;

                            SaleDetailsList newSaleDetailsList = new SaleDetailsList();
                            newSaleDetailsList.Description = descriptionLabel.Text;
                            newSaleDetailsList.Price = decimal.Parse(priceLabel.Text.Replace(@"$", string.Empty));
                            newSaleDetailsList.Quantity = int.Parse(quantityLabel.Text);
                            newSaleDetailsList.StockItemID = int.Parse(itemIDLabel.Value);
                            listsaledetails.Add(newSaleDetailsList);
                        }

                        Label SubtotalLabel = (Label)UpdatePanel3.FindControl("totalLabel2") as Label;
                        Label taxLabel = (Label)UpdatePanel3.FindControl("TaxLabel") as Label;
                        CouponController cpmgr = new CouponController();
                        Coupon coupon = cpmgr.Coupons_Get(CouponTextBox.Text);

                        ListSale newSaleList = new ListSale();
                        newSaleList.PaymentType = PaymentTypeDDL.SelectedItem.ToString();
                        newSaleList.CouponID = coupon == null ? null : coupon.CouponID;
                        newSaleList.SubTotal = decimal.Parse(SubtotalLabel.Text.Replace(@"$", string.Empty));
                        newSaleList.TaxAmount = decimal.Parse(taxLabel.Text.Replace(@"$", string.Empty));
                        listsales.Add(newSaleList);

                        SaleDetailController sysmgr = new SaleDetailController();
                        sysmgr.Add_AddToSale(employeeid, listsales, listsaledetails);

                        foreach (ListViewItem item in SaleList.Items)
                        {
                            HiddenField itemidLabel = item.FindControl("ItemIDLabel") as HiddenField;
                            int itemid = int.Parse(itemidLabel.Value);
                            ShoppingCartItemController spcitemmgr = new ShoppingCartItemController();
                            spcitemmgr.DeleteShoppingItems(employeeid, itemid);
                        }

                        ShoppingCartController spcmgr = new ShoppingCartController();
                        spcmgr.DeleteShoppingCart(employeeid);

                        //refresh the table
                        ShoppingCartItemController systemmgr = new ShoppingCartItemController();
                        List<UserShoppingCartItem> infos = systemmgr.List_ItemsForShoppingCart(employeeid);
                        ShoppingCartList.DataSource = infos;
                        ShoppingCartList.DataBind();

                        subtotalLabel.Text = "$" + "0";
                        TaxLabel.Text = "$" + "0";
                        CouponTextBox.Text = "";
                        DiscountLabel.Text = "$" + "0";
                        totalLabel2.Text = "$" + "0";
                        PaymentTypeDDL.SelectedIndex = 0;

                    }, "Success", "Order has been placed");
                }              
            }
            
        }




        #endregion

       
    }
}