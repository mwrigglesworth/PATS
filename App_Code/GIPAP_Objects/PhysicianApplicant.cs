using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Physician.
	/// </summary>
	public class PhysicianApplicant
	{
		private int mPhysicianApplicantID;
		private char mSex;
		private string mFirstName;
		private string mLastName;
		private string mSpecialty;
		private string mStreet1;
		private string mStreet2;
		private string mCity;
		private string mStateProvince;
		private string mPostalCode;
		private int mCountryID;
		private string mPhone;
		private string mFax;
		private string mMobile;
		private string mEmail;
		private string mClinic;
		private string mNotes;

		//other tables
		private string mCountryName;
		private string CPOEmail;
		private string MaxEmail;

		private DataSet PhysicianDS;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAP2;PWD=secret;UID=sa;";

		//**************************************************************************************************************
		public PhysicianApplicant()
		{
			//Default Constructor
			this.Clear();
		}

		//**********************************************************************************************************************
		public PhysicianApplicant(int currid)
		{
			if(currid == 0)
			{
				return;
			}
			else
			{
				DataSet myData = new DataSet();
				SqlParameter CurrID = new SqlParameter("@PhysicianApplicantID", SqlDbType.Int);
				CurrID.Value = currid;
				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianApplicantProfile", CurrID);

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

		//**********************************************************************************************************************
		public void CreatePhysicianApplicant(string createdby)
		{
			SqlParameter[] arrParams = new SqlParameter[19];

			arrParams[0] = new SqlParameter("@PhysicianApplicantID", SqlDbType.Int);
			arrParams[0].Direction = ParameterDirection.Output;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
			arrParams[1].Value = this.FirstName;

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.LastName;

			arrParams[3] = new SqlParameter("@Specialty", SqlDbType.NVarChar, 20);
			arrParams[3].Value = this.Specialty;

			arrParams[4] = new SqlParameter("@Street1", SqlDbType.VarChar, 100);
			arrParams[4].Value = this.Street1.Trim();

			arrParams[5] = new SqlParameter("@Street2", SqlDbType.VarChar, 100);
			arrParams[5].Value = this.Street2.Trim();

			arrParams[6] = new SqlParameter("@City", SqlDbType.VarChar, 30);
			arrParams[6].Value = this.City.Trim();

			arrParams[7] = new SqlParameter("@StateProvince", SqlDbType.VarChar, 30);
			arrParams[7].Value = this.StateProvince.Trim();

			arrParams[8] = new SqlParameter("@PostalCode", SqlDbType.VarChar, 10);
			arrParams[8].Value = this.PostalCode.Trim();

			arrParams[9] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[9].Value = this.CountryID;

			arrParams[10] = new SqlParameter("@Phone", SqlDbType.NVarChar, 500);
			arrParams[10].Value = this.Phone;

			arrParams[11] = new SqlParameter("@Fax", SqlDbType.NVarChar, 30);
			arrParams[11].Value = this.Fax;

			arrParams[12] = new SqlParameter("@Email", SqlDbType.NVarChar, 200);
			arrParams[12].Value = this.Email;

			arrParams[13] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30);
			arrParams[13].Value = this.Mobile;

			arrParams[14] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[14].Value = this.Notes;
					
			arrParams[15] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[15].Value = createdby;

			arrParams[16] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[16].Value = this.Sex;

			arrParams[17] = new SqlParameter("@Clinic", SqlDbType.NVarChar, 50);
			arrParams[17].Value = this.Clinic;

			arrParams[18] = new SqlParameter("@Country", SqlDbType.NVarChar, 50);
			arrParams[18].Direction = ParameterDirection.Output;

			this.PhysicianDS = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CreatePhysicianApplicant", arrParams);

			//Return the newly created records ID
			this.PhysicianApplicantID = (int)arrParams[0].Value;
			this.mCountryName = arrParams[18].Value.ToString();
			if(this.PhysicianDS.Tables[0].Rows.Count > 0)
			{
				for(int i=0; i<this.PhysicianDS.Tables[0].Rows.Count; i++)
				{
					this.CPOEmail += this.PhysicianDS.Tables[0].Rows[i]["email"].ToString() + "; ";
				}
			}
			if(this.PhysicianDS.Tables[1].Rows.Count > 0)
			{
				for(int i=0; i<this.PhysicianDS.Tables[1].Rows.Count; i++)
				{
					this.MaxEmail += this.PhysicianDS.Tables[1].Rows[i]["email"].ToString() + "; ";
				}
			}

			GIPAP_Objects.Email myEmail = this.ApplicantEmail();
			if(myEmail.To != "")
			{
				try
				{
					myEmail.Send("PhysicianApplication");
				}
				catch{}
			}

			//If the returned value is -1 then the record already exists
		}
		//**********************************************************************************************************************
		public void Transfer(string transferedby)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PhysicianApplicantID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianApplicantID;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = transferedby;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_TransferPhysicianApplicant", arrParams);

		}
		//**********************************************************************************************************************
		public GIPAP_Objects.Email ApplicantEmail()
		{
			GIPAP_Objects.Email myEmail = new Email();
			myEmail.To = "gipap@themaxfoundation.org; " + this.CPOEmail;
			myEmail.CC = this.MaxEmail;
			myEmail.From = "gipap@themaxfoundation.org";
			myEmail.Subject = "GIPAP Physician Application for " + this.FirstName + " " + this.LastName;
			myEmail.Message = "Dear Novartis CPO:\n\n";
			myEmail.Message += "The following application was submitted for participation in GIPAP.  Please provide your input regarding qualification of this physician for GIPAP.\n\n";
			myEmail.Message += "First Name: " + this.FirstName + "\n";
			myEmail.Message += "Last Name: " + this.LastName + "\n";
			myEmail.Message += "Sex: " + this.Sex + "\n";
			myEmail.Message += "Specialty: " + this.Specialty + "\n";
			myEmail.Message += "Clinic: " + this.Clinic + "\n";
			myEmail.Message += "Street 1: " + this.Street1 + "\n";
			myEmail.Message += "Street 2: " + this.Street2 + "\n";
			myEmail.Message += "City: " + this.City + "\n";
			myEmail.Message += "State: " + this.StateProvince + "\n";
			myEmail.Message += "Postal Code: " + this.PostalCode + "\n";
			myEmail.Message += "Country: " + this.mCountryName + "\n";
			myEmail.Message += "Phone: " + this.Phone + "\n";
			myEmail.Message += "Mobile: " + this.Mobile + "\n";
			myEmail.Message += "Fax: " + this.Fax + "\n";
			myEmail.Message += "Email: " + this.Email + "\n";
			myEmail.Message += "Notes: " + this.Notes;

			return myEmail;
		}
		//**********************************************************************************************************************
		public string PhysicianInfo()
		{
			string physInfo = "<font size=4 color=steelblue><strong>" + this.FirstName + " " + this.LastName + "</font></strong><br><br>";
			physInfo += "<b>Sex: </b>";
			if(this.Sex == Convert.ToChar("M"))
			{
				physInfo += "Male";
			}
			else if(this.Sex == Convert.ToChar("F"))
			{
				physInfo += "Female";
			}
			physInfo += "<br><strong>Clinic: </strong>" + this.Clinic + "<br>";
			physInfo += "<strong>Specialty: </strong>" + this.Specialty + "<br>";
			physInfo += "<br><b>Address: </b>" + this.Street1 + "<br>" + this.Street2 + "<br><b>City: </b>" + this.City + "<br><b>State / Province: </b>" + this.StateProvince + " " + this.PostalCode + "<br><b>Country: </b>" + this.mCountryName + "<br>";
			physInfo += this.Email + "<br><strong>(tel)</strong>" + this.Phone + "<br><strong>(fax)</strong>" + this.Fax + "<br><strong>(mobile)</strong>" + this.Mobile;
			return physInfo;
		}
		//**********************************************************************************************************************
		private void Clear() //Sets the object to the default values
		{		
			this.PhysicianApplicantID = 0;
			this.FirstName = "";
			this.LastName = "";
			this.Specialty = "";
			this.Phone = "";
			this.Fax = "";
			this.Mobile = "";
			this.Email = "";
			this.Clinic = "";
			this.CountryID = 0;
			this.Notes = "";
			this.Sex = ' ';
		}

		//**********************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			this.PhysicianApplicantID = (int)(ds.Tables[0].Rows[0]["PhysicianApplicantID"]);
			this.FirstName = (ds.Tables[0].Rows[0]["FirstName"]).ToString();
			this.LastName = (ds.Tables[0].Rows[0]["LastName"]).ToString();
			if(ds.Tables[0].Rows[0]["Sex"] == DBNull.Value)
			{this.Sex = ' ';}
			else
			{this.Sex = Convert.ToChar(ds.Tables[0].Rows[0]["Sex"]);}
			this.Specialty = (ds.Tables[0].Rows[0]["Specialty"]).ToString();
			this.Street1 = (ds.Tables[0].Rows[0]["Street1"]).ToString();
			this.Street2 = (ds.Tables[0].Rows[0]["Street2"]).ToString();
			this.City = (ds.Tables[0].Rows[0]["City"]).ToString();
			this.StateProvince = (ds.Tables[0].Rows[0]["StateProvince"]).ToString();
			this.PostalCode = (ds.Tables[0].Rows[0]["PostalCode"]).ToString();
			if(ds.Tables[0].Rows[0]["Phone"] == DBNull.Value)
			{this.Phone = "";}
			else
			{this.Phone = (ds.Tables[0].Rows[0]["Phone"]).ToString();}

			if(ds.Tables[0].Rows[0]["Fax"] == DBNull.Value)
			{this.Fax = "";}
			else
			{this.Fax = (ds.Tables[0].Rows[0]["Fax"]).ToString();}

			if(ds.Tables[0].Rows[0]["Email"] == DBNull.Value)
			{this.Email = "";}
			else
			{this.Email = (ds.Tables[0].Rows[0]["Email"]).ToString();}

			if(ds.Tables[0].Rows[0]["Mobile"] == DBNull.Value)
			{this.Mobile = "";}
			else
			{this.Mobile = (ds.Tables[0].Rows[0]["Mobile"]).ToString();}

			this.Notes = ds.Tables[0].Rows[0]["Notes"].ToString();

			this.Clinic = ds.Tables[0].Rows[0]["Clinic"].ToString();

			this.CountryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]);
			
			this.mCountryName = ds.Tables[1].Rows[0]["CountryName"].ToString();
			
			this.PhysicianDS = ds;
		}

		//**********************************************************************************************************************
		public int PhysicianApplicantID
		{
			get{return mPhysicianApplicantID;}
			set{mPhysicianApplicantID = value;}
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
		public char Sex
		{
			get{return mSex;}
			set{mSex = value;}
		}
		//**********************************************************************************************************************
		public string Specialty
		{
			get{return mSpecialty;}
			set{mSpecialty = value;}
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
		public int CountryID
		{
			get{return mCountryID;}
			set{mCountryID = value;}
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
		public string Clinic
		{
			get{return mClinic;}
			set{mClinic = value;}
		}
		//**********************************************************************************************************************
		public string Notes
		{
			get{return mNotes;}
			set{mNotes = value;}
		}
	}
}
