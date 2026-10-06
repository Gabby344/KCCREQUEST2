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
    public partial class Model_Page : System.Web.UI.MasterPage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                visibilityModule();
            }

        }

        public void visibilityModule() {

            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "ITRequest").FirstOrDefault().Number == 0) {

                mITrequest.Visible = false;
            }
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "DSKRequest").FirstOrDefault().Number == 0)
            {

               mDSKRequest.Visible = false;
            }
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "EWRequest").FirstOrDefault().Number == 0)
            {

                mEWRequest.Visible = false;
            }
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "RECTRequest").FirstOrDefault().Number == 0)
            {

                mRecruitmentRequest.Visible = false;
            }
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRNRequest").FirstOrDefault().Number == 0)
            {

                mTrainingRequest.Visible= false;
            }
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "Parameter").FirstOrDefault().Number == 0)
            {

                mParametre.Visible = false;
            }
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRANSRequest").FirstOrDefault().Number == 0)
            {

                mTransRequest.Visible = false;
            }
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "DCRequest").FirstOrDefault().Number == 0)
            {

                mDCRequest.Visible = false;
            }


        }
    }
}