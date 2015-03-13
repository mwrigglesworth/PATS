using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Person_PersonSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelHeader.Text = Request.QueryString["persontype"].ToString() + " Search";
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
    //**********************************************************************************************************************
    private void showResults(DataSet ds)
    {
        dgResults.Visible = true;
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found";
        dgResults.DataSource = ds;
        dgResults.DataBind();
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person();
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
        if (dropCountry.SelectedIndex != 0)
        {
            myPerson.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        }

        this.showResults(myPerson.PersonSearch(Request.QueryString["persontype"].ToString(), rblstActive.SelectedIndex));
    }
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("PersonSearch.aspx?persontype=" + Request.QueryString["persontype"].ToString());
    }
}
