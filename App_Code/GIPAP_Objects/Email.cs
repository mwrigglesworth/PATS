using System;
using System.Web;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace GIPAP_Objects
{
	/// <summary>
	/// Summary description for Email.
	/// </summary>
	public class Email
	{
		private int mEmailID;
		private int mPatientID;
		private int mPhysicianID;
		private string mFrom;
		private string mTo;
		private string mCC;
		private string mBCC;
		private string mSubject;
		private string mMessage;
		private string mMailType;
		private int mSAEID;

		string connString = ConfigurationSettings.AppSettings["ConnectionString"];
        string connMypap = ConfigurationSettings.AppSettings["ConnMYPAP"];
        string connTipap = ConfigurationSettings.AppSettings["ConnTIPAP"];
        string connNps = ConfigurationSettings.AppSettings["ConnNPS"];
		
		//********************************************************************************
		public Email()
		{
			this.Clear();
		}

		//********************************************************************************
		public Email(int emailID, string etype,string prog)
		{
			if(emailID == 0)
			{
				return;
			}
			else
			{
				DataSet myData = new DataSet();
				SqlParameter paramemailID = new SqlParameter("@EmailID", SqlDbType.Int);
				paramemailID.Value = emailID;
				if(etype == "Patient")
				{
                    switch (prog)
                    {
                        case "GIPAP": myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetemailProfile", paramemailID); break;
                        case "MYPAP": myData = SqlHelper.ExecuteDataset(connMypap, CommandType.StoredProcedure, "spr_GetemailProfile", paramemailID); break;
                        case "TIPAP": myData = SqlHelper.ExecuteDataset(connTipap, CommandType.StoredProcedure, "spr_GetemailProfile", paramemailID); break;
                        case "NPS": myData = SqlHelper.ExecuteDataset(connNps, CommandType.StoredProcedure, "spr_GetemailProfile", paramemailID); break;
                    }
					
				}
				else
				{
					myData = SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, "spr_GetPhysicianEmailProfile", paramemailID);
				}

				if (myData.Tables[0].Rows.Count <= 0)
				{
					return;
				}
				else
				{
					Inflate(myData.Tables[0].Rows[0]);
				}
				myData.Dispose();
			}
		}


		/*//********************************************************************************
		public Email(string from, string to, string cc, string bcc, string subject, string message, string mailtype)
		{
			this.From = from;
			this.To = to;
			this.CC = cc;
			this.BCC = bcc;
			this.Subject = subject;
			this.Message = message;
			this.MailType = mailtype;
		}*/

		//********************************************************************************
		public Email(string from, string to, string cc, string bcc, string subject, string message, int patientID, int physicianID, string mailtype, int saeid)
		{
			this.From = from;
			this.To = to;
			this.CC = cc;
			this.BCC = bcc;
			this.Subject = subject;
			this.Message = message;
			this.PatientID = patientID;
			this.PhysicianID = physicianID;
			this.MailType = mailtype;
			this.SAEID = saeid;
		}

        //********************************************************************************
        public void Send(string socialworker)
        {
            this.FormatToCC();
            //Create instance of main mail message class.
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("gipap@themaxfoundation.org", "GIPAP");
            //mailMessage.To.Add(new MailAddress("jwrigg21@yahoo.com"));
            //Set additinal addresses
            string[] emailArray = this.To.Trim().Split(';');
            foreach (string email in emailArray)
            {
                if (email.Length != 0)
                {
                    mailMessage.To.Add(new MailAddress(email));
                }
            }
            this.CC = this.CC.Replace("gipap@themaxfoundation.org", "");
            if (this.CC != "")
            {
                string[] ccArray = this.CC.Trim().Split(';');
                foreach (string cc in ccArray)
                {
                    if (cc.Length != 0)
                    {
                        mailMessage.CC.Add(new MailAddress(cc));
                    }
                }
            }
            if (this.BCC != "")
            {
                string[] bccArray = this.BCC.Trim().Split(';');
                foreach (string bcc in bccArray)
                {
                    if (bcc.Length != 0)
                    {
                        mailMessage.Bcc.Add(new MailAddress(bcc)); 
                    }
                }
            }

            //mailMessage.ReplyTo;

            //Set additional options
            mailMessage.Priority = System.Net.Mail.MailPriority.Normal;
            //Text/HTML
            mailMessage.IsBodyHtml = false;

            //Set the subjet and body text
            mailMessage.Subject = this.Subject.Trim();
            mailMessage.Body = this.Message.Trim();

            //Add one to many attachments
            //mailMessage.Attachments.Add(new System.Net.Mail.Attachment("c:\temp.txt");
            
            //Create an instance of the SmtpClient class for sending the email
            SmtpClient smtpClient = new SmtpClient();

            //Use a Try/Catch block to trap sending errors
            //Especially useful when looping through multiple sends
            if (this.To.Trim() != "")
            {
                try
                {
                  //smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                if (this.PatientID != 0)
                {
                    this.CreateEmail(socialworker);
                }
                else if (this.PhysicianID != 0)
                {
                    this.CreatePhysicianEmail(socialworker);
                }
            }

        }
        //********************************************************************************
        private void FormatToCC()
        {
            this.To = this.To.Replace(" ", "");
            this.To = this.To.Replace(",", ";");
            this.To = this.To.Replace("/", ";");

            this.CC = this.CC.Replace(" ", "");
            this.CC = this.CC.Replace(",", ";");
            this.CC = this.CC.Replace("/", ";");
        }


		//*******************************************************************************
		public void SendErrorEmail(string uName, string Eurl, string eMessage, string ipaddy)
		{
			try
			{
                //Construct the email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("gipap@themaxfoundation.org");
                mailMessage.To.Add("mwrigglesworth@themaxfoundation.org");
                //mailMessage.CC.Add("aparna.bhatta@themaxfoundation.org");
                mailMessage.Subject = "**PATS ERROR**";
                mailMessage.Body = "Error encountered in the GIPAP System\n\n";
                mailMessage.Body += "User: " + uName + "\n\n";
                mailMessage.Body += "Page: " + Eurl + "\n\n";
                mailMessage.Body += "IP Address: " + ipaddy + "\n\n";
                mailMessage.Body += "Error Message: " + eMessage + "\n\n";
                mailMessage.Body += "Date: " + DateTime.Today.ToLongDateString();
                mailMessage.IsBodyHtml = false;

                //Now send the message
                SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Send(mailMessage);

				//Construct the email message
				/*MailMessage myEmail = new MailMessage();
				myEmail.From = "gipap@themaxfoundation.org";
				myEmail.To = "mwrigglesworth@themaxfoundation.org";
				myEmail.Cc = "mwrigglesworth@themaxfoundation.org";
				myEmail.Bcc = "";
				myEmail.Subject = "**PATS ERROR**";
				myEmail.Body = "Error encountered in the PATS System\n\n";
				myEmail.Body += "User: " + uName + "\n\n";
				myEmail.Body += "Page: " + Eurl + "\n\n";
				myEmail.Body += "IP Address: " + ipaddy + "\n\n";
				myEmail.Body += "Error Message: " + eMessage + "\n\n";
				myEmail.Body += "Date: " + DateTime.Today.ToLongDateString();
				myEmail.BodyFormat = MailFormat.Text;
				
				//SmtpMail.SmtpServer = "Mail.themaxfoundation.org";
                SmtpMail.SmtpServer = "mailserv2";
				
				//Now send the message
                SmtpMail.Send(myEmail);*/
			}
			catch{}
		}
		//********************************************************************************
		public void SendPasswordEmail(string uName, string pword, string address)
		{
			
                //Construct the email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("gipap@themaxfoundation.org");
                //Set additinal addresses
                string[] emailArray = address.Trim().Split(';');
                foreach (string email in emailArray)
                {
                    if (email.Length != 0)
                    {
                        mailMessage.To.Add(new MailAddress(email));
                    }
                }
                mailMessage.Subject = "PATS Login Information";
                mailMessage.Body = "Your PATS User Name and Password are set as follows:\n\n";
                mailMessage.Body += "User: " + uName + "\n\n";
                mailMessage.Body += "Password: " + pword + "\n\n";
                mailMessage.Body += "Thankyou for using PATS.  To Log On, go to http://www.maxaid.org.";
                mailMessage.IsBodyHtml = false;

                //Now send the message
                SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Send(mailMessage);

				//Construct the email message
				/*MailMessage myEmail = new MailMessage();
				myEmail.From = "gipap@themaxfoundation.org";
				myEmail.To = address;
				myEmail.Cc = address;
				myEmail.Bcc = "";
				myEmail.Subject = "PATS Login Information";
				myEmail.Body = "Your PATS User Name and Password are set as follows:\n\n";
				myEmail.Body += "User Name: " + uName + "\n\n";
				myEmail.Body += "Password: " + pword + "\n\n";
				myEmail.Body += "Thankyou for using PATS.  To Log On, go to http://www.maxaid.org.";
				myEmail.BodyFormat = MailFormat.Text;
				
				//SmtpMail.SmtpServer = "Mail.themaxfoundation.org";
                SmtpMail.SmtpServer = "mailserv2";
				
				//Now send the message
                SmtpMail.Send(myEmail);*/
		}
		
		//**********************************************************************************************************************
		public void CreateEmail(string socialworker)
		{
			
			SqlParameter[] arrParams = new SqlParameter[10];

			arrParams[0] = new SqlParameter("@PatientID", SqlDbType.Int);
			arrParams[0].Value = this.PatientID;

			arrParams[1] = new SqlParameter("@EmailTo", SqlDbType.NVarChar, 500);
			arrParams[1].Value = this.To;

			arrParams[2] = new SqlParameter("@EmailFrom", SqlDbType.NVarChar, 500);
			arrParams[2].Value = this.From;

			arrParams[3] = new SqlParameter("@EmailCC", SqlDbType.NVarChar, 500);
			arrParams[3].Value = this.CC;

            arrParams[4] = new SqlParameter("@EmailBCC", SqlDbType.NVarChar, 500);
            arrParams[4].Value = this.BCC;

			arrParams[5] = new SqlParameter("@EmailSubject", SqlDbType.NVarChar, 500);
			arrParams[5].Value = this.Subject;

			arrParams[6] = new SqlParameter("@EmailBody", SqlDbType.Text);
			arrParams[6].Value = this.Message;

			arrParams[7] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[7].Value = socialworker;

			arrParams[8] = new SqlParameter("@MailType", SqlDbType.NVarChar, 50);
			arrParams[8].Value = this.MailType;

			arrParams[9] = new SqlParameter("@EmailID", SqlDbType.Int);
            arrParams[9].Direction = ParameterDirection.Output;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreateEmail", arrParams);

            this.EmailID = (int)arrParams[9].Value;
								
		}
		//**********************************************************************************************************************
		public void CreatePhysicianEmail(string socialworker)
		{
			
			SqlParameter[] arrParams = new SqlParameter[9];

			arrParams[0] = new SqlParameter("@PhysicianID", SqlDbType.Int);
			arrParams[0].Value = this.PhysicianID;

			arrParams[1] = new SqlParameter("@EmailTo", SqlDbType.NVarChar, 500);
			arrParams[1].Value = this.To;

			arrParams[2] = new SqlParameter("@EmailFrom", SqlDbType.NVarChar, 500);
			arrParams[2].Value = this.From;

			arrParams[3] = new SqlParameter("@EmailCC", SqlDbType.NVarChar, 500);
			arrParams[3].Value = this.CC;

            arrParams[4] = new SqlParameter("@EmailBCC", SqlDbType.NVarChar, 500);
            arrParams[4].Value = this.BCC;

			arrParams[5] = new SqlParameter("@EmailSubject", SqlDbType.NVarChar, 500);
			arrParams[5].Value = this.Subject;

			arrParams[6] = new SqlParameter("@EmailBody", SqlDbType.Text);
			arrParams[6].Value = this.Message;

			arrParams[7] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 20);
			arrParams[7].Value = socialworker;

			arrParams[8] = new SqlParameter("@MailType", SqlDbType.NVarChar, 50);
			arrParams[8].Value = this.MailType;

			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, "spr_CreatePhysicianEmail", arrParams);
								
		}
		//**********************************************************************************************************************
		private void Inflate(DataRow row)
		{
			this.EmailID = Convert.ToInt32(row["EmailID"]);
			try
			{
				this.PatientID = Convert.ToInt32(row["PatientID"]);
			}
			catch
			{
				this.PatientID = 0;
			}
			try
			{
				this.PhysicianID = Convert.ToInt32(row["PhysicianID"]);
			}
			catch
			{
				this.PhysicianID = 0;
			}
			this.From = Convert.ToString(row["EmailFrom"]);
			this.To = Convert.ToString(row["EmailTo"]);
			this.CC = Convert.ToString(row["EmailCC"]);
			this.BCC = Convert.ToString(row["EmailBCC"]);
			this.Subject = Convert.ToString(row["EmailSubject"]);
			this.Message = Convert.ToString(row["EmailBody"]);
		}
		//**********************************************************************************************************************
		private void Clear()
		{
			this.EmailID = 0;
			this.PatientID = 0;
			this.PhysicianID = 0;
			this.From = "";
			this.To = "";
			this.CC = "";
			this.BCC = "";
			this.Subject = "";
			this.Message = "";
			this.MailType = "";
			this.SAEID = 0;
		}
		//********************************************************************************
		public int EmailID
		{
			get{return mEmailID;}
			set{mEmailID = value;}
		}
		//********************************************************************************
		public int PatientID
		{
			get{return mPatientID;}
			set{mPatientID = value;}
		}
		//********************************************************************************
		public int PhysicianID
		{
			get{return mPhysicianID;}
			set{mPhysicianID = value;}
		}
		//********************************************************************************
		public string From
		{
			get{return mFrom;}
			set{mFrom = value;}
		}

		//********************************************************************************
		public string To
		{
			get{return mTo;}
			set{mTo = value;}
		}

		//********************************************************************************
		public string CC
		{
			get{return mCC;}
			set{mCC = value;}
		}

		//********************************************************************************
		public string BCC
		{
			get{return mBCC;}
			set{mBCC = value;}
		}

		//********************************************************************************
		public string Subject
		{
			get{return mSubject;}
			set{mSubject = value;}
		}

		//********************************************************************************
		public string Message
		{
			get{return mMessage;}
			set{mMessage = value;}
		}
		//********************************************************************************
		public string MailType
		{
			get{return mMailType;}
			set{mMailType = value;}
		}
		//********************************************************************************
		public int SAEID
		{
			get{return mSAEID;}
			set{mSAEID = value;}
		}
	}
}
