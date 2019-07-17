using System.Collections;
using Onix.Api.Erp.Dao.Models.UnitTesting;

namespace Onix.Api.Erp.Dao.Queries.UnitTesting
{
	public class MnplUpdateOnlyField1Testing : MnplUpdateCommonTesting
	{
        public MnplUpdateOnlyField1Testing(DbContextUnitTesting db) : base(db)
        {
            setUp(configEditUnitTesting, "PRIMARY_KEY_ID");
        }

        private ArrayList configEditUnitTesting()
        {
            ArrayList arr = new ArrayList();

            arr.Add("PrimaryKeyId:PRIMARY_KEY_ID");
            arr.Add("StringField1:STRING_FIELD1");

            return(arr);
        }

    }
}
