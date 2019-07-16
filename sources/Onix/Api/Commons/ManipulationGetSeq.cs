using System.Linq;
using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;

using Onix.Api.Utils;
using Onix.Api.Erp.Dao.Models;

namespace Onix.Api.Commons
{
	public class ManipulationGetSeq : IDatabaseSequence
	{
        private readonly DbContext context = null;

        public ManipulationGetSeq(DbContext db)
        {
            context = db;
        }

        public virtual int GetNextValue(string seqName)
        {
            int seq = 0;
            string sql = String.Format("SELECT NEXTVAL('{0}')", seqName);

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    if (result.Read())
                    {
                        seq = result.GetInt32(0);
                    }
                }
            }

            return(seq);
        }
    }
}
