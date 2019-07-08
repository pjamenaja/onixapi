using System;
using System.Collections;
using System.Reflection;
using Onix.Api.Commons.Business;

namespace Onix.Api.Factories
{   
    public class FactoryBusinessOperation
    {
        private static Hashtable classMaps = new Hashtable();
        private static Hashtable roleMaps = new Hashtable();

        private static void addClassConfig(string apiName, string fqdn, BusinessOperationOption option)
        {
            classMaps.Add(apiName, fqdn);
            roleMaps.Add(apiName, option);
        }

        static FactoryBusinessOperation()
        {
            initClassMap();
        }

        private static void initClassMap()
        {
            BusinessOperationOption adminRole = new BusinessOperationOption("Admins");
            BusinessOperationOption userRole = new BusinessOperationOption("Users");
            BusinessOperationOption allowAllRole = new BusinessOperationOption("All");

            addClassConfig("Init", "Onix.Api.Erp.Business.Admins.Authentication.Init", allowAllRole);
            addClassConfig("Logon", "Onix.Api.Erp.Business.Admins.Authentication.Logon", allowAllRole);
            addClassConfig("MockedLogon", "Onix.Api.Erp.Business.Admins.Authentication.MockedLogon", allowAllRole);

            addClassConfig("GetEmployeeList", "Onix.Api.Erp.Business.HumanResources.Employees.GetEmployeeList", userRole);                        
        }

        public static IBusinessOperation CreateBusinessOperationObject(string name)
        {
            string className = (string)classMaps[name];
            if (className == null)
            {
                throw new Exception(String.Format("Operation not found [{0}]", name));
            }

            Assembly asm = Assembly.GetExecutingAssembly();
            IBusinessOperation obj = (IBusinessOperation)asm.CreateInstance(className);

            return(obj);
        }

        public static BusinessOperationOption GetBusinessOperationAllowedRole(string name)
        {
            BusinessOperationOption role = (BusinessOperationOption) roleMaps[name];
            return(role);
        }        
    }
 
}