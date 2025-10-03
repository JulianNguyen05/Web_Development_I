using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhap.Controllers
{
    public class NhapController : Controller
    {
        // GET: Nhap
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail()
        {
            ViewBag.Id = "SV001";
            ViewBag.Name = "Nguyen Van A";
            ViewData["Marks"] = 20;
            return View();
        }
    }
}