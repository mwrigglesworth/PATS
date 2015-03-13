using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Application_NewApplicant : System.Web.UI.Page
{
    GIPAP_Objects.GIPAPApplicant myApplicant;
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    public bool showDistributor = false;

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
            if (myApplicant.CountryID != 76) /*if not India, show Distributor list*/
            {
                if (myApplicant.ApplicantDS.Tables[10] != null)
                {
                    if (myApplicant.ApplicantDS.Tables[10].Rows.Count > 0)
                    {
                        this.showDistributor = true;

                        //set distributor
                        dropDistributor.DataSource = myApplicant.ApplicantDS.Tables[10];
                        dropDistributor.DataTextField = "OfficeName";
                        dropDistributor.DataValueField = "DistributorID";
                        dropDistributor.DataBind();
                        dropDistributor.Items.Insert(0, "Select a Distributor");
                        /*make suggestions for Distributor*/
                        LabelDistributorSuggestion.Text = myApplicant.DistributorSuggestions();
                    }
                }
            }
            //set po
            dropPO.DataSource = myApplicant.ApplicantDS.Tables[1];
            dropPO.DataTextField = "POName";
            dropPO.DataValueField = "PersonID";
            dropPO.DataBind();
            dropPO.Items.Insert(0, "Select a PO");
            /*make suggestions for PO*/
            LabelSuggestion.Text = myApplicant.POSuggestions();
            //max station
            dropMaxStation.DataSource = myApplicant.ApplicantDS.Tables[5];
            dropMaxStation.DataTextField = "personName";
            dropMaxStation.DataValueField = "PersonID";
            dropMaxStation.DataBind();
            //add groups
            if (myApplicant.ApplicantDS.Tables[6].Rows.Count > 0)
            {
                for (int i = 0; i < myApplicant.ApplicantDS.Tables[6].Rows.Count; i++)
                {
                    ListItem myitem = new ListItem("[GROUP] " + myApplicant.ApplicantDS.Tables[6].Rows[i]["groupname"].ToString(), myApplicant.ApplicantDS.Tables[6].Rows[i]["persongroupid"].ToString());
                    dropMaxStation.Items.Insert(0, myitem);
                }
            }
            dropMaxStation.Items.Insert(0, "Select a MS");
            /*make suggestions for PO*/
            LabelMSSuggestion.Text = myApplicant.MSSuggestions();

            dropPhysician.DataSource = myApplicant.ApplicantDS.Tables[3];
            dropPhysician.DataTextField = "PhysicianName";
            dropPhysician.DataValueField = "PersonID";
            dropPhysician.DataBind();
            dropPhysician.Items.Insert(0, "Select a Physician");
            if (myApplicant.ApplicantDS.Tables[4].Rows.Count > 0)
            {
                lbExistingPatients.Visible = true;
            }
            if (myApplicant.PhysicianID != 0)
            {
                dropPhysician.SelectedValue = myApplicant.PhysicianID.ToString();
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

    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        if (dropDeleteReason.SelectedIndex != 0)
        {
            myApplicant.TransferApplicant(dropDeleteReason.SelectedValue);
            Response.Redirect("../TMF/DataDisplay.aspx?dset=patientwebapplicants");
        }
        else
        {
            LabelReasonDelete.Visible = true;
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        myApplicant.CountryID = Convert.ToInt32(dropCountry.SelectedValue);
        GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient(sessUse.Username, Convert.ToInt32(dropPO.SelectedValue), 0, Convert.ToInt32(dropPhysician.SelectedValue), myApplicant);
        //NOW ASSIGN MS
        if (dropMaxStation.SelectedIndex != 0)
        {
            GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
            CheckBoxList cb = new CheckBoxList();
            if (dropMaxStation.SelectedItem.Text.StartsWith("[GROUP]"))
            {
                myAr.MSGroupID = Convert.ToInt32(dropMaxStation.SelectedValue);
            }
            else
            {
                cb.Items.Add(dropMaxStation.SelectedValue);
                cb.Items[0].Selected = true;
            }
            myAr.Update(cb, sessUse.Username, sessUse.Role, myPatient.PatientID, "Patient", "MaxStation");
        }

        //NOW ASSIGN Distributor
        if ( this.dropDistributor.SelectedIndex > 0)
        {
            GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
            CheckBoxList cb = new CheckBoxList();
            cb.Items.Add(dropDistributor.SelectedValue);
            cb.Items[0].Selected = true;
            myAr.Update(cb, sessUse.Username, sessUse.Role, myPatient.PatientID, "Patient", "Distributor");
        }
        Response.Redirect("../Patient/PatientInfo.aspx?choice=" + myPatient.PatientID.ToString() + "&a=add");
    }
    protected void lbExistingPatients_Click(object sender, EventArgs e)
    {
        PanelApplicant.Visible = false;
        PanelExistingPatients.Visible = true;
        dgExisitngPatients.DataSource = myApplicant.ApplicantDS.Tables[4];
        dgExisitngPatients.DataBind();
        LabelHeader.Text = myApplicant.NameHeaderText("patient");
    }
    protected void lbReturn_Click(object sender, EventArgs e)
    {
        PanelApplicant.Visible = true;
        PanelExistingPatients.Visible = false;
    }
}
