-- 1. Tạo Cơ sở dữ liệu
CREATE DATABASE KT0720_65133958;
GO

-- 2. Sử dụng CSDL vừa tạo
USE KT0720_65133958;
GO

-- 3. Tạo bảng LỚP
CREATE TABLE LOP (
    MaLop NVARCHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(100) NOT NULL
);
GO

-- 4. Tạo bảng SINH VIÊN
CREATE TABLE SINHVIEN (
    MaSV NVARCHAR(10) PRIMARY KEY,
    HoSV NVARCHAR(50) NOT NULL,
    TenSV NVARCHAR(50) NOT NULL,
    NgaySinh DATE,
    GioiTinh BIT, -- 1 (True) là Nam, 0 (False) là Nữ
    AnhSV NVARCHAR(255) NULL,
    DiaChi NVARCHAR(255),
    MaLop NVARCHAR(10),
    
    -- Tạo khóa ngoại liên kết với bảng LỚP
    CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
);
GO

-- Sử dụng CSDL của bạn (nếu bạn chưa ở trong CSDL đó)
USE KT0720_65133958;
GO

-- Thêm 3 dòng dữ liệu cho bảng LỚP
INSERT INTO LOP (MaLop, TenLop) VALUES
(N'L01', N'Công nghệ thông tin K65'),
(N'L02', N'Kỹ thuật phần mềm K65'),
(N'L03', N'An toàn thông tin K66');
GO

-- Thêm 3 dòng dữ liệu cho bảng SINHVIEN
-- (Sử dụng mã SV của bạn cho 1 dòng)
INSERT INTO SINHVIEN (MaSV, HoSV, TenSV, NgaySinh, GioiTinh, AnhSV, DiaChi, MaLop) VALUES
(N'65133958', N'Nguyễn', N'Văn A', '2004-10-20', 1, NULL, N'Đà Nẵng', N'L01'),
(N'SV002', N'Trần', N'Thị B', '2004-05-15', 0, NULL, N'Quảng Nam', N'L01'),
(N'SV003', N'Lê', N'Văn C', '2003-11-02', 1, NULL, N'Hà Nội', N'L02');
GO