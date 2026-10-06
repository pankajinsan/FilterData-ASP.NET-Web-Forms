<%@ Page Title="" Language="C#" MasterPageFile="~/entry.master" AutoEventWireup="true" CodeFile="UpdateEmptySetNo.aspx.cs" Inherits="UpdateEmptySetNo" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
      
        function hide() {
            setTimeout(function () {
                $("[id*=AlertAfterSignUp]").fadeOut("slow");
            }, 2000);
        }
        function hideError() {
            setTimeout(function () {
                $("[id*=AlertErrorMsg]").fadeOut("slow");
            }, 2000);
        }
        function showpopError(msg, title) {
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-center",
                "preventDuplicates": true,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "9000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            // toastr['success'](msg, title);
            var d = Date();
            toastr.error(msg, title);
            return false;
        }
        function showpopSucess(msg, title) {
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-center",
                "preventDuplicates": true,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "9000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            // toastr['success'](msg, title);
            var d = Date();
            toastr.success(msg, title);
            return false;
        }
    </script>
     <link href="css/thankyou.css" rel="stylesheet" />
   
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <asp:UpdatePanel ID="updatepanel1" runat="server" >
            <ContentTemplate>
        <!-- Main content -->
        <section class="content">
  <div class="row">
                 <!-- left column -->
            <div class="col-md-12">
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
                  
              <!-- general form elements -->
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">View & Update Empty SetNo</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">

<div class="row11">
 <div class="panel panel-default">
  <div class="panel-body">
         <div class="form-group col-xs-3">

   <asp:TextBox ID="txtsearchbyAadharNo" runat="server" class="form-control" 
                            placeholder="Search By AadharNo"></asp:TextBox>
          </div>
       <div class="form-group col-xs-3">

   <asp:TextBox ID="txtsearchbyFormNo" runat="server" class="form-control" 
                            placeholder="Search By Form No"></asp:TextBox>
          </div>
      <%-- <div class="form-group col-xs-3">
   <asp:TextBox ID="txtserachbysetno" runat="server" class="form-control" 
                            placeholder="Search By Set No"></asp:TextBox>
          </div>--%>
       <div class="form-group col-xs-1">
      
     <asp:Button ID="btnSearch" runat="server" Text="Find"
                          class="btn btn-info" OnClick="btnSearch_Click" ></asp:Button>
          </div>
      <div class="form-group col-xs-1">
      
     <asp:Button ID="btnFilterReset" runat="server" Text="Reset"
                          class="btn btn-danger" OnClick="btnFilterReset_Click" ></asp:Button>
          </div>
          </div>
  </div>
</div>
    
                     
   </div>
  

    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false"  AllowPaging="true"
        Width="100%" CssClass="table table-bordered" OnRowDataBound="grvData_RowDataBound" OnRowCancelingEdit="grvData_RowCancelingEdit" OnRowEditing="grvData_RowEditing" OnRowUpdating="grvData_RowUpdating" OnPageIndexChanging="grvData_PageIndexChanging" >
        <Columns>
               <asp:TemplateField>  
                    <ItemTemplate>  
                        <asp:Button ID="btn_Edit" runat="server" Text="Edit" CommandName="Edit" />  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update"/>  
                        <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                    </EditItemTemplate>  
                </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Set No">
                    <ItemTemplate>  
                        <asp:Label ID="lblSetNo" runat="server" Text='<%# Bind("setno") %>'></asp:Label>  
                    </ItemTemplate>
                       <EditItemTemplate>  
                       <asp:TextBox ID="txt_EditSetNo" runat="server" Text='<%#Eval("setno") %>'></asp:TextBox>
                    </EditItemTemplate>    
                </asp:TemplateField>  
          
               <asp:BoundField DataField="ref_no" HeaderText="RefNo" ReadOnly="true" />
                             <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="true"  />
                             <asp:BoundField DataField="name" HeaderText="Name" ReadOnly="true" />
                              <%-- <asp:BoundField DataField="address" HeaderText="Address" ReadOnly="true" />--%>
                               
                                <asp:BoundField DataField="district" HeaderText="District" ReadOnly="true" />
                                <asp:BoundField DataField="state" HeaderText="State" ReadOnly="true" />
             <asp:BoundField DataField="phone" HeaderText="Phone" ReadOnly="true" />
                                <asp:BoundField DataField="aadhaar_card" HeaderText="Aadhaar Card" ReadOnly="true"  />
                                 <asp:BoundField DataField="No_of_Forms" HeaderText="No_ofForms" ReadOnly="true" />
                                 <asp:BoundField DataField="ListName" HeaderText="ListName" ReadOnly="true" />
         
               <asp:TemplateField HeaderText="Form No">
                    <ItemTemplate>  
                        <asp:Label ID="lblFormID" runat="server" Text='<%# Bind("formno") %>'></asp:Label>  
                    </ItemTemplate>  
                     <EditItemTemplate>  
                       <asp:Label ID="lbl_EditFormNo" runat="server" Text='<%#Eval("formno") %>'></asp:Label>
                    </EditItemTemplate>    
                </asp:TemplateField>  
            
            
        </Columns>
    </asp:GridView>
   
                 </ContentTemplate>

        </asp:UpdatePanel>
                  </div>
    
</asp:Content>

