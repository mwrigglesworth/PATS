using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_ActivityReport : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.CountryReport myReport = new GIPAP_Objects.CountryReport();
    int rid;
    int srid;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        if (!Page.IsPostBack)
        {
            this.FillYearComboBox();
            DateTime dt = DateTime.Now;
            dropEndDay.SelectedValue = dt.Day.ToString();
            dropEndMonth.SelectedValue = dt.Month.ToString();
            dropEndYear.SelectedValue = dt.Year.ToString();
            dt = Convert.ToDateTime("1/1/" + dt.Year.ToString());
            dropStartDay.SelectedValue = dt.Day.ToString();
            dropStartMonth.SelectedValue = dt.Month.ToString();
            dropStartYear.SelectedValue = dt.Year.ToString();

            dropCountry.DataSource = sessUse.CountryTable;
            dropCountry.DataTextField = "countryname";
            dropCountry.DataValueField = "countryid";
            dropCountry.DataBind();
            dropCountry.Items.Insert(0, "All Countries");
            dropCountry.Items[0].Value = "0";
        }
        try
        {
            rid = Convert.ToInt32(Request.QueryString["rid"]);
        }
        catch
        {
            rid = 0;
        }
        try
        {
            srid = Convert.ToInt32(Request.QueryString["srid"]);
        }
        catch
        {
            srid = 0;
        }
    }
    //**************************************************************************************************************
    private void FillYearComboBox()
    {
        DateTime dtNow = DateTime.Now;

        for (int i = dtNow.Year; i >= 2002; i--)
        {
            dropStartYear.Items.Add(i.ToString());
            dropEndYear.Items.Add(i.ToString());
        }
    }
    protected void ButtonActivity_Click(object sender, EventArgs e)
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
        if (sDate.AddYears(1) < eDate)
        {
            LabelError.Text = "Activity Report must be run for 1 year or less.  If you are trying to calculate total patients in the program, use <A href=TotalsReport.aspx>Patient Totals</a>";
            return;
        }
        PanelResults.Visible = true;
        if (dropCountry.SelectedIndex == 0)
        {
            dgResults.DataSource = sessUse.GIPAPActivity(sDate, eDate, rid, srid, rblstProgram.SelectedValue);
            dgResults.DataBind();
        }
        else
        {
            dgResults.DataSource = myReport.GetCountryActivity(Convert.ToInt32(dropCountry.SelectedValue), sDate, eDate, rblstProgram.SelectedValue);
            dgResults.DataBind();
        }
        //dgResults.HeaderStyle.BackColor = System.Drawing.Color.Silver;
        dgResults.Items[0].BackColor = System.Drawing.Color.Yellow;
        dgResults.Items[0].Font.Bold = true;
        LabelResultCount.Text = rblstProgram.SelectedValue + " Activity";
    }
    protected void dropCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCountry.SelectedIndex == 0)
        {
            PanelComplete.Visible = false;
        }
        else
        {
            PanelComplete.Visible = true;
        }
    }
    //**********************************************************************************************************************
    private void bindGrids(string rep, string headerline)
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
        PanelResults.Visible = true;
        myReport.GetCompleteCountryActivity(Convert.ToInt32(dropCountry.SelectedValue), sDate, eDate, sessUse.Role, rblstProgram.SelectedValue, rep);
        LabelResultCount.Text = myReport.ResultSet.Tables[1].Rows.Count.ToString() + " " + headerline;
        dgResults.DataSource = myReport.ResultSet.Tables[1];
        dgResults.DataBind();
    }
    protected void lbApprovals_Click(object sender, EventArgs e)
    {
        this.bindGrids("Approve", "Approvals");
    }
    protected void lbReapprovals_Click(object sender, EventArgs e)
    {
        this.bindGrids("ReApprove", "ReApprovals");
    }
    protected void lbClosures_Click(object sender, EventArgs e)
    {
        this.bindGrids("Close", "Closures");
    }
    protected void lbDeny_Click(object sender, EventArgs e)
    {
        this.bindGrids("Deny", "Denials");
    }
    protected void lbExtend_Click(object sender, EventArgs e)
    {
        this.bindGrids("Extend", "Extensions");
    }
    protected void lbDoseChange_Click(object sender, EventArgs e)
    {
        this.bindGrids("DoseChange", "Dose Changes");
    }
}
