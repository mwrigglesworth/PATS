using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_UpdateStatusReason : System.Web.UI.Page
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
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "updatestatusreason");

        if (!Page.IsPostBack)
        {
            if (myStatus.GipapStatus == "Active")
            {
                dropUpdateStatusReason.Items.Add("Approved by exception");
                dropUpdateStatusReason.Items.Add("Approved with Partial Coverage");
                dropUpdateStatusReason.Items.Add("Fulfills all criteria");
            }
            else if (myStatus.GipapStatus == "Denied")
            {
                dropUpdateStatusReason.Items.Add("Other Diagnosis");
                dropUpdateStatusReason.Items.Add("C-Kit Negative");
                dropUpdateStatusReason.Items.Add("GIST Tumor is Neither Metastatic or Unresectable");
                dropUpdateStatusReason.Items.Add("Philadelphia Chromosome Negative");
                dropUpdateStatusReason.Items.Add("Glivec not prescribed");
                dropUpdateStatusReason.Items.Add("Patient Had Successful Surgery/BMT");
                dropUpdateStatusReason.Items.Add("Patient has passed away");
                dropUpdateStatusReason.Items.Add("Patient has insurance");
                dropUpdateStatusReason.Items.Add("Insurance covers Glivec");
                dropUpdateStatusReason.Items.Add("Does not meet financial criteria for GIPAP");
                dropUpdateStatusReason.Items.Add("Patient Has Access to Reimbursement");
                dropUpdateStatusReason.Items.Add("Does Not Meet Country Financial Requirements");
                dropUpdateStatusReason.Items.Add("Glivec not approved in country");
                dropUpdateStatusReason.Items.Add("GIPAP not approved in country");
                dropUpdateStatusReason.Items.Add("Interferon treatment required");
                dropUpdateStatusReason.Items.Add("Duplicate Application");
                dropUpdateStatusReason.Items.Add("Underage");
                dropUpdateStatusReason.Items.Add("Referred to EAP");
                dropUpdateStatusReason.Items.Add("Does Not Meet Country Citizenship Requirements");
                dropUpdateStatusReason.Items.Add("Patient’s Physician not Approved for GIPAP");
                dropUpdateStatusReason.Items.Add("Patient or Doctor Retracts Application");
                dropUpdateStatusReason.Items.Add("Unspecified / Other");
                dropUpdateStatusReason.Items.Add("Lost contact with patient");
                dropUpdateStatusReason.Items.Add("Required documents not submitted");
                dropUpdateStatusReason.Items.Add("Referred to Clinical Trial");
            }

            else if (myStatus.GipapStatus == "Pending")
            {
                dropUpdateStatusReason.Items.Add("Waiting for PO Review");
                dropUpdateStatusReason.Items.Add("Waiting for Max Station Response");
                dropUpdateStatusReason.Items.Add("Waiting for GCC feedback");
                dropUpdateStatusReason.Items.Add("Waiting for Regulatory Approval");
                dropUpdateStatusReason.Items.Add("Waiting For GIPAP Approval From Novartis");
                dropUpdateStatusReason.Items.Add("Waiting For Medical Information");
                dropUpdateStatusReason.Items.Add("Waiting For Physician Information");
                dropUpdateStatusReason.Items.Add("Waiting For Reimbursement");
                dropUpdateStatusReason.Items.Add("Need to confirm Ph +");
                dropUpdateStatusReason.Items.Add("Need More Information");
                dropUpdateStatusReason.Items.Add("Patient Is Applying After Being Denied");
                dropUpdateStatusReason.Items.Add("Interferon treatment required");
                dropUpdateStatusReason.Items.Add("Unspecified / Other");
            }

            else if (myStatus.GipapStatus == "Closed")
            {
                dropUpdateStatusReason.Items.Add("Patient has passed away");
                dropUpdateStatusReason.Items.Add("Patient Not Responding to Treatment");
                dropUpdateStatusReason.Items.Add("Intolerance");
                dropUpdateStatusReason.Items.Add("Patient does not meet GIPAP criteria");
                dropUpdateStatusReason.Items.Add("Lost contact with patient");
                dropUpdateStatusReason.Items.Add("No re-evaluation information provided");
                dropUpdateStatusReason.Items.Add("Duplicate Patient");
                dropUpdateStatusReason.Items.Add("Pregnancy");
                dropUpdateStatusReason.Items.Add("Unspecified / Other");
            }
        }

    }
    protected void ButtonUpdateStatusReason_Click(object sender, EventArgs e)
    {
        myStatus.StatusReason = dropUpdateStatusReason.SelectedValue;
        myStatus.UpdateStatusReason(sessUse.Username);
        Response.Redirect("Patientinfo.aspx?a=updatestatusreason&choice=" + myStatus.PatientID.ToString());
    }
    protected void ButtonCancelUpdateStatusReason_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
}
