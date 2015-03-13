using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Patient_PatientSearch : System.Web.UI.Page
{
    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        DataSet ds = myPatient.getPatientSearchDropDowns(sessUse.UserID, sessUse.Role);

        if (sessUse.Role == "MaxStation")
        {
            LabelMax.Visible = false;
            dropMax.Visible = false;
        }

        if (!Page.IsPostBack)
        {
            this.FillDropBox(dropCountry, "CountryName", "CountryID", ds.Tables[0]);
            this.FillDropBox(dropPhysician, "PhysicianName", "PersonID", ds.Tables[1]);
            this.FillDropBox(dropSocial, "POName", "PersonID", ds.Tables[2]);
            this.FillDropBox(dropMax, "maxstation", "PersonID", ds.Tables[3]);
            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 100; i--)
            {
                dropBirthYear.Items.Add(i.ToString());
                dropBirthYearThru.Items.Add(i.ToString());
                dropIntakeYear.Items.Add(i.ToString());
                dropIntakeYearThru.Items.Add(i.ToString());
                dropIAYear.Items.Add(i.ToString());
                dropIAYearThru.Items.Add(i.ToString());
                dropStartYear.Items.Add(i.ToString());
                dropStartYearThru.Items.Add(i.ToString());
                dropEndYear.Items.Add(i.ToString());
                dropEndYearThru.Items.Add(i.ToString());
                dropCloseYear.Items.Add(i.ToString());
                DropCloseYearThru.Items.Add(i.ToString());
                dropDiagYear.Items.Add(i.ToString());
                dropDiagYearThru.Items.Add(i.ToString());
            }
        }
    }
    //**********************************************************************************************************************
    private void FillDropBox(System.Web.UI.WebControls.DropDownList drop, string dtfield, string dvfield, DataTable dt)
    {
        drop.DataSource = dt;
        drop.DataTextField = dtfield;
        drop.DataValueField = dvfield;
        drop.DataBind();
        drop.Items.Insert(0, "Select One");
    }


    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        string extra = "";
        try
        {
            string min = dropBirthMonth.SelectedItem.Value + "/" + dropBirthDay.SelectedItem.Text + "/" + dropBirthYear.SelectedItem.Text;
            Convert.ToDateTime(min);
            try
            {
                extra += "BirthDate >= '" + min + "' and ";
                string max = dropBirthMonthThru.SelectedItem.Value + "/" + dropBirthDayThru.SelectedItem.Text + "/" + dropBirthYearThru.SelectedItem.Text;
                Convert.ToDateTime(max);
                extra += "Birthdate <= '" + max + "' and ";
            }
            catch
            {
                extra += "BirthDate = '" + min + "' and ";
            }
        }
        catch { }

        try
        {
            string min = dropIntakeMonth.SelectedItem.Value + "/" + dropIntakeDay.SelectedItem.Text + "/" + dropIntakeYear.SelectedItem.Text;
            DateTime dMin = Convert.ToDateTime(min);
            extra += "IntakeDate >= '" + min + "' and ";
            try
            {
                string max = dropIntakeMonthThru.SelectedItem.Value + "/" + DropIntakeDayThru.SelectedItem.Text + "/" + dropIntakeYearThru.SelectedItem.Text;
                DateTime dMax = Convert.ToDateTime(max).AddDays(1);
                extra += "IntakeDate < '" + dMax.Month.ToString() + "/" + dMax.Day.ToString() + "/" + dMax.Year.ToString() + "' and ";
            }
            catch
            {
                dMin = dMin.AddDays(1);
                extra += "IntakeDate < '" + dMin.Month.ToString() + "/" + dMin.Day.ToString() + "/" + dMin.Year.ToString() + "' and ";
            }
        }
        catch { }

        try
        {
            string min = dropIAMonth.SelectedItem.Value + "/" + dropIADay.SelectedItem.Text + "/" + dropIAYear.SelectedItem.Text;
            DateTime dMin = Convert.ToDateTime(min);
            extra += "IADate >= '" + min + "' and ";
            try
            {
                string max = DropIAMonthThru.SelectedItem.Value + "/" + DropIADayThru.SelectedItem.Text + "/" + dropIAYearThru.SelectedItem.Text;
                DateTime dMax = Convert.ToDateTime(max).AddDays(1);
                extra += "IADate < '" + dMax.Month.ToString() + "/" + dMax.Day.ToString() + "/" + dMax.Year.ToString() + "' and ";
            }
            catch
            {
                dMin = dMin.AddDays(1);
                extra += "IADate < '" + dMin.Month.ToString() + "/" + dMin.Day.ToString() + "/" + dMin.Year.ToString() + "' and ";
            }
        }
        catch { }

        try
        {
            string min = dropEndMonth.SelectedItem.Value + "/" + dropEndDay.SelectedItem.Text + "/" + dropEndYear.SelectedItem.Text;
            Convert.ToDateTime(min);
            try
            {
                extra += "EndDate >= '" + min + "' and ";
                string max = dropEndMonthThru.SelectedItem.Value + "/" + dropEndDayThru.SelectedItem.Text + "/" + dropEndYearThru.SelectedItem.Text;
                Convert.ToDateTime(max);
                extra += "EndDate <= '" + max + "' and ";
            }
            catch
            {
                extra += "EndDate = '" + min + "' and ";
            }
        }
        catch { }

        try
        {
            string min = dropStartMonth.SelectedItem.Value + "/" + dropStartDay.SelectedItem.Text + "/" + dropStartYear.SelectedItem.Text;
            Convert.ToDateTime(min);
            try
            {
                extra += "StartDate >= '" + min + "' and ";
                string max = dropStartMonthThru.SelectedItem.Value + "/" + dropStartDayThru.SelectedItem.Text + "/" + dropStartYearThru.SelectedItem.Text;
                Convert.ToDateTime(max);
                extra += "StartDate <= '" + max + "' and ";
            }
            catch
            {
                extra += "StartDate = '" + min + "' and ";
            }
        }
        catch { }

        try
        {
            string min = dropCloseMonth.SelectedItem.Value + "/" + dropCloseDay.SelectedItem.Text + "/" + dropCloseYear.SelectedItem.Text;
            DateTime dMin = Convert.ToDateTime(min);
            extra += "ClosedDate >= '" + min + "' and ";
            try
            {
                string max = dropCloseMonthThru.SelectedItem.Value + "/" + dropCloseDayThru.SelectedItem.Text + "/" + DropCloseYearThru.SelectedItem.Text;
                DateTime dMax = Convert.ToDateTime(max).AddDays(1);
                extra += "ClosedDate < '" + dMax.Month.ToString() + "/" + dMax.Day.ToString() + "/" + dMax.Year.ToString() + "' and ";
            }
            catch
            {
                dMin = dMin.AddDays(1);
                extra += "ClosedDate  < '" + dMin.Month.ToString() + "/" + dMin.Day.ToString() + "/" + dMin.Year.ToString() + "' and ";
            }
        }
        catch { }

        try
        {
            string min = dropDiagMonth.SelectedItem.Value + "/" + dropDiagDay.SelectedItem.Text + "/" + dropDiagYear.SelectedItem.Text;
            Convert.ToDateTime(min);
            try
            {
                extra += "DiagnosisDate >= '" + min + "' and ";
                string max = dropDiagMonthThru.SelectedItem.Value + "/" + dropDiagDayThru.SelectedItem.Text + "/" + dropDiagYearThru.SelectedItem.Text;
                Convert.ToDateTime(max);
                extra += "DiagnosisDate <= '" + max + "' and ";
            }
            catch
            {
                extra += "DiagnosisDate = '" + min + "' and ";
            }
        }
        catch { }


        GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
        myPatient.FirstName = txtFirstName.Text.ToString();
        myPatient.LastName = txtLastName.Text.ToString();
        myPatient.PIN = txtPIN.Text.ToString();
        myPatient.City = txtCity.Text.ToString();
        myPatient.StateProvince = txtState.Text.ToString();
        myPatient.Phone = txtPhone.Text.ToString();
        myPatient.Fax = txtFax.Text.ToString();
        myPatient.Email = txtEmail.Text.ToString();

        if (dropCountry.SelectedIndex != 0)
        {
            myPatient.CountryID = Convert.ToInt32(dropCountry.SelectedItem.Value);
        }
        if (dropSocial.SelectedIndex != 0)
        {
            extra += "Patientid in (select patientid from tblprogramofficer where personid = " + dropSocial.SelectedValue + ") and ";
        }
        if (dropPhysician.SelectedIndex != 0)
        {
            extra += "Patientid in (select patientid from tblpatientphysician where personid = " + dropPhysician.SelectedValue + ") and ";
        }
        if (dropMax.SelectedIndex != 0)
        {
            extra += "Patientid in (select patientid from tblmaxstation where personid = " + dropMax.SelectedValue + ") and ";
        }
        if (dropStatReason.SelectedIndex != 0)
        {
            myPatient.StatusReason = dropStatReason.SelectedValue;
        }
        if (dropDiagnosis.SelectedIndex != 0)
        {
            myPatient.Diagnosis = dropDiagnosis.SelectedItem.Text.ToString();
        }
        if (dropDiagnosis.SelectedValue == "GIST")
        {
            myPatient.Adjuvant = chkAdjuvant.Checked;
        }
        if (dropDosage.SelectedIndex != 0)
        {
            myPatient.CurrentDosage = dropDosage.SelectedItem.Text.ToString();
        }
        if (dropCurrentPhase.SelectedIndex != 0)
        {
            myPatient.CurrentCMLPhase = dropCurrentPhase.SelectedItem.Text.ToString();
        }
        if (rblstGender.SelectedIndex != -1)
        {
            myPatient.Sex = rblstGender.SelectedItem.Text;
        }
        if (rblstPhilPos.SelectedIndex != -1)
        {
            extra += "PhilPos = '" + rblstPhilPos.SelectedValue + "' and ";
        }
        if (dropStatus.SelectedIndex != 0)
        {
            extra += "GIPAPStatus = '" + dropStatus.SelectedItem.Text + "' and ";
        }
        if (dropProgram.SelectedIndex != 0)
        {
            extra += "currentprogram = '" + dropProgram.SelectedItem.Text + "' and ";
        }
        if (rblstInterferon.SelectedIndex != -1)
        {
            extra += "Interferon = " + rblstInterferon.SelectedItem.Value + " and ";
        }
        if (rblstRefractory.SelectedIndex != -1)
        {
            extra += "Refractory = " + rblstRefractory.SelectedItem.Value + " and ";
        }
        if (rblstUnresponsive.SelectedIndex != -1)
        {
            extra += "Unresponsive = " + rblstUnresponsive.SelectedItem.Value + " and ";
        }
        if (rblstHema.SelectedIndex != -1)
        {
            extra += "HematologicFailure = " + rblstHema.SelectedItem.Value + " and ";
        }
        if (rblstCyto.SelectedIndex != -1)
        {
            extra += "CytogeneticFailure = " + rblstCyto.SelectedItem.Value + " and ";
        }
        if (rblstInsurance.SelectedIndex != -1)
        {
            extra += "Insurance = " + rblstInsurance.SelectedItem.Value + " and ";
        }
        if (rblstCKit.SelectedIndex != -1)
        {
            extra += "ckitpositive = '" + rblstCKit.SelectedIndex + "' and ";
        }

        DataSet ds = myPatient.PatientSearch(extra, sessUse);
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Matching Results were Found.";
        DGResults.DataSource = ds;
        DGResults.DataBind();
        PanelSearch.Visible = false;
        PanelResults.Visible = true;
    }
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientSearch.aspx");
    }
    protected void dropStatus_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (dropStatus.SelectedValue == "Active")
        {
            dropStatReason.Items.Clear();
            dropStatReason.Items.Add("Select One");
            dropStatReason.Items.Add("Approved by exception");
            dropStatReason.Items.Add("Approved with Partial Coverage");
            dropStatReason.Items.Add("Fulfills all criteria");
        }

        else if (dropStatus.SelectedValue == "Denied")
        {
            dropStatReason.Items.Clear();
            dropStatReason.Items.Add("Select One");
            dropStatReason.Items.Add("Other Diagnosis");
            dropStatReason.Items.Add("C-Kit Negative");
            dropStatReason.Items.Add("GIST Tumor is Neither Metastatic or Unresectable");
            dropStatReason.Items.Add("Philadelphia Chromosome -");
            dropStatReason.Items.Add("Glivec not prescribed");
            dropStatReason.Items.Add("Patient Had Successful Surgery/BMT");
            dropStatReason.Items.Add("Patient has passed away");
            dropStatReason.Items.Add("Patient has insurance");
            dropStatReason.Items.Add("Insurance covers Glivec");
            dropStatReason.Items.Add("Does not meet financial criteria for GIPAP");
            dropStatReason.Items.Add("Patient Has Access to Reimbursement");
            dropStatReason.Items.Add("Does Not Meet Country Financial Requirements");
            dropStatReason.Items.Add("Glivec not approved in country");
            dropStatReason.Items.Add("GIPAP not approved in country");
            dropStatReason.Items.Add("Interferon treatment required");
            dropStatReason.Items.Add("Duplicate Application");
            dropStatReason.Items.Add("Underage");
            dropStatReason.Items.Add("Referred to EAP");
            dropStatReason.Items.Add("Does Not Meet Country Citizenship Requirements");
            dropStatReason.Items.Add("Patient’s Physician not Approved for GIPAP");
            dropStatReason.Items.Add("Patient or Doctor Retracts Application");
            dropStatReason.Items.Add("Unspecified / Other");
            dropStatReason.Items.Add("Lost contact with patient");
            dropStatReason.Items.Add("Referred to Clinical Trial");
        }

        else if (dropStatus.SelectedValue == "Pending")
        {
            dropStatReason.Items.Clear();
            dropStatReason.Items.Add("Select One");
            dropStatReason.Items.Add("Waiting for SW Review");
            dropStatReason.Items.Add("Waiting for Max Station Response");
            dropStatReason.Items.Add("Waiting for GCC feedback");
            dropStatReason.Items.Add("Waiting for Regulatory Approval");
            dropStatReason.Items.Add("Waiting For GIPAP Approval From Novartis");
            dropStatReason.Items.Add("Waiting For Medical Information");
            dropStatReason.Items.Add("Waiting For Physician Information");
            dropStatReason.Items.Add("Waiting For Reimbursement");
            dropStatReason.Items.Add("Need to confirm Ph +");
            dropStatReason.Items.Add("Need More Information");
            dropStatReason.Items.Add("Patient Is Applying After Being Denied");
            dropStatReason.Items.Add("Interferon treatment required");
            dropStatReason.Items.Add("Unspecified / Other");
        }

        else if (dropStatus.SelectedValue == "Closed")
        {
            dropStatReason.Items.Clear();
            dropStatReason.Items.Add("Select One");
            dropStatReason.Items.Add("Clinical Reason");
            dropStatReason.Items.Add("Patient does not meet GIPAP criteria");
            dropStatReason.Items.Add("Lost contact with patient");
            dropStatReason.Items.Add("No re-evaluation information provided");
            dropStatReason.Items.Add("Duplicate Patient");
            dropStatReason.Items.Add("Unspecified / Other");
        }
    }
   
}
