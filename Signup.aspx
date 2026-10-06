<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/singlepage.master" AutoEventWireup="true" CodeFile="Signup.aspx.cs" Inherits="WebPortal_IT_Wing_Signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
  <div class="row">
                 <!-- left column -->
            <div class="col-md-6">
             <div class="box-body">
                 <div id="AlertAfterSignUp" runat="server" visible="false" class="alert alert-success alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h4>	<i class="icon fa fa-check"></i> Alert!</h4>
                    Your Account has been Created Sucessfully! You can now Login By entering your Email and Password!!
                  </div>
                    <div id="AlertErrorMsg" runat="server" visible="false" class="alert alert-danger alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                    
                   <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                  </div>
                  </div>
                  <%--<section class="content-header">--%>
                   <ol class="breadcrumb">
            <li>
            <a href="login.aspx" class="text-center">
           <%-- <i class="fa fa-dashboard"></i> --%>
            Login</a>
            </li>
            </ol>
          <%--  </section>--%>
              <!-- general form elements -->
             <asp:Panel ID="pnlsingup" runat="server">
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Sign Up</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                    <div class="form-group">
                      <label>Your Name</label>
                      <asp:TextBox ID="txtname" runat="server" class="form-control" placeholder="Your Name" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatornam" runat="server" ControlToValidate="txtname"
                    ErrorMessage="Enter Name !!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
               
                     <div class="form-group">
                      <label>Mobile Number</label>
                      <%--onkeydown="return validate(event);"--%>
                      <asp:TextBox ID="txtmobile" runat="server" class="form-control" 
                             placeholder="Mobile Number" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtmobile"
                    ErrorMessage="Enter Mobile No!!!" ValidationGroup="sav" ForeColor="red"></asp:RequiredFieldValidator>
                    <%-- <asp:RangeValidator ID="RangeValidatormob" runat="server" ControlToValidate="txtmobile"
                    ErrorMessage="Check Mobile Number" MaximumValue="9999999999" MinimumValue="7000000000"
                    SetFocusOnError="True" Type="Double" ValidationGroup="sav">*</asp:RangeValidator>--%>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtmobile"
                      ErrorMessage="Enter Valid Mobile No!!" ValidationExpression="^[0-9]{10,12}$" ValidationGroup="sav" ForeColor="red"></asp:RegularExpressionValidator>
                    </div>
                      
                    <div class="form-group">
                      <label>Email Id</label>
                      <asp:TextBox type="email" ID="txtprimaryemail" runat="server" class="form-control" placeholder="Enter Email Address"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtprimaryemail"
                    ErrorMessage="Enter Email Id!!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidatoreml" runat="server"
                    ControlToValidate="txtprimaryemail" ErrorMessage="Wrong E-mail Format" SetFocusOnError="True"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="sav" ForeColor="red"></asp:RegularExpressionValidator>
                   </div>
                     <div class="form-group">
                      <label>Password</label>
                      <asp:TextBox ID="txtpassword" runat="server" class="form-control" placeholder="Enter Password"></asp:TextBox>
                         
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" ControlToValidate="txtpassword" ForeColor="red"
                    ErrorMessage="Enter Password!!!" SetFocusOnError="True" ValidationGroup="sav" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                      
                   </div>
                        <div class="form-group">
                      <label>Retype Password</label>
                      <asp:TextBox ID="txtretypepassword" runat="server" 
                          class="form-control" placeholder="Retype Password"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtretypepassword"
                    ErrorMessage="Enter confirm password!!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                      <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToCompare="txtpassword" ControlToValidate="txtretypepassword" SetFocusOnError="True" ValidationGroup="sav" Display="Dynamic" 
                            ErrorMessage="Password Not Match !!" ForeColor="red"></asp:CompareValidator>
                    </div>
              

                  <div class="box-footer">
                   <asp:Button ID="btnsubmit" runat="server" Text="Sign Up" class="btn btn-success" 
                          ValidationGroup="sav" onclick="btnsubmit_Click"></asp:Button>
                   <asp:Button ID="btnreset" runat="server" Text="Reset" class="btn btn-danger" 
                          onclick="btnreset_Click"></asp:Button>
                  </div>
              <!-- /.box -->
              </div><!-- /.box -->
              </asp:Panel>

              </div>
              </div>
              </section>
             </div><!-- Content ./wrapper -->
</asp:Content>

