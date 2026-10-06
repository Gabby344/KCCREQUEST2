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
    public partial class New_service : System.Web.UI.Page
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

                Tableau_IT_Request_Service_modification.DataSource = db.Crud_itRequestService(null, null, null, null);
                Tableau_IT_Request_Service_modification.DataBind();
            }

         
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String action = "INSERT";
            Tableau_IT_Request_Service_modification.DataSource = db.Crud_itRequestService(null, ServiceDescription.Text, Observation.Text, action);
            Tableau_IT_Request_Service_modification.DataBind();
            ServiceDescription.Text = "";
            Observation.Text = "";
        }

        protected void Tableau_IT_Request_Service_modification_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Tableau_IT_Request_Service_modification.EditIndex = e.NewEditIndex;
            Tableau_IT_Request_Service_modification.DataSource = db.Crud_itRequestService(null, null, null, null);
            Tableau_IT_Request_Service_modification.DataBind();
        }

        protected void Tableau_IT_Request_Service_modification_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Tableau_IT_Request_Service_modification.EditIndex = -1;
            Tableau_IT_Request_Service_modification.DataSource = db.Crud_itRequestService(null, null, null, null);
            Tableau_IT_Request_Service_modification.DataBind();
        }

        protected void Tableau_IT_Request_Service_modification_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int IDService = int.Parse(Tableau_IT_Request_Service_modification.DataKeys[e.RowIndex].Values["ServiceID"].ToString());

            TextBox serviceDescription = (TextBox)Tableau_IT_Request_Service_modification.Rows[e.RowIndex].FindControl("txtServiceDescription");
            TextBox Observation = (TextBox)Tableau_IT_Request_Service_modification.Rows[e.RowIndex].FindControl("txtServiceObservation");

           db.Crud_itRequestService(IDService, serviceDescription.Text, Observation.Text, "UPDATE");
            Tableau_IT_Request_Service_modification.EditIndex = -1;

            Tableau_IT_Request_Service_modification.DataSource = db.Crud_itRequestService(null, null, null, null);
            Tableau_IT_Request_Service_modification.DataBind();
        }
    }
}