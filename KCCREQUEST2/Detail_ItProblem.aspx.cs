using KCCDBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2
{
    public partial class Detail_ItProblem : BasePage
    {
        int id;
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                PopulateDepartement();
                PopulateAffected();
                PopulateLocation();
                PopulateWorkArround();
                PopulateRequestor();

                if (Request.Params["IDITProblem"] != null)
                {
                    try
                    {
                        id = int.Parse(Request.Params["IDITProblem"]);
                        var head = db.Get_ITProblem().Where(x => x.Id == Convert.ToInt32(Request.Params["IDITProblem"]));
                        title.InnerText = id.ToString();
                        txtSubject.Text = head.FirstOrDefault().Comment;
                        txtDescription.Text = head.FirstOrDefault().Description;
                        LstAffected.SelectedValue = head.FirstOrDefault().AffectedID.ToString();
                        LstWorkAround.SelectedValue = head.FirstOrDefault().AroundID.ToString();
                        LstRequestedBy.SelectedValue = head.FirstOrDefault().RequestorLogin;
                        LstLocation.SelectedValue = head.FirstOrDefault().LocationID.ToString();
                        LstDepartment.SelectedValue = head.FirstOrDefault().Department.ToString();
                    txtContactNumber.Text = head.FirstOrDefault().PhoneNumber;

                        dgvFiles.DataSource = db.Get_ITProblemFile(id);
                        dgvFiles.DataBind();

                        enableControle(false);

                        myHpl.Text = "Go To SysAId";
                        myHpl.Target = "_blank";
                        string sysaid = db.Get_KCCRequest_Parametre("ITProblem_Detail_Link");
                        myHpl.NavigateUrl = sysaid + id.ToString(); ;
                    }
                    catch (Exception ex)
                    {

                        //string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                        //lblErreur.Text = error;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);

                        Response.Redirect("pageError.aspx");
                    }
                }
            }
        }


        public void PopulateDepartement()
        {
            try
            {
                LstDepartment.DataSource = db.Get_ITProblem_Department();
                LstDepartment.DataValueField = "value_key";
                LstDepartment.DataTextField = "value_caption";
                LstDepartment.DataBind();
                LstDepartment.Items.Insert(0, "Choose One");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

     
        public void PopulateAffected()
        {
            try
            {

                LstAffected.DataSource = db.Get_ITProblem_Affected();
                LstAffected.DataValueField = "value_key";
                LstAffected.DataTextField = "value_caption";
                LstAffected.DataBind();
                LstAffected.Items.Insert(0, "Choose One");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void PopulateWorkArround()
        {
            try
            {

                LstWorkAround.DataSource = db.Get_ITProblem_WorkAround();
                LstWorkAround.DataValueField = "value_key";
                LstWorkAround.DataTextField = "value_caption";
                LstWorkAround.DataBind();
                LstWorkAround.Items.Insert(0, "Choose One");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void PopulateLocation()
        {
            try
            {

                LstLocation.DataSource = db.Get_DeviceRegistration_Location();
                LstLocation.DataValueField = "value_key";
                LstLocation.DataTextField = "value_caption";
                LstLocation.DataBind();
                LstLocation.Items.Insert(0, "Choose One");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

       
        public void PopulateRequestor()
        {
            try
            {
                var listDepartement = new developpers_dbEntities().SP_Get_Employee().Where(x => !string.IsNullOrEmpty(x.Mail)).ToList();//new developpers_dbEntities().SP_Get_Employee() ** Developper.Employee.Get_Employee().Where(x => x.Sources == "VIP").ToList();
                LstRequestedBy.DataSource = listDepartement;
                LstRequestedBy.DataTextField = "FirstName";
                LstRequestedBy.DataValueField = "Login";
                LstRequestedBy.DataBind();
                LstRequestedBy.Items.Insert(0, "Choose One");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void enableControle(bool v) {
            txtDescription.Enabled = v;
            txtSubject.Enabled = v;
            LstAffected.Enabled = v;
            LstWorkAround.Enabled = v;
            LstRequestedBy.Enabled = v;
            txtContactNumber.Enabled = v;
            LstLocation.Enabled = v;
            LstDepartment.Enabled = v;
        }

        protected void DownloadAttachment(string mimeType, string fileName, byte[] file)
        {
            try
            {
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}\"", fileName));
                Response.BinaryWrite(file);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnview_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request.Params["IDITProblem"]);

                Button btn = (Button)sender;


                foreach (GridViewRow row in dgvFiles.Rows)
                {
                    Button btn1 = (Button)row.FindControl("btnview");

                    if (btn1.Equals(btn))
                    {

                        Label fileID = (Label)row.FindControl("txtFileID");
                        Label txtType = (Label)row.FindControl("txtType");
                        //Label txtFile = (Label)row.FindControl("File");
                        Label filename = (Label)row.FindControl("txtFileName");

                        byte[] bytes = (byte[])db.Get_ITProblemFile(id).Where(x => x.FileID.ToString() == fileID.Text).FirstOrDefault().File.ToArray();

                        DownloadAttachment(txtType.Text, filename.Text, bytes);

                        break;
                    }



                }
            }
            catch (FormatException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "FormatExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (NullReferenceException ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "NullReferenceExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (IndexOutOfRangeException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "IndexOutOfRangeExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
            catch (ArithmeticException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "ArithmeticExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (TimeoutException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "TimeoutExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (SystemException ex)
            {
                string error = (string)GetGlobalResourceObject("LocalizedText", "SystemExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

            catch (Exception ex)
            {

                string error = (string)GetGlobalResourceObject("LocalizedText", "ExceptionError");
                lblErreur.Text = error;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }
        }

        protected void btnGoSysaid_Click(object sender, EventArgs e)
        {
            string sysaid = db.Get_KCCRequest_Parametre("ITProblem_Detail_Link");

            this.ClientScript.RegisterStartupScript(this.GetType(), "aa", "window.open('" + sysaid + "" + title.InnerText + "','_blank');", true);
        }
    }
}