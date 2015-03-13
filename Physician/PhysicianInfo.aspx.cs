using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Physician_PhysicianInfo : System.Web.UI.Page
{
    GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();
    GIPAP_Objects.User sessUse;
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        string a = "";
        try
        {
            a = Request.QueryString["a"].ToString();
        }
        catch { }
        if (a != "")
        {
            PanelAlert.Visible = true;
            if (a == "edit")
            {
                LabelAlert.Text = "Physician information has been updated";
            }
            else if (a == "mail")
            {
                LabelAlert.Text = "Physician email has been sent";
            }
            else if (a == "approve")
            {
                LabelAlert.Text = "Physician has been approved";
            }
            else if (a == "transfer")
            {
                LabelAlert.Text = "Patients have been transferred to this physician";
            }
            else if (a == "notransfer")
            {
                LabelAlert.Text = "Patients were NOT transferred";
            }

            else if (a == "add")
            {
                LabelAlert.Text = "Physician has been added to PATS";
            }
        }
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myPhysician = new GIPAP_Objects.Physician(choice, sessUse.Role);
        LabelClinic.Text = myPhysician.ClinicName;
        LabelApprove.Text = myPhysician.ApprovedLabel();
        if (myPhysician.Approved == 1)
        {
            lbApprove.Text = "[Unapprove]";
        }
        else
        {
            lbApprove.Text = "[Approve]";
        }
        if (sessUse.Role == "TMFUser")
        {
            lbApprove.Visible = true;
        }
        LabelMou.Text = myPhysician.MOULabel();
        LabelNOA.Text = myPhysician.NOALabel();

        LabelList.Text = "<a href=EditPhysician.aspx?choice=" + choice.ToString() + ">Edit Physician Info</a><br><br>";
        if (sessUse.Role == "TMFUser")
        {
            LabelList.Text += "<a href=TransferPatients.aspx?choice=" + choice.ToString() + ">Mass Patient Transfer</a><br><br>";
        }
        if (myPhysician.GetPhysicianDataSets("patientsneedingreapproval").Tables[0].Rows.Count > 0)
        {
            LabelList.Text += "<a href=ReapprovalsByPhysician.aspx?choice=" + myPhysician.PhysicianID.ToString() + ">Patients Needing Reapproval</a><br><br>";
        }
        else
        {
            LabelList.Text += "<i><font color=gray>No Patients needing Reapproval</font></i><br><br>";
        } 

        LabelActions.Text = "<a href=PhysicianDataSets.aspx?choice=" + choice.ToString() + "&dset=allpatients>Patients</a><br><br>";
        LabelActions.Text += "<a href=PhysicianDataSets.aspx?choice=" + choice.ToString() + "&dset=profilechanges>Profile Changes</a><br><br>";
        LabelActions.Text += "<a href=PhysicianDataSets.aspx?choice=" + choice.ToString() + "&dset=physicianemails>Emails</a><br><br>";
        LabelActions.Text += "<a href=PatientReport.aspx?choice=" + choice.ToString() + ">Patient Report</a><br><br>";
    }

    protected void lbApprove_Click(object sender, EventArgs e)
    {
        if (myPhysician.Approved == 1)
        {
            myPhysician.UnApprove(sessUse.Username);
            lbApprove.Text = "[Approve]";
            myPhysician = new GIPAP_Objects.Physician(choice, sessUse.Role);
            LabelApprove.Text = myPhysician.ApprovedLabel();
        }
        else
        {
            myPhysician.Approve(sessUse.Username);
            lbApprove.Text = "[Unapprove]";
            Response.Redirect("PhysicianEmail.aspx?mailType=PhysicianApprovalEmailPhysician&choice=" + myPhysician.PhysicianID.ToString());
        }
    }

    protected void lbClinics_Click(object sender, EventArgs e)
    {
        PanelDisplay.Visible = false;
        PanelUpdate.Visible = true;
        LabelHeader.Text = "Clinics";
        ButtonUpdateClinic.Visible = true;
        cblstPersonel.DataSource = myPhysician.GetAvailablePhysicianClinics();
        cblstPersonel.DataTextField = "clinicname";
        cblstPersonel.DataValueField = "clinicid";
        cblstPersonel.DataBind();
        if (myPhysician.ClinicDT.Rows.Count > 0)
        {
            for (int i = 0; i < myPhysician.ClinicDT.Rows.Count; i++)
            {
                for (int i2 = 0; i2 < cblstPersonel.Items.Count; i2++)
                {
                    string mw = myPhysician.ClinicDT.Rows[i]["clinicid"].ToString();
                    if (cblstPersonel.Items[i2].Value == mw)
                    {
                        cblstPersonel.Items[i2].Selected = true;
                    }
                }
            }
        }
    }
    protected void ButtonUpdateClinic_Click(object sender, EventArgs e)
    {
        myPhysician.UpdatePhysicianClinics(cblstPersonel);
        Response.Redirect(Request.Url.ToString());
    }
}
