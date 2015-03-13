using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class TMF_MassTransfer : System.Web.UI.Page
{
    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
    GIPAP_Objects.User sessUse;
    string connString = ConfigurationSettings.AppSettings["ConnectionString"];
    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        DataSet ds = myPatient.getPatientSearchDropDowns(sessUse.UserID, sessUse.Role);

        if (!Page.IsPostBack)
        {
            this.FillDropBox(dropCountry, "CountryName", "CountryID", ds.Tables[0]);
            this.FillDropBox(dropPhysician, "PhysicianName", "PersonID", ds.Tables[1]);
            this.FillDropBox(dropSocial, "POName", "PersonID", ds.Tables[2]);
        }
    }

    //**********************************************************************************************************************
    private void FillDropBox(System.Web.UI.WebControls.DropDownList drop, string dtfield, string dvfield, DataTable dt)
    {
        ListItem item = new ListItem("Select One", "");
        drop.DataSource = dt;
        drop.DataTextField = dtfield;
        drop.DataValueField = dvfield;
        drop.DataBind();
        drop.Items.Insert(0, item);
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        string extra = "";
        if (dropCountry.SelectedIndex != 0)
        {
            myPatient.CountryID = Convert.ToInt32(dropCountry.SelectedItem.Value);
        }
        if (dropSocial.SelectedIndex != 0)
        {
            extra += "Patientid in (select patientid from tblprogramofficer where personid = " + dropSocial.SelectedValue + ")  ";
        }
        if (dropPhysician.SelectedIndex != 0)
        {
            if (extra == string.Empty)
            extra += "Patientid in (select patientid from tblpatientphysician where personid = " + dropPhysician.SelectedValue + ")  ";
            else
                extra += "and Patientid in (select patientid from tblpatientphysician where personid = " + dropPhysician.SelectedValue + ")  ";
        }


        string strSQL = "Select count(*) from tblpatient where ";
        strSQL += extra;
        if (myPatient.CountryID != 0)
        {
            if(strSQL==string.Empty)
            strSQL += "CountryID = " + myPatient.CountryID.ToString() + "";
            else
                strSQL += "and CountryID = " + myPatient.CountryID.ToString() + "";
        }

        string res= Convert.ToString(SqlHelper.ExecuteScalar(connString, CommandType.Text, strSQL));

        lblPatientCount.Text = res + "Results Found";
        lblPatientCount.Visible = true;
    }
}