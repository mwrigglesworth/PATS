using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class TMF_CaseNoteAlerts : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);

        if (!Page.IsPostBack)
        {
            DataTable ds = sessUse.getDatasets(Convert.ToInt32(Request.QueryString["choice"]), "casenotealerts").Tables[0];

            dgReapps.DataSource = ds;
            dgReapps.DataBind();
            LabelResultCount.Text = ds.Rows.Count.ToString() + " Case Note Alerts";
        }
    }
    protected void ButtonRemove_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgReapps.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgReapps.Rows[i].FindControl("lblCaseNoteID");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgReapps.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked)
            {
                sessUse.RemoveCaseNoteAlert(Convert.ToInt32(IdLabel.Text));
            }
        }
        dgReapps.Visible = ButtonRemove.Visible = false;
        LabelRemove.Visible = true;
        LabelRemove.Text += " [<a href=CaseNoteAlerts.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
    }
}
