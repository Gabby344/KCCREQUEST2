<%@ Page Title="" Language="C#" MasterPageFile="~/Model_Page.Master" AutoEventWireup="true" enableEventValidation="false" CodeBehind="CouleurMail.aspx.cs" Inherits="KCCREQUEST2.Admin.Parametre.CouleurMail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%--     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
<div class="outter-wp">
        <h2 class="inner-tittle">Parameter - Modules</h2>
        <asp:Label ID="AnswerFromServer" runat="server"></asp:Label>
    <div style="display:none">
      <asp:DropDownList ID="dllBodyMailColorCode" Visible="true" runat="server"></asp:DropDownList>
     <asp:DropDownList ID="dllBodyMailColorDescription" Visible="true" runat="server"></asp:DropDownList>
    </div>
       <br/>
       <br/>
        <div class="graph" id="table" style="background-color: transparent; height: 600px; overflow-y: auto; overflow-x: hidden;">
            <div class="tables">
               
                <asp:GridView ID="dgvCouleurMail" runat="server" AutoGenerateColumns="False" DataKeyNames="ModuleID" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA">
                    <Columns>
                        <asp:TemplateField HeaderText="Module ID" SortExpression="ModuleID">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ModuleID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Module Description English" SortExpression="ModuleDescription">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ModuleDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Module Description French" SortExpression="ModuleDescription">
                            <ItemTemplate>
                                <asp:Label ID="Labelfr" runat="server" Text='<%# Bind("ModuleDescriptionFrench") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Body Color" SortExpression="BodyMailColorCode">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("BodyMailColorCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Bar Color" SortExpression="BodyMailColorDescription">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("BodyMailColorDescription") %>'></asp:Label>
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

    <asp:HiddenField ID="ModuleDescriptionSet" runat="server" Value="5" Visible="true" />
      <asp:HiddenField ID="ModuleDescriptionFrSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="BodyMailColorCodeSet" runat="server" Value="5" Visible="true" />
     <asp:HiddenField ID="BodyMailColorDescriptionSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ModuleIDSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ActionSet" runat="server" Value="Up" Visible="true" />


    <style>
       
            #table {
                width:90%;
                float:right;
                top:-45px;
                overflow-x:hidden;
                height:521px;
            }
            .graph-form, #table {
            }
    </style>
    <script src="../../Scripts/jquery-3.2.1.js"></script>
<%--    <script src="../.../Scripts/jquery-ui-1.12.1.js"></script>--%>
        <script>
            $(function () {

                // J Detection et click sur le bouton EDIT  et SAVE de la gridView en trouvant les inputs celle-ci
                $('#<%=dgvCouleurMail.ClientID %>').find("input").each(function (index, element) {

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
                                if (ind == 1 || ind==2) {

                                    // On rend les cellules editables
                                    var dataTex = $.trim($(el).text());
                                    $(el).html("");
                                    $(el).html('<input type="text" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:20px"/>');
                                    $(el).find('input').attr('value', dataTex);
                                }

                                // On rend la 2 cellule editable par une DropDowlist preseectionné
                                if (ind == 3) {

                                    var dataText = $.trim($(el).text());
                                    var select = $('#<%=dllBodyMailColorCode.ClientID %>').clone();
                                    $(select).find('option').each(function (index, element) {

                                        if ($.trim($(element).text()) == dataText) {
                                            $(element).attr('selected', 'selected');
                                        }

                                    });
                                    $(el).empty();
                                    $(el).append($(select));
                                }

                                // On rend la 3 cellule editable par une DropDowlist preseectionné
                                if (ind == 4) {
                                    var dataText = $.trim($(el).text());
                                    var select = $('#<%=dllBodyMailColorDescription.ClientID %>').clone();
                                    $(select).find('option').each(function (index, element) {
                                        if ($.trim($(element).text()) == dataText) {
                                            $(element).attr('selected', 'selected');
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
                                if (ind == 1 || ind==2) {

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
                                else if ($(elemetSet).is('select') && ind == 4) {
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
                            $("#<%= ModuleIDSet.ClientID%>").attr("Value", tabData[0]);
                            $("#<%= ModuleDescriptionSet.ClientID%>").attr("Value", tabData[1]);
                            $("#<%= ModuleDescriptionFrSet.ClientID%>").attr("Value", tabData[2]);
                            $("#<%= BodyMailColorCodeSet.ClientID%>").attr("Value", tabData[3]);
                            $("#<%= BodyMailColorDescriptionSet.ClientID%>").attr("Value", tabData[4]);
                            $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");

                        }

                    });
                });

            });
        </script>
</asp:Content>

