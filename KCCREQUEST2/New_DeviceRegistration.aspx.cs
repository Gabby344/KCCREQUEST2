using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.Globalization;
using System.IO;
using System.Data.Entity.Core;
namespace KCCREQUEST2
{
    public partial class New_DeviceRegistration : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        private object Developper;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (db.Check_AccessDevice(utility.Login(loggedUser)) < 1)
                    {
                        Response.Redirect("pageAccesError.aspx");

                        //string error = (string)GetGlobalResourceObject("LocalizedText", "lblErrorDeviceRegistrationAccess");
                        //lblErreur.Text = error;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    }
                    else
                    {
                        PopulateDepartement();
                        PopulateAffected();
                        PopulateLocation();
                        PopulateWorkArround();
                        PopulateRequestor();

                        LstRequestedBy.SelectedValue = utility.Login(loggedUser);

                        LstRequestedBy_SelectedIndexChanged(sender, e);
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



        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {

               /* this.Page.MasterPageFile = "~/https://intranet.ktg.cd.glencore.net/_catalogs/masterpage/kcc_oslo_english.master";*/
               

            }
            catch (Exception ex)
            {

            }
        }




        public void PopulateDepartement()
        {
            try
            {
                LstDepartment.DataSource = db.Get_ITProblem_Department();
                LstDepartment.DataValueField = "value_key";
                LstDepartment.DataTextField = "value_caption";
                LstDepartment.DataBind();
                LstDepartment.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDepartement"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public void PopulateImpact()
        //{
        //    try
        //    {

        //        LstImpact.DataSource = db.Get_ITProblem_Impact();
        //        LstImpact.DataValueField = "value_key";
        //        LstImpact.DataTextField = "value_caption";
        //        LstImpact.DataBind();
        //        LstImpact.Items.Insert(0, "Choose One");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        public void PopulateAffected()
        {
            try
            {

                LstAffected.DataSource = db.Get_ITProblem_Affected();
                LstAffected.DataValueField = "value_key";
                LstAffected.DataTextField = "value_caption";
                LstAffected.DataBind();
                LstAffected.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAffected"));
                if (LstAffected.Items.Count >= 6)
                {
                    LstAffected.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void PopulateWorkArround()
        {
            try
            {

                LstWorkAround.DataSource = db.Get_ITProblem_WorkAround();
                LstWorkAround.DataValueField = "value_key";
                LstWorkAround.DataTextField = "value_caption";
                LstWorkAround.DataBind();
                LstWorkAround.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAffected"));
                if (LstWorkAround.Items.Count >= 4)
                {
                    LstWorkAround.SelectedIndex = 3;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateLocation()
        {
            try
            {
             
                LstLocation.DataSource = db.Get_DeviceRegistration_Location();
                LstLocation.DataValueField = "value_key";
                LstLocation.DataTextField = "value_caption";
                LstLocation.DataBind();
                LstLocation.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTrainingLocation"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //public void PopulateUrgency()
        //{
        //    try
        //    {
               
        //        LstUrgence.DataSource = db.Get_ITProblem_Urgency();
        //        LstUrgence.DataValueField = "value_key";
        //        LstUrgence.DataTextField = "value_caption";
        //        LstUrgence.DataBind();
        //        LstUrgence.Items.Insert(0, "Choose One");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        public void PopulateRequestor() {
            try
            {
                var listDepartement = new developpers_dbEntities().SP_Get_Employee().Where(x=>!string.IsNullOrEmpty(x.Mail)).ToList();//new developpers_dbEntities().SP_Get_Employee()
                LstRequestedBy.DataSource = listDepartement;
                LstRequestedBy.DataTextField = "FirstName";
                LstRequestedBy.DataValueField = "Login";
                LstRequestedBy.DataBind();
                LstRequestedBy.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
     
        public bool ValidInputs()
        {
            bool isValid = true;
            string error = "";
            string txtMacAddress = String.Format("{0}", Request["macAddress"]);
            if (txtSubject.Text==string.Empty || string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtSubjectEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "txtSubjectEmptyError");
                isValid = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (txtDescription.Text==string.Empty || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                //lblErreur.Text = "Beneficiary cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError");
                isValid = false;

            }
            if (txtNotes.Text==string.Empty || string.IsNullOrWhiteSpace(txtNotes.Text))
            {
                //lblErreur.Text = "Beneficiary job title cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                isValid = false;

            }
            //if (LstUrgence.SelectedValue== "Choose One")
            //{
            //    //lblErreur.Text = "Request Description cannot be empty";
            //    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstUrgenceEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "LstUrgenceEmptyError");
            //    isValid = false;

            //}
            //if (LstImpact.SelectedValue== "Choose One")
            //{
            //    //lblErreur.Text = "Justification cannot be empty";
            //    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstImpactEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstImpactEmptyError");
            //    isValid = false;

            if (LstAffected.SelectedIndex ==0)
            {
                //lblErreur.Text = "Request Description cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstAffectedEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "LstAffectedEmptyError");
                isValid = false;

            }
            if (LstWorkAround.SelectedIndex == 0)
            {
                //lblErreur.Text = "Justification cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstWorkAroundEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstWorkAroundEmptyError");
                isValid = false;

            }
            if (LstRequestedBy.SelectedIndex==0)
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError");
                isValid = false;
            }

            if (txtBeneficiary.Text == string.Empty || string.IsNullOrWhiteSpace(txtBeneficiary.Text))
            {
                //lblErreur.Text = "Beneficiary cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryEmptyError");
                isValid = false;

            }

            if (txtContactNumber.Text==string.Empty || string.IsNullOrWhiteSpace(txtContactNumber.Text))
            {
                //lblErreur.Text = "Priority cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtContactNumberEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtContactNumberEmptyError");
                isValid = false;

            }

            if (LstLocation.SelectedIndex ==0)
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstLocationEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstLocationEmptyError");
                isValid = false;
            }

            if (LstDepartment.SelectedIndex ==0)
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstDepartmentEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstDepartmentEmptyError");
                isValid = false;
            }
            if ((string.IsNullOrEmpty(txtMacAddress) || txtMacAddress.Length < 17 ))
            {
                
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtMacAddressError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtMacAddressError");
                isValid = false;
            }

            if (DeviceType.SelectedIndex == 0)
            {
                //lblErreur.Text = "Request Description cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstDeviceType") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstDeviceType");
                isValid = false;

            }


            if ((DeviceType.SelectedIndex == 1) && (string.IsNullOrEmpty(rooms.Text)))
            {
                //lblErreur.Text = "Request Description cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? "Room" : error + "<br />" + "Room";
                isValid = false;

            }


            if ((rbtnContractor.Checked) && (string.IsNullOrEmpty(LstCompanyName.Text)))
            {
                //lblErreur.Text = "Request Description cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? "Company Name" : error + "<br />" + "Company Name";
                isValid = false;

            }


            if (isValid == false)
            {
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            return isValid;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (rbtnGlencor.Checked)
            {
                txtBeneficiary.Text = LstRequestedBy.SelectedItem.ToString() ;

            }

            //if (rbtnContractor.Checked)
            //{
            //    string company = LstCompanyName.Text; 
            //}

            string Room = "";
            if (DeviceType.SelectedIndex.ToString() == "1")
            {
                Room = "\nRoom :" + rooms.Text;
                
            }
             

            string kccGroup = (rbtnContractor.Checked) ? "Contractor" : "Glencore staff/ICON";
            string txtMacAddress = String.Format("{0}", Request["macAddress"]);
            string txtDeviceType = DeviceType.SelectedValue;
            string description = "Device Request Information : " + txtDescription.Text + "\nRequested for : " + txtBeneficiary.Text + " - " + LstRequestedBy.SelectedValue + "\nJob Title : " + txtJobTitle.Text + "\nDevice Type : " + txtDeviceType + "\nMac Address : " + txtMacAddress+  "\nISE Description : " + txtBeneficiary.Text + " - " + LstRequestedBy.SelectedValue + " - "+ txtDeviceType+Room;
            if (kccGroup == "Glencore staff/ICON")
            {
                description = "Device Request Information : " + txtDescription.Text + "\nRequested for : " + txtBeneficiary.Text + " - " + LstRequestedBy.SelectedValue + "\nJob Title : " + txtJobTitle.Text + "\nDevice Type : " + txtDeviceType + "\nMac Address : " + txtMacAddress+ "\nISE Description : " + txtBeneficiary.Text + " - " + LstRequestedBy.SelectedValue + " - " + txtDeviceType + Room ;
                kccGroup = "Permanent KCC Staff or ICON";
            }
            else
            {
                description = "Device Request Information : " + txtDescription.Text + "\nRequested for : " + txtBeneficiary.Text + "\nJob Title : " + txtJobTitle.Text + "\nMac Address : " + txtMacAddress + "\nDevice Type : " + txtDeviceType + "\nISE Description : " + txtBeneficiary.Text + " - " + txtDeviceType + "\nCompany : " + LstCompanyName.Text + Room;
                kccGroup = "Permanent Contractors(eg.IFS or CSS)";
            }

            try
            {
                if (ValidInputs())
                {



                   decimal ID = db.Insert_ITProblem(kccGroup + " Device Registration of : " + txtSubject.Text, description , txtContactNumber.Text,txtSubject.Text,1,1, LstRequestedBy.SelectedValue, null, int.Parse(LstLocation.SelectedValue), int.Parse(LstDepartment.SelectedValue), utility.Login(loggedUser),int.Parse(LstAffected.SelectedValue),int.Parse(LstWorkAround.SelectedValue));

                    Insert_ITProblem_Files(ID);

                    lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    lblIDRequest.Text = ID.ToString();

                    ModalPopupExtender1.Show();

                }
            }
            catch (EntityCommandExecutionException ex)
            {
                string IDRequest = lblIDRequest.Text;
                string urlForRedirect = "Detail_ItProblem.aspx?IDITProblem=" + IDRequest;
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
            //string sysaid = db.Get_KCCRequest_Parametre("ITProblem_Detail_Link");

            //this.ClientScript.RegisterStartupScript(this.GetType(), "aa", "window.open('" + sysaid + "" + lblIDRequest.Text + "','_blank');", true);
            Response.Redirect("Detail_ItProblem.aspx?IDITProblem=" + lblIDRequest.Text);

            
        }


        protected void btnReset_Click(object sender, EventArgs e)
        {

            Response.Redirect("New_DeviceRegistration.aspx");

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openConfirm();", true);
        }


        public void Insert_ITProblem_Files(decimal ID)
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
                        db.Insert_ITProblem_File(Convert.ToInt32(ID), fileName, contentType, oneFile);

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

        protected void LstRequestedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //txtContactNumber.Text = string.Empty;

                var listDepartement = db.Get_Employee_ByID_Or_Login(LstRequestedBy.SelectedValue);
                string telephone = listDepartement.TelephoneNumberAD;
                //telephone = (string.IsNullOrEmpty(telephone)) ? listDepartement.FirstOrDefault().TelephoneNumber : " ";
                txtContactNumber.Text = telephone;
                txtJobTitle.Text = listDepartement.Jobtitle;
                txtBeneficiary.Text = listDepartement.FirstName;

                //LstDepartment.Items.Insert(0, listDepartement.Departement.ToString());
                //PopulateDepartement();
                LstDepartment.SelectedValue = listDepartement.Departement;
                

            }
            catch (Exception s) {

            }


        }

        protected void rbtn_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnContractor.Checked)
            {
                txtJobTitle.Text = string.Empty;
                LstDepartment.Enabled = true;
                txtContactNumber.Enabled = true;
                LstLocation.Enabled = true;
                txtBeneficiary.Text = string.Empty;

                txtBeneficiary.Enabled = true;
                txtBeneficiary.Visible = true;

                //LstRequestedBy.Enabled = true;
                LstRequestedBy.Visible = false;
                tdcompany.Visible = true;
                LstCompanyName.Text = "";


            }
            else
            {
                txtBeneficiary.Visible = false;
                txtBeneficiary.Enabled = false;

                //LstRequestedBy.Enabled = true;
                LstRequestedBy.Visible = true;

                LstLocation.Enabled = true;
                txtJobTitle.Enabled = true;
                txtContactNumber.Enabled = true;
                LstDepartment.Enabled = true;
                LstRequestedBy_SelectedIndexChanged(sender, e);
                tdcompany.Visible = false;
                LstCompanyName.Text = "KCC or ICON";

            }


            
        
        }


        protected void DeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DeviceTypes = DeviceType.SelectedIndex.ToString();
            if (DeviceTypes == "1")
            {
                dvRomm.Visible = true;
            }
            else { dvRomm.Visible = false; }



        }


        protected void LstAffected_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void CbGlencorestaff_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        protected void CbContractor_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ispermanent_CheckedChanged(object sender, EventArgs e)
        {
            if (ispermanent.Checked == true) { btnSave.Visible = true;  } else { btnSave.Visible = false; }
        }
    }
}