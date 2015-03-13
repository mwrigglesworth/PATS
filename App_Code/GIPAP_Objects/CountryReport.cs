using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for CountryReport.
	/// </summary>
	public class CountryReport
	{
		public int CountryID;
		public string CountryHeader;
		public string ResultCount;
		public string SubHeader;
		public DataSet ResultSet;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		//string connString = "SERVER=TESTBOX1;DATABASE=GIPAP;PWD=secret;UID=sa;";

		//**********************************************************************************************************************
		public CountryReport()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		//**********************************************************************************************************************
		public CountryReport(int currID)
		{
			this.CountryID = currID;
        }
        //**********************************************************************************************************************
        public DataSet NoaTotals(DateTime rDate)
        {
            DataSet ds;

            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[0].Value = this.CountryID;

            arrParams[1] = new SqlParameter("@ReportDate", SqlDbType.SmallDateTime);
            arrParams[1].Value = rDate;

            arrParams[2] = new SqlParameter("@EndDate", SqlDbType.SmallDateTime);
            arrParams[2].Value = rDate.AddDays(1);

            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_NOATotalsReport", arrParams);

            this.CountryHeader = ds.Tables[0].Rows[0]["countryname"].ToString();
            return ds;
        }
        //**********************************************************************************************************************
        public DataSet NoaIntakeReport(DateTime sDate, DateTime eDate)
        {
            DataSet ds;

            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[0].Value = this.CountryID;

            arrParams[1] = new SqlParameter("@StartDate", SqlDbType.SmallDateTime);
            arrParams[1].Value = sDate;

            arrParams[2] = new SqlParameter("@EndDate", SqlDbType.SmallDateTime);
            arrParams[2].Value = eDate.AddDays(1);

            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_NOAIntakeReport", arrParams);

            this.CountryHeader = ds.Tables[0].Rows[0]["countryname"].ToString();
            return ds;
        }
		//**********************************************************************************************************************
		public void GetCountryPatients(int currID, string Urole)
		{
			this.CountryID = currID;

			DataSet ds = new DataSet();

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			arrParams[1] = new SqlParameter("@Urole", SqlDbType.VarChar, 50);
			arrParams[1].Value = Urole;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryPatients2", arrParams);
			//this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();
			//this.ResultCount = this.ResultSet.Tables[1].Rows[0]["CountryCount"].ToString() + " Patients Found.";
		}
		//**********************************************************************************************************************
		public void GetCountryPhysicians(int currID, string Urole)
		{
			this.CountryID = currID;

			DataSet ds = new DataSet();

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			arrParams[1] = new SqlParameter("@Urole", SqlDbType.VarChar, 50);
			arrParams[1].Value = Urole;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryPhysicians2", arrParams);
			
		}
		//**********************************************************************************************************************
		public void GetCountrySAE(int currID, string Urole)
		{
			this.CountryID = currID;

			DataSet ds = new DataSet();

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			arrParams[1] = new SqlParameter("@Urole", SqlDbType.VarChar, 50);
			arrParams[1].Value = Urole;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountrySAE", arrParams);
			this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();
			this.ResultCount = this.ResultSet.Tables[1].Rows.Count.ToString() + " Adverse Events Found.";
		}
		//**********************************************************************************************************************
		public void GetCountryMaxStations(int currID)
		{
			this.CountryID = currID;

			DataSet ds = new DataSet();

			SqlParameter arrParams =  new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams.Value = this.CountryID;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryMaxStations2", arrParams);
			
		}
		//**********************************************************************************************************************
		public void GetCountryClinics(int currID, string Urole)
		{
			this.CountryID = currID;

			DataSet ds = new DataSet();

			SqlParameter[] arrParams = new SqlParameter[2];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			arrParams[1] = new SqlParameter("@Urole", SqlDbType.VarChar, 50);
			arrParams[1].Value = Urole;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryClinics2", arrParams);
			
		}
		//**********************************************************************************************************************
		public void DosageHistory(int currID)
		{
			this.CountryID = currID;

			SqlParameter arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams.Value = this.CountryID;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CountryDosageHistory", arrParams);
			this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();

            if (this.ResultSet.Tables[1].Rows.Count > 0)
            {
                this.ResultCount = "<b>Active Patient Dosage History</b>";
                this.ResultCount += "<br><br><font color=steelblue>" + this.ResultSet.Tables[1].Rows[0]["pin"].ToString() + "</font><br>";
                this.ResultCount += "<b>Diagnosis: </b>" + this.ResultSet.Tables[1].Rows[0]["diagnosis"].ToString() + "<br>";
                try
                {
                    DateTime ddate = Convert.ToDateTime(this.ResultSet.Tables[1].Rows[0]["glivecstartdate"].ToString());
                    this.ResultCount += "<b>Glivec Start Date: </b>" + ddate.Day.ToString() + " " + ddate.ToString("y") + "<br>";
                }
                catch
                {
                    this.ResultCount += "<b>Glivec Start Date: </b>Not Recorded";
                }
                this.ResultCount += "<b>Original Requested Dosage: </b>" + this.ResultSet.Tables[1].Rows[0]["originalrequesteddosage"].ToString() + "<br>";
                this.ResultCount += "<b>Original Approved Dosage: </b>" + this.ResultSet.Tables[1].Rows[0]["originalapproveddosage"].ToString() + "<br>";
                this.ResultCount += "<b>Dosage History</b>";
                if (this.ResultSet.Tables[1].Rows[0]["currentdosage"].ToString() != "")
                {
                    this.ResultCount += "<br>" + this.ResultSet.Tables[1].Rows[0]["currentdosage"].ToString();
                    try
                    {
                        DateTime sdate = Convert.ToDateTime(this.ResultSet.Tables[1].Rows[0]["startdate"].ToString());
                        this.ResultCount += " " + sdate.Day.ToString() + " " + sdate.ToString("y");
                    }
                    catch { }
                }
                if (this.ResultSet.Tables[1].Rows[0]["dosechange"].ToString() != "")
                {
                    this.ResultCount += "<br>" + this.ResultSet.Tables[1].Rows[0]["dosechange"].ToString();
                    try
                    {
                        DateTime sdate = Convert.ToDateTime(this.ResultSet.Tables[1].Rows[0]["changedate"].ToString());
                        this.ResultCount += " " + sdate.Day.ToString() + " " + sdate.ToString("y");
                    }
                    catch { }
                }
                string pin = this.ResultSet.Tables[1].Rows[0]["pin"].ToString();
                for (int i = 1; i < this.ResultSet.Tables[1].Rows.Count; i++)
                {
                    if (this.ResultSet.Tables[1].Rows[i]["pin"].ToString() != pin)
                    {
                        pin = this.ResultSet.Tables[1].Rows[i]["pin"].ToString();
                        this.ResultCount += "<br><br><font color=steelblue>" + this.ResultSet.Tables[1].Rows[i]["pin"].ToString() + "</font><br>";
                        this.ResultCount += "<b>Diagnosis: </b>" + this.ResultSet.Tables[1].Rows[i]["diagnosis"].ToString() + "<br>";
                        try
                        {
                            DateTime ddate = Convert.ToDateTime(this.ResultSet.Tables[1].Rows[i]["glivecstartdate"].ToString());
                            this.ResultCount += "<b>Glivec Start Date: </b>" + ddate.Day.ToString() + " " + ddate.ToString("y") + "<br>";
                        }
                        catch
                        {
                            this.ResultCount += "<b>Glivec Start Date: </b>Not Recorded";
                        }
                        this.ResultCount += "<b>Original Requested Dosage: </b>" + this.ResultSet.Tables[1].Rows[i]["originalrequesteddosage"].ToString() + "<br>";
                        this.ResultCount += "<b>Original Approved Dosage: </b>" + this.ResultSet.Tables[1].Rows[i]["originalapproveddosage"].ToString() + "<br>";
                        this.ResultCount += "<b>Dosage History</b>";
                    }
                    if (this.ResultSet.Tables[1].Rows[i]["currentdosage"].ToString() != "")
                    {
                        this.ResultCount += "<br>" + this.ResultSet.Tables[1].Rows[i]["currentdosage"].ToString();
                        try
                        {
                            DateTime sdate = Convert.ToDateTime(this.ResultSet.Tables[1].Rows[i]["startdate"].ToString());
                            this.ResultCount += " " + sdate.Day.ToString() + " " + sdate.ToString("y");
                        }
                        catch { }
                    }
                    if (this.ResultSet.Tables[1].Rows[i]["dosechange"].ToString() != "")
                    {
                        this.ResultCount += "<br>" + this.ResultSet.Tables[1].Rows[i]["dosechange"].ToString();
                        try
                        {
                            DateTime sdate = Convert.ToDateTime(this.ResultSet.Tables[1].Rows[i]["changedate"].ToString());
                            this.ResultCount += " " + sdate.Day.ToString() + " " + sdate.ToString("y");
                        }
                        catch { }
                    }
                }
            }
            else
            {
                this.ResultCount += "<i>No Active Patients</i>";
            }
		}
		//**********************************************************************************************************************
		public void CountryPersonelReport(int currID)
		{
			this.CountryID = currID;

			SqlParameter arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams.Value = this.CountryID;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CountryReport", arrParams);
			this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();
			//cpo
			this.ResultCount = "<hr><font color=steelblue size=4>GLC</font><br>";
			if(this.ResultSet.Tables[1].Rows.Count == 0)
			{
				this.ResultCount += "None<br>";
			}
			for(int i=0; i<this.ResultSet.Tables[1].Rows.Count; i++)
			{
				this.ResultCount += "<b>" + this.ResultSet.Tables[1].Rows[i]["firstname"].ToString() + " " + this.ResultSet.Tables[1].Rows[i]["lastname"].ToString() + "</b><br>";
				this.ResultCount += "<b>Address: </b>" + this.ResultSet.Tables[1].Rows[i]["street1"].ToString() + "<br>";
				this.ResultCount += this.ResultSet.Tables[1].Rows[i]["street2"].ToString() + "<br>";
				this.ResultCount += "<b>City: </b>" + this.ResultSet.Tables[1].Rows[i]["city"].ToString() + "<br>";
				this.ResultCount += "<b>State: </b>" + this.ResultSet.Tables[1].Rows[i]["stateprovince"].ToString() + "<br>";
				this.ResultCount += "<b>(tel)</b> " + this.ResultSet.Tables[1].Rows[i]["phone"].ToString() + "<br>";
				this.ResultCount += "<b>(fax)</b> " + this.ResultSet.Tables[1].Rows[i]["fax"].ToString() + "<br>";
				this.ResultCount += "<b>(email)</b> " + this.ResultSet.Tables[1].Rows[i]["email"].ToString() + "<br><br>";
			}
			//dsr
			this.ResultCount += "<hr><font color=steelblue size=4>DSR</font><br>";
			if(this.ResultSet.Tables[2].Rows.Count == 0)
			{
				this.ResultCount += "None<br>";
			}
			for(int i=0; i<this.ResultSet.Tables[2].Rows.Count; i++)
			{
				this.ResultCount += "<b>" + this.ResultSet.Tables[2].Rows[i]["firstname"].ToString() + " " + this.ResultSet.Tables[2].Rows[i]["lastname"].ToString() + "</b><br>";
				this.ResultCount += "<b>Address: </b>" + this.ResultSet.Tables[2].Rows[i]["street1"].ToString() + "<br>";
				this.ResultCount += this.ResultSet.Tables[2].Rows[i]["street2"].ToString() + "<br>";
				this.ResultCount += "<b>City: </b>" + this.ResultSet.Tables[2].Rows[i]["city"].ToString() + "<br>";
				this.ResultCount += "<b>State: </b>" + this.ResultSet.Tables[2].Rows[i]["stateprovince"].ToString() + "<br>";
				this.ResultCount += "<b>(tel)</b> " + this.ResultSet.Tables[2].Rows[i]["phone"].ToString() + "<br>";
				this.ResultCount += "<b>(fax)</b> " + this.ResultSet.Tables[2].Rows[i]["fax"].ToString() + "<br>";
				this.ResultCount += "<b>(email)</b> " + this.ResultSet.Tables[2].Rows[i]["email"].ToString() + "<br><br>";
			}
			//clinics & physicians
			this.ResultCount += "<hr><font color=steelblue size=4>Clinics & Physicians</font><br><br>";
			if(this.ResultSet.Tables[3].Rows.Count == 0)
			{
				this.ResultCount += "None<br>";
			}
			else
			{
				string clin = this.ResultSet.Tables[3].Rows[0]["Clinicname"].ToString();
				this.ResultCount += "<b><font color=steelblue>" + clin + "</font></b><br>";
				this.ResultCount += "<b>Address: </b>" + this.ResultSet.Tables[3].Rows[0]["street1"].ToString() + "<br>";
				this.ResultCount += this.ResultSet.Tables[3].Rows[0]["street2"].ToString() + "<br>";
				this.ResultCount += "<b>City: </b>" + this.ResultSet.Tables[3].Rows[0]["city"].ToString() + "<br>";
				this.ResultCount += "<b>State: </b>" + this.ResultSet.Tables[3].Rows[0]["stateprovince"].ToString() + "<br>";
				this.ResultCount += "<b>(tel)</b> " + this.ResultSet.Tables[3].Rows[0]["phone"].ToString() + "<br>";
				this.ResultCount += "<b>(fax)</b> " + this.ResultSet.Tables[3].Rows[0]["fax"].ToString() + "<br>";
				this.ResultCount += "<b>(email)</b> " + this.ResultSet.Tables[3].Rows[0]["email"].ToString() + "<br><br>";
				this.ResultCount += "<b><font color=gray>Administrator</font></b><br>";
				this.ResultCount += "<b>" + this.ResultSet.Tables[3].Rows[0]["adminfirstname"].ToString() + " " + this.ResultSet.Tables[3].Rows[0]["adminlastname"].ToString() + "</b><br>";
				this.ResultCount += "<b>(tel)</b> " + this.ResultSet.Tables[3].Rows[0]["adminphone"].ToString() + "<br>";
				this.ResultCount += "<b>(fax)</b> " + this.ResultSet.Tables[3].Rows[0]["adminfax"].ToString() + "<br>";
				this.ResultCount += "<b>(email)</b> " + this.ResultSet.Tables[3].Rows[0]["adminemail"].ToString() + "<br><br>";
				for(int i=0; i<this.ResultSet.Tables[3].Rows.Count; i++)
				{
					if(this.ResultSet.Tables[3].Rows[i]["clinicname"].ToString() != clin)
					{
						clin = this.ResultSet.Tables[3].Rows[i]["Clinicname"].ToString();
						this.ResultCount += "<hr><b><font color=steelblue>" + clin + "</font></b><br>";
						this.ResultCount += "<b>Address: </b>" + this.ResultSet.Tables[3].Rows[i]["street1"].ToString() + "<br>";
						this.ResultCount += this.ResultSet.Tables[3].Rows[i]["street2"].ToString() + "<br>";
						this.ResultCount += "<b>City: </b>" + this.ResultSet.Tables[3].Rows[i]["city"].ToString() + "<br>";
						this.ResultCount += "<b>State: </b>" + this.ResultSet.Tables[3].Rows[i]["stateprovince"].ToString() + "<br>";
						this.ResultCount += "<b>(tel)</b> " + this.ResultSet.Tables[3].Rows[i]["phone"].ToString() + "<br>";
						this.ResultCount += "<b>(fax)</b> " + this.ResultSet.Tables[3].Rows[i]["fax"].ToString() + "<br>";
						this.ResultCount += "<b>(email)</b> " + this.ResultSet.Tables[3].Rows[i]["email"].ToString() + "<br><br>";
						this.ResultCount += "<b><font color=gray>Administrator</font></b><br>";
						this.ResultCount += "<b>" + this.ResultSet.Tables[3].Rows[i]["adminfirstname"].ToString() + " " + this.ResultSet.Tables[3].Rows[i]["adminlastname"].ToString() + "</b><br>";
						this.ResultCount += "<b>(tel)</b> " + this.ResultSet.Tables[3].Rows[i]["adminphone"].ToString() + "<br>";
						this.ResultCount += "<b>(fax)</b> " + this.ResultSet.Tables[3].Rows[i]["adminfax"].ToString() + "<br>";
						this.ResultCount += "<b>(email)</b> " + this.ResultSet.Tables[3].Rows[i]["adminemail"].ToString() + "<br><br>";
					}
					this.ResultCount += "<b>" + this.ResultSet.Tables[3].Rows[i]["firstname"].ToString() + " " + this.ResultSet.Tables[3].Rows[i]["lastname"].ToString() + "</b><br>";
					this.ResultCount += "<b>City:</b> " + this.ResultSet.Tables[3].Rows[i]["pcity"].ToString() + "<br>";
					this.ResultCount += "<b>(tel)</b> " + this.ResultSet.Tables[3].Rows[i]["pphone"].ToString() + "<br>";
					this.ResultCount += "<b>(fax)</b> " + this.ResultSet.Tables[3].Rows[i]["pfax"].ToString() + "<br>";
					this.ResultCount += "<b>(email)</b> " + this.ResultSet.Tables[3].Rows[i]["pemail"].ToString() + "<br><br>";
				}
			}
			//indie physicians
			this.ResultCount += "<hr><font color=steelblue size=4>Independent Physicians</font><br><br>";
			if(this.ResultSet.Tables[4].Rows.Count == 0)
			{
				this.ResultCount += "None<br>";
			}
			for(int i=0; i<this.ResultSet.Tables[4].Rows.Count; i++)
			{
				this.ResultCount += "<b>" + this.ResultSet.Tables[4].Rows[i]["firstname"].ToString() + " " + this.ResultSet.Tables[4].Rows[i]["lastname"].ToString() + "</b><br>";
				this.ResultCount += "<b>City:</b> " + this.ResultSet.Tables[4].Rows[i]["city"].ToString() + "<br>";
				this.ResultCount += "<b>(tel)</b> " + this.ResultSet.Tables[4].Rows[i]["phone"].ToString() + "<br>";
				this.ResultCount += "<b>(fax)</b> " + this.ResultSet.Tables[4].Rows[i]["fax"].ToString() + "<br>";
				this.ResultCount += "<b>(email)</b> " + this.ResultSet.Tables[4].Rows[i]["email"].ToString() + "<br><br>";
			}
		}
		//**********************************************************************************************************************
		public void CountryDosageReport(int currID)
		{
			this.CountryID = currID;

			SqlParameter arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams.Value = this.CountryID;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CountryDosageReport", arrParams);
			this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();
			//200mg
			this.ResultCount += "<table width=400><tr><td colspan=3><br><br><hr><font color=steelblue size=4>200mg</font></td></tr>";
			this.ResultCount += "<tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Patient Count</b></td>";
			for(int i=0; i<this.ResultSet.Tables[1].Rows.Count; i++)
			{
				this.ResultCount += "<tr><td>" + this.ResultSet.Tables[1].Rows[i]["LastName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[1].Rows[i]["FirstName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[1].Rows[i]["PatientCount"].ToString() + "</td></tr>";
			}
			//260mg
			this.ResultCount += "<tr><td colspan=3><br><br><hr><font color=steelblue size=4>260mg</font></td></tr>";
			this.ResultCount += "<tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Patient Count</b></td>";
			for(int i=0; i<this.ResultSet.Tables[2].Rows.Count; i++)
			{
				this.ResultCount += "<tr><td>" + this.ResultSet.Tables[2].Rows[i]["LastName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[2].Rows[i]["FirstName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[2].Rows[i]["PatientCount"].ToString() + "</td></tr>";
			}
			//300mg
			this.ResultCount += "<tr><td colspan=3><br><br><hr><font color=steelblue size=4>300mg</font></td></tr>";
			this.ResultCount += "<tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Patient Count</b></td>";
			for(int i=0; i<this.ResultSet.Tables[3].Rows.Count; i++)
			{
				this.ResultCount += "<tr><td>" + this.ResultSet.Tables[3].Rows[i]["LastName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[3].Rows[i]["FirstName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[3].Rows[i]["PatientCount"].ToString() + "</td></tr>";
			}
			//400mg
			this.ResultCount += "<tr><td colspan=3><br><br><hr><font color=steelblue size=4>400mg</font></td></tr>";
			this.ResultCount += "<tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Patient Count</b></td>";
			for(int i=0; i<this.ResultSet.Tables[4].Rows.Count; i++)
			{
				this.ResultCount += "<tr><td>" + this.ResultSet.Tables[4].Rows[i]["LastName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[4].Rows[i]["FirstName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[4].Rows[i]["PatientCount"].ToString() + "</td></tr>";
			}
			//600mg
			this.ResultCount += "<tr><td colspan=3><br><br><hr><font color=steelblue size=4>600mg</font></td></tr>";
			this.ResultCount += "<tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Patient Count</b></td>";
			for(int i=0; i<this.ResultSet.Tables[5].Rows.Count; i++)
			{
				this.ResultCount += "<tr><td>" + this.ResultSet.Tables[5].Rows[i]["LastName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[5].Rows[i]["FirstName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[5].Rows[i]["PatientCount"].ToString() + "</td></tr>";
			}
			//800mg
			this.ResultCount += "<tr><td colspan=3><br><br><hr><font color=steelblue size=4>800mg</font></td></tr>";
			this.ResultCount += "<tr><td><b>Last Name</b></td><td><b>First Name</b></td><td><b>Patient Count</b></td>";
			for(int i=0; i<this.ResultSet.Tables[6].Rows.Count; i++)
			{
				this.ResultCount += "<tr><td>" + this.ResultSet.Tables[6].Rows[i]["LastName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[6].Rows[i]["FirstName"].ToString() + "</td>";
				this.ResultCount += "<td>" + this.ResultSet.Tables[6].Rows[i]["PatientCount"].ToString() + "</td></tr>";
			}
			this.ResultCount += "</table>";
				
		}
		//**********************************************************************************************************************
		public void CountryDiagnosisReport(int currID)
		{
			this.CountryID = currID;

			DataSet ds = new DataSet();

			SqlParameter arrParams = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams.Value = this.CountryID;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CountryDiagnosisReport", arrParams);
			this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();
		}
        //**********************************************************************************************************************
        public DataTable GetCountryActivity(int currID, DateTime sDate, DateTime eDate, string prog)
        {
            this.CountryID = currID;

            DataSet ds = new DataSet();

            SqlParameter[] arrParams = new SqlParameter[4];

            arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[0].Value = this.CountryID;

            arrParams[1] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            arrParams[1].Value = sDate;

            arrParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime);
            arrParams[2].Value = eDate.AddDays(1);

            arrParams[3] = new SqlParameter("@Program", SqlDbType.NVarChar, 50);
            arrParams[3].Value = prog;

            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CountryActivityLINQ", arrParams);
            this.CountryHeader = ds.Tables[5].Rows[0]["CountryHeader"].ToString();

            DataTable dtReport = new DataTable();
            DataColumn column;
            // Create new DataColumn, set DataType, ColumnName and add to DataTable.                
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Physician";
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

            /*left outer join*/
            var results = from table0 in ds.Tables[0].AsEnumerable()
                          join table1 in ds.Tables[1].AsEnumerable() on (int)table0["personid"] equals (int)table1["personid"]
                          into DataGroup1
                          from row1 in DataGroup1.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table2 in ds.Tables[2].AsEnumerable() on (int)table0["personid"] equals (int)table2["personid"]
                          into DataGroup2
                          from row2 in DataGroup2.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table3 in ds.Tables[3].AsEnumerable() on (int)table0["personid"] equals (int)table3["personid"]
                          into DataGroup3
                          from row3 in DataGroup3.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table4 in ds.Tables[4].AsEnumerable() on (int)table0["personid"] equals (int)table4["personid"]
                          into DataGroup4
                          from row4 in DataGroup4.DefaultIfEmpty<DataRow>(rowEmpty)
                          select new
                          {
                              p = (string)table0["physicianfirstname"] + " " + (string)table0["physicianlastname"],
                              app = (int)row1["ct"],
                              reapp = (int)row2["ct"],
                              den = (int)row3["ct"],
                              close = (int)row4["ct"]
                          };
            foreach (var item in results)
            {
                DataRow row = dtReport.NewRow();
                row["Physician"] = item.p;
                row["Approvals"] = item.app;
                row["Reapprovals"] = item.reapp;
                row["Denials"] = item.den;
                row["Closures"] = item.close;

                dtReport.Rows.Add(row);
            }
            DataTable dtTots = dtReport.Clone();
            DataRow totRow = dtTots.NewRow();
            totRow["Physician"] = "Totals";
            totRow["Approvals"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Approvals"));
            totRow["Reapprovals"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Reapprovals"));
            totRow["Denials"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Denials"));
            totRow["Closures"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Closures"));
            dtTots.Rows.Add(totRow);
            dtTots.Merge(dtReport);
            return dtTots;
        }
        //**********************************************************************************************************************
        public DataTable GetCountryEnrollment(int currID, int month, int year, bool initNOA)
        {
            this.CountryID = currID;

            DataSet ds = new DataSet();

            SqlParameter[] arrParams = new SqlParameter[4];

            arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[0].Value = this.CountryID;

            arrParams[1] = new SqlParameter("@Month", SqlDbType.Int);
            arrParams[1].Value = month;

            arrParams[2] = new SqlParameter("@Year", SqlDbType.Int);
            arrParams[2].Value = year;

            arrParams[3] = new SqlParameter("@NOA", SqlDbType.Bit);
            arrParams[3].Value = initNOA;

            ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_CountryEnrollment", arrParams);

            DataTable dtReport = new DataTable();
            DataColumn column;
            // Create new DataColumn, set DataType, ColumnName and add to DataTable. 
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "City";
            dtReport.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "State";
            dtReport.Columns.Add(column);

            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Physician";
            dtReport.Columns.Add(column);
            //enrolled (all)
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "Enrolled";
            dtReport.Columns.Add(column);
            //cml
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "CML";
            dtReport.Columns.Add(column);
            //gist
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "GIST";
            dtReport.Columns.Add(column);

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

            /*left outer join*/
            var results = from table0 in ds.Tables[0].AsEnumerable()
                          join table1 in ds.Tables[1].AsEnumerable() on (int)table0["personid"] equals (int)table1["personid"]
                          into DataGroup1
                          from row1 in DataGroup1.DefaultIfEmpty<DataRow>(rowEmpty)
                          join table2 in ds.Tables[2].AsEnumerable() on (int)table0["personid"] equals (int)table2["personid"]
                          into DataGroup2
                          from row2 in DataGroup2.DefaultIfEmpty<DataRow>(rowEmpty)

                          select new
                          {
                              city = (string)table0["city"],
                              state = (string)table0["STATEPROVINCE"],
                              p = (string)table0["Physician"],
                              enrolled = (int)table0["ct"],
                              cml = (int)row1["ct"],
                              gist = (int)row2["ct"]
                          };
            foreach (var item in results)
            {
                DataRow row = dtReport.NewRow();
                row["City"] = item.city;
                row["State"] = item.state;
                row["Physician"] = item.p;
                row["Enrolled"] = item.enrolled;
                row["CML"] = item.cml;
                row["GIST"] = item.gist;

                dtReport.Rows.Add(row);
            }
            DataTable dtTots = dtReport.Clone();
            DataRow totRow = dtTots.NewRow();
            totRow["City"] = "-";
            totRow["State"] = "-";
            totRow["Physician"] = "Totals";
            totRow["Enrolled"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("Enrolled"));
            totRow["CML"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("CML"));
            totRow["GIST"] = dtReport.AsEnumerable().Sum(x => x.Field<int>("GIST"));
            dtTots.Rows.Add(totRow);
            dtTots.Merge(dtReport);
            return dtTots;
        }
		//**********************************************************************************************************************
		public void ApprovalsBreakdown(int currID, DateTime sDate, DateTime eDate)
		{
			this.CountryID = currID;

			DataSet ds = new DataSet();

			SqlParameter[] arrParams = new SqlParameter[3];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			arrParams[1] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[1].Value = sDate;

			arrParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[2].Value = eDate.AddDays(1);

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_ApprovalsBreakdown", arrParams);
			this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();
		}
		//**********************************************************************************************************************
		public void ClosuresBreakdown(int currID, DateTime sDate, DateTime eDate, string Urole)
		{
			this.CountryID = currID;

			DataSet ds = new DataSet();

			SqlParameter[] arrParams = new SqlParameter[4];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			arrParams[1] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[1].Value = sDate;

			arrParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[2].Value = eDate.AddDays(1);

			arrParams[3] = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
			arrParams[3].Value = Urole;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_ClosuresBreakdown", arrParams);
			this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();
		}
		//**********************************************************************************************************************
		public void DenialsBreakdown(int currID, DateTime sDate, DateTime eDate, string Urole)
		{
			this.CountryID = currID;

			DataSet ds = new DataSet();

			SqlParameter[] arrParams = new SqlParameter[4];

			arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
			arrParams[0].Value = this.CountryID;

			arrParams[1] = new SqlParameter("@StartDate", SqlDbType.DateTime);
			arrParams[1].Value = sDate;

			arrParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime);
			arrParams[2].Value = eDate.AddDays(1);

			arrParams[3] = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
			arrParams[3].Value = Urole;

			this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_DenialsBreakdown", arrParams);
			this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();
		}
        //**********************************************************************************************************************
        public void GetCompleteCountryActivity(int currID, DateTime sDate, DateTime eDate, string Urole, string prog, string report)
        {
            this.CountryID = currID;

            DataSet ds = new DataSet();

            SqlParameter[] arrParams = new SqlParameter[5];

            arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[0].Value = this.CountryID;

            arrParams[1] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            arrParams[1].Value = sDate;

            arrParams[2] = new SqlParameter("@EndDate", SqlDbType.DateTime);
            arrParams[2].Value = eDate.AddDays(1);

            arrParams[3] = new SqlParameter("@Program", SqlDbType.NVarChar, 50);
            arrParams[3].Value = prog;

            arrParams[4] = new SqlParameter("@Report", SqlDbType.NVarChar, 50);
            arrParams[4].Value = report;

            if (Urole == "CPO" || Urole == "RCC" || Urole == "Novartis")
            {
                this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryActivityNovartis", arrParams);
            }
            else
            {
                this.ResultSet = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryActivity2", arrParams);
            }
            this.CountryHeader = this.ResultSet.Tables[0].Rows[0]["CountryHeader"].ToString();
        }
	}
}
