using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultError : System.Web.UI.Page
{
    GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    string uname;
    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            uname = sessUse.Username;
        }
        catch
        {
            uname = "Blank";
        }
        if (!Page.IsPostBack)
        {
            string sErr = (string)Session["sessError"].ToString();
            if (sErr.IndexOf("A potentially dangerous Request.Form") != -1)
            {
                Response.Redirect("closepage.htm");
            }
            else if (sErr.IndexOf("Object reference not set to an instance of an object.") != -1 && (uname == "Blank" || uname == ""))
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                myEmail.SendErrorEmail(uname, Request.UrlReferrer.ToString(), sErr, Request.UserHostAddress.ToString());
            }
        }
    }
}
