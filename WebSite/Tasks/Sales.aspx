<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Sales.aspx.cs" Inherits="Tasks_ViewCart" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <div style="background-color : lightgrey; padding:15px; margin-top: 20px;">
        <p style="font-size: 40px;"><b>Product Catalog  </b><span style="color:grey; font-size:20px;"><i class="fa fa-wrench"></i>  Available Online and In-Store</span></p>
    </div>
    <div class="row">
        <div class="col-sm-3">
            <h3>Browse by Category</h3>

            <div class="row" style="padding:5px 40px 5px 40px;background-color:cornflowerblue">
                <asp:LinkButton ID="All" CssClass="col-sm-10" ForeColor="white" runat="server" OnClick="All_Click">All</asp:LinkButton>
            </div>
            <asp:Repeater ID="CategoryRepeter" runat="server"
                          ItemType="eTools.Data.Entities.Pocos.CategoryList"
                          DataSourceID="ODSCategoryList">
                <ItemTemplate>
                    <div class="row" style="padding:5px 40px 5px 40px;color:cornflowerblue">
                        <asp:LinkButton ID="btn_category" class="col-sm-10" runat="server" CommandArgument="<%# Item.CategoryID %>" OnCommand="btn_category_Command"><%# Item.CategoryDescription %></asp:LinkButton><span style="background-color:lightgrey;border-radius:35%;padding:2px 10px"><%# Item.ItemCount %></span>
                    </div>
                    
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="col-sm-push-2 col-sm-7">
            <h2>Products</h2>

            <asp:Repeater ID="ItemReapter" runat="server"
                          ItemType="eTools.Data.Entities.StockItem" OnItemCommand="ItemReapter_ItemCommand"
                          >
                <ItemTemplate>
                    <div class="form-inline">
                        <asp:Button ID="Add" runat="server" Text="Add" CssClass="btn btn-default" BackColor="CornflowerBlue" ForeColor="White" height="30px" CommandName="Add" CommandArgument="<%# Item.StockItemID %>" />
                        <asp:LinkButton ID="Update" runat="server" CssClass="btn btn-default" ForeColor="black" height="30px" CommandName="Update" CommandArgument="<%# Item.StockItemID %>" Visible="false"><asp:label ID="itemCount" text="0" runat="server" /><span class="fa fa-shopping-cart"></span></asp:LinkButton>
                        <asp:Label ID="Icon" runat="server" heiht="30px"><span aria-hidden="true" class="fa fa-shopping-cart"></span>  0</asp:Label>
                        <asp:TextBox ID="Quantity" runat="server" TextMode="Number" CssClass="form-control" Width="100px">1</asp:TextBox>
                        <asp:Label ID="Label1" runat="server">$<%# Item.SellingPrice %> &nbsp  <%# Item.Description %> &nbsp  <%# Item.QuantityOnHand %>&nbsp in stock</asp:Label>
                    </div>
                </ItemTemplate>

            </asp:Repeater>
        </div>
    </div>

    
    <asp:ObjectDataSource ID="ODSCategoryList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCategoryList" TypeName="eTools.Application.BLL.SalesController"></asp:ObjectDataSource>
<%--    <asp:ObjectDataSource ID="ODSItemList" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetStockItemList" TypeName="eTools.Application.BLL.SalesController">
        <SelectParameters>
            <asp:Parameter Name="categoryid" Type="Int32"></asp:Parameter>
        </SelectParameters>
    </asp:ObjectDataSource>--%>
</asp:Content>

