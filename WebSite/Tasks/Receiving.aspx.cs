using eTools.Application.BLL;
using eTools.Data.Entities;
using eTools.Data.Entities.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tasks_Staff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.IsAuthenticated)
        {
            if (User.IsInRole("Employees"))
            {
                OutstandingOrders.Visible = true;
            }
            else
            {
                MessageUserControl.ShowInfo("You must be logged into an employee account to access this page.");
                OutstandingOrders.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void ShowPurchaseOrders_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        OrderInfo.Visible = true;
        OrderControls.Visible = true;
        int index = e.NewSelectedIndex;
        GridViewRow agrow = ShowPurchaseOrders.Rows[index];
        int orderNumber = int.Parse(agrow.Cells[0].Text);
        PONum.Text = agrow.Cells[0].Text;
        Vendor.Text = agrow.Cells[2].Text;
        Phone.Text = agrow.Cells[3].Text;
        MessageUserControl.TryRun(() =>
        {
            ReceivingController sysmgr = new ReceivingController();
            List<ViewPurchaseOrderItems> orderInfo = sysmgr.GetPurchaseOrderDetails(orderNumber);
            ShowOrderDetails.DataSource = orderInfo;
            ShowOrderDetails.DataBind();
        });
    }

    protected void ReceiveOrder_Click(object sender, EventArgs e)
    {
        if (PONum.Text != "")
        {
            bool orderCompleted = true;
            int orderNumber = int.Parse(PONum.Text);
            ViewPurchaseOrderItems orderItem = null;
            List<ViewPurchaseOrderItems> orderDetails = new List<ViewPurchaseOrderItems>();
            MessageUserControl.TryRun(() =>
            {
                int tempInt = 0;
                int count = 0;
                foreach (GridViewRow arow in ShowOrderDetails.Rows)
                {
                    orderItem = new ViewPurchaseOrderItems();
                    orderItem.ItemID = int.Parse((arow.FindControl("StockNum") as Label).Text);
                    orderItem.ItemDescription = (arow.FindControl("Description") as Label).Text;
                    orderItem.Ordered = int.Parse((arow.FindControl("QtyOrdered") as Label).Text);

                    if (string.IsNullOrEmpty((arow.FindControl("QtyReturned") as TextBox).Text))
                    {
                        orderItem.Returned = 0;
                    }
                    else
                    {
                        if (int.TryParse((arow.FindControl("QtyReturned") as TextBox).Text, out tempInt))
                        {
                            if (tempInt >= 0)
                            { 
                                orderItem.Returned = tempInt;
                                count += tempInt;
                            }
                            else
                            {
                                throw new Exception("You must enter a postive integer value for returned items.");
                            }
                        }
                        else
                        {
                            throw new Exception("You must enter an integer value for returned items.");
                        }
                    }

                    if (string.IsNullOrEmpty((arow.FindControl("QtyReceived") as TextBox).Text))
                    {
                        orderItem.Received = 0;
                    }
                    else
                    {
                        if (int.TryParse((arow.FindControl("QtyReceived") as TextBox).Text, out tempInt))
                        {
                            if (tempInt >= 0)
                            {
                                orderItem.Received = tempInt;
                                count += tempInt;
                            }
                            else
                            {
                                throw new Exception("You must enter a postive integer value for received items.");
                            }
                        }
                        else
                        {
                            throw new Exception("You must enter an integer value for received items.");
                        }
                    }

                    if ((arow.FindControl("ReasonReturned") as TextBox).Text == "" && orderItem.Returned > 0)
                    {
                        throw new Exception("You must enter a reason for returned items.");
                    }
                    else
                    {
                        if (orderItem.Returned > 0)
                        {
                            orderItem.Reason = (arow.FindControl("ReasonReturned") as TextBox).Text;
                        }
                    }

                    if (orderItem.Received < int.Parse((arow.FindControl("QtyOutstanding") as Label).Text))
                    {
                        orderCompleted = false;
                    }

                    orderDetails.Add(orderItem);
                }
                if (count == 0)
                {
                    throw new Exception("You cannot receive an empty order.");
                }
                ReceivingController sysmgr = new ReceivingController();
                sysmgr.ReceivePurchaseOrder(orderNumber, orderDetails, orderCompleted);
                PONum.Text = "";
                Vendor.Text = "";
                Phone.Text = "";
                ShowOrderDetails.DataSource = null;
                ShowOrderDetails.DataBind();
                ShowPurchaseOrders.DataBind();
                OrderInfo.Visible = false;
                OrderControls.Visible = false;
            }, "Purchase Order Receiving", "Purchase Order successfully received.");
        }
        else
        {
            MessageUserControl.ShowInfo("You must select a purchase order before receiving.");
        }
    }

    protected void ForceClose_Click(object sender, EventArgs e)
    {
        if (PONum.Text != "")
        {
            int orderNumber = int.Parse(PONum.Text);
            string reason = ReasonForceClosed.Text;
            MessageUserControl.TryRun(() =>
            {
                if (reason == "")
                {
                    throw new Exception("You must enter a reason for force closing.");
                }
                else
                {
                    ReceivingController sysmgr = new ReceivingController();
                    sysmgr.ForceClosePurchaseOrder(orderNumber, reason);
                    PONum.Text = "";
                    Vendor.Text = "";
                    Phone.Text = "";
                    ReasonForceClosed.Text = "";
                    ShowOrderDetails.DataSource = null;
                    ShowOrderDetails.DataBind();
                    ShowPurchaseOrders.DataBind();
                    OrderInfo.Visible = false;
                    OrderControls.Visible = false;
                }
            }, "Purchase Order Receiving", "Purchase Order successfully closed.");
        }
        else
        {
            MessageUserControl.ShowInfo("You must select a purchase order before closing.");
        }
    }
}