using System;
using Onix.Api.Commons;

using Firebase.Auth;

namespace Onix.Api.Erp.Business.Admins.AuthenProviders
{
	public class AuthenProviderFirebase : IAuthenProvider
	{
        private IFirebaseAuthProvider provider = null;

        public void SetProvider(IFirebaseAuthProvider pr)
        {
            provider = pr;
        }

        public CTable Login(CTable dat)
        {
            string apiKey = dat.GetFieldValue("API_KEY");
            string userName = dat.GetFieldValue("USER_NAME");
            string password = dat.GetFieldValue("PASSWORD");

            CTable o = new CTable("DATA");
            o.SetFieldValue("USER_ROLE", "DO NOT CHANGE THIS!!!! - THIS ROLE WILL NOT BE ALLOWED FOR ALL");

            if (provider == null)
            {
                provider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            }

            //MUST throw exception if error!!!!
            var auth = provider.SignInWithEmailAndPasswordAsync(userName, password);

            var result = auth.Result;
            string firebaseToken = result.FirebaseToken;
            if ((firebaseToken == null) || (firebaseToken.Length <= 30))
            {
                throw(new Exception("Authentication error - invalid token returned!!!"));
            }

            //May add "FIREBASE_TOKEN" from result.FirebaseToken in the future;
            o.SetFieldValue("EMAIL", result.User.Email);
            o.SetFieldValue("UID", result.User.LocalId);            

            return o;
        }

        public CTable CreateAccount(CTable dat)
        {
            return null;
        }

        public CTable DeleteAccount(CTable dat)
        {
            return null;
        }

        public CTable ChangePassword(CTable dat)
        {
            return null;
        }        
    }
}
