<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="itrequest.ascx.cs" Inherits="KCCREQUEST2.form.itrequest" %>

  <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">

                                    <!-- entite de la requisition-->
                                    <table style="width: 100%; margin:auto">
                                        <tr>
                                            <td colspan="2" style="width:100%">
                                                 <h5 class="header smaller lighter green">
											<i class="ace-icon fa fa-user"></i>
											IT Request
										</h5>
                                            </td>
                                            <%--<td>
                                                    </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="width:50%">
                                                <div>
                                                    <label for="form-field-select-1">Departement  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="dllDepartment" OnSelectedIndexChanged="dllDepartment_SelectedIndexChanged" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width:50%">
                                                <div>
                                                    <label for="form-field-select-1">Requestor  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestor" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width:50%">
                                                <div>
                                                    <label for="form-field-select-1">Beneficiary  <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtBeneficiary" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td style="width:50%">
                                                <div>
                                                    <label for="form-field-select-1">Beneficiary Job Title  <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtBeneficiaryJobTitle" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">Request  Description  <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtRequestDescription" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">Justification why?  <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtJustificationWhy" TextMode="SingleLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width:50%">
                                                <div>
                                                    <label for="form-field-select-1">Category  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstCategory" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width:50%">
                                                <div>
                                                    <label for="form-field-select-1">Priority  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstPriority" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>


                                        <tr>
                                           

                                            <td style="width:50%">
                                                <div>
                                                    <label for="form-field-select-1">File</label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload2" Width="90%" runat="server" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                            </td>

                                             <td style="width:50%;padding:0px;"valign="top">
                                                <div>
                                                    <label for="form-field-select-1">Planned Start Date <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtPlannedStartDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <%--<td colspan="2">
                                                <div>
                                                    <label for="form-field-select-1">File</label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                            </td>--%>
                                        </tr>
                                    </table>

                                    <!--fin entete requesition-->
                                 <%--   <hr />--%>

                                    <!--bloc detail de la requisition-->

                                    <div class="form-horizontal" id="Form2" style="width: 100%;">
                                       
                                         <h5 class="row header smaller lighter green">
											<span class="col-sm-8">
												<i class="ace-icon fa fa-check"></i>
												Select item(s) requested from the list bellow
											</span>
										</h5>
                                        <!--bloc des onglets des detail-->
                                        <div class="tabbable">
                                            <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab4">
                                                <li class="active">
                                                    <a data-toggle="tab" href="#home4">Computer</a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#profile4">Telephone</a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#dropdown14">Connectivity</a>
                                                </li>
                                                <li>
                                                    <a data-toggle="tab" href="#sofware">Software</a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#network">Network</a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#Application">Application</a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#FileShare">File share</a>
                                                </li>

                                            </ul>


                                            <div class="tab-content">
                                                <!--onglet computer-->
                                                <div id="home4" class="tab-pane in active">

                                                    <asp:UpdatePanel ID="UpdatePanel100" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvComputer" OnRowDataBound="dgvComputer_RowDataBound" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblIDComputer" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemComputer" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                                                <ContentTemplate>--%>
                                                                            <asp:CheckBox ID="chkRowComputer" Checked="false" OnCheckedChanged="chkRow_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                            <%--        </ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            Quantity
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--<asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />
                                                                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                                                                </Triggers>
                                                                                <ContentTemplate>--%>
                                                                            <asp:TextBox ID="txtQteComputer" MaxLength="2" runat="server" Enabled="false" Text="0" Width="50px" TextMode="Number"></asp:TextBox>

                                                                            <%--</ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            Justification (why)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--<asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />
                                                                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                                                                </Triggers>
                                                                                <ContentTemplate>--%>
                                                                            <asp:TextBox ID="txtCommentComputer" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                                                            <%-- </ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
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
                                                <!--fin onglet computer-->

                                                <!--onglet telephone-->
                                                <div id="profile4" class="tab-pane">

                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvTelephone" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtIDTelephone" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtItemTelephone" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:CheckBox ID="chkRowTelephone" OnCheckedChanged="chkRowTelephone_CheckedChanged" Checked="false" AutoPostBack="true" runat="server" />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            Quantity
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtQteTelephone" MaxLength="2" runat="server" Enabled="false" Text="0" Width="50px" TextMode="Number"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                           Justification (why)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtCommentTelephone" Width="100%" Enabled="false" runat="server"></asp:TextBox>

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
                                                <!--FIN ONGLET TELEPHONE-->


                                                <!--ONGLET CONNECTIVITY-->
                                                <div id="dropdown14" class="tab-pane">

                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%--   <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvConnectivity" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtIDConnectivity" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtItemConnectivity" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:CheckBox ID="chkRowConnectivity" Checked="false" OnCheckedChanged="chkRowConnectivity_CheckedChanged" AutoPostBack="true" runat="server" />


                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            Quantity
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtQteConnectivity" MaxLength="2" runat="server" Enabled="false" Text="0" Width="50px" TextMode="Number"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            Justification (why)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtCommentConnectivity" Width="100%" Enabled="false" runat="server"></asp:TextBox>

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

                                                    <!--DEBUT FORMULAIRE POUR NEW USER ACCOUNT-->
                                                    <asp:UpdatePanel ID="UpdatePanel36" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="dgvConnectivity" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <div class="widget-body" id="newUserAccount" runat="server">
                                                                <div class="step-content pos-rel">
                                                                    <div class="step-pane active" data-step="1">
                                                                        <div class="widget-main">
                                                                            <div class="form-horizontal" id="Div1">

                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right"><span style="color: red">News User Account</span></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block"></span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>



                                                                                <div class="form-group">

                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Visitor: </label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:RadioButton ID="rdVisitor" runat="server" GroupName="compteType" />

                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                    <br />
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">New Start: </label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:RadioButton ID="rdNewStart" runat="server" GroupName="compteType" />
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>

                                                                                </div>


                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Departement:  <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:DropDownList ID="lstDepartment" class="width-100 form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>



                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Employee N°:  <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtEmployeeNumber" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>

                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Phone Number:  <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtPhoneNumber" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>

                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Location:  <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtLocation" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>

                                                                                <div class="form-group">
                                                                                    <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Job Title:  <b style="color: red">*</b></label>

                                                                                    <div class="col-xs-12 col-sm-6">
                                                                                        <span class="input-icon block">
                                                                                            <asp:TextBox ID="txtJobTitle" runat="server" TextMode="SingleLine" class="width-100"></asp:TextBox>
                                                                                        </span>
                                                                                    </div>
                                                                                    <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <!--fin formulaire NEW USER ACCOUNT-->

                                                </div>
                                                <!--FIN ONGLET CONNECTIVITY-->

                                                <!--ONGLET SOFTWARE-->
                                                <div id="sofware" class="tab-pane">
                                                    <asp:GridView ID="dgvSofware" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                <HeaderTemplate>
                                                                    ID
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtIDSoftware" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                <HeaderTemplate>
                                                                    Items
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtItemSoftware" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate></HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="chkRowSoftware" Checked="false" OnCheckedChanged="chkRowSoftware_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate>
                                                                    Quantity
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkRowSoftware" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtQteSoftware" MaxLength="2" runat="server" Enabled="false" Text="0" Width="50px" TextMode="Number"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate>
                                                                    Justification (why)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkRowSoftware" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCommentSoftware" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
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
                                                <!--FIN SOFTWARE-->

                                                <!--ONGLET NETWORK-->
                                                <div id="network" class="tab-pane">

                                                    <asp:GridView ID="dgvNetwork" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                <HeaderTemplate>
                                                                    ID
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtIDNetwork" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                <HeaderTemplate>
                                                                    Items
                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtItemNetwork" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate></HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="chkRowNetwork" Checked="false" OnCheckedChanged="chkRowNetwork_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate>
                                                                    Quantity
                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:UpdatePanel ID="UpdatePanel31" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkRowNetwork" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtQteNetwork" MaxLength="2" runat="server" Enabled="false" Text="1" Width="50px" TextMode="Number"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                <HeaderTemplate>
                                                                    Justification (why)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel32" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkRowNetwork" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtCommentNetwork" Width="100%" Enabled="false" runat="server"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
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
                                                <!--FIN ONGLET NETWORK-->

                                                <!--ONGLET APPLICATION-->
                                                <div id="Application" class="tab-pane">
                                                    <asp:UpdatePanel ID="UpdatePanel300" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvApplication" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtIDApplication" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtItemApplication" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkRowApplication" Checked="false" OnCheckedChanged="chkRowApplication_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            Quantity
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtQteApplication" runat="server" Enabled="true" Text="1" Width="50px" TextMode="Number"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            Justification (why)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtCommentApplication" Width="100%" Enabled="true" runat="server"></asp:TextBox>
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


                                                    <!--DEBUT FORMULAIRE POUR APPLICATION-->
                                                    <div class="widget-body">
                                                        <div class="step-content pos-rel">
                                                            <div class="step-pane active" data-step="1">
                                                                <div class="widget-main">
                                                                    <div class="form-horizontal" id="Form1">

                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right"><span style="color: red">All fields are required</span></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block"></span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>


                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Application Name:  <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:DropDownList ID="ddlApplicationName" class="width-100 form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>



                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Application Responsible:  <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:DropDownList ID="ddlApplicationResponsible" class="width-100 form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>



                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Application Description:  <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:TextBox ID="txtApplicationDescription" runat="server" TextMode="MultiLine" class="width-100"></asp:TextBox>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Application Raison:  <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:TextBox ID="txtApplicationRaison" runat="server" TextMode="MultiLine" class="width-100"></asp:TextBox>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label for="inputError2" class="col-xs-12 col-sm-4 control-label no-padding-right">Application Sponsor:  <b style="color: red">*</b></label>

                                                                            <div class="col-xs-12 col-sm-6">
                                                                                <span class="input-icon block">
                                                                                    <asp:DropDownList ID="ddlApplicationSponsor" class="width-100 form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                                                </span>
                                                                            </div>
                                                                            <div class="help-block col-xs-12 col-sm-reset inline">&nbsp;</div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <!--fin formulaire application-->
                                                </div>
                                                <!--fin onglet application-->


                                                 <!--onglet fileShare-->
                                                <div id="FileShare" class="tab-pane">

                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:GridView ID="dgvFileShare" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="ItemID" AllowPaging="false" PageSize="5" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="ForestGreen" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                        <HeaderTemplate>
                                                                            ID
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtIDTelephone" runat="server" Text='<%#Eval("ItemID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                        <HeaderTemplate>
                                                                            Items
                                                
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtItemTelephone" runat="server" Text='<%#Eval("ItemDescription")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:CheckBox ID="chkRowTelephone"  Checked="false" AutoPostBack="true" runat="server" />

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            Quantity
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtQteTelephone" MaxLength="2" runat="server" Enabled="false" Text="0" Width="50px" TextMode="Number"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" ItemStyle-Width="40%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                           v
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtCommentTelephone" Width="100%" Enabled="false" runat="server"></asp:TextBox>

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
                                                <!--FIN ONGLET fileShare-->

                                            </div>

                                        </div>
                                        <!--fin bloc onglet detail-->
                                        <br />
                                    </div>

                                    <!-- button pour l'enregistrement et l'initialisation des données-->
                                    <div class="clearfix form-actions">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnSave" class="btn btn-info" OnClick="btnSave_Click" runat="server" Text="Save" />
                                            &nbsp; &nbsp; &nbsp;
											 <asp:Button ID="btnReset" OnClick="btnReset_Click" class="btn btn-danger" runat="server" Text="Reset" />

                                        </div>

                                    </div>
                                    <!--fin zone de button-->
                                    <br />
                                </div>

  <!--popup pour le message d'erreur-->


    <div id="my-modal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter blue no-margin">Invalid Input</h3>
                </div>

                <div class="modal-body">

                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <%--  <asp:AsyncPostBackTrigger ControlID="chkRowComputer" />--%>
                            <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                        </Triggers>
                        <ContentTemplate>
                            <h4 class="smaller lighter red no-margin">
                                <asp:Label ID="lblErreur" runat="server" Text=""></asp:Label>

                            </h4>



                            <asp:GridView ID="GridView1" runat="server"></asp:GridView>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="modal-footer">
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

    </script>