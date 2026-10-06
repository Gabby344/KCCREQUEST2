<%@ Page Title="" Language="C#" MasterPageFile="~/NewRequest.Master" AutoEventWireup="true" CodeBehind="New_DeviceRegistration.aspx.cs" Inherits="KCCREQUEST2.New_DeviceRegistration" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
         #lblStaffGlencore, #lblStaffContractor
         {
             margin-bottom:10px !important;
         }
    .modalBackground
        {
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
         .spacingCell{
             border-spacing:5px;
         }
         .hidden{
             display:none;
           
         }

     
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
                    <h1><asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, lbldeviceenregister%>"></asp:Label>
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
                            
                          
                          <asp:Label ID="lbldeviceregistration" runat="server" Text="<%$ Resources:LocalizedText, lbldeviceregistration%>"></asp:Label>
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
                                                <h5 class="lighter block"  style="color:rgb(187,135,72);font-weight:bold">
                                                   <asp:Literal ID="headewrequest" Text="<%$ Resources:LocalizedText, headewrequest%>" runat="server"></asp:Literal>

                                                </h5>
                                            </td>
                                        </tr>

                                      

                                        <tr>
                                            <td>
                                                 <asp:Panel ID="kcc" runat="server" GroupingText="Requested for">
                                                     <tr>
                                                         <td style="margin-bottom:1%;"width: 100%"  colspan="2" >
                                                                
                                                               <asp:Literal ID="Literal5" Text="Residential Status" runat="server"></asp:Literal><br> 
                                                          

                                                  <asp:RadioButton ID="rbtnGlencor" runat="server"  Checked="true" GroupName="kcc"  AutoPostBack="true"
                                                  OnCheckedChanged="rbtn_CheckedChanged" />
                                                  <asp:Literal  ID="lblStaffGlencore" Text="<%$ Resources:LocalizedText, lblStaffGlencore%>" runat="server"></asp:Literal> &nbsp&nbsp&nbsp

                                                
                                                <asp:RadioButton ID="rbtnContractor" runat="server" GroupName="kcc"  AutoPostBack="true"
                                                 OnCheckedChanged="rbtn_CheckedChanged" />
                                                     <asp:Literal  ID="lblStaffContractor" Text="<%$ Resources:LocalizedText, lblStaffContractor%>" runat="server"></asp:Literal><br><br>
                                                              

                                                </td>
                                                
                                             

                                               
                                                     </tr>  
                              
                                                
                                            </asp:Panel>
                                            </td>
                                           
                                        </tr>


                                                 
                                    

                                        
                                   
                                    <tr>
                                        <td  style="width:50%">
                                                <div class="">
                                                    <label for="form-field-select-1">
                                                      <asp:Literal  ID="Literal4" Text="<%$ Resources:LocalizedText, type%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>



                                                <%--    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="DeviceType" />
                                                                            </Triggers>
                                                                            <ContentTemplate>--%>
                                                                                                
                                     <asp:DropDownList ID="DeviceType" class="form-control" Width="  90%" runat="server"  OnSelectedIndexChanged="DeviceType_SelectedIndexChanged"  AutoPostBack="true" >
                                                                                                
                                                                  <asp:ListItem Text="Choose One / Selectionner"></asp:ListItem>
                                                                  <asp:ListItem Text="TV/Television"></asp:ListItem>
                                                                   <asp:ListItem Text="Phone/Telephone"></asp:ListItem>
                                                                   <asp:ListItem Text="Tablet/Tablette"></asp:ListItem>
                                                                    <asp:ListItem Text="Game console / Console de jeu"></asp:ListItem>


                                      </asp:DropDownList>    





                                                                           


<%--                                                                                   </ContentTemplate>
                                                                        </asp:UpdatePanel>--%>



                                    

                                                </div>
                                            </td>
                                                    
                                                        
                                                    <td style="width: 50%">
                                                  
                                                <div>
                                                   
                                                    <label for="form-field-select-1">
                                                      <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblMacAdress%>" runat="server"></asp:Literal>    
                                                        <b style="color: red">*</b></label>
                                                   <input name="macAddress" id="macAddress" class="form-control" Width="90%" oncontextmenu="return false" type="text" maxlength="17" ></input>

                                                </div> 
                                            </td>  <td>  </td>
                                          
                                          </tr>
                                          <tr>

                                              <td style="width: 50%" >
                                                   <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                             <ContentTemplate>
                                               <div visible="false" id="dvRomm" runat ="server">
                                                        
                                                    <label for="form-field-select-1">
                                                   
                                                       <asp:Literal ID="lblRooms" Text="<%$ Resources:LocalizedText, lblRoom%>" runat="server"></asp:Literal>    
                                                        <b style="color: red">*</b></label>
                                                   <asp:TextBox ID="rooms" MaxLength="50" runat="server" class="form-control" Width="90%"></asp:TextBox>
                                               
                                                </div>
                                                                  </ContentTemplate>
                                                        </asp:UpdatePanel>
                                              </td> <td> </td>
                                          </tr>

                                          <tr>
                                            <td colspan="2" style="width: 50%">
                                                <div>
                                                    <br />
                                                    <label for="form-field-select-1">
                                                      <asp:Literal ID="lblDevice" Text="<%$ Resources:LocalizedText, lblDevice%>" runat="server"></asp:Literal>    
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtSubject" MaxLength="50" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                            
                                        </tr>





                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                        Description 
                                                        <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtDescription" onkeypress="if (this.value.length > 150) { return false; }"  TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%; display:none">
                                                <div>
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
                                                    <asp:TextBox ID="txtNotes" Text="Okay" TextMode="SingleLine"  onkeypress="if (this.value.length > 50) { return false; }" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>

                                            <td style="width: 50%">
                                                <div class="hidden">
                                                    <label for="form-field-select-1">
                                                         <asp:Literal ID="lblWhoIsAffected" Text="<%$ Resources:LocalizedText, lblWhoIsAffected%>"  runat="server"></asp:Literal>   
                                                         <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstAffected" class="form-control" Width="  90%" Enabled="false"  runat="server" AutoPostBack="false" OnSelectedIndexChanged="LstAffected_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </td>
                                       
                                           
                                            <td style="width: 50%">
                                                <div class="hidden">
                                                    <label for="form-field-select-1">
                                                       <asp:Literal ID="lblDoyouHaveaworkaround" Text="<%$ Resources:LocalizedText, lblDoyouHaveaworkaround%>"  runat="server"></asp:Literal>   
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstWorkAround" class="form-control" Width="100%" Enabled="false" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                               </tr>

                                        <tr>
                                                 <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                           <%--<asp:Literal ID="lblRequestor" Text="<%$ Resources:LocalizedText, lblRequestor%>" runat="server"></asp:Literal>--%> 
                                                             <asp:Literal ID="lblBeneficiary" Text="<%$ Resources:LocalizedText, lblBeneficiary%>" runat="server"></asp:Literal>   
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList hidden="true" ID="LstRequestedBy" OnSelectedIndexChanged="LstRequestedBy_SelectedIndexChanged" class="chosen-select form-control" Width="95%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="LstRequestedBy" />
                                                                            </Triggers>
                                                        <ContentTemplate>
                                                    <asp:TextBox ID="txtBeneficiary" MaxLength="70" runat="server" Width="95%"  Visible="false"></asp:TextBox>
                                                          </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                </div>
                                            </td>
                                     
                                            
                                            <td colspan="2" style="width: 100%"  id="tdcompany" runat="server" visible="false" >

                                            <asp:Literal ID="lblCompany" Text="<%$ Resources:LocalizedText, lblCompany%>" runat="server"></asp:Literal>
                                            <b style="color: red">*</b></label>

                                                        <%-- <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="rbtnContractor" />
                                                                            </Triggers>
                                                                <ContentTemplate>--%>
                                            <span class="input-icon block">
                                                <%--<asp:DropDownList ID="LstCompanyName" class="width-100 form-control" runat="server" AutoPostBack="false" Enabled="true"></asp:DropDownList>--%>
                                                <asp:TextBox ID="LstCompanyName" MaxLength="70" runat="server" Width="100%"  ></asp:TextBox>
                                                 <%--<asp:TextBox ID="LstCompanyName" Text="Okay" TextMode="SingleLine"  onkeypress="if (this.value.length > 50) { return false; }" runat="server" Width="100%"></asp:TextBox>--%>
                                            </span>
                                     
                                            <%--   </ContentTemplate>
                                               </asp:UpdatePanel>--%>

                                      
                                            </td>


                                        
                                            

                                               
                                        </tr>

                                     <%--   <tr>
                                            

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                    <asp:Literal ID="lblFile" Text="<%$ Resources:LocalizedText, lblFile%>" runat="server"></asp:Literal>  

                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload2" Width="90%" runat="server" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>--%>

                                           <tr>



                                                                                           <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                           <asp:Literal ID="lblContactNumber" Text="<%$ Resources:LocalizedText, lblContactNumber%>" runat="server"></asp:Literal> 
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="LstRequestedBy" />
                                                                            </Triggers>
                                                                            <ContentTemplate>
                                                    <asp:TextBox ID="txtContactNumber" MaxLength="40" TextMode="Phone" runat="server" Width="95%"></asp:TextBox>
                                                                                   </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                </div>
                                            </td>
                                               <td>
                                                  <%--<div>
                                                    <label for="form-field-select-1">
                                                      <asp:Literal ID="lblBeneficiary" Text="<%$ Resources:LocalizedText, lblBeneficiary%>" runat="server"></asp:Literal>    
                                                        <b style="color: red">*</b></label><br />
                                        
                                                </div>--%>

                                                   <div class="">
                                                    <label for="form-field-select-1">
                                                      <asp:Literal  ID="lblDepartment" Text="<%$ Resources:LocalizedText, lblDepartment%>" runat="server"></asp:Literal>
                                                        <b style="color: red">*</b></label>
                                                    <br>  <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                        <asp:DropDownList   ID="LstDepartment" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </ContentTemplate>
                                                         </asp:UpdatePanel>
                                                        </div>

                                               </td>
                                       



                                                
                                        </tr>

                                        <tr>
                                                 

                                                                 <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                           <asp:Literal ID="lblJobTitle" Text="<%$ Resources:LocalizedText, lblJobTitle%>" runat="server"></asp:Literal> 
                                                        <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="LstRequestedBy" />
                                                                            </Triggers>
                                                                            <ContentTemplate>
                                                    <asp:TextBox ID="txtJobTitle" MaxLength="40"  runat="server" Width="95%"></asp:TextBox>
                                                                                   </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                </div>
                                            </td>

                                               <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">
                                                           <asp:Literal ID="lblEmplacement" Text="<%$ Resources:LocalizedText, lblEmplacement%>" runat="server"></asp:Literal>  
                                                         <b style="color: red">*</b></label>
                                                    <br>
                                                     <asp:DropDownList ID="LstLocation" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                            <tr>
                                               <td colspan="2" style="width: 100%" >
                                                   <br>

                                                  <asp:CheckBox ID="ispermanent" runat="server"  OnCheckedChanged="ispermanent_CheckedChanged"  AutoPostBack="true"/>
                                                  <asp:Literal  ID="lblispermanent" Text="<%$ Resources:LocalizedText, lblispermanent%>" runat="server"></asp:Literal> <br>
                                                   <br>
                                                    
                                               </td>
                                           
                                        </tr>

                                    </table>


                                 


                                    <!--fin entete requesition-->
                                    <hr />


                                     <!-- button pour l'enregistrement et l'initialisation des données-->
                                    
                                        <div class="clearfix form-actions">
                                  
                                             <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="ispermanent" />
                                                                            </Triggers>
                                                                            <ContentTemplate>
                                            <asp:Button ID="btnSave" cssclass="btn2 btn2-btnSave btn2-cons" OnClick="btnSave_Click"  Visible="false"  runat="server" Text="<%$ Resources:LocalizedText, btnSave%>"  />
                                          
											 <asp:Button ID="btnReset" OnClick="btnReset_Click" cssclass="btn2 btn2-btnReset btn2-cons"   runat="server" Text="<%$ Resources:LocalizedText, btnReset%>" />
                                             </ContentTemplate>
                                                                        </asp:UpdatePanel>
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
                    <h3 class="smaller lighter white no-margin"><asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, popUpInvalidInput%>" runat="server"></asp:Literal></h3>
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
        <div class="modal-header" style="background-color:#006666;">
            <h3 class="smaller lighter white no-margin">
                     CONFIRMATION
                    </h3>
        </div>
        <div class="modal-body">
           <h3 class="smaller lighter green no-margin">
            <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
                  <asp:Label ID="lblIDRequest" runat="server" Visible="false" Text=""></asp:Label>
                            </h3>
     
        </div>
               <div class="modal-footer">
                   
                      <asp:Button ID="btnOk" class="btn btn-success" OnClick="btnOk_Click"  runat="server" Text="OK"  />

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




    var macAddress = document.getElementById("macAddress");

    function formatMAC(e) {
        var r = /([a-f0-9]{2})([a-f0-9]{2})/i,
            str = e.target.value.replace(/[^a-f0-9]/ig, "");

        while (r.test(str)) {
            str = str.replace(r, '$1' + ':' + '$2');
        }

        e.target.value = str.slice(0, 17);
    };

    macAddress.addEventListener("keyup", formatMAC, false);


    
    
</script>
    <div class="loading" align="center" id="panelLoading" runat="server">
       <h4 class="widget-title red">
    <asp:Literal ID="Literal12" Text="<%$ Resources:LocalizedText, lblLoading%>" runat="server"></asp:Literal>
            </h4>
    <img src="bx_loader.gif" alt="" />
</div>
</asp:Content>
