using BaiTap5_65133958.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BaiTap5_65133958.Controllers
{
    public class Home_65133958Controller : Controller
    {
        // GET: Trang chủ

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Register(HttpPostedFileBase Avatar, EmpModel emp)
        {
            string postedFileName = Path.GetFileName(Avatar.FileName);
            var path = Server.MapPath("/Images/" + postedFileName);
            Avatar.SaveAs(path);

            string fSave = Server.MapPath("/emp.txt");
            string[] emInfo =
            {
            emp.EmpID, emp.Name, emp.BirthOfDate.ToShortDateString(),
            emp.Email, emp.Password, emp.Department, postedFileName
        };

            System.IO.File.WriteAllLines(fSave, emInfo);

            ViewBag.EmpID = emInfo[0];
            ViewBag.Name = emInfo[1];
            ViewBag.BirthOfDate = emInfo[2];
            ViewBag.Email = emInfo[3];
            ViewBag.Password = emInfo[4];
            ViewBag.Department = emInfo[5];
            ViewBag.Avatar = "/Images/" + emInfo[6];

            return View("Confirm");
        }

        public ActionResult Confirm()
        {
            return View();
        }


        // --- Chức năng Gửi Email (từ Bài 4) ---
        public ActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(MailInfo model)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(model.From);
            mail.To.Add(model.To);
            mail.Subject = model.Subject;
            mail.Body = model.Body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential(model.From, model.Password);
            smtp.EnableSsl = true;
            smtp.Send(mail);

            return Content("Đã gửi email thành công.");
        }


        // --- Chức năng Thay đổi Banner ---
        public ActionResult ChangeBanner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeBanner(HttpPostedFileBase banner)
        {
            string postedFileName = Path.GetFileName(banner.FileName);
            var path = Server.MapPath("/Images/" + postedFileName);
            banner.SaveAs(path);

            string fSave = Server.MapPath("/banner.txt");
            System.IO.File.WriteAllText(fSave, postedFileName);

            return RedirectToAction("Register");
        }
    }
}