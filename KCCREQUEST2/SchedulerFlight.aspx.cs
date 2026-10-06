using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using KCCDBAccess;
using System.Configuration;
using System.Globalization;
//using KCCREQUEST2.ServiceReference1;

namespace KCCREQUEST2
{
    public partial class SchedulerFlight : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                //this.Button1.Attributes.Add("OnClick", "self.close()");
                //lblTitle.Text = "Access Denied !";
                //lblMsg.Text = "Access is denied, as the 'department' or 'report to' in SAGE 300 has not yet been setup.";
                ////lblMsg2.Text = "meanwhile you cannot access to FBS ";
                //lblMsg4.Text = "Please contact HR Department to resolve your problem";

                //lblMsg2.Visible = true; lblMsg4.Visible = true; lblMsg5.Visible = true; lblMsg6.Visible = true; lblMsg7.Visible = true;
                //lblTitle.ForeColor = System.Drawing.Color.Red;
                //lblMsg.ForeColor = System.Drawing.Color.Green;
                //lblMsg2.ForeColor = System.Drawing.Color.Green;
                ////lblMsg3.ForeColor = System.Drawing.Color.Green;
                //lblMsg4.ForeColor = System.Drawing.Color.Green;
                //Button1.Visible = true;
                //this.Button1.Attributes.Add("OnClick", "self.close()");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            catch (Exception ex) { }

        }

        [WebMethod]
        public static IList GetEventsMonthWise(DateTime shiftdate)
        {
            string result;

            if (HttpContext.Current.User.Identity.Name.Split('\\').Count() <= 1)
            {
                result = HttpContext.Current.User.Identity.Name; // = "(&(objectClass=user)(sAMAccountname=" +  + "))";
            }
            else
            {
                result = HttpContext.Current.User.Identity.Name.Split('\\')[1];
            }


            IList events = new List<Event>();
            KCCDBWrapper db = new KCCDBWrapper();
            DataSet dt = new DataSet();
            string UserLogin = "gwemba";

            var data = db.Get_TrainingCalendars(UserLogin, shiftdate).ToList();
            foreach (var tout in data)
            {
                //try
                //{
                events.Add(new Event
                {
                    //EventName = tout.Information.ToString(),
                    EventName = tout.InterventionName.ToString() ,
                    StartDate = DateTime.Parse(tout.dtmStart.ToString()).ToString("MM/dd/yyyy") + " " + DateTime.Parse(tout.StartsAt.ToString()).ToString("HH:mm:ss"),
                    EndDate = DateTime.Parse(tout.dtmEnd.ToString()).ToString("MM/dd/yyyy") + " " + DateTime.Parse(tout.StartsAt.ToString()).ToString("HH:mm:ss"),
                    Url = @"NewRequisition_Page.aspx?FlightDate=" + HttpUtility.UrlEncode(tout.dtmStart.ToString().ToString()) + "" + "&" + "ArrPlace=" + HttpUtility.UrlPathEncode(tout.InterventionName.ToString().ToString().ToString()) + "&" + "Seat=" + HttpUtility.UrlPathEncode(tout.SeatsAvailable.ToString().ToString().ToString()) + "&" + "DepPlace=" + HttpUtility.UrlPathEncode(tout.InterventionName.ToString().ToString()) + "",

                    Airline = tout.CalendarColor.ToString(),
                    Seat = tout.SeatsAvailable.ToString(),

                }); 

                //}
                //catch (Exception ex)
                //{
                //    throw (ex);
                //}
            }

            return events;

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<script language='javascript'> { self.close() }</script>");

        }



        public class Event
        {
            public Guid EventID { get { return new Guid(); } }
            public string EventName { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public int ImageType { get; set; }
            public string Url { get; set; }
            public string Airline { get; set; }
            public string Seat { get; set; }

            public string DepLace { get; set; }
            public string ArrPlace { get; set; }
            public string FlightDate { get; set; }
        }


    }
}