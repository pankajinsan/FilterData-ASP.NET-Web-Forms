<%@ Page Title="" Language="C#" MasterPageFile="~/entry.master" AutoEventWireup="true" CodeFile="ViewMergeDetails.aspx.cs" Inherits="ViewMergeDetails" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
      
        function hide() {
            setTimeout(function () {
                $("[id*=AlertAfterSignUp]").fadeOut("slow");
            }, 2000);
        }
    </script>
     <link href="css/thankyou.css" rel="stylesheet" />
    <style type="text/css">
        .vertial-orientation {
            -webkit-transform: rotate(90deg);
            -moz-transform: rotate(90deg);
            -ms-transform: rotate(90deg);
            -o-transform: rotate(90deg);
            transform: rotate(90deg);
        }
    </style>
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
                  <h3 class="box-title">View Merge Entry Details</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
<center>
<div style="color:Red; font-weight:bold" id="divMsg">
<asp:Literal ID="litMsg" runat="server"></asp:Literal>
</div>
</center>
<div class="row11">
 <div class="panel panel-default">
  <div class="panel-body">
         <div class="form-group col-xs-3">
<%--<label>Search Name</label>--%>
   <asp:TextBox ID="txtsearchbyAadharNo" runat="server" class="form-control" 
                            placeholder="Search By AadharNo"></asp:TextBox>
          </div>
       <div class="form-group col-xs-3">
<%--<label>Search Name</label>--%>
   <asp:TextBox ID="txtsearchbyFormNo" runat="server" class="form-control" 
                            placeholder="Search By Form No"></asp:TextBox>
          </div>
       <div class="form-group col-xs-3">
<%--<label>Search Name</label>--%>
   <asp:TextBox ID="txtserachbysetno" runat="server" class="form-control" 
                            placeholder="Search By Set No"></asp:TextBox>
          </div>
       <div class="form-group col-xs-1">
      
     <asp:Button ID="btnSearch" runat="server" Text="Find"
                          class="btn btn-info" OnClick="btnSearch_Click" ></asp:Button>
          </div>
      <div class="form-group col-xs-1">
       <%--  <label> &nbsp;&nbsp;&nbsp;&nbsp;</label>--%>
     <asp:Button ID="btnFilterReset" runat="server" Text="Reset"
                          class="btn btn-danger" OnClick="btnFilterReset_Click" ></asp:Button>
          </div>
          </div>
  </div>
</div>
    
                     
   </div>
  

    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" 
        Width="100%" CssClass="table table-bordered" OnRowDataBound="grvData_RowDataBound" OnRowCancelingEdit="grvData_RowCancelingEdit" OnRowEditing="grvData_RowEditing" OnRowUpdating="grvData_RowUpdating" >
        <Columns>
                 
               <asp:TemplateField HeaderText="Form No">
                    <ItemTemplate>  
                        <asp:Label ID="lblFormID" runat="server" Text='<%# Bind("formno") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
               <asp:TemplateField HeaderText="Set No">
                    <ItemTemplate>  
                        <asp:Label ID="lblSetNo" runat="server" Text='<%# Bind("setno") %>'></asp:Label>  
                    </ItemTemplate>
                       <EditItemTemplate>  
                       <asp:TextBox ID="lbl_EditSetNo" runat="server" Text='<%#Eval("setno") %>'></asp:TextBox>
                    </EditItemTemplate>    
                </asp:TemplateField>  
              <asp:BoundField DataField="MergedByUserName" HeaderText="MergedBy UserName" />
              <asp:BoundField DataField="MergedByUserID" HeaderText="MergedBy UserID" />
              <asp:BoundField DataField="MergedByMobileNo" HeaderText="MergedBy MobileNo" />
              <asp:BoundField DataField="createdon" HeaderText="MergedOn"  DataFormatString="{0:dd-MMM-yyyy}" />
              <asp:BoundField DataField="SetNoByUserName" HeaderText="SetNoBy UserName" />
              <asp:BoundField DataField="SetNoByUserID" HeaderText="SetNoBy UserID" />
              <asp:BoundField DataField="SetNoByMobileNo" HeaderText="SetNoBy MobileNo" />
              <asp:BoundField DataField="setnocreatedon" HeaderText="SetNoCreatedON"  DataFormatString="{0:dd-MMM-yyyy}" />
        </Columns>
    </asp:GridView>
   
                 </ContentTemplate>

        </asp:UpdatePanel>
                  </div>
    
</asp:Content>

