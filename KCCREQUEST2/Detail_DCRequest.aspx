<%@ Page Title="" Language="C#" MasterPageFile="~/ViewRequest.Master" AutoEventWireup="true" CodeBehind="Detail_DCRequest.aspx.cs" Inherits="KCCREQUEST2.Detail_DCRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            background-color: #003468;
        }

        .btn2-btnReset {
            color: #fff;
            border-color: #357ebd;
            background-color: rgb(0,177,147);
        }

        .form-field-select-1 {
            margin-bottom: 10px; /* add margin bottom to labels */
        }

        .horizontal-checkboxlist {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            margin-bottom: 5px; /* add margin bottom to checkbox lists */
        }

            .horizontal-checkboxlist label {
                margin-right: 10px;
            }
    </style>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;


            document.getElementById("<%=btnOk.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
                <%--        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>--%>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1>
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, lblDCRequest%>"></asp:Label>
                        <small>
                            <i class="ace-icon fa fa-angle-double-right"></i>
                            <%--    New Request--%>
                        </small>
                    </h1>
                </div>
                <!--FIN TITRE DE LA PAGE-->

                <!--bloc pour les information du requisitionneur-->
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center">
                        <h4 class="widget-title lighter">


                            <asp:Label ID="lblDetailDCRequest" runat="server" Text="<%$ Resources:LocalizedText, lblDetailDCRequest%>"></asp:Label>
                            <span class="badge badge" style="background-color: #003468" id="title" runat="server"></span>
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
                                                <h5 class="lighter  smaller" style="color: rgb(187,135,72); font-weight: bold">

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
                                                    <asp:TextBox ID="txtBeneficiaryJobTitle" MaxLength="50" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="lblRequestDescription" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblRequestDescription %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtRequestDescription" TextMode="MultiLine" runat="server" Width="100%" onkeypress="if (this.value.length > 250) { return false; }"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="lblJustificationWhy" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblJustificationWhy %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtJustificationWhy" TextMode="MultiLine" onkeypress="if (this.value.length > 250) { return false; }" Text="okay" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal28" Text="<%$ Resources:LocalizedText, lblRequestType %>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="ddlRequestType" class="chosen-select form-control" Width="90%" OnSelectedIndexChanged="ddlRequestType_SelectedIndexChanged" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>


                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="Label5" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, Documenttitle %>"></asp:Label><b style="color: red"></b>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentTitle" onkeypress="if (this.value.length > 80) { return false; }" CssClass="form-control" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal14" Text="Service" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="ddlService" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>


                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal6" Text="<%$ Resources:LocalizedText, DocumentlangueID %>" runat="server"></asp:Literal>
                                                        <b style="color: red"></b>
                                                    </label>
                                                    <br>
                                                    <asp:DropDownList ID="LstDocumentLangueID" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>


                                            </td>


                                        </tr>


                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal5" Text="<%$ Resources:LocalizedText, Documentype %>" runat="server"></asp:Literal>
                                                        <b style="color: red"></b>
                                                    </label>
                                                    <br>
                                                    <asp:DropDownList ID="LstDocumentType" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>


                                            </td>

                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="Label4" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, DocumentVersionCode %>"></asp:Label><b style="color: red"></b>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentVersionCode" onkeypress="if (this.value.length > 80) { return false; }" CssClass="form-control" runat="server" Width="100%" TextMode="SingleLine"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>

                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="Label2" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, DocumentNumber %>"></asp:Label><b style="color: red"></b>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentNumber" onkeypress="if (this.value.length > 80) { return false; }" CssClass="form-control" runat="server" Width="90%" TextMode="SingleLine"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblCategory %>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstCategory" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>


                                            </td>

                                        </tr>


                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblPriority" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblUrgency %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:DropDownList ID="LstPriority" CssClass="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>


                                            <td style="width: 50%">
                                                <asp:Panel ID="panelUrgencyComment" Visible="true" runat="server">
                                                    <asp:Label ID="lblUrgencyComment" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblUrgencyComment %>"></asp:Label><b style="color: red" id="mendatory" runat="server"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtUrgencyComment" onkeypress="if (this.value.length > 80) { return false; }" CssClass="form-control" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                </asp:Panel>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="lblCostCenter" Text="<%$ Resources:LocalizedText, lblCostCenter%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstCostCenter" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>


                                            </td>

                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="lblPlannedStartDate" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblPlannedStartDate %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtPlannedStartDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <!--start of 2nd part of the table: applicability-->
                                        <tr id="Tr2" runat="server" visible="true">
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="Label19" CssClass="form-field-select-1" runat="server" Text="Applicability Assessment"></asp:Label><b style="color: red"></b>
                                                    <br>
                                                    <asp:DropDownList ID="ddlApplicability" runat="server" AutoPostBack="false" Width="90%" class="form-control">
                                                        <asp:ListItem Text="N/A" Value="N/A" />
                                                        <asp:ListItem Text="YES" Value="YES" />
                                                        <asp:ListItem Text="NO (Type: Fin, LGL, ADM etc.)" Value="NO" />
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                        
                                            <tr id="TrApplicability1" runat="server">
                                                <td colspan="2">
                                                    <div>
                                                        <asp:Label ID="Label20" CssClass="form-field-select-1" runat="server" Text="Applicable areas"></asp:Label><b style="color: red"></b>
                                                        <br>
                                                        <asp:CheckBoxList ID="CheckBoxListApplicableAreas" Width="100%" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal" RepeatLayout="Table" TextAlign="Right" CssClass="form-control horizontal-checkboxlist" runat="server" AutoPostBack="false"></asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" id="TrApplicability2" runat="server">
                                                    <div>
                                                        <asp:Label ID="Label21" CssClass="form-field-select-1" runat="server" Text="Applicability Reviewers"></asp:Label><b style="color: red"></b>
                                                        <br>
                                                        <asp:CheckBoxList ID="CheckBoxListApprovalReviewers" RepeatDirection="Horizontal" RepeatColumns="10" RepeatLayout="Table" TextAlign="Right" CssClass="form-control horizontal-checkboxlist" runat="server" AutoPostBack="false"></asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr id="TrApplicability3" runat="server">
                                                <td colspan="2">
                                                    <div>
                                                        <asp:Label ID="Label22" CssClass="form-field-select-1" runat="server" Text="Rollout Process By"></asp:Label><b style="color: red"></b>
                                                        <br>
                                                        <asp:CheckBoxList ID="CheckBoxListRolloutChannels" CssClass="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                        

                                        <!--end of  applicability section-->

                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal4" Text="<%$ Resources:LocalizedText, lblarea%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtArea" onkeypress="if (this.value.length > 80) { return false; }" CssClass="form-control" runat="server" Width="90%" TextMode="MultiLine"></asp:TextBox>
                                                </div>


                                            </td>

                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="Label3" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblRisk%>"></asp:Label><b style="color: red">*</b>
                                                    <br>
                                                    <asp:TextBox ID="txtRisk" onkeypress="if (this.value.length > 80) { return false; }" CssClass="form-control" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>



                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal7" Text="<%$ Resources:LocalizedText, DocVesion%>" runat="server"></asp:Literal>
                                                        <b style="color: red"></b>
                                                    </label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocVersion" onkeypress="if (this.value.length > 80) { return false; }" CssClass="form-control" runat="server" Width="90%" TextMode="Number"></asp:TextBox>
                                                </div>


                                            </td>

                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="Label6" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblDocumentVersionDate %>"></asp:Label><b style="color: red"></b>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentVersionDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal8" Text="<%$ Resources:LocalizedText, DocTranslator%>" runat="server"></asp:Literal>
                                                        <b style="color: red"></b>
                                                    </label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocTranslator" onkeypress="if (this.value.length > 80) { return false; }" CssClass="form-control" runat="server" Width="90%" TextMode="SingleLine"></asp:TextBox>
                                                </div>


                                            </td>

                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="Label7" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblDocumentReviewVersionDate %>"></asp:Label><b style="color: red"></b>
                                                    <br>
                                                    <asp:TextBox ID="txtDocReviewVersionDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal9" Text="<%$ Resources:LocalizedText, DocHardCopy%>" runat="server"></asp:Literal>
                                                        <b style="color: red"></b>
                                                    </label>
                                                    <br>
                                                    <asp:DropDownList ID="ddlDocHarCopy" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false">
                                                        <asp:ListItem Value="N/A">N/A</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>


                                            </td>


                                        </tr>


                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal13" Text="<%$ Resources:LocalizedText, DocTransmittalNo%>" runat="server"></asp:Literal>
                                                        <b style="color: red"></b>
                                                    </label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocTransmittalNo" class="form-control" TextMode="Number" runat="server" Width="90%"></asp:TextBox>
                                                </div>


                                            </td>

                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="Label10" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, AdditionalNotedId %>"></asp:Label><b style="color: red"></b>
                                                    <br>
                                                    <asp:TextBox ID="txtAdditionalNotedId" class="form-control" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="Label8" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblDocLink %>"></asp:Label><b style="color: red"></b>
                                                    <br>
                                                    <asp:TextBox ID="txtDocLink" class="form-control" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal21" Text="<%$ Resources:LocalizedText, lblStatut%>" runat="server"></asp:Literal>
                                                        <b style="color: red"></b>
                                                    </label>
                                                    <br>
                                                    <asp:TextBox ID="txtStatut" class="form-control" Enabled="false" TextMode="SingleLine" runat="server" Width="90%"></asp:TextBox>
                                                </div>


                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal22" Text="<%$ Resources:LocalizedText, lbdgvCurrentUser%>" runat="server"></asp:Literal>
                                                        <b style="color: red"></b>
                                                    </label>
                                                    <br>
                                                    <asp:TextBox ID="txtCurrentUsers" class="form-control" Enabled="false" TextMode="SingleLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>


                                            </td>

                                        </tr>


                                        <tr>
                                            <%-- <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                         <asp:Literal ID="lblFile" Text="<%$ Resources:LocalizedText, lblFile%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUploadDoc" accept="application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document" runat="server" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                            </td>--%>
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
                                                                                <asp:Button CssClass="btn  btn-xs btn-danger" ID="btnview" OnClick="btnview_Click" runat="server" Text="<%$ Resources:LocalizedText, lblView%>" />
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
                                        </div>
                                    </div>
                                    <hr />

                                    <!-- button pour l'enregistrement et l'initialisation des données-->

                                    <div class="clearfix form-actions">
                                        <div>
                                            <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click" class="btn btn-info btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btn_Update_Text%>" />

                                            <asp:Button ID="btnApproval" class="btn btn-primary btn-sm" OnClick="btnApproval_Click" runat="server" Text="<%$ Resources:LocalizedText, btn_Approve_Text%>" />

                                            <asp:Button ID="BtnReject" class="btn btn-danger btn-sm" runat="server" OnClick="BtnReject_Click" Text="<%$ Resources:LocalizedText, btn_Reject_Text%>" />

                                            <asp:Button ID="BtnReturnToOrginator" class="btn btn-success btn-sm" OnClick="BtnReturnToOrginator_Click" runat="server" Text="<%$ Resources:LocalizedText, btn_Return_Text%>" />

                                            <asp:Button ID="btnHistory" OnClick="btnHistory_Click" class="btn btn-pink btn-sm" runat="server" Text="Update History" />

                                            <asp:Button ID="btnBackToOriginator" class="btn btn-purple btn-sm" OnClick="btnBackToOriginator_Click" runat="server" Text="<%$ Resources:LocalizedText, btnBackToPrevious%>" />

                                            <%--<asp:Button ID="btnrRealocate" class="btn btn-warning btn-sm" OnClick="btnrRealocate_Click" runat="server" Text="<%$ Resources:LocalizedText, btnrRealocate%>" />--%>

                                            <asp:Button ID="btnAdHoc" class="btn btn-pink btn-sm" OnClick="btnAdHoc_Click" runat="server" Text="Ad Hoc" />

                                            <asp:Button ID="btnPrint" class="btn btn-inverse btn-sm" runat="server" Text="<%$ Resources:LocalizedText, btnPrint%>" />

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



    <!--popup pour comment-->


    <div id="my-modalComment" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">
                        <asp:Label ID="Label9" runat="server" Text="<%$ Resources:LocalizedText, lblDCRequest%>"></asp:Label><span>
                            <asp:Label ID="lblTitre" runat="server" Text="0" Visible="true"></asp:Label></span></h3>
                    <asp:Label ID="lblaction" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">


                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">

                        <ContentTemplate>

                            <h4 class="smaller lighter red no-margin">
                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:TextBox ID="txtComment" runat="server" Width="100%"></asp:TextBox>
                                </div>



                                <div>
                                    <label for="form-field-select-1">

                                        <asp:Literal ID="Literal10" Text="<%$ Resources:LocalizedText, lblFile%>" runat="server"></asp:Literal>
                                    </label>
                                    <div class="timeline-body">
                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="90%" class="multi" />
                                        <br>
                                    </div>
                                </div>

                            </h4>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">


                    <asp:Button ID="btnSave" OnClick="btnSave_Click" class="btn btn-sm btn-info" runat="server" Text="OK" />


                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        <asp:Literal ID="Literal11" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!--fin popup-->

    <!--popup pour le message d'erreur-->


    <div id="my-modal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #682525;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">
                        <asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, popUpInvalidInput%>" runat="server"></asp:Literal></h3>
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








    <!--popup pour adhoc-->


    <div id="my-adhoc" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">
                        <asp:Label ID="Label11" runat="server" Text="<%$ Resources:LocalizedText, lblDCRequest%>"></asp:Label>
                        -  Ad-Hoc</h3>
                    <asp:Label ID="Label12" runat="server" Text="0" Visible="false"></asp:Label>
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







    <div id="my-reallocate" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">
                        <asp:Label ID="Label13" runat="server" Text="<%$ Resources:LocalizedText, lblDCRequest%>"></asp:Label>
                        - 
                        <asp:Label ID="Label14" runat="server" Text="<%$ Resources:LocalizedText, btnHistory%>"></asp:Label></h3>
                    <asp:Label ID="Label15" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">


                    <h4 class="smaller lighter red no-margin">

                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>

                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="Literal17" Text="<%$ Resources:LocalizedText, lblUsers%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:DropDownList ID="dllUsers" class="chosen-select  form-control" OnSelectedIndexChanged="dllUsers_SelectedIndexChanged1" Width="100%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </div>


                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="Literal18" Text="<%$ Resources:LocalizedText, lblRole%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:DropDownList ID="dllRole" class="chosen-select  form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </div>
                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="Literal23" Text="<%$ Resources:LocalizedText, lblStatut%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:DropDownList ID="ddlStatusHistory" class="chosen-select  form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </div>


                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="Literal19" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:TextBox ID="txtCommentReallocate" runat="server" Width="100%"></asp:TextBox>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="Literal27" Text="<%$ Resources:LocalizedText, lbSignDate%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtSignDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                        </div>


                        <div>
                            <label for="form-field-select-1">

                                <asp:Literal ID="Literal29" Text="<%$ Resources:LocalizedText, lblFile%>" runat="server"></asp:Literal>
                            </label>
                            <div class="timeline-body">
                                <asp:FileUpload ID="FileUpload2" runat="server" Width="90%" class="multi" />
                                <br>
                            </div>
                        </div>


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




    <div id="addHistory" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #006666;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">
                        <asp:Label ID="Label16" runat="server" Text="<%$ Resources:LocalizedText, lblDCRequest%>"></asp:Label>
                        - 
                        <asp:Label ID="Label17" runat="server" Text="<%$ Resources:LocalizedText, btnHistory%>"></asp:Label></h3>
                    <asp:Label ID="Label18" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">


                    <h4 class="smaller lighter red no-margin">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="Literal24" Text="<%$ Resources:LocalizedText, lblRole%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:DropDownList ID="ddlRoleHistory" class="chosen-select  form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                </div>



                                <div>
                                    <label for="form-field-select-1">
                                        <asp:Literal ID="Literal25" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                        <b style="color: red">*</b></label><br />
                                    <asp:TextBox ID="txtCommentHistory" runat="server" Width="100%"></asp:TextBox>
                                </div>




                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </h4>

                </div>

                <div class="modal-footer">


                    <asp:Button ID="btnSaveHistory" OnClick="btnSaveHistory_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" class="btn btn-sm btn-info" runat="server" Text="OK" />


                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        <asp:Literal ID="Literal26" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
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
                <h3 class="smaller lighter green no-margin">
                    <asp:Label ID="lblActionSuccess" runat="server" Text="<%$ Resources:LocalizedText, lblActionSuccess%>"></asp:Label>
                    <asp:Label ID="lblIDRequest" runat="server" Visible="false" Text=""></asp:Label>
                </h3>

            </div>
            <div class="modal-footer">

                <asp:Button ID="btnOk" OnClick="btnOk_Click" class="btn btn-success" runat="server" Text="OK" />

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
        function openModalComment() {
            $('#my-modalComment').modal('show');
        }
        function HideModalComment() {
            $('#my-modalComment').modal('hide');
        }
        function openAdHoc() {
            $('#my-adhoc').modal('show');
        }
        function hideAdHoc() {
            $('#my-adhoc').modal('hide');
        }
        function openReallocate() {
            $('#my-reallocate').modal('show');
        }
        function hideReallocate() {
            $('#my-reallocate').modal('hide');
        }
        function openHistory() {
            $('#addHistory').modal('show');
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
            <asp:Literal ID="Literal12" Text="<%$ Resources:LocalizedText, lblLoading%>" runat="server"></asp:Literal>
        </h4>
        <img src="bx_loader.gif" alt="" />
    </div>
</asp:Content>
