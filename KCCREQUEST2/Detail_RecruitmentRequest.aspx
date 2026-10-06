<%@ Page Title="" Language="C#" MasterPageFile="~/ViewRequest.Master" AutoEventWireup="true" CodeBehind="Detail_RecruitmentRequest.aspx.cs" Inherits="KCCREQUEST2.Detail_RecruitmentRequest" %>

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




    <script type="text/javascript">
        function DisableButton() {

            if (document.getElementById("<%=btnSave.ClientID %>")) {

                 document.getElementById("<%=btnSave.ClientID %>").disabled = true;
             }

             if (document.getElementById("<%=btnSaveHrSystem.ClientID %>")) {

                 document.getElementById("<%=btnSaveHrSystem.ClientID %>").disabled = true;
             }
             
           
            
             
              document.getElementById("<%=btnOk.ClientID %>").disabled = true;
         }
         window.onbeforeunload = DisableButton;
    </script>


    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
                <%--   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1>
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, lblRecruitmentRequest%>"></asp:Label>
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
                            <asp:Label ID="lbltitleDetailRecruitment" runat="server" Text="<%$ Resources:LocalizedText, lbltitleDetailRecruitment%>"></asp:Label>
                            <span class="badge badge" style="background-color:#003468" id="title" runat="server"></span>
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
                                            <td style="width: 30%;">
                                                <h5 class="lighter block" style="color:rgb(187,135,72);font-weight:bold">
                                                    <asp:Literal ID="lblEmployeeRequisition" Text="<%$ Resources:LocalizedText, lblEmployeeRequisition%>" runat="server"></asp:Literal>

                                                </h5>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%;">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblDepartment" Text="<%$ Resources:LocalizedText, lblDepartment%>" runat="server"></asp:Literal>

                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstDepartment" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 30%;">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblRequestor" Text="<%$ Resources:LocalizedText, lblRequestor%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestor" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 30%;">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblRequestMethode" Text="<%$ Resources:LocalizedText, lblRequestMethode%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestMethode" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 30%;">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblCategory" Text="<%$ Resources:LocalizedText, lblCategory%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstCategory" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 30%;">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblPriority" Text="<%$ Resources:LocalizedText, lblPriority%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstPriority" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 30%;">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblPositionLevel" Text="<%$ Resources:LocalizedText, lbltypeEmployee%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstEmployeeType" OnSelectedIndexChanged="LstEmployeeType_SelectedIndexChanged" class="form-control" Width="100%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblTypeofContract" Text="<%$ Resources:LocalizedText, lblTypeofContract%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstTypeOfContrat" OnSelectedIndexChanged="LstTypeOfContrat_SelectedIndexChanged" class="form-control" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblDuration" Text="<%$ Resources:LocalizedText, lblDuration%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstDuration" class="form-control" Enabled="false" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblCostCenter" Text="<%$ Resources:LocalizedText, lblCostCenter%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstCostCenter" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>

                                                <div>

                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal208" Text="<%$ Resources:LocalizedText, lblBudgetedCheck%>" runat="server"></asp:Literal>?:
                                                                   <b style="color: red">*</b>
                                                    </label>
                                                    <%--  <asp:CheckBox ID="cbDetailBudged" OnCheckedChanged="cbDetailBudged_CheckedChanged" Enabled="false"  AutoPostBack="true" runat="server" class="ace" /><span>  </span>--%>
                                                    <br>
                                                    <asp:DropDownList ID="LstBudged" OnSelectedIndexChanged="LstBudged_SelectedIndexChanged" Enabled="false" class="form-control" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>

                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal19" Text="<%$ Resources:LocalizedText, lbbudgedYear%>" runat="server"></asp:Literal>
                                                        <%--  <b style="color: red">*</b>--%>
                                                    </label>
                                                    <br>
                                                    <asp:TextBox ID="txtDetailBudgedYear" TextMode="Number" Enabled="false" MaxLength="4" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>


                                        </tr>

                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblPlannedStartDate" Text="<%$ Resources:LocalizedText, lblPlannedStartDate%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtPlannedStartDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblJobTitle" Text="<%$ Resources:LocalizedText, lblJobTitle%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtJobTitle" MaxLength="30" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblNumberPosition" Text="<%$ Resources:LocalizedText, lblNumberPosition%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtNumberOfPosition" MaxLength="3" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblJobGrade" Text="<%$ Resources:LocalizedText, lblJobGrade%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstJobGrade" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>


                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal7" Text="<%$ Resources:LocalizedText, lblReportTo%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstReportTo" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                            <td>
                                                        <div>
                                                            <label for="form-field-select-1">

                                                                <asp:Literal ID="Literal21" Text="<%$ Resources:LocalizedText, lblOPS%>" runat="server"></asp:Literal>
                                                            <br>
                                                            <asp:TextBox ID="txtIsOPS" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>
                                                        </div>
                                                    </td>
                                        </tr>



                                        <tr style="display: none">
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal8" Text="<%$ Resources:LocalizedText, lblEmploymentEntity%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstEmploymentEntity" class="chosen-select  form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>


                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal11" Text="<%$ Resources:LocalizedText, lblCampLocation%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstCampLocation" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>

                                                <div>
                                                    <asp:CheckBox ID="CbIsReplacement" OnCheckedChanged="CbIsReplacement_CheckedChanged" AutoPostBack="true" runat="server" class="ace" /><span>  </span>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblIsReplacement" Text="<%$ Resources:LocalizedText, lblIsReplacement%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <br>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>


                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="txtEmployeeNumber" Text="<%$ Resources:LocalizedText, txtEmployeeNumber%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtIsReplacement" MaxLength="30" runat="server" Width="90%"></asp:TextBox>

                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblReplacementEmployee" Text="<%$ Resources:LocalizedText, lblReplacementEmployee%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtReplacementEmployeeName" MaxLength="30" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblReplacementPostNumber" Text="<%$ Resources:LocalizedText, lblReplacementPostNumber%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtReplacementPostNumber" MaxLength="30" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td colspan="3">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblReplacementReason" Text="<%$ Resources:LocalizedText, lblReplacementReason%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtReplacementReason" onkeypress="if (this.value.length > 250) { return false; }" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblReplacementJobTitle" Text="<%$ Resources:LocalizedText, lblReplacementJobTitle%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtReplacementJobTitle" TextMode="SingleLine" MaxLength="30" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>






                                        </tr>





                                        <tr>
                                            <td colspan="3">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblMainResponsibility" Text="<%$ Resources:LocalizedText, lblMainResponsibility%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />

                                                    <asp:TextBox ID="txtPrincipalTask" onkeypress="if (this.value.length > 250) { return false; }" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="3">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblMinimumQualification" Text="<%$ Resources:LocalizedText, lblMinimumQualification%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtMinimumQualification" onkeypress="if (this.value.length > 250) { return false; }" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="3">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblotherRequirement" Text="<%$ Resources:LocalizedText, lblotherRequirement%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtOhterRequirements" onkeypress="if (this.value.length > 250) { return false; }" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>



                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal9" Text="<%$ Resources:LocalizedText, lbdgvCurrentUser%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtCurrentUser" ReadOnly="true" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal10" Text="<%$ Resources:LocalizedText, lblStatut%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtStatut" ReadOnly="true" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div id="updateFile" runat="server">


                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="Literal6" Text="<%$ Resources:LocalizedText, lblFile%>" runat="server"></asp:Literal>

                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload3" runat="server" class="multi" />
                                                        <br>
                                                    </div>



                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="width: 100%;">
                                                <br />

                                                <div class="col-sm-12">
                                                    <div class="tabbable tabs-below">
                                                        <div class="tab-content">
                                                            <div id="home2" class="tab-pane in active">
                                                                <asp:GridView ID="dgvFiles" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="FileID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="15%">
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
                                                                               <%-- <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                                    <Triggers>
                                                                                    </Triggers>
                                                                                    <ContentTemplate>--%>
                                                                                        <asp:Button CssClass="btn  btn-xs btn-danger" OnClick="btnview_Click" ID="btnview" runat="server" Text="<%$ Resources:LocalizedText, lblView%>" />
                                                                           <%--         </ContentTemplate>
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
                                                    <a data-toggle="tab" href="#profile4">
                                                        <asp:Literal ID="zoneHistory" Text="<%$ Resources:LocalizedText, zoneHistory%>" runat="server"></asp:Literal>
                                                    </a>
                                                </li>
                                            </ul>


                                            <div class="tab-content">
                                                <!--onglet history-->
                                                <div id="profile4" class="tab-pane in active">

                                                    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
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


                                            <!-- button pour l'enregistrement et l'initialisation des données-->
                                            <div class="clearfix form-actions" id="blockbtn" runat="server">
                                                <div>


                                                    <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click1" class="btn btn-info btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Update_Text%>" />

                                                    <asp:Button ID="btnApproval" OnClick="btnApproval_Click" class="btn btn-primary btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btnApproval%>" />

                                                    <asp:Button ID="BtnReject" OnClick="BtnReject_Click" class="btn btn-danger btn-sm" runat="server" Text="<%$ Resources:LocalizedText, BtnReject%>" />

                                                    <asp:Button ID="BtnReturnToOrginator" OnClick="BtnReturnToOrginator_Click" class="btn btn-success btn-sm" runat="server" Text="<%$ Resources:LocalizedText, BtnReturnToOrginator%>" />

                                                    <asp:Button ID="btnBackToOriginator" OnClick="btnBackToOriginator_Click" class="btn btn-purple btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btnBackToPrevious%>" />


                                                    <asp:Button ID="btnrRealocate" class="btn btn-warning btn-sm" OnClick="btnrRealocate_Click" runat="server" Text="<%$ Resources:LocalizedText, btnrRealocate%>" />
                                                    <asp:Button ID="btnAdHoc" class="btn btn-pink btn-sm" OnClick="btnAdHoc_Click" runat="server" Text="Ad Hoc" />
                                                    <asp:Button ID="btnPrint" class="btn btn-inverse btn-sm" OnClick="btnPrint_Click" runat="server" Text="<%$ Resources:LocalizedText, btnPrint%>" />

                                                    <asp:Button ID="btnPrintExpatriate" class="btn btn-inverse btn-sm" OnClick="btnPrintExpatriate_Click" runat="server" Text="Print Expatriate Form" />

                                                    <asp:Button ID="btnClose" Visible="false" class="btn btn-danger btn-sm" OnClick="btnClose_Click" runat="server" Text="<%$ Resources:LocalizedText, btnCloseRequisition%>" />

                                                    <asp:Button ID="btnCallBack" class="btn btn-danger btn-sm" OnClick="btnCallBack_Click" runat="server" Text="<%$ Resources:LocalizedText, btnCallBack%>" />
                                                </div>

                                            </div>
                                            <br />
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
                        <div class="modal-header" style="background-color: #006666;">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h3 class="smaller lighter white no-margin"><asp:Label ID="Label8" runat="server" Text="<%$ Resources:LocalizedText, lblRecruitmentRequest%>"></asp:Label>  <span>
                                <asp:Label ID="lblTitre" runat="server" Text="0" Visible="true"></asp:Label></span></h3>
                            <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>

                            <asp:AsyncPostBackTrigger ControlID="btnApproval" />
                            <asp:AsyncPostBackTrigger ControlID="BtnReject" />

                            <asp:AsyncPostBackTrigger ControlID="BtnReturnToOrginator" />
                        </Triggers>
                        <ContentTemplate>--%>
                            <asp:Label ID="lblaction" runat="server" Text="" Visible="false"></asp:Label>

                            <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                        </div>

                        <div class="modal-body">


                            <h4 class="smaller lighter red no-margin">
                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                </div>

                            </h4>




                            <div id="attachement" runat="server">

                                <h4 class="smaller lighter red no-margin">
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="lblFile" Text="<%$ Resources:LocalizedText, lblFile%>" runat="server"></asp:Literal>

                                    </label>
                                    <div class="timeline-body">
                                        <asp:FileUpload ID="FileUpload2" runat="server" class="multi" />
                                        <br>
                                    </div>
                                </h4>


                            </div>




                        </div>

                        <div class="modal-footer">


                            <asp:Button ID="btnSave" OnClick="btnSave_Click" class="btn btn-sm btn-info" runat="server" Text="OK" />


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

        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--fin popup-->



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




    <!--popup pour hr-system -->


    <div id="my-modal-hrsystem" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">


                <div class="modal-header" style="background-color: #006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin"><asp:Label ID="Label6" runat="server" Text="<%$ Resources:LocalizedText, lblRecruitmentRequest%>"></asp:Label>  Hr System</h3>

                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">

                        <ContentTemplate>

                            <h4 class="smaller lighter red no-margin">
                                <div>
                                    <label for="form-field-select-1">
                                        Budget:

                                <b style="color: red">*</b></label>
                                    <br />
                                    <asp:DropDownList ID="LstBudgetHr" OnSelectedIndexChanged="LstBudgetHr_SelectedIndexChanged" class="form-control" Width="80%" runat="server" AutoPostBack="true">

                                        <asp:ListItem Text="<%$ Resources:LocalizedText, selectBudgetdNo%>"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:LocalizedText, selectBudgetdYes%>"></asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                            </h4>
                            <br />

                            <h4 class="smaller lighter red no-margin">
                                <div id="blocudgetYear" runat="server">
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="Literal17" Text="<%$ Resources:LocalizedText, lbbudgedYear%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:TextBox ID="txtBudgetYear" TextMode="Number" Enabled="true" runat="server" Width="40%"></asp:TextBox>
                                </div>
                            </h4>
                            <br />

                            <h4 class="smaller lighter red no-margin">
                                <div id="blocops" runat="server">
                                    <label for="form-field-select-1">
                                        Ops:

                                <b style="color: red">*</b></label>
                                    <asp:CheckBox ID="CbOps" runat="server" />
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


                            <h4 class="smaller lighter red no-margin">
                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="Literal5" Text="<%$ Resources:LocalizedText, lblFile%>" runat="server"></asp:Literal>

                                    </label>
                                    <div class="timeline-body">
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="multi" />
                                        <br>
                                    </div>
                                </div>

                            </h4>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">


                    <asp:Button ID="btnSaveHrSystem" OnClick="btnSaveHrSystem_Click" class="btn btn-sm btn-info" runat="server" Text="OK" />


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

    <!--fin popup-->


    <!--popup pour reallocate-->


    <div id="my-reallocate" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin"><asp:Label ID="Label3" runat="server" Text="<%$ Resources:LocalizedText, lblRecruitmentRequest%>"></asp:Label> -  <asp:Label ID="Label7" runat="server" Text="<%$ Resources:LocalizedText, btnrRealocate%>"></asp:Label></h3>
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
                <div class="modal-header" style="background-color: #006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin"> <asp:Label ID="Label2" runat="server" Text="<%$ Resources:LocalizedText, lblRecruitmentRequest%>"></asp:Label> -  Ad-Hoc</h3>
                    <asp:Label ID="Label5" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">


                    <h4 class="smaller lighter red no-margin">

                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
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
                        <asp:Literal ID="Literal20" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
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
            <div class="modal-header" style="background-color: #006666;">
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
        function openModal_hrsystem() {
            $('#my-modal-hrsystem').modal('show');
        }
        function hideModal_hrsystem() {
            $('#my-modal-hrsystem').modal('hide');
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
        $('form').live("submit",function () {
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
