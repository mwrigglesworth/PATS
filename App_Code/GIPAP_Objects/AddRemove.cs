using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for AddRemove.
	/// </summary>
	public class AddRemove
	{
		public int SenderID;
		public string Sender;
		public string AddType;
		public DataSet AddRemoveDS;
		public int PersonCount;
		public string pPin;
        public string Suggestions;
        public int MSGroupID;
        public int StockistID;
			
		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
		string connPS = ConfigurationSettings.AppSettings["connPS"];

		//**************************************************************************************************************
		public AddRemove()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		//**************************************************************************************************************
		public AddRemove(int sID, string sndr, string atype, string uRole)
		{
			this.SenderID = sID;
			this.Sender = sndr;
			this.AddType = atype;

			if(this.Sender == "Patient")
			{
				SqlParameter[] arrParams = new SqlParameter[3];

				arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
				arrParams[0].Value = this.SenderID;

				arrParams[1] = new SqlParameter("@PersonType", SqlDbType.NVarChar, 50);
                arrParams[1].Value = this.AddType;

                arrParams[2] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
                arrParams[2].Value = uRole;

				this.AddRemoveDS = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPatientPersonList2", arrParams);

                if (this.AddType == "MaxStation" && this.AddRemoveDS.Tables[3].Rows.Count > 0)
                {
                    this.Suggestions = "<b>Suggestions:</b>";
                    for (int i = 0; i < this.AddRemoveDS.Tables[3].Rows.Count; i++)
                    {
                        this.Suggestions += "<li>" + this.AddRemoveDS.Tables[3].Rows[i]["SuggestionName"].ToString() + "</li>";
                    }
                }
                
			}
			else if(this.Sender == "Country")
			{
				SqlParameter[] arrParams = new SqlParameter[2];

				arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
				arrParams[0].Value = this.SenderID;

				arrParams[1] = new SqlParameter("@PersonType", SqlDbType.NVarChar, 50);
				arrParams[1].Value = this.AddType;
                
                this.AddRemoveDS = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetCountryPersonList2", arrParams);
			}
            else if (this.Sender == "Region")
            {
                SqlParameter arrParams = new SqlParameter();

                arrParams = new SqlParameter("@RegionID", SqlDbType.Int);
                arrParams.Value = this.SenderID;

                this.AddRemoveDS = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetRegionPersonList2", arrParams);
            }
            else if (this.Sender == "SubRegion")
            {
                SqlParameter arrParams = new SqlParameter();

                arrParams = new SqlParameter("@SubRegionID", SqlDbType.Int);
                arrParams.Value = this.SenderID;

                this.AddRemoveDS = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetSubRegionPersonList2", arrParams);
 
            }
		}
		//**********************************************************************************************************************
		public string MSSuggestions()
		{
			string poS = "";
			if(this.AddRemoveDS.Tables[4].Rows.Count > 0)
			{
				poS = "<font color=gray>Suggestions:<i>";
				for(int i=0; i<this.AddRemoveDS.Tables[4].Rows.Count; i++)
				{
					poS += "<li>" + this.AddRemoveDS.Tables[4].Rows[i]["suggestionname"].ToString();
				}
				poS += "</i></font>";
			}
			return poS;
		}
		//**********************************************************************************************************************
		public GIPAP_Objects.Email PhysicianTransferEmail(int pID, string mailType, int pCount)
		{
			DataSet ds;
			GIPAP_Objects.Email myEmail = new Email();
			SqlParameter arrParams = new SqlParameter();

			arrParams = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams.Value = pID;
			
			ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_getPhysicianTransferProfile", arrParams);

			myEmail.From = "gipap@themaxfoundation.org";
			if(ds.Tables[3].Rows.Count > 0)
			{
				for(int i=0; i<ds.Tables[3].Rows.Count; i++)
				{
					myEmail.CC += ds.Tables[3].Rows[i]["maxstationemail"].ToString() + "; ";
				}
			}
			myEmail.PatientID = pID;

			if(mailType == "delete")
			{
				myEmail.Subject = "Patient Transfer - " + ds.Tables[0].Rows[0]["patientname"].ToString() + " " + ds.Tables[0].Rows[0]["pin"].ToString();
				this.PersonCount = ds.Tables[1].Rows.Count;
				myEmail.To = ds.Tables[1].Rows[pCount]["email"].ToString();
				myEmail.Message = "Dear Dr. " + ds.Tables[1].Rows[pCount]["deletephysicianname"].ToString() + ",\n\n";
                myEmail.Message += "We are writing to inform you that we have transferred the file of patient ";
				myEmail.Message += ds.Tables[0].Rows[0]["patientname"].ToString() + " " + ds.Tables[0].Rows[0]["pin"].ToString();
                myEmail.Message += " from your care under " + ds.Tables[0].Rows[0]["currentprogram"].ToString() + ", to another provider. \n\n";
				/*for(int i=0; i<ds.Tables[2].Rows.Count; i++)
				{
					myEmail.Message += "Dr. " + ds.Tables[2].Rows[i]["createphysicianname"].ToString() + "\n\n";
				}*/
                //myEmail.Message += "Thank you for the care and support that you and your staff have provided to this patient, and for your support of " + ds.Tables[0].Rows[0]["currentprogram"].ToString() + ".\n\n";
                //myEmail.Message += "In an effort to enhance our patient privacy standards, the name of the new provider will no longer be disclosed. It is up to the patient to share that information if they choose to do so. Please note that this protocol change was updated in order to strengthen the physician transfer process.\n\n";
                myEmail.Message += "For questions please write to gipap@themaxfoundation.org.";
				myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
			}
			else if(mailType == "create")
			{
				myEmail.Subject = "Patient Transfer - " + ds.Tables[0].Rows[0]["patientname"].ToString() + " " + ds.Tables[0].Rows[0]["pin"].ToString();
				this.PersonCount = ds.Tables[2].Rows.Count;
				myEmail.To = ds.Tables[2].Rows[pCount]["email"].ToString();
				myEmail.Message = "Dear Dr. " + ds.Tables[2].Rows[pCount]["createphysicianname"].ToString() + ",\n\n";
                myEmail.Message += "We are writing to confirm that we have transferred the file of patient ";
				myEmail.Message += ds.Tables[0].Rows[0]["patientname"].ToString() + " " + ds.Tables[0].Rows[0]["pin"].ToString();
                myEmail.Message += " to your care under " + ds.Tables[0].Rows[0]["currentprogram"].ToString() + ".\n\n";
				/*for(int i=0; i<ds.Tables[1].Rows.Count; i++)
				{
					myEmail.Message += "Dr. " + ds.Tables[1].Rows[i]["deletephysicianname"].ToString() + "\n\n";
				}*/
                myEmail.Message += "If the transfer of this patient’s case to your care has occurred in error, we ask that you let us know to gipap@themaxfoundation.org. In the meantime, you will be receiving all future correspondence regarding this patient's case and you are expected to provide regular updates on this patient’s GIPAP case as per the current MOU.\n\n";
				myEmail.Message += "Thank you very much for your assistance and support.";
				myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
			}
			else if(mailType == "novartis")
			{
				myEmail.Subject = "Patient Transfer - " + ds.Tables[0].Rows[0]["pin"].ToString();
				if(ds.Tables[5].Rows[0]["email"].ToString() != "")
				{
					this.PersonCount = 0;
					myEmail.To = ds.Tables[5].Rows[0]["email"].ToString();
				}
				else
				{
					this.PersonCount = ds.Tables[6].Rows.Count;
					myEmail.To = ds.Tables[6].Rows[pCount]["email"].ToString();
				}
				myEmail.Message = "Dear " + ds.Tables[6].Rows[pCount]["glcname"].ToString() + ",\n\n";
                myEmail.Message += "We are writing to confirm that we have transferred the " + ds.Tables[0].Rows[0]["currentprogram"].ToString() + " patient ";
				myEmail.Message += "PIN " + ds.Tables[0].Rows[0]["pin"].ToString() + " from the care of one physician to another.\n\n";
				myEmail.Message += "Previous Physician(s):\n";
				for(int i=0; i<ds.Tables[1].Rows.Count; i++)
				{
					myEmail.Message += "\nDr. " + ds.Tables[1].Rows[i]["deletephysicianname"].ToString();
				}
				myEmail.Message += "\n\nNew Physician(s):\n";
				for(int i=0; i<ds.Tables[2].Rows.Count; i++)
				{
					myEmail.Message += "\nDr. " + ds.Tables[2].Rows[i]["createphysicianname"].ToString();
				}
                myEmail.Message += "\n\nAll future correspondence concerning " + ds.Tables[0].Rows[0]["pin"].ToString() + " will be sent to the new physician.  Please ensure that future supply of " + ds.Tables[0].Rows[0]["treatment"].ToString() + " is redirected to the correct party.";
				myEmail.Message += "\n\nSincerely, \n\nThe Max Foundation";
			}
			return myEmail;
		}
		//**************************************************************************************************************
        public DataSet getPOSuggestions(int pid)
        {
            SqlParameter arrParams = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams.Value = pid;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPOSuggestions", arrParams);
        }
        //**************************************************************************************************************
        public void Update(System.Web.UI.WebControls.CheckBoxList cb, string updatedby, string uRole, int sID, string sndr, string atype)
		{
            this.SenderID = sID;
            this.Sender = sndr;
            this.AddType = atype;

			if(this.Sender == "Patient")
			{
                if (this.AddType == "Physician")
				{
                    SqlParameter[] arrParams = new SqlParameter[3];

                    arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
                    arrParams[0].Value = this.SenderID;

                    arrParams[1] = new SqlParameter("@PIN", SqlDbType.NVarChar, 50);
                    arrParams[1].Direction = ParameterDirection.Output;

                    arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
                    arrParams[2].Value = updatedby;

                    SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeletePatientPhysicians2", arrParams);
                    
                    SqlParameter[] psParams = new SqlParameter[2];

                    psParams[0] = new SqlParameter("@PIN", SqlDbType.NVarChar, 50);
                    psParams[0].Value = arrParams[1].Value;

                    psParams[1] = new SqlParameter("@AddType", SqlDbType.NVarChar, 50);
                    psParams[1].Value = this.AddType;

					SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_DeleteGIPAPPatientPersons", psParams);

					for(int i=0; i<cb.Items.Count; i++)
					{
                        if (cb.Items[i].Selected)
                        {
                            arrParams[1] = new SqlParameter("@PersonID", SqlDbType.Int);
                            arrParams[1].Value = Convert.ToInt32(cb.Items[i].Value);

                            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatientPhysicians", arrParams);
                            SqlHelper.ExecuteNonQuery(connPS, CommandType.StoredProcedure, "spr_UpdateGIPAPPatientPersons", psParams[0], psParams[1], arrParams[1]);
                        }
					}
                    DataSet ds = this.getPOSuggestions(this.SenderID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            this.Suggestions += "<li>" + ds.Tables[0].Rows[i]["suggestionname"].ToString() + "</li>";
                        }
                    }
                    else
                    {
                        this.Suggestions = "";
                    }
				}
				else if(this.AddType == "MaxStation")
				{
                    SqlParameter[] arrParams = new SqlParameter[2];

                    arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
                    arrParams[0].Value = this.SenderID;

					SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeletePatientMaxStations", arrParams[0]);

                    if (this.MSGroupID == 0)
                    {
                        for (int i = 0; i < cb.Items.Count; i++)
                        {
                            if (cb.Items[i].Selected)
                            {
                                arrParams[1] = new SqlParameter("@PersonID", SqlDbType.Int);
                                arrParams[1].Value = Convert.ToInt32(cb.Items[i].Value);

                                SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatientMaxStations", arrParams);

                            }
                        }
                    }
                    else
                    {
                        arrParams[1] = new SqlParameter("@PersonGroupID", SqlDbType.Int);
                        arrParams[1].Value = this.MSGroupID;

                        SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatientMaxStationsByGroup", arrParams);
                    }
				}
				else if(this.AddType == "TMFUser")
				{
                    SqlParameter[] arrParams = new SqlParameter[2];

                    arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
                    arrParams[0].Value = this.SenderID;

					SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeletePatientPO", arrParams[0]);

					for(int i=0; i<cb.Items.Count; i++)
					{
                        if (cb.Items[i].Selected)
                        {
                            arrParams[1] = new SqlParameter("@PersonID", SqlDbType.Int);
                            arrParams[1].Value = Convert.ToInt32(cb.Items[i].Value);

                            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatientPO", arrParams);
                        }
					}
                }
                else if (this.AddType == "FEBranch")
                {
                    SqlParameter[] arrParams = new SqlParameter[3];

                    arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
                    arrParams[0].Value = this.SenderID;

                    SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeletePatientFCBranch", arrParams[0]);
                    
                    arrParams[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
                    arrParams[1].Value = updatedby;

                    if (this.StockistID != -1)
                    {
                        SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeletePatientStockist", arrParams[0]);

                        arrParams[2] = new SqlParameter("@StockistID", SqlDbType.Int);
                        arrParams[2].Value = this.StockistID;

                        SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatientStockist", arrParams);
                    }

                    for (int i = 0; i < cb.Items.Count; i++)
                    {
                        if (cb.Items[i].Selected)
                        {
                            arrParams[2] = new SqlParameter("@FCOfficeID", SqlDbType.Int);
                            arrParams[2].Value = Convert.ToInt32(cb.Items[i].Value);

                            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatientFCBranch", arrParams);
                            //send email now
                        }
                    }
                }

                else if (this.AddType == "Distributor")
                {
                    SqlParameter[] arrParams = new SqlParameter[3];

                    arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
                    arrParams[0].Value = this.SenderID;

                    SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeletePatientDistributor", arrParams[0]);

                    arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
                    arrParams[2].Value = updatedby;

                    //if (this.StockistID != -1)
                    //{
                    //    SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeletePatientDistributor", arrParams[0]);

                    //    arrParams[2] = new SqlParameter("@StockistID", SqlDbType.Int);
                    //    arrParams[2].Value = this.StockistID;

                    //    SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatientDistributor", arrParams);
                    //}

                    for (int i = 0; i < cb.Items.Count; i++)
                    {
                        if (cb.Items[i].Selected)
                        {
                            arrParams[1] = new SqlParameter("@DistributorID", SqlDbType.Int);
                            arrParams[1].Value = Convert.ToInt32(cb.Items[i].Value);

                            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdatePatientDistributor", arrParams);
                            //send email now
                        }
                    }
                }
			}
			else if(this.Sender == "Country")
			{
                SqlParameter[] arrParams = new SqlParameter[2];

                arrParams[0] = new SqlParameter("@CountryID", SqlDbType.Int);
				arrParams[0].Value = this.SenderID;

				if(this.AddType == "DSR")
				{
					SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeleteCountryDSR", arrParams[0]);

					for(int i=0; i<cb.Items.Count; i++)
					{
                        if (cb.Items[i].Selected)
                        {
                            arrParams[1] = new SqlParameter("@DSRID", SqlDbType.Int);
                            arrParams[1].Value = Convert.ToInt32(cb.Items[i].Value);

                            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateCountryDSR", arrParams);
                        }
					}
				}
				else if(this.AddType == "GLC")
				{
					SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeleteCountryCPO", arrParams[0]);

					for(int i=0; i<cb.Items.Count; i++)
					{
                        if (cb.Items[i].Selected)
                        {
                            arrParams[1] = new SqlParameter("@CPOID", SqlDbType.Int);
                            arrParams[1].Value = Convert.ToInt32(cb.Items[i].Value);

                            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateCountryCPO", arrParams);
                        }
					}
				}
			}
            else if(this.Sender == "Region")
            {
                SqlParameter[] arrParams = new SqlParameter[2];

                arrParams[0] = new SqlParameter("@RegionID", SqlDbType.Int);
                arrParams[0].Value = this.SenderID;

                SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeleteRCC", arrParams[0]);

                for(int i=0; i<cb.Items.Count; i++)
                {
                    if (cb.Items[i].Selected)
                    {
                        arrParams[1] = new SqlParameter("@RCCID", SqlDbType.Int);
                        arrParams[1].Value = Convert.ToInt32(cb.Items[i].Value);

                        SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateRCC", arrParams);
                    }
                }
            }
            else if(this.Sender == "SubRegion")
            {
                SqlParameter[] arrParams = new SqlParameter[2];

                arrParams[0] = new SqlParameter("@SubRegionID", SqlDbType.Int);
                arrParams[0].Value = this.SenderID;

                SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_DeleteSRCC", arrParams[0]);

                for(int i=0; i<cb.Items.Count; i++)
                {
                    if (cb.Items[i].Selected)
                    {
                        arrParams[1] = new SqlParameter("@SRCCID", SqlDbType.Int);
                        arrParams[1].Value = Convert.ToInt32(cb.Items[i].Value);

                        SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateSRCC", arrParams);
                    }
                }
            }
        }
        //**********************************************************************************************************************
        public void PhysicianTransferRequest(string createdby, int physID, bool agreeable)
        {

            SqlParameter[] arrParams = new SqlParameter[4];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.SenderID;

            arrParams[1] = new SqlParameter("@PhysicianID", SqlDbType.Int);
            arrParams[1].Value = physID;

            arrParams[2] = new SqlParameter("@Agreeable", SqlDbType.Bit);
            if (agreeable)
            {
                arrParams[2].Value = 1;
            }
            else
            {
                arrParams[2].Value = 0;
            }

            arrParams[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
            arrParams[3].Value = createdby;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePhysicianTransferRequest", arrParams);

        }
        //**********************************************************************************************************************
        public void ProcessPhysicianTransferRequest(string processedby, int ptID, bool approved)
        {
            //first update

            SqlParameter[] ptParams = new SqlParameter[3];

            ptParams[0] = new SqlParameter("@TransferRequestID", SqlDbType.Int);
            ptParams[0].Value = ptID;

            ptParams[1] = new SqlParameter("@ProcessedBy", SqlDbType.NVarChar, 50);
            ptParams[1].Value = processedby;

            ptParams[2] = new SqlParameter("@Approved", SqlDbType.Bit);
            if (approved)
            {
                ptParams[2].Value = 1;
            }
            else
            {
                ptParams[2].Value = 0;
            }

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ProcessPhysicianTransferRequest", ptParams);

        }
	}
}
