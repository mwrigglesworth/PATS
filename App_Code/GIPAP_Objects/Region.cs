using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Region.
	/// </summary>
	public class Region
	{
		private int mRegionID;
		private string mRegionName;

		public string RCC;
		private DataSet RegionDS;
        public DataTable CountryDT;
        public DataTable SubRegionDT;
        public DataTable RegionNotes;
		string connString = ConfigurationSettings.AppSettings["ConnectionString"];

		//**********************************************************************************************************************
		public Region()
		{
			this.clear();
		}
		//**********************************************************************************************************************
		public Region(int currID, string Urole)
		{
			if(currID == 0)
			{
				return;
			}
			else
			{
				DataSet myData = new DataSet();
				
				SqlParameter[] arrParams = new SqlParameter[2];

				arrParams[0] = new SqlParameter("@RegionID", SqlDbType.Int);
				arrParams[0].Value = currID;

				arrParams[1] = new SqlParameter("@Urole", SqlDbType.VarChar, 50);
				arrParams[1].Value = Urole;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetRegionProfile2", arrParams);

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
		public string SubRegionInfo()
		{
			string sInfo = "<b>Sub Regions:</b>";
			for(int i=0; i<this.RegionDS.Tables[3].Rows.Count; i++)
			{
				sInfo += "<br>" + this.RegionDS.Tables[3].Rows[i]["SubRegion"].ToString();
			}
			return sInfo;
		}
		//**********************************************************************************************************************
		public void UpdateRCC(System.Web.UI.WebControls.ListBox lb)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@RegionID", SqlDbType.Int);
			arrParams[0].Value = this.RegionID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeleteRCC", arrParams[0]);

			for(int i=0; i<lb.Items.Count; i++)
			{
				arrParams[1] = new SqlParameter("@RCCID", SqlDbType.Int);
				arrParams[1].Value = Convert.ToInt32(lb.Items[i].Value);

				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateRCC", arrParams);
			}
		}
        //**************************************************************************************************************
        public void InflateRegionNotes(string Urole)
        {
            SqlParameter arrParams = new SqlParameter("@RegionID", SqlDbType.Int);
            arrParams.Value = this.RegionID;

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetRegionNotes", arrParams);

            this.RegionNotes = ds.Tables[0];
        }

        //**************************************************************************************************************
        public void AddRegionNote(string createdby, string note)
        {
            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@RegionID", SqlDbType.Int);
            arrParams[0].Value = this.RegionID;

            arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
            arrParams[1].Value = createdby;

            arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
            arrParams[2].Value = note;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateRegionNote", arrParams);
        }
		//**********************************************************************************************************************
		public DataTable GetRCCs()
		{
			return this.RegionDS.Tables[1];
		}
		//**********************************************************************************************************************
		public DataTable GetAllRCCs()
		{
			return this.RegionDS.Tables[2];
		}
		//**********************************************************************************************************************
		public DataTable GetCountries()
		{
			return this.RegionDS.Tables[4];
		}
		//**********************************************************************************************************************
		public DataTable GetCPOs()
		{
			return this.RegionDS.Tables[5];
		}
		//**********************************************************************************************************************
		public DataTable GetDSRs()
		{
			return this.RegionDS.Tables[6];
		}
		//**********************************************************************************************************************
		public DataTable GetClinics()
		{
			return this.RegionDS.Tables[8];
		}
		//**********************************************************************************************************************
		public DataTable GetMaxStations()
		{
			return this.RegionDS.Tables[7];
		}
		//**********************************************************************************************************************
		public DataTable GetPhysicians()
		{
			return this.RegionDS.Tables[9];
		}
		//**********************************************************************************************************************
		public DataTable GetPatients()
		{
			return this.RegionDS.Tables[10];
		}
		//**********************************************************************************************************************
		public DataTable GetRegionTotals()
		{
			return this.RegionDS.Tables[11];
		}
		
		//**********************************************************************************************************************
		public string RegionTotalsHeader()
		{
			return this.RegionDS.Tables[12].Rows[0]["TotalHeader"].ToString();
		}
		//**********************************************************************************************************************
		private void Inflate(DataSet ds, string Urole)
		{
			this.RegionID = Convert.ToInt32(ds.Tables[0].Rows[0]["RegionID"]);
			this.RegionName = ds.Tables[0].Rows[0]["Region"].ToString();

            this.SubRegionDT = ds.Tables[1];
            this.CountryDT = ds.Tables[2];
		}
        //**********************************************************************************************************************
        public void InflatePersonel(string Urole)
        {
            SqlParameter arrParams = new SqlParameter("@RegionID", SqlDbType.Int);
            arrParams.Value = this.RegionID;

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetRegionPersonel", arrParams);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.RCC += "<li><a href=../Person/PersonInfo.aspx?choice=" + ds.Tables[0].Rows[i]["personid"].ToString();
                    this.RCC += ">" + ds.Tables[0].Rows[i]["firstname"].ToString() + " " + ds.Tables[0].Rows[i]["lastname"].ToString() + "</a></li>";
                }
            }
        }
		//**********************************************************************************************************************
		public void clear()
		{
			this.RegionID = 0;
			this.RegionName = "";
			this.RCC = "";
		}
		//**********************************************************************************************************************
		public int RegionID
		{
			get{return mRegionID;}
			set{mRegionID = value;}
		}
		//**********************************************************************************************************************
		public string RegionName
		{
			get{return mRegionName;}
			set{mRegionName = value;}
		}
	}
}
