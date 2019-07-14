using System;
using System.Collections;

using Onix.Api.Erp.Dao.Models;
using Onix.Api.Commons;
//namespace Onix.DAO.Model
//namespace Onix.DAO.Query.AccountDoc
//namespace Onix.Busniess.Account.Sale

namespace Onix.Api.Erp.Dao.Queries.Employees
{
    public class ViewEmployee : ViewBase
    {
        public MEmployee EM {get; set;}
        public MMasterRef BNK {get; set;}
    }    
}