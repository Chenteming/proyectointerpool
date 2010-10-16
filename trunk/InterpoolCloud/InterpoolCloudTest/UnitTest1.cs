using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterpoolCloudWebRole.Data;
namespace InterpoolCloudTest
{
    [TestClass]
    public class UnitTest1
    {
        private InterpoolContainer container;
        private DataManager dm;


        [TestInitialize()]
        public void init()
        {
            container = new InterpoolContainer(@"metadata=res://*/Data.InterpoolModel.csdl|res://*/Data.InterpoolModel.ssdl|res://*/Data.InterpoolModel.msl; provider=System.Data.SqlClient; ;provider connection string='Data Source=MARTIN-PC\SQLEXPRESS;Initial Catalog=InterpoolDB;Integrated Security=True;MultipleActiveResultSets=True'");
            dm = new DataManager();
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<City> cities = dm.getCities(container).ToList();
            Assert.AreEqual(cities.Count, 32);
        }
    }
}
