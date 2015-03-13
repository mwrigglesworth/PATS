using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stockist_StockistInfo : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Stockist myStockist = new GIPAP_Objects.Stockist();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        string a = "";
        try
        {
            a = Request.QueryString["a"].ToString();
        }
        catch { }
        if (a != "")
        {
            PanelAlert.Visible = true;
            if (a == "edit")
            {
                LabelAlert.Text = "Stockist information has been updated";
            }
            else if (a == "add")
            {
                LabelAlert.Text = "Stockist has been added to PATS";
            }
        }
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myStockist = new GIPAP_Objects.Stockist(choice);
        LabelApprove.Text = myStockist.ApprovedInfo(sessUse.Role);
        LabelAdmin.Text = myStockist.AdminInfo(sessUse.Role);
        if (myStockist.Approved == 1)
        {
            lbApprove.Text = "[Unapprove]";
        }
        else
        {
            lbApprove.Text = "[Approve]";
        }


        LabelList.Text = "<a href=EditStockist.aspx?choice=" + choice.ToString() + ">Edit Stockist Info</a><br><br>";
    }
    protected void lbApprove_Click(object sender, EventArgs e)
    {
        if (myStockist.Approved == 1)
        {
            myStockist.UnApprove(sessUse.Username);
            lbApprove.Text = "[Approve]";
        }
        else
        {
            myStockist.Approve(sessUse.Username);
            lbApprove.Text = "[Unapprove]";
        }
        myStockist = new GIPAP_Objects.Stockist(choice);
        LabelApprove.Text = myStockist.ApprovedInfo(sessUse.Role);
    }
}
