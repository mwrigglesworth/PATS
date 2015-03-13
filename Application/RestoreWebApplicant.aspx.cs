using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Application_RestoreWebApplicant : System.Web.UI.Page
{
    GIPAP_Objects.GIPAPApplicant myApplicant;
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        myApplicant = new GIPAP_Objects.GIPAPApplicant(Convert.ToInt32(Request.QueryString["choice"]));
        LabelDiagInfo.Text = myApplicant.DiagnosisInfoTable();
        LabelFinancialInfo.Text = myApplicant.FinancialInfoTable();
        LabelInfo.Text = myApplicant.ApplicantInfo();
        LabelAddress.Text = myApplicant.AddressInfo();
        if (!Page.IsPostBack)
        {
            //set country
            this.FillCountryComboBox();
            dropCountry.SelectedValue = myApplicant.CountryID.ToString();
            LabelCountry.Text = dropCountry.SelectedItem.Text;
            //if (myApplicant.CountryID != 76) /*if not India, show Distributor list*/
            //{
            //    if (myApplicant.ApplicantDS.Tables[10] != null)
            //    {
            //        if (myApplicant.ApplicantDS.Tables[10].Rows.Count > 0)
            //        {
                       

            //            //set distributor
            //            dropDistributor.DataSource = myApplicant.ApplicantDS.Tables[10];
            //            dropDistributor.DataTextField = "OfficeName";
            //            dropDistributor.DataValueField = "DistributorID";
            //            dropDistributor.DataBind();
            //            dropDistributor.Items.Insert(0, "Select a Distributor");
            //        }
            //    }
            //}
            //set po
            //dropPO.DataSource = myApplicant.ApplicantDS.Tables[1];
            //dropPO.DataTextField = "POName";
            //dropPO.DataValueField = "PersonID";
            //dropPO.DataBind();
            //dropPO.Items.Insert(0, "Select a PO");
            /*make suggestions for PO*/
            //LabelSuggestion.Text = myApplicant.POSuggestions();
            //max station
            //dropMaxStation.DataSource = myApplicant.ApplicantDS.Tables[5];
            //dropMaxStation.DataTextField = "personName";
            //dropMaxStation.DataValueField = "PersonID";
            //dropMaxStation.DataBind();
            //add groups
            //if (myApplicant.ApplicantDS.Tables[6].Rows.Count > 0)
            //{
            //    for (int i = 0; i < myApplicant.ApplicantDS.Tables[6].Rows.Count; i++)
            //    {
            //        ListItem myitem = new ListItem("[GROUP] " + myApplicant.ApplicantDS.Tables[6].Rows[i]["groupname"].ToString(), myApplicant.ApplicantDS.Tables[6].Rows[i]["persongroupid"].ToString());
            //        dropMaxStation.Items.Insert(0, myitem);
            //    }
            //}
            //dropMaxStation.Items.Insert(0, "Select a MS");
            /*make suggestions for PO*/
            //LabelMSSuggestion.Text = myApplicant.MSSuggestions();

            dropPhysician.DataSource = myApplicant.ApplicantDS.Tables[3];
            dropPhysician.DataTextField = "PhysicianName";
            dropPhysician.DataValueField = "PersonID";
            dropPhysician.DataBind();
            dropPhysician.Items.Insert(0, "Select a Physician");
            if (myApplicant.ApplicantDS.Tables[4].Rows.Count > 0)
            {
                //lbExistingPatients.Visible = true;
            }
            if (myApplicant.PhysicianID != 0)
            {
                dropPhysician.SelectedValue = myApplicant.PhysicianID.ToString();
                LabelPhysician.Text = dropPhysician.SelectedItem.Text;
            }
            else
            {
                //LabelAppPhys.Text = myApplicant.PhysicianInfo();
                /*if (myApplicant.ApplicantDS.Tables[6].Rows.Count > 0)
                {
                    //lbExistingPhysicians.Visible = true;
                }*/
            }
        }
        LabelCountryPhysicians.Text = "<a href=../Country/CountryDataSets.aspx?choice=" + myApplicant.CountryID.ToString() + "&dset=Physicians>View physicians from this country</a>";
    }

    //**********************************************************************************************************************
    private void FillCountryComboBox()
    {
        //Fills the country combobox with the countries from the database
        DataSet ds;
        GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
        ds = myCountry.GetCountryList(true);
        //bind data to patient country
        dropCountry.DataSource = ds;
        dropCountry.DataValueField = "CountryID";
        dropCountry.DataTextField = "CountryName";
        dropCountry.DataBind();
        dropCountry.Items.Insert(0, "Select a country");
        dropCountry.SelectedItem.Value = "0";
    }
    protected void ButtonRestore_Click(object sender, EventArgs e)
    {
        myApplicant.RestoreApplicant(Convert.ToInt32(dropCountry.SelectedValue), Convert.ToInt32(dropPhysician.SelectedValue), dropPhysician.SelectedItem.Text.Split(',')[0], dropPhysician.SelectedItem.Text.Split(',')[1]);
            Response.Redirect("../TMF/DataDisplay.aspx?dset=patientwebapplicants");
    }
}