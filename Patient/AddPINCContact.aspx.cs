using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Patient_AddPINCContact : System.Web.UI.Page
{


    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
    string ppin; int pid;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessUse = (GIPAP_Objects.User)Session["sessUse"];
            ppin = Request.QueryString["pin"];
            pid = Convert.ToInt32(Request.QueryString["choice"]);
            myPatient = new GIPAP_Objects.Patient();//pID, sessUse.UserID);
            myPatient.PIN = ppin;
            myPatient.PatientID = pid;
        }
        catch
        {
            Session["sessUse"] = new GIPAP_Objects.User();
            Response.Redirect("Default.aspx");
        }
    }

    protected void ButtonContact_Click(object sender, System.EventArgs e)
    {
        myPatient.LogContact(cbContact, sessUse.Username);
        myPatient.LogContact(cbContact2, sessUse.Username);
        Response.Redirect("../Patient/PatientInfo.aspx?choice=" + myPatient.PatientID.ToString());

    }
    
}