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
    public partial class New_ITProblem : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(@"https://glencorecopper.service-now.com/it?id=sc_cat_item&sys_id=07cea2b11bf7fd1066859642b24bcb1d");
            //try
            //{
            //    if (!IsPostBack)
            //    {

            //        PopulateDepartement();
            //        PopulateAffected();
            //        PopulateLocation();
            //        PopulateWorkArround();
            //        PopulateRequestor();

            //        LstRequestedBy.SelectedValue = utility.Login(loggedUser);

            //        LstRequestedBy_SelectedIndexChanged(sender, e);
            //    }
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
             
                LstLocation.DataSource = db.Get_ITProblem_Location();
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
            if (isValid == false)
            {
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            return isValid;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidInputs())
                {
                    decimal ID = db.Insert_ITProblem(txtSubject.Text, txtDescription.Text,txtContactNumber.Text,txtSubject.Text,1,1, LstRequestedBy.SelectedValue, null, int.Parse(LstLocation.SelectedValue), int.Parse(LstDepartment.SelectedValue), utility.Login(loggedUser),int.Parse(LstAffected.SelectedValue),int.Parse(LstWorkAround.SelectedValue));

                    Insert_ITProblem_Files(ID);

                    lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    lblIDRequest.Text = ID.ToString();

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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //string sysaid = db.Get_KCCRequest_Parametre("ITProblem_Detail_Link");

            //this.ClientScript.RegisterStartupScript(this.GetType(), "aa", "window.open('" + sysaid + "" + lblIDRequest.Text + "','_blank');", true);

            Response.Redirect("Detail_ItProblem.aspx?IDITProblem=" + lblIDRequest.Text);
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
                txtContactNumber.Text = string.Empty;
                var listDepartement = Developper.Employee.Get_Employee().Where(x => x.Sources == "VIP" && x.Login == LstRequestedBy.SelectedValue).ToList();
                string telephone = listDepartement.FirstOrDefault().TelephoneNumberAD;
                //telephone = (string.IsNullOrEmpty(telephone)) ? listDepartement.FirstOrDefault().TelephoneNumber : " ";
                txtContactNumber.Text = telephone;
            }
            catch (Exception s) {

            }


        }
        
    }
}