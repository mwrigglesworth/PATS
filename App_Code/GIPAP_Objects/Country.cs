using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Country.
	/// </summary>
	public class Country
	{
		private int mCountryID;
		private int mSubRegionID;
		private string mCountryName;
		private string mCountryCode;
		private string mActiveGIPAP;
		private bool mNeedFinancialInfo;
		private DateTime mFinancialDeclarationDate;
		private string mEmail;
        private string mSAEEmail;
		private int mPediatricAge;
		private bool mAcceptingNewApps;
		private string mNotes;
		private bool mCMLApproved;
		private bool mPediatricApproved;
		private bool mNeedInterferonInfo;
		private string mCMLInfo;
		private bool mGISTApproved;
		private bool mGISTPedApproved;
		private string	mGISTInfo;
		private int	mPhAllStatus;
        private DateTime mALLDate;
        private bool mPhAllPedApproved;
		private string mPhALLInfo;
		private bool mDFSPApproved;
		private DateTime mDFSPDate;
		private bool mDFSPPedApproved;
		private string mDFSPInfo;
		private bool mLDC;
        //orphans
        private bool mADJGISTApproved;
        private DateTime mADJGISTDate;
        private bool mADJGISTPedApproved;
        private string mADJGISTInfo;
        private bool mMDSApproved;
        private DateTime mMDSDate;
        private bool mMDSPedApproved;
        private string mMDSInfo;
        private bool mMASTApproved;
        private DateTime mMASTDate;
        private bool mMASTPedApproved;
        private string mMASTInfo;
        private bool mHESApproved;
        private DateTime mHESDate;
        private bool mHESPedApproved;
        private string mHESInfo;
        //noa
        private bool mNOAGlivec;
        private int mNOATasigna;
        private bool mTasignaPedApproved;

		public string CpoName;
		public string SubRegion;
		public string Srcc;
		public string Region;
		public string Rcc;
		public string Dsr;
		public DataTable CountryNotes;
        private int NoaCount;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		//string connString = "SERVER=TESTBOX1;DATABASE=GIPAP;PWD=secret;UID=sa;";

		//**********************************************************************************************************************
		public Country()
		{
			//Default Constructor
			this.Clear();
		}

		//**********************************************************************************************************************
		public Country(int countryID, string Urole)
		{
			if(countryID == 0)
			{
				return;
			}
			else
			{
				DataSet myData = new DataSet();

                SqlParameter arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
                arrParams.Value = countryID;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryProfile2", arrParams);

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
		public void Create(string createdby)
		{
			//Update the country information.
			SqlParameter[] arrParams = new SqlParameter[11];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Direction = ParameterDirection.Output;

			arrParams[1] = new SqlParameter("@LDC", SqlDbType.Bit);
			if(this.LDC) 
			{
				arrParams[1].Value = 1;//true
			}
			else
			{
				arrParams[1].Value = 0;//false
			}
					
			arrParams[2] = new SqlParameter("@ActiveGIPAP", SqlDbType.NVarChar, 15);
			arrParams[2].Value = this.ActiveGIPAP;

			arrParams[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[3].Value = createdby;

			arrParams[4] = new SqlParameter("@NeedFinancialInfo", SqlDbType.Bit);
			if(this.NeedFinancialInfo) 
			{
				arrParams[4].Value = 1;//true
			}
			else
			{
				arrParams[4].Value = 0;//false
			}
			arrParams[5] = new SqlParameter("@NeedInterferonInfo", SqlDbType.Bit);
			if(this.NeedInterferonInfo) 
			{
				arrParams[5].Value = 1;//true
			}
			else
			{
				arrParams[5].Value = 0;//false
			}
			arrParams[6] = new SqlParameter("@PediatricApproved", SqlDbType.Bit);
			if(this.PediatricApproved) 
			{
				arrParams[6].Value = 1;//true
			}
			else
			{
				arrParams[6].Value = 0;//false
			}
			arrParams[7] = new SqlParameter("@GISTApproved", SqlDbType.Bit);
			if(this.GISTApproved) 
			{
				arrParams[7].Value = 1;//true
			}
			else
			{
				arrParams[7].Value = 0;//false
			}

			arrParams[8] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[8].Value = this.Notes;

			arrParams[9] = new SqlParameter("@Email", SqlDbType.NVarChar, 500);
			arrParams[9].Value = this.Email;

			arrParams[10] = new SqlParameter("@SubRegionID", SqlDbType.Int);
			arrParams[10].Value = this.SubRegionID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateCountry", arrParams);

			this.CountryID = (int)arrParams[0].Value;
		}
		//**********************************************************************************************************************
		public void Update(string modifiedby)
		{
			//Update the country information.
			SqlParameter[] arrParams = new SqlParameter[45];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			arrParams[1] = new SqlParameter("@AcceptingNewApps", SqlDbType.Bit);
			if(this.AcceptingNewApps) 
			{
				arrParams[1].Value = 1;//true
			}
			else
			{
				arrParams[1].Value = 0;//false
			}
					
			arrParams[2] = new SqlParameter("@ActiveGIPAP", SqlDbType.NVarChar, 15);
			arrParams[2].Value = this.ActiveGIPAP;

			arrParams[3] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 20);
			arrParams[3].Value = modifiedby;

			arrParams[4] = new SqlParameter("@NeedFinancialInfo", SqlDbType.Bit);
			if(this.NeedFinancialInfo) 
			{
				arrParams[4].Value = 1;//true
			}
			else
			{
				arrParams[4].Value = 0;//false
			}
			arrParams[5] = new SqlParameter("@NeedInterferonInfo", SqlDbType.Bit);
			if(this.NeedInterferonInfo) 
			{
				arrParams[5].Value = 1;//true
			}
			else
			{
				arrParams[5].Value = 0;//false
			}
			arrParams[6] = new SqlParameter("@PediatricApproved", SqlDbType.Bit);
			if(this.PediatricApproved) 
			{
				arrParams[6].Value = 1;//true
			}
			else
			{
				arrParams[6].Value = 0;//false
			}
			arrParams[7] = new SqlParameter("@GISTApproved", SqlDbType.Bit);
			if(this.GISTApproved) 
			{
				arrParams[7].Value = 1;//true
			}
			else
			{
				arrParams[7].Value = 0;//false
			}

			arrParams[8] = new SqlParameter("@Notes", SqlDbType.Text);
			arrParams[8].Value = this.Notes;

			arrParams[9] = new SqlParameter("@Email", SqlDbType.NVarChar, 500);
			arrParams[9].Value = this.Email;

			arrParams[10] = new SqlParameter("@SubRegionID", SqlDbType.Int);
			if(this.SubRegionID == 0)
			{
				arrParams[10].Value = DBNull.Value;
			}
			else
			{
				arrParams[10].Value = this.SubRegionID;
			}

			arrParams[11] = new SqlParameter("@FinancialDeclarationDate", SqlDbType.SmallDateTime);
			if(this.FinancialDeclarationDate != Convert.ToDateTime("1/1/0001"))
			{
				arrParams[11].Value = this.FinancialDeclarationDate;
			}
			else
			{
				arrParams[11].Value = DBNull.Value;
			}

			arrParams[12] = new SqlParameter("@PhAllStatus", SqlDbType.Int);
			arrParams[12].Value = this.PhAllStatus;

			arrParams[13] = new SqlParameter("@AllDate", SqlDbType.SmallDateTime);
			if(this.ALLDate != Convert.ToDateTime("1/1/0001"))
			{
				arrParams[13].Value = this.ALLDate;
			}
			else
			{
				arrParams[13].Value = DBNull.Value;
			}

			arrParams[14] = new SqlParameter("@PediatricAge", SqlDbType.Int);
			arrParams[14].Value = this.PediatricAge;

			//new cml stuff
			arrParams[15] = new SqlParameter("@CMLApproved", SqlDbType.Bit);
			if(this.CMLApproved) 
			{
				arrParams[15].Value = 1;//true
			}
			else
			{
				arrParams[15].Value = 0;//false
			}

			arrParams[16] = new SqlParameter("@CMLInfo", SqlDbType.Text);
			arrParams[16].Value = this.CMLInfo;

			//new gist stuff			
			arrParams[17] = new SqlParameter("@GISTPedApproved", SqlDbType.Bit);
			if(this.GISTPedApproved) 
			{
				arrParams[17].Value = 1;//true
			}
			else
			{
				arrParams[17].Value = 0;//false
			}

			arrParams[18] = new SqlParameter("@GISTInfo", SqlDbType.Text);
			arrParams[18].Value = this.GISTInfo;

			//new ph+ all stuff
			arrParams[19] = new SqlParameter("@PhALLInfo", SqlDbType.Text);
			arrParams[19].Value = this.PhALLInfo;

			//dfsp stuff
			arrParams[20] = new SqlParameter("@DFSPApproved", SqlDbType.Bit);
			if(this.DFSPApproved) 
			{
				arrParams[20].Value = 1;//true
			}
			else
			{
				arrParams[20].Value = 0;//false
			}

			arrParams[21] = new SqlParameter("@DFSPDate", SqlDbType.SmallDateTime);
			if(this.DFSPDate != Convert.ToDateTime("1/1/0001"))
			{
				arrParams[21].Value = this.DFSPDate;
			}
			else
			{
				arrParams[21].Value = DBNull.Value;
			}

			arrParams[22] = new SqlParameter("@DFSPPedApproved", SqlDbType.Bit);
			if(this.DFSPPedApproved) 
			{
				arrParams[22].Value = 1;//true
			}
			else
			{
				arrParams[22].Value = 0;//false
			}

			arrParams[23] = new SqlParameter("@DFSPInfo", SqlDbType.Text);
			arrParams[23].Value = this.DFSPInfo;

            //ADJ GIST stuff
            arrParams[24] = new SqlParameter("@ADJGISTApproved", SqlDbType.Bit);
            if (this.ADJGISTApproved)
            {
                arrParams[24].Value = 1;//true
            }
            else
            {
                arrParams[24].Value = 0;//false
            }

            arrParams[25] = new SqlParameter("@ADJGISTDate", SqlDbType.SmallDateTime);
            if (this.ADJGISTDate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[25].Value = this.ADJGISTDate;
            }
            else
            {
                arrParams[25].Value = DBNull.Value;
            }

            arrParams[26] = new SqlParameter("@ADJGISTPedApproved", SqlDbType.Bit);
            if (this.ADJGISTPedApproved)
            {
                arrParams[26].Value = 1;//true
            }
            else
            {
                arrParams[26].Value = 0;//false
            }

            arrParams[27] = new SqlParameter("@ADJGISTInfo", SqlDbType.Text);
            arrParams[27].Value = this.ADJGISTInfo;

            //MDS stuff
            arrParams[28] = new SqlParameter("@MDSApproved", SqlDbType.Bit);
            if (this.MDSApproved)
            {
                arrParams[28].Value = 1;//true
            }
            else
            {
                arrParams[28].Value = 0;//false
            }

            arrParams[29] = new SqlParameter("@MDSDate", SqlDbType.SmallDateTime);
            if (this.MDSDate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[29].Value = this.MDSDate;
            }
            else
            {
                arrParams[29].Value = DBNull.Value;
            }

            arrParams[30] = new SqlParameter("@MDSPedApproved", SqlDbType.Bit);
            if (this.MDSPedApproved)
            {
                arrParams[30].Value = 1;//true
            }
            else
            {
                arrParams[30].Value = 0;//false
            }

            arrParams[31] = new SqlParameter("@MDSInfo", SqlDbType.Text);
            arrParams[31].Value = this.MDSInfo;

            //MAST stuff
            arrParams[32] = new SqlParameter("@MASTApproved", SqlDbType.Bit);
            if (this.MASTApproved)
            {
                arrParams[32].Value = 1;//true
            }
            else
            {
                arrParams[32].Value = 0;//false
            }

            arrParams[33] = new SqlParameter("@MASTDate", SqlDbType.SmallDateTime);
            if (this.MASTDate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[33].Value = this.MASTDate;
            }
            else
            {
                arrParams[33].Value = DBNull.Value;
            }

            arrParams[34] = new SqlParameter("@MASTPedApproved", SqlDbType.Bit);
            if (this.MASTPedApproved)
            {
                arrParams[34].Value = 1;//true
            }
            else
            {
                arrParams[34].Value = 0;//false
            }

            arrParams[35] = new SqlParameter("@MASTInfo", SqlDbType.Text);
            arrParams[35].Value = this.MASTInfo;

            //HES stuff
            arrParams[36] = new SqlParameter("@HESApproved", SqlDbType.Bit);
            if (this.HESApproved)
            {
                arrParams[36].Value = 1;//true
            }
            else
            {
                arrParams[36].Value = 0;//false
            }

            arrParams[37] = new SqlParameter("@HESDate", SqlDbType.SmallDateTime);
            if (this.HESDate != Convert.ToDateTime("1/1/0001"))
            {
                arrParams[37].Value = this.HESDate;
            }
            else
            {
                arrParams[37].Value = DBNull.Value;
            }

            arrParams[38] = new SqlParameter("@HESPedApproved", SqlDbType.Bit);
            if (this.HESPedApproved)
            {
                arrParams[38].Value = 1;//true
            }
            else
            {
                arrParams[38].Value = 0;//false
            }

            arrParams[39] = new SqlParameter("@HESInfo", SqlDbType.Text);
            arrParams[39].Value = this.HESInfo;

            arrParams[40] = new SqlParameter("@saeEmail", SqlDbType.NVarChar, 500);
            arrParams[40].Value = this.SAEEmail;

            arrParams[41] = new SqlParameter("@PhAllPedApproved", SqlDbType.Bit);
            if (this.PhAllPedApproved)
            {
                arrParams[41].Value = 1;//true
            }
            else
            {
                arrParams[41].Value = 0;//false
            }

            //noa stuff
            arrParams[42] = new SqlParameter("@NOAGlivec", SqlDbType.Bit);
            if (this.NOAGlivec)
            {
                arrParams[42].Value = 1;//true
            }
            else
            {
                arrParams[42].Value = 0;//false
            }

            arrParams[43] = new SqlParameter("@NOATasigna", SqlDbType.Int);
            arrParams[43].Value = this.NOATasigna;

            arrParams[44] = new SqlParameter("@TasignaPedApproved", SqlDbType.Bit);
            if (this.TasignaPedApproved)
            {
                arrParams[44].Value = 1;//true
            }
            else
            {
                arrParams[44].Value = 0;//false
            }
				
			//Send the data to the database
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateCountry", arrParams);
		}
		//**********************************************************************************************************************
		//NOT SURE IF THESE ARE STAYING
		//**********************************************************************************************************************
		public DataSet DSRList()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_DSRList");
		}
		//**********************************************************************************************************************
		public DataSet CPOList()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CPOList");
		}
		//**********************************************************************************************************************
		public DataSet SubRegionList()
		{
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_SubRegionList");
		}
		//**********************************************************************************************************************
		//NOT SURE IF THESE ARE STAYING
		//**********************************************************************************************************************
		//**********************************************************************************************************************
		public DataSet GetCountryDSRList()
		{
			SqlParameter arrParams = new SqlParameter();

			arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams.Value = this.CountryID;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryDSRList", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet GetCountryCPOList()
		{
			SqlParameter arrParams = new SqlParameter();

			arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams.Value = this.CountryID;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryCPOList", arrParams);
		}
        //**********************************************************************************************************************
        public DataSet GetApplicationDatasets(int cid)
        {
            SqlParameter arrParams = new SqlParameter();

            arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams.Value = cid;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryDSRList", arrParams);
        }
		//**********************************************************************************************************************
		public void UpdateCountryDSR(System.Web.UI.WebControls.ListBox lb)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeleteCountryDSR", arrParams[0]);

			for(int i=0; i<lb.Items.Count; i++)
			{
				arrParams[1] = new SqlParameter("@DSRID", SqlDbType.Int);
				arrParams[1].Value = Convert.ToInt32(lb.Items[i].Value);

				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateCountryDSR", arrParams);
			}
		}
		//**********************************************************************************************************************
		public void UpdateCountryCPO(System.Web.UI.WebControls.ListBox lb)
		{
			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeleteCountryCPO", arrParams[0]);

			for(int i=0; i<lb.Items.Count; i++)
			{
				arrParams[1] = new SqlParameter("@CPOID", SqlDbType.Int);
				arrParams[1].Value = Convert.ToInt32(lb.Items[i].Value);

				SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateCountryCPO", arrParams);
			}
		}
		//**********************************************************************************************************************
		public string CountryLinks(string Urole)
		{
			string clinks = "<table width=100%><tr><td width=50%><b>List:</b></td><td><b>Reports:</b></td></tr><tr><td>";
			if(Urole == "Novartis")
			{
				clinks += "<li><A href=javascript:openNewWindow('fullpagereport.aspx?report=CountryPatients&choice=" + this.CountryID.ToString() + "','thewin','height=575,width=800,toolbar=no,scrollbars=yes')>Patients</a>";
			}
			else
			{
				clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=Patients><font color=blue>Patients</font></a>";
			}
			clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=Physicians><font color=blue>Physicians</font></a>";
			clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=Clinics><font color=blue>Clinics</font></a>";
			clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=MaxStations><font color=blue>Max Stations</font></a>";
			clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=SAE><font color=blue>Adverse Events</font></a>";
			clinks += "</td><td valign=top>";
			clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=Activity><font color=blue>Country Activity</font></a>";
			clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=DiagnosisReport><font color=blue>Diagnosis Report</font></a>";
			clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=DosageReport><font color=blue>Dosage Report</font></a>";
			clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=PersonelReport><font color=blue>Personnel Report</font></a>";
			clinks += "<li><a href=GIPAP.aspx?trgt=countryreports&choice=" + this.CountryID.ToString() + "&report=DosageHistory><font color=blue>Active Patient Dosage History</font></a>";
            if (this.NoaCount > 0)
            {
                clinks += "<li><a href=GIPAP.aspx?trgt=noatotals&choice=" + this.CountryID.ToString() + "><font color=blue>NOA Totals</font></a> <font color=red>new!</font>";
            }
			clinks += "</td></tr></table>";
			return clinks;
		}
        //**********************************************************************************************************************
        public string CountryHeader(string uRole)
        {
            string cInfo = "<h1><font color=steelblue>" + this.CountryName + "  (" + this.CountryCode + ")</font></h1>";
            if (uRole == "TMFUser" || uRole == "MaxStation")
            {
                cInfo += "<div class='LeftColSpacer'><a href=CountryInfo.aspx?choice=" + this.CountryID.ToString() + ">PATS</a> <a href=../GIPAPTrusted.aspx?id=" + this.CountryID.ToString() + "&reqform=Country><font color=deeppink>PINC</a></font></div>";                
            }
            return cInfo;
        }
		//**********************************************************************************************************************
		public string CountryInfo(string uRole)
		{
			string cInfo = "<font class='lbl'>Active GIPAP: </font>" + this.ActiveGIPAP + "<br>";
			//cInfo += "<font class='lbl'>Least Developed Country: </font>" + this.YN(this.LDC) + "<br>";
			cInfo += "<font class='lbl'>Financial Information Required: </font>" + this.YN(this.NeedFinancialInfo) + "<br>";
			if(this.FinancialDeclarationDate != Convert.ToDateTime("1/1/0001"))
			{
				cInfo += "<font class='lbl'>Date Financial Declaration Form Required: </font><br>" + this.FinancialDeclarationDate.Day.ToString() + " " + this.FinancialDeclarationDate.ToString("y") + "<br>";
			}
            cInfo += "<font class='lbl'>Email: </font>" + this.Email;
            cInfo += "<br><font class='lbl'>Adverse Event Email: </font>" + this.SAEEmail;
			cInfo += "<br><font class='lbl'>Pediatric Age: </font>";
			if(this.PediatricAge == 0)
			{
				cInfo += "<i>Not Set</i>";
			}
			else
			{
				cInfo += this.PediatricAge.ToString();
			}
			cInfo += "<br><font class='lbl'>Accepting New Applications: ";
			if(this.AcceptingNewApps)
			{
				cInfo += "<font color=steelblue>YES</font></font>";
			}
			else
			{
				cInfo += "<font color=red>NO</font></font>";
			}
            cInfo += "<br><br><font class='lbl'>Approved For NOA-Glivec: </font>";
            if (this.NOAGlivec)
            {
                cInfo += "<font color=steelblue>YES</font></font>";
            }
            else
            {
                cInfo += "NO";
            }
            cInfo += "<br><font class=Subtext>Physicians will have to be approved for NOA-Glivec individually, this setting only allows for physicians to be moved into NOA for this country</font>";
            cInfo += "<br><br><font class='lbl'>Approved For NOA-Tasigna: </font>";
            if (this.NOATasigna == 1)
            {
                cInfo += "1st + 2nd Line";
            }
            else if (this.NOATasigna == 2)
            {
                cInfo += "2nd Line only";
            }
            else
            {
                cInfo += "NO";
            }
            cInfo += "<br><font class=Subtext>Physicians will have to be approved for NOA-Tasigna individually</font>";
			return cInfo;
		}
		//**********************************************************************************************************************
		public string DiseaseTable(string tab)
		{
			string dInfo = "<table cellspacing=0 cellpadding=2 width=100% style='BORDER-BOTTOM: steelblue 1px solid;'>";
			if(tab == "")
			{
				dInfo += "<tr><td style='BORDER-LEFT: steelblue 1px solid; BORDER-TOP: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;' bgcolor=gainsboro>";
				dInfo += "<b><font color=steelblue>GIPAP</font></b></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=cml>CML</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=gist>GIST</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=phall>Ph+ ALL</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=dfsp>DFSP</a></td>";
				dInfo += "</tr><td colspan=5 bgcolor=gainsboro style='BORDER-LEFT: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;'>";
				dInfo += "<b>Additional GIPAP Protocol:</b><br>" + this.Notes + "</td></tr>";
			}
			else if(tab == "cml")
			{
				dInfo += "<tr><td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + ">GIPAP</a></td>";
				dInfo += "<td style='BORDER-LEFT: steelblue 1px solid; BORDER-TOP: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;' bgcolor=gainsboro>";
				dInfo += "<font color=steelblue><b>CML</b></font></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=gist>GIST</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=phall>Ph+ ALL</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=dfsp>DFSP</a></td>";
				dInfo += "</tr><tr><td colspan=5 bgcolor=gainsboro style='BORDER-LEFT: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;'>";
				dInfo += this.CMLInfoTab() + "</td></tr>";
			}
			else if(tab == "gist")
			{
				dInfo += "<tr><td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + ">GIPAP</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=cml>CML</a></td>";
				dInfo += "<td style='BORDER-LEFT: steelblue 1px solid; BORDER-TOP: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;' bgcolor=gainsboro>";
				dInfo += "<font color=steelblue><b>GIST</b></font></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=phall>Ph+ ALL</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=dfsp>DFSP</a></td>";
				dInfo += "</tr><tr><td colspan=5 bgcolor=gainsboro style='BORDER-LEFT: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;'>";
				dInfo += this.GISTInfoTab() + "</td></tr>";
			}
			else if(tab == "phall")
			{
				dInfo += "<tr><td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + ">GIPAP</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=cml>CML</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=gist>GIST</a></td>";
				dInfo += "<td style='BORDER-LEFT: steelblue 1px solid; BORDER-TOP: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;' bgcolor=gainsboro>";
				dInfo += "<font color=steelblue><b>Ph+ ALL</b></font></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=dfsp>DFSP</a></td>";
				dInfo += "</tr><tr><td colspan=5 bgcolor=gainsboro style='BORDER-LEFT: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;'>";
				dInfo += this.ALLInfoTab() + "</td></tr>";
			}
			else if(tab == "dfsp")
			{
				dInfo += "<tr><td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + ">GIPAP</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=cml>CML</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=gist>GIST</a></td>";
				dInfo += "<td width=20% bgcolor=gray style='BORDER-BOTTOM: steelblue 1px solid;'><a href=GIPAP.aspx?trgt=countryinfo&choice=" + this.CountryID.ToString() + "&tab=phall>Ph+ ALL</a></td>";
				dInfo += "<td style='BORDER-LEFT: steelblue 1px solid; BORDER-TOP: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;' bgcolor=gainsboro>";
				dInfo += "<font color=steelblue><b>DFSP</b></font></td>";
				dInfo += "</tr><tr><td colspan=5 bgcolor=gainsboro style='BORDER-LEFT: steelblue 1px solid; BORDER-RIGHT: steelblue 1px solid;'>";
				dInfo += this.DFSPInfoTab() + "</td></tr>";
			}
			dInfo += "</table>";
			return dInfo;
		}
		//**********************************************************************************************************************
		public string DFSPInfoTab()
		{
			string dInfo = "<font class='lbl'>DFSP Approved: </font>" + this.YN(this.DFSPApproved);
			if(this.DFSPDate != Convert.ToDateTime("1/1/0001"))
			{
				dInfo += "<li>DFSP Date: </font><font color=steelblue>" + this.DFSPDate.Day.ToString() + " " + this.DFSPDate.ToString("y") + "</font><br>";
			}
			dInfo += "<br><font class='lbl'>Pediatric Patients Accepted: </font>" + this.YN(this.DFSPPedApproved) + "<br>";
			dInfo += "<br><font class='lbl'>Additional " + this.CountryName + " DFSP Information:</font><br>";
			dInfo += this.DFSPInfo;
			return dInfo;
		}
        //**********************************************************************************************************************
        public string AdjGISTInfoTab()
        {
            StringBuilder dInfo = new StringBuilder();
            dInfo.Append("<font class='lbl'>Adjuvant GIST Approved: </font>" + this.YN(this.ADJGISTApproved));
            if (this.ADJGISTDate != Convert.ToDateTime("1/1/0001"))
            {
                dInfo.Append("<li>Adjuvant GIST Date: </font><font color=steelblue>" + this.ADJGISTDate.Day.ToString() + " " + this.ADJGISTDate.ToString("y") + "</font><br>");
            }
            dInfo.Append("<br><font class='lbl'>Pediatric Patients Accepted: </font>" + this.YN(this.ADJGISTPedApproved) + "<br>");
            dInfo.Append("<br><font class='lbl'>Additional " + this.CountryName + " Adjuvant GIST Information:</font><br>");
            dInfo.Append(this.ADJGISTInfo);
            return dInfo.ToString();
        }
        //**********************************************************************************************************************
        public string MDSInfoTab()
        {
            StringBuilder dInfo = new StringBuilder();
            dInfo.Append("<font class='lbl'>MDS / MPD Approved: </font>" + this.YN(this.MDSApproved));
            if (this.MDSDate != Convert.ToDateTime("1/1/0001"))
            {
                dInfo.Append("<li>MDS / MPD Date: </font><font color=steelblue>" + this.MDSDate.Day.ToString() + " " + this.MDSDate.ToString("y") + "</font><br>");
            }
            dInfo.Append("<br><font class='lbl'>Pediatric Patients Accepted: </font>" + this.YN(this.MDSPedApproved) + "<br>");
            dInfo.Append("<br><font class='lbl'>Additional " + this.CountryName + " MDS / MPD Information:</font><br>");
            dInfo.Append(this.MDSInfo);
            return dInfo.ToString();
        }
        //**********************************************************************************************************************
        public string SysMastInfoTab()
        {
            StringBuilder dInfo = new StringBuilder();
            dInfo.Append("<font class='lbl'>Systemic Mastocytosis Approved: </font>" + this.YN(this.MASTApproved));
            if (this.MASTDate != Convert.ToDateTime("1/1/0001"))
            {
                dInfo.Append("<li>Systemic Mastocytosis Date: </font><font color=steelblue>" + this.MASTDate.Day.ToString() + " " + this.MASTDate.ToString("y") + "</font><br>");
            }
            dInfo.Append("<br><font class='lbl'>Pediatric Patients Accepted: </font>" + this.YN(this.MASTPedApproved) + "<br>");
            dInfo.Append("<br><font class='lbl'>Additional " + this.CountryName + " Systemic Mastocytosis Information:</font><br>");
            dInfo.Append(this.MASTInfo);
            return dInfo.ToString();
        }
        //**********************************************************************************************************************
        public string HesInfoTab()
        {
            StringBuilder dInfo = new StringBuilder();
            dInfo.Append("<font class='lbl'>HES / CEL: </font>" + this.YN(this.HESApproved));
            if (this.HESDate != Convert.ToDateTime("1/1/0001"))
            {
                dInfo.Append("<li>HES / CEL Date: </font><font color=steelblue>" + this.HESDate.Day.ToString() + " " + this.HESDate.ToString("y") + "</font><br>");
            }
            dInfo.Append("<br><font class='lbl'>Pediatric Patients Accepted: </font>" + this.YN(this.HESPedApproved) + "<br>");
            dInfo.Append("<br><font class='lbl'>Additional " + this.CountryName + " HES / CEL Information:</font><br>");
            dInfo.Append(this.HESInfo);
            return dInfo.ToString();
        }
        //**********************************************************************************************************************
        public string TasignaInfoTab()
        {
            StringBuilder dInfo = new StringBuilder();
            dInfo.Append("<font class='lbl'>Tasigna: </font>");
            if (this.NOATasigna == 1)
            {
                dInfo.Append("1st + 2nd Line");
            }
            else if (this.NOATasigna == 2)
            {
                dInfo.Append("2nd Line only");
            }
            else
            {
                dInfo.Append("NO");
            }
            dInfo.Append("<br><font class='lbl'>Pediatric Patients Accepted: </font>" + this.YN(this.TasignaPedApproved));
            return dInfo.ToString();
        }
		//**********************************************************************************************************************
		public string ALLInfoTab()
		{
			string cInfo = "<font class='lbl'>Ph+ ALL Open: </font>";
			if(this.PhAllStatus == 0)
			{
				cInfo += "No<br>";
			}
			else if(this.PhAllStatus == 1)
			{
				cInfo += "2nd Line Only (U.S. Label)<br>";
			}
			else if(this.PhAllStatus == 2)
			{
				cInfo += "1st and 2nd Line (E.U. Label)<br>";
			}
			if(this.ALLDate != Convert.ToDateTime("1/1/0001"))
			{
				cInfo += "<li>Ph+ ALL Date: </font><font color=steelblue>" + this.ALLDate.Day.ToString() + " " + this.ALLDate.ToString("y") + "</font><br>";
			}
            cInfo += "<br><font class='lbl'>Pediatric Patients Accepted: </font>" + this.YN(this.PhAllPedApproved) + "<br>";
            cInfo += "<br><font class='lbl'>Additional " + this.CountryName + " Ph+ ALL Information:</font><br>";
			cInfo += this.PhALLInfo;
			return cInfo;
		}
		//**********************************************************************************************************************
		public string GISTInfoTab()
		{
			string gInfo = "<font class='lbl'>GIST Approved: </font>" + this.YN(this.GISTApproved);
			gInfo += "<br><br><font class='lbl'>Pediatric Patients Accepted: </font>" + this.YN(this.GISTPedApproved) + "<br><br>";
			gInfo += "<font class='lbl'>Additional " + this.CountryName + " GIST Information:</font><br>";
			gInfo += this.GISTInfo;
			return gInfo;
		}
		//**********************************************************************************************************************
		public string CMLInfoTab()
		{
			string cInfo = "<font class='lbl'>CML Approved: </font>" + this.YN(this.CMLApproved);
			cInfo += "<br><font class='lbl'>Pediatric Patients Accepted: </font>" + this.YN(this.PediatricApproved) + "<br><br>";
			cInfo += "<font class='lbl'>Interferon Information Required: </font>" + this.YN(this.NeedInterferonInfo) + "<br><br>";
			cInfo += "<font class='lbl'>Additional " + this.CountryName + " CML Information:</font><br>";
			cInfo += this.CMLInfo;
			return cInfo;
		}
		//**********************************************************************************************************************
		public string RegionalInfo()
		{
			string rInfo = "<font color=steelblue><b>Regional Information</b></font><br>";
			rInfo += "<b>Region: </b>" + this.Region + "<br>";
			rInfo += "<b>RCC: </b>" + this.Rcc + "<br>";
			rInfo += "<b>Sub-Region: </b>" + this.SubRegion + "<br>";
			rInfo += "<b>SRCC: </b>" + this.Srcc;
			return rInfo;
		}
		//**********************************************************************************************************************
		public string YN(bool bitVal)
		{
			if(bitVal)
			{
				return "Yes";
			}
			else
			{
				return "No";
			}
		}
		//**********************************************************************************************************************
		public DataSet GetCountryList(bool onlyActive)
		{
            SqlParameter parama = new SqlParameter("@Active", SqlDbType.Bit);
            if (onlyActive)
                parama.Value = 1;
            else
                parama.Value = 0;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryList", parama);
		}
		//**********************************************************************************************************************
		public DataSet GetClinics()
		{
			SqlParameter paramCountryID = new SqlParameter("@CountryID", SqlDbType.Int);
			paramCountryID.Value = this.mCountryID;
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetClinics", paramCountryID);
		}
		//**********************************************************************************************************************
		public DataSet GetActiveClinics()
		{
			SqlParameter paramCountryID = new SqlParameter("@CountryID", SqlDbType.Int);
			paramCountryID.Value = this.mCountryID;
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetActiveClinics", paramCountryID);
		}
		
		//**********************************************************************************************************************
		public DataSet GetPhysicians()
		{
			SqlParameter paramCountryID = new SqlParameter("@CountryID", SqlDbType.Int);
			paramCountryID.Value = this.mCountryID;
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicians", paramCountryID);
		}

		//**********************************************************************************************************************
		public DataSet GetCountryPatients()
		{
			SqlParameter paramCountryID = new SqlParameter("@CountryID", SqlDbType.Int);
			paramCountryID.Value = this.mCountryID;

			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryPatients", paramCountryID);
		}
        //**********************************************************************************************************************
        public DataSet GetCountryDataSets(string dset)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[0].Value = this.CountryID;

            arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
            arrParams[1].Value = dset;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryDatasets", arrParams);
        }
		//**************************************************************************************************************
		public void AddCountryNote(string createdby, string note)
		{
			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[1].Value = createdby;

			arrParams[2] = new SqlParameter("@Note", SqlDbType.Text);
			arrParams[2].Value = note;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateCountryNote", arrParams);
		}
		//**********************************************************************************************************************
		public DataSet GetActivePatients()
		{
			SqlParameter paramCountryID = new SqlParameter("@CountryID", SqlDbType.Int);
			paramCountryID.Value = this.mCountryID;
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetActivePatients", paramCountryID);
		}
		//**********************************************************************************************************************
		private void Clear() //Sets the object to the default values
		{		
			this.CountryID = 0;
			this.CountryName = "";
			this.LDC = false;
			this.CountryCode = "";
			this.ActiveGIPAP = "N/A";
			this.NeedFinancialInfo = true;
			this.NeedInterferonInfo = true;
			this.PediatricApproved = false;
			this.GISTApproved = false;
			this.PhAllStatus = 0;
			this.Notes = "";
			this.Email = "";
			this.CpoName = "";
			this.SubRegionID = 0;
		}

		//**********************************************************************************************************************
		private void Inflate(DataSet ds, string Urole)
		{
			this.CountryID = Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]);
			if(this.CountryID == 130)
			{
				this.CountryName = "The " + Convert.ToString(ds.Tables[0].Rows[0]["CountryName"]);
			}
			else
			{
				this.CountryName = Convert.ToString(ds.Tables[0].Rows[0]["CountryName"]);
			}
			this.LDC = Convert.ToBoolean(ds.Tables[0].Rows[0]["LDC"]);
			this.CountryCode = Convert.ToString(ds.Tables[0].Rows[0]["CountryCode"]);
			this.ActiveGIPAP = Convert.ToString(ds.Tables[0].Rows[0]["ActiveGIPAP"]);
			this.NeedFinancialInfo = Convert.ToBoolean(ds.Tables[0].Rows[0]["NeedFinancialInfo"]);
			this.PediatricAge = Convert.ToInt32(ds.Tables[0].Rows[0]["PediatricAge"]);
			this.AcceptingNewApps = Convert.ToBoolean(ds.Tables[0].Rows[0]["AcceptingNewApps"]);
			this.CMLApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["cmlapproved"]);
			this.NeedInterferonInfo = Convert.ToBoolean(ds.Tables[0].Rows[0]["NeedInterferonInfo"]);
			this.CMLInfo = ds.Tables[0].Rows[0]["cmlinfo"].ToString();
            //noa
            this.NOAGlivec = Convert.ToBoolean(ds.Tables[0].Rows[0]["NOAGlivec"]);
            this.NOATasigna = Convert.ToInt32(ds.Tables[0].Rows[0]["NOATasigna"]);
            this.TasignaPedApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["TasignaPedApproved"]);
			this.PediatricApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["PediatricApproved"]);
			try
			{
				this.FinancialDeclarationDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["FinancialDeclarationDate"]);
			}
			catch{}
			this.PhAllStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["PhAllStatus"]);
			try
			{
				this.ALLDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["allDate"]);
			}
            catch { }
            this.PhAllPedApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["PhAllPedApproved"]);
			this.PhALLInfo = ds.Tables[0].Rows[0]["phallinfo"].ToString();
			this.Notes = ds.Tables[0].Rows[0]["Notes"].ToString();
			this.Email = ds.Tables[0].Rows[0]["Email"].ToString();
            this.SAEEmail = ds.Tables[0].Rows[0]["saeEmail"].ToString();
            try
			{
				this.SubRegionID = Convert.ToInt32(ds.Tables[0].Rows[0]["SubRegionID"]);
			}
			catch{}
			if(ds.Tables[0].Rows[0]["GISTApproved"] == DBNull.Value)
			{
				this.GISTApproved = false;
			}
			else
			{
				this.GISTApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["GISTApproved"]);
			}
			this.GISTPedApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["gistpedapproved"]);
			this.GISTInfo = ds.Tables[0].Rows[0]["gistinfo"].ToString();
			this.DFSPApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["dfspapproved"]);
			try
			{
				this.DFSPDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["dfspDate"]);
			}
			catch{}
			this.DFSPPedApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["dfsppedapproved"]);
			this.DFSPInfo = ds.Tables[0].Rows[0]["dfspinfo"].ToString();
            //adj gist
            this.ADJGISTApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["ADJGISTapproved"]);
            try
            {
                this.ADJGISTDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ADJGISTDate"]);
            }
            catch { }
            this.ADJGISTPedApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["ADJGISTpedapproved"]);
            this.ADJGISTInfo = ds.Tables[0].Rows[0]["ADJGISTinfo"].ToString();
            //mds
            this.MDSApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["MDSapproved"]);
            try
            {
                this.MDSDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["MDSDate"]);
            }
            catch { }
            this.MDSPedApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["MDSpedapproved"]);
            this.MDSInfo = ds.Tables[0].Rows[0]["MDSinfo"].ToString();
            //mast
            this.MASTApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["MASTapproved"]);
            try
            {
                this.MASTDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["MASTDate"]);
            }
            catch { }
            this.MASTPedApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["MASTpedapproved"]);
            this.MASTInfo = ds.Tables[0].Rows[0]["MASTinfo"].ToString();
            //hes
            this.HESApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["HESapproved"]);
            try
            {
                this.HESDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["HESDate"]);
            }
            catch { }
            this.HESPedApproved = Convert.ToBoolean(ds.Tables[0].Rows[0]["HESpedapproved"]);
            this.HESInfo = ds.Tables[0].Rows[0]["HESinfo"].ToString();
		}
        //**********************************************************************************************************************
        public void InflatePersonel(string Urole)
        {
            SqlParameter arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams.Value = this.CountryID;

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryPersonel", arrParams);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.CpoName += "<li><a href=../Person/PersonInfo.aspx?choice=" + ds.Tables[0].Rows[i]["personid"].ToString();
                    this.CpoName += ">" + ds.Tables[0].Rows[i]["firstname"].ToString() + " " + ds.Tables[0].Rows[i]["lastname"].ToString() + "</a></li>";
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                if (Urole == "TMFUser")
                {
                    this.SubRegion = "<a href=../SubRegion/SubRegionInfo.aspx?choice=" + ds.Tables[1].Rows[0]["SubRegionID"].ToString() + ">" + ds.Tables[1].Rows[0]["SubRegion"].ToString() + "</a>";
                    this.Region = "<a href=../Region/RegionInfo.aspx?choice=" + ds.Tables[1].Rows[0]["RegionID"].ToString() + ">" + ds.Tables[1].Rows[0]["Region"].ToString() + "</a>";
                }
                else
                {
                    this.SubRegion = ds.Tables[1].Rows[0]["SubRegion"].ToString();
                    this.Region = ds.Tables[1].Rows[0]["Region"].ToString();
                }
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.Srcc += "<li><a href=../Person/PersonInfo.aspx?choice=" + ds.Tables[2].Rows[i]["personid"].ToString();
                    this.Srcc += ">" + ds.Tables[2].Rows[i]["firstname"].ToString() + " " + ds.Tables[2].Rows[i]["lastname"].ToString() + "</a></li>";
                }
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    this.Rcc += "<li><a href=../Person/PersonInfo.aspx?choice=" + ds.Tables[3].Rows[i]["personid"].ToString();
                    this.Rcc += ">" + ds.Tables[3].Rows[i]["firstname"].ToString() + " " + ds.Tables[3].Rows[i]["lastname"].ToString() + "</a></li>";
                }
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    this.Dsr += "<li><a href=../Person/PersonInfo.aspx?choice=" + ds.Tables[4].Rows[i]["personid"].ToString();
                    this.Dsr += ">" + ds.Tables[4].Rows[i]["firstname"].ToString() + " " + ds.Tables[4].Rows[i]["lastname"].ToString() + "</a></li>";
                }
            }

            this.NoaCount = Convert.ToInt32(ds.Tables[5].Rows[0]["count"]);
        }

        //**********************************************************************************************************************
        public void InflateCountryNotes(string Urole)
        {
            SqlParameter arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams.Value = this.CountryID;

            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryNotes", arrParams);

            this.CountryNotes = ds.Tables[0];
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
		public string Email
		{
			get{return mEmail;}
			set{mEmail = value;}
		}
        //**********************************************************************************************************************
        public string SAEEmail
        {
            get { return mSAEEmail; }
            set { mSAEEmail = value; }
        }
		//**********************************************************************************************************************
		public string Notes
		{
			get{return mNotes;}
			set{mNotes = value;}
		}
		//**********************************************************************************************************************
		public int SubRegionID
		{
			get{return mSubRegionID;}
			set{mSubRegionID = value;}
		}
		//**********************************************************************************************************************
		public int PediatricAge
		{
			get{return mPediatricAge;}
			set{mPediatricAge = value;}
		}
		//**********************************************************************************************************************
		public bool AcceptingNewApps
		{
			get{return mAcceptingNewApps;}
			set{mAcceptingNewApps = value;}
		}
		//**********************************************************************************************************************
		public DateTime FinancialDeclarationDate
		{
			get{return mFinancialDeclarationDate;}
			set{mFinancialDeclarationDate = value;}
		}
		//**********************************************************************************************************************
		public bool NeedFinancialInfo
		{
			get{return mNeedFinancialInfo;}
			set{mNeedFinancialInfo = value;}
		}

		//**********************************************************************************************************************
		public bool LDC
		{
			get{return mLDC;}
			set{mLDC = value;}
		}
		//**********************************************************************************************************************
		public bool CMLApproved
		{
			get{return mCMLApproved;}
			set{mCMLApproved = value;}
		}
		//**********************************************************************************************************************
		public bool NeedInterferonInfo
		{
			get{return mNeedInterferonInfo;}
			set{mNeedInterferonInfo = value;}
		}
		//**********************************************************************************************************************
		public string CMLInfo
		{
			get{return mCMLInfo;}
			set{mCMLInfo = value;}
		}
		//**********************************************************************************************************************
		public bool PediatricApproved
		{
			get{return mPediatricApproved;}
			set{mPediatricApproved = value;}
		}
		//**********************************************************************************************************************
		public string CountryCode
		{
			get{return mCountryCode;}
			set{mCountryCode = value;}
		}

		//**********************************************************************************************************************
		public string ActiveGIPAP
		{
			get{return mActiveGIPAP;}
			set{mActiveGIPAP = value;}
		}

		//**********************************************************************************************************************
		public bool GISTApproved
		{
			get{return mGISTApproved;}
			set{mGISTApproved = value;}
		}
		//**********************************************************************************************************************
		public bool GISTPedApproved
		{
			get{return mGISTPedApproved;}
			set{mGISTPedApproved = value;}
		}
		//**********************************************************************************************************************
		public string GISTInfo
		{
			get{return mGISTInfo;}
			set{mGISTInfo = value;}
		}
		//**********************************************************************************************************************
		public int PhAllStatus
		{
			get{return mPhAllStatus;}
			set{mPhAllStatus = value;}
		}
        //**********************************************************************************************************************
        public bool PhAllPedApproved
        {
            get { return mPhAllPedApproved; }
            set { mPhAllPedApproved = value; } 
        }
		//**********************************************************************************************************************
		public DateTime ALLDate
		{
			get{return mALLDate;}
			set{mALLDate = value;}
		}
		//**********************************************************************************************************************
		public string PhALLInfo
		{
			get{return mPhALLInfo;}
			set{mPhALLInfo = value;}
		}
		//**********************************************************************************************************************
		public bool DFSPApproved
		{
			get{return mDFSPApproved;}
			set{mDFSPApproved = value;}
		}
		//**********************************************************************************************************************
		public bool DFSPPedApproved
		{
			get{return mDFSPPedApproved;}
			set{mDFSPPedApproved = value;}
		}
		//**********************************************************************************************************************
		public DateTime DFSPDate
		{
			get{return mDFSPDate;}
			set{mDFSPDate = value;}
		}
		//**********************************************************************************************************************
		public string DFSPInfo
		{
			get{return mDFSPInfo;}
			set{mDFSPInfo = value;}
		}
        //**********************************************************************************************************************
        public bool ADJGISTApproved
        {
            get { return mADJGISTApproved; }
            set { mADJGISTApproved = value; }
        }
        //**********************************************************************************************************************
        public bool ADJGISTPedApproved
        {
            get { return mADJGISTPedApproved; }
            set { mADJGISTPedApproved = value; }
        }
        //**********************************************************************************************************************
        public DateTime ADJGISTDate
        {
            get { return mADJGISTDate; }
            set { mADJGISTDate = value; }
        }
        //**********************************************************************************************************************
        public string ADJGISTInfo
        {
            get { return mADJGISTInfo; }
            set { mADJGISTInfo = value; }
        }

        //**********************************************************************************************************************
        public bool MDSApproved
        {
            get { return mMDSApproved; }
            set { mMDSApproved = value; }
        }
        //**********************************************************************************************************************
        public bool MDSPedApproved
        {
            get { return mMDSPedApproved; }
            set { mMDSPedApproved = value; }
        }
        //**********************************************************************************************************************
        public DateTime MDSDate
        {
            get { return mMDSDate; }
            set { mMDSDate = value; }
        }
        //**********************************************************************************************************************
        public string MDSInfo
        {
            get { return mMDSInfo; }
            set { mMDSInfo = value; }
        }

        //**********************************************************************************************************************
        public bool MASTApproved
        {
            get { return mMASTApproved; }
            set { mMASTApproved = value; }
        }
        //**********************************************************************************************************************
        public bool MASTPedApproved
        {
            get { return mMASTPedApproved; }
            set { mMASTPedApproved = value; }
        }
        //**********************************************************************************************************************
        public DateTime MASTDate
        {
            get { return mMASTDate; }
            set { mMASTDate = value; }
        }
        //**********************************************************************************************************************
        public string MASTInfo
        {
            get { return mMASTInfo; }
            set { mMASTInfo = value; }
        }

        //**********************************************************************************************************************
        public bool HESApproved
        {
            get { return mHESApproved; }
            set { mHESApproved = value; }
        }
        //**********************************************************************************************************************
        public bool HESPedApproved
        {
            get { return mHESPedApproved; }
            set { mHESPedApproved = value; }
        }
        //**********************************************************************************************************************
        public DateTime HESDate
        {
            get { return mHESDate; }
            set { mHESDate = value; }
        }
        //**********************************************************************************************************************
        public string HESInfo
        {
            get { return mHESInfo; }
            set { mHESInfo = value; }
        }
        //**********************************************************************************************************************
        public bool NOAGlivec
        {
            get { return mNOAGlivec; }
            set { mNOAGlivec = value; }
        }
        //**********************************************************************************************************************
        public int NOATasigna
        {
            get { return mNOATasigna; }
            set { mNOATasigna = value; }
        }
        //**********************************************************************************************************************
        public bool TasignaPedApproved
        {
            get { return mTasignaPedApproved; }
            set { mTasignaPedApproved = value; }
        }
	}
}
