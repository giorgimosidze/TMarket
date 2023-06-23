using Newtonsoft.Json;

namespace TMarket.WEB.RequestModels.Errors
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
