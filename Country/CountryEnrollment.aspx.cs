using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Country_CountryEnrollment : System.Web.UI.Page
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
            dropMonth.SelectedValue = dt.Month.ToString();
            dropYear.SelectedValue = dt.Year.ToString();
        }
    }
    //**************************************************************************************************************
    private void FillYearComboBox()
    {
        DateTime dtNow = DateTime.Now;

        for (int i = dtNow.Year; i >= 2002; i--)
        {
            dropYear.Items.Add(i.ToString());
        }
    }
    protected void ButtonActivity_Click(object sender, EventArgs e)
    {
        PanelResults.Visible = true;
        LabelHeader.Text = "Enrolled";
        dgResults.DataSource = myReport.GetCountryEnrollment(choice, Convert.ToInt32(dropMonth.SelectedValue), Convert.ToInt32(dropYear.SelectedValue), rblstProgram.Items[1].Selected);
        dgResults.DataBind();
        //dgResults.HeaderStyle.BackColor = System.Drawing.Color.Silver;
        dgResults.Items[0].BackColor = System.Drawing.Color.Yellow;
        dgResults.Items[0].Font.Bold = true;
    }
}
