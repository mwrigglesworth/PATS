using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_Dashboard : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        LabelName.Text = sessUse.FullName;

        if (sessUse.TIPAPPhys)
        {
            hlTIPAP.Visible = true;
            hlTIPAP.NavigateUrl = "/TIPAP/gipaptrusted.aspx?reqform=TiPap&user=" + sessUse.Username;
        }
    }
}