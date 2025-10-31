CREATE DATABASE KT0720_65133958;
GO

USE KT0720_65133958;
GO

CREATE TABLE LOP (
    MaLop NVARCHAR(15) PRIMARY KEY, 
    TenLop NVARCHAR(100) NOT NULL   
);
GO

CREATE TABLE SINHVIEN (
    MaSV NVARCHAR(8) PRIMARY KEY,       
    HoSV NVARCHAR(50) NOT NULL,
    TenSV NVARCHAR(10) NOT NULL,
    NgaySinh DATE,                      
    GioiTinh BIT,                        
    AnhSV NVARCHAR(100),                 
    DiaChi NVARCHAR(100),
    MaLop NVARCHAR(15),                 

    CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
);
GO

INSERT INTO LOP (MaLop, TenLop) VALUES
(N'65.CNTT-1', N'Công nghệ thông tin 1 - K65'),
(N'65.CNTT-2', N'Công nghệ thông tin 2 - K65'),
(N'64.NNANH', N'Ngôn ngữ Anh - K64');
GO

INSERT INTO SINHVIEN (MaSV, HoSV, TenSV, NgaySinh, GioiTinh, AnhSV, DiaChi, MaLop) VALUES
(N'65133958', N'Nguyễn Hữu', N'Trọng', '07/16/2005', 1, N'65133958.png', N'Nha Trang, Khánh Hòa', N'65.CNTT-1'),
(N'65133001', N'Trần Thị', N'Lan', '2003-08-20', 0, N'65133001.png', N'Cam Ranh, Khánh Hòa', N'65.CNTT-1'),
(N'64123456', N'Lê Văn', N'Minh', '2002-01-15', 1, N'64123456.png', N'Hà Nội', N'64.NNANH');
GO

CREATE PROCEDURE sp_TimKiemSinhVien
    @MaSV NVARCHAR(8) = NULL,
    @HoTen NVARCHAR(60) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Result TABLE (
        MaSV NVARCHAR(8),
        HoTen NVARCHAR(60),
        NgaySinh DATE,
        GioiTinh NVARCHAR(10),
        DiaChi NVARCHAR(100),
        TenLop NVARCHAR(100)
    );

    INSERT INTO @Result
    SELECT 
        SV.MaSV,
        SV.HoSV + N' ' + SV.TenSV AS HoTen,
        SV.NgaySinh,
        CASE WHEN SV.GioiTinh = 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
        SV.DiaChi,
        L.TenLop
    FROM SINHVIEN SV
    INNER JOIN LOP L ON SV.MaLop = L.MaLop
    WHERE 
        (@MaSV IS NULL OR SV.MaSV = @MaSV)
        AND
        (@HoTen IS NULL OR (SV.HoSV + N' ' + SV.TenSV) LIKE N'%' + @HoTen + N'%');

    IF EXISTS (SELECT 1 FROM @Result)
        SELECT * FROM @Result;
    ELSE
        SELECT N'Không có thông tin cần tìm' AS ThongBao;
END;
GO