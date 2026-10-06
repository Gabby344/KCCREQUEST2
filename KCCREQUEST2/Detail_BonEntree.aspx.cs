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
using System.Collections.Generic;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;

namespace KCCREQUEST2
{
    public partial class Detail_BonEntree : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        kcc_request_dbEntities dbo = new kcc_request_dbEntities();

        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;



        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSave.UniqueID;
            if (!IsPostBack)
            {

                //PopulatePriority();
                //populateTrainingType();
                populateService();
               // populateTrainingCours();
                //populateTrainingLocation();
                disableControl(false);
                populateUsers();
                populateUsersAdHoc();
                populateRole();
                // populateBuged();
                populateCompanyName();
                PopulateSites();
                PopulateSitesFrom();
                PopulateEmployeeAndDepartment(utility.Login(loggedUser));


                btnApproval.Visible = true;
                BtnReject.Visible = true;
                BtnReturnToOrginator.Visible = true;
                btnUpdate.Visible = true;

                btnHold.Visible = true;

                if (db.Get_KCCRequest_UserHold(utility.Login(loggedUser)) > 0) btnHold.Visible = true;
                else btnHold.Visible = false;



                if (Request.Params["MCRequestID"] != null)
                {

                    //try
                    //{
                        int id = int.Parse(Request.Params["MCRequestID"]);
                        var Train = db.Get_MCRequest_BonEntree_By(id.ToString()).ToList();

                        foreach (var i in Train)
                        {


                            PopulateEmployeeAndDepartment(i.Requestor.ToString());
                            //PopulateEmployeeAndDepartment(i.Department.ToString());
                            title.InnerText = i.Id.ToString();
                            LstRequestor.SelectedValue = i.Requestor;
                            dllDepartment.SelectedValue = i.Department;
                            LstCompanyName.SelectedValue = i.IdCompany;
                            txtContract.Text = i.ContractNumber;
                            txtContractorManager.Text = i.ContractorManager.ToString();
                             LstFrom.SelectedValue = i.FromSiteId.ToString();
                             LstTmTp.SelectedValue = i.IdSite.ToString();
                            txtreason.Text = i.Reason.ToString();
                            txtContractorEmail.Text = i.ContractorContact;
                            //txtRequestDescription.Text = i.des.ToString();
                            txtEscoter.Text = i.EscorteurName.ToString();
                            txtFunctionEscoter.Text = i.EscorteurFunction.ToString();
                            StartDate.Text = DateTime.Parse(i.DateFrom.ToString()).ToString(format: "MM-dd-yyyy");
                            EndDate.Text = DateTime.Parse(i.DateTo.ToString()).ToString(format: "MM-dd-yyyy");

                        }

                        //dgvFiles.DataSource = db.Get_ITRequest_File(id);
                        //dgvFiles.DataBind();
                        var itemp = db.Get_KCCRequest_Action(int.Parse(Request.Params["MCRequestID"].ToString()), utility.Login(loggedUser), "MCRequest");
                        if (itemp.Where(x => x.Action == "CanEdit").FirstOrDefault().Visibility >= 1)
                        {
                        disableControl(true) ;
                            btnApproval.Visible = false;
                            btnUpdate.Visible = true;
                            BtnReturnToOrginator.Visible = false;
                            btnBackToOriginator.Visible = false;

                        }

                        else
                    {
                        disableControl(false); 
                        btnUpdate.Visible = false; btnApproval.Visible = true; BtnReturnToOrginator.Visible = true;
                       

                        }



                        dgvItems.DataSource = db.Get_MCRequest_ToApproveDetail1(id, utility.Login(loggedUser));
                        dgvItems.DataBind();

                      


                        Visibility();



                        dgvHistory.DataSource = db.Get_MCRequest_History(id);
                        dgvHistory.DataBind();

                        if (countItemsSelect() == 0)
                        {
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            BtnReturnToOrginator.Visible = false;
                            BtnReject.Visible = false;
                            btnAdHoc.Visible = false;
                            btnBackToOriginator.Visible = false;
                        }


                    //}
                    //catch (Exception ex)
                    //{

                    //    //string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                    //    //lblErreur.Text = error;
                    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

                    //    Response.Redirect("pageError.aspx");
                    //}

                    if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "MCRequest").FirstOrDefault().Number == 0)
                    {


                        btnrRealocate.Visible = false;

                    }
                    else if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "MCRequest").FirstOrDefault().Number == 1)
                    {
                        btnrRealocate.Visible = false;
                        if (countItemsSelect() == 0)
                        {
                            btnApproval.Visible = false;
                            BtnReturnToOrginator.Visible = false;
                            btnBackToOriginator.Visible = false;
                        }
                        if (dgvItems.Rows.Count > 0)
                        {
                            btnUpdate.Visible = true;
                            BtnReject.Visible = true;
                            btnrRealocate.Visible = true;
                        }


                    }


                }

            }
        }













        public void disableControl(bool statut)
        {

            LstRequestor.Enabled = statut;
            dllDepartment.Enabled = statut;
            txtContract.Enabled = statut;
            LstCompanyName.Enabled = statut;
            txtContractorManager.Enabled = statut;
            //txtRequestDescription.Enabled = statut;
            txtContractorEmail.Enabled = statut;
            LstFrom.Enabled = statut;
            LstTmTp.Enabled = statut;
            txtreason.Enabled = statut;
            txtEscoter.Enabled = statut;
            txtFunctionEscoter.Enabled = statut;
            StartDate.Enabled = statut;
            EndDate.Enabled = statut;



        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = btnUpdate.Text;

                int v;
                if (dgvItems.Rows.Count == 0)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else if (countItemsSelect() < 1)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {

                    lblaction.Text = "10";
                    Page.SetFocus(txtComment.ClientID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                    //db.Update_ITRequest(int.Parse(Request.Params["IDITrequest"]), txtBeneficiary.Text, txtBeneficiaryJobTitle.Text,txtRequestDescription.Text, txtJustificationWhy.Text, txtUrgencyComment.Text, new DateTime(), null, null,
                    //    null, null, null, null, int.Parse(LstPriority.SelectedValue), 0);

                    //    foreach (GridViewRow row in dgvDetails.Rows)
                    //    {

                    //        if (row.RowType == DataControlRowType.DataRow)
                    //        {
                    //            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    //            int itemsID = int.Parse((row.Cells[2].FindControl("lblIDitems") as Label).Text);
                    //            int IDitrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);
                    //            int status = int.Parse((row.Cells[2].FindControl("txtStatusID") as Label).Text);
                    //            Label quantite = (row.Cells[2].FindControl("txtQteItems") as Label);
                    //            Label comment = (row.Cells[2].FindControl("txtCommentItem") as Label);
                    //            if (chkRow.Checked == true)
                    //            {
                    //                if (status == 11)
                    //                {

                    //                    db.Update_ITRequest_Detail(IDitrequest, itemsID, int.Parse(quantite.Text), comment.Text, utility.Login(loggedUser));

                    //                }

                    //            }

                    //        }
                    //    }

                    //    insereFile(int.Parse(Request.Params["IDITrequest"]));

                    //    db.Update_ITRequest_Status(int.Parse(Request.Params["IDITrequest"]));
                    //    db.SendMail_ITRequest(int.Parse(Request.Params["IDITrequest"]), null, null, null, null, null);

                    //    dgvDetails.DataSource = db.Get_ITRequest_ToApproveDetail(int.Parse(Request.Params["IDITrequest"]), utility.Login(loggedUser));
                    //    dgvDetails.DataBind();


                    //    dgvFiles.DataSource = db.Get_ITRequest_File(int.Parse(Request.Params["IDITrequest"]));
                    //    dgvFiles.DataBind();

                    //    Visibility();


                    //    ModalPopupExtender1.Show();

                    //    if (countItemsSelect() == 0)
                    //    {
                    //        btnApproval.Visible = false;
                    //        btnUpdate.Visible = false;
                    //        BtnReturnToOrginator.Visible = false;
                    //        BtnReject.Visible = false;
                    //    }
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



        protected void btnrRealocate_Click(object sender, EventArgs e)
        {
            dllUsers.SelectedIndex = 0;
            dllRole.SelectedIndex = 0;
            txtCommentReallocate.Text = string.Empty;
            this.Form.DefaultButton = this.BtnSaveReallocate.UniqueID;
            if (dgvItems.Rows.Count == 0)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            else if (countItemsSelect() < 1)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openReallocate();", true);

        }

       

        protected void dllRole_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                else if (txtCommentReallocate.Text == string.Empty)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {

                    foreach (GridViewRow row in dgvItems.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                            int IDitrequest = int.Parse((row.Cells[2].FindControl("lblIdBonEntree") as Label).Text);
                            if (chkRow.Checked == true)
                            {
                                db.ITRequest_Reallocate(IDitrequest, txtCommentReallocate.Text, dllRole.SelectedValue, utility.Login(loggedUser));
                            }
                        }
                    }
                    db.SendMail_MCRequest(int.Parse(Request.Params["MCRequestID"]), null, null, null, null, null);
                    //Response.Redirect("Admin/IT_Request/RequisitionitRequest.aspx");

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

        protected void dllUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string employedID = db.Get_Employee_User().Where(x => x.Login == dllUsers.SelectedValue).FirstOrDefault().EmployeeId;


            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue);
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));
            dllRole.Items.Insert(1, employedID);
        }

        protected void btnAdHoc_Click(object sender, EventArgs e)
        {
            dllUserAdHoc.SelectedIndex = 0;
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

                    foreach (GridViewRow row in dgvItems.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                            int IDitrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);
                            if (chkRow.Checked == true)
                            {

                                dbo.SP_Ad_Hoc_MCRequest(IDitrequest, dllUserAdHoc.SelectedValue, txtCommentAdHoc.Text, utility.Login(loggedUser));
                            }
                        }
                    }
                    db.SendMail_MCRequest(int.Parse(Request.Params["MCRequestID"]), null, null, null, null, null);

                    //Response.Redirect("Detail_itRequest.aspx?IDITrequest=" + Request.Params["IDITrequest"]);
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

        protected void btnApproverAdHoc_Click(object sender, EventArgs e)
        {

            if (countItemsSelectAdHoc() < 1)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            else
            {
                lblaction.Text = "4";
                Page.SetFocus(txtComment.ClientID);
                this.Form.DefaultButton = this.btnSave.UniqueID;
                //lblTitre.Text = btnApproverAdHoc.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewReports.aspx?MCRequestID=" + Request.Params["MCRequestID"]);
        }

        public int countItemsSelectAdHoc()
        {
            int result = 0;
            foreach (GridViewRow row in dgvItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked == true)
                    {

                        result += 1;
                    }

                }
            }
            return result;
        }
















        public void populateUsers()
        {

            dllUsers.DataSource = db.Get_Employee_User();
            dllUsers.DataTextField = "FirstName";
            dllUsers.DataValueField = "Login";
            dllUsers.DataBind();
            dllUsers.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlluser"));
        }
        //public void populateBuged()
        //{
        //    ListItem itemYes = new ListItem((string)GetGlobalResourceObject("LocalizedText", "budgedYes"), "true");
        //    ListItem itemNo = new ListItem((string)GetGlobalResourceObject("LocalizedText", "budgedNon"), "false");
        //    LstBudged.Items.Insert(0, itemYes);
        //    LstBudged.Items.Insert(1, itemNo);
        //}
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

                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRNRequest").FirstOrDefault().Number == 1)
                {

                    dllDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    dllDepartment.DataTextField = "LongDescription";
                    dllDepartment.DataValueField = "Code";
                    dllDepartment.DataBind();
                    dllDepartment.Enabled = true;

                    dllDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(requestor, "").FirstOrDefault().DepartmentCode;


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

       

       




        public void populateCompanyName()
        {

            try
            {
                LstCompanyName.DataSource = Developper.Employee.Get_Company();
                LstCompanyName.DataValueField = "DepartmentCode";
                LstCompanyName.DataTextField = "DepartmentCode";
                LstCompanyName.DataBind();
                LstCompanyName.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlltypeTraining"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void PopulateSites()
        {
            try
            {
                LstTmTp.DataSource = db.Get_MCRequest_Sites().ToList();
                LstTmTp.DataTextField = "SiteName";
                LstTmTp.DataValueField = "Id";
                LstTmTp.DataBind();
                LstTmTp.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));

                //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                //dgvKCCEmployees.DataBind();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void PopulateSitesFrom()
        {
            try
            {
                LstFrom.DataSource = db.Get_MCRequest_Sites().ToList();
                LstFrom.DataTextField = "SiteName";
                LstFrom.DataValueField = "Id";
                LstFrom.DataBind();
                LstFrom.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));

                //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                //dgvKCCEmployees.DataBind();


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        protected void LstFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            LstTmTp.DataSource = db.Get_MCRequest_Sites().Where(x => x.ID.ToString() != LstFrom.SelectedValue).ToList();
            LstTmTp.DataTextField = "SiteName";
            LstTmTp.DataValueField = "Id";
            LstTmTp.DataBind();
            //LstTmTp.Items.Insert(0, LstTmTp.SelectedItem.ToString());
            LstTmTp.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "lstSelectGeneric"));



        }


        protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
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

        int action;
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            txtComment.Text = string.Empty;
            lblTitre.Text = btnApproval.Text;
            this.Form.DefaultButton = this.btnSave.UniqueID;

            if (dgvItems.Rows.Count == 0)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            else if (countItemsSelect() < 1)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            else
            {

                lblaction.Text = "1";
                Page.SetFocus(txtComment.ClientID);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

        }
        protected void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = BtnReject.Text;
                if (dgvItems.Rows.Count == 0)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else if (countItemsSelect() < 1)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    lblaction.Text = "2";
                    Page.SetFocus(txtComment.ClientID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }





        public int countItemsSelect()
        {
            int result = 0;
            foreach (GridViewRow row in dgvItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked == true)
                    {

                        result += 1;
                    }

                }
            }
            return result;
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
            int IDitrequest; int v;

            if (txtComment.Text == string.Empty || string.IsNullOrWhiteSpace(txtComment.Text))
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            else
            {
                try
                {
                    if (lblaction.Text == "1" || lblaction.Text == "2" || lblaction.Text == "3" || lblaction.Text == "5" || lblaction.Text == "12")
                    {
                        foreach (GridViewRow row in dgvItems.Rows)
                        {
                            v = int.Parse((row.Cells[2].FindControl("lblVisibility") as Label).Text);

                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                                IDitrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);


                                if (chkRow.Checked == true)
                                {
                                    if (v == 1)
                                    {
                                        if (lblaction.Text == "1")
                                        {
                                            db.Approve_MCRequest_Bonentree_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                            //
                                        }
                                        else if (lblaction.Text == "3")
                                        {
                                            db.Return_MCRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                        }
                                        else if (lblaction.Text == "2")
                                        {
                                            db.Reject_MCRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));

                                        }
                                        else if (lblaction.Text == "5")
                                        {
                                            db.SP_Back_MCRequest(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                        }
                                        else if (lblaction.Text == "12")
                                        {

                                            db.Hold_MCRequest(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                        }
                                    }
                                    else if (lblaction.Text == "2")
                                    {
                                        db.Reject_MCRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));

                                    }




                                }
                            }
                        }

                        insereFile(int.Parse(Request.Params["MCRequestID"]));

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                        ModalPopupExtender1.Show();
                    }
                    else if (lblaction.Text == "4")
                    {
                        foreach (GridViewRow row in dgvItems.Rows)
                        {
                            v = int.Parse((row.Cells[2].FindControl("lblVisibility") as Label).Text);

                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                                IDitrequest = int.Parse((row.Cells[2].FindControl("lblIdBonEntree") as Label).Text);


                                if (chkRow.Checked == true)
                                {
                                    if (v == 1)
                                    {
                                        db.Approve_MCRequest_Bonentree_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                    }
                                }
                            }
                        }

                        //Response.Redirect("Detail_itRequest.aspx");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                        ModalPopupExtender1.Show();
                    }

                    else if (lblaction.Text == "6")
                    {
                        foreach (GridViewRow row in dgvItems.Rows)
                        {


                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                                IDitrequest = int.Parse((row.Cells[2].FindControl("lblIdBonEntree") as Label).Text);


                                if (chkRow.Checked == true)
                                {

                                    db.SP_Back_MCRequest(IDitrequest, txtComment.Text, utility.Login(loggedUser));

                                }
                            }
                        }

                        //Response.Redirect("Detail_itRequest.aspx");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                        ModalPopupExtender1.Show();
                    }

                    else if (lblaction.Text == "10")
                    {

                        DateTime Date1 = DateTime.ParseExact(StartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                        DateTime Date2 = DateTime.ParseExact(EndDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);

                        dbo.SP_Update_MCRequest_BonEntree(Convert.ToInt32(Request.Params["MCRequestID"]), txtContract.Text, LstCompanyName.SelectedValue,Convert.ToInt32( LstFrom.SelectedValue), Convert.ToInt32( LstTmTp.SelectedValue), dllDepartment.SelectedValue, txtreason.Text, txtContractorEmail.Text, txtContractorManager.Text, txtEscoter.Text, txtFunctionEscoter.Text, LstRequestor.SelectedValue, Date1, Date2,DateTime.Now, 0, 0, loggedUser, 1);


                        foreach (GridViewRow row in dgvItems.Rows)
                        {

                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                               

                                int DetailId = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);
                                int status = int.Parse((row.Cells[2].FindControl("lblStatus") as Label).Text);
                                Label quantite = (row.Cells[2].FindControl("lblQuantity") as Label);
                                //Label comment = (row.Cells[2].FindControl("txtCommentItem") as Label);
                                Label DesignationProduit = (row.Cells[2].FindControl("lblDesignation") as Label);
                                if (chkRow.Checked == true)
                                {
                                    if (status == 11)
                                    {

                                        dbo.SP_Update_MCRequest_BonEntree_Detail(DetailId, DesignationProduit.Text, int.Parse(quantite.Text), txtComment.Text, utility.Login(loggedUser)); ;

                                    }

                                }

                            }
                        }

                        insereFile(int.Parse(Request.Params["MCRequestID"]));

                        //db.Update_ITRequest_Status(int.Parse(Request.Params["MCRequestID"]));
                        db.SendMail_MCRequest(int.Parse(Request.Params["MCRequestID"]), null, null, null, null, null);

                        dgvItems.DataSource = db.Get_ITRequest_ToApproveDetail(int.Parse(Request.Params["MCRequestID"]), utility.Login(loggedUser));
                        dgvItems.DataBind();


                        

                        ModalPopupExtender1.Show();

                        if (countItemsSelect() == 0)
                        {
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            BtnReturnToOrginator.Visible = false;
                            BtnReject.Visible = false;
                        }

                    }


                    //db.Update_ITRequest_Status(int.Parse(Request.Params["MCRequestID"]));
                    db.SendMail_MCRequest(int.Parse(Request.Params["MCRequestID"]), null, null, null, null, null);
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

                txtComment.Text = string.Empty;
                try
                {
                    dgvItems.DataSource = db.Get_MCRequest_ToApproveDetail1(int.Parse(Request.Params["MCRequestID"]), utility.Login(loggedUser));
                    dgvItems.DataBind();


                    Visibility();


                    if (countItemsSelect() == 0)
                    {
                        btnApproval.Visible = false;
                        btnUpdate.Visible = false;
                        BtnReturnToOrginator.Visible = false;
                        BtnReject.Visible = false;


                        Response.Redirect("My_RequestToAproval.aspx?type=MCRequest");

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

                //Response.Redirect("Detail_itRequest.aspx?IDITrequest=" + Request.Params["IDITrequest"]);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
            }
        }

        protected void btnSaveSec_Click(object sender, EventArgs e)
        {
            try
            {
              

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
                    db.Approve_MCRequest_Bonentree_Detail(int.Parse(Request.Params["MCRequestID"]), txtCommentSec.Text, utility.Login(loggedUser));
                    insereFile(int.Parse(Request.Params["MCRequestID"]));
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






        public void Visibility()
        {
            int v;
            foreach (GridViewRow row in dgvItems.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                v = int.Parse((row.Cells[2].FindControl("lblVisibility") as Label).Text);
                int status = int.Parse((row.Cells[2].FindControl("txtStatusID") as Label).Text);
                //TextBox quantite = (row.Cells[2].FindControl("txtQteItems") as TextBox);
                //TextBox comment = (row.Cells[2].FindControl("txtCommentItem") as TextBox);
                if (row.RowType == DataControlRowType.DataRow)
                {





                    if (v == 0 || status == 12)
                    {
                        chkRow.Enabled = false;
                        chkRow.Checked = false;
                    }
                    else if (v == 1)
                    {
                        chkRow.Enabled = true; 
                        chkRow.Checked = true; 
                        row.Cells[0].BackColor = System.Drawing.Color.DarkBlue;
                        if (status == 11)
                        {
                            //quantite.Enabled = true;
                            //comment.ReadOnly = false;
                        }
                    }



                }

                if ((db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "MCRequest").FirstOrDefault().Number == 1) && v == 0)
                {
                    if (status != 10 && status != 12)
                    {
                        chkRow.Enabled = true; row.Cells[0].BackColor = System.Drawing.Color.DarkRed;
                        //quantite.Enabled = true;
                        //comment.ReadOnly = false;
                    }
                }


            }

        }
        protected void btnBackToOriginator_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = btnBackToOriginator.Text;
                if (dgvItems.Rows.Count == 0)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else if (countItemsSelect() < 1)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    lblaction.Text = "5";
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

        protected void btnHold_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = btnHold.Text;
                if (dgvItems.Rows.Count == 0)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else if (countItemsSelect() < 1)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    lblaction.Text = "12";
                    Page.SetFocus(txtComment.ClientID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
            //lblTitre.Text = "- " + btnBackToPreview.Text;

            Page.SetFocus(txtComment.ClientID);

            lblaction.Text = "4";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }



        protected void BtnReturnToOrginator_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = BtnReturnToOrginator.Text;
                if (dgvItems.Rows.Count == 0)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else if (countItemsSelect() < 1)
                {
                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    lblaction.Text = "3";
                    Page.SetFocus(txtComment.ClientID);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
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

        protected void btnCallBack_Click(object sender, EventArgs e)
        {
            if (countItemsSelectAdHoc() < 1)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNoItemSelectedError");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            else
            {
                lblaction.Text = "6";
                Page.SetFocus(txtComment.ClientID);
                this.Form.DefaultButton = this.btnSave.UniqueID;
                //lblTitre.Text = btnCallBack.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }





        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (lblaction.Text == "10")
            {
                Response.Redirect("Default_View_Approve.aspx");
            }
            else
            {
                Response.Redirect("Detail_BonEntree.aspx?MCRequestID=" + title.InnerText);
            }

        }

      
        //public void populateTrainingCalendar(int TrainingEventID)
        //{
        //    try
        //    {

        //        dgvTraniningCalendar.DataSource = db.Get_TrainingCalendar_ById(TrainingEventID).ToList();
        //        dgvTraniningCalendar.DataBind();

        //    }

        //    catch { }
        //}

        protected void Selectall_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Enabled == true)
                        chkRow.Checked = true;
                }
            }
        }

        protected void Deselectall_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Enabled == true)
                        chkRow.Checked = false;
                }
            }
        }

        protected void Inverse_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Enabled == true)
                    {

                        if (chkRow.Checked == true) chkRow.Checked = false;
                        else if (chkRow.Checked == false) chkRow.Checked = true;
                    }

                }
            }
        }

      
    }



}
