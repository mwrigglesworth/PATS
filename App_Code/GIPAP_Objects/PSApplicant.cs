using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for PSApplicant.
	/// </summary>
	public class PSApplicant
	{
		private int mPatientID;
		private string mFirstName;
		private string mLastName;
		private DateTime mBirthDate;
		private string mStreet1;
		private string mStreet2;
		private string mCity;
		private string mStateProvince;
		private string mPostalCode;
		private int mCountryID;
		private string mCountryName;
		private string mPhone;		
		private string mMobile;
		private string mFax;
		private string mEmail;
		private string mDiagnosis;
		private string mDiagnosisYear;
		private string mMedication;
		private string mNeeds;
		/*private bool mTreatmentReferal;
		private bool mTransportation;
		private bool mStemCellTransplant;
		private bool mEmotionalSupport;
		private bool mAccessMedication;
		private string mOtherAssistance;*/
		private string mGender;
		private string mFindOut;
		private DateTime mSubmitDate;
        private bool mPrivacyPractices;
		//Physician
		/*private string mPhysicianFirstName;
		private string mPhysicianLastName;
		private string mPhysicianPhone;
		private string mPhysicianFax;
		private string mPhysicianEmail;
		private string mClinicName;
		private bool mContactPhysician;*/
		//Contact
		private string mContactFirstName;
		private string mContactLastName;
		private string mContactPhone;
		private string mContactFax;
		private string mContactEmail;
		private string mRelationship;
		//existing
		public int PhysicianID;
		public int ClinicID;
		public int SocialWorkerID;

		string connString = ConfigurationSettings.AppSettings["connPS"];

		//**********************************************************************************************************************
		public PSApplicant()
		{
			this.Clear();
		}
		//*********************************************************************************************************************
		public void CreateApplicant()
		{
			SqlParameter[] arParams = new SqlParameter[26];

			arParams[0] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50); 
			arParams[0].Value = this.FirstName;
				
			arParams[1] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50); 
			arParams[1].Value = this.LastName;
		
			arParams[2] = new SqlParameter("@Gender", SqlDbType.NVarChar, 10); 
			arParams[2].Value = this.Gender;
				
			arParams[3] = new SqlParameter("@BirthDate", SqlDbType.DateTime);
			arParams[3].Value = this.BirthDate;

			arParams[4] = new SqlParameter("@Address1", SqlDbType.NVarChar, 200); 
			arParams[4].Value = this.Street1;

			arParams[5] = new SqlParameter("@Address2", SqlDbType.NVarChar, 200); 
			arParams[5].Value = this.Street2;

			arParams[6] = new SqlParameter("@City", SqlDbType.VarChar, 40); 
			arParams[6].Value = this.City;

			arParams[7] = new SqlParameter("@State", SqlDbType.NVarChar, 40); 
			arParams[7].Value = this.StateProvince;

			arParams[8] = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 50); 
			arParams[8].Value = this.PostalCode;

			arParams[9] = new SqlParameter("@CountryID", SqlDbType.Int); 
			arParams[9].Value = this.CountryID;
			
			arParams[10] = new SqlParameter("@Phone", SqlDbType.NVarChar, 100); 
			arParams[10].Value = this.Phone;

			arParams[11] = new SqlParameter("@Fax", SqlDbType.NVarChar, 50); 
			arParams[11].Value = this.Fax;

			arParams[12] = new SqlParameter("@Email", SqlDbType.NVarChar, 200); 
			arParams[12].Value = this.Email;

			arParams[13] = new SqlParameter("@ContactFirstName", SqlDbType.NVarChar, 50); 
			arParams[13].Value = this.ContactFirstName;

			arParams[14] = new SqlParameter("@ContactLastName", SqlDbType.NVarChar, 50); 
			arParams[14].Value = this.ContactLastName;

			arParams[15] = new SqlParameter("@ContactPhone", SqlDbType.NVarChar, 50); 
			arParams[15].Value = this.ContactPhone;
		
			arParams[16] = new SqlParameter("@ContactFax", SqlDbType.NVarChar, 50); 
			arParams[16].Value = this.ContactFax;
			
			arParams[17] = new SqlParameter("@ContactEmail", SqlDbType.NVarChar, 200); 
			arParams[17].Value = this.ContactEmail;

			arParams[18] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 100); 
			arParams[18].Value = this.Mobile;

			/*arParams[18] = new SqlParameter("@PhysicianFirstName", SqlDbType.NVarChar, 50); 
			arParams[18].Value = this.PhysicianFirstName;

			arParams[19] = new SqlParameter("@PhysicianLastName", SqlDbType.NVarChar, 50); 
			arParams[19].Value = this.PhysicianLastName;

			arParams[20] = new SqlParameter("@ClinicName", SqlDbType.VarChar, 200); 
			arParams[20].Value = this.ClinicName;

			arParams[21] = new SqlParameter("@PhysicianPhone", SqlDbType.NVarChar, 50); 
			arParams[21].Value = this.PhysicianPhone;

			arParams[22] = new SqlParameter("@PhysicianFax", SqlDbType.NVarChar, 50); 
			arParams[22].Value = this.PhysicianFax;

			arParams[23] = new SqlParameter("@PhysicianEmail", SqlDbType.NVarChar, 200); 
			arParams[23].Value = this.PhysicianEmail;

			arParams[24] = new SqlParameter("@ContactPhysician", SqlDbType.Bit); 
			if(this.ContactPhysician)
			{
				arParams[24].Value = 1;
			}
			else
			{
				arParams[24].Value = 0;
			}*/

			arParams[19] = new SqlParameter("@Diagnosis", SqlDbType.NVarChar, 100); 
			arParams[19].Value = this.Diagnosis;

			arParams[20] = new SqlParameter("@DiagnosisYear", SqlDbType.NVarChar, 50);
			arParams[20].Value = this.DiagnosisYear;

			arParams[21] = new SqlParameter("@Needs", SqlDbType.Text); 
			arParams[21].Value = this.Needs;

			/*arParams[28] = new SqlParameter("@TreatmentReferal", SqlDbType.Bit); 
			if(this.TreatmentReferal)
			{
				arParams[28].Value = 1;
			}
			else
			{
				arParams[28].Value = 0;
			}

			arParams[29] = new SqlParameter("@Transportation", SqlDbType.Bit); 
			if(this.Transportation)
			{
				arParams[29].Value = 1;
			}
			else
			{
				arParams[29].Value = 0;
			}*/

			arParams[22] = new SqlParameter("@FindOut", SqlDbType.NVarChar, 50); 
			arParams[22].Value = this.FindOut;
			
			/*arParams[31] = new SqlParameter("@StemCellTransplant", SqlDbType.Bit); 
			if(this.StemCellTransplant)
			{
				arParams[31].Value = 1;
			}
			else
			{
				arParams[31].Value = 0;
			}
			arParams[32] = new SqlParameter("@EmotionalSupport", SqlDbType.Bit); 
			if(this.EmotionalSupport)
			{
				arParams[32].Value = 1;
			}
			else
			{
				arParams[32].Value = 0;
			}
			arParams[33] = new SqlParameter("@OtherAssistance", SqlDbType.NVarChar, 1000); 
			arParams[33].Value = this.OtherAssistance;*/

			arParams[23] = new SqlParameter("@Relationship", SqlDbType.NVarChar, 50); 
			arParams[23].Value = this.Relationship;

			arParams[24] = new SqlParameter("@Medication", SqlDbType.NVarChar, 50); 
			arParams[24].Value = this.Medication;

            arParams[25] = new SqlParameter("@PrivacyPractices", SqlDbType.Bit);
            if (this.PrivacyPractices)
                arParams[25].Value = true;
            else
                arParams[25].Value = false;

			// Call ExecuteNonQuery static method of SqlHelper class
			// We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePatientForm", arParams);

			this.EmailApp();
		}
		//**********************************************************************************************************************
		private void EmailApp()
		{
			GIPAP_Objects.Email myEmail = new Email();

			myEmail.To = "help@themaxfoundation.org";
			if(this.CountryID == 5 || this.CountryID == 174 || this.CountryID == 128 || this.CountryID == 21 || this.CountryID == 35)
			{
				myEmail.To += "; ines.garcia.gonzalez@themaxfoundation.org";
			}
			myEmail.CC = "";
			myEmail.From = "gipap@themaxfoundation.org";
			myEmail.Subject = "Help Application - " + this.LastName + ", " + this.FirstName;
			myEmail.Message = "Help Application\n\n";
			myEmail.Message += "First Name: " + this.FirstName + "\n";
			myEmail.Message += "Last Name: " + this.LastName + "\n";
			myEmail.Message += "Gender: " + this.Gender + "\n";
			myEmail.Message += "Birthdate: " + this.BirthDate.Day.ToString() + " " + this.BirthDate.ToString("y") + "\n";
			myEmail.Message += "Address: " + this.Street1 + " " + this.Street2 + "\n";
			myEmail.Message += "City: " + this.City + "\n";
			myEmail.Message += "State: " + this.StateProvince + "\n";
			myEmail.Message += "Country: " + this.CountryName + "\n";
			myEmail.Message += "Phone: " + this.Phone+ "\n";
			myEmail.Message += "Mobile: " + this.Mobile + "\n";
			myEmail.Message += "Fax: " + this.Fax + "\n";
			myEmail.Message += "Email: " + this.Email;
			if(this.ContactFirstName != "")
			{
				myEmail.Message += "\n\nContact Information\n\n";
				myEmail.Message += "First Name: " + this.ContactFirstName + "\n";
				myEmail.Message += "Last Name: " + this.ContactLastName + "\n";
				myEmail.Message += "Phone: " + this.ContactPhone+ "\n";
				myEmail.Message += "Fax: " + this.ContactFax + "\n";
				myEmail.Message += "Email: " + this.ContactEmail;
			}
			myEmail.Message += "\n\nDiagnosis Information\n\n";
			myEmail.Message += "Diagnosis: " + this.Diagnosis + "\n";
			myEmail.Message += "Diagnosis Year: " + this.DiagnosisYear + "\n";
			myEmail.Message += "Medication: " + this.Medication;

			/*myEmail.Message += "\n\nPhysician Information\n\n";
			myEmail.Message += "First Name: " + this.PhysicianFirstName + "\n";
			myEmail.Message += "Last Name: " + this.PhysicianLastName + "\n";
			myEmail.Message += "Clinic: " + this.ClinicName + "\n";
			myEmail.Message += "Phone: " + this.PhysicianPhone+ "\n";
			myEmail.Message += "Fax: " + this.PhysicianFax + "\n";
			myEmail.Message += "Email: " + this.PhysicianEmail + "\n";
			myEmail.Message += "May we contact physician?: ";
			if(this.ContactPhysician)
			{
				myEmail.Message += "Yes";
			}
			else
			{
				myEmail.Message += "No";
			}*/
			myEmail.Message += "\n\nPatient Needs: " + this.Needs + "\n";
			/*if(this.TreatmentReferal)
			{
				myEmail.Message += "Treatment Referal\n";
			}
			if(this.Transportation)
			{
				myEmail.Message += "Transportation\n";
			}
			if(this.StemCellTransplant)
			{
				myEmail.Message += "Stem Cell Transplant\n";
			}
			if(this.EmotionalSupport)
			{
				myEmail.Message += "Emotional Support\n";
			}
			if(this.AccessMedication)
			{
				myEmail.Message += "Access To Medicatin\n";
			}
			myEmail.Message += "Other: " + this.OtherAssistance + "\n\n";
			myEmail.Message += "Additional Comments: " + this.Comments + "\n";*/
			myEmail.Message += "How did you hear about us?: " + this.FindOut;
            myEmail.Message += "\n\nHave you read/understand Privacy Practices?: ";
            if (this.PrivacyPractices)
            {
                myEmail.Message += "Yes";
            }
            else
            {
                myEmail.Message += "No";
            }

			myEmail.Send("System");
		}

		//**********************************************************************************************************************
		private void Clear()
		{
			this.PatientID = 0;
			this.FirstName = "";
			this.LastName = "";
			this.Street1 = "";
			this.Street2 = "";
			this.City = "";
			this.StateProvince = "";
			this.PostalCode = "";
			this.CountryID = 0;
			this.CountryName = "";
			this.Phone = "";
			this.Fax = "";
			this.Email = "";
			this.Diagnosis = "";
			this.Medication = "";
			/*this.Comments = "";
			this.TreatmentReferal = false;
			this.Transportation = false;
			this.StemCellTransplant = false;
			this.EmotionalSupport = false;
			this.AccessMedication = false;
			this.OtherAssistance = "";*/
			this.Gender = "";
			this.FindOut = "";
			//Physician
			/*this.PhysicianFirstName = "";
			this.PhysicianLastName = "";
			this.PhysicianPhone = "";
			this.PhysicianFax = "";
			this.PhysicianEmail = "";
			this.ClinicName = "";
			this.ContactPhysician = false;*/
			//Contact
			this.ContactFirstName = "";
			this.ContactLastName = "";
			this.ContactPhone = "";
			this.ContactFax = "";
			this.ContactEmail = "";
			this.Relationship = "";
		}
		//**********************************************************************************************************************
		public int PatientID
		{
			get{return mPatientID;}
			set{mPatientID = value;}
		}
		//**********************************************************************************************************************
		public string FirstName
		{
			get{return mFirstName;}
			set{mFirstName = value;}
		}
		//**********************************************************************************************************************
		public string LastName
		{
			get{return mLastName;}
			set{mLastName = value;}
		}
		
		//**********************************************************************************************************************
		public DateTime BirthDate
		{
			get{return mBirthDate;}
			set{mBirthDate = value;}
		}

		//**********************************************************************************************************************
		public string Street1
		{
			get{return mStreet1;}
			set{mStreet1 = value;}
		}

		//**********************************************************************************************************************
		public string Street2
		{
			get{return mStreet2;}
			set{mStreet2 = value;}
		}

		//**********************************************************************************************************************
		public string City
		{
			get{return mCity;}
			set{mCity = value;}
		}

		//**********************************************************************************************************************
		public string StateProvince
		{
			get{return mStateProvince;}
			set{mStateProvince = value;}
		}

		//**********************************************************************************************************************
		public string PostalCode
		{
			get{return mPostalCode;}
			set{mPostalCode = value;}
		}

		//**********************************************************************************************************************
		public int CountryID
		{
			get{return mCountryID;}
			set{mCountryID = value;}
		}
		//**********************************************************************************************************************
		public string CountryName
		{
			get{return mCountryName;}
			set{mCountryName = value;}
		}

		//**********************************************************************************************************************
		public string Phone
		{
			get{return mPhone;}
			set{mPhone = value;}
		}

		//**********************************************************************************************************************
		public string Mobile
		{
			get{return mMobile;}
			set{mMobile = value;}
		}

		//**********************************************************************************************************************
		public string Fax
		{
			get{return mFax;}
			set{mFax = value;}
		}

		//**********************************************************************************************************************
		public string Email
		{
			get{return mEmail;}
			set{mEmail = value;}
		}

		//**********************************************************************************************************************
		public string Diagnosis
		{
			get{return mDiagnosis;}
			set{mDiagnosis = value;}
		}

		//**********************************************************************************************************************
		public string DiagnosisYear
		{
			get{return mDiagnosisYear;}
			set{mDiagnosisYear = value;}
		}

		//**********************************************************************************************************************
		public string Medication
		{
			get{return mMedication;}
			set{mMedication = value;}
		}

		//**********************************************************************************************************************
		public string Needs
		{
			get{return mNeeds;}
			set{mNeeds = value;}
		}

		//**********************************************************************************************************************
		/*public bool TreatmentReferal
		{
			get{return mTreatmentReferal;}
			set{mTreatmentReferal = value;}
		}

		//**********************************************************************************************************************
		public bool Transportation
		{
			get{return mTransportation;}
			set{mTransportation = value;}
		}

		//**********************************************************************************************************************
		public bool StemCellTransplant
		{
			get{return mStemCellTransplant;}
			set{mStemCellTransplant = value;}
		}

		//**********************************************************************************************************************
		public bool EmotionalSupport
		{
			get{return mEmotionalSupport;}
			set{mEmotionalSupport = value;}
		}

		//**********************************************************************************************************************
		public bool AccessMedication
		{
			get{return mAccessMedication;}
			set{mAccessMedication = value;}
		}

		//**********************************************************************************************************************
		public string OtherAssistance
		{
			get{return mOtherAssistance;}
			set{mOtherAssistance = value;}
		}*/

		//**********************************************************************************************************************
		public string FindOut
		{
			get{return mFindOut;}
			set{mFindOut = value;}
		}

		//**********************************************************************************************************************
		public DateTime SubmitDate
		{
			get{return mSubmitDate;}
			set{mSubmitDate = value;}
		}

		//**********************************************************************************************************************
		/*public string PhysicianFirstName
		{
			get{return mPhysicianFirstName;}
			set{mPhysicianFirstName = value;}
		}
		//**********************************************************************************************************************
		public string PhysicianLastName
		{
			get{return mPhysicianLastName;}
			set{mPhysicianLastName = value;}
		}
		//**********************************************************************************************************************
		public string PhysicianPhone
		{
			get{return mPhysicianPhone;}
			set{mPhysicianPhone = value;}
		}

		//**********************************************************************************************************************
		public string PhysicianFax
		{
			get{return mPhysicianFax;}
			set{mPhysicianFax = value;}
		}
		//**********************************************************************************************************************
		public string PhysicianEmail
		{
			get{return mPhysicianEmail;}
			set{mPhysicianEmail = value;}
		}
		//**********************************************************************************************************************
		public string ClinicName
		{
			get{return mClinicName;}
			set{mClinicName = value;}
		}

		//**********************************************************************************************************************
		public bool ContactPhysician
		{
			get{return mContactPhysician;}
			set{mContactPhysician = value;}
		}*/
		//**********************************************************************************************************************
		public string Gender
		{
			get{return mGender;}
			set{mGender = value;}
		}

		//**********************************************************************************************************************
		public string ContactFirstName
		{
			get{return mContactFirstName;}
			set{mContactFirstName = value;}
		}
		//**********************************************************************************************************************
		public string ContactLastName
		{
			get{return mContactLastName;}
			set{mContactLastName = value;}
		}
		//**********************************************************************************************************************
		public string ContactPhone
		{
			get{return mContactPhone;}
			set{mContactPhone = value;}
		}

		//**********************************************************************************************************************
		public string ContactFax
		{
			get{return mContactFax;}
			set{mContactFax = value;}
		}
		//**********************************************************************************************************************
		public string ContactEmail
		{
			get{return mContactEmail;}
			set{mContactEmail = value;}
		}
		//**********************************************************************************************************************
		public string Relationship
		{
			get{return mRelationship;}
			set{mRelationship = value;}
		}
        //**********************************************************************************************************************
        public bool PrivacyPractices
        {
            get { return mPrivacyPractices; }
            set { mPrivacyPractices = value; }
        }
	}
}
