using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_PhysicianEmail : System.Web.UI.Page
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
                choice = Convert.ToInt32(Request.QueryString["echoice"]);
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
                choice = Convert.ToInt32(Request.QueryString["echoice"]);
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

        if (mailType == "Sent")
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
            myEmail = new GIPAP_Objects.Email(choice, Etype,"GIPAP");
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtSubject.Text = "Resending: " + myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Sent Email";
            LabelPrintLink.Text = "<A href=javascript:openNewWindow('patient/printemail.aspx?choice=" + choice.ToString() + "&Etype=" + Etype + "','thewin','height=575,width=590,toolbar=yes,scrollbars=yes')>View Print-Ready Version</a>";
            choice = myEmail.PatientID;
            if (Etype == "Patient")
            {
                rDirect = "PatientInfo.aspx?choice=" + myEmail.PatientID.ToString();
            }
            else if (Etype == "Physician")
            {
                rDirect = "PhysicianInfo.aspx?a=mail&choice=" + myEmail.PhysicianID.ToString();
            }
        }
        else if (mailType == "PhysicianApprovalEmailPhysician")
        {
            GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician(Convert.ToInt32(Request.QueryString["choice"]), sessUse.Role);
            myEmail = myPhysician.createApprovalEmail();
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Physician Approval Email To Physician";
            choice = 0;

            rDirect = "PhysicianEmail.aspx?mailType=PhysicianApprovalEmailCPO&choice=" + myPhysician.PhysicianID.ToString();
        }
        else if (mailType == "PhysicianApprovalEmailCPO")
        {
            GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician(Convert.ToInt32(Request.QueryString["choice"]), sessUse.Role);
            myEmail = myPhysician.createApprovalEmailtoCPO();
            if (!Page.IsPostBack){ LabelFrom.Text = myEmail.From;
            txtTo.Text = myEmail.To;
            txtCC.Text = myEmail.CC;
            txtSubject.Text = myEmail.Subject;
            txtMessage.Text = myEmail.Message; }
            LabelHeader.Text = "Physician Approval Email To CPO";
            choice = 0;

            rDirect = "PhysicianInfo.aspx?a=approve&choice=" + myPhysician.PhysicianID.ToString();
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
