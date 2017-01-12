using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoxalinoWeb.backend;


namespace boxlinoTest.backend
{
    /// <summary>
    /// Summary description for DataFullExportTest
    /// </summary>
    [TestClass]
    public class DataFullExportTest
    {

        private string account = "boxalino_automated_tests";
        private string password = "boxalino_automated_tests";

        [TestMethod]
       
        public void testBackendDataFullExport()
        {
            DataFullExport _dataFullExport = new DataFullExport();
            try
            {
                _dataFullExport.account = this.account;
                _dataFullExport.password = this.password;
                _dataFullExport.print = false;
                _dataFullExport.dataFullExport();
            }
            catch(Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }           

        }
        

    }
}
