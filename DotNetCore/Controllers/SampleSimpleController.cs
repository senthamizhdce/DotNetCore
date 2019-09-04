using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.Controllers
{
    public class SampleSimpleController
    {
        public string Index()
        {
            return "Simple controller. Without inheriting controller class";
        }
    }
}