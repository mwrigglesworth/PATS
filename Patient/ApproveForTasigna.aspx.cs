using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_ApproveForTasigna : System.Web.UI.Page
{
    GIPAP_Objects.Request myRequest;
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Patient myPatient= new GIPAP_Objects.Patient();
    GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus();

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
        if (sessUse.Role != "TMFUser")
        {
            Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
        }
        myRequest = new GIPAP_Objects.Request(Convert.ToInt32(Request.QueryString["rid"]), Convert.ToInt32(Request.QueryString["choice"]));

        /*set up the values from the request for the status item*/
        if (!Page.IsPostBack)
        {
            labeltasignaInfo.Text = myPatient.DiagnosisInfoTable(Convert.ToInt32(Request.QueryString["choice"]), Convert.ToInt32(Request.QueryString["rid"]));
            dropRequestedTasignaDose.SelectedValue = myRequest.RequestedDosage;
            LabelTasignaStartDate.Text = DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y");
            LabelTasignaEndDate.Text = DateTime.Today.AddDays(119).Day.ToString() + " " + DateTime.Today.AddDays(119).ToString("y");
            LabelNotes.Text = myRequest.Notes;
        }
    }


    protected void ButtonChangeTreatment_Click(object sender, EventArgs e)
    {
        myStatus.PatientID = myRequest.PatientID;
        myStatus.RequestID = myRequest.RequestID;
        myStatus.FlagNoa = myRequest.FlagNoa;
        /*if (dropRequestedTasignaDose.SelectedValue != myRequest.RequestedDosage)
        {
            myStatus.CurrentDosage = dropRequestedTasignaDose.SelectedValue;
            myStatus.ChangeDosage(sessUse.Username);
        }*/
        myStatus.CurrentDosage = dropRequestedTasignaDose.SelectedValue;
        myStatus.StatusReason = "Changed Treatment to Tasigna";
        myStatus.Notes = "automatically logged during treatment change";
        myStatus.StartDate = Convert.ToDateTime(LabelTasignaStartDate.Text);
        myStatus.EndDate = Convert.ToDateTime(LabelTasignaEndDate.Text);
        myStatus.RestartTreatment = true;
        myStatus.FinancialStatus = true;
        myStatus.CurrentCMLPhase = myRequest.CurrentCMLPhase;
        if (myRequest.GipapStatus == "Active")
        {
            if (myRequest.GlivecIntolerant)
            {
                myStatus.StatusReason = "Intolerance";
            }
            else if (myRequest.GlivecResistant)
            {
                myStatus.StatusReason = "Patient Not Responding to Treatment";
            }
            myStatus.Close(sessUse.Username);
            myStatus.Notes = myRequest.Notes;
            myStatus.StatusReason = "Changed Treatment to Tasigna";
            myStatus.ReActivate(sessUse.Username, myStatus.StatusReason);
        }
        else if (myRequest.GipapStatus == "Closed")
        {
            myStatus.Notes = myRequest.Notes;
            myStatus.ReActivate(sessUse.Username, myStatus.StatusReason);
        }
        else if (myRequest.GipapStatus == "Denied")
        {
            myStatus.ReAssess(sessUse.Username, myStatus.Notes);
            myStatus.Notes = myRequest.Notes;
            myStatus.Approve(sessUse.Username);
        }
        Response.Redirect("PatientEmail.aspx?a=approvedtasigna&mailType=ApprovalEmailPatient&choice=" + myStatus.PatientID.ToString());
    }


    protected void ButtonRemoveRequest_Click(object sender, EventArgs e)
    {
        myRequest.Resolve(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=removerequest&choice=" + myRequest.PatientID.ToString());
    }
}