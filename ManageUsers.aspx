<%@ Page Title="" Language="C#" MasterPageFile="~/entry.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
      
        function hide() {
            setTimeout(function () {
                $("[id*=AlertAfterSignUp]").fadeOut("slow");
            }, 2000);
        }
    </script>
    <style type="text/css">
        .auto-style1 {}
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
                  <h3 class="box-title">Manage User Access</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
<center>
<div style="color:Red; font-weight:bold" id="divMsg">
<asp:Literal ID="litMsg" runat="server"></asp:Literal>
</div>
</center>

     <div class="form-group col-xs-2">

   <asp:TextBox ID="txtsearchbyName" runat="server" class="form-control" 
                            placeholder="Search By Name"></asp:TextBox>
          </div>
       <div class="form-group col-xs-2">

   <asp:TextBox ID="txtsearchbyMobileNo" runat="server" class="form-control" 
                            placeholder="Search By MobileNo"></asp:TextBox>
          </div>
      
      <div class="form-group col-xs-1">
       
     <asp:Button ID="btnFilter" runat="server" Text="Search"
                          class="btn btn-info" OnClick="btnFilter_Click" ></asp:Button>
     </div>
                    

    <asp:GridView ID="grdSrchData" runat="server" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="UserID" CssClass="table table-bordered table-striped" OnRowDataBound="grdSrchData_RowDataBound" EmptyDataText="No Rows Found" OnRowCancelingEdit="grdSrchData_RowCancelingEdit" OnRowEditing="grdSrchData_RowEditing" OnRowUpdating="grdSrchData_RowUpdating"
         AllowPaging="true" OnPageIndexChanging="grdSrchData_PageIndexChanging">
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
           <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>  
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
              <asp:TemplateField HeaderText="Username">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Username" runat="server" Text='<%#Eval("Username") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                       <asp:Label ID="lbl_EditUsername" runat="server" Text='<%#Eval("Username") %>'></asp:Label>
                    </EditItemTemplate>  
                </asp:TemplateField>  
                                  <asp:TemplateField HeaderText="Name">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Name") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                       <asp:Label ID="lbl_EditName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                    </EditItemTemplate>  
                </asp:TemplateField>  
               <asp:TemplateField HeaderText="Mobile">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Mobile" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                       <asp:Label ID="lbl_EditMobile" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>
                    </EditItemTemplate>  
                </asp:TemplateField>  
                            

             <asp:TemplateField HeaderText="ListAccess">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_ListAccess" runat="server" Text='<%#Eval("ListAccess") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:DropDownList ID="ddlAllList" runat="server">
                </asp:DropDownList>  
                    </EditItemTemplate>  
                </asp:TemplateField>  
                   <asp:TemplateField HeaderText="UserRole">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_UserRole" runat="server" Text='<%#Eval("Role") %>'></asp:Label>  
                    </ItemTemplate>  
                    <EditItemTemplate>  
                        <asp:DropDownList ID="ddlUserRole" runat="server">
                            <asp:ListItem>User</asp:ListItem>
                            <asp:ListItem>Incharge</asp:ListItem>
                             <asp:ListItem>ListB-Approver</asp:ListItem>
                </asp:DropDownList>  
                    </EditItemTemplate>  
                </asp:TemplateField>   
               <asp:TemplateField HeaderText="Password">  
                    <ItemTemplate>  
                        <asp:Label ID="lbl_Password" runat="server" Text='<%#Eval("Password") %>'></asp:Label>  
                    </ItemTemplate>  
                 
                </asp:TemplateField>  
                  
        </Columns>
        <HeaderStyle 
            HorizontalAlign="Left" />
    </asp:GridView>
   
                 </ContentTemplate>

        </asp:UpdatePanel>
                  </div>
</asp:Content>

