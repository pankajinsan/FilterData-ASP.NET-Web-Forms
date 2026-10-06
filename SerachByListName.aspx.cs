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
public partial class SerachByListName : System.Web.UI.Page
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
            if (ar[0] == "" || ar[3] == "")
            {
                Response.Redirect("~/login.aspx?UnAuthorizedAccess=1", false);
                return;
            }
            ListAccess = ar[3].ToString();
            if (ListAccess == "Disable")
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
            if (txtsearchbyAadharNo.Text != "" || txtsearchbySetNo.Text != "")
            {
                string qry = "select * from mergetb where 1=1";
                MySqlParameter AadharNoP, SetNoP;
                MySqlParameter[] p = { };
                if (txtsearchbyAadharNo.Text != "")
                {
                    qry += " and aadhaar_card=@AadharNo";
                    Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                    AadharNoP = new MySqlParameter("@AadharNo", txtsearchbyAadharNo.Text);
                    p[p.Length - 1] = AadharNoP;
                }
                if (txtsearchbySetNo.Text != "")
                {
                    qry += " and FormNo=@SetNo";
                    Array.Resize<MySqlParameter>(ref p, p.Length + 1);
                    SetNoP = new MySqlParameter("@SetNo", txtsearchbySetNo.Text);
                    p[p.Length - 1] = SetNoP;
                }
                //qry += " and ListName='" + ListAccess + "'";
                if (ListAccess != "All")
                {
                    qry += " and ListName='" + ListAccess + "'";
                }

                DataTable dt = MySqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["cn"].ToString(), qry, p).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lblname.Text = dt.Rows[0]["name"].ToString();
                    lbladdress.Text = dt.Rows[0]["address"].ToString();
                    lbldistrict.Text = dt.Rows[0]["district"].ToString();
                    lblstate.Text = dt.Rows[0]["state"].ToString();
                    lblAadharNo.Text = dt.Rows[0]["aadhaar_card"].ToString();
                    lblListName.Text = dt.Rows[0]["ListName"].ToString();
                    lblFormNo.Text = dt.Rows[0]["FormNo"].ToString();
                    hiddenSetNo.Value = dt.Rows[0]["SetNo"].ToString();
                    grvData.DataSource = dt;
                    grvData.DataBind();
                    if(hiddenSetNo.Value!="")
                    {
                        lblsetnumber.Text = hiddenSetNo.Value;
                        lblThankyou.Text = "Already Generated!";
                        lblPopupSucess.Text = "This Set No is Already Generated";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$(function () {");
                        sb.Append(" $('#ModalForm').modal('show');});");
                        sb.Append("</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                    }
                }
                else
                { Reset(); }
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
        lblname.Text = "";
        lbladdress.Text = "";
        lbldistrict.Text = "";
        lblstate.Text = "";
        lblAadharNo.Text = "";
        lblListName.Text = "";
        lblFormNo.Text = "";
        hiddenSetNo.Value = "";
        grvData.DataSource = null;
        grvData.DataBind();
        lblsetnumber.Text = "";
    }
    protected void grdSrchData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
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
        txtsearchbySetNo.Text = "";
        txtsearchbyAadharNo.Text = "";
        lblname.Text = "";
        lbladdress.Text = "";
        lbldistrict.Text = "";
        lblstate.Text = "";
        lblAadharNo.Text = "";
        lblListName.Text = "";
        lblFormNo.Text = "";
        hiddenSetNo.Value = "";
        grvData.DataSource = null;
        grvData.DataBind();
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";
            if (grvData.Rows.Count > 0)
            {
                MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString());
                MySqlTransaction tran;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                tran = con.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    int GetMaxSetNumber = 0;
                    if (hiddenSetNo.Value == "")
                    {
                        GetMaxSetNumber = common.GetMaxSetNumberByListName(lblListName.Text);
                    }
                    else
                    {
                        GetMaxSetNumber = Convert.ToInt32(hiddenSetNo.Value);
                    }
                    foreach (GridViewRow gvrow in grvData.Rows)
                    {
                        var lblID = gvrow.FindControl("lblID") as Label;
                        string qry = "update mergetb set SetNo=@SetNo,SetNoCreatedBy=@SetNoCreatedBy,SetNoCreatedOn=now() where Id=@Id";
                        MySqlParameter SetNoP = new MySqlParameter("@SetNo", GetMaxSetNumber);
                        MySqlParameter IdP = new MySqlParameter("@Id", lblID.Text);
                        MySqlParameter SetNoCreatedByP = new MySqlParameter("@SetNoCreatedBy", ar[1].ToString());
                        MySqlParameter[] p = { SetNoP, IdP, SetNoCreatedByP };
                        common.ExecuteNonQuery(con, tran, qry, p);
                    }
                    tran.Commit();
                    Refresh();
                    lblsetnumber.Text = GetMaxSetNumber.ToString();
                    lblThankyou.Text = "Thank You!";
                    lblPopupSucess.Text = "Data Submitted Successfully";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$(function () {");
                    sb.Append(" $('#ModalForm').modal('show');});");
                    sb.Append("</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                }
                catch (SqlException sqlerror)
                {
                    tran.Rollback();
                    AlertErrorMsg.Visible = true;
                    lblmsg.Text = "Error: " + sqlerror.Message;
                }
                finally
                {
                    con.Close();
                }
            }

        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void grvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered //
    }
}