<%@ Page Title="" Language="C#" MasterPageFile="~/Model_Page.Master" AutoEventWireup="true" CodeBehind="New_WorkType.aspx.cs" enableEventValidation="false"  Inherits="KCCREQUEST2.Admin.EW_Request.New_WorkType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="outter-wp">
        <h2 class="inner-tittle">EWO Request - Work Type</h2>
        <asp:Label ID="AnswerFromServer" runat="server"></asp:Label>
        <div class="graph-form" id="forme">
            <div class="form-body">
               <div class="validation-form">
                    <div class="col-md-12 form-group2 group-mail">
                        <asp:Label ID="Label1" runat="server" Text="Work Type Description: "></asp:Label>
                        <asp:TextBox ID="txtWorkType" runat="server" CssClass="form-control" placeholder="Work Type *"></asp:TextBox>
                    </div>
                    <div class="col-md-12 form-group2 group-mail">
                        <asp:Label ID="Label2" runat="server" Text="Work Type Description French: "></asp:Label>
                        <asp:TextBox ID="txtWorkTypeFrench" runat="server" CssClass="form-control" placeholder="Desrciption french *"></asp:TextBox>
                    </div>
                 <div class="col-md-12 form-group2 group-mail">
                            <asp:Label ID="Label6" runat="server" Text="Department: " CssClass="control-label"></asp:Label>
                            <asp:DropDownList ID="LstDepartment"   runat="server"></asp:DropDownList>
                        </div>
                    <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="Save" CssClass="btn btn-primary"/>
              </div>
            </div>
        </div>
        <br/> <br/>

        <div class="graph" id="table" style="background-color: transparent; height: 600px; overflow-y: auto; overflow-x: hidden;">
            <div class="tables">
               
                <asp:GridView ID="dgvWorkType" runat="server" AutoGenerateColumns="False" DataKeyNames="WorkTypeID" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA">
                    <Columns>
                        <asp:TemplateField HeaderText="Work Type ID" SortExpression="WorkTypeID">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("WorkTypeID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Technical Department Description" SortExpression="Departement">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("TechnicalDepDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Work Type Description" SortExpression="WorkTypeDescription">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("WorkTypeDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="WorkT ype Description French" SortExpression="WorktypeDescription">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("WorkTypeDescriptionFrench") %>'></asp:Label>
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

    <asp:HiddenField ID="WorkTypeIDSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="TechnicalDepDescriptionSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="WorkTypeDescriptionSet" runat="server" Value="5" Visible="true" />
        <asp:HiddenField ID="WorkTypeDescriptionFrenchSet" runat="server" Value="5" Visible="true" />
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
                $('#<%=dgvWorkType.ClientID %>').find("input").each(function (index, element) {

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
                                if (ind == 2 || ind==3) {

                                    // On rend les cellules editables
                                    var dataTex = $.trim($(el).text());
                                    $(el).html("");
                                    $(el).html('<input type="text" name="amount" style="width:100%;border:1px solid #EAEAEA; padding-left:20px"/>');
                                    $(el).find('input').attr('value', dataTex);
                                }

                                    // On rend la 1 cellule editable par une DropDowlist preselectionné
                             if (ind == 1) {

                                 var dataText = $.trim($(el).text());
                                 var select = $('#<%=LstDepartment.ClientID%>').clone();
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
                            console.log(tabData);

                            $("#<%= WorkTypeIDSet.ClientID%>").attr("Value", tabData[0]);
                            $("#<%= TechnicalDepDescriptionSet.ClientID%>").attr("Value", tabData[1]);
                            $("#<%= WorkTypeDescriptionSet.ClientID%>").attr("Value", tabData[2]);
                            $("#<%= WorkTypeDescriptionFrenchSet.ClientID%>").attr("Value", tabData[3]);
                            $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");

                        }

                    });
                });

            });
        </script>
</asp:Content>
