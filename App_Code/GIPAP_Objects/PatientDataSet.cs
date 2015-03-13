using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for PatientDataSet.
	/// </summary>
	public class PatientDataSet
	{
		private int mPatientID;
        /*private string mHeader;
        private string mDiagnosis;
        private bool Noa;*/
		//data tables
		public DataTable dsSae;
		public DataTable dsEmails;
		public DataTable dsIntake;
		public DataTable dsDenied;
		public DataTable dsReassess;
		public DataTable dsReApprove;
		public DataTable dsDosageChange;
		public DataTable dsExtend;
		public DataTable dsClose;
		public DataTable dsReactivate;
		/*public DataTable dsReapprovalRequests;
		public DataTable dsOtherRequests;*/
		public DataTable dsRequestHistory;
		//public DataTable dsFEFUpdates;
		public DataTable dsFEFHistory;
		public DataTable PhysicianHistory;
        private DataTable PhysicianTransferRequests;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];

		//**************************************************************************************************************
		public PatientDataSet()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		//**************************************************************************************************************
		public PatientDataSet(int currid, string dset)
		{
			//Default constructor using the currid to populate the parameters
			if(currid == 0)
			{
				return;
			}
			else
			{
				DataSet myData;
				SqlParameter[] arrParams = new SqlParameter[2];

				arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
				arrParams[0].Value = currid;

				arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
				arrParams[1].Value = dset;

				myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientDataSetProfile2", arrParams);
                Inflate(currid, myData, dset);

				myData.Dispose();
			}
		}
		//**************************************************************************************************************
		private void Inflate(int pid, DataSet ds, string dset)
		{
			//Populates the objects parameters with the data returned from the database
			this.PatientID = pid;
            /*this.Noa = Convert.ToBoolean(ds.Tables[0].Rows[0]["noa"]);
			this.Header = ds.Tables[0].Rows[0]["Header"].ToString();
			this.Diagnosis = ds.Tables[0].Rows[0]["Diagnosis"].ToString();*/
            if (dset == "sae")
            {
                this.dsSae = ds.Tables[0];
            }
            else if (dset == "emails")
            {
                this.dsEmails = ds.Tables[0];
            }
            else if (dset == "statushistory" || dset == "requesthistory")
            {
                this.dsIntake = ds.Tables[0];
                this.dsDenied = ds.Tables[1];
                this.dsReassess = ds.Tables[2];
                this.dsReApprove = ds.Tables[3];
                this.dsDosageChange = ds.Tables[4];
                this.dsExtend = ds.Tables[5];
                this.dsClose = ds.Tables[6];
                this.dsReactivate = ds.Tables[7];
                this.dsRequestHistory = ds.Tables[8];
            }
			//this.dsReapprovalRequests = ds.Tables[11];
			//this.dsOtherRequests = ds.Tables[12];
			//this.dsFEFUpdates = ds.Tables[14];
            else if (dset == "fefhistory")
            {
                this.dsFEFHistory = ds.Tables[0];
            }
            else if (dset == "physicianhistory")
            {
                this.PhysicianHistory = ds.Tables[0];
            }
            else if (dset == "PhysicianTransferRequests")
            {
                this.PhysicianTransferRequests = ds.Tables[0];
            }
		}
		//**********************************************************************************************************************
		public string PatientDataLinks(string Urole)
		{
			string dlinks = "";
			if(Urole == "TMFUser" || Urole == "MaxStation")
			{
				dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=statushistory><font color=blue>Status History</font></a> | ";
				dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=requesthistory><font color=blue>Request History</font></a> | ";
				dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=sae><font color=blue>Adverse Events</font></a> | ";
				dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=emails><font color=blue>Emails</font></a> | ";
				dlinks += "<a href=PatientDataSets.aspx?choice=" + this.PatientID.ToString() + "&ds=physicianhistory><font color=blue>Physician History</font></a><br><br>";
			}
			return dlinks;
		}
		//**********************************************************************************************************************
		public string PatientEmailLinks()
		{
			string dlinks = "";
			dlinks += "<a href=PatientEmail.aspx?mailType=firstReminderEmailPatient&choice=" + this.PatientID.ToString() + "><font color=steelblue>Reminder</font></a> | ";
			dlinks += "<a href=PatientEmail.aspx?mailType=Day90ReminderEmailPatient&choice=" + this.PatientID.ToString() + "><font color=steelblue>90 Day Reminder</font></a> | ";
			dlinks += "<a href=PatientEmail.aspx?mailType=CloseEmailPatient&choice=" + this.PatientID.ToString() + "><font color=steelblue>Closing</font></a> | ";
			dlinks += "<a href=PatientEmail.aspx?mailType=ApprovalEmailPatient&choice=" + this.PatientID.ToString() + "><font color=steelblue>Approval</font></a> | ";
            dlinks += "<a href=PatientEmail.aspx?mailType=ReApprovalEmailPatient&choice=" + this.PatientID.ToString() + "><font color=steelblue>Reapproval</font></a>";
            //if (this.Noa)
            //{ i dont know why this was only for noa
                dlinks += "<hr><a href=PatientEmail.aspx?mailType=ExtentionEmailPatient&choice=" + this.PatientID.ToString() + "><font color=steelblue>Extension</font></a> | ";
            /*}
            else
            {
                dlinks += " | ";
            }*/
            dlinks += "<a href=PatientEmail.aspx?mailType=Noa10DaySupply&choice=" + this.PatientID.ToString() + "><font color=steelblue>NOA 10 Day Extension</font></a> | ";
			dlinks += "<a href=PatientEmail.aspx?mailType=DoseChangeEmailPatient&choice=" + this.PatientID.ToString() + "><font color=steelblue>Dose Change</font></a> | ";
			dlinks += "<a href=PatientEmail.aspx?mailType=Blank&choice=" + this.PatientID.ToString() + "><font color=steelblue>Blank</font></a>";
			return dlinks;
		}
		//**********************************************************************************************************************
        public string PhysicianTransferRequestHistory()
        {
            string reHis = "";
            if (this.PhysicianTransferRequests.Rows.Count > 0)
            {
                for (int i = 0; i < this.PhysicianTransferRequests.Rows.Count; i++)
                {
                    reHis += "<font color=red><b><li>Transfer Request</font></b> " + this.PhysicianTransferRequests.Rows[i]["physicianname"].ToString();
                    reHis += "<br><font color=gray>" + this.PhysicianTransferRequests.Rows[i]["createdby"].ToString();
                    reHis += "<br>" + this.PhysicianTransferRequests.Rows[i]["createdate"].ToString() + "</font>";
                }
            }
            return reHis;
        }
		//**********************************************************************************************************************
		public string RequestHistory()
		{
			string reHis = "";
			if(this.dsRequestHistory.Rows.Count == 0)
			{
				return reHis;
			}
			for(int i=0; i<this.dsRequestHistory.Rows.Count; i++)
			{
				reHis += "<font color=red><b><li>" + this.dsRequestHistory.Rows[i]["requesttype"].ToString() + "</b></font> ";
				reHis += this.dsRequestHistory.Rows[i]["createdby"].ToString() + " - " + this.dsRequestHistory.Rows[i]["createdate"].ToString();
				if(this.dsRequestHistory.Rows[i]["requesttype"].ToString() == "Extend")
				{
					reHis += "<br><b>Extend Until: </b>" + this.dsRequestHistory.Rows[i]["enddate"].ToString();
					reHis += "<br><b>Reason: </b>" + this.dsRequestHistory.Rows[i]["statusreason"].ToString();
				}
				else if(this.dsRequestHistory.Rows[i]["requesttype"].ToString() == "Deny")
				{
					reHis += "<br><b>Reason: </b>" + this.dsRequestHistory.Rows[i]["statusreason"].ToString();
					reHis += "<br><b>Notes: </b>" + this.dsRequestHistory.Rows[i]["notes"].ToString();
				}
				else if(this.dsRequestHistory.Rows[i]["requesttype"].ToString() == "Close")
				{
					reHis += "<br><b>Reason: </b>" + this.dsRequestHistory.Rows[i]["statusreason"].ToString();
				}
				else if(this.dsRequestHistory.Rows[i]["requesttype"].ToString() == "Reactivate")
				{
					reHis += "<br><b>Reason: </b>" + this.dsRequestHistory.Rows[i]["reactivatereason"].ToString();
					reHis += "<br><b>Dosage: </b>" + this.dsRequestHistory.Rows[i]["currentdosage"].ToString();
					reHis += "<br><b>If different from previous dosage, please explain: </b>" + this.dsRequestHistory.Rows[i]["changedosagereason"].ToString();
					reHis += "<br>Do you recommend that the patient restart treatment with Glivec?";
					if(Convert.ToBoolean(this.dsRequestHistory.Rows[i]["restarttreatment"]))
					{
						reHis += "<b>Yes</b>";
					}
					else
					{
						reHis += "<b>No</b>";
					}
					reHis += "<br>To your knowlege has the financial status of the patient remained the same?";
					if(Convert.ToBoolean(this.dsRequestHistory.Rows[i]["financialstatus"]))
					{
						reHis += "<b>Yes</b>";
					}
					else
					{
						reHis += "<b>No</b>";
					}
					if(this.dsRequestHistory.Rows[i]["currentcmlphase"].ToString() != "")
					{
						reHis += "<br><b>Current CML Phase: </b>" + this.dsRequestHistory.Rows[i]["currentcmlphase"].ToString();
					}
					reHis += "<br><b>Notes: </b>" + this.dsRequestHistory.Rows[i]["notes"].ToString();
				}
				else if(this.dsRequestHistory.Rows[i]["requesttype"].ToString() == "ReAssess")
				{
					reHis += "<br><b>Notes: </b>" + this.dsRequestHistory.Rows[i]["notes"].ToString();
				}
				else if(this.dsRequestHistory.Rows[i]["requesttype"].ToString() == "DosageChange")
				{
					reHis += "<br><b>Current Dosage: </b>" + this.dsRequestHistory.Rows[i]["currentdosage"].ToString();
					reHis += "<br><b>Requested Dosage: </b>" + this.dsRequestHistory.Rows[i]["requesteddosage"].ToString();
					reHis += "<br><b>Reason: </b>" + this.dsRequestHistory.Rows[i]["changedosagereason"].ToString();
				}
				else if(this.dsRequestHistory.Rows[i]["requesttype"].ToString() == "SupplyUpdate")
				{
					reHis += "<br>To your knowlege, does the patient have a supply of Glivec beyond ";
					reHis += this.dsRequestHistory.Rows[i]["enddate"].ToString() + "? ";
					if(this.dsRequestHistory.Rows[i]["glivecsupply"].ToString() == "0")
					{
						reHis += "<b>No</b>";
					}
					else if(this.dsRequestHistory.Rows[i]["glivecsupply"].ToString() == "1")
					{
						reHis += "<b>Yes</b>";
					}
					else
					{
						reHis += "<b>Don't Know</b>";
					}
					reHis += "<br>If yes, please inform us of the amount of supply that remains: (in # of days)<br>";
					reHis += "<b>" + this.dsRequestHistory.Rows[i]["remainingsupply"].ToString() + "<br>";
					reHis += "Notes: </b>" + this.dsRequestHistory.Rows[i]["notes"].ToString();
				}
			}
			return reHis;
		}
		//**************************************************************************************************************
		public string StatusHistory()
		{
			string raHis = "<div style='clear:both;'>";
			if(this.dsIntake.Rows.Count > 0)
			{
				raHis += "<table width=100% style='BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid' borderColor=steelblue><tr><td bgcolor=silver><b><font color=steelblue>Intake</b></font></td></tr>";
				raHis += this.dsIntake.Rows[0]["IntakeReport"].ToString() + "</table><br />";
			}
			if(this.dsDenied.Rows.Count > 0)
			{
				raHis += "<table width=100% style='BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid' borderColor=red>";
                raHis += this.dsDenied.Rows[0]["DeniedReport"].ToString() + "</table><br />";
			}
			if(this.dsReassess.Rows.Count > 0)
			{
				raHis += "<table width=100% style='BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid' borderColor=green>";
                raHis += this.dsReassess.Rows[0]["ReassessReport"].ToString() + "</table><br />";
			}
			if(this.dsReApprove.Rows.Count > 0)
			{
				for(int i=0; i<this.dsReApprove.Rows.Count; i++)
				{
					if(i == 0)
					{
                        raHis += "<font color=green><b>Initial Approval</b></font><br />";
						raHis += "<table width=100% style='BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid' borderColor=green>";
						raHis += "<tr bgcolor=silver><td><b>" + this.dsReApprove.Rows[i]["cdate"].ToString() + " - " + this.dsReApprove.Rows[i]["createdby"].ToString() + " - <font color=green>" + this.dsReApprove.Rows[i]["startdate"].ToString() + "</font> - <font color=red>" + this.dsReApprove.Rows[i]["enddate"].ToString()+ "</b></td></tr>";
						raHis += "<tr><td><b>Recommended Dosage: <font color=steelblue>" + this.dsReApprove.Rows[i]["CURRENTDOSAGE"].ToString() + "</font></b><br>";
						//raHis += "If different from current dosage, please explain:  " + this.dsReApprove.Rows[i]["CHANGEDOSAGEREASON"].ToString();
						raHis += "<br><b>Notes: </b>" + this.dsReApprove.Rows[i]["Notes"].ToString();// + "</td></tr>";
					}
					else
					{
						raHis += "<table width=100% style='BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; BORDER-BOTTOM: 1px solid' borderColor=silver>";
						raHis += "<tr bgcolor=silver><td><b>" + this.dsReApprove.Rows[i]["cdate"].ToString() + " - " + this.dsReApprove.Rows[i]["createdby"].ToString() + " - <font color=green>" + this.dsReApprove.Rows[i]["startdate"].ToString() + "</font> - <font color=red>" + this.dsReApprove.Rows[i]["enddate"].ToString() + "</font> - Auto Approved: </b>" + this.YN(Convert.ToBoolean(this.dsReApprove.Rows[i]["AUTOAPPROVE"])) + "</td></tr>";
						raHis += "<tr><td><b>Recommended Dosage: <font color=steelblue>" + this.dsReApprove.Rows[i]["CURRENTDOSAGE"].ToString() + "</font></b><br>";
						//raHis += "If different from current dosage, please explain:  " + this.dsReApprove.Rows[i]["CHANGEDOSAGEREASON"].ToString()+ "<br>";
						raHis += "<br><b>Do you recommend that the patient continue treatment with Glivec?</b>  " + this.YN(Convert.ToBoolean(this.dsReApprove.Rows[i]["RESTARTTREATMENT"])) + "<br>";
						//raHis += "To your knowlege has the financial status of the patient remained the same? " + this.YN(Convert.ToBoolean(this.dsReApprove.Rows[i]["FINANCIALSTATUS"])) + "<br>";
						/*if(this.Diagnosis == "CML")
						{
							raHis += "<b>Current CML Phase: </b>" + this.dsReApprove.Rows[i]["currentcmlphase"].ToString() + "<br>";
							raHis += "<b>Hematologic Response: </b>" + this.dsReApprove.Rows[i]["HEMATOLOGICALRESPONSE"].ToString() + "<br>";
							raHis += "<b>Cytogenetic Response: </b>" + this.dsReApprove.Rows[i]["CYTOGENETICRESPONSE"].ToString() + "<br>";
						}
						else if(this.Diagnosis == "GIST")
						{
							raHis += "<b>Tumor Response: </b>" + this.dsReApprove.Rows[i]["TumorRESPONSE"].ToString() + "<br>";
						}
						raHis += "To your knowlege, does the patient have a supply of Glivec beyond " + this.dsReApprove.Rows[i]["enddate"].ToString() + "? " + this.YN(Convert.ToInt32(this.dsReApprove.Rows[i]["GLIVECSUPPLY"].ToString()));
						if(Convert.ToInt32(this.dsReApprove.Rows[i]["GLIVECSUPPLY"]) == 1)
						{
							raHis += " " + this.dsReApprove.Rows[i]["REMAININGSUPPLY"].ToString();
						}*/
						if(this.dsReApprove.Rows[i]["receivedby"].ToString() != "")
						{
							raHis += "<br><b>Information Received By: </b>" + this.dsReApprove.Rows[i]["receivedby"].ToString();
						}
						raHis += "<br><b>Notes: </b>" + this.dsReApprove.Rows[i]["Notes"].ToString() + "</td></tr>";
					}
					if(this.dsRequestHistory.Rows.Count > 0)
					{
						for(int q=0; q<this.dsRequestHistory.Rows.Count; q++)
						{
							try
							{
								if(Convert.ToDateTime(this.dsRequestHistory.Rows[q]["createdate"]) > Convert.ToDateTime(this.dsReApprove.Rows[i]["startdate"]))
								{
									try
									{
										if(Convert.ToDateTime(this.dsRequestHistory.Rows[q]["createdate"]) < Convert.ToDateTime(this.dsReApprove.Rows[i+1]["startdate"]))
										{
											raHis += "<tr><td bgcolor=bisque><font color=red><b><li>Request: ";
											raHis += this.dsRequestHistory.Rows[q]["requesttype"].ToString() + "</font> - ";
											raHis += this.dsRequestHistory.Rows[q]["createdby"].ToString() + " </b>";
											raHis += this.dsRequestHistory.Rows[q]["createdate"].ToString() + "</td></tr>";
										}
									}
									catch
									{
										raHis += "<tr><td bgcolor=bisque><font color=red><b><li>Request: ";
										raHis += this.dsRequestHistory.Rows[q]["requesttype"].ToString() + "</font> - ";
										raHis += this.dsRequestHistory.Rows[q]["createdby"].ToString() + " </b>";
										raHis += this.dsRequestHistory.Rows[q]["createdate"].ToString() + "</td></tr>";
									}
								}
							}
							catch{}
						}
					}
					if(this.dsDosageChange.Rows.Count > 0)
					{
						for(int d=0; d<this.dsDosageChange.Rows.Count; d++)
						{
							if(this.dsDosageChange.Rows[d]["gipapdetailid"].ToString() == this.dsReApprove.Rows[i]["gipapdetailid"].ToString())
							{
								raHis += "<tr><td bgcolor=azure><font color=steelblue><b>Dosage Changed To " + this.dsDosageChange.Rows[d]["currentdosage"].ToString();
								raHis += "</font> - " + this.dsDosageChange.Rows[d]["createdby"].ToString() + " </b>" + this.dsDosageChange.Rows[d]["createdate"].ToString();
								raHis += "<br><b>Notes: </b>" + this.dsDosageChange.Rows[d]["notes"].ToString() + "</td></tr>";
								raHis += "</td></tr>";
							}
						}
					}
					if(this.dsExtend.Rows.Count > 0)
					{
						for(int e=0; e<this.dsExtend.Rows.Count; e++)
						{
							if(this.dsExtend.Rows[e]["gipapdetailid"].ToString() == this.dsReApprove.Rows[i]["gipapdetailid"].ToString())
							{
								raHis += "<tr><td bgcolor=blue><b><font color=white>Period Extended - " + this.dsExtend.Rows[e]["createdby"].ToString() + " - " + this.dsExtend.Rows[e]["createdate"].ToString() + "</b></font></td></tr>";
								raHis += "<tr><td><font color=blue>Period Extended Until: <b>" + this.dsExtend.Rows[e]["enddate"].ToString().Replace("12:00:00 AM", "") + "<br>Notes: </b>";
								raHis += this.dsExtend.Rows[e]["reason"].ToString() + "</font></td></tr>";
							}
						}
					}
					if(this.dsClose.Rows.Count > 0)
					{
						for(int c=0; c<this.dsClose.Rows.Count; c++)
						{
							if(this.dsClose.Rows[c]["gipapdetailid"].ToString() == this.dsReApprove.Rows[i]["gipapdetailid"].ToString())
							{
								raHis += "<tr><td bgcolor=red><b><font color=white>Case Closed - " + this.dsClose.Rows[c]["closedby"].ToString() + " - " + this.dsClose.Rows[c]["closeddate"].ToString() + "</b></font></td></tr>";
								raHis += "<tr><td><font color=red><b>Reason: </b>" + this.dsClose.Rows[c]["statusreason"].ToString() + "<br>";
								raHis += "<b>Notes: </b>" + this.dsClose.Rows[c]["notes"].ToString() + "</font></td></tr>";

								/*reactivate*/
								if(this.dsReactivate.Rows.Count > 0)
								{
									for(int r=0; r<dsReactivate.Rows.Count; r++)
									{
										if(this.dsReactivate.Rows[r]["patientcloseid"].ToString() == this.dsClose.Rows[c]["patientcloseid"].ToString())
										{
											raHis += "<tr bgcolor=green><td><b><font color=white>ReActivated - " + this.dsReactivate.Rows[r]["createdby"].ToString() + " - " + this.dsReactivate.Rows[r]["REACTIVATIONDATE"].ToString() + "</b></font></td></tr>";
											raHis += "<tr><td><font color=green><b>Reason: </b>" + this.dsReactivate.Rows[r]["statusreason"].ToString() + "</font></td></tr>";
										}
									}
								}
							}
						}
					}
                    raHis += "</table><br />";
				}
			}
			else
			{
				if(this.dsRequestHistory.Rows.Count > 0)
				{
					for(int q=0; q<this.dsRequestHistory.Rows.Count; q++)
					{
                        raHis += "<table width=100%><tr><td bgcolor=bisque><font color=red><b><li>Request: ";
						raHis += this.dsRequestHistory.Rows[q]["requesttype"].ToString() + "</font> - ";
						raHis += this.dsRequestHistory.Rows[q]["createdby"].ToString() + " </b>";
						raHis += this.dsRequestHistory.Rows[q]["createdate"].ToString() + "</td></tr></table>";
					}
				}
			}
            raHis += "</div>";
			return raHis;
		}
		//**************************************************************************************************************
		public string YN(bool tf)
		{
			if(tf)
			{
				return "<font color=steelblue><b>Yes</b></font>";
			}
			else
			{
				return "<font color=red><b>No</b></font>";
			}
		}
		//**************************************************************************************************************
		public string YN(int ind)
		{
			if(ind == 1)
			{
				return "<b>Yes</b>";
			}
			else if(ind == 0)
			{
				return "<b>No</b>";
			}
			else
			{
				return "<b>Don't Know</b>";
			}
		}
		//**************************************************************************************************************
		public int PatientID
		{
			get{return mPatientID;}
			set{mPatientID = value;}
		}

		//**************************************************************************************************************
        /*public string Header
        {
            get{return mHeader;}
            set{mHeader = value;}
        }
        //**************************************************************************************************************
        public string Diagnosis
        {
            get{return mDiagnosis;}
            set{mDiagnosis = value;}
        }*/
	}
}
