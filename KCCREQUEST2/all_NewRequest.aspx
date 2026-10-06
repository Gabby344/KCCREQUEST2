<%@ Page Title="" Language="C#" MasterPageFile="~/NewRequest.Master" AutoEventWireup="true" CodeBehind="all_NewRequest.aspx.cs" Inherits="KCCREQUEST2.all_NewRequest" %>
<%@ Register Src="~/form/itrequest.ascx" TagPrefix="nc" TagName="TestControl1" %>
<%@ Register Src="~/form/dskrequest.ascx" TagPrefix="nc" TagName="TestControl2" %>
<%@ Register Src="~/form/trainingRequest.ascx" TagPrefix="nc" TagName="TestControl3" %>
<%@ Register Src="~/form/ewoRequest.ascx" TagPrefix="nc" TagName="TestControl4" %>
<%@ Register Src="~/form/RecrutmentRequest.ascx" TagPrefix="nc" TagName="TestControl5" %>
<%@ Register Src="~/form/sysaid.ascx" TagPrefix="nc" TagName="TestControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1>Request
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    New Request
                                </small>
                    </h1>
                </div>
                <!--FIN TITRE DE LA PAGE-->
                <!--bloc pour les information du requisitionneur-->
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align:left;">
                        <h4 class="widget-title lighter">

                        <asp:DropDownList AutoPostBack="true" ID="lstModule" class="chosen-select form-control" Width="20%"  runat="server" OnSelectedIndexChanged="lstModule_SelectedIndexChanged"></asp:DropDownList>
                        </h4>
                    </div>

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">


                                <asp:Panel ID="panelform" runat="server"></asp:Panel>



                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
