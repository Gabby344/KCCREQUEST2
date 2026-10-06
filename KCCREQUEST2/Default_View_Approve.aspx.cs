using KCCDBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2
{
    public partial class Default_View_Approve : BasePage
    {
        // Using inherited db from BasePage


        // Constants for access keys
        private const string SourceKey = "Source";
        private const string EmployeeIdKey = "EmployeeID_AD";
        private const string DepartmentCodeKey = "DepartmentCode_SAGE";
        private const string VIPSource = "VIP";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string error = (string)GetGlobalResourceObject("LocalizedText", "lblMessageAccessDenie") + "<br /> <br />";
                    bool isValid = true;

                    // Use cached access data for performance
                    var accessDict = GetCachedAccessData();

                    bool isVIP = accessDict.TryGetValue(SourceKey, out var src) &&
                                 string.Equals(src, VIPSource, StringComparison.OrdinalIgnoreCase);

                    bool missingEmployeeId = !accessDict.TryGetValue(EmployeeIdKey, out var emp) ||
                                             string.IsNullOrWhiteSpace(emp);

                    bool missingDeptCode = !accessDict.TryGetValue(DepartmentCodeKey, out var dept) ||
                                             string.IsNullOrWhiteSpace(dept);

                    if (isVIP || missingEmployeeId)
                    {
                        if (missingEmployeeId)
                        {
                            error += "<br />  - " + (string)GetGlobalResourceObject("LocalizedText", "lblwithoutEmployeeAD");
                            isValid = false;
                        }
                        if (missingDeptCode)
                        {
                            error += "<br /> - " + (string)GetGlobalResourceObject("LocalizedText", "lblwithoutDepartmentCode_SAGE");
                            isValid = false;
                        }

                        if (!isValid)
                        {
                            // Use the authenticated/cached login, not WindowsIdentity.GetCurrent()
                            var listDepartement = Developper.Employee.Get_Employee()
                                .Where(x => x.Sources == VIPSource && string.Equals(x.Login, CachedLogin, StringComparison.OrdinalIgnoreCase))
                                .ToList();

                            var telephone = listDepartement.FirstOrDefault()?.TelephoneNumber;

                            int incidentId = db.Insert_ITProblem(
                                "KCC Request",
                                "Access denied",
                                telephone,
                                error,
                                7, 2,
                                CachedLogin, // requester
                                null, 28, 17,
                                CachedLogin, // created by
                                1, 4);

                            string descriptionMsg =
                                (string)GetGlobalResourceObject("LocalizedText", "lblIncidentSysaid") +
                                " <span style='color:red'>" + incidentId + "</span> " +
                                (string)GetGlobalResourceObject("LocalizedText", "lblIncidentSysaid2");

                            error += "<br /><br /> " + descriptionMsg;
                            lblMessage.Text = error;
                            ModalPopupExtender1.Show();
                        }
                        else
                        {
                            if (RedirectToHomeIfNeeded()) return; // stop further processing after redirect
                        }
                    }
                    else
                    {
                        if (RedirectToHomeIfNeeded()) return;
                    }

                    // Use cached dashboard data for performance
                    var dashboardList = GetCachedApproveDashboard();
                    dgvModule.DataSource = dashboardList;
                    dgvModule.DataBind();

                }
            }
            catch (Exception ex)
            {
                lblErreur.Text = "An error occurred: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
        }
        }

        private bool RedirectToHomeIfNeeded()
        {
            // If not on dashboard and user’s preference is "My Requests", bounce there
            if (Request.Params["dashboard"] == null && HomePage == 0)
            {
                Response.Redirect("Default_View_MyRequests.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return true; // caller should return immediately
            }
            return false;
        }


        protected void Show_Hide_ChildGrid(object sender, EventArgs e)
        {
            var imgShowHide = sender as ImageButton;
            if (imgShowHide == null) return;

            var row = imgShowHide.NamingContainer as GridViewRow;
            if (row == null) return;

            var pnlAllRequest = row.FindControl("pnlAllRequest");
            if (imgShowHide.CommandArgument == "Show")
            {
                if (pnlAllRequest != null) pnlAllRequest.Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";

                string modulesID = dgvModule.DataKeys[row.RowIndex].Value.ToString();
                var dgvMyAllRequest = row.FindControl("dgvMyallrequest") as GridView;
                if (dgvMyAllRequest != null)
                {
                    dgvMyAllRequest.DataSource = db.Get_KCCRequest_ToApprove(modulesID, CachedLogin)
                        .GroupBy(x => new { x.Module, x.Id }).Select(x => x.First());
                    dgvMyAllRequest.DataBind();

                    if (modulesID == "ITRequest")
                    {
                        if (dgvMyAllRequest.Columns.Count > 10) dgvMyAllRequest.Columns[10].Visible = false;
                        if (dgvMyAllRequest.Columns.Count > 8) dgvMyAllRequest.Columns[8].Visible = false;
                    }
                }
            }
            else
            {
                if (pnlAllRequest != null) pnlAllRequest.Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }

        protected void btndetail_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as Button;
                if (btn == null) return;

                var row = btn.NamingContainer as GridViewRow;
                if (row == null) return;

                var reqId = row.FindControl("lblID") as Label;
                var module = row.FindControl("lblModule") as Label;
                if (reqId == null || module == null) return;

                RedirectToDetail(module.Text, reqId.Text);
            }
            catch (Exception ex)
            {
                lblErreur.Text = ex.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
        }

        private void RedirectToDetail(string module, string reqId)
        {
            string url = null;
            switch (module)
            {
                case "ITRequest":
                    url = $"Detail_itRequest.aspx?IDITrequest={reqId}";
                    break;
                case "EWRequest":
                    url = $"Detail_ewoRequest.aspx?EWRequestID={reqId}";
                    break;
                case "RECTRequest":
                    url = $"Detail_RecruitmentRequest.aspx?RectRequestID={reqId}";
                    break;
                case "TRNRequest":
                    url = $"Detail_trainingRequest.aspx?TrID={reqId}";
                    break;
                case "DSKRequest":
                    url = $"Detail_radiorequest.aspx?DSKRrequestID={reqId}";
                    break;
                case "ITProblem":
                    url = $"Detail_ItProblem.aspx?IDITProblem={reqId}";
                    break;
                case "TRANSRequest":
                    url = $"Detail_TRANSRequest.aspx?TRANSRequestID={reqId}";
                    break;
                case "DCRequest":
                    url = $"Detail_DCRequest.aspx?DCrequestID={reqId}";
                    break;
                case "MCRequest":
                    url = $"Detail_BonEntree.aspx?MCRequestID={reqId}";
                    break;
                case "MCSRequest":
                    url = $"Detail_BonSortie.aspx?MCSRequestID={reqId}";
                    break;
                default:
                    lblErreur.Text = "Unknown module type: " + module;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
                    return;
            }
            Response.Redirect(url);
        }
    }
}