using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Clinic_GIPAPClinic : System.Web.UI.MasterPage
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Clinic myClinic = new GIPAP_Objects.Clinic();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);

        myClinic = new GIPAP_Objects.Clinic(choice, sessUse.Role);
        LabelClinicHeader.Text = myClinic.ClinicHeader();
        LabelClinicInfo.Text = myClinic.ClinicInfo(sessUse.Role);
    }
}
