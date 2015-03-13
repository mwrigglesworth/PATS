using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_ViewFCRequest : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.FCRequest myRequest = new GIPAP_Objects.FCRequest();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["rchoice"]);
        myRequest = new GIPAP_Objects.FCRequest(0, 0, choice);
        LabelRequest.Text = myRequest.ViewRequest(sessUse.Role);
        if (myRequest.Resolved)
        {
            PanelResolve.Visible = false;
        }
        else
        {
            if (myRequest.ToType == "Call Center" && sessUse.Role == "FCCallCenter")
            {
                PanelResolve.Visible = true;
            }
            else if (myRequest.ToType == "FE Branch" && sessUse.Role == "FCBranch")
            {
                PanelResolve.Visible = true;
            }
            else if (myRequest.ToType == "Central Hub" && sessUse.Role == "FCFreedomDesk")
            {
                PanelResolve.Visible = true;
            }
            else if (myRequest.ToType == "TMF" && (sessUse.Role == "MaxStation" || sessUse.Role == "TMFUser"))
            {
                PanelResolve.Visible = true;
            }
            else
            {
                PanelResolve.Visible = false;
            }
        }
    }
    protected void ButtonReply_Click(object sender, EventArgs e)
    {
        Response.Redirect("fcrequest.aspx?choice=" + myRequest.PatientID.ToString() + "&repid=" + myRequest.FCRequestID.ToString());
    }
    protected void ButtonResolve_Click(object sender, EventArgs e)
    {
        myRequest.Resolve(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=resolvefcrequest&choice=" + myRequest.PatientID.ToString());
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(sessUse.HomePage);
    }
}
