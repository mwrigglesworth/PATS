﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for Stockist
/// </summary>
namespace GIPAP_Objects
{
    public class Stockist
    {
        private int mStockistID;
        private string mOfficeName;
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

        string connString = ConfigurationSettings.AppSettings["ConnectionString"];

        //**************************************************************************************************************
        public Stockist()
        {
            //Default constructor
        }
        //**************************************************************************************************************
        public Stockist(int currid)
        {
            DataSet myData;
            SqlParameter CurrID = new SqlParameter("@StockistID", SqlDbType.Int);
            CurrID.Value = currid;

            myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetStockistProfile", CurrID);
            if (myData.Tables[0].Rows.Count <= 0)
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
        public void Create(string createdby)
        {
            SqlParameter[] arrParams = new SqlParameter[18];

            arrParams[0] = new SqlParameter("@OfficeName", SqlDbType.NVarChar, 200);
            arrParams[0].Value = this.OfficeName;

            arrParams[1] = new SqlParameter("@StockistID", SqlDbType.Int);
            arrParams[1].Direction = ParameterDirection.Output;

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

            //Send the data to the database
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateStockist", arrParams);

            this.StockistID = (int)arrParams[1].Value;
        }
        //**********************************************************************************************************************
        public void Update(string modifiedby)
        {
            SqlParameter[] arrParams = new SqlParameter[18];

            arrParams[0] = new SqlParameter("@OfficeName", SqlDbType.NVarChar, 200);
            arrParams[0].Value = this.OfficeName;

            arrParams[1] = new SqlParameter("@StockistID", SqlDbType.Int);
            arrParams[1].Value = this.StockistID;

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

            //Send the data to the database
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UpdateStockist", arrParams);
        }
        //**********************************************************************************************************************
        public void Approve(string modifiedby)
        {

            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@StockistID", SqlDbType.Int);
            arrParams[0].Value = this.StockistID;

            arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
            arrParams[1].Value = modifiedby;

            //Send the data to the database
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ApproveStockist", arrParams);
        }
        //**********************************************************************************************************************
        public void UnApprove(string modifiedby)
        {

            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@StockistID", SqlDbType.Int);
            arrParams[0].Value = this.StockistID;

            arrParams[1] = new SqlParameter("@ModifiedBy", SqlDbType.NVarChar, 50);
            arrParams[1].Value = modifiedby;

            //Send the data to the database
            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_UnApproveStockist", arrParams);
        }
        //**********************************************************************************************************************
        public string StockistHeader()
        {
            StringBuilder cInfo = new StringBuilder();
            cInfo.Append("<h1><font color=green>" + this.OfficeName + "</font></h1>");
            return cInfo.ToString();
        }
        //**********************************************************************************************************************
        public string StockistInfo()
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
            //cInfo.Append("<br><font class='lbl'>Username: </font>" + this.UserName);
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
        public DataSet StockistSearch()
        {
            DataSet ds = new DataSet();
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("Select '<a href=StockistInfo.aspx?choice=' + Convert(nvarchar, Stockistid) + '>' + OfficeName + '</a>' as Stockist");
            strSQL.Append(", City, Replace(Replace(Replace(Approved, 0, 'No'), 1, 'Yes'), 2, 'Pending') as '<b>Approved</b>' from tblStockist where ");
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
            this.StockistID = Convert.ToInt32(ds.Tables[0].Rows[0]["stockistID"]);
            this.OfficeName = Convert.ToString(ds.Tables[0].Rows[0]["OfficeName"]);
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

            /*if (ds.Tables[2].Rows.Count > 0)
            {
                this.UserName = ds.Tables[2].Rows[0]["username"].ToString();
                this.Password = ds.Tables[2].Rows[0]["password"].ToString();
            }*/
        }
        //**********************************************************************************************************************
        public int StockistID
        {
            get { return mStockistID; }
            set { mStockistID = value; }
        }
        //**********************************************************************************************************************
        public string OfficeName
        {
            get { return mOfficeName; }
            set { mOfficeName = value; }
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
    }
}
