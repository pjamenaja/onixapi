using System;

namespace Onix.Api.Utils
{    
    public static class DateUtils
    {
        public static string DateTimeToDateStringInternalMin(DateTime? dt)
        {
            if (dt == null)
            {
                return ("");
            }

            DateTime dt2 = (DateTime)dt;

            string text = dt2.ToString("yyyy/MM/dd ", System.Globalization.CultureInfo.InvariantCulture);
            return (text + "00:00:00");
        }

        public static string DateTimeToDateStringInternalMax(DateTime? dt)
        {
            if (dt == null)
            {
                return ("");
            }

            DateTime dt2 = (DateTime)dt;

            string text = dt2.ToString("yyyy/MM/dd ", System.Globalization.CultureInfo.InvariantCulture);
            return (text + "23:59:59");
        }

        public static string DateTimeToDateStringInternal(DateTime? dt)
        {
            string text = string.Empty;
            if (dt != null)
                text = dt?.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            return (text);
        }

        public static DateTime InternalDateToDate(string intDate)
        {
            DateTime myDate;

            if (string.IsNullOrEmpty(intDate))
            {
                return (DateTime.Now);
            }

            try
            {
                myDate = DateTime.ParseExact(intDate.Trim(), "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                myDate = DateTime.MinValue;
            }

            return (myDate);
        }
    }
}