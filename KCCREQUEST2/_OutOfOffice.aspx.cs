using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.Globalization;
using System.Net;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace KCCREQUEST2
{
    public partial class _OutOfOffice : BasePage
    {

        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    
                    PopulateEmployees();
                    PopulateModule();
                    PopulateGrid();
                }
                
                catch (Exception ex)
                {
                    Response.Redirect("pageError.aspx");
                }
            }
        }
        protected void LstForUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateGrid();
            PopulateModule();
        }
        protected void lstModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "multiselect();", true);
                var user = Developper.Employee.Get_AD_User(LstForUser.SelectedValue);
                string login = ((user != null) && (user.Count > 0)) ? user.FirstOrDefault().Samaccountname : "";
                PopulateRoles(login);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //try
            //{
               

                if (ValidateInputs())
                {
                    string applicationType = "";
                    string rolesMail = "<br />";
                    string moduleRolemail = "", module = "";
                    applicationType = "Others Applications";
                    var user = Developper.Employee.Get_AD_User(LstForUser.SelectedValue);
                    string userLogin = ((user != null) && (user.Count > 0)) ? user.FirstOrDefault().Samaccountname : "";
                    if (LstRole.GetSelectedIndices().Contains(0))
                    {
                        foreach (ListItem role in LstRole.Items)
                        {
                            moduleRolemail = "";
                            if (role.Text != "ALL")
                            {
                                if (role.Text.Contains("- Default Role"))
                                {
                                    string matriculeRole, moduleID;
                                    matriculeRole = role.Value.Split('#')[1];
                                    moduleID = role.Value.Split('#')[0];
                                    moduleRolemail = role.Text;
                                    db.Insert_OutOfOffices(LstForUser.SelectedValue, LstForUser.SelectedItem.Text, LstAssignto.SelectedValue, LstAssignto.SelectedItem.Text, moduleID, matriculeRole, GetStartDatetTime(), GetEndDatetTime(), txtComment.Text, utility.Login(loggedUser), "");
                                }
                                else
                                {
                                    moduleRolemail = role.Value.Split('^')[0] + " - " + role.Text;
                                    if (role.Value == "ORB")
                                    {
                                        List<string> allORBProcess = db.Get_Role_ByUser("ORB", userLogin).Select(x => x.SubModuleID).Distinct().ToList();
                                        foreach (var orbProces in allORBProcess)
                                        {
                                            db.Insert_OutOfOffices(LstForUser.SelectedValue, LstForUser.SelectedItem.Text, LstAssignto.SelectedValue, LstAssignto.SelectedItem.Text, role.Value.Split('^')[0], role.Text, GetStartDatetTime(), GetEndDatetTime(), txtComment.Text, utility.Login(loggedUser), orbProces);
                                        }
                                    }
                                    else
                                    {
                                        db.Insert_OutOfOffices(LstForUser.SelectedValue, LstForUser.SelectedItem.Text, LstAssignto.SelectedValue, LstAssignto.SelectedItem.Text, role.Value.Split('^')[0], role.Text, GetStartDatetTime(), GetEndDatetTime(), txtComment.Text, utility.Login(loggedUser), "");
                                    }
                                }
                                rolesMail = rolesMail + moduleRolemail + "<br />";
                            }
                        }
                    }
                    else
                    {
                        foreach (ListItem role in LstRole.Items)
                        {
                            moduleRolemail = "";
                            if ((role.Text != "ALL") && (role.Selected))
                            {
                                if (role.Text.Contains("- Default Role"))
                                {
                                    string matriculeRole, moduleID;
                                    matriculeRole = role.Value.Split('#')[1];
                                    moduleID = role.Value.Split('#')[0];
                                    moduleRolemail = role.Text;
                                    db.Insert_OutOfOffices(LstForUser.SelectedValue, LstForUser.SelectedItem.Text, LstAssignto.SelectedValue, LstAssignto.SelectedItem.Text, moduleID, matriculeRole, GetStartDatetTime(), GetEndDatetTime(), txtComment.Text, utility.Login(loggedUser), "");
                                }
                                else
                                {
                                    moduleRolemail = role.Value.Split('^')[0] + " - " + role.Text;
                                    //db.Insert_OutOfOffices(LstForUser.SelectedValue, LstForUser.SelectedItem.Text, LstAssignto.SelectedValue, LstAssignto.SelectedItem.Text, role.Value, role.Text, GetStartDatetTime(), GetEndDatetTime(), txtComment.Text, utility.Login(loggedUser));
                                    if (role.Value == "ORB")
                                    {
                                        List<string> allORBProcess = db.Get_Role_ByUser("ORB", userLogin).Select(x => x.SubModuleID).Distinct().ToList();
                                        foreach (var orbProces in allORBProcess)
                                        {
                                            db.Insert_OutOfOffices(LstForUser.SelectedValue, LstForUser.SelectedItem.Text, LstAssignto.SelectedValue, LstAssignto.SelectedItem.Text, role.Value.Split('^')[0], role.Text, GetStartDatetTime(), GetEndDatetTime(), txtComment.Text, utility.Login(loggedUser), orbProces);
                                        }
                                    }
                                    else
                                    {
                                        db.Insert_OutOfOffices(LstForUser.SelectedValue, LstForUser.SelectedItem.Text, LstAssignto.SelectedValue, LstAssignto.SelectedItem.Text, role.Value.Split('^')[0], role.Text, GetStartDatetTime(), GetEndDatetTime(), txtComment.Text, utility.Login(loggedUser), "");
                                    }
                                }
                                rolesMail = rolesMail + moduleRolemail + "<br />";
                            }
                        }
                    }
                    if (chkSetOof.Checked)
                    {
                        string loginSmtp = utility.GetInformationFromADC(LstForUser.SelectedValue).SingleOrDefault().Email;
                        try
                        {
                            
                            SetOof(loginSmtp, txtMsgReply.Text, GetStartDatetTime(), GetEndDatetTime());
                        }
                        catch (Exception ex)
                        {

                            lblErreur.Text = ex.Message + "<br />" + ex.InnerException;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                        }
                        
                    }
                    db.Send_MailOutOfOffice(LstForUser.SelectedItem.Value, LstAssignto.SelectedItem.Value, GetStartDatetTime().ToString(), GetEndDatetTime().ToString(), rolesMail, txtComment.Text);
                    PopulateGrid();
                    Response.Redirect(Request.RawUrl);
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                    
                }
            //}
            //catch (FormatException ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}
            //catch (NullReferenceException ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}
            //catch (IndexOutOfRangeException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}
            //catch (ArithmeticException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}

            //catch (TimeoutException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}

            //catch (SystemException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}

            //catch (Exception ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}
        }
        void ClearFields()
        {
            txtComment.Text = "";
            //UpdatePanelModuleUsers.Update();
            lstModule.SelectionMode = ListSelectionMode.Multiple;
            LstRole.SelectionMode = ListSelectionMode.Multiple;
        }
        void PopulateModule()
        {
            try
            {
                lstModule.DataSource = db.Get_Module();
                lstModule.DataTextField = "ModuleDescription";
                lstModule.DataValueField = "ModuleID";
                lstModule.DataBind();
                lstModule.Items.Insert(0, "ALL");
                LstRole.Items.Clear();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        void PopulateRoles(string userLogin)
        {
            try
            {
                LstRole.Items.Clear();

                if (lstModule.GetSelectedIndices().Contains(0))
                {
                    foreach (ListItem item in lstModule.Items)
                    {
                        if (item.Text != "ALL")
                        {
                            var roles = db.Get_Role_ByUser(item.Value, userLogin).OrderBy(x=>x.Role);
                            foreach (var userRole in roles)
                            {
                                ListItem newItem = new ListItem();
                                newItem.Text = userRole.Role;
                                newItem.Value = userRole.ModuleID+"^"+userRole.Role;
                                if ((userRole.ModuleID == "ORB") && (LstRole.Items.FindByText(newItem.Text) != null))
                                {
                                    ;
                                }
                                else
                                {
                                    LstRole.Items.Add(newItem);
                                }
                                
                            }
                            if (item.Value != "ORB")
                            {
                                ListItem defaultRole = new ListItem();
                                defaultRole.Text = item.Text + " - Default Role";
                                defaultRole.Value = item.Value + "#" + LstForUser.SelectedValue;
                                LstRole.Items.Add(defaultRole);
                            }

                        }
                    }
                }
                else
                {
                    foreach (ListItem item in lstModule.Items)
                    {
                        if ((item.Selected) && (item.Text != "ALL"))
                        {
                            var roles = db.Get_Role_ByUser(item.Value, userLogin).OrderBy(x => x.Role);
                            foreach (var userRole in roles)
                            {
                                ListItem newItem = new ListItem();
                                newItem.Text = userRole.Role;
                                newItem.Value = userRole.ModuleID + "^" + userRole.Role;

                                if ((userRole.ModuleID == "ORB") && (LstRole.Items.FindByText(newItem.Text) != null))
                                {
                                    ;
                                }
                                else
                                {
                                    LstRole.Items.Add(newItem);
                                }
                            }
                            if (item.Value != "ORB")
                            {
                                ListItem defaultRole = new ListItem();
                                defaultRole.Text = item.Text + " - Default Role";
                                defaultRole.Value = item.Value + "#" + LstForUser.SelectedValue;
                                LstRole.Items.Add(defaultRole);
                            }
                        }
                    }
                }
                if (LstRole.Items.Count > 0)
                {
                   
                    LstRole.Items.Insert(0, "ALL");
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        void PopulateGrid()
        {
            try
            {
                var user = Developper.Employee.Get_AD_User(LstForUser.SelectedValue);
                string login = ((user != null) && (user.Count > 0)) ? user.FirstOrDefault().Samaccountname : "";
                dgvActiveOutoffice.DataSource = db.Get_OutOfOffices_ByUser(login).Where(x => x.StatusID == 1).ToList();
                dgvActiveOutoffice.DataBind();

                dgvInactiveOutOffice.DataSource = db.Get_OutOfOffices_ByUser(login).Where(x => x.StatusID == 0 || x.StatusID==2).ToList(); ;
                dgvInactiveOutOffice.DataBind();

                lblActive.InnerText = db.Get_OutOfOffices_ByUser(login).Where(x => x.StatusID == 1).Count().ToString();
                lblInactive.InnerText = db.Get_OutOfOffices_ByUser(login).Where(x => x.StatusID == 0||x.StatusID==2).Count().ToString();

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        void PopulateEmployees()
        {
            try
            {
                LstForUser.DataSource = db.Get_Employee_User();
                LstForUser.DataTextField = "FirstName";
                LstForUser.DataValueField = "EmployeeId";
                LstForUser.DataBind();
               
                LstForUser.Enabled = utility.IsAdmin(utility.Login(loggedUser));
                LstForUser.Items.Insert(0, "N/A");
                LstForUser.SelectedIndex = LstForUser.Items.IndexOf(LstForUser.Items.FindByValue(utility.GetInformationFromADC(utility.Login(loggedUser)).FirstOrDefault().EmployeeID));

                LstAssignto.DataSource = db.Get_Employee_User();
                LstAssignto.DataTextField = "FirstName";
                LstAssignto.DataValueField = "EmployeeId";
                LstAssignto.DataBind();
                LstAssignto.Items.Insert(0, "N/A");
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }
        DateTime GetStartDatetTime()
        {
            try
            {
                DateTime dateRetour = new DateTime();
                if ((!string.IsNullOrEmpty(txtDateFrom.Text)) && (!string.IsNullOrEmpty(txtTimeRangeStart.Text)))
                {
                    string heure;
                    int heureSimple = int.Parse(txtTimeRangeStart.Text.Split(':')[0]);
                    heure = (heureSimple >= 10) ? txtTimeRangeStart.Text : "0" + txtTimeRangeStart.Text;

                    dateRetour = DateTime.ParseExact(txtDateFrom.Text + " " + heure, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                return dateRetour;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        DateTime GetEndDatetTime()
        {
            try
            {
                DateTime dateRetour = new DateTime();
                if ((!string.IsNullOrEmpty(txtDateTo.Text)) && (!string.IsNullOrEmpty(txtTimeRangeEnd.Text)))
                {
                    string heure;
                    int heureSimple = int.Parse(txtTimeRangeEnd.Text.Split(':')[0]);
                    heure = (heureSimple >= 10) ? txtTimeRangeEnd.Text : "0" + txtTimeRangeEnd.Text;

                    dateRetour = DateTime.ParseExact(txtDateTo.Text + " " + heure, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                return dateRetour;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        bool ValidateInputs()
        {
            try
            {
                bool resp = true;
                string error = string.Empty;
                if (string.IsNullOrEmpty(txtComment.Text) || string.IsNullOrWhiteSpace(txtComment.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                    resp = false;
                }
                if (lstModule.GetSelectedIndices().ToList().Count <= 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstModuleEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstModuleEmptyError");
                    resp = false;
                }
                if (LstRole.GetSelectedIndices().ToList().Count <= 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstRoleEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstRoleEmptyError");
                    resp = false;
                }
                if (LstForUser.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstAssignEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstAssignEmptyError");
                    resp = false;
                }
                if (LstAssignto.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstAssignEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstAssignEmptyError");
                    resp = false;
                }
                if (string.IsNullOrEmpty(txtDateFrom.Text) || string.IsNullOrWhiteSpace(txtDateFrom.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDateFromEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDateFromEmptyError");
                    resp = false;
                }
                if (string.IsNullOrEmpty(txtDateTo.Text)|| string.IsNullOrWhiteSpace(txtDateTo.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDateToEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDateToEmptyError");
                    resp = false;
                }
                if (string.IsNullOrEmpty(txtTimeRangeStart.Text)|| string.IsNullOrWhiteSpace(txtTimeRangeStart.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtTimeStartEmpty") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtTimeStartEmpty");
                    resp = false;
                }
                if (string.IsNullOrEmpty(txtTimeRangeEnd.Text)|| string.IsNullOrWhiteSpace(txtTimeRangeEnd.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtTimeEndEmpty") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtTimeEndEmpty");
                    resp = false;
                }
                if ((!string.IsNullOrEmpty(txtDateFrom.Text)) && (!string.IsNullOrEmpty(txtDateTo.Text)) && (!string.IsNullOrEmpty(txtTimeRangeStart.Text)) && (!string.IsNullOrEmpty(txtTimeRangeEnd.Text)))
                {
                    if ((this.GetStartDatetTime() < DateTime.Now.Date) || (GetEndDatetTime() <= GetStartDatetTime()))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtStartDateError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtStartDateError");
                        resp = false;
                    }
                }
                if((chkSetOof.Checked) && (string.IsNullOrEmpty(txtMsgReply.Text.Trim())))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lblReplyMessageError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lblReplyMessageError");
                    resp = false;
                }
                if (resp == false)
                {
                    lblErreur.Text = error;

                }
                return resp;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        protected void btnDisable_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton disabled = (ImageButton)sender;
                foreach (GridViewRow row in dgvActiveOutoffice.Rows)
                {
                    ImageButton imgLoop = (ImageButton)row.FindControl("btnDisable");
                    if (imgLoop.Equals(disabled))
                    {
                        Label lableId = (Label)row.FindControl("lblID");
                        int id = int.Parse(lableId.Text);
                        db.Update_OutOfOffices_Status(id, 0, utility.Login(loggedUser));
                        PopulateGrid();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void chkSetOof_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSetOof.Checked)
                {
                    pnlMsg.Visible = true;
                    txtMsgReply.Text = GenerateMessage();
                    
                }
                else
                {
                    pnlMsg.Visible = false;
                    txtMsgReply.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        string GenerateMessage()
        {
            string msg = string.Empty;
            string replacementname = "";
            if ((!string.IsNullOrEmpty(LstAssignto.SelectedValue)) && (LstAssignto.SelectedIndex != 0))
            {

                replacementname = LstAssignto.SelectedItem.Text + " ( " + GetMail(LstAssignto.SelectedValue)+ " , " + GetPhone(LstAssignto.SelectedValue)+" )";

            }
            msg = string.Format("Greetings,I am currently out of office from {1} to {2}.Please contact {0} .Regards,\nBonjour,Je suis présentement en dehors du bureau du {1} au {2}.Veuillez contactez {0} .Cordialement,", replacementname, GetStartDatetTime().ToString(), GetEndDatetTime().ToString());
            return msg;
        }
        string GetMail(string employeeID)
        {
            var repl = db.Get_Employee_ByID_Or_Login(employeeID);
            string repMail = "";
            if (repl != null)
            {
                repMail = repl.Mail;
            }
            return repMail;
        }
        string GetPhone(string employeeID)
        {
            var repl = db.Get_Employee_ByID_Or_Login(employeeID);
            string repPhone = "";
            if (repl != null)
            {
                repPhone = repl.TelephoneNumberAD;
            }
            return repPhone;
        }
        private bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            bool result = false;
            Uri redirectionUri = new Uri(redirectionUrl);
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }
        public void SetOof(string fromLogin, string message, DateTime fromDate, DateTime toDate)
        {
            /*ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010);
            service.Credentials = CredentialCache.DefaultNetworkCredentials;
            service.TraceEnabled = true;
            service.TraceFlags = TraceFlags.All;
            service.AutodiscoverUrl(fromLogin, RedirectionUrlValidationCallback);


            OofSettings myOof = new OofSettings();
            myOof.State = Microsoft.Exchange.WebServices.Data.OofState.Scheduled;
            myOof.ExternalAudience = OofExternalAudience.All;
            myOof.Duration = new TimeWindow(fromDate, toDate);
            myOof.ExternalReply = new OofReply(message);
            myOof.InternalReply = new OofReply(message);
            service.SetUserOofSettings(fromLogin, myOof);*/
        }


        protected void Selectall_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvActiveOutoffice.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Enabled == true)
                        chkRow.Checked = true;
                }
            }
        }

        protected void Deselectall_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvActiveOutoffice.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Enabled == true)
                        chkRow.Checked = false;
                }
            }
        }

        protected void Inverse_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvActiveOutoffice.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Enabled == true)
                    {

                        if (chkRow.Checked == true) chkRow.Checked = false;
                        else if (chkRow.Checked == false) chkRow.Checked = true;
                    }

                }
            }
        }

        protected void btnDisable_Click1(object sender, EventArgs e)
        {
            try
            {

                if (dgvActiveOutoffice.Rows.Count == 0)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "dgvActiveOutOfficeEmptyError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else if (NbreSelectItem() == 0) {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "NbreSelectItemEmptyError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    foreach (GridViewRow row in dgvActiveOutoffice.Rows)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        if (chkRow.Checked == true)
                        {
                            Label lableId = (Label)row.FindControl("lblID");
                            int id = int.Parse(lableId.Text);
                            db.Update_OutOfOffices_Status(id, 0, utility.Login(loggedUser));
                            
                        }

                        PopulateGrid();
                      
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public int NbreSelectItem()
        {

            int nbre = 0;
            foreach (GridViewRow row in dgvActiveOutoffice.Rows)
            {


                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                

                if (chkRow.Checked == true)
                {
                    nbre++;
                }

            }
            return nbre;
        }

        protected void LstRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "multiselect();", true);
        }
        protected void dgvActiveOutoffice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvActiveOutoffice.PageIndex = e.NewPageIndex;
            PopulateGrid();
        }

        protected void dgvInactiveOutOffice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvInactiveOutOffice.PageIndex = e.NewPageIndex;
            PopulateGrid();
        }
    }
}