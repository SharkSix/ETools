using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eTools.Application.BLL;
using Microsoft.AspNet.Identity;
using eTools.Data.Entities;
using eTools.Data;
using System.Data;

public partial class Tasks_ViewCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SalesController controller = new SalesController();
            int catId = 0;
            ItemReapter.DataSource = controller.GetStockItemList(catId);
            ItemReapter.DataBind();
        }

    }


    protected void btn_category_Command(object sender, CommandEventArgs e)
    {
        int catId = int.Parse(e.CommandArgument.ToString());

        SalesController controller = new SalesController();

        ItemReapter.DataSource = null;
        ItemReapter.DataSource = controller.GetStockItemList(catId);
        ItemReapter.DataBind();
    }

    protected void All_Click(object sender, EventArgs e)
    {
        SalesController controller = new SalesController();
        int id = 0;
        ItemReapter.DataSource = null;
        ItemReapter.DataSource = controller.GetStockItemList(id);
        ItemReapter.DataBind();
    }

    protected void ItemReapter_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            //int i;

            int itemCode = Convert.ToInt32(e.CommandArgument.ToString());
            string username = User.Identity.GetUserName();
            DateTime Today = DateTime.Today;

            TextBox quantity = e.Item.FindControl("Quantity") as TextBox;
            int counts = Convert.ToInt32(quantity.Text);

            OnlineCustomer newCustomer = new OnlineCustomer();

            SalesController sysmgr = new SalesController();

            if (sysmgr.GetOnlineCustomerIDByUserName(username) == 0)
            {
                newCustomer.UserName = username;
                newCustomer.CreatedOn = Today;
                int onlineCustomerId = sysmgr.AddOnlineCustomer(newCustomer);
            }

            if (sysmgr.GetShoppingCartByUserName(username) == 0)
            {
                ShoppingCart newShoppingCart = new ShoppingCart();
                newShoppingCart.OnlineCustomerID = sysmgr.FindCustomerID(username);
                newShoppingCart.CreatedOn = Today;
                sysmgr.AddShoppingCart(newShoppingCart);
            }

            MessageUserControl.TryRun(() =>
            {
                sysmgr.AddToShoppingCart(itemCode, username, counts);
                Button add = e.Item.FindControl("Add") as Button;
                LinkButton update = e.Item.FindControl("Update") as LinkButton;
                Label itemCount = e.Item.FindControl("itemCount") as Label;

                add.Visible = false;
                update.Visible = true;
                itemCount.Text = quantity.Text;

            }, "Success", "New Item has been added to your shopping cart.");




        }

        if (e.CommandName == "Update")
        {


            if (int.Parse((e.Item.FindControl("Quantity") as TextBox).Text) < 1)
            {
                MessageUserControl.ShowInfo("Quantity should greater than 0.");
            }
            else
            {
                SalesController sysmgr = new SalesController();

                string username = User.Identity.GetUserName();
                int quantity = int.Parse((e.Item.FindControl("Quantity") as TextBox).Text);
                int itemCode = Convert.ToInt32(e.CommandArgument.ToString());
                int OnlineCustomerID = sysmgr.FindCustomerID(username);
                int shoppingCartItemID = sysmgr.GetShoppingCartItemID(OnlineCustomerID);

                MessageUserControl.TryRun(() =>
                {

                    sysmgr.UpdateToShoppingCart(itemCode, shoppingCartItemID, username, quantity);
                    Label itemCount = e.Item.FindControl("itemCount") as Label;

                    itemCount.Text = (int.Parse(itemCount.Text) + quantity).ToString();


                }, "Success", "Item quantity was updated");
            }

        }
    }
}