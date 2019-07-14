using System.ComponentModel;
using System.Linq;
using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Erp.Dao.Queries.Employees
{
	public class MnplAddEmployee : ManipulationInsert
	{
        public MnplAddEmployee(OnixDbContext db) : base(db)
        {
            setUp(configAddEdmployee);
        }

        protected override OnixBaseModel createModel()
        {
            MEmployee em = new MEmployee();
            return(em);
        }

        protected override void addData(CTable data)
        {
            applyAdd<MEmployee>(data);
        }

        private ArrayList configAddEdmployee()
        {
            ArrayList arr = new ArrayList();

            arr.Add("EmployeeId:EMPLOYEE_ID");
            arr.Add("EmployeeCode:EMPLOYEE_CODE");
            arr.Add("EmployeeName:EMPLOYEE_NAME");
            arr.Add("EmployeeLastname:EMPLOYEE_LASTNAME");
            arr.Add("Address:ADDRESS");
            arr.Add("Email:EMAIL");
            arr.Add("Website:WEBSITE");
            arr.Add("Phone:PHONE");
            arr.Add("BankId:BANK_ID");

            return(arr);
        }

    }
}
