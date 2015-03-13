using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NovartisUser_Profile : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person(sessUse);
        //LabelPersonInfo.Text = myPerson.PersonHeader();
        LabelSummary.Text = myPerson.PersonInfo(sessUse.Role);
        if (sessUse.Username.ToLower() == "novartis" || sessUse.Username.ToLower() == "aenovartis")
        {
            LabelSummary.Text += "<br><br>[<font color=gray><i>You are signed in as a non-personalized account, so there is no profile information</i></font>]";
        }
        else
        {
            LabelSummary.Text += "<br><br>[<a href='' id='editSelf'>Edit</a>]";
        }

    }
}