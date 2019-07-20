using System;
using Onix.Api.Commons;

namespace Onix.Api.Erp.Business.Admins.AuthenProviders
{
	public class AuthenProviderMocked : IAuthenProvider
	{
        public CTable Login(CTable dat)
        {
            CTable o = new CTable("DATA");

            string password = dat.GetFieldValue("PASSWORD");
            string expectedPassword = dat.GetFieldValue("EXPECTED_PASSWORD"); //This is for NUnit testing only
            if (!password.Equals(expectedPassword))
            {
                throw(new Exception("Password not match!!!"));
            }

            o.SetFieldValue("EMAIL", "mocked@gmail.com");
            o.SetFieldValue("UID", "1234abcdefg");
            o.SetFieldValue("USER_ROLE", "DO NOT CHANGE THIS!!!! - THIS ROLE WILL NOT BE ALLOWED FOR ALL");

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
