using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Physician_DataDisplay : System.Web.UI.Page
{
    GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();
    DataSet ds = new DataSet();
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
        DataTable dt = myPhysician.GetPhysicianDataSetsByUser(sessUse.UserID, dset).Tables[0];
        this.FillDataGrid(dt);
        if (dset == "patientsneedingreapproval")
        {
            LabelTitle.Text = "Patients Needing Reapproval";
        }
        else if (dset == "closepatients")
        {
            LabelTitle.Text = "Request To Close a Patient Case";
        }
        else if (dset == "saepatients")
        {
            LabelTitle.Text = "Report an Adverse Event";
        }
        else if (dset == "dosechangepatients")
        {
            LabelTitle.Text = "Change The Dosage for a Patient";
        }
        else if (dset == "reactivatepatients")
        {
            LabelTitle.Text = "Reactivate A Closed Patient Case";
        }
        else if (dset == "supplyupdatepatients")
        {
            LabelTitle.Text = "Update Supply for an Active Patient";
        }
        else if (dset == "allpatients")
        {
            LabelTitle.Text = "My GIPAP Patients";
        }
        else if (dset == "clinics")
        {
            LabelTitle.Text = "My GIPAP Clinics";
        }
        else if (dset == "extendpatients")
        {
            LabelTitle.Text = "Request an Extention for an Active Patient";
        }
        else if (dset == "changetreatment")
        {
            LabelTitle.Text = "Request to Change Treatment to Tasigna for an Active Patient";
        }
        else if (dset == "saelog")
        {
            LabelTitle.Text = "Adverse Event Log";
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