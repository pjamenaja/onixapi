using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Erp.Dao.Queries.Employees
{
    public class QueryEmployeeGetInfo : QueryBase
    {
        private OnixDbContext context = null;

        public QueryEmployeeGetInfo(OnixDbContext db) : base(db)
        {
            context = db;
            setUp(getInfoQuery, populateGetInfo, configGetInfo, getInfoOrderBy);
        }

        private ArrayList configGetInfo()
        {
            ArrayList arr = new ArrayList();
            
            //Only ID can be used
            arr.Add("EM.EmployeeId:ID:EMPLOYEE_ID:Y:Y");
            arr.Add("EM.EmployeeCode:C:EMPLOYEE_CODE_EXACT:N:N");
            arr.Add("EM.EmployeeCode:S:EMPLOYEE_CODE:N:Y");
            arr.Add("EM.EmployeeName:S:EMPLOYEE_NAME:N:Y");
            arr.Add("EM.EmployeeLastname:S:EMPLOYEE_LASTNAME:N:Y");
            arr.Add("EM.Address:S:ADDRESS:N:Y");
            arr.Add("EM.Email:S:EMAIL:N:Y");
            arr.Add("EM.Website:S:WEBSITE:N:Y");
            arr.Add("EM.Phone:S:PHONE:N:Y");
            arr.Add("EM.BankId:REFID:BANK_ID:N:Y");

            arr.Add("BNK.Code:S:BANK_CODE:N:Y");
            arr.Add("BNK.Description:S:BANK_NAME:N:Y");

            return(arr);
        }

        private IQueryable getInfoQuery(CTable data)
        {
            var query = (
                from en in context.Employee                
                join mr1 in context.MasterRef on en.BankId equals mr1.MasterId into xx1
                    from bnk in xx1.DefaultIfEmpty()
                select new ViewEmployee {EM = en, BNK = bnk});

            Expression<Func<ViewEmployee, bool>> expr = getWhereLambda<ViewEmployee>(data);
            if (expr != null)
            {
                query = query.Where(expr);
            }

            return(query);
        }

        private void populateGetInfo(CTable t, ViewBase vw, ArrayList cinfigs)
        {            
            t.DumpFields();
        }  

        private IQueryable getInfoOrderBy<T>(IQueryable<T> query) where T : ViewBase
        {
            return(query);
        }          
    }
}