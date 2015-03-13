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
	public class ClinicApplicant
	{
		private int mClinicApplicantID;
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

		private DateTime CreateDate;
		private string CPOEmail;
		private string MaxEmail;

		private DataSet ClinicDS;
			
		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		//string connString = "SERVER=CRAIGA1;DATABASE=GIPAP2;PWD=secret;UID=sa;";

		//**************************************************************************************************************
		public ClinicApplicant()
		{
			//Default constructor
			this.Clear();
		}

		//**************************************************************************************************************
		public ClinicApplicant(int clinicid)
		{
			//Default constructor using the caseworkerid to populate the parameters
			if(clinicid == 0)
			{
				return;
			}
			else
			{
				DataSet myData;
				SqlParameter[] arrParams = new SqlParameter[1];

				arrParams[0] = new SqlParameter("@ClinicApplicantID", SqlDbType.Int);
				arrParams[0].Value = clinicid;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetClinicApplicantProfile", arrParams);
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
			//The clinic does not exist.
			SqlParameter[] arrParams = new SqlParameter[34];

			arrParams[0] = new SqlParameter("@ClinicName", SqlDbType.NVarChar, 50);
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

			arrParams[33] = new SqlParameter("@Country", SqlDbType.NVarChar, 50);
			arrParams[33].Direction = ParameterDirection.Output;

			//Send the data to the database
			this.ClinicDS = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CreateClinicApplicant", arrParams);

			this.ClinicApplicantID = (int)arrParams[32].Value;
			this.mCountryName = arrParams[33].Value.ToString();
			if(this.ClinicDS.Tables[0].Rows.Count > 0)
			{
				for(int i=0; i<this.ClinicDS.Tables[0].Rows.Count; i++)
				{
					this.CPOEmail += this.ClinicDS.Tables[0].Rows[i]["email"].ToString() + "; ";
				}
			}
			if(this.ClinicDS.Tables[1].Rows.Count > 0)
			{
				for(int i=0; i<this.ClinicDS.Tables[1].Rows.Count; i++)
				{
					this.MaxEmail += this.ClinicDS.Tables[1].Rows[i]["email"].ToString() + "; ";
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
		}
		//**********************************************************************************************************************
		public GIPAP_Objects.Email ApplicantEmail()
		{
			GIPAP_Objects.Email myEmail = new Email();
			myEmail.To = "gipap@themaxfoundation.org; " + this.CPOEmail;
			myEmail.CC = this.MaxEmail;
			myEmail.From = "gipap@themaxfoundation.org";
			myEmail.Subject = "GIPAP Clinic Application for " + this.ClinicName;
			myEmail.Message = "Dear Novartis CPO:\n\n";
			myEmail.Message += "The following application was submitted for participation in GIPAP.  Please provide your input regarding qualification of this clinic for GIPAP.\n\n";
			myEmail.Message += "Clinic Name: " + this.ClinicName + "\n";
			myEmail.Message += "Department: " + this.Department + "\n";
			myEmail.Message += "Street 1: " + this.Street1 + "\n";
			myEmail.Message += "Street 2: " + this.Street2 + "\n";
			myEmail.Message += "City: " + this.City + "\n";
			myEmail.Message += "State: " + this.StateProvince + "\n";
			myEmail.Message += "Postal Code: " + this.PostalCode + "\n";
			myEmail.Message += "Country: " + this.mCountryName + "\n";
			myEmail.Message += "Phone: " + this.Phone + "\n";
			myEmail.Message += "Fax: " + this.Fax + "\n";
			myEmail.Message += "Email: " + this.Email + "\n\n";
			myEmail.Message += "CONTACT INFORMATION\n\n";
			myEmail.Message += "First Name: " + this.AdminFirstName + "\n";
			myEmail.Message += "Last Name: " + this.AdminLastName + "\n";
			myEmail.Message += "Phone: " + this.AdminPhone + "\n";
			myEmail.Message += "Fax: " + this.AdminFax + "\n";
			myEmail.Message += "Email: " + this.AdminEmail + "\n\n";
			myEmail.Message += "NUMBER OF CML PATIENTS TREATED IN THE PAST 10 YEARS\n\n";
			int yrNow = Convert.ToInt32(DateTime.Now.Year.ToString());
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear1.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear2.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear3.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear4.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear5.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear6.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear7.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear8.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear9.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.CMLYear10.ToString() + "\n\n";
			myEmail.Message += "NUMBER OF GIST PATIENTS TREATED IN THE PAST 10 YEARS\n\n";
			yrNow = Convert.ToInt32(DateTime.Now.Year.ToString());
			myEmail.Message += yrNow.ToString() + ": " + this.GISTYear1.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.GISTYear2.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.GISTYear3.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.GISTYear4.ToString() + "\n";
			yrNow--;
			myEmail.Message += yrNow.ToString() + ": " + this.GISTYear5.ToString();

			return myEmail;
		}
		//**********************************************************************************************************************
		public void Transfer(string transferedby)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@ClinicApplicantID", SqlDbType.Int);
			arrParams[0].Value = this.ClinicApplicantID;

			arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = transferedby;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_TransferClinicApplicant", arrParams);

		}
		//**********************************************************************************************************************
		public string ClinicInfo()
		{
			string cInfo = "<font size=4 color=steelblue>" + this.ClinicName + "</font><hr>";
			cInfo += "<b>Department: </b>" + this.Department + "<br>";
			cInfo += "<b>Address: </b>" + this.Street1 + "<br>" + this.Street2 + "<br>";
			cInfo += "<b>City: </b>" + this.City + "<br>";
			cInfo += "<b>State / Province: </b>" + this.StateProvince + "<br>";
			cInfo += "<b>Postal Code: </b>" + this.PostalCode + "<br>";
			cInfo += "<b>Country: </b>" + this.CountryName + "<br>";
			cInfo += "<b>Phone: </b>" + this.Phone + "<br>";
			cInfo += "<b>Fax: </b>" + this.Fax + "<br>";
			cInfo += "<b>Email: </b>" + this.Email + "<br><hr>";
			return cInfo;
		}
		//**********************************************************************************************************************
		public string AdminInfo()
		{
			string aInfo = "<b>Administrator: <font color=steelblue>" + this.AdminFirstName + " " + this.AdminLastName + "</font></b><br>";
			aInfo += "<b>Phone: </b>" + this.AdminPhone + "<br>";
			aInfo += "<b>Fax: </b>" + this.AdminFax + "<br>";
			aInfo += "<b>Email: </b>" + this.AdminEmail + "<br>";
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
		//**************************************************************************************************************
		public void Clear()
		{
			this.ClinicApplicantID = 0;
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
		}

		//**************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			//Populates the objects parameters with the data returned from the database
			this.ClinicApplicantID = Convert.ToInt32(ds.Tables[0].Rows[0]["ClinicID"]);
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

			this.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateDate"]);

			this.CountryID = Convert.ToInt32(ds.Tables[1].Rows[0]["CountryID"]);
			this.CountryName = ds.Tables[1].Rows[0]["CountryName"].ToString();

			this.ClinicDS = ds;
		}
		
		//**********************************************************************************************************************
		public int ClinicApplicantID
		{
			get{return mClinicApplicantID;}
			set{mClinicApplicantID = value;}
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
	}
}
