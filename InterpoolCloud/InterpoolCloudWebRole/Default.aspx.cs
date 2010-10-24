using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InterpoolCloudWebRole.Controller;
using InterpoolCloudWebRole.Utilities;
using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.FacebookCommunication;
using InterpoolCloudWebRole.Datatypes;
using System.Diagnostics;

namespace InterpoolCloudWebRole
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void StartGame_Click(object sender, EventArgs e)
        {
            InterpoolContainer conteiner = new InterpoolContainer();
            //Poner el id de facebook que se trae en el loguin cada vez que se conecta.
            IDataManager dm = new DataManager();
            string userId = dm.GetLastUserIdFacebook(dm.GetContainer());
            IProcessController ipc = new ProcessController(dm.GetContainer());
            ipc.StartGame(userId);
            
        }

        protected void NewsFamous_Click(object sender, EventArgs e)
        {
            Admin.loadFamousData();
            labelInfo.Text = "News Famous updated";
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            OAuthFacebook oAuth = new OAuthFacebook();
            Response.Redirect(oAuth.AuthorizationLinkGet());
        }

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

        protected void PruebaTravel_Click(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            string userId = "1358576832";
            DataCity dc = ipc.Travel(userId, "Auckland");
        }

        protected void PruebaArrestar_Click1(object sender, EventArgs e)
        {
            string user = "1358576832";
            string culpable = "1212";
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            ipc.EmitOrderOfArrest(user, culpable);
        }

        protected void PruebaEOA_Click(object sender, EventArgs e)
        {
            InterpoolContainer container = new InterpoolContainer();
            IProcessController ipc = new ProcessController(container);
            string userId = "1358576832";
            ipc.EmitOrderOfArrest(userId, userId);
        }

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
