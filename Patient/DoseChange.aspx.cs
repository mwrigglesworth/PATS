using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Patient_DoseChange : System.Web.UI.Page
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
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "dosagechange");
        try
        {
            myRequest = new GIPAP_Objects.Request(Convert.ToInt32(Request.QueryString["rid"]), Convert.ToInt32(Request.QueryString["choice"]));
        }
        catch { }
        LabelDoseChangeDosageMessage.Text = "*Dosages of 200mg and 260mg are pediatric only";
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

            //FINISH BUTTON DISABLER...
            sbDisable.Append(Page.GetPostBackEventReference(ButtonDosageChange));
            sbDisable.Append(";");
            //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

            ButtonDosageChange.Attributes.Add("onclick", sbDisable.ToString());

            if (sessUse.Role == "Physician")
            {
                PanelDoseReceived.Visible = false;
                if (sessUse.latamlist.Contains(sessUse.CountryID))
                {
                    LabelPhysWarning.Text += "<br /> POR FAVOR: No proveainformación personal de pacientes (talcomo el nombre), dado queestosdetallesseránincluidosen un reporte de eventoadverso para Novartis.";
                }

            }
            /*if (sessUse.Role != "TMFUser")
            {
                LabelDoseChangeDefinition.Visible = true;
            }*/

            for (int i = 0; i < 5; i++)
            {
                dropYear.Items.Add((DateTime.Now.Year - i).ToString());
            }
            //////////
            if (myStatus.Treatment == "Glivec")
            {
                if (myStatus.Diagnosis == "DFSP")
                {
                    dropDosageChange.Items.Add("400mg");
                    dropDosageChange.Items.Add("600mg");
                    dropDosageChange.Items.Add("800mg");
                    dropDosageChange.Items.Add("N/A");
                    LabelDoseChangeDosageMessage.Visible = false;
                }
                else if (myStatus.Diagnosis == "Systemic Mastocytosis" || myStatus.Diagnosis == "HES / CEL")
                {
                    dropDosageChange.Items.Add("100mg");
                    dropDosageChange.Items.Add("400mg");
                    LabelDoseChangeDosageMessage.Visible = false;
                }
                else if (myStatus.Diagnosis == "GIST" && myStatus.Adjuvant)
                {
                    dropDosageChange.Items.Add("300mg");
                    dropDosageChange.Items.Add("400mg");
                    LabelDoseChangeDosageMessage.Visible = false;
                }
                else if (myStatus.Diagnosis == "Ph+ ALL")
                {
                    dropDosageChange.Items.Add("260mg");
                    dropDosageChange.Items.Add("300mg");
                    dropDosageChange.Items.Add("400mg");
                    dropDosageChange.Items.Add("600mg");
                    LabelDoseChangeDosageMessage.Text = "*Dosages of 260mg and 300mg are pediatric only"; 
                    LabelDoseChangeDosageMessage.Visible = true;
                }
                else
                {
                    dropDosageChange.Items.Add("100mg");
                    dropDosageChange.Items.Add("200mg");
                    dropDosageChange.Items.Add("260mg");
                    dropDosageChange.Items.Add("300mg");
                    dropDosageChange.Items.Add("400mg");
                    dropDosageChange.Items.Add("600mg");
                    dropDosageChange.Items.Add("800mg");
                    dropDosageChange.Items.Add("N/A");
                }
            }
            else if (myStatus.Treatment == "Tasigna")
            {
                dropDosageChange.Items.Add("400mg BID");
                dropDosageChange.Items.Add("400mg QD");
                dropDosageChange.Items.Add("300mg BID");
                LabelDoseChangeDosageMessage.Visible = false;
            }

            //see if we are processing a request
            if (myRequest.RequestID != 0)
            {
                dropDosageChange.SelectedValue = myRequest.RequestedDosage;
                txtChangeDoseReason.Text = myRequest.Notes;//changed this to notes to conform with other requests
                cbAERelatedDoseChange.Checked = myRequest.AERelated;
                if (cbAERelatedDoseChange.Checked)
                {
                    PanelDoseChangeReason.Visible = true;
                }
                else
                {
                    PanelDoseChangeReason.Visible = false;
                }
                rblstDosePhysReq.SelectedIndex = Convert.ToInt32(myRequest.PhysicianRequested);
                dropDoseReceived.Items.Add("PATS");
                dropDoseReceived.SelectedValue = myRequest.ReceivedBy;
                ButtonRemoveRequest.Visible = true;
                if (myStatus.GipapStatus != "Active")
                {
                    PanelObsolete.Visible = true;
                    ButtonDosageChange.Enabled = false;
                }
            }
        }
    }
    protected void ButtonDosageChange_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (sessUse.Role == "Physician")
            {
                myStatus.PhysicianRequested = true;
                myStatus.ReceivedBy = "PATS";
            }
            else
            {
                myStatus.PhysicianRequested = Convert.ToBoolean(rblstDosePhysReq.SelectedIndex);
                if (myStatus.PhysicianRequested)
                {
                    myStatus.ReceivedBy = dropDoseReceived.SelectedValue;
                }
                else
                {
                    myStatus.ReceivedBy = "";
                }
            }
            if (sessUse.Role == "TMFUser")
            {
                if (myRequest.RequestID != 0)
                {
                    myStatus.RequestID = myRequest.RequestID;
                }
                //we send it into the method now to calculate supply for india myStatus.CurrentDosage = dropDosageChange.SelectedValue;
                myStatus.ChangeDosageReason = txtChangeDoseReason.Text;
                //ae stuff
                myStatus.AERelated = cbAERelatedDoseChange.Checked;
                int rid = 0;
                if (myStatus.AERelated && (myRequest.RequestID == 0 || !myRequest.AERelated))
                {
                    myRequest.PatientID = myStatus.PatientID;
                    myRequest.PhysicianRequested = myStatus.PhysicianRequested;
                    myRequest.Notes = txtChangeDoseReason.Text;
                    // myRequest.AdverseEvent(sessUse.Username);
                    //  rid = myRequest.RequestID;

                    /**********Sept 2014, New AE Logic***************
                    * No more AE requests*/
                    GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(0, myStatus.PatientID);
                    mySae.Event = "Dose Change requested, from " + myStatus.CurrentDosage + " to " + this.dropDosageChange.SelectedValue.ToString() + ", with the following note:\n";
                    mySae.Event += txtChangeDoseReason.Text;
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
                            this.LabelAEErrorDoseChange.Text = "Date AE Reported to Max was not valid";
                            this.LabelAEErrorDoseChange.Visible = true;
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
                        this.LabelAEErrorDoseChange.Text = "There was a problem sending the email report.  This AE was not logged.";
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
                myStatus.ChangeDosage(sessUse.Username, dropDosageChange.SelectedValue);
                string rd = "PatientEmail.aspx?a=dosechange&mailType=DoseChangeEmailPatient&pcount=0&choice=" + myStatus.PatientID.ToString();
                if (rid != 0)
                {
                    rd += "&rid=" + rid.ToString();
                }
                Response.Redirect(rd);
            }
            else
            {
                //we dont need tablet strength for the request because it is pulled from current gipapdetails
                myRequest.PatientID = myStatus.PatientID;
                myRequest.CurrentDosage = myStatus.CurrentDosage;
                myRequest.RequestedDosage = dropDosageChange.SelectedValue;
                myRequest.AERelated = cbAERelatedDoseChange.Checked;
                myRequest.ChangeDosageReason = txtChangeDoseReason.Text;
                myRequest.PhysicianRequested = myStatus.PhysicianRequested;
                myRequest.ReceivedBy = myStatus.ReceivedBy;
                if (myRequest.AERelated)
                {
                    myRequest.Notes = txtChangeDoseReason.Text;
                   // myRequest.AdverseEvent(sessUse.Username);
                  //  myRequest.AERequestID = myRequest.RequestID;

                    /**********Sept 2014, New AE Logic***************
                         * No more AE requests*/
                    GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(0, myStatus.PatientID);
                    mySae.Event = "Dose Change requested, from " + myStatus.CurrentDosage + " to " + this.dropDosageChange.SelectedValue.ToString() + ", with the following note:\n";
                    mySae.Event += txtChangeDoseReason.Text;
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
                            this.LabelAEErrorDoseChange.Text = "Date AE Reported to Max was not valid";
                            this.LabelAEErrorDoseChange.Visible = true;
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
                        this.LabelAEErrorDoseChange.Text = "There was a problem sending the email report.  This AE was not logged.";
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
                myRequest.ChangeDosage(sessUse.Username);
                Response.Redirect("Patientinfo.aspx?a=dosechangerequest&choice=" + myRequest.PatientID.ToString());
            }
        }
    }
    protected void ButtonCancelDosageChange_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
    protected void cbAERelatedDoseChange_CheckedChanged(object sender, EventArgs e)
    {
        if (cbAERelatedDoseChange.Checked)
        {
            PanelDoseChangeReason.Visible = true;
            RequiredFieldValidator11.Enabled = true;
            if (sessUse.Role == "Physician")
            {
                LabelPOWarningDoseChange.Visible = false;
                LabelPhysWarning.Visible = true;
                PanelAEDateLearned.Visible = false;
                LabelDoseChangeDefinition.Visible = true;

            }
               
            if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
            {
                LabelPOWarningDoseChange.Visible = true;
                LabelPhysWarning.Visible = false;
                PanelAEDateLearned.Visible = true;
                LabelDoseChangeDefinition.Visible = false;
                
            }
        }
        else
        {
            PanelDoseChangeReason.Visible = false;
            RequiredFieldValidator11.Enabled = false;
            LabelPOWarningDoseChange.Visible = false;
            LabelPhysWarning.Visible = false;
            PanelAEDateLearned.Visible = false;
            LabelDoseChangeDefinition.Visible = false;

        }
    }
    protected void ButtonRemoveRequest_Click(object sender, EventArgs e)
    {
        myRequest.Resolve(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=removerequest&choice=" + myStatus.PatientID.ToString());
    }
    
}
