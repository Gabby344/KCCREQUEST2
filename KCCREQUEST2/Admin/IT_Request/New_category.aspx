<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_category.aspx.cs" Inherits="KCCREQUEST2.New_category" enableEventValidation="false" MasterPageFile="~/Model_Page.Master" %>
    <asp:content id="Categorie" contentplaceholderid="ContentPlaceHolder1" runat="Server">

        <div class="outter-wp">
		    <h2 class="inner-tittle">IT REQUEST - Categorie</h2>
                <asp:Label ID="reponse" runat="server" Text=""></asp:Label>
		    <div class="graph-form">
			    <div class="form-body">
				 
					    <div class="form-group">
                            <asp:Label ID="Label6" runat="server" Text="Categorie Description: "></asp:Label>
                                <asp:TextBox ID="CategoryDescription" runat="server"  cssClass="form-control"  placeholder="Categorie Description *"></asp:TextBox> 
					    </div> 
					    <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Observation: "></asp:Label>
                            <asp:TextBox ID="Observations" runat="server" CssClass="form-control"  placeholder="Desrciption *"></asp:TextBox>
					    </div>
                        <asp:Button ID="Button1" runat="server" Text="enregistrer" cssClass="btn btn-primary" OnClick="Button1_Click" />
				  
			    </div>
		    </div>
		    <br/><br/>
			    <div class="graph" id="table">
				    <div class="tables">
                        <center>
                        <asp:GridView ID="Tableau_IT_Request_Categorie_modification" runat="server" AutoGenerateColumns="False" DataKeyNames="CategoryID"  cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA" >
                            <Columns>
                                <asp:TemplateField HeaderText="ID" SortExpression="CategoryID">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("CategoryID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                         CategoryID
                                    </HeaderTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CategoryDescription" SortExpression="CategoryDescription">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("CategoryDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Observations" SortExpression="Observations">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Observations") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actions" SortExpression="FirstName">
                                    <ItemTemplate>
                                        <asp:Button ID="Edit" runat="server" Text="Edit" CssClass="Button12" />
                                        <asp:Button ID="Save" runat="server" Text="Save"  CssClass="Button13"  />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                       </center>
				    </div>				
		        </div>
	    </div>

        <asp:HiddenField ID="CategoryDescriptionSet" runat="server" Value="5" Visible="true" />
        <asp:HiddenField ID="ObservationsSet" runat="server" Value="5" Visible="true" />  
        <asp:HiddenField ID="IDSet" runat="server" Value="5" Visible="true" /> 
        <asp:HiddenField ID="ActionSet" runat="server" Value="Up" Visible="true" /> 

        <style>
            #ContentPlaceHolder1_Tableau_IT_Request_Categorie_modification  {
               width:100%;
            }

            .graph-form {
                width:20%;
                float:left;
                height:521px;
            }
            #table {
                width:75%;
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


<%--    <script src="../../js/amcharts.js"></script>	--%>
    <%--<script src="../../js/serial.js"></script>	
    <script src="../../js/light.js"></script>	
    <script src="../../js/radar.js"></script>	
	<script type="text/javascript" src="../../js/TweenLite.min.js"></script>
	<script type="text/javascript" src="../../js/CSSPlugin.min.js"></script>--%>
	<%--<script src="../../js/jquery.nicescroll.js"></script>
	<script src="../../js/scripts.js"></script>--%>
   

    <script>
        $(function () {
            
            // Detection et click sur le bouton EDIT  et SAVE de la gridView en trouvant les inputs celle-ci
            $('#<%=Tableau_IT_Request_Categorie_modification.ClientID %>').find("input").each(function (index, element) {

                // On cache le bouton save
                if ($(element).attr("value") == "Save") {
                    $(element).hide();
                }

                // Click sur la input de la gridView
                $(element).click(function (event) {

                    //Si la valeur de l'attribut value de l'input est Edit
                    if ($(event.target).attr("value") == "Edit") {

                        $(event.target).css("left","0%");
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
                        $("#<%= CategoryDescriptionSet.ClientID%>").attr("Value", tabData[1]);
                        $("#<%= ObservationsSet.ClientID%>").attr("Value", tabData[2]);
                        $("#<%= ActionSet.ClientID%>").attr("Value", "UPDATE");
                        
                    }

                });
            });

        });
    </script>
</asp:content>
