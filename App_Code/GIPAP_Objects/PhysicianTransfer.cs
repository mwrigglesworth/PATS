using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
    public class PhysicianTransfer
    {
        private int mFromID;
        private string mFromName;
        private string mFromInfo;
        private int mToID;
        private string mToName;
        private string mToInfo;

        public DataTable physDT;

        string connString = ConfigurationSettings.AppSettings["ConnectionString"];

        //**********************************************************************************************************************
        public PhysicianTransfer()
        {
        }
        //**********************************************************************************************************************
        public PhysicianTransfer(int fromid, int toid)
        {
            DataSet myData = new DataSet();
            SqlParameter[] arrParams = new SqlParameter[2];

            arrParams[0] = new SqlParameter("@FromID", SqlDbType.Int);
            arrParams[0].Value = fromid;

            arrParams[1] = new SqlParameter("@ToID", SqlDbType.Int);
            arrParams[1].Value = toid;

            myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianPatientTransferProfile", arrParams);

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
        public void Transfer(string createdby, bool pinc)
        {
            SqlParameter[] arrParams = new SqlParameter[4];

            arrParams[0] = new SqlParameter("@FromID", SqlDbType.Int);
            arrParams[0].Value = this.FromID;

            arrParams[1] = new SqlParameter("@ToID", SqlDbType.Int);
            arrParams[1].Value = this.ToID;

            arrParams[2] = new SqlParameter("@CreatedBy", SqlDbType.NChar, 50);
            arrParams[2].Value = createdby;

            arrParams[3] = new SqlParameter("@PINC", SqlDbType.Bit);
            if (pinc)
            {
                arrParams[3].Value = 1;
            }
            else
            {
                arrParams[3].Value = 0;
            }

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_PhysicianPatientTransfer", arrParams);
        }
		//**********************************************************************************************************************
        private void Inflate(DataSet ds)
        {
            this.FromID = Convert.ToInt32(ds.Tables[0].Rows[0]["Personid"]);
            this.FromName = ds.Tables[0].Rows[0]["firstname"].ToString() + " " + ds.Tables[0].Rows[0]["lastname"].ToString();
            this.FromInfo = "<a href=GIPAP.aspx?trgt=physicianinfo&choice=" + this.FromID.ToString() + ">" + ds.Tables[0].Rows[0]["firstname"].ToString() + " " + ds.Tables[0].Rows[0]["lastname"].ToString() + "</a>";
            this.FromInfo += "<br><b>Specialty:</b> " + ds.Tables[0].Rows[0]["specialty"].ToString();
            this.FromInfo += "<br><b>Email:</b> " + ds.Tables[0].Rows[0]["email"].ToString();
            this.FromInfo += "<br><br><b>Clinic(s): </b>";
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (i != 0)
                    {
                        this.FromInfo += " / ";
                    }
                    this.FromInfo += ds.Tables[1].Rows[i]["clinicname"].ToString();
                }
            }
            this.FromInfo += "<br><br><li><i>" + ds.Tables[2].Rows[0]["pcount"].ToString() + " Patients</i><br><b><font color=red>These patients will be transfered.</font></b>";

            //to physician
            if (ds.Tables[3].Rows.Count > 0)
            {
                this.ToID = Convert.ToInt32(ds.Tables[3].Rows[0]["Personid"]);
                this.ToName = ds.Tables[3].Rows[0]["firstname"].ToString() + " " + ds.Tables[3].Rows[0]["lastname"].ToString();
                this.ToInfo = "<a href=GIPAP.aspx?trgt=physicianinfo&choice=" + this.ToID.ToString() + ">" + ds.Tables[3].Rows[0]["firstname"].ToString() + " " + ds.Tables[3].Rows[0]["lastname"].ToString() + "</a>";
                this.ToInfo += "<br><b>Specialty:</b> " + ds.Tables[3].Rows[0]["specialty"].ToString();
                this.ToInfo += "<br><b>Email:</b> " + ds.Tables[3].Rows[0]["email"].ToString();
                this.ToInfo += "<br><br><b>Clinic(s): </b>";
                if (ds.Tables[4].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                    {
                        if (i != 0)
                        {
                            this.ToInfo += " / ";
                        }
                        this.ToInfo += ds.Tables[4].Rows[i]["clinicname"].ToString();
                    }
                }
                this.ToInfo += "<br><br><li><i>" + ds.Tables[5].Rows[0]["pcount"].ToString() + " Patients</i>";
            }

            //phys list
            this.physDT = ds.Tables[6];
        }
        //**********************************************************************************************************************
        public int FromID
        {
            get { return mFromID; }
            set { mFromID = value; }
        }
        //**********************************************************************************************************************
        public string FromName
        {
            get { return mFromName; }
            set { mFromName = value; }
        }
        //**********************************************************************************************************************
        public string FromInfo
        {
            get { return mFromInfo; }
            set { mFromInfo = value; }
        }
        //**********************************************************************************************************************
        public int ToID
        {
            get { return mToID; }
            set { mToID = value; }
        }
        //**********************************************************************************************************************
        public string ToName
        {
            get { return mToName; }
            set { mToName = value; }
        }
        //**********************************************************************************************************************
        public string ToInfo
        {
            get { return mToInfo; }
            set { mToInfo = value; }
        }
    }
}
