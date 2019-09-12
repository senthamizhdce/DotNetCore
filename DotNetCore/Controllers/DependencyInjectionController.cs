using DotNetCore.Others.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependencyInjectionController : ControllerBase
    {
        private readonly DIAppData MyAppData;
        private readonly DIByHTTPRequest ByHTTPRequest;
        private readonly DIByObjectRequest ByObjectRequest;
        //private DIByHTTPRequest ByHTTPRequest2;
        //private DIByObjectRequest ByObjectRequest2;
        private IServiceProvider serviceProvider;

        public DependencyInjectionController(DIAppData myService,
            DIByHTTPRequest dIByHTTPRequest,
            DIByObjectRequest dIByObjectRequest,
            IServiceProvider serviceProvider)
        {
            this.MyAppData = myService;
            this.ByHTTPRequest = dIByHTTPRequest;
            this.ByObjectRequest = dIByObjectRequest;            
            this.serviceProvider = serviceProvider;

        }

        [HttpGet("GetInjectedAppData")]
        public ObjectResult GetInjectedAppData( )
        {
            System.Threading.Tasks.Task.Delay(1000).Wait();

            var ByHTTPRequestHetByIServiceProvider = (DIByHTTPRequest)serviceProvider.GetService(typeof(DIByHTTPRequest));
            var ByObjectRequestByIServiceProvider = (DIByObjectRequest)serviceProvider.GetService(typeof(DIByObjectRequest));
 
            return new ObjectResult(new
            {
                MyAppData = MyAppData.InjectedDateTime,
                ByHTTPRequest = ByHTTPRequest.InjectedDateTime,
                ByHTTPRequest2 = ByHTTPRequestHetByIServiceProvider.InjectedDateTime,
                ByObjectRequest = ByObjectRequest.InjectedDateTime,
                ByObjectRequest2 = ByObjectRequestByIServiceProvider.InjectedDateTime
            });
            
        }
    }
}