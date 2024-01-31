namespace RainfallAPI.Models
{
    public class ErrorResponseModel
    {
        public string Message { get; set; }
        public List<ErrorDetailModel> Details { get; set; }
    }
}
