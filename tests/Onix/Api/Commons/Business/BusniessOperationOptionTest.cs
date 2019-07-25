using System.Collections;
using NUnit.Framework;
using Onix.Api.Commons.Business;

namespace Onix.Test.Api.Commons.Business
{
    public class BusinessOperationOptionTest
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [TestCase("Admins,Users", "Users")]
        [TestCase("Admins,Users", "Admins")]
        [TestCase("Admins", "Admins")]
        [TestCase("All", "Abc,Defg")]
        public void CheckIfRoleIsAllowed(string allowList, string roleCheck)
        {
            BusinessOperationOption option = new BusinessOperationOption(allowList);
            bool isAllowed = option.IsRoleAllow(roleCheck);

            Assert.AreEqual(true, isAllowed, "Role {0} should be allowed!!!", roleCheck);
        }

        [TestCase("Admins,Users", "Abcd")]
        [TestCase("Admins,Users", "XyZ")]
        [TestCase("Admins,Users", "User")]
        [TestCase("Admins", "Admin")]        
        [TestCase("Admins,Users", "")]
        public void CheckIfRoleIsNotAllowed(string allowList, string roleCheck)
        {
            BusinessOperationOption option = new BusinessOperationOption(allowList);
            bool isAllowed = option.IsRoleAllow(roleCheck);

            Assert.AreEqual(false, isAllowed, "Role {0} should not be allowed!!!", roleCheck);
        }        
    }
}