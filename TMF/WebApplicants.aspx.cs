using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TMF_WebApplicants : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        if (!Page.IsPostBack)
        {
            DataSet ds = sessUse.getWebApplicants();

            dgResults.DataSource = ds.Tables[0];
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Web Applicants";
        }
    }
}
