using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InterpoolPrototypeWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceWP7" in code, svc and config file together.
    public class ServiceWP7 : IServiceWP7
    {
        public List<string> GetCities()
        {
            List<string> res = new List<string>();
            HelloWorldEntities context = new HelloWorldEntities();
            List<City> cities = new List<City>(context.Cities);
            foreach (City c in cities)
            {
                res.Add(String.Concat(c.Name, " - ", c.CountryName));
            }
            return res;
        }

        public List<string> GetProbablySuspects()
        {
            // TODO: Get from the database the lists of names of the friends
            // of the current user
            List<string> friendsNames = new List<string>();
            HelloWorldEntities context = new HelloWorldEntities();
            List<PrototypeSuspect> pSuspects = new List<PrototypeSuspect>(context.PrototypeSuspects);
            foreach (PrototypeSuspect ps in pSuspects)
            {
                friendsNames.Add(ps.Name);
            }
            return friendsNames;
        }
    }
}
