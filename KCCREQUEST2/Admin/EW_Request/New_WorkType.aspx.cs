using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
namespace KCCREQUEST2.Admin.EW_Request
{
    public partial class New_WorkType : System.Web.UI.Page
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

                LstDepartment.DataSource = db.Get_EWRequest_TechnicalDepartment(utility.Login(loggedUser));
                LstDepartment.DataTextField = "TechnicalDepDescription";
                LstDepartment.DataValueField = "TechincalDepId";
                LstDepartment.DataBind();
                LstDepartment.Items.Insert(0, new ListItem("--Select a department--", null));


                dgvWorkType.DataSource = db.Crud_EWORequest_WorkType(null, null, null, null,null,null,null,null,null);
                dgvWorkType.DataBind();
            }

            // Au Autres chargement
            else
            {
                try
                {
                    // Initialisation des variables
                    String Description, DescriptionFrench, Action;
                    int Id, TechDepartement;
                    Description = WorkTypeDescriptionSet.Value;
                    DescriptionFrench = WorkTypeDescriptionFrenchSet.Value;
                    TechDepartement = db.Get_EWRequest_TechnicalDepartment(utility.Login(loggedUser)).Where(x => x.TechnicalDepDescription == TechnicalDepDescriptionSet.Value).FirstOrDefault().TechincalDepId;
                    //Int32.TryParse(TechnicalDepDescriptionSet.Value, out TechDepartement);

                    Action = ActionSet.Value;
                    Int32.TryParse(WorkTypeIDSet.Value, out Id);

                    if (Action == "UPDATE")
                    {
                        // Modication dans labase de donnée
                        dgvWorkType.DataSource = db.Crud_EWORequest_WorkType(Id, TechDepartement, Description, DescriptionFrench, null, null, null, null, Action);
                        dgvWorkType.DataBind();
                    }
                }
                catch (Exception s) {

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (LstDepartment.SelectedIndex != 0 && !string.IsNullOrEmpty(txtWorkType.Text) && !string.IsNullOrEmpty(txtWorkTypeFrench.Text))
            {

                dgvWorkType.DataSource = db.Crud_EWORequest_WorkType(null,int.Parse(LstDepartment.SelectedValue),txtWorkType.Text,txtWorkTypeFrench.Text, null, null, null, null,"INSERT");
                dgvWorkType.DataBind();

                LstDepartment.SelectedIndex = 0;
                txtWorkTypeFrench.Text = string.Empty;
                txtWorkType.Text = string.Empty;

            }
        }
    }
}