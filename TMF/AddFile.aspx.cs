using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class TMF_AddFile : System.Web.UI.Page
{
    GIPAP_Objects.User sessUse = new GIPAP_Objects.User();
    public Regex reg;
    protected void Page_Load(object sender, EventArgs e)
    {
        sessUse = (GIPAP_Objects.User)Session["sessUse"];
        if (!Page.IsPostBack)
        {
            BindData();
            
        }
    }


    private void BindData()
    {
            GridFiles.DataSource = sessUse.getSharedFile();
            GridFiles.DataBind();
            if (!sessUse.IsFileSharingAdmin)
            {
                GridFiles.Columns[2].Visible = false;
                ShareFileAdmin.Visible = false;
               
            }
    }

    protected void ButtonPost_Click(object sender, System.EventArgs e)
    {

        sessUse.CreateSharedFile(txtFileName.Text.Replace("\n", "<br>"), txtFilePath.Text.Replace("\n", "<br>"));
        //else
        //    sessUse.UpdateSharedFile(TextFileName.Text.Replace("\n", "<br>"), TextFilePath.Text.Replace("\n", "<br>"),Convert.ToInt32(CurIndex.Value));
        Page.Response.Redirect(Page.Request.RawUrl, false);
    }

    protected void ButtonCancel_Click(object sender, System.EventArgs e)
    {
        Page.Response.Redirect(Page.Request.RawUrl, false);
    }

    //protected void editButton_OnClick(object sender, EventArgs e)
    //{
    //    ImageButton btn = sender as ImageButton;
    //    GridViewRow row = btn.NamingContainer as GridViewRow;
    //    string pk = GridFiles.DataKeys[row.RowIndex].Values[0].ToString();

    //    TextFileName.Text = row.Cells[0].Text;
    //    var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);

    //    TextFilePath.Text = reg.Replace(row.Cells[1].Text, "");
    //    CurIndex.Value = GridFiles.DataKeys[row.RowIndex].Values[0].ToString();
    //}


    //protected void deleteButton_OnClick(object sender, EventArgs e)
    //{
    //    ImageButton btn = sender as ImageButton;
    //    GridViewRow row = btn.NamingContainer as GridViewRow;
    //    string pk = GridFiles.DataKeys[row.RowIndex].Values[0].ToString();
    //    sessUse.DeleteRecord(pk);
    //    BindData();
        
    //}
    //protected void GridFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridFiles.PageIndex = e.NewPageIndex;
    //    BindData();
    //}
    protected void GridFiles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridFiles.EditIndex = -1;
        BindData();
    }

    protected void GridFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string pk = GridFiles.DataKeys[e.RowIndex].Values[0].ToString();
        sessUse.DeleteRecord(pk);
        GridFiles.EditIndex = -1;
        BindData();
    }

    protected void GridFiles_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int fileid = Convert.ToInt32(GridFiles.DataKeys[e.RowIndex].Values[0].ToString());
        TextBox txtname = (TextBox)GridFiles.Rows[e.RowIndex].FindControl("txtFileNameEdit");
        TextBox txtpath = (TextBox)GridFiles.Rows[e.RowIndex].FindControl("txtFilePathEdit");

        sessUse.UpdateSharedFile(txtname.Text.Replace("\n", "<br>"), txtpath.Text.Replace("\n", "<br>"),fileid);
        GridFiles.EditIndex = -1;
        BindData();
    }

    protected void GridFiles_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridFiles.EditIndex = e.NewEditIndex;
        BindData();
    }
}