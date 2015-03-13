using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class FinancialEvaluator_FESearch : System.Web.UI.Page
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
        GIPAP_Objects.FCOffice myOffice = new GIPAP_Objects.FCOffice();
        myOffice.OfficeName = txtName.Text;
        myOffice.Phone = txtPhone.Text;
        myOffice.Fax = txtFax.Text;
        myOffice.Email = txtEmail.Text;
        myOffice.City = txtCity.Text;
        if (dropCountry.SelectedIndex != 0)
        {
            myOffice.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        }

        myOffice.AdminFirstName = txtAdminFirstName.Text;
        myOffice.AdminLastName = txtAdminLastName.Text;
        myOffice.AdminPhone = txtAdminPhone.Text;
        myOffice.AdminFax = txtAdminFax.Text;
        myOffice.AdminEmail = txtAdminEmail.Text;
        myOffice.Approved = rblstApproved.SelectedIndex;

        dgResults.Visible = true;
        DataSet ds = myOffice.FCOfficeSearch();
        dgResults.DataSource = ds;
        dgResults.DataBind();
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found";
    }
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("FESearch.aspx");
    }
}
