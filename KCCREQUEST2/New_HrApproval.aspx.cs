using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using KCCDBAccess;
using System.Globalization;

namespace KCCREQUEST2
{
    public partial class New_HrApproval : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    string userLogin = utility.Login(loggedUser);

                  

                    CbDescriptionTitle_CheckedChanged(sender, e);
                    CbIsReplacement_CheckedChanged(sender, e);

                    PopulateEmployeeAndDepartment();
                    populateReportTo();
                    PopulateCategory();
                    PopulatePriority();
                    populateEmployeeType();
                    populateContract();
                    populateCostCenter();
                    populateJobGrade();
                    populateRecruitmentType();
                    populateDuration();
                    //PopulateCampLocation();
                    //PopulateEmploymentEntity();

                    LstEmployeeType_SelectedIndexChanged(sender, e);
                }
            }
           

           
            catch (Exception ex)
            {

                Response.Redirect("pageError.aspx");
            }
        

        }
        public void populateReportTo() {

            var listEmployees = Developper.Employee.Get_Employee().Where(x=>x.DepartmentCode==LstDepartment.SelectedValue).ToList();
            LstReportTo.DataSource = listEmployees;
            LstReportTo.DataTextField = "FirstName";
            LstReportTo.DataValueField = "EmployeeId";
            LstReportTo.DataBind();
            LstReportTo.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectLstReportTo"));
        }
        public void PopulateEmployeeAndDepartment()
        {
            try
            {
                string userLogin = utility.Login(loggedUser);
                if (int.Parse(Developper.Employee.Get_Employee_Type(userLogin)) == 1)
                {
                    var listDepartement = Developper.Employee.Get_Employee_Of_Department(userLogin, "").ToList();
                    LstRequestor.DataSource = listDepartement;
                    LstRequestor.DataTextField = "FirstName";
                    LstRequestor.DataValueField = "Login";
                    LstRequestor.DataBind();
                    LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));

                    

                    //Loading Departments
                    LstDepartment.DataSource = listDepartement.GroupBy(x => x.DepartmentCode).Select(x => x.First());
                    LstDepartment.DataTextField = "DepartmentCode";
                    LstDepartment.DataValueField = "DepartmentCode";
                    LstDepartment.DataBind();
                    LstDepartment.Enabled = false;
                }
                else
                {
                    LstDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    LstDepartment.DataTextField = "LongDescription";
                    LstDepartment.DataValueField = "Code";
                    LstDepartment.DataBind();
                    LstDepartment.Enabled = true;

                }

                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "RECTRequest").FirstOrDefault().Number == 1)
                {

                    LstDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    LstDepartment.DataTextField = "LongDescription";
                    LstDepartment.DataValueField = "Code";
                    LstDepartment.DataBind();
                    LstDepartment.Enabled = true;

                    LstDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(userLogin, "").FirstOrDefault().DepartmentCode;

                    LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
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
                LstCategory.DataSource = db.Get_KCCRequest_Category(utility.Login(loggedUser));
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

        public void PopulateEmploymentEntity()
        {
            try
            {
                LstEmploymentEntity.DataSource = db.Get_RECTRequest_EmploymentEntity();
                LstEmploymentEntity.DataValueField = "EmploymentEntityID";
                LstEmploymentEntity.DataTextField = "EmploymentEntityDescription";
                LstEmploymentEntity.DataBind();
                LstEmploymentEntity.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllEmploymentEntity"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void PopulateCampLocation()
        {
            try
            {
                LstCampLocation.DataSource = db.Get_RECTRequest_CampLocation(utility.Login(loggedUser));
                LstCampLocation.DataValueField = "CampLocationID";
                LstCampLocation.DataTextField = "CampLocationDescription";
                LstCampLocation.DataBind();
                LstCampLocation.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllCampLocation"));
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
                LstPriority.DataSource = db.Get_KCCRequest_Priority(utility.Login(loggedUser));
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

        public void populateEmployeeType() {
            try
            {
                LstEmployeeType.DataSource = db.Get_RECTRequest_EmployeeType(utility.Login(loggedUser));
                LstEmployeeType.DataValueField = "EmployeeTypeID";
                LstEmployeeType.DataTextField = "EmployeeType";
                LstEmployeeType.DataBind();
                LstEmployeeType.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllemployeeType"));

            }
            catch (Exception ex) {

            }

        }

        public void populateRecruitmentType() {
            LstRequestMethode.DataSource = db.Get_RECTRequest_Type(utility.Login(loggedUser));
            LstRequestMethode.DataTextField = "TypeDescription";
            LstRequestMethode.DataValueField = "TypeID";
            LstRequestMethode.DataBind();
            LstRequestMethode.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestMethode"));
        }
        public void populateContract() {
            try
            {
                LstTypeOfContrat.DataSource = db.Get_RECTRequest_Contract(utility.Login(loggedUser));
                LstTypeOfContrat.DataValueField = "ContractID";
                LstTypeOfContrat.DataTextField = "ContractDescription";
                LstTypeOfContrat.DataBind();
                LstTypeOfContrat.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTypeOfContract"));

            }
            catch (Exception ex) {

            }
        }

        public void populateCostCenter() {
            try
            {
                LstCostCenter.DataSource = db.Get_RECTRequest_CostCenter().Where(x=>x.DepartementID==LstDepartment.SelectedValue);
                LstCostCenter.DataValueField = "CostCenterID";
                LstCostCenter.DataTextField = "CostCenterID";
                LstCostCenter.DataBind();
                LstCostCenter.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllcostCenter"));

            }
            catch (Exception s) {

            }
        }

        public void populateJobGrade() {
            try
            {
                LstJobGrade.DataSource = db.Get_RECTRequest_JobGrade(int.Parse(LstEmployeeType.SelectedValue), utility.Login(loggedUser));
                LstJobGrade.DataValueField = "JobGradeID";
                LstJobGrade.DataTextField = "JobGradeDescription";
                LstJobGrade.DataBind();
                LstJobGrade.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllJobGrade"));
            }
            catch (Exception s) {

            }

        }
        public void populateDuration()
        {
            try
            {
                LstDuration.DataSource = db.Get_RECTRequest_Duration(utility.Login(loggedUser));
                LstDuration.DataValueField = "Month";
                LstDuration.DataTextField = "Month";
                LstDuration.DataBind();
                LstDuration.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllduration"));
            }
            catch (Exception s)
            {

            }

        }
        protected void CbIsReplacement_CheckedChanged(object sender, EventArgs e)
        {
            txtIsReplacement.Enabled = CbIsReplacement.Checked;
            txtReplacementEmployeeName.Enabled = CbIsReplacement.Checked;
            txtReplacementJobTitle.Enabled = CbIsReplacement.Checked;
            txtReplacementPostNumber.Enabled = CbIsReplacement.Checked;
            txtReplacementReason.Enabled = CbIsReplacement.Checked;

            txtIsReplacement.Text = string.Empty;
            txtReplacementEmployeeName.Text = string.Empty;
            txtReplacementJobTitle.Text = string.Empty;
            txtReplacementPostNumber.Text = string.Empty;
            txtReplacementReason.Text = string.Empty;

         
        }

        protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));


                populateReportTo();

                LstCostCenter.DataSource = db.Get_RECTRequest_CostCenter().Where(x => x.DepartementID == LstDepartment.SelectedValue);
                LstCostCenter.DataValueField = "CostCenterID";
                LstCostCenter.DataTextField = "CostCenterID";
                LstCostCenter.DataBind();
                LstCostCenter.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllcostCenter"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Insert_RectRequest_Files(decimal ID)
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
                        db.Insert_RectRequest_File(Convert.ToInt32(ID), fileName, contentType, oneFile);

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

        public int CountFiles()
        {
            
                HttpFileCollection files = Request.Files;
            int numberfiles = 0;
                for (int i = 0; i < files.Count; i++)
                {
                HttpPostedFile uploadedFile = files[i];
                string fileName = Path.GetFileName(uploadedFile.FileName);
                string contentType = uploadedFile.ContentType;
                if (uploadedFile.ContentLength > 0)
                {
                    numberfiles++;

                }
              
                }

            return numberfiles;
        }

        public bool ValidateInputs()
        {
            try
            {
                bool valid = true;
                string error = string.Empty;
                if (LstRequestor.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError");
                    valid = false;

                }
                if (LstRequestMethode.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstRequestMethoderEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "LstRequestMethodeEmptyError");
                    valid = false;

                }
                if (LstCategory.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError");
                    valid = false;
                }
                if (LstPriority.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError");
                    valid = false;
                }

                if (LstEmployeeType.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstEmployeeTypeEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstEmployeeTypeEmptyError");
                    valid = false;
                }
                //if (LstEmployeeType.SelectedValue == "2") {

                //    if (LstEmploymentEntity.SelectedIndex == 0) {

                //        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstEmploymentEntityEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstEmploymentEntityEmptyError");
                //        valid = false;
                //    }

                //    if (LstCampLocation.SelectedIndex == 0) {

                //        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstCampLocationEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstCampLocationEmptyError");
                //        valid = false;
                //    }

                //}

                if (LstTypeOfContrat.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstTypeOfContratEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstTypeOfContratEmptyError");
                    valid = false;
                }

                if (LstCostCenter.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstCostCenterEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstCostCenterEmptyError");
                    valid = false;
                }

               if (LstCategory.SelectedIndex ==0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError");
                    valid = false;
                }


                if (LstDuration.SelectedIndex ==0 && LstDuration.Enabled==true)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDurationEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDurationEmptyError");
                    valid = false;
                }

                if (txtPlannedStartDate.Text == string.Empty || string.IsNullOrWhiteSpace(txtPlannedStartDate.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError");
                    valid = false;
                }

                if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text)) && ((DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) <= DateTime.Today.AddDays(-1)))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess");
                    valid = false;
                }

                if (txtJobTitle.Text == string.Empty || string.IsNullOrWhiteSpace(txtJobTitle.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtJobTitleEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtJobTitleEmptyError");
                    valid = false;
                }

                if (txtNumberOfPosition.Text == string.Empty || string.IsNullOrWhiteSpace(txtNumberOfPosition.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtNumberOfPositionEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtNumberOfPositionEmptyError");
                    valid = false;
                }

                if (CbIsReplacement.Checked == true)
                {

                    if (txtIsReplacement.Text == string.Empty || string.IsNullOrWhiteSpace(txtIsReplacement.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtIsReplacementEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtIsReplacementEmptyError");
                        valid = false;
                    }

                    if (txtReplacementReason.Text == string.Empty || string.IsNullOrWhiteSpace(txtReplacementReason.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtReplacementReasonEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtReplacementReasonEmptyError");
                        valid = false;
                    }

                    if (txtReplacementJobTitle.Text == string.Empty || string.IsNullOrWhiteSpace(txtReplacementJobTitle.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtReplacementJobTitleEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtReplacementJobTitleEmptyError");
                        valid = false;
                    }
                }

                if (LstJobGrade.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstJobGradeEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstJobGradeEmptyError");
                    valid = false;
                }

                if (LstReportTo.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstReportToEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstReportToEmptyError");
                    valid = false;
                }

                if (txtPrincipalTask.Text == string.Empty || string.IsNullOrWhiteSpace(txtPrincipalTask.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPrincipalTaskEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPrincipalTaskEmptyError");
                    valid = false;
                }

                if (txtMinimumQualification.Text == string.Empty || string.IsNullOrWhiteSpace(txtMinimumQualification.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtMinimumQualificationEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtMinimumQualificationEmptyError");
                    valid = false;
                }

                if (txtOhterRequirements.Text == string.Empty || string.IsNullOrWhiteSpace(txtOhterRequirements.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtOhterRequirementsEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtOhterRequirementsEmptyError");
                    valid = false;
                }
            
              
                if (CountFiles()==0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "FileEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "FileEmptyError");
                    valid = false;
                }

                if (valid == false)
                {
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }

                
                return valid;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidateInputs())
                {
                    //int? employementEntityID, campLocation;
                    //if (LstEmployeeType.SelectedValue == "1")
                    //{
                    //    employementEntityID = null;
                    //    campLocation = null;
                    //}
                    //else
                    //{
                    //    employementEntityID = int.Parse(LstEmploymentEntity.SelectedValue);
                    //    campLocation = int.Parse(LstCampLocation.SelectedValue);
                    //}
                    decimal rectID = db.Insert_RectRequest(LstRequestor.SelectedValue, txtPrincipalTask.Text, txtJobTitle.Text, txtMinimumQualification.Text, txtOhterRequirements.Text,
                        int.Parse(txtNumberOfPosition.Text), LstDuration.SelectedValue, DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture), false, false, false, CbIsReplacement.Checked, txtIsReplacement.Text, txtReplacementEmployeeName.Text, txtReplacementReason.Text, txtReplacementPostNumber.Text, txtReplacementJobTitle.Text, int.Parse(LstTypeOfContrat.SelectedValue), int.Parse(LstJobGrade.SelectedValue), LstEmployeeType.SelectedValue, int.Parse(LstRequestMethode.SelectedValue), LstCostCenter.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture), utility.Login(loggedUser), LstReportTo.SelectedValue, int.Parse(LstEmployeeType.SelectedValue), false,null,null);

                    Insert_RectRequest_Files(rectID);

                    lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    lblIDRequest.Text = rectID.ToString();


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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_HrApproval.aspx");
        }

        protected void CbDescriptionTitle_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        protected void LstTypeOfContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstTypeOfContrat.SelectedValue == "1")
            {
                LstDuration.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "lblTypeContratUndertermined"));
                LstDuration.SelectedIndex = 0;
                LstDuration.Enabled = false;
                //txtDuration.Text = "Undertermined";
                //txtDuration.Enabled = false;
            }
            else {
                populateDuration();
                LstDuration.Enabled = true;
                //txtDuration.Text = string.Empty;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string userLogin = utility.Login(loggedUser);
            var infoRemplacerNumero = Developper.Employee.Get_Employee().Where(x => x.EmployeeId == txtIsReplacement.Text).ToList();
            foreach (var l in infoRemplacerNumero) {
                txtReplacementEmployeeName.Text = l.FirstName;
                txtReplacementJobTitle.Text = l.EmergencyContactName;
                txtReplacementPostNumber.Text = l.PositionCode;
            }


        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail_RecruitmentRequest.aspx?RectRequestID=" + lblIDRequest.Text);

        }

        protected void LstEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateJobGrade();

            if (LstEmployeeType.SelectedValue == "2")
            {
                LstEmploymentEntity.Enabled = true;
                LstCampLocation.Enabled = true;
            }
            else {
                LstEmploymentEntity.Enabled = false;
                LstCampLocation.Enabled =false;
            }
        }
    }
}