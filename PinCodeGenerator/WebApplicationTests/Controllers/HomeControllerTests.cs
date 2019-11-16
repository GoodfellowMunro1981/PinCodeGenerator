using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WebApplication.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        private HomeController controller = new HomeController();

        [TestMethod()]
        public void GetPinTest()
        {
            // ARRANGE

            // ACT
            var pin = controller.GetRandomPin();

            // ASSERT
            Assert.AreEqual(4, pin.Length);
        }

        [TestMethod()]
        public void CheckAllPinNumbersGenerated()
        {
            // ARRANGE

            // ACT
            var numbers = controller.Numbers;

            // ASSERT
            Assert.IsTrue(numbers.Contains(0));
            Assert.IsTrue(numbers.Contains(9999));
            Assert.AreEqual(10000, numbers.Count());
        }

        [TestMethod()]
        public void CheckInvalidPinNumbersNotGenerated()
        {
            // ARRANGE

            // ACT
            var numbers = controller.Numbers;

            // ASSERT
            Assert.IsFalse(numbers.Contains(-1));
            Assert.IsFalse(numbers.Contains(10000));
            Assert.IsFalse(numbers.Contains(10001));
        }

        [TestMethod()]
        public void CheckEmptyStringReturnedAfterAllPinsDisplayed()
        {
            // ARRANGE
            int i = 0;
            while (i < 10000)
            {
                controller.GetRandomPin();
                i++;
            }

            // ACT
            var pin = controller.GetRandomPin();

            // ASSERT
            Assert.AreEqual(default, pin);
        }
    }
}