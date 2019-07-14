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
	public class MnplDeleteByIdUnitTesting : ManipulationDelete
	{
        public MnplDeleteByIdUnitTesting(DbContextUnitTesting db) : base(db)
        {
            setUp(configDeleteUnitTesting);
        }

        protected override OnixBaseModel createModel()
        {
            MUnitTesting em = new MUnitTesting();
            return(em);
        }

        protected override void deleteData(CTable data)
        {
            applyDelete<MUnitTesting>(data);
        }

        private ArrayList configDeleteUnitTesting()
        {
            ArrayList arr = new ArrayList();
            arr.Add("PrimaryKeyId:PRIMARY_KEY_ID");

            return(arr);
        }
    }
}
