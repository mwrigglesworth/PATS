using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class TMF_DataDisplay : System.Web.UI.Page
{
    DataTable ds = new DataTable();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        string dset = Request.QueryString["dset"];
        try
        {
            choice = Convert.ToInt32(Request.QueryString["choice"]);
        }
        catch
        {
            choice = 0;
        }

        if (dset == "patientwebapplicants")
        {
            ds = sessUse.getTMFDatasets(dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Web Applicants";
        }
        else if (dset == "reapprovalrequests")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Reapproval Requests";
        }
        else if (dset == "requests")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "PO Requests";
        }
        else if (dset == "noareactivationrequests")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "NOA Reactivation Requests";
        }
        else if (dset == "noareassessmentrequests")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "NOA Reassessment Requests";
        }
        else if (dset == "pendingpatients")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Pending Patients";
        }
        else if (dset == "noapatientsneedingaction")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "NOA Patients Needing Action";
        }
        else if (dset == "pastduepatients")
        {
            ds = sessUse.getTMFDatasets(dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Past Due Patients";
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                if (Convert.ToDateTime(ds.Rows[i]["End Date"]) > DateTime.Today.AddDays(-29) && Convert.ToDateTime(ds.Rows[i]["End Date"]) < DateTime.Today.AddDays(-21))
                {
                    dgResults.Items[i].BackColor = System.Drawing.Color.LightSalmon;
                }
            }
        }
        else if (dset == "pendingnoa")
        {
            ds = sessUse.getTMFDatasets(dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Pending NOA Patients";
        }
        else if (dset == "physicianwebapplicants")
        {
            ds = sessUse.getTMFDatasets(dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Physician Web Applicants";
        }
        else if (dset == "clinicwebapplicants")
        {
            ds = sessUse.getTMFDatasets(dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Clinic Web Applicants";
        }
        else if (dset == "pendingphysicians")
        {
            ds = sessUse.getTMFDatasets(dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Pending Physicians";
        }
        else if (dset == "pendingclinics")
        {
            ds = sessUse.getTMFDatasets(dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Pending Clinics";
        }
        else if (dset == "maxstationgroups")
        {
            ds = sessUse.getTMFDatasets(dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Max Station Groups";
        }
        else if (dset == "deletedapplicants")
        {
            ds = sessUse.GetDeletedApplications().Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Deleted Applications";
        }
        else if (dset == "unresolvedNOARequests")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "NOA Patient Action Requests";
        }
        else if (dset == "YearlyReassessment")
        {
            DataSet yr = sessUse.getDatasets(choice, dset);
            int red = yr.Tables[0].Rows.Count;
            ds = yr.Tables[0];
            ds.Merge(yr.Tables[1]);
            this.FillDataGrid(ds);
            for (int i = 0; i < red; i++)
            {
                dgResults.Items[i].BackColor = System.Drawing.Color.LightSalmon;
            }
            LabelTitle.Text = "NOA Reassessment Required";
        }
        else if (dset == "resolvedNOARequests")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Resolved NOA Patient Action Requests";
        }
        else if (dset == "sentNOARequests")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "Requests I Sent";
        }
        else if (dset == "NOAChangeTreatmentRequests")
        {
            ds = sessUse.getDatasets(choice, dset).Tables[0];
            this.FillDataGrid(ds);
            LabelTitle.Text = "NOA Treatment Change Requests";
        }
    }
    //**********************************************************************************************************************
    private void FillDataGrid(DataTable dt)
    {
        dgResults.DataSource = dt;
        dgResults.DataBind();
        dgResults.HeaderStyle.Font.Bold = true;
        LabelResultCount.Text = dt.Rows.Count.ToString() + " Results Found";
    }
}
