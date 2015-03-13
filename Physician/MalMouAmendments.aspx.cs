using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_MalMouAmendments : System.Web.UI.Page
{

    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        //myPhysician = new GIPAP_Objects.Physician(sessUse, "");
        LabelName.Text = sessUse.FullName;
        LabelDate.Text = LabelPatDate.Text = DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y");
    }

    protected void ButtonAccept_Click(object sender, EventArgs e)
    {
        myPhysician.UpdateMOU(sessUse);
        Session["sessUse"] = sessUse;
        Response.Redirect("Dashboard.aspx");
    }

    protected void ButtonDecline_Click(object sender, EventArgs e)
    {
        Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
    }
}