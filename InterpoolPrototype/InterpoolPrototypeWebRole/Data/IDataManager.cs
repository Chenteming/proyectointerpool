using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpoolPrototypeWebRole.Data
{
    interface IDataManager
    {
        IQueryable<City> getCities(InterpoolContainer context);
        IQueryable<Level> getLevels(InterpoolContainer context);
        IQueryable<Famous> GetFamousByCity(City city, InterpoolContainer context);
    }
}
