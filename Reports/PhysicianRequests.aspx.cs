using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Reports_PhysicianRequests : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            choice = Convert.ToInt32(Request.QueryString["choice"]);
        }
        catch
        {
            choice = 0;
        }

        if (!Page.IsPostBack)
        {
            DateTime dt = DateTime.Now;
            for (int i = dt.Year; i >= 2012; i--)
            {
                dropStartYear.Items.Add(i.ToString());
                dropEndYear.Items.Add(i.ToString());
            }
            dropEndDay.SelectedValue = dt.Day.ToString();
            dropEndMonth.SelectedValue = dt.Month.ToString();
            dropEndYear.SelectedValue = dt.Year.ToString();
            dt = Convert.ToDateTime("1/1/" + dt.Year.ToString());
            if (dt < Convert.ToDateTime("3/15/2012"))
            {
                dt = Convert.ToDateTime("3/15/2012");
            }
            dropStartDay.SelectedValue = dt.Day.ToString();
            dropStartMonth.SelectedValue = dt.Month.ToString();
            dropStartYear.SelectedValue = dt.Year.ToString();

            //GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
            dropCountry.DataSource = sessUse.CountryTable;// myCountry.GetCountryList(true); ;
            dropCountry.DataValueField = "CountryID";
            dropCountry.DataTextField = "CountryName";
            dropCountry.DataBind();
            dropCountry.Items.Insert(0, "Select a country");
            dropCountry.SelectedItem.Value = "0";

            dropCountry.SelectedValue = choice.ToString();
        }
    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        DateTime sDate = new DateTime();
        DateTime eDate = new DateTime();
        try
        {
            sDate = Convert.ToDateTime(dropStartMonth.SelectedValue + "/" + dropStartDay.SelectedValue + "/" + dropStartYear.SelectedValue);
        }
        catch
        {
            LabelError.Text = "Please Select a Valid Start Date.";
            return;
        }
        try
        {
            eDate = Convert.ToDateTime(dropEndMonth.SelectedValue + "/" + dropEndDay.SelectedValue + "/" + dropEndYear.SelectedValue);
        }
        catch
        {
            LabelError.Text = "Please Select a Valid End Date.";
            return;
        }
        if (sDate > eDate)
        {
            LabelError.Text = "Start Date is greater than the End Date.";
            return;
        }
        if (sDate < Convert.ToDateTime("3/15/2012"))
        {
            sDate = Convert.ToDateTime("3/15/2012");
        }
        PanelGIPAPTotals.Visible = true;
        DataTable dt = sessUse.AEPhysicianRequest(sDate, eDate, rblstProgram.SelectedValue, cbNote.Checked, Convert.ToInt32(dropCountry.SelectedValue));
        DataView dv = dt.DefaultView;
        dv.Sort = "PIN, Date DESC";
        dgResults.DataSource = dv;
        dgResults.DataBind();
        LabelResultCount.Text = dt.Rows.Count.ToString() + " Results Found";
        dgResults.HeaderStyle.Font.Bold = true;
    }
}