using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaxStation_UserQueues : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GIPAP_Objects.User sessUse = (GIPAP_Objects.User)Session["sessUse"];
        LabelQueues.Text = sessUse.HomePageLinks();
    }
}
