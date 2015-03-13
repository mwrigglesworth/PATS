using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Person.
	/// </summary>
	public class Person
	{
		private int mPersonID;
		private string mPersonType;
		private string mFirstName;
		private string mLastName;
		private char mSex;
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
		private int mClinicID;
		private int mUserID;
		private string mNotes;

		//other tables
		private string mCountryName;
		private string mUserName;
        private string mPassword;

        //public DataSet PersonDS;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		string connPS = ConfigurationSettings.AppSettings["connPS"];
		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAP2;PWD=secret;UID=sa;";

		//**************************************************************************************************************
		public Person()
		{
			//Default Constructor
			this.Clear();
		}

		//**********************************************************************************************************************
		public Person(int currid, string Urole)
		{
			if(currid == 0)
			{
				return;
			}
			else
			{
				DataSet myData = new DataSet();
				SqlParameter CurrID = new SqlParameter("@PersonID", SqlDbType.Int);
				CurrID.Value = currid;
				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPersonProfile2", CurrID);

				if (myData.Tables[0].Rows.Count <= 0)
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


		//**********************************************************************************************************************
		public Person(GIPAP_Objects.User user)
		{
			int userID = user.UserID;
			if(userID == 0)
			{
				return;
			}
			else
			{
				SqlParameter[] arrParams = new SqlParameter[1];

				arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
				arrParams[0].Value = user.UserID;

				DataSet myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPersonProfilebyUser2", arrParams);

				if (myData.Tables[0].Rows.Count <= 0)
				{
					return;
				}
				else
				{
					Inflate(myData, "Physician");
				}
				myData.Dispose();
			}
		}

		//**********************************************************************************************************************
		public void Create(string createdby, string persontype)
		{
			SqlParameter[] arrParams = new SqlParameter[19];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Direction = ParameterDirection.Output;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
			arrParams[1].Value = this.FirstName.Trim();

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.LastName.Trim();

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

			arrParams[16] = new SqlParameter("@PersonType", SqlDbType.NVarChar, 20);
			arrParams[16].Value = persontype;

			arrParams[17] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[17].Value = this.Sex;

			arrParams[18] = new SqlParameter("@PersonLink", SqlDbType.NChar, 100);
			arrParams[18].Direction = ParameterDirection.Output;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePerson", arrParams);

			//Return the newly created records ID
			this.PersonID = (int)arrParams[0].Value;

			//If the returned value is -1 then the record already exists
			if(this.PersonID == -1)
			{
				throw new ArgumentException(arrParams[18].Value.ToString() + " already exists.");
			}
			else
			{
				this.CreatePS(createdby, persontype);
			}
		}
		//**********************************************************************************************************************
		private void CreatePS(string createdby, string persontype)
		{
			SqlParameter[] arrParams = new SqlParameter[17];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PersonID;

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

			arrParams[16] = new SqlParameter("@PersonType", SqlDbType.NVarChar, 20);
			arrParams[16].Value = persontype;

			SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_CreateGIPAPPerson", arrParams);

		}

		//**********************************************************************************************************************
		public void Update(string modifiedby)
		{
			//Update the GCC information.
			SqlParameter[] arrParams = new SqlParameter[17];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PersonID;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
			arrParams[1].Value = this.FirstName.Trim();

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.LastName.Trim();

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

			arrParams[13] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[13].Value = this.Sex;

			arrParams[14] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[14].Value = this.Notes;

			arrParams[15] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 20);
			arrParams[15].Value = modifiedby;

			arrParams[16] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30);
			arrParams[16].Value = this.Mobile;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePerson", arrParams);

			this.UpdatePS(modifiedby);
		}
		//**********************************************************************************************************************
		private void UpdatePS(string modifiedby)
		{
			//Update the GCC information.
			SqlParameter[] arrParams = new SqlParameter[16];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PersonID;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
			arrParams[1].Value = this.FirstName.Trim();

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.LastName.Trim();

			arrParams[3] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30);
			arrParams[3].Value = this.Mobile;

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

			arrParams[13] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[13].Value = this.Sex;

			arrParams[14] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[14].Value = this.Notes;

			arrParams[15] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 20);
			arrParams[15].Value = modifiedby;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_UpdateGIPAPPerson", arrParams);
		}
		//**************************************************************************************************************
		public void AddPersonNote(string createdby, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PersonID;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = createdby;

			arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
			arrParams[2].Value = note;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePersonNote", arrParams);
		}
		//**********************************************************************************************************************
		public void CreatePersonUser(string createdby)
		{

			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PersonID;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
			arrParams[1].Value = createdby;

			arrParams[2] = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.PersonType;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePersonUser", arrParams);
		}
		//**************************************************************************************************************
		public DataSet getPersonGroup(int groupID)
		{
			SqlParameter arrParams = new SqlParameter();

			arrParams = new SqlParameter("@PersonGroupID", SqlDbType.Int);
			arrParams.Value = groupID;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getPersonGroupProfile", arrParams);
		}
		//**********************************************************************************************************************
		public void CreatePersonGroup(string createdby, string groupName, System.Web.UI.WebControls.ListBox lbMax)
		{

			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@PersonGroupID", SqlDbType.Int);
			arrParams[0].Direction = ParameterDirection.Output;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
			arrParams[1].Value = createdby;

			arrParams[2] = new SqlParameter("@GroupName", SqlDbType.NVarChar, 100);
			arrParams[2].Value = groupName;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePersonGroup", arrParams);

			int pgID = (int)arrParams[0].Value;

			arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PersonGroupID", SqlDbType.Int);
			arrParams[0].Value = pgID;

			for(int i=0; i<lbMax.Items.Count; i++)
			{
				arrParams[1] = new SqlParameter("@PersonID", SqlDbType.Int);
				arrParams[1].Value = Convert.ToInt32(lbMax.Items[i].Value);

				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateMaxStationGroup", arrParams);
			}
		}
		//**********************************************************************************************************************
		public void UpdatePersonGroup(int groupID, string modifiedby, string groupName, System.Web.UI.WebControls.ListBox lbMax)
		{

			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@PersonGroupID", SqlDbType.Int);
			arrParams[0].Value = groupID;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
			arrParams[1].Value = modifiedby;

			arrParams[2] = new SqlParameter("@GroupName", SqlDbType.NVarChar, 100);
			arrParams[2].Value = groupName;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePersonGroup", arrParams);

			arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PersonGroupID", SqlDbType.Int);
			arrParams[0].Value = groupID;

			for(int i=0; i<lbMax.Items.Count; i++)
			{
				arrParams[1] = new SqlParameter("@PersonID", SqlDbType.Int);
				arrParams[1].Value = Convert.ToInt32(lbMax.Items[i].Value);

				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateMaxStationGroup", arrParams);
			}
		}
		//**************************************************************************************************************
		public DataSet getPersonDatasets(string dset)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PersonID;

			arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
			arrParams[1].Value = dset;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPersonDatasets", arrParams);
		}
        //**************************************************************************************************************
        public DataSet getMaxStationDatasets(GIPAP_Objects.User sUse, string dset)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[0].Value = sUse.UserID;

            arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
            arrParams[1].Value = dset;

            if (sUse.CountryID == 76)
            {
                return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetIndiaMaxStationDatasets", arrParams);
            }
            else
            {
                return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetMaxStationDatasets", arrParams);
            }
        }
        //**************************************************************************************************************
        public DataSet getNOAMaxStationDatasets(int uid, string dset)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[0].Value = uid;

            arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
            arrParams[1].Value = dset;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetNOAMaxStationDatasets", arrParams);
        }
        //**************************************************************************************************************
        public DataSet getNOAIndiaMaxStationDatasets(int uid, string dset)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[0].Value = uid;

            arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
            arrParams[1].Value = dset;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetNOAIndiaMaxStationDatasets", arrParams);
        }
		//**********************************************************************************************************************
        public string PersonHeader()
        {
            string physInfo = "<h1><font color=steelblue>" + this.FirstName + " " + this.LastName + "</font></h1>";
            return physInfo;
        }
        //**********************************************************************************************************************
		public string PersonInfo(string Urole)
		{
            string physInfo = "";
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
            physInfo += "<font class='lbl'>Email: </font>" + this.Email + "<br><font class='lbl'>(tel)</font>" + this.Phone + "<br><font class='lbl'>(fax)</font>" + this.Fax + "<br><font class='lbl'>(mobile)</font>" + this.Mobile;
			if(Urole == "TMFUser")
			{
				physInfo += "<br><font class='lbl'>Notes:</font><br>" + this.Notes;
			}
			return physInfo;
		}
		//**********************************************************************************************************************
		public string UserInfo(string uname)
		{
			string aInfo = "<br><font class='lbl'>Username: </font>" + this.UserName + "<br><font class='lbl'>Password: </font>" + (this.UserName==uname?this.Password:"****");
			return aInfo;
		}
		//**********************************************************************************************************************
		private void Clear() //Sets the object to the default values
		{		
			this.PersonID = 0;
			this.PersonType = "";
			this.FirstName = "";
			this.LastName = "";
			this.Specialty = "";
			this.Phone = "";
			this.Mobile = "";
			this.Fax = "";
			this.Email = "";
			this.ClinicID = 159;
			this.UserID = 0;
			this.Notes = "";
			this.Sex = ' ';
		}

		//**********************************************************************************************************************
		private void Inflate(DataSet ds, string Urole)
		{
			this.PersonID = (int)(ds.Tables[0].Rows[0]["PersonID"]);
			this.PersonType = ds.Tables[0].Rows[0]["PersonType"].ToString();
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

			if(ds.Tables[1].Rows.Count > 0)
			{
				this.CountryID = Convert.ToInt32(ds.Tables[1].Rows[0]["CountryID"]);
				if(Urole == "TMFUser" || Urole == "MaxStation")
				{
					this.mCountryName = "<a href=../Country/countryinfo.aspx?choice=" + ds.Tables[1].Rows[0]["CountryID"].ToString();
					this.mCountryName += ">" + ds.Tables[1].Rows[0]["CountryName"].ToString() + "</a>";
				}
				else
				{
				
					this.mCountryName = ds.Tables[1].Rows[0]["CountryName"].ToString();
				}
			}
            try
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    this.UserName = ds.Tables[2].Rows[0]["UserName"].ToString();
                    this.Password = ds.Tables[2].Rows[0]["Password"].ToString();
                }
            }
            catch { }
            //this.PersonDS = ds;
		}
		//**********************************************************************************************************************
		public DataSet PersonSearch(string pType, int activeU)
		{
			string strSQL = "Select '<a href=PersonInfo.aspx?choice=' + Convert(nvarchar, personid) + '>' + LastName as '<b>Last Name</b>',";
			strSQL += " FirstName as '<b>First Name</a>' from tblPerson where persontype = '";
			strSQL += pType + "' and ";
			strSQL += "FirstName like '" + this.FirstName + "%' and ";
			strSQL += "LastName like '" + this.LastName + "%' and ";
			strSQL += "Phone like '" + this.Phone + "%' and ";
			strSQL += "Fax like '" + this.Fax + "%' and ";
			strSQL += "Mobile like '" + this.Mobile + "%' and ";
			strSQL += "Street1 like '" + this.Street1+ "%' and ";
			strSQL += "Street2 like '" + this.Street2 + "%' and ";
			strSQL += "City like '" + this.City + "%' and ";
			strSQL += "stateprovince like '" + this.StateProvince + "%' and ";
			if(this.CountryID != 0)
			{
				strSQL += "CountryID = " + this.CountryID.ToString() + " and ";
			}
            if (pType == "TMFUser")
            {
                strSQL += "userid in (select userid from tbluser where roleid = 1 and isadmin = 0) and ";
            }
			if(activeU != -1)
			{
				if(activeU == 1)
				{
					if(pType == "MaxStation")
					{
						strSQL += "userid in (select userid from tbluser where roleid = 2) and ";
					}
					else if(pType == "Novartis")
					{
						strSQL += "userid in (select userid from tbluser where roleid = 5) and ";
					}
				}
				else
				{
					if(pType == "MaxStation")
					{
						strSQL += "userid not in (select userid from tbluser where roleid = 2) and ";
					}
					else if(pType == "Novartis")
					{
						strSQL += "userid not in (select userid from tbluser where roleid = 5) and ";
					}
				}
			}
			strSQL += "postalcode like '" + this.PostalCode + "%' and ";
			strSQL += "Email like '" + this.Email + "%' ";
			strSQL += "Order by LastName";

			return SqlHelper.ExecuteDataset(connString, CommandType.Text, strSQL);
		}
		//**********************************************************************************************************************
		public int PersonID
		{
			get{return mPersonID;}
			set{mPersonID = value;}
		}
		//**********************************************************************************************************************
		public string PersonType
		{
			get{return mPersonType;}
			set{mPersonType = value;}
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
		public int ClinicID
		{
			get{return mClinicID;}
			set{mClinicID = value;}
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
		public string UserName
		{
			get{return mUserName;}
			set{mUserName = value;}
		}
		//**********************************************************************************************************************
		public string Password
		{
			get{return mPassword;}
			set{mPassword = value;}
		}
	}
}
