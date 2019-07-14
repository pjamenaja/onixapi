using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Factories;
using Onix.Api.Commons;
using Onix.Api.Erp.Dao.Queries.Employees;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Test.Api.Erp.Dao.Employees
{
    public class QueryEmployeeGetInfoTest : TestEmployee
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(100)]
        public void SelectEmployeeByIDMustBeAllow(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectEmployeeByIDMustBeAllow");                    
            ArrayList sources = addEmployees(db, recCount);

            CTable dat = new CTable();
            dat.SetFieldValue("EMPLOYEE_ID", 1);

            ArrayList arr = createAndQuery(db, "QueryEmployeeGetInfo", dat);

            Assert.AreEqual(1, arr.Count, "Allow only 1 record returned!!!");
        }            
    }
}