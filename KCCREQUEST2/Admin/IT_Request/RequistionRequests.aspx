<%@ Page Title="" Language="C#" MasterPageFile="~/Model_Page.Master"  AutoEventWireup="true" CodeBehind="RequistionRequests.aspx.cs" Inherits="KCCREQUEST2.Admin.IT_Request.RequistionRequests" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    

        <div 
          class="graph" id="table" style="background-color: transparent; height: 750px; overflow-y: auto; overflow-x: auto;"
          >
<div class="tables">

      <div class="content-wrapper">
          <div class="content">							<div class="breadcrumb-wrapper">
								<h1 style="font-family:Verdana">IT Request for Services</h1>
									
<%--        
               <input type="text" name="query" id="search-input" class="form-control" placeholder="search" autofocus="" autocomplete="off"/>--%>
							
           <asp:TextBox ID="txtSearch"  class="form-control" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true" TextMode="SingleLine"  placeholder="search" autofocus="" runat="server" Width="30%"></asp:TextBox>
          </div>
							
							<div class="card card-default">
								<div class="card-header card-header-border-bottom" style="background-color:rgb(21,101,125);">								
									<h2 style="color:white;font-family:Verdana">Request's List</h2>

                                   
								</div>
								<div class="card-body">
								<%--	<div class="row">--%>
										
                                        <asp:GridView ID="dgvBeneficiary" runat="server" OnPageIndexChanging="dgvBeneficiary_PageIndexChanging" ShowHeader="true"  ShowFooter="true" AllowSorting="false" AllowPaging="true" PageSize="15" CssClass="tablestyle" Width="100%" AutoGenerateColumns="false">
                                                   <AlternatingRowStyle CssClass="altrowstyle" />
                                                               <HeaderStyle CssClass="headerstyle" />
                                                                <RowStyle CssClass="rowstyle" />    
                                                                 <PagerStyle CssClass="pagination-ys" />    
                                                                            
                                            
   <Columns>
                                          
                                            <asp:TemplateField    HeaderStyle-ForeColor ="#ffffff"   Visible="true" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                <HeaderTemplate>
                                                    ID
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                            <%--         <span class="badge badge-info">--%>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id")%>'></asp:Label>
                                                 <%--           </span>--%>
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

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="7%">
                                                <HeaderTemplate>
                                                   <asp:Literal ID="lblsBeneficiary" Text="<%$ Resources:LocalizedText, lblBeneficiary%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBeneficiary" runat="server" Text='<%#Eval("Beneficiary")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="10%">
                                                <HeaderTemplate>
                                                   Description
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
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

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="10%" ItemStyle-Width="7%">
                                                <HeaderTemplate>
                                                    <asp:Literal ID="lblPriority" Text="<%$ Resources:LocalizedText, lblPriority%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPriorite" runat="server" Text='<%#Eval("PrioriteDescprition")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="true" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="10%" ItemStyle-Width="5%">
                                                <HeaderTemplate>
                                                  <asp:Literal ID="lblStatut" Text="<%$ Resources:LocalizedText, lblStatut%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("StatusDescription")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="15%" ItemStyle-Width="10%">
                                                <HeaderTemplate>
                                                    <asp:Literal ID="lblPlannedStartDate" Text="<%$ Resources:LocalizedText, lblPlannedStartDate%>" runat="server"></asp:Literal>
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlannedDeliveryDate" runat="server" Text='<%# DateTime.Parse(Eval("PlannedDeliveryDate").ToString()).ToString("MM-dd-yyyy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false" HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="12%">
                                                <HeaderTemplate>
                                                    Doc pos
                                                
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocPos" runat="server" Text='<%#Eval("DocPos")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderStyle-ForeColor="#ffffff" Visible="false"  HeaderStyle-BackColor="SteelBlue" HeaderStyle-BorderColor="SteelBlue" HeaderStyle-Width="20%" ItemStyle-Width="12%">
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
                                            <asp:Button CssClass="btn  btn-xs btn-danger" OnClick="Detail_Click"  ID="btnDetail" runat="server" Text="Détail" />
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        </Columns>
                                                               
                                                             
                                                                <PagerStyle HorizontalAlign="Center" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                            </asp:GridView>




								<%--	</div>--%>
								</div>
							</div>

						

</div>

          


        </div>

</div>

            </div>
       <style>








.tablestyle 
{
    font-family: arial;
    font-size: small;
    border: solid 1px #7f7f7f;
}

.altrowstyle 
{
    background-color: #edf5ff;
}

.headerstyle th 
{
    background: url(assets/img/sprite.png) repeat-x 0px 0px;
    border-color: #989898 #cbcbcb #989898 #989898;
    border-style: solid solid solid none;
    border-width: 1px 1px 1px medium;
    color: #000;
    padding: 4px 5px 4px 10px;
    text-align: center;
    vertical-align: bottom;
}  

.headerstyle th a
{
    font-weight: normal;
    text-decoration: none;
    text-align: center;
    color: #000;
    display: block;
    padding-right: 10px;
}    

.rowstyle .sortaltrow, .altrowstyle .sortaltrow 
{
    background-color: #edf5ff;
}

.rowstyle .sortrow, .altrowstyle .sortrow 
{
    background-color: #dbeaff;
      color: #000;
}

.rowstyle td, .altrowstyle td 
{
    padding: 4px 10px 4px 10px;
    border-right: solid 1px #cbcbcb;
      color: #000;
}

.headerstyle .sortascheader 
{
    background: url(assets/img/sprite.png) repeat-x 0px -100px;
}

.headerstyle .sortascheader a 
{
    background: url(assets/img/dt-arrow-up.png) no-repeat right 50%;
} 

.headerstyle .sortdescheader 
{
    background: url(assets/img/sprite.png) repeat-x 0px -100px;
}   

.headerstyle .sortdescheader a 
{
    background: url(assets/img/dt-arrow-dn.png) no-repeat right 50%;
} 





I know this is old, But I found something, which is a css style, simple easy and fast

https://sufiawan.wordpress.com/2014/09/26/asp-net-use-bootstrap-pagination-on-gridview/

I hope it will save someone sometime.

update:

*In case the link is down:

You add the CSS

.pagination-ys {
    /*display: inline-block;*/
    padding-left: 0;
    margin: 20px 0;
    border-radius: 4px;
}

.pagination-ys table > tbody > tr > td {
    display: inline;
}

.pagination-ys table > tbody > tr > td > a,
.pagination-ys table > tbody > tr > td > span {
    position: relative;
    float: left;
    padding: 8px 12px;
    line-height: 1.42857143;
    text-decoration: none;
    color: #dd4814;
    background-color: #ffffff;
    border: 1px solid #dddddd;
    margin-left: -1px;
}

.pagination-ys table > tbody > tr > td > span {
    position: relative;
    float: left;
    padding: 8px 12px;
    line-height: 1.42857143;
    text-decoration: none;    
    margin-left: -1px;
    z-index: 2;
    color: #aea79f;
    background-color: #f5f5f5;
    border-color: #dddddd;
    cursor: default;
}

.pagination-ys table > tbody > tr > td:first-child > a,
.pagination-ys table > tbody > tr > td:first-child > span {
    margin-left: 0;
    border-bottom-left-radius: 4px;
    border-top-left-radius: 4px;
}

.pagination-ys table > tbody > tr > td:last-child > a,
.pagination-ys table > tbody > tr > td:last-child > span {
    border-bottom-right-radius: 4px;
    border-top-right-radius: 4px;
}

.pagination-ys table > tbody > tr > td > a:hover,
.pagination-ys table > tbody > tr > td > span:hover,
.pagination-ys table > tbody > tr > td > a:focus,
.pagination-ys table > tbody > tr > td > span:focus {
    color: #97310e;
    background-color: #eeeeee;
    border-color: #dddddd;
}
     </style>


</asp:Content>
