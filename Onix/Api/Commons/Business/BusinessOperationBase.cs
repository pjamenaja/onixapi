using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Onix.Api.Factories;

namespace Onix.Api.Commons.Business
{
	public class BusinessOperationBase : IBusinessOperation
	{
        private DbContext context = null;

        protected DbContext DbContext
        {
            get
            {
                return context;
            }
        }

        protected virtual CTable Execute(CTable dat)
        {
            return null;
        }

        public CTable Apply(CTable dat, DbContext ctx)
        {
            context = ctx;

            CTable result = Execute(dat);
            return result;
        }
    }    
}
