using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Person_PersonInfo : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        string a = "";
        try
        {
            a = Request.QueryString["a"].ToString();
        }
        catch { }
        if (a != "")
        {
            PanelAlert.Visible = true;
            if (a == "edit")
            {
                LabelAlert.Text = "Contact information has been updated";
            }
        }
        /////////
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myPerson = new GIPAP_Objects.Person(choice, sessUse.Role);
        dgCnotes.DataSource = myPerson.getPersonDatasets("notes");
        dgCnotes.DataBind();
        txtNote.Attributes.Add("onfocus", "if (this.value=='Add Note'){ this.value='' }");

        if (sessUse.Role == "TMFUser" || sessUse.UserID == myPerson.UserID)
        {
            LabelList.Text = "<a href=EditPerson.aspx?choice=" + choice.ToString() + ">Edit Contact Info</a><br><br>";
        }
        else
        {
            LabelList.Text = "<i><font color=gainsboro>Edit Contact Info</font></i><br><br>";
        }
        LabelActions.Text = "<a href=PersonDataSets.aspx?choice=" + choice.ToString() + "&dset=profilechanges>Profile Changes</a><br><br>";
        if (myPerson.PersonType == "MaxStation")
        {
            LabelActions.Text += "<a href=PersonDataSets.aspx?choice=" + choice.ToString() + "&dset=allpatients>Patients</a><br><br>";
            LabelActions.Text += "<a href=PersonDataSets.aspx?choice=" + choice.ToString() + "&dset=patientsneedingreapproval>Patients Needing Reapproval</a><br><br>";
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (txtNote.Text.Trim() != "" && txtNote.Text.Trim() != "Add Note")
        {
            myPerson.AddPersonNote(sessUse.Username, txtNote.Text.Trim());
            Response.Redirect("PersonInfo.aspx?choice=" + choice.ToString());
        }
    }
}
