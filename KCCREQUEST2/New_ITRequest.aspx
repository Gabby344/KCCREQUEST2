<%@ Page Title="" Language="C#" MasterPageFile="~/NewRequest.Master" AutoEventWireup="true" CodeBehind="New_ITRequest.aspx.cs" Inherits="KCCREQUEST2.New_ITRequest" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
    .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
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
        background-color:#003468;
        }
         .btn2-btnReset {
color: #fff;
border-color: #357ebd;
        background-color:rgb(0,177,147);
        }
    </style>

    <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;
            document.getElementById("<%=btnReset.ClientID %>").disabled = true;

                document.getElementById("<%=btnOk.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
           <%--     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1><asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, lblITRequest%>"></asp:Label>
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
                            <asp:Label ID="lblNewItRequest" runat="server" Text="<%$ Resources:LocalizedText, lblNewItRequest%>"></asp:Label>
                        </h4>
                    </div>

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">

                                    <!-- entite de la requisition-->
                                    <table style="width: 100%; margin: auto">
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <h5 class="lighter  smaller" style="color:rgb(187,135,72);font-weight:bold">
                                              
                                                    <%--The items on this request are to be allocated to :--%>
                                                    <asp:Label ID="lblMsgHeadIT" runat="server" Text="<%$ Resources:LocalizedText, lblMsgHeadIT %>"></asp:Label>
                                                </h5>
                                            </td>
                                            <%--<td>
                                                    </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblDepartment" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblDepartment %>"></asp:Label><b style="color: red"> *</b>
                                                    <br />
                                                    <asp:DropDownList ID="dllDepartment" OnSelectedIndexChanged="dllDepartment_SelectedIndexChanged" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblRequestor" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblRequestor %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestor" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblBeneficiary" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblBeneficiary %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtBeneficiary" MaxLength="50" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblBeneficiaryJobTitle" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblBeneficiaryJobTitle %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtBeneficiaryJobTitle" MaxLength="50"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="lblRequestDescription" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblRequestDescription %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtRequestDescription" TextMode="MultiLine"  runat="server" Width="100%" onkeypress="if (this.value.length > 250) { return false; }"  ></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td colspan="2">
                                                <div >
                                                    <asp:Label ID="lblJustificationWhy" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblJustificationWhy %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtJustificationWhy" TextMode="MultiLine" onkeypress="if (this.value.length > 250) { return false; }" Text="okay"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblPriority" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblUrgency %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:DropDownList ID="LstPriority" Cssclass="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstPriority_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </td>

                                            
                                                <td style="width: 50%">
                                                    <asp:Panel ID="panelUrgencyComment" Visible="false" runat="server">
                                                        <asp:Label ID="lblUrgencyComment" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblUrgencyComment %>"></asp:Label><b style="color: red" id="mendatory" runat="server"> *</b>
                                                        <br>
                                                        <asp:TextBox ID="txtUrgencyComment" onkeypress="if (this.value.length > 80) { return false; }"   Cssclass="form-control" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                     </asp:Panel>
                                                </td>
                                        </tr>


                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblFile" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblFile %>"></asp:Label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload2"  accept=".png,.jpg,.jpeg,.xlsx,.docx,.pdf,.ppt,.msg" Width="90%" runat="server" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                                <asp:Label ID="lblFileMendatory" Visible="false" runat="server" Text="0"></asp:Label>
                                                <asp:Label ID="CheckTelephone" Visible="false" runat="server" Text="0"></asp:Label>
                                                <asp:Label ID="CheckUsb" Visible="false" runat="server" Text="0"></asp:Label>
                                            </td>

                                            <%--<td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="lblPlannedStartDate" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblPlannedStartDate %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtPlannedStartDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>--%>
                                        </tr>

                                        <tr>
                                            <%--<td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">File</label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                            </td>--%>
                                        </tr>
                                    </table>

                                    <!--fin entete requesition-->
                                    <%--   <hr />--%>

                                    <!--bloc detail de la requisition-->

                                    <div class="form-horizontal" id="Form2" style="width: 100%;">

                                        <h5 class="row smaller lighter" style="color:rgb(187,135,72);font-weight:bold">
                                            <span class="col-sm-8">
                                               

                                                <asp:Label ID="lblMsgDetailIT" runat="server" Text="<%$ Resources:LocalizedText, lblMsgDetailIT%>"></asp:Label>
                                            </span>
                                        </h5>
                                        <!--bloc des onglets des detail-->
                                        <div class="tabbable">
                                            <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab4">
                                                <li class="active">
                                                    <a data-toggle="tab" href="#home4">
                                                        <asp:Literal ID="computer" Text="<%$ Resources:LocalizedText, computer%>" runat="server"></asp:Literal></a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#profile4">
                                                        <asp:Literal ID="telephone" Text="<%$ Resources:LocalizedText, telephone%>" runat="server"></asp:Literal></a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#dropdown14">
                                                        <asp:Literal ID="connectivity" Text="<%$ Resources:LocalizedText, connectivity%>" runat="server"></asp:Literal></a>
                                                </li>
                                                <li>
                                                    <a data-toggle="tab" href="#sofware">
                                                        <asp:Literal ID="software" Text="<%$ Resources:LocalizedText, software%>" runat="server"></asp:Literal></a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#network">
                                                        <asp:Literal ID="networks" Text="<%$ Resources:LocalizedText, networks%>" runat="server"></asp:Literal></a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#Application">
                                                        <asp:Literal ID="application" Text="<%$ Resources:LocalizedText, application%>" runat="server"></asp:Literal></a>
                                                </li>

                                              <%--  <li>
                                                    <a data-toggle="tab" href="#FileShare">
                                                        <asp:Literal ID="fileshare" Text="<%$ Resources:LocalizedText, fileshare%>" runat="server"></asp:Literal></a>
                                                </li>--%>

                                            </ul>


                                            <div class="tab-content">
                                                <!--onglet computer-->
                                                <div id="home4" class="tab-pane in active">

                                                    <asp:UpdatePanel ID="UpdatePanel100" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvComputer" OnRowDataBound="dgvComputer_RowDataBound" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIDComputer" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemComputer" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                                                <ContentTemplate>--%>
                                                                            <asp:CheckBox ID="chkRowComputer" Checked="false" OnCheckedChanged="chkRow_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                            <%--        </ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%-- Quantity--%>
                                                                            <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--<asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />
                                                                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                                                                </Triggers>
                                                                                <ContentTemplate>--%>
                                                                            <asp:TextBox ID="txtQteComputer" MaxLength="2" runat="server" Enabled="false" Text="0" Width="50px" TextMode="Number"></asp:TextBox>

                                                                            <%--</ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%--Justification (why)--%>
                                                                            <asp:Literal ID="lbdgvJustification" Text="<%$ Resources:LocalizedText, lbdgvJustification%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--<asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />
                                                                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                                                                </Triggers>
                                                                                <ContentTemplate>--%>
                                                                            <asp:TextBox ID="txtCommentComputer" MaxLength="52" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                                                            <%-- </ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <SelectedRowStyle CssClass="alert-info" />
                                                                <RowStyle CssClass="cursor-pointer" />
                                                                <HeaderStyle CssClass="alert-success" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--fin onglet computer-->

                                                <!--onglet telephone-->
                                                <div id="profile4" class="tab-pane">

                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvTelephone" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtIDTelephone" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtItemTelephone" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:CheckBox ID="chkRowTelephone" OnCheckedChanged="chkRowTelephone_CheckedChanged" Checked="false" AutoPostBack="true" runat="server" />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%--   Quantity--%>
                                                                            <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtQteTelephone" MaxLength="2" runat="server" Enabled="false" Text="0" Width="50px" TextMode="Number"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%-- Justification (why)--%>
                                                                            <asp:Literal ID="lbdgvJustification" Text="<%$ Resources:LocalizedText, lbdgvJustification%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtCommentTelephone" MaxLength="52" Width="100%" Enabled="false" runat="server"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <SelectedRowStyle CssClass="alert-info" />
                                                                <RowStyle CssClass="cursor-pointer" />
                                                                <HeaderStyle CssClass="alert-success" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--FIN ONGLET TELEPHONE-->


                                                <!--ONGLET CONNECTIVITY-->
                                                <div id="dropdown14" class="tab-pane">

                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%--   <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvConnectivity" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtIDConnectivity" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtItemConnectivity" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:CheckBox ID="chkRowConnectivity" Checked="false" OnCheckedChanged="chkRowConnectivity_CheckedChanged" AutoPostBack="true" runat="server" />


                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" Visible="false" HeaderStyle-BorderColor="#003468" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%--   Quantity--%>
                                                                            <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtQteConnectivity" MaxLength="2" runat="server" Enabled="false" Text="0" Width="50px" TextMode="Number"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%--  Justification (why)--%>
                                                                            <asp:Literal ID="lbdgvJustification" Text="<%$ Resources:LocalizedText, lbdgvJustification%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                               <div><asp:Label ID="lbdgvComment" runat="server" Text="" Visible="false"  ForeColor ="red"></asp:Label></div>
                                                                               <asp:TextBox ID="txtCommentConnectivity" MaxLength="52" Width="100%" Enabled="false" runat="server"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <SelectedRowStyle CssClass="alert-info" />
                                                                <RowStyle CssClass="cursor-pointer" />
                                                                <HeaderStyle CssClass="alert-success" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <!--DEBUT FORMULAIRE POUR NEW USER ACCOUNT-->
                                                    <asp:UpdatePanel ID="UpdatePanel36" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="dgvConnectivity" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <div class="widget-body" id="newUserAccount" runat="server">
                                                                <div class="step-content pos-rel">
                                                                    <div class="step-pane active" data-step="1">
                                                                        <div class="widget-main">
                                                                            <div class="form-horizontal" id="Div1">

                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">
                                                                                        <span style="color: red">
                                                                                            <asp:Literal ID="divUserAccountTitle" runat="server"></asp:Literal>
                                                                                        </span>
                                                                                    </label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block"></span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>



                                                                                <div class="form-group">

                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">
                                                                                        
                                                                                         <asp:Literal ID="divUserAccountNewStart" Text="<%$ Resources:LocalizedText, divUserAccountNewStart%>" runat="server"></asp:Literal>
                                                                                     <b style="color: red">*</b></label>
                                                                                    </label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">

                                                                                            <asp:DropDownList ID="EmployeeType" runat="server" class="width-100 form-control" OnSelectedIndexChanged="EmployeeType_SelectedIndexChanged" AutoPostBack="true" >
                                                                                                
                                                                                                  <asp:ListItem Text="Choose One"></asp:ListItem>
                                                                                                  <asp:ListItem Text="KCC/ICON"></asp:ListItem>
                                                                                                  <asp:ListItem Text="Visitor"></asp:ListItem>
                                                                                                  <asp:ListItem Text="Contractor"></asp:ListItem>


                                                                                            </asp:DropDownList>
                                                                                           
                                                                                            <asp:RadioButton ID="rdVisitor" OnCheckedChanged="rdVisitor_CheckedChanged" AutoPostBack="true" runat="server" GroupName="compteType" Visible ="false" />

                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                    <br />
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">
                                                                                        
                                                                                       <asp:Literal ID="divUserAccountVisitor" Text="<%$ Resources:LocalizedText, divUserAccountVisitor%>" runat="server" Visible="false"></asp:Literal>
                                                                                    </label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:RadioButton ID="rdNewStart" OnCheckedChanged="rdNewStart_CheckedChanged" AutoPostBack="true" runat="server" GroupName="compteType"  Visible="false"/>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>

                                                                                </div>



                                                                                   <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                        <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, divUserCompagnie%>" runat="server"></asp:Literal>

                                                                                        <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtCompany" MaxLength="40" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>


                                                                                
                                                                              <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                        <asp:Literal ID="Literal13" Text="<%$ Resources:LocalizedText, divUserAccountNameManager%>" runat="server"></asp:Literal>
                                                                                        <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtNameManager" MaxLength="40" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>


                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>


                                                                  <div id="forKCC" runat="server">
                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                        <asp:Literal ID="divUserAccountDepartment" Text="<%$ Resources:LocalizedText, divUserAccountDepartment%>" runat="server"></asp:Literal>
                                                                                        <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:DropDownList ID="lstDepartment" class="width-100 form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                                                        </span>
                                                                                    </div>


                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>




                                                                                      <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                        <asp:Literal ID="divUserAccountEmployee" Text="<%$ Resources:LocalizedText, divUserAccountEmployee%>" runat="server"></asp:Literal>

                                                                                        <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtEmployeeNumber" MaxLength="40" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>


                                                                                   <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                        <asp:Literal ID="divUserAccountJobTitle" Text="<%$ Resources:LocalizedText, divUserAccountJobTitle%>" runat="server"></asp:Literal>

                                                                                        <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtJobTitle" MaxLength="40" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>



                                                                              
                                                                               


                                                                             

                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                        <asp:Literal ID="divUserAccountLocation" Text="<%$ Resources:LocalizedText, divUserAccountLocation%>" runat="server"></asp:Literal>
                                                                                        <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtLocation" MaxLength="40" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>


                                                                                 <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                        <asp:Literal ID="Literal4" Text="<%$ Resources:LocalizedText, lblStartDate%>" runat="server"></asp:Literal>
                                                                                        <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <%--<asp:TextBox ID="txtStartDate" MaxLength="40" class="form-control date-picker"  data-date-format="mm-dd-yyyy" runat="server" TextMode="SingleLine"></asp:TextBox>--%>
                                                                                       
                                                                                            <input type="date" id="inputStartDate" name="trip-start" runat="server" MaxLength="40" class="width-100" />
                                                                                             </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>
                                                                              
                                                                        </div>
                                                                              









                                                                                <div id="forNonKcc" runat="server">  
                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                        <asp:Literal ID="Literal5" Text="<%$ Resources:LocalizedText, divUsertxtEmailAdress%>" runat="server"></asp:Literal>
                                                                                        <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtEmailAdress" MaxLength="40" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>






                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                        <asp:Literal ID="Literal6" Text="<%$ Resources:LocalizedText, divUserttxtDurationAccount%>" runat="server"></asp:Literal>
                                                                                      <%--  <asp:Literal ID="Literal14" Text="Date" runat="server"></asp:Literal>--%>
                                                                                        <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtDurationAccount" type="date" MaxLength="40" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>
                                                                            </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <!--fin formulaire NEW USER ACCOUNT-->

                                                </div>
                                                <!--FIN ONGLET CONNECTIVITY-->

                                                <!--ONGLET SOFTWARE-->
                                                <div id="sofware" class="tab-pane">
                                                       <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                    
                                               <div class="form-group">
                                                    <label for="inputError2" class="col-xs-6 col-sm-2 control-label no-padding-left">
                                                
                                                        <asp:Literal ID="Literal7" Text="<%$ Resources:LocalizedText, lblTypeSoftware%>" runat="server"></asp:Literal>
                                               </label>
                                                     <div class="col-xs-12 col-sm-6">
                                                    <span class="input-icon block">

                                                    <asp:DropDownList ID="txtSoftware" OnSelectedIndexChanged="txtSoftware_SelectedIndexChanged"  class="form-control" Width="80%" runat="server" AutoPostBack="true">

                                                         <asp:ListItem Text="<%$ Resources:LocalizedText, selectTypeSoftware%>"></asp:ListItem>
                                                         <asp:ListItem Text="<%$ Resources:LocalizedText, selectStandardSoftware%>"></asp:ListItem>
                                                         <asp:ListItem Text="<%$ Resources:LocalizedText, selectNonStandardSoftware%>"></asp:ListItem>
                                                    </asp:DropDownList>
                                                           </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                  </div>

                                                    <div id="standarSoftware" runat="server">
                                                    <asp:GridView ID="dgvSofware" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                <HeaderTemplate>
                                                                    ID
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtIDSoftware" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                <HeaderTemplate>
                                                                    Items
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtItemSoftware" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate></HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="chkRowSoftware" Checked="false" OnCheckedChanged="chkRowSoftware_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate>
                                                                    <%--Quantity--%>
                                                                    <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkRowSoftware" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtQteSoftware" MaxLength="2" runat="server" Enabled="false" Text="0" Width="50px" TextMode="Number"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate>
                                                                    <%--       Justification (why)--%>
                                                                    <asp:Literal ID="lbdgvJustification" Text="<%$ Resources:LocalizedText, lbdgvJustification%>" runat="server"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkRowSoftware" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCommentSoftware" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
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

                                                    <div id="nonstandardsoftware" runat="server">
                                                          <div>
                                                    <asp:Label ID="Label2" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lbdgvComment %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtCommentNonStandardSoftware" Height="100px" TextMode="MultiLine"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                                    </div>

                                                               </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--FIN SOFTWARE-->

                                                <!--ONGLET NETWORK-->
                                                <div id="network" class="tab-pane">

                                                    <asp:GridView ID="dgvNetwork" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                <HeaderTemplate>
                                                                    ID
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtIDNetwork" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                <HeaderTemplate>
                                                                    Items
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtItemNetwork" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate></HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="chkRowNetwork" Checked="false" OnCheckedChanged="chkRowNetwork_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate>
                                                                    <%--  Quantity--%>
                                                                    <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:UpdatePanel ID="UpdatePanel31" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkRowNetwork" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtQteNetwork" MaxLength="2" runat="server" Enabled="false" Text="1" Width="50px" TextMode="Number"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate>
                                                                    <%--     Justification (why)--%>
                                                                    <asp:Literal ID="lbdgvJustification" Text="<%$ Resources:LocalizedText, lbdgvJustification%>" runat="server"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel32" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkRowNetwork" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCommentNetwork" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
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
                                                <!--FIN ONGLET NETWORK-->

                                                <!--ONGLET APPLICATION-->
                                                <div id="Application" class="tab-pane">
                                                    <asp:UpdatePanel ID="UpdatePanel300" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvApplication" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtIDApplication" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtItemApplication" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkRowApplication" Checked="false" OnCheckedChanged="chkRowApplication_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%--  Quantity--%>
                                                                            <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtQteApplication" runat="server" Enabled="true" Text="1" Width="50px" TextMode="Number"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%--    Justification (why)--%>
                                                                            <asp:Literal ID="lbdgvJustification" Text="<%$ Resources:LocalizedText, lbdgvJustification%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtCommentApplication" Width="100%" Enabled="true" runat="server"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <SelectedRowStyle CssClass="alert-info" />
                                                                <RowStyle CssClass="cursor-pointer" />
                                                                <HeaderStyle CssClass="alert-success" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>
                                                       


                                                    <!--DEBUT FORMULAIRE POUR APPLICATION-->
                                                    <div class="widget-body">
                                                        <div class="step-content pos-rel">
                                                            <div class="step-pane active" data-step="1">
                                                                <div class="widget-main">
                                                                    <div class="form-horizontal" id="Form1">

                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">
                                                                                <span style="color: red">

                                                                                    <asp:Literal ID="divApplicationTitle" Text="<%$ Resources:LocalizedText, divApplicationTitle%>" runat="server"></asp:Literal>
                                                                                </span>
                                                                            </label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block"></span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                <asp:Literal ID="divApplicationName" Text="<%$ Resources:LocalizedText, divApplicationName%>" runat="server"></asp:Literal>
                                                                                <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:DropDownList ID="ddlApplicationName" class="width-100 form-control" runat="server" AutoPostBack="false" Enabled="true"></asp:DropDownList>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>



                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                <asp:Literal ID="divApplicationResponsible" Text="<%$ Resources:LocalizedText, divApplicationResponsible%>" runat="server"></asp:Literal>
                                                                                <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:DropDownList ID="ddlApplicationResponsible" class="width-100 form-control" runat="server" AutoPostBack="false" Enabled="true"></asp:DropDownList>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>



                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                <asp:Literal ID="divApplicationDescription" Text="<%$ Resources:LocalizedText, divApplicationDescription%>" runat="server"></asp:Literal>
                                                                                <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:TextBox ID="txtApplicationDescription" onkeypress="if (this.value.length > 100) { return false; }"  runat="server" TextMode="MultiLine" class="width-100" Enabled="true"></asp:TextBox>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                <asp:Literal ID="divApplicationRaison" Text="<%$ Resources:LocalizedText, divApplicationRaison%>" runat="server"></asp:Literal>
                                                                                <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:TextBox ID="txtApplicationRaison" runat="server" TextMode="MultiLine"  onkeypress="if (this.value.length > 100) { return false; }" class="width-100" Enabled="true"></asp:TextBox>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                <asp:Literal ID="divApplicationSponsor" Text="<%$ Resources:LocalizedText, divApplicationSponsor%>" runat="server"></asp:Literal>

                                                                                <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:DropDownList ID="ddlApplicationSponsor" class="width-100 form-control" runat="server" AutoPostBack="false" Enabled="true"></asp:DropDownList>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
 </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <!--fin formulaire application-->
                                                </div>
                                                <!--fin onglet application-->


                                                <!--onglet fileShare-->
                                                <div id="FileShare" class="tab-pane" style="display:none">

                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvFileShare" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtIDFileShare" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtItemFileShare" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:CheckBox ID="chkRowFileShare" Checked="false" AutoPostBack="true" runat="server" OnCheckedChanged="chkRowFileShare_CheckedChanged" />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%-- Quantity--%>
                                                                            <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtQteFileShare" MaxLength="2" runat="server" Enabled="false" Text="1" Width="50px" TextMode="Number"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <asp:Literal ID="lbdgvJustification" Text="<%$ Resources:LocalizedText, lbdgvJustification%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtCommentFileShare" MaxLength="40" Width="100%" Enabled="false" runat="server"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <SelectedRowStyle CssClass="alert-info" />
                                                                <RowStyle CssClass="cursor-pointer" />
                                                                <HeaderStyle CssClass="alert-success" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>


                                                            
                                                    <!--DEBUT FORMULAIRE POUR filShare-->
                                                    <div class="widget-body">
                                                        <div class="step-content pos-rel">
                                                            <div class="step-pane active" data-step="1">
                                                                <div class="widget-main">
                                                                    <div class="form-horizontal" id="Form3">
                                                                        <asp:Label ID="lblIsFileShare" runat="server" Visible="false" Text="0"></asp:Label>
                                                                      
                                                                        <div id="NewFileShare" runat="server" visible="false">
                                                                           <%-- <center>
                                                                             <label for="inputError2" class="col-12 control-label" style="color: red">

                                                                                <asp:Literal ID="Literal13" Text="<%$ Resources:LocalizedText, lblShareNeedAccess%>" runat="server"></asp:Literal>
                                                                                </label>
                                                                            </center>--%>
                                                                            <br/>
                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                <asp:Literal ID="Literal9" Text="<%$ Resources:LocalizedText, dgvnamefileshare%>" runat="server"></asp:Literal>
                                                                                <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                   <asp:TextBox ID="txtFileShareName" onkeypress="if (this.value.length > 100) { return false; }"  runat="server" TextMode="SingleLine" class="width-100" Enabled="true"></asp:TextBox>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                <asp:Literal ID="Literal10" Text="<%$ Resources:LocalizedText, dgvnDataOwner%>" runat="server"></asp:Literal>
                                                                                <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                     <asp:TextBox ID="txtDataOwnerFileShare" onkeypress="if (this.value.length > 100) { return false; }"  runat="server" TextMode="SingleLine" class="width-100" Enabled="true"></asp:TextBox>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>
                                                                    </div>

                                                                    
                                                                          <div id="FileShareAccess" runat="server" visible="false">
                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                <asp:Literal ID="Literal8" Text="<%$ Resources:LocalizedText, dgvusername%>" runat="server"></asp:Literal>
                                                                                <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                   <asp:TextBox ID="txtUsernameFileShare" onkeypress="if (this.value.length > 100) { return false; }"  runat="server" TextMode="SingleLine" class="width-100" Enabled="true"></asp:TextBox>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                                                <asp:Literal ID="Literal11" Text="<%$ Resources:LocalizedText, dgvEmailAddress%>" runat="server"></asp:Literal>
                                                                                <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                     <asp:TextBox ID="txtEmailAdresseFileShare" onkeypress="if (this.value.length > 100) { return false; }"  runat="server" TextMode="SingleLine" class="width-100" Enabled="true"></asp:TextBox>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>
                                                                    </div>

                                                                       
                                                                       
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>



                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--FIN ONGLET fileShare-->

                                            </div>

                                        </div>
                                        <!--fin bloc onglet detail-->
                                        <br />
                                    </div>

                                    <!-- button pour l'enregistrement et l'initialisation des données-->
                                    <div class="clearfix form-actions">
                                        <div>
                                          
                                            <asp:Button ID="btnSave" cssclass="btn2 btn2-btnSave btn2-cons" OnClick="btnSave_Click" runat="server" Text="<%$ Resources:LocalizedText, btnSave%>"  />
                                           
											 <asp:Button ID="btnReset" OnClick="btnReset_Click" cssclass="btn2 btn2-btnReset btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnReset%>"  />
                                           
                                        </div>

                                    </div>
                                    <!--fin zone de button-->
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


    <!--popup pour le message d'erreur-->


    <div id="my-modal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#682525;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">
                        <asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, popUpInvalidInput%>" runat="server"></asp:Literal>
                    </h3>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <%--  <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />--%>
                            <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                        </Triggers>
                        <ContentTemplate>
                            <h4 class="smaller lighter red no-margin">
                                <asp:Label ID="lblErreur" runat="server" Text=""></asp:Label>
                            </h4>
                            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
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




    





   


    <!--fin popup-->
      <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
     <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
          <div class="modal-content">
        <div class="modal-header" style="background-color:#006666;">
            <h3 class="smaller lighter white no-margin">
                     CONFIRMATION
                    </h3>
        </div>
        <div class="modal-body">  
           <h3 class="smaller lighter green no-margin">
            <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
                  <asp:Label ID="lblIDRequest" runat="server" Visible="false" Text=""></asp:Label>
                            </h3>
     
        </div>
               <div class="modal-footer">
                   
                      <asp:Button ID="btnOk"  class="btn btn-success" OnClick="btnOk_Click"  runat="server" Text="OK"  />

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
       
        function openConfirm() {
       

           

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
            var top = Math.max($(window).height() / 3 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
   
    $('form').live("submit", function () {
        var $btn = $(document.activeElement);
        if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_btnOk") {
            return true;
        }
        else {
            ShowProgress();
        }

    });

    
    
</script>
    <div class="loading" align="center" id="panelLoading" runat="server">
       <h4 class="widget-title red">
    <asp:Literal ID="Literal12" Text="<%$ Resources:LocalizedText, lblLoading%>" runat="server"></asp:Literal>
            </h4>
    <img src="bx_loader.gif" alt="" />
</div>
</asp:Content>
