using System;

namespace Onix.Api.Commons
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	class CField
	{
		private readonly String type;
		private readonly String name;
		private String value;		
		
		public CField(String t, String v, String fldName)
		{
			type = t;
			value = v;
			name = fldName;			
		}
		
		public String GetValue()
		{
			return(value);
		}
		
		public String GetName()
		{
			return(name);
		}
		
		public void SetValue(String v)
		{
			value = v;
		}
	}
}
