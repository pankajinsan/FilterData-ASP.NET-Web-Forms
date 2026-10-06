<%@ Page Language="C#" AutoEventWireup="true" CodeFile="exportpdf.aspx.cs" Inherits="exportpdf" EnableEventValidation = "false" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .myclass{display:none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExport" runat="server" Text="Export" onclick="btnExport_Click" />

    <asp:Label ID="lblResult" runat="server" />
       &nbsp;&nbsp;
       <table style="width: 100%;">
           <th></th>
           <th colspan="2"></th>
           <tr>
               <td>&nbsp;</td>
               <td colspan="2">&nbsp;</td>
           </tr>
           <tr>
               <td>&nbsp;</td>
               <td>&nbsp;</td>
               <td>&nbsp;</td>
           </tr>
           <tr>
               <td>&nbsp;</td>
               <td>&nbsp;</td>
               <td>&nbsp;</td>
           </tr>
       </table>
   
</form>
</body>
</html>
