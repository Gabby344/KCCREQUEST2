using KCCDBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2.Admin.IT_Request
{
    public partial class NewHomeApplication : System.Web.UI.Page
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

                Tableau_IT_Request_Application.DataSource = db.Crud_itRequestHomeLogiciel(null, null, null);
                Tableau_IT_Request_Application.DataBind();
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String action = "INSERT";
            Tableau_IT_Request_Application.DataSource = db.Crud_itRequestHomeLogiciel(txtLogicielCode.Text,LogicielDescription.Text, action);
            Tableau_IT_Request_Application.DataBind();
            LogicielDescription.Text = string.Empty;
            txtLogicielCode.Text = string.Empty;
          
        }

        protected void Tableau_IT_Request_Service_modification_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Tableau_IT_Request_Application.EditIndex = e.NewEditIndex;
            Tableau_IT_Request_Application.DataSource = db.Crud_itRequestHomeLogiciel(null, null, null);
            Tableau_IT_Request_Application.DataBind();
        }

        protected void Tableau_IT_Request_Service_modification_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Tableau_IT_Request_Application.EditIndex = -1;
            Tableau_IT_Request_Application.DataSource = db.Crud_itRequestHomeLogiciel(null, null, null);
            Tableau_IT_Request_Application.DataBind();
        }

        protected void Tableau_IT_Request_Service_modification_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string IDLogiciel = Tableau_IT_Request_Application.DataKeys[e.RowIndex].Values["LogicielID"].ToString();

            TextBox LogicielDescription = (TextBox)Tableau_IT_Request_Application.Rows[e.RowIndex].FindControl("txtLogicielDescription");



            db.Crud_itRequestHomeLogiciel(IDLogiciel, LogicielDescription.Text, "UPDATE");
            Tableau_IT_Request_Application.EditIndex = -1;

            Tableau_IT_Request_Application.DataSource = db.Crud_itRequestHomeLogiciel(null, null, null);
            Tableau_IT_Request_Application.DataBind();
        }
    }
}