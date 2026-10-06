using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using KCCDBAccess;
using System.Globalization;

namespace KCCREQUEST2
{
    public partial class New_TRANSRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        int Version = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsPostBack)
                {
                    PopulateEmployeeAndDepartment();
                    PopulateCategory();
                    PopulatePriority();
                    PopulateDeparturePlace();
                }
            }
            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

                Response.Redirect("pageError.aspx");
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

                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "EWRequest").FirstOrDefault().Number == 1)
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
        public void PopulateCategory()
        {
            try
            {
                LstCategory.DataSource = db.Get_KCCRequest_Category(utility.Login(loggedUser));
                LstCategory.DataValueField = "CategoryID";
                LstCategory.DataTextField = "CategoryDescprition";
                LstCategory.DataBind();
                LstCategory.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllCategorie"));
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
        public void PopulateDeparturePlace()
        {
            try
            {
                LstDeparturePlace.DataSource = db.Get_TRANSRequest_Place();
                LstDeparturePlace.DataValueField = "PlaceId";
                LstDeparturePlace.DataTextField = "PlaceDescription";
                LstDeparturePlace.DataBind();
                LstDeparturePlace.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAMTSite"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateDeparturePlace2(int DeparturePlaceId)
        {
            try
            {
                LstArrivalPlace.DataSource = db.Get_TRANSRequest_Place().Where(x => x.PlaceId != Convert.ToInt32(LstDeparturePlace.SelectedValue)) ;
                LstArrivalPlace.DataValueField = "PlaceId";
                LstArrivalPlace.DataTextField = "PlaceDescription";
                LstArrivalPlace.DataBind();
                LstArrivalPlace.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAMTSite"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void LstDeparturePlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDeparturePlace2(Convert.ToInt32(LstDeparturePlace.SelectedValue));
        }
     

        //public void PopulateAMTFleet(string fleetID)
        //{
        //    try
        //    {
        //        LstArrivalPlace.DataSource = Developper.Employee.Get_AMT_Fleet(fleetID);
        //        LstArrivalPlace.DataValueField = "Fleet";
        //        LstArrivalPlace.DataTextField = "Fleet";
        //        LstArrivalPlace.DataBind();
        //        LstArrivalPlace.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAMTEFleet"));
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
      

     

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
         
                if (LstArrivalPlace.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstArrivalEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstArrivalEmptyError");
                    valid = false;

                }
                if (LstDeparturePlace.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstDepartureEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstDepartureEmptyError");
                    valid = false;
                }

                //if (LstCategory.SelectedIndex == 0)
                //{
                //    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError");
                //    valid = false;
                //}
                //if (LstPriority.SelectedIndex == 0)
                //{
                //    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError");
                //    valid = false;
                //}

               
                    if (string.IsNullOrEmpty(txtTimeRange.Text) || string.IsNullOrWhiteSpace(txtTimeRange.Text))
                    {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError");
                    valid = false;
                }

                if (string.IsNullOrEmpty(txtRequestDescription.Text) || string.IsNullOrWhiteSpace(txtRequestDescription.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError");
                    valid = false;
                }

                if (string.IsNullOrEmpty(PlaceNumber.Text) || string.IsNullOrWhiteSpace(PlaceNumber.Text) || Convert.ToInt32(PlaceNumber.Text) < 1)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtNumberPlaceEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtNumberPlaceEmptyError");
                    valid = false;
                }

                if (string.IsNullOrEmpty(PhoneNumber.Text) || string.IsNullOrWhiteSpace(PhoneNumber.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtNumberPhoneEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtNumberPhoneEmptyError");
                    valid = false;
                }

                //DateTime dt = DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                if (txtPlannedStartDate.Text == string.Empty || string.IsNullOrWhiteSpace(txtPlannedStartDate.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateEmptyError");
                    valid = false;
                }
                if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text)) && ((DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)) <= DateTime.Today.AddDays(-1)))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtPlannedStartDateLess");
                    valid = false;
                }

                if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text))  && (!string.IsNullOrEmpty(txtTimeRange.Text)))
                {
                   
                 
                    string Date = (DateTime.Parse(DateTime.Now.Date.ToString()).ToString("MM-dd-yyyy")) + " " + (DateTime.Parse(DateTime.Now.TimeOfDay.ToString()).ToString("HH: mm"));


                    if ((this.GetStartDatetTime() < DateTime.Parse(Date)))
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtStartDateError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtStartDateError");
                        valid = false;
                    }
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


        protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDepartement"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        DateTime GetStartDatetTime()
        {
            try
            {
                DateTime dateRetour = new DateTime();
                if ((!string.IsNullOrEmpty(txtPlannedStartDate.Text)) && (!string.IsNullOrEmpty(txtTimeRange.Text)))
                {
                    string heure;
                    int heureSimple = int.Parse(txtTimeRange.Text.Split(':')[0]);
                    heure = (heureSimple >= 10) ? txtTimeRange.Text : "0" + txtTimeRange.Text;

                    dateRetour = DateTime.ParseExact(txtPlannedStartDate.Text + " " + heure, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture);
                }
                return dateRetour;
            }
            catch (Exception ex)
            {

                throw;
            }

        }




        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_TRANSRequest.aspx");
        }

    

     
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    int? ewrID = null;
                    //string date = txtPlannedStartDate.Text+' '+txtTimeRange.Text;
                   ewrID = Convert.ToInt32(db.Insert_TRANSRequest(LstRequestor.SelectedValue, PhoneNumber.Text, GetStartDatetTime(), Convert.ToInt32(LstDeparturePlace.SelectedValue) , Convert.ToInt32(LstArrivalPlace.SelectedValue) , Convert.ToInt32(PlaceNumber.Text), txtRequestDescription.Text, utility.Login(loggedUser)));


                    Insert_TRANSRequest_Files((int)ewrID);

                    lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                    lblIDRequest.Text = ewrID.ToString();


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

        public void Insert_TRANSRequest_Files(int ID)
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
                        db.insertTransportFile(ID, fileName, contentType, oneFile);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail_TRANSRequest.aspx?TRANSRequestID=" + lblIDRequest.Text);

        }

      
    }
}