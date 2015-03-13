using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for User.
	/// </summary>
	public class User
	{
		private int mUserID;
		private string mUsername;
		private string mPassword;
		private bool mIsAdmin;
        private bool mIsFileSharingAdmin;
		private bool mDisabled;
		private string mRole;
		private string mHomePage;
		private string mLeftNav;
		private string mErrorMessage;
		private string mHeader;
        private DateTime mExpireDate;
        private int mCountryID;
        private string mProgram;
        private string mFullName;
        private bool mRAP;
		//mou
        private bool MOU;
        private bool mTIPAPPhys;
        private bool mNOA;
        private DateTime mNOADate;
        private int mTasigna;
        private bool MOUAmmendments;
        private bool CountryMOU;
        private bool mAcceptingNewApps;

		public DataSet UserDS;
        //public string Message;
        public string CountryList;
		public int ClickCount;
        public int TempID;
        public string TempName;
        public DataTable OtherUsers;
        public DataTable CountryTable;
		
        public List<int> latamlist = new List<int>();
		
		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
        string connMYPAP = ConfigurationSettings.AppSettings["ConnMYPAP"];
        string connPS = ConfigurationSettings.AppSettings["connPS"];

		//**************************************************************************************************************
		public User()
		{
			//Default constructor
			this.Clear();
		}

		//**********************************************************************************************************************
		public User Login(string uPlatform, string uIP)
		{
			if (this.Password == "")
			{
				this.ErrorMessage = "You must enter a password";
				return this;
			}
			else
			{
				DataSet myData = new DataSet();
				SqlParameter paramUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
				paramUserName.Value = this.Username;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_ValidateUser2", paramUserName);

				if(myData.Tables[0].Rows.Count <= 0)
				{
					this.Clear();
					this.ErrorMessage = "The username entered does not exist.";
					return this;
				}
				else
				{
					if(this.Password != Convert.ToString(myData.Tables[0].Rows[0]["Password"]).ToString())
					{
						this.Clear();
						this.ErrorMessage = "The password  entered is invalid";
						return this;
					}
					else
					{
						Inflate(myData);
						this.LogUser(uPlatform, uIP);
						if(Role == "TMFUser")
						{
							this.HomePage = "TMF/Dashboard.aspx";
                            this.Program = "GIPAP";
							this.LeftNav = "TMF/LeftNav.ascx";
							this.Header = "TMF/Header.ascx";
						}
						else if(Role == "MaxStation")
						{
                            this.HomePage = "MaxStation/Dashboard.aspx";
                            this.Program = "GIPAP";
							this.LeftNav = "MaxStation/LeftNav.ascx";
							this.Header = "MaxStation/Header.ascx";
						}
						else if(Role == "Physician")
						{
                            if (this.CountryID != 102)
                            {
                                if (this.MOU)
                                {
                                    if (this.MOUAmmendments)
                                    {
                                        this.HomePage = "Physician/Dashboard.aspx";
                                    }
                                    else
                                    {
                                        this.HomePage = "Physician/MOUAmmendments.aspx";
                                    }
                                }
                                else
                                {
                                    this.HomePage = "Physician/MOU.aspx";
                                }
                            }
                            else
                            {
                                if (this.CountryMOU)
                                    this.HomePage = "Physician/Dashboard.aspx";
                                else
                                    this.HomePage = "Physician/MalMOUAmendments.aspx";
                            }
                            this.Program = "GIPAP";
							this.LeftNav = "Physician/LeftNav.ascx";
							this.Header = "Physician/Header.ascx";
                            AddLatamlist();
						}
						else if(Role == "Novartis")
                        {
                            this.HomePage = "NovartisUser/Dashboard.aspx";
							this.LeftNav = "Novartis/LeftNav.ascx";
							this.Header = "Novartis/Header.ascx";
						}
						else if(Role == "MYPAP")
						{
							this.HomePage = "/MYPAP/gipaptrusted.aspx?reqform=MyPap&user=" + this.Username;
							//this.LeftNav = "Novartis/LeftNav.ascx";
							//this.Header = "Novartis/Header.ascx";
                        }
                        else if (Role == "FCCallCenter")
                        {
                            this.HomePage = "GIPAP.aspx?trgt=fcmenu";
                            this.LeftNav = "FamilyCredit/CCLeftNav.ascx";
                            this.Header = "FamilyCredit/CCHeader.ascx";
                        }
                        else if (Role.StartsWith("FC"))
                        {
                            this.LeftNav = "FamilyCredit/LeftNav.ascx";
                            this.Header = "FamilyCredit/Header.ascx";
                            this.Program = "GIPAP";
                            if (this.ExpireDate < DateTime.Today.AddDays(7))
                            {
                                this.HomePage = "TMF/ChangePassword.aspx";
                            }
                            else
                            {
                                this.HomePage = "FinancialEvaluator/Dashboard.aspx";
                            }
                        }
						else
						{
							this.Clear();
						}
						return this;
					}
				}
			}
		}
        //**************************************************************************************************************

        public void AddLatamlist()
        {
            latamlist.Add(5);
            latamlist.Add(17);
            latamlist.Add(21);
            latamlist.Add(24);
            latamlist.Add(35);
            latamlist.Add(37);
            latamlist.Add(71);
            latamlist.Add(128);
            latamlist.Add(174);
            latamlist.Add(178);
            latamlist.Add(205);
        }

		//**************************************************************************************************************
		public void Create(string createdby)
		{
			//Add a new DSR to the database
			SqlParameter[] arrParams = new SqlParameter[5];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Direction = ParameterDirection.Output;

			arrParams[1] = new SqlParameter("@Username", SqlDbType.VarChar, 30);
			arrParams[1].Value = this.Username.Trim();

			arrParams[2] = new SqlParameter("@Password", SqlDbType.VarChar, 10);
			arrParams[2].Value = this.Password.Trim();

			arrParams[3] = new SqlParameter("@Role", SqlDbType.VarChar, 30);
			arrParams[3].Value = this.Role;

			arrParams[4] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 20);
			arrParams[4].Value = createdby;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateUser", arrParams);

			//Return the newly created records ID
			this.UserID = (int)arrParams[0].Value;

			//If the returned value is -1 then the record already exists
			if(this.UserID == -1)
			{
				throw new ArgumentException("The user already exists.");
			}
		}

		//**************************************************************************************************************	
		public void Update(string modifiedby)
		{
			//Role exists so update the information
			SqlParameter[] arrParams = new SqlParameter[6];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = this.UserID;

			arrParams[1] = new SqlParameter("@Username", SqlDbType.VarChar, 30);
			arrParams[1].Value = this.Username.Trim();

			arrParams[2] = new SqlParameter("@Password", SqlDbType.VarChar, 10);
			arrParams[2].Value = this.Password.Trim();

			arrParams[3] = new SqlParameter("@IsAdmin", SqlDbType.Bit);
			arrParams[3].Value = this.IsAdmin;

			arrParams[4] = new SqlParameter("@Disabled", SqlDbType.Bit);
			arrParams[4].Value = this.Disabled;

			arrParams[5] = new SqlParameter("@Role", SqlDbType.VarChar, 30);
			arrParams[5].Value = this.Role;

			arrParams[6] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 20);
			arrParams[6].Value = modifiedby;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateUser", arrParams);
		}
		//**************************************************************************************************************	
		public void UpdateUserPassword(string modifiedby)
		{
			//Role exists so update the information
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = this.UserID;

			arrParams[1] = new SqlParameter("@Password", SqlDbType.VarChar, 50);
			arrParams[1].Value = this.Password.Trim();

			arrParams[2] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 20);
			arrParams[2].Value = modifiedby;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateUserPassword", arrParams);
		}
		//**********************************************************************************************************************
		public string LogOut(GIPAP_Objects.User sessUse)
		{
			sessUse.Clear();
			return "/Default.aspx"; /*live*/
            //return "/patstest/Default.aspx"; /*test*/
		}
		//**********************************************************************************************************************
		public DataSet getDatasets(int uid, string dset)
		{
			DataSet ds;
			if(uid == 0)
			{
				uid = this.UserID;
			}

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = uid;
			
			arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
			arrParams[1].Value = dset;

			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetUserDataSets2", arrParams);
			return ds;
		}
        //**********************************************************************************************************************
        public DataSet getPORequests(int uid)
        {
            DataSet ds;
            if (uid == 0)
            {
                uid = this.UserID;
            }

            SqlParameter arrParams = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams.Value = uid;

            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPORequests", arrParams);
            return ds;
        }
		//**********************************************************************************************************************
		public DataSet getPOEmails(int uid)
		{
			DataSet ds;
			if(uid == 0)
			{
				uid = this.UserID;
			}

			SqlParameter arrParams = new SqlParameter();

			arrParams = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams.Value = uid;

			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPOEmails", arrParams);
			return ds;
		}


		//**********************************************************************************************************************
		public DataSet getPOQueues(int uid)
		{
			DataSet ds;
			if(uid == 0)
			{
				uid = this.UserID;
			}

			SqlParameter arrParams = new SqlParameter();

			arrParams = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams.Value = uid;

			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPOQueues2", arrParams);
			return ds;
		}


        //**********************************************************************************************************************
        public string getMYPAPQueues(int uid)
        {
            DataSet ds;
            
            SqlParameter[] arrParams = new SqlParameter[1];

            arrParams[0] = new SqlParameter("@UserName", SqlDbType.VarChar,50);
            arrParams[0].Value = this.Username;

            //arrParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            //arrParams[1].Value = uid;

            ds = SqlHelper.ExecuteDataset(connMYPAP, CommandType.StoredProcedure, "spr_GetMYPAPQueues", arrParams);
            foreach (DataTable tab in ds.Tables)
            {
                if (Convert.ToInt32(tab.Rows[0]["count"]) > 0)
                {
                    return "MYPAP Queues Available";
                    break;
                }
                //else
                //{
                //    return "MYPAP Queues Not Available";
                //}
            }
            return "";
        }

        //**********************************************************************************************************************
        public string getPINCQueues(int uid)
        {
            DataSet ds;

            SqlParameter[] arrParams = new SqlParameter[1];

            arrParams[0] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
            arrParams[0].Value = this.Username;

            //arrParams[1] = new SqlParameter("@UId", SqlDbType.Int);
            //arrParams[1].Value = uid;

            ds = SqlHelper.ExecuteDataset(connPS, CommandType.StoredProcedure, "spr_getPINCQueues", arrParams);
            foreach (DataTable tab in ds.Tables)
            {
                if (Convert.ToInt32(tab.Rows[0]["count"]) > 0)
                {
                    return "PINC Queues Available";
                }
                //else
                //{
                //    return "PINC Queues Not Available";
                //}

            }
            return "";
        }
        //**********************************************************************************************************************
        public string getUserMessage()
        {
            SqlParameter arrParams = new SqlParameter();

            arrParams = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            arrParams.Value = this.Username;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getUserMessage", arrParams);
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["newsmessage"].ToString();
            else
                return string.Empty;
        }
        //**********************************************************************************************************************
        public DataSet getPhysicianMessage()
        {
            SqlParameter arrParams = new SqlParameter();

            arrParams = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
            arrParams.Value = this.Username;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getPhysicianMessage", arrParams);
            //if (ds.Tables[0].Rows.Count > 0)
            //    return Convert.ToString(ds.Tables[0].Rows[0]["newsmessage"]);
            //else
            //    return string.Empty;
            return ds;
        }

        //**********************************************************************************************************************
        public DataSet getCurrentMessage(int role,int country)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@RoleId", SqlDbType.Int);
            arrParams[0].Value = role;

            arrParams[1] = new SqlParameter("@CountryId", SqlDbType.Int);
            arrParams[1].Value = country;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getCurrentMessage", arrParams);
            return ds;
        }


        //**********************************************************************************************************************
        public DataSet getAllMessage()
        {
            //SqlParameter[] arrParams = new SqlParameter[2];

            //arrParams[0] = new SqlParameter("@RoleId", SqlDbType.Int);
            //arrParams[0].Value = role;

            //arrParams[1] = new SqlParameter("@CountryId", SqlDbType.Int);
            //arrParams[1].Value = country;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getAllMessage");
            return ds;
        }
        //**********************************************************************************************************************
        public DataSet getSharedFile()
        {

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getSharedFiles");
            return ds;
        }


        //**********************************************************************************************************************
        public int DeleteRecord(string ID)
        {
            return SqlHelper.ExecuteNonQuery(connString, CommandType.Text, "update tblFileSharing set deleted=1 where FileID='" + ID + "'");
        }

        //**********************************************************************************************************************
        public int UpdateRecord(string name,string path, string id)
        {
            return SqlHelper.ExecuteNonQuery(connString, CommandType.Text, "update tblFileSharing set FileName='"+name+"',FilePath='"+path+"' where FileID='" + id+ "'");
        }
        //**********************************************************************************************************************
        public int DeleteCurrentMessage(int ID)
        {
            return SqlHelper.ExecuteNonQuery(connString,CommandType.Text,"update tblMessage set Deleted=1 where MessageID='"+ID+"'");
        }

		//**************************************************************************************************************	
		public void UpdateSurvey(int beneficial, int lesstime, int communications, string comments)
		{
			//Role exists so update the information
			SqlParameter[] arrParams = new SqlParameter[5];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = this.UserID;

			arrParams[1] = new SqlParameter("@Beneficial", SqlDbType.Int);
			arrParams[1].Value = beneficial;

			arrParams[2] = new SqlParameter("@LessTime", SqlDbType.Int);
			arrParams[2].Value = lesstime;

			arrParams[3] = new SqlParameter("@Communications", SqlDbType.Int);
			arrParams[3].Value = communications;

			arrParams[4] = new SqlParameter("@Comments", SqlDbType.Text);
			arrParams[4].Value = comments;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateSurvey", arrParams);

			this.HomePage = "GIPAP.aspx?trgt=physicianmenu";
		}
		
		//**********************************************************************************************************************
		public string HomePageLinks()
		{
            StringBuilder hp = new StringBuilder();
			if(this.Role == "Physician"/* && this.NOA && this.NOADate <= DateTime.Today*/)
            {
                if (this.AcceptingNewApps)
                {
                    hp.Append("<li><a href='../Application/GIPAPApplication.aspx' class='lbAR'>Submit an Application</font></a></li><br>");
                }
                else
                {
                    hp.Append("<li><font color=gray>No longer accepting applications in your country</font></li><br>");
                }
                hp.Append("<li><a href='../Physician/DataDisplay.aspx?dset=patientsneedingreapproval' class='lbAR'>Patients Needing Reapproval</font></a></li><br>");
                hp.Append("<li><a href='../Physician/DataDisplay.aspx?dset=closepatients' class='lbAR'>Close a Patient Case</font></a></li><br>");
                hp.Append("<li><a href='../Physician/DataDisplay.aspx?dset=dosechangepatients' class='lbAR'>Change Dosage</font></a></li><br>");
                hp.Append("<li><a href='../Physician/DataDisplay.aspx?dset=reactivatepatients' class='lbAR'>Reactivate a Closed Case</font></a></li><br>");
                hp.Append("<li><a href='../Physician/DataDisplay.aspx?dset=allpatients' class='lbAR'>View all my patients</font></a></li><br>");

                if (this.Tasigna > 0)
                {
                    hp.Append("<li><a href=../Physician/DataDisplay.aspx?dset=changetreatment>Active - Change Treatment</a></li><br><li><a href=../Physician/DataDisplay.aspx?dset=reactivatepatients>Closed - Reactivate With Tasigna</a></li>");
                    //notice from india
                    //hp.Append("<table width=100% style='BORDER: purple 1px solid;'><tr><td>");
                    //hp.Append("<font color=purple>Dear Physician,<br><br>We have updated the Patient Assistance Tracking System PATS, to support the management of patients in NOA-Tasigna.  You have signed the new NOA MOU and  may now request  treatment with Tasigna 1st or 2nd Line for qualified patients as per the label indication.  The NOA Tasigna program is based on a fixed 8 + 44 weeks donation scheme. <br><br>To request Tasigna for one of your Active or Closed Glivec patients to Tasigna treatment, click on the appropriate link under “Change to Tasigna” on the left hand side of your page.  To enter an application for a patient that is NOT in PATS, click “Submit an Application” under “My Patients” on the left hand side of your page and choose “Tasigna” as treatment.  <br><br>Should you have any questions, please contact the Programme Administrator - The Max Foundation’s office (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322.</font></td></tr></table>");
                }
			}
            else if (this.Role == "MaxStation")
            {
                SqlParameter arrParams = new SqlParameter("@UserID", SqlDbType.Int);
                arrParams.Value = this.UserID;

                if (this.CountryID == 76)
                {

                    DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetIndiaMaxStationQueues", arrParams);

                    /*if (ds.Tables[1].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=unresolvedNOARequests>" + ds.Tables[1].Rows[0]["count"].ToString() + " Patient Action Requests</a></li><br>");
                        this.NOA = true;
                    }*/
                    hp.Append("<li><a href=DataDisplay.aspx?dset=unresolvedNOARequests>Patient Action Requests</a></li><br>");
                    this.NOA = true;
                    if (ds.Tables[0].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=unassignedNOApending><font color=green>" + ds.Tables[0].Rows[0]["count"].ToString() + " Pending NOA Patients without a branch assigned</font></a></li><br>");
                        this.NOA = true;
                    }
                    /*for queues needing a FEF, rec=Deny are included just to keep MS aware, no action is technically needed*/
                    /*if (ds.Tables[3].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAPending><font color=purple>" + ds.Tables[3].Rows[0]["count"].ToString() + " Pending NOA Patients</font></a></li><br>");
                        this.NOA = true;
                    }*/
                    hp.Append("<li><a href=DataDisplay.aspx?dset=NOAPending><font color=purple>Pending NOA Patients</font></a></li><br>");
                    if (ds.Tables[1].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAreactivationrequestsNoBranch><font color=orange>" + ds.Tables[1].Rows[0]["count"].ToString() + " NOA Reactivation Requests without a branch assigned</font></a></li><br>");
                        this.NOA = true;
                    }
                    if (ds.Tables[2].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAreassessmentrequestsNoBranch><font color=crimson>" + ds.Tables[2].Rows[0]["count"].ToString() + " NOA Reassessment Requests without a branch assigned</font></a></li><br>");
                        this.NOA = true;
                    }
                    if (ds.Tables[5].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAchangetreatmentrequestsNoBranch><font color=purple>" + ds.Tables[5].Rows[0]["count"].ToString() + " NOA Treatment Change Requests without a branch assigned</font></a></li><br>");
                        this.NOA = true;
                    }
                    /*if (ds.Tables[6].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=YearlyReassessment><font color=firebrick>" + ds.Tables[6].Rows[0]["count"].ToString() + " NOA Yearly Reassessment Required</font></a></li><br>");
                        this.NOA = true;
                    }*/
                    hp.Append("<li><a href=DataDisplay.aspx?dset=YearlyReassessment><font color=firebrick>NOA Reassessment Required</font></a></li><br>");
                    if (ds.Tables[3].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAReactivationRequests><font color=navy>" + ds.Tables[3].Rows[0]["count"].ToString() + " NOA Reactivation Requests</font></a></li><br>");
                        this.NOA = true;
                    }
                    if (ds.Tables[4].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAReassessmentRequests><font color=darkorange>" + ds.Tables[4].Rows[0]["count"].ToString() + " NOA Reassessment Requests</font></a></li><br>");
                        this.NOA = true;
                    }
                    if (ds.Tables[6].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAChangeTreatmentRequests><font color=purple>" + ds.Tables[6].Rows[0]["count"].ToString() + " NOA Treatment Change Requests</font></a></li><br>");
                        this.NOA = true;
                    }
                    if (this.NOA)
                    {
                        hp.Append("<hr><b><font color=gray>Past NOA Requests</font></b><br><br><a href=DataDisplay.aspx?dset=resolvedNOARequests>Resolved Requests</a> |  <a href=DataDisplay.aspx?dset=sentNOARequests>Requests I Sent</a><hr>");
                    }
                }
                else
                {
                    DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetMaxStationQueues", arrParams);
                    
                    if (ds.Tables[0].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAPending><font color=purple>" + ds.Tables[0].Rows[0]["count"].ToString() + " Pending NOA Patients</font></a></li><br>");
                        this.NOA = true;
                    }
                    if (ds.Tables[1].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAReactivationRequests><font color=navy>" + ds.Tables[1].Rows[0]["count"].ToString() + " NOA Reactivation Requests</font></a></li><br>");
                        this.NOA = true;
                    }
                    if (ds.Tables[2].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAReassessmentRequests><font color=darkorange>" + ds.Tables[2].Rows[0]["count"].ToString() + " NOA Reassessment Requests</font></a></li><br>");
                        this.NOA = true;
                    }
                    if (ds.Tables[3].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=YearlyReassessment><font color=firebrick>" + ds.Tables[3].Rows[0]["count"].ToString() + " NOA Reassessment Required</font></a></li><br>");
                        this.NOA = true;
                    }
                    if (ds.Tables[4].Rows[0]["count"].ToString() != "0")
                    {
                        hp.Append("<li><a href=DataDisplay.aspx?dset=NOAChangeTreatmentRequests><font color=purple>" + ds.Tables[4].Rows[0]["count"].ToString() + " NOA Treatment Change Requests</font></a></li><br>");
                        this.NOA = true;
                    }
                }
                //for all max stations
                hp.Append("<li><a href='DataDisplay.aspx?dset=pendingpatients' class='lbAR'>Pending Patients</font></a></li><br>");
                hp.Append("<li><a href='DataDisplay.aspx?dset=pastduepatients' class='lbAR'>Past Due Patients</font></a></li><br>");
                hp.Append("<li><a href='DataDisplay.aspx?dset=patientsneedingreapproval' class='lbAR'>Patients Needing Reapproval</font></a></li><br>");
                hp.Append("<li><a href='DataDisplay.aspx?dset=pendingreapprovals' class='lbAR'>Pending Reapprovals</font></a></li><br>");
                hp.Append("<li><a href='DataDisplay.aspx?dset=pendingrequests' class='lbAR'>Pending Requests</font></a></li><br>");
            }
            return hp.ToString();
		}
		//**********************************************************************************************************************
		public string EmailLinks(int uid)
		{
			DataSet ds = this.getPOEmails(uid);
			string request = "<b>Emails / Auto-Closures for <font color=steelblue>" + ds.Tables[3].Rows[0]["firstname"].ToString() + " " + ds.Tables[3].Rows[0]["lastname"].ToString() + "</font></b><br><br>";
			if(ds.Tables[0].Rows[0]["count"].ToString() != "0")
			{
				request += "<li><a href=GIPAP.aspx?trgt=reminderemails&choice=" + uid.ToString() + "&dset=firstreminders><font color=deeppink>" + ds.Tables[0].Rows[0]["count"].ToString() + " Patient Reminders</font></a><br><br>";
			}
			else
			{
				request += "<li>0 Patient Reminders<br><br>";
			}
			if(ds.Tables[1].Rows[0]["count"].ToString() != "0")
			{
				request += "<li><a href=GIPAP.aspx?trgt=reminderemails&choice=" + uid.ToString() + "&dset=secondreminders><font color=green>" + ds.Tables[1].Rows[0]["count"].ToString() + " Patient Second Notices</font></a><br><br>";
			}
			else
			{
				request += "<li>0 Patient Second Notices<br><br>";
			}
			if(ds.Tables[4].Rows.Count > 0)
			{
				request += "<li><a href=GIPAP.aspx?trgt=reminderemails&choice=" + uid.ToString() + "&dset=physicianreminders><font color=steelblue>" + ds.Tables[4].Rows.Count.ToString() + " Physician Reminders</font></a><br><br>";
			}
			else
			{
				request += "<li>0 Physician Reminders<br><br>";
			}
			if(ds.Tables[5].Rows[0]["count"].ToString() != "0")
			{
				request += "<li><a href=GIPAP.aspx?trgt=autoclose&choice=" + uid.ToString() + "><font color=lime>" + ds.Tables[5].Rows[0]["count"].ToString() + " Auto Closures</font></a><br><br>";
			}
			else
			{
				request += "<li>0 Auto Closures<br><br>";
			}
			request += "<hr><b>Other TMF Users</b><br><br>" + ds.Tables[2].Rows[0]["username"].ToString();
			for(int i=1; i<ds.Tables[2].Rows.Count; i++)
			{
				request += " | " + ds.Tables[2].Rows[i]["username"].ToString();
			}
			return request;
		}
        //**********************************************************************************************************************
        public string AllUserLinks()
        {
            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPOAllQueues");
            StringBuilder request = new StringBuilder();
            if (ds.Tables[0].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?dset=patientwebapplicants><font color=red>" + ds.Tables[0].Rows[0]["count"].ToString() + " Web Applicants</font></a><br><br>");
            }
            if (ds.Tables[1].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?dset=pendingphysicians><font color=crimson>" + ds.Tables[1].Rows[0]["count"].ToString() + " Pending Physicians</font></a><br><br>");
            }
            if (ds.Tables[2].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?dset=pendingclinics><font color=firebrick>" + ds.Tables[2].Rows[0]["count"].ToString() + " Pending Clinics</font></a><br><br>");
            }
            request.Append("<li><a href=datadisplay.aspx?dset=pastduepatients><font color=purple>Past Due</font></a><br><br>");
            request.Append("<li><a href=datadisplay.aspx?dset=pendingnoa><font color=green>Pending NOA</font></a><br><br>");
            request.Append("<li><a href=datadisplay.aspx?dset=deletedapplicants><font color=blue>Deleted Applicants</font></a><br><br>");
            return request.ToString();
        }
        //**********************************************************************************************************************
        public DataSet getWebApplicants()
        {
            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetWebApplicants");
        } 
		//**********************************************************************************************************************
        public string HomePageLinks(int uid)
        {
            DataSet ds = this.getPOQueues(uid);
            StringBuilder request = new StringBuilder();
            if (ds.Tables[0].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=ReapprovalRequests.aspx?choice=" + uid.ToString() + "&dset=reapprovalrequests><font color=red>" + ds.Tables[0].Rows[0]["count"].ToString() + " Reapproval Requests</font></a><br><br>");
                //request.Append("<li><a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=reapprovalrequests><font color=red>" + ds.Tables[1].Rows[0]["count"].ToString() + " Reapproval Requests</font></a><br><br>");
            }
            if (ds.Tables[1].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=PORequests.aspx?choice=" + uid.ToString() + "><font color=orange>" + ds.Tables[1].Rows[0]["count"].ToString() + " Update Requests</font></a><br><br>");
            }
            if (ds.Tables[2].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=pendingpatients><font color=purple>" + ds.Tables[2].Rows[0]["count"].ToString() + " Pending Patients</font></a><br><br>");
            }
            if (ds.Tables[3].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=CaseNoteAlerts.aspx?choice=" + uid.ToString() + "><font color=blue>" + ds.Tables[3].Rows[0]["count"].ToString() + " Case Note Alerts</font></a><br><br>");
            }
            if (ds.Tables[9].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=SentCaseNoteAlerts.aspx?action=remove&choice=" + uid.ToString() + "><font color=navy>" + ds.Tables[9].Rows[0]["count"].ToString() + " Case Note Alerts I Sent</font></a><br><br>");
            }
            if (ds.Tables[4].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=PhysicianTransferRequests.aspx?choice=" + uid.ToString() + "><font color=darkorange>" + ds.Tables[4].Rows[0]["count"].ToString() + " Physician Transfer Requests</font></a><br><br>");
            }
            if (ds.Tables[5].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=noapatientsneedingaction><font color=green>" + ds.Tables[5].Rows[0]["count"].ToString() + " Pending NOA Patients Needing Action</font></a><br><br>");
            }
            if (ds.Tables[6].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=unresolvedNOARequests>" + ds.Tables[6].Rows[0]["count"].ToString() + " Patient Action Requests</a><br><br>");
            }
            if (ds.Tables[7].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=YearlyReassessment><font color=firebrick>" + ds.Tables[7].Rows[0]["count"].ToString() + " NOA Reassessment Required</font></a><br><br>");
            }
            if (ds.Tables[8].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=noareactivationrequests><font color=green>" + ds.Tables[8].Rows[0]["count"].ToString() + " NOA Reactivation Requests</font></a><br><br>");
            }
            if (ds.Tables[11].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=noareassessmentrequests><font color=citrusgreen>" + ds.Tables[11].Rows[0]["count"].ToString() + " NOA Reassessment Requests</font></a><br><br>");
            }
            if (ds.Tables[10].Rows[0]["count"].ToString() != "0")
            {
                request.Append("<li><a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=NOAChangeTreatmentRequests><font color=purple>" + ds.Tables[10].Rows[0]["count"].ToString() + " Treatment Change Requests</font></a><br><br>");
            }
            request.Append("<hr><b><font color=gray>Past NOA Requests</font></b><br><br>");
            request.Append("<a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=resolvedNOARequests>Resolved Requests</a> |  <a href=DataDisplay.aspx?choice=" + uid.ToString() + "&dset=sentNOARequests>Requests I Sent</a>");
            
            return request.ToString();
        }
		//**********************************************************************************************************************
		public DataSet getTMFDatasets(string dset)
		{
			DataSet ds;

			SqlParameter[] arrParams = new SqlParameter[1];
			
			arrParams[0] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
			arrParams[0].Value = dset;

			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getTMFDatasets2", arrParams);
			return ds;
        }
		//**********************************************************************************************************************
		public void TrustedLogin(string uName)
		{
			this.Username = uName;

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@Username", SqlDbType.NVarChar, 50);
			arrParams[0].Value = this.Username;

			arrParams[1] = new SqlParameter("@pword", SqlDbType.NVarChar, 50);
			arrParams[1].Direction = ParameterDirection.Output;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_TrustedLogin", arrParams);

			this.Password = arrParams[1].Value.ToString();
		}

		//**********************************************************************************************************************
		private void LogUser(string uPlatform, string uIP)
		{
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = this.UserID;

			arrParams[1] = new SqlParameter("@Platform", SqlDbType.NVarChar, 50);
			arrParams[1].Value = uPlatform;

			arrParams[2] = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 50);
			arrParams[2].Value = uIP;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_LogUser", arrParams);
		}
		//**********************************************************************************************************************
		public string GetUserInfo()
		{
			SqlParameter arrParams = new SqlParameter();
			arrParams = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams.Value = this.UserID;
			string Uinfo = "";

			DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetUserInfo", arrParams);
			if(ds.Tables[0].Rows.Count > 0)
			{
				Uinfo = "From: " + ds.Tables[0].Rows[0]["firstname"].ToString() + " " + ds.Tables[0].Rows[0]["lastname"].ToString();
				Uinfo += "\nEmail: " + ds.Tables[0].Rows[0]["email"].ToString();
                Uinfo += "\nCountry: " + ds.Tables[0].Rows[0]["countryname"].ToString();
			}
			else if(ds.Tables[1].Rows.Count > 0)
			{
				Uinfo = "From: " + ds.Tables[1].Rows[0]["clinicname"].ToString();
				Uinfo += "\nEmail: " + ds.Tables[1].Rows[0]["email"].ToString();
			}
			return Uinfo;
		}
		
		//**********************************************************************************************************************
		public DataSet getRoleList()
		{
			DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetRoleList");
			return ds;
		}
		//**********************************************************************************************************************
		public void EmailPassword(string uname, string email)
		{
			if(uname == "")
			{
				uname = "XXX";
			}
			if(email == "")
			{
				email = "XXX";
			}
			SqlParameter[] arrParams = new SqlParameter[2];
			GIPAP_Objects.Email myEmail = new Email();
			
			arrParams[0] = new SqlParameter("@UserName", SqlDbType.VarChar, 20);
			arrParams[0].Value = uname;

			arrParams[1] = new SqlParameter("@Email", SqlDbType.VarChar, 200);
			arrParams[1].Value = email;

			DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_EmailPassword", arrParams);

			if(ds.Tables[0].Rows.Count == 1)
			{
				myEmail.SendPasswordEmail(ds.Tables[0].Rows[0]["username"].ToString(), ds.Tables[0].Rows[0]["password"].ToString(), ds.Tables[0].Rows[0]["email"].ToString());
			}
			else if(ds.Tables[1].Rows.Count == 1 && ds.Tables[0].Rows.Count == 0)
			{
				myEmail.SendPasswordEmail(ds.Tables[1].Rows[0]["username"].ToString(), ds.Tables[1].Rows[0]["password"].ToString(), ds.Tables[1].Rows[0]["email"].ToString() + "; " + ds.Tables[1].Rows[0]["adminemail"].ToString());
            }
            else if (ds.Tables[2].Rows.Count == 1)
            {
                if (Convert.ToDateTime(ds.Tables[2].Rows[0]["expiredate"]) < DateTime.Today)
                {
                    throw new Exception("The user account password has expired.  Please email gipap@themaxfoundation.org for help.");
                }
                else
                {
                    myEmail.SendPasswordEmail(ds.Tables[2].Rows[0]["username"].ToString(), ds.Tables[2].Rows[0]["password"].ToString(), ds.Tables[2].Rows[0]["email"].ToString() + "; " + ds.Tables[2].Rows[0]["adminemail"].ToString());
                }
            }
            else if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
			{
				throw new Exception("Unrecognized User");
			}
            else if (ds.Tables[0].Rows.Count > 1 || ds.Tables[1].Rows.Count > 1 || ds.Tables[2].Rows.Count > 1)
			{
				throw new Exception("Not A Specific User");
			}
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

			GIPAP_Objects.Email myEmail = new Email();
			myEmail.To = "pat.garcia.gonzalez@themaxfoundation.org";
            //myEmail.CC = "darlene.lee@themaxfoundation.org";
			myEmail.From = "gipap@themaxfoundation.org";
			myEmail.Subject = "System Change Request";
			myEmail.Message = "Change Request From: " + this.Username + "\n\n" + "Request: " + request;
			myEmail.Send(this.Username);
		}
		//**********************************************************************************************************************
		public void ApproveChangeRequest(int requestid, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@ChangeRequestID", SqlDbType.Int);
			arrParams[0].Value = requestid;

			arrParams[1] = new SqlParameter("@ApproveNote", SqlDbType.Text);
			arrParams[1].Value = note;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ApproveChangeRequest", arrParams);
		}
		//**********************************************************************************************************************
		public void DenyChangeRequest(int requestid, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@ChangeRequestID", SqlDbType.Int);
			arrParams[0].Value = requestid;

			arrParams[1] = new SqlParameter("@ApproveNote", SqlDbType.Text);
			arrParams[1].Value = note;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DenyChangeRequest", arrParams);
		}
		//**************************************************************************************************************
		public void AddCaseNoteAlert(int patientid, string note, DateTime edate)
		{
			SqlParameter[] arrParams = new SqlParameter[4];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = patientid;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = this.Username;

			arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
			arrParams[2].Value = note;

			arrParams[3] = new SqlParameter("@EffectiveDate", SqlDbType.DateTime);
			arrParams[3].Value = edate;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateCaseNoteAlert", arrParams);
		}
		//**********************************************************************************************************************
		public void RemoveCaseNoteAlert(int cID)
		{
			SqlParameter[] arrParams = new SqlParameter[1];
			
			arrParams[0] = new SqlParameter("@CaseNoteID", SqlDbType.Int);
			arrParams[0].Value = cID;

			/*arrParams[1] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[1].Direction = ParameterDirection.Output;*/

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_RemoveCaseNoteAlert", arrParams);

			//return (int)arrParams[1].Value;
		}
		//**********************************************************************************************************************
		public void ResolveChangeRequest(int requestid, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[3];
			
			arrParams[0] = new SqlParameter("@ChangeRequestID", SqlDbType.Int);
			arrParams[0].Value = requestid;

			arrParams[1] = new SqlParameter("@ResolvedBy", SqlDbType.VarChar, 50);
			arrParams[1].Value = this.Username;

			arrParams[2] = new SqlParameter("@ResolveNote", SqlDbType.Text);
			arrParams[2].Value = note;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ResolveChangeRequest", arrParams);
		}
		//**********************************************************************************************************************
		public void CreateMessage(string message, int rid,int cid)
		{
			SqlParameter[] arrParams = new SqlParameter[4];
			
			arrParams[0] = new SqlParameter("@RoleID", SqlDbType.Int);
			arrParams[0].Value = rid;

			arrParams[1] = new SqlParameter("@Message", SqlDbType.VarChar, 2000);
			arrParams[1].Value = message;

			arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
			arrParams[2].Value = this.Username;

            arrParams[3] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[3].Value = cid;


			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateMessage", arrParams);
		}


        //**********************************************************************************************************************
        public void UpdateMessage(int id,string message)
        {
            SqlParameter[] arrParams = new SqlParameter[3];
            
            arrParams[0] = new SqlParameter("@Id", SqlDbType.Int);
			arrParams[0].Value = id;

            arrParams[1] = new SqlParameter("@Message", SqlDbType.VarChar,2000);
            arrParams[1].Value = message;

            arrParams[2] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
            arrParams[2].Value = this.Username;


            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateMessage", arrParams);
        }
        //**********************************************************************************************************************
        public void CreateSharedFile(string fileName, string filePath)
        {
            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@FileName", SqlDbType.NVarChar,100);
            arrParams[0].Value = fileName;

            arrParams[1] = new SqlParameter("@FilePath", SqlDbType.NVarChar);
            arrParams[1].Value = filePath;

            arrParams[2] = new SqlParameter("@Createdby", SqlDbType.VarChar,20);
            arrParams[2].Value = this.Username;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateSharedFile", arrParams);
        }
        //**********************************************************************************************************************
        public DataSet NPSPatientReport()
        {
            string connNPS = ConfigurationSettings.AppSettings["ConnNPS"];
            DataSet ds;
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[0].Value = 0;

            arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
            arrParams[1].Value = "allpatients";

            ds = SqlHelper.ExecuteDataset(connNPS, CommandType.StoredProcedure, "spr_GetUserDataSets2", arrParams);
            return ds;
        }

        //**********************************************************************************************************************
        public void UpdateSharedFile(string fileName, string filePath,int fileid)
        {
            SqlParameter[] arrParams = new SqlParameter[4];

            arrParams[0] = new SqlParameter("@FileName", SqlDbType.NVarChar, 100);
            arrParams[0].Value = fileName;

            arrParams[1] = new SqlParameter("@FilePath", SqlDbType.NVarChar);
            arrParams[1].Value = filePath;

            arrParams[2] = new SqlParameter("@FileId", SqlDbType.Int);
            arrParams[2].Value = fileid;

            arrParams[3] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar,20);
            arrParams[3].Value = this.Username;


            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateSharedFile", arrParams);
        }
        //**********************************************************************************************************************
        public DataTable GIPAPActivity(DateTime startdate, DateTime enddate, int regionid, int subregionid, string prog)
        {
            SqlParameter[] arrParams = new SqlParameter[5];

            arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            arrParams[0].Value = startdate;

            arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
            arrParams[1].Value = enddate.AddDays(1);

            arrParams[2] = new SqlParameter("@RegionID", SqlDbType.Int);
            arrParams[2].Value = regionid;

            arrParams[3] = new SqlParameter("@SubRegionID", SqlDbType.Int);
            arrParams[3].Value = subregionid;

            arrParams[4] = new SqlParameter("@Program", SqlDbType.NVarChar, 50);
            arrParams[4].Value = prog;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GIPAPActivityLINQ", arrParams);

            DataTable dtReport = new DataTable();
            DataColumn column;
            // Create new DataColumn, set DataType, ColumnName and add to DataTable.                
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Region";
            dtReport.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "SubRegion";
            dtReport.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Country";
            dtReport.Columns.Add(column);
            //approvals
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "Approvals";
            dtReport.Columns.Add(column);
            //reapps
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Reapprovals";
            dtReport.Columns.Add(column);
            //deny
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Denials";
            dtReport.Columns.Add(column);
            //close
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "Closures";
            dtReport.Columns.Add(column);

            //dtReport.Merge(ds.Tables[0]); //approvals
            //try creating empty row
            DataTable dtEmpty = new DataTable();
            DataColumn columnEmpty;
            //is null
            columnEmpty = new DataColumn();
            columnEmpty.DataType = Type.GetType("System.Int32");
            columnEmpty.ColumnName = "ct";
            dtEmpty.Columns.Add(columnEmpty);
            DataRow rowEmpty = dtEmpty.NewRow();
            rowEmpty["ct"] = 0;

            /*INNER JOIN!!!var results = from table0 in ds.Tables[0].AsEnumerable()
                          join table1 in ds.Tables[1].AsEnumerable() on (string)table0["countryname"] equals (string)table1["countryname"]
                          join table2 in ds.Tables[2].AsEnumerable() on (string)table0["countryname"] equals (string)table2["countryname"]
                          join table3 in ds.Tables[3].AsEnumerable() on (string)table0["countryname"] equals (string)table3["countryname"]
                          join table4 in ds.Tables[4].AsEnumerable() on (string)table0["countryname"] equals (string)table4["countryname"]*/
            /*left outer join*/
            var results = from table0 in ds.Tables[0].AsEnumerable()
                          join table1 in ds.Tables[1].AsEnumerable() on (string)table0["countryname"] equals (string)table1["countryname"]
                          into DataGroup1
                          from row1 in DataGroup1.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table2 in ds.Tables[2].AsEnumerable() on (string)table0["countryname"] equals (string)table2["countryname"]
                          into DataGroup2
                          from row2 in DataGroup2.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table3 in ds.Tables[3].AsEnumerable() on (string)table0["countryname"] equals (string)table3["countryname"]
                          into DataGroup3
                          from row3 in DataGroup3.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table4 in ds.Tables[4].AsEnumerable() on (string)table0["countryname"] equals (string)table4["countryname"]
                          into DataGroup4
                          from row4 in DataGroup4.DefaultIfEmpty<DataRow>(rowEmpty)
                          select new
                          {
                              r = (string)table0["region"],
                              s = (string)table0["subregion"],
                              c = (string)table0["countryname"],
                              app = (int)row1["ct"],
                              reapp = (int)row2["ct"],
                              den = (int)row3["ct"],
                              close = (int)row4["ct"]
                              /*reapp = (int)table2["ct"],
                              den = (int)table3["ct"],
                              close = (int)table4["ct"]*/
                          };
            foreach (var item in results)
            {
                // Console.WriteLine(String.Format("ID = {0}, ColX = {1}, ColY = {2}, ColZ = {3}", item.CustID, item.ColX, item.ColY, item.ColZ));
                DataRow row = dtReport.NewRow();
                row["Region"] = item.r;
                row["SubRegion"] = item.s;
                row["Country"] = item.c;
                row["Approvals"] = item.app;
                row["Reapprovals"] = item.reapp;
                row["Denials"] = item.den;
                row["Closures"] = item.close;

                dtReport.Rows.Add(row);
            }
            DataTable dtTots = dtReport.Clone();
            DataRow totRow = dtTots.NewRow();
            totRow["Region"] = "-";
            totRow["SubRegion"] = "-";
            totRow["Country"] = "Totals";
            totRow["Approvals"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Approvals"));
            totRow["Reapprovals"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Reapprovals"));
            totRow["Denials"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Denials"));
            totRow["Closures"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Closures"));
            dtTots.Rows.Add(totRow);
            dtTots.Merge(dtReport);
            return dtTots;
        }
        //**********************************************************************************************************************
		public DataSet RegionalDenialsBreakdown(DateTime startdate, DateTime enddate, int regionid, int subregionid)
		{
			SqlParameter[] arrParams = new SqlParameter[5];
			
			arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[0].Value = startdate;

			arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[1].Value = enddate.AddDays(1);

			arrParams[2] = new SqlParameter("@RegionID", SqlDbType.Int);
			arrParams[2].Value = regionid;

			arrParams[3] = new SqlParameter("@SubRegionID", SqlDbType.Int);
			arrParams[3].Value = subregionid;

			arrParams[4] = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
			arrParams[4].Value = this.Role;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_RegionalDenialsBreakdown", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet RegionalClosuresBreakdown(DateTime startdate, DateTime enddate, int regionid, int subregionid)
		{
			SqlParameter[] arrParams = new SqlParameter[5];
			
			arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[0].Value = startdate;

			arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[1].Value = enddate.AddDays(1);

			arrParams[2] = new SqlParameter("@RegionID", SqlDbType.Int);
			arrParams[2].Value = regionid;

			arrParams[3] = new SqlParameter("@SubRegionID", SqlDbType.Int);
			arrParams[3].Value = subregionid;

			arrParams[4] = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
			arrParams[4].Value = this.Role;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_RegionalClosuresBreakdown", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet RegionalApprovalsBreakdown(DateTime startdate, DateTime enddate, int regionid, int subregionid)
		{
			SqlParameter[] arrParams = new SqlParameter[5];
			
			arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[0].Value = startdate;

			arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[1].Value = enddate.AddDays(1);

			arrParams[2] = new SqlParameter("@RegionID", SqlDbType.Int);
			arrParams[2].Value = regionid;

			arrParams[3] = new SqlParameter("@SubRegionID", SqlDbType.Int);
			arrParams[3].Value = subregionid;

			arrParams[4] = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
			arrParams[4].Value = this.Role;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_RegionalApprovalsBreakdown", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet MyReports()
		{
			SqlParameter arrParams = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams.Value = this.UserID;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getDynamicReports", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet DiagnosisReport(int regionid, int subregionid)
		{
			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@RegionID", SqlDbType.Int);
			arrParams[0].Value = regionid;

			arrParams[1] = new SqlParameter("@SubRegionID", SqlDbType.Int);
			arrParams[1].Value = subregionid;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_DiagnosisReport", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet DosageReport(int regionid, int subregionid)
		{
			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@RegionID", SqlDbType.Int);
			arrParams[0].Value = regionid;

			arrParams[1] = new SqlParameter("@SubRegionID", SqlDbType.Int);
			arrParams[1].Value = subregionid;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_DosageReport", arrParams);
		}
		//**********************************************************************************************************************
		public string DosageHistory(int rID,int srID)
		{
			string dh = "";
			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@RegionID", SqlDbType.Int);
			arrParams[0].Value = rID;

			arrParams[1] = new SqlParameter("@SubRegionID", SqlDbType.Int);
			arrParams[1].Value = srID;

			DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_DosageHistory", arrParams);

			string reg = ds.Tables[0].Rows[0]["region"].ToString();
			dh += "<font color=steelblue><b>" + reg + "</b></font>";
			string subreg = ds.Tables[0].Rows[0]["subregion"].ToString();
			dh += "<br><br><b>" + subreg + "</b>";
			
			dh += "<br><br><font color=steelblue>" + ds.Tables[0].Rows[0]["pin"].ToString() + "</font><br>";
			dh += "<b>Diagnosis: </b>" + ds.Tables[0].Rows[0]["diagnosis"].ToString() + "<br>";
			try
			{
				DateTime ddate = Convert.ToDateTime(ds.Tables[0].Rows[0]["glivecstartdate"].ToString());
				dh += "<b>Glivec Start Date: </b>" + ddate.Day.ToString() + " " + ddate.ToString("y") + "<br>";
			}
			catch
			{
				dh += "<b>Glivec Start Date: </b>Not Recorded";
			}
			dh += "<b>Original Requested Dosage: </b>" + ds.Tables[0].Rows[0]["originalrequesteddosage"].ToString() + "<br>";
			dh += "<b>Original Approved Dosage: </b>" + ds.Tables[0].Rows[0]["originalapproveddosage"].ToString() + "<br>";
			dh += "<b>Dosage History</b>";
			if(ds.Tables[0].Rows[0]["currentdosage"].ToString() != "")
			{
				dh += "<br>" + ds.Tables[0].Rows[0]["currentdosage"].ToString();
				try
				{
					DateTime sdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["startdate"].ToString());
					dh += " " + sdate.Day.ToString() + " " + sdate.ToString("y");
				}
				catch{}
			}
			if(ds.Tables[0].Rows[0]["dosechange"].ToString() != "")
			{
				dh += "<br>" + ds.Tables[0].Rows[0]["dosechange"].ToString();
				try
				{
					DateTime sdate = Convert.ToDateTime(ds.Tables[0].Rows[0]["changedate"].ToString());
					dh += " " + sdate.Day.ToString() + " " + sdate.ToString("y");
				}
				catch{}
			}
			string pin = ds.Tables[0].Rows[0]["pin"].ToString();
			for(int i=1; i<ds.Tables[0].Rows.Count; i++)
			{
				if(ds.Tables[0].Rows[i]["region"].ToString() != reg)
				{
					reg = ds.Tables[0].Rows[i]["region"].ToString();
					dh += "<font color=steelblue><b>" + reg + "</b></font>";
				}
				if(ds.Tables[0].Rows[i]["subregion"].ToString() != subreg)
				{
					subreg = ds.Tables[0].Rows[i]["subregion"].ToString();
					dh += "<br><br><b>" + subreg + "</b>";
				}
				if(ds.Tables[0].Rows[i]["pin"].ToString() != pin)
				{
					pin = ds.Tables[0].Rows[i]["pin"].ToString();
					dh += "<br><br><font color=steelblue>" + ds.Tables[0].Rows[i]["pin"].ToString() + "</font><br>";
					dh += "<b>Diagnosis: </b>" + ds.Tables[0].Rows[i]["diagnosis"].ToString() + "<br>";
					try
					{
						DateTime ddate = Convert.ToDateTime(ds.Tables[0].Rows[i]["glivecstartdate"].ToString());
						dh += "<b>Glivec Start Date: </b>" + ddate.Day.ToString() + " " + ddate.ToString("y") + "<br>";
					}
					catch
					{
						dh += "<b>Glivec Start Date: </b>Not Recorded";
					}
					dh += "<b>Original Requested Dosage: </b>" + ds.Tables[0].Rows[i]["originalrequesteddosage"].ToString() + "<br>";
					dh += "<b>Original Approved Dosage: </b>" + ds.Tables[0].Rows[i]["originalapproveddosage"].ToString() + "<br>";
					dh += "<b>Dosage History</b>";
				}
				if(ds.Tables[0].Rows[i]["currentdosage"].ToString() != "")
				{
					dh += "<br>" + ds.Tables[0].Rows[i]["currentdosage"].ToString();
					try
					{
						DateTime sdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["startdate"].ToString());
						dh += " " + sdate.Day.ToString() + " " + sdate.ToString("y");
					}
					catch{}
				}
				if(ds.Tables[0].Rows[i]["dosechange"].ToString() != "")
				{
					dh += "<br>" + ds.Tables[0].Rows[i]["dosechange"].ToString();
					try
					{
						DateTime sdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["changedate"].ToString());
						dh += " " + sdate.Day.ToString() + " " + sdate.ToString("y");
					}
					catch{}
				}
			}
			return dh;
		}
		//**********************************************************************************************************************
		public DataSet QuickSearch(string pin, string first, string last, string noa)
		{
			DataSet ds;

			SqlParameter[] arrParams = new SqlParameter[7];
			
			arrParams[0] = new SqlParameter("@PIN", SqlDbType.VarChar, 50);
			arrParams[0].Value = pin;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
			arrParams[1].Value = first;

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
			arrParams[2].Value = last;

			arrParams[3] = new SqlParameter("@Role", SqlDbType.VarChar, 50);
			arrParams[3].Value = this.Role;

			arrParams[4] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[4].Value = this.UserID;

            arrParams[5] = new SqlParameter("@NOA", SqlDbType.NVarChar, 10);
            arrParams[5].Value = noa;

            arrParams[6] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[6].Value = this.CountryID;

			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_QuickSearch2", arrParams);
			return ds;
		}
        //**********************************************************************************************************************
        public DataTable GIPAPTotals(string prog)
        {
            SqlParameter arrParams = new SqlParameter("@Program", SqlDbType.NVarChar, 50);
            arrParams.Value = prog;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GIPAPTotalsLINQ", arrParams);

            DataTable dtReport = new DataTable();
            DataColumn column;
            // Create new DataColumn, set DataType, ColumnName and add to DataTable.                
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Country";
            dtReport.Columns.Add(column);
            //active
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "Active";
            dtReport.Columns.Add(column);
            //c
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Closed";
            dtReport.Columns.Add(column);
            //deny
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Denied";
            dtReport.Columns.Add(column);
            //pend
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "Pending";
            dtReport.Columns.Add(column);
            //perc
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "% of Active";
            dtReport.Columns.Add(column);

            //dtReport.Merge(ds.Tables[0]); //approvals
            //try creating empty row
            DataTable dtEmpty = new DataTable();
            DataColumn columnEmpty;
            //is null
            columnEmpty = new DataColumn();
            columnEmpty.DataType = Type.GetType("System.Int32");
            columnEmpty.ColumnName = "ct";
            dtEmpty.Columns.Add(columnEmpty);
            columnEmpty = new DataColumn();
            columnEmpty.DataType = Type.GetType("System.String");
            columnEmpty.ColumnName = "perc";
            dtEmpty.Columns.Add(columnEmpty);
            DataRow rowEmpty = dtEmpty.NewRow();
            rowEmpty["ct"] = 0;
            rowEmpty["perc"] = "0";

            /*left outer join*/
            var results = from table0 in ds.Tables[0].AsEnumerable()
                          join table1 in ds.Tables[1].AsEnumerable() on (string)table0["countryname"] equals (string)table1["countryname"]
                          into DataGroup1
                          from row1 in DataGroup1.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table2 in ds.Tables[2].AsEnumerable() on (string)table0["countryname"] equals (string)table2["countryname"]
                          into DataGroup2
                          from row2 in DataGroup2.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table3 in ds.Tables[3].AsEnumerable() on (string)table0["countryname"] equals (string)table3["countryname"]
                          into DataGroup3
                          from row3 in DataGroup3.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table4 in ds.Tables[4].AsEnumerable() on (string)table0["countryname"] equals (string)table4["countryname"]
                          into DataGroup4
                          from row4 in DataGroup4.DefaultIfEmpty<DataRow>(rowEmpty)

                          orderby row1.Field<Int32>("ct") descending
                          select new
                          {
                              c = (string)table0["countryname"],
                              app = (int)row1["ct"],
                              perc = (string)row1["perc"],
                              close = (int)row2["ct"],
                              den = (int)row3["ct"],
                              pend = (int)row4["ct"]
                          };
            foreach (var item in results)
            {
                // Console.WriteLine(String.Format("ID = {0}, ColX = {1}, ColY = {2}, ColZ = {3}", item.CustID, item.ColX, item.ColY, item.ColZ));
                DataRow row = dtReport.NewRow();
                row["Country"] = item.c;
                row["Active"] = item.app;
                row["Closed"] = item.close;
                row["Denied"] = item.den;
                row["Pending"] = item.pend;
                row["% of Active"] = item.perc;

                dtReport.Rows.Add(row);
            }
            DataTable dtTots = dtReport.Clone();
            DataRow totRow = dtTots.NewRow();
            totRow["Country"] = "Totals";
            totRow["Active"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Active"));
            totRow["Closed"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Closed"));
            totRow["Denied"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Denied"));
            totRow["Pending"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Pending"));
            totRow["% of Active"] = "100%";
            dtTots.Rows.Add(totRow);
            dtTots.Merge(dtReport);
            return dtTots;
        }
        //**********************************************************************************************************************
		public DataSet GIPAPTotals(DateTime reportDate)
		{
			SqlParameter[] arrParams = new SqlParameter[3];
			
			arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[0].Value = reportDate;

			arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[1].Value = reportDate.AddDays(1);

			arrParams[2] = new SqlParameter("@Role", SqlDbType.VarChar, 50);
			arrParams[2].Value = this.Role;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_HistoricalGIPAPTotals", arrParams);
		}
		//*********************************************************************************************************************
		public DataSet getApplications(DateTime appDate)
		{
			SqlParameter[] arParams = new SqlParameter[3];

			arParams[0] = new SqlParameter("@UserID", SqlDbType.Int); 
			arParams[0].Value = this.UserID;
			
			arParams[1] = new SqlParameter("@StartDate", SqlDbType.DateTime); 
			arParams[1].Value = appDate;

			arParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime); 
			arParams[2].Value = appDate.AddDays(1);
			
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getApplications2", arParams);
		}
		//**********************************************************************************************************************
		public DataSet GIPAPWeeklyTotals()
		{
			SqlParameter arrParams = new SqlParameter();
			
			arrParams = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
			arrParams.Value = this.Role;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GIPAPWeeklyTotals", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet GetRecentPosts()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetRecentPosts");
		}
		//**********************************************************************************************************************
		public DataSet GetDeletedApplications()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetDeletedApplications");
		}
		//**********************************************************************************************************************
		public DataSet GetThread(int threadID)
		{
			SqlParameter[] arParams = new SqlParameter[2];

			arParams[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 50); 
			arParams[0].Value = this.Username;
			
			arParams[1] = new SqlParameter("@ThreadID", SqlDbType.Int); 
			arParams[1].Value = threadID;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetThread", arParams);
		}
		//**********************************************************************************************************************
		public DataSet GetForums(int forumID)
		{
			SqlParameter arrParams = new SqlParameter();

			arrParams = new SqlParameter("@ForumID", SqlDbType.Int);
			arrParams.Value = forumID;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetForums", arrParams);
		}
		//**********************************************************************************************************************
		public void CreateThread(string subject, string message, int forumID)
		{
			//Update the GCC information.
			SqlParameter[] arrParams = new SqlParameter[4];

			arrParams[0] = new SqlParameter("@ForumID", SqlDbType.Int);
			arrParams[0].Value = forumID;

			arrParams[1] = new SqlParameter("@Subject", SqlDbType.NVarChar, 200);
			arrParams[1].Value = subject;

			arrParams[2] = new SqlParameter("@Message", SqlDbType.Text);
			arrParams[2].Value = message;

			arrParams[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 100);
			arrParams[3].Value = this.Username;

			SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CreateThread", arrParams);
		}
		//**********************************************************************************************************************
		public void CreateReply(string message, int threadID)
		{
			//Update the GCC information.
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@ThreadID", SqlDbType.Int);
			arrParams[0].Value = threadID;

			arrParams[1] = new SqlParameter("@Message", SqlDbType.Text);
			arrParams[1].Value = message;

			arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 100);
			arrParams[2].Value = this.Username;

			SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CreateReply", arrParams);
		}
		//**********************************************************************************************************************
		public string HistoricalData(DateTime dt, System.Web.HttpServerUtility Server)
		{
			string hd = "";
			string day;
			string month;
			if(dt.Day.ToString().Length == 1)
			{
				day = "0" + dt.Day.ToString();
			}
			else
			{
				day = dt.Day.ToString();
			}
			if(dt.Month.ToString().Length == 1)
			{
				month = "0" + dt.Month.ToString();
			}
			else
			{
				month = dt.Month.ToString();
			}
			string strPath = "HistoricalData/" + dt.Year.ToString() + "-" + month + "/" + dt.Year.ToString() + "-" + month + "-" + day + "/GIPAPTotals.xls";
			string mPath = Server.MapPath(strPath);
			if(File.Exists(mPath))
			{
				hd += "<li><a href=" + strPath + ">Click Here For GIPAP Totals Report</a>";
				strPath = "HistoricalData/" + dt.Year.ToString() + "-" + month + "/" + dt.Year.ToString() + "-" + month + "-" + day + "/GIPAPWeeklyTotals.xls";
				mPath = Server.MapPath(strPath);
				if(File.Exists(mPath))
				{
					hd += "<li><a href=" + strPath + ">Click Here For GIPAP Weekly Totals Report</a>";
				}
			}
			else
			{
				hd += "<font color=red>No Information Available for that Date. The following Reports are available from that period:<br><br></font>";
				for(int i=1; i<=7; i++)
				{
					strPath = "HistoricalData/" + dt.AddDays(-i).Year.ToString() + "-" + dt.AddDays(-i).Month.ToString() + "/" + dt.AddDays(-i).Year.ToString() + "-" + dt.AddDays(-i).Month.ToString() + "-" + dt.AddDays(-i).Day.ToString() + "/GIPAPTotals.xls";
					mPath = Server.MapPath(strPath);
					if(File.Exists(mPath))
					{
						hd += "<li><a href=" + strPath + ">Click Here For GIPAP Totals Report</a> (" + dt.AddDays(-i).Year.ToString() + "-" + dt.AddDays(-i).Month.ToString() + "-" + dt.AddDays(-i).Day.ToString() + ")";
						strPath = "HistoricalData/" + dt.AddDays(-i).Year.ToString() + "-" + dt.AddDays(-i).Month.ToString() + "/" + dt.AddDays(-i).Year.ToString() + "-" + dt.AddDays(-i).Month.ToString() + "-" + dt.AddDays(-i).Day.ToString() + "/GIPAPWeeklyTotals.xls";
						mPath = Server.MapPath(strPath);
						if(File.Exists(mPath))
						{
							hd += "<li><a href=" + strPath + ">Click Here For GIPAP Weekly Totals Report</a> (" + dt.AddDays(-i).Year.ToString() + "-" + dt.AddDays(-i).Month.ToString() + "-" + dt.AddDays(-i).Day.ToString() + ")";
							break;
						}
						break;
					}
				}
				for(int i=1; i<=7; i++)
				{
					strPath = "HistoricalData/" + dt.AddDays(i).Year.ToString() + "-" + dt.AddDays(i).Month.ToString() + "/" + dt.AddDays(i).Year.ToString() + "-" + dt.AddDays(i).Month.ToString() + "-" + dt.AddDays(i).Day.ToString() + "/GIPAPTotals.xls";
					mPath = Server.MapPath(strPath);
					if(File.Exists(mPath))
					{
						hd += "<li><a href=" + strPath + ">Click Here For GIPAP Totals Report</a> (" + dt.AddDays(i).Year.ToString() + "-" + dt.AddDays(i).Month.ToString() + "-" + dt.AddDays(i).Day.ToString() + ")";
						strPath = "HistoricalData/" + dt.AddDays(i).Year.ToString() + "-" + dt.AddDays(i).Month.ToString() + "/" + dt.AddDays(i).Year.ToString() + "-" + dt.AddDays(i).Month.ToString() + "-" + dt.AddDays(i).Day.ToString() + "/GIPAPWeeklyTotals.xls";
						mPath = Server.MapPath(strPath);
						if(File.Exists(mPath))
						{
							hd += "<li><a href=" + strPath + ">Click Here For GIPAP Weekly Totals Report</a> (" + dt.AddDays(i).Year.ToString() + "-" + dt.AddDays(i).Month.ToString() + "-" + dt.AddDays(i).Day.ToString() + ")";
							break;
						}
						break;
					}
				}
			}
			return hd;
		}
		//**************************************************************************************************************
		public void UpdateMOU(System.Web.UI.WebControls.CheckBoxList lb)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
			arrParams[0].Value = this.Username;

			for(int i=0; i<lb.Items.Count; i++)
			{
				if(lb.Items[i].Selected)
				{
					arrParams[1] = new SqlParameter("@MOUID", SqlDbType.Int);
					arrParams[1].Value =  Convert.ToInt32(lb.Items[i].Value);

					SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateMOUList", arrParams);
				}
			}
		}
		//**********************************************************************************************************************
		public DataSet MOUStatusReport()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_MOUReport");
		}			
		//**********************************************************************************************************************
		public string MOUEmails()
		{
			DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_MOUReport");
			string me = "<table width=600 border=0><tr><td><b>Physicians Who Have Signed</b></td></tr><tr><td>";
			for(int i=0; i<ds.Tables[0].Rows.Count; i++)
			{
				me += ds.Tables[0].Rows[i]["email"].ToString() + "; ";
			}
			me += "</td></tr><table width=600 border=0><tr><td><b>Physicians Who Have Not Signed</b></td></tr><tr><td>";
			for(int i=0; i<ds.Tables[1].Rows.Count; i++)
			{
				me += ds.Tables[1].Rows[i]["email"].ToString() + "; ";
			}
			me += "</td></tr></table>";
			return me;
		}
		//**********************************************************************************************************************
		public string MOUReport()
		{
			DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_MOUReport");
			string country = ds.Tables[0].Rows[0]["countryname"].ToString();
			int ccount = 0;
			string mr = "<table width=600 border=0><tr><td colspan=4 bgcolor=silver><b>Physicians Who Have Signed</b>";
			/*if(this.Role == "TMFUser")
			{
				mr += " - <a href=GIPAP.aspx?trgt=mouupdate>Click Here to Update</a>";
			}*/
            mr += "</td></tr><tr bgcolor=silver><td><b>Physician Name</b></td><td><b>Date Signed</b></td><td><b>Ammendments Signed</b></td><td><b>Email</b></td></tr>";
			mr += "<tr><td><b>" + country + " - " + ds.Tables[2].Rows[ccount]["ccount"].ToString() + "</b></td></tr>";
			for(int i=0; i<ds.Tables[0].Rows.Count; i++)
			{
				if(ds.Tables[0].Rows[i]["countryname"].ToString() != country)
				{
					ccount++;
					country = ds.Tables[0].Rows[i]["countryname"].ToString();
					mr += "<tr><td><b>" + country + " - " + ds.Tables[2].Rows[ccount]["ccount"].ToString() + "</b></td></tr>";
				}
				mr += "<tr><td>" + ds.Tables[0].Rows[i]["physicianname"].ToString() + "</td>";
                mr += "<td>" + ds.Tables[0].Rows[i]["moudate"].ToString() + "</td>";
                mr += "<td>" + ds.Tables[0].Rows[i]["ammendments"].ToString() + "</td>";
				mr += "<td>" + ds.Tables[0].Rows[i]["email"].ToString() + "</td></tr>";
			}
			//mr += "<tr><td colspan=4><a href=GIPAP.aspx?trgt=reports&choice=mouemails>Click here for an email list</a></td></tr>";
			mr += "</table>";

			country = ds.Tables[1].Rows[0]["countryname"].ToString();
			ccount = 0;
			mr += "<table width=600 border=0><tr><td colspan=3 bgcolor=silver><b>Physicians Who Have Not Signed</b></td></tr>";
			mr += "<tr bgcolor=silver><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Email</b></td></tr>";
			mr += "<tr><td><b>" + country + " - " + ds.Tables[3].Rows[ccount]["ccount"].ToString() + "</b></td></tr>";
			for(int i=0; i<ds.Tables[1].Rows.Count; i++)
			{
				if(ds.Tables[1].Rows[i]["countryname"].ToString() != country)
				{
					ccount++;
					country = ds.Tables[1].Rows[i]["countryname"].ToString();
					mr += "<tr><td><b>" + country + " - " + ds.Tables[3].Rows[ccount]["ccount"].ToString() + "</b></td></tr>";
				}
				mr += "<tr><td>" + ds.Tables[1].Rows[i]["lastname"].ToString() + "</td>";
				mr += "<td>" + ds.Tables[1].Rows[i]["firstname"].ToString() + "</td>";
				mr += "<td>" + ds.Tables[1].Rows[i]["email"].ToString() + "</td></tr>";
			}
			//mr += "<tr><td colspan=3><a href=GIPAP.aspx?trgt=reports&choice=mouemails>Click here for an email list</a></td></tr>";
			mr += "</table>";
			
			return mr;
		}
        //**********************************************************************************************************************
        public DataSet SAEReport(DateTime startdate, DateTime enddate, string prog, int countryid)
        {
            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            arrParams[0].Value = startdate;

            arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
            arrParams[1].Value = enddate.AddDays(1);

            arrParams[2] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[2].Value = countryid;

            string cs = "";
            if (prog == "TIPAP")
            {
                cs = ConfigurationSettings.AppSettings["ConnTIPAP"];
            }
            else if (prog == "MYPAP")
            {
                cs = ConfigurationSettings.AppSettings["ConnMYPAP"];
            }
            else if (prog == "NPS")
            {
                cs = ConfigurationSettings.AppSettings["ConnNPS"];
            }
            else
            {
                cs = connString;
            }

            return SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "spr_SAEReport", arrParams);
        }
        //**********************************************************************************************************************
        public DataTable AEPhysicianRequest(DateTime startdate, DateTime enddate, string prog, bool note, int cid)
        {
            SqlParameter[] arrParams = new SqlParameter[5];

            arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            arrParams[0].Value = startdate;

            arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
            arrParams[1].Value = enddate.AddDays(1);

            arrParams[2] = new SqlParameter("@WNote", SqlDbType.Bit);
            arrParams[2].Value = note;

            arrParams[3] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[3].Value = cid;

            arrParams[4] = new SqlParameter("@NOA", SqlDbType.Bit);

            string cs = "";
            if (prog == "TIPAP")
            {
                cs = ConfigurationSettings.AppSettings["ConnTIPAP"];
                arrParams[4].Value = 0;
            }
            else if (prog == "MYPAP")
            {
                cs = ConfigurationSettings.AppSettings["ConnMYPAP"];
                arrParams[4].Value = 0;
            }
            else
            {
                cs = connString;
                arrParams[4].Value = 0;
                /*if (prog == "NOA")
                {
                    arrParams[4].Value = 1;
                }
                else
                {
                    arrParams[4].Value = 0;
                }*/
            }

            //DataSet ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "spr_AEPhysicianRequests", arrParams);
            DataSet ds = SqlHelper.ExecuteDataset(cs, CommandType.StoredProcedure, "spr_AESourceDocs", arrParams);
            ds.Tables[0].Merge(ds.Tables[1]);
            ds.Tables[0].Merge(ds.Tables[2]);
            ds.Tables[0].Merge(ds.Tables[3]);

            return ds.Tables[0];
        }
		//**********************************************************************************************************************
		public string ReasonReport(string stat, DateTime startdate, DateTime enddate)
		{
			DataSet ds = new DataSet();
			SqlParameter[] arrParams = new SqlParameter[2];
			
			arrParams[0] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[0].Value = startdate;

			arrParams[1] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[1].Value = enddate.AddDays(1);

			if(stat == "Closed")
			{
				ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_ClosedReport", arrParams);
			}
			else if(stat == "Denied")
			{
				ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_DeniedReport", arrParams);
			}
			if(ds.Tables[0].Rows.Count <= 0)
			{
				return "No activity for that time period";
			}
			string cr = "<table width=500>";
			for(int a=0; a<ds.Tables[0].Rows.Count; a++)
			{
				int ktr = a+1;
				cr += "<tr><td><b>" + ktr.ToString() + "</b></td><td>" + ds.Tables[0].Rows[a]["statusreason"].ToString() + "</td></tr>";
			}
			cr += "</table><hr><table border=1 width=600><tr><td><b>Region</b></td><td><b>Sub Region</b></td><td><b>Country</b></td>";

			for(int i=0; i<ds.Tables[0].Rows.Count; i++)
			{
				int ktr = i + 1;
				cr += "<td><strong>" + ktr.ToString() + "</strong></td>";
			}
			cr += "<td><b>Totals</b></td></tr>";

			string country = ds.Tables[1].Rows[0]["countryname"].ToString();
			int placeKtr = 0;

			for(int m=0; m<ds.Tables[1].Rows.Count; m++)
			{
				if(country == "India")
				{
					country = "India";
				}
				if(country != ds.Tables[1].Rows[m]["countryname"].ToString() || m == 0)
				{
					country = ds.Tables[1].Rows[m]["countryname"].ToString();
					cr += "<tr><td>" + ds.Tables[1].Rows[m]["region"].ToString() + "</td>";
					cr += "<td>" + ds.Tables[1].Rows[m]["subregion"].ToString() + "</td>";
					cr += "<td>" + country + "</td>";
					placeKtr = 0;
				}
				for(int w=placeKtr; w<ds.Tables[0].Rows.Count; w++)
				{
					if(ds.Tables[1].Rows[m]["statusreason"].ToString().ToLower() == ds.Tables[0].Rows[w]["statusreason"].ToString().ToLower())
					{
						cr += "<td bgcolor='silver'>" + ds.Tables[1].Rows[m]["CountryCount"].ToString() + "</td>";
						placeKtr++;
						break;
					}
					else
					{
						cr += "<td>0</td>";
						placeKtr++;
					}
				}
				try
				{
					if(country != ds.Tables[1].Rows[m + 1]["countryname"].ToString())
					{
						int endRow = ds.Tables[0].Rows.Count - placeKtr;
						for(int z=0; z<endRow; z++)
						{
							cr += "<td>0</td>";
						}
						for(int ccnt=0; ccnt<ds.Tables[2].Rows.Count; ccnt++)
						{
							if(ds.Tables[2].Rows[ccnt]["countryname"].ToString() == country)
							{
								cr += "<td><b>" + ds.Tables[2].Rows[ccnt]["count"].ToString() + "</b></td>";
								break;
							}
						}
						cr += "</tr>";
					}
				}
				catch
				{
					int endRow = ds.Tables[0].Rows.Count - placeKtr;
					for(int z=0; z<endRow; z++)
					{
						cr += "<td>0</td>";
					}
					for(int ccnt=0; ccnt<ds.Tables[2].Rows.Count; ccnt++)
					{
						if(ds.Tables[2].Rows[ccnt]["countryname"].ToString() == country)
						{
							cr += "<td><b>" + ds.Tables[2].Rows[ccnt]["count"].ToString() + "</b></td>";
							break;
						}
					}
					cr += "</tr>";
				}
			}

			cr+= "<tr><td></td><td></td><td><b>Totals</b></td>";
			for(int last=0; last<ds.Tables[0].Rows.Count; last++)
			{
				cr += "<td><b>" + ds.Tables[0].Rows[last]["count"].ToString() + "</b></td>";
			}
			cr += "<td>X</td></tr></table>";
			return cr;
		}
		//**************************************************************************************************************
		public void Clear()
		{
			this.UserID = 0;
			this.Username = "";
			this.Password = "";
			this.IsAdmin = false;
			this.Disabled = false;
			this.Role = "";
			this.HomePage = "Default.aspx?trgt=home&choice=loginerror";
			this.LeftNav = "websitecontent/LeftNav.ascx";
			this.ErrorMessage = "";
			this.Header = "websitecontent/Header1.ascx";
			this.MOU = false;
			this.ClickCount = 0;
		}

		//**************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			//Populates the objects parameters with the data returned from the database
			this.UserID = (int)(ds.Tables[0].Rows[0]["UserID"]);
			this.Username = (ds.Tables[0].Rows[0]["Username"]).ToString();
            this.Password = (ds.Tables[0].Rows[0]["Password"]).ToString();
            this.FullName = (ds.Tables[0].Rows[0]["fullname"]).ToString();
			this.IsAdmin = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAdmin"]);
            this.IsFileSharingAdmin = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFileSharingAdmin"]);
			this.Disabled = Convert.ToBoolean(ds.Tables[0].Rows[0]["Disabled"]);
            this.Role = ds.Tables[0].Rows[0]["RoleName"].ToString();
            this.CountryID = (int)(ds.Tables[0].Rows[0]["CountryID"]);
            this.ExpireDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["expiredate"]);
            this.RAP = Convert.ToBoolean(ds.Tables[0].Rows[0]["rap"]);
            //default to false
            this.TIPAPPhys = false;

            this.TempID = 0;
            if (this.Role == "TMFUser")
            {
                this.OtherUsers = ds.Tables[1];
            }
            else if(this.Role == "MaxStation")
            {
                //this.Message = ds.Tables[1].Rows[0]["newsmessage"].ToString();
                if (this.CountryID == 76)
                {
                    this.CountryList += "<li><a href='../Country/CountryInfo.aspx?choice=76'>India</a></li>";
                }
                else if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.CountryList += "<li><a href='../Country/CountryInfo.aspx?choice=" + ds.Tables[1].Rows[i]["countryid"].ToString() + "'>" + ds.Tables[1].Rows[i]["countryname"].ToString() + "</a></li>";
                    }
                }
            }
            else if (this.Role == "Novartis")
            {
                if (this.IsAdmin || ds.Tables[1].Rows.Count > 0)
                {
                    this.IsAdmin = true; //rcc are admin
                    this.CountryTable = ds.Tables[2];
                    /*this.CountryList = "<ul><li><a href=''>Regions</a><ul class='Region'><li class=leftcol><h3>Regions</h3>";
                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        this.CountryList += "<a href='../Region/RegionInfo.aspx?choice=" + ds.Tables[3].Rows[i]["regionid"].ToString() + "'>" + ds.Tables[3].Rows[i]["region"].ToString() + "</a>";
                    }
                    this.CountryList += "<h3>Sub Regions</h3>";
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        this.CountryList += "<a href='../SubRegion/SubRegionInfo.aspx?choice=" + ds.Tables[2].Rows[i]["subregionid"].ToString() + "'>" + ds.Tables[2].Rows[i]["subregion"].ToString() + "</a>";
                    }
                    this.CountryList += "</li><li class='leftcol'><h3>Countries</h3>";
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if(i==60 || i==120){
                            this.CountryList += "</li><li class='leftcol'>";
                        }
                        this.CountryList += "<a href='../Country/CountryInfo.aspx?choice=" + ds.Tables[1].Rows[i]["countryid"].ToString() + "'>" + ds.Tables[1].Rows[i]["countryname"].ToString() + "</a>";
                    }
                    this.CountryList += "</li></ul></li></ul>";*/
                }
                else if (ds.Tables[3].Rows.Count > 0) //sub region coord
                {
                    this.CountryTable = ds.Tables[3];
                    /*this.CountryList = "<ul><li><a href=''>Regions</a><ul class='Region'><li class=leftcol><h3>Sub Regions</h3>";
                    for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                    {
                        this.CountryList += "<a href='../SubRegion/SubRegionInfo.aspx?choice=" + ds.Tables[5].Rows[i]["subregionid"].ToString() + "'>" + ds.Tables[5].Rows[i]["subregion"].ToString() + "</a>";
                    }
                    this.CountryList += "</li><li class='leftcol'><h3>Countries</h3>";
                    for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                    {
                        if (i == 60 || i == 120)
                        {
                            this.CountryList += "</li><li class='leftcol'>";
                        }
                        this.CountryList += "<a href='../Country/CountryInfo.aspx?choice=" + ds.Tables[6].Rows[i]["subregionid"].ToString() + "'>" + ds.Tables[6].Rows[i]["subregion"].ToString() + "</a>";
                    }
                    this.CountryList += "</li></ul></li></ul>";*/
                }
                else if (ds.Tables[4].Rows.Count > 0) //cpo or rcc
                {
                    this.CountryTable = ds.Tables[4];
                    /*this.CountryList += "<ul><li><a href=''>Countries</a><ul>";
                    for (int i = 0; i < ds.Tables[7].Rows.Count; i++)
                    {
                        this.CountryList += "<lddsfdfsi><a href='../Country/CountryInfo.aspx?choice=" + ds.Tables[7].Rows[i]["countryid"].ToString() + "'>" + ds.Tables[7].Rows[i]["countryname"].ToString() + "</a></li>";
                    }
                    this.CountryList += "</ul></li></ul>";*/
                }
            }
            else if (this.Role == "Physician" && ds.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToBoolean(ds.Tables[1].Rows[0]["needmou"]))
                {
                    if (Convert.ToBoolean(ds.Tables[1].Rows[0]["mou"]))
                    {
                        this.MOU = true;
                    }
                }
                else
                {
                    this.MOU = true;
                }
                this.MOUAmmendments = Convert.ToBoolean(ds.Tables[1].Rows[0]["mouammendments"]);
                this.CountryMOU = Convert.ToBoolean(ds.Tables[1].Rows[0]["CountryMOU"]);

                //check if TIPAP Physician
                if (Convert.ToBoolean(ds.Tables[1].Rows[0]["tipapphys"]))
                {
                    this.TIPAPPhys = true;
                }
                else
                {
                    this.TIPAPPhys = false;
                }
                //noa
                this.NOA = Convert.ToBoolean(ds.Tables[1].Rows[0]["noa"]);
                try
                {
                    this.NOADate = Convert.ToDateTime(ds.Tables[1].Rows[0]["noadate"]);
                }
                catch { }
                this.AcceptingNewApps = Convert.ToBoolean(ds.Tables[1].Rows[0]["AcceptingNewApps"]);
                this.Tasigna = Convert.ToInt32(ds.Tables[1].Rows[0]["Tasigna"]);
            }
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
        public string Program
        {
            get { return mProgram; }
            set { mProgram = value; }
        }
        //**************************************************************************************************************
        public string FullName
        {
            get { return mFullName; }
            set { mFullName = value; }
        }

		//**************************************************************************************************************
		public bool IsAdmin
		{
			get{return mIsAdmin;}
			set{mIsAdmin = value;}
		}

        //**************************************************************************************************************
        public bool IsFileSharingAdmin
        {
            get { return mIsFileSharingAdmin; }
            set { mIsFileSharingAdmin = value; }
        }
        //**************************************************************************************************************
        public bool RAP
        {
            get { return mRAP; }
            set { mRAP = value; }
        }
		//**************************************************************************************************************
		public bool Disabled
		{
			get{return mDisabled;}
			set{mDisabled = value;}
		}
        //**************************************************************************************************************
        public DateTime ExpireDate
        {
            get { return mExpireDate; }
            set { mExpireDate = value; }
        }
        //**************************************************************************************************************
        public int CountryID
        {
            get { return mCountryID; }
            set { mCountryID = value; }
        }
        //**************************************************************************************************************
        public bool TIPAPPhys
        {
            get { return mTIPAPPhys; }
            set { mTIPAPPhys = value; }
        }
        //**************************************************************************************************************
        public bool NOA
        {
            get { return mNOA; }
            set { mNOA = value; }
        }
        //**************************************************************************************************************
        public DateTime NOADate
        {
            get { return mNOADate; }
            set { mNOADate = value; }
        }
        //**************************************************************************************************************
        public int Tasigna
        {
            get { return mTasigna; }
            set { mTasigna = value; }
        }
		//**************************************************************************************************************
		public string Role
		{
			get{return mRole;}
			set{mRole = value;}
		}
		//**************************************************************************************************************
		public string HomePage
		{
			get{return mHomePage;}
			set{mHomePage = value;}
		}
		//**************************************************************************************************************
		public string LeftNav
		{
			get{return mLeftNav;}
			set{mLeftNav = value;}
		}
		//**************************************************************************************************************
		public string ErrorMessage
		{
			get{return mErrorMessage;}
			set{mErrorMessage = value;}
		}
		//**************************************************************************************************************
		public string Header
		{
			get{return mHeader;}
			set{mHeader = value;}
		}
        //**********************************************************************************************************************
        public bool AcceptingNewApps
        {
            get { return mAcceptingNewApps; }
            set { mAcceptingNewApps = value; }
        }
	}
}
