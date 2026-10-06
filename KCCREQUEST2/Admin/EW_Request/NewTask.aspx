<%@ Page Title="" Language="C#" MasterPageFile="~/Model_Page.Master" AutoEventWireup="true" CodeBehind="NewTask.aspx.cs"  enableEventValidation="false" Inherits="KCCREQUEST2.Admin.EW_Request.NewTask" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="outter-wp">
        <h2 class="inner-tittle">EWO Request - Task</h2>
        <asp:Label ID="AnswerFromServer" runat="server"></asp:Label>
        <div class="graph-form">
            <div class="form-body">
                 <div class="validation-form">
                   
                 <div class="col-md-12 form-group2 group-mail">
                            <asp:Label ID="Label6" runat="server" Text="Work Type: " CssClass="control-label"></asp:Label>
                            <asp:DropDownList ID="LstWorkType"  runat="server"></asp:DropDownList>
                        </div>

                 <div class="col-md-12 form-group2 group-mail">
                        <asp:Label ID="Label1" runat="server" Text="Task Description: "></asp:Label>
                        <asp:TextBox ID="txtTaskDescriptionEng" runat="server" CssClass="form-control" placeholder="Task Description *"></asp:TextBox>
                    </div>
                    <div class="col-md-12 form-group2 group-mail">
                        <asp:Label ID="Label2" runat="server" Text="Task Description French: "></asp:Label>
                        <asp:TextBox ID="txtTaskDescriptionFren" runat="server" CssClass="form-control" placeholder="Desrciption french *"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary"/>
              </div>
            </div>
        </div>
        <br/> <br/>

        <div class="graph" id="table" style="background-color: transparent; height: 600px; overflow-y: auto; overflow-x: hidden;">
            <div class="tables">
               
                <asp:GridView ID="dgvTask" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskId" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA">
                    <Columns>
                        <asp:TemplateField HeaderText="Task Id" SortExpression="TaskId">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("TaskId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Work Type Description" SortExpression="WorkTypeId">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("WorkTypeDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Task Description English" SortExpression="TaskDescription">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("TaskDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Task Description French" SortExpression="TaskDescriptionFrench">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("TaskDescriptionFrench") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Actions" SortExpression="FirstName">
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

    <asp:HiddenField ID="TaskIdSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="WorkTypeSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="TaskDescriptionEngSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="TaskDescriptionFrSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ActionSet" runat="server" Value="Up" Visible="true" />


    <style>
        .graph-form {
                width:30%;
                float:left;
                height:598px;
            }
            #table {
                width:65%;
                float:right;
                top:-45px;
                overflow-x:hidden;
                height:521px;
            }
            .graph-form, #table {
            }
    </style>
              <script src="../../Scripts/jquery-3.2.1.js"></script>
<%--    <script src="../../Scripts/jquery-ui-1.12.1.js"></script>--%>
        <script>
            $(function () {

                // J Detection et click sur le bouton EDIT  et SAVE de la gridView en trouvant les inputs celle-ci
                $('#<%=dgvTask.ClientID %>').find("input").each(function (index, element) {

                    // E On cache le bouton save
                    if ($(element).attr("value") == "Save") {
                        $(element).hide();
                    }

                    // S Click sur la input de la gridView
                    $(element).click(function (event) {

                        //U Si la valeur de l'attribut value de l'input est Edit
                        if ($(event.target).attr("value") == "Edit") {

                            $(event.target).css("left", "0%");

                            // S On stop l'action par defaut du bouton qui est de tourner sur le serveur en rafrachissant la page
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

                                // Sauf la derniere cellule et la premiere
                                if (ind == 2 || ind == 3) {

                                    // On rend les cellules editables
                                    var dataTex = $.trim($(el).text());
                                    $(el).html("");
                                    $(el).html('<input type="text" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:20px"/>');
                                    $(el).find('input').attr('value', dataTex);
                                }

                                 // On rend la 1 cellule editable par une DropDowlist preselectionné
                             if (ind == 1) {

                                 var dataText = $.trim($(el).text());
                                 var select = $('#<%=LstWorkType.ClientID%>').clone();
                                 $(select).find('option').each(function (index, element) {

                                     if ($.trim($(element).text()) == dataText) {
                                         $(element).attr('selected', 'selected');
                                         console.log($(element));
                                     }

                                 });
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
                                if (ind == 2 || ind == 3) {

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

                                else if ($(elemetSet).is('select') && ind == 1) {
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
                            $("#<%= TaskIdSet.ClientID%>").attr("Value", tabData[0]);
                            $("#<%= WorkTypeSet.ClientID%>").attr("Value", tabData[1]);
                            $("#<%= TaskDescriptionEngSet.ClientID%>").attr("Value", tabData[2]);
                            $("#<%= TaskDescriptionFrSet.ClientID%>").attr("Value", tabData[3]);
                            $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");

                        }

                    });
                });

            });
        </script>
</asp:Content>
