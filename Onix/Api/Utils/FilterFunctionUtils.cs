using System;
using System.Collections;

namespace Onix.Api.Utils
{
    public delegate string FilterFuncDelegate(string param);

    public class FilterFunctionUtils
    {
        private static Hashtable funcMap = null;

        private static string GetCurrentDateTimeStr(string paramValue)
        {
            DateTime currentDtm = DateTime.Now;
            string str = DateUtils.DateTimeToDateStringInternal(currentDtm);

            return str;
        }

        private static void initFilterFuncMap()
        {
            addFilterFunc("GetCurrentDateTimeStr", GetCurrentDateTimeStr);
        }

        private static void addFilterFunc(string funcName, FilterFuncDelegate filterFunc)
        {
            funcMap.Add(funcName, filterFunc);
        }

        public static string invoke(string funcName, string funcValue)
        {
            if (funcMap == null)
            {
                funcMap = new Hashtable();
                initFilterFuncMap();
            }

            FilterFuncDelegate func = (FilterFuncDelegate) funcMap[funcName];
            if (funcName.Equals("") || (func == null))
            {
                return funcValue;
            }

            string value = func(funcValue);

            return value;
        }
    }
}