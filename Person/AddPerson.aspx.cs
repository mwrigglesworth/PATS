using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Person_AddPerson : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        LabelH1.Text = "Add " + Request.QueryString["persontype"].ToString();
        if (!Page.IsPostBack)
        {
            this.FillCountryComboBox();
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
        myPerson.PersonType = Request.QueryString["persontype"].ToString();
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
        try
        {
            myPerson.Create(sessUse.Username, Request.QueryString["persontype"]);
        }
        catch (ArgumentException ex)
        {
            LabelError.Text = ex.Message.ToString();
            return;
        }
        Response.Redirect("PersonInfo.aspx?choice=" + myPerson.PersonID.ToString()); 
    }
}
