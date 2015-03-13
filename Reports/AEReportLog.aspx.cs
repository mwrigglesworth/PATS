using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Reports_AEReportLog : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        dgGipapTotals.Font.Size = FontUnit.Point(8);
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
            if (dt < Convert.ToDateTime("1/1/2012"))
            {
                dt = Convert.ToDateTime("1/1/2012");
            }
            dropStartDay.SelectedValue = dt.Day.ToString();
            dropStartMonth.SelectedValue = dt.Month.ToString();
            dropStartYear.SelectedValue = dt.Year.ToString();

            if (!Page.IsPostBack)
            {

                dropCountry.DataSource = sessUse.CountryTable;
                dropCountry.DataTextField = "countryname";
                dropCountry.DataValueField = "countryid";
                dropCountry.DataBind();
                dropCountry.Items.Insert(0, "All Countries");
                dropCountry.Items[0].Value = "0";
            }
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
        if (sDate < Convert.ToDateTime("1/1/2012"))
        {
            sDate = Convert.ToDateTime("1/1/2012");
        }
        PanelGIPAPTotals.Visible = true;
        DataSet ds = sessUse.SAEReport(sDate, eDate, rblstProgram.SelectedValue, Convert.ToInt32(dropCountry.SelectedValue));
        dgGipapTotals.DataSource = ds;
        dgGipapTotals.DataBind();
        LabelResultCount.Text = ds.Tables[0].Rows.Count.ToString() + " Results Found";
    }

    protected void dgGipapToals_Sorting(object sender, GridViewSortEventArgs e)
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
        if (sDate < Convert.ToDateTime("1/1/2012"))
        {
            sDate = Convert.ToDateTime("1/1/2012");
        }
        PanelGIPAPTotals.Visible = true;
        DataSet ds = sessUse.SAEReport(sDate, eDate, rblstProgram.SelectedValue, Convert.ToInt32(dropCountry.SelectedValue));
        DataView dv = ds.Tables[0].DefaultView;
        dv.Sort = e.SortExpression;
        dgGipapTotals.DataSource = dv;
        dgGipapTotals.DataBind();
    }
}