namespace mandiri_project.RequestResponseModels.ResponseModel.BusinessAreaMasters
{
    public class BusinessAreaMastersDetailsResponse
    {
        public int Acknowledge { get; set; }
        public string Message { get; set; } = null!;
        public string AreaCode { get; set; } = null!;
        public string AreaName { get; set; } = null!;
    }
}
