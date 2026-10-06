using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;

namespace KCCREQUEST2
{
    public partial class UC_DetailITRequest : System.Web.UI.UserControl
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;

        int id;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                BindDDL();
                if (Request.Params["IDITrequest"] != null)
                {
                    try
                    {
                        id = int.Parse(Request.Params["IDITrequest"]);
                        var head = db.Get_ITRequest_ByID(Convert.ToInt32(Request.Params["IDITrequest"]), utility.Login(loggedUser));


                        title.InnerText = head.Id.ToString();
                        LstRequestor.Items.Insert(0, head.RequestorName);
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
                        }
                        else
                        {
                            unableControls(); btnUpdate.Visible = false; btnApproval.Visible = true; BtnReturnToOrginator.Visible = true;
                        }


                        dgvDetails.DataSource = db.Get_ITRequest_ToApproveDetail(id, utility.Login(loggedUser));
                        dgvDetails.DataBind();

                        Visibility();

                        dgvHistory.DataSource = db.Get_ITRequest_History(id, utility.Login(loggedUser));
                        dgvHistory.DataBind();

                        if (countItemsSelect() == 0)
                        {
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            BtnReturnToOrginator.Visible = false;
                            BtnReject.Visible = false;
                        }

                        LstPriority_SelectedIndexChanged(sender, e);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }

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
            LstRequestor.Enabled = false;
            txtBeneficiary.Enabled = true;
            txtBeneficiaryJobTitle.Enabled = true;
            txtRequestDescription.Enabled = true;
            txtJustificationWhy.Enabled = true;
            //txtPlannedStartDate.Enabled = true;
            LstPriority.Enabled = true;
            //LstCategory.Enabled = true;
            txtUrgencyComment.Enabled = false;
        }
        public void BindDDL()
        {
            //LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(Login(), "").ToList();
            //LstRequestor.DataTextField = "FirstName";
            //LstRequestor.DataValueField = "Login";
            //LstRequestor.DataBind();
            //LstRequestor.Items.Insert(0, "N/A");

            //LstCategory.DataSource = db.Get_KCCRequest_Category();
            //LstCategory.DataValueField = "CategoryID";
            //LstCategory.DataTextField = "CategoryDescprition";
            //LstCategory.DataBind();
            //LstCategory.Items.Insert(0, "N/A");

            LstPriority.DataSource = db.Get_KCCRequest_Priority(utility.Login(loggedUser));
            LstPriority.DataValueField = "PrioriteID";
            LstPriority.DataTextField = "PrioriteDescprition";
            LstPriority.DataBind();
            LstPriority.Items.Insert(0, "N/A");

        }



        public void Visibility()
        {
            int v;
            foreach (GridViewRow row in dgvDetails.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    v = int.Parse((row.Cells[2].FindControl("lblVisibility") as Label).Text);
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    int status = int.Parse((row.Cells[2].FindControl("txtStatusID") as TextBox).Text);
                    TextBox quantite = (row.Cells[2].FindControl("txtQteItems") as TextBox);
                    TextBox comment = (row.Cells[2].FindControl("txtCommentItem") as TextBox);
                    if (v == 0)
                    {
                        chkRow.Enabled = false; chkRow.Checked = false;
                    }
                    else if (v == 1)
                    {
                        chkRow.Enabled = true; chkRow.Checked = true; row.Cells[0].BackColor = System.Drawing.Color.SteelBlue;
                        if (status == 11)
                        {
                            quantite.Enabled = true;
                            comment.ReadOnly = false;
                        }
                    }
                }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int IDitrequest;

            if (txtComment.Text == string.Empty)
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            else
            {
                try
                {
                    foreach (GridViewRow row in dgvDetails.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                            IDitrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);
                            if (chkRow.Checked == true)
                            {
                                if (lblaction.Text == "1")
                                {
                                    db.Approve_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                    //
                                }
                                else if (lblaction.Text == "2")
                                {
                                    db.Reject_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));

                                }
                                else if (lblaction.Text == "3")
                                {
                                    db.Return_ITRequest_Detail(IDitrequest, txtComment.Text, utility.Login(loggedUser));
                                }


                            }
                        }
                    }
                    db.Update_ITRequest_Status(int.Parse(Request.Params["IDITrequest"]));
                    db.SendMail_ITRequest(int.Parse(Request.Params["IDITrequest"]), null, null, null, null, null);
                }
                catch (Exception ex)
                {

                    throw;
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

                        Response.Redirect("My_RequestToAproval.aspx");

                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

                //Response.Redirect("Detail_itRequest.aspx?IDITrequest=" + Request.Params["IDITrequest"]);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
            }
        }

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {
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

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected void BtnReturnToOrginator_Click(object sender, EventArgs e)
        {
            try
            {
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
                    db.Update_ITRequest(int.Parse(Request.Params["IDITrequest"]), txtBeneficiary.Text, txtBeneficiaryJobTitle.Text, txtJustificationWhy.Text, txtComment.Text, txtUrgencyComment.Text, new DateTime(), null, null,
                    null, null, null, null, int.Parse(LstPriority.SelectedValue), 0,txtComment.Text, utility.Login(loggedUser));

                    foreach (GridViewRow row in dgvDetails.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                            int itemsID = int.Parse((row.Cells[2].FindControl("lblIDitems") as Label).Text);
                            int IDitrequest = int.Parse((row.Cells[2].FindControl("lblIDDetail") as Label).Text);
                            int status = int.Parse((row.Cells[2].FindControl("txtStatusID") as TextBox).Text);
                            TextBox quantite = (row.Cells[2].FindControl("txtQteItems") as TextBox);
                            TextBox comment = (row.Cells[2].FindControl("txtCommentItem") as TextBox);
                            if (chkRow.Checked == true)
                            {
                                if (status == 11)
                                {

                                    db.Update_ITRequest_Detail(IDitrequest, itemsID, int.Parse(quantite.Text), comment.Text, utility.Login(loggedUser));

                                }

                            }

                        }
                    }

                    db.Update_ITRequest_Status(int.Parse(Request.Params["IDITrequest"]));
                    db.SendMail_ITRequest(int.Parse(Request.Params["IDITrequest"]), null, null, null, null, null);

                    dgvDetails.DataSource = db.Get_ITRequest_ToApproveDetail(int.Parse(Request.Params["IDITrequest"]), utility.Login(loggedUser));
                    dgvDetails.DataBind();

                    Visibility();
                    if (countItemsSelect() == 0)
                    {
                        btnApproval.Visible = false;
                        btnUpdate.Visible = false;
                        BtnReturnToOrginator.Visible = false;
                        BtnReject.Visible = false;
                    }
                }
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
            catch (Exception s)
            {

                lblErreur.Text = s.ToString();
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
    }
}