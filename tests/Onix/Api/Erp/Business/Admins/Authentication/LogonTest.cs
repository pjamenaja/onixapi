using System;
using System.Threading.Tasks;
using NUnit.Framework;

using Moq;
using Microsoft.EntityFrameworkCore;
using Firebase.Auth;

using Onix.Api.Commons;
using Onix.Test.Commons;
using Onix.Api.Erp.Business.Admins.Authentication;
using Onix.Api.Erp.Business.Admins.AuthenProviders;

namespace Onix.Test.Api.Erp.Business.Admins.Authentication
{
    public class LogonTest : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        private AuthenProviderFirebase getFirebaseProvider(string tokenReturned)
        {
            var authLinkMocked = new Mock<FirebaseAuthLink>(null, null);
            FirebaseAuthLink authLink = authLinkMocked.Object; 
            authLink.FirebaseToken = tokenReturned;

            User u = new User();
            u.Email = "mocked@gmail.com";
            u.LocalId = "ThisIsLocalID";

            authLink.User = u;
                            
            Task<FirebaseAuthLink> task = Task<FirebaseAuthLink>.Factory.StartNew(() => authLink);

            var mock = new Mock<IFirebaseAuthProvider>();
            mock.Setup(p => p.SignInWithEmailAndPasswordAsync("username", "password")).Returns(task);
            

            AuthenProviderFirebase fb = new AuthenProviderFirebase();
            fb.SetProvider(mock.Object);

            return fb;
        }
        
        [TestCase("")]
        [TestCase(null)]
        [TestCase("1234567890")]
        [TestCase("123456789012345678901234567890")]
        public void TryToLogonFailWithProviderTest(string tokenReturned)
        {
            AuthenProviderFirebase provider = getFirebaseProvider(tokenReturned);

            Logon lo = new Logon();
            lo.SetAuthenticationProvider(provider);

            DbContext ctx = getInmemoryDbContextForQueryTesting("TryToLogonFailWithProviderTest");
            CTable data = new CTable();

            data.SetFieldValue("USER_NAME", "username");
            data.SetFieldValue("PASSWORD", "password");

            try
            {
                lo.Apply(data, ctx);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Authentication error - invalid token returned!!!", e.Message, "Error message not match !!!");
            }
        }

        [TestCase("123456789012345678901234567890A")]
        [TestCase("123456789012345678901234567890ABCDEFG")]
        public void TryToLogonSuccessWithProviderTest(string tokenReturned)
        {
            AuthenProviderFirebase provider = getFirebaseProvider(tokenReturned);

            Logon lo = new Logon();
            lo.SetAuthenticationProvider(provider);

            DbContext ctx = getInmemoryDbContextForQueryTesting("TryToLogonFailWithProviderTest");
            CTable data = new CTable();

            data.SetFieldValue("USER_NAME", "username");
            data.SetFieldValue("PASSWORD", "password");

            try
            {
                CTable dataOut = lo.Apply(data, ctx);

                Assert.AreEqual("mocked@gmail.com", dataOut.GetFieldValue("EMAIL"), "Email should be returned!!!");
                Assert.AreEqual("ThisIsLocalID", dataOut.GetFieldValue("UID"), "Local ID should be returned!!!");                
            }
            catch (Exception)
            {
                Assert.Fail("Should not get any exception here");
            }
        }                  
    }
}