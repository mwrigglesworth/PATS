using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TMF_AllUsers : System.Web.UI.UserControl
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();


    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        if (sessUse.Program == "GIPAP")
        {
            LabelAllUsers.Text = sessUse.AllUserLinks();
        }
        else if (sessUse.Program == "PINC")
        {
            PS_Objects.User psUse = new PS_Objects.User();
            psUse.TrustedLogin(sessUse.Username);
            LabelAllUsers.Text = psUse.HomePageAllUsers();
        }
    }
}
