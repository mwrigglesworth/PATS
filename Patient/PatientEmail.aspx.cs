using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_PatientEmail : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
    int choice;
    string mailType;
    string rDirect;
    int pcount;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessUse = (GIPAP_Objects.User)Session["sessUse"];

            if (sessUse.Role == "TMFUser")
            {
                mailType = Request.QueryString["mailType"].ToString();
                choice = Convert.ToInt32(Request.QueryString["choice"]);
                //SET UP send BUTTON DISABLER....
                System.Text.StringBuilder sbDisable = new System.Text.StringBuilder();
                sbDisable.Append("if (typeof(Page_ClientValidate) == 'function') {");
                sbDisable.Append("if (Page_ClientValidate() == false) {");
                sbDisable.Append("return false;");
                sbDisable.Append("}");
                sbDisable.Append("}");
                sbDisable.Append("this.value = 'Please wait...';");
                sbDisable.Append("this.disabled = true;");
                sbDisable.Append(Page.GetPostBackEventReference(ButtonSend));
                sbDisable.Append(";");
                //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

                ButtonSend.Attributes.Add("onclick", sbDisable.ToString());
                //SET UP send all BUTTON DISABLER....
                sbDisable = new System.Text.StringBuilder();
                sbDisable.Append("if (typeof(Page_ClientValidate) == 'function') {");
                sbDisable.Append("if (Page_ClientValidate() == false) {");
                sbDisable.Append("return false;");
                sbDisable.Append("}");
                sbDisable.Append("}");
                sbDisable.Append("this.value = 'Please wait...';");
                sbDisable.Append("this.disabled = true;");
                sbDisable.Append(Page.GetPostBackEventReference(ButtonSendAll));
                sbDisable.Append(";");
                //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

                ButtonSendAll.Attributes.Add("onclick", sbDisable.ToString());
            }
            else if (sessUse.Role == "MaxStation")
            {
                mailType = Request.QueryString["mailType"].ToString();
                choice = Convert.ToInt32(Request.QueryString["choice"]);
                if (mailType != "Noa10DaySupply" && mailType != "NOABranchAssignment")
                {
                    ButtonSend.Visible = false;
                    ButtonCancel.Visible = false;
                }
                ButtonSendAll.Visible = false;
            }
            else
            {
                Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
            }
        }
        catch
        {
            Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
        }
        //this is for ae requests
        string rid = "";
        try
        {
            rid = Request.QueryString["rid"].ToString();
        }
        catch { }

        if (mailType == "ApprovalEmailPatient")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.ApprovalEmailPatient(choice);
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
                txtTo.Text = myEmail.To;
                txtCC.Text = myEmail.CC;
                txtBCC.Text = myEmail.BCC;
                txtSubject.Text = myEmail.Subject;
                txtMessage.Text = myEmail.Message; }
            }
            LabelHeader.Text = "Patient Approval Email To Patient";

            rDirect = "PatientEmail.aspx?mailType=ApprovalEmailPhysician&pcount=0&choice=" + myPatient.PatientID.ToString();
        }
        else if (mailType == "ApprovalEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.ApprovalEmailPhysician(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Approval Email To Physician";
            pcount++;
            if (pcount == myPatient.PhysicianCount || myPatient.PhysicianCount == 0)
            {
                rDirect = "PatientEmail.aspx?mailType=ApprovalEmailCPO&pcount=0&choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=ApprovalEmailPhysician&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
        }
        else if (mailType == "ApprovalEmailCPO")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.ApprovalEmailCPO(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Approval Email To CPO";
            pcount++;
            if (pcount == myPatient.CPOCount || myPatient.CPOCount == 0)
            {
                rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=ApprovalEmailCPO&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
        }
        else if (mailType == "ReApprovalEmailPatient")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.ReApprovalEmailPatient(choice);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient ReApproval Email To Patient";

            rDirect = "PatientEmail.aspx?mailType=ReApprovalEmailPhysician&pcount=0&choice=" + myPatient.PatientID.ToString();
            if (rid != "")
            {
                rDirect += "&rid=" + rid;
            }
        }
        else if (mailType == "ReApprovalEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.ReApprovalEmailPhysician(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient ReApproval Email To Physician";
            pcount++;
            if (pcount == myPatient.PhysicianCount || myPatient.PhysicianCount == 0)
            {
                rDirect = "PatientEmail.aspx?mailType=ReApprovalEmailCPO&pcount=0&choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=ReApprovalEmailPhysician&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
            if (rid != "")
            {
                rDirect += "&rid=" + rid;
            }
        }
        else if (mailType == "ReApprovalEmailCPO")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.ReApprovalEmailCPO(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient ReApproval Email To CPO";
            pcount++;
            if (pcount == myPatient.CPOCount || myPatient.CPOCount == 0)
            {
                if (rid == "")
                {
                    rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
                }
                else
                {
                    rDirect = "SAE.aspx?choice=" + myPatient.PatientID.ToString() + "&rid=" + rid;
                }
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=ReApprovalEmailCPO&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
                if (rid != "")
                {
                    rDirect += "&rid=" + rid;
                }
            }
        }
        else if (mailType == "ClinicApprovalEmailClinic")
        {
            GIPAP_Objects.Clinic myClinic = new GIPAP_Objects.Clinic(choice, sessUse.Role);
            myEmail = myClinic.createApprovalEmail();
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Clinic Approval Email";
            choice = 0;

            rDirect = "PatientEmail.aspx?mailType=ClinicApprovalEmailCPO&choice=" + myClinic.ClinicID.ToString();
        }
        else if (mailType == "ClinicApprovalEmailCPO")
        {
            GIPAP_Objects.Clinic myClinic = new GIPAP_Objects.Clinic(choice, sessUse.Role);
            myEmail = myClinic.createApprovalEmailtoCPO();
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Clinic Approval Email";
            choice = 0;

            rDirect = "GIPAP.aspx?trgt=clinicinfo&choice=" + myClinic.ClinicID.ToString();
        }
        else if (mailType == "PhysicianApprovalEmailPhysician")
        {
            GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician(choice, sessUse.Role);
            myEmail = myPhysician.createApprovalEmail();
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Physician Approval Email To Physician";
            choice = 0;

            rDirect = "PatientEmail.aspx?mailType=PhysicianApprovalEmailCPO&choice=" + myPhysician.PhysicianID.ToString();
        }
        else if (mailType == "PhysicianApprovalEmailCPO")
        {
            GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician(choice, sessUse.Role);
            myEmail = myPhysician.createApprovalEmailtoCPO();
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Physician Approval Email To CPO";
            choice = 0;

            rDirect = "GIPAP.aspx?trgt=physicianinfo&choice=" + myPhysician.PhysicianID.ToString();
        }
        else if (mailType == "CloseEmailPatient")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.CloseEmailPatient(choice);
            if (myPatient.StatusReason == "Clinical Reason")
            {
                ButtonSendAll.Enabled = false;
            }
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Closing Email To Patient";
            pcount++;
            rDirect = "PatientEmail.aspx?mailType=CloseEmailPhysician&pcount=0&choice=" + myPatient.PatientID.ToString();
            if (rid != "")
            {
                rDirect += "&rid=" + rid;
            }
        }
        else if (mailType == "CloseEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.CloseEmailPhysician(choice, pcount);
            if (myPatient.StatusReason == "Clinical Reason")
            {
                ButtonSendAll.Enabled = false;
            }
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Closing Email To Physician";
            pcount++;
            if (pcount == myPatient.PhysicianCount || myPatient.PhysicianCount == 0)
            {
                rDirect = "PatientEmail.aspx?mailType=CloseEmail&choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=CloseEmailPhysician&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
            if (rid != "")
            {
                rDirect += "&rid=" + rid;
            }
        }
        else if (mailType == "CloseEmail")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.CloseEmail(choice);
            if (myPatient.StatusReason == "Clinical Reason")
            {
                ButtonSendAll.Enabled = false;
            }
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Closing Email To CPO";

            if (rid == "")
            {
                rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "SAE.aspx?choice=" + myPatient.PatientID.ToString() + "&rid=" + rid;
            }
        }
        else if (mailType == "AEAlert")
        {
            ButtonSendAll.Enabled = false;
            GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(choice, Convert.ToInt32(Request.QueryString["patid"]));
            //myEmail = mySae.SAEAlert();
            if (!Page.IsPostBack){ 
            LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Adverse Event Email";
            choice = mySae.PatientID;

            if (Request.QueryString["close"].ToString() == "yes")
            {
                rDirect = "PatientEmail.aspx?mailType=CloseEmailPatient&choice=" + mySae.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientInfo.aspx?choice=" + mySae.PatientID.ToString();
            }
        }
        else if (mailType == "AEAlertPPA")
        {
            ButtonSendAll.Enabled = false;
            GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(choice, Convert.ToInt32(Request.QueryString["patid"]));
            //myEmail = mySae.SAEAlertDeath();
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Advere Event: Patient Passed Away Email";
            choice = mySae.PatientID;

            rDirect = "PatientEmail.aspx?mailType=CloseEmailPhysician&pcount=0&choice=" + mySae.PatientID.ToString();
        }/*
			else if(mailType == "SAEEmailCPO")
			{
				GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(choice, Convert.ToInt32(Request.QueryString["patid"]));
				myEmail = mySae.SAEEmailCPO();
				if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
				txtTo.Text = myEmail.To;
				txtCC.Text = myEmail.CC;
				txtSubject.Text = myEmail.Subject;
				txtMessage.Text = myEmail.Message; }
				LabelHeader.Text = "Adverse Event Email To CPO";
				choice = mySae.PatientID;

				rDirect = "PatientInfo.aspx?choice=" + mySae.PatientID.ToString();
			}*/
        else if (mailType == "firstReminderEmailPatient")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.firstReminderEmailPatient(choice);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "First Re-Evaluation Request To Patient";

            //rDirect = "PatientEmail.aspx?mailType=firstReminderEmailPhysician&pcount=0&choice=" + myPatient.PatientID.ToString();
            rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
        }
        /*else if(mailType == "firstReminderEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.firstReminderEmailPhysician(pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "First Re-Evaluation Request To Physician";
            pcount++;
            if(pcount == myPatient.PhysicianCount || myPatient.PhysicianCount == 0)
            {
                rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=firstReminderEmailPhysician&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
        }*/
        else if (mailType == "Day90ReminderEmailPatient")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.Day90ReminderEmailPatient(choice);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Second Re-Evaluation Request To Patient";

            //rDirect = "PatientEmail.aspx?mailType=Day90ReminderEmailPhysician&pcount=0&choice=" + myPatient.PatientID.ToString();
            rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
        }/*
			else if(mailType == "Day90ReminderEmailPhysician")
			{
				GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
				pcount = Convert.ToInt32(Request.QueryString["pcount"]);
				myEmail = myPatient.Day90ReminderEmailPhysician(pcount);
				if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
				txtTo.Text = myEmail.To;
				txtCC.Text = myEmail.CC;
				txtSubject.Text = myEmail.Subject;
				txtMessage.Text = myEmail.Message; }
				LabelHeader.Text = "Second Re-Evaluation Request To Physician";
				pcount++;
				if(pcount == myPatient.PhysicianCount || myPatient.PhysicianCount == 0)
				{
					rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
				}
				else
				{
					rDirect = "PatientEmail.aspx?mailType=Day90ReminderEmailPhysician&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
				}
			}*/
        else if (mailType == "Blank")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            LabelFrom.Text = "gipap@themaxfoundation.org";
            if (!Page.IsPostBack)
            {
                txtTo.Text = myPatient.Email;
                txtCC.Text = "";
                txtBCC.Text = "";
                txtSubject.Text = "";
                txtMessage.Text = "";
            }
            LabelHeader.Text = "Blank Email";

            rDirect = "PatientInfo.aspx?choice=" + choice.ToString();
        }
        else if (mailType == "DoseChangeEmailPatient")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.DoseChangeEmailPatient(choice);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Dose Change Email To Patient";

            rDirect = "PatientEmail.aspx?mailType=DoseChangeEmailPhysician&pcount=0&choice=" + myPatient.PatientID.ToString();
            if (rid != "")
            {
                rDirect += "&rid=" + rid;
            }
        }
        else if (mailType == "DoseChangeEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.DoseChangeEmailPhysician(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Dose Change Email To Physician";
            pcount++;
            if (pcount == myPatient.PhysicianCount || myPatient.PhysicianCount == 0)
            {
                rDirect = "PatientEmail.aspx?mailType=DoseChangeEmailCPO&pcount=0&choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=DoseChangeEmailPhysician&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
            if (rid != "")
            {
                rDirect += "&rid=" + rid;
            }
        }
        else if (mailType == "DoseChangeEmailCPO")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.DoseChangeEmailCPO(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Dose Change Email To CPO";
            pcount++;
            if (pcount == myPatient.CPOCount || myPatient.CPOCount == 0)
            {
                if (rid == "")
                {
                    rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
                }
                else
                {
                    rDirect = "SAE.aspx?choice=" + myPatient.PatientID.ToString() + "&rid=" + rid;
                }
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=DoseChangeEmailCPO&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
                if (rid != "")
                {
                    rDirect += "&rid=" + rid;
                }
            }
        }
        else if (mailType == "ExtentionEmailPatient")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.ExtentionEmailPatient(choice);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Extension Email To Patient";

            rDirect = "PatientEmail.aspx?mailType=ExtentionEmailPhysician&pcount=0&choice=" + myPatient.PatientID.ToString();
        }
        else if (mailType == "ExtentionEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.ExtentionEmailPhysician(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Extension Email To Physician";
            pcount++;
            if (pcount == myPatient.PhysicianCount || myPatient.PhysicianCount == 0)
            {
                rDirect = "PatientEmail.aspx?mailType=ExtentionEmailCPO&pcount=0&choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=ExtentionEmailPhysician&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
        }
        else if (mailType == "ExtentionEmailCPO")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.ExtentionEmailCPO(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Extension Email To CPO";
            pcount++;
            if (pcount == myPatient.CPOCount || myPatient.CPOCount == 0)
            {
                //rDirect = "GIPAP.aspx?trgt=turnoffreminders&choice=" + myPatient.PatientID.ToString();
                rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=ExtentionEmailCPO&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
        }
        else if (mailType == "Sent")
        {
            string Etype;
            try
            {
                Etype = Request.QueryString["Etype"].ToString();
            }
            catch
            {
                Etype = "Patient";
            }
            int echoice = Convert.ToInt32(Request.QueryString["echoice"]);
            myEmail = new GIPAP_Objects.Email(echoice, Etype,"GIPAP");
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = "Resending: " + myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Sent Email";
            LabelPrintLink.Text = "<A href=javascript:openNewWindow('printemail.aspx?choice=" + echoice.ToString() + "&Prog=GIPAP&Etype=" + Etype + "','thewin','height=575,width=590,toolbar=yes,scrollbars=yes')>View Print-Ready Version</a>";
            choice = myEmail.PatientID;
            if (Etype == "Patient")
            {
                rDirect = "PatientInfo.aspx?choice=" + myEmail.PatientID.ToString();
            }
            else if (Etype == "Physician")
            {
                rDirect = "GIPAP.aspx?trgt=physicianinfo&choice=" + myEmail.PhysicianID.ToString();
            }
        }
        else if (mailType == "ReactivationEmailPatient")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.ReactivationEmailPatient(choice);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Reactivation Email To Patient";

            rDirect = "PatientEmail.aspx?mailType=ReactivationEmailPhysician&pcount=0&choice=" + myPatient.PatientID.ToString();
        }
        else if (mailType == "ReactivationEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.ReactivationEmailPhysician(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Reactivation Email To Physician";
            pcount++;
            if (pcount == myPatient.PhysicianCount || myPatient.PhysicianCount == 0)
            {
                rDirect = "PatientEmail.aspx?mailType=ReactivationEmailCPO&pcount=0&choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=ReactivationEmailPhysician&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
        }
        else if (mailType == "ReactivationEmailCPO")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.ReactivationEmailCPO(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Reactivation Email To CPO";
            pcount++;
            if (pcount == myPatient.CPOCount || myPatient.CPOCount == 0)
            {
                rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=ReactivationEmailCPO&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
        }
        else if (mailType == "DenialEmailPatient")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.DenialEmailPatient(choice);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Denial Email To Patient";

            rDirect = "PatientEmail.aspx?mailType=DenialEmailPhysician&pcount=0&choice=" + myPatient.PatientID.ToString();
        }
        else if (mailType == "DenialEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.DenialEmailPhysician(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Denial Email To Physician";
            pcount++;
            if (pcount == myPatient.PhysicianCount || myPatient.PhysicianCount == 0)
            {
                if (myPatient.FlagNOA)
                {
                    rDirect = "PatientEmail.aspx?mailType=NOADenialEmailNovartis&pcount=0&choice=" + myPatient.PatientID.ToString();
                }
                else
                {
                    rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
                }
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=DenialEmailPhysician&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
        }
        else if (mailType == "NOADenialEmailNovartis")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myPatient.NOADenialEmailCPO(choice, pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Denial Email To Novartis";
            pcount++;
            if (pcount == myPatient.CPOCount || myPatient.CPOCount == 0)
            {
                rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=NOADenialEmailNovartis&pcount=" + pcount.ToString() + "&choice=" + myPatient.PatientID.ToString();
            }
        }
        else if (mailType == "Noa10DaySupply")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.NOA10DaySupply(choice);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "NOA 10 Day Supply Email To Novartis";
            rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
        }
        else if (mailType == "NOABranchAssignment")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myEmail = myPatient.NOABranchAssignment(choice);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "NOA Branch Assignment";
            rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
            /*if (myPatient.GIPAPStatus == "Pending")
            {
                rDirect = "PatientEmail.aspx?mailType=Noa10DaySupply&choice=" + myPatient.PatientID.ToString();
            }
            else
            {
                rDirect = "PatientInfo.aspx?choice=" + myPatient.PatientID.ToString();
            }*/
            ButtonSend.Visible = true;
            ButtonCancel.Visible = true;
        }
        else if (mailType == "PhysicianTransferEmailDelete")
        {
            GIPAP_Objects.AddRemove myAR = new GIPAP_Objects.AddRemove();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myAR.PhysicianTransferEmail(choice, "delete", pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Transfer Email To Physician";
            pcount++;
            if (pcount == myAR.PersonCount || myAR.PersonCount == 0)
            {
                rDirect = "PatientEmail.aspx?mailType=PhysicianTransferEmailCreate&pcount=0&choice=" + choice.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=PhysicianTransferEmailDelete&pcount=" + pcount.ToString() + "&choice=" + choice.ToString();
            }
        }
        else if (mailType == "PhysicianTransferEmailCreate")
        {
            GIPAP_Objects.AddRemove myAR = new GIPAP_Objects.AddRemove();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myAR.PhysicianTransferEmail(choice, "create", pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Transfer Email To Physician";
            pcount++;
            if (pcount == myAR.PersonCount || myAR.PersonCount == 0)
            {
                rDirect = "PatientEmail.aspx?mailType=PhysicianTransferEmailNovartis&pcount=0&choice=" + choice.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=PhysicianTransferEmailCreate&pcount=" + pcount.ToString() + "&choice=" + choice.ToString();
            }
        }
        else if (mailType == "PhysicianTransferEmailNovartis")
        {
            GIPAP_Objects.AddRemove myAR = new GIPAP_Objects.AddRemove();
            pcount = Convert.ToInt32(Request.QueryString["pcount"]);
            myEmail = myAR.PhysicianTransferEmail(choice, "novartis", pcount);
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtBCC.Text = myEmail.BCC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Patient Transfer Email To Physician";
            pcount++;
            if (pcount == myAR.PersonCount || myAR.PersonCount == 0)
            {
                rDirect = "PatientInfo.aspx?choice=" + choice.ToString();
            }
            else
            {
                rDirect = "PatientEmail.aspx?mailType=PhysicianTransferEmailNovartis&pcount=" + pcount.ToString() + "&choice=" + choice.ToString();
            }
        }
        try
        {
            
            string a = Request.QueryString["a"].ToString();
            
            if (a != "")
            {
                rDirect += "&a=" + a;
            }
        }
        catch { }
        try
        {
            if (Request.QueryString["SendAll"].ToString() == "yes")
            {
                rDirect += "&SendAll=yes";
                this.SendMail();
            }
        }
        catch { }
        txtCC.Text = txtCC.Text.Replace("gipap@themaxfoundation.org", "");
    }
    //**************************************************************************************************************
    public void SendMail()
    {
        LabelError.Text = "";
        try
        {
            GIPAP_Objects.Email sendEmail = new GIPAP_Objects.Email(LabelFrom.Text, txtTo.Text, txtCC.Text, txtBCC.Text, txtSubject.Text, txtMessage.Text, choice, myEmail.PhysicianID, myEmail.MailType, myEmail.SAEID);
            sendEmail.Send(sessUse.Username);
        }
        catch (Exception exx)
        {
            LabelError.Text = exx.Message.ToString();
            ButtonSend.Enabled = true;
            return;
        }
        Response.Redirect(rDirect);
    }
    protected void ButtonSend_Click(object sender, EventArgs e)
    {
        this.SendMail();
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        if (mailType == "firstReminderEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myPatient.UpdateFirstReminder(sessUse.Username, choice);
        }
        else if (mailType == "Day90ReminderEmailPhysician")
        {
            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
            myPatient.UpdateSecondReminder(sessUse.Username, choice);
        }
        Response.Redirect(rDirect);
    }
    protected void ButtonSendAll_Click(object sender, EventArgs e)
    {
        rDirect += "&SendAll=yes";
        this.SendMail();
    }
}
