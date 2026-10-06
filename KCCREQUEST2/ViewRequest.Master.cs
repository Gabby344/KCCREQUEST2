using System;
using System.Web.UI;
using KCCDBAccess;

namespace KCCREQUEST2
{
    public partial class ViewRequest : System.Web.UI.MasterPage
    {
        // EMERGENCY FIX: Remove db field and use BasePage cached methods

        protected void Page_Load(object sender, EventArgs e)
        {
            var bp = Page as BasePage;
            if (bp?.CachedLogin == null) return;

            string cachedLogin = bp.CachedLogin;
            int homePage = bp.HomePage;
            int homeLang = bp.HomeLang;

            // EMERGENCY FIX: Cache profile to avoid repeated calls
            BindProfileFromSession(cachedLogin);

            // EMERGENCY FIX: Use cached permissions
            bool hasUserHold = bp.UserPermissions.ContainsKey("UserHold") && bp.UserPermissions["UserHold"] > 0;
            menuHold.Visible = hasUserHold;

            // Language UI
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

            // REMOVE: querystring culture redirect here — culture is handled in BasePage.InitializeCulture()
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
                {
                    // EMERGENCY FIX: Use BasePage db instance
                    var kccWrapper = new KCCDBWrapper();
                    kccWrapper.Insert_KCCRequest_User_HomePage(login, 0, lang);
                }

                bp.HomePage = 0; // keep session cache in sync
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
                {
                    // EMERGENCY FIX: Use new instance
                    var kccWrapper = new KCCDBWrapper();
                    kccWrapper.Insert_KCCRequest_User_HomePage(login, 1, lang);
                }

                bp.HomePage = 1;
            }

            Response.Redirect("Default_View_Approve.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void rdAllmodules_CheckedChanged(object sender, EventArgs e)
        {
            // implement if needed
        }

        private void BindProfileFromSession(string login)
        {
            // EMERGENCY FIX: Cache profile to avoid repeated DB/AD calls
            var profileKey = $"UserProfile_{login}";
            if (Session[profileKey] == null)
            {
                try
                {
                    new ProfilUsers().BindMyProfile(lblNameUser, Image1, Image2, lbluserAccompt, lblMatricule, lblDepartment, lblFonction, lblAdresseBook);
                    Session[profileKey] = "loaded";
                }
                catch { /* Handle gracefully */ }
            }
            
            Image2.Attributes["alt"] = lblNameUser.Text;
            Image1.Attributes["alt"] = lblNameUser.Text;
        }
    }
}
