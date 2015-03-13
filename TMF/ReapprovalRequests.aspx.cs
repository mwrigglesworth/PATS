using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class TMF_ReapprovalRequests : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;
    string noaError;

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
        DataSet ds = new DataSet();

        if (!Page.IsPostBack)
        {
            ds = sessUse.getDatasets(choice, "reapprovalrequests");
            LabelResultCount.Text = ds.Tables[1].Rows[0]["poname"].ToString() + " - " + ds.Tables[0].Rows.Count.ToString() + " Reapproval Requests";
            dgReapps.DataSource = ds.Tables[0];
            dgReapps.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["countryid"]) == 76 && !Convert.ToBoolean(ds.Tables[0].Rows[i]["pickedup"]) && Convert.ToInt32(ds.Tables[0].Rows[i]["donationlength"]) == 370 && Convert.ToDateTime(ds.Tables[0].Rows[i]["activecreatedate"]) > Convert.ToDateTime("11/10/2013"))
                    {
                        (dgReapps.Rows[i].FindControl("ImagePickedNotPickedUp") as Image).Visible = true;
                        (dgReapps.Rows[i].FindControl("chkProcess") as CheckBox).Enabled = false;
                    }
                }
                this.SetbuttonDisabler(ButtonReapprove);
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
    protected void ButtonReapprove_Click(object sender, EventArgs e)
    {
        noaError = "";
        for (int i = 0; i < dgReapps.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgReapps.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgReapps.Rows[i].FindControl("chkProcess");
            if (chkProc.Checked)
            {
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(IdLabel.Text), "reapprove");
                if (myStatus.StartDate < DateTime.Today)
                {
                    myStatus.StartDate = DateTime.Today;
                    //4 months all
                    myStatus.EndDate = DateTime.Today.AddDays(119);
                }
                if (!myStatus.NoaFEFNeeded)
                {
                    myStatus.AutoApprove = true;
                    myStatus.ReApprove(sessUse.Username);
                    // send email
                    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient(/*Convert.ToInt32(IdLabel.Text), "TMFUser"*/);
                    GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                    myEmail = myPatient.ReApprovalEmailPatient(Convert.ToInt32(IdLabel.Text));
                    myEmail.Send(sessUse.Username);
                    if (myPatient.PhysicianCount > 0)
                    {
                        for (int ph = 0; ph < myPatient.PhysicianCount; ph++)
                        {
                            myEmail = myPatient.ReApprovalEmailPhysician(ph);
                            myEmail.Send(sessUse.Username);
                        }
                    }
                    if (myPatient.CPOCount > 0)
                    {
                        for (int cp = 0; cp < myPatient.CPOCount; cp++)
                        {
                            myEmail = myPatient.ReApprovalEmailCPO(cp);
                            myEmail.Send(sessUse.Username);
                        }
                    }
                }
                else
                {
                    noaError += "<li>" + myStatus.PIN;
                }
            }
        }
        //now figure out where to go
        if (noaError == "")
        {
            dgReapps.Visible = ButtonReapprove.Visible = false;
            LabelReapprove.Visible = true;
            LabelReapprove.Text += " [<a href=ReapprovalRequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
        else
        {
            dgReapps.Visible = ButtonReapprove.Visible = false;
            LabelError.Visible = true;
            LabelError.Text = "The following patinets could not be reapproved because they require a new FEF:" + noaError + "<br>[<a href=ReapprovalRequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
            /*DataSet ds = sessUse.getDatasets(choice, "reapprovalrequests");
            LabelResultCount.Text = ds.Tables[1].Rows[0]["poname"].ToString() + " - " + ds.Tables[0].Rows.Count.ToString() + " Reapproval Requests";
            dgReapps.DataSource = ds.Tables[0];
            dgReapps.DataBind();*/
        }
    }
}
