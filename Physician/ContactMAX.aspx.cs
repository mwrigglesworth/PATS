using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_ContactMAX : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessUse = (GIPAP_Objects.User)Session["sessUse"];
        }
        catch
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void ButtonSend_Click(object sender, System.EventArgs e)
    {
        GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email("gipap@themaxfoundation.org", "gipap@themaxfoundation.org", "", "", txtSubject.Text, sessUse.GetUserInfo() + "\n\n" + txtMessage.Text, 0, 0, "Contact", 0);
        myEmail.Send("Contact");
        PanelContact.Visible = false;
        PanelConfirmation.Visible = true;
    }

    protected void ButtonCancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(sessUse.HomePage);
    }
}