<%@ Page Title="" Language="C#" MasterPageFile="~/entry.master" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
      
        function hide() {
            setTimeout(function () {
                $("[id*=AlertAfterSignUp]").fadeOut("slow");
            }, 10000);
        }
    </script>
     <style>
        .myclass{display:none;}
         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
  <div class="row">
                 <!-- left column -->
           <%-- <div class="col-md-6">--%>
             <div class="box-body">
                 <div id="AlertAfterSignUp" runat="server" visible="false" class="alert alert-success alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h4>	<i class="icon fa fa-check"></i> Alert!</h4>
                  <asp:Label ID="lblSucess" runat="server" Text="" Font-Bold="true"></asp:Label>
                  </div>
                    <div id="AlertErrorMsg" runat="server" visible="false" class="alert alert-danger alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                   <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                  </div>
                  </div>
                 <div class="col-sm-6"> 
              <!-- general form elements -->
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-upload"></i> Upload Excel Data</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                    <div class="form-group">
                      <label>List Type</label>
                      <asp:DropDownList ID="ddlListType" 
                        CssClass="form-control" runat="server" 
                         >
                          <asp:ListItem>ListA</asp:ListItem>
                          <asp:ListItem>ListB</asp:ListItem>
                    </asp:DropDownList> 
                    </div>
                    <div class="form-group">
                      <label>Please Select Excel File:</label>
                      
                     <asp:FileUpload ID="fileuploadExcel" runat="server" /><br />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Please choose a file."
    Style="color: red; visibility: visible;" ControlToValidate="fileuploadExcel" runat="server"
    Display="Dynamic" ForeColor="red" Font-Size="20px" ValidationGroup="sav" />
<br />
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$"
    ControlToValidate="fileuploadExcel" runat="server" ForeColor="red" Font-Size="20px"
    ErrorMessage="Please select xls or xlsx File with correct format." Display="Dynamic" ValidationGroup="sav" />

                    </div>
                  </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="btnsubmit" runat="server" Text="Upload Data" class="btn btn-success" 
                          ValidationGroup="sav" OnClick="btnsubmit_Click" ></asp:Button>
                   <%--<asp:Button ID="btnreset" runat="server" Text="Clear" class="btn btn-danger" 
                          ></asp:Button>--%>
                  </div>
                  </div>
              <!-- /.box -->
              </div><!-- /.box -->

       <div class="col-sm-6">
                 <!-- general form elements fa-calculator-->
              <div class="box box-info">
                <div class="box-header">
                  <h3 class="box-title"> Total Count</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                      <div class="row">
                    <div class="form-group col-xs-4">
                    <div class="info-box">
        <!-- Apply any bg-* class to to the icon to color it -->
        <%--<span class="info-box-icon bg-red"><i class="fa fa-star-o"></i></span>--%>
        <div class="info-box-content">
          <span class="info-box-text">ListA Pending Count</span>
          <span class="info-box-number"><asp:Label ID="lblListAPending" runat="server" Text="Label"></asp:Label></span>
        </div><!-- /.info-box-content -->
                        </div>
                        </div>

    <!-- /.info-box -->
                  <div class="form-group col-xs-4">
                        <div class="info-box">
        <!-- Apply any bg-* class to to the icon to color it -->
       <%-- <span class="info-box-icon bg-red"><i class="fa fa-star-o"></i></span>--%>
        <div class="info-box-content">
          <span class="info-box-text">ListB Total Records</span>
          <span class="info-box-number">
              <asp:Label ID="lblListBTotalRecords" runat="server" Text="Label"></asp:Label></span>
        </div><!-- /.info-box-content -->
      </div><!-- /.info-box -->
                      </div>
                      <div class="form-group col-xs-4">
                        <div class="info-box">
        <!-- Apply any bg-* class to to the icon to color it -->
       <%-- <span class="info-box-icon bg-red"><i class="fa fa-star-o"></i></span>--%>
        <div class="info-box-content">
          <span class="info-box-text">Total Records Merged</span>
          <span class="info-box-number">
              <asp:Label ID="lbltotalRecordMerged" runat="server" Text="Label"></asp:Label></span>
        </div><!-- /.info-box-content -->
      </div><!-- /.info-box -->
</div>
</div>
                   <div class="row">
                    <div class="form-group col-xs-5">
                    <div class="info-box">
        <!-- Apply any bg-* class to to the icon to color it -->
        <%--<span class="info-box-icon bg-red"><i class="fa fa-star-o"></i></span>--%>
        <div class="info-box-content">
          <span class="info-box-text">Total PDF Upload Count</span>
          <span class="info-box-number"><asp:Label ID="lbltotalPDFCount" runat="server" Text="Label"></asp:Label></span>
        </div><!-- /.info-box-content -->
                        </div>
                        </div>
                       </div>
                        <div class="info-box">
        <!-- Apply any bg-* class to to the icon to color it -->
       <%-- <span class="info-box-icon bg-red"><i class="fa fa-star-o"></i></span>--%>
        <div class="info-box-content">
          <span class="info-box-text">Total Set No Listwise</span>
        
              <asp:GridView ID="grvSetNoListwise" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" ShowFooter="false" runat="server">

                  <Columns>
    <asp:BoundField DataField="ListName" HeaderText="ListName" ItemStyle-Width="60" />
    <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-Width="60" />
       </Columns>
              </asp:GridView>
             
            
        </div><!-- /.info-box-content -->
      </div><!-- /.info-box -->
                          
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->
        <div class="col-sm-6"> 
              <!-- general form elements -->
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-upload"></i> Upload Multiple Pdf Files</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                    
                    <div class="form-group">
                      <label>Select PDf File:</label>
                      
                    <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" accept=".pdf"  />
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Please choose a file."
    Style="color: red; visibility: visible;" ControlToValidate="FileUpload1" runat="server"
    Display="Dynamic" ForeColor="red" Font-Size="20px" ValidationGroup="pdf" />

                    </div>
                  </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="btnupload_file" runat="server" Text="Upload Files" class="btn btn-success" 
                          ValidationGroup="pdf" OnClick="btnupload_file_Click" ></asp:Button>
                   <%--<asp:Button ID="btnreset" runat="server" Text="Clear" class="btn btn-danger" 
                          ></asp:Button>--%>
                  </div>
                  </div>
              <!-- /.box -->
              </div><!-- /.box -->

         <div class="col-sm-6"> 
              <!-- general form elements -->
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-upload"></i> Upload Multiple Confirmation Pdf Files</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                      <div class="form-group">
                      <label>Select List Type</label>
                      <asp:DropDownList ID="ddlconfimrationlisttype" 
                        CssClass="form-control" runat="server" 
                        >
                         
                    </asp:DropDownList> 
                    </div>
                    <div class="form-group">
                      <label>Select PDf File:</label>
                      
                    <asp:FileUpload ID="FileUpload2" runat="server" AllowMultiple="true" accept=".pdf" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ErrorMessage="Please choose a file."
    Style="color: red; visibility: visible;" ControlToValidate="FileUpload2" runat="server"
    Display="Dynamic" ForeColor="red" Font-Size="20px" ValidationGroup="pdfc" />

                    </div>
                  </div><!-- /.box-body -->

                  <div class="box-footer">
                   <asp:Button ID="btnuploadconfirmationsignature" runat="server" Text="Upload Files" class="btn btn-success" 
                          ValidationGroup="pdfc" OnClick="btnuploadconfirmationsignature_Click"></asp:Button>
                   <%--<asp:Button ID="btnreset" runat="server" Text="Clear" class="btn btn-danger" 
                          ></asp:Button>--%>
                  </div>
                  </div>
              <!-- /.box -->
              </div><!-- /.box -->









                <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Download OR <i class="fa fa-trash-o"></i> Delete Merged Data</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                        <div class="form-group">
                      <label>Select List Type</label>
                      <asp:DropDownList ID="ddlAllList" 
                        CssClass="form-control" runat="server" 
                         >
                    </asp:DropDownList> 
                    </div>
                    <div class="form-group">
                      <asp:Button ID="btnDownloadData" runat="server" Text="Download Merge Data" class="btn btn-dropbox" OnClick="btnDownloadData_Click" 
                           ></asp:Button>
                      
                    </div>
                    <div class="form-group">
                       <asp:Button ID="btndeleteMergedData" runat="server" Text="Delete Merged Data" class="btn btn-danger" OnClientClick="if (!window.confirm('Are you sure you want to permanently delete Merged Data?')) return false;" OnClick="btndeleteMergedData_Click" 
                           />
                    </div>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->

           <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Download OR <i class="fa fa-trash-o"></i> Delete ListA Data</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                    <div class="form-group">
                      <asp:Button ID="btnListADownload" runat="server" Text="Download ListA Data" class="btn btn-dropbox" OnClick="btnListADownload_Click" 
                           />
                        
                    </div>
                    <div class="form-group">
                       <asp:Button ID="btndeleteListA" runat="server" Text="Delete ListA Data" class="btn btn-danger" OnClientClick="if (!window.confirm('Are you sure you want to permanently delete ListA?')) return false;" OnClick="btndeleteListA_Click" 
                           />
                    </div>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->

        <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i><%--<i class="fa fa-trash-o"></i>--%> Download OR <i class="fa fa-trash-o"></i> Delete ListB Data</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                    <div class="form-group">
                  <asp:Button ID="btnDownlaodListBData" runat="server" Text="Download ListB Data" class="btn btn-dropbox" OnClick="btnDownlaodListBData_Click" 
                           />    
                        
                    </div>
                    <div class="form-group">
                       <asp:Button ID="btndeleteListBData" runat="server" Text="Delete ListB Data" class="btn btn-danger" OnClientClick="if (!window.confirm('Are you sure you want to permanently delete ListB?')) return false;" OnClick="btndeleteListBData_Click" 
                           />
                    </div>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->



       <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Download Old ListA Data</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                    <div class="form-group">
                      <asp:Button ID="Button1" runat="server" Text="Download Old ListA Data" class="btn btn-dropbox" OnClick="Button1_Click" 
                           />
                        
                    </div>
                  <%--  <div class="form-group">
                       <asp:Button ID="Button2" runat="server" Text="Delete ListA Data" class="btn btn-danger" OnClientClick="if (!window.confirm('Are you sure you want to permanently delete ListA?')) return false;" OnClick="btndeleteListA_Click" 
                           />
                    </div>--%>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->

           <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Download Old ListB Data</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                    <div class="form-group">
                      <asp:Button ID="Button2" runat="server" Text="Download Old ListB Data" class="btn btn-dropbox" OnClick="Button2_Click" 
                           />
                        
                    </div>
                  <%--  <div class="form-group">
                       <asp:Button ID="Button2" runat="server" Text="Delete ListA Data" class="btn btn-danger" OnClientClick="if (!window.confirm('Are you sure you want to permanently delete ListA?')) return false;" OnClick="btndeleteListA_Click" 
                           />
                    </div>--%>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->

        <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Download Combine PDF File </h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                      <div class="form-group">
                          <asp:checkbox ID="chkfooter" runat="server" Checked="True" Text="Print Footer"></asp:checkbox>
                      </div>
                        <div class="form-group">
                      <label>Select List Type</label>
                      <asp:DropDownList ID="ddllsitforpdf" 
                        CssClass="form-control" runat="server" 
                         >
                    </asp:DropDownList> 
                    </div>
                       <div class="form-group col-xs-4">
   <asp:TextBox ID="txtsetnofrom" runat="server" class="form-control" 
                            placeholder="Enter SetNo From"></asp:TextBox>
          </div>
       <div class="form-group col-xs-4">

   <asp:TextBox ID="txtsetnoTo" runat="server" class="form-control" 
                            placeholder="Enter SetNo To"></asp:TextBox>
          </div>
      
   

                    <div class="form-group">
                      <asp:Button ID="Button3" runat="server" Text="Download PDF" class="btn btn-dropbox" OnClick="Button3_Click"
                           />
                        
                    </div>
                  <%--  <div class="form-group">
                       <asp:Button ID="Button2" runat="server" Text="Delete ListA Data" class="btn btn-danger" OnClientClick="if (!window.confirm('Are you sure you want to permanently delete ListA?')) return false;" OnClick="btndeleteListA_Click" 
                           />
                    </div>--%>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->


       <asp:Label ID="lblResult" runat="server" CssClass="myclass" ></asp:Label>

        <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Generate Merge PDF Files </h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                     <%--   <div class="form-group">
                      <label>Select List Type</label>
                      <asp:DropDownList ID="DropDownList1" 
                        CssClass="form-control" runat="server" 
                         >
                    </asp:DropDownList> 
                    </div>--%>
                    <%--   <div class="form-group col-xs-4">
   <asp:TextBox ID="TextBox1" runat="server" class="form-control" 
                            placeholder="Enter SetNo From"></asp:TextBox>
          </div>--%>
      <%-- <div class="form-group col-xs-4">

   <asp:TextBox ID="TextBox2" runat="server" class="form-control" 
                            placeholder="Enter SetNo To"></asp:TextBox>
          </div>--%>
                    <div class="form-group">
                      <asp:Button ID="btnGenerateMergePDFFiles" runat="server" Text="Generate Merge PDF Files" class="btn btn-dropbox" OnClick="btnGenerateMergePDFFiles_Click" />
                           
                    </div>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->


      <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Download Merged PDF Files (List Wise) </h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                        <div class="form-group">
                      <label>Select List Type</label>
                      <asp:DropDownList ID="ddllisttypeformergedpdf" 
                        CssClass="form-control" runat="server" 
                         >
                    </asp:DropDownList> 
                    </div>
                    <%--   <div class="form-group col-xs-4">
   <asp:TextBox ID="TextBox1" runat="server" class="form-control" 
                            placeholder="Enter SetNo From"></asp:TextBox>
          </div>--%>
      <%-- <div class="form-group col-xs-4">

   <asp:TextBox ID="TextBox2" runat="server" class="form-control" 
                            placeholder="Enter SetNo To"></asp:TextBox>
          </div>--%>
                    <div class="form-group">
                      <asp:Button ID="Button4" runat="server" Text=" Download Merged PDF Files" class="btn btn-dropbox" OnClick="Button4_Click" />
                           
                    </div>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->

           
        <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Download Missing Aadhaar Card File</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                        <div class="form-group">
                      <label>Select List Type</label>
                      <asp:DropDownList ID="ddllistformissingaadharpdf" 
                        CssClass="form-control" runat="server" 
                         >
                    </asp:DropDownList> 
                    </div>
                  
                    <div class="form-group">
                      <asp:Button ID="Button5" runat="server" Text="Download Missing Aadhaar Card" class="btn btn-dropbox" OnClick="Button5_Click" />
                           
                    </div>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->

       <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Download List-B Missing Aadhaar Card File</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                      
                  
                    <div class="form-group">
                      <asp:Button ID="Button6" runat="server" Text="Download List-B Missing Aadhaar Card" class="btn btn-dropbox" OnClick="Button6_Click" />
                           
                    </div>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->
      
       <div class="col-sm-6">
                 <!-- general form elements -->
              <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title"><i class="fa fa-download"></i> Download Aadhaar Card File Name List</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                      
                  
                    <div class="form-group">
                      <asp:Button ID="Button7" runat="server" Text="Download Aadhaar Card File Name List" class="btn btn-dropbox" OnClick="Button7_Click" />
                           
                    </div>
                  </div><!-- /.box-body -->

                 </div>
              <!-- /.box -->
              </div><!-- /.box -->
              <%--</div>--%>
              </div>
              </section>
             </div>
    <div style="display:none">
    <asp:GridView ID="GridView1" runat="server" OnDataBound="GridView1_DataBound"></asp:GridView>
        </div>
</asp:Content>

