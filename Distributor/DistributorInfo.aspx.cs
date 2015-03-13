using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Distributor_DistributorInfo : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Distributor myDistributor = new GIPAP_Objects.Distributor();
    public int choice;

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
                LabelAlert.Text = "Distributor information has been updated";
            }
            else if (a == "add")
            {
                LabelAlert.Text = "Distributor has been added to PATS";
            }
        }
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myDistributor = new GIPAP_Objects.Distributor(choice);
        LabelApprove.Text = myDistributor.ApprovedInfo(sessUse.Role);
        LabelAdmin.Text = myDistributor.AdminInfo(sessUse.Role);
        if (myDistributor.Approved == 1)
        {
            lbApprove.Text = "[Unapprove]";
        }
        else
        {
            lbApprove.Text = "[Approve]";
        }


        LabelList.Text = "<a href=AddEditDistributor.aspx?action=Edit&choice=" + choice.ToString() + ">Edit Distributor Info</a><br><br>";
    }
    protected void lbApprove_Click(object sender, EventArgs e)
    {
        if (myDistributor.Approved == 1)
        {
            myDistributor.UnApprove(sessUse.Username);
            lbApprove.Text = "[Approve]";
        }
        else
        {
            myDistributor.Approve(sessUse.Username);
            lbApprove.Text = "[Unapprove]";
        }
        myDistributor = new GIPAP_Objects.Distributor(choice);
        LabelApprove.Text = myDistributor.ApprovedInfo(sessUse.Role);
    }
}