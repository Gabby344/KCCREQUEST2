using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
namespace KCCREQUEST2.Admin.EW_Request
{
    public partial class NewTask : System.Web.UI.Page
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

                LstWorkType.DataSource = db.Get_EWRequest_WorkType();
                LstWorkType.DataTextField = "WorkTypeDescription";
                LstWorkType.DataValueField = "WorkTypeID";
                LstWorkType.DataBind();
                LstWorkType.Items.Insert(0, new ListItem("--Select a work type--", null));


                dgvTask.DataSource = db.Crud_EWORequest_Task(null, null, null, null, null);
                dgvTask.DataBind();
            }

            // Au Autres chargement
            else
            {
                try
                {
                    // Initialisation des variables
                    String TaskDescription, TaskDescriptionFrench, Action;
                    int Id, WorkTypeID;
                    TaskDescription = TaskDescriptionEngSet.Value;
                    TaskDescriptionFrench = TaskDescriptionFrSet.Value;
                    WorkTypeID = db.Get_EWRequest_WorkType().Where(x => x.WorkTypeDescription == WorkTypeSet.Value).FirstOrDefault().WorkTypeID;
                    //Int32.TryParse(TechnicalDepDescriptionSet.Value, out TechDepartement);

                    Action = ActionSet.Value;
                    Int32.TryParse(TaskIdSet.Value, out Id);

                    if (Action == "UPDATE")
                    {
                        // Modication dans labase de donnée
                        dgvTask.DataSource = db.Crud_EWORequest_Task(Id,WorkTypeID,TaskDescription,TaskDescriptionFrench,Action);
                        dgvTask.DataBind();
                    }
            }
                catch (Exception s)
            {

            }
        }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (LstWorkType.SelectedIndex != 0 && !string.IsNullOrEmpty(txtTaskDescriptionEng.Text) && !string.IsNullOrEmpty(txtTaskDescriptionFren.Text))
            {

                dgvTask.DataSource = db.Crud_EWORequest_Task(null,int.Parse(LstWorkType.SelectedValue),txtTaskDescriptionEng.Text,txtTaskDescriptionFren.Text,"INSERT");
                dgvTask.DataBind();

                LstWorkType.SelectedIndex = 0;
                txtTaskDescriptionFren.Text = string.Empty;
                txtTaskDescriptionEng.Text = string.Empty;
            }
        }
    }
}