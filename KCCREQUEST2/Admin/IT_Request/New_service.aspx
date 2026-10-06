<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="New_service.aspx.cs" Inherits="KCCREQUEST2.New_service" enableEventValidation="false"  MasterPageFile="../../Model_Page.Master" %>
 <asp:Content ID="Service" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <div class="outter-wp">
           <center><h2 class="inner-tittle">IT REQUEST - Service</h2> </center>
           <div class="graph-form">
               <div class="form-body">
                     
                   <br/>
                       <div class="form-group">
                           <asp:Label ID="Label6" runat="server" Text="Service Description: "></asp:Label>
                           <asp:TextBox ID="ServiceDescription" runat="server" CssClass="form-control" placeholder="Service Description here *"></asp:TextBox>
                       </div>
                       <div class="form-group">
                           <asp:Label ID="Label1" runat="server" Text="Observation: "></asp:Label>
                           <asp:TextBox ID="Observation" runat="server" CssClass="form-control" placeholder="Desrciption here *"></asp:TextBox>
                       </div>
                       <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Enregister" OnClick="Button1_Click" />
          
               </div>
           </div>
           <br/><br/>
             
           <div class="graph" id="table" style="background-color: transparent; height: 600px; overflow-y: auto; overflow-x: hidden;">
               <div class="tables">
                   <center>
                   <asp:GridView ID="Tableau_IT_Request_Service_modification" DataKeyNames="ServiceID" OnRowEditing="Tableau_IT_Request_Service_modification_RowEditing" OnRowCancelingEdit="Tableau_IT_Request_Service_modification_RowCancelingEdit" OnRowUpdating="Tableau_IT_Request_Service_modification_RowUpdating" runat="server" AutoGenerateColumns="False" cssClass="table" CellPadding="0"  BorderStyle="None" GridLines="Horizontal" BorderColor="#EAEAEA" >
                       <Columns>
                           <asp:TemplateField HeaderText="ID" Visible="false" SortExpression="ServiceID">

                               <ItemTemplate>
                                   <asp:Label ID="Label3" runat="server" Text='<%# Bind("ServiceID") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Service Description" SortExpression="ServiceDescription">

                                <EditItemTemplate>
                        <asp:TextBox ID="txtServiceDescription" runat="server" Text='<%# Bind("ServiceDescription")%>'></asp:TextBox>
                    </EditItemTemplate>

                               <ItemTemplate>
                                   <asp:Label ID="Label4" runat="server" Text='<%# Bind("ServiceDescription") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Observation" SortExpression="Observation">
                               <EditItemTemplate>
                        <asp:TextBox ID="txtServiceObservation" runat="server" Text='<%# Bind("Observation")%>'></asp:TextBox>
                    </EditItemTemplate>
                               
                               <ItemTemplate>
                                   <asp:Label ID="Label5" runat="server" Text='<%# Bind("Observation") %>'></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>

                            <asp:CommandField ShowEditButton="True" EditText="Editer" ControlStyle-CssClass="button12" ButtonType="Button" />
                       </Columns>
                   </asp:GridView>
                 </center>
               </div>
           </div>
       </div>

    <asp:HiddenField ID="ServiceDescriptionSet" runat="server" Value="5" Visible="true" />
    <asp:HiddenField ID="ObservationsSet" runat="server" Value="5" Visible="true" />  
    <asp:HiddenField ID="IDSet" runat="server" Value="5" Visible="true" /> 
    <asp:HiddenField ID="ActionSet" runat="server" Value="Update" Visible="true" />

      <style>
            #ContentPlaceHolder1_Tableau_IT_Request_Service_modification {
               width:100%;
            }

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

       
</asp:Content>
