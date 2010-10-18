
namespace InterpoolCloudTest
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Controller;

    /// <summary>
    /// BuiltTravelTest
    /// </summary>
    [TestClass]
    public class BuiltTravelTest
    {
        private InterpoolContainer container;
        private DataManager dm;
        
        public BuiltTravelTest()
        {
            string conn = @"metadata=res://*/Data.InterpoolModel.csdl|res://*/Data.InterpoolModel.ssdl|res://*/Data.InterpoolModel.msl; provider=System.Data.SqlClient; ;provider connection string='Data Source=MARTIN-PC\SQLEXPRESS;Initial Catalog=InterpoolDB;Integrated Security=True;MultipleActiveResultSets=True'";
            container = new InterpoolContainer(conn);
            dm = new DataManager();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }

            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
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
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod()
        {
            ProcessController controler = new ProcessController(container);

            string userIdFacebook = "1358576832";
            User user = container.Users.Where(u => u.UserIdFacebook == userIdFacebook).First();

            Game game = controler.BuiltTravel(user);

            Console.WriteLine("Test: {0} ", "BuiltTravelTest");
        }
    }
}
