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

namespace KCCREQUEST2
{
    public partial class New_MCRequest_S : BasePage
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
            tbitems.Columns.Add("Id", typeof(int));
            tbitems.Columns.Add("IdBonEntree", typeof(string));
            tbitems.Columns.Add("DesignationProduit", typeof(string));
            tbitems.Columns.Add("Quantity", typeof(int));
            tbitems.Columns.Add("IdCompany", typeof(string));

            dr = tbitems.NewRow();
            ViewState["table1"] = tbitems;

        }

        protected void Addnewrow(int itemsID, int IdBonEntree,  string itemsDescription, int quantite, string comment)
        {
            tbitems = (DataTable)ViewState["table1"];
            dr = tbitems.NewRow();
            dr["Id"] = itemsID;
            dr["IdBonEntree"] = IdBonEntree;
            dr["DesignationProduit"] = itemsDescription;
            dr["Quantity"] = quantite;
            dr["IdCompany"] = comment;
            tbitems.Rows.Add(dr);
        }


        public void addItemsComputer()
        {

            try
            {
                int DetailId; int IdBonEntree; string itemsDescription; int quantite; string comment;
                foreach (GridViewRow row in dgvComputer.Rows)
                {

                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.FindControl("chkRowComputer") as CheckBox);

                        if (chkRow.Checked)
                        {
                            
                            DetailId = int.Parse((row.FindControl("DetailId") as Label).Text);
                            IdBonEntree = int.Parse((row.FindControl("IdBonEntree") as Label).Text);
                            itemsDescription = (row.FindControl("Label4") as Label).Text;
                            quantite = int.Parse((row.FindControl("txtQteComputer") as TextBox).Text);
                            comment = (row.FindControl("Label5") as Label).Text;

                            Addnewrow(DetailId, IdBonEntree, itemsDescription, quantite, comment);
                        }

                    }

                }

            }
            catch (Exception s)
            {

            }

        }



        public void createdtable()
        {

            tbitems.Columns.Add("Id", typeof(string));
            tbitems.Columns.Add("IdBonEntree", typeof(string));
            tbitems.Columns.Add("IdCompany", typeof(string));
            tbitems.Columns.Add("DesignationProduit", typeof(string));


            dr = tbitems.NewRow();

            dgvComputer.DataSource = tbitems;
            dgvComputer.DataBind();

            ViewState["table1"] = tbitems;

        }

        protected void Selectall_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvComputer.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRowComputer") as CheckBox);
                    if (chkRow.Enabled == true)
                        chkRow.Checked = true;
                }
            }
        }
        protected void Deselectall_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvComputer.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRowComputer") as CheckBox);

                    if (chkRow.Enabled == true)
                        chkRow.Checked = false;
                }
            }
        }

        protected void Inverse_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvComputer.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRowComputer") as CheckBox);

                    if (chkRow.Enabled == true)
                    {

                        if (chkRow.Checked == true) chkRow.Checked = false;
                        else if (chkRow.Checked == false) chkRow.Checked = true;
                    }

                }
            }
        }
        protected void Addednewrow()
        {
            try
            {

                tbitems = (DataTable)ViewState["table1"];
                dr = tbitems.NewRow();

                dr["Id"].ToString();
                dr["DesignationProduit"].ToString();
                dr["Quantity"].ToString();
                dr["IdCompany"] = ToString();



                tbitems.Rows.Add(dr);

                //GridView2.DataSource = tbitems;
                //GridView2.DataBind();



            }
            catch (FormatException ex)
            {

            }
        }

        protected void btnAddContractor_Click(object sender, EventArgs e)
        {

           
         
            addItemsComputer();
            Addednewrow();
            mid.Visible = true;
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
                    LstRequestor.Items.Insert(0, "Choose One");

                    //Loading Departments
                    dllDepartment.DataSource = listDepartement.GroupBy(x => x.DepartmentCode).Select(x => x.First());
                    dllDepartment.DataTextField = "DepartmentCode";
                    dllDepartment.DataValueField = "DepartmentCode";
                    dllDepartment.DataBind();
                    dllDepartment.Enabled = true;

                    // dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                    //dgvKCCEmployees.DataBind();

                }

                else
                {


                    dllDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    dllDepartment.DataTextField = "LongDescription";
                    dllDepartment.DataValueField = "Code";
                    dllDepartment.DataBind();
                    dllDepartment.Enabled = true;

                    //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                    //dgvKCCEmployees.DataBind();

                }
                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRNRequest").FirstOrDefault().Number == 1)
                {

                    dllDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    dllDepartment.DataTextField = "LongDescription";
                    dllDepartment.DataValueField = "Code";
                    dllDepartment.DataBind();
                    dllDepartment.Enabled = true;

                    dllDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(userLogin, "").FirstOrDefault().DepartmentCode;

                    //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                    //dgvKCCEmployees.DataBind();
                }
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
                LstSite.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "lstSelectGeneric"));

                //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                //dgvKCCEmployees.DataBind();


            }
            catch (Exception ex)
            {

                throw;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            if (!IsPostBack)
            {
                PopulateEmployeeAndDepartment();
                PopulateSites();
                populateTrainingType();
                createtable();










            }

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


                    if (chkRow.Checked == true)
                    {
                        txt.Text = txt.Text;

                    }
                    else
                    {
                        txt.Text = txt.Text;

                    }
                    txt.Enabled = chkRow.Checked;



                    break;
                }
            }
        }

        protected void dgvComputer_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }




        protected void dgvComputer_Init(object sender, EventArgs e)
        {


        }
        int idItRequest;



        protected void btnSave_Click(object sender, EventArgs e)

        {
            tbitems = new DataTable();
            createtable();
            addItemsComputer();


            //string Date = (DateTime.Parse(EndDate.Text).ToString("MM/dd/yyyy"));
            if (ValidateInputs())
            {
                int ID = 0;


                ID = db.Insert_MCRequest_BonSortie(LstCompanyName.SelectedValue, dllDepartment.SelectedValue, txtEmployerId.Text, txtEscoter.Text, txtFunctionEscoter.Text, Convert.ToDateTime(StartDate.Text), LstRequestor.Text, utility.Login(loggedUser), 0, 0, txtreason.Text, Convert.ToInt32(LstSite.SelectedIndex));


                //if (GridView2.Rows.Count > 0)
                //{
                    insertDetailSortie(ID);
                //}

                //insereFile((int)ID);
                //db.Sendmail_Training((int)ID);

                lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                lblIDRequest.Text = ID.ToString();


                ModalPopupExtender1.Show();
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
        public bool ValidateInputs()
        {
            try
            {
                bool valid = true;
                string error = string.Empty;
                if (LstRequestor.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstRequestorEmptyError");
                    valid = false;
                }

                if (LstCompanyName.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstCompanyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstCompanyError");
                    valid = false;
                }


                if (LstSite.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstMaterialSitesError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstMaterialSitesError");
                    valid = false;
                }
                if (txtreason.Text == string.Empty || string.IsNullOrWhiteSpace(txtreason.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtIdEmployerEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtIdEmployerEmptyError");
                    valid = false;
                }
                //if (txtEmployerId.Text == string.Empty || string.IsNullOrWhiteSpace(txtEmployerId.Text))
                //{
                //    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtReasonRequireError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtReasonRequireError");
                //    valid = false;
                //}
                if (txtEscoter.Text == string.Empty || string.IsNullOrWhiteSpace(txtEscoter.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtEscorteurError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtEscorteurError");
                    valid = false;
                }
                if (txtFunctionEscoter.Text == string.Empty || string.IsNullOrWhiteSpace(txtFunctionEscoter.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtEscorteurFunctionError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtEscorteurFunctionError");
                    valid = false;
                }
                if (StartDate.Text == string.Empty || string.IsNullOrWhiteSpace(StartDate.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtMCStartDateError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtMCStartDateError");
                    valid = false;
                }
                if ((!string.IsNullOrEmpty(StartDate.Text)) && ((DateTime.ParseExact(StartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) <= DateTime.Today.AddDays(-1)))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtMCStartDateError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtMCStartDateError");
                    valid = false;
                }


                if (dgvComputer.Rows.Count < 1)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "dgItemsEmptyOrFullError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "dgItemsEmptyOrFullError");
                    valid = false;
                }

                if (valid == false)
                {
                    lblErreur.Text = error;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                return valid;
            }
            catch (Exception ex)
            {

                throw;
            }
        }




















        public void insertDetailSortie(int idBonSortie)
        {

            foreach (DataRow dr in tbitems.Rows)
            {
                //db.Insert_ITRequest_Detail(ID, int.Parse(dr["ItemsID"].ToString()), 1, 0, int.Parse(dr["Quantite"].ToString()), dr["Comment"].ToString());
                db.Insert_MCRequest_BonSortie_Detail(int.Parse(dr["Id"].ToString()), idBonSortie, int.Parse(dr["Quantity"].ToString()), Convert.ToInt32(LstSite.SelectedValue), 0, txtreason.Text);

            }


            //int c = 0;
            //foreach (GridViewRow row in GridView2.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {

            //        Label DetailId2 = (row.FindControl("DetailId2") as Label);
            //        Label IdBonEntree2 = (row.FindControl("IdBonEntree2") as Label);
            //        Label DesignationProduit = (row.FindControl("DesignationProduit2") as Label);
            //        Label Quantity = (row.FindControl("Quantity2") as Label);



            //        if (!string.IsNullOrEmpty(DetailId2.Text))
            //        {
            //            db.Insert_MCRequest_BonSortie_Detail(Convert.ToInt32(DetailId2.Text),idBonSortie,Convert.ToInt32(Quantity.Text), Convert.ToInt32(LstSite.SelectedValue), 0, txtreason.Text);


            //        }




            //    }
            //}

        }




        public void populateTrainingType()
        {

            try
            {
                LstCompanyName.DataSource = Developper.Employee.Get_Company();
                LstCompanyName.DataValueField = "DepartmentCode";
                LstCompanyName.DataTextField = "DepartmentCode";
                LstCompanyName.DataBind();
                LstCompanyName.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "lstSelectGeneric"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public void insertItemItRequest(int ID)
        {

            foreach (DataRow dr in tbitems.Rows)
            {
                db.Insert_ITRequest_Detail(ID, int.Parse(dr["ItemsID"].ToString()), 1, 0, int.Parse(dr["Quantite"].ToString()), dr["Comment"].ToString());
            }
        }

        public bool ValidInputs()
        {
            bool isValid = true;
            string error = "";
            string desc = "";
            string com = "";

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
            if (tbitems.Rows.Count == 0)
            {
                //lblErreur.Text = "No items selected";
                error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError");
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
                    else if (rw["Comment"].ToString().Contains(((string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentTelephone"))))
                    {
                        error = (string.IsNullOrEmpty(error)) ? desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> " + desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                        isValid = false;

                    }
                    else if (rw["Comment"].ToString().Contains(((string)GetGlobalResourceObject("LocalizedText", "selecttxtCommentUSBaccess"))))
                    {
                        error = (string.IsNullOrEmpty(error)) ? desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> " + desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                        isValid = false;

                    }





                    int quantityOut = 0;
                    if ((!int.TryParse(rw["Quantite"].ToString(), out quantityOut)) && (quantityOut == 0))
                    {
                        //error = (string.IsNullOrEmpty(error)) ? rw["ItemsDescription"].ToString() + " 's quantity is not valid" : error + "<br />" + rw["ItemsDescription"].ToString() + " 's quantity is not valid";
                        error = (string.IsNullOrEmpty(error)) ? desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError") : error + "<br /> " + desc + " - " + (string)GetGlobalResourceObject("LocalizedText", "tbItemsEmptyError");
                        isValid = false;
                    }
                }
            }



            return isValid;
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

        public int RequestFiles()
        {
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

            Response.Redirect("New_MCRequest_S.aspx");

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openConfirm();", true);
        }


        protected void btnOk_Click(object sender, EventArgs e)
        {

            Response.Redirect("Detail_BonSortie.aspx?MCSRequestID=" + lblIDRequest.Text);

        }


        public void insertItemTempon(int ID)
        {

            foreach (DataRow dr in tbitems.Rows)
            {
                db.Insert_ITRequest_Detail(ID, int.Parse(dr["ItemsID"].ToString()), 1, 0, int.Parse(dr["Quantite"].ToString()), dr["Comment"].ToString());
            }
        }


        protected void rdVisitor_CheckedChanged(object sender, EventArgs e)
        {
            //forNonKcc.Visible = rdVisitor.Checked;
            //forKCC.Visible = rdNewStart.Checked;
        }

        protected void LstCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvComputer.DataSource = db.Get_MCRequest_BonEntree_Dedail_By_Departmen(utility.Login(loggedUser)).Where(x => x.Department == dllDepartment.SelectedValue && x.IdCompany == LstCompanyName.SelectedValue);
            dgvComputer.DataBind();
        }



        protected void Seach_Click(object sender, EventArgs e)
        {

            dgvComputer.DataSource = db.Get_MCRequest_BonEntree_Dedail_By_Departmen(utility.Login(loggedUser)).Where(x => x.Department == dllDepartment.SelectedValue && x.IdCompany == LstCompanyName.SelectedValue && x.IdBonEntree.ToString() == txtSubject.Text);
            dgvComputer.DataBind();
        }

        protected void LstSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvComputer.DataSource = db.Get_MCRequest_BonEntree_Dedail_By_Departmen(utility.Login(loggedUser)).Where(x => x.Department == dllDepartment.SelectedValue && x.IdCompany == LstCompanyName.SelectedValue && x.IdSite.ToString() == LstSite.SelectedValue);
            dgvComputer.DataBind();

            DivDetail.Visible = true;
            mid.Visible = true;
            
        }


        //protected void btnAddContractor_Click(object sender, EventArgs e)
        //{

        //}

        protected void rdNewStart_CheckedChanged(object sender, EventArgs e)
        {
            //forNonKcc.Visible = rdVisitor.Checked;
            //forKCC.Visible = rdNewStart.Checked;
        }




    }
}