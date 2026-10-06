using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Web.Security;
using System.Globalization;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Collections;
using Ionic.Zip;
public partial class upload : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    //SqlConnection sqlcon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLCON"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String[] ar = HttpContext.Current.User.Identity.Name.Split('/');

            if (ar[0] == "")
            {
                Response.Redirect("~/login.aspx?UnAuthorizedAccess=1", false);
                return;
            }

            if (!(ar[2].ToString() == "Admin"))
            {
                Response.Redirect("~/login.aspx?UnAuthorizedAccess=1", false);
                return;
            }
            if (!IsPostBack)
            {
                BindALLDLL();
                BindALLDLLforpdf();
                BindTotalCount();
                Fill_GV();

            }

        }
        catch
        {
            FormsAuthentication.RedirectToLoginPage();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    private void BindALLDLL()
    {
        string qry = "SELECT ListName FROM listname";
        DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
        ddlAllList.DataSource = dt;
        ddlAllList.DataTextField = "ListName";
        ddlAllList.DataValueField = "ListName";
        ddlAllList.DataBind();
        ddlAllList.Items.Insert(0, "All");
    }
    private void BindALLDLLforpdf()
    {
        string qry = "SELECT ListName FROM listname";
        DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
        ddllsitforpdf.DataSource = dt;
        ddllsitforpdf.DataTextField = "ListName";
        ddllsitforpdf.DataValueField = "ListName";
        ddllsitforpdf.DataBind();
        //for upload Signature Pdf files
        ddlconfimrationlisttype.DataSource = dt;
        ddlconfimrationlisttype.DataTextField = "ListName";
        ddlconfimrationlisttype.DataValueField = "ListName";
        ddlconfimrationlisttype.DataBind();

        // ddllisttypeformergedpdf
        ddllisttypeformergedpdf.DataSource = dt;
        ddllisttypeformergedpdf.DataTextField = "ListName";
        ddllisttypeformergedpdf.DataValueField = "ListName";
        ddllisttypeformergedpdf.DataBind();

        //ddllistformissingaadharpdf
        ddllistformissingaadharpdf.DataSource = dt;
        ddllistformissingaadharpdf.DataTextField = "ListName";
        ddllistformissingaadharpdf.DataValueField = "ListName";
        ddllistformissingaadharpdf.DataBind();

    }
    public System.Data.DataTable xlsInsert(string pth)
    {
        string strcon = string.Empty;

        if (Path.GetExtension(pth).ToLower().Equals(".xls"))
        {

            //read a 97-2003 file
            strcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                            + pth +
                            @";Extended Properties=" + Convert.ToChar(34).ToString() + @"Excel 8.0;Imex=1;HDR=YES;" + Convert.ToChar(34).ToString();
        }
        //read a 2007 file
        else if (Path.GetExtension(pth).ToLower().Equals(".xlsx"))
        {
            strcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                          + pth +
                         // @";Extended Properties=" + Convert.ToChar(34).ToString() + @"Excel 12.0;Imex=1HDR=YES;" + Convert.ToChar(34).ToString();
                         @";Extended Properties=" + Convert.ToChar(34).ToString() + @"Excel 12.0;HDR=YES;IMEX=1;" + Convert.ToChar(34).ToString();
        }
        else
        {
            lblmsg.Text = "Pls Check file Extension";
        }
        //string strselect = "Select * from [Sheet1$]";

        DataTable dtExcelRecords = new DataTable();
        OleDbConnection excelCon = new OleDbConnection(strcon);

        try
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = excelCon;
            excelCon.Open();
            OleDbDataAdapter exDA = new OleDbDataAdapter(cmd);
            DataTable dtExcelSheetName = excelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
            cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
            exDA.SelectCommand = cmd;
            exDA.Fill(dtExcelRecords);

        }
        catch (OleDbException oledb)
        {
            throw new Exception("OLEDB Error: " + oledb.Message.ToString());
        }
        finally
        {
            excelCon.Close();
        }
        //for (int i = 0; i < exDT.Rows.Count; i++)
        //{
        //    // Check if first column is empty
        //    // If empty then delete such record
        //    if (exDT.Rows[i]["Name"].ToString() == string.Empty)
        //    {
        //        exDT.Rows[i].Delete();
        //    }
        //}
        //exDT.AcceptChanges();  // refresh rows changes
        //lblprint.Text = dtExcelRecords.Rows.Count.ToString();
        if (dtExcelRecords.Rows.Count == 0)
        {
            throw new Exception("File uploaded has no record found.");
        }
        return dtExcelRecords;
        //}

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        UploadData();

    }
    //List<char> charsToRemove = new List<char>() { '@', '_', ',', '.' };

    private void UploadData()
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        AlertAfterSignUp.Visible = false;
        lblSucess.Text = "";
        try
        {
            if (fileuploadExcel.HasFile)
            {
                string fleUpload = Path.GetExtension(fileuploadExcel.FileName.ToString());
                if (fleUpload.Trim().ToLower() == ".xls" | fleUpload.Trim().ToLower() == ".xlsx")
                {
                    // Save excel file into Server sub dir  
                    // to catch excel file downloading permission    
                    if (!Directory.Exists(Server.MapPath("~/MyFolder/")))
                        Directory.CreateDirectory(Server.MapPath("~/MyFolder/"));
                    string[] files = Directory.GetFiles(Server.MapPath("~/MyFolder/"));
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                    fileuploadExcel.SaveAs(Server.MapPath("~/MyFolder/" + fileuploadExcel.FileName.ToString()));
                    string uploadedFile = (Server.MapPath("~/MyFolder/" + fileuploadExcel.FileName.ToString()));
                    // string uploadedFile = "~/MyFolder/" + fileuploadExcel.FileName.ToString();
                    MySqlConnection con = new MySqlConnection(common.GetConnectionString());
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    MySqlTransaction tran = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {

                        //  string strFile = "/MyFolder/LISTAlessDataCSV_1.csv";
                        dt.Clear();
                        dt = xlsInsert(uploadedFile);
                        if (ddlListType.SelectedValue.ToString() == "ListA")
                        {
                            //string modby = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();
                            foreach (DataRow dr in dt.Rows)
                            {
                                string ref_no = dr["ref_no"].ToString();
                                DateTime date = Convert.ToDateTime(dr["date"]);
                                string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["name"].ToString().ToLower());
                                string fathers_name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["fathers_name"].ToString().ToLower());
                                fathers_name = common.Filter(fathers_name).Trim();
                                string address = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["address"].ToString().ToLower());
                                string city = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["city"].ToString().ToLower());
                                string district = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["district"].ToString().ToLower());
                                string state = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["state"].ToString().ToLower());
                                string mobile_no = dr["mobile_no"].ToString();
                                string No_of_Forms = dr["No_of_Forms"].ToString();
                                string ListName = dr["ListName"].ToString();
                                string qry = "insert into lista(ref_no,date,name,fathers_name,address,city,district,state,mobile_no,No_of_Forms,ListName)values(@ref_no,@date,@name,@fathers_name,@address,@city,@district,@state,@mobile_no,@No_of_Forms,@ListName)";
                                MySqlParameter ref_noP = new MySqlParameter("@ref_no", ref_no);
                                MySqlParameter dateP = new MySqlParameter("@date", date);
                                MySqlParameter nameP = new MySqlParameter("@name", name);
                                MySqlParameter fathers_nameP = new MySqlParameter("@fathers_name", fathers_name);
                                MySqlParameter addressP = new MySqlParameter("@address", address);
                                MySqlParameter cityP = new MySqlParameter("@city", city);
                                MySqlParameter districtP = new MySqlParameter("@district", district);
                                MySqlParameter stateP = new MySqlParameter("@state", state);
                                MySqlParameter mobile_noP = new MySqlParameter("@mobile_no", mobile_no);
                                MySqlParameter No_of_FormsP = new MySqlParameter("@No_of_Forms", No_of_Forms);
                                MySqlParameter ListNameP = new MySqlParameter("@ListName", ListName);
                                MySqlParameter[] p = { ref_noP, dateP, nameP, fathers_nameP, addressP, cityP, districtP, stateP, mobile_noP, No_of_FormsP, ListNameP };
                                common.ExecuteNonQuery(con, tran, qry, p);
                            }
                        }
                        if (ddlListType.SelectedValue.ToString() == "ListB")
                        {
                            //string modby = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();
                            foreach (DataRow dr in dt.Rows)
                            {
                                string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["name"].ToString().ToLower());
                                string relation = dr["relation"].ToString();
                                string relation_name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["relation_name"].ToString().ToLower());
                                string address = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["address"].ToString().ToLower());
                                string district = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["district"].ToString().ToLower());
                                string state = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["state"].ToString().ToLower());
                                string phone = dr["phone"].ToString();
                                DateTime DOB = Convert.ToDateTime(dr["dob"]);
                                string aadhaar_card = dr["aadhaar_card"].ToString();
                                string qry = "insert into listb(name,relation,relation_name,address,district,state,phone,dob,aadhaar_card)"
                                    + "values(@name,@relation,@relation_name,@address,@district,@state,@phone,@dob,@aadhaar_card)";
                                MySqlParameter nameP = new MySqlParameter("@name", name);
                                MySqlParameter relationP = new MySqlParameter("@relation", relation);
                                MySqlParameter relation_nameP = new MySqlParameter("@relation_name", relation_name);
                                MySqlParameter addressP = new MySqlParameter("@address", address);
                                MySqlParameter districtP = new MySqlParameter("@district", district);
                                MySqlParameter stateP = new MySqlParameter("@state", state);
                                MySqlParameter phoneP = new MySqlParameter("@phone", phone);
                                MySqlParameter dobP = new MySqlParameter("@dob", DOB);
                                MySqlParameter aadhaar_cardP = new MySqlParameter("@aadhaar_card", aadhaar_card);
                                MySqlParameter[] p = { nameP, relationP, relation_nameP, addressP, districtP, stateP, phoneP, dobP, aadhaar_cardP };
                                common.ExecuteNonQuery(con, tran, qry, p);
                            }
                        }
                        tran.Commit();
                        con.Close();
                        File.Delete(uploadedFile);
                        AlertAfterSignUp.Visible = true;
                        lblSucess.Text = dt.Rows.Count + " Record's uploaded successfully into " + ddlListType.SelectedValue.ToString() + "";
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
                        BindTotalCount();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        con.Close();
                        AlertErrorMsg.Visible = true;
                        lblmsg.Text = "UploadData Error: " + ex.Message;
                    }

                }
            }
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "UploadData Error: " + ex.Message;
        }
    }
    protected void btnupload_file_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        AlertAfterSignUp.Visible = false;
        lblSucess.Text = "";
        string fileExists = "";
        try
        {
            if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0))
            {
                var count = 0;
                foreach (HttpPostedFile uploadedFile in FileUpload1.PostedFiles)
                {
                    string fn = System.IO.Path.GetFileName(uploadedFile.FileName);
                    string SaveLocation = Server.MapPath(common.GetUploadFolderPath()) + "\\" + fn;
                    try
                    {
                        if (!File.Exists(SaveLocation))
                        {
                            uploadedFile.SaveAs(SaveLocation);
                            count++;
                        }
                        else
                        {
                            fileExists += fn + ",";
                        }
                    }
                    catch (Exception ex)
                    {
                        AlertErrorMsg.Visible = true;
                        lblmsg.Text = "Error: " + ex.Message;
                    }
                }
                if (count > 0)
                {
                    AlertAfterSignUp.Visible = true;
                    lblSucess.Text = count + " files has been uploaded.";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
                }
                if (fileExists != "")
                {
                    fileExists = fileExists.Remove(fileExists.Length - 1, 1);
                    AlertErrorMsg.Visible = true;
                    lblmsg.Text = "Not able to upload,These files are Already Exists: " + fileExists;
                }
            }
            else
            {
                AlertErrorMsg.Visible = true;
                lblmsg.Text = "Please select a file to upload.";
            }
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "UploadData Error: " + ex.Message;
        }
    }
    protected void btnuploadconfirmationsignature_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        AlertAfterSignUp.Visible = false;
        lblSucess.Text = "";
        //string fileExists = "";
        try
        {
            string FolderName = common.GetConfirmationFolderPath();
            if ((FileUpload2.PostedFile != null) && (FileUpload2.PostedFile.ContentLength > 0))
            {
                var count = 0;
                if (!Directory.Exists(Server.MapPath(FolderName) + "\\" + ddlconfimrationlisttype.SelectedValue))
                {
                    Directory.CreateDirectory(Server.MapPath(FolderName) + "\\" + ddlconfimrationlisttype.SelectedValue);
                }
                string path = Server.MapPath(FolderName) + "\\" + ddlconfimrationlisttype.SelectedValue;
                foreach (HttpPostedFile uploadedFile in FileUpload2.PostedFiles)
                {
                    string fn = System.IO.Path.GetFileName(uploadedFile.FileName);
                    string SaveLocation = path + "\\" + fn;
                    try
                    {
                        //if (!File.Exists(SaveLocation))
                        //{
                        uploadedFile.SaveAs(SaveLocation);
                        count++;
                        //}
                        //else
                        //{
                        //    fileExists += fn + ",";
                        //}
                    }
                    catch (Exception ex)
                    {
                        AlertErrorMsg.Visible = true;
                        lblmsg.Text = "Error: " + ex.Message;
                    }
                }
                if (count > 0)
                {
                    AlertAfterSignUp.Visible = true;
                    lblSucess.Text = count + " files has been uploaded.";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
                }
                //if (fileExists != "")
                //{
                //    fileExists = fileExists.Remove(fileExists.Length - 1, 1);
                //    AlertErrorMsg.Visible = true;
                //    lblmsg.Text = "Not able to upload,These files are Already Exists: " + fileExists;
                //}
            }
            else
            {
                AlertErrorMsg.Visible = true;
                lblmsg.Text = "Please select a file to upload.";
            }
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "UploadData Error: " + ex.Message;
        }
    }
    private void BindTotalCount()
    {
        int TotalPendingListA = 0, TotalRecordMerged = 0, TotalListBRecords = 0, TotalPDFCount = 0;
        try
        {
            string qry = "Select count(*) from lista where IsDeleted=0";
            TotalPendingListA = Convert.ToInt32(MySqlHelper.ExecuteScalar(common.GetConnectionString(), qry));
        }
        catch
        {
            TotalPendingListA = 0;
        }
        lblListAPending.Text = TotalPendingListA.ToString();
        try
        {
            string qry = "Select count(*) from MergeTB";
            TotalRecordMerged = Convert.ToInt32(MySqlHelper.ExecuteScalar(common.GetConnectionString(), qry));
        }
        catch
        {
            TotalRecordMerged = 0;
        }
        lbltotalRecordMerged.Text = TotalRecordMerged.ToString();
        try { string qry = "select count(*) from listb where IsDeleted=0"; TotalListBRecords = Convert.ToInt32(MySqlHelper.ExecuteScalar(common.GetConnectionString(), qry)); } catch { TotalListBRecords = 0; }
        lblListBTotalRecords.Text = TotalListBRecords.ToString();

        try
        {
            string qry = "SELECT ListName,count(SetNo) as Total FROM mergetb group by ListName";
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            grvSetNoListwise.DataSource = dt;
            grvSetNoListwise.DataBind();
            //int total = 0;
            //total = dt.AsEnumerable().Sum(row => row.Field<Int32>("Total"));
            //grvSetNoListwise.FooterRow.Cells[1].Text = "Total";
            //grvSetNoListwise.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            //grvSetNoListwise.FooterRow.Cells[2].Text = total.ToString();
        }
        catch (Exception ex) { }

        try
        {
            string UploadLocation = Server.MapPath(common.GetUploadFolderPath());
            TotalPDFCount = Directory.EnumerateFiles(UploadLocation, "*.pdf").Count();
        }
        catch { TotalPDFCount = 0; }
        lbltotalPDFCount.Text = TotalPDFCount.ToString();
    }
    protected void btnDownloadData_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {
            btnDownloadData.Enabled = false;
            Fill_GV();
            string FileName = "Index_" + ddlAllList.SelectedValue + ".xls";
            ExportGridToExcel(FileName);
            btnDownloadData.Enabled = true;
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
    private void ExportGridToExcel(string FileName)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        //string FileName = "Merge_Data_" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }

    protected void btnListADownload_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {
            btnListADownload.Enabled = false;
            string qry = "Select ref_no,DATE_FORMAT(date,'%d/%b/%Y') as Date,name as Name,fathers_name as FatherName,address as Address,city as City,district as District,state as State,mobile_no as MobileNo,No_of_Forms,ListName from lista where IsDeleted=0";
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            // ExportToExcel(Convert.ToInt32(dt.Rows.Count));
            // dt = city.GetAllCity();//your datatable
            string attachment = "attachment; filename=ListA_Data.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            btnListADownload.Enabled = true;
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void btndeleteListA_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {
            string qry = "TRUNCATE TABLE lista";
            MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry);
            AlertAfterSignUp.Visible = true;
            lblSucess.Text = "ListA Deleted Successfully!!";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
            BindTotalCount();
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void btndeleteMergedData_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {
            string qry = "TRUNCATE TABLE mergetb";
            MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry);
            AlertAfterSignUp.Visible = true;
            lblSucess.Text = "Merged Data Deleted Successfully!!";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
            BindTotalCount();
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void btndeleteListBData_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {
            string qry = "TRUNCATE TABLE listb";
            MySqlHelper.ExecuteNonQuery(common.GetConnectionString(), qry);
            AlertAfterSignUp.Visible = true;
            lblSucess.Text = "ListB Deleted Successfully!!";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
            BindTotalCount();
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void btnDownlaodListBData_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {
            btnDownlaodListBData.Enabled = false;
            string qry = "Select name,relation,relation_name,address,district,state,phone,DATE_FORMAT(dob,'%d/%b/%Y') as dob,aadhaar_card from listb where IsDeleted=0";
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            // ExportToExcel(Convert.ToInt32(dt.Rows.Count));
            // dt = city.GetAllCity();//your datatable
            string attachment = "attachment; filename=ListB_Data.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            btnDownlaodListBData.Enabled = true;
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
    public void Fill_GV()
    {
        string qry = "Select m.name,m.address,m.district,m.state,m.phone,m.aadhaar_card,m.ref_no,DATE_FORMAT(m.date,'%d/%b/%Y') as "
            + "date,m.No_of_Forms,m.ListName,u.EmailId as CreatedBy,m.CreatedOn,m.FormNo,m.SetNo,u1.EmailId as SetNoCreatedBy,m.SetNoCreatedOn from MergeTB m left join users u "
           + "on m.CreatedBy=u.UserID left join users u1 on m.SetNoCreatedBy=u1.UserID ";
        if (ddlAllList.SelectedValue != "All")
        {
            qry += "where m.ListName='" + ddlAllList.SelectedValue + "'";
        }
        qry += " order by SetNo";
        DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];

        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        for (int i = GridView1.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GridView1.Rows[i];
            GridViewRow previousRow = GridView1.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[5].Text.Trim() == previousRow.Cells[5].Text.Trim())
                {
                    if (previousRow.Cells[0].RowSpan == 0)
                    {
                        if (row.Cells[0].RowSpan == 0)
                        {
                            previousRow.Cells[0].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[0].RowSpan = row.Cells[0].RowSpan + 1;
                        }
                        row.Cells[0].Visible = false;
                    }
                    if (previousRow.Cells[1].RowSpan == 0)
                    {
                        if (row.Cells[1].RowSpan == 0)
                        {
                            previousRow.Cells[1].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[1].RowSpan = row.Cells[1].RowSpan + 1;
                        }
                        row.Cells[1].Visible = false;
                    }
                    if (previousRow.Cells[2].RowSpan == 0)
                    {
                        if (row.Cells[2].RowSpan == 0)
                        {
                            previousRow.Cells[2].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[2].RowSpan = row.Cells[2].RowSpan + 1;
                        }
                        row.Cells[2].Visible = false;
                    }
                    if (previousRow.Cells[3].RowSpan == 0)
                    {
                        if (row.Cells[3].RowSpan == 0)
                        {
                            previousRow.Cells[3].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[3].RowSpan = row.Cells[3].RowSpan + 1;
                        }
                        row.Cells[3].Visible = false;
                    }
                    if (previousRow.Cells[4].RowSpan == 0)
                    {
                        if (row.Cells[4].RowSpan == 0)
                        {
                            previousRow.Cells[4].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[4].RowSpan = row.Cells[4].RowSpan + 1;
                        }
                        row.Cells[4].Visible = false;
                    }
                    if (previousRow.Cells[5].RowSpan == 0)
                    {
                        if (row.Cells[5].RowSpan == 0)
                        {
                            previousRow.Cells[5].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[5].RowSpan = row.Cells[5].RowSpan + 1;
                        }
                        row.Cells[5].Visible = false;
                    }
                    if (previousRow.Cells[12].RowSpan == 0)
                    {
                        if (row.Cells[12].RowSpan == 0)
                        {
                            previousRow.Cells[12].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[12].RowSpan = row.Cells[12].RowSpan + 1;
                        }
                        row.Cells[12].Visible = false;
                    }
                }
            }
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {

            string qry = "Select ref_no,DATE_FORMAT(date,'%d/%b/%Y') as Date,name as Name,fathers_name as FatherName,address as Address,city as City,district as District,state as State,mobile_no as MobileNo,No_of_Forms,ListName from lista where IsDeleted=1";
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            // ExportToExcel(Convert.ToInt32(dt.Rows.Count));
            // dt = city.GetAllCity();//your datatable
            string attachment = "attachment; filename=ListA_Old_Data.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
    protected void btnGenerateMergePDFFiles_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        AlertAfterSignUp.Visible = false;
        lblSucess.Text = "";
        int count = 0;
        try
        {
            string qry = "SELECT distinct aadhaar_card,SetNo,ListName FROM mergetb order by ListName,SetNo";
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (!Directory.Exists(Server.MapPath(common.GetMergedFolderPath()) + "\\" + dr["ListName"].ToString()))
                {
                    Directory.CreateDirectory(Server.MapPath(common.GetMergedFolderPath()) + "\\" + dr["ListName"].ToString());
                }
                if (File.Exists(Server.MapPath(common.GetUploadFolderPath()) + "\\"+dr["aadhaar_card"].ToString()+"A.pdf")&&
                    File.Exists(Server.MapPath(common.GetConfirmationFolderPath()) + "\\"+ dr["ListName"].ToString()+ "\\"+ dr["aadhaar_card"].ToString() + ".pdf"))
                {
                    using (PdfSharp.Pdf.PdfDocument one = PdfSharp.Pdf.IO.PdfReader.Open(Server.MapPath(common.GetConfirmationFolderPath()) + "\\" + dr["ListName"].ToString() + "\\" + dr["aadhaar_card"].ToString() + ".pdf", PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import))
                    using (PdfSharp.Pdf.PdfDocument two = PdfSharp.Pdf.IO.PdfReader.Open(Server.MapPath(common.GetUploadFolderPath()) + "\\" + dr["aadhaar_card"].ToString() + "A.pdf", PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import))
                    
                    using (PdfSharp.Pdf.PdfDocument outPdf = new PdfSharp.Pdf.PdfDocument())
                    {
                        CopyPages(one, outPdf);
                        CopyPages(two, outPdf);
                        outPdf.Save(Server.MapPath(common.GetMergedFolderPath()) + "\\" + dr["ListName"].ToString() + "\\" + dr["SetNo"].ToString() + ".pdf");
                        count++;
                    }
                }
            }
            if (count > 0)
            {
                AlertAfterSignUp.Visible = true;
                lblSucess.Text = count + " PDF files has been Merged Sucessfully.";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
            }
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {

            string qry = "Select name,relation,relation_name,address,district,state,phone,DATE_FORMAT(dob,'%d/%b/%Y') as dob,aadhaar_card from listb where IsDeleted=1";
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            // ExportToExcel(Convert.ToInt32(dt.Rows.Count));
            // dt = city.GetAllCity();//your datatable
            string attachment = "attachment; filename=ListB_Old_Data.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();

        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }


    private static DataTable srnodt = new DataTable();
    public static int endcount = 0;
    protected void Button3_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        AlertAfterSignUp.Visible = false;
        lblSucess.Text = "";
        endcount = 0;
        try
        {
            if (txtsetnofrom.Text != "" && txtsetnoTo.Text != "")
            {
                srnodt.Clear();
                string qry = "select * from mergetb where SetNo>='" + txtsetnofrom.Text + "' and SetNo<='" + txtsetnoTo.Text + "' and ListName='" + ddllsitforpdf.SelectedValue.ToString() + "' group by SetNo order by SetNo";
                srnodt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
                StringBuilder htmlTable = new StringBuilder();

                foreach (DataRow dr in srnodt.Rows)
                {
                    string name = dr["name"].ToString();
                    string relation= dr["relation"].ToString();
                    string relation_name= dr["relation_name"].ToString();
                    string address = dr["address"].ToString() + " District " + dr["district"].ToString() + " State " + dr["state"].ToString();
                    string aadharno = dr["aadhaar_card"].ToString();
                    string SetNO = dr["SetNo"].ToString();
                    //htmlTable.Append("<div style='page-break-before: always'>");
                  
                    htmlTable.Append("<div style='font-weight:bold;text-align: center;'><u>Confirmation towards voluntary contribution / donation</u></div>");
                   
                    htmlTable.Append("<br/>");
                    htmlTable.Append("<div>I, " + name +" "+ relation+" "+relation_name+" R/o " + address + ", Adhaar No. " + aadharno + ", do hereby declare and confirm as under:-</div>");
                    htmlTable.Append("<br/>");
                    htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That I am a permanent resident of above-mentioned address.</p>");
                    htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That I am the follower /devotee of Dera Sacha Sauda</p>");
                    htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That I have been regularly visiting Dera Sacha Sauda since long.</p>");
                    htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That on the request of Dera Sacha Sauda and after inspection of the copy of &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;aforesaid donation receipt produced before me by the Trust, I hereby declare and &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;confirm that I have contributed following amounts towards voluntary Contribution &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;for welfare and Charitable activities of Dera Sacha Sauda, Sirsa.</p>");
                    htmlTable.Append("<br/>");
                    htmlTable.Append("<table border='1' width='80%'>");
                    htmlTable.Append("<tr>");

                    htmlTable.Append("<th width='5%' style='text-align: center;'><b>Sr No.</b></th>");
                    htmlTable.Append("<th width='20%' style='text-align: center;'><b>Date</b></th>");
                    htmlTable.Append("<th width='20%' style='text-align: center;'><b>Amount</b></th>");
                    htmlTable.Append("<th colspan='2' width='55%' style='text-align: center;'><b>Receipt No.</b></th>");
                    htmlTable.Append("</tr>");
                    int k = 1;
                    string qry1 = "select date,No_of_Forms,ref_no from mergetb where SetNo='" + SetNO + "' and ListName='" + ddllsitforpdf.SelectedValue.ToString() + "'";
                    DataTable SrNoDT = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry1).Tables[0];
                    for (int j = 0; j < SrNoDT.Rows.Count; j++)
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td width='5%' style='text-align: center;'>" + k + "</td>");
                        htmlTable.Append("<td width='20%'>" + Convert.ToDateTime(SrNoDT.Rows[j]["date"]).ToString("dd-MMM-yyyy") + "</td>");
                        htmlTable.Append("<td width='20%' style='text-align: right;'>" + common.addCommas(Convert.ToDecimal(SrNoDT.Rows[j]["No_of_Forms"])) + "</td>");
                        htmlTable.Append("<td colspan='2' width='55%'>" + SrNoDT.Rows[j]["ref_no"] + "</td>");
                        htmlTable.Append("</tr>");
                        k++;
                    }
                    htmlTable.Append("</table>");
                    htmlTable.Append("<br/>");
                    //
                    htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That the aforesaid confirmation is given and signed by me as an evidence &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; of my voluntary contributionto the trust as required by the trust to be used in &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; their appellate proceedings under Income Tax Act.</p>");
                    htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;I am annexing the copy of Aadhaar card duly signed by me along with &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;this confirmation.</p>");
                    htmlTable.Append("<br/><br/>");
                    //htmlTable.Append("<div style='text-align: right;'>(" + name + ")</div>");
                    htmlTable.Append("<table border='0' float='right'>");
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td>&nbsp;</td>");
                    htmlTable.Append("<td>&nbsp;</td>");
                    htmlTable.Append("<td>&nbsp;</td>");
                    htmlTable.Append("<td>(" + name + ")</td>");
                    htmlTable.Append("</tr>");
                    htmlTable.Append("</table>");
                   // htmlTable.Append("<br/>");
                    //htmlTable.Append("<div style='width:20%;text-align: right;'>(" + address + ")</div>");
                    htmlTable.Append("<table border='0' float='right'>");
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td>&nbsp;</td>");
                    htmlTable.Append("<td>&nbsp;</td>");
                    htmlTable.Append("<td>&nbsp;</td>");
                    htmlTable.Append("<td colspan='2'>R/o " + address + "</td>");
                    htmlTable.Append("</tr>");
                    htmlTable.Append("</table>");
                    //htmlTable.Append("<div style='position:absolute; bottom:0; width:100%; height:60px;background:#6cf;'>This is a footer. Page </div>");
                    //htmlTable.Append("<br/><br/><br/><br/>");
                    //htmlTable.Append("<div style=\"font-family: \'Arial\', sans-serif; color: #ccc; font-size: 10px; margin: 0 auto;text-align:center;bottom:0;position:fixed;\">Simple page footer aligned by center.</div>");
                    
                    //htmlTable.Append("<div style='text-align: right;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;R/o " + address + "</div>");
                    //htmlTable.Append("<div style='page-break-after:always;'>&nbsp;</div>");
                    //htmlTable.Append("</div>");
                    //if (k < 5)
                    //{
                    //    htmlTable.Append("<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>");
                    //}
                    //else if (k >= 5 && k < 10)
                    //{ htmlTable.Append("<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>"); }
                    //else if ( k > 10)
                    //{ htmlTable.Append("<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>"); }
                    htmlTable.Append("<newpage />");
                }
                lblResult.Text = htmlTable.ToString();

                //Creating the object of the String Writer.
                StringWriter sw = new StringWriter();

                // Creating the object of HTML Writer and passing the object of String Writer to HTMl Text Writer
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                //this.Page.RenderControl(hw);
                lblResult.RenderControl(hw);
                // Now we what ever is rendered on the page we will give it to the object of the String reader so that we can 
                StringReader srdr = new StringReader(sw.ToString());

                // Creating the PDF DOCUMENT using the Document class from Itextsharp.pdf namespace
                Document pdfDoc = new Document(PageSize.A4, 50F, 50F, 50F, 0.2F);

                // HTML Worker allows us to parse the HTML Content to the PDF Document.To do this we will pass the object of Document class as a Parameter.

                //  HTMLWorker hparse = new HTMLWorker(pdfDoc);

                HTMLWorkerExtended hparse = new HTMLWorkerExtended(pdfDoc);
                //using (var htmlWorker = new HTMLWorkerExtended()
                //{
                //    htmlWorker.Open();
                //    htmlWorker.Parse(htmlViewReader);
                //}

                // Finally we write data to PDF and open the Document
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                /******Added*/
                if (chkfooter.Checked)
                {
                    writer.PageEvent = new Footer();
                }
               

                // Add the Paragraph object to the document
                
                /******Added*/
                // Now we will pass the entire content that is stored in String reader to HTML Worker object to achieve the data from to String to HTML and then to PDF.
                hparse.Parse(srdr);

                pdfDoc.Close();

                //Now finally we write to the PDF Document using the Response.Write method.
                Response.ContentType = "application/pdf"; // Setting the application
                                                          // Assigning the header
                Response.AddHeader("content-disposition", "attachment;filename=VOLUNTARY CONTRIBUTION FORMAT.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }

        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
   
    public partial class Footer : PdfPageEventHelper

    {

        public override void OnEndPage(PdfWriter writer, Document doc)

        {
            Paragraph footer = new Paragraph();
            try
            {
                footer = new Paragraph(srnodt.Rows[endcount]["ListName"].ToString()+" "+srnodt.Rows[endcount]["SetNo"].ToString(), FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));

                endcount++;
            }
            catch { }
            footer.Alignment = Element.ALIGN_RIGHT;

            PdfPTable footerTbl = new iTextSharp.text.pdf.PdfPTable(1);

            footerTbl.TotalWidth = 300;

            footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell cell = new PdfPCell(footer);

            cell.Border = 0;

            cell.PaddingLeft = 10;

            footerTbl.AddCell(cell);

            footerTbl.WriteSelectedRows(0, -1, 415, 20, writer.DirectContent);

        }
    }
    void CopyPages(PdfSharp.Pdf.PdfDocument from, PdfSharp.Pdf.PdfDocument to)
    {
        for (int i = 0; i < from.PageCount; i++)
        {
            to.AddPage(from.Pages[i]);
        }
    }




    protected void Button4_Click(object sender, EventArgs e)
    {
        
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName(ddllisttypeformergedpdf.SelectedValue);
            //string filePath = Server.MapPath("~/User/JSON_Audit/" + fileName);
            string[] files = Directory.GetFiles(Server.MapPath(common.GetMergedFolderPath() + "\\" + ddllisttypeformergedpdf.SelectedValue));
            foreach(string fi in files)
            {
                zip.AddFile(fi, ddllisttypeformergedpdf.SelectedValue);
            }
           

            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("" + ddllisttypeformergedpdf.SelectedValue + "_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            zip.Save(Response.OutputStream);
            Response.End();
            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment; filename=DownloadedFile.zip");
            //Response.ContentType = "application/zip";
            //zip.Save(Response.OutputStream);
            //Response.End();
        }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {
            string qry = "SELECT distinct aadhaar_card,SetNo,ListName FROM mergetb  where 1=1";
          
                qry += " and ListName='" + ddllistformissingaadharpdf.SelectedValue + "'";
            
            qry += " order by ListName,SetNo";
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            foreach (DataRow dr in dt.Rows)
            {
                //string FileName = dr["aadhaar_card"] + "A" + ".pdf";
                //string filePath = Server.MapPath("upload") + FileName;
                string fileextension = ".pdf";
                string myFileName = dr["aadhaar_card"] + "A";
                if (File.Exists(Server.MapPath(common.GetUploadFolderPath()) + "\\" + myFileName + fileextension))
                {

                    dr["Status"] = "Yes";
                }
                else
                {
                    dr["Status"] = "No";
                }
            }
            string attachment = "attachment; filename==Missing_AadharPDF.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {
            string qry = "Select name,relation,relation_name,address,district,state,phone,DATE_FORMAT(dob,'%d/%b/%Y') as dob,aadhaar_card from listb where IsDeleted=0";
            DataTable dt = MySqlHelper.ExecuteDataset(common.GetConnectionString(), qry).Tables[0];
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            foreach (DataRow dr in dt.Rows)
            {
                //string FileName = dr["aadhaar_card"] + "A" + ".pdf";
                //string filePath = Server.MapPath("upload") + FileName;
                string fileextension = ".pdf";
                string myFileName = dr["aadhaar_card"] + "A";
                if (File.Exists(Server.MapPath(common.GetUploadFolderPath()) + "\\" + myFileName + fileextension))
                {

                    dr["Status"] = "Yes";
                }
                else
                {
                    dr["Status"] = "No";
                }
            }
            string attachment = "attachment; filename==Missing_LIST-B_AadharPDF.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        AlertErrorMsg.Visible = false;
        lblmsg.Text = "";
        try
        {
            DirectoryInfo d = new DirectoryInfo(Server.MapPath(common.GetUploadFolderPath())); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles("*.pdf"); //Getting Text files
            string str = "";
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));
            foreach (FileInfo file in Files)
            {
                DataRow dr = dt.NewRow();
                dr["FileName"] = Path.GetFileNameWithoutExtension(file.Name);
                //str = str + ", " + file.Name;
                dt.Rows.Add(dr);
            }
            string attachment = "attachment; filename==AadharPDF_FileNames.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

}
public class HTMLWorkerExtended : HTMLWorker
{
    public HTMLWorkerExtended(Document document) : base(document)
    {

    }
    public override void StartElement(string tag, Hashtable h)
    {
        if (tag.Equals("newpage"))
            document.Add(Chunk.NEXTPAGE);
        else
            base.StartElement(tag, h);
    }
}