using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class patientservices_PSApplication : System.Web.UI.Page
{
    GIPAP_Objects.PSApplicant myApplicant = new GIPAP_Objects.PSApplicant();

    //**********************************************************************************************************************
    protected void Page_Load(object sender, EventArgs e)
    {
        //string rUrl = Request.Url.ToString();
        //if (rUrl.StartsWith("http://") && !rUrl.StartsWith("http://localhost") && !rUrl.StartsWith("http://webserv"))
        //{
        //    Response.Redirect(rUrl.Replace("http://", "https://"));
        //}
        if (!Page.IsPostBack)
        {
            this.FillCountryComboBox();
            this.FillYearComboBox();
        }
    }
    //**********************************************************************************************************************
    private void FillCountryComboBox()
    {
        //Fills the country combobox with the countries from the database
        //bind data to patient country
        GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
        //GIPAP_Objects.Country myCountry = new Country();
        dropPSCountry.DataSource = myCountry.GetCountryList(false);
        dropPSCountry.DataValueField = "CountryID";
        dropPSCountry.DataTextField = "CountryName";
        dropPSCountry.DataBind();
        dropPSCountry.Items.Insert(0, "Select a country");
        dropPSCountry.SelectedItem.Value = "0";
    }

    //**************************************************************************************************************
    private void FillYearComboBox()
    {
        DateTime dtNow = DateTime.Now;

        for (int i = dtNow.Year; i >= dtNow.Year-100; i--)
        {
            cboBirthYear.Items.Add(i.ToString());
            dropDiagnosisYear.Items.Add(i.ToString());            
        }
        cboBirthYear.Items.Insert(0, "Year");
        cboBirthYear.SelectedItem.Value = "0";
        dropDiagnosisYear.Items.Insert(0, "Year");
        dropDiagnosisYear.SelectedItem.Value = "0";
    }

    //**********************************************************************************************************************
    protected void ButtonPSCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://www.themaxfoundation.org");
    }

    //**********************************************************************************************************************
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //**********************************************************************************************************************
    protected void ButtonPSSubmit_Click(object sender, EventArgs e)
    {
        if (cbPrivacy.Checked)
        {
            LabelPriv.Visible = PanelPrivacy.Visible = false;
            if (Page.IsValid)
            {
                myApplicant.FirstName = txtPSFirstName.Text;
                myApplicant.LastName = txtPSLastName.Text;
                myApplicant.Gender = rbPSGender.SelectedValue;
                try
                {
                    myApplicant.BirthDate = Convert.ToDateTime(cboBirthMonth.SelectedValue + "/" + cboBirthDay.SelectedValue + "/" + cboBirthYear.SelectedValue);
                }
                catch
                {
                    /*Labelinvalid.Visible = true;
                    return;*/
                }
                myApplicant.Street1 = txtPSStreet1.Text;
                myApplicant.Street2 = txtPSStreet2.Text;
                myApplicant.City = txtPSCity.Text;
                myApplicant.StateProvince = txtPSState.Text;
                myApplicant.PostalCode = txtPSPostal.Text;
                myApplicant.CountryID = Convert.ToInt32(dropPSCountry.SelectedValue);
                myApplicant.CountryName = dropPSCountry.SelectedItem.Text;
                myApplicant.Phone = txtPSPhone.Text;
                myApplicant.Mobile = txtMobile.Text;
                myApplicant.Fax = txtPSFax.Text;
                myApplicant.Email = txtPSEmail.Text;
                myApplicant.ContactFirstName = txtPSContactFirst.Text;
                myApplicant.ContactLastName = txtPSContactLast.Text;
                myApplicant.ContactPhone = txtPSContactPhone.Text;
                myApplicant.ContactFax = txtPSContactFax.Text;
                myApplicant.ContactEmail = txtPSContactEmail.Text;
                myApplicant.Relationship = dropPSRelationship.SelectedValue;

                myApplicant.Diagnosis = dropDiagnosis.SelectedValue;
                if (dropDiagnosisYear.SelectedIndex != 0)
                {
                    myApplicant.DiagnosisYear = dropDiagnosisYear.SelectedValue;
                }
                /*if(dropMedication.SelectedIndex == 0 || dropMedication.SelectedIndex == 3)
                {
                    myApplicant.Medication = txtOtherMed.Text;
                }
                else
                {
                    myApplicant.Medication = dropMedication.SelectedValue;
                }*/
                myApplicant.Medication = "";// txtOtherMed.Text;

                /*myApplicant.PhysicianFirstName = txtPSPhysicianFirst.Text;
                myApplicant.PhysicianLastName = txtPSPhysicianLast.Text;
                myApplicant.ClinicName = txtPSClinic.Text;
                myApplicant.PhysicianPhone = txtPSPhysicianPhone.Text;
                myApplicant.PhysicianFax = txtPSPhysicianFax.Text;
                myApplicant.PhysicianEmail = txtPSPhysicianEmail.Text;
                myApplicant.ContactPhysician = rbPSContactPhysician.Items[0].Selected;

                myApplicant.TreatmentReferal = cbPSReferrals.Checked;
                myApplicant.Transportation = cbPSTransportation.Checked;
                myApplicant.StemCellTransplant = cbPSStemCell.Checked;
                myApplicant.EmotionalSupport = cbEmo.Checked;
                myApplicant.AccessMedication = cbMedication.Checked;
                myApplicant.OtherAssistance = txtPSOtherHelp.Text;*/

                myApplicant.Needs = txtPSNeeds.Text;
                myApplicant.FindOut = dropFindOut.SelectedValue;
                if (myApplicant.FindOut == "Other")
                {
                    myApplicant.FindOut = txtHearOther.Text;
                }
                myApplicant.PrivacyPractices = cbPrivacy.Checked;

                myApplicant.CreateApplicant();
                Response.Redirect("Confirmation.aspx");
            }
        }
        else
        {
            LabelPriv.Visible = PanelPrivacy.Visible = true;
        }
    }
}
