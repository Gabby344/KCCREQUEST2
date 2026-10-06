<%@ Page Title="" Language="C#" MasterPageFile="~/ViewRequest.Master" AutoEventWireup="true" CodeBehind="chattest.aspx.cs" Inherits="KCCREQUEST2.chattest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Smartsupp Live Chat script -->
<script type="text/javascript">
var smartsupp = smartsupp || {};
_smartsupp.key = '105612b86983d8cb00577685daf3df94356d10ab';
window.smartsupp||(function(d) {
  var s,c,o=smartsupp=function(){ o._.push(arguments)};o._=[];
  s=d.getElementsByTagName('script')[0];c=d.createElement('script');
  c.type='text/javascript';c.charset='utf-8';c.async=true;
  c.src='https://www.smartsuppchat.com/loader.js?';s.parentNode.insertBefore(c,s);
})(document);
</script>
</asp:Content>
