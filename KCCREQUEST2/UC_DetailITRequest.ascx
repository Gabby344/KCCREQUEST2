<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_DetailITRequest.ascx.cs" Inherits="KCCREQUEST2.UC_DetailITRequest" %>
  <script type="text/javascript">
         function DisableButton() {

             if (document.getElementById("<%=btnSave.ClientID %>")) {

                 document.getElementById("<%=btnSave.ClientID %>").disabled = true;
             }

             if (document.getElementById("<%=btnUpdate.ClientID %>")) {

                 document.getElementById("<%=btnUpdate.ClientID %>").disabled = true;
             }
          }
          window.onbeforeunload = DisableButton;
    </script>

    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1>IT REQUEST
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    <%--Detail Request--%>
                                </small>
                    </h1>
                </div>
                <!--FIN TITRE DE LA PAGE-->



                <!--bloc pour les information du requisitionneur-->
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center;">
                        <h4 class="widget-title lighter">
                          
                             <asp:Literal ID="detailITtitle" Text="<%$ Resources:LocalizedText, detailITtitle%>" runat="server"></asp:Literal>
                            <span class="badge badge-info" id="title" runat="server"></span></h4>
                    </div>

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">

                                    <!-- entite de la requisition-->
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2" style="width:100%">
                                                <h5 class="header smaller lighter green">
											<i class="ace-icon fa fa-user"></i>
											<asp:Label ID="lblMsgHeadIT" runat="server" Text="<%$ Resources:LocalizedText, lblMsgHeadIT %>"></asp:Label>
										</h5>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:50%">
                                                <div>
                                                    <asp:Label ID="lblRequestor" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblRequestor %>"></asp:Label><b style="color: red"> *</b>
                                                    <br/>
                                                    <asp:DropDownList ID="LstRequestor" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:50%">
                                                <div>
                                                  <asp:Label ID="lblBeneficiary" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblBeneficiary %>"></asp:Label><b style="color: red"> *</b>
                                                    <br/>
                                                    <asp:TextBox ID="txtBeneficiary" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width:50%">
                                                <div>
                                                     <asp:Label ID="lblBeneficiaryJobTitle" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblBeneficiaryJobTitle %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtBeneficiaryJobTitle" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                     <asp:Label ID="lblRequestDescription" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblRequestDescription %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtRequestDescription" runat="server" Width="100%" ></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="lblJustificationWhy" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblJustificationWhy %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtJustificationWhy"  runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <%--<td style="width:50%">
                                                <div>
                                                    <asp:Label ID="lblCategory" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblCategory %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:DropDownList ID="LstCategory" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>--%>

                                            <td style="width:50%">
                                                <div>
                                                      <asp:Label ID="lblPriority" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblPriority %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:DropDownList ID="LstPriority" class="form-control" Width="90%" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LstPriority_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:Panel ID="panelUrgencyComment" Visible="false" runat="server">
                                                    <asp:Label ID="lblUrgencyComment" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblUrgencyComment %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtUrgencyComment" runat="server" Width="100%" ></asp:TextBox>
                                                </asp:Panel>
                                            </td>
                                        </tr>


                                        <%--<tr>
                                            <td style="width:50%">
                                                <div>
                                                    <asp:Label ID="lblPlannedStartDate" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblPlannedStartDate %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtPlannedStartDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>--%>

                                        <tr>
                                            <td colspan="2" style="width:100%;">
                                                <br/>
                                                <div class="col-sm-12">
										<div class="tabbable tabs-below">
											<div class="tab-content">
												<div id="home2" class="tab-pane in active">
													<asp:GridView ID="dgvFiles" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="FileID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <HeaderTemplate>
                                                                            File ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtFileID" runat="server" Text='<%#Eval("FileID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="80%" ItemStyle-Width="80%">
                                                                        <HeaderTemplate>
                                                                         <asp:Literal ID="lbdgvFile" Text="<%$ Resources:LocalizedText, lbdgvFile%>" runat="server"></asp:Literal>
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtFileName" runat="server" Text='<%#Eval("FileName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                        <HeaderTemplate>
                                                                          Type
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  
                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <HeaderTemplate>
                                                                           File
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="File" runat="server" Text='<%#Eval("File")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                        <HeaderTemplate></HeaderTemplate>
                                        <ItemTemplate>

                                            <asp:Button CssClass="btn  btn-xs btn-danger" OnClick="btnview_Click"  ID="btnview" runat="server" Text="<%$ Resources:LocalizedText, lblView%>"	/>
                                            
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

											<ul class="nav nav-tabs" id="myTab2">
												<li class="active">
												<%--	<a data-toggle="tab" href="#home2">File</a>--%>
												</li>
											</ul>
										</div>
                                                    </div>
                                            </td>
                                        </tr>
                                    </table>

                                    <!--fin entete requesition-->
                                    

                                    <!--bloc detail de la requisition-->

                                    <div class="form-horizontal" id="Form2" style="width: 100%;">
                                        <h5 class="row header smaller lighter green">
											<span class="col-sm-8">
												<i class="ace-icon fa fa-check"></i>
												
                                                 <asp:Literal ID="detailmsgIT" Text="<%$ Resources:LocalizedText, detailmsgIT%>" runat="server"></asp:Literal>
											</span>
										</h5>
                                        <!--bloc des onglets des detail-->
                                        <div class="tabbable">
                                            <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab4">
                                                <li class="active">
                                                    <a data-toggle="tab" href="#home4">
                                                        
                                                       	
                                                 <asp:Literal ID="ZoneDetail" Text="<%$ Resources:LocalizedText, ZoneDetail%>" runat="server"></asp:Literal>


                                                    </a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#profile4">
                                                        
                                                      <asp:Literal ID="zoneHistory" Text="<%$ Resources:LocalizedText, zoneHistory%>" runat="server"></asp:Literal>


                                                    </a>
                                                </li>
                                            </ul>


                                            <div class="tab-content">
                                                <!--onglet details-->
                                                <div id="home4" class="tab-pane in active">
                                                    <div>
                                                        <p>
                                                            <asp:Button ID="selectall" runat="server" OnClick="Selectall_Click" Text="<%$ Resources:LocalizedText, selectall%>" class="btn btn-white btn-purple btn-sm" />
                                                            <asp:Button ID="Deselectall" runat="server" OnClick="Deselectall_Click" Text="<%$ Resources:LocalizedText, Deselectall%>"  class="btn btn-white btn-inverse btn-sm" />
                                                            <asp:Button ID="Inverse" runat="server" OnClick="Inverse_Click" Text="<%$ Resources:LocalizedText, Inverse%>" class="btn btn-white btn-pink btn-sm" />
                                                        </p>
                                                    </div>

                                                    <asp:UpdatePanel ID="UpdatePanel1005" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnApproval" />
                                                        <%--    <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                            <asp:AsyncPostBackTrigger ControlID="btnReject" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnReturnToOrginator" />
                                                            <asp:AsyncPostBackTrigger ControlID="selectall" />
                                                            <asp:AsyncPostBackTrigger ControlID="Deselectall" />
                                                            <asp:AsyncPostBackTrigger ControlID="Inverse" />
                                                            
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvDetails" OnRowDataBound="dgvDetails_RowDataBound" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:CheckBox ID="chkRow" Checked="false" AutoPostBack="false" runat="server" />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID details
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIDDetail" runat="server" Text='<%#Eval("DetailID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            IDitems
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIDitems" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>




                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                             <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtQteItems" MaxLength="2" runat="server" Enabled="false" Width="50px" TextMode="Number" Text='<%#Eval("Quantity")%>'></asp:TextBox>


                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                           Justification
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtCommentItem" Width="100%" Enabled="true" ReadOnly="true" runat="server" Text='<%#Eval("CommentItem")%>' ></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                         
                                                                              <asp:Literal ID="lbdgvCurrentUser" Text="<%$ Resources:LocalizedText, lbdgvCurrentUser%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtCurrentUser" Width="100%" Enabled="false" runat="server" Text='<%#Eval("CurrentUser")%>' ></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            Visibility
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVisibility" runat="server" Text='<%#Eval("Visibility")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>



                                                                      <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                           SysAid
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtSysAid" MaxLength="2" runat="server" Enabled="false" Width="50px" TextMode="Number" Text='<%#Eval("SysAidID")%>'></asp:TextBox>


                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                           Statut id
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtStatusID" MaxLength="2" runat="server" Enabled="false" Width="50px" TextMode="Number" Text='<%#Eval("StatusID")%>'></asp:TextBox>


                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                       <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                           Statut
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtStatusDescription"  runat="server" Enabled="false"  Text='<%#Eval("StatusDescription")%>'></asp:TextBox>


                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <SelectedRowStyle CssClass="alert-info" />
                                                                <RowStyle CssClass="cursor-pointer" />
                                                                <HeaderStyle CssClass="alert-success" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--fin onglet details-->

                                                <!--onglet history-->
                                                <div id="profile4" class="tab-pane">

                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnApproval" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnReject" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnReturnToOrginator" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvHistory" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <HeaderTemplate>
                                                                            Item
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtItemsDescription" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                        <HeaderTemplate>
                                                                           Action
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtAction" runat="server" Text='<%#Eval("Action")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                        <HeaderTemplate>
                                                                     
                                                 <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtComment" runat="server" Text='<%#Eval("Comment")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  
                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <HeaderTemplate>
                                                                     
                                                  <asp:Literal ID="lbdgvRole" Text="<%$ Resources:LocalizedText, lbdgvRole%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtRole" runat="server" Text='<%#Eval("Role")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                        <HeaderTemplate>
                                                                         <asp:Literal ID="lbdgvCreateDate" Text="<%$ Resources:LocalizedText, lbdgvCreateDate%>" runat="server"></asp:Literal>
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtCreateDate" runat="server" Text='<%# DateTime.Parse(Eval("CreationDate").ToString()).ToString("MM-dd-yyyy HH:mm:ss") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                     <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                        <HeaderTemplate>
                                                                             <asp:Literal ID="lbdgvCreateBy" Text="<%$ Resources:LocalizedText, lbdgvCreateBy%>" runat="server"></asp:Literal>
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtCreatedBy" runat="server" Text='<%#Eval("CreatedBy")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <SelectedRowStyle CssClass="alert-info" />
                                                                <RowStyle CssClass="cursor-pointer" />
                                                                <HeaderStyle CssClass="alert-success" />
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--FIN ONGLET history-->



                                            </div>

                                        </div>
                                        <!--fin bloc onglet detail-->
                                        <br />
                                    </div>

                                    <!-- button pour l'enregistrement et l'initialisation des données-->
                                    <div class="clearfix form-actions" id="blockbtn" runat="server">
                                        <div class="col-md-offset-2 col-md-12">
                                          <%--  <asp:UpdatePanel ID="UpdatePanel152" runat="server" UpdateMode="Conditional">

                                                <ContentTemplate>--%>
                                                    <asp:Button ID="btnUpdate" OnClick="btnUpdate_Click"  class="btn btn-info" runat="server" Text="<%$ Resources:LocalizedText, btn_Update_Text%>"	 />
                                                    &nbsp; &nbsp; &nbsp;
                                                    <asp:Button ID="btnApproval" OnClick="btnApproval_Click" class="btn btn-success" runat="server" Text="<%$ Resources:LocalizedText, btn_Approve_Text%>"	 />
                                                    &nbsp; &nbsp; &nbsp;
											 <asp:Button ID="BtnReject" OnClick="BtnReject_Click" class="btn btn-danger" runat="server" Text="<%$ Resources:LocalizedText, btn_Reject_Text%>"	/>
                                                    &nbsp; &nbsp; &nbsp;
											 <asp:Button ID="BtnReturnToOrginator" OnClick="BtnReturnToOrginator_Click" class="btn btn-primary" runat="server" Text="<%$ Resources:LocalizedText, btn_Return_Text%>"	 />
                                              <%--  </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>

                                    </div>
                                    <!--fin zone de button-->
                                    <br />
                                </div>

                            </div>
                            <!-- /.widget-main -->
                        </div>
                        <!-- /.widget-body -->
                    </div>
                </div>

            </div>
        </div>
    </div>


    <!--popup pour comment-->


    <div id="my-modal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter blue no-margin">IT Request</h3>
                    <asp:Label ID="lblaction" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">


                    <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                 <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>
                                
                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtComment"  runat="server" Width="100%"></asp:TextBox>
                        </div>

                    </h4>

                </div>

                <div class="modal-footer">
                   

                    <asp:Button ID="btnSave" OnClick="btnSave_Click" class="btn btn-sm btn-info" runat="server" Text="OK" />
                            

                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                         Close
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!--fin popup-->



    <!--popup pour le message d'erreur-->


    <div id="modal_erreur" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter blue no-margin">
                        <asp:Label ID="txtInvalidInput" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, txtInvalidInput %>"></asp:Label>
                    </h3>
                </div>

                <div class="modal-body">

                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <%--  <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />--%>
                            <asp:AsyncPostBackTrigger ControlID="btnApproval" />
                            <asp:AsyncPostBackTrigger ControlID="BtnReject" />
                           
                            <asp:AsyncPostBackTrigger ControlID="BtnReturnToOrginator" />
                        </Triggers>
                        <ContentTemplate>
                            <h4 class="smaller lighter red no-margin">
                                <asp:Label ID="lblErreur" runat="server" Text=""></asp:Label>

                            </h4>





                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        <asp:Literal ID="Literal2" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!--fin popup-->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <script src="assets/js/jquery.MultiFile.js"></script>

    <script type="text/javascript">
        if ('ontouchstart' in document.documentElement) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
    </script>
    <script src="assets/js/bootstrap.min.js"></script>

    <script src="assets/js/jquery-ui.custom.min.js"></script>
    <script src="assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="assets/js/chosen.jquery.min.js"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            if (!ace.vars['touch']) {
                $('.chosen-select').chosen({ allow_single_deselect: true });



            }
        });
    </script>

    <!-- page specific plugin scripts -->

    <!--[if lte IE 8]>
		  <script src="assets/js/excanvas.min.js"></script>
		<![endif]-->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <script src="assets/js/jquery-ui.custom.min.js"></script>
    <script src="assets/js/jquery.ui.touch-punch.min.js"></script>
    <script src="assets/js/chosen.jquery.min.js"></script>
    <script src="assets/js/spinbox.min.js"></script>
    <script src="assets/js/bootstrap-datepicker.min.js"></script>
    <script src="assets/js/bootstrap-timepicker.min.js"></script>
    <script src="assets/js/moment.min.js"></script>
    <script src="assets/js/daterangepicker.min.js"></script>
    <script src="assets/js/bootstrap-datetimepicker.min.js"></script>
    <script src="assets/js/bootstrap-colorpicker.min.js"></script>
    <script src="assets/js/jquery.knob.min.js"></script>
    <script src="assets/js/autosize.min.js"></script>
    <script src="assets/js/jquery.inputlimiter.min.js"></script>
    <script src="assets/js/jquery.maskedinput.min.js"></script>
    <script src="assets/js/bootstrap-tag.min.js"></script>

    <!-- ace scripts -->
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>

    <!-- inline scripts related to this page -->


    <script type="text/javascript">


        jQuery(function ($) {



            //datepicker plugin
            //link
            $('.date-picker').datepicker({
                autoclose: true,
                todayHighlight: true
            })
            //show datepicker when clicking on the icon
            .next().on(ace.click_event, function () {
                $(this).prev().focus();
            });

            //or change it into a date range picker
            $('.input-daterange').datepicker({ autoclose: true });


            //to translate the daterange picker, please copy the "examples/daterange-fr.js" contents here before initialization
            $('input[name=date-range-picker]').daterangepicker({
                'applyClass': 'btn-sm btn-success',
                'cancelClass': 'btn-sm btn-default',
                locale: {
                    applyLabel: 'Apply',
                    cancelLabel: 'Cancel',
                }
            })
            .prev().on(ace.click_event, function () {
                $(this).next().focus();
            });


            $('#timepicker1').timepicker({
                minuteStep: 1,
                showSeconds: true,
                showMeridian: false,
                disableFocus: true,
                icons: {
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down'
                }
            }).on('focus', function () {
                $('#timepicker1').timepicker('showWidget');
            }).next().on(ace.click_event, function () {
                $(this).prev().focus();
            });




            if (!ace.vars['old_ie']) $('#date-timepicker1').datetimepicker({
                //format: 'MM/DD/YYYY h:mm:ss A',//use this option to display seconds
                icons: {
                    time: 'fa fa-clock-o',
                    date: 'fa fa-calendar',
                    up: 'fa fa-chevron-up',
                    down: 'fa fa-chevron-down',
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-arrows ',
                    clear: 'fa fa-trash',
                    close: 'fa fa-times'
                }
            }).next().on(ace.click_event, function () {
                $(this).prev().focus();
            });
        });
    </script>


    <script type="text/javascript">
        function openModal() {
            $('#my-modal').modal('show');
        }
        function hideModal() {
            $('#my-modal').modal('hide');
        }
        function openModalErreur() {
            $('#modal_erreur').modal('show');
        }

    </script>



