using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using InterpoolPrototypeWebRole.FacebookComunication;
using InterpoolPrototypeWebRole.Data;

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
                    IDataManager idm = new DataManager();
                    string userId = facebookController.GetUserId(oAuth);
                    if (!userId.Equals(""))
                    {
                        // List<string> friendsIds = facebookController.GetFriendsId(userId);
                        List<string> friendsNames = facebookController.GetFriendsNames(oAuth, userId);
                        InterpoolContainer context = new InterpoolContainer();
                        List<Suspect> listSuspects = new List<Suspect>(context.Suspects);
                        // Deletes all the existing suspects
                        foreach (Suspect pSuspectDelete in listSuspects)
                        {
                            context.DeleteObject(pSuspectDelete);
                        }
                        context.SaveChanges();

                        User us = new User();

                        context.AddToUsers(us);

                        us.Level = context.Levels.Where(l => (l.LevelName == "Interpool Nivel 1")).First<Level>();
                        
                       
                        Game test = new Game();

                        us.Game = test;
                        test.PossibleSuspect = new System.Data.Objects.DataClasses.EntityCollection<Suspect>();
                        context.AddToGames(test);
                        Suspect pSuspect;
                        // Creates the suspects for the current user
                        foreach (string name in friendsNames)
                        {
                            pSuspect = new Suspect();
                            pSuspect.SuspectName = name;
                            pSuspect.SuspectPreferenceMovies = "";
                            pSuspect.SuspectPreferenceMusic = "";
                            context.AddToSuspects(pSuspect);

                            if (test.Suspect == null)
                                test.Suspect = pSuspect;

                            test.PossibleSuspect.Add(pSuspect);
                            context.AddToGames(test);
                        }
                        context.SaveChanges();
                    }
                    
                }
            }
        }
    }
}