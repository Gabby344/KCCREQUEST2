using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.Globalization;
using System.IO;

namespace KCCREQUEST2
{
    public partial class New_TNR_Type : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Au premier chargement de la page 
            if (!IsPostBack)
            {
                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRNRequest").FirstOrDefault().Number == 0)
                {

                    Response.Redirect("../default_admin.aspx");
                }
                Tableau_TRN_Type_Request_ProcessRole_modification.DataSource =db.Crud_TRNRequest_Type_ProcessRole(null, null, null);
                Tableau_TRN_Type_Request_ProcessRole_modification.DataBind();
            }

            // Au Autres chargement
            else
            {
                // Initialisation des variables
                String Description, Action;
                int Id;
                Description = DescriptionSet.Value;
                Action = ActionSet.Value;
                Int32.TryParse(IDSet.Value, out Id);

                if (Action == "UPDATE")
                {
                    // Modication dans labase de donnée
                    Tableau_TRN_Type_Request_ProcessRole_modification.DataSource = db.Crud_TRNRequest_Type_ProcessRole(Id, Description, Action);
                    Tableau_TRN_Type_Request_ProcessRole_modification.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if ((TypeDescription.Text).Length > 2)
            {
                String action = "INSERT";
                Tableau_TRN_Type_Request_ProcessRole_modification.DataSource = db.Crud_TRNRequest_Type_ProcessRole(null, TypeDescription.Text, action);
                Tableau_TRN_Type_Request_ProcessRole_modification.DataBind();
                TypeDescription.Text = "";
                AnswerFromServer.Text = "Data inserted";
            }
        }
    }
}