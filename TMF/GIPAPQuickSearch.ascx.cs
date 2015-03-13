using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TMF_GIPAPQuickSearch : System.Web.UI.UserControl
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
        {
            cbNoa.Visible = true;
        }
        txtPIN.Attributes.Add("onfocus", "this.value=''");
        txtFirstName.Attributes.Add("onfocus", "this.value=''");
        txtLastName.Attributes.Add("onfocus", "this.value=''");
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        if (txtPIN.Text.Trim() == "PIN")
        {
            txtPIN.Text = "";
        }
        if (txtFirstName.Text.Trim() == "First")
        {
            txtFirstName.Text = "";
        }
        if (txtLastName.Text.Trim() == "Last")
        {
            txtLastName.Text = "";
        }
        if (txtPIN.Text.Trim() != "" || txtFirstName.Text.Trim() != "" || txtLastName.Text.Trim() != "" || cbNoa.Checked)
        {
            Response.Redirect("../TMF/QuickSearch.aspx?pin=" + txtPIN.Text.Trim() + "&first=" + txtFirstName.Text.Trim() + "&last=" + txtLastName.Text.Trim() + "&noa=" + cbNoa.Checked.ToString());
        }
    }
}
