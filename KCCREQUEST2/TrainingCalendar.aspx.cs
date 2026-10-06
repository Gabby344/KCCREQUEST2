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
    public partial class TrainingCalendar : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        private static string CodeTraining = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["codeTraining"] != null)
            {
                CodeTraining = Request.Params["codeTraining"];
            }
            else {
                CodeTraining = null;
            }
        }

        [WebMethod]
        public static IList GetEventsMonthWise(DateTime shiftdate)
        {
            string result;
            //shiftdate = shiftdate.AddDays(2);
            //string month = shiftdate.ToString().Split('/')[0];
            //string day = shiftdate.ToString().Split('/')[1];
            //string year = shiftdate.ToString().Split('/')[2].Substring(0,4);
            //string newDateString = year + "-" + month + "-" + day;
            //DateTime newDate = DateTime.Parse(newDateString);
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
            dynamic data;
            if (CodeTraining != null)
            {
              data = db.Get_TrainingCalendarByTrainingID(UserLogin, shiftdate, int.Parse(CodeTraining)).ToList();

            }
            else
            {
               data = db.Get_TrainingCalendars(UserLogin, shiftdate).ToList();
            }

            int cptCours = 0;

            foreach (var tout in data)
            {
                cptCours++;
                try
                {
                    events.Add(new Event
                    {
                        EventName = tout.InterventionName.ToString(),
                                StartDate = DateTime.Parse(tout.dtmStart.ToString()).ToString("yyyy/MM/dd") + " " + DateTime.Parse(tout.StartsAt.ToString()).ToString("HH:mm:ss"),
                                EndDate = DateTime.Parse(tout.dtmEnd.ToString()).ToString("yyyy/MM/dd") + " " + DateTime.Parse(tout.StartsAt.ToString()).ToString("HH:mm:ss"),
                   
                               Url = @"New_TrainingRequest.aspx?StartDate=" + HttpUtility.UrlEncode(tout.dtmStart.ToString().ToString()) + "" + "&" + "Training=" + HttpUtility.UrlPathEncode(tout.InterventionName.ToString().ToString().ToString()) + "&" + "Seat=" + HttpUtility.UrlPathEncode(tout.SeatsAvailable.ToString().ToString().ToString()) + "&" + "TrainingVenue=" + HttpUtility.UrlPathEncode(tout.VenueName.ToString().ToString()) + "&" + "TrainingEventID="+HttpUtility.UrlPathEncode(tout.TrainingEventID.ToString().ToString()) +"&" + "TrainingType=Internal"+ "",

                                Airline = tout.CalendarColor.ToString(),
                               Seat = tout.SeatsAvailable.ToString(),
                                venueName=tout.VenueName
                });

            }
                catch (Exception ex)
            {
                throw (ex);
            }
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
            public string venueName { get; set; }
            
        }


    }
}