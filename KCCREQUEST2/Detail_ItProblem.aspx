<%@ Page Title="" Language="C#" MasterPageFile="~/ViewRequest.Master" AutoEventWireup="true" CodeBehind="Detail_ItProblem.aspx.cs" Inherits="KCCREQUEST2.Detail_ItProblem" %>
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
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
        <%--        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>--%>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1><asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, lblITProblem%>"></asp:Label>
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
                            
                          
                          <asp:Label ID="lblDetailitproblemRequest" runat="server" Text="<%$ Resources:LocalizedText, lblDetailitproblemRequest%>"></asp:Label>

                              <span class="badge badge-info" id="title" runat="server"></span>
                        </h4>
                    </div>

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">


                                    <!-- entete de la requisition-->
                                    <table style="width: 100%; margin: auto">
                                        
                                   
                                    <tr>
                                            <td colspan="2" style="width: 100%">
                                                <div>
                                                    <br/>
                                                    <label for="form-field-select-1">
                                                      <asp:Literal ID="lblTitle" Text="<%$ Resources:LocalizedText, lblTitle%>" runat="server"></asp:Literal>    
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtSubject" MaxLength="70" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                            
                                        </tr>

                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        Description 
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtDescription" MaxLength="70" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%; display:none">
                                                <div>
                                                    <label for="form-field-select-1">
                                                       <asp:Literal ID="lblRemarques" Text="<%$ Resources:LocalizedText, lblRemarques%>" runat="server"></asp:Literal>   
                                                  <%--      <b style="color: red">*</b>--%>

                                                    </label><br />
                                                    <asp:TextBox ID="txtNotes" Text="Okay" TextMode="SingleLine" MaxLength="70" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                          
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                         <asp:Literal ID="lblWhoIsAffected" Text="<%$ Resources:LocalizedText, lblWhoIsAffected%>" runat="server"></asp:Literal>   
                                                         <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstAffected" class="form-control" Width="  90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                       
                                           
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                       <asp:Literal ID="lblDoyouHaveaworkaround" Text="<%$ Resources:LocalizedText, lblDoyouHaveaworkaround%>" runat="server"></asp:Literal>   
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstWorkAround" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                               </tr>
                                        <tr>
                                                 <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                           <asp:Literal ID="lblRequestor" Text="<%$ Resources:LocalizedText, lblRequestor%>" runat="server"></asp:Literal> 
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestedBy" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                     


                                        
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                           <asp:Literal ID="lblContactNumber" Text="<%$ Resources:LocalizedText, lblContactNumber%>" runat="server"></asp:Literal> 
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtContactNumber" MaxLength="40" TextMode="Phone" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>

                                               
                                        </tr>

                                      

                                           <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                           <asp:Literal ID="lblEmplacement" Text="<%$ Resources:LocalizedText, lblEmplacement%>" runat="server"></asp:Literal>  
                                                         <b style="color: red">*</b></label>
                                                    <br>
                                                     <asp:DropDownList ID="LstLocation" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                                 <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                      <asp:Literal ID="lblDepartment" Text="<%$ Resources:LocalizedText, lblDepartment%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                     <asp:DropDownList ID="LstDepartment" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                          <tr>
                                            <td colspan="2" style="width:100%;">
                                                <br/>
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

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="80%" ItemStyle-Width="80%">
                                                                        <HeaderTemplate>
                                                                         <asp:Literal ID="lbdgvFile" Text="<%$ Resources:LocalizedText, lbdgvFile%>" runat="server"></asp:Literal>
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtFileName" runat="server" Text='<%#Eval("FileName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                        <HeaderTemplate>
                                                                          Type
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  
                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <HeaderTemplate>
                                                                           File
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="File" runat="server" Text='<%#Eval("File")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                        <HeaderTemplate></HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:Button CssClass="btn  btn-xs btn-danger"  ID="btnview" OnClick="btnview_Click" runat="server" Text="<%$ Resources:LocalizedText, lblView%>"	/>
                                            
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


                                     <!-- button pour l'enregistrement et l'initialisation des données-->
                                    
                                        <div class="clearfix form-actions">
                                        <div>
                                           <%-- <asp:Button ID="btnGoSysaid" class="btn btn-info"  OnClick="btnGoSysaid_Click"  OnClientClick="this.disabled=true;" UseSubmitBehavior="false" runat="server" Text="<%$ Resources:LocalizedText, GoSysaid%>"  />--%>
                                         
										<asp:HyperLink class="btn btn-info" Visible="false" runat="server" ID="myHpl" Text="<%$ Resources:LocalizedText, GoSysaid%>"  />

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
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter blue no-margin"><asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, popUpInvalidInput%>" runat="server"></asp:Literal></h3>
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
        <div class="modal-header">
            <h3 class="smaller lighter blue no-margin">
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
                   
                      <asp:Button ID="Button2" class="btn btn-success"  runat="server" Text="OK"  />

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

    </script>


</asp:Content>
