namespace mandiri_project.RequestResponseModels.ResponseModel.BusinessAreaMasters
{
    public class BusinessAreaMastersAllResponse
    {
        public int Acknowledge { get; set; }
        public string Message { get; set; } = null!;
        public List<BusinessAreaMastersData> areaMastersDataList { get; set; } = new List<BusinessAreaMastersData>();
       
    }

    public class BusinessAreaMastersData
    {
        public string AreaCode { get; set; } = null!;
        public string AreaName { get; set; } = null!;
    }
}
