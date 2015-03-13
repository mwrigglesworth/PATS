using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_TransferPatients : System.Web.UI.Page
{
    GIPAP_Objects.PhysicianTransfer myTran = new GIPAP_Objects.PhysicianTransfer();
    GIPAP_Objects.User sessUse;
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myTran = new GIPAP_Objects.PhysicianTransfer(choice, 0);
        LabelFrom.Text = myTran.FromInfo;
        if (!Page.IsPostBack)
        {
            dropPhysician.DataSource = myTran.physDT;
            dropPhysician.DataTextField = "physicianname";
            dropPhysician.DataValueField = "personid";
            dropPhysician.DataBind();
            dropPhysician.Items.Insert(0, "Select a physician");
            dropPhysician.SelectedItem.Value = "0";
        }
    }
    protected void dropPhysician_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropPhysician.SelectedIndex != 0)
        {
            myTran = new GIPAP_Objects.PhysicianTransfer(choice, Convert.ToInt32(dropPhysician.SelectedValue));
            PanelTo.Visible = true;
            LabelTo.Text = myTran.ToInfo;
            LabelDRFrom.Text = myTran.FromName;
            LabelDRTo.Text = myTran.ToName;
            ButtonTransfer.Text = "Yes, transfer all patients of Dr. " + myTran.FromName + " to Dr. " + myTran.ToName;
        }
    }
    protected void ButtonTransfer_Click(object sender, EventArgs e)
    {
        myTran = new GIPAP_Objects.PhysicianTransfer(choice, Convert.ToInt32(dropPhysician.SelectedValue));
        myTran.Transfer(sessUse.Username, rblstPINC.Items[1].Selected);
        Response.Redirect("PhysicianInfo.aspx?a=transfer&choice=" + myTran.ToID.ToString());
    }
    protected void ButtonNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("PhysicianInfo.aspx?a=notransfer&choice=" + myTran.FromID.ToString());
    }
}
