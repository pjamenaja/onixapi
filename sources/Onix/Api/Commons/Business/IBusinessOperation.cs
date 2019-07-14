using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;

namespace Onix.Api.Commons.Business
{
	public interface IBusinessOperation
	{
        CTable Apply(CTable dat, DbContext ctx);
    }
}
