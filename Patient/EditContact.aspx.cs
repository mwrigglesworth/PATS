using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Patient_EditContact : System.Web.UI.Page
{
    public string Action = "Add";
    public string patientid;
    int choice;
    public string contactid;
    int cid;
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Contact myContact = new GIPAP_Objects.Contact();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        patientid = choice.ToString();
        cid = Convert.ToInt32(Request.QueryString["cid"]);
        contactid = cid.ToString();
        Action = Request.QueryString["action"];

        myContact = new GIPAP_Objects.Contact(cid, choice);
        if (Action != "submit")
        {
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
            if (cid != 0)
            {
                txtContFirstName.Text = myContact.FirstName;
                txtContLastName.Text = myContact.LastName;
                txtContFirstName.Enabled = false;
                txtContLastName.Enabled = false;
                txtPhone.Text = myContact.Phone;
                txtFax.Text = myContact.Fax;
                txtEmail.Text = myContact.Email;
                txtMobile.Text = myContact.Mobile;
                txtStreet1.Text = myContact.Street1;
                txtStreet2.Text = myContact.Street2;
                txtCity.Text = myContact.City;
                txtState.Text = myContact.StateProvince;
                txtPostalCode.Text = myContact.PostalCode;
                if (myContact.Sex != ' ')
                {
                    try
                    {
                        rblstSex.SelectedValue = myContact.Sex.ToString();
                    }
                    catch { }
                }
                dropCountry.SelectedValue = myContact.CountryID.ToString();
                try
                {
                    dropRelationship.SelectedValue = myContact.Relationship;
                }
                catch { }
                txtRelDetails.Text = myContact.RelationshipDetails;
            }
        }

        if (Action == "submit")
        {
            myContact.FirstName = txtContFirstName.Text;
            myContact.LastName = txtContLastName.Text;
            myContact.Phone = txtPhone.Text;
            myContact.Fax = txtFax.Text;
            myContact.Email = txtEmail.Text;
            myContact.Mobile = txtMobile.Text;
            myContact.Street1 = txtStreet1.Text;
            myContact.Street2 = txtStreet2.Text;
            myContact.City = txtCity.Text;
            myContact.StateProvince = txtState.Text;
            myContact.PostalCode = txtPostalCode.Text;
            if (rblstSex.SelectedIndex != -1)
            {
                myContact.Sex = Convert.ToChar(rblstSex.SelectedValue);
            }
            myContact.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
            myContact.Relationship = dropRelationship.SelectedValue;
            myContact.RelationshipDetails = txtRelDetails.Text;
            if (cid == 0)
            {
                myContact.Create(sessUse.Username, choice);
            }
            else
            {
                myContact.Update(sessUse.Username, choice);
            }
          //  Response.Redirect("ContactInfo.aspx?choice=" + choice.ToString() + "&cid=" + myContact.ContactID.ToString());
        }
    }
    //protected void ButtonSave_Click(object sender, EventArgs e)
    //{
    //    myContact.FirstName = txtFirstName.Text;
    //    myContact.LastName = txtLastName.Text;
    //    myContact.Phone = txtPhone.Text;
    //    myContact.Fax = txtFax.Text;
    //    myContact.Email = txtEmail.Text;
    //    myContact.Mobile = txtMobile.Text;
    //    myContact.Street1 = txtStreet1.Text;
    //    myContact.Street2 = txtStreet2.Text;
    //    myContact.City = txtCity.Text;
    //    myContact.StateProvince = txtState.Text;
    //    myContact.PostalCode = txtPostalCode.Text;
    //    if (rblstSex.SelectedIndex != -1)
    //    {
    //        myContact.Sex = Convert.ToChar(rblstSex.SelectedValue);
    //    }
    //    myContact.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
    //    myContact.Relationship = dropRelationship.SelectedValue;
    //    myContact.RelationshipDetails = txtRelDetails.Text;
    //    if (cid == 0)
    //    {
    //        myContact.Create(sessUse.Username, choice);
    //    }
    //    else
    //    {
    //        myContact.Update(sessUse.Username, choice);
    //    }
    //    Response.Redirect("ContactInfo.aspx?choice=" + choice.ToString() + "&cid=" + myContact.ContactID.ToString());
    //}
    //protected void ButtonCancel_Click(object sender, EventArgs e)
    //{
    //    if (cid == 0)
    //    {
    //        Response.Redirect("PatientInfo.aspx?choice=" + choice.ToString());
    //    }
    //    else
    //    {
    //        Response.Redirect("ContactInfo.aspx?choice=" + choice.ToString() + "&cid=" + cid.ToString());
    //    }
    //}

  
}
