using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mandiri_project.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            AnnualReviews = new HashSet<AnnualReview>();
        }

        [Key]
        public string EmployeeNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public decimal Salary { get; set; }
        public string BusinessAreaCode { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual BusinessAreaMaster BusinessAreaCodeNavigation { get; set; } = null!;
        public virtual ICollection<AnnualReview> AnnualReviews { get; set; }
    }
}
