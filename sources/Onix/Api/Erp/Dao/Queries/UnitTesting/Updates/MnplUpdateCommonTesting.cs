using Onix.Api.Commons;
using Onix.Api.Erp.Dao.Models;
using Onix.Api.Erp.Dao.Models.UnitTesting;

namespace Onix.Api.Erp.Dao.Queries.UnitTesting
{
	public class MnplUpdateCommonTesting : ManipulationUpdate
	{
        public MnplUpdateCommonTesting(DbContextUnitTesting db) : base(db)
        {
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
    }
}
