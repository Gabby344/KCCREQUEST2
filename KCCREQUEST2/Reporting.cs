using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KCCDBAccess;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Security.Principal;

namespace KCCREQUEST2
{
    public class Reporting
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        public void renderReport(ReportViewer reportViewer, String Folder, String Report)
        {
            try
            {

                // Set the processing mode for the ReportViewer to Remote
                reportViewer.ProcessingMode = ProcessingMode.Remote;

                ServerReport serverReport = reportViewer.ServerReport;

                // Set the report server URL and report path
                string reportServer = db.Get_KCCRequest_Parametre("ReportServer");
                //string reportServer = "http://cdktgsvc4db005/Reportserver_l1";
                serverReport.ReportServerUrl = new Uri(reportServer);
                serverReport.ReportPath = Report;

                // Create the report parameter
                ReportParameter param1 = new ReportParameter();
                param1.Name = "Status";
                param1.Values.Add("Open");

                // Set the report parameters for the report
                //reportViewer.ServerReport.SetParameters(new ReportParameter[] { param1 });
                reportViewer.ShowZoomControl = true;

                //make sure it zooms to the percent defined in the aspx
                reportViewer.ZoomMode = ZoomMode.Percent;
                //reportViewer.SizeToReportContent = true; // Fit report to screen



            }
            catch (Exception ex)
            {

            }
        }

        public void renderReport(ReportViewer reportViewer, String Folder, String Report, string RequestID)
        {
            try
            {

                // Set the processing mode for the ReportViewer to Remote
                reportViewer.ProcessingMode = ProcessingMode.Remote;

                ServerReport serverReport = reportViewer.ServerReport;

                // Set the report server URL and report path
                string reportServer = db.Get_KCCRequest_Parametre("ReportServer");
                serverReport.ReportServerUrl = new Uri(reportServer);
                serverReport.ReportPath = Report;

                // Create the report parameter
                if (!string.IsNullOrEmpty(RequestID))
                {
                    ReportParameter param1 = new ReportParameter();
                    param1.Name = "ID";
                    param1.Values.Add(RequestID);

                    // Set the report parameters for the report
                    reportViewer.ServerReport.SetParameters(new ReportParameter[] { param1 });
                }

                reportViewer.ShowZoomControl = true;

                //make sure it zooms to the percent defined in the aspx
                reportViewer.ZoomMode = ZoomMode.Percent;
                //reportViewer.SizeToReportContent = true; // Fit report to screen



            }
            catch (Exception ex)
            {

            }
        }
        public void renderReport(ReportViewer reportViewer, String Folder, String Report, string reportParameterName, string reportParameterValue)
        {
            try
            {

                // Set the processing mode for the ReportViewer to Remote
                reportViewer.ProcessingMode = ProcessingMode.Remote;

                ServerReport serverReport = reportViewer.ServerReport;

                // Set the report server URL and report path
                string reportServer = db.Get_KCCRequest_Parametre("ReportServer");
                serverReport.ReportServerUrl = new Uri(reportServer);
                serverReport.ReportPath = Report;

                // Create the report parameter
                if (!string.IsNullOrEmpty(reportParameterName))
                {
                    ReportParameter param1 = new ReportParameter();
                    param1.Name = reportParameterName;
                    param1.Values.Add(reportParameterValue);

                    // Set the report parameters for the report
                    reportViewer.ServerReport.SetParameters(new ReportParameter[] { param1 });
                }

                reportViewer.ShowZoomControl = true;

                //make sure it zooms to the percent defined in the aspx
                reportViewer.ZoomMode = ZoomMode.Percent;
                //reportViewer.SizeToReportContent = true; // Fit report to screen



            }
            catch (Exception ex)
            {

            }
        }

        public List<Report> getReport()
        {


            List<Report> reportList = new List<Report>();

            var data = db.Get_KCCRequest_Report(utility.Login(loggedUser));

            if (data != null)
            {
                foreach (var tout in data)
                {

                    reportList.Add(new Report
                    {
                        Path = tout.Value,
                        Name = tout.Description,
                        ID = tout.Key,
                    });
                }
            }

            return reportList;

        }



        public List<Report> getReportBYiD(string ID)
        {

            List<Report> reportList = new List<Report>();

            DataTable dsFieldVisibility = new DataTable();
            var data = db.Get_KCCRequest_Report(utility.Login(loggedUser)).Where(x => x.Key == ID).ToList();

            if (data != null)
            {
                foreach (var tout in data)
                {

                    reportList.Add(new Report
                    {
                        Path = tout.Value,
                        Name = tout.Description,
                        ID = tout.Key,
                    });
                }
            }

            return reportList;

        }
    }
    public partial class HR_Reports
    {

        public int ID { get; set; }


        public string Name { get; set; }

        public string Path { get; set; }


        public virtual IEnumerable<HR_Reports> TMS_Report { get; set; }
    }
    [Serializable]
    public sealed class ReportServerNetworkCredentials : IReportServerCredentials
    {
        #region IReportServerCredentials Members

        /// <summary>
        /// Provides forms authentication to be used to connect to the report server.
        /// </summary>
        /// <param name="authCookie">A Report Server authentication cookie.</param>
        /// <param name="userName">The name of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="authority">The authority to use when authenticating the user, such as a Microsoft Windows domain.</param>
        /// <returns></returns>
        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName,
            out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            return false;
        }

        /// <summary>
        /// Specifies the user to impersonate when connecting to a report server.
        /// </summary>
        /// <value></value>
        /// <returns>A WindowsIdentity object representing the user to impersonate.</returns>
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Returns network credentials to be used for authentication with the report server.
        /// </summary>
        /// <value></value>
        /// <returns>A NetworkCredentials object.</returns>
        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                string userName = "gneema";
                string domainName = "kol";
                string password = "Pentateuques5";

                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion
    }


    public class Report
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
    }
}
