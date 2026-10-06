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
    public partial class New_Item : System.Web.UI.Page
    {
    KCCDBWrapper db = new KCCDBWrapper();
    Utility utility = new Utility();
    private string loggedUser = HttpContext.Current.User.Identity.Name;
    protected void Page_Load(object sender, EventArgs e)
        {
            // Au premier chargement de la page 
            if (!IsPostBack)
            {

                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "ITRequest").FirstOrDefault().Number == 0)
                {

                    Response.Redirect("../default_admin.aspx");
                }

                Service.DataSource = db.Get_ITRequestService();
                Service.DataTextField = "ServiceDescription";
                Service.DataValueField = "ServiceID";
                Service.DataBind();
                Service.Items.Insert(0, new ListItem("--Select a Service--", null));

                Category.DataSource = db.Get_ITRequestCategory();
                Category.DataTextField = "CategoryDescription";
                Category.DataValueField = "CategoryID";
                Category.DataBind();
                Category.Items.Insert(0, new ListItem("--Select a Category--", null));

                ApproverRole.DataSource = db.Get_ITRequestProcessRole();
                ApproverRole.DataTextField = "Role";
                ApproverRole.DataValueField = "Id";
                ApproverRole.DataBind();
                ApproverRole.Items.Insert(0, new ListItem("--Select an Approver--", null));


                Tableau_IT_Request_Item_modification.DataSource = db.Crud_itRequestItem(null, null,null, null, null, null, null,null,null);
                Tableau_IT_Request_Item_modification.DataBind();
            }

            // Au Autres chargement
            else
            {
                // Initialisation des variables
                String ApproverRole, Action, ItemDescription, Observation,ItemDescriptionFrench;
                int ID, ServiceID, CategoryID, Level;

                ApproverRole = ApproverRoleSet.Value;
                Action = ActionSet.Value;
                ItemDescription = ItemDescriptionSet.Value;
                ItemDescriptionFrench = ItemDescriptionFrenchSet.Value;
                Observation = ObservationSet.Value;
                Int32.TryParse(IDSet.Value, out ID);
                Int32.TryParse(ServiceDescriptionSet.Value, out ServiceID);
                Int32.TryParse(CategoryDescriptionSet.Value, out CategoryID);
                Int32.TryParse(LevelSet.Value, out Level);
                if (Action == "UPDATE")
                {
                    Tableau_IT_Request_Item_modification.DataSource = db.Crud_itRequestItem(ID, ServiceID, CategoryID, ItemDescription, Observation, ApproverRole, Level, Action,ItemDescriptionFrench);
                    Tableau_IT_Request_Item_modification.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int IdService,IdCategory, LevelIn;
            Int32.TryParse(Service.SelectedItem.Value, out IdService); 
            Int32.TryParse(Category.SelectedItem.Value, out IdCategory);
            Int32.TryParse(Level.Text, out LevelIn);
            String ItemDescriptionSet = ItemDescription.Text, ObservationSet = Observation.Text, ApproverSet=ApproverRole.SelectedItem.Text, ItemDescriptionFrenchSet = ItemDescriptionFrench.Text;
        
            if (!String.IsNullOrEmpty(ItemDescriptionSet) && !String.IsNullOrEmpty(ObservationSet) && !String.IsNullOrEmpty(ItemDescriptionFrenchSet))
            {
                Tableau_IT_Request_Item_modification.DataSource = db.Crud_itRequestItem(null, IdService, IdCategory, ItemDescriptionSet, ObservationSet, ApproverSet, LevelIn, "INSERT",ItemDescriptionFrenchSet);
                Tableau_IT_Request_Item_modification.DataBind();
                ItemDescription.Text = "";
                Observation.Text = "";
                ItemDescriptionFrench.Text = string.Empty;
                Service.SelectedIndex = 0;
                Category.SelectedIndex = 0;
                ApproverRole.SelectedIndex = 0;
                
            }
        }

        //protected void Tableau_IT_Request_Item_modification_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    Tableau_IT_Request_Item_modification.EditIndex = e.NewEditIndex;


        //    Tableau_IT_Request_Item_modification.DataSource = db.Crud_itRequestItem(null, null, null, null, null, null, null, null);
        //    Tableau_IT_Request_Item_modification.DataBind();
        //}

        //protected void Tableau_IT_Request_Item_modification_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    Tableau_IT_Request_Item_modification.EditIndex =-1;
        //    Tableau_IT_Request_Item_modification.DataSource = db.Crud_itRequestItem(null, null, null, null, null, null, null, null);
        //    Tableau_IT_Request_Item_modification.DataBind();
        //}
    
        //protected void Tableau_IT_Request_Item_modification_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {

        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {

        //            if (Tableau_IT_Request_Item_modification.EditIndex == e.Row.RowIndex)
        //            {
                      
        //                string selectedApproverRoleName = (string)DataBinder.Eval(e.Row.DataItem, "ApproverRole");



        //                DropDownList DDLApproverRoles = (DropDownList)e.Row.Cells[3].FindControl("DDLApproverRole");

        //                DDLApproverRoles.DataSource = db.Get_ITRequestProcessRole();
        //                DDLApproverRoles.DataTextField = "Role";
        //                DDLApproverRoles.DataValueField = "Id";
        //                DDLApproverRoles.DataBind();

        //                if (string.IsNullOrEmpty(selectedApproverRoleName))
        //                {
        //                    DDLApproverRoles.Items.FindByText(selectedApproverRoleName).Selected = true;
        //                }
        //                else
        //                {
        //                    DDLApproverRoles.Items.Insert(0, new ListItem("--Select an Approver--", null));
        //                }


        //            }
        //        }

        //    }
        //    catch (Exception) {


        //    }
        //}
    }
}