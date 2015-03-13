using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stockist_AddStockist : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Stockist myStockist = new GIPAP_Objects.Stockist();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        if (!Page.IsPostBack)
        {
            GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
            //bind data to country
            dropCountry.DataSource = myCountry.GetCountryList(false);
            dropCountry.DataValueField = "CountryID";
            dropCountry.DataTextField = "CountryName";
            dropCountry.DataBind();
            dropCountry.Items.Insert(0, "Select a country");
            dropCountry.SelectedItem.Value = "0";
        }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        myStockist.OfficeName = txtOfficeName.Text.Trim();
        myStockist.Street1 = txtAddress1.Text.Trim();
        myStockist.Street2 = txtAddress2.Text.Trim();
        myStockist.City = txtCity.Text.Trim();
        myStockist.StateProvince = txtState.Text.Trim();
        myStockist.PostalCode = txtPostalCode.Text.Trim();
        myStockist.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        myStockist.Phone = txtPhone.Text.Trim();
        myStockist.Fax = txtFax.Text.Trim();
        myStockist.Email = txtEmail.Text.Trim();
        //admin
        myStockist.AdminFirstName = txtAdminFirstName.Text.Trim();
        myStockist.AdminLastName = txtAdminLastName.Text.Trim();
        myStockist.AdminPhone = txtAdminPhone.Text.Trim();
        myStockist.AdminFax = txtAdminFax.Text.Trim();
        myStockist.AdminEmail = txtAdminEmail.Text.Trim();
        myStockist.AdminMobile = txtAdminMobile.Text.Trim();
        myStockist.Create(sessUse.Username);
        Response.Redirect("StockistInfo.aspx?a=add&choice=" + myStockist.StockistID.ToString());
    }
}
