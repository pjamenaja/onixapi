using System.Collections;
using System.Linq.Expressions;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models;
using Onix.Api.Erp.Dao.Models.UnitTesting;

namespace Onix.Test.Api.Erp.Dao.UnitTesting
{
    public class QueryWhereClauseTest : TestQueryClass
    {        
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1000, "CODE10")]
        [TestCase(1000, "CODE100")]
        public void SelectWhereClause_C_Column(int recCount, string keyCode)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClauseB_C_Column" + keyCode);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("UNIQUE_KEY_CODE", keyCode);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(1, arr.Count, "Should get only 1 record!!!");

            CTable first = (CTable) arr[0];

            Assert.AreEqual(keyCode, first.GetFieldValue("UNIQUE_KEY_CODE"), "Row should be order by ID as default setting!!!");
        }  

        [TestCase(1000, "CODEX0")]
        [TestCase(1000, "CODEX00")]
        public void SelectWhereClauseNotFound_C_Column(int recCount, string keyCode)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClauseNotFound_C_Column" + keyCode);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("UNIQUE_KEY_CODE", keyCode);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(0, arr.Count, "Should not get any record!!!");
        }     

        [TestCase(1000, 9999)]
        [TestCase(1000, 6666)]
        public void SelectWhereClauseNotFound_ID_Column(int recCount, int id)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClauseNotFound_ID_Column" + id);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("PRIMARY_KEY_ID", id);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(0, arr.Count, "Should not get any record!!!");
        } 

        [TestCase(1000, 1)]
        [TestCase(1000, 100)]
        public void SelectWhereClause_ID_Column(int recCount, int id)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClause_ID_Column" + id);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("PRIMARY_KEY_ID", id);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(1, arr.Count, "Should get only 1 record!!!");

            CTable first = (CTable) arr[0];

            Assert.AreEqual(id, first.GetFieldValueInt("PRIMARY_KEY_ID"), "Should get only 1 recod which I selected!!!");
        } 

        [TestCase(1000, "NAME10", 12)]
        [TestCase(1000, "NAME100", 2)]
        public void SelectWhereClause_S_Column(int recCount, string name, int expected)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClause_S_Column" + name);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("STRING_FIELD1", name);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(expected, arr.Count, "Should get number of record as I selected!!!");

            foreach (CTable rec in arr)
            {
                string fieldValue = rec.GetFieldValue("STRING_FIELD1");
                Assert.AreEqual(true, fieldValue.Contains(name), "Selected rows should atleat contains expected value!!!");
            }            
        } 

        [TestCase(1000, "NAMEA0", 0)]
        [TestCase(1000, "NAMEA00", 0)]
        public void SelectWhereClauseNotFound_S_Column(int recCount, string name, int expected)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClauseNotFound_S_Column" + name);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("STRING_FIELD1", name);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(expected, arr.Count, "Should not get any row!!!");    
        } 


        [TestCase(1000, 1)]
        [TestCase(1000, 100)]
        public void SelectWhereClause_REFID_Column(int recCount, int id)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClause_REFID_Column" + id);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("STATUS_NULLABLE_KEY", id);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(1, arr.Count, "Should get only 1 record!!!");

            CTable first = (CTable) arr[0];

            Assert.AreEqual(id, first.GetFieldValueInt("STATUS_NULLABLE_KEY"), "Should get only 1 recod which I selected!!!");
        }

        [TestCase(1000, "1,3,5,10", 4)]
        [TestCase(1000, "9000,1001", 0)]
        [TestCase(1000, "(1,3,5,10)", 4)]
        [TestCase(1000, "(9000,1001)", 0)]
        [TestCase(1000, "()", 0)]
        public void SelectWhereClause_Int_INSET_Column(int recCount, string setString, int expectedReturn)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClause_Int_INSET_Column" + setString);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("STATUS_NULLABLE_KEY_INC_SET", setString);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(expectedReturn, arr.Count, "Should get {0} record!!!", expectedReturn);
        }   

        [TestCase(1000, "1,3,5,10", 996)]
        [TestCase(1000, "9000,1001", 1000)]
        [TestCase(1000, "(1,3,5,10)", 996)]
        [TestCase(1000, "(9000,1001)", 1000)]
        [TestCase(1000, "()", 1000)]
        public void SelectWhereClause_Int_EXSET_Column(int recCount, string setString, int expectedReturn)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClause_Int_EXSET_Column" + setString);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("STATUS_NULLABLE_KEY_EXC_SET", setString);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(expectedReturn, arr.Count, "Should get {0} record!!!", expectedReturn);
        }

        [TestCase(1000, "A1,A3,A5,A10", 4)]
        [TestCase(1000, "A9000,A1001", 0)]
        [TestCase(1000, "(A1,A3,A5,A10)", 4)]
        [TestCase(1000, "(A9000,A1001)", 0)]
        [TestCase(1000, "()", 0)]
        public void SelectWhereClause_Str_INSET_Column(int recCount, string setString, int expectedReturn)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClause_Str_INSET_Column" + setString);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("FIELD1_NULLABLE_KEY_INC_SET", setString);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(expectedReturn, arr.Count, "Should get {0} record!!!", expectedReturn);
        }

        [TestCase(1000, "A1,A3,A5,A10", 996)]
        [TestCase(1000, "A9000,A1001", 1000)]
        [TestCase(1000, "(A1,A3,A5,A10)", 996)]
        [TestCase(1000, "(A9000,A1001)", 1000)]
        [TestCase(1000, "()", 1000)]
        public void SelectWhereClause_Str_EXSET_Column(int recCount, string setString, int expectedReturn)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClause_Str_EXSET_Column" + setString);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("FIELD1_NULLABLE_KEY_EXC_SET", setString);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(expectedReturn, arr.Count, "Should get {0} record!!!", expectedReturn);
        }

        [TestCase(1001, "Y", 501, "abc111")]
        [TestCase(1001, "N", 500, "xyz111")]
        public void SelectWhereClause_ISNULL_Column(int recCount, string setString, int expectedReturn, string dbKey)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClause_ISNULL_Column" + dbKey);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("STATUS_FOR_ISNULL_KEY", setString);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(expectedReturn, arr.Count, "Should get {0} record!!!", expectedReturn);
        } 

        [TestCase(1000, "", "", 1000, "no_from_to_date")]
        [TestCase(1000, "2019/05/25 00:00:00", "2018/05/25 23:59:59", 0, "wrong_to_date")]
        [TestCase(1000, "2019/05/25 00:00:00", "", 500, "no_to_date")]  
        [TestCase(1000, "", "2019/05/25 23:59:59", 1000, "no_from_date")] //Unpredictable for this case : FromDate should not be blank    
        [TestCase(1000, "2020/05/25 00:00:00", "", 0, "beyond_boundary")]
        [TestCase(1000, "2018/05/25 00:00:00", "2018/12/01 00:00:00", 0, "below_boundary")]
        public void SelectWhereClause_DATE_Column(int recCount, string fromDate, string toDate, int expectedReturn, string dbKey)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectWhereClause_DATE_Column" + dbKey);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("FROM_DOCUMENT_DATE", fromDate);
            dat.SetFieldValue("TO_DOCUMENT_DATE", toDate);
            ArrayList arr = createAndQuery(db, "QueryUnitTestingGeneric", dat);

            Assert.AreEqual(expectedReturn, arr.Count, "Should get {0} record!!!", expectedReturn);
        }   

        private Expression customWhereExprFunction(ParameterExpression param, CTable data)
        {
            Expression body = param;
            string property = "UT.PrimaryKeyId";

            foreach (var member in property.Split('.')) 
            {
                body = Expression.PropertyOrField(body, member);
            }

            Expression lowerValue = Expression.Constant(20);
            Expression upperValue = Expression.Constant(100);

            Expression start = Expression.GreaterThan(body, lowerValue);
            Expression end = Expression.LessThanOrEqual(body, upperValue);

            return(Expression.And(start, end));
        }

        [TestCase(1000, 80, "xxxxx")]
        [TestCase(50, 30, "abcdef")]
        public void SelectCustomWhereClause(int recCount, int expectedReturn, string dbKey)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectCustomWhereClause" + dbKey);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            IDatabaseQuery query = createQuery(db, "QueryUnitTestingGeneric");
            query.RegisterCustomerWhere(customWhereExprFunction);

            CTable dat = new CTable();
            ArrayList arr = query.Query(dat);
            int cnt = arr.Count;

            Assert.AreEqual(expectedReturn, arr.Count, "Should get {0} record!!!", expectedReturn);
        }

        private bool QueryFilterFunction(CTable data)
        {
            data.SetFieldValue("NEW_DUMMY_FIELD", "Hello World");
            int id = data.GetFieldValueInt("PRIMARY_KEY_ID");

            return((id % 2) == 0);
        }

        [TestCase(1000, 500, "many_rows")]
        [TestCase(50, 25, "even_rows")]
        [TestCase(51, 25, "odd_rows")]
        public void SelectCustomFilterFunction(int recCount, int expectedReturn, string dbKey)
        {
            DbContextUnitTesting db = getInmemoryDbContextForQueryTesting("SelectCustomFilterFunction" + dbKey);                    
            ArrayList sources = addMUnitTesting(db, recCount);

            IDatabaseQuery query = createQuery(db, "QueryUnitTestingGeneric");
            query.RegisterQueryFilter(QueryFilterFunction);

            CTable dat = new CTable();
            ArrayList arr = query.Query(dat);
            int cnt = arr.Count;

            Assert.AreEqual(expectedReturn, arr.Count, "Should get {0} record!!!", expectedReturn);

            CTable first = (CTable) arr[0];
            Assert.AreEqual("Hello World", first.GetFieldValue("NEW_DUMMY_FIELD"), "Should get new field populated!!!");            
        }                              
    }
}