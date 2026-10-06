using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
//using nsjameins;
using System.Configuration;
using System.IO;
public partial class WebPortal_main : System.Web.UI.MasterPage
{
   // Signup _singup = new Signup();
    public string gender;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String[] ar = HttpContext.Current.User.Identity.Name.Split('/');
            //Global.GenderData = ar[8];
            //lblusr.Text = ar[0];
            lblusername.Text = ar[0]; lblname.Text = ar[0];
            if (ar[0] == "")
            {
                Response.Redirect("~/login.aspx?UnAuthorizedAccess=1", false);
                return;
            }

            if (ar[2].ToString() == "Admin")
            {
                btnserachreports.Visible = true;
                btnmanageusers.Visible = true;
                btnApproveListB_data.Visible = true;
                btnviewmerged_entry.Visible = true;
                btnupdateemptySetNo.Visible = true;
                btnupdatesetno.Visible = true;
            }
            //ListB-Approver
            if (ar[2].ToString() == "Incharge")
            {
                btnmanageusers.Visible = true;
                btnApproveListB_data.Visible = true;
                btnviewmerged_entry.Visible = true;
                btnupdateemptySetNo.Visible = true;
                btnupdatesetno.Visible = true;
            }
            if (ar[2].ToString() == "ListB-Approver")
            { 
                btnApproveListB_data.Visible = true;
            }
            Profilepicmenu.ImageUrl = "~/img/avatar3.png";
            profilepicside.ImageUrl = "~/img/avatar3.png";

        }
        catch
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
    protected void logout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.RemoveAll();
        //Response.Redirect("../login.aspx", false);
        Response.Redirect("~/login.aspx?Logout=1", false);
        return;
    }
    //private string TodayTotalEntry()
    //{
    //    string total;
    //    string qry = "select count(1) from abhiyanlog where createdon BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 1 day)";
    //    total = MySqlHelper.ExecuteScalar(ConfigurationManager.ConnectionStrings["cn"].ToString(), qry).ToString();
    //    return total;
    //}
}
