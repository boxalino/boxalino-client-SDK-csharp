using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using BoxalinoWeb.frontend;
using System.Linq;

namespace boxlinoTest.frontend
{
    /// <summary>
    /// Summary description for SearchReturnFieldsTest
    /// </summary>
    [TestClass]
    public class SearchReturnFieldsTest
    {

        private string account = "boxalino_automated_tests";
        private string password = "boxalino_automated_tests";

        [TestInitialize]
        public void TestSetup()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new System.Web.HttpRequest("", "http://localhost:6989/", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            HttpContext httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer(Guid.NewGuid().ToString("N"),
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }

        [TestMethod]
        public void testFrontendSearchReturnFields()
        {
            SearchReturnFields _searchReturnFields = new SearchReturnFields();
            try
            {
                _searchReturnFields.account = this.account;
                _searchReturnFields.password = this.password;
                _searchReturnFields.print = false;

                _searchReturnFields.searchReturnFields();

                CollectionAssert.AreEqual(_searchReturnFields.bxResponse.getHitFieldValues((new List<string>() { { "products_color" } } ).ToArray())["41"]["products_color"], new List<string>() { { "Black" }, { "Gray" }, { "Yellow" } });
                CollectionAssert.AreEqual(_searchReturnFields.bxResponse.getHitFieldValues((new List<string>() { { "products_color" } }).ToArray())["41"]["products_color"], new List<string>() { { "Gray" }, { "Orange" }, { "Yellow" } });
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
