-- 1. Tạo Cơ sở dữ liệu
CREATE DATABASE ThiGK65CNTTCLC_65133958;
GO

-- 2. Sử dụng CSDL vừa tạo
USE ThiGK65CNTTCLC_65133958;
GO

-- 3. Tạo bảng DOITUONG
CREATE TABLE DOITUONG (
    MaDoiTuong NVARCHAR(10) PRIMARY KEY,
    TenDoiTuong NVARCHAR(100) NOT NULL
);
GO

-- 4. Tạo bảng HOCVIEN
CREATE TABLE HOCVIEN (
    MaHV NVARCHAR(10) PRIMARY KEY,
    HoHV NVARCHAR(50) NOT NULL,
    TenHV NVARCHAR(50) NOT NULL,
    AnhDaiDien NVARCHAR(255) NULL, 
    NgaySinh DATE,
    GioiTinh BIT,
    Email NVARCHAR(100) NULL, 
    DiaChi NVARCHAR(255),
    MaDoiTuong NVARCHAR(10),

    CONSTRAINT FK_HOCVIEN_DOITUONG FOREIGN KEY (MaDoiTuong) REFERENCES DOITUONG(MaDoiTuong)
);
GO

-- 5. Thêm 3 dòng dữ liệu cho bảng DOITUONG
INSERT INTO DOITUONG (MaDoiTuong, TenDoiTuong) VALUES
('DT01', N'Con em gia đình chính sách'),
('DT02', N'Hộ nghèo/Cận nghèo'),
('DT03', N'Đối tượng khác');
GO

-- 6. Thêm 3 dòng dữ liệu cho bảng HOCVIEN (Cấu trúc cuối cùng)
INSERT INTO HOCVIEN (MaHV, HoHV, TenHV, AnhDaiDien, NgaySinh, GioiTinh, Email, DiaChi, MaDoiTuong) VALUES
('65133958', N'Nguyễn Hữu', N'Trọng', 'anh_65133958.jpg', '2005-07-16', 1, 'trong.nh.65133958@ntu-hn.vn', N'Nha Trang, Khánh Hòa', 'DT01'),
('HV002', N'Trần Thị', N'B', NULL, '2003-05-20', 0, 'b.tt@example.com', N'456 Đường B, Hà Nội', 'DT02'),
('HV003', N'Lê Văn', N'C', 'anh_hv003.png', '2003-11-30', 1, 'c.vl@example.com', N'789 Đường C, TP. Hồ Chí Minh', 'DT01');
GO