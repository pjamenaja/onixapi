using System;
using NUnit.Framework;

using Onix.Api.Factories;
using Onix.Api.Commons.Business;

namespace Onix.Test.Api.Factories
{
    public class FactoryBusinessOperationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("Init", "All")]
        [TestCase("Logon", "All")]
        public void RoleForApiNameTest(string apiName, string expectedRoleName)
        {
            BusinessOperationOption opt = FactoryBusinessOperation.GetBusinessOperationAllowedRole(apiName);
            bool isAllowed = opt.IsRoleAllow(expectedRoleName);

            string msg = string.Format("Role name [{0}] should be allowed for [{1}] !!!", expectedRoleName, apiName);
            Assert.AreEqual(true, isAllowed, msg);
        }

        [TestCase("UnknowAPI")]
        public void UnknownApiNameTest(string apiName)
        {
            try
            {
                IBusinessOperation opt = FactoryBusinessOperation.CreateBusinessOperationObject(apiName);
                Assert.True(false, "Exception shoud be throw for unknow API!!!");
            }
            catch (Exception e)
            {
                Assert.True(true, e.Message);
            }
        }                                  
    }    
}