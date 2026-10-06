<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="KCCREQUEST2._default1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
    .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
      
    </style>

     <meta charset="utf-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="assets/css/index.css">
      <%--  <link rel="stylesheet" href="assets/css/materialize.css">--%>
        <script src="assets/js/jquery-2.1.4.min.js"></script>
       <%-- <script src="assets/js/materialize.js"></script>--%>
        <script src="assets/js/ofi.js"></script>
        
   <div class="main-content">
        <div class="main-content-inner" >
   <div class="page-content" id="homepage" style="position:absolute; width:100%; height:600px; overflow:hidden;">
    
       <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>

      
                <div class="row">

                    <!--MESSAGE DE BIENVENUE-->
                    <center>
                   <h3 class="widget-title lighter"">Welcome to KCC Request for Service</h3>
                   </center>
                    <br/>
                    <!--FIN MESSAGE BIENVENUE-->
                </div>
      <div class="row">
        <section>
            <div class="containeur" id="testConteneur">
                <div class="photo">
                    <img src="assets/images/gallery/m.jpg" alt=""  class="preview">
                </div>
                <div class="titre" style="background-color:#045FB4; padding-top: 10%;">
                    <span>IT REQUEST</span>
                </div>
                <div class="infobouton">
                    <div class="actionCenter">
                    <asp:Button class="actions" OnClick="CreateITRequest_Click"  ID="CreateITRequest" runat="server" Text="New |" />
                    <asp:Button class="actions"  ID="btnRequestsIT" OnClick="btnRequestsIT_Click" runat="server" Text="My Requests |" />
                    <asp:Button class="actions"  ID="btnApprovalIT" OnClick="btnApprovalIT_Click"  runat="server" Text=" To Approve (99)" />
                    </div>
                </div>
            </div>

            <div class=" containeur">
                <div class="photo">
                    <img src="assets/images/gallery/dsk.jpg" alt=""  class="preview">
                </div>
                <div class="titre" style="background-color: #2D8B3F; padding-top: 10%;">
                    <span>RADIO REQUEST</span>
                </div>
                  <div class="infobouton">
                      <div class="actionCenter">
                    <asp:Button class="actions"  ID="btnNewDSK" OnClick="btnNewDSK_Click" runat="server" Text="New |" />
                    <asp:Button class="actions"  ID="btnRequestDSK" OnClick="btnRequestDSK_Click" runat="server" Text="My Requests |" />
                    <asp:Button class="actions"  ID="btnApprovalDSK" OnClick="btnApprovalDSK_Click" runat="server" Text=" To Approve (99)" />
                          </div>
                </div>
            </div>

            
            <div class=" containeur">
                <div class="photo">
                    <img src="assets/images/gallery/trainning.jpg" alt=""  class="preview">
                </div>
                <div class="titre" style="background-color: #ff9800;">
                    <span>TRAINNING REQUEST</span>
                </div>
                <div class="infobouton">
                     <div class="actionCenter">
                    <asp:Button class="actions"  ID="btnNewTrainning" OnClick="btnNewTrainning_Click" runat="server" Text="New |" />
                    <asp:Button class="actions"  ID="btnRequestTrainning" OnClick="btnRequestTrainning_Click"  runat="server" Text="My Requests |" />
                    <asp:Button class="actions"  ID="btnApprovalTraining"  runat="server" OnClick="btnApprovalTraining_Click" Text=" To Approve (99)" />
                </div>
                </div>
            </div>
            <br/><br/>
            <div class=" containeur">
                <div class="photo">
                    <img src="assets/images/gallery/e1.jpg" alt=""  class="preview">
                </div>
                <div class="titre" style="background-color: #F44336; padding-top: 10%;">
                    <span>EWO REQUEST</span>
                </div>
                <div class="infobouton">
             <div class="actionCenter">
                    <asp:Button class="actions"  ID="btnNewEWO" OnClick="btnNewEWO_Click"  runat="server" Text="New |" />
                    <asp:Button class="actions"  ID="btnRequestEWO" OnClick="btnRequestEWO_Click"   runat="server" Text="My Requests |" />
                    <asp:Button class="actions"  ID="btnApprovalEWO" OnClick="btnApprovalEWO_Click" runat="server"  Text=" To Approve (99)" />
                </div>
                </div>
            </div>

            <div class=" containeur">
                <div class="photo">
                    <img src="assets/images/gallery/recruitment-strategies-of-indian-employers.jpg" alt=""  class="preview">
                </div>
                <div class="titre" style="background-color: #2c445a; padding-top: 10%;">
                    <span>RECRUITMENT REQUEST</span>
                </div>
                <div class="infobouton">
             <div class="actionCenter">
                    <asp:Button class="actions"  ID="btnNewRecrutment" OnClick="btnNewRecrutment_Click"  runat="server" Text="New |" />
                    <asp:Button class="actions"  ID="btnRequestRecrutment" OnClick="btnRequestRecrutment_Click"   runat="server" Text="My Requests |" />
                    <asp:Button class="actions"  ID="btnApprovalRecrutment" OnClick="btnApprovalRecrutment_Click" runat="server"  Text=" To Approve (99)" />
                 </div>
                </div>
            </div>
            <div class=" containeur">
                <div class="photo">
                    <img src="assets/images/gallery/m.jpg" alt=""  class="preview">
                </div>
                <div class="titre" style="background-color: #4a148c; padding-top: 10%;">
                    <span>IT PROBLEM</span>
                </div>
                <div class="infobouton">
             <div class="actionCenter">
                 <asp:Button class="actions"  ID="btnNewITProblem" OnClick="btnNewITProblem_Click"  runat="server" Text="New |" />
                    <asp:Button class="actions"  ID="btnITProblem"  OnClick="btnITProblem_Click"  runat="server" Text="My Requests |" />
                    <asp:Button class="actions"  ID="btnITProblemApproval"  OnClick="btnITProblemApproval_Click" runat="server"  Text=" To Approve (99)" />
                </div>
                </div
                >
            </div>
        </section>
        </DIV>
      </div>
  </DIV>
      </div>

    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
     <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
          <div class="modal-content">
        <div class="modal-header">
            <h3 class="smaller lighter red no-margin">
                      <asp:Literal ID="lbltitrehome" Text="<%$ Resources:LocalizedText, lbltitreAccessDenie%>" runat="server"></asp:Literal>
                    </h3>
        </div>
        <div class="modal-body">
           <h4 class="smaller lighter black no-margin">
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
              
                            </h4>
     
        </div>
               <div class="modal-footer">
                   
                    

                </div>
            </div>
    </asp:Panel>

      <script>
          $(function () {
              objectFitImages();
              //alert($(".main-content").width() + "=======" + $(".page-content").width());

              //$(".page-content").css("width", $(".main-content").width() + "px");
              $(".conteneur").resize(function (event) {
                  alert($(".main-content").width());
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

    </script>
</asp:Content>
