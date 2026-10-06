<%@ Page Title="" Language="C#" MasterPageFile="~/Model_Page.Master" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeBehind="TrainingIntegrate.aspx.cs" Inherits="KCCREQUEST2.Admin.TRN_Request.TrainingIntegrate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
<div class="graph" id="table" style="background-color: transparent; height: 600px; overflow-y: auto; overflow-x: hidden;">
            <center><h2 class="inner-tittle">Integration TMS</h2> </center>
            <div class="tables">
                <asp:Panel runat="server">
                 
                                    <asp:GridView ID="dgvMyallrequest"  runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="Id" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                  ID
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("DetailID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="9%">
                                                <HeaderTemplate>
                                                  Training Request ID
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTRAININGID" runat="server" Text='<%#Eval("TRNRequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                   Employee ID
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeID" runat="server" Text='<%#Eval("EmployeeID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="17%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                      Employee Name
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                   
                                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Eval("EmployeeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="7%">
                                                <HeaderTemplate>
                                                    Cours
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCours" runat="server" Text='<%#Eval("CourseTMS")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="16%">
                                        <HeaderTemplate>

                                            
                                                            <asp:Button ID="selectall" runat="server" OnClick="Selectall_Click" Text="<%$ Resources:LocalizedText, selectall%>" class="btn btn-white btn-purple btn-xs" />
                                                            <asp:Button ID="Deselectall" runat="server" OnClick="Deselectall_Click" Text="<%$ Resources:LocalizedText, Deselectall%>"  class="btn btn-white btn-inverse btn-xs" />
         <%--                                                   <asp:Button ID="Inverse" runat="server" OnClick="Inverse_Click" Text="<%$ Resources:LocalizedText, Inverse%>" class="btn btn-white btn-pink btn-xs" />--%>
                                                    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                               <asp:CheckBox ID="chkRow" Checked="false" AutoPostBack="false" runat="server" />
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        </Columns>
                                        <SelectedRowStyle CssClass="alert-info" />
                                        <RowStyle CssClass="cursor-pointer" />
                                        <HeaderStyle CssClass="alert-success" />
                                        <PagerStyle HorizontalAlign="Center" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                    </asp:GridView>

                    <div style="display:none">
                       <asp:GridView ID="dgIntegration" Visible="true"  runat="server" ShowHeader="true" ShowFooter="true"  AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                           
                                          <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="9%">
                                                <HeaderTemplate>
                                                  Training Request ID
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTRAININGID2" runat="server" Text='<%#Eval("TRNRequestID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                   Employee ID
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmployeeID2" runat="server" Text='<%#Eval("EmployeeID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="17%" ItemStyle-Width="8%">
                                                <HeaderTemplate>
                                                      Employee Name
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                   
                                                    <asp:Label ID="lblEmployeeName2" runat="server" Text='<%#Eval("EmployeeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="7%">
                                                <HeaderTemplate>
                                                    Cours
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCours2" runat="server" Text='<%#Eval("Course")%>'></asp:Label>
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
                        </asp:Panel>
                                  

            </div>

     
        </div>

       <div style="margin-left:90%;">
                                        <div >
                                            <asp:Button ID="btnSave" class="btn btn-success" OnClick="btnSave_Click" runat="server" Text="<%$ Resources:LocalizedText, btnSave%>" />
                                            &nbsp; &nbsp; &nbsp;
											 

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


