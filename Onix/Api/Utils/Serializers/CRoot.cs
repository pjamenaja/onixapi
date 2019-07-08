using System;
using System.Collections;
using System.Collections.Generic;

using Onix.Api.Commons;

namespace Onix.Api.Utils.Serializers
{
	public class CRoot
    {
		private CTable param = null;
		private CTable data = null;
        private Hashtable hashOfArray = new Hashtable();

		public CRoot(CTable prm, CTable dta)
		{
            param = prm;
            data = dta;
		}
		
		public void AddChildArray(String arrName, List<CTable> items)
		{
            hashOfArray.Add(arrName, items);
		}
		
		public List<CTable> GetChildArray(String arrName)
		{
			return((List<CTable>)hashOfArray[arrName]);
		}
		
		public Hashtable GetChildHash()
		{
			return(hashOfArray);
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
