using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
public partial class DisplayPDF : System.Web.UI.Page
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
            if (!IsPostBack)
            {
            }
        }
        catch
        {
            Response.Redirect("~/login.aspx?UnAuthorizedAccess=1", false);
            return;
        }
    }
   

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {

            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";
            AlertAfterSignUp.Visible = false;
            lblSucess.Text = "";
            btndeletefile.Visible = false;
            if (txtsearchbyName.Text != "")
            {
                string fileextension = ".pdf";
                string myFileName = txtsearchbyName.Text.Trim() + "A";
                if (File.Exists(Server.MapPath(common.GetUploadFolderPath()) + "\\" + myFileName + fileextension))
                {
                    if (ar[2].ToString() == "Admin")
                    {
                        btndeletefile.Visible = true;
                    }
                    //myframe.Attributes["src"] = Server.MapPath("upload") + "\\" + myFileName + fileextension;
                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"800px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/" + common.GetUploadFolderPath() + "/" + myFileName + ".pdf"));
                }
                else { ltEmbed.Text = ""; btndeletefile.Visible = false; }
            }
            else
            {
                ltEmbed.Text = "";
                btndeletefile.Visible = false;
            }
        }
        catch (Exception ex)
        {
            btndeletefile.Visible = false;
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }

    protected void btndeletefile_Click(object sender, EventArgs e)
    {
        try
        {
            AlertErrorMsg.Visible = false;
            lblmsg.Text = "";
            string fileextension = ".pdf";
            string myFileName = txtsearchbyName.Text.Trim() + "A";
            if (File.Exists(Server.MapPath(common.GetUploadFolderPath()) + "\\" + myFileName + fileextension))
            {
                File.Delete((Server.MapPath(common.GetUploadFolderPath()) + "\\" + myFileName + fileextension));
                ltEmbed.Text = "";
                txtsearchbyName.Text = "";
                btndeletefile.Visible = false;
                AlertAfterSignUp.Visible = true;
                lblSucess.Text = "file has been deleted Sucessfully!!";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "hide();", true);
            }
        }
        catch (Exception ex)
        {
            
            AlertErrorMsg.Visible = true;
            lblmsg.Text = "Error: " + ex.Message;
        }
    }
}