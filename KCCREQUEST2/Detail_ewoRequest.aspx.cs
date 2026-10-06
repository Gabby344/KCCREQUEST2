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
    public partial class Detail_ewoRequest : BasePage
    {
        DataTable tb = new DataTable();
        KCCDBWrapper db = new KCCDBWrapper();
        DataRow dr;
        int Version = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    disableControl(false);
                    createtable();
                    PopulateCategory();
                    PopulatePriority();
                    PopulateAMTSite();
                    populateUsers();
                    populateUsersAdHoc();
                    populateRole();
                    populateBuged();
                    populateStatus();

                    if (Version == 2)
                    {
                        blocEqp.Visible = true;
                        PopulateWorkType();
                        if (LstWorkType.SelectedIndex != 0)
                        {
                            PopulateTask(int.Parse(LstWorkType.SelectedValue));
                        }
                    }
                    else blocEqp.Visible = false;

                    btnApproval.Visible = false;
                    BtnReject.Visible = false;
                    BtnReturnToOrginator.Visible = false;
                    btnUpdate.Visible = false;
                    PopulateTechnicalDepartment();
                    estimation.Visible = false;
                    BtnReturnToOrginator.Visible = false;
                    estimation.Visible = false;
                    if (Request.Params["EWRequestID"] != null)
                    {
                        int id = int.Parse(Request.Params["EWRequestID"]);
                        var ewr = db.Get_EWRequest_ByID(id, CachedLogin);
                        var ewrRequest = ewr.FirstOrDefault();
                        PopulateDepartment(ewrRequest.RequestorLogin);
                        title.InnerText = ewrRequest.Id.ToString();
                        txtRequestDescription.Text = ewrRequest.Description;
                        txtJustificationWhy.Text = ewrRequest.Justification;
                        txtDesignedExisting.Text = ewrRequest.Existing;
                        txtScopeOfWork.Text = ewrRequest.ScopeOfWork;
                        txtPlannedStartDate.Text = DateTime.Parse(ewrRequest.PlannedDeliveryDate.ToString()).ToString(format: "MM-dd-yyyy"); 
                        txtTechnicalDepartement.Text = ewrRequest.TechnicalDepDescription;
                        txtTechnicalManager.Text = ewrRequest.TechnicalManager;
                        txtTechnicalPlanner.Text = ewrRequest.TechnicalPlanner;
                        txtStatut.Text = ewrRequest.StatusDescription;
                        txtCurrentUser.Text = ewrRequest.CurrentUser;

                        if (ewrRequest.BudgedID == true)
                        {
                            LstBudged.SelectedIndex = 0;
                        }
                        else
                        {
                            LstBudged.SelectedIndex = 1;
                        }
                        /**
                         * Loading departments
                         * **/
                        LstDepartment.SelectedIndex = LstDepartment.Items.IndexOf(LstDepartment.Items.FindByValue(ewrRequest.Department));
                        LstDepartment_SelectedIndexChanged(sender, e);
                        /**
                         * Loading sites
                         * **/
                        LstSite.SelectedIndex = LstSite.Items.IndexOf(LstSite.Items.FindByValue(ewrRequest.Site));
                        PopulateAMTFleet(LstSite.SelectedValue);

                        /**
                       * Loading work type and Task
                       * **/
                        LstWorkType.SelectedIndex = LstWorkType.Items.IndexOf(LstWorkType.Items.FindByValue(ewrRequest.WorkTypeID.ToString()));
                        LstWorkType_SelectedIndexChanged(sender, e);
                        LstTask.SelectedIndex = LstTask.Items.IndexOf(LstTask.Items.FindByValue(ewrRequest.TaskId.ToString()));
                       
                        /**
                         * Loading employees
                         * **/
                        LstRequestor.SelectedIndex = LstRequestor.Items.IndexOf(LstRequestor.Items.FindByValue(ewrRequest.RequestorLogin));
                        /**
                         * Loading category
                         * **/
                        LstCategory.SelectedIndex = LstCategory.Items.IndexOf(LstCategory.Items.FindByValue(ewrRequest.CategoryID.ToString()));
                        /**
                         * Loading priority
                         * **/
                        LstPriority.SelectedIndex = LstPriority.Items.IndexOf(LstPriority.Items.FindByValue(ewrRequest.PrioriteID.ToString()));
                        /**
                         * Loading fleets
                         * **/
                        LstFleet.SelectedIndex = LstFleet.Items.IndexOf(LstFleet.Items.FindByValue(ewrRequest.Fleet));
                        PopulateAMTEquipment(LstSite.SelectedValue, LstFleet.SelectedValue);
                        /**
                         * Loading equipments
                         * **/
                        LstEquipement.SelectedIndex = LstEquipement.Items.IndexOf(LstEquipement.Items.FindByValue(ewrRequest.Equipement));
                        /**
                        * Loading historique
                        * **/
                        dgvHistory.DataSource = db.Get_EWRRequest_History(id);
                        dgvHistory.DataBind();
                        /**
                        * Loading estimation
                        * **/
                        dgvEstimation.DataSource = db.Get_EWRequest_Estimation(id);
                        dgvEstimation.DataBind();
                        PopulateTable(id);
                        lbltotalgeneral.InnerText = totalGeneral().ToString();
                        /**
                        * Loading files
                        * **/
                        dgvFiles.DataSource = db.Get_EWRRequest_File(id);
                        dgvFiles.DataBind();

                        /**
                       * Loading AMT detail
                       * **/

                        fvDetailAmt.DataSource = db.Get_EWRequest_ByID(id, CachedLogin).ToList();
                        fvDetailAmt.DataBind();

                        var ewrActions = db.Get_KCCRequest_Action(id, CachedLogin, "EWRequest");
                        int statut = ewrRequest.StatusID.Value;// ewr.StatusID.Value;
                        var requestToApprove = db.Get_KCCRequest_ToApprove("EWRequest", CachedLogin).Where(x => (x.Id == id)).FirstOrDefault();
                        if (ewrActions.Where(x => x.Action == "CanEdit").FirstOrDefault().Visibility >= 1)
                        {
                            btnUpdate.Visible = true;
                            btnApproval.Visible = false;
                            BtnReject.Visible = true;
                            disableControl(true);
                            btnCallBack.Visible = false;

                        }

                        if (ewrActions.Where(x => x.Action == "CanCallBack").FirstOrDefault().Visibility >= 1)
                        {
                            btnCallBack.Visible = true;


                        }
                        else btnCallBack.Visible = false;

                        if ((requestToApprove != null) && (!string.IsNullOrEmpty(requestToApprove.Module)))
                        {
                            if (statut == 5)
                            {
                                estimation.Visible = true;
                                btnSaveEstimation.Visible = true;
                                dgvEstimation.Columns[5].Visible = true;
                                btnBackToOriginator.Visible = false;
                            }
                            else if (statut < 3)
                            {
                                btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Text");
                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = true;
                                if (statut == 2)
                                {
                                    btnBackToOriginator.Visible = true;
                                }
                            }
                            else if (statut == 3)
                            {
                                //btnApproval.Text = "Forward";
                                btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Forward_Text");
                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = true;
                                btnBackToOriginator.Visible = true;
                                LstSite.Enabled = true;
                                LstFleet.Enabled = true;
                                LstEquipement.Enabled = true;
                            }
                            else if (statut == 4)
                            {
                                //btnApproval.Text = "Assign";
                                btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Assign_Text");
                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = true;
                                btnBackToOriginator.Visible = true;
                            }
                            else if (statut == 6)
                            {
                                //btnApproval.Text = "Approve Estimation";
                                btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Estimation_Text");
                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = true;
                                btnBackToOriginator.Visible = true;
                            }
                            else if (statut == 7)
                            {
                                //btnApproval.Text = "Approve Budget";
                                btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Budget");
                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                //BtnReject.Text = "Reject Budget";
                                BtnReject.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Reject_Budget");
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = true;
                            }
                            else if (statut == 8)
                            {
                                //btnApproval.Text = "Approve Budget";
                                btnApproval.Text = (string)GetGlobalResourceObject("LocalizedText", "btnConfirmedSatisfaction");
                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Text = (string)GetGlobalResourceObject("LocalizedText", "btnRejectSatisfaction");
                                BtnReject.Visible = true;
                                BtnReturnToOrginator.Visible = false;
                                btnBackToOriginator.Visible = false;
                            }

                            if (statut == 15)
                            {

                                btnAdHoc.Visible = false;

                                btnApproval.Visible = true;
                                btnUpdate.Visible = false;
                                BtnReject.Visible = false;
                                BtnReturnToOrginator.Visible = false;
                                btnBackToOriginator.Visible = false;
                            }


                        }
                        else { btnAdHoc.Visible = false; }

                        var adminRoleCount = db.Get_KCCRequestIsAdmin(CachedLogin).Where(x => x.ModuleID == "EWRequest").FirstOrDefault().Number;
                        if (adminRoleCount == 0)
                        {


                            btnrRealocate.Visible = false;
                        }
                        else if (adminRoleCount == 1)
                        {


                            btnUpdate.Visible = true;
                            LstBudged.Enabled = true;
                            BtnReject.Visible = true;
                            disableControl(true);

                        }

                        if (statut == 10)
                        {
                            disableControl(false);
                            BtnReturnToOrginator.Visible = false;
                            btnBackToOriginator.Visible = false;
                            BtnReject.Visible = false;
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            btnAdHoc.Visible = false;
                            btnrRealocate.Visible = false;
                        }

                        if (btnCallBack.Visible == true)
                        {
                            disableControl(false);
                            BtnReturnToOrginator.Visible = false;
                            btnBackToOriginator.Visible = false;
                            BtnReject.Visible = false;
                            btnApproval.Visible = false;
                            btnUpdate.Visible = false;
                            btnAdHoc.Visible = false;
                            btnrRealocate.Visible = false;
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //if (Request.Params["EWRequestID"] != null)
            //{
            //    int id = int.Parse(Request.Params["EWRequestID"]);
            //    if (ValidateInputs())
            //    {
            //        if (Version == 1)
            //        {
            //            db.Update_EWRequest(id, LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtRequestDescription.Text, txtDesignedExisting.Text, txtScopeOfWork.Text, txtDesignedExisting.Text, LstSite.SelectedValue, LstFleet.SelectedValue, LstEquipement.SelectedValue, DateTime.Parse(txtPlannedStartDate.Text), LstDepartment.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), CachedLogin);
            //        }
            //        else if (Version == 2)
            //        {
            //            db.Update_EWRequest_I(id, LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtRequestDescription.Text, txtDesignedExisting.Text, txtScopeOfWork.Text, txtDesignedExisting.Text, LstSite.SelectedValue, LstFleet.SelectedValue, LstEquipement.SelectedValue, DateTime.Parse(txtPlannedStartDate.Text), LstDepartment.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), CachedLogin, int.Parse(LstTask.SelectedValue));
            //        }

            //        //Response.Redirect("Default_View_Approve.aspx");


            //        ModalPopupExtender1.Show();
            //    }
            //}


            if (Request.Params["EWRequestID"] != null)
            {
                if (btnUpdate.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Update_Text"))
                {
                    litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Update_Text");
                    btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Update_Text");
                    txtComment.Text = string.Empty;
                    Page.SetFocus(txtComment.ClientID);
                    this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                
                updatePanelPopUpPlan.Update();
            }
        }
        public void createtable()
        {

            tb.Columns.Add("Description", typeof(string));
            tb.Columns.Add("Qte", typeof(decimal));
            tb.Columns.Add("UnitCost", typeof(decimal));
            tb.Columns.Add("UnitMesure", typeof(string));
            tb.Columns.Add("Total", typeof(decimal));

            dr = tb.NewRow();

           dgvEstimation.DataSource = tb;
            dgvEstimation.DataBind();
            totalGeneral();

            ViewState["table1"] = tb;

        }
        void PopulateTable(int id)
        {
            try
            {
                foreach (var item in db.Get_EWRequest_Estimation(id))
                {
                    DataRow rw = tb.NewRow();
                    rw["Description"] = item.Description;
                    rw["Qte"] = item.Qte.Value.ToString();
                    rw["UnitCost"] = item.UnitCost.Value.ToString();
                    rw["UnitMesure"] = item.UnitMesure;
                    rw["Total"] = item.Total.Value.ToString();
                    tb.Rows.Add(rw);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        protected void Addnewrow()
        {
            try
            {
                if (txtDescriptionEstimation.Text == string.Empty || txtQuantity.Text == string.Empty || txtUnitCostEstimation.Text == string.Empty || txtUnitMesure.Text == string.Empty)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblNewRowError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    tb = (DataTable)ViewState["table1"];
                    dr = tb.NewRow();
                    dr["Description"] = txtDescriptionEstimation.Text;
                    dr["Qte"] = (txtQuantity.Text.Contains(".")) ? txtQuantity.Text.Replace('.', ',') : txtQuantity.Text;
                    dr["UnitCost"] = (txtUnitCostEstimation.Text.Contains(".")) ? txtUnitCostEstimation.Text.Replace('.', ',') : txtUnitCostEstimation.Text;
                    dr["UnitMesure"] = txtUnitMesure.Text;
                    dr["Total"] = decimal.Parse(dr["Qte"].ToString()) * decimal.Parse(dr["UnitCost"].ToString());
                    tb.Rows.Add(dr);

                    dgvEstimation.DataSource = tb;
                    dgvEstimation.DataBind();
                    lbltotalgeneral.InnerText = totalGeneral().ToString();

                    txtDescriptionEstimation.Text = string.Empty;
                    txtQuantity.Text = string.Empty;
                    txtUnitCostEstimation.Text = string.Empty;
                    txtUnitMesure.Text = string.Empty;
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

        protected void btnAddEstimation_Click(object sender, EventArgs e)
        {
            Addnewrow();
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {

            try
            {
                Button btn = (Button)sender;


                foreach (GridViewRow row in dgvEstimation.Rows)
                {
                    Button btn1 = (Button)row.FindControl("btndelete");

                    if (btn1.Equals(btn))
                    {
                        tb = (DataTable)ViewState["table1"];


                        int indexe = row.RowIndex;

                        tb.Rows.RemoveAt(indexe);
                        dgvEstimation.DataSource = tb;
                        dgvEstimation.DataBind();

                        lbltotalgeneral.InnerText = totalGeneral().ToString();

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

        protected void dgvEstimation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                {
                    e.Row.Cells[5].Attributes["onclick"] = "if(!confirm('Would you want to delete this item ?')) return false;";
                }
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

                if (db.Get_KCCRequestIsAdmin(CachedLogin).Where(x => x.ModuleID == "EWRequest").FirstOrDefault().Number == 1)
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
        public void PopulateCategory()
        {
            try
            {
                LstCategory.DataSource = db.Get_KCCRequest_Category(CachedLogin);
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

        public void populateBuged()
        {

            LstBudged.Items.Insert(0, "Yes");
            LstBudged.Items.Insert(1, "No");
        }
        public void PopulatePriority()
        {
            try
            {
                LstPriority.DataSource = db.Get_KCCRequest_Priority(CachedLogin);
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
        public void PopulateAMTSite()
        {
            try
            {
                LstSite.DataSource = Developper.Employee.Get_AMT_Site();
                LstSite.DataValueField = "SiteDescription";
                LstSite.DataTextField = "SiteDescription";
                LstSite.DataBind();
                LstSite.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAMTSite"));
                LstSite.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateAMTEquipment(string siteID, string fleetID)
        {
            try
            {
                LstEquipement.DataSource = Developper.Employee.Get_AMT_Equipment(siteID, fleetID);
                LstEquipement.DataValueField = "Equipment";
                LstEquipement.DataTextField = "Equipment";
                LstEquipement.DataBind();
                LstEquipement.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAMTEquipment"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateAMTFleet(string fleetID)
        {
            try
            {
                LstFleet.DataSource = Developper.Employee.Get_AMT_Fleet(fleetID);
                LstFleet.DataValueField = "Fleet";
                LstFleet.DataTextField = "Fleet";
                LstFleet.DataBind();
                LstFleet.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllAMTEFleet"));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void populateUsers() {

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
        public void populateRole() {

            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "EWRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));

        }




        public void populateStatus()
        {

            dllStatus.DataSource = db.Crud_EWORequest_Status (null, "", "", "", "", "", "", "", "", "", "", "SELECT","");

            dllStatus.DataTextField = "StatusDescription";
            dllStatus.DataValueField = "StatusId";
            dllStatus.DataBind();
            dllStatus.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));

        }






        public void PopulateWorkType()
        {
            try
            {
                LstWorkType.DataSource = db.Get_EWRequest_WorkType();
                LstWorkType.DataValueField = "WorkTypeID";
                LstWorkType.DataTextField = "WorkTypeDescription";
                LstWorkType.DataBind();
                LstWorkType.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllWorkType"));

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void PopulateTask(int WorkTypeID)
        {
            try
            {
                if (LstWorkType.SelectedIndex != 0)
                {
                    LstTask.DataSource = db.Get_EWRequest_Task(WorkTypeID);
                    LstTask.DataValueField = "TaskId";
                    LstTask.DataTextField = "TaskDescription";
                    LstTask.DataBind();
                    LstTask.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTask"));
                }

            }
            catch (Exception ex)
            {

                throw;
            }
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

        public void disableControl(bool statut)
        {
            LstDepartment.Enabled = statut;
            LstRequestor.Enabled = statut;
            LstSite.Enabled = statut;
            LstFleet.Enabled = statut;
            txtRequestDescription.Enabled = statut;
            txtJustificationWhy.Enabled = statut;
            LstCategory.Enabled = statut;
            LstPriority.Enabled = statut;
            txtPlannedStartDate.Enabled = statut;
            txtDesignedExisting.Enabled = statut;
            LstTask.Enabled = statut;
            LstWorkType.Enabled = statut;
           
        }

        protected void LstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, LstDepartment.SelectedValue).ToList();
            LstRequestor.DataTextField = "FirstName";
            LstRequestor.DataValueField = "Login";
            LstRequestor.DataBind();
            LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));
        }

        protected void LstSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAMTFleet(LstSite.SelectedValue);
        }

        protected void LstFleet_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAMTEquipment(LstSite.SelectedValue, LstFleet.SelectedValue);
        }

        protected void LstTechnicalDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstTechnicalDepartment.SelectedIndex != 0)
            {
                int techDep = int.Parse(LstTechnicalDepartment.SelectedValue);
                LstTechnicalManager.DataSource = db.Get_EWRequest_Manager_ByTechnicalDepartment(techDep);
                LstTechnicalManager.DataTextField = "Name";
                LstTechnicalManager.DataValueField = "Name";
                LstTechnicalManager.DataBind();
                LstTechnicalManager.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTechnicalManager"));
            }
        }
        public void PopulateTechnicalDepartment()
        {
            LstTechnicalDepartment.DataSource = db.Get_EWRequest_TechnicalDepartment(CachedLogin);
            LstTechnicalDepartment.DataTextField = "TechnicalDepDescription";
            LstTechnicalDepartment.DataValueField = "TechincalDepId";
            LstTechnicalDepartment.DataBind();
            LstTechnicalDepartment.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTechnicalDepartment"));
        }

        protected void btnSaveEstimation_Click(object sender, EventArgs e)
        {
            if (dgvEstimation.Rows.Count == 0)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "DgvEstimationEmptyError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            else
            {
                Page.SetFocus(txtComment.ClientID);

                litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btnSaveEstimation");
                btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btnSaveEstimation");

              
                this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                //updatePanelPopUpPlan.Update();
            }
        }
        protected void btnSaveEstimationDetail_Click(object sender, EventArgs e)
        {
            //try
            //{
                if (Request.Params["EWRequestID"] != null)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    if (!string.IsNullOrWhiteSpace(txtComment.Text))
                    {
                        int id = int.Parse(Request.Params["EWRequestID"]);
                        var ewr = db.Get_EWRequest_ByID(id, CachedLogin);
                        var ewrRequest = ewr.FirstOrDefault();
                        if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Return_Text"))
                        {
                            db.Return_EWRequest(id, txtComment.Text, CachedLogin);
                        }
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Reject_Text"))
                        {
                            db.Reject_EWRequest(id, txtComment.Text, CachedLogin);
                        }
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Text"))
                        {
                            db.Approve_EWRequest(id, txtComment.Text, CachedLogin);
                        }
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Budget"))
                        {
                            db.Approve_EWRequest_Budget(id, txtComment.Text, true, CachedLogin);
                        }
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Reject_Budget"))
                        {
                            db.Approve_EWRequest_Budget(id, txtComment.Text, false, CachedLogin);
                        }
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Estimation_Text"))
                        {
                            db.Approve_EWRequest_ByTechnicalManager(id, txtComment.Text, txtTechnicalPlanner.Text, ewrRequest.IsBudged.Value, CachedLogin);
                        }
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btnBackToPrevious"))
                        {
                            db.Back_EWRequest(id, txtComment.Text, CachedLogin);
                        }
                        else if (litModalHeader.Text == btnCallBack.Text)
                        {
                            db.Back_EWRequest(id, txtComment.Text, CachedLogin);
                        }
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btnConfirmedSatisfaction"))
                        {
                            db.Approve_EWRequest_Satisfaction(id, txtComment.Text, true, CachedLogin);
                        }
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btnRejectSatisfaction"))
                        {
                            db.Approve_EWRequest_Satisfaction(id, txtComment.Text, false, CachedLogin);
                        }
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btnSaveEstimation"))
                        {
                            db.Delete_EWRequest_Estimation(id);
                            foreach (GridViewRow row in dgvEstimation.Rows)
                            {
                                Label description = (Label)row.FindControl("lblDescription");
                                Label quantite = (Label)row.FindControl("lblQuantite");
                                Label unitCost = (Label)row.FindControl("lblUnitCost");
                                Label unitMesure = (Label)row.FindControl("lblUnitMesure");
                                //NumberFormatInfo numInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
                                //db.Insert_EWRequest_Estimation(id, description.Text, decimal.Parse(quantite.Text, numInfo), decimal.Parse(unitCost.Text, numInfo), unitMesure.Text, 0);

                                db.Insert_EWRequest_Estimation(id, description.Text, decimal.Parse(quantite.Text), decimal.Parse(unitCost.Text), unitMesure.Text, 0);
                            }
                            db.Approve_EWRequest_ByTechnicalPlanner(id, txtComment.Text, CachedLogin);
                        }
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                        //    //Response.Redirect(Request.RawUrl);
                        //    //Response.Redirect("Default_View_Approve.aspx");

                        //    ModalPopupExtender1.Show();
                        //}
                        else if (litModalHeader.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Update_Text"))
                        {


                            //int id = int.Parse(Request.Params["EWRequestID"]);
                            if (ValidateInputs())
                            {
                                if (Version == 1)
                                {
                                    db.Update_EWRequest(id, LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtRequestDescription.Text, txtDesignedExisting.Text, txtScopeOfWork.Text, txtDesignedExisting.Text, LstSite.SelectedValue, LstFleet.SelectedValue, LstEquipement.SelectedValue, DateTime.Parse(txtPlannedStartDate.Text), LstDepartment.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), CachedLogin);
                                }
                                else if (Version == 2)
                                {
                                    db.Update_EWRequest_I(id, LstRequestor.SelectedValue, LstRequestor.SelectedItem.Text, txtRequestDescription.Text, txtDesignedExisting.Text, txtScopeOfWork.Text, txtDesignedExisting.Text, LstSite.SelectedValue, LstFleet.SelectedValue, LstEquipement.SelectedValue, (DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture)), LstDepartment.SelectedValue, int.Parse(LstPriority.SelectedValue), int.Parse(LstCategory.SelectedValue), CachedLogin, int.Parse(LstTask.SelectedValue), txtComment.Text);
                                }

                                //Response.Redirect("Default_View_Approve.aspx");


                                //ModalPopupExtender1.Show();
                            }

                        }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModal();", true);
                    //Response.Redirect(Request.RawUrl);
                    //Response.Redirect("Default_View_Approve.aspx");

                    ModalPopupExtender1.Show();

                }
                else
                    {
                        lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                    }
                 
                }
            //}
            //catch (FormatException ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}
            //catch (NullReferenceException ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}
            //catch (IndexOutOfRangeException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}
            //catch (ArithmeticException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}

            //catch (TimeoutException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}

            //catch (SystemException ex)
            //{
            //    string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}

            //catch (Exception ex)
            //{

            //    string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
            //    lblErreur.Text = error;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            //}
        }
        protected void btnApproval_Click(object sender, EventArgs e)
        {
            if (Request.Params["EWRequestID"] != null)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                if (btnApproval.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Forward_Text"))
                {
                    PopulateTechnicalDepartment();
                    LstTechnicalDepartment_SelectedIndexChanged(sender, e);
                    txtCommentDispacher.Text = string.Empty;
                    Page.SetFocus(txtCommentDispacher.ClientID);
                    this.Form.DefaultButton = this.btnForward.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDispatcher();", true);
                }
                else if (btnApproval.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Assign_Text"))
                {
                    int id = int.Parse(Request.Params["EWRequestID"]);
                    var ewr = db.Get_EWRequest_ByID(id, CachedLogin);
                    var ewrRequest = ewr.FirstOrDefault();
                    PopulatePlanner(ewrRequest.TechincalDepId.Value);
                    txtCommentManager.Text = string.Empty;
                    Page.SetFocus(txtCommentManager.ClientID);
                    this.Form.DefaultButton = this.btnAssign.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalManager();", true);
                }
                else if(btnApproval.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Text"))
                {
                    litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Text");
                    btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Text");
                    txtComment.Text = string.Empty;
                    Page.SetFocus(txtComment.ClientID);
                    this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if(btnApproval.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Budget"))
                {
                    litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Budget");
                    btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Budget");
                    txtComment.Text = string.Empty;
                    Page.SetFocus(txtComment.ClientID);
                    this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (btnApproval.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Estimation_Text"))
                {
                    litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Estimation_Text");
                    btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Approve_Estimation_Text");
                    txtComment.Text = string.Empty;
                    Page.SetFocus(txtComment.ClientID);
                    this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (btnApproval.Text == (string)GetGlobalResourceObject("LocalizedText", "btnConfirmedSatisfaction"))
                {
                    litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btnConfirmedSatisfaction");
                    btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btnConfirmedSatisfaction");
                    txtComment.Text = string.Empty;
                    Page.SetFocus(txtComment.ClientID);
                    this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (btnApproval.Text == (string)GetGlobalResourceObject("LocalizedText", "btnRejectSatisfaction"))
                {
                    litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btnRejectSatisfaction");
                    btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btnRejectSatisfaction");
                    txtComment.Text = string.Empty;
                    Page.SetFocus(txtComment.ClientID);
                    this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }

              
            }
            updatePanelPopUpPlan.Update();
            updatePanelDispatch.Update();
            updatePanelManager.Update();
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.Params["EWRequestID"] != null)
                {
                    int id = int.Parse(Request.Params["EWRequestID"]);
                    if ((!string.IsNullOrEmpty(txtCommentManager.Text)) && (LstTechicalPlannerName.SelectedIndex != 0))
                    {
                        //bool budget = false;
                        //if (LstBudgetHr.SelectedIndex == 1) budget = true;
                        db.Approve_EWRequest_ByTechnicalManager(id, txtCommentManager.Text, LstTechicalPlannerName.SelectedValue,true, CachedLogin);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModalManager();", true);
                        //Response.Redirect(Request.RawUrl);
                        //Response.Redirect("Default_View_Approve.aspx");

                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "popUpInvalidInput");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
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
        public void PopulatePlanner(int department)
        {
            LstTechicalPlannerName.DataSource = db.Get_EWRequest_Planner_ByTechnicalDepartment(department);
            LstTechicalPlannerName.DataTextField = "Name";
            LstTechicalPlannerName.DataValueField = "Name";
            LstTechicalPlannerName.DataBind();
            LstTechicalPlannerName.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllTechnicalPlanner"));
        }
        protected void btnForward_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.Params["EWRequestID"] != null)
                {
                    int id = int.Parse(Request.Params["EWRequestID"]);
                    if ((!string.IsNullOrEmpty(txtCommentDispacher.Text)) && (LstEquipement.SelectedIndex != 0) && (LstTechnicalManager.SelectedIndex != 0) && (LstTechnicalDepartment.SelectedIndex != 0))
                    {
                        db.Approve_EWRequest_ByDispatcher(id, txtCommentDispacher.Text, LstEquipement.SelectedValue, int.Parse(LstTechnicalDepartment.SelectedValue), LstTechnicalManager.SelectedValue, CachedLogin);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "hideModalDispatcher();", true);
                        //Response.Redirect(Request.RawUrl);
                        //Response.Redirect("Default_View_Approve.aspx");

                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "popUpInvalidInput");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
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

        protected void BtnReject_Click(object sender, EventArgs e)
        {
            if (Request.Params["EWRequestID"] != null)
            {
                if (BtnReject.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Reject_Text"))
                {
                    litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Reject_Text");
                    btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Reject_Text");
                    txtComment.Text = string.Empty;
                    Page.SetFocus(txtComment.ClientID);
                    this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (BtnReject.Text == (string)GetGlobalResourceObject("LocalizedText", "btn_Reject_Budget"))
                {
                    litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Reject_Budget");
                    btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Reject_Budget");
                    txtComment.Text = string.Empty;
                    Page.SetFocus(txtComment.ClientID);
                    this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                updatePanelPopUpPlan.Update();
            }
        }
        protected void BtnReturnToOrginator_Click(object sender, EventArgs e)
        {
            if (Request.Params["EWRequestID"] != null)
            {
                litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Return_Text");
                btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btn_Return_Text");
                txtComment.Text = string.Empty;
                Page.SetFocus(txtComment.ClientID);
                this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            updatePanelPopUpPlan.Update();
        }

        protected void btnBackToOriginator_Click(object sender, EventArgs e)
        {
            if (Request.Params["EWRequestID"] != null)
            {
                litModalHeader.Text = (string)GetGlobalResourceObject("LocalizedText", "btnBackToPrevious");
                btnSaveEstimationDetail.Text = (string)GetGlobalResourceObject("LocalizedText", "btnBackToPrevious");
                Page.SetFocus(txtComment.ClientID);
                this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            updatePanelPopUpPlan.Update();
        }

        protected void dllUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string employedID = db.Get_Employee_User().Where(x => x.Login == dllUsers.SelectedValue).FirstOrDefault().EmployeeId;


            dllRole.DataSource = db.Get_Role_ByUser("KCC Request", dllUsers.SelectedValue, "EWRequest");
            dllRole.DataTextField = "Role";
            dllRole.DataValueField = "Role";
            dllRole.DataBind();
            dllRole.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllrole"));
            dllRole.Items.Insert(1, employedID);
        }

        protected void btnrRealocate_Click(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.BtnSaveReallocate.UniqueID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openReallocate();", true);
        }

        protected void BtnSaveReallocate_Click(object sender, EventArgs e)
        {
            try
            {

                if (dllUsers.SelectedIndex== 0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllUsersError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }

                else if (dllRole.SelectedIndex==0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllRoleError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }


                else if (dllStatus.SelectedIndex == 0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllStatusError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }


                else if (txtCommentReallocate.Text == string.Empty)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {

                
                           
          db.EWRequest_Reallocate(int.Parse(Request.Params["EWRequestID"]), txtCommentReallocate.Text, dllRole.SelectedValue, Convert.ToInt32(dllStatus.SelectedValue), CachedLogin);




                    //Response.Redirect("Admin/EW_Request/EWRequestRequisition.aspx");

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

        protected void btnAdHoc_Click(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnAdHocSav.UniqueID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openAdHoc();", true);
        }

        protected void btnAdHocSav_Click(object sender, EventArgs e)
        {
            try
            {

                if (dllUserAdHoc.SelectedIndex==0)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "dllUsersError");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }


                else if (txtCommentAdHoc.Text == string.Empty)
                {

                    lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "lblCommentEmptyError");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                }
                else
                {
                    db.EWRequest_AdHoc(int.Parse(Request.Params["EWRequestID"]), dllUserAdHoc.SelectedValue, txtCommentAdHoc.Text, CachedLogin);


                    //Response.Redirect("Default_View_Approve.aspx");

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
            Response.Redirect("ViewReports.aspx?IDEWORequest=" + Request.Params["EWRequestID"]);
        }

        protected void btnCallBack_Click(object sender, EventArgs e)
        {
            if (Request.Params["EWRequestID"] != null)
            {
                litModalHeader.Text = btnCallBack.Text;
                btnSaveEstimationDetail.Text = btnCallBack.Text;
                Page.SetFocus(txtComment.ClientID);
                this.Form.DefaultButton = this.btnSaveEstimationDetail.UniqueID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            updatePanelPopUpPlan.Update();
        }


        protected void btnOk_Click(object sender, EventArgs e)
        {
            //Response.Redirect("default.aspx");

          
            Response.Redirect("My_RequestToAproval.aspx?type=EWRequest");

        }

        protected void LstWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstWorkType.SelectedIndex != 0)
            {
                PopulateTask(int.Parse(LstWorkType.SelectedValue));
            }
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
                if (LstEquipement.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstEquipmentEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstEquipmentEmptyError");
                    valid = false;

                }
                if (LstFleet.SelectedIndex == 0)
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstFleetEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstFleetEmptyError");
                    valid = false;

                }
                if (LstSite.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstSiteEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstSiteEmptyError");
                    valid = false;
                }
                if (Version == 2)
                {

                    if (LstWorkType.SelectedIndex == 0)
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstWorkTypeEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstWorkTypeEmptyError");
                        valid = false;
                    }
                    if (LstTask.SelectedIndex == 0)
                    {
                        error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstTaskEmptyError") : error + "<br />" + (string)GetGlobalResourceObject("LocalizedText", "lstTaskEmptyError");
                        valid = false;
                    }
                }
                if (LstCategory.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstCategoryEmptyError");
                    valid = false;
                }
                if (LstPriority.SelectedIndex == 0)
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "lstPriorityEmptyError");
                    valid = false;
                }
                if (string.IsNullOrEmpty(txtJustificationWhy.Text) || string.IsNullOrWhiteSpace(txtJustificationWhy.Text))
                {
                    //
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtJustificationWhyEmptyError");
                    valid = false;
                }
                if (string.IsNullOrEmpty(txtRequestDescription.Text) || string.IsNullOrWhiteSpace(txtRequestDescription.Text))
                {
                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtRequestDescriptionEmptyError");
                    valid = false;
                }
                if (string.IsNullOrEmpty(txtScopeOfWork.Text) || string.IsNullOrWhiteSpace(txtScopeOfWork.Text))
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtScopeOfWorkEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtScopeOfWorkEmptyError");
                    valid = false;

                }

                if (string.IsNullOrEmpty(txtDesignedExisting.Text) || string.IsNullOrWhiteSpace(txtDesignedExisting.Text))
                {

                    error = (string.IsNullOrEmpty(error)) ? (string)GetGlobalResourceObject("LocalizedText", "txtDesignedExistingEmptyError") : error + "<br /> " + (string)GetGlobalResourceObject("LocalizedText", "txtDesignedExistingEmptyError");
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
                int id = int.Parse(Request.Params["EWRequestID"]);

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

                        byte[] bytes = (byte[])db.Get_EWRRequest_File(id).Where(x => x.FileID.ToString() == fileID.Text).FirstOrDefault().File.ToArray();

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


    }
}