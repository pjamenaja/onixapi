using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models;
using Onix.Api.Erp.Dao.Models.UnitTesting;

namespace Onix.Test.Api.Erp.Dao.UnitTesting
{
    public class QueryChunkTest : TestQueryClass
    {        
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1000)]
        public void SelectAllRowsWithNoWhereClauseAndOrderByID(int recCount)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectAllRowsWithNoWhereClauseAndOrderByID");                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(recCount, arr.Count, "All records should be returned!!!");

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[recCount-1];

            Assert.AreEqual(1, first.GetFieldValueInt("PRIMARY_KEY_ID"), "Row should be order by ID as default setting!!!");
            Assert.AreEqual(recCount, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Row should be order by ID as default setting");
        } 

        [TestCase(1000, 150)]
        public void SelectFirstChunkRowsWithNoWhereClause(int recCount, int rowPerChunk)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectFirstChunkRowsWithNoWhereClause");                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat, true);

            Assert.AreEqual(rowPerChunk, arr.Count, "First chunk rows should be returned!!!");

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[rowPerChunk-1];

            Assert.AreEqual(1, first.GetFieldValueInt("PRIMARY_KEY_ID"), "First row of chunk is wrong!!!");
            Assert.AreEqual(rowPerChunk, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Last row of chunk is wrong!!!");
        } 

        [TestCase(100, 150)]
        [TestCase(150, 150)]
        public void SelectFirstChunkRowsAllDataLessThanChunkCount(int recCount, int rowPerChunk)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectFirstChunkRowsAllDataLessThanChunkCount" + recCount);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat, true);

            Assert.AreEqual(recCount, arr.Count, "First chunk rows should be returned!!!");

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[recCount-1];

            Assert.AreEqual(1, first.GetFieldValueInt("PRIMARY_KEY_ID"), "First row of chunk is wrong!!!");
            Assert.AreEqual(recCount, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Last row of chunk is wrong!!!");
        }    

        [TestCase(150, 20)]
        public void SelectFirstChunkRowsWithChunkSetting(int recCount, int rowPerChunk)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectFirstChunkRowsWithChunkSetting" + recCount);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat, "EXT_CHUNK_NO", rowPerChunk);

            Assert.AreEqual(rowPerChunk, arr.Count, "Chunk size should be equal what setting!!!"); 

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[rowPerChunk-1];

            Assert.AreEqual(1, first.GetFieldValueInt("PRIMARY_KEY_ID"), "First row of chunk is wrong!!!");
            Assert.AreEqual(rowPerChunk, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Last row of chunk is wrong!!!");                 
        } 

        [TestCase(150, 20, 2)]
        [TestCase(150, 20, 3)]
        [TestCase(150, 20, 6)]
        public void SelectNextChunkRowsWithChunkSetting(int recCount, int rowPerChunk, int chunkNo)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectNextChunkRowsWithChunkSetting" + chunkNo);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("EXT_CHUNK_NO", chunkNo);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat, "EXT_CHUNK_NO", rowPerChunk);

            Assert.AreEqual(rowPerChunk, arr.Count, "Chunk size should be equal what setting!!!"); 

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[rowPerChunk-1];

            int firstID = (chunkNo-1) * rowPerChunk+1;
            int lastID = chunkNo * rowPerChunk;
            Assert.AreEqual(firstID, first.GetFieldValueInt("PRIMARY_KEY_ID"), "First row of chunk is wrong!!!");
            Assert.AreEqual(lastID, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Last row of chunk is wrong!!!");                 
        }

        [TestCase(150, 20, 8)]
        [TestCase(150, 12, 13)]
        public void SelectLastChunkRemainderRowsWithChunkSetting(int recCount, int rowPerChunk, int chunkNo)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectLastChunkRemainderRowsWithChunkSetting" + chunkNo);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("EXT_CHUNK_NO", chunkNo);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat, "EXT_CHUNK_NO", rowPerChunk);

            int cnt = arr.Count;
            int expected = chunkNo * rowPerChunk - recCount;
            Assert.AreEqual(expected, cnt, "Chunk size should be equal what setting!!!"); 

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[cnt-1];

            int firstID = (chunkNo-1) * rowPerChunk+1;
            int lastID = firstID + cnt -1;
            Assert.AreEqual(firstID, first.GetFieldValueInt("PRIMARY_KEY_ID"), "First row of chunk is wrong!!!");
            Assert.AreEqual(lastID, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Last row of chunk is wrong!!!");                 
        } 

        [TestCase(150, 20, 8)]
        [TestCase(150, 12, 13)]
        public void SelectWithChunkAndSeeChunkRelatedValues(int recCount, int rowPerChunk, int lastChunkNo)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWithChunkAndSeeChunkRelatedValues" + lastChunkNo);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            IDatabaseQuery query = createQuery(db, "QueryUnitTestingGeneric");
            query.SetPageChunk("EXT_CHUNK_NO", rowPerChunk*2); //Dummy setting row per chunk
            query.SetChunkSize(rowPerChunk); //Overrided by this method

            CTable dat = new CTable();
            dat.SetFieldValue("EXT_CHUNK_NO", lastChunkNo);
            ArrayList arr = query.Query(dat, true);

            int totalChunk = query.GetTotalChunk();
            Assert.AreEqual(lastChunkNo, totalChunk, "Chunk size should be equal what setting!!!");    

            int totalRow = query.GetTotalRow();
            Assert.AreEqual(recCount, totalRow, "GetTotalRow() returned wrong value!!!");     
        } 

        [TestCase(150, 20, 8)]
        [TestCase(150, 12, 13)]
        public void DefaultChunkFieldNameIsUsed(int recCount, int rowPerChunk, int lastChunkNo)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("DefaultChunkFieldNameIsUsed" + lastChunkNo);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            IDatabaseQuery query = createQuery(db, "QueryUnitTestingGeneric");
            query.SetChunkSize(rowPerChunk);

            CTable dat = new CTable();
            dat.SetFieldValue(defaultChunkField, lastChunkNo);
            ArrayList arr = query.Query(dat, true);
            int cnt = arr.Count;

            int totalChunk = query.GetTotalChunk();
            Assert.AreEqual(lastChunkNo, totalChunk, "Chunk size should be equal what setting!!!");    

            CTable first = (CTable) arr[0];
            CTable last = (CTable) arr[cnt-1];

            int firstID = (lastChunkNo-1) * rowPerChunk+1;
            int lastID = firstID + cnt -1;
            Assert.AreEqual(firstID, first.GetFieldValueInt("PRIMARY_KEY_ID"), "First row of chunk is wrong!!!");
            Assert.AreEqual(lastID, last.GetFieldValueInt("PRIMARY_KEY_ID"), "Last row of chunk is wrong!!!");     
        }                                                                     
    }
}