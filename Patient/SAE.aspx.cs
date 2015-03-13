using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_SAE : System.Web.UI.Page
{
    GIPAP_Objects.SAE mySae;
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.Request myRequest;
    GIPAP_Objects.PatientGipapStatus myStatus;
    
    int choice;
    int rid;
    string red;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        mySae = new GIPAP_Objects.SAE(0, choice);
        try
        {
            rid = Convert.ToInt32(Request.QueryString["rid"]);
        }
        catch
        {
            rid = 0;
        }
        try
        {
            red = Request.QueryString["red"].ToString();
        }
        catch
        {
            red = "";
        }
        if (rid != 0)
        {
            myRequest = new GIPAP_Objects.Request(rid, choice);
            ButtonCancel.Visible = false;
            ButtonResolve.Visible = true;
        }
        
        if (!Page.IsPostBack)
        {
            for (int i = 0; i < 5; i++)
            {
                dropYear.Items.Add((DateTime.Now.Year - i).ToString());
            }
           // dropDay.SelectedValue = DateTime.Today.Day.ToString();
          //  dropMonth.SelectedValue = DateTime.Today.Month.ToString();
          //  dropYear.SelectedValue = DateTime.Today.Year.ToString();
            LabelSAEEmail.Text = mySae.SAEEmail;
            if (mySae.SAEEmail.Trim() == "")
            {
                LabelEmailError.Text = "There is no SAE email address for this country.  This event cannot be logged.";
                PanelLog.Visible = false;
            }
            else if (rid != 0)
            {
                txtEvent.Text = myRequest.Notes;
                rblstLearned.Items[0].Selected = myRequest.PhysicianRequested;
                if (myRequest.PhysicianRequested)
                {
                    cbConsent.Checked = false;
                    PanelConsent.Visible = false;
                }

                if (myRequest.Resolved)
                {
                    Response.Redirect("GIPAP.aspx?trgt=patientinfo&choice=" + mySae.PatientID.ToString());
                }
            }
            if (mySae.GIPAPStatus == "Active")
            {
                PanelClose.Visible = true;
            }
            if (mySae.SAECount > 0)
            {
                PanelExisting.Visible = true;
                LabelExisting.Text = mySae.SAEList();// "<a href='patientdatasets.aspx?choice=" + mySae.PatientID.ToString() + "&ds=sae' class='lbAR'>" + mySae.SAECount.ToString() + " Adverse Events are logged for this patient</a>";
            }
        }
    }
    protected void ButtonSubmitEvent_Click(object sender, EventArgs e)
    {
        if (cbConsent.Checked || rblstLearned.SelectedIndex == 0)
        {
            mySae.Event = txtEvent.Text.Replace("\n", "<br>");
            mySae.LearnedFromPhysician = rblstLearned.Items[0].Selected;
            mySae.DateLearned = Convert.ToDateTime(dropMonth.SelectedValue + "/" + dropDay.SelectedValue + "/" + dropYear.SelectedValue);          
            mySae.Consent = cbConsent.Checked;
            mySae.CloseCase = rblstClosed.Items[1].Selected;
            try
            {
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                myEmail = mySae.SAEAlertNovartis();
                myEmail.Send(sessUse.Username);
                mySae.EmailID = myEmail.EmailID;
            }
            catch
            {
                LabelEmailError.Text = "There was a problem sending the email report.  This AE was not logged.";
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
            if (rid != 0)
            {
                myRequest.Resolve(sessUse.Username);
            }
            if (rblstClosed.Items[1].Selected)
            {
                Response.Redirect("Close.aspx?choice=" + mySae.PatientID.ToString() + "&action=close");
            }
            else if (red == "noafef")
            {
                Response.Redirect("NOAFEF.aspx?choice=" + mySae.PatientID.ToString());
            }
            else
            {
                Response.Redirect("Patientinfo.aspx?a=sae&choice=" + mySae.PatientID.ToString());
            }
        }
        else
        {
            LabelError.Visible = true;
        }
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + mySae.PatientID.ToString());
    }
    protected void rblstLearned_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblstLearned.SelectedIndex == 1)
        {
            PanelConsent.Visible = true;
        }
        else
        {
            cbConsent.Checked = false;
            PanelConsent.Visible = false;
        }
    }
    protected void ButtonResolve_Click(object sender, EventArgs e)
    {
        myRequest.Resolve(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=nosae&choice=" + mySae.PatientID.ToString());
    }
}
