using System.Xml.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Erp.Dao.Models.UnitTesting;

namespace Onix.Api.Erp.Dao.Queries.UnitTesting
{
    public class QueryUnitTestingGeneric : QueryBase
    {
        private DbContextUnitTesting context = null;

        public QueryUnitTestingGeneric(DbContextUnitTesting db) : base(db)
        {
            context = db;
            setUp(genericQuery, populateGenetic, configGenericQuery, getGenericOrderBy);
        }

        private ArrayList configGenericQuery()
        {
            ArrayList arr = new ArrayList();

            arr.Add("UT.PrimaryKeyId:ID:PRIMARY_KEY_ID:Y:Y");
            arr.Add("UT.UniqueKeyCode:C:UNIQUE_KEY_CODE:Y:Y");
            arr.Add("UT.StringField1:S:STRING_FIELD1:Y:Y");
            arr.Add("UT.StringField2:S:STRING_FIELD2:Y:Y");
            arr.Add("UT.StatusNotNullKey:REFID:STATUS_NOT_NULL_KEY:Y:Y");

            arr.Add("UT.StatusNullAbleKey:REFID:STATUS_NULLABLE_KEY:Y:Y");
            arr.Add("UT.StatusNullAbleKey:INC_SET:STATUS_NULLABLE_KEY_INC_SET:Y:N");
            arr.Add("UT.StatusNullAbleKey:EXC_SET:STATUS_NULLABLE_KEY_EXC_SET:Y:N");

            arr.Add("UT.StringNullAbleField1:S:STRING_NULLABLE_FIELD1:Y:N");
            arr.Add("UT.StringNullAbleField1:INC_SET:FIELD1_NULLABLE_KEY_INC_SET:Y:N");
            arr.Add("UT.StringNullAbleField1:EXC_SET:FIELD1_NULLABLE_KEY_EXC_SET:Y:N");

            arr.Add("UT.StatusForIsNullKey:CHECK_NULL:STATUS_FOR_ISNULL_KEY:Y:N");

            arr.Add("UT.DocumentDate:FD:FROM_DOCUMENT_DATE:Y:N");
            arr.Add("UT.DocumentDate:TD:TO_DOCUMENT_DATE:Y:N");
        
            return(arr);
        }

        private IQueryable genericQuery(CTable data)
        {   
            var query = (
                from ut in context.UnitTestingTable                
                select new ViewUnitTesting {UT = ut});

            Expression<Func<ViewUnitTesting, bool>> expr = getWhereLambda<ViewUnitTesting>(data);
            if (expr != null)
            {
                query = query.Where(expr);
            }

            return(query);
        } 

        private IQueryable getGenericOrderBy<T>(IQueryable<T> query) where T : ViewBase
        {
            IQueryable<ViewUnitTesting> q = (IQueryable<ViewUnitTesting>) query;
            q = q.OrderBy(i => i.UT.PrimaryKeyId);
            return(q);
        }

        private void populateGenetic(CTable t, ViewBase vw, ArrayList configs)
        {                    
        }
    }
}