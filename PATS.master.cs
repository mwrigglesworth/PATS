using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class PATS : System.Web.UI.MasterPage
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    string rDirect = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessUse = (GIPAP_Objects.User)Session["sessUse"];
            if (sessUse.Username == "")
            {
                rDirect = "../Default.aspx";
            }
            else
            {
                //LabelUser.Text = sessUse.FullName;
            }
        }
        catch
        {
            Session["sessUse"] = new GIPAP_Objects.User();
            rDirect = "../Default.aspx";
        }
        if (rDirect != "")
        {
            Response.Redirect(rDirect);
        }
        if (sessUse.Role == "Novartis")
        {
            PanelQuickSearch.Controls.Add(LoadControl("NovartisUser/NovartisHeader.ascx"));
        }
        else
        {
            PanelQuickSearch.Controls.Add(LoadControl("TMF/GIPAPQuickSearch.ascx"));
        }
        //USER TYPES
        if (sessUse.Role == "TMFUser")
        {
            hlGIPAP.Visible = true;
            hlPINC.Visible = true;
            hlMYPAP.Visible = true;
            hlTIPAP.Visible = true;
            hlRAP.Visible = true;
            PanelNav.Controls.Add(LoadControl("TMF/TMFNav.ascx"));
           hlMYPAP.NavigateUrl = "../MYPAP/gipaptrusted.aspx?reqform=MyPap&user=" + sessUse.Username;
           hlTIPAP.NavigateUrl = "../TIPAP/gipaptrusted.aspx?reqform=TiPap&user=" + sessUse.Username;
           if (sessUse.RAP)
           {
               hlRAP.NavigateUrl = "../RAP/gipaptrusted.aspx?reqform=RAP&user=" + sessUse.Username;
           }
        }
        else if (sessUse.Role == "MaxStation")
        {
            hlGIPAP.Visible = true;
            hlPINC.Visible = true;
            hlMYPAP.Visible = true;
            hlTIPAP.Visible = true;
            if (sessUse.RAP)
            {
                hlRAP.Visible = true;
                hlRAP.NavigateUrl = "../RAP/gipaptrusted.aspx?reqform=RAP&user=" + sessUse.Username;
            }
            PanelNav.Controls.Add(LoadControl("MaxStation/MaxNav.ascx"));
            if (sessUse.Username.StartsWith("MY"))
            {
               hlMYPAP.NavigateUrl = "../MYPAP/gipaptrusted.aspx?reqform=MyPap&user=" + sessUse.Username;
            }
            else if (sessUse.Username.StartsWith("TH") || sessUse.Username.StartsWith("ZA"))
            {
               hlTIPAP.NavigateUrl = "../TIPAP/gipaptrusted.aspx?reqform=TiPap&user=" + sessUse.Username;
            }
        }
        else if (sessUse.Role == "Physician")
        {
            hlGIPAP.Visible = false;
            hlPINC.Visible = false;
            hlMYPAP.Visible = false;
            if (sessUse.TIPAPPhys)
            {
                hlTIPAP.Visible = true;
                hlTIPAP.NavigateUrl = "/TIPAP/gipaptrusted.aspx?reqform=TiPap&user=" + sessUse.Username;
            }
            else
                hlTIPAP.Visible = false;
            hlRAP.Visible = false;
            PanelNav.Controls.Add(LoadControl("Physician/PhysNav.ascx"));
        }
        else if (sessUse.Role.StartsWith("FC")) //central hub and branches, don't use call center any more
        {
            PanelNav.Controls.Add(LoadControl("FinancialEvaluator/FENav.ascx"));
            if (sessUse.ExpireDate < DateTime.Today && Request.Url.ToString().IndexOf("ChangePassword.aspx") == -1)
            {
                Response.Redirect("~/TMF/ChangePassword.aspx");
            }
        }
        else if (sessUse.Role == "Novartis")
        {
            PanelNav.Controls.Add(LoadControl("NovartisUser/NovartisNav.ascx"));
        }
    }
}
