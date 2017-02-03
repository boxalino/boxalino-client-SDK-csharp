using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Web.SessionState;
using System.Web;
using System.Reflection;
using examples.frontend;
using boxalino_client_SDK_CSharp.Services;
using System.Linq;

namespace tests.frontend
{
    /// <summary>
    /// Summary description for SearchDebugRequestTest
    /// </summary>
    [TestClass]
    public class SearchDebugRequestTest
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
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }

        [TestMethod]
        public void testFrontendSearchDebugRequest()
        {
            SearchDebugRequest _searchDebugRequest = new SearchDebugRequest();
            try
            {
                _searchDebugRequest.account = this.account;
                _searchDebugRequest.password = this.password;
                _searchDebugRequest.print = false;

                _searchDebugRequest.searchDebugRequest();
                Assert.IsInstanceOfType(_searchDebugRequest.bxClient.getThriftChoiceRequest(), typeof(ChoiceRequest));
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
