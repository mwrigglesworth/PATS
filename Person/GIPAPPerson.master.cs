using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Person_GIPAPPerson : System.Web.UI.MasterPage
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myPerson = new GIPAP_Objects.Person(choice, sessUse.Role);
        LabelPersonInfo.Text = myPerson.PersonHeader();
        LabelSummary.Text = myPerson.PersonInfo(sessUse.Role);
        if (sessUse.Role == "TMFUser")
        {
            LabelUser.Text = myPerson.UserInfo(sessUse.Username);
            if (myPerson.UserName == null && myPerson.CountryID != 0 && sessUse.Username == "mwrigglesworth")
            {
                lbUser.Visible = true;
            }
        }

        if (myPerson.PersonType == "TMFUser")
        {
            LabelSummaryHeader.Text = "PO Info";
        }
        else if (myPerson.PersonType == "MaxStation")
        {
            LabelSummaryHeader.Text = "Max Station Info";
        }
        else if (myPerson.PersonType == "Novartis")
        {
            LabelSummaryHeader.Text = "Novartis Info";
        }
    }
    protected void lbUser_Click(object sender, EventArgs e)
    {
        myPerson.CreatePersonUser(sessUse.Username);
        Response.Redirect("PersonInfo.aspx?choice=" + myPerson.PersonID.ToString());
    }
}
