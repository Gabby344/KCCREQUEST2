using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.Globalization;
using System.IO;

namespace KCCREQUEST2.Admin.TRANS_Request
{
    public partial class NEW_TRANS_ProcessRole : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Au premier chargement de la page 
            if (!IsPostBack)
            {
                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRANSRequest").FirstOrDefault().Number == 0)
                {

                    Response.Redirect("../default_admin.aspx");
                }
                Tableau_EW_Request_ProcessRole_modification.DataSource = db.Crud_TRANSRequest_ProcessRole(null, null, null, null);
                Tableau_EW_Request_ProcessRole_modification.DataBind();
            }

            // Au Autres chargement
            else
            {
                // Initialisation des variables
                String Description, Role, Action;
                int Id;
                Description = DescriptionSet.Value;
                Role = RoleSet.Value;
                Action = ActionSet.Value;
                Int32.TryParse(IDSet.Value, out Id);

                if (Action == "UPDATE")
                {
                    // Modication dans labase de donnée
                    Tableau_EW_Request_ProcessRole_modification.DataSource = db.Crud_TRANSRequest_ProcessRole(Id, Role, Description, Action);
                    Tableau_EW_Request_ProcessRole_modification.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if ((Role.Text).Length > 2)
            {
                String action = "INSERT";
                Tableau_EW_Request_ProcessRole_modification.DataSource = db.Crud_TRANSRequest_ProcessRole(null, Role.Text, RoleDescription.Text, action);
                Tableau_EW_Request_ProcessRole_modification.DataBind();

                Role.Text = "";
                RoleDescription.Text = "";
                AnswerFromServer.Text = "Data inserted";
            }
        }
    }
}