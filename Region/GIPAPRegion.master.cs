using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_GIPAPRegion : System.Web.UI.MasterPage
{
    GIPAP_Objects.Region myReg = new GIPAP_Objects.Region();
    int choice;
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myReg = new GIPAP_Objects.Region(choice, sessUse.Role);

        LabelRInfo.Text = "<h1><font color=steelblue>" + myReg.RegionName + "</font></h1>";

        dgSubs.DataSource = myReg.SubRegionDT;
        dgSubs.DataBind();

        dgCountries.DataSource = myReg.CountryDT;
        dgCountries.DataBind();
    }
}
