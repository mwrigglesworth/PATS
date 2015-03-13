using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
    public class NOAFEF
    {
        private int mNOAFEFID;
        private int mPatientID;
        private int mAddressVer;
        private int mBankStatementVer;
        private int mPatientConsent;
        private int mMedicalChart;
        private int mPhilBCRTest;
        private int mCKitTest;
        private int mCopyOfID;
        private int mPhoto;
        private int mPrivateInsurance;
        private int mTaxReturn;
        private int mIncomeVer;
        private int mFinDeclaration;
        private int mUtilPhoneBill;
        private string mOtherDocs;
        private bool mInsurance;
        private bool mCoversRx;
        private bool mCoversCancerRx;
        private bool mCoversGlivecRx;
        private int mYearlyReassess;
        private DateTime mReassessDate;
        private string mRecommendation;
        private string mNoaPin;
        private int mDonationLength;
        private string mDonationLengthUnit;
        private string mPaymentOption;
        private bool mMSContacted;
        private DateTime CreateDate;
        private string CreatedBy;
        private DateTime ModifyDate;
        private string ModifiedBy;
        private bool mFixed;
        //patient / country
        private string PIN;
        private string PatientName;
        private string mGIPAPStatus;
        private string Street1;
        private string Street2;
        private string City;
        private string StateProvince;
        private string PostalCode;
        private string Phone;
        private string Fax;
        private string Email;
        private string Mobile;
        private string mCountryName;
        private string CurrentDosage;
        private string mTreatment;
        private string mReqTreatment;
        private string mTBLPATIENTTreatment;
        //other
        public DataTable dtNotes;
        public bool CollectNew;
        public bool Complete;
        private string PhysicianName;
        public DataTable dtPastFefs;
        private DataTable dtActionRequests;
        public DataTable dtDataLogChanges;
        public int ReassessRequestCount;
        public bool HideNotes;

        string connString = ConfigurationSettings.AppSettings["ConnectionString"];

        //**************************************************************************************************************
        public NOAFEF()
        {
            //
        }
        //**************************************************************************************************************
        public NOAFEF(int patID)
        {
            if (patID == 0)
            {
                return;
            }
            else
            {
                DataSet myData;
                SqlParameter CurrID = new SqlParameter("@PatientID", SqlDbType.Int);
                CurrID.Value = patID;

                myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetNOAFEFProfile", CurrID);
                if (myData.Tables[1].Rows.Count <= 0)
                {
                    return;
                }
                else
                {
                    Inflate(myData);
                }

                myData.Dispose();
            }
        }
		//**********************************************************************************************************************
        public void Create(string createdby)
        {
            //Add a new country to the database
            SqlParameter[] arrParams = new SqlParameter[27];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            arrParams[1] = new SqlParameter("@AddressVer", SqlDbType.Int);
            arrParams[1].Value = this.AddressVer;

            arrParams[2] = new SqlParameter("@BankStatementVer", SqlDbType.Int);
            arrParams[2].Value = this.BankStatementVer;

            arrParams[3] = new SqlParameter("@PatientConsent", SqlDbType.Int);
            arrParams[3].Value = this.PatientConsent;

            arrParams[4] = new SqlParameter("@MedicalChart", SqlDbType.Int);
            arrParams[4].Value = this.MedicalChart;

            arrParams[5] = new SqlParameter("@PhilBCRTest", SqlDbType.Int);
            arrParams[5].Value = this.PhilBCRTest;

            arrParams[6] = new SqlParameter("@CKitTest", SqlDbType.Int);
            arrParams[6].Value = this.CKitTest;

            arrParams[7] = new SqlParameter("@CopyOfID", SqlDbType.Int);
            arrParams[7].Value = this.CopyOfID;

            arrParams[8] = new SqlParameter("@Photo", SqlDbType.Int);
            arrParams[8].Value = this.Photo;

            arrParams[9] = new SqlParameter("@PrivateInsurance", SqlDbType.Int);
            arrParams[9].Value = this.PrivateInsurance;

            arrParams[10] = new SqlParameter("@TaxReturn", SqlDbType.Int);
            arrParams[10].Value = this.TaxReturn;

            arrParams[11] = new SqlParameter("@IncomeVer", SqlDbType.Int);
            arrParams[11].Value = this.IncomeVer;

            arrParams[12] = new SqlParameter("@FinDeclaration", SqlDbType.Int);
            arrParams[12].Value = this.FinDeclaration;

            arrParams[13] = new SqlParameter("@UtilPhoneBill", SqlDbType.Int);
            arrParams[13].Value = this.UtilPhoneBill;

            arrParams[14] = new SqlParameter("@OtherDocs", SqlDbType.NVarChar, 500);
            arrParams[14].Value = this.OtherDocs;

            arrParams[15] = new SqlParameter("@YearlyReassess", SqlDbType.Bit);
            if (this.YearlyReassess == -1)
            {
                arrParams[15].Value = DBNull.Value;
            }
            else
            {
                arrParams[15].Value = this.YearlyReassess;
            }

            arrParams[16] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
            arrParams[16].Value = createdby;

            arrParams[17] = new SqlParameter("@Recommendation", SqlDbType.NVarChar, 50);
            arrParams[17].Value = this.Recommendation;

            arrParams[18] = new SqlParameter("@NOAPIN", SqlDbType.NVarChar, 50);
            arrParams[18].Value = this.NoaPin;

            arrParams[19] = new SqlParameter("@DonationLength", SqlDbType.Int);
            if (this.DonationLength == -1)
            {
                arrParams[19].Value = DBNull.Value;
            }
            else
            {
                arrParams[19].Value = this.DonationLength;
            }

            arrParams[20] = new SqlParameter("@Insurance", SqlDbType.Bit);
            if (this.Insurance)
            { arrParams[20].Value = 1; }
            else
            { arrParams[20].Value = 0; }

            arrParams[21] = new SqlParameter("@CoversRx", SqlDbType.Bit);
            if (this.CoversRx)
            { arrParams[21].Value = 1; }
            else
            { arrParams[21].Value = 0; }

            arrParams[22] = new SqlParameter("@CoversCancerRx", SqlDbType.Bit);
            if (this.CoversCancerRx)
            { arrParams[22].Value = 1; }
            else
            { arrParams[22].Value = 0; }

            arrParams[23] = new SqlParameter("@CoversGlivecRx", SqlDbType.Bit);
            if (this.CoversGlivecRx)
            { arrParams[23].Value = 1; }
            else
            { arrParams[23].Value = 0; }

            arrParams[24] = new SqlParameter("@DonationLengthUnit", SqlDbType.NVarChar, 10);
            arrParams[24].Value = this.DonationLengthUnit;

            arrParams[25] = new SqlParameter("@Fixed", SqlDbType.Bit);
            if (this.Fixed)
            { arrParams[25].Value = 1; }
            else
            { arrParams[25].Value = 0; }

            arrParams[26] = new SqlParameter("@ReassessDate", SqlDbType.SmallDateTime);
            arrParams[26].Value = this.ReassessDate;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateNOAFEF", arrParams);
        }
        //**********************************************************************************************************************
        public void Update(string modifiedby)
        {
            //Add a new country to the database
            SqlParameter[] arrParams = new SqlParameter[27];

            arrParams[0] = new SqlParameter("@NOAFEFID", SqlDbType.Int);
            arrParams[0].Value = this.NOAFEFID;

            arrParams[1] = new SqlParameter("@AddressVer", SqlDbType.Int);
            arrParams[1].Value = this.AddressVer;

            arrParams[2] = new SqlParameter("@BankStatementVer", SqlDbType.Int);
            arrParams[2].Value = this.BankStatementVer;

            arrParams[3] = new SqlParameter("@PatientConsent", SqlDbType.Int);
            arrParams[3].Value = this.PatientConsent;

            arrParams[4] = new SqlParameter("@MedicalChart", SqlDbType.Int);
            arrParams[4].Value = this.MedicalChart;

            arrParams[5] = new SqlParameter("@PhilBCRTest", SqlDbType.Int);
            arrParams[5].Value = this.PhilBCRTest;

            arrParams[6] = new SqlParameter("@CKitTest", SqlDbType.Int);
            arrParams[6].Value = this.CKitTest;

            arrParams[7] = new SqlParameter("@CopyOfID", SqlDbType.Int);
            arrParams[7].Value = this.CopyOfID;

            arrParams[8] = new SqlParameter("@Photo", SqlDbType.Int);
            arrParams[8].Value = this.Photo;

            arrParams[9] = new SqlParameter("@PrivateInsurance", SqlDbType.Int);
            arrParams[9].Value = this.PrivateInsurance;

            arrParams[10] = new SqlParameter("@TaxReturn", SqlDbType.Int);
            arrParams[10].Value = this.TaxReturn;

            arrParams[11] = new SqlParameter("@IncomeVer", SqlDbType.Int);
            arrParams[11].Value = this.IncomeVer;

            arrParams[12] = new SqlParameter("@FinDeclaration", SqlDbType.Int);
            arrParams[12].Value = this.FinDeclaration;

            arrParams[13] = new SqlParameter("@UtilPhoneBill", SqlDbType.Int);
            arrParams[13].Value = this.UtilPhoneBill;

            arrParams[14] = new SqlParameter("@OtherDocs", SqlDbType.NVarChar, 500);
            arrParams[14].Value = this.OtherDocs;

            arrParams[15] = new SqlParameter("@YearlyReassess", SqlDbType.Bit);
            if (this.YearlyReassess == -1)
            {
                arrParams[15].Value = DBNull.Value;
            }
            else
            {
                arrParams[15].Value = this.YearlyReassess;
            }

            arrParams[16] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
            arrParams[16].Value = modifiedby;

            arrParams[17] = new SqlParameter("@Recommendation", SqlDbType.NVarChar, 50);
            arrParams[17].Value = this.Recommendation;

            arrParams[18] = new SqlParameter("@NOAPIN", SqlDbType.NVarChar, 50);
            arrParams[18].Value = this.NoaPin;

            arrParams[19] = new SqlParameter("@DonationLength", SqlDbType.Int);
            if (this.DonationLength == -1)
            {
                arrParams[19].Value = DBNull.Value;
            }
            else
            {
                arrParams[19].Value = this.DonationLength;
            }

            arrParams[20] = new SqlParameter("@Insurance", SqlDbType.Bit);
            if (this.Insurance)
            { arrParams[20].Value = 1; }
            else
            { arrParams[20].Value = 0; }

            arrParams[21] = new SqlParameter("@CoversRx", SqlDbType.Bit);
            if (this.CoversRx)
            { arrParams[21].Value = 1; }
            else
            { arrParams[21].Value = 0; }

            arrParams[22] = new SqlParameter("@CoversCancerRx", SqlDbType.Bit);
            if (this.CoversCancerRx)
            { arrParams[22].Value = 1; }
            else
            { arrParams[22].Value = 0; }

            arrParams[23] = new SqlParameter("@CoversGlivecRx", SqlDbType.Bit);
            if (this.CoversGlivecRx)
            { arrParams[23].Value = 1; }
            else
            { arrParams[23].Value = 0; }

            arrParams[24] = new SqlParameter("@DonationLengthUnit", SqlDbType.NVarChar, 10);
            arrParams[24].Value = this.DonationLengthUnit;

            arrParams[25] = new SqlParameter("@Fixed", SqlDbType.Bit);
            if (this.Fixed)
            { arrParams[25].Value = 1; }
            else
            { arrParams[25].Value = 0; }

            arrParams[26] = new SqlParameter("@ReassessDate", SqlDbType.SmallDateTime);
            arrParams[26].Value = this.ReassessDate;
            
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateNOAFEF", arrParams);
        }
        //**********************************************************************************************************************
        public void UpdateMSContact(string modifiedby)
        {
            //Add a new country to the database
            SqlParameter[] arrParams = new SqlParameter[4];

            arrParams[0] = new SqlParameter("@NOAFEFID", SqlDbType.Int);
            arrParams[0].Value = this.NOAFEFID;

            arrParams[1] = new SqlParameter("@MSContacted", SqlDbType.Bit);
            if (this.MSContacted)
            {
                arrParams[1].Value = 1;
            }
            else
            {
                arrParams[1].Value = 0;
            }

            arrParams[2] = new SqlParameter("@PaymentOption", SqlDbType.NVarChar, 50);
            arrParams[2].Value = this.PaymentOption;

            arrParams[3] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
            arrParams[3].Value = modifiedby;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateMSContacted", arrParams);
        }
        //**********************************************************************************************************************
        public void UpdateYearlyReassess(string modifiedby)
        {
            //Add a new country to the database
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@NOAFEFID", SqlDbType.Int);
            arrParams[0].Value = this.NOAFEFID;

            arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
            arrParams[1].Value = modifiedby;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateYearlyReassess", arrParams);
        }
        //**************************************************************************************************************
        public string Header(string uRole)
        {
            StringBuilder hSB = new StringBuilder();
            hSB.Append("<table width=100%><tr><td width=50%><font size=4 color=green>" + this.PIN + "<br>" + this.PatientName + "</font>");
            if (uRole != "FCCallCenter")
            {
                hSB.Append("<br><font color=gray>[ <a href=GIPAP.aspx?trgt=patientinfo&choice=" + this.PatientID.ToString() + ">View Patient Information</a> ]</font>");
            }
            hSB.Append("<br><font color=gray><b>" + this.GIPAPStatus + "</b>");
            hSB.Append("<br>" + this.CurrentDosage + "</font><br>");
            hSB.Append(this.Street1 + "<br>");
            if (this.Street2 != "")
            {
                hSB.Append(this.Street2 + "<br>");
            }
            hSB.Append(this.City + "<br>");
            hSB.Append(this.StateProvince + "<br>");
            hSB.Append(this.PostalCode);
            hSB.Append("</td><td><font color=gray><b>Contact Info:</b></font><br>");
            hSB.Append("<b>Phone: </b>" + this.Phone + "<br>");
            hSB.Append("<b>Fax: </b>" + this.Fax + "<br>");
            hSB.Append("<b>Mobile: </b>" + this.Mobile + "<br>");
            hSB.Append("<b>Email: </b>" + this.Email + "<br>");
            hSB.Append("<br><b><font color=gray>Physician(s):</font></b>" + this.PhysicianName);
            hSB.Append("<br><br><a href=GIPAP.aspx?trgt=fcrequest&choice=" + this.PatientID.ToString() + "><font color=green>Request FE Action</font></a>");
            hSB.Append("</td></tr></table>");
            return hSB.ToString();
        }
        //**********************************************************************************************************************
        private string DonationScheme()
        {

            int k;
            string ds = "";
            if (this.DonationLengthUnit == "Days")
            {
                k = 370 - this.DonationLength;
            }
            else if (this.DonationLengthUnit == "Months")
            {
                k = 12 - this.DonationLength;
            }
            else
            {
                k = 52 - this.DonationLength;
            }
            ds = k.ToString() + " + " + this.DonationLength.ToString() + " (" + this.DonationLengthUnit + ")";

            return ds;
        }
        //**************************************************************************************************************
        public string FEFInfo(string urole, string color)
        {
            StringBuilder fSB = new StringBuilder();
            fSB.Append("<table width=100% style='background-color:" + color + ";'>");
            fSB.Append("<tr><td colspan=2><font color=gray><b>Created: </b>" + this.CreateDate.ToString() + " " + this.CreatedBy);
            if (this.ModifiedBy != "")
            {
                fSB.Append(" | <b>Last Modified: </b>" + this.ModifyDate.ToString() + " " + this.ModifiedBy);
            }
            fSB.Append("</font></td></tr>");
            if (this.Fixed)
            {
                fSB.Append("<tr><td colspan=2 style='border: 1px solid #800000;' bgcolor=khaki><font color=maroon><b>Patient has chosen a Fixed " + this.DonationScheme() + " Donation Length</b></font></td></tr>");
            }
            this.AnswerInt(fSB, "Address has been verified", this.AddressVer);
            this.AnswerInt(fSB, "Bank statement has been verified", this.BankStatementVer);
            fSB.Append("<tr><td colspan=2><hr><b><font color=gray>FEF Documents</font></b></td></tr>");
            this.AnswerInt(fSB, "Patient Consent and Information Form:", this.PatientConsent);
            this.AnswerInt(fSB, "Summary of Medical Chart:", this.MedicalChart);
            this.AnswerInt(fSB, "Verification of Philadelphia Chromosome / BCR-Abl Results:", this.PhilBCRTest);
            this.AnswerInt(fSB, "Verification of C-Kit Test Results:", this.CKitTest);
            this.AnswerInt(fSB, "Copy of Patient's ID:", this.CopyOfID);
            this.AnswerInt(fSB, "Photo:", this.Photo);
            this.AnswerInt(fSB, "Private Insurance:", this.PrivateInsurance);
            this.AnswerInt(fSB, "Tax Return:", this.TaxReturn);
            this.AnswerInt(fSB, "Income Verification:", this.IncomeVer);
            this.AnswerInt(fSB, "Financial Declaration Form:", this.FinDeclaration);
            this.AnswerInt(fSB, "Utility / Phone Bill:", this.UtilPhoneBill);
            fSB.Append("<tr><td>Other Documents:</td><td>" + this.OtherDocs + "</td></tr>");
            fSB.Append("<tr><td colspan=2><hr><b><font color=gray>Insurance Information</font></b></td></tr>");
            this.AnswerInsurance(fSB, "Is the applicant eligible for health insurance/reimbursment?:", this.Insurance);
            this.AnswerInsurance(fSB, "If yes, does the insurance/reimbursment include prescription drugs?:", this.CoversRx);
            this.AnswerInsurance(fSB, "If yes, does the insurance/reimbursment include cancer drugs?:", this.CoversCancerRx);
            this.AnswerInsurance(fSB, "If yes, does the insurance/reimbursment include Glivec?:", this.CoversGlivecRx);
            fSB.Append("<tr><td colspan=2><hr><b><font color=gray>NOA Information</font></b></td></tr>");
            this.AnswerInt(fSB, "Re-assessment Required?:", Convert.ToInt32(this.YearlyReassess));
            fSB.Append("<tr><td>Recommendation:</td><td>" + this.Recommendation + "</td></tr>");
            fSB.Append("<tr><td>NOA / FE PIN:</td><td>" + this.NoaPin + "</td></tr>");
            if (this.DonationLength != -1)
            {
                fSB.Append("<tr><td>Donation Length:</td><td>" + this.DonationLength.ToString() + " " + this.DonationLengthUnit + " donated free</td></tr>");
            }
            else
            {
                fSB.Append("<tr><td>Donation Length:</td><td><i>Not Set</i></td></tr>");
            }
            if (!urole.StartsWith("FC"))
            {
                fSB.Append("<tr><td>Payment Option:</td><td>" + this.PaymentOption + "</td></tr>");
                /*if (this.DonationLength != 12 && this.DonationLength != -1 && this.DonationLength != 370 && this.DonationLength != 52)
                {needs to be done for all patients now*/
                    this.AnswerInt(fSB, "Contacted by Max Station about NOA approval?:", Convert.ToInt32(this.MSContacted));
                //}
            }
            fSB.Append("</table>");
            return fSB.ToString();
        }
        //**************************************************************************************************************
        public string TMFInfo()
        {
            StringBuilder fSB = new StringBuilder();
            fSB.Append("<table width=100%>");
            this.AnswerInt(fSB, "Has the patient been contacted by the Max Station about their NOA Approval?:", Convert.ToInt32(this.MSContacted));
            fSB.Append("<tr><td height=40>Payment Option:</td><td>");
            if (this.PaymentOption == "")
            {
                fSB.Append("<i>No Option Selected Yet</i>");
            }
            else
            {
                fSB.Append(this.PaymentOption);
            }
            fSB.Append("</td></tr></table>");
            return fSB.ToString();
        }
        //**************************************************************************************************************
        private void AnswerInt(StringBuilder tableSb, string question, int answer)
        {
            tableSb.Append("<tr><td width=70%>" + question + "</td><td><b>");
            if (answer == 0)
            {
                tableSb.Append("<font color=red>No</font>");
            }
            else if (answer == 1)
            {
                tableSb.Append("Yes");
            }
            else if (answer == 2)
            {
                tableSb.Append("<font color=gray>Waived</font>");
            }
            tableSb.Append("</b></td></tr>");
        } 
        //**************************************************************************************************************
        private void AnswerInsurance(StringBuilder tableSb, string question, bool answer)
        {
            tableSb.Append("<tr><td>" + question + "</td><td><b>");
            if (answer)
            {
                tableSb.Append("<font color=red>Yes</font>");
            }
            else
            {
                tableSb.Append("No");
            }
            tableSb.Append("</b></td></tr>");
        }
        //**************************************************************************************************************
        public void AddFEFNote(string createdby, string note)
        {
            SqlParameter[] arrParams = new SqlParameter[4];

            arrParams[0] = new SqlParameter("@NOAFEFID", SqlDbType.Int);
            arrParams[0].Value = this.NOAFEFID;

            arrParams[1] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[1].Value = this.PatientID;

            arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
            arrParams[2].Value = createdby;

            arrParams[3] = new SqlParameter("@Note", SqlDbType.Text);
            arrParams[3].Value = note;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateNOAFEFNote", arrParams);
        }
        //**************************************************************************************************************
        public void InflatePastFEF(int pastID)
        {
            SqlParameter arrParams = new SqlParameter("@NOAFEFID", SqlDbType.Int);
            arrParams.Value = pastID;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPastNOAFEFProfile", arrParams);

            this.Insurance = Convert.ToBoolean(ds.Tables[0].Rows[0]["Insurance"]);
            this.CoversRx = Convert.ToBoolean(ds.Tables[0].Rows[0]["CoversRx"]);
            this.CoversCancerRx = Convert.ToBoolean(ds.Tables[0].Rows[0]["CoversCancerRx"]);
            this.CoversGlivecRx = Convert.ToBoolean(ds.Tables[0].Rows[0]["CoversGlivecRx"]);
            this.Recommendation = ds.Tables[0].Rows[0]["Recommendation"].ToString();
            this.CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
            this.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateDate"]);
            this.ModifiedBy = ds.Tables[0].Rows[0]["ModifiedBy"].ToString();
            try
            {
                this.ModifyDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ModifyDate"]);
            }
            catch { }
            this.AddressVer = Convert.ToInt32(ds.Tables[0].Rows[0]["AddressVer"]);
            this.BankStatementVer = Convert.ToInt32(ds.Tables[0].Rows[0]["BankStatementVer"]);
            this.PatientConsent = Convert.ToInt32(ds.Tables[0].Rows[0]["PatientConsent"]);
            this.MedicalChart = Convert.ToInt32(ds.Tables[0].Rows[0]["MedicalChart"]);
            this.PhilBCRTest = Convert.ToInt32(ds.Tables[0].Rows[0]["PhilBCRTest"]);
            this.CKitTest = Convert.ToInt32(ds.Tables[0].Rows[0]["CKitTest"]);
            this.CopyOfID = Convert.ToInt32(ds.Tables[0].Rows[0]["CopyOfId"]);
            this.Photo = Convert.ToInt32(ds.Tables[0].Rows[0]["Photo"]);
            this.PrivateInsurance = Convert.ToInt32(ds.Tables[0].Rows[0]["PrivateInsurance"]);
            this.TaxReturn = Convert.ToInt32(ds.Tables[0].Rows[0]["TaxReturn"]);
            this.IncomeVer = Convert.ToInt32(ds.Tables[0].Rows[0]["IncomeVer"]);
            this.FinDeclaration = Convert.ToInt32(ds.Tables[0].Rows[0]["FinDeclaration"]);
            this.UtilPhoneBill = Convert.ToInt32(ds.Tables[0].Rows[0]["UtilPhoneBill"]);
            this.OtherDocs = ds.Tables[0].Rows[0]["OtherDocs"].ToString();
            try
            {
                this.YearlyReassess = Convert.ToInt32(ds.Tables[0].Rows[0]["YearlyReassess"]);
            }
            catch { this.YearlyReassess = -1; }
            try
            {
                this.ReassessDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReassessDate"]);
            }
            catch { }
            this.NoaPin = ds.Tables[0].Rows[0]["NOApin"].ToString();
            try
            {
                this.DonationLength = Convert.ToInt32(ds.Tables[0].Rows[0]["DonationLength"]);
            }
            catch { this.DonationLength = -1; }
            this.DonationLengthUnit = ds.Tables[0].Rows[0]["donationlengthunit"].ToString();
            this.PaymentOption = ds.Tables[0].Rows[0]["paymentoption"].ToString();
            this.MSContacted = Convert.ToBoolean(ds.Tables[0].Rows[0]["MSContacted"]);

            ds.Dispose();
        }
        //**************************************************************************************************************
        private void Inflate(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.CollectNew = false;
                this.NOAFEFID = Convert.ToInt32(ds.Tables[0].Rows[0]["NOAFEFID"]);
                this.Insurance = Convert.ToBoolean(ds.Tables[0].Rows[0]["Insurance"]);
                this.CoversRx = Convert.ToBoolean(ds.Tables[0].Rows[0]["CoversRx"]);
                this.CoversCancerRx = Convert.ToBoolean(ds.Tables[0].Rows[0]["CoversCancerRx"]);
                this.CoversGlivecRx = Convert.ToBoolean(ds.Tables[0].Rows[0]["CoversGlivecRx"]);
                this.Recommendation = ds.Tables[0].Rows[0]["Recommendation"].ToString();
                this.CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                this.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateDate"]);
                this.ModifiedBy = ds.Tables[0].Rows[0]["ModifiedBy"].ToString();
                this.Treatment = ds.Tables[0].Rows[0]["treatment"].ToString();
                try
                {
                    this.ModifyDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ModifyDate"]);
                }
                catch { }
                if (this.Recommendation == "")
                {
                    this.Complete = false;
                }
                else
                {
                    this.AddressVer = Convert.ToInt32(ds.Tables[0].Rows[0]["AddressVer"]);
                    this.BankStatementVer = Convert.ToInt32(ds.Tables[0].Rows[0]["BankStatementVer"]);
                    this.PatientConsent = Convert.ToInt32(ds.Tables[0].Rows[0]["PatientConsent"]);
                    this.MedicalChart = Convert.ToInt32(ds.Tables[0].Rows[0]["MedicalChart"]);
                    this.PhilBCRTest = Convert.ToInt32(ds.Tables[0].Rows[0]["PhilBCRTest"]);
                    this.CKitTest = Convert.ToInt32(ds.Tables[0].Rows[0]["CKitTest"]);
                    this.CopyOfID = Convert.ToInt32(ds.Tables[0].Rows[0]["CopyOfId"]);
                    this.Photo = Convert.ToInt32(ds.Tables[0].Rows[0]["Photo"]);
                    this.PrivateInsurance = Convert.ToInt32(ds.Tables[0].Rows[0]["PrivateInsurance"]);
                    this.TaxReturn = Convert.ToInt32(ds.Tables[0].Rows[0]["TaxReturn"]);
                    this.IncomeVer = Convert.ToInt32(ds.Tables[0].Rows[0]["IncomeVer"]);
                    this.FinDeclaration = Convert.ToInt32(ds.Tables[0].Rows[0]["FinDeclaration"]);
                    this.UtilPhoneBill = Convert.ToInt32(ds.Tables[0].Rows[0]["UtilPhoneBill"]);
                    this.OtherDocs = ds.Tables[0].Rows[0]["OtherDocs"].ToString();
                    if (ds.Tables[9].Rows.Count > 0)
                    {
                        this.ReqTreatment = ds.Tables[9].Rows[0]["treatment"].ToString();
                    }
                    try
                    {
                        this.YearlyReassess = Convert.ToInt32(ds.Tables[0].Rows[0]["YearlyReassess"]);
                    }
                    catch { this.YearlyReassess = -1; }
                    try
                    {
                        this.ReassessDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReassessDate"]);
                    }
                    catch { }
                    if (this.YearlyReassess == 1)
                    {
                        try
                        {
                            if (this.ReassessDate.AddMonths(-2) <= DateTime.Today)
                            {
                                this.CollectNew = true;
                            }
                        }
                        catch { }
                    }
                    if (this.Treatment != ds.Tables[9].Rows[0]["treatment"].ToString())
                    {
                        this.CollectNew = true;
                    }
                    this.NoaPin = ds.Tables[0].Rows[0]["NOApin"].ToString();
                    try
                    {
                        this.DonationLength = Convert.ToInt32(ds.Tables[0].Rows[0]["DonationLength"]);
                    }
                    catch { this.DonationLength = -1; }
                    this.DonationLengthUnit = ds.Tables[0].Rows[0]["donationlengthunit"].ToString();
                    this.PaymentOption = ds.Tables[0].Rows[0]["paymentoption"].ToString();
                    this.MSContacted = Convert.ToBoolean(ds.Tables[0].Rows[0]["MSContacted"]);
                    this.Fixed = Convert.ToBoolean(ds.Tables[0].Rows[0]["fixed"]);
                    if (this.NoaPin != "" && this.DonationLength != -1 && this.Recommendation == "Approve")
                    {
                        /*if (this.DonationLength == 12 || this.DonationLength == 370 || this.DonationLength == 52)
                        {
                            this.Complete = true;
                        }
                        else all patients need this now*/ if (this.MSContacted && this.PaymentOption != "")
                        {
                            this.Complete = true;
                        }
                        else
                        {
                            this.Complete = false;
                        }
                    }
                    else
                    {
                        this.Complete = false;
                    }
                }
            }
            else
            {
                this.CollectNew = true;
                this.HideNotes = true;
            }
            this.PIN = ds.Tables[1].Rows[0]["pin"].ToString();
            this.PatientID = Convert.ToInt32(ds.Tables[1].Rows[0]["PatientID"]);
            this.TBLPATIENTTreatment = ds.Tables[1].Rows[0]["treatment"].ToString();
            this.PatientName = ds.Tables[1].Rows[0]["firstname"].ToString() + " " + ds.Tables[1].Rows[0]["lastname"].ToString();
            this.GIPAPStatus = ds.Tables[1].Rows[0]["gipapstatus"].ToString();
            this.Street1 = ds.Tables[1].Rows[0]["street1"].ToString();
            this.Street2 = ds.Tables[1].Rows[0]["street2"].ToString();
            this.City = ds.Tables[1].Rows[0]["city"].ToString();
            this.StateProvince = ds.Tables[1].Rows[0]["stateprovince"].ToString();
            this.PostalCode = ds.Tables[1].Rows[0]["postalcode"].ToString();
            this.Phone = ds.Tables[1].Rows[0]["phone"].ToString();
            this.Fax = ds.Tables[1].Rows[0]["fax"].ToString();
            this.Email = ds.Tables[1].Rows[0]["email"].ToString();
            this.Mobile = ds.Tables[1].Rows[0]["mobile"].ToString();
            //make sure the current fef is valid
            if (this.CollectNew == false)
            {
                if (this.GIPAPStatus == "Denied")
                {
                    //see if a new fef has been collected for re-assessment
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["denied"]))
                    {
                        this.CollectNew = true;
                    }
                }
                else if (this.GIPAPStatus == "Closed")
                {
                    // see if you need a new fef to re-activate
                    try
                    {
                        if (Convert.ToDateTime(ds.Tables[0].Rows[0]["reassessdate"]) <= DateTime.Today.AddMonths(2) && this.YearlyReassess == 1)
                        {
                            this.CollectNew = true;
                        }
                    }
                    catch { }
                }
            }
            this.CountryName = ds.Tables[1].Rows[0]["countryname"].ToString();
            this.dtNotes = ds.Tables[2];
            //3 set proper dosage
            this.CurrentDosage = ds.Tables[3].Rows[0]["currentdosage"].ToString();
            if (this.CurrentDosage == "")
            {
                this.CurrentDosage = ds.Tables[3].Rows[0]["originalapproveddosage"].ToString();
            }
            if (this.CurrentDosage == "")
            {
                this.CurrentDosage = ds.Tables[3].Rows[0]["originalrequesteddosage"].ToString();
            }
            //physician
            if (ds.Tables[4].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    this.PhysicianName += "<li>" + ds.Tables[4].Rows[i]["physician"].ToString();
                }
            }
            this.dtPastFefs = ds.Tables[5];
            this.dtActionRequests = ds.Tables[6];
            this.dtDataLogChanges = ds.Tables[7];
            this.ReassessRequestCount = Convert.ToInt32(ds.Tables[8].Rows[0]["count"]);
        }
        //**********************************************************************************************************************
        public string ViewRequests()
        {
            StringBuilder vr = new StringBuilder();
            try
            {
                if (this.dtActionRequests.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dtActionRequests.Rows.Count; i++)
                    {
                        vr.Append("<b><font color=gray>From: </b></font>" + this.dtActionRequests.Rows[i]["Fromtype"].ToString());
                        vr.Append("<br><font color=gray>--> To: </font>" + this.dtActionRequests.Rows[i]["totype"].ToString());
                        vr.Append("<br><b>Subject: </b>" + this.dtActionRequests.Rows[i]["subject"].ToString());
                        vr.Append("<br>" + this.dtActionRequests.Rows[i]["message"].ToString());
                        vr.Append("<br><br><font color=gray>" + this.dtActionRequests.Rows[i]["createdby"].ToString() + "<br><i>" + this.dtActionRequests.Rows[i]["createdate"].ToString() + "</i></font>");
                        if (Convert.ToBoolean(this.dtActionRequests.Rows[i]["resolved"]))
                        {
                            vr.Append("<br><b>Resolved </b>" + this.dtActionRequests.Rows[i]["resolvedby"].ToString() + " <i>" + this.dtActionRequests.Rows[i]["resolvedate"].ToString() + "</i>");
                        }
                        else
                        {
                            vr.Append("<BR><font color=red><b>Unresolved</b></font>");
                        }
                        vr.Append("<hr>");
                    }
                }
                else
                {
                    vr.Append("<i>There are no Action Requests to display.</i>");
                }
            }
            catch { }
            return vr.ToString();
        }
        //**************************************************************************************************************
        public int NOAFEFID
        {
            get { return mNOAFEFID; }
            set { mNOAFEFID = value; }
        }
        //**************************************************************************************************************
        public int PatientID
        {
            get { return mPatientID; }
            set { mPatientID = value; }
        }
        //**************************************************************************************************************
        public string CountryName
        {
            get { return mCountryName; }
            set { mCountryName = value; }
        }
        //**************************************************************************************************************
        public string GIPAPStatus
        {
            get { return mGIPAPStatus; }
            set { mGIPAPStatus = value; }
        }
        //**************************************************************************************************************
        public int AddressVer
        {
            get { return mAddressVer; }
            set { mAddressVer = value; }
        }
        //**************************************************************************************************************
        public int BankStatementVer
        {
            get { return mBankStatementVer; }
            set { mBankStatementVer = value; }
        }
        //**************************************************************************************************************
        public int PatientConsent
        {
            get { return mPatientConsent; }
            set { mPatientConsent = value; }
        }
        //**************************************************************************************************************
        public int MedicalChart
        {
            get { return mMedicalChart; }
            set { mMedicalChart = value; }
        }
        //**************************************************************************************************************
        public int PhilBCRTest
        {
            get { return mPhilBCRTest; }
            set { mPhilBCRTest = value; }
        }
        //**************************************************************************************************************
        public int CKitTest
        {
            get { return mCKitTest; }
            set { mCKitTest = value; }
        }
        //**************************************************************************************************************
        public int CopyOfID
        {
            get { return mCopyOfID; }
            set { mCopyOfID = value; }
        }
        //**************************************************************************************************************
        public int Photo
        {
            get { return mPhoto; }
            set { mPhoto = value; }
        }
        //**************************************************************************************************************
        public int PrivateInsurance
        {
            get { return mPrivateInsurance; }
            set { mPrivateInsurance = value; }
        }
        //**************************************************************************************************************
        public int TaxReturn
        {
            get { return mTaxReturn; }
            set { mTaxReturn = value; }
        }
        //**************************************************************************************************************
        public int IncomeVer
        {
            get { return mIncomeVer; }
            set { mIncomeVer = value; }
        }
        //**************************************************************************************************************
        public int FinDeclaration
        {
            get { return mFinDeclaration; }
            set { mFinDeclaration = value; }
        }
        //**************************************************************************************************************
        public int UtilPhoneBill
        {
            get { return mUtilPhoneBill; }
            set { mUtilPhoneBill = value; }
        }
        //**************************************************************************************************************
        public string OtherDocs
        {
            get { return mOtherDocs; }
            set { mOtherDocs = value; }
        }
        //**************************************************************************************************************
        public bool Insurance
        {
            get { return mInsurance; }
            set { mInsurance = value; }
        }

        //**************************************************************************************************************
        public bool CoversRx
        {
            get { return mCoversRx; }
            set { mCoversRx = value; }
        }

        //**************************************************************************************************************
        public bool CoversCancerRx
        {
            get { return mCoversCancerRx; }
            set { mCoversCancerRx = value; }
        }

        //**************************************************************************************************************
        public bool CoversGlivecRx
        {
            get { return mCoversGlivecRx; }
            set { mCoversGlivecRx = value; }
        }
        //**************************************************************************************************************
        public int YearlyReassess
        {
            get { return mYearlyReassess; }
            set { mYearlyReassess = value; }
        }
        //**************************************************************************************************************
        public DateTime ReassessDate
        {
            get { return mReassessDate; }
            set { mReassessDate = value; }
        }
        //**************************************************************************************************************
        public string Recommendation
        {
            get { return mRecommendation; }
            set { mRecommendation = value; }
        }
        //**************************************************************************************************************
        public string NoaPin
        {
            get { return mNoaPin; }
            set { mNoaPin = value; }
        }
        //**************************************************************************************************************
        public int DonationLength
        {
            get { return mDonationLength; }
            set { mDonationLength = value; }
        }
        //**************************************************************************************************************
        public string DonationLengthUnit
        {
            get { return mDonationLengthUnit; }
            set { mDonationLengthUnit = value; }
        }
        //**************************************************************************************************************
        public string PaymentOption
        {
            get { return mPaymentOption; }
            set { mPaymentOption = value; }
        }
        //**************************************************************************************************************
        public bool MSContacted
        {
            get { return mMSContacted; }
            set { mMSContacted = value; }
        }
        //**************************************************************************************************************
        public bool Fixed
        {
            get { return mFixed; }
            set { mFixed = value; }
        }
        //**************************************************************************************************************
        public string Treatment
        {
            get { return mTreatment; }
            set { mTreatment = value; }
        }
        //**************************************************************************************************************
        public string ReqTreatment
        {
            get { return mReqTreatment; }
            set { mReqTreatment = value; }
        }
        //**************************************************************************************************************
        public string TBLPATIENTTreatment
        {
            get { return mTBLPATIENTTreatment; }
            set { mTBLPATIENTTreatment = value; }
        }
    }
}
