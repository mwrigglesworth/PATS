using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Verification.
	/// </summary>
	public class Verification
	{
		private int mVerificationID;
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
		private bool mInsurance;
		private bool mCoversRx;
		private bool mCoversCancerRx;
		private bool mCoversGlivecRx;
		private int mHouseholdMembers;
		private string mHouseholdOccupation;
		private string mHouseholdIncome;
		private string mAdditionalFunds;
		private string mHouseholdAssets;
		private string mRecommendation;
		private string mExplanation;
		private string mReasonChanged;
		//Patient & country & physician
		private int mPatientID;
		private string mPIN;
		private string mPatientName;
		private string mCity;
		private string mCurrentDosage;
		private string mOriginalRequestedDosage;
		private string mOriginalApprovedDosage;
		private DateTime mFinancialDeclarationDate;
        private DateTime mIntakeDate;
		//extra
		public bool VerificationComplete;
		public DataTable dtVNotes;
		private DateTime CreateDate;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];

		//**************************************************************************************************************
		public Verification()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		//**************************************************************************************************************
		public Verification(int patID)
		{
			if(patID == 0)
			{
				return;
			}
			else
			{
				DataSet myData;
				SqlParameter CurrID = new SqlParameter("@PatientID", SqlDbType.Int);
				CurrID.Value = patID;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientVerification", CurrID);
				if(myData.Tables[1].Rows.Count <= 0)
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
			SqlParameter[] arrParams = new SqlParameter[28];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@MedicalChart", SqlDbType.Int);
			arrParams[1].Value = this.MedicalChart;

			arrParams[2] = new SqlParameter("@PhiladelphiaVerification", SqlDbType.Int);
			arrParams[2].Value = this.PhiladelphiaVerification;

			arrParams[3] = new SqlParameter("@CKitVerification", SqlDbType.Int);
			arrParams[3].Value = this.CKitVerification;

			arrParams[4] = new SqlParameter("@CopyOfID", SqlDbType.Int);
			arrParams[4].Value = this.CopyOfID;

			arrParams[5] = new SqlParameter("@Photo", SqlDbType.Int);
			arrParams[5].Value = this.Photo;

			arrParams[6] = new SqlParameter("@SSCard", SqlDbType.Int);
			arrParams[6].Value = this.SSCard;

			arrParams[7] = new SqlParameter("@InsuranceCard", SqlDbType.Int);
			arrParams[7].Value = this.InsuranceCard;

			arrParams[8] = new SqlParameter("@TaxReturn", SqlDbType.Int);
			arrParams[8].Value = this.TaxReturn;

			arrParams[9] = new SqlParameter("@SalarySlip", SqlDbType.Int);
			arrParams[9].Value = this.SalarySlip;

			arrParams[10] = new SqlParameter("@PhoneBill", SqlDbType.Int);
			arrParams[10].Value = this.PhoneBill;

			arrParams[11] = new SqlParameter("@OtherDocs", SqlDbType.VarChar, 500);
			arrParams[11].Value = this.OtherDocs;

			arrParams[12] = new SqlParameter("@Insurance", SqlDbType.Bit);
			if(this.Insurance)
			{arrParams[12].Value = 1;}
			else
			{arrParams[12].Value = 0;}

			arrParams[13] = new SqlParameter("@CoversRx", SqlDbType.Bit);
			if(this.CoversRx)
			{arrParams[13].Value = 1;}
			else
			{arrParams[13].Value = 0;}

			arrParams[14] = new SqlParameter("@CoversCancerRx", SqlDbType.Bit);
			if(this.CoversCancerRx)
			{arrParams[14].Value = 1;}
			else
			{arrParams[14].Value = 0;}

			arrParams[15] = new SqlParameter("@CoversGlivecRx", SqlDbType.Bit);
			if(this.CoversGlivecRx)
			{arrParams[15].Value = 1;}
			else
			{arrParams[15].Value = 0;}

			arrParams[16] = new SqlParameter("@HouseholdMembers", SqlDbType.Int);
			arrParams[16].Value = this.HouseholdMembers;

			arrParams[17] = new SqlParameter("@HouseholdOccupation", SqlDbType.VarChar, 500);
			arrParams[17].Value = this.HouseholdOccupation;

			arrParams[18] = new SqlParameter("@HouseholdIncome", SqlDbType.NVarChar, 50);
			arrParams[18].Value = this.HouseholdIncome;

			arrParams[19] = new SqlParameter("@AdditionalFunds", SqlDbType.NVarChar, 50);
			arrParams[19].Value = this.AdditionalFunds;

			arrParams[20] = new SqlParameter("@HouseholdAssets", SqlDbType.NVarChar, 50);
			arrParams[20].Value = this.HouseholdAssets;

			arrParams[21] = new SqlParameter("@Recommendation", SqlDbType.VarChar, 50);
			arrParams[21].Value = this.Recommendation;

			arrParams[22] = new SqlParameter("@Explanation", SqlDbType.Text);
			arrParams[22].Value = this.Explanation;

			arrParams[23] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[23].Value = createdby;

			arrParams[24] = new SqlParameter("@InsuranceType", SqlDbType.VarChar, 50);
			arrParams[24].Value = this.InsuranceType;

			arrParams[25] = new SqlParameter("@VerificationID", SqlDbType.Int);
			arrParams[25].Value = this.VerificationID;

			arrParams[26] = new SqlParameter("@ReasonChanged", SqlDbType.VarChar, 500);
			arrParams[26].Value = this.ReasonChanged;

			arrParams[27] = new SqlParameter("@FinancialAffidavit", SqlDbType.Int);
            arrParams[27].Value = this.FinancialAffidavit;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateVerification", arrParams);

		}
		//**********************************************************************************************************************
		public string Summary()
		{
			string VerSum = "<strong>Verified Insurance: </strong>";
			VerSum += "<br><br><font color=steelblue><strong>Medical Evaluation Documents Collected:</strong></font>";
			VerSum += "<br><strong>Summary of Medical Chart: </strong>" + this.Answer(this.MedicalChart);
			VerSum += "<br><strong>Verification of Philadelphia Chromosome/BCR-Abl Test Results: </strong>" + this.Answer(this.PhiladelphiaVerification);
			VerSum += "<br><strong>Verification of C-Kit Test Results: </strong>" + this.Answer(this.CKitVerification);
			VerSum += "<br><br><font color=steelblue><strong>Financial Evaluation Documents Collected:</strong></font>";
			VerSum += "<br><strong>Copy of Patient's ID: </strong>" + this.Answer(this.CopyOfID);
			VerSum += "<br><strong>Photo: </strong>" + this.Answer(this.Photo);
			VerSum += "<br><strong>Social Security Card: </strong>" + this.Answer(this.SSCard);
			VerSum += "<br><strong>Private Insurance Card: </strong>" + this.Answer(this.InsuranceCard);
			VerSum += "<br><strong>Private Insurance: </strong>" + this.InsuranceType;
			VerSum += "<br><strong>Tax Return: </strong>" + this.Answer(this.TaxReturn);
			VerSum += "<br><strong>Income Verification Document(s): </strong>" + this.Answer(this.SalarySlip);
			if(this.FinancialAffidavit == 1)
			{
				VerSum += "<br><strong>Financial Declaration Form: </strong>" + this.Answer(this.FinancialAffidavit);
			}
			else
			{
				if(this.mFinancialDeclarationDate < this.CreateDate)
				{
					VerSum += "<br><strong><font color=red>Financial Declaration Form: </strong>" + this.Answer(this.FinancialAffidavit) + "</font>";
				}
				else
				{
					VerSum += "<br><strong>Financial Declaration Form: </strong>" + this.Answer(this.FinancialAffidavit);
				}
			}
			VerSum += "<br><strong>Utility / Phone Bill: </strong>" + this.Answer(this.PhoneBill);
			VerSum += "<br><strong>Other: </strong>" + this.OtherDocs;
			VerSum += "<br><br><font color=steelblue><strong>Summary</strong></font>";
			VerSum += "<br><strong>Health Insurance: </strong>";
			if(this.Insurance)
			{
				VerSum += "<font color=red>Yes</font>";
			}
			else
			{
				VerSum += "No";
			}
			VerSum += "<br><strong>Covers Rx: </strong>";
			if(this.CoversRx)
			{
				VerSum += "<font color=red>Yes</font>";
			}
			else
			{
				VerSum += "No";
			}
			VerSum += "<br><strong>Covers Cancer Rx: </strong>";
			if(this.CoversCancerRx)
			{
				VerSum += "<font color=red>Yes</font>";
			}
			else
			{
				VerSum += "No";
			}
			VerSum += "<br><strong>Covers Glivec Rx: </strong>";
			if(this.CoversGlivecRx)
			{
				VerSum += "<font color=red>Yes</font>";
			}
			else
			{
				VerSum += "No";
			}
			VerSum += "<br><strong>Number of Household Members: </strong>" + this.HouseholdMembers;
			VerSum += "<br><strong>Occupation of Financially contributing members of the Household: </strong>" + this.HouseholdOccupation;
			VerSum += "<br><strong>Household Annual Income: </strong>" + this.HouseholdIncome;
			VerSum += "<br><strong>Total of any additional Funds received by the Household: </strong>" + this.AdditionalFunds;
			VerSum += "<br><strong>Approximate value of assets of the Household: </strong>" + this.HouseholdAssets;
            VerSum += "<br><br><strong>Recommendation: </strong>";
			if(this.Recommendation != "Approve")
			{
				VerSum += "<font color=red>" + this.Recommendation + "</font>";
			}
			else
			{
				VerSum += this.Recommendation;
			}
			VerSum += "<br><strong>Case Summary: </strong>" + this.Explanation;
			return VerSum;
		}
		//**************************************************************************************************************
		private string Answer(int i)
		{
			if(i == 0)
			{
				return "No";
			}
			else if(i == 1)
			{
				return "Yes";
			}
			else
			{
				return "N/A";
			}
		}		
		//**************************************************************************************************************
		public void AddVerificationNote(string createdby, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@VerificationID", SqlDbType.Int);
			arrParams[0].Value = this.VerificationID;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = createdby;

			arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
			arrParams[2].Value = note;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateVerificationNote", arrParams);
		}
		//**************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			if(ds.Tables[0].Rows.Count > 0)
			{
				this.VerificationID = Convert.ToInt32(ds.Tables[0].Rows[0]["VerificationID"]);
				this.Insurance = Convert.ToBoolean(ds.Tables[0].Rows[0]["Insurance"]);
				this.CoversRx = Convert.ToBoolean(ds.Tables[0].Rows[0]["CoversRx"]);
				this.CoversCancerRx = Convert.ToBoolean(ds.Tables[0].Rows[0]["CoversCancerRx"]);
				this.CoversGlivecRx = Convert.ToBoolean(ds.Tables[0].Rows[0]["CoversGlivecRx"]);
				this.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["createdate"]);
				if(ds.Tables[0].Rows[0]["Recommendation"].ToString() != "")
				{
					this.VerificationComplete = true;
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
					this.FinancialAffidavit = Convert.ToInt32(ds.Tables[0].Rows[0]["financialaffidavit"]);
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
				else
				{
					this.VerificationComplete = false;
				}
			}
			else
			{
				this.VerificationID = 0;
			}
			//patient & country
			this.PatientID = Convert.ToInt32(ds.Tables[1].Rows[0]["PatientID"]);
			this.PIN = ds.Tables[1].Rows[0]["PIN"].ToString();
			this.PatientName = ds.Tables[1].Rows[0]["PatientName"].ToString();
			this.City = ds.Tables[1].Rows[0]["city"].ToString();
			try
			{
				this.mFinancialDeclarationDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["financialdeclarationdate"].ToString());
			}
			catch
			{
				this.mFinancialDeclarationDate = DateTime.Today.AddDays(1);
			}
            this.mIntakeDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["intakedate"].ToString());
			//Notes
			this.dtVNotes = ds.Tables[2];
			//dosages
			this.OriginalRequestedDosage = ds.Tables[3].Rows[0]["originalrequesteddosage"].ToString();
			this.OriginalApprovedDosage = ds.Tables[3].Rows[0]["originalapproveddosage"].ToString();
			this.CurrentDosage = ds.Tables[3].Rows[0]["currentdosage"].ToString();
		}
		//**************************************************************************************************************
		public int VerificationID
		{
			get{return mVerificationID;}
			set{mVerificationID = value;}
		}
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
		public bool Insurance
		{
			get{return mInsurance;}
			set{mInsurance = value;}
		}

		//**************************************************************************************************************
		public bool CoversRx
		{
			get{return mCoversRx;}
			set{mCoversRx = value;}
		}

		//**************************************************************************************************************
		public bool CoversCancerRx
		{
			get{return mCoversCancerRx;}
			set{mCoversCancerRx = value;}
		}

		//**************************************************************************************************************
		public bool CoversGlivecRx
		{
			get{return mCoversGlivecRx;}
			set{mCoversGlivecRx = value;}
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
		public string PatientName
		{
			get{return mPatientName;}
			set{mPatientName = value;}
		}
		//**************************************************************************************************************
		public string City
		{
			get{return mCity;}
			set{mCity = value;}
		}
		//**************************************************************************************************************
		public string CurrentDosage
		{
			get{return mCurrentDosage;}
			set{mCurrentDosage = value;}
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
		public string ReasonChanged
		{
			get{return mReasonChanged;}
			set{mReasonChanged = value;}
		}
        
	}
}
