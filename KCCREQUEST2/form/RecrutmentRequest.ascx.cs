using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KCCREQUEST2.form
{
    public partial class RecrutmentRequest : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIsReplacement.Enabled = CbIsReplacement.Checked;
        }

        protected void CbIsReplacement_CheckedChanged(object sender, EventArgs e)
        {
            txtIsReplacement.Enabled = CbIsReplacement.Checked;
        }
    }
}