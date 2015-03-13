using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class MaxStation_ActivePatients : System.Web.UI.Page
{
    GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person();
    DataSet ds = new DataSet();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

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
        DataSet ds = myPerson.getMaxStationDatasets(sessUse, "activepatients");
        dgResults.DataSource = ds;
        dgResults.DataBind();
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found";
        dgResults.ItemStyle.Font.Size = FontUnit.Point(10);
    }
}
