using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;

namespace KCCREQUEST2
{
    public partial class My_allRequest : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) {

                Response.Redirect("Default_View_MyRequests.aspx?dashboard=NO");
                if (Request["type"] == null)
                {
                    dgvMyallrequest.DataSource = db.Get_KCCRequest_CreatedBy(null, CachedLogin).GroupBy(x => new { x.Module, x.Id }).Select(x => x.First());
                    dgvMyallrequest.DataBind();
                }
                else if (Request["type"] != null){

                    title.InnerText = "All requests - " + Request["type"].ToString();
                    dgvMyallrequest.DataSource = db.Get_KCCRequest_CreatedBy(Request["type"].ToString(), CachedLogin).GroupBy(x => new { x.Module, x.Id }).Select(x => x.First());
                    dgvMyallrequest.DataBind();
                
                
                }
            
            }
        }

        protected void btndetail_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;


                foreach (GridViewRow row in dgvMyallrequest.Rows)
                {
                    Button btn1 = (Button)row.FindControl("btndetail");

                    if (btn1.Equals(btn))
                    {

                         Label reqId = (Label)row.FindControl("lblID");
                        Label module = (Label)row.FindControl("lblModule");

                        if(module.Text=="ITRequest")
                        Response.Redirect("Detail_itRequest.aspx?IDITrequest=" +reqId.Text);
                        if (module.Text == "RECTRequest")
                        {
                            Response.Redirect("Detail_RecruitmentRequest.aspx?RectRequestID=" + reqId.Text);
                        }
                        break;
                    }



                }
            }
            catch (Exception s)
            {

                lblErreur.Text = s.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalErreur();", true);
            }

        }

        protected void btnRechReq_Click(object sender, EventArgs e)
        {
            if (rechIDReq.Value != string.Empty)
            {
                int search = (int.TryParse(rechIDReq.Value, out search)) ? int.Parse(rechIDReq.Value) : 0;
                dgvMyallrequest.DataSource = db.Get_KCCRequest_CreatedBy(null, CachedLogin).Where(x=>x.Id == search).GroupBy(x => new { x.Module, x.Id }).Select(x => x.First());
                dgvMyallrequest.DataBind();
            }
            else
            {
                dgvMyallrequest.DataSource = db.Get_KCCRequest_CreatedBy(null, CachedLogin).GroupBy(x => new { x.Module, x.Id }).Select(x => x.First());
                dgvMyallrequest.DataBind();
            }
        }
    }
}