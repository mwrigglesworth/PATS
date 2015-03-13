using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Clinic_ClinicSearch : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
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
        GIPAP_Objects.Clinic myClinic = new GIPAP_Objects.Clinic();
        myClinic.ClinicName = txtClinicName.Text;
        myClinic.Phone = txtClinicPhone.Text;
        myClinic.Fax = txtClinicFax.Text;
        myClinic.Email = txtClinicEmail.Text;
        myClinic.City = txtClinicCity.Text;
        if (dropCountry.SelectedIndex != 0)
        {
            myClinic.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        }

        myClinic.AdminFirstName = txtAdminFirstName.Text;
        myClinic.AdminLastName = txtAdminLastName.Text;
        myClinic.AdminPhone = txtAdminPhone.Text;
        myClinic.AdminFax = txtAdminFax.Text;
        myClinic.AdminEmail = txtAdminEmail.Text;
        myClinic.Approved = rblstApproved.SelectedIndex;

        dgResults.Visible = true;
        DataSet ds = myClinic.ClinicSearch(sessUse);
        dgResults.DataSource = ds;
        dgResults.DataBind();
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found";
    }
    protected void ButtonNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClinicSearch.aspx");
    }
}
