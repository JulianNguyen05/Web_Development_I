using BaiTap5_65133958.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BaiTap5_65133958.Controllers
{
    public class DangKy_65133958Controller : Controller
    {
        // GET: DangKy_65133958
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(HttpPostedFileBase avatarNV, DangKy dangKy)
        {
            if (avatarNV != null && avatarNV.ContentLength > 0)
            {
                string fileName = Path.GetFileName(avatarNV.FileName);
                string savePath = Server.MapPath("~/Images/" + fileName);
                avatarNV.SaveAs(savePath);
                dangKy.avatarNV = fileName;
            }
            else
            {
                dangKy.avatarNV = "noavatar.png";
            }

            string fSave = Server.MapPath("~/dangKy.txt");
            string[] dangKyInfo =
            {
                "Mã NV: " + dangKy.idNV,
                "Họ tên: " + dangKy.nameNV,
                "Ngày sinh: " + dangKy.bodNV.ToShortDateString(),
                "Email: " + dangKy.emailNV,
                "Mật khẩu: " + dangKy.mkNV,
                "Đơn vị: " + dangKy.donViNV,
                "Avatar: " + dangKy.avatarNV,
                "----------------------------------"
            };

            System.IO.File.AppendAllLines(fSave, dangKyInfo);

            ViewBag.idNV = dangKy.idNV;
            ViewBag.nameNV = dangKy.nameNV;
            ViewBag.bodNV = dangKy.bodNV.ToShortDateString();
            ViewBag.emailNV = dangKy.emailNV;
            ViewBag.mkNV = dangKy.mkNV;
            ViewBag.donViNV = dangKy.donViNV;
            ViewBag.avatarNV = Url.Content("~/Images/" + dangKy.avatarNV);

            return View("Confirm");
        }

        public ActionResult Confirm()
        {
            return View();
        }
    }
}
