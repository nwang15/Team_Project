<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Purchasing.aspx.cs" Inherits="eToolsWebsite.eTools.Purchasing.Purchasing" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row jumbotron">
            <h1>Purchasing</h1>
        </div>
        <div class="row">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>

        <div class="row">
            <div class="col-md-6">
                <asp:DropDownList ID="VendorDDL" runat="server" DataSourceID="VendorODS" DataTextField="VendorName" DataValueField="VendorID" AppendDataBoundItems="true" OnSelectedIndexChanged="VendorDDL_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Selected="True" Value="0">Select a Vendor...</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource runat="server" ID="VendorODS" OldValuesParameterFormatString="original_{0}" SelectMethod="Vendor_List" TypeName="eToolsSystem.BLL.VendorController"></asp:ObjectDataSource>
                <asp:LinkButton ID="SearchButton" runat="server" OnClick="SearchButton_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
            </div>

            <div class="col-md-6">
                <table>
                    <thead>
                        <tr>
                            <th><span style="font-size: 20px;">Vendor Info</span></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label Text="Vendor:" runat="server" /></td>
                            <td>
                                <asp:Label Text="" runat="server" ID="VendorNameL" /></td>
                            <asp:Label Text="" runat="server" ID="Label1" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label Text="Address:" runat="server" /></td>
                            <td>
                                <asp:Label Text="" runat="server" ID="VendorAddressL" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label Text="Phone:" runat="server" /></td>
                            <td>
                                <asp:Label Text="" runat="server" ID="VendorPhoneL" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <br />
        <br />
        <asp:Panel Visible="false" ID="ItemToOrderPanel" runat="server" align="center">
            <asp:ListView ID="ItemToOrderListView" runat="server">
                <AlternatingItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label Text='<%# Eval("SID") %>' runat="server" ID="SIDLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("description") %>' runat="server" ID="descriptionLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qoo") %>' runat="server" ID="qooLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qoh") %>' runat="server" ID="qohLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("rol") %>' runat="server" ID="rolLabel" /></td>
                        <td>
                            <asp:TextBox Text='<%# Eval("qto") %>' runat="server" ID="qtoTB" Width="50" /></td>
                        <td>
                            <asp:TextBox Text='<%# Eval("price") %>' runat="server" ID="priceTB" Width="150" /></td>
                        <td>
                            <asp:LinkButton ID="RemoveButton" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Eval("podtailid") %>' OnCommand="RemoveButton_Click"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton></td>
                    </tr>
                </AlternatingItemTemplate>

                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>No items need to be purchase yet.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

                <ItemTemplate>

                    <tr style="">
                        <td>
                            <asp:Label Text='<%# Eval("SID") %>' runat="server" ID="SIDLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("description") %>' runat="server" ID="descriptionLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qoo") %>' runat="server" ID="qooLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qoh") %>' runat="server" ID="qohLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("rol") %>' runat="server" ID="rolLabel" /></td>
                        <td>
                            <asp:TextBox Text='<%# Eval("qto") %>' runat="server" ID="qtoTB" Width="50" /></td>
                        <td>
                            <asp:TextBox Text='<%# Eval("price") %>' runat="server" ID="priceTB" Width="150" /></td>
                        <td>
                            <asp:LinkButton ID="RemoveButton" runat="server" CssClass="btn btn-danger" CommandArgument='<%# Eval("podtailid") %>' OnCommand="RemoveButton_Click"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="" border="0">
                                    <tr runat="server" style="">
                                        <th runat="server" style="width: 100px;">SID</th>
                                        <th runat="server" style="width: 400px;">Description</th>
                                        <th runat="server" style="width: 50px;">qoo</th>
                                        <th runat="server" style="width: 50px;">qoh</th>
                                        <th runat="server" style="width: 50px;">rol</th>
                                        <th runat="server" style="width: 50px;">qto</th>
                                        <th runat="server" style="width: 50px;">Price</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style=""></td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>

            <div class="container">
                <div class="row pull-right">
                    
                    <table>
                        <tr>
                            <td><asp:Label Text="Subtotal:" runat="server" /></td>
                            <td>
                                <asp:Label ID="SubLabel" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label Text="GST:" runat="server" /></td>
                            <td>
                                <asp:Label ID="GSTLabel" runat="server" /></td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <br />
            <%-- -------------------
            ---2nd List View---
            ---------------------%>
            <asp:ListView ID="RemainingStockItemsListView" runat="server">
                <AlternatingItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label Text='<%# Eval("SID") %>' runat="server" ID="SIDLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("description") %>' runat="server" ID="descriptionLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qoo") %>' runat="server" ID="qooLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qoh") %>' runat="server" ID="qohLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("rol") %>' runat="server" ID="rolLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qto") %>' runat="server" ID="buffer" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("price") %>' runat="server" ID="priceLabel" /></td>
                        <td>
                            <asp:LinkButton ID="AddButton" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("SID") %>' OnCommand="AddButton_Command"><span class="glyphicon glyphicon-plus"></span></asp:LinkButton></td>
                    </tr>
                </AlternatingItemTemplate>

                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>No Items in stock for this vendor or all items are in purchase order list</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

                <ItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Label Text='<%# Eval("SID") %>' runat="server" ID="SIDLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("description") %>' runat="server" ID="descriptionLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qoo") %>' runat="server" ID="qooLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qoh") %>' runat="server" ID="qohLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("rol") %>' runat="server" ID="rolLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("qto") %>' runat="server" ID="buffer" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("price") %>' runat="server" ID="priceLabel" /></td>
                        <td>
                            <asp:LinkButton ID="AddButton" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("SID") %>' OnCommand="AddButton_Command"><span class="glyphicon glyphicon-plus"></span></asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="" border="1">
                                    <tr runat="server" style="">
                                        <th runat="server">SID</th>
                                        <th runat="server">description</th>
                                        <th runat="server">QuantityOnOrder</th>
                                        <th runat="server">QuantityOnHand</th>
                                        <th runat="server">ReorderLevel</th>
                                        <th runat="server">Buffer</th>
                                        <th runat="server">Price</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
            <%--------------------
            ---Proccess Buttons---
            ----------------------%>
            <div class="container">
                <div class="row">
                    <asp:LinkButton ID="UpdateButton" Text="Update" runat="server" CssClass="btn btn-primary" OnClick="UpdateButton_Click" />
                    <asp:LinkButton ID="PlaceButton" Text="Place" runat="server" CssClass="btn btn-primary" OnClick="PlaceButton_Click" />
                    <asp:LinkButton ID="DeleteButton" Text="Delete" runat="server" CssClass="btn btn-danger" OnClick="DeleteButton_Click"  OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" />
                    <asp:LinkButton ID="ClearButton" Text="Clear" runat="server" CssClass="btn btn-light" OnClick="ClearButton_Click" />
                </div>
            </div>
        </asp:Panel>

    </div>
</asp:Content>
