using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_GIPAPAsync_PhysNotes : System.Web.UI.UserControl
{
    GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

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
        myPhysician.PhysicianID = choice;
        dgCnotes.DataSource = myPhysician.GetPhysicianDataSets("notes");
        dgCnotes.DataBind();
        txtNote.Attributes.Add("onfocus", "if (this.value=='Add Note'){ this.value='' }");
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (txtNote.Text.Trim() != "" && txtNote.Text.Trim() != "Add Note")
        {
            myPhysician.AddPersonNote(sessUse.Username, txtNote.Text.Trim());
            dgCnotes.DataSource = myPhysician.GetPhysicianDataSets("notes");
            dgCnotes.DataBind();
            txtNote.Text = "";
        }
    }
}
