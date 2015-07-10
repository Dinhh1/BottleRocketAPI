using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BottleRocket.Controllers;
using BottleRocket.Models;
using BottleRocket.BusinessLogic;
using System.Threading.Tasks;


namespace BottleRocket.Tests.ApiFlowTest
{
    [TestClass]
    public class ApiFlowUnitTest
    {
        [TestMethod]
        public void TestRegistration()
        {
            /*
             * {  
                   "Email":"dinhho916@gmail.com",
                   "Password":"1D@in!h",
                   "ConfirmPassword":"1D@in!h",
                   "FirstName":"Dinh",
                   "LastName":"Ho",
                   "Address":"8303 Holly Jill Way",
                   "City":"Sacramento",
                   "State":"CA",
                   "ZipCode":"95823"
                }
             */
            AccountController controller = new AccountController();

            RegisterBindingModel model = new RegisterBindingModel()
            {
                Email = "dinhho916@gmail.com",
                Password = "password",
                ConfirmPassword = "password",
                FirstName = "Dinh",
                LastName = "Ho",
                Address = "8303 Holly Jill Way",
                City = "Sacramento",
                State = "CA",
                ZipCode = "95823"
            };

            var result = controller.Register(model);
           

            // create model
            
            //            Assert.IsNotNull(result);
            //            Assert.AreEqual(2, result.Count());
            //            Assert.AreEqual("value1", result.ElementAt(0));
            //            Assert.AreEqual("value2", result.ElementAt(1));

        }
    }
}
