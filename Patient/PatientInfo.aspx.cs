using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Patient_GIPAPControlPanel_PatientInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string a = "";
        int choice = Convert.ToInt32(Request.QueryString["choice"]);
        GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            a = Request.QueryString["a"].ToString();
        }
        catch { }
        if (a != "")
        {
            PanelAlert.Visible = true;
            if (a == "reapprove")
            {
                LabelAlert.Text = "Patient has been reapproved";
            }
            else if (a == "reapproverequest")
            {
                LabelAlert.Text = "Reapproval Request has been recorded";
            }
            else if (a == "edit")
            {
                LabelAlert.Text = "Patient information has been updated";
            }
            else if (a == "add")
            {
                LabelAlert.Text = "Patient added to PATS";
            }
            else if (a == "tabletstrength")
            {
                LabelAlert.Text = "Tablet strength has been updated";
            }
            else if (a == "extend")
            {
                LabelAlert.Text = "Current period has been extended";
            }
            else if (a == "extendrequest")
            {
                LabelAlert.Text = "Extend Request has been recorded";
            }
            else if (a == "closerequest")
            {
                LabelAlert.Text = "Close Request has been recorded";
            }
            else if (a == "close")
            {
                LabelAlert.Text = "Patient Case has been closed";
            }
            else if (a == "denyrequest")
            {
                LabelAlert.Text = "Deny Request has been recorded";
            }
            else if (a == "deny")
            {
                LabelAlert.Text = "Patient Case has been denied";
            }
            else if (a == "dosechangerequest")
            {
                LabelAlert.Text = "Dose Change Request has been recorded";
            }
            else if (a == "dosechange")
            {
                LabelAlert.Text = "Patient Dosage has been updated";
            }
            else if (a == "reassessrequest")
            {
                LabelAlert.Text = "Reassess Request has been recorded";
            }
            else if (a == "approve")
            {
                LabelAlert.Text = "Patient Case has been approved";
            }
            else if (a == "reactivaterequest")
            {
                LabelAlert.Text = "Reactivate Request has been recorded";
            }
            else if (a == "reactivate")
            {
                LabelAlert.Text = "Patient Case has been reactivated";
            }
            else if (a == "sae")
            {
                LabelAlert.Text = "SAE has been logged";
            }
            else if (a == "verification")
            {
                LabelAlert.Text = "Financial Verification Information updated";
            }
            else if (a == "tcrequest")
            {
                LabelAlert.Text = "Treatment Change Request has been recorded";
            }
            else if (a == "approvedtasigna")
            {
                LabelAlert.Text = "Patient has been approved for Tasigna";
            }
            else if (a == "removerequest")
            {
                LabelAlert.Text = "Request has been removed";
            }
            else if (a == "nosae")
            {
                LabelAlert.Text = "Adverse Event was not logged";
            }
            else if (a == "fcrequest")
            {
                LabelAlert.Text = "Your request has been logged";
            }
            else if (a == "resolvefcrequest")
            {
                LabelAlert.Text = "Request has been resolved";
            }
            else if (a == "updatestatusreason")
            {
                LabelAlert.Text = "Status Reason Updated";
            }
            else if (a == "phystranreq")
            {
                LabelAlert.Text = "Physician Transfer Request has been recorded";
            }
            else if (a == "unexpire")
            {
                LabelAlert.Text = "Order Un-expired";
            }
        }

        GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
        LabelList.Text = myPatient.PatientDataLinks(choice, sessUse.Role);
        LabelStatOptions.Text = myPatient.StatusOptions(choice, sessUse.Role);
        if (sessUse.Role != "TMFUser" && sessUse.Role != "MaxStation")
        {
            PartialUpdatePanelCaseNotes.UserControlPath = "Patient/GIPAPAsync/MainColPatientDiag.ascx";
        }
    }
}
