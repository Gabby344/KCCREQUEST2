<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_Item.aspx.cs" Inherits="KCCREQUEST2.New_Item" enableEventValidation="false"  MasterPageFile="../../Model_Page.Master" %>
<asp:Content ID="Item" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <h2 class="inner-tittle">IT REQUEST - Items</h2>
        <asp:Label ID="Label6" runat="server"></asp:Label>
        <div class="forms-main" id="forme">
            <div class="graph-form">
                <div class="validation-form">
               
                        <div class="col-md-12 form-group1 group-mail">
                            <asp:Label ID="Label4" runat="server" Text="Item description English: "></asp:Label>
                            <asp:TextBox ID="ItemDescription" runat="server" placeholder="Your Item description here *" BackColor="White"></asp:TextBox>
                        </div>

                     <div class="col-md-12 form-group1 group-mail">
                            <asp:Label ID="Label2" runat="server" Text="Item description French: "></asp:Label>
                            <asp:TextBox ID="ItemDescriptionFrench" runat="server" placeholder="Your Item description french here *" BackColor="White"></asp:TextBox>
                        </div>

                        <div class="clearfix"></div>
                        <div class="col-md-12 form-group1 group-mail">
                            <asp:Label ID="Label5" runat="server" Text="Observation: "></asp:Label>
                            <asp:TextBox ID="Observation" runat="server" placeholder="Your Observation here *" BackColor="White"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-12 form-group2 group-mail">
                            <label class="control-label">Service: </label>
                            <asp:DropDownList ID="Service" runat="server"></asp:DropDownList>

                            <label class="control-label">Category: </label>
                            <asp:DropDownList ID="Category" runat="server"></asp:DropDownList>


                            <asp:Label ID="Label3" runat="server" Text="ApproverRole: "></asp:Label>
                            <asp:DropDownList ID="ApproverRole" runat="server"></asp:DropDownList>

                        </div>
                        <div class="clearfix"></div>

                        <div class="col-md-12 form-group1 group-mail">
                            <asp:Label ID="Label1" runat="server" Text="Item Level: "></asp:Label>
                            <asp:TextBox ID="Level" runat="server" placeholder="Your Level here *" BackColor="White" type="number"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>

                        <div class="col-md-12 form-group button-2">
                            <asp:Button ID="Button1" runat="server" Text="Enregistrer" OnClick="Button1_Click" CssClass="btn btn-primary" />
                        </div>
                        <div class="clearfix"></div>
                
                </div>
            </div>
        </div><br/><br/>

        <div class="graph" id="table">
            <div class="tables">
                <asp:GridView ID="Tableau_IT_Request_Item_modification" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ItemID" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" Visible="true" SortExpression="ItemID">
                            <ItemTemplate>
                                <asp:Label ID="lblItems" runat="server" Text='<%# Bind("ItemID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Service" SortExpression="ServiceDescription">
                            <ItemTemplate>
                                <asp:Label ID="ServiceDescription" runat="server" Text='<%# Bind("ServiceDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Categorie" SortExpression="CategoryDescription">
                            <ItemTemplate>
                                <asp:Label ID="CategoryDescription" runat="server" Text='<%# Bind("CategoryDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Approver Role" SortExpression="ApproverRole">
                            <ItemTemplate>
                                <asp:Label ID="DDLApproverRole" runat="server" Text='<%# Bind("ApproverRole") %>'></asp:Label>
                            </ItemTemplate>

                          
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Item English" SortExpression="ItemDescription">
                            <ItemTemplate>
                                <asp:Label ID="ItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>


                           <asp:TemplateField HeaderText="Item French" SortExpression="ItemDescription">
                            <ItemTemplate>
                                <asp:Label ID="ItemDescriptionFrench" runat="server" Text='<%# Bind("ItemDescriptionFrench") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Observation" SortExpression="Observation">
                            <ItemTemplate>
                                <asp:Label ID="Observation" runat="server" Text='<%# Bind("Observation") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Level" SortExpression="Level">
                            <ItemTemplate>
                                <asp:Label ID="LevelItem" runat="server" Text='<%# Bind("Level") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Actions" SortExpression="ItemDescription" ItemStyle-Width="40%">
                            <ItemTemplate>
                                <asp:Button ID="Edit" runat="server" Width="50%" Text="Edit" CssClass="Button12" />
                                <asp:Button ID="Save" runat="server"  Text="Save" CssClass="Button13" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>



    <asp:HiddenField ID="IDSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ServiceDescriptionSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="CategoryDescriptionSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ApproverRoleSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ItemDescriptionSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ItemDescriptionFrenchSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ObservationSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="LevelSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ActionSet" runat="server" Value="Up" Visible="true" />

    <style>
        .graph-form {
            width:23%;
            float:left;
        }
        #table {
            width:75%;
            float:right;
            top:-55px;
            overflow: auto;
            height:590px;
        }
        .graph-form, #table {
        }
    </style>
  <script src="../../Scripts/jquery-3.2.1.js"></script>
 <%--   <script src="../.../Scripts/jquery-ui-1.12.1.js"></script>--%>
    <script>
       $(function () {

            // Detection et click sur le bouton EDIT  et SAVE de la gridView en trouvant les inputs celle-ci
            $('#<%=Tableau_IT_Request_Item_modification.ClientID %>').find("input").each(function (index, element) {

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

                            // Seulement la 4 ieme et 5 ieme cellule sont changés en textbox
                            if (ind == 4 || ind == 5 || ind == 6) {

                                // On rend les cellules editables
                                var dataTex = $.trim($(el).text());
                                $(el).html("");
                                $(el).html('<input type="text" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:20px"/>');
                                $(el).find('input').attr('value', dataTex);
                            }

                            // On rend la 1 cellule editable par une DropDowlist preseectionné
                            if (ind == 1) {

                                var dataText = $.trim($(el).text());
                                var select = $('#<%=Service.ClientID %>').clone();
                                $(select).find('option').each(function (index, element) {

                                    if ($.trim($(element).text()) == dataText) {
                                        $(element).attr('selected', 'selected');
                                    }

                                });
                                $(el).empty();
                                $(el).append($(select));
                            }

                            // On rend la 2 cellule editable par une DropDowlist preseectionné
                            if (ind == 2) {
                                var dataText = $.trim($(el).text());
                                var select = $('#<%=Category.ClientID %>').clone();
                                $(select).find('option').each(function (index, element) {
                                    if ($.trim($(element).text()) == dataText) {
                                        $(element).attr('selected', 'selected');
                                    }
                                });
                                $(el).empty();
                                $(el).append($(select));
                            }

                            // On rend la 3 cellule editable par une DropDowlist preseectionné
                            if (ind == 3) {
                                var dataText = $.trim($(el).text());
                                var select = $('#<%=ApproverRole.ClientID %>').clone();
                                $(select).find('option').each(function (index, element) {
                                    if ($.trim($(element).text()) == dataText) {
                                        $(element).attr('selected', 'selected');
                                        console.log($(element));
                                    }
                                });
                                $(el).empty();
                                $(el).append($(select));
                            }

                            // Seulement la 6 ieme cellule sont changés en textbox de type number
                            if (ind == 7) {

                                // On rend les cellules editables
                                var dataTex = $.trim($(el).text());
                                $(el).html("");
                                $(el).html('<input type="number" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:5px"/>');
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
                            var elemetSet = $(el).find('select');
                            if (ind == 4 || ind == 5 || ind == 6 || ind == 7) {

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
                            else if ($(elemetSet).is('select') && ind == 3) {
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
                        $("#<%= ServiceDescriptionSet.ClientID%>").attr("Value", tabData[1]);
                        $("#<%= CategoryDescriptionSet.ClientID%>").attr("Value", tabData[2]);
                        $("#<%= ApproverRoleSet.ClientID%>").attr("Value", tabData[3]);
                        $("#<%= ItemDescriptionSet.ClientID%>").attr("Value", tabData[4]);
                        $("#<%= ItemDescriptionFrenchSet.ClientID%>").attr("Value", tabData[5]);
                        $("#<%= ObservationSet.ClientID%>").attr("Value", tabData[6]);
                        $("#<%= LevelSet.ClientID%>").attr("Value", tabData[7]);
                        $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");
                    }

                });
            });
        });
    </script>
</asp:Content>
