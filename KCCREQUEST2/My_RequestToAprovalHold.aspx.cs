using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;
namespace KCCREQUEST2
{
    public partial class My_RequestToAprovalHold : BasePage
    {
        KCCDBWrapper db = new KCCDBWrapper();
        Utility utility = new Utility();
        private string loggedUser = HttpContext.Current.User.Identity.Name;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               
                  
                    dgvRequestToAproval.DataSource = db.Get_KCCRequest_ToApproveHold("ITRequest", utility.Login(loggedUser)).Distinct().ToList();
                    dgvRequestToAproval.DataBind();

                }
            
        }
        protected void btndetail_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;


                foreach (GridViewRow row in dgvRequestToAproval.Rows)
                {
                    Button btn1 = (Button)row.FindControl("btndetail");

                    if (btn1.Equals(btn))
                    {

                        Label reqId = (Label)row.FindControl("lblID");
                        Label module = (Label)row.FindControl("lblModule");

                        if (module.Text == "ITRequest")
                            Response.Redirect("Detail_itRequest.aspx?IDITrequest=" + reqId.Text);
                        if (module.Text == "RECTRequest")
                        {
                            Response.Redirect("Detail_RecruitmentRequest.aspx?RectRequestID=" + reqId.Text);
                        }
                        if (module.Text == "DSKRequest")
                        {
                            Response.Redirect("Detail_radiorequest.aspx?DSKRrequestID=" + reqId.Text);
                        }
                        if (module.Text == "TRAININGRequest")
                        {
                            Response.Redirect("Detail_trainingRequest.aspx?TrID=" + reqId.Text);
                        }

                        if (module.Text == "EWRequest")
                        {
                            Response.Redirect("Detail_ewoRequest.aspx?EWRequestID=" + reqId.Text);
                        }
                        if (module.Text == "DCRequest")
                        {
                            Response.Redirect("Detail_DCRequest.aspx?DCrequestID=" + reqId.Text);
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
            //List<SP_Get_KCCRequest_ToApprove_Result> listToApprove = db.Get_KCCRequest_ToApprove(null, utility.Login(loggedUser));
            //if (rechIDReq.Value != string.Empty)
            //{
            //    dgvRequestToAproval.DataSource = listToApprove.Where(x => x.Id.ToString().Equals(rechIDReq.Value));
            //    dgvRequestToAproval.DataBind();
            //}
            //else {
            //    dgvRequestToAproval.DataSource = listToApprove;
            //    dgvRequestToAproval.DataBind();
            //}
        }
    }
}