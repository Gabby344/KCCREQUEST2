<%@ Page Title="" Language="C#" MasterPageFile="~/ViewRequest.Master" AutoEventWireup="true" CodeBehind="Detail_ewoRequest.aspx.cs" Inherits="KCCREQUEST2.Detail_ewoRequest" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style type="text/css">
        .modalBackground {
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
       background-color:#e42526;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>


    <script type="text/javascript">
        function DisableButton() {

            if (document.getElementById("<%=btnSaveEstimationDetail.ClientID %>")) {

                document.getElementById("<%=btnSaveEstimationDetail.ClientID %>").disabled = true;
            }
            document.getElementById("<%=btnOk.ClientID %>").disabled = true;

          

        }
        window.onbeforeunload = DisableButton;
    </script>

    <style>
.ProductPropertyLabel {
    font-weight: bold;
}
    </style>
    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
    <%--            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1><asp:Label ID="Label6" runat="server" Text="<%$ Resources:LocalizedText, lblElectroniqueWorkRequest%>"></asp:Label>
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    <%--Detail Request--%>
                                </small>
                    </h1>
                </div>
                <!--FIN TITRE DE LA PAGE-->



                <!--bloc pour les information du requisitionneur-->
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center;">
                        <h4 class="widget-title lighter">
                            <asp:Literal ID="detailEWtitle" Text="<%$ Resources:LocalizedText, detailEWtitle%>" runat="server"></asp:Literal>
                            <span class="badge badge" style="background-color:#003468" id="title" runat="server"></span></h4>
                    </div>

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">

                                    <!-- entite de la requisition-->
                                    <table style="width: 100%;">

                                        <tr>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblDepartment" Text="<%$ Resources:LocalizedText, lblDepartment%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstDepartment" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstDepartment_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
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
                                                    <label for="form-field-select-1">Site <b style="color: red">*</b></label><br />
                                                    <asp:DropDownList ID="LstSite" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstSite_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblFleet" Text="<%$ Resources:LocalizedText, lblFleet%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:DropDownList ID="LstFleet" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstFleet_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblEquipment" Text="<%$ Resources:LocalizedText, lblEquipment%>" runat="server"></asp:Literal>

                                                    </label>
                                                    <br />
                                                    <asp:DropDownList ID="LstEquipement" class="chosen-select form-control" Enabled="false" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblTechnicalDepartment" Text="<%$ Resources:LocalizedText, lblTechnicalDepartment%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />
                                                    <asp:TextBox ID="txtTechnicalDepartement" MaxLength="40" Enabled="false" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblTechnicalManager" Text="<%$ Resources:LocalizedText, lblTechnicalManager%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />
                                                    <asp:TextBox ID="txtTechnicalManager" MaxLength="35" Enabled="false" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblTechnicalPlanner" Text="<%$ Resources:LocalizedText, lblTechnicalPlanner%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br />
                                                    <asp:TextBox ID="txtTechnicalPlanner" MaxLength="40" Enabled="false" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>


                                         <tr id="blocEqp" runat="server">
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1"> <asp:Literal ID="Literal20" Text="<%$ Resources:LocalizedText, lblWorkType%>" runat="server"></asp:Literal> <b style="color: red">*</b></label><br />
                                                   <asp:DropDownList ID="LstWorkType" class="chosen-select form-control" Width="90%" runat="server" OnSelectedIndexChanged="LstWorkType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal21" Text="<%$ Resources:LocalizedText, lblTask%>" runat="server"></asp:Literal>
                                                        
                                                         <b style="color: red">*</b></label><br />
                                                   <asp:DropDownList ID="LstTask" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                            
                                        </tr>


                                          <tr>
                                            <td>
                                               
                                                        <div> 
                                                           
                                                            <label for="form-field-select-1">
                                                              
                                                                  <asp:Literal ID="Literal28" Text="<%$ Resources:LocalizedText, lblBudgetedCheck%>" runat="server"></asp:Literal>?:
                                                                   <b style="color: red">*</b>
                                                            </label>
                                                           <%--  <asp:CheckBox ID="cbDetailBudged" OnCheckedChanged="cbDetailBudged_CheckedChanged" Enabled="false"  AutoPostBack="true" runat="server" class="ace" /><span>  </span>--%>
                                                            <br>
                                                            <asp:DropDownList ID="LstBudged"  Enabled="false" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                        </div>
                                                    
                                            </td>
                                            </tr>


                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblRequestDescription" Text="<%$ Resources:LocalizedText, lblRequestDescription%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtRequestDescription" onkeypress="if (this.value.length > 250) { return false; }" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblJustificationWhy" Text="<%$ Resources:LocalizedText, lblJustificationWhy%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtJustificationWhy" onkeypress="if (this.value.length >250) { return false; }" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr style="display:none">
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal17" Text="<%$ Resources:LocalizedText, lblScopeOfWork%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtScopeOfWork" onkeypress="if (this.value.length > 250) { return false; }" Text="ScopeOfWork" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td colspan="2" style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblDesigned" Text="<%$ Resources:LocalizedText, lblDesigned%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtDesignedExisting" onkeypress="if (this.value.length > 250) { return false; }" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
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

                                                                                <asp:Button CssClass="btn  btn-xs btn-danger" ID="btnview" OnClick="btnview_Click" runat="server" Text="<%$ Resources:LocalizedText, lblView%>" />

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
                                    <br />
                                    <!--fin entete requesition-->


                                    <!--bloc des onglets estimation et historique-->
                                    <div class="tabbable">
                                        <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab4">
                                            

                                            <li class="active">
                                                <a data-toggle="tab" href="#profile4">
                                                    <asp:Literal ID="zoneHistory" Text="<%$ Resources:LocalizedText, zoneHistory%>" runat="server"></asp:Literal>

                                                </a>
                                            </li>
                                            <li>
                                                <a data-toggle="tab" href="#home4">Estimation</a>
                                            </li>

                                            <li>
                                                <a data-toggle="tab" href="#profile5">
                                                    <asp:Literal ID="zoneDetailAMT" Text="<%$ Resources:LocalizedText, zoneDetailAMT%>" runat="server"></asp:Literal>

                                                </a>
                                            </li>
                                        </ul>


                                        <div class="tab-content">
                                            <!--onglet Estimation-->
                                            <div id="home4" class="tab-pane">
                                                <asp:Panel ID="estimation2" runat="server">

                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <%-- <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAddEstimation" />
                                                        </Triggers>--%>
                                                        <ContentTemplate>
                                                            <asp:Panel ID="estimation" runat="server">
                                                                <table style="width: 100%;">

                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            <div>
                                                                                <label for="form-field-select-1">Description <b style="color: red">*</b></label>
                                                                                <br>
                                                                                <asp:TextBox ID="txtDescriptionEstimation" runat="server" Width="90%"></asp:TextBox>
                                                                            </div>
                                                                        </td>


                                                                      
                                                                          <td style="width: 50%">
                                                                            <div>
                                                                                <label for="form-field-select-1">
                                                                                    <asp:Literal ID="lblUnitCost" Text="<%$ Resources:LocalizedText, lblUnitCost%>" runat="server"></asp:Literal>
                                                                                    <b style="color: red">*</b></label><br />
                                                                                <asp:TextBox ID="txtUnitCostEstimation" runat="server" Width="90%"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        


                                                                    </tr>
                                                                    <tr>
                                                                       
                                                                        
                                                                       <td style="width: 50%">
                                                                            <div>
                                                                                <label for="form-field-select-1">
                                                                                    <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                                    <b style="color: red">*</b></label><br />
                                                                                <asp:TextBox ID="txtQuantity" runat="server" Width="90%"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 50%">
                                                                            <div>
                                                                                <label for="form-field-select-1">
                                                                                    <asp:Literal ID="lblUnitMesure" Text="<%$ Resources:LocalizedText, lblUnitMesure%>" runat="server"></asp:Literal>
                                                                                    <b style="color: red">*</b></label><br />
                                                                                <asp:TextBox ID="txtUnitMesure" runat="server" Width="90%"></asp:TextBox>
                                                                            </div>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="width: 50%"></td>

                                                                        <td style="width: 50%">
                                                                            <br />
                                                                            <div>
                                                                                <%--   <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>--%>
                                                                                <asp:Button ID="btnAddEstimation" OnClick="btnAddEstimation_Click" cssclass="btn2 btn2-btnSave btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnAddEstimation%>" />
                                                                                <%--</ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                            </div>
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </asp:Panel>

                                                            <br />

                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddEstimation" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="dgvEstimation" OnRowDataBound="dgvEstimation_RowDataBound" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="Description" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="25%" ItemStyle-Width="12%">
                                                                                <HeaderTemplate>
                                                                                    Description
                                                
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                              <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="12%">
                                                                                <HeaderTemplate>
                                                                                    <asp:Literal ID="lblUnitCost" Text="<%$ Resources:LocalizedText, lblUnitCost%>" runat="server"></asp:Literal>

                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUnitCost" runat="server" Text='<%#Eval("UnitCost")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="12%">
                                                                                <HeaderTemplate>
                                                                                    <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>

                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblQuantite" runat="server" Text='<%#Eval("Qte")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                          

                                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="15%" ItemStyle-Width="7%">
                                                                                <HeaderTemplate>
                                                                                    <asp:Literal ID="lblUnitMesure" Text="<%$ Resources:LocalizedText, lblUnitMesure%>" runat="server"></asp:Literal>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUnitMesure" runat="server" Text='<%#Eval("UnitMesure")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                                                                <HeaderTemplate>
                                                                                    Total
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="7%" Visible="false">
                                                                                <HeaderTemplate></HeaderTemplate>
                                                                                <ItemTemplate>

                                                                                    <asp:Button cssclass="btn2 btn2-btnReset btn2-cons" OnClick="btndelete_Click" ID="btndelete" runat="server" Text="<%$ Resources:LocalizedText, btndelete%>" />

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

                                                            <br />
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddEstimation" />
                                                                    <asp:AsyncPostBackTrigger ControlID="dgvEstimation" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <div class="widget-header widget-header-black widget-header-flat" style="text-align: right;">
                                                                        <h4 class="widget-title lighter">Total: <span class="badge badge" style="background-color:#e42526" id="lbltotalgeneral" runat="server">0</span></h4>
                                                                    </div>

                                                                     </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                                    <div class="widget-header widget-header-black widget-header-flat" style="text-align: right;">
                                                                        <asp:Button cssclass="btn2 btn2-btnReset btn2-cons" Visible="false" ID="btnSaveEstimation" runat="server" Text="<%$ Resources:LocalizedText, btnSaveEstimation%>" OnClick="btnSaveEstimation_Click" />
                                                                    </div>
                                                               
                                                </asp:Panel>
                                            </div>
                                            <!--fin onglet estimation-->

                                            <!--onglet history-->
                                            <div id="profile4" class="tab-pane in active">
                                                <asp:GridView ID="dgvHistory" runat="server" ShowHeader="true" ShowFooter="true" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
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
                                                                Date
                                                
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

                                            </div>
                                            <!--FIN ONGLET history-->


                                            <!--onglet DETAIL AMT-->
                                            <div id="profile5" class="tab-pane">
                                          
                                                <asp:FormView ID="fvDetailAmt" runat="server" style="width:100%" EnableViewState="False">
                                                    <ItemTemplate>
                                                        <table style="width:100%" class="table table-bordered table-striped">
                                                            <tr>
                                                                <td class="ProductPropertyLabel">
                                                                     <asp:Literal ID="lblAmtCost" Text="<%$ Resources:LocalizedText, lblAmtCost%>" runat="server"></asp:Literal>:

                                                                </td>
                                                                <td class="ProductPropertyValue">
                                                                     <span class="badge badge" style="background-color:#e42526">
                                                                    <%# Eval("AMTCost") %>
                                                                    </span>
                                                                </td>

                                                                <td class="ProductPropertyLabel">
                                                                    <asp:Literal ID="lblAMTPlannedDuration" Text="<%$ Resources:LocalizedText, lblAMTPlannedDuration%>" runat="server"></asp:Literal>:

                                                                </td>
                                                                <td class="ProductPropertyValue">
                                                                      <span class="badge badge" style="background-color:#e42526">
                                                                    <%# Eval("AMTPlannedDuration")%></span></td>
                                                                  
                                                            </tr>
                                                            <tr>
                                                                <td class="ProductPropertyLabel">
                                                                      <asp:Literal ID="lblAMTDuration" Text="<%$ Resources:LocalizedText, lblAMTDuration%>" runat="server"></asp:Literal>:

                                                                </td>
                                                                
                                                                <td class="ProductPropertyValue">
                                                                     <span class="badge badge" style="background-color:#e42526">
                                                                    <%# Eval("AMTDuration") %></span></td>
                                                                
                                                                <td class="ProductPropertyLabel">
                                                                     <asp:Literal ID="lblAMTAge" Text="<%$ Resources:LocalizedText, lblAMTAge%>" runat="server"></asp:Literal>:

                                                                </td>
                                                                <td class="ProductPropertyValue">
                                                                     <span class="badge badge" style="background-color:#e42526">
                                                                    <%# Eval("AMTAge")%></span></td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="ProductPropertyLabel">
                                                                   <asp:Literal ID="lblAMTPlanLabourHours" Text="<%$ Resources:LocalizedText, lblAMTPlanLabourHours%>" runat="server"></asp:Literal>:

                                                                </td>
                                                                <td class="ProductPropertyValue">
                                                                     <span class="badge badge" style="background-color:#e42526">
                                                                    <%# Eval("AMTPlanLabourHours") %> </span></td>
                                                               
                                                                <td class="ProductPropertyLabel">
                                                                   <asp:Literal ID="lblAMTWorkOrderNumber" Text="<%$ Resources:LocalizedText, lblAMTWorkOrderNumber%>" runat="server"></asp:Literal>:

                                                                </td>
                                                                <td class="ProductPropertyValue">
                                                                     <span class="badge badge" style="background-color:#e42526">
                                                                    <%# Eval("AMTWorkOrderNumber")%>
                                                                          </span></td>
                                                               
                                                            </tr>
                                                            <tr>
                                                                <td class="ProductPropertyLabel">
                                                                    <asp:Literal ID="lblAMTWorkorderID" Text="<%$ Resources:LocalizedText, lblAMTWorkorderID%>" runat="server"></asp:Literal>:

                                                                </td>
                                                                <td class="ProductPropertyValue">
                                                                     <span class="badge badge" style="background-color:#e42526">
                                                                    <%# Eval("AMTWorkOrderID") %>   </span></td>
                                                             
                                                                <td class="ProductPropertyLabel">
                                                                    <asp:Literal ID="Literal18" Text="<%$ Resources:LocalizedText, lblAMTStatusDescription%>" runat="server"></asp:Literal>::

                                                                </td>
                                                                <td class="ProductPropertyValue"> 
                                                                      <span class="badge badge" style="background-color:#e42526">
                                                                     <%# Eval("AMTStatusDescription") %>
                                                                          </span>
                                                                          </td>
                                                                
                                                            </tr>

                                                            <tr>
                                                                <td class="ProductPropertyLabel" colspan="2">
                                                                     <asp:Literal ID="lblAMTCurrentUser" Text="<%$ Resources:LocalizedText, lblAMTCurrentUser%>" runat="server"></asp:Literal>:

                                                                </td>
                                                                <td class="ProductPropertyValue" colspan="2">
                                                                      <span class="badge badge" style="background-color:#e42526">
                                                                    <%# Eval("AMTCurrentUser") %>
                                                                          </span>
                                                                          </td>
                                                              
                                                            </tr>
                                                        </table>
                                                        <hr />
                                                    </ItemTemplate>
                                                </asp:FormView>

                                            </div>
                                            <!--FIN ONGLET detail AMT-->


                                        </div>

                                    </div>
                                    <!--fin bloc onglet estimation and history-->


                                    <!-- button pour l'enregistrement et l'initialisation des données-->
                                    <div class="clearfix form-actions" id="blockbtn" runat="server">
                                        <div>


                                            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" class="btn btn-info btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Update_Text%>" />

                                            <asp:Button ID="btnApproval" class="btn btn-success btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Approve_Text%>" OnClick="btnApproval_Click" />

                                            <asp:Button ID="BtnReject" class="btn btn-danger btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Reject_Text%>" OnClick="BtnReject_Click" />

                                            <asp:Button ID="BtnReturnToOrginator" class="btn btn-primary btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Return_Text%>" OnClick="BtnReturnToOrginator_Click" />

                                            <asp:Button ID="btnBackToOriginator" class="btn btn-purple btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btnBackToPrevious%>" OnClick="btnBackToOriginator_Click" Visible="false" />

                                            <asp:Button ID="btnrRealocate" class="btn btn-warning btn-sm" OnClick="btnrRealocate_Click" runat="server" Text="<%$ Resources:LocalizedText, btnrRealocate%>" />

                                            <asp:Button ID="btnAdHoc" class="btn btn-pink btn-sm" OnClick="btnAdHoc_Click" runat="server" Text="Ad Hoc" />

                                             <asp:Button ID="btnCallBack"  class="btn btn-danger btn-sm" OnClick="btnCallBack_Click"  runat="server" Text="<%$ Resources:LocalizedText, btnCallBack%>"/>

                                            <asp:Button ID="btnPrint" class="btn btn-inverse btn-sm" OnClick="btnPrint_Click" runat="server" Text="<%$ Resources:LocalizedText, btnPrint%>" />

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


    <!--popup pour TECHNICAL PLANNER STATUT 5 -->


    <div id="my-modal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">

                
                        <div class="modal-header" style="background-color:#006666;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h3 class="smaller lighter white no-margin">
                                <asp:Literal ID="litModalHeader" Text="<%$ Resources:LocalizedText, lblInsertComment%>" runat="server"></asp:Literal>

                            </h3>
                            <asp:Label ID="lblaction" runat="server" Text="0" Visible="false"></asp:Label>
                        </div>
                        <h4 class="smaller lighter red no-margin">
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>

                        </h4>



                        <asp:GridView ID="GridView1" runat="server"></asp:GridView>


                        <div class="modal-body">
                            <asp:UpdatePanel ID="updatePanelPopUpPlan" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <%--  <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />--%>
                        <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                   <%--     <asp:AsyncPostBackTrigger ControlID="btnSaveEstimationDetail" />--%>
                    <%--        <asp:AsyncPostBackTrigger ControlID="btnSaveEstimation" />--%>
                        
                    </Triggers>
                    <ContentTemplate>

                            <h4 class="smaller lighter red no-margin">
                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:TextBox ID="txtComment"  TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                </div>

                            </h4>
                          </ContentTemplate>
                </asp:UpdatePanel>
                        </div>

                        <div class="modal-footer">


                            <asp:Button ID="btnSaveEstimationDetail" class="btn btn-sm btn-info" runat="server" Text="OK" OnClick="btnSaveEstimationDetail_Click" />


                            <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                                <i class="ace-icon fa fa-times"></i>
                                <asp:Literal ID="Literal7" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                            </button>
                        </div>
                        </div>
                  
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        </div>
        <!--fin TECHNICAL PLANNER STATUT 5-->



        <!--popup pour DISPATCHER STATUT 3-->


        <div id="my-modalDispatcher" class="modal fade" tabindex="-1">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="updatePanelDispatch" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <%--  <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />--%>
                        <asp:AsyncPostBackTrigger ControlID="btnForward" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header" style="background-color:#006666;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h3 class="smaller lighter white no-margin">Dispatcher</h3>
                                <asp:Label ID="Label1" runat="server" Text="0" Visible="false"></asp:Label>
                            </div>

                            <div class="modal-body">


                                <h4 class="smaller lighter black no-margin">
                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal2" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label><br />
                                        <asp:TextBox ID="txtCommentDispacher" TextMode="SingleLine" runat="server" Width="70%"></asp:TextBox>
                                    </div>

                                </h4>


                                <%-- <br />
                    <h4 class="smaller lighter black no-margin">
                        <div>
                            <label for="form-field-select-1">Equipement <b style="color: red">*</b></label><br />
                            <asp:DropDownList ID="LstEquipement" class="chosen-select form-control" Width="70%" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>

                    </h4>--%>
                                <br />
                                <h4 class="smaller lighter black no-margin">
                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblTechnicalDepartment%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label><br />
                                        <asp:DropDownList ID="LstTechnicalDepartment" class="chosen-select form-control" Width="70%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstTechnicalDepartment_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                </h4>
                                <br />

                                <h4 class="smaller lighter black no-margin">
                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal4" Text="<%$ Resources:LocalizedText, lblTechnicalManager%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label><br />
                                        <asp:DropDownList ID="LstTechnicalManager" class="chosen-select form-control" Width="70%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                </h4>


                            </div>

                            <div class="modal-footer">


                                <asp:Button ID="btnForward" class="btn btn-sm btn-info" runat="server" Text="<%$ Resources:LocalizedText, btnForward%>" OnClick="btnForward_Click" />


                                <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                                    <i class="ace-icon fa fa-times"></i>
                                    <asp:Literal ID="Literal8" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                                </button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>


        <!--fin popup DISPATCHER STATUT 3-->




        <!--popup pour MANAGER STATUT 4-->


        <div id="my-modalManager" class="modal fade" tabindex="-1">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="updatePanelManager" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <%--  <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />--%>
                        <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header" style="background-color:#006666;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h3 class="smaller lighter white no-margin">
                                    <asp:Literal ID="Literal5" Text="<%$ Resources:LocalizedText, lblTechnicalManager%>" runat="server"></asp:Literal>

                                </h3>
                                <asp:Label ID="Label2" runat="server" Text="0" Visible="false"></asp:Label>
                            </div>

                            <div class="modal-body">


                                <h4 class="smaller lighter black no-margin">
                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal6" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label><br />
                                        <asp:TextBox ID="txtCommentManager" TextMode="SingleLine" runat="server" Width="70%"></asp:TextBox>
                                    </div>

                                </h4>


                                <br />
                                <h4 class="smaller lighter black no-margin">
                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="lblTechnicalPlannerName" Text="<%$ Resources:LocalizedText, lblTechnicalPlannerName%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label><br />
                                        <asp:DropDownList ID="LstTechicalPlannerName" class="chosen-select form-control" Width="70%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                </h4>

                                <br />
                                <h4 class="smaller lighter black no-margin">
                                    <div style="display:none">
                                        <label for="form-field-select-1">Budget: </label>
                                        <br />
                                        <asp:DropDownList ID="LstBudgetHr"  class="form-control" Width="80%" runat="server" AutoPostBack="true">

                                                         <asp:ListItem Text="<%$ Resources:LocalizedText, selectBudgetdNo%>"></asp:ListItem>
                                                         <asp:ListItem Text="<%$ Resources:LocalizedText, selectBudgetdYes%>"></asp:ListItem>
                                                     
                            </asp:DropDownList>
                                    </div>

                                </h4>


                            </div>

                            <div class="modal-footer">


                                <asp:Button ID="btnAssign" class="btn btn-sm btn-info" runat="server" Text="OK" OnClick="btnAssign_Click" />


                                <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                                    <i class="ace-icon fa fa-times"></i>
                                    <asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                                </button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>


        <!--fin popup MANAGER STATUT 4-->



        <!--popup pour le message d'erreur-->


        <div id="modal_erreur" class="modal fade" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color:#682525;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 class="smaller lighter white no-margin"> <asp:Label ID="txtInvalidInput" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, txtInvalidInput %>"></asp:Label></h3>
                    </div>

                    <div class="modal-body">

                           <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                            <Triggers>
                             
<%--                                <asp:AsyncPostBackTrigger ControlID="btnApproval" />
                                <asp:AsyncPostBackTrigger ControlID="BtnReject" />--%>
                               <%-- <asp:AsyncPostBackTrigger ControlID="btnSaveEstimation" />--%>
                          <%--      <asp:AsyncPostBackTrigger ControlID="dgvEstimation" />--%>
                                <asp:AsyncPostBackTrigger ControlID="btnAddEstimation" />
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
                            <i class="ace-icon fa fa-times">
                                <asp:Literal ID="Literal11" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                            </i>
                        </button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>



        <!--popup pour reallocate-->


        <div id="my-reallocate" class="modal fade" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color:#006666;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 class="smaller lighter white no-margin"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:LocalizedText, lblElectroniqueWorkRequest%>"></asp:Label> -  <asp:Label ID="Label9" runat="server" Text="<%$ Resources:LocalizedText, btnrRealocate%>"></asp:Label></h3>
                        <asp:Label ID="Label4" runat="server" Text="0" Visible="false"></asp:Label>
                    </div>

                    <div class="modal-body">


                        <h4 class="smaller lighter red no-margin">

                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>

                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal12" Text="<%$ Resources:LocalizedText, lblUsers%>" runat="server"></asp:Literal>

                                            <b style="color: red">*</b></label><br />
                                        <asp:DropDownList ID="dllUsers" class="chosen-select  form-control" OnSelectedIndexChanged="dllUsers_SelectedIndexChanged1" Width="100%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    </div>


                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal13" Text="<%$ Resources:LocalizedText, lblRole%>" runat="server"></asp:Literal>

                                            <b style="color: red">*</b></label><br />
                                        <asp:DropDownList ID="dllRole" class="chosen-select  form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    </div>


                                     <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal22" Text="<%$ Resources:LocalizedText, lblStatut%>" runat="server"></asp:Literal>

                                            <b style="color: red">*</b></label><br />
                                        <asp:DropDownList ID="dllStatus" class="chosen-select  form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                    </div>





                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal14" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                            <b style="color: red">*</b></label><br />
                                        <asp:TextBox ID="txtCommentReallocate" runat="server" Width="100%"></asp:TextBox>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </h4>

                    </div>

                    <div class="modal-footer">


                        <asp:Button ID="BtnSaveReallocate" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="BtnSaveReallocate_Click" class="btn btn-sm btn-info" runat="server" Text="OK" />


                        <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                            <i class="ace-icon fa fa-times"></i>
                            Close
                        </button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <!--fin popup-->


        <!--popup pour adhoc-->


        <div id="my-adhoc" class="modal fade" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color:#006666;">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h3 class="smaller lighter white no-margin"><asp:Label ID="Label7" runat="server" Text="<%$ Resources:LocalizedText, lblElectroniqueWorkRequest%>"></asp:Label> Ad-Hoc</h3>
                        <asp:Label ID="Label5" runat="server" Text="0" Visible="false"></asp:Label>
                    </div>

                    <div class="modal-body">


                        <h4 class="smaller lighter red no-margin">

                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>

                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal15" Text="<%$ Resources:LocalizedText, lblUsers%>" runat="server"></asp:Literal>

                                            <b style="color: red">*</b></label><br />
                                        <asp:DropDownList ID="dllUserAdHoc" class="chosen-select  form-control" Width="100%" runat="server"></asp:DropDownList>
                                    </div>




                                    <div>
                                        <label for="form-field-select-1">
                                            <asp:Literal ID="Literal16" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                            <b style="color: red">*</b></label><br />
                                        <asp:TextBox ID="txtCommentAdHoc" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </h4>

                    </div>

                    <div class="modal-footer">


                        <asp:Button ID="btnAdHocSav" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="btnAdHocSav_Click" class="btn btn-sm btn-info" runat="server" Text="OK" />


                        <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                            <i class="ace-icon fa fa-times"></i>
                            Close
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
                <h3 class="smaller lighter white no-margin">CONFIRMATION
                </h3>
            </div>
            <div class="modal-body">
                <h3 class="smaller lighter green no-margin">
                    <asp:Label ID="lblActionSuccess" runat="server" Text="<%$ Resources:LocalizedText, lblActionSuccess%>"></asp:Label>
                   
                </h3>

            </div>
            <div class="modal-footer">

                <asp:Button ID="btnOk" class="btn btn-success" OnClick="btnOk_Click" runat="server" Text="OK" />

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

            function openModalDispatcher() {
                $('#my-modalDispatcher').modal('show');
            }
            function hideModalDispatcher() {
                $('#my-modalDispatcher').modal('hide');
            }
            function openModalManager() {
                $('#my-modalManager').modal('show');
            }
            function hideModalManager() {
                $('#my-modalManager').modal('hide');
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
            var top = Math.max($(window).height() /2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
   
    $('form').live("submit", function () {
        var $btn = $(document.activeElement);

        //alert($btn.attr("id"))

        if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_dgvFiles_ctl02_btnview") {
            return true;
        }
        else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_btnOk") {
            return true;
        }
        else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_btnAddEstimation") {
            return true;
        }
        else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_btnSaveEstimation") {
            return true;
        }
        else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_dgvEstimation_ctl03_btndelete") {
            return true;
        }
        else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_dgvEstimation_ctl04_btndelete") {
            return true;
        }
        else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_dgvEstimation_ctl02_btndelete") {
            return true;
        } 
        else {
          
            ShowProgress();
        }

    });
    
    
</script>
    <div class="loading" align="center" id="panelLoading" runat="server">
       <h4 class="widget-title red">
    <asp:Literal ID="Literal19" Text="<%$ Resources:LocalizedText, lblLoading%>" runat="server"></asp:Literal>
            </h4>
    <img src="bx_loader.gif" alt="" />
</div>
</asp:Content>
