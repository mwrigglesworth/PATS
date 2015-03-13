using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Person_EditPerson : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myPerson = new GIPAP_Objects.Person(choice, sessUse.Role);
        if (myPerson.PersonType == "TMFUser")
        {
            LabelH1.Text = "Edit PO Information";
        }
        else if (myPerson.PersonType == "MaxStation")
        {
            LabelH1.Text = "Edit Max Station Information";
        }
        else if (myPerson.PersonType == "Novartis")
        {
            LabelH1.Text = "Edit Novartis Information";
        }
        if (!Page.IsPostBack)
        {
            this.FillCountryComboBox();
            txtFirstName.Text = myPerson.FirstName;
            txtLastName.Text = myPerson.LastName;
            txtPhone.Text = myPerson.Phone;
            txtFax.Text = myPerson.Fax;
            txtEmail.Text = myPerson.Email;
            txtMobile.Text = myPerson.Mobile;
            txtStreet1.Text = myPerson.Street1;
            txtStreet2.Text = myPerson.Street2;
            txtCity.Text = myPerson.City;
            txtState.Text = myPerson.StateProvince;
            txtPostalCode.Text = myPerson.PostalCode;
            if (myPerson.Sex != ' ')
            {
                rblstSex.SelectedValue = myPerson.Sex.ToString();
            }
            if (sessUse.Role == "TMFUser")
            {
                txtNotes.Text = myPerson.Notes;
            }
            else
            {
                LabelNotes.Visible = false;
                txtNotes.Visible = false;
            }

            dropCountry.SelectedValue = myPerson.CountryID.ToString();
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
        myPerson.FirstName = txtFirstName.Text;
        myPerson.LastName = txtLastName.Text;
        myPerson.Phone = txtPhone.Text;
        myPerson.Fax = txtFax.Text;
        myPerson.Email = txtEmail.Text;
        myPerson.Mobile = txtMobile.Text;
        myPerson.Street1 = txtStreet1.Text;
        myPerson.Street2 = txtStreet2.Text;
        myPerson.City = txtCity.Text;
        myPerson.StateProvince = txtState.Text;
        myPerson.PostalCode = txtPostalCode.Text;
        if (rblstSex.SelectedIndex != -1)
        {
            myPerson.Sex = Convert.ToChar(rblstSex.SelectedValue);
        }
        myPerson.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        if (sessUse.Role == "TMFUser")
        {
            myPerson.Notes = txtNotes.Text;
        }

        myPerson.Update(sessUse.Username);
        Response.Redirect("PersonInfo.aspx?a=edit&choice=" + myPerson.PersonID.ToString());
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PersonInfo.aspx?choice=" + myPerson.PersonID.ToString());
    }
}
