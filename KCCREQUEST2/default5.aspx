<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="default5.aspx.cs" Inherits="KCCREQUEST2._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content" id="homepage" >
        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">

                    <!--MESSAGE DE BIENVENUE-->
                    <center>
                   <h3 class="widget-title lighter"">Welcome to KCC Request for Service</h3>
                   </center>
                    <!--FIN MESSAGE BIENVENUE-->
                </div>
                 <div class="row">
                    <div id="d" class="col-xs-6 col-sm-4 col-md-3"></div>
                    <!--TUILE IT REQUEST-->
                    <div id="divIt" class="col-xs-6 col-sm-4 col-md-2" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                        <div class="divppe">
                            <img class="media-object" onmouseover="this.src='assets/images/gallery/imgItRequest2.jpg'" onmouseout="this.src='assets/images/gallery/ordinateur.jpg'" data-src="holder.js/100px200?theme=gray" src="assets/images/gallery/ordinateur.jpg" style="width: 100%; height: 50%" />
                            <div class="caption">
                                <h3 class="search-title" style="text-align: center">
                              
                            <b><span style="color:cornflowerblue;">IT Request</span></b>
                                </h3>
                                <hr />

                                <p style="margin-top:-10%">
                                    <!--buton d'action-->
                                    <asp:Button class="btn btn-success btn-sm tooltip-success" Style="width: 25%" OnClick="CreateITRequest_Click" data-rel="tooltip" data-placement="right" title="Create a new IT Request" ID="CreateITRequest" runat="server" Text="New" />
                                    <span class="btn btn-info btn-sm tooltip-info" style="width: 25%" data-rel="tooltip" data-placement="bottom" title="View my all IT Request">View</span>
                                    <span class="btn btn-danger btn-sm tooltip-info" data-rel="tooltip" style="width: 45%" data-placement="bottom" title="View all IT Request to Approve">To Approve(0)</span>
                                    <!-- fin button d'action-->
                               </p>
                            </div>
                        </div>
                    </div>
                    <!--FIN TUILE IT REQUEST-->

                    <!--TUILE TRAINING REQUEST-->
                 <div class="col-xs-6 col-sm-4 col-md-2" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                        <div class="divppe">
                            <img class="media-object" onmouseover="this.src='assets/images/gallery/imgTraining3.png'" onmouseout="this.src='assets/images/gallery/trainingcomputers.jpg'" data-src="holder.js/100px200?theme=gray" src="assets/images/gallery/trainingcomputers.jpg" style="width: 100%; height: 50%" />
                            <div class="caption">
                                <h3 class="search-title" style="text-align: center">
                                       <b><span style="color:green;">Training Request</span></b>
                                </h3>
                                <hr />
                               <p style="margin-top:-10%">
                                    <!--button d'action-->
                                    <asp:Button class="btn btn-success btn-sm tooltip-success" Style="width: 25%" OnClick="btnCreateTrainingRst_Click" data-rel="tooltip" data-placement="right" title="Create a new Training Request" ID="btnCreateTrainingRst" runat="server" Text="New" />
                                    <span class="btn btn-info btn-sm tooltip-info" style="width: 25%" data-rel="tooltip" data-placement="bottom" title="View my all Training Request">View</span>
                                    <span class="btn btn-danger btn-sm tooltip-info" data-rel="tooltip" style="width: 45%" data-placement="bottom" title="View all Training Request to Approve">To Approve(6)</span>
                                    <!--fin button d'action-->
                                </p>

                            </div>
                        </div>
                    </div>
                    <!--FIN TUILE TRAINING REQUEST-->

                    <!--TUILE DSK REQUEST-->
                    <div class="col-xs-6 col-sm-4 col-md-2" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                        <div class="divppe">
                            <img class="media-object" data-src="holder.js/100px200?theme=gray" onmouseover="this.src='assets/images/gallery/imgRadio.jpg'" onmouseout="this.src='assets/images/gallery/maritime-radio.jpg'" src="assets/images/gallery/maritime-radio.jpg" style="width: 100%; height: 50%" />
                            <div class="caption">
                                <h3 class="search-title" style="text-align: center">
                                    <b><span style="color:darkred;">Radio Request</span></b>
                                </h3>
                                <hr />
                                <p style="margin-top:-10%">
                                    <!--BUTTON D'ACTION-->
                                    <asp:Button class="btn btn-success btn-sm tooltip-success" Style="width: 25%" OnClick="btnNewDskRequest_Click" data-rel="tooltip" data-placement="right" title="Create a new Radio Request" ID="btnNewDskRequest" runat="server" Text="New" />
                                    <span class="btn btn-info btn-sm tooltip-info" style="width: 25%" data-rel="tooltip" data-placement="bottom" title="View my all Radio Request">View</span>
                                    <span class="btn btn-danger btn-sm tooltip-info" data-rel="tooltip" style="width: 45%" data-placement="bottom" title="View all Radio Request to Approve">To Approve(1)</span>
                                    <!--FIN BUTTON D'ACTION-->
                                </p>

                            </div>
                        </div>
                    </div>
                    <!--FIN TUILE DSK REQUEST-->

                    <div id="Divs" class="col-xs-6 col-sm-4 col-md-3"></div>
                </div>

                <br />
                <div class="row">
                    <div id="Div1" class="col-xs-6 col-sm-4 col-md-3"></div>
                    <!--TUILE EWO REQUEST-->
                    <div class="col-xs-6 col-sm-4 col-md-2" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                        <div class="divppe">
                            <img class="media-object" onmouseover="this.src='assets/images/gallery/imgEWO1.jpg'" onmouseout="this.src='assets/images/gallery/Mech_Engg1.jpg'" data-src="holder.js/100px200?theme=social" src="assets/images/gallery/Mech_Engg1.jpg" style="width: 100%; height: 50%" />
                            <div class="caption">
                                <h3 class="search-title" style="text-align: center;">
                                    <b><span style="color:darkgray;">EWO Request</span></b>
                                </h3>
                                <hr />
                                     <p style="margin-top:-10%">
                                    <!--BUTTON D'ACTION-->
                                    <asp:Button class="btn btn-success btn-sm tooltip-success" Style="width: 25%" OnClick="btnCreateEWORequest_Click" data-rel="tooltip" data-placement="right" title="Create a new EWO Request" ID="btnCreateEWORequest" runat="server" Text="New" />
                                    <span class="btn btn-info btn-sm tooltip-info" style="width: 25%" data-rel="tooltip" data-placement="bottom" title="View my all Electronic Request">View</span>
                                    <span class="btn btn-danger btn-sm tooltip-info" data-rel="tooltip" style="width: 45%" data-placement="bottom" title="View all Electronic Request to Approve">To Approve(2)</span>
                                    <!--FIN BUTTON D'ACTION-->
                                </p>
                            </div>
                        </div>
                    </div>
                    <!--FIN TUILE EWO REQUEST-->

                    <!--TUILE HR APPROVAL-->
                     <div class="col-xs-6 col-sm-4 col-md-2" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                        <div class="divppe">
                            <img class="media-object" onmouseover="this.src='assets/images/gallery/imgHrApproval1.png'" onmouseout="this.src='assets/images/gallery/imgRecrutment.jpg'" data-src="holder.js/100px200?theme=social" src="assets/images/gallery/imgRecrutment.jpg" style="width: 100%; height: 50%" />
                            <div class="caption">
                                <h3 class="search-title" style="text-align: center;margin-left:-9%">
                                    <b><span style="color:indianred;">Recrutment Request</span></b>
                                </h3>
                                <hr />
                                <p style="margin-top:-10%">
                                    <!--BUTTON D'ACTION-->
                                      <asp:Button class="btn btn-success btn-sm tooltip-success" Style="width: 25%" data-rel="tooltip" OnClick="btnCreateNewHrApproval_Click" data-placement="right" title="Create a new Recrutment Request" ID="btnCreateNewHrApproval" runat="server" Text="New" />
                                    <span class="btn btn-info btn-sm tooltip-info" style="width: 25%" data-rel="tooltip" data-placement="bottom" title="View my all Recrutment Request">View</span>
                                    <span class="btn btn-danger btn-sm tooltip-info" data-rel="tooltip" style="width: 45%" data-placement="bottom" title="View all Recrutment Request to Request">To Approve(5)</span>
                                <!--FIN BUTTON D'ACTION-->
                                </p>
                            </div>
                        </div>
                    </div>
                    <!--FIN TUILE HR APPROVAL-->


                    <!--TUILE PPE REQUEST-->
                    <div class="col-xs-6 col-sm-4 col-md-2" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                        <div class="divppe">
                            <img class="media-object" onmouseover="this.src='assets/images/gallery/imgppe1.jpg'" onmouseout="this.src='assets/images/gallery/PPE_Full_Set.jpg'" data-src="holder.js/100px200?theme=social" src="assets/images/gallery/PPE_Full_Set.jpg" style="width: 100%; height: 50%" />
                            
                                
                                <h3 class="search-title" style="text-align: center;">
                                    <b><span style="color:orangered;">PPE Request</span></b>
                                </h3>
                              
                                <hr />
                                <p style="margin-top:-10%">
                                    <!--BUTTON D'ACTION-->
                                    <span class="btn btn-success btn-sm tooltip-success" style="width: 25%" data-rel="tooltip" data-placement="right" title="Create a new PPE Request">New</span>
                                    <span class="btn btn-info btn-sm tooltip-info" style="width: 25%" data-rel="tooltip" data-placement="bottom" title="View my all PPE Request">View</span>
                                    <span class="btn btn-danger btn-sm tooltip-info" data-rel="tooltip" style="width: 45%" data-placement="bottom" title="View all PPE Request to approve">To Approve(10)</span>
                                    <!--FIN BUTTON D'ACTION-->
                                </p>
                            </div>
                        </div>
                    </div>
                    <!--FIN TUILE PPE REQUEST-->
                    <div id="Div2" class="col-xs-6 col-sm-4 col-md-3"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
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
</asp:Content>
