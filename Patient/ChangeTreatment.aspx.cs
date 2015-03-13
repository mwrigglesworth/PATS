using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_ChangeTreatment : System.Web.UI.Page
{
    string action;
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
        action = Request.QueryString["action"];
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "ChangeTreatment");
        if (!Page.IsPostBack)
        {
            //ae stuff
            if (sessUse.Role == "Physician")
            {
                PanelReceive.Visible = false;
                if (sessUse.latamlist.Contains(sessUse.CountryID))
                {
                    LabelPhysWarning.Text += "<br /> POR FAVOR: No proveainformación personal de pacientes (talcomo el nombre), dado queestosdetallesseránincluidosen un reporte de eventoadverso para Novartis.";
                }

            }
            if (sessUse.Role != "TMFUser")
            {
                LabelDefinition.Visible = true;
            }
            /***/
            for (int i = 0; i < 25; i++)
            {
                dropYear.Items.Add((DateTime.Now.Year - i).ToString());
            }

            for (int i = 0; i < 20; i++)
            {
                dropTasignaStartYear.Items.Add((DateTime.Now.Year - i).ToString());
            }


            //SET UP BUTTON DISABLER....
            System.Text.StringBuilder sbDisable = new System.Text.StringBuilder();
            sbDisable.Append("if (typeof(Page_ClientValidate) == 'function') {");
            sbDisable.Append("if (Page_ClientValidate() == false) {");
            sbDisable.Append("return false;");
            sbDisable.Append("}");
            sbDisable.Append("}");
            sbDisable.Append("this.value = 'Please wait...';");
            sbDisable.Append("this.disabled = true;");
            sbDisable.Append(Page.GetPostBackEventReference(ButtonSubmit));
            sbDisable.Append(";");

            ButtonSubmit.Attributes.Add("onclick", sbDisable.ToString());
        }
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            myRequest.PatientID = myStatus.PatientID;
            myRequest.Imatinib = rblstTasignaImatinib.Items[1].Selected;
            myRequest.GlivecIntolerant = rblstGlivecIntolerant.Items[1].Selected;
            myRequest.GlivecResistant = rblstGlivecResistant.Items[1].Selected;
            myRequest.Dasatinib = rblstDasatinib.Items[1].Selected;
            myRequest.DasatinibIntolerant = rblstDasatinibIntolerant.Items[1].Selected;
            myRequest.DasatinibResistant = rblstDasatinibResistant.Items[1].Selected;
            myRequest.Tasigna = rblstTasigna.Items[1].Selected;
            myRequest.NOATasigna = rblstNOATasigna.Items[1].Selected;
            myRequest.Notes = txtNotes.Text;
            myRequest.TasignaPatientConsent = rblstTasignaPatientConsent.Items[1].Selected;
            if (myRequest.Tasigna)
            {
                myRequest.TasignaStartDate = Convert.ToDateTime(dropTasignaStartMonth.SelectedValue + "/" + dropTasignaStartDay.SelectedValue + "/" + dropTasignaStartYear.SelectedValue);
            }
            myRequest.PrevTasignaDose = dropTasignaDose.SelectedValue;
            //ae stuff
            if (sessUse.Role == "Physician")
            {
                myRequest.PhysicianRequested = true;
                myRequest.ReceivedBy = "PATS";
            }
            else
            {
                myRequest.PhysicianRequested = Convert.ToBoolean(rblstPhysReq.SelectedIndex);
                if (myRequest.PhysicianRequested)
                {
                    myRequest.ReceivedBy = dropReceived.SelectedValue;
                }
                else
                {
                    myRequest.ReceivedBy = "";
                }
            }
            myRequest.AERelated = ((myStatus.GipapStatus == "Active"  || myStatus.GipapStatus == "Closed") && (rblstGlivecIntolerant.Items[1].Selected || rblstGlivecResistant.Items[1].Selected));
            //ae request
            int rid = 0;
            if (myRequest.AERelated)
            {
                //myRequest.AdverseEvent(sessUse.Username);
                //myRequest.AERequestID = myRequest.RequestID;
                //rid = myRequest.RequestID;

                /**********Sept 2014, New AE Logic***************
                        * No more AE requests*/
                GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(0, myStatus.PatientID);

                //Event information in detail
                mySae.Event = "Treatment Change requested, reason given ";
                if (rblstGlivecIntolerant.Items[1].Selected)
                    mySae.Event += "\"Intolerance\"";
                if (rblstGlivecResistant.Items[1].Selected)
                {
                    if (rblstGlivecIntolerant.Items[1].Selected)
                        mySae.Event += " and ";
                    mySae.Event += "\"Resistance\"";
                }
                mySae.Event += ", with the following note:\n" + this.txtNotes.Text;
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
                        this.LabelPOWarning.Text = "Date AE Reported to Max was not valid";
                        this.LabelPOWarning.Visible = true;
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
                    this.LabelPOWarning.Text = "There was a problem sending the email report.  This AE was not logged.";
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

            //do the same request for all
            myRequest.CurrentDosage = dropRequestedTasignaDose.SelectedValue;
            myRequest.CurrentCMLPhase = dropCMLphase.SelectedValue;
            myRequest.ChangeTreatment(sessUse.Username);
            if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
            {  
                Response.Redirect("NOAFEF.aspx?choice=" + myRequest.PatientID.ToString());
            }
            else
            {
                Response.Redirect("patientinfo.aspx?a=tcrequest&choice=" + myRequest.PatientID.ToString());
            }
            /*if (myStatus.GipapStatus == "Active")
            {
                myRequest.PatientID = myStatus.PatientID;
                myRequest.CurrentDosage = dropRequestedTasignaDose.SelectedValue;
                myRequest.CurrentCMLPhase = dropCMLphase.SelectedValue;
                myRequest.ChangeTreatment(sessUse.Username);
                Response.Redirect("GIPAP.aspx?trgt=requestconfirmation&choice=" + myRequest.RequestID.ToString() + "&patid=" + myRequest.PatientID.ToString());
            }
            else if (myStatus.GipapStatus == "Closed")
            {
                myRequest.PatientID = myStatus.PatientID;
                myRequest.CurrentDosage = dropRequestedTasignaDose.SelectedValue;
                myRequest.RestartTreatment = true;
                myRequest.FinancialStatus = true;
                myRequest.CurrentCMLPhase = dropCMLphase.SelectedValue;
                myRequest.Notes = "";
                myRequest.ReactivateReason = txtReason.Text;
                myRequest.StatusReason = myStatus.StatusReason;
                myRequest.ReActivateWithTasigna(sessUse.Username);
                Response.Redirect("GIPAP.aspx?trgt=requestconfirmation&choice=" + myRequest.RequestID.ToString() + "&patid=" + myRequest.PatientID.ToString());
            }
            else if (myStatus.GipapStatus == "Denied")
            {
                myRequest.PatientID = myStatus.PatientID;
                myRequest.ReAssessWithTasigna(sessUse.Username, txtReason.Text);
                Response.Redirect("GIPAP.aspx?trgt=requestconfirmation&choice=" + myRequest.RequestID.ToString() + "&patid=" + myRequest.PatientID.ToString());
            }*/
        }
    }
    protected void rblstGlivecIntolerant_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((myStatus.GipapStatus == "Active" || myStatus.GipapStatus == "Closed")&&  (rblstGlivecResistant.Items[1].Selected || rblstGlivecIntolerant.Items[1].Selected))
        {
            PanelAE.Visible = true;
            RequiredFieldValidator2.Enabled = true;
            if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
            {
                LabelPOWarning.Visible = true;
                PanelAEDateLearned.Visible = true;
                LabelPhysWarning.Visible = false;
                LabelDefinition.Visible = false;
            }
            if (sessUse.Role == "Physician")
            {
                PanelAEDateLearned.Visible = false;
                LabelPhysWarning.Visible = true;
                LabelPOWarning.Visible = false;
                LabelDefinition.Visible = true;
            }
        }
        else
        {
            PanelAE.Visible = false;
            RequiredFieldValidator2.Enabled = false;
            txtNotes.Text = "";
            PanelAEDateLearned.Visible = false;
            LabelPhysWarning.Visible = false;
        }
    }
    protected void rblstGlivecResistant_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((myStatus.GipapStatus == "Active" || myStatus.GipapStatus == "Closed") && (rblstGlivecResistant.Items[1].Selected || rblstGlivecIntolerant.Items[1].Selected))
        {
            PanelAE.Visible = true;
            RequiredFieldValidator2.Enabled = true;
            if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
            {
                LabelPOWarning.Visible = true;
                PanelAEDateLearned.Visible = true;
                LabelPhysWarning.Visible = false;
                LabelDefinition.Visible = false;
            }
            if (sessUse.Role == "Physician")
            {
                PanelAEDateLearned.Visible = false;
                LabelPhysWarning.Visible = true;
                LabelPOWarning.Visible = false;
                LabelDefinition.Visible = true;
            }
        }
        else
        {
            PanelAE.Visible = false;
            RequiredFieldValidator2.Enabled = false;
            txtNotes.Text = "";
            PanelAEDateLearned.Visible = false;
            LabelPhysWarning.Visible = false;
        }
    }
}
