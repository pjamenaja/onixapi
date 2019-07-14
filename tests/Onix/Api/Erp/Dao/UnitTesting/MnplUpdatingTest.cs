using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models;
using Onix.Api.Erp.Dao.Models.UnitTesting;

namespace Onix.Test.Api.Erp.Dao.UnitTesting
{
    public class MnplUpdatingTest : TestQueryClass
    {        
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1001, "NEW_VALUE1", "NEW_VALUE1001", "NAME1", "NAME1001")]
        public void UpdateToObjectsAfterQuery(int recCount, string startValue, string endValue, string orgStart, string orgEnd)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("UpdateToObjectsAfterQuery");                    
            addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);
            int cnt = arr.Count;

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplUpdateAllFieldsUnitTesting", db); 
            int i = 0;
            foreach (CTable row in arr)
            {
                i++;

                //Made sure we will modify the brand new object
                CTable o = row.CloneAll();
                o.SetFieldValue("PRIMARY_KEY_ID", row.GetFieldValueInt("PRIMARY_KEY_ID"));
                o.SetFieldValue("STRING_FIELD1", "NEW_VALUE" + i); //Update only this column
                mnpl.Apply(o);
            }

            //Query again
            ArrayList newArr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            CTable first = (CTable) newArr[0];
            CTable last = (CTable) newArr[cnt-1];

            Assert.AreEqual(startValue, first.GetFieldValue("STRING_FIELD1"), "Should get the value I just updated!!!");
            Assert.AreEqual(endValue, last.GetFieldValue("STRING_FIELD1"), "Should get the value I just updated!!!");

            //We should get the original value
            Assert.AreEqual(orgStart, first.GetFieldValue("STRING_FIELD2"), "Should get the original value!!!");
            Assert.AreEqual(orgEnd, last.GetFieldValue("STRING_FIELD2"), "Should get the original value!!!");
        }


        [TestCase(1001, "NEW_VALUE1", "NEW_VALUE1001", "NAME1", "NAME1001")]
        public void UpdateToObjectsSomeFieldAfterQuery(int recCount, string startValue, string endValue, string orgStart, string orgEnd)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("UpdateToObjectsSomeFieldAfterQuery");                    
            addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);
            int cnt = arr.Count;

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplUpdateOnlyField1Testing", db); 
            int i = 0;
            foreach (CTable row in arr)
            {
                i++;

                //Made sure we will modify the brand new object
                CTable o = new CTable();
                o.SetFieldValue("PRIMARY_KEY_ID", row.GetFieldValueInt("PRIMARY_KEY_ID"));
                o.SetFieldValue("STRING_FIELD1", "NEW_VALUE" + i); //Update only this column
                mnpl.Apply(o);
            }

            //Query again
            ArrayList newArr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            CTable first = (CTable) newArr[0];
            CTable last = (CTable) newArr[cnt-1];

            Assert.AreEqual(startValue, first.GetFieldValue("STRING_FIELD1"), "Should get the value I just updated!!!");
            Assert.AreEqual(endValue, last.GetFieldValue("STRING_FIELD1"), "Should get the value I just updated!!!");

            //We should get the original value
            Assert.AreEqual(orgStart, first.GetFieldValue("STRING_FIELD2"), "Should get the original value!!!");
            Assert.AreEqual(orgEnd, last.GetFieldValue("STRING_FIELD2"), "Should get the original value!!!");
        }                                                                         
    }
}