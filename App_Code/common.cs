using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for common
/// </summary>
public static class common
{

    public static string UserName { get; set; }
    public static string UserIP { get; set; }
    public static string UserMac { get; set; }
    public static string LoginSite { get; set; }
    public static MySqlConnection GetMySqlConnection()
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["Greens_SDB"].ToString());
        return con;

    }
    public static MySqlConnection GetRemoteConnection()
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlServer"].ToString());
        return con;
 
    }
    public static string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["cn"].ToString();
    }
    public static int GetSaleAccount()
    {

        return Convert.ToInt32(ConfigurationManager.AppSettings["SaleAccount"]);
    }
    public static int GetCashAccount()
    {

        return Convert.ToInt32(ConfigurationManager.AppSettings["CashAccount"]);
    }
    public static string GetUploadFolderPath()
    {
        return ConfigurationManager.AppSettings["UploadFilePath"].ToString();
    }
    public static string GetConfirmationFolderPath()
    {
        return ConfigurationManager.AppSettings["ConfirmationFilePath"].ToString();
    }
    public static string GetMergedFolderPath()
    {
        return ConfigurationManager.AppSettings["MergedFilePath"].ToString();
    }
    public static void SaveLogDetail()
    {
        try
        {
            string qry = "insert into applogs.logdetails(username,userip,usermac,loginsite,logintime)values(@username,@userip,@usermac,@loginsite,now())";
            MySqlParameter usernameP = new MySqlParameter("@username", UserName);
            MySqlParameter useripP = new MySqlParameter("@userip", UserIP);
            MySqlParameter usermacP = new MySqlParameter("@usermac", UserMac);
            MySqlParameter loginsiteP = new MySqlParameter("@loginsite", LoginSite);
            MySqlParameter[] p = { usernameP, useripP, usermacP, loginsiteP };
            MySqlHelper.ExecuteNonQuery(GetConnectionString(), qry, p);
        }
        catch
        {
            Exception ex = new Exception("login Error!");
            throw ex;
        }
    }
    public static bool CheckForDuplicate(string tablename, string colname, string colvalue)
    {
        bool result = false;
        try
        {
            string qry = "select " + colname + " from " + tablename + " where " + colname + "='" + colvalue + "'";
            DataRow dr = MySqlHelper.ExecuteDataRow(common.GetConnectionString(), qry);
            if (dr != null)
            {
                result = true;

            }
            return result;
        }
        catch
        {
            Exception ex = new Exception("Error In Check For Duplicate!");
            throw ex;
        }
    }
    public static string RemoveBefore(this string value, string character)
    {
        int index = value.IndexOf(character);
        if (index > 0)
        {
            value = value.Substring(index + 1);
        }
        return value;
    }
    public static string Filter(this string str)
    {
        var charsToRemove = new string[] { "sh.", "Sh." };
        foreach (var c in charsToRemove)
        {
            str = str.Replace(c, string.Empty);
        }

        return str;
    }
    public static int GetMaxSetNumber()
    {
        int MaxSetNumber = 1;
        try
        {
            string qry = "Select Max(FormNo) from mergetb";
            MaxSetNumber = Convert.ToInt32(MySqlHelper.ExecuteScalar(GetConnectionString(), qry)) + 1;
            //qry = "insert into max_number(number)values(@number)";
            //MySqlParameter numberP = new MySqlParameter("@number", MaxSetNumber);
            //MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry, numberP);

            return MaxSetNumber;
        }
        catch(Exception ex)
        {
            //return MaxSetNumber;
            throw ex;
        }
    }
    public static int GetMaxSetNumberByListName(string ListName)
    {
        int MaxSetNumber = 1;
        try
        {
            string qry = "Select Max(SetNo) from mergetb where ListName=@ListName";
            MySqlParameter ListNameP = new MySqlParameter("@ListName", ListName);
            MaxSetNumber = Convert.ToInt32(MySqlHelper.ExecuteScalar(GetConnectionString(), qry, ListNameP)) + 1;
            return MaxSetNumber;
        }
        catch
        {
            return MaxSetNumber;
        }
    }
    public static string addCommas(decimal cash)
    {
        return String.Format("{0:n}", cash);
    }
    public static void CreateCSVfile(DataTable dtable, string strFilePath)
    {
       System.IO.StreamWriter sw = new System.IO.StreamWriter(strFilePath, false);
        int icolcount = dtable.Columns.Count;
        foreach (DataRow drow in dtable.Rows)
        {
            for (int i = 0; i < icolcount; i++)
            {
                if (!Convert.IsDBNull(drow[i]))
                {
                    sw.Write(drow[i].ToString());
                }
                if (i < icolcount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();
        sw.Dispose();
    }
    /// <summary>
    /// Fills DropDownList with DataSet
    /// </summary>
    /// <param name="Control">Target DropDownList Control</param>
    /// <param name="ShowText">Text at first index</param>
    /// <param name="strOther">Other Text in list</param>
    /// <param name="lstText">DataTextField of listbox</param>
    /// <param name="lstValue">DataValueField of listbox</param>
    /// <param name="tableName">Name of the table which is to be bind</param>
    /// <param name="whereId">The name of the column, which will be used in where condition</param>
    public static void FillLists(ListControl Control, string ShowText, string strOther, string lstText, string lstValue, string tableName)
        {
            try
            {
                DataSet ds = new DataSet();
                string qry = "select * from " + tableName + " order by " + lstText;
                ds = MySqlHelper.ExecuteDataset(GetConnectionString(), qry);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Control.DataSource = ds;
                    Control.DataTextField = lstText;
                    Control.DataValueField = lstValue;
                    Control.DataBind();
                }
                else
                {
                    Control.Items.Clear();
                }
                if (ShowText != "")
                {
                    Control.Items.Insert(0, new ListItem(ShowText, ""));
                }
                if (strOther != "")
                {
                    Control.Items.Add(new ListItem(strOther, Convert.ToString(Control.Items.Count + 1)));
                }
            }
            catch (Exception Ex)
            {
                //ExceptionHandlingClass.HandleException(Ex);
            }
        }
    public static void FillListsCountry(ListControl Control, string ShowText, string strOther, string lstText, string lstValue, string tableName)
    {
        try
        {
            DataSet ds = new DataSet();
            string qry = "select * from " + tableName;
            ds = MySqlHelper.ExecuteDataset(GetConnectionString(), qry);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Control.DataSource = ds;
                Control.DataTextField = lstText;
                Control.DataValueField = lstValue;
                Control.DataBind();
            }
            else
            {
                Control.Items.Clear();
            }
            if (ShowText != "")
            {
                Control.Items.Insert(0, new ListItem(ShowText, ""));
            }
            if (strOther != "")
            {
                Control.Items.Add(new ListItem(strOther, Convert.ToString(Control.Items.Count + 1)));
            }

        }
        catch (Exception Ex)
        {
            //ExceptionHandlingClass.HandleException(Ex);
        }
    }

    public static void FillListsQualification(ListControl Control, string ShowText, string strOther, string lstText, string lstValue, string tableName)
    {
        try
        {
            DataSet ds = new DataSet();
            string qry = "select * from " + tableName;
            ds = MySqlHelper.ExecuteDataset(GetConnectionString(), qry);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Control.DataSource = ds;
                Control.DataTextField = lstText;
                Control.DataValueField = lstValue;
                Control.DataBind();
            }
            else
            {
                Control.Items.Clear();
            }
            if (ShowText != "")
            {
                Control.Items.Insert(0, new ListItem(ShowText, ""));
            }
            if (strOther != "")
            {
                Control.Items.Add(new ListItem(strOther, Convert.ToString(Control.Items.Count + 1)));
            }
        }
        catch (Exception Ex)
        {
            //ExceptionHandlingClass.HandleException(Ex);
        }
    }
    public static void FillCampaign(ListControl Control, string ShowText, string strOther, string lstText, string lstValue, string tableName,int campaignid)
    {
        try
        {
            DataSet ds = new DataSet();
            string qry = "select * from " + tableName + " where campaignid='" + campaignid + "'";
            ds = MySqlHelper.ExecuteDataset(GetConnectionString(), qry);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Control.DataSource = ds;
                Control.DataTextField = lstText;
                Control.DataValueField = lstValue;
                Control.DataBind();
            }
            else
            {
                Control.Items.Clear();
            }
            if (ShowText != "")
            {
                Control.Items.Insert(0, new ListItem(ShowText, "0"));
            }
            if (strOther != "")
            {
                Control.Items.Add(new ListItem(strOther, Convert.ToString(Control.Items.Count + 1)));
            }
        }
        catch (Exception Ex)
        {
            //ExceptionHandlingClass.HandleException(Ex);
        }
    }
    public static void FillUserList(ListControl Control, string ShowText, string strOther, string lstText, string lstValue, string tableName)
    {
        try
        {
            DataSet ds = new DataSet();
            string qry = "select * from " + tableName;
            ds = MySqlHelper.ExecuteDataset(GetRemoteConnection(), qry);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Control.DataSource = ds;
                Control.DataTextField = lstText;
                Control.DataValueField = lstValue;
                Control.DataBind();
            }
            else
            {
                Control.Items.Clear();
            }
            if (ShowText != "")
            {
                Control.Items.Insert(0, new ListItem(ShowText, "0"));
            }
            if (strOther != "")
            {
                Control.Items.Add(new ListItem(strOther, Convert.ToString(Control.Items.Count + 1)));
            }
        }
        catch (Exception Ex)
        {
            //ExceptionHandlingClass.HandleException(Ex);
        }
    }
    /// <summary>
    /// Fills DropDownList with DataSet
    /// </summary>
    /// <param name="Control">Target DropDownList Control</param>
    /// <param name="ShowText">Text at first index</param>
    /// <param name="strOther">Other Text in list</param>
    /// <param name="lstText">DataTextField of listbox</param>
    /// <param name="lstValue">DataValueField of listbox</param>
    /// <param name="tableName">Name of the table which is to be bind</param>
    /// <param name="whereId">The name of the column, which will be used in where condition</param>
    public static void FillLists(ListControl Control, string ShowText, string strOther, string lstText, string lstValue, string tableName, string whereId, int whereValue)
    {
        try
        {
            DataSet ds = new DataSet();
            string qry = "select * from " + tableName + " where " + whereId + "= @wherevalue order by " + lstText;
            MySqlParameter wherevalueP = new MySqlParameter("@wherevalue", whereValue);
            ds = MySqlHelper.ExecuteDataset(GetConnectionString(), qry, wherevalueP);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Control.DataSource = ds;
                Control.DataTextField = lstText;
                Control.DataValueField = lstValue;
                Control.DataBind();
            }
            else
            {
                Control.Items.Clear();
            }
            if (ShowText != "")
            {
                Control.Items.Insert(0, new ListItem(ShowText, ""));
            }
            if (strOther != "")
            {
                Control.Items.Add(new ListItem(strOther, Convert.ToString(Control.Items.Count + 1)));
            }
        }
        catch (Exception Ex)
        {
            //ExceptionHandlingClass.HandleException(Ex);
        }
    }
    public static int ExecuteNonQuery(MySqlConnection connection, MySqlTransaction tran, string commandText, params MySqlParameter[] commandParameters)
    {
        //create a command and prepare it for execution
        MySqlCommand cmd = new MySqlCommand();
        cmd.Transaction = tran;
        cmd.Connection = connection;
        cmd.CommandText = commandText;
        cmd.CommandType = CommandType.Text;

        if (commandParameters != null)
            foreach (MySqlParameter p in commandParameters)
                cmd.Parameters.Add(p);

        int result = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();

        return result;
    }
}