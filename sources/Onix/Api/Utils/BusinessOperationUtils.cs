using System.Security.AccessControl;
using System;
using System.Collections;

using Onix.Api.Commons;

namespace Onix.Api.Utils
{    
    public class BusinessOperationUtils
    {
        public static void PopulateRow(IDatabaseQuery qe, CTable data, String arrName, ArrayList rows)
        {
            data.SetFieldValue("EXT_RECORD_COUNT", qe.GetTotalRow());
            data.SetFieldValue("EXT_CHUNK_COUNT", qe.GetTotalChunk()); 

            data.AddChildArray(arrName, rows);
        }
    }
}