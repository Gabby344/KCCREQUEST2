using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;

namespace KCCREQUEST2
{
    public partial class all_NewRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Populate_KCCRequest_Module();
            }
               //UserControl itrequest = (UserControl)(LoadControl("~/form/itrequest.ascx"));
               //UserControl radiorequest = (UserControl)(LoadControl("~/form/dskrequest.ascx"));
               //UserControl trainingRequest = (UserControl)(LoadControl("~/form/trainingRequest.ascx"));
               //UserControl ewoRequest = (UserControl)(LoadControl("~/form/ewoRequest.ascx"));
               //UserControl RecrutmentRequest = (UserControl)(LoadControl("~/form/RecrutmentRequest.ascx"));

            //panelform.Controls.Add(itrequest);
            //panelform.Controls.Add(radiorequest);
            //panelform.Controls.Add(RecrutmentRequest);
           
         
        }
        public void Populate_KCCRequest_Module()
        {
            try
            {
                lstModule.DataSource = db.Get_KCCRequest_Module(utility.Login(loggedUser));
                lstModule.DataTextField = "ModuleDescription";
                lstModule.DataValueField = "ModuleID";
                lstModule.DataBind();
                lstModule.Items.Insert(0, "Choose One");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void lstModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstModule.SelectedValue == "DSKRequest")
            {
                UserControl radiorequest = (UserControl)(LoadControl("~/form/dskrequest.ascx"));
                panelform.Controls.Add(radiorequest);
            }
            else if (lstModule.SelectedValue == "EWRequest")
            {
                UserControl ewoRequest = (UserControl)(LoadControl("~/form/ewoRequest.ascx"));
                panelform.Controls.Add(ewoRequest);
            }
            else if (lstModule.SelectedValue == "ITRequest")
            {
                UserControl itrequest = (UserControl)(LoadControl("~/form/itrequest.ascx"));
                panelform.Controls.Add(itrequest);
            }
            //else if (lstModule.SelectedValue == "PPERequest")
            //{

            //}
            else if (lstModule.SelectedValue == "RECTRequest")
            {
                UserControl RecrutmentRequest = (UserControl)(LoadControl("~/form/RecrutmentRequest.ascx"));
                panelform.Controls.Add(RecrutmentRequest);
            }
            else if (lstModule.SelectedValue == "TRNRequest")
            {
                UserControl trainingRequest = (UserControl)(LoadControl("~/form/trainingRequest.ascx"));
                panelform.Controls.Add(trainingRequest);
            }
        }
    }
}