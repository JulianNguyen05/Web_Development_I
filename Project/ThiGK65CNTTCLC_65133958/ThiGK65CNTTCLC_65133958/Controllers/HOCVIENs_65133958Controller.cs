using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO; 
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThiGK65CNTTCLC_65133958.Models;
using PagedList; 

namespace ThiGK65CNTTCLC_65133958.Controllers
{
    public class HOCVIENs_65133958Controller : Controller
    {
        private ThiGK65CNTTCLC_65133958Entities2 db = new ThiGK65CNTTCLC_65133958Entities2();

        public ActionResult Index_65133958(int? page)
        {
            var hOCVIENs = db.HOCVIENs.Include(h => h.DOITUONG).OrderBy(h => h.MaHV);
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(hOCVIENs.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GioiThieu_65133958()
        {
            return View();
        }

        public ActionResult Details_65133958(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCVIEN hOCVIEN = db.HOCVIENs.Find(id);
            if (hOCVIEN == null)
            {
                return HttpNotFound();
            }
            return View(hOCVIEN);
        }

        public ActionResult Create_65133958()
        {
            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_65133958(
            [Bind(Include = "MaHV,HoHV,TenHV,AnhDaiDien,NgaySinh,GioiTinh,Email,DiaChi,MaDoiTuong")] HOCVIEN hOCVIEN,
            HttpPostedFileBase AnhUpload)
        {
            try
            {
                if (AnhUpload != null && AnhUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AnhUpload.FileName);
                    var serverPath = Path.Combine(Server.MapPath("~/Uploads/Images/"), fileName);
                    AnhUpload.SaveAs(serverPath);
                    hOCVIEN.AnhDaiDien = fileName;
                }
                else
                {
                    hOCVIEN.AnhDaiDien = null;
                }

                ModelState.Remove("AnhDaiDien");

                if (ModelState.IsValid)
                {
                    var existingHocVien = db.HOCVIENs.Find(hOCVIEN.MaHV);

                    if (existingHocVien != null)
                    {
                        ModelState.AddModelError("MaHV", "Mã học viên này đã tồn tại. Vui lòng nhập mã khác.");
                    }
                    else
                    {
                        db.HOCVIENs.Add(hOCVIEN);
                        db.SaveChanges();
                        return RedirectToAction("Index_65133958");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm mới: " + ex.Message);
            }

            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", hOCVIEN.MaDoiTuong);
            return View(hOCVIEN);
        }

        public ActionResult Edit_65133958(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCVIEN hOCVIEN = db.HOCVIENs.Include(h => h.DOITUONG).SingleOrDefault(h => h.MaHV == id);

            if (hOCVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", hOCVIEN.MaDoiTuong);
            return View(hOCVIEN);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_65133958([Bind(Include = "MaHV,HoHV,TenHV,AnhDaiDien,NgaySinh,GioiTinh,Email,DiaChi,MaDoiTuong")] HOCVIEN hOCVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOCVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_65133958");
            }
            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", hOCVIEN.MaDoiTuong);
            return View(hOCVIEN);
        }

        public ActionResult Delete_65133958(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCVIEN hOCVIEN = db.HOCVIENs.Find(id);
            if (hOCVIEN == null)
            {
                return HttpNotFound();
            }
            return View(hOCVIEN);
        }

        [HttpPost, ActionName("Delete_65133958")] 
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_65133958(string id) 
        {
            HOCVIEN hOCVIEN = db.HOCVIENs.Find(id);
            db.HOCVIENs.Remove(hOCVIEN);
            db.SaveChanges();
            return RedirectToAction("Index_65133958");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult TimKiemHV_65133958(string maHV, string tenHV, int? page, string reset)
        {
            ViewBag.MaHVSearch = maHV;
            ViewBag.TenHVSearch = tenHV;
            ViewBag.ResetSearch = reset; 

            var hOCVIENs = db.HOCVIENs.Include(h => h.DOITUONG).AsQueryable();

            bool isSearch = false;

            if (!string.IsNullOrEmpty(maHV))
            {
                hOCVIENs = hOCVIENs.Where(h => h.MaHV == maHV);
                isSearch = true;
            }

            if (!string.IsNullOrEmpty(tenHV))
            {
                hOCVIENs = hOCVIENs.Where(h => h.TenHV.Contains(tenHV));
                isSearch = true;
            }

            hOCVIENs = hOCVIENs.OrderBy(h => h.MaHV);

            if (isSearch && !hOCVIENs.Any())
            {
                ViewBag.Message = "Không có thông tin cần tìm";
            }

            if (!isSearch && string.IsNullOrEmpty(reset))
            {
                hOCVIENs = hOCVIENs.Where(h => h.MaHV == "---KHONG---TON---TAI---");
            }

            int pageSize = 2;
            int pageNumber = (page ?? 1);

            return View(hOCVIENs.ToPagedList(pageNumber, pageSize));
        }
    }
}