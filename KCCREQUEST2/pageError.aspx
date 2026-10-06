<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="pageError.aspx.cs" Inherits="KCCREQUEST2.pageError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="error-container">
									<div class="well">
										<h1 class="grey lighter smaller">
											<span class="blue bigger-125">
												<i class="ace-icon fa fa-random"></i>
												
											</span>
											<asp:Label ID="lblTitleError" runat="server" Text="<%$ Resources:LocalizedText, lblTitleError%>"></asp:Label>
										</h1>

										<hr />
										<h3 class="lighter smaller">
                                            <asp:Label ID="lblDescriptionError" runat="server" Text="<%$ Resources:LocalizedText, lblDescriptionError%>"></asp:Label>
										</h3>

										<div class="space"></div>

										

										<hr />
										<div class="space"></div>

										<div class="center">
											<a href="javascript:history.back()" class="btn btn-grey">
												<i class="ace-icon fa fa-arrow-left"></i>
												<asp:Literal ID="btnGoBack" Text="<%$ Resources:LocalizedText, btnGoBack%>" runat="server"></asp:Literal>
											</a>

											<a href="default.aspx" class="btn btn-primary">
												<i class="ace-icon fa fa-home"></i>
											<asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, btnHome%>" runat="server"></asp:Literal>
											</a>
										</div>
									</div>
								</div>



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
        function GoSysaidLink(url) {
            window.open(url, "_blank");

        }
    </script>



<%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript">
        $(function () {
            $("[src*=minus]").each(function () {
                $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                $(this).next().remove()
            });
        });
    </script>

</asp:Content>
