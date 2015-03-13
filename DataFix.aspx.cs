using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class DataFix : System.Web.UI.Page
{
    string connString = "SERVER=SQLSERV1;DATABASE=GIPAP;PWD=Deadmonds1;UID=sa; Connect Timeout=0;";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_BADDC");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            /*GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus();
            myStatus.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["startdate"]);
            myStatus.EndDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["enddate"]);
            myStatus.CalculateDoseChangeStrips(ds.Tables[0].Rows[i]["olddose"].ToString(), ds.Tables[0].Rows[i]["newdose"].ToString());

            SqlParameter[] arrParams = new SqlParameter[3];

            arrParams[0] = new SqlParameter("@DOSAGECHANGEID", SqlDbType.Int);
            arrParams[0].Value = Convert.ToInt32(ds.Tables[0].Rows[i]["DOSAGECHANGEID"]);

            arrParams[1] = new SqlParameter("@Strips100mg", SqlDbType.Int);
            arrParams[1].Value = myStatus.Strips100mg;

            arrParams[2] = new SqlParameter("@Strips400mg", SqlDbType.Int);
            arrParams[2].Value = myStatus.Strips400mg;

            SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_FIXDC", arrParams);*/
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string emailsql = "select distinct a.patientid, a.pin, a.gipapstatus, b.DONATIONLENGTH, b.NOAFEFID, c.[Date of approval]   from tblPatient a, v_CurrentNOAFEF b, KKT c  where a.PATIENTID = b.PATIENTID  and b.DONATIONLENGTH <> 250  and   a.PIN = c.PIN  and a.GIPAPSTATUS <> 'Active'  ";
        DataSet ds = SqlHelper.ExecuteDataset(connString, CommandType.Text, emailsql);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["gipapstatus"].ToString() == "Closed")
            {
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["patientid"]), "reactivate");
                myStatus.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date of approval"]);
                myStatus.EndDate = myStatus.StartDate.AddDays(119);
                myStatus.Notes = "Automatically logged for KKT cases as approved by NVS";
                if (myStatus.Diagnosis != "CML")
                {
                    myStatus.CurrentCMLPhase = "0";
                }
                myStatus.ReActivate("mwrigglesworth", "Patient Meets GIPAP Criteria");
            }
            else if (ds.Tables[0].Rows[i]["gipapstatus"].ToString() == "Pending")
            {
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["patientid"]), "approve");
                myStatus.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date of approval"]);
                myStatus.EndDate = myStatus.StartDate.AddDays(119);
                myStatus.StatusReason = "Approved with Partial Coverage";
                myStatus.Notes = "Automatically logged for KKT cases as approved by NVS";
                if (myStatus.CurrentDosage == "400mg")
                {
                    myStatus.TabletStrength = "1 x 400mg";
                }
                else if (myStatus.CurrentDosage == "600mg")
                {
                    myStatus.TabletStrength = "1 x 400mg + 2 x 100mg";
                }
                else if (myStatus.CurrentDosage == "800mg")
                {
                    myStatus.TabletStrength = "2 x 400mg";
                }
                try
                {
                    myStatus.Approve("mwrigglesworth");
                }
                catch
                {
                    Label1.Text += " " + ds.Tables[0].Rows[i]["pin"].ToString();
                }
            }
            else if (ds.Tables[0].Rows[i]["gipapstatus"].ToString() == "Denied")
            {
                GIPAP_Objects.PatientGipapStatus myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["patientid"]), "reassess");
                myStatus.ReAssess("mwrigglesworth", "Automatically logged for KKT cases as approved by NVS");

                myStatus = new GIPAP_Objects.PatientGipapStatus(Convert.ToInt32(ds.Tables[0].Rows[i]["patientid"]), "approve");
                myStatus.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date of approval"]);
                myStatus.EndDate = myStatus.StartDate.AddDays(119);
                myStatus.StatusReason = "Approved with Partial Coverage";
                myStatus.Notes = "Automatically logged for KKT cases as approved by NVS";
                if (myStatus.CurrentDosage == "400mg")
                {
                    myStatus.TabletStrength = "1 x 400mg";
                }
                else if (myStatus.CurrentDosage == "600mg")
                {
                    myStatus.TabletStrength = "1 x 400mg + 2 x 100mg";
                }
                else if (myStatus.CurrentDosage == "800mg")
                {
                    myStatus.TabletStrength = "2 x 400mg";
                }
                try
                {
                    myStatus.Approve("mwrigglesworth");
                }
                catch
                {
                    Label1.Text += " " + ds.Tables[0].Rows[i]["pin"].ToString();
                }
            }
        }
    }
}