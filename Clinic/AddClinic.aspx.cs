using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Clinic_AddClinic : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Clinic myClinic = new GIPAP_Objects.Clinic();

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
        myClinic.ClinicName = txtClinicName.Text;
        myClinic.Phone = txtClinicPhone.Text;
        myClinic.Fax = txtClinicFax.Text;
        myClinic.Email = txtClinicEmail.Text;

        myClinic.Department = txtClinicDepartment.Text;
        myClinic.Street1 = txtClinicAddress1.Text;
        myClinic.Street2 = txtClinicAddress2.Text;
        myClinic.City = txtClinicCity.Text;
        myClinic.StateProvince = txtClinicState.Text;
        myClinic.PostalCode = txtPostalCode.Text;
        myClinic.CountryID = Convert.ToInt32(dropCountry.SelectedValue);

        myClinic.AdminFirstName = txtAdminFirstName.Text;
        myClinic.AdminLastName = txtAdminLastName.Text;
        myClinic.AdminPhone = txtAdminPhone.Text;
        myClinic.AdminFax = txtAdminFax.Text;
        myClinic.AdminEmail = txtAdminEmail.Text;

        myClinic.Create(sessUse.Username);
        Response.Redirect("ClinicInfo.aspx?a=add&choice=" + myClinic.ClinicID.ToString());
    }
}
