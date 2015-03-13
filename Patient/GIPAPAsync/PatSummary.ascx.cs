using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_GIPAPAsync_PatSummary : System.Web.UI.UserControl
{
    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    public int choice;
    public string pin = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        iucon.web.Controls.ParameterCollection parameters = iucon.web.Controls.ParameterCollection.Instance;
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            choice = Convert.ToInt32(parameters["choice"]);
        }
        catch
        {
            choice = 0;
        }
        LabelSummary.Text = myPatient.PatientSummary(choice, sessUse);
        pin = myPatient.PIN;
        //show full if pending
        if (myPatient.GIPAPStatus == "Pending")
        {
            PanelSummary.Visible = false;
            PanelFull.Visible = true;
            PanelFull.Controls.Add(LoadControl("PatInfo.ascx"));
            PanelFull.Controls.Add(LoadControl("StatInfo.ascx"));
            if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation" || Request.UrlReferrer.ToString().ToLower().IndexOf("patientinfo.aspx") == -1)
            {
                PanelFull.Controls.Add(LoadControl("DiagInfo.ascx"));
            }
            lbShowFull.Text = "View Summary";
        }
        if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
        {
            LabelEAA.Text = myPatient.AutoApproveEnabled();
            if (myPatient.EnableAutoApprove)
            {
                lbEAA.Text = "[Disable]";
            }
            else
            {
                lbEAA.Text = "[Enable]";
            }
            LabelEAC.Text = myPatient.AutoCloseEnabled();
            if (sessUse.Role == "TMFUser")
            {
                if (myPatient.EnableAutoClose)
                {
                    lbEAC.Text = "[Disable]";
                }
                else
                {
                    lbEAC.Text = "[Enable]";
                }
            }
            else
            {
                lbEAC.Visible = false;
            }
        }
    }
    protected void lbShowFull_Click(object sender, EventArgs e)
    {
        if (lbShowFull.Text == "View Full Patient Info")
        {
            PanelSummary.Visible = false;
            PanelFull.Visible = true;
            PanelFull.Controls.Add(LoadControl("PatInfo.ascx"));
            PanelFull.Controls.Add(LoadControl("StatInfo.ascx"));
            if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation" || Request.UrlReferrer.ToString().ToLower().IndexOf("patientinfo.aspx") == -1)
            {
                PanelFull.Controls.Add(LoadControl("DiagInfo.ascx"));
            }
            lbShowFull.Text = "View Summary";
        }
        else
        {
            PanelSummary.Visible = true;
            PanelFull.Visible = false;
            lbShowFull.Text = "View Full Patient Info";
        }
    }
    protected void lbEAA_Click(object sender, EventArgs e)
    {
        myPatient.UpdateAutoApprove(sessUse.Username);
        LabelSummary.Text = myPatient.PatientSummary(choice, sessUse);
        LabelEAA.Text = myPatient.AutoApproveEnabled();
        if (myPatient.EnableAutoApprove)
        {
            lbEAA.Text = "[Disable]";
        }
        else
        {
            lbEAA.Text = "[Enable]";
        }
    }
    protected void lbEAC_Click(object sender, EventArgs e)
    {
        myPatient.UpdateAutoClose(sessUse.Username);
        LabelSummary.Text = myPatient.PatientSummary(choice, sessUse);
        LabelEAC.Text = myPatient.AutoCloseEnabled();
        if (myPatient.EnableAutoClose)
        {
            lbEAC.Text = "[Disable]";
        }
        else
        {
            lbEAC.Text = "[Enable]";
        }
    }
}
