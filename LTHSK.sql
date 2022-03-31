
--1 Tạo bảng tblNhomHang
create table tblNhomHang
(
	PK_MaNhomHang char(10) primary key,
	sTenNhomHang nvarchar(50)
)
--2 Tạo bảng tblHangHoa
create table tblHangHoa
(
	PK_MaHang char(10) primary key,
	sTenHang nvarchar(50),
	sMauSac nvarchar(50),
	iSoLuong int,
	fGiaBan float,
	sHangSX nvarchar(100),
	sMaNhomHang char(10)
)
--3 Tạo bảng tblHangBan
create table tblHangBan
(
	PK_MaHoaDon char(10)   NOT NULL  ,
	sMaHang char(10)  NOT NULL,
	iSoLuong int,
	fGiaBan float,
	dTGMua date,
	dTGBaoHanh date 
	CONSTRAINT pk_hb PRIMARY KEY(PK_MaHoaDon, sMaHang) 
)

DROP TABLE dbo.tblHangBan
ALTER TABLE dbo.tblHangBan 
ADD CONSTRAINT checksl CHECK (dTGBaoHanh >= dTGMua)

--4 Tạo bảng tblThietBiCanBaoHanh
create table tblThietBiCanBaoHanh
(
	PK_MaBH CHAR(10) NOT NULL PRIMARY KEY,
	sMaThietBi char(10),
	sTenThietBi nvarchar(50),
	sTinhTrang nvarchar(500),
	sNguyenNhan nvarchar (500),
	dTGNhanBH DATE,
	dBHDen DATE
)
SELECT * FROM dbo.tblThietBiCanBaoHanh
ALTER TABLE dbo.tblThietBiCanBaoHanh
ADD CONSTRAINT checkdatebh CHECK (dBHDen > dTGNhanBH)
DROP TABLE dbo.tblThietBiCanBaoHanh
--5 Tạo bảng tblNhatKiSuaChua
create table tblNhatKiSuaChua
(
	PK_MaSC CHAR(10) PRIMARY KEY NOT NULL,
	sMaHang char(10),
	sNguyenNhan nvarchar(500),
	sGiaiQuyet nvarchar(500),
	sMaNVSua char(10),
	sTGSua date,
	sTGTra date
)

ALTER TABLE dbo.tblNhatKiSuaChua
ADD CONSTRAINT chtg CHECK (sTGTra >= sTGSua)
--6 Tạo bảng tblPhieuNhan
create table tblPhieuNhan
(
	PK_MaPhieu char(10) primary key,
	sMaKH char(10),
	sTenKH nvarchar(50),
	sDiaChi nvarchar(50),
	sMaNVNhanSua char(10),
	dNgayNhanSua date,
	dTGTra date
)
ALTER TABLE dbo.tblPhieuNhan
ADD CONSTRAINT chtgtra CHECK (dNgayNhanSua <= dTGTra)
--7 Tạo bảng tblNhanVien
create table tblNhanVien
(
	PK_MaNV char(10) primary key,
	sTenNV nvarchar(50),
	bGioiTinh NVARCHAR(10),
	sDiaChi nvarchar(50),
	dNgaySinh date,
	fHSL float,
	sTenDangNhap char(50),
	sMatKhau char(50),
)

ALTER TABLE dbo.tblNhanVien
ADD CONSTRAINT chns CHECK (YEAR(dNgaySinh) <= YEAR(GETDATE())-18)
--8 Tạo bảng tblKhachHang
create table tblKhachHang
(
	PK_MaKH char(10) primary key,
	sTenKH nvarchar(50),
	sGioiTinh NVARCHAR(10),
	dNgaySinh DATE,
	sDiaChi nvarchar(50),
	sSDT char(10)
)


--9 Tạo bảng tblHoaDonBanHang
create table tblHoaDonBanHang
(
	PK_MaHoaDon char(10) primary key,
	sMaNVLap char(10),
	sMaKH char(10),
	dTGLap date,
	fTongTien float,
	bTTThuTien bit
)

ALTER TABLE dbo.tblHoaDonBanHang
ADD CONSTRAINT tthutien CHECK (bTTThuTien = N'Rồi' OR bTTThuTien = N'Chưa')
ALTER TABLE dbo.tblHoaDonBanHang
ADD CONSTRAINT chTGL CHECK(dTGLap <= GETDATE())
-- TẠO KHÓA NGOẠI CHO CÁC BẢNG
alter table tblHangHoa
add constraint fk_NhomHang foreign key (sMaNhomHang) references tblNhomHang(PK_MaNhomHang)

alter table tblThietBiCanBaoHanh
add constraint fk_TBBH foreign key (sMaThietBi) references tblHangHoa (PK_MaHang)

alter table tblNhatKiSuaChua
add constraint fk_nksc foreign key (sMaHang) references tblHangHoa(PK_MaHang)
alter table tblNhatKiSuaChua
add constraint fk_nvsc foreign key (sMaNVSua) references tblNhanVien(PK_MaNV)

alter table tblHangBan
add constraint fk_hhb foreign key (PK_MaHoaDon) references tblHoaDonBanHang(PK_MaHoaDon),
	constraint fk_maHangban foreign key (sMaHang) references tblHangHoa(PK_MaHang)

alter table tblHoaDonBanHang
add constraint fk_nvbh foreign key (sMaNVLap) references tblNhanVien(PK_MaNV),
	constraint fk_khmh foreign key (sMaKH) references tblKhachHang(PK_MaKH)

alter table tblPhieuNhan
add constraint fk_nvnh foreign key (sMaNVNhanSua) references tblNhanVien(PK_MaNV),
	constraint fk_khsh foreign key (sMaKH) references tblKhachHang(PK_MaKH)
alter table tblNhanVien
add constraint nvgt check (bGioitinh = N'Nam' or bGioitinh = N'Nữ')
alter table tblKhachHang
add constraint khgt check (sGioitinh = N'Nam' or sGioitinh = N'Nữ')

	--Dữ liệu bảng Nhóm Hàng

insert into tblNhomHang values ('NH01', N'Ổ cứng')
insert into tblNhomHang values ('NH02', N'RAM')
insert into tblNhomHang values ('NH03', N'Main')
insert into tblNhomHang values ('NH05', N'CPU')
insert into tblNhomHang values ('NH07', N'Monitor')

 --Dữ liệu bảng Hàng hóa

insert into tblHangHoa 
values('HH01', 'HDD WD Blue 1TB', N'Trắng', 15, 950000, N'Western Digital','NH01')
insert into tblHangHoa 
values('HH02', 'HDD Seagate Barracuda 1TB', N'Trắng xanh', 17, 980000, N'Seagate','NH01')
insert into tblHangHoa 
values('HH03', 'SSD KINGSTON A400 240GB M.2 Sata 3', N'Xanh đen', 21, 890000, N'KINGSTON','NH01')
insert into tblHangHoa 
values('HH04', 'Gigabyte SSD 120GB Sata III', N'Đen', 24, 590000, N'Gigabyte','NH01')

insert into tblHangHoa 
values('HH05', 'G.SKILL Ripjaws V 8GB', N'Đỏ đen', 18, 1090000, N'GSKILL','NH02')
insert into tblHangHoa 
values('HH06', 'PNY XLR8 RGB 8GB', N'Đen', 22, 1990000, N'PNY','NH02')
insert into tblHangHoa 
values('HH07', 'Adata XPG Lancer RGB', N'Đen', 11, 4990000, N'Adata','NH02')

insert into tblHangHoa 
values('HH08', 'Mainboard Asrock B660M Steel Legend DDR4', N'Đen trắng', 7, 4190000, N'Asrock ','NH03')
insert into tblHangHoa 
values('HH09', 'Mainboard MSI MAG B560M Mortar', N'Đen', 5, 3590000, N'MSI','NH03')
insert into tblHangHoa 
values('HH10', 'Mainboard GIGABYTE Z590 UD', N'Đen', 12, 4590000, N'GIGABYTE','NH03')

insert into tblHangHoa 
values('HH15', 'Intel Core i9-10900K', N'Lam', 13, 9990000, N'Intel','NH05')
insert into tblHangHoa 
values('HH13', 'AMD Ryzen 7 3700X', N'Xám', 16, 8790000, N'AMD','NH05')
insert into tblHangHoa 
values('HH14', 'AMD Athlon 3000G', N'Trắng', 17, 1490000, N'AMD','NH05')

insert into tblHangHoa 
values('HH11', 'Dell 18.5 inch E1920H', N'Đen', 11, 2690000, N'Dell','NH07')
insert into tblHangHoa 
values('HH12', 'HP V19 18.5 inch 9TN41AA', N'Trắng', 14, 2510000, N'HP','NH07')
insert into tblHangHoa 
values('HH19', 'Samsung 27 inch LC27F397FHEXXV', N'Trắng', 14, 4390000, N'Samsung','NH07')

insert into tblNhanVien
values ('NV01', N'Nguyễn Công Phượng', N'Nam', N'Nam Đàn, Nghệ An','12/02/1995', 4.3, 'congphuong10', 'cp10')
insert into tblNhanVien
values ('NV02', N'Nguyễn Tiến Linh', N'Nam', N'Thủ Dầu Một, Bình Dương', '11/05/1996', 4.1, 'tienlinh22', 'tl22')
insert into tblNhanVien
values ('NV03', N'Thái Thị Thảo', N'Nữ', N'Nghi Sơn, Hà Tĩnh','07/22/1998', 2.6, 'thaithao25', 'tt25')
insert into tblNhanVien
values ('NV05', N'Phạm Hải Yến', N'Nữ', N'Tam Đảo, Vĩnh Phúc','05/06/1998', 2.9, 'haiyen7', 'hy7')

insert into tblNhatKiSuaChua
values ('NKSC01','HH11',	N'Lỗi màu màn hình', N'Thay giắc màu', 'NV02', '02/11/2020', '02/20/2020' )
insert into tblNhatKiSuaChua
values ('NKSC03','HH06',	N'Lỗi RAM do sự cố', N'Thay RAM mới', 'NV03', '11/11/2020', '11/15/2020' )
insert into tblNhatKiSuaChua
values ('NKSC02','HH02',	N'Ổ cứng ngấm nước', N'Thay ổ cứng', 'NV01', '07/03/2021', '07/05/2021' )

INSERT INTO tblKhachHang VALUES 
('KH01', N'Nguyễn Văn Toàn',N'Nam', '05/15/1995', N'Thanh Hà, Hải Dương', '0987789987')
INSERT INTO tblKhachHang VALUES 
('KH03', N'Hoàng Thị Loan',N'Nữ','02/22/1998', N'Thanh Sơn, Phú Thọ', '0329756734' )
INSERT INTO tblKhachHang VALUES 
('KH02', N'Trần Tuyết Dung',N'Nữ',  '11/20/1996', N'Đông Anh, Hà Nội', '0965743821')
INSERT INTO tblKhachHang VALUES 
('KH06', N'Phạm Văn Mạnh',N'Nam','01/15/2000', N'Vĩnh Tường, Vĩnh Phúc', '0399754251' )


INSERT INTO tblHoaDonBanHang VALUES
('HD01', 'NV01', 'KH02', '02/10/2021',0, N'Rồi'),
('HD02', 'NV01', 'KH03', '11/20/2020', 0, N'Chưa'),
('HD03', 'NV03', 'KH06', '03/21/2021', 0, N'Rồi')

INSERT INTO dbo.tblThietBiCanBaoHanh
(
	PK_MaBH,
    sMaThietBi,
 
    sTinhTrang,
    sNguyenNhan,
    dTGNhanBH,
    dBHDen
)
VALUES
(   'BH01',
	'HH01',        -- sMaThietBi - char(10)
           -- sTenThietBi - nvarchar(50)
    N'Hư hỏng ô cứng',       -- sTinhTrang - nvarchar(500)
    N'Tác động vật lí bên ngoài',       -- sNguyenNhan - nvarchar(500)
    '02/10/2019', -- dTGNhanBH - date
    '02/10/2020'  -- dBHDen - date
    )
INSERT INTO dbo.tblPhieuNhan
(
    PK_MaPhieu,
    sMaKH,
    sTenKH,
    sDiaChi,
    sMaNVNhanSua,
    dNgayNhanSua,
    dTGTra
)
VALUES
(   'PN01',        -- PK_MaPhieu - char(10)
    'KH02',        -- sMaKH - char(10)
    N'Trần Tuyết Dung',       -- sTenKH - nvarchar(50)
    N'Đông Anh, Hà Nội',       -- sDiaChi - nvarchar(50)
    'NV03',        -- sMaNVNhanSua - char(10)
    '11/20/2019', -- dNgayNhanSua - date
    '11/25/2019'  -- dTGTra - date
    )
	SELECT * FROM dbo.tblThietBiCanBaoHanh
	go
---XỬ LÝ VỚI BẢNG HÀNG HÓA
CREATE PROC hienHH
AS BEGIN
       SELECT * FROM dbo.tblHangHoa
   END
   GO
ALTER TABLE dbo.tblHangHoa ADD sTenNhomHang NVARCHAR(50)
UPDATE dbo.tblHangHoa SET sTenNhomHang = dbo.tblNhomHang.sTenNhomHang FROM dbo.tblNhomHang
		WHERE dbo.tblNhomHang.PK_MaNhomHang = dbo.tblHangHoa.sMaNhomHang
GO 

    
create proc addDSHH
@mahang char(10), @tenhang nvarchar(50), @mausac nvarchar(50), @soluong int, @giaban float,
@hsx nvarchar(50), @tennhom NVARCHAR(50)
as begin
		insert into tblHangHoa ([PK_MaHang],[sTenHang],[sMauSac],[iSoLuong],[fGiaBan],[sHangSX],sTenNhomHang)
		values (@mahang, @tenhang, @mausac, @soluong, @giaban, @hsx, @tennhom)  
	end
	GO


CREATE PROC delHH 
@mahang CHAR(10)
AS BEGIN
       DELETE FROM dbo.tblHangHoa WHERE PK_MaHang = @mahang
   END
    DELETE FROM dbo.tblHangHoa WHERE PK_MaHang = 'HH01'
GO 

ALTER PROC updateHH
@mahang char(10), @tenhang nvarchar(50), @mausac nvarchar(50), @soluong int, @giaban float,
@hsx nvarchar(50), @tennhom NVARCHAR(50)
AS BEGIN
       UPDATE dbo.tblHangHoa SET  sTenHang = @tenhang, sMauSac = @mausac, iSoLuong = @soluong, fGiaBan = @giaban, sHangSX= @hsx, sTenNhomHang = @tennhom
				WHERE PK_MaHang = @mahang
   END
   GO
   

   GO 
	--XỬ LÝ DỮ LIỆU BẢNG HÀNG BÁN
ALTER PROC hienHB
AS BEGIN
       SELECT * FROM dbo.tblHangBan
   END
   GO
			

   --Thêm hàng bán
ALTER PROC addHB
@mahd CHAR(10), @mahang CHAR(10), @sl int, @giaban float, @tgmua date, @tgbh date
AS BEGIN
       INSERT INTO tblHangBan(PK_MaHoaDon, sMaHang, iSoLuong, fGiaBan, dTGMua, dTGBaoHanh )
							VALUES (@mahd, @mahang, @sl, @giaban, @tgmua, @tgbh)	
   END
   GO 
   EXEC addHB 'HD01', 'HH02', 15, 900000,  '01/01/2020','01/01/2023'
   EXEC addHB 'HD01', 'HH04', 4, 400000, '01/01/2021', '01/01/2024'
   EXEC addHB 'HD02', 'HH02', 2, 900000, '01/05/2019', '01/11/2020'

 SELECT * FROM dbo.tblHangHoa
GO
	--Sửa hàng bán
CREATE  PROC updateHB
@mahd CHAR(10), @mahang CHAR(10), @sl int, @giaban float, @tgmua date, @tgbh date
AS BEGIN
       UPDATE dbo.tblHangBan SET iSoLuong = @sl, fGiaBan = @giaban, dTGMua = @tgmua, dTGBaoHanh = @tgbh
	   WHERE PK_MaHoaDon = @mahd AND sMaHang = @mahang
   END
   
   GO 
   EXEC dbo.updateHB @mahd = 'HD01',  -- char(10)
                     @mahang = 'HH02', -- char(10)
					 @sl = 50,
					 @giaban = 5000000,
					 @tgmua = '10/20/2019',
					 @tgbh = '10/20/2023'
   GO 
   --Xóa mặt hàng bán ra
CREATE PROC delHB
@mahd CHAR(10), @mahang CHAR(10)
AS BEGIN
       DELETE FROM dbo.tblHangBan WHERE PK_MaHoaDon = @mahd AND sMaHang = @mahang
   END
   SELECT * FROM dbo.tblHangBan
   EXEC dbo.delHB @mahd = 'HD01',  -- char(10)
                  @mahang = 'HH04' -- char(10)
   GO 

	--Tìm kiếm hàng bán ra
ALTER PROC searchHB
@mahd CHAR(10), @mahang CHAR(10), @sl int, @giaban FLOAT
AS BEGIN
       SELECT * FROM dbo.tblHangBan WHERE PK_MaHoaDon = @mahd or sMaHang = @mahang 
										or iSoLuong = @sl or fGiaBan = @giaban 
   END
   EXEC dbo.searchHB @mahd = 'HD01',  -- char(10)
                     @mahang = '', -- char(10)
					 @sl = 0,
					 @giaban = 0,
					 @tgmua = '',
					 @tgbh = ''
   
   
   GO 
   

		--Xử lí sl hàng hóa giảm đi khi có đơn hàng
CREATE TRIGGER trg_dathang
ON dbo.tblHangBan AFTER INSERT 
AS BEGIN
       UPDATE dbo.tblHangHoa
	   SET dbo.tblHangHoa.iSoLuong = dbo.tblHangHoa.iSoLuong - 
					(SELECT Inserted.iSoLuong FROM Inserted
					WHERE Inserted.sMaHang = dbo.tblHangHoa.PK_MaHang )
		FROM dbo.tblHangHoa JOIN Inserted ON Inserted.sMaHang = tblHangHoa.PK_MaHang
   END
   GO
		-- Xử lí hàng hóa sau khi cập nhật hàng bán
CREATE TRIGGER trg_CapNhatDatHang on dbo.tblHangBan after update AS
	BEGIN
	   UPDATE dbo.tblHangHoa SET dbo.tblHangHoa.iSoLuong = dbo.tblHangHoa.iSoLuong -
		   (SELECT Inserted.iSoLuong FROM Inserted
					WHERE Inserted.sMaHang = dbo.tblHangHoa.PK_MaHang) +
		   (SELECT Deleted.iSoLuong FROM Deleted
					WHERE Deleted.sMaHang = dbo.tblHangHoa.PK_MaHang )
	   FROM dbo.tblHangHoa 
	   JOIN Deleted ON Deleted.sMaHang = tblHangHoa.PK_MaHang
	END
    GO 
	UPDATE dbo.tblHangBan SET iSoLuong = 12 WHERE sMaHang = 'HH02'
	GO 
		--Xử lí hàng hóa tăng lên khi hóa đơn bị xóa
create TRIGGER trg_HuyDatHang ON dbo.tblHangBan FOR DELETE 
AS 
	BEGIN
		UPDATE dbo.tblHangHoa
		SET dbo.tblHangHoa.iSoLuong = dbo.tblHangHoa.iSoLuong + (SELECT Deleted.iSoLuong FROM Deleted WHERE Deleted.sMaHang = dbo.tblHangHoa.PK_MaHang)
		FROM dbo.tblHangHoa 
		JOIN deleted ON Deleted.sMaHang = tblHangHoa.PK_MaHang
	END
	GO
    
		-- Xử lí hàng bán ra phải ít hơn hàng trong kho
CREATE TRIGGER checkhb
ON dbo.tblHangBan FOR INSERT, UPDATE
AS 
      iF (SELECT dbo.tblHangHoa.iSoLuong FROM dbo.tblHangHoa  JOIN Inserted ON tblHangHoa.PK_MaHang = Inserted.sMaHang  )
	  <
	  (SELECT dbo.tblHangBan.iSoLuong FROM dbo.tblHangBan  JOIN Inserted ON tblHangBan.sMaHang = Inserted.sMaHang  )
	   AND
       NOT EXISTS (SELECT dbo.tblHangHoa.PK_MaHang FROM dbo.tblHangHoa JOIN Inserted 
				on dbo.tblHangHoa.PK_MaHang = Inserted.sMaHang AND dbo.tblHangHoa.iSoLuong >0)
	  BEGIN
	      PRINT N'Số lượng hàng bán ra phải ít hơn số hàng hiện có'
		  ROLLBACK TRAN
	  END
   GO
   INSERT INTO dbo.tblHangBan
   (
       PK_MaHoaDon,
       sMaHang,
       iSoLuong,
       fGiaBan,
       dTGMua,
       dTGBaoHanh
   )
   VALUES
   (   'HD03',        -- PK_MaHoaDon - char(10)
       'HH09',        -- sMaHang - char(10)
       6,         -- iSoLuong - int
       30000000,       -- fGiaBan - float
       '03/10/2019', -- dTGMua - date
       '10/10/2021'  -- dTGBaoHanh - date
       )

	   SELECT * FROM dbo.tblHangBan
GO 
CREATE VIEW tkhb
AS SELECT dbo.tblHangBan.PK_MaHoaDon, dbo.tblHangHoa.sTenHang, dbo.tblHangBan.iSoLuong, dbo.tblHangBan.fGiaBan,
		dbo.tblKhachHang.sTenKH,dbo.tblKhachHang.sDiaChi, dbo.tblKhachHang.sSDT ,dbo.tblHoaDonBanHang.dTGLap, dbo.tblNhanVien.sTenNV
		FROM dbo.tblHoaDonBanHang, dbo.tblHangHoa, dbo.tblHangBan , dbo.tblKhachHang, dbo.tblNhanVien
		WHERE dbo.tblHangBan.PK_MaHoaDon = dbo.tblHoaDonBanHang.PK_MaHoaDon 
				AND dbo.tblHangBan.sMaHang = dbo.tblHangHoa.PK_MaHang
				AND dbo.tblHoaDonBanHang.sMaKH = dbo.tblKhachHang.PK_MaKH
				AND dbo.tblHoaDonBanHang.sMaNVLap = dbo.tblNhanVien.PK_MaNV
	GO 
	CREATE PROC rpHB
	 @mahd CHAR(10)
	AS BEGIN
	       SELECT * FROM tkhb WHERE PK_MaHoaDon = @mahd
	   END
			----XỬ LÝ DỮ LIỆU BẢNG THIẾT BỊ BẢO HÀNH
ALTER PROC hienTBBH
AS BEGIN
       SELECT * FROM dbo.tblThietBiCanBaoHanh
   END
   GO
				--Xử lí phù hợp giao diện
				ALTER TABLE dbo.tblThietBiCanBaoHanh ADD sTenhang NVARCHAR(50)
				UPDATE dbo.tblThietBiCanBaoHanh SET sTenhang = dbo.tblHangHoa.sTenHang FROM dbo.tblHangHoa
							WHERE dbo.tblHangHoa.PK_MaHang = dbo.tblThietBiCanBaoHanh.sMaThietBi
							GO
							--view Hiển thị bảo hành theo ngày
CREATE VIEW TBBHTheoNgay
AS SELECT PK_MaBH, dbo.tblHangHoa.sTenHang, sTinhTrang, sNguyenNhan, dTGNhanBH, dBHDen 
	   FROM dbo.tblThietBiCanBaoHanh, dbo.tblHangHoa
	   WHERE dbo.tblHangHoa.PK_MaHang = dbo.tblThietBiCanBaoHanh.sMaThietBi

	GO 
	SELECT * FROM dbo.TBBHTheoNgay
GO 
CREATE PROC BHTheoTg
@tgbd date, @tgkt date
AS BEGIN
       SELECT * FROM dbo.TBBHTheoNgay WHERE dTGNhanBH >= @tgbd AND dTGNhanBH <= @tgkt
   END
   GO
   EXEC dbo.BHTheoTg @tgbd = '2019-02-01', -- date
                     @tgkt = '2019-03-14'  -- date
   GO
   ALTER PROC TBBHTheoTen
   @ma CHAR(10)
   AS BEGIN
          SELECT * FROM dbo.tblThietBiCanBaoHanh WHERE sMaThietBi = @ma
      END
   --Thêm TBBH
ALTER PROC addTBBH
@mabh CHAR(10), @tentb nvarchar(50) , @tinhtrang NVARCHAR(500), @nguyennhan NVARCHAR(500),
@tgbh DATE, @tghetbh DATE
AS BEGIN
       INSERT INTO dbo.tblThietBiCanBaoHanh
       (
			PK_MaBH,
           sTenhang,
           
           sTinhTrang,
           sNguyenNhan,
           dTGNhanBH,
           dBHDen
       )
       VALUES
       (   
			@mabh,
			@tentb,        -- PK_MaThietBi - char(10)
                 -- sTenThietBi - nvarchar(50)
           @tinhtrang,       -- sTinhTrang - nvarchar(500)
           @nguyennhan,       -- sNguyenNhan - nvarchar(500)
           @tgbh, -- dTGNhanBH - date
           @tghetbh  -- dBHDen - date
           )
   END
  GO 
  --Sửa thiết bị bảo hành
ALTER PROC updateTBBH
@mabh CHAR(10), @tentb NVARCHAR(50), @tinhtrang NVARCHAR(500), @nguyennhan NVARCHAR(500),
@tgbh DATE, @tghetbh DATE
AS BEGIN
       UPDATE dbo.tblThietBiCanBaoHanh SET   sTinhTrang = @tinhtrang,
										sNguyenNhan = @nguyennhan, dTGNhanBH = @tgbh, dBHDen = @tghetbh
		WHERE PK_MaBH = @mabh and sTenhang = @tentb
   END
   GO
   --Xóa thiết bị bảo hành
ALTER PROC delTBBH
@mabh CHAR(10), @tentb NVARCHAR(50)
AS BEGIN
       DELETE FROM dbo.tblThietBiCanBaoHanh where PK_MaBH = @mabh AND sTenhang = @tentb
   END
   GO
ALTER PROC searchTBBH
@mabh CHAR(10), @tentb NVARCHAR(50)
AS BEGIN
       SELECT * FROM dbo.tblThietBiCanBaoHanh WHERE PK_MaBH = @mabh OR  sTenhang = @tentb
   END
   GO
   
   SELECT * FROM dbo.tblHangBan
   SELECT * FROM dbo.tblKhachHang
   SELECT * FROM dbo.tblHoaDonBanHang
   SELECT * FROM dbo.tblNhanVien
   SELECT* FROM dbo.tblThietBiCanBaoHanh
   SELECT * FROM dbo.tblNhatKiSuaChua
   SELECT * FROM dbo.tblHangHoa
   SELECT * FROM dbo.tblPhieuNhan
	
GRANT SELECT,  INSERT, UPDATE, DELETE  ON tblHangBan TO  thekhai
GRANT SELECT,  INSERT, UPDATE, DELETE  ON dbo.tblHangHoa TO  thekhai
GRANT SELECT,  INSERT, UPDATE, DELETE  ON dbo.tblHoaDonBanHang TO  thekhai
GRANT SELECT,  INSERT, UPDATE, DELETE  ON dbo.tblKhachHang TO  thekhai
GRANT SELECT,  INSERT, UPDATE, DELETE  ON dbo.tblNhanVien TO  thekhai
GRANT SELECT,  INSERT, UPDATE, DELETE  ON dbo.tblNhatKiSuaChua TO  thekhai
GRANT SELECT,  INSERT, UPDATE, DELETE  ON dbo.tblThietBiCanBaoHanh TO  thekhai
GRANT SELECT,  INSERT, UPDATE, DELETE  ON dbo.tblNhomHang TO  thekhai
GRANT SELECT,  INSERT, UPDATE, DELETE  ON dbo.tblPhieuNhan TO  thekhai
go
CREATE PROC laydshh
 @mahang CHAR(10)
 AS BEGIN
        SELECT dbo.tblHangHoa.*, dbo.tblNhomHang.sTenNhomHang  FROM dbo.tblHangHoa, dbo.tblNhomHang
		WHERE dbo.tblHangHoa.sMaNhomHang = dbo.tblNhomHang.PK_MaNhomHang
    END
	go
CREATE VIEW layds
AS SELECT *  FROM dbo.tblHangHoa

GO 
 ---thêm nhân viên
   create proc addNV
@manhanvien char(10), @tennhanvien nvarchar(50), @gioitinh nvarchar(10), @diachi nvarchar(50), @ngaysinh date, @hsl float, @tendn char(50), @matkhau char(50)
as begin
		insert into tblNhanVien([PK_MaNV],[sTenNV],[bGioiTinh],[sDiaChi],[dNgaySinh],[fHSL],[sTenDangNhap],[sMatKhau])
		values (@manhanvien, @tennhanvien, @gioitinh, @diachi, @ngaysinh, @hsl,@tendn,@matkhau)  
	end
	go
	---hiện nv
ALTER PROC LoginNV

AS BEGIN
       SELECT * FROM dbo.tblNhanVien
   END
   GO

  
   
   -------update nhân viên
  create PROC updateNV
@manv char(10), @tennhanvien nvarchar(50), @gioitinh nvarchar(10), @diachi nvarchar(50), @ngaysinh date, @hsl float, @tendn char(50), @matkhau char(50)
AS BEGIN
       UPDATE dbo.tblNhanVien   set [PK_MaNV] = @manv,[sTenNV] = @tennhanvien, [bGioiTinh] = @gioitinh, [dNgaySinh]= @ngaysinh, [fHSL] = @hsl,[sDiaChi] = @diachi,[sTenDangNhap] = @tendn,[sMatKhau] = @matkhau
		WHERE PK_MaNV = @manv
   END
   GO

   
   use LTHSK
   GO
   
   ----xóa nhân viên
   CREATE PROC deleteNV
	@manv char(10)
	AS BEGIN
		   DELETE FROM dbo.tblNhanVien where PK_MaNV= @manv 
	   END
	   go
	   ------tìm kiếm nhân viên
	   CREATE PROC searchNV
	@manv char(10), @tennhanvien nvarchar(50)
	AS BEGIN
		SELECT *FROM dbo.tblNhanVien where PK_MaNV = @manv or  sTenNV = @tennhanvien 
		END
		go
 -----------------XỬ LÝ BẢNG KHÁCH HÀNG
 CREATE PROC addKH
 @makh CHAR(10), @hoten NVARCHAR(50), @gt NVARCHAR(10), @ns DATE, @dc NVARCHAR(50), @sdt CHAR(10)
 AS INSERT INTO dbo.tblKhachHang
 (
     PK_MaKH,
     sTenKH,
     sGioiTinh,
     dNgaySinh,
     sDiaChi,
     sSDT
 )
 VALUES
 (   @makh,        -- PK_MaKH - char(10)
     @hoten,       -- sTenKH - nvarchar(50)
     @gt,       -- sGioiTinh - nvarchar(10)
     @ns, -- dNgaySinh - date
     @dc,       -- sDiaChi - nvarchar(50)
     @sdt         -- sSDT - char(10)
     )
	 EXEC dbo.addKH @makh = 'KH09',         -- char(10)
	                @hoten = N'Nguyễn Văn An',       -- nvarchar(50)
	                @gt = N'Nam',          -- nvarchar(10)
	                @ns = '2002-03-30', -- date
	                @dc = N'Nam Đàn, Nghê An',          -- nvarchar(50)
	                @sdt = '094375674354'           -- char(10)
	 GO
    CREATE PROC updateKH 
	@makh CHAR(10), @hoten NVARCHAR(50), @gt NVARCHAR(10), @ns DATE, @dc NVARCHAR(50), @sdt CHAR(10)
	AS BEGIN
	       UPDATE dbo.tblKhachHang SET sTenKH = @hoten, sGioiTinh = @gt, dNgaySinh = @ns, sSDT = @sdt, sDiaChi = @dc
					WHERE PK_MaKH = @makh
	   END
	   GO
       
	   CREATE PROC delKH
	   @makh CHAR(10)
	   AS BEGIN
	          DELETE FROM dbo.tblKhachHang WHERE PK_MaKH = @makh
	      END
		
		    EXEC dbo.delKH @makh = 'KH01' -- char(10)
 SELECT * FROM dbo.tblKhachHang WHERE PK_MaKH IS NOT NULL AND sTenKH LIKE N'%Văn%'
 go 
 CREATE PROC rpKH
 @gt NVARCHAR(10) 
 AS BEGIN
        SELECT * FROM dbo.tblKhachHang WHERE sGioiTinh = @gt
    END
	go
	---------------Sửa chữa thiết bị
	CREATE VIEW dstbsc
AS SELECT dbo.tblNhatKiSuaChua.*, dbo.tblNhanVien.sTenNV, dbo.tblHangHoa.sTenHang
			FROM dbo.tblHangHoa, dbo.tblNhanVien, dbo.tblNhatKiSuaChua
			WHERE dbo.tblNhatKiSuaChua.sMaHang = dbo.tblHangHoa.PK_MaHang
					AND dbo.tblNhatKiSuaChua.sMaNVSua = dbo.tblNhanVien.PK_MaNV
	SELECT * FROM dstbsc
	GO
    CREATE PROC hienNKSC
	AS BEGIN
	       SELECT * FROM dbo.dstbsc
	   END
	   GO
       
CREATE PROC addSC
@ma CHAR(10), @mahang CHAR(10), @nn NVARCHAR(500), @gq NVARCHAR(500), @manv CHAR(10), @tgsua DATE, @tgtra DATE
AS BEGIN
       INSERT INTO dbo.tblNhatKiSuaChua
       (
           PK_MaSC,
           sMaHang,
           sNguyenNhan,
           sGiaiQuyet,
           sMaNVSua,
           sTGSua,
           sTGTra
       )
       VALUES
       (   @ma,        -- PK_MaSC - char(10)
           @mahang,        -- sMaHang - char(10)
           @nn,       -- sNguyenNhan - nvarchar(500)
           @gq,       -- sGiaiQuyet - nvarchar(500)
           @manv,        -- sMaNVSua - char(10)
           @tgsua, -- sTGSua - date
           @tgtra  -- sTGTra - date
           )
   END
   GO 
ALTER PROC updateSC
@ma CHAR(10), @mahang CHAR(10), @nn NVARCHAR(500), @gq NVARCHAR(500), @manv CHAR(10), @tgsua DATE, @tgtra DATE
AS BEGIN
       UPDATE dbo.tblNhatKiSuaChua SET sMaHang = @mahang, sNguyenNhan = @nn, sGiaiQuyet = @gq, sTGSua = @tgsua, sTGTra = @tgtra, sMaNVSua = @manv
			WHERE PK_MaSC = @ma 
   END
   GO 
   CREATE PROC delSC
@ma CHAR(10)
AS BEGIN
       DELETE FROM dbo.tblNhatKiSuaChua WHERE PK_MaSC = @ma
   END
   GO

CREATE PROC rpNKSC
@ma CHAR(10)
AS SELECT * FROM dstbsc WHERE PK_MaSC = @ma
go
-------------Hóa đơn
SELECT * FROM dbo.tblHoaDonBanHang
UPDATE dbo.tblHoaDonBanHang SET fTongTien  = (dbo.tblHangBan.iSoLuong * tblHangBan.fGiaBan) FROM dbo.tblHangBan
		WHERE dbo.tblHangBan.PK_MaHoaDon = tblHoaDonBanHang.PK_MaHoaDon
		GO
CREATE VIEW dshd
AS SELECT dbo.tblHoaDonBanHang.*, dbo.tblKhachHang.sTenKH, tblNhanVien.sTenNV 
	FROM dbo.tblKhachHang, dbo.tblHoaDonBanHang, dbo.tblNhanVien
	WHERE tblHoaDonBanHang.sMaNVLap = tblNhanVien.PK_MaNV AND tblHoaDonBanHang.sMaKH = tblKhachHang.PK_MaKH
GO 
CREATE PROC prdshd
AS SELECT * FROM dbo.dshd
GO

CREATE PROC addHD
@ma CHAR(10), @manv CHAR(10), @makh CHAR(10), @tglap DATE , @tttt NVARCHAR(10)
AS BEGIN
       INSERT INTO dbo.tblHoaDonBanHang
       (
           PK_MaHoaDon,
           sMaNVLap,
           sMaKH,
           dTGLap,
           
           bTTThuTien
       )
       VALUES
       (   @ma,        -- PK_MaHoaDon - char(10)
           @manv,        -- sMaNVLap - char(10)
           @makh,        -- sMaKH - char(10)
           @tglap, -- dTGLap - date
           
           @tttt        -- bTTThuTien - nvarchar(10)
           )
   END

   GO
   CREATE PROC updateHD
   @ma CHAR(10), @manv CHAR(10), @makh CHAR(10), @tglap DATE , @tttt NVARCHAR(10)
   AS BEGIN
          UPDATE dbo.tblHoaDonBanHang SET sMaKH = @makh, sMaNVLap = @manv, dTGLap = @tglap, bTTThuTien = bTTThuTien
		  WHERE PK_MaHoaDon = @ma 
      END
	  GO
      
	  CREATE PROC delHD
	  @ma CHAR(10)
	  AS BEGIN
	         DELETE FROM dbo.tblHoaDonBanHang WHERE PK_MaHoaDon = @ma
	     END
GO
ALTER VIEW rphd
AS SELECT dbo.tblNhanVien.sTenNV, tblKhachHang.sTenKH, tblHoaDonBanHang.bTTThuTien, tblHoaDonBanHang.fTongTien, tblHoaDonBanHang.dTGLap
		, dbo.tblHangBan.iSoLuong, dbo.tblHangBan.fGiaBan, dbo.tblHangHoa.sTenHang
		FROM dbo.tblHangBan, dbo.tblHoaDonBanHang, tblKhachHang, dbo.tblNhanVien, dbo.tblHangHoa
		WHERE tblHoaDonBanHang.PK_MaHoaDon = tblHangBan.PK_MaHoaDon AND tblHoaDonBanHang.sMaNVLap = dbo.tblNhanVien.PK_MaNV
				AND tblHoaDonBanHang.sMaKH = tblKhachHang.PK_MaKH AND dbo.tblHangHoa.PK_MaHang = dbo.tblHangBan.sMaHang

go
CREATE PROC rpDSHD
@ma CHAR(10)
AS BEGIN
       SELECT * FROM dbo.rphd WHERE PK_MaHoaDon = @ma
   END
   GO 

   -------------------PHIẾU NHẬN---------------------
  ALTER PROC hienPN
  AS  SELECT dbo.tblPhieuNhan.PK_MaPhieu,dbo.tblPhieuNhan.sDiaChi, dbo.tblPhieuNhan.dNgayNhanSua, dbo.tblPhieuNhan.dTGTra
				,dbo.tblKhachHang.sTenKH,  dbo.tblNhanVien.sTenNV FROM dbo.tblPhieuNhan, dbo.tblNhanVien, dbo.tblKhachHang
		WHERE dbo.tblNhanVien.PK_MaNV = dbo.tblPhieuNhan.sMaNVNhanSua AND dbo.tblKhachHang.PK_MaKH = dbo.tblPhieuNhan.sMaKH
		GO
		CREATE VIEW hienDSPN
		AS SELECT dbo.tblPhieuNhan.PK_MaPhieu,dbo.tblPhieuNhan.sDiaChi, dbo.tblPhieuNhan.dNgayNhanSua, dbo.tblPhieuNhan.dTGTra
				,dbo.tblKhachHang.sTenKH,  dbo.tblNhanVien.sTenNV FROM dbo.tblPhieuNhan, dbo.tblNhanVien, dbo.tblKhachHang
		WHERE dbo.tblNhanVien.PK_MaNV = dbo.tblPhieuNhan.sMaNVNhanSua AND dbo.tblKhachHang.PK_MaKH = dbo.tblPhieuNhan.sMaKH
		GO
CREATE PROC addPN
@ma CHAR(10), @makh CHAR(10), @manv CHAR(10), @dc NVARCHAR(50), @ngaysua DATE, @ngaytra DATE 
AS BEGIN
       INSERT INTO dbo.tblPhieuNhan
       (
           PK_MaPhieu,
           sMaKH,
          
           sDiaChi,
           sMaNVNhanSua,
           dNgayNhanSua,
           dTGTra
       )
       VALUES
       (   @ma,        -- PK_MaPhieu - char(10)
           @makh,        -- sMaKH - char(10)
          
           @dc,       -- sDiaChi - nvarchar(50)
           @manv,        -- sMaNVNhanSua - char(10)
           @ngaysua, -- dNgayNhanSua - date
           @ngaytra  -- dTGTra - date
           )
   END
GO 
CREATE PROC updatePN
@ma CHAR(10), @makh CHAR(10), @manv CHAR(10), @dc NVARCHAR(50), @ngaysua DATE, @ngaytra DATE 
AS BEGIN
       UPDATE dbo.tblPhieuNhan SET sMaKH = @makh, sMaNVNhanSua = @manv, sDiaChi = @dc, dNgayNhanSua = @ngaysua, dTGTra = @ngaytra
				WHERE PK_MaPhieu = @ma
   END
   GO
CREATE PROC delPN
@ma CHAR(10)
AS BEGIN
       DELETE FROM dbo.tblPhieuNhan WHERE PK_MaPhieu = @ma
	END
go
	ALTER PROC rpPN
@ma CHAR(10)
AS BEGIN
       SELECT * FROM dbo.hienDSPN WHERE PK_MaPhieu = @ma
	END