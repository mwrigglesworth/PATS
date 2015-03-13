using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_UpdateTabletStrength : System.Web.UI.Page
{
    GIPAP_Objects.PatientGipapStatus myStatus;
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessUse = (GIPAP_Objects.User)Session["sessUse"];
        }
        catch
        {
            Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
        }
        if (sessUse.Role != "TMFUser" && sessUse.Role != "MaxStation" && sessUse.Role != "Physician")
        {
            Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
        }
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "reapprove");
        if (!Page.IsPostBack)
        {
            this.SetTabletStrength(myStatus.CurrentDosage);
            if (/*PanelTablet.Visible NOT UPLOADING YET, MUST BE INVISIBLE*/myStatus.CurrentDosage == "400mg" || myStatus.CurrentDosage == "600mg" || myStatus.CurrentDosage == "800mg")
            {
                try
                {
                    dropTablet.SelectedValue = myStatus.TabletStrength;
                }
                catch { }
            }
        }
    }
    private void SetTabletStrength(string dose)
    {
        if (dose == "400mg" || dose == "600mg" || dose == "800mg")
        {
            PanelTablet.Visible = true;
            dropTablet.Items.Clear();
            if (dose == "400mg")
            {
                dropTablet.Items.Add("1 x 400mg");
                dropTablet.Items.Add("4 x 100mg");
            }
            else if (dose == "600mg")
            {
                dropTablet.Items.Add("1 x 400mg + 2 x 100mg");
                dropTablet.Items.Add("6 x 100mg");
            }
            else if (dose == "800mg")
            {
                dropTablet.Items.Add("2 x 400mg");
                dropTablet.Items.Add("8 x 100mg");
            }
        }
        else
        {
            PanelTablet.Visible = false;
            dropTablet.Items.Clear();
        }
    }
    protected void ButtonReApprove_Click(object sender, EventArgs e)
    {
        myStatus.TabletStrength = dropTablet.SelectedValue;
        myStatus.UpdateTabletStrength(sessUse.Username);
        Response.Redirect("Patientinfo.aspx?a=tabletstrength&choice=" + myStatus.PatientID.ToString());
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
}