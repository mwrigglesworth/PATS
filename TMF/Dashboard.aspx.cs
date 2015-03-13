using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TMF_Dashboard : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    int choice;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        try
        {
            choice = Convert.ToInt32(Request.QueryString["choice"]);
        }
        catch
        {
            choice = 0;
        }
        if (choice != 0)
        {
            LabelName.Text = sessUse.TempName;
            if (choice == sessUse.TempID)
            {
                PanelTempHp.Visible = true;
            }
            else
            {
                lbMakeTempHp.Visible = true;
            }
        }
        else
        {
            if (sessUse.TempID != 0)
            {
                Response.Redirect(sessUse.HomePage);
            }
            else
            {
                LabelName.Text = sessUse.FullName;
            }
        }
        if (sessUse.Program == "GIPAP")
        {
            LabelProgram.Text = "<font color=steelblue size=6>GIPAP</font>";

            if (!Page.IsPostBack)
            {
                dropOtherUsers.DataSource = sessUse.OtherUsers;
                dropOtherUsers.DataTextField = "personname";
                dropOtherUsers.DataValueField = "userid";
                dropOtherUsers.DataBind();
                dropOtherUsers.Items.Insert(0, "Select another user's workload to view");
            }

            QueMypap.Text += sessUse.getMYPAPQueues(choice);
            QuePinc.Text += sessUse.getPINCQueues(choice);
            if (QueMypap.Text != string.Empty)
            {
                hlMYPAP.Text += "*";
                hlMYPAP.Font.Bold = true;
            }
            if (QuePinc.Text != string.Empty)
            {
                hlPINC.Text += "*";
                hlPINC.Font.Bold = true;
            }
        }
        else if (sessUse.Program == "PINC")
        {
            LabelProgram.Text = "<font color=deeppink size=6>PINC</font>";

            PS_Objects.User psUse = new PS_Objects.User();
            psUse.TrustedLogin(sessUse.Username);
            if (!Page.IsPostBack)
            {
                dropOtherUsers.DataSource = psUse.OtherUserDT;
                dropOtherUsers.DataTextField = "uname";
                dropOtherUsers.DataValueField = "userid";
                dropOtherUsers.DataBind();
                dropOtherUsers.Items.Insert(0, "Select another user's workload to view");
            }
        }
       hlMYPAP.NavigateUrl = "../MYPAP/gipaptrusted.aspx?reqform=MyPap&user=" + sessUse.Username;
        hlTIPAP.NavigateUrl = "../TIPAP/gipaptrusted.aspx?reqform=TiPap&user=" + sessUse.Username;
    }
    protected void lbMakeTempHp_Click(object sender, EventArgs e)
    {
        sessUse.TempID = choice;
        sessUse.HomePage = "../TMF/Dashboard.aspx?choice=" + choice.ToString();
        Session["sessUse"] = sessUse;
        Response.Redirect(sessUse.HomePage);
    }
    protected void lbMyHomepage_Click(object sender, EventArgs e)
    {
        sessUse.TempID = 0;
        sessUse.HomePage = "../TMF/Dashboard.aspx";
        Session["sessUse"] = sessUse;
        Response.Redirect(sessUse.HomePage);
    }
    protected void dropOtherUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropOtherUsers.SelectedIndex != 0)
        {
            sessUse.TempName = dropOtherUsers.SelectedItem.Text;
            Session["sessUse"] = sessUse;
            Response.Redirect("Dashboard.aspx?choice=" + dropOtherUsers.SelectedValue);
        }
    }
}
