using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_PhysicianTransferRequest : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.AddRemove myAR;
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myAR = new GIPAP_Objects.AddRemove(choice, Request.QueryString["Sender"].ToString(), Request.QueryString["AddType"].ToString(), sessUse.Role);
        if (!Page.IsPostBack)
        {
            dropPhysician.DataSource = myAR.AddRemoveDS.Tables[0];
            dropPhysician.DataTextField = "PersonName";
            dropPhysician.DataValueField = "PersonID";
            dropPhysician.DataBind();
            dropPhysician.Items.Insert(0, "Select Physician");
            dropPhysician.SelectedItem.Value = "0";
        }
    }
    protected void ButtonRequest_Click(object sender, EventArgs e)
    {
        myAR.PhysicianTransferRequest(sessUse.Username, Convert.ToInt32(dropPhysician.SelectedValue), rblstAgreeable.Items[1].Selected);
        Response.Redirect("PatientInfo.aspx?a=phystranreq&choice=" + choice.ToString());
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientInfo.aspx?choice=" + choice.ToString());
    }
}
