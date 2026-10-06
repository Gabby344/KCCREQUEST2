using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
namespace KCCREQUEST2.Admin.Parametre
{
    public partial class CouleurMail : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Au premier chargement de la page 
            if (!IsPostBack)
            {

                dllBodyMailColorCode.DataSource = db.Get_KCCRequest_Color();
                dllBodyMailColorCode.DataTextField = "ColorDescription";
                dllBodyMailColorCode.DataValueField = "ColorID";
                dllBodyMailColorCode.DataBind();
                dllBodyMailColorCode.Items.Insert(0, new ListItem("--Select a Color--", null));

                dllBodyMailColorDescription.DataSource = db.Get_KCCRequest_Color();
                dllBodyMailColorDescription.DataTextField = "ColorDescription";
                dllBodyMailColorDescription.DataValueField = "ColorID";
                dllBodyMailColorDescription.DataBind();
                dllBodyMailColorDescription.Items.Insert(0, new ListItem("--Select a Color--", null));

                dgvCouleurMail.DataSource = db.Crud_KCCRequest_Module(null, null, null, null,null,null);
                dgvCouleurMail.DataBind();
            }

            // Au Autres chargement
            else
            {
                // Initialisation des variables
                String ModuleDescription, ModuleDescriptionFr, BodyMailColorCode,BodyMailColorDescription, Action;
                string ModuleId;
                ModuleDescription = ModuleDescriptionSet.Value ;
                ModuleDescriptionFr = ModuleDescriptionFrSet.Value;
                BodyMailColorCode= BodyMailColorCodeSet.Value;
                BodyMailColorDescription = BodyMailColorDescriptionSet.Value;
                Action = ActionSet.Value;
                ModuleId= ModuleIDSet.Value;

                if (Action == "UPDATE")
                {
                    // Modication dans labase de donnée
                    dgvCouleurMail.DataSource = db.Crud_KCCRequest_Module(ModuleId,ModuleDescription,BodyMailColorCode,BodyMailColorDescription,Action,ModuleDescriptionFr);
                    dgvCouleurMail.DataBind();
                }
            }
        }
    }
}