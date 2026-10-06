<%@ Page Title="" Language="C#" MasterPageFile="~/ViewRequest.Master" AutoEventWireup="true" CodeBehind="View_DCRequest_clean.aspx.cs" Inherits="KCCREQUEST2.View_DCRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .form-field-select-1 {
            margin-bottom: 10px;
        }
        .horizontal-checkboxlist {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            margin-bottom: 5px;
        }
        .horizontal-checkboxlist label {
            margin-right: 10px;
        }
    </style>
    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
                <div class="page-header">
                    <h1>
                        <asp:Label ID="Label1" runat="server" Text="DCRequest Details"></asp:Label>
                    </h1>
                </div>
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center">
                        <h4 class="widget-title lighter">
                            <asp:Label ID="lblDetailDCRequest" runat="server" Text="Request Information"></asp:Label>
                            <span class="badge badge" style="background-color: #003468" id="title" runat="server"></span>
                        </h4>
                    </div>
                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">
                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">
                                    <table style="width: 100%; margin: auto">
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <h5 class="lighter  smaller" style="color: rgb(187,135,72); font-weight: bold">
                                                    <asp:Label ID="lblMsgHeadIT" runat="server" Text="Request Details"></asp:Label>
                                                </h5>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblDepartment" CssClass="form-field-select-1" runat="server" Text="Department:"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblRequestor" CssClass="form-field-select-1" runat="server" Text="Requestor:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtRequestor" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblBeneficiary" CssClass="form-field-select-1" runat="server" Text="Beneficiary:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtBeneficiary" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblBeneficiaryJobTitle" CssClass="form-field-select-1" runat="server" Text="Beneficiary Job Title:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtBeneficiaryJobTitle" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="lblRequestDescription" CssClass="form-field-select-1" runat="server" Text="Description:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtRequestDescription" runat="server" ReadOnly="true" Width="100%" TextMode="MultiLine" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="lblJustificationWhy" CssClass="form-field-select-1" runat="server" Text="Justification:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtJustificationWhy" runat="server" ReadOnly="true" Width="100%" TextMode="MultiLine" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblRequestType" CssClass="form-field-select-1" runat="server" Text="Request Type:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtRequestType" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="lblDocumentTitle" CssClass="form-field-select-1" runat="server" Text="Document Title:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentTitle" runat="server" ReadOnly="true" Width="100%" TextMode="MultiLine" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblService" CssClass="form-field-select-1" runat="server" Text="Service:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtService" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblDocumentLangueID" CssClass="form-field-select-1" runat="server" Text="Document Language:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentLangueID" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblDocumentType" CssClass="form-field-select-1" runat="server" Text="Document Type:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentType" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="lblDocumentVersionCode" CssClass="form-field-select-1" runat="server" Text="Document Version Code:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentVersionCode" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="lblDocumentNumber" CssClass="form-field-select-1" runat="server" Text="Document Number:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentNumber" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblCategory" CssClass="form-field-select-1" runat="server" Text="Category:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtCategory" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblPriority" CssClass="form-field-select-1" runat="server" Text="Priority:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtPriority" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <asp:Panel ID="panelUrgencyComment" Visible="true" runat="server">
                                                    <asp:Label ID="lblUrgencyComment" CssClass="form-field-select-1" runat="server" Text="Urgency Comment:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtUrgencyComment" runat="server" ReadOnly="true" Width="100%" TextMode="MultiLine" />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblCostCenter" CssClass="form-field-select-1" runat="server" Text="Cost Center:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtCostCenter" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="lblPlannedStartDate" CssClass="form-field-select-1" runat="server" Text="Planned Start Date:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtPlannedStartDate" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <!-- Applicability, Rollout, and other fields can be added similarly as needed -->
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblArea" CssClass="form-field-select-1" runat="server" Text="Area:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtArea" runat="server" ReadOnly="true" Width="90%" TextMode="MultiLine" />
                                                </div>
                                            </td>
                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="lblRisk" CssClass="form-field-select-1" runat="server" Text="Risk:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtRisk" runat="server" ReadOnly="true" Width="100%" TextMode="MultiLine" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblDocVersion" CssClass="form-field-select-1" runat="server" Text="Doc Version:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocVersion" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="lblDocumentVersionDate" CssClass="form-field-select-1" runat="server" Text="Document Version Date:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocumentVersionDate" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblDocTranslator" CssClass="form-field-select-1" runat="server" Text="Doc Translator:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocTranslator" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="lblDocumentReviewVersionDate" CssClass="form-field-select-1" runat="server" Text="Document Review Version Date:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocReviewVersionDate" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblDocHardCopy" CssClass="form-field-select-1" runat="server" Text="Doc Hard Copy:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocHardCopy" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblDocTransmittalNo" CssClass="form-field-select-1" runat="server" Text="Doc Transmittal No:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocTransmittalNo" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%; padding: 0px;" valign="top">
                                                <div>
                                                    <asp:Label ID="lblAdditionalNotedId" CssClass="form-field-select-1" runat="server" Text="Additional Noted Id:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtAdditionalNotedId" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div>
                                                    <asp:Label ID="lblDocLink" CssClass="form-field-select-1" runat="server" Text="Doc Link:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtDocLink" runat="server" ReadOnly="true" Width="100%" TextMode="MultiLine" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblStatut" CssClass="form-field-select-1" runat="server" Text="Status:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtStatut" runat="server" ReadOnly="true" Width="90%" />
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div>
                                                    <asp:Label ID="lblCurrentUsers" CssClass="form-field-select-1" runat="server" Text="Current User:"></asp:Label>
                                                    <br>
                                                    <asp:TextBox ID="txtCurrentUsers" runat="server" ReadOnly="true" Width="100%" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <!-- Approval buttons and features -->
                                    <div class="clearfix form-actions">
                                        <div>
                                            <asp:Button ID="btnApproval" class="btn btn-primary btn-sm" OnClick="btnApproval_Click" runat="server" Text="Approve" />
                                            <asp:Button ID="BtnReject" class="btn btn-danger btn-sm" runat="server" OnClick="BtnReject_Click" Text="Reject" />
                                            <asp:Button ID="BtnReturnToOrginator" class="btn btn-success btn-sm" OnClick="BtnReturnToOrginator_Click" runat="server" Text="Return" />
                                            <asp:Button ID="btnBackToOriginator" class="btn btn-purple btn-sm" OnClick="btnBackToOriginator_Click" runat="server" Text="Back to Previous" />
                                            <asp:Button ID="btnAdHoc" class="btn btn-pink btn-sm" OnClick="btnAdHoc_Click" runat="server" Text="Ad Hoc" />
                                            <asp:Button ID="btnPrint" class="btn btn-inverse btn-sm" runat="server" Text="Print" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
