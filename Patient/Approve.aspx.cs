using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Approve : System.Web.UI.Page
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
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "approve");

        if (!Page.IsPostBack)
        {
            //SET UP BUTTON DISABLER....
            System.Text.StringBuilder sbDisable = new System.Text.StringBuilder();
            sbDisable.Append("if (typeof(Page_ClientValidate) == 'function') {");
            sbDisable.Append("if (Page_ClientValidate() == false) {");
            sbDisable.Append("return false;");
            sbDisable.Append("}");
            sbDisable.Append("}");
            sbDisable.Append("this.value = 'Please wait...';");
            sbDisable.Append("this.disabled = true;");

            LabelApproveStartDate.Text = DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y");
            //all patients have 4 monts now
            LabelApproveEndDate.Text = DateTime.Today.AddDays(119).Day.ToString() + " " + DateTime.Today.AddDays(119).ToString("y");

            if (myStatus.Treatment == "Glivec")
            {
                if (myStatus.Diagnosis == "Ph+ ALL")
                {
                    DropApproveDose.Items.Add("260mg");
                    DropApproveDose.Items.Add("300mg");
                    DropApproveDose.Items.Add("400mg");
                    DropApproveDose.Items.Add("600mg");
                    //DropApproveDose.SelectedValue = "600mg";
                    PanelApproveDose.Visible = true;
                }
                else if (myStatus.Diagnosis == "MDS / MPD")
                {
                    DropApproveDose.Items.Add("400mg");
                    PanelApproveDose.Visible = false;
                }
                else if (myStatus.Diagnosis == "GIST" && myStatus.Adjuvant)
                {
                    DropApproveDose.Items.Add("300mg");
                    DropApproveDose.Items.Add("400mg");
                    PanelApproveDose.Visible = true;
                }
                else if (myStatus.Diagnosis == "DFSP")
                {
                    DropApproveDose.Items.Add("400mg");
                    DropApproveDose.Items.Add("600mg");
                    DropApproveDose.Items.Add("800mg");
                    DropApproveDose.Items.Add("N/A");
                    LabelApproveDosageMessage.Visible = false;
                }
                else if (myStatus.Diagnosis == "Systemic Mastocytosis" || myStatus.Diagnosis == "HES / CEL")
                {
                    DropApproveDose.Items.Add("100mg");
                    DropApproveDose.Items.Add("400mg");
                    LabelApproveDosageMessage.Visible = false;
                }
                else
                {
                    DropApproveDose.Items.Add("100mg");
                    DropApproveDose.Items.Add("200mg");
                    DropApproveDose.Items.Add("260mg");
                    DropApproveDose.Items.Add("300mg");
                    DropApproveDose.Items.Add("400mg");
                    DropApproveDose.Items.Add("600mg");
                    DropApproveDose.Items.Add("800mg");
                    DropApproveDose.Items.Add("N/A");
                }
            }
            else if (myStatus.Treatment == "Tasigna")
            {
                DropApproveDose.Items.Add("400mg BID");
                DropApproveDose.Items.Add("400mg QD");
                DropApproveDose.Items.Add("300mg BID");
                LabelApproveDosageMessage.Visible = false;
            }
            //Inida supply
            if (myStatus.CountryID == 76)
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
                DropApproveDose.AutoPostBack = true;
            }
            //patient
            try
            {
                DropApproveDose.SelectedValue = myStatus.CurrentDosage;
                //DropApproveDose.SelectedItem.Value = "1";
            }
            catch { }

            //FINISH BUTTON DISABLER...
            sbDisable.Append(Page.GetPostBackEventReference(ButtonApprove));
            sbDisable.Append(";");
            //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

            ButtonApprove.Attributes.Add("onclick", sbDisable.ToString());
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


    protected void ButtonApprove_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            myStatus.StatusReason = dropApproveStatusReason.SelectedValue;
            myStatus.CurrentDosage = DropApproveDose.SelectedValue;
            myStatus.TabletStrength = dropTablet.SelectedValue;
            myStatus.Notes = txtApproveNotes.Text;
            myStatus.Approve(sessUse.Username);
            Response.Redirect("PatientEmail.aspx?a=approve&mailType=ApprovalEmailPatient&choice=" + myStatus.PatientID.ToString());
        }
    }


    protected void ButtonCancelApprove_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }


    protected void DropApproveDose_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SetTabletStrength(DropApproveDose.SelectedItem.Text);
    }
}
