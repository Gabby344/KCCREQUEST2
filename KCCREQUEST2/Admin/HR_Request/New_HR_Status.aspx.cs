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
    public partial class New_HR_Status : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Au premier chargement de la page 
            if (!IsPostBack)
            {
                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "RECTRequest").FirstOrDefault().Number == 0)
                {

                    Response.Redirect("../default_admin.aspx");
                }
                Tableau_HR_Request_Status.DataSource = db.Crud_RECTRequest_Status(null, null, null, null, null, null, null, null, null, null, null, null, "SELECT");
                Tableau_HR_Request_Status.DataBind();
            }

             // Au Autres chargement
            else
            {
                int ID;
                Int32.TryParse(StatusIDSet.Value, out ID);
                if (ActionSet.Value == "UPDATE")
                {
                    Tableau_HR_Request_Status.DataSource = db.Crud_RECTRequest_Status(ID, StatusDescriptionSet.Value,StatusDescriptionFrSet.Value, ActionStatusSet.Value, EmailDescriptionOrginatorFrSet.Value, EmailDescriptionOrginatorEnSet.Value, EmailSubjectOrginatorFrSet.Value, EmailSubjectOrginatorEnSet.Value, EmailDescriptionApproverFrSet.Value, EmailDescriptionApproveEnSet.Value, EmailSubjectApproverFrSet.Value, EmailSubjectApproverEnSet.Value, ActionSet.Value);
                    Tableau_HR_Request_Status.DataBind();
                    AnswerServer.Text = "Data Updated";
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(StatusID.Text) && !String.IsNullOrEmpty(EmailDescriptionOrginatorFr.Text) && !String.IsNullOrEmpty(EmailSubjectOrginatorFr.Text) && !String.IsNullOrEmpty(EmailSubjectOrginatorEn.Text) && !String.IsNullOrEmpty(EmailDescriptionApproverFr.Text) && !String.IsNullOrEmpty(EmailSubjectApproverFr.Text))
            {
                int idStatus;
                Int32.TryParse(StatusID.Text, out idStatus);
                Tableau_HR_Request_Status.DataSource = db.Crud_RECTRequest_Status(idStatus, StatusDescription.Text,StatusDescriptionFr.Text, Action.Text, EmailDescriptionOrginatorFr.Text, EmailDescriptionOrginatorEn.Text, EmailSubjectOrginatorFr.Text, EmailSubjectOrginatorEn.Text, EmailDescriptionApproverFr.Text, EmailDescriptionApproveEn.Text, EmailSubjectApproverFr.Text, EmailSubjectApproverEn.Text, "INSERT");
                Tableau_HR_Request_Status.DataBind();
                StatusID.Text = StatusDescription.Text = Action.Text = EmailDescriptionOrginatorFr.Text = EmailDescriptionOrginatorEn.Text = EmailSubjectOrginatorFr.Text = EmailSubjectOrginatorEn.Text = EmailDescriptionApproverFr.Text = EmailDescriptionApproveEn.Text = EmailSubjectApproverFr.Text = EmailSubjectApproverEn.Text = "";
                StatusDescriptionFr.Text = string.Empty;
                AnswerServer.Text = "Data inseted";
            }
        }
    }
}