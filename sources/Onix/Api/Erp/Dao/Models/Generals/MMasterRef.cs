using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onix.Api.Erp.Dao.Models
{
    [Table("master_ref")]    
    public class MMasterRef : OnixBaseModel
    {
        public MMasterRef()
        {
        }

        [Key]
        [Column("master_id")] 
        public int MasterId { get; set; }

        [Column("code")]        
        public string Code { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("ref_type")]
        public int RefType { get; set; }

        [Column("optional")]
        public string Optional { get; set; }

        [Column("optional_eng")]
        public string OptionalEng { get; set; }

        [Column("description_eng")]
        public string DescriptionEng { get; set; }
    }
}
