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

namespace InterpoolCloudWebRole
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            InterpoolContainer conteiner = new InterpoolContainer();
            //Poner el id de facebook que se trae en el loguin cada vez que se conecta.
            string userId = "1358576832";
            IProcessController ipc = new ProcessController();
            ipc.StartGame(userId);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Admin.loadFamousData();
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            oAuthFacebook oAuth = new oAuthFacebook();
            Response.Redirect(oAuth.AuthorizationLinkGet());
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            string id = "1358576832";
            InterpoolContainer container = new InterpoolContainer();
            User user = container.Users.Where(u => u.UserIdFacebook == id).First();
            ProcessController pc = new ProcessController();

            //pc.deleteGame(user, container);
           // pc.deleteGame(user, container);
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            IProcessController ipc = new ProcessController();
            string userId = "1358576832";
            List<DataCity> col = ipc.GetCities(userId);

            foreach (DataCity d in col)
            {
                pruebaGetCities.Text = pruebaGetCities.Text+d.latitud + " " + d.longitud + " " + d.name_city + " " + d.name_file_city + "\n";
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            IProcessController ipc = new ProcessController();
            string userId = "1358576832";
            DataCity dc = ipc.Travel(userId, "a");

        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            IProcessController ipc = new ProcessController();
            string userId = "1358576832";
            ipc.EmitOrderOfArrest(userId, userId);
        }

        protected void Button7_Click(object sender, EventArgs e)
        {

        }

    }
}
