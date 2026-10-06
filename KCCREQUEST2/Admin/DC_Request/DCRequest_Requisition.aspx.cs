using KCCDBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2.Admin.DC_Request
{
    public partial class DCRequest_Requisition : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "DCRequest").FirstOrDefault().Number == 0)
                {

                    Response.Redirect("../default_admin.aspx");
                }

                dgvMyallrequest.DataSource = db.Get_DCRequest_Requisition();
                dgvMyallrequest.DataBind();

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //dgvMyallrequest.DataSource = db.Get_TRANSRequest_Search(txtRequisitionID.Text);
            //dgvMyallrequest.DataBind();

            dgvMyallrequest.DataSource = db.Get_DCRequest_Requisition().Where(x=>x.Id.ToString()==txtRequisitionID.Text);
            dgvMyallrequest.DataBind();
        }

        protected void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;


                foreach (GridViewRow row in dgvMyallrequest.Rows)
                {
                    Button btn1 = (Button)row.FindControl("btnDetail");

                    if (btn1.Equals(btn))
                    {


                        Label ID = (Label)row.FindControl("lblID");

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                        //Response.Redirect("Admin_DetailITRequest.aspx?IDITrequest=" + ID.Text);

                        Response.Redirect("../../Detail_DCRequest.aspx?DCrequestID=" + ID.Text);

                        break;
                    }


                }
            }
            catch (Exception s)
            {


            }
        }
    }
}