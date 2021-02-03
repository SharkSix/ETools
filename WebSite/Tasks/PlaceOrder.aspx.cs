using eTools.Application.BLL;
using eTools.Data.Entities.Pocos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tasks_PlaceOrder : System.Web.UI.Page
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

        OrderReapter.DataSource = itemList;
        OrderReapter.DataBind();
        int shoppingCardID = controller.GetShoppingCartID(username);
        decimal subTotal = controller.GetSubTotal(shoppingCardID);
        
        lbl_subTotal.Text = subTotal.ToString();
        lbl_total.Text = ((double)subTotal).ToString();
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        SalesController controller = new SalesController();
        if(!string.IsNullOrEmpty(lbl_discount.Text))
        {
            int discount = controller.GetCoupons(Coupon.Text);
            lbl_discount.Text = (discount * double.Parse(lbl_subTotal.Text)*0.01).ToString();

            lbl_total.Text = (double.Parse(lbl_subTotal.Text) * (1 - discount * 0.01)).ToString();

        }
    }
}