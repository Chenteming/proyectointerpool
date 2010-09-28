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
    }
}
