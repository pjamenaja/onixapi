using System;
using System.Collections;

namespace Onix.Api.Commons
{
	public interface IDatabaseManipulation
	{
        int Apply(CTable data);
    }
}
