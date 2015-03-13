using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class Application_GIPAPApplication : System.Web.UI.Page
{
    GIPAP_Objects.GIPAPApplicant myApplicant = new GIPAP_Objects.GIPAPApplicant();
    GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
    GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    string Uname;
    string Esubject;


    
    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        Uname = sessUse.Username;
        

        if (!Page.IsPostBack)
        {
            this.FillCountryComboBox(myCountry.GetCountryList(true).Tables[0]);
            this.FillYearComboBox();
        }

        if (sessUse.Role == "TMFUser")
        {
            Esubject = "GIPAP PATIENT REGISTRATION - PO - ";
        }
        else if (sessUse.Role == "Physician")
        {
            Esubject = "GIPAP PATIENT REGISTRATION - R - ";
            cboCountry.SelectedValue = sessUse.CountryID.ToString();
            cboCountry.Enabled = false;
            this.SetCountryOptions();
        }
        else if (sessUse.Role == "MaxStation")
        {
            Esubject = "GIPAP PATIENT REGISTRATION - MS - ";
        }
        else if (sessUse.Role == "Clinic")
        {
            Esubject = "GIPAP PATIENT REGISTRATION - C - ";
        }
        else if (sessUse.Role == "")
        {
            Uname = "System";
            Esubject = "GIPAP PATIENT REGISTRATION - NR - ";
        }
    }
    //**************************************************************************************************************
    private void SetCountryOptions()
    {
        if (cboCountry.SelectedIndex != 0)
        {
            DataSet ds = myApplicant.GetApplicantDataSets(sessUse.UserID, sessUse.Role, Convert.ToInt32(cboCountry.SelectedValue));
            DateTime alldate, dfspdate, adjgistdate, mdsdate, mastdate, hesdate;
            bool dfsp, adjgist, mds, mast, hes;
            int phall;
            // inflate country-diagnosis info
            phall = Convert.ToInt32(ds.Tables[0].Rows[0]["phallstatus"]);
            try
            {
                alldate = Convert.ToDateTime(ds.Tables[0].Rows[0]["alldate"]);
            }
            catch
            {
                alldate = Convert.ToDateTime("1/1/1900");
            }
            try
            {
                dfspdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["dfspdate"]);
            }
            catch
            {
                dfspdate = Convert.ToDateTime("1/1/1900");
            }
            dfsp = Convert.ToBoolean(ds.Tables[0].Rows[0]["dfspapproved"]);
            /*try
            {
                adjgistdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["adjgistdate"]);
            }
            catch
            {
                adjgistdate = Convert.ToDateTime("1/1/1900");
            }
            adjgist = Convert.ToBoolean(ds.Tables[0].Rows[0]["adjgistapproved"]);*/
            try
            {
                mdsdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["mdsdate"]);
            }
            catch
            {
                mdsdate = Convert.ToDateTime("1/1/1900");
            }
            mds = Convert.ToBoolean(ds.Tables[0].Rows[0]["mdsapproved"]);
            try
            {
                mastdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["mastdate"]);
            }
            catch
            {
                mastdate = Convert.ToDateTime("1/1/1900");
            }
            mast = Convert.ToBoolean(ds.Tables[0].Rows[0]["mastapproved"]);
            try
            {
                hesdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["hesdate"]);
            }
            catch
            {
                hesdate = Convert.ToDateTime("1/1/1900");
            }
            hes = Convert.ToBoolean(ds.Tables[0].Rows[0]["hesapproved"]);

            //add or remove options
            //phall
            if (phall > 0 && alldate <= DateTime.Today && alldate > Convert.ToDateTime("1/1/1999"))
            {
                cboDiagnosis.Items.Add("Ph+ ALL");
            }
            else
            {
                cboDiagnosis.Items.Remove("Ph+ ALL");
            }
            //dfsp
            if (dfsp && dfspdate <= DateTime.Today && dfspdate > Convert.ToDateTime("1/1/1999"))
            {
                cboDiagnosis.Items.Add("DFSP");
            }
            else
            {
                cboDiagnosis.Items.Remove("DFSP");
            }
            //MDS
            if (mds && mdsdate <= DateTime.Today && mdsdate > Convert.ToDateTime("1/1/1999"))
            {
                cboDiagnosis.Items.Add("MDS / MPD");
            }
            else
            {
                cboDiagnosis.Items.Remove("MDS / MPD");
            }
            //MAST
            if (mast && mastdate <= DateTime.Today && mastdate > Convert.ToDateTime("1/1/1999"))
            {
                cboDiagnosis.Items.Add("Systemic Mastocytosis");
            }
            else
            {
                cboDiagnosis.Items.Remove("Systemic Mastocytosis");
            }
            //HES
            if (hes && hesdate <= DateTime.Today && hesdate > Convert.ToDateTime("1/1/1999"))
            {
                cboDiagnosis.Items.Add("HES / CEL");
            }
            else
            {
                cboDiagnosis.Items.Remove("HES / CEL");
            }
            cboDiagnosis.Enabled = true;

            this.FillPhysicianDropBox(ds.Tables[1]);
            if (sessUse.Role == "Physician")
            {
                dropPhysician.SelectedValue = ds.Tables[1].Rows[0]["personid"].ToString();
                dropPhysician.Enabled = false;
                int tas = Convert.ToInt32(ds.Tables[1].Rows[0]["tasigna"]);
                if (tas > 0)
                {
                    LabelTreatment.Visible = true;
                    rblstTreatment.Visible = true;
                }
                else
                {
                    LabelTreatment.Visible = false;
                    rblstTreatment.Visible = false;
                    rblstTreatment.SelectedIndex = 0;
                    LabelTasignaCML.Visible = false;
                }
            }
            else
            {
                dropPhysician.Enabled = true;
            }
        }
    }
    //**************************************************************************************************************
    private void FillYearComboBox()
    {
        DateTime dtNow = DateTime.Now;

        for (int i = dtNow.Year; i >= 1900; i--)
        {
            cboDiagnosisYear.Items.Add(i.ToString());
            cboBirthYear.Items.Add(i.ToString());
            GlivecStartYear.Items.Add(i.ToString());
            dropTasignaStartYear.Items.Add(i.ToString());
            dropTasignaDiagYear.Items.Add(i.ToString());
        }
        cboDiagnosisYear.Items.Insert(0, "Year");
        cboDiagnosisYear.SelectedItem.Value = "0";
        cboBirthYear.Items.Insert(0, "Year");
        cboBirthYear.SelectedItem.Value = "0";
        GlivecStartYear.Items.Insert(0, "Year");
        GlivecStartYear.SelectedItem.Value = "0";
    }
    //**********************************************************************************************************************
    private void FillCountryComboBox(DataTable dt)
    {
        //Fills the country combobox with the countries from the database
        //bind data to patient country
        cboCountry.DataSource = dt;
        cboCountry.DataValueField = "CountryID";
        cboCountry.DataTextField = "CountryName";
        cboCountry.DataBind();
        cboCountry.Items.Insert(0, "Select a country");
        cboCountry.SelectedItem.Value = "0";
        //bind data to contact country
        cboContactCountry.DataSource = dt;
        cboContactCountry.DataValueField = "CountryID";
        cboContactCountry.DataTextField = "CountryName";
        cboContactCountry.DataBind();
        cboContactCountry.Items.Insert(0, "Select a country");
        cboContactCountry.SelectedItem.Value = "0";
    }
    //**********************************************************************************************************************
    private void FillPhysicianDropBox(DataTable dt)
    {
        dropPhysician.DataSource = dt;
        dropPhysician.DataTextField = "physicianname";
        dropPhysician.DataValueField = "personid";
        dropPhysician.DataBind();
        dropPhysician.Items.Insert(0, "Select a Physician");
        dropPhysician.SelectedItem.Value = "0";
    }
    //**************************************************************************************************************
    private void ShowIntroSummary()
    {
        LabelSummaryIntro.Text = "<font class='lbl'>Country: </font>" + this.cboCountry.SelectedItem.Text;
        LabelSummaryIntro.Text += "<br /><font class='lbl'>Diagnosis: </font>" + this.cboDiagnosis.SelectedItem.Text;
        if (dropPhysician.SelectedIndex != 0 || sessUse.Role == "Physician")
        {
            myPhysician = new GIPAP_Objects.Physician(Convert.ToInt32(dropPhysician.SelectedValue), sessUse.Role);
            /*txtPhysicianFirstName.Text = myPhysician.FirstName;
            txtPhysicianLastName.Text = myPhysician.LastName;*/
            if (sessUse.Role == "Physician" && sessUse.NOA && sessUse.NOADate <= DateTime.Today)
            {
                LabelSummaryIntro.Text += "<br /><font class='lbl'>GIPAP/NOA Physician Name: </font>";
            }
            else if (myPhysician.NOA && myPhysician.NOADate <= DateTime.Today)
            {
                LabelSummaryIntro.Text += "<br /><font class='lbl'>GIPAP/NOA Physician Name: </font>";
            }
            else if (myPhysician.Tasigna > 0)
            {
                LabelSummaryIntro.Text += "<br /><font class='lbl'>GIPAP/NOA Physician Name: </font>";
            }
            else
            {
                LabelSummaryIntro.Text += "<br /><font class='lbl'>Physician Name: </font>";
            }
            LabelSummaryIntro.Text += this.dropPhysician.SelectedItem.Text;
        }
        LabelSummaryIntro.Text += "<br /><font class='lbl'>Treatment: </font>" + rblstTreatment.SelectedValue;
    }
    //**************************************************************************************************************
    private int ShowPatientSummary()
    {
        //List out the applicants information
        LabelSummaryPatient.Text = "<h4>Applicant Information</h4>";
        LabelSummaryPatient.Text += "<font class='lbl'>Applicants Name: </font>" + this.txtFirstName.Text + " " + this.txtLastName.Text;
        if(cboCountry.SelectedItem.Text =="Thailand")
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Thai Name: </font>"+this.txtThaiName.Text;
        if (rbGender.SelectedItem.Value == "M")
        {
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Gender: </font>Male";
        }
        else
        {
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Gender: </font>Female";
        }
        try
        {
            Convert.ToDateTime(this.cboBirthDay.SelectedValue + " " + this.cboBirthMonth.SelectedItem.Text + ", " + this.cboBirthYear.SelectedValue);
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Birth Date: </font>" + this.cboBirthDay.SelectedValue + " " + this.cboBirthMonth.SelectedItem.Text + ", " + this.cboBirthYear.SelectedValue;
        }
        catch
        {
            LabelErrorPatient.Text = "<br />Invalid Birthdate.";
            return 0;
        }
        LabelSummaryPatient.Text += "<br /><font class='lbl'>Street1: </font>" + this.txtStreet1.Text;
        LabelSummaryPatient.Text += "<br /><font class='lbl'>Street2: </font>" + this.txtStreet2.Text;
        LabelSummaryPatient.Text += "<br /><font class='lbl'>City: </font>" + this.txtCity.Text;
        if (this.txtStateProvince.Text != "")
        {
            LabelSummaryPatient.Text += "<br /><font class='lbl'>State/Province: </font>" + this.txtStateProvince.Text;
        }
        if (this.txtPostalCode.Text != "")
        {
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Postal Code: </font>" + this.txtPostalCode.Text;
        }
        LabelSummaryPatient.Text += "<br /><font class='lbl'>Country: </font>" + this.cboCountry.SelectedItem.Text;
        if (this.txtPhone.Text != "")
        {
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Phone: </font>" + this.txtPhone.Text;
        }
        if (this.txtFax.Text != "")
        {
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Fax: </font>" + this.txtFax.Text;
        }
        if (this.txtEmail.Text != "")
        {
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Email: </font>" + this.txtEmail.Text;
        }
        if (this.txtMobile.Text != "")
        {
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Mobile: </font>" + this.txtMobile.Text;
        }

        //***********************************************************************************
        //Show alternate contact information if it was entered
        if (this.txtContactFirstName.Text != "" && this.txtContactLastName.Text != "")
        {
            LabelSummaryPatient.Text += "<h4>Contact Information</h4>";
            LabelSummaryPatient.Text += "<font class='lbl'>Contact Name: </font>" + this.txtContactFirstName.Text + " " + this.txtContactLastName.Text;

            if (this.txtContactStreet1.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>Street1: </font>" + this.txtContactStreet1.Text;
            }
            if (this.txtContactStreet2.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>Street2: </font>" + this.txtContactStreet2.Text;
            }
            if (this.txtContactCity.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>City: </font>" + this.txtContactCity.Text;
            }
            if (this.txtContactStateProvince.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>State/Province: </font>" + this.txtContactStateProvince.Text;
            }
            if (this.txtContactPostalCode.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>Postal Code: </font>" + this.txtContactPostalCode.Text;
            }
            LabelSummaryPatient.Text += "<br /><font class='lbl'>Country: </font>" + this.cboContactCountry.SelectedItem.Text;
            if (this.txtContactPhone.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>Phone: </font>" + this.txtContactPhone.Text;
            }
            if (this.txtContactFax.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>Fax: </font>" + this.txtContactFax.Text;
            }
            if (this.txtContactMobile.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>Mobile: </font>" + this.txtContactMobile.Text;
            }
            if (this.txtContactEmail.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>Email: </font>" + this.txtContactEmail.Text;
            }
            if (this.cboRelationship.SelectedItem.Text != "Select One")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>Relationship: </font>" + this.cboRelationship.SelectedItem.Text;
            }
            if (this.txtRelationshipDetails.Text != "")
            {
                LabelSummaryPatient.Text += "<br /><font class='lbl'>Relationship Details: </font>" + this.txtRelationshipDetails.Text;
            }
        }
        //Show Physician information
        LabelSummaryPatient.Text += "<h4>Physician Information</h4>";
        if (dropPhysician.SelectedIndex != 0 || sessUse.Role == "Physician")
        {
            myPhysician = new GIPAP_Objects.Physician(Convert.ToInt32(dropPhysician.SelectedValue), sessUse.Role);
            txtPhysicianFirstName.Text = myPhysician.FirstName;
            txtPhysicianLastName.Text = myPhysician.LastName;
            if (sessUse.Role == "Physician" && sessUse.NOA && sessUse.NOADate <= DateTime.Today)
            {
                LabelSummaryPatient.Text += "<font class='lbl'>GIPAP/NOA Physician Name: </font>";
            }
            else if (myPhysician.NOA && myPhysician.NOADate <= DateTime.Today)
            {
                LabelSummaryPatient.Text += "<font class='lbl'>GIPAP/NOA Physician Name: </font>";
            }
            else
            {
                LabelSummaryPatient.Text += "<font class='lbl'>Physician Name: </font>";
            }
            LabelSummaryPatient.Text += this.dropPhysician.SelectedItem.Text;
        }
        return 1;
    }
    //**************************************************************************************************************
    private int ShowDiagSummary()
    {
        LabelSummaryDiag.Text = "";
        if (rblstTreatment.SelectedValue == "Glivec")
        {
            LabelSummaryDiag.Text += "<h4>History and Diagnosis Information</h4>";
            //Applied for GIPAP answer
            LabelSummaryDiag.Text += "<font class='lbl'>Treatment: </font>" + rblstTreatment.SelectedValue;
            if (rbAppliedGIPAP.SelectedItem.Value == "0")
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Applied For GIPAP: </font>No";
            }
            else
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Applied For GIPAP: </font>Yes";
            }
            if (rblstPatientConsent.Visible)
            {
                if (rblstPatientConsent.Items[1].Selected)
                {
                    LabelSummaryDiag.Text += "<br /><font class='lbl'>Patient Consent form signed: </font>Yes";
                }
                else
                {
                    LabelSummaryDiag.Text += "<br /><font class='lbl'>Patient Consent form signed: </font>No";
                }
            }
            //Prescribed dosage answer
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Prescribed Daily Dosage: </font>" + this.cboDosage.SelectedValue;
            //Diagnosis date answer
            try
            {
                DateTime.Parse(cboDiagnosisMonth.SelectedValue + "/" + cboDiagnosisDay.SelectedValue + "/" + cboDiagnosisYear.SelectedValue);
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Diagnosis Date: </font>" + this.cboDiagnosisDay.SelectedValue + " " + this.cboDiagnosisMonth.SelectedItem.Text + ", " + this.cboDiagnosisYear.SelectedValue;
            }
            catch
            {
                LabelErrorDiag.Text = "<br />Invalid Diagnosis Date.";
                return 0;
            }
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Has patient previously taken Glivec®/imatinib?: : </font>" + rblstGlivec.SelectedItem.Text;
            if (rblstGlivec.SelectedIndex == 1)
            {
                try
                {
                    Convert.ToDateTime(GlivecStartMonth.SelectedValue + "/" + GlivecStartDay.SelectedValue + "/" + GlivecStartYear.SelectedValue);
                    LabelSummaryDiag.Text += "<br /><font class='lbl'>If yes, what was the starting date?: </font>" + this.GlivecStartDay.SelectedValue + " " + this.GlivecStartMonth.SelectedItem.Text + ", " + this.GlivecStartYear.SelectedValue;
                }
                catch
                {
                    LabelErrorDiag.Text = "<br />Invalid Glivec Start Date.";
                    return 0;
                }
            }
        }

        //***********************************************************************************
        if (this.cboDiagnosis.SelectedItem.Text == "CML")
        {
            //Show CML and interferon information
            LabelSummaryDiag.Text += "<h4>CML and Interferon Information</h4>";
            //Disease/diagnosis information
            LabelSummaryDiag.Text += "<font class='lbl'>Diagnosis: </font>" + this.cboDiagnosis.SelectedItem.Text;
            //Philidelphia positive answer
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Philadelphia Chromosome Positive: </font>" + rbPhilPos.SelectedItem.Text;
            if (rblstBCR.SelectedIndex != -1)
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>If no, is patient BCR-Abl positive?: </font>" + rblstBCR.SelectedItem.Text;
            }
            LabelSummaryDiag.Text += "<br /><font class='lbl'>CML Phase: </font>" + this.cboCMLPhase.SelectedItem.Text;
            //Has the applicant received interferon
        }
        else if (this.cboDiagnosis.SelectedItem.Text == "Ph+ ALL")
        {
            //Show CML and interferon information
            LabelSummaryDiag.Text += "<h4>Ph+ ALL Information</h4>";
            //Disease/diagnosis information
            LabelSummaryDiag.Text += "<font class='lbl'>Diagnosis: </font>" + this.cboDiagnosis.SelectedItem.Text;
            //Philidelphia positive answer
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Philadelphia Chromosome Positive: </font>" + rblstAllPh.SelectedItem.Text;
            if (rblstAllBCR.SelectedIndex != -1)
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>If no, is patient BCR-Abl positive?: </font>" + rblstAllBCR.SelectedItem.Text;
            }
            if (Panel2ndOnly.Visible)
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Is this patient diagnosed with relapsed or refractory Ph+ ALL?: </font>";
                if (rblst2ndLineRelapsed.SelectedIndex == 1)
                {
                    LabelSummaryDiag.Text += "Yes";
                }
                else
                {
                    LabelSummaryDiag.Text += "No";
                }
            }
            else if (Panel1stand2nd.Visible)
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Treatment Option: </font>" + rblst1stand2nd.SelectedValue;
            }
        }
        else if (this.cboDiagnosis.SelectedItem.Text == "DFSP")
        {
            //Show CML and interferon information
            LabelSummaryDiag.Text += "<h4>DFSP Information</h4>";
            //Disease/diagnosis information
            LabelSummaryDiag.Text += "<font class='lbl'>Diagnosis: </font>" + this.cboDiagnosis.SelectedItem.Text;
            //Philidelphia positive answer
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Is the tumor unresectable, recurrent or metastatic?: </font>" + rblstRecurrent.SelectedItem.Text;
        }
        //***********************************************************************************
        else if (this.cboDiagnosis.SelectedItem.Text == "MDS / MPD")
        {
            LabelSummaryDiag.Text += "<h4>MDS / MPD Information</h4>";
            LabelSummaryDiag.Text += "<font class='lbl'>Diagnosis: </font>" + this.cboDiagnosis.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Medical Summary: </font><br>" + txtMDSSummary.Text;
        }
        else if (this.cboDiagnosis.SelectedItem.Text == "Systemic Mastocytosis")
        {
            LabelSummaryDiag.Text += "<h4>Systemic Mastocytosis Information</h4>";
            LabelSummaryDiag.Text += "<font class='lbl'>Diagnosis: </font>" + this.cboDiagnosis.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Medical Summary: </font><br>" + txtSysMasSummary.Text;
        }
        else if (this.cboDiagnosis.SelectedItem.Text == "HES / CEL")
        {
            LabelSummaryDiag.Text += "<h4>HES / CEL Information</h4>";
            LabelSummaryDiag.Text += "<font class='lbl'>Diagnosis: </font>" + this.cboDiagnosis.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Medical Summary: </font><br>" + txtHesSummary.Text;
        }
        else//Show GIST and C-Kit information
        {
            LabelSummaryDiag.Text += "<h4>GIST and C-Kit Information</h4>";
            //Disease/diagnosis information
            LabelSummaryDiag.Text += "<font class='lbl'>Diagnosis: </font>" + this.cboDiagnosis.SelectedItem.Text;
            //is applicant C-Kit positive
            if (rbCKitPos.SelectedItem.Value == "0")
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>C-Kit Positive: </font>No";
            }
            else if (rbCKitPos.SelectedItem.Value == "1")
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>C-Kit Positive: </font>Yes";
            }
            else
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>C-Kit Positive: </font>Don't Know";
            }
            //is tumor unresectable
            if (rbUnresectable.SelectedItem.Value == "0")
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Tumor Unresectable: </font>No";
            }
            else if (rbUnresectable.SelectedItem.Value == "1")
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Tumor Unresectable: </font>Yes";
            }
            else
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Tumor Unresectable: </font>Don't Know";
            }
            //is tumor unresectable
            if (rbMetastatic.SelectedItem.Value == "0")
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Tumor Metastatic: </font>No";
            }
            else if (rbMetastatic.SelectedItem.Value == "1")
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Tumor Metastatic: </font>Yes";
            }
            else
            {
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Tumor Metastatic: </font>Don't Know";
            }
            if (rblstAdj.SelectedIndex != -1)
            {
                LabelSummaryDiag.Text += "<h4>Adjuvant GIST Information</h4>";

                LabelSummaryDiag.Text += "<font class='lbl'>Is this adjuvant treatment?: </font>" + rblstAdj.SelectedItem.Text;
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Is the case intermediate or high risk?: </font>" + (rblstHighRisk.SelectedItem == null ? "":rblstHighRisk.SelectedItem.Text);
            }
        }
        //TASIGNA
        if (rblstTreatment.SelectedValue == "Tasigna")
        {
            LabelSummaryDiag.Text += "<h4>History and Diagnosis Information</h4>";
            LabelSummaryDiag.Text += "<font class='lbl'>Treatment: </font>" + rblstTreatment.SelectedValue;
            try
            {
                DateTime.Parse(dropTasignaDiagMonth.SelectedValue + "/" + dropTasignaDiagDay.SelectedValue + "/" + dropTasignaDiagYear.SelectedValue);
                LabelSummaryDiag.Text += "<br /><font class='lbl'>Diagnosis Date: </font>" + this.dropTasignaDiagDay.SelectedValue + " " + dropTasignaDiagMonth.SelectedItem.Text + ", " + this.dropTasignaDiagYear.SelectedValue;
            }
            catch
            {
                LabelErrorDiag.Text = "<br />Invalid Diagnosis Date.";
                return 0;
            }
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Has patient previously taken Glivec®?: </font>" + rblstTasignaGlivec.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Has patient previously taken Imatinib?: </font>" + rblstTasignaImatinib.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Is the patient intolerant to Glivec?: </font>" + rblstGlivecIntolerant.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Is the patient resistant to Glivec?: </font>" + rblstGlivecResistant.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Is the patient currently receiving Dasatinib?: </font>" + rblstDasatinib.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Is the patient intolerant to Dasatinib?: </font>" + rblstDasatinibIntolerant.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Is the patient resistant to Dasatinib?: </font>" + rblstDasatinibResistant.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Has the patient previously taken nilotinib/Tasigna?: </font>" + rblstTasigna.SelectedItem.Text;
            if (rblstTasigna.SelectedIndex == 1)
            {
                try
                {
                    Convert.ToDateTime(dropTasignaStartMonth.SelectedValue + "/" + dropTasignaStartDay.SelectedValue + "/" + dropTasignaStartYear.SelectedValue);
                    LabelSummaryDiag.Text += "<br /><font class='lbl'>If yes, what was the starting date?: </font>" + this.dropTasignaStartDay.SelectedValue + " " + this.dropTasignaStartMonth.SelectedItem.Text + ", " + this.dropTasignaStartYear.SelectedValue;
                    LabelSummaryDiag.Text += "<br /><font class='lbl'>If Yes, what was the prescribed daily dose?: </font>" + dropTasignaDose.SelectedValue;
                }
                catch
                {
                    LabelErrorDiag.Text = "<br />Invalid Tasigna Start Date.";
                    return 0;
                }
            }
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Requested NOA Tasigna Dose: </font>" + dropRequestedTasignaDose.SelectedValue;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Has the patient applied to NOA Tasigna in the past?: </font>" + rblstNOATasigna.SelectedItem.Text;
            LabelSummaryDiag.Text += "<br /><font class='lbl'>Has the patient consent form been signed?: </font>" + rblstTasignaPatientConsent.SelectedItem.Text;
        }
        return 1;
    }
    //**************************************************************************************************************
    private void ShowFinancialSummary()
    {
        LabelSummaryFin.Text = "";
       //Verification
        if (PanelFEF.Visible)
        {
            //Financial Summary
            LabelSummaryFin.Text += "<h4>Medical Evaluation Documents Collected:</h4>";
            LabelSummaryFin.Text += "<br /><font class='lbl'>Medical Chart</font>: " + rblstMedicalChart.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Philidelphia Chromosome Information</font>: " + rblstPhil.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>C-Kit Information</font>: " + rblstCKit.SelectedValue;
            LabelSummaryFin.Text += "<h4>Financial Evaluation Documents Collected:</h4>";
            LabelSummaryFin.Text += "<br /><font class='lbl'>Copy of ID</font>: " + rblstCopyofID.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Photo</font>: " + rblstPhoto.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Social Security Card</font>: " + rblstSScard.SelectedValue;
            if (rblstInsuranceCard.SelectedIndex != -1)
            {
                LabelSummaryFin.Text += "<br /><font class='lbl'>Insurance Card</font>: " + rblstInsuranceCard.SelectedValue;
            }
            LabelSummaryFin.Text += "<br /><font class='lbl'>Insurance Type</font>: " + txtInsuranceType.Text;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Tax Return</font>: " + rblstTaxReturn.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Income Verification Document(s):</font>: " + rblstSalarySlip.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Financial Affidavit Form</font>: " + rblstFinAffidavit.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Phone Bill</font>: " + rblstPhoneBill.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Other</font>: " + txtOtherDocs.Text;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Household Members</font>: " + dropHousehold.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Household Occupation</font>: " + txtHouseholdOccupation.Text;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Household Income</font>: " + txtHouseholdIncom.Text;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Additional Funds</font>: " + txtAdditionalFunds.Text;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Household Assets</font>: " + txtAssets.Text;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Insurance</font>: " + rblstMaxInsurance.SelectedItem.Text;
            if (rblstMaxRx.SelectedIndex != -1)
            {
                LabelSummaryFin.Text += "<br /><font class='lbl'>Covers Prescriptions</font>: " + rblstMaxRx.SelectedItem.Text;
            }
            if (rblstMaxCancerRx.SelectedIndex != -1)
            {
                LabelSummaryFin.Text += "<br /><font class='lbl'>Covers Cancer Prescriptions</font>: " + rblstMaxCancerRx.SelectedItem.Text;
            }
            if (rblstMaxGlivecRx.SelectedIndex != -1)
            {
                LabelSummaryFin.Text += "<br /><font class='lbl'>Covers Glivec Prescriptions</font>: " + rblstMaxGlivecRx.SelectedItem.Text;
            }
            LabelSummaryFin.Text += "<br /><font class='lbl'>Recommendation</font>: " + rblstRecommendation.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Explanation</font>: " + txtExplanation.Text;
            
        }
        else if (PanelInsurance.Visible)
        {

            //***********************************************************************************
            //Show insurance information
            LabelSummaryFin.Text += "<h4>Insurance Information</h4>";
            LabelSummaryFin.Text += "<br /><font class='lbl'>Applicant has Insurance: </font>";
            LabelSummaryFin.Text += rblstHealthInsurance.SelectedItem.Text;
            if (rbCoversRx.SelectedIndex != -1)
            {
                LabelSummaryFin.Text += "<br /><font class='lbl'>Covers Prescriptions: </font>" + rbCoversRx.SelectedItem.Text;
            }
            if (rbCoversCancerRx.SelectedIndex != -1)
            {
                LabelSummaryFin.Text += "<br /><font class='lbl'>Covers Cancer Prescriptions: </font>" + rbCoversCancerRx.SelectedItem.Text;
            }
            if (rbCoversGlivecRx.SelectedIndex != -1)
            {
                LabelSummaryFin.Text += "<br /><font class='lbl'>Covers Glivec Prescriptions: </font>" + rbCoversGlivecRx.SelectedItem.Text;
            }
        }

        if (PanelFinancial.Visible)
        {
            //***********************************************************************************
            //Show financial information
            LabelSummaryFin.Text += "<h4>Financial Information</h4>";
            //estimated annual income
            LabelSummaryFin.Text += "<br /><font class='lbl'>Estimated Annual Income: </font>$" + dropIncome.SelectedValue;
            LabelSummaryFin.Text += "<br /><font class='lbl'>Patient's or primary income earner's Occupation: </font>" + dropOccupation.SelectedItem.Text;
            //applicant eligible for GIPAP
        }
        //Aditional notes
        if (this.txtNotes.Text != "")
        {
            LabelSummaryFin.Text += "<br /><font class='lbl'>Additional notes: </font>" + this.txtNotes.Text;
            
        }
    }
    //**************************************************************************************************************
    private void loadDiag()
    {
        PanelDiagButton.Visible = true; //this makes the button at the bottom visible, no matter which panels are visible
        DataSet ds = myApplicant.GetCountryPhysicianApplicantDatasets(Convert.ToInt32(cboCountry.SelectedValue), Convert.ToInt32(dropPhysician.SelectedValue));
        if (rblstTreatment.SelectedValue == "Tasigna")
        {
            PanelTasigna.Visible = true;
        }
        else
        {
            PanelDiagHistory.Visible = true;
            if (cboCountry.SelectedValue == "76")
            {
                cboDosage.AutoPostBack = true;
                //no patient consent in india
                LabelPatientConsent.Visible = false;
                rblstPatientConsent.Items[1].Selected = true;
                rblstPatientConsent.Visible = false;
            }
            else
            {
                cboDosage.AutoPostBack = false;
            }
        }
        LabelDoseMessage.Visible = false;
        if (cboDiagnosis.SelectedValue == "CML")
        {
            PanelCML.Visible = true;
        }
        else if (cboDiagnosis.SelectedValue == "Ph+ ALL")
        {
            //cboDosage.SelectedValue = "600mg";
            //cboDosage.Enabled = false;
            //LabelDoseMessage.Visible = true;
            //LabelDoseMessage.Text = "A dosage of 600mg is required for a Ph+ ALL diagnosis";
            cboDosage.Items.Remove("200mg");
            cboDosage.Items.Remove("800mg");
            LabelDoseMessage.Visible = true;
            LabelDoseMessage.Text = "Dosage of 260mg and 300mg are approved pediatric dose only.";
            PanelALL.Visible = true;
            int pha = Convert.ToInt32(ds.Tables[0].Rows[0]["phallstatus"]);
            if (pha == 1)
            {
                Panel2ndOnly.Visible = true;
                Panel1stand2nd.Visible = false;
            }
            else if (pha == 2)
            {
                Panel2ndOnly.Visible = false;
                Panel1stand2nd.Visible = true;
            }
        }
        else if (cboDiagnosis.SelectedValue == "DFSP")
        {
            cboDosage.Items.Remove("200mg");
            cboDosage.Items.Remove("260mg");
            cboDosage.Items.Remove("300mg");
            PanelDFSP.Visible = true;
        }
        else if (cboDiagnosis.SelectedValue == "MDS / MPD")
        {
            cboDosage.SelectedValue = "400mg";
            cboDosage.Enabled = false;
            LabelDoseMessage.Visible = true;
            LabelDoseMessage.Text = "A dosage of 400mg is required for a MDS / MPD diagnosis";
            PanelMDS.Visible = true;
        }
        else if (cboDiagnosis.SelectedValue == "Systemic Mastocytosis")
        {
            cboDosage.Items.Insert(1, "100mg");
            cboDosage.Items.Remove("200mg");
            cboDosage.Items.Remove("260mg");
            cboDosage.Items.Remove("300mg");
            cboDosage.Items.Remove("600mg");
            cboDosage.Items.Remove("800mg");
            PanelSysMast.Visible = true;
        }
        else if (cboDiagnosis.SelectedValue == "HES / CEL")
        {
            cboDosage.Items.Insert(1, "100mg");
            cboDosage.Items.Remove("200mg");
            cboDosage.Items.Remove("260mg");
            cboDosage.Items.Remove("300mg");
            cboDosage.Items.Remove("600mg");
            cboDosage.Items.Remove("800mg");
            PanelHES.Visible = true;
        }
        else //gist
        {
            PanelGIST.Visible = true;
            bool adjgist;
            DateTime adjgistdate;
            try
            {
                adjgistdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["adjgistdate"]);
            }
            catch
            {
                adjgistdate = Convert.ToDateTime("1/1/1900");
            }
            adjgist = Convert.ToBoolean(ds.Tables[0].Rows[0]["adjgistapproved"]);
            //adj gist
            if (adjgist && adjgistdate <= DateTime.Today && adjgistdate > Convert.ToDateTime("1/1/1999"))
            {
                PanelAdjGIST.Visible = true;
                /* cboDosage.SelectedValue = "400mg";
                 cboDosage.Enabled = false;*/
                LabelDoseMessage.Visible = true;
                LabelDoseMessage.Text = "A dosage of 400mg is required for Adjuvant treatment of GIST";
            }
            else
            {
                PanelAdjGIST.Visible = false;
            }
        }
    }
    //**************************************************************************************************************
    private void hideDiag()
    {
        PanelTasigna.Visible = false;
        PanelDiagButton.Visible = false;
        PanelDiagHistory.Visible = false;
        PanelDFSP.Visible = false;
        PanelMDS.Visible = false;
        PanelSysMast.Visible = false;
        PanelHES.Visible = false;
        PanelALL.Visible = false;
        PanelCML.Visible = false;
        PanelGIST.Visible = false;
        PanelAdjGIST.Visible = false;
    }
    //**************************************************************************************************************
    private void loadFinancial()
    {
        bool noaapp;
        DataSet ds = myApplicant.GetCountryPhysicianApplicantDatasets(Convert.ToInt32(cboCountry.SelectedValue), Convert.ToInt32(dropPhysician.SelectedValue));
        if (ds.Tables[1].Rows.Count > 0)
        {
            if (Convert.ToBoolean(ds.Tables[1].Rows[0]["noa"]))
            {
                DateTime noadt = Convert.ToDateTime(ds.Tables[1].Rows[0]["noadate"]);
                if (noadt <= DateTime.Now)
                {
                    noaapp = true;
                }
                else
                {
                    noaapp = false;
                }
            }
            else
            {
                noaapp = false;
            }
        }
        else
        {
            noaapp = false;
        }


        if (noaapp)
        {
            //no insurance info for noa
        }
        else if (sessUse.Role == "MaxStation")
        {
            PanelFEF.Visible = true;
        }
        else
        {
            PanelInsurance.Visible = true;
        }

        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["needfinancialinfo"]))
        {
            //not asking this for noa any more, but still keeping at country level
            PanelFinancial.Visible = true;
        }
        PanelNotes.Visible = true;
        if (noaapp)
        {
            if (cboCountry.SelectedValue == "108")
            {
                LabelFormHeader.Text = "PAT Applicant Information";
            }
            else if (rblstTreatment.SelectedValue == "Glivec")
            {
                LabelFormHeader.Text = "NOA - GIPAP Applicant Information";
            }
            else if (rblstTreatment.SelectedValue == "Tasigna")
            {
                LabelFormHeader.Text = "NOA Tasigna Applicant Information";
            }
        }
        else
        {
            LabelFormHeader.Text = "GIPAP Applicant Information";
        }
    }
    //**************************************************************************************************************
    private void hideFinancial()
    {
        PanelInsurance.Visible = false;
        PanelFEF.Visible = false;
        PanelFinancial.Visible = false;
        PanelNotes.Visible = false;
    }
    //**************************************************************************************************************
    private int FillApplicantObject(GIPAP_Objects.GIPAPApplicant myApplicant)
    {
        //Applicant information
        myApplicant.FirstName = this.txtFirstName.Text.Trim();
        myApplicant.LastName = this.txtLastName.Text.Trim();
        myApplicant.ThaiName = this.txtThaiName.Text;
        myApplicant.Sex = this.rbGender.SelectedValue;
        myApplicant.BirthDate = Convert.ToDateTime(this.cboBirthDay.SelectedValue + " " + this.cboBirthMonth.SelectedItem.Text + ", " + this.cboBirthYear.SelectedValue);
        /*try
        {
            myApplicant.BirthDate = Convert.ToDateTime(this.cboBirthDay.SelectedValue + " " + this.cboBirthMonth.SelectedItem.Text + ", " + this.cboBirthYear.SelectedValue);
        }
        catch
        {
            LabelError.Text = "<br />Birthdate Invalid.";
            return 0;
        }*/
        myApplicant.Street1 = this.txtStreet1.Text.Trim();
        myApplicant.Street2 = this.txtStreet2.Text.Trim();
        myApplicant.City = this.txtCity.Text.Trim();
        myApplicant.StateProvince = this.txtStateProvince.Text.Trim();
        myApplicant.PostalCode = this.txtPostalCode.Text.Trim();
        myApplicant.CountryID = int.Parse(this.cboCountry.SelectedValue);
        myApplicant.Phone = this.txtPhone.Text.Trim();
        myApplicant.Fax = this.txtFax.Text.Trim();
        myApplicant.Mobile = this.txtMobile.Text.Trim();
        myApplicant.Email = this.txtEmail.Text.Trim();

        //Alternate contact information
        myApplicant.ContactFirstName = this.txtContactFirstName.Text.Trim();
        myApplicant.ContactLastName = this.txtContactLastName.Text.Trim();
        myApplicant.ContactStreet1 = this.txtContactStreet1.Text.Trim();
        myApplicant.ContactStreet2 = this.txtContactStreet2.Text.Trim();
        myApplicant.ContactCity = this.txtContactCity.Text.Trim();
        myApplicant.ContactStateProvince = this.txtContactStateProvince.Text.Trim();
        myApplicant.ContactPostalCode = this.txtContactPostalCode.Text.Trim();
        myApplicant.ContactCountryID = int.Parse(this.cboContactCountry.SelectedValue);
        myApplicant.ContactPhone = this.txtContactPhone.Text.Trim();
        myApplicant.ContactFax = this.txtContactFax.Text.Trim();
        myApplicant.ContactMobile = this.txtContactMobile.Text.Trim();
        myApplicant.ContactEmail = this.txtContactEmail.Text.Trim();
        myApplicant.ContactRelationship = this.cboRelationship.SelectedValue;
        myApplicant.RelationshipDetails = this.txtRelationshipDetails.Text.Trim();

        //Physician information
        myApplicant.PhysicianID = Convert.ToInt32(dropPhysician.SelectedValue);
        myApplicant.PhysicianFirstName = txtPhysicianFirstName.Text;
        myApplicant.PhysicianLastName = txtPhysicianLastName.Text;

        //History and diagnosis information
        myApplicant.Treatment = rblstTreatment.SelectedValue;
        if (rblstTreatment.SelectedValue == "Glivec")
        {
            myApplicant.AppliedForGIPAP = Convert.ToBoolean(rbAppliedGIPAP.SelectedIndex);
            myApplicant.PatientConsent = rblstPatientConsent.Items[1].Selected;
            myApplicant.Dosage = this.cboDosage.SelectedValue;
            if (cboCountry.SelectedValue == "76")
            {
                myApplicant.TabletStrength = dropTabletStrength.SelectedValue;
            }
            myApplicant.DiagnosisDate = DateTime.Parse(cboDiagnosisMonth.SelectedValue + "/" + cboDiagnosisDay.SelectedValue + "/" + cboDiagnosisYear.SelectedValue);
            /*try
            {
                myApplicant.DiagnosisDate = DateTime.Parse(cboDiagnosisMonth.SelectedValue + "/" + cboDiagnosisDay.SelectedValue + "/" + cboDiagnosisYear.SelectedValue);
            }
            catch
            {
                LabelError.Text = "<br />Invalid Diagnosis Date.";
                return 0;
            }*/
            myApplicant.Glivec = Convert.ToBoolean(rblstGlivec.SelectedIndex);
            if (myApplicant.Glivec)
            {
                myApplicant.GlivecStartDate = Convert.ToDateTime(GlivecStartMonth.SelectedValue + "/" + GlivecStartDay.SelectedValue + "/" + GlivecStartYear.SelectedValue);
                /*try
                {
                    myApplicant.GlivecStartDate = Convert.ToDateTime(GlivecStartMonth.SelectedValue + "/" + GlivecStartDay.SelectedValue + "/" + GlivecStartYear.SelectedValue);
                }
                catch
                {
                    LabelError.Text = "<br />Invalid Glivec Start Date.";
                    return 0;
                }*/
            }
        }
        else if (rblstTreatment.SelectedValue == "Tasigna")
        {
            myApplicant.DiagnosisDate = DateTime.Parse(dropTasignaDiagMonth.SelectedValue + "/" + dropTasignaDiagDay.SelectedValue + "/" + dropTasignaDiagYear.SelectedValue);
            myApplicant.Glivec = Convert.ToBoolean(rblstTasignaGlivec.SelectedIndex);
            myApplicant.Imatinib = Convert.ToBoolean(rblstTasignaImatinib.SelectedIndex);
            myApplicant.GlivecIntolerant = Convert.ToBoolean(rblstGlivecIntolerant.SelectedIndex);
            myApplicant.GlivecResistant = Convert.ToBoolean(rblstGlivecResistant.SelectedIndex);
            myApplicant.Dasatinib = Convert.ToBoolean(rblstDasatinib.SelectedIndex);
            myApplicant.DasatinibIntolerant = Convert.ToBoolean(rblstDasatinibIntolerant.SelectedIndex);
            myApplicant.DasatinibResistant = Convert.ToBoolean(rblstDasatinibResistant.SelectedIndex);
            myApplicant.Tasigna = Convert.ToBoolean(rblstTasigna.SelectedIndex);
            if (myApplicant.Tasigna)
            {
                myApplicant.TasignaStartDate = Convert.ToDateTime(dropTasignaStartMonth.SelectedValue + "/" + dropTasignaStartDay.SelectedValue + "/" + dropTasignaStartYear.SelectedValue);
                /*try
                {
                    myApplicant.TasignaStartDate = Convert.ToDateTime(dropTasignaStartMonth.SelectedValue + "/" + dropTasignaStartDay.SelectedValue + "/" + dropTasignaStartYear.SelectedValue);
                }
                catch
                {
                    LabelError.Text = "<br />Invalid Tasigna Start Date.";
                    return 0;
                }*/
                myApplicant.PrevTasignaDose = dropTasignaDose.SelectedValue;
            }
            myApplicant.Dosage = dropRequestedTasignaDose.SelectedValue;
            myApplicant.NOATasigna = Convert.ToBoolean(rblstNOATasigna.SelectedIndex);
            myApplicant.PatientConsent = false;
            myApplicant.TasignaPatientConsent = Convert.ToBoolean(rblstTasignaPatientConsent.SelectedIndex);
        }
        myApplicant.Diagnosis = this.cboDiagnosis.SelectedItem.Text;

        //CML and Interferon information
        if (cboDiagnosis.SelectedValue == "CML")
        {
            myApplicant.PhilPositive = rbPhilPos.SelectedIndex;
            myApplicant.BCR = rblstBCR.SelectedIndex;
            myApplicant.CMLPhase = this.cboCMLPhase.SelectedValue;            
        }
        else if (cboDiagnosis.SelectedValue == "DFSP")
        {
            myApplicant.Recurrent = Convert.ToBoolean(rblstRecurrent.SelectedIndex);
        }
        else if (cboDiagnosis.SelectedValue == "MDS / MPD")
        {
            myApplicant.DiagSummary = txtMDSSummary.Text;
        }
        else if (cboDiagnosis.SelectedValue == "Systemic Mastocytosis")
        {
            myApplicant.DiagSummary = txtSysMasSummary.Text;
        }
        else if (cboDiagnosis.SelectedValue == "HES / CEL")
        {
            myApplicant.DiagSummary = txtHesSummary.Text;
        }
        else if (cboDiagnosis.SelectedValue == "Ph+ ALL")
        {
            myApplicant.PhilPositive = rblstAllPh.SelectedIndex;
            myApplicant.BCR = rblstAllBCR.SelectedIndex;
            if (rblst2ndLineRelapsed.SelectedIndex == -1)
            {
                if (rblst1stand2nd.SelectedIndex == 0)
                {
                    myApplicant.RelapsedRefractory = 1;
                    myApplicant.Chemo = -1;
                }
                else if (rblst1stand2nd.SelectedIndex == 1)
                {
                    myApplicant.RelapsedRefractory = -1;
                    myApplicant.Chemo = 1;
                }
            }
            else
            {
                myApplicant.RelapsedRefractory = rblst2ndLineRelapsed.SelectedIndex;
                myApplicant.Chemo = -1;
            }
        }
        else//GIST and C-Kit information
        {
            myApplicant.CKitPos = rbCKitPos.SelectedIndex;
            myApplicant.Unresectable = rbUnresectable.SelectedIndex;
            myApplicant.Metastatic = rbMetastatic.SelectedIndex;
            myApplicant.Adjuvant = rblstAdj.Items[1].Selected;
            myApplicant.HighRisk = rblstHighRisk.SelectedIndex;
        }

        //Verification
        if (rblstMaxInsurance.SelectedIndex != -1)
        {
            myApplicant.MedicalChart = rblstMedicalChart.SelectedIndex;
            myApplicant.PhiladelphiaVerification = rblstPhil.SelectedIndex;
            myApplicant.CKitVerification = rblstCKit.SelectedIndex;
            myApplicant.CopyOfID = rblstCopyofID.SelectedIndex;
            myApplicant.Photo = rblstPhoto.SelectedIndex;
            myApplicant.SSCard = rblstSScard.SelectedIndex;
            if (rblstInsuranceCard.SelectedIndex != -1)
            {
                myApplicant.InsuranceCard = rblstInsuranceCard.SelectedIndex;
            }
            else
            {
                myApplicant.InsuranceCard = 2;
            }
            myApplicant.InsuranceType = txtInsuranceType.Text;
            myApplicant.TaxReturn = rblstTaxReturn.SelectedIndex;
            myApplicant.SalarySlip = rblstSalarySlip.SelectedIndex;
            myApplicant.FinancialAffidavit = rblstFinAffidavit.SelectedIndex;
            myApplicant.PhoneBill = rblstPhoneBill.SelectedIndex;
            myApplicant.OtherDocs = txtOtherDocs.Text;
            myApplicant.HouseholdMembers = dropHousehold.SelectedIndex;
            myApplicant.HouseholdOccupation = txtHouseholdOccupation.Text;
            myApplicant.HouseholdIncome = txtHouseholdIncom.Text;
            myApplicant.AdditionalFunds = txtAdditionalFunds.Text;
            myApplicant.HouseholdAssets = txtAssets.Text;
            myApplicant.Recommendation = rblstRecommendation.SelectedValue;
            myApplicant.Explanation = txtExplanation.Text;
            //Insurance information for max stations
            myApplicant.Insurance = Convert.ToBoolean(rblstMaxInsurance.SelectedIndex);
            myApplicant.CoversRx = rblstMaxRx.Items[1].Selected;
            myApplicant.CoversCancerRx = rblstMaxCancerRx.Items[1].Selected;
            myApplicant.CoversGlivecRx = rblstMaxGlivecRx.Items[1].Selected;
        }
        //Insurance information
        else if (rblstHealthInsurance.SelectedIndex != -1)
        {
            //not being shown for noa patients
            myApplicant.Insurance = Convert.ToBoolean(rblstHealthInsurance.SelectedIndex);
            myApplicant.CoversRx = rbCoversRx.Items[1].Selected;
            myApplicant.CoversCancerRx = rbCoversCancerRx.Items[1].Selected;
            myApplicant.CoversGlivecRx = rbCoversGlivecRx.Items[1].Selected;
        }

        if (dropIncome.SelectedIndex != 0)
        {
            //Financial information
            myApplicant.AnnualIncome = dropIncome.SelectedValue;
            myApplicant.Occupation = dropOccupation.SelectedValue;
        }
        myApplicant.Notes = this.txtNotes.Text;
        return 1;
    }
    protected void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SetCountryOptions();
        if (cboCountry.SelectedItem.Text == "Thailand") thai.Visible = true;
        else
            thai.Visible = false;
    }
    protected void dropPhysician_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropPhysician.SelectedIndex != 0)
        {
            DataSet ds = myApplicant.GetCountryPhysicianApplicantDatasets(Convert.ToInt32(cboCountry.SelectedValue), Convert.ToInt32(dropPhysician.SelectedValue));
            if (Convert.ToBoolean(ds.Tables[1].Rows[0]["noa"])) //this now just means noa glivec
            {
                DateTime noadt = Convert.ToDateTime(ds.Tables[1].Rows[0]["noadate"]);
                if (noadt <= DateTime.Now)
                {
                    if (cboCountry.SelectedValue == "108")
                    {
                        LabelFormHeader.Text = "PAT Applicant Information";
                    }
                    else
                    {
                        LabelFormHeader.Text = "NOA Glivec Applicant Information";
                    }
                }
                else
                {
                    LabelFormHeader.Text = "GIPAP Applicant Information";
                }
            }
            else
            {
                LabelFormHeader.Text = "GIPAP Applicant Information";
            }
            //noa tasigna is separate now
            int tas = Convert.ToInt32(ds.Tables[1].Rows[0]["tasigna"]);
            if (tas > 0)
            {
                LabelTreatment.Visible = true;
                rblstTreatment.Visible = true;
            }
            else
            {
                LabelTreatment.Visible = false;
                rblstTreatment.SelectedIndex = 0;
                rblstTreatment.Visible = false;
                LabelTasignaCML.Visible = false;
            }
        }
    }
    protected void ButtonContinue_Click(object sender, EventArgs e)
    {
        if (rblstTreatment.SelectedValue == "Tasigna" && cboDiagnosis.SelectedValue != "CML")
        {
            LabelTasignaCML.Visible = true;
            return;
        }
        if (rblstTreatment.SelectedValue == "Tasigna")
        {
            LabelFormHeader.Text = "NOA Tasigna Applicant Information";
        }
        PanelIntro.Visible = false;
        PanelPatient.Visible = true;
        this.ShowIntroSummary();
        PanelValidation.Visible = true;
    }
    protected void ButtonContinueContact_Click(object sender, EventArgs e)
    {
        if (this.ShowPatientSummary() == 0)
        {
            return;
        }
        LabelErrorPatient.Text = "";
        lbEditPatient.Visible = true;
        PanelPatient.Visible = false;
        this.loadDiag();
    }


    protected void rblstAdj_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblstAdj.SelectedIndex == 1)
        {
            cboDosage.Items.Remove("200mg");
            cboDosage.Items.Remove("260mg");
            cboDosage.Items.Remove("600mg");
            cboDosage.Items.Remove("800mg");
            //cboDosage.SelectedValue = "300mg";
            cboDosage.Enabled = true;
            //adjuvant = true;
        }
        else
        {
            cboDosage.Enabled = true;
            //adjuvant = false;
            //RequiredFieldValidator151.ErrorMessage = null;
            //ValidationSummary1.HeaderText = null;
        }
    }


    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(sessUse.HomePage);
    }
    protected void ButtonContinueDiag_Click(object sender, EventArgs e)
    {
        if (this.ShowDiagSummary() == 0)
        {
            return;
        }
        LabelErrorDiag.Text = "";
        lbEditDiag.Visible = true;

        this.hideDiag();

        this.loadFinancial();
    }
    protected void ButtonContinueFinancial_Click(object sender, EventArgs e)
    {
        this.ShowFinancialSummary();
        PanelSummary.Visible = true;
        LabelSummaryPatient2.Text = LabelSummaryPatient.Text;
        LabelSummaryDiag2.Text = LabelSummaryDiag.Text;

        PanelValidation.Visible = false;
        this.hideFinancial();
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        if (this.FillApplicantObject(myApplicant) == 0)
        {
            return;
        }
        string Emess = LabelSummaryPatient2.Text + LabelSummaryDiag2.Text + LabelSummaryFin.Text;
        if (Emess.IndexOf("GIPAP/NOA Physician Name:") != -1)
        {
            myApplicant.FlagNOA = true;
            if (rblstTreatment.SelectedValue == "Tasigna")
            {
                Esubject = Esubject.Replace("GIPAP", "NOA-Tasigna");
            }
            else
            {
                Esubject = Esubject.Replace("GIPAP", "NOA/GIPAP");
            }
        }
        else
        {
            myApplicant.FlagNOA = false;
        }
        Emess = Emess.Replace("<h4>", "");
        Emess = Emess.Replace("</h4>", "");
        Emess = Emess.Replace("<font class='lbl'>", "");
        Emess = Emess.Replace("</font>", "");
        Emess = Emess.Replace("<br />", " \n");
        Emess = Emess.Replace("<hr>", " \n\n");
        Esubject = Esubject + cboCountry.SelectedItem.Text + " - " + txtFirstName.Text + " " + txtLastName.Text;
        myApplicant.CreateApplicant(Emess, Esubject, Uname);
        if (sessUse.Role == "TMFUser")
        {
            Response.Redirect("NewApplicant.aspx?choice=" + myApplicant.ApplicantID.ToString());
        }
        else
        {
            PanelSummary.Visible = false;
            PanelReceived.Visible = true;
        }
    }
    protected void lbEditIntro_Click(object sender, EventArgs e)
    {
        LabelSummaryDiag.Text = "";
        lbEditDiag.Visible = false;
        LabelSummaryPatient.Text = "";
        lbEditPatient.Visible = false;
        PanelValidation.Visible = false;
        PanelIntro.Visible = true;
        PanelPatient.Visible = false;
        this.hideDiag();
    }
    protected void lbEditPatient_Click(object sender, EventArgs e)
    {
        LabelSummaryDiag.Text = "";
        lbEditDiag.Visible = false;
        LabelSummaryPatient.Text = "";
        lbEditPatient.Visible = false;
        PanelIntro.Visible = false;
        PanelPatient.Visible = true;
        this.hideDiag();
    }
    protected void lbEditDiag_Click(object sender, EventArgs e)
    {
        LabelSummaryDiag.Text = "";
        lbEditDiag.Visible = false;
        this.loadDiag();
        this.hideFinancial();
    }
    protected void lbEditPatient2_Click(object sender, EventArgs e)
    {
        PanelSummary.Visible = false;

        PanelPatient.Visible = true;

        PanelValidation.Visible = true;
        LabelSummaryDiag.Text = "";
        lbEditDiag.Visible = false;
        LabelSummaryPatient.Text = "";
        lbEditPatient.Visible = false;
    }
    protected void lbEditDiag2_Click(object sender, EventArgs e)
    {
        PanelSummary.Visible = false;

        PanelValidation.Visible = true;
        LabelSummaryDiag.Text = "";
        lbEditDiag.Visible = false;

        this.loadDiag();
    }
    protected void lbEditFin_Click(object sender, EventArgs e)
    {
        PanelSummary.Visible = false;

        PanelValidation.Visible = true;

        this.loadFinancial();
    }
    protected void cboDosage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDosage.SelectedValue == "400mg" || cboDosage.SelectedValue == "600mg" || cboDosage.SelectedValue == "800mg")
        {
            LabelTablet.Visible = true;
            dropTabletStrength.Visible = true;
            dropTabletStrength.Items.Clear();
            if (cboDosage.SelectedValue == "400mg")
            {
                dropTabletStrength.Items.Add("1 x 400mg");
                dropTabletStrength.Items.Add("4 x 100mg");
            }
            else if (cboDosage.SelectedValue == "600mg")
            {
                dropTabletStrength.Items.Add("1 x 400mg + 2 x 100mg");
                dropTabletStrength.Items.Add("6 x 100mg");
            }
            else if (cboDosage.SelectedValue == "800mg")
            {
                dropTabletStrength.Items.Add("2 x 400mg");
                dropTabletStrength.Items.Add("8 x 100mg");
            }
        }
        else
        {
            dropTabletStrength.Items.Clear();
            LabelTablet.Visible = false;
            dropTabletStrength.Visible = false;
        }
    }

  
}
