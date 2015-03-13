using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_GIPAPAsync_CaseNotes : System.Web.UI.UserControl
{
    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
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
        dgCnotes.DataSource = myPatient.getCaseNotes(choice);
        dgCnotes.DataBind();
        txtNote.Attributes.Add("onfocus", "if (this.value=='Add Note'){ this.value='' }");
        if (dropYear.Items.Count == 0)
        {
            dropYear.Items.Add(DateTime.Now.Year.ToString());
            dropYear.Items.Add(DateTime.Now.AddYears(1).Year.ToString());
            dropYear.Items.Add(DateTime.Now.AddYears(2).Year.ToString());
            dropYear.Items.Add(DateTime.Now.AddYears(3).Year.ToString());
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (txtNote.Text.Trim() != "" && txtNote.Text.Trim() != "Add Note")
        {
            myPatient.AddCaseNote(sessUse.Username, txtNote.Text.Trim().Replace("\n", "<br>"));
            dgCnotes.DataSource = myPatient.getCaseNotes(choice);
            dgCnotes.DataBind();
            txtNote.Text = "";
        }
    }
    protected void lbAlert_Click(object sender, EventArgs e)
    {
        ButtonAdd.Visible = false;
        lbAlert.Visible = false;
        PanelAlert.Visible = true;
        dropYear.SelectedValue = DateTime.Now.Year.ToString();
        dropDay.SelectedValue = DateTime.Now.Day.ToString();
        dropMonth.SelectedValue = DateTime.Now.Month.ToString();
    }
    protected void ButtonAlert_Click(object sender, EventArgs e)
    {
        DateTime dt = new DateTime();
        try
        {
            dt = Convert.ToDateTime(dropMonth.SelectedValue + "/" + dropDay.SelectedValue + "/" + dropYear.SelectedValue);
        }
        catch
        {
            LabelErr.Text = "Invalid Date";
            return;
        }
        sessUse.AddCaseNoteAlert(choice, txtNote.Text.Replace("\n", "<br>"), dt);
        dgCnotes.DataSource = myPatient.getCaseNotes(choice);
        dgCnotes.DataBind();
        txtNote.Text = "";
        ButtonAdd.Visible = true;
        lbAlert.Visible = true;
        PanelAlert.Visible = false;
    }
}
