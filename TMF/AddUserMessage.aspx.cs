using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class TMF_AddUserMessage : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    DataSet CurrentMessages;
    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        if (!Page.IsPostBack)
        {
            FillCountryComboBox();
            FillMessages();
        }
    }


    private void FillMessages()
    {
        CurrentMessages = sessUse.getAllMessage();
        GridMessage.DataSource = CurrentMessages;
        GridMessage.DataBind();
    }


    protected void ButtonPost_Click(object sender, System.EventArgs e)
    {   
        sessUse.CreateMessage(txtMessage.Text.Replace("\n", "<br>"), Convert.ToInt32(dropRole.SelectedValue), Convert.ToInt32(dropCountry.SelectedValue));
        Page.Response.Redirect(Page.Request.RawUrl, false);
    }

    protected void ButtonCancel_Click(object sender, System.EventArgs e)
    {
        Page.Response.Redirect(Page.Request.RawUrl, false);
        //Response.Redirect("Dashboard.aspx");
    }

    protected void dropRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropRole.SelectedValue == "3")
        {
            CountryMenu.Visible = true;
            CountryHead.Visible = true;
            //GetCurrentMessage(dropRole.SelectedValue,"0");
        }
        else if (dropRole.SelectedValue == "2")
        {
            CountryMenu.Visible = false;
            CountryHead.Visible = false;
            dropCountry.SelectedValue = "0";
            //GetCurrentMessage(dropRole.SelectedValue,"0");
        }
        else
        {
            CountryMenu.Visible = false;
            CountryHead.Visible = false;
            dropCountry.SelectedValue = "0";
        }
    }

   
    private void FillCountryComboBox()
    {
        //Fills the country combobox with the countries from the database
        DataSet ds;
        GIPAP_Objects.Country myCountry = new GIPAP_Objects.Country();
        ds = myCountry.GetCountryList(false);
        //bind data to patient country
        dropCountry.DataSource = ds;
        dropCountry.DataValueField = "CountryID";
        dropCountry.DataTextField = "CountryName";
        dropCountry.DataBind();
        dropCountry.Items.Insert(0, "All Countries");
        dropCountry.SelectedItem.Value = "0";
    }

    protected void ButtonDisable_Click(object sender, EventArgs e)
    {
        //sessUse.DeleteCurrentMessage(Convert.ToInt32(LabelID.Text));
        //GetCurrentMessage(dropRole.SelectedValue, dropCountry.SelectedValue);
    }
    protected void dropCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetCurrentMessage(dropRole.SelectedValue, dropCountry.SelectedValue);
    }

   

    //protected void editButton_OnClick(object sender, EventArgs e)
    //{
    //    ImageButton btn = sender as ImageButton;
    //    GridViewRow row = btn.NamingContainer as GridViewRow;
    //    //string pk = GridMessage.DataKeys[row.RowIndex].Values[0].ToString();
    //    //DataRow[] result = CurrentMessages.Tables[0].Select("MessageId='" + pk + "'");
    //    //dropRole.SelectedItem.Text = row.Cells[0].Text;
    //    dropRole.SelectedValue = dropRole.Items.FindByText(row.Cells[0].Text).Value;
    //    dropRole.Enabled = false;
    //    dropCountry.Enabled = false;
    //    if (row.Cells[0].Text == "Physician")
    //    {
    //        CountryHead.Visible = true;
    //        CountryMenu.Visible = true;
    //        //dropCountry.SelectedItem.Text = row.Cells[1].Text;
    //       dropCountry.SelectedValue= dropCountry.Items.FindByText(row.Cells[1].Text).Value;
    //    }
    //    else
    //    {
    //        CountryHead.Visible = false;
    //        CountryMenu.Visible = false;
    //    }
    //    txtMessage.Text = row.Cells[2].Text;
    //    CurIndex.Value = GridMessage.DataKeys[row.RowIndex].Values[0].ToString();
    //}


    //protected void deleteButton_OnClick(object sender, EventArgs e)
    //{
    //    ImageButton btn = sender as ImageButton;
    //    GridViewRow row = btn.NamingContainer as GridViewRow;
    //    string pk = GridMessage.DataKeys[row.RowIndex].Values[0].ToString();
    //    sessUse.DeleteCurrentMessage(Convert.ToInt32(pk));
    //    FillMessages();
    //    Page.Response.Redirect(Page.Request.RawUrl, false);
    //}

    protected void GridMessage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridMessage.PageIndex = e.NewPageIndex;
        FillMessages();
    }
  
    protected void GridMessage_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridMessage.EditIndex = -1;
        FillMessages();
    }

    protected void GridMessage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string pk = GridMessage.DataKeys[e.RowIndex].Values[0].ToString();
        sessUse.DeleteCurrentMessage(Convert.ToInt32(pk));
        GridMessage.EditIndex = -1;
        FillMessages();
    }

    protected void GridMessage_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridMessage.EditIndex = e.NewEditIndex;
        FillMessages();
    }

    protected void GridMessage_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int msgid = Convert.ToInt32(GridMessage.DataKeys[e.RowIndex].Values[0].ToString());
        TextBox txtmessage = (TextBox)GridMessage.Rows[e.RowIndex].FindControl("txtMessage");
        sessUse.UpdateMessage(msgid, txtmessage.Text.Replace("\n", "<br>"));
        GridMessage.EditIndex = -1;
        FillMessages();
    }
}