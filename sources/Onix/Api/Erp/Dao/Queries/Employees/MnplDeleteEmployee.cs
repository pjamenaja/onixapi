using System.ComponentModel;
using System.Linq;
using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Erp.Dao.Queries.Employees
{
	public class MnplDeleteEmployee : ManipulationDelete
	{
        public MnplDeleteEmployee(OnixDbContext db) : base(db)
        {
            setUp(configDeleteEdmployee);
        }

        protected override OnixBaseModel createModel()
        {
            MEmployee em = new MEmployee();
            return(em);
        }

        protected override void deleteData(CTable data)
        {
            applyDelete<MEmployee>(data);
        }

        private ArrayList configDeleteEdmployee()
        {
            ArrayList arr = new ArrayList();
            arr.Add("EmployeeId:EMPLOYEE_ID");

            return(arr);
        }

    }
}
