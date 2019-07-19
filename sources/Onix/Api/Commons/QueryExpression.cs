using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;

using Onix.Api.Utils;

namespace Onix.Api.Commons
{        
    public static class QueryExpression
    {
        public static Expression GetExpression(ParameterExpression param, string property)
        {
            Expression body = param;
            foreach (var member in property.Split('.')) 
            {
                body = Expression.PropertyOrField(body, member);
            }
        
            return(body);
        }

        private static Expression getIntInSetExpr(ParameterExpression param, string property, string value)
        {
            string list = value.Replace("(", "").Replace(")", "");
            string[] fields = list.Split(',');

            int cnt = fields.Count();
            List<int?> exprLists = new List<int?>();
            for (int i=0; i<cnt; i++)
            {
                if (!fields[i].Equals(""))
                {
                    exprLists.Add(Convert.ToInt32(fields[i]));
                }
            }
            Expression body = GetExpression(param, property);
            Expression val = Expression.Constant(exprLists);

            Type[] arr = new Type[1] {typeof(int?)};

            var method = typeof(List<int?>).GetMethod("Contains", arr);
            Expression expr = Expression.Call(val, method, body);

            return expr;
        }

        private static Expression getStringInSetExpr(ParameterExpression param, string property, string value)
        {
            string list = value.Replace("(", "").Replace(")", "");
            string[] fields = list.Split(',');

            int cnt = fields.Count();
            List<string> exprLists = new List<string>();
            for (int i=0; i<cnt; i++)
            {
                exprLists.Add(fields[i]);
            }
            Expression body = GetExpression(param, property);
            Expression val = Expression.Constant(exprLists);

            val = convertToNullable(val, body);
            
            Type[] arr = new Type[1] {typeof(string)};

            var method = typeof(List<string>).GetMethod("Contains", arr);
            Expression expr = Expression.Call(val, method, body);

            return expr;
        }

        public static Expression GetInSetExpr(ParameterExpression param, string property, string value, Type type)
        {
            Type t = type;
            PropertyInfo prop = null;
            foreach (string nm in property.Split('.'))
            {
                prop = t.GetProperty(nm);
                t = prop.PropertyType;
            }

            Expression expr = null;
            if (t.Equals(typeof(string)))
            {
                expr = getStringInSetExpr(param, property, value);
            }
            else
            {
                expr = getIntInSetExpr(param, property, value);
            }

            return expr;
        }

        public static Expression GetNotInSetExpr(ParameterExpression param, string property, string value, Type type)
        {
            Expression expr = GetInSetExpr(param, property, value, type);
            expr = Expression.Not(expr);
            return(expr);
        }

        public static Expression GetGreaterThanExpr(ParameterExpression param, string property, string value)
        {
            Expression body = GetExpression(param, property);

            string minDate = DateUtils.DateTimeToDateStringInternalMin(DateUtils.InternalDateToDate(value));            
            Expression val = Expression.Constant(minDate);

            val = convertToNullable(val, body);

            Type[] arr = new Type[1] {typeof(string)};
            var method = typeof(string).GetMethod("CompareTo", arr);
            Expression expr = Expression.Call(body, method, val);
            expr = Expression.GreaterThanOrEqual(expr, Expression.Constant(0));

            return expr;
        }

        public static Expression GetLessThanExpr(ParameterExpression param, string property, string value)
        {
            Expression body = GetExpression(param, property);

            string maxDate = DateUtils.DateTimeToDateStringInternalMax(DateUtils.InternalDateToDate(value));            
            Expression val = Expression.Constant(maxDate);

            val = convertToNullable(val, body);

            Type[] arr = new Type[1] {typeof(string)};
            var method = typeof(string).GetMethod("CompareTo", arr);
            Expression expr = Expression.Call(body, method, val);
            expr = Expression.LessThanOrEqual(expr, Expression.Constant(0));

            return expr;
        }

        public static Expression GetEqualsExpr(ParameterExpression param, string property, string value)
        {
            Expression body = GetExpression(param, property);
            Expression val = Expression.Constant(value);

            val = convertToNullable(val, body);

            return Expression.Equal(body, val);
        }

        public static Expression GetNullExpr(ParameterExpression param, string property, string value)
        {
            Expression body = GetExpression(param, property);
            Expression val = Expression.Constant(null);

            val = convertToNullable(val, body);

            if (value.Equals("Y"))
            {
                return Expression.Equal(body, val);    
            }

            return Expression.NotEqual(body, val);
        }
        
        public static Expression GetEqualsExpr(ParameterExpression param, string property, int value)
        {
            Expression body = GetExpression(param, property);
            Expression val = Expression.Constant(value);

            val = convertToNullable(val, body);

            return Expression.Equal(body, val);
        }        

        private static Expression convertToNullable(Expression expr1, Expression expr2)
        {
            Type t = expr2.Type;
            bool isNullable = t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);

            if (isNullable)
            {
                return Expression.Convert(expr1, expr2.Type);
            }

            return expr1;
        }
        
        public static Expression GetLikeExpr(ParameterExpression param, string property, string value)
        {
            Expression body = GetExpression(param, property);
            Expression val = Expression.Constant(value);

            val = convertToNullable(val, body);

            Type[] arr = new Type[1] {typeof(string)};
            Expression expr = Expression.Call(body, typeof(string).GetMethod("Contains", arr), val);
            return(expr);
        }
    }
}