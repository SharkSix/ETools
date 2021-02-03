<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CustomerInformation.aspx.cs" Inherits="Tasks_CustomerInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="font-size:23px; color:cornflowerblue; align-content:center;">
        <asp:Label ID="ContinueShopping" runat="server" Text="Label" CssClass="col-sm-3">Continue Shopping <i class="fa fa-tags"></i></asp:Label>
        <asp:Label ID="ViewCart" runat="server" Text="Label" CssClass="col-sm-3">View / Edit Cart <i class="fa fa-list"></i></asp:Label>
        <asp:Label ID="CustomrInformation" runat="server" Text="Label" CssClass="col-sm-3" BackColor="cornflowerblue" ForeColor="white">Customer Information <i class="fa fa-comment"></i></asp:Label>
        <asp:Label ID="PlaceOrder" runat="server" Text="Label" CssClass="col-sm-3">Place Order <i class="fa fa-check"></i></asp:Label>
    </div>

    <div style="padding-left:50px; padding-right:50px;">
        <h1 style="padding-top:100px;">Purchase Details</h1>
        <p style="color:darkgray">Enter your information for shipping and billing here.</p>

        <div>
            <h3>Billing Details</h3>
            <div class="form-group">
                <asp:Label ID="BillingName" runat="server"><b>Name</b></asp:Label>
                <asp:TextBox ID="BillingTextName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Name" runat="server" ErrorMessage="Must fill name of billing" ControlToValidate="BillingTextName" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="BillingEmail" runat="server"><b>Email</b></asp:Label>
                <asp:TextBox ID="BillingTextEmail" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Email" runat="server" ErrorMessage="Must fill email of billing" ControlToValidate="BillingTextEmail" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="BillingAddress" runat="server"><b>Address</b></asp:Label>
                <asp:TextBox ID="BillingTextAddress" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Address" runat="server" ErrorMessage="Must fill address of billing" ControlToValidate="BillingTextAddress" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="BillingPhone" runat="server"><b>Phone</b></asp:Label>
                <asp:TextBox ID="BillingTextPhone" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Phone" runat="server" ErrorMessage="Must fill phone number of billing" ControlToValidate="BillingTextPhone" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div>
            <h3>Shipping Details</h3>
            <div class="form-group">
                <asp:Label ID="ShippingName" runat="server"><b>Name</b></asp:Label>
                <asp:TextBox ID="ShippingTextName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Must fill name of shipping" ControlToValidate="ShippingTextName" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="ShippingEmail" runat="server"><b>Email</b></asp:Label>
                <asp:TextBox ID="ShippingTextEmail" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Must fill email of shipping" ControlToValidate="ShippingTextEmail" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="ShippingAddress" runat="server"><b>Address</b></asp:Label>
                <asp:TextBox ID="ShippingTextAddress" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Must fill address of shipping" ControlToValidate="ShippingTextAddress" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="ShippingPhone" runat="server"><b>Phone</b></asp:Label>
                <asp:TextBox ID="ShippingTextPhone" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Must fill phone number of shipping" ControlToValidate="ShippingTextPhone" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div>
            <a class="btn btn-primary" runat="server" href="~/Tasks/ViewCart.aspx">Return to Cart &raquo;</a>
            <a class="btn btn-primary" runat="server" href="~/Tasks/PlaceOrder.aspx">Proceed &raquo;</a>            
        </div>
    </div>

</asp:Content>

