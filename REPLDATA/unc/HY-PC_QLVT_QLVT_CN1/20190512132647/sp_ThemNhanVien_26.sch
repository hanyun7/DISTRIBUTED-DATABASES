drop Procedure [dbo].[sp_ThemNhanVien]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_ThemNhanVien]
(
	@MANV numeric(18,0),
	@HO nvarchar(40),
	@TEN nvarchar(10),
	@DIACHI nvarchar(40),
	@NGAYSINH datetime,
	@LUONG float,
	@MACN char(10)
)
AS
BEGIN
	INSERT [dbo].[NHANVIEN] ([MANV], [HO], [TEN], [DIACHI], [NGAYSINH], [LUONG], [MACN]) VALUES (@MANV,@HO,@TEN,@DIACHI, CAST(@NGAYSINH AS DateTime),@LUONG,@MACN)
END	
go
