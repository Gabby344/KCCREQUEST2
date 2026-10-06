using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
namespace KCCREQUEST2
{
    public class ProfilUsers
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
     

        private string loggedUser = HttpContext.Current.User.Identity.Name;
        
        public void BindMyProfile(Label lblNameUser,Image Image1, Image Image2, Label usercompte,Label mat,Label Department,Label fonction,Label adressemail)
        {
           
           

            try
            {

                ADResultObject obj = utility.GetInformationFromADC(loggedUser).SingleOrDefault();
                string login = string.Empty;
                string matricule = obj.EmployeeID;

                string Log = obj.Username;
                var agent = db.GetUserProfile(Log);
                if (agent != null)
                { 



                    byte[] bytes = (byte[])agent.EImage.ToArray();

                    lblNameUser.Text = agent.DisplayName;


                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    Image1.ImageUrl = "data:image/png;base64," + base64String;
                    Image2.ImageUrl = "data:image/png;base64," + base64String;

                    usercompte.Text = agent.Samaccountname;

                    mat.Text = agent.EmployeeID;

                    Department.Text = agent.Department;

                    fonction.Text = agent.Title;

                    adressemail.Text = agent.Mail;


                }
               }
                catch (Exception ex) { }
        }
    }

    //public static class MyParametres {
    //    public static string lang { get; set; }
    //    public static int homeLang { get; set; }

    //    public static int homePage { get; set; }
    //}
}