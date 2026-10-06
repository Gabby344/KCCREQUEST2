using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2.form
{
    public partial class trainingRequest : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtMotivationEnvironment.Enabled = CbEnvironment.Checked;
            txtMotivationProduction.Enabled = CbProduction.Checked;
            txtMotivationSafety.Enabled = CbSafety.Checked;
        }

        protected void CbProduction_CheckedChanged(object sender, EventArgs e)
        {
            txtMotivationProduction.Enabled = CbProduction.Checked;
            txtMotivationProduction.Text = string.Empty;
        }

        protected void CbEnvironment_CheckedChanged(object sender, EventArgs e)
        {
            txtMotivationEnvironment.Enabled = CbEnvironment.Checked;
            txtMotivationEnvironment.Text = string.Empty;
        }

        protected void CbSafety_CheckedChanged(object sender, EventArgs e)
        {
            txtMotivationSafety.Enabled = CbSafety.Checked;
            txtMotivationSafety.Text = string.Empty;
        }
    }
}