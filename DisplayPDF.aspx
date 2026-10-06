<%@ Page Title="" Language="C#" MasterPageFile="~/entry.master" AutoEventWireup="true" CodeFile="DisplayPDF.aspx.cs" Inherits="DisplayPDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
        function hide() {
            setTimeout(function () {
                $("[id*=AlertAfterSignUp]").fadeOut("slow");
            }, 5000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <asp:UpdatePanel ID="updatepanel1" runat="server" >
            <ContentTemplate>
        <!-- Main content -->
        <section class="content">
  <div class="row">
                 <!-- left column -->
            
             <div class="box-body">
                 <div id="AlertAfterSignUp" runat="server" visible="false" class="alert alert-success alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h4>	<i class="icon fa fa-check"></i> Alert!</h4>
                     <asp:Label ID="lblSucess" runat="server" Text=""></asp:Label>
                  </div>
                    <div id="AlertErrorMsg" runat="server" visible="false" class="alert alert-danger alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                   <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                  </div>
                  </div>
                  <div class="col-md-12">
              <!-- general form elements -->
              <div class="box box-primary" style="height:1100px;">
                <div class="box-header">
                  <h3 class="box-title">View PDF File</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">


     <div class="form-group col-xs-3">

   <asp:TextBox ID="txtsearchbyName" runat="server" class="form-control" 
                            placeholder="Search By File Name"></asp:TextBox>
          
          </div>
      
      
      <div class="form-group col-xs-1">
       
     <asp:Button ID="btnFilter" runat="server" Text="Search"
                          class="btn btn-info" OnClick="btnFilter_Click" ValidationGroup="serach" ></asp:Button>
     </div>
                  <div class="form-group col-xs-1">
                      <asp:Button ID="btndeletefile" runat="server" Text="Delete"
                          class="btn btn-danger" Visible="false" OnClientClick="if (!window.confirm('Are you sure you want to delete this file?')) return false;" OnClick="btndeletefile_Click" ></asp:Button>
                  </div>   
  <asp:Literal ID="ltEmbed" runat="server" />
    </div>
</div>
                      </div>
      </div>
            </section>
                 </ContentTemplate>

        </asp:UpdatePanel>
                  </div>

</asp:Content>

