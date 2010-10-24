//-----------------------------------------------------------------------
// <copyright file="Site.Master.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using InterpoolCloudWebRole.Controller;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Datatypes;
    using InterpoolCloudWebRole.FacebookCommunication;
    using InterpoolCloudWebRole.Utilities;

    /// <summary>
    /// Class Site Master
    /// </summary>
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Page Load function
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
