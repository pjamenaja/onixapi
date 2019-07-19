using System;

namespace Onix.Api.Commons.Business
{
	public class BusinessOperationOption
	{    
        private readonly string allowRoles;

        public BusinessOperationOption(string allowList)
        {
            allowRoles = allowList;
        }

        public bool IsRoleAllow(string role)
        {
            if (allowRoles.Equals("All"))
            {
                return(true);
            }

            string[] allowList = allowRoles.Split(new Char [] {','});
            foreach (string allow in allowList)
            {
                if (allow.Equals(role))
                {
                    return(true);
                }
            }

            return(false);
        }        
    }
}
