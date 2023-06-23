using System;

namespace TMarket.WEB.RequestModels
{
    public class ApiLogItem
    {
        public DateTime RequestTime { get; set; }
        public long ResponseMillis { get; set; }
        public int StatusCode { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public string IpAddress { get; set; }
        public string LocalAdress { get; set; }
        public string LocalPort { get; set; }
        public string RemotePort { get; set; }

        public override string ToString()
        {
            return $"Request Time : {RequestTime}; Response Time Period : {ResponseMillis} ms; IP Address: {IpAddress}; {Environment.NewLine}" +
                   $"LocalAddress : {LocalAdress}; Local Port : {LocalPort}; Remote Port : {RemotePort} {Environment.NewLine}" + 
                $"Status Code : {StatusCode} Method : {Method}; Path : {Path}; QueryString : {QueryString}; {RequestBody}; {ResponseBody}";
        }
    }
}