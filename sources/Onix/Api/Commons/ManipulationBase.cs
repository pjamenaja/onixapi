using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Utils;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Commons
{
    public class ManipulationConfig
    {
        public ArrayList FieldConfigs = null;
    }

	public class ManipulationBase : IDatabaseManipulation
	{
        protected DbContext dbContext = null;
        protected ManipulationConfig cfg = new ManipulationConfig();
        protected string pkFieldName = "";

        protected DbContext getContext()
        {
            return(dbContext);
        }

        public ManipulationBase(DbContext db)
        {
            dbContext = db;
        }

        public virtual int Apply(CTable data)
        {
            return(0);
        }

        protected virtual OnixBaseModel createModel()
        {
            return(null);
        }

        protected DbSet<T> getDbSet<T>() where T : OnixBaseModel
        {
            DbContext context = getContext();
            DbSet<T> dbSet = context.Set<T>();
            return(dbSet);
        }

        protected void populatePkProperty(OnixBaseModel em, CTable data)
        {
            foreach (FieldConfig cfg in cfg.FieldConfigs)
            {
                if (!cfg.FieldName.Equals(pkFieldName))
                {
                    continue;
                }

                //Only primary key here and should be only one

                string fieldValue = data.GetFieldValue(cfg.FieldName);
                var propInfo = em.GetType().GetProperty(cfg.PropertyName);
                propInfo.SetValue(em, Int32.Parse(fieldValue));                            
            }
        }

        protected void populateProperties(OnixBaseModel em, CTable data)
        {
            foreach (FieldConfig cfg in cfg.FieldConfigs)
            {
                string fieldValue = data.GetFieldValue(cfg.FieldName);
                fieldValue = FilterFunctionUtils.invoke(cfg.FilterFuncName, fieldValue);

                var propInfo = em.GetType().GetProperty(cfg.PropertyName);
                Type t = propInfo.PropertyType;
                if ((t == typeof(int)) ||  (t == typeof(int?)))
                {
                    if (!fieldValue.Equals(""))
                    {
                        propInfo.SetValue(em, Int32.Parse(fieldValue));
                    }                     
                }
                else if (t == typeof(double) || t == typeof(int?))
                {
                    if (!fieldValue.Equals(""))
                    {                    
                        propInfo.SetValue(em, Double.Parse(fieldValue));
                    }
                }
                else
                {
                    propInfo.SetValue(em, fieldValue);
                }                                
            }
        }        
    }
}
