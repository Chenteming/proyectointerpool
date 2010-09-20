using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterpoolPrototypeWebRole.Data
{
    public class DataManager : IDataManager
    {
        public IQueryable<City> getCities(InterpoolContainer context)
        {
             
            return context.Cities;
        }

        public IQueryable<Level> getLevels(InterpoolContainer context)
        {
            return context.Levels;
        }


        public IQueryable<Famous> GetFamousByCity(City city, InterpoolContainer context)
        {
            return from f in context.Famous
                      where f.City == city
                      select f;
                    
        }
    }
}