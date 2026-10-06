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
    public partial class Detail_radiorequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                if (!IsPostBack)
                {

                    PopulatePriority();
                    PopulateCategory();
                    disableControl(false);
                    populateUsers();
                    populateRole();
                    populateUsersAdHoc();
                    btnApproval.Visible = false;
                    BtnReject.Visible = false;
                    BtnReturnToOrginator.Visible = false;
                    btnUpdateRadio.Visible = false;
                    btnBackToPreview.Visible = false;

                    if (Request.Params["DSKRrequestID"] != null)
                    {

                        int ID = int.Parse(Request.Params["DSKRrequestID"]);

                        var DskData = db.Get_DskRequestByID(ID, utility.Login(loggedUser));
                        title.InnerText = DskData.Id.ToString();
                        PopulateEmployeeAndDepartment(DskData.RequestorLogin);

                        LstRequestor.SelectedValue = DskData.RequestorLogin;
                        txtBeneficiary.Text = DskData.Beneficiary;
                        txtBeneficiaryJobTitle.Text = DskData.BeneficiaryJobTitle;
                        txtRequestDescription.Text = DskData.Description;
                        txtJustificationWhy.Text = DskData.Comment;
                        LstCategory.SelectedValue = DskData.CategoryID.ToString();
                        LstPriority.SelectedValue = DskData.PrioriteID.ToString();
                        txtPlannedStartDate.Text = DateTime.Parse(DskData.PlannedDeliveryDate.ToString()).ToString(format: "MM-dd-yyyy");

                        txtCurrentUser.Text = DskData.CurrentUser;
                        txtStatut.Text = DskData.StatusDescription;

                        dgvRadio.DataSource = db.Get_DskRequest_Detail(ID);
                        dgvRadio.DataBind();

                        dgvHistory.DataSource = db.Get_DskRequest_History(ID);
                        dgvHistory.DataBind();

                        dgvFiles.DataSource = db.Get_DskRequest_Files(ID);
                        dgvFiles.DataBind();

                        var TrainActions = db.Get_KCCRequest_Action(ID, utility.Login(loggedUser), "DSKRequest");

                        var requestToApprove = db.Get_KCCRequest_ToApprove("DSKRequest", utility.Login(loggedUser)).Where(x => (x.Id == ID)).FirstOrDefault();
                        if (TrainActions.Where(x => x.Action == "CanEdit").FirstOrDefault().Visibility >= 1)
                        {
                            btnUpdateRadio.Visible = true;
                            btnApproval.Visible = false;
                            BtnReject.Visible = true;
                            btnBackToPreview.Visible = false;
                            BtnReturnToOrginator.Visible = false;

                            disableControl(true);
                            btnCallBack.Visible = false;
                        }
                        if (TrainActions.Where(x => x.Action == "CanCallBack").FirstOrDefault().Visibility >= 1)
                        {
                            btnCallBack.Visible = true;

                        }
                        else btnCallBack.Visible = false;

                        if ((requestToApprove != null) && (!string.IsNullOrEmpty(requestToApprove.Module)))
                        {

                            if (DskData.StatusID < 4)
                            {
                                btnApproval.Visible = true;
                                btnUpdateRadio.Visible = false;
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = true;
                                btnBackToPreview.Visible = true;

                            }

                            if (DskData.StatusID == 15)
                            {

                                btnAdHoc.Visible = false;

                                btnApproval.Visible = true;
                                btnUpdateRadio.Visible = false;
                                BtnReject.Visible = false;
                                BtnReturnToOrginator.Visible = false;
                                btnBackToPreview.Visible = false;
                            btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "Adhocstatus");
                        }


                        }
                        else { btnAdHoc.Visible = false; }

                        if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "DSKRequest").FirstOrDefault().Number == 0)
                        {


                            btnrRealocate.Visible = false;
                            //btnAdHoc.Visible = false;
                        }
                        else if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "DSKRequest").FirstOrDefault().Number == 1)
                        {



                            btnUpdateRadio.Visible = true;
                            //btnAdHoc.Visible = false;
                            BtnReject.Visible = true;
                            disableControl(true);

                        }
                        if (DskData.StatusID == 10)
                        {
                            disableControl(false);
                            BtnReturnToOrginator.Visible = false;
                            btnBackToPreview.Visible = false;
                            BtnReject.Visible = false;
                            btnApproval.Visible = false;
                            btnUpdateRadio.Visible = false;
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
                            btnUpdateRadio.Visible = false;
                            btnAdHoc.Visible = false;
                            btnrRealocate.Visible = false;
                        }
                    }

                }
            //}
            //catch (Exception ex)
            //{

            //    //string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
            //    //lblErreur.Text = error;
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

            //    Response.Redirect("pageError.aspx");
            //}
        }


        public void PopulateEmployeeAndDepartment(string RequestLogin)
        {
            try
            {
                
                if (int.Parse(Developper.Employee.Get_Employee_Type(RequestLogin)) == 1)
                {
                    var listDepartement = Developper.Employee.Get_Employee_Of_Department(RequestLogin, "").ToList();
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

                    LstDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(RequestLogin, "").FirstOrDefault().DepartmentCode;

                    LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
                    LstRequestor.DataTextField = "FirstName";
                    LstRequestor.DataValueField = "Login";
                    LstRequestor.DataBind();
                    LstRequestor.Items.Insert(0, "Choose One");
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
                LstPriority.Items.Insert(0, "Choose One");
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
            dllUsers.Items.Insert(0, "Choose One");
        }

        public void populateUsersAdHoc()
        {

            dllUserAdHoc.DataSource = db.Get_Employee_User();
            dllUserAdHoc.DataTextField = "FirstName";
            dllUserAdHoc.DataValueField = "EmployeeId";
            dllUserAdHoc.DataBind();
            dllUserAdHoc.Items.Insert(0, "Choose One");
        }

        public void populateRole()
        {

            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "DSKRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, "Choose One");

        }
        public void PopulateCategory()
        {
            try
            {
                LstCategory.DataSource = db.Get_KCCRequest_Category(utility.Login(loggedUser));
                LstCategory.DataValueField = "CategoryID";
                LstCategory.DataTextField = "CategoryDescprition";
                LstCategory.DataBind();
                LstCategory.Items.Insert(0, "Choose One");
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidInputs())
                {

                    //db.Update_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), LstRequestor.SelectedValue, txtBeneficiary.Text, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtJustificationWhy.Text, (DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)), int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), utility.Login(loggedUser), utility.Login(loggedUser));
                    //Insert_DSKRequest_Files(int.Parse(Request.Params["DSKRrequestID"]));

                    ////Response.Redirect("Default_View_Approve.aspx");


                    //ModalPopupExtender1.Show();

                    

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

        protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, "Choose One");
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public void disableControl(bool statut)
        {

            LstRequestor.Enabled = statut;
            LstPriority.Enabled = statut;
            txtPlannedStartDate.Enabled = statut;
            txtRequestDescription.Enabled = statut;
            txtJustificationWhy.Enabled = statut;
            txtBeneficiaryJobTitle.Enabled = statut;
            LstCategory.Enabled = statut;
            txtBeneficiary.Enabled = statut;
            FileUpload2.Enabled = statut;
        }

        protected void btnview_Click(object sender, EventArgs e)
        {

            try
            {
                int id = int.Parse(Request.Params["DSKRrequestID"]);

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

                        byte[] bytes = (byte[])db.Get_DskRequest_Files(id).Where(x => x.FileID.ToString() == fileID.Text).FirstOrDefault().File.ToArray();

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
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- "+btnApproval.Text;
            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            lblaction.Text = "2";

            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- "+BtnReject.Text;

            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void BtnReturnToOrginator_Click(object sender, EventArgs e)
        {
            lblaction.Text = "3";
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- "+BtnReturnToOrginator.Text;

            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void btnBackToPreview_Click(object sender, EventArgs e)
        {
            lblaction.Text = "4";
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- "+btnBackToPreview.Text;

            Page.SetFocus(txtComment.ClientID);
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
                        db.Approve_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_DSKRequest_Files(int.Parse(Request.Params["DSKRrequestID"]));

                        //   int detailID, itemID, statut;
                        //  string docpost;
                        //  // detailID = db.Get_DskRequest_Detail(int.Parse(Request.Params["DSKRrequestID"])).FirstOrDefault().DetailID;
                        //   //itemID = (int)db.Get_DskRequest_Detail(int.Parse(Request.Params["DSKRrequestID"])).FirstOrDefault().ItemID;
                        //  docpost = db.Get_DskRequest_Detail(int.Parse(Request.Params["DSKRrequestID"])).FirstOrDefault().DocPos;
                        //   statut = (int)db.Get_DskRequest_Detail(int.Parse(Request.Params["DSKRrequestID"])).FirstOrDefault().StatusID;

                        // if (docpost == "DSK_Approver") {
                        //db.insert_DSKRequest_Sysaid(Request.Params["DSKRrequestID"], 1);
                        ////  db.Insert_ITProblem("ok", "Description", "Note", 1, 2, "itshibamb", null, 1, 1, "itshibamb",2, 1);
                        //  db.Approve_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), txtComment.Text, utility.Login(loggedUser));

                        //}
                        //   else
                        //   {

                        //   db.Approve_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), txtComment.Text, utility.Login(loggedUser));
                        //}



                    }
                    else if (lblaction.Text == "3")
                    {

                        db.Return_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_DSKRequest_Files(int.Parse(Request.Params["DSKRrequestID"]));
                    }
                    else if (lblaction.Text == "2")
                    {

                        db.Reject_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_DSKRequest_Files(int.Parse(Request.Params["DSKRrequestID"]));
                    }
                    else if (lblaction.Text == "4")
                    {

                        db.Back_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), txtComment.Text, utility.Login(loggedUser));
                        Insert_DSKRequest_Files(int.Parse(Request.Params["DSKRrequestID"]));
                    }
                    else if (lblaction.Text == "10") {

                        db.Update_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), LstRequestor.SelectedValue, txtBeneficiary.Text, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtJustificationWhy.Text, (DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)), int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), utility.Login(loggedUser), utility.Login(loggedUser));
                        Insert_DSKRequest_Files(int.Parse(Request.Params["DSKRrequestID"]));
                    }
                    //Response.Redirect("Default_View_Approve.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
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

        public void Insert_DSKRequest_Files(int ID)
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

            //if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text)) && ((DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) <= DateTime.Today.AddDays(-1)))
            //{
            //    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess");
            //    isValid = false;
            //}

           
            if (isValid == false)
            {
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            return isValid;
        }


        protected void dllUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string employedID = db.Get_Employee_User().Where(x => x.Login == dllUsers.SelectedValue).FirstOrDefault().EmployeeId;


            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "DSKRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, "Choose One");
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

                if (dllUsers.SelectedValue == "Choose One")
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllUsersError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }

                else if (dllRole.SelectedValue == "Choose One")
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
                    db.DSKRequest_Reallocate(int.Parse(Request.Params["DSKRrequestID"]), txtCommentReallocate.Text, dllRole.SelectedValue, utility.Login(loggedUser));


                    db.SendMail_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), null, null, null, null, null);

                    //Response.Redirect("Admin/DSK_Request/RequisitionDSK.aspx");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideReallocate();", true);
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

                if (dllUserAdHoc.SelectedValue == "Choose One")
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllUsersError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }


                else if (txtCommentAdHoc.Text == string.Empty)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    db.DSKRequest_AdHoc(int.Parse(Request.Params["DSKRrequestID"]), dllUserAdHoc.SelectedValue, txtCommentAdHoc.Text, utility.Login(loggedUser));
                    db.SendMail_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), null, null, null, null, null);

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


        protected void txtSysAid_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;


                foreach (GridViewRow row in dgvRadio.Rows)
                {
                    LinkButton btn1 = (LinkButton)row.FindControl("txtSysAid");

                    if (btn1.Equals(btn))
                    {

                        if (btn1.Text == "0")
                        {
                            lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNumberSysaidSelectedError");

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                        }
                        else
                        {
                            string sysaid = db.Get_KCCRequest_Parametre("ITProblem_Detail_Link") + "" + btn1.Text;


                            //Response.Redirect(sysaid + "" + btn1.Text);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "GoSysaidLink('" + sysaid + "');", true);

                            break;
                        }
                    }


                }
            }
            catch (Exception s)
            {


            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewReports.aspx?IDDSKRequest=" + Request.Params["DSKRrequestID"]);
        }

        protected void btnCallBack_Click(object sender, EventArgs e)
        {
            lblaction.Text = "4";
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnCallBack.Text;

            Page.SetFocus(txtComment.ClientID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            //Response.Redirect("default.aspx");

            Response.Redirect("My_RequestToAproval.aspx?type=DSKRequest");

        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
            
        }

        protected void btnUpdateRadio_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidInputs())
                {

                    //db.Update_DSKRequest(int.Parse(Request.Params["DSKRrequestID"]), LstRequestor.SelectedValue, txtBeneficiary.Text, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtJustificationWhy.Text, (DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)), int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), utility.Login(loggedUser), utility.Login(loggedUser));
                    //Insert_DSKRequest_Files(int.Parse(Request.Params["DSKRrequestID"]));

                    ////Response.Redirect("Default_View_Approve.aspx");


                    //ModalPopupExtender1.Show();

                    lblaction.Text = "10";

                    txtComment.Text = string.Empty;
                    this.Form.DefaultButton = this.btnSave.UniqueID;
                    lblTitre.Text = "- " + btnUpdateRadio.Text;

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
    }
}