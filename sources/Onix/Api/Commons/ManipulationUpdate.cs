using System.Linq;
using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Commons
{
	public class ManipulationUpdate : ManipulationBase
	{
        public ManipulationUpdate(DbContext db) : base(db)
        {
        }

        public override int Apply(CTable data)
        {
            updateData(data);
            return(1);
        }

        protected virtual void updateData(CTable data)
        {
            //Do nothing            
        }

        protected void applyUpdate<T>(CTable data) where T : OnixBaseModel
        {
            DbContext context = getContext();

            OnixBaseModel em = createModel();      
            DbSet<T> dbSet = getDbSet<T>();

            populatePkProperty(em, data);
            dbSet.Attach((T) em);
            populateProperties(em, data);            

            context.SaveChanges();
            dbContext.Entry(em).State = EntityState.Detached;
        }

        protected void setUp(ConfigFields cfgFunc, string keyName)
        {
            pkFieldName = keyName;

            if (cfgFunc == null)
            {
                return;
            }

            ArrayList arr = cfgFunc();

            //Default fields here
            arr.Add("ModifyDate:MODIFY_DATE:N:Y:GetCurrentDateTimeStr");

            ArrayList tempArr = setUpFieldConfigs(arr); 
            cfg.FieldConfigs = tempArr; 
        }        
    }
}
