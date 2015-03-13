using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_RemoveRequest : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        myRequest = new GIPAP_Objects.Request(Convert.ToInt32(Request.QueryString["rid"]), Convert.ToInt32(Request.QueryString["choice"]));
    }
    protected void ButtonYes_Click(object sender, EventArgs e)
    {
        myRequest.Resolve(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=removerequest&choice=" + myRequest.PatientID.ToString());
    }
    protected void ButtonNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientInfo.aspx?choice=" + myRequest.PatientID.ToString());
    }
}
