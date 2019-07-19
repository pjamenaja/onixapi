using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onix.Api.Erp.Dao.Models.UnitTesting
{
    public partial class MUnitTesting : OnixBaseModel
    {
        public MUnitTesting()
        {
        }

        [Key]
        public int PrimaryKeyId { get; set; }
        public string UniqueKeyCode { get; set; }
        public string StringField1 { get; set; }
        public string StringField2 { get; set; }
        public int StatusNotNullKey { get; set; }
        public int? StatusNullAbleKey { get; set; }
        public int? StatusForIsNullKey { get; set; }
        public string StringNullAbleField1 { get; set; }
        public string DocumentDate { get; set; }
        public double? ThisIsDoubleField1 { get; set; }
        public double ThisIsDoubleField2 { get; set; }
    }
}
