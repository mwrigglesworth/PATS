using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Clinic_FullPageClinicReport : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];

        GIPAP_Objects.Clinic myClinic = new GIPAP_Objects.Clinic();
        DataSet ds = myClinic.getClinicPatients(Convert.ToInt32(Request.QueryString["choice"]), sessUse.Role);
        dgResults.DataSource = ds.Tables[0];
        dgResults.DataBind();
        LabelClinicName.Text = ds.Tables[1].Rows[0]["clinicname"].ToString();
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Patients";
    }
}
