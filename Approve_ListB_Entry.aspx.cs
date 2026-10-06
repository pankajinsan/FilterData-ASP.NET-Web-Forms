using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
public partial class Approve_ListB_Entry : System.Web.UI.Page
{
   
    String[] ar = HttpContext.Current.User.Identity.Name.Split('/');
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
           
            if (ar[2].ToString() == "User")
            {
                Response.Redirect("~/login.aspx?UnAuthorizedAccess=1", false);
                return;
            }
            pnlsingup.Visible = false;
            if (ar[2].ToString() == "Admin" || ar[2].ToString() == "Incharge")
            {
                DivPendingListB_data.Visible = true;
            }
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
    
 
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            AlertErrorMsg.Visible = false;
            AlertAfterSignUp.Visible = false;
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString());
            MySqlTransaction tran;
            if (con.State == ConnectionState.Closed)
                con.Open();
            tran = con.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                string qry = "insert into listb(name,relation,relation_name,address,district,state,phone,dob,aadhaar_card)"
                                 + "values(@name,@relation,@relation_name,@address,@district,@state,@phone,STR_TO_DATE(@dob, '%d-%m-%Y'),@aadhaar_card)";
                MySqlParameter nameP = new MySqlParameter("@name", txtname.Text);
                MySqlParameter relationP = new MySqlParameter("@relation", ddlRelation.SelectedValue);
                MySqlParameter relation_nameP = new MySqlParameter("@relation_name", txtRelationName.Text);
                MySqlParameter addressP = new MySqlParameter("@address", txtAddress.Text);
                MySqlParameter districtP = new MySqlParameter("@district", txtDistrict.Text);
                MySqlParameter stateP = new MySqlParameter("@state", txtState.Text);
                MySqlParameter phoneP = new MySqlParameter("@phone", txtmobile.Text);
                MySqlParameter dobP = new MySqlParameter("@dob",txtdob.Text);
                MySqlParameter aadhaar_cardP = new MySqlParameter("@aadhaar_card", txtAadharCardNo.Text);
                MySqlParameter[] p = { nameP, relationP, relation_nameP, addressP, districtP, stateP, phoneP, dobP, aadhaar_cardP };
                common.ExecuteNonQuery(con, tran, qry, p);
                string qry1 = "delete from listb_dummy where id='" + HiddenField1.Value + "'";
                common.ExecuteNonQuery(con, tran, qry1);
                tran.Commit();
                Clear();
                BindGrid();
                AlertAfterSignUp.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
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
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Clear();
        lblmsg.Text = "";
    }
    private void Clear()
    {
        try
        {
            txtname.Text = "";
            //txtRelation.Text = "";
            txtRelationName.Text = "";
            txtAddress.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtdob.Text = "";
            txtAadharCardNo.Text = "";
            txtmobile.Text = "";
            HiddenField1.Value = "";
            pnlsingup.Visible = false;
            txtAadharCardNo.Enabled = true;
        }
        catch (Exception ex)
        { }
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
    private void BindGrid()
    {

        try
        {
            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";

            string qry = "select id,name,relation,relation_name,address,district,state,phone,DATE_FORMAT(dob,'%d/%b/%Y') as dob,aadhaar_card from listb_dummy";
            //if(txtsearchbyName.Text!="")
            //{ qry += " and Name like '" + txtsearchbyName.Text + "%'"; }
            //if (txtsearchbyMobileNo.Text != "")
            //{ qry += " and Mobile='" + txtsearchbyMobileNo.Text + "'"; }
            //qry += " and Role='User' or Role='Incharge'";
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["cn"].ToString(), qry).Tables[0];
            grdSrchData.DataSource = dt;
            grdSrchData.DataBind();
            lbltotal.Text = "Total Records: " + dt.Rows.Count;


        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        AlertErrorMsg.Visible = false;
        if (txtAadharCardNo.Text != "")
        {
            string qry = "Select * from listb_dummy where aadhaar_card=@aadhaar_card";
            MySqlParameter aadhaar_cardP = new MySqlParameter("@aadhaar_card", txtAadharCardNo.Text.Trim());
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry, aadhaar_cardP).Tables[0];
            if (dt.Rows.Count > 0)
            {
                pnlsingup.Visible = true;
                txtAadharCardNo.Enabled = false;
                HiddenField1.Value= dt.Rows[0]["id"].ToString();
                txtname.Text = dt.Rows[0]["name"].ToString();
                ddlRelation.SelectedValue = dt.Rows[0]["relation"].ToString();
                txtRelationName.Text = dt.Rows[0]["relation_name"].ToString();
                txtAddress.Text = dt.Rows[0]["address"].ToString();
                txtDistrict.Text = dt.Rows[0]["district"].ToString();
                txtState.Text = dt.Rows[0]["state"].ToString();
                txtdob.Text =Convert.ToDateTime(dt.Rows[0]["dob"]).ToString("dd-MM-yyyy");
                txtmobile.Text = dt.Rows[0]["phone"].ToString();
            }
            else
            {
                pnlsingup.Visible = false;
                txtname.Text = "";
                //txtRelation.Text = "";
                txtRelationName.Text = "";
                txtAddress.Text = "";
                txtDistrict.Text = "";
                txtState.Text = "";
                txtdob.Text = "";
                txtAadharCardNo.Text = "";
                txtmobile.Text = "";
                HiddenField1.Value = "";
                AlertErrorMsg.Visible = true;
                lblmsg.Text = "This Aadhaar Card is Not Found!!";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hideError();", true);
            }
        }
    }

    protected void grdSrchData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSrchData.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}