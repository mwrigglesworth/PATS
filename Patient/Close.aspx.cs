using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Close : System.Web.UI.Page
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
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "close");
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

            if (sessUse.Role == "Physician")
            {
                dropCloseStatusReason.Items.Remove("No re-evaluation information provided");
                dropCloseStatusReason.Items.Remove("Duplicate Patient");
                PanelCloseReceive.Visible = false;
                if (sessUse.latamlist.Contains(sessUse.CountryID))
                {
                    LabelPhysWarning.Text +="<br /> POR FAVOR: No proveainformación personal de pacientes (talcomo el nombre), dado queestosdetallesseránincluidosen un reporte de eventoadverso para Novartis.";
                }
            }
            if (sessUse.Role != "TMFUser")
            {
                LabelCloseDefinition.Visible = true;
            }
            //FINISH BUTTON DISABLER...
            sbDisable.Append(Page.GetPostBackEventReference(ButtonClose));
            sbDisable.Append(";");
            //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

            ButtonClose.Attributes.Add("onclick", sbDisable.ToString());

            for (int i = 0; i < 5; i++)
            {
                dropYear.Items.Add((DateTime.Now.Year - i).ToString());
            }
            //see if we are processing a request
            if (myRequest.RequestID != 0)
            {
                try
                {
                    dropCloseStatusReason.SelectedValue = myRequest.StatusReason;
                    //8.0 added code
                    if (dropCloseStatusReason.SelectedIndex == 0)
                    {
                        txtClose.Text = myRequest.StatusReason;
                    }
                }
                catch
                {
                    txtClose.Text = myRequest.StatusReason;
                }
                txtClose.Text += myRequest.Notes;
                rblstClosePhysReq.SelectedIndex = Convert.ToInt32(myRequest.PhysicianRequested);
                dropCloseReceived.Items.Add("PATS");
                dropCloseReceived.SelectedValue = myRequest.ReceivedBy;
                if (txtClose.Text != "")
                {
                    PanelCloseReason.Visible = true;
                    /*dont need this anymore if (myRequest.AERelated)
                    {
                        LabelPOWarningClose.Visible = true;
                    }*/
                }
                ButtonRemoveRequest.Visible = true;
                if (myStatus.GipapStatus != "Active")
                {
                    PanelObsolete.Visible = true;
                    ButtonClose.Enabled = false;
                }
            }
        }
    }
    protected void ButtonClose_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            myStatus.StatusReason = dropCloseStatusReason.SelectedValue;
            myStatus.Notes = txtClose.Text;
            if (sessUse.Role == "Physician")
            {
                myStatus.PhysicianRequested = true;
                myStatus.ReceivedBy = "PATS";
            }
            else
            {
                myStatus.PhysicianRequested = Convert.ToBoolean(rblstClosePhysReq.SelectedIndex);
                if (myStatus.PhysicianRequested)
                {
                    myStatus.ReceivedBy = dropCloseReceived.SelectedValue;
                }
                else
                {
                    myStatus.ReceivedBy = "";
                }
            }
            myStatus.AERelated = (dropCloseStatusReason.SelectedValue == "Patient has passed away" || dropCloseStatusReason.SelectedValue == "Patient Not Responding to Treatment" || dropCloseStatusReason.SelectedValue == "Intolerance" || dropCloseStatusReason.SelectedValue == "Pregnancy");
            if (sessUse.Role == "TMFUser")
            {
                if (myRequest.RequestID != 0) //this is for if you are processing a close request.  next we do ae request if applicable
                {
                    myStatus.RequestID = myRequest.RequestID;
                }
                //ae request
                int rid = 0;
                if (myStatus.AERelated && (myRequest.RequestID == 0 || !myRequest.AERelated))
                {
                    myRequest.PatientID = myStatus.PatientID;
                    myRequest.PhysicianRequested = myStatus.PhysicianRequested;
                    myRequest.Notes = myStatus.Notes;
                    //myRequest.AdverseEvent(sessUse.Username);
                    //rid = myRequest.RequestID;

                    /**********Sept 2014, New AE Logic***************
                        * No more AE requests*/
                    GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(0, myStatus.PatientID);
                    mySae.Event = "Case Closure requested";
                    if (dropCloseStatusReason.SelectedValue != "Other")
                        mySae.Event += ", reason given \"" + dropCloseStatusReason.SelectedValue.ToString();
                    mySae.Event += "\", with the following note:\n";
                    mySae.Event += this.txtClose.Text;
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
                            this.LabelAEErrorClose.Text =  "Date AE Reported to Max was not valid";
                            this.LabelAEErrorClose.Visible = true;
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
                        this.LabelAEErrorClose.Text = "There was a problem sending the email report.  This AE was not logged.";
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
                myStatus.Close(sessUse.Username);
                string rd;
                if (myStatus.StatusReason == "Patient has passed away")
                {
                    rd = "PatientEmail.aspx?mailType=CloseEmailPhysician&a=close&pcount=0&choice=" + myStatus.PatientID.ToString();
                }
                else
                {
                    rd = "PatientEmail.aspx?mailType=CloseEmailPatient&a=close&choice=" + myStatus.PatientID.ToString();
                }
                if (rid != 0)
                {
                    rd += "&rid=" + rid.ToString();
                }
                Response.Redirect(rd);
            }
            else
            {
                myRequest.PatientID = myStatus.PatientID;
                myRequest.StatusReason = dropCloseStatusReason.SelectedValue;
                myRequest.PhysicianRequested = myStatus.PhysicianRequested;
                myRequest.ReceivedBy = myStatus.ReceivedBy;
                myRequest.AERelated = myStatus.AERelated;
                myRequest.Notes = txtClose.Text;
                if (myRequest.AERelated)
                {
                   // myRequest.AdverseEvent(sessUse.Username);
                  //  myRequest.AERequestID = myRequest.RequestID;

                    /**********Sept 2014, New AE Logic***************
                        * No more AE requests*/
                    GIPAP_Objects.SAE mySae = new GIPAP_Objects.SAE(0, myStatus.PatientID);
                    mySae.Event = "Case Closure requested";
                    if (dropCloseStatusReason.SelectedValue != "Other")
                        mySae.Event += ", reason given \"" + dropCloseStatusReason.SelectedValue.ToString();
                    mySae.Event += "\", with the following note:\n";
                    mySae.Event += this.txtClose.Text;
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
                            this.LabelAEErrorClose.Text = "Date AE Reported to Max was not valid";
                            this.LabelAEErrorClose.Visible = true;
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
                        this.LabelAEErrorClose.Text = "There was a problem sending the email report.  This AE was not logged.";
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
                myRequest.Close(sessUse.Username);
                Response.Redirect("Patientinfo.aspx?a=closerequest&choice=" + myRequest.PatientID.ToString());
            }
        }
    }
    protected void ButtonNoClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
    protected void dropCloseStatusReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCloseStatusReason.SelectedValue == "Patient has passed away" || dropCloseStatusReason.SelectedValue == "Patient Not Responding to Treatment" || dropCloseStatusReason.SelectedValue == "Intolerance" || dropCloseStatusReason.SelectedValue == "Pregnancy" || dropCloseStatusReason.SelectedValue == "Other")//other makes the box show, but is not an AE
        {
            PanelCloseReason.Visible = true;
            if ((sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation") && dropCloseStatusReason.SelectedValue != "Other")
            {
                LabelPOWarningClose.Visible = true;
                LabelCloseDefinition.Visible = false;
                PanelAEDateLearned.Visible = true;
                LabelPhysWarning.Visible = false;
            }
            if (sessUse.Role == "Physician")
            {
                PanelAEDateLearned.Visible = false;
                LabelPOWarningClose.Visible = false;
                LabelCloseDefinition.Visible = true;
                LabelPhysWarning.Visible = true;
            }
        }
        else
        {
            PanelCloseReason.Visible = false;
            PanelAEDateLearned.Visible = false;
            LabelPOWarningClose.Visible = false;
            LabelCloseDefinition.Visible = false;
            LabelPhysWarning.Visible = false;
            PanelAEDateLearned.Visible = false;
        }
    }
    protected void ButtonRemoveRequest_Click(object sender, EventArgs e)
    {
        myRequest.Resolve(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=removerequest&choice=" + myStatus.PatientID.ToString());
    }
}
