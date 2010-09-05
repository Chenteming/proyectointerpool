using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using InterpoolPrototypeWebRole.FacebookComunication;

namespace InterpoolPrototypeWebRole
{
    public partial class Face : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oAuthFacebook oAuth = new oAuthFacebook();

            if (Request["code"] == null)
            {
                //Redirect the user back to Facebook for authorization.
                Response.Redirect(oAuth.AuthorizationLinkGet());
            }
            else
            {
                //Get the access token and secret.
                oAuth.AccessTokenGet(Request["code"]);

                if (oAuth.Token.Length > 0)
                {
                    IFacebookController facebookController = new FacebookController();
                    string userId = facebookController.GetUserId(oAuth);
                    if (!userId.Equals(""))
                    {
                        // List<string> friendsIds = facebookController.GetFriendsId(userId);
                        List<string> friendsNames = facebookController.GetFriendsNames(oAuth, userId);
                        HelloWorldEntities context = new HelloWorldEntities();
                        List<PrototypeSuspect> listSuspects = new List<PrototypeSuspect>(context.PrototypeSuspects);
                        // Deletes all the existing suspects
                        foreach (PrototypeSuspect pSuspectDelete in listSuspects)
                        {
                            context.DeleteObject(pSuspectDelete);
                        }
                        context.SaveChanges();

                        PrototypeSuspect pSuspect;
                        // Creates the suspects for the current user
                        foreach (string name in friendsNames)
                        {
                            pSuspect = new PrototypeSuspect();
                            pSuspect.Name = name;
                            context.AddToPrototypeSuspects(pSuspect);
                        }
                        context.SaveChanges();
                    }
                    
                }
            }
        }
    }
}