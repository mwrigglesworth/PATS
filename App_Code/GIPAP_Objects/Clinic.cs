using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Clinic.
	/// </summary>
	public class Clinic
	{
		private int mClinicID;
		private string mClinicName;
		private string mDepartment;
		private string mStreet1;
		private string mStreet2;
		private string mCity;
		private string mStateProvince;
		private string mPostalCode;
		private int mCountryID;
		private string mCountryName;
		private string mPhone;
		private string mFax;
		private string mEmail;
		private string mAdminFirstName;
		private string mAdminLastName;
		private string mAdminPhone;
		private string mAdminFax;
		private string mAdminEmail;
		private int mCMLYear1;
		private int mCMLYear2;
		private int mCMLYear3;
		private int mCMLYear4;
		private int mCMLYear5;
		private int mCMLYear6;
		private int mCMLYear7;
		private int mCMLYear8;
		private int mCMLYear9;
		private int mCMLYear10;
		private int mGISTYear1;
		private int mGISTYear2;
		private int mGISTYear3;
		private int mGISTYear4;
		private int mGISTYear5;
		private bool mCheckedOut;
		private int mApproved;
		private DateTime mApprovedDate;
		private int mUserID;

		private DateTime CreateDate;
		private string CPOEmail;

		public DataSet ClinicDS;
			
		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		string connPS = ConfigurationSettings.AppSettings["connPS"];

		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAP2;PWD=secret;UID=sa;";

		//**************************************************************************************************************
		public Clinic()
		{
			//Default constructor
			this.Clear();
		}

		//**************************************************************************************************************
		public Clinic(int clinicid, string Urole)
		{
			//Default constructor using the caseworkerid to populate the parameters
			if(clinicid == 0)
			{
				return;
			}
			else
			{
				DataSet myData;
				SqlParameter[] arrParams = new SqlParameter[2];

				arrParams[0] = new SqlParameter("@ClinicID", SqlDbType.Int);
				arrParams[0].Value = clinicid;

				arrParams[1] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
				arrParams[1].Value = Urole;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetClinicProfile2", arrParams);
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
		public Clinic(GIPAP_Objects.User ClinicUser)
		{
			//Default constructor using the caseworkerid to populate the parameters
			if(ClinicUser.UserID == 0)
			{
				return;
			}
			else
			{
				DataSet myData;
				SqlParameter[] arrParams = new SqlParameter[1];

				arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
				arrParams[0].Value = ClinicUser.UserID;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetClinicProfileByUser", arrParams);
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

		//**********************************************************************************************************************
		public void Create(string createdby)
		{
			if(this.ClinicID == 0)
			{
				//The clinic does not exist.
				SqlParameter[] arrParams = new SqlParameter[34];

				arrParams[0] = new SqlParameter("@ClinicName", SqlDbType.NVarChar, 200);
				arrParams[0].Value = this.ClinicName;

				arrParams[1] = new SqlParameter("@Department", SqlDbType.NVarChar, 50);
				arrParams[1].Value = this.Department;

				arrParams[2] = new SqlParameter("@Street1", SqlDbType.NVarChar, 50);
				arrParams[2].Value = this.Street1;

				arrParams[3] = new SqlParameter("@Street2", SqlDbType.NVarChar, 50);
				arrParams[3].Value = this.Street2;

				arrParams[4] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
				arrParams[4].Value = this.City;

				arrParams[5] = new SqlParameter("@StateProvince", SqlDbType.NVarChar, 50);
				arrParams[5].Value = this.StateProvince;

				arrParams[6] = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10);
				arrParams[6].Value = this.PostalCode;

				arrParams[7] = new SqlParameter("@CountryID", SqlDbType.Int);
				arrParams[7].Value = this.CountryID;

				arrParams[8] = new SqlParameter("@Phone", SqlDbType.NVarChar, 30);
				arrParams[8].Value = this.Phone;

				arrParams[9] = new SqlParameter("@Fax", SqlDbType.NVarChar, 30);
				arrParams[9].Value = this.Fax;

				arrParams[10] = new SqlParameter("@Email", SqlDbType.NVarChar, 500);
				arrParams[10].Value = this.Email;

				arrParams[11] = new SqlParameter("@AdminFirstName", SqlDbType.NVarChar, 50);
				arrParams[11].Value = this.AdminFirstName;

				arrParams[12] = new SqlParameter("@AdminLastName", SqlDbType.NVarChar, 50);
				arrParams[12].Value = this.AdminLastName;

				arrParams[13] = new SqlParameter("@AdminPhone", SqlDbType.NVarChar, 50);
				arrParams[13].Value = this.AdminPhone;

				arrParams[14] = new SqlParameter("@AdminFax", SqlDbType.NVarChar, 50);
				arrParams[14].Value = this.AdminFax;

				arrParams[15] = new SqlParameter("@AdminEmail", SqlDbType.NVarChar, 500);
				arrParams[15].Value = this.AdminEmail;

				arrParams[16] = new SqlParameter("@CMLYear1", SqlDbType.Int);
				arrParams[16].Value = this.CMLYear1;

				arrParams[17] = new SqlParameter("@CMLYear2", SqlDbType.Int);
				arrParams[17].Value = this.CMLYear2;

				arrParams[18] = new SqlParameter("@CMLYear3", SqlDbType.Int);
				arrParams[18].Value = this.CMLYear3;

				arrParams[19] = new SqlParameter("@CMLYear4", SqlDbType.Int);
				arrParams[19].Value = this.CMLYear4;

				arrParams[20] = new SqlParameter("@CMLYear5", SqlDbType.Int);
				arrParams[20].Value = this.CMLYear5;

				arrParams[21] = new SqlParameter("@CMLYear6", SqlDbType.Int);
				arrParams[21].Value = this.CMLYear6;

				arrParams[22] = new SqlParameter("@CMLYear7", SqlDbType.Int);
				arrParams[22].Value = this.CMLYear7;

				arrParams[23] = new SqlParameter("@CMLYear8", SqlDbType.Int);
				arrParams[23].Value = this.CMLYear8;

				arrParams[24] = new SqlParameter("@CMLYear9", SqlDbType.Int);
				arrParams[24].Value = this.CMLYear9;

				arrParams[25] = new SqlParameter("@CMLYear10", SqlDbType.Int);
				arrParams[25].Value = this.CMLYear10;

				arrParams[26] = new SqlParameter("@GISTYear1", SqlDbType.Int);
				arrParams[26].Value = this.GISTYear1;

				arrParams[27] = new SqlParameter("@GISTYear2", SqlDbType.Int);
				arrParams[27].Value = this.GISTYear2;

				arrParams[28] = new SqlParameter("@GISTYear3", SqlDbType.Int);
				arrParams[28].Value = this.GISTYear3;

				arrParams[29] = new SqlParameter("@GISTYear4", SqlDbType.Int);
				arrParams[29].Value = this.GISTYear4;

				arrParams[30] = new SqlParameter("@GISTYear5", SqlDbType.Int);
				arrParams[30].Value = this.GISTYear5;

				arrParams[31] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
				arrParams[31].Value = createdby;

				arrParams[32] = new SqlParameter("@ClinicID", SqlDbType.Int);
				arrParams[32].Direction = ParameterDirection.Output;

				arrParams[33] = new SqlParameter("@ClinicApplicantID", SqlDbType.Int);
				arrParams[33].Value = 0;

				//Send the data to the database
				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateClinic", arrParams);

				this.ClinicID = (int)arrParams[32].Value;

				this.CreatePS(createdby);				
			}
			else
			{
				throw new ArgumentException("The clinic already exists.");
			}
		}
		//**********************************************************************************************************************
		public void CreatePS(string createdby)
		{
			//The clinic does not exist.
			SqlParameter[] arrParams = new SqlParameter[12];

			arrParams[0] = new SqlParameter("@ClinicName", SqlDbType.NVarChar, 200);
			arrParams[0].Value = this.ClinicName;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = createdby;

			arrParams[2] = new SqlParameter("@Address1", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.Street1;

			arrParams[3] = new SqlParameter("@Address2", SqlDbType.NVarChar, 50);
			arrParams[3].Value = this.Street2;

			arrParams[4] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
			arrParams[4].Value = this.City;

			arrParams[5] = new SqlParameter("@State", SqlDbType.NVarChar, 50);
			arrParams[5].Value = this.StateProvince;

			arrParams[6] = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10);
			arrParams[6].Value = this.PostalCode;

			arrParams[7] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[7].Value = this.CountryID;

			arrParams[8] = new SqlParameter("@Phone", SqlDbType.NVarChar, 30);
			arrParams[8].Value = this.Phone;

			arrParams[9] = new SqlParameter("@Fax", SqlDbType.NVarChar, 30);
			arrParams[9].Value = this.Fax;

			arrParams[10] = new SqlParameter("@Email", SqlDbType.NVarChar, 500);
			arrParams[10].Value = this.Email;

			arrParams[11] = new SqlParameter("@ClinicID", SqlDbType.Int);
			arrParams[11].Value = this.ClinicID;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_CreateGIPAPClinic", arrParams);
		}

		//**********************************************************************************************************************
		public void Update(string modifiedby)
		{
			//Update the clinics information.
			SqlParameter[] arrParams = new SqlParameter[33];

			arrParams[0] = new SqlParameter("@ClinicName", SqlDbType.NVarChar, 200);
			arrParams[0].Value = this.ClinicName;

			arrParams[1] = new SqlParameter("@Department", SqlDbType.NVarChar, 50);
			arrParams[1].Value = this.Department;

			arrParams[2] = new SqlParameter("@Street1", SqlDbType.NVarChar, 50);
			arrParams[2].Value = this.Street1;

			arrParams[3] = new SqlParameter("@Street2", SqlDbType.NVarChar, 50);
			arrParams[3].Value = this.Street2;

			arrParams[4] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
			arrParams[4].Value = this.City;

			arrParams[5] = new SqlParameter("@StateProvince", SqlDbType.NVarChar, 50);
			arrParams[5].Value = this.StateProvince;

			arrParams[6] = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10);
			arrParams[6].Value = this.PostalCode;

			arrParams[7] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[7].Value = this.CountryID;

			arrParams[8] = new SqlParameter("@Phone", SqlDbType.NVarChar, 30);
			arrParams[8].Value = this.Phone;

			arrParams[9] = new SqlParameter("@Fax", SqlDbType.NVarChar, 30);
			arrParams[9].Value = this.Fax;

			arrParams[10] = new SqlParameter("@Email", SqlDbType.NVarChar, 500);
			arrParams[10].Value = this.Email;

			arrParams[11] = new SqlParameter("@AdminFirstName", SqlDbType.NVarChar, 50);
			arrParams[11].Value = this.AdminFirstName;

			arrParams[12] = new SqlParameter("@AdminLastName", SqlDbType.NVarChar, 50);
			arrParams[12].Value = this.AdminLastName;

			arrParams[13] = new SqlParameter("@AdminPhone", SqlDbType.NVarChar, 50);
			arrParams[13].Value = this.AdminPhone;

			arrParams[14] = new SqlParameter("@AdminFax", SqlDbType.NVarChar, 50);
			arrParams[14].Value = this.AdminFax;

			arrParams[15] = new SqlParameter("@AdminEmail", SqlDbType.NVarChar, 500);
			arrParams[15].Value = this.AdminEmail;

			arrParams[16] = new SqlParameter("@CMLYear1", SqlDbType.Int);
			arrParams[16].Value = this.CMLYear1;

			arrParams[17] = new SqlParameter("@CMLYear2", SqlDbType.Int);
			arrParams[17].Value = this.CMLYear2;

			arrParams[18] = new SqlParameter("@CMLYear3", SqlDbType.Int);
			arrParams[18].Value = this.CMLYear3;

			arrParams[19] = new SqlParameter("@CMLYear4", SqlDbType.Int);
			arrParams[19].Value = this.CMLYear4;

			arrParams[20] = new SqlParameter("@CMLYear5", SqlDbType.Int);
			arrParams[20].Value = this.CMLYear5;

			arrParams[21] = new SqlParameter("@CMLYear6", SqlDbType.Int);
			arrParams[21].Value = this.CMLYear6;

			arrParams[22] = new SqlParameter("@CMLYear7", SqlDbType.Int);
			arrParams[22].Value = this.CMLYear7;

			arrParams[23] = new SqlParameter("@CMLYear8", SqlDbType.Int);
			arrParams[23].Value = this.CMLYear8;

			arrParams[24] = new SqlParameter("@CMLYear9", SqlDbType.Int);
			arrParams[24].Value = this.CMLYear9;

			arrParams[25] = new SqlParameter("@CMLYear10", SqlDbType.Int);
			arrParams[25].Value = this.CMLYear10;

			arrParams[26] = new SqlParameter("@GISTYear1", SqlDbType.Int);
			arrParams[26].Value = this.GISTYear1;

			arrParams[27] = new SqlParameter("@GISTYear2", SqlDbType.Int);
			arrParams[27].Value = this.GISTYear2;

			arrParams[28] = new SqlParameter("@GISTYear3", SqlDbType.Int);
			arrParams[28].Value = this.GISTYear3;

			arrParams[29] = new SqlParameter("@GISTYear4", SqlDbType.Int);
			arrParams[29].Value = this.GISTYear4;

			arrParams[30] = new SqlParameter("@GISTYear5", SqlDbType.Int);
			arrParams[30].Value = this.GISTYear5;

			arrParams[31] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[31].Value = modifiedby;

			arrParams[32] = new SqlParameter("@ClinicID", SqlDbType.Int);
			arrParams[32].Value = this.ClinicID;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateClinic", arrParams);

            this.UpdatePS(modifiedby);
        }
        //**********************************************************************************************************************
        private void UpdatePS(string modifiedby)
        {
            //The clinic does not exist.
            SqlParameter[] arrParams = new SqlParameter[12];

            arrParams[0] = new SqlParameter("@ClinicName", SqlDbType.NVarChar, 200);
            arrParams[0].Value = this.ClinicName;

            arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
            arrParams[1].Value = modifiedby;

            arrParams[2] = new SqlParameter("@Address1", SqlDbType.NVarChar, 50);
            arrParams[2].Value = this.Street1;

            arrParams[3] = new SqlParameter("@Address2", SqlDbType.NVarChar, 50);
            arrParams[3].Value = this.Street2;

            arrParams[4] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
            arrParams[4].Value = this.City;

            arrParams[5] = new SqlParameter("@State", SqlDbType.NVarChar, 50);
            arrParams[5].Value = this.StateProvince;

            arrParams[6] = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10);
            arrParams[6].Value = this.PostalCode;

            arrParams[7] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[7].Value = this.CountryID;

            arrParams[8] = new SqlParameter("@Phone", SqlDbType.NVarChar, 30);
            arrParams[8].Value = this.Phone;

            arrParams[9] = new SqlParameter("@Fax", SqlDbType.NVarChar, 30);
            arrParams[9].Value = this.Fax;

            arrParams[10] = new SqlParameter("@Email", SqlDbType.NVarChar, 500);
            arrParams[10].Value = this.Email;

            arrParams[11] = new SqlParameter("@ClinicID", SqlDbType.Int);
            arrParams[11].Value = this.ClinicID;

            //Send the data to the database
            SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_UpdateGIPAPClinic", arrParams);
        }
		//**********************************************************************************************************************
		public void Create(string createdby, GIPAP_Objects.ClinicApplicant myApplicant)
		{
			//The clinic does not exist.
			SqlParameter[] arrParams = new SqlParameter[34];

			arrParams[0] = new SqlParameter("@ClinicName", SqlDbType.NVarChar, 50);
			arrParams[0].Value = myApplicant.ClinicName;

			arrParams[1] = new SqlParameter("@Department", SqlDbType.NVarChar, 50);
			arrParams[1].Value = myApplicant.Department;

			arrParams[2] = new SqlParameter("@Street1", SqlDbType.NVarChar, 50);
			arrParams[2].Value = myApplicant.Street1;

			arrParams[3] = new SqlParameter("@Street2", SqlDbType.NVarChar, 50);
			arrParams[3].Value = myApplicant.Street2;

			arrParams[4] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
			arrParams[4].Value = myApplicant.City;

			arrParams[5] = new SqlParameter("@StateProvince", SqlDbType.NVarChar, 50);
			arrParams[5].Value = myApplicant.StateProvince;

			arrParams[6] = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 10);
			arrParams[6].Value = myApplicant.PostalCode;

			arrParams[7] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[7].Value = myApplicant.CountryID;

			arrParams[8] = new SqlParameter("@Phone", SqlDbType.NVarChar, 30);
			arrParams[8].Value = myApplicant.Phone;

			arrParams[9] = new SqlParameter("@Fax", SqlDbType.NVarChar, 30);
			arrParams[9].Value = myApplicant.Fax;

			arrParams[10] = new SqlParameter("@Email", SqlDbType.NVarChar, 500);
			arrParams[10].Value = myApplicant.Email;

			arrParams[11] = new SqlParameter("@AdminFirstName", SqlDbType.NVarChar, 50);
			arrParams[11].Value = myApplicant.AdminFirstName;

			arrParams[12] = new SqlParameter("@AdminLastName", SqlDbType.NVarChar, 50);
			arrParams[12].Value = myApplicant.AdminLastName;

			arrParams[13] = new SqlParameter("@AdminPhone", SqlDbType.NVarChar, 50);
			arrParams[13].Value = myApplicant.AdminPhone;

			arrParams[14] = new SqlParameter("@AdminFax", SqlDbType.NVarChar, 50);
			arrParams[14].Value = myApplicant.AdminFax;

			arrParams[15] = new SqlParameter("@AdminEmail", SqlDbType.NVarChar, 500);
			arrParams[15].Value = myApplicant.AdminEmail;

			arrParams[16] = new SqlParameter("@CMLYear1", SqlDbType.Int);
			arrParams[16].Value = myApplicant.CMLYear1;

			arrParams[17] = new SqlParameter("@CMLYear2", SqlDbType.Int);
			arrParams[17].Value = myApplicant.CMLYear2;

			arrParams[18] = new SqlParameter("@CMLYear3", SqlDbType.Int);
			arrParams[18].Value = myApplicant.CMLYear3;

			arrParams[19] = new SqlParameter("@CMLYear4", SqlDbType.Int);
			arrParams[19].Value = myApplicant.CMLYear4;

			arrParams[20] = new SqlParameter("@CMLYear5", SqlDbType.Int);
			arrParams[20].Value = myApplicant.CMLYear5;

			arrParams[21] = new SqlParameter("@CMLYear6", SqlDbType.Int);
			arrParams[21].Value = myApplicant.CMLYear6;

			arrParams[22] = new SqlParameter("@CMLYear7", SqlDbType.Int);
			arrParams[22].Value = myApplicant.CMLYear7;

			arrParams[23] = new SqlParameter("@CMLYear8", SqlDbType.Int);
			arrParams[23].Value = myApplicant.CMLYear8;

			arrParams[24] = new SqlParameter("@CMLYear9", SqlDbType.Int);
			arrParams[24].Value = myApplicant.CMLYear9;

			arrParams[25] = new SqlParameter("@CMLYear10", SqlDbType.Int);
			arrParams[25].Value = myApplicant.CMLYear10;

			arrParams[26] = new SqlParameter("@GISTYear1", SqlDbType.Int);
			arrParams[26].Value = myApplicant.GISTYear1;

			arrParams[27] = new SqlParameter("@GISTYear2", SqlDbType.Int);
			arrParams[27].Value = myApplicant.GISTYear2;

			arrParams[28] = new SqlParameter("@GISTYear3", SqlDbType.Int);
			arrParams[28].Value = myApplicant.GISTYear3;

			arrParams[29] = new SqlParameter("@GISTYear4", SqlDbType.Int);
			arrParams[29].Value = myApplicant.GISTYear4;

			arrParams[30] = new SqlParameter("@GISTYear5", SqlDbType.Int);
			arrParams[30].Value = myApplicant.GISTYear5;

			arrParams[31] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[31].Value = createdby;

			arrParams[32] = new SqlParameter("@ClinicID", SqlDbType.Int);
			arrParams[32].Direction = ParameterDirection.Output;

			arrParams[33] = new SqlParameter("@ClinicApplicantID", SqlDbType.Int);
			arrParams[33].Value = myApplicant.ClinicApplicantID;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateClinic", arrParams);

			this.ClinicID = (int)arrParams[32].Value;
		}
		//**********************************************************************************************************************
		public void Approve(string modifiedby)
		{

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@ClinicID", SqlDbType.Int);
			arrParams[0].Value = this.ClinicID;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
			arrParams[1].Value = modifiedby;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ApproveClinic", arrParams);
		}
		//**********************************************************************************************************************
		public void UnApprove(string modifiedby)
		{

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@ClinicID", SqlDbType.Int);
			arrParams[0].Value = this.ClinicID;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
			arrParams[1].Value = modifiedby;

			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UnApproveClinic", arrParams);
        }
        //**********************************************************************************************************************
        public DataSet getClinicDataSets(int cid, string dset)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@ClinicID", SqlDbType.Int);
            arrParams[0].Value = cid;

            arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
            arrParams[1].Value = dset;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getClinicDataSets", arrParams);
        }
		//**********************************************************************************************************************
		public DataSet getClinicPhysicians(int cid)
		{
			//The clinic does not exist.
			SqlParameter arrParams = new SqlParameter();

			arrParams = new SqlParameter("@ClinicID", SqlDbType.Int);
			arrParams.Value = cid;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getClinicPhysicians", arrParams);
		}
		//**************************************************************************************************************
		public void AddClinicNote(string createdby, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@ClinicID", SqlDbType.Int);
			arrParams[0].Value = this.ClinicID;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = createdby;

			arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
			arrParams[2].Value = note;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateClinicNote", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet getClinicPatients(int CID, string Urole)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@ClinicID", SqlDbType.Int);
			arrParams[0].Value = CID;

			arrParams[1] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
			arrParams[1].Value = Urole;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getClinicPatients2", arrParams);
		}
        //**********************************************************************************************************************
        public string ClinicHeader()
        {
            string cInfo = "<h1><font color=steelblue>" + this.ClinicName + "</font></h1>";

            return cInfo;
        }
		//**********************************************************************************************************************
        public string ClinicInfo(string Urole)
		{
			string cInfo = "<font class='lbl'>Department: </font>" + this.Department + "<br>";
			cInfo += "<font class='lbl'>Address: </font>" + this.Street1 + "<br>" + this.Street2 + "<br>";
			cInfo += "<font class='lbl'>City: </font>" + this.City + "<br>";
			cInfo += "<font class='lbl'>State / Province: </font>" + this.StateProvince + "<br>";
			cInfo += "<font class='lbl'>Postal Code: </font>" + this.PostalCode + "<br>";
			cInfo += "<font class='lbl'>Country: </font>" + this.CountryName + "<br>";
			cInfo += "<font class='lbl'>Phone: </font>" + this.Phone + "<br>";
			cInfo += "<font class='lbl'>Fax: </font>" + this.Fax + "<br>";
			cInfo += "<font class='lbl'>Email: </font>" + this.Email + "<br>";
            if (Urole == "TMFUser" && this.ClinicDS.Tables[3].Rows.Count > 0)
            {
                cInfo += "<br><font class='lbl'>Username: </font>" + this.ClinicDS.Tables[3].Rows[0]["username"].ToString();
                cInfo += "<br><font class='lbl'>Password: </font>" + this.ClinicDS.Tables[3].Rows[0]["password"].ToString();
            }
			return cInfo;
		}
        //**********************************************************************************************************************
        public string ApprovedInfo(string Urole)
        {
            string aInfo = "";
            /*if (Urole == "TMFUser")
            {*/
            aInfo += "<font class='lbl'>Approved: </font>";
                if (this.Approved == 1)
                {
                    aInfo += "<font color=steelblue>Yes</font>";
                    if (this.ApprovedDate != Convert.ToDateTime("1/1/0001"))
                    {
                        aInfo += "<br><font class='lbl'>Approved Date: </font>" + this.ApprovedDate.Day.ToString() + " " + this.ApprovedDate.ToString("y");
                    }
                }
                else if (this.Approved == 0)
                {
                    aInfo += "<font color=red>No</font>";
                }
                else if (this.Approved == 2)
                {
                    aInfo += "<font color=red>Pending</font>";
                }
            //}
            return aInfo;
        }
		//**********************************************************************************************************************
		public string AdminInfo(string Urole)
		{
			string aInfo = "";
            aInfo += "<font class='lbl'>Administrator: </font><font color=steelblue>" + this.AdminFirstName + " " + this.AdminLastName + "</font><br>";
			aInfo += "<font class='lbl'>Phone: </font>" + this.AdminPhone + "<br>";
			aInfo += "<font class='lbl'>Fax: </font>" + this.AdminFax + "<br>";
			aInfo += "<font class='lbl'>Email: </font>" + this.AdminEmail + "<br>";
			
			return aInfo;
		}
		//**********************************************************************************************************************
		public string TreatmentInfo()
		{
			string tInfo = "<b><font color=steelblue>Number of CML Patients Treated</font></b><br>";
			int yr = Convert.ToInt32(this.CreateDate.Year.ToString()) - 1;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear1.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear2.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear3.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear4.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear5.ToString() + "<br>";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear6.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear7.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear8.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear9.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.CMLYear10.ToString() + "<br><br>";

			tInfo += "<b><font color=steelblue>Number of GIST Patients Treated</font></b><br>";
			yr = Convert.ToInt32(this.CreateDate.Year.ToString()) - 1;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.GISTYear1.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.GISTYear2.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.GISTYear3.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.GISTYear4.ToString() + "  ";
			yr--;
			tInfo += "<b>" + yr.ToString() + ":</b>" + this.GISTYear5.ToString();
			
			return tInfo;
		}
		//**********************************************************************************************************************
		public DataSet GetClinicList()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetClinicList");
		}
		//**********************************************************************************************************************
        public DataSet ClinicSearch(GIPAP_Objects.User searchU)
		{
			DataSet ds = new DataSet();
			string strSQL = "Select '<a href=ClinicInfo.aspx?choice=' + Convert(nvarchar, clinicid) + '>' + ClinicName + '</a>' as '<b>Clinic</b>'";
			strSQL += ", City as '<b>City</>', Replace(Replace(Replace(Approved, 0, 'No'), 1, 'Yes'), 2, 'Pending') as '<b>Approved</b>' from tblClinic where ";
			strSQL += "ClinicName like '" + this.ClinicName + "%' and ";
			strSQL += "Phone like '" + this.Phone + "%' and ";
			strSQL += "Fax like '" + this.Fax + "%' and ";
			if(this.Approved != -1)
			{
				strSQL += "approved = " + this.Approved.ToString() + " and ";
			}
			strSQL += "AdminEmail like '" + this.AdminEmail + "%' and ";
			if (this.CountryID != 0)
			{
				strSQL += "CountryID = " + this.CountryID.ToString() + " and ";
			}
            if (searchU.Role == "MaxStation")
            {
                if (searchU.CountryID == 76)
                {
                    strSQL += " countryid = 76 and ";
                }
                else
                {
                    strSQL += "clinicid in ( select b.clinicid from tblphysicianclinic b, tblpatientphysician c, tblMaxStation d, tblperson e where b.personid=c.personid and c.PATIENTID = d.PATIENTID and d.PERSONID=e.personid and e.userid=" + searchU.UserID.ToString() + ") and ";
                }
            }
            strSQL += "City like '" + this.City + "%' and ";
			strSQL += "AdminFirstName like '" + this.AdminFirstName + "%' and ";
			strSQL += "AdminLastName like '" + this.AdminLastName + "%' and ";
			strSQL += "AdminPhone like '" + this.AdminPhone + "%' and ";
			strSQL += "AdminFax like '" + this.AdminFax + "%' and ";
			strSQL += "AdminEmail like '" + this.AdminEmail + "%' ";
            strSQL += "Order by ClinicName";

			ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, strSQL);
			return ds;
		}
		//**********************************************************************************************************************
		public GIPAP_Objects.Email createApprovalEmail()
		{
			GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
			DateTime dt = DateTime.Now;
			myEmail.From = "gipap@themaxfoundation.org";
			myEmail.CC = "gipap@themaxfoundation.org";
			myEmail.To = "gipap@themaxfoundation.org; ";
			if(this.AdminEmail == "")
			{
				if(this.Email != "")
				{
					myEmail.To += this.Email;
				}
			}
			else
			{
				myEmail.To += this.AdminEmail;
			}
			myEmail.Subject = "New GIPAP Clinic Administrator Approval Letter";
			myEmail.Message += "NEW GIPAP CLINIC ADMINISTRATOR APPROVAL NOTICE\n\n";
			if(this.AdminFirstName != "")
			{
				myEmail.Message += this.AdminFirstName + " " + this.AdminLastName + "\n" + this.ClinicName + "\n";
			}
			else
			{
				myEmail.Message += this.ClinicName + "\n";
			}
			if(this.Street1 != "")
			{
				myEmail.Message += this.Street1 + "\n";
			}
			if(this.Street2 != "")
			{
				myEmail.Message += this.Street2 + "\n";
			}
			if(this.AdminPhone == "")
			{
				if(this.Phone != "")
				{
					myEmail.Message += this.Phone + "\n";
				}
			}
			else
			{
				myEmail.Message += this.AdminPhone + "\n";
			}
			if(this.AdminFax == "")
			{
				if(this.Fax != "")
				{
					myEmail.Message += this.Fax + "\n";
				}
			}
			else
			{
				myEmail.Message += this.AdminFax + "\n";
			}
			myEmail.Message += "\n" + dt.Day.ToString() + " " + dt.ToString("y") + "\n\n";
			if(this.AdminFirstName != "")
			{
				myEmail.Message += "Dear " + this.AdminFirstName + " " + this.AdminLastName + ":\n\n";
			}
			else
			{
				myEmail.Message += "Dear " + this.ClinicName + " Representative:\n\n";
			}
			myEmail.Message += "We are happy to inform you that you have been designated by The Max Foundation as the Admin person for " + this.ClinicName + " in GIPAP.\n\n";
			myEmail.Message += "You may review and modify your clinic information by logging on to www.maxaid.org, or through our website, www.themaxfoundation.org. (Click on the link for GIPAP/Glivec).\n\n";
			myEmail.Message += "This will allow you to:\n\n";
			myEmail.Message += "-	view and modify clinic information\n";
			myEmail.Message += "-	view a list of physicians from your clinic registered\n";
			myEmail.Message += "-	request that a new physician be added to this clinic\n\n";
			myEmail.Message += "Your clinic administrator username and password are listed below:\n\n";
			myEmail.Message += "UserName: " + this.ClinicDS.Tables[3].Rows[0]["username"].ToString() + "\n";
			myEmail.Message += "Password: " + this.ClinicDS.Tables[3].Rows[0]["password"].ToString() + "\n\n";
			myEmail.Message += "If you have any questions or concerns, please email us at gipap@themaxfoundation.org.\n\n";
			myEmail.Message += "Regards,\n\n";
			myEmail.Message += "The Max Foundation";
			return myEmail;
		}
		//**********************************************************************************************************************
		public GIPAP_Objects.Email createApprovalEmailtoCPO()
		{
			GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email();
			DateTime dt = DateTime.Now;
			myEmail.From = "gipap@themaxfoundation.org";			
			myEmail.CC = "gipap@themaxfoundation.org; ";
			if(this.ClinicDS.Tables[4].Rows.Count > 0)
			{
				for(int i=0; i<this.ClinicDS.Tables[4].Rows.Count; i++)
				{
					myEmail.CC += this.ClinicDS.Tables[4].Rows[i]["email"].ToString() + "; ";
				}
			}
			myEmail.To = "gipap@themaxfoundation.org; " + this.CPOEmail;
			myEmail.Subject = "GIPAP Clinic Approval Notification (Novartis)";
			myEmail.Message += "GIPAP CLINLIC APPROVAL NOTICE (NOVARTIS)\n\n";
			myEmail.Message += dt.Day.ToString() + " " + dt.ToString("y") + "\n\n";
			if(this.AdminFirstName != "")
			{
				myEmail.Message += "Clinic Administrator: " + this.AdminFirstName + " " + this.AdminLastName + "\nClinic: " + this.ClinicName + "\n";
			}
			else
			{
				myEmail.Message += "Clinic: " + this.ClinicName + "\n";
			}
			myEmail.Message += "Country: " + this.ClinicDS.Tables[1].Rows[0]["country"].ToString() + "\n\n";
			myEmail.Message += "We would like to notify you that we have registered ";
			myEmail.Message += this.ClinicName;
			myEmail.Message += " as a qualified GIPAP center per your request.\n\n";
			myEmail.Message += "Please update your records accordingly.\n\n";
			myEmail.Message += "If you have any questions or concerns, please do not hesitate to communicate with us at Gipap@themaxfoundation.org.\n\n";
			myEmail.Message += "Regards,\n\n";
			myEmail.Message += "The Max Foundation";
			return myEmail;
		}
		//**************************************************************************************************************
		public void Clear()
		{
			this.ClinicID = 0;
			this.ClinicName = "";
			this.Department = "";
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
			this.AdminFirstName = "";
			this.AdminLastName = "";
			this.AdminPhone = "";
			this.AdminFax = "";
			this.AdminEmail = "";
			this.CMLYear1 = 0;
			this.CMLYear2 = 0;
			this.CMLYear3 = 0;
			this.CMLYear4 = 0;
			this.CMLYear5 = 0;
			this.CMLYear6 = 0;
			this.CMLYear7 = 0;
			this.CMLYear8 = 0;
			this.CMLYear9 = 0;
			this.CMLYear10 = 0;
			this.GISTYear1 = 0;
			this.GISTYear2 = 0;
			this.GISTYear3 = 0;
			this.GISTYear4 = 0;
			this.GISTYear5 = 0;
			this.CheckedOut = false;
			this.Approved = 2;
			this.UserID = 0;
			this.CPOEmail = "";
		}

		//**************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			//Populates the objects parameters with the data returned from the database
			this.ClinicID = Convert.ToInt32(ds.Tables[0].Rows[0]["ClinicID"]);
			this.ClinicName = Convert.ToString(ds.Tables[0].Rows[0]["ClinicName"]);
			this.Department = Convert.ToString(ds.Tables[0].Rows[0]["Department"]);
			this.Street1 = Convert.ToString(ds.Tables[0].Rows[0]["Street1"]);
			this.Street2 = Convert.ToString(ds.Tables[0].Rows[0]["Street2"]);
			this.City = Convert.ToString(ds.Tables[0].Rows[0]["City"]);
			this.StateProvince = Convert.ToString(ds.Tables[0].Rows[0]["StateProvince"]);
			this.PostalCode = Convert.ToString(ds.Tables[0].Rows[0]["PostalCode"]);
			this.Phone = Convert.ToString(ds.Tables[0].Rows[0]["Phone"]);
			this.Fax = Convert.ToString(ds.Tables[0].Rows[0]["Fax"]);
			this.Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
			this.AdminFirstName = Convert.ToString(ds.Tables[0].Rows[0]["AdminFirstName"]);
			this.AdminLastName = Convert.ToString(ds.Tables[0].Rows[0]["AdminLastName"]);
			this.AdminPhone = Convert.ToString(ds.Tables[0].Rows[0]["AdminPhone"]);
			this.AdminFax = Convert.ToString(ds.Tables[0].Rows[0]["AdminFax"]);
			this.AdminEmail = Convert.ToString(ds.Tables[0].Rows[0]["AdminEmail"]);
			this.CMLYear1 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear1"]);
			this.CMLYear2 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear2"]);
			this.CMLYear3 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear3"]);
			this.CMLYear4 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear4"]);
			this.CMLYear5 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear5"]);
			this.CMLYear6 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear6"]);
			this.CMLYear7 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear7"]);
			this.CMLYear8 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear8"]);
			this.CMLYear9 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear9"]);
			this.CMLYear10 = Convert.ToInt32(ds.Tables[0].Rows[0]["CMLYear10"]);
			this.GISTYear1 = Convert.ToInt32(ds.Tables[0].Rows[0]["GISTYear1"]);
			this.GISTYear2 = Convert.ToInt32(ds.Tables[0].Rows[0]["GISTYear2"]);
			this.GISTYear3 = Convert.ToInt32(ds.Tables[0].Rows[0]["GISTYear3"]);
			this.GISTYear4 = Convert.ToInt32(ds.Tables[0].Rows[0]["GISTYear4"]);
			this.GISTYear5 = Convert.ToInt32(ds.Tables[0].Rows[0]["GISTYear5"]);
			//this.CheckedOut = Convert.ToBoolean(ds.Tables[0].Rows[0]["CheckedOut"]);
			this.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateDate"]);
			this.Approved = Convert.ToInt32(ds.Tables[0].Rows[0]["Approved"]);
			try
			{
				this.ApprovedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ApprovedDate"]);
			}
			catch{}
			if(ds.Tables[0].Rows[0]["UserID"] == DBNull.Value)
			{
				this.UserID = 0;
			}
			else
			{
				this.UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);
			}

			this.CountryID = Convert.ToInt32(ds.Tables[1].Rows[0]["CountryID"]);
			this.CountryName = ds.Tables[1].Rows[0]["CountryName"].ToString();

			if(ds.Tables[2].Rows.Count > 0)
			{
				for(int i=0; i<ds.Tables[2].Rows.Count; i++)
				{
					this.CPOEmail += ds.Tables[2].Rows[i]["email"].ToString() + "; ";
				}
			}

			this.ClinicDS = ds;
		}
		
		//**********************************************************************************************************************
		public int ClinicID
		{
			get{return mClinicID;}
			set{mClinicID = value;}
		}
		//**********************************************************************************************************************
		public string ClinicName
		{
			get{return mClinicName;}
			set{mClinicName = value;}
		}

		//**********************************************************************************************************************
		public string Department
		{
			get{return mDepartment;}
			set{mDepartment = value;}
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
		public string AdminFirstName
		{
			get{return mAdminFirstName;}
			set{mAdminFirstName = value;}
		}

		//**********************************************************************************************************************
		public string AdminLastName
		{
			get{return mAdminLastName;}
			set{mAdminLastName = value;}
		}

		//**********************************************************************************************************************
		public string AdminPhone
		{
			get{return mAdminPhone;}
			set{mAdminPhone = value;}
		}

		//**********************************************************************************************************************
		public string AdminFax
		{
			get{return mAdminFax;}
			set{mAdminFax= value;}
		}

		//**********************************************************************************************************************
		public string AdminEmail
		{
			get{return mAdminEmail;}
			set{mAdminEmail = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear1
		{
			get{return mCMLYear1;}
			set{mCMLYear1 = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear2
		{
			get{return mCMLYear2;}
			set{mCMLYear2 = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear3
		{
			get{return mCMLYear3;}
			set{mCMLYear3 = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear4
		{
			get{return mCMLYear4;}
			set{mCMLYear4 = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear5
		{
			get{return mCMLYear5;}
			set{mCMLYear5 = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear6
		{
			get{return mCMLYear6;}
			set{mCMLYear6 = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear7
		{
			get{return mCMLYear7;}
			set{mCMLYear7 = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear8
		{
			get{return mCMLYear8;}
			set{mCMLYear8 = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear9
		{
			get{return mCMLYear9;}
			set{mCMLYear9 = value;}
		}

		//**********************************************************************************************************************
		public int CMLYear10
		{
			get{return mCMLYear10;}
			set{mCMLYear10 = value;}
		}

		//**********************************************************************************************************************
		public int GISTYear1
		{
			get{return mGISTYear1;}
			set{mGISTYear1 = value;}
		}

		//**********************************************************************************************************************
		public int GISTYear2
		{
			get{return mGISTYear2;}
			set{mGISTYear2 = value;}
		}

		//**********************************************************************************************************************
		public int GISTYear3
		{
			get{return mGISTYear3;}
			set{mGISTYear3 = value;}
		}

		//**********************************************************************************************************************
		public int GISTYear4
		{
			get{return mGISTYear4;}
			set{mGISTYear4 = value;}
		}

		//**********************************************************************************************************************
		public int GISTYear5
		{
			get{return mGISTYear5;}
			set{mGISTYear5 = value;}
		}

		//**********************************************************************************************************************
		public bool CheckedOut
		{
			get{return mCheckedOut;}
			set{mCheckedOut = value;}
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
		public int UserID
		{
			get{return mUserID;}
			set{mUserID = value;}
		}
	}
}
