using AppSecurity.BLL;
using AppSecurity.DAL;
using AppSecurity.Entities;
using eTools.Data.DTOs;
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

namespace eToolsWebsite.eTools.Rentals
{
    public partial class Rentals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    if (!User.IsInRole(SecurityRoles.WebsiteAdmins))
                    {
                        Response.Redirect("~/Account/Login.aspx");
                    }
                }
            }
        }

        protected void ShowCustomerInfo()
        {
            CustomerFullNameLabel.Visible = true;
            CustomerAddressLabel.Visible = true;
            CustomerCityLabel.Visible = true;
            CustomerProvinceLabel.Visible = true;
            CustomerPostalCodeLabel.Visible = true;
        }

        protected void ShowCreditCardForm()
        {
            CreditCardLabel.Visible = true;
            CreditCard.Visible = true;
            CreditCardInfoSaveButton.Visible = true;
        }

        protected void ShowCouponForm()
        {
            CouponLabel.Visible = true;
            CouponInput.Visible = true;
            CouponUseButton.Visible = true;
        }

        protected void ShowAvailableEquipmentListView()
        {
            AvailableEquipmentListView.Visible = true;
        }

        protected void PickCustomer(object sender, CommandEventArgs e)
        {
            int customerid = int.Parse(e.CommandArgument.ToString());
            if (customerid == 0)
            {
                MessageUserControl.ShowInfo("Warning", "No customer has been found!");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    CustomerController csysmgr = new CustomerController();
                    Customer pickedCustomer = csysmgr.Customer_Get_By_Customerid(customerid);
                    ShowCustomerInfo();
                    CustomerFullName.Text = pickedCustomer.FirstName + ", " + pickedCustomer.LastName;
                    CustomerAddress.Text = pickedCustomer.Address;
                    CustomerCity.Text = pickedCustomer.City;
                    CustomerProvince.Text = pickedCustomer.Province;
                    CustomerPostalCode.Text = pickedCustomer.PostalCode;

                    ShowCreditCardForm();
                    ShowAvailableEquipmentListView();

                    CurrentCustomerID.Text = pickedCustomer.CustomerID.ToString();

                }, "Found", "Customer(s) has been found");
            }
        }

        protected void EquipmentAdd(object sender, CommandEventArgs e)
        {
            string username = User.Identity.Name;
            int employeeid;
            int equipmentid = int.Parse(e.CommandArgument.ToString());
            int customerid = int.Parse(CurrentCustomerID.Text);
            //ApplicationUserManager secmgr = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //EmployeeInfo info = secmgr.User_GetEmployee(username);
            employeeid = 1;//info.EmployeeID;
            List<RentalDetailRecord> details = new List<RentalDetailRecord>();
            List<RentalEquipment> einfoList = new List<RentalEquipment>();
            ShowCouponForm();

            if (employeeid == 0)
            {
                MessageUserControl.ShowInfo("Warning", "Please login as an Employee!");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    RentalRecord Record = null;
                    RentalDetailRecord Detail = null;
                    

                    if (CurrentRentalDetailListView.Items.Count == 0)
                    {
                        Record = new RentalRecord();
                        Detail = new RentalDetailRecord();

                        Detail.RentalEquipmentID = equipmentid;

                        RentalEquipmentController resysmgr = new RentalEquipmentController();
                        RentalEquipment reinfo = resysmgr.Equipment_Find_byID(equipmentid);

                        Detail.DailyRate = reinfo.DailyRate;
                        Detail.Days = 1;
                        Detail.ConditionOut = reinfo.Condition;
                        Detail.Paid = false;

                        
                        Record.RentalDate = DateTime.Now;
                        if(Detail == null)
                        {
                            MessageUserControl.ShowInfo("Warning", "Create new Rental failed!");
                        }
                        else
                        {
                            details.Add(Detail);
                        }
                        
                        Record.Details = details;
                        RentalController rsysmgr = new RentalController();
                        rsysmgr.Create_newRentalRecord(customerid, employeeid, Record);

                        einfoList.Add(reinfo);

                        CurrentRentalDetailListView.DataSource = einfoList;
                        CurrentRentalDetailListView.DataBind();

                    }
                    else
                    {
                        Detail = new RentalDetailRecord();
                        Detail.RentalEquipmentID = equipmentid;

                        

                        RentalDetailController rdsysmgr = new RentalDetailController();
                        var dinfo = rdsysmgr.List_RentalDetail_forRental(customerid, employeeid, Record.RentalDate);

                        foreach (var item in dinfo)
                        {
                            RentalEquipmentController resysmgr = new RentalEquipmentController();
                            RentalEquipment reinfo = resysmgr.Equipment_Find_byID(item.RentalEquipmentID);
                            Detail.DailyRate = reinfo.DailyRate;
                            Detail.Days = 1;
                            Detail.ConditionOut = reinfo.Condition;
                            Detail.Paid = false;
                            einfoList.Add(reinfo);
                        }

                        CurrentRentalDetailListView.DataSource = einfoList;
                        CurrentRentalDetailListView.DataBind();
                    }

                }, "Found", "Customer(s) has been found");
            }
        }

        protected void CreditCardInfoSaveButton_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                if (CreditCard.Text == null)
                {
                    MessageUserControl.ShowInfo("Warning", "Please enter valid Credit Card number!");
                }
                else
                {

                }
            }, "Success", "Credit Card number has been saved");
        }

        protected void CouponUseButton_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                CouponController csysmgr = new CouponController();
                Coupon cinfo = csysmgr.Coupons_Get(CouponInput.Text);
                if(cinfo == null)
                {
                    MessageUserControl.ShowInfo("Warning", "Please enter valid coupon value!");
                }
                else
                {
                    if(cinfo.StartDate > DateTime.Now || cinfo.EndDate < DateTime.Now)
                    {
                        MessageUserControl.ShowInfo("Warning", "This coupon cannot be used for now! Please check the valid date!");
                    }
                    else
                    {
                        
                    }
                }

                

            }, "Success", "Coupom has been used");
        }
    }
}