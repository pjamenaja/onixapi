using System;
using System.Collections;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Onix.Api.Factories
{   
    public class FactoryDbContext
    {
        private static Hashtable dbContextHash = new Hashtable();
        private static Hashtable dbContextHashForTesting = new Hashtable();
        private static Hashtable classMaps = null;

        private static Hashtable initContextMap()
        {
            Hashtable hs = new Hashtable();

            hs.Add("Onix", "Onix.Api.Erp.Dao.Models.OnixDbContext");
            hs.Add("OnixQueryUnitTesting", "Onix.Api.Erp.Dao.Models.UnitTesting.DbContextUnitTesting");

            return(hs);
        }

        public static DbContext GetDbContext(string dbName)
        {
            if (classMaps == null)
            {
                classMaps = initContextMap();
            }

            DbContext ctx = (DbContext) dbContextHash[dbName];            
            if (ctx == null)
            {
                string className = (string)classMaps[dbName];
                Assembly asm = Assembly.GetExecutingAssembly();
                ctx = (DbContext)asm.CreateInstance(className);
                ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                dbContextHash[dbName] = ctx;
            }

            return(ctx);
        }

        public static DbContext GetDbContextForTesting(string dbName, DbContextOptions options)
        {
            //In testing mode let always create a new db context
            
            if (classMaps == null)
            {
                classMaps = initContextMap();
            }

            string className = (string)classMaps[dbName];
            Type c = Type.GetType(className);
            DbContext ctx = (DbContext)Activator.CreateInstance(c, options);
            ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return(ctx);
        }        
    }
 
}