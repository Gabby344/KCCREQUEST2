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
using Developper;

namespace KCCREQUEST2
{
    public partial class Detail_BonSortie : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        kcc_request_dbEntities dbo = new kcc_request_dbEntities();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //PopulatePriority();
                //populateTrainingType();
                populateService();
                populateTrainingCours();
                populateTrainingLocation();
                disableControl(false);
                populateUsers();
                populateUsersAdHoc();
                populateRole();
               // populateBuged();
                populateCompanyName();
                PopulateSites();
                PopulateEmployeeAndDepartment(utility.Login(loggedUser));








                //try
                //{
                    if (Request.Params["MCSRequestID"] != null)
                    {
                        int id = int.Parse(Request.Params["MCSRequestID"]);
                    //db.Get_MCRequest_BonEntree_Detail_By(id)

                    var Train = dbo.SP_Get_MCRequest_BonSortie_By (id.ToString()).ToList();

                    foreach (var i in Train) {

                        title.InnerText = i.Id.ToString();
                        PopulateEmployeeAndDepartment(i.Requestor.ToString()) ;
                        //PopulateEmployeeAndDepartment(i.Department.ToString());
                       
                        LstRequestor.SelectedValue = i.Requestor;
                        dllDepartment.SelectedValue = i.Department;
                        LstCompanyName.SelectedValue = i.IdCompany;
                        txtEmployerId.Text = i.EmployeeID;
                        LstSite.SelectedValue = i.SiteId.ToString();



                        txtreason.Text = i.Reason.ToString();


                        txtEscoter.Text = i.EscorteurName.ToString();
                        txtFunctionEscoter.Text = i.EscoteurFunction;
                        StartDate.Text = DateTime.Parse(i.DateSortie.ToString()).ToString(format: "MM-dd-yyyy");
                        
                    }


                    //dgvComputer.DataSource = db.Get_MCRequest_BonEntree_Detail_By(id.ToString());
                    //dgvComputer.DataBind();
                   




                    //dgvFiles.DataSource = db.Get_ITRequest_File(id);
                    //dgvFiles.DataBind();
                    var itemp = db.Get_KCCRequest_Action(int.Parse(Request.Params["MCSRequestID"].ToString()), utility.Login(loggedUser), "MCSRequest");
                    if (itemp.Where(x => x.Action == "CanEdit").FirstOrDefault().Visibility >= 1)
                    {
                        disableControl(true);
                        btnApproval.Visible = false;
                        btnUpdate.Visible = true;
                        BtnReturnToOrginator.Visible = false;
                        btnBackToOriginator.Visible = false;

                    }

                    else
                    {
                        disableControl(true);
                        btnUpdate.Visible = false; btnApproval.Visible = true; BtnReturnToOrginator.Visible = true;

                    }

                    

                    dgvItems.DataSource = dbo.SP_Get_MCRequest_ToApproveDetail_S(id, utility.Login(loggedUser));
                    dgvItems.DataBind();




                    Visibility();



                    dgvHistory.DataSource = dbo.SP_Get_MCRequest_History_S(id);
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
        protected void dllDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

            LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
            LstRequestor.DataTextField = "FirstName";
            LstRequestor.DataValueField = "Login";
            LstRequestor.DataBind();
            LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDepartement"));

        }
        public void disableControl(bool statut)
        {

            LstRequestor.Enabled = statut;
            dllDepartment.Enabled = statut;
           
            LstCompanyName.Enabled = statut;
            txtEmployerId.Enabled = statut;
            LstSite.Enabled = false;
            txtreason.Enabled = statut;
            txtEscoter.Enabled = statut;
            txtFunctionEscoter.Enabled = statut;
            StartDate.Enabled = statut;
           



        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               


                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                //lblTitre.Text = "- " + btnUpdate.Text;

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

            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "MCRequest");
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

                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "MCRequest").FirstOrDefault().Number == 1)
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
                LstSite.DataSource = db.Get_MCRequest_Sites().ToList();
                LstSite.DataTextField = "SiteName";
                LstSite.DataValueField = "Id";
                LstSite.DataBind();
                LstSite.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));

                //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                //dgvKCCEmployees.DataBind();


            }
            catch (Exception ex)
            {

                throw;
            }
        }

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


        protected void btnview_Click(object sender, EventArgs e)
        {

            try
            {
                int id = int.Parse(Request.Params["TrID"]);

                Button btn = (Button)sender;


                //foreach (GridViewRow row in dgvFiles.Rows)
                //{
                //    Button btn1 = (Button)row.FindControl("btnview");

                //    if (btn1.Equals(btn))
                //    {

                //        Label fileID = (Label)row.FindControl("txtFileID");
                //        Label txtType = (Label)row.FindControl("txtType");
                //        //Label txtFile = (Label)row.FindControl("File");
                //        Label filename = (Label)row.FindControl("txtFileName");

                //        byte[] bytes = (byte[])db.Get_TrainingRequest_File(id).Where(x => x.FileID.ToString() == fileID.Text).FirstOrDefault().File.ToArray();

                //        DownloadAttachment(txtType.Text, filename.Text, bytes);

                //        break;
                //    }



                //}
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

                                            dbo.SP_Approve_MCRequest_BonSortie_Detail (IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                            //
                                        }
                                        else if (lblaction.Text == "3")
                                        {
                                            dbo.SP_Return_MCRequest_Detail_S (IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                        }
                                        else if (lblaction.Text == "2")
                                        {
                                            dbo.SP_Reject_MCRequest_Detail_S(IDitrequest, txtComment.Text, utility.Login(loggedUser));

                                        }
                                        else if (lblaction.Text == "5")
                                        {
                                            dbo.SP_Back_MCRequest_S(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                        }
                                        else if (lblaction.Text == "12")
                                        {

                                            dbo.SP_Hold_MCRequest_S(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                        }
                                    }
                                    else if (lblaction.Text == "2")
                                    {
                                        dbo.SP_Reject_MCRequest_Detail_S(IDitrequest, txtComment.Text, utility.Login(loggedUser));

                                    }




                                }
                            }
                        }

                        insereFile(int.Parse(Request.Params["MCSRequestID"]));

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
                                IDitrequest = int.Parse((row.Cells[2].FindControl("lblIdBonSortie") as Label).Text);


                                if (chkRow.Checked == true)
                                {
                                    if (v == 1)
                                    {
                                        dbo.SP_Approve_MCRequest_BonSortie_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
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
                                IDitrequest = int.Parse((row.Cells[2].FindControl("lblIdBonSortie") as Label).Text);


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


                        dbo.SP_Update_MCRequest_BonSortie(Convert.ToInt32(Request.Params["MCSRequestID"]), LstCompanyName.SelectedValue, dllDepartment.SelectedValue, txtEmployerId.Text, txtEscoter.Text, txtFunctionEscoter.Text, Date1, LstRequestor.SelectedValue, loggedUser, DateTime.Now, 2, 0, 0, txtreason.Text);


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

                                        dbo.SP_Update_MCRequest_BonSortie_Detail(DetailId, DesignationProduit.Text, int.Parse(quantite.Text), txtComment.Text, utility.Login(loggedUser));
                                    }

                                }

                            }
                        }

                        insereFile(int.Parse(Request.Params["MCSRequestID"]));

                        //db.Update_ITRequest_Status(int.Parse(Request.Params["MCRequestID"]));
                        dbo.SP_SendMail_MCRequest_S(int.Parse(Request.Params["MCSRequestID"]), null, null, null, null, null);

                        dgvItems.DataSource = dbo.SP_Get_MCRequest_ToApproveDetail_S(int.Parse(Request.Params["MCSRequestID"]), utility.Login(loggedUser));
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


                    //db.Update_ITRequest_Status(int.Parse(Request.Params["MCSRequestID"]));
                    dbo.SP_SendMail_MCRequest_S(int.Parse(Request.Params["MCSRequestID"]), null, null, null, null, null);
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
                    dgvItems.DataSource = dbo.SP_Get_MCRequest_ToApproveDetail_S(int.Parse(Request.Params["MCSRequestID"]), utility.Login(loggedUser));
                    dgvItems.DataBind();


                    Visibility();


                    if (countItemsSelect() == 0)
                    {
                        btnApproval.Visible = false;
                        btnUpdate.Visible = false;
                        BtnReturnToOrginator.Visible = false;
                        BtnReject.Visible = false;


                        Response.Redirect("My_RequestToAproval.aspx?type=MCSRequest");

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
                    db.Approve_TrainingSec(int.Parse(Request.Params["TrID"]), txtCommentSec.Text, isbudget, int.Parse(lstServiceSec.SelectedValue), utility.Login(loggedUser));
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
                   DateTime.ParseExact(txtStartDateComment.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(txtEndDateComment.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture), getSelectDay(), periodicity, txtTrainingVenue.Text, txtCommentPlanner.Text, utility.Login(loggedUser));
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


            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "MCRequest");
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



                    db.TRNRequest_Reallocate(int.Parse(Request.Params["TrID"]), txtCommentReallocate.Text, dllRole.SelectedValue, utility.Login(loggedUser));

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

                    foreach (GridViewRow row in dgvItems.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                            int IDitrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);
                            if (chkRow.Checked == true)
                            {

                                dbo.SP_Ad_Hoc_MCRequest_S(IDitrequest, dllUserAdHoc.SelectedValue, txtCommentAdHoc.Text, utility.Login(loggedUser));
                            }
                        }
                    }
                  
              dbo.SP_SendMail_MCRequest_S(int.Parse(Request.Params["MCSRequestID"]), null, null, null, null, null);

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



        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewReports.aspx?IDTRNRequest=" + Request.Params["TrID"]);
        }


        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (lblaction.Text == "10")
            {
                Response.Redirect("Default_View_Approve.aspx");
            }
            else
            {
                Response.Redirect("Detail_BonSortie.aspx?MCSRequestID=" + title.InnerText);
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

        protected void btnHold_Click(object sender, EventArgs e)
        {
            txtComment.Text = string.Empty;
            this.Form.DefaultButton = this.btnSave.UniqueID;
            lblTitre.Text = "- " + btnHold.Text;

            Page.SetFocus(txtComment.ClientID);

            lblaction.Text = "5";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
        protected void btnSendToNationalDevelopment_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.Params["TrID"]);
            if (db.Check_Can_SendToNationalDevelopment(id, utility.Login(loggedUser)) > 0)
            {
                db.Send_TRNRequest_ToNationalDevelopment(id, "", utility.Login(loggedUser));

                Response.Redirect("My_RequestToAproval.aspx?type=MCSRequest");
            }
        }
    }



}
