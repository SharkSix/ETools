<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Purchasing.aspx.cs" Inherits="Tasks_Product" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <h1>Purchasing</h1>
    </div>
    
    <div class="row">
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>


    <div>
        <asp:Label ID="VendorLabel" runat="server" Text="Vendor:"></asp:Label>
        <asp:DropDownList ID="VendorDropDownList" runat="server" 
            DataSourceID="VendorListODS" 
            DataTextField="VendorName" 
            DataValueField="VendorID" 
            AppendDataBoundItems="True">
            <Items>
            <asp:ListItem Value="0">Select Vendor</asp:ListItem>
            </Items>
        </asp:DropDownList>
         <asp:Button ID="GetOrders" runat="server" Text="Preview Suggested Order" CssClass="btn btn-primary"/>
        <br />
        <asp:Label ID="EmployeeLabel" runat="server" Text="Employee:"></asp:Label>
        <asp:DropDownList ID="EmployeeDropDownList" runat="server" 
            DataSourceID="EmployeeDataSource" 
            DataTextField="EmployeeName" 
            DataValueField="EmployeeID"
            AppendDataBoundItems="true"></asp:DropDownList>
        <br />
        </div>
        <asp:ObjectDataSource ID="VendorListODS" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="ListVendors" 
            TypeName="eTools.Application.BLL.PurchasingController">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="EmployeeDataSource" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="ListEmployees" 
            TypeName="eTools.Application.BLL.PurchasingController">
        </asp:ObjectDataSource>

        <div>
            <asp:Repeater ID="POrderInfoRepeater" runat="server" DataSourceID="POrderODS">
                <ItemTemplate>
                    Purchase Order#: <%# Eval("ID") %> &nbsp;&nbsp;
                    Vendor: <%# Eval("Name") %> &nbsp;&nbsp;
                    Location: <%# Eval("Location") %> &nbsp;&nbsp;
                    Phone: <%# Eval("Phone") %> &nbsp;&nbsp;
                    </ItemTemplate>
                </asp:Repeater>



                    <asp:GridView ID="POrderResultsList" runat="server"
                        OnSelectedIndexChanging="POrderResultsList_SelectedIndexChanging"
                        AutoGenerateColumns="False"
                        OnRowDeleting="POrderResultsList_RowDeleting" ItemType="eTools.Data.Entities.Pocos.PurchaseOrdersList"
                        ViewStateMode="Enabled" DataSourceID="PurchasedItemsODS">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" SortExpression="StockItemID">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("StockItemID") %>' ID="StockNumb"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Description") %>' ID="Desc"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QOH" SortExpression="QuantityOnHand">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("QuantityOnHand") %>' ID="QOH"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ROL" SortExpression="ReOrderLevel">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("ReOrderLevel") %>' ID="ROL"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QOO" SortExpression="QuantityOnOrder">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("QuantityOnOrder") %>' ID="QOO"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="POQty" >
                                <ItemTemplate>
                                    <asp:TextBox ID="PurchaseOrderQuantity" runat="server" Text='<%# Eval("PurchaseOrderQuantity") %>' Width="70px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="$$" >
                                <ItemTemplate>
                                    <asp:TextBox ID="PPrice" runat="server" Text='<%# Eval("PPrice") %>' Width="70px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="true" ShowDeleteButton="false" HeaderText="" SelectText="Remove"></asp:CommandField>
                        </Columns>

                    </asp:GridView>

            <asp:Repeater ID="POrderInfoRepeater2" runat="server" DataSourceID="POrderODS">
                <ItemTemplate>
                    Subtotal: $<%# Eval("Subtotal") %>&nbsp;&nbsp;
                    GST: $<%# Eval("GST") %>&nbsp;&nbsp;
                    Total: $<%# Eval("Total") %>&nbsp;&nbsp;
                    <br />

                </ItemTemplate>
            </asp:Repeater>
                    
                <asp:ObjectDataSource runat="server" ID="PurchasedItemsODS" OldValuesParameterFormatString="original_{0}" SelectMethod="ListPurchaseOrder" TypeName="eTools.Application.BLL.PurchasingController">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="VendorDropDownList" PropertyName="SelectedValue" Name="vendorid" Type="Int32"></asp:ControlParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
            
            <asp:ObjectDataSource runat="server" ID="POrderODS" OldValuesParameterFormatString="original_{0}" SelectMethod="GetPurchaseOrderInfo" TypeName="eTools.Application.BLL.PurchasingController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="VendorDropDownList" DefaultValue="" Name="vendorid" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br>
        <asp:LinkButton ID="DeleteOrderButton" runat="server" OnClick="DeleteOrderButton_Click">Delete Order</asp:LinkButton> 
        <asp:LinkButton ID="PlaceOrderButton" runat="server" OnClick="PlaceOrderButton_Click">Place Order</asp:LinkButton>
        <asp:LinkButton ID="ClearButton" runat="server" OnClick="ClearButton_Click">Clear</asp:LinkButton>
            <br />
            <br />
                    
            <asp:GridView ID="OtherItemsView" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanging="OtherItemsView_SelectedIndexChanging"
                DataSourceID="OtherItemsODS">
                <Columns>
                    <asp:BoundField DataField="ItemID" HeaderText="ItemID" SortExpression="ItemID"></asp:BoundField>
                    <asp:BoundField DataField="Desc" HeaderText="Desc" SortExpression="Desc"></asp:BoundField>
                    <asp:BoundField DataField="QOH" HeaderText="QOH" SortExpression="QOH"></asp:BoundField>
                    <asp:BoundField DataField="ROL" HeaderText="ROL" SortExpression="ROL"></asp:BoundField>
                    <asp:BoundField DataField="QOO" HeaderText="QOO" SortExpression="QOO"></asp:BoundField>
                    <asp:BoundField DataField="Buffer" HeaderText="Buffer" SortExpression="Buffer"></asp:BoundField>
                    <asp:BoundField DataField="PurchasePrice" HeaderText="$$" SortExpression="PurchasePrice"></asp:BoundField>

                    <asp:CommandField ShowSelectButton="true" ShowDeleteButton="false" HeaderText="" SelectText="Add to Order"></asp:CommandField>
                </Columns>
            </asp:GridView>
        <asp:ObjectDataSource runat="server" ID="OtherItemsODS" OldValuesParameterFormatString="original_{0}" SelectMethod="ListOrderInfo" TypeName="eTools.Application.BLL.PurchasingController">
            <SelectParameters>
                <asp:ControlParameter ControlID="VendorDropDownList" PropertyName="SelectedValue" Name="vendorid" Type="Int32"></asp:ControlParameter>
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>

    
</asp:Content>

