using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Deny : System.Web.UI.Page
{
    GIPAP_Objects.PatientGipapStatus myStatus;
    GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request();
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
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "deny");

        if (!Page.IsPostBack)
        {
            //SET UP BUTTON DISABLER....
            System.Text.StringBuilder sbDisable = new System.Text.StringBuilder();
            sbDisable.Append("if (typeof(Page_ClientValidate) == 'function') {");
            sbDisable.Append("if (Page_ClientValidate() == false) {");
            sbDisable.Append("return false;");
            sbDisable.Append("}");
            sbDisable.Append("}");
            sbDisable.Append("this.value = 'Please wait...';");
            sbDisable.Append("this.disabled = true;");

            //FINISH BUTTON DISABLER...
            sbDisable.Append(Page.GetPostBackEventReference(ButtonDeny));
            sbDisable.Append(";");
            //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

            ButtonDeny.Attributes.Add("onclick", sbDisable.ToString());
        }
    }
    protected void ButtonDeny_Click(object sender, EventArgs e)
    {
        if (sessUse.Role == "TMFUser")
        {
            myStatus.StatusReason = dropDenyStatusReason.SelectedValue;
            myStatus.Notes = txtDenyNotes.Text;
            myStatus.Deny(sessUse.Username);
            Response.Redirect("PatientEmail.aspx?a=deny&mailType=DenialEmailPatient&choice=" + myStatus.PatientID.ToString());
        }
        else
        {
            myRequest.PatientID = myStatus.PatientID;
            myRequest.StatusReason = dropDenyStatusReason.SelectedValue;
            myRequest.Notes = txtDenyNotes.Text;
            myRequest.Deny(sessUse.Username);
            Response.Redirect("Patientinfo.aspx?a=denyrequest&choice=" + myRequest.PatientID.ToString());
        }
    }
    protected void ButtonDenyNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
}
