using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class MaxStation_DataDisplay : System.Web.UI.Page
{
    GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    string dset;

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
        dset = Request.QueryString["dset"].ToString();
        if (dset == "patientsneedingreapproval")
        {
            this.FillDataGrid(myPerson.getMaxStationDatasets(sessUse, dset).Tables[0]);
            LabelTitle.Text = "Patients Needing Reapproval";
        }
        else if (dset == "allpatients")
        {
            this.FillDataGrid(myPerson.getMaxStationDatasets(sessUse, dset).Tables[0]);
            LabelTitle.Text = "My GIPAP Patients";
        }
        else if (dset == "clinics")
        {
            this.FillDataGrid(myPerson.getMaxStationDatasets(sessUse, dset).Tables[0]);
            LabelTitle.Text = "My GIPAP Clinics";
        }
        else if (dset == "physicians")
        {
            this.FillDataGrid(myPerson.getMaxStationDatasets(sessUse, dset).Tables[0]);
            LabelTitle.Text = "My GIPAP Physicians";
        }
        else if (dset == "pendingpatients")
        {
            this.FillDataGrid(myPerson.getMaxStationDatasets(sessUse, dset).Tables[0]);
            LabelTitle.Text = "My Pending Patients";
        }
        else if (dset == "pastduepatients")
        {
            DataTable dt = myPerson.getMaxStationDatasets(sessUse, dset).Tables[0];
            this.FillDataGrid(dt);
            LabelTitle.Text = "My Past Due Patients";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dt.Rows[i]["Period End Date"]) > DateTime.Today.AddDays(-29) && Convert.ToDateTime(dt.Rows[i]["Period End Date"]) < DateTime.Today.AddDays(-21))
                {
                    dgResults.Items[i].BackColor = System.Drawing.Color.LightSalmon;
                }
            }
        }
        else if (dset == "pendingrequests")
        {
            this.FillDataGrid(myPerson.getMaxStationDatasets(sessUse, dset).Tables[0]);
            LabelTitle.Text = "Pending Requests For My Patients";
        }
        else if (dset == "pendingreapprovals")
        {
            this.FillDataGrid(myPerson.getMaxStationDatasets(sessUse, dset).Tables[0]);
            LabelTitle.Text = "Pending Reapprovals For My Patients";
        }
        else if (dset == "activepatients")
        {
            this.FillDataGrid(myPerson.getMaxStationDatasets(sessUse, dset).Tables[0]);
            LabelTitle.Text = "Active Patients";
        }
        else if (dset == "patientconsent")
        {
            this.FillDataGrid(myPerson.getMaxStationDatasets(sessUse, dset).Tables[0]);
            LabelTitle.Text = "Patients who have not signed consent form";
        }
        else if (dset == "unresolvedNOARequests")
        {
            this.FillDataGrid(myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            LabelTitle.Text = "Unresolved NOA Patient Action Requests";
        }
        else if (dset == "resolvedNOARequests")
        {
            this.FillDataGrid(myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            LabelTitle.Text = "Resolved NOA Patient Action Requests";
        }
        else if (dset == "sentNOARequests")
        {
            this.FillDataGrid(myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            LabelTitle.Text = "NOA Patient Action Requests I Sent";
        }
        else if (dset == "unassignedNOApending")
        {
            this.FillDataGrid(myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            LabelTitle.Text = "Pending NOA Patients Needing Branch Assignment";
        }
        else if (dset == "NOAreactivationrequestsNoBranch")
        {
            this.FillDataGrid(myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            LabelTitle.Text = "NOA Reactivation Requests Needing Branch Assignment";
        }
        else if (dset == "NOAchangetreatmentrequestsNoBranch")
        {
            this.FillDataGrid(myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            LabelTitle.Text = "NOA Treatment Change Requests Needing Branch Assignment";
        }
        else if (dset == "NOAreassessmentrequestsNoBranch")
        {
            this.FillDataGrid(myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            LabelTitle.Text = "NOA Reassessment Requests Needing Branch Assignment";
        }
        else if (dset == "YearlyReassessment")
        {
            LabelTitle.Text = "NOA Reassessment Required";
            if (sessUse.CountryID == 76)
            {
                DataSet ds = myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset);
                int red = ds.Tables[0].Rows.Count;
                DataTable dt = ds.Tables[0];
                dt.Merge(ds.Tables[1]);
                this.FillDataGrid(dt);
                for (int i = 0; i < red; i++)
                {
                    dgResults.Items[i].BackColor = System.Drawing.Color.LightSalmon;
                }
            }
            else
            {
                this.FillDataGrid(myPerson.getNOAMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            }
        }
        else if (dset == "NOAReactivationRequests")
        {
            LabelTitle.Text = "NOA Reactivation Requests";
            if (sessUse.CountryID == 76)
            {
                DataSet ds = myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset);
                int red = ds.Tables[0].Rows.Count;
                DataTable dt = ds.Tables[0];
                dt.Merge(ds.Tables[1]);
                this.FillDataGrid(dt);
                for (int i = 0; i < red; i++)
                {
                    dgResults.Items[i].BackColor = System.Drawing.Color.LightSalmon;
                }
            }
            else
            {
                this.FillDataGrid(myPerson.getNOAMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            }
        }
        else if (dset == "NOAChangeTreatmentRequests")
        {
            LabelTitle.Text = "NOA Treatment Change Requests";
            if (sessUse.CountryID == 76)
            {
                DataSet ds = myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset);
                int red = ds.Tables[0].Rows.Count;
                DataTable dt = ds.Tables[0];
                dt.Merge(ds.Tables[1]);
                this.FillDataGrid(dt);
                for (int i = 0; i < red; i++)
                {
                    dgResults.Items[i].BackColor = System.Drawing.Color.LightSalmon;
                }
            }
            else
            {
                this.FillDataGrid(myPerson.getNOAMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            }
        }
        else if (dset == "NOAReassessmentRequests")
        {
            LabelTitle.Text = "NOA Reassessment Requests";
            if (sessUse.CountryID == 76)
            {
                DataSet ds = myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset);
                int red = ds.Tables[0].Rows.Count;
                DataTable dt = ds.Tables[0];
                dt.Merge(ds.Tables[1]);
                this.FillDataGrid(dt);
                for (int i = 0; i < red; i++)
                {
                    dgResults.Items[i].BackColor = System.Drawing.Color.LightSalmon;
                }
            }
            else
            {
                this.FillDataGrid(myPerson.getNOAMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            }
        }
        else if (dset == "NOAPending")
        {
            LabelTitle.Text = "NOA Pending Patients";
            if (sessUse.CountryID == 76)
            {
                DataSet ds = myPerson.getNOAIndiaMaxStationDatasets(sessUse.UserID, dset);
                int red = ds.Tables[0].Rows.Count;
                DataTable dt = ds.Tables[0];
                dt.Merge(ds.Tables[1]);
                this.FillDataGrid(dt);
                for (int i = 0; i < red; i++)
                {
                    dgResults.Items[i].BackColor = System.Drawing.Color.LightSalmon;
                }
            }
            else
            {
                this.FillDataGrid(myPerson.getNOAMaxStationDatasets(sessUse.UserID, dset).Tables[0]);
            }
        }
    }
    //**********************************************************************************************************************
    private void FillDataGrid(DataTable dt)
    {
        dgResults.DataSource = dt;
        dgResults.DataBind();
        LabelResultCount.Text = dt.Rows.Count.ToString() + " Results Found";
    }
}
