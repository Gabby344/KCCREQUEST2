<%@ Page Title="" Language="C#" MasterPageFile="~/NewRequest.Master" AutoEventWireup="true" CodeBehind="New_DCRequest.aspx.cs" Inherits="KCCREQUEST2.New_DCRequest" %>

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
            margin-bottom: 5px; /* margin bottom for checkbox lists */
        }

            .horizontal-checkboxlist label {
                margin-right: 10px;
                color: #333; /* Darker text color */
                margin-top: -3px;
            }

            .horizontal-checkboxlist input[type="checkbox"] {
                margin-right: 5px; /* Space between checkbox and label */
            }

            /* Hover effect on labels */
            .horizontal-checkboxlist label:hover {
                color: #03787c; /* Darker color on hover */
            }

        #applicability tr {
            border-bottom: 5px solid transparent;
        }

        .dropdown-container {
            padding-top: 8px; /* Adjust the value as needed */
        }
    </style>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;
            document.getElementById("<%=btnReset.ClientID %>").disabled = true;

            document.getElementById("<%=btnOk.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>

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


                            <asp:Label ID="lblNEWDCRequest" runat="server" Text="<%$ Resources:LocalizedText, lblNEWDCRequest%>"></asp:Label>
                        </h4>
                    </div>

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">


                                    <!-- entite de la requisition-->
                                    <table style="width: 100%; margin: auto">
                                        <%--<tbody>--%>
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

                                                            <asp:Literal ID="Literal7" Text="<%$ Resources:LocalizedText, lblRequestType %>" runat="server"></asp:Literal>
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

                                                            <asp:Literal ID="Literal4" Text="Service" runat="server"></asp:Literal>
                                                            <b style="color: red">*</b></label>
                                                        <br>
                                                        <asp:DropDownList ID="ddlService" class="chosen-select form-control" Width="100%" OnSelectedIndexChanged="ddlService_SelectedIndexChanged" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                    </div>


                                                </td>


                                            </tr>

                                            <tr>
                                                <td style="width: 50%">
                                                    <div>
                                                        <label for="form-field-select-1">

                                                            <asp:Literal ID="Literal6" Text="<%$ Resources:LocalizedText, DocumentlangueID %>" runat="server"></asp:Literal>
                                                            <b style="color: red"></b>
                                                        </label>
                                                        <br>
                                                        <asp:DropDownList ID="LstDocumentLangueID" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
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

                                                <td style="width: 50%">
                                                    <div>
                                                        <label for="form-field-select-1">

                                                            <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblCategory %>" runat="server"></asp:Literal>
                                                            <b style="color: red">*</b></label>
                                                        <br>
                                                        <asp:DropDownList ID="LstCategory" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
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
                                       <%-- </tbody>--%>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <tr id="Tr2" runat="server" visible="true">
                                                    <td style="width: 50%; margin-top: 2px">
                                                        <div>
                                                            <asp:Label ID="Label9" CssClass="form-field-select-1" runat="server" Text="Applicability Assessment"></asp:Label><b style="color: red"></b>
                                                            <br>
                                                            <asp:DropDownList ID="ddlSetApplicability" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSetApplicability_SelectedIndexChanged" Width="90%" class="form-control">
                                                                <asp:ListItem Text="N/A" Value="N/A" />
                                                                <asp:ListItem Text="YES" Value="YES" />
                                                                <asp:ListItem Text="NO (Type: Fin, LGL, ADM etc.)" Value="NO" />
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <!--end of 1rst part of the table before applicability section-->

                                                <!--start of 2nd part of the table: applicability-->
                                                <%--<tbody id="applicability_section" runat="server" class="text-dark">--%>
                                                    <tr id="TrAreas" runat="server" style="margin-top: 2px">
                                                        <td colspan="2" class="dropdown-container">
                                                            <div>
                                                                <asp:Label ID="Label6" CssClass="form-field-select-1" runat="server" Text="Applicable areas"></asp:Label><b style="color: red"></b>
                                                                <br>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" AutoPostBack="true" 
    OnCheckedChanged="chkSelectAll_CheckedChanged"/>
                                                                <asp:CheckBoxList ID="CheckBoxListApplicableAreas" Width="100%" CellPadding="5" CellSpacing="5" RepeatDirection="Horizontal" RepeatLayout="Table" TextAlign="Right" CssClass="form-control
                                                                    horizontal-checkboxlist"
                                                                    runat="server">
                                                                
                                                                </asp:CheckBoxList>
                                                                
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr id="TrReviewers" runat="server">
                                                        <td colspan="2">
                                                            <div>
                                                                <asp:Label ID="Label7" CssClass="form-field-select-1" runat="server" Text="Applicability Reviewers"></asp:Label><b style="color: red"></b>
                                                                <br>
                                                                <asp:CheckBoxList ID="CheckBoxListApprovalReviewers" RepeatDirection="Horizontal" RepeatColumns="10" RepeatLayout="Table" TextAlign="Right" CssClass="form-control horizontal-checkboxlist" runat="server" AutoPostBack="false"></asp:CheckBoxList>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                
                                            </ContentTemplate>
                                       
                                        </asp:UpdatePanel>

                                        <tr id="TrRollout" runat="server" visible="true">
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="Label10" CssClass="form-field-select-1" runat="server" Text="Rollout Process By"></asp:Label><b style="color: red"></b>
                                                    <br>
                                                    <asp:CheckBoxList ID="CheckBoxListRolloutChannels" CssClass="form-control horizontal-checkboxlist" Width="100%" runat="server" AutoPostBack="false"></asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>

                                        <!--end of applicability section-->


                                        <%--<tbody>--%>
                                            <tr>
                                                <%--   <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">

                                                        <asp:Literal ID="Literal4" Text="<%$ Resources:LocalizedText, lblarea%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                 <asp:TextBox ID="TextBox3" onkeypress="if (this.value.length > 80) { return false; }"   Cssclass="form-control" runat="server" Width="90%" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                               
                                               
                                            </td>--%>

                                                <td style="width: 50%; padding: 0px;" valign="top" colspan="2">
                                                    <div>
                                                        <asp:Label ID="Label3" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblRisk%>"></asp:Label><b style="color: red"> *</b>
                                                        <br>
                                                        <asp:TextBox ID="txtRisk" onkeypress="if (this.value.length > 80) { return false; }" CssClass="form-control" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>

                                            <tr id="doclink" runat="server" visible="false">
                                                <td colspan="2">
                                                    <div>
                                                        <asp:Label ID="Label8" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblDocLink %>"></asp:Label><b style="color: red"></b>
                                                        <br>
                                                        <asp:TextBox ID="txtDocLink" class="form-control" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div>


                                                        <label for="form-field-select-1" style="font-size: 20px">
                                                            <asp:Literal ID="lblFile" Text="<%$ Resources:LocalizedText, lblFile%>" runat="server"></asp:Literal>
                                                        </label>

                                                        <asp:Label ID="FileMessageLbl" runat="server" ForeColor="Red" Font-Size="20px"> </asp:Label>
                                                        <div class="timeline-body">
                                                            <asp:FileUpload ID="FileUploadDoc" accept="" runat="server" class="multi" />
                                                            <br>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        <%--</tbody>--%>
                                    </table>




                                    <!--fin entete requesition-->
                                    <hr />


                                    <!-- button pour l'enregistrement et l'initialisation des données-->

                                    <div class="clearfix form-actions">
                                        <div>
                                            <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn2 btn2-btnSave btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnSave%>" />

                                            <asp:Button ID="btnReset" CssClass="btn2 btn2-btnReset btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnReset%>" />

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
                    <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
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
    <%--<script type="text/javascript">
        jQuery(function ($) {
            if (!ace.vars['touch']) {
                $('.chosen-select').chosen({ allow_single_deselect: true });



            }
        });
    </script>--%>

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
            ShowProgress();
        });



    </script>

    

    <div class="loading" align="center" id="panelLoading" runat="server">
        <h4 class="widget-title red">
            <asp:Literal ID="Literal12" Text="<%$ Resources:LocalizedText, lblLoading%>" runat="server"></asp:Literal>
        </h4>
        <img src="bx_loader.gif" alt="" />
    </div>
</asp:Content>
