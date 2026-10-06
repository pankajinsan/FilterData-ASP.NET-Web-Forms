<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="dss_intranet_login" Title="Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <!-- Bootstrap 3.3.2 -->
    <link href="css/bootstrap.min.login.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome Icons -->
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- iCheck -->

    <link href="css/blue.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />--%>
</head>
<body class="login-page">
    <div class="login-box">
      <div class="login-logo">
        <a href="#"><b>Login</b></a>
      </div><!-- /.login-logo -->
      <div class="login-box-body">
    
        <p class="login-box-msg">  <asp:Literal ID="FailureText" runat="server" Text="Sign In with your credentials."></asp:Literal></p>
        <form id="form2" runat="server" >
          <div class="form-group has-feedback">
         <asp:TextBox ID="txtnam" class="form-control" runat="server" 
           placeholder="Email"></asp:TextBox>
             <asp:RequiredFieldValidator ID="UserNameRequired" 
           runat="server" ControlToValidate="txtnam" 
           ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup" 
           Font-Bold="False" ForeColor="#CC0000"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="RegularExpressionValidatoreml" runat="server"
                    ControlToValidate="txtnam" ErrorMessage="Wrong E-mail Format" SetFocusOnError="True" Display="Dynamic"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="LoginUserValidationGroup" ForeColor="#CC0000"></asp:RegularExpressionValidator>
            <%--<input type="text" class="form-control" placeholder="Email"/>--%>
            <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
          </div>
          <div class="form-group has-feedback">
           <asp:TextBox ID="txtpwd" class="form-control" placeholder="Password" 
           runat="server" TextMode="Password"></asp:TextBox>
           <asp:RequiredFieldValidator ID="PasswordRequired" 
           runat="server" ControlToValidate="txtpwd" 
           ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup" 
           ForeColor="#CC0000"></asp:RequiredFieldValidator>
            <%--<input type="password" class="form-control" placeholder="Password"/>--%>
            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
          </div>
          <div class="row">
            <div class="col-xs-8">    
              <div class="checkbox icheck">
                <label>
                 <%-- <input type="checkbox"> Remember Me--%>
                </label>
              </div>                        
            </div>
            <!-- /.col -->
            <div class="col-xs-4">
            <asp:Button ID="LoginButton" runat="server" Text="Sign In" class="btn btn-primary btn-block btn-flat" 
             ValidationGroup="LoginUserValidationGroup" onclick="btnlog_Click" />
              <%--<button type="submit" class="btn btn-primary btn-block btn-flat">Sign In</button>--%>
            </div><!-- /.col -->
          </div>
        </form>

      <%--  <div class="social-auth-links text-center">
          <p>- OR -</p>
          <a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i> Sign in using Facebook</a>
          <a href="#" class="btn btn-block btn-social btn-google-plus btn-flat"><i class="fa fa-google-plus"></i> Sign in using Google+</a>
        </div>--%>
        
        <!-- /.social-auth-links -->

       <%-- <a href="WebPortal/forgetpwd.aspx">I forgot my password</a><br--%>
        <a href="Signup.aspx" class="text-center">Register a new Account</a>

      </div><!-- /.login-box-body -->
    </div><!-- /.login-box -->

    <!-- jQuery 2.1.3 -->
    <script src="js/jQuery-2.1.3.min.js" type="text/javascript"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="js/bootstrap.min.login.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="js/icheck.min.js" type="text/javascript"></script>
   <%-- <script src="../../plugins/iCheck/icheck.min.js" type="text/javascript"></script>--%>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' // optional
            });
        });
    </script>
  </body>
</html>

