using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TMF_UserQueues : System.Web.UI.UserControl
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        iucon.web.Controls.ParameterCollection parameters = iucon.web.Controls.ParameterCollection.Instance;
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            choice = Convert.ToInt32(parameters["choice"]);
        }
        catch
        {
            choice = 0;
        }
        if (sessUse.Program == "GIPAP")
        {
            LabelQueues.Text = sessUse.HomePageLinks(choice);
        }
        else if (sessUse.Program == "PINC")
        {
            PS_Objects.User psUse = new PS_Objects.User();
            psUse.TrustedLogin(sessUse.Username);
            LabelQueues.Text = psUse.HomePageLinks(choice);
        }
    }
}
