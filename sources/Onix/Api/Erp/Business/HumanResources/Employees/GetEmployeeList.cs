using System;
using System.Collections;

using Onix.Api.Utils;
using Onix.Api.Commons;
using Onix.Api.Factories;
using Onix.Api.Commons.Business;

namespace Onix.Api.Erp.Business.HumanResources.Employees
{
	public class GetEmployeeList : BusinessOperationBase
	{
        protected override CTable Execute(CTable dat)
        {
            IDatabaseQuery qe = FactoryDbOperation.GetQueryObject("QueryEmployeeGetList", DbContext);
            ArrayList rows = qe.Query(dat, true);

            CTable o = new CTable();
            BusinessOperationUtils.PopulateRow(qe, o, "EMPLOYEE_LIST", rows);

            String s = "Hello";
            String k = "Hello";

            if (s != "")
            {
                throw new ArgumentException("Bad argument", "dat");
            }

            return o;
        }
    }    
}
