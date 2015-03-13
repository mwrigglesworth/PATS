using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Physician_EditPhysician : System.Web.UI.Page
{
    GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();
    GIPAP_Objects.User sessUse;
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myPhysician = new GIPAP_Objects.Physician(choice, sessUse.Role);
        if (!Page.IsPostBack)
        {
            this.FillCountryComboBox();
            for (int i = 2008; i < DateTime.Today.Year + 3; i++)
            {
                dropNOAYear.Items.Add(i.ToString());
            }
            //lbedit
            txtFirstName.Text = myPhysician.FirstName;
            txtLastName.Text = myPhysician.LastName;
            try
            {
                dropSpecialty.SelectedValue = myPhysician.Specialty;
            }
            catch { }
            txtPhone.Text = myPhysician.Phone;
            txtFax.Text = myPhysician.Fax;
            txtEmail.Text = myPhysician.Email;
            txtMobile.Text = myPhysician.Mobile;
            txtStreet1.Text = myPhysician.Street1;
            txtStreet2.Text = myPhysician.Street2;
            txtCity.Text = myPhysician.City;
            txtState.Text = myPhysician.StateProvince;
            txtPostalCode.Text = myPhysician.PostalCode;
            //LabelClinic.Text = myPhysician.ClinicName;
            rblstComputerAccess.SelectedIndex = myPhysician.ComputerAccess;
            rblstNOA.SelectedIndex = Convert.ToInt32(myPhysician.NOA);
            rblstNOA.Enabled = myPhysician.NOAGlivecCountry;
            try
            {
                dropNOAYear.SelectedValue = myPhysician.NOADate.Year.ToString();
                dropNOAMonth.SelectedValue = myPhysician.NOADate.Month.ToString();
                DropNOADay.SelectedValue = myPhysician.NOADate.Day.ToString();
            }
            catch { }
            DateTime dtnow = DateTime.Now;
            LabelNOADate.Text = "West Coast Date: <b>" + dtnow.Day.ToString() + " " + dtnow.ToString("y") + "</b><br>DATES ARE LOGGED IN WEST COAST TIME.  Please be advised that setting the NOA Date in the future will cause all Glivec applications entered under this physician to be entered as GIPAP until the date specified.";
            /*if (myPhysician.NOA)
            {
                rblstTasigna.Enabled = true;
            }*/
            rblstTasigna.SelectedIndex = myPhysician.Tasigna;
            if (myPhysician.NOATasignaCountry == 0)
            {
                rblstTasigna.Enabled = false;
            }
            else if (myPhysician.NOATasignaCountry == 1)
            {
                rblstTasigna.Items.Remove("2nd Line Only");
            }
            else if (myPhysician.NOATasignaCountry == 2)
            {
                rblstTasigna.Items.Remove("1st + 2nd Line");
            }

            if (myPhysician.Sex != ' ')
            {
                rblstSex.SelectedValue = myPhysician.Sex.ToString();
            }
            dropCountry.SelectedValue = myPhysician.CountryID.ToString();
            if (sessUse.Role == "TMFUser")
            {
                txtNotes.Text = myPhysician.Notes;
            }
            else if (sessUse.Role == "Novartis")
            {
                LabelNotes.Visible = false;
                txtNotes.Visible = false;
                PanelNOA.Visible = false;
                PanelTasigna.Visible = false;

                txtFirstName.Visible = false;
                LabelFirstName.Text = myPhysician.FirstName;
                txtLastName.Visible = false;
                LabelLastName.Text = myPhysician.LastName;
                dropCountry.Visible = false;
                LabelCountry.Text = myPhysician.mCountryName;
                txtEmail.Visible = false;
                LabelEmail.Text = myPhysician.Email;
                LabelCompAcc.Visible = false;
                rblstComputerAccess.Visible = false;
            }
            else if (sessUse.Role == "MaxStation")
            {
                txtNotes.Text = myPhysician.Notes;
                txtFirstName.Visible = false;
                LabelFirstName.Text = myPhysician.FirstName;
                txtLastName.Visible = false;
                LabelLastName.Text = myPhysician.LastName;
                dropCountry.Visible = false;
                LabelCountry.Text = myPhysician.mCountryName;
                PanelNOA.Visible = false;
                PanelTasigna.Visible = false;
            }
            else
            {
                LabelNotes.Visible = false;
                txtNotes.Visible = false;
                LabelCompAcc.Visible = false;
                rblstComputerAccess.Visible = false;
                PanelNOA.Visible = false;
                PanelTasigna.Visible = false;
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
        if (dropCountry.Visible)
        {
            myPhysician.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        }
        if (rblstSex.SelectedIndex != -1)
        {
            myPhysician.Sex = Convert.ToChar(rblstSex.SelectedValue);
        }
        if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
        {
            myPhysician.Notes = txtNotes.Text;
        }
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

        myPhysician.Update(sessUse.Username, sessUse.Role);
        Response.Redirect("PhysicianInfo.aspx?a=edit&choice=" + myPhysician.PhysicianID.ToString());
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PhysicianInfo.aspx?choice=" + myPhysician.PhysicianID.ToString());
    }
    protected void rblstNOA_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*if (rblstNOA.SelectedIndex == 1)
        {
            rblstTasigna.Enabled = true;
        }
        else
        {
            rblstTasigna.SelectedIndex = 0;
            rblstTasigna.Enabled = false;
        }*/
    }
}
