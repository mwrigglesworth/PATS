using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TMF_PORequests : System.Web.UI.Page
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
        /*removes cache so when they hit the back button it refreshes*/
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetAllowResponseInBrowserHistory(true);

        if (!Page.IsPostBack)
        {
            DataSet ds = sessUse.getPORequests(choice);
            
            if (ds.Tables[0].Rows.Count > 0)//close
            {
                dgResultsClosed.DataSource = ds.Tables[0];
                dgResultsClosed.DataBind();
                LabelResultCountClosed.Text = ds.Tables[0].Rows.Count.ToString() + " Close Requests";
                this.SetbuttonDisabler(ButtonClose);
            }
            else
            {
                PanelClosed.Visible = false;
            }

            if (ds.Tables[1].Rows.Count > 0)//reactivate
            {
                dgReactivate.DataSource = ds.Tables[1];
                dgReactivate.DataBind();
                LabelResultCountReactivate.Text = ds.Tables[1].Rows.Count.ToString() + " Reactivate Requests";
                this.SetbuttonDisabler(ButtonReactivate);
            }
            else
            {
                PanelReactivate.Visible = false;
            }

            if (ds.Tables[2].Rows.Count > 0)//extend
            {
                dgExtend.DataSource = ds.Tables[2];
                dgExtend.DataBind();
                LabelResultCountExtend.Text = ds.Tables[2].Rows.Count.ToString() + " Extend Requests";
                this.SetbuttonDisabler(ButtonExtend);
            }
            else
            {
                PanelExtend.Visible = false;
            }

            if (ds.Tables[3].Rows.Count > 0)//dose change
            {
                dgDoseChange.DataSource = ds.Tables[3];
                dgDoseChange.DataBind();
                LabelResultCountDoseChange.Text = ds.Tables[3].Rows.Count.ToString() + " Dosage Change Requests";
                this.SetbuttonDisabler(ButtonDoseChange);
            }
            else
            {
                PanelDoseChange.Visible = false;
            }

            if (ds.Tables[4].Rows.Count > 0)//treatment change
            {
                dgTreatmentChange.DataSource = ds.Tables[4];
                dgTreatmentChange.DataBind();
                LabelResultCountTreatmentChange.Text = ds.Tables[4].Rows.Count.ToString() + " Treatment Change Requests";
                this.SetbuttonDisabler(ButtonTreatmentChange);
            }
            else
            {
                PanelTreatmentChange.Visible = false;
            }

            if (ds.Tables[5].Rows.Count > 0)//reassess
            {
                dgReassess.DataSource = ds.Tables[5];
                dgReassess.DataBind();
                LabelResultCountReassess.Text = ds.Tables[5].Rows.Count.ToString() + " Reassess Requests";
                this.SetbuttonDisabler(ButtonReassess);
            }
            else
            {
                PanelReassess.Visible = false;
            }

            if (ds.Tables[6].Rows.Count > 0)//fef updates
            {
                dgFEF.DataSource = ds.Tables[6];
                dgFEF.DataBind();
                LabelResultCountFEF.Text = ds.Tables[6].Rows.Count.ToString() + " FEF Updates";
                this.SetbuttonDisabler(ButtonFEF);
            }
            else
            {
                PanelFEF.Visible = false;
            }

            if (ds.Tables[7].Rows.Count > 0)//ae
            {
                dgAE.DataSource = ds.Tables[7];
                dgAE.DataBind();
                LabelResultCountAE.Text = ds.Tables[7].Rows.Count.ToString() + " AE Requests";
                this.SetbuttonDisabler(ButtonAE);
            }
            else
            {
                PanelAE.Visible = false;
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;
        //mw
        Label lbrid = (Label)gvRow.FindControl("lblRequestID");
        Label lbpatid = (Label)gvRow.FindControl("lblPatientID");

        GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(lbrid.Text), Convert.ToInt32(lbpatid.Text));
        myRequest.Resolve(sessUse.Username);
        (gvRow.FindControl("chkProcess") as CheckBox).Checked = false;
        gvRow.Visible = false;
    }
    protected void ButtonSAE_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;
        //mw
        Label lbpatid = (Label)gvRow.FindControl("lblPatientID");

        GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();

        (gvRow.FindControl("lbAECount") as LinkButton).Visible = false;
        (gvRow.FindControl("LabelAEHistory") as Label).Text = myPatient.AEHistory(Convert.ToInt32(lbpatid.Text));
    }
    protected void dgResultsClosed_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        // Retrieve the row that contains the button clicked 
        // by the user from the Rows collection.      
        GridViewRow row = dgResultsClosed.Rows[index];

        Label lbrid = (Label)row.FindControl("lblRequestID");
        Label lbpatid = (Label)row.FindControl("lblPatientID");

        GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(lbrid.Text), Convert.ToInt32(lbpatid.Text));
        myRequest.Resolve(sessUse.Username);
        dgResultsClosed.Rows[index].Visible = false;
    }
    protected void ButtonClose_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgResultsClosed.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgResultsClosed.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label reqLabel = (System.Web.UI.WebControls.Label)dgResultsClosed.Rows[i].FindControl("lblRequestID");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgResultsClosed.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked)
            {
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus();
                GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(reqLabel.Text), Convert.ToInt32(IdLabel.Text));
                myStatus.PatientID = myRequest.PatientID;
                myStatus.RequestID = myRequest.RequestID;
                myStatus.StatusReason = myRequest.StatusReason;
                myStatus.Notes = myRequest.Notes;
                myStatus.PhysicianRequested = myRequest.PhysicianRequested;
                if (myStatus.PhysicianRequested)
                {
                    myStatus.ReceivedBy = myRequest.ReceivedBy;
                }
                else
                {
                    myStatus.ReceivedBy = "";
                }
                myStatus.AERelated = myRequest.AERelated;
                myStatus.Close(sessUse.Username);

                // send email
                GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                myEmail = myPatient.CloseEmailPatient(Convert.ToInt32(IdLabel.Text));
                if (myStatus.StatusReason != "Patient has passed away")
                {
                    myEmail.Send(sessUse.Username);
                }
                if (myPatient.PhysicianCount > 0)
                {
                    for (int ph = 0; ph < myPatient.PhysicianCount; ph++)
                    {
                        myEmail = myPatient.CloseEmailPhysician(ph);
                        myEmail.Send(sessUse.Username);
                    }
                }
                myEmail = myPatient.CloseEmail();
                myEmail.Send(sessUse.Username);
            }
        }
        dgResultsClosed.Visible = ButtonClose.Visible = false;
        LabelClose.Visible = true;
        LabelClose.Text += " [<a href=PORequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
    }
    protected void ButtonReactivate_Click(object sender, EventArgs e)
    {
        LabelReactivateError.Text = "";
        for (int i = 0; i < dgReactivate.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgReactivate.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label reqLabel = (System.Web.UI.WebControls.Label)dgReactivate.Rows[i].FindControl("lblRequestID");
            System.Web.UI.WebControls.Label eName = (System.Web.UI.WebControls.Label)dgReactivate.Rows[i].FindControl("lblErrorName");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgReactivate.Rows[i].FindControl("chkProcess");
            System.Web.UI.WebControls.DropDownList dlReason = (System.Web.UI.WebControls.DropDownList)dgReactivate.Rows[i].FindControl("dropReactivation");
            if (chkProc.Checked && dgReactivate.Rows[i].Enabled)
            {
                if (dlReason.SelectedValue != "0")
                {
                    GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus();
                    GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(reqLabel.Text), Convert.ToInt32(IdLabel.Text));
                    myStatus.PatientID = myRequest.PatientID;
                    myStatus.RequestID = myRequest.RequestID;
                    myStatus.Notes = myRequest.Notes;
                    myStatus.StatusReason = myRequest.StatusReason;
                    myStatus.PhysicianRequested = myRequest.PhysicianRequested;
                    if (myStatus.PhysicianRequested)
                    {
                        myStatus.ReceivedBy = myRequest.ReceivedBy;
                    }
                    else
                    {
                        myStatus.ReceivedBy = "";
                    }
                    myStatus.StartDate = DateTime.Today;
                    //4 month
                    myStatus.EndDate = DateTime.Today.AddDays(119);
                    myStatus.CurrentDosage = myRequest.CurrentDosage;
                    myStatus.RestartTreatment = myRequest.RestartTreatment;
                    myStatus.FinancialStatus = myRequest.FinancialStatus;
                    myStatus.CurrentCMLPhase = myRequest.CurrentCMLPhase;
                    myStatus.ReActivate(sessUse.Username, dlReason.SelectedValue);

                    // send email
                    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
                    GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                    myEmail = myPatient.ReactivationEmailPatient(Convert.ToInt32(IdLabel.Text));
                    myEmail.Send(sessUse.Username);
                    if (myPatient.PhysicianCount > 0)
                    {
                        for (int ph = 0; ph < myPatient.PhysicianCount; ph++)
                        {
                            myEmail = myPatient.ReactivationEmailPhysician(Convert.ToInt32(IdLabel.Text), ph);
                            myEmail.Send(sessUse.Username);
                        }
                    }
                    if (myPatient.CPOCount > 0)
                    {
                        for (int cp = 0; cp < myPatient.CPOCount; cp++)
                        {
                            myEmail = myPatient.ReactivationEmailCPO(Convert.ToInt32(IdLabel.Text), cp);
                            myEmail.Send(sessUse.Username);
                        }
                    }
                    dgReactivate.Rows[i].Enabled = false;
                }
                else
                {
                    LabelReactivateError.Text += "<li>" + eName.Text + "</li>";
                }
            }
        }
        if (LabelReactivateError.Text == "")
        {
            dgReactivate.Visible = ButtonReactivate.Visible = false;
            LabelReactivate.Visible = true;
            LabelReactivate.Text += " [<a href=PORequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
        else
        {
            LabelReactivateError.Visible = true;
            LabelReactivateError.Text = "You must select a reason to reactivate the following patients:" + LabelReactivateError.Text;
        }
    }
    protected void ButtonExtend_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgExtend.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgExtend.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label reqLabel = (System.Web.UI.WebControls.Label)dgExtend.Rows[i].FindControl("lblRequestID");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgExtend.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked)
            {
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus();
                GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(reqLabel.Text), Convert.ToInt32(IdLabel.Text));
                myStatus.PatientID = myRequest.PatientID;
                myStatus.RequestID = myRequest.RequestID;
                myStatus.StatusReason = myRequest.StatusReason;
                myStatus.EndDate = myRequest.EndDate;
                myStatus.PhysicianRequested = myRequest.PhysicianRequested;
                if (myStatus.PhysicianRequested)
                {
                    myStatus.ReceivedBy = myRequest.ReceivedBy;
                }
                else
                {
                    myStatus.ReceivedBy = "";
                }
                myStatus.Extend(sessUse.Username);

                // send email
                GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                myEmail = myPatient.ExtentionEmailPatient(Convert.ToInt32(IdLabel.Text));
                myEmail.Send(sessUse.Username);
                if (myPatient.PhysicianCount > 0)
                {
                    for (int ph = 0; ph < myPatient.PhysicianCount; ph++)
                    {
                        myEmail = myPatient.ExtentionEmailPhysician(Convert.ToInt32(IdLabel.Text), ph);
                        myEmail.Send(sessUse.Username);
                    }
                }
                if (myPatient.CPOCount > 0)
                {
                    for (int cp = 0; cp < myPatient.CPOCount; cp++)
                    {
                        myEmail = myPatient.ExtentionEmailCPO(Convert.ToInt32(IdLabel.Text), cp);
                        myEmail.Send(sessUse.Username);
                    }
                }
            }
        }
        dgExtend.Visible = ButtonExtend.Visible = false;
        LabelExtend.Visible = true;
        LabelExtend.Text += " [<a href=PORequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
    }
    protected void ButtonDoseChange_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgDoseChange.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgDoseChange.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label reqLabel = (System.Web.UI.WebControls.Label)dgDoseChange.Rows[i].FindControl("lblRequestID");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgDoseChange.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked)
            {
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(IdLabel.Text), "dosagechange");
                GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(reqLabel.Text), Convert.ToInt32(IdLabel.Text));
                myStatus.PatientID = myRequest.PatientID;
                myStatus.RequestID = myRequest.RequestID;
                myStatus.CountryID = myRequest.CountryID; //this is for the order creation in india
                //this is done in the method now myStatus.CurrentDosage = myRequest.RequestedDosage;
                myStatus.ChangeDosageReason = myRequest.Notes;
                myStatus.PhysicianRequested = myRequest.PhysicianRequested;
                if (myStatus.PhysicianRequested)
                {
                    myStatus.ReceivedBy = myRequest.ReceivedBy;
                }
                else
                {
                    myStatus.ReceivedBy = "";
                }
                myStatus.AERelated = myRequest.AERelated;
                myStatus.ChangeDosage(sessUse.Username, myRequest.RequestedDosage);

                // send email
                GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                myEmail = myPatient.DoseChangeEmailPatient(Convert.ToInt32(IdLabel.Text));
                myEmail.Send(sessUse.Username);
                if (myPatient.PhysicianCount > 0)
                {
                    for (int ph = 0; ph < myPatient.PhysicianCount; ph++)
                    {
                        myEmail = myPatient.DoseChangeEmailPhysician(Convert.ToInt32(IdLabel.Text), ph);
                        myEmail.Send(sessUse.Username);
                    }
                }
                if (myPatient.CPOCount > 0)
                {
                    for (int cp = 0; cp < myPatient.CPOCount; cp++)
                    {
                        myEmail = myPatient.DoseChangeEmailCPO(Convert.ToInt32(IdLabel.Text), cp);
                        myEmail.Send(sessUse.Username);
                    }
                }
            }
        }
        dgDoseChange.Visible = ButtonDoseChange.Visible = false;
        LabelDoseChange.Visible = true;
        LabelDoseChange.Text += " [<a href=PORequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
    }
    protected void ButtonTreatmentChange_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgTreatmentChange.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgTreatmentChange.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label reqLabel = (System.Web.UI.WebControls.Label)dgTreatmentChange.Rows[i].FindControl("lblRequestID");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgTreatmentChange.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked)
            {
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus();
                GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(reqLabel.Text), Convert.ToInt32(IdLabel.Text));
                myStatus.PatientID = myRequest.PatientID;
                myStatus.RequestID = myRequest.RequestID;
                myStatus.FlagNoa = myRequest.FlagNoa;
                myStatus.CurrentDosage = myRequest.RequestedDosage;
                myStatus.StatusReason = "Changed Treatment to Tasigna";
                myStatus.Notes = "automatically logged during treatment change";
                myStatus.StartDate = DateTime.Today;
                //4 month
                myStatus.EndDate = DateTime.Today.AddDays(119);
                myStatus.RestartTreatment = true;
                myStatus.FinancialStatus = true;
                myStatus.CurrentCMLPhase = myRequest.CurrentCMLPhase;
                if (myRequest.GipapStatus == "Active")
                {
                    myStatus.Close(sessUse.Username);
                    myStatus.Notes = myRequest.Notes;
                    myStatus.ReActivate(sessUse.Username, myStatus.StatusReason);
                }
                else if (myRequest.GipapStatus == "Closed")
                {
                    myStatus.Notes = myRequest.Notes;
                    myStatus.ReActivate(sessUse.Username, myStatus.StatusReason);
                }
                else if (myRequest.GipapStatus == "Denied")
                {
                    myStatus.ReAssess(sessUse.Username, myStatus.Notes);
                    myStatus.Notes = myRequest.Notes;
                    myStatus.Approve(sessUse.Username);
                }

                // send email
                GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                myEmail = myPatient.ApprovalEmailPatient(Convert.ToInt32(IdLabel.Text));
                myEmail.Send(sessUse.Username);
                if (myPatient.PhysicianCount > 0)
                {
                    for (int ph = 0; ph < myPatient.PhysicianCount; ph++)
                    {
                        myEmail = myPatient.ApprovalEmailPhysician(Convert.ToInt32(IdLabel.Text), ph);
                        myEmail.Send(sessUse.Username);
                    }
                }
                if (myPatient.CPOCount > 0)
                {
                    for (int cp = 0; cp < myPatient.CPOCount; cp++)
                    {
                        myEmail = myPatient.ApprovalEmailCPO(Convert.ToInt32(IdLabel.Text), cp);
                        myEmail.Send(sessUse.Username);
                    }
                }
            }
        }
        dgTreatmentChange.Visible = ButtonTreatmentChange.Visible = false;
        LabelTreatmentChagne.Visible = true;
        LabelTreatmentChagne.Text += " [<a href=PORequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
    }
    protected void ButtonReassess_Click(object sender, EventArgs e)
    {
        LabelReassessError.Text = "";
        for (int i = 0; i < dgReassess.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgReassess.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label reqLabel = (System.Web.UI.WebControls.Label)dgReassess.Rows[i].FindControl("lblRequestID");
            System.Web.UI.WebControls.Label eName = (System.Web.UI.WebControls.Label)dgReassess.Rows[i].FindControl("lblErrorName");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgReassess.Rows[i].FindControl("chkProcess");
            System.Web.UI.WebControls.DropDownList dlReason = (System.Web.UI.WebControls.DropDownList)dgReassess.Rows[i].FindControl("dropApproveStatusReason");
            if (chkProc.Checked && dgReassess.Rows[i].Enabled)
            {
                if (dlReason.SelectedValue != "0")
                {
                    GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus();
                    GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(reqLabel.Text), Convert.ToInt32(IdLabel.Text));
                    myStatus.PatientID = myRequest.PatientID;
                    myStatus.RequestID = myRequest.RequestID;
                    myStatus.ReAssess(sessUse.Username, myRequest.Notes);
                    
                    //we must inflate the new status because now we must approve
                    myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(IdLabel.Text), "approve");
                    myStatus.StatusReason = dlReason.SelectedValue;
                    myStatus.Notes = "Automatically logged from reassess request";
                    myStatus.Approve(sessUse.Username);

                    // send email
                    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
                    GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                    myEmail = myPatient.ApprovalEmailPatient(Convert.ToInt32(IdLabel.Text));
                    myEmail.Send(sessUse.Username);
                    if (myPatient.PhysicianCount > 0)
                    {
                        for (int ph = 0; ph < myPatient.PhysicianCount; ph++)
                        {
                            myEmail = myPatient.ApprovalEmailPhysician(Convert.ToInt32(IdLabel.Text), ph);
                            myEmail.Send(sessUse.Username);
                        }
                    }
                    if (myPatient.CPOCount > 0)
                    {
                        for (int cp = 0; cp < myPatient.CPOCount; cp++)
                        {
                            myEmail = myPatient.ApprovalEmailCPO(Convert.ToInt32(IdLabel.Text), cp);
                            myEmail.Send(sessUse.Username);
                        }
                    }
                    dgReassess.Rows[i].Enabled = false;
                }
                else
                {
                    LabelReassessError.Text += "<li>" + eName.Text + "</li>";
                }
            }
        }
        if (LabelReassessError.Text == "")
        {
            dgReassess.Visible = ButtonReassess.Visible = false;
            LabelReassess.Visible = true;
            LabelReassess.Text += " [<a href=PORequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
        else
        {
            LabelReassessError.Visible = true;
            LabelReassessError.Text = "You must select a reason to approve the following patients:" + LabelReassessError.Text;
        }
    }
    protected void ButtonFEF_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgFEF.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgFEF.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label reqLabel = (System.Web.UI.WebControls.Label)dgFEF.Rows[i].FindControl("lblRequestID");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgFEF.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked)
            {
                GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(reqLabel.Text), Convert.ToInt32(IdLabel.Text));
                myRequest.Resolve(sessUse.Username);
            }
        }
        dgFEF.Visible = ButtonFEF.Visible = false;
        LabelFEF.Visible = true;
        LabelFEF.Text += " [<a href=PORequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
    }
    protected void ButtonAE_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgAE.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgAE.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.Label reqLabel = (System.Web.UI.WebControls.Label)dgAE.Rows[i].FindControl("lblRequestID");
            System.Web.UI.WebControls.Label eName = (System.Web.UI.WebControls.Label)dgAE.Rows[i].FindControl("lblErrorName");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgAE.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked && dgAE.Rows[i].Enabled)
            {
                GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(0, Convert.ToInt32(IdLabel.Text));
                GIPAP_Objects.Request myRequest = new GIPAP_Objects.Request(Convert.ToInt32(reqLabel.Text), Convert.ToInt32(IdLabel.Text));
                if (mySae.SAEEmail.Trim() != "")
                {
                    mySae.Event = myRequest.Notes;
                    mySae.LearnedFromPhysician = myRequest.PhysicianRequested;
                    mySae.DateLearned = DateTime.Today;
                    if (myRequest.PhysicianRequested)
                    {
                        mySae.Consent = true;
                    }
                    else
                    {
                        mySae.Consent = false;
                    }
                    mySae.CloseCase = false;
                    bool proc = true;
                    try
                    {
                        GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                        myEmail = mySae.SAEAlertNovartis();
                        myEmail.Send(sessUse.Username);
                        mySae.EmailID = myEmail.EmailID;
                    }
                    catch
                    {
                        LabelAEError.Text += "<li>Problem Sending Email: " + eName.Text + "</li>";
                        proc = false;
                    }
                    try
                    {
                        GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                        myEmail = mySae.SAEAlertPhysician();
                        myEmail.Send(sessUse.Username);
                        mySae.PhyEmailID = myEmail.EmailID;
                    }
                    catch { }
                    if (proc)
                    {
                        mySae.Create(sessUse.Username);
                        myRequest.Resolve(sessUse.Username);
                        dgAE.Rows[i].Enabled = false;
                    }
                }
                else
                {
                    LabelAEError.Text += "<li>No Country AE Email: " + eName.Text + "</li>";
                }
            }
        }
        if (LabelAEError.Text == "")
        {
            dgAE.Visible = ButtonAE.Visible = false;
            LabelAE.Visible = true;
            LabelAE.Text += " [<a href=PORequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
        else
        {
            LabelAEError.Visible = true;
            LabelAEError.Text = "Errors were encountered on the following patients:" + LabelAEError.Text;
        }
    }
}
