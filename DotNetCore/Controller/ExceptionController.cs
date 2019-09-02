using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        
        [HttpGet("ThrowException") ]
        public string ThrowException()
        {
            throw new Exception("User Exception from Exception Controller"); 
        }

        [HttpGet]
        [Route("TryCatch")]
        public string TryCatch()
        {
            try
            {
                throw new Exception("User Exception from Exception Controller");                
            }
            catch 
            {
                return "False";
            }

        }

    }
}
