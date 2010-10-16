using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterpoolCloudWebRole.Tests
{
    using NUnit.Framework;
    using InterpoolCloudWebRole.Data;
    using System.Data.EntityClient;
using System.Data.SqlClient;

    [TestFixture]
    public class Test
    {
        private InterpoolContainer container;
        private DataManager dm;

        [SetUp]
        public void Init()
        {
           string conn = @"metadata=res://*/Data.InterpoolModel.csdl|res://*/Data.InterpoolModel.ssdl|res://*/Data.InterpoolModel.msl; provider=System.Data.SqlClient; ;provider connection string='Data Source=MARTIN-PC\SQLEXPRESS;Initial Catalog=InterpoolDB;Integrated Security=True;MultipleActiveResultSets=True'";
		   container = new InterpoolContainer(conn);
           dm = new DataManager();
            

        }

        [Test]
        public void testEjemplo() {
            List<City> list = dm.getCities(container).ToList();
            Assert.AreEqual(list.Count, 33);
            
        }
    }

   
}