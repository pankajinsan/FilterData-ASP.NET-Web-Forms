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
public partial class WebPortal_IT_Wing_Signup : System.Web.UI.Page
{
    Signup _signup = new Signup();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
            //int insanno = 0; bool parshadtaken = false;
            if (Regex.IsMatch(txtmobile.Text, "^[0-9]{10,12}$"))
            {
                //it's good
            }
            else
            {
                //it's bad
            }
            //string value = ddlyear.SelectedValue;
           
            bool EmailId = _signup.CheckForDuplicate("users", "EmailId", txtprimaryemail.Text);
            if (EmailId)
            {
                AlertErrorMsg.Visible = true;
                lblmsg.Text = "Email Id Already Exists! ";
            }
            bool MobileNo = _signup.CheckForDuplicate("users", "Mobile", txtmobile.Text);
            if (MobileNo)
            {
                AlertErrorMsg.Visible = true;
                lblmsg.Text = "Mobile No Already Exists! ";
            }


            if (AlertErrorMsg.Visible == false)
            {
                _signup = new Signup(txtname.Text, txtprimaryemail.Text, txtpassword.Text, txtmobile.Text);
                _signup.RegisterSignUp();
                //if (common.GetMailSendConfirmation() == "1")
                //{
                //    Exception mail = new Exception(txtprimaryemail.Text);
                //    ExceptionHandlingClass.SendMail(mail);
                //}
                Clear();
                AlertAfterSignUp.Visible = true;
                pnlsingup.Visible = false;
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

            txtmobile.Text = "";

            txtprimaryemail.Text = "";
            txtpassword.Text = "";
        }
        catch (Exception ex)
        { }
    }
  
}