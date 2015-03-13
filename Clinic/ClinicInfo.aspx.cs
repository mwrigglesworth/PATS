using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Clinic_ClinicInfo : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Clinic myClinic = new GIPAP_Objects.Clinic();
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
                LabelAlert.Text = "Clinic information has been updated";
            }
            else if (a == "add")
            {
                LabelAlert.Text = "Clinic has been added to PATS";
            }
        }
        /////////
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);

        myClinic = new GIPAP_Objects.Clinic(choice, sessUse.Role);
        LabelApprove.Text = myClinic.ApprovedInfo(sessUse.Role);
        LabelAdmin.Text = myClinic.AdminInfo(sessUse.Role);
        if (myClinic.Approved == 1)
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
        dgCnotes.DataSource = myClinic.ClinicDS.Tables[5];
        dgCnotes.DataBind();
        txtNote.Attributes.Add("onfocus", "if (this.value=='Add Note'){ this.value='' }");


        LabelList.Text = "<a href=EditClinic.aspx?choice=" + choice.ToString() + ">Edit Clinic Info</a><br><br>";

        LabelActions.Text = "<a href=ClinicDataSets.aspx?choice=" + choice.ToString() + "&dset=physicians>Physicians</a><br><br>";
        LabelActions.Text += "<a href=ClinicDataSets.aspx?choice=" + choice.ToString() + "&dset=patients>Patients</a><br><br>";
        if (sessUse.Role == "MaxStation" || sessUse.Role == "TMFUser")
        {
            LabelActions.Text += "<a href=FullPageClinicReport.aspx?choice=" + choice.ToString() + "&dset=patients>Patient Report</a><br><br>";
        }
    }
    protected void lbApprove_Click(object sender, EventArgs e)
    {
        if (myClinic.Approved == 1)
        {
            myClinic.UnApprove(sessUse.Username);
            lbApprove.Text = "[Approve]";
        }
        else
        {
            myClinic.Approve(sessUse.Username);
            lbApprove.Text = "[Unapprove]";
        }
        Response.Redirect("ClinicInfo.aspx?choice=" + myClinic.ClinicID.ToString());
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        if (txtNote.Text.Trim() != "" && txtNote.Text.Trim() != "Add Note")
        {
            myClinic.AddClinicNote(sessUse.Username, txtNote.Text.Trim());
            Response.Redirect("ClinicInfo.aspx?choice=" + choice.ToString());
        }
    }
}
