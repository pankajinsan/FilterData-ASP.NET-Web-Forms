<%@ Page Title="" Language="C#" MasterPageFile="~/entry.master" AutoEventWireup="true" CodeFile="AutomationMerge.aspx.cs" Inherits="AutomationMerge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <script type="text/javascript">
        function hide() {
            setTimeout(function () {
                $("[id*=AlertAfterSignUp]").fadeOut("slow");
            }, 5000);
        }
        function ShowModelPopUP()
        { $("[id*=ModalForm]").modal('show'); }
    </script>
    
    <style type="text/css">
        .auto-style1 {}
    </style>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>

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
                  
              
<%--<script type="text/javascript" src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js"></script>
<link href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(function () {
        $.ajax({
            type: "POST",
            url: "AutomationMerge.aspx/GetCustomers",
            //timeout: 30000,
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    });
   
                           
                             
    function OnSuccess(response) {
     
        $("#ContentPlaceHolder1_grdSrchData").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: false,
            bSort: true,
            bPaginate: true,
            data: response.d,
            columns: [{ 'data': 'id' },
                      { 'data': 'ref_no' },
                      { 'data': 'date' },
                      { 'data': 'name' },
                      { 'data': 'fathers_name' },
                      { 'data': 'address' },
                      { 'data': 'city' },
                      { 'data': 'district' },
                      { 'data': 'state' },
                      { 'data': 'mobile_no' },
                      { 'data': 'No_of_Forms' },
                      { 'data': 'ListName' }
            ]
        });
    };
</script>--%>

              <!-- general form elements -->
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Automation Merge Data</h3>
                   <div id="Div1" runat="server" class="alert alert-success alert-dismissable" style="float:right">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    
                    Matching Criteria: 1. Full Name,Father Name, District, State<br />
                       2. Full Name,District, State<br />
                       3. First Name,District, State<br />
                       System will Generate Unique SetNumber After Final Submit<br />
                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Show Only State Wise Results" Checked="True" />
                  </div>
                   
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
      <div class="row">
 
       <div class="form-group col-xs-2">

   <asp:TextBox ID="txtAadharCard" Width="200px" runat="server" class="form-control" 
                            placeholder="Enter Aadhar Card No"></asp:TextBox>
          </div>
       <div class="form-group col-xs-1">
       <%--  <label> &nbsp;&nbsp;&nbsp;&nbsp;</label>--%>
     <asp:Button ID="btnfindAAdhar" runat="server" Text="Find" OnClientClick="this.disabled = true; this.value = 'Search...';" 
  UseSubmitBehavior="false" class="btn btn-primary" OnClick="btnfindAAdhar_Click" ></asp:Button>
          </div>
              <div class="form-group col-xs-1">
      
     <asp:Button ID="btnRest" runat="server" Text="Reset"
                          class="btn btn-danger" OnClick="btnRest_Click" ></asp:Button>
          </div>    
   </div>
          
        <%-- <div class="form-group col-xs-2">

   <asp:TextBox ID="txtsearchbyName" runat="server" class="form-control" 
                            placeholder="Search By Name"></asp:TextBox>
          </div>
       <div class="form-group col-xs-2">

   <asp:TextBox ID="txtsearchbyState" runat="server" class="form-control" 
                            placeholder="Search By State"></asp:TextBox>
          </div>
       <div class="form-group col-xs-2">

   <asp:TextBox ID="txtsearchbyDistrict" runat="server" class="form-control" 
                            placeholder="Search By District"></asp:TextBox>
          </div>
       <div class="form-group col-xs-2">

   <asp:TextBox ID="txtsearchbyAddress" runat="server" class="form-control" 
                            placeholder="Search By Address"></asp:TextBox>
          </div>
       <div class="form-group col-xs-1">
       
     <asp:Button ID="btnsave" runat="server" Text="Find"
                          class="btn btn-info" OnClick="btnsave_Click" ></asp:Button>
          </div>
      <div class="form-group col-xs-1">
       
     <asp:Button ID="btnFilterReset" runat="server" Text="Reset"
                          class="btn btn-danger" OnClick="btnFilterReset_Click" ></asp:Button>--%>
          </div>
  </div>
</div>
    
                     
   </div>
   
 
                       <h4 class="box-title">ListA Data</h4>
               <div class="box box-secondary">
                <div class="box-header">
                 
                     <div class="form-group col-xs-2">
                      <asp:TextBox ID="txtfilter" runat="server" class="form-control" onKeyUp="SerachByNameFliter(this)"  placeholder="Search By Name"></asp:TextBox>
                      </div>
                     <div class="form-group col-xs-2">
                      <asp:TextBox ID="TextBox1" runat="server" class="form-control" onKeyUp="SerachByFatherNameFliter(this)"  placeholder="Search By Father Name"></asp:TextBox>
                      </div>
                     <div class="form-group col-xs-3">
                      <asp:TextBox ID="TextBox2" runat="server" class="form-control" onKeyUp="SerachByAddressFliter(this,5)"  placeholder="Search By Address"></asp:TextBox>
                      </div>
                     <div class="form-group col-xs-2">
                      <asp:TextBox ID="txtsearchbyCity" runat="server" class="form-control" onKeyUp="SerachByAddressFliter(this,6)"  placeholder="Search By City"></asp:TextBox>
                      </div>
                     <div class="form-group col-xs-2">
                      <asp:TextBox ID="txtSearchByDistrict" runat="server" class="form-control" onKeyUp="SerachByAddressFliter(this,7)"  placeholder="Search By District"></asp:TextBox>
                      </div>
                </div><!-- /.box-header -->
         
                  <div class="box-body">    
                       
<div style="height:200px; overflow:scroll">
   <%-- <inpu type='text' onkeyup='Filter(this);'/>--%>

    
    <asp:GridView ID="grdSrchData" runat="server" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="id" CssClass="table table-bordered table-striped" OnRowDataBound="grdSrchData_RowDataBound" EmptyDataText="No Rows Found"
        >
        <Columns>
              <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>  
                        <asp:CheckBox ID="CheckBox1" runat="server" />  
                    </ItemTemplate>  
                </asp:TemplateField> 
           <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>  
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                             <asp:BoundField DataField="ref_no" HeaderText="RefNo" />
                             <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}"  />
                             <asp:BoundField DataField="name" HeaderText="Name" />   <%--3--%>
                              <asp:BoundField DataField="fathers_name" HeaderText="FatherName" />  <%-- 4--%>
                               <asp:BoundField DataField="address" HeaderText="Address" />
                                <asp:BoundField DataField="city" HeaderText="City" />
                                <asp:BoundField DataField="district" HeaderText="District" />
                                <asp:BoundField DataField="state" HeaderText="State" />
                                <asp:BoundField DataField="mobile_no" HeaderText="MobileNo"  />
                                 <asp:BoundField DataField="No_of_Forms" HeaderText="No_ofForms" />
                                 <asp:BoundField DataField="ListName" HeaderText="ListName" />
        </Columns>
        <HeaderStyle 
            HorizontalAlign="Left" />
    </asp:GridView>
    </div>
</div></div>

<!--Start -->

  <div class="box box-success">
                <div class="box-header">
                  <h3 class="box-title">ListB Data</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
 
<div style="height:100px; overflow:scroll">
    <asp:GridView ID="grvAadharVCard" runat="server" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="id" CssClass="table table-bordered table-striped" OnRowDataBound="grvAadharVCard_RowDataBound" EmptyDataText="No Rows Found"
        >
        <Columns>
            
         <%--  <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>  
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  --%>
                             <asp:BoundField DataField="id" HeaderText="id" />
                             <asp:BoundField DataField="name" HeaderText="Name" />
                             <asp:BoundField DataField="relation" HeaderText="Rel"  />
                             <asp:BoundField DataField="relation_name" HeaderText="RelName" />
                              <asp:BoundField DataField="address" HeaderText="Address" />
                                <asp:BoundField DataField="district" HeaderText="District" />
                                <asp:BoundField DataField="state" HeaderText="State" />
                                <asp:BoundField DataField="phone" HeaderText="Phone"  />
                                 <asp:BoundField DataField="dob" HeaderText="Dob"  DataFormatString="{0:dd-MMM-yyyy}" />
                               <asp:BoundField DataField="aadhaar_card" HeaderText="AadhaarCard" />
        </Columns>
        <HeaderStyle 
            HorizontalAlign="Left" />
    </asp:GridView>
    </div>
</div></div>
<div class="row">
 
       <div class="form-group col-xs-2">
           <asp:Button ID="btnMerge" runat="server" Text="Merge"
                          class="btn btn-warning" OnClick="btnMerge_Click" ></asp:Button>
          </div>
       <div class="form-group col-xs-2">
          </div>
                     
   </div>
                      
  <div class="panel panel-default">
  <div class="panel-body">
      Name:&nbsp;&nbsp;&nbsp;&nbsp;   <asp:Label ID="lblname" runat="server" Font-Bold="True"></asp:Label>   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Address:&nbsp;&nbsp;   <asp:Label ID="lbladdress" runat="server" Font-Bold="True"></asp:Label>
      <br />
      District:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="lbldistrict" runat="server" Font-Bold="True"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <asp:Label ID="lblstate" runat="server" Font-Bold="True"></asp:Label>
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Aadhar No:&nbsp;&nbsp;
      <asp:Label ID="lblAadharNo" runat="server" Font-Bold="True"></asp:Label>
      </div></div>
<div style="height:200px; overflow:scroll">
    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" 
        Width="100%" CssClass="table table-bordered" OnRowDataBound="grvData_RowDataBound">
        <Columns>
          
             <asp:BoundField DataField="name" HeaderText="Name" />
                             <asp:BoundField DataField="address" HeaderText="Address"  />
                             <asp:BoundField DataField="district" HeaderText="District" />
                              <asp:BoundField DataField="state" HeaderText="State" />
            <asp:BoundField DataField="phone" HeaderText="Phone"  />
                                <asp:BoundField DataField="aadhaar_card" HeaderText="Aadhar No" />
                    <asp:TemplateField HeaderText="SrNo" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Left">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
            
             <asp:BoundField DataField="date" HeaderText="Date"  DataFormatString="{0:dd-MMM-yyyy}" />
             <asp:BoundField DataField="No_of_Forms" HeaderText="No.of Forms" />
                                
             <asp:BoundField DataField="ref_no" HeaderText="RefNo" />
            <asp:BoundField DataField="ListName" HeaderText="ListName" />
        </Columns>
    </asp:GridView>
    </div>
<div class="row">
 
       <div class="form-group col-xs-1">
           <asp:Button ID="btnsubmit" runat="server" Text="Submit"
                          class="btn btn-success" OnClick="btnsubmit_Click" ></asp:Button>
          </div>
       <div class="form-group col-xs-1">
           <asp:Button ID="btnReset" runat="server" Text="Reset"
                          class="btn btn-danger" OnClick="btnReset_Click" ></asp:Button>
          </div>
                     
   </div>




     </div>
                  </div>
                  </div>
     

                  </div>
                  </section>
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
                               
                                <h1>Thank You!</h1>
                                <p>Data Submitted Successfully</p>
                                <h3 class="cupon-pop">Your Form No: <span><asp:Label ID="lblsetnumber" runat="server" Text=""></asp:Label></span></h3>

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
   
     <link href="css/thankyou.css" rel="stylesheet" />
   <script type="text/javascript">
       function SerachByNameFliter(Obj) {
           var grid = document.getElementById("<%=grdSrchData.ClientID%>");
           var terms = Obj.value;
           var isExists = false;
           var ele;
           if (grid.rows.length > 0) {
               for (var r = 1; r < grid.rows.length; r++) {
                   isExists = false;
                   for (var j = 0; j < grid.rows[0].cells.length; j++) {
                       ele = grid.rows[r].cells[3].innerHTML;
                        
                       if (ele.toLowerCase().indexOf(terms.toLowerCase()) >= 0)
                           isExists = true;
                   }
                   if (!isExists)
                       grid.rows[r].style.display = 'none';
                   else
                       grid.rows[r].style.display = '';
               }
           }
       };
       function SerachByFatherNameFliter(Obj) {
           var grid = document.getElementById("<%=grdSrchData.ClientID%>");
           var terms = Obj.value;
           var isExists = false;
           var ele;
           if (grid.rows.length > 0) {
               for (var r = 1; r < grid.rows.length; r++) {
                   isExists = false;
                   for (var j = 0; j < grid.rows[0].cells.length; j++) {
                       ele = grid.rows[r].cells[4].innerHTML;
                        
                       if (ele.toLowerCase().indexOf(terms.toLowerCase()) >= 0)
                           isExists = true;
                   }
                   if (!isExists)
                       grid.rows[r].style.display = 'none';
                   else
                       grid.rows[r].style.display = '';
               }
           }
       };
       function SerachByAddressFliter(Obj,ColValue) {
           var grid = document.getElementById("<%=grdSrchData.ClientID%>");
           var terms = Obj.value;
           var isExists = false;
           var ele;
           if (grid.rows.length > 0) {
               for (var r = 1; r < grid.rows.length; r++) {
                   isExists = false;
                   for (var j = 0; j < grid.rows[0].cells.length; j++) {
                       ele = grid.rows[r].cells[ColValue].innerHTML;
                        
                       if (ele.toLowerCase().indexOf(terms.toLowerCase()) >= 0)
                           isExists = true;
                   }
                   if (!isExists)
                       grid.rows[r].style.display = 'none';
                   else
                       grid.rows[r].style.display = '';
               }
           }
       };
        function SerachByDistrictFliter(Obj) {
           var grid = document.getElementById("<%=grdSrchData.ClientID%>");
           var terms = Obj.value;
           var isExists = false;
           var ele;
           if (grid.rows.length > 0) {
               for (var r = 1; r < grid.rows.length; r++) {
                   isExists = false;
                   for (var j = 0; j < grid.rows[0].cells.length; j++) {
                       ele = grid.rows[r].cells[8].innerHTML;
                        
                       if (ele.toLowerCase().indexOf(terms.toLowerCase()) >= 0)
                           isExists = true;
                   }
                   if (!isExists)
                       grid.rows[r].style.display = 'none';
                   else
                       grid.rows[r].style.display = '';
               }
           }
       };
   </script>
</asp:Content>

