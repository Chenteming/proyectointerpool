using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpoolPrototypeWebRole.Data
{
    interface IDataManager
    {
        IQueryable<City> getCities();
        IQueryable<Famous> GetFamousByCity(City city);
    }
}
