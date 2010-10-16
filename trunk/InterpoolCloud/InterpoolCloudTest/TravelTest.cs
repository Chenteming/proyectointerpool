using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterpoolCloudTest
{
    [TestClass]
    public class TravelTest
    {
        private InterpoolContainer container;
        private DataManager dm;


        [TestInitialize()]
        public void init()
        {
            container = new InterpoolContainer(@"metadata=res://*/Data.InterpoolModel.csdl|res://*/Data.InterpoolModel.ssdl|res://*/Data.InterpoolModel.msl; provider=System.Data.SqlClient; ;provider connection string='Data Source=Diego-PC\SQLSERVER;Initial Catalog=InterpoolDB;Integrated Security=True;MultipleActiveResultSets=True'");
            dm = new DataManager();
        }

        [TestMethod]
        public void TravelGood()
        {
            ProcessController controller = new ProcessController(container);
            string userIdFacebook = dm.GetLastUserIdFacebook(container);
            NodePath currentNode = controller.GetCurrentNode(userIdFacebook);
            NodePath nextNode = controller.GetNextNode(userIdFacebook);

            Assert.IsTrue(currentNode.NodePathCurrent == true);
            Assert.IsTrue(nextNode.NodePathCurrent == false);

            controller.Travel(userIdFacebook, nextNode.City.CityName);

            Assert.IsTrue(currentNode.NodePathCurrent == false);
            Assert.IsTrue(nextNode.NodePathCurrent == true);

        }

        [TestMethod]
        public void TravelWrong()
        {
            ProcessController controller = new ProcessController(container);
            string userIdFacebook = dm.GetLastUserIdFacebook(container);
            NodePath currentNode = controller.GetCurrentNode(userIdFacebook);
            NodePath nextNode = controller.GetNextNode(userIdFacebook);

            Assert.IsTrue(currentNode.NodePathCurrent == true);
            Assert.IsTrue(nextNode.NodePathCurrent == false);

            controller.Travel(userIdFacebook, "Jupiter");

            Assert.IsTrue(currentNode.NodePathCurrent == true);
            Assert.IsTrue(nextNode.NodePathCurrent == false);

        }
    }
}
