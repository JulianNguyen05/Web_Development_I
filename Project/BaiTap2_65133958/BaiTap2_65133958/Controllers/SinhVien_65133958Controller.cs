using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTap2_65133958.Controllers
{
    public class SinhVien_65133958Controller : Controller
    {
        // GET: SinhVien_65133958
        public ActionResult Index()
        {
            return View();
        }

        // Request
        public ActionResult UseRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UseRequest(string action)
        {
            if (action == "register")
            {
                ViewBag.Id = Request["id"];
                ViewBag.Name = Request["name"];
                ViewBag.Marks = Request["marks"];
                ViewBag.Source = "UseRequest";

                return View("Result");
            }

            return View();
        }

        // Action
        public ActionResult UseAction()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UseAction(string id, string name, string marks, string action)
        {
            if (action == "register")
            {
                ViewBag.Id = id;
                ViewBag.Name = name;
                ViewBag.Marks = marks;
                ViewBag.Source = "UseAction";

                return View("Result");
            }

            return View();
        }

        // FormCollection
        public ActionResult UseFormCollection()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UseFormCollection(FormCollection form, string action)
        {
            if (action == "register")
            {
                ViewBag.Id = form["id"];
                ViewBag.Name = form["name"];
                ViewBag.Marks = form["marks"];
                ViewBag.Source = "UseFormCollection";

                return View("Result");
            }

            return View();
        }


        // Model
        public ActionResult UseModel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UseModel(Models.SinhVien_65133958Model sv, string action)
        {
            if (action == "register")
            {
                ViewBag.Id = sv.Id;
                ViewBag.Name = sv.Name;
                ViewBag.Marks = sv.Marks;
                ViewBag.Source = "UseModel";

                return View("Result");
            }

            return View();
        }
    }
}