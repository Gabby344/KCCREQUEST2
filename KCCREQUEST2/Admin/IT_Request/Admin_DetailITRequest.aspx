<%@ Page Title="" Language="C#" MasterPageFile="~/Model_Page.Master" AutoEventWireup="true" CodeBehind="Admin_DetailITRequest.aspx.cs" Inherits="KCCREQUEST2.Admin.IT_Request.Admin_DetailITRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="graph" id="table" style="background-color: transparent; height: 600px; overflow-y: auto; overflow-x: hidden;">
          
            <div class="tables">
                 
                <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                                    
                                  

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


      <div id="my-modale" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h3 class="smaller lighter blue no-margin">Radio Request</h3>
                    <asp:Label ID="lblaction" runat="server" Text="0" Visible="false"></asp:Label>
                </div>

                <div class="modal-body">


                   

                      <h4 class="smaller lighter red no-margin">
                        <div>
                            <label for="form-field-select-1">
                                <asp:Literal ID="lbdgvComment" Text="<%$ Resources:LocalizedText, lbdgvComment%>" runat="server"></asp:Literal>

                                <b style="color: red">*</b></label><br />
                            <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <br/>

                         <div>
                                                    <label for="form-field-select-1">
                                                        
                                                        <asp:Literal ID="Literal3" Text="<%$ Resources:LocalizedText, lblQuotationFile%>" runat="server"></asp:Literal>
                                                    </label>
                                                    <div class="timeline-body">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" Width="90%" class="multi" />
                                                        <br>
                                                    </div>
                                                </div>

                    </h4>





                </div>

                <div class="modal-footer">


                    <asp:Button ID="btnSave"   OnClientClick="this.disabled=true;" UseSubmitBehavior="false" class="btn btn-sm btn-info" runat="server" Text="OK" />


                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                        <i class="ace-icon fa fa-times"></i>
                        <asp:Literal ID="Literal1" Text="<%$ Resources:LocalizedText, btnClose%>" runat="server"></asp:Literal>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
           <script src="../../Scripts/jquery-3.2.1.js"></script>
    <script src="../.../Scripts/jquery-ui-1.12.1.js"></script>
      <script type="text/javascript">
        function openModal() {
            $('#my-modale').modal('show');
        }
        function hideModal() {
            $('#my-modal').modal('hide');
        }
        function openModalErreur() {
            $('#modal_erreur').modal('show');
        }

    </script>
</asp:Content>
