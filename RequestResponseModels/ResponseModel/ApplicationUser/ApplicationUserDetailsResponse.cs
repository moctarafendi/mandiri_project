namespace mandiri_project.RequestResponseModels.ResponseModel.ApplicationUser
{
    public class ApplicationUserDetailsResponse
    {
        public int Acknowledge { get; set; }
        public string Message { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string Phone { get; set; } = null!;
        public string BusinessAreaCode { get; set; } = null!;
    }
}
