using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Distributor_AddEditDistributor : System.Web.UI.Page
{
    public string Action = "Add";
    public int choice;
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Distributor myDistributor = new GIPAP_Objects.Distributor();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        Action = Request.QueryString["action"];
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
        if (Action == "Edit")
        {
            choice = Convert.ToInt32(Request.QueryString["choice"]);
            myDistributor = new GIPAP_Objects.Distributor(choice);

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

                txtOfficeName.Text = myDistributor.OfficeName;
                txtAddress1.Text = myDistributor.Street1;
                txtAddress2.Text = myDistributor.Street2;
                txtCity.Text = myDistributor.City;
                txtState.Text = myDistributor.StateProvince;
                txtPostalCode.Text = myDistributor.PostalCode;
                dropCountry.SelectedValue = myDistributor.CountryID.ToString();
                txtPhone.Text = myDistributor.Phone;
                txtFax.Text = myDistributor.Fax;
                txtEmail.Text = myDistributor.Email;
                //admin
                txtAdminFirstName.Text = myDistributor.AdminFirstName;
                txtAdminLastName.Text = myDistributor.AdminLastName;
                txtAdminPhone.Text = myDistributor.AdminPhone;
                txtAdminFax.Text = myDistributor.AdminFax;
                txtAdminEmail.Text = myDistributor.AdminEmail;
                txtAdminMobile.Text = myDistributor.AdminMobile;
            }
        }

    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        myDistributor.OfficeName = txtOfficeName.Text.Trim();
        myDistributor.Street1 = txtAddress1.Text.Trim();
        myDistributor.Street2 = txtAddress2.Text.Trim();
        myDistributor.City = txtCity.Text.Trim();
        myDistributor.StateProvince = txtState.Text.Trim();
        myDistributor.PostalCode = txtPostalCode.Text.Trim();
        myDistributor.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        myDistributor.Phone = txtPhone.Text.Trim();
        myDistributor.Fax = txtFax.Text.Trim();
        myDistributor.Email = txtEmail.Text.Trim();
        //admin
        myDistributor.AdminFirstName = txtAdminFirstName.Text.Trim();
        myDistributor.AdminLastName = txtAdminLastName.Text.Trim();
        myDistributor.AdminPhone = txtAdminPhone.Text.Trim();
        myDistributor.AdminFax = txtAdminFax.Text.Trim();
        myDistributor.AdminEmail = txtAdminEmail.Text.Trim();
        myDistributor.AdminMobile = txtAdminMobile.Text.Trim();
        if (choice > 0)
        {
            myDistributor.Update(sessUse.Username);
            Response.Redirect("DistributorInfo.aspx?a=edit&choice=" + myDistributor.DistributorID.ToString());
        }
        else
        {
            myDistributor.Create(sessUse.Username);
            Response.Redirect("DistributorInfo.aspx?a=add&choice=" + myDistributor.DistributorID.ToString());
        }
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DistributorInfo.aspx?choice=" + myDistributor.DistributorID.ToString());
    }
}