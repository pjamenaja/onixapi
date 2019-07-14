using System.Linq;
using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Utils;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Commons
{
	public class ManipulationInsert : ManipulationBase
	{
        public ManipulationInsert(DbContext db) : base(db)
        {
        }

        public override int Apply(CTable data)
        {
            addData(data);
            return(1);
        }

        protected virtual void addData(CTable data)
        {
            //Do nothing            
        }

        protected void applyAdd<T>(CTable data) where T : OnixBaseModel
        {
            DbContext context = getContext();

            OnixBaseModel em = createModel();
            populateProperties(em, data);

            DbSet<T> dbSet = getDbSet<T>();
            dbSet.Add((T) em);

            context.SaveChanges();
            context.Entry(em).State = EntityState.Detached;            
        }

        protected void setUp(ConfigFields cfgFunc)
        {
            if (cfgFunc == null)
            {
                return;
            }

            ArrayList arr = cfgFunc();

            //Default fields here
            arr.Add("CreateDate:CREATE_DATE:GetCurrentDateTimeStr");
            arr.Add("ModifyDate:MODIFY_DATE:GetCurrentDateTimeStr");

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

            cfg.FieldConfigs = tempArr; 
        }        
    }
}
