using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onix.Api.Erp.Dao.Models
{
    public partial class OnixBaseModel
    {
        public OnixBaseModel()
        {
        }

        [Column("create_date")]    
        public string CreateDate { get; set; }

        [Column("modify_date")] 
        public string ModifyDate { get; set; }
    }
}
