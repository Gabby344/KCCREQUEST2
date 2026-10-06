using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.IO;

namespace KCCREQUEST2.itrequest
{
    public partial class Detail_itRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;

        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnSave.UniqueID;
            if (!IsPostBack)
            {

                BindDDL();

                if (db.Get_KCCRequest_UserHold(utility.Login(loggedUser)) > 0) btnHold.Visible = true;
                else btnHold.Visible = false;



                if (Request.Params["IDITrequest"] != null)
                {

                    try
                    {
                        id = int.Parse(Request.Params["IDITrequest"]);
                        var head = db.Get_ITRequest_ByID(Convert.ToInt32(Request.Params["IDITrequest"]), utility.Login(loggedUser));

                        GetEmployee(head.RequestorLogin);
                        title.InnerText = head.Id.ToString();
                        dllDepartment.SelectedValue = head.Department;
                        LstRequestor.SelectedValue = head.RequestorLogin;
                        txtUrgencyComment.Text = head.CommentPriority;
                        txtBeneficiary.Text = head.Beneficiary;
                        txtBeneficiaryJobTitle.Text = head.BeneficiaryJobTitle;
                        txtRequestDescription.Text = head.Description;
                        txtJustificationWhy.Text = head.Comment;
                        //txtPlannedStartDate.Text = DateTime.Parse(head.PlannedDeliveryDate.ToString()).ToShortDateString();
                        //LstPriority.SelectedValue = head.PrioriteID.ToString();
                        //LstCategory.SelectedValue = head.CategoryID.ToString();
                        LstPriority.SelectedIndex = LstPriority.Items.IndexOf(LstPriority.Items.FindByValue(head.PrioriteID.ToString()));

                        dgvFiles.DataSource = db.Get_ITRequest_File(id);
                        dgvFiles.DataBind();
                        var itemp = db.Get_KCCRequest_Action(int.Parse(Request.Params["IDITrequest"].ToString()), utility.Login(loggedUser), "ITRequest");
                        if (itemp.Where(x => x.Action == "CanEdit").FirstOrDefault().Visibility >= 1)
                        {
                            ableControls();
                            btnApproval.Visible = false;
                            btnUpdate.Visible = true;
                            BtnReturnToOrginator.Visible = false;
                            btnCallBack.Visible = false;
                            btnBackToOriginator.Visible = false;

                        }

                        else
                        {
                            unableControls(); btnUpdate.Visible = false; btnApproval.Visible = true; BtnReturnToOrginator.Visible = true;

                        }



                        dgvDetails.DataSource = db.Get_ITRequest_ToApproveDetail(id, utility.Login(loggedUser));
                        dgvDetails.DataBind();

                        dgvAdHoc.DataSource = db.Get_ITRequest_ToApproveAdHoc(id, utility.Login(loggedUser));
                        dgvAdHoc.DataBind();

                        Visibility_AdHoc();

                        lblAdhoc.InnerText = db.Get_ITRequest_ToApproveAdHoc(id, utility.Login(loggedUser)).Count.ToString();


                        Visibility();



                        dgvHistory.DataSource = db.Get_ITRequest_History(id, utility.Login(loggedUser));
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


                        LstPriority_SelectedIndexChanged(sender, e);
                    }
                    catch (Exception ex)
                    {

                        //string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                        //lblErreur.Text = error;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

                        Response.Redirect("pageError.aspx");
                    }

                    if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "ITRequest").FirstOrDefault().Number == 0)
                    {


                        btnrRealocate.Visible = false;

                    }
                    else if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "ITRequest").FirstOrDefault().Number == 1)
                    {
                        btnrRealocate.Visible = false;
                        if (countItemsSelect() == 0)
                        {
                            btnApproval.Visible = false;
                            BtnReturnToOrginator.Visible = false;
                            btnBackToOriginator.Visible = false;
                        }
                        if (dgvDetails.Rows.Count > 0)
                        {
                            btnUpdate.Visible = true;
                            ableControls();
                            BtnReject.Visible = true;
                            btnrRealocate.Visible = true;
                        }


                    }

                    IsInSysaid();

                }

            }
        }

        protected void GetEmployee(string requestor)
        {


            if (int.Parse(Developper.Employee.Get_Employee_Type(requestor)) == 1)
            {
                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(requestor, "").ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));

                dllDepartment.DataSource = Developper.Employee.Get_Employee_Of_Department(requestor, "").GroupBy(p => p.DepartmentCode).Select(g => g.First());
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

                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department("", dllDepartment.SelectedValue).ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));

                dllDepartment.Enabled = true;

            }
            if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "ITRequest").FirstOrDefault().Number == 1)
            {

                dllDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                dllDepartment.DataTextField = "LongDescription";
                dllDepartment.DataValueField = "Code";
                dllDepartment.DataBind();
                dllDepartment.Enabled = true;

                dllDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(requestor, "").FirstOrDefault().DepartmentCode;

                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));
            }

        }

        public void unableControls()
        {
            LstRequestor.Enabled = false;
            txtBeneficiary.Enabled = false;
            txtBeneficiaryJobTitle.Enabled = false;
            txtRequestDescription.Enabled = false;
            txtJustificationWhy.Enabled = false;
            //txtPlannedStartDate.Enabled = false;
            LstPriority.Enabled = false;
            //LstCategory.Enabled = false;
            txtUrgencyComment.Enabled = false;

        }
        public void ableControls()
        {
            LstRequestor.Enabled = true;
            txtBeneficiary.Enabled = true;
            txtBeneficiaryJobTitle.Enabled = true;
            txtRequestDescription.Enabled = true;
            txtJustificationWhy.Enabled = true;
            //txtPlannedStartDate.Enabled = true;
            LstPriority.Enabled = true;
            //LstCategory.Enabled = true;
            txtUrgencyComment.Enabled = true;
        }
        public void BindDDL()
        {
            //LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(Login(), "").ToList();
            //LstRequestor.DataTextField = "FirstName";
            //LstRequestor.DataValueField = "Login";
            //LstRequestor.DataBind();
            //LstRequestor.Items.Insert(0, "Choose One");

            //LstCategory.DataSource = db.Get_KCCRequest_Category();
            //LstCategory.DataValueField = "CategoryID";
            //LstCategory.DataTextField = "CategoryDescprition";
            //LstCategory.DataBind();
            //LstCategory.Items.Insert(0, "Choose One");

            LstPriority.DataSource = db.Get_KCCRequest_Priority(utility.Login(loggedUser));
            LstPriority.DataValueField = "PrioriteID";
            LstPriority.DataTextField = "PrioriteDescprition";
            LstPriority.DataBind();
            LstPriority.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllpriority"));


            dllUsers.DataSource = db.Get_Employee_User();
            dllUsers.DataTextField = "FirstName";
            dllUsers.DataValueField = "Login";
            dllUsers.DataBind();
            dllUsers.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlluser"));

            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue);
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));

            dllUserAdHoc.DataSource = db.Get_Employee_User();
            dllUserAdHoc.DataTextField = "FirstName";
            dllUserAdHoc.DataValueField = "EmployeeId";
            dllUserAdHoc.DataBind();
            dllUserAdHoc.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlluser"));


        }

        protected void dllDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

            LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
            LstRequestor.DataTextField = "FirstName";
            LstRequestor.DataValueField = "Login";
            LstRequestor.DataBind();
            LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllpriority"));

        }

        public void Visibility()
        {
            int v;
            foreach (GridViewRow row in dgvDetails.Rows)
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
                        chkRow.Enabled = true; chkRow.Checked = true; row.Cells[0].BackColor = System.Drawing.Color.DarkBlue;
                        if (status == 11)
                        {
                            //quantite.Enabled = true;
                            //comment.ReadOnly = false;
                        }
                    }



                }

                if ((db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "ITRequest").FirstOrDefault().Number == 1) && v == 0)
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

        public void IsInSysaid()
        {
            int v; int c = 0;
            foreach (GridViewRow row in dgvDetails.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                v = int.Parse((row.Cells[2].FindControl("lblVisibility") as Label).Text);
                int status = int.Parse((row.Cells[2].FindControl("txtStatusID") as Label).Text);
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (status == 12 || status == 10)
                    {
                        c++;
                    }
                }
            }

            if (dgvDetails.Rows.Count == c)
            {

                btnApproval.Visible = false;
                btnUpdate.Visible = false;
                BtnReturnToOrginator.Visible = false;
                BtnReject.Visible = false;
                btnAdHoc.Visible = false;
                btnrRealocate.Visible = false;
                unableControls();
            }

        }

        public void Visibility_AdHoc()
        {
            int v; int ck = 0, ccb = 0; bool canCallBack; int detailID;
            foreach (GridViewRow row in dgvAdHoc.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                v = int.Parse((row.Cells[2].FindControl("lblVisibility") as Label).Text);
                detailID = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);

                if (db.Get_ITRequest_CanCallBack(detailID, utility.Login(loggedUser)).FirstOrDefault().Visibility == 1)
                {

                    canCallBack = true;

                }

                else
                {
                    canCallBack = false;

                }

                if (row.RowType == DataControlRowType.DataRow)
                {


                    int status = int.Parse((row.Cells[2].FindControl("txtStatusID") as Label).Text);
                    //TextBox quantite = (row.Cells[2].FindControl("txtQteItems") as TextBox);
                    //TextBox comment = (row.Cells[2].FindControl("txtCommentItem") as TextBox);
                    if (v == 0)
                    {
                        if (canCallBack == true)
                        {
                            chkRow.Enabled = true; chkRow.Checked = true;
                            ccb++;
                        }
                        else
                        {
                            chkRow.Enabled = false; chkRow.Checked = false;
                        }
                    }
                    else if (v == 1)
                    {
                        chkRow.Enabled = true; chkRow.Checked = true;

                        ck++;


                    }



                }


            }

            if (ck > 0)
            {
                btnApproverAdHoc.Visible = true;

            }

            else
            {
                btnApproverAdHoc.Visible = false;

            }

            if (ccb > 0)
            {
                btnCallBack.Visible = true;
            }
            else
            {
                btnCallBack.Visible = false;
            }


        }

        protected void Selectall_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvDetails.Rows)
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
            foreach (GridViewRow row in dgvDetails.Rows)
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
            foreach (GridViewRow row in dgvDetails.Rows)
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

        protected void dgvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    CheckBox chkRow = (e.Row.Cells[0].FindControl("chkRow") as CheckBox);

            //    if (e.Row.Cells[5].Text=="1")
            //    {

            //         chkRow.Checked = true;

            //    }

            //}
        }
        int action;
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            txtComment.Text = string.Empty;
            lblTitre.Text = btnApproval.Text;
            this.Form.DefaultButton = this.btnSave.UniqueID;

            if (dgvDetails.Rows.Count == 0)
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

        public int countItemsSelect()
        {
            int result = 0;
            foreach (GridViewRow row in dgvDetails.Rows)
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
        public int countItemsSelectAdHoc()
        {
            int result = 0;
            foreach (GridViewRow row in dgvAdHoc.Rows)
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
                        foreach (GridViewRow row in dgvDetails.Rows)
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
                                            db.Approve_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                            //
                                        }
                                        else if (lblaction.Text == "3")
                                        {
                                            db.Return_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                        }
                                        else if (lblaction.Text == "2")
                                        {
                                            db.Reject_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));

                                        }
                                        else if (lblaction.Text == "5")
                                        {
                                            db.Back_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                        }
                                        else if (lblaction.Text == "12")
                                        {

                                            db.Hold_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                        }
                                    }
                                    else if (lblaction.Text == "2")
                                    {
                                        db.Reject_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));

                                    }




                                }
                            }
                        }

                        insereFile(int.Parse(Request.Params["IDITrequest"]));

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                        ModalPopupExtender1.Show();
                    }
                    else if (lblaction.Text == "4")
                    {
                        foreach (GridViewRow row in dgvAdHoc.Rows)
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
                                        db.Approve_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
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
                        foreach (GridViewRow row in dgvAdHoc.Rows)
                        {


                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                                IDitrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);


                                if (chkRow.Checked == true)
                                {

                                    db.Back_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));

                                }
                            }
                        }

                        //Response.Redirect("Detail_itRequest.aspx");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                        ModalPopupExtender1.Show();
                    }

                    else if (lblaction.Text == "10")
                    {
                        db.Update_ITRequest(int.Parse(Request.Params["IDITrequest"]), txtBeneficiary.Text, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtJustificationWhy.Text, txtUrgencyComment.Text, new DateTime(), null, null,
                            null, null, null, null, int.Parse(LstPriority.SelectedValue), 0, txtComment.Text, utility.Login(loggedUser));

                        foreach (GridViewRow row in dgvDetails.Rows)
                        {

                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                                int itemsID = int.Parse((row.Cells[2].FindControl("lblIDitems") as Label).Text);
                                int Iditrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);
                                int status = int.Parse((row.Cells[2].FindControl("txtStatusID") as Label).Text);
                                Label quantite = (row.Cells[2].FindControl("txtQteItems") as Label);
                                Label comment = (row.Cells[2].FindControl("txtCommentItem") as Label);
                                if (chkRow.Checked == true)
                                {
                                    if (status == 11)
                                    {

                                        db.Update_ITRequest_Detail(Iditrequest, itemsID, int.Parse(quantite.Text), comment.Text, utility.Login(loggedUser));

                                    }

                                }

                            }
                        }

                        insereFile(int.Parse(Request.Params["IDITrequest"]));

                        db.Update_ITRequest_Status(int.Parse(Request.Params["IDITrequest"]));
                        db.SendMail_ITRequest(int.Parse(Request.Params["IDITrequest"]), null, null, null, null, null);

                        dgvDetails.DataSource = db.Get_ITRequest_ToApproveDetail(int.Parse(Request.Params["IDITrequest"]), utility.Login(loggedUser));
                        dgvDetails.DataBind();


                        dgvFiles.DataSource = db.Get_ITRequest_File(int.Parse(Request.Params["IDITrequest"]));
                        dgvFiles.DataBind();

                        Visibility();


                        ModalPopupExtender1.Show();

                        if (countItemsSelect() == 0)
                        {
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            BtnReturnToOrginator.Visible = false;
                            BtnReject.Visible = false;
                        }

                    }


                    db.Update_ITRequest_Status(int.Parse(Request.Params["IDITrequest"]));
                    db.SendMail_ITRequest(int.Parse(Request.Params["IDITrequest"]), null, null, null, null, null);
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
                    dgvDetails.DataSource = db.Get_ITRequest_ToApproveDetail(int.Parse(Request.Params["IDITrequest"]), utility.Login(loggedUser));
                    dgvDetails.DataBind();


                    Visibility();


                    if (countItemsSelect() == 0)
                    {
                        btnApproval.Visible = false;
                        btnUpdate.Visible = false;
                        BtnReturnToOrginator.Visible = false;
                        BtnReject.Visible = false;


                        Response.Redirect("My_RequestToAproval.aspx?type=ITRequest");

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

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = BtnReject.Text;
                if (dgvDetails.Rows.Count == 0)
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

        protected void btnrRealocate_Click(object sender, EventArgs e)
        {
            dllUsers.SelectedIndex = 0;
            dllRole.SelectedIndex = 0;
            txtCommentReallocate.Text = string.Empty;
            this.Form.DefaultButton = this.BtnSaveReallocate.UniqueID;
            if (dgvDetails.Rows.Count == 0)
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

        protected void btnAdHoc_Click(object sender, EventArgs e)
        {
            txtCommentAdHoc.Text = string.Empty;
            this.Form.DefaultButton = this.btnAdHocSav.UniqueID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openAdHoc();", true);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewReports.aspx?IDITrequest=" + Request.Params["IDITrequest"]);
        }
        protected void BtnReturnToOrginator_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = BtnReturnToOrginator.Text;
                if (dgvDetails.Rows.Count == 0)
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = btnUpdate.Text;

                int v;
                if (dgvDetails.Rows.Count == 0)
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

        protected void btnview_Click(object sender, EventArgs e)
        {

            try
            {
                int id = int.Parse(Request.Params["IDITrequest"]);

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

                        byte[] bytes = (byte[])db.Get_ITRequest_File(id).Where(x => x.FileID.ToString() == fileID.Text).FirstOrDefault().File.ToArray();

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

        protected void LstPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((LstPriority.SelectedItem.Text == "Very Urgent") || (LstPriority.SelectedItem.Text == "High"))
            {
                panelUrgencyComment.Visible = true;
            }
            else
            {
                panelUrgencyComment.Visible = false;
                txtUrgencyComment.Text = "";
            }
        }
        void ShowError(Exception s)
        {
            //lblErreur.Text = "An Error"
        }

       

        protected void dllUsers_SelectedIndexChanged(object sender, EventArgs e)
        {

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

                    foreach (GridViewRow row in dgvDetails.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                            int IDitrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);
                            if (chkRow.Checked == true)
                            {
                                db.ITRequest_Reallocate(IDitrequest, txtCommentReallocate.Text, dllRole.SelectedValue, utility.Login(loggedUser));
                            }
                        }
                    }
                    db.SendMail_ITRequest(int.Parse(Request.Params["IDITrequest"]), null, null, null, null, null);
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

                    foreach (GridViewRow row in dgvDetails.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                            int IDitrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);
                            if (chkRow.Checked == true)
                            {

                                db.ITRequest_AdHoc(IDitrequest, dllUserAdHoc.SelectedValue, txtCommentAdHoc.Text, utility.Login(loggedUser));
                            }
                        }
                    }
                    db.SendMail_ITRequest(int.Parse(Request.Params["IDITrequest"]), null, null, null, null, null);

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

      

        

        protected void txtSysAid_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;


                foreach (GridViewRow row in dgvDetails.Rows)
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


                            //Server.TransferRequest(sysaid + "" + btn1.Text);

                            //Response.Redirect(sysaid);
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

        protected void btnBackToOriginator_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = btnBackToOriginator.Text;
                if (dgvDetails.Rows.Count == 0)
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
                lblTitre.Text = btnCallBack.Text;
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
                Response.Redirect("Detail_itRequest.aspx?IDITrequest=" + title.InnerText);
            }

        }

        protected void btnHold_Click(object sender, EventArgs e)
        {
            try
            {
                txtComment.Text = string.Empty;
                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = btnHold.Text;
                if (dgvDetails.Rows.Count == 0)
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
                    db.Insert_ITRequest_File(id, fileName, contentType, uploadFile);
                }
            }
        }
    }
}