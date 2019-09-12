using DotNetCore.Others.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace DotNetCore.Controllers
{
    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-2.2
    //Cach - https://docs.microsoft.com/en-us/aspnet/core/performance/caching/response?view=aspnetcore-2.2

    [Route("api/[controller]")]
    [ApiController]
    public class StateManagementController : ControllerBase
    {
        private readonly DIAppData MyAppData;
        private readonly IMemoryCache _cache;

        public StateManagementController(
            DIAppData myService,
            IMemoryCache cache)
        {
            this.MyAppData = myService;
            _cache = cache;
        }

        [HttpGet]
        [Route("GetSession")]
        public IActionResult GetSession()
        {

            ISession a = Request.HttpContext.Session;

            if (a.GetString("JSON") == null)//10 Idle seconds time out
            {
                SetObject(a, "JSON", new { Name = "Selvan" });
                SetObject(a, "Date", DateTime.Now);
                a.SetInt32("Int32", DateTime.Now.Second);
                a.SetString("Name", "Selvan");
            }
            dynamic JSON = GetObject<dynamic>(a, "JSON");
            DateTime Date = GetObject<DateTime>(a, "Date");
            string Name = a.GetString("Name");
            Int32? Second = a.GetInt32("Int32");


            return Ok(new { Date = Date.ToString(), Name = Name, JSON = JSON, Second = Second, SessionID = HttpContext.Session.Id });
        }

        [NonAction]
        public static void SetObject(ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        [NonAction]
        public static T GetObject<T>(ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        [HttpGet("GetCookie")]
        public HttpResponseMessage GetCookie()
        {
            var resp = new HttpResponseMessage();

            var cookie = new CookieHeaderValue("Cookie1", "123456");
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            //cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return resp;
        }

        [HttpGet("GetInjectedAppData")]
        public string GetInjectedAppData()
        {
            return this.MyAppData.InjectedDateTime;
        }

        // GET: api/DemoCaching/memorycache
        [HttpGet("memorycache")]
        public string Get()
        {
            var cacheEntry = _cache.GetOrCreate("MyCacheKey", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                return LongTimeOperation();
            });
            return cacheEntry;
        }

        //http://anthonygiretti.com/2018/12/17/common-features-in-asp-net-core-2-2-webapi-caching/

        // GET: api/DemoCaching/responsecache
        [HttpGet("ResponseCache")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public string Get2()
        {
            return LongTimeOperation() + " : " + DateTime.Now.ToString();
        }

        // GET: api/DemoCaching/globalcache
        [HttpGet("globalcache")]
        public string Get3()
        {
            return LongTimeOperation();
        }

        private string LongTimeOperation()
        {
            Thread.Sleep(5000);
            return "Long time operation done!";
        }
    }
}