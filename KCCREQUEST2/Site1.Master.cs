using System;
using System.Linq;
using System.Web.UI;
using KCCDBAccess;

namespace KCCREQUEST2
{
    /// <summary>
    /// Master page providing common layout and functionality across the application
    /// </summary>
    public partial class Site1 : System.Web.UI.MasterPage
    {
        private readonly KCCDBWrapper db = new KCCDBWrapper();

        protected void Page_Load(object sender, EventArgs e)
        {
            var bp = Page as BasePage; // Single source of truth

            // Force compute if missing; safe even if null page (design-time)
            string cachedLogin = bp?.CachedLogin ?? string.Empty;
            int homePage = bp?.HomePage ?? 0;
            int homeLang = bp?.HomeLang ?? 0;

            if (!IsPostBack)
            {
                InitializeUserProfile();

                // Use values from BasePage – no direct Session access here
                InitializeMenuVisibility(cachedLogin);
                SetLanguageDisplay(homeLang);
                SetDefaultHomePage(homePage);
            }

            


        }

        private void InitializeUserProfile()
        {
            new ProfilUsers().BindMyProfile(
                lblNameUser, Image1, Image2,
                lbluserAccompt, lblMatricule, lblDepartment, lblFonction, lblAdresseBook);

            Image2.Attributes["alt"] = lblNameUser.Text;
            Image1.Attributes["alt"] = lblNameUser.Text;
        }

        private void InitializeMenuVisibility(string cachedLogin)
        {
            // If we have no login (anonymous/edge), hide hold menu
            if (string.IsNullOrEmpty(cachedLogin))
            {
                menuHold.Visible = false;
                return;
            }

            menuHold.Visible = db.Get_KCCRequest_UserHold(cachedLogin) > 0;
        }

        private void SetLanguageDisplay(int homeLang)
        {
            // 0 = en-us, 1 = fr-fr (as per your BasePage)
            btnEnglais.Visible = homeLang == 1;
            btnFrancais.Visible = homeLang == 0;
            flagEnglish.Visible = homeLang == 0;
            flagFrancais.Visible = homeLang == 1;
            lblLanguage.Text = homeLang == 0 ? "English" : "Français";
        }

        private void SetDefaultHomePage(int homePage)
        {
            rdAllmyrequests.Checked = homePage == 0;
            rdAllrequestApproval.Checked = homePage == 1;
        }

        protected void rdAllmyrequests_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdAllmyrequests.Checked) return;

            var bp = Page as BasePage;
            var cachedLogin = bp?.CachedLogin ?? string.Empty;
            var homeLang = bp?.HomeLang ?? 0;

            if (!string.IsNullOrEmpty(cachedLogin))
                db.Insert_KCCRequest_User_HomePage(cachedLogin, 0, homeLang);

            if (bp != null) bp.HomePage = 0; // keep Session in sync

            // Safer redirect (avoid ThreadAbortException)
            Response.Redirect("Default_View_MyRequests.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void rdAllrequestApproval_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdAllrequestApproval.Checked) return;

            var bp = Page as BasePage;
            var cachedLogin = bp?.CachedLogin ?? string.Empty;
            var homeLang = bp?.HomeLang ?? 0;

            if (!string.IsNullOrEmpty(cachedLogin))
                db.Insert_KCCRequest_User_HomePage(cachedLogin, 1, homeLang);

            if (bp != null) bp.HomePage = 1; // keep Session in sync

            Response.Redirect("Default_View_Approve.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void rdAllmodules_CheckedChanged(object sender, EventArgs e)
        {
            // Implement if/when needed
        }
    }
}
