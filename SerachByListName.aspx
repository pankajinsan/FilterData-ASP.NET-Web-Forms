<%@ Page Title="" Language="C#" MasterPageFile="~/entry.master" AutoEventWireup="true" CodeFile="SerachByListName.aspx.cs" Inherits="SerachByListName" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
      
        function hide() {
            setTimeout(function () {
                $("[id*=AlertAfterSignUp]").fadeOut("slow");
            }, 2000);
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
                  <h3 class="box-title">Search By Aadhar No and Form No</h3>
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
   <asp:TextBox ID="txtsearchbySetNo" runat="server" class="form-control" 
                            placeholder="Search By Form No"></asp:TextBox>
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
   <div class="panel panel-default">
  <div class="panel-body">
      <asp:HiddenField ID="hiddenSetNo" runat="server" />
      Name:&nbsp;&nbsp;&nbsp;&nbsp;   <asp:Label ID="lblname" runat="server" Font-Bold="True"></asp:Label>   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Address:&nbsp;&nbsp;   <asp:Label ID="lbladdress" runat="server" Font-Bold="True"></asp:Label>
      <br />
      District:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="lbldistrict" runat="server" Font-Bold="True"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="lblstate" runat="server" Font-Bold="True"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Aadhar No:&nbsp;&nbsp;
      <asp:Label ID="lblAadharNo" runat="server" Font-Bold="True"></asp:Label>
      <br />
      List Name:&nbsp;&nbsp;
      <asp:Label ID="lblListName" runat="server" Font-Bold="True"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Form No:
      <asp:Label ID="lblFormNo" runat="server" Font-Bold="True"></asp:Label>
      </div></div>
<div style="height:300px; overflow:scroll">
    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" 
        Width="100%" CssClass="table table-bordered" OnRowDataBound="grvData_RowDataBound" >
        <Columns>
              <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>  
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                    <asp:TemplateField HeaderText="SrNo" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
            
             <asp:BoundField DataField="date" HeaderText="Date"  DataFormatString="{0:dd-MMM-yyyy}" />
             <asp:BoundField DataField="No_of_Forms" HeaderText="No.of Forms" />
                                
             <asp:BoundField DataField="ref_no" HeaderText="RefNo" />
        </Columns>
    </asp:GridView>
    </div>
<div class="row">
 
       <div class="form-group col-xs-1">
           <asp:Button ID="btnsubmit" runat="server" Text="Submit"
                          class="btn btn-success" OnClick="btnsubmit_Click" ></asp:Button>
          </div>   
    
   </div>

 <!--Model Popup starts-->
    <div class="container">
        <div class="row">
            <!--<a class="btn btn-primary" data-toggle="modal" href="#ignismyModal">open Popup</a>-->
            <div class="modal fade" id="ModalForm" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label=""><span>×</span></button>
                        </div>

                        <div class="modal-body">

                            <div class="thank-you-pop">
                                <img src="img/Green-Round-Tick.png" />
                               
                                <h1>
                                    <asp:Label ID="lblThankyou" runat="server" Text="Label"></asp:Label></h1>
                                <p>
                                    <asp:Label ID="lblPopupSucess" runat="server" Text="Label"></asp:Label></p>
                                <h3 class="cupon-pop">Your Set No is: <span><asp:Label ID="lblsetnumber" runat="server" Text=""></asp:Label></span></h3>

                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Model Popup ends-->

                 </ContentTemplate>

        </asp:UpdatePanel>
                  </div>
    
</asp:Content>

