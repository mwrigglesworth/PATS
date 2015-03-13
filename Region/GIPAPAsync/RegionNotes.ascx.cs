using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Region_GIPAPAsync_RegionNotes : System.Web.UI.UserControl
{
    GIPAP_Objects.Region myRegion = new GIPAP_Objects.Region();
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
        myRegion.RegionID = choice;
        myRegion.InflateRegionNotes(sessUse.Role);
        dgCnotes.DataSource = myRegion.RegionNotes;
        dgCnotes.DataBind();
        txtNote.Attributes.Add("onfocus", "if (this.value=='Add Note'){ this.value='' }");

    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (txtNote.Text.Trim() != "" && txtNote.Text.Trim() != "Add Note")
        {
            myRegion.AddRegionNote(sessUse.Username, txtNote.Text.Trim().Replace("\n", "<br>"));
            myRegion.InflateRegionNotes(sessUse.Role);
            dgCnotes.DataSource = myRegion.RegionNotes;
            dgCnotes.DataBind();
            txtNote.Text = "";
        }
    }
}