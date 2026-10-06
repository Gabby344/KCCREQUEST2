using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KCCDBAccess;

namespace KCCREQUEST2
{
    public partial class _default : System.Web.UI.Page
    {
        KCCDBWrapper db = new KCCDBWrapper();
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void CreateITRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_ITRequest.aspx");
        }

        protected void btnNewDskRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_DSKRequest.aspx");
        }

        protected void btnCreateTrainingRst_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_TrainingRequest.aspx");
        }

        protected void btnCreateEWORequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_EWORequest.aspx");
        }

        protected void btnCreateNewHrApproval_Click(object sender, EventArgs e)
        {
            Response.Redirect("New_HrApproval.aspx");
        }
    }
}