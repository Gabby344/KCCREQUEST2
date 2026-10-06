using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using KCCDBAccess;
using System.Globalization;
using System.Data.Entity.Core;
using KCCREQUEST2.form;

namespace KCCREQUEST2
{
    public partial class New_EWORequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        int Version = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                if (!Page.IsPostBack)
                {
                    PopulateEmployeeAndDepartment();
                    PopulateCategory();
                    PopulatePriority();
                    PopulateAMTSite();
                    if (Version == 2)
                    {
                        blocEqp.Visible = true;
                        PopulateWorkType();
                        if (LstWorkType.SelectedIndex != 0)
                        {
                            PopulateTask(int.Parse(LstWorkType.SelectedValue));
                        }
                    }
                    else blocEqp.Visible = false;
                }
            //}
            //catch (Exception ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

            //    Response.Redirect("pageError.aspx");
            //}
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

                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "EWRequest").FirstOrDefault().Number == 1)
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
                LstCategory.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllCategorie"));
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
        public void PopulateAMTSite()
        {
            try
            {
                LstSite.DataSource = Developper.Employee.Get_AMT_Site();
                LstSite.DataValueField = "SiteDescription";
                LstSite.DataTextField = "SiteDescription";
                LstSite.DataBind();
                LstSite.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAMTSite"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateAMTEquipment(string siteID, string fleetID)
        {
            try
            {
                LstEquipement.DataSource = Developper.Employee.Get_AMT_Equipment(siteID, fleetID);
                LstEquipement.DataValueField = "Equipment";
                LstEquipement.DataTextField = "Equipment";
                LstEquipement.DataBind();
                LstEquipement.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAMTEquipment"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateAMTFleet(string fleetID)
        {
            try
            {
                LstFleet.DataSource = Developper.Employee.Get_AMT_Fleet(fleetID);
                LstFleet.DataValueField = "Fleet";
                LstFleet.DataTextField = "Fleet";
                LstFleet.DataBind();
                LstFleet.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAMTEFleet"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateWorkType()
        {
            try
            {
                LstWorkType.DataSource = db.Get_EWRequest_WorkType();
                LstWorkType.DataValueField = "WorkTypeID";
                LstWorkType.DataTextField = "WorkTypeDescription";
                LstWorkType.DataBind();
                LstWorkType.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllWorkType"));
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void PopulateTask(int WorkTypeID)
        {
            try
            {
                if (LstWorkType.SelectedIndex != 0)
                {
                    LstTask.DataSource = db.Get_EWRequest_Task(WorkTypeID);
                    LstTask.DataValueField = "TaskId";
                    LstTask.DataTextField = "TaskDescription";
                    LstTask.DataBind();
                    LstTask.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTask"));
                }

            }
            catch (Exception ex)
            {

                throw;
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
                if (LstEquipement.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstEquipmentEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstEquipmentEmptyError");
                    valid = false;

                }
                if (LstFleet.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstFleetEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstFleetEmptyError");
                    valid = false;

                }
                if (LstSite.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstSiteEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstSiteEmptyError");
                    valid = false;
                }
                if (Version == 2) {

                    if (LstWorkType.SelectedIndex == 0)
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstWorkTypeEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstWorkTypeEmptyError");
                        valid = false;
                    }
                    if (LstTask.SelectedIndex == 0) {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstTaskEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstTaskEmptyError");
                        valid = false;
                    }
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
                if (string.IsNullOrEmpty(txtJustificationWhy.Text)|| string.IsNullOrWhiteSpace(txtJustificationWhy.Text))
                {
                    //
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                    valid = false;
                }
                if (string.IsNullOrEmpty(txtRequestDescription.Text)|| string.IsNullOrWhiteSpace(txtRequestDescription.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError");
                    valid = false;
                }
                if (string.IsNullOrEmpty(txtScopeOfWork.Text) || string.IsNullOrWhiteSpace(txtScopeOfWork.Text)) {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtScopeOfWorkEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtScopeOfWorkEmptyError");
                    valid = false;

                }

                if (string.IsNullOrEmpty(txtDesignedExisting.Text) || string.IsNullOrWhiteSpace(txtDesignedExisting.Text))
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDesignedExistingEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDesignedExistingEmptyError");
                    valid = false;

                }

                //DateTime dt = DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
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
        

        protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDepartement"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Insert_EWRequest_Files(int ID)
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
                        db.Insert_EWRequest_File(ID, fileName, contentType, oneFile);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_EWORequest.aspx");
        }

        protected void LstSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAMTFleet(LstSite.SelectedValue);
        }

        protected void LstFeet_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAMTEquipment(LstSite.SelectedValue, LstFleet.SelectedValue);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    int? ewrID=null;

                    if (Version == 1)
                    {
                        ewrID = Convert.ToInt32(db.Insert_EWRequest(LstRequestor.SelectedValue, txtRequestDescription.Text, txtDesignedExisting.Text, txtScopeOfWork.Text, txtJustificationWhy.Text, LstSite.SelectedValue, LstFleet.SelectedValue, LstEquipement.SelectedValue, int.Parse(LstPriority.SelectedValue),
                                    int.Parse(LstCategory.SelectedValue), utility.Login(loggedUser), DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)).Id.Value);
                    }
                    else if (Version == 2) {
                        ewrID = Convert.ToInt32(db.Insert_EWRequest_I(LstRequestor.SelectedValue, txtRequestDescription.Text, txtDesignedExisting.Text, txtScopeOfWork.Text, txtJustificationWhy.Text, LstSite.SelectedValue, LstFleet.SelectedValue, LstEquipement.SelectedValue, int.Parse(LstPriority.SelectedValue),
                                            int.Parse(LstCategory.SelectedValue), utility.Login(loggedUser), DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture),int.Parse(LstTask.SelectedValue)));
                    }

                    Insert_EWRequest_Files((int)ewrID);

                    lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    lblIDRequest.Text = ewrID.ToString();


                    ModalPopupExtender1.Show();

                  
                }
            }
            catch (EntityCommandExecutionException ex)
            {
                string IDRequest = lblIDRequest.Text;
                string urlForRedirect = "Detail_ewoRequest.aspx?EWRequestID=" + IDRequest;
                // Call the global exception handler in the master page
                ((NewRequest)this.Master).handleSendingMailException(ex, urlForRedirect, IDRequest);
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail_ewoRequest.aspx?EWRequestID=" + lblIDRequest.Text);

        }

        protected void LstWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstWorkType.SelectedIndex != 0)
            {
                PopulateTask(int.Parse(LstWorkType.SelectedValue));
            }
        }
    }
}