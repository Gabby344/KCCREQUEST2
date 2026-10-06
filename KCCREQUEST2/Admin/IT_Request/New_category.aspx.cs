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
    public partial class New_category : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Au premier chargement de la page 
            if (!IsPostBack){

                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "ITRequest").FirstOrDefault().Number == 0)
                {

                    Response.Redirect("../default_admin.aspx");
                }

                Tableau_IT_Request_Categorie_modification.DataSource = db.Crud_itRequestCategory(null, null, null, null);
                Tableau_IT_Request_Categorie_modification.DataBind();

            }
            
            // Au Autres chargement
            else{
                // Initialisation des variables
                String CategoryDescription, Observations, Action;
                int IDCategorie;
                CategoryDescription = CategoryDescriptionSet.Value;
                Observations = ObservationsSet.Value;
                Action = ActionSet.Value;
                Int32.TryParse(IDSet.Value, out IDCategorie);

                if(Action=="UPDATE"){
                    // Modication dans labase de donnée
                    Tableau_IT_Request_Categorie_modification.DataSource = db.Crud_itRequestCategory(IDCategorie, CategoryDescription, Observations, Action);
                    Tableau_IT_Request_Categorie_modification.DataBind();
                }
            }
           

        }


        // Insertion d'un nouvel Item
        protected void Button1_Click(object sender, EventArgs e)
        {
            if ((CategoryDescription.Text).Length>2)
            {
                String action="INSERT";
                Tableau_IT_Request_Categorie_modification.DataSource= db.Crud_itRequestCategory(null, CategoryDescription.Text, Observations.Text, action);
                Tableau_IT_Request_Categorie_modification.DataBind();
                CategoryDescription.Text = "";
                Observations.Text = "";
            }
            
        }

    }
}