drop Procedure [dbo].[sp_ChinhSuaNhanVien]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_ChinhSuaNhanVien] 
(
	@MANV numeric,
	@HO nvarchar(40),
	@TEN nvarchar(10),
	@DIACHI nvarchar(40),
	@NGAYSINH datetime,
	@LUONG float,
	@MACN char(10)
)
AS
BEGIN
	UPDATE [dbo].[NHANVIEN]
	SET [HO] = @HO, [TEN] = @TEN, [DIACHI] = @DIACHI, [NGAYSINH] = @NGAYSINH, [LUONG] = @LUONG, [MACN] = @MACN
	WHERE [MANV] = @MANV
END	
go
