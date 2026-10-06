using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;
public partial class exportpdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string qry = "select * from mergetb where  FormNo='14'";
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["cn"].ToString(), qry).Tables[0];
            StringBuilder htmlTable = new StringBuilder();

            for (int m = 0; m <= 12; m++)
            {
                htmlTable.Append("<!DOCTYPE html>");
                htmlTable.Append("<html>");
                htmlTable.Append("<body>");
                //htmlTable.Append("<style> #footer {position: running(footer);} @page {@bottom-center {content: element(footer);}}</style>");
               
                htmlTable.Append("<div style='font-weight:bold;text-align: center;'><u>Confirmation towards voluntary contribution / donation</u></div>");
                htmlTable.Append("<br/>");
                htmlTable.Append("<div>I, Pankaj Kumar R/o 158, Jaguwas Alwar, 301701, District Alwar, State Rajasthan, Adhaar No. 7896-5687-5467,do hereby declare and confirm as under:-</div>");
                htmlTable.Append("<br/>");
                htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That I am a permanent resident of above-mentioned address.</p>");
                htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That I am the follower /devotee of DeraSachaSauda</p>");
                htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That I have been regularly visiting Dera Sacha Sauda since long.</p>");
                htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That on the request of DeraSachaSauda and after inspection of the copy of &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;aforesaid donation receipt produced before me by the Trust,I hereby declare and &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;confirm that I have contributed following amounts towards voluntary Contribution &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;for welfare and Charitable activities of DeraSachaSauda, Sirsa.</p>");
                htmlTable.Append("<br/>");
                htmlTable.Append("<table border='1'>");
                htmlTable.Append("<tr>");
                htmlTable.Append("<th width='5%' style='text-align: center;'><b>Sr No.</b></th>");
                htmlTable.Append("<th width='20%'><b>Date</b></th>");
                htmlTable.Append("<th width='20%' style='text-align: right;'><b>Amount</b></th>");
                htmlTable.Append("<th width='55%'><b>Receipt No.</b></th>");
                htmlTable.Append("</tr>");
                int k = 1;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td>" + k + "</td>");
                    htmlTable.Append("<td>" + Convert.ToDateTime(dt.Rows[j]["date"]).ToString("dd-MMM-yyyy") + "</td>");
                    htmlTable.Append("<td>" + dt.Rows[j]["No_of_Forms"] + "</td>");
                    htmlTable.Append("<td>" + dt.Rows[j]["ref_no"] + "</td>");
                    htmlTable.Append("</tr>");
                    k++;
                }
                htmlTable.Append("</table>");
                htmlTable.Append("<br/><br/>");
                string address = "R/o 158, Jaguwas Alwar, 301701, District Alwar, State Rajasthan R/o 158, Jaguwas Alwar, 301701, District Alwar, State Rajasthan";
                htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;That the aforesaid confirmation is given and signed by me as an evidence &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; of my voluntary contributionto the trust as required by the trust to be used in &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; their appellate proceedings under Income Tax Act.</p>");
                htmlTable.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;I am annexing the copy of Aadhaar card duly signed by me along with &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;this confirmation.</p>");
                htmlTable.Append("<br/><br/>");
                htmlTable.Append("<div style='text-align: right;'>(Pankaj Kumar)</div>");
                htmlTable.Append("<br/>");
                //htmlTable.Append("<div style='text-align: right;'>R/o 158, Jaguwas Alwar, 301701, District Alwar, State Rajasthan</div>");
                //htmlTable.Append("<table border='0' width='20%' style='float: right;'>");
                //htmlTable.Append("<tr>");
                //htmlTable.Append("<td>R/o 158, Jaguwas Alwar, 301701, District Alwar, State Rajasthan R/o 158, Jaguwas Alwar, 301701, District Alwar, State Rajasthan</td>");
                //htmlTable.Append("</tr>");
                //htmlTable.Append("</table>");
                htmlTable.Append("<div style='width:200px;text-align: right;'>(" + address + ")</div>");
                //htmlTable.Append("<div style='page-break-after:always;'>&nbsp;</div>");
                htmlTable.Append("</div>");
                //htmlTable.Append("<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>");
                htmlTable.Append("<footer>This is a footer. Page </footer>");
                htmlTable.Append("</body>");
                htmlTable.Append("</html>");
            }
            lblResult.Text = htmlTable.ToString();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {

        //Creating the object of the String Writer.
        StringWriter sw = new StringWriter();

        // Creating the object of HTML Writer and passing the object of String Writer to HTMl Text Writer
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //this.Page.RenderControl(hw);
        lblResult.RenderControl(hw);
        // Now we what ever is rendered on the page we will give it to the object of the String reader so that we can 
        StringReader srdr = new StringReader(sw.ToString());

        // Creating the PDF DOCUMENT using the Document class from Itextsharp.pdf namespace
        Document pdfDoc = new Document(PageSize.A4, 50F, 50F, 5F, 0.2F);

        // HTML Worker allows us to parse the HTML Content to the PDF Document.To do this we will pass the object of Document class as a Parameter.
        HTMLWorker hparse = new HTMLWorker(pdfDoc);
        // Finally we write data to PDF and open the Document
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();

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
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Verifies that the control is rendered //
    }
}