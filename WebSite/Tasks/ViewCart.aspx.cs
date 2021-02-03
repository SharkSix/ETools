using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eTools.Application.BLL;
using eTools.Data.Entities.Pocos;

public partial class Tasks_ViewCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = User.Identity.GetUserName();

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(username))
            {

                ShoppingCartItemData();


            }
            else
            {

            }
        }
        

    }

    public void ShoppingCartItemData()
    {
        string username = User.Identity.GetUserName();
        SalesController controller = new SalesController();
        List<ShoppingCartItemList> itemList = new List<ShoppingCartItemList>();

        itemList = controller.GetShoppingCartItems(username);

        CartItemReapter.DataSource = itemList;
        CartItemReapter.DataBind();

        int shoppingCardID = controller.GetShoppingCartID(username);
        decimal subTotal = controller.GetSubTotal(shoppingCardID);

        SubTotal.Text = subTotal.ToString();

    }

    protected void CartItemReapter_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ShoppingCartItemId = Convert.ToInt32(e.CommandArgument.ToString());
        SalesController saleManager = new SalesController();
        TextBox quantity = e.Item.FindControl("QuanityChange") as TextBox;
        int counts = Convert.ToInt32(quantity.Text);
        if (e.CommandName=="Remove")
        {
            MessageUserControl.TryRun(() =>
            {
                saleManager.DeleteShoppingCartItem(ShoppingCartItemId);
                ShoppingCartItemData();
            }, "Success", "Shopping cart item was deleted.");
        }
        else if(e.CommandName == "Update")
        {
            MessageUserControl.TryRun(() =>
            {
                saleManager.UpdateShoppingCartItem(ShoppingCartItemId, counts);
                ShoppingCartItemData();
            }, "Success", "Shopping cart item was Updated.");
        }
    }

}