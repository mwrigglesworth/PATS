using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class FinancialEvaluator_DataDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GIPAP_Objects.User sessUse = (GIPAP_Objects.User)Session["sessUse"];
        GIPAP_Objects.FCOffice myOffice = new GIPAP_Objects.FCOffice();
        string dset = Request.QueryString["dset"].ToString();
        DataSet ds = new DataSet();
        if (dset == "unresolvedrequests")
        {
            ds = myOffice.listRequests(sessUse.UserID, sessUse.Role, false);
            LabelTitle.Text = "Unresolved Patient Requests";
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "resolvedrequests")
        {
            ds = myOffice.listRequests(sessUse.UserID, sessUse.Role, true);
            LabelTitle.Text = "Resolved Patient Requests";
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "sentrequests")
        {
            ds = myOffice.listSentRequests(sessUse.Username);
            LabelTitle.Text = "Patient Requests I Sent";
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "assignedpending")
        {
            LabelTitle.Text = "Pending Patients Recently Assigned to Your Branch";
            ds = myOffice.getFCDatasets(sessUse.UserID, dset);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "yearlyreassess")
        {
            LabelTitle.Text = "Patients Needing a New FEF Assessment";
            ds = myOffice.getFCDatasets(sessUse.UserID, dset);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "reactivationrequests")
        {
            LabelTitle.Text = "Reactivation Requests";
            ds = myOffice.getFCDatasets(sessUse.UserID, dset);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "changetreatmentrequests")
        {
            LabelTitle.Text = "Treatment Change Requests";
            ds = myOffice.getFCDatasets(sessUse.UserID, dset);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "reassessmentrequests")
        {
            LabelTitle.Text = "Reassessment Requests";
            ds = myOffice.getFCDatasets(sessUse.UserID, dset);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "assignedpendingallbranches")
        {
            LabelTitle.Text = "Pending Patients Recently Assigned to a Branch";
            ds = myOffice.getFCDatasets(sessUse.UserID, dset);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "yearlyreassessallbranches")
        {
            LabelTitle.Text = "Patients Needing a New FEF Assessment";
            ds = myOffice.getFCDatasets(sessUse.UserID, dset);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "reactivationrequestsallbranches")
        {
            LabelTitle.Text = "Reactivation Requests";
            ds = myOffice.getFCDatasets(sessUse.UserID, dset);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "reassessmentrequestsallbranches")
        {
            LabelTitle.Text = "Reassessment Requests";
            ds = myOffice.getFCDatasets(sessUse.UserID, dset);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
        else if (dset == "listpatients")
        {
            string stat = Request.QueryString["status"].ToString();
            LabelTitle.Text = stat + " NOA Patients";
            ds = myOffice.listNOAPatients(stat, sessUse.UserID, sessUse.Role);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
    }
}