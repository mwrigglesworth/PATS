using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_CountryLists : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.CountryReport myReport = new GIPAP_Objects.CountryReport();
    int choice;

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
    protected void ButtonSub_Click(object sender, EventArgs e)
    {
        if (dropList.SelectedValue == "Patients")
        {
            myReport.GetCountryPatients(Convert.ToInt32(dropCountry.SelectedValue), sessUse.Role);
            dgResults.DataSource = myReport.ResultSet.Tables[0];
            dgResults.DataBind();
            dgResults.Font.Size = FontUnit.Point(10);
        }
        else if (dropList.SelectedValue == "MaxStations")
        {
            myReport.GetCountryMaxStations(Convert.ToInt32(dropCountry.SelectedValue));
            dgResults.DataSource = myReport.ResultSet.Tables[0];
            dgResults.DataBind();
            dgResults.Font.Size = FontUnit.Point(12);
        }
        else if (dropList.SelectedValue == "Physicians")
        {
            myReport.GetCountryPhysicians(Convert.ToInt32(dropCountry.SelectedValue), sessUse.Role);
            dgResults.DataSource = myReport.ResultSet.Tables[0];
            dgResults.DataBind();
            dgResults.Font.Size = FontUnit.Point(12);
        }
        else if (dropList.SelectedValue == "Clinics")
        {
            myReport.GetCountryClinics(Convert.ToInt32(dropCountry.SelectedValue), sessUse.Role);
            dgResults.DataSource = myReport.ResultSet.Tables[0];
            dgResults.DataBind();
            dgResults.Font.Size = FontUnit.Point(12);
        }
        PanelGIPAPTotals.Visible = true;
        LabelResultCount.Text = dropCountry.SelectedItem.Text + " " + dropList.SelectedValue + " " + dgResults.Items.Count.ToString() + " Results";
    }
}