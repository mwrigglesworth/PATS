using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_GIPAPPatient : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if ((string)Session["loadC"] != "")
            {
                PanelControlPan.Visible = false;
                PanelLoad.Controls.Add(LoadControl((string)Session["loadC"]));
            }
        }
        catch { }
        Session["loadC"] = "";
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        PanelControlPan.Visible = false;
        Session["loadC"] = "GIPAPControlPanel/EditPatient.ascx";
        PanelLoad.Controls.Add(LoadControl((string)Session["loadC"]));
    }
}
