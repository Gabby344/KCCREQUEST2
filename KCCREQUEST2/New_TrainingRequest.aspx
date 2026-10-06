<%@ Page Title="" Language="C#" MasterPageFile="~/NewRequest.Master" AutoEventWireup="true" CodeBehind="New_TrainingRequest.aspx.cs" Inherits="KCCREQUEST2.New_TrainingRequest" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
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
       background-color:#e42526;
        }
    </style>
         <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
     <script type="text/javascript">
         function DisableButton() {

             if (document.getElementById("<%=btnSave.ClientID %>")) {

                 document.getElementById("<%=btnSave.ClientID %>").disabled = true;
             }

             document.getElementById("<%=btnOk.ClientID %>").disabled = true;
             document.getElementById("<%=btnReset.ClientID %>").disabled = true;
                document.getElementById("btnClose").disabled = true;
          }
          window.onbeforeunload = DisableButton;
    </script>

    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
               <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1>TRAINING REQUEST
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
                            
                             <asp:Label ID="lblNewtrainingRequest" runat="server" Text="<%$ Resources:LocalizedText, lblNewtrainingRequest%>"></asp:Label>
                        </h4>
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
                                                <h5 class="lighter block" style="color:rgb(187,135,72);font-weight:bold">
                                                    <asp:Label ID="lblMsgHeadIT" runat="server" Text="<%$ Resources:LocalizedText, lblMsgHeadIT %>"></asp:Label>

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
                                                    <asp:DropDownList ID="LstDepartment" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstDepartment_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div >
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblRequestor" Text="<%$ Resources:LocalizedText, lblRequestor%>" runat="server"></asp:Literal> 
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestor" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="2">
                                        
    </td>
                                        </tr>

                                                    <tr>
                                            <td style="width: 50%">
                                             <div>
                                                    <asp:Label ID="lblBeneficiaryJobTitle" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblBeneficiaryJobTitle %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtBeneficiaryJobTitle" MaxLength="80" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                 <div>
                                                    <label for="form-field-select-1">
                                                         <asp:Literal ID="lblCourse" Text="<%$ Resources:LocalizedText, lblCourse%>" runat="server"></asp:Literal> 
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtCourse" MaxLength="40" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>



                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblTrainingType" Text="<%$ Resources:LocalizedText, lblTrainingType%>" runat="server"></asp:Literal> 
                                                        <b style="color: red">*</b></label><br />
                                                   <asp:DropDownList ID="LstTrainingType" class="chosen-select form-control" Width="90%" Enabled="false" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                               <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal13" Text="<%$ Resources:LocalizedText, lblCategory%>" runat="server"></asp:Literal> 
                                                        <b style="color: red">*</b></label><br />
                                                   <asp:DropDownList ID="LstTrainingCategory" class="chosen-select form-control" Width="100%" Enabled="true" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblRequestDescription" Text="<%$ Resources:LocalizedText, lblRequestDescription%>" runat="server"></asp:Literal>    
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtRequestDescription"  onkeypress="if (this.value.length >250) { return false; }" TextMode="MultiLine"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <asp:Panel runat="server" ID="pnlExternalTrng" Visible="false">
                                            <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblCoursesDates" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblCoursesDates %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtCourseDates" MaxLength="50" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblDurationOfStay" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblDurationOfStay %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtDurationOfStay" MaxLength="50"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                            <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblDepartureDate" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblDepartureDate %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtDepartureDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" MaxLength="50" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblReturnDate" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblReturnDate %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtReturnDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" MaxLength="50"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="2">
                                         <div>
                                                    <asp:Label ID="lblCourseVenue" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblCourseVenue %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtCourseVenue" MaxLength="80" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                        </td>
                                        </tr> 
                                            <tr>
                                            <td colspan="2">
                                         <div>
                                                    <asp:Label ID="lblCourseFees" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblCourseFees %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtCourseFees" MaxLength="80" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                        </td>
                                        </tr>
                                            <tr>
                                            <td colspan="2">
                                         <div>
                                                    <asp:Label ID="lblVisaRequirement" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblVisaRequirement %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtVisaRequirement" MaxLength="80" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                        </td>
                                        </tr> 
                                            <tr>
                                            <td colspan="2">
                                         <div>
                                                    <asp:Label ID="lblTransport" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblTransport %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtTransport" MaxLength="80" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                        </td>
                                        </tr>  
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal10" Text="<%$ Resources:LocalizedText, lblTrainingPurpose%>" runat="server"></asp:Literal>    
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtTrainingPurpose"  onkeypress="if (this.value.length >250) { return false; }" TextMode="MultiLine"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                            <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal11" Text="<%$ Resources:LocalizedText, lblEntrepriseBenefit%>" runat="server"></asp:Literal>    
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtEnterpriseBenefit"  onkeypress="if (this.value.length >250) { return false; }" TextMode="MultiLine"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                            <tr>
                                            <td colspan="2">
                                         <div>
                                                    <asp:Label ID="lblCertification" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblCertification %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtCertification" MaxLength="80" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                        </td>
                                        </tr> 
                                            <tr>
                                            <td colspan="2">
                                         <div>
                                                    <asp:Label ID="lblLegalRequirement" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblLegalRequirement %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtLegalRequirement" MaxLength="80" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                        </td>
                                        </tr> 
                                            <tr>
                                            <td colspan="2">
                                         <div>
                                                    <asp:Label ID="lblIndividualDevPlan" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblIndividualDevPlan %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtIndividualPlan" MaxLength="80" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                        </td>
                                        </tr>
                                            <tr>
                                            <td >
                                         <div> 
                                                           
                                                            <label for="form-field-select-1">
                                                              
                                                                  <asp:Literal ID="Literal28" Text="<%$ Resources:LocalizedText, lblBudgetedCheck%>" runat="server"></asp:Literal>?:
                                                                   <b style="color: red">*</b>
                                                            </label>
                                                           <%--  <asp:CheckBox ID="cbDetailBudged" OnCheckedChanged="cbDetailBudged_CheckedChanged" Enabled="false"  AutoPostBack="true" runat="server" class="ace" /><span>  </span>--%>
                                                            <br>
                                                            <asp:DropDownList ID="LstBudged"  class="form-control" Width="50%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                        </div>
                                        </td>
                                        </tr> 
                                        </asp:Panel>
                                        <tr style="display:none">
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                       <asp:Literal ID="lblJustificationWhy" Text="<%$ Resources:LocalizedText, lblJustificationWhy%>" runat="server"></asp:Literal>   
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtJustificationWhy"  onkeypress="if (this.value.length > 250) { return false; }" TextMode="MultiLine" Text="Okay" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblPriority" Text="<%$ Resources:LocalizedText, lblPriority%>" runat="server"></asp:Literal>      
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstPriority" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                       
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblPlannedStartDate" Text="<%$ Resources:LocalizedText, lblPlannedStartDate%>" runat="server"></asp:Literal>  
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtPlannedStartDate"  class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 50%">
                                                <h5 class="lighter block" style="color:rgb(187,135,72);font-weight:bold">
                                                   
                                                     <asp:Literal ID="lblCategoryImpact" Text="<%$ Resources:LocalizedText, lblCategoryImpact%>" runat="server"></asp:Literal>  
                                                </h5>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td  style="width: 50%; padding: 0px;" valign="top">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="CbProduction" AutoPostBack="true" OnCheckedChanged="CbProduction_CheckedChanged" runat="server" class="ace" /><span>   Production</span>
                                                        <span>
                                                            <asp:TextBox ID="txtMotivationProduction" MaxLength="40" runat="server" Width="90%"></asp:TextBox>
                                                        </span>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>

                                            <td style="width: 50%; padding: 0px;" valign="top" rowspan="3">
                                              <%--  <br />--%>
                                                <div>
                                                    <label for="form-field-select-1">
                                                        
                                                        <asp:Literal ID="lblQuotationFile" Text="<%$ Resources:LocalizedText, lblQuotationFile%>" runat="server"></asp:Literal>
                                                        <asp:Panel runat="server" visible="false" id="lblexternalTrngQuotatMand"><b style="color: red">*</b></asp:Panel>
                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" Width="90%" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td  style="width: 50%; padding: 0px;" valign="top">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="CbEnvironment" AutoPostBack="true" runat="server" OnCheckedChanged="CbEnvironment_CheckedChanged" class="ace" /><span> 
                                                             
                                                              <asp:Literal ID="lblEnvironment" Text="<%$ Resources:LocalizedText, lblEnvironment%>" runat="server"></asp:Literal>  
                                                                                                                                                                                          </span>
                                                        <span>
                                                            <asp:TextBox ID="txtMotivationEnvironment" MaxLength="40" runat="server" Width="90%"></asp:TextBox>
                                                        </span>
                                                        </td>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                        </tr>

                                        <tr>
                                            <td  style="width: 50%; padding: 0px;" valign="top">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="CbSafety" AutoPostBack="true" OnCheckedChanged="CbSafety_CheckedChanged" runat="server" class="ace" /><span>   
                                                          <asp:Literal ID="lblSafety" Text="<%$ Resources:LocalizedText, lblSafety%>" runat="server"></asp:Literal>  

                                                                                                                                                                                </span>
                                                        <span>
                                                            <asp:TextBox ID="txtMotivationSafety" MaxLength="40" runat="server" Width="90%"></asp:TextBox>
                                                        </span>
                                                        </td>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                        </tr>

                                       <%-- <tr>
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
                                        </tr>--%>
                                    </table>

                                    <!--fin entete requesition-->
                                    <hr />

                                    <!--bloc detail de la requisition-->

                                    <div class="form-horizontal" id="Form2" style="width: 100%;">
                                        <h5 class="lighter block" style="color:rgb(187,135,72);font-weight:bold">
                                           
                                            <asp:Literal ID="lblmsgtraining" Text="<%$ Resources:LocalizedText, lblmsgtraining%>" runat="server"></asp:Literal>
                                        </h5>

                                        <!--bloc des onglets des detail-->
                                        <div class="tabbable">
                                            <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab4">
                                                <li class="active">
                                                    <a data-toggle="tab" href="#home4">
                                                     
                                                         <asp:Literal ID="lblKCCEmployee" Text="<%$ Resources:LocalizedText, lblKCCEmployee%>" runat="server"></asp:Literal>
                                                    </a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#profile4">
                                                       
                                                        <asp:Literal ID="lblContractor" Text="<%$ Resources:LocalizedText, lblContractor%>" runat="server"></asp:Literal>
                                                    </a>
                                                </li>

                                             <li>
                                                <a data-toggle="tab" href="#profile5">
                                                           <asp:Literal ID="Literal26" Text="<%$ Resources:LocalizedText, lblTrainingProgramme%>" runat="server"></asp:Literal> 
                                                        
                                                    </a>
                                                </li>


                                            </ul>


                                            <div class="tab-content">
                                                <!--onglet computer-->
                                                <div id="home4" class="tab-pane in active">
                                                     <div runat="server" style="height: 400px; overflow-y: scroll;">
                                                    <asp:GridView ID="dgvKCCEmployees" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="EmployeeId " AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                <HeaderTemplate>
                                                                   EmployeeID
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmployeeID" runat="server" Text='<%#Eval("EmployeeId")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate></HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkRow" Checked="false" AutoPostBack="false" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                               
                                                 <asp:Literal ID="divUserAccountEmployee" Text="<%$ Resources:LocalizedText, divUserAccountEmployee%>" runat="server"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("FirstName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                         

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                                   
                                                <asp:Literal ID="lblFunction" Text="<%$ Resources:LocalizedText, lblFunction%>" runat="server"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFunction" runat="server" Text='<%#Eval("EmergencyContactName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <SelectedRowStyle CssClass="alert-info" />
                                                        <RowStyle CssClass="cursor-pointer" />
                                                        <HeaderStyle CssClass="alert-success" />
                                                        <PagerStyle HorizontalAlign="Center" />
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                    </asp:GridView>






                                                         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
            GridLines="Horizontal" BackColor="White" BorderColor="#336666" BorderStyle="Double"
            BorderWidth="3px">
            <Columns>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" BackColor="BurlyWood" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate>
                        <asp:CheckBoxList ID="CheckBoxList1" RepeatLayout="Flow" RepeatDirection="Horizontal"
                            runat="server">
                            <asp:ListItem Text="One" Value="1">
                            </asp:ListItem>
                            <asp:ListItem Text="Two" Value="2">
                            </asp:ListItem>
                            <asp:ListItem Text="Three" Value="3">
                            </asp:ListItem>
                            <asp:ListItem Text="Four" Value="4">
                            </asp:ListItem>
                        </asp:CheckBoxList>
                        <br />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>





                                                </div>
                                                </div>
                                                <!--fin onglet Employees-->

                                                <!--onglet Contractor-->
                                                <div id="profile4" class="tab-pane">
                                                      <asp:Panel ID="estimation2" runat="server">
                                                     <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>

                                                <%--     <asp:DropDownList ID="LstContractor" OnSelectedIndexChanged="LstContractor_SelectedIndexChanged" class="form-control" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>--%>
                                                 <table style="width: 100%;">
                                                     <!-- Ligne 1 : Company (gauche) | Employee number (droite, auto-rempli) -->
                                                     <tr>
                                                        <td style="width:50%">
                                                            <div>
                                                                <label for="form-field-select-1">
                                                                    <asp:Literal ID="Literal7" Text="<%$ Resources:LocalizedText, lblContractorCompanyName%>" runat="server"></asp:Literal>
                                                                    <b style="color: red">*</b></label>
                                                                <br>
                                                                <asp:DropDownList ID="ddlContractorCompany" class="form-control" Width="90%" runat="server"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlContractorCompany_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                        <td style="width:50%">
                                                            <div>
                                                                <label for="form-field-select-1">
                                                                    <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblContractorEmployeeNumber%>" runat="server"></asp:Literal>
                                                                    <b style="color: red">*</b></label>
                                                                <br>
                                                                <asp:TextBox ID="txtEmployeeNumber" class="form-control" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                     </tr>
                                                     <!-- Ligne 2 : liste des agents de l'entreprise choisie -->
                                                     <tr>
                                                        <td colspan="2">
                                                            <div>
                                                                <label for="form-field-select-1">
                                                                    <asp:Literal ID="Literal4" Text="<%$ Resources:LocalizedText, lblContractor%>" runat="server"></asp:Literal>
                                                                    <b style="color: red">*</b></label>
                                                                <br>
                                                                <asp:DropDownList ID="ddlContractorEmployee" class="form-control" Width="95%" runat="server"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlContractorEmployee_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                     </tr>
                                                     <!-- Ligne 3 : First Name | Last Name (auto-remplis) -->
                                                     <tr>
                                                        <td style="width:50%">
                                                            <div>
                                                                <label for="form-field-select-1">
                                                                    <asp:Literal ID="LiteralContractorFirstName" Text="<%$ Resources:LocalizedText, lblContractorNames%>" runat="server"></asp:Literal>
                                                                    <b style="color: red">*</b></label>
                                                                <br>
                                                                <asp:TextBox ID="txtContractorNames" class="form-control" runat="server" Width="90%" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <td style="width:50%">
                                                            <div>
                                                                <label for="form-field-select-1">
                                                                    <asp:Literal ID="Literal9" Text="<%$ Resources:LocalizedText, lblContractorSurNames%>" runat="server"></asp:Literal>
                                                                    <b style="color: red">*</b></label>
                                                                <br>
                                                                <asp:TextBox ID="txtSurname" class="form-control" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                     </tr>
                                                     <!-- Ligne 4 : Job Title | Nature of employee -->
                                                     <tr>
                                                        <td style="width:50%">
                                                            <div>
                                                                <label for="form-field-select-1">
                                                                    <asp:Literal ID="Literal5" Text="<%$ Resources:LocalizedText, lblContractorJobTitle%>" runat="server"></asp:Literal>
                                                                    <b style="color: red">*</b></label>
                                                                <br>
                                                                <asp:TextBox ID="txtContractorJobTitle" class="form-control" runat="server" Width="90%"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <td style="width:50%">
                                                            <div>
                                                                <label for="form-field-select-1">
                                                                    <asp:Literal ID="Literal6" Text="<%$ Resources:LocalizedText, lblContractorNatureOfEmployee%>" runat="server"></asp:Literal>
                                                                    <b style="color: red">*</b></label>
                                                                <br>
                                                                <asp:DropDownList ID="dllContratorNatureOfEmployee" class="form-control" Width="100%" runat="server">
                                                                    <asp:ListItem Value="Contractor">Contractor</asp:ListItem>
                                                                    <asp:ListItem Value="Visitor">Visitor</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                     </tr>
                                                     <!-- Ligne 5 : Language -->
                                                     <tr>
                                                        <td style="width:50%">
                                                            <div>
                                                                <label for="form-field-select-1">
                                                                    <asp:Literal ID="Literal8" Text="<%$ Resources:LocalizedText, lblContractorLanguage%>" runat="server"></asp:Literal>
                                                                    <b style="color: red">*</b></label>
                                                                <br>
                                                                <asp:DropDownList ID="dlltxtContractorLanguage" class="form-control" Width="90%" runat="server">
                                                                    <asp:ListItem Value="English" Text="<%$ Resources:LocalizedText, LAnglais%>"></asp:ListItem>
                                                                    <asp:ListItem Value="French" Text="<%$ Resources:LocalizedText, LFrancais%>"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                        <td></td>
                                                     </tr>
                                                     <tr>
                                                         <td>
                                                             <br/>
                                                             <asp:Button ID="btnAddContractor" OnClick="btnAddContractor_Click" cssclass="btn2 btn2-btnSave btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnAddEstimation%>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                        
                                        
                                        
                                        <br/>
                                          <div runat="server" style="height: 400px; overflow-y: scroll;">
                                                    <asp:GridView ID="dgvContractor" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="EmployeeNumber" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                        <Columns>
                                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                <HeaderTemplate>
                                                             <asp:Literal ID="divContractorEmployeeNumber" Text="<%$ Resources:LocalizedText, lblContractorEmployeeNumber%>" runat="server"></asp:Literal>
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmployeeNumber" runat="server" Text='<%#Eval("EmployeeNumber")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                                <asp:Literal ID="divUserContractorNames" Text="<%$ Resources:LocalizedText, lblContractorNames%>" runat="server"></asp:Literal>
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNames" runat="server" Text='<%#Eval("Names")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                          
                                                               <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                                <asp:Literal ID="divUserContractorNames" Text="<%$ Resources:LocalizedText, lblContractorSurNames%>" runat="server"></asp:Literal>
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSurname" runat="server" Text='<%#Eval("Surname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                              <asp:Literal ID="lcontractorJobTitle" Text="<%$ Resources:LocalizedText, lblContractorJobTitle%>" runat="server"></asp:Literal>
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFunction" runat="server" Text='<%#Eval("JobTitle")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                              <asp:Literal ID="lcontractorNatureOfEmployee" Text="<%$ Resources:LocalizedText, lblContractorNatureOfEmployee%>" runat="server"></asp:Literal>
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNatureOfEmployee" runat="server" Text='<%#Eval("NatureOfEmployee")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                              <asp:Literal ID="lcontractorCompanyName" Text="<%$ Resources:LocalizedText, lblContractorCompanyName%>" runat="server"></asp:Literal>
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompanyNameContractor" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                              <asp:Literal ID="lcontractorLanguage" Text="<%$ Resources:LocalizedText, lblContractorLanguage%>" runat="server"></asp:Literal>
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLanguage" runat="server" Text='<%#Eval("Language")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                              <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="7%" Visible="true">
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
                                              </div>
                                                 </ContentTemplate>
                                </asp:UpdatePanel>
                                                           </asp:Panel>
                                                </div>
                                                <!--FIN ONGLET CONTRACTOR-->




                                                
                                                <!--onglet Training Detail-->
                                                <div id="profile5" class="tab-pane">
                                                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>

                                                     <%--<asp:DropDownList ID="DropDownList1" OnSelectedIndexChanged="LstContractor_SelectedIndexChanged" class="form-control" Width="200px" runat="server" AutoPostBack="true"></asp:DropDownList>--%>
                                                    <br/>
                                          <div runat="server" style="height: 400px; overflow-y: scroll;">
                                                   <asp:GridView ID="dgvTraniningCalendar" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="No " AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                <HeaderTemplate>
                                                                   No
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmployeeID" runat="server" Text='<%#Eval("No")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                         

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                               
                                                 <asp:Literal ID="divUserAccountEmployee" Text="Description" runat="server"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                <HeaderTemplate>
                                                                   
                                                <asp:Literal ID="lblFunction" Text="Value" runat="server"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFunction" runat="server" Text='<%#Eval("Value")%>'></asp:Label>
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
                                                 </ContentTemplate>
                                </asp:UpdatePanel>
                                                </div>
                                                <!--FIN ONGLET CONTRACTOR-->



                                            </div>
                                            <!--fin onglet application-->


                                        </div>
                                        <!--fin bloc onglet detail-->
                                        <br />
                                    </div>

                                    <!-- button pour l'enregistrement et l'initialisation des données-->
                                    <div class="clearfix form-actions">
                                        <div>
                                            <asp:Button ID="btnSave" cssclass="btn2 btn2-btnSave btn2-cons" OnClick="btnSave_Click" runat="server" Text="<%$ Resources:LocalizedText, btnSave%>" />
                                        
											 <asp:Button ID="btnReset" cssclass="btn2 btn2-btnReset btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnReset%>" />

                                         
                                            
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
                    <h3 class="smaller lighter white no-margin"><asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, popUpInvalidInput%>" runat="server"></asp:Literal></h3>
                </div>

                <div class="modal-body">

                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <%--  <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />--%>
                             <asp:AsyncPostBackTrigger ControlID="btnAddContractor" />
                        </Triggers>
                        <ContentTemplate>
                            <h4 class="smaller lighter red no-margin">
                                <asp:Label ID="lblErreur" runat="server" Text=""></asp:Label>
                            </h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <button class="btn btn-sm btn-danger pull-right" id="btnClose" data-dismiss="modal">
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
        PopupControlID="pnlPopup" TargetControlID="lnkDummy"   BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server"  CssClass="modal-dialog" Style="display: none">
          <div class="modal-content">
        <div class="modal-header" style="background-color:#006666;">
            <h3 class="smaller lighter white no-margin">
                     CONFIRMATION
                    </h3>
        </div>
        <div class="modal-body">
           <h4 class="smaller lighter green no-margin">
            <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
                  <asp:Label ID="lblIDRequest" runat="server" Visible="false" Text=""></asp:Label>
                            </h4>
     
        </div>
               <div class="modal-footer">
                   
                      <asp:Button ID="btnOk" class="btn btn-success" OnClick="btnOk_Click"  runat="server" Text="OK"  />

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

                //Enlever espace sur matricule contractants
                /*$('$txtContractorEmployeeNumber').keyup(function () {
                    str = $(this).val()
                    str = str.replace(/\s/g, '')
                    $(this).val(str)
                });*/



                //Fin espace sur matricule contractants


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
         else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_btnAddContractor") {
             return true;
         }
         else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_dgvContractor_ctl03_btndelete") {
             return true;
         }
         else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_dgvContractor_ctl04_btndelete") {
             return true;
         }
         else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_dgvContractor_ctl02_btndelete") {
             return true;
         }
         else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_ddlContractorCompany") {
             return true;
         }
         else if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_ddlContractorEmployee") {
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
