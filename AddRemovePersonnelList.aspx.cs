using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AddRemovePersonnelList : System.Web.UI.Page
{
    string PERSONTYPE_CONTACT = "PatientContact";//System.Configuration.ConfigurationManager.AppSettings["PERSONTYPE_CAREGIVER"].ToString();
    string PERSONTYPE_OFFICER = "TMFUser";//System.Configuration.ConfigurationManager.AppSettings["PERSONTYPE_PROGOFFICER"].ToString();
    string PERSONTYPE_PHYSICIAN = "Physician";
    string PERSONTYPE_MAXSTATION = "MaxStation";//System.Configuration.ConfigurationManager.AppSettings["PERSONTYPE_PHYSICIAN"].ToString();
    string PERSONTYPE_STOCKIST = "FEBranch";
       

    GIPAP_Objects.Country countryObj = new GIPAP_Objects.Country();
    GIPAP_Objects.Person personObj = new GIPAP_Objects.Person();
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    GIPAP_Objects.Patient patientObj;
    GIPAP_Objects.AddRemove addRemovePatientPeople;

    public string SenderType = "";
    public int SenderId = 0;
    public string AddType = "";
    public string Action = "viewall";
    public string ReturnUrl = "";
    public string TransferData = "no";
    public string Suggestions = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];

        try
        {
            SenderId = Convert.ToInt32(Request.QueryString["senderid"]);
            SenderType = Request.QueryString["sendertype"].ToString();
            AddType = Request.QueryString["addtype"].ToString();
            Action = Request.QueryString["action"];

        }
        catch
        {
            Response.Redirect(sessUse.HomePage);
        }


        if (Action == "viewall")
        {

            GIPAP_Objects.AddRemove addRemovePatientPeople = new GIPAP_Objects.AddRemove(SenderId, SenderType, AddType, sessUse.Role);
            this.PersonnelPanel.Visible = true;

            DataSet patientPersonList = addRemovePatientPeople.AddRemoveDS;

            if(patientPersonList.Tables[0] == null || patientPersonList.Tables[0].Rows.Count <= 0)
            {
                NoPersonnelPanel.Visible = true;
                PersonnelPanel.Visible = false;
                return;
            }

            int numDbList = 0;
            if(patientPersonList.Tables[1] != null)
            {
                numDbList = patientPersonList.Tables[1].Rows.Count;
            }

            if (AddType == "Clinics")
                this.FillCheckBox(patientPersonList.Tables[0], patientPersonList.Tables[1], chkBoxLst, "clinicname", "clinicid");

            if (AddType == "Distributor")
            {
                this.rdPersonnelList.Visible = true;
                this.chkBoxLst.Visible = false;
                this.FillRadioButtonList(patientPersonList.Tables[0], patientPersonList.Tables[1], rdPersonnelList, "officename", "distributorid");
            }

            if (AddType == "FEBranch")
            {
                this.rdPersonnelList.Visible = true;
                this.chkBoxLst.Visible = true;
                //if (patientPersonList.Tables[2].Rows.Count > 0)
                //{
                //    rdPersonnelList.DataSource = patientPersonList.Tables[2];
                //    rdPersonnelList.DataTextField = "officename";
                //    rdPersonnelList.DataValueField = "stockistid";
                //    rdPersonnelList.DataBind();
                //    if (patientPersonList.Tables[3].Rows.Count > 0)
                //    {
                //        for (int i = 0; i < patientPersonList.Tables[3].Rows.Count; i++)
                //        {
                //            for (int k = 0; k < rdPersonnelList.Items.Count; k++)
                //            {
                //                if (patientPersonList.Tables[3].Rows[i]["stockistid"].ToString() == rdPersonnelList.Items[k].Value)
                //                {
                //                    rdPersonnelList.Items[k].Selected = true;
                //                }
                //            }
                //        }
                //    }
                //}
                this.FillRadioButtonList(patientPersonList.Tables[2], patientPersonList.Tables[3], rdPersonnelList, "officename", "stockistid");
                this.FillCheckBox(patientPersonList.Tables[0], patientPersonList.Tables[1], chkBoxLst, "officename", "fcofficeid");
            }

            if (AddType == PERSONTYPE_OFFICER || AddType == PERSONTYPE_PHYSICIAN || AddType == PERSONTYPE_MAXSTATION)
            {
                this.rdPersonnelList.Visible = false;
                this.chkBoxLst.Visible = true;
                this.FillCheckBox(patientPersonList.Tables[0], patientPersonList.Tables[1], chkBoxLst, "PersonName", "PersonID");
            }


        }
        else if (Action == "update")
        {
            CheckBoxList chkBoxList = new CheckBoxList();
            RadioButtonList rdBtnList = new RadioButtonList();
            GIPAP_Objects.AddRemove myAr = new GIPAP_Objects.AddRemove();
            
            if (AddType == "FEBranch")
            {
                string stockist_id = Request.QueryString["radid"].ToString();
                if (stockist_id != "" && stockist_id != null)
                {
                    int id_num = new int();
                    id_num = Convert.ToInt32(stockist_id);
                    ListItem item_ = new ListItem(id_num.ToString());
                    rdBtnList.Items.Add(item_);
                    item_.Selected = true;
                    myAr.StockistID = id_num;
                }
            }

            if (AddType == "Distributor")
            {
                string distributor_id = Request.QueryString["radid"].ToString();
                int id_num = new int();
                id_num = Convert.ToInt32(distributor_id);
                ListItem item_ = new ListItem(id_num.ToString());
                chkBoxList.Items.Add(item_);
                item_.Selected = true;
                item_.Selected = true;
                myAr.Update(chkBoxList, sessUse.Username, sessUse.Role, SenderId, SenderType, AddType);

            }
            else
            {
                string person_ids = Request.QueryString["ids"].ToString();
                if (person_ids != null && person_ids != "")
                {
                    string[] person_id_split = person_ids.Split(',');
                    int[] person_ids_num = new int[person_id_split.Length];
                    for (int numids = 0; numids < person_id_split.Length; numids++)
                    {
                        int id_num = new int();
                        id_num = Convert.ToInt32(person_id_split[numids]);
                        //person_ids_num[numids] = id_num;
                        ListItem item_ = new ListItem(id_num.ToString());
                        chkBoxList.Items.Add(item_);
                        item_.Selected = true;
                    }
                }

                myAr.Update(chkBoxList, sessUse.Username, sessUse.Role, SenderId, SenderType, AddType);
            }
            //UPdate physician list
            if (AddType.Equals(PERSONTYPE_PHYSICIAN, StringComparison.CurrentCultureIgnoreCase))
            {
                TransferData = "yes";
                if (myAr.Suggestions != "")
                {
                    Suggestions = myAr.Suggestions;
                }
            }

            //UPdate FEBranch/Stockist list
            if (AddType.Equals(PERSONTYPE_STOCKIST, StringComparison.CurrentCultureIgnoreCase))
            {
                TransferData = "yes";
                if (myAr.Suggestions != "")
                {
                    Suggestions = myAr.Suggestions;
                }
            }

            //Update program officer list.
            //if (personType.Equals(PERSONTYPE_OFFICER, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    AddRemove addRemovePatPORecs = new AddRemove(patientIdNum, "Patient", PERSONTYPE_OFFICER);
            //    addRemovePatPORecs.Update(lstBox, sessUse.Username);
            //    ReturnUrl = addRemovePatPORecs.ReturnURL;
            //}

            ////Update max station list.
            //if (personType.Equals(PERSONTYPE_MAXSTATION, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    AddRemove addRemovePatMaxStnRecs = new AddRemove(patientIdNum, "Patient", PERSONTYPE_MAXSTATION);
            //    addRemovePatMaxStnRecs.Update(lstBox, sessUse.Username);
            //    ReturnUrl = addRemovePatMaxStnRecs.ReturnURL;
            //}
        }
    }

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
                    cbFill.Items[i2].Attributes.Add("ID", "" + dt1.Rows[i2][valueF].ToString());
                    string mw = dt2.Rows[i][valueF].ToString();
                    if (cbFill.Items[i2].Value == mw)
                    {
                        cbFill.Items[i2].Selected = true;
                    }
                }
            }
        }
        else
        {
            for (int k = 0; k < cbFill.Items.Count; k++)
            {
                cbFill.Items[k].Attributes.Add("ID", "" + dt1.Rows[k][valueF].ToString());
            }
        }

    }

    private void FillRadioButtonList(DataTable dt1, DataTable dt2, System.Web.UI.WebControls.RadioButtonList rbFill, string textF, string valueF)
    {
        rbFill.DataSource = dt1;
        rbFill.DataTextField = textF;
        rbFill.DataValueField = valueF;
        rbFill.DataBind();
        if (dt2.Rows.Count > 0)
        {
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                for (int i2 = 0; i2 < rbFill.Items.Count; i2++)
                {
                    rbFill.Items[i2].Attributes.Add("ID", "" + dt1.Rows[i2][valueF].ToString());
                    string mw = dt2.Rows[i][valueF].ToString();
                    if (rbFill.Items[i2].Value == mw)
                    {
                        rbFill.Items[i2].Selected = true;
                    }
                }
            }
        }
        else
        {
            for (int k = 0; k < rbFill.Items.Count; k++)
            {
                rbFill.Items[k].Attributes.Add("ID", "" + dt1.Rows[k][valueF].ToString());
            }
        }


    }
}