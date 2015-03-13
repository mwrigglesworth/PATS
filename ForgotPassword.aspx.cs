using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ForgotPassword : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    //******************************************************************************************************************//    
    protected void ButtonEmail_Click(object sender, EventArgs e)
    {
        try
        {
            sessUse.EmailPassword(txtUsername.Text, txtEmail.Text);
            PanelEmail.Visible = false;
            PanelThanks.Visible = true;
        }
        catch (Exception ex)
        {
            LabelError.Text = ex.Message.ToString();
        }
    }
}
