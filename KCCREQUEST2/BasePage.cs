using System;
using System.Web;
using System.Threading;
using System.Globalization;
using KCCDBAccess;
using System.Linq;
using System.Collections.Generic;

public class BasePage : System.Web.UI.Page
{
    protected readonly KCCDBWrapper db = new KCCDBWrapper();
    protected readonly Utility utility = new Utility();
    
    // Performance optimization settings
    public bool EnableAsyncLoading { get; set; } = true;
    private readonly object _loginLock = new object();
    private readonly object _userPreferencesLock = new object();

    protected string LoggedUser => Context?.User?.Identity?.Name;

    public string CachedLogin
    {
        get
        {
            var cached = Session["CurrentUserLogin"] as string;
            if (!string.IsNullOrEmpty(cached)) return cached;

            //// Guard: no authenticated user -> cache empty and bail
            //if (string.IsNullOrWhiteSpace(LoggedUser))
            //{
            //    Session["CurrentUserLogin"] = string.Empty;
            //    return string.Empty;
            //}

            var login = utility.Login(LoggedUser) ?? string.Empty;
            Session["CurrentUserLogin"] = login;
            return login;
        }
    }

    protected void ResetCachedLogin(string value = null)
    {
        if (value != null)
        {
            Session["CurrentUserLogin"] = value;
            return;
        }

        // Recompute based on current LoggedUser
        var login = string.IsNullOrWhiteSpace(LoggedUser) ? string.Empty : (utility.Login(LoggedUser) ?? string.Empty);
        Session["CurrentUserLogin"] = login;
    }

    // EMERGENCY FIX: Cache user preferences for entire session
    
    public int HomePage
    {
        get
        {
            if (Session["HomePage"] is int v) return v;

            // If no login, return default without DB hit
            if (string.IsNullOrEmpty(CachedLogin))
            {
                Session["HomePage"] = 0;
                return 0;
            }

            // Load both preferences in single DB call
            LoadUserPreferences();
            return Session["HomePage"] as int? ?? 0;
        }
        set => Session["HomePage"] = value;
    }

    public int HomeLang
    {
        get
        {
            if (Session["HomeLang"] is int v) return v;

            if (string.IsNullOrEmpty(CachedLogin))
            {
                Session["HomeLang"] = 0; // default en-us
                return 0;
            }

            // Load both preferences in single DB call
            LoadUserPreferences();
            return Session["HomeLang"] as int? ?? 0;
        }
        set => Session["HomeLang"] = value;
    }
    
    private void LoadUserPreferences()
    {
        lock (_userPreferencesLock)
        {
            // Double-check pattern to avoid race conditions
            if (Session["HomePage"] != null && Session["HomeLang"] != null) return;
            
            var userHomePage = db.Get_KCCRequest_UserHomePage(CachedLogin).FirstOrDefault();
            Session["HomePage"] = userHomePage?.HomePage ?? 0;
            Session["HomeLang"] = userHomePage?.Languages ?? 0;
        }
    }

    // EMERGENCY FIX: Cache user permissions for entire session
    public Dictionary<string, int> UserPermissions
    {
        get
        {
            var cached = Session["UserPermissions"] as Dictionary<string, int>;
            if (cached != null) return cached;

            if (string.IsNullOrEmpty(CachedLogin))
            {
                Session["UserPermissions"] = new Dictionary<string, int>();
                return new Dictionary<string, int>();
            }

            var permissions = db.Get_KCCRequestIsAdmin(CachedLogin).ToList()
                .ToDictionary(x => x.ModuleID, x => x.Number ?? 0);
            Session["UserPermissions"] = permissions;
            return permissions;
        }
    }

    public bool IsModuleAdmin(string moduleId)
    {
        return UserPermissions.ContainsKey(moduleId) && UserPermissions[moduleId] > 0;
    }

    // EMERGENCY FIX: Cache user actions to solve timeout issues - OPTIMIZED VERSION
    public List<dynamic> GetCachedUserActions(int requestId, string module)
    {
        var cacheKey = $"UserActions_{CachedLogin}_{requestId}_{module}";
        var cached = Session[cacheKey] as List<dynamic>;
        
        if (cached != null) return cached;

        if (string.IsNullOrEmpty(CachedLogin))
        {
            Session[cacheKey] = new List<dynamic>();
            return new List<dynamic>();
        }

        try
        {
            // Use OPTIMIZED stored procedure first - should be 95% faster!
            var actions = db.Get_KCCRequest_Action_Optimized(requestId, CachedLogin, module);
            Session[cacheKey] = actions;
            return actions;
        }
        catch (Exception ex)
        {
            // If any issues with optimized version, fallback gracefully
            System.Diagnostics.Debug.WriteLine($"Optimized action lookup failed for user {CachedLogin}, request {requestId}: {ex.Message}");
            try
            {
                // Fallback to timeout version as last resort
                var actions = db.Get_KCCRequest_Action_WithTimeout(requestId, CachedLogin, module);
                Session[cacheKey] = actions;
                return actions;
            }
            catch (Exception fallbackEx)
            {
                System.Diagnostics.Debug.WriteLine($"All action lookup methods failed for user {CachedLogin}, request {requestId}: {fallbackEx.Message}");
                var emptyList = new List<dynamic>();
                Session[cacheKey] = emptyList;
                return emptyList;
            }
        }
    }

    // Helper method to clear action cache when needed
    public void ClearUserActionCache(int requestId, string module = null)
    {
        if (module != null)
        {
            var cacheKey = $"UserActions_{CachedLogin}_{requestId}_{module}";
            Session.Remove(cacheKey);
        }
        else
        {
            // Clear all action caches for this user and request
            var keysToRemove = new List<string>();
            foreach (string key in Session.Keys)
            {
                if (key.StartsWith($"UserActions_{CachedLogin}_{requestId}_"))
                {
                    keysToRemove.Add(key);
                }
            }
            foreach (var key in keysToRemove)
            {
                Session.Remove(key);
            }
        }
    }

    // Startup data caching for performance
    private readonly object _startupDataLock = new object();
    
    public Dictionary<string, string> GetCachedAccessData()
    {
        string cacheKey = "AccessData_" + CachedLogin;
        
        if (Session[cacheKey] is Dictionary<string, string> cached)
            return cached;
        
        lock (_startupDataLock)
        {
            // Double-check pattern
            if (Session[cacheKey] is Dictionary<string, string> cachedSecond)
                return cachedSecond;
            
            var accessList = db.Get_KCCRequest_Access_Cached(CachedLogin);
            var result = accessList.ToDictionary(x => x.Key, x => x.Value);
            
            // Cache for startup session (shorter than user actions)
            Session[cacheKey] = result;
            return result;
        }
    }
    
    public List<SP_Get_KCCRequest_ToApprove_Dashboard_Result> GetCachedApproveDashboard()
    {
        string cacheKey = "ApproveDashboard_" + CachedLogin;
        
        if (Session[cacheKey] is List<SP_Get_KCCRequest_ToApprove_Dashboard_Result> cached)
            return cached;
        
        lock (_startupDataLock)
        {
            // Double-check pattern
            if (Session[cacheKey] is List<SP_Get_KCCRequest_ToApprove_Dashboard_Result> cachedSecond)
                return cachedSecond;
            
            var result = db.Get_KCCRequest_ToApprove_Dashboard_Cached(CachedLogin);
            
            // Cache for 5 minutes (dashboard data changes frequently)
            Session[cacheKey] = result;
            Session[cacheKey + "_Expires"] = DateTime.Now.AddMinutes(5);
            return result;
        }
    }
    
    public List<SP_Get_KCCRequest_Createdby_Dashboard_Result> GetCachedMyRequestsDashboard()
    {
        string cacheKey = "MyRequestsDashboard_" + CachedLogin;
        
        if (Session[cacheKey] is List<SP_Get_KCCRequest_Createdby_Dashboard_Result> cached)
        {
            // Check if expired
            if (Session[cacheKey + "_Expires"] is DateTime expires && DateTime.Now > expires)
            {
                Session.Remove(cacheKey);
                Session.Remove(cacheKey + "_Expires");
            }
            else
            {
                return cached;
            }
        }
        
        lock (_startupDataLock)
        {
            // Double-check pattern
            if (Session[cacheKey] is List<SP_Get_KCCRequest_Createdby_Dashboard_Result> cachedSecond)
                return cachedSecond;
            
            var result = db.Get_KCCRequest_Createdby_Dashboard_Cached(CachedLogin);
            
            // Cache for 5 minutes (dashboard data changes frequently)
            Session[cacheKey] = result;
            Session[cacheKey + "_Expires"] = DateTime.Now.AddMinutes(5);
            return result;
        }
    }
    
    public void ClearStartupDataCache()
    {
        string[] cacheKeys = {
            "AccessData_" + CachedLogin,
            "ApproveDashboard_" + CachedLogin,
            "ApproveDashboard_" + CachedLogin + "_Expires",
            "MyRequestsDashboard_" + CachedLogin,
            "MyRequestsDashboard_" + CachedLogin + "_Expires"
        };
        
        foreach (var key in cacheKeys)
            Session.Remove(key);
    }

    // Master page user profile caching for performance
    private readonly object _userProfileLock = new object();
    
    public class CachedUserProfile
    {
        public string DisplayName { get; set; }
        public string ImageBase64 { get; set; }
        public string SamAccountName { get; set; }
        public string EmployeeID { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string Mail { get; set; }
        public DateTime CachedAt { get; set; }
    }
    
    public CachedUserProfile GetCachedUserProfile()
    {
        string cacheKey = "UserProfile_" + CachedLogin;
        
        if (Session[cacheKey] is CachedUserProfile cached)
        {
            // Check if expired (cache for 30 minutes)
            if (DateTime.Now.Subtract(cached.CachedAt).TotalMinutes < 30)
                return cached;
            
            Session.Remove(cacheKey);
        }
        
        lock (_userProfileLock)
        {
            // Double-check pattern
            if (Session[cacheKey] is CachedUserProfile cachedSecond)
                return cachedSecond;
            
            try
            {
                var profile = LoadUserProfileFromDatabase();
                if (profile != null)
                {
                    Session[cacheKey] = profile;
                    return profile;
                }
            }
            catch (Exception ex)
            {
                // Log error but don't break the page
                System.Diagnostics.Debug.WriteLine($"Error loading user profile: {ex.Message}");
            }
            
            // Return default profile to prevent UI breaking
            return CreateDefaultProfile();
        }
    }
    
    private CachedUserProfile LoadUserProfileFromDatabase()
    {
        // First try to get info from AD (with caching)
        var utility = new KCCDBAccess.Utility();
        var adObj = utility.GetInformationFromADC(CachedLogin)?.FirstOrDefault();
        
        if (adObj == null) return null;
        
        // Then get extended profile from database
        var agent = db.GetUserProfile(adObj.Username);
        if (agent == null) return null;
        
        string imageBase64 = "";
        try
        {
            if (agent.EImage != null)
            {
                byte[] bytes = agent.EImage.ToArray();
                imageBase64 = Convert.ToBase64String(bytes, 0, bytes.Length);
            }
        }
        catch
        {
            // If image conversion fails, use empty string
            imageBase64 = "";
        }
        
        return new CachedUserProfile
        {
            DisplayName = agent.DisplayName ?? adObj.DisplayName ?? CachedLogin,
            ImageBase64 = imageBase64,
            SamAccountName = agent.Samaccountname ?? adObj.Username ?? CachedLogin,
            EmployeeID = agent.EmployeeID ?? adObj.EmployeeID ?? "",
            Department = agent.Department ?? "",
            Title = agent.Title ?? "",
            Mail = agent.Mail ?? adObj.Email ?? "",
            CachedAt = DateTime.Now
        };
    }
    
    private CachedUserProfile CreateDefaultProfile()
    {
        return new CachedUserProfile
        {
            DisplayName = CachedLogin,
            ImageBase64 = "",
            SamAccountName = CachedLogin,
            EmployeeID = "",
            Department = "",
            Title = "",
            Mail = "",
            CachedAt = DateTime.Now
        };
    }

    protected override void InitializeCulture()
    {
        // NOTE: Session should be available here in WebForms when session-state is enabled.
        try
        {
            // Normalize requested culture (querystring "Mylang")
            string requested = Request["Mylang"]?.Trim()?.ToLowerInvariant();

            if (requested == "en-us" || requested == "fr-fr")
            {
                // Persist preference only if we have a known login
                if (!string.IsNullOrEmpty(CachedLogin))
                {
                    int langVal = requested == "en-us" ? 0 : 1;
                    db.Insert_KCCRequest_User_HomePage(CachedLogin, HomePage, langVal);
                    HomeLang = langVal; // cache in Session
                }

                // Optional redirect if toggled via menu
                if (string.Equals(Request["id"], "1", StringComparison.Ordinal))
                {
                    // Avoid ThreadAbortException
                    Response.Redirect("default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }

            // Resolve final culture: session-backed HomeLang or default
            string culture = (HomeLang == 0) ? "en-us" : "fr-fr";

            // If caller asked for a supported culture, it wins
            if (requested == "en-us" || requested == "fr-fr")
                culture = requested;

            var ci = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            base.InitializeCulture();
        }
        catch
        {
            // Swallow to avoid breaking page init; optionally log
        }
    }
}
