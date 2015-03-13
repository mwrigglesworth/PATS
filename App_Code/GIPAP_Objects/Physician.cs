using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Physician.
	/// </summary>
	public class Physician
	{
		private int mPhysicianID;
		private char mSex;
		private string mFirstName;
		private string mLastName;
		private	string mPersonType;
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
		private int mApproved;
		private DateTime mApprovedDate;
		private int mUserID;
		private string mNotes;
        private bool mNOA;
        private DateTime mNOADate;
        private int mTasigna;
        private bool mMou;
        private bool mNeedMou;
        private bool mMouAmmendments;
        private bool mCountryMou;

		//other tables
		public string ClinicName;
		public string mCountryName;
        private string NoLinkCountryName;
		private string mUserName;
		private string mPassword;
		private int mComputerAccess;
        private bool mNOAGlivecCountry;
        private int mNOATasignaCountry;

        private DataTable MSEmail;
        private DataTable POEmail;
        private DataTable NovartisEmail;
        public DataTable ClinicDT;
        public DataTable DisplayDT;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		string connPS = ConfigurationSettings.AppSettings["connPS"];
		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAP2;PWD=secret;UID=sa;";

		//**************************************************************************************************************
		public Physician()
		{
			//Default Constructor
			this.Clear();
		}

		//**********************************************************************************************************************
		public Physician(int currid, string Urole)
		{
			if(currid == 0)
			{
				return;
			}
			else
			{
				DataSet myData = new DataSet();
				SqlParameter CurrID = new SqlParameter("@PhysicianID", SqlDbType.Int);
				CurrID.Value = currid;
				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianProfile2", CurrID);

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
        /*public Physician(GIPAP_Objects.User user, string dSet)
		{
			if(user.UserID == 0)
			{
				return;
			}
			else
			{
				DataSet myData = new DataSet();
                SqlParameter[] arrParams = new SqlParameter[2];

                arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
                arrParams[0].Value = user.UserID;

                arrParams[1] = new SqlParameter("@dSet", SqlDbType.NVarChar, 50);
                arrParams[1].Value = dSet;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianProfileByUser", arrParams);

				if (myData.Tables[0].Rows.Count <= 0)
				{
					throw new ArgumentException("The physician user does not exist.");
				}
				else
				{
					Inflate(myData, "Physician");
				}
				myData.Dispose();
			}
		}*/

		//**********************************************************************************************************************
		public void CreatePhysician(string createdby)
		{
			SqlParameter[] arrParams = new SqlParameter[22];

			arrParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
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

			arrParams[16] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[16].Value = this.Sex;

			arrParams[17] = new SqlParameter("@ComputerAccess", SqlDbType.Int);
			arrParams[17].Value = this.ComputerAccess;

			arrParams[18] = new SqlParameter("@PersonLink", SqlDbType.NChar, 100);
			arrParams[18].Direction = ParameterDirection.Output;

            arrParams[19] = new SqlParameter("@NOA", SqlDbType.Bit);
            if (this.NOA)
            {
                arrParams[19].Value = 1;//true
            }
            else
            {
                arrParams[19].Value = 0;//false
            }

            arrParams[20] = new SqlParameter("@NoaDate", SqlDbType.SmallDateTime);
            if (this.NOADate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[20].Value = this.NOADate;
            }
            else
            {
                arrParams[20].Value = DBNull.Value;
            }

            arrParams[21] = new SqlParameter("@Tasigna", SqlDbType.Int);
            arrParams[21].Value = this.Tasigna;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePhysician", arrParams);

			//Return the newly created records ID
			this.PhysicianID = (int)arrParams[0].Value;

			//If the returned value is -1 then the record already exists
			if(this.PhysicianID == -1)
			{
				throw new ArgumentException(arrParams[18].Value.ToString() + " already exists.");
			}
			else
			{
				this.CreatePS(createdby);
			}
		}
		//**********************************************************************************************************************
		private void CreatePS(string createdby)
		{
			SqlParameter[] arrParams = new SqlParameter[17];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianID;

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
			arrParams[16].Value = "Physician";

			SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_CreateGIPAPPerson", arrParams);

		}
		//**********************************************************************************************************************
		/*public void CreatePhysician(string createdby, GIPAP_Objects.PhysicianApplicant myApplicant)
		{
			SqlParameter[] arrParams = new SqlParameter[20];

			arrParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
			arrParams[0].Direction = ParameterDirection.Output;

			arrParams[1] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50);
			arrParams[1].Value = myApplicant.FirstName.Trim();

			arrParams[2] = new SqlParameter("@LastName", SqlDbType.NVarChar, 50);
			arrParams[2].Value = myApplicant.LastName.Trim();

			arrParams[3] = new SqlParameter("@Specialty", SqlDbType.NVarChar, 20);
			arrParams[3].Value = myApplicant.Specialty;

			arrParams[4] = new SqlParameter("@Street1", SqlDbType.VarChar, 100);
			arrParams[4].Value = myApplicant.Street1.Trim();

			arrParams[5] = new SqlParameter("@Street2", SqlDbType.VarChar, 100);
			arrParams[5].Value = myApplicant.Street2.Trim();

			arrParams[6] = new SqlParameter("@City", SqlDbType.VarChar, 30);
			arrParams[6].Value = myApplicant.City.Trim();

			arrParams[7] = new SqlParameter("@StateProvince", SqlDbType.VarChar, 30);
			arrParams[7].Value = myApplicant.StateProvince.Trim();

			arrParams[8] = new SqlParameter("@PostalCode", SqlDbType.VarChar, 10);
			arrParams[8].Value = myApplicant.PostalCode.Trim();

			arrParams[9] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[9].Value = myApplicant.CountryID;

			arrParams[10] = new SqlParameter("@Phone", SqlDbType.NVarChar, 500);
			arrParams[10].Value = myApplicant.Phone;

			arrParams[11] = new SqlParameter("@Fax", SqlDbType.NVarChar, 30);
			arrParams[11].Value = myApplicant.Fax;

			arrParams[12] = new SqlParameter("@Email", SqlDbType.NVarChar, 200);
			arrParams[12].Value = myApplicant.Email;

			arrParams[13] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30);
			arrParams[13].Value = myApplicant.Mobile;

			arrParams[14] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[14].Value = myApplicant.Notes;
					
			arrParams[15] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[15].Value = createdby;

			arrParams[16] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[16].Value = myApplicant.Sex;

			arrParams[17] = new SqlParameter("@ApplicantID", SqlDbType.Int);
			arrParams[17].Value = myApplicant.PhysicianApplicantID;

			arrParams[18] = new SqlParameter("@Clinic", SqlDbType.NVarChar, 50);
			arrParams[18].Value = myApplicant.Clinic;

			arrParams[19] = new SqlParameter("@PersonLink", SqlDbType.NChar, 100);
			arrParams[19].Direction = ParameterDirection.Output;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePhysician", arrParams);

			//Return the newly created records ID
			this.PhysicianID = (int)arrParams[0].Value;

			//If the returned value is -1 then the record already exists
			if(this.PhysicianID == -1)
			{
				throw new ArgumentException(arrParams[19].Value.ToString() + " already exists.");
			}
		}*/
		//**************************************************************************************************************
		public void AddPersonNote(string createdby, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianID;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = createdby;

			arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
			arrParams[2].Value = note;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePersonNote", arrParams);
		}

		//**********************************************************************************************************************
		public void Update(string modifiedby, string urole)
		{
			SqlParameter[] arrParams = new SqlParameter[22];

			arrParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianID;

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

			arrParams[13] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[13].Value = this.Notes;

			arrParams[14] = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 20);
			arrParams[14].Value = modifiedby;

			arrParams[15] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 30);
			arrParams[15].Value = this.Mobile;

			arrParams[16] = new SqlParameter("@Sex", SqlDbType.Char);
			arrParams[16].Value = this.Sex;

			arrParams[17] = new SqlParameter("@ComputerAccess", SqlDbType.Int);
			arrParams[17].Value = this.ComputerAccess;

            arrParams[18] = new SqlParameter("@NOA", SqlDbType.Bit);
            if (this.NOA)
            {
                arrParams[18].Value = 1;//true
            }
            else
            {
                arrParams[18].Value = 0;//false
            }

            arrParams[19] = new SqlParameter("@NoaDate", SqlDbType.SmallDateTime);
            if (this.NOADate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[19].Value = this.NOADate;
            }
            else
            {
                arrParams[19].Value = DBNull.Value;
            }

            arrParams[20] = new SqlParameter("@Urole", SqlDbType.NVarChar, 30);
            arrParams[20].Value = urole;

            arrParams[21] = new SqlParameter("@Tasigna", SqlDbType.Int);
            arrParams[21].Value = this.Tasigna;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePhysician", arrParams);
			this.UpdatePS(modifiedby);
		}
		//**********************************************************************************************************************
		private void UpdatePS(string modifiedby)
		{
			//Update the GCC information.
			SqlParameter[] arrParams = new SqlParameter[16];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianID;

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
		//**********************************************************************************************************************
		public void Approve(string modifiedby)
		{

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianID;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
			arrParams[1].Value = modifiedby;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ApprovePhysician", arrParams);
		}
		//**********************************************************************************************************************
		public void UnApprove(string modifiedby)
		{

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianID;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
			arrParams[1].Value = modifiedby;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UnApprovePhysician", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet PhysicianSearch(string xtra, GIPAP_Objects.User searchU)
		{
			string strSQL = "Select '<a href=PhysicianInfo.aspx?choice=' + Convert(nvarchar, personid) + '>' + LastName as '<b>Last Name</b>',";
			strSQL += " FirstName as '<b>First Name</a>', Replace(Replace(Replace(Approved, 0, 'No'), 1, 'Yes'), 2, 'Pending') as '<b>Approved</b>' from tblPerson where persontype = 'Physician' and ";
			strSQL += "FirstName like '" + this.FirstName + "%' and ";
			strSQL += "LastName like '" + this.LastName + "%' and ";
			strSQL += "Phone like '" + this.Phone + "%' and ";
			strSQL += "Fax like '" + this.Fax + "%' and ";
			strSQL += "Mobile like '" + this.Mobile + "%' and ";
			strSQL += "Street1 like '" + this.Street1+ "%' and ";
			strSQL += "Street2 like '" + this.Street2 + "%' and ";
			strSQL += "City like '" + this.City + "%' and ";
			strSQL += xtra;
			strSQL += "stateprovince like '" + this.StateProvince + "%' and ";
			strSQL += "postalcode like '" + this.PostalCode + "%' and ";
			if(this.Approved != -1)
			{
				strSQL += "approved = " + this.Approved.ToString() + " and ";
			}
			if(this.ComputerAccess != -1)
			{
				strSQL += "personid in (select personid from tblmou where computeraccess = " + this.ComputerAccess.ToString() + ") and ";
			}
			if(this.CountryID != 0)
			{
				strSQL += "CountryID = " + this.CountryID.ToString() + " and ";
			}
			if(this.UserName != "")
			{
				strSQL += "userid = (select userid from tbluser where username = '" + this.UserName + "') and ";
			}

            if (searchU.Role == "MaxStation")
            {
                if (searchU.CountryID == 76)
                {
                    strSQL += " countryid = 76 and ";
                }
                else
                {
                    strSQL += "personid in ( select c.personid from tblpatientphysician c, tblMaxStation d, tblperson e where c.PATIENTID = d.PATIENTID and d.PERSONID=e.personid and e.userid=" + searchU.UserID.ToString() + ") and ";
                }
            }
			strSQL += "Specialty like '" + this.Specialty + "%' and ";
			strSQL += "Email like '" + this.Email + "%' ";
			strSQL += "Order by LastName";

			return SqlHelper.ExecuteDataset(connString, CommandType.Text, strSQL);
		}
		//**********************************************************************************************************************
		public string PhysicianInfo(string Urole, bool full,string Uname)
		{
            StringBuilder pi = new StringBuilder();
            pi.Append("<h1><font color=steelblue>" + this.FirstName + " " + this.LastName + "</font></h1>");
            if (full)
            {
                pi.Append("<div class='LeftColDivHeader'>Physician Info</div><div class='LeftColDiv'>");
                if (Urole == "TMFUser")
                {
                    pi.Append("<font class='lbl'>Sex: </font>");
                    if (this.Sex == Convert.ToChar("M"))
                    {
                        pi.Append("Male");
                    }
                    else if (this.Sex == Convert.ToChar("F"))
                    {
                        pi.Append("Female");
                    }
                }

                pi.Append("<br><font class='lbl'>Specialty: </font>" + this.Specialty + "<br>");
                pi.Append("<br><font class='lbl'>Address: </font>" + this.Street1 + "<br>" + this.Street2 + "<br><font class='lbl'>City: </font>" + this.City + "<br><font class='lbl'>State / Province: </font>" + this.StateProvince + " " + this.PostalCode + "<br><font class='lbl'>Country: </font>" + this.mCountryName + "<br>");
                pi.Append("<font class='lbl'> Email: </font><a href=mailto:" + this.Email + " >" + this.Email + "</a>");
                pi.Append("<br><font class='lbl'>(tel)</font>" + this.Phone + "<br><font class='lbl'>(fax)</font>" + this.Fax + "<br><font class='lbl'>(mobile)</font>" + this.Mobile);
                if (Urole == "TMFUser" || Urole == "MaxStation")
                {
                    pi.Append("<br><font class='lbl'>Additional Physician Information/Contact Details:</font><br>" + this.Notes);
                }
                if (Urole == "TMFUser")
                {
                    pi.Append("<br><br><font class='lbl'>Username: </font>" + this.UserName + "<br><font class='lbl'>Password: </font>" + (this.UserName == Uname ? this.Password : "****"));
                }
                pi.Append("</div>");
            }
			return pi.ToString();
		}
		//**********************************************************************************************************************
        public string MOULabel()
        {
            StringBuilder pi = new StringBuilder();
            if (this.CountryID == 102 && this.NOA)
            {
                pi.Append("<font class='lbl'>NOA Malaysia MOU: </font>");
                if (this.CountryMou)
                {
                    pi.Append("<font color=steelblue>Signed</font>");
                }
                else
                {
                    pi.Append("<font color=red>Not Signed</font>");
                }
                pi.Append("<br/>");
            }
            pi.Append("<font class='lbl'>MOU: </font>");
            if (this.NeedMou)
            {
                if (this.Mou)
                {
                    pi.Append("<font color=steelblue>Signed</font>");
                    pi.Append("<br><font class='lbl'>MOU Ammendments: </font>");
                    if (this.MouAmmendments)
                    {
                        pi.Append("<font color=steelblue>Signed</font>");
                    }
                    else
                    {
                        pi.Append("<font color=red>Not Signed</font>");
                    }
                }
                else
                {
                    pi.Append("<font color=red>Not Signed</font>");
                }
            }
            else
            {
                pi.Append("N/A");
                pi.Append("<br><font class='lbl'>MOU Ammendments: </font>");
                if (this.MouAmmendments)
                {
                    pi.Append("<font color=steelblue>Signed</font>");
                }
                else
                {
                    pi.Append("<font color=red>Not Signed</font>");
                }
            }
            if (this.ComputerAccess != -1)
            {
                pi.Append("<br><br><font class='lbl'>Computer Access: </font>");
                if (this.ComputerAccess == 0)
                {
                    pi.Append("No");
                }
                else if (this.ComputerAccess == 1)
                {
                    pi.Append("Yes");
                }
                else if (this.ComputerAccess == 2)
                {
                    pi.Append("Limited");
                }
            }
            return pi.ToString();
        }
        //**********************************************************************************************************************
        public string NOALabel()
        {
            StringBuilder pi = new StringBuilder();
            if (this.NOA)
            {
                pi.Append("<b><font color=steelblue>NOA Glivec Physician</b></font><br>");
                pi.Append(this.NOADate.Day.ToString() + " " + this.NOADate.ToString("y"));
            }
            else
            {
                pi.Append("<b><font color=steelblue>GIPAP Physician</b></font><br>");
            }
            if (this.Tasigna != 0)
            {
                pi.Append("<br><br><b><font color=purple>NOA Tasigna Approved</b></font><br>");
                if (this.Tasigna == 1)
                {
                    pi.Append("1st + 2nd Line");
                }
                else if (this.Tasigna == 2)
                {
                    pi.Append("2nd Line Only");
                }
            }

            return pi.ToString();
        }
		//**********************************************************************************************************************
		public string ApprovedLabel()
		{
            StringBuilder pi = new StringBuilder();
            pi.Append("<font class='lbl'>Approved:</font> ");
			if(this.Approved == 1)
			{
				pi.Append("<font color=steelblue>Yes</font>");
				if(this.ApprovedDate != Convert.ToDateTime("1/1/0001"))
				{
					pi.Append("<br><font class='lbl'>Approved Date: </font>" + this.ApprovedDate.Day.ToString() + " " + this.ApprovedDate.ToString("y"));
				}
			}
			else if(this.Approved == 0)
			{
				pi.Append("<font color=red>No</font>");
			}
			else if(this.Approved == 2)
			{
				pi.Append("<font color=red>Pending</font>");
			}
            
                
			return pi.ToString();
		}
		//**********************************************************************************************************************
		public string SAEInstructions(int patID)
		{

			SqlParameter arrParams = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams.Value = patID;

			DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetSAEInstructions", arrParams);

			string si = "If you are reporting an SAE, click on the link to open the Novartis Patient Assistance Program SAE form. Please print the form, complete all required fields, and fax to the Novartis office/fax number provided herein with a cover sheet from your office. In ";
			if(ds.Tables[0].Rows.Count > 0)
			{
				si += ds.Tables[0].Rows[0]["countryname"].ToString();
			}
			si += " please fax to ";
			if(ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["fax"].ToString() != "")
			{
				si += ds.Tables[1].Rows[0]["firstname"].ToString() + " " + ds.Tables[1].Rows[0]["lastname"].ToString();
				si += " at fax number ";
				si += ds.Tables[1].Rows[0]["fax"].ToString() + ".";
			}
			else if(ds.Tables[2].Rows.Count > 0 && ds.Tables[2].Rows[0]["fax"].ToString() != "")
			{
				si += ds.Tables[2].Rows[0]["firstname"].ToString() + " " + ds.Tables[2].Rows[0]["lastname"].ToString();
				si += " at fax number ";
				si += ds.Tables[2].Rows[0]["fax"].ToString() + ".";
			}
			si += "<br><br>It is only necessary to report Serious Adverse Events (SAE). These criteria are determined by drug regulatory agencies and confidential reporting of SAE’s contributes to monitoring the safety of registered drugs. Adverse Events that are Serious meet one or more of the following criteria:<br><br> <li>Patient died <li>Involved or prolonged inpatient hospitalization <li>Involved persistent or significant disability or incapacity <li>Life threatening episode <li>Congenital anomaly/birth defect <li>Other significant medical events ";
			return si;
		}

		//**********************************************************************************************************************
		public DataSet GetPhysicianList(int uid)
		{
			SqlParameter[] arrParams = new SqlParameter[1];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = uid;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianList", arrParams);
		}

		//**********************************************************************************************************************
		public void UpdatePhysicianClinics(System.Web.UI.WebControls.CheckBoxList lb)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeletePhysicianClinics", arrParams[0]);

			for(int i=0; i<lb.Items.Count; i++)
			{
                if (lb.Items[i].Selected)
                {
                    arrParams[1] = new SqlParameter("@ClinicID", SqlDbType.Int);
                    arrParams[1].Value = Convert.ToInt32(lb.Items[i].Value);

                    SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePhysicianClinics", arrParams);
                }
			}
		}
		//**********************************************************************************************************************
		public void UpdateMOU(GIPAP_Objects.User physUser)
		{
            SqlParameter arrParams = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams.Value = physUser.UserID;

			SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_UpdateMOU2", arrParams);

            physUser.HomePage = "Physician/Dashboard.aspx";
        }
        
        //**********************************************************************************************************************
        public void UpdateMOUAmmendments(GIPAP_Objects.User physUser)
        {
            SqlParameter arrParams = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams.Value = physUser.UserID;

            SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_UpdateMOUAmmendments2", arrParams);

            physUser.HomePage = "Physician/Dashboard.aspx";
        }
        //*********************************************************************************************************************
        public DataSet SnapShotPatientReport(DateTime repDate)
        {
            SqlParameter[] arParams = new SqlParameter[2];

            arParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
            arParams[0].Value = this.PhysicianID;

            arParams[1] = new SqlParameter("@ReportDate", SqlDbType.DateTime);
            arParams[1].Value = repDate;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_SnapShotPatientReport", arParams);
        }
		
		//**********************************************************************************************************************
		public DataSet GetPhysicianPatients(string Urole)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@PersonID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianID;

			arrParams[1] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
			arrParams[1].Value = Urole;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianPatients", arrParams);
		}


        //**********************************************************************************************************************
        public DataSet GetPhysicianDataSets(string dset)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
            arrParams[0].Value = this.PhysicianID;

            arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
            arrParams[1].Value = dset;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianDatasets", arrParams);
        }
        //**********************************************************************************************************************
        public DataSet GetPhysicianDataSetsByUser(int uid, string dset)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[0].Value = uid;

            arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
            arrParams[1].Value = dset;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianDatasetsByUser", arrParams);
        }
		//**********************************************************************************************************************
		public DataSet GetPhysicianClinicList()
		{
			SqlParameter paramPhysicianID = new SqlParameter("@PhysicianID", SqlDbType.Int);
			paramPhysicianID.Value = this.PhysicianID;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianClinicList", paramPhysicianID);
		}
        //**********************************************************************************************************************
        public DataSet GetAvailablePhysicianClinics()
        {
            SqlParameter paramPhysicianID = new SqlParameter("@CountryID", SqlDbType.Int);
            paramPhysicianID.Value = this.CountryID;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetAvailablePhysicianClinics", paramPhysicianID);
        }
		//**********************************************************************************************************************
		public GIPAP_Objects.Email ReminderEmail()
		{
			GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
			myEmail.From = "gipap@themaxfoundation.org";
			myEmail.To = this.Email;
			myEmail.CC = "";
			if(this.MSEmail.Rows.Count > 0)
			{
				for(int i=0; i<this.MSEmail.Rows.Count; i++)
				{
					myEmail.CC += this.MSEmail.Rows[i]["email"].ToString() + "; ";
				}
			}
            if (this.NOA)
            {
                if (this.CountryID == 108)//mexico
                {
                    myEmail.Subject = "PAT Re-Evaluation Request " + this.UserName;
                    myEmail.Message = "PAT RE-EVALUATION REQUEST (PHYSICIAN)\n\n";
                }
                else
                {
                    myEmail.Subject = "NOA/GIPAP Re-Evaluation Request " + this.UserName;
                    myEmail.Message = "NOA/GIPAP RE-EVALUATION REQUEST (PHYSICIAN)\n\n";
                }
            }
            else
            {
                myEmail.Subject = "GIPAP Re-Evaluation Request " + this.UserName;
                myEmail.Message = "GIPAP RE-EVALUATION REQUEST (PHYSICIAN)\n\n";
            }
			myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
			myEmail.Message += "Dear Dr. " + this.FirstName + " " + this.LastName + ":\n\n";
			myEmail.Message += "As of today there are " + this.GetPhysicianDataSets("patientsneedingreapproval").Tables[0].Rows.Count.ToString();
            if (this.NOA)
            {
                if (this.CountryID == 108)//mexico
                {
                    myEmail.Message += " patient(s) under your care who require re-evaluation in PAT.  Patient cases that are not re-evaluated are automatically closed 60 days after the end of the approval period. To re-approve these patients or request other actions, log on to the on-line PAT Patient Assistance Tracking System (PATS):\n\n";
                }
                else
                {
                    myEmail.Message += " patient(s) under your care who require re-evaluation in NOA/GIPAP.  Patient cases that are not re-evaluated are automatically closed 60 days after the end of the approval period. To re-approve these patients or request other actions, log on to the on-line NOA/GIPAP Patient Assistance Tracking System (PATS):\n\n";
                }
            }
            else
            {
                myEmail.Message += " patient(s) under your care who require re-evaluation in GIPAP.  Patient cases that are not re-evaluated are automatically closed 60 days after the end of the approval period. To re-approve these patients or request other actions, log on to the on-line GIPAP Patient Assistance Tracking System (PATS):\n\n";
            }
			myEmail.Message += "- Go to http://www.themaxfoundation.org \n";
			myEmail.Message += "- Login with your username and password.  (If you need this information please request it from us in an email at gipap@themaxfoundation.org)\n";
			myEmail.Message += "- Choose the appropriate action from the menu.\n\n";
			myEmail.Message += "If you do not have internet access and you are unable to login to PATS, answer the questions below separately for EACH patient listed at the end of this email and send the answers to us via email: ";
            if (this.CountryID == 108 && this.NOA) //mexico
            {
                myEmail.Message += "pat.mexico@themaxfoundation.org";
            }
            else
            {
                myEmail.Message += "Gipap@themaxfoundation.org";
            }
            myEmail.Message += " or fax: 1 (425) 778-8760.\n\n";
			myEmail.Message += "GLIVEC Re-approval Questions:\n";
			myEmail.Message += "1.	Do you recommend that the patient continue treatment with Glivec?\n";
			myEmail.Message += "2.	Do you recommend a change in the dosage? If so, choose from one of the following: 200mg, 260mg, 300mg, 400mg, 600mg, 800mg\n";
			myEmail.Message += "3.	Has the patient consent form been signed?\n\n";
            if (this.Tasigna > 0)
            {

                myEmail.Message += "TASIGNA Re-approval Questions:\n";
                myEmail.Message += "1.	Do you recommend that the patient continue treatment with Tasigna?\n";
                myEmail.Message += "2.	Do you recommend a change in the dosage? If so, choose from one of the following: 400mg BID, 400mg QD, 200mg QD\n";
                myEmail.Message += "3.	Has the patient consent form been signed?\n\n";
            }
			string fullname = this.POEmail.Rows[0]["firstname"].ToString() + " " + this.POEmail.Rows[0]["lastname"].ToString();
			string email = this.POEmail.Rows[0]["email"].ToString();
			if(this.CountryID != 201 && this.CountryID != 150)
			{
				myEmail.Message += "If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org.  ";
			}
			try
			{
				if(this.MSEmail.Rows[0]["phone"].ToString() != "")
				{
					myEmail.Message += "Locally, you may contact our MaxStation, " + this.MSEmail.Rows[0]["firstname"].ToString() + " " + this.MSEmail.Rows[0]["lastname"].ToString() + ", who works with " + this.NoLinkCountryName + " at " + this.MSEmail.Rows[0]["phone"].ToString();
					if(this.MSEmail.Rows[0]["email"].ToString() != "")
					{
						myEmail.Message += " or " + this.MSEmail.Rows[0]["email"].ToString() + ".";
					}
				}
				else
				{
					if(this.MSEmail.Rows[0]["email"].ToString() != "")
					{
						myEmail.Message += "Locally, you may contact our MaxStation, " + this.MSEmail.Rows[0]["firstname"].ToString() + " " + this.MSEmail.Rows[0]["lastname"].ToString() + ", who works with " + this.NoLinkCountryName + " at " + this.MSEmail.Rows[0]["email"].ToString();
					}
				}
				if(this.CountryID == 201)
				{
					myEmail.Message += "  If you have questions or concerns, please contact " + fullname + " at (425)778-8660 or send an email to gipap@themaxfoundation.org.  ";
				}
			}
			catch{}
			myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation\n\n";
			myEmail.Message += "PATIENT(S) REQUIRING RE-EVALUATION:\n\n";
            DataTable dt = this.GetPhysicianDataSets("patientsneedingreapproval").Tables[0];
			if(dt.Rows.Count > 0)
			{
				for(int i=0; i<dt.Rows.Count; i++)
				{
					myEmail.Message += dt.Rows[i]["firstname"].ToString() + " " + dt.Rows[i]["lastname"].ToString();
					myEmail.Message += ", " + dt.Rows[i]["currentdosage"].ToString() + "\n";
					myEmail.Message += "Period End Date: " + dt.Rows[i]["enddate"].ToString() + "\n\n";
				}
			}
			myEmail.MailType = "Reminder";
			myEmail.PhysicianID = this.PhysicianID;
			return myEmail;
		}
		//**********************************************************************************************************************
		public GIPAP_Objects.Email createApprovalEmail()
		{
            if (this.NOA)
            {
                return this.createApprovalEmailNOA();
            }
			GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
			myEmail.From = "gipap@themaxfoundation.org";
			myEmail.To = "gipap@themaxfoundation.org; " + this.Email;
			myEmail.CC = "gipap@themaxfoundation.org";
			myEmail.Subject = "New GIPAP Physician Approval Letter";
			myEmail.Message = this.FirstName + " " + this.LastName;
			if(this.Phone != "")
			{
				myEmail.Message += "\n" + this.Phone;
			}
			if(this.Fax != "")
			{
				myEmail.Message += "\n" + this.Fax;
			}
			myEmail.Message += "\n\n" + DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
			myEmail.Message += "Dear Dr. " + this.FirstName + " " + this.LastName + ":\n\n";
			myEmail.Message += "We are happy to inform you that you have been approved to participate in the Glivec International Patient Assistance Program (GIPAP) in " + this.NoLinkCountryName + ".\n\n";
			myEmail.Message += "You may refer patients to the program by filling out the GIPAP patient application form on www.maxaid.org, or through our website, www.themaxfoundation.org. (Click on the link for GIPAP/Glivec).\n\n";
			myEmail.Message += "Your username and password are provided below.  When you log in you will be prompted to review and accept a Memorandum of Understanding (MOU) which outlines the roles and responsibilities between TMF and you in the administration of GIPAP.\n\n";
			myEmail.Message += "UserName: " + this.UserName + "\n";
			myEmail.Message += "Password: " + this.Password + "\n\n";
			myEmail.Message += "Patients will be approved for three month periods and they will need to be re-evaluated by you every three months to remain in the program.  We will issue a reminder email to you with re-evaluation questions 30 days before the patient’s approval period is over. You will be able to respond to these questions through the web application using your username and password, or by replying to the email.\n\n";
			myEmail.Message += "For more information about how to navigate the GIPAP system, please refer to the PATS User Guide located in your menu in PATS after logging in at www.maxaid.org.  We welcome you to GIPAP and look forward to working with you and your patients.\n\n";
			myEmail.Message += "Regards,\n\n";
			myEmail.Message += "The Max Foundation";
			myEmail.MailType = "Physician";
			myEmail.PhysicianID = this.PhysicianID;
			return myEmail;
		}
        //**********************************************************************************************************************
        private GIPAP_Objects.Email createApprovalEmailNOA()
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.To = "gipap@themaxfoundation.org; " + this.Email;
            myEmail.CC = "gipap@themaxfoundation.org";
            if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "New PAT Physician Approval Letter";
            }
            else
            {
                myEmail.Subject = "New NOA/GIPAP Physician Approval Letter";
            }
            myEmail.Message = this.FirstName + " " + this.LastName;
            if (this.City != "")
            {
                myEmail.Message += "\nCity: " + this.City;
            }
            if (this.Phone != "")
            {
                myEmail.Message += "\n" + this.Phone;
            }
            if (this.Fax != "")
            {
                myEmail.Message += "\n" + this.Fax;
            }
            myEmail.Message += "\n\n" + DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Dear Dr. " + this.FirstName + " " + this.LastName + ":\n\n";
            if (this.CountryID == 102)
            {
                myEmail.Message += "We are happy to inform you that you have been successfully enrolled in the Novartis Oncology Access (NOA) Program in Malaysia.  NOA is a co-pay 1+1 program. For every 1 month purchased, Novartis will donate 1 month of supply.\n\n";
            }
            else if (this.CountryID == 150) //south africa
            {
                myEmail.Message += "We are happy to inform you that you have been successfully enrolled in the Novartis Oncology Access (NOA) Program in South Africa. \n\n";
            }
            else if (this.CountryID == 108)//mexico
            {
                myEmail.Message += "We are happy to inform you that you have been successfully enrolled in the Novartis Oncology Temporal Assistance Program (PAT) in Mexico.\n\n";
            }
            else
            {
                myEmail.Message += "We are happy to inform you that you have been approved to participate in the Novartis Oncology Access Program (NOA) / Glivec International Patient Assistance Program (GIPAP) in India.  As a selected physician you will be requested to sign a Memorandum of Understanding (MOU) which outlines the roles and responsibilities of physicians participating in this access program.\n\n";
            }
            myEmail.Message += "You may refer patients to the program by going to www.maxaid.org and logging into the Patient Tracking System (PATS). Your personal username and password are provided below: \n\n";
            myEmail.Message += "UserName: " + this.UserName + "\n";
            myEmail.Message += "Password: " + this.Password + "\n\n";
            if (this.CountryID == 108)
            {
                myEmail.Message += "Upon review of your registered patient applications and confirmation that patients fulfill all criterias for the program, patients will be enrolled into the PAT Program.\n\n";
            }
            if (this.CountryID == 76)
            {
                myEmail.Message += "Upon review of your submitted patient applications and confirmation that patients fulfill medical and financial criteria for the program, patients will be approved one year renewable upon annual review. Supply will be dispensed in three month periods and patients will need to be re-evaluated by you every three months to receive further supply of Glivec.\n\n";
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322.";
                myEmail.Message += "\n\nRegards, \n\nNOA Call Centre – managed by The Max Foundation";
            }
            else //generic
            {
                if (this.MSEmail.Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.MSEmail.Rows[0]["phone"].ToString() + " or " + this.MSEmail.Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.POEmail.Rows[0]["phone"].ToString() + " or " + this.POEmail.Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.MailType = "Physician";
            myEmail.PhysicianID = this.PhysicianID;
            return myEmail;
        }
		//**********************************************************************************************************************
		public GIPAP_Objects.Email createApprovalEmailtoCPO()
		{
            if (this.NOA)
            {
                return this.createApprovalEmailtoCPONOA();
            }
			GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
			myEmail.From = "gipap@themaxfoundation.org";
			myEmail.To = "gipap@themaxfoundation.org; ";
            if (this.NovartisEmail.Rows.Count > 0)
            {
                for (int i = 0; i < this.NovartisEmail.Rows.Count; i++)
                {
                    myEmail.To += this.NovartisEmail.Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.CC = "gipap@themaxfoundation.org; ";
            if (this.MSEmail.Rows.Count > 0)
            {
                for (int i = 0; i < this.MSEmail.Rows.Count; i++)
                {
                    myEmail.CC += this.MSEmail.Rows[i]["email"].ToString() + "; ";
                }
            }
			myEmail.Subject = "Physician Approval Notification (Novartis)";
			myEmail.Message += "GIPAP PHYSICIAN APPROVAL NOTIIFICATION (NOVARTIS)\n\n";
			myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
			myEmail.Message += "Physician Name: " + this.FirstName + " " + this.LastName + "\n";
			myEmail.Message += "Country: " + this.NoLinkCountryName + "\n\n";
			myEmail.Message += "We would like to notify you that we have registered ";
			myEmail.Message += this.FirstName + " " + this.LastName;
			myEmail.Message += " as a qualified GIPAP physician per your request.\n\n";
			myEmail.Message += "Please update your records accordingly.\n\n";
			myEmail.Message += "If you have any questions or concerns, please do not hesitate to communicate with us at Gipap@themaxfoundation.org.\n\n";
			myEmail.Message += "Regards,\n\n";
			myEmail.Message += "The Max Foundation";
			myEmail.MailType = "Physician";
			myEmail.PhysicianID = this.PhysicianID;

			return myEmail;
		}
        //**********************************************************************************************************************
        private GIPAP_Objects.Email createApprovalEmailtoCPONOA()
        {
            GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
            myEmail.From = "gipap@themaxfoundation.org";
            myEmail.To = "gipap@themaxfoundation.org; ";
            if (this.NovartisEmail.Rows.Count > 0)
            {
                for (int i = 0; i < this.NovartisEmail.Rows.Count; i++)
                {
                    myEmail.To += this.NovartisEmail.Rows[i]["email"].ToString() + "; ";
                }
            }
            myEmail.CC = "gipap@themaxfoundation.org; ";
            if (this.MSEmail.Rows.Count > 0)
            {
                for (int i = 0; i < this.MSEmail.Rows.Count; i++)
                {
                    myEmail.CC += this.MSEmail.Rows[i]["email"].ToString() + "; ";
                }
            }
            if (this.CountryID == 108)//mexico
            {
                myEmail.Subject = "PAT Physician Approval Notification (Novartis)";
                myEmail.Message += "PAT PHYSICIAN APPROVAL NOTIIFICATION (NOVARTIS)\n\n";
            }
            else
            {
                myEmail.Subject = "NOA/GIPAP Physician Approval Notification (Novartis)";
                myEmail.Message += "NOA/GIPAP PHYSICIAN APPROVAL NOTIIFICATION (NOVARTIS)\n\n";
            }
            myEmail.Message += DateTime.Today.Day.ToString() + " " + DateTime.Today.ToString("y") + "\n\n";
            myEmail.Message += "Physician Name: " + this.FirstName + " " + this.LastName + "\n";
            myEmail.Message += "Country: " + this.NoLinkCountryName + "\n";
            myEmail.Message += "City: " + this.City + "\n\n";
            myEmail.Message += "We would like to notify you that we have registered ";
            myEmail.Message += this.FirstName + " " + this.LastName;
            if (this.CountryID == 108)
            {
                myEmail.Message += " as a qualified PAT physician per your request.\n\n";
            }
            else
            {
                myEmail.Message += " as a qualified GIPAP/NOA physician per your request.\n\n";
            }
            myEmail.Message += "Please update your records accordingly.\n\n";
            if (this.CountryID == 76)
            {
                myEmail.Message += "If you have questions or concerns, please contact the Programme Administrator - The Max Foundation’s office (tel) 022 6660 3320 / 21; ( fax) 022 6660 3322.";
                myEmail.Message += "\n\nRegards, \n\nNOA Call Centre – managed by The Max Foundation";
            }
            else
            {
                if (this.MSEmail.Rows.Count > 0)
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.MSEmail.Rows[0]["phone"].ToString() + " or " + this.MSEmail.Rows[0]["email"].ToString() + ".";
                }
                else
                {
                    myEmail.Message += "If you have questions or concerns, please contact The Max Foundation’s office at " + this.POEmail.Rows[0]["phone"].ToString() + " or " + this.POEmail.Rows[0]["email"].ToString() + ".";
                }
                myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
            }
            myEmail.MailType = "Physician";
            myEmail.PhysicianID = this.PhysicianID;

            return myEmail;
        }
		//**********************************************************************************************************************
		private void Clear() //Sets the object to the default values
		{		
			this.PhysicianID = 0;
			this.FirstName = "";
			this.LastName = "";
			this.Specialty = "";
			this.Phone = "";
			this.Fax = "";
			this.Email = "";
			this.ClinicID = 0;
			this.Approved = 2;
			this.UserID = 0;
			this.Notes = "";
			this.Sex = ' ';
		}

		//**********************************************************************************************************************
		private void Inflate(DataSet ds, string Urole)
		{
			this.PhysicianID = (int)(ds.Tables[0].Rows[0]["PersonID"]);
			this.FirstName = (ds.Tables[0].Rows[0]["FirstName"]).ToString();
			this.LastName = (ds.Tables[0].Rows[0]["LastName"]).ToString();
			this.PersonType = (ds.Tables[0].Rows[0]["PersonType"]).ToString();
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

			this.Approved = Convert.ToInt32(ds.Tables[0].Rows[0]["Approved"]);
			try
			{
				this.ApprovedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ApprovedDate"]);
			}
			catch{}

			if(ds.Tables[0].Rows[0]["UserID"] == DBNull.Value)
				{this.UserID = 0;}
			else
				{this.UserID = (int)(ds.Tables[0].Rows[0]["UserID"]);}

			this.Notes = ds.Tables[0].Rows[0]["Notes"].ToString();

			//clinic
            this.ClinicDT = ds.Tables[1];
            if(ds.Tables[1].Rows.Count > 0)
			{
                for(int i=0; i<ds.Tables[1].Rows.Count; i++)
				{
                    this.ClinicName += "<li><a href=../Clinic/ClinicInfo.aspx?choice=" + ds.Tables[1].Rows[i]["ClinicID"].ToString() + ">" + ds.Tables[1].Rows[i]["ClinicName"].ToString() + "</a></li>";
				}
			}
            //country
			if(ds.Tables[2].Rows.Count > 0)
			{
				this.CountryID = Convert.ToInt32(ds.Tables[2].Rows[0]["CountryID"]);
                this.NoLinkCountryName = ds.Tables[2].Rows[0]["CountryName"].ToString();
                this.NOAGlivecCountry = Convert.ToBoolean(ds.Tables[2].Rows[0]["noaglivec"]);
                this.NOATasignaCountry = Convert.ToInt32(ds.Tables[2].Rows[0]["noatasigna"]);
                if (Urole == "TMFUser")
				{
					this.mCountryName = "<a href=../Country/CountryInfo.aspx?choice=" + ds.Tables[2].Rows[0]["CountryID"].ToString();
					this.mCountryName += ">" + ds.Tables[2].Rows[0]["CountryName"].ToString() + "</a>";
				}
				else
				{
				
					this.mCountryName = ds.Tables[2].Rows[0]["CountryName"].ToString();
				}
			}
			//user
			if(ds.Tables[3].Rows.Count > 0)
			{
				this.UserName = ds.Tables[3].Rows[0]["UserName"].ToString();
				this.Password = ds.Tables[3].Rows[0]["Password"].ToString();
			}
            //mou
            if (Urole != "Physician")
            {
                this.Mou = Convert.ToBoolean(ds.Tables[4].Rows[0]["mou"]);
                this.NeedMou = Convert.ToBoolean(ds.Tables[4].Rows[0]["needmou"]);
                this.MouAmmendments = Convert.ToBoolean(ds.Tables[4].Rows[0]["mouammendments"]);
                this.ComputerAccess = Convert.ToInt32(ds.Tables[4].Rows[0]["computeraccess"]);
                this.NOA = Convert.ToBoolean(ds.Tables[4].Rows[0]["noa"]);
                this.CountryMou = Convert.ToBoolean(ds.Tables[4].Rows[0]["CountryMOU"]);
                try
                {
                    this.NOADate = Convert.ToDateTime(ds.Tables[4].Rows[0]["noadate"]);
                }
                catch { }
                this.Tasigna = Convert.ToInt32(ds.Tables[4].Rows[0]["tasigna"]);

                this.MSEmail = ds.Tables[5];
                this.POEmail = ds.Tables[6];
                this.NovartisEmail = ds.Tables[7];
            }
            else //physician user... not usable yet
            {
                /*try
                {
                    this.DisplayDT = ds.Tables[4];
                }
                catch
                {
                    //defaults to clinics
                    this.DisplayDT = ds.Tables[1]; //clinics
                }*/
            }
		}

		//**********************************************************************************************************************
		public int PhysicianID
		{
			get{return mPhysicianID;}
			set{mPhysicianID = value;}
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
		public int ClinicID
		{
			get{return mClinicID;}
			set{mClinicID = value;}
		}

		//**********************************************************************************************************************
		public int Approved
		{
			get{return mApproved;}
			set{mApproved = value;}
		}
		//**********************************************************************************************************************
		public DateTime ApprovedDate
		{
			get{return mApprovedDate;}
			set{mApprovedDate = value;}
        }
        //**********************************************************************************************************************
        public bool MouAmmendments
        {
            get { return mMouAmmendments; }
            set { mMouAmmendments = value; }
        }
        //**********************************************************************************************************************
        public bool NeedMou
        {
            get { return mNeedMou; }
            set { mNeedMou = value; }
        }
        //**********************************************************************************************************************
        public bool Mou
        {
            get { return mMou; }
            set { mMou = value; }
        }

        //**********************************************************************************************************************
        public bool CountryMou
        {
            get { return mCountryMou; }
            set { mCountryMou = value; }
        }
        //**********************************************************************************************************************
        public bool NOA
        {
            get { return mNOA; }
            set { mNOA = value; }
        }
        //**********************************************************************************************************************
        public DateTime NOADate
        {
            get { return mNOADate; }
            set { mNOADate = value; }
        }
        //**********************************************************************************************************************
        public int Tasigna
        {
            get { return mTasigna; }
            set { mTasigna = value; }
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
		//**********************************************************************************************************************
		public int ComputerAccess
		{
			get{return mComputerAccess;}
			set{mComputerAccess = value;}
        }
        //**********************************************************************************************************************
        public bool NOAGlivecCountry
        {
            get { return mNOAGlivecCountry; }
            set { mNOAGlivecCountry = value; }
        }
        //**********************************************************************************************************************
        public int NOATasignaCountry
        {
            get { return mNOATasignaCountry; }
            set { mNOATasignaCountry = value; }
        }
	}
}
