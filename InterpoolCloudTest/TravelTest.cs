//-----------------------------------------------------------------------
// <copyright file="TravelTest.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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
    /// Class statement TravelTest
    /// </summary>
    [TestClass]
    public class TravelTest
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private InterpoolContainer container;

        /// <summary>
        /// Store for the property
        /// </summary>
        private DataManager dm;

        /// <summary>
        /// Description for Method.</summary>
        [TestInitialize()]
        public void Init()
        {
            this.container = new InterpoolContainer(@"metadata=res://*/Data.InterpoolModel.csdl|res://*/Data.InterpoolModel.ssdl|res://*/Data.InterpoolModel.msl; provider=System.Data.SqlClient; ;provider connection string='Data Source=Diego-PC\SQLSERVER;Initial Catalog=InterpoolDB;Integrated Security=True;MultipleActiveResultSets=True'");
            this.dm = new DataManager();
        }

        /// <summary>
        /// Description for Method.</summary>
        [TestMethod]
        public void TravelGood()
        {
            ProcessController controller = new ProcessController(this.container);
            string userIdFacebook = this.dm.GetLastUserIdFacebook(this.container);
            NodePath currentNode = controller.GetCurrentNode(userIdFacebook);
            NodePath nextNode = controller.GetNextNode(userIdFacebook);

            Assert.IsTrue(currentNode.NodePathCurrent == true);
            Assert.IsTrue(nextNode.NodePathCurrent == false);

            controller.Travel(userIdFacebook, nextNode.City.CityName);

            Assert.IsTrue(currentNode.NodePathCurrent == false);
            Assert.IsTrue(nextNode.NodePathCurrent == true);
        }

        /// <summary>
        /// Description for Method.</summary>
        [TestMethod]
        public void TravelWrong()
        {
            ProcessController controller = new ProcessController(this.container);
            string userIdFacebook = this.dm.GetLastUserIdFacebook(this.container);
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
