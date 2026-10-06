<%@ Page Title="" Language="C#" MasterPageFile="~/Model_Page.Master" AutoEventWireup="true" CodeBehind="TRANSRequisition.aspx.cs" Inherits="KCCREQUEST2.Admin.TRANS_Request.TRANSRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

 <div class="graph" id="table" style="background-color: transparent; height: 600px; overflow-y: auto; overflow-x: auto;">
            <center><h2 class="inner-tittle">TRANSPORT - Requisitions</h2> </center>
            <div class="tables">
                 <table>
                        <tr>
                           <td width="900px"></td>
                            <td>Requisition<br>
                              
                                        <asp:TextBox runat="server" id="txtRequisitionID"  Width="250px"   class="form-control pull-right" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                                <br/>
                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click"  class="btn green" runat="server" Text="Search"/>
                            </td>

                        </tr>
                    </table>
                      <br/>
                                    <asp:GridView ID="dgvMyallrequest"  runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="Id" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                          
                                            <asp:TemplateField  HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                <HeaderTemplate>
                                                    ID
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                       <%--              <span class="badge badge-info">--%>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                      <%--      </span>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                   Request login
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestLogin" runat="server" Text='<%#Eval("RequestorLogin")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="17%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                    <asp:Literal ID="requestorName" Text="<%$ Resources:LocalizedText, requestorName%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                   
                                                    <asp:Label ID="lblRequestorName" runat="server" Text='<%#Eval("RequestorName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="10%">
                                                <HeaderTemplate>
                                                   Description
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Comment")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="10%" ItemStyle-Width="5%">
                                                <HeaderTemplate>
                                                  <asp:Literal ID="lblsdepartment" Text="<%$ Resources:LocalizedText, lblsdepartment%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                          <%--   <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                                <HeaderTemplate>
                                                    <asp:Literal ID="lblPriority" Text="<%$ Resources:LocalizedText, lblPriority%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPriorite" runat="server" Text='<%#Eval("PrioriteDescprition")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="10%" ItemStyle-Width="5%">
                                                <HeaderTemplate>
                                                  <asp:Literal ID="lblStatut" Text="<%$ Resources:LocalizedText, lblStatut%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("StatusDescription")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


<%--                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="10%">
                                                <HeaderTemplate>
                                                    <asp:Literal ID="lblPlannedStartDate" Text="<%$ Resources:LocalizedText, lblPlannedStartDate%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlannedDeliveryDate" runat="server" Text='<%# DateTime.Parse(Eval("PlannedDeliveryDate").ToString()).ToString("MM-dd-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                <HeaderTemplate>
                                                    Doc pos
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocPos" runat="server" Text='<%#Eval("DocPos")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true"  HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                <HeaderTemplate>
                                                   Current User
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrentUser" runat="server" Text='<%#Eval("CurrentUser")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                <HeaderTemplate>
                                                   Created
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                        <HeaderTemplate></HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button CssClass="btn  btn-xs btn-danger"  ID="btnDetail" OnClick="btnDetail_Click" runat="server" Text="Détail" />
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        </Columns>
                                        <SelectedRowStyle CssClass="alert-info" />
                                        <RowStyle CssClass="cursor-pointer" />
                                        <HeaderStyle CssClass="alert-success" />
                                        <PagerStyle HorizontalAlign="Center" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    </asp:GridView>
                                  

            </div>
        </div>


     <style>
        .graph-form {
                width:30%;
                float:left;
                height:598px;
            }
            #table {
                width:93%;
                float:right;
                /*top:-45px;*/
                margin-right:3%;
                margin-top:1%;
                margin-bottom:3%;
                margin-left:3%;
                overflow-x:hidden;
                height:521px;
            }
            .graph-form, #table {
            }
    </style>
    </asp:Content>

