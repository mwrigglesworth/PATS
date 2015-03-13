using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Distributor_DistributorMaster : System.Web.UI.MasterPage
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.Distributor myDistributor = new GIPAP_Objects.Distributor();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);

        myDistributor = new GIPAP_Objects.Distributor(choice);
        LabelDistributorHeader.Text = myDistributor.DistributorHeader();
        LabelDistributorInfo.Text = myDistributor.DistributorInfo();
    }
}
