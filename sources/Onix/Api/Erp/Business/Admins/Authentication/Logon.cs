using System;

using Onix.Api.Commons;

namespace Onix.Api.Erp.Business.Admins.Authentication
{    
	public class Logon : AuthenticationBase
	{
        protected override CTable Execute(CTable dat)
        {
            CTable o = getAuthenticationProvider().Login(dat);

            //Query user role and info here
            o.SetFieldValue("USER_ROLE", "Users");

            return o;
        }
    }    
}
