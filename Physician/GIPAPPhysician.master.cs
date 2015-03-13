using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_GIPAPPhysician : System.Web.UI.MasterPage
{
    GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();
    GIPAP_Objects.User sessUse;
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myPhysician = new GIPAP_Objects.Physician(choice, sessUse.Role);
        if (Request.Url.ToString().IndexOf("PhysicianInfo.aspx") == -1)
        {
            LabelPhysInfo.Text = myPhysician.PhysicianInfo(sessUse.Role, false,sessUse.Username);
            LabelClinicsLeft.Text = "<font class='lbl'>Clinic(s):</font>" + myPhysician.ClinicName;
            LabelApproveLeft.Text = myPhysician.ApprovedLabel();
            LabelMOULeft.Text = myPhysician.MOULabel();
            LabelNOALeft.Text = myPhysician.NOALabel();
        }
        else
        {
            LabelPhysInfo.Text = myPhysician.PhysicianInfo(sessUse.Role, true,sessUse.Username);
            PanelFullInfo.Visible = false;
        }
    }
}
