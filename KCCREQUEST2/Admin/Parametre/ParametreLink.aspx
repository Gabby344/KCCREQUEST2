<%@ Page Title="" Language="C#" MasterPageFile="~/Model_Page.Master" AutoEventWireup="true" CodeBehind="ParametreLink.aspx.cs" enableEventValidation="false" Inherits="KCCREQUEST2.Admin.Parametre.ParametreLink" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="outter-wp">
        <h2 class="inner-tittle">Parameter - Links</h2>
        <asp:Label ID="AnswerFromServer" runat="server"></asp:Label>
        
        <br/> <br/>

        <div class="graph" id="table" style="background-color: transparent; height: 600px; overflow-y: auto; overflow-x: hidden;">
            <div class="tables">
               
                <asp:GridView ID="dgvLiens" runat="server" AutoGenerateColumns="False" DataKeyNames="Key" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA">
                    <Columns>
                        <asp:TemplateField HeaderText="Key" SortExpression="Key">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Key") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" SortExpression="Description">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Value" SortExpression="Value">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
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

    <asp:HiddenField ID="DescriptionSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ValueSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="KeySet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ActionSet" runat="server" Value="Up" Visible="true" />


    <style>
        .
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
  <%--  <script src="../.../Scripts/jquery-ui-1.12.1.js"></script>--%>
        <script>
            $(function () {

                // J Detection et click sur le bouton EDIT  et SAVE de la gridView en trouvant les inputs celle-ci
                $('#<%=dgvLiens.ClientID %>').find("input").each(function (index, element) {

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
                                if (ind !== 3 && ind !== 0) {

                                    // On rend les cellules editables
                                    var dataTex = $.trim($(el).text());
                                    $(el).html("");
                                    $(el).html('<input type="text" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:20px"/>');
                                    $(el).find('input').attr('value', dataTex);
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
                                if (ind !== 3) {

                                    // si les champs sont editables on les rend non editables
                                    var inputSet = $(el).find('input');
                                    if (inputSet.is('input')) {
                                        $(el).html(inputSet.val());
                                    }

                                    // on recupere les donnes de chaques cellules qu'on met dans le tableau sauf la derniere
                                    tabData.push($(el).text());
                                }
                            });

                            // on passe les modifications aux controles corespondants
                            $("#<%= KeySet.ClientID%>").attr("Value", tabData[0]);
                            $("#<%= DescriptionSet.ClientID%>").attr("Value", tabData[1]);
                            $("#<%= ValueSet.ClientID%>").attr("Value", tabData[2]);
                            $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");

                        }

                    });
                });

            });
        </script>
</asp:Content>

