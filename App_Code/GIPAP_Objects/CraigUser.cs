using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for CraigUser
/// </summary>
public class CraigUser
{
    private int mUserID;
    private string mUsername;
    private string mPassword;
    private string mName;
    private string mPhoto;
    private string mBio;

    public DataSet dsUser;

    string connTMF_DB = ConfigurationManager.AppSettings["connTMF_DB"];

	public CraigUser()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //**********************************************************************************************************************
    public CraigUser(int userid)
	{
		if(userid == 0)
		{
			return;
		}
		else
		{
			DataSet myData = new DataSet();
			SqlParameter paramUserID = new SqlParameter("@UserID", SqlDbType.Int);
            paramUserID.Value = userid;

            myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_GetUser", paramUserID);

			if (myData.Tables[0].Rows.Count <= 0)
			{
				return;
			}
			else
			{
                this.dsUser = myData;
			}
			myData.Dispose();
		}
	}

    //*************************************************************************************************************************
    private void Inflate(DataSet ds)
    {
        this.mUserID = (int)(ds.Tables[0].Rows[0]["USERID"]);
        this.mUsername = (ds.Tables[0].Rows[0]["USERNAME"]).ToString();
        this.mName = (ds.Tables[0].Rows[0]["NAME"]).ToString();
        this.mPhoto = (ds.Tables[0].Rows[0]["PHOTO"]).ToString();
    }

    //**********************************************************************************************************************
    public void CheckCredentials(string username)
    {
        DataSet myData = new DataSet();
        SqlParameter[] arrParams = new SqlParameter[3];

        arrParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
        arrParams[0].Direction = ParameterDirection.Output;

        arrParams[1] = new SqlParameter("@Username", SqlDbType.NVarChar, 50);
        arrParams[1].Value = username;

        arrParams[2] = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
        arrParams[2].Direction = ParameterDirection.Output;

        myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_VerifyUser", arrParams);

        this.UserID = (int)arrParams[0].Value;
        this.Password = (string)arrParams[2].Value;
    }

    //**********************************************************************************************************************
    public void GetUserBios()
    {
        DataSet myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_GetBloggerBios");
        this.dsUser = myData;
        myData.Dispose();
    }

    //**********************************************************************************************************************
    public DataSet GetAuthorNames()
    {
        DataSet myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_GetAuthorNames");
        this.dsUser = myData;
        myData.Dispose();
        return dsUser;        
    }

	//**********************************************************************************************************************
	public int UserID
	{
		get{return mUserID;}
		set{mUserID = value;}
	}

	//**********************************************************************************************************************
	public string Username
	{
		get{return mUsername;}
		set{mUsername = value;}
	}
	
	//**********************************************************************************************************************
	public string Password
	{
		get{return mPassword;}
		set{mPassword = value;}
	}

    //**********************************************************************************************************************
    public string Name
    {
        get { return mName; }
        set { mName = value; }
    }

    //**********************************************************************************************************************
    public string Photo
    {
        get { return mPhoto; }
        set { mPhoto = value; }
    }

    //**********************************************************************************************************************
    public string Bio
    {
        get { return mBio; }
        set { mBio = value; }
    }
}
