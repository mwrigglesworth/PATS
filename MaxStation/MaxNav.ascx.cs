using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaxStation_MaxNav : System.Web.UI.UserControl
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        LabelUser.Text = sessUse.FullName;
        LabelCountries.Text = sessUse.CountryList;
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
    }
}
