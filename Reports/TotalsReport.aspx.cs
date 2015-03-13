using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_TotalsReport : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
    }
    protected void ButtonActivity_Click(object sender, EventArgs e)
    {
        PanelResults.Visible = true;
        dgResults.DataSource = sessUse.GIPAPTotals(rblstProgram.SelectedValue);
        dgResults.DataBind();
        //dgResults.HeaderStyle.BackColor = System.Drawing.Color.Silver;
        dgResults.Items[0].BackColor = System.Drawing.Color.Yellow;
        dgResults.Items[0].Font.Bold = true;
        LabelResultCount.Text = rblstProgram.SelectedValue + " Totals";
    }
}
