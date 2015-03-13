using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Reapprove : System.Web.UI.Page
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
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "reapprove");

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
            if (sessUse.Role != "TMFUser")
            {
                lbEditStart.Visible = false;
                PanelAdvance.Visible = false;
            }
            if (sessUse.Role == "Physician")
            {
                PanelReceive.Visible = false;
                if (sessUse.latamlist.Contains(sessUse.CountryID))
                {
                    LabelPhysWarning.Text += "<br /> POR FAVOR: No proveainformación personal de pacientes (talcomo el nombre), dado queestosdetallesseránincluidosen un reporte de eventoadverso para Novartis.";
                }

            }

            for (int i = 0; i < 25; i++)
            {
                dropYear.Items.Add((DateTime.Now.Year - i).ToString());
            }
            if (!myStatus.AutoApprove)
            {
                ButtonRemoveRequest.Visible = true;
                if (myStatus.GipapStatus != "Active")
                {
                    PanelObsolete.Visible = true;
                    ButtonReApprove.Enabled = false;
                }
                if (myStatus.StartDate < DateTime.Today)
                {
                    LabelStartDate.Text = DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y");
                    //all patients have 4 months now
                    LabelEndDate.Text = DateTime.Today.AddDays(119).Day.ToString() + " " + DateTime.Today.AddDays(119).ToString("y");
                    CalendarStartDate.SelectedDate = DateTime.Today;
                }
                else
                {
                    LabelStartDate.Text = myStatus.StartDate.Day.ToString() + " " + myStatus.StartDate.ToString("y");
                    LabelEndDate.Text = myStatus.EndDate.Day.ToString() + " " + myStatus.EndDate.ToString("y");
                    CalendarStartDate.SelectedDate = myStatus.StartDate;
                    CalendarStartDate.VisibleDate = myStatus.StartDate;
                }
                //set other values
                rblstRestart.SelectedIndex = Convert.ToInt32(myStatus.RestartTreatment);
                txtNotes.Text = myStatus.Notes;
                PanelAdvance.Visible = false;
                if (myStatus.ReceivedBy == "PATS")
                {
                    dropReceived.Items.Add("PATS");
                    dropReceived.SelectedValue = myStatus.ReceivedBy;
                }
                else
                {
                    rblstPhysReq.SelectedIndex = Convert.ToInt32(myStatus.PhysicianRequested);
                    dropReceived.SelectedValue = myStatus.ReceivedBy;
                }
                cbReapprovalAE.Checked = myStatus.AERelated;
                if (myStatus.AERelated || myStatus.Notes != "")
                {
                    ButtonReapprovalDoseChange.Visible = false;
                    dropDosage.Enabled = true;
                    PanelReapproveDoseChange.Visible = true;
                    txtNotes.Visible = true;
                }
                //see if last order was picked up for india
                if (myStatus.CountryID == 76 && !myStatus.ActiveDetailPickedUp && !myStatus.FlagNoa /*flag noa means partial pay*/ && myStatus.DetailCreateDate > Convert.ToDateTime("11/10/2013"))
                {
                    PanelNotPickedUp.Visible = true;
                }

                //populate the Year for AE learned date
                for (int i = 0; i < 25; i++)
                {
                    dropYear.Items.Add((DateTime.Now.Year - i).ToString());
                }
            }
            else if (myStatus.EndDate < DateTime.Today)
            {
                LabelStartDate.Text = DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y");
                //all patient have 4 months, not just flag noa
                LabelEndDate.Text = DateTime.Today.AddDays(119).Day.ToString() + " " + DateTime.Today.AddDays(119).ToString("y");
                CalendarStartDate.SelectedDate = DateTime.Today;
            }
            else
            {
                LabelStartDate.Text = myStatus.EndDate.AddDays(1).Day.ToString() + " " + myStatus.EndDate.AddDays(1).ToString("y");
                LabelEndDate.Text = myStatus.EndDate.AddDays(120).Day.ToString() + " " + myStatus.EndDate.AddDays(120).ToString("y");
                /*if (myStatus.FlagNoa)
                {
                    LabelEndDate.Text = myStatus.EndDate.AddDays(120).Day.ToString() + " " + myStatus.EndDate.AddDays(120).ToString("y");
                }
                else
                {
                    LabelEndDate.Text = myStatus.EndDate.AddDays(90).Day.ToString() + " " + myStatus.EndDate.AddDays(90).ToString("y");
                }*/
                CalendarStartDate.SelectedDate = myStatus.EndDate.AddDays(1);
                CalendarStartDate.VisibleDate = myStatus.EndDate.AddDays(1);
            }
            if (CalendarStartDate.SelectedDate < (DateTime.Today.AddDays(30)))
            {
                PanelAdvance.Visible = false;
            }
            CalendarStartDate.Visible = false;

            if (myStatus.PatientConsent)
            {
                rbConsent.Items[1].Selected = true;
                PanelConsent.Visible = false;
            }
            else
            {
                rbConsent.Items[0].Selected = true;
            }
            //india supply
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
            }
            ///////
            if (myStatus.Treatment == "Glivec")
            {
                if (myStatus.Diagnosis == "Ph+ ALL")
                {
                    dropDosage.Items.Add("260mg");
                    dropDosage.Items.Add("300mg");
                    dropDosage.Items.Add("400mg");
                    dropDosage.Items.Add("600mg");
                    PanelReapproveDose.Visible = true;
                }
                else if (myStatus.Diagnosis == "MDS / MPD")
                {
                    dropDosage.Items.Add("400mg");
                    PanelReapproveDose.Visible = false;
                }
                else if (myStatus.Diagnosis == "GIST" && myStatus.Adjuvant)
                {
                    dropDosage.Items.Add("300mg");
                    dropDosage.Items.Add("400mg");
                    PanelReapproveDose.Visible = true;
                }
                else if (myStatus.Diagnosis == "DFSP")
                {
                    dropDosage.Items.Add("400mg");
                    dropDosage.Items.Add("600mg");
                    dropDosage.Items.Add("800mg");
                    dropDosage.Items.Add("N/A");
                    LabelReApprovalDosageMessage.Visible = false;
                }
                else if (myStatus.Diagnosis == "Systemic Mastocytosis" || myStatus.Diagnosis == "HES / CEL")
                {
                    dropDosage.Items.Add("100mg");
                    dropDosage.Items.Add("400mg");
                    LabelReApprovalDosageMessage.Visible = false;
                }
                else
                {
                    dropDosage.Items.Add("100mg");
                    dropDosage.Items.Add("200mg");
                    dropDosage.Items.Add("260mg");
                    dropDosage.Items.Add("300mg");
                    dropDosage.Items.Add("400mg");
                    dropDosage.Items.Add("600mg");
                    dropDosage.Items.Add("800mg");
                    dropDosage.Items.Add("N/A");
                }
            }
            else if (myStatus.Treatment == "Tasigna")
            {
                dropDosage.Items.Add("400mg BID");
                dropDosage.Items.Add("400mg QD");
                dropDosage.Items.Add("300mg BID");
                LabelReApprovalDosageMessage.Visible = false;
            }
            try
            {
                dropDosage.SelectedValue = myStatus.CurrentDosage;
                dropDosage.SelectedItem.Value = "1";
            }
            catch { }
            //FINISH BUTTON DISABLER...
            sbDisable.Append(Page.GetPostBackEventReference(ButtonReApprove));
            sbDisable.Append(";");
            //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

            ButtonReApprove.Attributes.Add("onclick", sbDisable.ToString());
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
        if (sessUse.ClickCount == 0  && Page.IsValid)
        {
            sessUse.ClickCount = 1;
            myStatus.StartDate = CalendarStartDate.SelectedDate;
            //4 months
            myStatus.EndDate = CalendarStartDate.SelectedDate.AddDays(119);
            myStatus.RestartTreatment = rblstRestart.Items[1].Selected;
            myStatus.Notes = txtNotes.Text;

            if (sessUse.Role == "Physician")
            {
                myStatus.PhysicianRequested = true;
                myStatus.ReceivedBy = "PATS";
            }
            else
            {
                myStatus.PhysicianRequested = Convert.ToBoolean(rblstPhysReq.SelectedIndex);
                if (myStatus.PhysicianRequested)
                {
                    myStatus.ReceivedBy = dropReceived.SelectedValue;
                }
                else
                {
                    myStatus.ReceivedBy = "";
                }
            }
            if (myStatus.CurrentDosage != dropDosage.SelectedItem.Text && cbReapprovalAE.Checked && txtNotes.Text.Trim() == "")
            {
                sessUse.ClickCount = 0;
                LabelreappDoseChange.Visible = true;
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Please enter a reason for the dosage change');", true); 
                return;
            }
            //ae stuff
            bool logAEonReappRequest = false;
            if (cbReapprovalAE.Checked && !myStatus.AERelated && !myStatus.AutoApprove) //if the request did not have an AE associated, but the PO decides there is one when processing
            {
                logAEonReappRequest = true;
            }
            myStatus.AERelated = cbReapprovalAE.Checked;
            int rid = 0;
            if (myStatus.AERelated && (myStatus.AutoApprove || logAEonReappRequest))
            {
                myRequest.PatientID = myStatus.PatientID;
                myRequest.PhysicianRequested = myStatus.PhysicianRequested;
                myRequest.Notes = myStatus.Notes;
               // myRequest.AdverseEvent(sessUse.Username);
               // rid = myRequest.RequestID;

                /**********Sept 2014, New AE Logic***************
                        * No more AE requests*/
                GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(0, myStatus.PatientID);
                mySae.Event = "Reapproval with Dose Change requested, from " + myStatus.CurrentDosage + " to " + this.dropDosage.SelectedValue.ToString() + ", with the following note:\n";
                mySae.Event += myRequest.Notes;
                mySae.LearnedFromPhysician = myRequest.PhysicianRequested;
                
                if (sessUse.Role == "Physician")
                    mySae.DateLearned = DateTime.Now;
                else
                {
                    try
                    {
                        mySae.DateLearned = Convert.ToDateTime(dropMonth.SelectedValue + "/" + dropDay.SelectedValue + "/" + dropYear.SelectedValue);
                    }
                    catch
                    {
                        this.LabelPOWarningReapproval.Text = "Date AE Reported to Max was not valid";
                        this.LabelPOWarningReapproval.Visible = true;
                        return;
                    }
                }
                try
                {
                    GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                    myEmail = mySae.SAEAlertNovartis();
                    myEmail.Send(sessUse.Username);
                    mySae.EmailID = myEmail.EmailID;
                }
                catch
                {
                    this.LabelPOWarningReapproval.Text = "There was a problem sending the email report.  This AE was not logged.";
                    return;
                }
                try
                {
                    GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                    myEmail = mySae.SAEAlertPhysician();
                    myEmail.Send(sessUse.Username);
                    mySae.PhyEmailID = myEmail.EmailID;
                }
                catch { }
                mySae.Create(sessUse.Username);
                myRequest.AERequestID = mySae.SAEID;
            }
            /*check auto approve constraints BEFORE setting current dosage*/
            if (sessUse.Role == "MaxStation")
            {
                myStatus.AutoApprove = (myStatus.EnableAutoApprove && myStatus.RestartTreatment && !myStatus.NoaFEFNeeded && (txtNotes.Text == "") && (myStatus.CurrentDosage == dropDosage.SelectedItem.Text) && (dropReceived.SelectedValue != "0"));
                if (myStatus.AutoApprove && myStatus.CountryID == 76 && !myStatus.FlagNoa && myStatus.DetailCreateDate > Convert.ToDateTime("11/10/2013"))
                {
                    myStatus.AutoApprove = myStatus.ActiveDetailPickedUp;
                }
            }
            else if (sessUse.Role == "TMFUser")
            {
                if (myStatus.StartDate > (DateTime.Today.AddDays(29)) && rblstAdvance.SelectedIndex == 1)
                {
                    myStatus.AutoApprove = false;
                }
                else if (myStatus.NoaFEFNeeded)
                {
                    myStatus.AutoApprove = false;
                }
                else
                {
                    myStatus.AutoApprove = true;
                }
            }
            else
            {
                myStatus.AutoApprove = (myStatus.EnableAutoApprove && myStatus.RestartTreatment && !myStatus.NoaFEFNeeded && (txtNotes.Text == "") && (myStatus.CurrentDosage == dropDosage.SelectedItem.Text));
                if (myStatus.AutoApprove && myStatus.CountryID == 76 && !myStatus.FlagNoa && myStatus.DetailCreateDate > Convert.ToDateTime("11/10/2013"))
                {
                    myStatus.AutoApprove = myStatus.ActiveDetailPickedUp;
                }
            }

            myStatus.CurrentDosage = dropDosage.SelectedItem.Text;
            myStatus.TabletStrength = dropTablet.SelectedValue;
            myStatus.NotPickedUpContacted = rblstNotPickedUp.Items[0].Selected;

            myStatus.PatientConsent = rbConsent.Items[1].Selected;
            /************************/
            myStatus.ReApprove(sessUse.Username);
            sessUse.ClickCount = 0;
            if (sessUse.Role == "TMFUser")
            {
                string rd;
                if (myStatus.AutoApprove)
                {
                    rd = "PatientEmail.aspx?mailType=ReApprovalEmailPatient&a=reapprove&choice=" + myStatus.PatientID.ToString();
                    if (rid != 0)
                    {
                        rd += "&rid=" + rid.ToString();
                    }
                    Response.Redirect(rd);
                }
                else
                {
                    if (rid == 0)
                    {
                        Response.Redirect("Patientinfo.aspx?a=reapproverequest&choice=" + myStatus.PatientID.ToString());
                    }
                    else
                    {
                        Response.Redirect("SAE.aspx?choice=" + myStatus.PatientID.ToString() + "&rid=" + rid.ToString());
                    }
                }
            }
            else/* if (sessUse.Role == "MaxStation")*/
            {
                Response.Redirect("Patientinfo.aspx?a=reapproverequest&choice=" + myStatus.PatientID.ToString());
            }/*
            else
            {
                Response.Redirect(sessUse.HomePage);
            }*/
        }
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
    protected void lbEditStart_Click(object sender, EventArgs e)
    {
        CalendarStartDate.Visible = true;
    }
    protected void CalendarStartDate_SelectionChanged(object sender, EventArgs e)
    {
        LabelStartDate.Text = CalendarStartDate.SelectedDate.Day.ToString() + " " + CalendarStartDate.SelectedDate.ToString("y");
        //4 mon
        LabelEndDate.Text = CalendarStartDate.SelectedDate.AddDays(119).Day.ToString() + " " + CalendarStartDate.SelectedDate.AddDays(119).ToString("y");
    }

    protected void ButtonReapprovalDoseChange_Click(object sender, EventArgs e)
    {
        dropDosage.Enabled = true;
        PanelReapproveDoseChange.Visible = true;
        
        ButtonReapprovalDoseChange.Visible = false;
       
        if (myStatus.CountryID == 76)
        {
            dropDosage.AutoPostBack = true;
        }
    }

    protected void cbReapprovalAE_CheckedChanged(object sender, EventArgs e)
    {
        if (cbReapprovalAE.Checked)
        {
            LabelreappDoseChange.Visible = true;
            txtNotes.Visible = true;
            RequiredFieldValidator11.Enabled = true;
            LabelPOWarningReapproval.Visible = true;
            if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
            {
                LabelPOWarningReapproval.Visible = true;
                PanelAEDateLearned.Visible = true;
                LabelReapprovalDefinition.Visible = false;
                LabelPhysWarning.Visible = false;
            }

            if (sessUse.Role == "Physician")
            {
                PanelAEDateLearned.Visible = false;
                LabelPOWarningReapproval.Visible = false;
                LabelPhysWarning.Visible = true;
                LabelReapprovalDefinition.Visible = true;
            }
        }
        else
        {
            LabelreappDoseChange.Visible = false;
            LabelPOWarningReapproval.Visible = false;
            RequiredFieldValidator11.Enabled = false;
            txtNotes.Visible = false;
            PanelAEDateLearned.Visible = false;
            txtNotes.Text = "";
            LabelReapprovalDefinition.Visible = false;
            LabelPhysWarning.Visible = false;
        }
    }
    protected void ButtonRemoveRequest_Click(object sender, EventArgs e)
    {
        myStatus.IgnoreReapprovalRequest(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=removerequest&choice=" + myStatus.PatientID.ToString());
    }
    protected void dropDosage_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SetTabletStrength(dropDosage.SelectedItem.Text);
    }
}
