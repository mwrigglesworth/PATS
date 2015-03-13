using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for SubRegion.
	/// </summary>
	public class SubRegion
	{
		private int mSubRegionID;
		private string mSubRegionName;
		private int mRegionID;

		public string SRCC;
		public string Region;
		public string RCC;

		private DataSet SubRegionDS;
        public DataTable CountryDT;
        public DataTable SubRegionNotes;
		string connString = ConfigurationSettings.AppSettings["ConnectionString"];

		//**********************************************************************************************************************
		public SubRegion()
		{
			this.clear();
		}
		//**********************************************************************************************************************
		public SubRegion(int currID, string Urole)
		{
			if(currID == 0)
			{
				return;
			}
			else
			{
				DataSet myData = new DataSet();
				
				SqlParameter[] arrParams = new SqlParameter[2];

				arrParams[0] = new SqlParameter("@SubRegionID", SqlDbType.Int);
				arrParams[0].Value = currID;

				arrParams[1] = new SqlParameter("@Urole", SqlDbType.VarChar, 50);
				arrParams[1].Value = Urole;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetSubRegionProfile2", arrParams);

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

        //**************************************************************************************************************
        public void InflateSubRegionNotes(string Urole)
        {
            SqlParameter arrParams = new SqlParameter("@SubRegionID", SqlDbType.Int);
            arrParams.Value = this.SubRegionID;

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetSubRegionNotes", arrParams);

            this.SubRegionNotes = ds.Tables[0];
        }

        //**************************************************************************************************************
        public void AddSubRegionNote(string createdby, string note)
        {
            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@SubRegionID", SqlDbType.Int);
            arrParams[0].Value = this.SubRegionID;

            arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
            arrParams[1].Value = createdby;

            arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
            arrParams[2].Value = note;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateSubRegionNote", arrParams);
        }

		//**********************************************************************************************************************
		public string RegionInfo()
		{
			return "<b>Region: </b>" + this.Region + "<br><b>RCC: </b>" + this.RCC;
		}
		//**********************************************************************************************************************
		public DataTable GetSRCCs()
		{
			return this.SubRegionDS.Tables[1];
		}
		//**********************************************************************************************************************
		public DataTable GetAllSRCCs()
		{
			return this.SubRegionDS.Tables[4];
		}
		//**********************************************************************************************************************
		public DataTable GetCountries()
		{
			return this.SubRegionDS.Tables[5];
		}
		//**********************************************************************************************************************
		public DataTable GetCPOs()
		{
			return this.SubRegionDS.Tables[6];
		}
		//**********************************************************************************************************************
		public DataTable GetDSRs()
		{
			return this.SubRegionDS.Tables[7];
		}
		//**********************************************************************************************************************
		public DataTable GetClinics()
		{
			return this.SubRegionDS.Tables[9];
		}
		//**********************************************************************************************************************
		public DataTable GetMaxStations()
		{
			return this.SubRegionDS.Tables[8];
		}
		//**********************************************************************************************************************
		public DataTable GetPhysicians()
		{
			return this.SubRegionDS.Tables[10];
		}
		//**********************************************************************************************************************
		public DataTable GetPatients()
		{
			return this.SubRegionDS.Tables[11];
		}
		//**********************************************************************************************************************
		public void UpdateSRCC(System.Web.UI.WebControls.ListBox lb)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@SubRegionID", SqlDbType.Int);
			arrParams[0].Value = this.SubRegionID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeleteSRCC", arrParams[0]);

			for(int i=0; i<lb.Items.Count; i++)
			{
				arrParams[1] = new SqlParameter("@SRCCID", SqlDbType.Int);
				arrParams[1].Value = Convert.ToInt32(lb.Items[i].Value);

				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateSRCC", arrParams);
			}
		}
		//**********************************************************************************************************************
		private void Inflate(DataSet ds, string Urole)
		{
			this.SubRegionID = Convert.ToInt32(ds.Tables[0].Rows[0]["SubRegionID"]);
			this.SubRegionName = ds.Tables[0].Rows[0]["SubRegion"].ToString();
			this.RegionID = Convert.ToInt32(ds.Tables[0].Rows[0]["RegionID"]);
			
			//data set
			this.CountryDT = ds.Tables[1];
		}
        //**********************************************************************************************************************
        public void InflatePersonel(string Urole)
        {
            SqlParameter arrParams = new SqlParameter("@SubRegionID", SqlDbType.Int);
            arrParams.Value = this.SubRegionID;

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetSubRegionPersonel", arrParams);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.SRCC += "<li><a href=../Person/PersonInfo.aspx?choice=" + ds.Tables[0].Rows[i]["personid"].ToString();
                    this.SRCC += ">" + ds.Tables[0].Rows[i]["firstname"].ToString() + " " + ds.Tables[0].Rows[i]["lastname"].ToString() + "</a></li>";
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.Region = "<a href=../Region/RegionInfo.aspx?choice=" + ds.Tables[1].Rows[0]["RegionID"].ToString() + ">" + ds.Tables[1].Rows[0]["Region"].ToString() + "</a>";
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.RCC += "<li><a href=../Person/PersonInfo.aspx?choice=" + ds.Tables[1].Rows[i]["personid"].ToString();
                    this.RCC += ">" + ds.Tables[1].Rows[i]["firstname"].ToString() + " " + ds.Tables[1].Rows[i]["lastname"].ToString() + "</a></li>";
                }
            }
        }
		//**********************************************************************************************************************
		public void clear()
		{
			this.SubRegionID = 0;
			this.SubRegionName = "";
			this.RegionID = 0;
			this.Region = "";
			this.SRCC = "";
			this.RCC = "";
		}
		//**********************************************************************************************************************
		public int SubRegionID
		{
			get{return mSubRegionID;}
			set{mSubRegionID = value;}
		}
		//**********************************************************************************************************************
		public string SubRegionName
		{
			get{return mSubRegionName;}
			set{mSubRegionName = value;}
		}
		//**********************************************************************************************************************
		public int RegionID
		{
			get{return mRegionID;}
			set{mRegionID = value;}
		}
	}
}
