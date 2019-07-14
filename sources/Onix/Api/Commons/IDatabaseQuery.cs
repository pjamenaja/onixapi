using System;
using System.Collections;

namespace Onix.Api.Commons
{
	public interface IDatabaseQuery
	{
        ArrayList Query(CTable dat, bool chunkFlag);
        ArrayList Query(CTable dat);
        void OverrideOrderBy(OrderByDelegate<ViewBase> orderFunc);
        void RegisterCustomerWhere(CustomWhereExprDelegate func);
        void RegisterQueryFilter(QueryFilterDelegate func);
        void SetPageChunk(string pageNumberFieldName, int pageSize);
        void SetChunkSize(int pageSize);
        int GetTotalRow();
        int GetTotalChunk();
    }
}
