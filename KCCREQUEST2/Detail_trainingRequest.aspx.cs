using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.Globalization;
using System.IO;
using System.Data;

using System.Runtime.InteropServices;

namespace KCCREQUEST2
{
    public partial class Detail_trainingRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        kcc_request_dbEntities dbo = new kcc_request_dbEntities();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CachedLogin comes from BasePage and it is a sessionvariable

                PopulatePriority();
                populateTrainingType();
                populateService();
                populateTrainingCours();
                populateTrainingLocation();
                disableControl(false);
                populateUsers();
                populateUsersAdHoc();
                populateRole();
                populateBuged();
                PopulateCategory();




                txtMotivationEnvironment.Enabled = CbEnvironment.Checked;
                txtMotivationProduction.Enabled = CbProduction.Checked;
                txtMotivationSafety.Enabled = CbSafety.Checked;

                btnApproval.Visible = false;
                BtnReject.Visible = false;
                BtnReturnToOrginator.Visible = false;
                btnUpdate.Visible = false;
                btnBackToPreview.Visible = false;
                btnHold.Visible = false;
                btnSendToNationalDevelopment.Visible = false;

                try
                {
                    if (Request.Params["TrID"] != null)
                    {
                        int id = int.Parse(Request.Params["TrID"]);

                        var Train = db.Get_TrainingRequest_ByID(id, CachedLogin);
                        PopulateEmployeeAndDepartment(Train.RequestorLogin);
                        title.InnerText = Train.Id.ToString();
                        LstDepartment.SelectedValue = Train.Department;
                        LstRequestor.SelectedValue = Train.RequestorLogin;
                        LstTrainingType.SelectedValue = Train.TypeID.ToString();
                        txtCourse.Text = Train.Course;
                        txtRequestDescription.Text = Train.Description;
                        txtJustificationWhy.Text = Train.Comment;
                        LstPriority.SelectedValue = Train.PrioriteID.ToString();
                        LstTrainingCategory.SelectedValue = Train.CategoryID.ToString();
                        txtPlannedStartDate.Text = DateTime.Parse(Train.PlannedDeliveryDate.ToString()).ToString(format: "MM-dd-yyyy");
                        txtBeneficiaryJobTitle.Text = Train.BeneficiaryJobTitle;
                        txtTrainingEventID.Text = Train.TrainingEventID.ToString();
                        populateTrainingCalendar(Convert.ToInt32(Train.TrainingEventID));

                    
                    

                    if (Train.BudgedID == true)
                        {
                            LstBudged.SelectedIndex = 0;
                        }
                        else
                        {
                            LstBudged.SelectedIndex = 1;
                        }
                        var TrainCat = db.Get_TrainingRequest_Motivation(id);

                        if (TrainCat.Production != string.Empty) { CbProduction.Checked = true; txtMotivationProduction.Text = TrainCat.Production; }
                        else if (TrainCat.Production == string.Empty) { txtMotivationProduction.Enabled = false; }

                        if (TrainCat.Environment != string.Empty) { CbEnvironment.Checked = true; txtMotivationEnvironment.Text = TrainCat.Environment; }
                        else if (TrainCat.Environment == string.Empty) { txtMotivationEnvironment.Enabled = false; }

                        if (TrainCat.Safety != string.Empty) { CbSafety.Checked = true; txtMotivationSafety.Text = TrainCat.Safety; }
                        else if (TrainCat.Safety == string.Empty) { txtMotivationSafety.Enabled = false; }

                        txtCurrentUser.Text = Train.CurrentUser;
                        txtStatut.Text = Train.StatusDescription;

                        if (Train.TypeID == 1)
                        {
                            LstBudgetSec.Enabled = false;
                            txtPlannedStartDate.Enabled=false;
                        LstBudged.Enabled = false;
                        }
                        else
                        {
                        pnlExternalTrng.Visible = true;
                        LstBudgetSec.Enabled = true;
                        txtPlannedStartDate.Enabled = true;
                        txtCourseDates.Text = Train.CourseDates;
                        txtCertification.Text = Train.Certification;
                        txtCourseFees.Text = Train.CourseFees;
                        txtCourseVenue.Text = Train.CourseVenue;
                        txtDurationOfStay.Text = Train.DurationOfstay;
                        txtEnterpriseBenefit.Text = Train.Benefit;
                        txtIndividualPlan.Text = Train.DevelopmentPlan;
                        txtLegalRequirement.Text = Train.LegalRequirement;
                        txtTrainingPurpose.Text = Train.Purpose;
                        txtTransport.Text = Train.Transport;
                        txtVisaRequirement.Text = Train.VisaRequirement;
                        txtDepartureDate.Text = DateTime.Parse(Train.DepartureDate.ToString()).ToString(format: "MM-dd-yyyy");
                        txtReturnDate.Text = DateTime.Parse(Train.ReturnDate.ToString()).ToString(format: "MM-dd-yyyy");
                        LstBudged.Enabled = true;
                    }



                        dgvFiles.DataSource = db.Get_TrainingRequest_File(id);
                        dgvFiles.DataBind();

                        dgvEmployees.DataSource = db.Get_TrainingRequest_Detail(id);
                        dgvEmployees.DataBind();

                        //ExportGridToExcel();


                        dgvHistory.DataSource = db.Get_TrainingRequest_History(id);
                        dgvHistory.DataBind();
                        if (true)
                        {

                        }

                        //fvTrainningProgramme.DataSource = db.Get_TrainingCalendar(id.ToString()).ToList();
                        //fvTrainningProgramme.DataBind();

                        var TrainActions = db.Get_KCCRequest_Action(id, CachedLogin, "TRNRequest");
                        int statut = (int)Train.StatusID;

                        if (statut == 7)
                        {
                            btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "btnApprovalBudget");
                        }

                        var requestToApprove = db.Get_KCCRequest_ToApprove("TRNRequest", CachedLogin).Where(x => (x.Id == id)).FirstOrDefault();
                        if (TrainActions.Where(x => x.Action == "CanEdit").FirstOrDefault().Visibility >= 1)
                        {
                            btnUpdate.Visible = true;
                            btnApproval.Visible = false;
                            BtnReject.Visible = true;
                            btnBackToPreview.Visible = false;
                            BtnReturnToOrginator.Visible = false;
                            btnHold.Visible = false;
                            disableControl(true);

                        }

                        if (TrainActions.Where(x => x.Action == "CanCallBack").FirstOrDefault().Visibility >= 1)
                        {
                            btnCallBack.Visible = true;

                        }
                        else btnCallBack.Visible = false;

                        if ((requestToApprove != null) && (!string.IsNullOrEmpty(requestToApprove.Module)))
                        {
                            if (statut <= 7)
                            {

                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = true;
                                btnBackToPreview.Visible = true;
                                btnHold.Visible = false;
                                if (Train.DocPos == "TRN_Admin") { BtnReject.Visible = false; btnHold.Visible = false; }
                                if (Train.DocPos == "TRN_MAN")
                                {
                                    btnHold.Visible = true;
                                }
                                if (Train.DocPos == "TRN_SUP_Engineering" || Train.DocPos == "TRN_SUP_General" || Train.DocPos == "TRN_SUP_Technical")
                                {
                                    BtnReject.Visible = false;
                                    btnHold.Visible = false;
                                    btnBackToPreview.Visible = false;
                                }
                            }
                            else if (statut == 14)
                            {

                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = true;
                                btnBackToPreview.Visible = true;
                                btnHold.Visible = false;
                            }


                        if (statut == 15)
                        {

                            btnAdHoc.Visible = false;

                            btnApproval.Visible = true;
                            btnUpdate.Visible = false;
                            BtnReject.Visible = false;
                            BtnReturnToOrginator.Visible = false;
                            btnBackToPreview.Visible = false;
                            btnHold.Visible = false;

                            btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "Adhocstatus");
                        }
                        else {
                            btnApproval.Text= (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Text");
                        }


                        }
                        else { btnAdHoc.Visible = false; }


                        var adminsList = db.Get_KCCRequestIsAdmin(CachedLogin).Where(x => x.ModuleID == "TRNRequest").FirstOrDefault();

                        if (adminsList.Number == 0)
                        {


                            btnrRealocate.Visible = false;
                        }
                        else if (adminsList.Number == 1)
                        {



                            btnUpdate.Visible = true;
                            //LstBudged.Enabled = true;
                            BtnReject.Visible = true;
                            disableControl(true);

                        }

                        if (statut == 10)
                        {
                            disableControl(false);
                            BtnReturnToOrginator.Visible = false;
                            btnBackToPreview.Visible = false;
                            BtnReject.Visible = false;
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            btnAdHoc.Visible = false;
                            btnrRealocate.Visible = false;
                        }

                        if (btnCallBack.Visible == true)
                        {
                            disableControl(false);
                            BtnReturnToOrginator.Visible = false;
                            btnBackToPreview.Visible = false;
                            BtnReject.Visible = false;
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            btnAdHoc.Visible = false;
                            btnrRealocate.Visible = false;
                        }
                    if (db.Check_Can_SendToNationalDevelopment(id, CachedLogin) > 0)
                    {
                        btnSendToNationalDevelopment.Visible = true;
                    }
                    else
                    {
                        btnSendToNationalDevelopment.Visible = false;
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
        }

        public void disableControl(bool statut)
        {

            LstRequestor.Enabled = statut;
            LstPriority.Enabled = statut;
            //txtPlannedStartDate.Enabled = statut;
            LstTrainingType.Enabled = statut;
            txtCourse.Enabled = statut;
            txtRequestDescription.Enabled = statut;
            txtJustificationWhy.Enabled = statut;
            txtMotivationEnvironment.Enabled = statut;
            txtMotivationProduction.Enabled = statut;
            txtMotivationSafety.Enabled = statut;
            CbProduction.Enabled = statut;
            CbEnvironment.Enabled = statut;
            CbSafety.Enabled = statut;
            txtBeneficiaryJobTitle.Enabled = statut;


            if (txtMotivationProduction.Text == string.Empty) { txtMotivationProduction.Enabled = false; }


            if (txtMotivationEnvironment.Text == string.Empty) { txtMotivationEnvironment.Enabled = false; }


            if (txtMotivationSafety.Text == string.Empty) { txtMotivationSafety.Enabled = false; }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //db.Update_Training(int.Parse(Request.Params["TrID"]), LstRequestor.SelectedValue, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtJustificationWhy.Text, txtCourse.Text, (DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)), int.Parse(LstTrainingType.SelectedValue), int.Parse(LstPriority.SelectedValue), 1, CachedLogin);

                //db.update_TrainingMotivation(int.Parse(Request.Params["TrID"]), txtMotivationProduction.Text, txtMotivationEnvironment.Text, txtMotivationSafety.Text);

                //insereFile(int.Parse(Request.Params["TrID"]));

                ////Response.Redirect("Default_View_Approve.aspx");


                //ModalPopupExtender1.Show();


                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = "- " + btnUpdate.Text;

                Page.SetFocus(txtComment.ClientID);
                lblaction.Text = "10";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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

        protected void CbProduction_CheckedChanged(object sender, EventArgs e)
        {
            txtMotivationProduction.Enabled = CbProduction.Checked;
            txtMotivationProduction.Text = string.Empty;
        }

        protected void CbEnvironment_CheckedChanged(object sender, EventArgs e)
        {
            txtMotivationEnvironment.Enabled = CbEnvironment.Checked;
            txtMotivationEnvironment.Text = string.Empty;
        }

        protected void CbSafety_CheckedChanged(object sender, EventArgs e)
        {
            txtMotivationSafety.Enabled = CbSafety.Checked;
            txtMotivationSafety.Text = string.Empty;
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



        public void PopulateCategory()
        {
            try
            {
                LstTrainingCategory.DataSource = dbo.SP_Get_TRNRequest_Category();
                LstTrainingCategory.DataValueField = "CategoryId";
                LstTrainingCategory.DataTextField = "CategoryDescription";
                LstTrainingCategory.DataBind();
                LstTrainingCategory.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllpriority"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void populateTrainingType()
        {

            try
            {
                LstTrainingType.DataSource = db.Get_TrainingType(CachedLogin);
                LstTrainingType.DataValueField = "TypeID";
                LstTrainingType.DataTextField = "TypeDescription";
                LstTrainingType.DataBind();
                LstTrainingType.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlltypeTraining"));
            }
            catch (Exception ex)
            {

                throw;
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
        public void populateBuged()
        {
            ListItem itemYes = new ListItem((string)GetGlobalResourceObject("LocalizedText", "budgedYes"), "true");
            ListItem itemNo = new ListItem((string)GetGlobalResourceObject("LocalizedText", "budgedNon"), "false");
            LstBudged.Items.Insert(0, itemYes);
            LstBudged.Items.Insert(1, itemNo);
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

            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "TRNRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));

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

                if (db.Get_KCCRequestIsAdmin(CachedLogin).Where(x => x.ModuleID == "TRNRequest").FirstOrDefault().Number == 1)
                {

                    LstDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    LstDepartment.DataTextField = "LongDescription";
                    LstDepartment.DataValueField = "Code";
                    LstDepartment.DataBind();
                    LstDepartment.Enabled = true;

                    LstDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(requestor, "").FirstOrDefault().DepartmentCode;


                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void populateService()
        {
            try
            {
                lstServiceSec.DataSource = db.Get_TrainingService();
                lstServiceSec.DataValueField = "ServiceID";
                lstServiceSec.DataTextField = "ServiceDescription";
                lstServiceSec.DataBind();
                lstServiceSec.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllService"));

            }
            catch (Exception s)
            {

            }
        }

        public void populateTrainingCours()
        {

            try
            {
                LstCourseDescription.DataSource = db.Get_TrainingCours();
                LstCourseDescription.DataValueField = "CoursID";
                LstCourseDescription.DataTextField = "CoursDescription";
                LstCourseDescription.DataBind();
                LstCourseDescription.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTrainingCours"));
            }
            catch (Exception s)
            {

            }
        }

        public void populateTrainingLocation()
        {

            try
            {
                LstTrainingLocation.DataSource = db.Get_TrainingLocation();
                LstTrainingLocation.DataValueField = "LocationId";
                LstTrainingLocation.DataTextField = "LocationDescription";
                LstTrainingLocation.DataBind();
                LstTrainingLocation.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTrainingLocation"));
            }
            catch (Exception s)
            {

            }
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
                int id = int.Parse(Request.Params["TrID"]);

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

                        byte[] bytes = (byte[])db.Get_TrainingRequest_File(id).Where(x => x.FileID.ToString() == fileID.Text).FirstOrDefault().File.ToArray();

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
            lblaction.Text = "1";
            int id = int.Parse(Request.Params["TrID"]);
            var Train = db.Get_TrainingRequest_ByID(id, CachedLogin);
             
            if (Train.StatusID == 1 || Train.StatusID == 2 || Train.StatusID == 5 || Train.StatusID == 14 || Train.StatusID == 6 || Train.StatusID == 7 || Train.StatusID == 15 || Train.StatusID == 3)
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = "- " + btnApproval.Text;

                Page.SetFocus(txtComment.ClientID);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            //else if (Train.StatusID == 3)
            //{
            //    txtCommentSec.Text = string.Empty;
            //    this.Form.DefaultButton = this.btnSaveSec.UniqueID;
            //    lblTitre.Text = "- " + btnApproval.Text;

            //    //Page.SetFocus(txtComment.ClientID);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_sec();", true);
            //}
            else if (Train.StatusID == 4)
            {
                txtCommentPlanner.Text = string.Empty;
                this.Form.DefaultButton = this.btnSavePlan.UniqueID;
                lblTitre.Text = "- " + btnApproval.Text;

                //Page.SetFocus(txtComment.ClientID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal_Plan();", true);
            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + BtnReject.Text;

            Page.SetFocus(txtComment.ClientID);

            lblaction.Text = "2";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void BtnReturnToOrginator_Click(object sender, EventArgs e)
        {
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + BtnReturnToOrginator.Text;

            Page.SetFocus(txtComment.ClientID);
            lblaction.Text = "3";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            try
            {

                if (txtComment.Text == string.Empty || string.IsNullOrWhiteSpace(txtComment.Text))
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "txtCommentPlannerEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    if (lblaction.Text == "1")
                    {
                        db.Approve_Training(int.Parse(Request.Params["TrID"]), txtComment.Text, CachedLogin);
                        insereFile(int.Parse(Request.Params["TrID"]));
                    }
                    else if (lblaction.Text == "3")
                    {

                        db.Return_Training(int.Parse(Request.Params["TrID"]), txtComment.Text, CachedLogin);
                        insereFile(int.Parse(Request.Params["TrID"]));
                    }
                    else if (lblaction.Text == "2")
                    {

                        db.Reject_Training(int.Parse(Request.Params["TrID"]), txtComment.Text, CachedLogin);
                        insereFile(int.Parse(Request.Params["TrID"]));
                    }
                    else if (lblaction.Text == "4")
                    {

                        db.Back_Training(int.Parse(Request.Params["TrID"]), txtComment.Text, CachedLogin);
                        insereFile(int.Parse(Request.Params["TrID"]));
                    }
                    else if (lblaction.Text == "5")
                    {
                        db.hold_Training(int.Parse(Request.Params["TrID"]), txtComment.Text, CachedLogin);
                        insereFile(int.Parse(Request.Params["TrID"]));
                    }
                    else if (lblaction.Text == "10") {

                    DateTime fromDate = (string.IsNullOrEmpty(txtDepartureDate.Text)) ? DateTime.Now : DateTime.ParseExact(txtDepartureDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    DateTime returnDate = (string.IsNullOrEmpty(txtReturnDate.Text)) ? DateTime.Now : DateTime.ParseExact(txtReturnDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);


                    db.Update_Training(
                            int.Parse(Request.Params["TrID"]),
                            LstRequestor.SelectedValue,
                            txtBeneficiaryJobTitle.Text,
                            txtRequestDescription.Text,
                            txtJustificationWhy.Text,
                            txtCourse.Text,
                            (DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)),
                            int.Parse(LstTrainingType.SelectedValue), int.Parse(LstPriority.SelectedValue),
                            int.Parse(LstTrainingCategory.SelectedValue), CachedLogin, txtComment.Text,
                            0,
                            txtCourseFees.Text,
                            txtCourseVenue.Text,
                            txtVisaRequirement.Text,
                            txtTransport.Text, txtCertification.Text,
                            txtLegalRequirement.Text,
                            fromDate,
                            //(DateTime.ParseExact(txtDepartureDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)),
                            returnDate,
                            //(DateTime.ParseExact(txtReturnDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)),
                            txtCourseDates.Text, txtDurationOfStay.Text, txtTrainingPurpose.Text, txtEnterpriseBenefit.Text, txtIndividualPlan.Text, Convert.ToBoolean(LstBudged.SelectedValue)); ;

                        db.update_TrainingMotivation(int.Parse(Request.Params["TrID"]), txtMotivationProduction.Text, txtMotivationEnvironment.Text, txtMotivationSafety.Text);
                        insereFile(int.Parse(Request.Params["TrID"]));
                    }
                    //Response.Redirect("Default_View_Approve.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                    ModalPopupExtender1.Show();
                }
        }
            catch (FormatException ex)
            {
                //        string errorMessage = ex.InnerException.Message.Replace("'", "\\'"); // Escape single quotes
                //string script = $"<script type='text/javascript'>alert('{errorMessage}');</script>";
                //ClientScript.RegisterStartupScript(this.GetType(), "ShowError", script);

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

        protected void btnSaveSec_Click(object sender, EventArgs e)
        {
            try
            {
                int isbudget;

                if (LstBudgetSec.SelectedIndex == 0) isbudget = 0;
                else isbudget = 1;

                if (lstServiceSec.SelectedIndex == 0)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lstServiceSecEmptyError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

                }
                else if (txtCommentSec.Text == string.Empty || string.IsNullOrWhiteSpace(txtCommentSec.Text))
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "txtCommentPlannerEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    db.Approve_TrainingSec(int.Parse(Request.Params["TrID"]), txtCommentSec.Text, isbudget, int.Parse(lstServiceSec.SelectedValue), CachedLogin);
                    insereFile(int.Parse(Request.Params["TrID"]));
                    //Response.Redirect("Default_View_Approve.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal_sec();", true);
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

        protected void btnSavePlan_Click(object sender, EventArgs e)
        {
            try
            {

                string periodicity = timepicker1.Value + " - " + timepicker2.Value;

                if (ValidateInputs())
                {
                    db.Approve_TrainingSup(int.Parse(Request.Params["TrID"]), int.Parse(LstTrainingLocation.SelectedValue), LstCourseDescription.SelectedValue,
                   DateTime.ParseExact(txtStartDateComment.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(txtEndDateComment.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture), getSelectDay(), periodicity, txtTrainingVenue.Text, txtCommentPlanner.Text, CachedLogin);
                    insereFile(int.Parse(Request.Params["TrID"]));
                    //Response.Redirect("Default_View_Approve.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal_Plan();", true);
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

        public string getSelectDay()
        {

            string day = "";
            if (cbMonday.Checked == true) day = (string.IsNullOrEmpty(day)) ? cbMonday.Text : day + "," + cbMonday.Text;
            if (cbTuesday.Checked == true) day = (string.IsNullOrEmpty(day)) ? cbTuesday.Text : day + "," + cbTuesday.Text;
            if (cbWednesday.Checked == true) day = (string.IsNullOrEmpty(day)) ? cbWednesday.Text : day + "," + cbWednesday.Text;
            if (cbThursday.Checked == true) day = (string.IsNullOrEmpty(day)) ? cbThursday.Text : day + "," + cbThursday.Text;
            if (cbFriday.Checked == true) day = (string.IsNullOrEmpty(day)) ? cbFriday.Text : day + "," + cbFriday.Text;
            if (cbSaturday.Checked == true) day = (string.IsNullOrEmpty(day)) ? cbSaturday.Text : day + "," + cbSaturday.Text;
            if (cbSunday.Checked == true) day = (string.IsNullOrEmpty(day)) ? cbSunday.Text : day + "," + cbSunday.Text;

            return day;
        }


        public bool ValidateInputs()
        {
            try
            {
                bool valid = true;
                string error = string.Empty;
                if (txtStartDateComment.Text == string.Empty || string.IsNullOrWhiteSpace(txtStartDateComment.Text))
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtStartDateCommentEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "txtStartDateCommentEmptyError");
                    valid = false;

                }
                if (txtEndDateComment.Text == string.Empty || string.IsNullOrWhiteSpace(txtEndDateComment.Text))
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtEndDateCommentEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "txtEndDateCommentEmptyError");
                    valid = false;

                }
                if (DateTime.Parse(txtStartDateComment.Text) > DateTime.Parse(txtEndDateComment.Text))
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "CompareDateStartAndEndEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "CompareDateStartAndEndEmptyError");
                    valid = false;
                }

                if (LstTrainingLocation.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstTrainingLocationEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "LstTrainingLocationEmptyError");
                    valid = false;

                }

                if (LstCourseDescription.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstCourseDescriptionEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "LstCourseDescriptionEmptyError");
                    valid = false;

                }

                if (txtTrainingVenue.Text == string.Empty || string.IsNullOrWhiteSpace(txtTrainingVenue.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtTrainingVenueEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "txtTrainingVenueEmptyError");
                    valid = false;

                }
                if (txtCommentPlanner.Text == string.Empty || string.IsNullOrWhiteSpace(txtCommentPlanner.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtCommentPlannerEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "txtCommentPlannerEmptyError");
                    valid = false;

                }
                if (string.IsNullOrEmpty(getSelectDay()))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtTrnDayEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "txtTrnDayEmptyError");
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

        protected void btnBackToPreview_Click(object sender, EventArgs e)
        {
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnBackToPreview.Text;

            Page.SetFocus(txtComment.ClientID);

            lblaction.Text = "4";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnHold.Text;

            Page.SetFocus(txtComment.ClientID);

            lblaction.Text = "5";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        public void insereFile(int id)
        {

            HttpFileCollection fileCollection = Request.Files;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];
                string fileName = Path.GetFileName(uploadfile.FileName);
                string contentType = uploadfile.ContentType;
                if (uploadfile.ContentLength > 0)
                {
                    byte[] uploadFile = new byte[uploadfile.InputStream.Length + 1];

                    uploadfile.InputStream.Read(uploadFile, 0, uploadFile.Length);
                    db.insertTrainingFile(id, fileName, contentType, uploadFile);
                }
            }
        }

        protected void dllUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string employedID = db.Get_Employee_User().Where(x => x.Login == dllUsers.SelectedValue).FirstOrDefault().EmployeeId;


            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "TRNRequest");
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


            //Page.SetFocus(txtComment.ClientID);
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



                    db.TRNRequest_Reallocate(int.Parse(Request.Params["TrID"]), txtCommentReallocate.Text, dllRole.SelectedValue, CachedLogin);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideReallocate();", true);
                    ModalPopupExtender1.Show();


                    //Response.Redirect("Admin/TRN_Request/TrainingRequest.aspx");
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

                if (dllUserAdHoc.SelectedIndex == 0)
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
                    db.TRNRequest_AdHoc(int.Parse(Request.Params["TrID"]), dllUserAdHoc.SelectedValue, txtCommentAdHoc.Text, CachedLogin);


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
            Response.Redirect("ViewReports.aspx?IDTRNRequest=" + Request.Params["TrID"]);
        }

        protected void btnCallBack_Click(object sender, EventArgs e)
        {
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnCallBack.Text;

            Page.SetFocus(txtComment.ClientID);

            lblaction.Text = "4";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            // Response.Redirect("default.aspx");

            Response.Redirect("My_RequestToAproval.aspx?type=TRNRequest");

        }
        public void populateTrainingCalendar(int TrainingEventID)
        {
            try
            {

                dgvTraniningCalendar.DataSource = db.Get_TrainingCalendar_ById(TrainingEventID).ToList();
                dgvTraniningCalendar.DataBind();

            }

            catch { }
        }

        protected void btnSendToNationalDevelopment_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.Params["TrID"]);
            if (db.Check_Can_SendToNationalDevelopment(id, CachedLogin) > 0)
            {
                db.Send_TRNRequest_ToNationalDevelopment(id, "", CachedLogin);

                Response.Redirect("My_RequestToAproval.aspx?type=TRNRequest");
            }
        }
    }



}
