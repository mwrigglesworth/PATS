using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_MOU : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GIPAP_Objects.User sessUse = (GIPAP_Objects.User)Session["sessUse"];

        LabelMOU.Text = sessUse.MOUReport();

        /*PanelGIPAPTotals.Visible = true;
        dgResults.DataSource = sessUse.MOUStatusReport().Tables[4];
        dgResults.DataBind();
        dgResults.HeaderStyle.BackColor = System.Drawing.Color.Silver;
        dgResults.Items[0].BackColor = System.Drawing.Color.Yellow;
        dgResults.Items[0].Font.Bold = true;*/
    }
}