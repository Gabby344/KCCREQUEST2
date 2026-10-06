using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Globalization;


namespace KCCDBAccess
{
    public class Utility
    {
        // Cache AD lookups for performance (static for application-wide caching)
        private static readonly Dictionary<string, List<ADResultObject>> _adCache = new Dictionary<string, List<ADResultObject>>();
        private static readonly object _adCacheLock = new object();
        private static readonly TimeSpan _adCacheTimeout = TimeSpan.FromMinutes(30);
        
        public List<ADResultObject> GetInformationFromADC(string strUser)
        {
            strUser = "gwemba";

            // Check cache first
            lock (_adCacheLock)
            {
                string cacheKey = strUser + "_" + DateTime.Now.ToString("yyyyMMddHH"); // Cache for 1 hour buckets
                if (_adCache.ContainsKey(cacheKey))
                {
                    return _adCache[cacheKey];
                }
                
                // Clean old cache entries (keep cache size reasonable)
                var expiredKeys = _adCache.Keys.Where(k => 
                {
                    var parts = k.Split('_');
                    if (parts.Length == 2 && DateTime.TryParseExact(parts[1], "yyyyMMddHH", null, DateTimeStyles.None, out DateTime cacheTime))
                    {
                        return DateTime.Now.Subtract(cacheTime) > _adCacheTimeout;
                    }
                    return true; // Remove malformed keys
                }).ToList();
                
                foreach (var key in expiredKeys)
                {
                    _adCache.Remove(key);
                }
            }

            try
            {
                var strRetVal = new List<ADResultObject>();
                List<developper.SP_Get_EmployeeByLoginResult> result;
                
                // Handle domain username format
                result = strUser.Contains("\\") 
                    ? Developper.Employee._Get_EmployeeByLogin(strUser.Split('\\')[1])
                    : Developper.Employee._Get_EmployeeByLogin(strUser);

                if (result != null)
                {
                    strRetVal.AddRange(result.Select(res => new ADResultObject
                    {
                        EmployeeID = res.EmployeeId,
                        DisplayName = res.FirstName?.ToString(),
                        Username = res.Login?.ToString(),
                        Email = res.Mail?.ToString()
                    }));
                }

                // Cache the result
                lock (_adCacheLock)
                {
                    string cacheKey = strUser + "_" + DateTime.Now.ToString("yyyyMMddHH");
                    _adCache[cacheKey] = strRetVal;
                }

                return strRetVal;
            }
            catch (Exception)
            {
                throw; // Consider wrapping in a custom exception with more context
            }
        }

        public string Login(string loggedUser)
        {

            try
            {
                var obj = GetInformationFromADC(loggedUser).SingleOrDefault();
                return obj?.Username;
            }
            catch (Exception)
            {
                return null; // Consider logging the error
            }
        }

        public bool IsAdmin(string login)
        {
            try
            {
                var kccWrapper = new KCCDBWrapper();
                var adminCheck = kccWrapper.Get_KCCRequestIsAdmin(login)
                    .FirstOrDefault(x => x.ModuleID == "Parameter");
                    
                return adminCheck?.Number != 0;
            }
            catch (Exception)
            {
                throw; // Consider wrapping in a custom exception
            }
        }


    }


    public class ADResultObject
    {

        private String displayName;
        public String DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private String email;
        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        private String sAMAccountName;
        public String Username
        {
            get { return sAMAccountName; }
            set { sAMAccountName = value; }
        }


        private String employeeiD;
        public String EmployeeID
        {
            get { return employeeiD; }
            set { employeeiD = value; }
        }


    }
}