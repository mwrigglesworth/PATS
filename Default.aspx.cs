using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    string rDirect = "";

    //******************************************************************************************************************//
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GIPAP_Objects.User sessUse = (GIPAP_Objects.User)Session["sessUse"];
            /*if (sessUse.Role != "" && sessUse.Role != "MYPAP" && sessUse.Role != "Novartis" && !sessUse.Role.StartsWith("FC"))
            {
                //rDirect = sessUse.HomePage;
            }*/
        }
        catch
        {
            rDirect = "";
        }
        if (rDirect != "")
        {
            //Response.Redirect(rDirect);
        }
        string rUrl = Request.Url.ToString();
        if ((rUrl.StartsWith("http://") || rUrl.StartsWith("https://gipap.org") || rUrl.StartsWith("https://www.gipap.org")) && !rUrl.StartsWith("http://localhost") && !rUrl.StartsWith("http://webserv"))
        {
            // Response.Redirect(rUrl.Replace("http://", "https://"));
            //Response.Redirect("https://www.maxaid.org"); /*TAKE OUT FOR TEST*/
        }
        try
        {
            //this.PlaceReturnButton();
            this.GetHeadlines();
            this.GetRecentBlogs();
        }
        catch { }
    }

    ////******************************************************************************************************************//
    //private void PlaceReturnButton()
    //{
    //    //Checks to see if you are inside the network or outside the network so that you can access PATS
    //    string rUrl = Request.Url.ToString();
    //    if (rUrl.StartsWith("http://webserv1") || rUrl.StartsWith("http://local") || rUrl.StartsWith("http://192.168.100.10"))
    //    {
    //        lblPatsImage.Text = "<a href=\"http://192.168.100.6\"><img src=\"images/ReturnTMF.png\" alt=\"Return to The Max Foundation\" /></a>";
    //    }
    //    else
    //    {
    //        lblPatsImage.Text = "<a href=\"http://www.themaxfoundation.org\"><img src=\"images/ReturnTMF.png\" alt=\"Return to The Max Foundation\" /></a>";
    //    }
    //}

    //******************************************************************************************************************//
    //private void GetHeadlines()
    //{
    //    News myNews = new News("Featured");
    //    if (myNews.dsNews.Tables[0].Rows.Count > 0)
    //    {
    //        //***** THIS IS FOR 2 FEATURED STORIES ACCROSS
    //        string headline_string = "<div class=\"grid_6 alpha\">";
    //        for (int i = 0; i < myNews.dsNews.Tables[0].Rows.Count; i++)
    //        {
    //            headline_string += "<a href=\"http://www.themaxfoundation.org/news/FullStory.aspx?choice=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\">";

    //            headline_string += "<img src=\"news/images/" + myNews.dsNews.Tables[0].Rows[i]["IMAGEPATH"].ToString() + "\" border=\"0\" style=\"width: 440px;\" ";
    //            headline_string += "alt=\"" + myNews.dsNews.Tables[0].Rows[i]["HEADING"].ToString() + "\" /></a>";
    //            headline_string += "<p>" + myNews.dsNews.Tables[0].Rows[i]["HEADING"].ToString() + "</p>";
    //            headline_string += "<p><a href=\"blog/FullStory.aspx?storyid=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\"><i>READ MORE</i></a></p></div>";
    //            if (i == 0)
    //            {
    //                headline_string += "<div class=\"grid_6 omega\">";
    //            }
    //        }
    //        featured_news.InnerHtml = headline_string.ToString();


    //        //***** THIS IS FOR 4 FEATURED STORIES ACCROSS
    //        string headline_string = "<div class=\"grid_3 alpha\">";
    //        for (int i = 0; i < myNews.dsNews.Tables[0].Rows.Count; i++)
    //        {
    //            headline_string += "<a href=\"http://www.themaxfoundation.org/news/FullStory.aspx?storyid=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\" target=\"_blank\">";
    //            headline_string += "<img src=\"news/images/" + myNews.dsNews.Tables[0].Rows[i]["IMAGEPATH"].ToString() + "\" border=\"0\" style=\"width: 200px;\" ";
    //            headline_string += "alt=\"" + myNews.dsNews.Tables[0].Rows[i]["HEADING"].ToString() + "\" /></a><br />";
    //            headline_string += "<p>" + myNews.dsNews.Tables[0].Rows[i]["HEADING"].ToString() + "</p>";
    //            headline_string += "<p><a href=\"blog/FullStory.aspx?storyid=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\"><i>READ MORE</i></a></p></div>";
    //            if (i == 2)
    //            {
    //                headline_string += "<div class=\"grid_3 omega\">";
    //            }
    //            else if (i == 0 || i == 1)
    //            {
    //                headline_string += "<div class=\"grid_3\">";
    //            }
    //        }
    //        featured_news.InnerHtml = headline_string.ToString();
    //    }
    //    else
    //    {
    //        featured_news.InnerHtml = "<p>No featured posts to display.</p>";
    //    }
    //}

    //******************************************************************************************************************//
    private void GetHeadlines()
    {
        News numberDisplayed = new News();
        numberDisplayed.GetProjectDisplayNumber();
        if ((int)numberDisplayed.dsNews.Tables[0].Rows[0]["NUMBEROFPROJECTSMAXAID"] == 2)
        {
            News myNews = new News("Project", 2);
            //***** THIS IS FOR 2 FEATURED STORIES ACCROSS
            string headline_string = "<div class=\"grid_6 alpha\">";
            for (int i = 0; i < myNews.dsNews.Tables[0].Rows.Count; i++)
            {
                headline_string += "<a href=\"http://www.themaxfoundation.org/news/FullStory.aspx?storyid=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\" target=\"_blank\">";

                headline_string += "<img src=\"news/images/" + myNews.dsNews.Tables[0].Rows[i]["IMAGEPATH"].ToString() + "\" border=\"0\" style=\"width: 440px;\" ";
                headline_string += "alt=\"" + myNews.dsNews.Tables[0].Rows[i]["HEADING"].ToString() + "\" /></a>";
                headline_string += "<p>" + myNews.dsNews.Tables[0].Rows[i]["HEADING"].ToString() + "</p>";
                headline_string += "<p><a href=\"http://www.themaxfoundation.org/news/FullStory.aspx?storyid=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\" target=\"_blank\">Read More >></a></p></div>";
                if (i == 0)
                {
                    headline_string += "<div class=\"grid_6 omega\">";
                }
            }
            featured_news.InnerHtml = headline_string.ToString();
        }
        else
        {
            News myNews = new News("Project", 4);
            //***** THIS IS FOR 4 FEATURED STORIES ACCROSS
            string headline_string = "<div class=\"grid_3 alpha\">";
            for (int i = 0; i < myNews.dsNews.Tables[0].Rows.Count; i++)
            {
                headline_string += "<a href=\"http://www.themaxfoundation.org/news/FullStory.aspx?storyid=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\" target=\"_blank\">";
                headline_string += "<img src=\"news/images/" + myNews.dsNews.Tables[0].Rows[i]["IMAGEPATH"].ToString() + "\" border=\"0\" style=\"width: 200px;\" ";
                headline_string += "alt=\"" + myNews.dsNews.Tables[0].Rows[i]["HEADING"].ToString() + "\" /></a><br />";
                headline_string += "<p>" + myNews.dsNews.Tables[0].Rows[i]["HEADING"].ToString() + "</p>";
                headline_string += "<p><a href=\"http://www.themaxfoundation.org/news/FullStory.aspx?storyid=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\" target=\"_blank\">Read More >></a></p></div>";
                if (i == 2)
                {
                    headline_string += "<div class=\"grid_3 omega\">";
                }
                else if (i == 0 || i == 1)
                {
                    headline_string += "<div class=\"grid_3\">";
                }
            }
            featured_news.InnerHtml = headline_string.ToString();
        }
    }


    //******************************************************************************************************************//
    private void GetRecentBlogs()
    {
        News myNews = new News("Blog");
        if (myNews.dsNews.Tables[0].Rows.Count > 0)
        {
            string blog_string = "";
            for (int i = 0; i < myNews.dsNews.Tables[0].Rows.Count; i++)
            {
                //CraigUser myUser = new CraigUser(int.Parse(myNews.dsNews.Tables[0].Rows[i]["USERID"].ToString()));
                blog_string += "<div class=\"post\">";
                blog_string += "<a href=\"http://www.themaxfoundation.org/blog/FullStory.aspx?storyid=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\" target=\"_blank\">";
                blog_string += "<img class=\"newsimagelists\" src=\"news/images/" + myNews.dsNews.Tables[0].Rows[i]["IMAGEPATH"].ToString() + "\" alt=\"\" /></a>";
                blog_string += "<h4>" + myNews.dsNews.Tables[0].Rows[i]["HEADING"].ToString() + "</h4>";
                blog_string += myNews.dsNews.Tables[0].Rows[i]["SUMMARY"].ToString() + "<p><a href=\"http://www.themaxfoundation.org/news/FullStory.aspx?storyid=" + myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString() + "\" target=\"_blank\">Read More >></a></p>";
                int commentCount = myNews.GetNewsCommentsCount(int.Parse(myNews.dsNews.Tables[0].Rows[i]["STORYID"].ToString()));

                blog_string += "<p class=\"details\">Posted by " + myNews.dsNews.Tables[0].Rows[i]["NAME"].ToString() + " on " + Convert.ToDateTime(myNews.dsNews.Tables[0].Rows[i]["STORYDATE"].ToString()).ToString("MMMM dd, yyyy") + " | Comments: " + commentCount;
                blog_string += "</p></div><hr />";
            }

            recent_blogs.InnerHtml = blog_string.ToString();
        }
        else
        {
            recent_blogs.InnerHtml = "<p>No blog posts to display.</p>";
        }
    }


    protected void ButtonLogin_Click(object sender, ImageClickEventArgs e)
    {
        if (txtUsername.Text != "")
        {
            GIPAP_Objects.User myUser = new GIPAP_Objects.User();
            myUser.Username = txtUsername.Text;
            myUser.Password = txtPassword.Text;
            Session["sessUse"] = myUser.Login(Request.Browser.Platform.ToString(), Request.UserHostAddress.ToString());
            Session.Timeout = 240;
            if (myUser.ErrorMessage == "")
            {
                if (myUser.TIPAPPhys)
                {
                    Response.Redirect("TIPAPPhysician.aspx");
                }
                Response.Redirect(myUser.HomePage);
            }
            else
            {
                LabelError.Text = myUser.ErrorMessage;
            }
        }
    }
}
