using System.ComponentModel;
using System.Linq;
using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Erp.Dao.Queries.Employees
{
	public class MnplUpdateEmployee : ManipulationUpdate
	{
        public MnplUpdateEmployee(OnixDbContext db) : base(db)
        {
            setUp(configEditEdmployee, "EMPLOYEE_ID");
        }

        protected override OnixBaseModel createModel()
        {
            MEmployee em = new MEmployee();
            return(em);
        }

        protected override void updateData(CTable data)
        {
            applyUpdate<MEmployee>(data);
        }

        private ArrayList configEditEdmployee()
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

            return(arr);
        }

    }
}
