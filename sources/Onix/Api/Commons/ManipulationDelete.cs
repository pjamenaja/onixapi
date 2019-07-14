using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Commons
{
	public class ManipulationDelete : ManipulationBase
	{
        public ManipulationDelete(DbContext db) : base(db)
        {
        }

        public override int Apply(CTable data)
        {
            deleteData(data);
            return(1);
        }

        protected virtual void deleteData(CTable data)
        {
            //Do nothing            
        }

        protected void applyDelete<T>(CTable data) where T : OnixBaseModel
        {
            DbContext context = getContext();

            OnixBaseModel em = createModel(); 
            populateProperties(em, data);

            DbSet<T> dbSet = getDbSet<T>();

            dbSet.Remove((T) em);   
/*                 
            List<T> lists = dbSet.Take(1).ToList();   

            populatePkProperty(em, data);
            dbSet.Attach((T) em);
            populateProperties(em, data);
*/
            context.SaveChanges();
        }

        protected void setUp(ConfigFields cfgFunc)
        {
            if (cfgFunc == null)
            {
                return;
            }

            ArrayList arr = cfgFunc();

            ArrayList tempArr = new ArrayList();
            foreach (String s in arr)
            {
                string[] fields = s.Split(':');
                string property = fields[0];
                string valueField = fields[1];

                FieldConfig fcfg = new FieldConfig();
                fcfg.FieldName = valueField;
                fcfg.PropertyName = property;

                tempArr.Add(fcfg);
            }

            cfg.FieldConfigs = tempArr; 
        }        
    }
}
