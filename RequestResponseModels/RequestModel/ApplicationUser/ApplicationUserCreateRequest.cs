namespace mandiri_project.RequestResponseModels.RequestModel.ApplicationUser
{
    public class ApplicationUserCreateRequest
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string Phone { get; set; } = null!;
        public string BusinessAreaCode { get; set; } = null!;
    }
}
