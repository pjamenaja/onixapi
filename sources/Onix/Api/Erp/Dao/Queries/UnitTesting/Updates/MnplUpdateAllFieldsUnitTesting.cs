using System.Collections;
using Onix.Api.Erp.Dao.Models.UnitTesting;

namespace Onix.Api.Erp.Dao.Queries.UnitTesting
{
	public class MnplUpdateAllFieldsUnitTesting : MnplUpdateCommonTesting
	{
        public MnplUpdateAllFieldsUnitTesting(DbContextUnitTesting db) : base(db)
        {
            setUp(configEditUnitTesting, "PRIMARY_KEY_ID");
        }

        private ArrayList configEditUnitTesting()
        {
            ArrayList arr = new ArrayList();

            arr.Add("PrimaryKeyId:PRIMARY_KEY_ID");
            arr.Add("UniqueKeyCode:UNIQUE_KEY_CODE");
            arr.Add("StringField1:STRING_FIELD1");
            arr.Add("StringField2:STRING_FIELD2");
            arr.Add("StatusNotNullKey:STATUS_NOT_NULL_KEY");
            arr.Add("StatusNullAbleKey:STATUS_NULLABLE_KEY");
            arr.Add("StringNullAbleField1:STRING_NULLABLE_FIELD1");
            arr.Add("StatusForIsNullKey:STATUS_FOR_ISNULL_KEY");
            arr.Add("DocumentDate:DOCUMENT_DATE");      

            return(arr);
        }

    }
}
