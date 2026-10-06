<%@ Page Title="" Language="C#" MasterPageFile="~/NewRequest.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="KCCREQUEST2.test" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
         .mid{
             text-align:center;
         }
    </style>

     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

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
                            <asp:Label ID="lblBonEntree" runat="server" Text="<%$ Resources:LocalizedText, lblBonEntree%>"></asp:Label>
                        </h4>
                    </div>



                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">

                       <br />

                                    <div class="form-group">
                                        <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                            <asp:Literal ID="lblRequestor" Text="<%$ Resources:LocalizedText, lblRequestor%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block">
                                                <asp:DropDownList ID="LstRequestor" class="width-100 form-control" runat="server" AutoPostBack="false" Enabled="true"></asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>


                                             
                                    <div class="form-group">
                                        <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                            <asp:Literal ID="lblDepartment" Text="<%$ Resources:LocalizedText, lblDepartment%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block">
                                                <asp:DropDownList ID="LstDepartment" class="width-100 form-control" runat="server" AutoPostBack="false" Enabled="true"></asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>


                                 



                                    <div class="form-group">
                                        <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                            <asp:Literal ID="lblCompany" Text="<%$ Resources:LocalizedText, lblCompany%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block">
                                                <asp:DropDownList ID="LstCompanyName" class="width-100 form-control" runat="server" OnSelectedIndexChanged="LstCompanyName_SelectedIndexChanged" AutoPostBack="true" Enabled="true"></asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>


                                       
                                    
                                    <div class="form-group">
                                        <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                            <asp:Literal ID="lblSite" Text="<%$ Resources:LocalizedText, lblSite%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block">
                                                                    <asp:DropDownList ID="LstSite" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                            </span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>










                                    <div class="form-group">
                                        <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                            <asp:Literal ID="lblreason" Text="<%$ Resources:LocalizedText, lblreason%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block">
                                                <asp:TextBox ID="txtreason" onkeypress="if (this.value.length > 100) { return false; }" runat="server" TextMode="MultiLine" class="width-100" Enabled="true"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>




                                 



                                    <div class="form-group">
                                        <span style="color: rgb(187,135,72); font-weight: bold">
                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                                <asp:Literal ID="lblDetailEscorter" Text="<%$ Resources:LocalizedText, lblDetailEscorter%>" runat="server"></asp:Literal>
                                            </label>
                                        </span>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block"></span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>



                                    <div class="form-group">
                                        <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                            <asp:Literal ID="lblEmployerId" Text="<%$ Resources:LocalizedText, lblEmployerId%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block">
                                                <asp:TextBox ID="txtEmployerId" onkeypress="if (this.value.length > 100) { return false; }" runat="server" class="width-100" Enabled="true"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>




                                    <div class="form-group">
                                        <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                            <asp:Literal ID="Literal4" Text="<%$ Resources:LocalizedText, lblEscoter%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block">
                                                <asp:TextBox ID="txtEscorterName" onkeypress="if (this.value.length > 100) { return false; }" runat="server" class="width-100" Enabled="true"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>





                                    <div class="form-group">
                                        <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                            <asp:Literal ID="Literal5" Text="<%$ Resources:LocalizedText, lblFunctionEscoter%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block">
                                                <asp:TextBox ID="txtEscorterFunction" onkeypress="if (this.value.length > 100) { return false; }" runat="server" class="width-100" Enabled="true"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>


                                    <div class="form-group">
                                        <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">

                                            <asp:Literal ID="lblStartDate" Text="<%$ Resources:LocalizedText, lblStartDate%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                        <div class="col-xs-12 col-sm-6">
                                            <span class="input-icon block">
                                                <asp:TextBox ID="StartDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                                            </span>
                                        </div>
                                        <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                    </div>
                                    <br />


</div>
      <br />

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">
                                       <!-- reseach-->
                                    <br />
                                     <table style="width: 100%; margin: auto">
                                         <tr>
                                            <asp:Literal ID="lblReseach" Text="<%$ Resources:LocalizedText, lblReseach%>" runat="server"></asp:Literal>

                                         </tr>
                                       
                                    
                                    <tr>
                                            <td colspan="2" style="width: 100%">
                                                <div>
                                                  

                                                    <asp:TextBox ID="txtSubject"   MaxLength="70" runat="server" Width="30%" ></asp:TextBox>
                                                   <asp:Button ID="Reseach" cssclass="btn2 btn2-btnReset btn2-cons"  OnClick="Seach_Click"  runat="server" Text="<%$ Resources:LocalizedText, btnSearch%>" />

                                                   
                                                </div>
                                            </td>
                                            
                                        </tr>
                                         </table>
                                    
                                   

                                   
                                    <!--reseach-->

                                    <!-- entite de la requisition-->
                                 

                                    <!--fin entete requesition-->
                                    <%--   <hr />--%>

                                    <!--bloc detail de la requisition-->

                                    <div class="form-horizontal" id="Form2" style="width: 100%;">

                                      
                                        <!--bloc des onglets des detail-->
                                        <div class="tabbable">
                                            
                                            <br />
                                                <!--onglet computer-->
                                                <div id="home4" class="tab-pane in active">

                                                    <asp:UpdatePanel ID="UpdatePanel100" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                             <div runat="server" style="height: 200px; overflow-y: scroll;">
                                                            <asp:GridView ID="dgvComputer" OnRowDataBound="dgvComputer_RowDataBound" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="Id" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
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
                                                                
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblId" Text="<%$ Resources:LocalizedText, lblId%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblIdBonEntree" Text="<%$ Resources:LocalizedText, lblIdBonEntree%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdBonEntree" runat="server" Text='<%#Eval("IdBonEntree")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                    
                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblDesignation" Text="<%$ Resources:LocalizedText, lblDesignation%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("DesignationProduit")%>'></asp:Label>
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
                                                                            <asp:TextBox ID="txtQteComputer" Text='<%#Eval("Quantity")%>' MaxLength="2" runat="server" Enabled="false"  Width="50px" TextMode="Number"></asp:TextBox>

                                                                            <%--</ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     



                                                                  <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblCompany" Text="<%$ Resources:LocalizedText, lblCompany%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCompany" runat="server" Text='<%#Eval("IdCompany")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                






<%--                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblQuantity" Text="<%$ Resources:LocalizedText, lblQuantity%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>



                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblEscoter" Text="<%$ Resources:LocalizedText, lblEscoter%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEscoter" runat="server" Text='<%#Eval("EscorteurName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                


                                                                  <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblDepartment" Text="<%$ Resources:LocalizedText, lblDepartment%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblReason1" Text="<%$ Resources:LocalizedText, lblReason1%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblReason1" runat="server" Text='<%#Eval("Reason")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                               



                                                                   <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblFrom" Text="<%$ Resources:LocalizedText, lblFrom%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrom" runat="server" Text='<%#Eval("FromSite")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 

                                                               

                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="7%" Visible="true">
                                                                    <HeaderTemplate></HeaderTemplate>
                                                                    <ItemTemplate>

<%--                                                                        <asp:Button CssClass="btn2 btn2-btnReset btn2-cons" OnClick="btndelete_Click" ID="btnDetail" runat="server" Text="<%$ Resources:LocalizedText, btndelete%>" />--%>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                               


                                                                 <%--<asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal visible="false" ID="lblFrom" Text="<%$ Resources:LocalizedText, lblFrom%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label Visible="false" ID="lblFrom1" runat="server"  Text='<%#Eval("From1")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>



                                                                </Columns>
                                                                <SelectedRowStyle CssClass="alert-info" />
                                                                <RowStyle CssClass="cursor-pointer" />
                                                                <HeaderStyle CssClass="alert-success" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>
                                                                 </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                             <br />
                                            <table>
                                                  <tr>

                                                            <td>
                                                                <br />
                                                                <asp:Button ID="btnAddContractor" onclick="btnAddContractor_Click" CssClass="btn2 btn2-btnSave btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnAddEstimation%>" />
                                                            </td>
                                                        </tr>
                                            </table>
                                              <br />

                                                                <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center">
                        <h4 class="widget-title lighter">
  
                              <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label>
                              <asp:Label ID="Label13" runat="server" Text="<%$ Resources:LocalizedText, lblItemRelease%>"></asp:Label>
                             <span class="badge badge" style="background-color:#003468" id="Span2" runat="server"></span>
                        </h4>
                    </div>


                                            <div id="profile5" class="tab-pane in active">
                                                  
                                       

                                                    <%--     <asp:DropDownList ID="LstContractor" OnSelectedIndexChanged="LstContractor_SelectedIndexChanged" class="form-control" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>--%>
                                                   

                                                    <br />
                                                    <div runat="server" style="height: 200px; overflow-y: scroll;">
                                                        <asp:GridView ID="GridView2" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="Id" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                            <Columns>

                                                                
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblId" Text="<%$ Resources:LocalizedText, lblId%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                               
                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblDesignation" Text="<%$ Resources:LocalizedText, lblDesignation%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("DesignationProduit")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtQteComputer" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                              <%--   
                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                        <HeaderTemplate>

                                                                            <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtComment" runat="server" Text='<%#Eval("Comment")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

                                         
                     



                                                           




                                                            </Columns>
                                                            <SelectedRowStyle CssClass="alert-info" />
                                                            <RowStyle CssClass="cursor-pointer" />
                                                            <HeaderStyle CssClass="alert-success" />
                                                            <PagerStyle HorizontalAlign="Center" />
                                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                        </asp:GridView>
                                                        </div>
                                                    
                                                
                                    </div>
                                                
                                              <br/>


                                               <%--Detail Escorteur--%>




  <div class="clearfix form-actions">
                                        <div id="mid">
                                          
                                            <asp:Button ID="btnSave" cssclass="btn2 btn2-btnSave btn2-cons" OnClick="btnSave_Click" runat="server" Text="<%$ Resources:LocalizedText, btnSave%>"  />
                                           
											 <asp:Button ID="btnReset" OnClick="btnReset_Click" cssclass="btn2 btn2-btnReset btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnReset%>"  />
                                           
                                        </div>

                                    </div>
                                 

                                    </div>


                                            <%--Fin Detail Escorteur--%>



                                            </div>
                                                                                    </div>
                                                <!--fin onglet computer-->

                                                <!--onglet telephone-->
                                               
                                                <!--FIN ONGLET TELEPHONE-->


                                                <!--ONGLET CONNECTIVITY-->
                                                

                                                    <!--DEBUT FORMULAIRE POUR NEW USER ACCOUNT-->
                                                   
                                                    <!--fin formulaire NEW USER ACCOUNT-->

                                              
                                                <!--FIN ONGLET CONNECTIVITY-->

                                                <!--ONGLET SOFTWARE-->
                                                <div id="sofware" class="tab-pane">
                                                       <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                    
                                             

                                                    <div id="standarSoftware" runat="server">
                                                   
                                                  </div>

                                                    
                                                               </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--FIN SOFTWARE-->

                                                <!--ONGLET NETWORK-->
                                                
                                                <!--FIN ONGLET NETWORK-->

                                                <!--ONGLET APPLICATION-->
                                              

                                                    <!--DEBUT FORMULAIRE POUR APPLICATION-->
                                                 
                                                    <!--fin formulaire application-->
                                                </>
                                                <!--fin onglet application-->


                                                <!--onglet fileShare-->
                                               
                                                    <!--DEBUT FORMULAIRE POUR filShare-->
                                                   
                                                <!--FIN ONGLET fileShare-->

                                            </div>

                                        </div>
                                        <!--fin bloc onglet detail-->
                                        <br />
                                    </div>

                                    <!-- button pour l'enregistrement et l'initialisation des données-->
                                   
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
                                            </ul>


                                            <div class="tab-content">
                                                <!--onglet computer-->
                                                <div id="home4" class="tab-pane in active">

                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                          GrideView
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--fin onglet computer-->


                                            </div>

                                        </div>
                                        <!--fin bloc onglet detail-->
                                        <br />
                                    </div>