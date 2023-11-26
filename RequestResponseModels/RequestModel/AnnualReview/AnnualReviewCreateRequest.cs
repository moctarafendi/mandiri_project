namespace mandiri_project.RequestResponseModels.RequestModel.AnnualReview
{
    public class AnnualReviewCreateRequest
    {
       // public Guid Id { get; set; }
        public string EmployeeNo { get; set; } = null!;
        public DateTime ReviewDate { get; set; }
       
    }
}
