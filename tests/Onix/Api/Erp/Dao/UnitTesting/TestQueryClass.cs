using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models.UnitTesting;
using Onix.Test.Commons;

namespace Onix.Test.Api.Erp.Dao.UnitTesting
{
    public class TestQueryClass : TestBase
    {
        protected string defaultChunkField = "SYS.PAGE";

        protected ArrayList addMUnitTesting(DbContextUnitTesting db, int cnt)
        {
            ArrayList arr = new ArrayList();       

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplAddUnitTesting", db); 
            for (int i=1; i<=cnt; i++)
            {
                CTable data = new CTable();
                data.SetFieldValue("PRIMARY_KEY_ID", i);
                data.SetFieldValue("UNIQUE_KEY_CODE", "CODE" + i);
                data.SetFieldValue("STRING_FIELD1", "NAME" + i);
                data.SetFieldValue("STRING_FIELD2", "NAME" + i);
                data.SetFieldValue("STATUS_NOT_NULL_KEY", i);
                data.SetFieldValue("STATUS_NULLABLE_KEY", i);
                data.SetFieldValue("STRING_NULLABLE_FIELD1", "A" + i.ToString());
                if ((i % 2) == 0)
                {
                    //Not null only if even number; Insert null for odd number
                    data.SetFieldValue("STATUS_FOR_ISNULL_KEY", i);
                    data.SetFieldValue("DOCUMENT_DATE", "2019/05/25 10:00:34");
                }

                arr.Add(data);
                mnpl.Apply(data);
            }

            return(arr);
        }           
    }
}