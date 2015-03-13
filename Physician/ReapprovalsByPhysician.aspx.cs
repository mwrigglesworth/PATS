using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Threading;

public partial class Physician_ReapprovalsByPhysician : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;
    string noaError;
    GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        DataSet ds = new DataSet();
        myPhysician = new GIPAP_Objects.Physician(choice, sessUse.Role);

       
            ds = myPhysician.GetPhysicianDataSets("patientsneedingreapproval");
            LabelResultCount.Text = myPhysician.FirstName + " " + myPhysician.LastName + " - " + ds.Tables[0].Rows.Count.ToString() + " Patients Needing Reapproval";
            if (!Page.IsPostBack)
            {
                dgReapps.DataSource = ds.Tables[0];
                dgReapps.DataBind();
                this.SetbuttonDisabler(ButtonReapprove);
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
        //MasterPage ct100 = (MasterPage)FindControl("ctl00");
        //ContentPlaceHolder content1 = (ContentPlaceHolder)ct100.FindControl("ContentPlaceHolder1");
        //GridView Reapps =(GridView)content1.FindControl("dgReapps");
        noaError = "";
        for (int i = 0; i < dgReapps.Rows.Count; i++)
        {
            
            //string ClientReapps = ((GridViewRow)Reapps.Rows[i]).ClientID;
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgReapps.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.CheckBox chkProc = (System.Web.UI.WebControls.CheckBox)dgReapps.Rows[i].FindControl("chkProcess");
            System.Web.UI.WebControls.DropDownList dropRec = (System.Web.UI.WebControls.DropDownList)dgReapps.Rows[i].FindControl("dropReceived");
            if (chkProc.Checked)
            {
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(IdLabel.Text), "reapprove");
                if (myStatus.AutoApprove)
                {
                    if (myStatus.EndDate < DateTime.Today)
                    {
                        myStatus.StartDate = DateTime.Today;
                        myStatus.EndDate = DateTime.Today.AddDays(119);
                    }
                    else
                    {
                        myStatus.StartDate = myStatus.EndDate.AddDays(1);
                        myStatus.EndDate = myStatus.StartDate.AddDays(119);
                    }
                    if (!myStatus.NoaFEFNeeded)
                    {
                        myStatus.AutoApprove = true;
                        myStatus.RestartTreatment = true;
                        myStatus.PhysicianRequested = true;
                        myStatus.ReceivedBy = dropRec.SelectedValue;
                        myStatus.ReApprove(sessUse.Username);
                        // send email
                        if (sessUse.Role == "TMFUser")
                        {
                            GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient(/*Convert.ToInt32(IdLabel.Text), "TMFUser"*/);
                            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                            myEmail = myPatient.ReApprovalEmailPatient(Convert.ToInt32(IdLabel.Text));
                            try
                            {
                                myEmail.Send(sessUse.Username);
                            }
                            catch (Exception ex)
                            {
                                //throw new Exception(ex.Message);
                                if (ex.Message == "Service not available, closing transmission channel. The server response was: 4.4.2 Message submission rate for this client has exceeded the configured limit")
                                {
                                    System.Threading.Thread.Sleep(30000);
                                }
                                else
                                {
                                    throw new Exception(ex.Message);
                                }
                            }
                            if (myPatient.PhysicianCount > 0)
                            {
                                for (int ph = 0; ph < myPatient.PhysicianCount; ph++)
                                {
                                    myEmail = myPatient.ReApprovalEmailPhysician(ph);
                                    try
                                    {
                                        myEmail.Send(sessUse.Username);
                                    }
                                    catch (Exception ex)
                                    {
                                        //throw new Exception(ex.Message);
                                        if (ex.Message == "Service not available, closing transmission channel. The server response was: 4.4.2 Message submission rate for this client has exceeded the configured limit")
                                        {
                                            System.Threading.Thread.Sleep(30000);
                                        }
                                        else
                                        {
                                            throw new Exception(ex.Message);
                                        }
                                    }
                                }
                            }
                            if (myPatient.CPOCount > 0)
                            {
                                for (int cp = 0; cp < myPatient.CPOCount; cp++)
                                {
                                    myEmail = myPatient.ReApprovalEmailCPO(cp);
                                    try
                                    {
                                        myEmail.Send(sessUse.Username);
                                    }
                                    catch (Exception ex)
                                    {
                                        //throw new Exception(ex.Message);
                                        if (ex.Message == "Service not available, closing transmission channel. The server response was: 4.4.2 Message submission rate for this client has exceeded the configured limit")
                                        {
                                            System.Threading.Thread.Sleep(30000);
                                        }
                                        else
                                        {
                                            throw new Exception(ex.Message);
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        noaError += "<li>" + myStatus.PIN;
                    }
                }
            }
        }

        //now figure out where to go
        if (noaError == "")
        {
            dgReapps.Visible = ButtonReapprove.Visible = false;
            LabelReapprove.Visible = true;
            LabelReapprove.Text += " [<a href=ReapprovalsByPhysician.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
        else
        {
            dgReapps.Visible = ButtonReapprove.Visible = false;
            LabelError.Visible = true;
            LabelError.Text = "The following patinets could not be reapproved because they require a new FEF:" + noaError + "<br>[<a href=ReapprovalsByPhysician.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
        }
    }

    protected void dgReapps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        dgReapps.PageIndex = e.NewPageIndex;
        dgReapps.DataBind();
        LabelResultCount.Text = (e.NewPageIndex) * dgReapps.PageSize + "-" + ((e.NewPageIndex + 1) < dgReapps.PageCount ? (e.NewPageIndex + 1) * dgReapps.PageSize : (e.NewPageIndex) * dgReapps.PageSize + dgReapps.Rows.Count) + " Records";

       
    }
}
