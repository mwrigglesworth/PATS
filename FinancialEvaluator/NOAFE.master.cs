using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FinancialEvaluator_NOAFE : System.Web.UI.MasterPage
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.FCOffice myOffice = new GIPAP_Objects.FCOffice();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);

        myOffice = new GIPAP_Objects.FCOffice(choice);
        LabelFEHeader.Text = myOffice.fcHeader();
        LabelFEInfo.Text = myOffice.fcInfo();
    }
}
