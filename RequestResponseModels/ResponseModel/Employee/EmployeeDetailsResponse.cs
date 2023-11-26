namespace mandiri_project.RequestResponseModels.ResponseModel.Employee
{
    public class EmployeeDetailsResponse
    {
        public int Acknowledge { get; set; }
        public string Message { get; set; } = null!;
        public string EmployeeNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public decimal Salary { get; set; }
        public string BusinessAreaCode { get; set; } = null!;
    }
}
