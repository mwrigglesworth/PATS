using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Patient_PersonelAssignment : System.Web.UI.UserControl
{
    GIPAP_Objects.Patient myPatient = new GIPAP_Objects.Patient();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    public int choice;
    public string userRole = "";
    public bool showDistributor = false;
    public bool privUser = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        iucon.web.Controls.ParameterCollection parameters = iucon.web.Controls.ParameterCollection.Instance;
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        userRole = sessUse.Role;
        try
        {
            choice = Convert.ToInt32(parameters["choice"]);
        }
        catch
        {
            choice = 0;
        }
        myPatient.InflateCountryInfo(choice, sessUse.Role);
        LabelMS.Text = myPatient.MaxStationName;
        LabelPhys.Text = "<br />" + myPatient.PhysicianName;

        //if (Request.UrlReferrer.Query.Contains("trans"))
        //{
        //    string suggestions = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["suggestions"];
        //    string addedPersonType = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["addtype"];
           
        //    if (addedPersonType == "Physician")
        //    {

        //        PanelPhysTrans.Visible = true;
        //        LabelPhysTrans.Text = "Physician Transfer Complete<br />[<a href=Patientemail.aspx?mailType=PhysicianTransferEmailDelete&pcount=0&choice=" + choice + ">Send Emails</a>]";
        //        if (suggestions != "")
        //        {
        //            LabelPhysTrans.Text += "<br /><br /><b>Does the PO need to be changed?</b><br /><br />Suggestions: " + suggestions;
        //        }
        //    }
        //    else if (addedPersonType == "FEBranch")
        //    {
        //        PanelBranchAssign.Visible = true;
        //        LabelBranchAssign.Text = "Branch Assignment Complete<br />[<a href=Patientemail.aspx?mailType=NOABranchAssignment&choice=" + choice + ">Send Emails</a>]";
        //        showDistributor = false;
        //    }
        //    else if (addedPersonType == "Distributor")
        //    {
        //        PanelDistributorAssign.Visible = true;
        //        LabelDistributorAssign.Text = "Distributor Assignment Complete<br />[<a href=Patientemail.aspx?mailType=NOABranchAssignment&choice=" + choice + ">Send Emails</a>]";
        //        showDistributor = true;
        //    }
            
        //}
        

        LabelContacts.Text = myPatient.ContactName;
        LabelPO.Text = myPatient.ProgramOfficerName;
        if (myPatient.CountryID == 76)/*myPatient.FlagNOA || myPatient.NOAPhys*/
        {
            LabelBranchHeader.Visible = true;
            LabelBranch.Text = "<br />" + myPatient.FCBranch;
                showDistributor = false;
        }
        else /* For all other countries show Distributor info*/
        {
            lblDistributorHeader.Visible = true;
            LabelDistributor.Text = myPatient.DistributorName;
           showDistributor = true;
        }

        if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
        {
            privUser = true;
        }
       // hlContact.NavigateUrl = "../EditContact.aspx?choice=" + choice.ToString() + "&cid=0";
       // hlPhys.NavigateUrl = "../PhysicianTransferRequest.aspx?choice=" + choice.ToString() + "&Sender=Patient&AddType=Physician";
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
    //protected void lbMaxStations_Click(object sender, EventArgs e)
    //{
    //    PanelDisplay.Visible = false;
    //    PanelUpdate.Visible = true;
    //    PanelGroup.Visible = true;
    //    LabelHeader.Text = "Max Stations";
    //    ButtonUpdateMS.Visible = true;
    //    GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove(choice, "Patient", "MaxStation", sessUse.Role);
    //    this.FillCheckBox(myAr.AddRemoveDS.Tables[0], myAr.AddRemoveDS.Tables[1], cblstPersonel, "personname", "personid");
    //    if (myAr.AddRemoveDS.Tables[2].Rows.Count > 0)
    //    {
    //        rblstGroups.DataSource = myAr.AddRemoveDS.Tables[2];
    //        rblstGroups.DataTextField = "groupname";
    //        rblstGroups.DataValueField = "persongroupid";
    //        rblstGroups.DataBind();
    //    }
    //    if (myAr.Suggestions != "")
    //    {
    //        LabelSuggestions.Text = myAr.Suggestions;
    //    }
    //}
    //protected void ButtonUpdateMS_Click(object sender, EventArgs e)
    //{
    //    GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
    //    if (rblstGroups.SelectedIndex == -1)
    //    {
    //        myAr.MSGroupID = 0;
    //    }
    //    else
    //    {
    //        myAr.MSGroupID = Convert.ToInt32(rblstGroups.SelectedValue);
    //    }
    //    myAr.Update(cblstPersonel, sessUse.Username, sessUse.Role, choice, "Patient", "MaxStation");
    //    myPatient.InflateCountryInfo(choice, sessUse.Role);
    //    LabelMS.Text = myPatient.MaxStationName;
    //    PanelDisplay.Visible = true;
    //    PanelUpdate.Visible = false;
    //    PanelGroup.Visible = false;
    //    ButtonUpdateMS.Visible = false;
    //    cblstPersonel.Enabled = true;
    //}
    //protected void lbPhys_Click(object sender, EventArgs e)
    //{
    //    PanelDisplay.Visible = false;
    //    PanelUpdate.Visible = true;
    //    PanelGroup.Visible = false;
    //    LabelHeader.Text = "Physicians";
    //    ButtonUpdatePhys.Visible = true;
    //    GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove(choice, "Patient", "Physician", sessUse.Role);
    //    this.FillCheckBox(myAr.AddRemoveDS.Tables[0], myAr.AddRemoveDS.Tables[1], cblstPersonel, "personname", "personid");
    //}
    //protected void rblstGroups_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    cblstPersonel.ClearSelection();
    //    cblstPersonel.Enabled = false;
    //}
    //protected void ButtonUpdatePhys_Click(object sender, EventArgs e)
    //{
    //    GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
    //    myAr.Update(cblstPersonel, sessUse.Username, sessUse.Role, choice, "Patient", "Physician");
    //    myPatient.InflateCountryInfo(choice, sessUse.Role);
    //    LabelPhys.Text = myPatient.PhysicianName + "<br />";
    //    PanelDisplay.Visible = true;
    //    PanelUpdate.Visible = false;
    //    PanelGroup.Visible = false;
    //    ButtonUpdatePhys.Visible = false;
    //    cblstPersonel.Enabled = true;
    //    //PanelPhysTrans.Visible = true;
    //   // LabelPhysTrans.Text = "Physician Transfer Complete<br />[<a href=Patientemail.aspx?mailType=PhysicianTransferEmailDelete&pcount=0&choice=" + myAr.SenderID.ToString() + ">Send Emails</a>]";
    //    if (myAr.Suggestions != "")
    //    {
    //  //      LabelPhysTrans.Text += "<br /><br /><b>Does the PO need to be changed?</b><br /><br />Suggestions: " + myAr.Suggestions;
    //    }
    //}
    //protected void lbPO_Click(object sender, EventArgs e)
    //{
    //    PanelDisplay.Visible = false;
    //    PanelUpdate.Visible = true;
    //    PanelGroup.Visible = false;
    //    LabelHeader.Text = "Program Officers";
    //    ButtonUpdatePO.Visible = true;
    //    GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove(choice, "Patient", "TMFUser", sessUse.Role);
    //    this.FillCheckBox(myAr.AddRemoveDS.Tables[0], myAr.AddRemoveDS.Tables[1], cblstPersonel, "personname", "personid");
    //}
    //protected void ButtonUpdatePO_Click(object sender, EventArgs e)
    //{
    //    GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
    //    myAr.Update(cblstPersonel, sessUse.Username, sessUse.Role, choice, "Patient", "TMFUser");
    //    myPatient.InflateCountryInfo(choice, sessUse.Role);
    //    LabelPO.Text = myPatient.ProgramOfficerName;
    //    PanelDisplay.Visible = true;
    //    PanelUpdate.Visible = false;
    //    PanelGroup.Visible = false;
    //    ButtonUpdatePO.Visible = false;
    //    cblstPersonel.Enabled = true;
    //}
    //protected void lbBranch_Click(object sender, EventArgs e)
    //{
    //    PanelDisplay.Visible = false;
    //    PanelUpdate.Visible = true;
    //    PanelGroup.Visible = false;
    //    LabelHeader.Text = "NOA FE Branches / Stockists";
    //    ButtonUpdateBranch.Visible = true;
    //    GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove(choice, "Patient", "FEBranch", sessUse.Role);
    //    this.FillCheckBox(myAr.AddRemoveDS.Tables[0], myAr.AddRemoveDS.Tables[1], cblstPersonel, "officename", "fcofficeid");
    //    //adding stockist stuff
    //    PanelStockist.Visible = true;
    //    if (myAr.AddRemoveDS.Tables[2].Rows.Count > 0)
    //    {
    //        rblstStockist.DataSource = myAr.AddRemoveDS.Tables[2];
    //        rblstStockist.DataTextField = "officename";
    //        rblstStockist.DataValueField = "stockistid";
    //        rblstStockist.DataBind();
    //        if (myAr.AddRemoveDS.Tables[3].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < myAr.AddRemoveDS.Tables[3].Rows.Count; i++)
    //            {
    //                for (int k = 0; k < rblstStockist.Items.Count; k++)
    //                {
    //                    if (myAr.AddRemoveDS.Tables[3].Rows[i]["stockistid"].ToString() == rblstStockist.Items[k].Value)
    //                    {
    //                        rblstStockist.Items[k].Selected = true;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    //protected void ButtonUpdateBranch_Click(object sender, EventArgs e)
    //{
    //    GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
    //    if (rblstStockist.SelectedIndex != -1)
    //    {
    //        myAr.StockistID = Convert.ToInt32(rblstStockist.SelectedValue);
    //        myAr.Update(cblstPersonel, sessUse.Username, sessUse.Role, choice, "Patient", "FEBranch");
    //        myPatient.InflateCountryInfo(choice, sessUse.Role);
    //        LabelBranch.Text = myPatient.FCBranch;
    //        PanelDisplay.Visible = true;
    //        PanelUpdate.Visible = false;
    //        PanelGroup.Visible = false;
    //        ButtonUpdateBranch.Visible = false;
    //        cblstPersonel.Enabled = true;
    //        if (sessUse.Role == "TMFUser" || sessUse.Role == "MaxStation")
    //        {
    //            PanelBranchAssign.Visible = true;
    //            LabelBranchAssign.Text = "Branch Assignment Complete<br />[<a href=Patientemail.aspx?mailType=NOABranchAssignment&choice=" + myAr.SenderID.ToString() + ">Send Emails</a>]";
    //        }
    //    }
    //    else
    //    {
    //        LabelStockist.Text = "A stockist is now required";
    //        myAr.StockistID = -1;
    //    }
  //  }
}
