drop Procedure [dbo].[sp_UndoChinhSuaNhanVien]
go

SET QUOTED_IDENTIFIER ON
go
SET ANSI_NULLS ON
go
CREATE PROC [dbo].[sp_UndoChinhSuaNhanVien]
(
	@HO nvarchar(40),
	@TEN nvarchar(10),
	@DIACHI nvarchar(40),
	@NGAYSINH datetime,
	@LUONG float,
	@Original_MANV numeric
)
AS
BEGIN
	UPDATE [DBO].[NHANVIEN]
	SET HO = @HO, TEN = @TEN, DIACHI = @DIACHI, NGAYSINH = @NGAYSINH, LUONG = @LUONG
	WHERE (MANV = @Original_MANV)
END	 
go
