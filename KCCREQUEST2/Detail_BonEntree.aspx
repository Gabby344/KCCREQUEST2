<%@ Page Title="" Language="C#" MasterPageFile="~/ViewRequest.Master" AutoEventWireup="true"  CodeBehind="Detail_BonEntree.aspx.cs" Inherits="KCCREQUEST2.Detail_BonEntree" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

  <%--   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
     <script type="text/javascript">
         function DisableButton() {

             if (document.getElementById("<%=btnSave.ClientID %>")) {

                 document.getElementById("<%=btnSave.ClientID %>").disabled = true;
             }

        if (document.getElementById("<%=btnSaveSec.ClientID %>")) {

                 document.getElementById("<%=btnSaveSec.ClientID %>").disabled = true;
             }
  <%--if (document.getElementById("<%=btnSavePlan.ClientID %>")) {

                 document.getElementById("<%=btnSavePlan.ClientID %>").disabled = true;
  }--%>

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
               


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1>MATERIAL CONTROL
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    <%--   New Request--%>
                                </small>
                    </h1>
                </div>
                <!--FIN TITRE DE LA PAGE-->



                <!--bloc pour les information du requisitionneur-->
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center">
                        <h4 class="widget-title lighter">
  
                              <asp:Label ID="txtTrainingEventID" runat="server" Text="Label" Visible="false"></asp:Label>
                              <asp:Label ID="detailTrainingRequest" runat="server" Text="<%$ Resources:LocalizedText, lblBonEntree%>"></asp:Label>
                             <span class="badge badge" style="background-color:#003468" id="title" runat="server"></span>
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
                                                </div> <br> 
                                            </td>

                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblRequestor" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblRequestor %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestor" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div> <br> 
                                            </td>

                                        </tr>





                                        <tr>
                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    
                                                    <asp:Label ID="lblCompanys" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblCompany %>"></asp:Label><b style="color: red"> *</b>
                                                    <br />
                                                    <asp:DropDownList ID="LstCompanyName"  class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                   <%--<asp:DropDownList ID="" class="width-100 form-control" runat="server" AutoPostBack="false" Enabled="true"></asp:DropDownList>--%>
                                                      </div> <br> 
                                            </td>

                                            <td style="width: 50%">


                                                   <div><asp:Label ID="lblContracts" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblContract %>"></asp:Label><b style="color: red"> *</b>    <br>                                      
                                                       <asp:TextBox ID="txtContract" onkeypress="if (this.value.length > 100) { return false; }" runat="server"  class="width-100" Enabled="true"></asp:TextBox>
                                                </div> <br>


                                            
                                            </td>

                                        </tr>


                                              


                                        
                                           <tr>
                                            <td style="width: 50%">

                                                       
                                                    <div><asp:Label ID="lblContractorManagers" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblContractorManager %>"></asp:Label><b style="color: red"> *</b>
                                                 
                                                           <br>
                                                    <asp:TextBox ID="txtContractorManager" MaxLength="50"  runat="server" Width="90%"></asp:TextBox>
                                                    <%--<asp:TextBox ID="" onkeypress="if (this.value.length > 100) { return false; }" runat="server" class="width-100" Enabled="true"></asp:TextBox>--%>
                                                </div> <br>
                                           
                                            </td>

                                            <td style="width: 50%">
                                                           
                                                    <div>
                                                        <asp:Label ID="lblContractorEmails" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblContractorEmail %>"></asp:Label><b style="color: red"> *</b>
                                                 
                                                           <br>
                                                    <asp:TextBox ID="txtContractorEmail" MaxLength="50"  runat="server"  TextMode="Email" Width="100%"></asp:TextBox>
                                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtContractorEmail"
    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
    Display = "Dynamic" ErrorMessage = "Invalid email address"/>
                                                  
                                                </div> <br>
                                            </td>

                                        </tr>
                                             <tr>


                                                            <td style="width: 50%">
                                                                <div>
                                                                    <label for="form-field-select-1">
                                                                        <asp:Literal ID="lblFrom" Text="<%$ Resources:LocalizedText, lblFrom%>" runat="server"></asp:Literal>
                                                                        <b style="color: red">*</b></label>
                                                                    <br>
                                                                    <asp:DropDownList ID="LstFrom" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstFrom_SelectedIndexChanged"></asp:DropDownList>

                                                                </div><br> 
                                                            </td>


                                                            <td style="width: 50%">
                                                                <div>
                                                                    <label for="form-field-select-1">
                                                                        <asp:Literal ID="lblTmTp" Text="<%$ Resources:LocalizedText, lblTmTp%>" runat="server"></asp:Literal>
                                                                        <b style="color: red">*</b></label><br />
                                                                    <asp:DropDownList ID="LstTmTp" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>


                                                                </div><br> 
                                                            </td>


                                                        </tr>

                                          <tr>
                                             

                                            <td style="width: 100%"  colspan="2">

                                                    <div><asp:Label ID="lblreasons" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblreason %>"></asp:Label><b style="color: red"> *</b>    <br>                                      
                                                       <asp:TextBox ID="txtreason" onkeypress="if (this.value.length > 100) { return false; }" runat="server"  class="width-100" Enabled="true" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                           
                                            </td>
                                        </tr>



                                    </table><br>
                                    



                                    
                                    <table style="width: 100%; margin: auto">
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <h5 class="lighter  smaller" style="color:rgb(187,135,72);font-weight:bold">
                                              
                                                    <%--The items on this request are to be allocated to :--%>
                                                    <asp:Label ID="lblDetailEscorters" runat="server" Text="<%$ Resources:LocalizedText, lblDetailEscorter %>"></asp:Label>
                                                </h5>
                                            </td>
                                            <%--<td>
                                                    </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblEscoters" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblEscoter %>"></asp:Label><b style="color: red"> *</b>
                                                    <br />
                                                   <asp:TextBox ID="txtEscoter" onkeypress="if (this.value.length > 100) { return false; }" runat="server" class="width-90" Enabled="true"></asp:TextBox>
                                                </div> <br> 
                                            </td>

                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblFunctionEscoters" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblFunctionEscoter %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtFunctionEscoter" onkeypress="if (this.value.length > 100) { return false; }" runat="server" class="width-100" Enabled="true"></asp:TextBox>
                                                </div> <br> 
                                            </td>

                                        </tr>






                                          <tr>
                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblStartDates" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblStartDate %>"></asp:Label><b style="color: red"> *</b>
                                                    <br /> <asp:TextBox ID="StartDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="90%"></asp:TextBox>
                                             
                                                </div> <br> 
                                            </td>

                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblEndDates" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblEndDate %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                     <asp:TextBox ID="EndDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                                                </div> <br> 
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
                                                    <a data-toggle="tab" href="#profile4">

                                                        <asp:Literal ID="lblItems" Text="<%$ Resources:LocalizedText, lblItems%>" runat="server"></asp:Literal>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#profile6">

                                                        <asp:Literal ID="zoneHistory" Text="<%$ Resources:LocalizedText, zoneHistory%>" runat="server"></asp:Literal>
                                                    </a>
                                                </li>

                                            </ul>
                                            <br />

                                            
                                                <!--fin onglet Employees-->

                                                <!--onglet Contractor-->
                                            <div class="tab-content">
                                             <div id="profile4" class="tab-pane in active">
                                                   <div>
                                                        <p>
                                                            <asp:Button ID="selectall" runat="server" OnClick="Selectall_Click" Text="<%$ Resources:LocalizedText, selectall%>" class="btn btn-white btn-purple btn-sm" />
                                                            <asp:Button ID="Deselectall" runat="server" OnClick="Deselectall_Click" Text="<%$ Resources:LocalizedText, Deselectall%>"  class="btn btn-white btn-inverse btn-sm" />
                                                            <asp:Button ID="Inverse" Visible="false" runat="server" OnClick="Inverse_Click" Text="<%$ Resources:LocalizedText, Inverse%>" class="btn btn-white btn-pink btn-sm" />
                                                        </p>
                                                    </div>

                                       

                                                    <%--     <asp:DropDownList ID="LstContractor" OnSelectedIndexChanged="LstContractor_SelectedIndexChanged" class="form-control" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>--%>
                                                   

                                                    <br />
                                                    <div runat="server" style="height: 400px; overflow-y: scroll;">
                                                        <asp:GridView ID="dgvItems" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="IdBonEntree" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                            <Columns>

                                                       <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:CheckBox ID="chkRow" Checked="false" AutoPostBack="false" runat="server" />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID details
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIDDetail" runat="server" Text='<%#Eval("DetailID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblIdBonEntree" Text="<%$ Resources:LocalizedText, lblIdBonEntree%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdBonEntree" runat="server" Text='<%#Eval("IdBonEntree")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblCodeProduct" Text="<%$ Resources:LocalizedText, lblCodeProduct%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodeProduct" runat="server" Text='<%#Eval("CodeProduit")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblDesignation" Text="<%$ Resources:LocalizedText, lblDesignation%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblQuantity" Text="<%$ Resources:LocalizedText, lblQuantity%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468"  Visible="false" HeaderStyle-Width="10%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblStatus" Text="<%$ Resources:LocalizedText, lblQuantity%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("StatusID")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                              <%--  <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblFrom" Text="<%$ Resources:LocalizedText, lblFrom%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFrom" runat="server" Text='<%#Eval("FromSite")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblTmTp" Text="<%$ Resources:LocalizedText, lblTmTp%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTmTp" runat="server" Text='<%#Eval("ToSite")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>


                                                                
                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            Visibility
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVisibility" runat="server" Text='<%#Eval("Visibility")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                
                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="5%" ItemStyle-Wrap="true">
                                                                        <HeaderTemplate>
                                                                           Statut id
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                          <%--  <asp:TextBox ID="txtStatusID" MaxLength="2" runat="server" Enabled="false" Width="50px" TextMode="Number" Text='<%#Eval("StatusID")%>'></asp:TextBox>--%>

                                                                               <asp:Label ID="txtStatusID" runat="server" Text='<%#Eval("StatusId")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="20%" ItemStyle-Wrap="true">
                                                                        <HeaderTemplate>
                                                                           Status
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                          <%--  <asp:TextBox ID="txtStatusID" MaxLength="2" runat="server" Enabled="false" Width="50px" TextMode="Number" Text='<%#Eval("StatusID")%>'></asp:TextBox>--%>

                                                                               <asp:Label ID="txtStatusIDx" runat="server" Text='<%#Eval("StatusDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="40%" ItemStyle-Wrap="true">
                                                                        <HeaderTemplate>
                                                                         
                                                                              <asp:Literal ID="lbdgvCurrentUser" Text="<%$ Resources:LocalizedText, lbdgvCurrentUser%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                           <%-- <asp:TextBox ID="txtCurrentUser" Width="300px" Enabled="false"    runat="server" Text='<%#Eval("CurrentUser")%>' ></asp:TextBox>--%>
                                                                            <asp:Label ID="txtCurrentUser" runat="server" Text='<%#Eval("CurrentUser")%>'></asp:Label>
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
                                                    
                                                
                                    </div>
                                               <!--onglet Contractor-->



                                    
                                                <!--FIN ONGLET CONTRACTOR-->

                                                 <!--onglet Training programme-->
                                     <div id="profile6" class="tab-pane">
                                                                                                         <div runat="server" style="height: 400px; overflow-y: scroll;">

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

                                                                            <asp:Literal ID="lblDesignationS" Text="<%$ Resources:LocalizedText, lblDesignation%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ArticleDescription" runat="server" Text='<%#Eval("ArticleDescription")%>'></asp:Label>
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
                                                                            <asp:Label ID="txtCreateDate" runat="server" Text='<%# DateTime.Parse(Eval("CreationDate").ToString()).ToString("MMM-dd-yyyy HH:mm:ss") %>'></asp:Label>
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
                                                </div>
                                                </div>
                                                     <%--<asp:DropDownList ID="DropDownList1" OnSelectedIndexChanged="LstContractor_SelectedIndexChanged" class="form-control" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>--%>
                                                    <br/>
                                        
                                               
                                            <!--FIN ONGLET training programme-->

                                            </div>
                                            <!--fin onglet application-->


                                        </div>
                                        <!--fin bloc onglet detail-->
                                        <br />
                                     <div class="clearfix form-actions" id="blockbtn" runat="server">
                                       
                                        <div>
                                             
                                             <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click"  class="btn btn-info btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Update_Text%>"	 />
                                               
                                             <asp:Button ID="btnApproval" OnClick="btnApproval_Click" class="btn btn-primary btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Approve_Text%>"	 />
                                                 
											 <asp:Button ID="BtnReject" OnClick="BtnReject_Click" class="btn btn-danger btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Reject_Text%>"	/>
                                                   
											 <asp:Button ID="BtnReturnToOrginator" OnClick="BtnReturnToOrginator_Click" class="btn btn-success btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Return_Text%>"	 />
                                        
                                             <asp:Button ID="btnBackToOriginator"  OnClick="btnBackToOriginator_Click" class="btn btn-purple btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btnBackToPrevious%>" />

                                               <asp:Button ID="btnHold" class="btn btn-info btn-sm"  runat="server" OnClick="btnHold_Click"  Text="<%$ Resources:LocalizedText, btnHold%>" />

											 <asp:Button ID="btnrRealocate" class="btn btn-warning btn-sm" OnClick="btnrRealocate_Click" runat="server" Text="<%$ Resources:LocalizedText, btnrRealocate%>" />

                                             <asp:Button ID="btnAdHoc" class="btn btn-pink btn-sm" OnClick="btnAdHoc_Click"  runat="server" Text="Ad Hoc" />
                                          
                                             <asp:Button ID="btnPrint" class="btn btn-inverse btn-sm" OnClick="btnPrint_Click"  runat="server" Text="<%$ Resources:LocalizedText, btnPrint%>" />
                                      
                                         </div>
                                         
                                         </div>
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

    <!--popup pour comment-->


    <div id="my-modal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin"><asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, lblMaterialControl%>"></asp:Label> - <span> <asp:Label ID="lblTitre" runat="server" Text="0" Visible="true"></asp:Label></span></h3>
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


                         <div>
                                                    <label for="form-field-select-1">
                                                        
                                                        <asp:Literal ID="Literal18" Text="<%$ Resources:LocalizedText, lblQuotationFile%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="90%" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>

                    </h4>

                </div>

                <div class="modal-footer">


                    <asp:Button ID="btnSave" class="btn btn-sm btn-info" OnClick="btnSave_Click" runat="server" Text="OK" />


                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
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




    <!--popup pour secretaire-->


    <div id="my-modal-sec" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">


                <div class="modal-header" style="background-color:#006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin"><asp:Label ID="Label2" runat="server" Text="<%$ Resources:LocalizedText, lblMaterialControl%>"></asp:Label> -  <span> <asp:Label ID="lblTitreSec" runat="server" Text="0" Visible="true"></asp:Label></span></h3>

                </div>
                  
          <%--       <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                    <ContentTemplate>--%>
                <div class="modal-body">

                  

                    <h4 class="smaller lighter red no-margin">
                        <div id="categorie" runat="server">
                            <label for="form-field-select-1">
                                <asp:Literal ID="Literal5" Text="Service" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                         
                                    <asp:DropDownList ID="lstServiceSec" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                          
                        </div>
                    </h4>

                    <br />
                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1"> 
                                <asp:Literal ID="Literal208" Text="<%$ Resources:LocalizedText, lblBudgetedCheck%>" runat="server"></asp:Literal>?:
                                <b style="color: red">*</b></label><br/>
                      <asp:DropDownList ID="LstBudgetSec"  class="form-control" Width="80%" runat="server" AutoPostBack="false">

                                                         <asp:ListItem Text="<%$ Resources:LocalizedText, selectBudgetdNo%>"></asp:ListItem>
                                                         <asp:ListItem Text="<%$ Resources:LocalizedText, selectBudgetdYes%>"></asp:ListItem>
                                                     
                            </asp:DropDownList>
                        </div>
                    </h4>
                    <br />
                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtCommentSec" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </h4>


                     <div>
                                                    <label for="form-field-select-1">
                                                        
                                                        <asp:Literal ID="Literal19" Text="<%$ Resources:LocalizedText, lblQuotationFile%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload3" runat="server" Width="90%" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>


                </div>
     
                <div class="modal-footer">


                    <asp:Button ID="btnSaveSec" class="btn btn-sm btn-info"  OnClick="btnSaveSec_Click" runat="server" Text="OK" />


                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        <asp:Literal ID="Literal4" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                    </button>
                </div>

                                                        
            </div>

<%--     </ContentTemplate>
                                                </asp:UpdatePanel>--%>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!--fin popup-->


    <!--popup pour Training Programme-->


    <div id="my-modal-Plan" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">

                       <asp:Label ID="Label9" runat="server" Text="<%$ Resources:LocalizedText, lblBonEntree%>"></asp:Label> - <asp:Literal ID="lblTrainingProgramme" Text="<%$ Resources:LocalizedText, lblTrainingProgramme%>" runat="server"></asp:Literal>
                    </h3>

                </div>

                <div class="modal-body">


                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="lblStartDate" Text="<%$ Resources:LocalizedText, lblStartDate%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtStartDateComment" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </h4>

                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="Literal6" Text="<%$ Resources:LocalizedText, lblEndDate%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtEndDateComment" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </h4>

                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="Literal11" Text="<%$ Resources:LocalizedText, lblTrainingLocation%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="LstTrainingLocation" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </h4>

                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="Literal12" Text="<%$ Resources:LocalizedText, lblCourseDescription%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="LstCourseDescription" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </h4>

                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="Literal13" Text="<%$ Resources:LocalizedText, lblTrainingVenue%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtTrainingVenue" TextMode="SingleLine" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </h4>
                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="Literal14" Text="<%$ Resources:LocalizedText, lblAllDayEvent%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <table>
                                <tr>
                                <td style="width:10%;margin-right:2%">   <asp:Literal ID="Literal16" Text="<%$ Resources:LocalizedText, lblfrom%>" runat="server"></asp:Literal>  </td>
                                <td style="width:35%">
                           <div class="input-group bootstrap-timepicker" >
															<input id="timepicker1" runat="server" type="text" class="form-control" />
															<span class="input-group-addon">
																<i class="fa fa-clock-o bigger-110"></i>
															</span>
														</div>
                            </td>
                                     <td style="width:10%;">&nbsp; &nbsp;   <asp:Literal ID="Literal17" Text="<%$ Resources:LocalizedText, lblTmTp%>" runat="server"></asp:Literal> </td>
                            <td style="width:35%">
                              <div class="input-group bootstrap-timepicker">
															<input id="timepicker2" runat="server" type="text" class="form-control" />
															<span class="input-group-addon">
																<i class="fa fa-clock-o bigger-110"></i>
															</span>
														</div>
                        </td>
                    </tr>
                    </table>
                        </div>
                    </h4>

                    <h4 class="smaller lighter red no-margin">
                       
                    </h4>
                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="Literal7" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtCommentPlanner" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </h4>


                     <div>
                                                    <label for="form-field-select-1">
                                                        
                                                        <asp:Literal ID="Literal20" Text="<%$ Resources:LocalizedText, lblQuotationFile%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload4" runat="server" Width="90%" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>


                </div>

                <div class="modal-footer">


<%--                    <asp:Button ID="btnSavePlan" OnClick="btnSavePlan_Click" class="btn btn-sm btn-info" runat="server" Text="OK" />--%>


                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        <asp:Literal ID="Literal8" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
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





                     <%--   </ContentTemplate>
                    </asp:UpdatePanel>--%>
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


      <!--popup pour reallocate-->


    <div id="my-reallocate" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin"><asp:Label ID="Label6" runat="server" Text="<%$ Resources:LocalizedText, lblMaterialControl%>"></asp:Label> - <asp:Label ID="Label7" runat="server" Text="<%$ Resources:LocalizedText, btnrRealocate%>"></asp:Label></h3>
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
                   

                    <asp:Button ID="BtnSaveReallocate" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" OnClick="BtnSaveReallocate_Click"  class="btn btn-sm btn-info" runat="server" Text="OK" />
                            

                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                      <asp:Literal ID="Literal30" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
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
                    <h3 class="smaller lighter white no-margin"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:LocalizedText, lblMaterialControl%>"></asp:Label> -   Ad-Hoc</h3>
                    <asp:Label ID="Label5" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">
                  

                    <h4 class="smaller lighter red no-margin">

                         <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                        
                        <div>
                            <label for="form-field-select-1">
                                 <asp:Literal ID="Literal24" Text="<%$ Resources:LocalizedText, lblUsers%>" runat="server"></asp:Literal>
                                
                                <b style="color: red">*</b></label><br />
                            <asp:DropDownList ID="dllUserAdHoc" class="chosen-select  form-control"   Width="100%" runat="server" ></asp:DropDownList>
                        </div>


                      

                        <div>
                            <label for="form-field-select-1">
                                 <asp:Literal ID="Literal25" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                
                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtCommentAdHoc" TextMode="MultiLine"  runat="server" Width="100%"></asp:TextBox>
                        </div>

                                   </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>
 
                </div>

                <div class="modal-footer">
                   

                    <asp:Button ID="btnAdHocSav" OnClientClick="this.disabled=true;" UseSubmitBehavior="false"  OnClick="btnAdHocSav_Click"  class="btn btn-sm btn-info" runat="server" Text="OK" />
                            

                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                       <asp:Literal ID="Literal31" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
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
        PopupControlID="pnlPopup" TargetControlID="lnkDummy"  BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
        <div class="modal-content">
            <div class="modal-header" style="background-color:#006666;">
                <h3 class="smaller lighter white no-margin">CONFIRMATION
                </h3>
            </div>
            <div class="modal-body">
                <h4 class="smaller lighter green no-margin">
                    <asp:Label ID="lblActionSuccess" runat="server" Text="<%$ Resources:LocalizedText, lblActionSuccess%>"></asp:Label>
                   
                </h4>

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

             <%-- document.getElementById("<%=btnSave.ClientID %>")--%>

            $('#<%=timepicker1.ClientID %>').timepicker({
                minuteStep: 1,
                showSeconds: false,
                showMeridian: false,
                disableFocus: true,
                icons: {
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down'
                }
            }).on('focus', function () {
                 $('#<%=timepicker1.ClientID %>').timepicker('showWidget');
            }).next().on(ace.click_event, function () {
                $(this).prev().focus();
            });


             $('#<%=timepicker2.ClientID %>').timepicker({
                minuteStep: 1,
                showSeconds: false,
                showMeridian: false,
                disableFocus: true,
                icons: {
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down'
                }
            }).on('focus', function () {
                 $('#<%=timepicker2.ClientID %>').timepicker('showWidget');
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
        function openModal_sec() {
            $('#my-modal-sec').modal('show');
        }
        function hideModal_sec() {
            $('#my-modal-sec').modal('hide');
        }
        function openModal_Plan() {
            $('#my-modal-Plan').modal('show');
        }
        function hideModal_Plan() {
            $('#my-modal-Plan').modal('hide');
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
            var top = Math.max($(window).height() / 1.9 - loading[0].offsetHeight / 2, 0);
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
    <asp:Literal ID="Literal29" Text="<%$ Resources:LocalizedText, lblLoading%>" runat="server"></asp:Literal>
            </h4>
    <img src="bx_loader.gif" alt="" />
</div>
</asp:Content>
























