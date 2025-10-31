using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KT0720NguyenHuuTrong_65133958.Models;
using PagedList;

namespace KT0720NguyenHuuTrong_65133958.Controllers
{
    public class SinhVien0720_65133958Controller : Controller
    {
        private KT0720_65133958Entities db = new KT0720_65133958Entities();

        // GET: SinhVien0720_65133958/Index_65133958
        public ActionResult Index_65133958(int? page)
        {
            var sINHVIENs = db.SINHVIENs.Include(s => s.LOP).OrderBy(s => s.MaSV);
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(sINHVIENs.ToPagedList(pageNumber, pageSize));
        }

        // GET: SinhVien0720_65133958/GioiThieu_65133958
        public ActionResult GioiThieu_65133958()
        {
            return View();
        }

        // GET: SinhVien0720_65133958/Details_65133958/5
        public ActionResult Details_65133958(string id) // <-- ĐÃ ĐỔI TÊN
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // GET: SinhVien0720_65133958/Create_65133958
        public ActionResult Create_65133958() // <-- ĐÃ ĐỔI TÊN
        {
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop");
            return View();
        }

        // POST: SinhVien0720_65133958/Create_65133958
        // POST: SinhVien0720_65133958/Create_65133958
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_65133958(
            [Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,DiaChi,MaLop")] SINHVIEN sINHVIEN,
            HttpPostedFileBase AnhUpload)
        {
            try
            {
                // Xử lý file ảnh (giữ nguyên)
                if (AnhUpload != null && AnhUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(AnhUpload.FileName);
                    var serverPath = Path.Combine(Server.MapPath("~/Uploads/Images/"), fileName);
                    AnhUpload.SaveAs(serverPath);
                    sINHVIEN.AnhSV = fileName;
                }
                else
                {
                    sINHVIEN.AnhSV = null;
                }
                ModelState.Remove("AnhSV");

                // Bắt đầu kiểm tra
                if (ModelState.IsValid)
                {
                    // --- THÊM BƯỚC KIỂM TRA TRÙNG LẶP ---
                    // 1. Tìm xem MaSV này đã có trong database chưa
                    var existingStudent = db.SINHVIENs.Find(sINHVIEN.MaSV);

                    if (existingStudent != null)
                    {
                        // 2. Nếu đã tồn tại, thêm lỗi vào ModelState
                        // Lỗi này sẽ tự động hiển thị bên dưới ô "Mã sinh viên"
                        ModelState.AddModelError("MaSV", "Mã sinh viên này đã tồn tại. Vui lòng nhập mã khác.");
                    }
                    else
                    {
                        // 3. Nếu không trùng, mới tiến hành thêm và lưu
                        db.SINHVIENs.Add(sINHVIEN);
                        db.SaveChanges();
                        return RedirectToAction("Index_65133958");
                    }
                    // --- KẾT THÚC BƯỚC KIỂM TRA ---
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm mới: " + ex.Message);
            }

            // Nếu có lỗi (hoặc trùng MaSV), tải lại DropDownList và hiển thị lại trang Create
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", sINHVIEN.MaLop);
            return View(sINHVIEN);
        }

        // GET: SinhVien0720_65133958/Edit_65133958/5
        public ActionResult Edit_65133958(string id) // <-- ĐÃ ĐỔI TÊN
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SINHVIEN sINHVIEN = db.SINHVIENs.Include(s => s.LOP).SingleOrDefault(s => s.MaSV == id);

            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }

            return View(sINHVIEN);
        }

        // POST: SinhVien0720_65133958/Edit_65133958/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_65133958( // <-- ĐÃ ĐỔI TÊN
            [Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,AnhSV,DiaChi,MaLop")] SINHVIEN sINHVIEN)
        {
            // Lưu ý: Cần code xử lý upload file ảnh cho Edit nếu muốn (tương tự Create)
            if (ModelState.IsValid)
            {
                db.Entry(sINHVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_65133958"); // <-- ĐÃ SỬA REDIRECT
            }
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", sINHVIEN.MaLop);
            return View(sINHVIEN);
        }

        // GET: SinhVien0720_65133958/Delete_65133958/5
        public ActionResult Delete_65133958(string id) // <-- ĐÃ ĐỔI TÊN
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // POST: SinhVien0720_65133958/Delete_65133958/5
        [HttpPost, ActionName("Delete_65133958")] // <-- ĐÃ ĐỔI TÊN ACTIONNAME
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_65133958(string id) // <-- ĐÃ ĐỔI TÊN PHƯƠNG THỨC
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            db.SINHVIENs.Remove(sINHVIEN);
            db.SaveChanges();
            return RedirectToAction("Index_65133958"); // <-- ĐÃ SỬA REDIRECT
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: SinhVien0720_65133958/TimKiem_65133958
        public ActionResult TimKiem_65133958(string maSV, string tenSV, int? page)
        {
            // --- Giữ lại giá trị tìm kiếm để điền lại vào text box ---
            ViewBag.MaSVSearch = maSV;
            ViewBag.TenSVSearch = tenSV; // "Họ tên" sẽ tìm theo Tên SV

            // --- Bắt đầu câu truy vấn ---
            // .AsQueryable() để có thể xây dựng truy vấn động
            var sINHVIENs = db.SINHVIENs.Include(s => s.LOP).AsQueryable();

            // Cờ để kiểm tra xem người dùng đã thực hiện tìm kiếm chưa
            bool isSearch = false;

            // 1. Lọc theo Mã SV (tìm kiếm chính xác)
            if (!string.IsNullOrEmpty(maSV))
            {
                sINHVIENs = sINHVIENs.Where(s => s.MaSV == maSV);
                isSearch = true;
            }

            // 2. Lọc theo Tên SV (tìm kiếm gần đúng - contains)
            if (!string.IsNullOrEmpty(tenSV))
            {
                sINHVIENs = sINHVIENs.Where(s => s.TenSV.Contains(tenSV));
                isSearch = true;
            }

            // --- Sắp xếp kết quả ---
            sINHVIENs = sINHVIENs.OrderBy(s => s.MaSV);

            // --- Xử lý thông báo "Không tìm thấy" ---
            // Nếu đã tìm kiếm (isSearch = true) nhưng không có kết quả
            if (isSearch && !sINHVIENs.Any())
            {
                ViewBag.Message = "Không có thông tin cần tìm";
            }

            // --- Quan trọng: Xử lý khi mới tải trang ---
            // Nếu không phải là đang tìm kiếm (mới tải trang),
            // chúng ta trả về 1 danh sách rỗng (không hiển thị tất cả sinh viên)
            if (!isSearch)
            {
                // Trả về danh sách rỗng bằng cách tạo 1 truy vấn không bao giờ đúng
                sINHVIENs = sINHVIENs.Where(s => s.MaSV == "---KHONG---TON---TAI---");
            }

            // --- Phân trang ---
            int pageSize = 2; // 2 dòng trên 1 trang
            int pageNumber = (page ?? 1);

            return View(sINHVIENs.ToPagedList(pageNumber, pageSize));
        }
    }
}