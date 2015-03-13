using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Distributor_DistributorSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
            dropCountry.DataSource = myCountry.GetCountryList(false);
            dropCountry.DataValueField = "CountryID";
            dropCountry.DataTextField = "CountryName";
            dropCountry.DataBind();
            dropCountry.Items.Insert(0, "Select a country");
            dropCountry.SelectedItem.Value = "0";
        }
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        GIPAP_Objects.Distributor myDistributor = new GIPAP_Objects.Distributor();
        myDistributor.OfficeName = txtName.Text;
        myDistributor.Phone = txtPhone.Text;
        myDistributor.Fax = txtFax.Text;
        myDistributor.Email = txtEmail.Text;
        myDistributor.City = txtCity.Text;
        if (dropCountry.SelectedIndex != 0)
        {
            myDistributor.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        }

        myDistributor.AdminFirstName = txtAdminFirstName.Text;
        myDistributor.AdminLastName = txtAdminLastName.Text;
        myDistributor.AdminPhone = txtAdminPhone.Text;
        myDistributor.AdminFax = txtAdminFax.Text;
        myDistributor.AdminEmail = txtAdminEmail.Text;
        myDistributor.Approved = rblstApproved.SelectedIndex;

        dgResults.Visible = true;
        DataSet ds = myDistributor.DistributorSearch();
        dgResults.DataSource = ds;
        dgResults.DataBind();
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found";
    }
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("DistributorSearch.aspx");
    }
}