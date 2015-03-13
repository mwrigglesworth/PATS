using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_CountryAE : System.Web.UI.Page
{
    int choice;
    GIPAP_Objects.CountryReport myReport = new GIPAP_Objects.CountryReport();
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            choice = Convert.ToInt32(Request.QueryString["choice"]);
        }
        catch
        {
            choice = 0;
        }
        if (!Page.IsPostBack)
        {

            dropCountry.DataSource = sessUse.CountryTable;
            dropCountry.DataTextField = "countryname";
            dropCountry.DataValueField = "countryid";
            dropCountry.DataBind();
            dropCountry.Items.Insert(0, "Select a Country");

            dropCountry.SelectedValue = choice.ToString();
        }
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        myReport.GetCountrySAE(Convert.ToInt32(dropCountry.SelectedValue), sessUse.Role);
        PanelGIPAPTotals.Visible = true;
        LabelResultCount.Text = myReport.ResultCount;
        dgResults.DataSource = myReport.ResultSet.Tables[1];
        dgResults.DataBind();
        dgResults.HeaderStyle.Font.Bold = true;
    }
}