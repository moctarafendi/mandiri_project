namespace mandiri_project.RequestResponseModels.RequestModel.Employee
{
    public class EmployeeEditRequest
    {
        public string EmployeeNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public decimal Salary { get; set; }
    }
}
