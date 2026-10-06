using System;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web;
using System.Configuration;
public partial class dss_intranet_login : System.Web.UI.Page
{
    //Signup _user = new Signup();
    MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["cn"].ToString());
    string usrid = "", usrnam = "", usremail = "",ListAccess="", twitterhandle = "", usrgender = ""; int approved, usrsts, usrcountryid, usrstaid, usrdstid, usrblkid; bool isauthorized = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request["Logout"] == "1")
        {
            FailureText.Text = "You Are Successfully Logged Out !";
            //Application["ADMIN_USERS"] = Convert.ToInt16(Application["ADMIN_USERS"]) - 1;
            FormsAuthentication.SignOut();
            Session["usrid"] = "";
            Session["usrnam"] = "";
            Session["usrusrnam"] = "";
            Session["usrsts"] = "";
        }
        if (!IsPostBack && Request["expired"] == "1")
        {
            FailureText.Text = "User Session Failure, Please Relogin !";
        }
        if (!IsPostBack && Request["UnAuthorizedAccess"] == "1")
        {
            FailureText.Text = "You Are Not Authrozied !";
        }
        if (!Page.IsPostBack)
        {
            txtnam.Focus();
        }
        //string path = Request.QueryString["ReturnUrl"];
        //if (path == "/")
        //{
        //   path = null;
        //   Request.QueryString["ReturnUrl"] = path;
        //}
    }
    protected void btnlog_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtnam.Text == "" || txtpwd.Text == "")
            {
                FailureText.Text = "UserName or Password can't be blank";
            }
            else
            {
                string qry = "Select Username,UserID,Role,ListAccess from Users where Username=@username and Password=@password";
                MySqlParameter usernameP = new MySqlParameter("@username", txtnam.Text.Trim());
                MySqlParameter passwordP = new MySqlParameter("@password", txtpwd.Text.Trim());
                MySqlParameter[] p = { usernameP, passwordP };
                DataTable dt = MySqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["cn"].ToString(), qry, p).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    usrnam= dt.Rows[0]["Username"].ToString();
                    usrid = dt.Rows[0]["UserID"].ToString();
                    string UserRole = dt.Rows[0]["Role"].ToString();
                    ListAccess= dt.Rows[0]["ListAccess"].ToString();
                    FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, usrnam + "/" + usrid + "/" + UserRole + "/" + ListAccess, DateTime.Now, DateTime.Now.AddHours(1), false, UserRole, FormsAuthentication.FormsCookiePath);
                    String st = FormsAuthentication.Encrypt(tkt);
                    HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, st);
                    Response.Cookies.Add(ck);
                    if (UserRole == "Admin")
                    {
                        Response.Redirect("upload.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("AutomationMerge.aspx", false);
                    }
                }
                else
                {
                    FailureText.Text = "Wrong UserName or Password";
                    txtpwd.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            FailureText.Text = "Error: " + ex.Message;
        }
    }

    //private string GetUserRole(string userid)
    //{
    //    string rolename = "";
    //    try
    //    {
    //        string qry = "SELECT rolnam from tbusrrol ur inner join tbrol r on r.rolid=ur.usrrolrolid where ur.usrrolusrid =" + userid;
    //        DataTable dt = MySqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["cn"].ConnectionString, qry).Tables[0];
    //        int count = dt.Rows.Count;
    //        if (count > 0)
    //        {
    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                rolename += dr["rolnam"] + ",";
    //            }
    //            rolename = rolename.Remove(rolename.Length - 1);
    //        }
    //        return rolename;
    //    }
    //    catch
    //    {
    //        return "0";
    //    }
    //}
    //private void SaveLogData(string LOGIN_USER_ID,string LOGIN_SITE)
    //{
    //    try
    //    {
    //        string qry = "INSERT INTO tblog(LOGIN_USER_ID,LOGIN_TIME,LOGIN_IP,LOGIN_SITE)VALUES(@LOGIN_USER_ID,now(),@LOGIN_IP,@LOGIN_SITE)";
    //        MyMySqlParameter LOGIN_USER_IDP = new MyMySqlParameter("@LOGIN_USER_ID", LOGIN_USER_ID);
    //        MyMySqlParameter LOGIN_IPP = new MyMySqlParameter("@LOGIN_IP", GetIP());
    //        MyMySqlParameter LOGIN_SITEP = new MyMySqlParameter("@LOGIN_SITE", LOGIN_SITE);
    //        MyMySqlParameter[] p = { LOGIN_USER_IDP, LOGIN_IPP, LOGIN_SITEP };
    //        MySqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["cn"].ConnectionString, qry, p);
    //    }
    //    catch(Exception ex)
    //    {

    //    }
    //}
    //private string GetIP()
    //{
    //    String ip = string.Empty;
    //    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //    if (!string.IsNullOrEmpty(ip))
    //    {
    //        string[] ipRange = ip.Split(',');
    //        Int32 le = ipRange.Length - 1;
    //        ip = ipRange[le] + " via " + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    //    }
    //    else
    //    {
    //        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    //    }
    //    return ip;
    //}

}