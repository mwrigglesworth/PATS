using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for DynamicSearch.
	/// </summary>
	public class DynamicSearch
	{
		private int mDynamicSearchID;
		private int mUserID;
		private string mReportName;
		private DateTime mCreateDate;
		private DateTime mLastRun;

		private string SQLQuerry;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];

		//**********************************************************************************************************************
		public DynamicSearch()
		{
			//
		}
		//**********************************************************************************************************************
		public DynamicSearch(int currID)
		{
			DataSet myData;
			SqlParameter arrParams = new SqlParameter();

			arrParams = new SqlParameter("@DynamicSearchID", SqlDbType.Int);
			arrParams.Value = currID;

			myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetDynamicSearchProfile", arrParams);
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
		//**********************************************************************************************************************
		public DataSet RunSearch(GIPAP_Objects.Patient myPatient, System.Web.UI.WebControls.ListBox lbFields, System.Web.UI.WebControls.ListBox lbSort, bool conts)
		{
			this.SQLQuerry = "Select PIN";
			if(lbFields.Items.Count > 1)
			{
				for(int i=1; i<lbFields.Items.Count; i++)
				{
					this.SQLQuerry += ", " + lbFields.Items[i].Value;
				}
			}
			this.SQLQuerry += " From v_DynamicPatientSearchNovartis ";
			if(conts)
			{
				this.SQLQuerry += "Where Pin like '%' ";
				if(myPatient.CountryID != 0)
				{
					this.SQLQuerry += " and countryid = " + myPatient.CountryID.ToString();
				}
				if(myPatient.GIPAPStatus != "0")
				{
					this.SQLQuerry += " and status = '" + myPatient.GIPAPStatus + "' ";
				}
				if(myPatient.Diagnosis != "0")
				{
					this.SQLQuerry += " and diagnosis = '" + myPatient.Diagnosis + "' ";
				}
				if(myPatient.OriginalCMLPhase != "0")
				{
					this.SQLQuerry += " and [initial phase] = '" + myPatient.OriginalCMLPhase + "' ";
				}
				if(myPatient.OriginalApprovedDosage != "0")
				{
					this.SQLQuerry += " and dosage = '" + myPatient.OriginalApprovedDosage + "' ";
				}
			}
			if(lbSort.Items.Count > 0)
			{
				this.SQLQuerry += "Order by ";
				if(lbSort.Items[0].Value == "[Last Reapproval Date]")
				{
					this.SQLQuerry += "startdate";
				}
				else if(lbSort.Items[0].Value == "[Initial Approval]" )
				{
					this.SQLQuerry += "iadate";
				}
				else if(lbSort.Items[0].Value == "[Closed Date]")
				{
					this.SQLQuerry += "closeddate";
				}
				else 
				{
					this.SQLQuerry += lbSort.Items[0].Value;
				}
				for(int i=1; i<lbSort.Items.Count; i++)
				{
					if(lbSort.Items[i].Value == "[Last Reapproval Date]")
					{
						this.SQLQuerry += ", startdate";
					}
					else if(lbSort.Items[i].Value == "[Initial Approval]" )
					{
						this.SQLQuerry += ", iadate";
					}
					else if(lbSort.Items[i].Value == "[Closed Date]")
					{
						this.SQLQuerry += ", closeddate";
					}
					else 
					{
						this.SQLQuerry += ", " + lbSort.Items[i].Value;
					}
				}
			}
			return SqlHelper.ExecuteDataset(connString, CommandType.Text, this.SQLQuerry);
		}
		//**************************************************************************************************************
		public DataSet RunReport()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.Text, this.SQLQuerry);
		}
		//**************************************************************************************************************
		public void SaveSearch()
		{
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
			arrParams[0].Value = this.UserID;

			arrParams[1] = new SqlParameter("@ReportName", SqlDbType.NVarChar, 100);
			arrParams[1].Value = this.ReportName;

			arrParams[2] = new SqlParameter("@SQLQuerry", SqlDbType.Text);
			arrParams[2].Value = this.SQLQuerry;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateDynamicSearch", arrParams);
		}
		//**************************************************************************************************************
		private void Inflate(DataSet ds)
		{
			//Populates the objects parameters with the data returned from the database
			this.UserID = (int)(ds.Tables[0].Rows[0]["userID"]);
			this.DynamicSearchID = (int)(ds.Tables[0].Rows[0]["dynamicsearchID"]);
			this.ReportName = (ds.Tables[0].Rows[0]["reportname"]).ToString();
			this.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["createdate"]);
			try
			{
				this.LastRun = Convert.ToDateTime(ds.Tables[0].Rows[0]["lastrun"]);
			}
			catch{}
			this.SQLQuerry = (ds.Tables[0].Rows[0]["sqlquerry"]).ToString();
		}
		//**************************************************************************************************************
		public int DynamicSearchID
		{
			get{return mDynamicSearchID;}
			set{mDynamicSearchID = value;}
		}
		//**************************************************************************************************************
		public int UserID
		{
			get{return mUserID;}
			set{mUserID = value;}
		}
		//**************************************************************************************************************
		public string ReportName
		{
			get{return mReportName;}
			set{mReportName = value;}
		}
		//**************************************************************************************************************
		public DateTime CreateDate
		{
			get{return mCreateDate;}
			set{mCreateDate = value;}
		}
		//**************************************************************************************************************
		public DateTime LastRun
		{
			get{return mLastRun;}
			set{mLastRun = value;}
		}
	}
}
