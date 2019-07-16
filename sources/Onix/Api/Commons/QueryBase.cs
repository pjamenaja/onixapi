using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Onix.Api.Commons
{        
    public delegate IQueryable QueryDelegate(CTable data);
    public delegate void PopulateCTable(CTable data, ViewBase vw, ArrayList configs);
    public delegate ArrayList ConfigFields();
    public delegate IQueryable OrderByDelegate<in T>(IQueryable<T> query) where T : ViewBase;

    public delegate Expression CustomWhereExprDelegate(ParameterExpression param, CTable data);

    public delegate bool QueryFilterDelegate(CTable data);

    class QueryConfig
    {
        public QueryDelegate QueryFunc = null;
        public PopulateCTable PopulateFunc = null;
        public ArrayList FieldConfigs = null;
        public OrderByDelegate<ViewBase> OrderByFunc = null;
    }

    class QueryConfigParam
    {
        public int ChunkSize = 150;
        public String PageNumberField = "SYS.PAGE";
        public CustomWhereExprDelegate CustomWhereExpressFunc = null;
        public QueryFilterDelegate QueryFilterFunc = null;
    }

    class FieldConfig
    {
        public String ObjectName = "";
        public String PropertyName = "";
        public String FieldType = "";
        public String FieldName = "";
        public Boolean WhereFlag = false;
        public Boolean SelectFlag = true;
        public string FilterFuncName = "";
    }

    public class QueryBase : IDatabaseQuery
    {
        private readonly QueryConfigParam param = new QueryConfigParam();
        private readonly Hashtable configs = new Hashtable();
        private int totalRow = 0;
        private int totalChunk = 0;

        protected DbContext dbContext = null;

        private QueryConfig getConfig(int index)
        {
            return (QueryConfig) configs[index];
        }

        protected void setUp(QueryDelegate func, 
            PopulateCTable popFunc, ConfigFields cfgFunc, OrderByDelegate<ViewBase> orderFunc)
        {
            QueryConfig cfg = new QueryConfig();
            cfg.QueryFunc = func;
            cfg.PopulateFunc = popFunc;
            cfg.OrderByFunc = orderFunc;

            if (cfgFunc != null)
            {
                ArrayList arr = cfgFunc();
                ArrayList tempArr = new ArrayList();
                foreach (String s in arr)
                {
                    string[] fields = s.Split(':');
                    string property = fields[0];
                    string[] tokens = property.Split('.');

                    FieldConfig fcfg = new FieldConfig();
                    fcfg.FieldType = fields[1];
                    fcfg.FieldName = fields[2];
                    fcfg.WhereFlag = "Y".Equals(fields[3]);
                    fcfg.SelectFlag = "Y".Equals(fields[4]);                
                    fcfg.ObjectName = tokens[0];
                    fcfg.PropertyName = tokens[1];  

                    tempArr.Add(fcfg);
                }

                cfg.FieldConfigs = tempArr;
            }

            configs.Add(0, cfg);
        }        

        protected Expression<Func<T, bool>> getWhereLambda<T>(CTable data) where T : ViewBase
        {
            Type type = typeof(T);            

            ParameterExpression startParam = Expression.Parameter(type);
            QueryConfig queryCfg = getConfig(0);

            Expression expr = Expression.Constant(true);
             foreach (FieldConfig cfg in queryCfg.FieldConfigs)
            {
                if (!cfg.WhereFlag)
                {
                    continue;
                }

                string value = data.GetFieldValue(cfg.FieldName);
                string name = String.Format("{0}.{1}", cfg.ObjectName, cfg.PropertyName);

                if (!string.IsNullOrEmpty(value) && cfg.FieldType.Equals("S"))                
                {
                    Expression likeExpr = QueryExpression.GetLikeExpr(startParam, name, value);
                    expr = Expression.And(expr, likeExpr);                    
                }
                else if (!string.IsNullOrEmpty(value) && cfg.FieldType.Equals("C"))                
                {
                    //String equal exactly
                    Expression likeExpr = QueryExpression.GetEqualsExpr(startParam, name, value);
                    expr = Expression.And(expr, likeExpr);                    
                }
                else if (!string.IsNullOrEmpty(value) && cfg.FieldType.Equals("CHECK_NULL"))                
                {
                    Expression nullExpr = QueryExpression.GetNullExpr(startParam, name, value);
                    expr = Expression.And(expr, nullExpr);                    
                }
                else if (!string.IsNullOrEmpty(value) && cfg.FieldType.Equals("INC_SET"))                
                {
                    Expression inSetExpr = QueryExpression.GetInSetExpr(startParam, name, value, type);
                    expr = Expression.And(expr, inSetExpr);                    
                }
                else if (!string.IsNullOrEmpty(value) && cfg.FieldType.Equals("EXC_SET"))                
                {
                    Expression inSetExpr = QueryExpression.GetNotInSetExpr(startParam, name, value, type);
                    expr = Expression.And(expr, inSetExpr);                    
                }
                else if (!string.IsNullOrEmpty(value) && cfg.FieldType.Equals("FD"))                
                {
                    Expression fromDateExpr = QueryExpression.GetGreaterThanExpr(startParam, name, value);
                    expr = Expression.And(expr, fromDateExpr);             
                }
                else if (!string.IsNullOrEmpty(value) && cfg.FieldType.Equals("TD"))                
                {
                    Expression toDateExpr = QueryExpression.GetLessThanExpr(startParam, name, value);
                    expr = Expression.And(expr, toDateExpr);             
                }                                                    
                else if (!string.IsNullOrEmpty(value)) 
                {
                    //Treat as number 
                    Expression idEqualExpr = QueryExpression.GetEqualsExpr(startParam, name, Convert.ToInt32(value));
                    expr = Expression.And(expr, idEqualExpr);
                }           
            }

            CustomWhereExprDelegate func = param.CustomWhereExpressFunc;
            if (func != null)
            {
                Expression customExpr = func(startParam, data);
                expr = Expression.And(expr, customExpr);
            }

            return Expression.Lambda<Func<T, bool>>(expr, startParam);
        }

        public QueryBase(DbContext db)
        {
            dbContext = db;
        }

        private void populateData(CTable t, ViewBase vw, ArrayList configs)
        {
            foreach (FieldConfig cfg in configs)
            {
                if (!cfg.SelectFlag)
                {
                    continue;
                }

                var propInfo1 = vw.GetType().GetProperty(cfg.ObjectName);
                var obj = propInfo1.GetValue(vw, null);

                String fieldValue = "";
                if (obj != null)
                {
                    var propInfo2 = obj.GetType().GetProperty(cfg.PropertyName);
                    var value = propInfo2.GetValue(obj, null);
                    if (value != null)
                    {
                        fieldValue = value.ToString();
                    }
                    
                    if (fieldValue == null)
                    {
                        fieldValue = "";
                    }
                }
                t.SetFieldValue(cfg.FieldName, fieldValue);
            }
        }

        private IQueryable applyLimitOffset(IQueryable query, int itemCount, CTable dat)
        {
            IQueryable<ViewBase> q = (IQueryable<ViewBase>) query;

            int limit = param.ChunkSize;
            int currentChunk = 0;

            string fieldValue = dat.GetFieldValue(param.PageNumberField);            
            if (!string.IsNullOrEmpty(fieldValue))
            {
                //Throw exception if not number
                currentChunk = Int32.Parse(fieldValue);
            }

            if (currentChunk <= 0)
            {
                currentChunk = 1;
            }

            int offset = ((currentChunk-1) * limit);
            totalChunk = (int) Math.Ceiling((double) itemCount / limit);

            IQueryable qr = q.Skip(offset).Take(limit);
            return(qr);
        }

        public virtual ArrayList Query(CTable dat, bool chunkFlag)
        {
            ArrayList arr = new ArrayList();
            QueryConfig cfg = getConfig(0);

            IQueryable query = cfg.QueryFunc(dat);
            IQueryable<ViewBase> q = (IQueryable<ViewBase>) query;
            
            if (cfg.OrderByFunc != null)
            {
                query = cfg.OrderByFunc(q);
                totalRow = q.Count();
                if (chunkFlag)
                {
                    query = applyLimitOffset(query, totalRow, dat);
                }
                //Query with chunk must be first order by
            }
            
            foreach (ViewBase v in query)
            {
                CTable tb = new CTable();
                populateData(tb, v, cfg.FieldConfigs);
                cfg.PopulateFunc(tb, v, cfg.FieldConfigs);

                QueryFilterDelegate filterFunc = param.QueryFilterFunc;
                bool include = true;
                if (filterFunc != null)
                {
                    include = filterFunc(tb);
                }

                if (include)
                {
                    arr.Add(tb);
                }
            }

            return(arr);
        }

        public virtual ArrayList Query(CTable dat)
        {
            ArrayList arr = Query(dat, false);
            return(arr);
        }

        public void OverrideOrderBy(OrderByDelegate<ViewBase> orderFunc)
        {
            QueryConfig cfg = getConfig(0);
            cfg.OrderByFunc = orderFunc;
        }

        public void RegisterCustomerWhere(CustomWhereExprDelegate func)
        {
            param.CustomWhereExpressFunc = func;
        }

        public void RegisterQueryFilter(QueryFilterDelegate func)
        {
            param.QueryFilterFunc = func;
        }        

        public void SetPageChunk(string pageNumberFieldName, int pageSize)
        {
            param.PageNumberField = pageNumberFieldName;
            param.ChunkSize = pageSize;
        }

        public void SetChunkSize(int pageSize)
        {
            param.ChunkSize = pageSize;
        } 

        public int GetTotalRow()
        {
            return(totalRow);
        }

        public int GetTotalChunk()
        {
            return(totalChunk);
        }        
    }
}