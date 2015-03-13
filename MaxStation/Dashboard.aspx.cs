using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaxStation_Dashboard : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        LabelName.Text = sessUse.FullName;
        if (sessUse.Program == "GIPAP")
        {
            LabelProgram.Text = "<font color=steelblue size=6>GIPAP</font>";
        }
        if (sessUse.Username.StartsWith("MY"))
        {
            hlMYPAP.NavigateUrl = "/MYPAP/gipaptrusted.aspx?reqform=MyPap&user=" + sessUse.Username;
        }
        else if (sessUse.Username.StartsWith("TH") || sessUse.Username.StartsWith("ZA"))
        {
            hlTIPAP.NavigateUrl = "/TIPAP/gipaptrusted.aspx?reqform=TiPap&user=" + sessUse.Username;
        }
    }
}
