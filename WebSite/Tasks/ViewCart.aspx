<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewCart.aspx.cs" Inherits="Tasks_ViewCart" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <div style="font-size:23px; color:cornflowerblue; align-content:center;">
        <asp:Label ID="ContinueShopping" runat="server" Text="Label" CssClass="col-sm-3">Continue Shopping <i class="fa fa-tags"></i></asp:Label>
        <asp:Label ID="ViewCart" runat="server" Text="Label" CssClass="col-sm-3" BackColor="cornflowerblue" ForeColor="white">View / Edit Cart <i class="fa fa-list"></i></asp:Label>
        <asp:Label ID="CustomrInformation" runat="server" Text="Label" CssClass="col-sm-3">Customer Information <i class="fa fa-comment"></i></asp:Label>
        <asp:Label ID="PlaceOrder" runat="server" Text="Label" CssClass="col-sm-3">Place Order <i class="fa fa-check"></i></asp:Label>
    </div>
    <div style="padding-left:50px; padding-right:50px;">
        <h1 style="padding-top:100px;">Your Shopping Cart</h1>
        <div>
            <asp:Repeater ID="CartItemReapter" runat="server" ItemType="eTools.Data.Entities.Pocos.ShoppingCartItemList" OnItemCommand="CartItemReapter_ItemCommand">
                <ItemTemplate>
                    <div class="form-inline">
                        <asp:Label ID="Description" runat="server" Text=<%# Item.Description %> CssClass="col-sm-4"></asp:Label> 
                        <asp:Label ID="Quantity" runat="server" Text=<%# Item.Quantity %> CssClass="col-sm-2"></asp:Label>
                        <asp:Label ID="SellingPrice" runat="server" Text=<%# Item.SellingPrice %> CssClass="col-sm-2"></asp:Label>
                        <asp:Label ID="TotalPrice" runat="server" Text=<%# Item.ItemTotal %> CssClass="col-sm-2"></asp:Label>
                        <asp:LinkButton ID="Delete" runat="server" CommandArgument=<%# Item.ShoppingCartItemID %> CommandName="Remove"><i class="fa fa-remove"></i></asp:LinkButton>
                        <asp:TextBox ID="QuanityChange" runat="server" CssClass="form-control" Width="50px" Text='<%#Eval("Quantity") %>' TextMode="Number"></asp:TextBox>
                        <asp:LinkButton ID="Update" runat="server" CommandArgument=<%# Item.ShoppingCartItemID %> CommandName="Update"><i class="fa fa-refresh"></i></asp:LinkButton>
                        <br />
                        <br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div>
                <asp:Label ID="Total" runat="server" Text="Total: " Font-Bold="true" CssClass="col-sm-offset-7 col-sm-1"></asp:Label>
                <asp:Label ID="SubTotal" Text="0" runat="server" CssClass="col-sm-4" />
            </div>
            <div>
                <a class="btn btn-primary" runat="server" href="~/Tasks/Sales.aspx">Continue Shopping &raquo;</a>
                <a class="btn btn-primary" runat="server" href="~/Tasks/CustomerInformation.aspx">Proceed &raquo;</a>            
            </div>
        </div>
    </div>
</asp:Content>

