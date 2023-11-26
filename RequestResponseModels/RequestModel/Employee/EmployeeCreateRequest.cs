namespace mandiri_project.RequestResponseModels.RequestModel.Employee
{
    public class EmployeeCreateRequest
    {
        public string EmployeeNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string BusinessAreaCode { get; set; } = null!;
    }
}
