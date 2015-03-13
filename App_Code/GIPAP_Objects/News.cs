using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;


public class News
{
    private int mStoryID;
    private string mHeading;
    private string mSummary;
    private string mStory;
    private string mImage;
    private string mImageCaption;
    private string mStoryType;
    private DateTime mStoryDate;
    private bool mArchive;
    private int mUserID;

    public DataSet dsNews;

    string connTMF_DB = ConfigurationManager.AppSettings["connTMF_DB"];

    //**********************************************************************************************************************
    public News()
    {
       
    }

    //**********************************************************************************************************************
    public News(string newstype, int displaynumber)
    {
        if (displaynumber == 2)
        {
            DataSet myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_GetFeaturedMAXAID2");
            this.dsNews = myData;
            myData.Dispose();
        }
        else
        {
            DataSet myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_GetFeaturedMAXAID4");
            this.dsNews = myData;
            myData.Dispose();
        }
    }


    //**********************************************************************************************************************
    public News(string newstype)
    {
        if (newstype == "Featured")
        {
            DataSet myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_GetFeaturedMAXAID");
            this.dsNews = myData;
            myData.Dispose();
        }

        else if (newstype == "Blog")
        {
            DataSet myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_GetBlogs");
            this.dsNews = myData;
            myData.Dispose();
        }
    }   

    //**********************************************************************************************************************
    public int GetNewsCommentsCount(int storyid)
    {
        if (storyid == 0)
        {
            return 0;
        }
        else
        {
            DataSet myData = new DataSet();
            SqlParameter paramStoryID = new SqlParameter("@StoryID", SqlDbType.Int);
            paramStoryID.Value = storyid;

            myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_GetNewsComments", paramStoryID);

            if (myData.Tables[0].Rows.Count <= 0)
            {
                return 0;
            }
            else
            {
                return myData.Tables[0].Rows.Count;
            }
        }
    }

    //**********************************************************************************************************************
    public DataSet GetProjectDisplayNumber()
    {
        DataSet myData = SqlHelper.ExecuteDataset(connTMF_DB, CommandType.StoredProcedure, "spr_GetProjectDisplayNumber");
        this.dsNews = myData;
        myData.Dispose();
        return dsNews;
    }


    //**********************************************************************************************************************
    public int StoryID
    {
        get { return mStoryID; }
        set { mStoryID = value; }
    }

    //**********************************************************************************************************************
    public string Heading
    {
        get { return mHeading; }
        set { mHeading = value; }
    }

    //**********************************************************************************************************************
    public string StoryType
    {
        get { return mStoryType; }
        set { mStoryType = value; }
    }

    //**********************************************************************************************************************
    public DateTime StoryDate
    {
        get { return mStoryDate; }
        set { mStoryDate = value; }
    }

    //**********************************************************************************************************************
    public string Summary
    {
        get { return mSummary; }
        set { mSummary = value; }
    }

    //**********************************************************************************************************************
    public string Image
    {
        get { return mImage; }
        set { mImage = value; }
    }

    //**********************************************************************************************************************
    public string ImageCaption
    {
        get { return mImageCaption; }
        set { mImageCaption = value; }
    }

    //**********************************************************************************************************************
    public string Story
    {
        get { return mStory; }
        set { mStory = value; }
    }

    //**********************************************************************************************************************
    public bool Archive
    {
        get { return mArchive; }
        set { mArchive = value; }
    }

    //**********************************************************************************************************************
    public int UserID
    {
        get { return mUserID; }
        set { mUserID = value; }
    }
}
