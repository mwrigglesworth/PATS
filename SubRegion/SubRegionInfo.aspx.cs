using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class SubRegion_SubRegionInfo : System.Web.UI.Page
{
    GIPAP_Objects.SubRegion mySub = new GIPAP_Objects.SubRegion();
    int choice;
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        mySub.SubRegionID = choice;
        if (!Page.IsPostBack)
        {
            mySub.InflatePersonel(sessUse.Role);

            LabelSRCC.Text = mySub.SRCC;
            LabelRegion.Text = mySub.RegionInfo();
        }
    }
    //**********************************************************************************************************************
    private void FillCheckBox(DataTable dt1, DataTable dt2, System.Web.UI.WebControls.CheckBoxList cbFill, string textF, string valueF)
    {
        cbFill.DataSource = dt1;
        cbFill.DataTextField = textF;
        cbFill.DataValueField = valueF;
        cbFill.DataBind();
        if (dt2.Rows.Count > 0)
        {
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                for (int i2 = 0; i2 < cbFill.Items.Count; i2++)
                {
                    string mw = dt2.Rows[i][valueF].ToString();
                    if (cbFill.Items[i2].Value == mw)
                    {
                        cbFill.Items[i2].Selected = true;
                    }
                }
            }
        }

    }
    protected void lbSRCC_Click(object sender, EventArgs e)
    {
        PanelDisplay.Visible = false;
        PanelUpdate.Visible = true;
        GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove(choice, "SubRegion", "SRCC", sessUse.Role);
        this.FillCheckBox(myAr.AddRemoveDS.Tables[0], myAr.AddRemoveDS.Tables[1], cblstPersonel, "personname", "personid");
    }
    protected void ButtonUpdateSRCC_Click(object sender, EventArgs e)
    {
        GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
        myAr.Update(cblstPersonel, sessUse.Username, sessUse.Role, choice, "SubRegion", "SRCC");
        mySub.SubRegionID = choice;
        mySub.InflatePersonel(sessUse.Role);
        LabelSRCC.Text = mySub.SRCC;
        LabelRegion.Text = mySub.RegionInfo();
        PanelDisplay.Visible = true;
        PanelUpdate.Visible = false;
    }
}
