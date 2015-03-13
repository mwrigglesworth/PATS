using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Request.
	/// </summary>
	public class Request
	{
		private int mRequestID;
		private int mPatientID;
		private string mRequestType;
		private string mPIN;
		private string mFirstName;
		private string mLastName;
        private int mCountryID;
		private DateTime mBirthDate;
		private string mGipapStatus;
		private string mStatusReason;
		private string mReactivateReason;
		private string mDiagnosis;
		private DateTime mStartDate;
		private DateTime mEndDate;
		private DateTime mCloseDate;
		private string mCurrentDosage;
		private string mChangeDosageReason;
		private string mRequestedDosage;
		private string mCurrentCMLPhase;
		private bool mFinancialStatus;
		private bool mRestartTreatment;
		private int mGlivecSupply;
		private string mRemainingSupply;
        private string mNotes;
        private bool mPhysicianRequested;
        private string mReceivedBy;
		private bool mResolved;
        //noa 
        private bool mFlagNOA;
        private bool mNoaPhys;
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
        //SAE
        private bool mAERelated;
        private int mAERequestID;
        //net suite order creation
        private string mTabletStrength;

		private string mRequestHeader;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];

		//**************************************************************************************************************
		public Request()
		{
			this.Clear();
		}
		//**************************************************************************************************************
		public Request(int currid, int patid)
		{
			//Default constructor using the currid to populate the parameters
			DataSet myData;
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[0].Value = currid;

			arrParams[1] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[1].Value = patid;

			myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetRequestProfile", arrParams);
			if(myData.Tables[0].Rows.Count <= 0)
			{
				return;
			}
			else
			{
				Inflate(myData);
			}

			myData.Dispose();
		}
		//**************************************************************************************************************
		public string RequestHeader()
		{
			return this.mRequestHeader + "<br><a href=GIPAP.aspx?trgt=patientinfo&choice=" + this.PatientID.ToString() + ">View Patient Information</a>";
		}
		//**************************************************************************************************************
		public string SupplyUpdateText()
		{
			string supup = "To your knowlege, does the patient have a supply of Glivec beyond " + this.EndDate.Day.ToString() + " " + this.EndDate.ToString("y") + "?<br>";
			if(this.GlivecSupply == 0)
			{
				supup += "<b>No</b>";
			}
			else if(this.GlivecSupply == 1)
			{
				supup += "<b>Yes</b>";
			}
			else if(this.GlivecSupply == 2)
			{
				supup += "<b>Don't Know</b>";
			}
			supup += "<br><br>If yes, please inform us of the amount of supply that remains: (in # of days)<br>";
			supup += this.RemainingSupply + "<br><br>Notes:<br>" + this.Notes;
			return supup;
		}
		//**************************************************************************************************************	
		public void ReActivate(string createdby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[12];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[1].Direction = ParameterDirection.Output;

			arrParams[2] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 500);
			arrParams[2].Value = this.StatusReason;
	
			arrParams[3] = new SqlParameter("@CurrentDosage", SqlDbType.VarChar, 20);
			arrParams[3].Value = this.CurrentDosage;

			arrParams[4] = new SqlParameter("@ChangeDosageReason", SqlDbType.VarChar, 500);
			arrParams[4].Value = this.ChangeDosageReason;

			arrParams[5] = new SqlParameter("@CurrentCMLPhase", SqlDbType.VarChar, 20);
			arrParams[5].Value = this.CurrentCMLPhase;

			arrParams[6] = new SqlParameter("@FinancialStatus", SqlDbType.Bit);
			if(this.FinancialStatus)
			{
				arrParams[6].Value = 1;
			}
			else
			{
				arrParams[6].Value = 0;
			}

			arrParams[7] = new SqlParameter("@ReactivateReason", SqlDbType.VarChar, 500);
			arrParams[7].Value = this.ReactivateReason;

			arrParams[8] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[8].Value = this.Notes;

			arrParams[9] = new SqlParameter("@RestartTreatment", SqlDbType.Bit);
			if(this.RestartTreatment)
			{
				arrParams[9].Value = 1;
			}
			else
			{
				arrParams[9].Value = 0;
			}

			arrParams[10] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[10].Value = createdby;

            arrParams[11] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
            arrParams[11].Value = this.TabletStrength;

            /*arrParams[11] = new SqlParameter("@NOA", SqlDbType.Bit);
            if (this.NoaPhys)
            {
                arrParams[11].Value = 1;
            }
            else
            {
                arrParams[11].Value = 0;
            }*/

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ReActivateRequest", arrParams);

			this.RequestID = (int)arrParams[1].Value;
		}
        //**************************************************************************************************************	
        public void ReActivateWithTasigna(string createdby)
        {
            //Patient exists so update the information
            SqlParameter[] arrParams = new SqlParameter[23];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            arrParams[1] = new SqlParameter("@RequestID", SqlDbType.Int);
            arrParams[1].Direction = ParameterDirection.Output;

            arrParams[2] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 500);
            arrParams[2].Value = this.StatusReason;

            arrParams[3] = new SqlParameter("@CurrentDosage", SqlDbType.VarChar, 20);
            arrParams[3].Value = this.CurrentDosage;

            arrParams[4] = new SqlParameter("@ChangeDosageReason", SqlDbType.VarChar, 500);
            arrParams[4].Value = this.ChangeDosageReason;

            arrParams[5] = new SqlParameter("@CurrentCMLPhase", SqlDbType.VarChar, 20);
            arrParams[5].Value = this.CurrentCMLPhase;

            arrParams[6] = new SqlParameter("@FinancialStatus", SqlDbType.Bit);
            if (this.FinancialStatus)
            {
                arrParams[6].Value = 1;
            }
            else
            {
                arrParams[6].Value = 0;
            }

            arrParams[7] = new SqlParameter("@ReactivateReason", SqlDbType.VarChar, 500);
            arrParams[7].Value = this.ReactivateReason;

            arrParams[8] = new SqlParameter("@Notes", SqlDbType.Text);
            arrParams[8].Value = this.Notes;

            arrParams[9] = new SqlParameter("@RestartTreatment", SqlDbType.Bit);
            if (this.RestartTreatment)
            {
                arrParams[9].Value = 1;
            }
            else
            {
                arrParams[9].Value = 0;
            }

            arrParams[10] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
            arrParams[10].Value = createdby;

            //TASIGNA FIELDS
            arrParams[11] = new SqlParameter("@Imatinib", SqlDbType.Bit);
            if (this.Imatinib)
            { arrParams[11].Value = 1; }
            else
            { arrParams[11].Value = 0; }

            arrParams[12] = new SqlParameter("@GlivecIntolerant", SqlDbType.Bit);
            if (this.GlivecIntolerant)
            { arrParams[12].Value = 1; }
            else
            { arrParams[12].Value = 0; }

            arrParams[13] = new SqlParameter("@GlivecResistant", SqlDbType.Bit);
            if (this.GlivecResistant)
            { arrParams[13].Value = 1; }
            else
            { arrParams[13].Value = 0; }

            arrParams[14] = new SqlParameter("@Dasatinib", SqlDbType.Bit);
            if (this.Dasatinib)
            { arrParams[14].Value = 1; }
            else
            { arrParams[14].Value = 0; }

            arrParams[15] = new SqlParameter("@DasatinibIntolerant", SqlDbType.Bit);
            if (this.DasatinibIntolerant)
            { arrParams[15].Value = 1; }
            else
            { arrParams[15].Value = 0; }

            arrParams[16] = new SqlParameter("@DasatinibResistant", SqlDbType.Bit);
            if (this.DasatinibResistant)
            { arrParams[16].Value = 1; }
            else
            { arrParams[16].Value = 0; }

            arrParams[17] = new SqlParameter("@Tasigna", SqlDbType.Bit);
            if (this.Tasigna)
            { arrParams[17].Value = 1; }
            else
            { arrParams[17].Value = 0; }

            arrParams[18] = new SqlParameter("@TasignaStartDate", SqlDbType.SmallDateTime);
            if (this.TasignaStartDate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[18].Value = this.TasignaStartDate;
            }
            else
            {
                arrParams[18].Value = DBNull.Value;
            }

            arrParams[19] = new SqlParameter("@PrevTasignaDose", SqlDbType.NVarChar, 20);
            arrParams[19].Value = this.PrevTasignaDose;

            arrParams[20] = new SqlParameter("@TasignaPatientConsent", SqlDbType.Bit);
            arrParams[20].Value = this.TasignaPatientConsent;

            arrParams[21] = new SqlParameter("@NOATasigna", SqlDbType.Bit);
            if (this.NOATasigna)
            { arrParams[21].Value = 1; }
            else
            { arrParams[21].Value = 0; }

            arrParams[22] = new SqlParameter("@Treatment", SqlDbType.NVarChar, 20);
            arrParams[22].Value = this.Treatment;


            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ReActivateRequestWithTasigna", arrParams);

            this.RequestID = (int)arrParams[1].Value;
        }
        //**************************************************************************************************************	
        public void ChangeTreatment(string createdby)
        {
            //Patient exists so update the information
            SqlParameter[] arrParams = new SqlParameter[21];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            arrParams[1] = new SqlParameter("@RequestID", SqlDbType.Int);
            arrParams[1].Direction = ParameterDirection.Output;

            arrParams[2] = new SqlParameter("@CurrentCMLPhase", SqlDbType.VarChar, 20);
            arrParams[2].Value = this.CurrentCMLPhase;

            arrParams[3] = new SqlParameter("@CurrentDosage", SqlDbType.VarChar, 20);
            arrParams[3].Value = this.CurrentDosage;

            //TASIGNA FIELDS
            arrParams[4] = new SqlParameter("@Imatinib", SqlDbType.Bit);
            if (this.Imatinib)
            { arrParams[4].Value = 1; }
            else
            { arrParams[4].Value = 0; }

            arrParams[5] = new SqlParameter("@GlivecIntolerant", SqlDbType.Bit);
            if (this.GlivecIntolerant)
            { arrParams[5].Value = 1; }
            else
            { arrParams[5].Value = 0; }

            arrParams[6] = new SqlParameter("@GlivecResistant", SqlDbType.Bit);
            if (this.GlivecResistant)
            { arrParams[6].Value = 1; }
            else
            { arrParams[6].Value = 0; }

            arrParams[7] = new SqlParameter("@Dasatinib", SqlDbType.Bit);
            if (this.Dasatinib)
            { arrParams[7].Value = 1; }
            else
            { arrParams[7].Value = 0; }

            arrParams[8] = new SqlParameter("@DasatinibIntolerant", SqlDbType.Bit);
            if (this.DasatinibIntolerant)
            { arrParams[8].Value = 1; }
            else
            { arrParams[8].Value = 0; }

            arrParams[9] = new SqlParameter("@DasatinibResistant", SqlDbType.Bit);
            if (this.DasatinibResistant)
            { arrParams[9].Value = 1; }
            else
            { arrParams[9].Value = 0; }

            arrParams[10] = new SqlParameter("@Tasigna", SqlDbType.Bit);
            if (this.Tasigna)
            { arrParams[10].Value = 1; }
            else
            { arrParams[10].Value = 0; }

            arrParams[11] = new SqlParameter("@TasignaStartDate", SqlDbType.SmallDateTime);
            if (this.TasignaStartDate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[11].Value = this.TasignaStartDate;
            }
            else
            {
                arrParams[11].Value = DBNull.Value;
            }

            arrParams[12] = new SqlParameter("@PrevTasignaDose", SqlDbType.NVarChar, 20);
            arrParams[12].Value = this.PrevTasignaDose;

            arrParams[13] = new SqlParameter("@TasignaPatientConsent", SqlDbType.Bit);
            arrParams[13].Value = this.TasignaPatientConsent;

            arrParams[14] = new SqlParameter("@NOATasigna", SqlDbType.Bit);
            if (this.NOATasigna)
            { arrParams[14].Value = 1; }
            else
            { arrParams[14].Value = 0; }

            arrParams[15] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
            arrParams[15].Value = createdby;

            arrParams[16] = new SqlParameter("@Notes", SqlDbType.Text);
            arrParams[16].Value = this.Notes;

            arrParams[17] = new SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 50);
            arrParams[17].Value = this.ReceivedBy;

            arrParams[18] = new SqlParameter("@PhysicianRequested", SqlDbType.Bit);
            arrParams[18].Value = this.PhysicianRequested;

            arrParams[19] = new SqlParameter("@AERelated", SqlDbType.Bit);
            arrParams[19].Value = this.AERelated;

            arrParams[20] = new SqlParameter("@AERequestID", SqlDbType.Int);
            arrParams[20].Value = this.AERequestID;


            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ChangeTreatmentRequest", arrParams);

            this.RequestID = (int)arrParams[1].Value;
        }
		//**************************************************************************************************************	
		public void Close(string closedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[9];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 500);
			arrParams[1].Value = this.StatusReason;
			
			arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[2].Value = closedby;

			arrParams[3] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[3].Value = this.Notes;

			arrParams[4] = new SqlParameter("@RequestID", SqlDbType.Int);
            arrParams[4].Direction = ParameterDirection.Output;

            arrParams[5] = new SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 50);
            arrParams[5].Value = this.ReceivedBy;

            arrParams[6] = new SqlParameter("@PhysicianRequested", SqlDbType.Bit);
            arrParams[6].Value = this.PhysicianRequested;

            arrParams[7] = new SqlParameter("@AERelated", SqlDbType.Bit);
            arrParams[7].Value = this.AERelated;

            arrParams[8] = new SqlParameter("@AERequestID", SqlDbType.Int);
            arrParams[8].Value = this.AERequestID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CloseRequest", arrParams);

			this.RequestID = (int)arrParams[4].Value;
		}
        //**************************************************************************************************************	
        public void AdverseEvent(string createdby)
        {
            //Patient exists so update the information
            SqlParameter[] arrParams = new SqlParameter[5];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            arrParams[1] = new SqlParameter("@Notes", SqlDbType.Text);
            arrParams[1].Value = this.Notes;

            arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
            arrParams[2].Value = createdby;

            arrParams[3] = new SqlParameter("@RequestID", SqlDbType.Int);
            arrParams[3].Direction = ParameterDirection.Output;

            arrParams[4] = new SqlParameter("@PhysicianRequested", SqlDbType.Bit);
            arrParams[4].Value = this.PhysicianRequested;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_AERequest", arrParams);

            this.RequestID = (int)arrParams[3].Value;
        }
		//**************************************************************************************************************	
		public void Extend(string extendedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[7];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 500);
			arrParams[1].Value = this.StatusReason;

			arrParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[2].Value = this.EndDate;

			arrParams[3] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[3].Value = extendedby;

			arrParams[4] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[4].Direction = ParameterDirection.Output;

            arrParams[5] = new SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 50);
            arrParams[5].Value = this.ReceivedBy;

            arrParams[6] = new SqlParameter("@PhysicianRequested", SqlDbType.Bit);
            arrParams[6].Value = this.PhysicianRequested;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ExtendRequest", arrParams);

			this.RequestID = (int)arrParams[4].Value;
		}
		//**************************************************************************************************************	
		public void ChangeDosage(string createdby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[10];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@RequestedDosage", SqlDbType.VarChar, 20);
			arrParams[1].Value = this.RequestedDosage;

			arrParams[2] = new SqlParameter("@CurrentDosage", SqlDbType.VarChar, 20);
			arrParams[2].Value = this.CurrentDosage;

			arrParams[3] = new SqlParameter("@ChangeDosageReason", SqlDbType.VarChar, 500);
            arrParams[3].Value = this.ChangeDosageReason;//changing this to go to notes

			arrParams[4] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[4].Value = createdby;

			arrParams[5] = new SqlParameter("@RequestID", SqlDbType.Int);
            arrParams[5].Direction = ParameterDirection.Output;

            arrParams[6] = new SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 50);
            arrParams[6].Value = this.ReceivedBy;

            arrParams[7] = new SqlParameter("@PhysicianRequested", SqlDbType.Bit);
            arrParams[7].Value = this.PhysicianRequested;

            arrParams[8] = new SqlParameter("@AERelated", SqlDbType.Bit);
            arrParams[8].Value = this.AERelated;

            arrParams[9] = new SqlParameter("@AERequestID", SqlDbType.Int);
            arrParams[9].Value = this.AERequestID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ChangeDosageRequest", arrParams);

			this.RequestID = (int)arrParams[5].Value;
		}
		//**************************************************************************************************************	
		public void Deny(string deniedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[5];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 500);
			arrParams[1].Value = this.StatusReason;
			
			arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[2].Value = deniedby;

			arrParams[3] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[3].Value = this.Notes;

			arrParams[4] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[4].Direction = ParameterDirection.Output;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DenyRequest", arrParams);

			this.RequestID = (int)arrParams[4].Value;
		}
		//**************************************************************************************************************	
		public void ReAssess(string createdby, string cNote)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[4];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[1].Value = cNote;
			
			arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
			arrParams[2].Value = createdby;

			arrParams[3] = new SqlParameter("@RequestID", SqlDbType.Int);
            arrParams[3].Direction = ParameterDirection.Output;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ReAssessRequest", arrParams);

			this.RequestID = (int)arrParams[3].Value;
		}
        //**************************************************************************************************************	
        public void ReAssessWithTasigna(string createdby, string cNote)
        {
            //Patient exists so update the information
            SqlParameter[] arrParams = new SqlParameter[17];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            arrParams[1] = new SqlParameter("@Notes", SqlDbType.Text);
            arrParams[1].Value = cNote;

            arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
            arrParams[2].Value = createdby;

            arrParams[3] = new SqlParameter("@RequestID", SqlDbType.Int);
            arrParams[3].Direction = ParameterDirection.Output;

            //TASIGNA FIELDS
            arrParams[4] = new SqlParameter("@Imatinib", SqlDbType.Bit);
            if (this.Imatinib)
            { arrParams[4].Value = 1; }
            else
            { arrParams[4].Value = 0; }

            arrParams[5] = new SqlParameter("@GlivecIntolerant", SqlDbType.Bit);
            if (this.GlivecIntolerant)
            { arrParams[5].Value = 1; }
            else
            { arrParams[5].Value = 0; }

            arrParams[6] = new SqlParameter("@GlivecResistant", SqlDbType.Bit);
            if (this.GlivecResistant)
            { arrParams[6].Value = 1; }
            else
            { arrParams[6].Value = 0; }

            arrParams[7] = new SqlParameter("@Dasatinib", SqlDbType.Bit);
            if (this.Dasatinib)
            { arrParams[7].Value = 1; }
            else
            { arrParams[7].Value = 0; }

            arrParams[8] = new SqlParameter("@DasatinibIntolerant", SqlDbType.Bit);
            if (this.DasatinibIntolerant)
            { arrParams[8].Value = 1; }
            else
            { arrParams[8].Value = 0; }

            arrParams[9] = new SqlParameter("@DasatinibResistant", SqlDbType.Bit);
            if (this.DasatinibResistant)
            { arrParams[9].Value = 1; }
            else
            { arrParams[9].Value = 0; }

            arrParams[10] = new SqlParameter("@Tasigna", SqlDbType.Bit);
            if (this.Tasigna)
            { arrParams[10].Value = 1; }
            else
            { arrParams[10].Value = 0; }

            arrParams[11] = new SqlParameter("@TasignaStartDate", SqlDbType.SmallDateTime);
            if (this.TasignaStartDate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[11].Value = this.TasignaStartDate;
            }
            else
            {
                arrParams[11].Value = DBNull.Value;
            }

            arrParams[12] = new SqlParameter("@PrevTasignaDose", SqlDbType.NVarChar, 20);
            arrParams[12].Value = this.PrevTasignaDose;

            arrParams[13] = new SqlParameter("@TasignaPatientConsent", SqlDbType.Bit);
            arrParams[13].Value = this.TasignaPatientConsent;

            arrParams[14] = new SqlParameter("@NOATasigna", SqlDbType.Bit);
            if (this.NOATasigna)
            { arrParams[14].Value = 1; }
            else
            { arrParams[14].Value = 0; }

            arrParams[15] = new SqlParameter("@Treatment", SqlDbType.NVarChar, 20);
            arrParams[15].Value = this.Treatment;

            arrParams[16] = new SqlParameter("@CurrentDosage", SqlDbType.NVarChar, 20);
            arrParams[16].Value = this.CurrentDosage;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ReAssessRequestWithTasigna", arrParams);

            this.RequestID = (int)arrParams[3].Value;
        }
		//**************************************************************************************************************	
		public void SuplyUpdate(string createdby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[7];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@GlivecSupply", SqlDbType.Int);
			arrParams[1].Value = this.GlivecSupply;

			arrParams[2] = new SqlParameter("@RemainingSupply", SqlDbType.VarChar, 50);
			arrParams[2].Value = this.RemainingSupply;

			arrParams[3] = new SqlParameter("@EndDate", SqlDbType.SmallDateTime);
			arrParams[3].Value = this.EndDate;

			arrParams[4] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[4].Value = this.Notes;
			
			arrParams[5] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
			arrParams[5].Value = createdby;

			arrParams[6] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[6].Direction = ParameterDirection.Output;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_SupplyUpdate", arrParams);

			this.RequestID = (int)arrParams[6].Value;
		}
		//**************************************************************************************************************	
		public void Resolve(string modifiedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[0].Value = this.RequestID;
			
			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 20);
			arrParams[1].Value = modifiedby;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_Resolve", arrParams);
		}
		//**************************************************************************************************************	
		public string ConfirmationText(string homepage, string uRole)
		{
			string ctext = "<br>";
			if(this.RequestType == "Close")
			{
				ctext += "You have requested to close patient <font color=steelblue>" + this.FirstName + " " + this.LastName + "</font>. ";
				ctext += "Your request has been noted and will be reviewed by a Max Foundation Program Officer as soon as possible.<br><br>Thank You.<br><br>";
				if(uRole == "Physician")
				{
					ctext += "Remember that you <u>must</u> report suspected serious adverse events to Glivec therapy to the Novartis IMS/PVO Desk within 24 hours.<br><br>Please click on the link below to learn more about reporting Serious Adverse Events (SAE) to Novartis and to download the Novartis SAE form.<br><br>";
					ctext += "<br><br><A href='Physician/SAEForm.doc'>Click here to download Novartis Serious Adverse Event form</A>";
				}
			}
			else if(this.RequestType == "Reactivate")
			{
				ctext += "You have requested to Reactivate patient <font color=steelblue>" + this.FirstName + " " + this.LastName + "</font>. ";
				ctext += "Your request has been noted and will be reviewed by a Max Foundation Program Officer as soon as possible.<br><br>Thank You.";
			}
			else if(this.RequestType == "Extend")
			{
				ctext += "You have requested to extend the current period for patient <font color=steelblue>" + this.FirstName + " " + this.LastName + "</font>. ";
				ctext += "Your request has been noted and will be reviewed by a Max Foundation Program Officer as soon as possible.<br><br>Thank You.";
			}
			else if(this.RequestType == "DosageChange")
			{
				ctext += "You have requested a dosage change for patient <font color=steelblue>" + this.FirstName + " " + this.LastName + "</font>. ";
				ctext += "Your request has been noted and will be reviewed by a Max Foundation Program Officer as soon as possible.<br><br>Thank You.";
			}
			else if(this.RequestType == "Deny")
			{
				ctext += "You have requested to Deny acceptance for patient <font color=steelblue>" + this.FirstName + " " + this.LastName + "</font>. ";
				ctext += "Your request has been noted and will be reviewed by a Max Foundation Program Officer as soon as possible.<br><br>Thank You.";
			}
			else if(this.RequestType == "ReAssess")
			{
				ctext += "You have requested to re-assess the case for patient <font color=steelblue>" + this.FirstName + " " + this.LastName + "</font>. ";
				ctext += "Your request has been noted and will be reviewed by a Max Foundation Program Officer as soon as possible.<br><br>Thank You.";
			}
			else if(this.RequestType == "SupplyUpdate")
			{
				ctext += "You have logged a supply update for patient <font color=steelblue>" + this.FirstName + " " + this.LastName + "</font>. ";
				ctext += "Your request has been noted and will be reviewed by a Max Foundation Program Officer as soon as possible.<br><br>Thank You.";
            }
            else if (this.RequestType == "ChangeTreatment")
            {
                ctext += "You have requested to Approve patient <font color=steelblue>" + this.FirstName + " " + this.LastName + "</font> for <font color=purple><b>Tasigna</b></font>. ";
                ctext += "Your request has been noted and will be processed as soon as possible.<br><br>Thank You.";
            }
			ctext += "<br><br><a href=" + homepage + "><font color=steelblue>Click Here to Return to Your Homepage</a><br>";
			return ctext;
		}
		//**************************************************************************************************************
		private void Clear()
		{
			this.PatientID = 0;
			this.PIN = "";
			this.FirstName = "";
			this.LastName = "";
			this.GipapStatus = "";
			this.Diagnosis = "";
			//request table
			this.RequestID = 0;
			this.StatusReason = "";
			this.ReactivateReason = "";
			this.CurrentDosage = "";
			this.RequestedDosage = "";
			this.ChangeDosageReason = "";
			this.FinancialStatus = false;
			this.RestartTreatment = false;
			this.GlivecSupply = 2;
			this.RemainingSupply = "";
			this.Notes = "";
			this.Resolved = false;
		}
		//**************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			this.PatientID = Convert.ToInt32(ds.Tables[0].Rows[0]["PatientID"]);
			this.PIN = ds.Tables[0].Rows[0]["PIN"].ToString();
			this.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
            this.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            this.CountryID = Convert.ToInt32(ds.Tables[0].Rows[0]["countryid"]);
			this.BirthDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["birthdate"]);
			this.GipapStatus = ds.Tables[0].Rows[0]["GipapStatus"].ToString();
            this.Diagnosis = ds.Tables[0].Rows[0]["Diagnosis"].ToString();
            this.Treatment = ds.Tables[0].Rows[0]["treatment"].ToString();
			//request table
			if(ds.Tables[1].Rows.Count > 0)
			{
				this.RequestID = Convert.ToInt32(ds.Tables[1].Rows[0]["RequestID"]);
				this.RequestType = ds.Tables[1].Rows[0]["RequestType"].ToString();
				this.StatusReason = (ds.Tables[1].Rows[0]["StatusReason"]).ToString();
				this.ReactivateReason = (ds.Tables[1].Rows[0]["ReactivateReason"]).ToString();
				try
				{
					this.StartDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["StartDate"]);
				}
				catch{}
				try
				{
					this.EndDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["EndDate"]);
				}
				catch{}
				try
				{
					this.CloseDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["ClosedDate"]);
				}
				catch{}
				try
				{
					this.CurrentDosage = ds.Tables[1].Rows[0]["CurrentDosage"].ToString();
				}
				catch{}
				try
				{
					this.ChangeDosageReason = ds.Tables[1].Rows[0]["ChangeDosageReason"].ToString();
				}
				catch{}
				try
				{
					this.RequestedDosage = ds.Tables[1].Rows[0]["RequestedDosage"].ToString();
				}
				catch{}
				try
				{
					this.FinancialStatus = Convert.ToBoolean(ds.Tables[1].Rows[0]["FinancialStatus"]);
					this.RestartTreatment = Convert.ToBoolean(ds.Tables[1].Rows[0]["RestartTreatment"]);
				}
				catch{}
				try
				{
					this.GlivecSupply = Convert.ToInt32(ds.Tables[1].Rows[0]["GlivecSupply"].ToString());
				}
				catch{}
				try
				{
					this.RemainingSupply = ds.Tables[1].Rows[0]["RemainingSupply"].ToString();
				}
				catch{}
				try
				{
					this.Notes = ds.Tables[1].Rows[0]["Notes"].ToString();
				}
                catch { }
                this.AERelated = Convert.ToBoolean(ds.Tables[1].Rows[0]["AERelated"]);
                try
                {
                    this.AERequestID = Convert.ToInt32(ds.Tables[1].Rows[0]["AERequestID"]);
                }
                catch { this.AERequestID = 0; }
                try
                {
                    this.PhysicianRequested = Convert.ToBoolean(ds.Tables[1].Rows[0]["PhysicianRequested"]);
                }
                catch { }
                try
                {
                    this.TabletStrength = ds.Tables[1].Rows[0]["TabletStrength"].ToString();
                }
                catch { }
                this.ReceivedBy = ds.Tables[1].Rows[0]["ReceivedBy"].ToString();
                this.Resolved = Convert.ToBoolean(ds.Tables[1].Rows[0]["Resolved"]);
                this.CurrentCMLPhase = ds.Tables[1].Rows[0]["CurrentCMLPhase"].ToString();
			}
			if(ds.Tables[2].Rows.Count > 0)
			{
				this.CurrentDosage = ds.Tables[2].Rows[0]["CurrentDosage"].ToString();
				if(this.StartDate == Convert.ToDateTime("1/1/0001"))
				{
					this.StartDate = Convert.ToDateTime(ds.Tables[2].Rows[0]["StartDate"]);
				}
			}
            //if(ds.Tables[3].Rows.Count > 0)
            //{
            //    this.CurrentCMLPhase = ds.Tables[3].Rows[0]["CurrentCMLPhase"].ToString();
            //}
			if(ds.Tables[4].Rows.Count >0)
			{
				this.mRequestHeader = ds.Tables[4].Rows[0]["statusheader"].ToString();
			}
			if(ds.Tables[5].Rows.Count >0)
			{
				this.mRequestHeader += ds.Tables[5].Rows[0]["statusheader"].ToString();
			}
            if (ds.Tables[6].Rows.Count > 0)
            {
                int dl;
                try
                {
                    dl = Convert.ToInt32(ds.Tables[6].Rows[0]["donationlength"]);
                }
                catch { dl = 0; }
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["noa"]) && dl != 12 && dl != 370)
                {
                    this.FlagNoa = true;
                }
                else
                {
                    this.FlagNoa = false;
                }
            }
            else
            {
                this.FlagNoa = false;
            }
            //check if noa phys
            this.NoaPhys = false;
            if (ds.Tables[7].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                {
                    if (Convert.ToBoolean(ds.Tables[7].Rows[i]["noa"]))
                    {
                        if (Convert.ToDateTime(ds.Tables[7].Rows[i]["noadate"]) < DateTime.Today)
                        {
                            this.NoaPhys = true;
                            break;
                        }
                    }
                }
            }

            //tasigna
            if (ds.Tables[8].Rows.Count > 0)
            {
                this.GlivecIntolerant = Convert.ToBoolean(ds.Tables[8].Rows[0]["GlivecIntolerant"]);
                this.GlivecResistant = Convert.ToBoolean(ds.Tables[8].Rows[0]["GlivecResistant"]);
            }
		}
		//**************************************************************************************************************
		public int RequestID
		{
			get{return mRequestID;}
			set{mRequestID = value;}
		}
		//**************************************************************************************************************
		public string RequestType
		{
			get{return mRequestType;}
			set{mRequestType = value;}
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
        //**************************************************************************************************************
        public int CountryID
        {
            get { return mCountryID; }
            set { mCountryID = value; }
        }
		//**************************************************************************************************************
		public DateTime BirthDate
		{
			get{return mBirthDate;}
			set{mBirthDate = value;}
		}
		//**************************************************************************************************************
		public string GipapStatus
		{
			get{return mGipapStatus;}
			set{mGipapStatus = value;}
		}
		//**************************************************************************************************************
		public string StatusReason
		{
			get{return mStatusReason;}
			set{mStatusReason = value;}
		}
		//**************************************************************************************************************
		public string ReactivateReason
		{
			get{return mReactivateReason;}
			set{mReactivateReason = value;}
		}
		//**************************************************************************************************************
		public string Diagnosis
		{
			get{return mDiagnosis;}
			set{mDiagnosis = value;}
		}
		//**************************************************************************************************************
		public DateTime CloseDate
		{
			get{return mCloseDate;}
			set{mCloseDate = value;}
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
		public string RequestedDosage
		{
			get{return mRequestedDosage;}
			set{mRequestedDosage = value;}
		}
		//**************************************************************************************************************
		public string ChangeDosageReason
		{
			get{return mChangeDosageReason;}
			set{mChangeDosageReason = value;}
		}
		//**************************************************************************************************************
		public string CurrentCMLPhase
		{
			get{return mCurrentCMLPhase;}
			set{mCurrentCMLPhase = value;}
		}
		//**************************************************************************************************************
		public bool FinancialStatus
		{
			get{return mFinancialStatus;}
			set{mFinancialStatus = value;}
		}
		//**************************************************************************************************************
		public bool RestartTreatment
		{
			get{return mRestartTreatment;}
			set{mRestartTreatment = value;}
		}
		//**************************************************************************************************************
		public int GlivecSupply
		{
			get{return mGlivecSupply;}
			set{mGlivecSupply = value;}
		}
		//**************************************************************************************************************
		public string RemainingSupply
		{
			get{return mRemainingSupply;}
			set{mRemainingSupply = value;}
		}
		//**************************************************************************************************************
		public string Notes
		{
			get{return mNotes;}
			set{mNotes = value;}
		}
        //**************************************************************************************************************
        public bool PhysicianRequested
        {
            get { return mPhysicianRequested; }
            set { mPhysicianRequested = value; }
        }
        //**************************************************************************************************************
        public string ReceivedBy
        {
            get { return mReceivedBy; }
            set { mReceivedBy = value; }
        }
		//**************************************************************************************************************
		public bool Resolved
		{
			get{return mResolved;}
			set{mResolved = value;}
		}
        //**************************************************************************************************************
        public bool FlagNoa
        {
            get { return mFlagNOA; }
            set { mFlagNOA = value; }
        }
        //**************************************************************************************************************
        public bool NoaPhys
        {
            get { return mNoaPhys; }
            set { mNoaPhys = value; }
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
        public bool AERelated
        {
            get { return mAERelated; }
            set { mAERelated = value; }
        }
        //*********************************************************************************************************************
        public int AERequestID
        {
            get { return mAERequestID; }
            set { mAERequestID = value; }
        }
        //*********************************************************************************************************************
        public string TabletStrength
        {
            get { return mTabletStrength; }
            set { mTabletStrength = value; }
        }
	}
}
