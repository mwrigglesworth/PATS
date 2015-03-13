using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Country_EditCountry : System.Web.UI.Page
{
    GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
    int choice;
    GIPAP_Objects.User sessUse;

    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        choice = Convert.ToInt32(Request.QueryString["choice"]);
        myCountry = new GIPAP_Objects.Country(choice, sessUse.Role);

        if (!Page.IsPostBack)
        {
            this.FillComboBox();

            rblstActive.SelectedValue = myCountry.ActiveGIPAP;
            rblstNeedFinInfo.SelectedIndex = Convert.ToInt32(myCountry.NeedFinancialInfo);
            if (myCountry.PediatricAge != 0)
            {
                txtPediatricAge.Text = myCountry.PediatricAge.ToString();
            }
            //noa
            rblstNOAGlivec.SelectedIndex = Convert.ToInt32(myCountry.NOAGlivec);
            rblstNoaTasigna.SelectedIndex = myCountry.NOATasigna;
            rblstTasignaPedApproved.SelectedIndex = Convert.ToInt32(myCountry.TasignaPedApproved);
            //
            rblstAcceptingNewApps.Items[1].Selected = myCountry.AcceptingNewApps;
            rblstCMLApproved.SelectedIndex = Convert.ToInt32(myCountry.CMLApproved);
            rblstNeedInterfInfo.SelectedIndex = Convert.ToInt32(myCountry.NeedInterferonInfo);
            txtCMLInfo.Text = myCountry.CMLInfo.Replace("<br>", "\n");
            rblstPediatric.SelectedIndex = Convert.ToInt32(myCountry.PediatricApproved);
            rblstGistApproved.SelectedIndex = Convert.ToInt32(myCountry.GISTApproved);
            rblstGISTPedApproved.SelectedIndex = Convert.ToInt32(myCountry.GISTPedApproved);
            txtGISTInfo.Text = myCountry.GISTInfo.Replace("<br>", "\n");
            try
            {
                dropSubRegion.SelectedValue = myCountry.SubRegionID.ToString();
            }
            catch { }
            for (int i = Convert.ToInt32(DateTime.Today.Year.ToString()); i >= 2000; i--)
            {
                dropFinYear.Items.Add(i.ToString());
                dropAllYear.Items.Add(i.ToString());
                dropDFSPYear.Items.Add(i.ToString());
                dropAdjGISTYear.Items.Add(i.ToString());
                dropMDSYear.Items.Add(i.ToString());
                dropMastYear.Items.Add(i.ToString());
                dropHESYear.Items.Add(i.ToString());
            }
            if (myCountry.FinancialDeclarationDate != Convert.ToDateTime("1/1/0001"))
            {
                DropFinDay.SelectedValue = myCountry.FinancialDeclarationDate.Day.ToString();
                dropFinMonth.SelectedValue = myCountry.FinancialDeclarationDate.Month.ToString();
                dropFinYear.SelectedValue = myCountry.FinancialDeclarationDate.Year.ToString();
            }
            rblstPhAll.SelectedIndex = myCountry.PhAllStatus;
            rblstPhallPedApproved.SelectedIndex = Convert.ToInt32(myCountry.PhAllPedApproved);
            if (myCountry.PhAllStatus == 0)
            {
                PanelPhAll.Visible = false;
            }
            else
            {
                PanelPhAll.Visible = true;
            }
            if (myCountry.ALLDate != Convert.ToDateTime("1/1/0001"))
            {
                dropAllDay.SelectedValue = myCountry.ALLDate.Day.ToString();
                dropAllMonth.SelectedValue = myCountry.ALLDate.Month.ToString();
                dropAllYear.SelectedValue = myCountry.ALLDate.Year.ToString();
            }
            txtPhAllInfo.Text = myCountry.PhALLInfo.Replace("<br>", "\n");
            //DFSP
            rblstDFSPApproved.SelectedIndex = Convert.ToInt32(myCountry.DFSPApproved);
            if (myCountry.DFSPApproved)
            {
                PanelDFSP.Visible = true;
            }
            else
            {
                PanelDFSP.Visible = false;
            }
            rblstDFSPPedApproved.SelectedIndex = Convert.ToInt32(myCountry.DFSPPedApproved);
            if (myCountry.DFSPDate != Convert.ToDateTime("1/1/0001"))
            {
                dropDFSPDay.SelectedValue = myCountry.DFSPDate.Day.ToString();
                dropDFSPMonth.SelectedValue = myCountry.DFSPDate.Month.ToString();
                dropDFSPYear.SelectedValue = myCountry.DFSPDate.Year.ToString();
            }
            txtDFSPInfo.Text = myCountry.DFSPInfo.Replace("<br>", "\n");
            // adj gist
            rblstADJGistApproved.SelectedIndex = Convert.ToInt32(myCountry.ADJGISTApproved);
            if (myCountry.ADJGISTApproved)
            {
                PanelADJGIST.Visible = true;
            }
            else
            {
                PanelADJGIST.Visible = false;
            }
            rblstADJGistPedApproved.SelectedIndex = Convert.ToInt32(myCountry.ADJGISTPedApproved);
            if (myCountry.ADJGISTDate != Convert.ToDateTime("1/1/0001"))
            {
                dropAdjGISTDay.SelectedValue = myCountry.ADJGISTDate.Day.ToString();
                dropAdjGISTMonth.SelectedValue = myCountry.ADJGISTDate.Month.ToString();
                dropAdjGISTYear.SelectedValue = myCountry.ADJGISTDate.Year.ToString();
            }
            txtADJGistInfo.Text = myCountry.ADJGISTInfo.Replace("<br>", "\n");
            //mds
            rblstMDSapproved.SelectedIndex = Convert.ToInt32(myCountry.MDSApproved);
            if (myCountry.MDSApproved)
            {
                PanelMDS.Visible = true;
            }
            else
            {
                PanelMDS.Visible = false;
            }
            rblstMDSPedApproved.SelectedIndex = Convert.ToInt32(myCountry.MDSPedApproved);
            if (myCountry.MDSDate != Convert.ToDateTime("1/1/0001"))
            {
                dropMDSDay.SelectedValue = myCountry.MDSDate.Day.ToString();
                dropMDSMonth.SelectedValue = myCountry.MDSDate.Month.ToString();
                dropMDSYear.SelectedValue = myCountry.MDSDate.Year.ToString();
            }
            txtMDSInfo.Text = myCountry.MDSInfo.Replace("<br>", "\n");
            //MAST
            rblstMastApproved.SelectedIndex = Convert.ToInt32(myCountry.MASTApproved);
            if (myCountry.MASTApproved)
            {
                PanelMast.Visible = true;
            }
            else
            {
                PanelMast.Visible = false;
            }
            rblstMASTPedApproved.SelectedIndex = Convert.ToInt32(myCountry.MASTPedApproved);
            if (myCountry.MASTDate != Convert.ToDateTime("1/1/0001"))
            {
                dropMastDay.SelectedValue = myCountry.MASTDate.Day.ToString();
                dropMastMonth.SelectedValue = myCountry.MASTDate.Month.ToString();
                dropMastYear.SelectedValue = myCountry.MASTDate.Year.ToString();
            }
            txtMASTInfo.Text = myCountry.MASTInfo.Replace("<br>", "\n");
            //hes
            rblstHESApproved.SelectedIndex = Convert.ToInt32(myCountry.HESApproved);
            if (myCountry.HESApproved)
            {
                PanelHES.Visible = true;
            }
            else
            {
                PanelHES.Visible = false;
            }
            rblstHESPedApproved.SelectedIndex = Convert.ToInt32(myCountry.HESPedApproved);
            if (myCountry.HESDate != Convert.ToDateTime("1/1/0001"))
            {
                dropHESDay.SelectedValue = myCountry.HESDate.Day.ToString();
                dropHESMonth.SelectedValue = myCountry.HESDate.Month.ToString();
                dropHESYear.SelectedValue = myCountry.HESDate.Year.ToString();
            }
            txtHESInfo.Text = myCountry.HESInfo.Replace("<br>", "\n");

            txtEmail.Text = myCountry.Email;
            txtsaeEmail.Text = myCountry.SAEEmail;
            txtNotes.Text = myCountry.Notes.Replace("<br>", "\n");

            LabelCountryName.Text = myCountry.CountryName;
        }
    }
    //**********************************************************************************************************************
    private void FillComboBox()
    {
        dropSubRegion.DataSource = myCountry.SubRegionList();
        dropSubRegion.DataTextField = "SubRegion";
        dropSubRegion.DataValueField = "SubRegionID";
        dropSubRegion.DataBind();
        dropSubRegion.Items.Insert(0, "Select One");
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        myCountry.ActiveGIPAP = rblstActive.SelectedValue;
        //myCountry.LDC = rblstLDC.Items[1].Selected;
        myCountry.AcceptingNewApps = Convert.ToBoolean(rblstAcceptingNewApps.SelectedIndex);
        try
        {
            myCountry.PediatricAge = Convert.ToInt32(txtPediatricAge.Text);
        }
        catch { }
        //noa
        myCountry.NOAGlivec = rblstNOAGlivec.Items[1].Selected;
        myCountry.NOATasigna = rblstNoaTasigna.SelectedIndex;
        myCountry.TasignaPedApproved = rblstTasignaPedApproved.Items[1].Selected;
        //
        myCountry.NeedFinancialInfo = rblstNeedFinInfo.Items[1].Selected;
        myCountry.GISTApproved = rblstGistApproved.Items[1].Selected;
        myCountry.GISTPedApproved = rblstGISTPedApproved.Items[1].Selected;
        myCountry.GISTInfo = txtGISTInfo.Text.Replace("\n", "<br>");
        myCountry.CMLApproved = rblstCMLApproved.Items[1].Selected;
        myCountry.PediatricApproved = rblstPediatric.Items[1].Selected;
        myCountry.NeedInterferonInfo = rblstNeedInterfInfo.Items[1].Selected;
        myCountry.CMLInfo = txtCMLInfo.Text.Replace("\n", "<br>");
        if (dropSubRegion.SelectedIndex != 0)
        {
            myCountry.SubRegionID = Convert.ToInt32(dropSubRegion.SelectedValue);
        }
        try
        {
            myCountry.FinancialDeclarationDate = Convert.ToDateTime(dropFinMonth.SelectedValue + "/" + DropFinDay.SelectedValue + "/" + dropFinYear.SelectedValue);
        }
        catch { }
        myCountry.PhAllStatus = rblstPhAll.SelectedIndex;
        myCountry.PhAllPedApproved = rblstPhallPedApproved.Items[1].Selected;
        try
        {
            myCountry.ALLDate = Convert.ToDateTime(dropAllMonth.SelectedValue + "/" + dropAllDay.SelectedValue + "/" + dropAllYear.SelectedValue);
        }
        catch { }
        myCountry.PhALLInfo = txtPhAllInfo.Text.Replace("\n", "<br>");
        //dfsp
        myCountry.DFSPApproved = Convert.ToBoolean(rblstDFSPApproved.SelectedIndex);
        try
        {
            myCountry.DFSPDate = Convert.ToDateTime(dropDFSPMonth.SelectedValue + "/" + dropDFSPDay.SelectedValue + "/" + dropDFSPYear.SelectedValue);
        }
        catch { }
        myCountry.DFSPPedApproved = Convert.ToBoolean(rblstDFSPPedApproved.SelectedIndex);
        myCountry.DFSPInfo = txtDFSPInfo.Text.Replace("\n", "<br>");
        //adj gist
        myCountry.ADJGISTApproved = Convert.ToBoolean(rblstADJGistApproved.SelectedIndex);
        try
        {
            myCountry.ADJGISTDate = Convert.ToDateTime(dropAdjGISTMonth.SelectedValue + "/" + dropAdjGISTDay.SelectedValue + "/" + dropAdjGISTYear.SelectedValue);
        }
        catch { }
        myCountry.ADJGISTPedApproved = Convert.ToBoolean(rblstADJGistPedApproved.SelectedIndex);
        myCountry.ADJGISTInfo = txtADJGistInfo.Text.Replace("\n", "<br>");
        //mds
        myCountry.MDSApproved = Convert.ToBoolean(rblstMDSapproved.SelectedIndex);
        try
        {
            myCountry.MDSDate = Convert.ToDateTime(dropMDSMonth.SelectedValue + "/" + dropMDSDay.SelectedValue + "/" + dropMDSYear.SelectedValue);
        }
        catch { }
        myCountry.MDSPedApproved = Convert.ToBoolean(rblstMDSPedApproved.SelectedIndex);
        myCountry.MDSInfo = txtMDSInfo.Text.Replace("\n", "<br>");
        //mast
        myCountry.MASTApproved = Convert.ToBoolean(rblstMastApproved.SelectedIndex);
        try
        {
            myCountry.MASTDate = Convert.ToDateTime(dropMastMonth.SelectedValue + "/" + dropMastDay.SelectedValue + "/" + dropMastYear.SelectedValue);
        }
        catch { }
        myCountry.MASTPedApproved = Convert.ToBoolean(rblstMASTPedApproved.SelectedIndex);
        myCountry.MASTInfo = txtMASTInfo.Text.Replace("\n", "<br>");
        //hes
        myCountry.HESApproved = Convert.ToBoolean(rblstHESApproved.SelectedIndex);
        try
        {
            myCountry.HESDate = Convert.ToDateTime(dropHESMonth.SelectedValue + "/" + dropHESDay.SelectedValue + "/" + dropHESYear.SelectedValue);
        }
        catch { }
        myCountry.HESPedApproved = Convert.ToBoolean(rblstHESPedApproved.SelectedIndex);
        myCountry.HESInfo = txtHESInfo.Text.Replace("\n", "<br>");

        myCountry.Email = txtEmail.Text.Trim();
        myCountry.SAEEmail = txtsaeEmail.Text.Trim();
        myCountry.Notes = txtNotes.Text.Replace("\n", "<br>");

        myCountry.Update(sessUse.Username);
        Response.Redirect("CountryInfo.aspx?a=update&choice=" + myCountry.CountryID.ToString());
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CountryInfo.aspx?choice=" + myCountry.CountryID.ToString());
    }
    protected void rblstPhAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblstPhAll.SelectedIndex == 0)
        {
            dropAllDay.SelectedIndex = 0;
            dropAllMonth.SelectedIndex = 0;
            dropAllYear.SelectedIndex = 0;
            PanelPhAll.Visible = false;
        }
        else
        {
            PanelPhAll.Visible = true;
        }
    }
    protected void rblstDFSPApproved_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblstDFSPApproved.SelectedIndex == 0)
        {
            dropDFSPDay.SelectedIndex = 0;
            dropDFSPMonth.SelectedIndex = 0;
            dropDFSPYear.SelectedIndex = 0;
            PanelDFSP.Visible = false;
        }
        else
        {
            PanelDFSP.Visible = true;
        }
    }
    protected void rblstADJGistApproved_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblstADJGistApproved.SelectedIndex == 0)
        {
            dropAdjGISTDay.SelectedIndex = 0;
            dropAdjGISTMonth.SelectedIndex = 0;
            dropAdjGISTYear.SelectedIndex = 0;
            PanelADJGIST.Visible = false;
        }
        else
        {
            PanelADJGIST.Visible = true;
        }
    }
    protected void rblstMDSapproved_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblstMDSapproved.SelectedIndex == 0)
        {
            dropMDSDay.SelectedIndex = 0;
            dropMDSMonth.SelectedIndex = 0;
            dropMDSYear.SelectedIndex = 0;
            PanelMDS.Visible = false;
        }
        else
        {
            PanelMDS.Visible = true;
        }
    }
    protected void rblstMastApproved_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblstMastApproved.SelectedIndex == 0)
        {
            dropMastDay.SelectedIndex = 0;
            dropMastMonth.SelectedIndex = 0;
            dropMastYear.SelectedIndex = 0;
            PanelMast.Visible = false;
        }
        else
        {
            PanelMast.Visible = true;
        }
    }
    protected void rblstHESApproved_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblstHESApproved.SelectedIndex == 0)
        {
            dropHESDay.SelectedIndex = 0;
            dropHESMonth.SelectedIndex = 0;
            dropHESYear.SelectedIndex = 0;
            PanelHES.Visible = false;
        }
        else
        {
            PanelHES.Visible = true;
        }
    }
}
