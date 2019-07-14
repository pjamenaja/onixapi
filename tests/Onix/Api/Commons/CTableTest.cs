using System.Collections;
using NUnit.Framework;

using Onix.Api.Commons;

namespace Onix.Test.Api.Commons
{
    public class CTableTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SetAndTheGetFieldIntEqual()
        {
            string fname = "FIELD1";
            int value = 9999;

            CTable tb = new CTable();

            tb.SetFieldValue(fname, value);
            int i = tb.GetFieldValueInt(fname);

            Assert.AreEqual(value, i, "Set and then get value from same field, they need to be equal!!!");
        }

        [Test]
        public void SetAndTheGetFieldIntString()
        {
            string fname = "FIELD1";
            string value = "dummy";

            CTable tb = new CTable();

            tb.SetFieldValue(fname, value);
            string fldValue = tb.GetFieldValue(fname);

            Assert.AreEqual(value, fldValue, "Set and then get value from same field, they need to be equal!!!");
        } 

        [Test]
        public void TableNameShouldDefaultByConstructor()
        {
            string tableName = "DUMMY_TABLENAME";

            CTable tb = new CTable(tableName);
            string name = tb.GetTableName();

            Assert.AreEqual(tableName, name, "Table name should be equal from constructor!!!");
        }   

        [Test]
        public void AllowDuplicateFieldNameUseLastValue()
        {
            CTable tb = new CTable();
            string fname = "FIELD1";

            tb.SetFieldValue(fname, "DUMMY1");
            tb.SetFieldValue(fname, "DUMMY2");
            tb.SetFieldValue(fname, "DUMMY3");
            
            string value = tb.GetFieldValue(fname);

            Assert.AreEqual("DUMMY3", value, "Last field set value should be used!!!");
        }   

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void AllowDuplicateFieldNameSet(int num)
        {
            CTable tb = new CTable();
            string fname = "FIELD";

            for (int i=1; i<=num; i++)
            {
                //Set same field
                tb.SetFieldValue(fname + i, "DUMMY");
                tb.SetFieldValue(fname + i, "DUMMY");
            }
            
            ArrayList fields = tb.GetTableFields();
            int cnt = fields.Count;

            Assert.AreEqual(num, cnt, "Field count should be only {0}!!!", num);
        }                           
    }
}