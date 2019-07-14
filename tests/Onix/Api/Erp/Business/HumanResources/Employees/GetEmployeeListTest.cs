using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models;
using Onix.Test.Api.Erp.Dao.Employees;
using Onix.Api.Commons.Business;

namespace Onix.Test.Api.Erp.Business.HumanResources.Employees
{
    public class GetEmployeeListTest : TestEmployee
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(200, 2)]
        [TestCase(151, 2)]
        public void GetEmployeeListSupportChunk(int recCount, int expectedChunk)
        {
            OnixDbContext db = getInmemoryDbContext("GetEmployeeListSupportChunk" + recCount);
            addEmployees(db, recCount);

            IBusinessOperation query = FactoryBusinessOperation.CreateBusinessOperationObject("GetEmployeeList");

            CTable dat = new CTable();
            CTable result = query.Apply(dat, db);

            int chunkCount = result.GetFieldValueInt("EXT_CHUNK_COUNT");
            int recordCount = result.GetFieldValueInt("EXT_RECORD_COUNT");
            ArrayList arr = result.GetChildArray("EMPLOYEE_LIST");

            Assert.AreEqual(recCount, recordCount, "Should get EXT_RECORD_COUNT as expected!!!");
            Assert.AreEqual(expectedChunk, chunkCount, "Should get EXT_CHUNK_COUNT as expected!!!");
        }           
    }
}