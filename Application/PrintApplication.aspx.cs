using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Application_PrintApplication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GIPAP_Objects.GIPAPApplicant myApplicant = new GIPAP_Objects.GIPAPApplicant(Convert.ToInt32(Request.QueryString["choice"]));
        LabelApp.Text = myApplicant.PrintApplication();
    }
}
