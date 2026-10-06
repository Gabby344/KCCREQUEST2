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
    public partial class New_DSKRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(@"https://glencorecopper.service-now.com/it?id=sc_category");
            //try
            //{
            //    if (!IsPostBack)
            //    {
            //        PopulateEmployeeAndDepartment();
            //        PopulatePriority();
            //        PopulateCategory();

            //        dgvRadio.DataSource = db.Get_DSKRequest_Items().Where(x => x.CategoryID == 3).ToList();
            //        dgvRadio.DataBind();

            //        dgvcctv.DataSource = db.Get_DSKRequest_Items().Where(x => x.CategoryID == 11).ToList();
            //        dgvcctv.DataBind();

            //    }
            //}
            //catch (Exception ex)
            //{

            //    //string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
            //    //lblErreur.Text = error;
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

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

                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "DSKRequest").FirstOrDefault().Number == 1)
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

        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvRadio.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);
               TextBox commentDetail= (row.FindControl("txtCommentDetail") as TextBox);

                if (chkRow.Equals(chk))
                {

                    commentDetail.Enabled = chkRow.Checked;
                    
                }
               
                else
                {
                    chkRow.Checked = false;
                    commentDetail.Enabled = chkRow.Checked;
                    commentDetail.Text = string.Empty;
                    DeselectdgCCTV();
                }
                if (chkRow.Checked == false){ commentDetail.Text = string.Empty; }
            }
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

            if (count_Check() == 0 && count_Checkcctv()==0)
            {

                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError1") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError1");
                isValid = false;
            }
            else if (count_Check() >= 1) {
                foreach (GridViewRow row in dgvRadio.Rows)
                {
                    CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);
                    TextBox commentDetail = (row.FindControl("txtCommentDetail") as TextBox);
                    Label iditem = (row.FindControl("lblItemID") as Label);
                    Label desc = (row.FindControl("lblItemDescription") as Label);

                    if (chkRow.Checked == true)
                    {
                        if (commentDetail.Text == string.Empty || string.IsNullOrWhiteSpace(commentDetail.Text)) {

                            error = (string.IsNullOrEmpty(error)) ?   desc.Text + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentPlannerEmptyError") : error + "<br /> " + desc.Text + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentPlannerEmptyError");
                            isValid = false;
                        }
                    }

                }
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
                    decimal ID = db.Insert_DSKRequest(LstRequestor.SelectedValue, txtBeneficiary.Text, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtJustificationWhy.Text, DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture), int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), utility.Login(loggedUser));

                    insertDetail_Radio(ID);
                    insertDetail_cctv(ID);
                    Insert_DSKRequest_Files(ID);
                    lblIDRequest.Text = ID.ToString();

                    db.SendMail_DSKRequest((int)ID, null, null, null, null, null);
                    lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    
                    ModalPopupExtender1.Show();

                }
            }
            catch (EntityCommandExecutionException ex)
            {
                string IDRequest = lblIDRequest.Text;
                string urlForRedirect = "Detail_radiorequest.aspx?DSKRrequestID=" + IDRequest;
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

        public void insertDetail_Radio(decimal id) {

            foreach (GridViewRow row in dgvRadio.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);
                

               
                if (chkRow.Checked) {

                    TextBox commentDetail = (row.FindControl("txtCommentDetail") as TextBox);
                    Label iditem = (row.FindControl("lblItemID") as Label);
                    db.insert_DSKRequest_Detail((int)id, int.Parse(iditem.Text), 0, 0, 1, commentDetail.Text);
                }
            }


        }

        public void insertDetail_cctv(decimal id)
        {

            foreach (GridViewRow row in dgvcctv.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowS") as CheckBox);



                if (chkRow.Checked)
                {

                    TextBox commentDetail = (row.FindControl("txtCommentDetail") as TextBox);
                    Label iditem = (row.FindControl("lblItemID") as Label);
                    db.insert_DSKRequest_Detail((int)id, int.Parse(iditem.Text), 0, 0, 1, commentDetail.Text);
                }
            }


        }

        public void Insert_DSKRequest_Files(decimal ID)
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
                          db.insert_DSKRequest_Files(Convert.ToInt32(ID), fileName, contentType, oneFile);

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


        public int count_Check() {

            int nbre = 0;
            foreach (GridViewRow row in dgvRadio.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);
                TextBox commentDetail = (row.FindControl("txtCommentDetail") as TextBox);
                Label iditem = (row.FindControl("lblItemID") as Label);


                if (chkRow.Checked == true)
                {
                    nbre += 1;
                }
         
        }

            return nbre;
    }

        public int count_Checkcctv()
        {

            int nbre = 0;
            foreach (GridViewRow row in dgvcctv.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowS") as CheckBox);
                TextBox commentDetail = (row.FindControl("txtCommentDetail") as TextBox);
                Label iditem = (row.FindControl("lblItemID") as Label);


                if (chkRow.Checked == true)
                {
                    nbre += 1;
                }

            }

            return nbre;
        }

        protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateEmployeeAndDepartment();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail_radiorequest.aspx?DSKRrequestID=" + lblIDRequest.Text);

        }

        protected void chkRowS_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvcctv.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowS") as CheckBox);
                TextBox commentDetail = (row.FindControl("txtCommentDetail") as TextBox);

                if (chkRow.Equals(chk))
                {

                    commentDetail.Enabled = chkRow.Checked;

                }

                else
                {
                    chkRow.Checked = false;
                    commentDetail.Enabled = chkRow.Checked;
                    commentDetail.Text = string.Empty;
                    DeselectdgRadio();
                }
                if (chkRow.Checked == false) { commentDetail.Text = string.Empty; }
            }
        }


        protected void DeselectdgRadio()
        {
            foreach (GridViewRow row in dgvRadio.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    TextBox txtBox = (row.Cells[0].FindControl("txtCommentDetail") as TextBox);
                    chkRow.Checked = false;
                    txtBox.Enabled = false;
                    txtBox.Text = string.Empty;
                }
            }
        }

        protected void DeselectdgCCTV()
        {
            foreach (GridViewRow row in dgvcctv.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRowS") as CheckBox);
                    TextBox txtBox = (row.Cells[0].FindControl("txtCommentDetail") as TextBox);

                    chkRow.Checked = false;
                    txtBox.Enabled = false;
                    txtBox.Text = string.Empty;
                }
            }
        }
    
    }
}