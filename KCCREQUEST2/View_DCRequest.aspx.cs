using System;
using KCCDBAccess;
using System.Linq;

namespace KCCREQUEST2
{
    public partial class View_DCRequest : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = 0;
                if (Request.Params["DCrequestID"] != null)
                {
                    int.TryParse(Request.Params["DCrequestID"], out id);
                }
                if (id > 0)
                {
                    LoadDCRequestView(id);
                }
            }
        }

        private void LoadDCRequestView(int id)
        {
            try
            {
                // PERFORMANCE: Single query to get main request data - no additional lookups!
                var request = db.Get_DCRequest_ById(id, CachedLogin)?.FirstOrDefault();
                if (request == null) return;

                // Basic request information - only populate fields that definitely exist
                if (txtDepartment != null) txtDepartment.Text = request.Department ?? "";
                if (txtRequestor != null) txtRequestor.Text = request.RequestorLogin ?? "";
                if (txtBeneficiary != null) txtBeneficiary.Text = request.Beneficiary ?? "";
                if (txtBeneficiaryJobTitle != null) txtBeneficiaryJobTitle.Text = request.BeneficiaryJobTitle ?? "";
                if (txtRequestDescription != null) txtRequestDescription.Text = request.Description ?? "";
                
                // Service and Type - show IDs for now (can be enhanced later)
                if (txtRequestType != null) txtRequestType.Text = request.TypeId?.ToString() ?? "";
                if (txtService != null) txtService.Text = request.ServiceID?.ToString() ?? "";
                
                // Document details - basic fields only
                if (txtDocumentTitle != null) txtDocumentTitle.Text = request.DocTitle ?? "";
                if (txtDocumentLangueID != null) txtDocumentLangueID.Text = request.LangId?.ToString() ?? "";
                if (txtDocumentType != null) txtDocumentType.Text = request.DocTypeId?.ToString() ?? "";
                if (txtDocumentVersionCode != null) txtDocumentVersionCode.Text = request.DocVersionCode ?? "";

                // Additional fields available from the request object
                if (txtDocumentNumber != null) txtDocumentNumber.Text = request.DocNumber ?? "";
                if (txtCategory != null) txtCategory.Text = request.CategoryID?.ToString() ?? "";
                if (txtPriority != null) txtPriority.Text = request.PrioriteID?.ToString() ?? "";
                if (txtCostCenter != null) txtCostCenter.Text = request.CostCenter ?? "";
                if (txtUrgencyComment != null) txtUrgencyComment.Text = request.CommentPriority ?? "";

                // Date fields with safe parsing
                if (txtPlannedStartDate != null && request.PlannedDeliveryDate != null)
                    txtPlannedStartDate.Text = DateTime.Parse(request.PlannedDeliveryDate.ToString()).ToString("dd/MM/yyyy");

                // More document fields
                if (txtArea != null) txtArea.Text = request.Area ?? "";
                if (txtRisk != null) txtRisk.Text = request.Risk ?? "";
                if (txtDocVersion != null) txtDocVersion.Text = request.DocVersion?.ToString() ?? "";

                if (txtDocumentVersionDate != null && request.DocVersionDate != null)
                    txtDocumentVersionDate.Text = DateTime.Parse(request.DocVersionDate.ToString()).ToString("dd/MM/yyyy");

                if (txtDocTranslator != null) txtDocTranslator.Text = request.DocTranslator ?? "";

                if (txtDocReviewVersionDate != null && request.DocReviewDate != null)
                    txtDocReviewVersionDate.Text = DateTime.Parse(request.DocReviewDate.ToString()).ToString("dd/MM/yyyy");

                if (txtDocHardCopy != null) txtDocHardCopy.Text = (request.DocHardcopy == true) ? "Yes" : "No";
                if (txtDocLink != null) txtDocLink.Text = request.DocLink ?? "";
                if (txtDocTransmittalNo != null) txtDocTransmittalNo.Text = request.DocTransmittalNo?.ToString() ?? "";
                if (txtAdditionalNotedId != null) txtAdditionalNotedId.Text = request.AdditionalNoteId?.ToString() ?? "";

                // Status fields
                if (txtStatut != null) txtStatut.Text = request.StatusDescription ?? "";
                if (txtCurrentUsers != null) txtCurrentUsers.Text = request.CurrentUser ?? "";

            }
            catch (Exception ex)
            {
                // Log error but don't break the page
                System.Diagnostics.Debug.WriteLine($"Error loading DC Request view: {ex.Message}");
            }
        }

        // Empty button event handlers
        protected void btnApproval_Click(object sender, EventArgs e) { }
        protected void BtnReject_Click(object sender, EventArgs e) { }
        protected void BtnReturnToOrginator_Click(object sender, EventArgs e) { }
        protected void btnBackToOriginator_Click(object sender, EventArgs e) { }
        protected void btnAdHoc_Click(object sender, EventArgs e) { }
    }
}