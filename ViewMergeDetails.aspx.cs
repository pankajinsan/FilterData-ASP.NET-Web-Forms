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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
public partial class ViewMergeDetails : System.Web.UI.Page
{
    //SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLCON"].ConnectionString);
    String[] ar = HttpContext.Current.User.Identity.Name.Split('/');
    public DataTable dtListName = new DataTable();
    string ListAccess = "";
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
            if (txtsearchbyAadharNo.Text != "" || txtsearchbyFormNo.Text != "" || txtserachbysetno.Text != "")
            {
                string qry = "select formno,setno,m.createdon,setnocreatedon,u.EmailId as MergedByUserID,u.Name as MergedByUserName,u.Mobile as MergedByMobileNo,u1.EmailId as SetNoByUserID,u1.Name as SetNoByUserName,u1.Mobile as SetNoByMobileNo from mergetb m left join users u on m.CreatedBy = u.UserID left join users u1 on m.SetNoCreatedBy = u1.UserID where 1 =1";
                MySqlParameter AadharNoP, FormNoP, SetNoP;
                MySqlParameter[] p = { };
                if (txtsearchbyAadharNo.Text != "")
                {
                    qry += " and aadhaar_card=@AadharNo";
                    Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                    AadharNoP = new MySqlParameter("@AadharNo", txtsearchbyAadharNo.Text);
                    p[p.Length - 1] = AadharNoP;
                }
                if (txtsearchbyFormNo.Text != "")
                {
                    qry += " and FormNo=@FormNo";
                    Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                    FormNoP = new MySqlParameter("@FormNo", txtsearchbyFormNo.Text);
                    p[p.Length - 1] = FormNoP;
                }
                if (txtserachbysetno.Text != "")
                {
                    qry += " and SetNo=@SetNo";
                    Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                    SetNoP = new MySqlParameter("@SetNo", txtserachbysetno.Text);
                    p[p.Length - 1] = SetNoP;
                }
                qry += " group by Setno";
                DataTable dt = MySqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["cn"].ToString(), qry, p).Tables[0];

                grvData.DataSource = dt;
                grvData.DataBind();

            }
            else
            {
                Reset();
            }
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
    private void Reset()
    {
        txtsearchbyFormNo.Text = "";
        txtserachbysetno.Text = "";
        txtsearchbyAadharNo.Text = "";
        grvData.DataSource = null;
        grvData.DataBind();
    }
    protected void grdSrchData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnFilterReset_Click(object sender, EventArgs e)
    {
        Refresh();
    }
    private void Refresh()
    {
        txtsearchbyFormNo.Text = "";
        txtserachbysetno.Text = "";
        txtsearchbyAadharNo.Text = "";
        grvData.DataSource = null;
        grvData.DataBind();
    }
    protected void grvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered //
    }

    protected void grvData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvData.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void grvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";
            //Label id = grdSrchData.Rows[e.RowIndex].FindControl("lblID") as Label;
            //string list = (grdSrchData.Rows[e.RowIndex].FindControl("ddlAllList") as DropDownList).SelectedItem.Value;
            //string role = (grdSrchData.Rows[e.RowIndex].FindControl("ddlUserRole") as DropDownList).SelectedItem.Value;
            //string qry = "Update Users set Role='" + role + "',ListAccess='" + list + "' where UserID='" + id.Text + "'";
            //MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry);
            //grdSrchData.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void grvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grvData.EditIndex = -1;
        BindGrid();
    }
}