
namespace InterpoolCloudTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InterpoolCloudWebRole.Controller;
    using InterpoolCloudWebRole.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
        
    /// <summary>
    /// Built Travel Test.
    /// </summary>
    [TestClass]
    public class BuiltTravelTest
    {
        private InterpoolContainer container;
        private DataManager dm;
        private TestContext testContextInstance;

        /// <summary>
        /// Initializes a new instance of the BuiltTravelTest class.</summary>
        public BuiltTravelTest()
        {
            ////string conn = @"metadata=res://*/Data.InterpoolModel.csdl|res://*/Data.InterpoolModel.ssdl|res://*/Data.InterpoolModel.msl; provider=System.Data.SqlClient; ;provider connection string='Data Source=MARTIN-PC\SQLEXPRESS;Initial Catalog=InterpoolDB;Integrated Security=True;MultipleActiveResultSets=True'";
            this.container = new InterpoolContainer();
            this.dm = new DataManager();
        }

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        //// public void MyTestCleanup() { }

        #endregion

        [TestMethod]
        public void TestMethod()
        {
            ProcessController controler = new ProcessController(this.container);

            string userIdFacebook = "1358576832";
            User user = this.container.Users.Where(u => u.UserIdFacebook == userIdFacebook).First();

            Game game = controler.BuiltTravel(user);

            List<int> numCities = new List<int>();

            Assert.AreEqual(game.NodePath.Count, 4, "Amount of NodePath");
           
            // check if not repeat city
            foreach (NodePath node in game.NodePath)
            {
                Assert.IsFalse(numCities.Contains(node.City.CityNumber));
                foreach (City city in node.PossibleCities)
                {
                    Assert.IsFalse(numCities.Contains(city.CityNumber));
                    numCities.Add(city.CityNumber);
                }

                numCities.Add(node.City.CityNumber);

                Assert.AreEqual(3, node.Famous.Count);
            }
        }
    }
}
