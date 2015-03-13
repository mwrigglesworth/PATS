using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_EditPatient : System.Web.UI.Page
{
    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        myPatient.InflateDiagnosisInfo(Convert.ToInt32(Request.QueryString["choice"]));

        if (myPatient.CountryID == 162)
            thai.Visible = true;
        else
            thai.Visible = false;

        if (!Page.IsPostBack)
        {
            txtFirstName.Text = myPatient.FirstName;
            txtLastName.Text = myPatient.LastName;
            txtThaiName.Text = myPatient.ThaiName;
            txtStreet1.Text = myPatient.Street1;
            txtStreet2.Text = myPatient.Street2;
            txtCity.Text = myPatient.City;
            txtState.Text = myPatient.StateProvince;
            txtPostal.Text = myPatient.PostalCode;
            rblstPatientConsent.Items[1].Selected = myPatient.PatientConsent;
            if (myPatient.FlagNOA && myPatient.CountryID == 76)
            {
                rblstPatientConsent.Enabled = false;
            }

            for (int i = Convert.ToInt32(DateTime.Today.Year.ToString()); i >= 1900; i--)
            {
                cboBirthYear.Items.Add(i.ToString());
                dropGlivecStartYear.Items.Add(i.ToString());
                dropDiagYear.Items.Add(i.ToString());
                dropTasignaDiagYear.Items.Add(i.ToString());
                dropTasignaStartYear.Items.Add(i.ToString());
            }
            cboBirthYear.Items.Insert(0, "Year");
            dropGlivecStartYear.Items.Insert(0, "Year");

            this.FillCountryComboBox();
            dropCountry.SelectedValue = myPatient.CountryID.ToString();
            cboBirthDay.SelectedValue = myPatient.BirthDate.Day.ToString();
            cboBirthMonth.SelectedIndex = Convert.ToInt32(myPatient.BirthDate.Month.ToString());
            cboBirthYear.SelectedValue = myPatient.BirthDate.Year.ToString();


            txtPhone.Text = myPatient.Phone;
            txtFax.Text = myPatient.Fax;
            txtEmail.Text = myPatient.Email;
            txtMobile.Text = myPatient.Mobile;

            rblstSex.SelectedValue = myPatient.Sex;
            /*set other valuse on panelEdit, then check usertype*/
            txtIncome.Text = myPatient.AnnualIncome;
            try
            {
                dropOccupation.SelectedValue = myPatient.Occupation;
            }
            catch { }
            //glivec
            if (myPatient.Treatment == "Glivec")
            {
                PanelGlivec.Visible = true;
                if (myPatient.GlivecStartDate != Convert.ToDateTime("1/1/1900"))
                {
                    try
                    {
                        dropGlivecStartDay.SelectedValue = myPatient.GlivecStartDate.Day.ToString();
                        dropGlivecStartMonth.SelectedValue = myPatient.GlivecStartDate.Month.ToString();
                        dropGlivecStartYear.SelectedValue = myPatient.GlivecStartDate.Year.ToString();
                    }
                    catch { }
                }
                try
                {
                    if (myPatient.Glivec)
                    {
                        rblstGlivec.SelectedIndex = 1;
                    }
                    else
                    {
                        rblstGlivec.SelectedIndex = 0;
                    }
                }
                catch { }
                if (myPatient.DiagnosisDate != Convert.ToDateTime("1/1/1900"))
                {
                    try
                    {
                        dropDiagDay.SelectedValue = myPatient.DiagnosisDate.Day.ToString();
                        dropDiagMonth.SelectedValue = myPatient.DiagnosisDate.Month.ToString();
                        dropDiagYear.SelectedValue = myPatient.DiagnosisDate.Year.ToString();
                    }
                    catch { }
                }
            }
            //TASIGNA
            else if (myPatient.Treatment == "Tasigna")
            {
                PanelTasigna.Visible = true;
                rblstTasignaGlivec.SelectedIndex = Convert.ToInt32(myPatient.Glivec);
                rblstTasignaImatinib.SelectedIndex = Convert.ToInt32(myPatient.Imatinib);
                rblstGlivecIntolerant.SelectedIndex = Convert.ToInt32(myPatient.GlivecIntolerant);
                rblstGlivecResistant.SelectedIndex = Convert.ToInt32(myPatient.GlivecResistant);
                rblstDasatinib.SelectedIndex = Convert.ToInt32(myPatient.Dasatinib);
                rblstDasatinibIntolerant.SelectedIndex = Convert.ToInt32(myPatient.DasatinibIntolerant);
                rblstDasatinibResistant.SelectedIndex = Convert.ToInt32(myPatient.DasatinibResistant);
                rblstTasigna.SelectedIndex = Convert.ToInt32(myPatient.Tasigna);
                rblstNOATasigna.SelectedIndex = Convert.ToInt32(myPatient.NOATasigna);
                rblstTasignaPatientConsent.SelectedIndex = Convert.ToInt32(myPatient.TasignaPatientConsent);
                if (myPatient.Tasigna)
                {
                    dropTasignaStartDay.SelectedValue = myPatient.TasignaStartDate.Day.ToString();
                    dropTasignaStartMonth.SelectedValue = myPatient.TasignaStartDate.Month.ToString();
                    dropTasignaStartYear.SelectedValue = myPatient.TasignaStartDate.Year.ToString();
                }
                dropTasignaDiagDay.SelectedValue = myPatient.DiagnosisDate.Day.ToString();
                dropTasignaDiagMonth.SelectedValue = myPatient.DiagnosisDate.Month.ToString();
                dropTasignaDiagYear.SelectedValue = myPatient.DiagnosisDate.Year.ToString();
            }

            if (myPatient.Diagnosis == "CML")
            {
                PanelCML.Visible = true;
                PanelPH.Visible = true;
                rblstPhilPos.SelectedIndex = myPatient.PhilPos;
                rblstBCR.SelectedIndex = myPatient.BCR;
                try
                {
                    dropCMLPhase.SelectedValue = myPatient.CurrentCMLPhase;
                }
                catch { }
                this.setRblst(myPatient.Interferon, rblstInterferon);
                try
                {
                    dropInterferonTime.SelectedValue = myPatient.InterferonTimeLength;
                }
                catch { }
                this.setRblst(myPatient.Intolerant, rblstIntolerant);
                this.setRblst(myPatient.CytogeneticFailure, rblstCyto);
                this.setRblst(myPatient.HematologicFailure, rblstHema);
            }
            else if (myPatient.Diagnosis == "Ph+ ALL")
            {
                PanelPH.Visible = true;
                PanelPHAll.Visible = true;
                rblstPhilPos.SelectedIndex = myPatient.PhilPos;
                rblstBCR.SelectedIndex = myPatient.BCR;
                rblstRelapsedRefractory.SelectedIndex = myPatient.RelapsedRefractory;
                rblstChemo.SelectedIndex = myPatient.Chemo;
            }
            else if (myPatient.Diagnosis == "DFSP")
            {
                PanelDFSP.Visible = true;
                this.setRblst(myPatient.Recurrent, rblstRecurrent);
            }
            /*else if (myPatient.Diagnosis == "Adjuvant GIST")
            {
                PanelAdjGist.Visible = true;
                PanelGIST.Visible = true;
                rblstHighRisk.SelectedIndex = myPatient.HighRisk;
                rblstCkit.SelectedIndex = myPatient.CKitPos;
                rblstUnresectable.SelectedIndex = myPatient.Unresectable;
                rblstMetastatic.SelectedIndex = myPatient.Metastatic;
            }*/
            else if (myPatient.Diagnosis == "MDS / MPD" || myPatient.Diagnosis == "Systemic Mastocytosis" || myPatient.Diagnosis == "HES / CEL")
            {
                PanelDiagSummary.Visible = true;
                txtDiagSummary.Text = myPatient.DiagSummary;
            }
            else if (myPatient.Diagnosis == "GIST")
            {
                PanelGIST.Visible = true;
                rblstCkit.SelectedIndex = myPatient.CKitPos;
                rblstUnresectable.SelectedIndex = myPatient.Unresectable;
                rblstMetastatic.SelectedIndex = myPatient.Metastatic;
                if (myPatient.adjCountry)
                {
                    PanelAdjGist.Visible = true;
                    rblstAdj.SelectedIndex = Convert.ToInt32(myPatient.Adjuvant);
                    rblstHighRisk.SelectedIndex = myPatient.HighRisk;
                    if (myPatient.CurrentDosage != "400mg")
                    {
                        LabelAdjuvantDose.Visible = true;
                        rblstAdj.Enabled = false;
                    }
                }
                else
                {
                    PanelAdjGist.Visible = false;
                }
            }

            if (sessUse.Role == "MaxStation")
            {
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                dropCountry.Enabled = false;
                cboBirthDay.Enabled = false;
                cboBirthMonth.Enabled = false;
                cboBirthYear.Enabled = false;
                dropOccupation.Enabled = false;
                PanelIncome.Visible = false;
            }
        }
    }
    //**********************************************************************************************************************
    private void setRblst(bool answer, System.Web.UI.WebControls.RadioButtonList rblst)
    {
        if (answer)
        {
            rblst.Items[1].Selected = true;
        }
        else
        {
            rblst.Items[0].Selected = true;
        }
    }
    //**********************************************************************************************************************
    private void setCalendar(DateTime dt, System.Web.UI.WebControls.Calendar cal, System.Web.UI.WebControls.Label lab)
    {
        cal.SelectedDate = dt;
        cal.VisibleDate = dt;
        cal.Visible = false;
        lab.Text = dt.Day.ToString() + " " + dt.ToString("y");
    }
    //**********************************************************************************************************************
    private void FillCountryComboBox()
    {
        //Fills the country combobox with the countries from the database
        DataSet ds;
        GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
        ds = myCountry.GetCountryList(false);
        //bind data to patient country
        dropCountry.DataSource = ds;
        dropCountry.DataValueField = "CountryID";
        dropCountry.DataTextField = "CountryName";
        dropCountry.DataBind();
        dropCountry.Items.Insert(0, "Select a country");
        dropCountry.SelectedItem.Value = "0";
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        myPatient.FirstName = txtFirstName.Text;
        myPatient.LastName = txtLastName.Text;
        myPatient.ThaiName = txtThaiName.Text;
        myPatient.Street1 = txtStreet1.Text;
        myPatient.Street2 = txtStreet2.Text;
        myPatient.City = txtCity.Text;
        myPatient.StateProvince = txtState.Text;
        myPatient.PostalCode = txtPostal.Text;
        myPatient.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        myPatient.Phone = txtPhone.Text;
        myPatient.Fax = txtFax.Text;
        myPatient.Email = txtEmail.Text;
        myPatient.Mobile = txtMobile.Text;
        myPatient.Sex = rblstSex.SelectedValue;
        myPatient.PatientConsent = rblstPatientConsent.Items[1].Selected;
        try
        {
            myPatient.BirthDate = Convert.ToDateTime(cboBirthMonth.SelectedValue + "/" + cboBirthDay.SelectedValue + "/" + cboBirthYear.SelectedValue);
        }
        catch { }

        if (myPatient.Diagnosis == "CML")
        {
            myPatient.PhilPos = rblstPhilPos.SelectedIndex;
            myPatient.BCR = rblstBCR.SelectedIndex;
            myPatient.CurrentCMLPhase = dropCMLPhase.SelectedValue;
            myPatient.Interferon = rblstInterferon.Items[1].Selected;
            myPatient.InterferonTimeLength = dropInterferonTime.SelectedValue;
            myPatient.Intolerant = rblstIntolerant.Items[1].Selected;
            myPatient.CytogeneticFailure = rblstCyto.Items[1].Selected;
            myPatient.HematologicFailure = rblstHema.Items[1].Selected;
        }
        else if (myPatient.Diagnosis == "Ph+ ALL")
        {
            myPatient.PhilPos = rblstPhilPos.SelectedIndex;
            myPatient.BCR = rblstBCR.SelectedIndex;
            myPatient.RelapsedRefractory = rblstRelapsedRefractory.SelectedIndex;
            myPatient.Chemo = rblstChemo.SelectedIndex;
        }
        else if (myPatient.Diagnosis == "DFSP")
        {
            myPatient.Recurrent = rblstRecurrent.Items[1].Selected;
        }
        /*else if (myPatient.Diagnosis == "Adjuvant GIST")
        {
            myPatient.HighRisk = rblstHighRisk.SelectedIndex;
            myPatient.CKitPos = rblstCkit.SelectedIndex;
            myPatient.Unresectable = rblstUnresectable.SelectedIndex;
            myPatient.Metastatic = rblstMetastatic.SelectedIndex;
        }*/
        else if (myPatient.Diagnosis == "MDS / MPD" || myPatient.Diagnosis == "Systemic Mastocytosis" || myPatient.Diagnosis == "HES / CEL")
        {
            myPatient.DiagSummary = txtDiagSummary.Text;
        }
        else if (myPatient.Diagnosis == "GIST")
        {
            myPatient.CKitPos = rblstCkit.SelectedIndex;
            myPatient.Unresectable = rblstUnresectable.SelectedIndex;
            myPatient.Metastatic = rblstMetastatic.SelectedIndex;
            myPatient.HighRisk = rblstHighRisk.SelectedIndex;
            myPatient.Adjuvant = rblstAdj.Items[1].Selected;
        }

        myPatient.AnnualIncome = txtIncome.Text;
        if (dropOccupation.SelectedIndex != 0)
        {
            myPatient.Occupation = dropOccupation.SelectedValue;
        }
        if (myPatient.Treatment == "Glivec")
        {
            try
            {
                myPatient.DiagnosisDate = Convert.ToDateTime(dropDiagMonth.SelectedValue + "/" + dropDiagDay.SelectedValue + "/" + dropDiagYear.SelectedValue);
            }
            catch { }
            try
            {
                myPatient.GlivecStartDate = Convert.ToDateTime(dropGlivecStartMonth.SelectedValue + "/" + dropGlivecStartDay.SelectedValue + "/" + dropGlivecStartYear.SelectedValue);
            }
            catch { }
            if (rblstGlivec.SelectedIndex != -1)
            {
                myPatient.Glivec = rblstGlivec.Items[1].Selected;
            }
        }
        else if (myPatient.Treatment == "Tasigna")
        {
            myPatient.DiagnosisDate = Convert.ToDateTime(dropTasignaDiagMonth.SelectedValue + "/" + dropTasignaDiagDay.SelectedValue + "/" + dropTasignaDiagYear.SelectedValue);
            myPatient.Glivec = rblstTasignaGlivec.Items[1].Selected;
            myPatient.Imatinib = rblstTasignaImatinib.Items[1].Selected;
            myPatient.GlivecIntolerant = rblstGlivecIntolerant.Items[1].Selected;
            myPatient.GlivecResistant = rblstGlivecResistant.Items[1].Selected;
            myPatient.Dasatinib = rblstDasatinib.Items[1].Selected;
            myPatient.DasatinibIntolerant = rblstDasatinibIntolerant.Items[1].Selected;
            myPatient.DasatinibResistant = rblstDasatinibResistant.Items[1].Selected;
            myPatient.Tasigna = rblstTasigna.Items[1].Selected;
            myPatient.NOATasigna = rblstNOATasigna.Items[1].Selected;
            myPatient.TasignaPatientConsent = rblstTasignaPatientConsent.Items[1].Selected;
            if (myPatient.Tasigna)
            {
                myPatient.TasignaStartDate = Convert.ToDateTime(dropTasignaStartMonth.SelectedValue + "/" + dropTasignaStartDay.SelectedValue + "/" + dropTasignaStartYear.SelectedValue);
            }
        }

        myPatient.Update(sessUse.Username, txtReasonForChanges.Text);
        Response.Redirect("PatientInfo.aspx?a=edit&choice=" + myPatient.PatientID.ToString());
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PatientInfo.aspx?&choice=" + myPatient.PatientID.ToString());
    }
}
