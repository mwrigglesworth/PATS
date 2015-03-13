using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Physician_AddPhysician : System.Web.UI.Page
{
    GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        if (!Page.IsPostBack)
        {
            this.FillCountryComboBox();
            for (int i = 2008; i < DateTime.Today.Year + 3; i++)
            {
                dropNOAYear.Items.Add(i.ToString());
            }
        }
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
    protected void rblstNOA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblstNOA.SelectedIndex == 1)
        {
            rblstTasigna.Enabled = true;
        }
        else
        {
            rblstTasigna.SelectedIndex = 0;
            rblstTasigna.Enabled = false;
        }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        myPhysician.FirstName = txtFirstName.Text;
        myPhysician.LastName = txtLastName.Text;
        myPhysician.Specialty = dropSpecialty.SelectedValue;
        myPhysician.Phone = txtPhone.Text;
        myPhysician.Fax = txtFax.Text;
        myPhysician.Email = txtEmail.Text.Replace(",", ";");
        myPhysician.Mobile = txtMobile.Text;
        myPhysician.Street1 = txtStreet1.Text;
        myPhysician.Street2 = txtStreet2.Text;
        myPhysician.City = txtCity.Text;
        myPhysician.StateProvince = txtState.Text;
        myPhysician.PostalCode = txtPostalCode.Text;
        myPhysician.ComputerAccess = rblstComputerAccess.SelectedIndex;
        if (rblstSex.SelectedIndex != -1)
        {
            myPhysician.Sex = Convert.ToChar(rblstSex.SelectedValue);
        }
        myPhysician.Notes = txtNotes.Text;
        myPhysician.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        //NOA fields
        myPhysician.NOA = rblstNOA.Items[1].Selected;
        try
        {
            myPhysician.NOADate = Convert.ToDateTime(dropNOAMonth.SelectedValue + "/" + DropNOADay.SelectedValue + "/" + dropNOAYear.SelectedValue);
        }
        catch { }
        if (rblstTasigna.SelectedValue == "1st + 2nd Line")
        {
            myPhysician.Tasigna = 1;
        }
        else if (rblstTasigna.SelectedValue == "2nd Line Only")
        {
            myPhysician.Tasigna = 2;
        }
        else
        {
            myPhysician.Tasigna = 0;
        }

        try
        {
            myPhysician.CreatePhysician(sessUse.Username);
        }
        catch (ArgumentException ex)
        {
            LabelError.Text = ex.Message.ToString();
            return;
        }
        Response.Redirect("PhysicianInfo.aspx?a=add&choice=" + myPhysician.PhysicianID.ToString());
    }
    protected void dropCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCountry.SelectedIndex > 0)
        {
            GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country(Convert.ToInt32(dropCountry.SelectedValue), sessUse.Role);
            if (myCountry.NOAGlivec)
            {
                rblstNOA.Enabled = true;
            }
            else
            {
                rblstNOA.Enabled = false;
                rblstNOA.SelectedIndex = 0;
            }
            rblstTasigna.SelectedIndex = 0;
            if (myCountry.NOATasigna == 0)
            {
                rblstTasigna.Enabled = false;
            }
            else if (myCountry.NOATasigna == 1)
            {
                rblstTasigna.Enabled = true;
                rblstTasigna.Items[2].Enabled = false;
            }
            else if (myCountry.NOATasigna == 2)
            {
                rblstTasigna.Enabled = true;
                rblstTasigna.Items[1].Enabled = false;
            }
        }
    }
}
