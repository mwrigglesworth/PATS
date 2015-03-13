using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Clinic_EditClinic : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Clinic myClinic = new GIPAP_Objects.Clinic();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);

        myClinic = new GIPAP_Objects.Clinic(choice, sessUse.Role);

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

            txtClinicName.Text = myClinic.ClinicName;
            txtClinicPhone.Text = myClinic.Phone;
            txtClinicFax.Text = myClinic.Fax;
            txtClinicEmail.Text = myClinic.Email;

            txtClinicAddress1.Text = myClinic.Street1;
            txtClinicAddress2.Text = myClinic.Street2;
            txtClinicCity.Text = myClinic.City;
            txtClinicState.Text = myClinic.StateProvince;
            txtClinicDepartment.Text = myClinic.Department;
            txtPostalCode.Text = myClinic.PostalCode;
            dropCountry.SelectedValue = myClinic.CountryID.ToString();

            txtAdminFirstName.Text = myClinic.AdminFirstName;
            txtAdminLastName.Text = myClinic.AdminLastName;
            txtAdminPhone.Text = myClinic.AdminPhone;
            txtAdminFax.Text = myClinic.AdminFax;
            txtAdminEmail.Text = myClinic.AdminEmail;
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

        myClinic.Update(sessUse.Username);
        Response.Redirect("ClinicInfo.aspx?a=edit&choice=" + myClinic.ClinicID.ToString());
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClinicInfo.aspx?choice=" + myClinic.ClinicID.ToString());
    }
}
