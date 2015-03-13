using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_NOAFEF : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.NOAFEF myFEF;
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        myFEF = new GIPAP_Objects.NOAFEF(choice);
        if (!Page.IsPostBack)
        {
            //set donation length values, based on treatment & country
            if (myFEF.CountryName == "Malaysia")
            {
                if (myFEF.TBLPATIENTTreatment == "Glivec")
                {
                    dropDonationLength.Items.Add(new ListItem("185 Days (185 Days Purchased)", "185"));
                }
                else if (myFEF.TBLPATIENTTreatment == "Tasigna")
                {
                    dropDonationLength.Items.Add(new ListItem("26 Weeks (26 Weeks Purchased)", "26"));
                }
            }
            else if (myFEF.CountryName == "Mexico")
            {
                if (myFEF.TBLPATIENTTreatment == "Glivec")
                {
                    dropDonationLength.Items.Add(new ListItem("370 Days (Full Donation)", "370"));
                }
            }
            else if (myFEF.CountryName == "South Africa")
            {
                if (myFEF.TBLPATIENTTreatment == "Glivec")
                {
                    dropDonationLength.Items.Add(new ListItem("370 Days (Full Donation)", "370"));
                }
                else if (myFEF.TBLPATIENTTreatment == "Tasigna")
                {
                    dropDonationLength.Items.Add(new ListItem("52 Weeks (Full Donation)", "52"));
                }
            }
            else if (myFEF.CountryName == "Vietnam")
            {
                if (myFEF.TBLPATIENTTreatment == "Tasigna")
                {
                    dropDonationLength.Items.Add(new ListItem("50 Weeks (2 Weeks Purchased)", "50"));
                }
            }
            else if (myFEF.CountryName == "India")
            {
                if (myFEF.TBLPATIENTTreatment == "Glivec")
                {
                    dropDonationLength.Items.Add(new ListItem("370 Days (Full Donation)", "370"));
                    dropDonationLength.Items.Add(new ListItem("330 Days (40 Days Purchased, KKT)", "330"));
                    dropDonationLength.Items.Add(new ListItem("250 Days (120 Days Purchased)", "250"));
                    dropDonationLength.Items.Add(new ListItem("130 Days (240 Days Purchased)", "130"));
                    //not doing this now dropDonationLength.Items.Add(new ListItem("0 Days (370 Days Purchased)", "0"));
                    //4months purchase 250 days nowdropDonationLength.Items.Add(new ListItem("360 Days (10 Days Purchased)", "360"));
                    //8monthspurchase , 130 days now dropDonationLength.Items.Add(new ListItem("350 Days (20 Days Purchased)", "350"));
                    /*dropDonationLength.Items.Add(new ListItem("340 Days (30 Days Purchased)", "340"));
                    dropDonationLength.Items.Add(new ListItem("330 Days (40 Days Purchased)", "330"));
                    dropDonationLength.Items.Add(new ListItem("290 Days (80 Days Purchased)", "290"));*/
                }
                else if (myFEF.TBLPATIENTTreatment == "Tasigna")
                {
                    dropDonationLength.Items.Add(new ListItem("44 Weeks (8 Weeks Purchased)", "44"));
                }
            }
            else
            {
                if (myFEF.TBLPATIENTTreatment == "Glivec")
                {
                    dropDonationLength.Items.Add(new ListItem("370 Days (Full Donation)", "370"));
                }
                else if (myFEF.TBLPATIENTTreatment == "Tasigna")
                {
                    dropDonationLength.Items.Add(new ListItem("52 Weeks (Full Donation)", "52"));
                }
            }
            if (dropDonationLength.Items.Count == 2)
            {
                dropDonationLength.Items[1].Selected = true;
                dropDonationLength.Enabled = false;
            }
            if (myFEF.dtPastFefs != null)
            {
                if (sessUse.Role != "FCCallCenter" && (myFEF.dtPastFefs.Rows.Count > 1 || (myFEF.dtPastFefs.Rows.Count == 1 && myFEF.CollectNew)))
                {
                    PanelOldFefs.Visible = true;
                    dropPastFEFs.DataSource = myFEF.dtPastFefs;
                    dropPastFEFs.DataTextField = "fef";
                    dropPastFEFs.DataValueField = "noafefid";
                    dropPastFEFs.DataBind();
                    if (myFEF.CollectNew)
                    {
                        dropPastFEFs.Items.Insert(0, "Current FEF");
                    }
                    else
                    {
                        dropPastFEFs.SelectedItem.Text = "Current FEF";
                    }
                }
            }
            if (myFEF.CollectNew)
            {
                if (sessUse.Role == "FCCallCenter")
                {
                    //darlene asked for this to be vis.... but not to call center
                    PanelNote.Visible = false;
                    lbEdit.Visible = false;
                }
                else
                {
                    this.LoadNotePanel();
                    //a new fef is required for all cases now, so removing "no yearly reassessment"
                    /*if (myFEF.YearlyReassess == 1)
                    {
                        //lbYearlyReassess.Visible = true;
                        PanelNoYearlyReassessment.Visible = true;
                    }*/
                }
                lbEdit.Visible = false;
                if (myFEF.HideNotes)
                {
                    lbFEFNotes.Visible = PanelFEFNotes.Visible = false;
                }
                if (sessUse.Role == "FCBranch")
                {
                    //PanelEdit.Visible = true;
                    // a new fef is needed, so we are making thigs clear for the branch
                    LabelFEFInfo.Text = "<i>A NEW FEF is required for this patient</i><br><br>";
                    PanelStartNewFEF.Visible = true;
                }
                else
                {
                    LabelFEFInfo.Text = "<i>A current NOA FEF has yet to be started</i><br><br>";
                    PanelMSContacted.Visible = false;
                    if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
                    {
                        //new - for max doing FEF
                        if (myFEF.CountryName != "India")
                        {
                            PanelStartNewFEF.Visible = true;
                            PanelFixed.Visible = true;
                            lbFixed.Attributes.Add("onclick", "confirmFixed()");
                        }
                    }
                }
            }
            else if (myFEF.Complete)
            {
                if (sessUse.Role == "FCCallCenter")
                {
                    PanelNote.Visible = false;
                    lbEdit.Visible = false;
                }
                else
                {
                    LabelFEFInfo.Text = myFEF.FEFInfo(sessUse.Role, "White");
                    this.LoadNotePanel();
                }
                //we will try letting branch edit fef while the patient is active.  they email mike to change don length, yearly reassess to yes
                if (sessUse.Role == "FCBranch" && /*myFEF.GIPAPStatus != "Active" &&*/ (myFEF.GIPAPStatus != "Denied" || myFEF.ReassessRequestCount == 0))
                {
                    if (myFEF.GIPAPStatus == "Active")
                    {
                        PanelStartNewFEF.Visible = true;
                    }
                    else
                    {
                        lbEdit.Visible = true;
                    }
                }
                else if (!sessUse.Role.StartsWith("FC") && /*let payment op be edited ***myFEF.GIPAPStatus != "Active" &&*/ myFEF.GIPAPStatus != "Denied")
                {
                    //since it is complete, only partial don would need to edit pay option
                    if (myFEF.DonationLength != -1 && myFEF.DonationLength != 12 && myFEF.DonationLength != 370 && myFEF.DonationLength != 52)
                    {
                        PanelMSContacted.Visible = true;
                        this.SetMSContact(myFEF.TMFInfo());
                    }
                }
            }
            else if (myFEF.Recommendation == "")
            {
                if (sessUse.Role == "FCCallCenter")
                {
                    //darlene asked for this to be vis.... but not to call center
                    PanelNote.Visible = false;
                }
                else
                {
                    this.LoadNotePanel();
                }
                if (sessUse.Role == "FCBranch")
                {
                    //a fef needs to be collected for the first time
                    PanelEdit.Visible = true;
                    rblstHealthInsurance.SelectedIndex = Convert.ToInt32(myFEF.Insurance);
                    rbCoversRx.SelectedIndex = Convert.ToInt32(myFEF.CoversRx);
                    rbCoversCancerRx.SelectedIndex = Convert.ToInt32(myFEF.CoversCancerRx);
                    rbCoversGlivecRx.SelectedIndex = Convert.ToInt32(myFEF.CoversGlivecRx);
                }
                else
                {
                    LabelFEFInfo.Text = "<i>NOA FEF has yet to be completed</i><br><br>";
                    if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
                    {
                        if (myFEF.CountryName != "India")
                        {
                            PanelStartNewFEF.Visible = true;
                            PanelFixed.Visible = true;
                            //lbFixed.Visible = true; making it a panel so it looks better
                            lbFixed.Attributes.Add("onclick", "confirmFixed()");
                        }
                    }
                }
            }
            else
            {
                if (sessUse.Role == "FCCallCenter")
                {
                    PanelNote.Visible = false;
                    lbEdit.Visible = false;
                }
                else
                {
                    LabelFEFInfo.Text = myFEF.FEFInfo(sessUse.Role, "White");
                    this.LoadNotePanel();
                    try
                    {
                        LabelResultCountChanges.Text = myFEF.dtDataLogChanges.Rows.Count.ToString() + " Updates Found";
                    }
                    catch { }
                    if (sessUse.Role == "FCBranch")
                    {
                        lbEdit.Visible = true;
                    }
                    else if (!sessUse.Role.StartsWith("FC"))
                    {
                        //need this for all patients, not just partial don... but not when rec is still at pending
                        if (myFEF.DonationLength != -1 && myFEF.Recommendation != "Pending")// myFEF.DonationLength != 12 && myFEF.DonationLength != 370 && myFEF.DonationLength != 52)
                        {
                        PanelMSContacted.Visible = true;
                        this.SetMSContact(myFEF.TMFInfo());
                        }
                        //non-india countries needs max to be able to edit FEF before its approved
                        if (myFEF.CountryName != "India")
                        {
                            lbEdit.Visible = true;
                        }
                    }
                }
            }
        }
    }
    protected void lbEdit_Click(object sender, EventArgs e)
    {
        LabelFEFInfo.Text = "";
        PanelEdit.Visible = true;
        PanelNote.Visible = false;
        PanelOldFefs.Visible = false;
        rblstAddressVer.SelectedIndex = myFEF.AddressVer;
        rblstBankState.SelectedIndex = myFEF.BankStatementVer;
        rblstPatientConsent.SelectedIndex = myFEF.PatientConsent;
        rblstMedicalChart.SelectedIndex = myFEF.MedicalChart;
        rblstPhilPos.SelectedIndex = myFEF.PhilBCRTest;
        rblstCKit.SelectedIndex = myFEF.CKitTest;
        rblstPatientID.SelectedIndex = myFEF.CopyOfID;
        rblstPhoto.SelectedIndex = myFEF.Photo;
        rblstPrivateInsurance.SelectedIndex = myFEF.PrivateInsurance;
        rblstTaxReturn.SelectedIndex = myFEF.TaxReturn;
        rblstIncomeVer.SelectedIndex = myFEF.IncomeVer;
        rblstFinDec.SelectedIndex = myFEF.FinDeclaration;
        rblstUtilPhone.SelectedIndex = myFEF.UtilPhoneBill;
        txtOtherDocs.Text = myFEF.OtherDocs;
        rblstHealthInsurance.SelectedIndex = Convert.ToInt32(myFEF.Insurance);
        rbCoversRx.SelectedIndex = Convert.ToInt32(myFEF.CoversRx);
        rbCoversCancerRx.SelectedIndex = Convert.ToInt32(myFEF.CoversCancerRx);
        rbCoversGlivecRx.SelectedIndex = Convert.ToInt32(myFEF.CoversGlivecRx);
        rblstYearlyReassess.SelectedIndex = myFEF.YearlyReassess;
        rblstRecommendation.SelectedValue = myFEF.Recommendation;
        //txtNOAPIN.Text = myFEF.NoaPin;
        try
        {
            dropDonationLength.SelectedValue = myFEF.DonationLength.ToString();
        }
        catch { }
    }
    protected void LoadNotePanel()
    {
            GridViewNotes.DataSource = myFEF.dtNotes;
            GridViewNotes.DataBind();
            try{
            LabelNoteCount.Text = myFEF.dtNotes.Rows.Count.ToString() + " Notes Found";
            }
            catch{}
            LabelRequests.Text = myFEF.ViewRequests();
            GridViewUpdates.DataSource = myFEF.dtDataLogChanges;
            GridViewUpdates.DataBind();
            try
            {
                LabelResultCountChanges.Text = myFEF.dtDataLogChanges.Rows.Count.ToString() + " Updates Found";
            }
            catch { }
    }

    protected void ProcessForm()
    {
        myFEF.AddressVer = rblstAddressVer.SelectedIndex;
        myFEF.BankStatementVer = rblstBankState.SelectedIndex;
        myFEF.PatientConsent = rblstPatientConsent.SelectedIndex;
        myFEF.MedicalChart = rblstMedicalChart.SelectedIndex;
        myFEF.PhilBCRTest = rblstPhilPos.SelectedIndex;
        myFEF.CKitTest = rblstCKit.SelectedIndex;
        myFEF.CopyOfID = rblstPatientID.SelectedIndex;
        myFEF.Photo = rblstPhoto.SelectedIndex;
        myFEF.PrivateInsurance = rblstPrivateInsurance.SelectedIndex;
        myFEF.TaxReturn = rblstTaxReturn.SelectedIndex;
        myFEF.IncomeVer = rblstIncomeVer.SelectedIndex;
        myFEF.FinDeclaration = rblstFinDec.SelectedIndex;
        myFEF.UtilPhoneBill = rblstUtilPhone.SelectedIndex;
        myFEF.OtherDocs = txtOtherDocs.Text.Trim();
        myFEF.Fixed = false;// rblstFixed.Items[1].Selected;
        myFEF.Insurance = Convert.ToBoolean(rblstHealthInsurance.SelectedIndex);
        if (rbCoversRx.SelectedIndex != -1)
        {
            myFEF.CoversRx = Convert.ToBoolean(rbCoversRx.SelectedIndex);
        }
        if (rbCoversCancerRx.SelectedIndex != -1)
        {
            myFEF.CoversCancerRx = Convert.ToBoolean(rbCoversCancerRx.SelectedIndex);
        }
        if (rbCoversGlivecRx.SelectedIndex != -1)
        {
            myFEF.CoversGlivecRx = Convert.ToBoolean(rbCoversGlivecRx.SelectedIndex);
        }
        if (rblstRecommendation.SelectedIndex != -1)
        {
            myFEF.Recommendation = rblstRecommendation.SelectedValue;
        }
        myFEF.NoaPin = "Financial Evaluation";
        if (dropDonationLength.SelectedIndex != 0)
        {
            myFEF.DonationLength = Convert.ToInt32(dropDonationLength.SelectedValue);
            if (myFEF.TBLPATIENTTreatment == "Tasigna")
            {
                myFEF.DonationLengthUnit = "Weeks";
            }
            else
            {
                myFEF.DonationLengthUnit = "Days";
            }
            /*if (myFEF.DonationLength > 12 && myFEF.DonationLength <= 52)
            {
                myFEF.DonationLengthUnit = "Weeks";
            }
            else if (myFEF.DonationLength >= 185)
            {
                myFEF.DonationLengthUnit = "Days";
            }
            else
            {
                myFEF.DonationLengthUnit = "Months";
            }*/
        }
        else
        {
            myFEF.DonationLength = -1;
            //myFEF.DonationLengthUnit = "";
        }
        //***if it is a partial pay/donation patient you must require yearly reassess***
        if (myFEF.DonationLength != 370 && myFEF.DonationLength != 52 && myFEF.DonationLength != 12)
        {
            myFEF.YearlyReassess = 1;
        }
        else
        {
            myFEF.YearlyReassess = rblstYearlyReassess.SelectedIndex;
        }

        if (myFEF.CollectNew)
        {
            if (myFEF.ReassessDate < DateTime.Today)
            {
                if (myFEF.CountryName=="India")
                    myFEF.ReassessDate = DateTime.Today.AddYears(2);
                else
                myFEF.ReassessDate = DateTime.Today.AddYears(1);
            }
            else
            {
                if(myFEF.CountryName == "India")
                    myFEF.ReassessDate = myFEF.ReassessDate.AddYears(2);
                else
                myFEF.ReassessDate = myFEF.ReassessDate.AddYears(1);
            }
            myFEF.Create(sessUse.Username);
        }
        else if (myFEF.MSContacted)//this is for if the branch starts a new fef before one is required
        {
            if(myFEF.CountryName=="India")
            myFEF.ReassessDate = DateTime.Today.AddYears(2);
            else
            myFEF.ReassessDate = DateTime.Today.AddYears(1);
            myFEF.Create(sessUse.Username);
        }
        else
        {
            if (myFEF.CountryName == "India")
            {
                if (myFEF.ReassessDate < DateTime.Today.AddYears(2))
                {
                    myFEF.ReassessDate = DateTime.Today.AddYears(2);
                }
            }
            else
            {
                if (myFEF.ReassessDate < DateTime.Today.AddYears(1))
                {
                    myFEF.ReassessDate = DateTime.Today.AddYears(1);
                }
            }
            myFEF.Update(sessUse.Username);
        }
    }
    protected void SetMSContact(string tmfinfo)
    {
        LabelTMFInfo.Text = tmfinfo;
        UpdateProgress1.AssociatedUpdatePanelID = "UpdatePanel1";
    }
    protected void SetPaymentValues(int dl, string unit)
    {
        // only run after Donation Length is set
        dropPaymentOption.Items.Clear();
        dropPaymentOption.Items.Insert(0, "Select One");
        dropPaymentOption.SelectedItem.Value = "0";
        if (unit == "Days")
        {
            if (dl == 360)
            {
                dropPaymentOption.Items.Add("1 x 1 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 350)
            {
                dropPaymentOption.Items.Add("1 x 2 Strips");
                dropPaymentOption.Items.Add("2 x 1 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 340)
            {
                dropPaymentOption.Items.Add("1 x 3 Strips");
                dropPaymentOption.Items.Add("1 x 2 Strips + 1 x 1 Strip");
                dropPaymentOption.Items.Add("3 x 1 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 330)
            {
                dropPaymentOption.Items.Add("1 x 4 Strips");
                dropPaymentOption.Items.Add("2 x 2 Strips");
                dropPaymentOption.Items.Add("4 x 1 Strip");
                dropPaymentOption.Items.Add("1 x 3 Strips + 1 x 1 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 290)
            {
                dropPaymentOption.Items.Add("1 x 8 Strips");
                dropPaymentOption.Items.Add("2 x 4 Strips");
                dropPaymentOption.Items.Add("4 x 2 Strips");
                dropPaymentOption.Items.Add("8 x 1 Strip");
                dropPaymentOption.Items.Add("2 x 3 Strips + 1 x 2 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 250)
            {
                //dropPaymentOption.Items.Add("1 x 1 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 130)
            {
                //dropPaymentOption.Items.Add("1 x 1 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 0)
            {
                dropPaymentOption.Items.Add("Full Payment");
                dropPaymentOption.SelectedValue = "Full Payment";
            }
            else if (dl == 185) //malaysia
            {
                dropPaymentOption.Items.Add("N/A");
                dropPaymentOption.SelectedValue = "N/A";
            }
            else
            {
                dropPaymentOption.Items.Add("No Payment");
                dropPaymentOption.SelectedValue = "No Payment";
            }
        }
        else if (unit == "Weeks")
        {
            if (dl == 44)
            {
                dropPaymentOption.Items.Add("1 x 8 Boxes");
                dropPaymentOption.Items.Add("2 x 4 Boxes");
                dropPaymentOption.Items.Add("2 x 3 Boxes + 1 x 2 Boxes");
                dropPaymentOption.Items.Add("4 x 2 Boxes");
                dropPaymentOption.Items.Add("8 x 1 Boxes");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 26) //malaysia
            {
                dropPaymentOption.Items.Add("N/A");
                dropPaymentOption.SelectedValue = "N/A";
            }
            else if (dl == 50) //vietnam
            {
                dropPaymentOption.Items.Add("2 Boxes");
                dropPaymentOption.SelectedValue = "2 Boxes";
            }
            else
            {
                dropPaymentOption.Items.Add("No Payment");
                dropPaymentOption.SelectedValue = "No Payment";
            }
        }
        else
        {
            if (dl == 11 || dl == 9 || dl == 6)
            {
                dropPaymentOption.Items.Add("400mg / 1 Box");
                dropPaymentOption.Items.Add("400mg / 1 Strip");
                dropPaymentOption.Items.Add("100mg / 1 Strip");
                dropPaymentOption.Items.Add("100mg / 2 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 10)
            {
                dropPaymentOption.Items.Add("400mg / 2 Box");
                dropPaymentOption.Items.Add("400mg / 2 Strip");
                dropPaymentOption.Items.Add("100mg / 2 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 51)
            {
                dropPaymentOption.Items.Add("400mg / 1 Strip");
                dropPaymentOption.Items.Add("100mg / 1 Strip");
                dropPaymentOption.Items.Add("100mg / 2 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else if (dl == 50)
            {
                dropPaymentOption.Items.Add("400mg / 2 Strip");
                dropPaymentOption.Items.Add("400mg / 1 Strip");
                dropPaymentOption.Items.Add("100mg / 1 Strip");
                dropPaymentOption.Items.Add("100mg / 2 Strip");
                dropPaymentOption.Items.Add("Individualized");
                dropPaymentOption.Enabled = true;
            }
            else
            {
                dropPaymentOption.Items.Add("No Payment");
                dropPaymentOption.SelectedValue = "No Payment";
            }
        }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        PanelStartNewFEF.Visible = false;
        PanelFixed.Visible = false;
        PanelOldFefs.Visible = false;
        PanelComplete.Visible = false;
        PanelNoAssessment.Visible = false;
        PanelDeny.Visible = false;
        PanelPending.Visible = false;
        PanelNote.Visible = false;
        PanelEdit.Visible = false;
        LabelFEFInfo.Text = "";
        if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
        {
            this.ProcessForm();
            Response.Redirect("NOAFEF.aspx?choice=" + myFEF.PatientID.ToString());
        }
        else
        {
            PanelSecure.Visible = true;
        }
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("patientinfo.aspx?choice=" + myFEF.PatientID.ToString());
    }
    protected void lbEditMsContact_Click(object sender, EventArgs e)
    {
        PanelEditMsContact.Visible = true;
        LabelTMFInfo.Text = "";
        rblstMSContacted.SelectedIndex = Convert.ToInt32(myFEF.MSContacted);
        lbEditMsContact.Visible = false;
        this.SetPaymentValues(myFEF.DonationLength, myFEF.DonationLengthUnit);
        try
        {
            dropPaymentOption.SelectedValue = myFEF.PaymentOption;
        }
        catch { }
    }
    protected void ButtonSaveMSContact_Click(object sender, EventArgs e)
    {
        myFEF.MSContacted = rblstMSContacted.Items[1].Selected;
        if (dropPaymentOption.SelectedIndex != 0)
        {
            myFEF.PaymentOption = dropPaymentOption.SelectedValue;
        }
        else
        {
            myFEF.PaymentOption = "";
        }
        myFEF.UpdateMSContact(sessUse.Username);
        Response.Redirect("NOAFEF.aspx?choice=" + myFEF.PatientID.ToString());
    }
    protected void dropPastFEFs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropPastFEFs.SelectedIndex == 0)
        {
            Response.Redirect("NOAFEF.aspx?choice=" + myFEF.PatientID.ToString());
        }
        else
        {
            myFEF.InflatePastFEF(Convert.ToInt32(dropPastFEFs.SelectedValue));
            PanelEdit.Visible = false;
            PanelFixed.Visible = false;
            PanelStartNewFEF.Visible = false;
            LabelFEFInfo.Text = myFEF.FEFInfo(sessUse.Role, "LightCyan");
            lbEdit.Visible = false;
            PanelMSContacted.Visible = false;
        }
    }
    protected void ButtonAddNote_Click(object sender, EventArgs e)
    {
        if (txtNote.Text.Trim() != "")
        {
            myFEF.AddFEFNote(sessUse.Username, txtNote.Text.Trim());
            Response.Redirect("NOAFEF.aspx?choice=" + myFEF.PatientID.ToString());
        }
    }
    protected void lbFixed_Click(object sender, EventArgs e)
    {
        myFEF.AddressVer = 2;
        myFEF.BankStatementVer = 2;
        myFEF.PatientConsent = 2;
        myFEF.MedicalChart = 2;
        myFEF.PhilBCRTest = 2;
        myFEF.CKitTest = 2;
        myFEF.CopyOfID = 2;
        myFEF.Photo = 2;
        myFEF.PrivateInsurance = 2;
        myFEF.TaxReturn = 2;
        myFEF.IncomeVer = 2;
        myFEF.FinDeclaration = 2;
        myFEF.UtilPhoneBill = 2;
        myFEF.OtherDocs = "";
        myFEF.Fixed = true;
        myFEF.Insurance = false;
        myFEF.YearlyReassess = 1;
        myFEF.Recommendation = "Approve";
        myFEF.NoaPin = "Fixed";
        if (myFEF.CountryName == "Malaysia")
        {
            if (myFEF.TBLPATIENTTreatment == "Glivec")
            {
                myFEF.DonationLength = 185;
                myFEF.DonationLengthUnit = "Days";
            }
            else if (myFEF.TBLPATIENTTreatment == "Tasigna")
            {
                myFEF.DonationLength = 26;
                myFEF.DonationLengthUnit = "Weeks";
            }
        }
        else if (myFEF.CountryName == "Mexico")
        {
            if (myFEF.TBLPATIENTTreatment == "Glivec")
            {
                myFEF.DonationLength = 370;
                myFEF.DonationLengthUnit = "Days";
            }
        }
        else if (myFEF.CountryName == "Vietnam")
        {
            if (myFEF.TBLPATIENTTreatment == "Tasigna")
            {
                myFEF.DonationLength = 50;
                myFEF.DonationLengthUnit = "Weeks";
            }
        }
        else if (myFEF.CountryName == "India")
        {
            if (myFEF.TBLPATIENTTreatment == "Glivec")
            {
                myFEF.DonationLength = 0;
                myFEF.DonationLengthUnit = "Days";
            }
            else if (myFEF.TBLPATIENTTreatment == "Tasigna")
            {
                myFEF.DonationLength = 44;
                myFEF.DonationLengthUnit = "Weeks";
            }
        }
        else //south africa, generic
        {
            if (myFEF.TBLPATIENTTreatment == "Glivec")
            {
                myFEF.DonationLength = 370;
                myFEF.DonationLengthUnit = "Days";
            }
            else if (myFEF.TBLPATIENTTreatment == "Tasigna")
            {
                myFEF.DonationLength = 52;
                myFEF.DonationLengthUnit = "Weeks";
            }
        }
        if (myFEF.CollectNew)
        {
            if (myFEF.ReassessDate < DateTime.Today)
            {
                myFEF.ReassessDate = DateTime.Today.AddYears(1);
            }
            else
            {
                myFEF.ReassessDate = myFEF.ReassessDate.AddYears(1);
            }
            myFEF.Create(sessUse.Username);
        }
        else
        {
            if (myFEF.ReassessDate < DateTime.Today.AddYears(1))
            {
                myFEF.ReassessDate = DateTime.Today.AddYears(1);
            }
            myFEF.Update(sessUse.Username);
        }
        Response.Redirect("NOAFEF.aspx?choice=" + myFEF.PatientID.ToString());
    }
    protected void lbnewfef_Click(object sender, EventArgs e)
    {
        if (!myFEF.CollectNew)
        {
            LabelFEFInfo.Text = "";
        }
        PanelStartNewFEF.Visible = false;
        PanelFixed.Visible = false;
        PanelOldFefs.Visible = false;
        PanelEdit.Visible = true;
        //new - for max doing FEF
        if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
        {
            rblstYearlyReassess.Items[1].Selected = true;
            rblstYearlyReassess.Enabled = false;
            rblstRecommendation.Items[1].Selected = true;
            rblstRecommendation.Enabled = false;
        }
    }
    protected void ButtonSecure_Click(object sender, EventArgs e)
    {
        if (txtPassword.Text == sessUse.Password)
        {
            LabelPwordError.Text = "";
            //process form
            this.ProcessForm();
            //NOW DEDICDE THE STATUS OF THE FEF
            PanelEdit.Visible = false;
            PanelSecure.Visible = false;
            if (myFEF.Recommendation == "Approve" && myFEF.DonationLength != -1)
            {
                if (myFEF.YearlyReassess == 1)
                {
                    PanelComplete.Visible = true;
                    LabelReassessdate.Text = myFEF.ReassessDate.Day.ToString() + " " + myFEF.ReassessDate.ToString("y");
                }
                else
                {
                    PanelNoAssessment.Visible = true;
                }
            }
            else if (myFEF.Recommendation == "Deny")
            {
                PanelDeny.Visible = true;
            }
            else
            {
                PanelPending.Visible = true;
            }
        }
        else
        {
            LabelPwordError.Text = "Password entered is incorrect";
        }
    }
    protected void lbFEFNotes_Click(object sender, EventArgs e)
    {
        if (PanelFEFNotes.Visible)
        {
            PanelFEFNotes.Visible = false;
            lbFEFNotes.Text = "View FEF Notes";
        }
        else
        {
            PanelFEFNotes.Visible = true;
            lbFEFNotes.Text = "Hide FEF Notes";
        }
    }
    protected void lbRequests_Click(object sender, EventArgs e)
    {
        if (PanelRequests.Visible)
        {
            PanelRequests.Visible = false;
            lbRequests.Text = "View Requests";
        }
        else
        {
            PanelRequests.Visible = true;
            lbRequests.Text = "Hide Requests";
        }
    }
    protected void lbUpdates_Click(object sender, EventArgs e)
    {
        if (PanelUpdate.Visible)
        {
            PanelUpdate.Visible = false;
            lbUpdates.Text = "View FEF Updates";
        }
        else
        {
            PanelUpdate.Visible = true;
            lbUpdates.Text = "Hide FEF Updates";
        }
    }
}
