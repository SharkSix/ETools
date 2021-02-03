<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PlaceOrder.aspx.cs" Inherits="Tasks_PlaceOrder" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <div style="font-size:23px; color:cornflowerblue; align-content:center;">
        <asp:Label ID="ContinueShopping" runat="server" Text="Label" CssClass="col-sm-3">Continue Shopping <i class="fa fa-tags"></i></asp:Label>
        <asp:Label ID="ViewCart" runat="server" Text="Label" CssClass="col-sm-3">View / Edit Cart <i class="fa fa-list"></i></asp:Label>
        <asp:Label ID="CustomrInformation" runat="server" Text="Label" CssClass="col-sm-3">Customer Information <i class="fa fa-comment"></i></asp:Label>
        <asp:Label ID="PlaceOrder" runat="server" Text="Label" CssClass="col-sm-3" BackColor="cornflowerblue" ForeColor="white">Place Order <i class="fa fa-check"></i></asp:Label>
    </div>
    <div style="padding-left:50px; padding-right:50px;">
        <h1 style="padding-top:100px;">Place Order</h1>
        <div>
            <asp:Repeater ID="OrderReapter" runat="server" ItemType="eTools.Data.Entities.Pocos.ShoppingCartItemList">
                <ItemTemplate>
                    <div class="form-inline">
                        <asp:Label ID="Description" runat="server" Text=<%# Item.Description %> CssClass="col-sm-4"></asp:Label> 
                        <asp:Label ID="Quantity" runat="server" Text=<%# Item.Quantity %> CssClass="col-sm-2"></asp:Label>
                        <asp:Label ID="SellingPrice" runat="server" Text=<%# Item.SellingPrice %> CssClass="col-sm-2"></asp:Label>
                        <asp:Label ID="TotalPrice" runat="server" Text=<%# Item.ItemTotal %> CssClass="col-sm-2"></asp:Label>
                        <br />
                        <br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="row">
            
            
            <div class="col-sm-offset-3 col-sm-4" style="padding-top:25px">
                <div class="form-inline">
                    <asp:TextBox ID="Coupon" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:LinkButton ID="Update" runat="server" OnClick="Update_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                
            </div>
            <div class="col-sm-5">
                <div class="form-inline" style="padding:15px">
                    <asp:Label ID="SubTotal" runat="server" Text="SubTotal: " Font-Bold="true" CssClass="col-sm-7"></asp:Label>
                    <asp:Label Text="0" runat="server" ID="lbl_subTotal" CssClass="col-sm-5" />
                </div>
                <div class="form-inline" style="padding:15px">
                    <asp:Label ID="Discount" runat="server" Text="Discount: " Font-Bold="true" CssClass="col-sm-7"></asp:Label>
                    <asp:Label Text="0" runat="server" ID="lbl_discount" CssClass="col-sm-5" />
                </div>

                <div class="form-inline" style="padding:15px">
                    <asp:Label ID="Total" runat="server" Text="Total: " Font-Bold="true" CssClass="col-sm-7"></asp:Label>
                    <asp:Label Text="0" runat="server" ID="lbl_total" CssClass="col-sm-5" />
                </div>
            </div>

            <div class="col-sm-offset-6 col-sm-6" style="padding:15px">
                <asp:RadioButton ID="Money" runat="server" Text="Cash" />
                <asp:RadioButton ID="Credit" runat="server" Text="Credit" />
                <asp:RadioButton ID="Debit" runat="server" Text="Debit" />
            </div>

            
        </div>
        
        <div class="col-sm-offset-9 col-sm-3">
            <a class="btn btn-primary" runat="server">Place Order &raquo;</a>
        </div>
    </div>

</asp:Content>

