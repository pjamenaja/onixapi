using System;
using System.Linq;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Utils;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Commons
{
    public class ManipulationConfig
    {
        public ArrayList FieldConfigs { get; set; }
    }

	public abstract class ManipulationBase : IDatabaseManipulation
	{
        protected DbContext dbContext = null;
        protected ManipulationConfig cfg = new ManipulationConfig();
        protected string pkFieldName = "";

        protected abstract OnixBaseModel createModel();
        public abstract int Apply(CTable data);

        protected DbContext getContext()
        {
            return(dbContext);
        }

        public ManipulationBase(DbContext db)
        {
            dbContext = db;
        }

        protected DbSet<T> getDbSet<T>() where T : OnixBaseModel
        {
            DbContext context = getContext();
            DbSet<T> dbSet = context.Set<T>();
            return(dbSet);
        }

        protected void populatePkProperty(OnixBaseModel em, CTable data)
        {
            foreach (FieldConfig fldCfg in cfg.FieldConfigs)
            {
                if (!fldCfg.FieldName.Equals(pkFieldName))
                {
                    continue;
                }

                //Only primary key here and should be only one

                string fieldValue = data.GetFieldValue(fldCfg.FieldName);
                var propInfo = em.GetType().GetProperty(fldCfg.PropertyName);
                propInfo.SetValue(em, Int32.Parse(fieldValue));                            
            }
        }

        protected void populateProperties(OnixBaseModel em, CTable data)
        {
            foreach (FieldConfig fldCfg in cfg.FieldConfigs)
            {
                string fieldValue = data.GetFieldValue(fldCfg.FieldName);
                fieldValue = FilterFunctionUtils.invoke(fldCfg.FilterFuncName, fieldValue);

                var propInfo = em.GetType().GetProperty(fldCfg.PropertyName);
                Type t = propInfo.PropertyType;
                if ((t == typeof(int)) ||  (t == typeof(int?)))
                {
                    if (!string.IsNullOrEmpty(fieldValue))
                    {
                        propInfo.SetValue(em, Int32.Parse(fieldValue));
                    }                     
                }
                else if (t == typeof(double) || t == typeof(double?))
                {
                    if (!string.IsNullOrEmpty(fieldValue))
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

        protected ArrayList setUpFieldConfigs(ArrayList arr)
        {
            ArrayList tempArr = new ArrayList();
            foreach (String s in arr)
            {
                string[] fields = s.Split(':');
                string property = fields[0];
                string valueField = fields[1];

                FieldConfig fcfg = new FieldConfig();
                fcfg.FieldName = valueField;
                fcfg.PropertyName = property;
                if (fields.Count() >= 3)
                {
                    fcfg.FilterFuncName = fields[2];
                }

                tempArr.Add(fcfg);
            }

            return tempArr; 
        }              
    }
}
