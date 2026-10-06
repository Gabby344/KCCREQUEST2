<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sysaid.ascx.cs" Inherits="KCCREQUEST2.form.sysaid" %>
  <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">


                                    <!-- entete de la requisition-->
                                    <table style="width: 100%; margin: auto">
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <h5 class="lighter block green">Please fill in the form and click save when ready </h5>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Sous type  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstSousType" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Description de la liste rapide  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstDescriptionListeRapide" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Catégorie <b style="color: red">*</b></label><br />
                                                   <asp:DropDownList ID="LstCategorie" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Titre <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtTitre" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                            
                                        </tr>

                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <div>
                                                    <label for="form-field-select-1">Description <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtDescription" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <div>
                                                    <label for="form-field-select-1">Remarques  <b style="color: red">*</b></label><br />
                                                    <asp:TextBox ID="txtRemarques" TextMode="SingleLine" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Statut <b style="color: red">*</b></label><br />
                                                    <asp:DropDownList ID="LstStatut" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </td>
                                        
                               
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Urgence  <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstUrgence" class="form-control" Width="  100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>
                                            <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Impact <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstImpact" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                                 <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Demandeur <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstDemandeur" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>


                                         <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Contact Number <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:TextBox ID="txtContactNumber" TextMode="Phone" runat="server" Width="90%"></asp:TextBox>
                                                </div>
                                            </td>

                                                 <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Attribué à <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstAttribuer" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <label for="form-field-select-1">Groupe d'admin <b style="color: red">*</b></label>
                                                    <br>
                                                    <asp:DropDownList ID="LstGroupeAdmin" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">File</label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload2" Width="100%" runat="server" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>

                                           <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Emplacement <b style="color: red">*</b></label>
                                                    <br>
                                                     <asp:DropDownList ID="LstEmplacement" class="form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>

                                                 <td style="width: 50%">
                                                <div>
                                                    <label for="form-field-select-1">Department<b style="color: red">*</b></label>
                                                    <br>
                                                     <asp:DropDownList ID="LstDepartment" class="form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div>
                                            </td>
                                        </tr>

                                    </table>

                                    <!--fin entete requesition-->
                                    <hr />
