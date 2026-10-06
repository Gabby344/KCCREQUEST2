<%@ Page Title="Out of Office" Language="C#" MasterPageFile="~/OutOfOffice.Master" AutoEventWireup="true" CodeBehind="_OutOfOffice.aspx.cs" Inherits="KCCREQUEST2._OutOfOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

         <style type="text/css">
    
       .btn2 {
display: inline-block;
padding: 9px 12px;
padding-top: 7px;
margin-bottom: 0;
font-size: 14px;
line-height: 20px;
color: #5e5e5e;
text-align: center;
vertical-align: middle;
cursor: pointer;
background-color: #d1dade;

background-image: none !important;
border: none;
text-shadow: none;
box-shadow: none;
transition: all 0.12s linear 0s !important;
font: 14px/20px "Helvetica Neue",Helvetica,Arial,sans-serif;
}
.btn2-cons {
margin-right: 5px;
min-width: 80px;
margin-bottom: 8px;
}
       .btn2-btnSave {
color: #fff;
border-color: #357ebd;
        background-color:#e42526;
        }
         .btn2-btnReset {
color: #fff;
border-color: #357ebd;
        background-color:rgb(0,177,147);
        }
    </style>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;


        }
        window.onbeforeunload = DisableButton;
    </script>
    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1>
                        <asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, lblOutOfOffice%>" runat="server"></asp:Literal>
                        <small>
                            <i class="ace-icon fa fa-angle-double-right"></i>
                            <%-- New Request--%>
                        </small>
                    </h1>
                </div>
                <!--FIN TITRE DE LA PAGE-->


                <!--bloc pour les information du requisitionneur-->
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center;">
                        <h4 class="widget-title lighter">



                            <asp:Literal ID="lblOutOfOfficetxt" Text="<%$ Resources:LocalizedText, lblOutOfOfficetxt%>" runat="server"></asp:Literal></h4>
                    </div>

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%;" id="bodyItRequest">


                                    <table style="width: 100%; margin: auto; table-layout:fixed;">
                                        <tr>
                                            <td colspan="2">
                                                <hr />
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblComment" Text="<%$ Resources:LocalizedText, lblComment%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />


                                                    <asp:TextBox ID="txtComment" MaxLength="80" runat="server" Width="100%"></asp:TextBox>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblForUser" Text="<%$ Resources:LocalizedText, lblForUser%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />


                                                    <asp:DropDownList ID="LstForUser" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstForUser_SelectedIndexChanged"></asp:DropDownList>



                                                </div>
                                            </td>


                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblAssignTo" Text="<%$ Resources:LocalizedText, lblAssignTo%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />
                                                    <asp:DropDownList ID="LstAssignto" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%;">

                                                <div>
                                                    <label for="form-field-select-1">Application</label>
                                                    <br />

                                                    <%-- <asp:DropDownList ID="lstModule" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>--%>

                                                    
                                                    <asp:ListBox ID="lstModule" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true" SelectionMode="Multiple" OnSelectedIndexChanged="lstModule_SelectedIndexChanged"></asp:ListBox>
                                                  
                                                
                                                </div>
                                            </td>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblRole" Text="<%$ Resources:LocalizedText, lblRole%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />
                                                  

                                                            <%-- <asp:DropDownList ID="LstRole" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>--%>
                                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lstModule" />
                                            <asp:AsyncPostBackTrigger ControlID="LstRole" />
                                            
                                        </Triggers>
                                        <ContentTemplate>
                                                            <asp:ListBox ID="LstRole" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstRole_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                                               
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblDateFrom" Text="<%$ Resources:LocalizedText, lblDateFrom%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />


                                                    <asp:TextBox ID="txtDateFrom" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                <div>

                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblDateTo" Text="<%$ Resources:LocalizedText, lblDateTo%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />


                                                    <asp:TextBox ID="txtDateTo" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>



                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblTimeRange" Text="<%$ Resources:LocalizedText, lblTimeRange%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />


                                                    <asp:TextBox ID="txtTimeRangeStart" type="text" class="form-control" runat="server" Width="90%"></asp:TextBox>

                                                </div>
                                            </td>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblTo" Text="<%$ Resources:LocalizedText, lblTo%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />


                                                    <asp:TextBox ID="txtTimeRangeEnd" type="text" class="form-control" runat="server" Width="100%"></asp:TextBox>



                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:UpdatePanel runat="server" Visible="false" ID="pnlCheck" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div>
                                                            <label for="form-field-select-1">

                                                                <asp:Literal ID="lblOof" Text="<%$ Resources:LocalizedText, lblSetOutOfOffice%>" runat="server"></asp:Literal>
                                                                &nbsp; &nbsp;
                                                            <asp:CheckBox ID="chkSetOof" AutoPostBack="true" runat="server" OnCheckedChanged="chkSetOof_CheckedChanged"></asp:CheckBox>
                                                            </label>
                                                            <br />



                                                            <asp:Panel runat="server" ID="pnlMsg" class="col-lg-12" Visible="false">
                                                                <label>
                                                                    <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblAutoReplyMessage%>" runat="server"></asp:Literal></label>
                                                                <asp:TextBox ID="txtMsgReply" type="text" class="form-control" runat="server" Height="100%" Width="100%" TextMode="MultiLine" Rows="7"></asp:TextBox>
                                                            </asp:Panel>

                                                        </div>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>

                                            </td>

                                        </tr>
                                    </table>

                                    <div class="clearfix form-actions">
                                        <div class="col-md-offset-9 col-md-12">
                                            <asp:Button ID="btnSave" cssclass="btn2 btn2-btnSave btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnSave%>" OnClick="btnSave_Click" />
                                            &nbsp; &nbsp; &nbsp;
											 

                                        </div>

                                    </div>




                                    <div class="form-horizontal" id="Form2" style="width: 100%;">
                                        <!--bloc des onglets des detail-->
                                        <div class="tabbable">
                                            <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab4">
                                                <li class="active">
                                                    <a data-toggle="tab" href="#home4">


                                                        <asp:Literal ID="lblActiveOutOfOffice" Text="<%$ Resources:LocalizedText, lblActiveOutOfOffice%>" runat="server"></asp:Literal>
                                                        <span class="badge badge-danger" id="lblActive" runat="server">0</span>

                                                    </a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#profile4">

                                                        <asp:Literal ID="lblInactiveOutOfOffice" Text="<%$ Resources:LocalizedText, lblInactiveOutOfOffice%>" runat="server"></asp:Literal>

                                                        <span class="badge badge-danger" id="lblInactive" runat="server">0</span>
                                                    </a>
                                                </li>
                                            </ul>


                                            <div class="tab-content">
                                                <!--onglet details-->
                                                <div id="home4" class="tab-pane in active">
                                                    <div id="Div2" style="width: 100%; overflow-x: scroll;">

                                                        <div>
                                                        <p>
                                                            <asp:Button ID="selectall" runat="server" OnClick="Selectall_Click" Text="<%$ Resources:LocalizedText, selectall%>" class="btn btn-white btn-purple btn-sm" />
                                                            <asp:Button ID="Deselectall" runat="server" OnClick="Deselectall_Click" Text="<%$ Resources:LocalizedText, Deselectall%>"  class="btn btn-white btn-inverse btn-sm" />
                                                            <asp:Button ID="Inverse" runat="server" OnClick="Inverse_Click" Text="<%$ Resources:LocalizedText, Inverse%>" class="btn btn-white btn-pink btn-sm" />
                                                        </p>
                                                    </div>

                                                        <asp:GridView ID="dgvActiveOutoffice" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="Id" AllowPaging="true" PageSize="20" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="dgvActiveOutoffice_PageIndexChanging">
                                                            <Columns>


                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:CheckBox ID="chkRow" Checked="false" AutoPostBack="false" runat="server" />
                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                    <HeaderTemplate>
                                                                        ID
                                                
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <span class="badge badge" style="background-color:#003468">
                                                                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lbComment" Text="<%$ Resources:LocalizedText, lblComment%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Comment")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblUser" Text="<%$ Resources:LocalizedText, lblUser%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUser" runat="server" Text='<%#Eval("ToUser")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="17%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        Application
                                                
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblModule" runat="server" Text='<%#Eval("Module")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="7%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblRoleText" Text="<%$ Resources:LocalizedText, lblRole%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRole" runat="server" Text='<%#Eval("Role")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="10%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblStartDate" Text="<%$ Resources:LocalizedText, lblStartDate%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# DateTime.Parse(Eval("StartDate").ToString()).ToString("MM-dd-yyyy HH:mm:ss") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="10%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblEndDate" Text="<%$ Resources:LocalizedText, lblEndDate%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# DateTime.Parse(Eval("EndDate").ToString()).ToString("MM-dd-yyyy HH:mm:ss") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="7%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblCreatedByText" Text="<%$ Resources:LocalizedText, lbdgvCreateBy%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                    <HeaderTemplate></HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%--<asp:CheckBox ID="chkRow" Checked="false" AutoPostBack="false" runat="server" />--%>

                                                                        <%--<asp:ImageButton runat="server" ID="btnDisable" ImageUrl="~/images/reject.png" class="btn btn-default" OnClick="btnDisable_Click" ToolTip="<%$ Resources:LocalizedText, lblDisableOutofOffice%>"></asp:ImageButton>--%>
                                                                       
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <SelectedRowStyle CssClass="alert-info" />
                                                            <RowStyle CssClass="cursor-pointer" />
                                                            <HeaderStyle CssClass="alert-success" />
                                                            <PagerStyle HorizontalAlign="Center" />
                                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                        </asp:GridView>




                                                        <div class="clearfix form-actions" id="blockbtn" runat="server">
                                       
                                        <div>
                                             
     
                                               
                                                    <asp:Button ID="btnDisable" OnClick="btnDisable_Click1"  cssclass="btn2 btn2-btnSave btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnDisable%>"	 />
                                                 
											
                                        </div>
                                          </div>

                                                    </div>
                                                </div>

                                                <div id="profile4" class="tab-pane">
                                                    <div id="Div2" style="width: 100%; overflow-x: scroll;">
                                                        <asp:GridView ID="dgvInactiveOutOffice" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="Id" AllowPaging="true" PageSize="20" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False" OnPageIndexChanging="dgvInactiveOutOffice_PageIndexChanging">
                                                            <Columns>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                    <HeaderTemplate>
                                                                        ID
                                                
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <span class="badge badge" style="background-color:#003468">
                                                                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                                        </span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lbComment" Text="<%$ Resources:LocalizedText, lblComment%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblComment" runat="server" Text='<%#Eval("Comment")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblUser" Text="<%$ Resources:LocalizedText, lblUser%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUser" runat="server" Text='<%#Eval("ToUser")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="17%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        Application
                                                
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblModule" runat="server" Text='<%#Eval("Module")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="7%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblRoleText" Text="<%$ Resources:LocalizedText, lblRole%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRole" runat="server" Text='<%#Eval("Role")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="10%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblStartDate" Text="<%$ Resources:LocalizedText, lblStartDate%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# DateTime.Parse(Eval("StartDate").ToString()).ToString("MM-dd-yyyy HH:mm:ss") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="10%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblEndDate" Text="<%$ Resources:LocalizedText, lblEndDate%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# DateTime.Parse(Eval("EndDate").ToString()).ToString("MM-dd-yyyy HH:mm:ss") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="7%">
                                                                    <HeaderTemplate>

                                                                        <asp:Literal ID="lblCreatedByText" Text="<%$ Resources:LocalizedText, lbdgvCreateBy%>" runat="server"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                                                    <HeaderTemplate></HeaderTemplate>
                                                                    <ItemTemplate>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <SelectedRowStyle CssClass="alert-info" />
                                                            <RowStyle CssClass="cursor-pointer" />
                                                            <HeaderStyle CssClass="alert-success" />
                                                            <PagerStyle HorizontalAlign="Center" />
                                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>




                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--popup pour le message d'erreur-->


    <div id="modal_erreur" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #682525;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">
                        <asp:Label ID="txtInvalidInput" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, txtInvalidInput %>"></asp:Label>
                    </h3>
                </div>

                <div class="modal-body">

                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <%--  <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />--%>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <h4 class="smaller lighter red no-margin">
                                <asp:Label ID="lblErreur" runat="server" Text=""></asp:Label>
                            </h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        <asp:Literal ID="Literal2" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!--fin popup-->
    <!--[if !IE]> -->


    <script src="assets/js/jquery-2.1.4.min.js"></script>

    <!-- <![endif]-->

    <!--[if IE]>
<script src="assets/js/jquery-1.11.3.min.js"></script>
<![endif]-->
    <script type="text/javascript">
        if ('ontouchstart' in document.documentElement) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
    </script>
    <script src="assets/js/bootstrap.min.js"></script>

    <!-- page specific plugin scripts -->

    <script src="assets/js/jquery-ui.custom.min.js"></script>
    <script src="assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="assets/js/chosen.jquery.min.js"></script>
    <script src="assets/js/spinbox.min.js"></script>
    <script src="assets/js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script src="assets/js/moment.min.js"></script>
    <script src="assets/js/daterangepicker.min.js"></script>
    <script src="assets/js/bootstrap-datetimepicker.min.js"></script>
    <script src="assets/js/bootstrap-colorpicker.min.js"></script>
    <script src="assets/js/jquery.knob.min.js"></script>
    <script src="assets/js/autosize.min.js"></script>
    <script src="assets/js/jquery.inputlimiter.min.js"></script>
    <script src="assets/js/jquery.maskedinput.min.js"></script>
    <script src="assets/js/bootstrap-tag.min.js"></script>

    <!-- ace scripts -->
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>

    <!-- inline scripts related to this page -->
    <script type="text/javascript">
        jQuery(function ($) {

            $('#id-disable-check').on('click', function () {
                var inp = $('#form-input-readonly').get(0);
                if (inp.hasAttribute('disabled')) {
                    inp.setAttribute('readonly', 'true');
                    inp.removeAttribute('disabled');
                    inp.value = "This text field is readonly!";
                }
                else {
                    inp.setAttribute('disabled', 'disabled');
                    inp.removeAttribute('readonly');
                    inp.value = "This text field is disabled!";
                }
            });


            if (!ace.vars['touch']) {
                $('.chosen-select').chosen({ allow_single_deselect: true });
                //resize the chosen on window resize

                $(window)
                .off('resize.chosen')

                //resize chosen on sidebar collapse/expand
                $(document).on('settings.ace.chosen', function (e, event_name, event_val) {
                    if (event_name != 'sidebar_collapsed') return;
                    $('.chosen-select').each(function () {
                        var $this = $(this);
                        $this.next().css({ 'width': $this.parent().width() });
                    })
                });


                $('#chosen-multiple-style .btn').on('click', function (e) {
                    var target = $(this).find('input[type=radio]');
                    var which = parseInt(target.val());
                    if (which == 2) $('#form-field-select-4').addClass('tag-input-style');
                    else $('#form-field-select-4').removeClass('tag-input-style');
                });
            }


            $('[data-rel=tooltip]').tooltip({ container: 'body' });
            $('[data-rel=popover]').popover({ container: 'body' });

            autosize($('textarea[class*=autosize]'));

            $('textarea.limited').inputlimiter({
                remText: '%n character%s remaining...',
                limitText: 'max allowed : %n.'
            });

            $.mask.definitions['~'] = '[+-]';
            $('.input-mask-date').mask('99/99/9999');
            $('.input-mask-phone').mask('(999) 999-9999');
            $('.input-mask-eyescript').mask('~9.99 ~9.99 999');
            $(".input-mask-product").mask("a*-999-a999", { placeholder: " ", completed: function () { alert("You typed the following: " + this.val()); } });



            $("#input-size-slider").css('width', '200px').slider({
                value: 1,
                range: "min",
                min: 1,
                max: 8,
                step: 1,
                slide: function (event, ui) {
                    var sizing = ['', 'input-sm', 'input-lg', 'input-mini', 'input-small', 'input-medium', 'input-large', 'input-xlarge', 'input-xxlarge'];
                    var val = parseInt(ui.value);
                    $('#form-field-4').attr('class', sizing[val]).attr('placeholder', '.' + sizing[val]);
                }
            });

            $("#input-span-slider").slider({
                value: 1,
                range: "min",
                min: 1,
                max: 12,
                step: 1,
                slide: function (event, ui) {
                    var val = parseInt(ui.value);
                    $('#form-field-5').attr('class', 'col-xs-' + val).val('.col-xs-' + val);
                }
            });



            //"jQuery UI Slider"
            //range slider tooltip example
            $("#slider-range").css('height', '200px').slider({
                orientation: "vertical",
                range: true,
                min: 0,
                max: 100,
                values: [17, 67],
                slide: function (event, ui) {
                    var val = ui.values[$(ui.handle).index() - 1] + "";

                    if (!ui.handle.firstChild) {
                        $("<div class='tooltip right in' style='display:none;left:16px;top:-6px;'><div class='tooltip-arrow'></div><div class='tooltip-inner'></div></div>")
                        .prependTo(ui.handle);
                    }
                    $(ui.handle.firstChild).show().children().eq(1).text(val);
                }
            }).find('span.ui-slider-handle').on('blur', function () {
                $(this.firstChild).hide();
            });


            $("#slider-range-max").slider({
                range: "max",
                min: 1,
                max: 10,
                value: 2
            });

            $("#slider-eq > span").css({ width: '90%', 'float': 'left', margin: '15px' }).each(function () {
                // read initial values from markup and remove that
                var value = parseInt($(this).text(), 10);
                $(this).empty().slider({
                    value: value,
                    range: "min",
                    animate: true

                });
            });

            $("#slider-eq > span.ui-slider-purple").slider('disable');//disable third item


            $('#id-input-file-1 , #id-input-file-2').ace_file_input({
                no_file: 'No File ...',
                btn_choose: 'Choose',
                btn_change: 'Change',
                droppable: false,
                onchange: null,
                thumbnail: false //| true | large
                //whitelist:'gif|png|jpg|jpeg'
                //blacklist:'exe|php'
                //onchange:''
                //
            });
            //pre-show a file name, for example a previously selected file
            //$('#id-input-file-1').ace_file_input('show_file_list', ['myfile.txt'])


            $('#id-input-file-3').ace_file_input({
                style: 'well',
                btn_choose: 'Drop files here or click to choose',
                btn_change: null,
                no_icon: 'ace-icon fa fa-cloud-upload',
                droppable: true,
                thumbnail: 'small'//large | fit
                //,icon_remove:null//set null, to hide remove/reset button
                /**,before_change:function(files, dropped) {
                    //Check an example below
                    //or examples/file-upload.html
                    return true;
                }*/
                /**,before_remove : function() {
                    return true;
                }*/
                ,
                preview_error: function (filename, error_code) {
                    //name of the file that failed
                    //error_code values
                    //1 = 'FILE_LOAD_FAILED',
                    //2 = 'IMAGE_LOAD_FAILED',
                    //3 = 'THUMBNAIL_FAILED'
                    //alert(error_code);
                }

            }).on('change', function () {
                //console.log($(this).data('ace_input_files'));
                //console.log($(this).data('ace_input_method'));
            });


            //$('#id-input-file-3')
            //.ace_file_input('show_file_list', [
            //{type: 'image', name: 'name of image', path: 'http://path/to/image/for/preview'},
            //{type: 'file', name: 'hello.txt'}
            //]);




            //dynamically change allowed formats by changing allowExt && allowMime function
            $('#id-file-format').removeAttr('checked').on('change', function () {
                var whitelist_ext, whitelist_mime;
                var btn_choose
                var no_icon
                if (this.checked) {
                    btn_choose = "Drop images here or click to choose";
                    no_icon = "ace-icon fa fa-picture-o";

                    whitelist_ext = ["jpeg", "jpg", "png", "gif", "bmp"];
                    whitelist_mime = ["image/jpg", "image/jpeg", "image/png", "image/gif", "image/bmp"];
                }
                else {
                    btn_choose = "Drop files here or click to choose";
                    no_icon = "ace-icon fa fa-cloud-upload";

                    whitelist_ext = null;//all extensions are acceptable
                    whitelist_mime = null;//all mimes are acceptable
                }
                var file_input = $('#id-input-file-3');
                file_input
                .ace_file_input('update_settings',
                {
                    'btn_choose': btn_choose,
                    'no_icon': no_icon,
                    'allowExt': whitelist_ext,
                    'allowMime': whitelist_mime
                })
                file_input.ace_file_input('reset_input');

                file_input
                .off('file.error.ace')
                .on('file.error.ace', function (e, info) {
                    //console.log(info.file_count);//number of selected files
                    //console.log(info.invalid_count);//number of invalid files
                    //console.log(info.error_list);//a list of errors in the following format

                    //info.error_count['ext']
                    //info.error_count['mime']
                    //info.error_count['size']

                    //info.error_list['ext']  = [list of file names with invalid extension]
                    //info.error_list['mime'] = [list of file names with invalid mimetype]
                    //info.error_list['size'] = [list of file names with invalid size]


                    /**
                    if( !info.dropped ) {
                        //perhapse reset file field if files have been selected, and there are invalid files among them
                        //when files are dropped, only valid files will be added to our file array
                        e.preventDefault();//it will rest input
                    }
                    */


                    //if files have been selected (not dropped), you can choose to reset input
                    //because browser keeps all selected files anyway and this cannot be changed
                    //we can only reset file field to become empty again
                    //on any case you still should check files with your server side script
                    //because any arbitrary file can be uploaded by user and it's not safe to rely on browser-side measures
                });


                /**
                file_input
                .off('file.preview.ace')
                .on('file.preview.ace', function(e, info) {
                    console.log(info.file.width);
                    console.log(info.file.height);
                    e.preventDefault();//to prevent preview
                });
                */

            });

            $('#spinner1').ace_spinner({ value: 0, min: 0, max: 200, step: 10, btn_up_class: 'btn-info', btn_down_class: 'btn-info' })
            .closest('.ace-spinner')
            .on('changed.fu.spinbox', function () {
                //console.log($('#spinner1').val())
            });
            $('#spinner2').ace_spinner({ value: 0, min: 0, max: 10000, step: 100, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up bigger-110', icon_down: 'ace-icon fa fa-caret-down bigger-110' });
            $('#spinner3').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus bigger-110', icon_down: 'ace-icon fa fa-minus bigger-110', btn_up_class: 'btn-success', btn_down_class: 'btn-danger' });
            $('#spinner4').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus', icon_down: 'ace-icon fa fa-minus', btn_up_class: 'btn-purple', btn_down_class: 'btn-purple' });

            //$('#spinner1').ace_spinner('disable').ace_spinner('value', 11);
            //or
            //$('#spinner1').closest('.ace-spinner').spinner('disable').spinner('enable').spinner('value', 11);//disable, enable or change value
            //$('#spinner1').closest('.ace-spinner').spinner('value', 0);//reset to 0


            //datepicker plugin
            //link
            $('.date-picker').datepicker({
                autoclose: true,
                todayHighlight: true
            })
            //show datepicker when clicking on the icon
            .next().on(ace.click_event, function () {
                $(this).prev().focus();
            });

            //or change it into a date range picker
            $('.input-daterange').datepicker({ autoclose: true });


            //to translate the daterange picker, please copy the "examples/daterange-fr.js" contents here before initialization
            $('input[name=date-range-picker]').daterangepicker({
                'applyClass': 'btn-sm btn-success',
                'cancelClass': 'btn-sm btn-default',
                locale: {
                    applyLabel: 'Apply',
                    cancelLabel: 'Cancel',
                }
            })
            .prev().on(ace.click_event, function () {
                $(this).next().focus();
            });


            $('#<%=txtTimeRangeStart.ClientID %>').timepicker({
                minuteStep: 1,
                showSeconds: false,
                showMeridian: false,
                disableFocus: true,
                icons: {
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down'
                }
            }).on('focus', function () {
                $('#<%=txtTimeRangeStart.ClientID %>').timepicker('showWidget');
            }).next().on(ace.click_event, function () {
                $(this).prev().focus();
            });


            $('#<%=txtTimeRangeEnd.ClientID %>').timepicker({
                minuteStep: 1,
                showSeconds: false,
                showMeridian: false,
                disableFocus: true,
                icons: {
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down'
                }
            }).on('focus', function () {
                $('#<%=txtTimeRangeEnd.ClientID %>').timepicker('showWidget');
            }).next().on(ace.click_event, function () {
                $(this).prev().focus();
            });

            if (!ace.vars['old_ie']) $('#date-timepicker1').datetimepicker({
                //format: 'MM/DD/YYYY h:mm:ss A',//use this option to display seconds
                icons: {
                    time: 'fa fa-clock-o',
                    date: 'fa fa-calendar',
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down',
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-arrows ',
                    clear: 'fa fa-trash',
                    close: 'fa fa-times'
                }
            }).next().on(ace.click_event, function () {
                $(this).prev().focus();
            });


            $('#colorpicker1').colorpicker();
            //$('.colorpicker').last().css('z-index', 2000);//if colorpicker is inside a modal, its z-index should be higher than modal'safe

            $('#simple-colorpicker-1').ace_colorpicker();
            //$('#simple-colorpicker-1').ace_colorpicker('pick', 2);//select 2nd color
            //$('#simple-colorpicker-1').ace_colorpicker('pick', '#fbe983');//select #fbe983 color
            //var picker = $('#simple-colorpicker-1').data('ace_colorpicker')
            //picker.pick('red', true);//insert the color if it doesn't exist


            $(".knob").knob();


            var tag_input = $('#form-field-tags');
            try {
                tag_input.tag(
                  {
                      placeholder: tag_input.attr('placeholder'),
                      //enable typeahead by specifying the source array
                      source: ace.vars['US_STATES'],//defined in ace.js >> ace.enable_search_ahead
                      /**
                      //or fetch data from database, fetch those that match "query"
                      source: function(query, process) {
                        $.ajax({url: 'remote_source.php?q='+encodeURIComponent(query)})
                        .done(function(result_items){
                          process(result_items);
                        });
                      }
                      */
                  }
                )

                //programmatically add/remove a tag
                var $tag_obj = $('#form-field-tags').data('tag');
                $tag_obj.add('Programmatically Added');

                var index = $tag_obj.inValues('some tag');
                $tag_obj.remove(index);
            }
            catch (e) {
                //display a textarea for old IE, because it doesn't support this plugin or another one I tried!
                tag_input.after('<textarea id="' + tag_input.attr('id') + '" name="' + tag_input.attr('name') + '" rows="3">' + tag_input.val() + '</textarea>').remove();
                //autosize($('#form-field-tags'));
            }


            /////////
            $('#modal-form input[type=file]').ace_file_input({
                style: 'well',
                btn_choose: 'Drop files here or click to choose',
                btn_change: null,
                no_icon: 'ace-icon fa fa-cloud-upload',
                droppable: true,
                thumbnail: 'large'
            })

            //chosen plugin inside a modal will have a zero width because the select element is originally hidden
            //and its width cannot be determined.
            //so we set the width after modal is show
            $('#modal-form').on('shown.bs.modal', function () {
                if (!ace.vars['touch']) {
                    $(this).find('.chosen-container').each(function () {
                        $(this).find('a:first-child').css('width', '210px');
                        $(this).find('.chosen-drop').css('width', '210px');
                        $(this).find('.chosen-search input').css('width', '200px');
                    });
                }
            })
            /**
            //or you can activate the chosen plugin after modal is shown
            //this way select element becomes visible with dimensions and chosen works as expected
            $('#modal-form').on('shown', function () {
                $(this).find('.modal-chosen').chosen();
            })
            */



            $(document).one('ajaxloadstart.page', function (e) {
                autosize.destroy('textarea[class*=autosize]')

                $('.limiterBox,.autosizejs').remove();
                $('.daterangepicker.dropdown-menu,.colorpicker.dropdown-menu,.bootstrap-datetimepicker-widget.dropdown-menu').remove();
            });

        });
    </script>

    <script type="text/javascript">

        function openModalErreur() {
           
            $('#modal_erreur').modal('show');
        }
        function disableThis() {
            var x = document.getElementById("ctl00_ContentPlaceHolder1_lblErreur");
            if ((x != null) && (x.value == '')) {
                return true;
            }
            else {
                return false;
            }

        }

        function multiselect() {
            $('.chosen-select').chosen({ allow_single_deselect: true });
            //resize the chosen on window resize

            $(window)
            .off('resize.chosen')
           
        }

    </script>

    <%--    
    <script type="text/javascript" src="assets/js/jquery-1.8.3.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modale");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 3 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
   
    $('form').live("submit", function () {
        ShowProgress();
    });

    
    
</script>
    <div class="loading" align="center" id="panelLoading" runat="server">
       <h4 class="widget-title red">
    <asp:Literal ID="Literal12" Text="<%$ Resources:LocalizedText, lblLoading%>" runat="server"></asp:Literal>
            </h4>
    <img src="bx_loader.gif" alt="" />
</div>--%>
</asp:Content>
