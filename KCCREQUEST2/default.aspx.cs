using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;

namespace KCCREQUEST2
{
    public partial class _default1 : BasePage
    {
        // Using inherited db from BasePage
        
        private const string VIP_SOURCE = "VIP";
        private const string EMPLOYEE_ID_KEY = "EmployeeID_AD";
        private const string DEPARTMENT_CODE_KEY = "DepartmentCode_SAGE";
        private const string REPORT_TO_KEY = "ReportTo";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // Use cached access data for performance
                    var accessDict = GetCachedAccessData();
                    
                    // Directly use inherited properties from BasePage
                    string login = CachedLogin;
                    int homePage = HomePage;
                    int homeLang = HomeLang;

                    // Redirect based on user homepage preference
                    if (homePage == 0)
                    {
                        Response.Redirect("Default_View_MyRequests.aspx");
                        return;
                    }
                    else if (homePage == 1)
                    {
                        Response.Redirect("Default_View_Approve.aspx");
                        return;
                    }

                    bool isVIP = accessDict.ContainsKey("Source") && accessDict["Source"] == VIP_SOURCE;
                    bool missingEmployeeID = !accessDict.ContainsKey(EMPLOYEE_ID_KEY) || accessDict[EMPLOYEE_ID_KEY] == null;

                    if (isVIP || missingEmployeeID)
                    {
                        string error = (string)GetGlobalResourceObject("LocalizedText", "lblMessageAccessDenie") + "<br /><br />";
                        bool isValid = true;

                        if (missingEmployeeID)
                        {
                            error += "<br />- " + (string)GetGlobalResourceObject("LocalizedText", "lblwithoutEmployeeAD");
                            isValid = false;
                        }
                        if (!accessDict.ContainsKey(DEPARTMENT_CODE_KEY) || accessDict[DEPARTMENT_CODE_KEY] == null)
                        {
                            error += "<br />- " + (string)GetGlobalResourceObject("LocalizedText", "lblwithoutDepartmentCode_SAGE");
                            isValid = false;
                        }
                        if (!accessDict.ContainsKey(REPORT_TO_KEY) || accessDict[REPORT_TO_KEY] == null)
                        {
                            error += "<br />- " + (string)GetGlobalResourceObject("LocalizedText", "lblwithoutReportTo");
                            isValid = false;
                        }

                        if (!isValid)
                        {
                            // Log incident using cached login
                            var listDepartement = Developper.Employee.Get_Employee()
                                .Where(x => x.Sources == VIP_SOURCE && string.Equals(x.Login, CachedLogin, StringComparison.OrdinalIgnoreCase))
                                .ToList();

                            var telephone = listDepartement.FirstOrDefault()?.TelephoneNumber;
                            int ID = db.Insert_ITProblem("KCC Request", "Access denied", telephone, error, 7, 2, CachedLogin, null, 28, 17, CachedLogin, 1, 4);
                            string descriptionMsg = (string)GetGlobalResourceObject("LocalizedText", "lblIncidentSysaid") + " <span style='color:red'>" + ID.ToString() + "</span> " + (string)GetGlobalResourceObject("LocalizedText", "lblIncidentSysaid2");
                            error += "<br /><br />" + descriptionMsg;

                            lblMessage.Text = error;
                            ModalPopupExtender1.Show();
                        }
                        else
                        {
                            // Re-check homepage after validation (original behavior)
                            if (homePage == 0)
                            {
                                Response.Redirect("Default_View_MyRequests.aspx");
                                return;
                            }
                            else if (homePage == 1)
                            {
                                Response.Redirect("Default_View_Approve.aspx");
                                return;
                            }
                        }
                    }

                    // Note: Code below this point will rarely execute due to redirects above
                    // Left for edge cases where homepage preference is not set
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred. Please contact support.";
                ModalPopupExtender1.Show();
                System.Diagnostics.Debug.WriteLine($"Default.aspx error: {ex.Message}");
            }
        }

        protected void CreateITRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_ITRequest.aspx");
        }

        private void UpdateApprovalButtonsAsync(string login)
        {
            // Use cached approval counts for better performance
            SetApprovalButtonTextCached(btnApprovalIT, "ITRequest", login);
            SetApprovalButtonTextCached(btnApprovalDSK, "DSKRequest", login);
            SetApprovalButtonTextCached(btnApprovalTraining, "TRAININGRequest", login);
            SetApprovalButtonTextCached(btnApprovalEWO, "EWRequest", login);
            SetApprovalButtonTextCached(btnApprovalRecrutment, "RECTRequest", login);
            SetApprovalButtonTextCached(btnITProblemApproval, "ITProblem", login);
        }

        private void SetApprovalButtonTextCached(Button button, string module, string login)
        {
            string cacheKey = $"ApprovalCount_{login}_{module}";
            
            if (!(Session[cacheKey] is int count) || 
                !(Session[cacheKey + "_Expires"] is DateTime expires) || 
                DateTime.Now > expires)
            {
                // Cache miss or expired - get fresh count
                try
                {
                    count = db.Get_KCCRequest_ToApprove(module, login).Count();
                    Session[cacheKey] = count;
                    Session[cacheKey + "_Expires"] = DateTime.Now.AddMinutes(5); // 5-minute cache
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error getting approval count for {module}: {ex.Message}");
                    count = 0; // Fallback to 0 if error
                }
            }
            
            button.Text = $"To Approve ({count})";
        }

        private void UpdateApprovalButtons(string login)
        {
            SetApprovalButtonText(btnApprovalIT, "ITRequest", login);
            SetApprovalButtonText(btnApprovalDSK, "DSKRequest", login);
            SetApprovalButtonText(btnApprovalTraining, "TRAININGRequest", login);
            SetApprovalButtonText(btnApprovalEWO, "EWRequest", login);
            SetApprovalButtonText(btnApprovalRecrutment, "RECTRequest", login);
            SetApprovalButtonText(btnITProblemApproval, "ITProblem", login);
        }

        private void SetApprovalButtonText(Button button, string module, string login)
        {
            int count = db.Get_KCCRequest_ToApprove(module, login).Count();
            button.Text = $"To Approve ({count})";
        }

        protected void btnRequestsIT_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_allRequest.aspx?type=ITRequest");
        }

        protected void btnApprovalIT_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_RequestToAproval.aspx?type=ITRequest");
        }

        protected void btnNewDSK_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_DSKRequest.aspx");
        }

        protected void btnRequestDSK_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_allRequest.aspx?type=DSKRequest");
        }

        protected void btnApprovalDSK_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_RequestToAproval.aspx?type=ITRequest");
        }

        protected void btnNewTrainning_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_TrainingRequest.aspx");
        }

        protected void btnRequestTrainning_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_allRequest.aspx?type=TRAININGRequest");
        }

        protected void btnApprovalTraining_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_RequestToAproval.aspx?type=TRAININGRequest");
        }

        protected void btnNewEWO_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_EWORequest.aspx");
        }

        protected void btnRequestEWO_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_allRequest.aspx?type=EWRequest");
        }

        protected void btnApprovalEWO_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_RequestToAproval.aspx?type=EWRequest");
        }

        protected void btnNewRecrutment_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_HrApproval.aspx");
        }

        protected void btnRequestRecrutment_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_allRequest.aspx?type=RECTRequest");
        }

        protected void btnApprovalRecrutment_Click(object sender, EventArgs e)
        {
            Response.Redirect("My_RequestToAproval.aspx?type=RECTRequest");
        }

        protected void btnNewITProblem_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_ITProblem.aspx");
        }

        protected void btnITProblem_Click(object sender, EventArgs e)
        {
            string sysaid = db.Get_KCCRequest_Parametre("ITProblem_Sysaid_Link");
            Response.Redirect(sysaid);
        }

        protected void btnITProblemApproval_Click(object sender, EventArgs e)
        {
            string sysaid = db.Get_KCCRequest_Parametre("ITProblem_Sysaid_Link");
            Response.Redirect(sysaid);
        }

        // Method to clear approval count cache when needed
        public void ClearApprovalCountCache()
        {
            var login = CachedLogin;
            var modules = new[] { "ITRequest", "DSKRequest", "TRAININGRequest", "EWRequest", "RECTRequest", "ITProblem" };
            
            foreach (var module in modules)
            {
                string cacheKey = $"ApprovalCount_{login}_{module}";
                Session.Remove(cacheKey);
                Session.Remove(cacheKey + "_Expires");
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Allow users to always access homepage by adding a query parameter
            if (Request.QueryString["force"] == "1")
            {
                Session["AllowHomepageView"] = true;
            }
        }
    }
}