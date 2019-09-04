using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DotNetCore.Controllers
{
    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-2.2

    [Route("api/[controller]")]
    [ApiController]
    public class StateManagementController : ControllerBase
    {
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
            

            return Ok(new { Date = Date.ToString(), Name = Name, JSON = JSON, Second = Second ,SessionID = HttpContext.Session.Id });
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

        [HttpGet("{id}")]
        public HttpResponseMessage GetCookie(int id)
        {
            var resp = new HttpResponseMessage();

            var cookie = new CookieHeaderValue("session-id", "12345");
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            //cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return resp;
        }
    }
}