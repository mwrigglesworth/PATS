using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class TMF_PhysicianTransferRequests : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        DataSet ds = new DataSet();

        if (!Page.IsPostBack)
        {
            ds = sessUse.getDatasets(choice, "physiciantransferrequests");
            LabelResultCount.Text = ds.Tables[1].Rows[0]["poname"].ToString() + " - " + ds.Tables[0].Rows.Count.ToString() + " Transfer Requests";
            dgRequests.DataSource = ds.Tables[0];
            dgRequests.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    GIPAP_Objects.AddRemove myAR = new GIPAP_Objects.AddRemove();
                    System.Web.UI.WebControls.Label PatientIdLabel = (System.Web.UI.WebControls.Label)dgRequests.Rows[i].FindControl("lblPatientID");
                    DataSet dsSug = myAR.getPOSuggestions(Convert.ToInt32(PatientIdLabel.Text));
                    if (dsSug.Tables[0].Rows.Count > 0)
                    {
                        GridViewRow gvRow = dgRequests.Rows[i];
                        (gvRow.FindControl("PanelPhysTrans") as Panel).Visible = true;
                        (gvRow.FindControl("LabelPhysTrans") as Label).Text = "<b>Does the PO need to be changed?</b><br /><br />Suggestions: ";
                        for (int k = 0; k < dsSug.Tables[0].Rows.Count; k++)
                        {
                            (gvRow.FindControl("LabelPhysTrans") as Label).Text += dsSug.Tables[0].Rows[k]["suggestionname"].ToString();
                        }
                    }
                }
            }
            this.SetbuttonDisabler(ButtonProcess);
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
    protected void ButtonProcess_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < dgRequests.Rows.Count; i++)
        {
            System.Web.UI.WebControls.Label IdLabel = (System.Web.UI.WebControls.Label)dgRequests.Rows[i].FindControl("lblTransferID");
            System.Web.UI.WebControls.Label PatientIdLabel = (System.Web.UI.WebControls.Label)dgRequests.Rows[i].FindControl("lblPatientID");
            System.Web.UI.WebControls.RadioButtonList rbApp = (System.Web.UI.WebControls.RadioButtonList)dgRequests.Rows[i].FindControl("rblstApprove");
            if (rbApp.SelectedIndex != -1)
            {
                GIPAP_Objects.AddRemove myAR = new GIPAP_Objects.AddRemove();
                myAR.ProcessPhysicianTransferRequest(sessUse.Username, Convert.ToInt32(IdLabel.Text), rbApp.Items[0].Selected);
                GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
                myEmail = myAR.PhysicianTransferEmail(Convert.ToInt32(PatientIdLabel.Text), "delete", 0);
                myEmail.Send(sessUse.Username);
                for (int a = 1; a < myAR.PersonCount; a++)
                {
                    myEmail = myAR.PhysicianTransferEmail(Convert.ToInt32(PatientIdLabel.Text), "delete", a);
                    myEmail.Send(sessUse.Username);
                }
                myEmail = myAR.PhysicianTransferEmail(Convert.ToInt32(PatientIdLabel.Text), "create", 0);
                myEmail.Send(sessUse.Username);
                for (int a = 1; a < myAR.PersonCount; a++)
                {
                    myEmail = myAR.PhysicianTransferEmail(Convert.ToInt32(PatientIdLabel.Text), "create", a);
                    myEmail.Send(sessUse.Username);
                }
                myEmail = myAR.PhysicianTransferEmail(Convert.ToInt32(PatientIdLabel.Text), "novartis", 0);
                myEmail.Send(sessUse.Username);
                for (int a = 1; a < myAR.PersonCount; a++)
                {
                    myEmail = myAR.PhysicianTransferEmail(Convert.ToInt32(PatientIdLabel.Text), "novartis", a);
                    myEmail.Send(sessUse.Username);
                }
            }
        }
        dgRequests.Visible = ButtonProcess.Visible = false;
        LabelRequest.Visible = true;
        LabelRequest.Text += " [<a href=PhysicianTransferRequests.aspx?choice=" + choice.ToString() + ">Reload Page</a>]";
    }
}
