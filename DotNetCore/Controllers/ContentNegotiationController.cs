using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.EF;
//'using Core.Models;
using Microsoft.AspNetCore.Mvc;


namespace Core.Controllers
{
    //public class HomeController : Controller
    public class ContentNegotiationController : Controller
    {
        private IEmployeeRepository _emp;

        public ContentNegotiationController(IEmployeeRepository employeeRepository)
        {
            _emp = employeeRepository;
        }

        
        //JSON result
        public JsonResult Emp()
        {
            return Json(_emp.GetEmployee(1));
        }

        //JSON result
        public JsonResult JSON()
        {
            return Json(new { ID = 1, Name = "Selvan" });
        }

        //Dynamic result 
        public ObjectResult JSONorXML()
        {
            return new ObjectResult(new { ID = 1, Desc = "ObjectResult" });
        }
        //Dynamic result 
        public ViewResult ReturnView()
        {
            return View();
        }

        public ObjectResult Index()
        {
            return new ObjectResult(_emp.GetEmployee(2));
            //return new ObjectResult(new { ID = 1, Desc = "ObjectResult" });
        }
    }
}
