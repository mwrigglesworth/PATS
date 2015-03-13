using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Clinic_ClinicDataSets : System.Web.UI.Page
{
    GIPAP_Objects.Clinic myClinic = new GIPAP_Objects.Clinic();
    GIPAP_Objects.User sessUse;
    int choice;
    string dset;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        dset = Request.QueryString["dset"];
        dgResults.DataSource = myClinic.getClinicDataSets(choice, dset);
        dgResults.DataBind();
        LabelHeader.Text = dgResults.Items.Count.ToString() + " Results Found";
    }
}
