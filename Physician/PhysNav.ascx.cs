using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_PhysNav : System.Web.UI.UserControl
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        LabelUser.Text = sessUse.FullName;
        if (sessUse.AcceptingNewApps)
        {
            LabelApp.Text = "<a href=../Application/GIPAPApplication.aspx>Submit an Application</a>";
        }
        else
        {
            LabelApp.Text = "<font color=gray>No longer accepting applications in your country</font>";
        }
        //tasigna
        if (sessUse.Tasigna > 0)
        {
            LabelTasigna.Text = "<a href=''><font color=#FADDFA>Tasigna</font></a><ul><li><a href=../Physician/DataDisplay.aspx?dset=changetreatment>Active - Change Treatment</a></li><li><a href=../Physician/DataDisplay.aspx?dset=reactivatepatients>Closed - Reactivate</a></li></ul>";
        }
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
    }
}