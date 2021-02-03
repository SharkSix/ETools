<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Receiving.aspx.cs" Inherits="Tasks_Staff" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row jumbotron">
        <h1>Receiving</h1>
    </div>
    <div class="row">
        <uc1:MessageUserControl runat="server" id="MessageUserControl" />
    </div>

    <div ID="OutstandingOrders" runat="server" class="row"  >
        <h2>Outstanding Orders</h2>
        <asp:GridView ID="ShowPurchaseOrders" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="ShowPurchaseOrders_SelectedIndexChanging"
            DataSourceID="OpenPurchaseOrders" CssClass="table table-bordered table-condensed" HeaderStyle-BackColor="#ADDFEE" RowStyle-BackColor="#DEF2F8" AlternatingRowStyle-BackColor="White">
            <Columns>
                <asp:BoundField DataField="OrderNumber" HeaderText="Order" SortExpression="OrderNumber"></asp:BoundField>
                <asp:BoundField DataField="DateOrdered" HeaderText="Order Date" SortExpression="DateOrdered" DataFormatString="{0:MMM dd, yyyy}"></asp:BoundField>
                <asp:BoundField DataField="Name" HeaderText="Vendor" SortExpression="Name"></asp:BoundField>
                <asp:BoundField DataField="ContactPhone" HeaderText="Contact Phone" SortExpression="ContactPhone"></asp:BoundField>
                <asp:CommandField ShowSelectButton="True" HeaderText="" SelectText="View Order"></asp:CommandField>
            </Columns>
            <EmptyDataTemplate>
                <p>No records found.</p>
            </EmptyDataTemplate>
        </asp:GridView>
    </div><br />

    <div ID="OrderInfo" runat="server" class="row" visible="false">
        <div>
            <b>PO #</b>
            <asp:Label ID="PONum" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
            <b>Vendor: </b>
            <asp:Label ID="Vendor" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
            <b>Contact Phone: </b>
            <asp:Label ID="Phone" runat="server" Text=""></asp:Label>
        </div>
        <asp:GridView ID="ShowOrderDetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-condensed" HeaderStyle-BackColor="#ADDFEE" RowStyle-BackColor="#DEF2F8" AlternatingRowStyle-BackColor="#EEF8FB">
            <Columns>
                <asp:TemplateField HeaderText="Stock #">
                    <ItemTemplate>
                        <asp:Label ID="StockNum" runat="server" Text='<%# Eval("ItemID") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Description" runat="server" Text='<%# Eval("ItemDescription") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ordered">
                    <ItemTemplate>
                        <asp:Label ID="QtyOrdered" runat="server" Text='<%# Eval("Ordered") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Outstanding">
                    <ItemTemplate>
                        <asp:Label ID="QtyOutstanding" runat="server" Text='<%# Eval("Outstanding") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Received">
                    <ItemTemplate>
                        <asp:TextBox ID="QtyReceived" runat="server" Text='<%# Eval("Received") %>' ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Returned">
                    <ItemTemplate>
                        <asp:TextBox ID="QtyReturned" runat="server" Text='<%# Eval("Returned") %>' ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reason">
                    <ItemTemplate>
                        <asp:TextBox ID="ReasonReturned" runat="server" Text='<%# Eval("Reason") %>' ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div><br />
    <asp:ObjectDataSource runat="server" ID="OpenPurchaseOrders" OldValuesParameterFormatString="original_{0}" SelectMethod="GetOpenPurchaseOrders" TypeName="eTools.Application.BLL.ReceivingController"></asp:ObjectDataSource>

    <div ID="OrderControls" runat="server" class="row" visible="false">
        <asp:LinkButton ID="ReceiveNewOrder" runat="server" Text="Receive" OnClick="ReceiveOrder_Click" /> &nbsp;&nbsp;
        <asp:LinkButton ID="ForceClose" runat="server" Text="Force Close" OnClick="ForceClose_Click" /> &nbsp;&nbsp;
        Reason: 
        <asp:TextBox ID="ReasonForceClosed" runat="server" Text="" ></asp:TextBox>
    </div>

</asp:Content>

