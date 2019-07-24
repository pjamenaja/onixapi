using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Firebase.Auth;
using Moq;

using Onix.Api.Commons;
using Onix.Test.Commons;
using Onix.Api.Erp.Business.Admins.AuthenProviders;

namespace Onix.Test.Api.Erp.Business.Admins.AuthenProviders
{
    public class MockedLogonTest : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        private IFirebaseAuthProvider getFirebaseProvider(string tokenReturned)
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
            return mock.Object;
        }
        
        [TestCase("")]
        [TestCase(null)]
        [TestCase("1234567890")]
        [TestCase("123456789012345678901234567890")]
        public void LoginErrorIfTokenIsUnQualifiedTest(string tokenReturned)
        {
            CTable dat = new CTable();
            dat.SetFieldValue("USER_NAME", "username");
            dat.SetFieldValue("PASSWORD", "password");

            IFirebaseAuthProvider provider = getFirebaseProvider(tokenReturned);

            AuthenProviderFirebase authen = new AuthenProviderFirebase();
            authen.SetProvider(provider);                        

            try
            {
                CTable dataOut = authen.Login(dat);
                Assert.Fail("Exception should be thrown !!!");
            }
            catch (Exception e)
            {
                Assert.AreEqual("Authentication error - invalid token returned!!!", e.Message, "Error message not match !!!");
            }
        } 

        [TestCase("123456789012345678901234567890A")]
        [TestCase("123456789012345678901234567890ABCDEFG")]
        public void LoginErrorIfTokenIsQualifiedTest(string tokenReturned)
        {
            CTable dat = new CTable();
            dat.SetFieldValue("USER_NAME", "username");
            dat.SetFieldValue("PASSWORD", "password");

            IFirebaseAuthProvider provider = getFirebaseProvider(tokenReturned);

            AuthenProviderFirebase authen = new AuthenProviderFirebase();
            authen.SetProvider(provider);                        

            try
            {
                CTable dataOut = authen.Login(dat);

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