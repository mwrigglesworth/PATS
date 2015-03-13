using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Physician_PhysicianSearch : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
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

        for (int i = 2004; i <= Convert.ToInt32(DateTime.Now.Year); i++)
        {
            dropApprovedYear.Items.Add(i.ToString());
            dropApprovedYearThru.Items.Add(i.ToString());
        }
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
        GIPAP_Objects.Physician myPhysician = new GIPAP_Objects.Physician();
        string extra = "";
        try
        {
            string min = dropApprovedMonth.SelectedItem.Value + "/" + dropApprovedDay.SelectedItem.Text + "/" + dropApprovedYear.SelectedItem.Text;
            Convert.ToDateTime(min);
            try
            {
                extra += "approveddate >= '" + min + "' and ";
                string max = dropApprovedMonthThru.SelectedItem.Value + "/" + dropApprovedDayThru.SelectedItem.Text + "/" + dropApprovedYearThru.SelectedItem.Text;
                Convert.ToDateTime(max);
                extra += "approveddate <= '" + max + "' and ";
            }
            catch
            {
                extra += "approveddate > '" + min + "' and ";
                min = Convert.ToDateTime(min).AddDays(1).ToString();
                extra += "approveddate < '" + min + "' and ";
            }
        }
        catch { }
        myPhysician.FirstName = txtFirstName.Text;
        myPhysician.LastName = txtLastName.Text;
        if (dropSpecialty.SelectedIndex != 0)
        {
            myPhysician.Specialty = dropSpecialty.SelectedValue;
        }
        myPhysician.Phone = txtPhone.Text;
        myPhysician.Fax = txtFax.Text;
        myPhysician.Email = txtEmail.Text;
        myPhysician.Mobile = txtMobile.Text;
        myPhysician.Street1 = txtStreet1.Text;
        myPhysician.Street2 = txtStreet2.Text;
        myPhysician.City = txtCity.Text;
        myPhysician.StateProvince = txtState.Text;
        myPhysician.PostalCode = txtPostalCode.Text;
        myPhysician.UserName = txtUsername.Text.ToString();
        myPhysician.ComputerAccess = rblstComputerAccess.SelectedIndex;
        if (dropCountry.SelectedIndex != 0)
        {
            myPhysician.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        }
        myPhysician.Approved = rblstApproved.SelectedIndex;

        this.showResults(myPhysician.PhysicianSearch(extra, sessUse));
    }
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("PhysicianSearch.aspx");
    }
}
