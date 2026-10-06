using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;

namespace KCCREQUEST2
{
    public partial class Rapports : System.Web.UI.MasterPage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        int homePage; int homeLang;

        protected void Page_Load(object sender, EventArgs e)
        {
            new ProfilUsers().BindMyProfile(lblNameUser, Image1, Image2, lbluserAccompt, lblMatricule, lblDepartment, lblFonction, lblAdresseBook);
            Image2.Attributes.Add("alt", lblNameUser.Text);
            Image1.Attributes.Add("alt", lblNameUser.Text);
            homePage = int.Parse(db.Get_KCCRequest_UserHomePage(utility.Login(loggedUser)).FirstOrDefault().HomePage.ToString());
            homeLang = int.Parse(db.Get_KCCRequest_UserHomePage(utility.Login(loggedUser)).FirstOrDefault().Languages.ToString());
            if (!IsPostBack)
            {
                if (db.Get_KCCRequest_UserHold(utility.Login(loggedUser)) > 0) menuHold.Visible = true;
                else menuHold.Visible = false;

                if (homeLang == 0)
                {

                    lblLanguage.Text = "English";
                    flagEnglish.Visible = true;
                    flagFrancais.Visible = false;
                    btnEnglais.Visible = false;
                    btnFrancais.Visible = true;


                }
                if (homeLang == 1)
                {
                    lblLanguage.Text = "Français";
                    flagEnglish.Visible = false;
                    flagFrancais.Visible = true;
                    btnEnglais.Visible = true;
                    btnFrancais.Visible = false;


                }

                //int.Parse(Server.HtmlEncode(Request.Cookies["homepage"].Value));
                //int homePage = int.Parse(db.Get_KCCRequest_UserHomePage(utility.Login(loggedUser)).FirstOrDefault().HomePage.ToString());
                if (homePage == 0)
                {
                    rdAllmyrequests.Checked = true;
                }
                else if (homePage == 1)
                {
                    rdAllrequestApproval.Checked = true;
                }

            }
        }
        protected void rdAllmyrequests_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAllmyrequests.Checked)
            {
                db.Insert_KCCRequest_User_HomePage(utility.Login(loggedUser), 0, homeLang);
                //MyCookies.homepage.Value = "2";
                //MyCookies.homepage.Expires = DateTime.Now.AddDays(60);
                //Response.Cookies.Add(MyCookies.homepage);
                Response.Redirect("Default_View_MyRequests.aspx");

            }
        }

        protected void rdAllrequestApproval_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAllrequestApproval.Checked)
            {
                db.Insert_KCCRequest_User_HomePage(utility.Login(loggedUser), 1, homeLang);
                //MyCookies.homepage.Value = "3";
                //MyCookies.homepage.Expires = DateTime.Now.AddDays(60);
                //Response.Cookies.Add(MyCookies.homepage);
                Response.Redirect("Default_View_Approve.aspx");
            }
        }


        protected void rdAllmodules_CheckedChanged(object sender, EventArgs e)
        {
           
        }
    }
}