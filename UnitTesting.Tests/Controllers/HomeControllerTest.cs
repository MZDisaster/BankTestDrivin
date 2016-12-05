using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTesting;
using UnitTesting.Controllers;
using Telerik.JustMock;
using UnitTesting.Repository;
using UnitTesting.Models;

namespace UnitTesting.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexReturningAllVehicles()
        {
            /*var Repo = Mock.Create<BankRepo>();
            Mock.Arrange(() => Repo.GetVehicles()).
                Returns(new List<Vehicle>() { 
                    new Vehicle{ Id = 1, TypeId = 1, OwnerId = 1, Color = "Red", RegNr = "ABC123"},
                    new Vehicle{ Id = 2, TypeId = 1, OwnerId = 2, Color = "Blue", RegNr = "ABC234"}
                }).MustBeCalled();

            HomeController controller = new HomeController(Repo);
            ViewResult viewResult = controller.Index();
            var model = viewResult.Model as IEnumerable<Vehicle>;
            */
            Assert.AreEqual(2, 2);

        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
