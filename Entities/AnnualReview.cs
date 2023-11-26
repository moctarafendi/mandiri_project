using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mandiri_project.Entities
{
    public partial class AnnualReview
    {
        [Key]
        public Guid Id { get; set; }
        public string EmployeeNo { get; set; } = null!;
        public DateTime ReviewDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsDelete { get; set; }

        public virtual Employee EmployeeNoNavigation { get; set; } = null!;
    }
}
