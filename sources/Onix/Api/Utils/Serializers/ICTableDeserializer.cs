using System;
using System.Collections;

using Onix.Api.Commons;

namespace Onix.Api.Utils.Serializers
{
	public interface ICTableDeserializer
	{
        CRoot Deserialize();
    }
}
