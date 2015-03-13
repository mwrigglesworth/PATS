using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_PatientReport : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician(Convert.ToInt32(Request.QueryString["choice"]), sessUse.Role);
        dgResults.DataSource = myPhysician.GetPhysicianPatients(sessUse.Role);
        dgResults.DataBind();
        LabelResultCount.Text = myPhysician.FirstName + " " + myPhysician.LastName + " - " + dgResults.Items.Count.ToString() + " Patients";
    }
}
