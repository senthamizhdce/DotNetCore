using System;

namespace DotNetCore.Others.Models
{
    public class DIAppData
    {
        public DIAppData()
        {
            InjectedDateTime = DateTime.Now.ToString();
        }
        public readonly string InjectedDateTime;
    }
}
