using System;
using System.Web.UI;
using KCCDBAccess;
using System.Data.Entity.Core;
using System.Data.SqlClient;

namespace KCCREQUEST2
{
    public partial class NewRequest : System.Web.UI.MasterPage
    {
        private readonly KCCDBWrapper db = new KCCDBWrapper();

        protected void Page_Load(object sender, EventArgs e)
        {
            var bp = Page as BasePage; // single source of truth

            string cachedLogin = bp?.CachedLogin ?? string.Empty;
            int homePage = bp?.HomePage ?? 0;
            int homeLang = bp?.HomeLang ?? 0;

            // Profile / header UI
            new ProfilUsers().BindMyProfile(
                lblNameUser, Image1, Image2,
                lbluserAccompt, lblMatricule, lblDepartment, lblFonction, lblAdresseBook);

            Image2.Attributes["alt"] = lblNameUser.Text;
            Image1.Attributes["alt"] = lblNameUser.Text;

            if (!IsPostBack)
            {
                // Menu visibility
                menuHold.Visible = !string.IsNullOrEmpty(cachedLogin) && db.Get_KCCRequest_UserHold(cachedLogin) > 0;

                // Language UI (0=en, 1=fr)
                if (homeLang == 0)
                {
                    lblLanguage.Text = "English";
                    flagEnglish.Visible = true; flagFrancais.Visible = false;
                    btnEnglais.Visible = false; btnFrancais.Visible = true;
                }
                else
                {
                    lblLanguage.Text = "Français";
                    flagEnglish.Visible = false; flagFrancais.Visible = true;
                    btnEnglais.Visible = true; btnFrancais.Visible = false;
                }

                // Home page radio
                rdAllmyrequests.Checked = (homePage == 0);
                rdAllrequestApproval.Checked = (homePage == 1);
            }
        }

        protected void rdAllmyrequests_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdAllmyrequests.Checked) return;

            var bp = Page as BasePage;
            if (bp != null)
            {
                var login = bp.CachedLogin;
                var lang = bp.HomeLang;
                if (!string.IsNullOrEmpty(login))
                    db.Insert_KCCRequest_User_HomePage(login, 0, lang);

                bp.HomePage = 0; // keep Session cache in sync
            }

            Response.Redirect("Default_View_MyRequests.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void rdAllrequestApproval_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdAllrequestApproval.Checked) return;

            var bp = Page as BasePage;
            if (bp != null)
            {
                var login = bp.CachedLogin;
                var lang = bp.HomeLang;
                if (!string.IsNullOrEmpty(login))
                    db.Insert_KCCRequest_User_HomePage(login, 1, lang);

                bp.HomePage = 1;
            }

            Response.Redirect("Default_View_Approve.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void rdAllmodules_CheckedChanged(object sender, EventArgs e)
        {
            // Implement when needed
        }

        // Centralized email-sending exception display (unchanged logic, just tightened a bit)
        public void handleSendingMailException(Exception ex, string url, string requestId)
        {
            string title = string.Empty;
            string successNotification = string.Empty;
            string notificationDelay = string.Empty;

            if (ex is EntityCommandExecutionException entityEx &&
                entityEx.InnerException is SqlException sqlEx &&
                sqlEx.Message.IndexOf("profile name is not valid", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                title = (string)GetGlobalResourceObject("LocalizedText", "EmailSendingErrorTitle");
                successNotification = string.Format(
                    (string)GetGlobalResourceObject("LocalizedText", "LblSendMailExceptionError"), requestId);
                notificationDelay = (string)GetGlobalResourceObject("LocalizedText", "LblNotificationFailureMessage");
            }

            lblHeader.Text = title;
            LblConfirmation.Text = successNotification;
            lblDetaiError.Text = notificationDelay;

            ModalPopupExtenderEmailSendingError.Show();

            // If you want to redirect after OK, keep it explicit in btnOk_Click
            // rather than storing a field that's easy to go stale.
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_allRequest.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
