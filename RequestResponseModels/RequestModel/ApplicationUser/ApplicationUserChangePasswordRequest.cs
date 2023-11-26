namespace mandiri_project.RequestResponseModels.RequestModel.ApplicationUser
{
    public class ApplicationUserChangePasswordRequest
    {
        public string UserId { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
