CREATE DATABASE QuanLyChungCu
GO
USE QuanLyChungCu
GO

---table---
CREATE TABLE CanHo
(
	id INT IDENTITY,
	SoPhong NVARCHAR(100) NOT NULL  PRIMARY KEY,
	stt NVARCHAR(100) NOT NULL DEFAULT N'Trống', 
	ChuSoHuu nvarchar(100) DEFAULT NULL,
	MaHD	NVARCHAR(10), 
	giaTien INT DEFAULT 1000000000,
	tinhTrang NVARCHAR(100) DEFAULT N'Chưa được sử dụng',
    IDKH NVARCHAR(10) DEFAULT NULL
)
GO
CREATE TABLE Account
(
	userName NVARCHAR(100) NOT NULL PRIMARY KEY,
	displayName NVARCHAR(100) NOT NULL,
	pass NVARCHAR(100) NOT NULL,
	type INT NOT NULL,
	chucVu NVARCHAR(100) NOT NULL
)
GO

CREATE TABLE	HoaDon
(
	IDHoaDon NVARCHAR(10) NOT NULL,
	IDHoaDonChiTiet	NVARCHAR(10) NOT NULL ,
	NgayLap	DATE NOT NULL,
	NguoiLap NVARCHAR(100), ---ID account
	TongTien int NOT NULL
	PRIMARY KEY(IDHoaDonChiTiet , IDHoaDon)
)
GO

-----Hoa Don Chi Tiet------
CREATE TABLE HoaDonChiTiet
(
	IDHoaDonChiTiet NVARCHAR(10) NOT NULL,---IDHoaDonChiTiet
	NgayLap date NOT NULL,
	IDDichVu	NVARCHAR(10) NOT NULL,
	DonGia	INT NOT NULL,
	SoLuong INT NOT NULL,
	ThanhTien INT NOT NULL,
	PRIMARY KEY(IDHoaDonChiTiet , IDDichVu, NgayLap)
)
GO

CREATE TABLE DichVu --dich vu
(
	IDDichVu NVARCHAR(10) PRIMARY KEY,	
	Name NVARCHAR(30) NOT NULL,
	DonGia INT NOT NULL,
	DonVi NVARCHAR(10)
)
GO

CREATE TABLE ChuCanHo
(
	ID NVARCHAR(10) PRIMARY KEY,
	NgayMuaNha	DATE NOT NULL,
	IDCanHo NVARCHAR(10) NOT NULL,
	CMND NVARCHAR(10) NOT NULL,
	name NVARCHAR(100) NOT NULL,
	GioiTinh NVARCHAR(10) NOT NULL,
	TrangThai NVARCHAR(100) NOT NULL 
)
GO

CREATE TABLE yeucau
(
	id INT IDENTITY NOT NULL PRIMARY KEY,
	fromAC	NVARCHAR(100) NOT NULL,
	toAC NVARCHAR(100) NOT NULL,
	NoiDung NVARCHAR(1000) NOT NULL,
	daXem INT DEFAULT 1
)
GO
---------------------Foreign Key-------------------------
ALTER TABLE dbo.HoaDon ADD FOREIGN KEY(NguoiLap) REFERENCES dbo.Account(userName)
GO

ALTER TABLE dbo.HoaDonChiTiet ADD FOREIGN KEY(IDDichVu) REFERENCES dbo.DichVu(IDDichVu)
GO

ALTER TABLE dbo.yeucau ADD FOREIGN KEY(fromAC) REFERENCES dbo.Account(userName)
GO

ALTER TABLE dbo.yeucau ADD FOREIGN KEY(toAC) REFERENCES dbo.Account(userName)
GO

-----------insert account------------------
INSERT dbo.Account
        ( userName ,
          displayName ,
          pass ,
          type ,
          chucVu
        )
VALUES  ( N'admin' , -- userName - nvarchar(100)
          N'Nguyen Van Khanh' , -- displayName - nvarchar(100)
          N'1' , -- passWord - nvarchar(1000)
          1 , -- type - int
          N'Admin'  -- chucVu - nvarchar(100)
        )

GO

INSERT dbo.Account
        ( userName ,
          displayName ,
          pass ,
          type ,
          chucVu
        )
VALUES  ( N'demo' , -- userName - nvarchar(100)
          N'khanh nguyen van' , -- displayName - nvarchar(100)
          N'123' , -- passWord - nvarchar(1000)
          1 , -- type - int
          N'Chủ tịch'  -- chucVu - nvarchar(100)
        )
GO

------Insert Can Ho-----------
---------Tang 1----------------
DECLARE @i INT = 1

WHILE @i <= 6
BEGIN
	INSERT dbo.CanHo
	        ( SoPhong ,
	         -- stt ,
	        --ChuSoHuu ,
	          MaHD 
	        --  giaTien ,
	         -- tinhTrang
	        )
	VALUES  ( N'F10' + CAST(@i AS NVARCHAR(100)) , -- SoPhong - nvarchar(100)
	         -- N'Trống' , -- stt - nvarchar(100)
	         --N'KH00' , -- ChuSoHuu - nvarchar(10)
	          N'HDF10'+ CAST(@i AS NVARCHAR(100))  -- MaHD - nvarchar(10)
	         -- 0 , -- giaTien - int
	         -- N''  -- tinhTrang - nvarchar(100)
	        )
	SET @i = @i + 1
END
GO
-----------tang 2------------
DECLARE @i INT = 1

WHILE @i <= 6
BEGIN
	INSERT dbo.CanHo
	        ( SoPhong ,
	         -- stt ,
	        --ChuSoHuu ,
	          MaHD 
	        --  giaTien ,
	         -- tinhTrang
	        )
	VALUES  ( N'F20' + CAST(@i AS NVARCHAR(100)) , -- SoPhong - nvarchar(100)
	         -- N'Trống' , -- stt - nvarchar(100)
	         --N'KH00' , -- ChuSoHuu - nvarchar(10)
	          N'HDF20'+ CAST(@i AS NVARCHAR(100))  -- MaHD - nvarchar(10)
	         -- 0 , -- giaTien - int
	         -- N''  -- tinhTrang - nvarchar(100)
	        )
	SET @i = @i + 1
END
GO
------------tang 3----------

DECLARE @i INT = 1

WHILE @i <= 6
BEGIN
	INSERT dbo.CanHo
	        ( SoPhong ,
	         -- stt ,
	        --ChuSoHuu ,
	          MaHD 
	        --  giaTien ,
	         -- tinhTrang
	        )
	VALUES  ( N'F30' + CAST(@i AS NVARCHAR(100)) , -- SoPhong - nvarchar(100)
	         -- N'Trống' , -- stt - nvarchar(100)
	         --N'KH00' , -- ChuSoHuu - nvarchar(10)
	          N'HDF30'+ CAST(@i AS NVARCHAR(100))  -- MaHD - nvarchar(10)
	         -- 0 , -- giaTien - int
	         -- N''  -- tinhTrang - nvarchar(100)
	        )
	SET @i = @i + 1
END
GO
--------tang 4-------------------
DECLARE @i INT = 1

WHILE @i <= 6
BEGIN
	INSERT dbo.CanHo
	        ( SoPhong ,
	         -- stt ,
	        --ChuSoHuu ,
	          MaHD 
	        --  giaTien ,
	         -- tinhTrang
	        )
	VALUES  ( N'F40' + CAST(@i AS NVARCHAR(100)) , -- SoPhong - nvarchar(100)
	         -- N'Trống' , -- stt - nvarchar(100)
	         --N'KH00' , -- ChuSoHuu - nvarchar(10)
	          N'HDF40'+ CAST(@i AS NVARCHAR(100))  -- MaHD - nvarchar(10)
	         -- 0 , -- giaTien - int
	         -- N''  -- tinhTrang - nvarchar(100)
	        )
	SET @i = @i + 1
END
GO
-------------tang 5---------------------
DECLARE @i INT = 1

WHILE @i <= 6
BEGIN
	INSERT dbo.CanHo
	        ( SoPhong ,
	         -- stt ,
	        --ChuSoHuu ,
	          MaHD 
	        --  giaTien ,
	         -- tinhTrang
	        )
	VALUES  ( N'F50' + CAST(@i AS NVARCHAR(100)) , -- SoPhong - nvarchar(100)
	         -- N'Trống' , -- stt - nvarchar(100)
	         --N'KH00' , -- ChuSoHuu - nvarchar(10)
	          N'HDF50'+ CAST(@i AS NVARCHAR(100))  -- MaHD - nvarchar(10)
	         -- 0 , -- giaTien - int
	         -- N''  -- tinhTrang - nvarchar(100)
	        )
	SET @i = @i + 1
END
GO

----tạo proc send message---
CREATE PROC USP_SendMessage
@guiTu NVARCHAR(100),@guiDen NVARCHAR(100), @mes nvarchar(1000)
AS
BEGIN
	INSERT dbo.yeucau
	        (  fromAC, toAC, NoiDung, daXem )
	VALUES  (  
			 
	          @guiTu, -- fromAC - nvarchar(100)
	          @guiDen, -- toAC - int
	          @mes, -- NoiDung - nvarchar(1000)
	          1  -- daXem - int
	          )
END
GO

--EXEC dbo.USP_SendMessage  @guiTu = N'', -- nvarchar(100)
--    @guiDen = N'', -- nvarchar(100)
--    @mes = N'' -- nvarchar(1000)

--GO
--INSERT [dbo].[DichVu] ([IDDichVu], [Name], [DonGia], [DonVi]) VALUES (N'MH001', N'Căn hộ cao cấp', 1000000000, N'Căn       ')
INSERT [dbo].[DichVu] ([IDDichVu], [Name], [DonGia], [DonVi]) VALUES (N'MH001', N'Điện', 5000, N'Chữ       ')
INSERT [dbo].[DichVu] ([IDDichVu], [Name], [DonGia], [DonVi]) VALUES (N'MH002', N'Nước', 20000, N'Khối      ')
INSERT [dbo].[DichVu] ([IDDichVu], [Name], [DonGia], [DonVi]) VALUES (N'MH003', N'Dịch vụ bảo dưỡng', 250000, N'Lần       ')
INSERT [dbo].[DichVu] ([IDDichVu], [Name], [DonGia], [DonVi]) VALUES (N'MH004', N'Giữ xe', 100000, N'Tháng     ')
INSERT [dbo].[DichVu] ([IDDichVu], [Name], [DonGia], [DonVi]) VALUES (N'MH005', N'Rửa xe', 50000, N'Lần       ')
INSERT [dbo].[DichVu] ([IDDichVu], [Name], [DonGia], [DonVi]) VALUES (N'MH006', N'Camera an ninh', 20000, N'Tháng     ')
INSERT [dbo].[DichVu] ([IDDichVu], [Name], [DonGia], [DonVi]) VALUES (N'MH007', N'Rác thải', 20000, N'Tháng     ')
GO


-------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROC USP_updateChuCanHo
@id NVARCHAR(10), @NgayMuaNha NVARCHAR(100), @CMND NVARCHAR(10), @gioiTinh NVARCHAR(10), @ten NVARCHAR(100), @tt NVARCHAR(100)
AS 
BEGIN
	UPDATE	dbo.ChuCanHo
	SET NgayMuaNha = CONVERT(DATETIME, @NgayMuaNha), CMND = @CMND, GioiTinh = @gioiTinh, name = @ten, TrangThai = @tt
	WHERE ID = @id
END
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROC USP_InsertKH
@ID NVARCHAR(10), @NgayMuaNha NVARCHAR(100), @IDCanHo NVARCHAR(10), @CMND NVARCHAR(10), @name NVARCHAR(100), @GioiTinh NVARCHAR(10), @TrangThai NVARCHAR(100)
AS
BEGIN
	INSERT dbo.ChuCanHo
	        ( ID ,
	          NgayMuaNha ,
	          IDCanHo ,
	          CMND ,
	          name ,
	          GioiTinh ,
	          TrangThai
	        )
	VALUES  ( @ID , -- ID - nvarchar(10)
	          CONVERT(DATETIME, @NgayMuaNha) , -- NgayMuaNha - date
	          @IDCanHo , -- IDCanHo - nvarchar(10)
	          @CMND , -- CMND - nvarchar(10)
	          @name , -- name - nvarchar(100)
	          @GioiTinh , -- GioiTinh - nvarchar(10)
	          @TrangThai  -- TrangThai - nvarchar(100)
	        )
END
GO
 -----------------------------------------------------------------------------------------------------------------------------------------------------------------
 CREATE PROC USP_InsertHoaDon
 @idHoaDon NVARCHAR(10), @idHDCT NVARCHAR(10), @idDV NVARCHAR(10), @sl INT,  @donGia INT, @NgayLap NVARCHAR(50), @NguoiLap NVARCHAR(100)
 AS
 BEGIN
	 DECLARE @dem INT 
     SET @dem = (SELECT COUNT(*) FROM dbo.HoaDonChiTiet WHERE IDHoaDonChiTiet = @idHDCT AND IDDichVu = @idDV AND NgayLap =CONVERT(DATETIME, @NgayLap))
	 ------Kiểm trả xem hóa đơn chi tiết có tồn tại trong bảng ko
	 IF(@dem = 1) ---có tồn tại trong dữ liệu	 
	  UPDATE dbo.HoaDonChiTiet SET SoLuong = SoLuong + @sl, ThanhTien = ThanhTien + DonGia * @sl	 
	 ELSE 
	 INSERT dbo.HoaDonChiTiet
	        ( IDHoaDonChiTiet ,
	          NgayLap ,
	          IDDichVu ,
	          DonGia ,
	          SoLuong ,
	          ThanhTien
	        )
	  VALUES  ( @idHDCT , -- IDHoaDonChiTiet - nvarchar(10)
	           CONVERT(DATETIME, @NgayLap) , -- NgayLap - date
	          @idDV , -- IDDichVu - nvarchar(10)
	          @donGia , -- DonGia - int
	          @sl , -- SoLuong - int
	          @sl * @donGia -- ThanhTien - int
	        )


	 DECLARE @dasd INT
	 SET @dasd = (SELECT COUNT(*) FROM dbo.HoaDon WHERE IDHoaDon = @idHoaDon AND IDHoaDonChiTiet = @idHDCT)
	 ---kiểm tra loại hóa đơn đã xuất hiện chưa
	 IF(@dasd = 1) --co trong bang thi update
	 UPDATE dbo.HoaDon SET TongTien = TongTien +  (@sl * @donGia) WHERE IDHoaDon = @idHoaDon AND IDHoaDonChiTiet = @idHDCT
	 ELSE ---chua co thi tao moi
	 INSERT dbo.HoaDon
	         ( IDHoaDon ,
	           NgayLap ,
	           IDHoaDonChiTiet ,
	           NguoiLap ,
	           TongTien
	         )
	 VALUES  ( @idHoaDon , -- IDHoaDon - nvarchar(10)
	           CONVERT(DATETIME, @NgayLap) , -- NgayLap - date
	           @idHDCT , -- IDHoaDonChiTiet - nvarchar(10)
	           @NguoiLap , -- NguoiLap - int
	           @sl * @donGia  -- TongTien - int
	         )
 END
GO

 ----------------------------------------------lay data theo thang--------------------
  CREATE PROC USP_ThongKeThuNhapCanHo
  AS
  BEGIN
	select p.IDHoaDon as 'Mã phòng', sum(p.TongTien) as 'Tổng Doanh thu'
	from dbo.HoaDon p
	group by P.IDHoaDon
	ORDER BY p.IDHoaDon ASC

  END
GO
  ---------------------------insert account------------------------------------
  CREATE PROC USP_InsertAccount
@userName NVARCHAR(100), @dpName NVARCHAR(100), @pass NVARCHAR(100), @loai INT, @chuc NVARCHAR(100)
AS
BEGIN
	INSERT dbo.Account
	        ( userName ,
	          displayName ,
	          pass ,
	          type ,
	          chucVu
	        )
	VALUES  ( @userName , -- userName - nvarchar(100)
	          @dpName , -- displayName - nvarchar(100)
	          @pass , -- passWord - nvarchar(1000)
	          @loai , -- type - int
	          @chuc  -- chucVu - nvarchar(100)
	        )
END

GO

----------------------------Update------------------------------------------------
CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @dpName NVARCHAR(100), @pass NVARCHAR(100), @loai INT, @chuc NVARCHAR(100)
AS
BEGIN
	UPDATE dbo.Account SET displayName = @dpName, pass = @pass, type = @loai, chucVu = @chuc WHERE userName = @userName	        
END
GO

-------------------Thêm dịch vụ-----------------------------
CREATE PROC USP_InsertDV
@id NVARCHAR(10), @name NVARCHAR(30), @gia INT, @dv NVARCHAR(10)
AS
BEGIN
	INSERT dbo.DichVu
	        ( IDDichVu, Name, DonGia, DonVi )
	VALUES  ( @id , -- IDDichVu - nvarchar(10)
	          @name , -- Name - nvarchar(30)
	          @gia , -- DonGia - int
	          @dv  -- DonVi - nvarchar(10)
	          )
END
GO
-------------------sửa dịch vụ-----------------
CREATE PROC USP_UpdateDV
@id NVARCHAR(10), @name NVARCHAR(30), @gia INT, @dv NVARCHAR(10)
AS
BEGIN
	UPDATE dbo.DichVu SET Name = @name, DonGia = @gia, DonVi = @dv WHERE IDDichVu = @id
END
GO
--------------------Delete HDCT---------------------------
CREATE PROC USP_DeleteHDCT
@hd NVARCHAR(10), @idHDCT NVARCHAR(10), @madv NVARCHAR(10), @gia INT
AS
BEGIN
	UPDATE dbo.HoaDon SET TongTien = TongTien -  @gia WHERE IDHoaDon = @hd AND IDHoaDonChiTiet = @idHDCT
	DELETE FROM dbo.HoaDonChiTiet WHERE IDHoaDonChiTiet = @idHDCT AND IDDichVu = @madv
END
GO
-------------------------------------------------



