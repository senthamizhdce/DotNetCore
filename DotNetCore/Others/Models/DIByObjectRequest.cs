using System;

namespace DotNetCore.Others.Models
{
    public class DIByObjectRequest
    {
        public DIByObjectRequest()
        {
            InjectedDateTime = DateTime.Now.ToString();
        }
        public readonly string InjectedDateTime ;
    }
}
