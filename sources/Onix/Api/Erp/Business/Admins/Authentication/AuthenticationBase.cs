using System;

using Onix.Api.Commons;
using Onix.Api.Commons.Business;
using Onix.Api.Erp.Business.Admins.AuthenProviders;

namespace Onix.Api.Erp.Business.Admins.Authentication
{    
	public class AuthenticationBase : BusinessOperationBase
	{
        private IAuthenProvider authenProvider = new AuthenProviderFirebase();
        
        protected IAuthenProvider getAuthenticationProvider()
        {
            return authenProvider;
        }

        public void SetAuthenticationProvider(IAuthenProvider provider)
        {
            authenProvider = provider;
        }        
    }    
}