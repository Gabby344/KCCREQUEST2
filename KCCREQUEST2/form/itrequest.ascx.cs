using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using KCCDBAccess;

namespace KCCREQUEST2.form
{
    public partial class itrequest : System.Web.UI.UserControl
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility(); 
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        public string Login()
        {
            ADResultObject obj = utility.GetInformationFromADC(loggedUser).SingleOrDefault();

            return obj.Username;
        }

        DataTable tbitems = new DataTable();
        DataRow dr;

        public void createtable()
        {
            tbitems.Columns.Add("ItemsID", typeof(int));
            tbitems.Columns.Add("ItemsDescription", typeof(string));
            tbitems.Columns.Add("Quantite", typeof(int));
            tbitems.Columns.Add("Comment", typeof(string));

            dr = tbitems.NewRow();
            ViewState["table1"] = tbitems;

        }

        protected void Addnewrow(int itemsID, string itemsDescription, int quantite, string comment)
        {
            tbitems = (DataTable)ViewState["table1"];
            dr = tbitems.NewRow();
            dr["ItemsID"] = itemsID;
            dr["ItemsDescription"] = itemsDescription;
            dr["Quantite"] = quantite;
            dr["Comment"] = comment;
            tbitems.Rows.Add(dr);
        }


        public void addItemsComputer()
        {

            try
            {
                int itemsID; string itemsDescription; int quantite; string comment;
                foreach (GridViewRow row in dgvComputer.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRowComputer") as CheckBox);

                        if (chkRow.Checked)
                        {
                            itemsID = int.Parse((row.Cells[2].FindControl("lblIDComputer") as Label).Text);
                            itemsDescription = (row.Cells[2].FindControl("lblItemComputer") as Label).Text;
                            quantite = int.Parse((row.Cells[2].FindControl("txtQteComputer") as TextBox).Text);
                            comment = (row.Cells[2].FindControl("txtCommentComputer") as TextBox).Text;

                            Addnewrow(itemsID, itemsDescription, quantite, comment);
                        }

                    }

                }

            }
            catch (Exception s)
            {

            }

        }


        public void addItemsTelephone()
        {

            try
            {
                int itemsID; string itemsDescription; int quantite; string comment;
                foreach (GridViewRow row in dgvTelephone.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRowTelephone") as CheckBox);

                        if (chkRow.Checked)
                        {
                            itemsID = int.Parse((row.Cells[2].FindControl("txtIDTelephone") as Label).Text);
                            itemsDescription = (row.Cells[2].FindControl("txtItemTelephone") as Label).Text;
                            quantite = int.Parse((row.Cells[2].FindControl("txtQteTelephone") as TextBox).Text);
                            comment = (row.Cells[2].FindControl("txtCommentTelephone") as TextBox).Text;

                            Addnewrow(itemsID, itemsDescription, quantite, comment);
                        }

                    }

                }

            }
            catch (Exception s)
            {

            }

        }

        int itemsID; string itemsDescription; int quantite; string comment; string accountType;
        public void addItemsConnectivity()
        {

            try
            {

                foreach (GridViewRow row in dgvConnectivity.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label txtID = (row.Cells[0].FindControl("txtIDConnectivity") as Label);
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRowConnectivity") as CheckBox);

                        if (chkRow.Checked)
                        {
                            if (txtID.Text == "26")
                            {

                                if (rdNewStart.Checked == true) { accountType = "New Start"; }
                                else if (rdVisitor.Checked == true) { accountType = "Visitor"; }

                                comment = comment + "New User Account Type: " + accountType + ", Department: " + lstDepartment.SelectedValue + ", Employee N°: " + txtEmployeeNumber.Text + ", Phone Number: " + txtPhoneNumber.Text + ", Location: " + txtLocation.Text + ", Job Title: " + txtJobTitle.Text;
                            }
                            else
                            {

                                comment = (row.Cells[2].FindControl("txtCommentConnectivity") as TextBox).Text;

                            }

                            itemsID = int.Parse((row.Cells[2].FindControl("txtIDConnectivity") as Label).Text);
                            itemsDescription = (row.Cells[2].FindControl("txtItemConnectivity") as Label).Text;
                            quantite = int.Parse((row.Cells[2].FindControl("txtQteConnectivity") as TextBox).Text);

                            Addnewrow(itemsID, itemsDescription, quantite, comment);

                        }

                    }

                }

            }
            catch (Exception s)
            {

            }

        }

        public void addItemsSoftware()
        {

            try
            {
                int itemsID; string itemsDescription; int quantite; string comment;
                foreach (GridViewRow row in dgvSofware.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRowSoftware") as CheckBox);

                        if (chkRow.Checked)
                        {
                            itemsID = int.Parse((row.Cells[2].FindControl("txtIDSoftware") as Label).Text);
                            itemsDescription = (row.Cells[2].FindControl("txtItemSoftware") as Label).Text;
                            quantite = int.Parse((row.Cells[2].FindControl("txtQteSoftware") as TextBox).Text);
                            comment = (row.Cells[2].FindControl("txtCommentSoftware") as TextBox).Text;

                            Addnewrow(itemsID, itemsDescription, quantite, comment);
                        }

                    }

                }

            }
            catch (Exception s)
            {

            }

        }


        public void addItemsNetwork()
        {

            try
            {
                int itemsID; string itemsDescription; int quantite; string comment;
                foreach (GridViewRow row in dgvNetwork.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRowNetwork") as CheckBox);

                        if (chkRow.Checked)
                        {
                            itemsID = int.Parse((row.Cells[2].FindControl("txtIDNetwork") as Label).Text);
                            itemsDescription = (row.Cells[2].FindControl("txtItemNetwork") as Label).Text;
                            quantite = int.Parse((row.Cells[2].FindControl("txtQteNetwork") as TextBox).Text);
                            comment = (row.Cells[2].FindControl("txtCommentNetwork") as TextBox).Text;

                            Addnewrow(itemsID, itemsDescription, quantite, comment);
                        }

                    }

                }

            }
            catch (Exception s)
            {

            }

        }


        string commentApplication;
        public void addItemsApplication()
        {

            try
            {
                int itemsID; string itemsDescription; int quantite;
                foreach (GridViewRow row in dgvApplication.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRowApplication") as CheckBox);

                        if (chkRow.Checked)
                        {
                            itemsID = int.Parse((row.Cells[2].FindControl("txtIDApplication") as Label).Text);
                            itemsDescription = (row.Cells[2].FindControl("txtItemApplication") as Label).Text;
                            quantite = int.Parse((row.Cells[2].FindControl("txtQteApplication") as TextBox).Text);
                            //comment = (row.Cells[2].FindControl("txtCommentApplication") as TextBox).Text;
                            commentApplication = commentApplication + "Application Name: " + ddlApplicationName.Text + ", Application Responsible: " + ddlApplicationResponsible.Text + ", Application Description : " + txtApplicationDescription.Text + ", Application Reason: " + txtApplicationRaison.Text + ", Application Sponsor: " + ddlApplicationSponsor.Text;
                            Addnewrow(itemsID, itemsDescription, quantite, commentApplication);
                        }

                    }

                }

            }
            catch (Exception s)
            {

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (true)
            {
                createtable();
                GetEmployee();
                binddl();
                dgvComputer.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 1);
                dgvComputer.DataBind();

                dgvTelephone.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 2);
                dgvTelephone.DataBind();

                dgvConnectivity.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 4);
                dgvConnectivity.DataBind();

                dgvSofware.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 5);
                dgvSofware.DataBind();

                dgvNetwork.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 7);
                dgvNetwork.DataBind();

                dgvApplication.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 6);
                dgvApplication.DataBind();


            }
        }

        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chk = (CheckBox)sender;


            foreach (GridViewRow row in dgvComputer.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRowComputer") as CheckBox);
                if (chkRow.Equals(chk))
                {

                    TextBox txt = (row.Cells[0].FindControl("txtQteComputer") as TextBox);
                    TextBox txtComment = (row.Cells[0].FindControl("txtCommentComputer") as TextBox);

                    if (chkRow.Checked == true)
                    {
                        txt.Text = "1";

                    }
                    else
                    {
                        txt.Text = "0";
                        txtComment.Text = string.Empty;
                    }
                    txt.Enabled = chkRow.Checked;
                    txtComment.Enabled = chkRow.Checked;


                    break;
                }
            }
        }

        protected void dgvComputer_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }

        protected void GetEmployee()
        {
            string l = Login();

            if (int.Parse(Developper.Employee.Get_Employee_Type(l)) == 1)
            {
                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(l, "").ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, "N/A");

                dllDepartment.DataSource = Developper.Employee.Get_Employee_Of_Department(l, "").GroupBy(p => p.DepartmentCode).Select(g => g.First());
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
                LstRequestor.Items.Insert(0, "N/A");

                dllDepartment.Enabled = true;

            }



            ddlApplicationResponsible.DataSource = Developper.Employee.Get_Employee_Of_Department(l, "").ToList();
            ddlApplicationResponsible.DataTextField = "FirstName";
            ddlApplicationResponsible.DataValueField = "Login";
            ddlApplicationResponsible.DataBind();
            ddlApplicationResponsible.Items.Insert(0, "N/A");

            ddlApplicationSponsor.DataSource = Developper.Employee.Get_Employee_Of_Department(l, "").ToList();
            ddlApplicationSponsor.DataTextField = "FirstName";
            ddlApplicationSponsor.DataValueField = "Login";
            ddlApplicationSponsor.DataBind();
            ddlApplicationSponsor.Items.Insert(0, "N/A");

            ddlApplicationName.DataSource = db.Get_KCCRequest_Module(utility.Login(loggedUser));
            ddlApplicationName.DataTextField = "ModuleDescription";
            ddlApplicationName.DataValueField = "ModuleID";
            ddlApplicationName.DataBind();
            ddlApplicationName.Items.Insert(0, "N/A");



        }

        public void binddl()
        {
            LstCategory.DataSource = db.Get_KCCRequest_Category(utility.Login(loggedUser));
            LstCategory.DataValueField = "CategoryID";
            LstCategory.DataTextField = "CategoryDescprition";
            LstCategory.DataBind();
            LstCategory.Items.Insert(0, "N/A");

            LstPriority.DataSource = db.Get_KCCRequest_Priority(utility.Login(loggedUser));
            LstPriority.DataValueField = "PrioriteID";
            LstPriority.DataTextField = "PrioriteDescprition";
            LstPriority.DataBind();
            LstPriority.Items.Insert(0, "N/A");

            lstDepartment.DataSource = Developper.Employee.Get_Department();
            lstDepartment.DataTextField = "LongDescription";
            lstDepartment.DataValueField = "LongDescription";
            lstDepartment.DataBind();
            lstDepartment.SelectedValue = "Choose One";

            newUserAccount.Visible = false;
        }

        protected void dgvComputer_Init(object sender, EventArgs e)
        {


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                tbitems.Rows.Clear();
                int idItRequest;


                addItemsComputer();
                addItemsTelephone();
                addItemsConnectivity();
                addItemsSoftware();
                addItemsNetwork();
                addItemsApplication();

                if (invalidInput() == false)
                {

                }
                else
                {

                    idItRequest = (int)db.Insert_ITRequest(LstRequestor.SelectedValue, txtBeneficiary.Text, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtJustificationWhy.Text, "",
                    DateTime.Parse(txtPlannedStartDate.Text), null, null, null, null, null, null, int.Parse(LstPriority.SelectedValue),  Login()).Id;

                    insertItemItRequest(idItRequest);

                    insereFile(idItRequest);

                    db.SendMail_ITRequest(idItRequest, null, null, null, null, null);

                    Response.Redirect("Detail_itRequest.aspx?IDITrequest=" + idItRequest.ToString());
                }

            }
            catch (Exception s)
            {
                lblErreur.Text = s.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }


        public void insertItemItRequest(int ID)
        {

            foreach (DataRow dr in tbitems.Rows)
            {
                db.Insert_ITRequest_Detail(ID, int.Parse(dr["ItemsID"].ToString()), 1, 0, int.Parse(dr["Quantite"].ToString()), dr["Comment"].ToString());
            }
        }

        public bool invalidInput()
        {
            bool isValid = true;

            if (LstRequestor.SelectedIndex == 0)
            {
                lblErreur.Text = "Requestor cannot be empty";
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txtBeneficiary.Text == string.Empty)
            {
                lblErreur.Text = "Beneficiary cannot be empty";
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txtBeneficiaryJobTitle.Text == string.Empty)
            {
                lblErreur.Text = "Beneficiary job title cannot be empty";
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txtRequestDescription.Text == string.Empty)
            {
                lblErreur.Text = "Request Description cannot be empty";
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txtJustificationWhy.Text == string.Empty)
            {
                lblErreur.Text = "Justification cannot be empty";
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (LstCategory.SelectedIndex == 0)
            {
                lblErreur.Text = "Category cannot be empty";
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (LstPriority.SelectedIndex == 0)
            {
                lblErreur.Text = "Priority cannot be empty";
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txtPlannedStartDate.Text == string.Empty)
            {
                lblErreur.Text = "Planned Start Date cannot be empty";
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (tbitems.Rows.Count == 0)
            {
                lblErreur.Text = "No items select";
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            return isValid;
        }

        protected void chkRowTelephone_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvTelephone.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRowTelephone") as CheckBox);
                if (chkRow.Equals(chk))
                {

                    TextBox txt = (row.Cells[0].FindControl("txtQteTelephone") as TextBox);
                    TextBox txtComment = (row.Cells[0].FindControl("txtCommentTelephone") as TextBox);

                    if (chkRow.Checked == true) txt.Text = "1";
                    else
                    {
                        txt.Text = "0";
                        txtComment.Text = string.Empty;
                    }
                    txt.Enabled = chkRow.Checked;
                    txtComment.Enabled = chkRow.Checked;

                    break;
                }
            }
        }

        protected void chkRowConnectivity_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvConnectivity.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRowConnectivity") as CheckBox);
                if (chkRow.Equals(chk))
                {
                    Label txtID = (row.Cells[0].FindControl("txtIDConnectivity") as Label);
                    TextBox txt = (row.Cells[0].FindControl("txtQteConnectivity") as TextBox);
                    TextBox txtComment = (row.Cells[0].FindControl("txtCommentConnectivity") as TextBox);

                    if (chkRow.Checked == true)
                    {

                        if (txtID.Text == "26")
                        {
                            txtComment.Text = "New User A";
                            newUserAccount.Visible = true;
                            txt.Text = "1";
                        }
                        else
                        {

                            txt.Text = "1"; newUserAccount.Visible = false;
                        }


                    }
                    else
                    {


                        txt.Text = "0";
                        txtComment.Text = string.Empty;
                        newUserAccount.Visible = false;

                    }
                    txt.Enabled = chkRow.Checked;
                    txtComment.Enabled = chkRow.Checked;

                    break;
                }
            }
        }

        protected void chkRowSoftware_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvSofware.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRowSoftware") as CheckBox);
                if (chkRow.Equals(chk))
                {

                    TextBox txt = (row.Cells[0].FindControl("txtQteSoftware") as TextBox);
                    TextBox txtComment = (row.Cells[0].FindControl("txtCommentSoftware") as TextBox);

                    if (chkRow.Checked == true) txt.Text = "1";
                    else
                    {
                        txt.Text = "0";
                        txtComment.Text = string.Empty;
                    }
                    txt.Enabled = chkRow.Checked;
                    txtComment.Enabled = chkRow.Checked;

                    break;
                }
            }
        }

        protected void chkRowNetwork_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvNetwork.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRowNetwork") as CheckBox);
                if (chkRow.Equals(chk))
                {

                    TextBox txt = (row.Cells[0].FindControl("txtQteNetwork") as TextBox);
                    TextBox txtComment = (row.Cells[0].FindControl("txtCommentNetwork") as TextBox);

                    if (chkRow.Checked == true) txt.Text = "1";
                    else
                    {
                        txt.Text = "0";
                        txtComment.Text = string.Empty;
                    }
                    txt.Enabled = chkRow.Checked;
                    txtComment.Enabled = chkRow.Checked;

                    break;
                }
            }
        }

        protected void dllDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

            LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
            LstRequestor.DataTextField = "FirstName";
            LstRequestor.DataValueField = "Login";
            LstRequestor.DataBind();
            LstRequestor.Items.Insert(0, "N/A");

        }

        protected void chkRowApplication_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvApplication.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRowApplication") as CheckBox);
                if (chkRow.Equals(chk))
                {
                    Label txt = (row.Cells[0].FindControl("txtIDApplication") as Label);

                    Deselectall_Click(sender, e, txt.Text);

                    break;
                }
            }
        }

        protected void Deselectall_Click(object sender, EventArgs e, string id)
        {
            foreach (GridViewRow row in dgvApplication.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label txt = (row.Cells[0].FindControl("txtIDApplication") as Label);
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRowApplication") as CheckBox);

                    if (txt.Text != id)
                        chkRow.Checked = false;
                }
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("all_NewRequest.aspx");
        }
    }
}