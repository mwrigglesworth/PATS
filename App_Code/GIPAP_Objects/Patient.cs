using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Patient.
	/// </summary>
	public class Patient
	{
		private int mPatientID;
		private string mPIN;
        private string CurrentProgram;
		private string mFirstName;
		private string mLastName;
        private string mThaiName;
		private string mSex;
		private DateTime mBirthDate;
		private string mStreet1;
		private string mStreet2;
		private string mCity;
		private string mStateProvince;
		private string mPostalCode;
		private string mPhone;
		private string mFax;
		private string mEmail;
		private string mMobile;
		private string mAnnualIncome;
		private string mOccupation;
		private DateTime mIntakeDate;
		private DateTime mIADate;
		private bool mAppliedForGipap;
		private bool mPatientConsent;
		private string mOriginalRequestedDosage;
		private string mOriginalApprovedDosage;
		private string mGIPAPStatus;
		private string mDiagnosis;
		private DateTime mDiagnosisDate;
		private DateTime mDeniedDate;
		private DateTime mClosedDate;
		private bool mGlivec;
		private DateTime mGlivecStartDate;
		private bool mEnableAutoApprove;
		private bool mEnableAutoClose;
		private int mApplicantID;
		// dfsp
		private bool mRecurrent;
        //mds, sys mat, hes
        private string mDiagSummary;
		//ph+ all only
		private int mRelapsedRefractory;
		private int mChemo;
		//cml
		private int mPhilPos;
		private int mBCR;
		private string mOriginalCMLPhase;
		private string mCurrentCMLPhase;
		//cml - interferon new
		private bool mInterferon;
		private string mInterferonTimeLength;
		private bool mIntolerant;
		private bool mHematologicFailure;
		private bool mCytogeneticFailure;
		//cml - interferon old
		private bool mRefractory;
		private bool mUnresponsive;
		//gist
		private int mCKitPos;
		private int mUnresectable;
        private int mMetastatic;
        // adj gist
        private bool mAdjuvant;
        private int mHighRisk;
		//gipap detail
		private string mStatusReason;
		private DateTime mStartDate;
		private DateTime mEndDate;
		private string mCurrentDosage;
		private bool mPhysicianClose;
		private bool mReminderEmail;
		private bool mReminderEmail90;
		private bool mAutoApprove;
		private bool mDelayedReapproval;
        private DateTime LalsDate;
		//data tables
		//public DataTable dsSae;
        private int SAEcount; //replacing dsSae
		public DataTable dsCaseNotes;
		public DataTable dsEmails;
		public DataTable dsStatusHistory;
		//country based display info
		public bool NeedInterferonInfo;
		public bool NeedFinancialInfo;
		public DateTime FinancialDeclarationDate;
		private bool PediatricApproved;//based on disease
		private bool GistApproved;
		private string CountryEmail;
		private int PediatricAge;
        public bool adjCountry;
		//verification
		private int VerificationID;
		private bool FinancialAffidavit;
		private bool VerificationComplete;
		private bool VerificationRed;
        //NOA FEF
        private bool mFlagNOA; //from tblpatient
        private string NOAPIN;
        private int DonationLength;
        private string DonationLengthUnit;
        private string PaymentOption;
        private bool MSContacted;
        private string Recommendation;
        private bool YearlyReassess;
        private DateTime ReassessDate;
        public bool NOAPhys; //checks physician mou
        private int TasignaPhys;
        private bool Fixed;
        //net suite order creation
        private string mOriginalTabletStrength;
        private string mTabletStrength;
        private bool mPickedUp;
        private bool Expired;
        private DateTime DetailCreateDate;
		//other tables
		private int mCountryID;
		private string mCountryName;
		private string mCountryCode;
		private int mMaxStationID;
		private string mMaxStationName;
		private int mClinicID;
		private string mClinicName;
		private int mPhysicianID;
		private string mPhysicianName;
		private int mProgramOfficerID;
		private string mProgramOfficerName;
		private string mContactName;
        public string FCBranch;
        public string mDistributorName;
        private string mReasonChanged;
        //TASIGNA FIELDS
        private string mTreatment;
        private string mReqTreatment;
        private bool mImatinib;
        private bool mGlivecIntolerant;
        private bool mGlivecResistant;
        private bool mDasatinib;
        private bool mDasatinibIntolerant;
        private bool mDasatinibResistant;
        private bool mTasigna;
        private DateTime mTasignaStartDate;
        private string mPrevTasignaDose;
        private string mOriginalRequestedTasignaDose;
        private bool mTasignaPatientConsent;
        private bool mNOATasigna;
		/*patient dataset*/
		private DataSet patientDS;
		/*counts*/
		public int PhysicianCount;
		public int CPOCount;
		public int ReapprovalRequestCount;
		public int OtherRequestCount;
		public int FEFUpdateCount;
        private int PhysTranRequests;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		string connPS = ConfigurationSettings.AppSettings["connPS"];
		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAP2;PWD=secret;UID=sa;";

		//**************************************************************************************************************
		public Patient()
		{
			//Default constructor
			this.Clear();
		}

		//**************************************************************************************************************
		public Patient(int currid, string Urole)
		{
			//Default constructor using the currid to populate the parameters
			if(currid == 0)
			{
				return;
			}
			else
			{
				DataSet myData;
				SqlParameter[] arrParams = new SqlParameter[2];

				arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
				arrParams[0].Value = currid;

				arrParams[1] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
				arrParams[1].Value = Urole;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientProfileWithDistributor", arrParams);
				if(myData.Tables[0].Rows.Count <= 0)
				{
					return;
				}
				else
				{
					Inflate(myData, Urole);
				}

				myData.Dispose();
			}
		}
		//**************************************************************************************************************
		public Patient(string createdby, int poID, int maxID, int physID, GIPAP_Objects.GIPAPApplicant applicant)
		{
			this.Create(createdby, poID, maxID, physID, applicant);
			//Default constructor using the currid to populate the parameters
			DataSet myData;
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
			arrParams[1].Value = "TMFUser";

			myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientProfileWithDistributor", arrParams);
			if(myData.Tables[0].Rows.Count <= 0)
			{
				return;
			}
			else
			{
				Inflate(myData, "TMFUser");
			}

			myData.Dispose();
		}

		//**************************************************************************************************************
		public int Create(string createdby, int poID, int maxID, int physID, GIPAP_Objects.GIPAPApplicant applicant)
		{
			//Add a new DSR to the database
			SqlParameter[] arrParams = new SqlParameter[101];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Direction = ParameterDirection.Output;

			arrParams[1] = new SqlParameter("@InsuranceType", SqlDbType.VarChar, 50);
			arrParams[1].Value = applicant.InsuranceType;

			arrParams[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
			arrParams[2].Value = applicant.FirstName.Trim();

			arrParams[3] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
			arrParams[3].Value = applicant.LastName.Trim();

			arrParams[4] = new SqlParameter("@Sex", SqlDbType.Char, 1);
			arrParams[4].Value = applicant.Sex.Trim();

			arrParams[5] = new SqlParameter("@BirthDate", SqlDbType.SmallDateTime);
			arrParams[5].Value = applicant.BirthDate;

			arrParams[6] = new SqlParameter("@Street1", SqlDbType.VarChar, 100);
			arrParams[6].Value = applicant.Street1.Trim();

			arrParams[7] = new SqlParameter("@Street2", SqlDbType.VarChar, 100);
			arrParams[7].Value = applicant.Street2.Trim();

			arrParams[8] = new SqlParameter("@City", SqlDbType.VarChar, 30);
			arrParams[8].Value = applicant.City.Trim();

			arrParams[9] = new SqlParameter("@StateProvince", SqlDbType.VarChar, 30);
			arrParams[9].Value = applicant.StateProvince.Trim();

			arrParams[10] = new SqlParameter("@PostalCode", SqlDbType.VarChar, 10);
			arrParams[10].Value = applicant.PostalCode.Trim();

			arrParams[11] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[11].Value = applicant.CountryID;

			arrParams[12] = new SqlParameter("@Phone", SqlDbType.VarChar, 50);
			arrParams[12].Value = applicant.Phone.Trim();

			arrParams[13] = new SqlParameter("@Fax", SqlDbType.VarChar, 50);
			arrParams[13].Value = applicant.Fax.Trim();

			arrParams[14] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
			arrParams[14].Value = applicant.Email.Trim();

			arrParams[15] = new SqlParameter("@ContactFirstName", SqlDbType.VarChar, 50);
			arrParams[15].Value = applicant.ContactFirstName.Trim();

			arrParams[16] = new SqlParameter("@ContactLastName", SqlDbType.VarChar, 50);
			arrParams[16].Value = applicant.ContactLastName.Trim();

			arrParams[17] = new SqlParameter("@ContactStreet1", SqlDbType.VarChar, 100);
			arrParams[17].Value = applicant.ContactStreet1.Trim();

			arrParams[18] = new SqlParameter("@ContactStreet2", SqlDbType.VarChar, 100);
			arrParams[18].Value = applicant.ContactStreet2.Trim();

			arrParams[19] = new SqlParameter("@ContactCity", SqlDbType.VarChar, 30);
			arrParams[19].Value = applicant.ContactCity.Trim();

			arrParams[20] = new SqlParameter("@ContactStateProvince", SqlDbType.VarChar, 30);
			arrParams[20].Value = applicant.ContactStateProvince.Trim();

			arrParams[21] = new SqlParameter("@ContactPostalCode", SqlDbType.VarChar, 10);
			arrParams[21].Value = applicant.ContactPostalCode.Trim();

			arrParams[22] = new SqlParameter("@ContactCountryID", SqlDbType.Int);
			arrParams[22].Value = applicant.ContactCountryID;

			arrParams[23] = new SqlParameter("@ContactPhone", SqlDbType.VarChar, 50);
			arrParams[23].Value = applicant.ContactPhone.Trim();

			arrParams[24] = new SqlParameter("@ContactFax", SqlDbType.VarChar, 50);
			arrParams[24].Value = applicant.ContactFax.Trim();

			arrParams[25] = new SqlParameter("@ContactEmail", SqlDbType.VarChar, 100);
			arrParams[25].Value = applicant.ContactEmail.Trim();

			arrParams[26] = new SqlParameter("@Relationship", SqlDbType.VarChar, 30);
			arrParams[26].Value = applicant.ContactRelationship.Trim();

			arrParams[27] = new SqlParameter("@RelationshipDetails", SqlDbType.VarChar, 100);
			arrParams[27].Value = applicant.RelationshipDetails.Trim();

			arrParams[28] = new SqlParameter("@AnnualIncome", SqlDbType.NVarChar, 20);
			arrParams[28].Value = applicant.AnnualIncome.Trim();

			arrParams[29] = new SqlParameter("@Occupation", SqlDbType.NVarChar, 50);
			arrParams[29].Value = applicant.Occupation;

			arrParams[30] = new SqlParameter("@BCR", SqlDbType.Int);
			arrParams[30].Value = applicant.BCR;

			arrParams[31] = new SqlParameter("@AppliedForGIPAP", SqlDbType.Bit);
			if(applicant.AppliedForGIPAP)
			{arrParams[31].Value = 1;}
			else
			{arrParams[31].Value = 0;}

			arrParams[32] = new SqlParameter("@MaxStationID", SqlDbType.Int);
			arrParams[32].Value = maxID;

			arrParams[33] = new SqlParameter("@Dosage", SqlDbType.NVarChar, 20);
			arrParams[33].Value = applicant.Dosage.Trim();
	
			arrParams[34] = new SqlParameter("@Diagnosis", SqlDbType.VarChar, 25);
			arrParams[34].Value = applicant.Diagnosis;

			arrParams[35] = new SqlParameter("@DiagnosisDate", SqlDbType.SmallDateTime);
			arrParams[35].Value = applicant.DiagnosisDate;

			arrParams[36] = new SqlParameter("@PhilPos", SqlDbType.Int);
			arrParams[36].Value = applicant.PhilPositive;

			arrParams[37] = new SqlParameter("@CMLPhase", SqlDbType.NVarChar, 20);
			arrParams[37].Value = applicant.CMLPhase.Trim();

			arrParams[38] = new SqlParameter("@Interferon", SqlDbType.Bit);
			arrParams[38].Value = applicant.Interferon;

			arrParams[39] = new SqlParameter("@InterferonTimeLength", SqlDbType.NVarChar, 20);
			arrParams[39].Value = applicant.InterferonTimeLength.Trim();

			arrParams[40] = new SqlParameter("@Intolerant", SqlDbType.Bit);
			arrParams[40].Value = applicant.Intolerant;

			arrParams[41] = new SqlParameter("@HematologicFailure", SqlDbType.Bit);
			arrParams[41].Value = applicant.HematologicFailure;

			arrParams[42] = new SqlParameter("@CytogeneticFailure", SqlDbType.Bit);
			arrParams[42].Value = applicant.CytogeneticFailure;

			arrParams[43] = new SqlParameter("@CKitPos", SqlDbType.Int);
			arrParams[43].Value = applicant.CKitPos;

			arrParams[44] = new SqlParameter("@Unresectable", SqlDbType.Int);
			arrParams[44].Value = applicant.Unresectable;

			arrParams[45] = new SqlParameter("@Metastatic", SqlDbType.Int);
			arrParams[45].Value = applicant.Metastatic;

			arrParams[46] = new SqlParameter("@PhysicianID", SqlDbType.Int);
			arrParams[46].Value = physID;

			arrParams[47] = new SqlParameter("@POID", SqlDbType.Int);
			arrParams[47].Value = poID;

			arrParams[48] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
			arrParams[48].Value = createdby;

			arrParams[49] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30); 
			arrParams[49].Value = applicant.Mobile.Trim();

			arrParams[50] = new SqlParameter("@ContactMobile", SqlDbType.NVarChar, 30); 
			arrParams[50].Value = applicant.ContactMobile.Trim();

			arrParams[51] = new SqlParameter("@Glivec", SqlDbType.Bit); 
			arrParams[51].Value = applicant.Glivec;

			arrParams[52] = new SqlParameter("@GlivecStartDate", SqlDbType.DateTime); 
			if(applicant.GlivecStartDate == Convert.ToDateTime("1/1/0001"))
			{
				arrParams[52].Value = DBNull.Value;
			}
			else
			{
				arrParams[52].Value = applicant.GlivecStartDate;
			}

			arrParams[53] = new SqlParameter("@Insurance", SqlDbType.Bit); 
			if(applicant.Insurance)
			{arrParams[53].Value = 1;}
			else
			{arrParams[53].Value = 0;}
			
			arrParams[54] = new SqlParameter("@CoversRx", SqlDbType.Bit); 
			if(applicant.CoversRx)
			{arrParams[54].Value = 1;}
			else
			{arrParams[54].Value = 0;}

			arrParams[55] = new SqlParameter("@CoversCancerRx", SqlDbType.Bit); 
			if(applicant.CoversCancerRx)
			{arrParams[55].Value = 1;}
			else
			{arrParams[55].Value = 0;}

			arrParams[56] = new SqlParameter("@CoversGlivecRx", SqlDbType.Bit); 
			if(applicant.CoversGlivecRx)
			{arrParams[56].Value = 1;}
			else
			{arrParams[56].Value = 0;}

			/*Verification*/

			arrParams[57] = new SqlParameter("@MedicalChart", SqlDbType.Int);
			arrParams[57].Value = applicant.MedicalChart;

			arrParams[58] = new SqlParameter("@PhiladelphiaVerification", SqlDbType.Int);
			arrParams[58].Value = applicant.PhiladelphiaVerification;

			arrParams[59] = new SqlParameter("@CKitVerification", SqlDbType.Int);
			arrParams[59].Value = applicant.CKitVerification;

			arrParams[60] = new SqlParameter("@CopyOfID", SqlDbType.Int);
			arrParams[60].Value = applicant.CopyOfID;

			arrParams[61] = new SqlParameter("@Photo", SqlDbType.Int);
			arrParams[61].Value = applicant.Photo;

			arrParams[62] = new SqlParameter("@SSCard", SqlDbType.Int);
			arrParams[62].Value = applicant.SSCard;

			arrParams[63] = new SqlParameter("@InsuranceCard", SqlDbType.Int);
			arrParams[63].Value = applicant.InsuranceCard;

			arrParams[64] = new SqlParameter("@TaxReturn", SqlDbType.Int);
			arrParams[64].Value = applicant.TaxReturn;

			arrParams[65] = new SqlParameter("@SalarySlip", SqlDbType.Int);
			arrParams[65].Value = applicant.SalarySlip;

			arrParams[66] = new SqlParameter("@PhoneBill", SqlDbType.Int);
			arrParams[66].Value = applicant.PhoneBill;

			arrParams[67] = new SqlParameter("@OtherDocs", SqlDbType.VarChar, 500);
			arrParams[67].Value = applicant.OtherDocs;

			arrParams[68] = new SqlParameter("@HouseholdMembers", SqlDbType.Int);
			arrParams[68].Value = applicant.HouseholdMembers;

			arrParams[69] = new SqlParameter("@HouseholdOccupation", SqlDbType.VarChar, 500);
			arrParams[69].Value = applicant.HouseholdOccupation;

			arrParams[70] = new SqlParameter("@HouseholdIncome", SqlDbType.NVarChar, 20);
			arrParams[70].Value = applicant.HouseholdIncome;

			arrParams[71] = new SqlParameter("@AdditionalFunds", SqlDbType.NVarChar, 20);
			arrParams[71].Value = applicant.AdditionalFunds;

			arrParams[72] = new SqlParameter("@HouseholdAssets", SqlDbType.NVarChar, 20);
			arrParams[72].Value = applicant.HouseholdAssets;

			arrParams[73] = new SqlParameter("@Recommendation", SqlDbType.VarChar, 50);
			arrParams[73].Value = applicant.Recommendation;

			arrParams[74] = new SqlParameter("@Explanation", SqlDbType.Text);
			arrParams[74].Value = applicant.Explanation;

			arrParams[75] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[75].Value = applicant.Notes;

			arrParams[76] = new SqlParameter("@ApplicantID", SqlDbType.Int);
			arrParams[76].Value = applicant.ApplicantID;

			arrParams[77] = new SqlParameter("@FinancialAffidavit", SqlDbType.Int);
			arrParams[77].Value = applicant.FinancialAffidavit;

			arrParams[78] = new SqlParameter("@PatientConsent", SqlDbType.Bit);
			if(applicant.PatientConsent)
			{arrParams[78].Value = 1;}
			else
			{arrParams[78].Value = 0;}

			arrParams[79] = new SqlParameter("@RelapsedRefractory", SqlDbType.Int);
			arrParams[79].Value = applicant.RelapsedRefractory;

			arrParams[80] = new SqlParameter("@Chemo", SqlDbType.Int);
			arrParams[80].Value = applicant.Chemo;

			arrParams[81] = new SqlParameter("@Recurrent", SqlDbType.Bit);
			if(applicant.Recurrent)
			{arrParams[81].Value = 1;}
			else
			{arrParams[81].Value = 0;}

			arrParams[82] = new SqlParameter("@PIN", SqlDbType.NVarChar, 50);
			arrParams[82].Direction = ParameterDirection.Output;

			arrParams[83] = new SqlParameter("@FlagNOA", SqlDbType.Bit);
			arrParams[83].Direction = ParameterDirection.Output;

            arrParams[84] = new SqlParameter("@HighRisk", SqlDbType.Int);
            arrParams[84].Value = applicant.HighRisk;

            arrParams[85] = new SqlParameter("@DiagSummary", SqlDbType.Text);
            arrParams[85].Value = applicant.DiagSummary;

            //TASIGNA FIELDS
            arrParams[86] = new SqlParameter("@NOATasigna", SqlDbType.Bit);
            if (applicant.NOATasigna)
            { arrParams[86].Value = 1; }
            else
            { arrParams[86].Value = 0; }

            arrParams[87] = new SqlParameter("@Treatment", SqlDbType.NVarChar, 20);
            arrParams[87].Value = applicant.Treatment;

            arrParams[88] = new SqlParameter("@Imatinib", SqlDbType.Bit);
            if (applicant.Imatinib)
            { arrParams[88].Value = 1; }
            else
            { arrParams[88].Value = 0; }

            arrParams[89] = new SqlParameter("@GlivecIntolerant", SqlDbType.Bit);
            if (applicant.GlivecIntolerant)
            { arrParams[89].Value = 1; }
            else
            { arrParams[89].Value = 0; }

            arrParams[90] = new SqlParameter("@GlivecResistant", SqlDbType.Bit);
            if (applicant.GlivecResistant)
            { arrParams[90].Value = 1; }
            else
            { arrParams[90].Value = 0; }

            arrParams[91] = new SqlParameter("@Dasatinib", SqlDbType.Bit);
            if (applicant.Dasatinib)
            { arrParams[91].Value = 1; }
            else
            { arrParams[91].Value = 0; }

            arrParams[92] = new SqlParameter("@DasatinibIntolerant", SqlDbType.Bit);
            if (applicant.DasatinibIntolerant)
            { arrParams[92].Value = 1; }
            else
            { arrParams[92].Value = 0; }

            arrParams[93] = new SqlParameter("@DasatinibResistant", SqlDbType.Bit);
            if (applicant.DasatinibResistant)
            { arrParams[93].Value = 1; }
            else
            { arrParams[93].Value = 0; }

            arrParams[94] = new SqlParameter("@Tasigna", SqlDbType.Bit);
            if (applicant.Tasigna)
            { arrParams[94].Value = 1; }
            else
            { arrParams[94].Value = 0; }

            arrParams[95] = new SqlParameter("@TasignaStartDate", SqlDbType.SmallDateTime);
            if (applicant.TasignaStartDate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[95].Value = applicant.TasignaStartDate;
            }
            else
            {
                arrParams[95].Value = DBNull.Value;
            }

            arrParams[96] = new SqlParameter("@PrevTasignaDose", SqlDbType.NVarChar, 20);
            arrParams[96].Value = applicant.PrevTasignaDose;

            arrParams[97] = new SqlParameter("@TasignaPatientConsent", SqlDbType.Bit);
            arrParams[97].Value = applicant.TasignaPatientConsent;

            arrParams[98] = new SqlParameter("@Adjuvant", SqlDbType.Bit);
            if (applicant.Adjuvant)
            { arrParams[98].Value = 1; }
            else
            { arrParams[98].Value = 0; }

            arrParams[99] = new SqlParameter("@OriginalTabletStrength", SqlDbType.VarChar, 20);
            arrParams[99].Value = applicant.TabletStrength;

            arrParams[100] = new SqlParameter("@ThaiName", SqlDbType.NVarChar, 100);
            arrParams[100].Value = applicant.ThaiName;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePatient", arrParams);

			//Return the newly created records ID
			this.PatientID = (int)arrParams[0].Value;
			this.PIN = arrParams[82].Value.ToString();
            this.FlagNOA = Convert.ToBoolean(arrParams[83].Value);
            if (this.FlagNOA)
            {
                this.CurrentProgram = "NOA-GIPAP";
            }
            else
            {
                this.CurrentProgram = "GIPAP";
            }

			//If the returned value is -1 then the record already exists
			if(this.PatientID == -1)
			{
				throw new ArgumentException("The patient already exists.");
			}
			try
			{
				this.CreatePS(createdby, this.PIN, physID, applicant);
			}
			catch{}
			return this.PatientID;
		}
		//**************************************************************************************************************
		private void CreatePS(string createdby, string gipapPIN, int physID, GIPAP_Objects.GIPAPApplicant applicant)
		{
			//Add a new DSR to the database
			SqlParameter[] arrParams = new SqlParameter[33];

			arrParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
			arrParams[0].Value = physID;

			arrParams[1] = new SqlParameter("@PIN", SqlDbType.VarChar, 50);
			arrParams[1].Value = gipapPIN;

			arrParams[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
			arrParams[2].Value = applicant.FirstName.Trim();

			arrParams[3] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
			arrParams[3].Value = applicant.LastName.Trim();

			arrParams[4] = new SqlParameter("@Sex", SqlDbType.Char, 1);
			arrParams[4].Value = applicant.Sex.Trim();

			arrParams[5] = new SqlParameter("@BirthDate", SqlDbType.SmallDateTime);
			arrParams[5].Value = applicant.BirthDate;

			arrParams[6] = new SqlParameter("@Street1", SqlDbType.VarChar, 100);
			arrParams[6].Value = applicant.Street1.Trim();

			arrParams[7] = new SqlParameter("@Street2", SqlDbType.VarChar, 100);
			arrParams[7].Value = applicant.Street2.Trim();

			arrParams[8] = new SqlParameter("@City", SqlDbType.VarChar, 30);
			arrParams[8].Value = applicant.City.Trim();

			arrParams[9] = new SqlParameter("@StateProvince", SqlDbType.VarChar, 30);
			arrParams[9].Value = applicant.StateProvince.Trim();

			arrParams[10] = new SqlParameter("@PostalCode", SqlDbType.VarChar, 10);
			arrParams[10].Value = applicant.PostalCode.Trim();

			arrParams[11] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[11].Value = applicant.CountryID;

			arrParams[12] = new SqlParameter("@Phone", SqlDbType.VarChar, 50);
			arrParams[12].Value = applicant.Phone.Trim();

			arrParams[13] = new SqlParameter("@Fax", SqlDbType.VarChar, 50);
			arrParams[13].Value = applicant.Fax.Trim();

			arrParams[14] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
			arrParams[14].Value = applicant.Email.Trim();

			arrParams[15] = new SqlParameter("@ContactFirstName", SqlDbType.VarChar, 50);
			arrParams[15].Value = applicant.ContactFirstName.Trim();

			arrParams[16] = new SqlParameter("@ContactLastName", SqlDbType.VarChar, 50);
			arrParams[16].Value = applicant.ContactLastName.Trim();

			arrParams[17] = new SqlParameter("@ContactStreet1", SqlDbType.VarChar, 100);
			arrParams[17].Value = applicant.ContactStreet1.Trim();

			arrParams[18] = new SqlParameter("@ContactStreet2", SqlDbType.VarChar, 100);
			arrParams[18].Value = applicant.ContactStreet2.Trim();

			arrParams[19] = new SqlParameter("@ContactCity", SqlDbType.VarChar, 30);
			arrParams[19].Value = applicant.ContactCity.Trim();

			arrParams[20] = new SqlParameter("@ContactStateProvince", SqlDbType.VarChar, 30);
			arrParams[20].Value = applicant.ContactStateProvince.Trim();

			arrParams[21] = new SqlParameter("@ContactPostalCode", SqlDbType.VarChar, 10);
			arrParams[21].Value = applicant.ContactPostalCode.Trim();

			arrParams[22] = new SqlParameter("@ContactCountryID", SqlDbType.Int);
			arrParams[22].Value = applicant.ContactCountryID;

			arrParams[23] = new SqlParameter("@ContactPhone", SqlDbType.VarChar, 50);
			arrParams[23].Value = applicant.ContactPhone.Trim();

			arrParams[24] = new SqlParameter("@ContactFax", SqlDbType.VarChar, 50);
			arrParams[24].Value = applicant.ContactFax.Trim();

			arrParams[25] = new SqlParameter("@ContactEmail", SqlDbType.VarChar, 100);
			arrParams[25].Value = applicant.ContactEmail.Trim();

			arrParams[26] = new SqlParameter("@Relationship", SqlDbType.VarChar, 30);
			arrParams[26].Value = applicant.ContactRelationship.Trim();

			arrParams[27] = new SqlParameter("@Diagnosis", SqlDbType.VarChar, 100);
			arrParams[27].Value = applicant.Diagnosis;

			arrParams[28] = new SqlParameter("@DiagnosisYear", SqlDbType.NVarChar, 50);
			arrParams[28].Value = applicant.DiagnosisDate.Year.ToString();

			arrParams[29] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
			arrParams[29].Value = createdby;

			arrParams[30] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30); 
			arrParams[30].Value = applicant.Mobile.Trim();

			arrParams[31] = new SqlParameter("@ContactMobile", SqlDbType.NVarChar, 30); 
			arrParams[31].Value = applicant.ContactMobile.Trim();

            arrParams[32] = new SqlParameter("@Source", SqlDbType.NVarChar, 20);
            arrParams[32].Value = this.CurrentProgram;

			SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_CreateGIPAPPatient", arrParams);
		}

		//**************************************************************************************************************	
		public void Update(string modifiedby, string reason)
		{
			//Add a new DSR to the database
			SqlParameter[] arrParams = new SqlParameter[53];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30); 
			arrParams[1].Value = this.Mobile.Trim();

			arrParams[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
			arrParams[2].Value = this.FirstName.Trim();

			arrParams[3] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
			arrParams[3].Value = this.LastName.Trim();

			arrParams[4] = new SqlParameter("@Sex", SqlDbType.Char, 1);
			arrParams[4].Value = this.Sex.Trim();

			arrParams[5] = new SqlParameter("@BirthDate", SqlDbType.SmallDateTime);
			arrParams[5].Value = this.BirthDate;

			arrParams[6] = new SqlParameter("@Street1", SqlDbType.VarChar, 100);
			arrParams[6].Value = this.Street1.Trim();

			arrParams[7] = new SqlParameter("@Street2", SqlDbType.VarChar, 100);
			arrParams[7].Value = this.Street2.Trim();

			arrParams[8] = new SqlParameter("@City", SqlDbType.VarChar, 30);
			arrParams[8].Value = this.City.Trim();

			arrParams[9] = new SqlParameter("@StateProvince", SqlDbType.VarChar, 30);
			arrParams[9].Value = this.StateProvince.Trim();

			arrParams[10] = new SqlParameter("@PostalCode", SqlDbType.VarChar, 10);
			arrParams[10].Value = this.PostalCode.Trim();

			arrParams[11] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[11].Value = this.CountryID;

			arrParams[12] = new SqlParameter("@Phone", SqlDbType.VarChar, 30);
			arrParams[12].Value = this.Phone.Trim();

			arrParams[13] = new SqlParameter("@Fax", SqlDbType.VarChar, 30);
			arrParams[13].Value = this.Fax.Trim();

			arrParams[14] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
			arrParams[14].Value = this.Email.Trim();

			arrParams[15] = new SqlParameter("@AnnualIncome", SqlDbType.NVarChar, 20);
			arrParams[15].Value = this.AnnualIncome.Trim();

			arrParams[16] = new SqlParameter("@Occupation", SqlDbType.NVarChar, 50);
			arrParams[16].Value = this.Occupation;

			arrParams[17] = new SqlParameter("@BCR", SqlDbType.Int);
			arrParams[17].Value = this.BCR;

			arrParams[18] = new SqlParameter("@ReasonChanged", SqlDbType.NVarChar, 200);
			arrParams[18].Value = reason;
	
			arrParams[19] = new SqlParameter("@DiagnosisDate", SqlDbType.SmallDateTime);
			if(this.DiagnosisDate == Convert.ToDateTime("1/1/0001"))
			{
				arrParams[19].Value = DBNull.Value;
			}
			else
			{
				arrParams[19].Value = this.DiagnosisDate;
			}

			arrParams[20] = new SqlParameter("@PhilPos", SqlDbType.Int);
			arrParams[20].Value = this.PhilPos;

			arrParams[21] = new SqlParameter("@CMLPhase", SqlDbType.NVarChar, 20);
			arrParams[21].Value = this.CurrentCMLPhase;

			arrParams[22] = new SqlParameter("@Interferon", SqlDbType.Bit);
			arrParams[22].Value = this.Interferon;

			arrParams[23] = new SqlParameter("@InterferonTimeLength", SqlDbType.NVarChar, 20);
			arrParams[23].Value = this.InterferonTimeLength;

			arrParams[24] = new SqlParameter("@Intolerant", SqlDbType.Bit);
			arrParams[24].Value = this.Intolerant;

			arrParams[25] = new SqlParameter("@HematologicFailure", SqlDbType.Bit);
			arrParams[25].Value = this.HematologicFailure;

			arrParams[26] = new SqlParameter("@CytogeneticFailure", SqlDbType.Bit);
			arrParams[26].Value = this.CytogeneticFailure;

			arrParams[27] = new SqlParameter("@CKitPos", SqlDbType.Int);
			arrParams[27].Value = this.CKitPos;

			arrParams[28] = new SqlParameter("@Unresectable", SqlDbType.Int);
			arrParams[28].Value = this.Unresectable;

			arrParams[29] = new SqlParameter("@Metastatic", SqlDbType.Int);
			arrParams[29].Value = this.Metastatic;

			arrParams[30] = new SqlParameter("@RelapsedRefractory", SqlDbType.Int);
			arrParams[30].Value = this.RelapsedRefractory;

			arrParams[31] = new SqlParameter("@Chemo", SqlDbType.Int);
			arrParams[31].Value = this.Chemo;

			arrParams[32] = new SqlParameter("@Glivec", SqlDbType.Bit); 
			arrParams[32].Value = this.Glivec;

			arrParams[33] = new SqlParameter("@GlivecStartDate", SqlDbType.DateTime); 
			if(this.GlivecStartDate == Convert.ToDateTime("1/1/0001"))
			{
				arrParams[33].Value = DBNull.Value;
			}
			else
			{
				arrParams[33].Value = this.GlivecStartDate;
			}

			arrParams[34] = new SqlParameter("@Diagnosis", SqlDbType.VarChar, 25);
			arrParams[34].Value = this.Diagnosis;

			arrParams[35] = new SqlParameter("@PatientConsent", SqlDbType.Bit);
			arrParams[35].Value = this.PatientConsent;

			arrParams[36] = new SqlParameter("@Recurrent", SqlDbType.Bit);
			arrParams[36].Value = this.Recurrent;

			arrParams[37] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
            arrParams[37].Value = modifiedby;

            arrParams[38] = new SqlParameter("@HighRisk", SqlDbType.Int);
            arrParams[38].Value = this.HighRisk;

            arrParams[39] = new SqlParameter("@DiagSummary", SqlDbType.Text);
            arrParams[39].Value = this.DiagSummary;

            //TASIGNA FIELDS
            arrParams[40] = new SqlParameter("@NOATasigna", SqlDbType.Bit);
            if (this.NOATasigna)
            { arrParams[40].Value = 1; }
            else
            { arrParams[40].Value = 0; }

            arrParams[41] = new SqlParameter("@Imatinib", SqlDbType.Bit);
            if (this.Imatinib)
            { arrParams[41].Value = 1; }
            else
            { arrParams[41].Value = 0; }

            arrParams[42] = new SqlParameter("@GlivecIntolerant", SqlDbType.Bit);
            if (this.GlivecIntolerant)
            { arrParams[42].Value = 1; }
            else
            { arrParams[42].Value = 0; }

            arrParams[43] = new SqlParameter("@GlivecResistant", SqlDbType.Bit);
            if (this.GlivecResistant)
            { arrParams[43].Value = 1; }
            else
            { arrParams[43].Value = 0; }

            arrParams[44] = new SqlParameter("@Dasatinib", SqlDbType.Bit);
            if (this.Dasatinib)
            { arrParams[44].Value = 1; }
            else
            { arrParams[44].Value = 0; }

            arrParams[45] = new SqlParameter("@DasatinibIntolerant", SqlDbType.Bit);
            if (this.DasatinibIntolerant)
            { arrParams[45].Value = 1; }
            else
            { arrParams[45].Value = 0; }

            arrParams[46] = new SqlParameter("@DasatinibResistant", SqlDbType.Bit);
            if (this.DasatinibResistant)
            { arrParams[46].Value = 1; }
            else
            { arrParams[46].Value = 0; }

            arrParams[47] = new SqlParameter("@Tasigna", SqlDbType.Bit);
            if (this.Tasigna)
            { arrParams[47].Value = 1; }
            else
            { arrParams[47].Value = 0; }

            arrParams[48] = new SqlParameter("@TasignaStartDate", SqlDbType.SmallDateTime);
            if (this.TasignaStartDate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[48].Value = this.TasignaStartDate;
            }
            else
            {
                arrParams[48].Value = DBNull.Value;
            }

            arrParams[49] = new SqlParameter("@TasignaPatientConsent", SqlDbType.Bit);
            arrParams[49].Value = this.TasignaPatientConsent;

            arrParams[50] = new SqlParameter("@Treatment", SqlDbType.NVarChar, 20);
            arrParams[50].Value = this.Treatment;

            arrParams[51] = new SqlParameter("@Adjuvant", SqlDbType.Bit);
            arrParams[51].Value = this.Adjuvant;

            arrParams[52] = new SqlParameter("@ThaiName", SqlDbType.NVarChar, 100);
            arrParams[52].Value = this.ThaiName;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatient", arrParams);

			this.UpdatePS(modifiedby);
		}
		//**************************************************************************************************************	
		private void UpdatePS(string modifiedby)
		{
			//Add a new DSR to the database
			SqlParameter[] arrParams = new SqlParameter[17];

			arrParams[0] = new SqlParameter("@PIN", SqlDbType.NVarChar, 50);
			arrParams[0].Value = this.PIN;

			arrParams[1] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30); 
			arrParams[1].Value = this.Mobile.Trim();

			arrParams[2] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
			arrParams[2].Value = this.FirstName.Trim();

			arrParams[3] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
			arrParams[3].Value = this.LastName.Trim();

			arrParams[4] = new SqlParameter("@Sex", SqlDbType.Char, 1);
			arrParams[4].Value = this.Sex.Trim();

			arrParams[5] = new SqlParameter("@BirthDate", SqlDbType.SmallDateTime);
			arrParams[5].Value = this.BirthDate;

			arrParams[6] = new SqlParameter("@Street1", SqlDbType.VarChar, 100);
			arrParams[6].Value = this.Street1.Trim();

			arrParams[7] = new SqlParameter("@Street2", SqlDbType.VarChar, 100);
			arrParams[7].Value = this.Street2.Trim();

			arrParams[8] = new SqlParameter("@City", SqlDbType.VarChar, 30);
			arrParams[8].Value = this.City.Trim();

			arrParams[9] = new SqlParameter("@StateProvince", SqlDbType.VarChar, 30);
			arrParams[9].Value = this.StateProvince.Trim();

			arrParams[10] = new SqlParameter("@PostalCode", SqlDbType.VarChar, 10);
			arrParams[10].Value = this.PostalCode.Trim();

			arrParams[11] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[11].Value = this.CountryID;

			arrParams[12] = new SqlParameter("@Phone", SqlDbType.VarChar, 30);
			arrParams[12].Value = this.Phone.Trim();

			arrParams[13] = new SqlParameter("@Fax", SqlDbType.VarChar, 30);
			arrParams[13].Value = this.Fax.Trim();

			arrParams[14] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
			arrParams[14].Value = this.Email.Trim();

			arrParams[15] = new SqlParameter("@DiagnosisYear", SqlDbType.NVarChar, 50);
			if(this.DiagnosisDate == Convert.ToDateTime("1/1/0001"))
			{
				arrParams[15].Value = DBNull.Value;
			}
			else
			{
				arrParams[15].Value = this.DiagnosisDate.Year.ToString();
			}

			arrParams[16] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
			arrParams[16].Value = modifiedby;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_UpdateGIPAPPatient", arrParams);
		}
		
		//**********************************************************************************************************************
		public DataSet DynamicSearch(System.Web.UI.WebControls.ListBox lbFields, System.Web.UI.WebControls.ListBox lbSort, bool conts)
		{
			string sqlStr = "Select PIN";
			if(lbFields.Items.Count > 1)
			{
				for(int i=1; i<lbFields.Items.Count; i++)
				{
					sqlStr += ", " + lbFields.Items[i].Value;
				}
                foreach (string it in lbFields.Items)
                {
                    sqlStr = it.ToString();
                }
			}
			sqlStr += " From v_DynamicPatientSearchNovartis ";
			if(conts)
			{
				sqlStr += "Where Pin like '%' ";
				if(this.CountryID != 0)
				{
					sqlStr += " and countryid = " + this.CountryID.ToString();
				}
				if(this.GIPAPStatus != "0")
				{
					sqlStr += " and status = '" + this.GIPAPStatus + "' ";
				}
				if(this.Diagnosis != "0")
				{
					sqlStr += " and diagnosis = '" + this.Diagnosis + "' ";
				}
				if(this.OriginalCMLPhase != "0")
				{
					sqlStr += " and [initial phase] = '" + this.OriginalCMLPhase + "' ";
				}
				if(this.OriginalApprovedDosage != "0")
				{
					sqlStr += " and dosage = '" + this.OriginalApprovedDosage + "' ";
				}
			}
			if(lbSort.Items.Count > 0)
			{
				sqlStr += "Order by ";
				if(lbSort.Items[0].Value == "[Last Reapproval Date]")
				{
					sqlStr += "startdate";
				}
				else if(lbSort.Items[0].Value == "[Initial Approval]" )
				{
					sqlStr += "iadate";
				}
				else if(lbSort.Items[0].Value == "[Closed Date]")
				{
					sqlStr += "closeddate";
				}
				else 
				{
					sqlStr += lbSort.Items[0].Value;
				}
				for(int i=1; i<lbSort.Items.Count; i++)
				{
					if(lbSort.Items[i].Value == "[Last Reapproval Date]")
					{
						sqlStr += ", startdate";
					}
					else if(lbSort.Items[i].Value == "[Initial Approval]" )
					{
						sqlStr += ", iadate";
					}
					else if(lbSort.Items[i].Value == "[Closed Date]")
					{
						sqlStr += ", closeddate";
					}
					else 
					{
						sqlStr += ", " + lbSort.Items[i].Value;
					}
				}
			}
			return SqlHelper.ExecuteDataset(connString, CommandType.Text, sqlStr);
		}


		//**********************************************************************************************************************
		public DataSet PatientSearch(string extras, GIPAP_Objects.User searchUser)
		{
			DataSet ds = new DataSet();
			string strSQL = "Select '<a href=PatientInfo.aspx?choice=' + Convert(varchar(10), PatientID) + '>' + PIN + '</a>' as '<strong>PIN</strong>', LastName as '<strong>Last Name</strong>', FirstName as '<strong>First Name</strong>', GIPAPStatus as '<strong>Status</strong>', currentprogram as '<b>Program</b>' from v_PatientSearch where ";
			strSQL += "PIN like '" + this.PIN + "%' and ";
			strSQL += "FirstName like '" + this.FirstName + "%' and ";
			strSQL += "LastName like '" + this.LastName + "%' and ";
			strSQL += "Phone like '" + this.Phone + "%' and ";
			strSQL += "Fax like '" + this.Fax + "%' and ";
			strSQL += extras;
			strSQL += "Email like '" + this.Email + "%' and ";
			strSQL += "Diagnosis like '" + this.Diagnosis + "%' and ";
            if (this.Diagnosis == "GIST")
                strSQL += "adjuvant = " + Convert.ToByte(this.Adjuvant) + " and ";
			strSQL += "statusreason like '" + this.StatusReason + "%' and ";
			if(this.CurrentDosage != "0mg")
			{
				strSQL += "CurrentDosage like '" + this.CurrentDosage + "%' and ";
			}
			strSQL += "CurrentCMLPhase like '" + this.CurrentCMLPhase + "%' and ";
			strSQL += "StateProvince like '" + this.StateProvince + "%' and ";
			if (this.CountryID != 0)
			{
				strSQL += "CountryID = " + this.CountryID.ToString() + " and ";
			}
			if(this.Sex != "")
			{
				strSQL += "sex = '" + this.Sex + "' and ";
			}
			strSQL += "City like '" + this.City + "%' ";
			if(searchUser.Role == "MaxStation")
			{
                if (searchUser.CountryID == 76)
                {
                    strSQL += " and countryid = 76 ";
                }
                else
                {
                    strSQL += " and patientid in (select patientid from tblmaxstation a, tblperson b where a.personid = b.personid and b.userid = ";
                    strSQL += searchUser.UserID.ToString() + ") ";
                }
			}
			strSQL += "Order by LastName";

			ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, strSQL);
			return ds;
		}


        //**********************************************************************************************************************
        public DataSet FCSearch(string noap)
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("Select '<a href=GIPAP.aspx?trgt=patientinfo&choice=' + Convert(varchar(10), PatientID) + '>' + PIN + '</a>' as '<strong>PIN</strong>', LastName as '<strong>Last Name</strong>', FirstName as '<strong>First Name</strong>', GIPAPStatus as '<strong>Status</strong>' from tblpatient where ");
            strSQL.Append("PIN like '" + this.PIN + "%' and ");
            strSQL.Append("FirstName like '" + this.FirstName + "%' and ");
            strSQL.Append("LastName like '" + this.LastName + "%' and ");
            strSQL.Append("Phone like '" + this.Phone + "%' and ");
            strSQL.Append("Fax like '" + this.Fax + "%' and ");
            strSQL.Append("noa = 1 and ");
            strSQL.Append("Email like '" + this.Email + "%' and ");
            strSQL.Append("mobile like '" + this.Mobile + "%' and ");
            strSQL.Append("gipapstatus like '" + this.GIPAPStatus + "%' and ");
            if (this.CountryID != 0)
            {
                strSQL.Append("CountryID = " + this.CountryID.ToString() + " and ");
            }
            if (noap != "")
            {
                strSQL.Append("patientid in (select patientid from tblnoafef where noapin like '" + noap + "%' ) and ");
            }
            strSQL.Append("City like '" + this.City + "%' ");
            strSQL.Append("Order by LastName");

            return SqlHelper.ExecuteDataset(connString, CommandType.Text, strSQL.ToString());
        }


		//**********************************************************************************************************************
		public string StatusInfoTable(int pid, string Urole)
		{
            this.InflateStatusInfo(pid);
            StringBuilder statInfo = new StringBuilder();
			statInfo.Append("<strong><font color=steelblue>" + this.GIPAPStatus + "</font></strong><br>");
            statInfo.Append("<font class='lbl'>Status Reason: </font>");
			if(this.StatusReason == "Approved with Partial Coverage")
			{
				statInfo.Append("<font color=red>" +  this.StatusReason + "</font>");
			}
			else
			{
				statInfo.Append(this.StatusReason);
			}
			if(Urole == "TMFUser" && this.GIPAPStatus !="Closed")
			{
				statInfo.Append(" <a href=UpdateStatusReason.aspx?choice=" + this.PatientID.ToString() + ">update</a>");
			}
			if(this.GIPAPStatus == "Active")
			{
                statInfo.Append("<br><br><font class='lbl'>Current Period</font><br>");
				if(this.StartDate.AddDays(120) < this.EndDate)
				{
					statInfo.Append("<strong><font color=blue>" + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " To ");
					statInfo.Append(this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "</font></strong>");
				}
				else
				{
					statInfo.Append("<strong><font color=steelblue>" + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + "</font> To ");
					statInfo.Append("<font color=steelblue>" + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "</font></strong>");
				}
                if (Urole == "TMFUser" || Urole == "MaxStation")
                {
                    statInfo.Append("<br><br><A href=javascript:openNewWindow('Patient/PrintForm.aspx?choice=" + this.PatientID.ToString() + "','thewin','height=400,width=400,toolbar=yes,scrollbars=yes')>Print Approval Slip</a>");
                }
                statInfo.Append("<br><font class='lbl'>Reminder Letter Date: </font>" + this.EndDate.AddDays(-29).Day.ToString() + " " + this.EndDate.AddDays(-29).ToString("y"));
                if (this.LalsDate != Convert.ToDateTime(null))
                {
                    statInfo.Append("<br><font class='lbl'>Last Approval Letter Sent: </font>" + this.LalsDate.Day.ToString() + " " + this.LalsDate.ToString("y"));
                }
                statInfo.Append("<br><br><font class='lbl'>Intake Date: </font>" + this.IntakeDate.Day.ToString() + " " + this.IntakeDate.ToString("y"));
                statInfo.Append("<br><font class='lbl'>Initial Approval Date: </font>" + this.IADate.Day.ToString() + " " + this.IADate.ToString("y"));
			}
			else if(this.GIPAPStatus == "Closed")
			{
                statInfo.Append("<br><br><font class='lbl'>Close Date: </font>" + this.ClosedDate.Day.ToString() + " " + this.ClosedDate.ToString("y"));
			}
			else if(this.GIPAPStatus == "Pending")
			{
                statInfo.Append("<br><br><font class='lbl'>Intake Date: </font>" + this.IntakeDate.Day.ToString() + " " + this.IntakeDate.ToString("y"));
			}
			if(Urole == "TMFUser" || Urole == "MaxStation")
			{
				if(this.ApplicantID != 0)
				{
                    statInfo.Append("<br><A href=javascript:openNewWindow('../application/printapplication.aspx?choice=" + this.ApplicantID.ToString() + "','thewin','height=500,width=400,toolbar=yes,scrollbars=yes')>View Application</a>");
				}
			}
			return statInfo.ToString();
		}
        //**********************************************************************************************************************
        private void InflateStatusInfo(int pid)
        {
            SqlParameter[] arrParams = new SqlParameter[1];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = pid;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientStatusProfile", arrParams);

            //0 PATIENT INFO
            this.PatientID = (int)(ds.Tables[0].Rows[0]["PatientID"]);
            this.IntakeDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IntakeDate"]);
            try
            {
                this.IADate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IADate"]);
            }
            catch { }
            this.GIPAPStatus = (ds.Tables[0].Rows[0]["GIPAPStatus"]).ToString();
            this.StatusReason = ds.Tables[0].Rows[0]["StatusReason"].ToString();
            try
            {
                this.ApplicantID = Convert.ToInt32(ds.Tables[0].Rows[0]["ApplicantID"]);
            }
            catch
            {
                this.ApplicantID = 0;
            }
            //1 gipap details
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.StatusReason = (ds.Tables[1].Rows[0]["StatusReason"]).ToString();
                try
                {
                    this.StartDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["StartDate"]);
                }
                catch { }
                try
                {
                    this.EndDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["EndDate"]);
                }
                catch { }
            }
            //2 extended date
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.EndDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["EndDate"]);
            }
            //3 close date
            if (ds.Tables[3].Rows.Count > 0)
            {
                this.ClosedDate = Convert.ToDateTime(ds.Tables[3].Rows[0]["ClosedDate"]);
                this.StatusReason = ds.Tables[3].Rows[0]["StatusReason"].ToString();
            }
            //4 deny date
            if (ds.Tables[4].Rows.Count > 0)
            {
                this.DeniedDate = Convert.ToDateTime(ds.Tables[4].Rows[0]["DeniedDate"]);
                if (this.DeniedDate > this.StartDate)
                {
                    this.StatusReason = ds.Tables[4].Rows[0]["StatusReason"].ToString();
                }
            }
            //5 lals date
            if (ds.Tables[5].Rows.Count > 0)
            {
                this.LalsDate = Convert.ToDateTime(ds.Tables[5].Rows[0]["CreateDate"]);
            }
            ds.Dispose();
        }
		//**********************************************************************************************************************
		public string StatusOptions(int pid, string Urole)
		{
			/*if(Urole != "TMFUser" && Urole != "MaxStation" && !Urole.StartsWith("FC"))
			{
				return "";
			}*/
            DataTable requestDt = this.InflateStatusOptions(pid);
            StringBuilder statOptions = new StringBuilder();
            bool showRequests = true;
			if(this.GIPAPStatus == "Active")
			{
                if (Urole == "TMFUser" || Urole == "MaxStation" || Urole.StartsWith("FC"))
                {
                    if (this.FlagNOA || (/*this.NOAPhys && */this.ReqTreatment == "Tasigna"))
                    {
                        //anyone can view, edit permissions are decided on fef page
                        statOptions.Append("<a href=noafef.aspx?choice=" + this.PatientID.ToString() + ">View NOA FEF</a><br><br>");
                        statOptions.Append("<a href=fcrequest.aspx?choice=" + this.PatientID.ToString() + "><font color=limegreen>Request FE Action</font></a><br><br>");
                        if (!this.VerificationComplete)
                        {
                            showRequests = false;
                        }
                    }
                    else
                    {
                        if (this.VerificationRed)
                        {
                            statOptions.Append("<a href=verification.aspx?choice=" + this.PatientID.ToString() + "><font color=red>Review Evaluation</font></a><br><br>");
                        }
                        else
                        {
                            statOptions.Append("<a href=verification.aspx?choice=" + this.PatientID.ToString() + "><font color=blue>Review Evaluation</font></a><br><br>");
                        }
                    }
                }
				if(Urole == "TMFUser" && this.ReapprovalRequestCount == 0)
				{
					statOptions.Append("<a href=Reapprove.aspx?choice=" + this.PatientID.ToString() + ">Re-Approve</a><br><br>");
				}
				if((Urole == "MaxStation" || Urole == "Physician"/*new*/) && this.EndDate <= DateTime.Now.AddDays(29) && this.ReapprovalRequestCount == 0)
				{
					statOptions.Append("<a href=Reapprove.aspx?choice=" + this.PatientID.ToString() + ">Re-Approve</a><br><br>");
				}
                if ((Urole == "MaxStation" || Urole == "TMFUser" || Urole == "Physician" /*new*/) && this.Treatment == "Glivec"  /*&& this.Glivec==false*/ && this.TasignaPhys > 0 && this.Diagnosis == "CML" && ReqTreatment == null)
                {
                    statOptions.Append("<a href=ChangeTreatment.aspx?choice=" + this.PatientID.ToString() + "><font color=#FADDFA>Change Treatment to Tasigna</font></a><br><br>");
                }
                if (!Urole.StartsWith("FC"))
                {
                    if (Urole == "TMFUser" || Urole == "MaxStation")
                    {
                        statOptions.Append("<a href=Extend.aspx?choice=" + this.PatientID.ToString() + ">Extend</a><br><br>");
                        statOptions.Append("<a href=Close.aspx?choice=" + this.PatientID.ToString() + ">Close</a><br><br>");
                        statOptions.Append("<a href=SAE.aspx?choice=" + this.PatientID.ToString() + ">Log An Adverse Event</a> | <a href=patientdatasets.aspx?choice=" + this.PatientID.ToString() + "&ds=sae><font color=gray>" + this.SAEcount.ToString() + " Logged</font></a>");
                    }
                    else if (Urole == "Physician"/*new*/)
                    {
                        statOptions.Append("<a href=Close.aspx?choice=" + this.PatientID.ToString() + ">Close</a><br><br>");
                    }
                }
			}
			else if(this.GIPAPStatus == "Pending")
			{
                if (Urole == "TMFUser" || Urole == "MaxStation" || Urole.StartsWith("FC") /*new*/)
                {
                    if (this.FlagNOA)
                    {
                        if (this.VerificationComplete)
                        {
                            statOptions.Append("<a href=NOAFEF.aspx?choice=" + this.PatientID.ToString() + ">View NOA FEF</a><br><br>");
                            if (Urole == "TMFUser")
                            {
                                statOptions.Append("<a href=Approve.aspx?choice=" + this.PatientID.ToString() + "&action=approve>Approve</a><br><br>");
                            }
                        }
                        else
                        {
                            statOptions.Append("<a href=NOAFEF.aspx?choice=" + this.PatientID.ToString() + ">View NOA FEF</a><br><br>");
                            showRequests = false;
                        }
                        statOptions.Append("<a href=FCRequest.aspx?choice=" + this.PatientID.ToString() + "><font color=limegreen>Request FE Action</font></a><br><br>");
                    }
                    else
                    {

                        if (this.VerificationID == 0)
                        {
                            statOptions.Append("<a href=Verification.aspx?choice=" + this.PatientID.ToString() + ">Medical and Financial Verification</a><br><br>");
                        }
                        else
                        {
                            if (this.VerificationComplete)
                            {
                                if (this.VerificationRed)
                                {
                                    statOptions.Append("<a href=verification.aspx?choice=" + this.PatientID.ToString() + "><font color=red>Review Evaluation</font></a><br><br>");
                                }
                                else
                                {
                                    statOptions.Append("<a href=verification.aspx?choice=" + this.PatientID.ToString() + "><font color=limegreen>Review Evaluation</font></a><br><br>");
                                }
                                if (Urole == "TMFUser")
                                {
                                    statOptions.Append("<a href=Approve.aspx?choice=" + this.PatientID.ToString() + ">Approve</a><br><br>");
                                }
                            }
                            else
                            {
                                statOptions.Append("<a href=Verification.aspx?choice=" + this.PatientID.ToString() + ">Complete Evaluation</a><br><br>");
                            }
                        }
                    }
                    if (Urole == "TMFUser")
                    {
                        statOptions.Append("<a href=Deny.aspx?choice=" + this.PatientID.ToString() + ">Deny</a><br>");
                    }
                }
			}
			else if(this.GIPAPStatus == "Denied")
			{
                if (Urole == "TMFUser" || Urole == "MaxStation" || Urole.StartsWith("FC") /*new*/)
                {
                    if (this.NOAPhys || this.FlagNOA || (this.TasignaPhys > 0 && this.Treatment == "Tasigna"))
                    {
                        if (this.FlagNOA && this.VerificationComplete)
                        {
                            statOptions.Append("<a href=NOAFEF.aspx?choice=" + this.PatientID.ToString() + ">View NOA FEF</a><br><br>");
                            if (!Urole.StartsWith("FC"))
                            {
                                if (this.Treatment == "Glivec" && this.TasignaPhys > 0 && this.Diagnosis == "CML" && ReqTreatment == null)
                                {
                                    statOptions.Append("<a href=Reassess.aspx?choice=" + this.PatientID.ToString() + ">Re-Assess with Glivec</a><br><br>");
                                    statOptions.Append("<a href=ChangeTreatment.aspx?choice=" + this.PatientID.ToString() + "&action=reassess><font color=#FADDFA>Re-Assess with Tasigna</font></a><br><br>");
                                }
                                else if (this.OtherRequestCount == 0) //make sure a request has not been entered
                                {
                                    statOptions.Append("<a href=Reassess.aspx?choice=" + this.PatientID.ToString() + ">Re-Assess</a><br><br>");
                                }
                            }
                        }
                        else
                        {
                            statOptions.Append("<a href=NOAFEF.aspx?choice=" + this.PatientID.ToString() + ">View NOA FEF</a><br><br>");
                            if (Urole == "MaxStation" && this.OtherRequestCount == 0) 
                            {
                                if (this.Treatment == "Glivec" && this.TasignaPhys > 0 && this.Diagnosis == "CML")
                                {
                                    statOptions.Append("<a href=Reassess.aspx?choice=" + this.PatientID.ToString() + ">Re-Assess with Glivec</a><br><br>");
                                    statOptions.Append("<a href=ChangeTreatment.aspx?choice=" + this.PatientID.ToString() + "&action=reassess><font color=#FADDFA>Re-Assess with Tasigna</font></a><br><br>");
                                }
                                else if (this.OtherRequestCount == 0) //make sure a request has not been entered
                                {
                                    statOptions.Append("<a href=Reassess.aspx?choice=" + this.PatientID.ToString() + ">Re-Assess</a><br><br>");
                                }
                            }
                            else if (Urole == "TMFUser")//po needs to be able to do this for some countries, a glivec reassess will go through automatically
                            {
                                if (this.Treatment == "Glivec" && this.TasignaPhys > 0 && this.Diagnosis == "CML")
                                {
                                    if (!this.NOAPhys) //allows for reassment with glivec into GIPAP for physicians who are only noa tasigna
                                    {
                                        statOptions.Append("<a href=Reassess.aspx?choice=" + this.PatientID.ToString() + ">Re-Assess with Glivec (GIPAP)</a><br><br>");
                                    }
                                    statOptions.Append("<a href=ChangeTreatment.aspx?choice=" + this.PatientID.ToString() + "&action=reassess><font color=#FADDFA>Re-Assess with Tasigna (NOA)</font></a><br><br>");
                                }
                                else if (!this.NOAPhys)
                                {
                                    statOptions.Append("<a href=Reassess.aspx?choice=" + this.PatientID.ToString() + ">Re-Assess</a><br><br>");
                                }
                            }
                            showRequests = false;
                        }
                        statOptions.Append("<a href=FCRequest.aspx?choice=" + this.PatientID.ToString() + "><font color=limegreen>Request FE Action</font></a><br><br>");
                    }
                    else
                    {
                        if (this.VerificationRed)
                        {
                            statOptions.Append("<a href=Verification.aspx?choice=" + this.PatientID.ToString() + "><font color=red>Review Evaluation</font></a><br><br>");
                        }
                        else
                        {
                            statOptions.Append("<a href=Verification.aspx?choice=" + this.PatientID.ToString() + "><font color=blue>Review Evaluation</font></a><br><br>");
                        }
                        statOptions.Append("<a href=Reassess.aspx?choice=" + this.PatientID.ToString() + ">Re-Assess</a><br><br>");
                    }
                }
			}
			else if(this.GIPAPStatus == "Closed")
			{
                if (this.NOAPhys || this.FlagNOA || (this.TasignaPhys > 0 /*not sure why this was here... && this.Treatment == "Tasigna"*/))
                {
                    if (Urole == "TMFUser" || Urole == "MaxStation" || Urole.StartsWith("FC") /*new*/)
                    {
                        statOptions.Append("<a href=NOAFEF.aspx?choice=" + this.PatientID.ToString() + ">View NOA FEF</a><br><br>");
                    }
                    if (this.FlagNOA && this.VerificationComplete)
                    {
                        if (!Urole.StartsWith("FC"))
                        {
                            if (this.Treatment == "Glivec" && this.TasignaPhys > 0 && this.Diagnosis == "CML" && OtherRequestCount==0)
                            {
                                statOptions.Append("<a href=Reactivate.aspx?choice=" + this.PatientID.ToString() + ">Re-Activate with Glivec</a><br><br>");
                                statOptions.Append("<a href=ChangeTreatment.aspx?choice=" + this.PatientID.ToString() + "&action=reactivate><font color=#FADDFA>Re-Activate with Tasigna</font></a><br><br>");
                            }
                            else if (this.OtherRequestCount == 0) //make sure a request has not been entered
                            {
                                statOptions.Append("<a href=Reactivate.aspx?choice=" + this.PatientID.ToString() + ">Re-Activate</a><br><br>");
                            }
                        }
                    }
                    else
                    {
                        if ((Urole == "MaxStation" || Urole == "Physician" || Urole == "TMFUser")/*new*/ && this.OtherRequestCount == 0)
                        {
                            if (this.Treatment == "Glivec" && this.TasignaPhys > 0 && this.Diagnosis == "CML")
                            {
                                statOptions.Append("<a href=Reactivate.aspx?choice=" + this.PatientID.ToString() + ">Re-Activate with Glivec</a><br><br>");
                                statOptions.Append("<a href=ChangeTreatment.aspx?choice=" + this.PatientID.ToString() + "&action=reactivate><font color=#FADDFA>Re-Activate with Tasigna</font></a><br><br>");
                            }
                            else
                            {
                                statOptions.Append("<a href=Reactivate.aspx?choice=" + this.PatientID.ToString() + ">Re-Activate</a><br><br>");
                            }
                        }
                        else if (Urole == "TMFUser") //po needs to be able to do this for some countries, allow to reactivate into GIPAP for glivec patients
                        {
                            if (this.Treatment == "Glivec" && this.TasignaPhys > 0 && this.Diagnosis == "CML" && OtherRequestCount ==0)
                            {
                                if (!this.NOAPhys) //only if physician is gipap glivec, noa tasigna. noa glivec needs a fef first
                                {
                                    statOptions.Append("<a href=Reactivate.aspx?choice=" + this.PatientID.ToString() + ">Re-Activate with Glivec (GIPAP)</a><br><br>");
                                }
                                statOptions.Append("<a href=ChangeTreatment.aspx?choice=" + this.PatientID.ToString() + "&action=reactivate><font color=#FADDFA>Re-Activate with Tasigna (NOA)</font></a><br><br>");
                            }
                            else if (!this.NOAPhys)
                            {
                                statOptions.Append("<a href=Reactivate.aspx?choice=" + this.PatientID.ToString() + ">Re-Activate</a><br><br>");
                            }
                        }
                        showRequests = false;
                    }
                    if (Urole != "Physician" /*new*/)
                    {
                        statOptions.Append("<a href=FCRequest.aspx?choice=" + this.PatientID.ToString() + "><font color=limegreen>Request FE Action</font></a><br><br>");
                    }
                }
                else
                {
                    if (Urole != "Physician" /*new*/)
                    {
                        if (this.VerificationRed)
                        {
                            statOptions.Append("<a href=Verification.aspx?choice=" + this.PatientID.ToString() + "><font color=red>Review Evaluation</font></a><br><br>");
                        }
                        else
                        {
                            statOptions.Append("<a href=Verification.aspx?choice=" + this.PatientID.ToString() + "><font color=blue>Review Evaluation</font></a><br><br>");
                        }
                    }
                    statOptions.Append("<a href=Reactivate.aspx?choice=" + this.PatientID.ToString() + ">Re-Activate</a><br><br>");
                }
                if (!Urole.StartsWith("FC") && Urole != "Physician" /*new*/)
                {
                    statOptions.Append("<a href=SAE.aspx?choice=" + this.PatientID.ToString() + ">Log An Adverse Event</a> | <a href=patientdatasets.aspx?choice=" + this.PatientID.ToString() + "&ds=sae><font color=gray>" + this.SAEcount.ToString() + " Logged</font></a>");
                }
			}
			if(Urole == "TMFUser" && showRequests)
			{
				if(this.ReapprovalRequestCount > 0)
				{
					statOptions.Append("<br><br><a href=Reapprove.aspx?choice=" + this.PatientID.ToString() + "><font color=red>" + this.ReapprovalRequestCount.ToString() + " ReApproval Requests</font></a>");
				}
				if(this.OtherRequestCount > 0)
				{
                    for (int i = 0; i < this.OtherRequestCount; i++)
                    {
                        //statOptions.Append("<br><br><a href=GIPAP.aspx?trgt=patientdatasets&choice=" + this.PatientID.ToString() + "&ds=requests><font color=red>" + this.OtherRequestCount.ToString() + " Requests</font></a>");
                        string typ = requestDt.Rows[i]["requesttype"].ToString();
                        if (typ == "ReAssess")
                        {
                            statOptions.Append("<br><br><a href=Reassess.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Reassess Request</font></a>");
                        }
                        else if (typ == "ChangeTreatment")
                        {
                            statOptions.Append("<br><br><a href=ApproveForTasigna.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Change Treatment Request</font></a>");
                        }
                        else if (typ == "DosageChange")
                        {
                            statOptions.Append("<br><br><a href=DoseChange.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Dosage Change Request</font></a>");
                        }
                        else if (typ == "Close")
                        {
                            statOptions.Append("<br><br><a href=Close.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Close Request</font></a>");
                        }
                        else if (typ == "Extend")
                        {
                            statOptions.Append("<br><br><a href=Extend.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Extend Request</font></a>");
                        }
                        else if (typ == "Reactivate")
                        {
                            statOptions.Append("<br><br><a href=Reactivate.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Reactivate Request</font></a>");
                        }
                        else if (typ == "FEF Update")
                        {
                            statOptions.Append("<br><br><a href=Verification.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>FEF Update</font></a>");
                        }
                        else if (typ == "Adverse Event")
                        {
                            statOptions.Append("<br><br><a href=SAE.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Adverse Event</font></a>");
                        }
                    }
				}
				if(this.FEFUpdateCount > 0)
				{
					statOptions.Append("<br><br><a href=patientdatasets.aspx?choice=" + this.PatientID.ToString() + "&ds=fefupdates><font color=red>" + this.FEFUpdateCount.ToString() + " FEF Updates</font></a>");
				}
            }
            else if(!Urole.StartsWith("FC") && Urole != "Physician" /*new*/)
            {
                if (this.ReapprovalRequestCount > 0)
                {
                    statOptions.Append("<br><br><font color=white>" + this.ReapprovalRequestCount.ToString() + " ReApproval Requests</font>");
                    if (Urole == "TMFUser" && this.Recommendation == "Deny")
                    {
                        statOptions.Append(" [<a href=RemoveReapprovalRequest.aspx?choice=" + this.PatientID.ToString() + ">remove</a> ]");
                    }
                }
                if (this.OtherRequestCount > 0)
                {
                    statOptions.Append("<br><br><font color=white>" + this.OtherRequestCount.ToString() + " Request(s)</font>");
                    if (Urole == "TMFUser")
                    {
                        for (int i = 0; i < this.OtherRequestCount; i++)
                        {
                            string typ = requestDt.Rows[i]["requesttype"].ToString();
                            if (typ == "DosageChange")
                            {
                                statOptions.Append("<br><a href=DoseChange.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Dosage Change Request</font></a>");
                            }
                            else if (typ == "Close")
                            {
                                statOptions.Append("<br><a href=Close.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Close Request</font></a>");
                            }
                            else if (typ == "Adverse Event")
                            {
                                statOptions.Append("<br><br><a href=SAE.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=red>Adverse Event</font></a>");
                            }/*
                            else if (typ == "Reactivate")
                            {
                                statOptions.Append("<br><a href=Reactivate.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=white>Reactivate</font></a>");
                            }
                            else if (typ == "Reassess")
                            {
                                statOptions.Append("<br><a href=Reassess.aspx?choice=" + this.PatientID + "&rid=" + requestDt.Rows[i]["requestid"].ToString() + "><font color=white>Reassess</font></a>");
                            }*/
                            else
                            {
                                statOptions.Append("<br><font color=white><i>" + requestDt.Rows[i]["requesttype"].ToString() + " </i>[ ");
                                statOptions.Append("<a href=RemoveRequest.aspx?rid=" + requestDt.Rows[i]["requestid"].ToString() + "&choice=" + this.PatientID.ToString() + ">remove</a> ]</font>");
                            }
                        }
                    }
                }
            }
            return statOptions.ToString();
		}


        //**********************************************************************************************************************
        private DataTable InflateStatusOptions(int pid)
        {
            SqlParameter[] arrParams = new SqlParameter[1];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = pid;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientStatusOptionsProfile", arrParams);

            //0 PATIENT INFO
            this.PatientID = (int)(ds.Tables[0].Rows[0]["PatientID"]);
            this.GIPAPStatus = (ds.Tables[0].Rows[0]["GIPAPStatus"]).ToString();
            this.IntakeDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IntakeDate"]);
            this.Diagnosis = ds.Tables[0].Rows[0]["diagnosis"].ToString();

            try
            {
                this.FinancialDeclarationDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FinancialDeclarationDate"].ToString());
            }
            catch
            {
                this.FinancialDeclarationDate = DateTime.Today.AddDays(1);
            }
            //NOA
            this.FlagNOA = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOA"]);
            this.Treatment = ds.Tables[0].Rows[0]["treatment"].ToString();
            this.Glivec = Convert.ToBoolean(ds.Tables[0].Rows[0]["Glivec"]);
            // 9 Treatment Change Request 
            if (ds.Tables[9].Rows.Count > 0)
            {
                this.ReqTreatment = ds.Tables[9].Rows[0]["treatment"].ToString();
            }
            //1 gipap details
            if (ds.Tables[1].Rows.Count > 0)
            {
                try
                {
                    this.EndDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["EndDate"]);
                }
                catch { }
            }
            //2 extended date
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.EndDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["EndDate"]);
            }
            //3 phys
            if (ds.Tables[3].Rows.Count > 0)
            {
                bool chkPhys = true;
                bool chkTas = true;
                for (int phyK = 0; phyK < ds.Tables[3].Rows.Count; phyK++)
                {
                    //flag PHYSICIAN NOA - checks if physician is noa
                    if (Convert.ToBoolean(ds.Tables[3].Rows[phyK]["noa"]) && chkPhys)
                    {
                        try
                        {
                            if (Convert.ToDateTime(ds.Tables[3].Rows[phyK]["noadate"]) <= DateTime.Now)
                            {
                                this.NOAPhys = true;
                                this.VerificationComplete = false;
                                chkPhys = false;
                            }
                            else
                            {
                                this.NOAPhys = false;
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        this.NOAPhys = false;
                        //independent of noa glivec noa this.TasignaPhys = 0;
                    }
                    //check if noa tasinga, independent of noa glivec
                    if (Convert.ToInt32(ds.Tables[3].Rows[phyK]["tasigna"]) > 0 && chkTas)
                    {
                        this.TasignaPhys = Convert.ToInt32(ds.Tables[3].Rows[phyK]["tasigna"]);
                        chkTas = false;
                    }
                    else
                    {
                        this.TasignaPhys = 0;
                    }
                }

            }
            //4 verification / NOA FEF
            if (this.FlagNOA || this.ReqTreatment == "Tasigna") 
            {
                if (ds.Tables[4].Rows.Count > 0 && this.FlagNOA)// if they don't have an fef, then complete = false, they are flagged noa when an fef is made
                {
                    this.NOAPIN = ds.Tables[4].Rows[0]["noapin"].ToString();
                    try
                    {
                        this.DonationLength = Convert.ToInt32(ds.Tables[4].Rows[0]["donationlength"]);
                    }
                    catch
                    {
                        this.DonationLength = -1;
                    }
                    this.PaymentOption = ds.Tables[4].Rows[0]["PaymentOption"].ToString();
                    this.MSContacted = Convert.ToBoolean(ds.Tables[4].Rows[0]["mscontacted"]);
                    try
                    {
                        this.YearlyReassess = Convert.ToBoolean(ds.Tables[4].Rows[0]["yearlyreassess"]);
                    }
                    catch { }
                    if (this.YearlyReassess && this.GIPAPStatus == "Active")
                    {
                        try
                        {
                            this.ReassessDate = Convert.ToDateTime(ds.Tables[4].Rows[0]["reassessdate"]);
                        }
                        catch { }
                    }
                    this.Recommendation = ds.Tables[4].Rows[0]["recommendation"].ToString();
                    //see if the verification is completed properly
                    if (this.NOAPIN != "" && this.Recommendation == "Approve" && this.DonationLength != -1 && (this.ReqTreatment == ds.Tables[4].Rows[0]["treatment"].ToString() || this.ReqTreatment==null))
                    {
                        /*if (this.DonationLength == 12 || this.DonationLength == 370 || this.DonationLength == 52)
                        {
                            this.VerificationComplete = true;
                        }
                        else */
                        if (this.MSContacted && this.PaymentOption != "")//full donation patients need this now
                        {
                            this.VerificationComplete = true;
                        }
                        else
                        {
                            this.VerificationComplete = false;
                        }

                    }
                    else
                    {
                        this.VerificationComplete = false;
                    }

                    //now make sure there is not a new FEF required
                    if (this.VerificationComplete)
                    {
                        //see if a new fef has been collected for re-assessment
                        if (this.GIPAPStatus == "Denied")
                        {
                            if (Convert.ToBoolean(ds.Tables[4].Rows[0]["denied"]))
                            {
                                this.VerificationComplete = false;
                            }
                        }
                        // see if you need a new fef to re-activate
                        else if (this.GIPAPStatus == "Closed")
                        {
                            try
                            {
                                //switching back to 1 month to allow requests at 1 month, goes into queues at 2 months still
                                if (Convert.ToDateTime(ds.Tables[4].Rows[0]["reassessdate"]) <= DateTime.Today.AddMonths(1) && this.YearlyReassess)
                                {
                                    this.VerificationComplete = false;
                                }
                            }
                            catch { }
                        }
                        // see if yearly re-assessment is needed
                        else if (this.GIPAPStatus == "Active")
                        {
                            if (this.YearlyReassess)
                            {
                                try//required for active patients with treatment change request
                                {
                                    //switching back to 1 month to allow requests at 1 month, goes into queues at 2 months still
                                    if (this.ReassessDate.AddMonths(-1) <= DateTime.Today)
                                    {
                                        this.VerificationComplete = false;
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
                else
                {
                    this.VerificationComplete = false;
                    //since we mark a patient as noa when they get a reactiv/reasesss/treatment req, the else/if below is not needed anymore
                }
            }
            /*else if (this.NOAPhys && this.Treatment == "Tasigna")//active patients with treatment change req
            {
                this.VerificationComplete = false;
            }*/
            else
            {
                if (ds.Tables[4].Rows.Count > 0)
                {
                    this.VerificationID = Convert.ToInt32(ds.Tables[4].Rows[0]["VerificationID"]);
                    try
                    {
                        if (Convert.ToInt32(ds.Tables[4].Rows[0]["FINANCIALAFFIDAVIT"]) == 1)
                        {
                            this.FinancialAffidavit = true;
                        }
                        else
                        {
                            if (this.FinancialDeclarationDate < this.IntakeDate)
                            {
                                this.FinancialAffidavit = false;
                            }
                            else
                            {
                                this.FinancialAffidavit = true;
                            }
                        }
                    }
                    catch
                    {
                        this.FinancialAffidavit = true;
                    }
                    if (ds.Tables[4].Rows[0]["RECOMMENDATION"].ToString() != "")
                    {
                        this.VerificationComplete = true;
                        this.VerificationRed = (Convert.ToBoolean(ds.Tables[4].Rows[0]["INSURANCE"]) || Convert.ToBoolean(ds.Tables[4].Rows[0]["COVERSRX"]) || Convert.ToBoolean(ds.Tables[4].Rows[0]["COVERSCANCERRX"]) || Convert.ToBoolean(ds.Tables[4].Rows[0]["COVERSGLIVECRX"]) || (ds.Tables[4].Rows[0]["RECOMMENDATION"].ToString() != "Approve") || !this.FinancialAffidavit);
                    }
                    else
                    {
                        this.VerificationComplete = false;
                    }
                }
                else
                {
                    this.VerificationID = 0;
                }
            }

            

            this.ReapprovalRequestCount = ds.Tables[5].Rows.Count;
            this.OtherRequestCount = ds.Tables[6].Rows.Count;
            DataTable returndt = ds.Tables[6];
            this.FEFUpdateCount = ds.Tables[7].Rows.Count;
            this.SAEcount = Convert.ToInt32(ds.Tables[8].Rows[0]["count"]);
            ds.Dispose();
            return returndt;
        }
		//**********************************************************************************************************************
		public string DiagnosisInfoTable(int pid,int rid)
		{
            this.InflateDiagnosisInfo(pid);
            StringBuilder diagInfo = new StringBuilder();
            if (this.Treatment == "Glivec")
            {
                diagInfo.Append("<b><font color=steelblue>Glivec</font></b>");
                diagInfo.Append("<br><font class='lbl'>Applied for GIPAP before: </font>");
                if (this.AppliedForGipap)
                {
                    diagInfo.Append("Yes<br>");
                }
                else
                {
                    diagInfo.Append("No<br>");
                }
                if (this.CountryID != 76 || (!this.FlagNOA && this.CountryID == 76)) //hide this for noa india
                {
                    diagInfo.Append("<font class='lbl'>GIPAP Patient Consent Form: </font>");
                    if (this.PatientConsent)
                    {
                        diagInfo.Append("Yes<br><br>");
                    }
                    else
                    {
                        diagInfo.Append("<font color=red>No</font><br><br>");
                    }
                }
            }
            if (rid == 0)
            {
                diagInfo.Append("<b><font class='lbl'>Diagnosis: </font>");
                if (this.Diagnosis == "GIST" && !this.GistApproved)
                {
                    diagInfo.Append("<font color=red>");
                }
                else if (this.Diagnosis == "Ph+ ALL" || this.Diagnosis == "DFSP")
                {
                    diagInfo.Append("<font color=red>");
                }
                else
                {
                    diagInfo.Append("<font color=steelblue>");
                }
                diagInfo.Append(this.Diagnosis + "</font></b><br>");
                diagInfo.Append("<font class='lbl'>Diagnosis Date: </font>" + this.DiagnosisDate.Day.ToString() + " " + this.DiagnosisDate.ToString("y") + "<br><br><hr>");

                if (this.Diagnosis == "CML")
                {
                    diagInfo.Append("<font class='lbl'>Philadelphia +: </font>");
                    if (this.PhilPos == 0)
                    {
                        if (this.BCR == 0 || this.BCR == 2)
                        {
                            diagInfo.Append("<font color=red>No</font>");
                        }
                        else
                        {
                            diagInfo.Append("No");
                        }
                    }
                    else if (this.PhilPos == 1)
                    {
                        diagInfo.Append("Yes");
                    }
                    else
                    {
                        if (this.BCR == 1)
                        {
                            diagInfo.Append("Don't Know");
                        }
                        else
                        {
                            diagInfo.Append("<font color=red>Don't Know</font>");
                        }
                    }
                    diagInfo.Append("<br><font class='lbl'>BCR-Abl +: </font>");
                    if (this.BCR == 0)
                    {
                        diagInfo.Append("No");
                    }
                    else if (this.BCR == 1)
                    {
                        diagInfo.Append("Yes");
                    }
                    else
                    {
                        diagInfo.Append("Don't Know");
                    }
                    diagInfo.Append("<br><font class='lbl'>Original Phase: </font>" + this.OriginalCMLPhase);
                    //if (this.Treatment == "Tasigna")
                    //{
                    //    if (this.Glivec || this.Imatinib || this.Dasatinib)//2nd line
                    //    {
                    //        if (this.CurrentCMLPhase == "Blast Crisis")
                    //        {
                    //            diagInfo.Append("<br><strong><font color=red>Current Phase: " + this.CurrentCMLPhase + "</font></strong>");
                    //        }
                    //        else
                    //        {
                    //            diagInfo.Append("<br><strong><font color=steelblue>Current Phase: " + this.CurrentCMLPhase + "</font></strong>");
                    //        }
                    //    }
                    //    else //1st line
                    //    {
                    //        if (this.CurrentCMLPhase == "Blast Crisis")
                    //        {
                    //            diagInfo.Append("<br><strong><font color=red>Current Phase: " + this.CurrentCMLPhase + "</font></strong>");
                    //        }
                    //        else
                    //        {
                    //            diagInfo.Append("<br><strong><font color=steelblue>Current Phase: " + this.CurrentCMLPhase + "</font></strong>");
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    diagInfo.Append("<br><strong><font color=steelblue>Current Phase: " + this.CurrentCMLPhase + "</font></strong>");
                    //}
                    if (this.NeedInterferonInfo)
                    {
                        diagInfo.Append("<br><br><font class='lbl'>Taken Interferon: </font>");
                        if (this.Interferon)
                        {
                            diagInfo.Append("Yes");
                        }
                        else
                        {
                            if (!this.HematologicFailure && !this.Intolerant && !this.CytogeneticFailure)
                            {
                                diagInfo.Append("<font color=red>No</font>");
                            }
                            else
                            {
                                diagInfo.Append("No");
                            }
                        }
                        if (this.IntakeDate > Convert.ToDateTime("4/13/2003"))
                        {
                            diagInfo.Append("<br><font class='lbl'>Interferon Time Length: </font>" + this.InterferonTimeLength);
                            diagInfo.Append("<br><br><font class='lbl'>Intolerant: </font>");
                            if (this.Intolerant)
                            {
                                diagInfo.Append("Yes");
                            }
                            else
                            {
                                diagInfo.Append("No");
                            }
                            diagInfo.Append("<br><font class='lbl'>Hematolgoic Failure: </font>");
                            if (this.HematologicFailure)
                            {
                                diagInfo.Append("Yes");
                            }
                            else
                            {
                                diagInfo.Append("No");
                            }
                            diagInfo.Append("<br><font class='lbl'>Cytogenetic Failure: </font>");
                            if (this.CytogeneticFailure)
                            {
                                diagInfo.Append("Yes");
                            }
                            else
                            {
                                diagInfo.Append("No");
                            }
                        }
                        else
                        {
                            diagInfo.Append("<br><br><font class='lbl'>Refractory: </font>");
                            if (this.Refractory)
                            {
                                diagInfo.Append("Yes");
                            }
                            else
                            {
                                diagInfo.Append("No");
                            }
                            diagInfo.Append("<br><font class='lbl'>Unresponsive: </font>");
                            if (this.Unresponsive)
                            {
                                diagInfo.Append("Yes");
                            }
                            else
                            {
                                diagInfo.Append("No");
                            }
                        }
                    }
                }
                else if (this.Diagnosis == "DFSP")
                {
                    diagInfo.Append("<font class='lbl'>Is the tumor unresectable, recurrent or metastatic?: </font>");
                    if (this.Recurrent)
                    {
                        diagInfo.Append("Yes");
                    }
                    else
                    {
                        diagInfo.Append("<font color=red>No</font>");
                    }
                }
                else if (this.Diagnosis == "MDS / MPD" || this.Diagnosis == "Systemic Mastocytosis" || this.Diagnosis == "HES / CEL")
                {
                    diagInfo.Append("<font class='lbl'>Medical Summary:</font><br>" + this.DiagSummary);
                }

                else if (this.Diagnosis == "Ph+ ALL")
                {
                    diagInfo.Append("<font class='lbl'>Philadelphia +: </font>");
                    if (this.PhilPos == 0)
                    {
                        if (this.BCR == 0 || this.BCR == 2)
                        {
                            diagInfo.Append("<font color=red>No</font>");
                        }
                        else
                        {
                            diagInfo.Append("No");
                        }
                    }
                    else if (this.PhilPos == 1)
                    {
                        diagInfo.Append("Yes");
                    }
                    else
                    {
                        if (this.BCR == 1)
                        {
                            diagInfo.Append("Don't Know");
                        }
                        else
                        {
                            diagInfo.Append("<font color=red>Don't Know</font>");
                        }
                    }
                    diagInfo.Append("<br><font class='lbl'>BCR-Abl +: </font>");
                    if (this.BCR == 0)
                    {
                        diagInfo.Append("No");
                    }
                    else if (this.BCR == 1)
                    {
                        diagInfo.Append("Yes");
                    }
                    else
                    {
                        diagInfo.Append("Don't Know");
                    }
                    if (this.RelapsedRefractory != -1)
                    {
                        diagInfo.Append("<br><font class='lbl'>Relapsed / Refractory: </font>");
                        if (this.RelapsedRefractory == 1)
                        {
                            diagInfo.Append("Yes");
                        }
                        else
                        {
                            diagInfo.Append("<font color=red>No</font>");
                        }
                    }
                    if (this.Chemo != -1)
                    {
                        diagInfo.Append("<br><font class='lbl'>Newly Diagnosed / Integrated with Chemotherapy: </font>");
                        if (this.Chemo == 1)
                        {
                            diagInfo.Append("Yes");
                        }
                        else
                        {
                            diagInfo.Append("<font color=red>No</font>");
                        }
                    }
                }
                else if (this.Diagnosis == "GIST")
                {
                    diagInfo.Append("<font class='lbl'>C-Kit +: </font>");
                    if (this.CKitPos == 0)
                    {
                        diagInfo.Append("<font color=red>No</font>");
                    }
                    else if (this.CKitPos == 1)
                    {
                        diagInfo.Append("Yes");
                    }
                    else
                    {
                        diagInfo.Append("<font color=red>Don't Know</font>");
                    }
                    diagInfo.Append("<br><font class='lbl'>Metastatic: </font>");
                    if (this.Metastatic == 0)
                    {
                        if (!this.Adjuvant && (this.Unresectable == 0 || this.Unresectable == 2))
                        {
                            diagInfo.Append("<font color=red>No</font>");
                        }
                        else
                        {
                            diagInfo.Append("No");
                        }
                    }
                    else if (this.Metastatic == 1)
                    {
                        if (this.Adjuvant)
                        {
                            diagInfo.Append("<font color=red>Yes</font>");
                        }
                        else
                        {
                            diagInfo.Append("Yes");
                        }
                    }
                    else
                    {
                        if (!this.Adjuvant && (this.Unresectable == 0 || this.Unresectable == 2))
                        {
                            diagInfo.Append("<font color=red>Don't Know</font>");
                        }
                        else if (this.Adjuvant)
                        {
                            diagInfo.Append("<font color=red>Don't Know</font>");
                        }
                        else
                        {
                            diagInfo.Append("Don't Know");
                        }
                    }
                    diagInfo.Append("<br><font class='lbl'>Unresectable: </font>");
                    if (this.Unresectable == 0)
                    {
                        if (!this.Adjuvant && (this.Metastatic == 0 || this.Metastatic == 2))
                        {
                            diagInfo.Append("<font color=red>No</font>");
                        }
                        else
                        {
                            diagInfo.Append("No");
                        }
                    }
                    else if (this.Unresectable == 1)
                    {
                        if (this.Adjuvant)
                        {
                            diagInfo.Append("<font color=red>Yes</font>");
                        }
                        else
                        {
                            diagInfo.Append("Yes");
                        }
                    }
                    else
                    {
                        if (!this.Adjuvant && (this.Metastatic == 0 || this.Metastatic == 2))
                        {
                            diagInfo.Append("<font color=red>Don't Know</font>");
                        }
                        else if (this.Adjuvant)
                        {
                            diagInfo.Append("<font color=red>Don't Know</font>");
                        }
                        else
                        {
                            diagInfo.Append("Don't Know");
                        }
                    }
                    if (this.Adjuvant)
                    {
                        diagInfo.Append("<br><br><font color=gray><b>Adjuvant Treatment</b></font><br>");
                        diagInfo.Append("<font class='lbl'>Intermediate / High Risk due to Mitotic Count / Tumor size: </font>");
                        if (this.HighRisk == 0)
                        {
                            diagInfo.Append("<font color=red>No</font>");
                        }
                        else if (this.HighRisk == 1)
                        {
                            diagInfo.Append("Yes");
                        }
                        else
                        {
                            diagInfo.Append("<font color=red>Don't Know</font>");
                        }
                    }
                }
            }
            if (this.ReasonChanged == "Change Treatment Request")
            {
                diagInfo.Append("<br><div style='width:100%; background-color:gainsboro; border:1px solid purple;'><b><font color=purple>Tasigna</font> ");
                if (this.Glivec || this.Imatinib || this.Dasatinib)//2nd line
                {
                    if (this.TasignaPhys > 0)
                    {
                        diagInfo.Append(" <font color=gray>2nd Line</font>");
                    }
                    else
                    {
                        diagInfo.Append(" <font color=red>2nd Line</font>");
                    }
                }
                else //1st line
                {
                    if (this.TasignaPhys == 1)
                    {
                        diagInfo.Append(" <font color=gray>1st Line</font>");
                    }
                    else
                    {
                        diagInfo.Append(" <font color=red>1st Line</font>");
                    }
                }
                if (this.CurrentCMLPhase == "Blast Crisis")
                {
                    diagInfo.Append("</b><br><font class='lbl'>Current Phase: </font><font color=red>" + this.CurrentCMLPhase + "</font>");
                }
                else
                {
                    diagInfo.Append("</b><br><font class='lbl'>Current Phase: </font>" + this.CurrentCMLPhase);
                }
                diagInfo.Append("</b><br><font class='lbl'>Taken Glivec?: </font>" + this.BoolAnswer(this.Glivec, (this.TasignaPhys != 1 && !this.Imatinib)));
                diagInfo.Append("<font class='lbl'>Taken Generic Imatinib?: </font>" + this.BoolAnswer(this.Imatinib, (this.TasignaPhys != 1 && !this.Glivec)));
                if (!this.GlivecIntolerant && !this.GlivecResistant && (this.Glivec || this.Imatinib))
                {
                    diagInfo.Append("<font class='lbl'>Glivec Intolerant?: </font>" + this.BoolAnswer(this.GlivecIntolerant, true));
                    diagInfo.Append("<font class='lbl'>Glivec Resistant?: </font>" + this.BoolAnswer(this.GlivecResistant, true));
                }
                else
                {
                    diagInfo.Append("<font class='lbl'>Glivec Intolerant?: </font>" + this.BoolAnswer(this.GlivecIntolerant, false));
                    diagInfo.Append("<font class='lbl'>Glivec Resistant?: </font>" + this.BoolAnswer(this.GlivecResistant, false));
                }
                diagInfo.Append("<br><font class='lbl'>Receiving Dasatinib?: </font>" + this.BoolAnswer(this.Dasatinib, false));
                if (this.Dasatinib)
                {
                    diagInfo.Append("<font class='lbl'>Dasatinib Intolerant?: </font>" + this.BoolAnswer(this.DasatinibIntolerant, false));
                    diagInfo.Append("<font class='lbl'>Dasatinib Resistant?: </font>" + this.BoolAnswer(this.DasatinibResistant, false));
                }
                diagInfo.Append("<br><br><font class='lbl'>Taken nilotinib/Tasigna?: </font>" + this.BoolAnswer(this.Tasigna, false));
                if (this.Tasigna)
                {
                    diagInfo.Append("<font class='lbl'>Start Date: </font>" + this.TasignaStartDate.Day.ToString() + " " + this.TasignaStartDate.ToString("y"));
                    diagInfo.Append("<br><font class='lbl'>Previous daily dose: </font>" + this.PrevTasignaDose + "<br>");
                }
                diagInfo.Append("<br><b>Original Requested NOA Tasigna dose: </b>" + this.mOriginalRequestedTasignaDose);
                diagInfo.Append("<br><br><font class='lbl'>Prev. Applied for NOA Tasigna?: </font>" + this.BoolAnswer(this.NOATasigna, false));
                diagInfo.Append("<font class='lbl'>Patient Consent Form signed?: </font>" + this.BoolAnswer(this.TasignaPatientConsent, true));
                diagInfo.Append("</div>");
            }

			return diagInfo.ToString();
        }
        //**********************************************************************************************************************
        private string BoolAnswer(bool answer, bool red)
        {
            if (answer)
            {
                return "Yes<br>";
            }
            else
            {
                if (red)
                {
                    return "<font color=red>No</font><br>";
                }
                else
                {
                    return "No<br>";
                }
            }
        }
        //**********************************************************************************************************************
        public void InflateDiagnosisInfo(int pid)
        {
            SqlParameter[] arrParams = new SqlParameter[1];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = pid;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientDiagnosisProfile", arrParams);

            //0 PATIENT INFO
            this.PatientID = (int)(ds.Tables[0].Rows[0]["PatientID"]);
            //info for edit patient page
            this.FirstName = (ds.Tables[0].Rows[0]["FirstName"]).ToString();
            this.LastName = (ds.Tables[0].Rows[0]["LastName"]).ToString();
            this.ThaiName = (ds.Tables[0].Rows[0]["ThaiName"]).ToString();
            this.PIN = (ds.Tables[0].Rows[0]["pin"]).ToString();
            this.Sex = (ds.Tables[0].Rows[0]["Sex"]).ToString();
            this.BirthDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"]);
            this.Street1 = (ds.Tables[0].Rows[0]["Street1"]).ToString();
            this.Street2 = (ds.Tables[0].Rows[0]["Street2"]).ToString();
            this.City = (ds.Tables[0].Rows[0]["City"]).ToString();
            this.StateProvince = (ds.Tables[0].Rows[0]["StateProvince"]).ToString();
            this.PostalCode = (ds.Tables[0].Rows[0]["PostalCode"]).ToString();
            this.Phone = (ds.Tables[0].Rows[0]["Phone"]).ToString();
            this.Fax = (ds.Tables[0].Rows[0]["Fax"]).ToString();
            this.Email = (ds.Tables[0].Rows[0]["Email"]).ToString();
            this.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
            this.CountryID = Convert.ToInt32(ds.Tables[0].Rows[0]["countryid"]);
            
            //rest
            this.IntakeDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IntakeDate"]);
            this.GIPAPStatus = (ds.Tables[0].Rows[0]["GIPAPStatus"]).ToString();
            this.BirthDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"]);
            this.FlagNOA = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOA"]);
            this.AppliedForGipap = Convert.ToBoolean(ds.Tables[0].Rows[0]["AppliedForGipap"]);
            this.PatientConsent = Convert.ToBoolean(ds.Tables[0].Rows[0]["patientconsent"]);
            this.AnnualIncome = (ds.Tables[0].Rows[0]["AnnualIncome"]).ToString();
            this.Occupation = (ds.Tables[0].Rows[0]["Occupation"]).ToString();
            this.Diagnosis = ds.Tables[0].Rows[0]["Diagnosis"].ToString();
            try
            {
                this.DiagnosisDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DiagnosisDate"]);
            }
            catch { }
            this.OriginalRequestedDosage = (ds.Tables[0].Rows[0]["OriginalRequestedDosage"]).ToString();
            this.OriginalApprovedDosage = (ds.Tables[0].Rows[0]["OriginalApprovedDosage"]).ToString();
            //current dosage is original for pending, gets updated if not
            this.CurrentDosage = (ds.Tables[0].Rows[0]["OriginalRequestedDosage"]).ToString();
            try
            {
                this.GlivecStartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["GlivecStartDate"]);
            }
            catch { }
            try
            {
                this.Glivec = Convert.ToBoolean(ds.Tables[0].Rows[0]["Glivec"]);
            }
            catch { }

            this.Treatment = (ds.Tables[0].Rows[0]["treatment"]).ToString();
            this.ReasonChanged = (ds.Tables[0].Rows[0]["reasonchanged"]).ToString();
            //1 country
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.NeedInterferonInfo = Convert.ToBoolean(ds.Tables[1].Rows[0]["NeedInterferonInfo"]);
                this.GistApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["GistApproved"]);
                this.PediatricAge = Convert.ToInt32(ds.Tables[1].Rows[0]["PediatricAge"]);
                if (this.PediatricAge == 0)
                {
                    this.PediatricAge = 16;
                }
                try
                {
                    this.adjCountry = (Convert.ToBoolean(ds.Tables[1].Rows[0]["adjgistapproved"]) && Convert.ToDateTime(ds.Tables[1].Rows[0]["adjgistdate"]) < DateTime.Today);
                }
                catch { this.adjCountry = false; }
            }
            //TASIGNA FIELDS - must be done before other dose values are set
            if (this.ReasonChanged == "Change Treatment Request")
            {
                this.Imatinib = (bool)(ds.Tables[6].Rows[0]["imATINIB"]);
                this.GlivecIntolerant = (bool)(ds.Tables[6].Rows[0]["GLIVECINTOLERANT"]);
                this.GlivecResistant = (bool)(ds.Tables[6].Rows[0]["GLIVECRESISTANT"]);
                this.Dasatinib = (bool)(ds.Tables[6].Rows[0]["DASATINIB"]);
                this.DasatinibIntolerant = (bool)(ds.Tables[6].Rows[0]["DASATINIBINTOLERANT"]);
                this.DasatinibResistant = (bool)(ds.Tables[6].Rows[0]["DASATINIBRESISTANT"]);
                this.Tasigna = (bool)(ds.Tables[6].Rows[0]["TASIGNA"]);
                try
                {
                    this.TasignaStartDate = Convert.ToDateTime(ds.Tables[6].Rows[0]["TASIGNASTARTDATE"]);
                }
                catch { }
                this.PrevTasignaDose = (ds.Tables[6].Rows[0]["PREVTASIGNADOSE"]).ToString();
                this.TasignaPatientConsent = Convert.ToBoolean(ds.Tables[6].Rows[0]["patientconsent"]);
                this.NOATasigna = (bool)(ds.Tables[6].Rows[0]["NOATASIGNA"]);
                this.mOriginalRequestedTasignaDose = ds.Tables[6].Rows[0]["OriginalRequestedDosage"].ToString();
                this.CurrentCMLPhase = Convert.ToString(ds.Tables[6].Rows[0]["CurrentCMLPhase"]);

                //check physician status
                this.TasignaPhys = 0;
                if (ds.Tables[7].Rows.Count > 0)
                {
                    if (Convert.ToBoolean(ds.Tables[7].Rows[0]["noa"]) && Convert.ToDateTime(ds.Tables[7].Rows[0]["noadate"]) <= DateTime.Today)
                    {
                        this.TasignaPhys = Convert.ToInt32(ds.Tables[7].Rows[0]["tasigna"]);
                    }
                }
            }
            //2 current detail
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.CurrentDosage = ds.Tables[2].Rows[0]["currentdosage"].ToString();
                this.StartDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["startdate"].ToString());
                this.mPickedUp = Convert.ToBoolean(ds.Tables[2].Rows[0]["pickedup"].ToString());
                this.mTabletStrength = ds.Tables[2].Rows[0]["tabletstrength"].ToString();
                this.Expired = Convert.ToBoolean(ds.Tables[2].Rows[0]["expired"].ToString());
            }
            //3 dosage change
            if (ds.Tables[3].Rows.Count > 0)
            {
                this.CurrentDosage = ds.Tables[3].Rows[0]["currentdosage"].ToString();
                this.mTabletStrength = "";
                this.Expired = Convert.ToBoolean(ds.Tables[3].Rows[0]["expired"].ToString());
            }
            if (this.Diagnosis == "CML")
            {
                this.PhilPos = Convert.ToInt32(ds.Tables[4].Rows[0]["PhilPos"]);
                this.BCR = Convert.ToInt32(ds.Tables[4].Rows[0]["BCR"]);
                this.OriginalCMLPhase = Convert.ToString(ds.Tables[4].Rows[0]["OriginalCMLPhase"]);
                //this is for tasigna only now 
                if (this.Treatment == "Glivec")
                {
                    this.CurrentCMLPhase = Convert.ToString(ds.Tables[4].Rows[0]["CurrentCMLPhase"]);
                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    this.Interferon = Convert.ToBoolean(ds.Tables[5].Rows[0]["Interferon"]);
                    //old
                    try
                    {
                        this.Refractory = Convert.ToBoolean(ds.Tables[5].Rows[0]["Refractory"]);
                        this.Unresponsive = Convert.ToBoolean(ds.Tables[5].Rows[0]["Unresponsive"]);
                    }
                    catch { }
                    //new
                    this.InterferonTimeLength = Convert.ToString(ds.Tables[5].Rows[0]["InterferonTimeLength"]);
                    try
                    {
                        this.Intolerant = Convert.ToBoolean(ds.Tables[5].Rows[0]["Intolerant"]);
                    }
                    catch { this.Intolerant = false; }
                    try
                    {
                        this.HematologicFailure = Convert.ToBoolean(ds.Tables[5].Rows[0]["HematologicFailure"]);
                    }
                    catch { this.HematologicFailure = false; }
                    try
                    {
                        this.CytogeneticFailure = Convert.ToBoolean(ds.Tables[5].Rows[0]["CytogeneticFailure"]);
                    }
                    catch { this.CytogeneticFailure = false; }
                }
                

            }
            else if (this.Diagnosis == "Ph+ ALL")
            {
                this.PhilPos = Convert.ToInt32(ds.Tables[4].Rows[0]["PhilPos"]);
                this.BCR = Convert.ToInt32(ds.Tables[4].Rows[0]["BCR"]);
                this.RelapsedRefractory = Convert.ToInt32(ds.Tables[4].Rows[0]["RelapsedRefractory"]);
                this.Chemo = Convert.ToInt32(ds.Tables[4].Rows[0]["Chemo"]);
                this.PediatricApproved = false;
            }
            else if (this.Diagnosis == "DFSP")
            {
                this.Recurrent = Convert.ToBoolean(ds.Tables[4].Rows[0]["recurrent"]);
            }
            else if (this.Diagnosis == "GIST")
            {
                this.CKitPos = Convert.ToInt32(ds.Tables[4].Rows[0]["CKitPositive"]);
                this.Unresectable = Convert.ToInt32(ds.Tables[4].Rows[0]["Unresectable"]);
                this.Metastatic = Convert.ToInt32(ds.Tables[4].Rows[0]["Metastatic"]);
                this.HighRisk = Convert.ToInt32(ds.Tables[4].Rows[0]["HighRisk"]);
                this.Adjuvant = Convert.ToBoolean(ds.Tables[4].Rows[0]["adjuvant"]);
            }
            /*else if (this.Diagnosis == "Adjuvant GIST")
            {
                this.HighRisk = Convert.ToInt32(ds.Tables[4].Rows[0]["HighRisk"]);
                this.CKitPos = Convert.ToInt32(ds.Tables[4].Rows[0]["CKitPositive"]);
                this.Unresectable = Convert.ToInt32(ds.Tables[4].Rows[0]["Unresectable"]);
                this.Metastatic = Convert.ToInt32(ds.Tables[4].Rows[0]["Metastatic"]);
            }*/
            else if (this.Diagnosis == "MDS / MPD" || this.Diagnosis == "Systemic Mastocytosis" || this.Diagnosis == "HES / CEL")
            {
                this.DiagSummary = ds.Tables[4].Rows[0]["summary"].ToString();
            }
            ds.Dispose();
        }
		//**********************************************************************************************************************
		public string FinancialInfoTable(string Urole)
		{
            StringBuilder finInfo = new StringBuilder();
			if(this.GIPAPStatus == "Pending" || this.GIPAPStatus == "Denied")
			{
                finInfo.Append("<font class='lbl'>Original Requested Dosage: </font>");
				if(this.Diagnosis == "GIST")
				{
                    if (this.Adjuvant)
                    {
                        if (this.OriginalRequestedDosage == "300mg")
                        {
                            finInfo.Append("<font color=red>" + this.OriginalRequestedDosage + "</font>");
                        }
                        else
                        {
                            finInfo.Append(this.OriginalRequestedDosage);
                        }
                    }
                    else
                    {
                        
                        if (this.OriginalRequestedDosage == "400mg")
                        {
                            finInfo.Append(this.OriginalRequestedDosage);
                        }
                        else if ((this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today) && (this.OriginalRequestedDosage == "200mg" || this.OriginalRequestedDosage == "260mg" || this.OriginalRequestedDosage == "300mg"))
                        {
                            finInfo.Append(this.OriginalRequestedDosage);
                        }
                        else
                        {
                            finInfo.Append("<font color=red>" + this.OriginalRequestedDosage + "</font>");
                        }
                    }
				}
                else if (this.Diagnosis == "Ph+ ALL" || this.Diagnosis == "MDS / MPD" || this.Diagnosis == "Systemic Mastocytosis" || this.Diagnosis == "HES / CEL" || this.Diagnosis == "Adjuvant GIST")
                {
                    if (this.OriginalRequestedDosage == "400mg" && this.Glivec == false && this.GIPAPStatus == "Pending" && this.BirthDate.AddYears(this.PediatricAge) < DateTime.Today && this.Diagnosis == "Ph+ ALL")
                    {
                        finInfo.Append("<font color=red>" + this.OriginalRequestedDosage + "</font></strong>");
                    }
                    else
                    finInfo.Append(this.OriginalRequestedDosage);
                }
				else if(this.Diagnosis == "DFSP")
				{
					if(!this.Glivec && this.OriginalRequestedDosage != "800mg")
					{
						finInfo.Append("<font color=red>" + this.OriginalRequestedDosage + "</font>");
					}
					else
					{
						finInfo.Append(this.OriginalRequestedDosage);
					}
				}
				else if(this.Diagnosis == "CML")
				{
					if(this.CurrentCMLPhase == "Blast Crisis" && this.OriginalRequestedDosage == "600mg")
					{
						finInfo.Append(this.OriginalRequestedDosage);
					}
					else if(this.CurrentCMLPhase == "Accelerated" && this.OriginalRequestedDosage == "600mg")
					{
						finInfo.Append(this.OriginalRequestedDosage);
					}
					else if(this.CurrentCMLPhase == "Chronic" && this.OriginalRequestedDosage == "400mg")
					{
						finInfo.Append(this.OriginalRequestedDosage);
					}
					else if(this.CurrentCMLPhase == "Remission" && this.OriginalRequestedDosage == "400mg")
					{
						finInfo.Append(this.OriginalRequestedDosage);
					}
					else if((this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today) && (this.OriginalRequestedDosage == "200mg" || this.OriginalRequestedDosage == "260mg" || this.OriginalRequestedDosage == "300mg"))
					{
						finInfo.Append(this.OriginalRequestedDosage);
					}
                    else if (this.Treatment == "Tasigna")
                    {
                        if (this.Glivec || this.Imatinib || this.Dasatinib)//2nd line should be 400
                        {
                            if (this.OriginalRequestedDosage == "300mg BID")
                            {
                                finInfo.Append("<font color=red>" + this.OriginalRequestedDosage + "</font>");
                            }
                            else
                            {
                                finInfo.Append(this.OriginalRequestedDosage);
                            }
                        }
                        else //1st line... should be 300
                        {
                            if (this.OriginalRequestedDosage == "300mg BID")
                            {
                                finInfo.Append(this.OriginalRequestedDosage);
                            }
                            else
                            {
                                finInfo.Append("<font color=red>" + this.OriginalRequestedDosage + "</font>");
                            }
                        }
                    }
                    else
                    {
                        finInfo.Append("<font color=red>" + this.OriginalRequestedDosage + "</font>");
                    }
				}
			}			
			else if(this.GIPAPStatus == "Active" || this.GIPAPStatus == "Closed")
			{
                finInfo.Append("<font class='lbl'>Original Requested Dosage: </font>" + this.OriginalRequestedDosage);
                finInfo.Append("<br><font class='lbl'>Original Approved Dosage: </font>" + this.OriginalApprovedDosage);
				finInfo.Append("<br><strong><font color=steelblue>Current Dosage: </font>");
				if(this.Diagnosis == "GIST")
				{
                    if (this.Adjuvant)
                    {
                        if (this.CurrentDosage == "300mg")
                        {
                            finInfo.Append("<font color=red>" + this.CurrentDosage + "</font></strong>");
                        }
                        else
                        {
                            finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                        }
                    }
                    else
                    {
                        if (this.CurrentDosage == "400mg")
                        {
                            finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                        }
                        else if ((this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today) && (this.CurrentDosage == "200mg" || this.CurrentDosage == "260mg" || this.CurrentDosage == "300mg") && !this.Adjuvant)
                        {
                            finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                        }
                        else
                        {
                            finInfo.Append("<font color=red>" + this.CurrentDosage + "</font></strong>");
                        }
                    }
				}
                else if (this.Diagnosis == "Ph+ ALL")
                {   
                    if ((this.BirthDate.AddYears(this.PediatricAge) < DateTime.Today) && (this.CurrentDosage == "260mg" || this.CurrentDosage == "300mg"))
                    {
                        finInfo.Append("<font color=red>" + this.CurrentDosage + "</font></strong>");
                    }
                    else
                    {
                        finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                    }
                }
                else if (this.Diagnosis == "MDS / MPD" || this.Diagnosis == "Systemic Mastocytosis" || this.Diagnosis == "HES / CEL" || this.Diagnosis == "Adjuvant GIST")
                {
                    finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                }
                else if (this.Diagnosis == "DFSP")
                {
                    if (!this.Glivec && this.CurrentDosage != "800mg")
                    {
                        finInfo.Append("<font color=red>" + this.CurrentDosage + "</font></strong>");
                    }
                    else
                    {
                        finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                    }
                }
                else if (this.Diagnosis == "CML")
                {
                    if (this.CurrentCMLPhase == "Blast Crisis" && this.CurrentDosage == "600mg")
                    {
                        finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                    }
                    else if (this.CurrentCMLPhase == "Accelerated" && this.CurrentDosage == "600mg")
                    {
                        finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                    }
                    else if (this.CurrentCMLPhase == "Chronic" && this.CurrentDosage == "400mg")
                    {
                        finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                    }
                    else if (this.CurrentCMLPhase == "Remission" && this.CurrentDosage == "400mg")
                    {
                        finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                    }
                    else if ((this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today) && (this.CurrentDosage == "200mg" || this.CurrentDosage == "260mg" || this.CurrentDosage == "300mg"))
                    {
                        finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                    }
                    else if (this.Treatment == "Tasigna")
                    {
                        if (this.Glivec || this.Imatinib || this.Dasatinib)//2nd line should be 400
                        {
                            if (this.CurrentDosage == "300mg BID")
                            {
                                finInfo.Append("<font color=red>" + this.CurrentDosage + "</font></strong>");
                            }
                            else
                            {
                                finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                            }
                        }
                        else //1st line... should be 300
                        {
                            if (this.CurrentDosage == "300mg BID")
                            {
                                finInfo.Append("<font color=steelblue>" + this.CurrentDosage + "</font></strong>");
                            }
                            else
                            {
                                finInfo.Append("<font color=red>" + this.CurrentDosage + "</font></strong>");
                            }
                        }
                    }
                    else
                    {
                        finInfo.Append("<font color=red>" + this.CurrentDosage + "</font></strong>");
                    }
                }
                
                if (this.GIPAPStatus == "Active")
                {
                    //india
                    if (this.CountryID == 76 && Urole == "TMFUser")
                    {
                        if (this.mTabletStrength != "")
                        {
                            finInfo.Append("<br><font color=gray>" + this.mTabletStrength + "</font>");

                            if (this.CurrentDosage == "400mg" || this.CurrentDosage == "600mg" || this.CurrentDosage == "800mg")
                            {
                                if (!this.mPickedUp)
                                {
                                    //CHANGE THIS PART IN YOGESH'S CODE
                                    /*if (this.StartDate.AddDays(10) > DateTime.Today && this.mTabletStrength.IndexOf("400mg") != -1)
                                    {*/
                                        finInfo.Append("<br><br>[<a href=UpdateTabletStrength.aspx?choice=" + this.PatientID + "><font color=blue>Change Tablet Strength</font></a>]");
                                    //}
                                }
                            }
                        }
                        //check if expired
                        if (this.Expired)
                        {
                            finInfo.Append("<br><br><font color=gray>Order Expired [<a href=Unexpire.aspx?choice=" + this.PatientID.ToString() + "><font color=blue>Un-expire</font></a>]");
                        }
                    }
                    if (this.Diagnosis != "MDS / MPD")
                    {
                        if (Urole == "TMFUser" || Urole == "MaxStation")
                        {
                            /*AVB - added this to avoid changing the dosage when a user
                             * has a ChangeTreatmentRequest pending for Tasigna
                             * */
                            if (this.Treatment == "Tasigna" && ((this.CurrentDosage != "400mg BID") || (this.CurrentDosage != "400mg QD") ||(this.CurrentDosage != "300mg BID")))
                            { }
                            else
                                finInfo.Append("<br><br>[<a href=DoseChange.aspx?choice=" + this.PatientID.ToString() + "><font color=blue>Change Dosage</font></a>]");
                        }
                    }
                }
			}
            if (this.Treatment == "Glivec")
            {
                if (this.GlivecStartDate != Convert.ToDateTime("1/1/1900") && this.GlivecStartDate != Convert.ToDateTime("1/1/0001") && this.GlivecStartDate != Convert.ToDateTime("1/1/1901"))
                {
                    finInfo.Append("<br><br><font class='lbl'>Glivec Start Date: </font>" + this.GlivecStartDate.Day.ToString() + " " + this.GlivecStartDate.ToString("y"));
                }
                try
                {
                    finInfo.Append("<br><font class='lbl'>Previously Taken Glivec®/Imatinib: </font>");
                    if (this.Glivec)
                    {
                        finInfo.Append("Yes");
                    }
                    else
                    {
                        finInfo.Append("No");
                    }
                }
                catch { }
            }
            finInfo.Append("<br><br><hr><font class='lbl'>Annual Income: </font>" + this.AnnualIncome + "<br>");
            finInfo.Append("<font class='lbl'>Occupation: </font>" + this.Occupation);
			
			return finInfo.ToString();
		}
		//**********************************************************************************************************************
		public string PatientInfo(int pid, GIPAP_Objects.User sUse)
		{
            this.InflatePatientInfo(pid);
            StringBuilder patInfo = new StringBuilder();
            string headColor;
            if (this.FlagNOA)
            {
                headColor = "green";
            }
            else if (this.CurrentProgram == "MYPAP")
            {
                headColor = "darkorange";
            }
            else if (this.CurrentProgram == "TIPAP")
            {
                headColor = "purple";
            }
            else
            {
                headColor = "steelblue";
            }
		    patInfo.Append("<h1><font color=" + headColor + ">" + this.FirstName + " " + this.LastName + "<br>" + this.PIN + "</font></h1>");
            if (sUse.Role == "TMFUser" || sUse.Role == "MaxStation")
            {
                patInfo.Append("<div class='LeftColSpacer'><a href=PatientInfo.aspx?choice=" + this.PatientID.ToString() + ">PATS</a>");
                patInfo.Append("  <a href=../GIPAPTrusted.aspx?pin=" + this.PIN + "&reqform=Patient><font color=deeppink>PINC</a></font>");
                if (this.CurrentProgram == "MYPAP")
                {
                    patInfo.Append("  <a href=/MYPAP/gipaptrusted.aspx?pin=" + this.PIN + "&reqform=MyPapPatient&user=" + sUse.Username + "><font color=darkorange>MYPAP</font></a>");
                }
                else if (this.CurrentProgram == "TIPAP")
                {
                    patInfo.Append("  <a href=/TIPAP/gipaptrusted.aspx?pin=" + this.PIN + "&reqform=TiPapPatient&user=" + sUse.Username + "><font color=purple>TIPAP</font></a>");
                }
                patInfo.Append("</div>");
            }
            else // not possible yet
            {
                patInfo.Append("<div class='LeftColSpacer'></div>");
            }
            if (this.FlagNOA && this.VerificationComplete) //complete here means only that an fef has been started
            {
                if (this.DonationLength == 12 || this.DonationLength == -1 || this.DonationLength == 370 || this.DonationLength == 52)
                {
                    patInfo.Append("<div class='NOAGIPAPBox'><STRONG><FONT color=maroon>" + this.CurrentProgram + "</STRONG><BR>");
                }
                else
                {
                    patInfo.Append("<div class='NOABox'><STRONG><FONT color=green>" + this.CurrentProgram + "</STRONG><BR>");
                }
                if (this.NOAPIN == "Fixed")
                {
                    patInfo.Append(this.NOAPIN + "<br>");
                }
                if (this.DonationLength != -1)
                {
                    int k;
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
                    patInfo.Append("<font color=gray><b>" + k.ToString() + " + " + this.DonationLength.ToString() + " (" + this.DonationLengthUnit + ")</b>");
                    if (this.PaymentOption != "")
                    {
                        patInfo.Append(" | " + this.PaymentOption);
                    }
                    patInfo.Append("</font>");
                }
                if (this.GIPAPStatus == "Active" || this.GIPAPStatus == "Closed")
                {
                    if (this.YearlyReassess)
                    {
                        if (this.ReassessDate != Convert.ToDateTime("1/1/0001"))
                        {
                            patInfo.Append("<br><br>Re-assessment: <b>");
                            patInfo.Append(this.ReassessDate.Day.ToString() + " " + this.ReassessDate.ToString("y") + "</b>");
                        }
                    }
                    else
                    {
                        patInfo.Append("<br><br><font color=gray><i>No re-assessment required</i></font>");
                    }
                }
                if (this.Recommendation != "")
                {
                    patInfo.Append("<br><br><b>Recommendation: </b>");
                    if (this.Recommendation == "Pending")
                    {
                        patInfo.Append("<font color=gray>" + this.Recommendation + "</font>");
                    }
                    else if (this.Recommendation == "Deny")
                    {
                        patInfo.Append("<font color=red><b>" + this.Recommendation + "</b></font>");
                    }
                    else
                    {
                        patInfo.Append(this.Recommendation);
                        /*if (this.DonationLength != 12 && this.DonationLength != 370 && this.DonationLength != 52)
                        { this needs to be done for full donation patients now too*/
                            if (this.MSContacted)
                            {
                                patInfo.Append("<br><font color=gray>Patient has been contacted</font>");
                            }
                            else
                            {
                                patInfo.Append("<br><font color=red><i>Patient has NOT been contacted</i></font>");
                            }
                       // }
                    }
                }
                patInfo.Append("</font></div><div class='LeftColSpacer'></div>");
            } 
			return patInfo.ToString();
		}
		//**********************************************************************************************************************
        public string AddressInfo(string Urole)
		{
            StringBuilder patInfo = new StringBuilder();
            if (this.CountryName=="Thailand")
            patInfo.Append("<font class='lbl'>Thai Name: </font>" + this.ThaiName + "<br>");
            patInfo.Append("<font class='lbl'>Sex: </font>" + this.Sex + "<br>");
            patInfo.Append("<font class='lbl'>Birth Date: </font>");
            if ((!this.PediatricApproved) && (this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today))
            {
                patInfo.Append("<font color=red>" + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y") + "</font>");
            }
            else if ((this.PediatricApproved) && (this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today))
            {
                if (this.BirthDate.AddYears(2) > DateTime.Today)
                {
                    patInfo.Append("<font color=red>" + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y") + "</font><br><font color=salmon>Under 2 years of age</font>");
                }
                else
                {
                    patInfo.Append("<font color=green>" + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y") + "</font>");
                }
            }
            else
            {
                patInfo.Append(this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y"));
            }
            patInfo.Append("<br><font class='lbl'>Address: </font>" + this.Street1 + "<br>" + this.Street2 + "<br><font class='lbl'>City: </font>" + this.City + "<br><font class='lbl'>State / Province: </font>" + this.StateProvince + " " + this.PostalCode);
            
            if (Urole == "TMFUser" || Urole == "MaxStation")
            {
                patInfo.Append("<br><font class='lbl'>Country: </font><a href=../Country/CountryInfo.aspx?choice=" + this.CountryID.ToString() + ">" + this.CountryName + "</a>");
            }
            else
            {
                patInfo.Append("<br><font class='lbl'>Country: </font>" + this.CountryName);
            }
            patInfo.Append("<br><br><font class='lbl'>Email: </font>" + this.Email + "<br><font class='lbl'>(tel)</font>" + this.Phone + "<br><font class='lbl'>(fax)</font>" + this.Fax + "<br><font class='lbl'>(mobile)</font>" + this.Mobile);
            return patInfo.ToString();
		}
        //**********************************************************************************************************************
        private void InflatePatientInfo(int pid)
        {
            SqlParameter[] arrParams = new SqlParameter[1];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = pid;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientInfoProfile", arrParams);

            //0 PATIENT INFO
            this.FlagNOA = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOA"]);
            this.PatientID = (int)(ds.Tables[0].Rows[0]["PatientID"]);
            this.PIN = (ds.Tables[0].Rows[0]["PIN"]).ToString();
            this.CurrentProgram = ds.Tables[0].Rows[0]["currentprogram"].ToString();
            this.FirstName = (ds.Tables[0].Rows[0]["FirstName"]).ToString();
            this.LastName = (ds.Tables[0].Rows[0]["LastName"]).ToString();
            this.ThaiName = (ds.Tables[0].Rows[0]["ThaiName"]).ToString();
            this.Sex = (ds.Tables[0].Rows[0]["Sex"]).ToString();
            this.BirthDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"]);
            this.Street1 = (ds.Tables[0].Rows[0]["Street1"]).ToString();
            this.Street2 = (ds.Tables[0].Rows[0]["Street2"]).ToString();
            this.City = (ds.Tables[0].Rows[0]["City"]).ToString();
            this.StateProvince = (ds.Tables[0].Rows[0]["StateProvince"]).ToString();
            this.PostalCode = (ds.Tables[0].Rows[0]["PostalCode"]).ToString();
            this.Phone = (ds.Tables[0].Rows[0]["Phone"]).ToString();
            this.Fax = (ds.Tables[0].Rows[0]["Fax"]).ToString();
            this.Email = (ds.Tables[0].Rows[0]["Email"]).ToString();
            this.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
            this.Diagnosis = ds.Tables[0].Rows[0]["Diagnosis"].ToString();
            this.GIPAPStatus = (ds.Tables[0].Rows[0]["GIPAPStatus"]).ToString();
            this.Treatment = (ds.Tables[0].Rows[0]["treatment"]).ToString();
            //1 country
            this.PediatricAge = Convert.ToInt32(ds.Tables[1].Rows[0]["PediatricAge"]);
            if (this.PediatricAge == 0)
            {
                this.PediatricAge = 16;
            }
            if (this.Treatment == "Tasigna")
            {
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["tasignaPedApproved"]);
            }
            else if (this.Diagnosis == "CML")
            {
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["PediatricApproved"]);
            }
            else if (this.Diagnosis == "Ph+ ALL")
            {
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["phallPedApproved"]);
            }
            else if (this.Diagnosis == "DFSP")
            {
                //coutry
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["dfspPedApproved"]);
            }
            else if (this.Diagnosis == "GIST")
            {
                //country
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["gistPedApproved"]);
            }
            this.CountryName = ds.Tables[1].Rows[0]["countryname"].ToString();
            this.CountryID = Convert.ToInt32(ds.Tables[1].Rows[0]["countryid"]);
            //2 NOA FEF
            if (this.FlagNOA)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    this.VerificationComplete = true; //in this instance complete only means that a fef has been started.  this is because patients are now marked noa after a request comes in
                    this.NOAPIN = ds.Tables[2].Rows[0]["noapin"].ToString();
                    try
                    {
                        this.DonationLength = Convert.ToInt32(ds.Tables[2].Rows[0]["donationlength"]);
                    }
                    catch
                    {
                        this.DonationLength = -1;
                    }
                    this.DonationLengthUnit = ds.Tables[2].Rows[0]["donationlengthUnit"].ToString();
                    this.PaymentOption = ds.Tables[2].Rows[0]["PaymentOption"].ToString();
                    this.MSContacted = Convert.ToBoolean(ds.Tables[2].Rows[0]["mscontacted"]);
                    try
                    {
                        this.YearlyReassess = Convert.ToBoolean(ds.Tables[2].Rows[0]["yearlyreassess"]);
                    }
                    catch { }
                    if (this.YearlyReassess && this.GIPAPStatus == "Active")
                    {
                        try
                        {
                            this.ReassessDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["reassessdate"]);
                        }
                        catch { }
                    }
                    this.Recommendation = ds.Tables[2].Rows[0]["recommendation"].ToString();
                }
                else
                {
                    this.VerificationComplete = false;
                }
            }

            ds.Dispose();
        }
        //**********************************************************************************************************************
        private void InflatePatientSummary(int pid)
        {
            SqlParameter[] arrParams = new SqlParameter[1];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = pid;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientSummaryProfile", arrParams);

            //0 PATIENT INFO
            this.FlagNOA = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOA"]);
            this.PatientID = (int)(ds.Tables[0].Rows[0]["PatientID"]);
            this.PIN = (ds.Tables[0].Rows[0]["PIN"]).ToString();
            this.CurrentProgram = ds.Tables[0].Rows[0]["currentprogram"].ToString();
            this.FirstName = (ds.Tables[0].Rows[0]["FirstName"]).ToString();
            this.LastName = (ds.Tables[0].Rows[0]["LastName"]).ToString();
            this.ThaiName = (ds.Tables[0].Rows[0]["ThaiName"]).ToString();
            this.Sex = (ds.Tables[0].Rows[0]["Sex"]).ToString();
            this.BirthDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"]);
            this.Diagnosis = ds.Tables[0].Rows[0]["Diagnosis"].ToString();
            this.GIPAPStatus = (ds.Tables[0].Rows[0]["GIPAPStatus"]).ToString();
            this.StatusReason = ds.Tables[0].Rows[0]["StatusReason"].ToString();
            this.Treatment = (ds.Tables[0].Rows[0]["treatment"]).ToString();
            this.IntakeDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IntakeDate"]);
            this.EnableAutoApprove = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnableAutoApprove"]);
            this.EnableAutoClose = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnableAutoClose"]); 
            //1 country
            this.PediatricAge = Convert.ToInt32(ds.Tables[1].Rows[0]["PediatricAge"]);
            if (this.PediatricAge == 0)
            {
                this.PediatricAge = 16;
            }
            if (this.Treatment == "Tasigna")
            {
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["tasignaPedApproved"]);
            }
            else if (this.Diagnosis == "CML")
            {
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["PediatricApproved"]);
            }
            else if (this.Diagnosis == "Ph+ ALL")
            {
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["phallPedApproved"]);
            }
            else if (this.Diagnosis == "DFSP")
            {
                //coutry
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["dfspPedApproved"]);
            }
            else if (this.Diagnosis == "GIST")
            {
                //country
                this.GistApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["GistApproved"]);
                this.PediatricApproved = Convert.ToBoolean(ds.Tables[1].Rows[0]["gistPedApproved"]);
            }
            this.CountryName = ds.Tables[1].Rows[0]["countryname"].ToString();
            this.CountryID = Convert.ToInt32(ds.Tables[1].Rows[0]["countryid"]);
            //2 gipap details ACTIVE ONLY
            if (ds.Tables[2].Rows.Count > 0)
            {
                try
                {
                    this.StartDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["StartDate"]);
                }
                catch { }
                try
                {
                    this.EndDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["EndDate"]);
                }
                catch { }
                this.CurrentDosage = ds.Tables[2].Rows[0]["currentdosage"].ToString();
                this.mPickedUp = Convert.ToBoolean(ds.Tables[2].Rows[0]["pickedup"]);
                try
                {
                    this.DetailCreateDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["createDate"]);
                }
                catch { }
                
            }
            //5 dosage change
            if (ds.Tables[5].Rows.Count > 0)
            {
                this.CurrentDosage = ds.Tables[5].Rows[0]["currentdosage"].ToString();
            }
            //3 close date
            if (ds.Tables[3].Rows.Count > 0)
            {
                this.ClosedDate = Convert.ToDateTime(ds.Tables[3].Rows[0]["ClosedDate"]);
                this.StatusReason = ds.Tables[3].Rows[0]["StatusReason"].ToString();
            }
            //4 deny date
            if (ds.Tables[4].Rows.Count > 0)
            {
                this.DeniedDate = Convert.ToDateTime(ds.Tables[4].Rows[0]["DeniedDate"]);
                this.StatusReason = ds.Tables[4].Rows[0]["StatusReason"].ToString();
            }
            //6 NOA FEF
            if (this.FlagNOA)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.VerificationComplete = true; //complete here means only that the fef has been started
                    this.NOAPIN = ds.Tables[6].Rows[0]["noapin"].ToString();
                    try
                    {
                        this.DonationLength = Convert.ToInt32(ds.Tables[6].Rows[0]["donationlength"]);
                    }
                    catch
                    {
                        this.DonationLength = -1;
                    }
                    this.DonationLengthUnit = ds.Tables[6].Rows[0]["donationlengthUnit"].ToString();
                    this.PaymentOption = ds.Tables[6].Rows[0]["PaymentOption"].ToString();
                    this.MSContacted = Convert.ToBoolean(ds.Tables[6].Rows[0]["mscontacted"]);
                    try
                    {
                        this.YearlyReassess = Convert.ToBoolean(ds.Tables[6].Rows[0]["yearlyreassess"]);
                    }
                    catch { }
                    if (this.YearlyReassess && this.GIPAPStatus == "Active")
                    {
                        try
                        {
                            this.ReassessDate = Convert.ToDateTime(ds.Tables[6].Rows[0]["reassessdate"]);
                        }
                        catch { }
                    }
                    this.Recommendation = ds.Tables[6].Rows[0]["recommendation"].ToString();
                }
                else
                {
                    this.VerificationComplete = false;
                }
            }

            ds.Dispose();
        }
        //**********************************************************************************************************************
        public string PatientSummary(int pid, GIPAP_Objects.User sUse)
        {
            this.InflatePatientSummary(pid);
            StringBuilder patInfo = new StringBuilder();
            string headColor;
            if (this.FlagNOA)
            {
                headColor = "green";
            }
            else if (this.CurrentProgram == "MYPAP")
            {
                headColor = "darkorange";
            }
            else if (this.CurrentProgram == "TIPAP")
            {
                headColor = "purple";
            }
            else
            {
                headColor = "steelblue";
            }
            patInfo.Append("<h1><font color=" + headColor + ">" + this.FirstName + " " + this.LastName + "<br>" + this.PIN + "</font></h1>");
            if (sUse.Role == "TMFUser" || sUse.Role == "MaxStation")
            {
                patInfo.Append("<div class='LeftColSpacer'><a href=PatientInfo.aspx?choice=" + this.PatientID.ToString() + ">PATS</a>");
                patInfo.Append("  <a href=../GIPAPTrusted.aspx?pin=" + this.PIN + "&reqform=Patient><font color=deeppink>PINC</a></font>");
                patInfo.Append("  <a href='' id='addPINCContact'><font color=deeppink>+</a></font>");
                if (this.CurrentProgram == "MYPAP")
                {
                    patInfo.Append("  <a href=/MYPAP/gipaptrusted.aspx?pin=" + this.PIN + "&reqform=MyPapPatient&user=" + sUse.Username + "><font color=darkorange>MYPAP</font></a>");
                }
                else if (this.CurrentProgram == "TIPAP")
                {
                    patInfo.Append("  <a href=/TIPAP/gipaptrusted.aspx?pin=" + this.PIN + "&reqform=TiPapPatient&user=" + sUse.Username + "><font color=purple>TIPAP</font></a>");
                }
                patInfo.Append("</div>");
            }
            else // not possible yet
            {
                patInfo.Append("<div class='LeftColSpacer'></div>");
            }
            if (this.FlagNOA && this.VerificationComplete) //complete means only that an fef has been started
            {
                if (this.DonationLength == 12 || this.DonationLength == -1 || this.DonationLength == 370 || this.DonationLength == 52)
                {
                    patInfo.Append("<div class='NOAGIPAPBox'><STRONG><FONT color=maroon>" + this.CurrentProgram + "</STRONG><BR>");
                }
                else
                {
                    patInfo.Append("<div class='NOABox'><STRONG><FONT color=green>" + this.CurrentProgram + "</STRONG><BR>");
                }
                if (this.NOAPIN == "Fixed")
                {
                    patInfo.Append(this.NOAPIN + "<br>");
                }
                if (this.DonationLength != -1)
                {
                    int k;
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
                    patInfo.Append("<font color=gray><b>" + k.ToString() + " + " + this.DonationLength.ToString() + " (" + this.DonationLengthUnit + ")</b>");
                    if (this.PaymentOption != "")
                    {
                        patInfo.Append(" | " + this.PaymentOption);
                    }
                    patInfo.Append("</font>");
                }
                if (this.GIPAPStatus == "Active" || this.GIPAPStatus == "Closed")
                {
                    if (this.YearlyReassess)
                    {
                        if (this.ReassessDate != Convert.ToDateTime("1/1/0001"))
                        {
                            patInfo.Append("<br><br>Re-assessment: <b>");
                            patInfo.Append(this.ReassessDate.Day.ToString() + " " + this.ReassessDate.ToString("y") + "</b>");
                        }
                    }
                    else
                    {
                        patInfo.Append("<br><br><font color=gray><i>No re-assessment required</i></font>");
                    }
                }
                if (this.Recommendation != "")
                {
                    patInfo.Append("<br><br><b>Recommendation: </b>");
                    if (this.Recommendation == "Pending")
                    {
                        patInfo.Append("<font color=gray>" + this.Recommendation + "</font>");
                    }
                    else if (this.Recommendation == "Deny")
                    {
                        patInfo.Append("<font color=red><b>" + this.Recommendation + "</b></font>");
                    }
                    else
                    {
                        patInfo.Append(this.Recommendation);
                        /*if (this.DonationLength != 12 && this.DonationLength != 370 && this.DonationLength != 52)
                        { this needs to be done for full donation patients now too*/
                            if (this.MSContacted)
                            {
                                patInfo.Append("<br><font color=gray>Patient has been contacted</font>");
                            }
                            else
                            {
                                patInfo.Append("<br><font color=red><i>Patient has NOT been contacted</i></font>");
                            }
                       // }
                    }
                }
                patInfo.Append("</font></div><div class='LeftColSpacer'></div>");
            }
            patInfo.Append("<div class='LeftColDivHeader'>Patient Summary</div><div class='LeftColDiv'>");
            if (this.CountryName == "Thailand") 
            patInfo.Append("<font class='lbl'>Thai Name: </font>" + this.ThaiName + "<br>");
            patInfo.Append("<font class='lbl'>Sex: </font>" + this.Sex + "<br>");
            patInfo.Append("<font class='lbl'>Birth Date: </font>");
            if ((!this.PediatricApproved) && (this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today))
            {
                patInfo.Append("<font color=red>" + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y") + "</font>");
            }
            else if ((this.PediatricApproved) && (this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today))
            {
                if (this.BirthDate.AddYears(2) > DateTime.Today)
                {
                    patInfo.Append("<font color=red>" + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y") + "</font><br><font color=salmon>Under 2 years of age</font>");
                }
                else
                {
                    patInfo.Append("<font color=green>" + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y") + "</font>");
                }
            }
            else
            {
                patInfo.Append(this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y"));
            }
            if (sUse.Role == "TMFUser" || sUse.Role == "MaxStation")
            {
                patInfo.Append("<br><font class='lbl'>Country: </font><a href=../Country/CountryInfo.aspx?choice=" + this.CountryID.ToString() + ">" + this.CountryName + "</a>");
            }
            else
            {
                patInfo.Append("<br><font class='lbl'>Country: </font>" + this.CountryName);
            }
            patInfo.Append("<br><br><strong><font color=steelblue>" + this.GIPAPStatus + "</font> <font class='lbl'>" + this.CurrentProgram + "</font></strong><br>");
            if (this.GIPAPStatus == "Active")
            {
                patInfo.Append("<font class='lbl'>Current Period</font><br>");
                if (this.StartDate.AddDays(120) < this.EndDate)
                {
                    patInfo.Append("<strong><font color=blue>" + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " To ");
                    patInfo.Append(this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "</font></strong>");
                }
                else
                {
                    patInfo.Append("<strong><font color=steelblue>" + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + "</font> To ");
                    patInfo.Append("<font color=steelblue>" + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "</font></strong>");
                }
                //flag if the order was not picked up
                if (this.CountryID == 76 && DateTime.Today.AddDays(30) > this.EndDate && this.DetailCreateDate > Convert.ToDateTime("11/10/2013"))
                {
                    if (!this.FlagNOA || this.DonationLength == 370)
                    {
                        if (!this.mPickedUp)
                        {
                            patInfo.Append("<div class='AlertDiv' style='width:90%';>Previous order was not picked up</div>");
                        }
                    }
                }
            }
            else if (this.GIPAPStatus == "Closed")
            {
                patInfo.Append("<font class='lbl'>Close Date: </font>" + this.ClosedDate.Day.ToString() + " " + this.ClosedDate.ToString("y")+"<br>");
                patInfo.Append("<font class='lbl'>Status Reason: </font>");
                if (this.StatusReason == "Approved with Partial Coverage")
                {
                    patInfo.Append("<font color=red>" + this.StatusReason + "</font>");
                }
                else
                {
                    patInfo.Append(this.StatusReason);
                }
            }
            else if (this.GIPAPStatus == "Pending")
            {
                patInfo.Append("<font class='lbl'>Intake Date: </font>" + this.IntakeDate.Day.ToString() + " " + this.IntakeDate.ToString("y"));
            }
            else if (this.GIPAPStatus == "Denied")
            {
                patInfo.Append("<font class='lbl'>Denied Date: </font>" + this.DeniedDate.Day.ToString() + " " + this.DeniedDate.ToString("y")+"<br>");
                patInfo.Append("<font class='lbl'>Status Reason: </font>");
                if (this.StatusReason == "Approved with Partial Coverage")
                {
                    patInfo.Append("<font color=red>" + this.StatusReason + "</font>");
                }
                else
                {
                    patInfo.Append(this.StatusReason);
                }
            }
            patInfo.Append("<br><br><b><font class='lbl'>Diagnosis: </font>");
            if (this.Diagnosis == "GIST" && !this.GistApproved)
            {
                patInfo.Append("<font color=red>");
            }
            else if (this.Diagnosis == "Ph+ ALL" || this.Diagnosis == "DFSP")
            {
                patInfo.Append("<font color=red>");
            }
            else
            {
                patInfo.Append("<font color=steelblue>");
            }
            patInfo.Append(this.Diagnosis + "</font></b><br>");
            if (this.Treatment == "Glivec")
            {
                patInfo.Append("<font color=#F76900><b>Glivec ");
                //dosage info
                patInfo.Append(this.CurrentDosage + "</b></font>");

            }
            else if (this.Treatment == "Tasigna")
            {
                patInfo.Append("<font color=purple><b>Tasigna ");
                //dosage info
                if (this.GIPAPStatus == "Active")
                {
                    if (this.CurrentDosage == "400mg BID" || this.CurrentDosage == "400mg QD" || this.CurrentDosage == "300mg BID")
                    {
                        patInfo.Append(this.CurrentDosage + "</b></font>");
                    }
                    else
                    {
                        patInfo.Append("</font> <font color=#F682F6>(Approval Pending)</b></font>");
                    }
                }
            }
            return patInfo.ToString();
        }
		//**********************************************************************************************************************
		public string CountryInfo(int pid, string Urole)
		{
            this.InflateCountryInfo(pid, Urole);
            StringBuilder countInfo = new StringBuilder();
			countInfo.Append("<strong>Country: </strong>" + this.CountryName + "<br>");
            if (!Urole.StartsWith("FC"))
            {
                countInfo.Append("<strong>Max Station: </strong>" + this.MaxStationName + "<br>");
            }
			//countInfo += "<strong>Clinic: </strong>" + this.ClinicName + "<br>";
            if (this.PhysTranRequests > 0 && !Urole.StartsWith("FC"))
            {
                countInfo.Append("<TABLE style='BORDER-RIGHT: red 1px solid; BORDER-TOP: red 1px solid; BORDER-LEFT: red 1px solid; BORDER-BOTTOM: red 1px solid' width='100%'>");
                countInfo.Append("<TR><TD bgColor='lightsalmon'>");
                countInfo.Append("<strong>Physician: </strong>" + this.PhysicianName + "<br>");
                countInfo.Append("<font color=gainsboro>Physician Transfer Request Pending</b></td></tr></table>");
            }
            else
            {
                countInfo.Append("<strong>Physician: </strong>" + this.PhysicianName + "<br>");
            }
            if (!Urole.StartsWith("FC"))
            {
                countInfo.Append("<strong>Program Officer: </strong>" + this.ProgramOfficerName + "<br>");
            }
			countInfo.Append("<b>Contact: </b>" + this.ContactName);
            if (this.FlagNOA || this.NOAPhys)
            {
                countInfo.Append("<br><b>FE Branch: </b>" + this.FCBranch);
            }
            return countInfo.ToString();
        }
        //**********************************************************************************************************************
        public void InflateCountryInfo(int pid, string Urole)
        {
            this.CountryName = "";
            this.MaxStationName = "";
            this.ContactName = "";
            this.ProgramOfficerName = "";
            this.FCBranch = "";
            this.PhysicianName = "";

            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = pid;

            arrParams[1] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
            arrParams[1].Value = Urole;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientCountryProfile2", arrParams);

            //0 PATIENT INFO
            this.PatientID = pid;
            
            this.FlagNOA = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOA"]);
            this.EnableAutoApprove = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnableAutoApprove"]);
            this.EnableAutoClose = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnableAutoClose"]); 
            //1 country
            if (ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["activegipap"].ToString() == "Yes")
                {
                    this.CountryName = ds.Tables[1].Rows[0]["Countryname"].ToString();
                }
                else
                {
                    this.CountryName = "<font color=red>" + ds.Tables[1].Rows[0]["Countryname"].ToString() + "</font>";
                }
                this.CountryID = Convert.ToInt32(ds.Tables[1].Rows[0]["CountryID"]);
                if (Urole == "TMFUser")
                {
                    this.CountryName = "<a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + ">" + this.CountryName + "</a>";
                }
            }
            //2 max station
            if (ds.Tables[2].Rows.Count > 0)
            {
                for (int maxK = 0; maxK < ds.Tables[2].Rows.Count; maxK++)
                {
                    if (Urole == "TMFUser" || Urole == "MaxStation")
                    {
                        this.MaxStationName += "<li><a href=../Person/PersonInfo.aspx?choice=" + ds.Tables[2].Rows[maxK]["personid"].ToString() + "><font color=blue>" + ds.Tables[2].Rows[maxK]["firstname"].ToString() + " " + ds.Tables[2].Rows[maxK]["lastname"].ToString() + "</font></a></li>";
                    }
                    else
                    {
                        this.MaxStationName += "<li>" + ds.Tables[2].Rows[maxK]["firstname"].ToString() + " " + ds.Tables[2].Rows[maxK]["lastname"].ToString() + "</li>";
                    }
                }
            }
            //3 Physician | turn red if not approved
            this.PhysicianCount = ds.Tables[3].Rows.Count;
            if (this.PhysicianCount > 0)
            {
                bool chkPhys = true;
                for (int phyK = 0; phyK < ds.Tables[3].Rows.Count; phyK++)
                {
                    if (Urole == "TMFUser" || Urole == "MaxStation")
                    {
                        if (Convert.ToInt32(ds.Tables[3].Rows[phyK]["approved"]) == 1)
                        {
                            this.PhysicianName += "<li><a href=../Physician/PhysicianInfo.aspx?choice=" + ds.Tables[3].Rows[phyK]["personid"].ToString() + "><font color=blue>" + ds.Tables[3].Rows[phyK]["PhysicianName"].ToString() + "</font></a></li>";
                        }
                        else
                        {
                            this.PhysicianName += "<li><a href=../Physician/PhysicianInfo.aspx?choice=" + ds.Tables[3].Rows[phyK]["personid"].ToString() + "><font color=red>" + ds.Tables[3].Rows[phyK]["PhysicianName"].ToString() + "</font></a></li>";
                        }
                    }
                    else
                    {
                        this.PhysicianName += "<li>" + ds.Tables[3].Rows[phyK]["PhysicianName"].ToString() + "</li>";
                    }
                    //flag PHYSICIAN NOA - checks if physician is noa
                    if (Convert.ToBoolean(ds.Tables[3].Rows[phyK]["noa"]) && chkPhys)
                    {
                        try
                        {
                            if (Convert.ToDateTime(ds.Tables[3].Rows[phyK]["noadate"]) <= DateTime.Now)
                            {
                                this.NOAPhys = true;
                                this.VerificationComplete = false;
                                chkPhys = false;
                            }
                            else { this.NOAPhys = false; }
                        }
                        catch { }
                    }
                    else { this.NOAPhys = false; }
                }
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                for (int poK = 0; poK < ds.Tables[4].Rows.Count; poK++)
                {
                    if (Urole == "TMFUser" || Urole == "MaxStation")
                    {
                        this.ProgramOfficerName += "<li><a href=../Person/PersonInfo.aspx?choice=" + ds.Tables[4].Rows[poK]["personid"].ToString() + "><font color=blue>" + ds.Tables[4].Rows[poK]["firstname"].ToString() + " " + ds.Tables[4].Rows[poK]["lastname"].ToString() + "</font></a></li>";
                    }
                    else
                    {
                        this.ProgramOfficerName += "<li>" + ds.Tables[4].Rows[poK]["firstname"].ToString() + " " + ds.Tables[4].Rows[poK]["lastname"].ToString() + "</li>";
                    }
                }
            }

            //contact
            if (ds.Tables[5].Rows.Count > 0)
            {
                if (Urole == "TMFUser" || Urole == "MaxStation")
                {
                    for (int poK = 0; poK < ds.Tables[5].Rows.Count; poK++)
                    {
                        this.ContactName += "<li>" + "<a href=ContactInfo.aspx?cid=" + ds.Tables[5].Rows[poK]["personid"].ToString() + "&choice=" + this.PatientID.ToString() + ">" + ds.Tables[5].Rows[poK]["ContactName"].ToString() + "</a></li>";
                    }
                }
                else
                {
                    for (int poK = 0; poK < ds.Tables[5].Rows.Count; poK++)
                    {
                        this.ContactName += "<li>" + ds.Tables[5].Rows[poK]["ContactName"].ToString() + "</li>";
                    }
                }
            }
            this.PhysTranRequests = Convert.ToInt32(ds.Tables[6].Rows[0]["count"]);

            //FC Branch
            if (this.CountryID == 76)
            {
                if (ds.Tables[7].Rows.Count > 0)
                {
                    for (int poK = 0; poK < ds.Tables[7].Rows.Count; poK++)
                    {
                        if (Urole == "TMFUser" || Urole == "MaxStation")
                        {
                            this.FCBranch += "<li><a href=../FinancialEvaluator/FEInfo.aspx?choice=" + ds.Tables[7].Rows[poK]["fcofficeid"].ToString() + "><font color=blue>" + ds.Tables[7].Rows[poK]["officename"].ToString() + "</font></a></li>";
                        }
                        else
                        {
                            this.FCBranch += "<li>" + ds.Tables[7].Rows[poK]["officename"].ToString() + "</li>";
                        }
                    }
                }
                //stockist... attach to fe branch
                if (ds.Tables[8].Rows.Count > 0)
                {
                    this.FCBranch += "Stockist:";
                    for (int poK = 0; poK < ds.Tables[8].Rows.Count; poK++)
                    {
                        if (Urole == "TMFUser" || Urole == "MaxStation")
                        {
                            this.FCBranch += "<li><a href=../Stockist/StockistInfo.aspx?choice=" + ds.Tables[8].Rows[poK]["stockistid"].ToString() + "><font color=blue>" + ds.Tables[8].Rows[poK]["officename"].ToString() + "</font></a></li>";
                        }
                        else
                        {
                            this.FCBranch += "<li>" + ds.Tables[8].Rows[poK]["officename"].ToString() + "</li>";
                        }
                    }
                }
            }
            else //if not INdia, show Distributor
            {
                if (ds.Tables[9].Rows.Count > 0)
                {
                    for (int poK = 0; poK < ds.Tables[9].Rows.Count; poK++)
                    {
                        if (Urole == "TMFUser" || Urole == "MaxStation")
                        {
                            this.DistributorName += "<li><a href=../Distributor/DistributorInfo.aspx?choice=" + ds.Tables[9].Rows[poK]["distributorid"].ToString() + "><font color=blue>" + ds.Tables[9].Rows[poK]["officename"].ToString() + "</font></a></li>";
                        }
                        else
                        {
                            this.DistributorName += "<li>" + ds.Tables[9].Rows[poK]["officename"].ToString() + "</li>";
                        }
                    }
                }
            }
            ds.Dispose();
        }
        //**********************************************************************************************************************
        public string PrintApprovalSlip(int pid)
        {
            this.InflateStatusInfo(pid);
            this.InflateDiagnosisInfo(pid);
            StringBuilder diagInfo = new StringBuilder();
            diagInfo.Append("<font color=steelblue size=4>" + this.PIN + "<br>" + this.FirstName + " " + this.LastName + "</font>");
            diagInfo.Append("<br><br><b><font color=gray>Dates currently approved for:</font><br>");
            diagInfo.Append(this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + "</b> to <b>");
            diagInfo.Append(this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "<br><br>");
            diagInfo.Append("<font color=gray>Dosage Prescribed:<br></font>" + this.CurrentDosage + "</b>/Day");
            return diagInfo.ToString();
        }
        //**********************************************************************************************************************
        public DataSet getCaseNotes(int pid)
        {
            this.PatientID = pid;

            SqlParameter[] arrParams = new SqlParameter[1];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCaseNotes2", arrParams);
        }
        //**********************************************************************************************************************
        public string AEHistory(int pid)
        {
            this.PatientID = pid;

            SqlParameter[] arrParams = new SqlParameter[1];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetAEHistory", arrParams);

            StringBuilder sae = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sae.Append("<p>" + ds.Tables[0].Rows[i]["event"].ToString() + "<br><b><i>" + ds.Tables[0].Rows[i]["createdby"].ToString() + " " + ds.Tables[0].Rows[i]["createdate"].ToString() + "</i></b></p>");
                }
            }
            return sae.ToString();
        }
		//**********************************************************************************************************************
		public string AutoApproveEnabled()
		{
            string eaa = "<font class='lbl'>Auto Approve: </font>";
			if(this.EnableAutoApprove)
			{
				eaa += "<font color=steelblue>Enabled</font>";
			}
			else
			{
				eaa += "<font color=red>Disabled</font>";
			}
			return eaa;
		}
		//**********************************************************************************************************************
		public string AutoCloseEnabled()
		{
            string eac = "<font class='lbl'>Auto Close: </font>";
			if(this.EnableAutoClose)
			{
				eac += "<font color=steelblue>Enabled</font>";
			}
			else
			{
				eac += "<font color=red>Disabled</font>";
			}
			return eac;
		}
		//**********************************************************************************************************************
		public string PatientDataLinks(int pid, string Urole)
		{
            this.PatientID = pid;
			string dlinks = "";
			if(Urole == "TMFUser" || Urole == "MaxStation")
			{
                dlinks += "<a href=EditPatient.aspx?choice=" + this.PatientID.ToString() + ">Edit Patient Info</a><br><br>";
                dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=statushistory>Status History</a><br><br>";
                dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=requesthistory>Request History</a><br><br>";
                dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=sae>Adverse Events</a><br><br>";
                dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=emails>Emails</a><br><br>";
                dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=physicianhistory>Physician History</a>";
			}
			return dlinks;
		}
		//**********************************************************************************************************************
		public int getPatientID(string pPin)
		{
			SqlParameter arrParams = new SqlParameter("@PIN", SqlDbType.NVarChar, 50);
			arrParams.Value = pPin;

			DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientID", arrParams);
			return (int)ds.Tables[0].Rows[0]["patientid"];
		}
		//**********************************************************************************************************************
		public DataSet getPatientPersonList(string pType)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@PersonType", SqlDbType.NVarChar, 50);
			arrParams[1].Value = pType;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientPersonList", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet getPatientSearchDropDowns(int Uid, string Urole)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = Uid;

			arrParams[1] = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
			arrParams[1].Value = Urole;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getPatientSearchDropDowns", arrParams);
		}


        //**********************************************************************************************************************
        public string FindMassTransferPO(string countryid, string physicianid, string poid)
        {
            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@CountryID", SqlDbType.NVarChar,50);
            arrParams[0].Value = countryid;

            arrParams[1] = new SqlParameter("@PhysicianID", SqlDbType.NVarChar, 50);
            arrParams[1].Value = physicianid;

            arrParams[2] = new SqlParameter("@Personid", SqlDbType.NVarChar, 50);
            arrParams[2].Value = poid;

            return Convert.ToString (SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, "spr_getMassTransferPOCount", arrParams));
        }
		//**************************************************************************************************************
		public void UpdatePerson(System.Web.UI.WebControls.ListBox lb, string pType)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			if(pType == "Physician")
			{
				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeletePatientPhysicians", arrParams[0]);

				for(int i=0; i<lb.Items.Count; i++)
				{
					arrParams[1] = new SqlParameter("@PersonID", SqlDbType.Int);
					arrParams[1].Value = Convert.ToInt32(lb.Items[i].Value);

					SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatientPhysicians", arrParams);
				}
			}
		}
		//**************************************************************************************************************
		public void AddCaseNote(string createdby, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = createdby;

			arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
			arrParams[2].Value = note;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateCaseNote", arrParams);
		}		
		//**************************************************************************************************************
		public void UpdateAutoApprove(string modifiedby)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = modifiedby;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateAutoApprove", arrParams);
		}
		//**************************************************************************************************************
		public void UpdateAutoClose(string modifiedby)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = modifiedby;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateAutoClose", arrParams);
		}
		//**************************************************************************************************************
		public void UpdateFirstReminder(string modifiedby, int patid)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = patid;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = modifiedby;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateFirstReminder", arrParams);
		}
		//**************************************************************************************************************
		public void UpdateSecondReminder(string modifiedby, int patid)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = patid;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = modifiedby;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateSecondReminder", arrParams);
		}
        //**********************************************************************************************************************
        public void LogContact(System.Web.UI.WebControls.CheckBoxList contactCB, string cBy)
        {
            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@PIN", SqlDbType.VarChar, 50);
            arrParams[0].Value = this.PIN;

            arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
            arrParams[1].Value = cBy;

            if (contactCB.Items.Count > 0)
            {
                for (int i = 0; i < contactCB.Items.Count; i++)
                {
                    if (contactCB.Items[i].Selected)
                    {

                        arrParams[2] = new SqlParameter("@ContactType", SqlDbType.NVarChar, 50);
                        arrParams[2].Value = contactCB.Items[i].Value;

                        SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_LogPINCPatientContact", arrParams);
                    }
                }
            }
        }
        //**********************************************************************************************************************
        /*EMAILS**********************************************************************************************************/
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
        public GIPAP_Objects.Email firstReminderEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                if (this.DonationLength != 12 && this.DonationLength != 370)
                {
                    return this.NOAfirstReminderEmailPatient(pid);
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.To = this.Email.ToString();
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.Subject = "GIPAP Re-Evaluation Reminder - " + this.PIN.ToString();

            myEmail.Message = "GIPAP RE-EVALUATION REMINDER" + "(PATIENT) " + "\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["FirstName"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["LastName"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName.ToString() + " " + this.LastName.ToString() + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["CountryName"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName.ToString() + " " + this.LastName + ":\n";
            myEmail.Message += "It is time for you to be re-evaluated to continue receiving your supply of Glivec(R) through the Glivec(R) International Patient Assistance Program (GIPAP).   Please visit your physician at your scheduled time.   This visit is needed to determine your eligibility for your next 120 day supply. \n\n";
            myEmail.Message += "Original GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y") + "\n";
            myEmail.Message += "Dates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y");
            myEmail.Message += " to " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.MailType = "Reminder";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOAfirstReminderEmailPatient(int pid)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.To = this.Email.ToString();
            myEmail.From = "gipap@themaxfoundation.org";
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Re-Evaluation Reminder - " + this.PIN.ToString();
                    myEmail.Message = "TIPAP RE-EVALUATION REMINDER" + "(PATIENT) " + "\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Re-Evaluation Reminder - " + this.PIN.ToString();
                    myEmail.Message = "NOA Tasigna RE-EVALUATION REMINDER" + "(PATIENT) " + "\n\n";
                }
            }
            else
            {
                myEmail.Subject = "NOA Re-Evaluation Reminder - " + this.PIN.ToString();
                myEmail.Message = "NOA RE-EVALUATION REMINDER" + "(PATIENT) " + "\n\n";
            }

            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["FirstName"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["LastName"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName.ToString() + " " + this.LastName.ToString() + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["CountryName"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName.ToString() + " " + this.LastName + ":\n";
            if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += "It is time for you to be re-evaluated to continue receiving your supply of " + this.Treatment + "(R) through the Tasigna(R) International Patient Assistance Program (TIPAP) of Novartis Pharma AG.   Please visit your physician at your scheduled time.  This visit is needed to determine your eligibility for your next 120 day supply. \n\n";
            }
            else
            {
                myEmail.Message += "It is time for you to be re-evaluated to continue receiving your supply of " + this.Treatment + "(R) through the Novartis Oncology Access Program (NOA) of Novartis Pharma AG.   Please visit your physician at your scheduled time.  This visit is needed to determine your eligibility for your next 120 day supply. \n\n";
            }
            myEmail.Message += "Original Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y") + "\n";
            myEmail.Message += "Dates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y");
            myEmail.Message += " to " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.MailType = "Reminder";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        /*public GIPAP_Objects.Email firstReminderEmailPhysician(int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if(this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[10].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";
            if(this.patientDS.Tables[3].Rows.Count > 0)
            {
                for(int i=0; i<this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += "; " + this.patientDS.Tables[3].Rows[i]["email"].ToString();
                }
            }
            myEmail.Subject = "GIPAP Re-Evaluation Request - " + this.PIN.ToString();
            myEmail.Message = "GIPAP RE-EVALUATION REQUEST (PHYSICIAN) " + "\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[10].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[10].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "The current GIPAP approval period for your patient, " + this.FirstName + " " + this.LastName + " will be ending on ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + ".  We appreciate your help in re-evaluating your patient’s financial and medical eligibility for GIPAP.  Please log on to the on-line GIPAP Patient Assistance Tracking System (PATS):\n";
            myEmail.Message += "	Go to http://www.maxaid.org (or http://www.themaxfoundation.org and click on the GIPAP/Glivec section)\n";
            myEmail.Message += "	Login with your username and password.  (If you need this information please request it from us in an email at Gipap@themaxfoundation.org)\n";
            myEmail.Message += "	Choose the appropriate action from the menu.\n\n";
            myEmail.Message += "---PATIENT INFORMATION---\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName + "\n";
            myEmail.Message += "Original GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y") + "\n";
            myEmail.Message += "Dates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y");
            myEmail.Message += " to " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "If you are unable to login please answer the questions below and send them to us via email (Gipap@themaxfoundation.org) or fax. 1 (425) 778-8760\n\n";
            myEmail.Message += "Re-approval Questions:\n\n";
            myEmail.Message += "1.	The current dosage prescribed is " + this.CurrentDosage + "/day. Do you recommend a change in the dosage? (200mg, 260mg, 300mg, 400mg, 600mg, 800mg) \nThe reason for a dosage change is no longer required.";
            myEmail.Message += "2.	Do you recommend that the patient continue treatment with Glivec?\n";
            myEmail.Message += "3.	To your knowledge has the financial status of the patient remained the same?\n";
            if(this.Diagnosis == "CML")
            {
                myEmail.Message += "4.	Current CML phase (blast crisis- accelerated – chronic – remission)\n";
                myEmail.Message += "5.	Hematological response (complete – partial – minor – none)\n";
                myEmail.Message += "6.	Cytogenetic response(complete – partial – minor – minimal – none)\n";
            }
            else if(this.Diagnosis == "GIST")
            {
                myEmail.Message += "4.	What has been the tumor response: (Complete Response – Partial Response – Stable Disease – Progression Disease – Don’t Know)\n";
            }
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if(this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if(this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[8].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if(this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if(this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[8].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if(this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch{}
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            myEmail.MailType = "Reminder";
            return myEmail;
        }*/
        //**********************************************************************************************************************
        public GIPAP_Objects.Email Day90ReminderEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                if (this.DonationLength != 12 && this.DonationLength != 370)
                {
                    return this.NOADay90ReminderEmailPatient(pid);
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.To = this.Email.ToString();
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.Subject = "GIPAP Re-Evaluation 90 Day Reminder - " + this.PIN.ToString();

            myEmail.Message += "FINAL GIPAP RE EVALUATION REMINDER (PATIENT)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
            myEmail.Message += "Your current approval period in the Glivec International Patient Assistance Program (GIPAP) has ended. As a requirement of your participation in GIPAP, you must contact your physician for re-evaluation as soon as possible. \n\n";
            myEmail.Message += "Original GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            myEmail.MailType = "SecondReminder";
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOADay90ReminderEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.To = this.Email.ToString();
            myEmail.From = "gipap@themaxfoundation.org";
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Re-Evaluation 90 Day Reminder - " + this.PIN.ToString();
                    myEmail.Message += "FINAL TIPAP RE EVALUATION REMINDER (PATIENT)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Re-Evaluation 90 Day Reminder - " + this.PIN.ToString();
                    myEmail.Message += "FINAL NOA Tasigna RE EVALUATION REMINDER (PATIENT)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "NOA Re-Evaluation 90 Day Reminder - " + this.PIN.ToString();
                myEmail.Message += "FINAL NOA RE EVALUATION REMINDER (PATIENT)\n\n";
            }

            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
            if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += "Your current approval period in the Tasigna(R) International Patient Assistance Program (TIPAP) has ended. As a requirement of your participation in TIPAP, you must contact your physician for re-evaluation as soon as possible.\n\n";
            }
            else
            {
                myEmail.Message += "Your current approval period in the Novartis Oncology Access Program (NOA) has ended. As a requirement of your participation in NOA, you must contact your physician for re-evaluation as soon as possible.\n\n";
            }
            myEmail.Message += "Original Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.MailType = "SecondReminder";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ApprovalEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                return this.NOAApprovalEmailPatient();
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[5].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[5].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To = this.patientDS.Tables[5].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (myEmail.To == "")
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.From = "gipap@themaxfoundation.org";

            myEmail.Subject = "GIPAP New Patient Approval - " + this.PIN;
            myEmail.Message = "GIPAP NEW PATIENT APPROVAL (PATIENT)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
          
                myEmail.Message += "We have reviewed your medical and financial information and we are happy to inform you that you have qualified for the Glivec(R) International Patient Assistance Program (GIPAP) of Novartis Pharma AG. As you remain eligible for the program, you will receive Glivec (R) at no charge.\n\nNovartis will provide a donation of Glivec as prescribed by your treating physician for the period stated below.";
                myEmail.Message += "\n\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
                myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n";
                myEmail.Message += "\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
                myEmail.Message += "Your treating physician may change your dosage for medical reasons. In this case, the donation dosage will automatically be adjusted with the new prescription.\n\n";
                myEmail.Message += "You will receive your first 120 day supply of Glivec(R) through your physician.  To remain in the program, you will be required to visit your physician on a regular basis to check your progress.  You will be considered for your next 120 day supply of Glivec(R) based upon your physician’s determination of your eligibility.\n\n";
                myEmail.Message += "It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
            
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOAApprovalEmailPatient()
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[5].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[5].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To = this.patientDS.Tables[5].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (myEmail.To == "")
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.From = "gipap@themaxfoundation.org";

            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP New Patient Approval - " + this.PIN;
                    myEmail.Message = "TIPAP NEW PATIENT APPROVAL (PATIENT)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna New Patient Approval - " + this.PIN;
                    myEmail.Message = "NOA Tasigna NEW PATIENT APPROVAL (PATIENT)\n\n";
                }
            }
            else if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "PAT APROBACIÓN DE PACIENTE - " + this.PIN;
                myEmail.Message = "PAT APROBACIÓN DE PACIENTE (PACIENTE)\n\n";
            }
            else if (this.DonationLength == 12 || this.DonationLength == 370)
            {
                myEmail.Subject = "NOA / GIPAP New Patient Approval - " + this.PIN;
                myEmail.Message = "NOA / GIPAP NEW PATIENT APPROVAL (PATIENT)\n\n";
            }
            else
            {
                myEmail.Subject = "NOA New Patient Approval - " + this.PIN;
                myEmail.Message = "NOA NEW PATIENT APPROVAL (PATIENT)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                if (this.CountryID == 108)
                {
                    myEmail.Message += "Nombre del Médico: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
                }
                else
                {
                    myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "Estimado " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "Revisamos su información médica y financiera y nos complace el informarle que usted a calificado para el Programa de Asistencia Temporal de Novartis (PAT). Por el tiempo que permanezca elegible, usted recibirá Glivec® sin costo alguno.\n\n";
            }
            else if (this.CountryID == 102)//malaysia
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "We are happy to inform you that you have qualified for the Novartis Oncology Access (NOA) Program. NOA is a co-pay 1+1 program. For every 1 month purchased, Novartis will donate 1 month of supply.";
            }
            else if (this.CountryID == 76) // india
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
                if (!this.Fixed)
                {
                    myEmail.Message += "At the outset, we would like to appreciate your cooperation to undergo financial evaluation with Indiabulls. Indiabulls, an independent third party financial evaluation agency, used World Health Organisation guidelines on drug access plans to evaluate their decision.";
                }
                myEmail.Message += "We are happy to inform you that you have qualified for a donation of " + this.Treatment + "® according to a ";
                myEmail.Message += this.DonationScheme();
                if (this.DonationLength == 12 || this.DonationLength == 370)
                {
                    myEmail.Message += " donation scheme. ";
                }
                else if (this.DonationLength == 0)
                {
                    myEmail.Message += " payment scheme. ";
                }
                else
                {
                    myEmail.Message += " co-pay/donation scheme. Your co-pay will be achieved according to the " + this.PaymentOption + " payment option you have chosen.  ";
                }
                if (this.Treatment == "Tasigna")
                {
                    myEmail.Message += "This determination is valid for the 24 months following this approval. If your physician recommends continuation of treatment after this 24 month period, we will inform you at that time of the available schemes.";
                }
                else
                {
                    myEmail.Message += "Every 24 months, your financial information will be re-assessed to determine your ongoing eligibility for the program.";
                }
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "We are happy to inform you that you have qualified for the Tasigna(R) International Patient Assistance Program (TIPAP) of Novartis Pharma AG.";
            }
            else if (this.CountryID == 179)
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "We are happy to inform you that you have qualified for the Novartis Oncology Access (NOA) Program ";

                myEmail.Message += "for access to Tasigna® according to a 2 + 50 weeks co-pay/donation scheme.This assessment is valid for 52 weeks or 12 months. The program requires that you purchase one week of Tasigna® within the first four months of the annual approval period.\n You will be required to purchase another week of Tasigna® in the next four months of the annual approval period. As long as your treating physician continues to prescribe this treatment to you, the NOA program will make available 50 weeks of supply of Tasigna® at no charge for a 12 month period. Every 12 months, your financial information will be re-assessed to determine your ongoing eligibility for the program.";

            }
            else//south africa, generic
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "We are happy to inform you that you have qualified for the Novartis Oncology Access (NOA) Program.";
            }
            if (this.CountryID == 108)
            {
                myEmail.Message += "\n\nPeriodo period: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " a ";
                myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n";
                myEmail.Message += "\nDosis prescrita: " + this.CurrentDosage.ToString() + "/day\n\n";
            }
            else
            {
                myEmail.Message += "\n\nSupply Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
                myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n";
                myEmail.Message += "\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            }

            if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "Para permanecer en el programa, es necesario que visites a tu medico tratante regularmente para revisar el progreso de tu enfermedad (mensualmente).  Su médico llevará a cabo una evaluación mensualmente. Cada cuatro meses, tendrá que volver a aplicar para continuar en el programa. Su información financiera será re-evaluada cada 12 meses, para determinar su elegibilidad para el programa en curso.\n\n";
                myEmail.Message += "Es muy importante que siga las instrucciones de su médico al pie de la letra y que asista a todas sus citas.\n\n";
                myEmail.Message += "Su médico tratante puede cambiar la dosis por razones médicas. En este caso, los medicamentos adicionales, tendrán que ser adquiridos según los términos y condiciones del programa.\n\n";
                myEmail.Message += "Si tiene alguna pregunta o aclaración, por favor contáctese con Fundación Max en México al 52 5595 85 66 o stefany.green@THEMAXFOUNDATION.ORG , o en Monterrey al 52 81 8317 4292 o  cynthia.figueroa@THEMAXFOUNDATION.ORG, o bien comuníquese con su médico tratante.";
                myEmail.Message += "\n\nAtentamente, \n\nFundación Max";
            }
            else if (this.CountryID == 76) //india
            {
                myEmail.Message += "To remain in the program, you will be required to visit your physician on a regular basis to check your progress. You will be considered for your next ";
                if (this.DonationLength == 12 || this.DonationLength == 370)
                {
                    myEmail.Message += "120 day supply of " + this.Treatment + "(R) based upon your physician’s determination of your medical eligibility. It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
                }
                else
                {
                    myEmail.Message += "120 day supply of " + this.Treatment + "(R) based upon your physician’s determination of your medical eligibility. It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
                }
                myEmail.Message += "Your treating physician may change your dosage for medical reasons. In this case, the donation dosage will automatically be adjusted with the new prescription.\n\n";
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
           
            else//malaysia, south africa, generic
            {
                myEmail.Message += "To remain in the program, you will be required to visit your physician on a regular basis to check your progress. There will be a periodic medical assessment by your physician every 4 months. It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
                if (this.DonationLength == 370 || this.DonationLength == 52) //fulldonation
                {
                    myEmail.Message += "Your treating physician may change your dosage for medical reasons. In this case, the additional drugs required will send by Novartis to meet your treatment needs.";
                }
                else //co pay
                {
                    myEmail.Message += "Your treating physician may change your dosage for medical reasons. In this case, the additional drugs required will need to be purchased based on co-pay program terms and conditions.\n\n";
                }
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ApprovalEmailPhysician(int pid, int count)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                return this.NOAApprovalEmailPhysician(count);
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";

            if (this.patientDS.Tables[16].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[16].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[16].Rows[i]["email"].ToString() != "")
                    {
                        if (myEmail.CC.Length > 0)
                            myEmail.CC += ";" + this.patientDS.Tables[16].Rows[i]["email"].ToString();
                        else
                            myEmail.CC = this.patientDS.Tables[16].Rows[i]["email"].ToString();
                    }
                }
            }

            myEmail.Subject = "GIPAP New Patient Approval - " + this.PIN;
            if (this.CurrentCMLPhase == "Blast Crisis")
            {
                myEmail.Subject += " - Blast Crisis";
            }
            myEmail.Message = "GIPAP NEW PATIENT APPROVAL (PHYSICIAN)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString();
            if (this.patientDS.Tables[4].Rows[count]["phone"].ToString() != "")
            {
                myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[count]["phone"].ToString();
            }
            if (this.patientDS.Tables[4].Rows[count]["fax"].ToString() != "")
            {
                myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[count]["fax"].ToString();
            }
            myEmail.Message += "\n" + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "Your patient, " + this.FirstName + " " + this.LastName + ", has qualified for the Glivec(R) International Patient Assistance Program (GIPAP) of Novartis Pharma AG. The first 120 day supply of Glivec(R) for your patient will be sent to you by Novartis. To remain in the program he/she will be required to visit you on a regular basis to check his/her progress. \n\nFor your patient to qualify for additional supplies of Glivec(R), and to ensure that here is no interruption in the supply of Glivec (R), you must file an eligibility re-evaluation with The Max Foundation 30 days before the current Glivec(R) supply expires even if you patient has extra supply that will last beyond the end of his/her approval period.  As long as your patient remains eligible, an additional supply of Glivec(R) for the next 120 day period will be delivered to you for his/her use at no charge. \n\n";
            myEmail.Message += "The supply of Glivec(R) was sent to you by Novartis exclusively to be used by this patient.  In handling your patient’s leftover Glivec(R), please act in accordance with the protocol outlined in the Qualified Institution-Physician MOU.\n\n";
            myEmail.Message += "---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
            myEmail.Message += "\nOriginal GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage + "/day\n\n";

            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOAApprovalEmailPhysician(int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";

            if (this.patientDS.Tables[16].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[16].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[16].Rows[i]["email"].ToString() != "")
                    {
                        if (myEmail.CC.Length > 0)
                            myEmail.CC += ";" + this.patientDS.Tables[16].Rows[i]["email"].ToString();
                        else
                            myEmail.CC = this.patientDS.Tables[16].Rows[i]["email"].ToString();
                    }
                }
            }


            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP New Patient Approval - " + this.PIN;
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna New Patient Approval - " + this.PIN;
                }
            }
            else if (this.CountryID == 108)
            {
                myEmail.Subject = "PAT New Patient Approval - " + this.PIN;
            }
            else if (this.DonationLength == 12 || this.DonationLength == 370)
            {
                myEmail.Subject = "NOA / GIPAP New Patient Approval - " + this.PIN;
            }
            else
            {

                myEmail.Subject = "NOA New Patient Approval - " + this.PIN;
            }
            if (this.CurrentCMLPhase == "Blast Crisis")
            {
                myEmail.Subject += " - Blast Crisis";
            }

            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Message = "TIPAP NEW PATIENT APPROVAL (PHYSICIAN)\n\n";
                }
                else
                {
                    myEmail.Message = "NOA Tasigna NEW PATIENT APPROVAL (PHYSICIAN)\n\n";
                }
            }
            else if (this.CountryID == 108)
            {
                myEmail.Message = "PAT NEW PATIENT APPROVAL (PHYSICIAN)\n\n";
            }
            else if (this.DonationLength == 12 || this.DonationLength == 370)
            {
                myEmail.Message = "NOA / GIPAP NEW PATIENT APPROVAL (PHYSICIAN)\n\n";
            }
            else
            {
                myEmail.Message = "NOA NEW PATIENT APPROVAL (PHYSICIAN)\n\n";
            }

            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString();
            if (this.patientDS.Tables[4].Rows[count]["city"].ToString() != "")
            {
                myEmail.Message += "\nCity: " + this.patientDS.Tables[4].Rows[count]["city"].ToString();
            }
            if (this.patientDS.Tables[4].Rows[count]["phone"].ToString() != "")
            {
                myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[count]["phone"].ToString();
            }
            if (this.patientDS.Tables[4].Rows[count]["fax"].ToString() != "")
            {
                myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[count]["fax"].ToString();
            }
            myEmail.Message += "\n" + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "Your patient, " + this.FirstName + " " + this.LastName;
            if (this.CountryID == 102)
            {
                myEmail.Message += " has qualified for the Novartis Oncology Access (NOA) Program. NOA is a co-pay 1+1 program. For every 1 month purchased, Novartis will donate 1 month of supply. \n\n";
                myEmail.Message += "To remain in the program he/she will be required to visit you on a regular basis to check his/her progress.\n\n";
                myEmail.Message += "For your patient to qualify for additional supplies of " + this.Treatment + "®, and to ensure that there is no interruption in the supply of " + this.Treatment + "®, you must file a medical re-evaluation 30 days before the current " + this.Treatment + "® supply expires.  As long as your patient remains eligible in NOA, s/he may continue to access " + this.Treatment + "® for the next 4 months period on co-pay scheme.\n\n";
            }
            else if (this.CountryID == 108)
            {
                myEmail.Message += " has qualified for the Novartis program: Novartis Oncology Temporal Assistance Program (PAT). For the time that the patient remains eligible, the patient will receive Glivec® free of charge.\n\n";
                myEmail.Message += "Novartis will provide the donation of Glivec ® as per your prescription below and for the period of time mentioned below.\n\n";
                myEmail.Message += "To remain in the program he/she will be required to visit you on a regular basis to check his/her progress. \n\n";
                myEmail.Message += "For your patient to qualify for additional supplies of Glivec®, and to ensure that there is no interruption in the supply of Glivec®, you must file a medical re-evaluation 30 days before the current Glivec® supply expires.  As long as your patient remains eligible in PAT, she/he may continue to access Glivec® for the next 4 months period.\n\n";
            }
            else if (this.CountryID == 76) //india
            {
                myEmail.Message += ", has qualified for a donation of " + this.Treatment + "® with approval for a ";
                myEmail.Message += this.DonationScheme();
                if (this.DonationLength == 12 || this.DonationLength == 370)
                {
                    myEmail.Message += " donation scheme. ";
                }
                else if (this.DonationLength == 0)
                {
                    myEmail.Message += " payment scheme. ";
                }
                else
                {
                    myEmail.Message += " co-pay/donation scheme. ";
                }
                if (this.Treatment == "Tasigna")
                {
                    myEmail.Message += "This determination is valid for the 24 months following this approval. If you recommend continuation of treatment after this 24 month period, we will inform your patient at that time of the available schemes.";
                }
                else
                {
                    myEmail.Message += "Every 24 months, the patient’s financial information will be re-assessed to determine ongoing eligibility for the program.";
                }
                if (!this.Fixed)
                {
                    myEmail.Message += " As known to you, the suitable donation program for your patient has been decided by an independent financial evaluation agency, Indiabulls, which has used World Health Organisation guidelines for drug access programs.";
                }
                myEmail.Message += "\n\nYour patient has been instructed regarding collecting the first supply of " + this.Treatment + "®. To remain in the program he/she will be required to visit you on a regular basis to check his/her progress.\n\n";

                myEmail.Message += "For your patient to qualify for additional supplies of " + this.Treatment + "®, and to ensure that there is no interruption in the supply of " + this.Treatment + "®, you must file a medical re-evaluation 30 days before the current " + this.Treatment + "® supply expires.  As long as your patient remains medically and financially eligible, s/he may continue to collect additional supply of " + this.Treatment + "® for the next ";
                if (this.DonationLength == 12 || this.DonationLength == 370)
                {
                    myEmail.Message += "120 day period.\n\n";
                }
                else
                {
                    myEmail.Message += "120 day period.\n\n";
                }
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += " has qualified for the Tasigna(R) International Patient Assistance Program (TIPAP) of Novartis Pharma AG. \n\n";
                myEmail.Message += "To remain in the program he/she will be required to visit you on a regular basis to check his/her progress.  He/she will also be required to undergo a yearly financial assessment.\n\n";
                myEmail.Message += "For your patient to qualify for additional supplies of " + this.Treatment + "®, and to ensure that there is no interruption in the supply of " + this.Treatment + "®, you must file a medical re-evaluation 30 days before the current " + this.Treatment + "® supply expires.  As long as your patient remains eligible in TIPAP, s/he may continue to access " + this.Treatment + "® for the next 4 months period.\n\n";
            }
            else if (this.CountryID == 179) //vietnam
            {
                myEmail.Message += " has qualified for the Novartis Oncology Access (NOA) Program according to a 2 + 50 weeks co-pay/donation scheme. \n\n";
                myEmail.Message += "To remain in the program he/she will be required to visit you on a regular basis to check his/her progress.  He/she will also be required to undergo a yearly financial assessment.\n\n";
                myEmail.Message += "For your patient to qualify for additional supplies of " + this.Treatment + "®, and to ensure that there is no interruption in the supply of " + this.Treatment + "®, you must file a medical re-evaluation 30 days before the current " + this.Treatment + "® supply expires.  As long as your patient remains eligible in NOA, s/he may continue to access " + this.Treatment + "® for the next 4 months period.\n\n";
            }
            else //south africa, generic
            {
                myEmail.Message += " has qualified for the Novartis Oncology Access (NOA) Program. \n\n";
                myEmail.Message += "To remain in the program he/she will be required to visit you on a regular basis to check his/her progress.  He/she will also be required to undergo a yearly financial assessment.\n\n";
                myEmail.Message += "For your patient to qualify for additional supplies of " + this.Treatment + "®, and to ensure that there is no interruption in the supply of " + this.Treatment + "®, you must file a medical re-evaluation 30 days before the current " + this.Treatment + "® supply expires.  As long as your patient remains eligible in NOA, s/he may continue to access " + this.Treatment + "® for the next 4 months period.\n\n";
            }
            myEmail.Message += "---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
            myEmail.Message += "\nApproval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nSupply Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage + "/day\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ReApprovalEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                if ((this.DonationLength != 12 && this.DonationLength != 370) || this.CountryID == 108)
                {
                    return this.NOAReApprovalEmailPatient();
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
            myEmail.From = "gipap@themaxfoundation.org";

            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += "; " + this.patientDS.Tables[3].Rows[i]["email"].ToString();
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[5].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[5].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To = this.patientDS.Tables[5].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (myEmail.To == "")
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }

            myEmail.Subject = "GIPAP Patient Re-Approval - " + this.PIN;
            myEmail.Message = "GIPAP PATIENT RE-APPROVAL (PATIENT)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
            myEmail.Message += "We are happy to inform you that you have re-qualified for the Glivec(R) International Patient Assistance Program (GIPAP) of Novartis Pharma AG.\n\n";
            myEmail.Message += "Original GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y") + "\n";
            myEmail.Message += "New Period Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            myEmail.Message += "Your treating physician may change your dosage for medical reasons. In this case, the donation dosage will automatically be adjusted with the new prescription.\n\n";
            myEmail.Message += "It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";

            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOAReApprovalEmailPatient()
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
            myEmail.From = "gipap@themaxfoundation.org";

            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[3].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.CC += "; " + this.patientDS.Tables[3].Rows[i]["email"].ToString();
                    }
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[5].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[5].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To = this.patientDS.Tables[5].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (myEmail.To == "")
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.CountryID == 108)
            {
                myEmail.Subject = "PAT RE-APROBACIÓN PACIENTE - " + this.PIN;
                myEmail.Message = "PAT RE-APROBACIÓN PACIENTE (PACIENTE)\n\n";
            }
            else if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Patient Re-Approval - " + this.PIN;
                    myEmail.Message = "TIPAP PATIENT RE-APPROVAL (PATIENT)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Patient Re-Approval - " + this.PIN;
                    myEmail.Message = "NOA Tasigna PATIENT RE-APPROVAL (PATIENT)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "NOA Patient Re-Approval - " + this.PIN;
                myEmail.Message = "NOA PATIENT RE-APPROVAL (PATIENT)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "Estimado " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "Estamos felices de informarle que después de revisar la información socioeconómica y financiera que ha entregado, ha sido re-aprobado para el Programa de Asistencia Temporal de Novartis (PAT).\n\n";
                myEmail.Message += "Fecha Inicial: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y") + "\n";
                myEmail.Message += "Periodo: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
                myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
                myEmail.Message += "Dosis Prescrita: " + this.CurrentDosage.ToString() + "/al día\n\n";
                myEmail.Message += "Su médico tratante puede cambiar la dosis por razones médicas. Es muy importante que siga las instrucciones de su médico al pie de la letra y que asista a todas sus citas, así como, regresar las cajas vacías de Glivec a su médico.\n\n";
                myEmail.Message += "Si tiene alguna pregunta o aclaración, por favor contáctese con Fundación Max en México al 52 5595 85 66 o stefany.green@THEMAXFOUNDATION.ORG , o en Monterrey al 52 81 8317 4292 o  cynthia.figueroa@THEMAXFOUNDATION.ORG, o bien comuníquese con su médico tratante.";
                myEmail.Message += "\n\nAtentamente, \n\nFundación Max";
            }
            else
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
                if (this.CountryID == 102)
                {
                    myEmail.Message += "We are happy that you will be continuing with the Novartis Oncology Access (NOA) Program.  NOA is a co-pay 1+1 program. For every 1 month purchased, Novartis will donate 1 month of supply.\n\n";
                }
                else if (this.CountryID == 76)
                {
                    myEmail.Message += "We are happy to inform you that you have re-qualified for your next 120 day re-approval period in the Novartis Oncology Access (NOA) Program of Novartis Pharma AG. You will continue to receive " + this.Treatment + "® according to a ";
                    myEmail.Message += this.DonationScheme();
                    if (this.DonationLength == 0)
                    {
                        myEmail.Message += " payment ";
                    }
                    else
                    {
                        myEmail.Message += " co-pay/donation ";
                    }
                    myEmail.Message += "scheme for which you are approved, and the " + this.PaymentOption + " payment option you have chosen.\n\n";
                }
                else if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
                {
                    myEmail.Message += "We are happy that you will be continuing with the Tasigna(R) International Patient Assistance Program (TIPAP) of Novartis Pharma AG. \n\n";
                }
                else //generic
                {
                    myEmail.Message += "We are happy that you will be continuing with the Novartis Oncology Access (NOA) Program. \n\n";
                }
                myEmail.Message += "Initial Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y") + "\n";
                myEmail.Message += "New Period Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
                myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
                myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
                /*if (this.CountryID == 102)
                {
                    myEmail.Message += "Your treating physician may change your dosage for medical reasons. In this case, it is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
                    myEmail.Message += "Every 12 months, your case will be re-assessed to determine your ongoing eligibility for the program. \n\n";
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ", or speak with your physician.";
                    myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
                }
                else*/ if (this.CountryID == 76)
                {
                    myEmail.Message += "Your treating physician may change your dosage for medical reasons. In this case, the donation dosage will automatically be adjusted with the new prescription. It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
                    if (this.YearlyReassess && this.Treatment != "Tasigna")// dont do this for tasigna
                    {
                        myEmail.Message += "Every 24 months, your financial information will be re-assessed to determine your ongoing eligibility for the program. Please be prepared to participate in the re-evaluation process.\n\n";
                        myEmail.Message += "Date of next financial assessment: " + this.ReassessDate.Day.ToString() + " " + this.ReassessDate.ToString("y") + "\n\n";
                        if (this.ReassessDate > this.StartDate && this.ReassessDate < this.EndDate)
                        {
                            myEmail.Message += "ALERT FOR REASSESSMENT OF YOUR CASE IN NOA \n\nBEFORE YOUR CASE CAN BE PROCESSED FOR THE NEXT REAPPROVAL PERIOD, YOU WILL NEED TO UNDERGO A FINANCIAL REASSESSMENT ONCE AGAIN WITH INDIABULLS. PLEASE CONTACT MAXINDIA FOR DETAILS AND ASSISTANCE ON 022 24932368 and 022 666 03320 / 21\n\n";
                        }

                    }

                    myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                    myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
                }
                else //generic
                {
                    myEmail.Message += "Your treating physician may change your dosage for medical reasons. In this case, it is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
                    if (this.patientDS.Tables[3].Rows.Count > 0)
                    {
                        myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ", or speak with your physician.";
                    }
                    else
                    {
                        myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ", or speak with your physician.";
                    }
                    myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
                }
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ReApprovalEmailPhysician(int pid, int count)
        {
            this.InflateEmail(pid);
            return this.ReApprovalEmailPhysician(count);
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ReApprovalEmailPhysician(int count)
        {
            if (this.FlagNOA)
            {
                if ((this.DonationLength != 12 && this.DonationLength != 370) || this.CountryID == 108)
                {
                    return this.NOAReApprovalEmailPhysician(count);
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";

            if (this.patientDS.Tables[16].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[16].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[16].Rows[i]["email"].ToString() != "")
                    {
                        if (myEmail.CC.Length > 0)
                            myEmail.CC += ";" + this.patientDS.Tables[16].Rows[i]["email"].ToString();
                        else
                            myEmail.CC = this.patientDS.Tables[16].Rows[i]["email"].ToString();
                    }
                }
            }

            myEmail.Subject = "GIPAP Patient Re-Approval - " + this.PIN;
            myEmail.Message = "GIPAP PATIENT RE-APPROVAL (PHYSICIAN)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString();
            if (this.patientDS.Tables[4].Rows[count]["phone"].ToString() != "")
            {
                myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[count]["phone"].ToString();
            }
            if (this.patientDS.Tables[4].Rows[count]["fax"].ToString() != "")
            {
                myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[count]["fax"].ToString();
            }
            myEmail.Message += "\n" + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "Your patient, " + this.FirstName + " " + this.LastName + ", has re-qualified for the Glivec(R) International Patient Assistance Program (GIPAP) of Novartis Pharma AG.";
            myEmail.Message += " The supply of Glivec(R) will be delivered to you by Novartis for the patient’s use at no charge.  We hope that Glivec(R) will enhance your patient’s quality of life as it has for many others.";
            myEmail.Message += "\n\n---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
            myEmail.Message += "\nOriginal GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage + "/day\n\n";

            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOAReApprovalEmailPhysician(int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";

            if (this.patientDS.Tables[16].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[16].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[16].Rows[i]["email"].ToString() != "")
                    {
                        if (myEmail.CC.Length > 0)
                            myEmail.CC += ";" + this.patientDS.Tables[16].Rows[i]["email"].ToString();
                        else
                            myEmail.CC = this.patientDS.Tables[16].Rows[i]["email"].ToString();
                    }
                }
            }


            if (this.CountryID == 108)
            {
                myEmail.Subject = "PAT Patient Re-Approval - " + this.PIN;
                myEmail.Message = "PAT PATIENT RE-APPROVAL (PHYSICIAN)\n\n";
            }
            else if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Patient Re-Approval - " + this.PIN;
                    myEmail.Message = "TIPAP PATIENT RE-APPROVAL (PHYSICIAN)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Patient Re-Approval - " + this.PIN;
                    myEmail.Message = "NOA Tasigna PATIENT RE-APPROVAL (PHYSICIAN)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "NOA Patient Re-Approval - " + this.PIN;
                myEmail.Message = "NOA PATIENT RE-APPROVAL (PHYSICIAN)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString();
            if (this.patientDS.Tables[4].Rows[count]["phone"].ToString() != "")
            {
                myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[count]["phone"].ToString();
            }
            if (this.patientDS.Tables[4].Rows[count]["fax"].ToString() != "")
            {
                myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[count]["fax"].ToString();
            }
            myEmail.Message += "\n" + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "Your patient, " + this.FirstName + " " + this.LastName;
            if (this.CountryID == 108)
            {
                myEmail.Message += ", has re-qualified for the Novartis Program: Programa de Asistencia Temporal (PAT).";
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += ", has re-qualified for the next 120 day re-approval period in the Tasigna(R) International Patient Assistance Program (TIPAP) of Novartis Pharma AG.  We hope that " + this.Treatment + "(R) will enhance your patient’s quality of life as it has for many others.";
            }
            else
            {
                myEmail.Message += ", has re-qualified for the next 120 day re-approval period in the Novartis Oncology Access (NOA) Program of Novartis Pharma AG.  We hope that " + this.Treatment + "(R) will enhance your patient’s quality of life as it has for many others.";
            }
            myEmail.Message += "\n\n---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
            myEmail.Message += "\nOriginal Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nSupply  Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage + "/day\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ApprovalEmailCPO(int pid, int count)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                return this.NOAApprovalEmailCPO(count);
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    myEmail.To = this.patientDS.Tables[9].Rows[count]["email"].ToString();
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }

            myEmail.Subject = "GIPAP New Patient Approval - " + this.PIN;
            if (this.CurrentCMLPhase == "Blast Crisis")
            {
                myEmail.Subject += " - Blast Crisis";
            }
            myEmail.Message = "GIPAP NEW PATIENT APPROVAL (NOVARTIS)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "Patient " + this.PIN + " has qualified for the Glivec(R) International Patient Assistance Program (GIPAP).  ";
            myEmail.Message += "Please ensure that the physician receives the first 120 day supply of Glivec(R) for the approved patient.  Continued patient eligibility in GIPAP is determined by physician evaluation of the patient.  We will send you a Re-Approval notification when the next 120 day supply is authorized.\n\n";
            myEmail.Message += "If there is a need to contact the treating physician regarding this patient’s supply of Glivec(R), please refer to the patient PIN# in your communication.  This will help to protect patient confidentiality.\n\n";
            myEmail.Message += "---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient PIN: " + this.PIN;
            myEmail.Message += "\nDiagnosis: " + this.Diagnosis;
            myEmail.Message += "\nOriginal GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage + "/day\n\n";

            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOAApprovalEmailCPO(int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    myEmail.To = this.patientDS.Tables[9].Rows[count]["email"].ToString();
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP New Patient Approval - " + this.PIN;
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna New Patient Approval - " + this.PIN;
                }
            }
            else if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "PAT New Patient Approval - " + this.PIN;
            }
            else if (this.DonationLength == 12 || this.DonationLength == 370)
            {
                myEmail.Subject = "NOA / GIPAP New Patient Approval - " + this.PIN;
            }
            else
            {
                myEmail.Subject = "NOA New Patient Approval - " + this.PIN;
            }
            if (this.CurrentCMLPhase == "Blast Crisis")
            {
                myEmail.Subject += " - Blast Crisis";
            }
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Message = "TIPAP NEW PATIENT APPROVAL (NOVARTIS)\n\n";
                }
                else
                {
                    myEmail.Message = "NOA Tasigna NEW PATIENT APPROVAL (NOVARTIS)\n\n";
                }
            }
            else if (this.CountryID == 108)//mexico
            {
                myEmail.Message = "PAT NEW PATIENT APPROVAL (NOVARTIS)\n\n";
            }
            else if (this.DonationLength == 12 || this.DonationLength == 370)
            {
                myEmail.Message = "NOA / GIPAP NEW PATIENT APPROVAL (NOVARTIS)\n\n";
            }
            else
            {
                myEmail.Message = "NOA NEW PATIENT APPROVAL (NOVARTIS)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["city"].ToString() != "")
                {
                    myEmail.Message += "\nCity: " + this.patientDS.Tables[4].Rows[i]["city"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "Patient " + this.PIN + " has qualified for ";
            if (this.CountryID == 102)
            {
                myEmail.Message += "NOA " + this.Treatment + ". Please ensure that the NOA physician receives appropriate supply of " + this.Treatment + "® under this co-pay program for the approved patient.  A re-approval notification will be sent to you when the next 4 month period is authorized.\n\n";
            }
            else if (this.CountryID == 108)
            {
                myEmail.Message += " the Novartis program: Novartis Oncology Temporal Assistance Program (PAT) (Glivec ®). Please ensure that the PAT physician receives appropriate supply of Glivec® under this temporal program for the approved patient.  A re-approval notification will be sent to you when the next 4 month period is authorized.\n\n";
            }
            else if (this.CountryID == 76)
            {
                myEmail.Message += "a donation of " + this.Treatment + "® with a ";
                myEmail.Message += this.DonationScheme();
                if (this.DonationLength == 12 || this.DonationLength == 370)
                {
                    myEmail.Message += " donation scheme. Please ensure that Novartis C&F outlet receives the first 120 day supply of " + this.Treatment + "® for the approved patient.  ";
                }
                else if (this.DonationLength == 0)
                {
                    myEmail.Message += " payment scheme. Please ensure that Novartis C&F outlet receives the first 120 day supply of " + this.Treatment + "® for the approved patient.  ";
                }
                else
                {
                    myEmail.Message += " co-pay/donation scheme. Please ensure that Novartis C&F outlet receives appropriate supply of " + this.Treatment + "® for the approved patient according to the " + this.PaymentOption + " payment option.  ";
                }
                myEmail.Message += "Continued patient eligibility is determined by yearly financial evaluation and quarterly physician medical evaluation of the patient.  A re-approval notification will be sent to you when the next ";
                if (this.DonationLength == 12 || this.DonationLength == 370)
                {
                    myEmail.Message += "120 day supply is authorized.\n\n";
                }
                else
                {
                    myEmail.Message += "120 day supply is authorized.\n\n";
                }
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna") //tipap
            {
                myEmail.Message += "TIPAP. Please ensure that the TIPAP physician receives appropriate supply of " + this.Treatment + "® under this program for the approved patient.  A re-approval notification will be sent to you when the next 4 month period is authorized.\n\n";
            }
            else //south africa, generic
            {
                myEmail.Message += "NOA " + this.Treatment + ". Please ensure that the NOA physician receives appropriate supply of " + this.Treatment + "® under this program for the approved patient.  A re-approval notification will be sent to you when the next 4 month period is authorized.\n\n";
            }
            myEmail.Message += "If there is a need to contact the treating physician regarding this patient’s supply of " + this.Treatment + "®, please refer to the patient PIN# in your communication.  This will help to protect patient confidentiality.\n\n";
            myEmail.Message += "---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient PIN: " + this.PIN;
            myEmail.Message += "\nDiagnosis: " + this.Diagnosis;
            myEmail.Message += "\nOriginal Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage + "/day\n\n";

            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ReApprovalEmailCPO(int pid, int count)
        {
            this.InflateEmail(pid);
            return this.ReApprovalEmailCPO(count);
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ReApprovalEmailCPO(int count)
        {
            if (this.FlagNOA)
            {
                if ((this.DonationLength != 12 && this.DonationLength != 370) || this.CountryID == 108)
                {
                    return this.NOAReApprovalEmailCPO(count);
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    myEmail.To = this.patientDS.Tables[9].Rows[count]["email"].ToString();
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }
            myEmail.Subject = "GIPAP Patient Re-Approval - " + this.PIN;
            myEmail.Message = "GIPAP PATIENT RE-APPROVAL (NOVARTIS)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "Patient " + this.PIN + " has re-qualified for the Glivec(R) International Patient Assistance Program (GIPAP).  ";
            myEmail.Message += "Please make available to the physician the additional supply of Glivec(R) for the new 120 day approval period.";
            myEmail.Message += "\n\n---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient PIN: " + this.PIN;
            myEmail.Message += "\nDiagnosis: " + this.Diagnosis;
            myEmail.Message += "\nOriginal GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage + "/day\n\n";

            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOAReApprovalEmailCPO(int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    myEmail.To = this.patientDS.Tables[9].Rows[count]["email"].ToString();
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }
            if (this.CountryID == 108)
            {
                myEmail.Subject = "PAT Patient Re-Approval - " + this.PIN;
                myEmail.Message = "PAT PATIENT RE-APPROVAL (NOVARTIS)\n\n";
            }
            else if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Patient Re-Approval - " + this.PIN;
                    myEmail.Message = "TIPAP PATIENT RE-APPROVAL (NOVARTIS)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Patient Re-Approval - " + this.PIN;
                    myEmail.Message = "NOA Tasigna PATIENT RE-APPROVAL (NOVARTIS)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "NOA Patient Re-Approval - " + this.PIN;
                myEmail.Message = "NOA PATIENT RE-APPROVAL (NOVARTIS)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "Patient " + this.PIN;
            if (this.CountryID == 102)
            {
                myEmail.Message += " has re-qualified for the next 4 month re-approval period in the Novartis Oncology Access (NOA) Program. Please ensure that the physician receives the appropriate supply of " + this.Treatment + "(R) for the patient according to the co-pay program for which s/he is approved. Continued patient eligibility is determined by yearly financial evaluation and physician medical evaluation of the patient three times a year.\n\n";
            }
            else if (this.CountryID == 108)
            {
                myEmail.Message += " has re-qualified for the next 4 month re-approval period in the Novartis Program: Programa de Asistencia Temporal (PAT). Please ensure that the physician receives the appropriate supply of Glivec(R)for the patient. \n\n";
            }
            else if (this.CountryID == 76)
            {
                myEmail.Message += " has re-qualified for the next 120 day re-approval period in the Novartis Oncology Access (NOA) Program. Please ensure that Novartis C&F outlet receives the appropriate supply of " + this.Treatment + "(R) for the patient according to ";
                myEmail.Message += this.DonationScheme();
                if (this.DonationLength == 0)
                {
                    myEmail.Message += " payment scheme ";
                }
                else
                {
                    myEmail.Message += " co-pay/donation scheme ";
                }
                myEmail.Message += "for which s/he is approved, and the " + this.PaymentOption + " payment option chosen. Continued patient eligibility is determined by yearly financial evaluation and physician medical evaluation of the patient three times a year.\n\n";
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += " has re-qualified for the next 4 month re-approval period in the Tasigna(R) International Patient Assistance Program (TIPAP) of Novartis Pharma AG. Please ensure that the physician receives the appropriate supply of " + this.Treatment + "(R) for the patient. Continued patient eligibility is determined by physician medical evaluation of the patient three times a year.\n\n";
            }
            else //generic
            {
                myEmail.Message += " has re-qualified for the next 4 month re-approval period in the Novartis Oncology Access (NOA) Program. Please ensure that the physician receives the appropriate supply of " + this.Treatment + "(R) for the patient. Continued patient eligibility is determined by physician medical evaluation of the patient three times a year.\n\n";
            }
            myEmail.Message += "If there is a need to contact the treating physician regarding this patient’s supply of " + this.Treatment + "®, please refer to the patient PIN# in your communication.  This will help to protect patient confidentiality.\n\n";
            myEmail.Message += "\n\n---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient PIN: " + this.PIN + "\n\n";
            myEmail.Message += "\nDiagnosis: " + this.Diagnosis;
            myEmail.Message += "\nOriginal Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nSupply Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Dosage Prescribed: " + this.CurrentDosage + "/day\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ExtentionEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[5].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[5].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To = this.patientDS.Tables[5].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (myEmail.To == "")
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.From = "gipap@themaxfoundation.org";

            myEmail.Subject = "GIPAP Extension of Approval Period – " + this.PIN.ToString();
            myEmail.Message = "GIPAP EXTENSION OF APPROVAL PERIOD (PATIENT)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + "\n";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + "\n";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
            myEmail.Message += "We have been advised that you have not completely utilized the 120 day supply of Glivec given to your physician for your use. For that reason we are extending your current approval period for GIPAP.\n\n";
            myEmail.Message += "Your current approval period is now extended until " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "---PATIENT INFORMATION---\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName + ":\n";
            myEmail.Message += "Original GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nNew Period Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " to " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y");
            myEmail.Message += "\n\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            myEmail.Message += "It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ExtentionEmailPhysician(int pid, int count)
        {
            this.InflateEmail(pid);
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";

            myEmail.Subject = "GIPAP Extension of Approval Period – " + this.PIN.ToString();
            myEmail.Message = "GIPAP EXTENSION OF APPROVAL PERIOD (PHYSICIAN)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString();
            if (this.patientDS.Tables[4].Rows[count]["phone"].ToString() != "")
            {
                myEmail.Message += "\n" + this.patientDS.Tables[4].Rows[count]["phone"].ToString();
            }
            if (this.patientDS.Tables[4].Rows[count]["fax"].ToString() != "")
            {
                myEmail.Message += "\n" + this.patientDS.Tables[4].Rows[count]["fax"].ToString();
            }
            if (this.patientDS.Tables[4].Rows[count]["mobile"].ToString() != "")
            {
                myEmail.Message += "\n" + this.patientDS.Tables[4].Rows[count]["mobile"].ToString();
            }
            myEmail.Message += "\n\nDear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "We have been advised that for reasons out of your control, " + this.FirstName + " " + this.LastName + " has not yet completely utilized the 120 day supply of Glivec given to you for his/her use. For that reason we are extending this patient's current approval period for GIPAP.\n\n";
            myEmail.Message += "Current approval period is now extended until " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "---PATIENT INFORMATION---\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName + ":\n";
            myEmail.Message += "Original GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nNew Period Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " to " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y");
            myEmail.Message += "\n\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            myEmail.Message += "No new supply of Glivec (beyond the original 120 day supply) will be sent for this patient at this time. If you believe that you will need more medication before ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + ", please let us know.\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ExtentionEmailCPO(int pid, int count)
        {
            this.InflateEmail(pid);
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    myEmail.To = this.patientDS.Tables[9].Rows[count]["email"].ToString();
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }

            myEmail.Subject = "GIPAP Extension of Approval Period – " + this.PIN.ToString();
            myEmail.Message = "GIPAP EXTENSION OF APPROVAL PERIOD – (NOVARTIS)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "\n\nDear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + "\n\n";
            myEmail.Message += "We have been advised that patient " + this.PIN + " has not yet completely utilized the 120 day supply of Glivec given to him/her. For that reason we are extending this patient's current approval period for GIPAP.\n\n";
            myEmail.Message += "Current approval period is now extended until " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "---PATIENT INFORMATION---\n\n";
            myEmail.Message += "Patient PIN: " + this.PIN + "\n";
            myEmail.Message += "Original GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nNew Period Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " to " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y");
            myEmail.Message += "\n\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            myEmail.Message += "No new supply of Glivec will be needed for this patient until a re-approval letter is issued or a dosage increase is requested.\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email DoseChangeEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                if ((this.DonationLength != 12 && this.DonationLength != 370) || this.CountryID == 108)
                {
                    return this.NOADoseChangeEmailPatient(pid);
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[3].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0 && this.patientDS.Tables[5].Rows[0]["email"].ToString() != "")
            {
                myEmail.To = this.patientDS.Tables[5].Rows[0]["email"].ToString();
            }
            else
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.Subject = "GIPAP Dosage Change - " + this.PIN;
            myEmail.Message = "GIPAP DOSAGE CHANGE (PATIENT)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Physician Name: ";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "\n" + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
            }
            myEmail.Message += "\n\nPatient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
            myEmail.Message += "This notification reflects a dosage adjustment in your treatment with Glivec as requested by your physician.\n\n";
            myEmail.Message += "Your GIPAP approval period remains the same. If needed, you should receive more Glivec to support you until " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + ".\n\n";
            myEmail.Message += "\n\n---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
            myEmail.Message += "\nOriginal GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "New Dosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            myEmail.Message += "It is very important that you follow your physician's instructions carefully and that you keep all your medical appointments.\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOADoseChangeEmailPatient(int pid)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[3].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0 && this.patientDS.Tables[5].Rows[0]["email"].ToString() != "")
            {
                myEmail.To = this.patientDS.Tables[5].Rows[0]["email"].ToString();
            }
            else
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.From = "gipap@themaxfoundation.org";
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Dosage Change - " + this.PIN;
                    myEmail.Message = "TIPAP DOSAGE CHANGE (PATIENT)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Dosage Change - " + this.PIN;
                    myEmail.Message = "NOA Tasigna DOSAGE CHANGE (PATIENT)\n\n";
                }
            }
            else if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "PAT CAMBIO DE DOSIS - " + this.PIN;
                myEmail.Message = "PAT CAMBIO DE DOSIS (PACIENTE)\n\n";
            }
            else
            {
                myEmail.Subject = "NOA Dosage Change - " + this.PIN;
                myEmail.Message = "NOA DOSAGE CHANGE (PATIENT)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            if (this.CountryID == 108)
            {
                myEmail.Message += "Nombre del Médico: ";
            }
            else
            {
                myEmail.Message += "Physician Name: ";
            }
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "\n" + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
            }
            myEmail.Message += "\n\nPatient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            if (this.CountryID == 108) // mexico
            {
                myEmail.Message += "Estimado " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "Esta notificación refleja un ajuste de dosis en el tratamiento con Glivec  solicitado por su médico.\n\n";
                myEmail.Message += "Su periodo en PAT permanece sin cambios.\n\n";
                myEmail.Message += "\n\n---- INFORMACIÓN DEL PACIENTE ----\n\n";
                myEmail.Message += "Nombre del Paciente: " + this.FirstName + " " + this.LastName;
                myEmail.Message += "\nFecha de aprobación inicial del paciente: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
                myEmail.Message += "\nPeriodo de re-aprobación actual: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
                myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
                myEmail.Message += "Nueva Dosis Prescrita: " + this.CurrentDosage.ToString() + "/day\n\n";
                myEmail.Message += "Es muy importante que siga las instrucciones de su médico al pie de la letra y que asista a todas sus citas.\n\n";
                myEmail.Message += "Si tiene alguna pregunta o aclaración, por favor contáctese con Fundación Max en México al 52 5595 85 66 o stefany.green@THEMAXFOUNDATION.ORG , o en Monterrey al 52 81 8317 4292 o  cynthia.figueroa@THEMAXFOUNDATION.ORG, o bien comuníquese con su médico tratante.";
                myEmail.Message += "\n\nAtentamente, \n\nFundación Max";
            }
            else
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "This notification reflects a dosage adjustment in your treatment with " + this.Treatment + " as requested by your physician.\n\n";
                if (this.CountryID == 102)
                {
                    myEmail.Message += "Your NOA period remains the same. \n\nFor any additional drugs required in your new dosage, you will need to purchase the additional drug supply based on NOA program terms and conditions (if applicable).";
                }
                else if (this.CountryID == 76)
                {
                    myEmail.Message += "Your approval period remains the same. If needed, you should receive more " + this.Treatment + " to support you until " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + ".\n\n";
                }
                else if (this.CountryID == 162 && this.Treatment == "Tasigna") //tipap
                {
                    myEmail.Message += "Your TIPAP period remains the same. ";
                }
                else //generic
                {
                    myEmail.Message += "Your NOA period remains the same. ";
                }
                myEmail.Message += "\n\n---- PATIENT INFORMATION ----\n\n";
                myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
                myEmail.Message += "\nOriginal Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
                myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
                myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
                myEmail.Message += "New Dosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
                myEmail.Message += "It is very important that you follow your physician's instructions carefully and that you keep all your medical appointments.\n\n";
                if (this.CountryID == 76)
                {
                    myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                    myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
                }
                else //generic
                {
                    if (this.patientDS.Tables[3].Rows.Count > 0)
                    {
                        myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ", or speak with your physician.";
                    }
                    else
                    {
                        myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ", or speak with your physician.";
                    }
                    myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
                }
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email DoseChangeEmailPhysician(int pid, int count)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                if ((this.DonationLength != 12 && this.DonationLength != 370) || this.CountryID == 108)
                {
                    return this.NOADoseChangeEmailPhysician(pid, count);
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";

            if (this.patientDS.Tables[16].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[16].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[16].Rows[i]["email"].ToString() != "")
                    {
                        if (myEmail.CC.Length > 0)
                            myEmail.CC += ";" + this.patientDS.Tables[16].Rows[i]["email"].ToString();
                        else
                            myEmail.CC = this.patientDS.Tables[16].Rows[i]["email"].ToString();
                    }
                }
            }

            myEmail.Subject = "GIPAP Dosage Change - " + this.PIN;
            myEmail.Message = "GIPAP DOSAGE CHANGE (PHYSICIAN)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "This notification reflects a dosage adjustment for your patient, " + this.FirstName + " " + this.LastName + " as per your request. The approval period remains the same. Please alert the Novartis representative who is in charge of supplying the Glivec(R) if more will be needed to support this patient until " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + ".\n\n";
            myEmail.Message += "\n\n---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
            myEmail.Message += "\nOriginal GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "New Dosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            myEmail.Message += "Supplies of Glivec(R) are sent to you to be used exclusively by this patient. Please note that if for any reason the patient needs to discontinue the treatment, all leftover Glivec(R) in your possession should be sent back to the supplying Novartis office immediately.  However, Glivec that has been in the patient’s possession should be returned to your clinic and destroyed.\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOADoseChangeEmailPhysician(int pid, int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";

            if (this.patientDS.Tables[16].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[16].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[16].Rows[i]["email"].ToString() != "")
                    {
                        if (myEmail.CC.Length > 0)
                            myEmail.CC += ";" + this.patientDS.Tables[16].Rows[i]["email"].ToString();
                        else
                            myEmail.CC = this.patientDS.Tables[16].Rows[i]["email"].ToString();
                    }
                }
            }


            if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "PAT Dosage Change - " + this.PIN;
                myEmail.Message = "PAT DOSAGE CHANGE (PHYSICIAN)\n\n";
            }
            else if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Dosage Change - " + this.PIN;
                    myEmail.Message = "TIPAP DOSAGE CHANGE (PHYSICIAN)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Dosage Change - " + this.PIN;
                    myEmail.Message = "NOA Tasigna DOSAGE CHANGE (PHYSICIAN)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "NOA Dosage Change - " + this.PIN;
                myEmail.Message = "NOA DOSAGE CHANGE (PHYSICIAN)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "This notification reflects a dosage adjustment for your patient, " + this.FirstName + " " + this.LastName + " as per your request. The approval period remains the same. Novartis has been alerted to this change and will make available the appropriate additional supply of " + this.Treatment + "®";
            if (this.CountryID == 102)
            {
                myEmail.Message += " based on NOA co-pay program terms and conditions.";
            }
            else if (this.CountryID == 76)
            {
                myEmail.Message += ".";
            }
            else //south africa, generic
            {
                myEmail.Message += " based on program terms and conditions.";
            }
            myEmail.Message += "\n\n---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
            myEmail.Message += "\nInitial Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "New Dosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "Please note that any excess supply of " + this.Treatment + "(R) should be handled in accordance with the protocol specified in the NOA Physician MOU.\n\n";
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email DoseChangeEmailCPO(int pid, int count)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                if ((this.DonationLength != 12 && this.DonationLength != 370) || this.CountryID == 108)
                {
                    return this.NOADoseChangeEmailNovartis(pid, count);
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    myEmail.To = this.patientDS.Tables[9].Rows[count]["email"].ToString();
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }

            myEmail.Subject = "GIPAP Dosage Change – " + this.PIN.ToString();
            myEmail.Message = "GIPAP DOSAGE CHANGE  (NOVARTIS)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient PIN Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "This notification reflects a dosage adjustment for patient " + this.PIN + " as per (his/her) physician's request. The approval period remains the same. You will be notified by the physician if more Glivec(R) will be needed to support this patient until " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + ".\n\n";
            myEmail.Message += "---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient PIN: " + this.PIN;
            myEmail.Message += "\nOriginal GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "New Dosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOADoseChangeEmailNovartis(int pid, int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    myEmail.To = this.patientDS.Tables[9].Rows[count]["email"].ToString();
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }
            if (this.CountryID == 108)
            {
                myEmail.Subject = "PAT Dosage Change – " + this.PIN.ToString();
                myEmail.Message = "PAT DOSAGE CHANGE  (NOVARTIS)\n\n";
            }
            else if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Dosage Change – " + this.PIN.ToString();
                    myEmail.Message = "TIPAP DOSAGE CHANGE  (NOVARTIS)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Dosage Change – " + this.PIN.ToString();
                    myEmail.Message = "NOA Tasigna DOSAGE CHANGE  (NOVARTIS)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "NOA Dosage Change – " + this.PIN.ToString();
                myEmail.Message = "NOA DOSAGE CHANGE  (NOVARTIS)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient PIN Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "This notification reflects a dosage adjustment for patient " + this.PIN + " as per (his/her) physician's request. The approval period remains the same. ";
            if (this.CountryID == 76)
            {
                myEmail.Message += "Please ensure that Novartis C&F outlet receives the appropriate additional supply of " + this.Treatment + "®.\n\n";
            }
            else //generic
            {
                myEmail.Message += "Please ensure that his/her treating physician receives the appropriate additional supply of " + this.Treatment + "®.\n\n";
            }
            myEmail.Message += "---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient PIN: " + this.PIN;
            myEmail.Message += "\nInitial Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "New Dosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email CloseEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                if ((this.DonationLength != 12 && this.DonationLength != 370) || this.CountryID == 108)
                {
                    return this.NOACloseEmailPatient(pid);
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.To = this.Email;
            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[3].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.CC += "; " + this.patientDS.Tables[3].Rows[i]["email"].ToString();
                    }
                }
            }
            myEmail.Subject = "GIPAP Closed Case Notification – " + this.PIN;
            myEmail.Message = "GIPAP PATIENT CLOSED CASE NOTIFICATION (PATIENT)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
            if (this.StatusReason == "No re-evaluation information provided")
            {
                myEmail.Message += "We have closed your case in GIPAP as your current approval period has ended.  We have not received responses to our requests for re-evaluation information from your physician.  Please contact your physician regarding further evaluation.\n\n";
            }
            else
            {
                myEmail.Message += "We have closed your case in GIPAP due to the following reason: " + this.StatusReason + "\n\n";
            }
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOACloseEmailPatient(int pid)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.To = this.Email;
            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[3].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.CC += "; " + this.patientDS.Tables[3].Rows[i]["email"].ToString();
                    }
                }
            }
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Closed Case Notification – " + this.PIN;
                    myEmail.Message = "TIPAP PATIENT CLOSED CASE NOTIFICATION (PATIENT)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasinga Closed Case Notification – " + this.PIN;
                    myEmail.Message = "NOA Tasigna PATIENT CLOSED CASE NOTIFICATION (PATIENT)\n\n";
                }
            }
            else if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "PAT NOTIFIACIÓN DE CIERRE DE CASO – " + this.PIN;
                myEmail.Message = "PAT NOTIFIACIÓN DE CIERRE DE CASO (PACIENTE)\n\n";
            }
            else
            {
                myEmail.Subject = "NOA Closed Case Notification – " + this.PIN;
                myEmail.Message = "NOA PATIENT CLOSED CASE NOTIFICATION (PATIENT)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "PIN: (TMF) " + this.PIN + "\n\n";
                myEmail.Message += "--Información del Médico—-\n\n";
            }
            else
            {
                myEmail.Message += "Patient Identification Number: " + this.PIN + "\n\n";
                myEmail.Message += "--Physician Information--\n\n";
            }
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "Estimado " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "Cerramos su caso en el Programa de Asistencia Temporal de Novartis (PAT) debido a la siguiente razón: Razones del cierre del caso: " + this.StatusReason + "\n\n";
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "We have closed your case in the Tasigna(R) International Patient Assistance Program (TIPAP) due to the following reason: " + this.StatusReason + "\n\n";
            }
            else
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
                myEmail.Message += "We have closed your case in the Novartis Oncology Access (NOA) Program due to the following reason: " + this.StatusReason + "\n\n";
            }

            if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "Si tiene alguna pregunta o aclaración, por favor contáctese con Fundación Max en México al 52 5595 85 66 o stefany.green@THEMAXFOUNDATION.ORG , o en Monterrey al 52 81 8317 4292 o  cynthia.figueroa@THEMAXFOUNDATION.ORG, o bien comuníquese con su médico tratante.";
                myEmail.Message += "\n\nAtentamente, \n\nFundación Max";
            }
            else if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email CloseEmailPhysician(int pid, int count)
        {
            this.InflateEmail(pid);
            return this.CloseEmailPhysician(count);
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email CloseEmailPhysician(int count)
        {
            if (this.FlagNOA)
            {
                if ((this.DonationLength != 12 && this.DonationLength != 370) || this.CountryID == 108)
                {
                    return this.NOACloseEmailPhysician(count);
                }
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";
            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[3].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.CC += ";" + this.patientDS.Tables[3].Rows[i]["email"].ToString();
                    }
                }
            }

            if (this.patientDS.Tables[16].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[16].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[16].Rows[i]["email"].ToString() != "")
                    {
                        if (myEmail.CC.Length > 0)
                            myEmail.CC += ";" + this.patientDS.Tables[16].Rows[i]["email"].ToString();
                        else
                            myEmail.CC = this.patientDS.Tables[16].Rows[i]["email"].ToString();
                    }
                }
            }
            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.Subject = "GIPAP Closed Case Notification – " + this.PIN;
            myEmail.Message = "GIPAP PATIENT CLOSED CASE NOTIFICATION (PHYSICIAN)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName + "\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ",\n\n";
            if (this.StatusReason == "Duplicate Patient")
            {
                myEmail.Message += "Duplicate applications were submitted and approved for " + this.FirstName + " " + this.LastName + ".  We have closed the GIPAP case for the PIN: " + this.PIN + ".  The case will continue to be managed under the original PIN.\n\n";
            }
            else
            {
                myEmail.Message += "We have closed the GIPAP case for your patient " + this.FirstName + " " + this.LastName + ".\n\n";
                myEmail.Message += "Reason for closing: " + this.StatusReason + "\n\n";
                if (this.StatusReason == "No re-evaluation information provided")
                {
                    myEmail.Message += "We are closing this case as we have not received input in response to re-evaluation reminders.  We are assuming that contact with the patient has been lost.  If and when the patient re-surfaces the case may be re-activated provided the patient remains eligible on all counts.\n\n";
                    myEmail.Message += "The request for reactivation may be submitted to TMF by logging into the Patient Assistance Tracking System (PATS) at www.maxaid.org and selecting the “Reactivate Patient Case” action from your “my Patients” menu.\n\n";
                }
                else if (this.StatusReason == "Lost contact with patient")
                {
                    myEmail.Message += "If and when the patient re-surfaces the case may be re-activated provided the patient remains eligible on all counts.\n\n";
                    myEmail.Message += "The request for reactivation may be submitted to TMF by logging into the Patient Assistance Tracking System (PATS) at www.maxaid.org and selecting the “Reactivate Patient Case” action from your “my Patients” menu.\n\n";
                }
                else if (this.StatusReason == "Clinical Reason")
                {
                    myEmail.Message += "If the clinical reason for closure is other than death, you may recommend the patient to restart Glivec by logging into the Patient Assistance Tracking System (PATS) at www.maxaid.org and selecting the “Reactivate Patient Case” action from your “my Patients” menu. The patient’s case will be reviewed for re-activation at that time.\n\n";
                }
                else
                {
                    myEmail.Message += "The patient's case in GIPAP will be reviewed for re-activation if and when we receive your recommendation for the patient to restart Glivec.\n\n";
                    myEmail.Message += "The request for reactivation may be submitted to TMF by logging into the Patient Assistance Tracking System (PATS) at www.maxaid.org and selecting the “Reactivate Patient Case” action from your “my Patients” menu.\n\n";
                }
                myEmail.Message += "The supply of Glivec(R) was sent to you exclusively to be used by this patient.  In handling your patient’s leftover Glivec(R), please act in accordance with the protocol outlined in the Qualified Institution-Physician MOU.\n\n";
                if (this.StatusReason == "Clinical Reason")
                {
                    myEmail.Message += "Please remember that you must report to Novartis within 24 hours of notification, any information concerning a suspected serious adverse event (suspected SAE) related to the use of Glivec by completing the Novartis suspected SAE form. This shall include your assessment of the relationship of the event to treatment with Glivec. \n";
                    myEmail.Message += "In countries where Novartis has local Novartis Country Pharma Organizations (CPO's), suspected SAE shall be reported to the Novartis CPO's Integrated Medical Safety Pharmacovigilance Operations (IMS/PVO) Desk. The suspected SAE form shall be sent by fax to the number provided by Novartis. \n";
                    myEmail.Message += "In countries where Novartis has no CPO's, SAE form shall be reported to the Novartis Central processing site in Horsham, UK, marked for the attention of Novartis IMS Safety Data Management, Horsham, UK. (fax number :+44 1403 32 3500 or +44 1403 32 4500 ) \n";
                    myEmail.Message += "'Serious' as used in this section refers to any experience, which is fatal, life-threatening, results in permanent or substantial disability, in-patient or prolongation of hospitalization, suspected transmission of infectious agent or is a congenital anomaly. \n";
                    myEmail.Message += "'Suspected' as used in this section refers to the reasonable possibility of the drug being associated with the event. That is the temporal relationship of the clinical event to the drug administration makes a causal relationship possible, and other drugs, therapeutic interventions or underlying conditions do not provide a sufficient explanation for the observed event.\n\n";
                }
            }
            myEmail.Message += "If you have questions or concerns, please do not hesitate to communicate with us at Gipap@themaxfoundation.org.\n\n";
            myEmail.Message += "Regards,\n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOACloseEmailPhysician(int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[3].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.CC += "; " + this.patientDS.Tables[3].Rows[i]["email"].ToString();
                    }
                }
            }

            if (this.patientDS.Tables[16].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[16].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[16].Rows[i]["email"].ToString() != "")
                    {
                        if (myEmail.CC.Length > 0)
                            myEmail.CC += ";" + this.patientDS.Tables[16].Rows[i]["email"].ToString();
                        else
                            myEmail.CC = this.patientDS.Tables[16].Rows[i]["email"].ToString();
                    }
                }
            }

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Closed Case Notification – " + this.PIN;
                    myEmail.Message = "TIPAP PATIENT CLOSED CASE NOTIFICATION (PHYSICIAN)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Closed Case Notification – " + this.PIN;
                    myEmail.Message = "NOA Tasigna PATIENT CLOSED CASE NOTIFICATION (PHYSICIAN)\n\n";
                }
            }
            else if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "PAT Closed Case Notification – " + this.PIN;
                myEmail.Message = "PAT PATIENT CLOSED CASE NOTIFICATION (PHYSICIAN)\n\n";
            }
            else
            {
                myEmail.Subject = "NOA Closed Case Notification – " + this.PIN;
                myEmail.Message = "NOA PATIENT CLOSED CASE NOTIFICATION (PHYSICIAN)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName + "\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ",\n\n";
            if (this.CountryID == 108)
            {
                myEmail.Message += "We have closed the PAT case for your patient " + this.FirstName + " " + this.LastName + ".\n\n";
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += "We have closed the TIPAP case for your patient " + this.FirstName + " " + this.LastName + ".\n\n";
            }
            else
            {
                myEmail.Message += "We have closed the NOA case for your patient " + this.FirstName + " " + this.LastName + ".\n\n";
            }
            myEmail.Message += "Reason for closing: " + this.StatusReason + "\n\n";
            if (this.StatusReason == "No re-evaluation information provided")
            {
                myEmail.Message += "We are closing this case as we have not received input in response to re-evaluation reminders.  We are assuming that contact with the patient has been lost.  If and when the patient re-surfaces, the case may be re-activated provided the patient remains eligible on all counts.\n\n";
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += "The patient's case in TIPAP will be reviewed for re-activation if and when we receive your recommendation for the patient to restart " + this.Treatment + ".\n\n";
            }
            else if (this.CountryID != 108)//not for mexico
            {
                myEmail.Message += "The patient's case in NOA will be reviewed for re-activation if and when we receive your recommendation for the patient to restart " + this.Treatment + ".\n\n";
            }
            myEmail.Message += "A request for reactivation may be submitted by logging into the Patient Assistance Tracking System (PATS) at www.maxaid.org and selecting the “Reactivate Patient Case” action from the “My Patients” menu.\n\n";

            if (this.CountryID == 76)
            {
                myEmail.Message += "The supply of " + this.Treatment + "(R) was given for exclusive use by this patient.  In handling your patient’s leftover " + this.Treatment + "(R), please act in accordance with the protocol outlined in the NOA Physician MOU.\n\n";
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email CloseEmail(int pid)
        {
            this.InflateEmail(pid);
            return this.CloseEmail();
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email CloseEmail()
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    for (int i = 0; i < this.patientDS.Tables[9].Rows.Count; i++)
                    {
                        myEmail.To += this.patientDS.Tables[9].Rows[i]["email"].ToString() + "; ";
                    }
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }
            //noa partial donation and tasigna patients get this
            if (this.FlagNOA && this.DonationLength != 12 && this.DonationLength != 370)
            {
                if (this.Treatment == "Tasigna")
                {
                    if (this.CountryID == 162)
                    {
                        myEmail.Subject = "TIPAP Closed Case Notification – " + this.PIN;
                        myEmail.Message = "TIPAP PATIENT CLOSED CASE NOTIFICATION (NOVARTIS)\n\n";
                    }
                    else
                    {
                        myEmail.Subject = "NOA Tasigna Closed Case Notification – " + this.PIN;
                        myEmail.Message = "NOA Tasigna PATIENT CLOSED CASE NOTIFICATION (NOVARTIS)\n\n";
                    }
                }
                else
                {
                    myEmail.Subject = "NOA Closed Case Notification – " + this.PIN;
                    myEmail.Message = "NOA PATIENT CLOSED CASE NOTIFICATION (NOVARTIS)\n\n";
                }
            }
            else if (this.FlagNOA && this.CountryID == 108)//pat mexico
            {
                myEmail.Subject = "PAT Closed Case Notification – " + this.PIN;
                myEmail.Message = "PAT PATIENT CLOSED CASE NOTIFICATION (NOVARTIS)\n\n";
            }
            else
            {
                myEmail.Subject = "GIPAP Closed Case Notification – " + this.PIN;
                myEmail.Message = "GIPAP PATIENT CLOSED CASE NOTIFICATION (NOVARTIS)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "We would like to notify you that we have closed the case for Patient " + this.PIN + ".\n\n";
            myEmail.Message += "Reason for closing: " + this.StatusReason;
            if (this.CountryID == 76)
            {
                myEmail.Message += "\n\nWe have advised the physician to act in accordance with the protocol for leftover medication as outlined in the  Qualified Institution-Physician MOU for any leftover supply that is maintained for this patient by the physician.\n\n";
                myEmail.Message += "If you have questions or concerns, please do not hesitate to communicate with us at Gipap@themaxfoundation.org.\n\n";
                myEmail.Message += "Regards,\n\nThe Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ReactivationEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                return this.NOAReactivationEmailPatient();
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[5].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[5].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To = this.patientDS.Tables[5].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (myEmail.To == "")
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.From = "gipap@themaxfoundation.org";

            myEmail.Subject = "GIPAP Patient Reactivation - " + this.PIN;
            myEmail.Message = "GIPAP PATIENT REACTIVATION (PATIENT)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
            myEmail.Message += "Your case in the Glivec(R) International Patient Assistance Program (GIPAP) of Novartis Pharma AG has been reactivated.  As you remain eligible for the program, you will receive Glivec (R) at no charge.";
            myEmail.Message += "\n\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Reason for Reactivation: " + this.patientDS.Tables[12].Rows[0]["statusreason"].ToString();
            myEmail.Message += "\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            myEmail.Message += "Your treating physician may change your dosage for medical reasons. In this case, the donation dosage will automatically be adjusted with the new prescription.\n\n";
            myEmail.Message += "You will receive a 120 day supply of Glivec (R) through your physician.  To remain in the program, you will be required to visit your physician on a regular basis to check your progress.  You will be considered for your next 120 day supply of Glivec (R) based upon your physician’s determination of your eligibility.\n\n";
            myEmail.Message += "It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";

            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOAReactivationEmailPatient()
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[5].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[5].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To = this.patientDS.Tables[5].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (myEmail.To == "")
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.From = "gipap@themaxfoundation.org";

            if (this.CountryID == 108)
            {
                myEmail.Subject = "PAT Patient Reactivation - " + this.PIN;
                myEmail.Message = "PAT PATIENT REACTIVATION (PATIENT)\n\n";
            }
            else if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Patient Reactivation - " + this.PIN;
                    myEmail.Message = "TIPAP PATIENT REACTIVATION (PATIENT)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Patient Reactivation - " + this.PIN;
                    myEmail.Message = "NOA Tasigna PATIENT REACTIVATION (PATIENT)\n\n";
                }
            }
            else if (this.DonationLength == 12 || this.DonationLength == 370)
            {
                myEmail.Subject = "NOA / GIPAP Patient Reactivation - " + this.PIN;
                myEmail.Message = "NOA / GIPAP PATIENT REACTIVATION (PATIENT)\n\n";
            }
            else
            {
                myEmail.Subject = "NOA Patient Reactivation - " + this.PIN;
                myEmail.Message = "NOA PATIENT REACTIVATION (PATIENT)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "Physician Name: " + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString() + "\n\n";
            }
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "Estimado " + this.FirstName + " " + this.LastName + ":\n\n";

                myEmail.Message += "Nos es grato informarle que se ha re-activado con éxito en el Programa de Asistencia Temporal de Novartis (PAT). Por el tiempo que siga siendo elegible, recibirá gratuitamente Glivec.\n\n";

                myEmail.Message += "Periodo: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
                myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
                myEmail.Message += "\nDosis prescrita: " + this.CurrentDosage.ToString() + "/day\n\n";
                myEmail.Message += "Será necesario que asista con su médico tratante regularmente para revisar su progreso (mensualmente). Su médico tratante puede cambiar la dosis por razones médicas. Es muy importante que siga las instrucciones de su médico al pie de la letra y que asista a sus consultas, así como, regresar las cajas vacías de Glivec a su médico en la siguiente cita.\n\n";
                myEmail.Message += "Si tiene alguna pregunta o aclaración, por favor contáctese con Fundación Max en México al 52 5595 85 66 o stefany.green@THEMAXFOUNDATION.ORG , o en Monterrey al 52 81 8317 4292 o  cynthia.figueroa@THEMAXFOUNDATION.ORG, o bien comuníquese con su médico tratante.";
                myEmail.Message += "\n\nAtentamente, \n\nFundación Max";
            }
            else
            {
                myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";

                if (this.CountryID == 102)
                {
                    myEmail.Message += "We are happy to inform you that your Novartis Oncology Access (NOA) Program has been successfully re-activated.  NOA is a co-pay 1+1 program. For every 1 month purchased, Novartis will donate 1 month of supply.\n\n";
                }
                else if (this.CountryID == 76)
                {
                    if (!this.Fixed)
                    {
                        myEmail.Message += "At the outset, we would like to appreciate your cooperation to undergo financial evaluation with Indiabulls. Indiabulls, an independent third party financial evaluation agency, used World Health Organisation guidelines on drug access plans to evaluate their decision.  ";
                    }
                    myEmail.Message += "We are happy to inform you that you have qualified for reactivation of your case according to a ";
                    myEmail.Message += this.DonationScheme();
                    if (this.DonationLength == 12 || this.DonationLength == 370)
                    {
                        myEmail.Message += " donation scheme. ";
                    }
                    else if (this.DonationLength == 0)
                    {
                        myEmail.Message += " payment scheme. ";
                    }
                    else
                    {
                        myEmail.Message += " co-pay/donation scheme. Your co-pay will be achieved according to the " + this.PaymentOption + " payment option you have chosen.  ";
                    }
                    if (this.YearlyReassess)
                    {
                        myEmail.Message += " Every 24 months, your financial information will be re-assessed to determine your ongoing eligibility for the program.";
                    }
                }
                else if (this.CountryID == 162 && this.Treatment == "Tasigna") //tipap
                {
                    myEmail.Message += "We are happy to inform you that your Tasigna(R) International Patient Assistance Program (TIPAP) of Novartis Pharma AG has been successfully re-activated. \n\n";
                }
                else //generic
                {
                    myEmail.Message += "We are happy to inform you that your Novartis Oncology Access (NOA) Program has been successfully re-activated. \n\n";
                }
                myEmail.Message += "\n\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
                myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
                myEmail.Message += "\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
                if (this.CountryID == 76)
                {
                    myEmail.Message += "To remain in the program, you will be required to visit your physician on a regular basis to check your progress.  Your treating physician may change your dosage for medical reasons. In this case, the donation dosage will automatically be adjusted with the new prescription. It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
                    myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                    myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
                }
                else //generic
                {
                    myEmail.Message += "You will be required to visit your physician on a regular basis to check your progress.  Your treating physician may change your dosage for medical reasons. It is very important that you follow your physician’s instructions carefully and that you keep all your medical appointments.\n\n";
                    if (this.patientDS.Tables[3].Rows.Count > 0)
                    {
                        myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ", or speak with your physician.";
                    }
                    else
                    {
                        myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ", or speak with your physician.";
                    }
                    myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
                }
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ReactivationEmailPhysician(int pid, int count)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                return this.NOAReactivationEmailPhysician(count);
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";

            myEmail.Subject = "GIPAP Patient Reactivation - " + this.PIN;
            if (this.CurrentCMLPhase == "Blast Crisis")
            {
                myEmail.Subject += " - Blast Crisis";
            }
            myEmail.Message = "GIPAP PATIENT REACTIVATION (PHYSICIAN)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString();
            if (this.patientDS.Tables[4].Rows[count]["phone"].ToString() != "")
            {
                myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[count]["phone"].ToString();
            }
            if (this.patientDS.Tables[4].Rows[count]["fax"].ToString() != "")
            {
                myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[count]["fax"].ToString();
            }
            myEmail.Message += "\n" + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "The case in the Glivec(R) International Patient Assistance Program (GIPAP) of Novartis Pharma AG for your patient, " + this.FirstName + " " + this.LastName + ", has been reactivated. A 120 day supply of Glivec(R) for your patient will be sent to you exclusively to be used by this patient.\n\n";
            myEmail.Message += "The supply of Glivec(R) was sent to you by Novartis exclusively to be used by this patient.  In handling your patient’s leftover Glivec(R), please act in accordance with the protocol outlined in the Qualified Institution-Physician MOU.\n\n";
            myEmail.Message += "---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
            myEmail.Message += "\nOriginal GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Reason for Reactivation: " + this.patientDS.Tables[12].Rows[0]["statusreason"].ToString();
            myEmail.Message += "\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";

            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOAReactivationEmailPhysician(int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";
            if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "PAT Patient Reactivation - " + this.PIN;
                myEmail.Message = "PAT PATIENT REACTIVATION (PHYSICIAN)\n\n";
            }
            else if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Patient Reactivation - " + this.PIN;
                    myEmail.Message = "TIPAP Tasigna PATIENT REACTIVATION (PHYSICIAN)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Patient Reactivation - " + this.PIN;
                    myEmail.Message = "NOA Tasigna PATIENT REACTIVATION (PHYSICIAN)\n\n";
                }
            }
            else if (this.DonationLength == 12 || this.DonationLength == 370)
            {
                myEmail.Subject = "NOA / GIPAP Patient Reactivation - " + this.PIN;
                myEmail.Message = "NOA / GIPAP PATIENT REACTIVATION (PHYSICIAN)\n\n";
            }
            else
            {
                myEmail.Subject = "NOA Patient Reactivation - " + this.PIN;
                myEmail.Message = "NOA PATIENT REACTIVATION (PHYSICIAN)\n\n";
            }
            if (this.CurrentCMLPhase == "Blast Crisis")
            {
                myEmail.Subject += " - Blast Crisis";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString();
            if (this.patientDS.Tables[4].Rows[count]["phone"].ToString() != "")
            {
                myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[count]["phone"].ToString();
            }
            if (this.patientDS.Tables[4].Rows[count]["fax"].ToString() != "")
            {
                myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[count]["fax"].ToString();
            }
            myEmail.Message += "\n" + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "Your patient, " + this.FirstName + " " + this.LastName;
            if (this.CountryID == 108) //mexico
            {
                myEmail.Message += " has been reactivated in Novartis Oncology Temporal Assistance Program (PAT).";
            }
            else if (this.CountryID == 76)
            {
                myEmail.Message += " has qualified for reactivation of their case with approval for a ";
                myEmail.Message += this.DonationScheme();
                if (this.DonationLength == 12 || this.DonationLength == 370)
                {
                    myEmail.Message += " donation scheme. ";
                }
                else if (this.DonationLength == 0)
                {
                    myEmail.Message += " payment scheme. ";
                }
                else
                {
                    myEmail.Message += " co-pay/donation scheme.  ";
                }
                if (this.YearlyReassess)
                {
                    myEmail.Message += " Every 24 months, the patient’s financial information will be re-assessed to determine ongoing eligibility for the program.  ";
                }
                if (!this.Fixed)
                {
                    myEmail.Message += "As known to you, the suitable donation program for your patient has been decided by an independent financial evaluation agency, Indiabulls, which has used World Health Organisation guidelines for drug access programs.";
                }
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna") //tipap
            {
                myEmail.Message += " has been reactivated in Tasigna(R) International Patient Assistance Program (TIPAP) of Novartis Pharma AG. ";
            }
            else //generic
            {
                myEmail.Message += " has been reactivated in Novartis Oncology Access (NOA) Program. ";
            }
            myEmail.Message += "\n\n---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient Name: " + this.FirstName + " " + this.LastName;
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y");
            myEmail.Message += "\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email ReactivationEmailCPO(int pid, int count)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                return this.NOAReactivationEmailCPO(count);
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    myEmail.To = this.patientDS.Tables[9].Rows[count]["email"].ToString();
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }

            myEmail.Subject = "GIPAP Patient Reactivation - " + this.PIN;
            if (this.CurrentCMLPhase == "Blast Crisis")
            {
                myEmail.Subject += " - Blast Crisis";
            }
            myEmail.Message = "GIPAP PATIENT REACTIVATION (NOVARTIS)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + ":\n\n";
            myEmail.Message += "The case in GIPAP for patient " + this.PIN + " has been reactivated.  Please ensure that the physician receives the 120 day supply of Glivec (R) for the approved patient.\n\n";
            myEmail.Message += "If there is a need to contact the treating physician regarding this patient’s supply of Glivec (R), please refer to the patient PIN# in your communication.  This will help to protect patient confidentiality.\n\n";
            myEmail.Message += "---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient PIN: " + this.PIN;
            myEmail.Message += "\nDiagnosis: " + this.Diagnosis;
            myEmail.Message += "\nOriginal GIPAP Approval Date: " + this.IADate.Day.ToString() + " " + this.IADate.ToString("y");
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Reason for Reactivation: " + this.patientDS.Tables[12].Rows[0]["statusreason"].ToString();
            myEmail.Message += "\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";

            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOAReactivationEmailCPO(int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    myEmail.To = this.patientDS.Tables[9].Rows[count]["email"].ToString();
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }
            if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "PAT Patient Reactivation - " + this.PIN;
                myEmail.Message = "PAT PATIENT REACTIVATION (NOVARTIS)\n\n";
            }
            else if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "TIPAP Patient Reactivation - " + this.PIN;
                    myEmail.Message = "TIPAP PATIENT REACTIVATION (NOVARTIS)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA Tasigna Patient Reactivation - " + this.PIN;
                    myEmail.Message = "NOA Tasigna PATIENT REACTIVATION (NOVARTIS)\n\n";
                }
            }
            else if (this.DonationLength == 12 || this.DonationLength == 370)
            {
                myEmail.Subject = "NOA / GIPAP Patient Reactivation - " + this.PIN;
                myEmail.Message = "NOA / GIPAP PATIENT REACTIVATION (NOVARTIS)\n\n";
            }
            else
            {
                myEmail.Subject = "NOA Patient Reactivation - " + this.PIN;
                myEmail.Message = "NOA PATIENT REACTIVATION (NOVARTIS)\n\n";
            }
            if (this.CurrentCMLPhase == "Blast Crisis")
            {
                myEmail.Subject += " - Blast Crisis";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += "--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "Dear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + ":\n\n";
            if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "Patient " + this.PIN + " has been reactivated for Novartis Oncology Temporal Assistance Program (PAT).  Please ensure that the treating physician receives the appropriate supply of Glivec(R) for the patient.\n\n";
            }
            else if (this.CountryID == 76)
            {
                myEmail.Message += "The case for patient " + this.PIN + " has been reactivated with a ";
                myEmail.Message += this.DonationScheme();
                if (this.DonationLength == 12 || this.DonationLength == 370)
                {
                    myEmail.Message += " donation scheme. Please ensure that Novartis C&F outlet receives the first 120 day supply of " + this.Treatment + "® for the approved patient.  ";
                }
                else if (this.DonationLength == 0)
                {
                    myEmail.Message += " payment scheme. Please ensure that Novartis C&F outlet receives appropriate supply of " + this.Treatment + "® for the approved patient according to the " + this.PaymentOption + " payment option.  ";
                }
                else
                {
                    myEmail.Message += " co-pay/donation scheme. Please ensure that Novartis C&F outlet receives appropriate supply of " + this.Treatment + "® for the approved patient according to the " + this.PaymentOption + " payment option.  ";
                }
                myEmail.Message += "Continued patient eligibility is determined by yearly financial evaluation and quarterly physician medical evaluation of the patient.  A re-approval notification will be sent to you when the next ";
                if (this.DonationLength == 12 || this.DonationLength == 370)
                {
                    myEmail.Message += "120 day supply is authorized.\n\n";
                }
                else
                {
                    myEmail.Message += "120 day supply is authorized.\n\n";
                }
            }
            else if (this.CountryID == 162 && this.Treatment == "Tasigna")//tipap
            {
                myEmail.Message += "Tasigna(R) International Patient Assistance Program has been reactivated for Patient " + this.PIN + ".  Please ensure that the treating physician receives the appropriate supply of " + this.Treatment + "(R) for the patient.\n\n";
            }
            else
            {
                myEmail.Message += "Novartis Oncology Access (NOA) Program has been reactivated for Patient " + this.PIN + ".  Please ensure that the treating physician receives the appropriate supply of " + this.Treatment + "(R) for the patient.\n\n";
            }
            myEmail.Message += "If there is a need to contact the treating physician regarding this patient’s supply of " + this.Treatment + "(R), please refer to the patient PIN# in your communication.  This will help to protect patient confidentiality.\n\n";
            myEmail.Message += "---- PATIENT INFORMATION ----\n\n";
            myEmail.Message += "Patient PIN: (TMF)" + this.PIN + " (FC)" + this.NOAPIN;
            myEmail.Message += "\nDiagnosis: " + this.Diagnosis;
            myEmail.Message += "\nDates Currently Approved For: " + this.StartDate.Day.ToString() + " " + this.StartDate.ToString("y") + " through ";
            myEmail.Message += this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "\n\n";
            myEmail.Message += "Reason for Reactivation: " + this.patientDS.Tables[12].Rows[0]["statusreason"].ToString();
            myEmail.Message += "\nDosage Prescribed: " + this.CurrentDosage.ToString() + "/day\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email DenialEmailPatient(int pid)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                return this.NOADenialEmailPatient();
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[5].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[5].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To = this.patientDS.Tables[5].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (myEmail.To == "")
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.Subject = "Denial of Application for GIPAP – " + this.PIN;
            myEmail.Message = "DENIAL OF APPLICATION FOR GIPAP (PATIENT)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Physician Name: ";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "\n" + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
            }
            myEmail.Message += "\n\nPatient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
            myEmail.Message += "We have reviewed the GIPAP application submitted to The Max Foundation on your behalf. The result of the review indicates that your application does not meet the criteria for GIPAP at this time.\n\n";
            myEmail.Message += "Reason for Denial: " + this.StatusReason;
            myEmail.Message += "\n\nShould your situation change, please ask your physician to let us know of the change and we will re-evaluate your case.\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org, or speak with your physician.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOADenialEmailPatient()
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.Email != "")
            {
                myEmail.To = this.Email;
            }
            else if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[5].Rows.Count; i++)
                {
                    if (this.patientDS.Tables[5].Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To = this.patientDS.Tables[5].Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if (myEmail.To == "")
            {
                for (int i = 0; i < this.PhysicianCount; i++)
                {
                    myEmail.To = this.patientDS.Tables[4].Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "Denial of Application for TIPAP – " + this.PIN;
                    myEmail.Message = "DENIAL OF APPLICATION FOR TIPAP (PATIENT)\n\n";
                }
                else
                {
                    myEmail.Subject = "Denial of Application for NOA Tasigna – " + this.PIN;
                    myEmail.Message = "DENIAL OF APPLICATION FOR NOA Tasigna (PATIENT)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "Denial of Application for NOA / GIPAP – " + this.PIN;
                myEmail.Message = "DENIAL OF APPLICATION FOR NOA / GIPAP (PATIENT)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Physician Name: ";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += "\n" + this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
            }
            myEmail.Message += "\n\nPatient Identification Number: " + this.PIN.ToString() + "\n\n";
            myEmail.Message += this.FirstName + " " + this.LastName + "\n";
            if (this.Street1.ToString() != "")
            {
                myEmail.Message += this.Street1.ToString() + "\n";
            }
            if (this.Street2.ToString() != "")
            {
                myEmail.Message += this.Street2.ToString() + "\n";
            }
            if (this.City.ToString() != "")
            {
                myEmail.Message += this.City.ToString() + ", ";
            }
            if (this.StateProvince.ToString() != "")
            {
                myEmail.Message += this.StateProvince.ToString() + " ";
            }
            if (this.PostalCode.ToString() != "")
            {
                myEmail.Message += this.PostalCode.ToString() + "\n";
            }
            myEmail.Message += this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + "\n\n";
            myEmail.Message += "Dear " + this.FirstName + " " + this.LastName + ":\n\n";
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Message += "Your TIPAP application ";
                }
                else
                {
                    myEmail.Message += "Your NOA Tasigna application ";
                }
            }
            else
            {
                myEmail.Message += "Your GIPAP/NOA application ";
            }
            if (this.CountryID == 76 && !this.Fixed)
            {
                myEmail.Message += "has been assessed by Indiabulls., an independent third party financial evaluation agency, who used World Health Organisation guidelines on drug access plans to evaluate their decision. ";
            }
            else
            {
                myEmail.Message += "has been assessed.  ";
            }
            myEmail.Message += "The result of the review indicates that your application does not meet the criteria for the program at this time.\n\n";
            myEmail.Message += "Reason for Denial: " + this.StatusReason;
            myEmail.Message += "\n\nShould your situation change, please ask your physician to let us know of the change and we will re-evaluate your case.\n\n";

            if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "Si tiene alguna pregunta o aclaración, por favor contáctese con Fundación Max en México al 52 5595 85 66 o stefany.green@THEMAXFOUNDATION.ORG , o en Monterrey al 52 81 8317 4292 o  cynthia.figueroa@THEMAXFOUNDATION.ORG, o bien comuníquese con su médico tratante.";
                myEmail.Message += "\n\nAtentamente, \n\nFundación Max";
            }
            else if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office at (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322, or speak with your physician.";
                myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            }
            else //generic
            {
                if (this.patientDS.Tables[3].Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or " + this.patientDS.Tables[11].Rows[0]["email"].ToString() + ", or speak with your physician.";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email DenialEmailPhysician(int pid, int count)
        {
            this.InflateEmail(pid);
            if (this.FlagNOA)
            {
                return this.NOADenialEmailPhysician(count);
            }
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";
            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            myEmail.Subject = "Denial of Application for GIPAP – " + this.PIN;
            myEmail.Message = "DENIAL OF APPLICATION FOR GIPAP (PHYSICIAN)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ",\n\n";
            myEmail.Message += "We have reviewed the GIPAP application submitted to The Max Foundation to the behalf of your patient, " + this.FirstName + " " + this.LastName;
            myEmail.Message += ". The result of the review indicates that this case does not meet the criteria for GIPAP at this time.\n\n";
            myEmail.Message += "Reason for Denial: " + this.StatusReason;
            myEmail.Message += "\n\nShould the situation change, please let us know of the change and we will re-evaluate this patient’s case.\n\n";
            string fullname = this.patientDS.Tables[11].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[11].Rows[0]["lastname"].ToString();
            string email = this.patientDS.Tables[11].Rows[0]["phone"].ToString() + " or send an email to " + this.patientDS.Tables[11].Rows[0]["email"].ToString();
            if (this.CountryID != 201)
            {
                myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at " + email + " or speak with your physician.  ";
            }
            try
            {
                if (this.patientDS.Tables[3].Rows[0]["phone"].ToString() != "")
                {
                    myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["phone"].ToString();
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += " or " + this.patientDS.Tables[3].Rows[0]["email"].ToString() + ".";
                    }
                }
                else
                {
                    if (this.patientDS.Tables[3].Rows[0]["email"].ToString() != "")
                    {
                        myEmail.Message += "Locally, you may contact our MaxStation, " + this.patientDS.Tables[3].Rows[0]["firstname"].ToString() + " " + this.patientDS.Tables[3].Rows[0]["lastname"].ToString() + ", who works with " + this.patientDS.Tables[2].Rows[0]["countryname"].ToString() + " at " + this.patientDS.Tables[3].Rows[0]["email"].ToString();
                    }
                }
                if (this.CountryID == 201)
                {
                    myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org.  ";
                }
            }
            catch { }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        private GIPAP_Objects.Email NOADenialEmailPhysician(int count)
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.CC = "gipap@themaxfoundation.org";
            if (this.PhysicianCount > 0)
            {
                myEmail.To = this.patientDS.Tables[4].Rows[count]["email"].ToString();
            }
            else
            {
                return myEmail;
            }
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "Denial of Application for TIPAP – " + this.PIN;
                    myEmail.Message = "DENIAL OF APPLICATION FOR TIPAP (PHYSICIAN)\n\n";
                }
                else
                {
                    myEmail.Subject = "Denial of Application for NOA Tasigna – " + this.PIN;
                    myEmail.Message = "DENIAL OF APPLICATION FOR NOA Tasigna (PHYSICIAN)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "Denial of Application for NOA / GIPAP – " + this.PIN;
                myEmail.Message = "DENIAL OF APPLICATION FOR NOA / GIPAP (PHYSICIAN)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN + "\n\n";
            myEmail.Message += "Dear Dr. " + this.patientDS.Tables[4].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[count]["lastname"].ToString() + ",\n\n";
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Message += "The TIPAP ";
                }
                else
                {
                    myEmail.Message += "The NOA Tasigna ";
                }
            }
            else
            {
                myEmail.Message += "The GIPAP/NOA ";
            }
            myEmail.Message += "application submitted to on behalf of your patient, " + this.FirstName + " " + this.LastName;
            if (this.Fixed || this.CountryID != 76)
            {
                myEmail.Message += " has been assessed.  ";
            }
            else
            {
                myEmail.Message += " has been assessed by Indiabulls., an independent third party financial evaluation agency, who used World Health Organisation guidelines on drug access plans to evaluate their decision. ";
            }
            myEmail.Message += "The result of the review indicates that " + this.FirstName + " " + this.LastName + " does not meet the criteria for ";
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Message += "TIPAP ";
                }
                else
                {
                    myEmail.Message += "NOA Tasigna ";
                }
            }
            else
            {
                myEmail.Message += "GIPAP/NOA ";
            }
            myEmail.Message += "at this time.\n\n";
            myEmail.Message += "Reason for Denial: " + this.StatusReason;
            myEmail.Message += "\n\nShould the situation change, please let us know of the change and we will re-evaluate this patient’s case.";
            myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOADenialEmailCPO(int pid, int count)
        {
            this.InflateEmail(pid);
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CPOCount > 0)
            {
                if (this.CountryEmail == "")
                {
                    for (int i = 0; i < this.patientDS.Tables[9].Rows.Count; i++)
                    {
                        myEmail.To += this.patientDS.Tables[9].Rows[i]["email"].ToString() + "; ";
                    }
                }
                else
                {
                    myEmail.To = this.CountryEmail;
                }
            }
            else
            {
                return myEmail;
            }
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Subject = "Denial of Application for TIPAP – " + this.PIN;
                    myEmail.Message = "DENIAL OF APPLICATION FOR TIPAP (NOVARTIS)\n\n";
                }
                else
                {
                    myEmail.Subject = "Denial of Application for NOA Tasigna – " + this.PIN;
                    myEmail.Message = "DENIAL OF APPLICATION FOR NOA Tasigna (NOVARTIS)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "Denial of Application for NOA / GIPAP – " + this.PIN;
                myEmail.Message = "DENIAL OF APPLICATION FOR NOA / GIPAP (NOVARTIS)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN + "\n\n";
            myEmail.Message += "Dear " + this.patientDS.Tables[9].Rows[count]["firstname"].ToString() + " " + this.patientDS.Tables[9].Rows[count]["lastname"].ToString() + ":\n\n";
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Message += "The TIPAP ";
                }
                else
                {
                    myEmail.Message += "The NOA Tasigna ";
                }
            }
            else
            {
                myEmail.Message += "The GIPAP/NOA ";
            }
            myEmail.Message += "application submitted on behalf of the patient " + this.PIN;
            if (this.Fixed || this.CountryID != 76)
            {
                myEmail.Message += " has been reviewed.  ";
            }
            else
            {
                myEmail.Message += " has been reviewed by Indiabulls. ";
            }
            myEmail.Message += "The result of the review indicates that this case does not meet the criteria for ";
            if (this.Treatment == "Tasigna")
            {
                if (this.CountryID == 162)
                {
                    myEmail.Message += "TIPAP ";
                }
                else
                {
                    myEmail.Message += "NOA Tasigna ";
                }
            }
            else
            {
                myEmail.Message += "GIPAP/NOA ";
            }
            myEmail.Message += "at this time.\n\n";
            myEmail.Message += "Reason for Denial: " + this.StatusReason;
            myEmail.Message += "\n\nSincerely, \n\nProgramme Administrator - The Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOA10DaySupply(int pid)
        {
            this.InflateEmail(pid);
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            if (this.CountryEmail != "")
            {
                myEmail.To = this.CountryEmail;
            }
            else if (this.CPOCount > 0)
            {
                for (int i = 0; i < this.CPOCount; i++)
                {
                    myEmail.To += this.patientDS.Tables[9].Rows[i]["email"].ToString() + "; ";
                }
            }
            else
            {
                return myEmail;
            }
            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.Subject = "10 Day Supply for new patient - " + this.PIN;
            myEmail.Message = "10 Day Supply for new patient (NOVARTIS)\n\n";
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Dear Novartis Personnel,\nPlease release a 10-Day supply to the following patient:\n\n";
            myEmail.Message += "Patient Identification Number: " + this.PIN.ToString() + "\n";
            myEmail.Message += "Dosage Prescribed: " + this.OriginalRequestedDosage;
            myEmail.Message += "\n\n--Physician Information--\n\n";
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                myEmail.Message += this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString();
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    myEmail.Message += "\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    myEmail.Message += "\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString();
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    myEmail.Message += "\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n";
                }
            }
            myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        //**********************************************************************************************************************
        public GIPAP_Objects.Email NOABranchAssignment(int pid)
        {
            this.InflateEmail(pid);
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

            myEmail.From = "gipap@themaxfoundation.org";
            GIPAP_Objects.FCOffice myOffice = new FCOffice(0);
            if (myOffice.FreedomDeskEmail != "")
            {
                myEmail.To += myOffice.FreedomDeskEmail;
            }
            else
            {
                return myEmail;
            }/*
            if (this.patientDS.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < this.patientDS.Tables[3].Rows.Count; i++)
                {
                    myEmail.CC += this.patientDS.Tables[3].Rows[i]["email"].ToString() + "; ";
                }
            }*/
            myEmail.Subject = "NOA Branch Assignment - " + this.PIN;
            StringBuilder appInfo = new StringBuilder();
            appInfo.Append("NOA Branch Assignment \n\n");
            appInfo.Append(DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n");

            appInfo.Append("Patient Name: " + this.FirstName + " " + this.LastName + "\n");
            appInfo.Append("Patient PIN: " + this.PIN + "\n");
            if (this.patientDS.Tables[14].Rows.Count > 0)
            {
                appInfo.Append("Branch(es): ");
                for (int i = 0; i < this.patientDS.Tables[14].Rows.Count; i++)
                {
                    appInfo.Append(this.patientDS.Tables[14].Rows[i]["officename"].ToString() + "\n");
                }
            }
            appInfo.Append("\nGender: " + this.Sex + "\n");
            appInfo.Append("Birthdate: " + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y") + "\n");
            appInfo.Append("Street1: " + this.Street1 + "\n");
            appInfo.Append("Street2: " + this.Street2 + "\n");
            appInfo.Append("City: " + this.City + "\n");
            appInfo.Append("State/Province: " + this.StateProvince + "\n");
            appInfo.Append("Postal Code: " + this.PostalCode + "\n");
            appInfo.Append("Country: " + this.patientDS.Tables[2].Rows[0]["Country"].ToString() + "\n");
            appInfo.Append("Phone: " + this.Phone + "\n");
            appInfo.Append("Fax: " + this.Fax + "\n");
            appInfo.Append("Email: " + this.Email + "\n\n");
            if (this.patientDS.Tables[5].Rows.Count > 0)
            {
                appInfo.Append("Contact Info\n\n");
                for (int poK = 1; poK < this.patientDS.Tables[5].Rows.Count; poK++)
                {
                    appInfo.Append(this.patientDS.Tables[5].Rows[poK]["ContactName"].ToString() + "\n");
                }
            }
            appInfo.Append("\n--Physician Information--\n\n");
            for (int i = 0; i < this.PhysicianCount; i++)
            {
                appInfo.Append(this.patientDS.Tables[4].Rows[i]["firstname"].ToString() + " " + this.patientDS.Tables[4].Rows[i]["lastname"].ToString());
                if (this.patientDS.Tables[4].Rows[i]["phone"].ToString() != "")
                {
                    appInfo.Append("\nTel: " + this.patientDS.Tables[4].Rows[i]["phone"].ToString());
                }
                if (this.patientDS.Tables[4].Rows[i]["fax"].ToString() != "")
                {
                    appInfo.Append("\nFax: " + this.patientDS.Tables[4].Rows[i]["fax"].ToString());
                }
                if (this.patientDS.Tables[4].Rows[i]["email"].ToString() != "")
                {
                    appInfo.Append("\nEmail: " + this.patientDS.Tables[4].Rows[i]["email"].ToString() + "\n\n");
                }
            }

            appInfo.Append("History and Diagnosis Information\nApplied for GIPAP: ");
            if (this.AppliedForGipap)
            {
                appInfo.Append("Yes\n");
            }
            else
            {
                appInfo.Append("No\n");
            }
            appInfo.Append("Prescribed Daily Dosage: " + this.CurrentDosage + "\n");
            appInfo.Append("Diagnosis Date: " + this.DiagnosisDate.Day.ToString() + " " + this.DiagnosisDate.ToString("y") + "\n");
            appInfo.Append("Has patient previously taken Glivec®/imatinib?: ");
            if (this.Glivec)
            {
                appInfo.Append("Yes\n");
                appInfo.Append("If yes, what was the starting date?: " + this.GlivecStartDate.Day.ToString() + " " + this.GlivecStartDate.ToString("y") + "\n\n");
            }
            else
            {
                appInfo.Append("No\n\n");
            }

            if (this.Diagnosis == "CML")
            {
                appInfo.Append("CML and Interferon Information\nDiagnosis: CML\n");
                appInfo.Append("Philadelphia Chromosome Positive: ");
                if (this.PhilPos == 0)
                {
                    appInfo.Append("No\nIf no, is patient BCR-Abl positive?: ");
                    if (this.BCR == 0)
                    {
                        appInfo.Append("No\n");
                    }
                    else if (this.BCR == 1)
                    {
                        appInfo.Append("Yes\n");
                    }
                    else
                    {
                        appInfo.Append("Don't Know\n");
                    }
                }
                else if (this.PhilPos == 1)
                {
                    appInfo.Append("Yes\n");
                }
                else
                {
                    appInfo.Append("Don't Know\n");
                }
                appInfo.Append("CML Phase: " + this.CurrentCMLPhase + "\n");
                if (this.InterferonTimeLength != "" && this.InterferonTimeLength != "0")
                {
                    appInfo.Append("Recieved Interferon: ");
                    if (this.Interferon)
                    {
                        appInfo.Append("Yes\n");
                        appInfo.Append("Interferon Treatment Period: " + this.InterferonTimeLength + "\n");
                        appInfo.Append("Intolerant To Interferon: ");
                        if (this.Intolerant)
                        {
                            appInfo.Append("Yes\n");
                        }
                        else
                        {
                            appInfo.Append("No\n");
                        }
                        appInfo.Append("Hematologic Failure: ");
                        if (this.HematologicFailure)
                        {
                            appInfo.Append("Yes\n");
                        }
                        else
                        {
                            appInfo.Append("No\n");
                        }
                        appInfo.Append("Cytogenetic Failure: ");
                        if (this.CytogeneticFailure)
                        {
                            appInfo.Append("Yes\n");
                        }
                        else
                        {
                            appInfo.Append("No\n");
                        }
                    }
                    else
                    {
                        appInfo.Append("No\n");
                    }
                }
            }
            else if (this.Diagnosis == "Ph+ ALL")
            {
                appInfo.Append("Ph+ ALL Information\nDiagnosis: Ph+ ALL\n");
                appInfo.Append("Philadelphia Chromosome Positive: ");
                if (this.PhilPos == 0)
                {
                    appInfo.Append("No\nIf no, is patient BCR-Abl positive?: ");
                    if (this.BCR == 0)
                    {
                        appInfo.Append("No\n");
                    }
                    else if (this.BCR == 1)
                    {
                        appInfo.Append("Yes\n");
                    }
                    else
                    {
                        appInfo.Append("Don't Know\n");
                    }
                }
                else if (this.PhilPos == 1)
                {
                    appInfo.Append("Yes\n");
                }
                else
                {
                    appInfo.Append("Don't Know\n");
                }
                if (this.RelapsedRefractory != -1)
                {
                    appInfo.Append("\nRelapsed / Refractory: ");
                    if (this.RelapsedRefractory == 1)
                    {
                        appInfo.Append("Yes");
                    }
                    else
                    {
                        appInfo.Append("No");
                    }
                }
                if (this.Chemo != -1)
                {
                    appInfo.Append("\nNewly Diagnosed / Integrated with Chemotherapy: ");
                    if (this.Chemo == 1)
                    {
                        appInfo.Append("Yes");
                    }
                    else
                    {
                        appInfo.Append("No");
                    }
                }
            }
            else if (this.Diagnosis == "GIST")
            {
                appInfo.Append("GIST and C-Kit Information\nDiagnosis: GIST\n");
                appInfo.Append("C-Kit Positive: ");
                if (this.CKitPos == 0)
                {
                    appInfo.Append("No\n");
                }
                else if (this.CKitPos == 1)
                {
                    appInfo.Append("Yes\n");
                }
                else
                {
                    appInfo.Append("Don't Know\n");
                }
                appInfo.Append("Tumor Unresectable: ");
                if (this.Unresectable == 0)
                {
                    appInfo.Append("No\n");
                }
                else if (this.Unresectable == 1)
                {
                    appInfo.Append("Yes\n");
                }
                else
                {
                    appInfo.Append("Don't Know\n");
                }
                appInfo.Append("Tumor Metastatic: ");
                if (this.Metastatic == 0)
                {
                    appInfo.Append("No\n");
                }
                else if (this.Metastatic == 1)
                {
                    appInfo.Append("Yes\n");
                }
                else
                {
                    appInfo.Append("Don't Know\n");
                }
            }
            appInfo.Append("\nInsurance Information\n");
            appInfo.Append("Insurance: ");
            if (Convert.ToBoolean(this.patientDS.Tables[13].Rows[0]["insurance"]))
            {
                appInfo.Append("Yes\n");
                appInfo.Append("Covers Prescriptions: ");
                if (Convert.ToBoolean(this.patientDS.Tables[13].Rows[0]["coversrx"]))
                {
                    appInfo.Append("Yes\n");
                }
                else
                {
                    appInfo.Append("No\n");
                }
                appInfo.Append("Covers Cancer Prescriptions: ");
                if (Convert.ToBoolean(this.patientDS.Tables[13].Rows[0]["coverscancerrx"]))
                {
                    appInfo.Append("Yes\n");
                }
                else
                {
                    appInfo.Append("No\n");
                }
                appInfo.Append("Covers Glivec Prescriptions: ");
                if (Convert.ToBoolean(this.patientDS.Tables[13].Rows[0]["coversglivecrx"]))
                {
                    appInfo.Append("Yes\n");
                }
                else
                {
                    appInfo.Append("No\n");
                }
            }
            else
            {
                appInfo.Append("No\n");
            }
            //financial info
            if (this.AnnualIncome != "0")
            {
                appInfo.Append("\nFinancial Information\nEstimated Annual Income: $" + this.AnnualIncome + "\n");
                appInfo.Append("Patient's or primary income earner's Occupation: " + this.Occupation + "\n");
                //appInfo.Append("Additional notes: " + this.Notes);
            }
            appInfo.Append("\nPlease contact MaxIndia@themaxfoundation.org with questions or concerns.\n\nSincerely, \n\nThe Max Foundation");
            myEmail.Message = appInfo.ToString();
            myEmail.PatientID = this.PatientID;
            return myEmail;
        }
        /*End Emails*/
		//**************************************************************************************************************
		public void Clear()
		{
			this.PatientID = 0;
			this.PIN = "";
			this.FirstName = "";
			this.LastName = "";
			this.Sex = "";
			this.Street1 = "";
			this.Street2 = "";
			this.City = "";
			this.StateProvince = "";
			this.PostalCode = "";
			this.CountryID = 0;
			this.Phone = "";
			this.Fax = "";
			this.Email = "";
			this.Mobile = "";
			this.PhysicianID = 0;
			this.ProgramOfficerID = 0;
			this.AnnualIncome = "";
			this.Occupation = "";
			this.AppliedForGipap = false;
			this.OriginalRequestedDosage = "";
			this.OriginalApprovedDosage = "";
			this.GIPAPStatus = "";
			this.StatusReason = "";
			this.Diagnosis = "";
			this.EnableAutoApprove = true;
			//cml
			this.PhilPos = 2;
			this.OriginalCMLPhase = "";
			this.CurrentCMLPhase = "";
			this.Interferon = false;
			this.InterferonTimeLength = "";
			this.Intolerant = false;
			this.HematologicFailure = false;
			this.CytogeneticFailure = false;
			//CML OLD
			this.Refractory = false;
			this.Unresponsive = false;
			//gist
			this.CKitPos = 2;
			this.Unresectable = 2;
			this.Metastatic = 2;
			//gipap detail
			this.CurrentDosage = "";
			this.PhysicianClose = false;
			this.ReminderEmail = false;
			this.ReminderEmail90 = false;
			this.AutoApprove = false;
			this.DelayedReapproval = false;
			//other tables
            this.FlagNOA = false;
		}

		//**************************************************************************************************************
		private void Inflate(DataSet ds, string Urole)
		{
			//Populates the objects parameters with the data returned from the database
			this.PatientID = (int)(ds.Tables[0].Rows[0]["PatientID"]);
			this.PIN = (ds.Tables[0].Rows[0]["PIN"]).ToString();
			this.FirstName = (ds.Tables[0].Rows[0]["FirstName"]).ToString();
			this.LastName = (ds.Tables[0].Rows[0]["LastName"]).ToString();
			this.Sex = (ds.Tables[0].Rows[0]["Sex"]).ToString();
			this.BirthDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"]);
			this.Street1 = (ds.Tables[0].Rows[0]["Street1"]).ToString();
			this.Street2 = (ds.Tables[0].Rows[0]["Street2"]).ToString();
			this.City = (ds.Tables[0].Rows[0]["City"]).ToString();
			this.StateProvince = (ds.Tables[0].Rows[0]["StateProvince"]).ToString();
			this.PostalCode = (ds.Tables[0].Rows[0]["PostalCode"]).ToString();
			this.Phone = (ds.Tables[0].Rows[0]["Phone"]).ToString();
			this.Fax = (ds.Tables[0].Rows[0]["Fax"]).ToString();
			this.Email = (ds.Tables[0].Rows[0]["Email"]).ToString();
			this.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
			this.AnnualIncome = (ds.Tables[0].Rows[0]["AnnualIncome"]).ToString();
			this.Occupation = (ds.Tables[0].Rows[0]["Occupation"]).ToString();
			this.IntakeDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IntakeDate"]);
			try
			{
				this.IADate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IADate"]);
			}
			catch{}
			this.AppliedForGipap = Convert.ToBoolean(ds.Tables[0].Rows[0]["AppliedForGipap"]);
			this.PatientConsent = Convert.ToBoolean(ds.Tables[0].Rows[0]["patientconsent"]);
			this.OriginalRequestedDosage = (ds.Tables[0].Rows[0]["OriginalRequestedDosage"]).ToString();
            this.OriginalApprovedDosage = (ds.Tables[0].Rows[0]["OriginalApprovedDosage"]).ToString();
            this.OriginalTabletStrength = (ds.Tables[0].Rows[0]["OriginalTabletStrength"]).ToString();
			this.GIPAPStatus = (ds.Tables[0].Rows[0]["GIPAPStatus"]).ToString();
			this.StatusReason = ds.Tables[0].Rows[0]["StatusReason"].ToString();
			this.EnableAutoApprove = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnableAutoApprove"]);
            this.EnableAutoClose = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnableAutoClose"]); 
            //NOA
            this.FlagNOA = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOA"]);
			try
			{
				this.ApplicantID = Convert.ToInt32(ds.Tables[0].Rows[0]["ApplicantID"]);
			}
			catch
			{
				this.ApplicantID = 0;
			}
			this.Diagnosis = ds.Tables[0].Rows[0]["Diagnosis"].ToString();
			try
			{
				this.DiagnosisDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DiagnosisDate"]);
			}
			catch{}
			try
			{
				this.PhysicianClose = Convert.ToBoolean(ds.Tables[0].Rows[0]["PhysicianClose"]);
			}
			catch{}
			try
			{
				this.GlivecStartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["GlivecStartDate"]);
			}
			catch{}
			try
			{
				this.Glivec = Convert.ToBoolean(ds.Tables[0].Rows[0]["Glivec"]);
			}
			catch{}
			if(this.Diagnosis == "CML")
			{
				this.PhilPos = Convert.ToInt32(ds.Tables[1].Rows[0]["PhilPos"]);
				this.BCR = Convert.ToInt32(ds.Tables[1].Rows[0]["BCR"]);
				this.OriginalCMLPhase = Convert.ToString(ds.Tables[1].Rows[0]["OriginalCMLPhase"]);
				this.CurrentCMLPhase = Convert.ToString(ds.Tables[1].Rows[0]["CurrentCMLPhase"]);
				//country table
				this.PediatricApproved = Convert.ToBoolean(ds.Tables[8].Rows[0]["PediatricApproved"]);

				if(ds.Tables[2].Rows.Count > 0)
				{
					this.Interferon = Convert.ToBoolean(ds.Tables[2].Rows[0]["Interferon"]);
					//old
					try
					{
						this.Refractory = Convert.ToBoolean(ds.Tables[2].Rows[0]["Refractory"]);
						this.Unresponsive = Convert.ToBoolean(ds.Tables[2].Rows[0]["Unresponsive"]);
					}
					catch{}
					//new
					this.InterferonTimeLength = Convert.ToString(ds.Tables[2].Rows[0]["InterferonTimeLength"]);
					try
					{
						this.Intolerant = Convert.ToBoolean(ds.Tables[2].Rows[0]["Intolerant"]);
					}
					catch{this.Intolerant = false;}
					try
					{
						this.HematologicFailure = Convert.ToBoolean(ds.Tables[2].Rows[0]["HematologicFailure"]);
					}
					catch{this.HematologicFailure = false;}
					try
					{
						this.CytogeneticFailure = Convert.ToBoolean(ds.Tables[2].Rows[0]["CytogeneticFailure"]);
					}
					catch{this.CytogeneticFailure = false;}
				}
			}
			else if(this.Diagnosis == "Ph+ ALL")
			{
				this.PhilPos = Convert.ToInt32(ds.Tables[26].Rows[0]["PhilPos"]);
				this.BCR = Convert.ToInt32(ds.Tables[26].Rows[0]["BCR"]);
				this.RelapsedRefractory = Convert.ToInt32(ds.Tables[26].Rows[0]["RelapsedRefractory"]);
				this.Chemo = Convert.ToInt32(ds.Tables[26].Rows[0]["Chemo"]);
				this.PediatricApproved = false;
			}
			else if(this.Diagnosis == "DFSP")
			{
				this.Recurrent = Convert.ToBoolean(ds.Tables[27].Rows[0]["recurrent"]);
				//coutry
				this.PediatricApproved = Convert.ToBoolean(ds.Tables[8].Rows[0]["dfspPedApproved"]);
			}
			else if(this.Diagnosis == "GIST")
			{
				this.CKitPos = Convert.ToInt32(ds.Tables[3].Rows[0]["CKitPositive"]);
				this.Unresectable = Convert.ToInt32(ds.Tables[3].Rows[0]["Unresectable"]);
				this.Metastatic = Convert.ToInt32(ds.Tables[3].Rows[0]["Metastatic"]);
				//country
				this.PediatricApproved = Convert.ToBoolean(ds.Tables[8].Rows[0]["gistPedApproved"]);
			}
			if(ds.Tables[4].Rows.Count > 0)
			{
				this.StatusReason = (ds.Tables[4].Rows[0]["StatusReason"]).ToString();
				try
				{
					this.StartDate = Convert.ToDateTime(ds.Tables[4].Rows[0]["StartDate"]);
				}
				catch{}
				try
				{
					this.EndDate = Convert.ToDateTime(ds.Tables[4].Rows[0]["EndDate"]);
				}
				catch{}
				this.CurrentDosage = (ds.Tables[4].Rows[0]["CurrentDosage"]).ToString();
				this.ReminderEmail = Convert.ToBoolean(ds.Tables[4].Rows[0]["ReminderEmail"]);
				this.ReminderEmail90 = Convert.ToBoolean(ds.Tables[4].Rows[0]["ReminderEmail90"]);
				this.AutoApprove = Convert.ToBoolean(ds.Tables[4].Rows[0]["AutoApprove"]);
				//this.DelayedReapproval = Convert.ToBoolean(ds.Tables[4].Rows[0]["DelayedReapproval"]);
			}

			dsStatusHistory = ds.Tables[14];//not used
			//dsSae = ds.Tables[5]; //only used as a count, prev. used in emails
			dsCaseNotes = ds.Tables[6];
			dsEmails = ds.Tables[7];//not used

			if(ds.Tables[8].Rows.Count > 0)
			{
				if(ds.Tables[8].Rows[0]["activegipap"].ToString() == "Yes")
				{
					this.CountryName = ds.Tables[8].Rows[0]["Country"].ToString();
				}
				else
				{
					this.CountryName = "<font color=red>" + ds.Tables[8].Rows[0]["Country"].ToString() + "</font>";
				}
				this.CountryID = Convert.ToInt32(ds.Tables[8].Rows[0]["CountryID"]);
				if(Urole == "TMFUser")
				{
					this.CountryName = "<a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + ">" + this.CountryName + "</a>";
				}
			
				this.CountryCode = ds.Tables[8].Rows[0]["CountryCode"].ToString();
				this.NeedInterferonInfo = Convert.ToBoolean(ds.Tables[8].Rows[0]["NeedInterferonInfo"]);
				this.NeedFinancialInfo = Convert.ToBoolean(ds.Tables[8].Rows[0]["NeedFinancialInfo"]);
				try
				{
					this.FinancialDeclarationDate = Convert.ToDateTime(ds.Tables[8].Rows[0]["FinancialDeclarationDate"].ToString());
				}
				catch
				{
					this.FinancialDeclarationDate = DateTime.Today.AddDays(1);
				}
				//this.PediatricApproved = Convert.ToBoolean(ds.Tables[8].Rows[0]["PediatricApproved"]);
				this.PediatricAge = Convert.ToInt32(ds.Tables[8].Rows[0]["PediatricAge"]);
				if(this.PediatricAge == 0)
				{
					this.PediatricAge = 16;
				}
				this.GistApproved = Convert.ToBoolean(ds.Tables[8].Rows[0]["GistApproved"]);
				this.CountryEmail = ds.Tables[8].Rows[0]["Email"].ToString();
			}

			if(ds.Tables[9].Rows.Count > 0)
			{
				this.MaxStationName = ds.Tables[9].Rows[0]["MaxStationName"].ToString();
				for(int maxK = 1; maxK<ds.Tables[9].Rows.Count; maxK++)
				{
					this.MaxStationName += " / " + ds.Tables[9].Rows[maxK]["MaxStationName"].ToString();
				}
			}
			//turn red if not approved
			this.PhysicianCount = ds.Tables[10].Rows.Count;
			if(this.PhysicianCount > 0)
			{
                bool chkPhys = true;
				for(int phyK = 0; phyK<ds.Tables[10].Rows.Count; phyK++)
				{
                    if (phyK != 0)
                    {
                        this.PhysicianName += " / ";
                    }
					if(Convert.ToInt32(ds.Tables[10].Rows[phyK]["approved"]) == 1)
					{
						this.PhysicianName += "<a href=GIPAP.aspx?trgt=physicianinfo&choice=" + ds.Tables[10].Rows[phyK]["personid"].ToString() + "><font color=blue>" + ds.Tables[10].Rows[phyK]["PhysicianName"].ToString() + "</font></a>";
					}
					else
					{
						this.PhysicianName += "<a href=GIPAP.aspx?trgt=physicianinfo&choice=" + ds.Tables[10].Rows[phyK]["personid"].ToString() + "><font color=red>" + ds.Tables[10].Rows[phyK]["PhysicianName"].ToString() + "</font></a>";
					}
                    //flag PHYSICIAN NOA - checks if physician is noa
                    if (Convert.ToBoolean(ds.Tables[10].Rows[phyK]["noa"]) && chkPhys)
                    {
                        try
                        {
                            if (Convert.ToDateTime(ds.Tables[10].Rows[phyK]["noadate"]) <= DateTime.Now)
                            {
                                this.NOAPhys = true;
                                this.VerificationComplete = false;
                                chkPhys = false;
                            }
                            else { this.NOAPhys = false; }
                        }
                        catch { }
                    }
                    else { this.NOAPhys = false; }
				}
			}

			if(ds.Tables[11].Rows.Count > 0)
			{
				this.ProgramOfficerName = ds.Tables[11].Rows[0]["ProgramOfficerName"].ToString();
				for(int poK = 1; poK<ds.Tables[11].Rows.Count; poK++)
				{
					this.ProgramOfficerName += " / " + ds.Tables[11].Rows[poK]["ProgramOfficerName"].ToString();
				}
			}
			
			//contact
			if(ds.Tables[12].Rows.Count > 0)
			{
				if(Urole == "TMFUser" || Urole == "MaxStation")
				{
					this.ContactName = "<a href=GIPAP.aspx?trgt=contactinfo&choice=" + ds.Tables[12].Rows[0]["personid"].ToString() + "&patid=" + this.PatientID.ToString() + ">" + ds.Tables[12].Rows[0]["ContactName"].ToString() + "</a>";
					for(int poK = 1; poK<ds.Tables[12].Rows.Count; poK++)
					{
						this.ContactName += " / " + "<a href=GIPAP.aspx?trgt=contactinfo&choice=" + ds.Tables[12].Rows[poK]["personid"].ToString() + "&patid=" + this.PatientID.ToString() + ">" + ds.Tables[12].Rows[poK]["ContactName"].ToString() + "</a>";
					}
				}
				else
				{
					this.ContactName = ds.Tables[12].Rows[0]["ContactName"].ToString();
					for(int poK = 1; poK<ds.Tables[12].Rows.Count; poK++)
					{
						this.ContactName += " / " + ds.Tables[12].Rows[poK]["ContactName"].ToString();
					}
				}
			}

            //FC Branch
            if (ds.Tables[29].Rows.Count > 0)
            {
                this.FCBranch = ds.Tables[29].Rows[0]["Office"].ToString();
                for (int poK = 1; poK < ds.Tables[29].Rows.Count; poK++)
                {
                    this.FCBranch += " / " + ds.Tables[29].Rows[poK]["Office"].ToString();
                }
            }

            //Distributor
            if (ds.Tables[30].Rows.Count > 0)
            {
                this.DistributorName = ds.Tables[30].Rows[0]["Distributor"].ToString();
                for (int poK = 1; poK < ds.Tables[30].Rows.Count; poK++)
                {
                    this.DistributorName += "<li>" + ds.Tables[30].Rows[poK]["Distributor"].ToString() + "</li>";
                }
            }

			// +/-
			if(Urole == "TMFUser")
			{
				this.MaxStationName += " (<a href=GIPAP.aspx?trgt=addremove&choice=" + this.PatientID.ToString() + "&Sender=Patient&AddType=MaxStation><font color=blue>+/-</font></a>)";
				this.PhysicianName += " (<a href=GIPAP.aspx?trgt=addremove&choice=" + this.PatientID.ToString() + "&Sender=Patient&AddType=Physician><font color=blue>+/-</font></a>)";
				this.ProgramOfficerName += " (<a href=GIPAP.aspx?trgt=addremove&choice=" + this.PatientID.ToString() + "&Sender=Patient&AddType=TMFUser><font color=blue>+/-</font></a>)";
                this.ContactName += " (<a href=GIPAP.aspx?trgt=contactinfo&choice=0&patid=" + this.PatientID.ToString() + "><font color=blue>add</font></a>)";
            }
			if(Urole == "MaxStation")
			{
                this.PhysicianName += " (<a href=GIPAP.aspx?trgt=physiciantransferrequest&choice=" + this.PatientID.ToString() + "&Sender=Patient&AddType=Physician><font color=blue>+/-</font></a>)";
				this.ContactName += " (<a href=GIPAP.aspx?trgt=contactinfo&choice=0&patid=" + this.PatientID.ToString() +"><font color=blue>add</font></a>)";
			}
            if (Urole == "TMFUser" || Urole == "MaxStation" || Urole == "FCFreedomDesk")
            {
                this.FCBranch += " (<a href=GIPAP.aspx?trgt=addremove&choice=" + this.PatientID.ToString() + "&Sender=Patient&AddType=FCBranch><font color=blue>+/-</font></a>)";
            }

			//verification / NOA FEF
            if (this.FlagNOA)
            {
                this.NOAPIN = ds.Tables[13].Rows[0]["noapin"].ToString();
                try
                {
                    this.DonationLength = Convert.ToInt32(ds.Tables[13].Rows[0]["donationlength"]);
                }
                catch
                {
                    this.DonationLength = -1;
                }
                this.DonationLengthUnit = ds.Tables[13].Rows[0]["donationlengthunit"].ToString();
                this.PaymentOption = ds.Tables[13].Rows[0]["PaymentOption"].ToString();
                this.MSContacted = Convert.ToBoolean(ds.Tables[13].Rows[0]["mscontacted"]);
                try
                {
                    this.YearlyReassess = Convert.ToBoolean(ds.Tables[13].Rows[0]["yearlyreassess"]);
                }
                catch { }
                if (this.YearlyReassess && this.GIPAPStatus == "Active")
                {
                    this.ReassessDate = Convert.ToDateTime(ds.Tables[13].Rows[0]["reassessdate"]);
                }
                this.Recommendation = ds.Tables[13].Rows[0]["recommendation"].ToString();
                //see if the verification is completed properly
                if(this.NOAPIN != "" && this.Recommendation == "Approve" && this.DonationLength != -1)
                {
                    if (this.DonationLength == 12 || this.DonationLength == 370 || this.DonationLength == 52)
                    {
                        this.VerificationComplete = true;
                    }
                    else if (this.MSContacted && this.PaymentOption != "")
                    {
                        this.VerificationComplete = true;
                    }
                    else
                    {
                        this.VerificationComplete = false;
                    }

                }
                else
                {
                    this.VerificationComplete = false;
                }
                //now make sure there is not a new FEF required
                if (this.VerificationComplete)
                {
                    //see if a new fef has been collected for re-assessment
                    if (this.GIPAPStatus == "Denied")
                    {
                        if (Convert.ToBoolean(ds.Tables[13].Rows[0]["denied"]))
                        {
                            this.VerificationComplete = false;
                        }
                    }
                    // see if you need a new fef to re-activate
                    else if (this.GIPAPStatus == "Closed")
                    {
                        try
                        {
                            if (Convert.ToDateTime(ds.Tables[13].Rows[0]["reassessdate"]) <= DateTime.Today.AddMonths(2) && this.YearlyReassess)
                            {
                                this.VerificationComplete = false;
                            }
                        }
                        catch { }
                    }
                    // see if yearly re-assessment is needed
                    else if (this.GIPAPStatus == "Active")
                    {
                        if (this.YearlyReassess)
                        {
                            if (this.ReassessDate.AddMonths(-2) <= DateTime.Today)
                            {
                                this.VerificationComplete = false;
                            }
                        }
                    }
                }
                    
            }
            else
            {
                if (ds.Tables[13].Rows.Count > 0)
                {
                    this.VerificationID = Convert.ToInt32(ds.Tables[13].Rows[0]["VerificationID"]);
                    try
                    {
                        if (Convert.ToInt32(ds.Tables[13].Rows[0]["FINANCIALAFFIDAVIT"]) == 1)
                        {
                            this.FinancialAffidavit = true;
                        }
                        else
                        {
                            if (this.FinancialDeclarationDate < this.IntakeDate)
                            {
                                this.FinancialAffidavit = false;
                            }
                            else
                            {
                                this.FinancialAffidavit = true;
                            }
                        }
                    }
                    catch
                    {
                        this.FinancialAffidavit = true;
                    }
                    if (ds.Tables[13].Rows[0]["RECOMMENDATION"].ToString() != "")
                    {
                        this.VerificationComplete = true;
                        this.VerificationRed = (Convert.ToBoolean(ds.Tables[13].Rows[0]["INSURANCE"]) || Convert.ToBoolean(ds.Tables[13].Rows[0]["COVERSRX"]) || Convert.ToBoolean(ds.Tables[13].Rows[0]["COVERSCANCERRX"]) || Convert.ToBoolean(ds.Tables[13].Rows[0]["COVERSGLIVECRX"]) || (ds.Tables[13].Rows[0]["RECOMMENDATION"].ToString() != "Approve") || !this.FinancialAffidavit);
                    }
                    else
                    {
                        this.VerificationComplete = false;
                    }
                }
                else
                {
                    this.VerificationID = 0;
                }
            }

			//close date
			if(ds.Tables[15].Rows.Count > 0)
			{
				this.ClosedDate = Convert.ToDateTime(ds.Tables[15].Rows[0]["ClosedDate"]);
				this.StatusReason = ds.Tables[15].Rows[0]["StatusReason"].ToString();
			}
			//deny date
			if(ds.Tables[16].Rows.Count > 0)
			{
				this.DeniedDate = Convert.ToDateTime(ds.Tables[16].Rows[0]["DeniedDate"]);
				if(this.DeniedDate > this.StartDate)
				{
					this.StatusReason = ds.Tables[16].Rows[0]["StatusReason"].ToString();
				}
			}
			//extended date
			if(ds.Tables[17].Rows.Count > 0)
			{
				this.EndDate = Convert.ToDateTime(ds.Tables[17].Rows[0]["EndDate"]);
			}

			this.CPOCount = ds.Tables[18].Rows.Count;

			this.ReapprovalRequestCount = ds.Tables[20].Rows.Count;
			this.OtherRequestCount = ds.Tables[21].Rows.Count;

			//dosage change
			if(ds.Tables[22].Rows.Count > 0)
			{
				this.CurrentDosage = ds.Tables[22].Rows[0]["currentdosage"].ToString();
			}

			this.FEFUpdateCount = ds.Tables[25].Rows.Count;
            this.PhysTranRequests = Convert.ToInt32(ds.Tables[28].Rows[0]["count"]);
			this.patientDS = ds;
	}
        //**************************************************************************************************************
        public void InflateEmail(int currid)
        {
            DataSet ds;
            SqlParameter arrParams = new SqlParameter("@PatientID", SqlDbType.Int);
                arrParams.Value = currid;

                ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientProfile2", arrParams);
            
            //Populates the objects parameters with the data returned from the database
            this.PatientID = (int)(ds.Tables[0].Rows[0]["PatientID"]);
            this.PIN = (ds.Tables[0].Rows[0]["PIN"]).ToString();
            this.FirstName = (ds.Tables[0].Rows[0]["FirstName"]).ToString();
            this.LastName = (ds.Tables[0].Rows[0]["LastName"]).ToString();
            this.Sex = (ds.Tables[0].Rows[0]["Sex"]).ToString();
            this.BirthDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"]);
            this.Street1 = (ds.Tables[0].Rows[0]["Street1"]).ToString();
            this.Street2 = (ds.Tables[0].Rows[0]["Street2"]).ToString();
            this.City = (ds.Tables[0].Rows[0]["City"]).ToString();
            this.StateProvince = (ds.Tables[0].Rows[0]["StateProvince"]).ToString();
            this.PostalCode = (ds.Tables[0].Rows[0]["PostalCode"]).ToString();
            this.Phone = (ds.Tables[0].Rows[0]["Phone"]).ToString();
            this.Fax = (ds.Tables[0].Rows[0]["Fax"]).ToString();
            this.Email = (ds.Tables[0].Rows[0]["Email"]).ToString();
            this.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
            this.AnnualIncome = (ds.Tables[0].Rows[0]["AnnualIncome"]).ToString();
            this.Occupation = (ds.Tables[0].Rows[0]["Occupation"]).ToString();
            this.IntakeDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IntakeDate"]);
            try
            {
                this.IADate = Convert.ToDateTime(ds.Tables[0].Rows[0]["IADate"]);
            }
            catch { }
            this.AppliedForGipap = Convert.ToBoolean(ds.Tables[0].Rows[0]["AppliedForGipap"]);
            this.PatientConsent = Convert.ToBoolean(ds.Tables[0].Rows[0]["patientconsent"]);
            this.OriginalRequestedDosage = (ds.Tables[0].Rows[0]["OriginalRequestedDosage"]).ToString();
            this.OriginalApprovedDosage = (ds.Tables[0].Rows[0]["OriginalApprovedDosage"]).ToString();
            this.GIPAPStatus = (ds.Tables[0].Rows[0]["GIPAPStatus"]).ToString();
            this.StatusReason = ds.Tables[0].Rows[0]["StatusReason"].ToString();
            this.EnableAutoApprove = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnableAutoApprove"]);
            this.EnableAutoClose = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnableAutoClose"]);
            //NOA
            this.FlagNOA = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOA"]);
            this.Treatment = ds.Tables[0].Rows[0]["treatment"].ToString();
            try
            {
                this.ApplicantID = Convert.ToInt32(ds.Tables[0].Rows[0]["ApplicantID"]);
            }
            catch
            {
                this.ApplicantID = 0;
            }
            this.Diagnosis = ds.Tables[0].Rows[0]["Diagnosis"].ToString();
            try
            {
                this.DiagnosisDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DiagnosisDate"]);
            }
            catch { }
            try
            {
                this.PhysicianClose = Convert.ToBoolean(ds.Tables[0].Rows[0]["PhysicianClose"]);
            }
            catch { }
            try
            {
                this.GlivecStartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["GlivecStartDate"]);
            }
            catch { }
            try
            {
                this.Glivec = Convert.ToBoolean(ds.Tables[0].Rows[0]["Glivec"]);
            }
            catch { }
            //1 GIPAP DETAILS
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.StatusReason = (ds.Tables[1].Rows[0]["StatusReason"]).ToString();
                try
                {
                    this.StartDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["StartDate"]);
                }
                catch { }
                try
                {
                    this.EndDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["EndDate"]);
                }
                catch { }
                this.CurrentDosage = (ds.Tables[1].Rows[0]["CurrentDosage"]).ToString();
                this.ReminderEmail = Convert.ToBoolean(ds.Tables[1].Rows[0]["ReminderEmail"]);
                this.ReminderEmail90 = Convert.ToBoolean(ds.Tables[1].Rows[0]["ReminderEmail90"]);
                this.AutoApprove = Convert.ToBoolean(ds.Tables[1].Rows[0]["AutoApprove"]);
            } 
            //2 country
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.CountryName = ds.Tables[2].Rows[0]["Country"].ToString();
                this.CountryID = Convert.ToInt32(ds.Tables[2].Rows[0]["CountryID"]);
                this.CountryCode = ds.Tables[2].Rows[0]["CountryCode"].ToString();
                this.NeedInterferonInfo = Convert.ToBoolean(ds.Tables[2].Rows[0]["NeedInterferonInfo"]);
                this.NeedFinancialInfo = Convert.ToBoolean(ds.Tables[2].Rows[0]["NeedFinancialInfo"]);
                try
                {
                    this.FinancialDeclarationDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["FinancialDeclarationDate"].ToString());
                }
                catch
                {
                    this.FinancialDeclarationDate = DateTime.Today.AddDays(1);
                }
                this.PediatricAge = Convert.ToInt32(ds.Tables[2].Rows[0]["PediatricAge"]);
                if (this.PediatricAge == 0)
                {
                    this.PediatricAge = 16;
                }
                this.GistApproved = Convert.ToBoolean(ds.Tables[2].Rows[0]["GistApproved"]);
                this.CountryEmail = ds.Tables[2].Rows[0]["Email"].ToString();
            }
            //3 max station
            if (ds.Tables[3].Rows.Count > 0)
            {

            }
            //4 physician 
            this.PhysicianCount = ds.Tables[4].Rows.Count;
            if (this.PhysicianCount > 0)
            {
                bool chkPhys = true;
                for (int phyK = 0; phyK < ds.Tables[4].Rows.Count; phyK++)
                {
                    //flag PHYSICIAN NOA - checks if physician is noa
                    if (Convert.ToBoolean(ds.Tables[4].Rows[phyK]["noa"]) && chkPhys)
                    {
                        try
                        {
                            if (Convert.ToDateTime(ds.Tables[4].Rows[phyK]["noadate"]) <= DateTime.Now)
                            {
                                this.NOAPhys = true;
                                this.VerificationComplete = false;
                                chkPhys = false;
                            }
                            else { this.NOAPhys = false; }
                        }
                        catch { }
                    }
                    else { this.NOAPhys = false; }
                }
            }

            //5 contact
            if (ds.Tables[5].Rows.Count > 0)
            {
                //
            }

            //6 close date
            if (ds.Tables[6].Rows.Count > 0)
            {
                this.ClosedDate = Convert.ToDateTime(ds.Tables[6].Rows[0]["ClosedDate"]);
                this.StatusReason = ds.Tables[6].Rows[0]["StatusReason"].ToString();
            }
            //7 deny date
            if (ds.Tables[7].Rows.Count > 0)
            {
                this.DeniedDate = Convert.ToDateTime(ds.Tables[7].Rows[0]["DeniedDate"]);
                if (this.DeniedDate > this.StartDate)
                {
                    this.StatusReason = ds.Tables[7].Rows[0]["StatusReason"].ToString();
                }
            }
            //8 extended date
            if (ds.Tables[8].Rows.Count > 0)
            {
                this.EndDate = Convert.ToDateTime(ds.Tables[8].Rows[0]["EndDate"]);
            }
            //9 cpo
            this.CPOCount = ds.Tables[9].Rows.Count;

            //10 dosage change
            if (ds.Tables[10].Rows.Count > 0)
            {
                this.CurrentDosage = ds.Tables[10].Rows[0]["currentdosage"].ToString();
            }
            //11 programofficer 
            if (ds.Tables[11].Rows.Count > 0)
            {
                //
            }
            //12 reactivationreason 
            if (ds.Tables[12].Rows.Count > 0)
            {
                //
            }
            //12 verification / NOA FEF
            if (this.FlagNOA)
            {
                this.NOAPIN = ds.Tables[13].Rows[0]["noapin"].ToString();
                try
                {
                    this.DonationLength = Convert.ToInt32(ds.Tables[13].Rows[0]["donationlength"]);
                }
                catch
                {
                    this.DonationLength = -1;
                }
                this.DonationLengthUnit = ds.Tables[13].Rows[0]["donationlengthunit"].ToString();
                this.PaymentOption = ds.Tables[13].Rows[0]["PaymentOption"].ToString();
                this.MSContacted = Convert.ToBoolean(ds.Tables[13].Rows[0]["mscontacted"]);
                this.Fixed = Convert.ToBoolean(ds.Tables[13].Rows[0]["fixed"]);
                try
                {
                    this.YearlyReassess = Convert.ToBoolean(ds.Tables[13].Rows[0]["yearlyreassess"]);
                }
                catch { }
                if (this.YearlyReassess && this.GIPAPStatus == "Active")
                {
                    this.ReassessDate = Convert.ToDateTime(ds.Tables[13].Rows[0]["reassessdate"]);
                }
                this.Recommendation = ds.Tables[13].Rows[0]["recommendation"].ToString();
                //see if the verification is completed properly
                if (this.NOAPIN != "" && this.Recommendation == "Approve" && this.DonationLength != -1)
                {
                    /*if (this.DonationLength == 12 || this.DonationLength == 370 || this.DonationLength == 52)
                    {
                        this.VerificationComplete = true;
                    }
                    else*/ if (this.MSContacted && this.PaymentOption != "")
                    {
                        this.VerificationComplete = true;
                    }
                    else
                    {
                        this.VerificationComplete = false;
                    }

                }
                else
                {
                    this.VerificationComplete = false;
                }
                //now make sure there is not a new FEF required
                if (this.VerificationComplete)
                {
                    //see if a new fef has been collected for re-assessment
                    if (this.GIPAPStatus == "Denied")
                    {
                        if (Convert.ToBoolean(ds.Tables[13].Rows[0]["denied"]))
                        {
                            this.VerificationComplete = false;
                        }
                    }
                    // see if you need a new fef to re-activate
                    else if (this.GIPAPStatus == "Closed")
                    {
                        try
                        {
                            if (Convert.ToDateTime(ds.Tables[13].Rows[0]["reassessdate"]) <= DateTime.Today.AddMonths(2) && this.YearlyReassess)
                            {
                                this.VerificationComplete = false;
                            }
                        }
                        catch { }
                    }
                    // see if yearly re-assessment is needed
                    else if (this.GIPAPStatus == "Active")
                    {
                        if (this.YearlyReassess)
                        {
                            if (this.ReassessDate.AddMonths(-2) <= DateTime.Today)
                            {
                                this.VerificationComplete = false;
                            }
                        }
                    }
                }
                //14 FE BRANCH
                if (ds.Tables[14].Rows.Count > 0)
                {
                    //
                }

                //15 DISEASE INFO
                if (this.Diagnosis == "CML")
                {
                    this.PhilPos = Convert.ToInt32(ds.Tables[15].Rows[0]["PhilPos"]);
                    this.BCR = Convert.ToInt32(ds.Tables[15].Rows[0]["BCR"]);
                    this.OriginalCMLPhase = Convert.ToString(ds.Tables[15].Rows[0]["OriginalCMLPhase"]);
                    this.CurrentCMLPhase = Convert.ToString(ds.Tables[15].Rows[0]["CurrentCMLPhase"]);
                    //country table
                    this.PediatricApproved = Convert.ToBoolean(ds.Tables[2].Rows[0]["PediatricApproved"]);

                    /*if (ds.Tables[16].Rows.Count > 0)
                    {
                        this.Interferon = Convert.ToBoolean(ds.Tables[16].Rows[0]["Interferon"]);
                        //old
                        try
                        {
                            this.Refractory = Convert.ToBoolean(ds.Tables[16].Rows[0]["Refractory"]);
                            this.Unresponsive = Convert.ToBoolean(ds.Tables[16].Rows[0]["Unresponsive"]);
                        }
                        catch { }
                        //new
                        this.InterferonTimeLength = Convert.ToString(ds.Tables[16].Rows[0]["InterferonTimeLength"]);
                        try
                        {
                            this.Intolerant = Convert.ToBoolean(ds.Tables[16].Rows[0]["Intolerant"]);
                        }
                        catch { this.Intolerant = false; }
                        try
                        {
                            this.HematologicFailure = Convert.ToBoolean(ds.Tables[16].Rows[0]["HematologicFailure"]);
                        }
                        catch { this.HematologicFailure = false; }
                        try
                        {
                            this.CytogeneticFailure = Convert.ToBoolean(ds.Tables[16].Rows[0]["CytogeneticFailure"]);
                        }
                        catch { this.CytogeneticFailure = false; }
                    }*/
                }
                else if (this.Diagnosis == "Ph+ ALL")
                {
                    this.PhilPos = Convert.ToInt32(ds.Tables[15].Rows[0]["PhilPos"]);
                    this.BCR = Convert.ToInt32(ds.Tables[15].Rows[0]["BCR"]);
                    this.RelapsedRefractory = Convert.ToInt32(ds.Tables[15].Rows[0]["RelapsedRefractory"]);
                    this.Chemo = Convert.ToInt32(ds.Tables[15].Rows[0]["Chemo"]);
                    this.PediatricApproved = false;
                }
                else if (this.Diagnosis == "DFSP")
                {
                    this.Recurrent = Convert.ToBoolean(ds.Tables[15].Rows[0]["recurrent"]);
                    //coutry
                    this.PediatricApproved = Convert.ToBoolean(ds.Tables[2].Rows[0]["dfspPedApproved"]);
                }
                else if (this.Diagnosis == "GIST")
                {
                    this.CKitPos = Convert.ToInt32(ds.Tables[15].Rows[0]["CKitPositive"]);
                    this.Unresectable = Convert.ToInt32(ds.Tables[15].Rows[0]["Unresectable"]);
                    this.Metastatic = Convert.ToInt32(ds.Tables[15].Rows[0]["Metastatic"]);
                    //country
                    this.PediatricApproved = Convert.ToBoolean(ds.Tables[2].Rows[0]["gistPedApproved"]);
                }

            }
            // NOT NOA
            else
            {
                if (ds.Tables[13].Rows.Count > 0)
                {
                    this.VerificationID = Convert.ToInt32(ds.Tables[13].Rows[0]["VerificationID"]);
                    try
                    {
                        if (Convert.ToInt32(ds.Tables[13].Rows[0]["FINANCIALAFFIDAVIT"]) == 1)
                        {
                            this.FinancialAffidavit = true;
                        }
                        else
                        {
                            if (this.FinancialDeclarationDate < this.IntakeDate)
                            {
                                this.FinancialAffidavit = false;
                            }
                            else
                            {
                                this.FinancialAffidavit = true;
                            }
                        }
                    }
                    catch
                    {
                        this.FinancialAffidavit = true;
                    }
                    if (ds.Tables[13].Rows[0]["RECOMMENDATION"].ToString() != "")
                    {
                        this.VerificationComplete = true;
                        this.VerificationRed = (Convert.ToBoolean(ds.Tables[13].Rows[0]["INSURANCE"]) || Convert.ToBoolean(ds.Tables[13].Rows[0]["COVERSRX"]) || Convert.ToBoolean(ds.Tables[13].Rows[0]["COVERSCANCERRX"]) || Convert.ToBoolean(ds.Tables[13].Rows[0]["COVERSGLIVECRX"]) || (ds.Tables[13].Rows[0]["RECOMMENDATION"].ToString() != "Approve") || !this.FinancialAffidavit);
                    }
                    else
                    {
                        this.VerificationComplete = false;
                    }
                }
                else
                {
                    this.VerificationID = 0;
                }
            }
            this.patientDS = ds;
            ds.Dispose();
        }
		//**************************************************************************************************************
		public int PatientID
		{
			get{return mPatientID;}
			set{mPatientID = value;}
		}

		//**************************************************************************************************************
		public string PIN
		{
			get{return mPIN;}
			set{mPIN = value;}
		}

		//**************************************************************************************************************
		public string FirstName
		{
			get{return mFirstName;}
			set{mFirstName = value;}
		}

		//**************************************************************************************************************
		public string LastName
		{
			get{return mLastName;}
			set{mLastName = value;}
		}

        //*********************************************************************************************************************
        public string ThaiName
        {
            get { return mThaiName; }
            set { mThaiName = value; }
        }
		//**************************************************************************************************************
		public string Sex
		{
			get{return mSex;}
			set{mSex = value;}
		}

		//**************************************************************************************************************
		public DateTime BirthDate
		{
			get{return mBirthDate;}
			set{mBirthDate = value;}
		}

		//**************************************************************************************************************
		public string Street1
		{
			get{return mStreet1;}
			set{mStreet1 = value;}
		}

		//**************************************************************************************************************
		public string Street2
		{
			get{return mStreet2;}
			set{mStreet2 = value;}
		}

		//**************************************************************************************************************
		public string City
		{
			get{return mCity;}
			set{mCity = value;}
		}

		//**************************************************************************************************************
		public string StateProvince
		{
			get{return mStateProvince;}
			set{mStateProvince = value;}
		}

		//**************************************************************************************************************
		public string PostalCode
		{
			get{return mPostalCode;}
			set{mPostalCode = value;}
		}

		//**************************************************************************************************************
		public string Phone
		{
			get{return mPhone;}
			set{mPhone = value;}
		}

		//**************************************************************************************************************
		public string Fax
		{
			get{return mFax;}
			set{mFax = value;}
		}

		//**************************************************************************************************************
		public string Email
		{
			get{return mEmail;}
			set{mEmail = value;}
		}

		//**************************************************************************************************************
		public string Mobile
		{
			get{return mMobile;}
			set{mMobile = value;}
		}

		//**************************************************************************************************************
		public string AnnualIncome
		{
			get{return mAnnualIncome;}
			set{mAnnualIncome = value;}
		}

		//**************************************************************************************************************
		public string Occupation
		{
			get{return mOccupation;}
			set{mOccupation = value;}
		}
		//**************************************************************************************************************
		public DateTime IntakeDate
		{
			get{return mIntakeDate;}
			set{mIntakeDate = value;}
		}
		//**************************************************************************************************************
		public DateTime IADate
		{
			get{return mIADate;}
			set{mIADate = value;}
		}
		//**************************************************************************************************************
		public bool AppliedForGipap
		{
			get{return mAppliedForGipap;}
			set{mAppliedForGipap = value;}
		}
		//**************************************************************************************************************
		public bool PatientConsent
		{
			get{return mPatientConsent;}
			set{mPatientConsent = value;}
		}

		//**************************************************************************************************************
		public string OriginalRequestedDosage
		{
			get{return mOriginalRequestedDosage;}
			set{mOriginalRequestedDosage = value;}
		}
		//**************************************************************************************************************
		public string OriginalApprovedDosage
		{
			get{return mOriginalApprovedDosage;}
			set{mOriginalApprovedDosage = value;}
		}
		//**************************************************************************************************************
		public string GIPAPStatus
		{
			get{return mGIPAPStatus;}
			set{mGIPAPStatus = value;}
		}

		//**************************************************************************************************************
		public string StatusReason
		{
			get{return mStatusReason;}
			set{mStatusReason = value;}
		}

		//**************************************************************************************************************
		public string Diagnosis
		{
			get{return mDiagnosis;}
			set{mDiagnosis = value;}
		}

		//**************************************************************************************************************
		public DateTime DiagnosisDate
		{
			get{return mDiagnosisDate;}
			set{mDiagnosisDate = value;}
		}

		//**************************************************************************************************************
		public bool EnableAutoApprove
		{
			get{return mEnableAutoApprove;}
			set{mEnableAutoApprove = value;}
		}
		//**************************************************************************************************************
		public bool EnableAutoClose
		{
			get{return mEnableAutoClose;}
			set{mEnableAutoClose = value;}
		}

		//**************************************************************************************************************
		public DateTime DeniedDate
		{
			get{return mDeniedDate;}
			set{mDeniedDate = value;}
		}

		//**************************************************************************************************************
		public DateTime ClosedDate
		{
			get{return mClosedDate;}
			set{mClosedDate = value;}
		}
		//DFSP
		//*********************************************************************************************************************
		public bool Recurrent
		{
			get{return mRecurrent;}
			set{mRecurrent=value;}
		}
        //*********************************************************************************************************************
        public bool Adjuvant
        {
            get { return mAdjuvant; }
            set { mAdjuvant = value; }
        }
        //*********************************************************************************************************************
        public int HighRisk
        {
            get { return mHighRisk; }
            set { mHighRisk = value; }
        }
        //**************************************************************************************************************
        public string DiagSummary
        {
            get { return mDiagSummary; }
            set { mDiagSummary = value; }
        }
		//**********************************************************************************************************************
		public int PhilPos
		{
			get{return mPhilPos;}
			set{mPhilPos = value;}
		}

		//**********************************************************************************************************************
		public int BCR
		{
			get{return mBCR;}
			set{mBCR = value;}
		}
		//*********************************************************************************************************************
		public int RelapsedRefractory
		{
			get{return mRelapsedRefractory;}
			set{mRelapsedRefractory=value;}
		}

		//*********************************************************************************************************************
		public int Chemo
		{
			get{return mChemo;}
			set{mChemo=value;}
		}
		//**********************************************************************************************************************
		public string OriginalCMLPhase
		{
			get{return mOriginalCMLPhase;}
			set{mOriginalCMLPhase = value;}
		}
		//**********************************************************************************************************************
		public string CurrentCMLPhase
		{
			get{return mCurrentCMLPhase;}
			set{mCurrentCMLPhase = value;}
		}

		//**********************************************************************************************************************
		public bool Interferon
		{
			get{return mInterferon;}
			set{mInterferon = value;}
		}
		//*********************************************************************************************************************
		public string InterferonTimeLength
		{
			get{return mInterferonTimeLength;}
			set{mInterferonTimeLength=value;}
		}
		//*********************************************************************************************************************
		public bool Intolerant
		{
			get{return mIntolerant;}
			set{mIntolerant=value;}
		}

		//*********************************************************************************************************************
		public bool HematologicFailure
		{
			get{return mHematologicFailure;}
			set{mHematologicFailure=value;}
		}

		//*********************************************************************************************************************
		public bool CytogeneticFailure
		{
			get{return mCytogeneticFailure;}
			set{mCytogeneticFailure=value;}
		}
		//**********************************************************************************************************************
		public int CKitPos
		{
			get{return mCKitPos;}
			set{mCKitPos = value;}
		}
		//*********************************************************************************************************************
		public int Unresectable
		{
			get{return mUnresectable;}
			set{mUnresectable=value;}
		}
		//*********************************************************************************************************************
		public int Metastatic
		{
			get{return mMetastatic;}
			set{mMetastatic=value;}
		}
		//*********************************************************************************************************************
		public int ApplicantID
		{
			get{return mApplicantID;}
			set{mApplicantID=value;}
		}
		//**********************************************************************************************************************
		public bool Refractory
		{
			get{return mRefractory;}
			set{mRefractory = value;}
		}

		//**********************************************************************************************************************
		public bool Unresponsive
		{
			get{return mUnresponsive;}
			set{mUnresponsive = value;}
		}
		//**************************************************************************************************************
		public DateTime StartDate
		{
			get{return mStartDate;}
			set{mStartDate = value;}
		}

		//**************************************************************************************************************
		public DateTime EndDate
		{
			get{return mEndDate;}
			set{mEndDate = value;}
		}

		//**************************************************************************************************************
		public string CurrentDosage
		{
			get{return mCurrentDosage;}
			set{mCurrentDosage = value;}
		}

		//**************************************************************************************************************
		public bool PhysicianClose
		{
			get{return mPhysicianClose;}
			set{mPhysicianClose = value;}
		}

		//**************************************************************************************************************
		public bool Glivec
		{
			get{return mGlivec;}
			set{mGlivec = value;}
		}
		//**************************************************************************************************************
		public DateTime GlivecStartDate
		{
			get{return mGlivecStartDate;}
			set{mGlivecStartDate = value;}
		}

		//**************************************************************************************************************
		public bool ReminderEmail
		{
			get{return mReminderEmail;}
			set{mReminderEmail = value;}
		}

		//**************************************************************************************************************
		public bool ReminderEmail90
		{
			get{return mReminderEmail90;}
			set{mReminderEmail90 = value;}
		}

		//**************************************************************************************************************
		public bool AutoApprove
		{
			get{return mAutoApprove;}
			set{mAutoApprove = value;}
		}

		//**************************************************************************************************************
		public bool DelayedReapproval
		{
			get{return mDelayedReapproval;}
			set{mDelayedReapproval = value;}
		}
		//**************************************************************************************************************
		public int CountryID
		{
			get{return mCountryID;}
			set{mCountryID = value;}
		}
		//**************************************************************************************************************
		public string CountryName
		{
			get{return mCountryName;}
			set{mCountryName = value;}
		}
		//**************************************************************************************************************
		public string CountryCode
		{
			get{return mCountryCode;}
			set{mCountryCode = value;}
		}
		//**************************************************************************************************************
		public int MaxStationID
		{
			get{return mMaxStationID;}
			set{mMaxStationID = value;}
		}
		//**************************************************************************************************************
		public string MaxStationName
		{
			get{return mMaxStationName;}
			set{mMaxStationName = value;}
		}
		//**************************************************************************************************************
		public int ClinicID
		{
			get{return mClinicID;}
			set{mClinicID = value;}
		}
		//**************************************************************************************************************
		public string ClinicName
		{
			get{return mClinicName;}
			set{mClinicName = value;}
		}
		//**************************************************************************************************************
		public int PhysicianID
		{
			get{return mPhysicianID;}
			set{mPhysicianID = value;}
		}
		//**************************************************************************************************************
		public string PhysicianName
		{
			get{return mPhysicianName;}
			set{mPhysicianName = value;}
        }
        //**************************************************************************************************************
        public bool FlagNOA
        {
            get { return mFlagNOA; }
            set { mFlagNOA = value; }
        }
		//**************************************************************************************************************
		public int ProgramOfficerID
		{
			get{return mProgramOfficerID;}
			set{mProgramOfficerID = value;}
		}
		//**************************************************************************************************************
		public string ProgramOfficerName
		{
			get{return mProgramOfficerName;}
			set{mProgramOfficerName = value;}
		}
		//**************************************************************************************************************
		public string ContactName
		{
			get{return mContactName;}
			set{mContactName = value;}
		}

        //**************************************************************************************************************
        public string DistributorName
        {
            get { return mDistributorName; }
            set { mDistributorName = value; }
        }
        //**************************************************************************************************************
        public string ReqTreatment
        {
            get { return mReqTreatment; }
            set { mReqTreatment = value; }
        }
        //**************************************************************************************************************
        public string ReasonChanged
        {
            get { return mReasonChanged; }
            set { mReasonChanged = value; }
        }
        //TASIGNA FIELDS
        public string Treatment
        {
            get { return mTreatment; }
            set { mTreatment = value; }
        }
        //*********************************************************************************************************************
        public bool Imatinib
        {
            get { return mImatinib; }
            set { mImatinib = value; }
        }
        //*********************************************************************************************************************
        public bool GlivecIntolerant
        {
            get { return mGlivecIntolerant; }
            set { mGlivecIntolerant = value; }
        }
        //*********************************************************************************************************************
        public bool GlivecResistant
        {
            get { return mGlivecResistant; }
            set { mGlivecResistant = value; }
        }
        //*********************************************************************************************************************
        public bool Dasatinib
        {
            get { return mDasatinib; }
            set { mDasatinib = value; }
        }
        //*********************************************************************************************************************
        public bool DasatinibIntolerant
        {
            get { return mDasatinibIntolerant; }
            set { mDasatinibIntolerant = value; }
        }
        //*********************************************************************************************************************
        public bool DasatinibResistant
        {
            get { return mDasatinibResistant; }
            set { mDasatinibResistant = value; }
        }
        //*********************************************************************************************************************
        public bool Tasigna
        {
            get { return mTasigna; }
            set { mTasigna = value; }
        }
        //*********************************************************************************************************************
        public DateTime TasignaStartDate
        {
            get { return mTasignaStartDate; }
            set { mTasignaStartDate = value; }
        }
        //*********************************************************************************************************************
        public string PrevTasignaDose
        {
            get { return mPrevTasignaDose; }
            set { mPrevTasignaDose = value; }
        }
        //*********************************************************************************************************************
        public bool TasignaPatientConsent
        {
            get { return mTasignaPatientConsent; }
            set { mTasignaPatientConsent = value; }
        }
        //*********************************************************************************************************************
        public bool NOATasigna
        {
            get { return mNOATasigna; }
            set { mNOATasigna = value; }
        }
        //*********************************************************************************************************************
        public string OriginalTabletStrength
        {
            get { return mOriginalTabletStrength; }
            set { mOriginalTabletStrength = value; }
        }
	}
}