using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stockist_NOAStockist : System.Web.UI.MasterPage
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Stockist myStockist = new GIPAP_Objects.Stockist();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);

        myStockist = new GIPAP_Objects.Stockist(choice);
        LabelStockistHeader.Text = myStockist.StockistHeader();
        LabelStockistInfo.Text = myStockist.StockistInfo();
    }
}
