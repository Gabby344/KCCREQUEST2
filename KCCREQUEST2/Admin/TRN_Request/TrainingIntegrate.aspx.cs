using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;

namespace KCCREQUEST2.Admin.TRN_Request
{
    public partial class TrainingIntegrate : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;

        DataTable tbitems = new DataTable();
        DataRow dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "TRNRequest").FirstOrDefault().Number == 0)
                {

                    Response.Redirect("../default_admin.aspx");
                }
                createtable();
                dgvMyallrequest.DataSource = db.Get_TRNRequest_DetailToIntegrate();
            dgvMyallrequest.DataBind();
            }
        }

        public void createtable()
        {
            tbitems.Columns.Add("TRNRequestID", typeof(string));
            tbitems.Columns.Add("EmployeeID", typeof(string));
            tbitems.Columns.Add("EmployeeName", typeof(string));
            tbitems.Columns.Add("Course", typeof(string));

            dr = tbitems.NewRow();
            ViewState["table1"] = tbitems;

        }

        protected void Addnewrow(string TrainingRequestID, string EmployeeID, string EmployeeName, string Course)
        {
            tbitems = (DataTable)ViewState["table1"];
            dr = tbitems.NewRow();
            dr["TRNRequestID"] = TrainingRequestID;
            dr["EmployeeID"] = EmployeeID;
            dr["EmployeeName"] = EmployeeName;
            dr["Course"] = Course;
            tbitems.Rows.Add(dr);

            dgIntegration.DataSource = tbitems;
            dgIntegration.DataBind();
        }

        protected void Selectall_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvMyallrequest.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    if (chkRow.Enabled == true)
                        chkRow.Checked = true;
                }
            }
        }

        protected void Deselectall_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvMyallrequest.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Enabled == true)
                        chkRow.Checked = false;
                }
            }
        }

        protected void Inverse_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvMyallrequest.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Enabled == true)
                    {

                        if (chkRow.Checked == true) chkRow.Checked = false;
                        else if (chkRow.Checked == false) chkRow.Checked = true;
                    }

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            SelectRowsCheckGrid();
            foreach (GridViewRow row in dgvMyallrequest.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    Label EmployeeID = (Label)row.FindControl("lblEmployeeID");
                    Label EmployeeName = (Label)row.FindControl("lblEmployeeName");
                    Label Cours = (Label)row.FindControl("lblCours");
                    Label DetailID = (Label)row.FindControl("lblID");

                    if (chkRow.Checked == true)
                    {
                        db.Approved_TRNRequest_DetailByAdmin(int.Parse(DetailID.Text));

                    }

                
                        }
            }

            dgvMyallrequest.DataSource = db.Get_TRNRequest_DetailToIntegrate();
            dgvMyallrequest.DataBind();

            //string fileName =  "TRNRequest-Student" + "_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute  +"_" + DateTime.Now.Second+".xls";

            //ExportToExcel(sender, e, fileName);
            //db.SendMail_TRNRequest_File(fileName);

        

        }

        private void ExportGridToExcel(string namefile)
        {
            //int ligne = 1;
            //string folder = db.Get_KCCRequest_Parametre("TRNRequest_Folder_Link");
            ////if (File.Exists("d:\\TRNRequest-Student.xls"))
            ////{
            ////    File.Delete("d:\\TRNRequest-Student.xls");
            ////}

            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();



            //Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            //Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;

            //xlWorkBook = xlApp.Workbooks.Add(misValue);
            //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //xlWorkSheet.Cells[1, 1] = "Training Request ID";
            //xlWorkSheet.Cells[1, 2] = "Employee ID";
            //xlWorkSheet.Cells[1, 3] = "Name";
            //xlWorkSheet.Cells[1, 4] = "Courses";
           

            //foreach (GridViewRow row in dgvMyallrequest.Rows)
            //{

                
            //    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
            //    Label EmployeeID = (Label)row.FindControl("lblEmployeeID");
            //    Label EmployeeName = (Label)row.FindControl("lblEmployeeName");
            //    Label Cours = (Label)row.FindControl("lblCours");
            //    Label TrainingID = (Label)row.FindControl("lblTRAININGID");

            //    if (chkRow.Checked == true)
            //    {
            //        ligne++;
            //        xlWorkSheet.Cells[ligne, 1] = TrainingID.Text;
            //        xlWorkSheet.Cells[ligne, 2] = EmployeeID.Text;
            //        xlWorkSheet.Cells[ligne, 3] = EmployeeName.Text;
            //        xlWorkSheet.Cells[ligne, 4] = Cours.Text;
                 
            //    }






            //}




            //xlWorkBook.SaveAs(folder + @"\" + namefile, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //Marshal.ReleaseComObject(xlWorkSheet);
            //Marshal.ReleaseComObject(xlWorkBook);
            //Marshal.ReleaseComObject(xlApp);

        }
        
        public void SelectRowsCheckGrid()
        {
            foreach (GridViewRow row in dgvMyallrequest.Rows)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                Label EmployeeID = (Label)row.FindControl("lblEmployeeID");
                Label EmployeeName = (Label)row.FindControl("lblEmployeeName");
                Label Cours = (Label)row.FindControl("lblCours");
                Label TrainingID = (Label)row.FindControl("lblTRAININGID");

                if (chkRow.Checked == true)
                {
                    Addnewrow(TrainingID.Text,EmployeeID.Text,EmployeeName.Text,Cours.Text);

                }
            }

            
        }
        private void SelectRowsToExport()
        {
            try
            {
                //SelectRowsCheckGrid();

                dgIntegration.AllowPaging = false;
                dgIntegration.HeaderRow.Visible = true;
                dgIntegration.GridLines = GridLines.Both;
                dgIntegration.AutoGenerateSelectButton = false;
                dgIntegration.HeaderStyle.Font.Bold = true;


                //dgvMyallrequest.HeaderRow.Style.Add("background-color", "#000");
                //dgvMyallrequest.HeaderRow.Cells[0].Style.Add("background-color", "blue");
                //dgvMyallrequest.HeaderRow.Cells[1].Style.Add("background-color", "blue");
                //dgvMyallrequest.HeaderRow.Cells[2].Style.Add("background-color", "blue");
                //dgvMyallrequest.HeaderRow.Cells[4].Style.Add("background-color", "blue");


                dgIntegration.HeaderRow.Cells[0].Text = "Training Request ID";
                dgIntegration.HeaderRow.Cells[1].Text = "Employee ID";
                dgIntegration.HeaderRow.Cells[2].Text = "Employee Name";
                dgIntegration.HeaderRow.Cells[3].Text = "Cours";

            }
            catch (Exception s) {

            }
           
            
        }

        protected void ExportToExcel(object sender, EventArgs e,string name)
        {
            string folder = db.Get_KCCRequest_Parametre("TRNRequest_Folder_Link");

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = name;
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            SelectRowsToExport();
            dgIntegration.RenderControl(htmltextwrtter);

            System.IO.TextWriter w = new System.IO.StreamWriter(@folder + name);
            w.Write(strwritter.ToString());
            w.Flush();
            w.Close();

            Response.Redirect("TrainingIntegrate.aspx");


            //Response.Write(strwritter.ToString());

            //Response.End();
        }



       

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }



    }
}