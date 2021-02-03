using eTools.Application.BLL;
using eTools.Data.Entities;
using eTools.Data.Entities.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tasks_Product : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void PreviewButton_Click(object sender, EventArgs e)
    {

    }

    protected void DeleteOrderButton_Click(object sender, EventArgs e)
    {

    }

    protected void PlaceOrderButton_Click(object sender, EventArgs e)
    {

    }

    protected void ClearButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Tasks/Purchasing.aspx");
    }

    protected void POrderResultsList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void POrderResultsList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int index = e.NewSelectedIndex;
        GridViewRow row = POrderResultsList.Rows[index];
        int itemID = int.Parse(row.Cells[0].Text);
        Label stockLabel = row.FindControl("StockNumb") as Label;
        PurchaseOrdersList vendoritem = new PurchaseOrdersList();

        List<PurchaseOrdersList> existing = OtherItemsGridview(OtherItemsView);
        existing.Add(vendoritem);
        OtherItemsView.DataSource = existing;
        OtherItemsView.DataBind();

        List<PurchaseOrdersList> existingData = new List<PurchaseOrdersList>();
        foreach (GridViewRow theRow in OtherItemsView.Rows)
        {
            itemID = int.Parse(theRow.Cells[0].Text);
            existingData.Add(new PurchaseOrdersList());
        }
        existingData.RemoveAt(index);
        OtherItemsView.DataSource = existingData;
        OtherItemsView.DataBind();
    }

    private List<PurchaseOrdersList> OtherItemsGridview(GridView gv)
    {
        List<PurchaseOrdersList> lists = new List<PurchaseOrdersList>();

        foreach (GridViewRow row in gv.Rows)
        {
            int stockid = int.Parse(row.Cells[0].Text);
            string desc = row.Cells[1].Text;
            int qoh = int.Parse(row.Cells[2].Text);
            int rol = int.Parse(row.Cells[3].Text);
            int qoo = int.Parse(row.Cells[4].Text);
            int pqty = int.Parse(row.Cells[5].Text);
            decimal pprice = int.Parse(row.Cells[6].Text);

            // Now I can create a ship object
            PurchaseOrdersList frontLine = new PurchaseOrdersList();
            // Add it to my list
            lists.Add(frontLine);
        }

        return lists;
    }


    protected void OtherItemsView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int index = e.NewSelectedIndex;
        GridViewRow row = OtherItemsView.Rows[index];
        int itemID = int.Parse(row.Cells[0].Text);
        Label stockLabel = row.FindControl("StockNumb") as Label;
        PurchaseOrdersList vendoritem = new PurchaseOrdersList();

        List<PurchaseOrdersList> existing = BoughtItemsGridView(POrderResultsList);
        existing.Add(vendoritem);
        OtherItemsView.DataSource = existing;
        OtherItemsView.DataBind();

        List<PurchaseOrdersList> existingData = new List<PurchaseOrdersList>();
        foreach (GridViewRow theRow in OtherItemsView.Rows)
        {
            itemID = int.Parse(theRow.Cells[0].Text);
            existingData.Add(new PurchaseOrdersList());
        }
        existingData.RemoveAt(index);
        OtherItemsView.DataSource = existingData;
        OtherItemsView.DataBind();
    }

    private List<PurchaseOrdersList> BoughtItemsGridView(GridView gv)
    {
        List<PurchaseOrdersList> lists = new List<PurchaseOrdersList>();

        foreach (GridViewRow row in gv.Rows)
        {
            int stockid = int.Parse(row.Cells[0].Text);
            string desc = row.Cells[1].Text;
            int qoh = int.Parse(row.Cells[2].Text);
            int rol = int.Parse(row.Cells[3].Text);
            int qoo = int.Parse(row.Cells[4].Text);
            int pqty = int.Parse(row.Cells[5].Text);
            decimal pprice = int.Parse(row.Cells[6].Text);

            // Now I can create a ship object
            PurchaseOrdersList frontLine = new PurchaseOrdersList();
            // Add it to my list
            lists.Add(frontLine);
        }

        return lists;
    }
}