using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.Globalization;
using System.IO;

namespace KCCREQUEST2.Admin.TRANS_Request
{
    public partial class NEW_TRANS_Place : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Au premier chargement de la page 
                if (!IsPostBack)
                {
                    if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRANSRequest").FirstOrDefault().Number == 0)
                    {

                        Response.Redirect("../default_admin.aspx");
                    }
                    dllSite.DataSource = db.Get_TRANSRequest_Site();
                    dllSite.DataTextField = "SiteDescription";
                    dllSite.DataValueField = "SiteId";
                    dllSite.DataBind();
                    dllSite.Items.Insert(0, new ListItem("--Select a Place--", null));


                    //dllSite.DataSource = db.Get_TRANSRequest_Site();
                    //dllSite.DataTextField = "SiteId";
                    //dllSite.DataValueField = "SiteDescription";
                    //dllSite.DataBind();
                    //dllSite.Items.Insert(0, new ListItem("--Select a Place--", null));


                    Tableau_EW_Request_ProcessRoleUser.DataSource = db.Crud_TRANSRequest_Place(null, null, null, true, null);
                    Tableau_EW_Request_ProcessRoleUser.DataBind();
                }

                // Au Autres chargement
                else
                {
                    // Initialisation des variables
                    int PlaceId, SiteId;
                    String PlaceDescription,action ;
              
                    bool Activated;

                    Activated = (ActivatedSet.Value == "1") ? true : false;
                    
                    action = ActionSet.Value;
                    PlaceDescription = PlaceDescriptionSet.Value;
                    Int32.TryParse(PlaceIdSet.Value, out PlaceId);

                    if (action == "UPDATE" && PlaceDescriptionSet.Value.Length > 3 && SiteIdSet.Value.Length > 3)
                    {
                        PlaceDescription = PlaceDescriptionSet.Value;
                        SiteId = Convert.ToInt32(SiteIdSet.Value);
                        Tableau_EW_Request_ProcessRoleUser.DataSource = db.Crud_TRANSRequest_Place(PlaceId, SiteId, PlaceDescription, Activated, action);
                        Tableau_EW_Request_ProcessRoleUser.DataBind();
                        Label5.Text = "Data Updated";
                    }
                }
            }
            catch (Exception) { }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {

            String SiteIdSet = dllSite.SelectedValue, PlaceDecriptionSet = PlaceDescription.Text, action = "INSERT";

            if (!String.IsNullOrEmpty(SiteIdSet) && !String.IsNullOrEmpty(PlaceDecriptionSet) && !String.IsNullOrEmpty(PlaceDescription.Text) && !String.IsNullOrEmpty(dllSite.SelectedValue))
            {
                //DateTime Startdt = DateTime.ParseExact(PlaceDescription.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                //DateTime Enddt = DateTime.ParseExact(SiteId.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                Tableau_EW_Request_ProcessRoleUser.DataSource = db.Crud_TRANSRequest_Place(null,   Convert.ToInt32(SiteIdSet), PlaceDecriptionSet,  true, action);
                Tableau_EW_Request_ProcessRoleUser.DataBind();

                dllSite.SelectedIndex = 0;
                PlaceDescription.Text = "";
                //SiteId.Text = "";
                Label5.Text = "Data inserted";
            }
            else
            {
                Label5.Text = "The field with * is required";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Tableau_EW_Request_ProcessRoleUser.DataSource = db.Crud_TRANSRequest_Place(null, null, null, true, null).Where(x => x.PlaceDescription.Contains(txtRequisitionID.Text) );
            Tableau_EW_Request_ProcessRoleUser.DataBind();
        }
    }
}