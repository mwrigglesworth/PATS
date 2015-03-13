using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class TMF_SentCaseNoteAlerts : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        if (!Page.IsPostBack)
        {
            DataTable ds = sessUse.getDatasets(Convert.ToInt32(Request.QueryString["choice"]), "sentcasenotealerts").Tables[0];
            dgReapps.DataSource = ds;
            dgReapps.DataBind();
            LabelResultCount.Text = ds.Rows.Count.ToString() + " Case Notes";
        }
    }
    protected void dgReapps_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        // Retrieve the row that contains the button clicked 
        // by the user from the Rows collection.      
        GridViewRow row = dgReapps.Rows[index];

        Label lbcnoteid = (Label)row.FindControl("lblCaseNoteID");
        Label lbpatid = (Label)row.FindControl("lblPatientID");
        TextBox txtRemove = (TextBox)row.FindControl("txtCnote");

        if (txtRemove.Text != "")
        {
            sessUse.RemoveCaseNoteAlert(Convert.ToInt32(lbcnoteid.Text));

            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myPatient.PatientID = Convert.ToInt32(lbpatid.Text);
            myPatient.AddCaseNote(sessUse.Username, txtRemove.Text.Trim().Replace("\n", "<br>"));
            (dgReapps.Rows[index].FindControl("lblAlert") as Label).Text.Replace("<br><br><font color=red>Note Required</font>", "");
            (dgReapps.Rows[index].FindControl("lblAlert") as Label).Text += "<br><br><font color=red>Case Note Removed</font>";
            (dgReapps.Rows[index].FindControl("PanelRemove") as Panel).Visible = false;
        }
        else
        {
            (dgReapps.Rows[index].FindControl("lblAlert") as Label).Text += "<br><br><font color=red>Note Required</font>";
        }
    }
}
