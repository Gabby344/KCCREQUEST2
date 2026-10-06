<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_Status.aspx.cs" Inherits="KCCREQUEST2.New_Status"  enableEventValidation="false" MasterPageFile="~/Model_Page.Master" %>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="outter-wp">
        <h2 class="inner-tittle">IT Request - Status</h2>
        <asp:Label ID="AnswerServer" runat="server"></asp:Label>
        <div class="graph-form">
            <div class="form-body">
              
                    <div class="form-group">
                        <asp:Label ID="Label12" runat="server" Text="Status ID: "></asp:Label>
                        <asp:TextBox ID="StatusID" runat="server" CssClass="form-control" placeholder="Status ID *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label11" runat="server" Text="Status Description English: "></asp:Label>
                        <asp:TextBox ID="StatusDescription" runat="server" CssClass="form-control" placeholder="Status Description *"></asp:TextBox>
                    </div>
                 <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="Status Description French: "></asp:Label>
                        <asp:TextBox ID="StatusDescriptionFrench" runat="server" CssClass="form-control" placeholder="Status Description french *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Action: "></asp:Label>
                        <asp:TextBox ID="Action" runat="server" CssClass="form-control" placeholder="Action *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Email Description Orginator in French : "></asp:Label>
                        <asp:TextBox ID="EmailDescriptionOrginatorFr" runat="server" CssClass="form-control" placeholder="Email Description Orginator in French *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Email Description Orginator in English: "></asp:Label>
                        <asp:TextBox ID="EmailDescriptionOrginatorEn" runat="server" CssClass="form-control" placeholder="Email Description Orginator in English *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server" Text="Email Subject Orginator in French: "></asp:Label>
                        <asp:TextBox ID="EmailSubjectOrginatorFr" runat="server" CssClass="form-control" placeholder="Email Subject Orginator in French *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                         <asp:Label ID="Label6" runat="server" Text="Email Subject Orginator in English: "></asp:Label>
                        <asp:TextBox ID="EmailSubjectOrginatorEn" runat="server" CssClass="form-control" placeholder="Email Subject Orginator in English *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label7" runat="server" Text="Email Description Approver in French: "></asp:Label>
                        <asp:TextBox ID="EmailDescriptionApproverFr" runat="server" CssClass="form-control" placeholder="Email Description Approver in French *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                         <asp:Label ID="Label8" runat="server" Text="Email Description Approver in English: "></asp:Label>
                        <asp:TextBox ID="EmailDescriptionApproveEn" runat="server" CssClass="form-control" placeholder="Email Description Approver in English *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label9" runat="server" Text="Email Subject Approver in French: "></asp:Label>
                        <asp:TextBox ID="EmailSubjectApproverFr" runat="server" CssClass="form-control" placeholder="Email Subject Approver in French *"></asp:TextBox>
                    </div>
                    <div class="form-group">
                         <asp:Label ID="Label10" runat="server" Text="Email Subject Approver in English: "></asp:Label>
                        <asp:TextBox ID="EmailSubjectApproverEn" runat="server" CssClass="form-control" placeholder="Email Subject Approver in English *"></asp:TextBox>
                    </div>

                    <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Enregistrer" OnClick="Submit_Click" />
              
            </div>
        </div>
        <br />
        <br />

        <div class="graph" id="table">
            <div class="tables">
                
                <asp:GridView ID="Tableau_IT_Request_Status" runat="server" AutoGenerateColumns="False" DataKeyNames="StatusID" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA" Width="2800px">
                    <Columns>
                        <asp:TemplateField HeaderText="Status ID" SortExpression="StatusID">
                            <ItemTemplate>
                                <asp:Label ID="StatusID" runat="server" Text='<%# Bind("StatusID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status Description" SortExpression="StatusDescription">
                            <ItemTemplate>
                                <asp:Label ID="StatusDescription" runat="server" Text='<%# Bind("StatusDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Status Description" SortExpression="StatusDescription">
                            <ItemTemplate>
                                <asp:Label ID="StatusDescriptionFr" runat="server" Text='<%# Bind("StatusDescriptionFr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Action statut" SortExpression="ActionStatut">
                            <ItemTemplate>
                                <asp:Label ID="ActionStatut" runat="server" Text='<%# Bind("Action") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Description Orginator Fr" SortExpression="EmailDescriptionOrginatorFr">
                            <ItemTemplate>
                                <asp:Label ID="EmailDescriptionOrginatorFr" runat="server" Text='<%# Bind("EmailDescriptionOrginatorFr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email Description Orginator En" SortExpression="EmailDescriptionOrginatorEn">
                            <ItemTemplate>
                                <asp:Label ID="EmailDescriptionOrginatorEn" runat="server" Text='<%# Bind("EmailDescriptionOrginatorEn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email SubjectOrginator Fr" SortExpression="EmailSubjectOrginatorFr">
                            <ItemTemplate>
                                <asp:Label ID="EmailSubjectOrginatorFr" runat="server" Text='<%# Bind("EmailSubjectOrginatorFr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email Subject Orginator En" SortExpression="EmailSubjectOrginatorEn">
                            <ItemTemplate>
                                <asp:Label ID="EmailSubjectOrginatorEn" runat="server" Text='<%# Bind("EmailSubjectOrginatorEn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email Description Approver Fr" SortExpression="EmailDescriptionApproverFr">
                            <ItemTemplate>
                                <asp:Label ID="EmailDescriptionApproverFr" runat="server" Text='<%# Bind("EmailDescriptionApproverFr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email Description Approve En" SortExpression="EmailDescriptionApproveEn">
                            <ItemTemplate>
                                <asp:Label ID="EmailDescriptionApproveEn" runat="server" Text='<%# Bind("EmailDescriptionApproveEn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email Subject Approver Fr" SortExpression="EmailSubjectApproverFr">
                            <ItemTemplate>
                                <asp:Label ID="EmailSubjectApproverFr" runat="server" Text='<%# Bind("EmailSubjectApproverFr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email Subject Approver En" SortExpression="EmailSubjectApproverEn">
                            <ItemTemplate>
                                <asp:Label ID="EmailSubjectApproverEn" runat="server" Text='<%# Bind("EmailSubjectApproverEn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Actions" SortExpression="ItemDescription" ItemStyle-Width="190px">
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

    <asp:HiddenField ID="StatusIDSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="StatusDescriptionSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="StatusDescriptionFrSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ActionStatusSet" runat="server" Value="5" Visible="true" />  
    <asp:HiddenField ID="EmailDescriptionOrginatorFrSet" runat="server" Value="5" Visible="true" /> 
    <asp:HiddenField ID="EmailDescriptionOrginatorEnSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="EmailSubjectOrginatorFrSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="EmailSubjectOrginatorEnSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="EmailDescriptionApproverFrSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="EmailDescriptionApproveEnSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="EmailSubjectApproverFrSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="EmailSubjectApproverEnSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ActionSet" runat="server" Value="Up" Visible="true" /> 

          <script src="../../Scripts/jquery-3.2.1.js"></script>
<%--    <script src="../.../Scripts/jquery-ui-1.12.1.js"></script>--%>
    <script>
        $(function () {

            // Detection et click sur le bouton EDIT  et SAVE de la gridView en trouvant les inputs celle-ci
            $('#<%=Tableau_IT_Request_Status.ClientID %>').find("input").each(function (index, element) {

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

                            // On rend les cellules  editables
                            if (ind >0 && ind< 12) {

                                var dataTex = $.trim($(el).text());
                                $(el).html(" ");
                                $(el).html('<textarea name="amount"  rows="4" cols="100"/>');
                                //$(el).find('textarea').attr('value', dataTex);
                                $(el).find('textarea').val(dataTex);
                            }

                            // On rend la cellule 0  editable de type number
                            if (ind ==0) {

                                var dataTex = $.trim($(el).text());
                                $(el).html(" ");
                                $(el).html('<textarea name="amount"  rows="4" cols="100"/>');
                                $(el).find('textarea').val(dataTex);
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
                            if (ind !== 12) {
                                // si le champs est editable on le rend non editable
                                var inputSet = $(el).find('textarea');
                                if (inputSet.is('textarea')) {
                                    $(el).html(inputSet.val());
                                    tabData.push($(el).text());
                                }
                            }

                        });

                        // on passe les modifications aux controles corespondants  
                        console.log(tabData);
                        $("#<%= StatusIDSet.ClientID%>").attr("Value", tabData[0]);
                        $("#<%= StatusDescriptionSet.ClientID%>").attr("Value", tabData[1]);
                        $("#<%= StatusDescriptionFrSet.ClientID%>").attr("Value", tabData[2]);
                        $("#<%= ActionStatusSet.ClientID%>").attr("Value", tabData[3]);
                        $("#<%= EmailDescriptionOrginatorFrSet.ClientID%>").attr("Value", tabData[4]);
                        $("#<%= EmailDescriptionOrginatorEnSet.ClientID%>").attr("Value", tabData[5]);
                        $("#<%= EmailSubjectOrginatorFrSet.ClientID%>").attr("Value", tabData[6]);
                        $("#<%= EmailSubjectOrginatorEnSet.ClientID%>").attr("Value", tabData[7]);
                        $("#<%= EmailDescriptionApproverFrSet.ClientID%>").attr("Value", tabData[8]);
                         $("#<%= EmailDescriptionApproveEnSet.ClientID%>").attr("Value", tabData[9]);
                        $("#<%= EmailSubjectApproverFrSet.ClientID%>").attr("Value", tabData[10]);
                        $("#<%= EmailSubjectApproverEnSet.ClientID%>").attr("Value", tabData[11]);
                        $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");
                       
                     }

                 });
             });


         });
    </script>
</asp:Content>