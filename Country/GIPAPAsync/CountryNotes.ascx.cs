using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Country_GIPAPAsync_CountryNotes : System.Web.UI.UserControl
{
    GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
    int choice;
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        iucon.web.Controls.ParameterCollection parameters = iucon.web.Controls.ParameterCollection.Instance;
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            choice = Convert.ToInt32(parameters["choice"]);
        }
        catch
        {
            choice = 0;
        }
        myCountry.CountryID = choice;
        myCountry.InflateCountryNotes(sessUse.Role);
        dgCnotes.DataSource = myCountry.CountryNotes;
        dgCnotes.DataBind();
        txtNote.Attributes.Add("onfocus", "if (this.value=='Add Note'){ this.value='' }");
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (txtNote.Text.Trim() != "" && txtNote.Text.Trim() != "Add Note")
        {
            myCountry.AddCountryNote(sessUse.Username, txtNote.Text.Trim().Replace("\n", "<br>"));
            myCountry.InflateCountryNotes(sessUse.Role);
            dgCnotes.DataSource = myCountry.CountryNotes;
            dgCnotes.DataBind();
            txtNote.Text = "";
        }
    }
}
