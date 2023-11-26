namespace mandiri_project.RequestResponseModels.ResponseModel.ApplicationUser
{
    public class ApplicationUserAllResponse
    {
        public int Acknowledge { get; set; }
        public string Message { get; set; } = null!;
       
        public List<ApplicationUserData> appUserDataList { get; set; } = new List<ApplicationUserData>();
    }

    public class ApplicationUserData
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string Phone { get; set; } = null!;
        public string BusinessAreaCode { get; set; } = null!;
    }
}
