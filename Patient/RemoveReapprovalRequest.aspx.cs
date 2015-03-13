using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_RemoveReapprovalRequest : System.Web.UI.Page
{
    GIPAP_Objects.PatientGipapStatus myStatus;
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessUse = (GIPAP_Objects.User)Session["sessUse"];
        }
        catch
        {
            Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
        }
        if (sessUse.Role != "TMFUser" && sessUse.Role != "MaxStation" && sessUse.Role != "Physician")
        {
            Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
        }
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "reapprove");
    }
    protected void ButtonYes_Click(object sender, EventArgs e)
    {
        myStatus.IgnoreReapprovalRequest(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=removerequest&choice=" + myStatus.PatientID.ToString());
    }
    protected void ButtonNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientInfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
}
