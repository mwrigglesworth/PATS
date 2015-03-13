 using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for GipapStatus.
	/// </summary>
	public class PatientGipapStatus
	{
		private int mPatientID;
		private string mPIN;
		private string mFirstName;
		private string mLastName;
        private int mCountryID;
        private string CurrentProgram;
		private DateTime mBirthDate;
		private string mGipapStatus;
		private string mStatusReason;
		private string mDiagnosis;
        private bool mAdjuvant;
		private DateTime mStartDate;
		private DateTime mEndDate;
		private string mCurrentDosage;
		private string mChangeDosageReason;
		private string mCurrentCMLPhase;
		private bool mFinancialStatus;
		private bool mRestartTreatment;
		private bool mAutoApprove;
		private bool mEnableAutoApprove;
		//private string mHematologicalResponse;
		//private string mCytogeneticResponse;
		//private string mTumorResponse;
		//private int mGlivecSupply;
		//private string mRemainingSupply;
		private string mNotes;
        private bool mPhysicianRequested;
		private string mReceivedBy;
		private bool mReminderEmail;
		private bool mReminderEmail90;
		private bool mPatientConsent;
        private bool mFlagNoa;
        private bool mNoaPhys;
        private bool mNoaFEFNeeded;
        private string mTreatment;
        private int mDonationLength;
        //net suite order creation
        private int mStrips100mg;
        private int mStrips400mg;
        private string mTabletStrength;
        private int mReturn400mg; // used if patient needs to bring back pills on a dosage decrease, done in TABLETS not strips
        private bool mActiveDetailPickedUp; //if the current active order is picked up
        private bool mNotPickedUpContacted;
        private DateTime mDetailCreateDate;
        private bool mNewOrderNumber; //on dose change do we issue a new order number (true), or update of old
        private int Truncate100mg;
        private int Truncate400mg;
        //SAE
        private bool mAERelated;

		private string mStatusHeader;
		private bool mPhysicianApproved;

		/*for requests*/
		public int RequestID;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAP2;PWD=secret;UID=sa;";

		public PatientGipapStatus()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		//**************************************************************************************************************
		public PatientGipapStatus(int currid, string action)
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
	
				arrParams[1] = new SqlParameter("@Action", SqlDbType.VarChar, 20);
				arrParams[1].Value = action;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetStatusProfile", arrParams);
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
		}
        //**************************************************************************************************************	
        private void SetStripNumbers()
        {
            if (this.CurrentDosage == "100mg")
            {
                this.Strips100mg = 12;
                this.Strips400mg = 0;
            }
            else if (this.CurrentDosage == "200mg")
            {
                this.Strips100mg = 24;
                this.Strips400mg = 0;
            }
            else if (this.CurrentDosage == "260mg")
            {
                this.Strips100mg = 36;
                this.Strips400mg = 0;
            }
            else if (this.CurrentDosage == "300mg")
            {
                this.Strips100mg = 36;
                this.Strips400mg = 0;
            }
            else if (this.CurrentDosage == "400mg")
            {
                if (this.TabletStrength == "1 x 400mg")
                {
                    this.Strips100mg = 0;
                    this.Strips400mg = 12;
                }
                else
                {
                    this.Strips100mg = 48;
                    this.Strips400mg = 0;
                }
            }
            else if (this.CurrentDosage == "600mg")
            {
                if (this.TabletStrength == "1 x 400mg + 2 x 100mg")
                {
                    this.Strips100mg = 24;
                    this.Strips400mg = 12;
                }
                else
                {
                    this.Strips100mg = 72;
                    this.Strips400mg = 0;
                }
            }
            else if (this.CurrentDosage == "800mg")
            {
                if (this.TabletStrength == "2 x 400mg")
                {
                    this.Strips100mg = 0;
                    this.Strips400mg = 24;
                }
                else
                {
                    this.Strips100mg = 96;
                    this.Strips400mg = 0;
                }
            }
        }
        //**************************************************************************************************************	
        public void UpdateTabletStrength(string modifiedby)
        {
            //Patient exists so update the information
            SqlParameter[] arrParams = new SqlParameter[5];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
            arrParams[1].Value = modifiedby;

            if (this.CountryID == 76)
            {
                //this.SetStripNumbers();
                //TRY using this instead, in case the 
                this.CalculateDoseChangeStrips(this.CurrentDosage, this.CurrentDosage);
                arrParams[2] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
                arrParams[2].Value = this.TabletStrength;

                arrParams[3] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[3].Value = this.Strips100mg;

                arrParams[4] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[4].Value = this.Strips400mg;
            }
            else
            {
                arrParams[2] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
                arrParams[2].Value = DBNull.Value;

                arrParams[3] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[3].Value = DBNull.Value;

                arrParams[4] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[4].Value = DBNull.Value;
            }

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateTabletStrength", arrParams);
        }
        //**************************************************************************************************************	
        public void Unexpire(string modifiedby)
        {
            //Patient exists so update the information
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
            arrParams[1].Value = modifiedby;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UnexpireOrder", arrParams);
        }
        //**************************************************************************************************************	
        private void CalculateDoseChangeStrips(string oldDose, string newDose)
        {
            this.Return400mg = 0;
            int oldD = Convert.ToInt32(oldDose.Replace("mg", ""));
            int newD = Convert.ToInt32(newDose.Replace("mg", ""));
            int doseIncrease = newD - oldD;
            TimeSpan ts;
            if (DateTime.Today > this.StartDate)
            {
                ts = (this.EndDate - DateTime.Today);
            }
            else //period hasn't started yet
            {
                ts = (this.EndDate - this.StartDate);
            }
            int periodDays = Convert.ToInt32(ts.TotalDays);
            if (periodDays > 0) 
            {
                if (this.ActiveDetailPickedUp)//if their order has been picked up, they are only given the increase
                {
                    this.mNewOrderNumber = true; //new "DI" order number
                    if (doseIncrease == 400 && this.TabletStrength == "1 x 400mg") //400 to 800, 1 pill a day needed
                    {
                        this.Strips100mg = 0;
                        this.Strips400mg = periodDays / 10 + Convert.ToInt32(periodDays % 10 > 0); //checks to see if there is a remainder and converts bool to 1 or 0 more strips!!

                        this.Truncate100mg = 0;
                        this.Truncate400mg = 1;
                    }
                    else if (doseIncrease > 40) //dose increase only, 260mg to 300mg does not require more pills
                    {
                        int NoPills = ((doseIncrease / 100)/*number of pills a day*/ * periodDays);
                        this.Strips100mg = NoPills / 10 + Convert.ToInt32(NoPills % 10 > 0);
                        this.Strips400mg = 0;

                        this.Truncate100mg = (doseIncrease / 100);/*number of pills a day (i can leave out *10 days / 10 pills per strip)*/
                        this.Truncate400mg = 0;
                    }
                    else if (doseIncrease < 0 && oldD >= 400 && newD < 400 && this.TabletStrength.IndexOf("400mg") != -1) //need the full order of the new dosage in 100mg pills
                    {
                        int NoPills = ((newD / 100)/*number of pills a day*/ * periodDays);
                        this.Strips100mg = NoPills / 10 + Convert.ToInt32(NoPills % 10 > 0);
                        this.Strips400mg = 0;

                        this.Truncate100mg = (newD / 100)/*number of pills a day*/;
                        this.Truncate400mg = 0;
                        //calculate return in TABLETS not strips
                        if (oldD == 800)
                        {
                            this.Return400mg = periodDays * 2; /*2pills a day*/
                        }
                        else
                        {
                            if (oldD == 600)
                            {
                                int Number100mgRemaining = periodDays * 2; //patient still has 2 100mg pills a day for the rest of the period
                                int AdditionalPillsNeeded = NoPills - Number100mgRemaining;
                                this.Strips100mg = AdditionalPillsNeeded / 10 + Convert.ToInt32(AdditionalPillsNeeded % 10 > 0);
                            }
                            this.Return400mg = periodDays;
                        }
                    }
                    else
                    {
                        this.Strips100mg = 0;
                        this.Strips400mg = 0;

                        this.Truncate100mg = 0;
                        this.Truncate400mg = 0;
                    }
                }
                else //original order has not been picked up yet, so we are issuing a order for the full daily dosage, with the original order number from detail tbl
                {
                    this.mNewOrderNumber = false; //keep original order number

                    if (newDose == "100mg")
                    {
                        this.Strips100mg = periodDays / 10 + Convert.ToInt32(periodDays % 10 > 0);
                        this.Strips400mg = 0;

                        this.Truncate100mg = 1;
                        this.Truncate400mg = 0;
                    }
                    else if (newDose == "260mg" || newDose == "300mg")
                    {
                        this.Strips100mg = (3 * periodDays) / 10 + Convert.ToInt32(periodDays % 10 > 0);
                        this.Strips400mg = 0;

                        this.Truncate100mg = 3;
                        this.Truncate400mg = 0;
                    }
                    else if (newDose == "400mg")
                    {
                        if (this.TabletStrength.IndexOf("400mg") != -1 || oldD < 400)
                        {
                            this.Strips100mg = 0;
                            this.Strips400mg = periodDays / 10 + Convert.ToInt32(periodDays % 10 > 0);

                            this.Truncate100mg = 0;
                            this.Truncate400mg = 1;
                        }
                        else
                        {
                            this.Strips100mg = (4 * periodDays) / 10 + Convert.ToInt32(periodDays % 10 > 0);
                            this.Strips400mg = 0;

                            this.Truncate100mg = 4;
                            this.Truncate400mg = 0;
                        }
                    }
                    else if (newDose == "600mg")
                    {
                        if (this.TabletStrength.IndexOf("400mg") != -1 || oldD < 400)
                        {
                            this.Strips100mg = (2 * periodDays) / 10 + Convert.ToInt32(periodDays % 10 > 0);
                            this.Strips400mg = periodDays / 10 + Convert.ToInt32(periodDays % 10 > 0);

                            this.Truncate100mg = 2;
                            this.Truncate400mg = 1;
                        }
                        else
                        {
                            this.Strips100mg = (6 * periodDays) / 10 + Convert.ToInt32(periodDays % 10 > 0);
                            this.Strips400mg = 0;

                            this.Truncate100mg = 6;
                            this.Truncate400mg = 0;
                        }
                    }
                    else if (newDose == "800mg")
                    {
                        if (this.TabletStrength.IndexOf("400mg") != -1 || oldD < 400)
                        {
                            this.Strips100mg = 0;
                            this.Strips400mg = (2 * periodDays) / 10 + Convert.ToInt32(periodDays % 10 > 0);

                            this.Truncate100mg = 0;
                            this.Truncate400mg = 2;
                        }
                        else
                        {
                            this.Strips100mg = (8 * periodDays) / 10 + Convert.ToInt32(periodDays % 10 > 0);
                            this.Strips400mg = 0;

                            this.Truncate100mg = 8;
                            this.Truncate400mg = 0;
                        }
                    }

                }
            }
            else
            {
                this.Strips100mg = 0;
                this.Strips400mg = 0;

                this.Truncate100mg = 0;
                this.Truncate400mg = 0;
            }

        }
		//**************************************************************************************************************	
		public void Approve(string approvedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[11];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;
	
			arrParams[1] = new SqlParameter("@CurrentDosage", SqlDbType.VarChar, 20);
			arrParams[1].Value = this.CurrentDosage;

			arrParams[2] = new SqlParameter("@ChangeDosageReason", SqlDbType.VarChar, 500);
			arrParams[2].Value = this.ChangeDosageReason;

			arrParams[3] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[3].Value = this.Notes;

			arrParams[4] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[4].Value = approvedby;

			arrParams[5] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 500);
            arrParams[5].Value = this.StatusReason;
            
            arrParams[6] = new SqlParameter("@NOA", SqlDbType.Bit);
            if (this.FlagNoa)
            {
                arrParams[6].Value = 1;
            }
            else
            {
                arrParams[6].Value = 0;
            }

            if (this.CountryID == 76)
            {
                this.SetStripNumbers();
                arrParams[7] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
                arrParams[7].Value = this.TabletStrength;

                arrParams[8] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[8].Value = this.Strips100mg;

                arrParams[9] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[9].Value = this.Strips400mg;
            }
            else
            {
                arrParams[7] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
                arrParams[7].Value = DBNull.Value;

                arrParams[8] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[8].Value = DBNull.Value;

                arrParams[9] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[9].Value = DBNull.Value;
            }
            arrParams[10] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[10].Value = this.CountryID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_Approve", arrParams);
		}
		//**************************************************************************************************************	
		public void ReApprove(string reapprovedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[21];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[1].Value = this.StartDate;

			arrParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[2].Value = this.EndDate;
	
			arrParams[3] = new SqlParameter("@CurrentDosage", SqlDbType.VarChar, 20);
			arrParams[3].Value = this.CurrentDosage;

			arrParams[4] = new SqlParameter("@ChangeDosageReason", SqlDbType.VarChar, 500);
			arrParams[4].Value = this.ChangeDosageReason;

			arrParams[5] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[5].Value = this.Notes;

			arrParams[6] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[6].Value = reapprovedby;

			arrParams[7] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 500);
			arrParams[7].Value = this.StatusReason;

			arrParams[8] = new SqlParameter("@RestartTreatment", SqlDbType.Bit);
			if(this.RestartTreatment)
			{
				arrParams[8].Value = 1;
			}
			else
			{
				arrParams[8].Value = 0;
			}

			arrParams[9] = new SqlParameter("@AutoApprove", SqlDbType.Bit);
			if(this.AutoApprove)
			{
				arrParams[9].Value = 1;
			}
			else
			{
				arrParams[9].Value = 0;
			}

			arrParams[10] = new SqlParameter("@RoleID", SqlDbType.Int);
			arrParams[10].Direction = ParameterDirection.Output;

			arrParams[11] = new SqlParameter("@NeedsReapproval", SqlDbType.Bit);
			arrParams[11].Direction = ParameterDirection.Output;

			arrParams[12] = new SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 50);
			arrParams[12].Value = this.ReceivedBy;

			arrParams[13] = new SqlParameter("@PatientConsent", SqlDbType.Bit);
			if(this.PatientConsent)
			{
				arrParams[13].Value = 1;
			}
			else
			{
				arrParams[13].Value = 0;
            }

            arrParams[14] = new SqlParameter("@NOA", SqlDbType.Bit);
            if (this.FlagNoa)
            {
                arrParams[14].Value = 1;
            }
            else
            {
                arrParams[14].Value = 0;
            }

            arrParams[15] = new SqlParameter("@PhysicianRequested", SqlDbType.Bit);
            arrParams[15].Value = this.PhysicianRequested;

            arrParams[16] = new SqlParameter("@AERelated", SqlDbType.Bit);
            arrParams[16].Value = this.AERelated;

            if (this.CountryID == 76)
            {
                this.SetStripNumbers();
                arrParams[17] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
                arrParams[17].Value = this.TabletStrength;

                arrParams[18] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[18].Value = this.Strips100mg;

                arrParams[19] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[19].Value = this.Strips400mg;
            }
            else
            {
                arrParams[17] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
                arrParams[17].Value = DBNull.Value;

                arrParams[18] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[18].Value = DBNull.Value;

                arrParams[19] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[19].Value = DBNull.Value;
            }

            arrParams[20] = new SqlParameter("@NotPickedUpContacted", SqlDbType.Bit);
            if (this.NotPickedUpContacted)
            {
                arrParams[20].Value = true;
            }
            else
            {
                arrParams[20].Value = false;
            }

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ReApprove", arrParams);

			if(((int)arrParams[10].Value != 1) && this.AutoApprove && ((bool)arrParams[11].Value))
			{
				GIPAP_Objects.Patient myPatient = new Patient(/*this.PatientID, "TMFUser"*/);
				GIPAP_Objects.Email myEmail = new Email();
				myEmail = myPatient.ReApprovalEmailPatient(this.PatientID);
				myEmail.Send(reapprovedby);
				if(myPatient.PhysicianCount > 0)
				{
					for(int ph=0; ph<myPatient.PhysicianCount; ph++)
					{
						myEmail = myPatient.ReApprovalEmailPhysician(ph);
						myEmail.Send(reapprovedby);
					}
				}
				if(myPatient.CPOCount > 0)
				{
					for(int cp=0; cp<myPatient.CPOCount; cp++)
					{
						myEmail = myPatient.ReApprovalEmailCPO(cp);
						myEmail.Send(reapprovedby);
					}
				}
			}
		}
		//**************************************************************************************************************	
		public void Extend(string extendedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[7];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@Reason", SqlDbType.VarChar, 500);
			arrParams[1].Value = this.StatusReason;

			arrParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[2].Value = this.EndDate;

			arrParams[3] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[3].Value = extendedby;

			arrParams[4] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[4].Value = this.RequestID;

            arrParams[5] = new SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 50);
            arrParams[5].Value = this.ReceivedBy;

            arrParams[6] = new SqlParameter("@PhysicianRequested", SqlDbType.Bit);
            arrParams[6].Value = this.PhysicianRequested;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_Extend", arrParams);
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
			
			arrParams[2] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
			arrParams[2].Value = deniedby;

			arrParams[3] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[3].Value = this.Notes;

			arrParams[4] = new SqlParameter("@RequestID", SqlDbType.Int);
            arrParams[4].Value = this.RequestID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_Deny", arrParams);
		}
		//**************************************************************************************************************	
		public void Close(string closedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[8];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 1000);
			arrParams[1].Value = this.StatusReason;
			
			arrParams[2] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
			arrParams[2].Value = closedby;

			arrParams[3] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[3].Value = this.Notes;

			arrParams[4] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[4].Value = this.RequestID;

            arrParams[5] = new SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 50);
            arrParams[5].Value = this.ReceivedBy;

            arrParams[6] = new SqlParameter("@PhysicianRequested", SqlDbType.Bit);
            arrParams[6].Value = this.PhysicianRequested;

            arrParams[7] = new SqlParameter("@AERelated", SqlDbType.Bit);
            arrParams[7].Value = this.AERelated;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_Close", arrParams);
		}
		//**************************************************************************************************************	
		public void ReActivate(string createdby, string reason)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[18];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[1].Value = this.StartDate;

			arrParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[2].Value = this.EndDate;
	
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

			arrParams[7] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 500);
			arrParams[7].Value = this.StatusReason;

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

			arrParams[10] = new SqlParameter("@ReactivateReason", SqlDbType.VarChar, 500);
			arrParams[10].Value = reason;
			
			arrParams[11] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[11].Value = createdby;

			arrParams[12] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[12].Value = this.RequestID;

            arrParams[13] = new SqlParameter("@NOA", SqlDbType.Bit);
            if (this.FlagNoa)
            {
                arrParams[13].Value = 1;
            }
            else
            {
                arrParams[13].Value = 0;
            }

            if (this.CountryID == 76)
            {
                this.SetStripNumbers();
                arrParams[14] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
                arrParams[14].Value = this.TabletStrength;

                arrParams[15] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[15].Value = this.Strips100mg;

                arrParams[16] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[16].Value = this.Strips400mg;
            }
            else
            {
                arrParams[14] = new SqlParameter("@TabletStrength", SqlDbType.VarChar, 25);
                arrParams[14].Value = DBNull.Value;

                arrParams[15] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[15].Value = DBNull.Value;

                arrParams[16] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[16].Value = DBNull.Value;
            }
            arrParams[17] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[17].Value = this.CountryID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ReActivate", arrParams);
		}
		//**************************************************************************************************************	
		public void ReAssess(string createdby, string cNote)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[5];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@Note", SqlDbType.Text);
			arrParams[1].Value = cNote;
			
			arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
			arrParams[2].Value = createdby;

			arrParams[3] = new SqlParameter("@RequestID", SqlDbType.Int);
			arrParams[3].Value = this.RequestID;

            arrParams[4] = new SqlParameter("@StatusReason", SqlDbType.VarChar,500);
            arrParams[4].Value = this.StatusReason;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ReAssess", arrParams);
		}
		//**************************************************************************************************************	
		public void ChangeDosage(string modifiedby, string newdose)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[14];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@CurrentDosage", SqlDbType.VarChar, 20);
            arrParams[1].Value = newdose;

			arrParams[2] = new SqlParameter("@ChangeDosageReason", SqlDbType.VarChar, 500);
			arrParams[2].Value = this.ChangeDosageReason; 

			arrParams[3] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
			arrParams[3].Value = modifiedby;

			arrParams[4] = new SqlParameter("@RequestID", SqlDbType.Int);
            arrParams[4].Value = this.RequestID;

            arrParams[5] = new SqlParameter("@ReceivedBy", SqlDbType.NVarChar, 50);
            arrParams[5].Value = this.ReceivedBy;

            arrParams[6] = new SqlParameter("@PhysicianRequested", SqlDbType.Bit);
            arrParams[6].Value = this.PhysicianRequested;

            arrParams[7] = new SqlParameter("@AERelated", SqlDbType.Bit);
            arrParams[7].Value = this.AERelated;

            if (this.CountryID == 76)
            {
                this.CalculateDoseChangeStrips(this.CurrentDosage, newdose);

                arrParams[8] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[8].Value = this.Strips100mg;

                arrParams[9] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[9].Value = this.Strips400mg;

                arrParams[10] = new SqlParameter("@Return400mg", SqlDbType.Int);
                if (this.Return400mg == 0)
                {
                    arrParams[10].Value = DBNull.Value;
                }
                else
                {
                    arrParams[10].Value = this.Return400mg;
                }

                arrParams[11] = new SqlParameter("@NewOrderNumber", SqlDbType.Bit);
                arrParams[11].Value = this.mNewOrderNumber;

                arrParams[12] = new SqlParameter("@Truncate100mg", SqlDbType.Int);
                arrParams[12].Value = this.Truncate100mg;

                arrParams[13] = new SqlParameter("@Truncate400mg", SqlDbType.Int);
                arrParams[13].Value = this.Truncate400mg;
            }
            else
            {

                arrParams[8] = new SqlParameter("@Strips100mg", SqlDbType.Int);
                arrParams[8].Value = DBNull.Value;

                arrParams[9] = new SqlParameter("@Strips400mg", SqlDbType.Int);
                arrParams[9].Value = DBNull.Value;

                arrParams[10] = new SqlParameter("@Return400mg", SqlDbType.Int);
                arrParams[10].Value = DBNull.Value;

                arrParams[11] = new SqlParameter("@NewOrderNumber", SqlDbType.Bit);
                arrParams[11].Value = DBNull.Value;

                arrParams[12] = new SqlParameter("@Truncate100mg", SqlDbType.Int);
                arrParams[12].Value = DBNull.Value;

                arrParams[13] = new SqlParameter("@Truncate400mg", SqlDbType.Int);
                arrParams[13].Value = DBNull.Value;
            }

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ChangeDosage", arrParams);
		}
		//**************************************************************************************************************	
		public void UpdateStatusReason(string modifiedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[4];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@StatusReason", SqlDbType.VarChar, 500);
			arrParams[1].Value = this.StatusReason;

			arrParams[2] = new SqlParameter("@GIPAPStatus", SqlDbType.VarChar, 50);
			arrParams[2].Value = this.GipapStatus;

			arrParams[3] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
			arrParams[3].Value = modifiedby;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateStatusReason", arrParams);
		}
		//**************************************************************************************************************	
		public void IgnoreReapprovalRequest(string modifiedby)
		{
			//Patient exists so update the information
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;
			
			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 20);
			arrParams[1].Value = modifiedby;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_IgnoreReapprovalRequest", arrParams);
		}
		//**************************************************************************************************************
		public string StatusHeader()
		{
			return this.mStatusHeader + "<br><a href=GIPAP.aspx?trgt=patientinfo&choice=" + this.PatientID.ToString() + ">View Patient Information</a>";
		}
		//**************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			this.PatientID = Convert.ToInt32(ds.Tables[0].Rows[0]["PatientID"]);
			this.PIN = ds.Tables[0].Rows[0]["PIN"].ToString();
            this.CurrentProgram = ds.Tables[0].Rows[0]["currentprogram"].ToString();
			this.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
			this.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            this.CountryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]);
            this.BirthDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["birthdate"]);
			this.GipapStatus = ds.Tables[0].Rows[0]["GipapStatus"].ToString();
			this.Diagnosis = ds.Tables[0].Rows[0]["Diagnosis"].ToString();
			this.PatientConsent = Convert.ToBoolean(ds.Tables[0].Rows[0]["PatientConsent"]);
            this.Treatment = ds.Tables[0].Rows[0]["treatment"].ToString();
			if(this.GipapStatus == "Pending" || this.GipapStatus == "Denied")
			{
                if (this.Treatment == "Tasigna")
                {
                    this.CurrentDosage = ds.Tables[10].Rows[0]["OriginalRequestedDosage"].ToString();
                }
                else
                {
                    this.CurrentDosage = ds.Tables[0].Rows[0]["OriginalRequestedDosage"].ToString();
                    this.TabletStrength = ds.Tables[0].Rows[0]["OriginalTabletStrength"].ToString();
                }
				this.StatusReason = (ds.Tables[0].Rows[0]["StatusReason"]).ToString();
			}
			else
			{
				if(ds.Tables[1].Rows.Count > 0)
				{
					this.StatusReason = (ds.Tables[1].Rows[0]["StatusReason"]).ToString();
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
                    if (this.Treatment == "Tasigna" && this.GipapStatus == "Closed")
                    {
                        this.CurrentDosage = ds.Tables[10].Rows[0]["OriginalRequestedDosage"].ToString();
                    }
                    else
                    {
                        this.CurrentDosage = ds.Tables[1].Rows[0]["CurrentDosage"].ToString();
                    }
					this.ChangeDosageReason = ds.Tables[1].Rows[0]["ChangeDosageReason"].ToString();
					//this.FinancialStatus = Convert.ToBoolean(ds.Tables[1].Rows[0]["FinancialStatus"]);
					this.RestartTreatment = Convert.ToBoolean(ds.Tables[1].Rows[0]["RestartTreatment"]);
					//this.HematologicalResponse = ds.Tables[1].Rows[0]["HematologicalResponse"].ToString();
					//this.CytogeneticResponse = ds.Tables[1].Rows[0]["CytogeneticResponse"].ToString();
					//this.TumorResponse = ds.Tables[1].Rows[0]["TumorResponse"].ToString();
					//this.GlivecSupply = Convert.ToInt32(ds.Tables[1].Rows[0]["GlivecSupply"].ToString());
					//this.RemainingSupply = ds.Tables[1].Rows[0]["RemainingSupply"].ToString();
                    this.Notes = ds.Tables[1].Rows[0]["Notes"].ToString();
                    try
                    {
                        this.PhysicianRequested = Convert.ToBoolean(ds.Tables[1].Rows[0]["PhysicianRequested"]);
                    }
                    catch { }
                    try
                    {
                        this.AERelated = Convert.ToBoolean(ds.Tables[1].Rows[0]["AERelated"]);
                    }
                    catch { }
					this.ReceivedBy = ds.Tables[1].Rows[0]["ReceivedBy"].ToString();
					this.ReminderEmail = Convert.ToBoolean(ds.Tables[1].Rows[0]["ReminderEmail"]);
					this.ReminderEmail90 = Convert.ToBoolean(ds.Tables[1].Rows[0]["ReminderEmail90"]);
					this.AutoApprove = Convert.ToBoolean(ds.Tables[1].Rows[0]["AutoApprove"]);
					this.EnableAutoApprove = Convert.ToBoolean(ds.Tables[0].Rows[0]["EnableAutoApprove"]);
                    this.TabletStrength = ds.Tables[1].Rows[0]["TabletStrength"].ToString();
                    try
                    {
                        this.Strips100mg = Convert.ToInt32(ds.Tables[1].Rows[0]["Strips100mg"]);
                    }
                    catch { }
                    try
                    {
                        this.Strips400mg = Convert.ToInt32(ds.Tables[1].Rows[0]["Strips400mg"]);
                    }
                    catch { }
				}

				if(ds.Tables[3].Rows.Count > 0)
				{
					if(ds.Tables[3].Rows[0]["gipapdetailid"].ToString() == ds.Tables[1].Rows[0]["gipapdetailid"].ToString())
					{
						this.EndDate = Convert.ToDateTime(ds.Tables[3].Rows[0]["EndDate"]);
					}
				}
				if(ds.Tables[4].Rows.Count > 0)
				{
					this.StatusReason = (ds.Tables[4].Rows[0]["StatusReason"]).ToString();
				}
				if(ds.Tables[5].Rows.Count > 0)
				{
					if(ds.Tables[5].Rows[0]["gipapdetailid"].ToString() == ds.Tables[1].Rows[0]["gipapdetailid"].ToString())
					{
						this.CurrentDosage = ds.Tables[5].Rows[0]["currentdosage"].ToString();
                        this.ChangeDosageReason = ds.Tables[5].Rows[0]["notes"].ToString();
					}
				}
			}
			if(ds.Tables[6].Rows.Count >0)
			{
                this.mStatusHeader = ds.Tables[6].Rows[0]["statusheader"].ToString();
			}
			if(this.Diagnosis == "CML")
			{
				this.CurrentCMLPhase = ds.Tables[2].Rows[0]["CurrentCMLPhase"].ToString();
				this.mStatusHeader = this.mStatusHeader.Replace("<br><b>Status:", "<br><b>Current CML Phase: </b>" + this.CurrentCMLPhase + "<br><b>Status:");
			}
            else if (this.Diagnosis == "GIST")
            {
                this.Adjuvant = Convert.ToBoolean(ds.Tables[11].Rows[0]["adjuvant"]);
            }
			if(ds.Tables[7].Rows.Count >0)
			{
                this.mStatusHeader += ds.Tables[7].Rows[0]["statusheader"].ToString();
                this.ActiveDetailPickedUp = Convert.ToBoolean(ds.Tables[7].Rows[0]["PickedUp"]); //this is from v_activeperiods, need the active one!!
                this.DetailCreateDate = Convert.ToDateTime(ds.Tables[7].Rows[0]["CreateDate"]);
			}
			this.PhysicianApproved = false;
			if(ds.Tables[8].Rows.Count > 0)
			{
				for(int i=0; i<ds.Tables[8].Rows.Count; i++)
				{
					if(Convert.ToBoolean(ds.Tables[8].Rows[i]["approved"]))
					{
						this.PhysicianApproved = true;
						break;
					}
				}
			}
            if (ds.Tables[9].Rows.Count > 0)
            {
                try
                {
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["noa"]) && Convert.ToInt32(ds.Tables[9].Rows[0]["donationlength"]) != 12 && Convert.ToInt32(ds.Tables[9].Rows[0]["donationlength"]) != 370)
                    {
                        this.FlagNoa = true;
                    }
                    else
                    {
                        this.FlagNoa = false;
                    }
                }
                catch
                {
                    this.FlagNoa = false;
                }
                //check if new noa fef needed
                this.NoaFEFNeeded = false;
                try
                {
                    this.mDonationLength = Convert.ToInt32(ds.Tables[9].Rows[0]["donationlength"]);
                }
                catch
                {
                    this.mDonationLength = -1;
                }
                if (this.GipapStatus == "Active" && this.Treatment == ds.Tables[9].Rows[0]["treatment"].ToString())
                {
                    //switching back to 1 month to allow requests at 1 month, goes into queues at 2 months still
                    if (Convert.ToBoolean(ds.Tables[9].Rows[0]["yearlyreassess"]) && Convert.ToDateTime(ds.Tables[9].Rows[0]["reassessdate"]).AddMonths(-1) <= DateTime.Today)
                    {
                        this.NoaFEFNeeded = true;
                    }
                    else if (ds.Tables[9].Rows[0]["noapin"].ToString() == "" || ds.Tables[9].Rows[0]["Recommendation"].ToString() != "Approve" || this.mDonationLength == -1)
                    {
                        this.NoaFEFNeeded = true;
                    }
                    else if (/*this.FlagNoa && full don patients need contact now*/ (!Convert.ToBoolean(ds.Tables[9].Rows[0]["mscontacted"]) || ds.Tables[9].Rows[0]["paymentoption"].ToString() == ""))
                    {
                        this.NoaFEFNeeded = true;
                    }
                }
            }
            else
            {
                this.FlagNoa = false;
                this.NoaFEFNeeded = false;
            }
            //check if noa phys
            this.NoaPhys = false;
            /*if (ds.Tables[10].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                {
                    if (Convert.ToBoolean(ds.Tables[10].Rows[i]["noa"]))
                    {
                        if (Convert.ToDateTime(ds.Tables[10].Rows[i]["noadate"]) < DateTime.Today)
                        {
                            this.NoaPhys = true;
                            break;
                        }
                    }
                }
            }*/
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
		public string Diagnosis
		{
			get{return mDiagnosis;}
			set{mDiagnosis = value;}
		}
        //**************************************************************************************************************
        public bool Adjuvant
        {
            get { return mAdjuvant; }
            set { mAdjuvant = value; }
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
		public bool AutoApprove
		{
			get{return mAutoApprove;}
			set{mAutoApprove = value;}
		}
		//**************************************************************************************************************
		public bool EnableAutoApprove
		{
			get{return mEnableAutoApprove;}
			set{mEnableAutoApprove = value;}
		}
		//**************************************************************************************************************
		public bool PhysicianApproved
		{
			get{return mPhysicianApproved;}
			set{mPhysicianApproved = value;}
		}
		//**************************************************************************************************************
		/*public string HematologicalResponse
		{
			get{return mHematologicalResponse;}
			set{mHematologicalResponse = value;}
		}
		//**************************************************************************************************************
		public string CytogeneticResponse
		{
			get{return mCytogeneticResponse;}
			set{mCytogeneticResponse = value;}
		}
		//**************************************************************************************************************
		public string TumorResponse
		{
			get{return mTumorResponse;}
			set{mTumorResponse = value;}
		}*/
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
			get{return mReceivedBy;}
			set{mReceivedBy = value;}
		}
		//**************************************************************************************************************
		/*public int GlivecSupply
		{
			get{return mGlivecSupply;}
			set{mGlivecSupply = value;}
		}
		//**************************************************************************************************************
		public string RemainingSupply
		{
			get{return mRemainingSupply;}
			set{mRemainingSupply = value;}
		}*/
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
		public bool PatientConsent
		{
			get{return mPatientConsent;}
			set{mPatientConsent = value;}
		}
        //**************************************************************************************************************
        public bool FlagNoa
        {
            get { return mFlagNoa; }
            set { mFlagNoa = value; }
        }
        //**************************************************************************************************************
        public bool NoaPhys
        {
            get { return mNoaPhys; }
            set { mNoaPhys = value; }
        }
        //**************************************************************************************************************
        public bool NoaFEFNeeded
        {
            get { return mNoaFEFNeeded; }
            set { mNoaFEFNeeded = value; }
        }
        //**************************************************************************************************************
        public string Treatment
        {
            get { return mTreatment; }
            set { mTreatment = value; }
        }
        //*********************************************************************************************************************
        public bool AERelated
        {
            get { return mAERelated; }
            set { mAERelated = value; }
        }
        //*********************************************************************************************************************
        public int Strips100mg
        {
            get { return mStrips100mg; }
            set { mStrips100mg = value; }
        }
        //*********************************************************************************************************************
        public int Strips400mg
        {
            get { return mStrips400mg; }
            set { mStrips400mg = value; }
        }
        //*********************************************************************************************************************
        public string TabletStrength
        {
            get { return mTabletStrength; }
            set { mTabletStrength = value; }
        }
        //*********************************************************************************************************************
        public int Return400mg
        {
            get { return mReturn400mg; }
            set { mReturn400mg = value; }
        }
        //**************************************************************************************************************
        public bool ActiveDetailPickedUp
        {
            get { return mActiveDetailPickedUp; }
            set { mActiveDetailPickedUp = value; }
        }
        //**************************************************************************************************************
        public bool NotPickedUpContacted
        {
            get { return mNotPickedUpContacted; }
            set { mNotPickedUpContacted = value; }
        }
        //**************************************************************************************************************
        public DateTime DetailCreateDate
        {
            get { return mDetailCreateDate; }
            set { mDetailCreateDate = value; }
        }
	}
}
