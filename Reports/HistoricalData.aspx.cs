using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Reports_HistoricalData : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
    }
    protected void CalendarHD_SelectionChanged(object sender, EventArgs e)
    {
        LabelDate.Text = CalendarHD.SelectedDate.Day.ToString() + " " + CalendarHD.SelectedDate.ToString("y");
        /*LabelHD.Text = sessUse.HistoricalData(CalendarHD.SelectedDate, Server);*/
        PanelGIPAPTotals.Visible = true;
        DateTime rDate = CalendarHD.SelectedDate;
        if (rDate > DateTime.Today)
        {
            LabelHD.Visible = true;
        }
        DataSet rDs = new DataSet();
        for (int i = 0; i < 1; rDate = rDate.AddDays(-1))
        {
            rDs = sessUse.GIPAPTotals(rDate);
            if (rDs.Tables[0].Rows.Count > 1)
            {
                break;
            }
        }
        dgResults.DataSource = rDs;
        dgResults.DataBind();
        dgResults.HeaderStyle.BackColor = System.Drawing.Color.Silver;
        dgResults.Items[0].BackColor = System.Drawing.Color.Yellow;
        dgResults.Items[0].Font.Bold = true;
    }
}