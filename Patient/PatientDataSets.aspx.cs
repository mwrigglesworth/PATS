using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_PatientDataSets : System.Web.UI.Page
{
    string ds;
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.PatientDataSet myPDS;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        ds = Request.QueryString["ds"];
        myPDS = new GIPAP_Objects.PatientDataSet(Convert.ToInt32(Request.QueryString["choice"]), ds);
        LabelPatientLinks.Text = myPDS.PatientDataLinks(sessUse.Role);
        if (ds == "emails")
        {
            LabelReportTitle.Text = "Email History";
            dgResults.DataSource = myPDS.dsEmails;
            dgResults.DataBind();
            if (sessUse.Role == "TMFUser")
            {
                LabelStringReport.Text = "<font class='lbl'>Create GIPAP Emails:</font><br>" + myPDS.PatientEmailLinks();
            }
        }
        else if (ds == "sae")
        {
            LabelReportTitle.Text = "Adverse Event History";
            dgResults.DataSource = myPDS.dsSae;
            dgResults.DataBind();
            hlAE.Visible = true;
            hlAE.NavigateUrl = "SAE.aspx?choice=" + myPDS.PatientID.ToString();
        }
        else if (ds == "statushistory")
        {
            LabelReportTitle.Text = "Status History";
            LabelStringReport.Text = myPDS.StatusHistory();
        }
        else if (ds == "requesthistory")
        {
            LabelReportTitle.Text = "Request History";
            LabelStringReport.Text = myPDS.RequestHistory();
        }
        //i dont think we use these any more
        /*else if (ds == "requests")
        {
            LabelReportTitle.Text = "Outstanding Requests";
            dgResults.DataSource = myPDS.dsOtherRequests;
            dgResults.DataBind();
        }
        else if (ds == "reapprovalrequests")
        {
            LabelReportTitle.Text = "Outstanding Reapproval Requests";
            dgResults.DataSource = myPDS.dsReapprovalRequests;
            dgResults.DataBind();
        }
        else if (ds == "fefupdates")
        {
            LabelReportTitle.Text = "FEF Updates";
            dgResults.DataSource = myPDS.dsFEFUpdates;
            dgResults.DataBind();
        }*/
        else if (ds == "fefhistory")
        {
            LabelReportTitle.Text = "FEF History";
            dgResults.DataSource = myPDS.dsFEFHistory;
            dgResults.DataBind();
        }
        else if (ds == "physicianhistory")
        {
            LabelReportTitle.Text = "Physician History";
            dgResults.DataSource = myPDS.PhysicianHistory;
            dgResults.DataBind();
            //LabelStringReport.Text = myPDS.PhysicianTransferRequestHistory();
        }
    }
}
