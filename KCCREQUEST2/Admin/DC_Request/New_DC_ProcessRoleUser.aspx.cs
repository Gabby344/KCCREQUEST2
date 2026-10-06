using KCCDBAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2.Admin.DC_Request
{
    public partial class New_DC_ProcessRoleUser : System.Web.UI.Page
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
                    if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "DCRequest").FirstOrDefault().Number == 0)
                    {

                        Response.Redirect("../default_admin.aspx");
                    }
                    Role.DataSource = db.Crud_DCRequestProcessRole(null, null, null, null);
                    Role.DataTextField = "Role";
                    Role.DataValueField = "Id";
                    Role.DataBind();
                    Role.Items.Insert(0, new ListItem("--Select a Role--", null));


                    dllUsers.DataSource = db.Get_Employee_User();
                    dllUsers.DataTextField = "FirstName";
                    dllUsers.DataValueField = "Login";
                    dllUsers.DataBind();
                    dllUsers.Items.Insert(0, new ListItem("--Select a Role--", null));


                    Tableau_EW_Request_ProcessRoleUser.DataSource = db.Crud_DCRequestProcessRoleUser(null, null, null, null, null, null, null, null);
                    Tableau_EW_Request_ProcessRoleUser.DataBind();
                }

                // Au Autres chargement
                else
                {
                    // Initialisation des variables
                    String UserLogin, Role, action;
                    DateTime StartDate, EndDate;
                    int ID;
                    bool Activated;
                    Activated = (ActivatedSet.Value == "1") ? true : false;
                    UserLogin = UserLoginSet.Value;
                    action = ActionSet.Value;
                    Role = RoleSet.Value;
                    Int32.TryParse(IDSet.Value, out ID);

                    if (action == "UPDATE" && StartDateSet.Value.Length > 3 && EndDateSet.Value.Length > 3)
                    {
                        StartDate = DateTime.Parse(StartDateSet.Value);
                        EndDate = DateTime.Parse(EndDateSet.Value);
                        Tableau_EW_Request_ProcessRoleUser.DataSource = db.Crud_DCRequestProcessRoleUser(ID, UserLogin, Role, StartDate, EndDate, Activated, 1, action);
                        Tableau_EW_Request_ProcessRoleUser.DataBind();
                        Label5.Text = "Data Updated";
                    }
                }
            }
            catch (Exception) { }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {

            String UserLoginSet = dllUsers.SelectedValue, RoleSet = Role.SelectedItem.Text, action = "INSERT";

            if (!String.IsNullOrEmpty(UserLoginSet) && !String.IsNullOrEmpty(RoleSet) && !String.IsNullOrEmpty(StartDate.Text) && !String.IsNullOrEmpty(EndDate.Text))
            {
                DateTime Startdt = DateTime.ParseExact(StartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                DateTime Enddt = DateTime.ParseExact(EndDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                Tableau_EW_Request_ProcessRoleUser.DataSource = db.Crud_DCRequestProcessRoleUser(null, UserLoginSet, RoleSet, Startdt, Enddt, true, 1, action);
                Tableau_EW_Request_ProcessRoleUser.DataBind();

                dllUsers.SelectedIndex = 0;
                Role.SelectedIndex = 0;
                StartDate.Text = "";
                EndDate.Text = "";
                Label5.Text = "Data inserted";
            }
            else
            {
                Label5.Text = "The field with * is required";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Tableau_EW_Request_ProcessRoleUser.DataSource = db.Crud_DCRequestProcessRoleUser(null, null, null, null, null, null, null, null).Where(x => x.Role.Contains(txtRequisitionID.Text) || x.UserLogin.Contains(txtRequisitionID.Text));
            Tableau_EW_Request_ProcessRoleUser.DataBind();
        }
    }
}