using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;

namespace KCCREQUEST2.Admin.IT_Request
{
    public partial class RequistionRequests : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (db.Get_KCCRequestIsAdmin(utility.Login(loggedUser)).Where(x => x.ModuleID == "ITRequest").FirstOrDefault().Number == 0)
                    {

                        Response.Redirect("../default_admin.aspx");
                    }
                    this.BindGrid();

                }

            }

            catch (Exception s)
            {

            }
        }

        private void BindGrid()
        {
            dgvBeneficiary.DataSource = db.Get_ITReques();
            dgvBeneficiary.DataBind();
            //Required for jQuery DataTables to work.
            dgvBeneficiary.UseAccessibleHeader = true;
            dgvBeneficiary.HeaderRow.TableSection = TableRowSection.TableHeader;

        }

        protected void dgvBeneficiary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                dgvBeneficiary.PageIndex = e.NewPageIndex;
                dgvBeneficiary.DataSource = db.Get_ITReques();
                dgvBeneficiary.DataBind();
            }
            else
            {
                dgvBeneficiary.PageIndex = e.NewPageIndex;
                dgvBeneficiary.DataSource = db.Get_ITRequest_Search(txtSearch.Text);
                dgvBeneficiary.DataBind();
            }
        }

        protected void Detail_Click(object sender, EventArgs e)
        {

            try
            {
                Button btn = (Button)sender;


                foreach (GridViewRow row in dgvBeneficiary.Rows)
                {
                    Button btn1 = (Button)row.FindControl("btnDetail");

                    if (btn1.Equals(btn))
                    {


                        Label ID = (Label)row.FindControl("lblID");


                        Response.Redirect("../../Detail_itRequest.aspx?IDITrequest=" + ID.Text);

                        break;
                    }


                }
            }
            catch (Exception s)
            {


            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvBeneficiary.DataSource = db.Get_ITRequest_Search(txtSearch.Text);
            dgvBeneficiary.DataBind();
        }




    }
}