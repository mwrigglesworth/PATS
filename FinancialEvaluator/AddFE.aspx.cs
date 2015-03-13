using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FinancialEvaluator_AddFE : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.FCOffice myOffice = new GIPAP_Objects.FCOffice();

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
        myOffice.OfficeName = txtOfficeName.Text.Trim();
        myOffice.OfficeType = rblstType.SelectedValue;
        myOffice.Street1 = txtAddress1.Text.Trim();
        myOffice.Street2 = txtAddress2.Text.Trim();
        myOffice.City = txtCity.Text.Trim();
        myOffice.StateProvince = txtState.Text.Trim();
        myOffice.PostalCode = txtPostalCode.Text.Trim();
        myOffice.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        myOffice.Phone = txtPhone.Text.Trim();
        myOffice.Fax = txtFax.Text.Trim();
        myOffice.Email = txtEmail.Text.Trim();
        //admin
        myOffice.AdminFirstName = txtAdminFirstName.Text.Trim();
        myOffice.AdminLastName = txtAdminLastName.Text.Trim();
        myOffice.AdminPhone = txtAdminPhone.Text.Trim();
        myOffice.AdminFax = txtAdminFax.Text.Trim();
        myOffice.AdminEmail = txtAdminEmail.Text.Trim();
        myOffice.AdminMobile = txtAdminMobile.Text.Trim();
        myOffice.Create(sessUse.Username);
        Response.Redirect("FEInfo.aspx?a=add&choice=" + myOffice.FCOfficeID.ToString());
    }
}
