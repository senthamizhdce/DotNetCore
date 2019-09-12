using System;

namespace DotNetCore.Others.Models
{
    public class DIByHTTPRequest
    {
        public DIByHTTPRequest()
        {
            InjectedDateTime = DateTime.Now.ToString();
        }
        public readonly string InjectedDateTime;
    }
}
