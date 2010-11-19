//-----------------------------------------------------------------------
// <copyright file="LoginSuccess.aspx.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace InterpoolCloudWebRole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Partial class declaration LoginSuccess
    /// </summary>
    public partial class LoginSuccess : System.Web.UI.Page
    {
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //// To close the browser
            
            const string javaScript = "<script language=javascript>window.top.close();</script>";
            if (!ClientScript.IsStartupScriptRegistered("CloseMyWindow"))
            {
                ClientScript.RegisterStartupScript(GetType(), "CloseMyWindow", javaScript);
            }
        }
    }
}