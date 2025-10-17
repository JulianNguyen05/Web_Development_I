using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTap5_65133958.Controllers
{
    public class ThayDoiLayout_65133958Controller : Controller
    {
        // GET: ThayDoiLayout
        public ActionResult ChangeBanner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeBanner(HttpPostedFileBase banner)
        {
            string postedFileName =
System.IO.Path.GetFileName(banner.FileName);
            var path = Server.MapPath("~/Images/"+postedFileName);
            banner.SaveAs(path);
            string fSave = Server.MapPath("~/banner.txt");
            System.IO.File.WriteAllText(fSave, postedFileName);
            return View();
        }
    }
}