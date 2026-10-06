using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
//using nsjameins;
//using System.Web.Security;
using MySql.Data.MySqlClient;
using System.Configuration;
public partial class WebPortal_Single_page : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        }
        catch
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}
