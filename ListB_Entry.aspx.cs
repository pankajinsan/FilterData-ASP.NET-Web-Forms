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
public partial class WebPortal_IT_Wing_ListB_Entry : System.Web.UI.Page
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
            pnlsingup.Visible = false;
            if (!IsPostBack)
            {
              
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
    
    private List<ListItem> FillNaamYear()
    {
        bool flag = true;
        var yearsList = new List<ListItem>();
        int year = 1948;
        //yearsList.Items.Insert(0, new ListItem(ShowText, ""));
        yearsList.Add(new ListItem("Select", ""));
        while (flag)
        {
            yearsList.Add(new ListItem(year.ToString()));
            year++;
            //string qry = "insert into tbyear(year)values('" + year + "')";
            //MySqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["cn"].ConnectionString, qry);
            if (year > DateTime.Now.Year)
                flag = false;
        }
        return yearsList;
    }
   
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";
            AlertErrorMsg.Visible = false;
            AlertAfterSignUp.Visible = false;
            string qry = "insert into listb_dummy(name,relation,relation_name,address,district,state,phone,dob,aadhaar_card)"
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
            MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, p);
            Clear();
            AlertAfterSignUp.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
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
            pnlsingup.Visible = false;
            txtAadharCardNo.Enabled = true;
        }
        catch (Exception ex)
        { }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        AlertErrorMsg.Visible = false;
        AlertAfterSignUp.Visible = false;
        if (txtAadharCardNo.Text != "")
        {
            string qry = "Select aadhaar_card from ListB where aadhaar_card=@aadhaar_card";
            MySqlParameter aadhaar_cardP = new MySqlParameter("@aadhaar_card", txtAadharCardNo.Text.Trim());
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry, aadhaar_cardP).Tables[0];
            if (dt.Rows.Count > 0)
            {
                pnlsingup.Visible = false;
                AlertErrorMsg.Visible = true;
                lblmsg.Text = "This Aadhaar Card is Already Exists!!";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hideError();", true);
            }
            else
            {
                qry = "Select aadhaar_card from listb_dummy where aadhaar_card=@aadhaar_card";
                dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry, aadhaar_cardP).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    pnlsingup.Visible = false;
                    AlertErrorMsg.Visible = true;
                    lblmsg.Text = "This Aadhaar Card is Already Exists!!";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hideError();", true);
                }
                else
                {
                    pnlsingup.Visible = true;
                    txtAadharCardNo.Enabled = false;
                }
            }
        }
    }
}