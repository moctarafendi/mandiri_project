
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mandiri_project.Entities
{
    public partial class BusinessAreaMaster
    {
        public BusinessAreaMaster()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public string AreaCode { get; set; } = null!;
        public string? AreaName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
