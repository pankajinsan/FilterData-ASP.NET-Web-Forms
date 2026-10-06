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
using System.Web.Services;
public partial class AutomationMerge : System.Web.UI.Page
{
    //SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLCON"].ConnectionString);
    String[] ar = HttpContext.Current.User.Identity.Name.Split('/');
    private static DataTable DtListA = new DataTable();
    //private static List<ListA> customers = new List<ListA>();
    //private static DataTable AADHARCARDDT = new DataTable();
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
            if (!IsPostBack)
            {
                DataTable dt = new DataTable("PankajGargInsan");
                dt.Columns.Add(new DataColumn("ListID", typeof(int)));
                dt.Columns.Add(new DataColumn("ListBID", typeof(int)));
                dt.Columns.Add(new DataColumn("ref_no", typeof(string)));
                dt.Columns.Add(new DataColumn("date", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("name", typeof(string)));
                dt.Columns.Add(new DataColumn("relation", typeof(string)));
                dt.Columns.Add(new DataColumn("relation_name", typeof(string)));
                dt.Columns.Add(new DataColumn("address", typeof(string)));
                dt.Columns.Add(new DataColumn("district", typeof(string)));
                dt.Columns.Add(new DataColumn("state", typeof(string)));
                dt.Columns.Add(new DataColumn("phone", typeof(string)));
                dt.Columns.Add(new DataColumn("aadhaar_card", typeof(string)));
                dt.Columns.Add(new DataColumn("No_of_Forms", typeof(string)));
                dt.Columns.Add(new DataColumn("ListName", typeof(string)));
                Session["DataTable"] = dt;
                grvData.DataSource = dt;
                grvData.DataBind();
                


                //DataTable dummy = new DataTable();
                //dummy.Columns.Add("id");
                //dummy.Columns.Add("ref_no");
                //dummy.Columns.Add("date");
                //dummy.Columns.Add("name");
                //dummy.Columns.Add("fathers_name");
                //dummy.Columns.Add("address");
                //dummy.Columns.Add("city");
                //dummy.Columns.Add("district");
                //dummy.Columns.Add("state");
                //dummy.Columns.Add("mobile_no");
                //dummy.Columns.Add("No_of_Forms");
                //dummy.Columns.Add("ListName");
                //dummy.Rows.Add();
                //grdSrchData.DataSource = dummy;
                //grdSrchData.DataBind();

                ////Required for jQuery DataTables to work.
                //grdSrchData.UseAccessibleHeader = true;
                //grdSrchData.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
   
    //[WebMethod]
    //public static List<ListA> GetCustomers()
    //{
       
    //    string constr = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
    //    using (MySqlConnection con = new MySqlConnection(constr))
    //    {
    //        // name like '%Jiwan%' and fathers_name like '%prem singh%' and district like '%Barnala%' and state like '%Punjab%' and
    //        using (MySqlCommand cmd = new MySqlCommand("SELECT id,ref_no,date,name,fathers_name,address,city,district,state,mobile_no,No_of_Forms,ListName FROM lista where IsDeleted=0", con))
    //        {
    //            con.Open();
    //            using (MySqlDataReader sdr = cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    customers.Add(new ListA
    //                    {
    //                        id = sdr["id"].ToString(),
    //                        ref_no = sdr["ref_no"].ToString(),
    //                        name = sdr["name"].ToString(),
    //                        date = sdr["date"].ToString(),
    //                        fathers_name = sdr["fathers_name"].ToString(),
    //                        address = sdr["address"].ToString(),
    //                        city = sdr["city"].ToString(),
    //                        district = sdr["district"].ToString(),
    //                        state = sdr["state"].ToString(),
    //                        mobile_no = sdr["mobile_no"].ToString(),
    //                        No_of_Forms = sdr["No_of_Forms"].ToString(),
    //                        ListName = sdr["ListName"].ToString(),


    //                    });
    //                }
    //            }
    //            con.Close();
    //        }
    //    }
      
    //    return customers;
    //}
    private DataTable SearchDataByAAdharCardNo(string name,string fathername,string district,string state)
    {
        DataTable dt = new DataTable();
        try
        {

            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";
            DtListA.Clear();
            string qry = "SELECT Id,ref_no,date,name,fathers_name,address,city,district,state,mobile_no,No_of_Forms,ListName FROM lista ";
            qry += "where name like '%" + name + "%' and IsDeleted=0";
            DtListA = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            //and fathers_name like '%" + fathername + "%' and district like '%" + district + "%' and state like '%" + state + "%' and
            if (name != "" && fathername != "" && district != "" && state != "")
            {
                if (CheckBox2.Checked)
                {
                    try
                    {
                        dt = (from emp in DtListA.AsEnumerable()
                              where emp.Field<string>("name").Contains(name) && emp.Field<string>("fathers_name").Contains(fathername) && emp.Field<string>("state").Contains(state)
                              select emp).CopyToDataTable();
                    }
                    catch
                    {
                        dt = (from emp in DtListA.AsEnumerable()
                              where emp.Field<string>("name").Contains(name) && emp.Field<string>("state").Contains(state)
                              select emp).CopyToDataTable();
                    }


                    if (name != "" && fathername == "" && district != "" && state != "")
                    {
                        dt = (from emp in DtListA.AsEnumerable()
                              where emp.Field<string>("name").Contains(name) && emp.Field<string>("state").Contains(state)
                              select emp).CopyToDataTable();
                    }
                    else
                    {
                        grdSrchData.DataSource = null;
                        grdSrchData.DataBind();
                    }
                    return dt;
                }
                else
                {
                    try
                    {
                        dt = (from emp in DtListA.AsEnumerable()
                              where emp.Field<string>("name").Contains(name) && emp.Field<string>("fathers_name").Contains(fathername) && emp.Field<string>("state").Contains(state) && emp.Field<string>("district").Contains(district)
                              select emp).CopyToDataTable();
                    }
                    catch
                    {
                        dt = (from emp in DtListA.AsEnumerable()
                              where emp.Field<string>("name").Contains(name) && emp.Field<string>("state").Contains(state) && emp.Field<string>("district").Contains(district)
                              select emp).CopyToDataTable();
                    }


                    if (name != "" && fathername == "" && district != "" && state != "")
                    {
                        dt = (from emp in DtListA.AsEnumerable()
                              where emp.Field<string>("name").Contains(name) && emp.Field<string>("state").Contains(state) && emp.Field<string>("district").Contains(district)
                              select emp).CopyToDataTable();


                    }
                    else
                    {
                        grdSrchData.DataSource = null;
                        grdSrchData.DataBind();
                    }
                    return dt;
                }
               
            }
            return dt;
        }
        catch (Exception ex)
        {
            //AlertErrorMsg.Visible = true;
            //lblmsg.Text = "Error: " + ex.Message;
            grdSrchData.DataSource = null;
            grdSrchData.DataBind();
            return dt;
        }
    }
    protected void grdSrchData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void btnfindAAdhar_Click(object sender, EventArgs e)
    {
        try
        {
            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";
            if (txtAadharCard.Text != "")
            {
                string qry12 = "select distinct ListName,FormNo from MergeTb where aadhaar_card=@aadhaarcard";

                string qry = "select * from LISTB where aadhaar_card=@aadhaarcard and IsDeleted=0 limit 1";
                MySqlParameter aadhaarcardP = new MySqlParameter("@aadhaarcard", txtAadharCard.Text.Trim());
                DataTable Mergedt = MySqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["cn"].ToString(), qry12, aadhaarcardP).Tables[0];
                if (Mergedt.Rows.Count == 0)
                {
                    DataTable dt = MySqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["cn"].ToString(), qry, aadhaarcardP).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string fullName = dt.Rows[0]["name"].ToString();
                        var names = fullName.Split(' ');
                        string firstName = names[0];
                        DataTable NewDT = SearchDataByAAdharCardNo(firstName, dt.Rows[0]["relation_name"].ToString(), dt.Rows[0]["district"].ToString(), dt.Rows[0]["state"].ToString());
                        if (NewDT.Rows.Count > 0)
                        {
                            grdSrchData.DataSource = NewDT;
                            grdSrchData.DataBind();
                            grvAadharVCard.DataSource = dt;
                            grvAadharVCard.DataBind();
                        }
                        else
                        {
                            NewDT = SearchDataByAAdharCardNo(dt.Rows[0]["name"].ToString(), "", dt.Rows[0]["district"].ToString(), dt.Rows[0]["state"].ToString());
                            if (NewDT.Rows.Count > 0)
                            {
                                grdSrchData.DataSource = NewDT;
                                grdSrchData.DataBind();
                                grvAadharVCard.DataSource = dt;
                                grvAadharVCard.DataBind();
                            }
                            else
                            {
                                //string fullName = dt.Rows[0]["name"].ToString();
                                //var names = fullName.Split(' ');
                                //string firstName = names[0];
                                //string lastName = names[1];
                                NewDT = SearchDataByAAdharCardNo(firstName, "", dt.Rows[0]["district"].ToString(), dt.Rows[0]["state"].ToString());
                                if (NewDT.Rows.Count > 0)
                                {
                                    grdSrchData.DataSource = NewDT;
                                    grdSrchData.DataBind();
                                    grvAadharVCard.DataSource = dt;
                                    grvAadharVCard.DataBind();
                                }
                                else
                                {
                                    grdSrchData.DataSource = null;
                                    grdSrchData.DataBind();
                                    grvAadharVCard.DataSource = null;
                                    grvAadharVCard.DataBind();
                                }
                            }
                        }
                    }
                    else
                    {
                        grdSrchData.DataSource = null;
                        grdSrchData.DataBind();
                        grvAadharVCard.DataSource = null;
                        grvAadharVCard.DataBind();
                    }
                }
                else
                {
                    lblSucess.Text = "";
                    string ListType = "";
                    foreach (DataRow dr in Mergedt.Rows)
                    {
                        ListType += dr["ListName"] + ",";
                    }
                    ListType = ListType.Remove(ListType.Length - 1, 1);
                   
                    AlertAfterSignUp.Visible = true;
                    lblSucess.Text = "This Data is Already Generated with ListType: " + ListType + " and FormNo: " + Mergedt.Rows[0]["FormNo"].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
                }

            }
            else
            {
                grdSrchData.DataSource = null;
                grdSrchData.DataBind();
                grvAadharVCard.DataSource = null;
                grvAadharVCard.DataBind();
            }
           
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void grvAadharVCard_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void btnMerge_Click(object sender, EventArgs e)
    {
        try
        {
            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";
            int i = 0;
            if (grdSrchData.Rows.Count > 0 && grvAadharVCard.Rows.Count > 0)
            {
                DataTable dt = (DataTable)Session["DataTable"];
                dt.Clear();
                foreach (GridViewRow gvrow in grdSrchData.Rows)
                {
                    var checkbox = gvrow.FindControl("CheckBox1") as CheckBox;
                    if (checkbox.Checked)
                    {
                        i++;
                    }
                }
                if (i > 0)
                {


                    lblname.Text = grvAadharVCard.Rows[0].Cells[1].Text.ToString();
                    lbladdress.Text = grvAadharVCard.Rows[0].Cells[4].Text.ToString();
                    lbldistrict.Text = grvAadharVCard.Rows[0].Cells[5].Text.ToString();
                    lblstate.Text = grvAadharVCard.Rows[0].Cells[6].Text.ToString();
                    lblAadharNo.Text = grvAadharVCard.Rows[0].Cells[9].Text.ToString();

                    foreach (GridViewRow gvrow in grdSrchData.Rows)
                    {
                        var checkbox = gvrow.FindControl("CheckBox1") as CheckBox;
                        if (checkbox.Checked)
                        {
                            var lblID = gvrow.FindControl("lblID") as Label;
                            DataRow rd = dt.NewRow();
                            rd["ListID"] = lblID.Text;
                            rd["ref_no"] = gvrow.Cells[2].Text.ToString();
                            rd["date"] = Convert.ToDateTime(gvrow.Cells[3].Text);
                            rd["name"] = gvrow.Cells[4].Text.ToString();
                            rd["ListBID"] = Convert.ToInt32(grvAadharVCard.Rows[0].Cells[0].Text); //AadharCardDT
                            rd["relation"] = grvAadharVCard.Rows[0].Cells[2].Text.ToString(); //AadharCardDT
                            rd["relation_name"] = grvAadharVCard.Rows[0].Cells[3].Text.ToString(); //AadharCardDT
                            rd["address"] = grvAadharVCard.Rows[0].Cells[4].Text.ToString(); //AadharCardDT
                            rd["district"] = grvAadharVCard.Rows[0].Cells[5].Text.ToString(); ////AadharCardDT
                            rd["state"] = grvAadharVCard.Rows[0].Cells[6].Text.ToString(); ////AadharCardDT
                            rd["phone"] = grvAadharVCard.Rows[0].Cells[7].Text.ToString();////AadharCardDT
                            rd["aadhaar_card"] = grvAadharVCard.Rows[0].Cells[9].Text.ToString();
                            rd["No_of_Forms"] = gvrow.Cells[11].Text.ToString();
                            rd["ListName"] = gvrow.Cells[12].Text.ToString();
                            dt.Rows.Add(rd);

                        }
                    }
                    dt.DefaultView.Sort = "ListName";
                    dt = dt.DefaultView.ToTable();
                    grvData.DataSource = dt;
                    grvData.DataBind();
                    foreach (GridViewRow gvrow in grdSrchData.Rows)
                    {
                        var checkbox = gvrow.FindControl("CheckBox1") as CheckBox;
                        checkbox.Checked = false;
                    }


                }
                else
                {
                    AlertErrorMsg.Visible = true;
                    lblmsg.Text = "Error: Select some Rows for Processing!";
                }
            }
            else
            {
                AlertErrorMsg.Visible = true;
                lblmsg.Text = "Error: No data found for Processing!";
            }

        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
    private void refreshdata()
    {
        RefreshFilter();
        txtAadharCard.Text = "";
        grdSrchData.DataSource = null;
        grdSrchData.DataBind();
        grvAadharVCard.DataSource = null;
        grvAadharVCard.DataBind();
        DataTable dt = (DataTable)Session["DataTable"];
        dt.Clear();
        grvData.DataSource = null;
        grvData.DataBind();
        lblname.Text = "";
        lbladdress.Text = "";
        lbldistrict.Text = "";
        lblstate.Text = "";
        lblAadharNo.Text = "";
        txtfilter.Text = "";
        DtListA.Clear();
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
                    int GetMaxSetNumber = common.GetMaxSetNumber();
                    DataTable dt = (DataTable)Session["DataTable"];
                    string qry = "";
                    string LISTID = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        qry = "insert into MergeTB(ref_no,date,name,relation,relation_name,address,district,state,phone,aadhaar_card,No_of_Forms,CreatedBy,CreatedOn,ListName,FormNo)values(@ref_no,@date,@name,@relation,@relation_name,@address,@district,@state,@phone,@aadhaar_card,@No_of_Forms,@CreatedBy,now(),@ListName,@SetNumber)";
                        MySqlParameter ref_noP = new MySqlParameter("@ref_no", dr["ref_no"].ToString());
                        MySqlParameter dateP = new MySqlParameter("@date", Convert.ToDateTime(dr["date"]));
                        MySqlParameter nameP = new MySqlParameter("@name", dr["name"].ToString());
                        MySqlParameter relationP = new MySqlParameter("@relation", dr["relation"].ToString());
                        MySqlParameter relation_nameP = new MySqlParameter("@relation_name", dr["relation_name"].ToString());
                        MySqlParameter addressP = new MySqlParameter("@address", dr["address"].ToString());
                        MySqlParameter districtP = new MySqlParameter("@district", dr["district"].ToString());
                        MySqlParameter stateP = new MySqlParameter("@state", dr["state"].ToString());
                        MySqlParameter phoneP = new MySqlParameter("@phone", dr["phone"].ToString());
                        MySqlParameter aadhaar_cardP = new MySqlParameter("@aadhaar_card", dr["aadhaar_card"].ToString());
                        MySqlParameter No_of_FormsP = new MySqlParameter("@No_of_Forms", dr["No_of_Forms"].ToString());
                        MySqlParameter CreatedByP = new MySqlParameter("@CreatedBy", ar[1].ToString());
                        MySqlParameter ListNameP = new MySqlParameter("@ListName", dr["ListName"].ToString());
                        MySqlParameter SetNumberP = new MySqlParameter("@SetNumber", GetMaxSetNumber);
                        MySqlParameter[] p = { ref_noP, dateP, nameP, relationP, relation_nameP, addressP, districtP, stateP, phoneP, aadhaar_cardP, No_of_FormsP, CreatedByP, ListNameP, SetNumberP };
                        common.ExecuteNonQuery(con, tran, qry, p);
                        LISTID += dr["ListID"].ToString() + ",";
                        //qry = "update LISTA set IsDeleted=1 where id='" + dr["ListID"].ToString() + "'";
                        //common.ExecuteNonQuery(con, tran, qry);
                    }
                    LISTID= LISTID.Remove(LISTID.Length - 1, 1);
                    qry = "update LISTA set IsDeleted=1 where id IN(" + LISTID + ")";
                    common.ExecuteNonQuery(con, tran, qry);
                    //
                    qry = "update LISTB set IsDeleted=1 where id='" + dt.Rows[0]["ListBID"].ToString() + "'";
                    common.ExecuteNonQuery(con, tran, qry);
                    //qry = "delete from max_number where number='" + GetMaxSetNumber + "'";
                    //common.ExecuteNonQuery(con, tran, qry);
                    tran.Commit();
                    refreshdata();
                    //AlertAfterSignUp.Visible = true;
                    //lblSucess.Text = "Data Submitted successfully!!";
                   // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
                    lblsetnumber.Text = GetMaxSetNumber.ToString();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$(function () {");
                    sb.Append(" $('#ModalForm').modal('show');});");
                    sb.Append("</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModelScript", sb.ToString(), false);
                    

                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowModelPopUP();", true);
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
            else
            {
                AlertErrorMsg.Visible = true;
                lblmsg.Text = "Error: No data found for Submit!";
            }
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        refreshdata();
    }

    protected void btnFilterReset_Click(object sender, EventArgs e)
    {
        RefreshFilter();
    }
    private void RefreshFilter()
    {
        //txtsearchbyName.Text = string.Empty;
        //txtsearchbyState.Text = string.Empty;
        //txtsearchbyDistrict.Text = string.Empty;
        //txtsearchbyAddress.Text = string.Empty;
    }

    protected void grvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Font.Bold = true;
            if (e.Row.Cells[10].Text == "List1")
            {
                //e.Row.Font.Bold = false;
                e.Row.BackColor = System.Drawing.Color.Yellow;

            }
            if (e.Row.Cells[10].Text == "List2")
            {
                //e.Row.Font.Bold = false;
                e.Row.ForeColor = System.Drawing.Color.White;
                e.Row.BackColor = System.Drawing.Color.Green;

            }
            if (e.Row.Cells[10].Text == "List3")
            {
                //e.Row.Font.Bold = false;
                e.Row.BackColor = System.Drawing.Color.Yellow;

            }
            if (e.Row.Cells[10].Text == "List4")
            {
                //e.Row.Font.Bold = false;
                e.Row.ForeColor = System.Drawing.Color.White;
                e.Row.BackColor = System.Drawing.Color.Green;

            }
            if (e.Row.Cells[10].Text == "List5")
            {
                //e.Row.Font.Bold = false;
                e.Row.BackColor = System.Drawing.Color.Yellow;

            }
            if (e.Row.Cells[10].Text == "List6")
            {
                //e.Row.Font.Bold = false;
                e.Row.ForeColor = System.Drawing.Color.White;
                e.Row.BackColor = System.Drawing.Color.Green;

            }
            if (e.Row.Cells[10].Text == "List7")
            {
                //e.Row.Font.Bold = false;
                e.Row.BackColor = System.Drawing.Color.Yellow;

            }
            if (e.Row.Cells[10].Text == "List8")
            {
                //e.Row.Font.Bold = false;
                e.Row.ForeColor = System.Drawing.Color.White;
                e.Row.BackColor = System.Drawing.Color.Green;

            }
            if (e.Row.Cells[10].Text == "List9")
            {
                //e.Row.Font.Bold = false;
                e.Row.BackColor = System.Drawing.Color.Yellow;

            }
            if (e.Row.Cells[10].Text == "List10")
            {
                //e.Row.Font.Bold = false;
                e.Row.ForeColor = System.Drawing.Color.White;
                e.Row.BackColor = System.Drawing.Color.Green;

            }

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
    }

    protected void btnRest_Click(object sender, EventArgs e)
    {
        txtAadharCard.Text = "";
        grdSrchData.DataSource = null;
        grdSrchData.DataBind();
        grvAadharVCard.DataSource = null;
        grvAadharVCard.DataBind();
        lblSucess.Text = "";
        AlertAfterSignUp.Visible = false;
    }
}