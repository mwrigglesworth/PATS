using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GIPAPTrusted : System.Web.UI.Page
{
    GIPAP_Objects.User myUser = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UrlReferrer.ToString().IndexOf("webserv") != -1 || Request.UrlReferrer.ToString().IndexOf("173.160.130.249") != -1 || Request.UrlReferrer.ToString().IndexOf("192.168.100") != -1 || Request.UrlReferrer.ToString().IndexOf("maxaid.org") != -1 || Request.UrlReferrer.ToString().IndexOf("GIPAPTrusted.aspx") != -1)
        {
            if (Request.QueryString["reqform"].ToString() == "PATS")
            {
                myUser.TrustedLogin(Request.QueryString["user"].ToString());
                Session["sessUse"] = myUser.Login(Request.Browser.Platform.ToString(), Request.UserHostAddress.ToString());
                Session.Timeout = 240;
                Response.Redirect(myUser.HomePage);
            }
            myUser = (GIPAP_Objects.User)Session["sessUse"];
            if (Request.QueryString["reqform"].ToString() == "Patient")
            {
                Response.Redirect("/PSdir/pstrusted.aspx?reqform=Patient&user=" + myUser.Username + "&pin=" + Request.QueryString["pin"].ToString());
            }
            else if (Request.QueryString["reqform"].ToString() == "Country")
            {
                Response.Redirect("/PSdir/pstrusted.aspx?reqform=Country&user=" + myUser.Username + "&id=" + Request.QueryString["id"].ToString());
            }
            else if (Request.QueryString["reqform"].ToString() == "home")
            {
                Response.Redirect("../PSdir/pstrusted.aspx?reqform=home&user=" + myUser.Username);
            }
        }
    }
}
