using KCCDBAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2
{
    public partial class New_DCRequest : BasePage
    {
        private readonly KCCDBWrapper db = new KCCDBWrapper();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    var login = CachedLogin;

                    PopulateEmployeeAndDepartment(login);
                    PopulateCategory(login);
                    PopulatePriority(login);
                    PopulateDocumentType(login);
                    PopulateDocumentLangue();
                    PopulateService(login);
                    PopulateDCRequest_Type();
                    txtDocumentNumber.ReadOnly = true;

                    PopulateApplicabilityAreas();
                    PopulateApplicabilityReviewGroups();
                    PopulateApplicabilityRolloutChannels();

                    CheckBoxListApprovalReviewers.Enabled = false;

                    // Service: non-admin => force Service=1 and lock dropdown
                    var adminEntry = db.Get_KCCRequestIsAdmin(login)
                                       .FirstOrDefault(x => x.ModuleID == "DCRequest");
                    bool isAdmin = (adminEntry?.Number ?? 0) == 1;

                    if (!isAdmin)
                    {
                        if (ddlService.Items.FindByValue("1") != null)
                            ddlService.SelectedValue = "1";
                        ddlService.Enabled = false;
                    }
                    else
                    {
                        ddlService.Enabled = true;
                    }

                    TrAreas.Visible = false;
                    TrReviewers.Visible = false;

                    VerifyApplicability();
                    toggleVisibilityBasedOnApplicability();
                }
            }
            catch
            {
                Response.Redirect("pageError.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void toggleVisibilityBasedOnApplicability()
        {
            var val = ddlSetApplicability.SelectedValue ?? "N/A";
            bool yes = string.Equals(val, "YES", StringComparison.OrdinalIgnoreCase);
            TrAreas.Visible = yes;
            TrReviewers.Visible = yes;
        }

        private void VerifyApplicability()
        {
            if (dllDepartment.SelectedValue == null)
                return;

            int applicability = db.Get_DCRequest_ApplicabilityForDepartment(dllDepartment.SelectedValue);

            if (applicability == 1)
            {
                ddlSetApplicability.Enabled = false;
                ddlSetApplicability.SelectedValue = "YES";
            }
            else
            {
                ddlSetApplicability.Enabled = true;
                ddlSetApplicability.SelectedValue = "N/A";
            }
        }

        public void PopulateApplicabilityReviewGroups()
        {
            var data = db.Get_DCRequest_ApplicabilityReviewGroups().ToList();
            CheckBoxListApprovalReviewers.DataSource = data;
            CheckBoxListApprovalReviewers.DataTextField = "Description";
            CheckBoxListApprovalReviewers.DataValueField = "ID";
            CheckBoxListApprovalReviewers.DataBind();

            if (CheckBoxListApprovalReviewers.Items.Count > 0)
                CheckBoxListApprovalReviewers.SelectedIndex = 0;
        }

        public void PopulateApplicabilityRolloutChannels()
        {
            var items = db.Get_DCRequest_RolloutChannel().ToList();

            // Avoid HTML in ListItem text (CheckBoxList encodes text).
            var transformed = items.Select(i => new
            {
                i.ID,
                DisplayText = string.IsNullOrEmpty(i.Responsible)
                    ? i.Channel
                    : $"{i.Channel} - {i.Responsible}"
            }).ToList();

            CheckBoxListRolloutChannels.DataSource = transformed;
            CheckBoxListRolloutChannels.DataTextField = "DisplayText";
            CheckBoxListRolloutChannels.DataValueField = "ID";
            CheckBoxListRolloutChannels.DataBind();

            // Document control checked and disabled by default
            if (CheckBoxListRolloutChannels.Items.Count > 0)
            {
                CheckBoxListRolloutChannels.Items[0].Selected = true;
                CheckBoxListRolloutChannels.Items[0].Enabled = false;
            }
        }

        public void PopulateApplicabilityAreas()
        {
            var data = db.Get_DCRequest_ApplicabilityAreas().ToList();
            CheckBoxListApplicableAreas.DataSource = data;
            CheckBoxListApplicableAreas.DataTextField = "Description";
            CheckBoxListApplicableAreas.DataValueField = "ID";
            CheckBoxListApplicableAreas.DataBind();

            // If you rely on item[0] = "ALL", guard the index.
            if (CheckBoxListApplicableAreas.Items.Count > 0)
            {
                CheckBoxListApplicableAreas.Items[0]
                    .Attributes.Add("onclick", "toggleAllCheckboxes(this);");
            }
        }

        public void PopulateEmployeeAndDepartment(string login)
        {
            // Employee type: 1 => restricted to own dept; else => all depts
            int empType = 0;
            int.TryParse(Developper.Employee.Get_Employee_Type(login), out empType);

            if (empType == 1)
            {
                var list = Developper.Employee.Get_Employee_Of_Department(login, "").ToList();

                LstRequestor.DataSource = list;
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));

                dllDepartment.DataSource = list.GroupBy(x => x.DepartmentCode).Select(g => g.First());
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

            var adminEntry = db.Get_KCCRequestIsAdmin(login)
                               .FirstOrDefault(x => x.ModuleID == "DCRequest");
            bool isAdmin = (adminEntry?.Number ?? 0) == 1;

            if (isAdmin)
            {
                dllDepartment.DataSource = Developper.Employee.Get_Department().ToList();
                dllDepartment.DataTextField = "LongDescription";
                dllDepartment.DataValueField = "Code";
                dllDepartment.DataBind();
                dllDepartment.Enabled = true;

                var emp = Developper.Employee.Get_Employee_Of_Department(login, "").FirstOrDefault();
                if (emp != null && dllDepartment.Items.FindByValue(emp.DepartmentCode) != null)
                    dllDepartment.SelectedValue = emp.DepartmentCode;

                LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
                LstRequestor.DataTextField = "FirstName";
                LstRequestor.DataValueField = "Login";
                LstRequestor.DataBind();
                LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllRequestor"));
            }
        }

        public void PopulateCategory(string login)
        {
            LstCategory.DataSource = db.Get_KCCRequest_Category(login);
            LstCategory.DataValueField = "CategoryID";
            LstCategory.DataTextField = "CategoryDescprition";
            LstCategory.DataBind();
            LstCategory.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllcategorie"));
        }

        public void PopulateDCRequest_Type()
        {
            ddlRequestType.DataSource = db.Get_DCRequest_Type().ToList();
            ddlRequestType.DataValueField = "TypeId";
            ddlRequestType.DataTextField = "TypeDescription";
            ddlRequestType.DataBind();
            ddlRequestType.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDCRType"));
        }

        public void PopulateService(string login)
        {
            ddlService.DataSource = db.Get_DCRequest_Service(login).ToList();
            ddlService.DataValueField = "ServiceID";
            ddlService.DataTextField = "ServiceDescription";
            ddlService.DataBind();
            ddlService.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllService"));
        }

        public void PopulatePriority(string login)
        {
            LstPriority.DataSource = db.Get_KCCRequest_Priority(login);
            LstPriority.DataValueField = "PrioriteID";
            LstPriority.DataTextField = "PrioriteDescprition";
            LstPriority.DataBind();
            LstPriority.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllpriority"));
        }

        public void PopulateDocumentType(string login)
        {
            LstDocumentType.DataSource = db.Get_DCRequest_DocumentType(login);
            LstDocumentType.DataValueField = "DocTypeId";
            LstDocumentType.DataTextField = "DocTypeDescription";
            LstDocumentType.DataBind();
            LstDocumentType.Items.Insert(0, "N/A");
        }

        public void PopulateDocumentLangue()
        {
            LstDocumentLangueID.DataSource = db.Get_KCCRequest_Lang();
            LstDocumentLangueID.DataValueField = "LangId";
            LstDocumentLangueID.DataTextField = "LangDescription";
            LstDocumentLangueID.DataBind();
            LstDocumentLangueID.Items.Insert(0, "N/A");
        }

        public bool ValidInputs()
        {
            bool isValid = true;
            var sb = new StringBuilder();

            void AddErr(string key)
            {
                var msg = (string)GetGlobalResourceObject("LocalizedText", key);
                if (sb.Length > 0) sb.AppendLine("<br /> ");
                sb.Append(msg);
                isValid = false;
            }

            if (LstRequestor.SelectedIndex <= 0) AddErr("lstRequestorEmptyError");
            if (string.IsNullOrWhiteSpace(txtBeneficiary.Text)) AddErr("txtBeneficiaryEmptyError");
            if (string.IsNullOrWhiteSpace(txtBeneficiaryJobTitle.Text)) AddErr("txtBeneficiaryJobTitleEmptyError");
            if (string.IsNullOrWhiteSpace(txtRequestDescription.Text)) AddErr("txtRequestDescriptionEmptyError");
            if (string.IsNullOrWhiteSpace(txtJustificationWhy.Text)) AddErr("txtJustificationWhyEmptyError");
            if (LstCategory.SelectedIndex <= 0) AddErr("lstCategoryEmptyError");
            if (LstPriority.SelectedIndex <= 0) AddErr("lstPriorityEmptyError");
            if (ddlService.SelectedIndex <= 0) AddErr("lstServiceSecEmptyError");

            // Date validation (MM-dd-yyyy invariant)
            if (string.IsNullOrWhiteSpace(txtPlannedStartDate.Text))
            {
                AddErr("txtPlannedStartDateEmptyError");
            }
            else
            {
                if (!DateTime.TryParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture,
                                            DateTimeStyles.None, out var startDate))
                {
                    AddErr("FormatExceptionError");
                }
                else if (startDate <= DateTime.Today.AddDays(-1))
                {
                    AddErr("txtPlannedStartDateLess");
                }
            }

            // Applicability
            var applicability = ddlSetApplicability.SelectedValue ?? "N/A";
            if (applicability == "N/A") AddErr("ddlSetApplicabilityError");

            if (applicability == "YES")
            {
                bool anyAreas = CheckBoxListApplicableAreas.Items.Cast<ListItem>().Any(i => i.Selected);
                bool anyReviewers = CheckBoxListApprovalReviewers.Items.Cast<ListItem>().Any(i => i.Selected);
                bool anyChannels = CheckBoxListRolloutChannels.Items.Cast<ListItem>().Any(i => i.Selected);

                if (!anyAreas) AddErr("CheckBoxListApplicableAreasError");
                if (!anyReviewers) AddErr("CheckBoxListApprovalReviewersError");
                if (!anyChannels) AddErr("CheckBoxListRolloutChannelsError");
            }

            if (string.IsNullOrWhiteSpace(txtRisk.Text)) AddErr("txtRiskEmptyError");

            if (RequestFiles() == 0) AddErr("FileAttachesEmptyError");

            if (!isValid)
            {
                lblErreur.Text = sb.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            return isValid;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidInputs()) return;

                int? docTypeId = null, docLangId = null;
                if (LstDocumentType.SelectedIndex > 0) docTypeId = int.Parse(LstDocumentType.SelectedValue);
                if (LstDocumentLangueID.SelectedIndex > 0) docLangId = int.Parse(LstDocumentLangueID.SelectedValue);

                bool isApplicable = string.Equals(ddlSetApplicability.SelectedValue, "YES", StringComparison.OrdinalIgnoreCase);

                var plannedStart = DateTime.ParseExact(txtPlannedStartDate.Text, "MM-dd-yyyy", CultureInfo.InvariantCulture);

                decimal id = db.DCRequest_Insert(
                    LstRequestor.SelectedValue,
                    txtBeneficiary.Text,
                    txtBeneficiaryJobTitle.Text,
                    txtRequestDescription.Text,
                    null,
                    txtUrgencyComment.Text,
                    plannedStart,
                    txtRisk.Text,
                    int.Parse(LstPriority.SelectedValue),
                    int.Parse(LstCategory.SelectedValue),
                    txtDocumentNumber.Text,
                    txtDocumentVersionCode.Text,
                    docTypeId,
                    txtDocumentTitle.Text,
                    docLangId,
                    null, null, null, null, null, null, null, null,
                    int.Parse(ddlService.SelectedValue),
                    CachedLogin,
                    int.Parse(ddlRequestType.SelectedValue),
                    isApplicable
                );

                lblIDRequest.Text = id.ToString();

                Insert_DCRequest_Files((int)id);

                // Rollout channels
                var rolloutChannelIds = string.Join(",",
                    CheckBoxListRolloutChannels.Items.Cast<ListItem>()
                        .Where(i => i.Selected)
                        .Select(i => i.Value));

                string areaIds = string.Empty;
                string reviewerIds = string.Empty;

                if (isApplicable)
                {
                    areaIds = string.Join(",",
                        CheckBoxListApplicableAreas.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value));

                    reviewerIds = string.Join(",",
                        CheckBoxListApprovalReviewers.Items.Cast<ListItem>()
                            .Where(i => i.Selected)
                            .Select(i => i.Value));
                }

                db.Insert_DCRequest_ApplicabilityDetails((int)id, areaIds, reviewerIds, rolloutChannelIds);

                // db.DCRequest_SendMail((int)id);
                lblSuccess.Text = (string)GetGlobalResourceObject("LocalizedText", "lblSuccess");
                lblIDRequest.Text = id.ToString();
                ModalPopupExtender1.Show();
            }
            catch (EntityCommandExecutionException ex)
            {
                string idReq = lblIDRequest.Text;
                string url = "Detail_DCRequest.aspx?DCrequestID=" + idReq;
                ((NewRequest)this.Master).handleSendingMailException(ex, url, idReq);
            }
            catch (FormatException)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (NullReferenceException)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (IndexOutOfRangeException)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (ArithmeticException)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (TimeoutException)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (SystemException)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Detail_DCRequest.aspx?DCrequestID=" + lblIDRequest.Text, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void dllDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LstRequestor.DataSource = Developper.Employee.Get_Employee_Of_Department(null, dllDepartment.SelectedValue).ToList();
            LstRequestor.DataTextField = "FirstName";
            LstRequestor.DataValueField = "Login";
            LstRequestor.DataBind();
            LstRequestor.Items.Insert(0, (string)GetGlobalResourceObject("LocalizedText", "selectdllDepartement"));

            VerifyApplicability();
            toggleVisibilityBasedOnApplicability();
        }

        public void Insert_DCRequest_Files(int id)
        {
            try
            {
                var files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    var f = files[i];
                    if (f?.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(f.FileName);
                        var contentType = f.ContentType;

                        // Read exactly ContentLength bytes
                        byte[] bytes = new byte[f.ContentLength];
                        using (var br = new BinaryReader(f.InputStream))
                        {
                            int read = br.Read(bytes, 0, bytes.Length);
                            if (read != bytes.Length)
                            {
                                // fallback: stream may not support length; use MemoryStream
                                using (var ms = new MemoryStream())
                                {
                                    f.InputStream.Position = 0;
                                    f.InputStream.CopyTo(ms);
                                    bytes = ms.ToArray();
                                }
                            }
                        }

                        db.DCRequest_Insert_File(id, fileName, contentType, bytes);
                    }
                }
            }
            catch (Exception)
            {
                lblErreur.Text = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
        }

        protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            doclink.Visible = ddlService.SelectedValue == "2";
        }

        public int RequestFiles()
        {
            int n = 0;
            var files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                var f = files[i];
                if (f?.ContentLength > 0) n++;
            }
            return n;
        }

        protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRequestType.SelectedValue == "2")
            {
                txtDocumentNumber.ReadOnly = false;
                FileMessageLbl.Text = (string)GetGlobalResourceObject("LocalizedText", "FileMessageLbl");
            }
            else
            {
                txtDocumentNumber.ReadOnly = true; // ← fix: was false in your code
                FileMessageLbl.Text = string.Empty;
            }
        }

        protected void CheckBoxListApplicableAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckBoxListApplicableAreas.Items.Count == 0) return;
            bool all = CheckBoxListApplicableAreas.Items[0].Selected;
            for (int i = 1; i < CheckBoxListApplicableAreas.Items.Count; i++)
                CheckBoxListApplicableAreas.Items[i].Selected = all;
        }

        protected void ddlSetApplicability_SelectedIndexChanged(object sender, EventArgs e)
        {
            toggleVisibilityBasedOnApplicability();
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkSelectAll.Checked;
            foreach (ListItem item in CheckBoxListApplicableAreas.Items)
                item.Selected = isChecked;

            chkSelectAll.Text = isChecked ? "Deselect All" : "Select All";
        }
    }
}
