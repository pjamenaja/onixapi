using System;
using NUnit.Framework;

using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Commons.Business;
using Onix.Test.Commons;

namespace Onix.Test.Api.Erp.Business.Admins.Authentication
{
    public class MockedLogonTest : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        private IBusinessOperation getDbOperation(string name)
        {
            IBusinessOperation opr = FactoryBusinessOperation.CreateBusinessOperationObject(name);
            return opr;
        }
        
        [TestCase]
        public void TryToLogonFailWithMockProviderTest()
        {
            DbContext ctx = getInmemoryDbContextForQueryTesting("TryToLogonFailWithMockProviderTest");
            CTable logon = new CTable();

            logon.SetFieldValue("PASSWORD", "1234");
            logon.SetFieldValue("EXPECTED_PASSWORD", "12345678");

            IBusinessOperation opr = getDbOperation("MockedLogon");

            try
            {
                CTable dat = opr.Apply(logon, ctx);
                Assert.True(false, "Exception should be thrown !!!");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Password not match!!!", e.Message, "Error message not match !!!");
            }
        } 

        [TestCase]
        public void TryToLogonSuccessWithMockProviderTest()
        {
            DbContext ctx = getInmemoryDbContextForQueryTesting("TryToLogonSuccessWithMockProviderTest");
            CTable logon = new CTable();

            logon.SetFieldValue("PASSWORD", "1234");
            logon.SetFieldValue("EXPECTED_PASSWORD", "1234");

            IBusinessOperation opr = getDbOperation("MockedLogon");
            CTable dat = opr.Apply(logon, ctx);

            string role = dat.GetFieldValue("USER_ROLE");
            Assert.AreEqual("DO NOT CHANGE THIS!!!! - THIS ROLE WILL NOT BE ALLOWED FOR ALL", role, "Role name incorrect !!!");
        }                    
    }
}