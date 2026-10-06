using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Microsoft.Reporting.WebForms;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Data;
using KCCDBAccess;

namespace KCCREQUEST2
{
    public partial class ViewRapports : BasePage
    {
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    

                    if (Request.Params["IDITRequest"] != null)
                    {
                        new Reporting().renderReport(ReportViewer1, "KCC REQUEST", "/Reports/KCC AppDev/KCC REQUEST/IT_Request_Details", Request.Params["IDITRequest"]);

                    }
                    else if (Request.Params["IDTRNRequest"] != null) {

                        new Reporting().renderReport(ReportViewer1, "KCC REQUEST", "/Reports/KCC AppDev/KCC REQUEST/Training_Request_Details", Request.Params["IDTRNRequest"]);
                    }
                    else if (Request.Params["IDRECTRequest"] != null)
                    {

                        new Reporting().renderReport(ReportViewer1, "KCC REQUEST", "/Reports/KCC AppDev/KCC REQUEST/Recruitment_Request_Details", Request.Params["IDRECTRequest"]);
                    }
                    else if (Request.Params["IDRECTRequestExp"] != null)
                    {

                        new Reporting().renderReport(ReportViewer1, "KCC REQUEST", "/Reports/KCC AppDev/KCC REQUEST/ExpatriateRecrutmentForm", Request.Params["IDRECTRequestExp"]);
                    }
                    else if (Request.Params["IDDSKRequest"] != null)
                    {

                        new Reporting().renderReport(ReportViewer1, "KCC REQUEST", "/Reports/KCC AppDev/KCC REQUEST/DSK_Request_Details", Request.Params["IDDSKRequest"]);
                    }
                    else if (Request.Params["IDEWORequest"] != null)
                    {

                        new Reporting().renderReport(ReportViewer1, "KCC REQUEST", "/Reports/KCC AppDev/KCC REQUEST/EWRDetails", Request.Params["IDEWORequest"]);
                    }
                    else
                    {
                        ReportViewer1.ApplyStyleSheetSkin(this.Page);
                        //populateControls();
                        LoadDefaultReport(sender, e);
                        BindReports();
                        ddlReport.SelectedIndex = 0;
                       
                    }

                   

                }
            }
            catch { }
        }
        public enum MessageType { Success, Error, Info, Warning };
        protected void ShowMessage(string Message, MessageType type)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

        }

        KCCDBWrapper db = new KCCDBWrapper();
        protected void LoadDefaultReport(object sender, EventArgs e)
        {
            try
            {

                if (test.Text == "")
                {
                    ddlReport.SelectedIndex = 0;
                    //new Reporting().renderReport(ReportViewer1, "KCC REQUEST", "/KCC AppDev/KCC REQUEST/All_Requests_by_Module_Dashboard");
                    new Reporting().renderReport(ReportViewer1, "KCC REQUEST", "/Reports/KCC AppDev/KCC REQUEST/All_Requests_by_Module_Dashboard");
                }



            }
            catch (Exception ex)
            {

            }
        }

        protected void BindReports()
        {
            try
            {
                if (!IsPostBack)
                {
                    DataSet dt = new DataSet();

                    ddlReport.DataSource = db.Get_KCCRequest_Report(utility.Login(loggedUser));

                    ddlReport.DataTextField = "Description";

                    ddlReport.DataValueField = "Value";
                    ddlReport.DataBind();
                }  

            }
            catch (Exception ed) { ShowMessage("Sorry There is a problem with a network" + ed.Message, MessageType.Info); }
        }

        protected void populateControls()
        {
            try
            {

                //ddlReport.DataSource = new Reporting().getReport();
                //ddlReport.DataTextField = "Name";
                //ddlReport.DataValueField = "Path";
                //ddlReport.DataBind();

            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlReport.SelectedIndex == 0)
                {
                    new Reporting().renderReport(ReportViewer1, "KCC REQUEST", "/Reports/KCC AppDev/KCC REQUEST/All_Requests_by_Module_Dashboard");

                }
                else if (ddlReport.SelectedItem.Text == "Requests I Worked On")
                {
                    new Reporting().renderReport(ReportViewer1, "KCC REQUEST", ddlReport.SelectedValue, "UserLogin", utility.Login(loggedUser));
                }
                else
                {
                    new Reporting().renderReport(ReportViewer1, "KCC REQUEST", ddlReport.SelectedValue.Trim());
                }
               
                 
                    
               
            }
            catch (Exception ex)
            {

            }
        }

    }
}