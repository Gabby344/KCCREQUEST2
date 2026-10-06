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
    public partial class New_EW_TechnicalDepartment : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Au premier chargement de la page 
            if (!IsPostBack)
            {
                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "EWRequest").FirstOrDefault().Number == 0)
                {

                    Response.Redirect("../default_admin.aspx");
                }
                RoleCoordonator.DataSource = db.Get_EWOProcessRole();
                RoleCoordonator.DataTextField = "Role";
                RoleCoordonator.DataValueField = "Id";
                RoleCoordonator.DataBind();
                RoleCoordonator.Items.Insert(0, new ListItem("--Select a Role Coordonator--", null));

                RoleMan.DataSource = db.Get_EWOProcessRole();
                RoleMan.DataTextField = "Role";
                RoleMan.DataValueField = "Id";
                RoleMan.DataBind();
                RoleMan.Items.Insert(0, new ListItem("--Select a Role Man--", null));

                Rolegm.DataSource = db.Get_EWOProcessRole();
                Rolegm.DataTextField = "Role";
                Rolegm.DataValueField = "Id";
                Rolegm.DataBind();
                Rolegm.Items.Insert(0, new ListItem("--Select an Role GM--", null));


                Tableau_EW_TechnicalDepartment_Request_Item_modification.DataSource = db.Crud_EWORequest_TechlDepStatut(null, null, null, null, null, null, null, null);
                Tableau_EW_TechnicalDepartment_Request_Item_modification.DataBind();
            }

            // Au Autres chargement
            else
            {
                // Initialisation des variables
                String Action, TechnicalDepDescription, RoleCoordonator, RoleMan, RoleGM;
                int ID, DepartmentCode;
                bool Istchnical;

                Action = ActionSet.Value;
                TechnicalDepDescription = TechnicalDepDescriptionSet.Value;
                RoleCoordonator = RoleCoordonatorSet.Value;
                RoleMan = RoleManSet.Value;
                RoleGM = RoleGMSet.Value;
                Int32.TryParse(IDSet.Value, out ID);
                Int32.TryParse(DepartmentCodeSet.Value, out DepartmentCode);
                Istchnical = (IstchnicalSet.Value=="0") ? false : true ;
                if (Action == "UPDATE")
                {
                    Tableau_EW_TechnicalDepartment_Request_Item_modification.DataSource = db.Crud_EWORequest_TechlDepStatut(ID, TechnicalDepDescription, DepartmentCode, RoleCoordonator, RoleMan, RoleGM, Istchnical, Action);
                    Tableau_EW_TechnicalDepartment_Request_Item_modification.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int DepartmentCodeIn;
            bool Istchnical;
            Istchnical = (IstchnicalSet.Value == "0") ? false : true;
            Int32.TryParse(DepartmentCode.Text, out DepartmentCodeIn);
            String TechnicalDepDescriptionSet = TechnicalDepDescription.Text, RoleCoordonatorSet = RoleCoordonator.SelectedItem.Text, RoleManSet = RoleMan.SelectedItem.Text, RoleGMSet = Rolegm.SelectedItem.Text;

            if (!String.IsNullOrEmpty(TechnicalDepDescriptionSet) && !String.IsNullOrEmpty(RoleCoordonatorSet))
            {
                Tableau_EW_TechnicalDepartment_Request_Item_modification.DataSource = db.Crud_EWORequest_TechlDepStatut(null, TechnicalDepDescriptionSet, DepartmentCodeIn, RoleCoordonatorSet, RoleManSet, RoleGMSet, Istchnical, "INSERT");
                Tableau_EW_TechnicalDepartment_Request_Item_modification.DataBind();
                TechnicalDepDescription.Text = "";
                RoleCoordonator.SelectedIndex = 0;
                RoleMan.SelectedIndex = 0;
                Rolegm.SelectedIndex = 0;

            }
        }
    }
}