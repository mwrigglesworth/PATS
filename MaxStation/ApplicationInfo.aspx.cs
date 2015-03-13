using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class MaxStation_ApplicationInfo : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessUse = (GIPAP_Objects.User)Session["sessUse"];
        }
        catch
        {
            Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
        }
        if (!Page.IsPostBack)
        {
            CalendarApps.SelectedDate = DateTime.Today;
            ds = sessUse.getApplications(CalendarApps.SelectedDate);
            dgResults.DataSource = ds;
            dgResults.DataBind();
            LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
        }
    }
    protected void CalendarApps_SelectionChanged(object sender, EventArgs e)
    {
        PanelResults.Visible = true;
        ds = sessUse.getApplications(CalendarApps.SelectedDate);
        dgResults.DataSource = ds;
        dgResults.DataBind();
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found.";
    }
}
