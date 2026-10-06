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
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Core;

namespace KCCREQUEST2
{
    public partial class New_TrainingRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        kcc_request_dbEntities dbo = new kcc_request_dbEntities();

        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        DataTable tb = new DataTable();
        DataRow dr;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {


                if (!IsPostBack)
                {
                    PopulateEmployeeAndDepartment();
                    PopulatePriority();
                    populateTrainingType();
                    populateContractant();
                    populateTrainingCalendar(Convert.ToInt32(Request["TrainingEventID"])) ;
                    populateBuged();
                    createtable();
                    PopulateCategory();


                    txtMotivationEnvironment.Enabled = CbEnvironment.Checked;
                    txtMotivationProduction.Enabled = CbProduction.Checked;
                    txtMotivationSafety.Enabled = CbSafety.Checked;

                    txtCourse.Text = Request.Params["Training"];
                   
                    //Interne
                    if (txtCourse.Text != "") 
                    {   txtCourse.Enabled = false;
                        txtPlannedStartDate.Enabled = false;
                        LstTrainingType.SelectedValue = "1";
                        LstTrainingCategory.SelectedValue = "2";
                        LstTrainingCategory.Enabled = false;

                        //string Date  =  (DateTime.Parse(Request.Params["StartDate"]).ToString("MM/dd/yyyy"));
                        string Date="" ;

                        var data = db.Get_TrainingCalendar_ById(Convert.ToInt32(Request["TrainingEventID"])).Where(x => x.Description == "Start Date").ToList();
                        foreach (var i in data) { Date=i.Value.ToString(); }
                        Date = (DateTime.Parse(Date).ToString("MM/dd/yyyy"));

                        string month = Date.Split('/')[0];
                        string day = Date.Split('/')[1];
                        string year = Date.Split('/')[2];

                        Date = month + '-' + day + '-' + year;

                        txtPlannedStartDate.Text = Date;
                        //string Date = DepartureDate.Text = DateTime.Parse(Request.Params["FlightDate"]).ToString("MM/dd/yyyy");

                    }

                    string TrainingType = Request["TrainingType"];
                    string Seat = Request["Seat"];
                    if (TrainingType == "External")
                    {
                        txtCourse.Enabled = true;
                        LstTrainingType.SelectedValue = "2";
                        pnlExternalTrng.Visible = true;
                        lblexternalTrngQuotatMand.Visible = true;

                    } 


                    if ((TrainingType == "Internal") && (Seat == "0"))
                            {
                                Response.Redirect("TrainingCalendar.aspx");
                            }
                }
            }
            catch (Exception ex)
            {

                //string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                //lblErreur.Text = error;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

                //Response.Redirect("pageError.aspx");
                Response.Redirect("TrainingCalendar.aspx");
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

        public void populateTrainingType() {

            try
            {
                LstTrainingType.DataSource = db.Get_TrainingType(utility.Login(loggedUser));
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

        public void populateContractant() {

            try
            {
                PopulateContractorCompanies();
                PopulateContractorEmployees(string.Empty);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Loading contractor companies failed: {0}", ex);
                ShowContractorError("Unable to load the list of companies.");
            }
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
                    LstRequestor.Items.Insert(0, "Choose One");

                    //Loading Departments
                    LstDepartment.DataSource = listDepartement.GroupBy(x => x.DepartmentCode).Select(x => x.First());
                    LstDepartment.DataTextField = "DepartmentCode";
                    LstDepartment.DataValueField = "DepartmentCode";
                    LstDepartment.DataBind();
                    LstDepartment.Enabled = false;

                    dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x=>x.DepartmentCode==LstDepartment.SelectedValue).ToList();
                    dgvKCCEmployees.DataBind();

                }
                
                else
                {
                   

                    LstDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    LstDepartment.DataTextField = "LongDescription";
                    LstDepartment.DataValueField = "Code";
                    LstDepartment.DataBind();
                    LstDepartment.Enabled = true;

                    dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                    dgvKCCEmployees.DataBind();

                }
                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRNRequest").FirstOrDefault().Number == 1) {

                    LstDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    LstDepartment.DataTextField = "LongDescription";
                    LstDepartment.DataValueField = "Code";
                    LstDepartment.DataBind();
                    LstDepartment.Enabled = true;

                    LstDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(userLogin, "").FirstOrDefault().DepartmentCode;

                    dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                    dgvKCCEmployees.DataBind();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
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



        protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));

                dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                dgvKCCEmployees.DataBind();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void LstContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dgvContractor.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstContractor.SelectedValue);
            //dgvContractor.DataBind();
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
                if (LstTrainingType.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstTrainingTypeEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "LstTrainingTypeEmptyError");
                    valid = false;

                }
               if (int.Parse(LstTrainingType.SelectedValue) == 2) {
                    if (RequestFiles() == 0) {

                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "FileEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "FileEmptyError");
                        valid = false;

                    }
                }
               
                if (LstPriority.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError");
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

                if (txtCourse.Text == string.Empty || string.IsNullOrWhiteSpace(txtCourse.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtCourseEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtJobTitleEmptyError");
                    valid = false;
                }

                if (txtJustificationWhy.Text == string.Empty || string.IsNullOrWhiteSpace(txtJustificationWhy.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                    valid = false;
                }

                if (txtRequestDescription.Text == string.Empty || string.IsNullOrWhiteSpace(txtRequestDescription.Text)) {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError");
                    valid = false;
                }
                if (CbEnvironment.Checked == true)
                {

                    if (txtMotivationEnvironment.Text == string.Empty || string.IsNullOrWhiteSpace(txtMotivationEnvironment.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtMotivationEnvironmentEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtMotivationEnvironmentEmptyError");
                        valid = false;
                    }
                }
                if (CbProduction.Checked == true) {
                    if (txtMotivationProduction.Text == string.Empty || string.IsNullOrWhiteSpace(txtMotivationProduction.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtMotivationProductionEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtMotivationProductionEmptyError");
                        valid = false;
                    }
                }
                if (CbSafety.Checked == true) { 
                    if (txtMotivationSafety.Text == string.Empty || string.IsNullOrWhiteSpace(txtMotivationSafety.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtMotivationSafetyEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtMotivationSafetyEmptyError");
                        valid = false;
                    }
                }


                if (dgvKCCEmployees.Rows.Count >= 1|| dgvContractor.Rows.Count>=1)
                {
             
                    if (CountSelectEmplyee() == 0 && dgvContractor.Rows.Count==0)
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "CountSelectEmployeeEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "CountSelectEmployeeEmptyError");
                        valid = false;

                    }
                }


                //if (dgvContractor.Rows.Count >= 1)
                //{
                //    if (CountSelectContractor() == 0)
                //    {
                //        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "CountSelectContractorEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "CountSelectContractorEmptyError");
                //        valid = false;

                //    }
                //}

                if ((CountEmployees()+dgvContractor.Rows.Count) > Convert.ToInt32(Request["Seat"]) && (Request["TrainingType"] == "Internal"))
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtCountEmployeeError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtCountEmployeeError");
                    valid = false;

                }
                if (Request["TrainingType"] == "External")
                {
                    if (txtCertification.Text == string.Empty || string.IsNullOrWhiteSpace(txtCertification.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtCertificationError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtCertificationError");
                        valid = false;
                    }
                    if (txtCourseDates.Text == string.Empty || string.IsNullOrWhiteSpace(txtCourseDates.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtCourseDatesError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtCourseDatesError");
                        valid = false;
                    }
                    if (txtCourseFees.Text == string.Empty || string.IsNullOrWhiteSpace(txtCourseFees.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtCourseFeesError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtCourseFeesError");
                        valid = false;
                    }
                    if (txtCourseVenue.Text == string.Empty || string.IsNullOrWhiteSpace(txtCourseVenue.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtCourseVenueError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtCourseVenueError");
                        valid = false;
                    }
                    if (txtDurationOfStay.Text == string.Empty || string.IsNullOrWhiteSpace(txtDurationOfStay.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDurationOfStayError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDurationOfStayError");
                        valid = false;
                    }
                    if (txtEnterpriseBenefit.Text == string.Empty || string.IsNullOrWhiteSpace(txtEnterpriseBenefit.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtEnterpriseBenefitError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtEnterpriseBenefitError");
                        valid = false;
                    }
                    if (txtIndividualPlan.Text == string.Empty || string.IsNullOrWhiteSpace(txtIndividualPlan.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtIndividualPlanError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtIndividualPlanError");
                        valid = false;
                    }
                    if (txtTrainingPurpose.Text == string.Empty || string.IsNullOrWhiteSpace(txtTrainingPurpose.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtTrainingPurposeError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtTrainingPurposeError");
                        valid = false;
                    }
                    if (txtTransport.Text == string.Empty || string.IsNullOrWhiteSpace(txtTransport.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtTransportError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtTransportError");
                        valid = false;
                    }


                    if (txtDepartureDate.Text == string.Empty || string.IsNullOrWhiteSpace(txtDepartureDate.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDepartureDateError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDepartureDateError");
                        valid = false;
                    }

                    /*if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text)) && ((DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) <= DateTime.Today.AddDays(-1)))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess");
                        valid = false;
                    }*/

                    if (txtReturnDate.Text == string.Empty || string.IsNullOrWhiteSpace(txtReturnDate.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtReturnDateError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtReturnDateError");
                        valid = false;
                    }

                    /*if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text)) && ((DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) <= DateTime.Today.AddDays(-1)))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess");
                        valid = false;
                    }*/
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
            //try
            //{
                if (ValidateInputs())
                {
                    //DateTime date = DateTime.Parse(txtPlannedStartDate.Text);
                    //string _date = date.ToString("yyyy-MM-dd");
                    int TrainingEventID = Convert.ToInt32(Request["TrainingEventID"]);
                DateTime fromDate = (string.IsNullOrEmpty(txtDepartureDate.Text)) ? DateTime.Now : DateTime.ParseExact(txtDepartureDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                DateTime returnDate = (string.IsNullOrEmpty(txtReturnDate.Text)) ? DateTime.Now : DateTime.ParseExact(txtReturnDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);

                decimal trainingID = db.inserTraining(LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtJustificationWhy.Text, txtCourse.Text,
                    DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture), int.Parse(LstTrainingType.SelectedValue), int.Parse(LstPriority.SelectedValue), int.Parse(LstTrainingCategory.SelectedValue), utility.Login(loggedUser), TrainingEventID
                    , txtCourseFees.Text, txtCourseVenue.Text, txtVisaRequirement.Text, txtTransport.Text, txtCertification.Text, txtLegalRequirement.Text, fromDate
                    , returnDate, txtCourseDates.Text, txtDurationOfStay.Text, txtTrainingPurpose.Text, txtEnterpriseBenefit.Text, txtIndividualPlan.Text, Convert.ToBoolean(LstBudged.SelectedValue));

                    db.insertTrainingMotivation((int)trainingID, txtMotivationProduction.Text, txtMotivationEnvironment.Text, txtMotivationSafety.Text);

                    insertEmployee((int)trainingID);
                    insereFile((int)trainingID);

                    if (dgvContractor.Rows.Count > 0)
                    {
                        insertContractant((int)trainingID);
                    }

                    db.Sendmail_Training((int)trainingID);

                    lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    lblIDRequest.Text = trainingID.ToString();

                   
                   
                    ModalPopupExtender1.Show();
                    
                    //Label1.Text = CountEmployees().ToString();



                }
            //}
            //catch (EntityCommandExecutionException ex)
            //{
            //    string IDRequest = lblIDRequest.Text;
            //    string urlForRedirect = "Detail_trainingRequest.aspx?TrID=" + IDRequest;
            //    // Call the global exception handler in the master page
            //    ((NewRequest)this.Master).handleSendingMailException(ex, urlForRedirect, IDRequest);
            //}
            //catch (FormatException ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //}
            //catch (NullReferenceException ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //}
            //catch (IndexOutOfRangeException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //}
            //catch (ArithmeticException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //}

            //catch (TimeoutException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //}

            //catch (SystemException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //}

            //catch (Exception ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //}
        }

        public int CountSelectEmplyee() {
            int c = 0;
            foreach (GridViewRow row in dgvKCCEmployees.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                   
                    CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked == true)
                        c++;
                }
            }
            return c;
        }

        //public int CountSelectContractor()
        //{
        //    int c = 0;
        //    foreach (GridViewRow row in dgvContractor.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {

        //            CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);

        //            if (chkRow.Checked == true)
        //                c++;
        //        }
        //    }
        //    return c;
        //}


        public void insertEmployee(int trainingID)
        {
            int c = 0;
            foreach (GridViewRow row in dgvKCCEmployees.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);
                    Label employeeID= (row.FindControl("lblEmployeeID") as Label);
                    Label employeeName = (row.FindControl("lblUserName") as Label);
                    Label jobTitle = (row.FindControl("lblFunction") as Label);

                    if (chkRow.Checked == true) {
                        db.insertTrainingDetail(trainingID, employeeID.Text,employeeName.Text,jobTitle.Text,"","KCC","");
                    }
                        
                }
            }
          
        }


        public int CountEmployees()
        {
            int c = 0;
            foreach (GridViewRow row in dgvKCCEmployees.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);
                    Label employeeID = (row.FindControl("lblEmployeeID") as Label);

                    if (chkRow.Checked == true)
                    {
                        c = c + 1;
                        //db.insertTrainingDetail(trainingID, employeeID.Text);
                    }

                }
            }
            return c;
        }


        public void insertContractant(int trainingID)
        {
            int c = 0;
            foreach (GridViewRow row in dgvContractor.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                  
                    Label employeeID = (row.FindControl("lblEmployeeNumber") as Label);
                    Label employeeName = (row.FindControl("lblNames") as Label);
                    Label employeeSurname = (row.FindControl("lblSurname") as Label);
                    Label JobTitle = (row.FindControl("lblFunction") as Label);
                    Label NatureOfEmployee = (row.FindControl("lblNatureOfEmployee") as Label);
                    Label CompanyName = (row.FindControl("lblCompanyNameContractor") as Label);
                    Label Language = (row.FindControl("lblLanguage") as Label);
                    string contractorName = employeeName.Text + " " + employeeSurname.Text;

                  
                        db.insertTrainingDetail(trainingID, employeeID.Text,contractorName,JobTitle.Text,NatureOfEmployee.Text,CompanyName.Text,Language.Text);
                 

                }
            }

        }
        public void populateBuged()
        {

            ListItem itemYes = new ListItem((string)GetGlobalResourceObject("LocalizedText", "budgedYes"), "true");
            ListItem itemNo = new ListItem((string)GetGlobalResourceObject("LocalizedText", "budgedNon"), "false");
            LstBudged.Items.Insert(0, itemYes);
            LstBudged.Items.Insert(1, itemNo);
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail_trainingRequest.aspx?TrID=" + lblIDRequest.Text);
        
        }

        public void createtable()
        {

            tb.Columns.Add("EmployeeNumber", typeof(string));
            tb.Columns.Add("Names", typeof(string));
            tb.Columns.Add("surname", typeof(string));
            tb.Columns.Add("JobTitle", typeof(string));
            tb.Columns.Add("NatureOfEmployee", typeof(string));
            tb.Columns.Add("CompanyName", typeof(string));
            tb.Columns.Add("Language", typeof(string));

            dr = tb.NewRow();

            dgvContractor.DataSource = tb;
            dgvContractor.DataBind();

            ViewState["table1"] = tb;

        }
        //void PopulateTable()
        //{
        //    try
        //    {
        //        foreach (var item in db.Get_EWRequest_Estimation(id))
        //        {
        //            DataRow rw = tb.NewRow();
        //            rw["Description"] = item.Description;
        //            rw["Qte"] = item.Qte.Value.ToString();
        //            rw["UnitCost"] = item.UnitCost.Value.ToString();
        //            rw["UnitMesure"] = item.UnitMesure;
        //            rw["Total"] = item.Total.Value.ToString();
        //            tb.Rows.Add(rw);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}
        protected void Addnewrow()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmployeeNumber.Text) || (string.IsNullOrWhiteSpace(txtContractorNames.Text) && string.IsNullOrWhiteSpace(txtSurname.Text)) || string.IsNullOrWhiteSpace(txtContractorJobTitle.Text) || ddlContractorCompany.SelectedIndex <= 0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNewRowError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    tb = (DataTable)ViewState["table1"];
                    dr = tb.NewRow();
                    dr["EmployeeNumber"] = txtEmployeeNumber.Text.Replace(" ","");
                    dr["Names"] = txtContractorNames.Text;
                    dr["Surname"] = txtSurname.Text ;
                    dr["JobTitle"] = txtContractorJobTitle.Text;
                    dr["NatureOfEmployee"] =dllContratorNatureOfEmployee.SelectedValue;
                    dr["CompanyName"] = ddlContractorCompany.SelectedValue;
                    dr["Language"] = dlltxtContractorLanguage.SelectedValue;
                    tb.Rows.Add(dr);

                    dgvContractor.DataSource = tb;
                    dgvContractor.DataBind();
                    

                    ClearContractorFields();
                    if (ddlContractorEmployee.Items.Count > 0) ddlContractorEmployee.SelectedIndex = 0;
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

        // ===== Onglet Contractor : Company -> agents -> auto-remplissage =====
        private const string ContractorCacheKey = "KCC_ContractorList";

        private string GetDeveloperDbConnectionString()
        {
            // Chaine EF "developpers_dbEntities" -> chaine SQL native (suit le web.config Test/Prod)
            string efConn = ConfigurationManager.ConnectionStrings["developpers_dbEntities"].ConnectionString;
            return new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(efConn).ProviderConnectionString;
        }

        private DataTable ExecuteDeveloperSp(string spName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetDeveloperDbConnectionString()))
            using (SqlCommand cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // SP_Get_Employee_Contractor : aucun parametre, ~18 000 lignes -> cache 10 minutes
        private DataTable GetContractorList()
        {
            DataTable dt = HttpRuntime.Cache[ContractorCacheKey] as DataTable;
            if (dt == null)
            {
                dt = ExecuteDeveloperSp("dbo.SP_Get_Employee_Contractor");
                HttpRuntime.Cache.Insert(ContractorCacheKey, dt, null,
                    DateTime.Now.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return dt;
        }

        private static string Cell(DataRow row, string column)
        {
            if (!row.Table.Columns.Contains(column)) return string.Empty;
            return Convert.ToString(row[column]).Trim();
        }

        private string ResText(string key, string fallback)
        {
            string value = GetGlobalResourceObject("LocalizedText", key) as string;
            return string.IsNullOrEmpty(value) ? fallback : value;
        }

        private void ShowContractorError(string message)
        {
            lblErreur.Text = message;
            UpdatePanel10.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ContractorError", "openModal();", true);
        }

        // Etape 0 : liste des entreprises (SP_Get_Company, colonne DepartmentCode)
        private void PopulateContractorCompanies()
        {
            ddlContractorCompany.Items.Clear();
            ddlContractorCompany.Items.Add(new ListItem(ResText("selectdllContractor", "-- Select Company --"), ""));

            DataTable dt = ExecuteDeveloperSp("dbo.SP_Get_Company");
            if (!dt.Columns.Contains("DepartmentCode")) return;

            List<string> companies = new List<string>();
            foreach (DataRow r in dt.Rows)
            {
                string name = Cell(r, "DepartmentCode");
                if (name.Length == 0) continue;
                if (companies.Exists(x => string.Equals(x, name, StringComparison.OrdinalIgnoreCase))) continue;
                companies.Add(name);
            }
            companies.Sort(StringComparer.OrdinalIgnoreCase);

            foreach (string name in companies)
            {
                ddlContractorCompany.Items.Add(new ListItem(name, name));
            }
        }

        // Etape 1 : agents de l'entreprise choisie
        private void PopulateContractorEmployees(string company)
        {
            ddlContractorEmployee.Items.Clear();
            ddlContractorEmployee.Items.Add(new ListItem(ResText("selectdllContractor", "-- Select Contractor --"), ""));

            if (string.IsNullOrWhiteSpace(company)) return;

            List<ListItem> agents = new List<ListItem>();
            foreach (DataRow r in GetContractorList().Rows)
            {
                if (!string.Equals(Cell(r, "Company"), company.Trim(), StringComparison.OrdinalIgnoreCase)) continue;

                string code = Cell(r, "EmployeeCode");
                if (code.Length == 0) continue;

                string fullName = (Cell(r, "LastName") + " " + Cell(r, "FirstNames")).Trim();
                agents.Add(new ListItem(fullName + " (" + code + ")", code));
            }
            agents.Sort((x, y) => string.Compare(x.Text, y.Text, StringComparison.OrdinalIgnoreCase));
            ddlContractorEmployee.Items.AddRange(agents.ToArray());
        }

        protected void ddlContractorCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearContractorFields();
            try
            {
                PopulateContractorEmployees(ddlContractorCompany.SelectedValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Loading contractor employees failed: {0}", ex);
                ddlContractorEmployee.Items.Clear();
                ddlContractorEmployee.Items.Add(new ListItem(ResText("selectdllContractor", "-- Select Contractor --"), ""));
                ShowContractorError("Unable to load the contractors of this company. Please try again.");
            }
        }

        // Etape 2 : auto-remplissage a partir de l'agent choisi
        protected void ddlContractorEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearContractorFields();

            string code = ddlContractorEmployee.SelectedValue;
            if (string.IsNullOrWhiteSpace(code)) return;

            try
            {
                string company = ddlContractorCompany.SelectedValue.Trim();
                DataRow found = null;

                foreach (DataRow r in GetContractorList().Rows)
                {
                    if (string.Equals(Cell(r, "EmployeeCode"), code, StringComparison.OrdinalIgnoreCase)
                        && string.Equals(Cell(r, "Company"), company, StringComparison.OrdinalIgnoreCase))
                    {
                        found = r;
                        break;
                    }
                }

                if (found == null)
                {
                    ddlContractorEmployee.SelectedIndex = 0;
                    ShowContractorError("No contractor was found for employee number " + HttpUtility.HtmlEncode(code) + ".");
                    return;
                }

                txtEmployeeNumber.Text = Cell(found, "EmployeeCode");
                txtContractorNames.Text = Cell(found, "FirstNames");
                txtSurname.Text = Cell(found, "LastName");

                // La vue V_Employee_Contractor n'a pas (a notre connaissance) de colonne fonction :
                // si elle en a une on la reprend, sinon Job Title reste a saisir.
                foreach (string col in new[] { "JobTitle", "Function", "Position", "Title" })
                {
                    string job = Cell(found, col);
                    if (job.Length > 0) { txtContractorJobTitle.Text = job; break; }
                }
                // Nature of employee / Language : valeurs par defaut des listes (Contractor / English), modifiables.
            }
            catch (Exception ex)
            {
                ClearContractorFields();
                System.Diagnostics.Trace.TraceError("Contractor auto-fill failed for {0}: {1}", code, ex);
                ShowContractorError("Unable to retrieve the contractor information. Please try again.");
            }
        }

        // Vide les champs dependants (pas la liste Company)
        private void ClearContractorFields()
        {
            txtEmployeeNumber.Text = string.Empty;
            txtContractorNames.Text = string.Empty;
            txtSurname.Text = string.Empty;
            txtContractorJobTitle.Text = string.Empty;

            dllContratorNatureOfEmployee.ClearSelection();
            dlltxtContractorLanguage.ClearSelection();
            if (dllContratorNatureOfEmployee.Items.Count > 0) dllContratorNatureOfEmployee.SelectedIndex = 0;
            if (dlltxtContractorLanguage.Items.Count > 0) dlltxtContractorLanguage.SelectedIndex = 0;
        }

        protected void btnAddContractor_Click(object sender, EventArgs e)
        {
            Addnewrow();
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {

            try
            {
                Button btn = (Button)sender;


                foreach (GridViewRow row in dgvContractor.Rows)
                {
                    Button btn1 = (Button)row.FindControl("btndelete");

                    if (btn1.Equals(btn))
                    {
                        tb = (DataTable)ViewState["table1"];


                        int indexe = row.RowIndex;

                        tb.Rows.RemoveAt(indexe);
                        dgvContractor.DataSource = tb;
                        dgvContractor.DataBind();

                       

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


        public int RequestFiles()
        {
            int nbre = 0;
            HttpFileCollection fileCollection = Request.Files;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];
                string fileName = Path.GetFileName(uploadfile.FileName);
                string contentType = uploadfile.ContentType;
                if (uploadfile.ContentLength > 0)
                {
                    nbre += 1;
                }
            }
            return nbre;
        }
    }
}