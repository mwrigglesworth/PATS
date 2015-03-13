using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TMF_QuickSearch : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        DataSet ds = sessUse.QuickSearch(Request.QueryString["pin"], Request.QueryString["first"], Request.QueryString["last"], Request.QueryString["noa"]);
        dgResults.DataSource = ds;
        dgResults.DataBind();
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Matching Results";
    }
}
