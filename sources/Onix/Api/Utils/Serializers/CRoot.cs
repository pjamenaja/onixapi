using System;
using System.Collections;

using Onix.Api.Commons;

namespace Onix.Api.Utils.Serializers
{
	public class CRoot
    {
		private CTable param = null;
		private CTable data = null;
        private readonly Hashtable hashOfArray = new Hashtable();

		public CRoot(CTable prm, CTable dta)
		{
            param = prm;
            data = dta;
		}
		
        public CTable Param
        {
            get
            {
                return (param);
            }

            set
            {
                param = value;
            }
        }

        public CTable Data
        {
            get
            {
                return (data);
            }

            set
            {
                data = value;
            }
        }
    }
}
