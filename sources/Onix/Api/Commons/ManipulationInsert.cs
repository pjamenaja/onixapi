using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Commons
{
	public abstract class ManipulationInsert : ManipulationBase
	{
        protected abstract void addData(CTable data);

        protected ManipulationInsert(DbContext db) : base(db)
        {
        }

        public override int Apply(CTable data)
        {
            addData(data);
            return(1);
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
            ArrayList arr = cfgFunc();

            //Default fields here
            arr.Add("CreateDate:CREATE_DATE:GetCurrentDateTimeStr");
            arr.Add("ModifyDate:MODIFY_DATE:GetCurrentDateTimeStr");

            ArrayList tempArr = setUpFieldConfigs(arr); 
            cfg.FieldConfigs = tempArr; 
        }        
    }
}
