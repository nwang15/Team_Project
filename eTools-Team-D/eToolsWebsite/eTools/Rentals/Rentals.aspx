<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rentals.aspx.cs" Inherits="eToolsWebsite.eTools.Rentals.Rentals" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row jumbotron">
        <h1>Rentals</h1>
    </div>
    <div class="row">
        <div class="col-md-11" style="text-align:center; margin-left:80px;">
            <asp:UpdatePanel ID="MessageUpdates" runat="server">
                <ContentTemplate>
                    <uc1:MessageUserControl runat="server" id="MessageUserControl"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="row">
        <div class="col-md-11">
            <ul class="nav nav-tabs">
                <li class="active">
                    <a href="#rent" data-toggle="tab" id="rent">Rent</a>
                </li>
                <li>
                    <a href="#return" data-toggle="tab" id="return">Return</a>                  
                </li>
            </ul>
            <br />
            <div class="tab-content">
                <div class="tab-pane active" id="renting">
                    <div class="row">
                        <h2 style="text-align:left; margin-left:15px;">Start Renting</h2>
                        <br />
                        <div class="col-sm-3">
                            <asp:Label ID="ContactPhoneLabel" runat="server" Text="Phone: " Font-Bold="True"></asp:Label>
                            <br />
                            <asp:TextBox ID="ContactPhone" runat="server"></asp:TextBox>
                            <asp:Button ID="Search" runat="server" Text="Search"/>
                            <br /><br />
                            <asp:Label ID="CustomerFullNameLabel" runat="server" Text="Full Name: " Visible="false" Font-Bold="True"></asp:Label>
                            <asp:Label ID="CustomerFullName" runat="server" Text=""></asp:Label>
                            <asp:Label ID="CurrentCustomerID" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="CustomerAddressLabel" runat="server" Text="Address: " Visible="false" Font-Bold="True"></asp:Label>
                            <asp:Label ID="CustomerAddress" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="CustomerCityLabel" runat="server" Text="City: " Visible="false" Font-Bold="True"></asp:Label>
                            <asp:Label ID="CustomerCity" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="CustomerProvinceLabel" runat="server" Text="Province: " Visible="false" Font-Bold="True"></asp:Label>
                            <asp:Label ID="CustomerProvince" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="CustomerPostalCodeLabel" runat="server" Text="Postal Code: " Visible="false" Font-Bold="True"></asp:Label>
                            <asp:Label ID="CustomerPostalCode" runat="server" Text=""></asp:Label>
                        </div>
                        <br />
                        <div style="margin-top:-20px;" class="col-sm-7">
                            <asp:ListView ID="CustomerListView" runat="server" DataSourceID="CustomerListODS">
                                <AlternatingItemTemplate>
                                    <tr style="background-color: #FFFFFF; color: #284775;">
                                        <td>
                                            <asp:Label Text='<%# Eval("LastName") %>' runat="server" ID="LastNameLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("FirstName") %>' runat="server" ID="FirstNameLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Address") %>' runat="server" ID="AddressLabel" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("CustomerID") %>' OnCommand="PickCustomer">Pick</asp:LinkButton></td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <EmptyDataTemplate>
                                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                                        <tr>
                                            <td>No data was returned.</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: #E0FFFF; color: #333333;">
                                        <td>
                                            <asp:Label Text='<%# Eval("LastName") %>' runat="server" ID="LastNameLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("FirstName") %>' runat="server" ID="FirstNameLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Address") %>' runat="server" ID="AddressLabel" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("CustomerID") %>' OnCommand="PickCustomer">Pick</asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table runat="server">
                                        <tr runat="server">
                                            <td runat="server">
                                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                                        <th runat="server">LastName</th>
                                                        <th runat="server">FirstName</th>
                                                        <th runat="server">Address</th>
                                                        <th runat="server"></th>
                                                    </tr>
                                                    <tr runat="server" id="itemPlaceholder"></tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server">
                                            <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF"></td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <SelectedItemTemplate>
                                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                                        <td>
                                            <asp:Label Text='<%# Eval("LastName") %>' runat="server" ID="LastNameLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("FirstName") %>' runat="server" ID="FirstNameLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Address") %>' runat="server" ID="AddressLabel" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("CustomerID") %>' OnCommand="PickCustomer">Pick</asp:LinkButton></td>
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-5">
                            <asp:Label ID="CreditCardLabel" runat="server" Text="Credit Card: " Font-Bold="True" Visible="false"></asp:Label>
                            <br />
                            <asp:TextBox ID="CreditCard" runat="server" Visible="false"></asp:TextBox>
                            <asp:Button ID="CreditCardInfoSaveButton" runat="server" Text="Commit" Visible="false" OnClick="CreditCardInfoSaveButton_Click"/>
                        </div>
                        <div style="margin-left:40px; float:right" class="col-sm-5">
                            <asp:Label ID="CouponLabel" runat="server" Text="Coupon Value: " Font-Bold="True" Visible="false"></asp:Label>
                            <br />
                            <asp:TextBox ID="CouponInput" runat="server" Visible="false"></asp:TextBox>
                            <asp:Button ID="CouponUseButton" runat="server" Text="Commit" Visible="false" OnClick="CouponUseButton_Click"/>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <asp:ListView ID="AvailableEquipmentListView" runat="server" DataSourceID="AvailableEquipmentListODS" Visible="false">
                                <AlternatingItemTemplate>
                                    <tr style="background-color: #FFFFFF; color: #284775;">
                                        <td>
                                            <asp:Label Text='<%# Eval("RentalEquipmentID") %>' runat="server" ID="RentalEquipmentIDLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("SerialNumber") %>' runat="server" ID="SerialNumberLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("DailyRate") %>' runat="server" ID="DailyRateLabel" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("RentalEquipmentID") %>' OnCommand="EquipmentAdd">Add</asp:LinkButton></td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <EmptyDataTemplate>
                                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                                        <tr>
                                            <td>No data was returned.</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: #E0FFFF; color: #333333;">
                                        <td>
                                            <asp:Label Text='<%# Eval("RentalEquipmentID") %>' runat="server" ID="RentalEquipmentIDLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("SerialNumber") %>' runat="server" ID="SerialNumberLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("DailyRate") %>' runat="server" ID="DailyRateLabel" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("RentalEquipmentID") %>' OnCommand="EquipmentAdd">Add</asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table runat="server">
                                        <tr runat="server">
                                            <td runat="server">
                                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                                        <th runat="server">ID</th>
                                                        <th runat="server">Description</th>
                                                        <th runat="server">Serial#</th>
                                                        <th runat="server">DailyRate</th>
                                                        <th runat="server"></th>
                                                    </tr>
                                                    <tr runat="server" id="itemPlaceholder"></tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server">
                                            <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                <asp:DataPager runat="server" ID="DataPager1">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                        <asp:NumericPagerField></asp:NumericPagerField>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <SelectedItemTemplate>
                                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                                        <td>
                                            <asp:Label Text='<%# Eval("RentalEquipmentID") %>' runat="server" ID="RentalEquipmentIDLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("SerialNumber") %>' runat="server" ID="SerialNumberLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("DailyRate") %>' runat="server" ID="DailyRateLabel" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("RentalEquipmentID") %>' OnCommand="EquipmentAdd">Add</asp:LinkButton></td>
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="col-md-6">
                            <asp:ListView ID="CurrentRentalDetailListView" runat="server">
                                <AlternatingItemTemplate>
                                    <tr style="background-color: #FFFFFF; color: #284775;">
                                        <td>
                                            <asp:Label Text='<%# Eval("RentalEquipmentID") %>' runat="server" ID="RentalEquipmentIDLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("SerialNumber") %>' runat="server" ID="SerialNumberLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("DailyRate") %>' runat="server" ID="DailyRateLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Condition") %>' runat="server" ID="Label1" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("RentalEquipmentID") %>' OnCommand="EquipmentAdd">Add</asp:LinkButton></td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <EmptyDataTemplate>
                                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                                        <tr>
                                            <td>No data was returned.</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr style="background-color: #E0FFFF; color: #333333;">
                                        <td>
                                            <asp:Label Text='<%# Eval("RentalEquipmentID") %>' runat="server" ID="RentalEquipmentIDLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("SerialNumber") %>' runat="server" ID="SerialNumberLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("DailyRate") %>' runat="server" ID="DailyRateLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Condition") %>' runat="server" ID="Label1" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("RentalEquipmentID") %>' OnCommand="EquipmentAdd">Add</asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table runat="server">
                                        <tr runat="server">
                                            <td runat="server">
                                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                                        <th runat="server">ID</th>
                                                        <th runat="server">Description</th>
                                                        <th runat="server">Serial#</th>
                                                        <th runat="server">DailyRate</th>
                                                        <th runat="server">Condition</th>
                                                        <th runat="server"></th>
                                                    </tr>
                                                    <tr runat="server" id="itemPlaceholder"></tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr runat="server">
                                            <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                                <asp:DataPager runat="server" ID="DataPager1">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                        <asp:NumericPagerField></asp:NumericPagerField>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <SelectedItemTemplate>
                                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                                        <td>
                                            <asp:Label Text='<%# Eval("RentalEquipmentID") %>' runat="server" ID="RentalEquipmentIDLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("SerialNumber") %>' runat="server" ID="SerialNumberLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("DailyRate") %>' runat="server" ID="DailyRateLabel" /></td>
                                        <td>
                                            <asp:Label Text='<%# Eval("Condition") %>' runat="server" ID="Label1" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("RentalEquipmentID") %>' OnCommand="EquipmentAdd">Add</asp:LinkButton></td>
                                    </tr>
                                </SelectedItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <asp:ObjectDataSource ID="CustomerListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Customer_Get_By_PhoneNumber" 
        TypeName="eToolsSystem.BLL.CustomerController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ContactPhone" PropertyName="Text" Name="ContactPhone" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="AvailableEquipmentListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get_AvailableEquipmentList" 
        TypeName="eToolsSystem.BLL.RentalEquipmentController">
    </asp:ObjectDataSource>
</asp:Content>
