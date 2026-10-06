using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.Globalization;

namespace KCCREQUEST2
{
    public partial class Detail_TRANSRequest : BasePage
    {
        DataTable tb = new DataTable();
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        DataRow dr;
        int Version = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
            {

                PopulateCategory();
                PopulatePriority();
                PopulateDeparturePlace();
                populateUsers();
                disableControl(false);

                btnApproval.Visible = false;
                BtnReject.Visible = false;
      
                btnUpdate.Visible = false;

                if (Request.Params["TRANSRequestID"] != null)
                {
                    int id = int.Parse(Request.Params["TRANSRequestID"]);
                    var ewr = db.Get_TRANSRequest_ById(id, utility.Login(loggedUser));
                    PopulateDepartment(ewr.FirstOrDefault().RequestorLogin);
                    title.InnerText = ewr.FirstOrDefault().Id.ToString();
                    txtRequestDescription.Text = ewr.FirstOrDefault().Comment;

                    PhoneNumber.Text = ewr.FirstOrDefault().PhoneNumber;
                    PlaceNumber.Text = ewr.FirstOrDefault().PlaceNumber.ToString() ;
                    txtPlannedStartDate.Text = DateTime.Parse(ewr.FirstOrDefault().DepartureDateTime.ToString()).ToString(format: "MM-dd-yyyy HH:mm");

                    LstDeparturePlace.SelectedValue = ewr.FirstOrDefault().DeparturePlaceId.ToString();
                    PopulateDeparturePlace2(Convert.ToInt32(LstDeparturePlace.SelectedValue)) ;
                    LstArrivalPlace.SelectedValue = ewr.FirstOrDefault().ArrivalPlaceId.ToString();
                   
                    LstDepartment.SelectedIndex = LstDepartment.Items.IndexOf(LstDepartment.Items.FindByValue(ewr.FirstOrDefault().Department));
                    LstDepartment_SelectedIndexChanged(sender, e);

                    txtCurrentUser.Text = ewr.FirstOrDefault().CurrentUser ;
                    txtStatut.Text = ewr.FirstOrDefault().StatusDescription ;
                   
                    LstRequestor.SelectedIndex = LstRequestor.Items.IndexOf(LstRequestor.Items.FindByValue(ewr.FirstOrDefault().RequestorLogin));
               

                    dgvHistory.DataSource = db.Get_TRANSRequest_History(id);
                    dgvHistory.DataBind();
                  
                    dgvFiles.DataSource = db.Get_TRANSRequest_File(id);
                    dgvFiles.DataBind();

                 

                    var ewrActions = db.Get_KCCRequest_Action(id, utility.Login(loggedUser), "TRANSRequest");
                    int statut = ewr.FirstOrDefault().StatusID.Value;// ewr.StatusID.Value;
                    var requestToApprove = db.Get_KCCRequest_ToApprove("TRANSRequest", utility.Login(loggedUser)).Where(x => (x.Id == id)).FirstOrDefault();
                    if (ewrActions.Where(x => x.Action == "CanEdit").FirstOrDefault().Visibility >= 1)
                    {
                        btnUpdate.Visible = true;
                        btnApproval.Visible = false;
                        BtnReject.Visible = true;
           

                    }
                    if ((requestToApprove != null) && (!string.IsNullOrEmpty(requestToApprove.Module)))
                    {
                        btnApproval.Visible = true;
                        btnUpdate.Visible = false;
                        BtnReject.Visible = true;
                    }


                }
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

        public void disableControl(bool statut)
        {

            LstRequestor.Enabled = statut;
            LstPriority.Enabled = statut;
            txtPlannedStartDate.Enabled = statut;
            txtStatut.Enabled = statut;
            txtCurrentUser.Enabled = statut;
            PlaceNumber.Enabled = statut;
            txtRequestDescription.Enabled = statut;
            PhoneNumber.Enabled = statut;
            txtPlannedStartDate.Enabled = statut;
            LstArrivalPlace.Enabled = statut;
            LstDeparturePlace.Enabled = statut;
            LstRequestor.Enabled = statut;
            LstDepartment.Enabled = statut;
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



       
        public void PopulateDepartment(string requestor)
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

                    LstDepartment.SelectedValue = Developper.Employee.Get_Employee_Of_Department(requestor, "").FirstOrDefault().DepartmentCode;

                    LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
                    LstRequestor.DataTextField = "FirstName";
                    LstRequestor.DataValueField = "Login";
                    LstRequestor.DataBind();
                    LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));
                }

            }
            catch (Exception ex)
            {

                //throw;
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewReports.aspx?IDEWORequest=" + Request.Params["EWRequestID"]);
        }
        public void PopulateCategory()
        {
            try
            {
                LstCategory.DataSource = db.Get_KCCRequest_Category(utility.Login(loggedUser));
                LstCategory.DataValueField = "CategoryID";
                LstCategory.DataTextField = "CategoryDescprition";
                LstCategory.DataBind();
                LstCategory.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllcategorie"));
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



        public void populateUsers()
        {

            dllUsers.DataSource = db.Get_Employee_User();
            dllUsers.DataTextField = "FirstName";
            dllUsers.DataValueField = "Login";
            dllUsers.DataBind();
            dllUsers.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdlluser"));
        }
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

            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "EWRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));

        }

     


        public decimal totalGeneral()
        {
            decimal result = 0;
            foreach (DataRow dr in tb.Rows)
            {

                //if (dr["Total"] != string.Empty)
                result += decimal.Parse(dr["Total"].ToString());
            }
            return result;

        }

       

        protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
            LstRequestor.DataTextField = "FirstName";
            LstRequestor.DataValueField = "Login";
            LstRequestor.DataBind();
            LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));
        }

   

     

        public void PopulateDeparturePlace2(int DeparturePlaceId)
        {
            try
            {
                LstArrivalPlace.DataSource = db.Get_TRANSRequest_Place().Where(x => x.PlaceId != Convert.ToInt32(LstDeparturePlace.SelectedValue));
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

        protected void btnUpdate_Click(object sender, EventArgs e)
            {
                //if (Request.Params["EWRequestID"] != null)
                //{
                //    int id = int.Parse(Request.Params["EWRequestID"]);
                //    if (ValidateInputs())
                //    {
                //        if (Version == 1)
                //        {
                //            db.Update_EWRequest(id, LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtRequestDescription.Text, txtDesignedExisting.Text, txtScopeOfWork.Text, txtDesignedExisting.Text, LstSite.SelectedValue, LstFleet.SelectedValue, LstEquipement.SelectedValue, DateTime.Parse(txtPlannedStartDate.Text), LstDepartment.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), utility.Login(loggedUser));
                //        }
                //        else if (Version == 2)
                //        {
                //            db.Update_EWRequest_I(id, LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtRequestDescription.Text, txtDesignedExisting.Text, txtScopeOfWork.Text, txtDesignedExisting.Text, LstSite.SelectedValue, LstFleet.SelectedValue, LstEquipement.SelectedValue, DateTime.Parse(txtPlannedStartDate.Text), LstDepartment.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), utility.Login(loggedUser), int.Parse(LstTask.SelectedValue));
                //        }

                //        //Response.Redirect("Default_View_Approve.aspx");


                //        ModalPopupExtender1.Show();
                //    }
                //}


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
                int id = int.Parse(Request.Params["TRANSRequestID"]);

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

                        byte[] bytes = (byte[])db.Get_TRANSRequest_File(id).Where(x => x.FileID.ToString() == fileID.Text).FirstOrDefault().File.ToArray();

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



        protected void dllUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string employedID = db.Get_Employee_User().Where(x => x.Login == dllUsers.SelectedValue).FirstOrDefault().EmployeeId;


            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "TRANSRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));
            dllRole.Items.Insert(1, employedID);
        }

        protected void btnApproval_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.Params["TRANSRequestID"]);
            var rect = db.Get_TRANSRequest_ById (id, utility.Login(loggedUser));
            lblaction.Text = "1";
            attachement.Visible = true;
            lblFile.Visible = true;
            FileUpload2.Visible = true;
       
          
                txtComment.Text = string.Empty;

                this.Form.DefaultButton = this.btnSave.UniqueID;
                lblTitre.Text = "- " + btnApproval.Text;

                Page.SetFocus(txtComment.ClientID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (txtComment.Text == string.Empty || string.IsNullOrWhiteSpace(txtComment.Text))
            {

                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            else
            {
                try
                {
                    //Approve
                    if (lblaction.Text == "1")
                    {

                        db.ApproveTRANSRequest(int.Parse(Request["TRANSRequestID"]), txtComment.Text, utility.Login(loggedUser));
                        //Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }

                    //Rejected
                    if (lblaction.Text == "3")
                    {

                        db.RejectTRANSRequest(int.Parse(Request["TRANSRequestID"]), txtComment.Text, utility.Login(loggedUser));
                        //Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));
                    }

                 

                    //db.Update_RectRequest(int.Parse(Request["RectRequestID"]), LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtPrincipalTask.Text, txtJobTitle.Text, txtMinimumQualification.Text, txtOhterRequirements.Text,
                    //int.Parse(txtNumberOfPosition.Text), LstDuration.SelectedValue, (DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)), false, budged, false, CbIsReplacement.Checked, LstRequestor.SelectedValue, txtIsReplacement.Text, txtReplacementEmployeeName.Text, txtReplacementReason.Text,
                    //txtReplacementPostNumber.Text, txtReplacementJobTitle.Text, int.Parse(LstTypeOfContrat.SelectedValue), int.Parse(LstJobGrade.SelectedValue), LstEmployeeType.SelectedValue, int.Parse(LstRequestMethode.SelectedValue), LstCostCenter.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), LstReportTo.SelectedValue, utility.Login(loggedUser), int.Parse(LstEmployeeType.SelectedValue), null, null, budgedYear, txtComment.Text, utility.Login(loggedUser));

                    //Insert_RectRequest_Files(decimal.Parse(Request["RectRequestID"]));


                    Response.Redirect("My_RequestToAproval.aspx?type=TRANSRequest");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                    ModalPopupExtender1.Show();
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




        protected void BtnReject_Click(object sender, EventArgs e)
        {
            if (Request.Params["TRANSRequestID"] != null)
            {
                lblaction.Text = "3";


                    txtComment.Text = string.Empty;
                    Page.SetFocus(txtComment.ClientID);
                  
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                
            }
        }



    }








}
