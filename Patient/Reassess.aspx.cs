using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Reassess : System.Web.UI.Page
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
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "reassess");
        try
        {
            myRequest = new GIPAP_Objects.Request(Convert.ToInt32(Request.QueryString["rid"]), Convert.ToInt32(Request.QueryString["choice"]));
        }
        catch { }

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
            sbDisable.Append(Page.GetPostBackEventReference(ButtonReAssess));
            sbDisable.Append(";");
            //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

            ButtonReAssess.Attributes.Add("onclick", sbDisable.ToString());

            //see if we are processing a request
            if (myRequest.RequestID != 0)
            {
                txtCaseNote.Text = myRequest.Notes;
                ButtonRemoveRequest.Visible = true;
                if (myStatus.GipapStatus != "Denied")
                {
                    PanelObsolete.Visible = true;
                    ButtonReAssess.Enabled = false;
                }/*
                else if (myStatus.NoaFEFNeeded)
                {
                    ButtonReAssess.Enabled = false;
                }*/
            }
        }
    }

    protected void ButtonReAssess_Click(object sender, EventArgs e)
    {
        if (sessUse.Role == "TMFUser")
        {
            if (myRequest.RequestID != 0)
            {
                myStatus.RequestID = myRequest.RequestID;
            }
            myStatus.ReAssess(sessUse.Username, txtCaseNote.Text);
            Response.Redirect("Approve.aspx?choice=" + myStatus.PatientID.ToString());
        }
        else
        {
            myRequest.PatientID = myStatus.PatientID;
            myRequest.ReAssess(sessUse.Username, txtCaseNote.Text);
            Response.Redirect("Patientinfo.aspx?a=reassessrequest&choice=" + myRequest.PatientID.ToString());
        }
    }
    protected void ButtonCancelReAssess_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
    protected void ButtonRemoveRequest_Click(object sender, EventArgs e)
    {
        myRequest.Resolve(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=removerequest&choice=" + myStatus.PatientID.ToString());
    }
}
