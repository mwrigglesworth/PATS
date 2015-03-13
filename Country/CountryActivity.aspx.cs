using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Country_CountryActivity : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.CountryReport myReport = new GIPAP_Objects.CountryReport();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
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
        myReport.GetCompleteCountryActivity(choice, sDate, eDate, sessUse.Role, rblstProgram.SelectedValue, rep);
        LabelHeader.Text = myReport.ResultSet.Tables[1].Rows.Count.ToString() + " " + headerline;
        dgResults.DataSource = myReport.ResultSet.Tables[1];
        dgResults.DataBind();
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
            LabelError.Text = "Activity Report must be run for 1 year or less.  If you are trying to calculate total patients in the program, use <A href=GIPAP.aspx?trgt=totals>Patient Totals</a>";
            return;
        }
        PanelResults.Visible = true;
        dgResults.DataSource = myReport.GetCountryActivity(choice, sDate, eDate, rblstProgram.SelectedValue);
        dgResults.DataBind();
        //dgResults.HeaderStyle.BackColor = System.Drawing.Color.Silver;
        dgResults.Items[0].BackColor = System.Drawing.Color.Yellow;
        dgResults.Items[0].Font.Bold = true;
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
