namespace mandiri_project.RequestResponseModels.ResponseModel.Employee
{
    public class EmployeeAllResponse
    {
        public int Acknowledge { get; set; }
        public string Message { get; set; } = null!;
        public List<EmployeeAllData> empDataList { get; set; } = new List<EmployeeAllData>();
       
    }

    public class EmployeeAllData
    {
        public string EmployeeNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public decimal Salary { get; set; }
        public string BusinessAreaCode { get; set; } = null!;
    }
}
