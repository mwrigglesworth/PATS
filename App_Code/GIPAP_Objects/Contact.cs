using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Contact.
	/// </summary>
	public class Contact
	{
		private int mContactID;
		private string mFirstName;
		private string mLastName;
		private char mSex;
		private string mPersonType;
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
		private bool mApproved;
		private int mUserID;
		private string mNotes;

		//other tables
		private string mRelationship;
		private string mRelationshipDetails;
		private string mCountryName;
		private string mPatientName;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		string connPS = ConfigurationSettings.AppSettings["connPS"];
		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAP2;PWD=secret;UID=sa;";

		//**************************************************************************************************************
		public Contact()
		{
			//Default Constructor
			this.Clear();
		}

		//**********************************************************************************************************************
		public Contact(int currid, int patid)
		{
			DataSet myData = new DataSet();
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@ContactID", SqlDbType.Int);
			arrParams[0].Value = currid;

			arrParams[1] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[1].Value = patid;

			myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetContactProfile", arrParams);
			Inflate(myData);
			myData.Dispose();
		}

		//**********************************************************************************************************************
		public void Create(string createdby, int patid)
		{
			//Update the GCC information.
			SqlParameter[] arrParams = new SqlParameter[21];

			arrParams[0] = new SqlParameter("@ContactID", SqlDbType.Int);
			arrParams[0].Direction = ParameterDirection.Output;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
			arrParams[1].Value = this.FirstName.Trim();

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.LastName.Trim();

			arrParams[3] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[3].Value = this.Sex;

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

			arrParams[14] = new SqlParameter("@Approved", SqlDbType.Bit);
			arrParams[14].Value = this.Approved;

			arrParams[15] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[15].Value = this.Notes;
					
			arrParams[16] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[16].Value = createdby;

			arrParams[17] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[17].Value = patid;

			arrParams[18] = new SqlParameter("@Relationship", SqlDbType.NVarChar, 20);
			arrParams[18].Value = this.Relationship;

			arrParams[19] = new SqlParameter("@RelationshipDetails", SqlDbType.NVarChar, 200);
			arrParams[19].Value = this.RelationshipDetails;

			arrParams[20] = new SqlParameter("@PIN", SqlDbType.NVarChar, 50);
			arrParams[20].Direction = ParameterDirection.Output;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateContact", arrParams);

			//Return the newly created records ID
			this.ContactID = (int)arrParams[0].Value;

			//If the returned value is -1 then the record already exists
			if(this.ContactID == -1)
			{
				throw new ArgumentException("The Contact already exists.");
			}
			else
			{
				this.CreatePS(createdby, arrParams[20].Value.ToString());
			}
		}
		//**********************************************************************************************************************
		public void CreatePS(string createdby, string ppin)
		{
			SqlParameter[] arrParams = new SqlParameter[18];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.ContactID;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
			arrParams[1].Value = this.FirstName.Trim();

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.LastName.Trim();

			arrParams[3] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[3].Value = this.Sex;

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

			arrParams[16] = new SqlParameter("@PIN", SqlDbType.NVarChar, 50);
			arrParams[16].Value = ppin;

			arrParams[17] = new SqlParameter("@Relationship", SqlDbType.NVarChar, 20);
			arrParams[17].Value = this.Relationship;

			SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_CreateGIPAPContact", arrParams);

		}

		//**********************************************************************************************************************
		public void Update(string modifiedby, int patid)
		{
			//Update the GCC information.
			SqlParameter[] arrParams = new SqlParameter[21];

			arrParams[0] = new SqlParameter("@ContactID", SqlDbType.Int);
			arrParams[0].Value = this.ContactID;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
			arrParams[1].Value = this.FirstName.Trim();

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.LastName.Trim();

			arrParams[3] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[3].Value = this.Sex;

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

			arrParams[14] = new SqlParameter("@Approved", SqlDbType.Bit);
			arrParams[14].Value = this.Approved;

			arrParams[15] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[15].Value = this.Notes;
					
			arrParams[16] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[16].Value = modifiedby;

			arrParams[17] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[17].Value = patid;

			arrParams[18] = new SqlParameter("@Relationship", SqlDbType.NVarChar, 20);
			arrParams[18].Value = this.Relationship;

			arrParams[19] = new SqlParameter("@RelationshipDetails", SqlDbType.NVarChar, 200);
			arrParams[19].Value = this.RelationshipDetails;

			arrParams[20] = new SqlParameter("@PIN", SqlDbType.NVarChar, 50);
			arrParams[20].Direction = ParameterDirection.Output;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateContact", arrParams);
			
			this.UpdatePS(modifiedby, arrParams[20].Value.ToString());
		}
		//**********************************************************************************************************************
		private void UpdatePS(string modifiedby, string ppin)
		{
			//Update the GCC information.
			SqlParameter[] arrParams = new SqlParameter[20];

			arrParams[0] = new SqlParameter("@ContactID", SqlDbType.Int);
			arrParams[0].Value = this.ContactID;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
			arrParams[1].Value = this.FirstName.Trim();

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.LastName.Trim();

			arrParams[3] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[3].Value = this.Sex;

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

			arrParams[14] = new SqlParameter("@Approved", SqlDbType.Bit);
			arrParams[14].Value = this.Approved;

			arrParams[15] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[15].Value = this.Notes;
					
			arrParams[16] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[16].Value = modifiedby;

			arrParams[17] = new SqlParameter("@PIN", SqlDbType.NVarChar, 50);
			arrParams[17].Value = ppin;

			arrParams[18] = new SqlParameter("@Relationship", SqlDbType.NVarChar, 20);
			arrParams[18].Value = this.Relationship;

			arrParams[19] = new SqlParameter("@RelationshipDetails", SqlDbType.NVarChar, 200);
			arrParams[19].Value = this.RelationshipDetails;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_UpdateGIPAPContact", arrParams);
		}
		//**********************************************************************************************************************
		public string ContactInfo(string Urole)
		{
			string physInfo = "<h1><font color=steelblue>" + this.FirstName + " " + this.LastName + "</font></h1>";
			//physInfo += "<b>Patient Name: </b><font color=steelblue>" + this.PatientName + "</font>";
			if(Urole == "TMFUser")
			{
				physInfo += "<font class='lbl'>Sex: </font>";
				if(this.Sex == Convert.ToChar("M"))
				{
					physInfo += "Male";
				}
				else if(this.Sex == Convert.ToChar("F"))
				{
					physInfo += "Female";
				}
			}
			physInfo += "<br><font class='lbl'>Address: </font>" + this.Street1 + "<br>" + this.Street2 + "<br><font class='lbl'>City: </font>" + this.City + "<br><font class='lbl'>State / Province: </font>" + this.StateProvince + " " + this.PostalCode + "<br><font class='lbl'>Country: </font>" + this.mCountryName + "<br>";
			physInfo += "<font class='lbl'>Email: </font>" + this.Email + "<br><strong>(tel)</strong>" + this.Phone + "<br><strong>(fax)</strong>" + this.Fax + "<br><strong>(mobile)</strong>" + this.Mobile;
			physInfo += "<br><font class='lbl'>Relationship To Patient: </font>" + this.Relationship + "<br><font class='lbl'>Relationship Details: </font>" + this.RelationshipDetails;
			return physInfo;
		}
		//**********************************************************************************************************************
		private void Clear() //Sets the object to the default values
		{		
			this.ContactID = 0;
			this.FirstName = "";
			this.LastName = "";
			this.PersonType = "";
			this.Phone = "";
			this.Mobile = "";
			this.Fax = "";
			this.Email = "";
			this.Approved = false;
			this.UserID = 0;
			this.Notes = "";
			this.Sex = ' ';
		}

		//**********************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			if(ds.Tables[0].Rows.Count > 0)
			{
				this.ContactID = (int)(ds.Tables[0].Rows[0]["PersonID"]);
				this.PersonType = (ds.Tables[0].Rows[0]["persontype"]).ToString();
				if(ds.Tables[0].Rows[0]["Sex"] == DBNull.Value)
				{this.Sex = ' ';}
				else
				{this.Sex = Convert.ToChar(ds.Tables[0].Rows[0]["Sex"]);}
				this.FirstName = (ds.Tables[0].Rows[0]["FirstName"]).ToString();
				this.LastName = (ds.Tables[0].Rows[0]["LastName"]).ToString();
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

				if(ds.Tables[0].Rows[0]["Mobile"] == DBNull.Value)
				{this.Mobile = "";}
				else
				{this.Mobile = (ds.Tables[0].Rows[0]["Mobile"]).ToString();}

				if(ds.Tables[0].Rows[0]["Email"] == DBNull.Value)
				{this.Email = "";}
				else
				{this.Email = (ds.Tables[0].Rows[0]["Email"]).ToString();}

				if(ds.Tables[0].Rows[0]["UserID"] == DBNull.Value)
				{this.UserID = 0;}
				else
				{this.UserID = (int)(ds.Tables[0].Rows[0]["UserID"]);}

				this.Notes = ds.Tables[0].Rows[0]["Notes"].ToString();
			}

			if(ds.Tables[1].Rows.Count > 0)
			{
				this.Relationship = ds.Tables[1].Rows[0]["Relationship"].ToString();
				this.RelationshipDetails = ds.Tables[1].Rows[0]["RelationshipDetails"].ToString();
			}

			if(ds.Tables[2].Rows.Count > 0)
			{
				this.CountryID = Convert.ToInt32(ds.Tables[2].Rows[0]["CountryID"]);
				this.mCountryName = ds.Tables[2].Rows[0]["CountryName"].ToString();
			}

			if(ds.Tables[3].Rows.Count > 0)
			{
				this.PatientName = ds.Tables[3].Rows[0]["PatientName"].ToString();
			}
		}
		//**********************************************************************************************************************
		public int ContactID
		{
			get{return mContactID;}
			set{mContactID = value;}
		}
		//**********************************************************************************************************************
		public char Sex
		{
			get{return mSex;}
			set{mSex = value;}
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
		public string PersonType
		{
			get{return mPersonType;}
			set{mPersonType = value;}
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
		public bool Approved
		{
			get{return mApproved;}
			set{mApproved = value;}
		}
		//**********************************************************************************************************************
		public int UserID
		{
			get{return mUserID;}
			set{mUserID = value;}
		}
		//**********************************************************************************************************************
		public string Notes
		{
			get{return mNotes;}
			set{mNotes = value;}
		}
		//**********************************************************************************************************************
		public string Relationship
		{
			get{return mRelationship;}
			set{mRelationship = value;}
		}
		//**********************************************************************************************************************
		public string RelationshipDetails
		{
			get{return mRelationshipDetails;}
			set{mRelationshipDetails = value;}
		}
		//**********************************************************************************************************************
		public string PatientName
		{
			get{return mPatientName;}
			set{mPatientName = value;}
		}
	}
}
