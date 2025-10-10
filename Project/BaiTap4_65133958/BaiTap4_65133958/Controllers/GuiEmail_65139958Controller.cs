using BaiTap4_65133958.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BaiTap4_65133958.Controllers
{
    public class GuiEmail_65139958Controller : Controller
    {
        // GET: GuiEmail_65139958
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(MailInfo model)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress("trong.nh.65cntt@ntu.edu.vn");
            mail.To.Add(model.To);
            mail.Subject = model.Subject;
            mail.Body = model.Body;
            mail.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential("trong.nh.65cntt@ntu.edu.vn", "ykzf kvoc vuvr jyri");
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return Content("Đã gửi email.");
        }
    }
}