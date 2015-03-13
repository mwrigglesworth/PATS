using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NovartisUser_NovartisNav : System.Web.UI.UserControl
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        LabelUser.Text = sessUse.FullName;
        if (sessUse.Username.ToLower() == "aenovartis")
        {
            LabelCountries.Text = "<li><a href='../Reports/AEReportLog.aspx'>Adverse Event Report Log</a></li>";
        }
        else
        {
            LabelCountries.Text = "<li><a href='../Reports/ActivityReport.aspx'>Program Activity</a></li>" +
                        "<li><a href='../Reports/TotalsReport.aspx'>Patient Totals</a></li>" +
                        "<li><a href='../Reports/CountryLists.aspx'>Lists by Country</a></li>" +
                        "<li><a href='../Reports/Enrollment.aspx'>Enrollment by Diagnosis</a></li>" +
                        "<li><a href='../Reports/PhysicianRequests.aspx'>Physician Request Report</a></li>";
            if (sessUse.IsAdmin)
            {
                LabelCountries.Text += "<li><a href='../Reports/NPSPatientReport.aspx'>NPS Patient Report</a></li>";
                LabelCountries.Text += "<li><a href='../Reports/AEReportLog.aspx'>Adverse Event Report Log</a></li>";
                //LabelCountries.Text += "<li><a href='../Reports/PhysicianRequests.aspx'>Physician Request Report</a></li>";
            }
            else 
            {
                try
                {
                    if (sessUse.CountryTable.Rows.Count > 0)
                    {
                        LabelCountries.Text += "<li><a href='../Reports/CountryAE.aspx'>Adverse Events</a></li>";
                    }
                }
                catch
                {
                    Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
                }
            }
        }
    }
    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
    }
}