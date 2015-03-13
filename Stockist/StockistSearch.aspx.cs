using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Stockist_StockistSearch : System.Web.UI.Page
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
        GIPAP_Objects.Stockist myStockist = new GIPAP_Objects.Stockist();
        myStockist.OfficeName = txtName.Text;
        myStockist.Phone = txtPhone.Text;
        myStockist.Fax = txtFax.Text;
        myStockist.Email = txtEmail.Text;
        myStockist.City = txtCity.Text;
        if (dropCountry.SelectedIndex != 0)
        {
            myStockist.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        }

        myStockist.AdminFirstName = txtAdminFirstName.Text;
        myStockist.AdminLastName = txtAdminLastName.Text;
        myStockist.AdminPhone = txtAdminPhone.Text;
        myStockist.AdminFax = txtAdminFax.Text;
        myStockist.AdminEmail = txtAdminEmail.Text;
        myStockist.Approved = rblstApproved.SelectedIndex;

        dgResults.Visible = true;
        DataSet ds = myStockist.StockistSearch();
        dgResults.DataSource = ds;
        dgResults.DataBind();
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found";
    }
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockistSearch.aspx");
    }
}
