<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="eToolsWebsite.eTools.Sales.Sales" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row jumbotron">
        <h1>Sales</h1>
    </div>
     <div class="row">
                 <div class="col-md-11" style="text-align:center; margin-left:80px;">
                    <asp:UpdatePanel ID="MessageUpdates" runat="server">
                        <ContentTemplate>
                            <%-- Message User Control --%>
                             <uc1:MessageUserControl runat="server" id="MessageUserControl"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   
                </div>
            </div>
    <div class="row">
        <div class="col-md-11">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#shopping" data-toggle="tab" id="shoppingOpen">Continue Shopping</a></li>
                <li>
                    <a href="#shoppingcart" data-toggle="tab" id="cartOpen">View Cart <input ID="notificationIcon" runat="server" type="button" class="btn btn-danger btn-xs" value="" style="visibility:hidden;"></a>                  
                </li>
                <li><a href="#checkout" data-toggle="tab" id="checkoutOpen">Checkout</a></li>
            </ul>
            <br />
           
           
            
            <div class="tab-content">
                <%-----------------------%>
                <%-- Continue Shopping --%>
                <%-----------------------%>
                <div class="tab-pane active" id="shopping">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <h2 style="text-align:left; margin-left:15px;">Continue Shopping</h2>
                                <br />
                                <div class="col-sm-2">
                                    <asp:Label ID="Label1" runat="server" Text="Select a Category:"></asp:Label>
                                    <asp:DropDownList ID="CategoryDDL" runat="server"
                                        DataSourceID="CategoryDDLODS"
                                        DataTextField="Description"
                                        DataValueField="CategoryID" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="CategoryDDL_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Select..." Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="" Text="All"></asp:ListItem>
                                    </asp:DropDownList>                                                                                                    
                                </div>
                                <br />
                                <%-- Category Items Count --%>

                                <div class="col-sm-2">
                                    <input class="form-control" runat="server" style="width: 60px; text-align:center;" id="categoryCount" type="text" value="0" disabled Text='<%# Eval("categoryCount") %>'>                                                                                                  
                                </div>

                                <div style="margin-top:-80px;" class="col-sm-7">

                                    <%-- StockItems ListView --%>

                                    <asp:ListView ID="stockList" runat="server" DataSourceID="StockItemListODS" 
                                        OnItemCommand="stockList_ItemCommand" OnSelectedIndexChanged="stockList_SelectedIndexChanged">
                                        <AlternatingItemTemplate>
                                            <tr style="background-color: #FFFFFF;" >
                                                <td>
                                                    <asp:LinkButton ID="AddtoCart" runat="server" Visible='<%#User_Link() %>'
                                                            CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("StockItemID") %>'>
                                                        <span class="glyphicon glyphicon-shopping-cart"></span> 
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <div class="input-group number-spinner">
                                                        <span class="input-group-btn">
                                                            <a class="btn btn-danger btn-sm" data-dir="dwn"><span class="glyphicon glyphicon-minus"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="QuantityLabel" runat="server" class="form-control text-center" Text="1" Max=999 min=1></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <a class="btn btn-primary btn-sm" data-dir="up"><span class="glyphicon glyphicon-plus"></span></a>
                                                        </span>
                                                    </div>
                                                </td>   
                                                <td>
                                                    <asp:Label Text='<%# Eval("Items") %>' runat="server" ID="ItemsLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("Price", "{0:c}") %>' runat="server" ID="PriceLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("QtyOnStock") %>' runat="server" ID="QtyOnStockLabel" /></td>
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
                                            <tr style="background-color: #f0f0f0; color: #000000;">
                                                <td>
                                                    <asp:LinkButton ID="AddtoCart" runat="server" Visible='<%#User_Link()%>'
                                                            CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("StockItemID") %>'>
                                                        <span class="glyphicon glyphicon-shopping-cart"></span> 
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <div class="input-group number-spinner">
                                                        <span class="input-group-btn">
                                                            <a class="btn btn-danger btn-sm" data-dir="dwn"><span class="glyphicon glyphicon-minus"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="QuantityLabel" runat="server" class="form-control text-center" Text="1" Max=999 min=1></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <a class="btn btn-primary btn-sm" data-dir="up"><span class="glyphicon glyphicon-plus"></span></a>
                                                        </span>
                                                    </div>
                                                </td> 
                                                <td>
                                                    <asp:Label Text='<%# Eval("Items") %>' runat="server" ID="ItemsLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("Price", "{0:c}") %>' runat="server" ID="PriceLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("QtyOnStock") %>' runat="server" ID="QtyOnStockLabel" /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <table runat="server">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <table runat="server" id="itemPlaceholderContainer" width="800px" style="background-color: #FFFFFF; text-align:center; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr runat="server" style="background-color: #fff; color: #000;">
                                                                <th runat="server" width="50px"></th>
                                                                <th runat="server" width="140px"></th>
                                                                <th runat="server">Items</th>
                                                                <th runat="server">Price</th>
                                                                <th runat="server">QtyOnStock</th>
                                                            </tr>
                                                            <tr runat="server" id="itemPlaceholder"></tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td runat="server" style="text-align: center; background-color: #f0f0f0; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000;">
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
                                    </asp:ListView>
                                    
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div style="text-align:right;">
                                    <asp:LinkButton ID="Cart" runat="server"
                                        class="btn btn-primary "                                      
                                        onclick="Cart_Click"
                                        onclientclick="openCart();"
                                        style="margin-right:-85px;"                                        
                                        >
                                        Cart
                                        <span class="glyphicon glyphicon-chevron-right"></span>  
                                    </asp:LinkButton>
                                                                          
                                </div>
                            </div>
                            <asp:ObjectDataSource runat="server" ID="CategoryDDLODS" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectMethod="Categories_List" 
                                TypeName="eToolsSystem.BLL.CategoryController">
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource runat="server" ID="StockItemListODS" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectMethod="List_StockItemsForCategorySelection" 
                                TypeName="eToolsSystem.BLL.StockItemsController">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CategoryDDL" PropertyName="SelectedValue" Name="categoryid" Type="Int32"></asp:ControlParameter>
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%-----------------------%>
                <%---- Shopping Cart ----%>
                <%-----------------------%>
                <div class="tab-pane" id="shoppingcart">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <h2 style="text-align:left; margin-left:15px;">Shopping Cart</h2>
                                <br />
                                <div style="margin-top:-25px;" class="col-sm-9">                                 

                                    <%-- StockItems ListView --%>
                                    <asp:ListView ID="ShoppingCartList" runat="server" OnItemCommand="ShoppingCartList_ItemCommand">
                                        <AlternatingItemTemplate>
                                            <tr style="background-color: #FFFFFF;" >                                                 
                                                <td>
                                                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                                <td>
                                                    <div class="input-group number-spinner">
                                                        <span class="input-group-btn">
                                                            <a class="btn btn-danger btn-sm" data-dir="dwn"><span class="glyphicon glyphicon-minus"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="QuantityLabel" Text='<%# Eval("Quantity") %>' runat="server" class="form-control text-center" Max=999 min=1></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <a class="btn btn-primary btn-sm" data-dir="up"><span class="glyphicon glyphicon-plus"></span></a>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>                                                   
                                                    <asp:Label Text='<%# Eval("Price", "{0:c}") %>' runat="server" ID="PriceLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("Total", "{0:c}") %>' runat="server" ID="TotalLabel" /></td>
                                                <td>
                                                    <asp:LinkButton ID="RefreshCart" runat="server"
                                                            CssClass="btn btn-info btn-xs" CommandArgument='<%# Eval("ItemID") %>' CommandName="refresh">
                                                        <span class="glyphicon glyphicon-refresh"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="RemoveFromCart" runat="server"
                                                            CssClass="btn btn-danger btn-xs" CommandArgument='<%# Eval("ItemID") %>' CommandName="remove">
                                                        <span class="glyphicon glyphicon-trash"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="display:none">
                                                    <asp:HiddenField ID="ItemIDLabel" runat="server" Value='<%# Eval("ShoppingItemID") %>' />
                                                </td>
                                           <%--     <td style="display:none">
                                                    <asp:HiddenField ID="StockItemIDLabel" runat="server" Value='<%# Eval("StockItemID") %>' /> 
                                                </td>--%>
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
                                            <tr style="background-color: #f0f0f0; color: #000000;">                                               
                                                <td>
                                                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>                                                
                                                <td>
                                                    <div class="input-group number-spinner">
                                                        <span class="input-group-btn">
                                                            <a class="btn btn-danger btn-sm" data-dir="dwn"><span class="glyphicon glyphicon-minus"></span></a>
                                                        </span>
                                                        <asp:TextBox ID="QuantityLabel" Text='<%# Eval("Quantity") %>' runat="server" class="form-control text-center" max=999 min=1></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <a class="btn btn-primary btn-sm" data-dir="up"><span class="glyphicon glyphicon-plus"></span></a>
                                                        </span>
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("Price", "{0:c}") %>' runat="server" ID="PriceLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("Total", "{0:c}") %>' runat="server" ID="TotalLabel" /></td>
                                                <td>
                                                    <asp:LinkButton ID="RefreshCart" runat="server"
                                                            CssClass="btn btn-info btn-xs" CommandArgument='<%# Eval("ItemID") %>' CommandName="refresh">
                                                        <span class="glyphicon glyphicon-refresh"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="RemoveFromCart" runat="server"
                                                            CssClass="btn btn-danger btn-xs" CommandArgument='<%# Eval("ItemID") %>' CommandName="remove">
                                                        <span class="glyphicon glyphicon-trash"></span>
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="display:none">
                                                    <asp:HiddenField ID="ItemIDLabel" runat="server" Value='<%# Eval("ShoppingItemID") %>' />
                                                </td>
   <%--                                             <td style="display:none">
                                                    <asp:HiddenField ID="StockItemIDLabel" runat="server" Value='<%# Eval("StockItemID") %>' /> 
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <table runat="server">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <table runat="server" id="itemPlaceholderContainer" width="800px" style="background-color: #FFFFFF; text-align:center; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr runat="server" style="background-color: #fff; color: #000;">
                                                                <th runat="server">Description</th>
                                                                <th runat="server" width="140px">Quantity</th>
                                                                <th runat="server">Price</th>
                                                                <th runat="server">Total</th>
                                                                <th runat="server"></th>
                                                                <th runat="server"></th>
                                                                <th runat="server"></th>
                                                            </tr>
                                                            <tr runat="server" id="itemPlaceholder"></tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td runat="server" style="text-align: center; background-color: #f0f0f0; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000;">
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
                                    </asp:ListView>                                   
                                </div>
                                <div class="col-sm-2" style="text-align:center; margin-left:30px; padding:20px;">
                                    <asp:Label Text="TOTAL" runat="server" ID="TotalbuttonLabel" />
                                    <br />&nbsp
                                    <br />
                                    <asp:Label ID="totalLabel" runat="server" Text="0" CssClass="btn btn-info"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-5" style="text-align:left;">
                                    <asp:LinkButton ID="Shopping" runat="server"
                                        class="btn btn-primary "                                      
                                        onclientclick="openShopping();"                                        
                                        >
                                        <span class="glyphicon glyphicon-chevron-left"></span>
                                         Shopping  
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-6" style="text-align:right;">
                                    <asp:LinkButton ID="Checkout" runat="server"
                                        class="btn btn-primary "                                      
                                        onclick="Checkout_Click"
                                        onclientclick="openCheckout();"
                                        style="margin-right:-190px;"                                       
                                        >
                                        Checkout
                                        <span class="glyphicon glyphicon-chevron-right"></span>  
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%-----------------------%>
                <%------- Checkout ------%>
                <%-----------------------%>
                <div class="tab-pane" id="checkout">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row">                              
                                <h2 style="text-align:left; margin-left:15px;">CheckOut</h2>
                                <br />
                                <%-- Checkout listview --%>
                                <div style="margin-top:-25px; text-align:center; margin-left:150px;" class="col-sm-11">
                                    <asp:ListView ID="SaleList" runat="server">
                                        <AlternatingItemTemplate>
                                            <tr style="background-color: #FFFFFF;">
                                                <td>
                                                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("Price", "{0:c}") %>' runat="server" ID="PriceLabel" /></td>  
                                                <td>
                                                    <asp:Label Text='<%# Eval("Total", "{0:c}") %>' runat="server" ID="TotalLabel" /></td>                                         
                                                <td style="display:none">
                                                    <asp:HiddenField ID="StockItemIDLabel" runat="server" Value='<%# Eval("StockItemID") %>' />
                                                </td>
                                                <td style="display:none">
                                                    <asp:HiddenField ID="ItemIDLabel" runat="server" Value='<%# Eval("ItemID") %>' />
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
                                            <tr style="background-color: #f0f0f0; color: #000000;">
                                                <td>
                                                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                                                <td>
                                                    <asp:Label Text='<%# Eval("Price", "{0:c}") %>' runat="server" ID="PriceLabel" /></td>  
                                                <td>
                                                    <asp:Label Text='<%# Eval("Total", "{0:c}") %>' runat="server" ID="TotalLabel" /></td>                                         
                                                <td style="display:none">
                                                    <asp:HiddenField ID="StockItemIDLabel" runat="server" Value='<%# Eval("StockItemID") %>' />
                                                </td>
                                                <td style="display:none">
                                                    <asp:HiddenField ID="ItemIDLabel" runat="server" Value='<%# Eval("ItemID") %>' />
                                                </td>                                                                                      
                                            </tr>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <table runat="server">
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <table runat="server" id="itemPlaceholderContainer" width="800px" style="background-color: #FFFFFF; text-align:center; font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                            <tr runat="server" style="background-color: #fff; color: #000;">
                                                                <th runat="server">Description</th>
                                                                <th runat="server">Quantity</th>
                                                                <th runat="server">Price</th>                                                                                                                       
                                                                <th runat="server">Total</th>
                                                                <th runat="server"></th>
                                                                <th runat="server"></th>
                                                            </tr>
                                                            <tr runat="server" id="itemPlaceholder"></tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr runat="server">
                                                    <td runat="server" style="text-align: center; background-color: #f0f0f0; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000">
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
                                
                                <%-- Checkout Details --%>
                                <div class="row">
                                    <div style="text-align:center; margin-left:150px;">
                                        <div class="col-sm-6" style="text-align:center; margin-top:30px;">
                                            <asp:Label Text="COUPON" runat="server" ID="Label3" />&nbsp
                                            <asp:TextBox ID="CouponTextBox" runat="server"></asp:TextBox>
                                            <asp:LinkButton ID="CheckCouponButton" runat="server" class="btn btn-success" OnClick="CheckCouponButton_Click"
                                                ><span class="glyphicon glyphicon-ok"></span> </asp:LinkButton>
                                        </div>
                                        <br />
                                        <div class="col-sm-6" style="text-align:left; margin-top:30px; width:230px;">
                                            <asp:DropDownList CssClass="form-control" ID="PaymentTypeDDL" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    
                                    <div class="col-sm-5" style="text-align:right; margin-left:595px; margin-top:30px;">
                                        <asp:Label Text="SUBTOTAL" runat="server" ID="Label2" />&nbsp
                                        <asp:Label ID="subtotalLabel" runat="server" Text="0" CssClass="btn btn-info"></asp:Label>
                                    </div>
                                    <div class="col-sm-5" style="text-align:right; margin-left:595px; margin-top:10px;">
                                        <asp:Label Text="TAX" runat="server" ID="Label4" />&nbsp
                                        <asp:Label ID="TaxLabel" runat="server" Text="0" CssClass="btn btn-info"></asp:Label>
                                    </div>
                                    <div class="col-sm-5" style="text-align:right; margin-left:595px; margin-top:10px;">
                                        <asp:Label Text="" runat="server" ID="PercentageLabel" />
                                        <asp:Label Text="% DISCOUNT" runat="server" ID="Label6" />&nbsp
                                        <asp:Label ID="DiscountLabel" runat="server" Text="0" CssClass="btn btn-info"></asp:Label>
                                    </div>
                                    <div class="col-sm-5" style="text-align:right; margin-left:595px; margin-top:10px;">
                                        <asp:Label Text="TOTAL" runat="server" ID="Label8" />&nbsp
                                        <asp:Label ID="totalLabel2" runat="server" Text="0" CssClass="btn btn-info"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-sm-5" style="text-align:left;">
                                    <asp:LinkButton ID="ShoppingCart" runat="server"
                                        class="btn btn-primary "
                                        onclick="ShoppingCart_Click"                                   
                                        onclientclick="openCart();"                                        
                                        >
                                        <span class="glyphicon glyphicon-chevron-left"></span>
                                         Cart  
                                    </asp:LinkButton>
                                </div>
                                <div class="col-sm-6" style="text-align:right;">
                                    <asp:LinkButton ID="PlaceOrder" runat="server"
                                        class="btn btn-primary "                                      
                                        onclick="PlaceOrder_Click"
                                        OnClientClick="if (!confirm('Are you sure you want to place this order?')) return false;"
                                        style="margin-right:-190px;"                                        
                                        >
                                        Place Order
                                        <span class="glyphicon glyphicon-chevron-right"></span>  
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <%-- eoc --%>
        <div class="col-md-1">
            <asp:Button class="btn btn-primary" ID="Cancel" runat="server" Text="Cancel" onclick="Cancel_Click"/>
        </div>
    </div>
    <%----------------%>
    <%-- JAVASCRIPT --%>
    <%----------------%>
    <script type="text/javascript">
        $(function () {
            // spinner(+-btn to change value) & total to parent input 
            $(document).on('click', '.number-spinner a', function () {
                var btn = $(this),
                input = btn.closest('.number-spinner').find('input'),
                total = $('#passengers').val(),
                oldValue = input.val().trim();

                if (btn.attr('data-dir') == 'up') {
                    if (oldValue < input.attr('max')) {
                        oldValue++;
                        total++;
                    }
                } else {
                    if (oldValue > input.attr('min')) {
                        oldValue--;
                        total--;
                    }
                }
                $('#passengers').val(total);
                input.val(oldValue);
            });
            $(".popover-markup>.trigger").popover('show');
        });
        function openCart() {
            $('#cartOpen').trigger('click')
            return false;
        }
        function openShopping() {
            $('#shoppingOpen').trigger('click')
        }
        function openCheckout() {
            $('#checkoutOpen').trigger('click')
        }
    </script>
</asp:Content>
