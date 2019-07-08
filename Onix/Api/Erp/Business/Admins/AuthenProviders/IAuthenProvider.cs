using System;
using Onix.Api.Commons;

namespace Onix.Api.Erp.Business.Admins.AuthenProviders
{
	public interface IAuthenProvider
	{
        CTable Login(CTable dat);
        CTable CreateAccount(CTable dat);
        CTable DeleteAccount(CTable dat);
        CTable ChangePassword(CTable dat);
    }
}
