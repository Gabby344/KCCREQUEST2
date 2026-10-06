<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NEW_TRANS_Site.aspx.cs" Inherits="KCCREQUEST2.Admin.TRANS_Request.NEW_TRANS_Site" enableEventValidation="false"  MasterPageFile="~/Model_Page.Master" %>
<asp:Content ID="ProcessRole" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <h2 class="inner-tittle">TRANSPORT Request - Site</h2>
        <asp:Label ID="AnswerFromServer" runat="server"></asp:Label>
        <div class="graph-form">
            <div class="form-body">
              
                    <%--<div class="form-group" >
                        <asp:Label ID="Label1" runat="server" Text="Name Of Role: "></asp:Label>
                        <asp:TextBox ID="Role" runat="server" CssClass="form-control" placeholder="Name Of Role *"></asp:TextBox>
                    </div>--%>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Site Description: "></asp:Label>
                        <asp:TextBox ID="RoleDescription" runat="server" CssClass="form-control" placeholder="Desrciption *"></asp:TextBox>
                    </div>
                    <asp:Button ID="Button1" runat="server" Text="enregistrer" OnClick="Button1_Click" CssClass="btn btn-primary"/>
              
            </div>
        </div>
        <br/> <br/>

        <div class="graph" id="table" style="background-color: transparent; height: 600px; overflow-y: auto; overflow-x: hidden;">
            <div class="tables">
               
                <asp:GridView ID="Tableau_EW_Request_ProcessRole_modification" runat="server" AutoGenerateColumns="False" DataKeyNames="SiteId" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" SortExpression="CategoryID">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("SiteId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Site" SortExpression="Site" >
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("SiteDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:TemplateField HeaderText="Site Description" SortExpression="Description">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("SiteDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

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
    <asp:HiddenField ID="RoleSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="IDSet" runat="server" Value="5" Visible="true" />
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
                $('#<%=Tableau_EW_Request_ProcessRole_modification.ClientID %>').find("input").each(function (index, element) {

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
                            $("#<%= IDSet.ClientID%>").attr("Value", tabData[0]);
                            $("#<%= RoleSet.ClientID%>").attr("Value", tabData[1]);
                            $("#<%= DescriptionSet.ClientID%>").attr("Value", tabData[2]);
                            $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");

                        }

                    });
                });

            });
        </script>
</asp:Content>
