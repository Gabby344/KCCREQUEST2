using KCCDBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2
{
    public partial class Default_View_MyRequests : BasePage
    {
        // Using inherited db from BasePage

        protected void Page_Load(object sender, EventArgs e)
        {
   
            try
            {
                if (!IsPostBack)
                {
                    // Use cached access data for performance
                    var accessDict = GetCachedAccessData();

                    string error = (string)GetGlobalResourceObject("LocalizedText", "lblMessageAccessDenie") + "<br /> <br />";
                    bool isValid = true;

                    bool isVIP = accessDict.TryGetValue("Source", out var source) && source == "VIP";
                    bool missingEmployeeID = !accessDict.TryGetValue("EmployeeID_AD", out var empId) || empId == null;

                    if (isVIP || missingEmployeeID)
                    {
                        // Validate access fields
                        if (missingEmployeeID)
                        {
                            error += "<br /> - " + (string)GetGlobalResourceObject("LocalizedText", "lblwithoutEmployeeAD");
                            isValid = false;
                        }
                        if (!accessDict.TryGetValue("DepartmentCode_SAGE", out var deptCode) || deptCode == null)
                        {
                            error += "<br /> - " + (string)GetGlobalResourceObject("LocalizedText", "lblwithoutDepartmentCode_SAGE");
                            isValid = false;
                        }
                        if (!accessDict.TryGetValue("ReportTo", out var reportTo) || reportTo == null)
                        {
                            error += "<br /> - " + (string)GetGlobalResourceObject("LocalizedText", "lblwithoutReportTo");
                            isValid = false;
                        }

                        if (!isValid)
                        {
                            // Use the authenticated/cached login, not WindowsIdentity.GetCurrent()
                            var listDepartement = Developper.Employee.Get_Employee()
                                .Where(x => x.Sources == "VIP" && string.Equals(x.Login, CachedLogin, StringComparison.OrdinalIgnoreCase))
                                .ToList();

                            string phone = listDepartement.FirstOrDefault()?.TelephoneNumber;
                            int ID = db.Insert_ITProblem(
                                "KCC Request", "Access denied", phone, error, 7, 2,
                                CachedLogin, null, 28, 17, // Use cached login
                                CachedLogin, 1, 4);

                            string descriptionMsg = (string)GetGlobalResourceObject("LocalizedText", "lblIncidentSysaid") +
                                " <span style='color:red'>" + ID.ToString() + "</span> " +
                                (string)GetGlobalResourceObject("LocalizedText", "lblIncidentSysaid2");

                            error += "<br /><br /> " + descriptionMsg;
                            lblMessage.Text = error;
                            ModalPopupExtender1.Show();
                        }
                        else
                        {
                            if (Request.Params["dashboard"] == null && HomePage == 1)
                            {
                                Response.Redirect("Default_View_Approve.aspx");
                            }
                        }
                    }
                    else
                    {
                        if (Request.Params["dashboard"] == null && HomePage == 1)
                        {
                            Response.Redirect("Default_View_Approve.aspx");
                        }
                    }

                    // Use cached dashboard data for performance
                    var liste = GetCachedMyRequestsDashboard();
                    dgvModule.DataSource = liste;
                    dgvModule.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblErreur.Text = "An error occurred. Please contact support.";
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        protected void Show_Hide_ChildGrid(object sender, EventArgs e)
        {
            var imgShowHide = sender as ImageButton;
            var row = imgShowHide?.NamingContainer as GridViewRow;
            if (imgShowHide == null || row == null) return;

            var pnlAllRequest = row.FindControl("pnlAllRequest");
            if (pnlAllRequest == null) return;

            if (imgShowHide.CommandArgument == "Show")
            {
                pnlAllRequest.Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/images/minus.png";

                string modulesID = dgvModule.DataKeys[row.RowIndex].Value.ToString();
                var dgvMyAllRequest = row.FindControl("dgvMyallrequest") as GridView;
                if (dgvMyAllRequest != null)
                {
                    // Consider moving grouping to SQL for large datasets
                    dgvMyAllRequest.DataSource = db.Get_KCCRequest_CreatedBy(modulesID, CachedLogin)
                        .GroupBy(x => new { x.Module, x.Id })
                        .Select(x => x.First());
                    dgvMyAllRequest.DataBind();

                    if (modulesID == "ITRequest")
                    {
                        // Defensive: check column count before hiding
                        if (dgvMyAllRequest.Columns.Count > 10) dgvMyAllRequest.Columns[10].Visible = false;
                        if (dgvMyAllRequest.Columns.Count > 8) dgvMyAllRequest.Columns[8].Visible = false;
                    }
                }
            }
            else
            {
                pnlAllRequest.Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/images/plus.png";
            }
        }

        protected void btndetail_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = sender as Button;
                var row = btn?.NamingContainer as GridViewRow;
                if (btn == null || row == null) return;

                var reqId = row.FindControl("lblID") as Label;
                var module = row.FindControl("lblModule") as Label;
                if (reqId == null || module == null) return;

                var redirectMap = new Dictionary<string, string>
                {
                    { "ITRequest", $"Detail_itRequest.aspx?IDITrequest={reqId.Text}" },
                    { "EWRequest", $"Detail_ewoRequest.aspx?EWRequestID={reqId.Text}" },
                    { "RECTRequest", $"Detail_RecruitmentRequest.aspx?RectRequestID={reqId.Text}" },
                    { "TRNRequest", $"Detail_trainingRequest.aspx?TrID={reqId.Text}" },
                    { "DSKRequest", $"Detail_radiorequest.aspx?DSKRrequestID={reqId.Text}" },
                    { "ITProblem", $"Detail_ItProblem.aspx?IDITProblem={reqId.Text}" },
                    { "TRANSRequest", $"Detail_TRANSRequest.aspx?TRANSRequestID={reqId.Text}" },
                    { "DCRequest", $"Detail_DCRequest.aspx?DCrequestID={reqId.Text}" },
                    { "MCRequest", $"Detail_BonEntree.aspx?MCRequestID={reqId.Text}" },
                    { "MCSRequest", $"Detail_BonSortie.aspx?MCSRequestID={reqId.Text}" }
                };

                if (redirectMap.TryGetValue(module.Text, out string url))
                {
                    Response.Redirect(url);
                }
            }
            catch (Exception ex)
            {
                lblErreur.Text = "An error occurred. Please contact support.";
                System.Diagnostics.Debug.WriteLine(ex);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
        }
    }
}