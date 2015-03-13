using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubRegion_GIPAPSubRegion : System.Web.UI.MasterPage
{
    GIPAP_Objects.SubRegion mySub = new GIPAP_Objects.SubRegion();
    int choice;
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        mySub = new GIPAP_Objects.SubRegion(choice, sessUse.Role);

        LabelSRInfo.Text = "<h1><font color=steelblue>" + mySub.SubRegionName + "</font></h1>";

        dgCountries.DataSource = mySub.CountryDT;
        dgCountries.DataBind();
    }
}
