<%@ Page Title="" Language="C#" MasterPageFile="~/NewRequest.Master" AutoEventWireup="true" CodeBehind="New_MCRequest_S.aspx.cs" Inherits="KCCREQUEST2.New_MCRequest_S" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
    .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
       .btn2 {
display: inline-block;
padding: 9px 12px;
padding-top: 7px;
margin-bottom: 0;
font-size: 14px;
line-height: 20px;
color: #5e5e5e;
text-align: center;
vertical-align: middle; 
cursor: pointer;
background-color: #d1dade;

background-image: none !important;
border: none;
text-shadow: none;
box-shadow: none;
transition: all 0.12s linear 0s !important;
font: 14px/20px "Helvetica Neue",Helvetica,Arial,sans-serif;
}
.btn2-cons {
margin-right: 5px;
min-width: 80px;
margin-bottom: 8px;
}
       .btn2-btnSave {
color: #fff;
border-color: #357ebd;
        background-color:#003468;
        }
         .btn2-btnReset {
color: #fff;
border-color: #357ebd;
        background-color:rgb(0,177,147);
        }
         .mid{
             text-align:center;
         }
    </style>

     <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;
            document.getElementById("<%=btnReset.ClientID %>").disabled = true;

                document.getElementById("<%=btnOk.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <div class="main-content">
        <div class="main-content-inner">
            <div class="page-content">
           <%--     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


                <!--TITRE DE LA PAGE-->
                <div class="page-header">
                    <h1><asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, lblITRequest%>"></asp:Label>
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    <%-- New Request--%>
                                </small>
                    </h1>
                </div>
                <!--FIN TITRE DE LA PAGE-->


                <!--bloc pour les information du requisitionneur-->
                <div class="widget-box" id="blocitrequest">
                    <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center;">
                        <h4 class="widget-title lighter">
                            <asp:Label ID="lblBonEntree" runat="server" Text="<%$ Resources:LocalizedText, lblBonsortie%>"></asp:Label>
                        </h4>
                    </div>



                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">

                       <br />
                          
    <table style="width: 100%; margin: auto">
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <h5 class="lighter  smaller" style="color:rgb(187,135,72);font-weight:bold">
                                              
                                                    <%--The items on this request are to be allocated to :--%>
                                                    <asp:Label ID="lblMsgHeadIT" runat="server" Text="<%$ Resources:LocalizedText, lblMsgHeadIT %>"></asp:Label>
                                                </h5>
                                            </td>
                                            <%--<td>
                                                    </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblDepartment" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblDepartment %>"></asp:Label><b style="color: red"> *</b>
                                                    <br />
                                                    <asp:DropDownList ID="dllDepartment" OnSelectedIndexChanged="dllDepartment_SelectedIndexChanged" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div> <br> 
                                            </td>

                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblRequestor" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblRequestor %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:DropDownList ID="LstRequestor" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                </div> <br> 
                                            </td>

                                        </tr>





                                           <tr>
                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    
                                                    <asp:Label ID="lblCompanys" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblCompany %>"></asp:Label><b style="color: red"> *</b>
                                                    <br />
                                                    <asp:DropDownList ID="LstCompanyName"  class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                   <%--<asp:DropDownList ID="" class="width-100 form-control" runat="server" AutoPostBack="false" Enabled="true"></asp:DropDownList>--%>
                                                      </div> <br> 
                                            </td>

                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                  
                                                    <asp:Label ID="lblSites" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblSite %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                     <asp:DropDownList ID="LstSite" class="chosen-select form-control" Width="100%" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="LstSite_SelectedIndexChanged"></asp:DropDownList>
                                                      <%--<asp:DropDownList ID="" class="chosen-select form-control" Width="90%" runat="server" AutoPostBack="false"></asp:DropDownList>--%>
                                                </div> <br> 
                                            </td>

                                        </tr>




                                          <tr>

                                                   <td style="width: 100%"  colspan="2">

                                                    <div><asp:Label ID="lblreasons" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblreason %>"></asp:Label><b style="color: red"> *</b>    <br>                                      
                                                       <asp:TextBox ID="txtreason" onkeypress="if (this.value.length > 100) { return false; }" runat="server"  class="width-100" Enabled="true" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                           
                                            </td>
                                          <%--    <td style="width: 50%">

                                           
                                           
                                            </td>--%>

                                       
                                        </tr>



                                    </table><br>
                                    



                                    
                                    <table style="width: 100%; margin: auto">
                                        <tr>
                                            <td colspan="2" style="width: 100%">
                                                <h5 class="lighter  smaller" style="color:rgb(187,135,72);font-weight:bold">
                                              
                                                    <%--The items on this request are to be allocated to :--%>
                                                    <asp:Label ID="lblDetailEscorters" runat="server" Text="<%$ Resources:LocalizedText, lblDetailEscorter %>"></asp:Label>
                                                </h5>
                                            </td>
                                          <%--  <td>   
                                                    </td>--%>
                                        </tr>

                                                       <tr>
                                            <td  style="width: 50%">
                                                 <div><asp:Label ID="lblEmployerId" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblEmployerId %>"></asp:Label><b style="color: red"> *</b>    <br>                                      
                                                       <asp:TextBox ID="txtEmployerId" onkeypress="if (this.value.length > 100) { return false; }" runat="server"  class="width-90" Enabled="true"></asp:TextBox>
                                                </div>  <br>   
                                            </td>
                                           <td style="width: 50%">

                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblEscoters" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblEscoter %>"></asp:Label><b style="color: red"> *</b>
                                                    <br />
                                                   <asp:TextBox ID="txtEscoter" onkeypress="if (this.value.length > 100) { return false; }" runat="server" class="width-100" Enabled="true"></asp:TextBox>
                                                </div> <br> 
                                            </td>
                                        </tr>



                                           


                                        <tr>


                                  

                                            <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblFunctionEscoters" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblFunctionEscoter %>"></asp:Label><b style="color: red"> *</b>
                                                    <br>
                                                    <asp:TextBox ID="txtFunctionEscoter" onkeypress="if (this.value.length > 100) { return false; }" runat="server" class="width-90" Enabled="true"></asp:TextBox>
                                                </div> <br> 
                                            </td>

                                                   <td style="width: 50%">
                                                <div class="form-field-select-1">
                                                    <asp:Label ID="lblStartDates" CssClass="form-field-select-1" runat="server" Text="<%$ Resources:LocalizedText, lblEndDate %>"></asp:Label><b style="color: red"> *</b>
                                                    <br /> <asp:TextBox ID="StartDate" class="form-control date-picker" data-date-format="mm-dd-yyyy" runat="server" Width="100%"></asp:TextBox>
                                             
                                                </div> <br> 
                                            </td>
                                          

                                        </tr>





                                        
                                         </table>
                                    <br />

                                       <br />
                                    <div id="DivDetail" runat ="server" visible="false">
                                     <table style="width: 100%; margin: auto">
                                         <tr>
                                            <asp:Literal ID="lblReseach" Text="<%$ Resources:LocalizedText, lblReseach%>" runat="server"></asp:Literal>

                                         </tr>
                                       
                                    
                                    <tr>
                                            <td colspan="2" style="width: 100%">
                                                <div>
                                                  

                                                    <asp:TextBox ID="txtSubject"   MaxLength="70" runat="server" Width="30%" ></asp:TextBox>
                                                   <asp:Button ID="Reseach" cssclass="btn2 btn2-btnReset btn2-cons"  OnClick="Seach_Click"  runat="server" Text="<%$ Resources:LocalizedText, btnSearch%>" />

                                                   
                                                </div>
                                            </td>
                                            
                                        </tr>
                                         </table>


                                           <div class="form-horizontal" id="Form2" style="width: 100%;">

                                        <h5 class="row smaller lighter" style="color:rgb(187,135,72);font-weight:bold">
                                            <span class="col-sm-8">
                                               

                                                <asp:Label ID="lblMsgDetailIT" runat="server" Text="<%$ Resources:LocalizedText, lblMsgDetailIT%>"></asp:Label>
                                            </span>
                                        </h5>
                                        <!--bloc des onglets des detail-->
                                        <div class="tabbable">
                                            <ul class="nav nav-tabs padding-12 tab-color-blue background-blue" id="myTab4">
                                                <li class="active">
                                                    <a data-toggle="tab" href="#home4">
                                                        <asp:Literal ID="computer" Text="Items" runat="server"></asp:Literal></a>
                                                </li>
                                            </ul>


                                            <div class="tab-content">
                                                <!--onglet computer-->
                                                <div id="home4" class="tab-pane in active">


                                                                                                       <div>
                                                        <p>
                                                            <asp:Button ID="selectall" runat="server" OnClick="Selectall_Click" Text="<%$ Resources:LocalizedText, selectall%>" class="btn btn-white btn-purple btn-sm" />
                                                            <asp:Button ID="Deselectall" runat="server" OnClick="Deselectall_Click" Text="<%$ Resources:LocalizedText, Deselectall%>"  class="btn btn-white btn-inverse btn-sm" />
                                                            <asp:Button ID="Inverse" Visible="false" runat="server" OnClick="Inverse_Click" Text="<%$ Resources:LocalizedText, Inverse%>" class="btn btn-white btn-pink btn-sm" />
                                                        </p>
                                                    </div>


                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                              <asp:GridView ID="dgvComputer" OnRowDataBound="dgvComputer_RowDataBound" runat="server" ShowHeader="true" ShowFooter="true" DataKeyNames="Id" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="3%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate></HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%--  <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                                                <ContentTemplate>--%>
                                                                            <asp:CheckBox ID="chkRowComputer" Checked="false" OnCheckedChanged="chkRow_CheckedChanged" AutoPostBack="true" runat="server" />
                                                                            <%--        </ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="false"  HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblId" Text="<%$ Resources:LocalizedText, lblId%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="DetailId" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="15%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblIdBonEntree" Text="<%$ Resources:LocalizedText, lblIdBonEntree%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="IdBonEntree" runat="server" Text='<%#Eval("IdBonEntree")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                    
                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="40%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblDesignation" Text="<%$ Resources:LocalizedText, lblDesignation%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("DesignationProduit")%>'></asp:Label>
                                                                        <asp:RangeValidator runat="server" ControlToValidate="txtQteComputer" ErrorMessage="The quantity is greater than the expected quantity" Type="Integer" MinimumValue="1" MaximumValue=<%#Eval("NewQuantity")%> ForeColor="Red"></asp:RangeValidator>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                              <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" ItemStyle-Width="5%" ItemStyle-Wrap="false">
                                                                        <HeaderTemplate>
                                                                            <%-- Quantity--%>
                                                                            <asp:Literal ID="lbdgvQuantity" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                      
                                                                            <asp:TextBox ID="txtQteComputer" Text='<%#Eval("NewQuantity")%>' MaxLength="2" runat="server" Enabled="false"   Min="1" Max='<%#Eval("NewQuantity")%>' Width="50px" TextMode="Number"></asp:TextBox>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     



                                                                  <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, lblCompany%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("IdCompany")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                

                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="15%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblEscoter" Text="<%$ Resources:LocalizedText, lblEscoter%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("EscorteurName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                


                                                                  <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" Visible="false" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="Literal2" Text="<%$ Resources:LocalizedText, lblDepartment%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("Department")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblReason1" Text="<%$ Resources:LocalizedText, lblReason1%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("Reason")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                   <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="false" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="lblFrom" Text="<%$ Resources:LocalizedText, lblFrom%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label9" runat="server" Text='<%#Eval("FromSite")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" HeaderStyle-Width="10%" ItemStyle-Width="7%" Visible="true">
                                                                    <HeaderTemplate></HeaderTemplate>
                                                                    <ItemTemplate>


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


                                            </div>

                                        </div>
                                        <!--fin bloc onglet detail-->
                                        <br />
                                    </div>



                                    
                                     <div class="tabbable">
                                            
<%--                                            <br />
                    

                                             <br />
                                            <table>
                                                  <tr>

                                                            <td>
                                                                <br />
                                                                <asp:Button ID="btnAddContractor" onclick="btnAddContractor_Click" CssClass="btn2 btn2-btnSave btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnAddEstimation%>" />
                                                            </td>
                                                        </tr>
                                            </table>
                                              <br />--%>

 <%--                <div class="widget-header widget-header-blue widget-header-flat" style="text-align: center">
                        <h4 class="widget-title lighter">
  
                              <asp:Label ID="Label12" runat="server" Text="Label" Visible="false"></asp:Label>
                              <asp:Label ID="Label13" runat="server" Text="<%$ Resources:LocalizedText, lblItemRelease%>"></asp:Label>
                             <span class="badge badge" style="background-color:#003468" id="Span2" runat="server"></span>
                        </h4>
                    </div>--%>


<%--                                            <div id="profile5" class="tab-pane in active">
                    
                                                    <br />
                                                    <div runat="server" style="height: 200px; overflow-y: scroll;">
                                                        <asp:GridView ID="GridView2" runat="server" ShowHeader="true" ShowFooter="true"  DataKeyNames="Id" AllowPaging="false" ShowHeaderWhenEmpty="true" CssClass="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="False">
                                                            <Columns>

                                                                
                                                                <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblId%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="DetailId2" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="false" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblId%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="IdBonEntree2" runat="server" Text='<%#Eval("IdBonEntree")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                               
                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="Literal6" Text="<%$ Resources:LocalizedText, lblDesignation%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="DesignationProduit2" runat="server" Text='<%#Eval("DesignationProduit")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                 <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="#003468" HeaderStyle-BorderColor="#003468" Visible="true" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                    <HeaderTemplate>
                                                                        <asp:Literal ID="Literal7" Text="<%$ Resources:LocalizedText, lbdgvQuantity%>" runat="server"></asp:Literal>

                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Quantity2" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                                                    </ItemTemplate>

                                                            </Columns>
                                                            <SelectedRowStyle CssClass="alert-info" />
                                                            <RowStyle CssClass="cursor-pointer" />
                                                            <HeaderStyle CssClass="alert-success" />
                                                            <PagerStyle HorizontalAlign="Center" />
                                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                                        </asp:GridView>
                                                        </div>

                                            </div>
                                                
                                              <br/>--%>




                                         


  <div class="clearfix form-actions">
                                        <div id="mid" runat ="server" visible="false" >
                                          
                                            <asp:Button ID="btnSave" cssclass="btn2 btn2-btnSave btn2-cons" OnClick="btnSave_Click" runat="server" Text="<%$ Resources:LocalizedText, btnSave%>"  />
                                           
											 <asp:Button ID="btnReset" OnClick="btnReset_Click" cssclass="btn2 btn2-btnReset btn2-cons" runat="server" Text="<%$ Resources:LocalizedText, btnReset%>"  />
                                           
                                        </div>

                                    </div>
                                 

                                    </div>
                                    </div>

</div>
      <br />

                    <div class="widget-body">
                        <div class="step-content pos-rel">
                            <div class="step-pane active" data-step="1">

                                <!--bloc des controls de la requisition-->

                                <div style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19); width: 60%; margin-left: 20%; margin-right: 20%; padding-left: 5%; padding-right: 5%" id="bodyItRequest">
                                       <!-- reseach-->
                                 
                                   
                                    <!--reseach-->

                                    <!-- entite de la requisition-->
                                 

                                    <!--fin entete requesition-->
                                    <%--   <hr />--%>

                                    <!--bloc detail de la requisition-->

                                    <div class="form-horizontal" id="Form2" style="width: 100%;">

                                      
                                        <!--bloc des onglets des detail-->
                                       


                                            <%--Fin Detail Escorteur--%>



                                            </div>
                                                                                    </div>
                                                <!--fin onglet computer-->

                                                <!--onglet telephone-->
                                               
                                                <!--FIN ONGLET TELEPHONE-->


                                                <!--ONGLET CONNECTIVITY-->
                                                

                                                    <!--DEBUT FORMULAIRE POUR NEW USER ACCOUNT-->
                                                   
                                                    <!--fin formulaire NEW USER ACCOUNT-->

                                              
                                                <!--FIN ONGLET CONNECTIVITY-->

                                                <!--ONGLET SOFTWARE-->
                                                <div id="sofware" class="tab-pane">
                                                       <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                    
                                             

                                                    <div id="standarSoftware" runat="server">
                                                   
                                                  </div>

                                                    
                                                               </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <!--FIN SOFTWARE-->

                                                <!--ONGLET NETWORK-->
                                                
                                                <!--FIN ONGLET NETWORK-->

                                                <!--ONGLET APPLICATION-->
                                              

                                                    <!--DEBUT FORMULAIRE POUR APPLICATION-->
                                                 
                                                    <!--fin formulaire application-->
                                                </>
                                                <!--fin onglet application-->


                                                <!--onglet fileShare-->
                                               
                                                    <!--DEBUT FORMULAIRE POUR filShare-->
                                                   
                                                <!--FIN ONGLET fileShare-->

                                      

                                        <!--fin bloc onglet detail-->
                                        <br />
                                 

                                    <!-- button pour l'enregistrement et l'initialisation des données-->
                                   
                                    <!--fin zone de button-->
                                    <br />
                                </div>

                            </div>
                            <!-- /.widget-main -->
                        </div>
                        <!-- /.widget-body -->
    


    <!--popup pour le message d'erreur-->


    <div id="my-modal" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color:#682525;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter white no-margin">
                        <asp:Literal ID="Literal8" Text="<%$ Resources:LocalizedText, popUpInvalidInput%>" runat="server"></asp:Literal>
                    </h3>
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
                        <asp:Literal ID="Literal9" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!--fin popup-->




    





   


    <!--fin popup-->
      <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
     <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
          <div class="modal-content">
        <div class="modal-header" style="background-color:#006666;">
            <h3 class="smaller lighter white no-margin">
                     CONFIRMATION
                    </h3>
        </div>
        <div class="modal-body">  
           <h3 class="smaller lighter green no-margin">
            <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
                  <asp:Label ID="lblIDRequest" runat="server" Visible="false" Text=""></asp:Label>
                            </h3>
     
        </div>
               <div class="modal-footer">
                   
                      <asp:Button ID="btnOk"  class="btn btn-success" OnClick="btnOk_Click"  runat="server" Text="OK"  />

                </div>
            </div>
    </asp:Panel>

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
       
        function openConfirm() {
       

           

        }
       
    </script>
    <script type="text/javascript" src="assets/js/jquery-1.8.3.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modale");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 3 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
   
    $('form').live("submit", function () {
        var $btn = $(document.activeElement);
        if ($btn.attr("id") == "ctl00_ContentPlaceHolder1_btnOk") {
            return true;
        }
        else {
            ShowProgress();
        }

    });

    
    
</script>
    <div class="loading" align="center" id="panelLoading" runat="server">
       <h4 class="widget-title red">
    <asp:Literal ID="Literal12" Text="<%$ Resources:LocalizedText, lblLoading%>" runat="server"></asp:Literal>
            </h4>
    <img src="bx_loader.gif" alt="" />
</div>
</asp:Content>




