using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.Controllers
{
    public class SampleMVCController : Controller
    {
        // GET: SampleMVC
        public ActionResult Index()
        {
            return View();
        }

        // GET: SampleMVC/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SampleMVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SampleMVC/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SampleMVC/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SampleMVC/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SampleMVC/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SampleMVC/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}