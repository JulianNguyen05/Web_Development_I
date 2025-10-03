using BaiTap3_65133958.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTap3_65133958.Controllers
{
    public class NhanVien_65133958Controller : Controller
    {
        // GET: NhanVien_65133958
        public ActionResult Index()
        {
            return View();
        }

        // POST: Index (Nhận dữ liệu đăng ký)
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase Avatar, Emp_65133958Model emp)
        {
            // Lấy tên file ảnh
            string postedFileName = Path.GetFileName(Avatar.FileName);

            // Lưu hình vào thư mục /Images
            var path = Server.MapPath("/Images/" + postedFileName);
            Avatar.SaveAs(path);

            // Lưu thông tin nhân viên vào file emp.txt
            string fSave = Server.MapPath("/emp.txt");
            string[] emInfo =
            {   
                emp.EmpID,
                emp.Name,
                emp.BirthOfDate.ToShortDateString(),
                emp.Email,
                emp.Password,
                emp.Department,
                postedFileName
            };
            System.IO.File.WriteAllLines(fSave, emInfo);

            // Gửi thông tin qua View Confirm
            ViewBag.EmpID = emInfo[0];
            ViewBag.Name = emInfo[1];
            ViewBag.BirthOfDate = emInfo[2];
            ViewBag.Email = emInfo[3];
            ViewBag.Password = emInfo[4];
            ViewBag.Department = emInfo[5];
            ViewBag.Avatar = "/Images/" + emInfo[6];

            return View("Confirm");
        }

        // Trang xác nhận
        public ActionResult Confirm()
        {
            return View();
        }
    }
}