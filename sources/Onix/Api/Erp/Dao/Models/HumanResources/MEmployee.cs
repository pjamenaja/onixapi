using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onix.Api.Erp.Dao.Models
{
    [Table("employee")]  
    public class MEmployee : OnixBaseModel
    {
        public MEmployee()
        {
        }

        [Key]
        [Column("employee_id")]
        public int EmployeeId { get; set; }

        [Column("employee_code")]
        public string EmployeeCode { get; set; }

        [Column("employee_name")]
        public string EmployeeName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("website")]
        public string Website { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("fax")]
        public string Fax { get; set; }

        [Column("employee_type")]
        public int? EmployeeType { get; set; }

        [Column("employee_group")]
        public int? EmployeeGroup { get; set; }

        [Column("category")]
        public int? Category { get; set; }

        [Column("note")]
        public string Note { get; set; }

        [Column("branch_id")]
        public int? BranchId { get; set; }

        [Column("commission_cycle_id")]
        public int? CommissionCycleId { get; set; }

        [Column("commission_cycle_type")]
        public int? CommissionCycleType { get; set; }

        [Column("salesman_specific_flag")]
        public char? SalesmanSpecificFlag { get; set; }

        [Column("employee_name_eng")]
        public string EmployeeNameEng { get; set; }

        [Column("employee_lastname")]
        public string EmployeeLastname { get; set; }

        [Column("fingerprint_code")]
        public string FingerprintCode { get; set; }

        [Column("name_prefix")]
        public int? NamePrefix { get; set; }

        [Column("gender")]
        public int? Gender { get; set; }

        [Column("employee_lastname_eng")]
        public string EmployeeLastnameEng { get; set; }

        [Column("line_id")]
        public string LineId { get; set; }

        [Column("employee_position")]
        public int? EmployeePosition { get; set; }

        [Column("employee_department")]
        public int? EmployeeDepartment { get; set; }

        [Column("employee_profile_image")]
        public string EmployeeProfileImage { get; set; }

        [Column("bank_id")]
        public int? BankId { get; set; }

        [Column("account_no")]
        public string AccountNo { get; set; }

        [Column("bank_branch_name")]
        public string BankBranchName { get; set; }

        [Column("id_number")]
        public string IdNumber { get; set; }

        [Column("hour_rate")]
        public decimal? HourRate { get; set; }
    }
}
