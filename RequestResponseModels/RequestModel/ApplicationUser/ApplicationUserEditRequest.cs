namespace mandiri_project.RequestResponseModels.RequestModel.ApplicationUser
{
    public class ApplicationUserEditRequest
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string Phone { get; set; } = null!;
    }
}
