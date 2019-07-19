using System.Collections;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Erp.Dao.Models;
using Onix.Test.Commons;

namespace Onix.Test.Api.Erp.Dao.Employees
{
    public class TestEmployee : TestBase
    {
        protected ArrayList addEmployees(OnixDbContext db, int cnt)
        {
            ArrayList arr = new ArrayList();
            
            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplAddEmployee", db); 
            for (int i=1; i<=cnt; i++)
            {
                CTable data = new CTable();
                data.SetFieldValue("EMPLOYEE_ID", i);
                data.SetFieldValue("EMPLOYEE_CODE", "CODE" + i);
                data.SetFieldValue("EMPLOYEE_NAME", "NAME" + i);
                data.SetFieldValue("HOUR_RATE", 1.10 + i);

                arr.Add(data);
                mnpl.Apply(data);
            }

            return(arr);
        } 

        protected ArrayList addEmployeesWithSomeSameName(OnixDbContext db, int cnt)
        {
            ArrayList arr = new ArrayList();

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplAddEmployee", db); 
            for (int i=1; i<=cnt; i++)
            {
                CTable data = new CTable();
                data.SetFieldValue("EMPLOYEE_ID", i);
                data.SetFieldValue("EMPLOYEE_CODE", "CODE" + i);

                int remainder = i % 3;
                data.SetFieldValue("EMPLOYEE_NAME", "NAME" + remainder);

                arr.Add(data);
                mnpl.Apply(data);
            }

            return(arr);
        }  

        private void addBanks(OnixDbContext db)
        {
            MMasterRef mr1 = new MMasterRef() { MasterId = 1, Code = "BBL", Description = "Bangkok bank"};
            db.MasterRef.Add(mr1);

            MMasterRef mr2 = new MMasterRef() { MasterId = 2, Code = "KTB", Description = "Krung thai bank"};
            db.MasterRef.Add(mr2);

            MMasterRef mr3 = new MMasterRef() { MasterId = 3, Code = "BAY", Description = "Bank of Ayuthaya"};
            db.MasterRef.Add(mr3);

            db.SaveChanges();   
        }

        protected ArrayList addEmployeesWithBank(OnixDbContext db, int cnt)
        {
            ArrayList arr = new ArrayList();

            addBanks(db);

            IDatabaseManipulation mnpl = FactoryDbOperation.GetDataManipulationObject("MnplAddEmployee", db); 
            for (int i=1; i<=cnt; i++)
            {
                int remainder = i % 3;

                CTable data = new CTable();
                data.SetFieldValue("EMPLOYEE_ID", i);
                data.SetFieldValue("EMPLOYEE_CODE", "CODE" + i);
                data.SetFieldValue("BANK_ID", remainder+1);
                data.SetFieldValue("EMPLOYEE_NAME", "NAME" + remainder);

                arr.Add(data);
                mnpl.Apply(data);
            }

            return(arr);
        }                
    }
}