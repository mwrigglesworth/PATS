using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_FCRequest : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;
    GIPAP_Objects.FCRequest myRequest = new GIPAP_Objects.FCRequest();
    int choice;
    int repid;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        try
        {
            repid = Convert.ToInt32(Request.QueryString["repid"]);
        }
        catch
        {
            repid = 0;
        }
        myRequest = new GIPAP_Objects.FCRequest(choice, repid, 0);
        if (myRequest.ReplyID != 0)
        {
            LabelReply.Text = "<font color=gray><b>Reply to: </b><i>" + myRequest.ReplySubject + "</i></font>";
            txtSubject.Text = "RE: " + myRequest.ReplySubject;
            rblstToType.SelectedValue = myRequest.ReplyTo;
            rblstToType.Enabled = false;
        }
        if (sessUse.Role != "FCFreedomDesk")
        {
            rblstToType.SelectedIndex = 0;
            rblstToType.Enabled = false;
        }
    }


    protected void ButtonSub_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            myRequest.ToType = rblstToType.SelectedValue;
            myRequest.Subject = txtSubject.Text.Trim();
            myRequest.Message = txtMessage.Text;
            if (sessUse.Role == "FCFreedomDesk")
            {
                myRequest.FromType = "Central Hub";
            }
            else if (sessUse.Role == "FCCallCenter")
            {
                myRequest.FromType = "Call Center";
            }
            else if (sessUse.Role == "FCBranch")
            {
                myRequest.FromType = "Branch";
            }
            else if (sessUse.Role == "MaxStation" || sessUse.Role == "TMFUser")
            {
                myRequest.FromType = "TMF";
            }
            myRequest.ReplyID = repid;
            myRequest.Create(sessUse.Username);
            Response.Redirect("PatientInfo.aspx?a=fcrequest&choice=" + choice.ToString());
        }
    }
}
