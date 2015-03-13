using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class Country_CountryInfo : System.Web.UI.Page
{
    GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
    int choice;
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        string a = "";
        try
        {
            a = Request.QueryString["a"].ToString();
        }
        catch { }
        if (a != "")
        {
            PanelAlert.Visible = true;
            if (a == "update")
            {
                LabelAlert.Text = "Country information has been updated";
            }
        }
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myCountry.CountryID = choice;
        if (!Page.IsPostBack)
        {
            myCountry.InflatePersonel(sessUse.Role);

            LabelGLC.Text = myCountry.CpoName;
            LabelDSR.Text = myCountry.Dsr;
            LabelRegion.Text = myCountry.RegionalInfo();
        }
        if (sessUse.Role == "TMFUser")
        {
            lbDSR.Visible = true;
            lbGLC.Visible = true;
        }

        if (sessUse.Role == "TMFUser")
        {
            LabelList.Text = "<a href=EditCountry.aspx?choice=" + choice.ToString() + ">Edit Country Info</a><br><br>";
        }
        else
        {
            LabelList.Text = "<i><font color=gainsboro>Edit Country Info</font></i><br><br>";
        }
        LabelList.Text += "<a href=CountryActivity.aspx?choice=" + choice.ToString() + ">Country Activity</a><br><br>";
        LabelList.Text += "<a href=CountryEnrollment.aspx?choice=" + choice.ToString() + ">Enrollment by Diagnosis</a><br><br>";

        LabelActions.Text = "<a href=CountryDataSets.aspx?choice=" + choice.ToString() + "&dset=Patients>Patients</a><br><br>";
        LabelActions.Text += "<a href=CountryDataSets.aspx?choice=" + choice.ToString() + "&dset=Physicians>Physicians</a><br><br>";
        LabelActions.Text += "<a href=CountryDataSets.aspx?choice=" + choice.ToString() + "&dset=Clinics>Clinics</a><br><br>";
        LabelActions.Text += "<a href=CountryDataSets.aspx?choice=" + choice.ToString() + "&dset=MaxStations>Max Stations</a><br><br>";
        LabelActions.Text += "<a href=CountryDataSets.aspx?choice=" + choice.ToString() + "&dset=AEs>Adverse Events</a><br><br>";
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
    protected void lbGLC_Click(object sender, EventArgs e)
    {
        PanelDisplay.Visible = false;
        PanelUpdate.Visible = true;
        LabelHeader.Text = "GLC Assignment";
        ButtonUpdateGLC.Visible = true;
        GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove(choice, "Country", "GLC", sessUse.Role);
        this.FillCheckBox(myAr.AddRemoveDS.Tables[0], myAr.AddRemoveDS.Tables[1], cblstPersonel, "personname", "personid");
    }
    protected void lbDSR_Click(object sender, EventArgs e)
    {
        PanelDisplay.Visible = false;
        PanelUpdate.Visible = true;
        LabelHeader.Text = "DSR Assignment";
        ButtonUpdateDSR.Visible = true;
        GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove(choice, "Country", "DSR", sessUse.Role);
        this.FillCheckBox(myAr.AddRemoveDS.Tables[0], myAr.AddRemoveDS.Tables[1], cblstPersonel, "personname", "personid");
    }
    protected void ButtonUpdateDSR_Click(object sender, EventArgs e)
    {
        GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
        myAr.Update(cblstPersonel, sessUse.Username, sessUse.Role, choice, "Country", "DSR");
        myCountry.CountryID = choice;
        myCountry.InflatePersonel(sessUse.Role);
        LabelGLC.Text = myCountry.CpoName;
        LabelDSR.Text = myCountry.Dsr;
        PanelDisplay.Visible = true;
        PanelUpdate.Visible = false;
        ButtonUpdateDSR.Visible = false;
    }
    protected void ButtonUpdateGLC_Click(object sender, EventArgs e)
    {
        GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
        myAr.Update(cblstPersonel, sessUse.Username, sessUse.Role, choice, "Country", "GLC");
        myCountry.CountryID = choice;
        myCountry.InflatePersonel(sessUse.Role);
        LabelGLC.Text = myCountry.CpoName;
        LabelDSR.Text = myCountry.Dsr;
        PanelDisplay.Visible = true;
        PanelUpdate.Visible = false;
        ButtonUpdateGLC.Visible = false;
    }
}
