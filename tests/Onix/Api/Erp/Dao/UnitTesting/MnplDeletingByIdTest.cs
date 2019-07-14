using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models;
using Onix.Api.Erp.Dao.Models.UnitTesting;

namespace Onix.Test.Api.Erp.Dao.UnitTesting
{
    public class MnplDeletingByIdTest : TestQueryClass
    {        
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1001, 0, true)]
        [TestCase(1002, 0, false)]
        public void DeleteObjectsAfterQuery(int recCount, int rowAfterDeleted, bool cloneFLag)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("DeleteObjectsAfterQuery" + recCount);                    
            addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);
            int cnt = arr.Count;

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplDeleteByIdUnitTesting", db); 
            int i = 0;
            foreach (CTable row in arr)
            {
                int id = row.GetFieldValueInt("PRIMARY_KEY_ID");
                i++;

                CTable newObj = row;
                if (cloneFLag)
                {
                    //Delete by new object
                    newObj = new CTable();
                    newObj.SetFieldValue("PRIMARY_KEY_ID", id);
                }
                
                mnpl.Apply(row);
            }

            //Query again
            ArrayList newArr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            //We should get the original value
            Assert.AreEqual(rowAfterDeleted, newArr.Count, "Should not get any records after deleted them all!!!");
        }  

        [TestCase(1000, 500)]
        public void DeleteSomeObjectsAfterQuery(int recCount, int rowAfterDeleted)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("DeleteSomeObjectsAfterQuery" + recCount);                    
            addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);
            int cnt = arr.Count;

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplDeleteByIdUnitTesting", db); 
            int i = 0;
            foreach (CTable row in arr)
            {
                i++;

                int id = row.GetFieldValueInt("PRIMARY_KEY_ID");
                if ((i % 2) == 0)
                {
                    row.SetFieldValue("PRIMARY_KEY_ID", id);
                    mnpl.Apply(row);                    
                }                
            }

            ArrayList newArr = createAndQuery(db, "QueryUnitTestingGeneric", dat);                    
            Assert.AreEqual(rowAfterDeleted, newArr.Count, "Should get some records after deleted some of them!!!");
            
            foreach (CTable row in newArr)
            {
                int id = row.GetFieldValueInt("PRIMARY_KEY_ID");
                if ((id % 2) == 0)
                {
                    Assert.Fail("Should note get the record ID=[{0}] because I deleted already!!!", id);
                }                
            }
        }  

        [TestCase(1000, 1000)]
        public void DeleteKeyNotFoundAfterQuery(int recCount, int rowAfterDeleted)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("DeleteKeyNotFoundAfterQuery" + recCount);                    
            addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);
            int cnt = arr.Count;

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplDeleteByIdUnitTesting", db); 
            foreach (CTable row in arr)
            {
                int id = row.GetFieldValueInt("PRIMARY_KEY_ID");
                row.SetFieldValue("PRIMARY_KEY_ID", id + 1000);
                Assert.Throws<DbUpdateConcurrencyException>(() => 
                {
                    //Expected key not found exception
                    mnpl.Apply(row);  
                });
            }

            ArrayList newArr = createAndQuery(db, "QueryUnitTestingGeneric", dat);                    
            Assert.AreEqual(rowAfterDeleted, newArr.Count, "Should not delete anything!!!");
        }
    }
}