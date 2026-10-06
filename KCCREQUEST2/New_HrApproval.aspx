<%@ Page Title="" Language="C#" MasterPageFile="~/NewRequest.Master" AutoEventWireup="true" CodeBehind="New_HrApproval.aspx.cs" Inherits="KCCREQUEST2.New_HrApproval" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
        background-color:rgb(0,177,147);
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
         }
         window.onbeforeunload = DisableButton;
    </script>

    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
                <%--                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1><asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, lblRecruitmentRequest%>"></asp:Label>
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
                            <asp:Label ID="lblNewHrRequest" runat="server" Text="<%$ Resources:LocalizedText, lblNewHrRequest%>"></asp:Label>

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


                                            <td>
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
                                                    <asp:DropDownList ID="LstDepartment" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstDepartment_SelectedIndexChanged"></asp:DropDownList>
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
                                                    <asp:DropDownList ID="LstEmployeeType" class="form-control" OnSelectedIndexChanged="LstEmployeeType_SelectedIndexChanged" Width="100%" runat="server" AutoPostBack="true"></asp:DropDownList>
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
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
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

                                                                <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblReportTo%>" runat="server"></asp:Literal>
                                                                <b style="color: red">*</b></label>
                                                            <br>
                                                            <asp:DropDownList ID="LstReportTo" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                        </div>
                                                    </td>                                                    
                                                </tr>



                                                 <tr style="display:none">
                                                    <td>
                                                        <div>
                                                            <label for="form-field-select-1">

                                                                <asp:Literal ID="Literal4" Text="<%$ Resources:LocalizedText, lblEmploymentEntity%>" runat="server"></asp:Literal>
                                                                <b style="color: red">*</b></label>
                                                            <br>
                                                            <asp:DropDownList ID="LstEmploymentEntity" class="chosen-select  form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                        </div>
                                                    </td>


                                                    <td>
                                                        <div>
                                                            <label for="form-field-select-1">

                                                                <asp:Literal ID="Literal5" Text="<%$ Resources:LocalizedText, lblCampLocation%>" runat="server"></asp:Literal>
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
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>


                                                <tr>
                                                    <td colspan="3">
                                                        <div>
                                                            <label for="form-field-select-1">

                                                                <asp:Literal ID="lblMainResponsibility" Text="<%$ Resources:LocalizedText, lblMainResponsibility%>" runat="server"></asp:Literal>
                                                                <b style="color: red">*</b></label><br />

                                                            <asp:TextBox ID="txtPrincipalTask" TextMode="MultiLine" onkeypress="if (this.value.length > 250) { return false; }" runat="server" Width="100%"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtOhterRequirements" TextMode="MultiLine" onkeypress="if (this.value.length > 250) { return false; }" runat="server" Width="100%"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        <asp:Literal ID="lblFile" Text="<%$ Resources:LocalizedText, lblFile%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b>
                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>

                                    <!--fin entete requesition-->
                                    <hr />



                                    <!-- button pour l'enregistrement et l'initialisation des données-->
                                    <div class="clearfix form-actions">
                                        <div>
                                            <asp:Button ID="btnSave" Enabled="false" OnClick="btnSave_Click" cssclass="btn2 btn2-btnSave btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnSave%>" />

                                            <asp:Button ID="btnReset" OnClick="btnReset_Click" cssclass="btn2 btn2-btnReset btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnReset%>" />

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
                    <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblIDRequest" runat="server" Visible="false" Text=""></asp:Label>
                </h3>

            </div>
            <div class="modal-footer">

                <asp:Button ID="btnOk" class="btn btn-success" OnClick="btnOk_Click" runat="server" Text="OK" />

            </div>
        </div>
    </asp:Panel>


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

    <script type="text/javascript">
        function openModal() {
            $('#my-modal').modal('show');
        }

    </script>


</asp:Content>
