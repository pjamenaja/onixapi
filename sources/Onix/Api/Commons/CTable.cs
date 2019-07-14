using System;
using System.Collections;

namespace Onix.Api.Commons
{
	public class CTable
	{
		private Hashtable fieldsHash = new Hashtable();
		private ArrayList fieldsArr = new ArrayList();
		
		private String tbn = "";
		private Hashtable childHash = new Hashtable();
		
		public CTable(String table)
		{
			tbn = table;					
		}
		
		public CTable()
		{				
		}

        public void DumpFields()
        {
            Console.WriteLine("============================");
            foreach (CField f in fieldsArr)
            {
                Console.WriteLine("{0}:{1}", f.GetName(), f.GetValue());
            }
        }

		public void AddChildArray(String itemName, ArrayList items)
		{
			childHash.Add(itemName, items);
		}

        public void RemoveChildArray(String itemName)
        {
            if (childHash.ContainsKey(itemName))
            {
                childHash.Remove(itemName);
            }
        }

        public ArrayList GetChildArray(String itemName)
		{
			return((ArrayList) childHash[itemName]);
		}
		
		public Hashtable GetChildHash()
		{
			return(childHash);
		}
		
		protected void AddField(String fldName, String type, String value)
		{
			CField f = new CField(type, value, fldName);
			fieldsHash.Add(fldName, f);			
			fieldsArr.Add(f);
		}		
		
		public void SetFieldValue(String fldName, String value)
		{
			if (fieldsHash.Contains(fldName))
			{
				CField f = (CField) fieldsHash[fldName];
				f.SetValue(value);				
			}
			else
			{
				AddField(fldName, "S", value);
			}
		}
		
		public void SetFieldValue(String fldName, int value)
		{
            SetFieldValue(fldName, value.ToString());
		}

		public void SetFieldValue(String fldName, double value)
		{
            SetFieldValue(fldName, value.ToString());
		}

		public String GetFieldValue(String fldName)
		{
			if (fieldsHash.Contains(fldName))
			{
				CField f = (CField) fieldsHash[fldName];
				return(f.GetValue());
			}
			
			return("");
		}
		
		public int GetFieldValueInt(String fldName)
		{
            int value = Int16.Parse(GetFieldValue(fldName));			
			return(value);
		}

		public String GetTableName()
		{
			return(tbn);
		}
		
		public ArrayList GetTableFields()
		{
			return(fieldsArr);
		}

        public void CopyFrom(CTable t)
        {
            foreach (CField f in t.GetTableFields())
            {
                SetFieldValue(f.GetName(), f.GetValue());
            }
        }

        public CTable Clone()
        {
            CTable c = new CTable(tbn);

            foreach (CField f in fieldsArr)
            {
                c.AddField(f.GetName(), "S", f.GetValue());
            }

            Hashtable ht = GetChildHash();
            foreach (String key in ht.Keys)
            {
                ArrayList arr = (ArrayList) ht[key];

                ArrayList newArr = new ArrayList();
                c.AddChildArray(key, newArr);

                foreach (CTable t in arr)
                {
                    CTable tb = new CTable(t.GetTableName());
                    newArr.Add(tb);

                    foreach (CField f in t.GetTableFields())
                    {
                        tb.AddField(f.GetName(), "S", f.GetValue());
                    }
                }
            }

            return (c);
        }

        public CTable CloneAll()
        {
            CTable c = new CTable(tbn);

            foreach (CField f in fieldsArr)
            {
                c.AddField(f.GetName(), "S", f.GetValue());
            }

            Hashtable ht = GetChildHash();
            foreach (String key in ht.Keys)
            {
                ArrayList arr = (ArrayList)ht[key];

                ArrayList newArr = new ArrayList();
                c.AddChildArray(key, newArr);

                foreach (CTable t in arr)
                {
                    CTable child = t.CloneAll();
                    newArr.Add(child);
                }
            }

            return (c);
        }
    }
}
