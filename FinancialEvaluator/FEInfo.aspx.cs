using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FinancialEvaluator_FEInfo : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.FCOffice myOffice = new GIPAP_Objects.FCOffice();
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
                LabelAlert.Text = "NOA FE information has been updated";
            }
            else if (a == "add")
            {
                LabelAlert.Text = "NOA FE has been added to PATS";
            }
        }
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myOffice = new GIPAP_Objects.FCOffice(choice);
        LabelApprove.Text = myOffice.ApprovedInfo(sessUse.Role);
        LabelAdmin.Text = myOffice.AdminInfo(sessUse.Role);
        if (myOffice.Approved == 1)
        {
            lbApprove.Text = "[Unapprove]";
        }
        else
        {
            lbApprove.Text = "[Approve]";
        }
        if (sessUse.Role == "TMFUser")
        {
            lbApprove.Visible = true;
        }

        LabelList.Text = "<a href=EditFE.aspx?choice=" + choice.ToString() + ">Edit FE Info</a><br><br>";
    }
    protected void lbApprove_Click(object sender, EventArgs e)
    {
        if (myOffice.Approved == 1)
        {
            myOffice.UnApprove(sessUse.Username);
            lbApprove.Text = "[Approve]";
        }
        else
        {
            myOffice.Approve(sessUse.Username);
            lbApprove.Text = "[Unapprove]";
        }
        myOffice = new GIPAP_Objects.FCOffice(choice);
        LabelApprove.Text = myOffice.ApprovedInfo(sessUse.Role);
    }
}
