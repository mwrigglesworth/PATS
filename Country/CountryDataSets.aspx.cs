using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Country_CountryDataSets : System.Web.UI.Page
{
    GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
    GIPAP_Objects.User sessUse;
    int choice;
    string dset;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        dset = Request.QueryString["dset"];

        myCountry.CountryID = choice;
        DataSet ds = myCountry.GetCountryDataSets(dset);
        LabelCountry.Text = ds.Tables[0].Rows[0]["countryname"].ToString();
        dgResults.DataSource = ds.Tables[1];
        dgResults.DataBind();
        LabelResultCount.Text = ds.Tables[1].Rows.Count.ToString() + " " + dset;

        if (dset == "Patients")
        {
            if (ds.Tables[2].Rows.Count > 0)
            {
                LabelResultCount.Text += " | ";
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    LabelResultCount.Text += ds.Tables[2].Rows[i]["CountryCount"].ToString() + " " + ds.Tables[2].Rows[i]["gipapstatus"].ToString() + "  ";
                }
            }
        }
    }
}
