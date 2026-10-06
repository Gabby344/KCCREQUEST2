<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_EW_TechnicalDepartment.aspx.cs" Inherits="KCCREQUEST2.New_EW_TechnicalDepartment" enableEventValidation="false"  MasterPageFile="~/Model_Page.Master" %>
<asp:Content ID="Item" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <h2 class="inner-tittle">EWO Request - Technical department status</h2>
        <asp:Label ID="Label6" runat="server"></asp:Label>
        <div class="forms-main" id="forme">
            <div class="graph-form">
                <div class="validation-form">
                   
                        <div class="col-md-12 form-group1 group-mail">
                            <asp:Label ID="Label4" runat="server" Text="Technical DepDescription: "></asp:Label>
                            <asp:TextBox ID="TechnicalDepDescription" runat="server" placeholder="Your Technical DepDescription here *" BackColor="White"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-12 form-group1 group-mail">
                            <asp:Label ID="Label5" runat="server" Text="Department Code: "></asp:Label>
                            <asp:TextBox ID="DepartmentCode" runat="server" placeholder="Your Department Code here *" BackColor="White" type="number"></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                        <div class="col-md-12 form-group2 group-mail">
                            <label class="control-label">Role Coordonator: </label>
                            <asp:DropDownList ID="RoleCoordonator" runat="server"></asp:DropDownList>

                            <label class="control-label">Role Man: </label>
                            <asp:DropDownList ID="RoleMan" runat="server"></asp:DropDownList>


                            <asp:Label ID="Label3" runat="server" Text="Role GM : "></asp:Label>
                            <asp:DropDownList ID="Rolegm" runat="server"></asp:DropDownList>

                        </div>
                        <div class="clearfix"></div>

                        <div class="col-md-12 form-group1 group-mail">
                            <asp:Label ID="Label1" runat="server" Text="Is technical: "></asp:Label>
                            <asp:TextBox ID="Istchnical" runat="server"  BackColor="White" type="number"></asp:TextBox>
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
                <asp:GridView ID="Tableau_EW_TechnicalDepartment_Request_Item_modification" runat="server" AutoGenerateColumns="False" DataKeyNames="TechincalDepId" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA">
                    <Columns>
                        <asp:TemplateField HeaderText="ID" SortExpression="ItemID">
                            <ItemTemplate>
                                <asp:Label ID="lblItems" runat="server" Text='<%# Bind("TechincalDepId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Technical DepDescription" SortExpression="TechnicalDepDescription">
                            <ItemTemplate>
                                <asp:Label ID="ServiceDescription" runat="server" Text='<%# Bind("TechnicalDepDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Department Code" SortExpression="DepartmentCod">
                            <ItemTemplate>
                                <asp:Label ID="CategoryDescription" runat="server" Text='<%# Bind("DepartmentCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Role Coordonator" SortExpression="RoleCoordonator">
                            <ItemTemplate>
                                <asp:Label ID="ApproverRole" runat="server" Text='<%# Bind("RoleCoordonator") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Role Man" SortExpression="RoleMan">
                            <ItemTemplate>
                                <asp:Label ID="ItemDescription" runat="server" Text='<%# Bind("RoleMan") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Role GM" SortExpression="RoleGM">
                            <ItemTemplate>
                                <asp:Label ID="Observation" runat="server" Text='<%# Bind("RoleGM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Is technical" SortExpression="Istchnical">
                            <ItemTemplate>
                                <asp:Label ID="LevelItem" runat="server" Text='<%# Bind("Istchnical") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Actions" SortExpression="ItemDescription" ItemStyle-Width="200px">
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
    <asp:HiddenField ID="TechnicalDepDescriptionSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="DepartmentCodeSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="RoleCoordonatorSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="RoleManSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="RoleGMSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="IstchnicalSet" runat="server" Value="5" Visible="true" />
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
            overflow:auto;
            height:590px;
        }
        .graph-form, #table {
        }
    </style>
              <script src="../../Scripts/jquery-3.2.1.js"></script>
   <%-- <script src="../.../Scripts/jquery-ui-1.12.1.js"></script>--%>
    <script>
        $(function () {

            // Detection et click sur le bouton EDIT  et SAVE de la gridView en trouvant les inputs celle-ci
            $('#<%=Tableau_EW_TechnicalDepartment_Request_Item_modification.ClientID %>').find("input").each(function (index, element) {

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


                            // On rend la 1 cellule editable par une DropDowlist preseectionné
                            if (ind == 1) {

                                // On rend les cellules editables
                                var dataTex = $.trim($(el).text());
                                $(el).html("");
                                $(el).html('<input type="text" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:2px"/>');
                                $(el).find('input').attr('value', dataTex);
                            }

                            // On rend la 2 cellule editable par une input de type number
                            if (ind == 2) {

                                // On rend les cellules editables
                                var dataTex = $.trim($(el).text());
                                $(el).html("");
                                $(el).html('<input type="number" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:2px"/>');
                                $(el).find('input').attr('value', dataTex);

                                $(el).append($(select));
                            }

                            // On rend la 3 cellule editable par une DropDowlist preseectionné
                            if (ind == 3) {

                                var dataText = $.trim($(el).text());
                                var select = $('#<%=RoleCoordonator.ClientID %>').clone();
                                $(select).find('option').each(function (index, element) {

                                    if ($.trim($(element).text()) == dataText) {
                                        $(element).attr('selected', 'selected');
                                    }

                                });
                                $(el).empty();
                                $(el).append($(select));
                              
                            }

                            // On rend la 4 cellule editable par une DropDowlist preseectionné
                            if (ind == 4) {

                                var dataText = $.trim($(el).text());
                                var select = $('#<%=RoleMan.ClientID %>').clone();
                                $(select).find('option').each(function (index, element) {
                                    if ($.trim($(element).text()) == dataText) {
                                        $(element).attr('selected', 'selected');
                                    }
                                });
                                $(el).empty();
                                $(el).append($(select));
                            }

                            // On rend la 5 cellule editable par une DropDowlist preseectionné
                            if (ind == 5) {

                                var dataText = $.trim($(el).text());
                                var select = $('#<%=Rolegm.ClientID %>').clone();
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
                            if (ind == 6) {

                                // On rend les cellules editables
                                var dataTex = $.trim($(el).text());
                                var dataInt;
                                dataInt = (dataTex == "False") ? 0 : 1;
                                $(el).html("");
                                $(el).html('<input type="number" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:2px"/>');
                                $(el).find('input').attr('value', dataInt);
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
                            if (ind == 1 || ind == 2 || ind==6 ) {

                                // si le champs est editable on le rend non editable
                                var inputSet = $(el).find('input');
                                if (inputSet.is('input')) {
                                    $(el).html(inputSet.val());
                                }
                            }

                            // on recupere les donnes de chaques cellules qu'on met dans le tableau sauf la derniere
                            if (ind!==7) {

                                if (!$(elemetSet).is('select')) {

                                    tabData.push($.trim($(el).text()));
                                }

                                else {
                                    var det = $(elemetSet).val();
                                    $(elemetSet).find('option').each(function (pos, objElem) {

                                        if ($(objElem).attr('value') == det) {
                                            tabData.push($(objElem).text());
                                        }
                                    });
                                }
                            }

                        });

                        // on passe les modifications aux controles corespondants  
                        console.log(tabData);

                        $("#<%= IDSet.ClientID%>").attr("Value", tabData[0]);
                        $("#<%= TechnicalDepDescriptionSet.ClientID%>").attr("Value", tabData[1]);
                        $("#<%= DepartmentCodeSet.ClientID%>").attr("Value", tabData[2]);
                        $("#<%= RoleCoordonatorSet.ClientID%>").attr("Value", tabData[3]);
                        $("#<%= RoleManSet.ClientID%>").attr("Value", tabData[4]);
                        $("#<%= RoleGMSet.ClientID%>").attr("Value", tabData[5]);
                        $("#<%= IstchnicalSet.ClientID%>").attr("Value", tabData[6]);
                        $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");
                    }

                });
            });
        });
    </script>
</asp:Content>
