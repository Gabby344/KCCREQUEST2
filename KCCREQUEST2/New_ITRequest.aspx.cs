using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using KCCDBAccess;
using System.Globalization;
using System.Drawing;
using System.Data.SqlClient;

namespace KCCREQUEST2
{
    public partial class New_ITRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        bool applicationChecked = false, newApp = false;

        DataTable tbitems = new DataTable();
        DataRow dr;
        int IsFileShare = 0;
   
        public void createtable()
        {
            tbitems.Columns.Add("ItemsID", typeof(int));
            tbitems.Columns.Add("ItemsDescription", typeof(string));
            tbitems.Columns.Add("Quantite", typeof(int));
            tbitems.Columns.Add("Comment", typeof(string));

            dr = tbitems.NewRow();
            ViewState["table1"] = tbitems;

        }

        protected void Addnewrow(int itemsID,string itemsDescription,int quantite,string comment)
        {
            tbitems = (DataTable)ViewState["table1"];
            dr = tbitems.NewRow();
            dr["ItemsID"] = itemsID;
            dr["ItemsDescription"] = itemsDescription;
            dr["Quantite"] = quantite;
            dr["Comment"] = comment;
            tbitems.Rows.Add(dr);
        }


        public void addItemsComputer() {

            try
            {
                int itemsID; string itemsDescription; int quantite; string comment;
                foreach (GridViewRow row in dgvComputer.Rows) {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.FindControl("chkRowComputer") as CheckBox);

                        if (chkRow.Checked) {
                            itemsID = int.Parse((row.FindControl("lblIDComputer") as Label).Text);
                            itemsDescription = (row.FindControl("lblItemComputer") as Label).Text;
                            quantite = int.Parse((row.FindControl("txtQteComputer") as TextBox).Text);
                            comment = (row.FindControl("txtCommentComputer") as TextBox).Text;

                            Addnewrow(itemsID, itemsDescription,quantite,comment);
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
                        CheckBox chkRow = (row.FindControl("chkRowTelephone") as CheckBox);

                        if (chkRow.Checked)
                        {
                            itemsID = int.Parse((row.FindControl("txtIDTelephone") as Label).Text);
                            itemsDescription = (row.FindControl("txtItemTelephone") as Label).Text;
                            quantite = int.Parse((row.FindControl("txtQteTelephone") as TextBox).Text);
                            comment = (row.FindControl("txtCommentTelephone") as TextBox).Text;

                            Addnewrow(itemsID, itemsDescription, quantite, comment);
                        }

                    }

                }

            }
            catch (Exception s)
            {
                throw;
            }

        }

        string commentFileShare;
        public void addItemsFileShare()
        {

            try
            {
                int itemsID; string itemsDescription; int quantite; 
                foreach (GridViewRow row in dgvFileShare.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.FindControl("chkRowFileShare") as CheckBox);

                        if (chkRow.Checked)
                        {
                            itemsID = int.Parse((row.FindControl("txtIDFileShare") as Label).Text);

                            if (itemsID == 59)
                            {
                                commentFileShare = (string)GetGlobalResourceObject("LocalizedText", "txtCommentNameOfTheFileShare")+" " + txtFileShareName.Text + " , "+ (string)GetGlobalResourceObject("LocalizedText", "txtCommentDataOwner")+" " + txtDataOwnerFileShare.Text;
                            }
                            else if (itemsID == 60) {
                                commentFileShare = (string)GetGlobalResourceObject("LocalizedText", "txtCommentUserName")+" " + txtUsernameFileShare.Text + " , "+ (string)GetGlobalResourceObject("LocalizedText", "txtCommentEmailAdresse") +" " + txtEmailAdresseFileShare.Text;
                            }
                            itemsDescription = (row.FindControl("txtItemFileShare") as Label).Text;
                            quantite = int.Parse((row.FindControl("txtQteFileShare") as TextBox).Text);
                            

                            Addnewrow(itemsID, itemsDescription, quantite, commentFileShare);
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
                        Label txtID = (row.FindControl("txtIDConnectivity") as Label);
                        CheckBox chkRow = (row.FindControl("chkRowConnectivity") as CheckBox);

                        if (chkRow.Checked)
                        {
                            if ((txtID.Text == "20") || (txtID.Text == "62"))
                            {
                               if(txtID.Text == "20")
                                { 

                                            if (EmployeeType.SelectedIndex.ToString() == "1")
                                            {
                                                accountType = "KCC/ICON";
                                                comment = comment + (string)GetGlobalResourceObject("LocalizedText", "txtCommentNewUserAccountType")+" " + EmployeeType.SelectedValue + ", " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentCompany")+" " + txtCompany.Text + ", "+ (string)GetGlobalResourceObject("LocalizedText", "txtCommentdepartment") + " " + lstDepartment.SelectedValue + ", " + (string)GetGlobalResourceObject("LocalizedText", "divUserAccountNameManager") + " " + txtNameManager.Text + ", " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentEmployeeNumber")+" " + txtEmployeeNumber.Text + ", "+ (string)GetGlobalResourceObject("LocalizedText", "txtCommentLocation")+" " + txtLocation.Text + ", "+ (string)GetGlobalResourceObject("LocalizedText", "txtCommentJobTitle")+" " + txtJobTitle.Text + ", "+ (string)GetGlobalResourceObject("LocalizedText", "txtCommentStartDate")+" " + inputStartDate.Value;
                                            }
                                            else if ((EmployeeType.SelectedIndex.ToString() == "2") ||  (EmployeeType.SelectedIndex.ToString() == "3"))
                                            {
                                                accountType = "NON KCC/ICON";

                                                comment = comment + (string)GetGlobalResourceObject("LocalizedText", "txtCommentNewUserAccountType")+" " + EmployeeType.SelectedValue + ", "+ (string)GetGlobalResourceObject("LocalizedText", "txtCommentCompany")+" " + txtCompany.Text + ", "+ (string)GetGlobalResourceObject("LocalizedText", "txtCommentEmailAdresse")+" " + txtEmailAdress.Text + " , "+ (string)GetGlobalResourceObject("LocalizedText", "txtCommentDurationofAccount")+" " + txtDurationAccount.Text + ", " + (string)GetGlobalResourceObject("LocalizedText", "divUserAccountNameManager") + " " + txtNameManager.Text;
                                            }
                                }

                                if (txtID.Text == "62")
                                {
                                    //comment = (row.FindControl("txtCommentConnectivity") as TextBox).Text;
                                    comment = "";
                                     if (EmployeeType.SelectedIndex.ToString() == "1")
                                        {
                                        accountType = "";
                                        comment = comment + (string)GetGlobalResourceObject("LocalizedText", "DivtextVisitor") + " :  " + EmployeeType.SelectedValue + " " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentCompany") + " " + txtCompany.Text + ", " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentdepartment") + " " + lstDepartment.SelectedValue + ", " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentEmployeeNumber") + " " + txtEmployeeNumber.Text + ", " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentLocation") + " " + txtLocation.Text + ", " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentJobTitle") + " " + txtJobTitle.Text + ", " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentStartDate") + " " + inputStartDate.Value;
                                    }
                                    else if ((EmployeeType.SelectedIndex.ToString() == "2") || (EmployeeType.SelectedIndex.ToString() == "3"))
                                    {
                                        accountType = "";
                                       
                                        comment = comment + (string)GetGlobalResourceObject("LocalizedText", "DivtextVisitor") + " : " + EmployeeType.SelectedValue + " " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentCompany") + " " + txtCompany.Text + ", " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentEmailAdresse") + " " + txtEmailAdress.Text + " , " + (string)GetGlobalResourceObject("LocalizedText", "divUserttxtFullName") + " " + txtDurationAccount.Text + ", " + (string)GetGlobalResourceObject("LocalizedText", "divUserAccountNameManager") + " " + txtNameManager.Text;
                                    }
                                }

                            }
                            else
                            {
                              
                                comment = (row.FindControl("txtCommentConnectivity") as TextBox).Text;
                              
                            }

                            itemsID = int.Parse((row.FindControl("txtIDConnectivity") as Label).Text);
                            itemsDescription = (row.FindControl("txtItemConnectivity") as Label).Text;
                            quantite = int.Parse((row.FindControl("txtQteConnectivity") as TextBox).Text);

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

                if (txtSoftware.SelectedIndex == 1)
                {
                    foreach (GridViewRow row in dgvSofware.Rows)
                    {

                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (row.FindControl("chkRowSoftware") as CheckBox);

                            if (chkRow.Checked)
                            {
                                itemsID = int.Parse((row.FindControl("txtIDSoftware") as Label).Text);
                                itemsDescription = (row.FindControl("txtItemSoftware") as Label).Text;
                                quantite = int.Parse((row.FindControl("txtQteSoftware") as TextBox).Text);
                                comment = (row.FindControl("txtCommentSoftware") as TextBox).Text;

                                Addnewrow(itemsID, itemsDescription, quantite, comment);
                            }

                        }

                    }

                }
                else if (txtSoftware.SelectedIndex == 2) {

                    Addnewrow(44,txtSoftware.SelectedValue,1,txtCommentNonStandardSoftware.Text);
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
                        CheckBox chkRow = (row.FindControl("chkRowNetwork") as CheckBox);

                        if (chkRow.Checked)
                        {
                            itemsID = int.Parse((row.FindControl("txtIDNetwork") as Label).Text);
                            itemsDescription = (row.FindControl("txtItemNetwork") as Label).Text;
                            quantite = int.Parse((row.FindControl("txtQteNetwork") as TextBox).Text);
                            comment = (row.FindControl("txtCommentNetwork") as TextBox).Text;

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
                        CheckBox chkRow = (row.FindControl("chkRowApplication") as CheckBox);

                        if (chkRow.Checked)
                        {
                            itemsID = int.Parse((row.FindControl("txtIDApplication") as Label).Text);
                            itemsDescription = (row.FindControl("txtItemApplication") as Label).Text;
                            quantite = int.Parse((row.FindControl("txtQteApplication") as TextBox).Text);
                            //comment = (row.FindControl("txtCommentApplication") as TextBox).Text;

                            if (ddlApplicationName.Enabled == true)
                            {
                                commentApplication = commentApplication + (string)GetGlobalResourceObject("LocalizedText", "txtCommentApplicationName") + " " + ddlApplicationName.Text + ",";
                            }
                            else {
                                commentApplication = commentApplication + (string)GetGlobalResourceObject("LocalizedText", "txtCommentApplicationName") + " " + " None " + ",";
                            }
                            if (ddlApplicationResponsible.Enabled == true)
                            {
                                commentApplication = commentApplication + (string)GetGlobalResourceObject("LocalizedText", "txtCommentApplicationResponsible") + " " + ddlApplicationResponsible.Text + ", ";
                            }
                            else {
                                commentApplication = commentApplication + (string)GetGlobalResourceObject("LocalizedText", "txtCommentApplicationResponsible") + " " + " None "+ ", ";
                            }
                            commentApplication = commentApplication + (string)GetGlobalResourceObject("LocalizedText", "txtCommentApplicationDescription") + " " + txtApplicationDescription.Text + ", " + (string)GetGlobalResourceObject("LocalizedText", "txtCommentApplicationReason") + " " + txtApplicationRaison.Text + ", ";
                            if (ddlApplicationSponsor.Enabled == true) {
                                commentApplication = commentApplication + (string)GetGlobalResourceObject("LocalizedText", "txtCommentApplicationSponsor") + " " + ddlApplicationSponsor.Text;
                            }
                            else {
                                commentApplication = commentApplication + (string)GetGlobalResourceObject("LocalizedText", "txtCommentApplicationSponsor") + " " + " None ";
                            }
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
            Response.Redirect(@"https://glencorecopper.service-now.com/it?id=sc_category");
            //try
            //{
            //    if (!IsPostBack)
            //    {

            //        createtable();
            //        GetEmployee();
            //        binddl();
            //        dgvComputer.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 1);
            //        dgvComputer.DataBind();

            //        dgvTelephone.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 2);
            //        dgvTelephone.DataBind();

            //        dgvConnectivity.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 4);
            //        dgvConnectivity.DataBind();

            //        dgvSofware.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 5 && x.ItemID != 22);
            //        dgvSofware.DataBind();

            //        dgvNetwork.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 7);
            //        dgvNetwork.DataBind();

            //        dgvApplication.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 6);
            //        dgvApplication.DataBind();

            //        dgvFileShare.DataSource = db.Get_ITRequest_Items(utility.Login(loggedUser)).Where(x => x.CategoryID == 10);
            //        dgvFileShare.DataBind();

            //        txtSoftware_SelectedIndexChanged(sender, e);



            //        GetEmployeeTypes();

            //        EmployeeType.Items.Insert(0, "Choose One"); /*(string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"))*/
            //        EmployeeType.Items.Insert(1, "KCC/ICON");
            //        EmployeeType.Items.Insert(2, "Visitor");
            //        EmployeeType.Items.Insert(3, "Contractor");


            //        forNonKcc.Visible = false;
            //        forKCC.Visible = false;


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

        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chk = (CheckBox)sender;
          
            
            foreach (GridViewRow row in dgvComputer.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowComputer") as CheckBox);
                if (chkRow.Equals(chk))
                {

                    TextBox txt = (row.FindControl("txtQteComputer") as TextBox);
                    TextBox txtComment = (row.FindControl("txtCommentComputer") as TextBox);

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
            string l = utility.Login(loggedUser);

            if (int.Parse(Developper.Employee.Get_Employee_Type(l)) == 1)
            {
                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(l, "").ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));

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

                dllDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(l, "").FirstOrDefault().DepartmentCode;

                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));
            }


            ddlApplicationResponsible.DataSource = Developper.Employee.Get_Employee_Of_Department(l, "").ToList();
                ddlApplicationResponsible.DataTextField = "FirstName";
                ddlApplicationResponsible.DataValueField = "Login";
                ddlApplicationResponsible.DataBind();
                ddlApplicationResponsible.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllApplicationResponsable"));

                ddlApplicationSponsor.DataSource = Developper.Employee.Get_Employee_Of_Department(l, "").ToList();
                ddlApplicationSponsor.DataTextField = "FirstName";
                ddlApplicationSponsor.DataValueField = "Login";
                ddlApplicationSponsor.DataBind();
                ddlApplicationSponsor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllApplicationSponsor"));

                ddlApplicationName.DataSource = db.Get_KCCRequest_Logiciel();
                ddlApplicationName.DataTextField = "LogicielDescription";
                ddlApplicationName.DataValueField = "LogicielDescription";
                ddlApplicationName.DataBind();
                ddlApplicationName.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllApplicationName"));



        }

        public void binddl() {
            //LstCategory.DataSource = db.Get_KCCRequest_Category();
            //LstCategory.DataValueField = "CategoryID";
            //LstCategory.DataTextField= "CategoryDescprition";
            //LstCategory.DataBind();
            //LstCategory.Items.Insert(0, "N/A");

            LstPriority.DataSource = db.Get_KCCRequest_Priority(utility.Login(loggedUser));
            LstPriority.DataValueField = "PrioriteID";
            LstPriority.DataTextField = "PrioriteDescprition";
            LstPriority.DataBind();
            LstPriority.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllpriority"));

            lstDepartment.DataSource = Developper.Employee.Get_Department().Where(x=>x.Code!="0000");
            lstDepartment.DataTextField = "LongDescription";
            lstDepartment.DataValueField = "LongDescription";
            lstDepartment.DataBind();
            lstDepartment.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDepartement"));


            newUserAccount.Visible = false;
        }

        protected void dgvComputer_Init(object sender, EventArgs e)
        {
           
               
        }
        int idItRequest;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //tbitems.Clear();
                //tbitems.Rows.Clear();
                tbitems = new DataTable();
                createtable();
               
               
                
                addItemsComputer();
                addItemsTelephone();
                addItemsConnectivity();
                addItemsSoftware();
                addItemsNetwork();
                addItemsApplication();
                addItemsFileShare();

                if (ValidInputs() == false)
                {

                }
                else
                {

                    int idItRequest = (int)db.Insert_ITRequest(LstRequestor.SelectedValue, txtBeneficiary.Text, txtBeneficiaryJobTitle.Text, txtRequestDescription.Text, txtJustificationWhy.Text, txtUrgencyComment.Text,
                    new DateTime(), null, null, null, null, null, null, int.Parse(LstPriority.SelectedValue), utility.Login(loggedUser)).Id;

                    insertItemItRequest(idItRequest);

                    insereFile(idItRequest);
                    db.Update_ITRequest_Status(idItRequest);

                    
                    db.SendMail_ITRequest(idItRequest, null, null, null, null, null);


                    lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    lblIDRequest.Text = idItRequest.ToString();
                   

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


        public void insertItemItRequest(int ID) {

            foreach (DataRow dr in tbitems.Rows)
            {
                db.Insert_ITRequest_Detail(ID, int.Parse(dr["ItemsID"].ToString()), 1, 0, int.Parse(dr["Quantite"].ToString()), dr["Comment"].ToString());
            }
        }

        public bool ValidInputs()
        {
            bool isValid = true;
            string error = "";
            string desc="";
            string com = "";
            if (LstRequestor.SelectedIndex == 0)
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError") : error + "<br />"+ (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError");
                isValid = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (txtBeneficiary.Text == string.Empty || string.IsNullOrWhiteSpace(txtBeneficiary.Text)) {
                //lblErreur.Text = "Beneficiary cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryEmptyError") : error + "<br /> "+ (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryEmptyError");
                isValid = false;

            }
            if (txtBeneficiaryJobTitle.Text == string.Empty || string.IsNullOrWhiteSpace(txtBeneficiaryJobTitle.Text)) {
                //lblErreur.Text = "Beneficiary job title cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryJobTitleEmptyError") : error + "<br />"+ (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryJobTitleEmptyError");
                isValid = false;
                
            }
            if (txtRequestDescription.Text == string.Empty || string.IsNullOrWhiteSpace(txtRequestDescription.Text)) {
                //lblErreur.Text = "Request Description cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError") : error + "<br />"+ (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError");
                isValid = false;
               
            }
            if (txtJustificationWhy.Text == string.Empty || string.IsNullOrWhiteSpace(txtJustificationWhy.Text)) {
                //lblErreur.Text = "Justification cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> "+ (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                isValid = false;
                
            }
            if ((panelUrgencyComment.Visible == true) && (string.IsNullOrEmpty(txtUrgencyComment.Text)) && ((int.Parse(LstPriority.SelectedValue)==1)||(int.Parse(LstPriority.SelectedValue) == 2)))
            {
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lblUrgencyCommentError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lblUrgencyCommentError");
                isValid = false;

            }
            if (LstPriority.SelectedIndex == 0)
            {
                //lblErreur.Text = "Priority cannot be empty";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "LstUrgenceEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "LstUrgenceEmptyError");
                isValid = false;

            }
            //if (txtPlannedStartDate.Text == string.Empty) {
            //    //lblErreur.Text = "Planned Delivery Date cannot be empty";
            //    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError") : error + "<br /> "+ (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError");
            //    isValid = false;

            //}
            ////DateTime dt = DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
            //if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text)) && ((DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) <= DateTime.Today.AddDays(-1)))
            //{
            //    //lblErreur.Text = "Planned Delivery Date cannot be empty can not be less than today's date";
            //    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess") : error + "<br /> "+ (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess");
            //    isValid = false;

            //}
            if (tbitems.Rows.Count == 0) {
                //lblErreur.Text = "No items selected";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError") : error + "<br /> "+ (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError");
                isValid = false;
                
            }
            if (tbitems.Rows.Count > 0)
            {
                foreach (DataRow rw in tbitems.Rows)
                {
                    string qt = rw["Quantite"].ToString();
                    com = rw["Comment"].ToString();
                   desc = rw["ItemsDescription"].ToString();
                    
                    if (string.IsNullOrEmpty(rw["Comment"].ToString()) || string.IsNullOrWhiteSpace(rw["Comment"].ToString()))
                    {
                        //error = (string.IsNullOrEmpty(error)) ? rw["ItemsDescription"].ToString() + " 's business justification is empty" : error + "<br />"+ rw["ItemsDescription"].ToString() + " 's business justification is empty";
                        error = (string.IsNullOrEmpty(error)) ? desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> " + desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                        isValid = false;
                    }
                    else if(rw["Comment"].ToString().Contains(((string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentTelephone")))) {
                        error = (string.IsNullOrEmpty(error)) ? desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> " + desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                        isValid = false;

                    }
                    else if (rw["Comment"].ToString().Contains(((string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentUSBaccess"))))
                    {
                        error = (string.IsNullOrEmpty(error)) ? desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> " + desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                        isValid = false;

                    }





                    int quantityOut = 0;
                    if ((!int.TryParse(rw["Quantite"].ToString(),out quantityOut)) && (quantityOut == 0))
                    {
                        //error = (string.IsNullOrEmpty(error)) ? rw["ItemsDescription"].ToString() + " 's quantity is not valid" : error + "<br />" + rw["ItemsDescription"].ToString() + " 's quantity is not valid";
                        error = (string.IsNullOrEmpty(error)) ? desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError") : error + "<br /> " + desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError");
                        isValid = false;
                    }
                }
            }
            if (newUserAccount.Visible == true)
            {


            

                if (EmployeeType.SelectedIndex == 0)
                {
                    //error = (string.IsNullOrEmpty(error)) ? "Please select Visitor or New Start" : error + "<br /> Please select Visitor or New Start";
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "rdVisitorNewStartEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "rdVisitorNewStartEmptyError");
                    isValid = false;
                }
                else if (EmployeeType.SelectedIndex == 1)
                {
                    if (string.IsNullOrEmpty(txtCompany.Text) || string.IsNullOrWhiteSpace(txtCompany.Text)) {

                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtCompanyEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtCompanyEmptyError");
                        isValid = false;
                    }
                    if (lstDepartment.SelectedIndex == 0)
                    {
                        //error = (string.IsNullOrEmpty(error)) ? "Please select New Account's Department" : error + "<br /> Please select New Account's Department";
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstDepartmentEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstDepartmentEmptyError");
                        isValid = false;
                    }

                    if (string.IsNullOrEmpty(txtNameManager.Text) || string.IsNullOrWhiteSpace(txtNameManager.Text))
                    {

                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtNameManagerEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtNameManagerEmptyError");
                        isValid = false;
                    }

                    if (((rdNewStart.Checked) && (string.IsNullOrEmpty(txtEmployeeNumber.Text))) || string.IsNullOrWhiteSpace(txtEmployeeNumber.Text))
                    {
                        //txtEmployeeNumberError
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtEmployeeNumberError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtEmployeeNumberError");
                        isValid = false;
                    }
                    if (string.IsNullOrEmpty(txtJobTitle.Text) || string.IsNullOrWhiteSpace(txtJobTitle.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryJobTitleEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtBeneficiaryJobTitleEmptyError");
                        isValid = false;
                    }

                    if (string.IsNullOrEmpty(txtLocation.Text) || string.IsNullOrWhiteSpace(txtLocation.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtLocationEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtLocationEmptyError");
                        isValid = false;
                    }

                    if (string.IsNullOrEmpty(inputStartDate.Value) || string.IsNullOrWhiteSpace(inputStartDate.Value)) 
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtStartDateEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtStartDateEmptyError");
                        isValid = false;
                    }
                }

                else if ((EmployeeType.SelectedIndex == 2)  || (EmployeeType.SelectedIndex == 3))
                {
                    if (string.IsNullOrEmpty(txtCompany.Text) || string.IsNullOrWhiteSpace(txtCompany.Text))
                    {

                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtCompanyEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtCompanyEmptyError");
                        isValid = false;
                    }
                    if (string.IsNullOrEmpty(txtEmailAdress.Text) || string.IsNullOrWhiteSpace(txtEmailAdress.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtEmailAdressEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtEmailAdressEmptyError");
                        isValid = false;
                    }

                    if (string.IsNullOrEmpty(txtDurationAccount.Text) || string.IsNullOrWhiteSpace(txtDurationAccount.Text))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDurationAccountEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDurationAccountEmptyError");
                        isValid = false;
                    }


                    if (string.IsNullOrEmpty(txtNameManager.Text) || string.IsNullOrWhiteSpace(txtNameManager.Text))
                    {

                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtNameManagerEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtNameManagerEmptyError");
                        isValid = false;
                    }
                }
            }
            if (applicationChecked)
            {
                if (ddlApplicationResponsible.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "ddlAppResponsibleError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "ddlAppResponsibleError");
                    isValid = false;
                }
                if (ddlApplicationName.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "ddlAppNameError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "ddlAppNameError");
                    isValid = false;
                }
                if (string.IsNullOrEmpty(txtApplicationDescription.Text) || string.IsNullOrWhiteSpace(txtApplicationDescription.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtAppDescriptionError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtAppDescriptionError");
                    isValid = false;
                }
                if (string.IsNullOrEmpty(txtApplicationRaison.Text)|| string.IsNullOrWhiteSpace(txtApplicationRaison.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtAppReasonError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtAppReasonError");
                    isValid = false;
                }
                if (ddlApplicationSponsor.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "ddlAppSponsorError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "ddlAppSponsorError");
                    isValid = false;
                }
            }

            if (int.Parse(lblIsFileShare.Text) == 1) {
                if (string.IsNullOrEmpty(txtFileShareName.Text) || string.IsNullOrWhiteSpace(txtFileShareName.Text))
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtFileShareNameEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtFileShareNameEmptyError");
                    isValid = false;
                }
                if (string.IsNullOrEmpty(txtDataOwnerFileShare.Text) || string.IsNullOrWhiteSpace(txtDataOwnerFileShare.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "DataOwnerEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "DataOwnerEmptyError");
                    isValid = false;
                }

            }
            else if (int.Parse(lblIsFileShare.Text) == 2)
            {
                if (string.IsNullOrEmpty(txtUsernameFileShare.Text) || string.IsNullOrWhiteSpace(txtUsernameFileShare.Text))
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtUsernameFileShareEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtUsernameFileShareEmptyError");
                    isValid = false;
                }
                if (string.IsNullOrEmpty(txtEmailAdress.Text) || string.IsNullOrWhiteSpace(txtEmailAdress.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtEmailAdressEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtEmailAdressEmptyError");
                    isValid = false;
                }

            }
            if (int.Parse(lblFileMendatory.Text) == 1)
            {
               
                if (RequestFiles()==0 && CheckTelephone.Text=="1")
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentTelephone") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentTelephone");
                    isValid = false;
                }
                if (RequestFiles() == 0 && CheckUsb.Text == "1")
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentUSBaccess") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentUSBaccess");
                    isValid = false;
                }
                if (CheckTelephone.Text == "1" && CheckUsb.Text == "1") {

                    if (RequestFiles() <= 1) {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentUSBaccess") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentUSBaccess");
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentTelephone") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentTelephone");
                        isValid = false;
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

        protected void chkRowTelephone_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvTelephone.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowTelephone") as CheckBox);
                if (chkRow.Equals(chk))
                {
                    Label ID = (row.FindControl("txtIDTelephone") as Label);
                    TextBox txt = (row.FindControl("txtQteTelephone") as TextBox);
                    TextBox txtComment = (row.FindControl("txtCommentTelephone") as TextBox);

                    if (chkRow.Checked == true) txt.Text = "1";
                    else
                    {
                        txt.Text = "0";
                        txtComment.Text = string.Empty;
                    }
                    txt.Enabled = chkRow.Checked;
                    txtComment.Enabled = chkRow.Checked;
                    if (ID.Text == "61" && chkRow.Checked == true)
                    {


                        txtComment.Text = (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentTelephone")+" * ";
                       
                        lblFileMendatory.Text ="1";
                        CheckTelephone.Text = "1";
                    }
                    else if (ID.Text == "61" && chkRow.Checked == false)
                    {
                        txtComment.Text = string.Empty;
                        lblFileMendatory.Text = "0";
                        CheckTelephone.Text = "0";
                    }
                        break;
                }
            }
        }

        protected void chkRowConnectivity_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvConnectivity.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowConnectivity") as CheckBox);
                if (chkRow.Equals(chk))
                {
                    Label txtID = (row.FindControl("txtIDConnectivity") as Label);

                    Label txtdgvComment = (row.FindControl("lbdgvComment") as Label);

                    TextBox txt = (row.FindControl("txtQteConnectivity") as TextBox);
                    TextBox txtComment = (row.FindControl("txtCommentConnectivity") as TextBox);

                    if ((chkRow.Checked == true && txtID.Text == "20") || (chkRow.Checked == true && txtID.Text == "62"))
                    {

                        if  (txtID.Text == "20")
                        {
                        txtComment.Text = (string)GetGlobalResourceObject("LocalizedText", "selecttxtNewUserConnectivity");
                            divUserAccountTitle.Text = (string)GetGlobalResourceObject("LocalizedText", "divUserAccountTitle");
                            Literal6.Text = (string)GetGlobalResourceObject("LocalizedText", "divUserttxtDurationAccount");
                            txtComment.ReadOnly = true;
                            //rdNewStart.Checked = true;
                            //rdNewStart.Visible = true;
                            divUserAccountNewStart.Visible = true;

                        }
                        if (txtID.Text == "62")
                        {

                            txtComment.Text= (string)GetGlobalResourceObject("LocalizedText", "selecttxtInternetVisitorConnectivity");
                            //txtdgvComment.Visible = true;
                            Literal6.Text= (string)GetGlobalResourceObject("LocalizedText", "lblEndDate");
                            //Literal6.Text = (string)GetGlobalResourceObject("LocalizedText", "divUserttxtFullName");
                            
                            rdVisitor.Checked = true;
                            rdVisitor.Visible = false;
                            txtComment.ReadOnly = true;
                            divUserAccountVisitor.Visible = false;
                            //rdNewStart.Checked = false;
                            //rdNewStart.Visible = false;
                            divUserAccountNewStart.Visible = false;
                            //txtComment.Text = (string)GetGlobalResourceObject("LocalizedText", "selecttxtInternetVisitorConnectivity");
                            divUserAccountTitle.Text = (string)GetGlobalResourceObject("LocalizedText", "DivtextVisitor");
                            


                        }


                       


                        
                        newUserAccount.Visible = true;
                        txt.Text = "1";
                       
                        rdNewStart_CheckedChanged(sender, e);
                        rdVisitor_CheckedChanged(sender, e);
                    }
                    else if (txtID.Text == "31") {

                        txtComment.Text = (string)GetGlobalResourceObject("LocalizedText", "lblShareNeedAccess");

                    }
                    else if (chkRow.Checked == false && txtID.Text == "20") 
                    {

                        txt.Text = "0"; newUserAccount.Visible = false;
                    }
                    else
                    {
                        if (chkRow.Checked == true && txtID.Text == "62")
                        {
                            txtComment.Text = (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentConnectivity");
                        }
                        else if (chkRow.Checked == false && txtID.Text == "62")
                        {
                            txtComment.Text = string.Empty;
                        }
                        else
                        {
                            txt.Text = "1";
                            txtComment.Text = string.Empty;
                        }

                    }
                    txt.Enabled = chkRow.Checked;
                    txtComment.Enabled = chkRow.Checked;


                    if (txtID.Text == "66" && chkRow.Checked == true)
                    {


                        txtComment.Text = (string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentUSBaccess") + " * ";

                        lblFileMendatory.Text = "1";
                        CheckUsb.Text = "1";
                    }
                    else if (txtID.Text == "66" && chkRow.Checked == false)
                    {
                        txtComment.Text = string.Empty;
                        lblFileMendatory.Text = "0";
                        CheckUsb.Text = "0";
                    }

                    break;
                }
            }
        }

        protected void chkRowSoftware_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in  dgvSofware.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowSoftware") as CheckBox);
                if (chkRow.Equals(chk))
                {
                    TextBox txtID = (row.FindControl("txtIDSoftware") as TextBox);
                    TextBox txt = (row.FindControl("txtQteSoftware") as TextBox);
                    TextBox txtComment = (row.FindControl("txtCommentSoftware") as TextBox);

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

        protected void chkRowNetwork_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvNetwork.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowNetwork") as CheckBox);
                if (chkRow.Equals(chk))
                {

                    TextBox txt = (row.FindControl("txtQteNetwork") as TextBox);
                    TextBox txtComment = (row.FindControl("txtCommentNetwork") as TextBox);

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
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDepartement"));
          
        }

        protected void chkRowApplication_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvApplication.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowApplication") as CheckBox);
                if (chkRow.Equals(chk))
                {
                    Label txtID = (row.FindControl("txtIDApplication") as Label);
                    if (txtID.Text == "54" && chk.Checked==true)
                    {
                        ddlApplicationName.Enabled = false;
                       

                        ddlApplicationResponsible.Enabled = true;
                        txtApplicationDescription.Enabled = true;
                        ddlApplicationSponsor.Enabled = true;
                        txtApplicationDescription.Text = string.Empty;
                    }
                    else
                    {
                        ddlApplicationName.Enabled = true;
                        ddlApplicationResponsible.Enabled = false;
                        

                        txtApplicationDescription.Enabled = false;
                      

                        ddlApplicationSponsor.Enabled = false;
                        
                        txtApplicationDescription.Text = "None";
                    }
                    if (chk.Checked)
                    {
                        applicationChecked = true;
                        newApp = (txtID.Text == "54") ? true : false;
                    }
                    else
                    {
                        applicationChecked = false;
                    }
                }
                else
                {
                    chkRow.Checked = false;
                }
            }
            UpdatePanel300.Update();
        }

        protected void Deselectall_Click(object sender, EventArgs e,string id)
        {
            foreach (GridViewRow row in dgvApplication.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label txt = (row.FindControl("txtIDApplication") as Label);
                    CheckBox chkRow = (row.FindControl("chkRowApplication") as CheckBox);

                    if (txt.Text!=id)
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

        public int RequestFiles() {
            int nbre = 0;
            HttpFileCollection fileCollection = Request.Files;
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile uploadfile = fileCollection[i];
                string fileName = Path.GetFileName(uploadfile.FileName);
                string contentType = uploadfile.ContentType;
                if (uploadfile.ContentLength > 0)
                {
                    nbre += 1;
                }
            }
            return nbre;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
         
            Response.Redirect("New_ITRequest.aspx");

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openConfirm();", true);
        }

        protected void chkRowFileShare_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            foreach (GridViewRow row in dgvFileShare.Rows)
            {
                CheckBox chkRow = (row.FindControl("chkRowFileShare") as CheckBox);
             
                if (chkRow.Equals(chk))
                {
                    Label txtID = (row.FindControl("txtIDFileShare") as Label);
                    TextBox txt = (row.FindControl("txtQteFileShare") as TextBox);
                    TextBox txtComment = (row.FindControl("txtCommentFileShare") as TextBox);

                    if (chkRow.Checked == true) txt.Text = "1";
                    else
                    {
                        txt.Text = "1";
                        txtComment.Text = string.Empty;
                    }
                    txt.Enabled = chkRow.Checked;
                    txtComment.Enabled = chkRow.Checked;

                    if (txtID.Text == "59")
                    {
                        NewFileShare.Visible = true;
                        FileShareAccess.Visible = false;
                        lblIsFileShare.Text = "1";
                        txtFileShareName.Text = string.Empty;
                        txtDataOwnerFileShare.Text = string.Empty;
                    }
                    else if (txtID.Text == "60")
                    {
                        NewFileShare.Visible = false;
                        FileShareAccess.Visible = true;
                        lblIsFileShare.Text = "2";
                        txtUsernameFileShare.Text = string.Empty;
                        txtEmailAdresseFileShare.Text = string.Empty;
                    }
                    if (chkRow.Checked == false)
                    {

                        NewFileShare.Visible = false;
                        FileShareAccess.Visible = false;
                        lblIsFileShare.Text = "0";
                        txtFileShareName.Text = string.Empty;
                        txtDataOwnerFileShare.Text = string.Empty;
                        txtUsernameFileShare.Text = string.Empty;
                        txtEmailAdresseFileShare.Text = string.Empty;
                    }
                }
                else
                {
                    chkRow.Checked = false;
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
          
            Response.Redirect("Detail_itRequest.aspx?IDITrequest=" + lblIDRequest.Text);
        
        }

      

        protected void txtSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSoftware.SelectedIndex == 0)
            {
                standarSoftware.Visible = false;
                nonstandardsoftware.Visible = false;
            }
            else if (txtSoftware.SelectedIndex==1)
            {
                standarSoftware.Visible = true;
                nonstandardsoftware.Visible = false;
            }
            else if (txtSoftware.SelectedIndex==2) {
                standarSoftware.Visible = false;
                nonstandardsoftware.Visible = true;
            }
        }
        protected void GetEmployeeTypes()
        {
            EmployeeType.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));
            EmployeeType.Items.Insert(1, "KCC/ICON");
            EmployeeType.Items.Insert(2, "Visitor");
            EmployeeType.Items.Insert(3, "Contractor");

        }


      

        protected void LstPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if ((LstPriority.SelectedItem.Text == "Very Urgent") || (LstPriority.SelectedItem.Text == "High"))
            //{
            //    panelUrgencyComment.Visible = true;
            //}
            //else
            //{
            //    panelUrgencyComment.Visible = false;
            //    txtUrgencyComment.Text = "";
            //}
            if (LstPriority.SelectedIndex != 0)
            {
                panelUrgencyComment.Visible = true;
                txtUrgencyComment.Text = "";
            }
            if (int.Parse(LstPriority.SelectedValue) == 3 || int.Parse(LstPriority.SelectedValue) == 4)
            {
                mendatory.Visible = false;
            }
            else {
                mendatory.Visible = true;
            }
        }


        protected void rdVisitor_CheckedChanged(object sender, EventArgs e)
        {
            //forNonKcc.Visible = rdVisitor.Checked;
            //forKCC.Visible = rdNewStart.Checked;
        }

        protected void rdNewStart_CheckedChanged(object sender, EventArgs e)
        {
            //forNonKcc.Visible = rdVisitor.Checked;
            //forKCC.Visible = rdNewStart.Checked;
        }

        protected void EmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string EmployeeTypes = EmployeeType.SelectedIndex.ToString() ;
            if (EmployeeTypes == "1")
            {
                forNonKcc.Visible = false ;
                forKCC.Visible = true;
                txtCompany.Text = "Kamoto Copper Company";
            }
            else if ((EmployeeTypes == "2") || (EmployeeTypes == "3"))
            {

                forNonKcc.Visible = true ;
                forKCC.Visible =false ;
                txtCompany.Text = "";
            }


               
        }
        

    }
}