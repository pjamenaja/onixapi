using System.Xml.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Erp.Dao.Queries.Employees
{
    public class QueryEmployeeGetList : QueryBase
    {
        private readonly OnixDbContext context = null;

        public QueryEmployeeGetList(OnixDbContext db) : base(db)
        {
            context = db;
            setUp(getListQuery, populateGetList, configGetList, getListOrderBy);
        }

        private ArrayList configGetList()
        {
            ArrayList arr = new ArrayList();

            arr.Add("EM.EmployeeId:ID:EMPLOYEE_ID:Y:Y");
            arr.Add("EM.EmployeeCode:C:EMPLOYEE_CODE_EXACT:Y:N");
            arr.Add("EM.EmployeeCode:S:EMPLOYEE_CODE:Y:Y");
            arr.Add("EM.EmployeeName:S:EMPLOYEE_NAME:Y:Y");
            arr.Add("EM.EmployeeLastname:S:EMPLOYEE_LASTNAME:Y:Y");
            arr.Add("EM.Address:S:ADDRESS:N:Y");
            arr.Add("EM.Email:S:EMAIL:N:Y");
            arr.Add("EM.Website:S:WEBSITE:N:Y");
            arr.Add("EM.Phone:S:PHONE:N:Y");
            arr.Add("EM.BankId:REFID:BANK_ID:N:Y");
            arr.Add("EM.HourRate:N:HOUR_RATE:N:Y");

            arr.Add("EM.CreateDate:S:CREATE_DATE:N:N");
            arr.Add("EM.CreateDate:FD:FROM_CREATE_DATE:N:N");
            arr.Add("EM.CreateDate:TD:TO_CREATE_DATE:N:N");

            arr.Add("BNK.Code:S:BANK_CODE:Y:Y");
            arr.Add("BNK.Description:S:BANK_NAME:Y:Y");

            return(arr);
        }

        private IQueryable getListQuery(CTable data)
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

        private IQueryable getListOrderBy<T>(IQueryable<T> query) where T : ViewBase
        {
            IQueryable<ViewEmployee> q = (IQueryable<ViewEmployee>) query;
            q = q.OrderBy(i => i.EM.EmployeeCode);
            return(q);
        }

        private void populateGetList(CTable t, ViewBase vw, ArrayList configs)
        {  
            //Intended to do nothing                  
        }
    }
}