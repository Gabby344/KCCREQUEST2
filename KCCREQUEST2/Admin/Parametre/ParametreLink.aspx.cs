using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
namespace KCCREQUEST2.Admin.Parametre
{
    public partial class ParametreLink : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Au premier chargement de la page 
            if (!IsPostBack)
            {
             
                dgvLiens.DataSource = db.Crud_KCCRequest_Parametre(null, null, null, null);
                dgvLiens.DataBind();
            }

            // Au Autres chargement
            else
            {
                // Initialisation des variables
                String Key,Description,value, Action;

                Key = KeySet.Value;
                Description = DescriptionSet.Value;
                value = ValueSet.Value;
                Action = ActionSet.Value;
               

                if (Action == "UPDATE")
                {
                    // Modication dans labase de donnée
                    dgvLiens.DataSource = db.Crud_KCCRequest_Parametre(Key.Trim(),Description,value,Action);
                    dgvLiens.DataBind();
                }
            }
        }

        
        
    }
}