using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class TMF_EmailsAutoClose : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            choice = Convert.ToInt32(Request.QueryString["choice"]);
        }
        catch
        {
            choice = 0;
        }
        if (choice != 0)
        {
            LabelPOName.Text = sessUse.TempName;
        }
        else
        {
            LabelPOName.Text = sessUse.FullName;
        }
        if (!Page.IsPostBack)
        {
            dropOtherUsers.DataSource = sessUse.OtherUsers;
            dropOtherUsers.DataTextField = "personname";
            dropOtherUsers.DataValueField = "userid";
            dropOtherUsers.DataBind();
            dropOtherUsers.Items.Insert(0, "Select another user's workload to view");

            //first notices
            DataTable dt = sessUse.getDatasets(choice, "firstreminders").Tables[0];
            if (dt.Rows.Count > 0)
            {
                dgPatientReminders.DataSource = dt;
                dgPatientReminders.DataBind();
                LabelResultCountPatientReminders.Text = dt.Rows.Count.ToString() + " Patient Reminders";
                this.SetbuttonDisabler(ButtonSendPatientReminders);
            }
            else
            {
                PanelPatientReminders.Visible = false;
            }

            //second notices
            dt = sessUse.getDatasets(choice, "secondreminders").Tables[0];
            if (dt.Rows.Count > 0)
            {
                dgSecondNotices.DataSource = dt;
                dgSecondNotices.DataBind();
                LabelResultCountSecondNotices.Text = dt.Rows.Count.ToString() + " Second Notices";
                this.SetbuttonDisabler(ButtonSendSecondNotices);
            }
            else
            {
                PanelSecondNotices.Visible = false;
            }

            //physician
            dt = sessUse.getDatasets(choice, "physicianreminders").Tables[0];
            if (dt.Rows.Count > 0)
            {
                dgPhysicianReminders.DataSource = dt;
                dgPhysicianReminders.DataBind();
                LabelResultCountPhysicianReminders.Text = dt.Rows.Count.ToString() + " Physician Reminders";
                this.SetbuttonDisabler(ButtonSendPhysicianReminders);
            }
            else
            {
                PanelPhysicianReminders.Visible = false;
            }

            //auto close
            dt = sessUse.getDatasets(choice, "autoclose").Tables[0];
            if (dt.Rows.Count > 0)
            {
                dgAutoClose.DataSource = dt;
                dgAutoClose.DataBind();
                LabelResultCountAutoClose.Text = dt.Rows.Count.ToString() + " Auto Closures";
                this.SetbuttonDisabler(ButtonAutoClose);
            }
            else
            {
                PanelAutoClose.Visible = false;
            }
        }
    }
    private void SetbuttonDisabler(Button myButton)
    {

        //SET UP send BUTTON DISABLER....
        System.Text.StringBuilder sbDisable = new System.Text.StringBuilder();
        sbDisable.Append("if (typeof(Page_ClientValidate) == 'function') {");
        sbDisable.Append("if (Page_ClientValidate() == false) {");
        sbDisable.Append("return false;");
        sbDisable.Append("}");
        sbDisable.Append("}");
        sbDisable.Append("this.value = 'Please wait...';");
        sbDisable.Append("this.disabled = true;");
        sbDisable.Append(Page.GetPostBackEventReference(myButton));
        sbDisable.Append(";");
        //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

        myButton.Attributes.Add("onclick", sbDisable.ToString());
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;
        //mw
        //GridViewRow row = dgResultsClosed.Rows[index];
        (gvRow.FindControl("ButtonRemove") as ImageButton).Visible = false;
        (gvRow.FindControl("ButtonRemove2") as Button).Visible = true;
    }
    protected void Button2PatientReminders_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;

        Label lbpatid = (Label)gvRow.FindControl("lblPatientID");

        GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
        myPatient.UpdateFirstReminder(sessUse.Username, Convert.ToInt32(lbpatid.Text));

        (gvRow.FindControl("chkProcess") as CheckBox).Checked = false;
        gvRow.Visible = false;
    }
    protected void Button2SecondNotices_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;

        Label lbpatid = (Label)gvRow.FindControl("lblPatientID");

        GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
        myPatient.UpdateSecondReminder(sessUse.Username, Convert.ToInt32(lbpatid.Text));

        (gvRow.FindControl("chkProcess") as CheckBox).Checked = false;
        gvRow.Visible = false;
    }
    protected void Button2PhysicianReminders_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;

        (gvRow.FindControl("chkProcess") as CheckBox).Checked = false;
        gvRow.Visible = false;
    }
    protected void Button2AutoClose_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;

        (gvRow.FindControl("chkProcess") as CheckBox).Checked = false;
        gvRow.Visible = false;
    }
    protected void dropOtherUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropOtherUsers.SelectedIndex != 0)
        {
            sessUse.TempName = dropOtherUsers.SelectedItem.Text;
            Session["sessUse"] = sessUse;
            Response.Redirect("EmailsAutoClose.aspx?choice=" + dropOtherUsers.SelectedValue);
        }
    }
    protected void ButtonSendPatientReminders_Click(object sender, EventArgs e)
    {
        LabelPatientReminderError.Text = "";
        for (int i = 0; i < dgPatientReminders.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgPatientReminders.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label eName = (System.Web.UI.WebControls.Label)dgPatientReminders.Rows[i].FindControl("lblErrorName");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgPatientReminders.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked && dgPatientReminders.Rows[i].Enabled)
            {
                GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                myEmail = myPatient.firstReminderEmailPatient(Convert.ToInt32(IdLabel.Text));
                try
                {
                    myEmail.Send(sessUse.Username);
                    dgPatientReminders.Rows[i].Enabled = false;
                }
                catch
                {
                    LabelPatientReminderError.Text += "<li>" + eName.Text + "</li>";
                }
            }
        }
        if (LabelPatientReminderError.Text == "")
        {
            dgPatientReminders.Visible = ButtonSendPatientReminders.Visible = false;
            LabelPatientReminders.Visible = true;
            LabelPatientReminders.Text += " [<a href=EmailsAutoClose.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
        else
        {
            LabelPatientReminderError.Visible = true;
            LabelPatientReminderError.Text = "There was a problem with the email address for the following patients:" + LabelPatientReminderError.Text;
        }
    }
    protected void ButtonSelectAllPatientReminders_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgPatientReminders.Rows.Count; i++)
        {
            if (dgPatientReminders.Rows[i].Visible)
            {
                (dgPatientReminders.Rows[i].FindControl("chkProcess") as CheckBox).Checked = true;
            }
        }
    }
    protected void ButtonSendSecondNotices_Click(object sender, EventArgs e)
    {
        LabelSecondNoticesError.Text = "";
        for (int i = 0; i < dgSecondNotices.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgSecondNotices.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label eName = (System.Web.UI.WebControls.Label)dgSecondNotices.Rows[i].FindControl("lblErrorName");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgSecondNotices.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked && dgSecondNotices.Rows[i].Enabled)
            {
                GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                myEmail = myPatient.Day90ReminderEmailPatient(Convert.ToInt32(IdLabel.Text));
                try
                {
                    myEmail.Send(sessUse.Username);
                    dgSecondNotices.Rows[i].Enabled = false;
                }
                catch
                {
                    LabelSecondNoticesError.Text += "<li>" + eName.Text + "</li>";
                }
            }
        }
        if (LabelSecondNoticesError.Text == "")
        {
            dgSecondNotices.Visible = ButtonSendSecondNotices.Visible = false;
            LabelSecondNotices.Visible = true;
            LabelSecondNotices.Text += " [<a href=EmailsAutoClose.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
        else
        {
            LabelSecondNoticesError.Visible = true;
            LabelSecondNoticesError.Text = "There was a problem with the email address for the following patients:" + LabelSecondNoticesError.Text;
        }
    }
    protected void ButtonSelectAllSecondNotices_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgSecondNotices.Rows.Count; i++)
        {
            if (dgSecondNotices.Rows[i].Visible)
            {
                (dgSecondNotices.Rows[i].FindControl("chkProcess") as CheckBox).Checked = true;
            }
        }
    }
    protected void ButtonSendPhysicianReminders_Click(object sender, EventArgs e)
    {
        LabelPhysicianReminderError.Text = "";
        for (int i = 0; i < dgPhysicianReminders.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgPhysicianReminders.Rows[i].FindControl("lblPhysicianID");
            System.Web.UI.WebControls.Label eName = (System.Web.UI.WebControls.Label)dgPhysicianReminders.Rows[i].FindControl("lblErrorName");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgPhysicianReminders.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked && dgPhysicianReminders.Rows[i].Enabled)
            {
                GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician(Convert.ToInt32(IdLabel.Text), sessUse.Role);
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                myEmail = myPhysician.ReminderEmail();
                try
                {
                    myEmail.Send(sessUse.Username);
                    dgPhysicianReminders.Rows[i].Enabled = false;
                }
                catch
                {
                    LabelPhysicianReminderError.Text += "<li>" + eName.Text + "</li>";
                }
            }
        }
        if (LabelPhysicianReminderError.Text == "")
        {
            dgPhysicianReminders.Visible = ButtonSendPhysicianReminders.Visible = false;
            LabelPhysicianReminders.Visible = true;
            LabelPhysicianReminders.Text += " [<a href=EmailsAutoClose.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
        else
        {
            LabelPhysicianReminderError.Visible = true;
            LabelPhysicianReminderError.Text = "There was a problem with the email address for the following physicians:" + LabelPhysicianReminderError.Text;
        }
    }
    protected void ButtonSelectAllPhysicianReminders_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgPhysicianReminders.Rows.Count; i++)
        {
            if (dgPhysicianReminders.Rows[i].Visible)
            {
                (dgPhysicianReminders.Rows[i].FindControl("chkProcess") as CheckBox).Checked = true;
            }
        }
    }
    protected void ButtonAutoClose_Click(object sender, EventArgs e)
    {
        LabelAutoCloseError.Text = "";
        for (int i = 0; i < dgAutoClose.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgAutoClose.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label eName = (System.Web.UI.WebControls.Label)dgAutoClose.Rows[i].FindControl("lblErrorName");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgAutoClose.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked && dgAutoClose.Rows[i].Enabled)
            {
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(IdLabel.Text), "close");
                myStatus.StatusReason = "No re-evaluation information provided";
                myStatus.Notes = "Automatically logged by program officer";
                myStatus.PhysicianRequested = false;
                myStatus.Close(sessUse.Username);
                GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
                try
                {
                    myEmail = myPatient.CloseEmailPatient(Convert.ToInt32(IdLabel.Text));
                    myEmail.Send(sessUse.Username);
                    for (int p = 0; p < myPatient.PhysicianCount; p++)
                    {
                        myEmail = myPatient.CloseEmailPhysician(p);
                        myEmail.Send(sessUse.Username);
                    }
                    myEmail = myPatient.CloseEmail();
                    myEmail.Send(sessUse.Username);

                    dgAutoClose.Rows[i].Enabled = false;
                }
                catch
                {
                    LabelAutoCloseError.Text += "<li>" + eName.Text + "</li>";
                }
            }
        }
        if (LabelAutoCloseError.Text == "")
        {
            dgAutoClose.Visible = ButtonAutoClose.Visible = false;
            LabelAutoClose.Visible = true;
            LabelAutoClose.Text += " [<a href=EmailsAutoClose.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
        else
        {
            LabelAutoCloseError.Visible = true;
            LabelAutoCloseError.Text = "There was a problem with the email address for the following patients:" + LabelAutoCloseError.Text;
        }
    }
    protected void ButtonSelectAllAutoClose_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgAutoClose.Rows.Count; i++)
        {
            if (dgAutoClose.Rows[i].Visible)
            {
                (dgAutoClose.Rows[i].FindControl("chkProcess") as CheckBox).Checked = true;
            }
        }
    }
}
