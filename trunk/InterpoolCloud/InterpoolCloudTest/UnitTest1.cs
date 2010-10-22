
namespace InterpoolCloudTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InterpoolCloudWebRole.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Class statement UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private InterpoolContainer container;

        /// <summary>
        /// Store for the property
        /// </summary>
        private DataManager dm;

        [TestInitialize()]
        public void Init()
        {
            this.container = new InterpoolContainer(@"metadata=res://*/Data.InterpoolModel.csdl|res://*/Data.InterpoolModel.ssdl|res://*/Data.InterpoolModel.msl; provider=System.Data.SqlClient; ;provider connection string='Data Source=Diego-PC\SQLSERVER;Initial Catalog=InterpoolDB;Integrated Security=True;MultipleActiveResultSets=True'");
            this.dm = new DataManager();
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<City> cities = this.dm.GetCities(this.container).ToList();
            Assert.AreEqual(cities.Count, 32);
        }
    }
}
