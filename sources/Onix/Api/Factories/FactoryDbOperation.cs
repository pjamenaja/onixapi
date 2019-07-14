using System;
using System.Collections;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;

namespace Onix.Api.Factories
{   
    public class FactoryDbOperation
    {
        private static Hashtable classMaps = new Hashtable();

        private static void initQueryClassMap()
        {
            classMaps.Add("QueryUnitTestingGeneric", "Onix.Api.Erp.Dao.Queries.UnitTesting.QueryUnitTestingGeneric");

            classMaps.Add("QueryEmployeeGetList", "Onix.Api.Erp.Dao.Queries.Employees.QueryEmployeeGetList");
            classMaps.Add("QueryEmployeeGetInfo", "Onix.Api.Erp.Dao.Queries.Employees.QueryEmployeeGetInfo");
        }

        private static void initDataManipulationClassMap()
        {
            classMaps.Add("MnplAddUnitTesting", "Onix.Api.Erp.Dao.Queries.UnitTesting.MnplAddUnitTesting");
            classMaps.Add("MnplUpdateAllFieldsUnitTesting", "Onix.Api.Erp.Dao.Queries.UnitTesting.MnplUpdateAllFieldsUnitTesting");
            classMaps.Add("MnplUpdateOnlyField1Testing", "Onix.Api.Erp.Dao.Queries.UnitTesting.MnplUpdateOnlyField1Testing");
            classMaps.Add("MnplDeleteByIdUnitTesting", "Onix.Api.Erp.Dao.Queries.UnitTesting.MnplDeleteByIdUnitTesting");            

            classMaps.Add("ManipulationGetSeq", "Onix.Api.Commons.ManipulationGetSeq");

            classMaps.Add("MnplAddEmployee", "Onix.Api.Erp.Dao.Queries.Employees.MnplAddEmployee");
            classMaps.Add("MnplUpdateEmployee", "Onix.Api.Erp.Dao.Queries.Employees.MnplUpdateEmployee");
            classMaps.Add("MnplDeleteEmployee", "Onix.Api.Erp.Dao.Queries.Employees.MnplDeleteEmployee");            
        }

        static FactoryDbOperation()
        {
            initQueryClassMap();
            initDataManipulationClassMap();
        }

        public static IDatabaseQuery GetQueryObject(string name, DbContext db)
        {
            string className = (string)classMaps[name];
            Type c = Type.GetType(className);
            IDatabaseQuery obj = (IDatabaseQuery)Activator.CreateInstance(c, db);

            return(obj);
        }   

        public static IDatabaseManipulation GetDataManipulationObject(string name, DbContext db)
        {
            string className = (string)classMaps[name];
            Type c = Type.GetType(className);
            IDatabaseManipulation obj = (IDatabaseManipulation)Activator.CreateInstance(c, db);

            return(obj);
        }   

        public static IDatabaseSequence GetDataSequenceObject(string name, DbContext db)
        {
            string className = (string)classMaps[name];
            Type c = Type.GetType(className);
            IDatabaseSequence obj = (IDatabaseSequence)Activator.CreateInstance(c, db);

            return(obj);
        }
    }
}