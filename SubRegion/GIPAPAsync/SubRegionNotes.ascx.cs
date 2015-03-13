using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SubRegion_GIPAPAsync_SubRegionNotes : System.Web.UI.UserControl
{
    GIPAP_Objects.SubRegion mySubRegion = new GIPAP_Objects.SubRegion();
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
        mySubRegion.SubRegionID = choice;
        mySubRegion.InflateSubRegionNotes(sessUse.Role);
        dgCnotes.DataSource = mySubRegion.SubRegionNotes;
        dgCnotes.DataBind();
        txtNote.Attributes.Add("onfocus", "if (this.value=='Add Note'){ this.value='' }");
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (txtNote.Text.Trim() != "" && txtNote.Text.Trim() != "Add Note")
        {
            mySubRegion.AddSubRegionNote(sessUse.Username, txtNote.Text.Trim().Replace("\n", "<br>"));
            mySubRegion.InflateSubRegionNotes(sessUse.Role);
            dgCnotes.DataSource = mySubRegion.SubRegionNotes;
            dgCnotes.DataBind();
            txtNote.Text = "";
        }
    }
}