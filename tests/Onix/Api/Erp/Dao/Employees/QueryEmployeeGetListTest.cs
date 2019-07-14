using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Test.Api.Erp.Dao.Employees
{
    public class QueryEmployeeGetListTest : TestEmployee
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(100)]
        public void SelectAllEmployeeWithNoWhereClause(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectAllEmployeeWithNoWhereClause");                    
            ArrayList sources = addEmployees(db, recCount);

            CTable dat = new CTable();
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            Assert.AreEqual(recCount, arr.Count, "All records should be returned!!!");
        }

        [TestCase(120)]
        public void SelectEmployeeByNameWildCard(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectEmployeeByNameWildCard");
            ArrayList sources = addEmployeesWithSomeSameName(db, recCount);

            CTable dat = new CTable("");
            dat.SetFieldValue("EMPLOYEE_NAME", "NAME0");
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = recCount/3;

            Assert.AreEqual(need, arr.Count, "EMPLOYEE_NAME should be able to search by wildcard!!!");
        }  

        [TestCase(12)]
        public void SelectEmployeeByCodeWildCard(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectEmployeeByCodeWildCard");
            ArrayList sources = addEmployeesWithSomeSameName(db, recCount);

            CTable dat = new CTable("");
            dat.SetFieldValue("EMPLOYEE_CODE", "CODE1");
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = 4;
            
            Assert.AreEqual(need, arr.Count, "EMPLOYEE_CODE should be able to search by wildcard!!!");
        } 

        [TestCase(12)]
        public void SelectEmployeeByCodeExactMatch(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectEmployeeByCodeExactMatch");
            ArrayList sources = addEmployeesWithSomeSameName(db, recCount);

            CTable dat = new CTable("");
            dat.SetFieldValue("EMPLOYEE_CODE_EXACT", "CODE1");
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = 1;
            
            Assert.AreEqual(need, arr.Count, "EMPLOYEE_CODE should be able to search by exact match!!!");
        } 

        [TestCase(12)]
        public void SelectEmployeeByCodeExactMatchNotFound(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectEmployeeByCodeExactMatchNotFound");
            ArrayList sources = addEmployeesWithSomeSameName(db, recCount);

            CTable dat = new CTable("");
            dat.SetFieldValue("EMPLOYEE_CODE_EXACT", "CODE");
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = 0;
            
            Assert.AreEqual(need, arr.Count, "EMPLOYEE_CODE should be able to search by exact match!!!");
        }            

        [TestCase(100)]
        public void SelectEmployeeByCodeWildCardGetAll(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectEmployeeByCodeWildCardGetAll");
            ArrayList sources = addEmployeesWithSomeSameName(db, recCount);

            CTable dat = new CTable("");
            dat.SetFieldValue("EMPLOYEE_CODE", "CODE");
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = 100;
            
            Assert.AreEqual(need, arr.Count, "EMPLOYEE_CODE should be able to search by wildcard!!!");
        }  

        [TestCase(10)]
        public void SelectEmployeeByBankCodeWildCard(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectEmployeeByBankCodeWildCard");
            ArrayList sources = addEmployeesWithBank(db, recCount);

            CTable dat = new CTable("");
            dat.SetFieldValue("BANK_CODE", "BB");
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = 3;
            
            Assert.AreEqual(need, arr.Count, "BANK_CODE should be able to search by wildcard!!!");
        }

        [TestCase(10)]
        public void SelectEmployeeByBankNameWildCard(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectEmployeeByBankNameWildCard");
            ArrayList sources = addEmployeesWithBank(db, recCount);

            CTable dat = new CTable("");
            dat.SetFieldValue("BANK_NAME", "Krung");
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = 4;
            
            Assert.AreEqual(need, arr.Count, "BANK_NAME should be able to search by wildcard!!!");
        }    

        [TestCase(100)]
        public void SelectEmployeeByID(int recCount)
        {
            OnixDbContext db = getInmemoryDbContext("SelectEmployeeByID");
            ArrayList sources = addEmployeesWithBank(db, recCount);

            int id = 2;

            CTable dat = new CTable("");            
            dat.SetFieldValue("EMPLOYEE_ID", id);
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = 1;            
            Assert.AreEqual(need, arr.Count, "EMPLOYEE_ID should be able to search as integer!!!");

            CTable result = (CTable) arr[0];
            int resultID = result.GetFieldValueInt("EMPLOYEE_ID");

            Assert.AreEqual(id, resultID, "EMPLOYEE_ID must match what I expected!!!");
        }              
    }
}