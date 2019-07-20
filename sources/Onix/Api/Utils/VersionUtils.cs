using System;
using System.Reflection;

namespace Onix.Api.Utils
{    
    public static class VersionUtils
    {
        public static string GetVersion()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString(); 
            return version;
        }     
    }    
}