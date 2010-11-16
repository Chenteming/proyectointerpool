//-----------------------------------------------------------------------
// <copyright file="Default.aspx.cs" company="Interpool">
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
    /// _Default Class
    /// </summary>
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// Page Load function
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        /// <summary>
        /// News Famous Click
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void NewsFamous_Click(object sender, EventArgs e)
        {
            Admin.LoadFamousData();
            this.labelInfo.Text = "News Famous updated";
        }
    }
}
