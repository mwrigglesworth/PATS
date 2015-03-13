using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Patient_PrintEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GIPAP_Objects.Email myEmail = new GIPAP_Objects.Email(Convert.ToInt32(Request.QueryString["choice"]), Request.QueryString["Etype"].ToString(), Request.QueryString["Prog"].ToString());
        LabelEmail.Text = "<b>From: </b>" + myEmail.From + "<br>";
        LabelEmail.Text += "<b>To: </b>" + myEmail.To + "<br>";
        LabelEmail.Text += "<b>CC: </b>" + myEmail.CC + "<br><br>";
        LabelEmail.Text += "<b>Subject: </b>" + myEmail.Subject + "<br><br>";
        LabelEmail.Text += "<b>Message:</b><br>" + myEmail.Message.Replace("\n", "<br>");
    }
}
