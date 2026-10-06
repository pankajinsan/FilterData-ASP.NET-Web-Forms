<%@ Page Title="List B Data Entry" Language="C#" MasterPageFile="~/entry.master" AutoEventWireup="true" CodeFile="Approve_ListB_Entry.aspx.cs" Inherits="Approve_ListB_Entry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function hide() {
            setTimeout(function () {
                $("[id*=AlertAfterSignUp]").fadeOut("slow");
            }, 5000);
        }
        function hideError() {
            setTimeout(function () {
                $("[id*=AlertErrorMsg]").fadeOut("slow");
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
            <div class="col-md-12">
             <div class="box-body">
                 <div id="AlertAfterSignUp" runat="server" visible="false" class="alert alert-success alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h4>	<i class="icon fa fa-check"></i> Alert!</h4>
                    List B Data Saved Sucessfully!!!
                  </div>
                    <div id="AlertErrorMsg" runat="server" visible="false" class="alert alert-danger alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                    <h4><i class="icon fa fa-ban"></i> Alert!</h4>
                    
                   <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                  </div>
                  </div>
                  <%--<section class="content-header">--%>
                  
          <%--  </section>--%>
              <!-- general form elements -->
           
              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">Approve List B Data</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                   <div class="row">
                    <div class="col-xs-3">
                     
                      <asp:TextBox ID="txtAadharCardNo" runat="server" class="form-control" placeholder="Enter Aadhar Card No" MaxLength="12" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAadharCardNo"
                    ErrorMessage="Enter Aadhar Card No !!!" SetFocusOnError="True" ValidationGroup="serach" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                  <div class="col-xs-3">
                      
                   <asp:Button ID="btnSearch" runat="server" Text="Find"
                          class="btn btn-warning" OnClick="btnSearch_Click" ValidationGroup="serach"></asp:Button>
                  </div>

                   </div>
                   <asp:Panel ID="pnlsingup" runat="server" Visible="false">
                       <asp:HiddenField ID="HiddenField1" runat="server" />
                    <div class="row">
                        <div class="col-xs-4">
                      <label>Name</label>
                      <asp:TextBox ID="txtname" runat="server" class="form-control" placeholder="Name" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatornam" runat="server" ControlToValidate="txtname"
                    ErrorMessage="Enter Name !!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                <div class="col-xs-2">
                      <label>Relation</label>
                       <asp:DropDownList ID="ddlRelation" runat="server" class="form-control">
                        <asp:ListItem>S/O</asp:ListItem>
                        <asp:ListItem>C/O</asp:ListItem>
                        <asp:ListItem>W/O</asp:ListItem>
                        <asp:ListItem>D/O</asp:ListItem>
                      </asp:DropDownList>
                    <%--  <asp:TextBox ID="txtRelation" runat="server" class="form-control" placeholder="Relation" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRelation"
                    ErrorMessage="Enter Relation !!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                    </div>
                         <div class="col-xs-4">
                      <label>Relation Name</label>
                      <asp:TextBox ID="txtRelationName" runat="server" class="form-control" placeholder="Relation Name" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRelationName"
                    ErrorMessage="Enter Relation Name !!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                        </div>
                      
                       <div class="row">
                       <div class="col-xs-5">
                      <label>Address</label>
                      <asp:TextBox ID="txtAddress" runat="server" class="form-control" placeholder="Address" TextMode="MultiLine" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAddress"
                    ErrorMessage="Enter Address !!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                       <div class="col-xs-3">
                      <label>District</label>
                      <asp:TextBox ID="txtDistrict" runat="server" class="form-control" placeholder="District" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDistrict"
                    ErrorMessage="Enter District !!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                       <div class="col-xs-3">
                      <label>State</label>
                      <asp:TextBox ID="txtState" runat="server" class="form-control" placeholder="State" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtState"
                    ErrorMessage="Enter State !!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                           </div>

                        <div class="row">
                       <div class="col-xs-3">
                      <label>Phone Number</label>
                      <%--onkeydown="return validate(event);"--%>
                      <asp:TextBox ID="txtmobile" runat="server" class="form-control" 
                             placeholder="Phone Number" MaxLength="10"></asp:TextBox>
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtmobile"
                    ErrorMessage="Enter Phone Number!!!" ValidationGroup="sav" ForeColor="red"></asp:RequiredFieldValidator>--%>
                    <%-- <asp:RangeValidator ID="RangeValidatormob" runat="server" ControlToValidate="txtmobile"
                    ErrorMessage="Check Mobile Number" MaximumValue="9999999999" MinimumValue="7000000000"
                    SetFocusOnError="True" Type="Double" ValidationGroup="sav">*</asp:RangeValidator>--%>
                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtmobile"
                      ErrorMessage="Enter Valid Mobile No!!" ValidationExpression="^[0-9]{10,12}$" ValidationGroup="sav" ForeColor="red"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-xs-3">
                      <label>Date of Birth (DD-MM-YYYY)</label>
                      <asp:TextBox ID="txtdob" runat="server" class="form-control" placeholder="Date of Birth (DD-MM-YYYY)" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdob"
                    ErrorMessage="Enter Date Of Birth !!!" SetFocusOnError="True" ValidationGroup="sav" ForeColor="red" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                              Enabled="True" TargetControlID="txtdob" Format="dd-MM-yyyy">
                          </asp:CalendarExtender>
                    </div>
                
                  </div>
                  <div class="box-footer">
                   <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="btn btn-success" 
                          ValidationGroup="sav" onclick="btnsubmit_Click"></asp:Button>
                   <asp:Button ID="btnreset" runat="server" Text="Reset" class="btn btn-danger" 
                          onclick="btnreset_Click"></asp:Button>
                  </div>
                      </asp:Panel>

                      

                      <br />
<br />


<br />




<br />




<br />




<br />



<br />

<br />



<br />


<br />


              <!-- /.box -->
              </div><!-- /.box -->
                  </div>
              
              <div id="DivPendingListB_data" runat="server" class="box box-info" visible="false">
                <div class="box-header">
                  <h3 class="box-title">
                      Pending Approval List B Data</h3>
                </div><!-- /.box-header -->
                  <div class="box-body">
                   <div class="row">
                       <div class="col-md-12">
                       <asp:Label ID="lbltotal" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="#009933"></asp:Label>


                  <asp:GridView ID="grdSrchData" runat="server" AutoGenerateColumns="False" 
        Width="100%" DataKeyNames="id" CssClass="table table-bordered table-striped" OnRowDataBound="grdSrchData_RowDataBound" EmptyDataText="No Rows Found" 
                       AllowPaging="true" PageSize="30" OnPageIndexChanging="grdSrchData_PageIndexChanging"
        >
        <Columns>
     
           <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>  
                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                             <asp:BoundField DataField="name" HeaderText="Name" />
                             <asp:BoundField DataField="relation" HeaderText="Rel"  />
                             <asp:BoundField DataField="relation_name" HeaderText="RelName" />
                              <asp:BoundField DataField="address" HeaderText="Address" />
                                <asp:BoundField DataField="district" HeaderText="District" />
                                <asp:BoundField DataField="state" HeaderText="State" />
                                <asp:BoundField DataField="phone" HeaderText="Phone"  />
                                
                               <asp:BoundField DataField="aadhaar_card" HeaderText="AadhaarCard" />
        </Columns>
        <HeaderStyle 
            HorizontalAlign="Left" />
    </asp:GridView>
</div>
</div>
                      </div></div>
              
              </div>
              </div>
              </section>
                </ContentTemplate>
              </asp:UpdatePanel>
             </div><!-- Content ./wrapper -->
</asp:Content>

