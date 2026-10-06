using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.Security;
public partial class ManageUsers : System.Web.UI.Page
{
    //SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLCON"].ConnectionString);
    String[] ar = HttpContext.Current.User.Identity.Name.Split('/');
    public DataTable dtListName = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx?UnAuthorizedAccess=1", false);
                return;
            }
            if (ar[0] == "")
            {
                Response.Redirect("~/login.aspx?UnAuthorizedAccess=1", false);
                return;
            }
            if (!(ar[2].ToString() == "Admin" || ar[2].ToString() == "Incharge"))
            {
                Response.Redirect("~/login.aspx?UnAuthorizedAccess=1", false);
                return;
            }
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        catch
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }

    private void BindGrid()
    {
        try
        {
            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";

            string qry = "select * from users where 1=1";
            if(txtsearchbyName.Text!="")
            { qry += " and Name like '" + txtsearchbyName.Text + "%'"; }
            if (txtsearchbyMobileNo.Text != "")
            { qry += " and Mobile='" + txtsearchbyMobileNo.Text + "'"; }
            //ListB-Approver
            qry += " and Role='User' or Role='Incharge' or Role='ListB-Approver'";
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["cn"].ToString(), qry).Tables[0];
            grdSrchData.DataSource = dt;
            grdSrchData.DataBind();



        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void grdSrchData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
            if (ar[2].ToString() == "Incharge")
            { //e.Row.Cells[6].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
            if (ar[2].ToString() == "Incharge")
            { //e.Row.Cells[6].Visible = false; 
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow && grdSrchData.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddlAllList = (DropDownList)e.Row.FindControl("ddlAllList");
            DropDownList ddlUserRole = (DropDownList)e.Row.FindControl("ddlUserRole");
            string qry = "SELECT DISTINCT ListName FROM mergetb";
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            ddlAllList.DataSource = dt;
            ddlAllList.DataTextField = "ListName";
            ddlAllList.DataValueField = "ListName";
            ddlAllList.DataBind();
            ddlAllList.Items.Insert(0, "Disable");
            string selectedCity = DataBinder.Eval(e.Row.DataItem, "ListAccess").ToString();
            if (selectedCity != "")
            {
                ddlAllList.Items.FindByValue(selectedCity).Selected = true;
            }
            string selectedRole = DataBinder.Eval(e.Row.DataItem, "Role").ToString();
            if (selectedRole != "")
            {
                ddlUserRole.Items.FindByValue(selectedRole).Selected = true;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnFilterReset_Click(object sender, EventArgs e)
    {
       
        grdSrchData.DataSource = null;
        grdSrchData.DataBind();
    }

    protected void grdSrchData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited.  
        grdSrchData.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void grdSrchData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";
            Label id = grdSrchData.Rows[e.RowIndex].FindControl("lblID") as Label;
            string list = (grdSrchData.Rows[e.RowIndex].FindControl("ddlAllList") as DropDownList).SelectedItem.Value;
            string role = (grdSrchData.Rows[e.RowIndex].FindControl("ddlUserRole") as DropDownList).SelectedItem.Value;
            string qry = "Update Users set Role='" + role + "',ListAccess='" + list + "' where UserID='" + id.Text + "'";
            MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry);
            grdSrchData.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void grdSrchData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdSrchData.EditIndex = -1;
        BindGrid();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void grdSrchData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSrchData.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}