using System;
using System.Collections;

namespace Onix.Api.Commons
{
	public interface IDatabaseSequence
	{
        int GetNextValue(string seqName);
    }
}
