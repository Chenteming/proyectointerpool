
namespace InterpoolCloudWebRole.Account
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Partial class declaration Login
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }
    }
}
