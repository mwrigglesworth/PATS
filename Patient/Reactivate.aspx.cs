using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Reactivate : System.Web.UI.Page
{
    GIPAP_Objects.PatientGipapStatus myStatus;
    GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request();
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
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "reactivate");
        try
        {
            myRequest = new GIPAP_Objects.Request(Convert.ToInt32(Request.QueryString["rid"]), Convert.ToInt32(Request.QueryString["choice"]));
        }
        catch { }


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

            if (sessUse.Role == "TMFUser")
            {
                LabelStartReactivate.Text = "Start Date: " + DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y");
                //all patients have 4 months
                LabelEndReactivate.Text = "End Date: " + DateTime.Today.AddDays(119).Day.ToString() + " " + DateTime.Today.AddDays(119).ToString("y");
                CalendarReactivateStart.SelectedDate = DateTime.Today;
                txtReactivationReason.Visible = false;
                RequiredFieldValidator10.Visible = false;
            }
            else
            {
                dropReactivation.Visible = false;
                lbEditReactivateStart.Visible = false;
                CompareValidator8.Visible = false;
            }
            CalendarReactivateStart.Visible = false;

            ////////////
            if (myStatus.Treatment == "Glivec")
            {
                if (myStatus.Diagnosis == "Ph+ ALL" || myStatus.Diagnosis == "MDS / MPD")
                {
                    dropReactivateDose.Items.Add("260mg");
                    dropReactivateDose.Items.Add("300mg");
                    dropReactivateDose.Items.Add("400mg");
                    dropReactivateDose.Items.Add("600mg");
                    PanelReactivateDose.Visible = true;
                    LabelReactivateDosageMessage.Visible = true;
                }
                else if (myStatus.Diagnosis == "GIST" && myStatus.Adjuvant)
                {
                    dropReactivateDose.Items.Add("300mg");
                    dropReactivateDose.Items.Add("400mg");
                    PanelReactivateDose.Visible = true;
                }
                else if (myStatus.Diagnosis == "DFSP")
                {
                    dropReactivateDose.Items.Add("400mg");
                    dropReactivateDose.Items.Add("600mg");
                    dropReactivateDose.Items.Add("800mg");
                    dropReactivateDose.Items.Add("N/A");
                    LabelReactivateDosageMessage.Visible = false;
                }
                else if (myStatus.Diagnosis == "Systemic Mastocytosis" || myStatus.Diagnosis == "HES / CEL")
                {
                    dropReactivateDose.Items.Add("100mg");
                    dropReactivateDose.Items.Add("400mg");
                    LabelReactivateDosageMessage.Visible = false;
                }
                else
                {
                    dropReactivateDose.Items.Add("100mg");
                    dropReactivateDose.Items.Add("200mg");
                    dropReactivateDose.Items.Add("260mg");
                    dropReactivateDose.Items.Add("300mg");
                    dropReactivateDose.Items.Add("400mg");
                    dropReactivateDose.Items.Add("600mg");
                    dropReactivateDose.Items.Add("800mg");
                    dropReactivateDose.Items.Add("N/A");
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
                    dropReactivateDose.AutoPostBack = true;
                }
            }
            else if (myStatus.Treatment == "Tasigna")
            {
                dropReactivateDose.Items.Add("400mg BID");
                dropReactivateDose.Items.Add("400mg QD");
                dropReactivateDose.Items.Add("300mg BID");
                LabelReactivateDosageMessage.Visible = false;
            }
            try
            {
                dropReactivateDose.SelectedValue = myStatus.CurrentDosage;
                dropReactivateDose.SelectedItem.Value = "1";
            }
            catch { }
            if (myStatus.Diagnosis == "CML")
            {
                dropCMLReactivate.SelectedValue = myStatus.CurrentCMLPhase;
            }
            else
            {
                PanelReactivateCML.Visible = false;
            }
            //FINISH BUTTON DISABLER...
            sbDisable.Append(Page.GetPostBackEventReference(ButtonReactivate));
            sbDisable.Append(";");
            //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

            ButtonReactivate.Attributes.Add("onclick", sbDisable.ToString());

            //see if we are processing a request
            if (myRequest.RequestID != 0)
            {
                dropReactivateDose.SelectedValue = myRequest.CurrentDosage;
                //Inida supply
                if (myStatus.CountryID == 76)
                {
                    this.SetTabletStrength(myRequest.CurrentDosage);
                    if (/*PanelTablet.Visible NOT UPLOADING YET, MUST BE INVISIBLE*/myRequest.CurrentDosage == "400mg" || myRequest.CurrentDosage == "600mg" || myRequest.CurrentDosage == "800mg")
                    {
                        try
                        {
                            dropTablet.SelectedValue = myRequest.TabletStrength;
                        }
                        catch { }
                    }
                    dropReactivateDose.AutoPostBack = true;
                }
                /*set to request values*/
                rblstContinue.SelectedIndex = Convert.ToInt32(myRequest.RestartTreatment);
                rblstReactivateFinancial.Items[0].Selected = !myRequest.FinancialStatus;
                txtReactivateNote.Text = myRequest.Notes;
                txtReactivationReason.Visible = true;
                txtReactivationReason.Text = myRequest.ReactivateReason;
                if (myRequest.Diagnosis == "CML")
                {
                    dropCMLReactivate.SelectedValue = myRequest.CurrentCMLPhase;
                }
                else
                {
                    PanelReactivateCML.Visible = false;
                }
                ButtonRemoveRequest.Visible = true;
                if (myStatus.GipapStatus != "Closed")
                {
                    PanelObsolete.Visible = true;
                    ButtonReactivate.Enabled = false;
                }/*
                else if (myStatus.NoaFEFNeeded)
                {
                    ButtonReactivate.Enabled = false;
                }*/
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
    protected void ButtonReactivate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (sessUse.Role == "TMFUser")
            {
                if (myRequest.RequestID != 0)
                {
                    myStatus.RequestID = myRequest.RequestID;
                    myStatus.StatusReason = txtReactivationReason.Text;
                }
                myStatus.StartDate = CalendarReactivateStart.SelectedDate;
                //4 month
                myStatus.EndDate = CalendarReactivateStart.SelectedDate.AddDays(119);
                myStatus.CurrentDosage = dropReactivateDose.SelectedItem.Text;
                myStatus.TabletStrength = dropTablet.SelectedValue;
                myStatus.RestartTreatment = rblstContinue.Items[1].Selected;
                myStatus.FinancialStatus = rblstReactivateFinancial.Items[1].Selected;
                myStatus.CurrentCMLPhase = dropCMLReactivate.SelectedValue;
                myStatus.Notes = txtReactivateNote.Text;
                myStatus.ReActivate(sessUse.Username, dropReactivation.SelectedValue);
                Response.Redirect("PatientEmail.aspx?a=reactivate&mailType=ReactivationEmailPatient&choice=" + myStatus.PatientID.ToString());
            }
            else
            {
                myRequest.PatientID = myStatus.PatientID;
                myRequest.CurrentDosage = dropReactivateDose.SelectedItem.Text;
                myRequest.TabletStrength = dropTablet.SelectedValue;
                myRequest.RestartTreatment = rblstContinue.Items[1].Selected;
                myRequest.FinancialStatus = rblstReactivateFinancial.Items[1].Selected;
                myRequest.CurrentCMLPhase = dropCMLReactivate.SelectedValue;
                myRequest.Notes = txtReactivateNote.Text;
                myRequest.ReactivateReason = txtReactivationReason.Text;
                myRequest.StatusReason = myStatus.StatusReason;
                myRequest.ReActivate(sessUse.Username);
                Response.Redirect("Patientinfo.aspx?a=reactivaterequest&choice=" + myRequest.PatientID.ToString());
            }
        }
    }
    protected void ButtonCancelReactivate_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
    protected void lbEditReactivateStart_Click(object sender, EventArgs e)
    {
        CalendarReactivateStart.Visible = true;
    }
    protected void CalendarReactivateStart_SelectionChanged(object sender, EventArgs e)
    {
        LabelStartReactivate.Text = "Start Date: " + CalendarReactivateStart.SelectedDate.Day.ToString() + " " + CalendarReactivateStart.SelectedDate.ToString("y");
        LabelEndReactivate.Text = "End Date: " + CalendarReactivateStart.SelectedDate.AddDays(119).Day.ToString() + " " + CalendarReactivateStart.SelectedDate.AddDays(119).ToString("y");
        CalendarReactivateStart.Visible = false;
    }
    protected void ButtonRemoveRequest_Click(object sender, EventArgs e)
    {
        myRequest.Resolve(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=removerequest&choice=" + myStatus.PatientID.ToString());
    }
    protected void dropReactivateDose_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SetTabletStrength(dropReactivateDose.SelectedItem.Text);
    }
}
