using System.Collections.Generic;

namespace TMarket.WEB.RequestModels.Errors
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
