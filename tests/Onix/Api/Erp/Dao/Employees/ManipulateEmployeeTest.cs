using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Test.Api.Erp.Dao.Employees
{
    public class ManipulateEmployeeTest : TestEmployee
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(100, 10)]
        [TestCase(100, 20)]
        public void DeleteEmployeeByID(int recCount, int empId)
        {
            OnixDbContext db = getInmemoryDbContext("DeleteEmployeeByID" + empId); //To make database name unique
            ArrayList sources = addEmployeesWithBank(db, recCount);
        
            CTable dat = new CTable();
            dat.SetFieldValue("EMPLOYEE_ID", empId);

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplDeleteEmployee", db); 
            mnpl.Apply(dat);

            //Query by using the same ID
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = 0;
            
            Assert.AreEqual(need, arr.Count, "Employee should be already deleted by EMPLOYEE_ID!!!");
        }  

        [TestCase(100, 10)]
        [TestCase(100, 20)]
        public void UpdateEmployeeByID(int recCount, int empId)
        {
            OnixDbContext db = getInmemoryDbContext("UpdateEmployeeByID" + empId); //To make database name unique
            ArrayList sources = addEmployeesWithBank(db, recCount);
        
            string newName = "SEUBPONG_" + empId;

            CTable dat = new CTable();
            dat.SetFieldValue("EMPLOYEE_ID", empId);
            dat.SetFieldValue("EMPLOYEE_NAME", newName);

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplUpdateEmployee", db); 
            mnpl.Apply(dat);

            //Query by using the same ID
            ArrayList arr = createAndQuery(db, "QueryEmployeeGetList", dat);

            int need = 1;            
            Assert.AreEqual(need, arr.Count, "Employee should be already updated by EMPLOYEE_ID!!!");

            CTable newData = (CTable) arr[0];
            string name = newData.GetFieldValue("EMPLOYEE_NAME");

            Assert.AreEqual(newName, name , "Employee name should be the one I just updated!!!");
        }              
    }
}