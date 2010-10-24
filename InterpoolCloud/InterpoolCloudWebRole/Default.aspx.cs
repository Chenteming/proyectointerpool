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
        /// Satar game click
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void StartGame_Click(object sender, EventArgs e)
        {
            InterpoolContainer conteiner = new InterpoolContainer();
            ////Poner el id de facebook que se trae en el loguin cada vez que se conecta.
            IDataManager dm = new DataManager();
            string userId = dm.GetLastUserIdFacebook(dm.GetContainer());
            IProcessController ipc = new ProcessController(dm.GetContainer());
            ipc.StartGame(userId);
        }

        /// <summary>
        /// New Famous Click
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void NewsFamous_Click(object sender, EventArgs e)
        {
            Admin.LoadFamousData();
            labelInfo.Text = "News Famous updated";
        }

        /// <summary>
        /// Login Click function
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void Login_Click(object sender, EventArgs e)
        {
            OAuthFacebook oauth = new OAuthFacebook();
            Response.Redirect(oauth.AuthorizationLinkGet());
        }

        /// <summary>
        /// Delete Game Click
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void DeleteGame_Click(object sender, EventArgs e)
        {
            /*
            string id = "1358576832";
            InterpoolContainer container = new InterpoolContainer();
            User user = container.Users.Where(u => u.UserIdFacebook == id).First();
            ProcessController pc = new ProcessController();

            //pc.deleteGame(user, container);
            // pc.deleteGame(user, container);*/
        }

        /// <summary>
        /// Prueba Get City click
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void PruebaGetCities_Click(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            string userId = "1358576832";
            List<DataCity> col = ipc.GetCities(userId);

            foreach (DataCity d in col)
            {
                pruebaGetCities.Text = pruebaGetCities.Text + d.Latitud + " " + d.Longitud + " " + d.NameCity + " " + d.NameFileCity + "\n";
            }
        }
        
        /// <summary>
        /// Prueba Travel Click
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void PruebaTravel_Click(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            string userId = "1358576832";
            DataGame dc = ipc.Travel(userId, "Auckland");
        }

        /// <summary>
        /// Prueba Arrestar Click
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void PruebaArrestar_Click1(object sender, EventArgs e)
        {
            string user = "1358576832";
            string culpable = "1212";
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            ipc.EmitOrderOfArrest(user, culpable);
        }

        /// <summary>
        /// Prueba End of Arrest
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void PruebaEOA_Click(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            string userId = "1358576832";
            ipc.EmitOrderOfArrest(userId, userId);
        }

        /// <summary>
        /// Prueba arrestar Click
        /// </summary>
        /// <param name="sender">Parameter description for sender goes here</param>
        /// <param name="e">Parameter description for e goes here</param>
        protected void PruebaArrestar_Click(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            string user = "1358576832";
            string culpable = "1212";
            IProcessController ipc = new ProcessController(container);
            ipc.EmitOrderOfArrest(user, culpable);
        }
    }
}
