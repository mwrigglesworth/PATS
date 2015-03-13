using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NovartisUser_Dashboard : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    public int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        LabelName.Text = sessUse.FullName;
        LabelProgram.Text = "<font color=steelblue size=4>GIPAP NOA TIPAP</font>";

        GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person(sessUse);
        choice = myPerson.PersonID;

        if (sessUse.Username.ToLower() == "aenovartis")
        {
            Response.Redirect("../Reports/AEReportLog.aspx");
        }
        else
        {
            LabelCountries.Text = "<li><a href='../Reports/ActivityReport.aspx' class='lbAR'>Program Activity</a></li><br>" +
                        "<li><a href='../Reports/TotalsReport.aspx' class='lbAR'>Patient Totals</a></li><br>" +
                        "<li><a href='../Reports/Enrollment.aspx' class='lbAR'>Enrollment by Diagnosis</a></li><br>" +
                        "<li><a href='../Reports/CountryLists.aspx' class='lbAR'>Lists by Country</a></li><br>" +
                        "<li><a href='../Reports/PhysicianRequests.aspx' class='lbAR'>Physician Request Report</a></li><br>";
            if (sessUse.IsAdmin)
            {
                LabelCountries.Text += "<li><a href='../Reports/NPSPatientReport.aspx'>NPS Patient Report</a></li><br>";
                LabelCountries.Text += "<li><a href='../Reports/AEReportLog.aspx' class='lbAR'>Adverse Event Report Log</a></li><br>";
            }
            else
            {
                LabelCountries.Text += "<li><a href='../Reports/CountryAE.aspx' class='lbAR'>Adverse Events</a></li><br>";
            }
        }
    }
}