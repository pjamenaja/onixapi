using System;
using System.Collections;

using Onix.Api.Commons;
using Onix.Api.Commons.Business;

using Firebase.Auth;

namespace Onix.Api.Erp.Business.Admins.Authentication
{
	public class MockedLogon : BusinessOperationBase
	{
        protected override CTable Execute(CTable dat)
        {
            string password = dat.GetFieldValue("PASSWORD");
            string expectedPassword = dat.GetFieldValue("EXPECTED_PASSWORD"); //This is for NUnit testing only
            if (!password.Equals(expectedPassword))
            {
                throw(new Exception("Password not match!!!"));
            }

            CTable o = new CTable("DATA");            
            o.SetFieldValue("EMAIL", "mocked@gmail.com");
            o.SetFieldValue("UID", "1234abcdefg");
            o.SetFieldValue("USER_ROLE", "DO NOT CHANGE THIS!!!! - THIS ROLE WILL NOT BE ALLOWED FOR ALL");

            return o;
        }
    }    
}
