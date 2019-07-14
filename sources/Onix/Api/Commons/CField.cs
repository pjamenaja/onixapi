using System;

namespace Onix.Api.Commons
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	class CField
	{
		private String type = "";
		private String value = "";
		private String name = "";
		
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
