using System;
using NUnit.Framework;

using Onix.Api.Utils;

namespace Onix.Test.Api.Utils
{
    public class DateUtilsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("2019/07/20", "10:20:45")]
        [TestCase("2019/07/20", "23:59:59")]
        [TestCase("2019/07/20", "00:00:00")]
        [TestCase("2019/07/20", "00:00:01")]
        public void DateTimeToDateStringInternalMinTest(string strDate, string strTime)
        {
            string strDtm = String.Format("{0} {1}", strDate, strTime);
            DateTime myDate = DateTime.ParseExact(strDtm, "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            string dtm = DateUtils.DateTimeToDateStringInternalMin(null);
            Assert.AreEqual("", dtm, "Date should be empty when get null !!!");

            dtm = DateUtils.DateTimeToDateStringInternalMin(myDate);
            string expectedDtm = String.Format("{0} 00:00:00", strDate);
            Assert.AreEqual(expectedDtm, dtm, "Time should be 00:00:00 !!!");            
        }    

        [TestCase("2019/07/20", "10:20:45")]
        [TestCase("2019/07/20", "23:59:59")]
        [TestCase("2019/07/20", "00:00:00")]
        [TestCase("2019/07/20", "00:00:01")]
        public void DateTimeToDateStringInternalMaxTest(string strDate, string strTime)
        {
            string strDtm = String.Format("{0} {1}", strDate, strTime);
            DateTime myDate = DateTime.ParseExact(strDtm, "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            string dtm = DateUtils.DateTimeToDateStringInternalMax(null);
            Assert.AreEqual("", dtm, "Date should be empty when get null !!!");

            dtm = DateUtils.DateTimeToDateStringInternalMax(myDate);
            string expectedDtm = String.Format("{0} 23:59:59", strDate);
            Assert.AreEqual(expectedDtm, dtm, "Time should be 23:59:59 !!!");            
        }

        [TestCase("2019/07/20", "10:20:45")]
        [TestCase("2019/07/20", "23:59:59")]
        [TestCase("2019/07/20", "00:00:00")]
        [TestCase("2019/07/20", "00:00:01")]
        public void DateTimeToStringInternalTest(string strDate, string strTime)
        {
            string strDtm = String.Format("{0} {1}", strDate, strTime);
            DateTime myDate = DateTime.ParseExact(strDtm, "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            string dtm = DateUtils.DateTimeToDateStringInternal(null);
            Assert.AreEqual("", dtm, "Date should be empty when get null !!!");

            dtm = DateUtils.DateTimeToDateStringInternal(myDate);
            string expectedDtm = String.Format("{0} {1}", strDate, strTime);
            Assert.AreEqual(expectedDtm, dtm, "Time should be {0} !!!", strTime);            
        } 

        [TestCase("2019/07/20", "10:20:45")]
        [TestCase("2019/07/20", "23:59:59")]
        [TestCase("2019/07/20", "00:00:00")]
        [TestCase("2019/07/20", "00:00:01")]
        public void InternalDateToDateTest(string strDate, string strTime)
        {
            string strDtm = String.Format("{0} {1}", strDate, strTime);
            DateTime myDate = DateTime.ParseExact(strDtm, "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            DateTime startDtm = DateTime.Now;
            DateTime dtm = DateUtils.InternalDateToDate(""); //Should return DateTime.Now;
            Assert.GreaterOrEqual(dtm, startDtm, "Date should be current date/time when get null !!!");

            dtm = DateUtils.InternalDateToDate(strDtm);
            string convertDate = DateUtils.DateTimeToDateStringInternal(dtm);

            Assert.AreEqual(strDtm, convertDate, "Date should be equal !!!");            
        }  

        [TestCase("2019/00/20", "10:20:45")]
        [TestCase("2019/11/20", "99:99:99")]
        [TestCase("          ", "23:59:59")]
        [TestCase("2019/07/35", "99:00:00")]
        [TestCase("2019/07/00", "00:00:01")]
        public void InvalidInternalDateToDateTest(string strDate, string strTime)
        {
            string strDtm = String.Format("{0} {1}", strDate, strTime);
            DateTime minDate = DateUtils.InternalDateToDate(strDtm);
            Assert.AreEqual(DateTime.MinValue, minDate, "Date should be DateTime.MinValue !!!");            
        }                                                    
    }    
}