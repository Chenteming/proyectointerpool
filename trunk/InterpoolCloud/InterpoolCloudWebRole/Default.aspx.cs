
namespace InterpoolCloudWebRole
{
    using System;
    using System.Collections.Generic;
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
    /// Partial class declaration _Default
    /// </summary>
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            InterpoolContainer conteiner = new InterpoolContainer();
            ////Poner el id de facebook que se trae en el loguin cada vez que se conecta.
            IDataManager dm = new DataManager();
            string userId = dm.GetLastUserIdFacebook(dm.GetContainer());
            IProcessController ipc = new ProcessController(dm.GetContainer());
            ipc.StartGame(userId);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            Admin.LoadFamousData();
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            OAuthFacebook auth = new OAuthFacebook();
            Response.Redirect(auth.AuthorizationLinkGet());
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Button3_Click(object sender, EventArgs e)
        {
            string id = "1358576832";
            InterpoolContainer container = new InterpoolContainer();
            User user = container.Users.Where(u => u.UserIdFacebook == id).First();
            ProcessController pc = new ProcessController(container);

            ////pc.deleteGame(user, container);
            //// pc.deleteGame(user, container);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            string userId = "1358576832";
            List<DataCity> col = ipc.GetCities(userId);

            foreach (DataCity d in col)
            {
                this.pruebaGetCities.Text = this.pruebaGetCities.Text + d.Latitud + " " + d.Longitud + " " + d.NameCity + " " + d.NameFileCity + "\n";
            }
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Button5_Click(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            string userId = "1358576832";
            DataCity dc = ipc.Travel(userId, "Auckland");
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Button6_Click(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            string userId = "1358576832";
            ipc.EmitOrderOfArrest(userId, userId);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Button6_Click1(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            string user = "1358576832";
            string culpable = "1212";
            IProcessController ipc = new ProcessController(container);
            ipc.EmitOrderOfArrest(user, culpable);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="sender"> Parameter description for sender goes here</param>
        /// <param name="e"> Parameter description for e goes here</param>
        protected void Button7_Click(object sender, EventArgs e)
        {
            InterpoolContainer c = new InterpoolContainer();
            ProcessController ipc = new ProcessController(c);

            Suspect trucho = new Suspect();
            trucho.SuspectMusic = "mi musica";
            trucho.SuspectFirstName = "mi nombre";
            List<string> list = new List<string>();
            list.Add("SuspectFirstName");
            list.Add("SuspectFacebookId");
            list.Add("SuspectLastName");
            ipc.CreateHardCodeSuspects(trucho, list);
           /* string user = "1358576832";
            InterpoolContainer c = new InterpoolContainer();
            Int32 id = 1;
            Game g = c.Games.Where(ga => ga.GameId==id).First(); */
           /* ProcessController ipc = new ProcessController();
            ipc.Arrest();*/
        }
    }
}
