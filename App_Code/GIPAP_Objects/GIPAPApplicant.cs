using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	public class GIPAPApplicant
	{
		private int mApplicantID;
		private string mFirstName;
        private string mLastName;
        private string mThaiName;
        private bool mFlagNOA;
		private string mSex;
		private DateTime mBirthDate;
		private string mStreet1;
		private string mStreet2;
		private string mCity;
		private string mStateProvince;
		private string mPostalCode;
		private int mCountryID;
		private string mPhone;
		private string mMobile;
		private string mFax;
		private string mEmail;
		private string mContactFirstName;
		private string mContactLastName;
		private string mContactStreet1;
		private string mContactStreet2;
		private string mContactCity;
		private string mContactStateProvince;
		private string mContactPostalCode;
		private int mContactCountryID;
		private string mContactPhone;
		private string mContactMobile;
		private string mContactFax;
		private string mContactEmail;
		private string mContactRelationship;
		private string mRelationshipDetails;
		private int mPhysicianID;
		private string mPhysicianFirstName;
		private string mPhysicianLastName;
		private string mSpecialty;
		private string mClinic;
		private string mPhysicianPhone;
		private string mPhysicianFax;
		private string mPhysicianEmail;
		private bool mAppliedForGIPAP;
		private bool mPatientConsent;
		private string mDosage;
		private string mDiagnosis;
		private DateTime mDiagnosisDate;
		private int mPhilPositive;
		private int mBCR;
		private int mRelapsedRefractory;
		private int mChemo;
		private string mCMLPhase;
		private bool mInterferon;
		private string mInterferonTimeLength;
		private bool mIntolerant;
		private bool mHematologicFailure;
		private bool mCytogeneticFailure;
		private int mCKitPos;
		private int mUnresectable;
		private int mMetastatic;
		//dfsp
		private bool mRecurrent;
        //adj gist
        private bool mAdjuvant;
        private int mHighRisk;
        //mds, mast, hes
        private string mDiagSummary;
        //india order creation
        private string mTabletStrength;

		private bool mGlivec;
		private DateTime mGlivecStartDate;
		private bool mInsurance;
		private bool mCoversRx;
		private bool mCoversCancerRx;
		private bool mCoversGlivecRx;
		private string mAnnualIncome;
		private string mOccupation;
		private string mNotes;
		//verification
		private int mMedicalChart;
		private int mPhiladelphiaVerification;
		private int mCKitVerification;
		private int mCopyOfID;
		private int mPhoto;
		private int mSSCard;
		private int mInsuranceCard;
		private string mInsuranceType;
		private int mTaxReturn;
		private int mSalarySlip;
		private int mFinancialAffidavit;
		private int mPhoneBill;
		private string mOtherDocs;
		private int mHouseholdMembers;
		private string mHouseholdOccupation;
		private string mHouseholdIncome;
		private string mAdditionalFunds;
		private string mHouseholdAssets;
		private string mRecommendation;
		private string mExplanation;
        //TASIGNA FIELDS
        private string mTreatment;
        private bool mImatinib;
        private bool mGlivecIntolerant;
        private bool mGlivecResistant;
        private bool mDasatinib;
        private bool mDasatinibIntolerant;
        private bool mDasatinibResistant;
        private bool mTasigna;
        private DateTime mTasignaStartDate;
        private string mPrevTasignaDose;
        private bool mTasignaPatientConsent;
        private bool mNOATasigna;
        private int TasignaPhys;

		public DataSet ApplicantDS;
		public string CountryName;
		public string NoLinkCountryName;
        private bool GISTApproved;
        private int PediatricAge;
		public DateTime PatientConsentDate;

		private DateTime CreateDate;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAPForms;PWD=secret;UID=sa;";

		//**********************************************************************************************************************
		public GIPAPApplicant()
		{
			this.Clear();
		}

		//**********************************************************************************************************************
		public GIPAPApplicant(int ApplicantID)
		{
			if(ApplicantID == 0)
			{
				return;
			}
			else
			{
				DataSet myData = new DataSet();
				SqlParameter paramPatientID = new SqlParameter("@ApplicantID", SqlDbType.Int);
				paramPatientID.Value = ApplicantID;
				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetGIPAPApplicantProfile2", paramPatientID);

				if (myData.Tables[0].Rows.Count <= 0)
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

		//*********************************************************************************************************************
		public void CreateApplicant(string emailText, string subject, string sentby)
		{			
			SqlParameter[] arParams = new SqlParameter[105];

			arParams[0] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50); 
			arParams[0].Value = this.FirstName.Trim();
			
			arParams[1] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50); 
			arParams[1].Value = this.LastName.Trim();

			arParams[2] = new SqlParameter("@Sex", SqlDbType.Char, 1); 
			arParams[2].Value = this.Sex;
			
			arParams[3] = new SqlParameter("@BirthDate", SqlDbType.SmallDateTime);
			arParams[3].Value = this.BirthDate;

			arParams[4] = new SqlParameter("@Street1", SqlDbType.NVarChar, 100); 
			arParams[4].Value = this.Street1.Trim();

			arParams[5] = new SqlParameter("@Street2", SqlDbType.NVarChar, 100); 
			arParams[5].Value = this.Street2.Trim();

			arParams[6] = new SqlParameter("@City", SqlDbType.NVarChar, 30); 
			arParams[6].Value = this.City.Trim();

			arParams[7] = new SqlParameter("@StateProvince", SqlDbType.NVarChar, 30); 
			arParams[7].Value = this.StateProvince.Trim();

			arParams[8] = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10); 
			arParams[8].Value = this.PostalCode.Trim();

			arParams[9] = new SqlParameter("@CountryID", SqlDbType.Int); 
			arParams[9].Value = this.CountryID;
		
			arParams[10] = new SqlParameter("@Phone", SqlDbType.NVarChar, 50); 
			arParams[10].Value = this.Phone.Trim();

			arParams[11] = new SqlParameter("@Fax", SqlDbType.NVarChar, 50); 
			arParams[11].Value = this.Fax.Trim();

			arParams[12] = new SqlParameter("@Email", SqlDbType.NVarChar, 100); 
			arParams[12].Value = this.Email.Trim();

			arParams[13] = new SqlParameter("@ContactFirstName", SqlDbType.NVarChar, 50); 
			arParams[13].Value = this.ContactFirstName.Trim();

			arParams[14] = new SqlParameter("@ContactLastName", SqlDbType.NVarChar, 50); 
			arParams[14].Value = this.ContactLastName.Trim();

			arParams[15] = new SqlParameter("@ContactStreet1", SqlDbType.NVarChar, 100); 
			arParams[15].Value = this.ContactStreet1.Trim();

			arParams[16] = new SqlParameter("@ContactStreet2", SqlDbType.NVarChar, 100); 
			arParams[16].Value = this.ContactStreet2.Trim();

			arParams[17] = new SqlParameter("@ContactCity", SqlDbType.NVarChar, 30); 
			arParams[17].Value = this.ContactCity.Trim();

			arParams[18] = new SqlParameter("@ContactStateProvince", SqlDbType.NVarChar, 30); 
			arParams[18].Value = this.ContactStateProvince.Trim();

			arParams[19] = new SqlParameter("@ContactPostalCode", SqlDbType.NVarChar, 10); 
			arParams[19].Value = this.ContactPostalCode.Trim();

			arParams[20] = new SqlParameter("@ContactCountryID", SqlDbType.Int); 
			arParams[20].Value = this.ContactCountryID;

			arParams[21] = new SqlParameter("@ContactPhone", SqlDbType.NVarChar, 50); 
			arParams[21].Value = this.ContactPhone.Trim();

			arParams[22] = new SqlParameter("@ContactFax", SqlDbType.NVarChar, 50); 
			arParams[22].Value = this.ContactFax.Trim();
		
			arParams[23] = new SqlParameter("@ContactEmail", SqlDbType.NVarChar, 100); 
			arParams[23].Value = this.ContactEmail.Trim();

			arParams[24] = new SqlParameter("@Relationship", SqlDbType.NVarChar, 20); 
			arParams[24].Value = this.ContactRelationship.Trim();

			arParams[25] = new SqlParameter("@RelationshipDetails", SqlDbType.NVarChar, 200); 
			arParams[25].Value = this.RelationshipDetails.Trim();

			arParams[26] = new SqlParameter("@PhysicianFirstName", SqlDbType.NVarChar, 50); 
			arParams[26].Value = this.PhysicianFirstName.Trim();

			arParams[27] = new SqlParameter("@PhysicianLastName", SqlDbType.NVarChar, 50); 
			arParams[27].Value = this.PhysicianLastName.Trim();

			arParams[28] = new SqlParameter("@Specialty", SqlDbType.NVarChar, 20); 
			arParams[28].Value = this.Specialty.Trim();

			arParams[29] = new SqlParameter("@Clinic", SqlDbType.NVarChar, 100); 
			arParams[29].Value = this.Clinic.Trim();

			arParams[30] = new SqlParameter("@PhysicianPhone", SqlDbType.NVarChar, 50); 
			arParams[30].Value = this.PhysicianPhone.Trim();

			arParams[31] = new SqlParameter("@PhysicianFax", SqlDbType.NVarChar, 50); 
			arParams[31].Value = this.PhysicianFax.Trim();

			arParams[32] = new SqlParameter("@PhysicianEmail", SqlDbType.NVarChar, 100); 
			arParams[32].Value = this.PhysicianEmail.Trim();

			arParams[33] = new SqlParameter("@AppliedForGIPAP", SqlDbType.Bit);
			if(this.AppliedForGIPAP)
				{arParams[33].Value = 1;}
			else
				{arParams[33].Value = 0;}		
	
			arParams[34] = new SqlParameter("@BCR", SqlDbType.Int); 
			arParams[34].Value = this.BCR;

			arParams[35] = new SqlParameter("@Dosage", SqlDbType.NVarChar, 20); 
			arParams[35].Value = this.Dosage.Trim();
			
			arParams[36] = new SqlParameter("@Diagnosis", SqlDbType.NVarChar, 25); 
			arParams[36].Value = this.Diagnosis.Trim();

			arParams[37] = new SqlParameter("@DiagnosisDate", SqlDbType.SmallDateTime);
			arParams[37].Value = this.DiagnosisDate;

			arParams[38] = new SqlParameter("@PhilPos", SqlDbType.Int); 
			arParams[38].Value = this.PhilPositive;

			arParams[39] = new SqlParameter("@CMLPhase", SqlDbType.VarChar, 20); 
			arParams[39].Value = this.CMLPhase.Trim();

			arParams[40] = new SqlParameter("@Interferon", SqlDbType.Bit); 
			if(this.Interferon)
				{arParams[40].Value = 1;}
			else
				{arParams[40].Value = 0;}

			arParams[41] = new SqlParameter("@InterferonTimeLength", SqlDbType.NVarChar, 20); 
			arParams[41].Value = this.InterferonTimeLength.Trim();

			arParams[42] = new SqlParameter("@Intolerant", SqlDbType.Bit); 
			if(this.Intolerant)
				{arParams[42].Value = 1;}
			else
				{arParams[42].Value = 0;}

			arParams[43] = new SqlParameter("@HematologicFailure", SqlDbType.Bit); 
			if(this.HematologicFailure)
			{arParams[43].Value = 1;}
			else
			{arParams[43].Value = 0;}

			arParams[44] = new SqlParameter("@CytogeneticFailure", SqlDbType.Bit); 
			if(this.CytogeneticFailure)
				{arParams[44].Value = 1;}
			else
				{arParams[44].Value = 0;}

			arParams[45] = new SqlParameter("@CKitPositive", SqlDbType.Int); 
			arParams[45].Value = this.CKitPos;

			arParams[46] = new SqlParameter("@Unresectable", SqlDbType.Int); 
			arParams[46].Value = this.Unresectable;

			arParams[47] = new SqlParameter("@Metastatic", SqlDbType.Int); 
			arParams[47].Value = this.Metastatic;			

			arParams[48] = new SqlParameter("@Glivec", SqlDbType.Bit); 
			if(this.Glivec)
				{arParams[48].Value = 1;}
			else
				{arParams[48].Value = 0;}

			arParams[49] = new SqlParameter("@GlivecStartDate", SqlDbType.DateTime); 
			if(this.GlivecStartDate == Convert.ToDateTime("1/1/0001"))
			{
				arParams[49].Value = DBNull.Value;
			}
			else
			{
				arParams[49].Value = this.GlivecStartDate;
			}

			arParams[50] = new SqlParameter("@Insurance", SqlDbType.Bit); 
			if(this.Insurance)
			{arParams[50].Value = 1;}
			else
			{arParams[50].Value = 0;}
			
			arParams[51] = new SqlParameter("@CoversRx", SqlDbType.Bit); 
			if(this.CoversRx)
				{arParams[51].Value = 1;}
			else
				{arParams[51].Value = 0;}

			arParams[52] = new SqlParameter("@CoversCancerRx", SqlDbType.Bit); 
			if(this.CoversCancerRx)
				{arParams[52].Value = 1;}
			else
				{arParams[52].Value = 0;}

			arParams[53] = new SqlParameter("@CoversGlivecRx", SqlDbType.Bit); 
			if(this.CoversGlivecRx)
				{arParams[53].Value = 1;}
			else
				{arParams[53].Value = 0;}

			arParams[54] = new SqlParameter("@AnnualIncome", SqlDbType.NVarChar, 20); 
			arParams[54].Value = this.AnnualIncome.Trim();

			arParams[55] = new SqlParameter("@Occupation", SqlDbType.NVarChar, 50); 
			arParams[55].Value = this.Occupation.Trim();

			arParams[56] = new SqlParameter("@Notes", SqlDbType.Text); 
			arParams[56].Value = this.Notes;

			arParams[57] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 50); 
			arParams[57].Value = this.Mobile.Trim();

			arParams[58] = new SqlParameter("@ContactMobile", SqlDbType.NVarChar, 50); 
			arParams[58].Value = this.ContactMobile.Trim();

			//verification

			arParams[59] = new SqlParameter("@MedicalChart", SqlDbType.Int);
			arParams[59].Value = this.MedicalChart;

			arParams[60] = new SqlParameter("@PhiladelphiaVerification", SqlDbType.Int);
			arParams[60].Value = this.PhiladelphiaVerification;

			arParams[61] = new SqlParameter("@CKitVerification", SqlDbType.Int);
			arParams[61].Value = this.CKitVerification;

			arParams[62] = new SqlParameter("@CopyOfID", SqlDbType.Int);
			arParams[62].Value = this.CopyOfID;

			arParams[63] = new SqlParameter("@Photo", SqlDbType.Int);
			arParams[63].Value = this.Photo;

			arParams[64] = new SqlParameter("@SSCard", SqlDbType.Int);
			arParams[64].Value = this.SSCard;

			arParams[65] = new SqlParameter("@InsuranceCard", SqlDbType.Int);
			arParams[65].Value = this.InsuranceCard;

			arParams[66] = new SqlParameter("@TaxReturn", SqlDbType.Int);
			arParams[66].Value = this.TaxReturn;

			arParams[67] = new SqlParameter("@SalarySlip", SqlDbType.Int);
			arParams[67].Value = this.SalarySlip;

			arParams[68] = new SqlParameter("@PhoneBill", SqlDbType.Int);
			arParams[68].Value = this.PhoneBill;

			arParams[69] = new SqlParameter("@OtherDocs", SqlDbType.VarChar, 500);
			arParams[69].Value = this.OtherDocs;

			arParams[70] = new SqlParameter("@HouseholdMembers", SqlDbType.Int);
			arParams[70].Value = this.HouseholdMembers;

			arParams[71] = new SqlParameter("@HouseholdOccupation", SqlDbType.VarChar, 500);
			arParams[71].Value = this.HouseholdOccupation;

			arParams[72] = new SqlParameter("@HouseholdIncome", SqlDbType.NVarChar, 50);
			arParams[72].Value = this.HouseholdIncome;

			arParams[73] = new SqlParameter("@AdditionalFunds", SqlDbType.NVarChar, 50);
			arParams[73].Value = this.AdditionalFunds;

			arParams[74] = new SqlParameter("@HouseholdAssets", SqlDbType.NVarChar, 50);
			arParams[74].Value = this.HouseholdAssets;

			arParams[75] = new SqlParameter("@Recommendation", SqlDbType.VarChar, 50);
			arParams[75].Value = this.Recommendation;

			arParams[76] = new SqlParameter("@Explanation", SqlDbType.Text);
			arParams[76].Value = this.Explanation;

			arParams[77] = new SqlParameter("@InsuranceType", SqlDbType.VarChar, 50);
			arParams[77].Value = this.InsuranceType;

			arParams[78] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arParams[78].Value = sentby;

			arParams[79] = new SqlParameter("@FinancialAffidavit", SqlDbType.Int);
			arParams[79].Value = this.FinancialAffidavit;

			arParams[80] = new SqlParameter("@PatientConsent", SqlDbType.Bit);
			if(this.PatientConsent)
			{arParams[80].Value = 1;}
			else
			{arParams[80].Value = 0;}

			arParams[81] = new SqlParameter("@PatientConsentDate", SqlDbType.SmallDateTime);
			arParams[81].Direction = ParameterDirection.Output;

			arParams[82] = new SqlParameter("@PhysicianID", SqlDbType.Int);
			arParams[82].Value = this.PhysicianID;

			arParams[83] = new SqlParameter("@RelapsedRefractory", SqlDbType.Int);
			arParams[83].Value = this.RelapsedRefractory;

			arParams[84] = new SqlParameter("@Chemo", SqlDbType.Int);
			arParams[84].Value = this.Chemo;

			arParams[85] = new SqlParameter("@Recurrent", SqlDbType.Bit);
			if(this.Recurrent)
			{arParams[85].Value = 1;}
			else
			{arParams[85].Value = 0;}

            arParams[86] = new SqlParameter("@HighRisk", SqlDbType.Int);
            arParams[86].Value = this.HighRisk;

            arParams[87] = new SqlParameter("@DiagSummary", SqlDbType.Text);
            arParams[87].Value = this.DiagSummary;

            //TASIGNA FIELDS
            arParams[88] = new SqlParameter("@Imatinib", SqlDbType.Bit);
            if (this.Imatinib)
            { arParams[88].Value = 1; }
            else
            { arParams[88].Value = 0; }

            arParams[89] = new SqlParameter("@GlivecIntolerant", SqlDbType.Bit);
            if (this.GlivecIntolerant)
            { arParams[89].Value = 1; }
            else
            { arParams[89].Value = 0; }

            arParams[90] = new SqlParameter("@GlivecResistant", SqlDbType.Bit);
            if (this.GlivecResistant)
            { arParams[90].Value = 1; }
            else
            { arParams[90].Value = 0; }

            arParams[91] = new SqlParameter("@Dasatinib", SqlDbType.Bit);
            if (this.Dasatinib)
            { arParams[91].Value = 1; }
            else
            { arParams[91].Value = 0; }

            arParams[92] = new SqlParameter("@DasatinibIntolerant", SqlDbType.Bit);
            if (this.DasatinibIntolerant)
            { arParams[92].Value = 1; }
            else
            { arParams[92].Value = 0; }

            arParams[93] = new SqlParameter("@DasatinibResistant", SqlDbType.Bit);
            if (this.DasatinibResistant)
            { arParams[93].Value = 1; }
            else
            { arParams[93].Value = 0; }

            arParams[94] = new SqlParameter("@Tasigna", SqlDbType.Bit);
            if (this.Tasigna)
            { arParams[94].Value = 1; }
            else
            { arParams[94].Value = 0; }

            arParams[95] = new SqlParameter("@TasignaStartDate", SqlDbType.SmallDateTime);
            if (this.TasignaStartDate != Convert.ToDateTime("1/1/0001"))
            {
                arParams[95].Value = this.TasignaStartDate;
            }
            else
            {
                arParams[95].Value = DBNull.Value;
            }

            arParams[96] = new SqlParameter("@PrevTasignaDose", SqlDbType.NVarChar, 20);
            arParams[96].Value = this.PrevTasignaDose;

            arParams[97] = new SqlParameter("@TasignaPatientConsent", SqlDbType.Bit);
            arParams[97].Value = this.TasignaPatientConsent;

            arParams[98] = new SqlParameter("@NOATasigna", SqlDbType.Bit);
            if (this.NOATasigna)
            { arParams[98].Value = 1; }
            else
            { arParams[98].Value = 0; }

            arParams[99] = new SqlParameter("@Treatment", SqlDbType.NVarChar, 20);
            arParams[99].Value = this.Treatment;

            arParams[100] = new SqlParameter("@Adjuvant", SqlDbType.Bit);
            if (this.Adjuvant)
            {
                arParams[100].Value = 1;
            }
            else { arParams[100].Value = 0; }

            arParams[101] = new SqlParameter("@NOA", SqlDbType.Bit);
            if (this.FlagNOA)
            {
                arParams[101].Value = 1;
            }
            else { arParams[101].Value = 0; }

            arParams[102] = new SqlParameter("@ApplicantID", SqlDbType.Int);
            arParams[102].Direction = ParameterDirection.Output;

            arParams[103] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
            arParams[103].Value = this.TabletStrength;

            arParams[104] = new SqlParameter("@ThaiName", SqlDbType.NVarChar, 100);
            arParams[104].Value = this.ThaiName;

			//Send the data to the database
			DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CreateApplicant2", arParams);

			try
			{
				this.PatientConsentDate = Convert.ToDateTime(arParams[81].Value);
			}
			catch
			{
				this.PatientConsentDate = DateTime.Today.AddDays(1);
			}
            this.ApplicantID = (int)arParams[102].Value;

			GIPAP_Objects.Email myEmail = new Email();
			//myEmail.To = "gipap@themaxfoundation.org; ";
			if(ds.Tables[0].Rows.Count > 0)
			{
				for(int i=0; i<ds.Tables[0].Rows.Count; i++)
				{
					myEmail.To += ds.Tables[0].Rows[i]["email"].ToString() + "; ";
				}
			}
			myEmail.From = "gipap@themaxfoundation.org";
			myEmail.CC = "";
			myEmail.Subject = subject;
			myEmail.Message = emailText;
			myEmail.Send(sentby);
		}

		//**********************************************************************************************************************
		private void Clear()
		{
			this.ApplicantID = 0;
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
			this.Mobile = "";
			this.Fax = "";
			this.Email = "";
			this.ContactFirstName = "";
			this.ContactLastName = "";
			this.ContactStreet1 = "";
			this.ContactStreet2 = "";
			this.ContactCity = "";
			this.ContactStateProvince = "";
			this.ContactPostalCode = "";
			this.ContactCountryID = 0;
			this.ContactPhone = "";
			this.ContactMobile = "";
			this.ContactFax = "";
			this.ContactEmail = "";
			this.ContactRelationship = "";
			this.RelationshipDetails = "";
			this.PhysicianFirstName = "";
			this.PhysicianLastName = "";
			this.Specialty = "";
			this.Clinic = "";
			this.PhysicianPhone = "";
			this.PhysicianFax = "";
			this.PhysicianEmail = "";
			this.AppliedForGIPAP = false;
			this.Dosage = "";
			this.Diagnosis = "";
			this.PhilPositive = 0;
			this.CMLPhase = "";
			this.Interferon = false;
			this.HematologicFailure = false;
			this.CytogeneticFailure = false;
			this.Intolerant = false;
			this.CoversRx = false;
			this.CoversCancerRx = false;
			this.CoversGlivecRx = false;
			this.Occupation = "";
			this.AnnualIncome = "";
			this.CKitPos = 0;
			this.InterferonTimeLength = "";
			this.Notes = "";
			this.Unresectable = 0;
			this.Metastatic = 0;
            this.HighRisk = 2;
			this.Recommendation = "";
		}
		
		//**********************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			this.ApplicantID = (int)(ds.Tables[0].Rows[0]["ApplicantID"]);
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
			this.CountryID = (int)(ds.Tables[0].Rows[0]["CountryID"]);
			this.Phone = (ds.Tables[0].Rows[0]["Phone"]).ToString();
			this.Mobile = (ds.Tables[0].Rows[0]["Mobile"]).ToString();
			this.Fax = (ds.Tables[0].Rows[0]["Fax"]).ToString();
			this.Email = (ds.Tables[0].Rows[0]["Email"]).ToString();
			this.ContactFirstName = (ds.Tables[0].Rows[0]["ContactFirstName"]).ToString();
			this.ContactLastName = (ds.Tables[0].Rows[0]["ContactLastName"]).ToString();
			this.ContactStreet1 = (ds.Tables[0].Rows[0]["ContactStreet1"]).ToString();
			this.ContactStreet2 = (ds.Tables[0].Rows[0]["ContactStreet2"]).ToString();
			this.ContactCity = (ds.Tables[0].Rows[0]["ContactCity"]).ToString();
			this.ContactStateProvince = (ds.Tables[0].Rows[0]["ContactStateProvince"]).ToString();
			this.ContactPostalCode = (ds.Tables[0].Rows[0]["ContactPostalCode"]).ToString();
			this.ContactCountryID = (int)(ds.Tables[0].Rows[0]["ContactCountryID"]);
			this.ContactPhone = (ds.Tables[0].Rows[0]["ContactPhone"]).ToString();
			this.ContactMobile = (ds.Tables[0].Rows[0]["ContactMobile"]).ToString();
			this.ContactFax = (ds.Tables[0].Rows[0]["ContactFax"]).ToString();
			this.ContactEmail = (ds.Tables[0].Rows[0]["ContactEmail"]).ToString();
			this.ContactRelationship = (ds.Tables[0].Rows[0]["ContactRelationship"]).ToString();
			this.RelationshipDetails = (ds.Tables[0].Rows[0]["RelationshipDetails"]).ToString();
			try
			{
				this.PhysicianID = Convert.ToInt32(ds.Tables[0].Rows[0]["PhysicianID"]);
			}
			catch
			{
				this.PhysicianID = 0;
			}
			this.PhysicianFirstName = (ds.Tables[0].Rows[0]["PhysicianFirstName"]).ToString();
			this.PhysicianLastName = (ds.Tables[0].Rows[0]["PhysicianLastName"]).ToString();
			this.Specialty = (ds.Tables[0].Rows[0]["Specialty"]).ToString();
			this.Clinic = (ds.Tables[0].Rows[0]["Clinic"]).ToString();
			this.PhysicianPhone = (ds.Tables[0].Rows[0]["PhysicianPhone"]).ToString();
			this.PhysicianFax = (ds.Tables[0].Rows[0]["PhysicianFax"]).ToString();
			this.PhysicianEmail = (ds.Tables[0].Rows[0]["PhysicianEmail"]).ToString();
			this.AppliedForGIPAP = (bool)(ds.Tables[0].Rows[0]["AppliedForGIPAP"]);
			this.PatientConsent = Convert.ToBoolean(ds.Tables[0].Rows[0]["patientconsent"]);
			this.Dosage = (ds.Tables[0].Rows[0]["Dosage"]).ToString();
			this.Diagnosis = (ds.Tables[0].Rows[0]["Diagnosis"]).ToString();
			this.DiagnosisDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DiagnosisDate"]);
			this.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["createdate"]);
            this.FlagNOA = (bool)(ds.Tables[0].Rows[0]["noa"]);
			try
			{
				this.PhilPositive = (int)(ds.Tables[0].Rows[0]["PhilPositive"]);
			}
			catch
			{
				this.PhilPositive = 2;
			}
			try
			{
				this.BCR = Convert.ToInt32(ds.Tables[0].Rows[0]["BCR"]);
			}
			catch
			{
				this.BCR = 2;
			}
			try
			{
				this.RelapsedRefractory = (int)(ds.Tables[0].Rows[0]["relapsedrefractory"]);
			}
			catch{}
			try
			{
				this.Chemo = (int)(ds.Tables[0].Rows[0]["Chemo"]);
			}
			catch{}
			this.CMLPhase = (ds.Tables[0].Rows[0]["CMLPhase"]).ToString();
			try
			{
				this.Interferon = (bool)(ds.Tables[0].Rows[0]["Interferon"]);
				this.HematologicFailure = (bool)(ds.Tables[0].Rows[0]["HematologicFailure"]);
				this.CytogeneticFailure = (bool)(ds.Tables[0].Rows[0]["CytogeneticFailure"]);
			}
			catch
			{
				this.Interferon = false;
				this.HematologicFailure = false;
				this.CytogeneticFailure = false;
			}
			this.Intolerant = (bool)(ds.Tables[0].Rows[0]["Intolerant"]);
            this.Adjuvant = (bool)(ds.Tables[0].Rows[0]["adjuvant"]);
            this.HighRisk = (int)(ds.Tables[0].Rows[0]["HighRisk"]);
            this.DiagSummary = ds.Tables[0].Rows[0]["DiagSummary"].ToString();
			try
			{
				this.Insurance = (bool)(ds.Tables[0].Rows[0]["Insurance"]);
			}
			catch
			{
				this.Insurance = false;
			}
			this.Recurrent = Convert.ToBoolean(ds.Tables[0].Rows[0]["recurrent"]);
			try
			{
				this.CoversRx = (bool)(ds.Tables[0].Rows[0]["CoversRx"]);
			}
			catch
			{
				this.CoversRx = false;
			}
			try
			{
				this.CoversCancerRx = (bool)(ds.Tables[0].Rows[0]["CoversCancerRx"]);
			}
			catch
			{
				this.CoversCancerRx = false;
			}
			try
			{
				this.CoversGlivecRx = (bool)(ds.Tables[0].Rows[0]["CoversGlivecRx"]);
			}
			catch
			{
				this.CoversGlivecRx = false;
			}
			this.AnnualIncome = (ds.Tables[0].Rows[0]["AnnualIncome"]).ToString();
			this.Occupation = (ds.Tables[0].Rows[0]["Occupation"]).ToString();
			try
			{
				this.CKitPos = (int)(ds.Tables[0].Rows[0]["CKITPOSITIVE"]);
			}
			catch
			{
				this.CKitPos = 2;
			}
			this.InterferonTimeLength = (ds.Tables[0].Rows[0]["InterferonTimeLength"]).ToString();
			this.Notes = ds.Tables[0].Rows[0]["Notes"].ToString();
			try
			{
				this.Unresectable = (int)(ds.Tables[0].Rows[0]["Unresectable"]);
			}
			catch
			{
				this.Unresectable = 2;
			}
			try
			{
				this.Metastatic = (int)(ds.Tables[0].Rows[0]["Metastatic"]);
			}
			catch
			{
				this.Metastatic = 2;
			}
            this.Glivec = (bool)(ds.Tables[0].Rows[0]["Glivec"]);
            try
			{
				this.GlivecStartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["GlivecStartDate"]);
			}
			catch
			{}
            this.TabletStrength = ds.Tables[0].Rows[0]["TabletStrength"].ToString();
            //TASIGNA FIELDS
            this.Treatment = (ds.Tables[0].Rows[0]["treatment"]).ToString();
            this.Imatinib = (bool)(ds.Tables[0].Rows[0]["imatinib"]);
            this.GlivecIntolerant = (bool)(ds.Tables[0].Rows[0]["GLIVECINTOLERANT"]);
            this.GlivecResistant = (bool)(ds.Tables[0].Rows[0]["GLIVECRESISTANT"]);
            this.Dasatinib = (bool)(ds.Tables[0].Rows[0]["DASATINIB"]);
            this.DasatinibIntolerant = (bool)(ds.Tables[0].Rows[0]["DASATINIBINTOLERANT"]);
            this.DasatinibResistant = (bool)(ds.Tables[0].Rows[0]["DASATINIBRESISTANT"]);
            this.Tasigna = (bool)(ds.Tables[0].Rows[0]["TASIGNA"]);
            try
            {
                this.TasignaStartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["TASIGNASTARTDATE"]);
            }
            catch { }
            this.PrevTasignaDose = (ds.Tables[0].Rows[0]["PREVTASIGNADOSE"]).ToString();
            this.TasignaPatientConsent = Convert.ToBoolean(ds.Tables[0].Rows[0]["tasignapatientconsent"]);
            this.NOATasigna = (bool)(ds.Tables[0].Rows[0]["NOATASIGNA"]);
			/*Verification*/
			if(ds.Tables[0].Rows[0]["Recommendation"].ToString() != "")
			{
				this.MedicalChart = Convert.ToInt32(ds.Tables[0].Rows[0]["MedicalChart"]);
				this.PhiladelphiaVerification = Convert.ToInt32(ds.Tables[0].Rows[0]["PhiladelphiaVerification"]);
				this.CKitVerification = Convert.ToInt32(ds.Tables[0].Rows[0]["CKitVerification"]);
				this.CopyOfID = Convert.ToInt32(ds.Tables[0].Rows[0]["CopyOfID"]);
				this.Photo = Convert.ToInt32(ds.Tables[0].Rows[0]["Photo"]);
				this.SSCard = Convert.ToInt32(ds.Tables[0].Rows[0]["SSCard"]);
				this.InsuranceCard = Convert.ToInt32(ds.Tables[0].Rows[0]["InsuranceCard"]);
				this.InsuranceType = ds.Tables[0].Rows[0]["InsuranceType"].ToString();
				this.TaxReturn = Convert.ToInt32(ds.Tables[0].Rows[0]["TaxReturn"]);
				this.SalarySlip = Convert.ToInt32(ds.Tables[0].Rows[0]["SalarySlip"]);
				this.FinancialAffidavit = Convert.ToInt32(ds.Tables[0].Rows[0]["FinancialAffidavit"]);
				this.PhoneBill = Convert.ToInt32(ds.Tables[0].Rows[0]["PhoneBill"]);
				this.OtherDocs = ds.Tables[0].Rows[0]["OtherDocs"].ToString();
				this.HouseholdMembers = Convert.ToInt32(ds.Tables[0].Rows[0]["HouseholdMembers"]);
				this.HouseholdOccupation = ds.Tables[0].Rows[0]["HouseholdOccupation"].ToString();
				this.HouseholdIncome = ds.Tables[0].Rows[0]["HouseholdIncome"].ToString();
				this.AdditionalFunds = ds.Tables[0].Rows[0]["AdditionalFunds"].ToString();
				this.HouseholdAssets = ds.Tables[0].Rows[0]["HouseholdAssets"].ToString();
				this.Recommendation = ds.Tables[0].Rows[0]["Recommendation"].ToString();
				this.Explanation = ds.Tables[0].Rows[0]["Explanation"].ToString();
			}
			if(ds.Tables[8].Rows.Count > 0)
			{
				this.CountryName = ds.Tables[8].Rows[0]["countryname"].ToString();
				this.NoLinkCountryName = ds.Tables[8].Rows[0]["nolinkcountryname"].ToString();
				try
				{
					this.PatientConsentDate = Convert.ToDateTime(ds.Tables[8].Rows[0]["patientconsentdate"]);
				}
				catch
				{
					this.PatientConsentDate = DateTime.Today.AddDays(1);
				}
                this.GISTApproved = Convert.ToBoolean(ds.Tables[8].Rows[0]["gistapproved"]);
                this.PediatricAge = Convert.ToInt32(ds.Tables[8].Rows[0]["PediatricAge"]);
			}
            //8 tasigna phys from mou
            try
            {
                this.TasignaPhys = Convert.ToInt32(ds.Tables[9].Rows[0]["tasigna"]);
            }
            catch
            {
                this.TasignaPhys = 0;
            }

			this.ApplicantDS = ds;
		}
		
		//*********************************************************************************************************************
		public DataSet GetGipapApplicationList()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetGIPAPApplicantList");
		}


        //*********************************************************************************************************************
        public void RestoreApplicant(int countryid, int physid,string lastname,string firstname)
        {
            SqlParameter[] arParams = new SqlParameter[5];

            arParams[0] = new SqlParameter("@ApplicantID", SqlDbType.Int);
            arParams[0].Value = this.ApplicantID;

            arParams[1] = new SqlParameter("@PhysicianId", SqlDbType.Int);
            arParams[1].Value = physid;

            arParams[2] = new SqlParameter("@CountryId", SqlDbType.Int);
            arParams[2].Value = countryid;

            arParams[3] = new SqlParameter("@lastname", SqlDbType.VarChar,100);
            arParams[3].Value = lastname;

            arParams[4] = new SqlParameter("@FirstName", SqlDbType.VarChar,100);
            arParams[4].Value = firstname;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_RestorePatientApplicant", arParams);
        }

		//*********************************************************************************************************************
		public void TransferApplicant(string reason)
		{
			SqlParameter[] arParams = new SqlParameter[2];

			arParams[0] = new SqlParameter("@ApplicantID", SqlDbType.Int); 
			arParams[0].Value = this.ApplicantID;
				
			arParams[1] = new SqlParameter("@DeleteReason", SqlDbType.NVarChar, 50); 
			arParams[1].Value = reason;
			
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_TransferPatientApplicant", arParams);
		}
		//*********************************************************************************************************************
		public void RetrieveApplicant()
		{
			SqlParameter arParams = new SqlParameter("@ApplicantID", SqlDbType.Int); 
			arParams.Value = this.ApplicantID;
			
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_RetrievePatientApplicant", arParams);
		}
		//*********************************************************************************************************************
		public DataSet GetApplicantDataSets(int uid, string urole, int countID)
		{
			SqlParameter[] arParams = new SqlParameter[3];

			arParams[0] = new SqlParameter("@UserID", SqlDbType.Int); 
			arParams[0].Value = uid;
				
			arParams[1] = new SqlParameter("@Role", SqlDbType.NVarChar, 50); 
			arParams[1].Value = urole;

            arParams[2] = new SqlParameter("@CountryID", SqlDbType.Int);
            arParams[2].Value = countID;
			
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetApplicantDataSets", arParams);
        }
        //*********************************************************************************************************************
        public DataSet GetCountryPhysicianApplicantDatasets(int countID, int physID)
        {
            SqlParameter[] arParams = new SqlParameter[2];

            arParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
            arParams[0].Value = countID;

            arParams[1] = new SqlParameter("@PhysicianID", SqlDbType.Int);
            arParams[1].Value = physID;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryPhysicianApplicantDataSets", arParams);
        }
		//*********************************************************************************************************************
		public int CheckPhysician()
		{
			DataSet ds = new DataSet();
			SqlParameter[] arParams = new SqlParameter[2];

			arParams[0] = new SqlParameter("@PhysicianFirstName", SqlDbType.NVarChar, 50); 
			arParams[0].Value = this.PhysicianFirstName;
				
			arParams[1] = new SqlParameter("@PhysicianLastName", SqlDbType.NVarChar, 50); 
			arParams[1].Value = this.PhysicianLastName;

			try
			{
				// Call ExecuteNonQuery static method of SqlHelper class
				// We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
				ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CheckPhysician", arParams);
			}
			catch (Exception e)
			{
				// throw an exception
				throw e;
			}
			if (ds.Tables[0].Rows.Count == 1)
			{
				return (int)(ds.Tables[0].Rows[0].ItemArray[0]);
			}
			else
			{
				return 0;
			}
		}

		//*********************************************************************************************************************
		public int CheckClinic()
		{
			DataSet ds = new DataSet();
			SqlParameter arParams = new SqlParameter();

			arParams = new SqlParameter("@Clinic", SqlDbType.VarChar, 40); 
			arParams.Value = this.Clinic;
			

			try
			{
				// Call ExecuteNonQuery static method of SqlHelper class
				// We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
				ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CheckClinic", arParams);
			}
			catch (Exception e)
			{
				// throw an exception
				throw e;
			}
			if (ds.Tables[0].Rows.Count == 1)
			{
				return (int)(ds.Tables[0].Rows[0].ItemArray[0]);
			}
			else
			{
				return 0;
			}
		}
		//*********************************************************************************************************************
		public DataSet ApplicantDropDownLists()
		{
			string cString = "SERVER=CRAIGA1;DATABASE=GIPAP;PWD=secret;UID=sa;";
			return SqlHelper.ExecuteDataset(cString, CommandType.StoredProcedure, "spr_ApplicantDropDownLists");
		}
		//*********************************************************************************************************************
		public string NameHeaderText(string pType)
		{
			if(pType == "patient")
			{
				return "<font size=4 color=steelblue>" + this.FirstName + " " + this.LastName + "</font><br><b>Birth Date: " + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y");
			}
			else if(pType == "physician")
			{
				return "<font size=4 color=steelblue>" + this.PhysicianFirstName + " " + this.PhysicianLastName + "</font><br><b>Specialty: " + this.Specialty;
			}
			else
			{
				return "";
			}
		}

		//*********************************************************************************************************************
		public string ApplicantInfo()
		{
			string patInfo = "<font color=steelblue>" + this.FirstName + " " + this.LastName + "</font>";
            if(ThaiName != string.Empty)
                patInfo += "<br><font color=steelblue size=3>" + this.ThaiName + "</font>";
            return patInfo;
		}
		//**********************************************************************************************************************
		public string AddressInfo()
		{
            string address = "<font class='lbl'>Sex: </font>" + this.Sex + "<br>";
            address += "<font class='lbl'>Birth Date: </font>" + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y");
            address += "<br /><br /><font class='lbl'>Address: </font>" + this.Street1 + "<br>" + this.Street2 + "<br><font class='lbl'>City: </font>" + this.City + "<br><font class='lbl'>State / Province: </font>" + this.StateProvince + " " + this.PostalCode + "<br><font class='lbl'>Country: </font>" + this.CountryName + "<br><br>";
            address += "<font class='lbl'>Email: </font>" + this.Email + "<br><font class='lbl'>(tel)</font>" + this.Phone + "<br><font class='lbl'>(fax)</font>" + this.Fax + "<br><font class='lbl'>(mobile)</font>" + this.Mobile;
			return address;
		}
		//**********************************************************************************************************************
		public string POSuggestions()
		{
			string poS = "";
			if(this.ApplicantDS.Tables[2].Rows.Count > 0)
			{
                poS = "<font color=gray style='font-size:8pt;'>Suggestions:<i>";
				for(int i=0; i<this.ApplicantDS.Tables[2].Rows.Count; i++)
				{
					poS += "<br>" + this.ApplicantDS.Tables[2].Rows[i]["suggestionname"].ToString();
				}
				poS += "</i></font>";
			}
			return poS;
		}
        //**********************************************************************************************************************
        public string MSSuggestions()
        {
            string poS = "";
            if (this.ApplicantDS.Tables[7].Rows.Count > 0)
            {
                poS = "<font color=gray style='font-size:8pt;'>Suggestions:<i>";
                for (int i = 0; i < this.ApplicantDS.Tables[7].Rows.Count; i++)
                {
                    poS += "<br>" + this.ApplicantDS.Tables[7].Rows[i]["suggestionname"].ToString();
                }
                poS += "</i></font>";
            }
            return poS;
        }

        //**********************************************************************************************************************
        public string DistributorSuggestions()
        {
            string doS = "";
            if (this.ApplicantDS.Tables[11].Rows.Count > 0)
            {
                doS = "<font color=gray style='font-size:8pt;'>Suggestions:<i>";
                for (int i = 0; i < this.ApplicantDS.Tables[11].Rows.Count; i++)
                {
                    doS += "<br>" + this.ApplicantDS.Tables[11].Rows[i]["suggestionname"].ToString();
                }
                doS += "</i></font>";
            }
            return doS;
        }

		//**********************************************************************************************************************
		public string PhysicianInfo()
		{
			string physinfo = "<b>Physician Entered: </b>" + this.PhysicianFirstName + " " + this.PhysicianLastName;
			physinfo += "<br><b>Clinic: </b>" + this.Clinic;
			physinfo += "<br>(<a href=GIPAP.aspx?trgt=physicianinfo&choice=0&appid=" + this.ApplicantID.ToString() + "><font color=blue>Add</font></a>)";
			return physinfo;
		}
		//**********************************************************************************************************************
		/*public string DiagnosisInfoTable()
		{
            string diagInfo = "";
            
			diagInfo += "<b><font class='lbl'>Diagnosis: </font>";
			if(this.Diagnosis == "Ph+ ALL" || this.Diagnosis == "DFSP")
			{
				diagInfo += "<font color=red>";
			}
			else
			{
				diagInfo += "<font color=steelblue>";
			}
			diagInfo += this.Diagnosis + "</font></b><br>";
			diagInfo += "<font class='lbl'>Diagnosis Date: </font>" + this.DiagnosisDate.Day.ToString() + " " + this.DiagnosisDate.ToString("y") + "<br><br>";
			if(this.Diagnosis == "CML")
			{
				diagInfo += "<font class='lbl'>Philadelphia +: </font>";
				if(this.PhilPositive == 0)
				{
					diagInfo += "No";
				}
				else if(this.PhilPositive == 1)
				{
					diagInfo += "Yes";
				}
				else
				{
					diagInfo += "Don't Know";
				}
				diagInfo += "<br><font class='lbl'>BCR-Abl +: </font>";
				if(this.BCR == 0)
				{
					diagInfo += "No";
				}
				else if(this.BCR == 1)
				{
					diagInfo += "Yes";
				}
				else
				{
					diagInfo += "Don't Know";
				}
                if (this.Treatment == "Tasigna")
                {
                    if (this.Glivec || this.Imatinib || this.Dasatinib)//2nd line
                    {
                        if (this.CMLPhase == "Blast Crisis")
                        {
                            diagInfo += "<br><font color=red>CML Phase: " + this.CMLPhase + "</font>";
                        }
                        else
                        {
                            diagInfo += "<br><font color=steelblue>CML Phase: " + this.CMLPhase + "</font>";
                        }
                    }
                    else //1st line
                    {
                        if (this.CMLPhase == "Blast Crisis")
                        {
                            diagInfo += "<br><font color=red>CML Phase: " + this.CMLPhase + "</font>";
                        }
                        else
                        {
                            diagInfo += "<br><font color=steelblue>CML Phase: " + this.CMLPhase + "</font>";
                        }
                    }
                }
                else
                {
                    diagInfo += "<br><font color=steelblue>CML Phase: " + this.CMLPhase + "</font>";
                }
			}
			else if(this.Diagnosis == "DFSP")
			{
                diagInfo += "<font class='lbl'>Is the tumor unresectable, recurrent or metastatic?: </font>";
				if(this.Recurrent)
				{
					diagInfo += "Yes";
				}
				else
				{
					diagInfo += "<font color=red>No</font>";
				}
			}
            else if (this.Diagnosis == "MDS / MPD" || this.Diagnosis == "Systemic Mastocytosis" || this.Diagnosis == "HES / CEL")
            {
                diagInfo += "<font class='lbl'>Medical Summary:</font><br>" + this.DiagSummary;
            }

            else if (this.Diagnosis == "Ph+ ALL")
            {
                diagInfo += "<font class='lbl'>Philadelphia +: </font>";
                if (this.PhilPositive == 0)
                {
                    diagInfo += "No";
                }
                else if (this.PhilPositive == 1)
                {
                    diagInfo += "Yes";
                }
                else
                {
                    diagInfo += "Don't Know";
                }
                diagInfo += "<br><font class='lbl'>BCR-Abl +: </font>";
                if (this.BCR == 0)
                {
                    diagInfo += "No";
                }
                else if (this.BCR == 1)
                {
                    diagInfo += "Yes";
                }
                else
                {
                    diagInfo += "Don't Know";
                }
                if (this.RelapsedRefractory != -1)
                {
                    diagInfo += "<br><font class='lbl'>Relapsed / Refractory: </font>";
                    if (this.RelapsedRefractory == 1)
                    {
                        diagInfo += "Yes";
                    }
                    else
                    {
                        diagInfo += "<font color=red>No</font>";
                    }
                }
                if (this.Chemo != -1)
                {
                    diagInfo += "<br><font class='lbl'>Newly Diagnosed / Integrated with Chemotherapy: </font>";
                    if (this.Chemo == 1)
                    {
                        diagInfo += "Yes";
                    }
                    else
                    {
                        diagInfo += "<font color=red>No</font>";
                    }
                }
            }
            else if (this.Diagnosis == "GIST")
            {
                diagInfo += "<font class='lbl'>C-Kit +: </font>";
                if (this.CKitPos == 0)
                {
                    diagInfo += "No";
                }
                else if (this.CKitPos == 1)
                {
                    diagInfo += "Yes";
                }
                else
                {
                    diagInfo += "Don't Know";
                }
                diagInfo += "<br><font class='lbl'>Metastatic +: </font>";
                if (this.Metastatic == 0)
                {
                    diagInfo += "No";
                }
                else if (this.Metastatic == 1)
                {
                    diagInfo += "Yes";
                }
                else
                {
                    diagInfo += "Don't Know";
                }
                diagInfo += "<br><font class='lbl'>Unresectable +: </font>";
                if (this.Unresectable == 0)
                {
                    diagInfo += "No";
                }
                else if (this.Unresectable == 1)
                {
                    diagInfo += "Yes";
                }
                else
                {
                    diagInfo += "Don't Know";
                }
                //adjuvant
                if (this.Adjuvant)
                {
                    diagInfo += "<br><br><b><font color=gray>Adjuvant Treatment</font></b><br>";
                    diagInfo += "<font class='lbl'>Is the case high risk due to mitotic count and tumor size: </font>";
                    if (this.HighRisk == 0)
                    {
                        diagInfo += "<font color=red>No</font>";
                    }
                    else if (this.HighRisk == 1)
                    {
                        diagInfo += "Yes";
                    }
                    else
                    {
                        diagInfo += "<font color=red>Don't Know</font>";
                    }
                }
            }
            if (this.Treatment == "Glivec")
            {
                diagInfo += "<br><font class='lbl'>Applied for GIPAP before: </font>";
                if (this.AppliedForGIPAP)
                {
                    diagInfo += "Yes<br>";
                }
                else
                {
                    diagInfo += "No<br>";
                }
                if (this.PatientConsentDate <= this.CreateDate)
                {
                    diagInfo += "<font class='lbl'>Patient Consent Form: </font>";
                    if (this.PatientConsent)
                    {
                        diagInfo += "Yes<br><br>";
                    }
                    else
                    {
                        diagInfo += "<font color=red>No</font><br><br>";
                    }
                }
                diagInfo += "<font class='lbl'>Glivec®/imatinib: </font>";
                if (this.Glivec)
                {
                    diagInfo += "Yes";
                    diagInfo += "<br><font class='lbl'>Date Started Glivec: </font>" + this.GlivecStartDate.Day.ToString() + " " + this.GlivecStartDate.ToString("y");
                }
                else
                {
                    diagInfo += "No";
                }
                if (this.Diagnosis == "DFSP" && !this.Glivec && this.Dosage != "800mg")
                {
                    diagInfo += "<br><b><font color=steelblue>Requested Dosage: <font color=red>" + this.Dosage + "</font></font></b>";
                }
                else
                {
                    diagInfo += "<br><b><font color=steelblue>Requested Dosage: " + this.Dosage + "</font></b>";
                }
            }
            else if (this.Treatment == "Tasigna")
            {
                diagInfo += "<br><b><font color=purple>Tasigna</font>";
                if (this.Glivec || this.Imatinib || this.Dasatinib)//2nd line
                {
                    if (this.TasignaPhys > 0)
                    {
                        diagInfo += " <font color=gray>2nd Line</font>";
                    }
                    else
                    {
                        diagInfo += " <font color=red>2nd Line</font>";
                    }
                }
                else //1st line
                {
                    if (this.TasignaPhys == 1)
                    {
                        diagInfo += " <font color=gray>1st Line</font>";
                    }
                    else
                    {
                        diagInfo += " <font color=red>1st Line</font>";
                    }
                }
                diagInfo += "</b><br><font class='lbl'>Taken Glivec?: </font>" + this.BoolAnswer(this.Glivec, (this.TasignaPhys != 1 && !this.Imatinib));
                diagInfo += "<font class='lbl'>Taken generic Imatinib?: </font>" + this.BoolAnswer(this.Imatinib, (this.TasignaPhys != 1 && !this.Glivec));
                if (!this.GlivecIntolerant && !this.GlivecResistant && (this.Glivec || this.Imatinib))
                {
                    diagInfo += "<font class='lbl'>Glivec Intolerant?: </font>" + this.BoolAnswer(this.GlivecIntolerant, true);
                    diagInfo += "<font class='lbl'>Glivec Resistant?: </font>" + this.BoolAnswer(this.GlivecResistant, true);
                }
                else
                {
                    diagInfo += "<font class='lbl'>Glivec Intolerant?: </font>" + this.BoolAnswer(this.GlivecIntolerant, false);
                    diagInfo += "<font class='lbl'>Glivec Resistant?: </font>" + this.BoolAnswer(this.GlivecResistant, false);
                }
                diagInfo += "<br><font class='lbl'>Receiving Dasatinib?: </font>" + this.BoolAnswer(this.Dasatinib, false);
                if (this.Dasatinib)
                {
                    diagInfo += "<font class='lbl'>Dasatinib Intolerant?: </font>" + this.BoolAnswer(this.DasatinibIntolerant, false);
                    diagInfo += "<font class='lbl'>Dasatinib Resistant?: </font>" + this.BoolAnswer(this.DasatinibResistant, false);
                }
                diagInfo += "<br><br><font class='lbl'>Taken nilotinib/Tasigna?: </font>" + this.BoolAnswer(this.Tasigna, false);
                if (this.Tasigna)
                {
                    diagInfo += "<font class='lbl'>Start Date: </font>" + this.TasignaStartDate.Day.ToString() + " " + this.TasignaStartDate.ToString("y");
                    diagInfo += "<br><font class='lbl'>Previous daily dose: </font>" + this.PrevTasignaDose + "<br>";
                }
                diagInfo += "<br><font class='lbl'>Requested NOA Tasigna dose: </font>";
                if (this.Glivec || this.Imatinib || this.Dasatinib)//2nd line should be 400
                {
                    if (this.Dosage == "300mg BID")
                    {
                        diagInfo += "<font color=red>" + this.Dosage + "</font>";
                    }
                    else
                    {
                        diagInfo += this.Dosage;
                    }
                }
                else //1st line... should be 300
                {
                    if (this.Dosage == "300mg BID")
                    {
                        diagInfo += this.Dosage;
                    }
                    else
                    {
                        diagInfo += "<font color=red>" + this.Dosage + "</font>";
                    }
                }
                diagInfo += "<br><br><font class='lbl'>Prev. Applied for NOA Tasigna?: </font>" + this.BoolAnswer(this.NOATasigna, false);
                diagInfo += "<font class='lbl'>Patient Consent Form signed?: </font>" + this.BoolAnswer(this.TasignaPatientConsent, true);
            }
			return diagInfo;
		}*/
        //**********************************************************************************************************************
        public string DiagnosisInfoTable()
        {
            StringBuilder diagInfo = new StringBuilder();
            if (this.Treatment == "Glivec")
            {
                diagInfo.Append("<b><font color=steelblue>Glivec</font></b>");
                diagInfo.Append("<br><font class='lbl'>Applied for GIPAP before: </font>");
                if (this.AppliedForGIPAP)
                {
                    diagInfo.Append("Yes<br>");
                }
                else
                {
                    diagInfo.Append("No<br>");
                }
                if (!this.FlagNOA)
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
            diagInfo.Append("<b><font class='lbl'>Diagnosis: </font>");
            if (this.Diagnosis == "GIST" && !this.GISTApproved)
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
            diagInfo.Append("<font class='lbl'>Diagnosis Date: </font>" + this.DiagnosisDate.Day.ToString() + " " + this.DiagnosisDate.ToString("y") + "<br><br>");
            if (this.Diagnosis == "CML")
            {
                diagInfo.Append("<font class='lbl'>Philadelphia +: </font>");
                if (this.PhilPositive == 0)
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
                else if (this.PhilPositive == 1)
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
                if (this.Treatment == "Tasigna")
                {
                    if (this.Glivec || this.Imatinib || this.Dasatinib)//2nd line
                    {
                        if (this.CMLPhase == "Blast Crisis")
                        {
                            diagInfo.Append("<br><strong><font color=red>Current Phase: " + this.CMLPhase + "</font></strong>");
                        }
                        else
                        {
                            diagInfo.Append("<br><strong><font color=steelblue>Current Phase: " + this.CMLPhase + "</font></strong>");
                        }
                    }
                    else //1st line
                    {
                        if (this.CMLPhase == "Blast Crisis")
                        {
                            diagInfo.Append("<br><strong><font color=red>Current Phase: " + this.CMLPhase + "</font></strong>");
                        }
                        else
                        {
                            diagInfo.Append("<br><strong><font color=steelblue>Current Phase: " + this.CMLPhase + "</font></strong>");
                        }
                    }
                }
                else
                {
                    diagInfo.Append("<br><strong><font color=steelblue>Current Phase: " + this.CMLPhase + "</font></strong>");
                }
                /*if (this.NeedInterferonInfo)
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
                }*/
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
                if (this.PhilPositive == 0)
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
                else if (this.PhilPositive == 1)
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
            if (this.Treatment == "Tasigna")
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
                //diagInfo.Append("<br><b>Requested NOA Tasigna dose: </b>" + this.OriginalRequestedDosage);
                diagInfo.Append("<br><br><font class='lbl'>Prev. Applied for NOA Tasigna?: </font>" + this.BoolAnswer(this.NOATasigna, false));
                diagInfo.Append("<font class='lbl'>Patient Consent Form signed?: </font>" + this.BoolAnswer(this.TasignaPatientConsent, true));
                diagInfo.Append("</div>");
            }

            return diagInfo.ToString();
        }
        //**********************************************************************************************************************
		private string BoolAnswer(bool answer, bool red)
		{
			if(answer)
			{
				return "Yes<br>";
			}
			else
			{
				if(red)
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
		public string PrintApplication()
		{
			string appInfo = "Applicant Information<br>Applicant Name: " + this.FirstName + " " + this.LastName + "<br>";
			appInfo += "Gender: " + this.Sex + "<br>";
			appInfo += "Birthdate: " + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y") + "<br>";
			appInfo += "Street1: " + this.Street1 + "<br>";
			appInfo += "Street2: " + this.Street2 + "<br>";
			appInfo += "City: " + this.City + "<br>";
			appInfo += "State/Province: " + this.StateProvince + "<br>";
			appInfo += "Postal Code: " + this.PostalCode + "<br>";
			appInfo += "Country: " + this.NoLinkCountryName + "<br>";
			appInfo += "Phone: " + this.Phone + "<br>";
			appInfo += "Fax: " + this.Fax + "<br>";
			appInfo += "Email: " + this.Email + "<br><br>";
			if(this.ContactFirstName != "" && this.ContactLastName != "")
			{
				appInfo += "Contact Information<br>Contact Name: " + this.ContactFirstName + " " + this.ContactLastName + "<br>";
				appInfo += "Street1: " + this.ContactStreet1 + "<br>";
				appInfo += "Street2: " + this.ContactStreet2 + "<br>";
				appInfo += "City: " + this.ContactCity + "<br>";
				appInfo += "State/Province: " + this.ContactStateProvince + "<br>";
				appInfo += "Postal Code: " + this.ContactPostalCode + "<br>";
				appInfo += "Phone: " + this.ContactPhone + "<br>";
				appInfo += "Fax: " + this.ContactFax + "<br>";
				appInfo += "Email: " + this.ContactEmail + "<br><br>";
			}
			appInfo += "Physician Information<br>Physician Name: " + this.PhysicianFirstName + " " + this.PhysicianLastName + "<br>";
			appInfo += "Clinic: " + this.Clinic + "<br>";
			appInfo += "Specialty: " + this.Specialty + "<br>";
			appInfo += "Phone: " + this.PhysicianPhone + "<br>";
			appInfo += "Fax: " + this.PhysicianFax + "<br>";
			appInfo += "Email: " + this.PhysicianEmail + "<br><br>";

			appInfo += "History and Diagnosis Information<br>";
            appInfo += "Treatment: " + this.Treatment;
            appInfo += "<br>Diagnosis: " + this.Diagnosis + "";
            appInfo += "<br>Diagnosis Date: " + this.DiagnosisDate.Day.ToString() + " " + this.DiagnosisDate.ToString("y") + "<br>";
            if (this.Treatment == "Glivec")
            {
                appInfo += "<br>Applied for GIPAP: ";
                if (this.AppliedForGIPAP)
                {
                    appInfo += "Yes<br>";
                }
                else
                {
                    appInfo += "No<br>";
                }
                if (this.PatientConsentDate <= this.CreateDate)
                {
                    appInfo += "Patient Consent Form signed: ";
                    if (this.PatientConsent)
                    {
                        appInfo += "Yes<br>";
                    }
                    else
                    {
                        appInfo += "No<br>";
                    }
                }
                appInfo += "Prescribed Daily Dosage: " + this.Dosage + "<br>";
                appInfo += "Diagnosis Date: " + this.DiagnosisDate.Day.ToString() + " " + this.DiagnosisDate.ToString("y") + "<br>";
                appInfo += "Has patient previously taken Glivec®/imatinib?: ";
                if (this.Glivec)
                {
                    appInfo += "Yes<br>";
                    appInfo += "If yes, what was the starting date?: " + this.GlivecStartDate.Day.ToString() + " " + this.GlivecStartDate.ToString("y") + "<br><br>";
                }
                else
                {
                    appInfo += "No<br><br>";
                }
            }
            else if (this.Treatment == "Tasigna")
            {
                appInfo += "<br>Taken Glivec?: " + this.BoolAnswer(this.Glivec, false);
                appInfo += "<br>Taken Imatinib?: " + this.BoolAnswer(this.Imatinib, false);
                appInfo += "Glivec Intolerant?: " + this.BoolAnswer(this.GlivecIntolerant, false);
                appInfo += "Glivec Resistant?: " + this.BoolAnswer(this.GlivecResistant, false);
                appInfo += "<br>";
                appInfo += "<br>Taken Dasatinib?: " + this.BoolAnswer(this.Dasatinib, false);
                appInfo += "<br>Dasatinib Intolerant?: " + this.BoolAnswer(this.DasatinibIntolerant, false);
                appInfo += "Dasatinib Resistant?: " + this.BoolAnswer(this.DasatinibResistant, false);
                appInfo += "<br><br>Taken nilotinib/Tasigna?: " + this.BoolAnswer(this.Tasigna, false);
                if (this.Tasigna)
                {
                    appInfo += "Start Date: " + this.TasignaStartDate.Day.ToString() + " " + this.TasignaStartDate.ToString("y");
                    appInfo += "<br>Previous daily dose: " + this.PrevTasignaDose + "<br>";
                }
                appInfo += "<br>Requested NOA Tasigna dose: " + this.Dosage;
                appInfo += "<br><br>Prev. Applied for NOA Tasigna?: " + this.BoolAnswer(this.NOATasigna, false);
                appInfo += "Patient Consent Form signed?: " + this.BoolAnswer(this.TasignaPatientConsent, false);
            }
			
			if(this.Diagnosis == "CML")
			{
				appInfo += "CML and Interferon Information<br>Diagnosis: CML<br>";
				appInfo += "Philadelphia Chromosome Positive: ";
				if(this.PhilPositive == 0)
				{
					appInfo += "No<br>If no, is patient BCR-Abl positive?: ";
					if(this.BCR == 0)
					{
						appInfo += "No<br>";
					}
					else if(this.BCR == 1)
					{
						appInfo += "Yes<br>";
					}
					else
					{
						appInfo += "Don't Know<br>";
					}
				}
				else if(this.PhilPositive == 1)
				{
					appInfo += "Yes<br>";
				}
				else
				{
					appInfo += "Don't Know<br>";
				}
				appInfo += "CML Phase: " + this.CMLPhase + "<br>";
				if(this.InterferonTimeLength != "" && this.InterferonTimeLength != "0")
				{
					appInfo += "Recieved Interferon: ";
					if(this.Interferon)
					{
						appInfo += "Yes<br>";
						appInfo += "Interferon Treatment Period: " + this.InterferonTimeLength + "<br>";
						appInfo += "Intolerant To Interferon: ";
						if(this.Intolerant)
						{
							appInfo += "Yes<br>";
						}
						else
						{
							appInfo += "No<br>";
						}
						appInfo += "Hematologic Failure: ";
						if(this.HematologicFailure)
						{
							appInfo += "Yes<br>";
						}
						else
						{
							appInfo += "No<br>";
						}
						appInfo += "Cytogenetic Failure: ";
						if(this.CytogeneticFailure)
						{
							appInfo += "Yes<br>";
						}
						else
						{
							appInfo += "No<br>";
						}
					}
					else
					{
						appInfo += "No<br>";
					}
				}
			}
			else if(this.Diagnosis == "Ph+ ALL")
			{
				appInfo += "Ph+ ALL Information<br>Diagnosis: Ph+ ALL<br>";
				appInfo += "Philadelphia Chromosome Positive: ";
				if(this.PhilPositive == 0)
				{
					appInfo += "No<br>If no, is patient BCR-Abl positive?: ";
					if(this.BCR == 0)
					{
						appInfo += "No<br>";
					}
					else if(this.BCR == 1)
					{
						appInfo += "Yes<br>";
					}
					else
					{
						appInfo += "Don't Know<br>";
					}
				}
				else if(this.PhilPositive == 1)
				{
					appInfo += "Yes<br>";
				}
				else
				{
					appInfo += "Don't Know<br>";
				}
				if(this.RelapsedRefractory != -1)
				{
					appInfo += "<br><b>Relapsed / Refractory: </b>";
					if(this.RelapsedRefractory == 1)
					{
						appInfo += "Yes";
					}
					else
					{
						appInfo += "No";
					}
				}
				if(this.Chemo != -1)
				{
					appInfo += "<br><b>Newly Diagnosed / Integrated with Chemotherapy: </b>";
					if(this.Chemo == 1)
					{
						appInfo += "Yes";
					}
					else
					{
						appInfo += "No";
					}
				}
			}
			else if(this.Diagnosis == "GIST")
			{
				appInfo += "GIST and C-Kit Information<br>Diagnosis: GIST<br>";
				appInfo += "C-Kit Positive: ";
				if(this.CKitPos == 0)
				{
					appInfo += "No<br>";
				}
				else if(this.CKitPos == 1)
				{
					appInfo += "Yes<br>";
				}
				else
				{
					appInfo += "Don't Know<br>";
				}
				appInfo += "Tumor Unresectable: ";
				if(this.Unresectable == 0)
				{
					appInfo += "No<br>";
				}
				else if(this.Unresectable == 1)
				{
					appInfo += "Yes<br>";
				}
				else
				{
					appInfo += "Don't Know<br>";
				}
				appInfo += "Tumor Metastatic: ";
				if(this.Metastatic == 0)
				{
					appInfo += "No<br>";
				}
				else if(this.Metastatic == 1)
				{
					appInfo += "Yes<br>";
				}
				else
				{
					appInfo += "Don't Know<br>";
				}
			}
			//verification
			if(this.Recommendation != null)
			{
				appInfo += "<br>Medical Evaluation Documents Collected<br>Medical Chart: ";
				appInfo += this.FinancialInfoAnswer(this.MedicalChart);
				appInfo += "Philadelphia Chromosome Information: " + this.FinancialInfoAnswer(this.PhiladelphiaVerification);
				appInfo += "C-Kit Information: " + this.FinancialInfoAnswer(this.CKitVerification);
				appInfo += "<br><br>Financial Evaluation Documents Collected<br>";
				appInfo += "Copy of ID: " + this.FinancialInfoAnswer(this.CopyOfID);
				appInfo += "Photo: " + this.FinancialInfoAnswer(this.Photo);
				appInfo += "Social Security Card: " + this.FinancialInfoAnswer(this.SSCard);
				appInfo += "Insurance Card: " + this.FinancialInfoAnswer(this.InsuranceCard);
				appInfo += "Insurance Type: " + this.InsuranceType + "<br>";
				appInfo += "Tax Return: " + this.FinancialInfoAnswer(this.TaxReturn);
				appInfo += "Income Verification Document(s): " + this.FinancialInfoAnswer(this.SalarySlip);
				appInfo += "Financial Declaration Form: " + this.FinancialInfoAnswer(this.FinancialAffidavit);
				appInfo += "Phone Bill: " + this.FinancialInfoAnswer(this.PhoneBill);
				appInfo += "Other: " + this.OtherDocs + "<br>";
				appInfo += "Household Members: " + this.HouseholdAssets + "<br>";
				appInfo += "Household Occupation: " + this.HouseholdOccupation + "<br>";
				appInfo += "Household Income: " + this.HouseholdIncome + "<br>";
				appInfo += "Additional Funds: " + this.AdditionalFunds + "<br>";
				appInfo += "Household Assets: " + this.HouseholdAssets + "<br>";
				appInfo += "Insurance: ";
				if(this.Insurance)
				{
					appInfo += "Yes<br>";
					appInfo += "Covers Prescriptions: ";
					if(this.CoversRx)
					{
						appInfo += "Yes<br>";
					}
					else
					{
						appInfo += "No<br>";
					}
					appInfo += "Covers Cancer Prescriptions: ";
					if(this.CoversCancerRx)
					{
						appInfo += "Yes<br>";
					}
					else
					{
						appInfo += "No<br>";
					}
					appInfo += "Covers Glivec Prescriptions: ";
					if(this.CoversGlivecRx)
					{
						appInfo += "Yes<br>";
					}
					else
					{
						appInfo += "No<br>";
					}
				}
				else
				{
					appInfo += "No<br>";
				}
				appInfo += "Recommendation: " + this.Recommendation + "<br>";
				appInfo += "Explanation: " + this.Explanation + "<br>";
			}
			//insurance info
			else
			{
				appInfo += "<br>Insurance Information<br>";
				appInfo += "Insurance: ";
				if(this.Insurance)
				{
					appInfo += "Yes<br>";
					appInfo += "Covers Prescriptions: ";
					if(this.CoversRx)
					{
						appInfo += "Yes<br>";
					}
					else
					{
						appInfo += "No<br>";
					}
					appInfo += "Covers Cancer Prescriptions: ";
					if(this.CoversCancerRx)
					{
						appInfo += "Yes<br>";
					}
					else
					{
						appInfo += "No<br>";
					}
					appInfo += "Covers Glivec Prescriptions: ";
					if(this.CoversGlivecRx)
					{
						appInfo += "Yes<br>";
					}
					else
					{
						appInfo += "No<br>";
					}
				}
				else
				{
					appInfo += "No<br>";
				}
			}
			//financial info
			if(this.AnnualIncome != "0")
			{
				appInfo += "<br>Financial Information<br>Estimated Annual Income: $" + this.AnnualIncome + "<br>";
				appInfo += "Patient's or primary income earner's Occupation: " + this.Occupation + "<br>";
				appInfo += "Additional notes: " + this.Notes;
			}
			return appInfo;
		}
		//**********************************************************************************************************************
		private string FinancialInfoAnswer(int answer)
		{
			if(answer == 0)
			{
				return "No<br>";
			}
			else if(this.Metastatic == 1)
			{
				return "Yes<br>";
			}
			else
			{
				return "N/A<br>";
			}
		}

		//**********************************************************************************************************************
		/*public string FinancialInfoTable()
		{
			string finInfo = "<font class='lbl'>Annual Income: </font>" + this.AnnualIncome + "<br><br>";
            finInfo += "<font class='lbl'>Occupation: </font>" + this.Occupation + "<br><br>";
            finInfo += "<font class='lbl'>Health Insurance/Reimbursment?: </font>";
			if(this.Insurance)
			{
				finInfo += "Yes";
			}
			else
			{
				finInfo += "No";
			}
            finInfo += "<br><font class='lbl'>Covers Rx: </font>";
			if(this.CoversRx)
			{
				finInfo += "Yes";
			}
			else
			{
				finInfo += "No";
			}
            finInfo += "<br><font class='lbl'>Covers Cancer Rx: </font>";
			if(this.CoversCancerRx)
			{
				finInfo += "Yes";
			}
			else
			{
				finInfo += "No";
			}
            finInfo += "<br><font class='lbl'>Covers Glivec Rx: </font>";
			if(this.CoversGlivecRx)
			{
				finInfo += "Yes";
			}
			else
			{
				finInfo += "No";
			}
            finInfo += "<br /><font class='lbl'>Notes:</font><br>" + this.Notes;
			return finInfo;
		}*/
        //**********************************************************************************************************************
        public string FinancialInfoTable()
        {
            StringBuilder finInfo = new StringBuilder();
            
            
                finInfo.Append("<strong><font color=steelblue>Requested Dosage: </font>");
                if (this.Diagnosis == "GIST")
                {
                    if (this.Dosage == "400mg")
                    {
                        finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                    }
                    else if ((this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today) && (this.Dosage == "200mg" || this.Dosage == "260mg" || this.Dosage == "300mg") && !this.Adjuvant)
                    {
                        finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                    }
                    else
                    {
                        finInfo.Append("<font color=red>" + this.Dosage + "</font></strong>");
                    }
                }
                else if (this.Diagnosis == "Ph+ ALL")
                {
                    if ((this.Dosage == "260mg" || this.Dosage == "300mg" || this.Dosage == "400mg") && this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today)
                    {
                        finInfo.Append("<font color=red>" + this.Dosage + "</font></strong>");
                    }
                    else
                    {
                        finInfo.Append("<font color=red>" + this.Dosage + "</font></strong>");
                    }
                }
                else if (this.Diagnosis == "Ph+ ALL" || this.Diagnosis == "Adjuvant GIST" || this.Diagnosis == "MDS / MPD" || this.Diagnosis == "Systemic Mastocytosis" || this.Diagnosis == "HES / CEL")
                {
                    finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                }
                else if (this.Diagnosis == "DFSP")
                {
                    if (!this.Glivec && this.Dosage != "800mg")
                    {
                        finInfo.Append("<font color=red>" + this.Dosage + "</font></strong>");
                    }
                    else
                    {
                        finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                    }
                }
                else if (this.Diagnosis == "CML")
                {
                    if (this.CMLPhase == "Blast Crisis" && this.Dosage == "600mg")
                    {
                        finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                    }
                    else if (this.CMLPhase == "Accelerated" && this.Dosage == "600mg")
                    {
                        finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                    }
                    else if (this.CMLPhase == "Chronic" && this.Dosage == "400mg")
                    {
                        finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                    }
                    else if (this.CMLPhase == "Remission" && this.Dosage == "400mg")
                    {
                        finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                    }
                    else if ((this.BirthDate.AddYears(this.PediatricAge) > DateTime.Today) && (this.Dosage == "200mg" || this.Dosage == "260mg" || this.Dosage == "300mg"))
                    {
                        finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                    }
                    else if (this.Treatment == "Tasigna")
                    {
                        if (this.Glivec || this.Imatinib || this.Dasatinib)//2nd line should be 400
                        {
                            if (this.Dosage == "300mg BID")
                            {
                                finInfo.Append("<font color=red>" + this.Dosage + "</font></strong>");
                            }
                            else
                            {
                                finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                            }
                        }
                        else //1st line... should be 300
                        {
                            if (this.Dosage == "300mg BID")
                            {
                                finInfo.Append("<font color=steelblue>" + this.Dosage + "</font></strong>");
                            }
                            else
                            {
                                finInfo.Append("<font color=red>" + this.Dosage + "</font></strong>");
                            }
                        }
                    }
                    else
                    {
                        finInfo.Append("<font color=red>" + this.Dosage + "</font></strong>");
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
            finInfo.Append("<br><br><font class='lbl'>Annual Income: </font>" + this.AnnualIncome + "<br>");
            finInfo.Append("<font class='lbl'>Occupation: </font>" + this.Occupation);
            finInfo.Append("<br /><br /><font class='lbl'>Health Insurance/Reimbursment?: </font>");
            if (this.Insurance)
            {
                finInfo.Append("Yes");
            }
            else
            {
                finInfo.Append("No");
            }
            finInfo.Append("<br><font class='lbl'>Covers Rx: </font>");
            if (this.CoversRx)
            {
                finInfo.Append("Yes");
            }
            else
            {
                finInfo.Append("No");
            }
            finInfo.Append("<br><font class='lbl'>Covers Cancer Rx: </font>");
            if (this.CoversCancerRx)
            {
                finInfo.Append("Yes");
            }
            else
            {
                finInfo.Append("No");
            }
            finInfo.Append("<br><font class='lbl'>Covers Glivec Rx: </font>");
            if (this.CoversGlivecRx)
            {
                finInfo.Append("Yes");
            }
            else
            {
                finInfo.Append("No");
            }
            finInfo.Append("<br /><font class='lbl'>Notes:</font><br>" + this.Notes);

            return finInfo.ToString();
        }
		//*********************************************************************************************************************
		public int ApplicantID
		{
			get{return mApplicantID;}
			set{mApplicantID=value;}
		}
		//*********************************************************************************************************************
		public string FirstName
		{
			get{return mFirstName;}
			set{mFirstName=value;}
		}

		//*********************************************************************************************************************
		public string LastName
		{
			get{return mLastName;}
			set{mLastName=value;}
		}

        //*********************************************************************************************************************
        public string ThaiName
        {
            get { return mThaiName; }
            set { mThaiName = value; }
        }

        //*********************************************************************************************************************
        public bool FlagNOA
        {
            get { return mFlagNOA; }
            set { mFlagNOA = value; }
        }
		//*********************************************************************************************************************
		public string Sex
		{
			get{return mSex;}
			set{mSex=value;}
		}
		//*********************************************************************************************************************
		public DateTime BirthDate
		{
			get{return mBirthDate;}
			set{mBirthDate=value;}
		}

		//*********************************************************************************************************************
		public string Street1
		{
			get{return mStreet1;}
			set{mStreet1=value;}
		}

		//*********************************************************************************************************************
		public string Street2
		{
			get{return mStreet2;}
			set{mStreet2=value;}
		}

		//*********************************************************************************************************************
		public string City
		{
			get{return mCity;}
			set{mCity=value;}
		}

		//*********************************************************************************************************************
		public string StateProvince
		{
			get{return mStateProvince;}
			set{mStateProvince=value;}
		}

		//*********************************************************************************************************************
		public string PostalCode
		{
			get{return mPostalCode;}
			set{mPostalCode=value;}
		}

		//*********************************************************************************************************************
		public int CountryID
		{
			get{return mCountryID;}
			set{mCountryID=value;}
		}

		//*********************************************************************************************************************
		public string Phone
		{
			get{return mPhone;}
			set{mPhone=value;}
		}
		//*********************************************************************************************************************
		public string Mobile
		{
			get{return mMobile;}
			set{mMobile=value;}
		}

		//*********************************************************************************************************************
		public string Fax
		{
			get{return mFax;}
			set{mFax=value;}
		}

		//*********************************************************************************************************************
		public string Email
		{
			get{return mEmail;}
			set{mEmail=value;}
		}

		//*********************************************************************************************************************
		public string ContactFirstName
		{
			get{return mContactFirstName;}
			set{mContactFirstName=value;}
		}

		//*********************************************************************************************************************
		public string ContactLastName
		{
			get{return mContactLastName;}
			set{mContactLastName=value;}
		}

		//*********************************************************************************************************************
		public string ContactStreet1
		{
			get{return mContactStreet1;}
			set{mContactStreet1=value;}
		}

		//*********************************************************************************************************************
		public string ContactStreet2
		{
			get{return mContactStreet2;}
			set{mContactStreet2=value;}
		}

		//*********************************************************************************************************************
		public string ContactCity
		{
			get{return mContactCity;}
			set{mContactCity=value;}
		}

		//*********************************************************************************************************************
		public string ContactStateProvince
		{
			get{return mContactStateProvince;}
			set{mContactStateProvince=value;}
		}

		//*********************************************************************************************************************
		public string ContactPostalCode
		{
			get{return mContactPostalCode;}
			set{mContactPostalCode=value;}
		}

		//*********************************************************************************************************************
		public int ContactCountryID
		{
			get{return mContactCountryID;}
			set{mContactCountryID=value;}
		}

		//*********************************************************************************************************************
		public string ContactPhone
		{
			get{return mContactPhone;}
			set{mContactPhone=value;}
		}
		//*********************************************************************************************************************
		public string ContactMobile
		{
			get{return mContactMobile;}
			set{mContactMobile=value;}
		}
		//*********************************************************************************************************************
		public string ContactFax
		{
			get{return mContactFax;}
			set{mContactFax=value;}
		}

		//*********************************************************************************************************************
		public string ContactEmail
		{
			get{return mContactEmail;}
			set{mContactEmail=value;}
		}

		//*********************************************************************************************************************
		public string ContactRelationship
		{
			get{return mContactRelationship;}
			set{mContactRelationship=value;}
		}

		//*********************************************************************************************************************
		public string RelationshipDetails
		{
			get{return mRelationshipDetails;}
			set{mRelationshipDetails=value;}
		}
		//*********************************************************************************************************************
		public int PhysicianID
		{
			get{return mPhysicianID;}
			set{mPhysicianID=value;}
		}

		//*********************************************************************************************************************
		public string PhysicianFirstName
		{
			get{return mPhysicianFirstName;}
			set{mPhysicianFirstName=value;}
		}

		//*********************************************************************************************************************
		public string PhysicianLastName
		{
			get{return mPhysicianLastName;}
			set{mPhysicianLastName=value;}
		}

		//*********************************************************************************************************************
		public string Specialty
		{
			get{return mSpecialty;}
			set{mSpecialty=value;}
		}

		//*********************************************************************************************************************
		public string Clinic
		{
			get{return mClinic;}
			set{mClinic=value;}
		}

		//*********************************************************************************************************************
		public string PhysicianPhone
		{
			get{return mPhysicianPhone;}
			set{mPhysicianPhone=value;}
		}

		//*********************************************************************************************************************
		public string PhysicianFax
		{
			get{return mPhysicianFax;}
			set{mPhysicianFax=value;}
		}

		//*********************************************************************************************************************
		public string PhysicianEmail
		{
			get{return mPhysicianEmail;}
			set{mPhysicianEmail=value;}
		}

		//*********************************************************************************************************************
		public bool AppliedForGIPAP
		{
			get{return mAppliedForGIPAP;}
			set{mAppliedForGIPAP=value;}
		}

		//**************************************************************************************************************
		public bool PatientConsent
		{
			get{return mPatientConsent;}
			set{mPatientConsent = value;}
		}
		//*********************************************************************************************************************
		public string Dosage
		{
			get{return mDosage;}
			set{mDosage=value;}
		}

		//*********************************************************************************************************************
		public string Diagnosis
		{
			get{return mDiagnosis;}
			set{mDiagnosis=value;}
		}

		//*********************************************************************************************************************
		public DateTime DiagnosisDate
		{
			get{return mDiagnosisDate;}
			set{mDiagnosisDate=value;}
		}

		//*********************************************************************************************************************
		public int PhilPositive
		{
			get{return mPhilPositive;}
			set{mPhilPositive=value;}
		}

		//*********************************************************************************************************************
		public string CMLPhase
		{
			get{return mCMLPhase;}
			set{mCMLPhase=value;}
		}

		//*********************************************************************************************************************
		public bool Interferon
		{
			get{return mInterferon;}
			set{mInterferon=value;}
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

		//*********************************************************************************************************************
		public int CKitPos
		{
			get{return mCKitPos;}
			set{mCKitPos=value;}
		}

		//*********************************************************************************************************************
		public int BCR
		{
			get{return mBCR;}
			set{mBCR=value;}
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
		//*********************************************************************************************************************
		public bool Glivec
		{
			get{return mGlivec;}
			set{mGlivec=value;}
		}
		//*********************************************************************************************************************
		public DateTime GlivecStartDate
		{
			get{return mGlivecStartDate;}
			set{mGlivecStartDate=value;}
		}
		//*********************************************************************************************************************
		public bool Insurance
		{
			get{return mInsurance;}
			set{mInsurance=value;}
		}

		//*********************************************************************************************************************
		public bool CoversRx
		{
			get{return mCoversRx;}
			set{mCoversRx=value;}
		}

		//*********************************************************************************************************************
		public bool CoversCancerRx
		{
			get{return mCoversCancerRx;}
			set{mCoversCancerRx=value;}
		}

		//*********************************************************************************************************************
		public bool CoversGlivecRx
		{
			get{return mCoversGlivecRx;}
			set{mCoversGlivecRx=value;}
		}

		//*********************************************************************************************************************
		public string AnnualIncome
		{
			get{return mAnnualIncome;}
			set{mAnnualIncome=value;}
		}

		//*********************************************************************************************************************
		public string Occupation
		{
			get{return mOccupation;}
			set{mOccupation=value;}
		}
		//*********************************************************************************************************************
		public string Notes
		{
			get{return mNotes;}
			set{mNotes=value;}
		}
		//verification
		//**************************************************************************************************************
		public int MedicalChart
		{
			get{return mMedicalChart;}
			set{mMedicalChart = value;}
		}
		//**************************************************************************************************************
		public int PhiladelphiaVerification
		{
			get{return mPhiladelphiaVerification;}
			set{mPhiladelphiaVerification = value;}
		}
		//**************************************************************************************************************
		public int CKitVerification
		{
			get{return mCKitVerification;}
			set{mCKitVerification = value;}
		}
		//**************************************************************************************************************
		public int CopyOfID
		{
			get{return mCopyOfID;}
			set{mCopyOfID = value;}
		}
		//**************************************************************************************************************
		public int Photo
		{
			get{return mPhoto;}
			set{mPhoto = value;}
		}
		//**************************************************************************************************************
		public int SSCard
		{
			get{return mSSCard;}
			set{mSSCard = value;}
		}
		//**************************************************************************************************************
		public int InsuranceCard
		{
			get{return mInsuranceCard;}
			set{mInsuranceCard = value;}
		}
		//**************************************************************************************************************
		public string InsuranceType
		{
			get{return mInsuranceType;}
			set{mInsuranceType = value;}
		}
		//**************************************************************************************************************
		public int TaxReturn
		{
			get{return mTaxReturn;}
			set{mTaxReturn = value;}
		}
		//**************************************************************************************************************
		public int SalarySlip
		{
			get{return mSalarySlip;}
			set{mSalarySlip = value;}
		}
		//**************************************************************************************************************
		public int FinancialAffidavit
		{
			get{return mFinancialAffidavit;}
			set{mFinancialAffidavit = value;}
		}
		//**************************************************************************************************************
		public int PhoneBill
		{
			get{return mPhoneBill;}
			set{mPhoneBill = value;}
		}
		//**************************************************************************************************************
		public string  OtherDocs
		{
			get{return mOtherDocs;}
			set{mOtherDocs = value;}
		}
		//**************************************************************************************************************
		public int HouseholdMembers
		{
			get{return mHouseholdMembers;}
			set{mHouseholdMembers = value;}
		}
		//**************************************************************************************************************
		public string HouseholdOccupation
		{
			get{return mHouseholdOccupation;}
			set{mHouseholdOccupation = value;}
		}
		//**************************************************************************************************************
		public string HouseholdIncome
		{
			get{return mHouseholdIncome;}
			set{mHouseholdIncome = value;}
		}
		//**************************************************************************************************************
		public string AdditionalFunds
		{
			get{return mAdditionalFunds;}
			set{mAdditionalFunds = value;}
		}
		//**************************************************************************************************************
		public string HouseholdAssets
		{
			get{return mHouseholdAssets;}
			set{mHouseholdAssets = value;}
		}
		//**************************************************************************************************************
		public string Recommendation
		{
			get{return mRecommendation;}
			set{mRecommendation = value;}
		}
		//**************************************************************************************************************
		public string Explanation
		{
			get{return mExplanation;}
			set{mExplanation = value;}
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
        public string TabletStrength
        {
            get { return mTabletStrength; }
            set { mTabletStrength = value; }
        }
	}
}
