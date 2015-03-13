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
    public class FCRequest
    {
        private int mFCRequestID;
        private int mPatientID;
        private string mPIN;
        private string mFirstName;
        private string mLastName;
        private string mGIPAPStatus;
        private string mFromType;
        private string mToType;
        private string mSubject;
        private string mMessage;
        private int mReplyID;
        private string mReplySubject;
        private string mReplyTo;
        private string CreatedBy;
        private string CreateDate;
        private bool mResolved;
        private string ResolvedBy;
        private string ResolveDate;

        string connString = ConfigurationSettings.AppSettings["ConnectionString"];

        //**************************************************************************************************************
		public FCRequest()
		{
			//Default constructor
		}
        //**************************************************************************************************************
        public FCRequest(int patid, int repid, int req)
        {
            DataSet myData;
            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = patid;

            arrParams[1] = new SqlParameter("@ReplyID", SqlDbType.Int);
            arrParams[1].Value = repid;

            arrParams[2] = new SqlParameter("@FCRequestID", SqlDbType.Int);
            arrParams[2].Value = req;

            myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetFCRequestProfile", arrParams);
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
            SqlParameter[] arrParams = new SqlParameter[7];

            arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
            arrParams[0].Value = this.PatientID;

            arrParams[1] = new SqlParameter("@FromType", SqlDbType.NVarChar, 50);
            arrParams[1].Value = this.FromType;

            arrParams[2] = new SqlParameter("@ToType", SqlDbType.NVarChar, 50);
            arrParams[2].Value = this.ToType;

            arrParams[3] = new SqlParameter("@Subject", SqlDbType.NVarChar, 100);
            arrParams[3].Value = this.Subject;

            arrParams[4] = new SqlParameter("@Message", SqlDbType.Text);
            arrParams[4].Value = this.Message;

            arrParams[5] = new SqlParameter("@Createdby", SqlDbType.NVarChar, 50);
            arrParams[5].Value = createdby;

            arrParams[6] = new SqlParameter("@ReplyID", SqlDbType.Int);
            if (this.ReplyID == 0)
            {
                arrParams[6].Value = DBNull.Value;
            }
            else
            {
                arrParams[6].Value = this.ReplyID;
            }

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateFCRequest", arrParams);
        }
        //**************************************************************************************************************
        public void Resolve(string resby)
        {
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@FCRequestID", SqlDbType.Int);
            arrParams[0].Value = this.FCRequestID;

            arrParams[1] = new SqlParameter("@ResolvedBy", SqlDbType.NVarChar, 50);
            arrParams[1].Value = resby;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_ResolveFCRequest", arrParams);
        }            
        //**********************************************************************************************************************
        public string PatientHeader(string uRole)
        {
            StringBuilder pi = new StringBuilder();
            pi.Append("<font class=NoaHead>" + this.PIN + "<br>" + this.FirstName + " " + this.LastName + "</font>");
            pi.Append("<br><font color=gray><b>" + this.GIPAPStatus + "</b></font>");
            if (uRole != "FCCallCenter")
            {
                pi.Append("<br><br>[<a href=GIPAP.aspx?trgt=patientinfo&choice=" + this.PatientID.ToString() + ">Click here to view patient page</a>]");
            }
            return pi.ToString();
        }
        //**********************************************************************************************************************
        public string ViewRequest(string uRole)
        {
            StringBuilder vr = new StringBuilder();
            //vr.Append(this.PatientHeader(uRole) + "<hr>");
            vr.Append("<b><font color=gray>From: </b></font>" + this.FromType);
            vr.Append("<br><font color=gray>--> To: </font>" + this.ToType + "<BR>");
            if (this.ReplySubject != "")
            {
                vr.Append("<font color=gray><b>Reply To: </b>" + this.ReplySubject + "</font><br>");
            }
            vr.Append("<b>Subject: </b>" + this.Subject);
            vr.Append("<br><b>Message:</b><br>" + this.Message);
            vr.Append("<br><br><font color=gray>" + this.CreatedBy + "<br><i>" + this.CreateDate + "</i></font>");
            if (this.Resolved)
            {
                vr.Append("<br><b>Resolved </b>" + this.ResolvedBy + " <i>" + this.ResolveDate + "</i>");
            }
            else
            {
                vr.Append("<BR><font color=red><b>Unresolved</b></font>");
            }
            return vr.ToString();
        }
        //**************************************************************************************************************
        private void Inflate(DataSet ds)
        {
            this.PatientID = Convert.ToInt32(ds.Tables[0].Rows[0]["PatientID"]);
            this.PIN = ds.Tables[0].Rows[0]["PIN"].ToString();
            this.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
            this.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            this.GIPAPStatus = ds.Tables[0].Rows[0]["GipapStatus"].ToString();
            //request table
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.ReplyID = Convert.ToInt32(ds.Tables[1].Rows[0]["FCRequestID"]);
                this.ReplySubject = ds.Tables[1].Rows[0]["subject"].ToString();
                this.ReplyTo = ds.Tables[1].Rows[0]["fromtype"].ToString();
            }
            else
            {
                this.ReplyID = 0;
            }

            //actual request (for viewing)
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.FCRequestID = Convert.ToInt32(ds.Tables[2].Rows[0]["FCRequestID"]);
                this.FromType = ds.Tables[2].Rows[0]["fromtype"].ToString();
                this.ToType = ds.Tables[2].Rows[0]["totype"].ToString();
                this.Subject = ds.Tables[2].Rows[0]["subject"].ToString();
                this.Message = ds.Tables[2].Rows[0]["message"].ToString();
                this.CreatedBy = ds.Tables[2].Rows[0]["createdby"].ToString();
                this.CreateDate = ds.Tables[2].Rows[0]["createdate"].ToString();
                this.Resolved = Convert.ToBoolean(ds.Tables[2].Rows[0]["resolved"]);
                this.ResolvedBy = ds.Tables[2].Rows[0]["resolvedby"].ToString();
                this.ResolveDate = ds.Tables[2].Rows[0]["resolvedate"].ToString();
            }
        }
        //**********************************************************************************************************************
        public int FCRequestID
        {
            get { return mFCRequestID; }
            set { mFCRequestID = value; }
        }
        //**********************************************************************************************************************
        public int PatientID
        {
            get { return mPatientID; }
            set { mPatientID = value; }
        }
        //**********************************************************************************************************************
        public string PIN
        {
            get { return mPIN; }
            set { mPIN = value; }
        }
        //**********************************************************************************************************************
        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }
        //**********************************************************************************************************************
        public string LastName
        {
            get { return mLastName; }
            set { mLastName = value; }
        }
        //**********************************************************************************************************************
        public string FromType
        {
            get { return mFromType; }
            set { mFromType = value; }
        }
        //**********************************************************************************************************************
        public string GIPAPStatus
        {
            get { return mGIPAPStatus; }
            set { mGIPAPStatus = value; }
        }
        //**********************************************************************************************************************
        public string ToType
        {
            get { return mToType; }
            set { mToType = value; }
        }
        //**********************************************************************************************************************
        public string Subject
        {
            get { return mSubject; }
            set { mSubject = value; }
        }
        //**********************************************************************************************************************
        public string Message
        {
            get { return mMessage; }
            set { mMessage = value; }
        }
        //**********************************************************************************************************************
        public int ReplyID
        {
            get { return mReplyID; }
            set { mReplyID = value; }
        }
        //**********************************************************************************************************************
        public string ReplySubject
        {
            get { return mReplySubject; }
            set { mReplySubject = value; }
        }
        //**********************************************************************************************************************
        public string ReplyTo
        {
            get { return mReplyTo; }
            set { mReplyTo = value; }
        }
        //**********************************************************************************************************************
        public bool Resolved
        {
            get { return mResolved; }
            set { mResolved = value; }
        }
    }
}
