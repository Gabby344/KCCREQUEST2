using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCCDBAccess
{
    // Helper class for file metadata results from optimized stored procedure
    public class FileMetadataResult
    {
        public int FileID { get; set; }
        public int DCRequestID { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
        public long FileSizeBytes { get; set; }
        public string FileSizeFormatted { get; set; }
    }

    public class KCCDBWrapper
    {
        /// CRUD and Populate methods for KCC Requests or Generalities
        /// 
        /// 
        ///
        #region KCCRequest Methods
        public SP_Get_KCCRequest_Profile_Result GetUserProfile(string login)
        {
            try 
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Profile(login).FirstOrDefault();

                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public List<SP_Get_KCCRequest_ToApprove_Result> Get_KCCRequest_ToApprove(string moduleID, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_ToApprove(moduleID, login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_KCCRequest_HoldToApprove_Result> Get_KCCRequest_ToApproveHold(string moduleID, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_HoldToApprove(moduleID, login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_KCCRequest_Color_Result> Get_KCCRequest_Color()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Color().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_KCCRequest_Access_Result> Get_KCCRequest_Access(string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Access(login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // Cached version for startup performance
        public List<SP_Get_KCCRequest_Access_Result> Get_KCCRequest_Access_Cached(string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.Database.CommandTimeout = 60; // Extended timeout for startup
                    return db.SP_Get_KCCRequest_Access(login).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SP_Get_KCCRequest_ToApprove_Dashboard_Result> Get_KCCRequest_ToApprove_Dashboard(string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_ToApprove_Dashboard(login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // Cached version for startup performance
        public List<SP_Get_KCCRequest_ToApprove_Dashboard_Result> Get_KCCRequest_ToApprove_Dashboard_Cached(string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.Database.CommandTimeout = 60; // Extended timeout for startup
                    return db.SP_Get_KCCRequest_ToApprove_Dashboard(login).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<SP_Get_KCCRequest_CreatedBy_Result> Get_KCCRequest_CreatedBy(string moduleID, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_CreatedBy(moduleID, login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_KCCRequest_Category_Result> Get_KCCRequest_Category(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Category(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_KCCRequest_Priority_Result> Get_KCCRequest_Priority(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Priority(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_KCCRequest_Action_Result> Get_KCCRequest_Action(int ID, string login, string module)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Action(ID, login, module).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // EMERGENCY FIX: Version with extended timeout to handle slow permission queries
        public List<dynamic> Get_KCCRequest_Action_WithTimeout(int ID, string login, string module)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    // Set longer timeout for this critical operation
                    db.Database.CommandTimeout = 120; // 2 minutes instead of default 30 seconds
                    
                    var results = db.SP_Get_KCCRequest_Action(ID, login, module).ToList();
                    return results.Cast<dynamic>().ToList();
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx) when (sqlEx.Number == -2) // Timeout
            {
                // If still timing out, return minimal permissions to allow page to load
                System.Diagnostics.Debug.WriteLine($"SQL timeout for user {login}, request {ID}, module {module}");
                return new List<dynamic>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Action lookup error for user {login}, request {ID}: {ex.Message}");
                return new List<dynamic>();
            }
        }

        // OPTIMIZED VERSION: Uses the ultra-fast module-specific stored procedure
        public List<dynamic> Get_KCCRequest_Action_Optimized(int ID, string login, string module)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    // Use optimized stored procedure that only queries the specific module
                    db.Database.CommandTimeout = 30; // Should be fast enough now
                    
                    // Call the optimized SP using raw SQL since EF might not have the new SP yet
                    var results = db.Database.SqlQuery<dynamic>(
                        "EXEC SP_Get_KCCRequest_Action_V2 @Id, @UserLogin, @Module",
                        new System.Data.SqlClient.SqlParameter("@Id", ID),
                        new System.Data.SqlClient.SqlParameter("@UserLogin", login),
                        new System.Data.SqlClient.SqlParameter("@Module", module)
                    ).ToList();
                    
                    return results;
                }
            }
            catch (System.Data.SqlClient.SqlException sqlEx) when (sqlEx.Number == -2) // Timeout
            {
                // Fallback to original method with timeout if optimized SP isn't deployed yet
                System.Diagnostics.Debug.WriteLine($"Optimized SP not available, falling back to timeout version for user {login}, request {ID}, module {module}");
                return Get_KCCRequest_Action_WithTimeout(ID, login, module);
            }
            catch (Exception ex)
            {
                // Fallback to original method if any issues
                System.Diagnostics.Debug.WriteLine($"Optimized SP error for user {login}, request {ID}: {ex.Message}, falling back to timeout version");
                return Get_KCCRequest_Action_WithTimeout(ID, login, module);
            }
        }
        public List<SP_Get_KCCRequest_Module_Result> Get_KCCRequest_Module(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Module(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_KCCRequest_Report_Result1> Get_KCCRequest_Report(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Report(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_KCCRequest_Lang_Result> Get_KCCRequest_Lang()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Lang().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_KCCRequest_Createdby_Dashboard_Result> Get_KCCRequest_Createdby_Dashboard(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Createdby_Dashboard(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // Cached version for startup performance
        public List<SP_Get_KCCRequest_Createdby_Dashboard_Result> Get_KCCRequest_Createdby_Dashboard_Cached(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.Database.CommandTimeout = 60; // Extended timeout for startup
                    return db.SP_Get_KCCRequest_Createdby_Dashboard(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<SP_Get_KCCRequest_User_HomePage1_Result> Get_KCCRequest_UserHomePage(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_User_HomePage1(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public string Get_KCCRequest_Parametre(string key)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_Parameter_byKey(key).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public int Get_KCCRequest_UserHold(string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return (int)db.SP_Get_KCCRequest_UserHold(login).FirstOrDefault().Result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void Insert_KCCRequest_User_HomePage(string userLogin, int value, int language)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_KCCRequest_User_HomePage1(userLogin, value, language);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
        /// CRUD and Populate methods for IT Requests
        /// 
        /// 
        /// 

        #region ITRequest Methods
        public List<SP_Get_ITRequest_Item_Result> Get_ITRequest_Items(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_Item(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_ITRequest_ToApproveDetail_Result> Get_ITRequest_ToApproveDetail(int ID, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_ToApproveDetail(ID, login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_ITRequest_ToApproveDetail_Had_Hoc_Result> Get_ITRequest_ToApproveAdHoc(int ID, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_ToApproveDetail_Had_Hoc(ID, login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_ITRequest_CanCallBack_Result> Get_ITRequest_CanCallBack(int DetailID, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_CanCallBack(DetailID, login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public SP_Get_ITRequest_ByID_Result Get_ITRequest_ByID(int ID, string UserLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_ByID(ID, UserLogin).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Approve_ITRequest_Detail(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_ITRequest_Detail(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Hold_ITRequest_Detail(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {

                    db.SP_Hold_ITRequest(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Back_ITRequest_Detail(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Back_ITRequest(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void ITRequest_AdHoc(int DetailId, string employeeId, string comment, string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Ad_Hoc_ITRequest(DetailId, employeeId, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Reject_ITRequest_Detail(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reject_ITRequest_Detail(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_ITRequest_History_Result> Get_ITRequest_History(int ID, string UserLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_History(ID, UserLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Return_ITRequest_Detail(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Return_ITRequest_Detail(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void SendMail_ITRequest(int ID, string recipient, string emailDescriptionFr, string emailDescriptionEn, string emailSubjectFr, string emailSujectEn)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_SendMail_ITRequest(ID, recipient, emailDescriptionFr, emailDescriptionEn, emailSubjectFr, emailSujectEn);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update_ITRequest(int ID, string beneficiary, string beneficiaryJobTitle, string description, string comment, string commentPriority, DateTime plannedDeliveryDate, string moduleID, string moduleResponsible,
                                     string moduleName, string moduleDescription, string moduleReason, string moduleSponsor, int priorityID, int categoryID, string commentUpdate, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_ITRequest(ID, beneficiary, beneficiaryJobTitle, description, comment, commentPriority, plannedDeliveryDate, moduleID, moduleResponsible, moduleName, moduleDescription, moduleReason, moduleSponsor, priorityID, commentUpdate, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update_ITRequest_Detail(int detailID, int itemID, int quantity, string comment, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_ITRequest_Detail(detailID, itemID, quantity, comment, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update_ITRequest_Status(int id)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_ITRequest_Status(id);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_ITRequest_File_Result> Get_ITRequest_File(int ID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_File(ID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public SP_Insert_ITRequest_Result Insert_ITRequest(string requestorLogin, string beneficiary, string beneficiaryJobTitle, string description, string comment, string commentPriority, DateTime plannedDeliveryDate, string moduleID, string moduleResponsible,
                                     string moduleName, string moduleDescription, string moduleReason, string moduleSponsor, int priorityID, string createdBy)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Insert_ITRequest(requestorLogin, beneficiary, beneficiaryJobTitle, description, comment, commentPriority, plannedDeliveryDate, moduleID, moduleResponsible, moduleName, moduleDescription, moduleReason, moduleSponsor,
                                            priorityID, createdBy).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Insert_ITRequest_Detail(int ID, int itemID, int statusID, int sysAidID, int quantity, string comment)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_ITRequest_Detail(ID, itemID, statusID, sysAidID, quantity, comment);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Insert_ITRequest_File(int ID, string fileName, string type, byte[] file)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_ITRequest_File(ID, fileName, type, file);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
        #region EWR Requests Methods
        public SP_Insert_EWRequest_Result Insert_EWRequest(string requestorLogin, string description, string existing, string scopeOfWork, string justification, string site, string fleet, string equipement, int priorityID, int categoryID, string createdBy, DateTime plannedDeliveryDate)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Insert_EWRequest(requestorLogin, description, existing, scopeOfWork, justification, site, fleet, equipement, priorityID, categoryID, createdBy, plannedDeliveryDate).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public decimal Insert_EWRequest_I(string requestorLogin, string description, string existing, string scopeOfWork, string justification, string site, string fleet, string equipement, int priorityID, int categoryID, string createdBy, DateTime plannedDeliveryDate, int TaskId)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return (decimal)(db.SP_Insert_EWRequest_I(requestorLogin, description, existing, scopeOfWork, justification, site, fleet, equipement, priorityID, categoryID, createdBy, plannedDeliveryDate, TaskId).FirstOrDefault().Value);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<object> Get_Employee_ByID_Or_Login(string v, object employeeID)
        {
            throw new NotImplementedException();
        }

        public void Update_EWRequest(int IDEWRequest, string RequestorLogin, string RequestorName, string description, string existing, string scopeofWork, string justification, string site, string feet, string equipement, DateTime plannedDeliveryDate, string departement, int priority, int category, string userLogin)
        {

            using (kcc_request_dbEntities db = new kcc_request_dbEntities())
            {
                //db.SP_Update_EWRequest(IDEWRequest, RequestorLogin, RequestorName, description, existing, scopeofWork, justification, site, feet, equipement, plannedDeliveryDate, departement, priority, category, userLogin);
            }

        }

        public void Update_EWRequest_I(int IDEWRequest, string RequestorLogin, string RequestorName, string description, string existing, string scopeofWork, string justification, string site, string feet, string equipement, DateTime plannedDeliveryDate, string departement, int priority, int category, string userLogin, int TaskID, string userComment)
        {

            using (kcc_request_dbEntities db = new kcc_request_dbEntities())
            {
                db.SP_Update_EWRequest_I(IDEWRequest, RequestorLogin, RequestorName, description, existing, scopeofWork, justification, site, feet, equipement, plannedDeliveryDate, departement, priority, category, userLogin, TaskID/*, userComment*/);
            }

        }
        public void Insert_EWRequest_File(int EWRequestID, string fileName, string type, byte[] file)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_EWRequest_File(EWRequestID, fileName, type, file);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_EWRequest_ByID_Result> Get_EWRequest_ByID(int ewrID, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_EWRequest_ByID(ewrID, userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_EWRequest_File_Result> Get_EWRRequest_File(int ewrID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_EWRequest_File(ewrID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_EWRequest_History_Result> Get_EWRRequest_History(int ewrID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_EWRequest_History(ewrID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_EWRequest_TechnicalDepartment_Result> Get_EWRequest_TechnicalDepartment(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_EWRequest_TechnicalDepartment(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_EWRequest_Manager_ByTechnicalDepartment_Result> Get_EWRequest_Manager_ByTechnicalDepartment(int techDepartment)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_EWRequest_Manager_ByTechnicalDepartment(techDepartment).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_EWRequest_Planner_ByTechnicalDepartment_Result> Get_EWRequest_Planner_ByTechnicalDepartment(int techDepartment)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_EWRequest_Planner_ByTechnicalDepartment(techDepartment).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_EWRequest_Estimation_Result> Get_EWRequest_Estimation(int eWRequestID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {

                    return db.SP_Get_EWRequest_Estimation(eWRequestID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_EWRequest_WorkType_Result> Get_EWRequest_WorkType()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {

                    return db.SP_Get_EWRequest_WorkType().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_EWRequest_Task_ByWorkTypeId_Result> Get_EWRequest_Task(int workType)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {

                    return db.SP_Get_EWRequest_Task_ByWorkTypeId(workType).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Approve_EWRequest(int ewRequestID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_EWRequest(ewRequestID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void EWRequest_AdHoc(int Id, string employeeId, string comment, string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Ad_Hoc_EWRequest(Id, employeeId, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Approve_EWRequest_Budget(int ewRequestID, string comment, bool Isbudgetized, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_EWRequest_Budget(ewRequestID, comment, Isbudgetized, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Approve_EWRequest_ByDispatcher(int eWRequestID, string comment, string equipment, int technicalDepartment, string technicalManagerName, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_EWRequest_ByDispatcher(eWRequestID, comment, equipment, technicalDepartment, technicalManagerName, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Approve_EWRequest_ByTechnicalManager(int eWRequestID, string comment, string technicalPlannerName, bool budget, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_EWRequest_ByTechnicalManager(eWRequestID, comment, technicalPlannerName, budget, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Approve_EWRequest_ByTechnicalPlanner(int eWRequestID, string comment, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_EWRequest_ByTechnicalPlanner(eWRequestID, comment, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Reject_EWRequest(int eWRequestID, string comment, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reject_EWRequest(eWRequestID, comment, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Return_EWRequest(int eWRequestID, string comment, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Return_EWRequest(eWRequestID, comment, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Insert_EWRequest_Estimation(int eWRequestID, string description, decimal qte, decimal unitCost, string unitMesure, int type)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_EWRequest_Estimation(eWRequestID, description, qte, unitCost, unitMesure, type);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Back_EWRequest(int eWRequestID, string comment, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Back_EWRequest(eWRequestID, comment, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Approve_EWRequest_Satisfaction(int eWRequestID, string comment, bool yesNoSatisfaction, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_EWRequest_Satisfaction(eWRequestID, comment, yesNoSatisfaction, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Delete_EWRequest_Estimation(int idEstimation)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Delete_EWRequest_EstimationByID(idEstimation);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }





        //#region Transport Request Methods
        public decimal Insert_TRANSRequest(string requestorLogin, string PhoneNumber, DateTime DepartureDateTime, int DeparturePlaceId, int ArrivalPlaceId, int PlaceNumber, string Comment, string CreatedBy)
        {


            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {

                    return (decimal)(db.SP_Insert_TRANSRequest(requestorLogin, PhoneNumber, DepartureDateTime, DeparturePlaceId, ArrivalPlaceId, PlaceNumber, Comment, CreatedBy).FirstOrDefault().Value);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_TRANSRequest_Place_Result> Get_TRANSRequest_Place()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRANSRequest_Place().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_TRANSRequest_ByID_Result> Get_TRANSRequest_ById(int Id, String UserLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRANSRequest_ByID(Id, UserLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRANSRequest_File_Result> Get_TRANSRequest_File(int TRANSRequestID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRANSRequest_File(TRANSRequestID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void insertTransportFile(int trainingID, string filename, string type, byte[] file)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_TRANSRequest_File(trainingID, filename, type, file);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRANSRequest_History_Result> Get_TRANSRequest_History(int ewrID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRANSRequest_History(ewrID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void ApproveTRANSRequest(int TransRequestID, string Comment, string UserLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_TRANSRequest(TransRequestID, Comment, UserLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void RejectTRANSRequest(int TransRequestID, string Comment, string UserLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reject_TRANSRequest(TransRequestID, Comment, UserLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_TRANSRequest_Result> Get_TRANSRequest()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRANSRequest().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRANSRequest_ProcessRole_Result> Get_TransProcessRole()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRANSRequest_ProcessRole().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }






        public List<SP_Get_TRANSRequest_Search_Result> Get_TRANSRequest_Search(string Search)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRANSRequest_Search(Search).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public IList<SP_CRUD_TRANSRequest_ProcessRole_Result> Crud_TRANSRequest_ProcessRole(int? idrole, string role, string description, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_TRANSRequest_ProcessRole(idrole, role, description, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_TRANSRequest_ProcessRole_User_Result> Crud_TRANSRequest_ProcessRoleUser(int? idroleUser, string userLogin, string role, DateTime? startDate, DateTime? endDate, bool? activated, int? type, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_TRANSRequest_ProcessRole_User(idroleUser, userLogin, role, startDate, endDate, activated, type, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_TRANSRequest_Status_Result> Crud_TRANSRequest_Status(int? status, string statusDescription, string actionStatus, string emailDescriptionOriginatorFr, string emailDescriptionOrginatorEn, string emailSubjectOrginitorFr, string emailSubjectOrginatorEn, string EmailDescriptionApproverFr, string EmailDescriptionApproverEn, string emailSubjectApproverFr, string emailSubjectApproverEn, string action, string stautDescriptionFrench)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_TRANSRequest_Status(status, statusDescription, stautDescriptionFrench, actionStatus, emailDescriptionOriginatorFr, emailDescriptionOrginatorEn, emailSubjectOrginitorFr, emailSubjectOrginatorEn, EmailDescriptionApproverFr, EmailDescriptionApproverEn, emailSubjectApproverFr, emailSubjectApproverEn, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_TRANSRequest_Site_Result> Crud_TRANSRequest_Site(int? SiteId, string SiteDescription, bool SiteActivated, string Action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_TRANSRequest_Site(SiteId, SiteDescription, SiteActivated, Action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }



        public IList<SP_CRUD_TRANSRequest_Place_Result> Crud_TRANSRequest_Place(int? PlaceId, int? SiteId, string PlaceDescription, bool PlaceActivated, string Action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_TRANSRequest_Place(PlaceId, SiteId, PlaceDescription, PlaceActivated, Action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_Get_TRANSRequest_Site_Result> Get_TRANSRequest_Site()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRANSRequest_Site().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }






        #endregion
        #region Recrutement Request Methods
        public List<SP_Get_RECTRequest_Level_Result> Get_RECTRequest_Level()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_Level().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_RECTRequest_CampLocation_Result> Get_RECTRequest_CampLocation(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_CampLocation(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_RECTRequest_EmploymentEntity_Result> Get_RECTRequest_EmploymentEntity()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_EmploymentEntity().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_RECTRequest_EmployeeType_Result> Get_RECTRequest_EmployeeType(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_EmployeeType(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_RECTRequest_Contract_Result> Get_RECTRequest_Contract(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_Contract(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_RECTRequest_Duration_Result> Get_RECTRequest_Duration(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_Duration(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_RECTRequest_Type_Result> Get_RECTRequest_Type(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_Type(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_RECTRequest_CostCenter_Result> Get_RECTRequest_CostCenter()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_CostCenter().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_RECTRequest_JobGrade_Result> Get_RECTRequest_JobGrade(int employeeTypeID, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_JobGrade(employeeTypeID, userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Insert_RectRequest_File(int RectRequestID, string fileName, string type, byte[] file)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_RECTRequest_File(RectRequestID, fileName, type, file);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public decimal Insert_RectRequest(string RequestorLogin, string PricipalTask, string Function, string NominalQualification, string othersExigences, int NumberOfCandidat, string duration, DateTime plannedDeliveryDate, bool isexpat, bool isbudgeted, bool isplanned, bool isreplacement,
            string remplacementEmployeeId, string remplacementName, string remplacementMotif, string remplacementPostNumber, string remplacementJobTitle, int contractTypeId, int jobGradeId, string levelID, int typeRect, string costCenter, int priority, int category, DateTime createDate, string createBy, string reportto, int employeeType, bool IsOps, int? employementEntity, int? campLocation)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return Convert.ToDecimal(db.SP_Insert_RECTRequest(RequestorLogin, PricipalTask, Function, NominalQualification, othersExigences, NumberOfCandidat, duration, plannedDeliveryDate, isexpat, isbudgeted, isplanned, isreplacement,
                remplacementEmployeeId, remplacementName, remplacementMotif, remplacementPostNumber, remplacementJobTitle, contractTypeId, jobGradeId, levelID, typeRect, costCenter, priority, category, createDate, createBy, reportto, employeeType, IsOps, employementEntity, campLocation).FirstOrDefault().Value);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public SP_Get_RECTRequest_ByID_Result Get_RectRequest_ByID(int RectID, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_ByID(RectID, userLogin).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_RECTRequest_File_Result> Get_RectRequest_File(int RectID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_File(RectID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_RECTRequest_History_Result> Get_RectRequest_History(int RectID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_History(RectID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Approve_RectRequest(int RectID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_RECTRequest(RectID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Close_RectRequest(int RectID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Close_RECTRequest(RectID, comment, login);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void RECTRequest_AdHoc(int Id, string employeeId, string comment, string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Ad_Hoc_RECTRequest(Id, employeeId, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Approve_RectRequestHRSystem(int RectID, string comment, string login, bool isbudged, bool isops, int? budgedYear)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_RECTRequest_ByHRSystem(RectID, comment, login, isbudged, isops, budgedYear);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Reject_RectRequest(int RectID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reject_RECTRequest(RectID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Return_RectRequest(int RectID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Return_RECTRequest(RectID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void PreviewBack_RectRequest(int RectID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Back_RECTRequest(RectID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void Update_RectRequest(int RectID, string RequestorLogin, string requestName, string PricipalTask, string Function, string NominalQualification, string othersExigences, int NumberOfCandidat, string duration, DateTime plannedDeliveryDate, bool isexpat, bool isbudgeted, bool isplanned, bool isreplacement
          , string confirmBy, string remplacementEmployeeId, string remplacementName, string remplacementMotif, string remplacementPostNumber, string remplacementJobTitle, int contractTypeId, int jobGradeId, string levelID, int typeRect, string costCenter, int priority, int category, string reportto, string createBy, int EmployeeType, int? employementEntity, int? campLocation, int? budgedYear, string userComment, string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {

                    db.SP_Update_RECTRequest(RectID, RequestorLogin, requestName, PricipalTask, Function, NominalQualification, othersExigences, NumberOfCandidat, duration, plannedDeliveryDate, isexpat, isbudgeted, isplanned, isreplacement
                , confirmBy, remplacementEmployeeId, remplacementName, remplacementMotif, remplacementPostNumber, remplacementJobTitle, contractTypeId, jobGradeId, levelID, typeRect, costCenter, priority, category, reportto, createBy, EmployeeType, employementEntity, campLocation, budgedYear, userComment, login);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
        #region Training Request Methods

        public List<SP_Get_TRNRequest_Type_Result> Get_TrainingType(string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Type(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRNRequest_Calendars_Result> Get_TrainingCalendar(string IDTraining)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Calendars(IDTraining).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRNRequest_Calendar_Result> Get_TrainingCalendars(string UserLogin, DateTime Dateoncalendar)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Calendar(UserLogin, Dateoncalendar).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_TRNRequest_Calendar_ByTrainingID_Result> Get_TrainingCalendarByTrainingID(string UserLogin, DateTime Dateoncalendar, int trainingID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Calendar_ByTrainingID(UserLogin, Dateoncalendar, trainingID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_TRNRequest_Calendar_ById_Result> Get_TrainingCalendar_ById(int TrainingEventID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Calendar_ById(TrainingEventID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public decimal inserTraining(string requestLogin, string beneficiary, string beneficiaryjobtitle, string description, string comment, string course, DateTime plannedDeliveryDate, int TypeID, int priorityID, int categoryID, string createdBy, int TrainingEventID, string courseFees, string courseVenue, string visaRequirement, string transport, string certification, string legalRequirement, Nullable<System.DateTime> departureDate, Nullable<System.DateTime> returnDate, string courseDates, string durationOfstay, string purpose, string benefit, string developmentPlan, bool isBudgeted)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return (db.SP_Insert_TRNRequest(requestLogin, beneficiary, beneficiaryjobtitle, description, comment, course, plannedDeliveryDate, isBudgeted, TypeID, priorityID, categoryID, createdBy, TrainingEventID, courseFees, courseVenue, visaRequirement, transport,
                        certification, legalRequirement, departureDate.Value, returnDate.Value, courseDates, durationOfstay, purpose, benefit, developmentPlan).FirstOrDefault().Value);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void insertTrainingDetail(int trainingID, string employeeID, string employeeName, string jobTitle, string natureOfEmployee, string companyName, string language)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_TRNRequest_Detail(trainingID, employeeID, employeeName, jobTitle, natureOfEmployee, companyName, language);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void insertTrainingMotivation(int trainingID, string production, string environment, string safety)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_TRNRequest_Motivation(trainingID, production, environment, safety);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void insertTrainingFile(int trainingID, string filename, string type, byte[] file)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_TRNRequest_File(trainingID, filename, type, file);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public SP_Get_TRNRequest_ByID_Result Get_TrainingRequest_ByID(int TrainingID, string userLogin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_ByID(TrainingID, userLogin).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public SP_Get_TRNRequest_Motivation_ByID_Result Get_TrainingRequest_Motivation(int TrainingID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Motivation_ByID(TrainingID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRNRequest_File_Result> Get_TrainingRequest_File(int TrainingID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_File(TrainingID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRNRequest_Detail_Result> Get_TrainingRequest_Detail(int TrainingID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Detail(TrainingID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRNRequest_History_Result> Get_TrainingRequest_History(int TrainingID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_History(TrainingID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Approve_Training(int TrainingID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_TRNRequest(TrainingID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRNRequest_Service_Result> Get_TrainingService()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Service().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Approve_TrainingSec(int TrainingID, string comment, int isbadgeted, int serviceID, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_TRNRequest_ByTrainingAdmin(TrainingID, comment, isbadgeted, serviceID, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void TRNRequest_AdHoc(int Id, string employeeId, string comment, string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Ad_Hoc_TRNRequest(Id, employeeId, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_TRNRequest_Cours_Result> Get_TrainingCours()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Cours().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRNRequest_Location_Result> Get_TrainingLocation()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Location().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Approve_TrainingSup(int TrainingID, int locationID, string coursID, DateTime startDate, DateTime endDate, string days, string periodicity, string trainingvenue, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_TRNRequest_BySuperintendent(TrainingID, locationID, coursID, startDate, endDate, days, periodicity, trainingvenue, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Return_Training(int trainingID, string comment, string loggin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Return_TRNRequest(trainingID, comment, loggin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void Update_Training(int trainingID, string beneficiary, string benefiaryJobTitle, string description, string comment, string course, DateTime plannedDeliveryDate, int typeID, int priorityID, int categoryID, string loggin, string userComment, int TrainingEventID, string courseFees, string courseVenue, string visaRequirement, string transport, string certification, string legalRequirement, Nullable<System.DateTime> departureDate, Nullable<System.DateTime> returnDate, string courseDates, string durationOfstay, string purpose, string benefit, string developmentPlan, bool isBudgeted)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_TRNRequest(trainingID, beneficiary, benefiaryJobTitle, description, comment, course, plannedDeliveryDate, isBudgeted, typeID, priorityID, categoryID, loggin, userComment, TrainingEventID, courseFees, courseVenue, visaRequirement,
                        transport, certification, legalRequirement, departureDate.Value, returnDate.Value, courseDates, durationOfstay, purpose, benefit, developmentPlan);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void update_TrainingMotivation(int trainingID, string production, string environment, string safety)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_TRNRequest_Motivation(trainingID, production, environment, safety);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Reject_Training(int trainingID, string comment, string loggin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reject_TRNRequest(trainingID, comment, loggin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void Back_Training(int trainingID, string comment, string loggin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Back_TRNRequest(trainingID, comment, loggin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void hold_Training(int trainingID, string comment, string loggin)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Hold_TRNRequest(trainingID, comment, loggin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Sendmail_Training(int trainingID)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_SendMail_TRNRequest(trainingID);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int Check_Can_SendToNationalDevelopment(int Id, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Check_Can_Send_ToNationalDevelopment(Id, login).FirstOrDefault().Value;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public void Send_TRNRequest_ToNationalDevelopment(int Id, string comments, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Send_TRNRequest_ToNationalDevelopment(Id, comments, login);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        #region KCCRequest Methods

        #endregion
        #region DSK Request Methods
        public List<SP_Get_DSKRequest_Item_Result> Get_DSKRequest_Items()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DSKRequest_Item().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void SendMail_DSKRequest(int ID, string recipient, string emailDescriptionFr, string emailDescriptionEn, string emailSubjectFr, string emailSujectEn)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_SendMail_DSKRequest(ID, recipient, emailDescriptionFr, emailDescriptionEn, emailSubjectFr, emailSujectEn);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public decimal Insert_DSKRequest(string requestLogin, string beneficiary, string beneficiaryJobTitle, string description, string comment, DateTime plannedDeliveryDate, int priorityID, int CategoryID, string createBy)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return (db.SP_Insert_DSKRequest(requestLogin, beneficiary, beneficiaryJobTitle, description, comment, plannedDeliveryDate, priorityID, CategoryID, createBy).FirstOrDefault().Value);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void insert_DSKRequest_Detail(int DskRequestID, int itemsID, int statutID, int sysaidID, int quantity, string comment)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_DSKRequest_Detail(DskRequestID, itemsID, statutID, sysaidID, quantity, comment);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void insert_DSKRequest_Sysaid(string DskRequestID, int service)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_DSKRequest_SysAid(DskRequestID, service);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void insert_DSKRequest_Files(int DskRequestID, string fileName, string type, byte[] images)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_DSKRequest_File(DskRequestID, fileName, type, images);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public SP_Get_DSKRequest_ByID_Result Get_DskRequestByID(int ID, string UserLogin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DSKRequest_ByID(ID, UserLogin).ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DSKRequest_Detail_Result> Get_DskRequest_Detail(int ID)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DSKRequest_Detail(ID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DSKRequest_History_Result> Get_DskRequest_History(int ID)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DSKRequest_History(ID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_DSKRequest_File_Result> Get_DskRequest_Files(int ID)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DSKRequest_File(ID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Approve_DSKRequest(int DSKRequestID, string comment, string loggin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_DSKRequest(DSKRequestID, comment, loggin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void DSKRequest_AdHoc(int Id, string employeeId, string comment, string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Ad_Hoc_DSKRequest(Id, employeeId, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Return_DSKRequest(int DSKRequestID, string comment, string loggin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Return_DSKRequest(DSKRequestID, comment, loggin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Update_DSKRequest(int DSKRequestId, string requestLogin, string beneficiary, string beneficiaryJobTitle, string description, string comment, DateTime plannedDeliveryDate, int priorityID, int categoryID, string createBy, string userLogin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_DSKRequest(DSKRequestId, requestLogin, beneficiary, beneficiaryJobTitle, description, comment, plannedDeliveryDate, priorityID, categoryID, createBy, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Reject_DSKRequest(int DSKRequestID, string comment, string loggin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reject_DSKRequest(DSKRequestID, comment, loggin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Back_DSKRequest(int DSKRequestID, string comment, string loggin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Back_DSKRequest(DSKRequestID, comment, loggin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
        #region PPE Request Methods

        #endregion
        #region it probleme

        public List<SP_Get_ITProblem_Department_Result> Get_ITProblem_Department()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITProblem_Department().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DeviceRegistration_Location_Result1> Get_DeviceRegistration_Location()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DeviceRegistration_Location().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_ITProblem_Location_Result> Get_ITProblem_Location()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITProblem_Location().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_ITProblem_Urgency_Result> Get_ITProblem_Urgency()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITProblem_Urgency().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_ITProblem_Impact_Result> Get_ITProblem_Impact()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITProblem_Impact().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_ITProblem_Affected_Result> Get_ITProblem_Affected()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITProblem_Affected().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_ITProblem_WorkAround_Result> Get_ITProblem_WorkAround()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITProblem_WorkAround().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_ITProblem_Result> Get_ITProblem()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITProblem().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_ITProblem_File_Result> Get_ITProblemFile(int ITProblemID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITProblem_File(ITProblemID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int Insert_ITProblem(string Subject, string Description, string telephone, string Note, int Urgency, int Impact, string RequestorLogin, byte[] attachement, int Location, int Department, string Submit_User, int affected, int around)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return (db.SP_Insert_ITProblem(Subject, Description, Note, Urgency, Impact, RequestorLogin, attachement, Location, Department, affected, around, Submit_User, telephone)).FirstOrDefault().Value;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void Insert_ITProblem_File(int ITProblemID, string fileName, string type, byte[] images)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_ITProblem_File(ITProblemID, fileName, type, images);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int Check_AccessDevice(string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                   return db.SP_Check_AccessDevice(login).FirstOrDefault().Valeur.Value;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
        #region Out of Office Methods

        public SP_Get_EmployeeByLogin_Result Get_Employee_ByID_Or_Login(string employeeID)
        {
            try
            {
                using (developpers_dbEntities db = new developpers_dbEntities())
                {
                    return db.SP_Get_EmployeeByLogin(employeeID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_Module_Result> Get_Module()
        {
            try
            {
                using (developpers_dbEntities db = new developpers_dbEntities())
                {
                    return db.SP_Get_Module().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Insert_OutOfOffices(string fromEmployeeID, string fromUser, string toEmployeeID, string toUser, string module, string role, DateTime startDate, DateTime endDate, string comment, string createdBy, string processID)
        {
            try
            {
                using (developpers_dbEntities db = new developpers_dbEntities())
                {

                    db.SP_Insert_OutOfOffices(fromEmployeeID, fromUser, toEmployeeID, toUser, module, role, startDate, endDate, comment, createdBy, processID);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_OutOfOffices_ByUser_Result> Get_OutOfOffices_ByUser(string userLogin)
        {
            try
            {
                using (developpers_dbEntities db = new developpers_dbEntities())
                {
                    return db.SP_Get_OutOfOffices_ByUser(userLogin).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SP_Get_Employee_User_Result> Get_Employee_User()
        {
            //try
            //{
            using (developpers_dbEntities db = new developpers_dbEntities())
                //{
                return db.SP_Get_Employee_User().ToList();
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
        }
        public void Update_OutOfOffices_Status(int id, int statusID, string userLogin)
        {
            try
            {
                using (developpers_dbEntities db = new developpers_dbEntities())
                {
                    db.SP_Update_OutOfOffices_Status(id, statusID, userLogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_Role_ByUser_Result> Get_Role_ByUser(string module, string userLogin, string submodule)
        {
            try
            {
                using (developpers_dbEntities db = new developpers_dbEntities())
                {
                    return db.SP_Get_Role_ByUser(module, userLogin).Where(x => x.SubModuleID == submodule).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public List<SP_Get_Role_ByUser_Result> Get_Role_ByUser(string module, string userLogin)
        {
            try
            {
                using (developpers_dbEntities db = new developpers_dbEntities())
                {
                    return db.SP_Get_Role_ByUser(module, userLogin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public void Send_MailOutOfOffice(string delegateFrom, string delegateTo, string date1, string date2, string role, string comment)
        {
            try
            {
                using (developpers_dbEntities db = new developpers_dbEntities())
                {
                    db.SP_Send_MailOutOfOffice(delegateFrom, delegateTo, date1, date2, role, comment);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
        #region kccRequest admin

        public IList<SP_CRUD_ITRequestCategory_Result> Crud_itRequestCategory(int? categoryID, string categoryDescription, string observation, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_ITRequestCategory(categoryID, categoryDescription, observation, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }



        public IList<SP_CRUD_ITRequestItem_Result> Crud_itRequestItem(int? itemID, int? serviceID, int? categoryID, string itemDescription, string observation, string approverole, int? level, string action, string descriptionFrench)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_ITRequestItem(itemID, serviceID, categoryID, itemDescription, descriptionFrench, observation, approverole, level, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_ITRequest_ProcessRole_Result> Crud_itRequestProcessRole(int? idrole, string role, string description, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_ITRequest_ProcessRole(idrole, role, description, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_ITRequest_ProcessRole_User_Result> Crud_itRequestProcessRoleUser(int? idroleUser, string userLogin, string role, DateTime? startDate, DateTime? endDate, bool? activated, int? type, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_ITRequest_ProcessRole_User(idroleUser, userLogin, role, startDate, endDate, activated, type, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_ITRequest_Service_Result> Crud_itRequestService(int? serviceID, string serviceDescription, string observation, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_ITRequest_Service(serviceID, serviceDescription, observation, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_ITRequest_HomeApplication_Result> Crud_itRequestHomeLogiciel(string LogicielID, string LogicielDescription, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_ITRequest_HomeApplication(LogicielID, LogicielDescription, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_ITRequest_Status_Result> Crud_itRequestStatus(int? status, string statusDescription, string actionStatus, string emailDescriptionOriginatorFr, string emailDescriptionOrginatorEn, string emailSubjectOrginitorFr, string emailSubjectOrginatorEn, string EmailDescriptionApproverFr, string EmailDescriptionApproverEn, string emailSubjectApproverFr, string emailSubjectApproverEn, string action, string StatutdescriptionFrench)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_ITRequest_Status(status, statusDescription, StatutdescriptionFrench, actionStatus, emailDescriptionOriginatorFr, emailDescriptionOrginatorEn, emailSubjectOrginitorFr, emailSubjectOrginatorEn, EmailDescriptionApproverFr, EmailDescriptionApproverEn, emailSubjectApproverFr, emailSubjectApproverEn, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<SP_Get_ITRequest_Category_Result> Get_ITRequestCategory()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_Category().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_ITRequest_Service_Result> Get_ITRequestService()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_Service().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_ITRequest_ProcessRole_Result> Get_ITRequestProcessRole()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_ProcessRole().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DSKRequest_ProcessRole_Result> Get_DSKRequestProcessRole()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DSKRequest_ProcessRole().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_ITRequest_Result> Get_ITReques()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DSKRequest_Result> Get_DSKRequest()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DSKRequest().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_EWRequest_Result> Get_EWRequest()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_EWRequest().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRNRequest_Result> Get_TrainingRequest()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_RECTRequest_Result> Get_RECTRequest()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IList<SP_CRUD_DSKRequestItem_Result> Crud_DSKRequestItems(int? itemsID, int? serviceID, int? category, string itemDescription, string observation, string approverRole, int? level, string action, string descriptionFrench)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_DSKRequestItem(itemsID, serviceID, category, itemDescription, descriptionFrench, observation, approverRole, level, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_DSKRequest_ProcessRole_Result> Crud_DSKRequest_ProcessRole(int? IdRole, string role, string description, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_DSKRequest_ProcessRole(IdRole, role, description, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_DSKRequest_ProcessRole_User_Result> Crud_DSKRequest_ProcessRoleUser(int? idRoleUser, string UserLogin, string role, DateTime? stardDate, DateTime? endDate, bool? activated, int? type, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_DSKRequest_ProcessRole_User(idRoleUser, UserLogin, role, stardDate, endDate, activated, type, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_DSKRequest_Status_Result> Crud_DSKRequest_Status(int? status, string statusDescription, string actionStatus, string emailDescriptionOriginatorFr, string emailDescriptionOrginatorEn, string emailSubjectOrginitorFr, string emailSubjectOrginatorEn, string EmailDescriptionApproverFr, string EmailDescriptionApproverEn, string emailSubjectApproverFr, string emailSubjectApproverEn, string action, string statutDescriptionFrench)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_DSKRequest_Status(status, statusDescription, statutDescriptionFrench, actionStatus, emailDescriptionOriginatorFr, emailDescriptionOrginatorEn, emailSubjectOrginitorFr, emailSubjectOrginatorEn, EmailDescriptionApproverFr, EmailDescriptionApproverEn, emailSubjectApproverFr, emailSubjectApproverEn, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_EWRequest_ProcessRole_Result> Crud_EWORequest_ProcessRole(int? IdRole, string role, string description, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_EWRequest_ProcessRole(IdRole, role, description, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_EWRequest_WorkType_Result> Crud_EWORequest_WorkType(int? WorkTypeID, int? TechnicalDepId, string WorkTypeDescription, string WorkTypeDescriptionFr, string observation, string approveRole, int? level, int? order, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_EWRequest_WorkType(WorkTypeID, TechnicalDepId, WorkTypeDescription, WorkTypeDescriptionFr, observation, approveRole, level, order, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }



        public IList<SP_CRUD_EWRequest_Task_Result> Crud_EWORequest_Task(int? TaskID, int? WorkTypeID, string TaskDescription, string TaskDescriptionFr, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_EWRequest_Task(TaskID, WorkTypeID, TaskDescription, TaskDescriptionFr, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<SP_Get_EWRequest_ProcessRole_Result> Get_EWOProcessRole()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_EWRequest_ProcessRole().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IList<SP_CRUD_EWRequest_ProcessRole_User_Result> Crud_EWORequest_ProcessRoleUser(int? idRoleUser, string UserLogin, string role, DateTime? stardDate, DateTime? endDate, bool? activated, int? type, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_EWRequest_ProcessRole_User(idRoleUser, UserLogin, role, stardDate, endDate, activated, type, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_EWRequest_Status_Result> Crud_EWORequest_Status(int? status, string statusDescription, string actionStatus, string emailDescriptionOriginatorFr, string emailDescriptionOrginatorEn, string emailSubjectOrginitorFr, string emailSubjectOrginatorEn, string EmailDescriptionApproverFr, string EmailDescriptionApproverEn, string emailSubjectApproverFr, string emailSubjectApproverEn, string action, string stautDescriptionFrench)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_EWRequest_Status(status, statusDescription, stautDescriptionFrench, actionStatus, emailDescriptionOriginatorFr, emailDescriptionOrginatorEn, emailSubjectOrginitorFr, emailSubjectOrginatorEn, EmailDescriptionApproverFr, EmailDescriptionApproverEn, emailSubjectApproverFr, emailSubjectApproverEn, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_EWRequest_TechnicalDepartment_Status_Result> Crud_EWORequest_TechlDepStatut(int? TechnicalDepId, string TechnicalDepDescription, int? DepartementCode, string roleCoordonator, string roleMan, string roleGM, bool? Istchical, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_EWRequest_TechnicalDepartment_Status(TechnicalDepId, TechnicalDepDescription, DepartementCode, roleCoordonator, roleMan, roleGM, Istchical, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_RECTRequest_ProcessRole_Result> Crud_RECTRequest_ProcessRole(int? IdRole, string role, string description, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_RECTRequest_ProcessRole(IdRole, role, description, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<SP_Get_RECTRequest_ProcessRole_Result> Get_RECTProcessRole()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_ProcessRole().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IList<SP_CRUD_RECTRequest_ProcessRole_User_Result> Crud_RECTRequest_ProcessRoleUser(int? idRoleUser, string UserLogin, string role, DateTime? stardDate, DateTime? endDate, bool? activated, int? type, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_RECTRequest_ProcessRole_User(idRoleUser, UserLogin, role, stardDate, endDate, activated, type, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_RECTRequest_Status_Result> Crud_RECTRequest_Status(int? status, string statusDescription, string statusDescriptionFr, string actionStatus, string emailDescriptionOriginatorFr, string emailDescriptionOrginatorEn, string emailSubjectOrginitorFr, string emailSubjectOrginatorEn, string EmailDescriptionApproverFr, string EmailDescriptionApproverEn, string emailSubjectApproverFr, string emailSubjectApproverEn, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_RECTRequest_Status(status, statusDescription, actionStatus, statusDescriptionFr, emailDescriptionOriginatorFr, emailDescriptionOrginatorEn, emailSubjectOrginitorFr, emailSubjectOrginatorEn, EmailDescriptionApproverFr, EmailDescriptionApproverEn, emailSubjectApproverFr, emailSubjectApproverEn, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public IList<SP_CRUD_TRNRequest_ProcessRole_Result> Crud_TRNRequest_ProcessRole(int? IdRole, string role, string description, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_TRNRequest_ProcessRole(IdRole, role, description, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<SP_Get_TRNRequest_ProcessRole_Result> Get_TRNProcessRole()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_ProcessRole().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IList<SP_CRUD_TRNRequest_ProcessRole_User_Result> Crud_TRNRequest_ProcessRoleUser(int? idRoleUser, string UserLogin, string role, DateTime? stardDate, DateTime? endDate, bool? activated, int? type, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_TRNRequest_ProcessRole_User(idRoleUser, UserLogin, role, stardDate, endDate, activated, type, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_TRNRequest_Status_Result> Crud_TRNRequest_Status(int? status, string statusDescription, string statusDescriptionFr, string actionStatus, string emailDescriptionOriginatorFr, string emailDescriptionOrginatorEn, string emailSubjectOrginitorFr, string emailSubjectOrginatorEn, string EmailDescriptionApproverFr, string EmailDescriptionApproverEn, string emailSubjectApproverFr, string emailSubjectApproverEn, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_TRNRequest_Status(status, statusDescription, actionStatus, statusDescriptionFr, emailDescriptionOriginatorFr, emailDescriptionOrginatorEn, emailSubjectOrginitorFr, emailSubjectOrginatorEn, EmailDescriptionApproverFr, EmailDescriptionApproverEn, emailSubjectApproverFr, emailSubjectApproverEn, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_T_TRNRequest_Type_ProcessRole_Result> Crud_TRNRequest_Type_ProcessRole(int? id, string description, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_T_TRNRequest_Type_ProcessRole(id, description, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public IList<SP_CRUD_KCCRequest_Module_Result> Crud_KCCRequest_Module(string ModuleID, string ModuleDescription, string BodyMailColorCode, string BodyMailColorDescription, string action, string moduleDescriptionFrench)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_KCCRequest_Module(ModuleID, ModuleDescription, moduleDescriptionFrench, BodyMailColorCode, BodyMailColorDescription, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_KCCRequest_Parameter_Result> Crud_KCCRequest_Parametre(string key, string description, string value, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_KCCRequest_Parameter(key, description, value, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public List<SP_Get_TRNRequest_DetailToIntegrate_Result> Get_TRNRequest_DetailToIntegrate()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_DetailToIntegrate().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Approved_TRNRequest_DetailByAdmin(int DetailID)
        {

            //try
            //{
            using (kcc_request_dbEntities db = new kcc_request_dbEntities())
            {
                db.SP_Approve_TRNRequest_DetailByAdmin(DetailID);
            }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
        }
        public void SendMail_TRNRequest_File(string fileName)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_SendMail_TRNRequest_File(fileName);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public List<SP_Get_KCCRequest_IsAdmin_Result> Get_KCCRequestIsAdmin(string loggin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_KCCRequest_IsAdmin(loggin).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_GET_KCCRequest_Logiciel_Result> Get_KCCRequest_Logiciel()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_GET_KCCRequest_Logiciel().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void ITRequest_Reallocate(int DetailId, string comment, string role, string userlogin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reallocate_ITRequest(DetailId, comment, role, userlogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void EWRequest_Reallocate(int EWID, string comment, string role, int status, string userlogin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reallocate_EWRequest(EWID, comment, role, status, userlogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void HRRequest_Reallocate(int HrID, string comment, string role, string userlogin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reallocate_RECTRequest(HrID, comment, role, userlogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void TRNRequest_Reallocate(int TRNID, string comment, string role, string userlogin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reallocate_TRNRequest(TRNID, comment, role, userlogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void DSKRequest_Reallocate(int DSKID, string comment, string role, string userlogin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reallocate_DSKRequest(DSKID, comment, role, userlogin);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public List<SP_Get_ITRequest_Search_Result> Get_ITRequest_Search(string parametre)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_ITRequest_Search(parametre).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_EWRequest_Search_Result> Get_EWRequest_Search(string parametre)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_EWRequest_Search(parametre).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_RECTRequest_Search_Result> Get_RECTRequest_Search(string parametre)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_RECTRequest_Search(parametre).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_TRNRequest_Search_Result> Get_TRNRequest_Search(string parametre)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_TRNRequest_Search(parametre).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DSKRequest_Search_Result> Get_DSKRequest_Search(string parametre)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DSKRequest_Search(parametre).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion


        #region Document control
        public List<SP_Get_DCRequest_ApplicableAreas_ByRequestId_Result> Get_DCRequest_ApplicableAreas_BYRequestId(int requestID) 
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_ApplicableAreas_ByRequestId(requestID).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<SP_Get_DCRequest_ReviewerGroups_ByRequestId_Result> Get_DCRequest_ApplicabilityReviewGroup_BYRequestId(int requestID) 
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_ReviewerGroups_ByRequestId(requestID).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SP_Get_DCRequest_RolloutProcesses_ByRequestId_Result> Get_DCRequest_RolloutProcesses_ByRequestId(int requestID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_RolloutProcesses_ByRequestId(requestID).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Insert_DCRequest_ApplicabilityDetails(int requestId, string strAreaIds, string strReviewerIds, string strRolloutChannelIds)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_DCRequest_applicabilityDetails(requestId, strAreaIds, strReviewerIds, strRolloutChannelIds);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DCRequest_ApplicableAreaConfig_Result> Get_DCRequest_ApplicabilityAreas()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_ApplicableAreaConfig().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DCRequest_ApplicablilityReviewGroupConfig_Result> Get_DCRequest_ApplicabilityReviewGroups()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_ApplicablilityReviewGroupConfig().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DCRequest_RolloutChannelConfig_Result> Get_DCRequest_RolloutChannel()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_RolloutChannelConfig().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DCRequest_Document_Type_Result> Get_DCRequest_DocumentType(string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_Document_Type(login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public decimal DCRequest_Insert(string requestorLogin, string beneficiary, string beneficiaryJobTitle, string description, string comment, string commentPriority, DateTime PlannedDeliveryDate,
            string risk, int priorityID, int CategoryID, string DocNumber, string DocVersionCode, int? DoctypeId, string DocTitle, int? DocLangId, int? DocVersion, DateTime? DocVersionDate, DateTime? DocReviewDate,
            string DocTranslator, bool? DocHardcopy, string DocLink, int? DocTransmittalNo, string AdditionalNoteId, int ServiceID, string CreatedBy,int TypeID, bool applicability)
        {


            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return (db.SP_Insert_DCRequest(requestorLogin, beneficiary, beneficiaryJobTitle, description, comment, commentPriority, PlannedDeliveryDate, risk, priorityID, CategoryID, DocNumber, DocVersionCode,
                           DoctypeId, DocTitle, DocLangId, DocVersion, DocVersionDate, DocReviewDate, DocTranslator, DocHardcopy, DocLink, DocTransmittalNo, AdditionalNoteId, ServiceID, CreatedBy,TypeID, applicability)).FirstOrDefault().Value;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DCRequest_Service_Result> Get_DCRequest_Service(string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_Service().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int Get_DCRequest_ApplicabilityForDepartment(string departementorlogin)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    
                    return (int)db.SP_Check_IsOpertionDep(departementorlogin).FirstOrDefault().IsOperationDep;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DCRequest_ByID_Result1> Get_DCRequest_ById(int ID, string login)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_ByID(ID, login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void DCRequest_Update(int id, string requestorLogin, string requestorName, string beneficiary, string beneficiaryJobTitle, string description, string comment, string commentPriority, DateTime PlannedDeliveryDate,
            string costCenter, string department, string area, string risk, int priorityID, int CategoryID, string DocNumber, string DocVersionCode, int? DoctypeId, string DocTitle, int? DocLangId, int? DocVersion, DateTime? DocVersionDate, DateTime? DocReviewDate,
           string DocTranslator, bool? DocHardcopy, string DocLink, int? DocTransmittalNo, string AdditionalNoteId, int ServiceID, string CreatedBy,int typeID)
        {


            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_DCRequest(id, requestorLogin, requestorName, beneficiary, beneficiaryJobTitle, description, comment, commentPriority, PlannedDeliveryDate, costCenter, department, area, risk, priorityID, CategoryID, DocNumber, DocVersionCode,
                           DoctypeId, DocTitle, DocLangId, DocVersion, DocVersionDate, DocReviewDate, DocTranslator, DocHardcopy, DocLink, DocTransmittalNo, AdditionalNoteId, ServiceID, CreatedBy,typeID);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void DCRequest_AdHoc(int ID, string EmployeeId, string comment, string login) {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Ad_Hoc_DCRequest(ID, EmployeeId, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public void DCRequest_Approve(int ID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_DCRequest(ID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void DCRequest_Back(int ID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Back_DCRequest(ID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void DCRequest_Reallocate(int ID, string action,string comment, string Role, string login, int statusId,DateTime? signDate)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reallocate_DCRequest(ID, action, comment,Role, statusId, login,signDate);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void DCRequest_Reject(int ID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reject_DCRequest(ID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void DCRequest_Return(int ID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Return_DCRequest(ID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<SP_Get_DCRequest_History_Result> Get_DCRequest_History(int ID)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_History(ID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DCRequest_File_Result> Get_DCRequest_File(int ID)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_File(ID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PERFORMANCE OPTIMIZATION: Get file metadata only (no binaries) for fast page loading
        public List<SP_Get_DCRequest_File_Result> Get_DCRequest_File_Metadata(int DCRequestID)
        {
            try
            {
                // SAFE APPROACH: Use existing stored procedure but exclude binary data
                var allFiles = Get_DCRequest_File(DCRequestID);
                
                // Create lightweight versions without the binary File data
                return allFiles.Select(f => new SP_Get_DCRequest_File_Result
                {
                    FileID = f.FileID,
                    DCRequestID = f.DCRequestID,
                    FileName = f.FileName,
                    Type = f.Type,
                    File = null // Exclude binary data for fast loading
                }).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"File metadata loading error: {ex.Message}");
                // Return empty list if there's an error
                return new List<SP_Get_DCRequest_File_Result>();
            }
        }

        // PERFORMANCE OPTIMIZATION: Download specific file only when user requests it
        public SP_Get_DCRequest_File_Result Get_DCRequest_File_Download(int FileID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    // Use raw SQL to get specific file with binary data
                    var result = db.Database.SqlQuery<SP_Get_DCRequest_File_Result>(
                        "SELECT FileID, DCRequestID, FileName, Type, [File] FROM [T_DCRequest_File] WHERE FileID = @FileID",
                        new System.Data.SqlClient.SqlParameter("@FileID", FileID)
                    ).FirstOrDefault();
                    
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"File download error: {ex.Message}");
                return null;
            }
        }

        public void DCRequest_Insert_File(int ID, string fileName, string type, byte[] file)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Insert_DCRequest_File(ID, fileName, type, file);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void DCRequest_SendMail(int ID) {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_SendMail_DCRequest(ID);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IList<SP_CRUD_DCRequest_ProcessRole_User_Result> Crud_DCRequestProcessRoleUser(int? idroleUser, string userLogin, string role, DateTime? startDate, DateTime? endDate, bool? activated, int? type, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_DCRequest_ProcessRole_User(idroleUser, userLogin, role, startDate, endDate, activated, type, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public IList<SP_CRUD_DCRequest_ProcessRole_Result> Crud_DCRequestProcessRole(int? idroleUser, string role, string description, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_DCRequest_ProcessRole(idroleUser, role, description, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IList<SP_CRUD_DCRequest_Status_Result> Crud_DCRequest_Status(int? status, string statusDescription, string statusDescriptionFr, string actionStatus, string emailDescriptionOriginatorFr, string emailDescriptionOrginatorEn, string emailSubjectOrginitorFr, string emailSubjectOrginatorEn, string EmailDescriptionApproverFr, string EmailDescriptionApproverEn, string emailSubjectApproverFr, string emailSubjectApproverEn, string action)
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_CRUD_DCRequest_Status(status, statusDescription, actionStatus, statusDescriptionFr, emailDescriptionOriginatorFr, emailDescriptionOrginatorEn, emailSubjectOrginitorFr, emailSubjectOrginatorEn, EmailDescriptionApproverFr, EmailDescriptionApproverEn, emailSubjectApproverFr, emailSubjectApproverEn, action).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public void DCRequest_InsertHistory(int id, string action,string comment,string role,int statusID,string createBy,DateTime signDate) {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                  db.SP_Insert_DCRequest_History(id,action,comment,role,statusID,createBy,signDate);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<SP_Get_DCRequest_Type_Result> Get_DCRequest_Type()
        {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest_Type().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_DCRequest_Result> Get_DCRequest_Requisition() {

            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_DCRequest().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #endregion
        #region Material Control Methods
        public void Delete_MCRequest_BonEntree_Detail(int id)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Delete_MCRequest_BonEntree_Detail(id);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Delete_MCRequest_BonSortie_Detail(int id)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Delete_MCRequest_BonSortie_Detail(id);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_MCRequest_BonEntree_Detail_Result> Get_MCRequest_BonEntree_Detail()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_BonEntree_Detail().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_MCRequest_BonEntree_Detail_By_Result> Get_MCRequest_BonEntree_Detail_By(string search)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_BonEntree_Detail_By(search).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public List<SP_Get_MCRequest_BonSortie_Detail_By_Result> Get_MCRequest_BonSortie_Detail_By(string search)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_BonSortie_Detail_By(search).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_MCRequest_BonSortie_By_Result> Get_MCRequest_BonSortie_By(string search)
        {
            //try
            //{
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_BonSortie_By(search).ToList();
                }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
        }


        public List<SP_Get_MCRequest_BonEntree_By_Department_Result> Get_MCRequest_BonEntree_By_Department(string search)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_BonEntree_By_Department(search).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_MCRequest_BonEntree_Dedail_By_Department_Result> Get_MCRequest_BonEntree_Dedail_By_Departmen(string search)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_BonEntree_Dedail_By_Department(search).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public virtual List<SP_Get_MCRequest_Sites_Result> Get_MCRequest_Sites()
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_Sites().ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int Insert_MCRequest_BonEntree(string contractNumber, string idCompany,int FromSiteId, int idSite, string department, string reason, string contractorContact, 
                                                   string contractorManager, string escorteurName, string escorteurFunction, string requestor, DateTime dateFrom, DateTime dateTo,
                                                          int quantityTotal, int nombreArticles, string createdBy)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return (int)db.SP_Insert_MCRequest_BonEntree(contractNumber, idCompany, FromSiteId, idSite, department, reason, contractorContact, contractorManager, escorteurName, escorteurFunction, requestor, dateFrom, dateTo, DateTime.Now,
                        quantityTotal, nombreArticles, createdBy, 1).FirstOrDefault().Id.Value;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message + " \n" + ex.InnerException;
                throw ;
            }
        }
        public int Insert_MCRequest_BonEntree_Detail(int idBonEntree, string codeProduit, string designationProduit, int quantity, int idFrom, int idTo, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Insert_MCRequest_BonEntree_Detail(idBonEntree, codeProduit, designationProduit, quantity, idFrom, idTo, dateFrom, dateTo, 1, "", "", DateTime.Now);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int Insert_MCRequest_BonSortie(string idCompany, string department, string employeeID, string escorteurName, string escoteurFunction, DateTime dateSortie, string requestor, string createdBy,  
                                              int quantityTotal, int nombreArticles, string reason, int siteId)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return (int)db.SP_Insert_MCRequest_BonSortie(idCompany, department, employeeID, escorteurName, escoteurFunction, dateSortie, requestor, createdBy, DateTime.Now, 1, quantityTotal, nombreArticles, reason, siteId).FirstOrDefault().Id.Value;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int Insert_MCRequest_BonSortie_Detail(int IdBonSortie, int iDBonEntreeDetail, int quantite,  int iDFrom, int iDTo, string comments)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Insert_MCRequest_BonSortie_Detail(IdBonSortie,iDBonEntreeDetail, quantite, 1, iDFrom, iDTo, "", "", null, comments);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int Insert_MCRequest_Sites(string siteName, string siteDescription, string approverRole)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Insert_MCRequest_Sites(siteName, siteDescription, approverRole);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update_MCRequest_BonEntree(int id, string contractNumber, string idCompany,int FromSiteId, int idSite, string department, string reason, string contractorContact, string contractorManager, string escorteurName, 
                                                        string escorteurFunction, string requestor, DateTime dateFrom, DateTime dateTo, DateTime createdDate, int quantityTotal, int nombreArticles, string createdBy, int idStatus)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_MCRequest_BonEntree(id, contractNumber, idCompany, FromSiteId, idSite, department, reason, contractorContact, contractorManager, escorteurName, escorteurFunction, requestor, dateFrom, dateTo, createdDate, quantityTotal
                        , nombreArticles, createdBy, idStatus);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void Update_MCRequest_BonSortie(int id, string idCompany, string department, string employeeID, string escorteurName, string escoteurFunction, 
                                                            DateTime dateSortie, string requestor, string createdBy, DateTime createdDate, int idStatus, int quantityTotal, int nombreArticles, string reason)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_MCRequest_BonSortie(id, idCompany, department, employeeID, escorteurName, escoteurFunction, dateSortie, requestor, createdBy, createdDate, idStatus, quantityTotal, nombreArticles, reason);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void SP_Update_MCRequest_Sites(int id, string siteName, string siteDescription, string approverRole)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Update_MCRequest_Sites(id, siteName, siteDescription, approverRole);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<SP_Get_MCRequest_BonEntree_By_Result> Get_MCRequest_BonEntree_By(string search)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_BonEntree_By(search).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_MCRequest_History_Result> Get_MCRequest_History(int MCRequestID)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_History(MCRequestID).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //Bon entree
        public void Reject_MCRequest_Detail(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Reject_MCRequest_Detail(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Return_MCRequest_Detail(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Return_MCRequest_Detail(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void SP_Back_MCRequest(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Back_MCRequest(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Hold_MCRequest(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {

                    db.SP_Hold_MCRequest(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void Approve_MCRequest_Bonentree_Detail(int detailID, string comment, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_Approve_MCRequest_Bonentree_Detail(detailID, comment, login);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SP_Get_MCRequest_ToApproveDetail1_Result> Get_MCRequest_ToApproveDetail1(int moduleID, string login)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    return db.SP_Get_MCRequest_ToApproveDetail1(moduleID, login).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void SendMail_MCRequest(int ID, string recipient, string emailDescriptionFr, string emailDescriptionEn, string emailSubjectFr, string emailSujectEn)
        {
            try
            {
                using (kcc_request_dbEntities db = new kcc_request_dbEntities())
                {
                    db.SP_SendMail_MCRequest(ID, recipient, emailDescriptionFr, emailDescriptionEn, emailSubjectFr, emailSujectEn);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //End Bon entree

        #endregion
    }
}
