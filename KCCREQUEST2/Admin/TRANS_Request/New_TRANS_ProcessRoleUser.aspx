<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_TRANS_ProcessRoleUser.aspx.cs" enableEventValidation="false" Inherits="KCCREQUEST2.Admin.TRANS_Request.New_TRANS_ProcessRoleUser"  enableEventValidation="false"  MasterPageFile="~/Model_Page.Master" %>

<asp:Content ID="ProcessRole_User" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- Champ necessaire au Srcipt JS --%>
    <select name="Role" id="Select1_Actived" style="display: none;">
        <option value="1">True</option>
        <option value="0">False</option>
    </select>
    <%-- Champ necessaire au Srcipt JS --%>

    <div class="outter-wp">
        <h2 class="inner-tittle">TRANSPORT Request - process role</h2>
        <asp:Label ID="Label5" runat="server"></asp:Label>
        <div class="forms-main" id="forme">
            <div class="graph-form">
                <div class="validation-form">
                   
                       <div class="col-md-12 form-group2 group-mail">
                            <asp:Label ID="Label1" runat="server" Text="User Loggin: "></asp:Label>
                           <%-- <asp:TextBox ID="UserLoggin" runat="server" placeholder="User Loggin here *" BackColor="White"></asp:TextBox>--%>
                             <asp:DropDownList ID="dllUsers"  class="chosen-select  form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="clearfix"></div>

                        <div class="col-md-12 form-group1 group-mail">
                            <asp:Label ID="Label6" runat="server" Text="StartDate: "></asp:Label>
                            <asp:TextBox ID="StartDate" runat="server" data-date-format="mm-dd-yyyy"  placeholder="Start Date here *" BackColor="White" CssClass="DatePic"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>

                        <div class="col-md-12 form-group1 group-mail">
                            <asp:Label ID="Label4" runat="server" Text="EndDate: "></asp:Label>
                            <asp:TextBox ID="EndDate" runat="server" data-date-format="mm-dd-yyyy"  placeholder="End Date here *" BackColor="White" CssClass="DatePic"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>

                        <div class="col-md-12 form-group2 group-mail">
                            <asp:Label ID="Label2" runat="server" Text="Role: " CssClass="control-label"></asp:Label>
                            <asp:DropDownList ID="Role" runat="server"></asp:DropDownList>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-12 form-group button-2">
                            <asp:Button ID="Submit" runat="server" Text="Enregistrer" OnClick="Submit_Click" CssClass="btn btn-primary"/>
                        </div>
                        <div class="clearfix"></div>
                
                </div>
            </div>
        </div>
        <br>
        <br>
        <div class="graph" id="table">
            <div class="tables">

                 <table>
                        <tr>
                           <td width="900px"></td>
                            <td>Role/User<br>
                              
                                        <asp:TextBox runat="server" id="txtRequisitionID"  Width="250px"   class="form-control pull-right" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                                <br/>
                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click"  class="btn green" runat="server" Text="Search"/>
                            </td>

                        </tr>
                    </table>
                      <br/>

                <asp:GridView ID="Tableau_EW_Request_ProcessRoleUser" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" SortExpression="Id">
                            <ItemTemplate>
                                <asp:Label ID="lblItems" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="UserLogin" SortExpression="UserLogin">
                            <ItemTemplate>
                                <asp:Label ID="UserLogin" runat="server" Text='<%# Bind("UserLogin") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Role" SortExpression="Role">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Start Date" SortExpression="StartDate">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="End Date" SortExpression="EndDate">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Activated" SortExpression="Activated">
                            <ItemTemplate>
                                <asp:Label ID="Activated" runat="server" Text='<%# Bind("Activated") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Actions" SortExpression="ItemDescription" ItemStyle-Width="150px">
                            <ItemTemplate>
                                 <asp:Button ID="Edit" runat="server" Text="Edit" CssClass="Button12" />
                                <asp:Button ID="Save" runat="server" Text="Save" CssClass="Button13" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>



    <asp:HiddenField ID="IDSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="UserLoginSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="RoleSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="StartDateSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="EndDateSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ActivatedSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ActionSet" runat="server" Value="Up" Visible="true" />

    <style>
        .graph-form {
                width:30%;
                float:left;
                height:521px;
            }
            #table {
                width:65%;
                float:right;
                top:-54px;
                overflow-x:hidden;
                height:521px;
            }
            .graph-form, #table {
            }
    </style>
              <script src="../../Scripts/jquery-3.2.1.js"></script>
<%--    <script src="../.../Scripts/jquery-ui-1.12.1.js"></script>--%>

    <script src="../../assets/js/bootstrap-datepicker.min.js"></script>
		<script src="../../assets/js/bootstrap-timepicker.min.js"></script>
		<script src="../../assets/js/moment.min.js"></script>
		<script src="../../assets/js/daterangepicker.min.js"></script>
		<script src="../../assets/js/bootstrap-datetimepicker.min.js"></script>
		

	

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


     <script>
         $(function () {

             // Detection et click sur le bouton EDIT  et SAVE de la gridView en trouvant les inputs celle-ci
             $('#<%=Tableau_EW_Request_ProcessRoleUser.ClientID %>').find("input").each(function (index, element) {

                 // On cache le bouton save
                 if ($(element).attr("value") == "Save") {
                     $(element).hide();
                 }

                 // Click sur la input de la gridView
                 $(element).click(function (event) {

                     //Si la valeur de l'attribut value de l'input est Edit
                     if ($(event.target).attr("value") == "Edit") {

                         $(event.target).css("left", "0%");

                         // On stop l'action par defaut du bouton qui est de tourner sur le serveur en rafrachissant la page
                         event.preventDefault();

                         // On l'affiche le bouton Save;
                         $(event.target).next().show();


                         // On change la valeur du bouton  de Edit en Cancel
                         $(event.target).attr("value", "Cancel");

                         // On recuprer la ligne correspndante
                         var rowLine = $(event.target).parent().parent();

                         // On trouve une refence des cellules de la ligne 
                         var cellLine = rowLine.find("td");

                         // On parcour chaque celleule de la ligne
                         cellLine.each(function (ind, el) {

                             // On rend la cellule 1 editables
                             if (ind == 1) {

                                 var dataTex = $.trim($(el).text());
                                 $(el).html(" ");
                                 $(el).html('<input type="text" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:20px"/>');
                                 $(el).find('input').attr('value', dataTex);
                             }

                             // On rend la 2 cellule editable par une DropDowlist preselectionné
                             if (ind == 2) {

                                 var dataText = $.trim($(el).text());
                                 var select = $('#<%=Role.ClientID %>').clone();
                                 $(select).find('option').each(function (index, element) {

                                     if ($.trim($(element).text()) == dataText) {
                                         $(element).attr('selected', 'selected');
                                     }

                                 });
                                 $(el).empty();
                                 $(el).append($(select));
                             }

                             // On rend les cellules 3 et 4  editables par des date pickers
                             if (ind == 3 || ind == 4) {

                                 var dataTex = $.trim($(el).text());
                                 var new_datepicker = $('<input type="text" value="' + dataTex + '" style="width:100%;border:1px solid #EAEAEA; padding-left:20px">').datepicker();
                                 $(el).empty();
                                 $(el).append(new_datepicker);
                             }

                             // Sauf la derniere cellule et la premiere
                             if (ind == 5) {

                                 var dataText = $.trim($(el).text());
                                 var select = $("#Select1_Actived").clone();
                                 $(select).find('option').each(function (index, element) {

                                     if ($.trim($(element).text()) == dataText) {
                                         $(element).attr('selected', 'selected');
                                     }

                                 });
                                 $(select).css("display", "block");
                                 $(el).empty();
                                 $(el).append($(select));
                             }

                         });
                     }

                     //Si la valeur de l'attribut value de l'input est Save
                     if ($(event.target).attr("value") == "Save") {

                         // On recuprer la ligne correspndante
                         var rowLine = $(event.target).parent().parent();

                         // On trouve une refence des cellules de la ligne 
                         var cellLine = rowLine.find("td");

                         // On cree un tableau aui contiendra les valeurs a passer au controles HiddenField pour C#
                         var tabData = [];

                         // On parcour chaque cellule de la ligne sauf la derniere cellule
                         cellLine.each(function (ind, el) {

                             var elemetSet = $(el).find('select');
                             if (ind == 1 || ind == 3 || ind == 4) {

                                 // si le champs est editable on le rend non editable
                                 var inputSet = $(el).find('input');
                                 if (inputSet.is('input')) {
                                     $(el).html(inputSet.val());
                                 }
                             }

                             // on recupere les donnes de chaques cellules qu'on met dans le tableau sauf la derniere
                             if (!$(elemetSet).is('select')) {

                                 tabData.push($.trim($(el).text()));
                             }
                             else if ($(elemetSet).is('select') && ind == 2) {
                                 var det = $(elemetSet).val();
                                 $(elemetSet).find('option').each(function (pos, objElem) {

                                     if ($(objElem).attr('value') == det) {
                                         tabData.push($(objElem).text());
                                     }
                                 });
                             }
                             else {
                                 tabData.push($(elemetSet).val());
                             }

                         });

                         // on passe les modifications aux controles corespondants  
                         console.log(tabData);
                         $("#<%= IDSet.ClientID%>").attr("Value", tabData[0]);
                         $("#<%= UserLoginSet.ClientID%>").attr("Value", tabData[1]);
                         $("#<%= RoleSet.ClientID%>").attr("Value", tabData[2]);
                         $("#<%= StartDateSet.ClientID%>").attr("Value", tabData[3].split(" ")[0]);
                         $("#<%= EndDateSet.ClientID%>").attr("Value", tabData[4].split(" ")[0]);
                         $("#<%= ActivatedSet.ClientID%>").attr("Value", tabData[5].split(" ")[0]);
                         $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");
                     }

                 });
             });

             $(".DatePic").datepicker();

         });
     </script>
</asp:Content>

