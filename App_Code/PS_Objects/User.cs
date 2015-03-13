using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Web;
using System.IO;

namespace PS_Objects
{
	/// <summary>
	/// Summary description for User.
	/// </summary>
	public class User
	{
		private int mUserID;
		private string mUsername;
		private string mPassword;
		private string mPrivilege;

		public string HomePage;

		public DataSet UserDataSet;
        public DataTable OtherUserDT;
        public int TempID;

        string connString = ConfigurationSettings.AppSettings["connPS"];
		
		//**************************************************************************************************************
		public User()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		//**************************************************************************************************************
		public User(int UID)
		{
			//
			// TODO: Add constructor logic here
			//
		}
		//**********************************************************************************************************************
		public User Login()
		{
			if (this.Password == "")
			{
				throw new ArgumentException("You must enter a password.");
			}
			else
			{
				DataSet myData = new DataSet();
				SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
				paramUserName.Value = this.Username;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_ValidateUser", paramUserName);

				if(myData.Tables[0].Rows.Count <= 0)
				{
					this.Clear();
					throw new ArgumentException("The username entered does not exist.");
				}
				else
				{
					if(this.Password != Convert.ToString(myData.Tables[0].Rows[0]["Passwrd"]).ToString())
					{
						this.Clear();
						throw new ArgumentException("The password  entered is invalid.");
					}
					else
					{
						Inflate(myData);
						//this.LogUser();
						return this;
					}
				}
			}
		}
		//**********************************************************************************************************************
		public void TrustedLogin(string uName)
		{
			this.Username = uName;

			SqlParameter arrParams = new SqlParameter("@Username", SqlDbType.NVarChar, 50);
			arrParams.Value = this.Username;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_TrustedLogin", arrParams);

            this.Inflate(ds);

            this.OtherUserDT = ds.Tables[1];
		}
		//**********************************************************************************************************************
		public DataSet QuickSearch(string pin, string first, string last)
		{
			DataSet ds;

			SqlParameter[] arrParams = new SqlParameter[3];
			
			arrParams[0] = new SqlParameter("@PIN", SqlDbType.VarChar, 50);
			arrParams[0].Value = pin;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
			arrParams[1].Value = first;

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
			arrParams[2].Value = last;

			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_QuickSearch", arrParams);
			return ds;
		}
		//**********************************************************************************************************************
		public DataSet GetSocialWorkerList()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_listSocialWorkers");
		}
		//**********************************************************************************************************************
		public DataSet GetTagList()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getTagList");
		}
		//**************************************************************************************************************
		public string MakeFavorite(int FavID, string FavType, string addremove)
		{
			string FavReturn = "";
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = this.UserID;

			arrParams[1] = new SqlParameter("@AddRemove", SqlDbType.NVarChar, 20);
			arrParams[1].Value = addremove;

			if(FavType == "Organization")
			{
				arrParams[2] = new SqlParameter("@OrganizationID", SqlDbType.Int);
				arrParams[2].Value = FavID;

				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_MakeOrgFavorite", arrParams);
				FavReturn = "PatientServices.aspx?trgt=organizationinfo&choice=" + FavID;
			}
			else if(FavType == "Program")
			{
				arrParams[2] = new SqlParameter("@ProgramID", SqlDbType.Int);
				arrParams[2].Value = FavID;

				DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_MakeProgramFavorite", arrParams);
				FavReturn = "PatientServices.aspx?trgt=organizationinfo&choice=" + ds.Tables[0].Rows[0]["organizationid"].ToString();
			}
			else if(FavType == "Clinic")
			{
				arrParams[2] = new SqlParameter("@ClinicID", SqlDbType.Int);
				arrParams[2].Value = FavID;

				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_MakeClinicFavorite", arrParams);
				FavReturn = "PatientServices.aspx?trgt=clinicinfo&choice=" + FavID;
			}
			else if(FavType == "Physician")
			{
				arrParams[2] = new SqlParameter("@PersonID", SqlDbType.Int);
				arrParams[2].Value = FavID;

				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_MakePhysicianFavorite", arrParams);
				FavReturn = "PatientServices.aspx?trgt=physicianinfo&choice=" + FavID;
			}
			return FavReturn;
		}
		//**********************************************************************************************************************
		public DataSet ContactActivity(DateTime sDate, DateTime eDate, string report)
		{
			DataSet ds;

			SqlParameter[] arrParams = new SqlParameter[3];
			
			arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[0].Value = sDate;

			arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[1].Value = eDate;

			arrParams[2] = new SqlParameter("@Report", SqlDbType.NVarChar, 20);
			arrParams[2].Value = report;

			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_ContactActivity", arrParams);
			return ds;
		}
        //**********************************************************************************************************************
        public DataSet PINCIntakeReport(DateTime sDate, DateTime eDate, string report)
        {
            DataSet ds = new DataSet();

            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            arrParams[0].Value = sDate;

            arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
            arrParams[1].Value = eDate;

            arrParams[2] = new SqlParameter("@ReportUser", SqlDbType.NVarChar, 50);
            arrParams[2].Value = this.Username;

            if (report == "User Totals")
            {
                ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_PINCIntakeUserTotals", arrParams);
            }
            else if (report == "Monthly Totals")
            {
                ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_PINCIntakeMonthlyTotals", arrParams);
            }
            else if (report == "Patient Country")
            {
                ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_PINCIntakePatientCountry", arrParams);
            }
            return ds;
        }
		//**********************************************************************************************************************
		public DataSet ContactRates(DateTime sDate, DateTime eDate, string report)
		{
			DataSet ds = new DataSet();

			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[0].Value = sDate;

			arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[1].Value = eDate;

			if(report == "Approved")
			{
				ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_ApprovedVsContacted", arrParams);
			}
			else if(report == "Active")
			{
				ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_ActiveVsContacted", arrParams);
			}
			return ds;
		}
		//**********************************************************************************************************************
		public DataSet NotContacted(DateTime sDate, DateTime eDate, int countryid, string program)
		{
			DataSet ds = new DataSet();

			SqlParameter[] arrParams = new SqlParameter[4];
			
			arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[0].Value = sDate;

			arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[1].Value = eDate;

			arrParams[2] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[2].Value = countryid;

            arrParams[3] = new SqlParameter("@Program", SqlDbType.NVarChar, 50);
            arrParams[3].Value = program;

			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_NotContacted", arrParams);

			return ds;
		}
		//**********************************************************************************************************************
		public void CreateChangeRequest(string request)
		{
			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@ChangeRequest", SqlDbType.Text);
			arrParams[0].Value = request;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[1].Value = this.Username;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateChangeRequest", arrParams);

            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
			//myEmail.SendChangeRequestEmail(this.Username, request);
		}
		//**********************************************************************************************************************
		public void ProcessChangeRequest(int cReqID, string action, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[4];
			
			arrParams[0] = new SqlParameter("@ChangeRequestID", SqlDbType.Int);
			arrParams[0].Value = cReqID;

			arrParams[1] = new SqlParameter("@Action", SqlDbType.VarChar, 50);
			arrParams[1].Value = action;

			arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
			arrParams[2].Value = note;

			arrParams[3] = new SqlParameter("@ResolvedBy", SqlDbType.NVarChar, 50);
			arrParams[3].Value = this.Username;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ProcessChangeRequest", arrParams);

		}
		//**********************************************************************************************************************
		public DataSet ChangeRequests(string action)
		{
			DataSet ds;
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = this.UserID;
			
			arrParams[1] = new SqlParameter("@Action", SqlDbType.NVarChar, 50);
			arrParams[1].Value = action;

			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetChangeRequests", arrParams);
			return ds;
		}
		//**********************************************************************************************************************
		public DataSet PSNonActiveGIPAPServices(DateTime sDate, DateTime eDate)
		{
			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[0].Value = sDate;

			arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[1].Value = eDate;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_PSNonActiveGIPAPServices", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet GetMaxStationList()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_listMaxStations");
		}
		//**********************************************************************************************************************
		public DataSet GetUserList()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getUserList");
		}
		//**********************************************************************************************************************
		public DataSet GetNewForms()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetNewForms");
		}
		//**********************************************************************************************************************
		public DataSet GetWorkLoadDataSets(int uid)
		{
			SqlParameter arrParams = new SqlParameter("@UserID", SqlDbType.Int);
            if (uid == 0)
            {
                arrParams.Value = this.UserID;
            }
            else
            {
                arrParams.Value = uid;
            }

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetWorkLoad", arrParams);
		}
		//**********************************************************************************************************************
		public void AcceptTag(int pid)
		{
			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = this.UserID;

			arrParams[1] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[1].Value = pid;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_AcceptTag", arrParams);
		}
        //**********************************************************************************************************************
        public string HomePageAllUsers()
        {
            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetQueuesAllUsers");

            string hplinks = "";
            hplinks += "<li><a href=PatientServices.aspx?trgt=datadisplay&ds=WebApplicants>" + ds.Tables[0].Rows[0]["count"].ToString() + " Web Applicants</a><br><br>";
            return hplinks;
        }
		//**********************************************************************************************************************
		public string HomePageLinks(int uid)
		{
            SqlParameter arrParams = new SqlParameter("@UserID", SqlDbType.Int);
            if (uid == 0)
            {
                arrParams.Value = this.UserID;
            }
            else
            {
                arrParams.Value = uid;
            }

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetQueues", arrParams);

			string hplinks = "";
            hplinks += "<b>My Tags!</b><br><br>";
			if(ds.Tables[0].Rows[0]["count"].ToString() !=  "0")
			{
				hplinks += "<li><b><a href=PatientServices.aspx?trgt=datadisplay&ds=unresolvedtags&choice=" + uid.ToString() + ">Unresolved Tags (" + ds.Tables[0].Rows[0]["count"].ToString() + ")</a></b><br><br>";
			}
			else
			{
                hplinks += "<li><a href=PatientServices.aspx?trgt=datadisplay&ds=unresolvedtags&choice=" + uid.ToString() + ">Unresolved Tags</a><br><br>";
			}
            hplinks += "<li><a href=PatientServices.aspx?trgt=datadisplay&ds=senttags&choice=" + uid.ToString() + ">Tags I have sent</a><br><br>";
            hplinks += "<li><a href=PatientServices.aspx?trgt=datadisplay&ds=resolvedtags&choice=" + uid.ToString() + ">Resolved Tags</a><br><br>";
			if(this.UserID == 1 || this.UserID == 3)
			{
                try
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        hplinks += "<br><br><b>Change Requests</b>";
                        hplinks += "<li><a href=PatientServices.aspx?trgt=processrequests>" + ds.Tables[0].Rows.Count.ToString() + " Change Requests</a>";
                    }
                }
                catch { }
			}
			return hplinks;
		}
		//**************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			//Populates the objects parameters with the data returned from the database
			this.UserID = (int)(ds.Tables[0].Rows[0]["UserID"]);
			this.Username = (ds.Tables[0].Rows[0]["Username"]).ToString();
			this.Password = (ds.Tables[0].Rows[0]["Passwrd"]).ToString();
			this.Privilege = ds.Tables[0].Rows[0]["Privilege"].ToString();
			this.HomePage = "PatientServices.aspx?trgt=homepage";
            this.TempID = 0;
			//this.UserDataSet = ds;
		}

		//**************************************************************************************************************
		public void Clear()
		{
			this.UserID = 0;
			this.Username = "";
			this.Password = "";
			this.Privilege = "";
			this.HomePage = "Default.aspx";
		}

		//**************************************************************************************************************
		public int UserID
		{
			get{return mUserID;}
			set{mUserID = value;}
		}

		//**************************************************************************************************************
		public string Username
		{
			get{return mUsername;}
			set{mUsername = value;}
		}

		//**************************************************************************************************************
		public string Password
		{
			get{return mPassword;}
			set{mPassword = value;}
		}

		//**************************************************************************************************************
		public string Privilege
		{
			get{return mPrivilege;}
			set{mPrivilege = value;}
		}

	}
}
