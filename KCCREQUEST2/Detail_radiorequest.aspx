<%@ Page Title="" Language="C#" MasterPageFile="~/ViewRequest.Master" AutoEventWireup="true" CodeBehind="Detail_radiorequest.aspx.cs" Inherits="KCCREQUEST2.Detail_radiorequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
    </style>

    <script type="text/javascript">
         function DisableButton() {

             if (document.getElementById("<%=btnSave.ClientID %>")) {

                 document.getElementById("<%=btnSave.ClientID %>").disabled = true;
             }
         
            
              
                   document.getElementById("<%=btnOk.ClientID %>").disabled = true;
          }
          window.onbeforeunload = DisableButton;
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
     <%--           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1><asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, lblRadioRequest%>"></asp:Label>
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    <%--  New Request--%>
                                </small>
                    </h1>
                </div>
                <!--FIN TITRE DE LA PAGE-->



                <!--bloc pour les information du requisitionneur-->
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center">
                        <h4 class="widget-title lighter">

                            <asp:Label ID="detailRadioRequest" runat="server" Text="<%$ Resources:LocalizedText, detailRadioRequest%>"></asp:Label>
                                <span class="badge badge" style="background-color:#003468" id="title" runat="server"></span>
                        </h4>
                    </div>

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">

                                    <!-- entete de la requisition-->
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2">
                                                <h5 class="lighter block" style="color:rgb(187,135,72);font-weight:bold">
                                                    <asp:Label ID="lblMsgHeadIT"  runat="server" Text="<%$ Resources:LocalizedText, lblMsgHeadIT %>"></asp:Label>

                                                </h5>
                                            </td>
                                        </tr>
                                        <tr>

                                             <td style="width: 50%;" >
                                                <div>
                                                    <label for="form-field-select-1">
                                                        
                                                       <asp:Literal ID="lblDepartment" Text="<%$ Resources:LocalizedText, lblDepartment%>" runat="server"></asp:Literal>
                                                        
                                                         <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstDepartment" class="chosen-select form-control" OnSelectedIndexChanged="LstDepartment_SelectedIndexChanged" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblRequestor" Text="<%$ Resources:LocalizedText, lblRequestor%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestor" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblBeneficiary" Text="<%$ Resources:LocalizedText, lblBeneficiary%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtBeneficiary" MaxLength="40" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblBeneficiaryJobTitle" Text="<%$ Resources:LocalizedText, lblBeneficiaryJobTitle%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtBeneficiaryJobTitle" MaxLength="40" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblRequestDescription" Text="<%$ Resources:LocalizedText, lblRequestDescription%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtRequestDescription"  onkeypress="if (this.value.length > 250) { return false; }" TextMode="MultiLine"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblJustificationWhy" Text="<%$ Resources:LocalizedText, lblJustificationWhy%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtJustificationWhy"  onkeypress="if (this.value.length > 250) { return false; }" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblCategory" Text="<%$ Resources:LocalizedText, lblCategory%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstCategory" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblPriority" Text="<%$ Resources:LocalizedText, lblPriority%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstPriority" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblPlannedStartDate" Text="<%$ Resources:LocalizedText, lblPlannedStartDate%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtPlannedStartDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal9" Text="<%$ Resources:LocalizedText, lbdgvCurrentUser%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtCurrentUser" ReadOnly="true" class="form-control" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal10" Text="<%$ Resources:LocalizedText, lblStatut%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtStatut" ReadOnly="true" class="form-control" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <br />
                                                <div>
                                                    <label for="form-field-select-1">
                                                        
                                                        <asp:Literal ID="lblQuotationFile" Text="<%$ Resources:LocalizedText, lblQuotationFile%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" Width="90%" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%;">
                                                <br />
                                                <div class="col-sm-12">
                                                    <div class="tabbable tabs-below">
                                                        <div class="tab-content">
                                                            <div id="home2" class="tab-pane in active">
                                                                  <asp:GridView ID="dgvFiles" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="FileID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                            <HeaderTemplate>
                                                                                File ID
                                                
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtFileID" runat="server" Text='<%#Eval("FileID")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="80%" ItemStyle-Width="80%">
                                                                            <HeaderTemplate>
                                                                                <asp:Literal ID="lbdgvFile" Text="<%$ Resources:LocalizedText, lbdgvFile%>" runat="server"></asp:Literal>

                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtFileName" runat="server" Text='<%#Eval("FileName")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                            <HeaderTemplate>
                                                                                Type
                                                
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                            <HeaderTemplate>
                                                                                File
                                                
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="File" runat="server" Text='<%#Eval("File")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                                                            <HeaderTemplate></HeaderTemplate>
                                                                            <ItemTemplate>

                                                                                <asp:Button CssClass="btn  btn-xs btn-danger" OnClick="btnview_Click" ID="btnview" runat="server" Text="<%$ Resources:LocalizedText, lblView%>" />

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

                                                        <ul class="nav nav-tabs" id="myTab2">
                                                            <li class="active">
                                                                <%--	<a data-toggle="tab" href="#home2">File</a>--%>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                    <!--fin entete requesition-->
                                    <hr />

                                    <!--bloc detail de la requisition-->

                                    <div class="form-horizontal" id="Form2" style="width: 100%;">
                                        <!--bloc des onglets des detail-->
                                        <div class="tabbable">
                                            <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab4">
                                                <li class="active">
                                                    <a data-toggle="tab" href="#home4">
                                                        <asp:Literal ID="ZoneDetail" Text="<%$ Resources:LocalizedText, ZoneDetail%>" runat="server"></asp:Literal>

                                                    </a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#profile4">

                                                        <asp:Literal ID="zoneHistory" Text="<%$ Resources:LocalizedText, zoneHistory%>" runat="server"></asp:Literal>


                                                    </a>
                                                </li>
                                            </ul>


                                            <div class="tab-content">
                                                <!--onglet radio-->
                                                <div id="home4" class="tab-pane in active">

                                                      <asp:GridView ID="dgvRadio" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                <HeaderTemplate>
                                                                    ID
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemID" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="25%" ItemStyle-Width="25%">
                                                                <HeaderTemplate>
                                                                    Items
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="30%" ItemStyle-Wrap="true">
                                                                <HeaderTemplate>
                                                                   <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblItemComment" runat="server" Text='<%#Eval("CommentItem")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="12%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                                    Sysaid ID
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                <%--    <asp:Label ID="lblSysAidID" runat="server" Text='<%#Eval("SysAidID")%>'></asp:Label>--%>
                                                                      <asp:LinkButton ID="txtSysAid" MaxLength="2" runat="server"  OnClick="txtSysAid_Click" Text='<%#Eval("SysAidID")%>' >0</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                <HeaderTemplate>
                                                                    Sysaid User
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSysAidProcessUser" runat="server" Text='<%#Eval("SysAidProcessUser")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                                    Sysaid Status
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSysAidCurrentStatut" runat="server" Text='<%#Eval("SysAidCurrentStatus")%>'></asp:Label>
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
                                                <!--fin onglet radio-->

                                                <!--onglet history-->
                                                <div id="profile4" class="tab-pane">

                                                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnApproval" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnReject" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnReturnToOrginator" />
                                                        </Triggers>
                                                        <ContentTemplate>--%>
                                                             <asp:GridView ID="dgvHistory" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="HistroryID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                   

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                        <HeaderTemplate>
                                                                            Action
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtAction" runat="server" Text='<%#Eval("Action")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                        <HeaderTemplate>

                                                                            <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtComment" runat="server" Text='<%#Eval("Comment")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <HeaderTemplate>

                                                                            <asp:Literal ID="lbdgvRole" Text="<%$ Resources:LocalizedText, lbdgvRole%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtRole" runat="server" Text='<%#Eval("Role")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <HeaderTemplate>
                                                                            <asp:Literal ID="lbdgvCreateDate" Text="<%$ Resources:LocalizedText, lbdgvCreateDate%>" runat="server"></asp:Literal>

                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtCreateDate" runat="server" Text='<%# DateTime.Parse(Eval("CreationDate").ToString()).ToString("MM-dd-yyyy HH:mm:ss") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                        <HeaderTemplate>
                                                                            <asp:Literal ID="lbdgvCreateBy" Text="<%$ Resources:LocalizedText, lbdgvCreateBy%>" runat="server"></asp:Literal>

                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtCreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <SelectedRowStyle CssClass="alert-info" />
                                                                <RowStyle CssClass="cursor-pointer" />
                                                                <HeaderStyle CssClass="alert-success" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>
                                                       <%-- </ContentTemplate>
                                                    </asp:UpdatePanel>--%>
                                                </div>
                                                <!--FIN ONGLET history-->
                                            </div>
                                            <!--fin onglet application-->
                                        </div>
                                        <!--fin bloc onglet detail-->
                                        <br />
                                    </div>

                                    <!-- button pour l'enregistrement et l'initialisation des données-->
                                    <div class="clearfix form-actions" id="blockbtn" runat="server">
                                        <div>
                                        
                                                    <asp:Button ID="btnUpdateRadio"    OnClick="btnUpdateRadio_Click"  class="btn btn-info btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Update_Text%>" />
                                                
                                                    <asp:Button ID="btnApproval"  OnClick="btnApproval_Click" class="btn btn-primary btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btnApproval%>" />
                                                  
											 <asp:Button ID="BtnReject" OnClick="BtnReject_Click"  class="btn btn-danger btn-sm" runat="server" Text="<%$ Resources:LocalizedText, BtnReject%>"/>
                                                 
											 <asp:Button ID="BtnReturnToOrginator" OnClick="BtnReturnToOrginator_Click"  class="btn btn-success btn-sm" runat="server" Text="<%$ Resources:LocalizedText, BtnReturnToOrginator%>"  />
                                             
                                                  <asp:Button ID="btnBackToPreview" OnClick="btnBackToPreview_Click"  class="btn btn-purple btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btnBackToPrevious%>" />

                                               
											 <asp:Button ID="btnrRealocate" class="btn btn-warning btn-sm" OnClick="btnrRealocate_Click" runat="server" Text="<%$ Resources:LocalizedText, btnrRealocate%>"	 />
                                          
                                   <asp:Button ID="btnAdHoc" class="btn btn-pink btn-sm" OnClick="btnAdHoc_Click"  runat="server" Text="Ad Hoc" />
                                             <asp:Button ID="btnCallBack"  class="btn btn-danger btn-sm" OnClick="btnCallBack_Click"  runat="server" Text="<%$ Resources:LocalizedText, btnCallBack%>"/>

                                               <asp:Button ID="btnPrint" class="btn btn-inverse btn-sm" OnClick="btnPrint_Click"  runat="server" Text="<%$ Resources:LocalizedText, btnPrint%>" />
                                        </div>

                                    </div>
                                    <br />
                                </div>

                            </div>
                            <!-- /.widget-main -->
                        </div>
                        <!-- /.widget-body -->
                    </div>
                </div>

            </div>
        </div>
    </div>

    <!--popup pour comment-->


    <div id="my-modal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin"><asp:Label ID="Label6" runat="server" Text="<%$ Resources:LocalizedText, lblRadioRequest%>"></asp:Label> - <span> <asp:Label ID="lblTitre" runat="server" Text="0" Visible="true"></asp:Label></span></h3>
                    <asp:Label ID="lblaction" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">


                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <br/>

                         <div>
                                                    <label for="form-field-select-1">
                                                        
                                                        <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblQuotationFile%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="90%" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>

                    </h4>

                </div>

                <div class="modal-footer">


                    <asp:Button ID="btnSave" OnClick="btnSave_Click"   class="btn btn-sm btn-info" runat="server" Text="OK" />


                    <button class="btn btn-sm btn-danger pull-right" id="btnClose" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        <asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!--fin popup-->



    <!--popup pour le message d'erreur-->


    <div id="modal_erreur" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#682525;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">
                        <asp:Label ID="txtInvalidInput" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, txtInvalidInput %>"></asp:Label>
                    </h3>
                </div>

                <div class="modal-body">

                   <%-- <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>

                            <asp:AsyncPostBackTrigger ControlID="btnApproval" />
                            <asp:AsyncPostBackTrigger ControlID="BtnReject" />

                            <asp:AsyncPostBackTrigger ControlID="BtnReturnToOrginator" />
                        </Triggers>
                        <ContentTemplate>--%>
                            <h4 class="smaller lighter red no-margin">
                                <asp:Label ID="lblErreur" runat="server" Text=""></asp:Label>

                            </h4>





<%--                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>

                <div class="modal-footer">
                    <button class="btn btn-sm btn-danger pull-right" id="modalclose" data-dismiss="modal">
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


     <!--popup pour reallocate-->


    <div id="my-reallocate" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin"><asp:Label ID="Label5" runat="server" Text="<%$ Resources:LocalizedText, lblRadioRequest%>"></asp:Label> - <asp:Label ID="Label7" runat="server" Text="<%$ Resources:LocalizedText, btnrRealocate%>"></asp:Label></h3>
                    <asp:Label ID="Label4" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">
                  

                    <h4 class="smaller lighter red no-margin">

                         <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                        
                        <div>
                            <label for="form-field-select-1">
                                 <asp:Literal ID="Literal21" Text="<%$ Resources:LocalizedText, lblUsers%>" runat="server"></asp:Literal>
                                
                                <b style="color: red">*</b></label><br />
                            <asp:DropDownList ID="dllUsers" class="chosen-select  form-control" OnSelectedIndexChanged="dllUsers_SelectedIndexChanged1"  Width="100%" runat="server" AutoPostBack="true" ></asp:DropDownList>
                        </div>


                         <div>
                            <label for="form-field-select-1">
                                 <asp:Literal ID="Literal22" Text="<%$ Resources:LocalizedText, lblRole%>" runat="server"></asp:Literal>
                                
                                <b style="color: red">*</b></label><br />
                            <asp:DropDownList ID="dllRole" class="chosen-select  form-control" Width="100%" runat="server" AutoPostBack="false" ></asp:DropDownList>
                        </div>

                        <div>
                            <label for="form-field-select-1">
                                 <asp:Literal ID="Literal23" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                
                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtCommentReallocate"  runat="server" Width="100%"></asp:TextBox>
                        </div>

                                   </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
 
                </div>

                <div class="modal-footer">
                   

                    <asp:Button ID="BtnSaveReallocate"  OnClick="BtnSaveReallocate_Click"  class="btn btn-sm btn-info" runat="server" Text="OK" />
                            

                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                <asp:Literal ID="Literal4" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>



    <!--popup pour adhoc-->


    <div id="my-adhoc" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:LocalizedText, lblRadioRequest%>"></asp:Label> - Ad-Hoc</h3>
                    <asp:Label ID="Label2" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">
                  

                    <h4 class="smaller lighter red no-margin">

                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                        
                        <div>
                            <label for="form-field-select-1">
                                 <asp:Literal ID="Literal5" Text="<%$ Resources:LocalizedText, lblUsers%>" runat="server"></asp:Literal>
                                
                                <b style="color: red">*</b></label><br />
                            <asp:DropDownList ID="dllUserAdHoc" class="chosen-select  form-control"   Width="100%" runat="server" ></asp:DropDownList>
                        </div>


                      

                        <div>
                            <label for="form-field-select-1">
                                 <asp:Literal ID="Literal7" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                
                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtCommentAdHoc" TextMode="MultiLine"  runat="server" Width="100%"></asp:TextBox>
                        </div>

                                   </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
 
                </div>

                <div class="modal-footer">
                   

                    <asp:Button ID="btnAdHocSav"   OnClick="btnAdHocSav_Click"  class="btn btn-sm btn-info" runat="server" Text="OK" />
                            

                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
              <asp:Literal ID="Literal6" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!--fin popup-->


    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
        <div class="modal-content">
            <div class="modal-header" style="background-color:#006666;">
                <h3 class="smaller lighter white no-margin">CONFIRMATION
                </h3>
            </div>
            <div class="modal-body">
                <h3 class="smaller lighter green no-margin">
                    <asp:Label ID="lblActionSuccess" runat="server" Text="<%$ Resources:LocalizedText, lblActionSuccess%>"></asp:Label>
                   
                </h3>

            </div>
            <div class="modal-footer">

                <asp:Button ID="btnOk" class="btn btn-success"  OnClick="btnOk_Click" runat="server" Text="OK" />

            </div>
        </div>
    </asp:Panel>

    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <script src="assets/js/jquery.MultiFile.js"></script>

    <script type="text/javascript">
        if ('ontouchstart' in document.documentElement) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
    </script>
    <script src="assets/js/bootstrap.min.js"></script>

    <script src="assets/js/jquery-ui.custom.min.js"></script>
    <script src="assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="assets/js/chosen.jquery.min.js"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            if (!ace.vars['touch']) {
                $('.chosen-select').chosen({ allow_single_deselect: true });



            }
        });
    </script>

    <!-- page specific plugin scripts -->

    <!--[if lte IE 8]>
		  <script src="assets/js/excanvas.min.js"></script>
		<![endif]-->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
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


            $('#timepicker1').timepicker({
                minuteStep: 1,
                showSeconds: true,
                showMeridian: false,
                disableFocus: true,
                icons: {
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down'
                }
            }).on('focus', function () {
                $('#timepicker1').timepicker('showWidget');
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
        });
    </script>


    <script type="text/javascript">
        function openModal() {
            $('#my-modal').modal('show');
        }
        function hideModal() {
            $('#my-modal').modal('hide');
        }
        function openModalErreur() {

            $('#modal_erreur').modal('show');
            
        }
        function openReallocate() {
            $('#my-reallocate').modal('show');
        }
        function hideReallocate() {
            $('#my-reallocate').modal('hide');
        }
        function openAdHoc() {
            $('#my-adhoc').modal('show');
        }
        function hideAdHoc() {
            $('#my-adhoc').modal('hide');
        }

        function GoSysaidLink(url) {
            window.open(url, "_blank");

        }
    </script>

     <script type="text/javascript" src="assets/js/jquery-1.8.3.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modale");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 1.8 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
   
    $('form').live("submit", function () {
        var $btn = $(document.activeElement);
        if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_dgvFiles_ctl02_btnview") {
            return true;
        }
        else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_btnOk") {
            return true;
        }
        else {
            ShowProgress();
        }

    });

    
    
</script>
    <div class="loading" align="center" id="panelLoading" runat="server">
       <h4 class="widget-title red">
    <asp:Literal ID="Literal18" Text="<%$ Resources:LocalizedText, lblLoading%>" runat="server"></asp:Literal>
            </h4>
    <img src="bx_loader.gif" alt="" />
</div>
</asp:Content>
