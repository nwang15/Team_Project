using AppSecurity.Entities;
using eTools.Data.Entities;
using eTools.Data.POCOs;
using eToolsSystem.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eToolsWebsite.eTools.Recieving
{
    public partial class Recieving : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (!Request.IsAuthenticated)
            //    {
            //        Response.Redirect("~/Account/Login.aspx");
            //    }              
            //        else
            //        {
            //            if (!User.IsInRole("Receive"))
            //            {
            //                Response.Redirect("~/eTools/Unauthorized.aspx");
            //            }
            //        }
            //    }
            }
        

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }


        protected void SelectOrder_Command(object sender, CommandEventArgs e)
        {
            RFListView.Visible = true;
            ReceiveLinkButton.Visible = true;
            CloseLinkButton.Visible = true;
            ReasonLabel.Visible = true;
            ReasonTextBox.Visible = true;

            MessageUserControl.TryRun(() =>
            {
                ReceivingController recmgr = new ReceivingController();
                List<RecevingOrderList> recevingResoure = new List<RecevingOrderList>();
                int poid = int.Parse((e.CommandArgument).ToString());
                recevingResoure = recmgr.List_PurchaseOrderDetail(poid);
                RFListView.DataSource = recevingResoure;
                RFListView.DataBind();

                PurchaseOrderHiddenField.Value = (poid).ToString();
                //UnOrderedReturnsListView.DataSource = recmgr.UnOrderedReturns_Select(poid);
                //UnOrderedReturnsListView.DataBind();

                UnOrderedReturnsListView.Visible = true;

            }, "Succeed", "Selected an purchase order successfully.");

        }

        protected void UnOrderedReturnsListView_DataBound(object sender, EventArgs e)
        {
            TextBox poidbox = UnOrderedReturnsListView.InsertItem.FindControl("PurchaseOrderIDTextBox") as TextBox;

            poidbox.Text = PurchaseOrderHiddenField.Value;
        }

        protected void ReceiveLinkButton_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {

                List<RecevingOrderList> recevOrder = new List<RecevingOrderList>();

                // ReturnedOrderDetail newReturnOrderDetail = new ReturnedOrderDetail();
                //int receorderid = newReturnOrderDetail.ReceiveOrderID;
                // int purorderdetailid= (newReturnOrderDetail.PurchaseOrderDetailID).GetValueOrDefault() ;
                int poid = int.Parse(PurchaseOrderHiddenField.Value);

                int remainQTY = 0;
                int checkReceiv = 0;

                foreach (var item in RFListView.Items)
                {
                    TextBox receivingTB = item.FindControl("ReceivingTextBox") as TextBox;
                    TextBox returningTB = item.FindControl("ReturningTextBox") as TextBox;
                    TextBox ReasonTB = item.FindControl("ReasonTextBox") as TextBox;
                    Label newSID = item.FindControl("SIDLabel") as Label;
                    Label oust = item.FindControl("OutstandingLabel") as Label;
                    HiddenField newpodeid = item.FindControl("PurchasePrderDetailIDHF1") as HiddenField;
                    Label outstandingL = item.FindControl("OutstandingLabel") as Label;
                    int os = int.Parse(outstandingL.Text);
                    RecevingOrderList reorlist = new RecevingOrderList();

                    reorlist.Receiving = int.Parse(receivingTB.Text);
                    reorlist.Returning = int.Parse(returningTB.Text);
                    reorlist.Reason = ReasonTB.Text;
                    reorlist.SID = int.Parse(newSID.Text);
                    reorlist.PurchasePrderDetail = int.Parse(newpodeid.Value);
                    reorlist.Outstanding = int.Parse(oust.Text);

                    // check ramaining outstanding
                    remainQTY += (reorlist.Outstanding - reorlist.Receiving).GetValueOrDefault();
                    // check any quantity inputs
                    checkReceiv += (reorlist.Receiving + reorlist.Returning).GetValueOrDefault();
                    if (reorlist.Receiving > os)
                    {
                        throw new Exception("Receiving can not be greater than outstanding");
                    }
                    else if(reorlist.Returning != 0)
                    {
                        if(reorlist.Reason == "")
                        {
                            throw new Exception("Must have a reason if you have returning");
                        }
                        else
                        {
                            recevOrder.Add(reorlist);
                        }
                    }
                   
                }
                if (checkReceiv == 0)
                {
                    throw new Exception("Nothing received?");
                }
                bool checkOutstand = false;
                if (remainQTY == 0)
                {
                    checkOutstand = true;
                }
                ReceivingController resmgr = new ReceivingController();
                resmgr.RecevingReturningReason_transaction(checkOutstand, poid, recevOrder);
                MessageUserControl.ShowInfo("Success", "order received succesfully");
                outstandingListView.DataBind();
                HidePanles();
            });

        }

        protected void CloseLinkButton_Click(object sender, EventArgs e)
        {
            string reason = ReasonTextBox.Text;
            if (reason == "")
            {
                MessageUserControl.ShowInfo("Error", "Please provide a reason to force close the Order");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    int poid = int.Parse(PurchaseOrderHiddenField.Value);
                    ReceivingController sysmg = new ReceivingController();
                 
                    outstandingListView.DataBind();
                    HidePanles();

                }, "Success", "The Purchase Order has been closed");
            }



        }

        private void HidePanles()
        {
            RFListView.Visible = false;
            ReceiveLinkButton.Visible = false;
            UnOrderedReturnsListView.Visible = false;
            CloseLinkButton.Visible = false;
            ReasonLabel.Visible = false;
            ReasonTextBox.Visible = false;
        }


    }
}