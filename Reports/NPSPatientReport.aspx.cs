using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Reports_NPSPatientReport : System.Web.UI.Page
{
    DataTable ds = new DataTable();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        string dset = Request.QueryString["dset"];
        try
        {
            choice = Convert.ToInt32(Request.QueryString["choice"]);
        }
        catch
        {
            choice = 0;
        }

        ds = sessUse.NPSPatientReport().Tables[0];
        this.FillDataGrid(ds);
        LabelTitle.Text = "NPS Patient Report";
        dgResults.ItemStyle.Font.Size = FontUnit.Point(10);
        dgResults.HeaderStyle.Font.Size = FontUnit.Point(10);
    }
    private void FillDataGrid(DataTable dt)
    {
        dgResults.DataSource = dt;
        dgResults.DataBind();
        dgResults.HeaderStyle.Font.Bold = true;

        LabelResultCount.Text = dt.Rows.Count.ToString() + " Results Found";


    }


    protected void dgResults_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgResults.CurrentPageIndex = e.NewPageIndex;
        dgResults.DataBind();
        LabelResultCount.Text = (e.NewPageIndex) * dgResults.PageSize + "-" + ((e.NewPageIndex + 1) < dgResults.PageCount ? (e.NewPageIndex + 1) * dgResults.PageSize : (e.NewPageIndex) * dgResults.PageSize + dgResults.Items.Count) + " Records";
    }

    protected void dgResults_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
}