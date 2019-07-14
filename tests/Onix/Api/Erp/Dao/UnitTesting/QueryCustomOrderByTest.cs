using System.Linq;
using System.Reflection.Metadata;
using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models;
using Onix.Api.Erp.Dao.Models.UnitTesting;
using Onix.Api.Erp.Dao.Queries.UnitTesting;

namespace Onix.Test.Api.Erp.Dao.UnitTesting
{
    public class QueryCustomOrderByTest : TestQueryClass
    {
        [SetUp]
        public void Setup()
        {
        }

        private IQueryable getListOrderBy<T>(IQueryable<T> query) where T : ViewBase
        {
            IQueryable<ViewUnitTesting> q = (IQueryable<ViewUnitTesting>) query;
            q = q.OrderByDescending(i => i.UT.PrimaryKeyId);
            return(q);
        }

        [TestCase(150)]
        public void SelectAllWithCustomOrderBy(int recCount)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectAllWithCustomOrderBy" + recCount);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            IDatabaseQuery query = createQuery(db, "QueryUnitTestingGeneric");
            query.OverrideOrderBy(getListOrderBy);

            CTable dat = new CTable();
            ArrayList arr = query.Query(dat);
            int cnt = arr.Count;
            int totalRow = query.GetTotalRow();

            Assert.AreEqual(recCount, cnt, "Total row from array should be equal what setting!!!");   
            Assert.AreEqual(recCount, totalRow, "Total row from GetTotalRow() should be equal what setting!!!");    

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[cnt-1];

            //Descending order
            int firstID = cnt;
            int lastID = 1;

            Assert.AreEqual(firstID, first.GetFieldValueInt("PRIMARY_KEY_ID"), "First row of array is wrong!!!");
            Assert.AreEqual(lastID, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Last row of array is wrong!!!");                 
        }       

        [TestCase(150, 20)]
        public void SelectByChunkWithCustomOrderBy(int recCount, int recordPerChunk)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectByChunkWithCustomOrderBy" + recCount);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            IDatabaseQuery query = createQuery(db, "QueryUnitTestingGeneric");
            query.SetChunkSize(recordPerChunk);
            query.OverrideOrderBy(getListOrderBy);

            CTable dat = new CTable();
            ArrayList arr = query.Query(dat, true);
            int cnt = arr.Count;
            int totalRow = query.GetTotalRow();

            Assert.AreEqual(recordPerChunk, cnt, "Total row of chunk should be equal what setting!!!");   
            Assert.AreEqual(recCount, totalRow, "Total row from GetTotalRow() should be equal what setting!!!");    

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[recordPerChunk-1];

            //Descending order
            int firstID = recCount;
            int lastID = firstID - recordPerChunk + 1;

            Assert.AreEqual(firstID, first.GetFieldValueInt("PRIMARY_KEY_ID"), "First row of array is wrong!!!");
            Assert.AreEqual(lastID, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Last row of array is wrong!!!");                 
        }    


        [TestCase(150, 20)]
        public void SelectByChunkWithWhereAndCustomOrderBy(int recCount, int recordPerChunk)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectByChunkWithWhereAndCustomOrderBy" + recCount);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            IDatabaseQuery query = createQuery(db, "QueryUnitTestingGeneric");
            query.SetChunkSize(recordPerChunk);
            query.OverrideOrderBy(getListOrderBy);

            CTable dat = new CTable();
            dat.SetFieldValue("STRING_FIELD1", "NAME1");

            ArrayList arr = query.Query(dat, true);
            int cnt = arr.Count;
            int totalRow = query.GetTotalRow();

            Assert.AreEqual(recordPerChunk, cnt, "Total row of chunk should be equal what setting!!!");   
            Assert.AreEqual(62, totalRow, "Total row from GetTotalRow() should be equal what setting!!!");    

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[recordPerChunk-1];

            //Descending order
            int firstID = recCount;
            int lastID = firstID - recordPerChunk + 1;

            Assert.AreEqual(firstID, first.GetFieldValueInt("PRIMARY_KEY_ID"), "First row of array is wrong!!!");
            Assert.AreEqual(131, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Last row of array is wrong!!!");                 
        }                                                                                      
    }
}