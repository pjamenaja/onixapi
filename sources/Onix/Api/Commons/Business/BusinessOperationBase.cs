using System;
using Microsoft.EntityFrameworkCore;

namespace Onix.Api.Commons.Business
{
	public abstract class BusinessOperationBase : IBusinessOperation
	{
        private DbContext context = null;

        protected abstract CTable Execute(CTable dat);

        protected DbContext DbContext
        {
            get
            {
                return context;
            }
        }

        public CTable Apply(CTable dat, DbContext ctx)
        {
            context = ctx;

            CTable result = Execute(dat);
            return result;
        }
    }    
}
