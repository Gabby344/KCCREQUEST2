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
    public partial class Detail_RecruitmentRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    PopulateCategory();
                    PopulatePriority();
                    populateEmployeeType();
                    populateContract();
                    populateCostCenter();
                    populateJobGrade();
                    populateRecruitmentType();
                    populateUsers();
                    populateUsersAdHoc();
                    populateRole();
                    populateBuged();
                    //PopulateCampLocation();
                    //PopulateEmploymentEntity();
                    populateDuration();
                    btnApproval.Visible = false;
                    BtnReject.Visible = false;
                    BtnReturnToOrginator.Visible = false;
                    btnUpdate.Visible = false;
                    btnBackToOriginator.Visible = false;
                    disableControl(false);

                    if (Request.Params["RectRequestID"] != null)
                    {
                        int id = int.Parse(Request.Params["RectRequestID"]);
                        var rect = db.Get_RectRequest_ByID(id, utility.Login(loggedUser));
                        title.InnerText = rect.Id.ToString();
                        PopulateEmployeeAndDepartment(rect.RequestorLogin);
                        populateReportTo();
                        LstDepartment.SelectedValue = rect.Department;
                        LstReportTo.SelectedValue = rect.ReportTo;
                        populateCostCenter();
                        LstRequestor.SelectedValue = rect.RequestorLogin;
                        LstReportTo.SelectedValue = rect.ReportTo;
                        LstRequestMethode.SelectedValue = rect.TypeID.ToString();
                        LstCategory.SelectedValue = rect.CategoryID.ToString();
                        LstPriority.SelectedValue = rect.PrioriteID.ToString();
                        LstEmployeeType.SelectedValue = rect.EmployeeTypeID.ToString();
                        populateJobGrade();
                        LstTypeOfContrat.SelectedValue = rect.ContractID.ToString();
                        txtIsOPS.Text = rect.OpsDescription;

                        if (rect.ContractID == 1)
                        {
                            LstDuration.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "lblTypeContratUndertermined"));
                            LstDuration.SelectedIndex = 0;
                            LstDuration.Enabled = false;
                        }
                        else
                        {
                            LstDuration.SelectedValue = rect.Duration;
                        }

                        //if (rect.EmployeeTypeID == 2)
                        //{

                        //    LstEmploymentEntity.SelectedValue = rect.EmploymentEntityID.ToString();
                        //    LstCampLocation.SelectedValue = rect.CampLocationID.ToString();
                        //}
                        LstCostCenter.SelectedValue = rect.CostCenterID;
                        txtPlannedStartDate.Text = DateTime.Parse(rect.PlannedDeliveryDate.ToString()).ToString(format: "MM-dd-yyyy");
                        txtJobTitle.Text = rect.Function;
                        txtNumberOfPosition.Text = rect.NumberOfCandidat.ToString();
                        CbIsReplacement.Checked = (bool)rect.ISreplacement;
                        CbIsReplacement_CheckedChanged(sender, e);
                        txtIsReplacement.Text = rect.ReplacementEmployeeId;
                        txtReplacementEmployeeName.Text = rect.ReplacementName;
                        txtReplacementPostNumber.Text = rect.ReplacementPostNumber;
                        txtReplacementReason.Text = rect.ReplaceementMotif;
                        txtReplacementJobTitle.Text = rect.ReplacementJobTitle;
                        LstJobGrade.SelectedValue = rect.JobGradeID.ToString();
                        txtPrincipalTask.Text = rect.PricipalTask;
                        txtMinimumQualification.Text = rect.NominalQualification;
                        txtOhterRequirements.Text = rect.Otherexigences;
                        txtCurrentUser.Text = rect.CurrentUser;
                        txtStatut.Text = rect.StatusDescription;
                        //cbDetailBudged.Checked = rect.BudgedID;
                        if (rect.BudgedID == true)
                        {
                            LstBudged.SelectedIndex = 0;
                        }
                        else {
                            LstBudged.SelectedIndex = 1;
                        }
                        txtDetailBudgedYear.Text = rect.BudgtedYear.ToString();

                        dgvFiles.DataSource = db.Get_RectRequest_File(id);
                        dgvFiles.DataBind();

                        dgvHistory.DataSource = db.Get_RectRequest_History(id);
                        dgvHistory.DataBind();



                        var RectActions = db.Get_KCCRequest_Action(id, utility.Login(loggedUser), "RECTRequest");
                        int statut = rect.StatusID.Value;
                        var requestToApprove = db.Get_KCCRequest_ToApprove("RECTRequest", utility.Login(loggedUser)).Where(x => (x.Id == id)).FirstOrDefault();

                        if ((rect.DocPos == "HR_SYSTEM_NAT" || rect.DocPos == "HR_SYSTEM_EXP") && requestToApprove != null)
                        {
                            btnUpdate.Visible = true;

                            disableControl(true);
                            BtnReturnToOrginator.Visible = true;
                            btnBackToOriginator.Visible = true;
                            BtnReject.Visible = false;
                            btnApproval.Visible = true;
                            btnCallBack.Visible = false;

                            LstEmployeeType_SelectedIndexChanged(sender, e);
                        }


                        if (RectActions.Where(x => x.Action == "CanEdit").FirstOrDefault().Visibility >= 1)
                        {
                            btnUpdate.Visible = true;
                            btnApproval.Visible = false;
                            BtnReject.Visible = true;
                            btnBackToOriginator.Visible = true;
                            disableControl(true);

                            //CbIsReplacement_CheckedChanged(sender, e);
                            //CbDescriptionTitle_CheckedChanged(sender, e);
                            if (statut == 1)
                            {
                                BtnReturnToOrginator.Visible = false;
                                btnBackToOriginator.Visible = false;
                                BtnReject.Visible = false;
                                btnApproval.Visible = true;
                                //btnUpdate.Visible = false;
                            }
                            btnCallBack.Visible = false;
                            LstEmployeeType_SelectedIndexChanged(sender, e);

                        }
                        if (RectActions.Where(x => x.Action == "CanCallBack").FirstOrDefault().Visibility >= 1)
                        {
                            btnCallBack.Visible = true;

                        }
                        else btnCallBack.Visible = false;


                        if ((requestToApprove != null) && (!string.IsNullOrEmpty(requestToApprove.Module)))
                        {
                            if (statut <= 3 && statut > 1)
                            {
                                //btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Text");
                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = true;

                                //lblaction.Text = "2";
                                if (statut == 1)
                                {
                                    BtnReturnToOrginator.Visible = false;
                                    btnBackToOriginator.Visible = false;
                                    BtnReject.Visible = false;
                                    btnApproval.Visible = true;
                                    //btnUpdate.Visible = false;
                                }
                                if (statut == 3)
                                {
                                    btnBackToOriginator.Visible = true;
                                }
                            }
                            if (statut == 5)
                            {
                                BtnReject.Visible = true;
                                btnApproval.Visible = true;
                            }
                            if (statut == 15)
                            {

                                btnAdHoc.Visible = false;

                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = false;
                                BtnReturnToOrginator.Visible = false;
                                btnBackToOriginator.Visible = false;
                                btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "Adhocstatus");
                            }
                            else
                            {
                           
                                 btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Text");
                    
                            }


                            if (statut == 10 && (rect.DocPos == "RECRUTMENT_NAT" || rect.DocPos == "RECRUTMENT_EXP"))
                            {
                                btnClose.Visible = true;

                                btnAdHoc.Visible = false;

                                btnApproval.Visible = false;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = false;
                                BtnReturnToOrginator.Visible = false;
                                btnBackToOriginator.Visible = false;
                                btnAdHoc.Visible = false;
                            }
                            else btnClose.Visible = false;


                        }
                        else { btnAdHoc.Visible = false; }




                        if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "RECTRequest").FirstOrDefault().Number == 0)
                        {


                            btnrRealocate.Visible = false;
                        }
                        else if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "RECTRequest").FirstOrDefault().Number == 1)
                        {
                            btnUpdate.Visible = true;

                            BtnReject.Visible = true;
                            disableControl(true);

                            //cbDetailBudged.Enabled = true;
                            LstBudged.Enabled = true;
                            //cbDetailBudged_CheckedChanged(sender, e);
                            LstBudged_SelectedIndexChanged(sender, e);

                        }

                        if (statut == 4)
                        {
                            disableControl(false);
                            BtnReturnToOrginator.Visible = false;
                            btnBackToOriginator.Visible = false;
                            BtnReject.Visible = false;
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            btnAdHoc.Visible = false;
                            btnrRealocate.Visible = false;
                        }

                        if (statut == 13)
                        {

                            disableControl(false);
                            BtnReturnToOrginator.Visible = false;
                            btnBackToOriginator.Visible = false;
                            BtnReject.Visible = false;
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            btnAdHoc.Visible = false;
                            btnrRealocate.Visible = false;

                        }



                        if (rect.EmployeeTypeID == 2)
                        {
                            btnPrintExpatriate.Visible = true;
                        }
                        else
                        {
                            btnPrintExpatriate.Visible = false;
                        }


                    }

                    if (btnCallBack.Visible == true)
                    {
                        disableControl(false);
                        BtnReturnToOrginator.Visible = false;
                        btnBackToOriginator.Visible = false;
                        BtnReject.Visible = false;
                        btnApproval.Visible = false;
                        btnUpdate.Visible = false;
                        btnAdHoc.Visible = false;
                        btnrRealocate.Visible = false;
                    }





                }

            }
            catch (Exception ex)
            {

                //string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                //lblErreur.Text = error;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

                Response.Redirect("pageError.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_hrsystem();", true);
        }
        public void disableControl(bool statut)
        {
            LstDepartment.Enabled = statut;
            LstRequestor.Enabled = statut;
            LstCategory.Enabled = statut;
            LstPriority.Enabled = statut;
            txtPlannedStartDate.Enabled = statut;
            
            if (LstTypeOfContrat.SelectedValue == "1")
            {
               LstDuration.Enabled = false;
            }
            else LstDuration.Enabled = statut;

            LstTypeOfContrat.Enabled = statut;
            LstRequestMethode.Enabled = statut;
           
            LstCostCenter.Enabled = statut;
            txtJobTitle.Enabled = statut;
            txtNumberOfPosition.Enabled = statut;

       
            CbIsReplacement.Enabled = statut;
            if (CbIsReplacement.Checked == false)
            {
                txtReplacementEmployeeName.Enabled = false;
                txtReplacementPostNumber.Enabled = false;
                txtReplacementReason.Enabled = false;
                txtReplacementJobTitle.Enabled = false;
                txtIsReplacement.Enabled = false;
            }
            else
            {
                txtReplacementEmployeeName.Enabled = statut;
                txtReplacementPostNumber.Enabled = statut;
                txtReplacementReason.Enabled = statut;
                txtReplacementJobTitle.Enabled = statut;
                txtIsReplacement.Enabled = statut;
            }
            LstJobGrade.Enabled = statut;
            txtMinimumQualification.Enabled = statut;
            txtOhterRequirements.Enabled = statut;
            LstReportTo.Enabled = statut;
            LstEmploymentEntity.Enabled = statut;
            LstCampLocation.Enabled = statut;
            txtPrincipalTask.Enabled = statut;

            FileUpload3.Enabled = statut;

            LstEmployeeType.Enabled = statut;
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

        public void populateBuged() {

            LstBudged.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "budgedYes"));
            LstBudged.Items.Insert(1, (string)GetGlobalResourceObject("LocalizedText", "budgedNon"));
        }
        public void populateReportTo()
        {

            var listEmployees = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
            LstReportTo.DataSource = listEmployees;
            LstReportTo.DataTextField = "FirstName";
            LstReportTo.DataValueField = "EmployeeId";
            LstReportTo.DataBind();
            LstReportTo.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectLstReportTo"));
        }
        public void PopulateEmployeeAndDepartment(string requestor)
        {
            try
            {
              
                if (int.Parse(Developper.Employee.Get_Employee_Type(requestor)) == 1)
                {
                    var listDepartement = Developper.Employee.Get_Employee_Of_Department(requestor, "").ToList();
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

                    LstDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(requestor, "").FirstOrDefault().DepartmentCode;

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
        public void populateRecruitmentType()
        {
            LstRequestMethode.DataSource = db.Get_RECTRequest_Type(utility.Login(loggedUser));
            LstRequestMethode.DataTextField = "TypeDescription";
            LstRequestMethode.DataValueField = "TypeID";
            LstRequestMethode.DataBind();
            LstRequestMethode.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestMethode"));
        }
        public void populateEmployeeType()
        {
            try
            {
                LstEmployeeType.DataSource = db.Get_RECTRequest_EmployeeType(utility.Login(loggedUser));
                LstEmployeeType.DataValueField = "EmployeeTypeID";
                LstEmployeeType.DataTextField = "EmployeeType";
                LstEmployeeType.DataBind();
                LstEmployeeType.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllemployeeType"));

            }
            catch (Exception ex)
            {

            }

        }

        public void populateContract()
        {
            try
            {
                LstTypeOfContrat.DataSource = db.Get_RECTRequest_Contract(utility.Login(loggedUser));
                LstTypeOfContrat.DataValueField = "ContractID";
                LstTypeOfContrat.DataTextField = "ContractDescription";
                LstTypeOfContrat.DataBind();
                LstTypeOfContrat.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTypeOfContract"));

            }
            catch (Exception ex)
            {

            }
        }

        public void populateCostCenter()
        {
            try
            {
                LstCostCenter.DataSource = db.Get_RECTRequest_CostCenter().Where(x => x.DepartementID == LstDepartment.SelectedValue);
                LstCostCenter.DataValueField = "CostCenterID";
                LstCostCenter.DataTextField = "CostCenterID";
                LstCostCenter.DataBind();
                LstCostCenter.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllcostCenter"));

            }
            catch (Exception s)
            {

            }
        }

        public void populateJobGrade()
        {
            try
            {
                LstJobGrade.DataSource = db.Get_RECTRequest_JobGrade(int.Parse(LstEmployeeType.SelectedValue), utility.Login(loggedUser));
                LstJobGrade.DataValueField = "JobGradeID";
                LstJobGrade.DataTextField = "JobGradeDescription";
                LstJobGrade.DataBind();
                LstJobGrade.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllJobGrade"));
            }
            catch (Exception s)
            {

            }

        }

        public void populateUsers()
        {

            dllUsers.DataSource = db.Get_Employee_User();
            dllUsers.DataTextField = "FirstName";
            dllUsers.DataValueField = "Login";
            dllUsers.DataBind();
            dllUsers.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlluser"));
        }
        public void populateUsersAdHoc()
        {

            dllUserAdHoc.DataSource = db.Get_Employee_User();
            dllUserAdHoc.DataTextField = "FirstName";
            dllUserAdHoc.DataValueField = "EmployeeId";
            dllUserAdHoc.DataBind();
            dllUserAdHoc.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlluser"));
        }
        public void populateRole()
        {

            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "RECTRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));

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


        protected void btnview_Click(object sender, EventArgs e)
        {

            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideProgress();", true);

                int id = int.Parse(Request.Params["RectRequestID"]);

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

                        byte[] bytes = (byte[])db.Get_RectRequest_File(id).Where(x => x.FileID.ToString() == fileID.Text).FirstOrDefault().File.ToArray();

                        DownloadAttachment(txtType.Text, filename.Text, bytes);

                        break;
                    }



                }
            }
            catch (FormatException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (NullReferenceException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
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

        protected void btnApproval_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.Params["RectRequestID"]);
            var rect = db.Get_RectRequest_ByID(id, utility.Login(loggedUser));
            lblaction.Text = "1";
            attachement.Visible = true;
            lblFile.Visible = true;
            FileUpload2.Visible = true;
            if (rect.StatusID == 1)
            {
                txtCommentSec.Text = string.Empty;
                this.Form.DefaultButton = this.btnSaveHrSystem.UniqueID;
                lblTitre.Text = "- " + btnApproval.Text;

                LstBudgetHr_SelectedIndexChanged(sender, e);
                //Page.SetFocus(txtComment.ClientID);

              
                

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_hrsystem();", true);
            }
            else if (rect.StatusID>1) {
               txtComment.Text = string.Empty;
               
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = "- " + btnApproval.Text;

                Page.SetFocus(txtComment.ClientID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
         
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
         
            if (txtComment.Text == string.Empty || string.IsNullOrWhiteSpace(txtComment.Text))
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            else
            {
                try
                {
                    if (lblaction.Text == "1")
                    {

                        db.Approve_RectRequest(int.Parse(Request["RectRequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }
                    else if (lblaction.Text == "3")
                    {
                        db.Reject_RectRequest(int.Parse(Request["RectRequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }
                    else if (lblaction.Text == "4")
                    {
                        db.Return_RectRequest(int.Parse(Request["RectRequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }
                    else if (lblaction.Text == "5")
                    {
                        db.PreviewBack_RectRequest(int.Parse(Request["RectRequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }
                    else if (lblaction.Text == "13")
                    {
                        db.Close_RectRequest(int.Parse(Request["RectRequestID"]), txtComment.Text, utility.Login(loggedUser));

                    }
                    else if (lblaction.Text == "10") {

                        int? budgedYear = null;
                        bool budged;
                        if (LstBudged.SelectedIndex == 0)
                        {
                            budged = true;
                        }
                        else
                        {

                            budged = false;
                        }
                        if (txtDetailBudgedYear.Text != string.Empty)
                        {
                            budgedYear = int.Parse(txtDetailBudgedYear.Text);
                        }

                        db.Update_RectRequest(int.Parse(Request["RectRequestID"]), LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtPrincipalTask.Text, txtJobTitle.Text, txtMinimumQualification.Text, txtOhterRequirements.Text,
                        int.Parse(txtNumberOfPosition.Text), LstDuration.SelectedValue, (DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)), false, budged, false, CbIsReplacement.Checked, LstRequestor.SelectedValue, txtIsReplacement.Text, txtReplacementEmployeeName.Text, txtReplacementReason.Text,
                        txtReplacementPostNumber.Text, txtReplacementJobTitle.Text, int.Parse(LstTypeOfContrat.SelectedValue), int.Parse(LstJobGrade.SelectedValue), LstEmployeeType.SelectedValue, int.Parse(LstRequestMethode.SelectedValue), LstCostCenter.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), LstReportTo.SelectedValue, utility.Login(loggedUser), int.Parse(LstEmployeeType.SelectedValue), null, null, budgedYear,txtComment.Text, utility.Login(loggedUser));

                        Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                        
                    }
                    //Response.Redirect("My_RequestToAproval.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                    ModalPopupExtender1.Show();
                }
                catch (FormatException ex)
                {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                catch (NullReferenceException ex)
                {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                catch (IndexOutOfRangeException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                catch (ArithmeticException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }

                catch (TimeoutException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }

                catch (SystemException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }

                catch (Exception ex)
                {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
            }
            }

        protected void btnSaveHrSystem_Click(object sender, EventArgs e)
        {
            int? budgetYear = null;
            if (txtCommentSec.Text == string.Empty || string.IsNullOrWhiteSpace(txtCommentSec.Text))
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
           else if ((txtBudgetYear.Text == string.Empty || string.IsNullOrWhiteSpace(txtBudgetYear.Text)) && LstBudgetHr.SelectedIndex == 1)
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "txtBudgetYearEmptyError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            else
            {
                try
                {

                    if (lblaction.Text == "1")
                    {
                        int id = int.Parse(Request.Params["RectRequestID"]);
                        var rect = db.Get_RectRequest_ByID(id, utility.Login(loggedUser));
                       
                        bool ispos, budget=false;
                      
                        ispos = CbOps.Checked;
                        if (LstBudgetHr.SelectedIndex == 1) { budgetYear = int.Parse(txtBudgetYear.Text); budget = true; }
                             
                            db.Approve_RectRequestHRSystem(int.Parse(Request["RectRequestID"]), txtCommentSec.Text, utility.Login(loggedUser),budget, ispos, budgetYear);
                            Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                       
                        
                    }
                    else if (lblaction.Text == "3")
                    {
                        db.Reject_RectRequest(int.Parse(Request["RectRequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }
                    else if (lblaction.Text == "4")
                    {
                        db.Return_RectRequest(int.Parse(Request["RectRequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }
                    //Response.Redirect("My_RequestToAproval.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal_hrsystem();", true);

                    ModalPopupExtender1.Show();
                }
                catch (FormatException ex)
                {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                catch (NullReferenceException ex)
                {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                catch (IndexOutOfRangeException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                catch (ArithmeticException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }

                catch (TimeoutException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }

                catch (SystemException ex)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }

                catch (Exception ex)
                {

                    string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
        

            lblaction.Text = "3";
            attachement.Visible = false;
            lblFile.Visible = false;
            FileUpload2.Visible = false;
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + BtnReject.Text;

            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
           
        }

        protected void BtnReturnToOrginator_Click(object sender, EventArgs e)
        {
            lblaction.Text = "4";
            attachement.Visible = false;
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + BtnReturnToOrginator.Text;
            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            
        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
            try
            {

                if (ValidateInputs())
                {

                    //int? budgedYear = null;
                    //bool budged;
                    //if (LstBudged.SelectedIndex == 0) {
                    //    budged = true;
                    //}
                    //else
                    //{

                    //    budged = false;
                    //}
                    //if (txtDetailBudgedYear.Text != string.Empty) {
                    //    budgedYear = int.Parse(txtDetailBudgedYear.Text);
                    //}

                    //db.Update_RectRequest(int.Parse(Request["RectRequestID"]), LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtPrincipalTask.Text, txtJobTitle.Text, txtMinimumQualification.Text, txtOhterRequirements.Text,
                    //int.Parse(txtNumberOfPosition.Text), LstDuration.SelectedValue, (DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)), false,budged, false, CbIsReplacement.Checked, LstRequestor.SelectedValue, txtIsReplacement.Text, txtReplacementEmployeeName.Text, txtReplacementReason.Text,
                    //txtReplacementPostNumber.Text, txtReplacementJobTitle.Text, int.Parse(LstTypeOfContrat.SelectedValue), int.Parse(LstJobGrade.SelectedValue), LstEmployeeType.SelectedValue, int.Parse(LstRequestMethode.SelectedValue), LstCostCenter.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), LstReportTo.SelectedValue, utility.Login(loggedUser),int.Parse(LstEmployeeType.SelectedValue),null,null,budgedYear);

                    //Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    ////Response.Redirect("My_allRequest.aspx");


                    //ModalPopupExtender1.Show();

                    lblaction.Text = "10";
                    attachement.Visible = false;
                    txtComment.Text = string.Empty;
                    this.Form.DefaultButton = this.btnSave.UniqueID;
                    lblTitre.Text = "- " + btnUpdate.Text;
                    Page.SetFocus(txtComment.ClientID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                }
            }
            catch (FormatException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (NullReferenceException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
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
                //else if (LstEmployeeType.SelectedValue == "2")
                //{

                //    if (LstEmploymentEntity.SelectedIndex == 0)
                //    {

                //        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstEmploymentEntityEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstEmploymentEntityEmptyError");
                //        valid = false;
                //    }

                //    if (LstCampLocation.SelectedIndex == 0)
                //    {

                //        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstCampLocationEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstCampLocationEmptyError");
                //        valid = false;
                //    }

                //}
                if (LstTypeOfContrat.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstTypeOfContratEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstTypeOfContratEmptyError");
                    valid = false;
                }
                if (LstDuration.SelectedIndex == 0 && LstDuration.Enabled == true)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDurationEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDurationEmptyError");
                    valid = false;
                }
                if (LstCostCenter.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstCostCenterEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstCostCenterEmptyError");
                    valid = false;
                }

                if (LstBudged.Enabled == true) {

                    if (LstBudged.SelectedIndex== 0) {

                        if (txtDetailBudgedYear.Text == string.Empty || string.IsNullOrWhiteSpace(txtDetailBudgedYear.Text))
                        {
                            error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtBudgetYearEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtBudgetYearEmptyError");
                            valid = false;
                        }
                    }
                }



                if (LstCategory.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDurationEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDurationEmptyError");
                    valid = false;
                }

                if (txtPlannedStartDate.Text == string.Empty || string.IsNullOrWhiteSpace(txtPlannedStartDate.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError");
                    valid = false;
                }

                //if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text)) && ((DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) <= DateTime.Today.AddDays(-1)))
                //{
                //    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess");
                //    valid = false;
                //}

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


                if (FileUpload2.PostedFiles.Count == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "FileEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "FileEmptyError");
                    valid = false;
                }

                if (valid == false)
                {
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }


                return valid;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void CbIsReplacement_CheckedChanged(object sender, EventArgs e)
        {
            txtIsReplacement.Enabled = CbIsReplacement.Checked;
            txtReplacementEmployeeName.Enabled = CbIsReplacement.Checked;
            txtReplacementJobTitle.Enabled = CbIsReplacement.Checked;
            txtReplacementPostNumber.Enabled = CbIsReplacement.Checked;
            txtReplacementReason.Enabled = CbIsReplacement.Checked;

            if (CbIsReplacement.Checked == false) { 
            txtIsReplacement.Text = string.Empty;
            txtReplacementEmployeeName.Text = string.Empty;
            txtReplacementJobTitle.Text = string.Empty;
            txtReplacementPostNumber.Text = string.Empty;
            txtReplacementReason.Text = string.Empty;
        }

            if (CbIsReplacement.Enabled == false) {

                txtIsReplacement.Enabled = false;
                txtReplacementEmployeeName.Enabled = false;
                txtReplacementJobTitle.Enabled = false;
                txtReplacementPostNumber.Enabled = false;
                txtReplacementReason.Enabled = false;
            }
          
        }

      
       

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string userLogin = utility.Login(loggedUser);
            var infoRemplacerNumero = Developper.Employee.Get_Employee().Where(x => x.EmployeeId == txtIsReplacement.Text).ToList();
            foreach (var l in infoRemplacerNumero)
            {
                txtReplacementEmployeeName.Text = l.FirstName;
                txtReplacementJobTitle.Text = l.EmergencyContactName;
                txtReplacementPostNumber.Text = l.PositionCode;

            }


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
            else
            {
                populateDuration();
                LstDuration.Enabled = true;
                //txtDuration.Text = string.Empty;
            }
        }

        protected void btnBackToOriginator_Click(object sender, EventArgs e)
        {
            lblaction.Text = "5";
            attachement.Visible = false;
            lblFile.Visible =false;
            FileUpload2.Visible = false;
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnBackToOriginator.Text;
            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (NullReferenceException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
        }

        protected void dllUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string employedID = db.Get_Employee_User().Where(x => x.Login == dllUsers.SelectedValue).FirstOrDefault().EmployeeId;


            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "RECTRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));
            dllRole.Items.Insert(1, employedID);
        }

        protected void btnrRealocate_Click(object sender, EventArgs e)
        {
           txtCommentReallocate.Text = string.Empty;
            this.Form.DefaultButton = this.BtnSaveReallocate.UniqueID;
      

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openReallocate();", true);
        }

        protected void BtnSaveReallocate_Click(object sender, EventArgs e)
        {
            try
            {

                if (dllUsers.SelectedIndex == 0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllUsersError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }

                else if (dllRole.SelectedIndex == 0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllRoleError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else if (txtCommentReallocate.Text == string.Empty || string.IsNullOrWhiteSpace(txtCommentReallocate.Text))
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {



                    db.HRRequest_Reallocate(int.Parse(Request.Params["RectRequestID"]), txtCommentReallocate.Text, dllRole.SelectedValue, utility.Login(loggedUser));



                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideReallocate();", true);
                    ModalPopupExtender1.Show();
                    //Response.Redirect("Admin/HR_Request/HRRequisition.aspx");
                }
            }
            catch (FormatException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (NullReferenceException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
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

                if (dllUserAdHoc.SelectedIndex ==0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllUsersError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }


                else if (txtCommentAdHoc.Text == string.Empty || string.IsNullOrWhiteSpace(txtCommentAdHoc.Text))
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    db.RECTRequest_AdHoc(int.Parse(Request.Params["RectRequestID"]), dllUserAdHoc.SelectedValue, txtCommentAdHoc.Text, utility.Login(loggedUser));


                    //Response.Redirect("Default_View_Approve.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideAdHoc();", true);
                    ModalPopupExtender1.Show();
                }
            }
            catch (FormatException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (NullReferenceException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewReports.aspx?IDRECTRequest=" + Request.Params["RectRequestID"]);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            lblaction.Text = "13";
            attachement.Visible = false;
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnClose.Text;
            Page.SetFocus(txtComment.ClientID);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void LstEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateJobGrade();

            if (LstEmployeeType.SelectedValue == "2")
            {
                LstEmploymentEntity.Enabled = true;
                LstCampLocation.Enabled = true;
            }
            else
            {
                LstEmploymentEntity.Enabled = false;
                LstCampLocation.Enabled = false;
            }
        }

        protected void CbBudget_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnCallBack_Click(object sender, EventArgs e)
        {
            lblaction.Text = "5";
            attachement.Visible = false;
            lblFile.Visible = false;
            FileUpload2.Visible = false;
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnCallBack.Text;
            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void btnPrintExpatriate_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewReports.aspx?IDRECTRequestExp=" + Request.Params["RectRequestID"]);
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            // Response.Redirect("default.aspx");
            Response.Redirect("My_RequestToAproval.aspx?type=RECTRequest");

        }

        //protected void cbDetailBudged_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "RECTRequest").FirstOrDefault().Number == 1)
        //    {
        //        txtDetailBudgedYear.Enabled = cbDetailBudged.Checked;

        //        if (cbDetailBudged.Checked == false) txtDetailBudgedYear.Text = string.Empty;
        //    }
        //}

        protected void LstBudged_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "RECTRequest").FirstOrDefault().Number == 1)
            {
                if (LstBudged.SelectedIndex == 0)
                {
                    txtDetailBudgedYear.Enabled = true;
                }
                else {
                    txtDetailBudgedYear.Enabled = false;
                }
                

                if (LstBudged.SelectedIndex==1) txtDetailBudgedYear.Text = string.Empty;
            }
        }

        protected void LstBudgetHr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstEmployeeType.SelectedValue == "2")
            {
                if (LstBudgetHr.SelectedIndex == 1)
                {
                    blocops.Visible = true;
                    
                }
                else
                {
                    blocops.Visible = true;
                    blocudgetYear.Visible = false;
                }
            
            }
            else
            {
                blocops.Visible = true;

            }

            if (LstBudgetHr.SelectedIndex == 1) blocudgetYear.Visible = true;
            else blocudgetYear.Visible = false;

            txtBudgetYear.Text = string.Empty;
        }
    }
}