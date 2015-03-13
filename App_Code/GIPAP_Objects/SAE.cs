using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for SAE.
	/// </summary>
	public class SAE
	{
		private int mSAEID;
		private int mPatientID;
		private string mEvent;
		private bool mLearnedFromPhysician;
		private bool mSharePermission;
		private int mGlivecRelated;
		private int mSerious;
		private DateTime mCreateDate;
		private string mCreatedBy;
        private DateTime mDateLearned;
        private bool mConsent;
        private int mEmailID;
        private int mPhyEmailID;
        private bool mCloseCase;
        private string mProgram;
        private string mProgramID; //Parameters needed by Novartis for AE purpose
		//Patient
		private string mPIN;
		private string mPatientName;
		private string mSex;
		private DateTime mBirthDate;
        private string mOriginalApprovedDosage;
        private string mOriginalTasignaDosage;
        private DateTime mIADate;
        private DateTime mIATasignaDate;
		private string mDiagnosis;
		private DateTime mStartDate;
		private string mCurrentDosage;
        private string mGIPAPStatus;
        private bool mNOA;
        private string mTreatment;
        //country
        private string mSAEEmail;
        private string CountryName;
		//datatable
		private DataTable EmailDT;
		private DataTable PhysicianDT;
        private DataTable MaxStationDT;
        private DataTable SaeDT;
        //sae count
        private int mSaeCount;
		
		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAP2;PWD=secret;UID=sa;";

		//**************************************************************************************************************
		public SAE()
		{
			//Default constructor
			this.Clear();
		}

		//**************************************************************************************************************
		public SAE(int currid, int patid)
		{
			DataSet myData;
			//Default constructor using the currid to populate the parameters
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@SAEID", SqlDbType.Int);
			arrParams[0].Value = currid;

			arrParams[1] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[1].Value = patid;

			myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetSAEProfile", arrParams);
			Inflate(myData);

			myData.Dispose();
		}

		//**************************************************************************************************************
		public void Create(string createdby)
		{
			//Add a new DSR to the database
			SqlParameter[] arrParams = new SqlParameter[13];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@Event", SqlDbType.Text);
			arrParams[1].Value = this.Event;

			arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
			arrParams[2].Value = createdby;

			arrParams[3] = new SqlParameter("@LearnedFromPhysician", SqlDbType.Bit);
			arrParams[3].Value = this.LearnedFromPhysician;

            /*arrParams[4] = new SqlParameter("@Serious", SqlDbType.Int);
            if(this.Serious == -1)
            {
                arrParams[4].Value = DBNull.Value;
            }
            else
            {
                arrParams[4].Value = this.Serious;
            }*/

            arrParams[4] = new SqlParameter("@SAEID", SqlDbType.Int);
			arrParams[4].Direction = ParameterDirection.Output;

            arrParams[5] = new SqlParameter("@DateLearned", SqlDbType.SmallDateTime);
            arrParams[5].Value = this.DateLearned;

            arrParams[6] = new SqlParameter("@Consent", SqlDbType.Bit);
            arrParams[6].Value = this.Consent;

            arrParams[7] = new SqlParameter("@EmailID", SqlDbType.Int);
            arrParams[7].Value = this.EmailID;

            arrParams[8] = new SqlParameter("@PhyEmailID", SqlDbType.Int);
            arrParams[8].Value = this.PhyEmailID;

            arrParams[9] = new SqlParameter("@CloseCase", SqlDbType.Bit);
            arrParams[9].Value = this.CloseCase;

            arrParams[10] = new SqlParameter("@Program", SqlDbType.NVarChar, 50);
            arrParams[11] = new SqlParameter("@ProgramID", SqlDbType.NVarChar, 50);
            if (this.CountryName == "Malaysia" && this.mNOA)
            {
                arrParams[10].Value = "NOA Malaysia";
                arrParams[11].Value = "POP00005341";
            }
            else if (this.CountryName == "Vietnam" && this.mNOA && this.mTreatment == "Tasigna")
            {
                arrParams[10].Value = "2nd Tasigna NOA co-pay program in Viet Nam";
                arrParams[11].Value = "POP20141124";
            }
            else
            {
                arrParams[10].Value = "GIPAP with TMF";
                arrParams[11].Value = "POP00003906";
            }

            arrParams[12] = new SqlParameter("@Treatment", SqlDbType.NVarChar, 50);
            arrParams[12].Value = this.Treatment;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateSAE", arrParams);

			//Return the newly created records ID
			this.SAEID = (int)arrParams[4].Value;

			/*if(this.LearnedFromPhysician)
			{
				if(this.GlivecRelated == 1 || this.GlivecRelated == 2)
				{
					if(this.Serious == 1)
					{
						this.SAEAlertNovartis().Send(createdby);
						this.SAEAlertPhysician().Send(createdby);
					}
					else if(this.Serious == 2)
					{
						this.SAEAlertPhysician().Send(createdby);
					}
				}
			}
			else
			{
				if(this.SharePermission)
				{
					if(this.Serious == 1)
					{
						this.SAEAlertNovartis().Send(createdby);
						this.SAEAlertPhysician().Send(createdby);
					}
					else if(this.Serious == 2)
					{
						this.SAEAlertPhysician().Send(createdby);
					}
				}
			}*/
		}
		//**************************************************************************************************************
		public string SaeConfirmation(GIPAP_Objects.User roleUser)
		{
			string saeConf = "<b>Thank you.</b><br>";
			saeConf += "Thank you for submitting the information about the adverse event. It has been received.<br><br>";
			saeConf += "To see the information about the adverse event you have entered, click on the link at the top of the patient’s screen 'Adverse Events'.<br><br>";
			saeConf += "Emails have been sent to the Novartis personnel, and you have been copied.  To see the emails, click on the 'Emails' link in the patient’s screen.";
			saeConf += "<br><br> <a href=GIPAP.aspx?trgt=patientinfo&choice=" + this.PatientID.ToString() + ">Patient Information</a>";
			
            return saeConf;
		}
		//**************************************************************************************************************
		public string PatientInfo()
		{
			return "<font color=steelblue size=4>" + this.PatientName + "<br>" + this.PIN + "</font>";
		}
        //**************************************************************************************************************
        public string SAEList()
        {
            StringBuilder sae = new StringBuilder();
            if (this.SAECount > 0)
            {
                for (int i = 0; i < this.SAECount; i++)
                {
                    sae.Append("<p>" + this.SaeDT.Rows[i]["event"].ToString() + "<br><b><i>" + this.SaeDT.Rows[i]["createdby"].ToString() + " " + this.SaeDT.Rows[i]["createdate"].ToString() + "</i></b></p>");
                }
            }
            return sae.ToString();
        }


		//**********************************************************************************************************************
		public GIPAP_Objects.Email SAEAlertNovartis()
		{
			GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

			myEmail.From = "gipap@themaxfoundation.org";
			//myEmail.To = "gipap@themaxfoundation.org; ";
            if (this.SAEEmail != "")
            {
                myEmail.To += this.SAEEmail;
            }
            myEmail.BCC = "GIPAPSD@themaxfoundation.org";
            /*else if(this.EmailDT.Rows.Count > 0)
            {
                for(int i=0; i<this.EmailDT.Rows.Count; i++)
                {
                    if(this.EmailDT.Rows[i]["email"].ToString() != "")
                    {
                        myEmail.To += this.EmailDT.Rows[i]["email"].ToString() + "; ";
                    }
                }
            }
            if(this.MaxStationDT.Rows.Count > 0)
            {
                for(int i=0; i<this.MaxStationDT.Rows.Count; i++)
                {
                    if(this.MaxStationDT.Rows[i]["email"].ToString() != "")
                    {
                        myEmail.CC += this.MaxStationDT.Rows[i]["email"].ToString() + "; ";
                    }
                }
            }*/

            myEmail.Subject = "ADVERSE EVENT REPORT FROM THE MAX FOUNDATION – " + this.PIN;
            myEmail.Message = "ADVERSE EVENT REPORT FROM THE MAX FOUNDATION\n\n";
			myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Date AE Reported to Max: " + this.DateLearned.Day.ToString() + " " + this.DateLearned.ToString("y");
            myEmail.Message += "\n\nPatient Identification Number: " + this.PIN.ToString();
			for(int i=0; i<this.PhysicianDT.Rows.Count; i++)
			{
				myEmail.Message += "\n\nPhysician Information:\n";
				myEmail.Message += this.PhysicianDT.Rows[i]["firstname"].ToString() + " " + this.PhysicianDT.Rows[i]["lastname"].ToString();
                if (this.PhysicianDT.Rows[i]["street1"].ToString() != "")
                {
                    myEmail.Message += "\n" + this.PhysicianDT.Rows[i]["street1"].ToString() + " " + this.PhysicianDT.Rows[i]["street2"].ToString();
                }
                if (this.PhysicianDT.Rows[i]["city"].ToString() != "" || this.PhysicianDT.Rows[i]["stateprovince"].ToString() != "" || this.PhysicianDT.Rows[i]["postalcode"].ToString() != "")
                {
                    myEmail.Message += "\n" + this.PhysicianDT.Rows[i]["city"].ToString() + " " + this.PhysicianDT.Rows[i]["stateprovince"].ToString() + " " + this.PhysicianDT.Rows[i]["postalcode"].ToString();
                }
				if(this.PhysicianDT.Rows[i]["phone"].ToString() != "")
				{
					myEmail.Message += "\n(tel) " + this.PhysicianDT.Rows[i]["phone"].ToString();
				}
				if(this.PhysicianDT.Rows[i]["fax"].ToString() != "")
				{
					myEmail.Message += "\n(fax) " + this.PhysicianDT.Rows[i]["fax"].ToString();
				}
				if(this.PhysicianDT.Rows[i]["email"].ToString() != "")
				{
					myEmail.Message += "\n(email) " + this.PhysicianDT.Rows[i]["email"].ToString();
				}
			}
			myEmail.Message += "\n\nDear Novartis Personnel,";
            myEmail.Message += "\n\nWe would like to notify you that an Adverse Event has been reported for Patient " + this.PIN;
            if (this.LearnedFromPhysician)
			{
                myEmail.Message += "\n\nReporter: Physician";
			}
			else
			{
                myEmail.Message += "\n\nReporter: Patient/caregiver";
			}
            myEmail.Message += "\n\nBelow is a description of the event:\n\n" + this.Event.Replace("<br>", "\n");
			myEmail.Message += "\n\nPATIENT DETAILS:\n\n";
			myEmail.Message += "Birth Date: " + this.mBirthDate.Day.ToString() + " " + this.mBirthDate.ToString("y");
            myEmail.Message += "\nCountry: " + this.CountryName;
            myEmail.Message += "\nGender: " + this.mSex;
            myEmail.Message += "\nIndication: " + this.mDiagnosis;
            myEmail.Message += "\nTreatment: " + this.mTreatment;
			myEmail.Message += "\nCurrent Dosage: " + this.mCurrentDosage;
			myEmail.Message += "\nDate of recent approval period: " + this.mStartDate.Day.ToString() + " " + this.mStartDate.ToString("y");
            
            if (this.mTreatment.Equals("Tasigna", StringComparison.CurrentCultureIgnoreCase))
                myEmail.Message += "\nOriginal Dosage: " + this.mOriginalTasignaDosage;
            else
                myEmail.Message += "\nOriginal Dosage: " + this.mOriginalApprovedDosage;
            
			if (this.mTreatment.Equals("Tasigna", StringComparison.CurrentCultureIgnoreCase))
                myEmail.Message += "\nOriginal Approval Date: " + this.mIATasignaDate.Day.ToString() + " " + this.mIATasignaDate.ToString("y");
            else
                myEmail.Message += "\nOriginal Approval Date: " + this.mIADate.Day.ToString() + " " + this.mIADate.ToString("y");
            
            //Program and Program ID as per new AE stuff - Sept 2014
            myEmail.Message += "\nProgram: " + this.mProgram;
            myEmail.Message += "\nProgramID: " + this.mProgramID;

            myEmail.Message += "\n\nPhysicians participating in patient support programs have been advised that the reporting of AE's is a regulatory requirement.  Physicians are informed that they can fax the completed form to either the local Novartis Office or where there is no local Novartis affiliate, send the completed form by email to the Novartis Drug Safety Representative at processing.hyderabad@novartis.com.";
			myEmail.Message += "\n\nIf you have questions or concerns, please do not hesitate to communicate with us at Gipap@themaxfoundation.org.";
			myEmail.Message += "\n\nRegards,\n\nThe Max Foundation";
			myEmail.PatientID = this.PatientID;
			myEmail.SAEID = this.SAEID;
			myEmail.MailType = "SAE";

			return myEmail;
		}


		//**********************************************************************************************************************
		public GIPAP_Objects.Email SAEAlertPhysician()
		{
			GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();

			myEmail.From = "gipap@themaxfoundation.org";
			//myEmail.To = "gipap@themaxfoundation.org; ";
            myEmail.To = "";
            //myEmail.BCC = "GIPAPSD@themaxfoundation.org";
			if(this.PhysicianDT.Rows.Count > 0)
			{
				for(int i=0; i<this.PhysicianDT.Rows.Count; i++)
				{
					if(this.PhysicianDT.Rows[i]["email"].ToString() != "")
					{
						myEmail.To += this.PhysicianDT.Rows[i]["email"].ToString() + "; ";
					}
				}
			}
			/*if(this.MaxStationDT.Rows.Count > 0)
			{
				for(int i=0; i<this.MaxStationDT.Rows.Count; i++)
				{
					if(this.MaxStationDT.Rows[i]["email"].ToString() != "")
					{
						myEmail.CC += this.MaxStationDT.Rows[i]["email"].ToString() + "; ";
					}
				}
			}*/
            myEmail.Subject = "ADVERSE EVENT REPORT FROM THE MAX FOUNDATION – " + this.PIN;
            myEmail.Message = "ADVERSE EVENT REPORT FROM THE MAX FOUNDATION (Physician)\n\n";
			myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
			/*for(int i=0; i<this.PhysicianDT.Rows.Count; i++)
			{
				myEmail.Message += "\n\nPhysician Information:\n";
				myEmail.Message += this.PhysicianDT.Rows[i]["firstname"].ToString() + " " + this.PhysicianDT.Rows[i]["lastname"].ToString();
				if(this.PhysicianDT.Rows[i]["phone"].ToString() != "")
				{
					myEmail.Message += "\n(tel) " + this.PhysicianDT.Rows[i]["phone"].ToString();
				}
				if(this.PhysicianDT.Rows[i]["fax"].ToString() != "")
				{
					myEmail.Message += "\n(fax) " + this.PhysicianDT.Rows[i]["fax"].ToString();
				}
				if(this.PhysicianDT.Rows[i]["email"].ToString() != "")
				{
					myEmail.Message += "\n(email) " + this.PhysicianDT.Rows[i]["email"].ToString();
				}
			}*/
			myEmail.Message += "\n\nDear GIPAP Physician,\n\n";
            myEmail.Message += "This is to inform you that an AE report was sent to Novartis for a patient under your care.\n\n";
            myEmail.Message += "Patient PIN: " + this.PIN.ToString();
            myEmail.Message += "\nPatient Name: " + this.PatientName;
            myEmail.Message += "\n\nAdverse Event Reported: " + this.Event.Replace("<br>", "\n");
            myEmail.Message += "\n\nDate Reported: " + this.DateLearned.Day.ToString() + " " + this.DateLearned.ToString("y");
            if (this.LearnedFromPhysician)
            {
                myEmail.Message += "\nInformation submitted to MAX by: Physician";
            }
            else
            {
                myEmail.Message += "\nInformation submitted to MAX by: Patient/caregiver";
            }
            myEmail.Message += "\n\nThe reporting of Adverse Events by The MAX Foundation is a regulatory requirement.  Novartis Drug Safety & Epidemiology Pharmacovigilance Operations desk will follow up with you regarding this report. \n\n";
            myEmail.Message += "For your convenience, you can find the list of AE reported by MAX to Novartis for patients under you care in your home page of the Patient Access Tracking System (PATS): From your homepage, click on ‘MAX reported AE list’.\n\n";
            myEmail.Message += "If you have questions or concerns, please do not hesitate to communicate with us at Gipap@themaxfoundation.org.";
			myEmail.Message += "\n\nRegards,\n\nThe Max Foundation";
			myEmail.PatientID = this.PatientID;
			myEmail.SAEID = this.SAEID;
			myEmail.MailType = "SAE";

			return myEmail;
		}


		//**************************************************************************************************************
		public void Clear()
		{
			this.SAEID = 0;
			this.PatientID = 0;
			this.Event = "";
			this.GlivecRelated = 2;
			this.Serious = 2;
		}

		//**************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			//Populates the objects parameters with the data returned from the database
			if(ds.Tables[0].Rows.Count > 0)
			{
				this.SAEID = Convert.ToInt32(ds.Tables[0].Rows[0]["SAEID"]);
				this.PatientID = Convert.ToInt32(ds.Tables[0].Rows[0]["PatientID"]);
                try
                {
                    this.EmailID = Convert.ToInt32(ds.Tables[0].Rows[0]["EmailID"]);
                }
                catch { }
                this.Event = ds.Tables[0].Rows[0]["Event"].ToString();
				this.LearnedFromPhysician = Convert.ToBoolean(ds.Tables[0].Rows[0]["learnedfromphysician"]);
				this.SharePermission = Convert.ToBoolean(ds.Tables[0].Rows[0]["sharepermission"]);
				try
				{
					this.GlivecRelated = Convert.ToInt32(ds.Tables[0].Rows[0]["GlivecRelated"]);
				}
				catch{}
				try
				{
					this.Serious = Convert.ToInt32(ds.Tables[0].Rows[0]["Serious"]);
				}
                catch { }
                this.Consent = Convert.ToBoolean(ds.Tables[0].Rows[0]["consent"]);
                this.DateLearned = Convert.ToDateTime(ds.Tables[0].Rows[0]["DateLearned"]);
                this.Program = ds.Tables[0].Rows[0]["Program"].ToString();
                this.ProgramID = ds.Tables[0].Rows[0]["ProgramID"].ToString();
				this.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateDate"]);
				this.CreatedBy = ds.Tables[0].Rows[0]["createdby"].ToString();
			}
			else
			{
				this.Clear();
			}
			//patient
			this.PatientID = (int)(ds.Tables[1].Rows[0]["PatientID"]);
            this.PIN = ds.Tables[1].Rows[0]["PIN"].ToString();
            this.GIPAPStatus = ds.Tables[1].Rows[0]["gipapstatus"].ToString();
            this.SAEEmail = ds.Tables[1].Rows[0]["saeemail"].ToString().Trim();
            this.CountryName = ds.Tables[1].Rows[0]["countryname"].ToString();
            this.PatientName = ds.Tables[1].Rows[0]["PatientName"].ToString();
			this.mSex = ds.Tables[1].Rows[0]["Sex"].ToString();
			this.mBirthDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["birthdate"].ToString());
			this.mOriginalApprovedDosage = ds.Tables[1].Rows[0]["originalapproveddosage"].ToString();
			this.mIADate = Convert.ToDateTime(ds.Tables[1].Rows[0]["iadate"].ToString());
			this.mDiagnosis = ds.Tables[1].Rows[0]["diagnosis"].ToString();
			this.mStartDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["startdate"].ToString());
            this.mCurrentDosage = ds.Tables[1].Rows[0]["currentdosage"].ToString();
            this.mTreatment = ds.Tables[1].Rows[0]["treatment"].ToString();
            this.mNOA = Convert.ToBoolean(ds.Tables[1].Rows[0]["noa"].ToString());
            if(this.CountryName == "Malaysia" && this.mNOA){
               this.Program = "NOA Malaysia";
                    this.ProgramID = "POP00005341";
            }
            else if (this.CountryName == "Vietnam" && this.mNOA && this.mTreatment == "Tasigna")
            {
                this.Program = "2nd Tasigna NOA co-pay program in Viet Nam";
                this.ProgramID = "POP20141124";
            }
            else{
                this.Program = "GIPAP with TMF";
                    this.ProgramID = "POP00003906";
            }
            //If the patient is on Tasigna, we need the original Tasigna Dosage and ApprovedDate
            this.mOriginalTasignaDosage = ds.Tables[1].Rows[0]["originaltasignadosage"].ToString();
            if (ds.Tables[1].Rows[0]["iatasignadate"] != null)
            {
                if (ds.Tables[1].Rows[0]["iatasignadate"].ToString() != "")
                    this.mIATasignaDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["iatasignadate"].ToString());
            }

			this.EmailDT = ds.Tables[2];
			this.PhysicianDT = ds.Tables[3];
			this.MaxStationDT = ds.Tables[4];

            //dosechange
            if (ds.Tables[5].Rows.Count > 0)
            {
                this.mCurrentDosage = ds.Tables[5].Rows[0]["currentdosage"].ToString();
            }
            //sae count
            this.SaeDT = ds.Tables[6];
            this.SAECount = ds.Tables[6].Rows.Count;
		}

		//**********************************************************************************************************************
		public int SAEID
		{
			get{return mSAEID;}
			set{mSAEID = value;}
		}
		//**********************************************************************************************************************
		public int PatientID
		{
			get{return mPatientID;}
			set{mPatientID = value;}
		}
        //**********************************************************************************************************************
        public int EmailID
        {
            get { return mEmailID; }
            set { mEmailID = value; }
        }
        //**********************************************************************************************************************
        public int PhyEmailID
        {
            get { return mPhyEmailID; }
            set { mPhyEmailID = value; }
        }
        //**********************************************************************************************************************
        public bool CloseCase
        {
            get { return mCloseCase; }
            set { mCloseCase = value; }
        }
		//**********************************************************************************************************************
		public string Event
		{
			get{return mEvent;}
			set{mEvent = value;}
		}
        //**********************************************************************************************************************
        public string SAEEmail
        {
            get { return mSAEEmail; }
            set { mSAEEmail = value; }
        }
		//**********************************************************************************************************************
		public bool LearnedFromPhysician
		{
			get{return mLearnedFromPhysician;}
			set{mLearnedFromPhysician = value;}
		}
		//**********************************************************************************************************************
		public bool SharePermission
		{
			get{return mSharePermission;}
			set{mSharePermission = value;}
		}
        //**********************************************************************************************************************
        public bool Consent
        {
            get { return mConsent; }
            set { mConsent = value; }
        }
		//**********************************************************************************************************************
		public DateTime CreateDate
		{
			get{return mCreateDate;}
			set{mCreateDate = value;}
		}
        //**********************************************************************************************************************
        public DateTime DateLearned
        {
            get { return mDateLearned; }
            set { mDateLearned = value; }
        }

        //**********************************************************************************************************************
        public string  Program
        {
            get { return mProgram; }
            set { mProgram = value; }
        }

        //**********************************************************************************************************************
        public string ProgramID
        {
            get { return mProgramID; }
            set { mProgramID = value; }
        }

        //**********************************************************************************************************************
        public string Treatment
        {
            get { return mTreatment; }
            set { mTreatment = value; }
        }
        
		//**********************************************************************************************************************
		public int GlivecRelated
		{
			get{return mGlivecRelated;}
			set{mGlivecRelated = value;}
		}
		//**************************************************************************************************************
		public string CreatedBy
		{
			get{return mCreatedBy;}
			set{mCreatedBy = value;}
		}
		//**********************************************************************************************************************
		public int Serious
		{
			get{return mSerious;}
			set{mSerious = value;}
		}
		//**************************************************************************************************************
		public string PIN
		{
			get{return mPIN;}
			set{mPIN = value;}
		}
        //**************************************************************************************************************
        public string GIPAPStatus
        {
            get { return mGIPAPStatus; }
            set { mGIPAPStatus = value; }
        }
		//**************************************************************************************************************
		public string PatientName
		{
			get{return mPatientName;}
			set{mPatientName = value;}
		}
        //**********************************************************************************************************************
        public int SAECount
        {
            get { return mSaeCount; }
            set { mSaeCount = value; }
        }
	}
}
