using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_ContactInfo : System.Web.UI.Page
{
    int choice;
    int cid;
    public string patientid;
    public string contactid;
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Contact myContact = new GIPAP_Objects.Contact();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        cid = Convert.ToInt32(Request.QueryString["cid"]);
        patientid = choice.ToString();
        contactid = cid.ToString();

        myContact = new GIPAP_Objects.Contact(cid, choice);
        LabelContactInfo.Text = myContact.ContactInfo(sessUse.Role);
    }
    protected void ButtonEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditContact.aspx?choice=" + choice.ToString() + "&cid=" + cid.ToString());
    }
}
