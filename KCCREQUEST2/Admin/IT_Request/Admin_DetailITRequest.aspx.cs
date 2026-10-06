using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.Globalization;
using System.IO;

namespace KCCREQUEST2.Admin.IT_Request
{
    public partial class Admin_DetailITRequest : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserControl detail = (UserControl)Page.LoadControl("../../UC_DetailITRequest.ascx");
            Panel1.Controls.Add(detail);
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "ITRequest").FirstOrDefault().Number == 0)
            {

                Response.Redirect("../default_admin.aspx");
            }
        }
    }
}