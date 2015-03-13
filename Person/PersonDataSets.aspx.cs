using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Person_PersonDataSets : System.Web.UI.Page
{
    GIPAP_Objects.Person myPerson = new GIPAP_Objects.Person();
    GIPAP_Objects.User sessUse;
    int choice;
    string dset;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        dset = Request.QueryString["dset"];
        myPerson.PersonID = choice;
        dgResults.DataSource = myPerson.getPersonDatasets(dset);
        dgResults.DataBind();
        LabelReportTitle.Text = dgResults.Items.Count.ToString() + " Results Found";
        LabelLinks.Text = "<a href=PersonInfo.aspx?choice=" + choice.ToString() + ">Control Panel</a><br><br>";
    }
}
