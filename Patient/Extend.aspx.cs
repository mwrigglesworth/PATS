using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Extend : System.Web.UI.Page
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
        myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(Request.QueryString["choice"]), "extend");
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

            LabelExtendError.Visible = false;
            LabelEditStart.Text = myStatus.StartDate.Day.ToString() + " " + myStatus.StartDate.ToString("y");
            LabelEditEnd.Text = myStatus.EndDate.Day.ToString() + " " + myStatus.EndDate.ToString("y");
            CalendarEditEnd.Visible = false;
            CalendarEditEnd.SelectedDate = myStatus.EndDate;
            CalendarEditEnd.VisibleDate = myStatus.EndDate;
            //FINISH BUTTON DISABLER...
            sbDisable.Append(Page.GetPostBackEventReference(ButtonExtend));
            sbDisable.Append(";");
            //GetPostBackEventReference obtains a reference to a client-side script function that causes the server to post back to the page.

            ButtonExtend.Attributes.Add("onclick", sbDisable.ToString());

            //see if we are processing a request
            if (myRequest.RequestID != 0)
            {
                LabelEditStart.Text = myRequest.StartDate.Day.ToString() + " " + myRequest.StartDate.ToString("y");
                LabelEditEnd.Text = myRequest.EndDate.Day.ToString() + " " + myRequest.EndDate.ToString("y");
                CalendarEditEnd.Visible = false;
                CalendarEditEnd.SelectedDate = myRequest.EndDate;
                CalendarEditEnd.VisibleDate = myRequest.EndDate;
                /*reason*/
                txtExtendReason.Text = myRequest.StatusReason;
                rblstExtendPhysReq.SelectedIndex = Convert.ToInt32(myRequest.PhysicianRequested);
                dropExtendReceived.SelectedValue = myRequest.ReceivedBy;
                ButtonRemoveRequest.Visible = true;
                ButtonExtend.Enabled = true;
                if (myStatus.GipapStatus != "Active")
                {
                    PanelObsolete.Visible = true;
                    ButtonExtend.Enabled = false;
                }
            }
        }
    }
    //*****************************************************************************************
    private void showCalendarDate(System.Web.UI.WebControls.Calendar myCal, System.Web.UI.WebControls.Label myLab)
    {
        myLab.Text = myCal.SelectedDate.Day.ToString() + " " + myCal.SelectedDate.ToString("y");
        myCal.Visible = false;
    }
    protected void lbExtendEnd_Click(object sender, EventArgs e)
    {
        CalendarEditEnd.Visible = true;
    }
    protected void CalendarEditEnd_SelectionChanged(object sender, EventArgs e)
    {
        if (CalendarEditEnd.SelectedDate >= myStatus.EndDate.AddDays(15))
        {
            this.showCalendarDate(CalendarEditEnd, LabelEditEnd);
            ButtonExtend.Enabled = true;
            LabelExtendError.Visible = false;
        }
        else
        {
            LabelExtendError.Visible = true;
        }
    }
    protected void ButtonExtend_Click(object sender, EventArgs e)
    {
        if (sessUse.Role == "TMFUser")
        {
            if (myRequest.RequestID != 0)
            {
                myStatus.RequestID = myRequest.RequestID;
            }
            myStatus.StatusReason = txtExtendReason.Text;
            myStatus.EndDate = CalendarEditEnd.SelectedDate;
            myStatus.PhysicianRequested = Convert.ToBoolean(rblstExtendPhysReq.SelectedIndex);
            if (myStatus.PhysicianRequested)
            {
                myStatus.ReceivedBy = dropExtendReceived.SelectedValue;
            }
            else
            {
                myStatus.ReceivedBy = "";
            }
            myStatus.Extend(sessUse.Username);
            Response.Redirect("PatientEmail.aspx?mailType=ExtentionEmailPatient&a=extend&choice=" + myStatus.PatientID.ToString());
        }
        else
        {
            myRequest.PatientID = myStatus.PatientID;
            myRequest.StatusReason = txtExtendReason.Text;
            myRequest.EndDate = CalendarEditEnd.SelectedDate;
            myRequest.PhysicianRequested = Convert.ToBoolean(rblstExtendPhysReq.SelectedIndex);
            if (myRequest.PhysicianRequested)
            {
                myRequest.ReceivedBy = dropExtendReceived.SelectedValue;
            }
            else
            {
                myRequest.ReceivedBy = "";
            }
            myRequest.Extend(sessUse.Username);
            Response.Redirect("Patientinfo.aspx?a=extendrequest&choice=" + myRequest.PatientID.ToString());
        }
    }
    protected void ButtonCancelExtend_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myStatus.PatientID.ToString());
    }
    protected void ButtonRemoveRequest_Click(object sender, EventArgs e)
    {
        myRequest.Resolve(sessUse.Username);
        Response.Redirect("PatientInfo.aspx?a=removerequest&choice=" + myStatus.PatientID.ToString());
    }
}
