<%@ Page Title="" Language="C#" MasterPageFile="~/ViewRequest.Master" AutoEventWireup="true" CodeBehind="My_RequestToAprovalHold.aspx.cs" Inherits="KCCREQUEST2.My_RequestToAprovalHold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="assets/css/responsive.css">

    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
            


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1>
                       <asp:Literal ID="smenuAllRequestAppr" Text="<%$ Resources:LocalizedText, smenuAllRequestApprHold%>" runat="server"></asp:Literal>
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    <%--All requests to Approve--%>
                                </small>
                    </h1>
                 
																
															
														

                </div>
                <!--FIN TITRE DE LA PAGE-->



                <!--bloc pour les information du requisitionneur-->
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center;">
                        <h4 class="widget-title lighter" id="title" runat="server">
                           <%-- Requests to Approve--%>

                        </h4> 

                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                     
                    </div>

                    <div class="widget-body" >
                       <div>
                            <div>

                                <!--bloc du grid des requisitions-->
                             
                               <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 98%; position: inherit; height: 97%; margin-left: 1%; margin-bottom: 1%; padding-bottom: 5%; margin-right: 1%; padding-left: 2%; padding-right: 2%;" id="bodyItRequest">
                                    <br />
                                       <div id="Div2" class="tab-pane">
                                    <asp:GridView ID="dgvRequestToAproval"  runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="Id" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                          
                                            <asp:TemplateField  HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                    ID
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                       <span class="badge badge-info" style="background-color:#003468">
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                   Request login
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestLogin" runat="server" Text='<%#Eval("RequestorLogin")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="17%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                  <asp:Literal ID="requestorName" Text="<%$ Resources:LocalizedText, requestorName%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestorName" runat="server" Text='<%#Eval("RequestorName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="15%" ItemStyle-Width="7%">
                                                <HeaderTemplate>
                                                   <asp:Literal ID="lblsBeneficiary" Text="<%$ Resources:LocalizedText, lblBeneficiary%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiary" runat="server" Text='<%#Eval("Beneficiary")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                           
                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="20%" ItemStyle-Width="10%">
                                                <HeaderTemplate>
                                                   Description
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="10%" ItemStyle-Width="5%">
                                                <HeaderTemplate>
                                                   <asp:Literal ID="lblsdepartment" Text="<%$ Resources:LocalizedText, lblsdepartment%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                                <HeaderTemplate>
                                                 <asp:Literal ID="lblPriority" Text="<%$ Resources:LocalizedText, lblPriority%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPriorite" runat="server" Text='<%#Eval("PrioriteDescprition")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="10%" ItemStyle-Width="5%">
                                                <HeaderTemplate>
                                                       <asp:Literal ID="lblStatut" Text="<%$ Resources:LocalizedText, lblStatut%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("StatusDescription")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="15%" ItemStyle-Width="10%">
                                                <HeaderTemplate>
                                                    <asp:Literal ID="lblPlannedStartDate" Text="<%$ Resources:LocalizedText, lblPlannedStartDate%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlannedDeliveryDate" runat="server" Text='<%# DateTime.Parse(Eval("PlannedDeliveryDate").ToString()).ToString("MM-dd-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                <HeaderTemplate>
                                                    Doc pos
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocPos" runat="server" Text='<%#Eval("DocPos")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false"  HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                <HeaderTemplate>
                                                   Current User
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrentUser" runat="server" Text='<%#Eval("CurrentUser")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff"  Visible="false" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                <HeaderTemplate>
                                                   Created
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                <HeaderTemplate>
                                                   Module
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                      <span class="label label-info arrowed-right arrowed-in" >
                                                    <asp:Label ID="lblModule" runat="server" Text='<%#Eval("Module")%>'></asp:Label>
                                                            </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#737CA1" HeaderStyle-BorderColor="#737CA1" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                        <HeaderTemplate></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button CssClass="btn  btn-xs btn-danger" OnClick="btndetail_Click" ID="btndetail" runat="server" Text="Detail" />
                                            
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
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter blue no-margin">Comment</h3>
                </div>

                <div class="modal-body">


                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">Comment <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                        </div>

                    </h4>

                </div>

                <div class="modal-footer">


                    <asp:Button ID="btnSave"  class="btn btn-sm btn-info" runat="server" Text="OK" />


                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        Close
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
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter blue no-margin">Invalid Input</h3>
                </div>

                <div class="modal-body">

                   
                            <h4 class="smaller lighter red no-margin">
                                <asp:Label ID="lblErreur" runat="server" Text=""></asp:Label>

                            </h4>





                      
                </div>

                <div class="modal-footer">
                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        Close
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!--fin popup-->
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
</asp:Content>
