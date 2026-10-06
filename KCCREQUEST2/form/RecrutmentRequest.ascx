<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecrutmentRequest.ascx.cs" Inherits="KCCREQUEST2.form.RecrutmentRequest" %>

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">

                                    <!-- entite de la requisition-->
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <h5 class="lighter block green">Recrutment Request</h5>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <div>
                                                    <label for="form-field-select-1">Requestor  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestor" class="chosen-select form-control" Width="50%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Category  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstCategory" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Priority  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstPriority" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Position Level  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstPositionLevel" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Type of Contract <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstTypeOfContrat" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Duration  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtDuration" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Cost Center  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstCostCenter" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Planned Start Date <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtPlannedStartDate"  class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Job Title  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtJobTitle" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Number of Positions  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtNumberOfPosition" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div>
                                                            <asp:CheckBox ID="CbIsReplacement" OnCheckedChanged="CbIsReplacement_CheckedChanged" AutoPostBack="true" runat="server" class="ace" /><span>  </span>
                                                            <label for="form-field-select-1">Is Replacement</label>
                                                            <br>
                                                            <asp:TextBox ID="txtIsReplacement" runat="server" Width="90%"></asp:TextBox>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Replacement Employee Name  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtReplacementEmployeeName" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Replacement Post Number  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtReplacementPostNumber" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>


                                         <tr>
                                            <td colspan="3">
                                                <div>
                                                    <label for="form-field-select-1">Replacement reason <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtReplacementReason" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Replacement Job Title <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="TextBox1" TextMode="SingleLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="CbDescriptionTitle" AutoPostBack="true"  runat="server" class="ace" /><span> Job Description Title</span>
                                                      
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td> <h5 class="lighter block green">Main Responsible</h5></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div>
                                                    <label for="form-field-select-1">Job Grade <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstJobGrade" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td colspan="3">
                                                <div>
                                                    <br/>
                                                    <asp:TextBox ID="txtJobGrade" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td colspan="3">
                                                <div>
                                                    <label for="form-field-select-1">Minimum Qualifications <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtMinimumQualification" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td colspan="3">
                                                <div>
                                                    <label for="form-field-select-1">Other Requirements <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtOhterRequirements" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">Manager <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="TextBox2" TextMode="SingleLine" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                              <td>
                                                <div>
                                                    <label for="form-field-select-1">Create By <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="TextBox3" TextMode="SingleLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">File</label>
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
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnSave" class="btn btn-info" runat="server" Text="Save" />
                                            &nbsp; &nbsp; &nbsp;
											 <asp:Button ID="Button1" class="btn btn-danger" runat="server" Text="Reset" />

                                        </div>

                                    </div>
                                    <!--fin zone de button-->
                                    <br />
                                </div>

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