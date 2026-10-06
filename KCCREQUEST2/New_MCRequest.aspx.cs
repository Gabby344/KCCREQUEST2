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
using System.Configuration;
using System.Data.SqlClient;

namespace KCCREQUEST2
{
    public partial class New_MCRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        DataTable tb = new DataTable();
        DataRow dr;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {


                if (!IsPostBack)
                {
                    GetEmployee();
                    //PopulateEmployeeAndDepartment();
                    PopulatePriority();
                    populateTrainingType();
                    populateContractant();
                    PopulateMCRSites();
                    //PopulateSites();
                    populateTrainingCalendar(Convert.ToInt32(Request["TrainingEventID"]));
                  
                    createtable();


                  



                }
            }
            catch (Exception ex)
            {

                //string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                //lblErreur.Text = error;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

                //Response.Redirect("pageError.aspx");
               // Response.Redirect("TrainingCalendar.aspx");
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

        public void PopulatePriority()
        {
            try
            {
            //    LstPriority.DataSource = db.Get_KCCRequest_Priority(utility.Login(loggedUser));
            //    LstPriority.DataValueField = "PrioriteID";
            //    LstPriority.DataTextField = "PrioriteDescprition";
            //    LstPriority.DataBind();
            //    LstPriority.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllpriority"));
            }
            catch (Exception ex)
            {

                throw;
            }
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

        public void populateContractant()
        {

            //LstContractor.DataSource = Developper.Employee.Get_Company();
            //LstContractor.DataTextField = "DepartmentCode";
            //LstContractor.DataValueField = "DepartmentCode";
            //LstContractor.DataBind();
            //LstContractor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllContractor"));
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

                    ////Loading Departments
                    //LstDepartment.DataSource = listDepartement.GroupBy(x => x.DepartmentCode).Select(x => x.First());
                    //LstDepartment.DataTextField = "DepartmentCode";
                    //LstDepartment.DataValueField = "DepartmentCode";
                    //LstDepartment.DataBind();
                    //LstDepartment.Enabled = true;

                   // dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                    //dgvKCCEmployees.DataBind();

                }

                else
                {


                    //LstDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    //LstDepartment.DataTextField = "LongDescription";
                    //LstDepartment.DataValueField = "Code";
                    //LstDepartment.DataBind();
                    //LstDepartment.Enabled = true;

                    //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                    //dgvKCCEmployees.DataBind();

                }
                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRNRequest").FirstOrDefault().Number == 1)
                {

                    //LstDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                    //LstDepartment.DataTextField = "LongDescription";
                    //LstDepartment.DataValueField = "Code";
                    //LstDepartment.DataBind();
                    //LstDepartment.Enabled = true;

                    //LstDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(userLogin, "").FirstOrDefault().DepartmentCode;

                    //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                    //dgvKCCEmployees.DataBind();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void populateTrainingCalendar(int TrainingEventID)
        {
            try
            {

                //dgvTraniningCalendar.DataSource = db.Get_TrainingCalendar_ById(TrainingEventID).ToList();
                //dgvTraniningCalendar.DataBind();

            }

            catch { }
        }



        //protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
        //        LstRequestor.DataTextField = "FirstName";
        //        LstRequestor.DataValueField = "Login";
        //        LstRequestor.DataBind();
        //        LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "lstSelectGeneric"));

        //        //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
        //        //dgvKCCEmployees.DataBind();


        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        protected void LstContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dgvContractor.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstContractor.SelectedValue);
            //dgvContractor.DataBind();
        }

         
            

        protected void btnSave_Click(object sender, EventArgs e)

        {
            try
            {
                DateTime Date1=   DateTime.ParseExact(StartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
         DateTime Date2 = DateTime.ParseExact(EndDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
            //string Date = (DateTime.Parse(EndDate.Text).ToString("MM/dd/yyyy"));
            if (ValidateInputs())
            {
                int ID = 0;


                ID = db.Insert_MCRequest_BonEntree(txtContract.Text, LstCompanyName.SelectedValue, int.Parse(LstFrom.SelectedValue), int.Parse(LstTmTp.SelectedValue), dllDepartment.SelectedValue, txtreason.Text, txtContractorEmail.Text, txtContractorManager.Text, txtEscoter.Text, txtFunctionEscoter.Text, LstRequestor.Text, Date1, Date2, 0, 0, utility.Login(loggedUser));


                if (dgvContractor.Rows.Count > 0)
                {
                    insertContractant(ID);
                }

                //insereFile((int)ID);
                //db.Sendmail_Training((int)ID);

                lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                lblIDRequest.Text = ID.ToString();


                ModalPopupExtender1.Show();
            }
            }
            catch
            {

            }

        }


        public int CountSelectContractor()
        {
            int c = 0;
            foreach (GridViewRow row in dgvContractor.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox chkRow = (row.FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked == true)
                        c++;
                }
            }
            return c;
        }


       


        public void insertContractant(int idBonEntree)
        {
            int c = 0;
            foreach (GridViewRow row in dgvContractor.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DateTime Date1 = DateTime.ParseExact(StartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    DateTime Date2 = DateTime.ParseExact(EndDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);

                    Label employeeID = (row.FindControl("lblEmployeeNumber") as Label);
                    Label CodeProduct = (row.FindControl("lblCodeProduct") as Label);
                    Label Designation = (row.FindControl("lblDesignation") as Label);
                    Label Quantity = (row.FindControl("lblQuantity") as Label);
                    Label From = (row.FindControl("lblFrom") as Label);
                    Label TmTp = (row.FindControl("lblTmTp") as Label);
                    Label From1 = (row.FindControl("lblFrom1") as Label);
                    Label TmTp1 = (row.FindControl("lblTmTp1") as Label);



                    db.Insert_MCRequest_BonEntree_Detail (idBonEntree, CodeProduct.Text, Designation.Text, Convert.ToInt32(Quantity.Text),Convert.ToInt32(From1.Text), Convert.ToInt32(TmTp1.Text), Date1, Date2);


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
                    db.insertTrainingFile(id, fileName, contentType, uploadFile);
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail_BonEntree.aspx?MCRequestID=" + lblIDRequest.Text);

        }

        public void createtable()
        {

            tb.Columns.Add("EmployeeNumber", typeof(string));
            tb.Columns.Add("CodeProduct", typeof(string));
            tb.Columns.Add("Designation", typeof(string));
            tb.Columns.Add("Quantity", typeof(string));
            tb.Columns.Add("From", typeof(string));
            tb.Columns.Add("TmTp", typeof(string));
            tb.Columns.Add("From1", typeof(string));
            tb.Columns.Add("TmTp1", typeof(string));


            dr = tb.NewRow();

            dgvContractor.DataSource = tb;
            dgvContractor.DataBind();

            ViewState["table1"] = tb;

        }
       



      


        protected void Addnewrow()
        {
            try
            {
                if (txtCodeProduct.Text == string.Empty || txtDesignation.Text == string.Empty || txtQuantity.Text == string.Empty || LstFrom.Text == string.Empty || LstFrom.SelectedIndex == 0 || LstTmTp.SelectedIndex == 0 || int.Parse(txtQuantity.Text)<1)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNewRowError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    tb = (DataTable)ViewState["table1"];
                    dr = tb.NewRow();
                    //dr["EmployeeNumber"] = txtContractorEmployeeNumber.Text;
                    dr["CodeProduct"] = txtCodeProduct.Text;
                    dr["Designation"] = txtDesignation.Text;
                    dr["Quantity"] = txtQuantity.Text;
                   
                    dr["From"] = LstFrom.SelectedItem;
                    dr["TmTp"] = LstTmTp.SelectedItem;

                    dr["From1"] = LstFrom.SelectedValue;
                    dr["TmTp1"] = LstTmTp.SelectedValue;

                    tb.Rows.Add(dr);

                    dgvContractor.DataSource = tb;
                    dgvContractor.DataBind();


                   // txtContractorEmployeeNumber.Text = string.Empty;
                    txtCodeProduct.Text = string.Empty;
                    txtDesignation.Text = string.Empty;
                    //txtQuantity.Text = string.Empty;
                    LstFrom.Text = string.Empty;
                    //txtTmTp.Text = string.Empty;
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

        protected void btnAddContractor_Click(object sender, EventArgs e)
        {
            Addnewrow();
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {

            try
            {
                Button btn = (Button)sender;


                foreach (GridViewRow row in dgvContractor.Rows)
                {
                    Button btn1 = (Button)row.FindControl("btndelete");

                    if (btn1.Equals(btn))
                    {
                        tb = (DataTable)ViewState["table1"];


                        int indexe = row.RowIndex;

                        tb.Rows.RemoveAt(indexe);
                        dgvContractor.DataSource = tb;
                        dgvContractor.DataBind();



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

        protected void PopulateMCRSites()
        {
            try
            {
                LstFrom.DataSource = db.Get_MCRequest_Sites().ToList();
                LstFrom.DataTextField = "SiteName";
                LstFrom.DataValueField = "Id";
                LstFrom.DataBind();
                LstFrom.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "lstSelectGeneric"));

                //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
                //dgvKCCEmployees.DataBind();


            }
            catch (Exception ex)
            {

                throw;
            }
        }



        //protected void PopulateSites()
        //{
        //    try
        //    {
        //        LstSite.DataSource = db.Get_MCRequest_Sites().ToList();
        //        LstSite.DataTextField = "SiteName";
        //        LstSite.DataValueField = "Id";
        //        LstSite.DataBind();
        //        LstSite.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "lstSelectGeneric"));

        //        //dgvKCCEmployees.DataSource = Developper.Employee.Get_Employee().Where(x => x.DepartmentCode == LstDepartment.SelectedValue).ToList();
        //        //dgvKCCEmployees.DataBind();


        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
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

        protected void LstFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            LstTmTp.DataSource = db.Get_MCRequest_Sites().Where(x => x.ID.ToString() != LstFrom.SelectedValue).ToList();
            LstTmTp.DataTextField = "SiteName";
            LstTmTp.DataValueField = "Id";
            LstTmTp.DataBind();
            //LstTmTp.Items.Insert(0, LstTmTp.SelectedItem.ToString());
            LstTmTp.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "lstSelectGeneric"));



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
                if (txtContract.Text == string.Empty || string.IsNullOrWhiteSpace(txtContract.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtContractEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtContractEmptyError");
                    valid = false;
                }
                if (LstCompanyName.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstCompanyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstCompanyError");
                    valid = false;
                }
                if (txtContractorManager.Text == string.Empty || string.IsNullOrWhiteSpace(txtContractorManager.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtContractManagerError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtContractManagerError");
                    valid = false;
                }
                if (txtContractorEmail.Text == string.Empty || string.IsNullOrWhiteSpace(txtContractorEmail.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtContractorEmailError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtContractorEmailError");
                    valid = false;
                }
                if (LstTmTp.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstMaterialSitesError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstMaterialSitesError");
                    valid = false;
                }
                if (txtreason.Text == string.Empty || string.IsNullOrWhiteSpace(txtreason.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtReasonRequireError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtReasonRequireError");
                    valid = false;
                }
                //if (txtRequestDescription.Text == string.Empty || string.IsNullOrWhiteSpace(txtRequestDescription.Text))
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
                if (EndDate.Text == string.Empty || string.IsNullOrWhiteSpace(EndDate.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtMCEndDateError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtMCEndDateError");
                    valid = false;
                }
                if ((!string.IsNullOrEmpty(EndDate.Text)) && ((!string.IsNullOrEmpty(StartDate.Text)))  && ((DateTime.ParseExact(EndDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) < (DateTime.ParseExact(StartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture))))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtMCEndDateError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtMCEndDateError");
                    valid = false;
                }
                if (dgvContractor.Rows.Count < 1)
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
    }
}