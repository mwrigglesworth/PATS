using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserMessage : System.Web.UI.UserControl
{
    DataSet Message;
    protected void Page_Load(object sender, EventArgs e)
    {
        GIPAP_Objects.User sessUse = (GIPAP_Objects.User)Session["sessUse"];
        
        if (sessUse.Role == "Physician" || sessUse.Role == "MaxStation")
        {
            PanelNew.Visible = true;
           
            //PanelInfo.Controls.Add(LoadControl("Profile.ascx"));
            //PanelInfo.Visible = true;

            Message = sessUse.getPhysicianMessage();
            if (Message.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in Message.Tables[0].Rows)
                {
                    LabelMessage.Text += dr["newsmessage"].ToString()+"<br>";
                }
            }
        }
        else
            LabelMessage.Text = sessUse.getUserMessage();
    }
}
