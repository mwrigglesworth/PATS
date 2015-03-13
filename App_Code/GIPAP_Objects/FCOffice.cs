using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
    public class FCOffice
    {
        private int mFCOfficeID;
        private string mOfficeName;
        private string mOfficeType;
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
        private string mAdminMobile;
        private int mApproved;
        private DateTime mApprovedDate;
        private int mUserID;
        private string mUserName;
        private string mPassword;
        //freedom desk email - needed for all
        private string mFreedomDeskEmail;

        string connString = ConfigurationSettings.AppSettings["ConnectionString"];

        //**************************************************************************************************************
		public FCOffice()
		{
			//Default constructor
		}
        //**************************************************************************************************************
        public FCOffice(int currid)
        {
            DataSet myData;
                SqlParameter CurrID = new SqlParameter("@FCOfficeID", SqlDbType.Int);
                CurrID.Value = currid;

                myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetFCOfficeProfile", CurrID);
                if (myData.Tables[0].Rows.Count <= 0)
                {
                    if (myData.Tables[3].Rows.Count > 0)
                    {
                        for (int i = 0; i < myData.Tables[3].Rows.Count; i++)
                        {
                            this.FreedomDeskEmail = myData.Tables[3].Rows[i]["email"].ToString() + "; ";
                        }
                    }
                    return;
                }
                else
                {
                    Inflate(myData);
                }

                myData.Dispose();
        }
        //**********************************************************************************************************************
        public void Create(string createdby)
        {
            SqlParameter[] arrParams = new SqlParameter[19];

            arrParams[0] = new SqlParameter("@OfficeName", SqlDbType.NVarChar, 200);
            arrParams[0].Value = this.OfficeName;

            arrParams[1] = new SqlParameter("@OfficeType", SqlDbType.NVarChar, 50);
            arrParams[1].Value = this.OfficeType;

            arrParams[2] = new SqlParameter("@Street1", SqlDbType.NVarChar, 100);
            arrParams[2].Value = this.Street1;

            arrParams[3] = new SqlParameter("@Street2", SqlDbType.NVarChar, 100);
            arrParams[3].Value = this.Street2;

            arrParams[4] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
            arrParams[4].Value = this.City;

            arrParams[5] = new SqlParameter("@StateProvince", SqlDbType.NVarChar, 30);
            arrParams[5].Value = this.StateProvince;

            arrParams[6] = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 20);
            arrParams[6].Value = this.PostalCode;

            arrParams[7] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[7].Value = this.CountryID;

            arrParams[8] = new SqlParameter("@Phone", SqlDbType.NVarChar, 50);
            arrParams[8].Value = this.Phone;

            arrParams[9] = new SqlParameter("@Fax", SqlDbType.NVarChar, 50);
            arrParams[9].Value = this.Fax;

            arrParams[10] = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            arrParams[10].Value = this.Email;

            arrParams[11] = new SqlParameter("@AdminFirstName", SqlDbType.NVarChar, 50);
            arrParams[11].Value = this.AdminFirstName;

            arrParams[12] = new SqlParameter("@AdminLastName", SqlDbType.NVarChar, 50);
            arrParams[12].Value = this.AdminLastName;

            arrParams[13] = new SqlParameter("@AdminPhone", SqlDbType.NVarChar, 50);
            arrParams[13].Value = this.AdminPhone;

            arrParams[14] = new SqlParameter("@AdminFax", SqlDbType.NVarChar, 50);
            arrParams[14].Value = this.AdminFax;

            arrParams[15] = new SqlParameter("@AdminEmail", SqlDbType.NVarChar, 100);
            arrParams[15].Value = this.AdminEmail;

            arrParams[16] = new SqlParameter("@AdminMobile", SqlDbType.NVarChar, 50);
            arrParams[16].Value = this.AdminMobile;

            arrParams[17] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 50);
            arrParams[17].Value = createdby;

            arrParams[18] = new SqlParameter("@FCOfficeID", SqlDbType.Int);
            arrParams[18].Direction = ParameterDirection.Output;

            //Send the data to the database
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateFCOffice", arrParams);

            this.FCOfficeID = (int)arrParams[18].Value;
        }
        //**********************************************************************************************************************
        public void Update(string modifiedby)
        {
            SqlParameter[] arrParams = new SqlParameter[19];

            arrParams[0] = new SqlParameter("@OfficeName", SqlDbType.NVarChar, 200);
            arrParams[0].Value = this.OfficeName;

            arrParams[1] = new SqlParameter("@OfficeType", SqlDbType.NVarChar, 50);
            arrParams[1].Value = this.OfficeType;

            arrParams[2] = new SqlParameter("@Street1", SqlDbType.NVarChar, 100);
            arrParams[2].Value = this.Street1;

            arrParams[3] = new SqlParameter("@Street2", SqlDbType.NVarChar, 100);
            arrParams[3].Value = this.Street2;

            arrParams[4] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
            arrParams[4].Value = this.City;

            arrParams[5] = new SqlParameter("@StateProvince", SqlDbType.NVarChar, 30);
            arrParams[5].Value = this.StateProvince;

            arrParams[6] = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 20);
            arrParams[6].Value = this.PostalCode;

            arrParams[7] = new SqlParameter("@CountryID", SqlDbType.Int);
            arrParams[7].Value = this.CountryID;

            arrParams[8] = new SqlParameter("@Phone", SqlDbType.NVarChar, 50);
            arrParams[8].Value = this.Phone;

            arrParams[9] = new SqlParameter("@Fax", SqlDbType.NVarChar, 50);
            arrParams[9].Value = this.Fax;

            arrParams[10] = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            arrParams[10].Value = this.Email;

            arrParams[11] = new SqlParameter("@AdminFirstName", SqlDbType.NVarChar, 50);
            arrParams[11].Value = this.AdminFirstName;

            arrParams[12] = new SqlParameter("@AdminLastName", SqlDbType.NVarChar, 50);
            arrParams[12].Value = this.AdminLastName;

            arrParams[13] = new SqlParameter("@AdminPhone", SqlDbType.NVarChar, 50);
            arrParams[13].Value = this.AdminPhone;

            arrParams[14] = new SqlParameter("@AdminFax", SqlDbType.NVarChar, 50);
            arrParams[14].Value = this.AdminFax;

            arrParams[15] = new SqlParameter("@AdminEmail", SqlDbType.NVarChar, 100);
            arrParams[15].Value = this.AdminEmail;

            arrParams[16] = new SqlParameter("@AdminMobile", SqlDbType.NVarChar, 50);
            arrParams[16].Value = this.AdminMobile;

            arrParams[17] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
            arrParams[17].Value = modifiedby;

            arrParams[18] = new SqlParameter("@FCOfficeID", SqlDbType.Int);
            arrParams[18].Value = this.FCOfficeID;

            //Send the data to the database
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateFCOffice", arrParams);
        }
        //**********************************************************************************************************************
        public string HomePageLinks(int uid, string uRole)
        {
            StringBuilder sbLinks = new StringBuilder();
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[0].Value = uid;

            arrParams[1] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
            arrParams[1].Value = uRole;

            DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetFCQueues", arrParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //sbLinks.Append("<b>Workload for <font color=steelblue>" + ds.Tables[0].Rows[0]["officename"].ToString() + "</font></b><BR><BR>");

                if (ds.Tables[1].Rows[0]["count"].ToString() != "0")
                {
                    sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=unresolvedrequests' class='lbAR'>" + ds.Tables[1].Rows[0]["count"].ToString() + " Patient Action Requests</a><BR><BR>");
                }
                if (uRole == "FCBranch")
                {
                    if (ds.Tables[2].Rows[0]["count"].ToString() != "0")
                    {
                        sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=assignedpending'><font color=firebrick>" + ds.Tables[2].Rows[0]["count"].ToString() + " Pending Patients Recently Assigned to Your Branch</font></a><br><br>");
                    }
                    if (ds.Tables[3].Rows[0]["count"].ToString() != "0")
                    {
                        sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=yearlyreassess'><font color=green>" + ds.Tables[3].Rows[0]["count"].ToString() + " Patients Needing a New FEF Assessment</font></a><br><br>");
                    }
                    if (ds.Tables[4].Rows[0]["count"].ToString() != "0")
                    {
                        sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=reactivationrequests'><font color=orange>" + ds.Tables[4].Rows[0]["count"].ToString() + " Reactivation Requests</font></a><br><br>");
                    }
                    if (ds.Tables[5].Rows[0]["count"].ToString() != "0")
                    {
                        sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=reassessmentrequests'><font color=crimson>" + ds.Tables[5].Rows[0]["count"].ToString() + " Reassessment Requests</font></a><br><br>");
                    }
                    if (ds.Tables[6].Rows[0]["count"].ToString() != "0")
                    {
                        sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=changetreatmentrequests'><font color=purple>" + ds.Tables[6].Rows[0]["count"].ToString() + " Treatment Change Requests</font></a><br><br>");
                    }
                }
                else if (uRole == "FCFreedomDesk")
                {
                    if (ds.Tables[2].Rows[0]["count"].ToString() != "0")
                    {
                        sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=assignedpendingallbranches'><font color=firebrick>" + ds.Tables[2].Rows[0]["count"].ToString() + " Pending Patients Recently Assigned to a Branch</FONT></a><br><br>");
                    }
                    if (ds.Tables[3].Rows[0]["count"].ToString() != "0")
                    {
                        sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=yearlyreassessallbranches'><font color=green>" + ds.Tables[3].Rows[0]["count"].ToString() + " Patients Needing a New FEF Assessment</font></a><br><br>");
                    }
                    if (ds.Tables[4].Rows[0]["count"].ToString() != "0")
                    {
                        sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=reactivationrequestsallbranches'><font color=orange>" + ds.Tables[4].Rows[0]["count"].ToString() + " Reactivation Requests</font></a><br><br>");
                    }
                    if (ds.Tables[5].Rows[0]["count"].ToString() != "0")
                    {
                        sbLinks.Append("<li><a href='../FinancialEvaluator/DataDisplay.aspx?dset=reassessmentrequestsallbranches'><font color=crimson>" + ds.Tables[5].Rows[0]["count"].ToString() + " Reassessment Requests</font></a><br><br>");
                    }
                }
                        
            }
            return sbLinks.ToString();
        }
        //**********************************************************************************************************************
        public DataSet getFCDatasets(int uid, string dset)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[0].Value = uid;

            arrParams[1] = new SqlParameter("@Dset", SqlDbType.NVarChar, 50);
            arrParams[1].Value = dset;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetFCDatasets2", arrParams);
        }
        //**********************************************************************************************************************
        public DataSet listNOAPatients(string status, int uid, string urole)
        {
            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[0].Value = uid;

            arrParams[1] = new SqlParameter("@Status", SqlDbType.NVarChar, 50);
            arrParams[1].Value = status;

            arrParams[2] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
            arrParams[2].Value = urole;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_listNOAPatients2", arrParams);
        }
        //**********************************************************************************************************************
        public DataSet listRequests(int uid, string uRole, bool resolved)
        {
            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
            arrParams[0].Value = uid;

            arrParams[1] = new SqlParameter("@Urole", SqlDbType.NVarChar, 50);
            arrParams[1].Value = uRole;

            arrParams[2] = new SqlParameter("@Resolved", SqlDbType.Bit);
            if (resolved)
            {
                arrParams[2].Value = 1;
            }
            else
            {
                arrParams[2].Value = 0;
            }

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetFCRequests2", arrParams);
        }
        //**********************************************************************************************************************
        public DataSet listSentRequests(string uName)
        {
            SqlParameter arrParams = new SqlParameter("@Username", SqlDbType.NVarChar, 50);
            arrParams.Value = uName;

            return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetSentFCRequests2", arrParams);
        }
        //**********************************************************************************************************************
        public void Approve(string modifiedby)
        {

            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@FCOfficeID", SqlDbType.Int);
            arrParams[0].Value = this.FCOfficeID;

            arrParams[1] = new SqlParameter("@OfficeType", SqlDbType.NVarChar, 50);
            arrParams[1].Value = this.OfficeType;

            arrParams[2] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
            arrParams[2].Value = modifiedby;

            //Send the data to the database
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ApproveFCOffice", arrParams);
        }
        //**********************************************************************************************************************
        public void UnApprove(string modifiedby)
        {

            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@FCOfficeID", SqlDbType.Int);
            arrParams[0].Value = this.FCOfficeID;

            arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
            arrParams[1].Value = modifiedby;

            //Send the data to the database
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UnApproveFCOffice", arrParams);
        }
        //**********************************************************************************************************************
        public string fcHeader()
        {
            StringBuilder cInfo = new StringBuilder();
            cInfo.Append("<h1><font color=green>" + this.OfficeName + "</font></h1>");
            cInfo.Append("<h3>" + this.OfficeType + "</h3>");
            return cInfo.ToString();
        }
        //**********************************************************************************************************************
        public string fcInfo()
        {
            StringBuilder cInfo = new StringBuilder();
            cInfo.Append("<font class='lbl'>Address: </font>" + this.Street1 + "<br>");
            if (this.Street2 != "")
            {
                cInfo.Append(this.Street2 + "<br>");
            }
            cInfo.Append("<font class='lbl'>City: </font>" + this.City + "<br>");
            cInfo.Append("<font class='lbl'>State / Province: </font>" + this.StateProvince + "<br>");
            cInfo.Append("<font class='lbl'>Postal Code: </font>" + this.PostalCode + "<br>");
            cInfo.Append("<font class='lbl'>Country: </font>" + this.CountryName + "<br>");
            cInfo.Append("<font class='lbl'>Phone: </font>" + this.Phone + "<br>");
            cInfo.Append("<font class='lbl'>Fax: </font>" + this.Fax + "<br>");
            cInfo.Append("<font class='lbl'>Email: </font>" + this.Email + "<br>");
            cInfo.Append("<br><font class='lbl'>Username: </font>" + this.UserName);
            return cInfo.ToString();
        }
        //**********************************************************************************************************************
        public string ApprovedInfo(string Urole)
        {
            StringBuilder aInfo = new StringBuilder();
            /*if (Urole == "TMFUser")
            {*/
                aInfo.Append("<font class='lbl'>Approved: </font>");
                if (this.Approved == 1)
                {
                    aInfo.Append("<font color=green>Yes</font>");
                    if (this.ApprovedDate != Convert.ToDateTime("1/1/0001"))
                    {
                        aInfo.Append("<br><font class='lbl'>Approved Date: </font>" + this.ApprovedDate.Day.ToString() + " " + this.ApprovedDate.ToString("y"));
                    }
                }
                else if (this.Approved == 0)
                {
                    aInfo.Append("<font color=red>No</font>");
                }
                else if (this.Approved == 2)
                {
                    aInfo.Append("<font color=red>Pending</font>");
                }
            /*}*/
            return aInfo.ToString();
        }
        //**********************************************************************************************************************
        public string AdminInfo(string Urole)
        {
            StringBuilder aInfo = new StringBuilder();
            aInfo.Append("<font class='lbl'>Administrator: </font><font color=green>" + this.AdminFirstName + " " + this.AdminLastName + "</font><br>");
            aInfo.Append("<font class='lbl'>Phone: </font>" + this.AdminPhone + "<br>");
            aInfo.Append("<font class='lbl'>Fax: </font>" + this.AdminFax + "<br>");
            aInfo.Append("<font class='lbl'>Mobile: </font>" + this.AdminMobile + "<br>");
            aInfo.Append("<font class='lbl'>Email: </font>" + this.AdminEmail + "<br>");
            return aInfo.ToString();
        }
        //**********************************************************************************************************************
        public DataSet FCOfficeSearch()
        {
            DataSet ds = new DataSet();
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("Select '<a href=FEInfo.aspx?choice=' + Convert(nvarchar, fcofficeid) + '>' + OfficeName + '</a>' as Office");
            strSQL.Append(", Officetype as Type, City, Replace(Replace(Replace(Approved, 0, 'No'), 1, 'Yes'), 2, 'Pending') as '<b>Approved</b>' from tblfcOffice where ");
            strSQL.Append("OfficeName like '" + this.OfficeName + "%' and ");
            strSQL.Append("Phone like '" + this.Phone + "%' and ");
            strSQL.Append("Fax like '" + this.Fax + "%' and ");
            if (this.Approved != -1)
            {
                strSQL.Append("approved = " + this.Approved.ToString() + " and ");
            }
            strSQL.Append("AdminEmail like '" + this.AdminEmail + "%' and ");
            if (this.CountryID != 0)
            {
                strSQL.Append("CountryID = " + this.CountryID.ToString() + " and ");
            }
            strSQL.Append("City like '" + this.City + "%' and ");
            strSQL.Append("AdminFirstName like '" + this.AdminFirstName + "%' and ");
            strSQL.Append("AdminLastName like '" + this.AdminLastName + "%' and ");
            strSQL.Append("AdminPhone like '" + this.AdminPhone + "%' and ");
            strSQL.Append("AdminFax like '" + this.AdminFax + "%' and ");
            strSQL.Append("AdminEmail like '" + this.AdminEmail + "%' ");
            strSQL.Append("Order by OfficeName");

            ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, strSQL.ToString());
            return ds;
        }
        //**************************************************************************************************************
        private void Inflate(DataSet ds)
        {
            //Populates the objects parameters with the data returned from the database
            this.FCOfficeID = Convert.ToInt32(ds.Tables[0].Rows[0]["fcofficeID"]);
            this.OfficeName = Convert.ToString(ds.Tables[0].Rows[0]["OfficeName"]);
            this.OfficeType = Convert.ToString(ds.Tables[0].Rows[0]["OfficeType"]);
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
            this.AdminMobile = Convert.ToString(ds.Tables[0].Rows[0]["AdminMobile"]);
            this.Approved = Convert.ToInt32(ds.Tables[0].Rows[0]["Approved"]);
            try
            {
                this.ApprovedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ApprovedDate"]);
            }
            catch { }
            if (ds.Tables[0].Rows[0]["UserID"] == DBNull.Value)
            {
                this.UserID = 0;
            }
            else
            {
                this.UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);
            }

            this.CountryID = Convert.ToInt32(ds.Tables[1].Rows[0]["CountryID"]);
            this.CountryName = ds.Tables[1].Rows[0]["CountryName"].ToString();

            if (ds.Tables[2].Rows.Count > 0)
            {
                this.UserName = ds.Tables[2].Rows[0]["username"].ToString();
                this.Password = ds.Tables[2].Rows[0]["password"].ToString();
            }
        }
        //**********************************************************************************************************************
        public int FCOfficeID
        {
            get { return mFCOfficeID; }
            set { mFCOfficeID = value; }
        }
        //**********************************************************************************************************************
        public string OfficeName
        {
            get { return mOfficeName; }
            set { mOfficeName = value; }
        }
        //**********************************************************************************************************************
        public string OfficeType
        {
            get { return mOfficeType; }
            set { mOfficeType = value; }
        }
        //**********************************************************************************************************************
        public string Street1
        {
            get { return mStreet1; }
            set { mStreet1 = value; }
        }
        //**********************************************************************************************************************
        public string Street2
        {
            get { return mStreet2; }
            set { mStreet2 = value; }
        }
        //**********************************************************************************************************************
        public string City
        {
            get { return mCity; }
            set { mCity = value; }
        }
        //**********************************************************************************************************************
        public string StateProvince
        {
            get { return mStateProvince; }
            set { mStateProvince = value; }
        }
        //**********************************************************************************************************************
        public string PostalCode
        {
            get { return mPostalCode; }
            set { mPostalCode = value; }
        }
        //**********************************************************************************************************************
        public int CountryID
        {
            get { return mCountryID; }
            set { mCountryID = value; }
        }
        //**********************************************************************************************************************
        public string CountryName
        {
            get { return mCountryName; }
            set { mCountryName = value; }
        }
        //**********************************************************************************************************************
        public string Phone
        {
            get { return mPhone; }
            set { mPhone = value; }
        }
        //**********************************************************************************************************************
        public string Fax
        {
            get { return mFax; }
            set { mFax = value; }
        }
        //**********************************************************************************************************************
        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        //**********************************************************************************************************************
        public string AdminFirstName
        {
            get { return mAdminFirstName; }
            set { mAdminFirstName = value; }
        }
        //**********************************************************************************************************************
        public string AdminLastName
        {
            get { return mAdminLastName; }
            set { mAdminLastName = value; }
        }
        //**********************************************************************************************************************
        public string AdminPhone
        {
            get { return mAdminPhone; }
            set { mAdminPhone = value; }
        }
        //**********************************************************************************************************************
        public string AdminFax
        {
            get { return mAdminFax; }
            set { mAdminFax = value; }
        }
        //**********************************************************************************************************************
        public string AdminEmail
        {
            get { return mAdminEmail; }
            set { mAdminEmail = value; }
        }
        //**********************************************************************************************************************
        public string AdminMobile
        {
            get { return mAdminMobile; }
            set { mAdminMobile = value; }
        }
        //**********************************************************************************************************************
        public int Approved
        {
            get { return mApproved; }
            set { mApproved = value; }
        }
        //**********************************************************************************************************************
        public DateTime ApprovedDate
        {
            get { return mApprovedDate; }
            set { mApprovedDate = value; }
        }
        //**********************************************************************************************************************
        public int UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }
        //**********************************************************************************************************************
        public string UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }
        //**********************************************************************************************************************
        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        //**********************************************************************************************************************
        public string FreedomDeskEmail
        {
            get { return mFreedomDeskEmail; }
            set { mFreedomDeskEmail = value; }
        }
    }
}
