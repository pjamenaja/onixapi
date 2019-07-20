using System;

using Onix.Api.Commons;
using Onix.Api.Erp.Business.Admins.AuthenProviders;

namespace Onix.Api.Erp.Business.Admins.Authentication
{
	public class MockedLogon : AuthenticationBase
	{
        protected override CTable Execute(CTable dat)
        {
            SetAuthenticationProvider(new AuthenProviderMocked());
            IAuthenProvider provider = getAuthenticationProvider();
            CTable o = provider.Login(dat);

            return o;
        }
    }    
}
