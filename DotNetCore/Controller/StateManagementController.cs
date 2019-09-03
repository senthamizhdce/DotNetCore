using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateManagementController : ControllerBase
    {
        [HttpGet]
        [Route("GetSession")]
        public IEnumerable<string> GetSession()
        {

            string Value1;

            ISession a = Request.HttpContext.Session;

            if (a.GetString("Value1") == null)
            {
                a.SetString("Value1", "One");
            }

            Value1 = a.GetString("Value1").ToString();

            return new string[] { Value1 };
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