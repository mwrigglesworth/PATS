using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Physician_ChangePassword : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sessUse = (GIPAP_Objects.User)Session["sessUse"];
        }
        catch
        {
            Response.Redirect(sessUse.LogOut((GIPAP_Objects.User)Session["sessUse"]));
        }
        if (!Page.IsPostBack)
        {
            txtPassword.Attributes.Add("onkeyup", "passwordStrength(this.value)");
        }
        if (sessUse.Role.StartsWith("FC") && sessUse.ExpireDate >= DateTime.Today)
        {
            LabelExpire.Text = "Your password is set to expire on " + sessUse.ExpireDate.Day.ToString() + " " + sessUse.ExpireDate.ToString("y") + ".  Please update your password so you can continue to log in to PATS.";
            sessUse.HomePage = "FinancialEvaluator/Dashboard.aspx";
            Session["sessUse"] = sessUse;
        }
        else if (sessUse.Username.ToLower() == "novartis" || sessUse.Username.ToLower() == "aenovartis")
        {
            PanelChange.Visible = false;
            PanelNonPersonal.Visible = true;
        }
    }
    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        if (txtPassword.Text == txtPasswordConfirm.Text)
        {
            if (txtPassword.Text.Length >= 8)
            {
                if (Regex.IsMatch(txtPassword.Text, "\\d"))
                {
                    if (Regex.IsMatch(txtPassword.Text, "[A-Z]") && Regex.IsMatch(txtPassword.Text, "[a-z]"))
                    {
                        if (Regex.IsMatch(txtPassword.Text, "[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]"))
                        {
                            sessUse.Password = txtPassword.Text;
                            sessUse.UpdateUserPassword(sessUse.Username);
                            if (sessUse.Role.StartsWith("FC"))
                            {
                                Session["sessUse"] = sessUse.Login(Request.Browser.Platform.ToString(), Request.UserHostAddress.ToString());
                                //Response.Redirect("../FinancialEvaluator/Dashboard.aspx");
                                PanelChange.Visible = false;
                                PanelThanks.Visible = true;
                            }
                            else
                            {
                                PanelChange.Visible = false;
                                PanelThanks.Visible = true;
                            }
                        }
                        else
                        {
                            LabelError.Text = "Your password must contain at least one symbol.";
                            LabelError.Visible = true;
                        }
                    }
                    else
                    {
                        LabelError.Text = "Your password must contain both upper and lower case characters.";
                        LabelError.Visible = true;
                    }
                }
                else
                {
                    LabelError.Text = "Your password must contain at least 1 number.";
                    LabelError.Visible = true;
                }
            }
            else
            {
                LabelError.Text = "Your password must be at least 8 characters.";
                LabelError.Visible = true;
            }
        }
        else
        {
            LabelError.Text = "The passwords entered do not match.";
            LabelError.Visible = true;
        }
    }
}