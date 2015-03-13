using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_Verification : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.Verification myVerification;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        myVerification = new GIPAP_Objects.Verification(Convert.ToInt32(Request.QueryString["choice"]));
        if (!Page.IsPostBack)
        {
            if (myVerification.VerificationID == 0)
            {
                PanelSummary.Visible = false;
                PanelReasonChange.Visible = false;
                PanelNotes.Visible = false;
            }
            else
            {
                if (myVerification.VerificationComplete)
                {
                    PanelForm.Visible = false;
                    LabelSummary.Text = myVerification.Summary();
                    PanelNotes.Visible = true;
                    if (myVerification.dtVNotes.Rows.Count > 0)
                    {
                        dgPatientInfo.DataSource = myVerification.dtVNotes;
                        dgPatientInfo.DataBind();
                    }
                }
                else
                {
                    PanelSummary.Visible = false;
                    rblstHealthInsurance.SelectedIndex = Convert.ToInt32(myVerification.Insurance);
                    rbCoversRx.SelectedIndex = Convert.ToInt32(myVerification.CoversRx);
                    rbCoversCancerRx.SelectedIndex = Convert.ToInt32(myVerification.CoversCancerRx);
                    rbCoversGlivecRx.SelectedIndex = Convert.ToInt32(myVerification.CoversGlivecRx);
                    PanelReasonChange.Visible = false;
                    PanelNotes.Visible = false;
                }
            }
        }
    }
    protected void ButtonSub_Click(object sender, EventArgs e)
    {
        myVerification.MedicalChart = rblstMedicalChart.SelectedIndex;
        myVerification.PhiladelphiaVerification = rblstPhil.SelectedIndex;
        myVerification.CKitVerification = rblstCKit.SelectedIndex;
        myVerification.CopyOfID = rblstCopyofID.SelectedIndex;
        myVerification.Photo = rblstPhoto.SelectedIndex;
        myVerification.SSCard = rblstSScard.SelectedIndex;
        if (rblstInsuranceCard.SelectedIndex == -1)
        {
            myVerification.InsuranceCard = 2;
        }
        else
        {
            myVerification.InsuranceCard = rblstInsuranceCard.SelectedIndex;
        }
        myVerification.InsuranceType = txtInsuranceType.Text;
        myVerification.TaxReturn = rblstTaxReturn.SelectedIndex;
        myVerification.SalarySlip = rblstSalarySlip.SelectedIndex;
        myVerification.FinancialAffidavit = rblstFinAffidavit.SelectedIndex;
        myVerification.PhoneBill = rblstPhoneBill.SelectedIndex;
        myVerification.OtherDocs = txtOtherDocs.Text;
        myVerification.Insurance = Convert.ToBoolean(rblstHealthInsurance.SelectedIndex);
        if (rbCoversRx.SelectedIndex != -1)
        {
            myVerification.CoversRx = Convert.ToBoolean(rbCoversRx.SelectedIndex);
        }
        if (rbCoversCancerRx.SelectedIndex != -1)
        {
            myVerification.CoversCancerRx = Convert.ToBoolean(rbCoversCancerRx.SelectedIndex);
        }
        if (rbCoversGlivecRx.SelectedIndex != -1)
        {
            myVerification.CoversGlivecRx = Convert.ToBoolean(rbCoversGlivecRx.SelectedIndex);
        }
        myVerification.HouseholdMembers = dropHousehold.SelectedIndex;
        myVerification.HouseholdOccupation = txtHouseholdOccupation.Text;
        myVerification.HouseholdIncome = txtHouseholdIncom.Text;
        myVerification.AdditionalFunds = txtAdditionalFunds.Text;
        myVerification.HouseholdAssets = txtAssets.Text;
        myVerification.Recommendation = rblstRecommendation.SelectedValue;
        myVerification.Explanation = txtExplanation.Text;
        myVerification.ReasonChanged = txtChangeReason.Text;

        myVerification.Create(sessUse.Username);
        Response.Redirect("patientinfo.aspx?a=verification&choice=" + myVerification.PatientID.ToString());
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myVerification.PatientID.ToString());
    }
    protected void lbExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientinfo.aspx?choice=" + myVerification.PatientID.ToString());
    }
    protected void lbModify_Click(object sender, EventArgs e)
    {
        rblstMedicalChart.SelectedIndex = myVerification.MedicalChart;
        rblstPhil.SelectedIndex = myVerification.PhiladelphiaVerification;
        rblstCKit.SelectedIndex = myVerification.CKitVerification;
        rblstCopyofID.SelectedIndex = myVerification.CopyOfID;
        rblstPhoto.SelectedIndex = myVerification.Photo;
        rblstSScard.SelectedIndex = myVerification.SSCard;
        rblstInsuranceCard.SelectedIndex = myVerification.InsuranceCard;
        txtInsuranceType.Text = myVerification.InsuranceType;
        rblstTaxReturn.SelectedIndex = myVerification.TaxReturn;
        rblstSalarySlip.SelectedIndex = myVerification.SalarySlip;
        rblstFinAffidavit.SelectedIndex = myVerification.FinancialAffidavit;
        rblstPhoneBill.SelectedIndex = myVerification.PhoneBill;
        txtOtherDocs.Text = myVerification.OtherDocs;
        rblstHealthInsurance.SelectedIndex = Convert.ToInt32(myVerification.Insurance);
        rbCoversRx.SelectedIndex = Convert.ToInt32(myVerification.CoversRx);
        rbCoversCancerRx.SelectedIndex = Convert.ToInt32(myVerification.CoversCancerRx);
        rbCoversGlivecRx.SelectedIndex = Convert.ToInt32(myVerification.CoversGlivecRx);
        dropHousehold.SelectedIndex = myVerification.HouseholdMembers;
        txtHouseholdOccupation.Text = myVerification.HouseholdOccupation;
        txtHouseholdIncom.Text = myVerification.HouseholdIncome;
        txtAdditionalFunds.Text = myVerification.AdditionalFunds;
        txtAssets.Text = myVerification.HouseholdAssets;
        rblstRecommendation.SelectedValue = myVerification.Recommendation;
        txtExplanation.Text = myVerification.Explanation;

        PanelSummary.Visible = false;
        PanelNotes.Visible = false;
        PanelForm.Visible = true;
    }
}
