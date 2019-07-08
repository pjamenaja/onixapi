using System.ComponentModel;
using System.Linq;
using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Commons;
using Onix.Api.Erp.Dao.Models;
using Onix.Api.Erp.Dao.Models.UnitTesting;

namespace Onix.Api.Erp.Dao.Queries.UnitTesting
{
	public class MnplUpdateOnlyField1Testing : ManipulationUpdate
	{
        public MnplUpdateOnlyField1Testing(DbContextUnitTesting db) : base(db)
        {
            setUp(configEditUnitTesting, "PRIMARY_KEY_ID");
        }

        protected override OnixBaseModel createModel()
        {
            MUnitTesting em = new MUnitTesting();
            return(em);
        }

        protected override void updateData(CTable data)
        {
            applyUpdate<MUnitTesting>(data);
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
