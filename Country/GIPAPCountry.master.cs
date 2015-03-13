using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Country_GIPAPCountry : System.Web.UI.MasterPage
{
    GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
    int choice;
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myCountry = new GIPAP_Objects.Country(choice, sessUse.Role);

        LabelCountryInfo.Text = myCountry.CountryHeader(sessUse.Role);
        LabelCountrySummary.Text = myCountry.CountryInfo(sessUse.Role);
        LabelDisease.Text = myCountry.Notes;
    }
    protected void DropDisease_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDisease.SelectedValue == "Country Program Info")
        {
            LabelDisease.Text = myCountry.Notes;
        }
        else if (DropDisease.SelectedValue == "CML")
        {
            LabelDisease.Text = myCountry.CMLInfoTab();
        }
        else if (DropDisease.SelectedValue == "GIST")
        {
            LabelDisease.Text = myCountry.GISTInfoTab();
        }
        else if (DropDisease.SelectedValue == "Adjuvant GIST")
        {
            LabelDisease.Text = myCountry.AdjGISTInfoTab();
        }
        else if (DropDisease.SelectedValue == "PH+ ALL")
        {
            LabelDisease.Text = myCountry.ALLInfoTab();
        }
        else if (DropDisease.SelectedValue == "DFSP")
        {
            LabelDisease.Text = myCountry.DFSPInfoTab();
        }
        else if (DropDisease.SelectedValue == "MDS / MPD")
        {
            LabelDisease.Text = myCountry.MDSInfoTab();
        }
        else if (DropDisease.SelectedValue == "Systemic Mastocytosis")
        {
            LabelDisease.Text = myCountry.SysMastInfoTab();
        }
        else if (DropDisease.SelectedValue == "HES / CEL")
        {
            LabelDisease.Text = myCountry.HesInfoTab();
        }
        else if (DropDisease.SelectedValue == "Tasigna")
        {
            LabelDisease.Text = myCountry.TasignaInfoTab();
        }
    }
}
