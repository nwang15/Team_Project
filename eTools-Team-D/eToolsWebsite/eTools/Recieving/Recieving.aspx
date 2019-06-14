<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recieving.aspx.cs" Inherits="eToolsWebsite.eTools.Recieving.Recieving" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row jumbotron">
        <h1>Receiving</h1>
    </div>


    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />

    <asp:HiddenField ID="PurchaseOrderHiddenField" runat="server" />
    <%--  --------------------------------------------------------------------PART-ONE--------------------------------------------------------------------------------------------%>
    <div class="row col-md-15" align="center">

        <br />
        <asp:ListView ID="outstandingListView" runat="server" DataSourceID="OutstandingODS" DataKeyNames="PurchaseOrderID">
            <AlternatingItemTemplate>
                <tr style="background-color: #FFFFFF; color: #5D7B9D;">
                    <td>
                        <asp:Label Text='<%# Eval("PON") %>' runat="server" ID="PONLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("OrderDate") %>' runat="server" ID="OrderDateLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("VendorName") %>' runat="server" ID="VendorNameLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Phone") %>' runat="server" ID="PhoneLabel" /></td>

                    <%--<asp:HiddenField ID="ProductOrderIDHF" runat="server" Value='<%# Eval("PurchaseOrderID") %>'/>--%>
                    <asp:Label Text='<%# Eval("PurchaseOrderID") %>' runat="server" ID="PurchaseOrderIDLabel" Visible="false" />

                    <td>
                        <asp:LinkButton ID="SelectOrder" runat="server" CommandArgument='<%# Eval("PurchaseOrderID") %>' OnCommand="SelectOrder_Command"> Select Order</asp:LinkButton>
                    </td>
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
                <tr style="background-color: #FFFFFF; color: #5D7B9D;">
                    <td>
                        <asp:Label Text='<%# Eval("PON") %>' runat="server" ID="PONLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("OrderDate") %>' runat="server" ID="OrderDateLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("VendorName") %>' runat="server" ID="VendorNameLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Phone") %>' runat="server" ID="PhoneLabel" /></td>

                    <asp:Label Text='<%# Eval("PurchaseOrderID") %>' runat="server" ID="PurchaseOrderIDLabel" Visible="false" />
                    <%--</td>
                      <%--  <asp:HiddenField ID="ProductOrderIDHF" runat="server" Value='<%# Eval("PurchaseOrderID") %>'/>--%>
                    <td>
                        <asp:LinkButton ID="SelectOrder" runat="server" CommandArgument='<%# Eval("PurchaseOrderID") %>' OnCommand="SelectOrder_Command"> Select Order</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                <tr runat="server" style="background-color: #FFFFFF; color: #333333;">
                                    <th runat="server" style="width: 50px">PON</th>
                                    <th runat="server" style="width: 200px">OrderDate</th>
                                    <th runat="server" style="width: 170px">VendorName</th>
                                    <th runat="server" style="width: 170px">Phone</th>
                                    <%-- <th runat="server" style="width:70px">PurchaseOrderID</th>--%>
                                    <th runat="server" style="width: 120px"></th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder"></tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center; background-color: #FFFFFF; font-family: Verdana, Arial, Helvetica, sans-serif; color: #5D7B9D; border-collapse: collapse; border-width: 1px; border-color: #999999;">
                            <asp:DataPager runat="server" ID="DataPager2">
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

        </asp:ListView>
    </div>
    <%--  --------------------------------------------------------------------PART-ONE-END---------------------------------------------------------------------------------------------%>
    <br />
    <br />
    <br />
    <br />
    <%---------------------------------------------------------------------------PART-TWO---------------------------------------------------------------------------------------------%>
    <div class="row col-md-15">
        <div>
            <asp:LinkButton ID="ReceiveLinkButton" runat="server" class="btn btn-info btn-sm" Visible="false" OnClick="ReceiveLinkButton_Click">Receive</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="CloseLinkButton" runat="server" class="btn btn-info btn-sm" Visible="false" OnClick="CloseLinkButton_Click">Force Close</asp:LinkButton>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;

            <asp:Label ID="ReasonLabel" runat="server" Text="Label" Visible="false">Reason</asp:Label>
            <asp:TextBox ID="ReasonTextBox" runat="server" Visible="false" Width="300px"></asp:TextBox>
        </div>
        <br />
        <asp:ListView ID="RFListView" runat="server" Visible="false">
            <AlternatingItemTemplate>
                <tr style="background-color: #FFFFFF; color: #284775;">


                    <asp:HiddenField Value='<%# Eval("PurchasePrderDetail") %>' runat="server" ID="PurchasePrderDetailIDHF1" />
                    <td>
                        <asp:Label Text='<%# Eval("SID") %>' runat="server" ID="SIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Desription") %>' runat="server" ID="DesriptionLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Ordered") %>' runat="server" ID="OrderedLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Outstanding") %>' runat="server" ID="OutstandingLabel" /></td>
                    <td>
                        <asp:TextBox ID="ReceivingTextBox" runat="server" Text="0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ReturningTextBox" runat="server" Text="0"></asp:TextBox>

                    <td>
                        <asp:TextBox ID="ReasonTextBox" runat="server"></asp:TextBox>
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
                <tr style="background-color: #FFFFFF; color: #5D7B9D;">

                    <asp:HiddenField Value='<%# Eval("PurchasePrderDetail") %>' runat="server" ID="PurchasePrderDetailIDHF1" />

                    <td>
                        <asp:Label Text='<%# Eval("SID") %>' runat="server" ID="SIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Desription") %>' runat="server" ID="DesriptionLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Ordered") %>' runat="server" ID="OrderedLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Outstanding") %>' runat="server" ID="OutstandingLabel" /></td>
                    <td>
                        <asp:TextBox ID="ReceivingTextBox" runat="server" Text="0"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="ReturningTextBox" runat="server" Text="0"></asp:TextBox>

                    <td>
                        <asp:TextBox ID="ReasonTextBox" runat="server"></asp:TextBox>
                </tr>

            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                <tr runat="server" style="background-color: #FFFFFF; color: #333333;">
                                    <th runat="server">SID</th>
                                    <th runat="server">Desription</th>
                                    <th runat="server">Ordered</th>
                                    <th runat="server">Outstanding</th>
                                    <th runat="server">Receiving</th>
                                    <th runat="server">Returning</th>
                                    <th runat="server">Reason</th>
                                    <%--  <th runat="server">QOS</th>
                                    <th runat="server">QtyO</th>
                                    <th runat="server">PurchaseOrderID</th>
                                    <th runat="server">PurchasePrderDetail</th>
                                    <th runat="server">CategoryID</th>--%>
                                </tr>
                                <tr runat="server" id="itemPlaceholder"></tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center; background-color: #FFFFFF; font-family: Verdana, Arial, Helvetica, sans-serif; color: #5D7B9D">
                            <asp:DataPager runat="server" ID="DataPager3">
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

        </asp:ListView>
        <br />

    </div>

    <%--  --------------------------------------------------------------------PART-TWO-END---------------------------------------------------------------------------------------------%>
    <br />
    <br />
    <br />
    <br />
    <%---------------------------------------------------------------------------PART-THREE---------------------------------------------------------------------------------------------%>

    <div>
        <asp:ListView ID="UnOrderedReturnsListView"
            runat="server" OnDataBound="UnOrderedReturnsListView_DataBound"
            InsertItemPosition="LastItem" DataKeyNames="CartID"
            Visible="false" DataSourceID="unorderedReturnListViewODS">

            <AlternatingItemTemplate>
                <tr style="background-color: #FFFFFF; color: #284775;">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("CartID") %>' runat="server" ID="CartIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("PurchaseOrderID") %>' runat="server" ID="PurchaseOrderIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("VendorStockNumber") %>' runat="server" ID="VendorStockNumberLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                </tr>
            </AlternatingItemTemplate>

            <EmptyDataTemplate>
                <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                       <%-- <asp:Button runat="server" CommandName="Cancel" Text="Remove" ID="CancelButton" />--%>
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("CartID") %>' runat="server" ID="CartIDTextBox" Visible="false" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("PurchaseOrderID") %>' runat="server" ID="PurchaseOrderIDTextBox" Visible="false" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("VendorStockNumber") %>' runat="server" ID="VendorStockNumberTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Quantity") %>' runat="server" ID="QuantityTextBox" /></td>

                    <%--<td>
                        <asp:TextBox Text='<%# Bind("PON") %>' runat="server" ID="PONTextBox" /></td>
                </tr>--%>
            </InsertItemTemplate>

            <EditItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                        <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                    </td>
                    <td>
                        <asp:TextBox Text='<%# Bind("CartID") %>' runat="server" ID="CartIDTextBox" Visible="false" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("PurchaseOrderID") %>' runat="server" ID="PurchaseOrderIDTextBox" Visible="false" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("VendorStockNumber") %>' runat="server" ID="VendorStockNumberTextBox" /></td>
                    <td>
                        <asp:TextBox Text='<%# Bind("Quantity") %>' runat="server" ID="QuantityTextBox" /></td>

                    <%--<td>
                        <asp:TextBox Text='<%# Bind("PON") %>' runat="server" ID="PONTextBox" /></td>
                </tr>--%>
            </EditItemTemplate>

            <ItemTemplate>
                <tr style="background-color: #E0FFFF; color: #333333;">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("CartID") %>' runat="server" ID="CartIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("PurchaseOrderID") %>' runat="server" ID="PurchaseOrderIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("VendorStockNumber") %>' runat="server" ID="VendorStockNumberLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                </tr>
            </ItemTemplate>

            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                    <th runat="server"></th>
                                    <th runat="server">CartID</th>
                                    <th runat="server">PurchaseOrderID</th>
                                    <th runat="server">Description</th>
                                    <th runat="server">VendorStockNumber</th>

                                    <th runat="server">Quantity</th>
                                </tr>
                                <tr runat="server" id="itemPlaceholder"></tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                            <asp:DataPager runat="server" ID="DataPager1">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>

            <SelectedItemTemplate>
                <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                    <td>
                        <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                    </td>
                    <td>
                        <asp:Label Text='<%# Eval("CartID") %>' runat="server" ID="CartIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("PurchaseOrderID") %>' runat="server" ID="PurchaseOrderIDLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("VendorStockNumber") %>' runat="server" ID="VendorStockNumberLabel" /></td>
                    <td>
                        <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
    </div>

    <%--  --------------------------------------------------------------------PART-THREE-END---------------------------------------------------------------------------------------------%>


    <%------Part-One-ODS-------%>
    <asp:ObjectDataSource runat="server" ID="OutstandingODS"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="List_PurchaseOrderForReceiveOrdersSelection"
        TypeName="eToolsSystem.BLL.ReceivingController"></asp:ObjectDataSource>


    

    <%------Part-Three-ODS-------%>

    <asp:ObjectDataSource runat="server"
        ID="unorderedReturnListViewODS"
        DataObjectTypeName="eTools.Data.Entities.UnorderedPurchaseItemCart"
        OnDeleted="CheckForException"
        OnInserted="CheckForException"
        OnUpdated="CheckForException"
        OnSelected="CheckForException"
        OldValuesParameterFormatString="original_{0}"
        TypeName="eToolsSystem.BLL.ReceivingController"
        DeleteMethod="UnOrderReturns_Delete"
        InsertMethod="UnOrderedReturns_Add"
        SelectMethod="UnOrderedReturns_List">
        <SelectParameters>
            <asp:ControlParameter ControlID="PurchaseOrderHiddenField"
                PropertyName="Value" Name="purchaseorderid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
