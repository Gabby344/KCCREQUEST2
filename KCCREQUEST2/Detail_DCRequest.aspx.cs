using KCCDBAccess;
using KCCREQUEST2.form;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2
{
    public partial class Detail_DCRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                if (!Page.IsPostBack)
                {
                    // PERFORMANCE FIX: Populate static data from cache first
                    PopulateStaticDataFromCache();
                    
                    // Initialize UI state
                    InitializeUIState();

                    if (Request.Params["DCrequestID"] != null)
                    {
                        int id = int.Parse(Request.Params["DCrequestID"]);
                        
                        // PERFORMANCE FIX: Use optimized data loading (includes permission configuration)
                        LoadDCRequestData(id);
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("pageError.aspx");
            //}
        }

        private void PopulateStaticDataFromCache()
        {
            // CRITICAL FIX: Always populate dropdowns to prevent empty dropdown issue
            // The previous caching logic was preventing dropdown population on subsequent page loads
            
            PopulateCategory();
            PopulatePriority();
            PopulateDocumentType(CachedLogin);
            PopulateDocumentLangue();
            PopulateCostCenter();
            PopulateService();
            PopulateDCRequest_Type();
            populateUsers();
            populateRole();
            populateUsersAdHoc();
            populateRoleHistory();
            populateStatusHistory();
            
            // Note: If we want to cache dropdown data in the future, we should cache the actual
            // data results in each populate method, not skip the population entirely
        }

        private void InitializeUIState()
        {
            disableControl(false);
            btnAdHoc.Visible = false;
            btnApproval.Visible = false;
            btnBackToOriginator.Visible = false;
            BtnReject.Visible = false;
            BtnReturnToOrginator.Visible = false;
            btnUpdate.Visible = false;
            btnHistory.Visible = false;
            txtDocumentNumber.ReadOnly = true;
            ddlApplicability.SelectedValue = "N/A";

            //set visibility of applicability related UIs 
            TrApplicability1.Visible = false;
            TrApplicability2.Visible = false;
            CheckBoxListApplicableAreas.Enabled = false;
            CheckBoxListApprovalReviewers.Enabled = false;
            CheckBoxListRolloutChannels.Enabled = false;
            ddlApplicability.Enabled = false;
        }

        
        private void SetSelectedValues(CheckBoxList chklistBox, List<int> selectedValues)
        {
            // Use HashSet for faster lookups if the list is large
            HashSet<int> selectedValuesSet = new HashSet<int>(selectedValues);

            foreach (ListItem item in chklistBox.Items)
            {
                // Use int.TryParse to safely parse the item value
                if (int.TryParse(item.Value, out int itemValue))
                {
                    item.Selected = selectedValuesSet.Contains(itemValue);
                }
                else
                {
                    // Handle cases where item.Value is not a valid integer
                    item.Selected = false;
                }
            }
        }

        private void BindApplicabilityListBoxItems()
        {

            CheckBoxListApplicableAreas.DataSource = db.Get_DCRequest_ApplicabilityAreas(); // Example method to get data
            CheckBoxListApplicableAreas.DataTextField = "Description"; // Adjust based on your data structure
            CheckBoxListApplicableAreas.DataValueField = "ID";
            CheckBoxListApplicableAreas.DataBind();

            CheckBoxListApprovalReviewers.DataSource = db.Get_DCRequest_ApplicabilityReviewGroups(); // Example method to get data
            CheckBoxListApprovalReviewers.DataTextField = "Description"; // Adjust based on your data structure
            CheckBoxListApprovalReviewers.DataValueField = "ID";
            CheckBoxListApprovalReviewers.DataBind();

            CheckBoxListRolloutChannels.DataSource = db.Get_DCRequest_RolloutChannel(); // Example method to get data
            CheckBoxListRolloutChannels.DataTextField = "Channel"; // Adjust based on your data structure
            CheckBoxListRolloutChannels.DataValueField = "ID";
            CheckBoxListRolloutChannels.DataBind();
        }

        public void PopulateEmployeeAndDepartment(string requestor)
        {
            try
            {
                string CachedLogin = Session["CurrentCachedLogin"] as string;
                if (int.Parse(Developper.Employee.Get_Employee_Type(requestor)) == 1)
                {
                    var listDepartement = Developper.Employee.Get_Employee_Of_Department(requestor, "").ToList();
                    LstRequestor.DataSource = listDepartement;
                    LstRequestor.DataTextField = "FirstName";
                    LstRequestor.DataValueField = "Login";
                    LstRequestor.DataBind();
                    LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));



                    //Loading Departments
                    dllDepartment.DataSource = listDepartement.GroupBy(x => x.DepartmentCode).Select(x => x.First());
                    dllDepartment.DataTextField = "DepartmentCode";
                    dllDepartment.DataValueField = "DepartmentCode";
                    dllDepartment.DataBind();
                    dllDepartment.Enabled = false;
                }
                else
                {
                    dllDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    dllDepartment.DataTextField = "LongDescription";
                    dllDepartment.DataValueField = "Code";
                    dllDepartment.DataBind();
                    dllDepartment.Enabled = true;

                }

                if (db.Get_KCCRequestIsAdmin(CachedLogin).Where(x => x.ModuleID == "DCRequest").FirstOrDefault().Number == 1)
                {

                    dllDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    dllDepartment.DataTextField = "LongDescription";
                    dllDepartment.DataValueField = "Code";
                    dllDepartment.DataBind();
                    dllDepartment.Enabled = true;

                    dllDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(requestor, "").FirstOrDefault().DepartmentCode;

                    LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
                    LstRequestor.DataTextField = "FirstName";
                    LstRequestor.DataValueField = "Login";
                    LstRequestor.DataBind();
                    LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));


                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateCategory()
        {
            try
            {
                // PERFORMANCE OPTIMIZATION: Cache category data in session
                var cacheKey = $"CategoryData_{CachedLogin}";
                var cachedData = Session[cacheKey] as List<dynamic>;
                
                if (cachedData == null)
                {
                    cachedData = db.Get_KCCRequest_Category(CachedLogin).Cast<dynamic>().ToList();
                    Session[cacheKey] = cachedData;
                }

                LstCategory.DataSource = cachedData;
                LstCategory.DataValueField = "CategoryID";
                LstCategory.DataTextField = "CategoryDescprition";
                LstCategory.DataBind();
                LstCategory.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllcategorie"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public void PopulatePriority()
        {
            try
            {
                LstPriority.DataSource = db.Get_KCCRequest_Priority(CachedLogin);
                LstPriority.DataValueField = "PrioriteID";
                LstPriority.DataTextField = "PrioriteDescprition";
                LstPriority.DataBind();
                LstPriority.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllpriority"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateDCRequest_Type()
        {
            try
            {

                ddlRequestType.DataSource = db.Get_DCRequest_Type().ToList();
                ddlRequestType.DataValueField = "TypeId";
                ddlRequestType.DataTextField = "TypeDescription";
                ddlRequestType.DataBind();
                ddlRequestType.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDCRType"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateDocumentType(string login)
        {
            LstDocumentType.DataSource = db.Get_DCRequest_DocumentType(CachedLogin);
            LstDocumentType.DataValueField = "DocTypeId";
            LstDocumentType.DataTextField = "DocTypeDescription";
            LstDocumentType.DataBind();
            LstDocumentType.Items.Insert(0, "N/A");

        }

        public void PopulateDocumentLangue()
        {
            LstDocumentLangueID.DataSource = db.Get_KCCRequest_Lang();
            LstDocumentLangueID.DataValueField = "LangId";
            LstDocumentLangueID.DataTextField = "LangDescription";
            LstDocumentLangueID.DataBind();
            LstDocumentLangueID.Items.Insert(0, "N/A");

        }

        public void PopulateCostCenter()
        {
            LstCostCenter.DataSource = db.Get_RECTRequest_CostCenter();
            LstCostCenter.DataValueField = "CostCenterID";
            LstCostCenter.DataTextField = "CostCenterDescription";
            LstCostCenter.DataBind();
            LstCostCenter.Items.Insert(0, "N/A");

        }
        public void populateUsers()
        {

            dllUsers.DataSource = db.Get_Employee_User();
            dllUsers.DataTextField = "FirstName";
            dllUsers.DataValueField = "Login";
            dllUsers.DataBind();
            dllUsers.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlluser"));
        }

        protected void dllUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string employedID = db.Get_Employee_User().Where(x => x.Login == dllUsers.SelectedValue).FirstOrDefault().EmployeeId;

            int Isadmi = Convert.ToInt32((db.Get_KCCRequestIsAdmin(dllUsers.SelectedValue).Where(x => x.ModuleID == "DCRequest").FirstOrDefault().Number.ToString()));

            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "DCRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));
            dllRole.Items.Insert(1, employedID);
            if (Isadmi > 0)
            { dllRole.Items.Insert(2, "ADMIN"); }

        }
        public void populateRole()
        {

            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "DCRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));

        }
        public void populateUsersAdHoc()
        {

            dllUserAdHoc.DataSource = db.Get_Employee_User();
            dllUserAdHoc.DataTextField = "FirstName";
            dllUserAdHoc.DataValueField = "EmployeeId";
            dllUserAdHoc.DataBind();
            dllUserAdHoc.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlluser"));
        }
        public void PopulateService()
        {
            try
            {
                // PERFORMANCE OPTIMIZATION: Cache service data in session
                var cacheKey = $"ServiceData_{CachedLogin}";
                var cachedData = Session[cacheKey] as List<dynamic>;
                
                if (cachedData == null)
                {
                    cachedData = db.Get_DCRequest_Service(CachedLogin).Cast<dynamic>().ToList();
                    Session[cacheKey] = cachedData;
                }

                ddlService.DataSource = cachedData;
                ddlService.DataValueField = "ServiceID";
                ddlService.DataTextField = "ServiceDescription";
                ddlService.DataBind();
                ddlService.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllService"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void populateRoleHistory()
        {

            ddlRoleHistory.DataSource = db.Get_Employee_User();
            ddlRoleHistory.DataTextField = "FirstName";
            ddlRoleHistory.DataValueField = "EmployeeId";
            ddlRoleHistory.DataBind();
            ddlRoleHistory.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));

        }

        public void populateStatusHistory()
        {
            ddlStatusHistory.DataSource = db.Crud_DCRequest_Status(null, null, null, null, null, null, null, null, null, null, null, null, null);
            ddlStatusHistory.DataValueField = "StatusID";
            ddlStatusHistory.DataTextField = "StatusDescription";
            ddlStatusHistory.DataBind();
            ddlStatusHistory.Items.Insert(0, "N/A");
        }
        public bool ValidInputs()
        {
            bool isValid = true;
            string error = "";
            if (LstRequestor.SelectedIndex == 0)
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError");
                isValid = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (txtBeneficiary.Text == string.Empty || string.IsNullOrWhiteSpace(txtBeneficiary.Text))
            {
                //lblErreur.Text = "Beneficiary cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryEmptyError");
                isValid = false;

            }
            if (txtBeneficiaryJobTitle.Text == string.Empty || string.IsNullOrWhiteSpace(txtBeneficiaryJobTitle.Text))
            {
                //lblErreur.Text = "Beneficiary job title cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryJobTitleEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryJobTitleEmptyError");
                isValid = false;

            }
            if (txtRequestDescription.Text == string.Empty || string.IsNullOrWhiteSpace(txtRequestDescription.Text))
            {
                //lblErreur.Text = "Request Description cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError");
                isValid = false;

            }
            if (txtJustificationWhy.Text == string.Empty || string.IsNullOrWhiteSpace(txtJustificationWhy.Text))
            {
                //lblErreur.Text = "Justification cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                isValid = false;

            }
            if (ddlService.SelectedIndex == 0)
            {
                //lblErreur.Text = "Priority cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstServiceSecEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstServiceSecEmptyError");
                isValid = false;

            }
            if (LstCategory.SelectedIndex == 0)
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError");
                isValid = false;
            }
            if (LstPriority.SelectedIndex == 0)
            {
                //lblErreur.Text = "Priority cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError");
                isValid = false;

            }

            if (txtPlannedStartDate.Text == string.Empty || string.IsNullOrWhiteSpace(txtPlannedStartDate.Text))
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError");
                isValid = false;
            }

            if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text)) && ((DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) <= DateTime.Today.AddDays(-1)))
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess");
                isValid = false;
            }

            if (txtRisk.Text == string.Empty || string.IsNullOrWhiteSpace(txtRisk.Text))
            {
                //lblErreur.Text = "Justification cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtRiskEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtRiskEmptyError");
                isValid = false;

            }

            if (isValid == false)
            {
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            return isValid;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblaction.Text = "10";
            //attachement.Visible = false;
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnUpdate.Text;
            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalComment();", true);
        }

        protected void dllDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

            LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
            LstRequestor.DataTextField = "FirstName";
            LstRequestor.DataValueField = "Login";
            LstRequestor.DataBind();
            LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDepartement"));

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtComment.Text == string.Empty || string.IsNullOrWhiteSpace(txtComment.Text))
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            else
            {
                try
                {
                    if (lblaction.Text == "10")
                    {

                        int? DocTypeID = null; int? DocLangID = null;
                        if (LstDocumentType.SelectedIndex != 0) DocTypeID = int.Parse(LstDocumentType.SelectedValue);
                        if (LstDocumentLangueID.SelectedIndex != 0) DocLangID = int.Parse(LstDocumentLangueID.SelectedValue);

                        int? DocVersion = null;
                        if (txtDocVersion.Text != string.Empty) DocVersion = int.Parse(txtDocVersion.Text);

                        DateTime? DocVersionDate = null;
                        if (txtDocumentVersionDate.Text != string.Empty) DocVersionDate = DateTime.ParseExact(txtDocumentVersionDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);

                        DateTime? DocReviewDate = null;
                        if (txtDocReviewVersionDate.Text != string.Empty) DocReviewDate = DateTime.ParseExact(txtDocReviewVersionDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);

                        bool HardCopy = false;
                        if (ddlDocHarCopy.SelectedValue == "1") HardCopy = true;

                        int? TransmmitId = null;
                        if (txtDocTransmittalNo.Text != string.Empty) TransmmitId = int.Parse(txtDocTransmittalNo.Text);

                        db.DCRequest_Update(int.Parse(Request.Params["DCrequestID"]), LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtBeneficiary.Text, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtComment.Text, txtUrgencyComment.Text, DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture),
                            LstCostCenter.SelectedValue, dllDepartment.SelectedValue, txtArea.Text, txtRisk.Text, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), txtDocumentNumber.Text, txtDocumentVersionCode.Text, DocTypeID, txtDocumentTitle.Text, DocLangID, DocVersion,
                                                DocVersionDate, DocReviewDate, txtDocTranslator.Text, HardCopy, txtDocLink.Text, TransmmitId, txtAdditionalNotedId.Text, int.Parse(ddlService.SelectedValue), CachedLogin, int.Parse(ddlRequestType.SelectedValue));
                        //lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                        //lblIDRequest.Text = ID.ToString();
                        //ModalPopupExtender1.Show();

                    }
                    else if (lblaction.Text == "1")
                    {

                        db.DCRequest_Approve(int.Parse(Request["DCrequestID"]), txtComment.Text, CachedLogin);
                        //Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }
                    else if (lblaction.Text == "3")
                    {
                        db.DCRequest_Reject(int.Parse(Request["DCrequestID"]), txtComment.Text, CachedLogin);
                        //Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }
                    else if (lblaction.Text == "4")
                    {
                        db.DCRequest_Return(int.Parse(Request["DCrequestID"]), txtComment.Text, CachedLogin);
                        //Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }
                    else if (lblaction.Text == "5")
                    {
                        db.DCRequest_Back(int.Parse(Request["DCrequestID"]), txtComment.Text, CachedLogin);
                        //Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }

                    Insert_DCRequest_Files(int.Parse(Request["DCrequestID"]));
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "HideModalComment();", true);
                    //lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    lblIDRequest.Text = ID.ToString();
                    ModalPopupExtender1.Show();

                }
                catch (FormatException ex)
                {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                catch (NullReferenceException ex)
                {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                catch (IndexOutOfRangeException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                catch (ArithmeticException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }

                catch (TimeoutException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }

                catch (SystemException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }

                catch (Exception ex)
                {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Default_View_Approve.aspx");
            Response.Redirect("My_RequestToAproval.aspx?type=DCRequest");
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {


            lblaction.Text = "3";
            //attachement.Visible = false;
            //lblFile.Visible = false;
            //FileUpload2.Visible = false;
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + BtnReject.Text;

            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalComment();", true);

        }

        protected void BtnReturnToOrginator_Click(object sender, EventArgs e)
        {
            lblaction.Text = "4";
            //attachement.Visible = false;
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + BtnReturnToOrginator.Text;
            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalComment();", true);

        }

        protected void btnBackToOriginator_Click(object sender, EventArgs e)
        {
            lblaction.Text = "5";
            //attachement.Visible = false;
            //lblFile.Visible = false;
            //FileUpload2.Visible = false;
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnBackToOriginator.Text;
            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalComment();", true);
        }
        protected void btnrRealocate_Click(object sender, EventArgs e)
        {
            txtCommentReallocate.Text = string.Empty;
            this.Form.DefaultButton = this.BtnSaveReallocate.UniqueID;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openReallocate();", true);
        }

        protected void BtnSaveReallocate_Click(object sender, EventArgs e)
        {
            //try
            //{

            if (dllUsers.SelectedIndex == 0)
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllUsersError");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            else if (dllRole.SelectedIndex == 0)
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllRoleError");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txtCommentReallocate.Text == string.Empty || string.IsNullOrWhiteSpace(txtCommentReallocate.Text))
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ddlStatusHistory.SelectedIndex == 0)
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllStatusError");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                DateTime? SignD = null;
                if (!string.IsNullOrEmpty(txtSignDate.Text)) SignD = DateTime.ParseExact(txtSignDate.Text, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture); 

                int id = int.Parse(Request.Params["DCrequestID"]);
                var rect = db.Get_DCRequest_ById(id, CachedLogin);

                db.DCRequest_Reallocate(int.Parse(Request.Params["DCrequestID"]), ddlStatusHistory.SelectedItem.Text, txtCommentReallocate.Text, dllRole.SelectedValue, CachedLogin, int.Parse(ddlStatusHistory.SelectedValue), SignD);
                Insert_DCRequest_Files(int.Parse(Request["DCrequestID"]));

                //lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideReallocate();", true);
                //ModalPopupExtender1.Show();
                //Response.Redirect("Admin/HR_Request/HRRequisition.aspx");

                Response.Redirect("Detail_DCRequest.aspx?DCrequestID=" + int.Parse(Request["DCrequestID"]));
            }
            //}
            //catch (FormatException ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal;", true);
            //}
            //catch (NullReferenceException ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal;", true);
            //}
            //catch (IndexOutOfRangeException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal;", true);
            //}
            //catch (ArithmeticException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal;", true);
            //}

            //catch (TimeoutException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal;", true);
            //}

            //catch (SystemException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal;", true);
            //}

            //catch (Exception ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal;", true);
            //}
        }

        protected void btnAdHoc_Click(object sender, EventArgs e)
        {
            txtCommentAdHoc.Text = string.Empty;
            this.Form.DefaultButton = this.btnAdHocSav.UniqueID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openAdHoc();", true);
        }

        protected void btnAdHocSav_Click(object sender, EventArgs e)
        {
            try
            {

                if (dllUserAdHoc.SelectedIndex == 0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllUsersError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }


                else if (txtCommentAdHoc.Text == string.Empty || string.IsNullOrWhiteSpace(txtCommentAdHoc.Text))
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    db.DCRequest_AdHoc(int.Parse(Request.Params["DCrequestID"]), dllUserAdHoc.SelectedValue, txtCommentAdHoc.Text, CachedLogin);
                    Insert_DCRequest_Files(int.Parse(Request["DCrequestID"]));
                    //lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    //Response.Redirect("Default_View_Approve.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideAdHoc();", true);
                    ModalPopupExtender1.Show();
                }
            }
            catch (FormatException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (NullReferenceException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ViewReports.aspx?IDRECTRequest=" + Request.Params["RectRequestID"]);
        }

        protected void btnApproval_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.Params["DCrequestID"]);
            //var rect = db.Get_RectRequest_ByID(id, CachedLogin);
            lblaction.Text = "1";
            ////attachement.Visible = true;
            //lblFile.Visible = true;
            //FileUpload2.Visible = true;
            //if (rect.StatusID == 1)
            //{
            //    txtCommentSec.Text = string.Empty;
            //    this.Form.DefaultButton = this.btnSaveHrSystem.UniqueID;
            //    lblTitre.Text = "- " + btnApproval.Text;

            //    LstBudgetHr_SelectedIndexChanged(sender, e);
            //    //Page.SetFocus(txtComment.ClientID);




            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_hrsystem();", true);
            //}
            //else if (rect.StatusID > 1)
            //{
            txtComment.Text = string.Empty;

            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnApproval.Text;

            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalComment();", true);
            //}


        }


        public void disableControl(bool statut)
        {
            dllDepartment.Enabled = statut;
            LstRequestor.Enabled = statut;
            LstCategory.Enabled = statut;
            LstPriority.Enabled = statut;
            txtPlannedStartDate.Enabled = statut;
            LstCostCenter.Enabled = statut;
            txtBeneficiary.Enabled = statut;
            txtBeneficiaryJobTitle.Enabled = statut;
            txtJustificationWhy.Enabled = statut;
            txtUrgencyComment.Enabled = statut;
            txtArea.Enabled = statut;
            txtRisk.Enabled = statut;
            LstPriority.Enabled = statut;
            txtDocumentNumber.Enabled = statut;
            txtDocumentVersionCode.Enabled = statut;
            LstDocumentType.Enabled = statut;
            txtDocumentTitle.Enabled = statut;
            LstDocumentLangueID.Enabled = statut;
            txtDocVersion.Enabled = statut;
            txtDocumentVersionDate.Enabled = statut;
            txtDocReviewVersionDate.Enabled = statut;
            txtDocTranslator.Enabled = statut;
            ddlDocHarCopy.Enabled = statut;
            txtDocLink.Enabled = statut;
            txtDocTransmittalNo.Enabled = statut;
            txtAdditionalNotedId.Enabled = statut;
            //ddlService.Enabled = statut;
            txtRequestDescription.Enabled = statut;
            ddlRequestType.Enabled = statut;

            if (db.Get_KCCRequestIsAdmin(CachedLogin).Where(x => x.ModuleID == "DCRequest").FirstOrDefault().Number == 0)
            {
                //ddlService.SelectedValue = "1";
                ddlService.Enabled = false;
            }
            else
            {
                ddlService.Enabled = statut;
            }
        }


        public void Insert_DCRequest_Files(int ID)
        {
            try
            {
                HttpFileCollection files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile uploadedFile = files[i];
                    string fileName = Path.GetFileName(uploadedFile.FileName);
                    string contentType = uploadedFile.ContentType;
                    if (uploadedFile.ContentLength > 0)
                    {
                        byte[] oneFile = new byte[uploadedFile.InputStream.Length + 1];
                        uploadedFile.InputStream.Read(oneFile, 0, oneFile.Length);
                        db.DCRequest_Insert_File(ID, fileName, contentType, oneFile);
                    }
                }
            }
            catch (FormatException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (NullReferenceException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }


        protected void DownloadAttachment(string mimeType, string fileName, byte[] file)
        {
            try
            {
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));
                Response.BinaryWrite(file);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


        protected void btnview_Click(object sender, EventArgs e)
        {

            try
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideProgress();", true);

                int id = int.Parse(Request.Params["DCrequestID"]);

                Button btn = (Button)sender;


                foreach (GridViewRow row in dgvFiles.Rows)
                {
                    Button btn1 = (Button)row.FindControl("btnview");

                    if (btn1.Equals(btn))
                    {

                        Label fileID = (Label)row.FindControl("txtFileID");
                        Label txtType = (Label)row.FindControl("txtType");
                        //Label txtFile = (Label)row.FindControl("File");
                        Label filename = (Label)row.FindControl("txtFileName");

                        // MAJOR PERFORMANCE OPTIMIZATION: Download only the specific file when requested
                        // This eliminates loading all file binaries on every page load
                        int fileIdValue = int.Parse(fileID.Text);
                        var fileData = db.Get_DCRequest_File_Download(fileIdValue);
                        
                        if (fileData != null && fileData.File != null)
                        {
                            byte[] bytes = (byte[])fileData.File;
                            DownloadAttachment(txtType.Text, filename.Text, bytes);
                        }
                        else
                        {
                            // Fallback to original method if optimized version fails
                            byte[] bytes = (byte[])db.Get_DCRequest_File(id).Where(x => x.FileID.ToString() == fileID.Text).FirstOrDefault().File.ToArray();
                            DownloadAttachment(txtType.Text, filename.Text, bytes);
                        }

                        break;
                    }
                }
            }
            catch (FormatException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (NullReferenceException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnview_Click1(object sender, EventArgs e)
        {

        }

        protected void btnHistory_Click(object sender, EventArgs e)
        {





            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openHistory();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openReallocate();", true);
        }

        protected void btnSaveHistory_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlRoleHistory.SelectedIndex == 0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllUsersError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }

                else if (ddlStatusHistory.SelectedIndex == 0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllStatusError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txtSignDate.Text == string.Empty || string.IsNullOrWhiteSpace(txtSignDate.Text))
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSignDateEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    db.DCRequest_InsertHistory(int.Parse(Request["DCrequestID"]), "Add History", txtCommentHistory.Text, ddlRoleHistory.SelectedValue, int.Parse(ddlStatusHistory.SelectedValue), CachedLogin, DateTime.Parse(txtSignDate.Text));

                    //db.DCRequest_AdHoc(int.Parse(Request.Params["DCrequestID"]), dllUserAdHoc.SelectedValue, txtCommentAdHoc.Text, CachedLogin);
                    //Insert_DCRequest_Files(int.Parse(Request["DCrequestID"]));
                    //lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    //Response.Redirect("Default_View_Approve.aspx");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideAdHoc();", true);
                    //ModalPopupExtender1.Show();
                    Response.Redirect("Detail_DCRequest.aspx?DCrequestID=" + int.Parse(Request["DCrequestID"]));
                }


                Insert_DCRequest_Files(int.Parse(Request["DCrequestID"]));








            }
            catch (FormatException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (NullReferenceException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlRequestType.SelectedValue == "2") txtDocumentNumber.ReadOnly = false;
            //else txtDocumentNumber.ReadOnly = true;

        }

        // PERFORMANCE OPTIMIZED: Consolidated data loading method
        private void LoadDCRequestData(int id)
        {
            // PERFORMANCE FIX: Load all data in parallel to reduce wait time
            var mainRequestTask = new System.Threading.Tasks.Task<dynamic>(() => 
                db.Get_DCRequest_ById(id, CachedLogin).FirstOrDefault());
            // MAJOR PERFORMANCE OPTIMIZATION: Load only file metadata (no binaries) for instant page load
            var filesTask = new System.Threading.Tasks.Task<dynamic>(() => 
                db.Get_DCRequest_File_Metadata(id));
            var historyTask = new System.Threading.Tasks.Task<dynamic>(() => 
                db.Get_DCRequest_History(id).ToList());
            
            // Start all tasks
            mainRequestTask.Start();
            filesTask.Start();
            historyTask.Start();
            
            // Wait for main request data first
            mainRequestTask.Wait();
            var request = mainRequestTask.Result;
            if (request == null) return;

            // Populate form fields while other tasks complete
            PopulateFormFields(request);
            
            // Wait for remaining tasks and bind data
            filesTask.Wait();
            historyTask.Wait();
            
            dgvFiles.DataSource = filesTask.Result;
            dgvFiles.DataBind();
            dgvHistory.DataSource = historyTask.Result;
            dgvHistory.DataBind();

            // Handle applicability with optimized queries
            LoadApplicabilityDataOptimized(id, request.Applicability);
            
            // Configure UI permissions with request data
            ConfigureUIPermissions(id, request);
        }

        private void PopulateFormFields(dynamic request)
        {
            // PERFORMANCE FIX: Batch all form field population
            PopulateEmployeeAndDepartment(request.RequestorLogin);
            title.InnerText = request.Id.ToString();
            dllDepartment.SelectedValue = request.Department ?? "";
            LstRequestor.SelectedValue = request.RequestorLogin ?? "";
            txtBeneficiary.Text = request.Beneficiary ?? "";
            txtBeneficiaryJobTitle.Text = request.BeneficiaryJobTitle ?? "";
            txtRequestDescription.Text = request.Description ?? "";
            ddlService.SelectedValue = request.ServiceID?.ToString() ?? "";
            ddlRequestType.SelectedValue = request.TypeId?.ToString() ?? "";
            if (request.LangId != null) LstDocumentLangueID.Text = request.LangId.ToString();
            txtDocumentTitle.Text = request.DocTitle ?? "";
            LstDocumentType.SelectedValue = request.DocTypeId?.ToString() ?? "";
            txtDocumentVersionCode.Text = request.DocVersionCode ?? "";
            txtDocumentNumber.Text = request.DocNumber ?? "";
            LstCategory.SelectedValue = request.CategoryID?.ToString() ?? "";
            LstPriority.SelectedValue = request.PrioriteID?.ToString() ?? "";
            txtUrgencyComment.Text = request.CommentPriority ?? "";
            LstCostCenter.SelectedValue = request.CostCenter ?? "";
            
            // Safe date parsing
            if (request.PlannedDeliveryDate != null)
                txtPlannedStartDate.Text = DateTime.Parse(request.PlannedDeliveryDate.ToString()).ToString("MM-dd-yyyy");
            
            txtArea.Text = request.Area ?? "";
            txtRisk.Text = request.Risk ?? "";
            if (request.DocVersion != null) txtDocVersion.Text = request.DocVersion.ToString();
            if (request.DocVersionDate != null) 
                txtDocumentVersionDate.Text = DateTime.Parse(request.DocVersionDate.ToString()).ToString("MM-dd-yyyy");
            txtDocTranslator.Text = request.DocTranslator ?? "";
            if (request.DocReviewDate != null) 
                txtDocReviewVersionDate.Text = DateTime.Parse(request.DocReviewDate.ToString()).ToString("MM-dd-yyyy");
            
            ddlDocHarCopy.SelectedValue = (request.DocHardcopy == true) ? "1" : "0";
            txtDocLink.Text = request.DocLink ?? "";
            if (request.DocTransmittalNo != null) txtDocTransmittalNo.Text = request.DocTransmittalNo.ToString();
            if (request.AdditionalNoteId != null) txtAdditionalNotedId.Text = request.AdditionalNoteId.ToString();
            txtStatut.Text = request.StatusDescription ?? "";
            txtCurrentUsers.Text = request.CurrentUser ?? "";
        }

        private void LoadApplicabilityDataOptimized(int id, bool? applicability)
        {
            TrApplicability3.Visible = true;
            
            if (applicability == true)
            {
                // PERFORMANCE FIX: Load applicability data in parallel
                var rolloutTask = new System.Threading.Tasks.Task<List<int>>(() => 
                    db.Get_DCRequest_RolloutProcesses_ByRequestId(id).Select(x => x.RolloutID).ToList());
                var areasTask = new System.Threading.Tasks.Task<List<int>>(() => 
                    db.Get_DCRequest_ApplicableAreas_BYRequestId(id).Select(x => x.AreaID).ToList());
                var reviewersTask = new System.Threading.Tasks.Task<List<int>>(() => 
                    db.Get_DCRequest_ApplicabilityReviewGroup_BYRequestId(id).Select(x => x.ReviewerGroupID).ToList());
                
                rolloutTask.Start();
                areasTask.Start();
                reviewersTask.Start();
                
                BindApplicabilityListBoxItems();
                ddlApplicability.SelectedValue = "YES";
                
                System.Threading.Tasks.Task.WaitAll(rolloutTask, areasTask, reviewersTask);
                
                SetSelectedValues(CheckBoxListApplicableAreas, areasTask.Result);
                SetSelectedValues(CheckBoxListApprovalReviewers, reviewersTask.Result);
                SetSelectedValues(CheckBoxListRolloutChannels, rolloutTask.Result);
                
                TrApplicability1.Visible = true;
                TrApplicability2.Visible = true;
            }
            else
            {
                BindApplicabilityListBoxItems();
                ddlApplicability.SelectedValue = "NO";
                
                var rolloutChannels = db.Get_DCRequest_RolloutProcesses_ByRequestId(id).Select(x => x.RolloutID).ToList();
                SetSelectedValues(CheckBoxListRolloutChannels, rolloutChannels);
            }
        }

        private void ConfigureUIPermissions(int id, dynamic requestData = null)
        {
            // EMERGENCY FIX: Use cached permissions to prevent timeout issues
            bool isDCRequestAdmin = IsModuleAdmin("DCRequest");

            // EMERGENCY FIX: Use cached action lookup to solve SP_Get_KCCRequest_Action timeout
            var ewrActions = GetCachedUserActions(id, "DCRequest");
            var requestToApprove = db.Get_KCCRequest_ToApprove("DCRequest", CachedLogin).Where(x => (x.Id == id)).FirstOrDefault();

            if (requestToApprove != null)
            {
                var statut = requestToApprove.StatusID;
                
                // PERFORMANCE FIX: Safe action extraction with null checks
                var canEdit = GetActionValue(ewrActions, "CanEdit");
                var canCallBack = GetActionValue(ewrActions, "CanCallBack");
                
                // CRITICAL FIX: Approval rights come from requestToApprove (not from action stored procedure)
                // The stored procedure only returns CanEdit and CanCallBack actions
                bool hasApprovalRights = (requestToApprove != null && !string.IsNullOrEmpty(requestToApprove.Module));

                if (canEdit >= 1)
                {
                    disableControl(true);
                    btnUpdate.Visible = true;
                    btnApproval.Visible = false;
                    BtnReject.Visible = true; // Can reject when can edit
                    btnBackToOriginator.Visible = false;
                    BtnReturnToOrginator.Visible = false;
                    btnAdHoc.Visible = false;
                }
                else if (hasApprovalRights)
                {
                    // User can approve this request (logic from Get_KCCRequest_ToApprove)
                    disableControl(false);
                    btnUpdate.Visible = false;
                    
                    if (statut < 4)
                    {
                        btnApproval.Visible = true;
                        BtnReject.Visible = true;
                        BtnReturnToOrginator.Visible = true;
                        btnBackToOriginator.Visible = true;
                        btnAdHoc.Visible = true;
                    }
                    else if (statut == 4)
                    {
                        var createdBy = requestData?.CreatedBy ?? requestToApprove?.CreatedBy;
                        var userLogin = CachedLogin;
                        
                        if (createdBy != userLogin)
                        {
                            btnApproval.Visible = true;
                            BtnReject.Visible = false;
                            BtnReturnToOrginator.Visible = true;
                            btnBackToOriginator.Visible = false;
                            btnAdHoc.Visible = true;
                        }
                        else // revision during hsec review
                        {
                            btnUpdate.Visible = true;
                            BtnReturnToOrginator.Visible = false;
                            btnAdHoc.Visible = true;
                        }
                    }
                }
                else
                {
                    // No edit or approval rights
                    disableControl(false);
                    btnUpdate.Visible = false;
                    btnApproval.Visible = false;
                    BtnReject.Visible = false;
                    BtnReturnToOrginator.Visible = false;
                    btnBackToOriginator.Visible = false;
                    btnAdHoc.Visible = false;
                }

                if (statut == 15)
                {
                    btnAdHoc.Visible = false;
                    btnApproval.Visible = true;
                    btnUpdate.Visible = true;
                    BtnReject.Visible = false;
                    BtnReturnToOrginator.Visible = false;
                    btnBackToOriginator.Visible = false;
                    btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "Adhocstatus");
                }

                if (statut == 13)
                {
                    btnAdHoc.Visible = false;
                    btnApproval.Visible = false;
                    btnBackToOriginator.Visible = false;
                    BtnReject.Visible = false;
                    BtnReturnToOrginator.Visible = false;
                    btnUpdate.Visible = false;
                    btnHistory.Visible = false;
                }
            }

            // PERFORMANCE FIX: Admin controls using cached permissions
            if (!isDCRequestAdmin)
            {
                btnHistory.Visible = false;
            }
            else
            {
                btnUpdate.Visible = true;
                BtnReject.Visible = true;
                btnHistory.Visible = true;
                disableControl(true);
            }
        }

        // EMERGENCY FIX: Helper method to safely extract action values from dynamic list
        private int GetActionValue(List<dynamic> actions, string actionName)
        {
            try
            {
                if (actions == null || !actions.Any()) return 0;
                
                foreach (var action in actions)
                {
                    // Handle both SP_Get_KCCRequest_Action_Result and dynamic types
                    string currentAction = null;
                    int? visibility = null;
                    
                    if (action != null)
                    {
                        var actionType = action.GetType();
                        
                        // Try to get Action property
                        var actionProp = actionType.GetProperty("Action");
                        if (actionProp != null)
                        {
                            currentAction = actionProp.GetValue(action)?.ToString();
                        }
                        
                        // Try to get Visibility property
                        var visibilityProp = actionType.GetProperty("Visibility");
                        if (visibilityProp != null)
                        {
                            visibility = visibilityProp.GetValue(action) as int?;
                        }
                        
                        if (currentAction == actionName)
                        {
                            return visibility ?? 0;
                        }
                    }
                }
                return 0;
            }
            catch
            {
                return 0; // Safe fallback
            }
        }
    }
}